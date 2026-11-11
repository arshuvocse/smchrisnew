 
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.Shared;
using DAL.Increment_DAL;
using DAL.MeetingMinorsDAL;
using DAL.MPBudget;
using DAL.UserPermissions_DAL;


public partial class Report_UI_DocumentMemoReportViwer : System.Web.UI.Page
{   
   
    
    ReportDocument rptdoc = new ReportDocument();


    MemoPrintIncrementDAL aDAL = new MemoPrintIncrementDAL();
    MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();
    protected void Page_Init(object sender, EventArgs e)
    {
        string rptType = Request.QueryString["rptType"];
 
      

        DataSet mainDS = new DataSet();


        
              if (rptType != "")
        {


            DataTable Master = new DataTable();
            Master = AMAsterDal.GetMasterInfoDALrpt(rptType).Copy();
            Master.TableName = "dtMasterInfo";
            mainDS.Tables.Add(Master);




            DataTable ApprovalPath = new DataTable();
            ApprovalPath = AMAsterDal.LoadApprovalPathRpt(rptType).Copy();
            ApprovalPath.TableName = "dtApprovalPath";
            mainDS.Tables.Add(ApprovalPath);


            DataTable ApprovalLog = new DataTable();
            ApprovalLog = AMAsterDal.LoadApprovalLogRpt(rptType).Copy();
            ApprovalLog.TableName = "dtApprovalLog";
            mainDS.Tables.Add(ApprovalLog);

            DataTable document = new DataTable();
            document = AMAsterDal.LoadDocumentRpt(rptType).Copy();
            document.TableName = "dtDocument";
            mainDS.Tables.Add(document);

                if (mainDS.Tables[0].Rows.Count > 0)
                {
                     //  mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dtDocumentMemo.xsd"));
                  ShowReport(mainDS, "crpDocumentMemofinal.rpt");

                       
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