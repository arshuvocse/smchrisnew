 
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Increment_DAL;
using DAL.MasterSetup_DAL;
using DAL.MPBudget;
using DAL.UserPermissions_DAL;


public partial class Report_UI_JobRequisitionFormViewReportPreview : System.Web.UI.Page
{   
   
    
    ReportDocument rptdoc = new ReportDocument();


    EmployeeRequsitionDAL aDAL = new EmployeeRequsitionDAL();
    protected void Page_Init(object sender, EventArgs e)
    {
        string rptType = Request.QueryString["rptType"];
      

        DataSet mainDS = new DataSet();
     



        if (rptType != "")
        {
            
             
                DataTable allDataTable = new DataTable();
                allDataTable = aDAL.RptLoadEmpJobRequisitionById(Convert.ToInt32(rptType).ToString()).Copy();
                allDataTable.TableName = "MemoJobCreationDataTable";
                mainDS.Tables.Add(allDataTable);

                DataTable ReqEducationDT = new DataTable();
                ReqEducationDT = aDAL.LoadReqEducationById(rptType).Copy();
                ReqEducationDT.TableName = "ReqEducationDataTable";
                mainDS.Tables.Add(ReqEducationDT);


                DataTable KeyResponsDT = new DataTable();
                KeyResponsDT = aDAL.LoadKeyResponseById(rptType).Copy();
                KeyResponsDT.TableName = "KeyResponseDataTable";
                mainDS.Tables.Add(KeyResponsDT);



              





                DataTable JobEduReqDT = new DataTable();
                JobEduReqDT = aDAL.GetJobEduReqByJobId(Convert.ToInt32(rptType)).Copy();
                JobEduReqDT.TableName = "JobEduReqDataTable";
                mainDS.Tables.Add(JobEduReqDT);




                DataTable EducationRequirementsDT = new DataTable();
                EducationRequirementsDT = aDAL.GetEducationRequirementsDetailId(Convert.ToInt32(rptType)).Copy();
                EducationRequirementsDT.TableName = "EducationRequirementsDetailDataTable";
                mainDS.Tables.Add(EducationRequirementsDT);



                DataTable tOtherRequirementsDT = new DataTable();
                tOtherRequirementsDT = aDAL.GetOtherRequirementsDetailId(Convert.ToInt32(rptType)).Copy();
                tOtherRequirementsDT.TableName = "OtherRequirementsDataTable";
                mainDS.Tables.Add(tOtherRequirementsDT);




                DataTable PreferedWayOfCircularDt = new DataTable();
                PreferedWayOfCircularDt = aDAL.RptGetJCPreferedWayOfCircular(Convert.ToInt32(rptType).ToString()).Copy();
                PreferedWayOfCircularDt.TableName = "PreferedWayOfCircularDataTable";
                mainDS.Tables.Add(PreferedWayOfCircularDt);


                DataTable Office = new DataTable();
                Office = aDAL.RptGetOffice(Convert.ToInt32(rptType).ToString()).Copy();
                Office.TableName = "OfficeDataTable";
                mainDS.Tables.Add(Office);


                if (mainDS.Tables[0].Rows.Count > 0)
                {
               //  mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\JobRequisitionFormListInfo.xsd"));
            
                    ShowReport(mainDS, "crpJobRequisitionFormListListInfo.rpt");
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