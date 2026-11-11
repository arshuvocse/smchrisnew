using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using DAL.HealthCare_DAL;

public partial class Report_UI_HC_TopSheetReportViewer : System.Web.UI.Page
{
    ReportDocument rptdoc = new ReportDocument();

    private TopSheetDal aSheetDal = new TopSheetDal();

    protected void Page_Init(object sender, EventArgs e)
    {
        string rptType = Request.QueryString["rptType"];

        string rptexcel = Request.QueryString["rptexcel"];

        DataSet mainDS = new DataSet();

        if (rptType != "")
        {

            DataTable aMasterTable = new DataTable();
            if (string.Equals(rptexcel, "CSV", StringComparison.OrdinalIgnoreCase))
            {
                aMasterTable = GetTopSheetCsvData(Convert.ToInt32(rptType)).Copy();
                aMasterTable.TableName = "HealthCareTopSheet";
                mainDS.Tables.Add(aMasterTable);
            }

            else
            {
                aMasterTable = aSheetDal.Get_TopSheet_RPT(Convert.ToInt32(rptType)).Copy();
                aMasterTable.TableName = "HealthCareTopSheet";
                mainDS.Tables.Add(aMasterTable);
            }

              

            string CompanyId = aMasterTable.Rows[0]["CompanyId"].ToString(); ;
            string ApplicationType = aMasterTable.Rows[0]["Type"].ToString(); ;
            string SalaryLoationId = aMasterTable.Rows[0]["SalaryLoationId"].ToString(); ;

            DataTable dtConvenor_MemberSecretory = new DataTable();
            dtConvenor_MemberSecretory = aSheetDal.Get_Convenor_MemberSecretory_RPT(ApplicationType, CompanyId, SalaryLoationId).Copy();
            dtConvenor_MemberSecretory.TableName = "dtConvenor_MemberSecretory";
            mainDS.Tables.Add(dtConvenor_MemberSecretory);

            DataTable dtMember = new DataTable();
            dtMember = aSheetDal.Get_Member_RPT(ApplicationType, CompanyId, SalaryLoationId).Copy();
            dtMember.TableName = "dtMember";
            mainDS.Tables.Add(dtMember);

            if (mainDS.Tables[0].Rows.Count > 0)
            {
                if (string.Equals(rptexcel, "Exvl", StringComparison.OrdinalIgnoreCase))
                {
                    ExportHealthCareTopSheetExcel(mainDS.Tables["HealthCareTopSheet"], "Expenses Reimbursement");
                    return;
                }
                else if (string.Equals(rptexcel, "CSV", StringComparison.OrdinalIgnoreCase))
                {
                    ExportHealthCareTopSheetCsv(mainDS.Tables["HealthCareTopSheet"], "Expenses Reimbursement");
                    return;
                }
                else
                {
                    ShowReport(mainDS, "crpMeetingGen_HC.rpt");
                    //crpMeetingGen_HC_Excel
                    //    mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsHC_TopSheet.xsd"));
                }
            }

        }
    }

    private DataTable GetTopSheetCsvData(int rptType)
    {
        var csvMethod = aSheetDal.GetType().GetMethod("Get_TopSheet_RPTCSV");
        if (csvMethod != null)
        {
            object csvResult = csvMethod.Invoke(aSheetDal, new object[] { rptType });
            DataTable csvTable = csvResult as DataTable;
            if (csvTable != null)
            {
                return csvTable;
            }
        }

        return aSheetDal.Get_TopSheet_RPT(rptType);
    }

    private void ExportHealthCareTopSheetExcel(DataTable dataTable, string fileName)
    {
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
            lblMsg.Text = "No Data Found!!!!";
            return;
        }

        StringBuilder excelBuilder = new StringBuilder();
        string[] headers = GetTopSheetExportHeaders();

        excelBuilder.AppendLine("<html>");
        excelBuilder.AppendLine("<head><meta charset='utf-8' /></head>");
        excelBuilder.AppendLine("<body>");
        excelBuilder.AppendLine("<table border='1' cellspacing='0' cellpadding='2'>");

        excelBuilder.Append("<tr>");
        foreach (string header in headers)
        {
            excelBuilder.Append("<th style='background-color:#f2f2f2;font-weight:bold;'>");
            excelBuilder.Append(HttpUtility.HtmlEncode(header));
            excelBuilder.Append("</th>");
        }
        excelBuilder.AppendLine("</tr>");

        foreach (DataRow dataRow in dataTable.Rows)
        {
            string[] values = GetTopSheetExportValues(dataRow);
            excelBuilder.Append("<tr>");

            foreach (string value in values)
            {
                excelBuilder.Append("<td style=\"mso-number-format:'\\@';\">");
                excelBuilder.Append(HttpUtility.HtmlEncode(value ?? string.Empty));
                excelBuilder.Append("</td>");
            }

            excelBuilder.AppendLine("</tr>");
        }

        excelBuilder.AppendLine("</table>");
        excelBuilder.AppendLine("</body>");
        excelBuilder.AppendLine("</html>");

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.ContentEncoding = Encoding.UTF8;
        Response.Charset = "utf-8";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + ".xls\"");
        Response.BinaryWrite(Encoding.UTF8.GetPreamble());
        Response.Write(excelBuilder.ToString());
        Response.Flush();
        Response.SuppressContent = true;
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }

    private void ExportHealthCareTopSheetCsv(DataTable dataTable, string fileName)
    {
        if (dataTable == null || dataTable.Rows.Count == 0)
        {
            lblMsg.Text = "No Data Found!!!!";
            return;
        }

        StringBuilder csvBuilder = new StringBuilder();
        string[] headers = GetTopSheetCsvExportHeaders();

        AppendCsvLine(csvBuilder, headers);

        foreach (DataRow dataRow in dataTable.Rows)
        {
            string[] values = GetTopSheetCsvExportValues(dataRow);

            AppendCsvLine(csvBuilder, values);
        }

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "text/csv";
        Response.ContentEncoding = Encoding.UTF8;
        Response.Charset = "utf-8";
        Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + ".csv\"");
        Response.BinaryWrite(Encoding.UTF8.GetPreamble());
        Response.Write(csvBuilder.ToString());
        Response.Flush();
        Response.SuppressContent = true;
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }

    private static string[] GetTopSheetCsvExportHeaders()
    {
        return new[]
        {
            "SL",
            "CustomerReference",
            "PayeeName",
            "PayeeBankAccNo",
            "PayeeAccType",
            "PayeeBankRouting",
            "Amount",
            "Reason",
            "PaymentDate",
            "DebitACNo",
            "PayeeEmailAddress"
        };
    }

    private static string[] GetTopSheetCsvExportValues(DataRow dataRow)
    {
        return new[]
        {
            GetColumnValue(dataRow, "SL"),
            GetColumnValue(dataRow, "CustomerReference"),
            GetColumnValue(dataRow, "PayeeName"),
            GetColumnValue(dataRow, "PayeeBankAccNo"),
            GetColumnValue(dataRow, "PayeeAccType"),
            GetColumnValue(dataRow, "PayeeBankRouting"),
            GetColumnValue(dataRow, "Amount"),
            GetColumnValue(dataRow, "Reason"),
            GetColumnValue(dataRow, "PaymentDate"),
            GetColumnValue(dataRow, "Debit A/C No."),
            GetColumnValue(dataRow, "PayeeEmailAddress")
        };
    }

    private static string[] GetTopSheetExportHeaders()
    {
        return new[]
        {
            "SL",
            "Emp. ID",
            "Emp Name",
            "Designation",
            "Section",
            "Department",
            "Job Location",
            "Name of Patient",
            "Relationship",
            "Illness Description",
            "Submit Date",
            "Dr. visit date (s)",
            "Bill received date by HRD",
            "Attached Documents",
            "Claim Amount",
            "Actual Amount to be disbursed",
            "Bank & Branch Name",
            "Account No",
            "Routing No",
            "Comments",
            "Actual Amount",
            "Hospital Name",
            "Hospital Addmission and Discharge Date",
            "Remining Blanace",
            "HOD Approve Date",
            "Remarks"
        };
    }

    private static string[] GetTopSheetExportValues(DataRow dataRow)
    {
        string bankAndBranch = JoinNonEmpty(" - ",
            GetColumnValue(dataRow, "BankName"),
            GetColumnValue(dataRow, "BranchName"));

        return new[]
        {
            GetColumnValue(dataRow, "SL"),
            GetColumnValue(dataRow, "EmpMasterCode"),
            GetColumnValue(dataRow, "EmpName"),
            GetColumnValue(dataRow, "Designation"),
            GetColumnValue(dataRow, "SectionName", "ClosingBalance"),
            GetColumnValue(dataRow, "DepartmentName"),
            GetColumnValue(dataRow, "SalaryLocation"),
            GetColumnValue(dataRow, "Illness" ),
            GetColumnValue(dataRow, "Relationship", "Relationship1"),
            GetColumnValue(dataRow, "PatientName"),
            GetColumnValue(dataRow, "SubmitDate", "SubmitDate1"),
            GetColumnValue(dataRow, "DocVisitDate"),
            GetColumnValue(dataRow, "SubmitDate", "SubmitDate1", "BillAmount"),
            GetColumnValue(dataRow, "DocumentStatus"),
            GetColumnValue(dataRow, "Ceilling"),
            GetColumnValue(dataRow, "ApplicableAmount"),
            bankAndBranch,
            GetColumnValue(dataRow, "BankAccountNo"),
            GetColumnValue(dataRow, "RoutingNo"),
            GetColumnValue(dataRow, "Comments", "Venue"),
            GetColumnValue(dataRow, "ActualAmount"),
            GetColumnValue(dataRow, "HospitalName"),
            GetColumnValue(dataRow, "HospitalAddmissionDischargeDate"),
            GetColumnValue(dataRow, "ReminingBlanace"),
            GetColumnValue(dataRow, "HeadofDptDate", "HeadOfDptDate", "HODApproveDate", "HodApproveDate"),
            GetColumnValue(dataRow, "BlankRemarks")
        };
    }

    private static void AppendCsvLine(StringBuilder csvBuilder, IEnumerable<string> values)
    {
        bool first = true;
        foreach (string value in values)
        {
            if (!first)
            {
                csvBuilder.Append(",");
            }

            csvBuilder.Append(EscapeCsvValue(value));
            first = false;
        }

        csvBuilder.AppendLine();
    }

    private static string GetColumnValue(DataRow row, params string[] candidateNames)
    {
        foreach (string candidateName in candidateNames)
        {
            DataColumn exactColumn = row.Table.Columns
                .Cast<DataColumn>()
                .FirstOrDefault(c => string.Equals(c.ColumnName, candidateName, StringComparison.OrdinalIgnoreCase));

            if (exactColumn != null)
            {
                return NormalizeCellValue(row[exactColumn]);
            }

            DataColumn prefixedColumn = row.Table.Columns
                .Cast<DataColumn>()
                .FirstOrDefault(c => c.ColumnName.StartsWith(candidateName, StringComparison.OrdinalIgnoreCase));

            if (prefixedColumn != null)
            {
                return NormalizeCellValue(row[prefixedColumn]);
            }
        }

        return string.Empty;
    }

    private static string NormalizeCellValue(object value)
    {
        if (value == null || value == DBNull.Value)
        {
            return string.Empty;
        }

        DateTime dateValue;
        if (value is DateTime)
        {
            dateValue = (DateTime)value;
            return dateValue.ToString("dd-MMM-yyyy");
        }

        return Convert.ToString(value).Trim();
    }

    private static string JoinNonEmpty(string separator, params string[] values)
    {
        return string.Join(separator, values.Where(v => !string.IsNullOrWhiteSpace(v)).ToArray());
    }

    private static string EscapeCsvValue(string value)
    {
        if (value == null)
        {
            return "\"\"";
        }

        string cleanedValue = value.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " ");
        return "\"" + cleanedValue.Replace("\"", "\"\"") + "\"";
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
