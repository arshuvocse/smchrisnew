using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using DAL.DataManager;
using DAL.InternalCls;

namespace DAL.Report_DAL
{
  public  class EmployeeProfileDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public void LoadCompanyDropDownList(DropDownList ddl)
        {
            string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo  WITH (NOLOCK) WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            //string query = "SELECT * FROM tblCompanyInfo";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
        }
        public DataTable GetEmpDDL()
        {
            string queryStr = @"SELECT CompanyId as Value,CompanyName, ShortName as TextField FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            //string query = @"SELECT CompanyId as Value, ShortName as TextField FROM tblCompanyInfo";
            return aCommonInternalDal.GetDTforDDL(queryStr, null, DataBase.HRDB);
        }
        //public void LoadEmpInfo(ComboBox ddl)
        //{
        //    string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo  WITH (NOLOCK) WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
        //    //string query = "SELECT * FROM tblCompanyInfo";
        //    aCommonInternalDal.LoadDropDownValue(ddl, "CompanyName", "CompanyId", queryStr, "HRDB");
        //}

        public DataTable GetEmployeeInfoDAL(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
           string queryStr = @"SELECT  SpouseOccup.Description SpouseOccupation,SpusMax.Description SpouseMaxEducation, PreThana.Title PresentThana, PerThana.Title PermanentThana,PerDis.Titel PermanentDistrict, PreDis.Titel PresentDistrict,  PresDiv.Title PresentDivision, PerDiv.Title ParmanentDivision, tblOccupation.Description FatherOccupation, motherOcc.Description MotherOccupation,com.ShortName, Div.DivisionName,DivW.DivisionWingName, Sec.SectionName, dept.DepartmentName,
SuSec.SubSectionName, Cat.EmpCategoryName, Sgrd.GradeName, SStep.SalaryStepName, Desg.Designation,
DType.DesigTypeName, SLoc.SalaryLocation, JLOC.Location, EType.EmpType, Pro.ProjectName, eGen.EmpName ReportingEmp,
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
WHERE Emp.EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetEmployeeInfoDALBasic(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            const string queryStr = @"SELECT  'Employee ID: '+ Emp.EmpMasterCode +', Employee Name: '+Emp.EmpName  + ISNULL(',  Date of Joining: '+ FORMAT(Emp.DateOfJoin,'dd-MMM-yyyy') ,'')+    ISNULL(',  Designation: '+Desg.Designation ,'') + ISNULL(',  Department: '+dept.DepartmentName ,'') AchievementsName,0  EmpAchievementsId,0	EmpInfoId,0	MasterAchievementsId	, 0 IsActive, 0	MasterAchievementsId,''	AchievementsName,0	IsActive,''	EntryBy,''	EntryDate,''	UpdateBy,''	UpdateDate from tblEmpGeneralInfo Emp
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
WHERE Emp.EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetComeInfoDAL(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            const string queryStr = @"SELECT  com.CompanyName from tblEmpGeneralInfo Emp
left JOIN dbo.tblCompanyInfo com ON com.CompanyId = Emp.CompanyId
 
WHERE Emp.EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetComeInfoDAL_Pro(string Id, string header)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            string queryStr = @"SELECT  com.CompanyName, dv.DivisionName, '" + header + @"' as HeaderInfo from tblEmpGeneralInfo Emp
left JOIN dbo.tblCompanyInfo com ON com.CompanyId = Emp.CompanyId
left JOIN dbo.tblDivision dv ON emp.DivisionId = dv.DivisionId

 
WHERE Emp.EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetRefEmpInfoDAL(string Id)
        {

            
              string queryStr = @"SELECT Emp.ReferenceID FROM tblEmpGeneralInfo Emp
 
 
WHERE Emp.ReferenceID  IS NOT NULL AND  Emp.EmpInfoId=" + Id;
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public DataTable GetRefEmpInfoDAL2(string Id)
        {


            string queryStr = @"SELECT Emp.ReferenceID FROM tblEmpGeneralInfo Emp
 
 
WHERE   Emp.EmpInfoId=" + Id;
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public DataTable GetEmpChildrenInfoDAL(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            const string queryStr = @"SELECT  SpouseOccup.Description SpouseOccupation,SpusMax.Description SpouseMaxEducation, PreThana.Title PresentThana, PerThana.Title PermanentThana,PerDis.Titel PermanentDistrict, PreDis.Titel PresentDistrict,  PresDiv.Title PresentDivision, PerDiv.Title ParmanentDivision, tblOccupation.Description FatherOccupation, motherOcc.Description MotherOccupation,chil.ChildrenName ShortName, chil.ChildrenGender DivisionName,occ.Description DivisionWingName, FORMAT(chil.ChildrenDOB,'dd-MMM-yyyy') SectionName, chil.ChildrenMaritalStatus DepartmentName,
'' SubSectionName, Cat.EmpCategoryName, Sgrd.GradeName, SStep.SalaryStepName, Desg.Designation,
DType.DesigTypeName, SLoc.SalaryLocation, JLOC.Location, EType.EmpType, Pro.ProjectName, eGen.EmpName ReportingEmp,
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

LEFT JOIN dbo.tblEmpChildren chil ON chil.EmpInfoId = Emp.EmpInfoId
  left JOIN dbo.tblOccupation occ ON occ.OccupationID=chil.ChildrenOccupation
WHERE Emp.EmpInfoId=@EmpInfoId";
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
            const string queryStr = @"SELECT CAST((DATEDIFF(year, tblEmpExperience.ExpFromDate, ExpToDate)  - (CASE WHEN DATEADD(year, DATEDIFF(year, tblEmpExperience.ExpFromDate, ExpToDate), tblEmpExperience.ExpFromDate) > ExpToDate THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(ExpToDate - DATEADD(year, DATEDIFF(year, tblEmpExperience.ExpFromDate, ExpToDate), tblEmpExperience.ExpFromDate)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(ExpToDate - DATEADD(year, DATEDIFF(year, tblEmpExperience.ExpFromDate, ExpToDate), tblEmpExperience.ExpFromDate)) -(CASE WHEN DATEADD(year, DATEDIFF(year, tblEmpExperience.ExpFromDate, ExpToDate), tblEmpExperience.ExpFromDate) > ExpToDate THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'
  ExpRemarks, * from tblEmpExperience WHERE tblEmpExperience.IsActive=1 and tblEmpExperience.EmpInfoId=@EmpInfoId";
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
 WHERE   ddlt.EmpinfoId    IN (SELECT tblEvaluateTrainingMaster.EmpInfoId FROM tblEvaluateTrainingMaster )  AND ddlt.EmpInfoId=@EmpInfoId ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable GetEmpTrainingCountInfoDAL(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            const string queryStr = @"SELECT  ISNULL(COUNT(dbo.tblEmpTraining.EmpTrainingId),0) TotalTrainCount, ISNULL(COUNT(tblTraC.TrainCountOwnCountry),0) TrainCountOwnCountry,  ISNULL(COUNT(tblTraOtherCountry.TrainCountOtherCountry),0)TrainCountOtherCountry  from tblEmpTraining   
 LEFT JOIN (SELECT EmpTrainingId, COUNT(EmpTrainingId) TrainCountOwnCountry FROM tblEmpTraining
WHERE TrainingCountry=13 AND tblEmpTraining.EmpInfoId=@EmpInfoId  AND tblEmpTraining.IsActive=1 GROUP BY EmpTrainingId) tblTraC ON tblEmpTraining.EmpTrainingId=tblTraC.EmpTrainingId

 LEFT JOIN (SELECT EmpTrainingId, ISNULL(COUNT(EmpTrainingId),0) TrainCountOtherCountry FROM tblEmpTraining
WHERE TrainingCountry NOT IN(13) AND tblEmpTraining.EmpInfoId=@EmpInfoId  AND tblEmpTraining.IsActive=1 GROUP BY EmpTrainingId) tblTraOtherCountry ON tblEmpTraining.EmpTrainingId=tblTraOtherCountry.EmpTrainingId

WHERE tblEmpTraining.EmpInfoId=@EmpInfoId  AND  tblEmpTraining.IsActive=1
UNION ALL 
SELECT ISNULL(COUNT(mas.TrainingRecordMasterId ),0) TrainingRecordMasterId, 0 TrainCountOwnCountry, 0 TrainCountOtherCountry FROM dbo.tblTrainingRecordMaster mas
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
LEFT JOIN tblOccupation ON dbo.tblEmpReference.RefOccupation=dbo.tblOccupation.OccupationID WHERE tblEmpReference.IsActive=1 and tblEmpReference.EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable GetPromotion(string Id)
        {
            
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
              string queryStr = @"SELECT  EMPP.Effectivedate eff,   FORMAT(EMPP.Effectivedate, 'dd-MMM-yyyy')  Effectivedate,isnull(ProType.PromotionTypeName, 'Reappointment')   AS PromotionType,
                                     CASE WHEN EMPP.IsReappointment = 1 THEN 'Yes' ELSE 'No' END AS Reappointment, EmpType.EmpType  AS PreviousGrade, EMPP.Remarks AS previousStep,
                                     psesg.Designation AS PreviousDesignation, ISNULL(ngrd.GradeCode, '')  GradeName,nSTEP.SalaryStepName,nsesg.Designation 
                                     FROM dbo.tblEmployeePromotionEntry AS EMPP WITH (NOLOCK)
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON EMPP.EmployeeId = EGI.EmpInfoId
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = EGI.CompanyId
                                     LEFT JOIN dbo.tblSalaryGrade pgrd ON pgrd.SalaryGradeId = EMPP.PSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep pstep ON pstep.SalaryStepId = EMPP.PStepId
                                     LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = EMPP.PDesignationId
                                     LEFT JOIN dbo.tblSalaryGrade ngrd ON ngrd.SalaryGradeId = EMPP.NSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep nstep ON nstep.SalaryStepId = EMPP.NSalaryStepId
                                     LEFT JOIN dbo.tblDesignation nsesg ON nsesg.DesignationId = EMPP.NDesignationId
                                     LEFT JOIN dbo.tblPromotionType ProType ON ProType.PromotionTypeId = EMPP.NPromoTypeId
                                     LEFT JOIN dbo.tblEmployeeType EmpType ON EmpType.EmpTypeId = EMPP.EmpTypeId

                           WHERE    EMPP.EmployeeId = @EmpInfoId   

 UNION ALL 

									 SELECT EffectDate eff, FORMAT(EffectDate, 'dd-MMM-yyyy') Effectivedate , TypeOfPromotion AS PromotionType,  Reappointmant Reappointment, EmpType	 PreviousGrade, tblPromotionUpgrationHistory.Remarks previousStep, '' PreviousDesignation,  ISNULL(GradeCode, '')  GradeName, SalaryStepName, '' Designation  FROM tblPromotionUpgrationHistory  WITH (NOLOCK)


						  WHERE EmployeeID = @EmpInfoId

								 UNION ALL 

									  SELECT  INC. EffectiveDate eff,FORMAT(INC.EffectiveDate, 'dd-MMM-yyyy')   EffectiveDate,  IncType.Name PromotionType , 'No' Reappointment,EmpType.EmpType AS PreviousGrade , ''  previousStep, '' PreviousDesignation,ISNULL(GradeCode, '')  GradeName,ISTP.SalaryStepName,DSG.Designation  
                               FROM dbo.tblIncrement AS INC  WITH (NOLOCK)
                             LEFT JOIN dbo.tblDesignation AS DSG ON INC.DesignationId = DSG.DesignationId
                             LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON INC.EmployeeId = EGI.EmpInfoId
                             LEFT JOIN dbo.tblDepartment AS DPT ON INC.DepartmentId = DPT.DepartmentId
                             LEFT JOIN dbo.tblSalaryGrade AS GD ON INC.SalaryGradeId = GD.SalaryGradeId
                             LEFT JOIN dbo.tblSalaryStep AS CSTP ON INC.CurrentStepId = CSTP.SalaryStepId
                             LEFT JOIN dbo.tblSalaryStep AS ISTP ON INC.IncrementalStepId = ISTP.SalaryStepId
                             LEFT JOIN dbo.tblIncrementInfoMaster AS IncType ON INC.IncrementTypeId = IncType.IncrementInfoMasterId
							LEFT JOIN dbo.tblEmpGeneralInfo AS E ON INC.EmployeeId = E.EmpInfoId
                                     LEFT JOIN dbo.tblEmployeeType EmpType ON EmpType.EmpTypeId = INC.EmpTypeId

                            WHERE INC.EmployeeId=@EmpInfoId  
							 UNION ALL
 SELECT  EffectiveDate eff,  FORMAT(EffectiveDate, 'dd-MMM-yyyy')  EffectiveDate,  'Yearly increment'    as PromotionType , 'No' Reappointment,EmpType PreviousGrade ,  tblIncrement_HistoricalData.Remarks PreviousStep ,  '' PreviousDesignation,   GradeName,   IncrementalStep SalaryStepName, DSG.Designation Designation  
								 --'Yearly increment' Name,
								  FROM tblIncrement_HistoricalData  WITH (NOLOCK)
									  INNER JOIN dbo.tblEmpGeneralInfo AS Emp ON Emp.EmpInfoId = tblIncrement_HistoricalData.EmployeeId
									   LEFT JOIN dbo.tblDesignation AS DSG ON Emp.DesignationId = DSG.DesignationId
									    LEFT JOIN dbo.tblDepartment AS DPT ON Emp.DepartmentId = DPT.DepartmentId where  Increment_HistoricalDataId is not null  AND    tblIncrement_HistoricalData.EmployeeId=@EmpInfoId 

 
 
										 UNION ALL SELECT  EMPP.Effectivedate eff,   ISNULL(FORMAT(EMPP.Effectivedate,'dd-MMM-yy'),'')   Effectivedate,case   when EMPP.TypeOfPromotion is null and  EMPP.isReappointment=1 then 'Reappointment'  when EMPP.TypeOfPromotion is null and  EMPP.IsRenew=1 then 'Renew'   when EMPP.TypeOfPromotion is null and  EMPP.IsExtension=1 then 'Extension' when EMPP.TypeOfPromotion is null and  EMPP.IsContractualToPermanent=1 then 'Contractual to Permanent'  when EMPP.TypeOfPromotion is null and  EMPP.IsPermanentToContractual=1 then 'Permanent to Contractual'  else  EMPP.TypeOfPromotion end   AS PromotionType,
                                     CASE WHEN EMPP.IsReappointment = 1 THEN 'Yes' ELSE 'No' END AS Reappointment, EmpType.EmpType  AS PreviousGrade, EMPP.Remarks AS previousStep,
                                     '' AS PreviousDesignation,ISNULL(ngrd.GradeCode, '')  GradeName,nSTEP.SalaryStepName,'' Designation 
                                     FROM dbo.tblContractualEmpManage AS EMPP WITH (NOLOCK)
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON EMPP.EmployeeId = EGI.EmpInfoId
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = EGI.CompanyId
                                     --LEFT JOIN dbo.tblSalaryGrade pgrd ON pgrd.SalaryGradeId = EMPP.PSalGradeId
                                     --LEFT JOIN dbo.tblSalaryStep pstep ON pstep.SalaryStepId = EMPP.PStepId
                                     --LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = EMPP.PDesignationId
                                     LEFT JOIN dbo.tblSalaryGrade ngrd ON ngrd.SalaryGradeId = EGI.SalaryGradeId
                                     LEFT JOIN dbo.tblSalaryStep nstep ON nstep.SalaryStepId = EGI.SalaryStepId
                                     --LEFT JOIN dbo.tblDesignation nsesg ON nsesg.DesignationId = EMPP.NDesignationId
                                     --LEFT JOIN dbo.tblPromotionType ProType ON ProType.PromotionTypeId = EMPP.NPromoTypeId
                                     LEFT JOIN dbo.tblEmployeeType EmpType ON EmpType.EmpTypeId = EMPP.EmpTypeId

                           WHERE  EMPP.EmployeeId=@EmpInfoId 



										order by eff asc
";

            // EMPP.TypeOfPromotion IN ('Joining','Reappointment')  
            //
            //										
            //										AND
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable GetPromotionParmThree(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            const string queryStr = @"SELECT   EMPP.Effectivedate,ProType.PromotionTypeName   AS PromotionType,
                                     CASE WHEN EMPP.IsReappointment = 1 THEN 'Yes' ELSE 'No' END AS Reappointment, EmpType.EmpType  AS PreviousGrade, EMPP.Remarks AS previousStep,
                                     psesg.Designation AS PreviousDesignation, ISNULL(ngrd.GradeCode, '')  GradeName,nSTEP.SalaryStepName,nsesg.Designation 
                                     FROM dbo.tblEmployeePromotionEntry AS EMPP WITH (NOLOCK)
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON EMPP.EmployeeId = EGI.EmpInfoId
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = EGI.CompanyId
                                     LEFT JOIN dbo.tblSalaryGrade pgrd ON pgrd.SalaryGradeId = EMPP.PSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep pstep ON pstep.SalaryStepId = EMPP.PStepId
                                     LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = EMPP.PDesignationId
                                     LEFT JOIN dbo.tblSalaryGrade ngrd ON ngrd.SalaryGradeId = EMPP.NSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep nstep ON nstep.SalaryStepId = EMPP.NSalaryStepId
                                     LEFT JOIN dbo.tblDesignation nsesg ON nsesg.DesignationId = EMPP.NDesignationId
                                     LEFT JOIN dbo.tblPromotionType ProType ON ProType.PromotionTypeId = EMPP.NPromoTypeId
                                     LEFT JOIN dbo.tblEmployeeType EmpType ON EmpType.EmpTypeId = EMPP.EmpTypeId

                           WHERE    EMPP.EmployeeId = @EmpInfoId     and NPromoTypeId in(1,2)

 UNION ALL 

									 SELECT FORMAT(EffectDate, 'dd-MMM-yyyy') Effectivedate , TypeOfPromotion AS PromotionType,  Reappointmant Reappointment, EmpType	 PreviousGrade, tblPromotionUpgrationHistory.Remarks previousStep, '' PreviousDesignation,  ISNULL(GradeCode, '')  GradeName, SalaryStepName, '' Designation  FROM tblPromotionUpgrationHistory  WITH (NOLOCK)


						  WHERE EmployeeID = @EmpInfoId and TypeOfPromotion in ('Promotion','Upgradation')

								 UNION ALL 

									  SELECT INC.EffectiveDate,  IncType.Name PromotionType , 'No' Reappointment,EmpType.EmpType AS PreviousGrade , ''  previousStep, '' PreviousDesignation,ISNULL(GradeCode, '')  GradeName,ISTP.SalaryStepName,DSG.Designation  
                               FROM dbo.tblIncrement AS INC  WITH (NOLOCK)
                             LEFT JOIN dbo.tblDesignation AS DSG ON INC.DesignationId = DSG.DesignationId
                             LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON INC.EmployeeId = EGI.EmpInfoId
                             LEFT JOIN dbo.tblDepartment AS DPT ON INC.DepartmentId = DPT.DepartmentId
                             LEFT JOIN dbo.tblSalaryGrade AS GD ON INC.SalaryGradeId = GD.SalaryGradeId
                             LEFT JOIN dbo.tblSalaryStep AS CSTP ON INC.CurrentStepId = CSTP.SalaryStepId
                             LEFT JOIN dbo.tblSalaryStep AS ISTP ON INC.IncrementalStepId = ISTP.SalaryStepId
                             LEFT JOIN dbo.tblIncrementInfoMaster AS IncType ON INC.IncrementTypeId = IncType.IncrementInfoMasterId
							LEFT JOIN dbo.tblEmpGeneralInfo AS E ON INC.EmployeeId = E.EmpInfoId
                                     LEFT JOIN dbo.tblEmployeeType EmpType ON EmpType.EmpTypeId = INC.EmpTypeId

                            WHERE INC.EmployeeId=@EmpInfoId    and IncrementTypeId in (1)
							 UNION ALL
 SELECT  EffectiveDate EffectiveDate,  'Yearly increment'    as PromotionType , 'No' Reappointment,EmpType PreviousGrade ,  tblIncrement_HistoricalData.Remarks PreviousStep ,  '' PreviousDesignation,   GradeName,   IncrementalStep SalaryStepName, DSG.Designation Designation  
								 --'Yearly increment' Name,
								  FROM tblIncrement_HistoricalData  WITH (NOLOCK)
									  INNER JOIN dbo.tblEmpGeneralInfo AS Emp ON Emp.EmpInfoId = tblIncrement_HistoricalData.EmployeeId
									   LEFT JOIN dbo.tblDesignation AS DSG ON Emp.DesignationId = DSG.DesignationId
									    LEFT JOIN dbo.tblDepartment AS DPT ON Emp.DepartmentId = DPT.DepartmentId where  Increment_HistoricalDataId is not null  AND    tblIncrement_HistoricalData.EmployeeId=@EmpInfoId 

										 
										order by EffectiveDate asc
";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable GetPromotionMulti(string Id, string olll)
        {


            string queryStr = @"SELECT  EMPP.Effectivedate eff,  FORMAT(EMPP.Effectivedate, 'dd-MMM-yyyy')  Effectivedate, ProType.PromotionTypeName   AS PromotionType,
                                     CASE WHEN EMPP.IsReappointment = 1 THEN 'Yes' ELSE 'No' END AS Reappointment, EmpType.EmpType  AS PreviousGrade, EMPP.Remarks AS previousStep,
                                     psesg.Designation AS PreviousDesignation, ISNULL(ngrd.GradeCode, '')  GradeName,nSTEP.SalaryStepName,nsesg.Designation 
                                     FROM dbo.tblEmployeePromotionEntry AS EMPP WITH (NOLOCK)
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON EMPP.EmployeeId = EGI.EmpInfoId
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = EGI.CompanyId
                                     LEFT JOIN dbo.tblSalaryGrade pgrd ON pgrd.SalaryGradeId = EMPP.PSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep pstep ON pstep.SalaryStepId = EMPP.PStepId
                                     LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = EMPP.PDesignationId
                                     LEFT JOIN dbo.tblSalaryGrade ngrd ON ngrd.SalaryGradeId = EMPP.NSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep nstep ON nstep.SalaryStepId = EMPP.NSalaryStepId
                                     LEFT JOIN dbo.tblDesignation nsesg ON nsesg.DesignationId = EMPP.NDesignationId
                                     LEFT JOIN dbo.tblPromotionType ProType ON ProType.PromotionTypeId = EMPP.NPromoTypeId
                                     LEFT JOIN dbo.tblEmployeeType EmpType ON EmpType.EmpTypeId = EMPP.EmpTypeId

                           WHERE    EMPP." + Id + @"   

 UNION ALL 

									 SELECT  EffectDate eff,   FORMAT(EffectDate, 'dd-MMM-yyyy') Effectivedate , TypeOfPromotion AS PromotionType,  Reappointmant Reappointment, EmpType	 PreviousGrade, tblPromotionUpgrationHistory.Remarks previousStep, '' PreviousDesignation,  ISNULL(GradeCode, '')  GradeName, SalaryStepName, '' Designation  FROM tblPromotionUpgrationHistory  WITH (NOLOCK)


						  WHERE  " + Id + @"

								 UNION ALL 

									  SELECT  INC. EffectiveDate eff, FORMAT(INC.EffectiveDate, 'dd-MMM-yyyy')   EffectiveDate,  IncType.Name PromotionType , 'No' Reappointment,EmpType.EmpType AS PreviousGrade , ''  previousStep, '' PreviousDesignation,ISNULL(GradeCode, '')  GradeName,ISTP.SalaryStepName,DSG.Designation  
                               FROM dbo.tblIncrement AS INC  WITH (NOLOCK)
                             LEFT JOIN dbo.tblDesignation AS DSG ON INC.DesignationId = DSG.DesignationId
                             LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON INC.EmployeeId = EGI.EmpInfoId
                             LEFT JOIN dbo.tblDepartment AS DPT ON INC.DepartmentId = DPT.DepartmentId
                             LEFT JOIN dbo.tblSalaryGrade AS GD ON INC.SalaryGradeId = GD.SalaryGradeId
                             LEFT JOIN dbo.tblSalaryStep AS CSTP ON INC.CurrentStepId = CSTP.SalaryStepId
                             LEFT JOIN dbo.tblSalaryStep AS ISTP ON INC.IncrementalStepId = ISTP.SalaryStepId
                             LEFT JOIN dbo.tblIncrementInfoMaster AS IncType ON INC.IncrementTypeId = IncType.IncrementInfoMasterId
							LEFT JOIN dbo.tblEmpGeneralInfo AS E ON INC.EmployeeId = E.EmpInfoId
                                     LEFT JOIN dbo.tblEmployeeType EmpType ON EmpType.EmpTypeId = INC.EmpTypeId

                            WHERE INC." + Id + @" 
							 UNION ALL
 SELECT  EffectiveDate eff,   FORMAT(EffectiveDate, 'dd-MMM-yyyy')  EffectiveDate,  'Yearly increment'    as PromotionType , 'No' Reappointment,EmpType PreviousGrade ,  tblIncrement_HistoricalData.Remarks PreviousStep ,  '' PreviousDesignation,   GradeName,   IncrementalStep SalaryStepName, DSG.Designation Designation  
								 --'Yearly increment' Name,
								  FROM tblIncrement_HistoricalData  WITH (NOLOCK)
									  INNER JOIN dbo.tblEmpGeneralInfo AS Emp ON Emp.EmpInfoId = tblIncrement_HistoricalData.EmployeeId
									   LEFT JOIN dbo.tblDesignation AS DSG ON Emp.DesignationId = DSG.DesignationId
									    LEFT JOIN dbo.tblDepartment AS DPT ON Emp.DepartmentId = DPT.DepartmentId where  Increment_HistoricalDataId is not null  AND    tblIncrement_HistoricalData." + Id + @" 


 

								

										 UNION ALL SELECT   EMPP.Effectivedate eff,  ISNULL(FORMAT(EMPP.Effectivedate,'dd-MMM-yy'),'')   Effectivedate,case   when EMPP.TypeOfPromotion is null and  EMPP.isReappointment=1 then 'Reappointment'  when EMPP.TypeOfPromotion is null and  EMPP.IsRenew=1 then 'Renew'   when EMPP.TypeOfPromotion is null and  EMPP.IsExtension=1 then 'Extension' when EMPP.TypeOfPromotion is null and  EMPP.IsContractualToPermanent=1 then 'Contractual to Permanent'  when EMPP.TypeOfPromotion is null and  EMPP.IsPermanentToContractual=1 then 'Permanent to Contractual'  else  EMPP.TypeOfPromotion end   AS PromotionType,
                                     CASE WHEN EMPP.IsReappointment = 1 THEN 'Yes' ELSE 'No' END AS Reappointment, EmpType.EmpType  AS PreviousGrade, EMPP.Remarks AS previousStep,
                                     '' AS PreviousDesignation,ISNULL(ngrd.GradeCode, '')  GradeName,nSTEP.SalaryStepName,'' Designation 
                                     FROM dbo.tblContractualEmpManage AS EMPP WITH (NOLOCK)
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON EMPP.EmployeeId = EGI.EmpInfoId
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = EGI.CompanyId
                                     --LEFT JOIN dbo.tblSalaryGrade pgrd ON pgrd.SalaryGradeId = EMPP.PSalGradeId
                                     --LEFT JOIN dbo.tblSalaryStep pstep ON pstep.SalaryStepId = EMPP.PStepId
                                     --LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = EMPP.PDesignationId
                                     LEFT JOIN dbo.tblSalaryGrade ngrd ON ngrd.SalaryGradeId = EGI.SalaryGradeId
                                     LEFT JOIN dbo.tblSalaryStep nstep ON nstep.SalaryStepId = EGI.SalaryStepId
                                     --LEFT JOIN dbo.tblDesignation nsesg ON nsesg.DesignationId = EMPP.NDesignationId
                                     --LEFT JOIN dbo.tblPromotionType ProType ON ProType.PromotionTypeId = EMPP.NPromoTypeId
                                     LEFT JOIN dbo.tblEmployeeType EmpType ON EmpType.EmpTypeId = EMPP.EmpTypeId

                           WHERE   EMPP." + Id + @"

 

										order by eff asc
";
            return aCommonInternalDal.DataContainerDataTable(queryStr,   DataBase.HRDB);
        }



        public DataTable GetPromotionMultiParmThree(string Id)
        {


            string queryStr = @"SELECT   EMPP.Effectivedate,ProType.PromotionTypeName   AS PromotionType,
                                     CASE WHEN EMPP.IsReappointment = 1 THEN 'Yes' ELSE 'No' END AS Reappointment, EmpType.EmpType  AS PreviousGrade, EMPP.Remarks AS previousStep,
                                     psesg.Designation AS PreviousDesignation, ISNULL(ngrd.GradeCode, '')  GradeName,nSTEP.SalaryStepName,nsesg.Designation 
                                     FROM dbo.tblEmployeePromotionEntry AS EMPP WITH (NOLOCK)
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON EMPP.EmployeeId = EGI.EmpInfoId
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = EGI.CompanyId
                                     LEFT JOIN dbo.tblSalaryGrade pgrd ON pgrd.SalaryGradeId = EMPP.PSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep pstep ON pstep.SalaryStepId = EMPP.PStepId
                                     LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = EMPP.PDesignationId
                                     LEFT JOIN dbo.tblSalaryGrade ngrd ON ngrd.SalaryGradeId = EMPP.NSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep nstep ON nstep.SalaryStepId = EMPP.NSalaryStepId
                                     LEFT JOIN dbo.tblDesignation nsesg ON nsesg.DesignationId = EMPP.NDesignationId
                                     LEFT JOIN dbo.tblPromotionType ProType ON ProType.PromotionTypeId = EMPP.NPromoTypeId
                                     LEFT JOIN dbo.tblEmployeeType EmpType ON EmpType.EmpTypeId = EMPP.EmpTypeId

                           WHERE    EMPP." + Id + @"       and NPromoTypeId in(1,2)

 UNION ALL 

									 SELECT FORMAT(EffectDate, 'dd-MMM-yyyy') Effectivedate , TypeOfPromotion AS PromotionType,  Reappointmant Reappointment, EmpType	 PreviousGrade, tblPromotionUpgrationHistory.Remarks previousStep, '' PreviousDesignation,  ISNULL(GradeCode, '')  GradeName, SalaryStepName, '' Designation  FROM tblPromotionUpgrationHistory  WITH (NOLOCK)


						  WHERE  " + Id + @" and TypeOfPromotion in ('Promotion','Upgradation')
 
								 UNION ALL 

									  SELECT INC.EffectiveDate,  IncType.Name PromotionType , 'No' Reappointment,EmpType.EmpType AS PreviousGrade , ''  previousStep, '' PreviousDesignation,ISNULL(GradeCode, '')  GradeName,ISTP.SalaryStepName,DSG.Designation  
                               FROM dbo.tblIncrement AS INC  WITH (NOLOCK)
                             LEFT JOIN dbo.tblDesignation AS DSG ON INC.DesignationId = DSG.DesignationId
                             LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON INC.EmployeeId = EGI.EmpInfoId
                             LEFT JOIN dbo.tblDepartment AS DPT ON INC.DepartmentId = DPT.DepartmentId
                             LEFT JOIN dbo.tblSalaryGrade AS GD ON INC.SalaryGradeId = GD.SalaryGradeId
                             LEFT JOIN dbo.tblSalaryStep AS CSTP ON INC.CurrentStepId = CSTP.SalaryStepId
                             LEFT JOIN dbo.tblSalaryStep AS ISTP ON INC.IncrementalStepId = ISTP.SalaryStepId
                             LEFT JOIN dbo.tblIncrementInfoMaster AS IncType ON INC.IncrementTypeId = IncType.IncrementInfoMasterId
							LEFT JOIN dbo.tblEmpGeneralInfo AS E ON INC.EmployeeId = E.EmpInfoId
                                     LEFT JOIN dbo.tblEmployeeType EmpType ON EmpType.EmpTypeId = INC.EmpTypeId

                            WHERE INC." + Id + @"   and IncrementTypeId in (1)
							 UNION ALL
 SELECT  EffectiveDate EffectiveDate,  'Yearly increment'    as PromotionType , 'No' Reappointment,EmpType PreviousGrade ,  tblIncrement_HistoricalData.Remarks PreviousStep ,  '' PreviousDesignation,   GradeName,   IncrementalStep SalaryStepName, DSG.Designation Designation  
								 --'Yearly increment' Name,
								  FROM tblIncrement_HistoricalData  WITH (NOLOCK)
									  INNER JOIN dbo.tblEmpGeneralInfo AS Emp ON Emp.EmpInfoId = tblIncrement_HistoricalData.EmployeeId
									   LEFT JOIN dbo.tblDesignation AS DSG ON Emp.DesignationId = DSG.DesignationId
									    LEFT JOIN dbo.tblDepartment AS DPT ON Emp.DepartmentId = DPT.DepartmentId where  Increment_HistoricalDataId is not null  AND    tblIncrement_HistoricalData." + Id + @" 

									 

										order by EffectiveDate asc
";
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }


        public DataTable GetPromotion_Pro(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            const string queryStr = @"SELECT CI.ShortName, nsesg.Designation, DPT.DepartmentName, EMPP.Effectivedate,ProType.PromotionTypeName   AS PromotionType,
                                     CASE WHEN EMPP.IsReappointment = 1 THEN 'Yes' ELSE 'No' END AS Reappointment, EmpType.EmpType  AS PreviousGrade, EMPP.Remarks AS previousStep,
                                     psesg.Designation AS PreviousDesignation, ISNULL(ngrd.GradeCode, '')  GradeName,nSTEP.SalaryStepName + ISNULL(+' : ' +CAST( nSTEP.GrossAmount as nvarchar(max)),'') AS SalaryStepName ,nsesg.Designation 
                                     FROM dbo.tblEmployeePromotionEntry AS EMPP WITH (NOLOCK)
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON EMPP.EmployeeId = EGI.EmpInfoId
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = EGI.CompanyId
                                     LEFT JOIN dbo.tblSalaryGrade pgrd ON pgrd.SalaryGradeId = EMPP.PSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep pstep ON pstep.SalaryStepId = EMPP.PStepId
                                     LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = EMPP.PDesignationId
                                     LEFT JOIN dbo.tblSalaryGrade ngrd ON ngrd.SalaryGradeId = EMPP.NSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep nstep ON nstep.SalaryStepId = EMPP.NSalaryStepId
                                     LEFT JOIN dbo.tblDesignation nsesg ON nsesg.DesignationId = EMPP.NDesignationId
                                     LEFT JOIN dbo.tblPromotionType ProType ON ProType.PromotionTypeId = EMPP.NPromoTypeId
                                     LEFT JOIN dbo.tblEmployeeType EmpType ON EmpType.EmpTypeId = EMPP.EmpTypeId
									   LEFT JOIN dbo.tblDepartment AS DPT ON EMPP.DepartmentId = DPT.DepartmentId
                          WHERE   ProType.PromotionTypeName in ('Promotion') and    EMPP.EmployeeId = @EmpInfoId   

 UNION ALL 

									 SELECT  CI.ShortName, '' Designation, '' DepartmentName,  FORMAT(EffectDate, 'dd-MMM-yyyy') Effectivedate , TypeOfPromotion AS PromotionType,  Reappointmant Reappointment, EmpType	 PreviousGrade, tblPromotionUpgrationHistory.Remarks previousStep, '' PreviousDesignation,  ISNULL(GradeCode, '')  GradeName, SalaryStepName, '' Designation  FROM tblPromotionUpgrationHistory  WITH (NOLOCK)
									   left JOIN dbo.tblEmpGeneralInfo AS emp ON emp.EmpInfoId = tblPromotionUpgrationHistory.EmployeeID

									   left JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = emp.CompanyId
									      LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = emp.DesignationId
										     LEFT JOIN dbo.tblDepartment AS DPT ON emp.DepartmentId = DPT.DepartmentId
						 WHERE TypeOfPromotion in  ('Promotion') and  EmployeeID = @EmpInfoId

								   

										order by EffectiveDate desc
";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable GetPromotionMulti_Pro(string Id)
        {


            string queryStr = @"SELECT CI.ShortName, nsesg.Designation, DPT.DepartmentName, EMPP.Effectivedate,ProType.PromotionTypeName   AS PromotionType,
                                     CASE WHEN EMPP.IsReappointment = 1 THEN 'Yes' ELSE 'No' END AS Reappointment, EmpType.EmpType  AS PreviousGrade, EMPP.Remarks AS previousStep,
                                     psesg.Designation AS PreviousDesignation, ISNULL(ngrd.GradeCode, '')  GradeName,nSTEP.SalaryStepName + ISNULL(+' : ' +CAST( nSTEP.GrossAmount as nvarchar(max)),'') AS SalaryStepName ,nsesg.Designation 
                                     FROM dbo.tblEmployeePromotionEntry AS EMPP WITH (NOLOCK)
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON EMPP.EmployeeId = EGI.EmpInfoId
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = EGI.CompanyId
                                     LEFT JOIN dbo.tblSalaryGrade pgrd ON pgrd.SalaryGradeId = EMPP.PSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep pstep ON pstep.SalaryStepId = EMPP.PStepId
                                     LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = EMPP.PDesignationId
                                     LEFT JOIN dbo.tblSalaryGrade ngrd ON ngrd.SalaryGradeId = EMPP.NSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep nstep ON nstep.SalaryStepId = EMPP.NSalaryStepId
                                     LEFT JOIN dbo.tblDesignation nsesg ON nsesg.DesignationId = EMPP.NDesignationId
                                     LEFT JOIN dbo.tblPromotionType ProType ON ProType.PromotionTypeId = EMPP.NPromoTypeId
                                     LEFT JOIN dbo.tblEmployeeType EmpType ON EmpType.EmpTypeId = EMPP.EmpTypeId
									   LEFT JOIN dbo.tblDepartment AS DPT ON EMPP.DepartmentId = DPT.DepartmentId
                          WHERE  ProType.PromotionTypeName in ('Promotion') and   EMPP.EmployeeId " + Id + @"  

 UNION ALL 

									 SELECT  CI.ShortName, '' Designation, '' DepartmentName,  FORMAT(EffectDate, 'dd-MMM-yyyy') Effectivedate , TypeOfPromotion AS PromotionType,  Reappointmant Reappointment, EmpType	 PreviousGrade, tblPromotionUpgrationHistory.Remarks previousStep, '' PreviousDesignation,  ISNULL(GradeCode, '')  GradeName, SalaryStepName, '' Designation  FROM tblPromotionUpgrationHistory  WITH (NOLOCK)
									   left JOIN dbo.tblEmpGeneralInfo AS emp ON emp.EmpInfoId = tblPromotionUpgrationHistory.EmployeeID

									   left JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = emp.CompanyId
									      LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = emp.DesignationId
										     LEFT JOIN dbo.tblDepartment AS DPT ON emp.DepartmentId = DPT.DepartmentId
						 WHERE  TypeOfPromotion in  ('Promotion') and  EmployeeID " + Id + @"

								

										order by EffectiveDate desc
";
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }


        public DataTable GetPromotion_Pro_Up(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            const string queryStr = @"SELECT CI.ShortName, psesg.Designation, DPT.DepartmentName, EMPP.Effectivedate,ProType.PromotionTypeName   AS PromotionType,
                                     CASE WHEN EMPP.IsReappointment = 1 THEN 'Yes' ELSE 'No' END AS Reappointment, EmpType.EmpType  AS PreviousGrade, EMPP.Remarks AS previousStep,
                                     psesg.Designation AS PreviousDesignation, ISNULL(ngrd.GradeCode, '')  GradeName,nSTEP.SalaryStepName + ISNULL(+' : ' +CAST( nSTEP.GrossAmount as nvarchar(max)),'') AS SalaryStepName ,nsesg.Designation 
                                     FROM dbo.tblEmployeePromotionEntry AS EMPP WITH (NOLOCK)
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON EMPP.EmployeeId = EGI.EmpInfoId
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = EGI.CompanyId
                                     LEFT JOIN dbo.tblSalaryGrade pgrd ON pgrd.SalaryGradeId = EMPP.PSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep pstep ON pstep.SalaryStepId = EMPP.PStepId
                                     LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = EMPP.PDesignationId
                                     LEFT JOIN dbo.tblSalaryGrade ngrd ON ngrd.SalaryGradeId = EMPP.NSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep nstep ON nstep.SalaryStepId = EMPP.NSalaryStepId
                                     LEFT JOIN dbo.tblDesignation nsesg ON nsesg.DesignationId = EMPP.NDesignationId
                                     LEFT JOIN dbo.tblPromotionType ProType ON ProType.PromotionTypeId = EMPP.NPromoTypeId
                                     LEFT JOIN dbo.tblEmployeeType EmpType ON EmpType.EmpTypeId = EMPP.EmpTypeId
									   LEFT JOIN dbo.tblDepartment AS DPT ON EMPP.DepartmentId = DPT.DepartmentId
                          WHERE   ProType.PromotionTypeName in ('Upgradation') and    EMPP.EmployeeId = @EmpInfoId   

 UNION ALL 

									 SELECT  CI.ShortName, '' Designation, '' DepartmentName,  FORMAT(EffectDate, 'dd-MMM-yyyy') Effectivedate , TypeOfPromotion AS PromotionType,  Reappointmant Reappointment, EmpType	 PreviousGrade, tblPromotionUpgrationHistory.Remarks previousStep, '' PreviousDesignation,  ISNULL(GradeCode, '')  GradeName, SalaryStepName, '' Designation  FROM tblPromotionUpgrationHistory  WITH (NOLOCK)
									   left JOIN dbo.tblEmpGeneralInfo AS emp ON emp.EmpInfoId = tblPromotionUpgrationHistory.EmployeeID

									   left JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = emp.CompanyId
									      LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = emp.DesignationId
										     LEFT JOIN dbo.tblDepartment AS DPT ON emp.DepartmentId = DPT.DepartmentId
						 WHERE TypeOfPromotion in  ('Upgradation') and  EmployeeID = @EmpInfoId

								   

										order by EffectiveDate desc
";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable GetPromotionMulti_Pro_Up(string Id)
        {


            string queryStr = @"SELECT CI.ShortName, psesg.Designation, DPT.DepartmentName, EMPP.Effectivedate,ProType.PromotionTypeName   AS PromotionType,
                                     CASE WHEN EMPP.IsReappointment = 1 THEN 'Yes' ELSE 'No' END AS Reappointment, EmpType.EmpType  AS PreviousGrade, EMPP.Remarks AS previousStep,
                                     psesg.Designation AS PreviousDesignation, ISNULL(ngrd.GradeCode, '')  GradeName,nSTEP.SalaryStepName + ISNULL(+' : ' +CAST( nSTEP.GrossAmount as nvarchar(max)),'') AS SalaryStepName ,nsesg.Designation 
                                     FROM dbo.tblEmployeePromotionEntry AS EMPP WITH (NOLOCK)
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON EMPP.EmployeeId = EGI.EmpInfoId
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = EGI.CompanyId
                                     LEFT JOIN dbo.tblSalaryGrade pgrd ON pgrd.SalaryGradeId = EMPP.PSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep pstep ON pstep.SalaryStepId = EMPP.PStepId
                                     LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = EMPP.PDesignationId
                                     LEFT JOIN dbo.tblSalaryGrade ngrd ON ngrd.SalaryGradeId = EMPP.NSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep nstep ON nstep.SalaryStepId = EMPP.NSalaryStepId
                                     LEFT JOIN dbo.tblDesignation nsesg ON nsesg.DesignationId = EMPP.NDesignationId
                                     LEFT JOIN dbo.tblPromotionType ProType ON ProType.PromotionTypeId = EMPP.NPromoTypeId
                                     LEFT JOIN dbo.tblEmployeeType EmpType ON EmpType.EmpTypeId = EMPP.EmpTypeId
									   LEFT JOIN dbo.tblDepartment AS DPT ON EMPP.DepartmentId = DPT.DepartmentId
                          WHERE  ProType.PromotionTypeName in ('Upgradation') and   EMPP.EmployeeId " + Id + @"  

 UNION ALL 

									 SELECT  CI.ShortName,  '' Designation, '' DepartmentName,  FORMAT(EffectDate, 'dd-MMM-yyyy') Effectivedate , TypeOfPromotion AS PromotionType,  Reappointmant Reappointment, EmpType	 PreviousGrade, tblPromotionUpgrationHistory.Remarks previousStep, '' PreviousDesignation,  ISNULL(GradeCode, '')  GradeName, SalaryStepName, '' Designation  FROM tblPromotionUpgrationHistory  WITH (NOLOCK)
									   left JOIN dbo.tblEmpGeneralInfo AS emp ON emp.EmpInfoId = tblPromotionUpgrationHistory.EmployeeID

									   left JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = emp.CompanyId
									      LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = emp.DesignationId
										     LEFT JOIN dbo.tblDepartment AS DPT ON emp.DepartmentId = DPT.DepartmentId
						 WHERE  TypeOfPromotion in  ('Upgradation') and  EmployeeID " + Id + @"

								

										order by EffectiveDate desc
";
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public DataTable GetTransfer(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            const string queryStr = @" SELECT  EPE.Effectivedate, Emp.EmpMasterCode,  NDEs.Designation  FinancialYearDesc,  '' InterCompanyTransfer, com2.ShortName NewCompany, '' NewOffice, '' NewPlace, '' NewDivision, '' NewWing, '' NewDepartment,
									  '' NewSection, '' NewSubSection       From tblEmployeeReDesignation EPE WITH (NOLOCK)
 inner JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
 inner JOIN dbo.tblCompanyInfo  com ON com.CompanyId = Emp.CompanyId


  left JOIN dbo.tblDesignation  PDEG ON EPE.PDesignationId = PDEG.DesignationId
  left JOIN dbo.tblDesignation  NDEs ON EPE.NDesignationId = NDEs.DesignationId
 
 
  left JOIN dbo.tblDepartment  NDEG ON EPE.DepartmentId = NDEG.DepartmentId
  left JOIN  dbo.tblCompanyInfo com2 ON com2.CompanyId = EPE.CompanyId 
	  
   WHERE EPE.EmployeeId  in ( @EmpInfoId  ) 
   UNION ALL SELECT   EMPP.Effectivedate, EGI.EmpMasterCode,nsesg.Designation FinancialYearDesc,  '' InterCompanyTransfer, CI.ShortName NewCompany,
   SL.SalaryLocation NewOffice,
JL.Location  NewPlace, DV.DivisionName NewDivision, '' NewWing, ISNULL(NDPT.DepartmentName,'') +ISNULL( ', '+NW.DivisionWingName,'') AS NewDepartment  ,
									  '' NewSection, '' NewSubSection 
                                     FROM dbo.tblEmployeePromotionEntry AS EMPP WITH (NOLOCK)
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON EMPP.EmployeeId = EGI.EmpInfoId
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = EGI.CompanyId
                                     LEFT JOIN dbo.tblSalaryGrade pgrd ON pgrd.SalaryGradeId = EMPP.PSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep pstep ON pstep.SalaryStepId = EMPP.PStepId
                                     LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = EMPP.PDesignationId
                                     LEFT JOIN dbo.tblSalaryGrade ngrd ON ngrd.SalaryGradeId = EMPP.NSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep nstep ON nstep.SalaryStepId = EMPP.NSalaryStepId
                                     LEFT JOIN dbo.tblDesignation nsesg ON nsesg.DesignationId = EMPP.NDesignationId
                                     LEFT JOIN dbo.tblPromotionType ProType ON ProType.PromotionTypeId = EMPP.NPromoTypeId
                                     LEFT JOIN dbo.tblEmployeeType EmpType ON EmpType.EmpTypeId = EMPP.EmpTypeId
									  LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EGI.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EGI.JobLocationId 
								  left JOIN dbo.tblDivisionWing NW ON NW.DivisionWId = EGI.DivisionWId
                                     LEFT JOIN dbo.tblDepartment NDPT ON NDPT.DepartmentId = EGI.DepartmentId
									   LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EGI.DivisionId
                          WHERE EMPP.IsReappointment=0 AND  EMPP.EmployeeId in ( @EmpInfoId  ) 
    UNION ALL 

SELECT  TD.EffectiveDate,EGI.EmpMasterCode,  dgs.Designation FinancialYearDesc,CASE WHEN TD.IsInterCompanyTransfer = 1 THEN 'Yes' ELSE 'No' END AS InterCompanyTransfer,NCI.ShortName AS NewCompany,
                                     NLC.SalaryLocation AS NewOffice, NLOC.Location AS NewPlace, DSN.DivisionName AS NewDivision,
                                      NW.DivisionWingName AS NewWing,  ISNULL(NDPT.DepartmentName,'') +ISNULL( ', '+NW.DivisionWingName,'') AS NewDepartment  ,
                                     NSec.SectionName AS NewSection, NSuSec.SubSectionName AS NewSubSection 
                                     FROM dbo.tblEmpTransferAndRedesignation AS TD  WITH (NOLOCK)
                                     INNER JOIN dbo.tblFinancialYear AS FIN ON FIN.FinancialYearId = TD.FinancialYearId
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON TD.EmployeeId = EGI.EmpInfoId
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = TD.OldCompanyId
                                     INNER JOIN dbo.tblCompanyInfo AS NCI ON NCI.CompanyId = TD.NewCompanyId
                                     LEFT JOIN dbo.tblSalaryLocation PLC ON PLC.SalaryLoationId = TD.OldSalaryLocationId
                                     LEFT JOIN dbo.tblSalaryLocation NLC ON NLC.SalaryLoationId = TD.NewSalaryLocationId
                                     LEFT JOIN dbo.tblJobLocation NLOC ON NLOC.JobLocationID = TD.NewJobLocationId
                                     LEFT JOIN dbo.tblJobLocation PLOC ON PLOC.JobLocationID = TD.OldJobLocationId
                                     LEFT JOIN dbo.tblDivision PDiv ON PDiv.DivisionId = TD.OldDivisionId
                                     left JOIN dbo.tblDivision DSN ON DSN.DivisionId = TD.NewDivisionId
                                     left JOIN dbo.tblDesignation dgs ON dgs.DesignationId = EGI.DesignationId
                                     left JOIN dbo.tblDivisionWing NW ON NW.DivisionWId = TD.NewWingId
                                     left JOIN dbo.tblDivisionWing PNW ON PNW.DivisionWId = TD.OldWingId
                                     LEFT JOIN dbo.tblDepartment Pdept ON Pdept.DepartmentId = TD.OldDepartmentId
                                     LEFT JOIN dbo.tblDepartment NDPT ON NDPT.DepartmentId = TD.NewDepartmentId
                                     LEFT JOIN dbo.tblSection PSec ON PSec.SectionId = TD.OldSectionId
                                     LEFT JOIN dbo.tblSection NSec ON NSec.SectionId = TD.NewSectionId
                                     LEFT JOIN dbo.tblSubSection PSuSec ON TD.OldSubSectionId = PSuSec.SubSectionId 
                                     LEFT JOIN dbo.tblSubSection NSuSec ON TD.NewSubSectionId = NSuSec.SubSectionId
                                    WHERE IsReappointment=1 AND  TD.EmployeeId    in ( @EmpInfoId  )   

 UNION ALL SELECT  EffectiveDate, EmployeeOldID EmpMasterCode,Designation FinancialYearDesc, '' InterCompanyTransfer,
									  CompanyName as NewCompany, Office NewOffice, Place NewPlace, Division NewDivision, Wing NewWing, Department NewDepartment,
									  Section NewSection, SubSection NewSubSection 
									    FROM tblTransferHistory   WITH (NOLOCK)   WHERE EmployeeId    in ( @EmpInfoId  ) 
										
                                         


										 
										



   ORDER BY EffectiveDate DESC
";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetTransferMulti(string Id)
        {

             
              string queryStr = @" SELECT  EPE.Effectivedate, Emp.EmpMasterCode,  NDEs.Designation  FinancialYearDesc,  '' InterCompanyTransfer, com2.ShortName NewCompany, '' NewOffice, '' NewPlace, '' NewDivision, '' NewWing, '' NewDepartment,
									  '' NewSection, '' NewSubSection       From tblEmployeeReDesignation EPE WITH (NOLOCK)
 inner JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
 inner JOIN dbo.tblCompanyInfo  com ON com.CompanyId = Emp.CompanyId


  left JOIN dbo.tblDesignation  PDEG ON EPE.PDesignationId = PDEG.DesignationId
  left JOIN dbo.tblDesignation  NDEs ON EPE.NDesignationId = NDEs.DesignationId
 
 
  left JOIN dbo.tblDepartment  NDEG ON EPE.DepartmentId = NDEG.DepartmentId
  left JOIN  dbo.tblCompanyInfo com2 ON com2.CompanyId = EPE.CompanyId 
	  
   WHERE EPE."+Id+@"
   UNION ALL SELECT   EMPP.Effectivedate, EGI.EmpMasterCode,nsesg.Designation FinancialYearDesc,  '' InterCompanyTransfer, CI.ShortName NewCompany,
   SL.SalaryLocation NewOffice,
JL.Location  NewPlace, DV.DivisionName NewDivision, '' NewWing, ISNULL(NDPT.DepartmentName,'') +ISNULL( ', '+NW.DivisionWingName,'') AS NewDepartment  ,
									  '' NewSection, '' NewSubSection 
                                     FROM dbo.tblEmployeePromotionEntry AS EMPP WITH (NOLOCK)
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON EMPP.EmployeeId = EGI.EmpInfoId
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = EGI.CompanyId
                                     LEFT JOIN dbo.tblSalaryGrade pgrd ON pgrd.SalaryGradeId = EMPP.PSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep pstep ON pstep.SalaryStepId = EMPP.PStepId
                                     LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = EMPP.PDesignationId
                                     LEFT JOIN dbo.tblSalaryGrade ngrd ON ngrd.SalaryGradeId = EMPP.NSalGradeId
                                     LEFT JOIN dbo.tblSalaryStep nstep ON nstep.SalaryStepId = EMPP.NSalaryStepId
                                     LEFT JOIN dbo.tblDesignation nsesg ON nsesg.DesignationId = EMPP.NDesignationId
                                     LEFT JOIN dbo.tblPromotionType ProType ON ProType.PromotionTypeId = EMPP.NPromoTypeId
                                     LEFT JOIN dbo.tblEmployeeType EmpType ON EmpType.EmpTypeId = EMPP.EmpTypeId
									  LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EGI.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EGI.JobLocationId 
								  left JOIN dbo.tblDivisionWing NW ON NW.DivisionWId = EGI.DivisionWId
                                     LEFT JOIN dbo.tblDepartment NDPT ON NDPT.DepartmentId = EGI.DepartmentId
									   LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EGI.DivisionId
                          WHERE EMPP.IsReappointment=0 AND  EMPP." + Id + @" 
    UNION ALL 

SELECT  TD.EffectiveDate,EGI.EmpMasterCode,  dgs.Designation FinancialYearDesc,CASE WHEN TD.IsInterCompanyTransfer = 1 THEN 'Yes' ELSE 'No' END AS InterCompanyTransfer,NCI.ShortName AS NewCompany,
                                     NLC.SalaryLocation AS NewOffice, NLOC.Location AS NewPlace, DSN.DivisionName AS NewDivision,
                                      NW.DivisionWingName AS NewWing,  ISNULL(NDPT.DepartmentName,'') +ISNULL( ', '+NW.DivisionWingName,'') AS NewDepartment  ,
                                     NSec.SectionName AS NewSection, NSuSec.SubSectionName AS NewSubSection 
                                     FROM dbo.tblEmpTransferAndRedesignation AS TD  WITH (NOLOCK)
                                     INNER JOIN dbo.tblFinancialYear AS FIN ON FIN.FinancialYearId = TD.FinancialYearId
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON TD.EmployeeId = EGI.EmpInfoId
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = TD.OldCompanyId
                                     INNER JOIN dbo.tblCompanyInfo AS NCI ON NCI.CompanyId = TD.NewCompanyId
                                     LEFT JOIN dbo.tblSalaryLocation PLC ON PLC.SalaryLoationId = TD.OldSalaryLocationId
                                     LEFT JOIN dbo.tblSalaryLocation NLC ON NLC.SalaryLoationId = TD.NewSalaryLocationId
                                     LEFT JOIN dbo.tblJobLocation NLOC ON NLOC.JobLocationID = TD.NewJobLocationId
                                     LEFT JOIN dbo.tblJobLocation PLOC ON PLOC.JobLocationID = TD.OldJobLocationId
                                     LEFT JOIN dbo.tblDivision PDiv ON PDiv.DivisionId = TD.OldDivisionId
                                     left JOIN dbo.tblDivision DSN ON DSN.DivisionId = TD.NewDivisionId
                                     left JOIN dbo.tblDesignation dgs ON dgs.DesignationId = EGI.DesignationId
                                     left JOIN dbo.tblDivisionWing NW ON NW.DivisionWId = TD.NewWingId
                                     left JOIN dbo.tblDivisionWing PNW ON PNW.DivisionWId = TD.OldWingId
                                     LEFT JOIN dbo.tblDepartment Pdept ON Pdept.DepartmentId = TD.OldDepartmentId
                                     LEFT JOIN dbo.tblDepartment NDPT ON NDPT.DepartmentId = TD.NewDepartmentId
                                     LEFT JOIN dbo.tblSection PSec ON PSec.SectionId = TD.OldSectionId
                                     LEFT JOIN dbo.tblSection NSec ON NSec.SectionId = TD.NewSectionId
                                     LEFT JOIN dbo.tblSubSection PSuSec ON TD.OldSubSectionId = PSuSec.SubSectionId 
                                     LEFT JOIN dbo.tblSubSection NSuSec ON TD.NewSubSectionId = NSuSec.SubSectionId
                                    WHERE IsReappointment=1 AND  TD." + Id + @"   

  UNION ALL 

SELECT  TD.EffectiveDate,EGI.EmpMasterCode,  dgs.Designation FinancialYearDesc,CASE WHEN TD.IsInterCompanyTransfer = 1 THEN 'Yes' ELSE 'No' END AS InterCompanyTransfer,NCI.ShortName AS NewCompany,
                                     NLC.SalaryLocation AS NewOffice, NLOC.Location AS NewPlace, DSN.DivisionName AS NewDivision,
                                      NW.DivisionWingName AS NewWing,  ISNULL(NDPT.DepartmentName,'') +ISNULL( ', '+NW.DivisionWingName,'') AS NewDepartment  ,
                                     NSec.SectionName AS NewSection, NSuSec.SubSectionName AS NewSubSection 
                                     FROM dbo.tblEmpTransferAndRedesignation AS TD  WITH (NOLOCK)
                                     INNER JOIN dbo.tblFinancialYear AS FIN ON FIN.FinancialYearId = TD.FinancialYearId
                                     INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON TD.EmployeeId = EGI.EmpInfoId
                                     INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = TD.OldCompanyId
                                     INNER JOIN dbo.tblCompanyInfo AS NCI ON NCI.CompanyId = TD.NewCompanyId
                                     LEFT JOIN dbo.tblSalaryLocation PLC ON PLC.SalaryLoationId = TD.OldSalaryLocationId
                                     LEFT JOIN dbo.tblSalaryLocation NLC ON NLC.SalaryLoationId = TD.NewSalaryLocationId
                                     LEFT JOIN dbo.tblJobLocation NLOC ON NLOC.JobLocationID = TD.NewJobLocationId
                                     LEFT JOIN dbo.tblJobLocation PLOC ON PLOC.JobLocationID = TD.OldJobLocationId
                                     LEFT JOIN dbo.tblDivision PDiv ON PDiv.DivisionId = TD.OldDivisionId
                                     left JOIN dbo.tblDivision DSN ON DSN.DivisionId = TD.NewDivisionId
                                     left JOIN dbo.tblDesignation dgs ON dgs.DesignationId = EGI.DesignationId
                                     left JOIN dbo.tblDivisionWing NW ON NW.DivisionWId = TD.NewWingId
                                     left JOIN dbo.tblDivisionWing PNW ON PNW.DivisionWId = TD.OldWingId
                                     LEFT JOIN dbo.tblDepartment Pdept ON Pdept.DepartmentId = TD.OldDepartmentId
                                     LEFT JOIN dbo.tblDepartment NDPT ON NDPT.DepartmentId = TD.NewDepartmentId
                                     LEFT JOIN dbo.tblSection PSec ON PSec.SectionId = TD.OldSectionId
                                     LEFT JOIN dbo.tblSection NSec ON NSec.SectionId = TD.NewSectionId
                                     LEFT JOIN dbo.tblSubSection PSuSec ON TD.OldSubSectionId = PSuSec.SubSectionId 
                                     LEFT JOIN dbo.tblSubSection NSuSec ON TD.NewSubSectionId = NSuSec.SubSectionId
                                    WHERE IsReappointment=0 AND  TD." + Id + @"   

 UNION ALL SELECT  EffectiveDate, EmployeeOldID EmpMasterCode,Designation FinancialYearDesc, '' InterCompanyTransfer,
									  CompanyName as NewCompany, Office NewOffice, Place NewPlace, Division NewDivision, Wing NewWing, Department NewDepartment,
									  Section NewSection, SubSection NewSubSection 
									    FROM tblTransferHistory   WITH (NOLOCK)   WHERE " + Id + @" 
										
                                         


										 
										



   ORDER BY EffectiveDate DESC
";
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public DataTable GetDiciplinary(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            const string queryStr = @"SELECT re.SuspendReasonEntry,  * from dbo.tblDiciplinaryAction mas
LEFT JOIN dbo.tblEmpGeneralInfo e ON mas.EmpInfoId=e.EmpInfoId
LEFT JOIN  tblSuspendReasonEntry  re ON re.SuspendReasonEntryId=mas.ReasonId
 where
 mas.EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetDiciplinaryMulti(string Id)
        {

            
              string queryStr = @"SELECT re.SuspendReasonEntry,  * from dbo.tblDiciplinaryAction mas
LEFT JOIN dbo.tblEmpGeneralInfo e ON mas.EmpInfoId=e.EmpInfoId
LEFT JOIN  tblSuspendReasonEntry  re ON re.SuspendReasonEntryId=mas.ReasonId
 where
 mas." + Id;
            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }

        public DataTable GetEmpNomineeInfoDAL(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            const string queryStr = @"SELECT tblNominationPurpose.Description NominationPurposeName, tblRelation.Description NomineeRelationName,tblOccupation.Description NomineeOccupationName, * from tblEmpNominee
LEFT JOIN tblNominationPurpose ON tblEmpNominee.NominationPurpose=dbo.tblNominationPurpose.NPID
LEFT JOIN tblRelation ON tblEmpNominee.NomineeRelation=dbo.tblRelation.RelationID

LEFT JOIN  tblOccupation ON tblEmpNominee.NomineeOccupation=dbo.tblOccupation.OccupationID WHERE tblEmpNominee.IsActive=1 and tblEmpNominee.EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetEmpHobbyInfoDAL(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            const string queryStr = @"SELECT tblMasterHobby.HobbyName, * from tblEmpHobby
LEFT JOIN dbo.tblMasterHobby ON tblMasterHobby.MasterHobbyId = tblEmpHobby.MasterHobbyId WHERE tblEmpHobby.IsActive=1 and tblEmpHobby.EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable GetEmpExtraCurriculamInfoDAL(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            const string queryStr = @"SELECT  tblMasterExtraCurriculam.ExtraCurriculamName,* FROM tblEmpExtraCurriculam

LEFT JOIN dbo.tblMasterExtraCurriculam ON 
tblEmpExtraCurriculam.MasterExtraCurriculamId= tblMasterExtraCurriculam.MasterExtraCurriculamId  WHERE tblEmpExtraCurriculam.IsActive=1 and tblEmpExtraCurriculam.EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetEmpAchievementsInfoDAL(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            const string queryStr = @"SELECT tblMasterAchievements.AchievementsName, * FROM tblEmpAchievements 
LEFT JOIN dbo.tblMasterAchievements ON tblMasterAchievements.MasterAchievementsId = tblEmpAchievements.MasterAchievementsId  WHERE tblEmpAchievements.IsActive=1 and tblEmpAchievements.EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }



        public DataTable LoadIncrementInfo(string Id)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            string query = @"SELECT INC.IncrementId, INC.EmployeeId, INC.EmployeeCode,E.EmpName,DSG.Designation,DPT.DepartmentName,CSTP.SalaryStepName AS PreviousStep, 
                             ISTP.SalaryStepName AS IncrementalStep, INC.EffectiveDate, IncType.Name IncrementType,ForEmp.EmpName as AwEmpName FROM dbo.tblIncrement AS INC
                             LEFT JOIN dbo.tblDesignation AS DSG ON INC.DesignationId = DSG.DesignationId
                             LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON INC.EmployeeId = EGI.EmpInfoId
                             LEFT JOIN dbo.tblDepartment AS DPT ON INC.DepartmentId = DPT.DepartmentId
                             LEFT JOIN dbo.tblSalaryGrade AS GD ON INC.SalaryGradeId = GD.SalaryGradeId
                             LEFT JOIN dbo.tblSalaryStep AS CSTP ON INC.CurrentStepId = CSTP.SalaryStepId
                             LEFT JOIN dbo.tblSalaryStep AS ISTP ON INC.IncrementalStepId = ISTP.SalaryStepId
                             LEFT JOIN dbo.tblIncrementInfoMaster AS IncType ON INC.IncrementTypeId = IncType.IncrementInfoMasterId
							LEFT JOIN dbo.tblEmpGeneralInfo AS E ON INC.EmployeeId = E.EmpInfoId
                            LEFT JOIN (SELECT IncrementId,MAX(Version)MaxVer FROM dbo.tblIncrementAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY IncrementId) AS LogApp ON LogApp.IncrementId= INC.IncrementId
								LEFT JOIN dbo.tblIncrementAppLog ON tblIncrementAppLog.IncrementId = INC.IncrementId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblIncrementAppLog.ForEmpInfoId
                                LEFT JOIN dbo.tblIncrementAppLog PreLog ON PreLog.IncrementId=INC.IncrementId AND PreLog.Version = CONVERT(INT,LogApp.MaxVer)-1
								LEFT JOIN dbo.tblEmpGeneralInfo PreEmp ON PreEmp.EmpInfoId=PreLog.ForEmpInfoId  WHERE INC.EmployeeId=@EmpInfoId   UNION ALL
 SELECT 0 AS IncrementId, EmployeeId EmployeeId, Emp.EmpMasterCode EmployeeCode, EmpName EmpName,DSG.Designation Designation ,DPT.DepartmentName DepartmentName,null PreviousStep, 
								 --'Yearly increment' Name,
								   IncrementalStep IncrementalStep, EffectiveDate EffectiveDate,    tblIncrement_HistoricalData.Remarks as IncrementType,''  as AwEmpName  FROM tblIncrement_HistoricalData
									  INNER JOIN dbo.tblEmpGeneralInfo AS Emp ON Emp.EmpInfoId = tblIncrement_HistoricalData.EmployeeId
									   LEFT JOIN dbo.tblDesignation AS DSG ON Emp.DesignationId = DSG.DesignationId
									    LEFT JOIN dbo.tblDepartment AS DPT ON Emp.DepartmentId = DPT.DepartmentId where  Increment_HistoricalDataId is not null AND  tblIncrement_HistoricalData.EmployeeId=@EmpInfoId order by EffectiveDate asc";

            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);

        }



        public DataTable LoadKPIInfolastThreeYears(string Id)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            string query = @"select fy.FinancialYearDesc,  ISNULL((ISNULL(appfin.TotalScore,0)),0) appScore from tblAppraisalFinalStatus appfin
inner join tblAppraisalMaster app on appfin.AppraisalMasterId=app.AppraisalMasterId
left join tblFinancialYear fy on fy.FinancialYearId=app.FinancialYearId

where app.CurrentStatus='Approved'  and app.EmpInfoId in (" + Id + @")   and fy.FinancialYearDesc in (select  distinct top 4 FinancialYearDesc from tblFinancialYear
order by  FinancialYearDesc desc)";

            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);

        }


        //								 UNION ALL
        // SELECT 0 AS IncrementId,  com.ShortName, tblIncrement_HistoricalData.GradeName, EmployeeId EmployeeId, Emp.EmpMasterCode EmployeeCode, EmpName EmpName,DSG.Designation Designation ,DPT.DepartmentName DepartmentName,null PreviousStep, 
        //								 --'Yearly increment' Name,
        //								   IncrementalStep IncrementalStep, EffectiveDate EffectiveDate,    tblIncrement_HistoricalData.Remarks as IncrementType,''  as AwEmpName  FROM tblIncrement_HistoricalData
        //									  INNER JOIN dbo.tblEmpGeneralInfo AS Emp ON Emp.EmpInfoId = tblIncrement_HistoricalData.EmployeeId
        //								     LEFT JOIN dbo.tblCompanyInfo AS com ON Emp.CompanyId = com.CompanyId
        //
        //									   LEFT JOIN dbo.tblDesignation AS DSG ON Emp.DesignationId = DSG.DesignationId
        //									    LEFT JOIN dbo.tblDepartment AS DPT ON Emp.DepartmentId = DPT.DepartmentId where  Increment_HistoricalDataId is not null  AND  tblIncrement_HistoricalData.EmployeeId=@EmpInfoId

        public DataTable LoadIncrementInfo_Pro(string Id)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            string query = @"SELECT INC.IncrementId, com.ShortName, GD.GradeCode +ISNULL(' : '+GD.GradeName,'')  as GradeCode,INC.EmployeeId, INC.EmployeeCode,E.EmpName,DSG.Designation,DPT.DepartmentName,CSTP.SalaryStepName AS PreviousStep, 
                             ISTP.SalaryStepName + ISNULL(+' : ' +CAST( ISTP.GrossAmount as nvarchar(max)),'') AS IncrementalStep, INC.EffectiveDate, IncType.Name IncrementType,ForEmp.EmpName as AwEmpName FROM dbo.tblIncrement AS INC
                             LEFT JOIN dbo.tblCompanyInfo AS com ON INC.CompanyId = com.CompanyId
                             LEFT JOIN dbo.tblDesignation AS DSG ON INC.DesignationId = DSG.DesignationId
 


                             LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON INC.EmployeeId = EGI.EmpInfoId
                             LEFT JOIN dbo.tblDepartment AS DPT ON INC.DepartmentId = DPT.DepartmentId
                             LEFT JOIN dbo.tblSalaryGrade AS GD ON INC.SalaryGradeId = GD.SalaryGradeId
                             LEFT JOIN dbo.tblSalaryStep AS CSTP ON INC.CurrentStepId = CSTP.SalaryStepId
                             LEFT JOIN dbo.tblSalaryStep AS ISTP ON INC.IncrementalStepId = ISTP.SalaryStepId
                             LEFT JOIN dbo.tblIncrementInfoMaster AS IncType ON INC.IncrementTypeId = IncType.IncrementInfoMasterId
							LEFT JOIN dbo.tblEmpGeneralInfo AS E ON INC.EmployeeId = E.EmpInfoId
                            LEFT JOIN (SELECT IncrementId,MAX(Version)MaxVer FROM dbo.tblIncrementAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY IncrementId) AS LogApp ON LogApp.IncrementId= INC.IncrementId
								LEFT JOIN dbo.tblIncrementAppLog ON tblIncrementAppLog.IncrementId = INC.IncrementId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblIncrementAppLog.ForEmpInfoId
                                LEFT JOIN dbo.tblIncrementAppLog PreLog ON PreLog.IncrementId=INC.IncrementId AND PreLog.Version = CONVERT(INT,LogApp.MaxVer)-1
								LEFT JOIN dbo.tblEmpGeneralInfo PreEmp ON PreEmp.EmpInfoId=PreLog.ForEmpInfoId  WHERE   INC.IncrementTypeId in (1) and     INC.EmployeeId=@EmpInfoId  
								
								


union all
										SELECT  0 AS IncrementId,  CI.ShortName,  ISNULL(GradeCode, '')  GradeName, tblPromotionUpgrationHistory.EmployeeID EmployeeId,  Emp.EmpMasterCode EmployeeCode, EmpName EmpName, ''    Designation,''   DepartmentName,  null PreviousStep,  tblPromotionUpgrationHistory.SalaryStepName IncrementalStep, FORMAT(EffectDate, 'dd-MMM-yyyy') EffectiveDate    ,'Special Increment'   IncrementType, ''  as AwEmpName  FROM tblPromotionUpgrationHistory  WITH (NOLOCK)
									   left JOIN dbo.tblEmpGeneralInfo AS emp ON emp.EmpInfoId = tblPromotionUpgrationHistory.EmployeeID

									   left JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = emp.CompanyId
									      LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = emp.DesignationId
										     LEFT JOIN dbo.tblDepartment AS DPT ON emp.DepartmentId = DPT.DepartmentId
						 WHERE  TypeOfPromotion in  ('Other') and  EmployeeID =@EmpInfoId 

order by EffectiveDate Desc";

            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);

        }

        public DataTable LoadIncrementInfoMult(string Id)
        {
            
            string query = @"SELECT INC.IncrementId, INC.EmployeeId, INC.EmployeeCode,E.EmpName,DSG.Designation,DPT.DepartmentName,CSTP.SalaryStepName AS PreviousStep, 
                             ISTP.SalaryStepName AS IncrementalStep, INC.EffectiveDate, IncType.Name IncrementType,ForEmp.EmpName as AwEmpName FROM dbo.tblIncrement AS INC
                             LEFT JOIN dbo.tblDesignation AS DSG ON INC.DesignationId = DSG.DesignationId
                             LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON INC.EmployeeId = EGI.EmpInfoId
                             LEFT JOIN dbo.tblDepartment AS DPT ON INC.DepartmentId = DPT.DepartmentId
                             LEFT JOIN dbo.tblSalaryGrade AS GD ON INC.SalaryGradeId = GD.SalaryGradeId
                             LEFT JOIN dbo.tblSalaryStep AS CSTP ON INC.CurrentStepId = CSTP.SalaryStepId
                             LEFT JOIN dbo.tblSalaryStep AS ISTP ON INC.IncrementalStepId = ISTP.SalaryStepId
                             LEFT JOIN dbo.tblIncrementInfoMaster AS IncType ON INC.IncrementTypeId = IncType.IncrementInfoMasterId
							LEFT JOIN dbo.tblEmpGeneralInfo AS E ON INC.EmployeeId = E.EmpInfoId
                            LEFT JOIN (SELECT IncrementId,MAX(Version)MaxVer FROM dbo.tblIncrementAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY IncrementId) AS LogApp ON LogApp.IncrementId= INC.IncrementId
								LEFT JOIN dbo.tblIncrementAppLog ON tblIncrementAppLog.IncrementId = INC.IncrementId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblIncrementAppLog.ForEmpInfoId
                                LEFT JOIN dbo.tblIncrementAppLog PreLog ON PreLog.IncrementId=INC.IncrementId AND PreLog.Version = CONVERT(INT,LogApp.MaxVer)-1
								LEFT JOIN dbo.tblEmpGeneralInfo PreEmp ON PreEmp.EmpInfoId=PreLog.ForEmpInfoId  WHERE INC.EmployeeId" + Id + @"    UNION ALL
 SELECT 0 AS IncrementId, EmployeeId EmployeeId, Emp.EmpMasterCode EmployeeCode, EmpName EmpName,DSG.Designation Designation ,DPT.DepartmentName DepartmentName,null PreviousStep, 
								 --'Yearly increment' Name,
								   IncrementalStep IncrementalStep, EffectiveDate EffectiveDate,    tblIncrement_HistoricalData.Remarks as IncrementType,''  as AwEmpName  FROM tblIncrement_HistoricalData
									  INNER JOIN dbo.tblEmpGeneralInfo AS Emp ON Emp.EmpInfoId = tblIncrement_HistoricalData.EmployeeId
									   LEFT JOIN dbo.tblDesignation AS DSG ON Emp.DesignationId = DSG.DesignationId
									    LEFT JOIN dbo.tblDepartment AS DPT ON Emp.DepartmentId = DPT.DepartmentId where  Increment_HistoricalDataId is not null AND  tblIncrement_HistoricalData.EmployeeId" + Id + @" order by EffectiveDate asc";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);

        }
       //UNION ALL
// SELECT 0 AS IncrementId,  com.ShortName, tblIncrement_HistoricalData.GradeName, EmployeeId EmployeeId, Emp.EmpMasterCode EmployeeCode, EmpName EmpName,DSG.Designation Designation ,DPT.DepartmentName DepartmentName,null PreviousStep, 
//								 --'Yearly increment' Name,
//								   IncrementalStep IncrementalStep, EffectiveDate EffectiveDate,    tblIncrement_HistoricalData.Remarks as IncrementType,''  as AwEmpName  FROM tblIncrement_HistoricalData
//									  INNER JOIN dbo.tblEmpGeneralInfo AS Emp ON Emp.EmpInfoId = tblIncrement_HistoricalData.EmployeeId
//								     LEFT JOIN dbo.tblCompanyInfo AS com ON Emp.CompanyId = com.CompanyId
//
//									   LEFT JOIN dbo.tblDesignation AS DSG ON Emp.DesignationId = DSG.DesignationId
//////									    LEFT JOIN dbo.tblDepartment AS DPT ON Emp.DepartmentId = DPT.DepartmentId where  Increment_HistoricalDataId is not null  AND  tblIncrement_HistoricalData.EmployeeId " + Id 
  
  //+ @"

        public DataTable LoadIncrementInfoMult_Pro(string Id)
        {

            string query = @"SELECT INC.IncrementId, com.ShortName, GD.GradeCode +ISNULL(' : '+GD.GradeName,'')  as GradeCode,INC.EmployeeId, INC.EmployeeCode,E.EmpName,DSG.Designation,DPT.DepartmentName,CSTP.SalaryStepName AS PreviousStep, 
                             ISTP.SalaryStepName + ISNULL(+' : ' +CAST( ISTP.GrossAmount as nvarchar(max)),'') AS IncrementalStep, INC.EffectiveDate, IncType.Name IncrementType,'' as AwEmpName FROM dbo.tblIncrement AS INC
                             LEFT JOIN dbo.tblCompanyInfo AS com ON INC.CompanyId = com.CompanyId
 


                             LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON INC.EmployeeId = EGI.EmpInfoId
                             LEFT JOIN dbo.tblDesignation AS DSG ON INC.DesignationId = EGI.DesignationId

                             LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                             LEFT JOIN dbo.tblSalaryGrade AS GD ON INC.SalaryGradeId = GD.SalaryGradeId
                             LEFT JOIN dbo.tblSalaryStep AS CSTP ON INC.CurrentStepId = CSTP.SalaryStepId
                             LEFT JOIN dbo.tblSalaryStep AS ISTP ON INC.IncrementalStepId = ISTP.SalaryStepId
                             LEFT JOIN dbo.tblIncrementInfoMaster AS IncType ON INC.IncrementTypeId = IncType.IncrementInfoMasterId
							LEFT JOIN dbo.tblEmpGeneralInfo AS E ON INC.EmployeeId = E.EmpInfoId
                          WHERE   INC.IncrementTypeId in (1) and      INC.EmployeeId" + Id + @"  
								
								
 							


union all
										SELECT  0 AS IncrementId,  CI.ShortName,  ISNULL(GradeCode, '')  GradeName, tblPromotionUpgrationHistory.EmployeeID EmployeeId,  Emp.EmpMasterCode EmployeeCode, EmpName EmpName,''    Designation, ''   DepartmentName,  null PreviousStep,  tblPromotionUpgrationHistory.SalaryStepName IncrementalStep, FORMAT(EffectDate, 'dd-MMM-yyyy') EffectiveDate    ,'Special Increment' AS IncrementType, ''  as AwEmpName  FROM tblPromotionUpgrationHistory  WITH (NOLOCK)
									   left JOIN dbo.tblEmpGeneralInfo AS emp ON emp.EmpInfoId = tblPromotionUpgrationHistory.EmployeeID

									   left JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = emp.CompanyId
									      LEFT JOIN dbo.tblDesignation psesg ON psesg.DesignationId = emp.DesignationId
										     LEFT JOIN dbo.tblDepartment AS DPT ON emp.DepartmentId = DPT.DepartmentId
						 WHERE  TypeOfPromotion in  ('Other') and  EmployeeID " + Id + @" 

order by EffectiveDate Desc";

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);

        }
        public DataTable OtherEmpHobbyInfoDAL(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            const string queryStr = @"SELECT  tblMasterHobby.HobbyName,* FROM tblEmpHobby
LEFT JOIN dbo.tblMasterHobby ON tblMasterHobby.MasterHobbyId = tblEmpHobby.MasterHobbyId  WHERE tblEmpHobby.IsActive=1 and tblEmpHobby.EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable EmpOtherTalentsInfoDAL(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            const string queryStr = @"SELECT tblMasterOtherTalents.OtherTalentsName, * FROM dbo.tblEmpOtherTalents
LEFT JOIN dbo.tblMasterOtherTalents ON tblMasterOtherTalents.MasterOtherTalentsId = tblEmpOtherTalents.MasterOtherTalentsId  WHERE tblEmpOtherTalents.IsActive=1 AND tblEmpOtherTalents.EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetEmpMasterCode(long EmpInfoId)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", EmpInfoId));

            return aCommonInternalDal.GetDataByStoreProcedure("usp_GetEmpMasterCode_Contractual", aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable GetEmpMasterCodeForNewEntry(long EmpInfoId)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", EmpInfoId));

            return aCommonInternalDal.GetDataByStoreProcedure("usp_GetEmpMasterCode_Entry", aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetEmpMasterCodeContracttoParmanent(long EmpInfoId)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", EmpInfoId));

            return aCommonInternalDal.GetDataByStoreProcedure("usp_GetEmpMasterCode_ContractualtoParmanent", aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetEmpMasterCodeParmanenttoContractual(long EmpInfoId)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", EmpInfoId));

            return aCommonInternalDal.GetDataByStoreProcedure("usp_GetEmpMasterCode_ParmanenttoContractual", aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetContractEndDate(long EmpInfoId)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", EmpInfoId));

            return aCommonInternalDal.GetDataByStoreProcedure("usp_EmpDetailInformation", aSqlParameterlist, DataBase.HRDB);
        }
    }
}
