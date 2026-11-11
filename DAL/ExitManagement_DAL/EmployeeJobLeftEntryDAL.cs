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
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;
using Library.DAO.HRM_Entities;

namespace DAL.ExitManagement_DAL
{
   public class EmployeeJobLeftEntryDAL
    {
       ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();



       public DataTable GetDocDataById(string ID)
       {
           try
           {
               string query = @"SELECT  emp.EmpMasterCode, emp.EmpName + ISNULL(case when dtl.IsDone=0 then  isnull(  case when dtl.EmpInfoId=dtl.IsForwardEmpId then '' else ' [forwarded Employee Info: '+empFor.EmpMasterCode+' : '+empFor.EmpName+']'end,'') else '' end,'')    EmpName, dpt.DepartmentName, dtl.ApprovalStatus Rolev, CASE WHEN dtl.IsDone=1 THEN 'Done' ELSE 'Pending' END AS Status
  FROM [dbo].[tblEmpExitDetail] dtl WITH (NOLOCK)
  LEFT JOIN dbo.tblEmpGeneralInfo  emp ON emp.EmpInfoId = dtl.EmpInfoId
  LEFT JOIN dbo.tblEmpGeneralInfo  empFor ON empFor.EmpInfoId = dtl.IsForwardEmpId
  LEFT JOIN dbo.tblDepartment  dpt ON emp.DepartmentId = dpt.DepartmentId
  WHERE dtl.MasterId=" +
                              ID;

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }


       public DataTable GetDocDataByIdComment(string ID)
       {
           try
           {
               string query = @"  SELECT emp.EmpMasterCode EmpMasterCode, emp.EmpName EmpName,  ApprovalCondition,  Resource     from tblDepWiseClearanceResourceUpdate
								 inner join tblDepartment on tblDepWiseClearanceResourceUpdate.DepID=tblDepartment.DepartmentId
								 left  JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId=dbo.tblDepWiseClearanceResourceUpdate.IsDoneEmpId
								 left  JOIN dbo.tblEmpGeneralInfo empMain ON empMain.EmpInfoId=dbo.tblDepWiseClearanceResourceUpdate.EmpID
								 left  JOIN dbo.tblCompanyInfo com ON com.CompanyId=empMain.CompanyId



								  INNER JOIN dbo.tblDesignation dgs ON emp.DesignationId=dgs.DesignationId
								  where  ((Resource<>'')  OR (MainRemarks<>'')) AND   EmpID=" +
                              ID;

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

       public DataTable GetDocDataByIdOtherComment(string ID)
       {
           try
           {
               string query = @"  SELECT  DISTINCT  up.Resource, up.MainRemarks, emp.EmpMasterCode, emp.EmpName, dpt.DepartmentName, dtl.ApprovalStatus Rolev, CASE WHEN dtl.IsDone=1 THEN 'Done' ELSE 'Pending' END AS Status
  FROM [dbo].[tblEmpExitDetail] dtl WITH (NOLOCK)
  LEFT JOIN dbo.tblEmpGeneralInfo  emp ON emp.EmpInfoId = dtl.EmpInfoId
  LEFT JOIN dbo.tblDepartment  dpt ON emp.DepartmentId = dpt.DepartmentId
  LEFT JOIN tblDepWiseClearanceResourceUpdate up ON up.EmpID=dtl.EmployeeIdForClearance AND dtl.EmpInfoId=up.IsDoneEmpId

  WHERE    dtl.IsDone=1 AND dtl.MasterId=" +
                              ID;

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public DataTable LoadExitInterviewInformation(string param)
       {
           string queryStr = @"SELECT  EIM.ExitMasterId,  Emp.EmpName, Com.CompanyName, JType.JobLeftType, UserR.UserName AS EntryBy, UpBY.UserName AS UpdateBy ,EPE.*,(CASE WHEN EIM.ExitMasterId
                                 IS NULL THEN 'Pending' ELSE 'Completed' END)AS ExitFormStatus From tblEmployeeJobLeft EPE
                                 INNER JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
                                 INNER JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
                                 INNER JOIN dbo.tblJobLeftType  JType ON EPE.JobLeftTypeId = JType.JobLeftTypeId         
								 LEFT JOIN dbo.tblUser  UserR ON EPE.EntryBy = UserR.UserId                                  
						         LEFT JOIN dbo.tblUser  UpBY ON EPE.UpdateBy = UpBY.UserId   
						         INNER JOIN dbo.tblExitInterviewFormMaster EIM ON EIM.EmployeeId = EPE.EmployeeId                                  
                                 Where ( EPE.IsDelete IS NULL OR EPE.IsDelete = 0) AND EPE.IsExitInterview = 1 " + param + "  ORDER BY EPE.EmployeeJobLeftId DESC";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }
       public void LoadCompanyDropDownList(DropDownList ddl)
       {
           string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
           //string query = "SELECT * FROM tblCompanyInfo";
           aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
       }


       public bool DeleteJobCreationById(EmpExitMasterDao aJobCreationDao)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@ExitMasterId", aJobCreationDao.ExitMasterId));



            
           aSqlParameterlist.Add(new SqlParameter("@DeleteBy", aJobCreationDao.DeleteBy));
           aSqlParameterlist.Add(new SqlParameter("@DeleteDate", aJobCreationDao.DeleteDate));


           const string query = @"
SELECT  [ExitMasterId]
      ,[CompanyId]
      ,[EmployeeId]
      ,[EmpCode]
      ,[EmpName]
      ,[JoiningDate]
      ,[DivisionId]
      ,[DesignationId]
      ,[SalaryGradeId]
      ,[Description]
      ,[ActionStatus]
      ,[EntryBy]
      ,[EntryDate]
      ,[UpdateBy]
      ,[UpdateDate]
      ,[IsSales]
  FROM [dbo].[tblEmpExitMasterDEL]
GO


INSERT INTO tblEmpExitMasterDEL ([ExitMasterId]
      ,[CompanyId]
      ,[EmployeeId]
      ,[EmpCode]
      ,[EmpName]
      ,[JoiningDate]
      ,[DivisionId]
      ,[DesignationId]
      ,[SalaryGradeId]
      ,[Description]
      ,[ActionStatus]
      ,[EntryBy]
      ,[EntryDate]
      ,[UpdateBy]
      ,[UpdateDate]
      ,[IsSales],DeleteBy,DeleteDate)
SELECT [ExitMasterId]
      ,[CompanyId]
      ,[EmployeeId]
      ,[EmpCode]
      ,[EmpName]
      ,[JoiningDate]
      ,[DivisionId]
      ,[DesignationId]
      ,[SalaryGradeId]
      ,[Description]
      ,[ActionStatus]
      ,[EntryBy]
      ,[EntryDate]
      ,[UpdateBy]
      ,[UpdateDate]
      ,[IsSales],@DeleteBy,@DeleteDate
FROM tblEmpExitMaster
WHERE ExitMasterId=@ExitMasterId


DELETE FROM  tblEmpExitMaster WHERE ExitMasterId=@ExitMasterId";


           return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
       }
       public DataTable CheckEmpExitFormInfo(int empId)
       {
           string query = @"SELECT * FROM dbo.tblExitInterviewFormMaster WHERE EmployeeId = " + empId;
           return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
       }

       public DataTable CheckEmpClearenceFormInfo(int empId)
       {
           string query = @"SELECT * FROM dbo.tblEmpClearenceForm WHERE EmployeeId = " + empId;
           return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
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

       public void LoadJobLeftTypeDropDownList(DropDownList ddl)
       {
           string query = "SELECT * FROM tblJobLeftType";
           aCommonInternalDal.LoadDropDownValue(ddl, "JobLeftType", "JobLeftTypeId", query, "HRDB");
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

           aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aEmployeeJobLeftEntryDAO.EmployeeId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@CompanyId", aEmployeeJobLeftEntryDAO.CompanyId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@JobLeftTypeId", aEmployeeJobLeftEntryDAO.JobLeftTypeId ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@JobLeftDate", aEmployeeJobLeftEntryDAO.JobLeftDate ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@Reason", aEmployeeJobLeftEntryDAO.Reason ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@EntryBy", aEmployeeJobLeftEntryDAO.EntryBy));
           aSqlParameterlist.Add(new SqlParameter("@EntryDate", aEmployeeJobLeftEntryDAO.EntryDate));


           aSqlParameterlist.Add(new SqlParameter("@SubmissionDate", aEmployeeJobLeftEntryDAO.SubmissionDate ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsClearanceForm", aEmployeeJobLeftEntryDAO.IsClearanceForm ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsExitInterview", aEmployeeJobLeftEntryDAO.IsExitInterview ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@AutoProcess", aEmployeeJobLeftEntryDAO.AutoProcess ?? (object)DBNull.Value));


            

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
SubmissionDate,
AutoProcess

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
@SubmissionDate,@AutoProcess)";

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
           string queryStr = @"SELECT Emp.EmpMasterCode,  EPE.EmployeeId, Emp.EmpName, Com.CompanyName, JType.JobLeftType, UserR.UserName AS EntryBy, UpBY.UserName AS UpdateBy ,  CASE WHEN EPE.IsExitInterview=0 THEN 'Not Required' else (CASE WHEN EIM.ExitMasterId
IS NULL THEN 'Pending' ELSE 'Completed' END) END  AS ExitFormStatus,ActionStatus2,ForEmp.EmpName as AwEmpName,* From tblEmployeeJobLeft EPE
                                    INNER JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
                                    INNER JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
                                    INNER JOIN dbo.tblJobLeftType  JType ON EPE.JobLeftTypeId = JType.JobLeftTypeId         
								 LEFT JOIN dbo.tblUser  UserR ON EPE.EntryBy = UserR.UserId                                  
						  LEFT JOIN dbo.tblUser  UpBY ON EPE.UpdateBy = UpBY.UserId   
					 LEFT JOIN dbo.tblEmpExitMaster EIM ON EIM.EmployeeId = EPE.EmployeeId         
					  LEFT JOIN (SELECT EmployeeJobLeftId,MAX(Version)MaxVer FROM dbo.tblEmployeeJobLeftAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY EmployeeJobLeftId) AS LogApp ON LogApp.EmployeeJobLeftId= EPE.EmployeeJobLeftId
								LEFT JOIN dbo.tblEmployeeJobLeftAppLog ON tblEmployeeJobLeftAppLog.EmployeeJobLeftId = EPE.EmployeeJobLeftId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblEmployeeJobLeftAppLog.ForEmpInfoId
                                LEFT JOIN dbo.tblEmployeeJobLeftAppLog PreLog ON PreLog.EmployeeJobLeftId=EPE.EmployeeJobLeftId AND PreLog.Version = CONVERT(INT,LogApp.MaxVer)-1
								LEFT JOIN dbo.tblEmpGeneralInfo PreEmp ON PreEmp.EmpInfoId=PreLog.ForEmpInfoId                         
 Where ( EPE.IsDelete IS NULL OR EPE.IsDelete = 0) AND (tblEmployeeJobLeftAppLog.Version=LogApp.MaxVer OR tblEmployeeJobLeftAppLog.Version IS NULL)  " + param + "      ORDER BY EPE.JobLeftDate DESC";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }

       public DataTable LoadInformationALlList(string param, string paramTransfer, string ComId)
       {
           string queryStr = @"SELECT DISTINCT * FROM(SELECT epe.EntryDate, epe.UpdateDate,EPE.EmployeeJobLeftId, EPE.IsJobLeft, EPE.SubmissionDate,EPE.JobLeftDate,EPE.Reason,  Emp.EmpMasterCode,  EPE.EmployeeId, Emp.EmpName, Com.CompanyName, JType.JobLeftType, UserR.UserName AS EntryBy, UpBY.UserName AS UpdateBy ,  CASE WHEN EPE.IsExitInterview=0 THEN 'Not Required' else (CASE WHEN EIM.ExitMasterId
IS NULL THEN 'Pending' ELSE 'Completed' END) END  AS ExitFormStatus,ActionStatus2,ForEmp.EmpName as AwEmpName From tblEmployeeJobLeft EPE
                                    INNER JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
                                    INNER JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
                                    INNER JOIN dbo.tblJobLeftType  JType ON EPE.JobLeftTypeId = JType.JobLeftTypeId         
								 LEFT JOIN dbo.tblUser  UserR ON EPE.EntryBy = UserR.UserId                                  
						  LEFT JOIN dbo.tblUser  UpBY ON EPE.UpdateBy = UpBY.UserId   
					 LEFT JOIN dbo.tblEmpExitMaster EIM ON EIM.EmployeeId = EPE.EmployeeId         
					  LEFT JOIN (SELECT EmployeeJobLeftId,MAX(Version)MaxVer FROM dbo.tblEmployeeJobLeftAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY EmployeeJobLeftId) AS LogApp ON LogApp.EmployeeJobLeftId= EPE.EmployeeJobLeftId
								LEFT JOIN dbo.tblEmployeeJobLeftAppLog ON tblEmployeeJobLeftAppLog.EmployeeJobLeftId = EPE.EmployeeJobLeftId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblEmployeeJobLeftAppLog.ForEmpInfoId
                                LEFT JOIN dbo.tblEmployeeJobLeftAppLog PreLog ON PreLog.EmployeeJobLeftId=EPE.EmployeeJobLeftId AND PreLog.Version = CONVERT(INT,LogApp.MaxVer)-1
								LEFT JOIN dbo.tblEmpGeneralInfo PreEmp ON PreEmp.EmpInfoId=PreLog.ForEmpInfoId                         
 Where ( EPE.IsDelete IS NULL OR EPE.IsDelete = 0) AND (tblEmployeeJobLeftAppLog.Version=LogApp.MaxVer OR tblEmployeeJobLeftAppLog.Version IS NULL)  " + param + @"    
union All 

 SELECT '' EntryDate, '' UpdateDate, EPE.EmployeeJobLeftId, EPE.IsJobLeft, EPE.SubmissionDate,EPE.JobLeftDate,EPE.Reason,  Emp.EmpMasterCode,  EPE.EmployeeId, Emp.EmpName, Com.CompanyName, JType.JobLeftType, UserR.UserName AS EntryBy, UpBY.UserName AS UpdateBy ,  CASE WHEN EPE.IsExitInterview=0 THEN 'Not Required' else (CASE WHEN EIM.ExitMasterId
IS NULL THEN 'Pending' ELSE 'Completed' END) END  AS ExitFormStatus,ActionStatus2,ForEmp.EmpName as AwEmpName  From tblEmployeeJobLeft EPE
                                    INNER JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
  inner JOIN   tblEmpAllRefference reff  ON EPE.EmployeeId = reff.RefferenceEmpId   

                                    INNER JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
                                    INNER JOIN dbo.tblJobLeftType  JType ON EPE.JobLeftTypeId = JType.JobLeftTypeId         
								 LEFT JOIN dbo.tblUser  UserR ON EPE.EntryBy = UserR.UserId                                  
						  LEFT JOIN dbo.tblUser  UpBY ON EPE.UpdateBy = UpBY.UserId   
					 LEFT JOIN dbo.tblEmpExitMaster EIM ON EIM.EmployeeId = EPE.EmployeeId         
					  LEFT JOIN (SELECT EmployeeJobLeftId,MAX(Version)MaxVer FROM dbo.tblEmployeeJobLeftAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY EmployeeJobLeftId) AS LogApp ON LogApp.EmployeeJobLeftId= EPE.EmployeeJobLeftId
								LEFT JOIN dbo.tblEmployeeJobLeftAppLog ON tblEmployeeJobLeftAppLog.EmployeeJobLeftId = EPE.EmployeeJobLeftId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblEmployeeJobLeftAppLog.ForEmpInfoId
                                LEFT JOIN dbo.tblEmployeeJobLeftAppLog PreLog ON PreLog.EmployeeJobLeftId=EPE.EmployeeJobLeftId AND PreLog.Version = CONVERT(INT,LogApp.MaxVer)-1
								LEFT JOIN dbo.tblEmpGeneralInfo PreEmp ON PreEmp.EmpInfoId=PreLog.ForEmpInfoId  
								
								 inner join (select   NewEmployeeId,OnlyViewComId from tblEmpSpecialTransfer where OnlyView=1) tblPer on reff.EmployeeId =tblPer.NewEmployeeId                       
 Where ( EPE.IsDelete IS NULL OR EPE.IsDelete = 0) AND (tblEmployeeJobLeftAppLog.Version=LogApp.MaxVer OR tblEmployeeJobLeftAppLog.Version IS NULL) " + paramTransfer + " )HH      ORDER BY HH.JobLeftDate DESC";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }
       public DataTable LoadInformationALlApproval(string param)
       {
           string queryStr = @"SELECT tblD.ApprovalStatus ApprovalStatusShow,Emp.EmpMasterCode,  EPE.EmployeeId, Emp.EmpName, Com.CompanyName, JType.JobLeftType, UserR.UserName AS EntryBy, UpBY.UserName AS UpdateBy ,  CASE WHEN EPE.IsExitInterview=0 THEN 'Not Required' else (CASE WHEN EIM.ExitMasterId
IS NULL THEN 'Pending' ELSE 'Completed' END) END  AS ExitFormStatus,ActionStatus2, * From tblEmployeeJobLeft EPE
                                    INNER JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
                                    INNER JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
                                    INNER JOIN dbo.tblJobLeftType  JType ON EPE.JobLeftTypeId = JType.JobLeftTypeId         
								 LEFT JOIN dbo.tblUser  UserR ON EPE.EntryBy = UserR.UserId                                  
						  LEFT JOIN dbo.tblUser  UpBY ON EPE.UpdateBy = UpBY.UserId   
					 LEFT JOIN dbo.tblEmpExitMaster EIM ON EIM.EmployeeId = EPE.EmployeeId 
					 --INNER JOIN dbo.tblEmpExitDetail EIMd ON EIMd.MasterId = EIM.ExitMasterId 

					  LEFT JOIN (SELECT DISTINCT E.ExitDetailId,MasterId, ApprovalStatus FROM tblEmpExitDetail E WHERE E.IsDone=0 AND E.EmpInfoIdApproval='" + HttpContext.Current.Session["EmpInfoId"].ToString() + @"')tblD ON EIM.ExitMasterId=tblD.MasterId
					 
					         
					  LEFT JOIN (SELECT EmployeeJobLeftId,MAX(Version)MaxVer FROM dbo.tblEmployeeJobLeftAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY EmployeeJobLeftId) AS LogApp ON LogApp.EmployeeJobLeftId= EPE.EmployeeJobLeftId
							--	LEFT JOIN dbo.tblEmployeeJobLeftAppLog ON tblEmployeeJobLeftAppLog.EmployeeJobLeftId = EPE.EmployeeJobLeftId
							--	LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblEmployeeJobLeftAppLog.ForEmpInfoId
                                LEFT JOIN dbo.tblEmployeeJobLeftAppLog PreLog ON PreLog.EmployeeJobLeftId=EPE.EmployeeJobLeftId AND PreLog.Version = CONVERT(INT,LogApp.MaxVer)-1
								LEFT JOIN dbo.tblEmpGeneralInfo PreEmp ON PreEmp.EmpInfoId=PreLog.ForEmpInfoId                         
 Where ( EPE.IsDelete IS NULL OR EPE.IsDelete = 0)    " + param + "  ORDER BY EPE.JobLeftDate DESC";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }


       public DataTable GetPendingClearanceMasterIds(string currentEmpId)
       {
           string query = @"SELECT DISTINCT MasterId FROM dbo.tblEmpExitDetail 
               WHERE IsDone = 0 
                 AND (
                     (
                         EmpInfoIdApproval = '" + currentEmpId + @"'
                         AND (
                             ISNULL(isForwardBackDone, 0) = 1
                             OR EmpInfoIdApproval NOT IN (
                                 SELECT m.EmployeeId 
                                 FROM dbo.tblClearanceForwardingMaster m 
                                 INNER JOIN dbo.tblClearanceForwardingDetail d ON m.ClearanceForwardingId = d.ClearanceForwardingId 
                                 WHERE m.Status = 'Forwarded' AND d.Status = 'Pending'
                             )
                         )
                     )
                     OR (
                         ISNULL(isForwardBackDone, 0) = 0
                         AND EmpInfoIdApproval IN (
                             SELECT m.EmployeeId 
                             FROM dbo.tblClearanceForwardingMaster m 
                             INNER JOIN dbo.tblClearanceForwardingDetail d ON m.ClearanceForwardingId = d.ClearanceForwardingId 
                             WHERE d.SupervisedEmployeeId = '" + currentEmpId + @"' 
                               AND d.Status = 'Pending' 
                               AND m.Status = 'Forwarded'
                         )
                     )
                 )";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }

       public DataTable LoadInformationALlApprovalNew(string param)
       {
           string currentEmpId = (HttpContext.Current != null && HttpContext.Current.Session != null && HttpContext.Current.Session["EmpInfoId"] != null)
               ? HttpContext.Current.Session["EmpInfoId"].ToString()
               : "0";
           string queryStr = @"SELECT DISTINCT    STUFF((
    SELECT ', ' + emp2.EmpName + ' (' + emp2.EmpMasterCode + ')'
        + ISNULL(
            CASE WHEN dtl2.IsDone = 0 AND dtl2.EmpInfoId <> dtl2.EmpInfoIdApproval THEN 
                ' [forwarded Employee Info: ' + empFor2.EmpName + ' (' + empFor2.EmpMasterCode + ')]'
            ELSE '' END
        , '')
        + ISNULL((
            SELECT ' [Forwarded To: ' + STUFF((
                SELECT '; ' + fEmp.EmpName + ' (' + fEmp.EmpMasterCode + ')' + CASE WHEN ISNULL(fDtl.OrderNo, 1) = 1 THEN ' [Primary Person]' ELSE '' END
                FROM dbo.tblClearanceForwardingDetail fDtl
                INNER JOIN dbo.tblEmpGeneralInfo fEmp ON fDtl.SupervisedEmployeeId = fEmp.EmpInfoId
                WHERE fDtl.ClearanceForwardingId = fMst.ClearanceForwardingId AND fDtl.Status = 'Pending'
                ORDER BY ISNULL(fDtl.OrderNo, 99999), fDtl.ClearanceForwardingDetailId ASC
                FOR XML PATH(''), TYPE
            ).value('.', 'nvarchar(max)'), 1, 2, '') + ']'
            FROM dbo.tblClearanceForwardingMaster fMst
            WHERE fMst.EmployeeId = dtl2.EmpInfoIdApproval AND fMst.Status = 'Forwarded'
        ), '')
        + CASE WHEN dtl2.IsDone = 1 THEN '| (Done)' ELSE '| (Pending)' END
    FROM dbo.tblEmpExitDetail dtl2
    LEFT JOIN dbo.tblEmpGeneralInfo emp2 ON emp2.EmpInfoId = dtl2.EmpInfoId
    LEFT JOIN dbo.tblEmpGeneralInfo empFor2 ON empFor2.EmpInfoId = dtl2.IsForwardEmpId
    WHERE dtl2.MasterId = EIM.ExitMasterId
    FOR XML PATH(''), TYPE
).value('.', 'nvarchar(max)'), 1, 2, '') AS EmpNameStatusforAssaignList,    dtlX.empinfoid empinfoidForMain, dtlX.EmpInfoIdApproval,  case when dtlX.IsForward=0 and isnull(dtlX.isForwardBackDone,0)=0 and dtlX.IsForwardEmpId  is not null  then CAST(isnull(DATEDIFF(
                DAY,
                   CONVERT(date,dtlX.IsForwardDate)
                 ,
                ISNULL(CONVERT(date,dtlX.isForwardBackDoneDate), CONVERT(date,GETDATE()))
             ) ,0)+1 as nvarchar(max))  else 
CAST(
    CASE
        WHEN dtlX.IsDone = 1
        THEN isnull(DATEDIFF(
                DAY,
                CONVERT(date,EIM.EntryDate),
                ISNULL(CONVERT(date,clrForm.IsDoneDate), CONVERT(date,GETDATE()))
             ) ,0)+1
        WHEN ISNULL(dtlX.IsDone, 0) = 0
        THEN isnull(DATEDIFF(
                DAY,
                CONVERT(date,EIM.EntryDate),
                CONVERT(date,GETDATE())
             )  ,0)+1
        ELSE 0
    END AS NVARCHAR(MAX)
)   -isnull(case when dtlX.IsForwardDate is not null and dtlX.isForwardBackDoneDate is not null  then isnull(DATEDIFF(
                DAY,
                   CONVERT(date,dtlX.IsForwardDate)
                 ,
                 CONVERT(date,dtlX.isForwardBackDoneDate) 
             ) ,0) end,0) end  PendingDaysInfo,
case when dtlX.IsForward=0 and isnull(dtlX.isForwardBackDone,0)=0 and dtlX.IsForwardEmpId  is not null  then    EmpMain.EmpName +' ('+EmpMain.EmpMasterCode+') ' +' Pending' when dtlX.IsForward=1 and isnull(dtlX.isForwardBackDone,0)=1   then    Empforward.EmpName +' ('+Empforward.EmpMasterCode+') ' + ' Done' when dtlX.EmpInfoIdApproval IS NOT NULL AND dtlX.EmpInfoIdApproval <> '" + currentEmpId + @"' then '[Auto Forward] Forwarded from ' + EmpMain.EmpName +' ('+EmpMain.EmpMasterCode+') Pending' else 'N/A' end Forward,
Emp.DivisionId,
EPE.JobLeftDate,
'' ApprovalStatus,
EPE.Reason,
EIM.ExitMasterId,
EPE.EmployeeJobLeftId,
tblD.ExitDetailId,
tblD.ApprovalStatus ApprovalStatusShow,
Emp.EmpMasterCode,
EIM.EmployeeId,
Emp.EmpName,
Com.ShortName CompanyName,
JType.JobLeftType,   dgs.Designation DesignationName, sal.SalaryLocation,
CASE WHEN EPE.IsExitInterview=0 THEN 'Not Required' 
     else (CASE WHEN EIM.ExitMasterId IS NULL THEN 'Pending' ELSE 'Completed' END) 
END AS ExitFormStatus,
ActionStatus2,
case when UserR.EmpInfoId is null then EIM.EntryBy 
     else UserEmp.EmpMasterCode + ' : ' + UserEmp.EmpName 
end EmpEntryBy,
EIM.EntryDate

From dbo.tblEmpExitMaster EIM
LEFT JOIN tblEmployeeJobLeft EPE ON EIM.EmployeeId = EPE.EmployeeId 


-- ✅ FIX: LEFT JOIN এর বদলে OUTER APPLY TOP 1
OUTER APPLY (
    SELECT TOP 1 *
    FROM dbo.tblEmpExitDetail
    WHERE MasterId = EIM.ExitMasterId
      AND (
          (
              EmpInfoIdApproval = '" + currentEmpId + @"'
              AND (
                  ISNULL(isForwardBackDone, 0) = 1
                  OR EmpInfoIdApproval NOT IN (
                      SELECT m.EmployeeId 
                      FROM dbo.tblClearanceForwardingMaster m 
                      INNER JOIN dbo.tblClearanceForwardingDetail d ON m.ClearanceForwardingId = d.ClearanceForwardingId 
                      WHERE m.Status = 'Forwarded' AND d.Status = 'Pending'
                  )
              )
          )
          OR (
              ISNULL(isForwardBackDone, 0) = 0
              AND EmpInfoIdApproval IN (
                  SELECT m.EmployeeId 
                  FROM dbo.tblClearanceForwardingMaster m 
                  INNER JOIN dbo.tblClearanceForwardingDetail d ON m.ClearanceForwardingId = d.ClearanceForwardingId 
                  WHERE d.SupervisedEmployeeId = '" + currentEmpId + @"' 
                    AND d.Status = 'Pending' 
                    AND m.Status = 'Forwarded'
              )
          )
      )
      AND IsDone = 0
    ORDER BY CASE WHEN EmpInfoIdApproval = '" + currentEmpId + @"' THEN 0 ELSE 1 END, ExitDetailId DESC
) dtlX

LEFT JOIN dbo.tblDepWiseClearanceResourceUpdate clrForm 
    ON clrForm.exitDetailIdNew = dtlX.ExitDetailId
LEFT JOIN dbo.tblEmpGeneralInfo Emp ON EIM.EmployeeId = Emp.EmpInfoId
INNER JOIN dbo.tblCompanyInfo Com ON Emp.CompanyId = Com.CompanyId
LEFT JOIN dbo.tblDesignation dgs ON dgs.DesignationId = Emp.DesignationId
LEFT JOIN dbo.tblSalaryLocation sal ON sal.SalaryLoationId = Emp.SalaryLoationId
LEFT JOIN dbo.tblJobLeftType JType ON EPE.JobLeftTypeId = JType.JobLeftTypeId
LEFT JOIN dbo.tblUser UserR ON EIM.EntryBy = UserR.UserName
LEFT JOIN dbo.tblEmpGeneralInfo UserEmp ON UserR.EmpInfoId = UserEmp.EmpInfoId
LEFT JOIN dbo.tblUser UpBY ON EPE.UpdateBy = UpBY.UserId

OUTER APPLY (
    SELECT TOP 1 E.ExitDetailId, E.MasterId, E.ApprovalStatus 
    FROM dbo.tblEmpExitDetail E 
    WHERE E.MasterId = EIM.ExitMasterId 
      AND E.IsDone = 0 
      AND (
          (
              E.EmpInfoIdApproval = '" + currentEmpId + @"'
              AND (
                  ISNULL(E.isForwardBackDone, 0) = 1
                  OR E.EmpInfoIdApproval NOT IN (
                      SELECT m.EmployeeId 
                      FROM dbo.tblClearanceForwardingMaster m 
                      INNER JOIN dbo.tblClearanceForwardingDetail d ON m.ClearanceForwardingId = d.ClearanceForwardingId 
                      WHERE m.Status = 'Forwarded' AND d.Status = 'Pending'
                  )
              )
          )
          OR (
              ISNULL(E.isForwardBackDone, 0) = 0
              AND E.EmpInfoIdApproval IN (
                  SELECT m.EmployeeId 
                  FROM dbo.tblClearanceForwardingMaster m 
                  INNER JOIN dbo.tblClearanceForwardingDetail d ON m.ClearanceForwardingId = d.ClearanceForwardingId 
                  WHERE d.SupervisedEmployeeId = '" + currentEmpId + @"' 
                    AND d.Status = 'Pending' 
                    AND m.Status = 'Forwarded'
              )
          )
      )
    ORDER BY CASE WHEN E.EmpInfoIdApproval = '" + currentEmpId + @"' THEN 0 ELSE 1 END, E.ExitDetailId DESC
) tblD

LEFT JOIN dbo.tblEmpGeneralInfo EmpMain ON dtlX.EmpInfoId = EmpMain.EmpInfoId

LEFT JOIN dbo.tblEmpGeneralInfo Empforward ON dtlX.IsForwardEmpId = Empforward.EmpInfoId

OUTER APPLY (
    SELECT TOP 1 
        clrSup.IsDoneDate AS SupervisorDoneDate
    FROM dbo.tblEmpExitDetail sup
    LEFT JOIN dbo.tblDepWiseClearanceResourceUpdate clrSup 
        ON clrSup.exitDetailIdNew = sup.ExitDetailId
    WHERE sup.MasterId = dtlX.MasterId
       
      AND sup.IsDone = 1
    ORDER BY clrSup.IsDoneDate DESC
) supDone

LEFT JOIN (
    SELECT EmpID, max(IsDoneDate) IsDoneDate 
    FROM tblDepWiseClearanceResourceUpdate 
    GROUP BY EmpID
) tblcmt ON tblcmt.EmpID = EIM.EmployeeId

					 
					 
					        
                                                          
 Where   ( EPE.IsDelete IS NULL OR EPE.IsDelete = 0) AND   " + param + "  ORDER BY EPE.JobLeftDate DESC";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }

       public DataTable ClearenceFormCHECKFORpHARMA(string param)
       {
           string queryStr = @"     SELECT  case when  ISNULL(tblCount2.AppCount,0)-2=ISNULL(tblCount.AppCount,0) then '1' else '0' end AppCountICT ,  case when  ISNULL(tblCount2.AppCount,0)-1=ISNULL(tblCount.AppCount,0) then '1' else '0' end AppCount  FROM tblEmpExitMaster mas 
LEFT JOIN dbo.tblEmpGeneralInfo emp ON mas.EmployeeId=emp.EmpInfoId
LEFT JOIN dbo.tblDesignation dgs ON dgs.DesignationId=emp.DesignationId 
left join (select ISNULL(COUNT(*),0) AppCount,dtl.MasterId from [tblEmpExitDetail] dtl where IsDone=1 group by dtl.MasterId)tblCount on tblCount.MasterId=mas.ExitMasterId

left join (select ISNULL(COUNT(*),0) AppCount,dtl.MasterId from [tblEmpExitDetail] dtl  group by dtl.MasterId)tblCount2 on tblCount2.MasterId=mas.ExitMasterId  WHERE mas.ExitMasterId=" + param + "  ORDER BY mas.EntryDate DESC";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }
       public DataTable LoadInformationALlApprovalsss(string param)
        {
            string sessionEmpId = (HttpContext.Current.Session["EmpInfoId"] != null) ? HttpContext.Current.Session["EmpInfoId"].ToString() : "0";
            string queryStr = @"SELECT  tblD.ApprovalStatus ApprovalStatusShow,  dgs.Designation DesignationName, sal.SalaryLocation, tblD.ApprovedBy, tblD.ForwardedTo, Emp.EmpMasterCode,  EPE.EmployeeId, Emp.EmpName, Com.CompanyName, JType.JobLeftType, UserR.UserName AS EntryBy, '' UpdateBy ,  CASE WHEN EPE.IsExitInterview=0 THEN 'Not Required' else (CASE WHEN EIM.ExitMasterId
IS NULL THEN 'Pending' ELSE 'Completed' END) END  AS ExitFormStatus,ActionStatus2, case when UserR.EmpInfoId is null then  EIM.EntryBy else  UserEmp.EmpMasterCode+ ' : ' + UserEmp.EmpName end EmpEntryBy  ,EIM.EntryDate, * From tblEmployeeJobLeft EPE
                                    INNER JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
LEFT JOIN dbo.tblDesignation dgs ON dgs.DesignationId = Emp.DesignationId
LEFT JOIN dbo.tblSalaryLocation sal ON sal.SalaryLoationId = Emp.SalaryLoationId
                                    INNER JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
                                    INNER JOIN dbo.tblJobLeftType  JType ON EPE.JobLeftTypeId = JType.JobLeftTypeId         
						 
					 LEFT JOIN dbo.tblEmpExitMaster EIM ON EIM.EmployeeId = EPE.EmployeeId 
	 LEFT JOIN dbo.tblUser  UserR ON EIM.EntryBy = UserR.UserName
								 LEFT JOIN dbo.tblEmpGeneralInfo  UserEmp ON UserR.EmpInfoId = UserEmp.EmpInfoId                        								                                 
						 
					 --INNER JOIN dbo.tblEmpExitDetail EIMd ON EIMd.MasterId = EIM.ExitMasterId 
						                      							
					  LEFT JOIN (
                          SELECT DISTINCT 
                              E.ExitDetailId,
                              E.MasterId, 
                              E.ApprovalStatus,
                              CASE 
                                  WHEN empAppr.EmpInfoId IS NOT NULL 
                                  THEN empAppr.EmpMasterCode + ' : ' + empAppr.EmpName 
                                  ELSE '' 
                              END AS ApprovedBy,
                              CASE 
                                  WHEN empFwd.EmpInfoId IS NOT NULL 
                                  THEN empFwd.EmpMasterCode + ' : ' + empFwd.EmpName 
                                  WHEN fEmp.EmpInfoId IS NOT NULL
                                  THEN fEmp.EmpMasterCode + ' : ' + fEmp.EmpName
                                  ELSE '' 
                              END AS ForwardedTo
                          FROM tblEmpExitDetail E 
                          OUTER APPLY (
                              SELECT TOP 1 upd.IsDoneEmpId
                              FROM dbo.tblDepWiseClearanceResourceUpdate upd
                              WHERE upd.exitDetailIdNew = E.ExitDetailId
                              ORDER BY upd.ID DESC
                          ) updAppr
                          LEFT JOIN dbo.tblEmpGeneralInfo empAppr ON COALESCE(updAppr.IsDoneEmpId, E.EmpInfoIdApproval) = empAppr.EmpInfoId
                          LEFT JOIN dbo.tblEmpGeneralInfo empFwd ON E.IsForwardEmpId = empFwd.EmpInfoId
                          OUTER APPLY (
                              SELECT TOP 1 subEmp.EmpMasterCode, subEmp.EmpName, subEmp.EmpInfoId
                              FROM dbo.tblClearanceForwardingMaster fMst
                              INNER JOIN dbo.tblClearanceForwardingDetail fDtl ON fMst.ClearanceForwardingId = fDtl.ClearanceForwardingId
                              INNER JOIN dbo.tblEmpGeneralInfo subEmp ON fDtl.SupervisedEmployeeId = subEmp.EmpInfoId
                              WHERE fMst.EmployeeId = E.EmpInfoId AND fMst.Status = 'Forwarded'
                              ORDER BY ISNULL(fDtl.OrderNo, 99999), fDtl.ClearanceForwardingDetailId ASC
                          ) fEmp
                          WHERE E.IsDone = 1 
                            AND (
                                (E.EmpInfoIdApproval = '" + sessionEmpId + @"')  
                                OR (E.EmpInfoId = '" + sessionEmpId + @"')  
                                OR (E.IsForwardEmpId = '" + sessionEmpId + @"')
                                OR (E.EmpInfoIdApproval IN (
                                    SELECT m.EmployeeId 
                                    FROM dbo.tblClearanceForwardingMaster m 
                                    INNER JOIN dbo.tblClearanceForwardingDetail d ON m.ClearanceForwardingId = d.ClearanceForwardingId 
                                    WHERE d.SupervisedEmployeeId = '" + sessionEmpId + @"'
                                ))
                            )
                      ) tblD ON EIM.ExitMasterId = tblD.MasterId
					 
					         
					  LEFT JOIN (SELECT EmployeeJobLeftId,MAX(Version)MaxVer FROM dbo.tblEmployeeJobLeftAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY EmployeeJobLeftId) AS LogApp ON LogApp.EmployeeJobLeftId= EPE.EmployeeJobLeftId
							--	LEFT JOIN dbo.tblEmployeeJobLeftAppLog ON tblEmployeeJobLeftAppLog.EmployeeJobLeftId = EPE.EmployeeJobLeftId
							--	LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblEmployeeJobLeftAppLog.ForEmpInfoId
                                 LEFT JOIN dbo.tblEmployeeJobLeftAppLog PreLog ON PreLog.EmployeeJobLeftId=EPE.EmployeeJobLeftId AND PreLog.Version = CONVERT(INT,LogApp.MaxVer)-1
								LEFT JOIN dbo.tblEmpGeneralInfo PreEmp ON PreEmp.EmpInfoId=PreLog.ForEmpInfoId                         
 Where ( EPE.IsDelete IS NULL OR EPE.IsDelete = 0)    " + param + "  ORDER BY EPE.JobLeftDate DESC";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

       public DataTable LoadInformationALlApprovalNewCheck(string Emid, string param)
       {
           string queryStr = @"SELECT DISTINCT EPE.JobLeftDate, ''ApprovalStatus, EPE.Reason, EIM.ExitMasterId, EPE.EmployeeJobLeftId, tblD.ExitDetailId, tblD.ApprovalStatus ApprovalStatusShow,Emp.EmpMasterCode,  EIM.EmployeeId, Emp.EmpName, Com.ShortName CompanyName, JType.JobLeftType,   CASE WHEN EPE.IsExitInterview=0 THEN 'Not Required' else (CASE WHEN EIM.ExitMasterId
IS NULL THEN 'Pending' ELSE 'Completed' END) END  AS ExitFormStatus,ActionStatus2  From dbo.tblEmpExitMaster EIM
	 LEFT JOIN tblEmployeeJobLeft EPE  ON EIM.EmployeeId = EPE.EmployeeId 
                                    left JOIN dbo.tblEmpGeneralInfo  Emp ON EIM.EmployeeId = Emp.EmpInfoId
                                    INNER JOIN dbo.tblCompanyInfo  Com ON Emp.CompanyId = Com.CompanyId
                                    left JOIN dbo.tblJobLeftType  JType ON EPE.JobLeftTypeId = JType.JobLeftTypeId         
								 LEFT JOIN dbo.tblUser  UserR ON EPE.EntryBy = UserR.UserId                                  
						  LEFT JOIN dbo.tblUser  UpBY ON EPE.UpdateBy = UpBY.UserId   
				 

					  LEFT JOIN (SELECT DISTINCT E.ExitDetailId,MasterId, ApprovalStatus FROM tblEmpExitDetail E WHERE E.IsDone=0 AND E.EmpInfoIdApproval='" + Emid + @"')tblD ON EIM.ExitMasterId=tblD.MasterId
					 
					        
                                                          
 Where   ( EPE.IsDelete IS NULL OR EPE.IsDelete = 0) AND   " + param + "  ORDER BY EPE.JobLeftDate DESC";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }

       public DataTable LoadInformationALlApprovalForIT(string param)
       {
           string queryStr = @"SELECT tblD.ApprovalStatus ApprovalStatusShow,Emp.EmpMasterCode,  EPE.EmployeeId, Emp.EmpName, Com.CompanyName, JType.JobLeftType, UserR.UserName AS EntryBy, UpBY.UserName AS UpdateBy ,  CASE WHEN EPE.IsExitInterview=0 THEN 'Not Required' else (CASE WHEN EIM.ExitMasterId
IS NULL THEN 'Pending' ELSE 'Completed' END) END  AS ExitFormStatus,ActionStatus2, * From tblEmployeeJobLeft EPE
                                    INNER JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
                                    INNER JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
                                    INNER JOIN dbo.tblJobLeftType  JType ON EPE.JobLeftTypeId = JType.JobLeftTypeId         
								 LEFT JOIN dbo.tblUser  UserR ON EPE.EntryBy = UserR.UserId                                  
						  LEFT JOIN dbo.tblUser  UpBY ON EPE.UpdateBy = UpBY.UserId   
					 LEFT JOIN dbo.tblEmpExitMaster EIM ON EIM.EmployeeId = EPE.EmployeeId 
					 --INNER JOIN dbo.tblEmpExitDetail EIMd ON EIMd.MasterId = EIM.ExitMasterId 

					  LEFT JOIN (SELECT DISTINCT E.ExitDetailId,MasterId, ApprovalStatus FROM tblEmpExitDetail E WHERE E.IsDone=0 AND E.EmpInfoIdApproval='" + HttpContext.Current.Session["EmpInfoId"].ToString() + @"')tblD ON EIM.ExitMasterId=tblD.MasterId
					 
					         
					  LEFT JOIN (SELECT EmployeeJobLeftId,MAX(Version)MaxVer FROM dbo.tblEmployeeJobLeftAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY EmployeeJobLeftId) AS LogApp ON LogApp.EmployeeJobLeftId= EPE.EmployeeJobLeftId
							--	LEFT JOIN dbo.tblEmployeeJobLeftAppLog ON tblEmployeeJobLeftAppLog.EmployeeJobLeftId = EPE.EmployeeJobLeftId
							--	LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblEmployeeJobLeftAppLog.ForEmpInfoId
                                LEFT JOIN dbo.tblEmployeeJobLeftAppLog PreLog ON PreLog.EmployeeJobLeftId=EPE.EmployeeJobLeftId AND PreLog.Version = CONVERT(INT,LogApp.MaxVer)-1
								LEFT JOIN dbo.tblEmpGeneralInfo PreEmp ON PreEmp.EmpInfoId=PreLog.ForEmpInfoId                         
 Where ( EPE.IsDelete IS NULL OR EPE.IsDelete = 0)    " + param + "  ORDER BY EPE.JobLeftDate DESC";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }



       public DataTable ClearenceFormSetupView(string param, string paramAll)
       {
           string queryStr = @"    select distinct *  
    ,
    DATEDIFF(DAY, CONVERT(date,tbl.EntryDate), isnull( case when ApprovalStatus='Completed' then tblcmt.IsDoneDate else getdate() end,getdate()))+1 AS TotalDays from (  SELECT   EPE.JobLeftDate, DeclineComment, isnull(mas.IsRunning,0) IsRunning, mas.EntryDate, mas.EntryBy, dgs.Designation, Emp.EmpName,  Emp.EmpMasterCode, Emp.DivisionId, Emp.DepartmentId, mas.ExitMasterId, mas.EmployeeId, case when tblCount2.AppCount IS NOT NULL AND tblCount2.AppCount=tblCount.AppCount then 'Completed' else 'Pending' end ApprovalStatus, case when tblCount2.AppCount IS NOT NULL AND tblCount2.AppCount=tblCount.AppCount then 'btn btn-info btn-sm ' else '  btn btn-danger btn-sm' end AppCount  FROM tblEmpExitMaster mas 
LEFT JOIN dbo.tblEmpGeneralInfo emp ON mas.EmployeeId=emp.EmpInfoId
LEFT JOIN tblEmployeeJobLeft EPE  ON mas.EmployeeId = EPE.EmployeeId 
LEFT JOIN dbo.tblDesignation dgs ON dgs.DesignationId=emp.DesignationId 
left join (select ISNULL(COUNT(*),0) AppCount,dtl.MasterId from [tblEmpExitDetail] dtl where IsDone=1 group by dtl.MasterId)tblCount on tblCount.MasterId=mas.ExitMasterId

left join (select ISNULL(COUNT(*),0) AppCount,dtl.MasterId from [tblEmpExitDetail] dtl  group by dtl.MasterId)tblCount2 on tblCount2.MasterId=mas.ExitMasterId   WHERE emp.CompanyId=" + param +   @"  
union all 

 SELECT  EPE.JobLeftDate,   DeclineComment, isnull(mas.IsRunning,0) IsRunning,mas.EntryDate, mas.EntryBy, dgs.Designation, Emp.EmpName, Emp.EmpMasterCode, Emp.DivisionId, Emp.DepartmentId, mas.ExitMasterId, mas.EmployeeId, case when tblCount2.AppCount IS NOT NULL AND tblCount2.AppCount=tblCount.AppCount then 'Completed' else 'Pending' end ApprovalStatus, case when tblCount2.AppCount IS NOT NULL AND tblCount2.AppCount=tblCount.AppCount then 'btn btn-info btn-sm ' else '  btn btn-danger btn-sm' end AppCount  FROM tblEmpExitMaster mas 
LEFT JOIN dbo.tblEmpGeneralInfo emp ON mas.EmployeeId=emp.EmpInfoId
LEFT JOIN tblEmployeeJobLeft EPE  ON mas.EmployeeId = EPE.EmployeeId 
LEFT JOIN dbo.tblDesignation dgs ON dgs.DesignationId=emp.DesignationId 
left join (select ISNULL(COUNT(*),0) AppCount,dtl.MasterId from [tblEmpExitDetail] dtl where IsDone=1 group by dtl.MasterId)tblCount on tblCount.MasterId=mas.ExitMasterId

left join (select ISNULL(COUNT(*),0) AppCount,dtl.MasterId from [tblEmpExitDetail] dtl  group by dtl.MasterId)tblCount2 on tblCount2.MasterId=mas.ExitMasterId    

inner JOIN   tblEmpAllRefference reff  ON emp.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 WHERE  EmpMasterCode IS NOT  NULL  and      reff.ShowCompany in (ComAssain) "  + @" )tbl


-- ============ Approval Status Comma String ============
LEFT JOIN 
(
    SELECT 
        dtl.MasterId,
        STUFF(
            (
                SELECT  distinct 
                    ' | ' +
                    ISNULL(empX.EmpName, '') + 
                    ' (' + ISNULL(empX.EmpMasterCode, '') + ')' +
                    case when dtlX.ApprovalStatus  ='as Supervisor' then ' [Supervisor] ' else '' end +
ISNULL(
    CASE 
        WHEN dtlX.IsDone = 0 
        THEN ISNULL(
            CASE 
                WHEN dtlX.EmpInfoId = dtlX.IsForwardEmpId THEN '' 
                WHEN ISNULL(dtlX.isForwardBackDone, 0) = 1 THEN ''
                ELSE  isnull( ' [Forward to : '+ empFor.EmpName  + 
                     ' ('  +(empFor.EmpMasterCode ) + ')]' ,'')
            END, '')
        ELSE '' 
    END, '') 
+
ISNULL((
    SELECT TOP 1 ' [Auto forward: ' + STUFF((
        SELECT '; ' + 
               CASE WHEN ISNULL(fDtl.OrderNo, 1) = 1 
                    THEN 'Primary Person: ' 
                    ELSE 'Alternative: ' 
               END + fEmp.EmpName + ' (' + fEmp.EmpMasterCode + ')'
        FROM dbo.tblClearanceForwardingDetail fDtl
        INNER JOIN dbo.tblEmpGeneralInfo fEmp ON fDtl.SupervisedEmployeeId = fEmp.EmpInfoId
        WHERE fDtl.ClearanceForwardingId = fMst.ClearanceForwardingId AND fDtl.Status = 'Pending'
        ORDER BY ISNULL(fDtl.OrderNo, 99999), fDtl.ClearanceForwardingDetailId ASC
        FOR XML PATH(''), TYPE
    ).value('.', 'nvarchar(max)'), 1, 2, '') + ']'
    FROM dbo.tblClearanceForwardingMaster fMst
    WHERE fMst.EmployeeId = dtlX.EmpInfoId AND fMst.Status = 'Forwarded'
    ORDER BY fMst.ClearanceForwardingId DESC
), '') 

+ ' Days: ' +
CAST(
    CASE
        -- Division 45 হলে Supervisor Done না হলে 0 days
        WHEN dtlX.EmpInfoId = 3001 
             AND supDone.SupervisorDoneDate IS NULL
        THEN 0

        -- Done হলে
        WHEN dtlX.IsDone = 1
        THEN isnull(DATEDIFF(
                DAY,
                CASE 
                    WHEN dtlX.EmpInfoId = 3001 
                    THEN CONVERT(date,supDone.SupervisorDoneDate)
                    ELSE CONVERT(date,mas.EntryDate)
                END,
                ISNULL(CONVERT(date,clrForm.IsDoneDate), CONVERT(date,GETDATE()))
             ) ,0)+1 

        -- Pending হলে
        WHEN ISNULL(dtlX.IsDone, 0) = 0
        THEN isnull(DATEDIFF(
                DAY,
                CASE 
                    WHEN dtlX.EmpInfoId = 3001 
                    THEN CONVERT(date,supDone.SupervisorDoneDate)
                    ELSE CONVERT(date,mas.EntryDate)
                END,
                CONVERT(date,GETDATE())
             )  ,0)+1

        ELSE 0
    END AS NVARCHAR(MAX)
)
+ ' ' +
CASE   
    WHEN dtlX.EmpInfoId = 3001 
         AND supDone.SupervisorDoneDate IS NULL
        THEN ' (Not Yet Reached)'

    WHEN dtlX.IsDone = 1 
        THEN ' (Done)'

    WHEN ISNULL(dtlX.IsDone, 0) = 0 
        THEN ' (Pending)'  

END + '~' + CAST(ISNULL(dtlX.isNotification, 0) AS VARCHAR(1)) 
                FROM dbo.tblEmpExitDetail dtlX
               LEFT JOIN   tblEmpExitMaster mas on  dtlX.MasterId=mas.ExitMasterId
                 LEFT JOIN dbo.tblDepWiseClearanceResourceUpdate clrForm ON clrForm.exitDetailIdNew = dtlX.ExitDetailId
                LEFT JOIN dbo.tblEmpGeneralInfo empX ON empX.EmpInfoId = dtlX.EmpInfoId
                LEFT JOIN dbo.tblEmpGeneralInfo empFor ON empFor.EmpInfoId = dtlX.IsForwardEmpId
                LEFT JOIN dbo.tblDepartment dpt ON empX.DepartmentId = dpt.DepartmentId
                OUTER APPLY
(
    SELECT TOP 1 
        clrSup.IsDoneDate AS SupervisorDoneDate
    FROM dbo.tblEmpExitDetail sup
    LEFT JOIN dbo.tblDepWiseClearanceResourceUpdate clrSup 
        ON clrSup.exitDetailIdNew = sup.ExitDetailId
    WHERE sup.MasterId = dtlX.MasterId
      AND sup.ApprovalStatus = 'as Supervisor'
      AND sup.IsDone = 1
    ORDER BY clrSup.IsDoneDate DESC
) supDone
                WHERE dtlX.MasterId = dtl.MasterId
 
                FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)'),
            1, 3, ''
        ) AS EmployeeApprovalStatus
    FROM dbo.tblEmpExitDetail dtl
    GROUP BY dtl.MasterId  -- ✅ শুধু MasterId রাখো, বাকিগুলো বাদ
) dtlAgg 
    ON dtlAgg.MasterId = tbl.ExitMasterId
left join (select EmpID, max(IsDoneDate) IsDoneDate from tblDepWiseClearanceResourceUpdate group by EmpID)tblcmt on tblcmt.EmpID=tbl.EmployeeId

where EmpMasterCode is not null
 " +  paramAll + @"

  ORDER BY  EntryDate DESC";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }

       public DataTable ClearenceFormSetupViewPharma(string param, string paramAll)
       {
           string queryStr = @" select distinct  *  
    ,
    DATEDIFF(DAY, CONVERT(date,tbl.EntryDate), isnull( case when ApprovalStatus='Completed' then tblcmt.IsDoneDate else getdate() end,getdate()))+1 AS TotalDays from (  SELECT   EPE.JobLeftDate, DeclineComment, isnull(mas.IsRunning,0) IsRunning, mas.EntryDate, mas.EntryBy, sal.SalaryLocation, dgs.Designation, Emp.EmpName,  Emp.EmpMasterCode, Emp.DivisionId, Emp.DepartmentId, mas.ExitMasterId, mas.EmployeeId, case when tblCount2.AppCount IS NOT NULL AND tblCount2.AppCount=tblCount.AppCount then 'Completed' else 'Pending' end ApprovalStatus, case when tblCount2.AppCount IS NOT NULL AND tblCount2.AppCount=tblCount.AppCount then 'btn btn-info btn-sm ' else '  btn btn-danger btn-sm' end AppCount  FROM tblEmpExitMaster mas 
LEFT JOIN dbo.tblEmpGeneralInfo emp ON mas.EmployeeId=emp.EmpInfoId
LEFT JOIN tblEmployeeJobLeft EPE  ON mas.EmployeeId = EPE.EmployeeId 
LEFT JOIN dbo.tblDesignation dgs ON dgs.DesignationId=emp.DesignationId 
LEFT JOIN dbo.tblSalaryLocation sal ON sal.SalaryLoationId = Emp.SalaryLoationId
left join (select ISNULL(COUNT(*),0) AppCount,dtl.MasterId from [tblEmpExitDetail] dtl where IsDone=1 group by dtl.MasterId)tblCount on tblCount.MasterId=mas.ExitMasterId

left join (select ISNULL(COUNT(*),0) AppCount,dtl.MasterId from [tblEmpExitDetail] dtl  group by dtl.MasterId)tblCount2 on tblCount2.MasterId=mas.ExitMasterId  WHERE emp.CompanyId=" + param + @"  
union all 

 SELECT  EPE.JobLeftDate,   DeclineComment, isnull(mas.IsRunning,0) IsRunning,mas.EntryDate, mas.EntryBy, sal.SalaryLocation,dgs.Designation, Emp.EmpName, Emp.EmpMasterCode, Emp.DivisionId, Emp.DepartmentId, mas.ExitMasterId, mas.EmployeeId, case when tblCount2.AppCount IS NOT NULL AND tblCount2.AppCount=tblCount.AppCount then 'Completed' else 'Pending' end ApprovalStatus, case when tblCount2.AppCount IS NOT NULL AND tblCount2.AppCount=tblCount.AppCount then 'btn btn-info btn-sm ' else '  btn btn-danger btn-sm' end AppCount  FROM tblEmpExitMaster mas 
LEFT JOIN dbo.tblEmpGeneralInfo emp ON mas.EmployeeId=emp.EmpInfoId
LEFT JOIN tblEmployeeJobLeft EPE  ON mas.EmployeeId = EPE.EmployeeId 
LEFT JOIN dbo.tblDesignation dgs ON dgs.DesignationId=emp.DesignationId 
LEFT JOIN dbo.tblSalaryLocation sal ON sal.SalaryLoationId = Emp.SalaryLoationId
left join (select ISNULL(COUNT(*),0) AppCount,dtl.MasterId from [tblEmpExitDetail] dtl where IsDone=1 group by dtl.MasterId)tblCount on tblCount.MasterId=mas.ExitMasterId

left join (select ISNULL(COUNT(*),0) AppCount,dtl.MasterId from [tblEmpExitDetail] dtl  group by dtl.MasterId)tblCount2 on tblCount2.MasterId=mas.ExitMasterId    

inner JOIN   tblEmpAllRefference reff  ON emp.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 WHERE  EmpMasterCode IS NOT  NULL  and      reff.ShowCompany in (ComAssain) " + @" )tbl


-- ============ Approval Status Comma String ============

-- ============ Approval Status Comma String ============
LEFT JOIN 
(
    SELECT 
        dtl.MasterId,
        STUFF(
            (
     SELECT  distinct 
                    ' | ' +
                    ISNULL(empX.EmpName, '') + 
                    ' (' + ISNULL(empX.EmpMasterCode, '') + ')' +
                    case when dtlX.ApprovalStatus  ='as Supervisor' then ' [Supervisor] ' else '' end +
ISNULL(
    CASE 
        WHEN dtlX.IsDone = 0
        THEN ISNULL(
            CASE
                WHEN dtlX.EmpInfoId = dtlX.IsForwardEmpId THEN ''
                WHEN ISNULL(dtlX.isForwardBackDone, 0) = 1 THEN ''
                ELSE  isnull( ' [Forward to : '+ empFor.EmpName  +
                     ' ('  +(empFor.EmpMasterCode ) + ')]' ,'')
            END, '')
        ELSE ''
    END, '')
+
ISNULL((
    SELECT TOP 1 ' [Auto forward: ' + STUFF((
        SELECT '; ' + 
               CASE WHEN ISNULL(fDtl.OrderNo, 1) = 1 
                    THEN 'Primary Person: ' 
                    ELSE 'Alternative: ' 
               END + fEmp.EmpName + ' (' + fEmp.EmpMasterCode + ')'
        FROM dbo.tblClearanceForwardingDetail fDtl
        INNER JOIN dbo.tblEmpGeneralInfo fEmp ON fDtl.SupervisedEmployeeId = fEmp.EmpInfoId
        WHERE fDtl.ClearanceForwardingId = fMst.ClearanceForwardingId AND fDtl.Status = 'Pending'
        ORDER BY ISNULL(fDtl.OrderNo, 99999), fDtl.ClearanceForwardingDetailId ASC
        FOR XML PATH(''), TYPE
    ).value('.', 'nvarchar(max)'), 1, 2, '') + ']'
    FROM dbo.tblClearanceForwardingMaster fMst
    WHERE fMst.EmployeeId = dtlX.EmpInfoId AND fMst.Status = 'Forwarded'
    ORDER BY fMst.ClearanceForwardingId DESC
), '')

+ ' Days: ' +
CAST(
    CASE
        WHEN dtlX.IsDone = 1
        THEN isnull(DATEDIFF(
                DAY,
                CONVERT(date,mas.EntryDate),
                ISNULL(CONVERT(date,clrForm.IsDoneDate), CONVERT(date,GETDATE()))
             )  ,0)+1

        WHEN ISNULL(dtlX.IsDone, 0) = 0
        THEN isnull(DATEDIFF(
                DAY,
                CONVERT(date,mas.EntryDate),
                CONVERT(date,GETDATE())
             )  ,0)+1

        ELSE 0
    END AS NVARCHAR(MAX)
)
+ ' ' +
CASE
    WHEN dtlX.IsDone = 1
        THEN ' (Done)'

    WHEN ISNULL(dtlX.IsDone, 0) = 0
        THEN ' (Pending)'

END + '~' + CAST(ISNULL(dtlX.isNotification, 0) AS VARCHAR(1))
                FROM dbo.tblEmpExitDetail dtlX
               LEFT JOIN   tblEmpExitMaster mas on  dtlX.MasterId=mas.ExitMasterId
                 LEFT JOIN dbo.tblDepWiseClearanceResourceUpdate clrForm ON clrForm.exitDetailIdNew = dtlX.ExitDetailId
                LEFT JOIN dbo.tblEmpGeneralInfo empX ON empX.EmpInfoId = dtlX.EmpInfoId
                LEFT JOIN dbo.tblEmpGeneralInfo empFor ON empFor.EmpInfoId = dtlX.IsForwardEmpId
                LEFT JOIN dbo.tblDepartment dpt ON empX.DepartmentId = dpt.DepartmentId

                left join (     SELECT  mas.ExitMasterId,    case when  ISNULL(tblCount2.AppCount,0)-2=ISNULL(tblCount.AppCount,0) then '1' else '0' end AppCountICTDiv ,    case when  ISNULL(tblCount2.AppCount,0)-1=ISNULL(tblCount.AppCount,0) then '1' else '0' end AppCount  FROM tblEmpExitMaster mas 
LEFT JOIN dbo.tblEmpGeneralInfo emp ON mas.EmployeeId=emp.EmpInfoId
LEFT JOIN dbo.tblDesignation dgs ON dgs.DesignationId=emp.DesignationId 
left join (select ISNULL(COUNT(*),0) AppCount,dtl.MasterId from [tblEmpExitDetail] dtl where IsDone=1 group by dtl.MasterId)tblCount on tblCount.MasterId=mas.ExitMasterId

left join (select ISNULL(COUNT(*),0) AppCount,dtl.MasterId from [tblEmpExitDetail] dtl  group by dtl.MasterId)tblCount2 on tblCount2.MasterId=mas.ExitMasterId      group by mas.ExitMasterId,ISNULL(tblCount2.AppCount,0),ISNULL(tblCount.AppCount,0)) tbCount on tbCount.ExitMasterId=dtlX.MasterId

                OUTER APPLY
(
    SELECT TOP 1 
        max(clrSup.IsDoneDate) AS SupervisorDoneDate
    FROM dbo.tblEmpExitDetail sup
    LEFT JOIN dbo.tblDepWiseClearanceResourceUpdate clrSup 
        ON clrSup.exitDetailIdNew = sup.ExitDetailId
    WHERE sup.MasterId = dtlX.MasterId
       
      AND sup.IsDone = 1 and clrSup.SetInfo<>'Div'
    
) supDone 
                WHERE dtlX.MasterId = dtl.MasterId




           
                FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)'),
            1, 3, ''
        ) AS EmployeeApprovalStatus
    FROM dbo.tblEmpExitDetail dtl
    GROUP BY dtl.MasterId  -- ✅ শুধু MasterId রাখো, বাকিগুলো বাদ
) dtlAgg 
    ON dtlAgg.MasterId = tbl.ExitMasterId
left join (select EmpID, max(IsDoneDate) IsDoneDate from tblDepWiseClearanceResourceUpdate group by EmpID)tblcmt on tblcmt.EmpID=tbl.EmployeeId
where EmpMasterCode is not null
 " +  paramAll + @"

  ORDER BY  EntryDate DESC";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }

       public bool UpdateExitRunningStatus(int exitMasterId, bool isRunning, string declineComment)
       {
           List<SqlParameter> parameters = new List<SqlParameter>();
           parameters.Add(new SqlParameter("@ExitMasterId", exitMasterId));
           parameters.Add(new SqlParameter("@IsRunning", isRunning));
           parameters.Add(new SqlParameter("@DeclineComment", declineComment));

           const string query = @"UPDATE tblEmpExitMaster
SET IsRunning=@IsRunning,
    DeclineComment=@DeclineComment, IsRunningDate=getdate()
WHERE ExitMasterId=@ExitMasterId";

           return aCommonInternalDal.UpdateDataByUpdateCommand(query, parameters, DataBase.HRDB);
       }

       public bool UpdateEmpExitDetailStatus(int masterId, string empCode, string approvalStatus, bool isDone)
       {
           int empInfoId = 0;
           string empQuery = "SELECT EmpInfoId FROM tblEmpGeneralInfo WHERE EmpMasterCode = @EmpMasterCode";
           List<SqlParameter> empParams = new List<SqlParameter>();
           empParams.Add(new SqlParameter("@EmpMasterCode", empCode));
           DataTable dt = aCommonInternalDal.DataContainerDataTable(empQuery, empParams, "HRDB");
           if (dt != null && dt.Rows.Count > 0)
           {
               empInfoId = Convert.ToInt32(dt.Rows[0]["EmpInfoId"]);
           }
           else
           {
               return false;
           }

           List<SqlParameter> parameters = new List<SqlParameter>();
           parameters.Add(new SqlParameter("@MasterId", masterId));
           parameters.Add(new SqlParameter("@EmpInfoId", empInfoId));
           parameters.Add(new SqlParameter("@ApprovalStatus", approvalStatus));
           parameters.Add(new SqlParameter("@IsDone", isDone ? 1 : 0));

           string query = @"
UPDATE tblEmpExitDetail
SET isNotification = @IsDone,
    isNotificationDate = " + (isDone ? "GETDATE()" : "NULL") + @"
WHERE MasterId = @MasterId
  AND EmpInfoId = @EmpInfoId
  AND ApprovalStatus = @ApprovalStatus";

           return aCommonInternalDal.UpdateDataByUpdateCommand(query, parameters, DataBase.HRDB);
       }

       public DataTable LoadInformationALlClear(string param)
       {
           string queryStr = @"SELECT * FROM dbo.tblEmpExitMaster EPE
INNER JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
INNER JOIN dbo.tblEmpExitDetail ON dbo.tblEmpExitDetail.MasterId=EPE.ExitMasterId
WHERE EmpInfoIdApproval='"+HttpContext.Current.Session["UserId"].ToString()+"'";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
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
           string query = @" 
SELECT  tblEmployeeJobLeft.ActionStatus,  EGE.EmpInfoId as UserEmpInfoId,tblEmployeeJobLeft.EmployeeId as EmpInfoId,  EG.EmpName AS EmployeeName , EG.EmpMasterCode, deg.Designation, 
SG.GradeCode+':'+ SG.GradeName AS GradeName,   Div.DivisionName, Wing.DivisionWingName, 
Sec.SectionName, SubSec.SubSectionName, Dpt.DepartmentName,  * FROM tblEmployeeJobLeft
LEFT JOIN dbo.tblEmpGeneralInfo EG ON tblEmployeeJobLeft.EmployeeId= EG.EmpInfoId 
LEFT JOIN dbo.tblDesignation  deg ON EG.DesignationId=deg.DesignationId
							LEFT JOIN dbo.tblSalaryGrade  SG ON EG.SalaryGradeId=SG.SalaryGradeId
							LEFT JOIN dbo.tblDivision  Div ON EG.DivisionId=Div.DivisionId
							LEFT JOIN dbo.tblDivisionWing  Wing ON EG.DivisionWId=Wing.DivisionWId
							LEFT JOIN dbo.tblSection  Sec ON EG.SectionId=Sec.SectionId
							LEFT JOIN dbo.tblSubSection  SubSec ON EG.SubSectionId=SubSec.SubSectionId						
								LEFT JOIN dbo.tblDepartment  Dpt ON EG.DepartmentId=Dpt.DepartmentId
LEFT JOIN dbo.tblUser AS U ON U.UserId = tblEmployeeJobLeft.EntryBy

									INNER JOIN dbo.tblEmpGeneralInfo EGE ON EGE.EmpInfoId=U.EmpInfoId

								WHERE tblEmployeeJobLeft.EmployeeJobLeftId='" + id + "'";


           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }



       public DataTable GetAppLogCommByJobId(int jobId)
       {
           string query = @"  SELECT Alg.EmployeeJobLeftAppLogId, emp.EmpName PreEmp, emp2.EmpName ForEmp, Version, Us.UserName ApproveBy, Alg.ActionStatus, Alg.ApproveDate, Alg.EmployeeJobLeftId, Alg.Comments
  FROM tblEmployeeJobLeftAppLog Alg
LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId=Alg.PreEmpInfoId
LEFT JOIN dbo.tblEmpGeneralInfo emp2 ON emp2.EmpInfoId=Alg.ForEmpInfoId
LEFT JOIN dbo.tblUser Us ON Alg.ApproveBy=Us.UserId WHERE  Alg.ActionStatus!= 'Drafted' and Alg.EmployeeJobLeftId='" + jobId + "'";
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


           aSqlParameterlist.Add(new SqlParameter("@SubmissionDate", aEmployeeJobLeftEntryDAO.SubmissionDate ?? (object)DBNull.Value));

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

       public bool UpdateEmployeeExitInfo(EmpGeneralInfo aInfo)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           aSqlParameterlist.Add(new SqlParameter("@IsActive", aInfo.IsActive));
           aSqlParameterlist.Add(new SqlParameter("@InactiveReason", aInfo.InactiveReason ));
           aSqlParameterlist.Add(new SqlParameter("@EmployeeStatus", aInfo.EmployeeStatus));
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aInfo.EmpInfoId));
           aSqlParameterlist.Add(new SqlParameter("@Updateby", Convert.ToInt32(HttpContext.Current.Session["UserId"])));
           string query = @"UPDATE dbo.tblEmpGeneralInfo SET IsActive = @IsActive,InactiveReason = @InactiveReason , EmployeeStatus = @EmployeeStatus , Updateby=@Updateby, UpdateDate=GETDATE() WHERE EmpInfoId = @EmpInfoId";

           return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, DataBase.HRDB);
       }

       public DataTable FetchEmployeeInfoById(int jobLeftId)
       {
           string query = @"SELECT  EmpInfoId AS EmployeeId FROM tblSuspend WHERE SuspendId =  " + jobLeftId;
           return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
       }



       public DataTable LoadInformationApprove()
       {
           string queryStr = @"SELECT  EPE.EmployeeId, Emp.EmpName, Com.CompanyName, JType.JobLeftType, UserR.UserName AS EntryBy, UpBY.UserName AS UpdateBy ,EPE.*,(CASE WHEN EIM.ExitMasterId
IS NULL THEN 'Pending' ELSE 'Completed' END)AS ExitFormStatus,EmployeeJobLeftAppLogId From tblEmployeeJobLeft EPE
                                    INNER JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
                                    INNER JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
                                    INNER JOIN dbo.tblJobLeftType  JType ON EPE.JobLeftTypeId = JType.JobLeftTypeId         
								 LEFT JOIN dbo.tblUser  UserR ON EPE.EntryBy = UserR.UserId                                  
						  LEFT JOIN dbo.tblUser  UpBY ON EPE.UpdateBy = UpBY.UserId   
						  LEFT JOIN dbo.tblExitInterviewFormMaster EIM ON EIM.EmployeeId = EPE.EmployeeId                              
                            INNER JOIN (SELECT EmployeeJobLeftId,MAX(Version)MaxVer FROM dbo.tblEmployeeJobLeftAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY EmployeeJobLeftId) AS CELog ON CELog.EmployeeJobLeftId= EPE.EmployeeJobLeftId
								INNER JOIN dbo.tblEmployeeJobLeftAppLog ON tblEmployeeJobLeftAppLog.EmployeeJobLeftId = EPE.EmployeeJobLeftId
                                where (EPE.IsDelete is null or EPE.IsDelete = 0) and Version=CELog.MaxVer  and  ForEmpInfoId = '" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }
       public DataTable GetEmpInfo(string param)
       {
           try
           {
               string query = @"SELECT * FROM dbo.tblEmpGeneralInfo " + param + "";

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public bool UpdateContractural(EmployeeJobLeftEntryDAO aMaster)
       {

           try
           {
               int pk = 0;

               if (aMaster.EmployeeJobLeftId > 0)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@EmployeeJobLeftId", aMaster.EmployeeJobLeftId));
                   aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));


                   string query =
                       @"update tblEmployeeJobLeft set ActionStatus=@ActionStatus  where EmployeeJobLeftId = @EmployeeJobLeftId";

                   bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                   return result;

               }

           }
           catch (Exception exception)
           {

               throw exception;
           }
           return true;
       }


       public DataTable GetDataReviewEntryBy(string id, string entryby, string actionstatu)
       {
           //var aSqlParameterlist = new List<SqlParameter>();
           //aSqlParameterlist.Add(new SqlParameter("@Parameter", Parameter));


           string queryStr = @"SELECT * FROM dbo.tblEmployeeJobLeft WHERE ActionStatus='" + actionstatu + "' AND EntryBy='" + entryby + "' AND EmployeeJobLeftId='" + id + "'";

           return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
       }

       public bool UpdateJobReqStatus2(EmployeeJobLeftEntryDAO aMaster)
       {

           try
           {
               int pk = 0;

               if (aMaster.EmployeeJobLeftId > 0)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@EmployeeJobLeftId", aMaster.EmployeeJobLeftId));
                   aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));


                   string query =
                       @"update tblEmployeeJobLeft set ActionStatus2=@ActionStatus  where EmployeeJobLeftId = @EmployeeJobLeftId";

                   bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                   return result;

               }

           }
           catch (Exception exception)
           {

               throw exception;
           }
           return true;
       }
       public bool UpdateSelfApprove(int id, bool selfapp)
       {

           try
           {
               int pk = 0;

               if (id > 0)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@ID", id));
                   aParameters.Add(new SqlParameter("@IsSelfApp", selfapp));


                   string query =
                       @"update tblEmployeeJobLeft set IsSelfApp=@IsSelfApp  where EmployeeJobLeftId = @ID";

                   bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                   return result;

               }

           }
           catch (Exception exception)
           {

               throw exception;
           }
           return true;
       }
       public DataTable GetSupervisorAppId(string url, string param)
       {
           try
           {
               string query = @"SELECT * FROM dbo.tblSupevisorMenuApproval
LEFT JOIN dbo.tblMainMenu ON tblMainMenu.MainMenuId = tblSupevisorMenuApproval.MainMenuId WHERE URL='" + url + "' " + param + "";

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public DataTable GetSupervisorEmployeeAppId(string empinfoId, string fromempInfoId)
       {
           try
           {
               string query = @"SELECT * FROM dbo.tblSupevisorMenuApproval WHERE EmpInfoId='" + empinfoId + "' AND FromEmpInfoId='" + fromempInfoId + "'";

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public DataTable GetHRAdminEmployeeAppId(string parameter)
       {
           try
           {
               string query = @"SELECT * FROM dbo.tblEmployeeApprovalByOpearationDetail
            LEFT JOIN dbo.tblMainMenu ON dbo.tblEmployeeApprovalByOpearationDetail.Operation=dbo.tblMainMenu.MainMenuId " + parameter + "";

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }


       public DataTable GetEmpInfoPrevious(string forempInfoid, string jdmasterId)
       {
           try
           {
               string query = @"SELECT * FROM dbo.tblEmployeeJobLeftAppLog WHERE ForEmpInfoId='" + forempInfoid + "' AND EmployeeJobLeftId='" + jdmasterId + "' AND ActionStatus NOT IN ('Review')   order by EmployeeJobLeftAppLogId desc ";

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public bool UpdateAppLog(string status, string id)
       {

           try
           {
               int pk = 0;

               //if (id.JdMasterId > 0)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@EmployeeJobLeftAppLogId", id));
                   aParameters.Add(new SqlParameter("@ActionStatus", status));


                   string query =
                       @"update tblEmployeeJobLeftAppLog set ActionStatus=@ActionStatus  where EmployeeJobLeftAppLogId = @EmployeeJobLeftAppLogId";

                   bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                   return result;

               }

           }
           catch (Exception exception)
           {

               throw exception;
           }
       }
       public DataTable GetContractualDataInfo(string id)
       {
           //var aSqlParameterlist = new List<SqlParameter>();
           //aSqlParameterlist.Add(new SqlParameter("@Parameter", Parameter));


           string queryStr = @"SELECT *,CONVERT(BIT,(CASE WHEN IsSelfApp IS NULL THEN '0' ELSE '1' END ))IsSelfApp FROM dbo.tblEmployeeJobLeft
LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmployeeJobLeft.EmployeeId WHERE EmployeeJobLeftId='" + id + "'";

           return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
       }
       public int SavAppLog(EmployeeJobLeftAppLogDAO appLogDao)
       {

           try
           {
               int pk = 0;


               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@EmployeeJobLeftId", appLogDao.EmployeeJobLeftId));
                   aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                   aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                   aParameters.Add(new SqlParameter("@Version", appLogDao.Version));
                   aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                   aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                   aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                   aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));
                   aParameters.Add(new SqlParameter("@CommentsId", appLogDao.CommentsId ?? (object)DBNull.Value));


                   string query = @"INSERT INTO dbo.tblEmployeeJobLeftAppLog
                                    (
                                    EmployeeJobLeftId,
                                    PreEmpInfoId,
                                    ForEmpInfoId,
                                    Version,
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments,CommentId
                                    )
                                    VALUES(
                                    @EmployeeJobLeftId,
                                    @PreEmpInfoId,
                                    @ForEmpInfoId,
                                    (SELECT (COUNT(*)+1) FROM dbo.tblEmployeeJobLeftAppLog WHERE EmployeeJobLeftId=@EmployeeJobLeftId),
                                    @ApproveBy,
                                    @ApproveDate,
                                    @ActionStatus,@Comments,@CommentsId
                                    )";

                   pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
               }


               return pk;
           }
           catch (Exception exception)
           {

               throw exception;
           }
       }
       public int SaveComment(string masterId, string empinfoId, string comments)
       {

           try
           {
               int pk = 0;


               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   //aParameters.Add(new SqlParameter("@Id", masterId));
                   aParameters.Add(new SqlParameter("@EmpInfoId", empinfoId));
                   aParameters.Add(new SqlParameter("@Comments", comments));


                   string query = @" INSERT INTO dbo.tblEmployeeJobLeftAppLogComnt
                                    (
                                        EmpInfoId,
                                        Comments
                                    )
                                    VALUES
                                    (   @EmpInfoId,
                                        @Comments
                                    )";

                   pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
               }


               return pk;
           }
           catch (Exception exception)
           {

               throw exception;
           }
       }
    }
}
