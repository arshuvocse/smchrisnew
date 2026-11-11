using System;
using System.Data;
using System.Linq;
using CrystalDecisions.CrystalReports.Engine;
using DAL.Appraisal;

public partial class Report_UI_MOKRBSCInformationReportViewer : System.Web.UI.Page
{
    ReportDocument rptdoc = new ReportDocument();

    private BSCAppraisalFunctionalPartDAL _appPartA = new BSCAppraisalFunctionalPartDAL();
    private AppraisalPartBDAL _appraisalPartBdal = new AppraisalPartBDAL();

    protected void Page_Init(object sender, EventArgs e)
    {
        int empId = int.Parse(Request.QueryString["EmpInfoId"]);
        int finYear = int.Parse(Request.QueryString["financialYearId"]);
        string op = Request.QueryString["M"].ToString();

        bool myOp = op == "BSC";
        DataSet mainDS = new DataSet();

        DataTable allDataTable = _appPartA.GetEmployeeDetailsOKRREportMIDYear(empId, finYear).Copy();
        allDataTable.TableName = "EmployeeInfoListDataTable";
        mainDS.Tables.Add(allDataTable);

        DataTable dtaa = _appPartA.GetApprsaisalSelfByEmpFinYearMIDYear(empId, finYear);
        if (dtaa.Rows.Count > 0)
        {
            int mid = Convert.ToInt32(dtaa.Rows[0][0]);

            DataTable dtMaster = _appPartA.GetAppraisalMAstet(mid).Copy();
            dtMaster.TableName = "GetAppraisalSelfMaster";
            mainDS.Tables.Add(dtMaster);

            DataTable gtGetApprisalMasterId = _appPartA.GetAppraisalMasterIdFromAppraisalSelfMasterIdMIdYear(mid);

            int appraisalMasterId = 0;
            if (gtGetApprisalMasterId.Rows.Count > 0)
            {
                appraisalMasterId = Convert.ToInt32(gtGetApprisalMasterId.Rows[0]["BSCAppraisalMasterId"].ToString());
            }

            DataTable dtFunc = _appPartA.GetAppraisalFuncRptMIDYear(appraisalMasterId, myOp).Copy();
            if (dtFunc.Rows.Count > 0)
            {
                dtFunc.TableName = "GetAppraisalSelfDetails";
                mainDS.Tables.Add(dtFunc);
            }
            else
            {
                DataTable dt2 = _appPartA.GetAppraisalSelfFuncRpt(mid, myOp).Copy();
                dt2.TableName = "GetAppraisalSelfDetails";
                mainDS.Tables.Add(dt2);
            }

            DataTable dtAppraisalfB = _appPartA.GetAppraisalB_RptMIDYear(appraisalMasterId).Copy();
            if (dtAppraisalfB.Rows.Count > 0)
            {
                dtAppraisalfB.TableName = "GetAppraisalSelfB";
                mainDS.Tables.Add(dtAppraisalfB);
            }
            else
            {
                DataTable d3 = _appPartA.GetAppraisalSelfB_Rpt(mid).Copy();
                d3.TableName = "GetAppraisalSelfB";
                mainDS.Tables.Add(d3);
            }

            DataTable dtw2 = _appPartA.GetAppraisalfDetailsFromSupMidYear(mid).Copy();
            dtw2.TableName = "GetAppraisalFuncAreaapp";
            mainDS.Tables.Add(dtw2);

            DataTable dtTrain = _appPartA.GetAppraisalTraining(appraisalMasterId).Copy();
            dtTrain.TableName = "AppraisalTraining";
            mainDS.Tables.Add(dtTrain);

            DataTable dtFinalStatus = _appraisalPartBdal.GetAppraiSalFinalStatusrptBSCOKRMIDYEAR(appraisalMasterId).Copy();
            if (dtFinalStatus.Rows.Count > 0)
            {
                try
                {
                    decimal funcMark = 0;
                    decimal behaveMark = 0;
                    try
                    {
                        funcMark = dtFinalStatus.AsEnumerable().Sum(row => row.Field<decimal>("funcMark"));
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {
                        behaveMark = dtFinalStatus.AsEnumerable().Sum(row => row.Field<decimal>("behaveMark"));
                    }
                    catch (Exception)
                    {
                    }

                    decimal res = funcMark + behaveMark;
                    if (res == 0)
                    {
                        dtFinalStatus = _appraisalPartBdal.GetAppraiSalFinalStatusrptSelfMidYear(appraisalMasterId).Copy();
                    }
                }
                catch (Exception)
                {
                }
            }

            if (dtFinalStatus.Rows.Count == 0)
            {
                dtFinalStatus = _appraisalPartBdal.GetAppraiSalFinalStatusrptSelfBSCOKR(appraisalMasterId).Copy();
                if (dtFinalStatus.Rows.Count == 0)
                {
                    dtFinalStatus = _appraisalPartBdal.GetAppraiSalFinalStatusrptSelf_KPIBSCOKR(mid).Copy();
                }
            }

            dtFinalStatus.TableName = "AppraisalFinalStatus";
            mainDS.Tables.Add(dtFinalStatus);
        }

        if (mainDS.Tables.Count > 0 && mainDS.Tables[0].Rows.Count > 0)
        {
            ShowReport(mainDS, "crpBSCKPIMidYearInformamationDetails.rpt");
        }
    }

    private void ShowReport(DataSet dsDataSet, string reportName)
    {
        if (dsDataSet.Tables[0].Rows.Count > 0)
        {
            rptdoc.Load(ReportPath(reportName));
            rptdoc.SetDataSource(dsDataSet);
            crReportViewer.ReportSource = rptdoc;
            crReportViewer.DataBind();
        }
        else
        {
            lblMsg.Text = "No Data Found!!!!";
        }
    }

    private string ReportPath(string rptName)
    {
        return Convert.ToString(Server.MapPath("~\\Reports\\CrystalReports\\" + rptName));
    }

    protected void rptViewerBasic_Unload(object sender, EventArgs e)
    {
        if (this.rptdoc != null)
        {
            rptdoc.Close();
            rptdoc.Dispose();
            crReportViewer.Dispose();
        }
    }

    protected void rptViewerBasic_Disposed(object sender, EventArgs e)
    {
        if (this.rptdoc != null)
        {
            rptdoc.Close();
            rptdoc.Dispose();
            crReportViewer.Dispose();
        }
    }
}
