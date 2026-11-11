using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using DAL.HealthCare_DAL;
using DAL.UserPermissions_DAL;

public partial class Report_UI_ReimbursmentFormReportViewer : System.Web.UI.Page
{
    ReportDocument rptdoc = new ReportDocument();

    ReimbursmentFormDal aFormDal = new ReimbursmentFormDal();

    protected void Page_Init(object sender, EventArgs e)
    {
        string rptType = Request.QueryString["rptType"];

        DataSet mainDS = new DataSet();

        if (rptType != "")
        {

            DataTable aMasterTable = new DataTable();
            aMasterTable = aFormDal.Get_ReimbusrmentMasterForRPT(Convert.ToInt32(rptType)).Copy();
            aMasterTable.TableName = "ReimbusrmentFormMaster";
            mainDS.Tables.Add(aMasterTable);

            DataTable riefDescription = new DataTable();
            riefDescription = aFormDal.Get_briefDescriptionForRPT(Convert.ToInt32(rptType)).Copy();
            riefDescription.TableName = "BriefDescriptionOfIllness";
            mainDS.Tables.Add(riefDescription);

            DataTable EnClosuresTickMark = new DataTable();
            EnClosuresTickMark = aFormDal.Get_EnClosuresTickMarkForRPT(Convert.ToInt32(rptType)).Copy();
            EnClosuresTickMark.TableName = "EnClosuresTickMark";
            mainDS.Tables.Add(EnClosuresTickMark);

            DataTable ClaimDetsils = new DataTable();
            ClaimDetsils = aFormDal.Get_ClaimDetailsForRPT(Convert.ToInt32(rptType)).Copy();
            ClaimDetsils.TableName = "ClaimDetsils";
            mainDS.Tables.Add(ClaimDetsils);



            DataTable EmpList = new DataTable();
            EmpList = aFormDal.Get_EmpListForRPT(Convert.ToInt32(rptType)).Copy();
            EmpList.TableName = "EmpList";
            mainDS.Tables.Add(EmpList);

            DataTable Table = new DataTable();
            Table = aFormDal.Get_ReportForReimbursmentBillInMainReport(Convert.ToInt32(rptType)).Copy();
            Table.TableName = "ReimbusrmentBill";
            mainDS.Tables.Add(Table);

            if (mainDS.Tables[0].Rows.Count > 0)
            {
             //       mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsReimbursmentForm.xsd"));
             ShowReport(mainDS, "crpReimbursmentForm.rpt");
            }

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