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

namespace DAL.MPBudget
{
    public class MPBudgetCommonDAL
    {
        private ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();


        public DataTable GetAppLogEmployeeApprovalID(string mid)
        {
            try
            {
                string query = @"select distinct  u.EmpInfoId from [dbo].tblMPBudgetMaster J

inner join [dbo].tblMPBudgetMasterAppLog A on J.MPBudgetMasterId=A.MPBudgetMasterId
inner join tblUser U on U.UserId=A.ApproveBy
inner join tblEmpGeneralInfo E on E.EmpInfoId=U.EmpInfoId where j.MPBudgetMasterId=" + mid;

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetMPBudgetById(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@mid", mid));

            const string queryStr = @"SELECT m.MPBudgetMasterId,
       m.BudgetCode,
       m.CompanyId,
       m.DepartmentId,
       m.FinancialYearId,EGE.EmpInfoId,* FROM dbo.tblMPBudgetMaster m 
    LEFT JOIN dbo.tblUser AS U ON U.UserId = m.EntryBy
							left JOIN dbo.tblEmpGeneralInfo EGE ON EGE.EmpInfoId=U.EmpInfoId
            WHERE m.IsActive=1 AND m.MPBudgetMasterId=@mid";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetAppLogCommById(int Id)
        {
            string query = @"SELECT Alg.MPBudgetMasterAppLogId, emp.EmpName PreEmp, emp2.EmpName ForEmp, Version, Us.UserName ApproveBy, Alg.ActionStatus, Alg.ApproveDate, Alg.MPBudgetMasterId, Alg.Comments FROM dbo.tblMPBudgetMasterAppLog Alg
LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId=Alg.PreEmpInfoId
LEFT JOIN dbo.tblEmpGeneralInfo emp2 ON emp2.EmpInfoId=Alg.ForEmpInfoId
LEFT JOIN dbo.tblUser Us ON Alg.ApproveBy=Us.UserId WHERE  Alg.ActionStatus!= 'Drafted' and  Alg.MPBudgetMasterId='" + Id + "'";
            return _aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public bool UpdateStatus(string id, string status, string approveby, DateTime approvedate)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MPBudgetMasterId", id));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", status));
            aSqlParameterlist.Add(new SqlParameter("@ApproveBy", approveby));
            aSqlParameterlist.Add(new SqlParameter("@ApproveDate", approvedate));


            const string query = @"UPDATE dbo.tblMPBudgetMaster SET ActionStatus=@ActionStatus,ApproveBy=@ApproveBy,ApproveDate=@ApproveDate WHERE MPBudgetMasterId=@MPBudgetMasterId";
            return _aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }
        public int SaveMPBudgetAppLog(MPBudgetAppLogDAO appLogDao)
        {

            try
            {
                int pk = 0;


                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@MPBudgetMasterId", appLogDao.MPBudgetMasterId));
                    aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                    aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                    aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                    aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));


                    string query = @"INSERT INTO dbo.tblMPBudgetAppLog
                                    (
                                    MPBudgetMasterId,
                                    
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments
                                    )
                                    VALUES(
                                    @MPBudgetMasterId,
                                    
                                    @ApproveBy,
                                    @ApproveDate,
                                    @ActionStatus,@Comments
                                    )";

                    pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                }


                return pk;
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }

        public List<MPBudgetViewModel> GetMPBudgetList()
        {
            List<MPBudgetViewModel> lModels = null;
            var aSqlParameterlist = new List<SqlParameter>();

            const string queryStr = @"SELECT bm.MPBudgetMasterId,
       bm.BudgetCode,
       bm.CompanyId,
	   c.ShortName,
       bm.DepartmentId,
	   d.DepartmentName,
       bm.FinancialYearId,
	   fy.FinancialYearDesc FROM dbo.tblMPBudgetMaster bm 
LEFT JOIN dbo.tblCompanyInfo c ON c.CompanyId = bm.CompanyId
LEFT JOIN dbo.tblDepartment d ON d.DepartmentId=bm.DepartmentId
LEFT JOIN dbo.tblFinancialYear fy ON fy.FinancialYearId = bm.FinancialYearId
WHERE bm.IsActive=0 ";
            using (DataTable dt = _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB))
            {
                if (dt.Rows.Count>0)
                {
                    lModels = new List<MPBudgetViewModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MPBudgetViewModel model = new MPBudgetViewModel()
                        {
                            MPBudgetMasterId = int.Parse(dt.Rows[i]["MPBudgetMasterId"].ToString()),
                            BudgetCode = dt.Rows[i]["BudgetCode"].ToString(),
                            CompanyId = int.Parse(dt.Rows[i]["CompanyId"].ToString()),
                            CompanyShortName = dt.Rows[i]["ShortName"].ToString(),
                            DepartmentName = dt.Rows[i]["DepartmentName"].ToString(),
                            DepartmentId = int.Parse(dt.Rows[i]["DepartmentId"].ToString()),
                            FinancialYearId = int.Parse(dt.Rows[i]["FinancialYearId"].ToString()),
                            FinancialYearDesc = dt.Rows[i]["FinancialYearDesc"].ToString(),


                            //DesignationId = int.Parse(dt.Rows[i]["DesignationId"].ToString()),
                            //ExistingEmployee = int.Parse(dt.Rows[i]["ExistingEmployee"].ToString()),
                            //ExistingSalary = decimal.Parse(dt.Rows[i]["ExistingSalary"].ToString()),
                            //SalaryGradeId = int.Parse(dt.Rows[i]["SalaryGradeId"].ToString()),
                            //SalaryStepId = int.Parse(dt.Rows[i]["SalaryStepId"].ToString()),
                            //GExistingEmployee = int.Parse(dt.Rows[i]["GExistingEmployee"].ToString()),
                            //GExistingSalary = decimal.Parse(dt.Rows[i]["GExistingSalary"].ToString()),
                            //EmployeeRequisition = int.Parse(dt.Rows[i]["EmployeeRequisition"].ToString()),
                            //ReqApproxSalary = decimal.Parse(dt.Rows[i]["ReqApproxSalary"].ToString()),
                            //ReqTotalSalary = decimal.Parse(dt.Rows[i]["ReqTotalSalary"].ToString()),
                            //QuarterId = int.Parse(dt.Rows[i]["QuarterId"].ToString()),
                            //Designation = dt.Rows[i]["Designation"].ToString(),
                            //GradeName = dt.Rows[i]["GradeName"].ToString(),
                            //SalaryStepName = dt.Rows[i]["SalaryStepName"].ToString(),
                            //QuarterName = dt.Rows[i]["QuarterName"].ToString()
                        };
                        lModels.Add(model);
                    }
                }
            }
            return lModels;
        }

        public List<MPBudgetViewModel> FilterMPBudgetTable(string company, string dept, string finyear)
        {
            List<MPBudgetViewModel> lModels = null;
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@company", company));
            aSqlParameterlist.Add(new SqlParameter("@dept", dept));
            aSqlParameterlist.Add(new SqlParameter("@finyear", finyear));
            const string queryStr = @"SELECT bm.MPBudgetMasterId,
       bm.BudgetCode,
       bm.CompanyId,
	   c.ShortName,
       bm.DepartmentId,
	   d.DepartmentName,
       bm.FinancialYearId,
	   fy.FinancialYearDesc FROM dbo.tblMPBudgetMaster bm 
LEFT JOIN dbo.tblCompanyInfo c ON c.CompanyId = bm.CompanyId
LEFT JOIN dbo.tblDepartment d ON d.DepartmentId=bm.DepartmentId
LEFT JOIN dbo.tblFinancialYear fy ON fy.FinancialYearId = bm.FinancialYearId
WHERE bm.IsActive=1 
AND bm.CompanyId=@company
AND bm.DepartmentId=COALESCE(NULLIF(@dept,'-1'),bm.DepartmentId)
AND bm.FinancialYearId=COALESCE(NULLIF(@finyear,'-1'),bm.FinancialYearId)";
            using (DataTable dt = _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB))
            {
                if (dt.Rows.Count > 0)
                {
                    lModels = new List<MPBudgetViewModel>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        MPBudgetViewModel model = new MPBudgetViewModel()
                        {
                            MPBudgetMasterId = int.Parse(dt.Rows[i]["MPBudgetMasterId"].ToString()),
                            BudgetCode = dt.Rows[i]["BudgetCode"].ToString(),
                            CompanyId = int.Parse(dt.Rows[i]["CompanyId"].ToString()),
                            CompanyShortName = dt.Rows[i]["ShortName"].ToString(),
                            DepartmentName = dt.Rows[i]["DepartmentName"].ToString(),
                            DepartmentId = int.Parse(dt.Rows[i]["DepartmentId"].ToString()),
                            FinancialYearId = int.Parse(dt.Rows[i]["FinancialYearId"].ToString()),
                            FinancialYearDesc = dt.Rows[i]["FinancialYearDesc"].ToString()
                        };
                        lModels.Add(model);
                    }
                }
            }
            return lModels;
        }


        public void OrphanDetailsInActive(int mid, string detailsTobeInactiveExcept)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@mid", mid));
            aSqlParameterlist.Add(new SqlParameter("@detailsTobeInactiveExcept", detailsTobeInactiveExcept));

            string queryStr = @"UPDATE dbo.tblMPBudgetDetails SET IsActive=0 WHERE MPBudgetMasterId=@mid AND MPBudgetDetailsId NOT IN ("+detailsTobeInactiveExcept+");select 1";
             _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetMPBudgetDetailsBymid(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@mid", mid));

            const string queryStr = @"SELECT d.MPBudgetDetailsId,
              d.MPBudgetMasterId,
              d.DesignationId,
			  d.Designation,
              d.EmployeeRequisition,
              d.ReqApproxSalary,
              d.ReqTotalSalary,
              d.QuarterId,
			  q.QuarterName,
              d.SalaryGradeId,
			  (sg.GradeCode + ' : ' +sg.GradeName) AS GradeName,
              d.DtlRemarks,
			  d.EmployeeTypeId,
			  et.EmpType AS EmployeeType,
			  d.ProjectId AS ProjectId,
			  p.ProjectName AS Project,
			  d.EmpCategoryId,
			  ec.EmpCategoryName 			  
			  FROM dbo.tblMPBudgetDetails d 
			  LEFT JOIN dbo.tblQuarterInfo q ON q.QuarterId = d.QuarterId
			  LEFT JOIN dbo.tblSalaryGrade sg ON sg.SalaryGradeId = d.SalaryGradeId
			  LEFT JOIN dbo.tblEmployeeType et ON d.EmployeeTypeId=et.EmpTypeId
			  LEFT JOIN dbo.tblEmpCategory ec ON ec.EmpCategoryId=d.EmpCategoryId
			  LEFT JOIN dbo.tblProjectSetup p ON d.ProjectId=p.ProjectId
			  WHERE  d.MPBudgetMasterId=@mid";
           return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }



        public DataTable GetMPBudgetMasterInfoDALrpt(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MPBudgetMasterId", Id));
            const string queryStr = @"    SELECT div.DivisionName, divWingd.DivisionWingName, bm.MPBudgetMasterId,
       bm.BudgetCode,
	   c.ShortName,
	   d.DepartmentName,
	   fy.FinancialYearDesc FROM dbo.tblMPBudgetMaster bm 
LEFT JOIN dbo.tblCompanyInfo c ON c.CompanyId = bm.CompanyId
LEFT JOIN dbo.tblDepartment d ON d.DepartmentId=bm.DepartmentId
LEFT JOIN dbo.tblFinancialYear fy ON fy.FinancialYearId = bm.FinancialYearId
LEFT JOIN dbo.tblDivisionWing divWing ON d.DivisionWId=divWing.DivisionWId
LEFT JOIN dbo.tblDivision div ON divWing.DivisionId=div.DivisionId

left JOIN (SELECT  DivisionWId, DivisionWingName  FROM  dbo.tblDivisionWing WHERE Invisible=0 ) divWingd ON divWingd.DivisionWId=d.DivisionWId                          
--left JOIN (SELECT  DivisionId, DivisionName  FROM  dbo.tblDivision ) div  ON div.DivisionId=divWing.DivisionId                                  
 
WHERE   bm.MPBudgetMasterId=@MPBudgetMasterId and ( bm.IsDelete IS NULL OR bm.IsDelete = 0) ";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetMPBudgetDetailsInfoDALrpt(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MPBudgetMasterId", Id));
            const string queryStr = @" SELECT d.MPBudgetDetailsId,
              d.MPBudgetMasterId,
              d.DesignationId,
			  d.Designation,
              d.EmployeeRequisition,
              d.ReqApproxSalary,
              d.ReqTotalSalary,
              d.QuarterId,
			  q.QuarterName,
              d.SalaryGradeId,
			  (sg.GradeCode + ' : ' +sg.GradeName) AS GradeName,
              d.DtlRemarks,
			  d.EmployeeTypeId,
			  et.EmpType AS EmployeeType,
			  d.ProjectId AS ProjectId,
			  p.ProjectName AS Project,
			  d.EmpCategoryId,
			  ec.EmpCategoryName 			  
			  FROM dbo.tblMPBudgetDetails d 
			  LEFT JOIN dbo.tblQuarterInfo q ON q.QuarterId = d.QuarterId
			  LEFT JOIN dbo.tblSalaryGrade sg ON sg.SalaryGradeId = d.SalaryGradeId
			  LEFT JOIN dbo.tblEmployeeType et ON d.EmployeeTypeId=et.EmpTypeId
			  LEFT JOIN dbo.tblEmpCategory ec ON ec.EmpCategoryId=d.EmpCategoryId
			  LEFT JOIN dbo.tblProjectSetup p ON d.ProjectId=p.ProjectId WHERE d.MPBudgetMasterId=@MPBudgetMasterId";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        /// Approval Code/////


        public DataTable GetEmpInfoPrevious(string forempInfoid, string jdmasterId)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblMPBudgetMasterAppLog WHERE ForEmpInfoId='" + forempInfoid + "' AND MPBudgetMasterId='" + jdmasterId + "' AND ActionStatus NOT IN ('Review')     order by MPBudgetMasterAppLogId desc ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
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
                    aParameters.Add(new SqlParameter("@MPBudgetMasterAppLogId", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", status));


                    string query =
                        @"update tblMPBudgetMasterAppLog set ActionStatus=@ActionStatus  where MPBudgetMasterAppLogId = @MPBudgetMasterAppLogId";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    return result;

                }

            }
            catch (Exception exception)
            {

                throw exception;
            }
        }
        public int SavAppLog(MPBudgetMasterAppLogDAO appLogDao)
        {

            try
            {
                int pk = 0;


                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@MPBudgetMasterId", appLogDao.MPBudgetMasterId));
                    aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                    aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                    aParameters.Add(new SqlParameter("@Version", appLogDao.Version));
                    aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                    aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                    aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                    aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));
                    aParameters.Add(new SqlParameter("@CommentsId", appLogDao.CommentsId));


                    string query = @"INSERT INTO dbo.tblMPBudgetMasterAppLog
                                    (
                                    MPBudgetMasterId,
                                    PreEmpInfoId,
                                    ForEmpInfoId,
                                    Version,
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments,CommentId
                                    )
                                    VALUES(
                                    @MPBudgetMasterId,
                                    @PreEmpInfoId,
                                    @ForEmpInfoId,
                                    (SELECT (COUNT(*)+1) FROM dbo.tblMPBudgetMasterAppLog WHERE MPBudgetMasterId=@MPBudgetMasterId),
                                    @ApproveBy,
                                    @ApproveDate,
                                    @ActionStatus,@Comments,@CommentsId
                                    )";

                    pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
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


                    string query = @" INSERT INTO dbo.tblMPBudgetMasterAppLogComnt
                                    (
                                        EmpInfoId,
                                        Comments
                                    )
                                    VALUES
                                    (   @EmpInfoId,
                                        @Comments
                                    )";

                    pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                }


                return pk;
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }
        public DataTable GetSupervisorEmployeeAppId(string empinfoId, string fromempInfoId)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblSupevisorMenuApproval WHERE EmpInfoId='" + empinfoId + "' AND FromEmpInfoId='" + fromempInfoId + "'";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
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

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool UpdateContractural(string actionstatus ,int id)
        {

            try
            {
                int pk = 0;

                if (id > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@MPBudgetMasterId", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", actionstatus));


                    string query =
                        @"update tblMPBudgetMaster set ActionStatus=@ActionStatus  where MPBudgetMasterId = @MPBudgetMasterId";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
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


            string queryStr = @"SELECT * FROM dbo.tblMPBudgetMaster WHERE ActionStatus='" + actionstatu + "' AND EntryBy='" + entryby + "' AND MPBudgetMasterId='" + id + "'";

            return _aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }

        public bool UpdateJobReqStatus2(string actionstatus, int id)
        {

            try
            {
                int pk = 0;

                if (id > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@MPBudgetMasterId", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", actionstatus));


                    string query =
                        @"update tblMPBudgetMaster set ActionStatus2=@ActionStatus  where MPBudgetMasterId = @MPBudgetMasterId";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    return result;

                }

            }
            catch (Exception exception)
            {

                throw exception;
            }
            return true;
        }
        public DataTable GetEmpInfo(string param)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblEmpGeneralInfo " + param + "";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable GetAppLogStatus(string mid, string forempId)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblMPBudgetMasterAppLog WHERE ForEmpInfoId='" + forempId + "' AND MPBudgetMasterId='" + mid + "' AND ActionStatus<>'Review'";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




    }
}
