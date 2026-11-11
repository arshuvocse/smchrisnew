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
    public class TrainingRecordsReportDAL
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
           string queryStr = @"SELECT Desig.Designation, SGrade.GradeName, Emp.DateOfJoin, Emp.EmpName, Emp.EmpMasterCode, Com.CompanyName, JType.JobLeftType, UserR.UserName AS EntryBy, UpBY.UserName AS UpdateBy ,EPE.*,(CASE WHEN EIM.ExitMasterId
IS NULL THEN 'Pending' ELSE 'Completed' END) AS ExitFormStatus,cast((DATEDIFF(m, Emp.DateOfJoin, GETDATE())/12) as varchar) + ' Year , ' + 
       cast((DATEDIFF(m, Emp.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, Emp.DateOfJoin, GETDATE())%12) as varchar) + ' day'  as LengthServicewithSMC From tblEmployeeJobLeft EPE
                                    INNER JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
                                    INNER JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
                                    INNER JOIN dbo.tblJobLeftType  JType ON EPE.JobLeftTypeId = JType.JobLeftTypeId         
								 LEFT JOIN dbo.tblUser  UserR ON EPE.EntryBy = UserR.UserId                                  
								 LEFT JOIN dbo.tblDesignation  Desig ON Emp.DesignationId = Desig.DesignationId                                  
								 LEFT JOIN dbo.tblSalaryGrade  SGrade ON Emp.SalaryGradeId = SGrade.SalaryGradeId                                  
						  LEFT JOIN dbo.tblUser  UpBY ON EPE.UpdateBy = UpBY.UserId   
						  LEFT JOIN dbo.tblExitInterviewFormMaster EIM ON EIM.EmployeeId = EPE.EmployeeId                             
 Where ( EPE.IsDelete IS NULL OR EPE.IsDelete = 0) " + param + "  ORDER BY EPE.EmployeeJobLeftId DESC";
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
