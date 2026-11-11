using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using DAL.Audit_Dal;
using DAL.MeetingMinorsDAL;
using DAL.Survey;

public partial class Report_UI_EmployeeAuditTrailViewer : System.Web.UI.Page
{
    ReportDocument rptdoc = new ReportDocument();
    ClearenceFormDal aEmployeeInfoListReportDAL = new ClearenceFormDal();
    protected void Page_Init(object sender, EventArgs e)
    {
    
      EmployeeAuditDal auditDal = new EmployeeAuditDal();

        string rptType = Request.QueryString["rptType"];
        string MID = Request.QueryString["MID"];

        DataSet mainDS = new DataSet();

        if (rptType == "Increment")
        {

            DataTable dtMaster = new DataTable();
            dtMaster = auditDal.GetIncrementAuditTrailByMasterId(MID).Copy();
            dtMaster.TableName = "dtIncrementAuditTrail";
            mainDS.Tables.Add(dtMaster);

            if (mainDS.Tables[0].Rows.Count > 0)
            {
               // mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsIncrementAuditTrail.xsd"));
                ShowReport(mainDS, "crpIncrementAudit.rpt");
            }
        }


        if (rptType == "Seperation")
        {

            DataTable dtMaster = new DataTable();
            dtMaster = auditDal.GetSeparationAuditforCrp(MID).Copy();
            dtMaster.TableName = "dtSeperationAuditTrail";
            mainDS.Tables.Add(dtMaster);



            if (mainDS.Tables[0].Rows.Count > 0)
            {
                //mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsSeperationAuditTrail.xsd"));
                 ShowReport(mainDS, "crpSeparationAuditTrail.rpt");
            }
        }


        if (rptType == "KIP")
        {

            DataTable dtMaster = new DataTable();
            dtMaster = auditDal.GetKPIMasterAuditforCrp(MID).Copy();
            dtMaster = auditDal.GetKPIMasterAuditforCrp(MID).Copy();
            dtMaster.TableName = "dtKPIMasterAuditTrail";
            mainDS.Tables.Add(dtMaster);

            DataTable dtFunctional = new DataTable();
            dtFunctional = auditDal.GetKPIFuntionalAuditforCrp(MID).Copy();
            dtFunctional.TableName = "dtKPIFuntionalAreaAuditTrail";
            mainDS.Tables.Add(dtFunctional);

            DataTable dtBehavioralArea = new DataTable();
            dtBehavioralArea = auditDal.GetKPIBehavioralAuditforCrp(MID).Copy();
            dtBehavioralArea.TableName = "dtKPIBehavioralAuditTrail";
            mainDS.Tables.Add(dtBehavioralArea);

            if (mainDS.Tables[0].Rows.Count > 0)
            {
            //   mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsKPIAuditTrail.xsd"));
                 ShowReport(mainDS, "crpKPIAuditTrail.rpt");
            }

        }




        if (rptType == "ContrauctualEmployee")
        {

            DataTable dtMaster = new DataTable();
            dtMaster = auditDal.GetContractualEmployeeAuditforCrp(MID).Copy();
            dtMaster.TableName = "dtContrauctualEmployeeAuditTrail";
            mainDS.Tables.Add(dtMaster);
            if (mainDS.Tables[0].Rows.Count > 0)
            {
              //  mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsContrauctualEmployeeAuditTrail.xsd"));
                 ShowReport(mainDS, "crpContractualEmployee.rpt");
            }
        }


        if (rptType == "JobRequisition")
        {
            DataTable dtMaster = new DataTable();
            dtMaster = auditDal.GetJobRequisitionAuditforCrp(MID).Copy();
            dtMaster.TableName = "dtJobRequisitionMasterAuditTrail";
            mainDS.Tables.Add(dtMaster);

            //job Requisition Key Response
            DataTable dtJobReqKeyRespon = new DataTable();
            dtJobReqKeyRespon = auditDal.GetJobReqKeyResponAuditforCrp(MID).Copy();
            dtJobReqKeyRespon.TableName = "dtJobReqKeyResponAuditTrail";
            mainDS.Tables.Add(dtJobReqKeyRespon);

            //Education_Requerement_Details
            DataTable dtEducationRequirementsDetail = new DataTable();
            dtEducationRequirementsDetail = auditDal.GetEducationRequirementsDetailAuditforCrp(MID).Copy();
            dtEducationRequirementsDetail.TableName = "dtEducationRequirementsDetailAuditTrail";
            mainDS.Tables.Add(dtEducationRequirementsDetail);

            //Education_Info

            DataTable dtEducationRequirDesJobReq = new DataTable();
            dtEducationRequirDesJobReq = auditDal.GetEducationRequirDesJobReqAuditforCrp(MID).Copy();
            dtEducationRequirDesJobReq.TableName = "dtEducationRequirDesJobReqAuditTrail";
            mainDS.Tables.Add(dtEducationRequirDesJobReq);

            //Other_Requirement_Details

            DataTable dtOtherRequirementDetail = new DataTable();
            dtOtherRequirementDetail = auditDal.GetOtherRequirementDetailAuditforCrp(MID).Copy();
            dtOtherRequirementDetail.TableName = "dtOtherRequirementDetailAuditTrail";
            mainDS.Tables.Add(dtOtherRequirementDetail);

            //CirculationWayDetails
            DataTable dtCirculationWayDetail = new DataTable();
            dtCirculationWayDetail = auditDal.GetCirculationWayDetailAuditforCrp(MID).Copy();
            dtCirculationWayDetail.TableName = "dtCirculationWayDetailAuditTrail";
            mainDS.Tables.Add(dtCirculationWayDetail);

            if (mainDS.Tables[0].Rows.Count > 0)
            {
                //  mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsJobRequisitionAuditTrail.xsd"));

                ShowReport(mainDS, "crpJobRequisitionAuditTrail.rpt");

            }
        }





        if (rptType == "EmployeeInformation")
        {


            DataTable allDataTable = new DataTable();
            allDataTable = auditDal.GetEmployeeInformationAuditforCrp(MID).Copy();
            allDataTable.TableName = "EmployeeInfoListDataTable";
            mainDS.Tables.Add(allDataTable);

            DataTable EmpChildrenDataTable = new DataTable();
            EmpChildrenDataTable = auditDal.GetEmployeeChildrenAuditforCrp(MID).Copy();
            EmpChildrenDataTable.TableName = "EmpChildrenDataTable";
            mainDS.Tables.Add(EmpChildrenDataTable);


            DataTable EmpEducationDataTable = new DataTable();
            EmpEducationDataTable = auditDal.GetEmployeeEducationAuditforCrp(MID).Copy();
            EmpEducationDataTable.TableName = "EmpEducationDataTable";
            mainDS.Tables.Add(EmpEducationDataTable);


            DataTable EmpExperienceDataTable = new DataTable();
            EmpExperienceDataTable = auditDal.GetEmployeExperinceAuditforCrp(MID).Copy();
            EmpExperienceDataTable.TableName = "EmpExperienceDataTable";
            mainDS.Tables.Add(EmpExperienceDataTable);


            DataTable EmpTrainingDataTable = new DataTable();
            EmpTrainingDataTable = auditDal.GetEmployeeTrainingAuditforCrp(MID).Copy();
            EmpTrainingDataTable.TableName = "EmpTrainingDataTable";
            mainDS.Tables.Add(EmpTrainingDataTable);


            DataTable EmpReferenceDataTable = new DataTable();
            EmpReferenceDataTable = auditDal.GetEmployeeReferanceAuditforCrp(MID).Copy();
            EmpReferenceDataTable.TableName = "EmpReferenceDataTable";
            mainDS.Tables.Add(EmpReferenceDataTable);


            DataTable EmpNomineeDataTable = new DataTable();
            EmpNomineeDataTable = auditDal.GetEmployeeNomineeAuditforCrp(MID).Copy();
            EmpNomineeDataTable.TableName = "EmpNomineeDataTable";
            mainDS.Tables.Add(EmpNomineeDataTable);


            DataTable EmpExtraCurriculamDataTable = new DataTable();
            EmpExtraCurriculamDataTable = auditDal.GetEmployeExtraCurriculamAuditforCrp(MID).Copy();
            EmpExtraCurriculamDataTable.TableName = "EmpExtraCurriculamDataTable";
            mainDS.Tables.Add(EmpExtraCurriculamDataTable);


            DataTable EmpDerectlySupervisorDataTable = new DataTable();
            EmpDerectlySupervisorDataTable = auditDal.GetDerectlySupervisorAuditforCrp(MID).Copy();
            EmpDerectlySupervisorDataTable.TableName = "EmpDerectlySupervisorDataTable";
            mainDS.Tables.Add(EmpDerectlySupervisorDataTable);


            //

            DataTable EmpEmpOtherDataTable = new DataTable();
            EmpEmpOtherDataTable = auditDal.GetOtherTalentAuditforCrp(MID).Copy();
            EmpEmpOtherDataTable.TableName = "EmpOtherTalentDataTable";
            mainDS.Tables.Add(EmpEmpOtherDataTable);


            DataTable GetEmployeeHobbyDataTable = new DataTable();
            GetEmployeeHobbyDataTable = auditDal.GetHobbyAuditforCrp(MID).Copy();
            GetEmployeeHobbyDataTable.TableName = "GetEmployeeHobbyDataTable";
            mainDS.Tables.Add(GetEmployeeHobbyDataTable);

            // EmpAchievement
            DataTable EmpAchievementDataTable = new DataTable();
            EmpAchievementDataTable = auditDal.GetAchievementsAuditforCrp(MID).Copy();
            EmpAchievementDataTable.TableName = "EmpAchievementDataTable";
            mainDS.Tables.Add(EmpAchievementDataTable);




            if (mainDS.Tables[0].Rows.Count > 0)
            {
                // mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsEmployeeInformationAuditTrail.xsd"));
                ShowReport(mainDS, "crpEmployeeInformationAuditTrail.rpt");
            }
        }


        if (rptType == "JD")
        {

            DataTable dtMaster = new DataTable();
            dtMaster = auditDal.GetJDAuditforCrp(MID).Copy();
            dtMaster.TableName = "dtJDAuditTrail";
            mainDS.Tables.Add(dtMaster);

            DataTable dtJdDetails = new DataTable();
            dtJdDetails = auditDal.GetJdDetailsAuditforCrp(MID).Copy();
            dtJdDetails.TableName = "dtJdDetailsAuditTrail";
            mainDS.Tables.Add(dtJdDetails);

            if (mainDS.Tables[0].Rows.Count > 0)
            {
                //mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsJDAuditTrail.xsd"));
                ShowReport(mainDS, "crpJobDescriptionAuditTrail.rpt");
            }
        }

        if (rptType == "EmployeeProbation")
        {

            DataTable dtMaster = new DataTable();
            dtMaster = auditDal.GetEmployeeProbationAuditforCrp(MID).Copy();
            dtMaster.TableName = "dtEmployeeProbationAuditTrail";
            mainDS.Tables.Add(dtMaster);
            if (mainDS.Tables[0].Rows.Count > 0)
            {
                // mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsEmployeeProbationAuditTrail.xsd"));

                ShowReport(mainDS, "crpEmployeeProbationAuditTrail.rpt");
            }
        }
        if (rptType == "MPBudget")
        {
            DataTable dtMaster = new DataTable();
            dtMaster = auditDal.GetMpBudgetAuditforCrp(MID).Copy();
            dtMaster.TableName = "dtMPBudgetAuditTrail";
            mainDS.Tables.Add(dtMaster);

            DataTable dtDetails = new DataTable();
            dtDetails = auditDal.GetMpBudgetDetailsAuditforCrp(MID).Copy();
            dtDetails.TableName = "dtMPBudgetDetailsAuditTrail";
            mainDS.Tables.Add(dtDetails);

            if (mainDS.Tables[0].Rows.Count > 0)
            {
              //   mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsMPBudgetAuditTrail.xsd"));
               // ShowReport(mainDS, "crpMPBudget.rpt");
               ShowReport(mainDS, "crpMPBudgetAuditTrail.rpt");
            }
        }

        if (rptType == "Transfer")
        {

            DataTable dtMaster = new DataTable();
            dtMaster = auditDal.GettransferAuditforCrp(MID).Copy();
            dtMaster.TableName = "dtTransferAuditTrail";
            mainDS.Tables.Add(dtMaster);

            if (mainDS.Tables[0].Rows.Count > 0)
            {
                // mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsTransferAuditTrail.xsd"));
                ShowReport(mainDS, "crpTransferAuditTrail.rpt");
            }
        }


        if (rptType == "Redesignation")
        {

            DataTable dtMaster = new DataTable();
            dtMaster = auditDal.GetRedesignationAuditforCrp(MID).Copy();
            dtMaster.TableName = "dtRedesignationAuditTrail";
            mainDS.Tables.Add(dtMaster);

            if (mainDS.Tables[0].Rows.Count > 0)
            {
                // mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsRedesignationAuditTrail.xsd"));
                ShowReport(mainDS, "crpEmpRedesignationAudit.rpt");
            }
        }
  
        if (rptType == "Promotion")
        {

            DataTable dtMaster = new DataTable();
            dtMaster = auditDal.GetPromotionAuditforCrp(MID).Copy();
            dtMaster.TableName = "dtPromotionAuditTrail";
            mainDS.Tables.Add(dtMaster);

            DataTable dt = new DataTable();
            dt = auditDal.GetPromotionAudi_Ds_tforCrp(MID).Copy();
            dt.TableName = "dtPromotionDsAuditTrail";
            mainDS.Tables.Add(dt);

            if (mainDS.Tables[0].Rows.Count > 0)
            {
               // mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsPromotionAuditTrail.xsd"));
                 ShowReport(mainDS, "crpPromotionAuditTrail.rpt");
            }

        }
      
        if (rptType == "Suspend")
        {

            DataTable dtMaster = new DataTable();
            dtMaster = auditDal.GetSuspendAuditforCrp(MID).Copy();
            dtMaster.TableName = "dtSuspendAuditTrail";
            mainDS.Tables.Add(dtMaster);
     
            if (mainDS.Tables[0].Rows.Count > 0)
            {
               // mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsSuspendAuditTrail.xsd"));
                 ShowReport(mainDS, "crpSuspendAuditTrail.rpt");
            }

        }


        if (rptType == "TrainningBudget")
        {

            DataTable dtMaster = new DataTable();
            dtMaster = auditDal.GeTtrainingBudgetAuditTrailByMasterId(MID).Copy();
            dtMaster.TableName = "dtTrainningBudgetAuditTrailMaster";
            mainDS.Tables.Add(dtMaster);

            DataTable details = new DataTable();
            details = auditDal.GeTtrainingBudgetDetailsAuditTrail(MID).Copy();
            details.TableName = "dtTrainningBudgetAuditTrailDetais";
            mainDS.Tables.Add(details);

            if (mainDS.Tables[0].Rows.Count > 0)
            {
           //    mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsTrainningBudgetAuditTrail.xsd"));
                   ShowReport(mainDS, "CrpTrainingBudgetAuditTrail.rpt");
            }
        }


        if (rptType == "DiciplinaryAction")
        {

            DataTable dtMaster = new DataTable();
            dtMaster = auditDal.GetDiciplinaryActionAuditforCrp(MID).Copy();
            dtMaster.TableName = "dtDiciplinaryActionAuditTrail";
            mainDS.Tables.Add(dtMaster);

            if (mainDS.Tables[0].Rows.Count > 0)
            {
               
                
              ShowReport(mainDS, "crpDeciplinaryActionAuditTrail.rpt");

            }

        }


        
        
        
        
        
        
        if (rptType == "JobCirculation")
        {

            DataTable dtMaster = new DataTable();
            dtMaster = auditDal.GetJobCirculationAuditforCrp(MID).Copy();
            dtMaster.TableName = "dtJobCirculationAuditTrail";
            mainDS.Tables.Add(dtMaster);

            DataTable dtJdDetails = new DataTable();
            dtJdDetails = auditDal.GetJobCirculationDetailsAuditforCrp(MID).Copy();
            dtJdDetails.TableName = "dtJobCirculationDetailsAuditTrail";
            mainDS.Tables.Add(dtJdDetails);

            if (mainDS.Tables[0].Rows.Count > 0)
            {
             //  mainDS.WriteXmlSchema(MapPath("~\\Reports\\DataSets\\dsJobCirculationAuditTrail.xsd"));
                 ShowReport(mainDS, "crpJobCirculationAuditTrail.rpt");
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