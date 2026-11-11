using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.InternalCls;

namespace DAL.MasterSetup_DAL
{
  public  class VivaSetupInfoEntryDAL
    {
      readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

      public DataTable GetVacanceyEntryformationParam(string parm)
      {
          string queryStr = @"SELECT us.UserName EntryBy, usUp.UserName UpdateBy,  tblCompanyInfo.ShortName, * FROM tblVivaSetupInfo
LEFT JOIN dbo.tblCompanyInfo ON tblCompanyInfo.CompanyId = tblVivaSetupInfo.CompanyId
left JOIN  dbo.tblUser us   ON  tblVivaSetupInfo.EntryBy =us.UserId  
left JOIN  dbo.tblUser usUp   ON  tblVivaSetupInfo.UpdateBy =usUp.UserId WHERE (tblVivaSetupInfo.IsDelete is NULL) OR (tblVivaSetupInfo.IsDelete =0 )" + parm + "";
          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }



      public DataTable GeMedicalCheckupInfo(string parm)
      {
          string queryStr = @"SELECT us.UserName EntryBy, usUp.UserName UpdateBy,  tblCompanyInfo.ShortName, * FROM tblMedicalCheckUpMaster
LEFT JOIN dbo.tblCompanyInfo ON tblCompanyInfo.CompanyId = tblMedicalCheckUpMaster.CompanyId
left JOIN  dbo.tblUser us   ON  tblMedicalCheckUpMaster.EntryBy =us.UserId  
left JOIN  dbo.tblUser usUp   ON  tblMedicalCheckUpMaster.UpdateBy =usUp.UserId WHERE ((tblMedicalCheckUpMaster.IsDelete is NULL) OR (tblMedicalCheckUpMaster.IsDelete =0 ))  " + parm + "";
          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }



      public DataTable LoadMedicalEditLoadById(string id)
      {
          string query = @"SELECT e.EmpName, e.EmpMasterCode  ,* FROM tblMedicalCheckUp ckup 
LEFT JOIN dbo.tblEmpGeneralInfo e ON e.EmpInfoId = ckup.EmpInfoId
 WHERE ckup.MasterId='" + id + "'";


          return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
      }




      public DataTable GeMedicalCheckupDetailsInfoRPT(string parm)
      {
          string queryStr = @"SELECT    tblEmpGeneralInfo.EmpMasterCode, tblEmpGeneralInfo.EmpName, dbo.tblDesignation.Designation, tblDepartment.DepartmentName, *
 FROM   tblMedicalCheckUpMaster

inner JOIN dbo.tblMedicalCheckUp ON tblMedicalCheckUpMaster.MedicalCheckUpMasterId = tblMedicalCheckUp.MasterId 
 
LEFT JOIN dbo.tblEmpGeneralInfo ON tblEmpGeneralInfo.EmpInfoId = tblMedicalCheckUp.EmpInfoId
LEFT JOIN dbo.tblDesignation ON tblEmpGeneralInfo.DesignationId = tblDesignation.DesignationId
LEFT JOIN dbo.tblDepartment ON tblEmpGeneralInfo.DepartmentId = tblDepartment.DepartmentId
 WHERE (tblMedicalCheckUpMaster.IsDelete is NULL  OR  tblMedicalCheckUpMaster.IsDelete =0 )  " + parm + "";
          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }


      public DataTable NewGeMedicalCheckupDetailsInfoRPT(string parm)
      {
          string queryStr = @"SELECT  EG.ContractPeriod, EG.DateOfJoin,  EG.SMCOldCode, ST.GrossAmount,  ISNULL(ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL),'') ParentsInfo ,rptBody.EmpName Supervisor,  com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , MCM.Date MedicalCheckUpDate, MCM.Comments MedicalCheckUpComments, tblMedicalCheckUp.Remarks MedicalCheckUpRemarks,   *
 FROM   tblMedicalCheckUpMaster MCM
 inner JOIN dbo.tblMedicalCheckUp ON MCM.MedicalCheckUpMasterId = tblMedicalCheckUp.MasterId 
 LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = tblMedicalCheckUp.CompanyId
 LEFT JOIN dbo.tblEmpGeneralInfo EG ON EG.EmpInfoId = tblMedicalCheckUp.EmpInfoId
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
 WHERE (MCM.IsDelete is NULL  OR  MCM.IsDelete =0 )     " + parm + "    ORDER BY EG.EmpMasterCode ASC";
          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }

      public DataTable GeMedicalCheckupDetailsInfoNotRPT(string parm)
      {
          string queryStr = @" SELECT '' Date,''Remarks,* FROM dbo.tblEmpGeneralInfo 
 LEFT JOIN dbo.tblDesignation ON tblEmpGeneralInfo.DesignationId = tblDesignation.DesignationId
LEFT JOIN dbo.tblDepartment ON tblEmpGeneralInfo.DepartmentId = tblDepartment.DepartmentId 
WHERE tblEmpGeneralInfo.IsActive=1 and  tblEmpGeneralInfo.EmpInfoId not  IN (SELECT EmpInfoId FROM tblMedicalCheckUpMaster
inner JOIN dbo.tblMedicalCheckUp ON tblMedicalCheckUpMaster.MedicalCheckUpMasterId = tblMedicalCheckUp.MasterId 
 WHERE (tblMedicalCheckUpMaster.IsDelete is NULL) OR (tblMedicalCheckUpMaster.IsDelete =1 )) " + parm + "";
          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }



      public DataTable NEWGeMedicalCheckupDetailsInfoNotRPT(string parm)
      {
          string queryStr = @"  SELECT rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , '' MedicalCheckUpDate, '' MedicalCheckUpComments, '' MedicalCheckUpRemarks,* FROM dbo.tblEmpGeneralInfo  EG
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
WHERE EG.IsActive=1 and  EG.EmpInfoId not  IN (SELECT EmpInfoId FROM tblMedicalCheckUpMaster
inner JOIN dbo.tblMedicalCheckUp ON tblMedicalCheckUpMaster.MedicalCheckUpMasterId = tblMedicalCheckUp.MasterId 
 WHERE (tblMedicalCheckUpMaster.IsDelete is NULL) OR (tblMedicalCheckUpMaster.IsDelete =1 )) " + parm + "";
          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }
    }
}
