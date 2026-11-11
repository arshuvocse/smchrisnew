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
    public class EmployeeSeparationReportDAL
    {
       ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
       public void LoadCompanyDropDownList(DropDownList ddl)
       {
           string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
           //string query = "SELECT * FROM tblCompanyInfo";
           aCommonInternalDal.LoadDropDownValue(ddl, "CompanyName", "CompanyId", queryStr, "HRDB");
       }

       public DataTable ValidattionForEffectiveDate(string id, string date)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmployeeId", id));
           aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", date));
           string query = @" SELECT EmployeeId,JobLeftDate  FROM dbo.tblEmployeeJobLeft WHERE  EmployeeId=@EmployeeId and JobLeftDate=@EffectiveDate";
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
           string queryStr = @"SELECT *	 FROM tblJobLeftType  WITH (NOLOCK)";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }
       public DataTable LoadEmpJInfoInTextBoxById(int id)
       {
           string query = @" SELECT tblEmployeeType.EmpType EmployeeMentType, Egf.EmpMasterCode, Egf.EmpName, Egf.DateOfJoin,   deg.Designation, SG.GradeCode+' : '+ SG.GradeName SalaryGrade, Com.CompanyName, Div.DivisionName, Wing.DivisionWingName,  Dpt.DepartmentName,     Sec.SectionName, SubSec.SubSectionName, *  FROM dbo.tblEmpGeneralInfo Egf  WITH (NOLOCK)
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
           string queryStr = @"SELECT  EG.ContractPeriod, EG.DateOfJoin,  EG.SMCOldCode, ST.GrossAmount,  ISNULL(ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL),'') ParentsInfo , rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
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

CAST(DAY(EPE.JobLeftDate - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate), EG.DateOfJoin) > EPE.JobLeftDate THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  as LengthServicewithSMC, JType.JobLeftType, * From tblEmployeeJobLeft EPE
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
						   Where ( EPE.IsDelete IS NULL OR EPE.IsDelete = 0) " + param + "  ORDER BY EG.EmpMasterCode ASC";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }


       public DataTable LoadWPPFInformationALl(string param)
       {
           string queryStr = @"SELECT dtEmployeePromotion.EffectiveDate lastPromotionDate, dtDiciplinaryAction.EffectiveDate DisciplinaryIssuesDate, dtDiciplinaryAction.EmpType DiciplinaryActionType,  EPE.SubmissionDate SuspendDate,LL.JobLeftType SuspendActionType, EG.EmpMasterCode, EG.SMCOldCode, ETpe.EmpType JobLeftType,  rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.NationalIdNo, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate   ,
	     rptBody.EmpName Supervisor,
		
     (CASE WHEN EIM.ExitMasterId
IS NULL THEN 'Pending' ELSE 'Completed' END) AS ExitFormStatus,cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year , ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  as LengthServicewithSMC, EG.ContractPeriod, * From tblEmployeeJobLeft EPE
	    INNER JOIN dbo.tblEmpGeneralInfo  EG ON EPE.EmployeeId = EG.EmpInfoId
	                            INNER JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
								LEFT JOIN dbo.tblJobLeftType LL ON EPE.JobLeftTypeId = LL.JobLeftTypeId
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
						   LEFT JOIN dbo.tblExitInterviewFormMaster EIM ON EIM.EmployeeId = EG.EmpInfoId   
  left JOIN dbo.tblEmployeeType  ETpe ON eg.EmpTypeId = ETpe.EmpTypeId    
LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
  left JOIN (SELECT TOP 1 EmpInfoId,EffectiveDate,   tblSuspendReasonEntry.SuspendReasonEntry EmpType FROM  dbo.tblDiciplinaryAction
  LEFT JOIN dbo.tblSuspendReasonEntry ON tblSuspendReasonEntry.SuspendReasonEntryId = tblDiciplinaryAction.ReasonId
   ORDER BY  EffectiveDate desc)dtDiciplinaryAction ON dtDiciplinaryAction.EmpInfoId=EPE.EmployeeId                               
  left JOIN (SELECT TOP 1 EmpInfoId,EffectiveDate, tblSuspendReasonEntry.SuspendReasonEntry EmpType FROM  dbo.tblSuspend
  LEFT JOIN dbo.tblSuspendReasonEntry ON tblSuspendReasonEntry.SuspendReasonEntryId = tblSuspend.ReasonId
   ORDER BY  EffectiveDate desc)dtSuspend ON dtSuspend.EmpInfoId=EPE.EmployeeId                          
 
   left JOIN (SELECT EmployeeId,MAX(Effectivedate)AS Effectivedate  FROM  dbo.tblEmployeePromotionEntry GROUP BY EmployeeId  )dtEmployeePromotion ON dtEmployeePromotion.EmployeeId=EPE.EmployeeId  
                        
                        
 Where EG.EmpMasterCode IS NOT NULL   " + param + " ORDER BY EG.EmpMasterCode ASC";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }


       public DataTable LoadEmpIsActiveTrue(string param)
       {
           string queryStr = @"SELECT   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month' ServiceLengthMonth,   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, dtEmployeePromotion.EffectiveDate lastPromotionDate, dtDiciplinaryAction.EffectiveDate DisciplinaryIssuesDate, dtDiciplinaryAction.EmpType DiciplinaryActionType,  EPE.SubmissionDate SuspendDate,LL.JobLeftType SuspendActionType, EG.EmpMasterCode, EG.SMCOldCode, ETpe.EmpType JobLeftType,  rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.NationalIdNo, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
  EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate   ,
	     rptBody.EmpName Supervisor,
		
     (CASE WHEN EIM.ExitMasterId
IS NULL THEN 'Pending' ELSE 'Completed' END) AS ExitFormStatus,cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year , ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  as LengthServicewithSMC, EG.ContractPeriod, * From   dbo.tblEmpGeneralInfo  EG
	    left JOIN tblEmployeeJobLeft EPE ON EPE.EmployeeId = EG.EmpInfoId
	                            INNER JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
								LEFT JOIN dbo.tblJobLeftType LL ON EPE.JobLeftTypeId = LL.JobLeftTypeId
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
						   LEFT JOIN dbo.tblExitInterviewFormMaster EIM ON EIM.EmployeeId = EG.EmpInfoId   
  left JOIN dbo.tblEmployeeType  ETpe ON eg.EmpTypeId = ETpe.EmpTypeId    
LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
  left JOIN (SELECT TOP 1 EmpInfoId,EffectiveDate,   tblSuspendReasonEntry.SuspendReasonEntry EmpType FROM  dbo.tblDiciplinaryAction
  LEFT JOIN dbo.tblSuspendReasonEntry ON tblSuspendReasonEntry.SuspendReasonEntryId = tblDiciplinaryAction.ReasonId
   ORDER BY  EffectiveDate desc)dtDiciplinaryAction ON dtDiciplinaryAction.EmpInfoId=EPE.EmployeeId                               
  left JOIN (SELECT TOP 1 EmpInfoId,EffectiveDate, tblSuspendReasonEntry.SuspendReasonEntry EmpType FROM  dbo.tblSuspend
  LEFT JOIN dbo.tblSuspendReasonEntry ON tblSuspendReasonEntry.SuspendReasonEntryId = tblSuspend.ReasonId
   ORDER BY  EffectiveDate desc)dtSuspend ON dtSuspend.EmpInfoId=EPE.EmployeeId                          
 
   left JOIN (SELECT EmployeeId,MAX(Effectivedate)AS Effectivedate  FROM  dbo.tblEmployeePromotionEntry GROUP BY EmployeeId  )dtEmployeePromotion ON dtEmployeePromotion.EmployeeId=EPE.EmployeeId  
                        
                        
 Where EG.EmpMasterCode IS NOT NULL  AND EG.IsActive=1  AND  cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar)  >=6   " + param + " ORDER BY EG.EmpMasterCode ASC";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }


       public void FinYearByCompDropDown(DropDownList ddl, string id)
       {
           string queryStr = @"SELECT * FROM dbo.tblFinancialYear WHERE CompanyId='" + id + "'";
           aCommonInternalDal.LoadDropDownValue(ddl, "FinancialYearDesc", "FinancialYearId", queryStr, DataBase.HRDB);
       }

       public void SurveyByCompDropDown(DropDownList ddl, string id, string FinYear)
       {

           string queryStr = @"SELECT * FROM dbo.tblSurveyMaster WHERE IsActive=1 AND CompanyId='" + id + "' AND FinancialYearId='" + FinYear + "'";
           aCommonInternalDal.LoadDropDownValue(ddl, "SurveyName", "SurveyMasterId", queryStr, DataBase.HRDB);
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
