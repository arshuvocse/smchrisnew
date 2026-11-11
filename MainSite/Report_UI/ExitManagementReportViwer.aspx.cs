 
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


public partial class Report_UI_ExitInterViewFormReport : System.Web.UI.Page
{   
   
    
    ReportDocument rptdoc = new ReportDocument();


    ExitManagementReportViwerDAL aEmployeeInfoListReportDAL = new ExitManagementReportViwerDAL();

    protected void Page_Init(object sender, EventArgs e)
    {
        string rptType = Request.QueryString["rptType"];
      

        DataSet mainDS = new DataSet();
     



        if (rptType != "")
        {
            
             
                DataTable allDataTable = new DataTable();
                allDataTable = aEmployeeInfoListReportDAL.GetJdByMaster(Convert.ToInt32(rptType)).Copy();
                allDataTable.TableName = "ExitInterviewFormMasterDataTable";
                mainDS.Tables.Add(allDataTable);

                DataTable ExitServeyDetail = new DataTable();
                ExitServeyDetail = aEmployeeInfoListReportDAL.GetExitServeyDetail(Convert.ToInt32(rptType)).Copy();
                ExitServeyDetail.TableName = "ExitServeyDetailDataTable";
                mainDS.Tables.Add(ExitServeyDetail);


                DataTable ExitReasonDetail = new DataTable();
                ExitReasonDetail = aEmployeeInfoListReportDAL.GetExitReasonDetail(Convert.ToInt32(rptType)).Copy();
                ExitReasonDetail.TableName = "ExitReasonDetailDataTable";
                mainDS.Tables.Add(ExitReasonDetail);


                if (mainDS.Tables[0].Rows.Count > 0)
                {
               //   mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\ExitInterviewFormMasterListInfo.xsd"));
                  ShowReport(mainDS, "crpExitInterviewFormMasterListInfoReport.rpt");
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