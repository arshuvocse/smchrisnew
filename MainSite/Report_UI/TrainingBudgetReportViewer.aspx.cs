 
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.TrainingDAL;
using DAL.UserPermissions_DAL;


public partial class Report_UI_TrainingBudgetReportViewer : System.Web.UI.Page
{   
   
    
    ReportDocument rptdoc = new ReportDocument();
    TrainingBudgetDal aDalInfo = new TrainingBudgetDal();
    protected void Page_Init(object sender, EventArgs e)
    {
        string rptType = Request.QueryString["rptType"];
        DataSet mainDS = new DataSet();
     
        if (rptType != "")
        {            
             DataTable allDataTable = new DataTable();
             allDataTable = aDalInfo.GetTrainingBudgetDAL(rptType).Copy();
             allDataTable.TableName = "TrainingBudgettDataTable";
             mainDS.Tables.Add(allDataTable);

             if (mainDS.Tables[0].Rows.Count > 0)
             {
                 //mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsTrainingBudget.xsd"));
                 ShowReport(mainDS, "crpTrainingBudgetReport.rpt");
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