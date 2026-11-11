using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;

namespace DAL.Report_DAL
{
    public class ReportDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();



        public DataTable GetEmpTrainingInfoDAL(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", Id));
            string queryStr = @"SELECT  * FROM  ( SELECT  EG.SalaryGradeId,EG.SalaryStepId,EG.EmployeeStatus,EG.SalaryLoationId,eg.JobLocationId, EG.FatherName, EG.MotherName, EG.ContractPeriod, EG.DateOfJoin,  EG.SMCOldCode, ST.GrossAmount,  ISNULL(ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL),'') ParentsInfo , DepartmentName, DS.Designation, EG.CompanyId ComId, tblEmpTraining.EmpInfoId, com.ShortName, EG.EmpName, EG.EmpMasterCode, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
 CAST((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate ,
	    rptBody.EmpName Supervisor,   tblEmpTraining.TrainingName,  tblEmpInfoTrainingType.Description TrainingTypeName, dbo.tblEmpTraining.TrainingDescription, 
		tblEmpTrainingInstitution.Description TrainingInstitutionName,
 tblCountry.Title TrainingCountryName,
 tblEmpTraining.TrFromDate, tblEmpTraining.TrToDate ,dbo.tblEmpTraining.TrainingAchievment, dbo.tblEmpTraining.TrainingDays, tblEmpTraining.TrainingPlace, tblEmpTraining.TrRemarks, 
   0 GrandTotal from tblEmpTraining WITH (nolock)
LEFT JOIN tblEmpInfoTrainingType ON tblEmpTraining.TrainingType=tblEmpInfoTrainingType.TrainingTypeID
LEFT JOIN tblEmpTrainingInstitution ON  tblEmpTraining.TrainingInstitution=tblEmpTrainingInstitution.InstitutionID
LEFT JOIN dbo.tblCountry ON  tblEmpTraining.TrainingCountry=tblCountry.CountryID
LEFT JOIN dbo.tblEmpGeneralInfo EG ON  EG.EmpInfoId=tblEmpTraining.EmpInfoId
  LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId

                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               

                                LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 

                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 

								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 

                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana 
                                LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
 
 WHERE tblEmpTraining.IsActive=1  
UNION ALL 
SELECT EG.SalaryGradeId,EG.SalaryStepId,EG.EmployeeStatus,EG.SalaryLoationId,eg.JobLocationId, EG.FatherName, EG.MotherName, EG.ContractPeriod, EG.DateOfJoin,  EG.SMCOldCode, ST.GrossAmount,  ISNULL(ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL),'') ParentsInfo , DepartmentName,  DS.Designation, mas.CompanyId ComId, ddlt.EmpInfoId,com.ShortName, EG.EmpName,EG.EmpMasterCode, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
  CAST((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate ,
	    rptBody.EmpName Supervisor, 
		 mas.TrainingTitle TrainingName, tt.TrainingType TrainingTypeName,  mas.TrainingDetails TrainingDescription,ti.TrainingOrgName  TrainingInstitutionName, ' ' TrainingCountryName  
   
, FORMAT(mas.StartDate,'dd-MMM-yyyy') AS TrFromDate , FORMAT(mas.EndDate,'dd-MMM-yyyy') TrToDate, ' ' TrainingAchievment, mas.NoOfDays TrainingDays,
CASE
    WHEN TrainingOrgLocation = 0 THEN vn.VenueName
    WHEN TrainingOrgLocation != 0 THEN brn.BranchDetails
    
END AS TrainingPlace, '' TrRemarks, mas.GrandTotal    FROM dbo.tblTrainingRecordMaster mas WITH (nolock)
LEFT JOIN  tbl_trainingRecordDetailsEmployee ddlt  ON mas.TrainingRecordMasterId = ddlt.TrainingRecordMasterId
   LEFT JOIN dbo.tblTrainingType tt ON tt.TrainingTypeID = mas.TrainingTypeId
  LEFT JOIN dbo.tblTrainingOrgInfo ti ON ti.TrainingOrgId=mas.TrainingOrgId
  LEFT JOIN tblSMCTrainingVenue vn ON mas.TrainingVenue= vn.SMCVenueID
  LEFT JOIN tblOfficeBranch brn ON brn.TrainingOrgId = mas.TrainingOrgId
  LEFT JOIN dbo.tblEmpGeneralInfo EG ON  EG.EmpInfoId=ddlt.EmpInfoId
    LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId

                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               

                                LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 

                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 

								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 

                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana 
                                LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
 WHERE   ddlt.EmpinfoId    IN (SELECT tblEvaluateTrainingMaster.EmpInfoId FROM tblEvaluateTrainingMaster ) )   EG  WHERE EG.EmpInfoId IS NOT NULL  " + Id;
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public void LoadCompanyDropDownList(DropDownList ddl)
        {
            try
            {
                string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
                aCommonInternalDal.LoadDropDownValueCompany(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
            }
            catch (Exception)
            {

                //throw;
            }
        }


        public void LoadSurveyNameDropDownList(DropDownList ddl)
        {
            try
            {
                string queryStr = "SELECT SurveyMasterId, SurveyName FROM tblSurveyMaster  WITH (NOLOCK) ";
                aCommonInternalDal.LoadDropDownValue(ddl, "SurveyName", "SurveyMasterId", queryStr, "HRDB");
            }
            catch (Exception)
            {

                //throw;
            }
        }
        public DataTable GetAllDivision(string compId)
        {
            string queryStr = @"SELECT * FROM dbo.tblDivision  WITH (NOLOCK) WHERE IsActive='1' AND CompanyId='" + compId + "'";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllWing(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblDivisionWing  WITH (NOLOCK) 
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
  WHERE tblDivisionWing.IsActive='1' AND (Invisible='0' OR Invisible IS NULL) " + param + " ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllDepartment(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblDepartment  WITH (NOLOCK) 
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
WHERE tblDepartment.IsActive='1' AND (tblDepartment.Invisible='0' OR tblDepartment.Invisible IS NULL) " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllSection(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblSection  WITH (NOLOCK) 
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
WHERE tblSection.IsActive='1' AND (tblSection.Invisible='0' OR tblSection.Invisible IS NULL) " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllSubSection(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblSubSection  WITH (NOLOCK) 
LEFT JOIN dbo.tblSection ON tblSection.SectionId = tblSubSection.SectionId
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
WHERE tblSubSection.IsActive='1'  " + param + "";
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

            string queryStr = @"SELECT * FROM dbo.tblSubSection  WITH (NOLOCK) 
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

            string queryStr = @"SELECT DepartmentId,DepartmentName FROM tblDepartment  WITH (NOLOCK) 
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE tblDepartment.IsActive = 'True' AND tblDivision.DivisionId = @DivisionId AND (tblDepartment.Invisible IS NULL OR tblDepartment.Invisible='False')";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", queryStr, aSqlParameterlist, "HRDB");
        }
        public void GetSectionByDivList(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = @"SELECT * FROM dbo.tblSection  WITH (NOLOCK) 
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

            string queryStr = "SELECT DivisionId,DivisionName FROM tblDivision  WITH (NOLOCK)  WHERE IsActive = 'True' AND CompanyId = @CompanyId";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionName", "DivisionId", queryStr, aSqlParameterlist, "HRDB");
        }

        public void GetDivisionWingList(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            string queryStr = "SELECT DivisionWId,DivisionWingName FROM tblDivisionWing  WITH (NOLOCK) WHERE IsActive = 'True' AND DivisionId = @DivisionId AND (Invisible IS NULL OR Invisible='False')";
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
        public void GetCategory(DropDownList ddl)
        {
            
            string queryStr = "SELECT * FROM dbo.tblEmpCategory";
            aCommonInternalDal.LoadDropDownValue(ddl, "EmpCategoryName", "EmpCategoryId", queryStr, "HRDB");
        }
        public void GetJobleftReason(DropDownList ddl)
        {

            string queryStr = "SELECT * FROM dbo.tblJobLeftType";
            aCommonInternalDal.LoadDropDownValue(ddl, "JobLeftType", "JobLeftTypeId", queryStr, "HRDB");
        }
        public void GetSuspendReason(DropDownList ddl)
        {

            string queryStr = "SELECT * FROM dbo.tblSuspendReasonEntry";
            aCommonInternalDal.LoadDropDownValue(ddl, "SuspendReasonEntry", "SuspendReasonEntryId", queryStr, "HRDB");
        }
        public DataTable GetGrade()
        {
            string queryStr = @"SELECT * FROM dbo.tblSalaryGrade  WITH (NOLOCK)";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetSalLoc()
        {
            string queryStr = @"SELECT * FROM dbo.tblSalaryLocation  WITH (NOLOCK)";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        } public DataTable GetSalLocbyCompany(int comId)
        {
            string queryStr = @"SELECT * FROM dbo.tblSalaryLocation where IsActive=1 and ComID=" + comId+ "  order by SalaryLocation asc";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetDesig()
        {
            string queryStr = @"SELECT * FROM dbo.tblDesignation  WITH (NOLOCK) WHERE IsActive='1' ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetStep(string param)
        {
            string queryStr = @"SELECT SalaryStepName+'('+GradeCode+')'AS SalaryStepName,SalaryStepId  FROM dbo.tblSalaryStep EG  WITH (NOLOCK)
LEFT JOIN dbo.tblSalaryGrade ON tblSalaryGrade.SalaryGradeId = EG.SalaryGradeId WHERE GradeCode IS NOT NULL " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        
        }
        public DataTable GetEmpInfOnlyViewo(string param, string param2)
        {
            string queryStr = @"select Distinct * from (SELECT EG.PresentTelNo,EG.PersonalMobile PermanantTelNo, EG.EmergencyContactAddress, EG.EmergencyContactPerson,  ISNULL(EG.MaritalStatus, 'Unknown') MaritalStatus, FORMAT(EG.DateOfMarriage ,'dd-MMM-yyyy') MarriageDate,  case when  pro.reappCount>0 then 'Yes' else 'No' end reappointment, case when  EG.RecruitmentTypeNew=1 then 'Yes' else 'No' end  RecruitmentTypeNew,   case when  EG.RecruitmentTypeReplacement=1 then 'Yes' else 'No' end  RecruitmentTypeReplacement, empType.EmpType, EG.SMCOldCode,  EG.TinNo, EG.OfficialEmail,EG.PersonalEmail,EG.EmergencyContactNumber, EG.PersonalMobile, EG.MotherName,EG.FatherName, EG.ContractPeriod, EG.DateOfJoin Doj, EG.DateofBirth DOB,  DP.DepartmentName, DS.Designation, EG.EmpName, EG.EmpInfoId,EG.EmpMasterCode, STUFF( (SELECT CONCAT( ' ('+ CAST(ROW_NUMBER() OVER(ORDER BY (SELECT 1)) as nvarchar(max)) + ') ', mm.NomineeName+isnull(' : '+rel.Description , ''), '  ')   FROM tblEmpNominee mm (NOLOCK)  left join tblRelation rel on mm.NomineeRelation=rel.RelationID WHERE mm.EmpInfoId=EG.EmpInfoId and mm.IsActive=1 ORDER BY mm.EmpNomineeId asc FOR XML PATH ('') ),1,1,'') AS NomineeName,  CAST((DATEDIFF(year, EG.DateOfBirth, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  EmpAge,   CAST(ISNULL( EmpExp.Experiece ,0)+  ISNULL((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) ,0) AS NVARCHAR(max))+ ' Years, '+ CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'   as  EmpExperiece , ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL) ParentsInfo , com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   CAST((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , rptBody.EmpName Supervisor, ST.GrossAmount, EJ.JobLeftDate  FROM dbo.tblEmpGeneralInfo EG  WITH (NOLOCK)
                                LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId

                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               

                                LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 

                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 

								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 

                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana 
                                LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
     LEFT JOIN dbo.tblEmployeeType empType ON empType.EmpTypeId = EG. EmpTypeId
                              
LEFT JOIN  (select ISNULL(COUNT(EmployeeId),0) reappCount, EmployeeId from  dbo.tblEmployeePromotionEntry where IsReappointment=1 group by  EmployeeId)  pro ON pro.EmployeeId = EG. EmpInfoId   

                                     LEFT JOIN (SELECT   EmployeeId,MAX(JobLeftDate)JobLeftDate FROM tblEmployeeJobLeft WHERE  ( (IsDelete IS NULL) OR (IsDelete = 0)) GROUP BY EmployeeId) AS EJ ON EG.EmpInfoId = EJ.EmployeeId

 

								 LEFT JOIN (SELECT EmpInfoId,ISNULL(SUM(DATEDIFF(YEAR,ExpFromDate,ExpToDate)),0) Experiece FROM dbo.tblEmpExperience WITH (NOLOCK) WHERE IsActive=1  GROUP BY EmpInfoId) AS EmpExp ON EG.EmpInfoId = EmpExp.EmpInfoId


                                WHERE EG.EmpMasterCode is not null  " + param + @" 
								union all 


								SELECT  EG.PresentTelNo,EG.PersonalMobile PermanantTelNo, EG.EmergencyContactAddress, EG.EmergencyContactPerson,  ISNULL(EG.MaritalStatus, 'Unknown') MaritalStatus, FORMAT(EG.DateOfMarriage ,'dd-MMM-yyyy') MarriageDate,   case when  pro.reappCount>0 then 'Yes' else 'No' end reappointment,  case when  EG.RecruitmentTypeNew=1 then 'Yes' else 'No' end  RecruitmentTypeNew,   case when  EG.RecruitmentTypeReplacement=1 then 'Yes' else 'No' end  RecruitmentTypeReplacement,  empType.EmpType,  EG.SMCOldCode, EG.TinNo, EG.OfficialEmail,EG.PersonalEmail, EG.EmergencyContactNumber,EG.PersonalMobile,EG.MotherName,EG.FatherName, EG.ContractPeriod,  EG.DateOfJoin Doj, EG.DateofBirth DOB,  DP.DepartmentName, DS.Designation,EG.EmpName, EG.EmpInfoId,EG.EmpMasterCode,STUFF( (SELECT CONCAT( ' ('+ CAST(ROW_NUMBER() OVER(ORDER BY (SELECT 1)) as nvarchar(max)) + ') ', mm.NomineeName+isnull(' : '+rel.Description , ''), '  ')   FROM tblEmpNominee mm (NOLOCK)  left join tblRelation rel on mm.NomineeRelation=rel.RelationID WHERE mm.EmpInfoId=EG.EmpInfoId and mm.IsActive=1 ORDER BY mm.EmpNomineeId asc FOR XML PATH ('') ),1,1,'') AS NomineeName,  CAST((DATEDIFF(year, EG.DateOfBirth, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  EmpAge,   CAST(ISNULL( EmpExp.Experiece ,0)+  ISNULL((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) ,0) AS NVARCHAR(max))+ ' Years, '+ CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'   as  EmpExperiece , ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL) ParentsInfo , com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   CAST((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , rptBody.EmpName Supervisor, ST.GrossAmount, EJ.JobLeftDate  FROM dbo.tblEmpGeneralInfo EG  WITH (NOLOCK)
                                LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId

                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               

                                LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 

                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 

								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 

                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana 
                                LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
     LEFT JOIN dbo.tblEmployeeType empType ON empType.EmpTypeId = EG. EmpTypeId
                              
LEFT JOIN  (select ISNULL(COUNT(EmployeeId),0) reappCount, EmployeeId from  dbo.tblEmployeePromotionEntry where IsReappointment=1 group by  EmployeeId)  pro ON pro.EmployeeId = EG. EmpInfoId   

                                     LEFT JOIN (SELECT   EmployeeId,MAX(JobLeftDate)JobLeftDate FROM tblEmployeeJobLeft WHERE  ( (IsDelete IS NULL) OR (IsDelete = 0)) GROUP BY EmployeeId) AS EJ ON EG.EmpInfoId = EJ.EmployeeId

 

								 LEFT JOIN (SELECT EmpInfoId,ISNULL(SUM(DATEDIFF(YEAR,ExpFromDate,ExpToDate)),0) Experiece FROM dbo.tblEmpExperience WITH (NOLOCK) WHERE IsActive=1  GROUP BY EmpInfoId) AS EmpExp ON EG.EmpInfoId = EmpExp.EmpInfoId
  inner JOIN   tblEmpAllRefference reff  ON EG.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 WHERE        reff.ShowCompany in (ComAssain)   " + param2 + " )tbl  ORDER BY  EmpMasterCode ASC";

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }


        public DataTable GetEmpInfOnlyViewRequirment(string param, string param2, string pppp)
        {
            string queryStr = @"select Distinct   * from (SELECT ISNULL(proEffectivedate,  EG.DateOfJoin )   proEffectivedate,  case when  pro.reappCount>0 then 'Reappointment'   when  EG.RecruitmentTypeNew=1 then 'New'    when  EG.RecruitmentTypeReplacement=1 then 'Replacement' else '' end  RecruitmentType, empType.EmpType, EG.SMCOldCode,  EG.TinNo, EG.OfficialEmail,EG.PersonalEmail,EG.EmergencyContactNumber, EG.PersonalMobile, EG.MotherName,EG.FatherName, EG.ContractPeriod, EG.DateOfJoin Doj, EG.DateofBirth DOB,  DP.DepartmentName, DS.Designation, EG.EmpName, EG.EmpInfoId,EG.EmpMasterCode, STUFF( (SELECT CONCAT( ' ('+ CAST(ROW_NUMBER() OVER(ORDER BY (SELECT 1)) as nvarchar(max)) + ') ', mm.NomineeName+isnull(' : '+rel.Description , ''), '  ')   FROM tblEmpNominee mm (NOLOCK)  left join tblRelation rel on mm.NomineeRelation=rel.RelationID WHERE mm.EmpInfoId=EG.EmpInfoId and mm.IsActive=1 ORDER BY mm.EmpNomineeId asc FOR XML PATH ('') ),1,1,'') AS NomineeName,  CAST((DATEDIFF(year, EG.DateOfBirth, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  EmpAge,   CAST(ISNULL( EmpExp.Experiece ,0)+  ISNULL((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) ,0) AS NVARCHAR(max))+ ' Years, '+ CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'   as  EmpExperiece , ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL) ParentsInfo , com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   CAST((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , rptBody.EmpName Supervisor, ST.GrossAmount, EJ.JobLeftDate  FROM dbo.tblEmpGeneralInfo EG  WITH (NOLOCK)
                                LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId

                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               

                                LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 

                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 

								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 

                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana 
                                LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
     LEFT JOIN dbo.tblEmployeeType empType ON empType.EmpTypeId = EG. EmpTypeId
                              
LEFT JOIN  (select ISNULL(COUNT(EmployeeId),0) reappCount, MAX(Effectivedate) proEffectivedate, EmployeeId from  dbo.tblEmployeePromotionEntry where IsReappointment=1 group by  EmployeeId)  pro ON pro.EmployeeId = EG. EmpInfoId

                                     LEFT JOIN (SELECT   EmployeeId,MAX(JobLeftDate)JobLeftDate FROM tblEmployeeJobLeft WHERE  ( (IsDelete IS NULL) OR (IsDelete = 0)) GROUP BY EmployeeId) AS EJ ON EG.EmpInfoId = EJ.EmployeeId

 

								 LEFT JOIN (SELECT EmpInfoId,ISNULL(SUM(DATEDIFF(YEAR,ExpFromDate,ExpToDate)),0) Experiece FROM dbo.tblEmpExperience WITH (NOLOCK) WHERE IsActive=1  GROUP BY EmpInfoId) AS EmpExp ON EG.EmpInfoId = EmpExp.EmpInfoId


                                WHERE EG.EmpMasterCode is not null   " + param + @" 
								union all 


								SELECT ISNULL(proEffectivedate,  EG.DateOfJoin )  proEffectivedate,    case when  pro.reappCount>0 then 'Reappointment'   when  EG.RecruitmentTypeNew=1 then 'New'    when  EG.RecruitmentTypeReplacement=1 then 'Replacement' else '' end  RecruitmentType ,  empType.EmpType,  EG.SMCOldCode, EG.TinNo, EG.OfficialEmail,EG.PersonalEmail, EG.EmergencyContactNumber,EG.PersonalMobile,EG.MotherName,EG.FatherName, EG.ContractPeriod,  EG.DateOfJoin Doj, EG.DateofBirth DOB,  DP.DepartmentName, DS.Designation,EG.EmpName, EG.EmpInfoId,EG.EmpMasterCode,STUFF( (SELECT CONCAT( ' ('+ CAST(ROW_NUMBER() OVER(ORDER BY (SELECT 1)) as nvarchar(max)) + ') ', mm.NomineeName+isnull(' : '+rel.Description , ''), '  ')   FROM tblEmpNominee mm (NOLOCK)  left join tblRelation rel on mm.NomineeRelation=rel.RelationID WHERE mm.EmpInfoId=EG.EmpInfoId and mm.IsActive=1 ORDER BY mm.EmpNomineeId asc FOR XML PATH ('') ),1,1,'') AS NomineeName,  CAST((DATEDIFF(year, EG.DateOfBirth, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  EmpAge,   CAST(ISNULL( EmpExp.Experiece ,0)+  ISNULL((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) ,0) AS NVARCHAR(max))+ ' Years, '+ CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'   as  EmpExperiece , ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL) ParentsInfo , com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   CAST((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , rptBody.EmpName Supervisor, ST.GrossAmount, EJ.JobLeftDate  FROM dbo.tblEmpGeneralInfo EG  WITH (NOLOCK)
                                LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId

                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               

                                LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 

                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 

								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 

                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana 
                                LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
     LEFT JOIN dbo.tblEmployeeType empType ON empType.EmpTypeId = EG. EmpTypeId
                              
LEFT JOIN  (select ISNULL(COUNT(EmployeeId),0) reappCount, MAX(Effectivedate) proEffectivedate, EmployeeId from  dbo.tblEmployeePromotionEntry where IsReappointment=1 group by  EmployeeId)  pro ON pro.EmployeeId = EG. EmpInfoId   

                                     LEFT JOIN (SELECT   EmployeeId,MAX(JobLeftDate)JobLeftDate FROM tblEmployeeJobLeft WHERE  ( (IsDelete IS NULL) OR (IsDelete = 0)) GROUP BY EmployeeId) AS EJ ON EG.EmpInfoId = EJ.EmployeeId

 

								 LEFT JOIN (SELECT EmpInfoId,ISNULL(SUM(DATEDIFF(YEAR,ExpFromDate,ExpToDate)),0) Experiece FROM dbo.tblEmpExperience WITH (NOLOCK) WHERE IsActive=1  GROUP BY EmpInfoId) AS EmpExp ON EG.EmpInfoId = EmpExp.EmpInfoId
  inner JOIN   tblEmpAllRefference reff  ON EG.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 WHERE        reff.ShowCompany in (ComAssain)     " + param2 + " )tbl  where RecruitmentType <>''  " + pppp + @"  ORDER BY  EmpMasterCode ASC";

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetEmpInfo(string param)
        {
            string queryStr = @"SELECT  STUFF( (SELECT CONCAT( ' ('+ CAST(ROW_NUMBER() OVER(ORDER BY (SELECT 1)) as nvarchar(max)) + ') ', mm.NomineeName+isnull(' : '+rel.Description , ''), '  ')   FROM tblEmpNominee mm (NOLOCK)  left join tblRelation rel on mm.NomineeRelation=rel.RelationID WHERE mm.EmpInfoId=EG.EmpInfoId and mm.IsActive=1 ORDER BY mm.EmpNomineeId asc FOR XML PATH ('') ),1,1,'') AS NomineeName,  CAST((DATEDIFF(year, EG.DateOfBirth, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  EmpAge,   CAST(ISNULL( EmpExp.Experiece ,0)+  ISNULL((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) ,0) AS NVARCHAR(max))+ ' Years, '+ CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'   as  EmpExperiece , ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL) ParentsInfo , com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   CAST((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , rptBody.EmpName Supervisor, ST.GrossAmount, EJ.JobLeftDate,
* FROM dbo.tblEmpGeneralInfo EG  WITH (NOLOCK)
                                LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId

                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               

                                LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 

                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 

								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 

                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana 
                                LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
     LEFT JOIN dbo.tblEmployeeType empType ON empType.EmpTypeId = EG. EmpTypeId
                              

                                     LEFT JOIN (SELECT   EmployeeId,MAX(JobLeftDate)JobLeftDate FROM tblEmployeeJobLeft WHERE  ( (IsDelete IS NULL) OR (IsDelete = 0)) GROUP BY EmployeeId) AS EJ ON EG.EmpInfoId = EJ.EmployeeId

 

								 LEFT JOIN (SELECT EmpInfoId,ISNULL(SUM(DATEDIFF(YEAR,ExpFromDate,ExpToDate)),0) Experiece FROM dbo.tblEmpExperience WITH (NOLOCK) WHERE IsActive=1  GROUP BY EmpInfoId) AS EmpExp ON EG.EmpInfoId = EmpExp.EmpInfoId


                                WHERE EG.EmpMasterCode is not null  " + param + " ORDER BY EG.EmpMasterCode ASC";

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }


        public DataTable GetDTEmpChildrenByEmpId(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@mid", mid));
            string query = @"SELECT FORMAT(  c.ChildrenDOB,  'dd-MMM-yyyy') AS ChildrenDOB,  
       c.EmpChildrenId,
       c.EmpInfoId,
       c.ChildrenName,
       c.ChildrenGender,
       c.ChildrenOccupation,
	   occ.Description ChildrenOccupationName,
       c.ChildrenDOB,
       c.ChildrenMaritalStatus,
       c.IsActive FROM dbo.tblEmpChildren c
	   left JOIN dbo.tblOccupation occ ON occ.OccupationID=c.ChildrenOccupation
	    WHERE c.IsActive=1 and c.EmpInfoId=@mid";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable GetEmployeeFamilyInfoDAL(string Id)
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




        public DataTable LoadContractualEmpInfo(string param, string param2)
        {
            string queryStr = @"SELECT EPE.PermanentToContractualEffectiveDate,  EPE.RenewToDate, EPE.RenewStartDate, EPE.RenewStartDate, EPE.ExtensionToDate, EPE.ExtensionFromDate,  EG.ContractPeriod, DV.DivisionName Division, DP.DepartmentName, DS.Designation, EG.EmpName, EG.EmpMasterCode,rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   CAST((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate,  ISNULL('From: '+EPE.FromProject+' - To:'+EPE.ToProject,'') TransferProject   From tblContractualEmpManage EPE  with (nolock)

   LEFT JOIN dbo.tblEmpGeneralInfo EG ON EPE.EmployeeId = EG.EmpInfoId
	   LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
 LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId

                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               

                                LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 

                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 

								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 
LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana
  WHERE ((IsDelete=0) OR (IsDelete IS NULL))   " + param + @" UNION ALL SELECT ''PermanentToContractualEffectiveDate,  ''RenewToDate, ''RenewStartDate, ''RenewStartDate, EPE.ExtensionToDate, ''ExtensionFromDate, EG.ContractPeriod, DV.DivisionName Division, DP.DepartmentName, DS.Designation, EG.EmpName, EG.EmpMasterCode,rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   CAST((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate, '' TransferProject
									  FROM tblStateChange_HistoricalDataId EPE

									    LEFT JOIN dbo.tblEmpGeneralInfo EG ON EPE.EmployeeId = EG.EmpInfoId
	   LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
 LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId

                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               

                                LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 

                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 

								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 
LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana
 left JOIN dbo.tblDepartment  Dpt ON EG.DepartmentId = Dpt.DepartmentId  where StateChange_HistoricalDataId  is not null  " + param2+" ORDER BY  EmpMasterCode asc";

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }


        public DataTable EducationGetEmpInfo(string param)
        {
            string queryStr = @"SELECT  ISNULL(ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL),'') ParentsInfo , rptBody.EmpName Supervisor, edu.EmpEducationId,
       edu.EmpInfoId,
      
	   en.Description AS EducationName,
       
	   bu.Description AS BoardUniversity,
      
	   esg.Description AS SubjectGroup,
       
	   ei.Description AS EducationalInstitute,
     
	  sp.Description AS FieldOfSpecialization,
       edu.PassingYear,
       edu.Result,
       edu.CgpaOrTotalMarks,
       edu.EduIsLastLevel,
edu.IsProfessionalEdu, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   CAST((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate ,
* FROM dbo.tblEmpGeneralInfo EG   WITH (NOLOCK)
                                LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId

                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               

                                LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 

                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 

								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 

                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana 
                                LEFT JOIN dbo.tblEmpEducation edu ON edu.EmpInfoId = EG.EmpInfoId 

								 LEFT JOIN dbo.tblEducationName en ON en.EducationNameID = edu.EducationNameId
	   LEFT JOIN dbo.tblBoardUniversity bu ON bu.BoardUniversityID = edu.BoardUniversityId
	   LEFT JOIN dbo.tblEducationSubjectGroup esg ON esg.EducationSubjectGroupID=edu.SubjectGroupId
	   LEFT JOIN dbo.tblEducationalInstitution ei ON ei.InstitutionID=edu.EducationalInstituteId
	   LEFT JOIN dbo.tblSpecialization sp ON sp.SpecializationID=edu.FieldOfSpecializationId
LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
                                LEFT JOIN (SELECT TOP 1 EmployeeId,JobLeftDate FROM tblEmployeeJobLeft WHERE IsDelete 
                                IS NULL GROUP BY EmployeeId,JobLeftDate ORDER BY JobLeftDate DESC) AS EJ ON EG.EmpInfoId = EJ.EmployeeId
                                WHERE EG.EmpMasterCode IS NOT NULL   " + param + "  ORDER BY EG.EmpMasterCode ASC";

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }


        public DataTable GetDistrict()
        {
            string queryStr = @"SELECT DistrictID,Titel FROM tblDistrict  WITH (NOLOCK) ORDER BY Titel";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetThana(string param)
        {
            string queryStr = @"SELECT EG.Title +' ('+ tblDistrict.Titel +')'AS Thana,EG.ThanaID  FROM dbo.tblThana EG  WITH (NOLOCK) 
                                LEFT JOIN dbo.tblDistrict ON tblDistrict.DistrictID = EG.DistrictID WHERE EG.Code IS NOT NULL " + param + " ORDER BY tblDistrict.Titel";

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetPlace(string param)
        {
            string queryStr = @"Select EG.JobLocationID, EG.SalaryLoationId,   EG.Location +' ('+ tblSalaryLocation.SalaryLocation+')' AS Location FROM dbo.tblJobLocation EG   WITH (NOLOCK)
 LEFT JOIN dbo.tblSalaryLocation  ON tblSalaryLocation.SalaryLoationId = EG.SalaryLoationId
WHERE EG.SalaryLoationId IS NOT NULL  " + param + " ORDER BY EG.Location";

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }


        public DataTable GetNominationPurposes()
        {
            string queryStr = @"SELECT NPID,Description FROM dbo.tblNominationPurpose  WITH (NOLOCK) ORDER BY Description";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public void LoadddlEducation(DropDownList ddl)
        {
            string queryStr = "SELECT EducationNameID,Description FROM dbo.tblEducationName  WITH (NOLOCK) ";
            aCommonInternalDal.LoadDropDownValue(ddl, "Description", "EducationNameID", queryStr, "HRDB");
        }

        public void LoadddlSubjectGroup(DropDownList ddl)
        {
            string queryStr = "SELECT EducationSubjectGroupID, Description FROM dbo.tblEducationSubjectGroup  WITH (NOLOCK) ";
            aCommonInternalDal.LoadDropDownValue(ddl, "Description", "EducationSubjectGroupID", queryStr, "HRDB");
        }



        public DataTable LoadddlEducation()
        {
            string queryStr = "SELECT EducationNameID,Description FROM dbo.tblEducationName  WITH (NOLOCK)";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable LoadddlSubjectGroup()
        {
            string queryStr = "SELECT EducationSubjectGroupID, Description FROM dbo.tblEducationSubjectGroup  WITH (NOLOCK)";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public void LoadActionType(DropDownList ddl, string companyId)
        {
            string query = @"SELECT SPN.SuspendReasonEntryId,
                            SPN.SuspendReasonEntry FROM dbo.tblSuspendReasonEntry AS SPN  WITH (NOLOCK)
                            WHERE SPN.IsActive = 1 AND (SPN.IsDelete = 0 OR SPN.IsDelete IS NULL) AND IsDisciplinary = 1 AND  SPN.CompanyId = " + companyId;
            aCommonInternalDal.LoadDropDownValue(ddl, "SuspendReasonEntry", "SuspendReasonEntryId", query, "HRDB");
        }

        public void LoadddlCountry(DropDownList ddl)
        {
            string queryStr = "SELECT CountryID,Title FROM dbo.tblCountry  WITH (NOLOCK) ";
            aCommonInternalDal.LoadDropDownValue(ddl, "Title", "CountryID", queryStr, "HRDB");
        }

        public DataTable rptGetSurveySetupListINNNN(string param)
        //string param
        {

            try
            {
                string query = @"SELECT  rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
CAST((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days' ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , a.SurveyMasterId ,  d.FinancialYearDesc , A.SurveyName, A.SurveyFrom, A.SurveyTo,* 
  from dbo.tblSurveyMaster A  WITH (NOLOCK) 
   left join (SELECT SurveyMasterId, EmployeeId from tblSurveyParticipate group BY EmployeeId,  SurveyMasterId) B on a.SurveyMasterId = b.SurveyMasterId  
                                     LEFT JOIN dbo.tblEmpGeneralInfo EG ON  B.EmployeeId = EG.EmpInfoId
                                     LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId
                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 
                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 
								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 
                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana
                                left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId
LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
                                where (a.IsDelete is null or a.IsDelete = 0) and     B.EmployeeId     IN (SELECT tblSurveySubmitMaster.EmployeeId FROM tblSurveySubmitMaster ) " + param;
                //+ param
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }


        public DataTable rptGetApprisalSetupListINNNN(string param)
        //string param
        {

            try
            {
                string query = @"SELECT SupervisorMark, rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   CAST((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate  ,* 
  from dbo.tblAppraisalDeadlineMaster A  WITH (NOLOCK) 
   left join (SELECT AppraisalDeadLineMasterId, EmpinfoId from tblAppraisalDeadLineDetails group BY AppraisalDeadLineMasterId, EmpinfoId) B on a.AppraisalDeadLineMasterId = b.AppraisalDeadLineMasterId  
   left join (SELECT tblAppraisalMaster.EmpInfoId,tblAppraisalMaster.AppraisalSelfMasterId, SUM(ISNULL(aa.SupervisorMark,0))SupervisorMark FROM tblAppraisalMaster
    left join tblAppraisalFuncArea aa on aa.AppraisalSelfMasterId=tblAppraisalMaster.AppraisalSelfMasterId group by tblAppraisalMaster.EmpInfoId,tblAppraisalMaster.AppraisalSelfMasterId
   )  mdtMarks on mdtMarks.EmpInfoId = b.EmpinfoId 
  
                                     LEFT JOIN dbo.tblEmpGeneralInfo EG ON  B.EmpinfoId = EG.EmpInfoId
                                     LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId
                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 
                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 
								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 
                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana
                                left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId
LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
                                where (a.IsDelete is null or a.IsDelete = 0) and     B.EmpinfoId    IN (SELECT tblAppraisalMaster.EmpInfoId FROM tblAppraisalMaster WHERE tblAppraisalMaster.SelfApprove='Approved' )   " + param;
                //+ param
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }


        public DataTable rptGetApprisalSetupListINNNNNew(string param, string param2, string FinYear)
        //string param
        {

            try
            {
                string query = @"select distinct * from (SELECT A.Subject, A.DeclarationDate , EG.TinNo, empType.EmpType,  EG.ContractPeriod, EG.FatherName, EG.MotherName, EG.PersonalMobile, EG.EmergencyContactNumber, EG.PersonalEmail, EG.OfficialEmail, CAST(ISNULL( EmpExp.Experiece ,0)+  ISNULL((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) ,0) AS NVARCHAR(max))+ ' Years, '+ CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'   as  EmpExperiece , DP.DepartmentName DepartmentName, DS.Designation,  EG.EmpName EmpName, d.FinancialYearDesc,  EG.SMCOldCode, EG.EmpMasterCode,  ISNULL(dtScore14_15.HisScore,0) HisScore14_15 ,  ISNULL(dtScore15_16.HisScore,0) HisScore15_16 ,  ISNULL(dtScore16_17.HisScore,0) HisScore16_17,  ISNULL(dtScore17_18.HisScore,0) HisScore17_18,  ISNULL(dtScore18_19.HisScore,0) HisScore18_19,ISNULL(0,0) HisScore19_20, ISNULL(dtScore21_22.HisScoreScore21_22,0) HisScore21_22, ISNULL(FORMAT(dtEmployeePromotion.Effectivedate, 'dd-MMM-yyyy'),dtEmployeePromotionNew.EffectivedateNew)
    
    AS LastPromotion,    CAST((DATEDIFF(year, EG.DateOfBirth, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  EmpAge,  CASE when ISNULL(tbldis.DisCout,0)=0 THEN 'No' ELSE 'Yes' end DiciplinaryCout,  STUFF( (SELECT CONCAT('             '+ CHAR(13), '('+CAST(ROW_NUMBER() Over (Order by AppraisalTrainingId) AS NVARCHAR(max))+')  '+ mm.TrainingNeeds + CHAR(13), '') FROM tblAppraisalTrainingNeeds mm (NOLOCK)  WHERE appMaster.AppraisalMasterId=mm.AppraisalMasterId ORDER BY mm.AppraisalTrainingId FOR XML PATH ('') ),1,1,'') AS DegreeName,  ISNULL(func.SupervisorMark,0)SupervisorMarks, ISNULL(behave.Score,0) SupervisorScore,  ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0) TotalMarks, 
  CASE
  WHEN ISNULL(func.SupervisorMark,0) + ISNULL(behave.Score,0) <= 60 THEN 'Poor'
  WHEN ISNULL(func.SupervisorMark,0) + ISNULL(behave.Score,0) > 60
       AND ISNULL(func.SupervisorMark,0) + ISNULL(behave.Score,0) <= 75 THEN 'Average'
  WHEN ISNULL(func.SupervisorMark,0) + ISNULL(behave.Score,0) > 75
       AND ISNULL(func.SupervisorMark,0) + ISNULL(behave.Score,0) <= 80 THEN 'Good'
  WHEN ISNULL(func.SupervisorMark,0) + ISNULL(behave.Score,0) > 80
       AND ISNULL(func.SupervisorMark,0) + ISNULL(behave.Score,0) <= 90 THEN 'Excellent'
  WHEN ISNULL(func.SupervisorMark,0) + ISNULL(behave.Score,0) > 90 THEN 'Outstanding'
  ELSE ''
END AS FinalStatus,  CASE WHEN appFin.GeneralIncrement=1 THEN 'Yes' ELSE 'No' END GI,  CASE WHEN appFin.SpecialIncrement=1 THEN 'Yes' ELSE 'No' END SI,   CASE WHEN appFin.IsPromotion=1 THEN 'Yes' ELSE 'No' END Promotion,    CASE WHEN appFin.Pip=1 THEN 'Yes' ELSE 'No' END Pip,    CASE WHEN appFin.Other=1 THEN 'Yes' ELSE 'No' END PromotionwithIncrement, SupervisorMark, rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, cat.EmpCategoryName, SG.GradeCode Grade , ST.GrossAmount, ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   CAST((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate    
  from dbo.tblAppraisalDeadlineMaster A  WITH (NOLOCK) 
   left join (SELECT AppraisalDeadLineMasterId, EmpinfoId from tblAppraisalDeadLineDetails group BY AppraisalDeadLineMasterId, EmpinfoId) B on a.AppraisalDeadLineMasterId = b.AppraisalDeadLineMasterId  
  



   --  left join (SELECT tblAppraisalMaster.EmpInfoId,tblAppraisalMaster.AppraisalSelfMasterId, SUM(ISNULL(aa.SupervisorScore,0))SupervisorScore FROM tblAppraisalMaster
   -- left join dbo.tblAppraisalBehaveArea aa on aa.AppraisalSelfMasterId=tblAppraisalMaster.AppraisalSelfMasterId group by tblAppraisalMaster.EmpInfoId,tblAppraisalMaster.AppraisalSelfMasterId
   --)  mdbehabe on mdtMarks.EmpInfoId = b.EmpinfoId 
  
                                     LEFT JOIN dbo.tblEmpGeneralInfo EG ON  B.EmpinfoId = EG.EmpInfoId
                                     LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId
                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 
                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 
								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 
                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana
                                left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId
LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
   LEFT JOIN tblAppraisalMaster appMaster ON appMaster.EmpInfoId = EG.EmpInfoId  and appMaster.FinancialYearId=d.FinancialYearId
      LEFT JOIN ( SELECT  SUM(SupervisorMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON appMaster.AppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SupervisorScore) Score ,
                            AppraisalMasterId
                    FROM    tblAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON appMaster.AppraisalMasterId = behave.AppraisalMasterId

 left JOIN (SELECT EmployeeId,MAX(EffectDate)AS Effectivedate  FROM  dbo.tblPromotionUpgrationHistory   where TypeOfPromotion in ('Promotion','Upgradation') GROUP BY EmployeeId
 
   )dtEmployeePromotion ON dtEmployeePromotion.EmployeeId=EG.EmpInfoId   


   left JOIN (SELECT EmployeeId,MAX(Effectivedate)AS EffectivedateNew  FROM  dbo.tblEmployeePromotionEntry GROUP BY EmployeeId
 
   )dtEmployeePromotionNew ON dtEmployeePromotionNew.EmployeeId=EG.EmpInfoId    LEFT JOIN (SELECT EmpInfoId,ISNULL(SUM(DATEDIFF(YEAR,ExpFromDate,ExpToDate)),0) Experiece FROM dbo.tblEmpExperience WITH (NOLOCK) WHERE IsActive=1  GROUP BY EmpInfoId) AS EmpExp ON EG.EmpInfoId = EmpExp.EmpInfoId
 LEFT JOIN dbo.tblAppraisalFinalStatus appFin ON appFin.AppraisalMasterId = appMaster.AppraisalMasterId

  LEFT JOIN dbo.tblEmployeeType empType ON empType.EmpTypeId = EG. EmpTypeId


    left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (19,20) 
	 GROUP BY EmpMasterID
 
   )dtScore14_15 ON dtScore14_15.EmpMasterID=EG.EmpMasterCode  
   
   
    left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (17,18) 
	 GROUP BY EmpMasterID
 
   )dtScore15_16 ON dtScore15_16.EmpMasterID=EG.EmpMasterCode 


     left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (15,16) 
	 GROUP BY EmpMasterID
 
   )dtScore16_17 ON dtScore16_17.EmpMasterID=EG.EmpMasterCode 

    left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (13,14) 
	 GROUP BY EmpMasterID
 
   )dtScore17_18 ON dtScore17_18.EmpMasterID=EG.EmpMasterCode 

    left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (1,2) 
	 GROUP BY EmpMasterID
 
   )dtScore18_19 ON dtScore18_19.EmpMasterID=EG.EmpMasterCode 



   left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScoreScore21_22 FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (11,12) 
	 GROUP BY EmpMasterID
 
   )dtScore21_22 ON dtScore21_22.EmpMasterID=EG.EmpMasterCode 

                              
 LEFT JOIN (SELECT EmpInfoId, ISNULL(COUNT(*),0) DisCout FROM dbo.tblDiciplinaryAction WHERE EffectiveDate BETWEEN  CONVERT(DATE,dateadd(yy,-3,datediff(d,0,getdate()))) AND CONVERT(DATE,dateadd(yy,0,datediff(d,0,getdate())))   GROUP BY EmpInfoId) tbldis ON  tbldis.EmpInfoId = EG.EmpInfoId
  
                                where (a.IsDelete is null or a.IsDelete = 0) and     B.EmpinfoId    IN ( SELECT tblAppraisalMaster.EmpInfoId FROM tblAppraisalMaster  LEFT JOIN tblFinancialYear y ON tblAppraisalMaster.FinancialYearId = y.FinancialYearId WHERE tblAppraisalMaster.SelfApprove='Approved'  AND    tblAppraisalMaster.CurrentStatus='Approved' and y.FinancialYearDesc='" + FinYear+"' )          " + param + @"   	union all 


								
SELECT A.Subject, A.DeclarationDate , EG.TinNo, empType.EmpType,  EG.ContractPeriod, EG.FatherName, EG.MotherName, EG.PersonalMobile, EG.EmergencyContactNumber, EG.PersonalEmail, EG.OfficialEmail, CAST(ISNULL( EmpExp.Experiece ,0)+  ISNULL((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) ,0) AS NVARCHAR(max))+ ' Years, '+ CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'   as  EmpExperiece , DP.DepartmentName DepartmentName, DS.Designation,  EG.EmpName EmpName, d.FinancialYearDesc,  EG.SMCOldCode, EG.EmpMasterCode,  ISNULL(dtScore14_15.HisScore,0) HisScore14_15 ,  ISNULL(dtScore15_16.HisScore,0) HisScore15_16 ,  ISNULL(dtScore16_17.HisScore,0) HisScore16_17,  ISNULL(dtScore17_18.HisScore,0) HisScore17_18,  ISNULL(dtScore18_19.HisScore,0) HisScore18_19,ISNULL(0,0) HisScore19_20, ISNULL(dtScore21_22.HisScoreScore21_22,0) HisScore21_22, ISNULL(FORMAT(dtEmployeePromotion.Effectivedate, 'dd-MMM-yyyy'),dtEmployeePromotionNew.EffectivedateNew)
    
    AS LastPromotion,    CAST((DATEDIFF(year, EG.DateOfBirth, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  EmpAge,  CASE when ISNULL(tbldis.DisCout,0)=0 THEN 'No' ELSE 'Yes' end DiciplinaryCout,  STUFF( (SELECT CONCAT('             '+ CHAR(13), '('+CAST(ROW_NUMBER() Over (Order by AppraisalTrainingId) AS NVARCHAR(max))+')  '+ mm.TrainingNeeds + CHAR(13), '') FROM tblAppraisalTrainingNeeds mm (NOLOCK)  WHERE appMaster.AppraisalMasterId=mm.AppraisalMasterId ORDER BY mm.AppraisalTrainingId FOR XML PATH ('') ),1,1,'') AS DegreeName,  ISNULL(func.SupervisorMark,0)SupervisorMarks, ISNULL(behave.Score,0) SupervisorScore,  ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0) TotalMarks, 
 CASE
  WHEN ISNULL(func.SupervisorMark,0) + ISNULL(behave.Score,0) <= 60 THEN 'Poor'
  WHEN ISNULL(func.SupervisorMark,0) + ISNULL(behave.Score,0) > 60
       AND ISNULL(func.SupervisorMark,0) + ISNULL(behave.Score,0) <= 75 THEN 'Average'
  WHEN ISNULL(func.SupervisorMark,0) + ISNULL(behave.Score,0) > 75
       AND ISNULL(func.SupervisorMark,0) + ISNULL(behave.Score,0) <= 80 THEN 'Good'
  WHEN ISNULL(func.SupervisorMark,0) + ISNULL(behave.Score,0) > 80
       AND ISNULL(func.SupervisorMark,0) + ISNULL(behave.Score,0) <= 90 THEN 'Excellent'
  WHEN ISNULL(func.SupervisorMark,0) + ISNULL(behave.Score,0) > 90 THEN 'Outstanding'
  ELSE ''
END AS FinalStatus,  CASE WHEN appFin.GeneralIncrement=1 THEN 'Yes' ELSE 'No' END GI,  CASE WHEN appFin.SpecialIncrement=1 THEN 'Yes' ELSE 'No' END SI,   CASE WHEN appFin.IsPromotion=1 THEN 'Yes' ELSE 'No' END Promotion,    CASE WHEN appFin.Pip=1 THEN 'Yes' ELSE 'No' END Pip,    CASE WHEN appFin.Other=1 THEN 'Yes' ELSE 'No' END PromotionwithIncrement, SupervisorMark, rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, cat.EmpCategoryName, SG.GradeCode Grade , ST.GrossAmount, ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   CAST((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate    
  from dbo.tblAppraisalDeadlineMaster A  WITH (NOLOCK) 
   left join (SELECT AppraisalDeadLineMasterId, EmpinfoId from tblAppraisalDeadLineDetails group BY AppraisalDeadLineMasterId, EmpinfoId) B on a.AppraisalDeadLineMasterId = b.AppraisalDeadLineMasterId  
  



   --  left join (SELECT tblAppraisalMaster.EmpInfoId,tblAppraisalMaster.AppraisalSelfMasterId, SUM(ISNULL(aa.SupervisorScore,0))SupervisorScore FROM tblAppraisalMaster
   -- left join dbo.tblAppraisalBehaveArea aa on aa.AppraisalSelfMasterId=tblAppraisalMaster.AppraisalSelfMasterId group by tblAppraisalMaster.EmpInfoId,tblAppraisalMaster.AppraisalSelfMasterId
   --)  mdbehabe on mdtMarks.EmpInfoId = b.EmpinfoId 
  
                                     LEFT JOIN dbo.tblEmpGeneralInfo EG ON  B.EmpinfoId = EG.EmpInfoId
                                     LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId
                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 
                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 
								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 
                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana
                                left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId
LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
   LEFT JOIN tblAppraisalMaster appMaster ON appMaster.EmpInfoId = EG.EmpInfoId  and appMaster.FinancialYearId=d.FinancialYearId
      LEFT JOIN ( SELECT  SUM(SupervisorMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON appMaster.AppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SupervisorScore) Score ,
                            AppraisalMasterId
                    FROM    tblAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON appMaster.AppraisalMasterId = behave.AppraisalMasterId

 left JOIN (SELECT EmployeeId,MAX(EffectDate)AS Effectivedate  FROM  dbo.tblPromotionUpgrationHistory   where TypeOfPromotion in ('Promotion','Upgradation')  GROUP BY EmployeeId
 
   )dtEmployeePromotion ON dtEmployeePromotion.EmployeeId=EG.EmpInfoId   


   left JOIN (SELECT EmployeeId,MAX(Effectivedate)AS EffectivedateNew  FROM  dbo.tblEmployeePromotionEntry GROUP BY EmployeeId
 
   )dtEmployeePromotionNew ON dtEmployeePromotionNew.EmployeeId=EG.EmpInfoId    LEFT JOIN (SELECT EmpInfoId,ISNULL(SUM(DATEDIFF(YEAR,ExpFromDate,ExpToDate)),0) Experiece FROM dbo.tblEmpExperience WITH (NOLOCK) WHERE IsActive=1  GROUP BY EmpInfoId) AS EmpExp ON EG.EmpInfoId = EmpExp.EmpInfoId
 LEFT JOIN dbo.tblAppraisalFinalStatus appFin ON appFin.AppraisalMasterId = appMaster.AppraisalMasterId

  LEFT JOIN dbo.tblEmployeeType empType ON empType.EmpTypeId = EG. EmpTypeId


    left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (19,20) 
	 GROUP BY EmpMasterID
 
   )dtScore14_15 ON dtScore14_15.EmpMasterID=EG.EmpMasterCode  
   
   
    left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (17,18) 
	 GROUP BY EmpMasterID
 
   )dtScore15_16 ON dtScore15_16.EmpMasterID=EG.EmpMasterCode 


     left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (15,16) 
	 GROUP BY EmpMasterID
 
   )dtScore16_17 ON dtScore16_17.EmpMasterID=EG.EmpMasterCode 

    left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (13,14) 
	 GROUP BY EmpMasterID
 
   )dtScore17_18 ON dtScore17_18.EmpMasterID=EG.EmpMasterCode 

    left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (1,2) 
	 GROUP BY EmpMasterID
 
   )dtScore18_19 ON dtScore18_19.EmpMasterID=EG.EmpMasterCode 



   left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScoreScore21_22 FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (11,12) 
	 GROUP BY EmpMasterID
 
   )dtScore21_22 ON dtScore21_22.EmpMasterID=EG.EmpMasterCode 

                              
 LEFT JOIN (SELECT EmpInfoId, ISNULL(COUNT(*),0) DisCout FROM dbo.tblDiciplinaryAction WHERE EffectiveDate BETWEEN  CONVERT(DATE,dateadd(yy,-3,datediff(d,0,getdate()))) AND CONVERT(DATE,dateadd(yy,0,datediff(d,0,getdate())))   GROUP BY EmpInfoId) tbldis ON  tbldis.EmpInfoId = EG.EmpInfoId
   inner JOIN   tblEmpAllRefference reff  ON EG.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 


    
                                where (a.IsDelete is null or a.IsDelete = 0) and     B.EmpinfoId    IN (SELECT tblAppraisalMaster.EmpInfoId FROM tblAppraisalMaster  LEFT JOIN tblFinancialYear y ON tblAppraisalMaster.FinancialYearId = y.FinancialYearId WHERE tblAppraisalMaster.SelfApprove='Approved'  AND    tblAppraisalMaster.CurrentStatus='Approved' and y.FinancialYearDesc='" + FinYear + "' )       and  EG.IsActive=1  and     reff.ShowCompany in (ComAssain) " + param2 + " ) tbl   order by EmpMasterCode asc ";
                //+ param 
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }



        public DataTable rptGetApprisalSetupListNotComplete(string param, string param2, string fyer)
        //string param
        {

            try
            {
                string query = @"select distinct * from (SELECT  '' Subject, '' DeclarationDate , EG.TinNo, empType.EmpType,  EG.ContractPeriod, EG.FatherName, EG.MotherName, EG.PersonalMobile, EG.EmergencyContactNumber, EG.PersonalEmail, EG.OfficialEmail, CAST(ISNULL( EmpExp.Experiece ,0)+  ISNULL((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) ,0) AS NVARCHAR(max))+ ' Years, '+ CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'   as  EmpExperiece , DP.DepartmentName DepartmentName, DS.Designation,  EG.EmpName EmpName, d.FinancialYearDesc,  EG.SMCOldCode, EG.EmpMasterCode,  ISNULL(dtScore14_15.HisScore,0) HisScore14_15 ,  ISNULL(dtScore15_16.HisScore,0) HisScore15_16 ,  ISNULL(dtScore16_17.HisScore,0) HisScore16_17,  ISNULL(dtScore17_18.HisScore,0) HisScore17_18,  ISNULL(dtScore18_19.HisScore,0) HisScore18_19,ISNULL(0,0) HisScore19_20, ISNULL(dtScore21_22.HisScoreScore21_22,0) HisScore21_22, ISNULL(FORMAT(dtEmployeePromotion.Effectivedate, 'dd-MMM-yyyy'),dtEmployeePromotionNew.EffectivedateNew)
    
    AS LastPromotion,    CAST((DATEDIFF(year, EG.DateOfBirth, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  EmpAge,  CASE when ISNULL(tbldis.DisCout,0)=0 THEN 'No' ELSE 'Yes' end DiciplinaryCout,  '' AS DegreeName,  ISNULL(func.SupervisorMark,0)SupervisorMarks, ISNULL(behave.Score,0) SupervisorScore,  ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0) TotalMarks, 
  case when ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0)<= 60 Then 'Poor'

   when ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0)>= 61 and ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0)<= 75 Then 'Average'
   
    when ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0)>= 76 and ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0)<= 80 Then 'Good'
	
	 when ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0)>= 81 and ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0)<= 90 Then 'Excellent'
	
	 when ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0)>= 91 Then 'Outstanding'  else '' end   FinalStatus,  CASE WHEN appFin.GeneralIncrement=1 THEN 'Yes' ELSE 'No' END GI,  CASE WHEN appFin.SpecialIncrement=1 THEN 'Yes' ELSE 'No' END SI,   CASE WHEN appFin.IsPromotion=1 THEN 'Yes' ELSE 'No' END Promotion,    CASE WHEN appFin.Pip=1 THEN 'Yes' ELSE 'No' END Pip,    CASE WHEN appFin.Other=1 THEN 'Yes' ELSE 'No' END PromotionwithIncrement, ISNULL(SupervisorMark,0) SupervisorMark, rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, cat.EmpCategoryName, SG.GradeCode Grade , ST.GrossAmount, ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   CAST((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate    
  from dbo.tblAppraisalDeadlineMaster A  WITH (NOLOCK) 
   left join (SELECT AppraisalDeadLineMasterId, EmpinfoId from tblAppraisalDeadLineDetails group BY AppraisalDeadLineMasterId, EmpinfoId) B on a.AppraisalDeadLineMasterId = b.AppraisalDeadLineMasterId  
  



   --  left join (SELECT tblAppraisalMaster.EmpInfoId,tblAppraisalMaster.AppraisalSelfMasterId, SUM(ISNULL(aa.SupervisorScore,0))SupervisorScore FROM tblAppraisalMaster
   -- left join dbo.tblAppraisalBehaveArea aa on aa.AppraisalSelfMasterId=tblAppraisalMaster.AppraisalSelfMasterId group by tblAppraisalMaster.EmpInfoId,tblAppraisalMaster.AppraisalSelfMasterId
   --)  mdbehabe on mdtMarks.EmpInfoId = b.EmpinfoId 
  
                                     LEFT JOIN dbo.tblEmpGeneralInfo EG ON  B.EmpinfoId = EG.EmpInfoId
                                     LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId
                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 
                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 
								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 
                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana
                                left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId
LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
   LEFT JOIN tblAppraisalMaster appMaster ON appMaster.EmpInfoId = EG.EmpInfoId  and appMaster.FinancialYearId=d.FinancialYearId
      LEFT JOIN ( SELECT  SUM(SupervisorMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON appMaster.AppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SupervisorScore) Score ,
                            AppraisalMasterId
                    FROM    tblAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON appMaster.AppraisalMasterId = behave.AppraisalMasterId

 left JOIN (SELECT EmployeeId,MAX(EffectDate)AS Effectivedate  FROM  dbo.tblPromotionUpgrationHistory   where TypeOfPromotion in ('Promotion','Upgradation') GROUP BY EmployeeId
 
   )dtEmployeePromotion ON dtEmployeePromotion.EmployeeId=EG.EmpInfoId   


   left JOIN (SELECT EmployeeId,MAX(Effectivedate)AS EffectivedateNew  FROM  dbo.tblEmployeePromotionEntry GROUP BY EmployeeId
 
   )dtEmployeePromotionNew ON dtEmployeePromotionNew.EmployeeId=EG.EmpInfoId    LEFT JOIN (SELECT EmpInfoId,ISNULL(SUM(DATEDIFF(YEAR,ExpFromDate,ExpToDate)),0) Experiece FROM dbo.tblEmpExperience WITH (NOLOCK) WHERE IsActive=1  GROUP BY EmpInfoId) AS EmpExp ON EG.EmpInfoId = EmpExp.EmpInfoId
 LEFT JOIN dbo.tblAppraisalFinalStatus appFin ON appFin.AppraisalMasterId = appMaster.AppraisalMasterId

  LEFT JOIN dbo.tblEmployeeType empType ON empType.EmpTypeId = EG. EmpTypeId


    left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (19,20) 
	 GROUP BY EmpMasterID
 
   )dtScore14_15 ON dtScore14_15.EmpMasterID=EG.EmpMasterCode  
   
   
    left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (17,18) 
	 GROUP BY EmpMasterID
 
   )dtScore15_16 ON dtScore15_16.EmpMasterID=EG.EmpMasterCode 


     left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (15,16) 
	 GROUP BY EmpMasterID
 
   )dtScore16_17 ON dtScore16_17.EmpMasterID=EG.EmpMasterCode 

    left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (13,14) 
	 GROUP BY EmpMasterID
 
   )dtScore17_18 ON dtScore17_18.EmpMasterID=EG.EmpMasterCode 

    left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (1,2) 
	 GROUP BY EmpMasterID
 
   )dtScore18_19 ON dtScore18_19.EmpMasterID=EG.EmpMasterCode 



   left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScoreScore21_22 FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (11,12) 
	 GROUP BY EmpMasterID
 
   )dtScore21_22 ON dtScore21_22.EmpMasterID=EG.EmpMasterCode 

                              
 LEFT JOIN (SELECT EmpInfoId, ISNULL(COUNT(*),0) DisCout FROM dbo.tblDiciplinaryAction WHERE EffectiveDate BETWEEN  CONVERT(DATE,dateadd(yy,-3,datediff(d,0,getdate()))) AND CONVERT(DATE,dateadd(yy,0,datediff(d,0,getdate())))   GROUP BY EmpInfoId) tbldis ON  tbldis.EmpInfoId = EG.EmpInfoId
  
                                where (a.IsDelete is null or a.IsDelete = 0) and        B.EmpinfoId  not  IN (SELECT amass.EmpInfoId FROM tblAppraisalMaster amass
								  left join tblFinancialYear fd on amass.FinancialYearId = fd.FinancialYearId
								 WHERE amass.CurrentStatus='Approved' and fd.FinancialYearDesc ='" + fyer + "' )      " + param + @"   	union all 


								
SELECT ''  Subject,  ''  DeclarationDate , EG.TinNo, empType.EmpType,  EG.ContractPeriod, EG.FatherName, EG.MotherName, EG.PersonalMobile, EG.EmergencyContactNumber, EG.PersonalEmail, EG.OfficialEmail, CAST(ISNULL( EmpExp.Experiece ,0)+  ISNULL((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) ,0) AS NVARCHAR(max))+ ' Years, '+ CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'   as  EmpExperiece , DP.DepartmentName DepartmentName, DS.Designation,  EG.EmpName EmpName, d.FinancialYearDesc,  EG.SMCOldCode, EG.EmpMasterCode,  ISNULL(dtScore14_15.HisScore,0) HisScore14_15 ,  ISNULL(dtScore15_16.HisScore,0) HisScore15_16 ,  ISNULL(dtScore16_17.HisScore,0) HisScore16_17,  ISNULL(dtScore17_18.HisScore,0) HisScore17_18,  ISNULL(dtScore18_19.HisScore,0) HisScore18_19,ISNULL(0,0) HisScore19_20, ISNULL(dtScore21_22.HisScoreScore21_22,0) HisScore21_22, ISNULL(FORMAT(dtEmployeePromotion.Effectivedate, 'dd-MMM-yyyy'),dtEmployeePromotionNew.EffectivedateNew)
    
    AS LastPromotion,    CAST((DATEDIFF(year, EG.DateOfBirth, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  EmpAge,  CASE when ISNULL(tbldis.DisCout,0)=0 THEN 'No' ELSE 'Yes' end DiciplinaryCout,''AS DegreeName,  ISNULL(func.SupervisorMark,0)SupervisorMarks, ISNULL(behave.Score,0) SupervisorScore,  ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0) TotalMarks, 
  case when ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0)<= 60 Then 'Poor'

   when ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0)>= 61 and ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0)<= 70 Then 'Average'
   
    when ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0)>= 71 and ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0)<= 80 Then 'Good'
	
	 when ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0)>= 81 and ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0)<= 90 Then 'Excellent'
	
	 when ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0)>= 91 Then 'Outstanding'  else '' end   FinalStatus,  CASE WHEN appFin.GeneralIncrement=1 THEN 'Yes' ELSE 'No' END GI,  CASE WHEN appFin.SpecialIncrement=1 THEN 'Yes' ELSE 'No' END SI,   CASE WHEN appFin.IsPromotion=1 THEN 'Yes' ELSE 'No' END Promotion,    CASE WHEN appFin.Pip=1 THEN 'Yes' ELSE 'No' END Pip,    CASE WHEN appFin.Other=1 THEN 'Yes' ELSE 'No' END PromotionwithIncrement, isnull(SupervisorMark,0) SupervisorMark , rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, cat.EmpCategoryName, SG.GradeCode Grade , ST.GrossAmount, ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   CAST((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate    
  from dbo.tblAppraisalDeadlineMaster A  WITH (NOLOCK) 
   left join (SELECT AppraisalDeadLineMasterId, EmpinfoId from tblAppraisalDeadLineDetails group BY AppraisalDeadLineMasterId, EmpinfoId) B on a.AppraisalDeadLineMasterId = b.AppraisalDeadLineMasterId  
  



   --  left join (SELECT tblAppraisalMaster.EmpInfoId,tblAppraisalMaster.AppraisalSelfMasterId, SUM(ISNULL(aa.SupervisorScore,0))SupervisorScore FROM tblAppraisalMaster
   -- left join dbo.tblAppraisalBehaveArea aa on aa.AppraisalSelfMasterId=tblAppraisalMaster.AppraisalSelfMasterId group by tblAppraisalMaster.EmpInfoId,tblAppraisalMaster.AppraisalSelfMasterId
   --)  mdbehabe on mdtMarks.EmpInfoId = b.EmpinfoId 
  
                                     LEFT JOIN dbo.tblEmpGeneralInfo EG ON  B.EmpinfoId = EG.EmpInfoId
                                     LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId
                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 
                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 
								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 
                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana
                                left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId
LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
   LEFT JOIN tblAppraisalMaster appMaster ON appMaster.EmpInfoId = EG.EmpInfoId  and appMaster.FinancialYearId=d.FinancialYearId
      LEFT JOIN ( SELECT  SUM(SupervisorMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON appMaster.AppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SupervisorScore) Score ,
                            AppraisalMasterId
                    FROM    tblAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON appMaster.AppraisalMasterId = behave.AppraisalMasterId

 left JOIN (SELECT EmployeeId,MAX(EffectDate)AS Effectivedate  FROM  dbo.tblPromotionUpgrationHistory   where TypeOfPromotion in ('Promotion','Upgradation')  GROUP BY EmployeeId
 
   )dtEmployeePromotion ON dtEmployeePromotion.EmployeeId=EG.EmpInfoId   


   left JOIN (SELECT EmployeeId,MAX(Effectivedate)AS EffectivedateNew  FROM  dbo.tblEmployeePromotionEntry GROUP BY EmployeeId
 
   )dtEmployeePromotionNew ON dtEmployeePromotionNew.EmployeeId=EG.EmpInfoId    LEFT JOIN (SELECT EmpInfoId,ISNULL(SUM(DATEDIFF(YEAR,ExpFromDate,ExpToDate)),0) Experiece FROM dbo.tblEmpExperience WITH (NOLOCK) WHERE IsActive=1  GROUP BY EmpInfoId) AS EmpExp ON EG.EmpInfoId = EmpExp.EmpInfoId
 LEFT JOIN dbo.tblAppraisalFinalStatus appFin ON appFin.AppraisalMasterId = appMaster.AppraisalMasterId

  LEFT JOIN dbo.tblEmployeeType empType ON empType.EmpTypeId = EG. EmpTypeId


    left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (19,20) 
	 GROUP BY EmpMasterID
 
   )dtScore14_15 ON dtScore14_15.EmpMasterID=EG.EmpMasterCode  
   
   
    left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (17,18) 
	 GROUP BY EmpMasterID
 
   )dtScore15_16 ON dtScore15_16.EmpMasterID=EG.EmpMasterCode 


     left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (15,16) 
	 GROUP BY EmpMasterID
 
   )dtScore16_17 ON dtScore16_17.EmpMasterID=EG.EmpMasterCode 

    left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (13,14) 
	 GROUP BY EmpMasterID
 
   )dtScore17_18 ON dtScore17_18.EmpMasterID=EG.EmpMasterCode 

    left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (1,2) 
	 GROUP BY EmpMasterID
 
   )dtScore18_19 ON dtScore18_19.EmpMasterID=EG.EmpMasterCode 



   left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScoreScore21_22 FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (11,12) 
	 GROUP BY EmpMasterID
 
   )dtScore21_22 ON dtScore21_22.EmpMasterID=EG.EmpMasterCode 

                              
 LEFT JOIN (SELECT EmpInfoId, ISNULL(COUNT(*),0) DisCout FROM dbo.tblDiciplinaryAction WHERE EffectiveDate BETWEEN  CONVERT(DATE,dateadd(yy,-3,datediff(d,0,getdate()))) AND CONVERT(DATE,dateadd(yy,0,datediff(d,0,getdate())))   GROUP BY EmpInfoId) tbldis ON  tbldis.EmpInfoId = EG.EmpInfoId
   inner JOIN   tblEmpAllRefference reff  ON EG.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 


    
                                where (a.IsDelete is null or a.IsDelete = 0) and         B.EmpinfoId  not  IN (SELECT amass.EmpInfoId FROM tblAppraisalMaster amass
								  left join tblFinancialYear fd on amass.FinancialYearId = fd.FinancialYearId
								 WHERE amass.CurrentStatus='Approved' and fd.FinancialYearDesc ='" + fyer + "' )          and  EG.IsActive=1  and     reff.ShowCompany in (ComAssain) " + param2 + " ) tbl   order by EmpMasterCode asc ";
                //+ param 
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }


        public DataTable rptGetSurveySetupListNOTINNN(string param)
        //string param
        {

            try
            {
                string query = @"SELECT rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   CAST((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , a.SurveyMasterId ,  d.FinancialYearDesc ,* 
  from tblSurveyMaster A  WITH (NOLOCK) 
   left join (SELECT SurveyMasterId, EmployeeId from tblSurveyParticipate group BY SurveyMasterId, EmployeeId) B on a.SurveyMasterId = b.SurveyMasterId  
                                     LEFT JOIN dbo.tblEmpGeneralInfo EG ON  B.EmployeeId = EG.EmpInfoId
                                     LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId
                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 
                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 
								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 
                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana
                                left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId
LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
                                where (a.IsDelete is null or a.IsDelete = 0) and     B.EmployeeId  Not  IN (SELECT tblSurveySubmitMaster.EmployeeId FROM tblSurveySubmitMaster )   " + param;
                //+ param
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }



        public DataTable rptGetApprisalSetupListNOTINNN(string param)
        //string param
        {

            try
            {
                string query = @"SELECT EG.EmpMasterCode,  ISNULL(dtScore14_15.HisScore,0) HisScore14_15 ,  ISNULL(dtScore15_16.HisScore,0) HisScore15_16 ,  ISNULL(dtScore16_17.HisScore,0) HisScore16_17,  ISNULL(dtScore17_18.HisScore,0) HisScore17_18,  ISNULL(dtScore18_19.HisScore,0) HisScore18_19,ISNULL(0,0) HisScore19_20, ISNULL(dtScore21_22.HisScoreScore21_22,0) HisScore21_22, ISNULL(FORMAT(dtEmployeePromotion.Effectivedate, 'dd-MMM-yyyy'),dtEmployeePromotionNew.EffectivedateNew)
    
    AS LastPromotion,    CAST((DATEDIFF(year, EG.DateOfBirth, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfBirth)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfBirth, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  EmpAge,  CASE when ISNULL(tbldis.DisCout,0)=0 THEN 'No' ELSE 'Yes' end DiciplinaryCout,  STUFF( (SELECT CONCAT('             '+ CHAR(13), '('+CAST(ROW_NUMBER() Over (Order by AppraisalTrainingId) AS NVARCHAR(max))+')  '+ mm.TrainingNeeds + CHAR(13), '') FROM tblAppraisalTrainingNeeds mm (NOLOCK)  WHERE appMaster.AppraisalMasterId=mm.AppraisalMasterId ORDER BY mm.AppraisalTrainingId FOR XML PATH ('') ),1,1,'') AS DegreeName,  ISNULL(func.SupervisorMark,0)SupervisorMarks, ISNULL(behave.Score,0) SupervisorScore,  ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0) TotalMarks, appFin.FinalStatus,  CASE WHEN appFin.GeneralIncrement=1 THEN 'Yes' ELSE 'No' END GI,  CASE WHEN appFin.SpecialIncrement=1 THEN 'Yes' ELSE 'No' END SI,   CASE WHEN appFin.IsPromotion=1 THEN 'Yes' ELSE 'No' END Promotion,    CASE WHEN appFin.Pip=1 THEN 'Yes' ELSE 'No' END Pip,    CASE WHEN appFin.Other=1 THEN 'Yes' ELSE 'No' END PromotionwithIncrement, SupervisorMark, rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   CAST((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate  ,* 
  from dbo.tblAppraisalDeadlineMaster A  WITH (NOLOCK) 
   left join (SELECT AppraisalDeadLineMasterId, EmpinfoId from tblAppraisalDeadLineDetails group BY AppraisalDeadLineMasterId, EmpinfoId) B on a.AppraisalDeadLineMasterId = b.AppraisalDeadLineMasterId  
   
  
                                     LEFT JOIN dbo.tblEmpGeneralInfo EG ON  B.EmpinfoId = EG.EmpInfoId
                                     LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId
                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 
                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 
								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 
                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana
                                left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId
LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
   LEFT JOIN tblAppraisalMaster appMaster ON appMaster.EmpInfoId = EG.EmpInfoId  and appMaster.FinancialYearId=d.FinancialYearId
      LEFT JOIN ( SELECT  SUM(SupervisorMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON appMaster.AppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SupervisorScore) Score ,
                            AppraisalMasterId
                    FROM    tblAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON appMaster.AppraisalMasterId = behave.AppraisalMasterId

 left JOIN (SELECT EmployeeId,MAX(EffectDate)AS Effectivedate  FROM  dbo.tblPromotionUpgrationHistory GROUP BY EmployeeId
 
   )dtEmployeePromotion ON dtEmployeePromotion.EmployeeId=EG.EmpInfoId   


   left JOIN (SELECT EmployeeId,MAX(Effectivedate)AS EffectivedateNew  FROM  dbo.tblEmployeePromotionEntry GROUP BY EmployeeId
 
   )dtEmployeePromotionNew ON dtEmployeePromotionNew.EmployeeId=EG.EmpInfoId    
 LEFT JOIN dbo.tblAppraisalFinalStatus appFin ON appFin.AppraisalMasterId = appMaster.AppraisalMasterId

  LEFT JOIN dbo.tblEmployeeType empType ON empType.EmpTypeId = EG. EmpTypeId
   left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (19,20) 
	 GROUP BY EmpMasterID
 
   )dtScore14_15 ON dtScore14_15.EmpMasterID=EG.EmpMasterCode  
   
   
    left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (17,18) 
	 GROUP BY EmpMasterID
 
   )dtScore15_16 ON dtScore15_16.EmpMasterID=EG.EmpMasterCode 


     left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (15,16) 
	 GROUP BY EmpMasterID
 
   )dtScore16_17 ON dtScore16_17.EmpMasterID=EG.EmpMasterCode 

    left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (13,14) 
	 GROUP BY EmpMasterID
 
   )dtScore17_18 ON dtScore17_18.EmpMasterID=EG.EmpMasterCode 

    left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScore FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (1,2) 
	 GROUP BY EmpMasterID
 
   )dtScore18_19 ON dtScore18_19.EmpMasterID=EG.EmpMasterCode 



   left JOIN (SELECT EmpMasterID,SUM(Score)AS HisScoreScore21_22 FROM  dbo.tblHistoryAppraisalScore
	WHERE HistoryAppraisalScoreId IS NOT NULL   AND FinId IN (11,12) 
	 GROUP BY EmpMasterID
 
   )dtScore21_22 ON dtScore21_22.EmpMasterID=EG.EmpMasterCode 

                              
 LEFT JOIN (SELECT EmpInfoId, ISNULL(COUNT(*),0) DisCout FROM dbo.tblDiciplinaryAction WHERE EffectiveDate BETWEEN  CONVERT(DATE,dateadd(yy,-3,datediff(d,0,getdate()))) AND CONVERT(DATE,dateadd(yy,0,datediff(d,0,getdate())))   GROUP BY EmpInfoId) tbldis ON  tbldis.EmpInfoId = EG.EmpInfoId
  
                                where (a.IsDelete is null or a.IsDelete = 0)  and     B.EmpinfoId  not  IN (SELECT tblAppraisalMaster.EmpInfoId FROM tblAppraisalMaster WHERE tblAppraisalMaster.SelfApprove='Approved' )    " + param;
                //+ param
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }
    }
}
