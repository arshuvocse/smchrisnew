using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.DataManager;
using DAL.InternalCls;
using DAL.MAIN_FUNCTION;

namespace DAL.UserProfileDAL
{
    public class KPIDashboardDAL
    {

       ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
       public DataTable LoadEmpJobRequisitionApp(string EmpID, string FinId)
       {
           string query = @"SELECT JQF.JobReqId, JQF.ReqCode, CI.CompanyName,JQF.JobTitle, JQF.ReqDate, JQF.Nos,FINY.FinancialYearDesc,JobReqFormAppLogId FROM dbo.tblJobReqForm AS JQF
                            INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = JQF.CompanyId
                            INNER JOIN dbo.tblFinancialYear AS FINY ON JQF.FinYearId = FINY.FinancialYearId
							INNER JOIN (SELECT JobReqId,MAX(Version)MaxVer FROM dbo.tblJobReqFormAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY JobReqId) AS CELog ON CELog.JobReqId= JQF.JobReqId
								INNER JOIN dbo.tblJobReqFormAppLog ON tblJobReqFormAppLog.JobReqId = JQF.JobReqId
                                where (JQF.IsDelete is null or JQF.IsDelete = 0) and Version=CELog.MaxVer  and  ForEmpInfoId = '" + EmpID + "'   and  JQF.FinYearId =" + FinId;


           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }

       public DataTable GetManpowerBudgetInfoApproval(string EmpID,  string FinId)
       {

           string queryStr =
               @"SELECT * FROM dbo.tblMPBudgetMaster bm
								 
								LEFT JOIN dbo.tblCompanyInfo c ON c.CompanyId = bm.CompanyId
                                LEFT JOIN dbo.tblDepartment d ON d.DepartmentId=bm.DepartmentId
                                LEFT JOIN dbo.tblFinancialYear fy ON fy.FinancialYearId = bm.FinancialYearId 
                                INNER JOIN (SELECT MPBudgetMasterId,MAX(Version)MaxVer FROM dbo.tblMPBudgetMasterAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY MPBudgetMasterId) AS CELog ON CELog.MPBudgetMasterId= bm.MPBudgetMasterId
								INNER JOIN dbo.tblMPBudgetMasterAppLog ON tblMPBudgetMasterAppLog.MPBudgetMasterId = bm.MPBudgetMasterId
                                where (bm.IsDelete is null or bm.IsDelete = 0) and Version=CELog.MaxVer  and  ForEmpInfoId =" +
               EmpID;  
           //            string queryStr = @"SELECT 0 as MPBudgetMasterAppLogId,* FROM dbo.tblMPBudgetMaster bm
           //								LEFT JOIN dbo.tblMPBudgetDetails bd ON bd.MPBudgetMasterId = bm.MPBudgetMasterId
           //								LEFT JOIN dbo.tblCompanyInfo c ON c.CompanyId = bm.CompanyId
           //                                LEFT JOIN dbo.tblDepartment d ON d.DepartmentId=bm.DepartmentId
           //                                LEFT JOIN dbo.tblFinancialYear fy ON fy.FinancialYearId = bm.FinancialYearId 
           //                               
           //                                where (bm.IsDelete is null or bm.IsDelete = 0)";
           return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
       }


       public DataTable GetManpowerBudgetInfoApprovalWithOutFin(string EmpID)
       {

           string queryStr = @"SELECT * FROM dbo.tblMPBudgetMaster bm
								 
								LEFT JOIN dbo.tblCompanyInfo c ON c.CompanyId = bm.CompanyId
                                LEFT JOIN dbo.tblDepartment d ON d.DepartmentId=bm.DepartmentId
                                LEFT JOIN dbo.tblFinancialYear fy ON fy.FinancialYearId = bm.FinancialYearId 
                                INNER JOIN (SELECT MPBudgetMasterId,MAX(Version)MaxVer FROM dbo.tblMPBudgetMasterAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY MPBudgetMasterId) AS CELog ON CELog.MPBudgetMasterId= bm.MPBudgetMasterId
								INNER JOIN dbo.tblMPBudgetMasterAppLog ON tblMPBudgetMasterAppLog.MPBudgetMasterId = bm.MPBudgetMasterId
                                where (bm.IsDelete is null or bm.IsDelete = 0) and Version=CELog.MaxVer  and  ForEmpInfoId = '" + EmpID ;
           //            string queryStr = @"SELECT 0 as MPBudgetMasterAppLogId,* FROM dbo.tblMPBudgetMaster bm
           //								LEFT JOIN dbo.tblMPBudgetDetails bd ON bd.MPBudgetMasterId = bm.MPBudgetMasterId
           //								LEFT JOIN dbo.tblCompanyInfo c ON c.CompanyId = bm.CompanyId
           //                                LEFT JOIN dbo.tblDepartment d ON d.DepartmentId=bm.DepartmentId
           //                                LEFT JOIN dbo.tblFinancialYear fy ON fy.FinancialYearId = bm.FinancialYearId 
           //                               
           //                                where (bm.IsDelete is null or bm.IsDelete = 0)";
           return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
       }

       public DataTable GetEmployeeInfoDAL(string Id)
       {
           
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
           const string queryStr = @" SELECT nat.Description Nationality,  SpouseOccup.Description SpouseOccupation,SpusMax.Description SpouseMaxEducation, PreThana.Title PresentThana, PerThana.Title PermanentThana,PerDis.Titel PermanentDistrict, PreDis.Titel PresentDistrict,  PresDiv.Title PresentDivision, PerDiv.Title ParmanentDivision, tblOccupation.Description FatherOccupation, motherOcc.Description MotherOccupation,com.ShortName, Div.DivisionName,DivW.DivisionWingName, Sec.SectionName, dept.DepartmentName,
SuSec.SubSectionName, Cat.EmpCategoryName, Sgrd.GradeName, SStep.SalaryStepName, Desg.Designation,
DType.DesigTypeName, SLoc.SalaryLocation, JLOC.Location, EType.EmpType, Pro.ProjectName, eGen.EmpName ReportingEmp, CASE
    WHEN Emp.ConformationStatus=1 THEN 'Yes'
    WHEN Emp.ConformationStatus=0 THEN 'No'
    
    ELSE 'Not available' END AS ConformationStatus , CASE
    WHEN Emp.IsProbationary=1 THEN 'Yes'
    WHEN Emp.IsProbationary=0 THEN 'No'
    
    ELSE 'Not available' END AS    IsProbationary,
 *    from tblEmpGeneralInfo Emp
left JOIN dbo.tblCompanyInfo com ON com.CompanyId = Emp.CompanyId
left JOIN dbo.tblDivision Div ON Div.DivisionId = Emp.DivisionId
left JOIN dbo.tblDivisionWing DivW ON DivW.DivisionWId = Emp.DivisionWId
LEFT JOIN dbo.tblDepartment dept ON dept.DepartmentId = Emp.DepartmentId
LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = Emp.SectionId
LEFT JOIN dbo.tblSubSection SuSec ON SuSec.SubSectionId = Emp.SubSectionId
LEFT JOIN dbo.tblEmpCategory Cat ON Cat.EmpCategoryId = Emp.EmpCategoryId
LEFT JOIN dbo.tblSalaryGrade Sgrd ON Sgrd.SalaryGradeId = Emp.SalaryGradeId
LEFT JOIN dbo.tblSalaryStep SStep ON SStep.SalaryStepId = Emp.SalaryStepId
LEFT JOIN dbo.tblDesignation Desg ON Desg.DesignationId = Emp.DesignationId
LEFT JOIN dbo.tblDesignationType DType ON DType.DesignationTypeId = Emp.DesignationTypeId
LEFT JOIN dbo.tblSalaryLocation SLoc ON SLoc.SalaryLoationId = Emp.SalaryLoationId
LEFT JOIN dbo.tblJobLocation JLOC ON JLOC.JobLocationID = Emp.JobLocationId
LEFT JOIN dbo.tblEmployeeType EType ON EType.EmpTypeId = Emp.EmpTypeId
LEFT JOIN dbo.tblProjectSetup Pro ON Pro.ProjectId = Emp.ProjectID
LEFT JOIN dbo.tblEmpGeneralInfo eGen ON eGen.EmpInfoId=Emp.ReportingEmpId
LEFT JOIN tblOccupation ON Emp.FatherOccupation=tblOccupation.OccupationID
LEFT JOIN tblOccupation motherOcc ON Emp.MotherOccupation=motherOcc.OccupationID
LEFT JOIN tblAddressDivision PresDiv ON Emp.PresentDivision= PresDiv.AddressDivisionID
LEFT JOIN tblAddressDivision PerDiv ON Emp.ParmanentDivision= PerDiv.AddressDivisionID
LEFT JOIN tblDistrict PreDis ON  Emp.PresentDistrict= PreDis.DistrictID
LEFT JOIN tblDistrict PerDis ON  Emp.PermanentDistrict= PerDis.DistrictID
LEFT JOIN tblThana PreThana ON Emp.PresentThana= PreThana.ThanaID
LEFT JOIN tblThana PerThana ON Emp.PermanentThana= PerThana.ThanaID
LEFT JOIN tblEducationName SpusMax ON Emp.SpouseMaxEducation= SpusMax.EducationNameID
LEFT JOIN tblOccupation SpouseOccup ON  Emp.SpouseOccupation= SpouseOccup.OccupationID
LEFT JOIN dbo.tblNationality nat ON  Emp.Nationality= nat.Nationality
WHERE Emp.EmpInfoId=@EmpInfoId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }


       public DataTable GetKPISelfStatusDAL(string EmpID,  string FinId)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", EmpID));
           aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", FinId));
           const string queryStr = @" select * from tblAppraisalSelfMaster  WITH (NOLOCK)  where EmpInfoId=@EmpInfoId and FinancialYearId=@FinancialYearId   and ((ActionStatus='Approved') or (ActionStatus='Verified'))";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }

       public DataTable GetApprisalSelfStatusDAL(string EmpID, string FinId)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", EmpID));
           aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", FinId));
           const string queryStr = @" select * from  tblAppraisalMaster  WITH (NOLOCK)  where EmpInfoId=@EmpInfoId and FinancialYearId=@FinancialYearId  and ((CurrentStatus='Approved') or (CurrentStatus='Verified'))";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }


       public DataTable GetKPIStatusDAL(string EmpID, string FinId)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", EmpID));
           aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", FinId));
           const string queryStr = @"select  ALog.ForEmpInfoId, * from  tblAppraisalSelfMaster mas   WITH (NOLOCK) 

inner join tblAppraisalSelfAppLog  ALog on ALog.AppraisalSelfMasterId=mas.AppraisalSelfMasterId
 where   mas.FinancialYearId=@FinancialYearId  and ALog.ActionStatus <>'Drafted' and ALog.ForEmpInfoId=@EmpInfoId and 
 ALog.ActionStatus='Approved'";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }


       public DataTable GetKPIStatusDALPending(string EmpID, string FinId)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", EmpID));
           aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", FinId));
           const string queryStr = @"select  ALog.ForEmpInfoId, * from  tblAppraisalSelfMaster mas   WITH (NOLOCK) 

inner join tblAppraisalSelfAppLog  ALog on ALog.AppraisalSelfMasterId=mas.AppraisalSelfMasterId
 where   mas.FinancialYearId=@FinancialYearId  and ALog.ActionStatus <>'Drafted' and ALog.ForEmpInfoId=@EmpInfoId and 
 ALog.ActionStatus='Verified'";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }
       public DataTable GetAppppStatusDAL(string EmpID, string FinId)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", EmpID));
           aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", FinId));
           const string queryStr = @"
 select  ALog.ForEmpInfoId, * from  tblAppraisalMaster mas   WITH (NOLOCK) 

inner join tblAppraisalMasterAppLog  ALog on ALog.AppraisalMasterId=mas.AppraisalMasterId
 where   mas.FinancialYearId=@FinancialYearId and ALog.ActionStatus <>'Drafted' and ALog.ForEmpInfoId=@EmpInfoId  and 
 ALog.ActionStatus='Approved'";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }


       public DataTable GetAppppStatusDALPend(string EmpID, string FinId)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", EmpID));
           aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", FinId));
           const string queryStr = @"
 select  ALog.ForEmpInfoId, * from  tblAppraisalMaster mas  WITH (NOLOCK) 

inner join tblAppraisalMasterAppLog  ALog on ALog.AppraisalMasterId=mas.AppraisalMasterId
 where   mas.FinancialYearId=@FinancialYearId and ALog.ActionStatus <>'Drafted' and ALog.ForEmpInfoId=@EmpInfoId  and 
 ALog.ActionStatus='Verified'";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }


       public DataTable GetEmpChildrenInfoDAL(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
           const string queryStr = @"SELECT  tblOccupation.Description AS ChildrenOccupationName, CONVERT(VARCHAR(10), tblEmpChildren.ChildrenDOB, 105) AS ChildrenDOB,* from tblEmpChildren 

LEFT JOIN dbo.tblOccupation ON dbo.tblEmpChildren.ChildrenOccupation=dbo.tblOccupation.OccupationID where EmpInfoId=@EmpInfoId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }


       public DataTable GetEmpEducationInfoDAL(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
           const string queryStr = @"SELECT e.EmpEducationId,
       e.EmpInfoId,
       e.EducationNameId,
	   en.Description AS EducationName,
       e.BoardUniversityId,
	   bu.Description AS BoardUniversity,
       e.SubjectGroupId,
	   sg.Description AS SubjectGroup,
       e.EducationalInstituteId,
	   ei.Description AS EducationalInstitute,
       e.FieldOfSpecializationId,
	  sp.Description AS FieldOfSpecialization,
       e.PassingYear,
       e.Result,
       e.CgpaOrTotalMarks,
       e.EduIsLastLevel,
e.IsProfessionalEdu,
       e.IsActive FROM dbo.tblEmpEducation e 
	   LEFT JOIN dbo.tblEducationName en ON en.EducationNameID = e.EducationNameId
	   LEFT JOIN dbo.tblBoardUniversity bu ON bu.BoardUniversityID = e.BoardUniversityId
	   LEFT JOIN dbo.tblEducationSubjectGroup sg ON sg.EducationSubjectGroupID=e.SubjectGroupId
	   LEFT JOIN dbo.tblEducationalInstitution ei ON ei.InstitutionID=e.EducationalInstituteId
	   LEFT JOIN dbo.tblSpecialization sp ON sp.SpecializationID=e.FieldOfSpecializationId
	   WHERE e.IsActive=1 AND e.EmpInfoId=@EmpInfoId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }


       public DataTable GetEmpExperienceInfoDAL(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
           const string queryStr = @"SELECT   CONVERT(VARCHAR(10), tblEmpExperience.ExpFromDate, 105) AS ExpFromDate, CONVERT(VARCHAR(10), tblEmpExperience.ExpToDate, 105) AS ExpToDate, * from tblEmpExperience where EmpInfoId=@EmpInfoId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }


       public DataTable GetEmpTrainingInfoDAL(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
           const string queryStr = @"SELECT tblEmpTraining.TrainingName,  tblEmpInfoTrainingType.Description TrainingTypeName, dbo.tblEmpTraining.TrainingDescription, tblEmpTrainingInstitution.Description TrainingInstitutionName, tblCountry.Title TrainingCountryName,
 tblEmpTraining.TrFromDate, tblEmpTraining.TrToDate ,dbo.tblEmpTraining.TrainingAchievment, dbo.tblEmpTraining.TrainingDays, tblEmpTraining.TrainingPlace, tblEmpTraining.TrRemarks from tblEmpTraining
LEFT JOIN tblEmpInfoTrainingType ON tblEmpTraining.TrainingType=tblEmpInfoTrainingType.TrainingTypeID
LEFT JOIN tblEmpTrainingInstitution ON  tblEmpTraining.TrainingInstitution=tblEmpTrainingInstitution.InstitutionID
LEFT JOIN dbo.tblCountry ON  tblEmpTraining.TrainingCountry=tblCountry.CountryID WHERE tblEmpTraining.IsActive=1 and EmpInfoId=@EmpInfoId
UNION ALL 
SELECT mas.TrainingTitle TrainingName, tt.TrainingType TrainingTypeName,  mas.TrainingDetails TrainingDescription,ti.TrainingOrgName  TrainingInstitutionName, ' ' TrainingCountryName  
   
, FORMAT(mas.StartDate,'dd-MMM-yyyy') TrFromDate , FORMAT(mas.EndDate,'dd-MMM-yyyy') TrToDate, ' ' TrainingAchievment, mas.NoOfDays TrainingDays,
CASE
    WHEN TrainingOrgLocation = 0 THEN vn.VenueName
    WHEN TrainingOrgLocation != 0 THEN brn.BranchDetails
    
END AS TrainingPlace, '' TrRemarks FROM dbo.tblTrainingRecordMaster mas
LEFT JOIN  tbl_trainingRecordDetailsEmployee ddlt  ON mas.TrainingRecordMasterId = ddlt.TrainingRecordMasterId
   LEFT JOIN dbo.tblTrainingType tt ON tt.TrainingTypeID = mas.TrainingTypeId
  LEFT JOIN dbo.tblTrainingOrgInfo ti ON ti.TrainingOrgId=mas.TrainingOrgId
  LEFT JOIN tblSMCTrainingVenue vn ON mas.TrainingVenue= vn.SMCVenueID
  LEFT JOIN tblOfficeBranch brn ON brn.TrainingOrgId = mas.TrainingOrgId
 WHERE   ddlt.EmpinfoId    IN (SELECT tblEvaluateTrainingMaster.EmpInfoId FROM tblEvaluateTrainingMaster )  AND ddlt.EmpInfoId=@EmpInfoId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }

       public DataTable GetEmpReferenceInfoDAL(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
           const string queryStr = @"SELECT dbo.tblOccupation.Description RefOccupationName,* from tblEmpReference
LEFT JOIN tblOccupation ON dbo.tblEmpReference.RefOccupation=dbo.tblOccupation.OccupationID where EmpInfoId=@EmpInfoId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }


       public DataTable GetEmpNomineeInfoDAL(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
           const string queryStr = @"SELECT ('../UploadImg/'+tblEmpNominee.NomNomineImg) ShowNominationImage,CONVERT(VARCHAR(10), tblEmpNominee.NomineeDOB, 105) AS NomineeDOB,tblNominationPurpose.Description NominationPurposeName, tblRelation.Description NomineeRelationName,tblOccupation.Description NomineeOccupationName, * from tblEmpNominee
LEFT JOIN tblNominationPurpose ON tblEmpNominee.NominationPurpose=dbo.tblNominationPurpose.NPID
LEFT JOIN tblRelation ON tblEmpNominee.NomineeRelation=dbo.tblRelation.RelationID

LEFT JOIN  tblOccupation ON tblEmpNominee.NomineeOccupation=dbo.tblOccupation.OccupationID
 where EmpInfoId=@EmpInfoId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }

       public DataTable GetEmpHobbyInfoDAL(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
           const string queryStr = @"SELECT tblMasterHobby.HobbyName, * from tblEmpHobby
LEFT JOIN dbo.tblMasterHobby ON tblMasterHobby.MasterHobbyId = tblEmpHobby.MasterHobbyId where tblEmpHobby.EmpInfoId=@EmpInfoId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }


       public DataTable GetEmpExtraCurriculamInfoDAL(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
           const string queryStr = @"SELECT  tblMasterExtraCurriculam.ExtraCurriculamName,* FROM tblEmpExtraCurriculam

LEFT JOIN dbo.tblMasterExtraCurriculam ON 
tblEmpExtraCurriculam.MasterExtraCurriculamId= tblMasterExtraCurriculam.MasterExtraCurriculamId  where tblEmpExtraCurriculam.EmpInfoId=@EmpInfoId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }

       public DataTable GetEmpAchievementsInfoDAL(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
           const string queryStr = @"SELECT tblMasterAchievements.AchievementsName, * FROM tblEmpAchievements 
LEFT JOIN dbo.tblMasterAchievements ON tblMasterAchievements.MasterAchievementsId = tblEmpAchievements.MasterAchievementsId  where tblEmpAchievements.EmpInfoId=@EmpInfoId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }


       public DataTable OtherEmpHobbyInfoDAL(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
           const string queryStr = @"SELECT  tblMasterHobby.HobbyName,* FROM tblEmpHobby
LEFT JOIN dbo.tblMasterHobby ON tblMasterHobby.MasterHobbyId = tblEmpHobby.MasterHobbyId  where tblEmpHobby.EmpInfoId=@EmpInfoId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }


       public DataTable EmpOtherTalentsInfoDAL(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
           const string queryStr = @"SELECT tblMasterOtherTalents.OtherTalentsName, * FROM dbo.tblEmpOtherTalents
LEFT JOIN dbo.tblMasterOtherTalents ON tblMasterOtherTalents.MasterOtherTalentsId = tblEmpOtherTalents.MasterOtherTalentsId  where tblEmpOtherTalents.EmpInfoId=@EmpInfoId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }
   
   }
}
