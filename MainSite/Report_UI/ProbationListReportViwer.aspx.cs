 
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MPBudget;
using DAL.Report_DAL;
using DAL.UserPermissions_DAL;


public partial class Report_UI_ProbationListReportViwer : System.Web.UI.Page
{   
   
    
    ReportDocument rptdoc = new ReportDocument();


    ProbationListReportDAL aEmployeeInfoListReportDAL = new ProbationListReportDAL();
    protected void Page_Init(object sender, EventArgs e)
    {
        string rptType = Request.QueryString["rptType"];
      

        DataSet mainDS = new DataSet();
     



        if (rptType != "")
        {
            
             
                DataTable allDataTable = new DataTable();
                allDataTable = aEmployeeInfoListReportDAL.GetJdByMaster(Convert.ToInt32(rptType)).Copy();
                allDataTable.TableName = "ProbationListMasterDataTable";
                mainDS.Tables.Add(allDataTable);

                DataTable EmpChildrenDataTable = new DataTable();
                EmpChildrenDataTable = aEmployeeInfoListReportDAL.GetJdDetails(Convert.ToInt32(rptType)).Copy();
                EmpChildrenDataTable.TableName = "ProbationListDetailsDataTable";
                mainDS.Tables.Add(EmpChildrenDataTable);


                if (mainDS.Tables[0].Rows.Count > 0)
                {
                   // mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\ProbationListListInfo.xsd"));
                   ShowReport(mainDS, "crpProbationListInfoReport.rpt");
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