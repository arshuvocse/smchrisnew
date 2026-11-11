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
using Library.DAO.HRM_Entities;

namespace DAL.SuspendAndDiciplinary_Dal
{
    public class EmployeeSuspendDal
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

            string queryStr = @"SELECT EGI.EmpInfoId,EGI.EmpMasterCode,DPT.DepartmentName,ETP.EmpTypeId, ETP.EmpType, 
                                EGI.DateOfJoin, EGI.EmpName,DPT.DepartmentId,DPT.DepartmentName,DSG.DesignationId,DSG.Designation FROM dbo.tblSuspend AS SPND 
                                LEFT JOIN tblEmpGeneralInfo AS EGI ON EGI.EmpInfoId = SPND.EmpInfoId
                                LEFT JOIN dbo.tblDepartment AS DPT ON SPND.DeptId = DPT.DepartmentId
                                LEFT JOIN dbo.tblDesignation AS DSG ON SPND.DesigId = DSG.DesignationId
                                LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
                                WHERE EGI.EmpInfoId = @EmployeeCode AND EGI.IsActive=1 and EGI.EmployeeStatus='Active'";
            
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist ,"HRDB");
        }
        public DataTable LoadEmpInfoFromEmp(string empCode)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmployeeCode", empCode));

            string queryStr = @"SELECT EGI.EmpInfoId,EGI.EmpMasterCode,DPT.DepartmentName,ETP.EmpTypeId, ETP.EmpType, 
                                EGI.DateOfJoin, EGI.EmpName,DPT.DepartmentId,DPT.DepartmentName,DSG.DesignationId,DSG.Designation, EGI.* FROM dbo.tblEmpGeneralInfo AS EGI 
                                
                                LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                                LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
                                LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
                                WHERE EGI.EmpInfoId = @EmployeeCode AND EGI.IsActive=1 and EGI.EmployeeStatus='Active'";
            
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist ,"HRDB");
        }

        public Int32 SaveDataForSuspend(EmployeeSuspendDao aSuspend)
        {
            var aSqlParameterlist = new List<SqlParameter>();


            
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aSuspend.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyInfoId", aSuspend.CompanyInfoId));

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aSuspend.DivisionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DeptId", aSuspend.DeptId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SectionId", aSuspend.SectionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", aSuspend.EmpTypeId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@DesigId", aSuspend.DesigId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", aSuspend.EffectiveDate));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", aSuspend.ActionStatus));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aSuspend.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aSuspend.EntryDate));

            aSqlParameterlist.Add(new SqlParameter("@JoiningDate", aSuspend.JoiningDate));
            aSqlParameterlist.Add(new SqlParameter("@Description", aSuspend.Description));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aSuspend.Remarks));


            aSqlParameterlist.Add(new SqlParameter("@DivisionWId", aSuspend.DivisionWId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SubSectionId", aSuspend.SubSectionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", aSuspend.FinancialYearId));
            aSqlParameterlist.Add(new SqlParameter("@EmpCode", aSuspend.EmpCode));
            aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", aSuspend.SalaryLoationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@JobLocationId", aSuspend.JobLocationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aSuspend.IsActive ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ReasonId", aSuspend.ReasonId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EffectiveToDate", aSuspend.EffectiveToDate ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@AutoProcess", aSuspend.AutoProcess ?? (object)DBNull.Value));
 

            

            //aSqlParameterlist.Add(new SqlParameter("@isSuspensionLetter", aSuspend.isSuspensionLetter));
            //aSqlParameterlist.Add(new SqlParameter("@isWithPay", aSuspend.isWithPay));
            //aSqlParameterlist.Add(new SqlParameter("@isWithoutPay", aSuspend.isWithoutPay));



            const string insertQuery = @"INSERT INTO dbo.tblSuspend (
                                       EmpInfoId,CompanyInfoId,DivisionWId,DivisionId,DeptId,SectionId,SubSectionId,DesigId,EmpTypeId,EffectiveDate,ActionStatus,EntryBy,EntryDate,
	                                   IsActive,JoiningDate,Description,Remarks,ReasonId, FinancialYearId, EmpCode,SalaryLoationId,JobLocationId,AutoProcess, EffectiveToDate)
                                       VALUES (@EmpInfoId,@CompanyInfoId,@DivisionWId,@DivisionId,@DeptId,@SectionId,@SubSectionId,@DesigId,@EmpTypeId,@EffectiveDate,@ActionStatus,@EntryBy,@EntryDate,@IsActive,
                                       @JoiningDate,@Description,@Remarks,@ReasonId, @FinancialYearId, @EmpCode,@SalaryLoationId,@JobLocationId,@AutoProcess, @EffectiveToDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public DataTable LoadSuspend(string param)
        {
            string query = @"SELECT us.UserName, Dg.Designation, dpt.DepartmentName,  SPND.SuspendId,EGI.EmpMasterCode, EGI.DateOfJoin, EGI.EmpName,SPND.EffectiveDate,SPND.Description,SPND.Remarks,SPNDR.SuspendReasonEntry, IsSuspand, SPND.*  FROM dbo.tblSuspend AS SPND 
LEFT JOIN tblEmpGeneralInfo AS EGI ON EGI.EmpInfoId = SPND.EmpInfoId
LEFT JOIN dbo.tblDesignation AS Dg ON EGI.DesignationId = Dg.DesignationId
LEFT JOIN dbo.tblDepartment AS dpt ON EGI.DepartmentId= dpt.DepartmentId
LEFT JOIN dbo.tblUser AS us ON us.UserId = SPND.EntryBy
LEFT JOIN dbo.tblSuspendReasonEntry AS SPNDR ON SPND.ReasonId =  SPNDR.SuspendReasonEntryId
WHERE SPND.ActionStatus in ('Posted','Cancel') and SPND.IsActive = 1 " + param + " order by SPND.SuspendId desc";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
//        public DataTable LoadSuspend(string empcode)
//        {
//            string query = @"SELECT *
//                                   FROM tblSuspend AS SPND 
//                                   LEFT JOIN tblEmpGeneralInfo AS EGI ON EGI.EmpInfoId = SPND.EmpInfoId
//                                   LEFT JOIN dbo.tblCompanyInfo AS CI ON SPND.CompanyInfoId = CI.CompanyId
//                                   LEFT JOIN dbo.tblDivision AS DSN ON SPND.DivisionId = DSN.DivisionId
//                                   LEFT JOIN dbo.tblDivisionWing AS DSNW ON SPND.DivisionWId = DSNW.DivisionWId
//                                   LEFT JOIN dbo.tblDepartment AS DPT ON SPND.DeptId = DPT.DepartmentId
//                                   LEFT JOIN dbo.tblSection AS SEC ON SPND.SectionId = SEC.SectionId
//                                   LEFT JOIN dbo.tblSubSection AS SSEC ON SPND.SubSectionId = SSEC.SubSectionId
//                                   LEFT JOIN dbo.tblDesignation AS DSG ON SPND.DesigId = DSG.DesignationId  where EGI.EmpMasterCode='"+empcode+"' order by SPND.SuspendId desc";

//            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
//        }

        public bool UpdateDataForEmpSuspend(EmployeeSuspendDao aSuspend)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SuspendId", aSuspend.SuspendId));
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aSuspend.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyInfoId", aSuspend.CompanyInfoId));

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aSuspend.DivisionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DeptId", aSuspend.DeptId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SectionId", aSuspend.SectionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", aSuspend.EmpTypeId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@DesigId", aSuspend.DesigId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", aSuspend.EffectiveDate));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", aSuspend.ActionStatus));
           
           
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aSuspend.UpdateBy));

            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aSuspend.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@JoiningDate", aSuspend.JoiningDate));
            aSqlParameterlist.Add(new SqlParameter("@Description", aSuspend.Description));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aSuspend.Remarks));


            aSqlParameterlist.Add(new SqlParameter("@DivisionWId", aSuspend.DivisionWId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SubSectionId", aSuspend.SubSectionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", aSuspend.FinancialYearId));
            aSqlParameterlist.Add(new SqlParameter("@EmpCode", aSuspend.EmpCode));
            aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", aSuspend.SalaryLoationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@JobLocationId", aSuspend.JobLocationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aSuspend.IsActive ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ReasonId", aSuspend.ReasonId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EffectiveToDate", aSuspend.EffectiveToDate ?? (object)DBNull.Value));

            const string query = @"UPDATE tblSuspend SET   SalaryLoationId=@SalaryLoationId,JobLocationId=@JobLocationId, EmpInfoId=@EmpInfoId,CompanyInfoId=@CompanyInfoId,DivisionId=@DivisionId,DeptId=@DeptId,SectionId=@SectionId,EmpTypeId=@EmpTypeId,DesigId=@DesigId," +
                                 "EffectiveDate=@EffectiveDate,ActionStatus=@ActionStatus,UpdateBy=@UpdateBy ,JoiningDate = @JoiningDate,Description = @Description,Remarks = @Remarks" +
                                 ",ReasonId = @ReasonId, FinancialYearId=@FinancialYearId, EmpCode=@EmpCode, EffectiveToDate=@EffectiveToDate WHERE SuspendId=@SuspendId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }

        public DataTable EmpSuspendInformation(string suspendId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SuspendId", suspendId));

            const string query = @"SELECT SPND.SuspendId,EGI.EmpInfoId,EGI.EmpMasterCode,DPT.DepartmentName, ETP.EmpTypeId, ETP.EmpType,ReasonId,SPND.CompanyInfoId,
                                  EGI.DateOfJoin, EGI.EmpName,SPND.EffectiveDate,SPND.Description,
                                  SPND.Remarks,SPNDR.SuspendReasonEntry,DPT.DepartmentId,DPT.DepartmentName,DSG.DesignationId,DSG.Designation,SPND.*  FROM dbo.tblSuspend AS SPND 
                                  LEFT JOIN tblEmpGeneralInfo AS EGI ON EGI.EmpInfoId = SPND.EmpInfoId
                                  LEFT JOIN dbo.tblDepartment AS DPT ON SPND.DeptId = DPT.DepartmentId
                                  LEFT JOIN dbo.tblSuspendReasonEntry AS SPNDR ON SPND.ReasonId =  SPNDR.SuspendReasonEntryId
                                  LEFT JOIN dbo.tblDesignation AS DSG ON SPND.DesigId = DSG.DesignationId
                                  LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
								   WHERE SPND.SuspendId =  @SuspendId";

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
        public int InsertDeleteSuspendInfoById(EmployeeSuspendDao aSuspend)
        {
          
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SuspendId", aSuspend.SuspendId));
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aSuspend.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyInfoId", aSuspend.CompanyInfoId));

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aSuspend.DivisionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DeptId", aSuspend.DeptId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SectionId", aSuspend.SectionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", aSuspend.EmpTypeId ?? (object)DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@DesigId", aSuspend.DesigId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", aSuspend.EffectiveDate));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", aSuspend.ActionStatus));


            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aSuspend.EntryBy));

            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aSuspend.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@JoiningDate", aSuspend.JoiningDate));
            aSqlParameterlist.Add(new SqlParameter("@Description", aSuspend.Description));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aSuspend.Remarks));


            aSqlParameterlist.Add(new SqlParameter("@DivisionWId", aSuspend.DivisionWId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SubSectionId", aSuspend.SubSectionId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", aSuspend.FinancialYearId));
            aSqlParameterlist.Add(new SqlParameter("@EmpCode", aSuspend.EmpCode));
            aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", aSuspend.SalaryLoationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@JobLocationId", aSuspend.JobLocationId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aSuspend.IsActive ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ReasonId", aSuspend.ReasonId ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EffectiveToDate", aSuspend.EffectiveToDate ?? (object)DBNull.Value));
            string query = @"INSERT INTO dbo.DELtblSuspend (SuspendId,
                                       EmpInfoId,CompanyInfoId,DivisionWId,DivisionId,DeptId,SectionId,SubSectionId,DesigId,EmpTypeId,EffectiveDate,ActionStatus,EntryBy,EntryDate,
	                                   IsActive,JoiningDate,Description,Remarks,ReasonId, FinancialYearId, EmpCode,SalaryLoationId,JobLocationId, EffectiveToDate)
                                       VALUES (@SuspendId, @EmpInfoId,@CompanyInfoId,@DivisionWId,@DivisionId,@DeptId,@SectionId,@SubSectionId,@DesigId,@EmpTypeId,@EffectiveDate,@ActionStatus,@EntryBy,@EntryDate,@IsActive,
                                       @JoiningDate,@Description,@Remarks,@ReasonId, @FinancialYearId, @EmpCode,@SalaryLoationId,@JobLocationId, @EffectiveToDate)";
            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, "HRDB");
        }
        public DataTable GetEmployeeSuspand(string empinfoId)
        {
            string query = @"SELECT * FROM dbo.tblSuspend WHERE EmpInfoId='"+empinfoId+"' AND (IsSuspand IS NULL OR IsSuspand='0')";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
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

        public DataTable LoadSuspendReleaseList(string param)
        {
            string query = @"SELECT SPND.SuspendId,EGI.EmpMasterCode, EGI.DateOfJoin, EGI.EmpName,SPND.EffectiveDate,SPND.Description,SPND.Remarks,SPNDR.SuspendReasonEntry  FROM dbo.tblSuspend AS SPND 
LEFT JOIN tblEmpGeneralInfo AS EGI ON EGI.EmpInfoId = SPND.EmpInfoId
LEFT JOIN dbo.tblSuspendReasonEntry AS SPNDR ON SPND.ReasonId =  SPNDR.SuspendReasonEntryId  where SPND.isRelease IS NULL "+param+"  order by SPND.SuspendId desc";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public bool UpdateSuspendReleaseInfo(EmployeeSuspendDao aSuspendDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SuspendId", aSuspendDao.SuspendId));
            aSqlParameterlist.Add(new SqlParameter("@ReleasedBy", aSuspendDao.ReleasedBy));
            aSqlParameterlist.Add(new SqlParameter("@RelesedOn", aSuspendDao.RelesedOn));
            aSqlParameterlist.Add(new SqlParameter("@isRelease", aSuspendDao.isRelease));
         
            aSqlParameterlist.Add(new SqlParameter("@ReleaseRemarks", aSuspendDao.ReleaseRemarks));
            aSqlParameterlist.Add(new SqlParameter("@ReleaseExplain", aSuspendDao.ReleaseExplain));

            const string query = @"UPDATE dbo.tblSuspend SET isRelease = @isRelease , ReleasedBy = @ReleasedBy, RelesedOn = @RelesedOn,ReleaseRemarks=@ReleaseRemarks,ReleaseExplain=@ReleaseExplain WHERE SuspendId = @SuspendId";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }
        public bool UpdateSuspendReleaseInfoInEmp(string empid)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmpInfoid", empid));


            const string query = @"UPDATE dbo.tblEmpGeneralInfo SET EmployeeStatus='Active' WHERE EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }
        public void LoadActionType(DropDownList ddl, string companyId)
        {
            string query = @"
								SELECT SPN.SuspendReasonEntryId,
                            SPN.SuspendReasonEntry FROM dbo.tblSuspendReasonEntry AS SPN
                            WHERE SPN.IsActive = 1  AND (SPN.IsDelete IS NULL OR SPN.IsDelete=0) AND IsSuspend = 1 AND  SPN.CompanyId = " + companyId;
            aCommonInternalDal.LoadDropDownValue(ddl, "SuspendReasonEntry", "SuspendReasonEntryId", query, "HRDB");
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
            string query = @"SELECT EmpInfoId, EffectiveDate FROM dbo.tblSuspend WHERE  EmpInfoId=@EmpInfoId and EffectiveDate=@EffectiveDate";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable DeleteValidattionForEffectiveDate(string id)
        {
            string query = @"SELECT  SuspendId, EffectiveDate FROM dbo.tblSuspend WHERE SuspendId=" + id;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public bool UpdateEmployeeExitInfo(EmpGeneralInfo aInfo)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            //aSqlParameterlist.Add(new SqlParameter("@IsActive", aInfo.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@InactiveReason", aInfo.InactiveReason ?? (object)DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeStatus", aInfo.EmployeeStatus));
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aInfo.EmpInfoId));

            string query = @"UPDATE dbo.tblEmpGeneralInfo SET InactiveReason = @InactiveReason , EmployeeStatus = @EmployeeStatus WHERE EmpInfoId = @EmpInfoId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable FetchEmployeeInfoById(int jobLeftId)
        {
            string query = @"SELECT EmployeeId FROM tblEmployeeJobLeft WHERE EmployeeJobLeftId =  " + jobLeftId;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }


    }
}
