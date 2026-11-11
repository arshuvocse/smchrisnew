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

namespace DAL.RecruitmentApp_DAL
{
    public class RecruitmentAppDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public DataTable GetRecruitmentInfo(string param)
        {
            string query = @"
SELECT * FROM dbo.tblRecruitmentApproval RA 
LEFT JOIN tblJobCreation JC ON JC.JobID = RA.JobId

                                   LEFT JOIN tblCompanyInfo AS CI ON JC.CompanyId = CI.CompanyId
                                   LEFT JOIN tblJobCreationLocation AS JCL ON JC.JobID = JCL.JobID
                                  
                                   LEFT JOIN dbo.tblJobReqForm AS RF ON JC.ReqCodeId = RF.JobReqId
                                   LEFT JOIN dbo.tblFinancialYear AS FY ON RF.FinYearId = FY.FinancialYearId
                                   LEFT JOIN dbo.tblDepartment AS dpt ON RF.DeptId = dpt.DepartmentId 
								   
							LEFT JOIN dbo.tblFinancialYear AS FINY ON RF.FinYearId = FINY.FinancialYearId
                            INNER JOIN (SELECT RecruitmentId,MAX(Version)MaxVer FROM dbo.tblRecruitmentApprovalAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY RecruitmentId) AS CELog ON CELog.RecruitmentId= RA.RecruitmentId
							INNER JOIN dbo.tblRecruitmentApprovalAppLog ON tblRecruitmentApprovalAppLog.RecruitmentId = RA.RecruitmentId
                                where Version=CELog.MaxVer   and  ForEmpInfoId = '" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'";


   

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetRecruitmentInfoCheck(string param)
        {
            string query = @"
SELECT * FROM dbo.tblRecruitmentApproval RA 
LEFT JOIN tblJobCreation JC ON JC.JobID = RA.JobId

                                   LEFT JOIN tblCompanyInfo AS CI ON JC.CompanyId = CI.CompanyId
                                   LEFT JOIN tblJobCreationLocation AS JCL ON JC.JobID = JCL.JobID
                                  
                                   LEFT JOIN dbo.tblJobReqForm AS RF ON JC.ReqCodeId = RF.JobReqId
                                   LEFT JOIN dbo.tblFinancialYear AS FY ON RF.FinYearId = FY.FinancialYearId
                                   LEFT JOIN dbo.tblDepartment AS dpt ON RF.DeptId = dpt.DepartmentId 
								   
							LEFT JOIN dbo.tblFinancialYear AS FINY ON RF.FinYearId = FINY.FinancialYearId
                            INNER JOIN (SELECT RecruitmentId,MAX(Version)MaxVer FROM dbo.tblRecruitmentApprovalAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY RecruitmentId) AS CELog ON CELog.RecruitmentId= RA.RecruitmentId
							INNER JOIN dbo.tblRecruitmentApprovalAppLog ON tblRecruitmentApprovalAppLog.RecruitmentId = RA.RecruitmentId
                                where Version=CELog.MaxVer   and  ForEmpInfoId = '"+param ;




            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable GetCandidateInfo(string id)
        {
            string query = @"SELECT * FROM dbo.tblInterViewCandidateSelection
LEFT JOIN dbo.tblInterviewCandidateInfo ON tblInterviewCandidateInfo.CandidateID = tblInterViewCandidateSelection.CandidateID
WHERE tblInterViewCandidateSelection.JobID='"+id+"' AND CandidateStatus='Selected'";




            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public void LoadFinancialYear(DropDownList ddl, string comapnyId)
        {
            string query = @"SELECT FINY.FinancialYearId,
                            FINY.FinancialYearDesc FROM dbo.tblFinancialYear AS FINY 
                            WHERE FINY.Status = 'Active' AND FINY.CompanyId ='" + comapnyId + "'";

            aCommonInternalDal.LoadDropDownValue(ddl, "FinancialYearDesc", "FinancialYearId", query, "HRDB");
        }
        public DataTable GetPositionDescriptionListByReqCode(int id)
        {
            string query = @"   SELECT * FROM dbo.tblRecruitmentApproval Where RecruitmentId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public bool UpdateStatus(string jobReqId, string status)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@JobID", jobReqId));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", status));

            const string query = @"UPDATE dbo.tblJobCreation SET ActionStatus=@ActionStatus WHERE JobID=@JobID";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }
        public Int32 SaveRecruitmentApp(RecruitmentApprovalDAO approvalDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            //aSqlParameterlist.Add(new SqlParameter("@RecruitmentId", approvalDao.RecruitmentId));
            aSqlParameterlist.Add(new SqlParameter("@JobId", approvalDao.JobId));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", approvalDao.ActionStatus));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus2", approvalDao.ActionStatus2));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", approvalDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", approvalDao.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@ApproveBy", approvalDao.ApproveBy));
            aSqlParameterlist.Add(new SqlParameter("@ApproveDate", approvalDao.ApproveDate));




            string insertQuery = @"INSERT INTO dbo.tblRecruitmentApproval
                                    (
                                        JobId,
                                        ActionStatus,
                                        ActionStatus2,
                                        EntryBy,
                                        EntryDate,
                                        ApproveBy,
                                        ApproveDate
                                    )
                                    VALUES
                                    (   @JobId,
                                        @ActionStatus,
                                        @ActionStatus2,
                                        @EntryBy,
                                        @EntryDate,
                                        @ApproveBy,
                                        @ApproveDate
                                    )";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");

        }

        public DataTable GetEmpInfo(string param)
        {
            try
            {
                string query = @"SELECT *FROM dbo.tblEmpGeneralInfo " + param + "";

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool UpdateContractural(string actionstatus, int id)
        {

            try
            {
                int pk = 0;

                if (id > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@RecruitmentId", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", actionstatus));


                    string query =
                        @"update tblRecruitmentApproval set ActionStatus=@ActionStatus  where RecruitmentId = @RecruitmentId";

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
        public bool UpdateJobReqStatus2(RecruitmentApprovalDAO aMaster)
        {

            try
            {
                int pk = 0;

                if (aMaster.RecruitmentId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@RecruitmentId", aMaster.RecruitmentId));
                    aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));


                    string query =
                        @"update tblRecruitmentApproval set ActionStatus2=@ActionStatus  where RecruitmentId = @RecruitmentId";

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
        public DataTable LoadInformationAppALl(string param)
        {
            string queryStr = @"SELECT  EPE.RecruitmentId,  Com.CompanyName,  EPE.JobTitle, EPE.ReqDate, EPE.Nos,FINY.FinancialYearDesc, Dpt.DepartmentName, EPE.ActionStatus,* From dbo.tblRecruitmentApproval EPE
 
 left JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
 LEFT JOIN dbo.tblFinancialYear AS FINY ON EPE.FinYearId = FINY.FinancialYearId
  LEFT JOIN dbo.tblDepartment AS Dpt ON EPE.DeptId = Dpt.DepartmentId 
INNER JOIN (SELECT RecruitmentId,MAX(Version)MaxVer FROM dbo.tblRecruitmentApprovalAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY RecruitmentId) AS CELog ON CELog.RecruitmentId= EPE.RecruitmentId
							INNER JOIN dbo.tblRecruitmentApprovalAppLog ON tblRecruitmentApprovalAppLog.RecruitmentId = EPE.RecruitmentId
                                where Version=CELog.MaxVer   and  ForEmpInfoId = '" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'";

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetEmpInfoPrevious(string forempInfoid, string jdmasterId)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblRecruitmentApprovalAppLog WHERE ForEmpInfoId='" + forempInfoid + "' AND RecruitmentId='" + jdmasterId + "' AND ActionStatus NOT IN ('Review')";

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
                    aParameters.Add(new SqlParameter("@RecruitmentAppLogId", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", status));


                    string query =
                        @"update tblRecruitmentApprovalAppLog set ActionStatus=@ActionStatus  where RecruitmentAppLogId = @RecruitmentAppLogId";

                    bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    return result;

                }

            }
            catch (Exception exception)
            {

                throw exception;
            }
        }
        public int SavAppLog(RecruitmentApprovalAppLogDAO appLogDao)
        {

            try
            {
                int pk = 0;


                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@RecruitmentId", appLogDao.RecruitmentId));
                    aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                    aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                    aParameters.Add(new SqlParameter("@Version", appLogDao.Version));
                    aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                    aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                    aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                    aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));
                    aParameters.Add(new SqlParameter("@CommentsId", appLogDao.CommentsId ?? (object)DBNull.Value));


                    string query = @"INSERT INTO dbo.tblRecruitmentApprovalAppLog
                                    (
                                    RecruitmentId,
                                    PreEmpInfoId,
                                    ForEmpInfoId,
                                    Version,
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments,CommentId
                                    )
                                    VALUES(
                                    @RecruitmentId,
                                    @PreEmpInfoId,
                                    @ForEmpInfoId,
                                    (SELECT (COUNT(*)+1) FROM dbo.tblRecruitmentApprovalAppLog WHERE RecruitmentId=@RecruitmentId),
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


                    string query = @" INSERT INTO dbo.tblRecruitmentApprovalAppLogComnt
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


        public DataTable GetAppLogStatus(string mid, string forempId)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblRecruitmentApprovalAppLog WHERE ForEmpInfoId='" + forempId + "' AND RecruitmentId='" + mid + "' AND ActionStatus<>'Review'";

                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



    }
}
