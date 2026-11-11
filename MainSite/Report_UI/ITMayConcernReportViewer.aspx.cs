 
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.AllPrintLetter_DAL;
using DAL.TrainingDAL;
using DAL.UserPermissions_DAL;

using System.Configuration;
using System.Data.SqlClient;
using System.Drawing.Printing;
using CrystalDecisions.CrystalReports.Engine;
public partial class Report_UI_ITMayConcernReportViewer : System.Web.UI.Page
{   
   
    
    ReportDocument rptdoc = new ReportDocument();
    ITMayConcernDAL aDalInfo = new ITMayConcernDAL();
    protected void Page_Init(object sender, EventArgs e)
    {
        string rptType = Request.QueryString["rptType"];
        string rptMain = Request.QueryString["rptT"];
        DataSet mainDS = new DataSet();


        if (rptMain == "ITFromEmpInfo")
        {
            if (rptType != "")
            {
                DataTable allDataTable = new DataTable();
                allDataTable = aDalInfo.GetITMayConcernDAL(rptType).Copy();
                allDataTable.TableName = "GetITMayConcernDataTable";
                mainDS.Tables.Add(allDataTable);

                if (mainDS.Tables[0].Rows.Count > 0)
                {
                    //   mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\ITMayConcernInfo.xsd"));
                    ShowReport(mainDS, "crpITMayConcernInfo.rpt");
                }

            }
        }

        if (rptMain == "ITSeparInfo")
        {
            if (rptType != "")
            {
                DataTable allDataTable = new DataTable();
                allDataTable = aDalInfo.GetSeparationITMayConcernDAL(rptType).Copy();
                allDataTable.TableName = "GetSeparationITMayDataTable";
                mainDS.Tables.Add(allDataTable);

                if (mainDS.Tables[0].Rows.Count > 0)
                {
                   // mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\SeparationITMayInfo.xsd"));
                     ShowReport(mainDS, "crpSeparationITMayInfo.rpt");
                     rptdoc.Refresh();
                     rptdoc.PrintToPrinter(2, true, 1, 2);
                    
                }

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