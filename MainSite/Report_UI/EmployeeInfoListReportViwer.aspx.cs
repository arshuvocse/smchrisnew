 
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.UserPermissions_DAL;


public partial class Report_UI_EmployeeInfoListReportViwer : System.Web.UI.Page
{   
   
    
    ReportDocument rptdoc = new ReportDocument();


    EmployeeInfoListReportDAL aEmployeeInfoListReportDAL = new EmployeeInfoListReportDAL();
    protected void Page_Init(object sender, EventArgs e)
    {
        string rptType = Request.QueryString["rptType"];
      

        DataSet mainDS = new DataSet();
     



        if (rptType != "")
        {
            
             
                DataTable allDataTable = new DataTable();
                allDataTable = aEmployeeInfoListReportDAL.GetEmployeeInfoDAL(rptType).Copy();
                allDataTable.TableName = "EmployeeInfoListDataTable";
                mainDS.Tables.Add(allDataTable);

                DataTable EmpChildrenDataTable = new DataTable();
                EmpChildrenDataTable = aEmployeeInfoListReportDAL.GetEmpChildrenInfoDAL(rptType).Copy();
                EmpChildrenDataTable.TableName = "EmpChildrenDataTable";
                mainDS.Tables.Add(EmpChildrenDataTable);


                DataTable EmpEducationDataTable = new DataTable();
                EmpEducationDataTable = aEmployeeInfoListReportDAL.GetEmpEducationInfoDAL(rptType).Copy();
                EmpEducationDataTable.TableName = "EmpEducationDataTable";
                mainDS.Tables.Add(EmpEducationDataTable);




                DataTable EmpExperienceDataTable = new DataTable();
                EmpExperienceDataTable = aEmployeeInfoListReportDAL.GetEmpExperienceInfoDAL(rptType).Copy();
                EmpExperienceDataTable.TableName = "EmpExperienceDataTable";
                mainDS.Tables.Add(EmpExperienceDataTable);





                DataTable EmpTrainingDataTable = new DataTable();
                EmpTrainingDataTable = aEmployeeInfoListReportDAL.GetEmpTrainingInfoDAL(rptType).Copy();
                EmpTrainingDataTable.TableName = "EmpTrainingDataTable";
                mainDS.Tables.Add(EmpTrainingDataTable);



                DataTable EmpReferenceDataTable = new DataTable();
                EmpReferenceDataTable = aEmployeeInfoListReportDAL.GetEmpReferenceInfoDAL(rptType).Copy();
                EmpReferenceDataTable.TableName = "EmpReferenceDataTable";
                mainDS.Tables.Add(EmpReferenceDataTable);




                DataTable EmpNomineeDataTable = new DataTable();
                EmpNomineeDataTable = aEmployeeInfoListReportDAL.GetEmpNomineeInfoDAL(rptType).Copy();
                EmpNomineeDataTable.TableName = "EmpNomineeDataTable";
                mainDS.Tables.Add(EmpNomineeDataTable);



                DataTable EmpHobbyDataTable = new DataTable();
                EmpHobbyDataTable = aEmployeeInfoListReportDAL.GetEmpHobbyInfoDAL(rptType).Copy();
                EmpHobbyDataTable.TableName = "EmpHobbyDataTable";
                mainDS.Tables.Add(EmpHobbyDataTable);

                DataTable EmpExtraCurriculamDataTable = new DataTable();
                EmpExtraCurriculamDataTable = aEmployeeInfoListReportDAL.GetEmpExtraCurriculamInfoDAL(rptType).Copy();
                EmpExtraCurriculamDataTable.TableName = "EmpExtraCurriculamDataTable";
                mainDS.Tables.Add(EmpExtraCurriculamDataTable);


                DataTable EmpAchievementsDataTable = new DataTable();
                EmpAchievementsDataTable = aEmployeeInfoListReportDAL.GetEmpAchievementsInfoDAL(rptType).Copy();
                EmpAchievementsDataTable.TableName = "EmpAchievementsDataTable";
                mainDS.Tables.Add(EmpAchievementsDataTable);



                DataTable OtherEmpHobbyDataTable = new DataTable();
                OtherEmpHobbyDataTable = aEmployeeInfoListReportDAL.OtherEmpHobbyInfoDAL(rptType).Copy();
                OtherEmpHobbyDataTable.TableName = "OtherEmpHobbyDataTable";
                mainDS.Tables.Add(OtherEmpHobbyDataTable);



                DataTable EmpOtherTalentsDataTable = new DataTable();
                EmpOtherTalentsDataTable = aEmployeeInfoListReportDAL.EmpOtherTalentsInfoDAL(rptType).Copy();
                EmpOtherTalentsDataTable.TableName = "EmpOtherTalentsDataTable";
                mainDS.Tables.Add(EmpOtherTalentsDataTable);


                if (mainDS.Tables[0].Rows.Count > 0)
                {
                   // mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\EmployeeInfoList.xsd"));
                    ShowReport(mainDS, "crpEmployeeInfoListReport.rpt");
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