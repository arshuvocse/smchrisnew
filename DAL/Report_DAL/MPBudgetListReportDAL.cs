using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DAL.InternalCls;
using  DAL.DataManager;

namespace DAL.COMMON_DAL
{
    public class MPBudgetListReportDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public bool DeleteEmpInfofoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));


            const string queryStr = @"DELETE FROM tblEmpGeneralInfo  WHERE EmpInfoId = @EmpInfoId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable GetCompanyDDL()
        {
            string queryStr = @"SELECT CompanyId as Value,CompanyName, ShortName as TextField FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            //string query = @"SELECT CompanyId as Value, ShortName as TextField FROM tblCompanyInfo";
            return aCommonInternalDal.GetDTforDDL(queryStr, null, DataBase.HRDB);
        }
        public DataTable GetDDLDesignationAll()
        {

            string query = @"SELECT d.DesignationId as Value,d.Designation as TextField FROM dbo.tblDesignation d WHERE d.IsActive=1";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }
        public DataTable GetCompanyDDLPopUp()
        {
            string queryStr = @"SELECT CompanyId as Value,CompanyName, ShortName as TextField FROM tblCompanyInfo";
            //string query = @"SELECT CompanyId as Value, ShortName as TextField FROM tblCompanyInfo";
            return aCommonInternalDal.GetDTforDDL(queryStr, null, DataBase.HRDB);
        }
        public void FinYearByCompDropDown(DropDownList ddl, string id)
        {
            string queryStr = @"SELECT * FROM dbo.tblFinancialYear WHERE CompanyId='" + id + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "FinancialYearDesc", "FinancialYearId", queryStr, DataBase.HRDB);
        }
        public void CompanyDropDown(DropDownList ddl, string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblCompanyInfo " + param + "";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, DataBase.HRDB);
        }
        public bool DeleteInterviewCandidateInfoById(string CandidateID)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CandidateID", CandidateID));

            const string query = @"DELETE FROM tblInterviewCandidateInfo WHERE CandidateID = @CandidateID";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
        }
        public DataTable GetDTEmpTrainingByEmpId(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@mid", mid));
            string query = @"SELECT CONVERT(VARCHAR(10), t.TrFromDate, 105) AS TrFromDate, CONVERT(VARCHAR(10), t.TrToDate, 105) AS TrToDate, t.EmpTrainingId,
       t.EmpInfoId,
       t.TrainingName,
       t.TrainingType,
	   tt.TrainingType AS TrainingTypeName,
       t.TrainingDescription,
       t.TrainingInstitution,
	   ti.Description AS TrainingInstitutionName,
       t.TrainingPlace,
       t.TrainingCountry,
	   c.Title AS TrainingCountryName,
       t.TrainingAchievment,
       t.TrFromDate,
       t.TrToDate,
       t.TrainingDays,
       t.TrRemarks,
       t.IsActive FROM dbo.tblEmpTraining t
	   LEFT JOIN dbo.tblTrainingType tt ON tt.TrainingTypeID = t.TrainingType
	   LEFT JOIN dbo.tblEmpTrainingInstitution ti ON ti.InstitutionID=t.TrainingInstitution
	   LEFT JOIN dbo.tblCountry c ON c.CountryID=t.TrainingCountry
WHERE t.IsActive=1 AND t.EmpInfoId=@mid";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDTEmpOtherTalentsByEmpId(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@mid", mid));
            string query = @"SELECT ex.MasterOtherTalentsId AS Value FROM dbo.tblEmpOtherTalents ex
WHERE ex.IsActive=1 AND ex.EmpInfoId=@mid";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDTEmpHobbyByEmpId(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@mid", mid));
            string query = @"SELECT ex.MasterHobbyId AS Value FROM dbo.tblEmpHobby ex
WHERE ex.IsActive=1 AND ex.EmpInfoId=@mid";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDTEmpEducationByEmpId(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@mid", mid));
            string query = @"SELECT e.EmpEducationId,
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
	   'No Seed Data' AS FieldOfSpecialization,
       e.PassingYear,
       e.Result,
       e.CgpaOrTotalMarks,
       e.EduIsLastLevel,
e.IsProfessionalEdu,
       e.IsActive FROM dbo.tblEmpEducation e 
	   LEFT JOIN dbo.tblEducationName en ON en.EducationNameID = e.EducationNameId
	   LEFT JOIN dbo.tblBoardUniversity bu ON bu.BoardUniversityID = e.BoardUniversityId
	   LEFT JOIN dbo.tblEducationSubjectGroup sg ON sg.EducationSubjectGroupID=e.SubjectGroupId
	   LEFT JOIN dbo.tblBoardUniversity ei ON ei.BoardUniversityID=e.EducationalInstituteId
	   WHERE e.IsActive=1 AND e.EmpInfoId=@mid";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetDTEmpAchievementsByEmpId(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@mid", mid));
            string query = @"SELECT ex.MasterAchievementsId AS Value FROM dbo.tblEmpAchievements ex
WHERE ex.IsActive=1 AND ex.EmpInfoId=@mid";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDTEmpExtraCurriculamByEmpId(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@mid", mid));
            string query = @"SELECT ex.MasterExtraCurriculamId AS Value FROM dbo.tblEmpExtraCurriculam ex
WHERE ex.IsActive=1 AND ex.EmpInfoId=@mid";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDTEmpNomineeByEmpId(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@mid", mid));
            string query = @"SELECT CONVERT(VARCHAR(10), n.NomineeDOB, 105) AS NomineeDOB, CONVERT(VARCHAR(10), n.DateOfNomination, 105) AS DateOfNomination, n.EmpNomineeId,
       n.EmpInfoId,
       n.NominationPurpose,
	   np.Description AS NominationPurposeName,
       n.NomineeName,
       n.NomineeOccupation,
	   o.Description AS NomineeOccupationName,
       n.DateOfNomination,
       n.NominationPercentage,
       n.NomineeDOB,
       n.NomineeRelation,
	   r.Description AS NomineeRelationName,
       n.NomineeTelephone,
       n.NomineeAddress,
       n.IsActive FROM dbo.tblEmpNominee n
	   LEFT JOIN dbo.tblNominationPurpose np ON np.NPID=n.NominationPurpose
	   LEFT JOIN dbo.tblOccupation o ON o.OccupationID=n.NomineeOccupation
	   LEFT JOIN dbo.tblRelation r ON r.RelationID=n.NomineeRelation
WHERE n.IsActive=1 AND n.EmpInfoId=@mid";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDTEmpReferenceByEmpId(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@mid", mid));
            string query = @"SELECT r.EmpReferenceId,
       r.EmpInfoId,
       r.ReferenceName,
       r.RefOccupation,
	   oc.Description AS RefOccupationName,
       r.RefAddress,
       r.RefMobile,
       r.IsActive FROM dbo.tblEmpReference r
	   LEFT JOIN dbo.tblOccupation oc ON oc.OccupationID=r.RefOccupation
WHERE r.IsActive=1 AND r.EmpInfoId=@mid";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDTEmpExperienceByEmpId(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@mid", mid));
            string query = @"SELECT CONVERT(VARCHAR(10), e.ExpFromDate, 105) AS ExpFromDate, CONVERT(VARCHAR(10), e.ExpToDate, 105) AS ExpToDate,  
       e.EmpExperienceId,
       e.EmpInfoId,
       e.ExpCompany,
       e.ExpContactPerson,
       e.ExpAddress,
       e.ExpNatureofBusiness,
       e.ExpJobType,
       e.ExpLeavingSalary,
       e.ExpFromDate,
       e.ExpToDate,
       e.ExpLastJob,
       e.ExpDesignation,
       e.ExpJobDescription,
       e.ExpTelNo,
       e.ExpRemarks,
       e.IsActive FROM dbo.tblEmpExperience e
WHERE e.IsActive=1 AND e.EmpInfoId=@mid";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetAllProject(string CompanyId)
        {

            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", CompanyId));
            string query = @"SELECT p.ProjectId AS Value,p.ProjectName AS TextField FROM dbo.tblProjectSetup p WHERE p.IsActive=1";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDDLSalFromProject(string CompanyId)
        {

            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", CompanyId));
            string query = @"SELECT p.ProjectId AS Value,p.ProjectName AS TextField FROM dbo.tblProjectSetup p WHERE p.IsActive=1";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetAllCompany()
        {
            string query = @"SELECT CompanyId as Value, ShortName as TextField FROM tblCompanyInfo";
            return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
        }
        public DataTable GetInterviewActivity()
        {
            string query = @"SELECT ia.InterviewActivityId AS Value,
       ia.InterviewActivity AS TextField FROM dbo.tblInterviewActivity ia WHERE ia.IsActive=1";
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }
        public DataTable GetUserType()
        {
            string query = @"SELECT UserTypeId as Value,UserType as TextField FROM dbo.tblUserType WHERE IsActive=1";
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetMenuTypeDDL()
        {
            string query = @"SELECT MenuTypeId as Value, MenuTypeName as TextField FROM tblMenuTypeSetup WHERE IsActive=1";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }
        public DataTable GetUserDDL()
        {
            string query = @"SELECT UserId as Value, UserName as TextField FROM dbo.tblUser WHERE IsActive=1";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }
        public DataTable GetUserDDLNew2()
        {
            string query = @"SELECT UserId as Value, UserName as TextField FROM dbo.tblUser WHERE IsActive=1 AND UserId NOT IN (SELECT UserId FROM dbo.tblMenuGroupPermission
INNER JOIN dbo.tblMenuGroupPermissionDtl ON tblMenuGroupPermissionDtl.MenuGroupPermissionId = tblMenuGroupPermission.MenuGroupPermissionId)";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }
        public DataTable GetUserDDLNew()
        {
            string query = @"SELECT UserId as Value, UserName as TextField FROM dbo.tblUser WHERE IsActive=1 AND UserId NOT IN (SELECT UserId FROM dbo.tblMenuApprovalGroupPermission)";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }
        public DataTable GetDDLDepartmentByCompanyId(int CompanyId)
        {
            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@CompanyId", CompanyId) };
            string query = @"SELECT dept.DepartmentId as Value,dept.ShortName,dept.DepartmentName as TextField FROM dbo.tblDepartment dept WHERE dept.CompanyId=@CompanyId AND DepartmentId IN (SELECT DepartmentId FROM dbo.tblUserDepartmentPermission WHERE UserId='"+HttpContext.Current.Session["UserId"].ToString()+"')";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDDLDepartmentByCompanyUserId(int CompanyId)
        {
            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@CompanyId", CompanyId) };
            string query = @"SELECT dept.DepartmentId as Value,dept.ShortName,dept.DepartmentName as TextField FROM dbo.tblDepartment dept WHERE dept.CompanyId=@CompanyId AND dept.DepartmentId IN (SELECT DepartmentId FROM dbo.tblUserDepartmentPermission WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDepartmentByCompanyId(int CompanyId)
        {
            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@CompanyId", CompanyId) };
            string query = @"SELECT dept.DepartmentId as Value,dept.ShortName,dept.DepartmentName as TextField FROM dbo.tblDepartment dept WHERE dept.CompanyId=@CompanyId";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDepartmentBySelectedCompany(string selectedCompanys)
        {
            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@selectedCompanys", selectedCompanys) };
            string query = @"SELECT dept.DepartmentId as Value,dept.ShortName+'('+c.ShortName+')' ShortName,dept.DepartmentName+'('+c.ShortName+')' as TextField 
FROM dbo.tblDepartment dept 
INNER JOIN dbo.tblCompanyInfo c ON c.CompanyId = dept.CompanyId
WHERE dept.CompanyId IN ("+selectedCompanys+")";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetDDLFinYearByCompanyId(int CompanyId)
        {
            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@CompanyId", CompanyId) };
            string query = @"SELECT fy.FinancialYearId as Value, fy.FinancialYearDesc as TextField FROM dbo.tblFinancialYear fy
                                WHERE  fy.Status='Active' and fy.CompanyId=@CompanyId";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDDLFinYearByCompanyIdAll(int CompanyId)
        {
            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@CompanyId", CompanyId) };
            string query = @"SELECT fy.FinancialYearId as Value, fy.FinancialYearDesc as TextField FROM dbo.tblFinancialYear fy
                                WHERE   fy.CompanyId=@CompanyId";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDDLDesignationByCompanyId(int CompanyId)
        {
            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@CompanyId", CompanyId) };
            string query = @"SELECT d.DesignationId as Value,d.Designation as TextField FROM dbo.tblDesignation d WHERE d.IsActive=1";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDDLDesignationByGrade(int SalaryGradeId)
        {
            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@SalaryGradeId", SalaryGradeId) };
            string query = @"SELECT d.DesignationId as Value,d.Designation as TextField FROM dbo.tblDesignation d WHERE d.IsActive=1 AND d.SalaryGradeId=@SalaryGradeId";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetQuarterByCompanyId(int CompanyId)
        {
            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@CompanyId", CompanyId) };
            string query = @"SELECT q.QuarterId as Value,q.QuarterName as TextField FROM dbo.tblQuarterInfo q WHERE q.IsActive=1";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetEmpInfo(string empInfoId)
        {
            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@EmpInfoId", empInfoId) };
            string query = @"SELECT * FROM dbo.tblEmpGeneralInfo WHERE EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDDLStepByCompanyId(int CompanyId)
        {
            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@CompanyId", CompanyId) };
            string query = @"SELECT s.SalaryStepId AS Value,s.SalaryStepName as TextField FROM dbo.tblSalaryStep s WHERE s.IsActive=1";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDDLStepByGrade(int SalaryGradeId)
        {
            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@SalaryGradeId", SalaryGradeId) };
            string query = @"SELECT DISTINCT s.SalaryStepId AS Value,s.SalaryStepName + '( '+CAST(COUNT(e.SalaryStepId ) AS VARCHAR(10))+' )' as TextField
FROM dbo.tblSalaryStep s 
LEFT JOIN dbo.tblEmpGeneralInfo e ON e.SalaryStepId=s.SalaryStepId
WHERE s.IsActive=1 AND s.SalaryGradeId=@SalaryGradeId
GROUP BY s.SalaryStepId,
         s.SalaryStepName HAVING COUNT(e.SalaryStepId )>0 ";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetDDLGradeByDepartment(int DepartmentId)
        {
            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@DepartmentId", DepartmentId) };
            string query = @"SELECT DISTINCT g.SalaryGradeId as Value,(g.GradeCode+' : '+g.GradeName) AS TextField FROM dbo.tblSalaryGrade g

WHERE e.DepartmentId=@DepartmentId";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDDLGradeByDepartmentAndCategory(int DepartmentId, int EmpCategoryId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@DepartmentId", DepartmentId));
            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", EmpCategoryId));
            
            string query = @"SELECT DISTINCT g.SalaryGradeId as Value,(g.GradeCode+' : '+g.GradeName) AS TextField FROM dbo.tblSalaryGrade g
INNER JOIN dbo.tblEmpGeneralInfo e ON e.SalaryGradeId=g.SalaryGradeId
WHERE e.DepartmentId=@DepartmentId AND e.EmpCategoryId=@EmpCategoryId";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetDDLGradeByCategory( int EmpCategoryId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
          
            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", EmpCategoryId));

            string query = @"SELECT DISTINCT g.SalaryGradeId as Value,(g.GradeCode+' : '+g.GradeName) AS TextField FROM dbo.tblSalaryGrade g
INNER JOIN dbo.tblEmpGeneralInfo e ON e.SalaryGradeId=g.SalaryGradeId
WHERE e.DepartmentId=@DepartmentId AND e.EmpCategoryId=@EmpCategoryId";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }
//        public DataTable GetSalaryGradeByStepId(int SalaryStepId)
//        {
//            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@SalaryStepId", SalaryStepId) };
//            string query = @"SELECT sg.SalaryGradeId,sg.GradeName,ss.SalaryStepId FROM dbo.tblSalaryGrade sg
//                            INNER JOIN  dbo.tblSalaryStep ss ON	ss.SalaryGradeId = sg.SalaryGradeId
//                            WHERE ss.SalaryStepId=@SalaryStepId";
//            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
//        }

        public DataTable GetMasterAchievements()
        {
            string query = @"SELECT MasterAchievementsId AS Value, AchievementsName AS TextField FROM dbo.tblMasterAchievements a WHERE a.IsActive=1";
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetMasterExtraCurriculam()
        {
            string query = @"SELECT a.MasterExtraCurriculamId AS Value, a.ExtraCurriculamName AS TextField FROM dbo.tblMasterExtraCurriculam a WHERE a.IsActive=1";
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetMasterOtherTalents()
        {
            string query = @"SELECT a.MasterOtherTalentsId AS Value, a.OtherTalentsName AS TextField FROM dbo.tblMasterOtherTalents a WHERE a.IsActive=1";
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetMasterHobby()
        {
            string query = @"SELECT a.MasterHobbyId AS Value, a.HobbyName AS TextField FROM dbo.tblMasterHobby a WHERE a.IsActive=1";
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable GetDDLEmpType()
        {
            //var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@a", 0) };
            string query = @"SELECT a.EmpTypeId AS Value, a.EmpType AS TextField FROM dbo.tblEmployeeType a WHERE a.IsActive=1";
            return aCommonInternalDal.GetDTforDDL(query,null, DataBase.HRDB);
        }
        public DataTable GetEmpType()
        {
            //var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@a", 0) };
            string query = @"SELECT a.EmpTypeId AS Value, a.EmpType AS TextField FROM dbo.tblEmployeeType a WHERE a.IsActive=1";
            return aCommonInternalDal.DataContainerDataTable(query, null, DataBase.HRDB);
        }

        public void LoadDesignaation(DropDownList ddl)
        {
            string query = @"SELECT * FROM dbo.tblDesignation";
            aCommonInternalDal.LoadDropDownValue(ddl, "Designation", "DesignationId",query,DataBase.HRDB);
        }

        public DataTable GetDesignationChangeInfo(int DesignationId)
        {
            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@DesignationId", DesignationId) };
            string query = @"DECLARE @SalaryGradeId INT
                            DECLARE @GradeName NVARCHAR(50)
                            DECLARE @DesigExistingEmployee NVARCHAR(50)
                            DECLARE @DesigExistingTotalSalary NVARCHAR(50)
                            SELECT TOP 1 @SalaryGradeId=g.SalaryGradeId, @GradeName=g.GradeName FROM dbo.tblSalaryGrade g WHERE g.IsActive=1 AND g.DesignationId=@DesignationId
                            SELECT @DesigExistingTotalSalary=SUM(Amount) FROM dbo.tblSalaryInformation s
                            INNER JOIN dbo.tblEmpGeneralInfo e ON e.EmpInfoId = s.EmpInfoId WHERE e.IsActive=1 AND e.DesignationId=@DesignationId
                            SELECT @DesigExistingEmployee=COUNT(EmpInfoId) FROM dbo.tblEmpGeneralInfo WHERE IsActive=1 AND DesignationId=@DesignationId
                            SELECT @SalaryGradeId AS SalaryGradeId,@GradeName AS GradeName,
                            @DesigExistingEmployee AS DesigExistingEmployee,@DesigExistingTotalSalary AS DesigExistingTotalSalary";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetStepChangeInfo(int SalaryStepId)
        {
            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@SalaryStepId", SalaryStepId) };
            string query = @"DECLARE @StepExistingEmployee NVARCHAR(50)
DECLARE @StepExistingTotalSalary NVARCHAR(50)
DECLARE @StepSalary NVARCHAR(50)
SELECT @StepExistingTotalSalary=SUM(Amount) FROM dbo.tblSalaryInformation s
INNER JOIN dbo.tblEmpGeneralInfo e ON e.EmpInfoId = s.EmpInfoId WHERE e.IsActive=1 AND e.SalaryStepId=@SalaryStepId
SELECT @StepExistingEmployee=COUNT(EmpInfoId) FROM dbo.tblEmpGeneralInfo WHERE IsActive=1 AND SalaryStepId=@SalaryStepId
SELECT @StepExistingEmployee AS StepExistingEmployee,@StepExistingTotalSalary AS StepExistingTotalSalary,'54500' AS StepSalary";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetGradeExChangeInfo(int SalaryGradeId)
        {
            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@SalaryGradeId", SalaryGradeId) };
            string query = @"DECLARE @ExGradeTotalEmp NVARCHAR(50)
DECLARE @ExGradeTotalSal NVARCHAR(50)
DECLARE @ExGradeMaxSal NVARCHAR(50)
DECLARE @ExGradeMinSal NVARCHAR(50)
SELECT @ExGradeTotalEmp= COUNT(EmpInfoId) FROM dbo.tblEmpGeneralInfo WHERE SalaryGradeId=@SalaryGradeId
SELECT @ExGradeTotalSal= SUM(s.Amount) FROM dbo.tblSalaryInformation s
INNER JOIN dbo.tblEmpGeneralInfo e ON e.EmpInfoId=s.EmpInfoId
 WHERE e.SalaryGradeId=@SalaryGradeId
 SELECT @ExGradeMaxSal= MAX(s.Amount) FROM dbo.tblSalaryInformation s
INNER JOIN dbo.tblEmpGeneralInfo e ON e.EmpInfoId=s.EmpInfoId
 WHERE e.SalaryGradeId=@SalaryGradeId
 GROUP BY s.EmpInfoId
  SELECT @ExGradeMinSal= MIN(s.Amount) FROM dbo.tblSalaryInformation s
INNER JOIN dbo.tblEmpGeneralInfo e ON e.EmpInfoId=s.EmpInfoId
 WHERE e.SalaryGradeId=@SalaryGradeId
 GROUP BY s.EmpInfoId
SELECT @ExGradeTotalEmp AS ExGradeTotalEmp, @ExGradeTotalSal AS ExGradeTotalSal,@ExGradeMaxSal AS ExGradeMaxSal,@ExGradeMinSal AS ExGradeMinSal";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetGradeExChangeInfoNew(int SalaryGradeId)
        {
            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@SalaryGradeId", SalaryGradeId) };
            string query = @"DECLARE @TotalEmp INT=0
SELECT @TotalEmp=COUNT(*) FROM dbo.tblEmpGeneralInfo WHERE SalaryGradeId=@SalaryGradeId
SELECT @TotalEmp AS ExGradeTotalEmp,MAX(GrossAmount)ExGradeMaxSal,MIN(GrossAmount)ExGradeMinSal,SUM(GrossAmount)ExGradeTotalSal FROM dbo.tblSalaryStep WHERE SalaryGradeId=@SalaryGradeId";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetStepExChangeInfo(int SalaryStepId)
        {
            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@SalaryStepId", SalaryStepId) };
            string query = @"DECLARE @ExStepTotalEmp NVARCHAR(50)
SELECT @ExStepTotalEmp= COUNT(EmpInfoId) FROM dbo.tblEmpGeneralInfo WHERE SalaryStepId=@SalaryStepId
SELECT @ExStepTotalEmp AS ExStepTotalEmp";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetDDLGradeNew(int CompanyId)
        {
            var aSqlParameterlist = new List<SqlParameter>() { new SqlParameter("@CompanyId", CompanyId) };

            string query = @"SELECT DISTINCT g.SalaryGradeId as Value,(g.GradeCode+' : '+g.GradeName) AS TextField FROM dbo.tblSalaryGrade g
WHERE g.SalaryGradeId NOT IN ( SELECT e.SalaryGradeId FROM  dbo.tblEmpGeneralInfo e) ";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetDDLGradeNewByCategory(int CompanyId, int EmpCategoryId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", EmpCategoryId));

            string query = @"SELECT DISTINCT g.SalaryGradeId as Value,(g.GradeCode+' : '+g.GradeName) AS TextField FROM dbo.tblSalaryGrade g
WHERE g.SalaryGradeId NOT IN ( SELECT e.SalaryGradeId FROM  dbo.tblEmpGeneralInfo e) AND g.EmpCategoryId=@EmpCategoryId";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetEmpCategoryDDL()
        {
            string query = @"SELECT c.EmpCategoryId AS Value,c.EmpCategoryName AS TextField FROM dbo.tblEmpCategory c WHERE c.IsActive=1";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetDDLNominationPurpose()
        {
            string query = @"SELECT np.NPID AS Value, np.Description AS TextField  FROM dbo.tblNominationPurpose np";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetDDLOccupation()
        {
            string query = @"SELECT o.OccupationID AS Value, o.Description AS TextField FROM dbo.tblOccupation o ";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetDDLRelation()
        {
            string query = @"SELECT r.RelationID AS Value, r.Description AS TextField  FROM dbo.tblRelation r";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetDDLTrainingType()
        {
            string query = @"SELECT tt.TrainingTypeID AS Value, tt.Description AS TextField  FROM dbo.tblEmpInfoTrainingType tt";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetDDLTrainingInstitution()
        {
            string query = @"SELECT ti.InstitutionID AS Value, ti.Description AS TextField FROM dbo.tblEmpTrainingInstitution ti";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetDDLCountry()
        {
            string query = @"SELECT c.CountryID AS Value, c.Title AS TextField FROM dbo.tblCountry c";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetDDLEducationName()
        {
            string query = @"SELECT en.EducationNameID AS Value, en.Description AS TextField  FROM dbo.tblEducationName en";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetDDLBoardUniversity()
        {
            string query = @"SELECT bu.BoardUniversityID AS Value, bu.Description AS TextField  FROM dbo.tblBoardUniversity bu";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetDDLSubjectGroup()
        {////TODO
            string query = @"SELECT sg.EducationSubjectGroupID AS Value, sg.Description AS TextField FROM tblEducationSubjectGroup sg";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetDDLEducationalInstitute()
        {
            ////TODO
            string query = @"SELECT bu.InstitutionID AS Value, bu.Description AS TextField  FROM dbo.tblEducationalInstitution bu";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetDDLSpecialization()
        {
            ////TODO
            string query = @"SELECT bu.SpecializationID AS Value, bu.Description AS TextField  FROM dbo.tblSpecialization bu";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }
        public DataTable GetDDLComDivision(string cid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@cid", cid));
            string query = @"SELECT d.DivisionId AS Value, d.DivisionName AS TextField FROM dbo.tblDivision d WHERE d.CompanyId=@cid";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDTEmpChildrenByEmpId(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@mid", mid));
            string query = @"SELECT CONVERT(VARCHAR(10), c.ChildrenDOB, 105) AS ChildrenDOB,  
       c.EmpChildrenId,
       c.EmpInfoId,
       c.ChildrenName,
       c.ChildrenGender,
       c.ChildrenOccupation,
	   occ.Description ChildrenOccupationName,
       c.ChildrenDOB,
       c.ChildrenMaritalStatus,
       c.IsActive FROM dbo.tblEmpChildren c
	   INNER JOIN dbo.tblOccupation occ ON occ.OccupationID=c.ChildrenOccupation
	    WHERE c.IsActive=1 and c.EmpInfoId=@mid";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDDLComWind(string cid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@cid", cid));
            string query = @"SELECT w.DivisionWId AS Value, w.DivisionWingName AS TextField FROM dbo.tblDivisionWing w WHERE (w.Invisible IS NULL OR w.Invisible=0) AND  w.CompanyId=@cid";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDDLComDepartment(string cid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@cid", cid));
            string query = @"SELECT d.DepartmentId AS Value, d.DepartmentName AS TextField FROM dbo.tblDepartment d WHERE  (d.Invisible IS NULL OR d.Invisible=0) and d.CompanyId=@cid";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDDLComSection(string cid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@cid", cid));
            string query = @"SELECT s.SectionId AS Value, s.SectionName AS TextField FROM dbo.tblSection s WHERE (s.Invisible IS NULL OR s.Invisible=0) and s.CompanyId=@cid";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDDLComSubSection(string cid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@cid", cid));
            string query = @"SELECT s.SubSectionId AS Value, s.SubSectionName AS TextField FROM dbo.tblSubSection s WHERE s.CompanyId=@cid";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDDLComCategory()
        {

            string query = @"SELECT s.EmpCategoryId AS Value, s.EmpCategoryName AS TextField FROM dbo.tblEmpCategory s WHERE s.IsActive=1";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }
        public DataTable GetDDLDesignationType()
        {

            string query = @"SELECT dt.DesignationTypeId AS Value, dt.DesigTypeName AS TextField FROM dbo.tblDesignationType dt WHERE dt.IsActive=1";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }
        public DataTable GetDDLJobLocation()
        {

            string query = @"SELECT dt.JobLocationID AS Value, dt.Location AS TextField FROM dbo.tblJobLocation dt WHERE dt.IsActive=1";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }
        public DataTable GetDDLSalaryLocation()
        {

            string query = @"SELECT dt.SalaryLoationId AS Value, dt.SalaryLocation AS TextField FROM dbo.tblSalaryLocation dt WHERE dt.IsActive=1";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public DataTable GetDDLAddressDivision()
        {
            string query = @"SELECT d.AddressDivisionId AS Value, d.Title AS TextField FROM dbo.tblAddressDivision d";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }
        public DataTable GetDDLSalaryStep(string SalaryGradeId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", SalaryGradeId));
            string query = @"SELECT s.SalaryStepId AS Value, s.SalaryStepName AS TextField FROM dbo.tblSalaryStep s WHERE s.IsActive=1 AND s.SalaryGradeId=@SalaryGradeId";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable GetDDLJobLocation(string SalaryLocID)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", SalaryLocID));
            string query = @"SELECT Jloc.JobLocationID AS Value, Jloc.SalaryLoationId, Jloc.Location AS TextField FROM dbo.tblJobLocation Jloc WHERE Jloc.SalaryLoationId=@SalaryLoationId";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable GetDDLSalaryGrade(string EmpCategory)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpCategory", EmpCategory));
            string query = @"SELECT DISTINCT g.SalaryGradeId as Value,(g.GradeCode+' : '+g.GradeName) AS TextField FROM dbo.tblSalaryGrade g
WHERE g.EmpCategoryId=@EmpCategory AND g.IsActive=1";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetDDLDistrict(string Division)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@Division", Division));
            string query = @"SELECT d.DistrictID AS Value, d.Titel AS TextField FROM dbo.tblDistrict d WHERE d.DivisionID=@Division";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetDDLThana(string District)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@District", District));
            string query = @"SELECT t.ThanaID AS Value, t.Title AS TextField FROM dbo.tblThana t WHERE t.DistrictID=@District";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetProjectNameDDL(int CompanyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", CompanyId));
            string query = @"SELECT p.ProjectId AS Value, p.ProjectName AS TextField FROM dbo.tblProjectSetup p WHERE p.IsActive=1 AND p.CompanyId=@CompanyId";
            return aCommonInternalDal.GetDTforDDL(query, aSqlParameterlist, DataBase.HRDB);
        }



        public DataTable GetManpowerBudgetInfo(string company, string dept, string finyear)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@company", company));
            aSqlParameterlist.Add(new SqlParameter("@dept", dept));
            aSqlParameterlist.Add(new SqlParameter("@finyear", finyear));

            string queryStr = @"SELECT * FROM dbo.tblMPBudgetMaster bm
								LEFT JOIN dbo.tblMPBudgetDetails bd ON bd.MPBudgetMasterId = bm.MPBudgetMasterId
								LEFT JOIN dbo.tblCompanyInfo c ON c.CompanyId = bm.CompanyId
                                LEFT JOIN dbo.tblDepartment d ON d.DepartmentId=bm.DepartmentId
                                LEFT JOIN dbo.tblFinancialYear fy ON fy.FinancialYearId = bm.FinancialYearId WHERE
								bm.IsActive=1 
                                AND bm.CompanyId=@company
                                AND bm.DepartmentId=COALESCE(NULLIF(@dept,'-1'),bm.DepartmentId)
                                AND bm.FinancialYearId=COALESCE(NULLIF(@finyear,'-1'),bm.FinancialYearId)";

            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        //public bool DeleteManpowerInfoById(string MPBudgetMasterId)
        //{
        //    var aSqlParameterlist = new List<SqlParameter>();
        //    aSqlParameterlist.Add(new SqlParameter("@MPBudgetMasterId", MPBudgetMasterId));

        //    const string query = @"DELETE FROM tblMPBudgetMaster WHERE MPBudgetMasterId = @MPBudgetMasterId";
        //    return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
        //}



        public bool DeleteManpowerInfoById(int id, string user)
        {
            try
            {
                bool result = false;
                List<SqlParameter> aperam = new List<SqlParameter>();

                aperam.Add(new SqlParameter("@MPBudgetMasterId", id));
                aperam.Add(new SqlParameter("@DeleteBy", user));
                aperam.Add(new SqlParameter("@DeleteDate", System.DateTime.Now));
                string query = @"update tblMPBudgetMaster set  IsActive=0 ,IsDelete=1 , DeleteBy = @DeleteBy ,DeleteDate=@DeleteDate where MPBudgetMasterId = @MPBudgetMasterId ";


                result = aCommonInternalDal.DeleteDataByDeleteCommand(query, aperam, DataBase.HRDB);
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }




        public DataTable GetManpowerBudgetListInfo(string company, string dept, string finyear, string param)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@company", company));
            aSqlParameterlist.Add(new SqlParameter("@dept", dept));
            aSqlParameterlist.Add(new SqlParameter("@finyear", finyear));

            string queryStr = @"SELECT  * FROM dbo.tblMPBudgetMaster bm
								
								LEFT JOIN dbo.tblCompanyInfo c ON c.CompanyId = bm.CompanyId
                                LEFT JOIN dbo.tblDepartment d ON d.DepartmentId=bm.DepartmentId
                                LEFT JOIN dbo.tblFinancialYear fy ON fy.FinancialYearId = bm.FinancialYearId WHERE
                                bm.CompanyId=@company
                                AND bm.DepartmentId=COALESCE(NULLIF(@dept,'-1'),bm.DepartmentId)
                                AND bm.FinancialYearId=COALESCE(NULLIF(@finyear,'-1'),bm.FinancialYearId)  " + param + "";

            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
//        public DataTable GetManpowerBudgetInfoApp(string actionstatus, string param)
//        {
            

//            string queryStr = @"SELECT * FROM dbo.tblMPBudgetMaster bm
//								LEFT JOIN dbo.tblMPBudgetDetails bd ON bd.MPBudgetMasterId = bm.MPBudgetMasterId
//								LEFT JOIN dbo.tblCompanyInfo c ON c.CompanyId = bm.CompanyId
//                                LEFT JOIN dbo.tblDepartment d ON d.DepartmentId=bm.DepartmentId
//                                LEFT JOIN dbo.tblFinancialYear fy ON fy.FinancialYearId = bm.FinancialYearId WHERE
//								bm.IsActive=1  AND   bd.ActionStatus IS NULL " + param + "";

//           // AND bd.ActionStatus='"+actionstatus+"'

//            return aCommonInternalDal.DataContainerDataTable(queryStr,  DataBase.HRDB);
//        }
        public bool DeleteManpowerDeleteInfoById(string MPBudgetMasterId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MPBudgetMasterId", MPBudgetMasterId));

            const string query = @"DELETE FROM tblMPBudgetDetails WHERE MPBudgetMasterId = @MPBudgetMasterId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
        }
        public void GetCompanyListIntoDropdown(DropDownList ddl)
        {
            string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE  CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
        }

        public DataTable GetManpowerBudgetInfoApp(string actionstatus, string param)
        {


            string queryStr = @"SELECT * FROM dbo.tblMPBudgetMaster bm
								
								LEFT JOIN dbo.tblCompanyInfo c ON c.CompanyId = bm.CompanyId
                                LEFT JOIN dbo.tblDepartment d ON d.DepartmentId=bm.DepartmentId
                                LEFT JOIN dbo.tblFinancialYear fy ON fy.FinancialYearId = bm.FinancialYearId WHERE ActionStatus='" + actionstatus + "' AND  bm.IsActive=1  " + param + "";

            // AND bd.ActionStatus='"+actionstatus+"'

            return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public DataTable GetNationality()
        {
            string query = @"SELECT n.Nationality AS Value,n.Description AS TextField FROM dbo.tblNationality n";
            return aCommonInternalDal.GetDTforDDL(query, null,DataBase.HRDB);
        }

        public DataTable GetCGPATotalMarks()
        {
            string query = @"SELECT n.EducationResultID AS Value,n.Description AS TextField FROM dbo.tblEducationResult n";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }

        public bool DeleteEmployeeById(int EmpInfoId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", EmpInfoId));

            const string query = @"DELETE FROM	dbo.tblEmpGeneralInfo WHERE EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
        }

        public DataTable GetRetirementSetting()
        {
            string queryStr = @"select RetirementLength from [tblRetirementAgeSetting] where IsActive=1";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetDepartmentRelaton(string id, string param)
        {
            string queryStr = @"SELECT tblDivisionWing.Invisible,* FROM tblDepartment
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE tblDepartment.IsActive = 'True' AND DepartmentId = '" + id + "' " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetSectionRelaton(string id, string param)
        {
            string queryStr = @"SELECT tblDepartment.Invisible,* FROM dbo.tblSection
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE dbo.tblSection.IsActive = 'True' AND SectionId = '" + id + "' " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetSubSectionRelaton(string id, string param)
        {
            string queryStr = @"SELECT dbo.tblSection.Invisible,* FROM dbo.tblSubSection
LEFT JOIN dbo.tblSection ON tblSection.SectionId = tblSubSection.SectionId
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE dbo.tblSubSection.IsActive = 'True' AND SubSectionId = '" + id + "' " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public void GetDivisionWingListAll(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = "SELECT DivisionWId,DivisionWingName FROM tblDivisionWing WHERE IsActive = 'True' AND DivisionId = @DivisionId ";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionWingName", "DivisionWId", queryStr, aSqlParameterlist, "HRDB");
        }
        public void GetSubSectionListAll(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = @"SELECT * FROM dbo.tblSubSection
LEFT JOIN dbo.tblSection ON tblSection.SectionId = tblSubSection.SectionId
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE tblSubSection.IsActive = 'True' AND tblDivision.DivisionId = @DivisionId ";
            aCommonInternalDal.LoadDropDownValue(ddl, "SubSectionName", "SubSectionId", queryStr, aSqlParameterlist, "HRDB");
        }
        public void GetDepartmentByDivListAll(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = @"SELECT DepartmentId,DepartmentName FROM tblDepartment
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE tblDepartment.IsActive = 'True' AND tblDivision.DivisionId = @DivisionId ";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", queryStr, aSqlParameterlist, "HRDB");
        }
        public void GetDepartmentByDivList(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = @"SELECT DepartmentId,DepartmentName FROM tblDepartment
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE tblDepartment.IsActive = 'True' AND tblDivision.DivisionId = @DivisionId AND (tblDepartment.Invisible IS NULL OR tblDepartment.Invisible='False')";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", queryStr, aSqlParameterlist, "HRDB");
        }
        public void GetSectionByDivList(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = @"SELECT * FROM dbo.tblSection
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE tblSection.IsActive = 'True' AND tblDivision.DivisionId = @DivisionId AND (tblSection.Invisible IS NULL OR tblSection.Invisible='False')";
            aCommonInternalDal.LoadDropDownValue(ddl, "SectionName", "SectionId", queryStr, aSqlParameterlist, "HRDB");
        }
        public void GetSectionByDivListAll(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = @"SELECT * FROM dbo.tblSection
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE tblSection.IsActive = 'True' AND tblDivision.DivisionId = @DivisionId ";
            aCommonInternalDal.LoadDropDownValue(ddl, "SectionName", "SectionId", queryStr, aSqlParameterlist, "HRDB");
        }

        public void GetDivisionList(DropDownList ddl, string companyId)
        {

            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            string queryStr = "SELECT DivisionId,DivisionName FROM tblDivision WHERE IsActive = 'True' AND CompanyId = @CompanyId";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionName", "DivisionId", queryStr, aSqlParameterlist, "HRDB");
        }

        public void GetDivisionWingList(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = "SELECT DivisionWId,DivisionWingName FROM tblDivisionWing WHERE IsActive = 'True' AND DivisionId = @DivisionId AND (Invisible IS NULL OR Invisible='False')";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionWingName", "DivisionWId", queryStr, aSqlParameterlist, "HRDB");
        }

        public void GetDepartmentList(DropDownList ddl, string wingId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@wingId", wingId));

            string queryStr = "SELECT DepartmentId,DepartmentName FROM tblDepartment WHERE IsActive = 'True' AND DivisionWId = @wingId AND (Invisible IS NULL OR Invisible='False')";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", queryStr, aSqlParameterlist, "HRDB");
        }

        public void GetSectionList(DropDownList ddl, string departmentId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@departmentId", departmentId));

            string queryStr = "SELECT SectionId,SectionName FROM tblSection WHERE IsActive = 'True' AND DepartmentId = @departmentId AND (Invisible IS NULL OR Invisible='False')";
            aCommonInternalDal.LoadDropDownValue(ddl, "SectionName", "SectionId", queryStr, aSqlParameterlist, "HRDB");
        }















    }


}
