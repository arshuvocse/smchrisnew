using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI.WebControls;
using DAL.InternalCls;

namespace DAL.Report_DAL
{
  public  class AccountsInformationReportDAL
    {

      ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

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
          string queryStr = @"SELECT tblDivisionWing.Invisible,* FROM tblDepartment  WITH (NOLOCK) 
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE tblDepartment.IsActive = 'True' AND DepartmentId = '" + id + "' " + param + "";
          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }
      public DataTable GetSectionRelaton(string id, string param)
      {
          string queryStr = @"SELECT tblDepartment.Invisible,* FROM dbo.tblSection  WITH (NOLOCK) 
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
 WHERE dbo.tblSection.IsActive = 'True' AND SectionId = '" + id + "' " + param + "";
          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }
      public DataTable GetSubSectionRelaton(string id, string param)
      {
          string queryStr = @"SELECT dbo.tblSection.Invisible,* FROM dbo.tblSubSection  WITH (NOLOCK) 
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

          string queryStr = "SELECT DivisionWId,DivisionWingName FROM tblDivisionWing  WITH (NOLOCK)  WHERE IsActive = 'True' AND DivisionId = @DivisionId ";
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

          string queryStr = @"SELECT DepartmentId,DepartmentName FROM tblDepartment  WITH (NOLOCK) 
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

          string queryStr = @"SELECT * FROM dbo.tblSection  WITH (NOLOCK) 
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

          string queryStr = "SELECT DepartmentId,DepartmentName FROM tblDepartment  WITH (NOLOCK)  WHERE IsActive = 'True' AND DivisionWId = @wingId AND (Invisible IS NULL OR Invisible='False')";
          aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", queryStr, aSqlParameterlist, "HRDB");
      }

      public void GetSectionList(DropDownList ddl, string departmentId)
      {
          var aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@departmentId", departmentId));

          string queryStr = "SELECT SectionId,SectionName FROM tblSection  WITH (NOLOCK)  WHERE IsActive = 'True' AND DepartmentId = @departmentId AND (Invisible IS NULL OR Invisible='False')";
          aCommonInternalDal.LoadDropDownValue(ddl, "SectionName", "SectionId", queryStr, aSqlParameterlist, "HRDB");
      }

      public DataTable NewJoinerListDAL(string param)
      {
          string queryStr = @"  SELECT  EG.ContractPeriod, EG.DateOfJoin,  EG.SMCOldCode, ST.GrossAmount,  ISNULL(ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL),'') ParentsInfo ,  rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode+' : '+SG.GradeName Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , PT.PromotionTypeName , EPE.FinancialYearId,EPE.Effectivedate ,SLoc.SalaryLocation,  
  Desig.Designation, SGrade.GradeCode GradeName, SStep.SalaryStepName, SStep.GrossAmount, cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year , ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  as LengthServicewithSMC,  CASE
    WHEN EPE.IsReappointment =1 and EPE.NPromoTypeId IS NULL THEN 'Reappointment- '+ FORMAT(dtEmployeePromotion.Effectivedate, 'dd-MMM-yyyy')
    WHEN EPE.IsReappointment =0 and EPE.NPromoTypeId=1 THEN 'Promotion- '+ FORMAT(dtEmployeePromotion.Effectivedate, 'dd-MMM-yyyy')
    WHEN   EPE.IsReappointment =0 and  EPE.NPromoTypeId=2 THEN 'Upgradation- '+ FORMAT(dtEmployeePromotion.Effectivedate, 'dd-MMM-yyyy')
    ELSE 'N/A'
END AS LastPromotion, cast((DATEDIFF(m, dtEmployeePromotion.Effectivedate, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, dtEmployeePromotion.Effectivedate, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d,dtEmployeePromotion.Effectivedate, GETDATE())%12) as varchar) + ' day'  TotalLengthfromlastpromotion, dtDiciplinaryAction.Effectivedate diciplinaryEffectivedate, * From tblEmployeePromotionEntry EPE  WITH (NOLOCK)
 left JOIN dbo.tblEmpGeneralInfo  EG ON EPE.EmployeeId = EG.EmpInfoId
  left JOIN dbo.tblDesignation  DEG ON EG.DesignationId = DEG.DesignationId
  left JOIN dbo.tblSalaryLocation  SLoc ON EG.SalaryLoationId = SLoc.SalaryLoationId
 left JOIN dbo.tblDepartment  NDEG ON EG.DepartmentId = NDEG.DepartmentId
 left JOIN  dbo.tblCompanyInfo com ON com.CompanyId = EPE.CompanyId 
left JOIN dbo.tblPromotionType PT ON EPE.NPromoTypeId =PT.PromotionTypeId
left JOIN dbo.tblDesignation Desig ON EPE.NDesignationId =Desig.DesignationId
left JOIN dbo.tblSalaryGrade SGrade ON EPE.NSalGradeId =SGrade.SalaryGradeId
left JOIN dbo.tblSalaryStep SStep ON EPE.NSalaryStepId =SStep.SalaryStepId
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

								   left JOIN (SELECT TOP 1 EmpInfoId,EffectiveDate FROM  dbo.tblDiciplinaryAction
 
   ORDER BY  EffectiveDate desc)dtDiciplinaryAction ON dtDiciplinaryAction.EmpInfoId=EPE.EmployeeId     

     left JOIN (SELECT EmployeeId,MAX(Effectivedate)AS Effectivedate  FROM  dbo.tblEmployeePromotionEntry GROUP BY EmployeeId
 
   )dtEmployeePromotion ON dtEmployeePromotion.EmployeeId=EPE.EmployeeId     

								   Where ( EPE.IsDelete IS NULL OR EPE.IsDelete = 0)
    " + param + "  ORDER BY EG.EmpMasterCode ASC";
          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }


      public DataTable LoadInfoSeparationDAL(string param)
      {
          string queryStr = @"SELECT  EG.ContractPeriod, EG.DateOfJoin,  EG.SMCOldCode, ST.GrossAmount,  ISNULL(ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL),'') ParentsInfo , rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode+' : '+SG.GradeName Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.NationalIdNo, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
  CAST((DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate)  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate), EG.DateOfJoin) > EPE.JobLeftDate THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(EPE.JobLeftDate - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(EPE.JobLeftDate - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate), EG.DateOfJoin) > EPE.JobLeftDate THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days' ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate ,
   (CASE WHEN EIM.ExitMasterId
IS NULL THEN 'Pending' ELSE 'Completed' END) AS ExitFormStatus,CAST((DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate)  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate), EG.DateOfJoin) > EPE.JobLeftDate THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(EPE.JobLeftDate - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(EPE.JobLeftDate - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate), EG.DateOfJoin) > EPE.JobLeftDate THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  as LengthServicewithSMC, JType.JobLeftType, * From tblEmployeeJobLeft EPE  WITH (NOLOCK)
                                    INNER JOIN dbo.tblEmpGeneralInfo  EG ON EPE.EmployeeId = EG.EmpInfoId
                                    INNER JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
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
						  LEFT JOIN dbo.tblUser  UpBY ON EPE.UpdateBy = UpBY.UserId   
						  LEFT JOIN dbo.tblExitInterviewFormMaster EIM ON EIM.EmployeeId = EPE.EmployeeId  

LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
  INNER JOIN dbo.tblJobLeftType  JType ON EPE.JobLeftTypeId = JType.JobLeftTypeId 
"
              + HttpContext.Current.Session["finalup"] + " Where ( EPE.IsDelete IS NULL OR EPE.IsDelete = 0) " + param + "  ORDER BY EG.EmpMasterCode ASC";
          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }


      public DataTable LoadInfoProbationaryEmployeeDAL(string param)
      {
          string queryStr = @"SELECT     com.ShortName, EG.EmpMasterCode, EG.EmpName, EG.DateOfJoin,  EG.ProbationEndDate, DS.Designation,  EPE.ProbationEndReason, EPE.ExProDate,*  FROM tblProbationEvaluationMaster EPE  WITH (NOLOCK) 
  INNER JOIN dbo.tblEmpGeneralInfo  EG ON EPE.EmpInfoId = EG.EmpInfoId
                                    INNER JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
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
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana                             where EPE.ProbationEvaluationMasterId is not null 
						 " + param + "  ORDER BY EG.EmpMasterCode ASC";
          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }


      public DataTable LoadInforedesignationDAL(string param)
      {
          string queryStr = @"SELECT com.ShortName,  SG.GradeCode+' : '+SG.GradeName Grade , EG.EmpMasterCode,EG.EmpName,  NDEs.Designation NDesignation,  PDEG.Designation PDesignation,   NDEG.DepartmentName , EPE.Effectivedate,  * From tblEmployeeReDesignation EPE with (nolock)
 inner JOIN dbo.tblEmpGeneralInfo  EG ON EPE.EmployeeId = EG.EmpInfoId

  left JOIN dbo.tblDesignation  PDEG ON EPE.PDesignationId = PDEG.DesignationId
  left JOIN dbo.tblDesignation  NDEs ON EPE.NDesignationId = NDEs.DesignationId
 
 
  left JOIN dbo.tblDepartment  NDEG ON EPE.DepartmentId = NDEG.DepartmentId
  
 
  
    left JOIN  dbo.tblCompanyInfo com ON com.CompanyId = EPE.CompanyId 
	                            LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
	                           	where EPE.EmployeeReDesignationId is not null
						 " + param + "  ORDER BY EG.EmpMasterCode ASC";
          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }
    }
}
