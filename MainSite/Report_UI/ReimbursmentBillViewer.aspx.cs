using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using DAL.HealthCare_DAL;

public partial class Report_UI_ReimbursmentBillViewer : System.Web.UI.Page
{
    ReportDocument rptdoc = new ReportDocument();

   // ReimbursmentFormDal aFormDal = new ReimbursmentFormDal();

    BillSettlementDal aDal = new BillSettlementDal();


    protected void Page_Init(object sender, EventArgs e)
    {
        string rptType = Request.QueryString["rptType"];

        DataSet mainDS = new DataSet();

        if (rptType != "")
        {

            DataTable aMasterTable = new DataTable();
            aMasterTable = aDal.Get_ReportForReimbursmentBill(Convert.ToInt32(rptType)).Copy();
            aMasterTable.TableName = "ReimbusrmentBill";
            mainDS.Tables.Add(aMasterTable);

            if (mainDS.Tables[0].Rows.Count > 0)
            {
               // mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsReimbusrmentBill.xsd"));
                ShowReport(mainDS, "crpReimbursmentBill.rpt");
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