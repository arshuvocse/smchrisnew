using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;
using DAO.SkillWill_Dao;

namespace DAL.SKILL_WILL_DAL
{
   public class Skill_Will_Dal
    {

        ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        DataAccessManager _aAcessManager = new DataAccessManager();
        public bool UpdateContractural(EmpSkillWillAssessmentMasterAppLogDAO aMaster)
        {

            try
            {
                int pk = 0;

                if (aMaster.EmpSkillWillMasterId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@EmpSkillWillMasterId", aMaster.EmpSkillWillMasterId));
                    aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));


                    string query =
                        @"update tblEmpSkillWillAssessmentMaster set ActionStatus=@ActionStatus  where EmpSkillWillMasterId = @EmpSkillWillMasterId";

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

        public bool UpdateAppLog(string status, string id)
        {

            try
            {
                int pk = 0;

                //if (id.JdMasterId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@EmpSkillWillAssessmentMasterAppLogId", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", status));


                    string query =
                        @"update tblEmpSkillWillAssessmentMasterAppLog set ActionStatus=@ActionStatus  where EmpSkillWillAssessmentMasterAppLogId = @EmpSkillWillAssessmentMasterAppLogId";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    return result;

                }

            }
            catch (Exception exception)
            {

                throw exception;
            }
        }

        public DataTable GetEmpInfoPrevious(string forempInfoid, string jdmasterId)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblEmpSkillWillAssessmentMasterAppLog WHERE ForEmpInfoId='" + forempInfoid + "' AND EmpSkillWillMasterId='" + jdmasterId + "' AND ActionStatus NOT IN ('Review')   order by EmpSkillWillAssessmentMasterAppLogId desc";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int SaveEmpAppLog(EmpSkillWillAssessmentMasterAppLogDAO appLogDao)
        {

            try
            {
                int pk = 0;


                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@EmpSkillWillMasterId", appLogDao.EmpSkillWillMasterId));
                    aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                    aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                    aParameters.Add(new SqlParameter("@Version", appLogDao.Version));
                    aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                    aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                    aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                    aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));
                    aParameters.Add(new SqlParameter("@CommentsEMPID", appLogDao.CommentsEMP));



                    string query = @"INSERT INTO dbo.tblEmpSkillWillAssessmentMasterAppLog
                                    (
                                    [EmpSkillWillMasterId]
           ,[PreEmpInfoId]
           ,[ForEmpInfoId]
           ,[Version]
           ,[ApproveBy]
           ,[ApproveDate]
           ,[ActionStatus]
           ,[Comments]
           ,[CommentId]
                                    )
  VALUES
                                     (@EmpSkillWillMasterId 
           ,@PreEmpInfoId 
           ,@ForEmpInfoId 
           ,(SELECT (COUNT(*)+1) FROM dbo.tblEmpSkillWillAssessmentMasterAppLog WHERE EmpSkillWillMasterId=@EmpSkillWillMasterId)
           ,@ApproveBy 
           ,@ApproveDate 
           ,@ActionStatus 
           ,@Comments 
           ,@CommentsEMPID )";

                    pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                }


                return pk;
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }
        public DataTable GetSKILLALL()
        {
            try
            {
                string query = @"SELECT * from tblSkill ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable GetWillALL()
        {
            try
            {
                string query = @"SELECT '' Areasconsidered, * FROM tblWILL";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public int SaveEmpSkillWillMaster(EmpSkillWillAssessmentMaster aMaster, string user)
        {
            try
            {
                if (aMaster.EmpSkillWillMasterId > 0)
                {
                    /////asdasddasd
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@EmpSkillWillMasterId", aMaster.EmpSkillWillMasterId));
                    aParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@EntryBy", user));
                    aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));
                    aParameters.Add(new SqlParameter("@EntryEmpId", HttpContext.Current.Session["EmpInfoId"].ToString()));

                    string query = @" UPDATE [dbo].[tblEmpSkillWillAssessmentMaster]
   SET  [UpdateBy] =  @EntryBy 
      ,[UpdateDate] = @EntryDate 
 WHERE EmpSkillWillMasterId=@EmpSkillWillMasterId";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);

                    if (result == true)
                    {
                        return aMaster.EmpSkillWillMasterId;
                    }
                    else
                    {
                        return 0;
                    }

                }
                else
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@EntryEmpId", HttpContext.Current.Session["EmpInfoId"].ToString()));

                    aParameters.Add(new SqlParameter("@EntryBy", user));
                    aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));
                    aParameters.Add(new SqlParameter("@IsActive", true));
                    string query = @"Insert into tblEmpSkillWillAssessmentMaster (EmpInfoId,EntryBy,EntryDate,IsActive,FinancialYearId,ActionStatus,EntryEmpId ) values(@EmpInfoId,@EntryBy,@EntryDate,@IsActive,@FinancialYearId,'Pending',@EntryEmpId)";
                    int pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                    return pk;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public bool SaveEmpSkillWillDetails(List<EmpSkillWillDetailsDao> aList, int masterid)
        {
            try
            {
                List<SqlParameter> aParametersd = new List<SqlParameter>();
                aParametersd.Add(new SqlParameter("@EmpSkillWillMasterId", masterid));
                string queryDel = @"Delete from tblEmpSkillWillAssessmentDetails where EmpSkillWillMasterId = @EmpSkillWillMasterId";

                bool delRes = _aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParametersd, DataBase.HRDB);


                bool result = false;
                foreach (var item in aList)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();

                    aParameters.Add(new SqlParameter("@EmpSkillWillMasterId", masterid));
                    aParameters.Add(new SqlParameter("@KRA",  (object)item.KRA ?? DBNull.Value));
                    aParameters.Add(new SqlParameter("@SKILL", item.SKILL));
                    aParameters.Add(new SqlParameter("@WILL", item.WILL));
                    aParameters.Add(new SqlParameter("@Areasconsidered", item.Areasconsidered));

                    string query = @"insert into tblEmpSkillWillAssessmentDetails(EmpSkillWillMasterId, KRA, SKILL, WILL,Areasconsidered) values(@EmpSkillWillMasterId, @KRA, @SKILL, @WILL,@Areasconsidered)";
                    result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);

                    if (result == false)
                    {
                        return false;
                    }


                }
                return result;


            }
            catch (Exception x)
            {

                throw;
            }
        }



        public DataTable GetSkillWillMasterALL()
        {
            try
            {
                string query = @"SELECT ( 'Employee ID: '+ EMP.EmpMasterCode + ', Employee Name: ' + EMP.EmpName +    ISNULL(', Designation: ' + desg.Designation,'')) employee,* FROM tblEmpSkillWillAssessmentMaster M 
LEFT JOIN tblEmpGeneralInfo EMP ON M.EmpInfoId = EMP.EmpInfoId
LEFT JOIN tblDesignation desg ON EMP.DesignationId = desg.DesignationId
LEFT JOIN tblDepartment dpt ON EMP.DepartmentId = dpt.DepartmentId";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetSkillWillMasterApprovalList()
        {
            try
            {
                string query = @"SELECT ( 'Employee ID: '+ EMP.EmpMasterCode + ', Employee Name: ' + EMP.EmpName +    ISNULL(', Designation: ' + desg.Designation,'')) employee,* FROM tblEmpSkillWillAssessmentMaster M 
LEFT JOIN tblEmpGeneralInfo EMP ON M.EmpInfoId = EMP.EmpInfoId
LEFT JOIN tblDesignation desg ON EMP.DesignationId = desg.DesignationId
LEFT JOIN tblDepartment dpt ON EMP.DepartmentId = dpt.DepartmentId
 INNER JOIN (SELECT EmpSkillWillMasterId,MAX(Version)MaxVer FROM dbo.tblEmpSkillWillAssessmentMasterAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY EmpSkillWillMasterId) AS CELog ON CELog.EmpSkillWillMasterId= M.EmpSkillWillMasterId
								INNER JOIN dbo.tblEmpSkillWillAssessmentMasterAppLog ON tblEmpSkillWillAssessmentMasterAppLog.EmpSkillWillMasterId = M.EmpSkillWillMasterId
                                where    Version=CELog.MaxVer and      ForEmpInfoId = '" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'    and      EntryEmpId  not in ( '" + HttpContext.Current.Session["EmpInfoId"].ToString() + "')";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetSkillWillDetailsById(int id)
        {
            try
            {
                string query = @"SELECT  EMP.EmpName ,* FROM tblEmpSkillWillAssessmentDetails D
LEFT JOIN  tblEmpSkillWillAssessmentMaster M ON D.EmpSkillWillMasterId = M.EmpSkillWillMasterId
LEFT JOIN tblEmpGeneralInfo EMP ON M.EmpInfoId = EMP.EmpInfoId
WHERE M.EmpSkillWillMasterId IS NOT NULL AND D.EmpSkillWillMasterId= " + id + "";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
