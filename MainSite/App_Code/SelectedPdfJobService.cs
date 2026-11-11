using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using DAL.Appraisal;

namespace HRIS.Appraisal
{
    public class SelectedPdfRequestItem
    {
        public int EmpInfoId { get; set; }
        public int FinancialYearId { get; set; }
        public string OptionInfo { get; set; }
        public string FinancialYearDesc { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
    }

    public class SelectedPdfJob
    {
        public string JobId { get; set; }
        public int Total { get; set; }
        public int Done;
        public int Failed;
        public string CurrentName { get; set; }
        public bool IsCompleted { get; set; }
        public string Error { get; set; }
        public byte[] ZipBytes { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public readonly List<string> ItemErrors = new List<string>();
    }

    public static class SelectedPdfJobService
    {
        private static readonly ConcurrentDictionary<string, SelectedPdfJob> Jobs = new ConcurrentDictionary<string, SelectedPdfJob>();

        public static string StartJob(List<SelectedPdfRequestItem> items, string reportsBasePath)
        {
            string jobId = Guid.NewGuid().ToString("N");

            SelectedPdfJob job = new SelectedPdfJob
            {
                JobId = jobId,
                Total = items.Count,
                Done = 0,
                Failed = 0,
                IsCompleted = false,
                CreatedAtUtc = DateTime.UtcNow
            };

            Jobs[jobId] = job;

            Task.Run(() => RunJob(job, items, reportsBasePath));

            CleanupOldJobs();

            return jobId;
        }

        public static SelectedPdfJob GetJob(string jobId)
        {
            SelectedPdfJob job;
            Jobs.TryGetValue(jobId, out job);
            return job;
        }

        public static void RemoveJob(string jobId)
        {
            SelectedPdfJob job;
            Jobs.TryRemove(jobId, out job);
        }

        private static void CleanupOldJobs()
        {
            DateTime cutoff = DateTime.UtcNow.AddHours(-1);
            foreach (KeyValuePair<string, SelectedPdfJob> kvp in Jobs)
            {
                if (kvp.Value.IsCompleted && kvp.Value.CreatedAtUtc < cutoff)
                {
                    SelectedPdfJob removed;
                    Jobs.TryRemove(kvp.Key, out removed);
                }
            }
        }

        private static void RunJob(SelectedPdfJob job, List<SelectedPdfRequestItem> items, string reportsBasePath)
        {
            AppraisalFunctionalPartDAL appPartAForPdf = new AppraisalFunctionalPartDAL();
            BSCAppraisalFunctionalPartDAL bscAppPartAForPdf = new BSCAppraisalFunctionalPartDAL();
            AppraisalPartBDAL appraisalPartBdalForPdf = new AppraisalPartBDAL();

            try
            {
                using (MemoryStream zipStream = new MemoryStream())
                {
                    using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                    {
                        foreach (SelectedPdfRequestItem item in items)
                        {
                            job.CurrentName = item.EmpName;

                            try
                            {
                                byte[] pdfBytes;

                                if (item.OptionInfo == "BSC" || item.OptionInfo == "OKR")
                                {
                                    pdfBytes = GenerateBscOkrPdf(bscAppPartAForPdf, appraisalPartBdalForPdf, item.EmpInfoId, item.FinancialYearId, item.OptionInfo, reportsBasePath);
                                }
                                else
                                {
                                    pdfBytes = GenerateKpiPdf(appPartAForPdf, appraisalPartBdalForPdf, item.EmpInfoId, item.FinancialYearId, reportsBasePath);
                                }

                                if (pdfBytes != null && pdfBytes.Length > 0)
                                {
                                    string fileName = SanitizeFileName(item.EmpName + "(" + item.EmpCode + ")-" + item.FinancialYearDesc) + ".pdf";
                                    ZipArchiveEntry entry = archive.CreateEntry(fileName, CompressionLevel.Optimal);
                                    using (Stream entryStream = entry.Open())
                                    {
                                        entryStream.Write(pdfBytes, 0, pdfBytes.Length);
                                    }
                                }
                                else
                                {
                                    Interlocked.Increment(ref job.Failed);
                                    job.ItemErrors.Add(item.EmpName + ": No report data found for the selected year.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Interlocked.Increment(ref job.Failed);
                                job.ItemErrors.Add(item.EmpName + ": " + ex.Message);
                            }

                            Interlocked.Increment(ref job.Done);
                        }
                    }

                    job.ZipBytes = zipStream.ToArray();
                }

                job.IsCompleted = true;
            }
            catch (Exception ex)
            {
                job.Error = ex.Message;
                job.IsCompleted = true;
            }
        }

        private static byte[] GenerateKpiPdf(AppraisalFunctionalPartDAL appPartAForPdf, AppraisalPartBDAL appraisalPartBdalForPdf, int empId, int finYear, string reportsBasePath)
        {
            DataSet mainDS = new DataSet();

            DataTable allDataTable = appPartAForPdf.GetEmployeeDetailsKPIREport(empId, finYear).Copy();
            allDataTable.TableName = "EmployeeInfoListDataTable";
            mainDS.Tables.Add(allDataTable);

            DataTable dtfin = appPartAForPdf.GetFinYear(finYear);
            string finYeartxt = "";
            if (dtfin.Rows.Count > 0)
            {
                finYeartxt = dtfin.Rows[0]["FinancialYearDesc"].ToString();
            }

            DataTable dtaa = appPartAForPdf.GetApprsaisalSelfByEmpFinYear(empId, finYeartxt);

            if (dtaa.Rows.Count > 0)
            {
                int mid = Convert.ToInt32(dtaa.Rows[0][0]);

                DataTable dtMaster = appPartAForPdf.GetAppraisalMAstet(mid).Copy();
                dtMaster.TableName = "GetAppraisalSelfMaster";
                mainDS.Tables.Add(dtMaster);

                DataTable gtGetApprisalMasterId = appPartAForPdf.GetAppraisalMasterIdFromAppraisalSelfMasterId(mid);

                int AppraisalMasterId = 0;
                if (gtGetApprisalMasterId.Rows.Count > 0)
                {
                    AppraisalMasterId = Convert.ToInt32(gtGetApprisalMasterId.Rows[0]["AppraisalMasterId"].ToString());
                }

                DataTable dtFunc = appPartAForPdf.GetAppraisalFuncDetails(AppraisalMasterId).Copy();

                if (dtFunc.Rows.Count > 0)
                {
                    dtFunc.TableName = "GetAppraisalSelfDetails";
                    mainDS.Tables.Add(dtFunc);
                }
                else
                {
                    DataTable dt2 = appPartAForPdf.GetAppraisalSelfDetails(mid).Copy();
                    dt2.TableName = "GetAppraisalSelfDetails";
                    mainDS.Tables.Add(dt2);
                }

                DataTable dtAppraisalfB = appPartAForPdf.GetAppraisalfB(AppraisalMasterId).Copy();

                if (dtAppraisalfB.Rows.Count > 0)
                {
                    dtAppraisalfB.TableName = "GetAppraisalSelfB";
                    mainDS.Tables.Add(dtAppraisalfB);
                }
                else
                {
                    DataTable d3 = appPartAForPdf.GetAppraisalSelfB_Rpt(mid).Copy();
                    d3.TableName = "GetAppraisalSelfB";
                    mainDS.Tables.Add(d3);
                }

                DataTable dtw2 = appPartAForPdf.GetAppraisalfDetailsFromSup(mid).Copy();
                dtw2.TableName = "GetAppraisalFuncAreaapp";
                mainDS.Tables.Add(dtw2);

                DataTable dtTrain = appPartAForPdf.GetAppraisalTraining(AppraisalMasterId).Copy();
                dtTrain.TableName = "AppraisalTraining";
                mainDS.Tables.Add(dtTrain);

                DataTable dtFinalStatus = appraisalPartBdalForPdf.GetAppraiSalFinalStatusrpt(AppraisalMasterId).Copy();

                if (dtFinalStatus.Rows.Count > 0)
                {
                    try
                    {
                        decimal funcMark = 0;
                        decimal behaveMark = 0;
                        try { funcMark = dtFinalStatus.AsEnumerable().Sum(r => r.Field<decimal>("funcMark")); } catch (Exception) { }
                        try { behaveMark = dtFinalStatus.AsEnumerable().Sum(r => r.Field<decimal>("behaveMark")); } catch (Exception) { }
                        decimal res = funcMark + behaveMark;
                        if (res == 0)
                        {
                            dtFinalStatus = appraisalPartBdalForPdf.GetAppraiSalFinalStatusrptSelf(AppraisalMasterId).Copy();
                        }
                    }
                    catch (Exception) { }
                }

                if (dtFinalStatus.Rows.Count == 0)
                {
                    dtFinalStatus = appraisalPartBdalForPdf.GetAppraiSalFinalStatusrptSelf(AppraisalMasterId).Copy();

                    if (dtFinalStatus.Rows.Count == 0)
                    {
                        dtFinalStatus = appraisalPartBdalForPdf.GetAppraiSalFinalStatusrptSelf_KPI(mid).Copy();
                    }
                }

                dtFinalStatus.TableName = "AppraisalFinalStatus";
                mainDS.Tables.Add(dtFinalStatus);
            }

            if (mainDS.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            return ExportReportToPdfBytes(mainDS, "crpKPIInformamationDetails.rpt", reportsBasePath);
        }

        private static byte[] GenerateBscOkrPdf(BSCAppraisalFunctionalPartDAL bscAppPartAForPdf, AppraisalPartBDAL appraisalPartBdalForPdf, int empId, int finYear, string op, string reportsBasePath)
        {
            bool MyOp = op == "BSC";
            DataSet mainDS = new DataSet();

            DataTable allDataTable = bscAppPartAForPdf.GetEmployeeDetailsKPIREport(empId, finYear).Copy();
            allDataTable.TableName = "EmployeeInfoListDataTable";
            mainDS.Tables.Add(allDataTable);

            DataTable dtaa = bscAppPartAForPdf.GetApprsaisalSelfByEmpFinYear(empId, finYear);

            if (dtaa.Rows.Count > 0)
            {
                int mid = Convert.ToInt32(dtaa.Rows[0][0]);

                DataTable dtMaster = bscAppPartAForPdf.GetAppraisalMAstet(mid).Copy();
                dtMaster.TableName = "GetAppraisalSelfMaster";
                mainDS.Tables.Add(dtMaster);

                DataTable gtGetApprisalMasterId = bscAppPartAForPdf.GetAppraisalMasterIdFromAppraisalSelfMasterId(mid);

                int AppraisalMasterId = 0;
                if (gtGetApprisalMasterId.Rows.Count > 0)
                {
                    AppraisalMasterId = Convert.ToInt32(gtGetApprisalMasterId.Rows[0]["BSCAppraisalMasterId"].ToString());
                }

                DataTable dtFunc = bscAppPartAForPdf.GetAppraisalFuncRpt(AppraisalMasterId, MyOp).Copy();

                if (dtFunc.Rows.Count > 0)
                {
                    dtFunc.TableName = "GetAppraisalSelfDetails";
                    mainDS.Tables.Add(dtFunc);
                }
                else
                {
                    DataTable dt2 = bscAppPartAForPdf.GetAppraisalSelfFuncRpt(mid, MyOp).Copy();
                    dt2.TableName = "GetAppraisalSelfDetails";
                    mainDS.Tables.Add(dt2);
                }

                DataTable dtAppraisalfB = bscAppPartAForPdf.GetAppraisalB_Rpt(AppraisalMasterId).Copy();

                if (dtAppraisalfB.Rows.Count > 0)
                {
                    dtAppraisalfB.TableName = "GetAppraisalSelfB";
                    mainDS.Tables.Add(dtAppraisalfB);
                }
                else
                {
                    DataTable d3 = bscAppPartAForPdf.GetAppraisalSelfB_Rpt(mid).Copy();
                    d3.TableName = "GetAppraisalSelfB";
                    mainDS.Tables.Add(d3);
                }

                DataTable dtw2 = bscAppPartAForPdf.GetAppraisalfDetailsFromSup(mid).Copy();
                dtw2.TableName = "GetAppraisalFuncAreaapp";
                mainDS.Tables.Add(dtw2);

                DataTable dtTrain = bscAppPartAForPdf.GetAppraisalTraining(AppraisalMasterId).Copy();
                dtTrain.TableName = "AppraisalTraining";
                mainDS.Tables.Add(dtTrain);

                DataTable dtFinalStatus = appraisalPartBdalForPdf.GetAppraiSalFinalStatusrptBSCOKR(AppraisalMasterId).Copy();

                if (dtFinalStatus.Rows.Count > 0)
                {
                    try
                    {
                        decimal funcMark = 0;
                        decimal behaveMark = 0;
                        try { funcMark = dtFinalStatus.AsEnumerable().Sum(r => r.Field<decimal>("funcMark")); } catch (Exception) { }
                        try { behaveMark = dtFinalStatus.AsEnumerable().Sum(r => r.Field<decimal>("behaveMark")); } catch (Exception) { }
                        decimal res = funcMark + behaveMark;
                        if (res == 0)
                        {
                            dtFinalStatus = appraisalPartBdalForPdf.GetAppraiSalFinalStatusrptSelf(AppraisalMasterId).Copy();
                        }
                    }
                    catch (Exception) { }
                }

                if (dtFinalStatus.Rows.Count == 0)
                {
                    dtFinalStatus = appraisalPartBdalForPdf.GetAppraiSalFinalStatusrptSelfBSCOKR(AppraisalMasterId).Copy();

                    if (dtFinalStatus.Rows.Count == 0)
                    {
                        dtFinalStatus = appraisalPartBdalForPdf.GetAppraiSalFinalStatusrptSelf_KPIBSCOKR(mid).Copy();
                    }
                }

                dtFinalStatus.TableName = "AppraisalFinalStatus";
                mainDS.Tables.Add(dtFinalStatus);
            }

            if (mainDS.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            return ExportReportToPdfBytes(mainDS, "crpBSCKPIInformamationDetails.rpt", reportsBasePath);
        }

        private static byte[] ExportReportToPdfBytes(DataSet dsDataSet, string reportName, string reportsBasePath)
        {
            ReportDocument rptdoc = new ReportDocument();
            try
            {
                rptdoc.Load(Path.Combine(reportsBasePath, reportName));
                rptdoc.SetDataSource(dsDataSet);

                using (Stream stream = rptdoc.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    return ms.ToArray();
                }
            }
            finally
            {
                rptdoc.Close();
                rptdoc.Dispose();
            }
        }

        private static string SanitizeFileName(string name)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '_');
            }
            return name;
        }
    }
}
