 
using System.Globalization;
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
using DAL.MPBudget;
using DAL.Report_DAL;
using DAL.UserPermissions_DAL;


public partial class Report_UI_SurveyReportViewr : System.Web.UI.Page
{   
   
    
    ReportDocument rptdoc = new ReportDocument();


    ContractualEmpManagementReportDal aDAL = new ContractualEmpManagementReportDal();
    protected void Page_Init(object sender, EventArgs e)
    {
        string rptType = Request.QueryString["rptType"];
        string EmpID = Request.QueryString["EmpID"];
        string MasterID = Request.QueryString["MasterID"];
      

        DataSet mainDS = new DataSet();
     



        if (rptType != "")
        {



            DataTable master = new DataTable();
            master = aDAL.RptSurveyLoad(MasterID, EmpID).Copy();
            master.TableName = "SurveySubmitMaster";
            mainDS.Tables.Add(master);
            if (master.Rows.Count > 0)
            {
                HiddenMasterId.Value = master.Rows[0].Field<Int32>("SurveySubmitMasterId").ToString(CultureInfo.InvariantCulture);
            }




            DataTable KeyResponseDataTable = new DataTable();
            KeyResponseDataTable = aDAL.RptSurveyLoadDetails(Convert.ToInt32(HiddenMasterId.Value)).Copy();
                KeyResponseDataTable.TableName = "SurveySubmitDetails";
                mainDS.Tables.Add(KeyResponseDataTable);


                DataTable KeyResponseDataTable2 = new DataTable();
                KeyResponseDataTable2 = aDAL.RptSurveyLoadDetails2(Convert.ToInt32(HiddenMasterId.Value)).Copy();
                KeyResponseDataTable2.TableName = "SurveySubmitDetailsQT2";
                mainDS.Tables.Add(KeyResponseDataTable2);


                


                if (mainDS.Tables[0].Rows.Count > 0)
                {
                //   mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\SurveySubmitDataTableInfo.xsd"));
                  ShowReport(mainDS, "crpSurveySubmit.rpt");
                   rptdoc.ExportToHttpResponse(ExportFormatType.ExcelRecord, Response, true,   "Survey_Form");
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