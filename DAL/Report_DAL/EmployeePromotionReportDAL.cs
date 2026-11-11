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

namespace DAL.ExitManagement_DAL
{
    public class EmployeePromotionReportDAL
    {

        public void LoadPromotionTypeDropDownList(DropDownList ddl)
        {
            string query = "SELECT * FROM tblPromotionType  WITH (NOLOCK)";
            aCommonInternalDal.LoadDropDownValue(ddl, "PromotionTypeName", "PromotionTypeId", query, "HRDB");
        }

       ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
       public void LoadCompanyDropDownList(DropDownList ddl)
       {
           string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo  WITH (NOLOCK) WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
           //string query = "SELECT * FROM tblCompanyInfo";
           aCommonInternalDal.LoadDropDownValue(ddl, "CompanyName", "CompanyId", queryStr, "HRDB");
       }

       public DataTable ValidattionForEffectiveDate(string id, string date)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmployeeId", id));
           aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", date));
           string query = @" SELECT EmployeeId,JobLeftDate  FROM dbo.tblEmployeeJobLeft  WITH (NOLOCK) WHERE  EmployeeId=@EmployeeId and JobLeftDate=@EffectiveDate";
           return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
       }

       public DataTable DeleteValidattionForEffectiveDate(string id)
       {
           string query = @" SELECT  EmployeeJobLeftId, JobLeftDate FROM dbo.tblEmployeeJobLeft WHERE EmployeeJobLeftId=" + id;
           return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
       }

       public void LoadJobLeftTypeDropDownList(CheckBoxList ddl)
       {
           

           //var aSqlParameterlist = new List<SqlParameter>();
           //aSqlParameterlist.Add(new SqlParameter("@ID", null));

           //const string queryStr = @"SELECT * FROM tblJobLeftType";
           //return aCommonInternalDal.DataContainerDataTable(queryStr,   "HRDB");
       }
       public DataTable GetJobleftType()
       {
           string queryStr = @"SELECT *	 FROM tblJobLeftType";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }
       public DataTable LoadEmpJInfoInTextBoxById(int id)
       {
           string query = @" SELECT tblEmployeeType.EmpType EmployeeMentType, Egf.EmpMasterCode, Egf.EmpName, Egf.DateOfJoin,   deg.Designation, SG.GradeCode+' : '+ SG.GradeName SalaryGrade, Com.CompanyName, Div.DivisionName, Wing.DivisionWingName,  Dpt.DepartmentName,     Sec.SectionName, SubSec.SubSectionName, *  FROM dbo.tblEmpGeneralInfo Egf
							left JOIN dbo.tblDesignation  deg ON Egf.DesignationId=deg.DesignationId
							left JOIN dbo.tblSalaryGrade  SG ON Egf.SalaryGradeId=SG.SalaryGradeId
							left JOIN dbo.tblCompanyInfo  Com ON Egf.CompanyId=Com.CompanyId
							left JOIN dbo.tblSalaryLocation  Loc ON Egf.SalaryLoationId=Loc.SalaryLoationId
							left JOIN dbo.tblJobLocation  JLoc ON Egf.JobLocationId=JLoc.JobLocationID
							left JOIN dbo.tblDivision  Div ON Egf.DivisionId=Div.DivisionId
							LEFT JOIN dbo.tblDivisionWing  Wing ON Egf.DivisionWId=Wing.DivisionWId
							left JOIN dbo.tblSection  Sec ON Egf.SectionId=Sec.SectionId
							LEFT JOIN dbo.tblSubSection  SubSec ON Egf.SubSectionId=SubSec.SubSectionId						
								LEFT JOIN dbo.tblDepartment  Dpt ON Egf.DepartmentId=Dpt.DepartmentId
INNER JOIN dbo.tblEmployeeType ON tblEmployeeType.EmpTypeId = Egf.EmpTypeId
							 WHERE Egf.EmpInfoId='" + id + "'";


           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
       public DataTable GetEmployeeIsJobLeft(string empinfoId)
       {
           string query = @"SELECT * FROM dbo.tblEmployeeJobLeft WHERE EmployeeId='" + empinfoId + "' AND (IsJobLeft IS NULL OR IsJobLeft='0')";

           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }

       public DataTable LoadBenifitInformation(string param)
       {
           string query = @" SELECT *,'0'Amount FROM dbo.tblBenefitMaster WHERE BenefitMasterId IN (

	   SELECT tblBenefitDetail.BenefitMasterId FROM dbo.tblBenefitMaster
	   LEFT JOIN dbo.tblBenefitDetail ON tblBenefitDetail.BenefitMasterId = tblBenefitMaster.BenefitMasterId
	   LEFT JOIN dbo.tblBenefitJobNature ON tblBenefitJobNature.BenefitMasterId = tblBenefitMaster.BenefitMasterId
	   "+param+")";


           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }

//       }public DataTable LoadBenifitInformation()
//       {
//           string query = @" SELECT *, 0 as Amount
//						FROM tblBenefitName
//
//						WHERE IsActive=1
//							 ";


//           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
//       }


       public DataTable LoadEmpJoblefSereachById(int id)
       {
           string query = @"SELECT Egf.EmpMasterCode, Egf.EmpName, Egf.DateOfJoin,   deg.Designation, SG.GradeName SalaryGrade, Com.CompanyName, Div.DivisionName, Wing.DivisionWingName,  Dpt.DepartmentName,     Sec.SectionName, SubSec.SubSectionName, *  FROM dbo.tblEmpGeneralInfo Egf

							left JOIN dbo.tblDesignation  deg ON Egf.DesignationId=deg.DesignationId
							left JOIN dbo.tblSalaryGrade  SG ON Egf.SalaryGradeId=SG.SalaryGradeId
							left JOIN dbo.tblCompanyInfo  Com ON Egf.CompanyId=Com.CompanyId
							left JOIN dbo.tblSalaryLocation  Loc ON Egf.SalaryLoationId=Loc.SalaryLoationId
							left JOIN dbo.tblJobLocation  JLoc ON Egf.JobLocationId=JLoc.JobLocationID
							left JOIN dbo.tblDivision  Div ON Egf.DivisionId=Div.DivisionId
							LEFT JOIN dbo.tblDivisionWing  Wing ON Egf.DivisionWId=Wing.DivisionWId
							left JOIN dbo.tblSection  Sec ON Egf.SectionId=Sec.SectionId
							LEFT JOIN dbo.tblSubSection  SubSec ON Egf.SubSectionId=SubSec.SectionId						
								LEFT JOIN dbo.tblDepartment  Dpt ON Egf.DepartmentId=Dpt.DepartmentId
							
			
							
							 WHERE Egf.EmpInfoId='" + id + "'";


           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }


       public Int32 EmployeePromotionEntrySaveInfo(EmployeeJobLeftEntryDAO aEmployeeJobLeftEntryDAO)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aEmployeeJobLeftEntryDAO.EmployeeId));
           aSqlParameterlist.Add(new SqlParameter("@CompanyId", aEmployeeJobLeftEntryDAO.CompanyId));







           aSqlParameterlist.Add(new SqlParameter("@JobLeftTypeId", aEmployeeJobLeftEntryDAO.JobLeftTypeId));

           aSqlParameterlist.Add(new SqlParameter("@JobLeftDate", aEmployeeJobLeftEntryDAO.JobLeftDate));
           aSqlParameterlist.Add(new SqlParameter("@Reason", aEmployeeJobLeftEntryDAO.Reason));

           aSqlParameterlist.Add(new SqlParameter("@EntryBy", aEmployeeJobLeftEntryDAO.EntryBy));
           aSqlParameterlist.Add(new SqlParameter("@EntryDate", aEmployeeJobLeftEntryDAO.EntryDate));


           aSqlParameterlist.Add(new SqlParameter("@SubmissionDate", aEmployeeJobLeftEntryDAO.SubmissionDate));
           aSqlParameterlist.Add(new SqlParameter("@IsClearanceForm", aEmployeeJobLeftEntryDAO.IsClearanceForm ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsExitInterview", aEmployeeJobLeftEntryDAO.IsExitInterview ?? (object)DBNull.Value));




           string insertQuery = @" INSERT INTO [dbo].[tblEmployeeJobLeft]
           (CompanyId
           ,EmployeeId
           ,JobLeftTypeId
           ,JobLeftDate
           ,Reason
           ,EntryBy
           ,EntryDate,
IsClearanceForm,
IsExitInterview,
SubmissionDate

         )
     VALUES
           (@CompanyId
           ,@EmployeeId
           ,@JobLeftTypeId
           ,@JobLeftDate
           ,@Reason
           ,@EntryBy
           ,@EntryDate,
@IsClearanceForm,
@IsExitInterview,
@SubmissionDate)";

           return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
       }
       public Int32 EmployeePromotionBenefitEntrySaveInfo(EmployeeJobLeftEntryDAO aEmployeeJobLeftEntryDAO)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           aSqlParameterlist.Add(new SqlParameter("@EmployeeJobLeftId", aEmployeeJobLeftEntryDAO.EmployeeJobLeftId));
           aSqlParameterlist.Add(new SqlParameter("@BenefitId", aEmployeeJobLeftEntryDAO.BenefitId));
           aSqlParameterlist.Add(new SqlParameter("@Amount", aEmployeeJobLeftEntryDAO.Amount));
           aSqlParameterlist.Add(new SqlParameter("@Active", aEmployeeJobLeftEntryDAO.Active));
           aSqlParameterlist.Add(new SqlParameter("@IsAddition", aEmployeeJobLeftEntryDAO.IsAddition));
           aSqlParameterlist.Add(new SqlParameter("@IsDeduction", aEmployeeJobLeftEntryDAO.IsDeduction));

           string insertQuery = @"INSERT INTO dbo.tblEmployeeJobLeftBenefit
	   (
	       BenefitId,
	       EmployeeJobLeftId,Amount,Active,IsAddition,IsDeduction
	   )
	   VALUES
	   (   @BenefitId,
	       @EmployeeJobLeftId,@Amount,@Active,@IsAddition,@IsDeduction
	   )";

           return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
       }
       public bool DeleteEmployeeJobLeftBenefitById(string id)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmployeeJobLeftId", id));

           const string query = @"DELETE FROM tblEmployeeJobLeftBenefit where EmployeeJobLeftId=@EmployeeJobLeftId";
           //  const string query = @"DELETE FROM tblEmployeeJobLeft WHERE EmployeeJobLeftId = @EmployeeJobLeftId";
           return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
       }
       public DataTable LoadEmpJobleftBenefit(string id )
       {
           string queryStr = @"SELECT * FROM dbo.tblEmployeeJobLeftBenefit
LEFT JOIN dbo.tblBenefitMaster ON dbo.tblBenefitMaster.BenefitMasterId=dbo.tblEmployeeJobLeftBenefit.BenefitId WHERE EmployeeJobLeftId='"+id+"'";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }
       public DataTable LoadEmpJobleftBenefitByBenefit(string id,string benefitId )
       {
           string queryStr = @"SELECT * FROM dbo.tblEmployeeJobLeftBenefit
LEFT JOIN dbo.tblBenefitMaster ON dbo.tblBenefitMaster.BenefitMasterId=dbo.tblEmployeeJobLeftBenefit.BenefitId WHERE EmployeeJobLeftId='" + id + "' AND BenefitId='" + benefitId + "'";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }
       public DataTable LoadInformationALl(string param)
       {
           string queryStr = @"  SELECT  EG.ContractPeriod, EG.DateOfJoin,  EG.SMCOldCode, ST.GrossAmount,  ISNULL(ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL),'') ParentsInfo ,  rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
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
	   
	   +    cast((DATEDIFF(d,dtEmployeePromotion.Effectivedate, GETDATE())%12) as varchar) + ' day'  TotalLengthfromlastpromotion, dtDiciplinaryAction.Effectivedate diciplinaryEffectivedate, * From tblEmployeePromotionEntry EPE
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



       public DataTable LoadInformationALl22(string param, string param2, string paramSP)
       {
           string queryStr = @"    select distinct * from (SELECT    NDEG.DepartmentName,  EG.EmpName, EG.EmpMasterCode,  EG.ContractPeriod, EG.DateOfJoin,  EG.SMCOldCode, ST.GrossAmount,  ISNULL(ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL),'') ParentsInfo ,  rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , PT.PromotionTypeName , EPE.FinancialYearId,EPE.Effectivedate ,SLoc.SalaryLocation,  
  Desig.Designation, SGrade.GradeCode GradeName, SStep.SalaryStepName,  cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year , ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  as LengthServicewithSMC,  CASE
    WHEN EPE.IsReappointment =1 and EPE.NPromoTypeId IS NULL THEN 'Reappointment- '+ FORMAT(dtEmployeePromotion.Effectivedate, 'dd-MMM-yyyy')
    WHEN EPE.IsReappointment =0 and EPE.NPromoTypeId=1 THEN 'Promotion- '+ FORMAT(dtEmployeePromotion.Effectivedate, 'dd-MMM-yyyy')
    WHEN   EPE.IsReappointment =0 and  EPE.NPromoTypeId=2 THEN 'Upgradation- '+ FORMAT(dtEmployeePromotion.Effectivedate, 'dd-MMM-yyyy')
    ELSE 'N/A'
END AS LastPromotion, cast((DATEDIFF(m, dtEmployeePromotion.Effectivedate, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, dtEmployeePromotion.Effectivedate, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d,dtEmployeePromotion.Effectivedate, GETDATE())%12) as varchar) + ' day'  TotalLengthfromlastpromotion, dtDiciplinaryAction.Effectivedate diciplinaryEffectivedate  From tblEmployeePromotionEntry EPE
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
    " + param + @"       UNION ALL 


								   SELECT  NDEG.DepartmentName,  EG.EmpName, EG.EmpMasterCode,  EG.ContractPeriod, EG.DateOfJoin,  EG.SMCOldCode, ST.GrossAmount,  ISNULL(ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL),'') ParentsInfo ,  rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , EPE.TypeOfPromotion PromotionTypeName , NULL FinancialYearId ,EPE.EffectDate ,SLoc.SalaryLocation,  
  Desig.Designation, SGrade.GradeCode GradeName, SStep.SalaryStepName,  cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year , ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  as LengthServicewithSMC,   'Promotion- '+ FORMAT(dtEmployeePromotion.Effectivedate, 'dd-MMM-yyyy')
    
    AS LastPromotion, cast((DATEDIFF(m, dtEmployeePromotion.Effectivedate, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, dtEmployeePromotion.Effectivedate, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d,dtEmployeePromotion.Effectivedate, GETDATE())%12) as varchar) + ' day'  TotalLengthfromlastpromotion, dtDiciplinaryAction.Effectivedate diciplinaryEffectivedate  From tblPromotionUpgrationHistory EPE
 left JOIN dbo.tblEmpGeneralInfo  EG ON EPE.EmployeeId = EG.EmpInfoId
  left JOIN dbo.tblDesignation  DEG ON EG.DesignationId = DEG.DesignationId
  left JOIN dbo.tblSalaryLocation  SLoc ON EG.SalaryLoationId = SLoc.SalaryLoationId
 left JOIN dbo.tblDepartment  NDEG ON EG.DepartmentId = NDEG.DepartmentId
 left JOIN  dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId 
 
left JOIN dbo.tblDesignation Desig ON EG.DesignationId =Desig.DesignationId
left JOIN dbo.tblSalaryGrade SGrade ON EG.SalaryGradeId =SGrade.SalaryGradeId
left JOIN dbo.tblSalaryStep SStep ON EG.SalaryStepId =SStep.SalaryStepId
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

     left JOIN (SELECT EmployeeId,MAX(EffectDate)AS Effectivedate  FROM  dbo.tblPromotionUpgrationHistory GROUP BY EmployeeId
 
   )dtEmployeePromotion ON dtEmployeePromotion.EmployeeId=EPE.EmployeeId  where   EPE.TypeOfPromotion IN ('Promotion','Upgradation')  " + param2 + @"  union all 


   SELECT   NDEG.DepartmentName,  EG.EmpName, EG.EmpMasterCode,  EG.ContractPeriod, EG.DateOfJoin,  EG.SMCOldCode, ST.GrossAmount,  ISNULL(ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL),'') ParentsInfo ,  rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , PT.PromotionTypeName , EPE.FinancialYearId,EPE.Effectivedate ,SLoc.SalaryLocation,  
  Desig.Designation, SGrade.GradeCode GradeName, SStep.SalaryStepName,  cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year , ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  as LengthServicewithSMC,  CASE
    WHEN EPE.IsReappointment =1 and EPE.NPromoTypeId IS NULL THEN 'Reappointment- '+ FORMAT(dtEmployeePromotion.Effectivedate, 'dd-MMM-yyyy')
    WHEN EPE.IsReappointment =0 and EPE.NPromoTypeId=1 THEN 'Promotion- '+ FORMAT(dtEmployeePromotion.Effectivedate, 'dd-MMM-yyyy')
    WHEN   EPE.IsReappointment =0 and  EPE.NPromoTypeId=2 THEN 'Upgradation- '+ FORMAT(dtEmployeePromotion.Effectivedate, 'dd-MMM-yyyy')
    ELSE 'N/A'
END AS LastPromotion, cast((DATEDIFF(m, dtEmployeePromotion.Effectivedate, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, dtEmployeePromotion.Effectivedate, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d,dtEmployeePromotion.Effectivedate, GETDATE())%12) as varchar) + ' day'  TotalLengthfromlastpromotion, dtDiciplinaryAction.Effectivedate diciplinaryEffectivedate  From tblEmployeePromotionEntry EPE

 left JOIN dbo.tblFinancialYear  fn ON fn.FinancialYearId = EPE.FinancialYearId
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
      inner JOIN   tblEmpAllRefference reff  ON EG.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
                                where  EG.IsActive=1  and     reff.ShowCompany in (ComAssain)  and ( EPE.IsDelete IS NULL OR EPE.IsDelete = 0) " + paramSP + "  ) tbl order by  EmpMasterCode ";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }



       public DataTable NOLoadInformationALl(string param, string param2, string Param3, string Param4)
       {
           string queryStr = @" SELECT DISTINCT  * FROM      ( SELECT EG.EmpInfoId AS EmpInfoId, EG.ContractPeriod, EG.DateOfBirth, DP.DepartmentName, EG.EmpName, EG.EmpMasterCode, DS.Designation,  EG.DateOfJoin,  EG.SMCOldCode, ST.GrossAmount,  ISNULL(ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL),'') ParentsInfo ,   com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place,   nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , rptBody.EmpName Supervisor FROM dbo.tblEmpGeneralInfo EG  WITH (NOLOCK)
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
								WHERE EG.EmployeeStatus='Active'
    " + param + @"      UNION ALL 

									 SELECT EG.EmpInfoId AS EmpInfoId, EG.ContractPeriod, EG.DateOfBirth, DP.DepartmentName, EG.EmpName, EG.EmpMasterCode, DS.Designation,  EG.DateOfJoin,  EG.SMCOldCode, ST.GrossAmount,  ISNULL(ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL),'') ParentsInfo ,   com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place,   nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , rptBody.EmpName Supervisor    FROM tblEmpGeneralInfo   EG   with (NOLOCk)
  
   left JOIN dbo.tblDesignation  DEG ON EG.DesignationId = DEG.DesignationId
   LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
 LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
   LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
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
  left JOIN dbo.tblDepartment  NDEG ON EG.DepartmentId = NDEG.DepartmentId
									    where EG.EmpInfoId is not NULL        AND    EG.EmpInfoId  not IN (SELECT EmployeeId FROM tblPromotionUpgrationHistory EPE where   EPE.TypeOfPromotion IN ('Promotion')  ) AND EG.EmployeeStatus='Active'  " + param2 + @"   )    tblz    where (tblz.EmpInfoId  not IN ((SELECT EmployeeId FROM tblEmployeePromotionEntry EPE WHERE ( (EPE.IsDelete IS NULL) OR (EPE.IsDelete = 0) ) AND EPE.NPromoTypeId=1   " + Param4 + @"
							   ) 
								 ))
								  ORDER BY  EmpMasterCode ASC";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }

       public void FinYearByCompDropDown(DropDownList ddl, string id)
       {
           string queryStr = @"SELECT * FROM dbo.tblFinancialYear WHERE CompanyId='" + id + "'";
           aCommonInternalDal.LoadDropDownValue(ddl, "FinancialYearDesc", "FinancialYearId", queryStr, DataBase.HRDB);
       }
       public bool DeleteEmployeeJobLeftById(EmployeeJobLeftEntryDAO aEmployeeJobLeftEntryDAO)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmployeeJobLeftId", aEmployeeJobLeftEntryDAO.EmployeeJobLeftId));
           aSqlParameterlist.Add(new SqlParameter("@IsDelete", aEmployeeJobLeftEntryDAO.IsDelete));
           aSqlParameterlist.Add(new SqlParameter("@DeleteBy", aEmployeeJobLeftEntryDAO.DeleteBy));
           aSqlParameterlist.Add(new SqlParameter("@DeleteDate", aEmployeeJobLeftEntryDAO.DeleteDate));


           const string query = @"Update tblEmployeeJobLeft  set IsDelete=@IsDelete, DeleteBy=@DeleteBy, DeleteDate=@DeleteDate  WHERE EmployeeJobLeftId = @EmployeeJobLeftId";
         //  const string query = @"DELETE FROM tblEmployeeJobLeft WHERE EmployeeJobLeftId = @EmployeeJobLeftId";
           return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
       }


       public DataTable GetEmployeeJobLeftEntryById(string id)
       {
           string query = @"SELECT  EG.EmpName AS EmployeeName , EG.EmpMasterCode, deg.Designation, SG.GradeCode+':'+ SG.GradeName AS GradeName,   Div.DivisionName, Wing.DivisionWingName, Sec.SectionName, SubSec.SubSectionName, Dpt.DepartmentName,  * FROM tblEmployeeJobLeft
LEFT JOIN dbo.tblEmpGeneralInfo EG ON tblEmployeeJobLeft.EmployeeId= EG.EmpInfoId 
LEFT JOIN dbo.tblDesignation  deg ON EG.DesignationId=deg.DesignationId
							LEFT JOIN dbo.tblSalaryGrade  SG ON EG.SalaryGradeId=SG.SalaryGradeId
							LEFT JOIN dbo.tblDivision  Div ON EG.DivisionId=Div.DivisionId
							LEFT JOIN dbo.tblDivisionWing  Wing ON EG.DivisionWId=Wing.DivisionWId
							LEFT JOIN dbo.tblSection  Sec ON EG.SectionId=Sec.SectionId
							LEFT JOIN dbo.tblSubSection  SubSec ON EG.SubSectionId=SubSec.SubSectionId						
								LEFT JOIN dbo.tblDepartment  Dpt ON EG.DepartmentId=Dpt.DepartmentId
								WHERE tblEmployeeJobLeft.EmployeeJobLeftId='" + id + "'";


           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }


       public bool EmployeeJobLeftUpsateInfo(EmployeeJobLeftEntryDAO aEmployeeJobLeftEntryDAO)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmployeeJobLeftId", aEmployeeJobLeftEntryDAO.EmployeeJobLeftId));

           aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aEmployeeJobLeftEntryDAO.EmployeeId));


           aSqlParameterlist.Add(new SqlParameter("@CompanyId", aEmployeeJobLeftEntryDAO.CompanyId));

           aSqlParameterlist.Add(new SqlParameter("@JobLeftTypeId", aEmployeeJobLeftEntryDAO.JobLeftTypeId));
           aSqlParameterlist.Add(new SqlParameter("@JobLeftDate", aEmployeeJobLeftEntryDAO.JobLeftDate));

           aSqlParameterlist.Add(new SqlParameter("@Reason", aEmployeeJobLeftEntryDAO.Reason));

           aSqlParameterlist.Add(new SqlParameter("@IsClearanceForm", aEmployeeJobLeftEntryDAO.IsClearanceForm ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsExitInterview", aEmployeeJobLeftEntryDAO.IsExitInterview ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aEmployeeJobLeftEntryDAO.UpdateBy));


           aSqlParameterlist.Add(new SqlParameter("@SubmissionDate", aEmployeeJobLeftEntryDAO.SubmissionDate));
           aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aEmployeeJobLeftEntryDAO.UpdateDate));

           string UpdateQuery = @"UPDATE  tblEmployeeJobLeft SET 
IsClearanceForm=@IsClearanceForm,
IsExitInterview=@IsExitInterview,
                              
                                    EmployeeId=@EmployeeId,
CompanyId=@CompanyId,
         
           JobLeftTypeId=@JobLeftTypeId,
           JobLeftDate=@JobLeftDate,
           Reason=@Reason,
            
           
           UpdateBy=@UpdateBy,
           UpdateDate=@UpdateDate, SubmissionDate=@SubmissionDate      WHERE EmployeeJobLeftId=@EmployeeJobLeftId";

           return aCommonInternalDal.UpdateDataByUpdateCommand(UpdateQuery, aSqlParameterlist, "HRDB");
       }
    }
}
