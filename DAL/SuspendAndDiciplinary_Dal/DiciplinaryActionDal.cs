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

namespace DAL.SuspendAndDiciplinary_Dal
{
    public class DiciplinaryActionDal
    {

        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public DataTable CheckStartEndDateExistOrNotDAL(string FinancialYearId, string StartDate, string EndDate)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", FinancialYearId));
            aSqlParameterlist.Add(new SqlParameter("@StartDate", StartDate));
            aSqlParameterlist.Add(new SqlParameter("@EndDate", EndDate));


            const string queryStr = @"
SELECT FinancialYearId ,
       StartDate ,
       EndDate 
       FROM tblFinancialYear  WHERE  FinancialYearId=@FinancialYearId and StartDate <= @StartDate AND EndDate >= @EndDate";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
        public bool DeleteInfofoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@DiciplinaryId", Id));


            const string queryStr = @"DELETE FROM tblDiciplinaryAction  WHERE DiciplinaryId = @DiciplinaryId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public void EmployeeNameDropDown(DropDownList ddl, string CompanyId)
        {
            string query = "SELECT * FROM dbo.tblEmpGeneralInfo WHERE CompanyId='" + CompanyId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "EmpName", "EmpInfoId", query, "HRDB");
        }

        public void LoadCompanyDropDownList(DropDownList ddl)
        {
            string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
        }


        public DataTable LoadEmpInfo(string empCode)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmployeeCode", empCode));

            const string queryStr = @"SELECT EGI.EmpInfoId,EGI.EmpMasterCode,DPT.DepartmentName,ETP.EmpTypeId, ETP.EmpType, 
                                EGI.DateOfJoin, EGI.EmpName,DPT.DepartmentId,DPT.DepartmentName,DSG.DesignationId,DSG.Designation FROM dbo.tblSuspend AS SPND 
                                LEFT JOIN tblEmpGeneralInfo AS EGI ON EGI.EmpInfoId = SPND.EmpInfoId
                                LEFT JOIN dbo.tblDepartment AS DPT ON SPND.DeptId = DPT.DepartmentId
                                LEFT JOIN dbo.tblDesignation AS DSG ON SPND.DesigId = DSG.DesignationId
                                LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
                                WHERE EGI.EmpInfoId = @EmployeeCode AND EGI.IsActive=1 and EGI.EmployeeStatus='Active'";

            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable LoadEmpInfoFromEmployee(string empCode)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmployeeCode", empCode));

            const string queryStr = @"SELECT EGI.EmpInfoId,EGI.EmpMasterCode,DPT.DepartmentName,ETP.EmpTypeId, ETP.EmpType, 
                                EGI.DateOfJoin, EGI.EmpName,DPT.DepartmentId,DPT.DepartmentName,DSG.DesignationId,DSG.Designation, EGI.* FROM dbo.tblEmpGeneralInfo AS EGI 
                                
                                LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                                LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
                                LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
                                WHERE EGI.EmpInfoId = @EmployeeCode AND EGI.IsActive=1 and EGI.EmployeeStatus='Active'";

            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
        public Int32 SaveDataForDiciplinaryAction(DiciplinaryAction aSuspend)
        {
            var aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aSuspend.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyInfoId", aSuspend.CompanyInfoId));



            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aSuspend.DivisionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DeptId", aSuspend.DeptId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SectionId", aSuspend.SectionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", aSuspend.EmpTypeId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@DesigId", aSuspend.DesigId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DivisionWId", aSuspend.DivisionWId ?? (object)DBNull.Value));

         
 


            aSqlParameterlist.Add(new SqlParameter("@SubSectionId", aSuspend.SubSectionId ?? (object)DBNull.Value));
            

            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", aSuspend.EffectiveDate));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", aSuspend.ActionStatus));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aSuspend.EntryBy));

            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aSuspend.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aSuspend.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@JoiningDate", aSuspend.JoiningDate));

            aSqlParameterlist.Add(new SqlParameter("@Description", aSuspend.Description));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aSuspend.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@isWarningLetter", aSuspend.isWarningLetter));

            aSqlParameterlist.Add(new SqlParameter("@isHoldIncrement", aSuspend.isHoldIncrement));
            aSqlParameterlist.Add(new SqlParameter("@isHoldIncentive", aSuspend.isHoldIncentive));
            aSqlParameterlist.Add(new SqlParameter("@isTermination", aSuspend.isTermination));

            aSqlParameterlist.Add(new SqlParameter("@isDeductionOfSalary", aSuspend.isDeductionOfSalary));
            aSqlParameterlist.Add(new SqlParameter("@is7DaysSalaryDeduction", aSuspend.is7DaysSalaryDeduction));

            aSqlParameterlist.Add(new SqlParameter("@ReasonId", aSuspend.ReasonId));
            aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", aSuspend.FinancialYearId));
            aSqlParameterlist.Add(new SqlParameter("@EmpCode", aSuspend.EmpCode));
            aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", aSuspend.SalaryLoationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@JobLocationId", aSuspend.JobLocationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ReasonIdStr", aSuspend.ReasonIdStr ?? (object)DBNull.Value));

            const string insertQuery = @"INSERT INTO dbo.tblDiciplinaryAction 
(EmpInfoId,CompanyInfoId,DivisionId,DeptId,SectionId,DesigId,EmpTypeId,EffectiveDate,ActionStatus,EntryBy,EntryDate,IsActive,JoiningDate,Description,Remarks,DivisionWId,SubSectionId,
isWarningLetter,isHoldIncrement,isHoldIncentive,isDeductionOfSalary,is7DaysSalaryDeduction,isTermination,ReasonId, FinancialYearId, EmpCode, SalaryLoationId, JobLocationId,ReasonIdStr) 
VALUES (@EmpInfoId,@CompanyInfoId,@DivisionId,@DeptId,@SectionId,@DesigId,@EmpTypeId,@EffectiveDate,@ActionStatus,@EntryBy,@EntryDate,@IsActive,@JoiningDate,@Description,@Remarks,@DivisionWId,@SubSectionId,
                @isWarningLetter,@isHoldIncrement,@isHoldIncentive,@isDeductionOfSalary,@is7DaysSalaryDeduction,@isTermination,@ReasonId,  @FinancialYearId, @EmpCode, @SalaryLoationId, @JobLocationId,@ReasonIdStr)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }


        public Int32 DELSaveDataForDiciplinaryAction(DiciplinaryAction aSuspend)
        {
            var aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@DiciplinaryId", aSuspend.DiciplinaryId));
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aSuspend.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyInfoId", aSuspend.CompanyInfoId));



            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aSuspend.DivisionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DeptId", aSuspend.DeptId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SectionId", aSuspend.SectionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", aSuspend.EmpTypeId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@DesigId", aSuspend.DesigId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DivisionWId", aSuspend.DivisionWId ?? (object)DBNull.Value));





            aSqlParameterlist.Add(new SqlParameter("@SubSectionId", aSuspend.SubSectionId ?? (object)DBNull.Value));


            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", aSuspend.EffectiveDate));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", aSuspend.ActionStatus));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aSuspend.EntryBy));

            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aSuspend.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aSuspend.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@JoiningDate", aSuspend.JoiningDate));

            aSqlParameterlist.Add(new SqlParameter("@Description", aSuspend.Description));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aSuspend.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@isWarningLetter", aSuspend.isWarningLetter));

            aSqlParameterlist.Add(new SqlParameter("@isHoldIncrement", aSuspend.isHoldIncrement));
            aSqlParameterlist.Add(new SqlParameter("@isHoldIncentive", aSuspend.isHoldIncentive));
            aSqlParameterlist.Add(new SqlParameter("@isTermination", aSuspend.isTermination));

            aSqlParameterlist.Add(new SqlParameter("@isDeductionOfSalary", aSuspend.isDeductionOfSalary));
            aSqlParameterlist.Add(new SqlParameter("@is7DaysSalaryDeduction", aSuspend.is7DaysSalaryDeduction));

            aSqlParameterlist.Add(new SqlParameter("@ReasonId", aSuspend.ReasonId));
            aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", aSuspend.FinancialYearId));
            aSqlParameterlist.Add(new SqlParameter("@EmpCode", aSuspend.EmpCode));
            aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", aSuspend.SalaryLoationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@JobLocationId", aSuspend.JobLocationId ?? (object)DBNull.Value));

            const string insertQuery = @"INSERT INTO dbo.DELtblDiciplinaryAction 
(DiciplinaryId, EmpInfoId,CompanyInfoId,DivisionId,DeptId,SectionId,DesigId,EmpTypeId,EffectiveDate,ActionStatus,EntryBy,EntryDate,IsActive,JoiningDate,Description,Remarks,DivisionWId,SubSectionId,
isWarningLetter,isHoldIncrement,isHoldIncentive,isDeductionOfSalary,is7DaysSalaryDeduction,isTermination,ReasonId, FinancialYearId, EmpCode, SalaryLoationId, JobLocationId) 
VALUES (@DiciplinaryId, @EmpInfoId,@CompanyInfoId,@DivisionId,@DeptId,@SectionId,@DesigId,@EmpTypeId,@EffectiveDate,@ActionStatus,@EntryBy,@EntryDate,@IsActive,@JoiningDate,@Description,@Remarks,@DivisionWId,@SubSectionId,
                @isWarningLetter,@isHoldIncrement,@isHoldIncentive,@isDeductionOfSalary,@is7DaysSalaryDeduction,@isTermination,@ReasonId,  @FinancialYearId, @EmpCode, @SalaryLoationId, @JobLocationId)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public DataTable LoadSuspend()
        {
            const string query = @"SELECT SPND.EffectiveDate,SPND.ActionStatus, SPND.SuspendId, EGI.EmpMasterCode, EGI.DateOfJoin, EGI.EmpInfoId, EGI.EmpName ,CI.CompanyId, CI.CompanyName, DSN.DivisionId, DSN.DivisionName, DSNW.DivisionWId, DSNW.DivisionWingName,DPT.DepartmentId, DPT.DepartmentName,
                            SEC.SectionId, SEC.SectionName, SSEC.SubSectionId, SSEC.SubSectionName, DSG.DesignationId, DSG.Designation, EMG.GradeId, EMG.GradeName, SPND.Description, SPND.Remarks, SPND.EmpTypeId
                            FROM tblSuspend AS SPND 
                            LEFT JOIN tblEmpGeneralInfo AS EGI ON EGI.EmpInfoId = SPND.EmpInfoId
                            LEFT JOIN dbo.tblCompanyInfo AS CI ON SPND.CompanyInfoId = CI.CompanyId
                            LEFT JOIN dbo.tblDivision AS DSN ON SPND.DivisionId = DSN.DivisionId
                            LEFT JOIN dbo.tblDivisionWing AS DSNW ON SPND.DivisionWId = DSNW.DivisionWId
                            LEFT JOIN dbo.tblDepartment AS DPT ON SPND.DeptId = DPT.DepartmentId
                            LEFT JOIN dbo.tblSection AS SEC ON SPND.SectionId = SEC.SectionId
                            LEFT JOIN dbo.tblSubSection AS SSEC ON SPND.SubSectionId = SSEC.SubSectionId
                            LEFT JOIN dbo.tblDesignation AS DSG ON SPND.DesigId = DSG.DesignationId
                            LEFT JOIN dbo.tblEmployeeGrade AS EMG ON SPND.EmpGradeId = EMG.GradeId  where SPND.ActionStatus in ('Posted','Cancel') and SPND.IsActive = 1 order by SPND.SuspendId desc";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public bool UpdateDataForEmpSuspend(DiciplinaryAction aSuspend)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DiciplinaryId", aSuspend.DiciplinaryId));
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aSuspend.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyInfoId", aSuspend.CompanyInfoId));



            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aSuspend.DivisionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DivisionWId", aSuspend.DivisionWId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DeptId", aSuspend.DeptId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SectionId", aSuspend.SectionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", aSuspend.EmpTypeId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@DesigId", aSuspend.DesigId ?? (object)DBNull.Value));





            aSqlParameterlist.Add(new SqlParameter("@SubSectionId", aSuspend.SubSectionId ?? (object)DBNull.Value));


            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", aSuspend.EffectiveDate));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", aSuspend.ActionStatus));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aSuspend.UpdateBy));

            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aSuspend.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aSuspend.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@JoiningDate", aSuspend.JoiningDate));

            aSqlParameterlist.Add(new SqlParameter("@Description", aSuspend.Description));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aSuspend.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@isWarningLetter", aSuspend.isWarningLetter));

            aSqlParameterlist.Add(new SqlParameter("@isHoldIncrement", aSuspend.isHoldIncrement));
            aSqlParameterlist.Add(new SqlParameter("@isHoldIncentive", aSuspend.isHoldIncentive));
            aSqlParameterlist.Add(new SqlParameter("@isTermination", aSuspend.isTermination));

            aSqlParameterlist.Add(new SqlParameter("@isDeductionOfSalary", aSuspend.isDeductionOfSalary));
            aSqlParameterlist.Add(new SqlParameter("@is7DaysSalaryDeduction", aSuspend.is7DaysSalaryDeduction));

            aSqlParameterlist.Add(new SqlParameter("@ReasonId", aSuspend.ReasonId));
            aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", aSuspend.FinancialYearId));
            aSqlParameterlist.Add(new SqlParameter("@EmpCode", aSuspend.EmpCode));
            aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", aSuspend.SalaryLoationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@JobLocationId", aSuspend.JobLocationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ReasonIdStr", aSuspend.ReasonIdStr ?? (object)DBNull.Value));
            
            const string query = @"UPDATE tblDiciplinaryAction SET  EmpInfoId=@EmpInfoId,CompanyInfoId=@CompanyInfoId, DivisionId=@DivisionId,DeptId=@DeptId,SectionId=@SectionId,EmpTypeId=@EmpTypeId, DesigId=@DesigId," +
                                 "EffectiveDate=@EffectiveDate,ActionStatus=@ActionStatus,UpdateBy=@UpdateBy,UpdateDate=@UpdateDate  ,JoiningDate = @JoiningDate,Description = @Description,Remarks = @Remarks ,DivisionWId = @DivisionWId,SubSectionId = @SubSectionId , SalaryLoationId=@SalaryLoationId, JobLocationId=@JobLocationId, ReasonIdStr=@ReasonIdStr where DiciplinaryId=@DiciplinaryId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }

        public DataTable EmpSuspendInformation(string suspendId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@DiciplinaryId", suspendId));

            const string query = @"SELECT SPND.EffectiveDate, SPND.DiciplinaryId,
SPND.ActionStatus, EGI.EmpMasterCode, EGI.DateOfJoin,
 EGI.EmpInfoId, EGI.EmpName ,CI.CompanyId, CI.CompanyName, 
 DSN.DivisionId, DSN.DivisionName, DSNW.DivisionWId, DSNW.DivisionWingName,
 DPT.DepartmentId, DPT.DepartmentName,ReasonId,
                            SEC.SectionId, SEC.SectionName, SSEC.SubSectionId, 
							SSEC.SubSectionName, DSG.DesignationId, DSG.Designation, 
						  SPND.Description, SPND.Remarks, SPND.EmpTypeId, EmpType.EmpType,SPND.*
                            FROM tblDiciplinaryAction AS SPND 
                            LEFT JOIN tblEmpGeneralInfo AS EGI ON EGI.EmpInfoId = SPND.EmpInfoId
                            LEFT JOIN dbo.tblCompanyInfo AS CI ON SPND.CompanyInfoId = CI.CompanyId
                            LEFT JOIN dbo.tblDivision AS DSN ON SPND.DivisionId = DSN.DivisionId
                            LEFT JOIN dbo.tblDivisionWing AS DSNW ON SPND.DivisionWId = DSNW.DivisionWId
                            LEFT JOIN dbo.tblDepartment AS DPT ON SPND.DeptId = DPT.DepartmentId
                            LEFT JOIN dbo.tblSection AS SEC ON SPND.SectionId = SEC.SectionId
                            LEFT JOIN dbo.tblSubSection AS SSEC ON SPND.SubSectionId = SSEC.SubSectionId
                            LEFT JOIN dbo.tblDesignation AS DSG ON SPND.DesigId = DSG.DesignationId 
							 LEFT JOIN dbo.tblEmployeeType AS EmpType ON EGI.EmpTypeId = EmpType.EmpTypeId 
                          
							 where DiciplinaryId =  @DiciplinaryId";

            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, "HRDB");
        }

        public void EmployeeTypeList(DropDownList ddl)
        {
            const string query = "SELECT * FROM tblEmployeeType";
            aCommonInternalDal.LoadDropDownValue(ddl, "EmpType", "EmpTypeId", query, "HRDB");
        }

        public bool DeleteSuspendInfoById(string suspendId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SuspendId", suspendId));

            const string query = "DELETE FROM tblSuspend WHERE SuspendId = @SuspendId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
        }

        public Int32 SaveEmployeeType(EmployeeTypeDao aTypeDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmpType", aTypeDao.EmpType));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aTypeDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aTypeDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aTypeDao.EntryDate));

            const string query = "INSERT INTO tblEmployeeType (EmpType,IsActive,EntryBy,EntryDate) VALUES (@EmpType,@IsActive,@EntryBy,@EntryDate)";
            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, "HRDB");
        }

        public DataTable GetDiciplinaryActionInfo(string param)
        {

            string query = @"select distinct * from (SELECT   DCPA.EntryDate,us.UserName, SPNDR.SuspendReasonEntry, Dg.Designation, dpt.DepartmentName, DCPA.DiciplinaryId, EGI.EmpMasterCode ,EGI.EmpName,DCPA.EffectiveDate,DCPA.Description, DCPA.Remarks  FROM tblDiciplinaryAction AS DCPA 
								   LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON DCPA.EmpInfoId = EGI.EmpInfoId
								   LEFT JOIN dbo.tblDesignation AS Dg ON EGI.DesignationId = Dg.DesignationId
LEFT JOIN dbo.tblDepartment AS dpt ON EGI.DepartmentId= dpt.DepartmentId
LEFT JOIN dbo.tblUser AS us ON us.UserId = DCPA.EntryBy
LEFT JOIN dbo.tblSuspendReasonEntry AS SPNDR ON DCPA.ReasonId =  SPNDR.SuspendReasonEntryId
								   WHERE DCPA.ActionStatus in ('Posted','Cancel') and DCPA.IsActive = 1
								  " + param + @"     union all 


								   SELECT  DCPA.EntryDate,us.UserName, SPNDR.SuspendReasonEntry, Dg.Designation, dpt.DepartmentName, DCPA.DiciplinaryId, EGI.EmpMasterCode ,EGI.EmpName,DCPA.EffectiveDate,DCPA.Description, DCPA.Remarks FROM tblDiciplinaryAction AS DCPA 
								   LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON DCPA.EmpInfoId = EGI.EmpInfoId
								   LEFT JOIN dbo.tblDesignation AS Dg ON EGI.DesignationId = Dg.DesignationId
LEFT JOIN dbo.tblDepartment AS dpt ON EGI.DepartmentId= dpt.DepartmentId
LEFT JOIN dbo.tblUser AS us ON us.UserId = DCPA.EntryBy
LEFT JOIN dbo.tblSuspendReasonEntry AS SPNDR ON DCPA.ReasonId =  SPNDR.SuspendReasonEntryId
inner JOIN   tblEmpAllRefference reff  ON EGI.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 WHERE  EmpMasterCode IS NOT  NULL  and      reff.ShowCompany in (ComAssain) 
								   and DCPA.ActionStatus in ('Posted','Cancel') and DCPA.IsActive = 1   ) tbl     order by  EffectiveDate desc";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
//        public DataTable GetDiciplinaryActionInfo(string empcode)
//        {

//            string query = @"SELECT * FROM dbo.tblDiciplinaryAction AS DCPA 
//                                   INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON DCPA.EmpInfoId = EGI.EmpInfoId
//                                   INNER JOIN dbo.tblCompanyInfo AS CMP ON DCPA.CompanyInfoId = CMP.CompanyId
//                                   INNER JOIN dbo.tblDivision AS DSN ON DCPA.DivisionId = DSN.DivisionId
//                                   INNER JOIN dbo.tblDivisionWing AS DSNW ON DCPA.DivisionWId = DSNW.DivisionWId
//                                   INNER JOIN dbo.tblDepartment AS DPT ON DCPA.DeptId = DPT.DepartmentId
//                                   INNER JOIN dbo.tblDesignation AS DSG ON DCPA.DesigId = DSG.DesignationId
//                                   INNER JOIN dbo.tblSection AS SCN ON DCPA.SectionId = SCN.SectionId
//                                   INNER JOIN dbo.tblSubSection AS SSCN ON DCPA.SubSectionId = SSCN.SubSectionId
//                                   INNER JOIN dbo.tblEmployeeType AS EMPT ON DCPA.EmpTypeId = EMPT.EmpTypeId  where EGI.EmpMasterCode='" + empcode + "' ";

//            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
//        }


        public void LoadActionType(DropDownList ddl, string companyId)
        {
            string query = @"SELECT SPN.SuspendReasonEntryId,
                            SPN.SuspendReasonEntry FROM dbo.tblSuspendReasonEntry AS SPN
                            WHERE SPN.IsActive = 1 AND (SPN.IsDelete = 0 OR SPN.IsDelete IS NULL) AND IsDisciplinary = 1 AND  SPN.CompanyId = " + companyId;
            aCommonInternalDal.LoadDropDownValue(ddl, "SuspendReasonEntry", "SuspendReasonEntryId", query, "HRDB");
        }

        public DataTable CheckBoxLoadActionType(string companyId)
        {
            string query = @"SELECT SPN.SuspendReasonEntryId,
                            SPN.SuspendReasonEntry FROM dbo.tblSuspendReasonEntry AS SPN
                            WHERE SPN.IsActive = 1 AND (SPN.IsDelete = 0 OR SPN.IsDelete IS NULL) AND IsDisciplinary = 1 AND  SPN.CompanyId = " + companyId;
         
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);

        }
        public void FinancialYearDropDown(DropDownList ddl, string CompanyId)
        {
            string query = "SELECT  FinancialYearId, FinancialYearDesc FROM dbo.tblFinancialYear WHERE Status='Active' and CompanyId='" + CompanyId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "FinancialYearDesc", "FinancialYearId", query, "HRDB");

        }
        public DataTable ValidattionForEffectiveDate(string id, string date)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", id));
            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", date));
            string query = @"SELECT EmpInfoId, EffectiveDate FROM dbo.tblDiciplinaryAction WHERE  EmpInfoId=@EmpInfoId and EffectiveDate=@EffectiveDate";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable DeleteValidattionForEffectiveDate(string id)
        {
            string query = @"SELECT  DiciplinaryId, EffectiveDate FROM dbo.tblDiciplinaryAction WHERE DiciplinaryId=" + id;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }
    }



}
