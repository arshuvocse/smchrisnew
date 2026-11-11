using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;

namespace DAL.Employee_DAL
{
  public   class EmpExperienceDAL
    {
      ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
      public Int32 SaveJobReqKeyRespon(tblEmpExperience ExpDao)
      {
          List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


          aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", ExpDao.EmpInfoId));

          aSqlParameterlist.Add(new SqlParameter("@ExpCompany", ExpDao.ExpCompany ?? (object)DBNull.Value));
          aSqlParameterlist.Add(new SqlParameter("@ExpContactPerson", ExpDao.ExpContactPerson ?? (object)DBNull.Value));
          aSqlParameterlist.Add(new SqlParameter("@ExpAddress", ExpDao.ExpAddress ?? (object)DBNull.Value));
          aSqlParameterlist.Add(new SqlParameter("@ExpNatureofBusiness", ExpDao.ExpNatureofBusiness ?? (object)DBNull.Value));
          aSqlParameterlist.Add(new SqlParameter("@ExpJobType", ExpDao.ExpJobType ?? (object)DBNull.Value));
          aSqlParameterlist.Add(new SqlParameter("@ExpLeavingSalary", ExpDao.ExpLeavingSalary ?? (object)DBNull.Value));
          aSqlParameterlist.Add(new SqlParameter("@ExpFromDate", ExpDao.ExpFromDate ?? (object)DBNull.Value));
          aSqlParameterlist.Add(new SqlParameter("@ExpToDate", ExpDao.ExpToDate ?? (object)DBNull.Value));
          aSqlParameterlist.Add(new SqlParameter("@ExpLastJob", ExpDao.ExpLastJob ?? (object)DBNull.Value));
          aSqlParameterlist.Add(new SqlParameter("@ExpDesignation", ExpDao.ExpDesignation ?? (object)DBNull.Value));
          aSqlParameterlist.Add(new SqlParameter("@ExpJobDescription", ExpDao.ExpJobDescription ?? (object)DBNull.Value));
          aSqlParameterlist.Add(new SqlParameter("@ExpTelNo", ExpDao.ExpTelNo ?? (object)DBNull.Value));
          aSqlParameterlist.Add(new SqlParameter("@ExpRemarks", ExpDao.ExpRemarks ?? (object)DBNull.Value));


          string insertQuery = @"INSERT INTO [dbo].[tblEmpExperience]
           ([EmpInfoId]
           ,[ExpContactPerson]
           ,[ExpCompany]
           ,[ExpAddress]
           ,[ExpTelNo]
           ,[ExpNatureofBusiness]
           ,[ExpDesignation]
           ,[ExpJobDescription]
           ,[ExpFromDate]
           ,[ExpToDate]
           ,[ExpJobType]
           ,[ExpLastJob]
           ,[ExpRemarks]
           ,[ExpLeavingSalary]
           ,[IsActive])
     VALUES
           (@EmpInfoId 
           ,@ExpContactPerson 
           ,@ExpCompany 
           ,@ExpAddress 
           ,@ExpTelNo 
           ,@ExpNatureofBusiness 
           ,@ExpDesignation 
           ,@ExpJobDescription 
           ,@ExpFromDate 
           ,@ExpToDate 
           ,@ExpJobType 
           ,@ExpLastJob 
           ,@ExpRemarks 
           ,@ExpLeavingSalary 
           ,1)";

          return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");

      }


      public DataTable GetDTEmpExperienceByEmpId(int mid)
      {
          var aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@mid", mid));
          string query = @"SELECT FORMAT(e.ExpFromDate,'dd-MMM-yyyy') AS ExpFromDate, FORMAT(e.ExpToDate,'dd-MMM-yyyy') AS ExpToDate,  
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



      public DataTable ReportAllGetDTEmpExperience(string Param)
      {

          string query = @"SELECT   cast((DATEDIFF(m,  e.ExpFromDate, e.ExpToDate)/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m,  e.ExpFromDate, e.ExpToDate)%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d,  e.ExpFromDate,e.ExpToDate)%12) as varchar) + ' day'  TotalExperience, ST.GrossAmount,  ISNULL(ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL),'') ParentsInfo , rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , FORMAT(e.ExpFromDate,'dd-MMM-yyyy') AS ExpFromDate, FORMAT(e.ExpToDate,'dd-MMM-yyyy') AS ExpToDate,  
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
       e.IsActive,* FROM dbo.tblEmpExperience e WITH (NOLOCK)

	   LEFT JOIN dbo.tblEmpGeneralInfo EG ON e.EmpInfoId = EG.EmpInfoId
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

                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana
LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
WHERE e.IsActive=1  " + Param + "    ORDER BY EG.EmpMasterCode ASC";
          return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
      }


      public DataTable ReportAllGetDTEmpProject(string Param)
      {

          string query = @"  SELECT rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate ,* FROM tblEmployeeWiseProjectAllocationMaster mas

LEFT JOIN tblEmployeeWiseProjectAllocationDetail  dtl on mas.EmpWiseProjectID=dtl.EmployeeWiseProjectAllocationMasterId 
 LEFT JOIN dbo.tblEmpGeneralInfo EG ON mas.EmpInfoId = EG.EmpInfoId
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

                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana
LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
WHERE    mas.EmpWiseProjectID IS NOT NULL  " + Param + "    ORDER BY EG.EmpMasterCode ASC";
          return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
      }
    }
}
