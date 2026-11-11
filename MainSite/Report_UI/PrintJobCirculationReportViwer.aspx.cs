 
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Increment_DAL;
using DAL.MPBudget;
using DAL.UserPermissions_DAL;


public partial class Report_UI_PrintJobCirculationReportViwer : System.Web.UI.Page
{   
   
    
    ReportDocument rptdoc = new ReportDocument();


    PrintJobCirculationDAL aDAL = new PrintJobCirculationDAL();
    protected void Page_Init(object sender, EventArgs e)
    {
        string rptType = Request.QueryString["rptType"];
      

        DataSet mainDS = new DataSet();
     



        if (rptType != "")
        {
            
             
                DataTable allDataTable = new DataTable();
                allDataTable = aDAL.GetJobCreationInformationById(Convert.ToInt32(rptType).ToString()).Copy();
                HiddenFieldJobReqId.Value = allDataTable.Rows[0]["JobReqId"].ToString();
                allDataTable.TableName = "JobCreationDataTable";
                mainDS.Tables.Add(allDataTable);

                DataTable KeyResponseDataTable = new DataTable();
                KeyResponseDataTable = aDAL.LoadKeyResponseByJobReqFormId(HiddenFieldJobReqId.Value).Copy();
                KeyResponseDataTable.TableName = "KeyResponseDataTable";
                mainDS.Tables.Add(KeyResponseDataTable);


                DataTable RequirementDataTable = new DataTable();
                RequirementDataTable = aDAL.GetOtherRequirementsDetailId(HiddenFieldJobReqId.Value).Copy();
                RequirementDataTable.TableName = "OtherRequirementsDataTable";
                mainDS.Tables.Add(RequirementDataTable);


                if (mainDS.Tables[0].Rows.Count > 0)
                {
              //     mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\JobCreationListInfo.xsd"));
                   ShowReport(mainDS, "crpJobCreationListInfo.rpt");
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