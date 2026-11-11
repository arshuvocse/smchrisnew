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
using DAO.HRIS_DAO;

namespace DAL.Appraisal
{
  public  class KPISETUPListDAL
    {
      ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
      public void GetCompanyListShortNameIntoDropdown(DropDownList ddl)
      {

          string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
          aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
      }

      public void LoadFinancialYearForSearch(DropDownList ddl, string comapnyId)
      {
          string query = @"SELECT FINY.FinancialYearId,
                            FINY.FinancialYearDesc FROM dbo.tblFinancialYear AS FINY 
                            WHERE FINY.Status = 'Active' AND FINY.CompanyId ='" + comapnyId + "'";

          aCommonInternalDal.LoadDropDownValue(ddl, "FinancialYearDesc", "FinancialYearId", query, "HRDB");
      }

      public DataTable GetKpiSetupList(string param)
      //string param
      {

          try
          {
              string query = @"select a.KPIDeadLineMasterId , c.CompanyName ,d.FinancialYearDesc , b.TotalEmployee  , CONVERT(nvarchar (11),a.EntryDate , 106)EntryDate ,a.EntryBy, CONVERT(nvarchar (11),a.DeclarationDate , 106)DeclarationDate,*   from tblKpiDeadlineMaster A 
                                    left join tblCompanyInfo c on a.CompanyId = c.CompanyId
                                    left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId
                                    
                                    left join (select count(EmpinfoId)TotalEmployee , KPIDeadLineMasterId from tblKPIDeadLineDetails group by KPIDeadLineMasterId) B on a.KPIDeadLineMasterId = b.KPIDeadLineMasterId  
                                    
                                    where (a.IsDelete is null or a.IsDelete = 0) " + param;
              //+ param
              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception exception)
          {

              throw exception;
          }
      }



      public DataTable rptGetKpiSetupListINNNN(string param,string param_SP, string FinYear )
      //string param
      {

          try
          {
              string query = @"select distinct * from (SELECT Eg.DateOfJoin, A.Subject,  EG.EmpMasterCode, Eg.SMCOldCode, Eg.EmpName, DS.Designation, DP.DepartmentName, 
--
 rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , a.KPIDeadLineMasterId ,  d.FinancialYearDesc , CONVERT(nvarchar (11),a.DeclarationDate , 106)DeclarationDate  
  from tblKpiDeadlineMaster A  WITH (NOLOCK) 
   left join (SELECT KPIDeadLineMasterId, EmpinfoId from tblKPIDeadLineDetails group BY EmpinfoId,  KPIDeadLineMasterId) B on a.KPIDeadLineMasterId = b.KPIDeadLineMasterId  
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
                                where (a.IsDelete is null or a.IsDelete = 0) and     B.EmpinfoId     IN (SELECT tblAppraisalSelfMaster.EmpInfoId FROM tblAppraisalSelfMaster    LEFT JOIN tblFinancialYear y ON tblAppraisalSelfMaster.FinancialYearId = y.FinancialYearId  where   y.FinancialYearDesc='" + FinYear + "' ) " + param + @"  Union all SELECT Eg.DateOfJoin, A.Subject,  EG.EmpMasterCode, Eg.SMCOldCode, Eg.EmpName, DS.Designation, DP.DepartmentName,  
--
 rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , a.KPIDeadLineMasterId ,  d.FinancialYearDesc , CONVERT(nvarchar (11),a.DeclarationDate , 106)DeclarationDate 
  from tblKpiDeadlineMaster A  WITH (NOLOCK) 
   left join (SELECT KPIDeadLineMasterId, EmpinfoId from tblKPIDeadLineDetails group BY EmpinfoId,  KPIDeadLineMasterId) B on a.KPIDeadLineMasterId = b.KPIDeadLineMasterId  
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
inner JOIN   tblEmpAllRefference reff  ON EG.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
                                where  EG.IsActive=1  and     reff.ShowCompany in (ComAssain)  and (a.IsDelete is null or a.IsDelete = 0)  and    B.EmpinfoId     IN (SELECT tblAppraisalSelfMaster.EmpInfoId FROM tblAppraisalSelfMaster    LEFT JOIN tblFinancialYear y ON tblAppraisalSelfMaster.FinancialYearId = y.FinancialYearId  where   y.FinancialYearDesc='" + FinYear + "' ) " + param_SP + ") tbl  order by EmpMasterCode";
              //+ param
              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception exception)
          {

              throw exception;
          }
      }
      public DataTable rptGetKpiSetupListNOTINNN(string param, string param_SP, string FinYear)
      //string param
      {

          try
          {
              string query = @"select distinct * from (SELECT Eg.DateOfJoin, A.Subject,  EG.EmpMasterCode, Eg.SMCOldCode, Eg.EmpName, DS.Designation, DP.DepartmentName, 
--
 rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , a.KPIDeadLineMasterId ,  d.FinancialYearDesc , CONVERT(nvarchar (11),a.DeclarationDate , 106)DeclarationDate  
  from tblKpiDeadlineMaster A  WITH (NOLOCK) 
   left join (SELECT KPIDeadLineMasterId, EmpinfoId from tblKPIDeadLineDetails group BY EmpinfoId,  KPIDeadLineMasterId) B on a.KPIDeadLineMasterId = b.KPIDeadLineMasterId  
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
                                where (a.IsDelete is null or a.IsDelete = 0) and     B.EmpinfoId  not   IN (SELECT tblAppraisalSelfMaster.EmpInfoId FROM tblAppraisalSelfMaster    LEFT JOIN tblFinancialYear y ON tblAppraisalSelfMaster.FinancialYearId = y.FinancialYearId  where   y.FinancialYearDesc='" + FinYear + "' ) " + param + @"  Union all SELECT Eg.DateOfJoin, A.Subject,  EG.EmpMasterCode, Eg.SMCOldCode, Eg.EmpName, DS.Designation, DP.DepartmentName,  
--
 rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , a.KPIDeadLineMasterId ,  d.FinancialYearDesc , CONVERT(nvarchar (11),a.DeclarationDate , 106)DeclarationDate 
  from tblKpiDeadlineMaster A  WITH (NOLOCK) 
   left join (SELECT KPIDeadLineMasterId, EmpinfoId from tblKPIDeadLineDetails group BY EmpinfoId,  KPIDeadLineMasterId) B on a.KPIDeadLineMasterId = b.KPIDeadLineMasterId  
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
                            inner JOIN   tblEmpAllRefference reff  ON EG.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
                                where  EG.IsActive=1  and     reff.ShowCompany in (ComAssain)  and (a.IsDelete is null or a.IsDelete = 0)  and      B.EmpinfoId  not   IN (SELECT tblAppraisalSelfMaster.EmpInfoId FROM tblAppraisalSelfMaster    LEFT JOIN tblFinancialYear y ON tblAppraisalSelfMaster.FinancialYearId = y.FinancialYearId  where   y.FinancialYearDesc='" + FinYear + "' ) " + param_SP + ") tbl  order by EmpMasterCode";
              //+ param
              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception exception)
          {

              throw exception;
          }
      }
//       public DataTable rptGetKpiSetupListNOTINNN(string param, string param_SP, string FinYear)
       
//      {

//          try
//          {
//              string query = @"select distinct * from (SELECT Eg.DateOfJoin, A.Subject,  EG.EmpMasterCode, Eg.SMCOldCode, Eg.EmpName, DS.Designation, DP.DepartmentName, 
//--
// rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
//cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
//JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
//EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
//EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
//  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
//  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
//   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
//       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
//	   
//	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , a.KPIDeadLineMasterId ,  d.FinancialYearDesc , CONVERT(nvarchar (11),a.DeclarationDate , 106)DeclarationDate,* 
//  from tblKpiDeadlineMaster A  WITH (NOLOCK) 
//   left join (SELECT KPIDeadLineMasterId, EmpinfoId from tblKPIDeadLineDetails group BY EmpinfoId,  KPIDeadLineMasterId) B on a.KPIDeadLineMasterId = b.KPIDeadLineMasterId  
//                                     LEFT JOIN dbo.tblEmpGeneralInfo EG ON  B.EmpinfoId = EG.EmpInfoId
//                                     LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
//                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
//                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
//                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
//                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId
//                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
//                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
//                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
//                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
//                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
//                               LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
//                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
//                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 
//                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
//                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 
//								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
//                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 
//                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
//                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana
//                                left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId
//LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
//                                where (a.IsDelete is null or a.IsDelete = 0) and     B.EmpinfoId   not  IN (SELECT tblAppraisalSelfMaster.EmpInfoId FROM tblAppraisalSelfMaster    LEFT JOIN tblFinancialYear y ON tblAppraisalSelfMaster.FinancialYearId = y.FinancialYearId  where   y.FinancialYearDesc='" + FinYear + "' ) " + param + @"  Union all SELECT  Eg.DateOfJoin, A.Subject,  EG.EmpMasterCode, Eg.SMCOldCode, Eg.EmpName, DS.Designation, DP.DepartmentName,  
//--
// rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
//cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
//JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
//EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
//EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
//  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
//  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
//   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
//       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
//	   
//	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , a.KPIDeadLineMasterId ,  d.FinancialYearDesc , CONVERT(nvarchar (11),a.DeclarationDate , 106)DeclarationDate,* 
//  from tblKpiDeadlineMaster A  WITH (NOLOCK) 
//   left join (SELECT KPIDeadLineMasterId, EmpinfoId from tblKPIDeadLineDetails group BY EmpinfoId,  KPIDeadLineMasterId) B on a.KPIDeadLineMasterId = b.KPIDeadLineMasterId  
//                                     LEFT JOIN dbo.tblEmpGeneralInfo EG ON  B.EmpinfoId = EG.EmpInfoId
//                                     LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
//                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
//                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
//                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
//                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId
//                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
//                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
//                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
//                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
//                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
//                               LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
//                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
//                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 
//                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
//                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 
//								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
//                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 
//                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
//                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana
//                                left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId
//LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
//                                where (a.IsDelete is null or a.IsDelete = 0) and     B.EmpinfoId  not   IN (SELECT tblAppraisalSelfMaster.EmpInfoId FROM tblAppraisalSelfMaster    LEFT JOIN tblFinancialYear y ON tblAppraisalSelfMaster.FinancialYearId = y.FinancialYearId  where   y.FinancialYearDesc='" + FinYear + "' ) " + param_SP + ") tbl  order by EmpMasterCode";
//              //+ param
//              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
//          }
//          catch (Exception exception)
//          {

//              throw exception;
//          }
//      }


//      public DataTable rptGetKpiSetupListNOTINNN(string param, string param_SP, string FinYear)
//      //string param
//      {

//          try
//          {
//              string query = @"SELECT rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
//cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
//JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
//EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
//EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
//  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
//  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
//   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
//       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
//	   
//	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , a.KPIDeadLineMasterId ,  d.FinancialYearDesc , CONVERT(nvarchar (11),a.DeclarationDate , 106)DeclarationDate,* 
//  from tblKpiDeadlineMaster A  WITH (NOLOCK) 
//   left join (SELECT KPIDeadLineMasterId, EmpinfoId from tblKPIDeadLineDetails group BY EmpinfoId,  KPIDeadLineMasterId) B on a.KPIDeadLineMasterId = b.KPIDeadLineMasterId  
//                                     LEFT JOIN dbo.tblEmpGeneralInfo EG ON  B.EmpinfoId = EG.EmpInfoId
//                                     LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
//                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
//                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
//                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
//                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId
//                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
//                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
//                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
//                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
//                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
//                               LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
//                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
//                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 
//                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
//                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 
//								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
//                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 
//                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
//                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana
//                                left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId
//LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
//                                where (a.IsDelete is null or a.IsDelete = 0) and     B.EmpinfoId  Not  IN (SELECT tblAppraisalSelfMaster.EmpInfoId FROM tblAppraisalSelfMaster )   " + param;
//              //+ param
//              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
//          }
//          catch (Exception exception)
//          {

//              throw exception;
//          }
//      }


      public int SaveKpiSetupMaster(KpiDeadlineMaster aMaster, string user)
      {
          try
          {
              int pk = 0;

              if (aMaster.KPIDeadLineMasterId == 0)
              {
                  List<SqlParameter> aParameters = new List<SqlParameter>();
                  aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                  aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                  aParameters.Add(new SqlParameter("@Subject", aMaster.Subject));
                  aParameters.Add(new SqlParameter("@IsCommon", aMaster.IsCommon));
                  aParameters.Add(new SqlParameter("@DeclarationDate", aMaster.DeclarationDate));
                  aParameters.Add(new SqlParameter("@EntryBy", user));
                  aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));

                  string query =
                      @"insert into tblKpiDeadlineMaster (CompanyId, FinancialYearId, IsCommon ,EntryDate, EntryBy,Subject, DeclarationDate) values(@CompanyId, @FinancialYearId, @IsCommon ,@EntryDate, @EntryBy,@Subject, @DeclarationDate)";

                  pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                  return pk;
              }
              else
              {
                  List<SqlParameter> aParameters = new List<SqlParameter>();
                  aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                  aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                  aParameters.Add(new SqlParameter("@KPIDeadLineMasterId", aMaster.KPIDeadLineMasterId));
                  aParameters.Add(new SqlParameter("@IsCommon", aMaster.IsCommon));
                  aParameters.Add(new SqlParameter("@Subject", aMaster.Subject));
                  aParameters.Add(new SqlParameter("@UpdateBy", user));
                  aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));
                  aParameters.Add(new SqlParameter("@DeclarationDate", aMaster.DeclarationDate));

                  string query = @"update tblKpiDeadlineMaster set CompanyId = @CompanyId ,Subject=@Subject, FinancialYearId = @FinancialYearId , IsCommon = @IsCommon , UpdateBy = @UpdateBy , UpdateDate = @UpdateDate, DeclarationDate=@DeclarationDate where KPIDeadLineMasterId = @KPIDeadLineMasterId ";

                  bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);

                  if (result == true)
                  {
                      pk = aMaster.KPIDeadLineMasterId;
                  }

                  return pk;
              }

          }
          catch (Exception ex)
          {

              throw ex;
          }
      }

    }
}
