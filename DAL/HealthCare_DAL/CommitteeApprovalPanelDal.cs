using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HealthCare_Dao;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;

namespace DAL.HealthCare_DAL
{
   public class CommitteeApprovalPanelDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        DataAccessManager accessManager = new DataAccessManager();

        public DataTable GetFianncialYearByComIdDDl(int id)
        {
            string query = @"SELECT FinancialYearId as Value,FinancialYearDesc as TextField FROM tblFinancialYear where CompanyId ="+id+" and Status ='Active' Convert(date,'"+DateTime.Now+"') Between StartDate and EndDate ";
            return aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);

        }

        public void Load_Meeting(DropDownList ddl)
        {
            string queryStr = @"SELECT TopsheetGeneMasId,MeetingNo +' ('+ CONVERT(varchar,MeetingDate,106) + ')' MeetingNo from TopSheetGenerateMaster_H";
            aCommonInternalDal.LoadDropDownValue(ddl, "MeetingNo", "TopsheetGeneMasId", queryStr, DataBase.HRDB);
        }
        public DataTable Get_CommitteePanel(string param, string Idd)
        {
            try
            {
                string query = "";

                if(Idd== "459")
                {
                    query = @"SELECT   R.Relationship, R.PatientName, FORMAT(R.SubmitDate,'dd-MMM-yyyy') SubmitDate, tblamt.Amount, * FROM TopSheetGenerateMaster_H H 
                LEFT JOIN TopSheetGenerateDetails_H D ON D.TopsheetGeneMasId = H.TopsheetGeneMasId
                LEFT JOIN tbl_ReimbursmentFormMaster_HealthCare R ON D.ReimbursFromMasterId = R.ReimbursFromMasterId
                LEFT JOIN tblEmpGeneralInfo EMP ON R.EmpInfoId = Emp.EmpInfoId
                LEFT JOIN tblDivision Divi ON R.DivisionId = Divi.DivisionId
                LEFT JOIN tblDepartment Dept ON R.DepartmentId = Dept.DepartmentId
                LEFT JOIN tblDesignation DEG ON R.DesignationId = DEG.DesignationId
                LEFT JOIN tblCompanyInfo COM ON R.CompanyId = COM.CompanyId
                LEFT JOIN tblReimbursementSelfAppLog lg ON R.ReimbursFromMasterId = lg.ReimbursFromMasterId

left join (select ReimbursFromMasterId, SUM(tbl_ReimbursmentformClaimDetails_HC.Amount) Amount from tbl_ReimbursmentformClaimDetails_HC  group by ReimbursFromMasterId) tblamt on tblamt.ReimbursFromMasterId=R.ReimbursFromMasterId 
                INNER JOIN (SELECT ReimbursFromMasterId,MAX(Version)MaxVer FROM dbo.tblReimbursementSelfAppLog  GROUP BY ReimbursFromMasterId) AS CELog ON CELog.ReimbursFromMasterId= R.ReimbursFromMasterId
                WHERE R.ReimbursFromMasterId IS NOT NULL  AND    Version=CELog.MaxVer    " + param + "  order by H.TopsheetGeneMasId DESC ";

                }
                else
                {
                    query = @"SELECT   R.Relationship, R.PatientName, FORMAT(R.SubmitDate,'dd-MMM-yyyy') SubmitDate, tblamt.Amount, * FROM TopSheetGenerateMaster_H H 
                LEFT JOIN TopSheetGenerateDetails_H D ON D.TopsheetGeneMasId = H.TopsheetGeneMasId
                LEFT JOIN tbl_ReimbursmentFormMaster_HealthCare R ON D.ReimbursFromMasterId = R.ReimbursFromMasterId
                LEFT JOIN tblEmpGeneralInfo EMP ON R.EmpInfoId = Emp.EmpInfoId
                LEFT JOIN tblDivision Divi ON R.DivisionId = Divi.DivisionId
                LEFT JOIN tblDepartment Dept ON R.DepartmentId = Dept.DepartmentId
                LEFT JOIN tblDesignation DEG ON R.DesignationId = DEG.DesignationId
                LEFT JOIN tblCompanyInfo COM ON R.CompanyId = COM.CompanyId
                LEFT JOIN tblReimbursementSelfAppLog lg ON R.ReimbursFromMasterId = lg.ReimbursFromMasterId

left join (select ReimbursFromMasterId, SUM(tbl_ReimbursmentformClaimDetails_HC.Amount) Amount from tbl_ReimbursmentformClaimDetails_HC  group by ReimbursFromMasterId) tblamt on tblamt.ReimbursFromMasterId=R.ReimbursFromMasterId 
                INNER JOIN (SELECT ReimbursFromMasterId,MAX(Version)MaxVer FROM dbo.tblReimbursementSelfAppLog  GROUP BY ReimbursFromMasterId) AS CELog ON CELog.ReimbursFromMasterId= R.ReimbursFromMasterId
                WHERE R.ReimbursFromMasterId IS NOT NULL  AND lg.ActionStatus ='Verified'  AND  Version=CELog.MaxVer    " + param + "  order by H.TopsheetGeneMasId DESC ";
                }

                 


                //AND lg.ForEmpInfoId= " + HttpContext.Current.Session["EmpInfoId"].ToString() + "
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable Get_TopSheet()
        {
            try
            {
                string query = @"SELECT EN.UserName EntryBy, Un.UserName UpdateBy, * FROM TopSheetGenerateMaster_H M 
LEFT JOIN tblUser EN ON M.EntryBy = EN.UserId
LEFT JOIN tblUser Un ON M.UpdateBy = Un.UserId";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool ExpenseReimbursementFormAppoval(string FromMasterId, string status)
        {
            bool result = false;

            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@ReimbursementMasterId", FromMasterId));
                aParameters.Add(new SqlParameter("@status", status));
                aParameters.Add(new SqlParameter("@UpdateBy", HttpContext.Current.Session["UserId"].ToString()));
                result = accessManager.UpdateData("sp_Approved_ExpenseReimbursmentFrom", aParameters);
            }
            catch (Exception e)
            {
                result = false;
                accessManager.SqlConnectionClose(true);
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }


            return result;
        }
        public bool Returnfrom_MettingList(string FromMasterId, string CmntMeetingReturn, string TopsheetGeneMasId)
        {
            bool result = false;

            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@ReimbursementMasterId", FromMasterId));
                aParameters.Add(new SqlParameter("@CmntMeetingReturn", CmntMeetingReturn));
                aParameters.Add(new SqlParameter("@TopsheetGeneMasId", TopsheetGeneMasId));
                aParameters.Add(new SqlParameter("@CmntMeetingReturnBy", HttpContext.Current.Session["UserId"].ToString()));
                result = accessManager.UpdateData("sp_ReturnMettingListReimbursmentFrom", aParameters);
            }
            catch (Exception e)
            {
                result = false;
                accessManager.SqlConnectionClose(true);
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }


            return result;
        }



        public bool Returnfrom_FROmHR(string FromMasterId)
        {
            bool result = false;

            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@ReimbursementMasterId", FromMasterId));
                aParameters.Add(new SqlParameter("@CmntMeetingReturnBy", ""));
                result = accessManager.UpdateData("sp_ReturnMettingListReimbursmentFrom", aParameters);
            }
            catch (Exception e)
            {
                result = false;
                accessManager.SqlConnectionClose(true);
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }


            return result;
        }


        public bool Returnfrom_MettingStatusUpdate(  string TopsheetGeneMasId)
        {
            bool result = false;

            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aParameters = new List<SqlParameter>();

            
                aParameters.Add(new SqlParameter("@TopsheetGeneMasId", TopsheetGeneMasId));
                aParameters.Add(new SqlParameter("@UpdateBy", HttpContext.Current.Session["UserId"].ToString()));
                result = accessManager.UpdateData("sp_MettingStatusUpdateFrom", aParameters);
            }
            catch (Exception e)
            {
                result = false;
                accessManager.SqlConnectionClose(true);
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }


            return result;
        }

        public int SaveEmpAppLog(ReimbursementSelfAppLogDAO appLogDao)
        {
            try
            {
                int pk = 0;
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@ReimbursFromMasterId", appLogDao.ReimbursFromMasterId));
                    aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                    aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                    aParameters.Add(new SqlParameter("@Version", appLogDao.Version));
                    aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                    aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                    aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                    aParameters.Add(new SqlParameter("@Comments", (object)appLogDao.Comments ?? DBNull.Value));
                    aParameters.Add(new SqlParameter("@CommentsEMPID", (object)appLogDao.CommentsEMP ?? DBNull.Value));

                    string query = @"INSERT INTO dbo.tblReimbursementSelfAppLog
                                    (
                                    ReimbursFromMasterId,
                                    PreEmpInfoId,
                                    ForEmpInfoId,
                                    Version,
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments,CommentsEMPID
                                    )
                                    VALUES(
                                    @ReimbursFromMasterId,
                                    @PreEmpInfoId,
                                    @ForEmpInfoId,
                                    (SELECT (COUNT(*)+1) FROM dbo.tblReimbursementSelfAppLog WHERE ReimbursFromMasterId=@ReimbursFromMasterId),
                                    @ApproveBy,
                                    @ApproveDate,
                                    @ActionStatus,@Comments,@CommentsEMPID
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

        public bool UpdateAppLog(string status, string id)
        {

            try
            {
                int pk = 0;

                //if (id.JdMasterId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@AppraisalSelfAppLogId", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", status));

                    string query =
                        @"update tblReimbursementSelfAppLog set ActionStatus=@ActionStatus  where ReimbursementSelfAppLogId = @AppraisalSelfAppLogId";

                    bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    return result;

                }

            }
            catch (Exception exception)
            {

                throw exception;
            }
        }

        public DataTable Get_CommitteeFeedBack(string Id,string param, string comId)
        {
            try
            {
                string query = @"

WITH RankedFeedback AS (
    SELECT
        ReimbursFromMasterId,
        Feedback,
        ROW_NUMBER() OVER (PARTITION BY ReimbursFromMasterId ORDER BY FeedbackDate DESC) AS RowNum
    FROM
        tblCommitteeFeedback_HC
)
 
 Select H.ReimbursFromMasterId ,H.ReimbursFromMasterId, EMP.EmpName, COM.ShortName,Fy.FinancialYearId ,H.EmpInfoId ,FY.FinancialYearDesc, Dpt.DepartmentName,H.ActionStatus, * from tbl_ReimbursmentFormMaster_HealthCare H 
LEFT JOIN tblEmpGeneralInfo EMP ON H.EmpInfoId= Emp.EmpInfoId
LEFT JOIN tblCompanyInfo COM ON COM.CompanyId = H.CompanyId
LEFT JOIN tblFinancialYear FY ON FY.FinancialYearId = H.FinancialYearId	
LEFT JOIN tblDesignation DG ON DG.DesignationId = EMP.DesignationId
LEFT JOIN tblDepartment Dpt ON Dpt.DepartmentId = Emp.DepartmentId
LEFT JOIN tblSalaryLocation SL ON EMP.SalaryLoationId= SL.SalaryLoationId
LEFT JOIN (
    SELECT
        ReimbursFromMasterId,
        Feedback
    FROM
        RankedFeedback
    WHERE
        RowNum = 1
) AS Comm ON Comm.ReimbursFromMasterId = H.ReimbursFromMasterId
INNER JOIN (SELECT ReimbursFromMasterId,MAX(Version)MaxVer FROM dbo.tblReimbursementSelfAppLog  GROUP BY ReimbursFromMasterId) AS CELog ON CELog.ReimbursFromMasterId= H.ReimbursFromMasterId
INNER JOIN dbo.tblReimbursementSelfAppLog ON tblReimbursementSelfAppLog.ReimbursFromMasterId = H.ReimbursFromMasterId    
WHERE H.ReimbursFromMasterId IS NOT NULL AND Version=CELog.MaxVer  AND H.CompanyId in (" + comId + @") AND    Type in (Select    ApplicationType  from tblCommiteeSetupMaster M 
left join tblCommiteeSetupDetails D ON M.ComSetupMasId= D.ComSetupMasId
Where IsDoctor=1 And  CompanyId in(" + comId + @") and EmpInfoId =" + Id + @") AND SL.SalaryLoationId  in (Select    SalaryLoationId  from tblCommiteeSetupMaster M 
left join tblCommiteeSetupDetails D ON M.ComSetupMasId= D.ComSetupMasId
Where IsDoctor=1 And  CompanyId in (" + comId + @") And EmpInfoId =" + Id + ") " + param + " order by H.EntryDate desc ";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public DataTable Get_CommitteeFeedBackElPayroll(string Id,string param, string comId)
        {
            try
            {
                string query = @"

WITH RankedFeedback AS (
    SELECT
        ReimbursFromMasterId,
        Feedback,
        ROW_NUMBER() OVER (PARTITION BY ReimbursFromMasterId ORDER BY FeedbackDate DESC) AS RowNum
    FROM
        tblCommitteeFeedback_HC
)
 
 Select H.ReimbursFromMasterId ,H.ReimbursFromMasterId, EMP.EmpName, COM.ShortName,Fy.FinancialYearId ,H.EmpInfoId ,FY.FinancialYearDesc, Dpt.DepartmentName,H.ActionStatus, * from tbl_ReimbursmentFormMaster_HealthCare H 
LEFT JOIN tblEmpGeneralInfo EMP ON H.EmpInfoId= Emp.EmpInfoId
LEFT JOIN tblCompanyInfo COM ON COM.CompanyId = H.CompanyId
LEFT JOIN tblFinancialYear FY ON FY.FinancialYearId = H.FinancialYearId	
LEFT JOIN tblDesignation DG ON DG.DesignationId = EMP.DesignationId
LEFT JOIN tblDepartment Dpt ON Dpt.DepartmentId = Emp.DepartmentId
LEFT JOIN tblSalaryLocation SL ON EMP.SalaryLoationId= SL.SalaryLoationId
LEFT JOIN (
    SELECT
        ReimbursFromMasterId,
        Feedback
    FROM
        RankedFeedback
    WHERE
        RowNum = 1
) AS Comm ON Comm.ReimbursFromMasterId = H.ReimbursFromMasterId
INNER JOIN (SELECT ReimbursFromMasterId,MAX(Version)MaxVer FROM dbo.tblReimbursementSelfAppLog  GROUP BY ReimbursFromMasterId) AS CELog ON CELog.ReimbursFromMasterId= H.ReimbursFromMasterId
INNER JOIN dbo.tblReimbursementSelfAppLog ON tblReimbursementSelfAppLog.ReimbursFromMasterId = H.ReimbursFromMasterId    
WHERE H.ReimbursFromMasterId IS NOT NULL AND Version=CELog.MaxVer and EMP.HealthcareCompanyId is not null  AND EMP.HealthcareCompanyId  in (" + comId + @") AND    Type in (Select    ApplicationType  from tblCommiteeSetupMaster M 
left join tblCommiteeSetupDetails D ON M.ComSetupMasId= D.ComSetupMasId
Where IsDoctor=1 And  CompanyId in(" + comId + @") and EmpInfoId =" + Id + @") AND SL.SalaryLoationId  in (Select    SalaryLoationId  from tblCommiteeSetupMaster M 
left join tblCommiteeSetupDetails D ON M.ComSetupMasId= D.ComSetupMasId
Where IsDoctor=1 And  CompanyId in (" + comId + @") And EmpInfoId =" + Id + ") " + param + " order by H.EntryDate desc ";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

          public DataTable Get_CommitteeFeedBackForId(string Id,string param, string comId)
        {
            try
            {
                string query = @"

WITH RankedFeedback AS (
    SELECT
        ReimbursFromMasterId,
        Feedback,
        ROW_NUMBER() OVER (PARTITION BY ReimbursFromMasterId ORDER BY FeedbackDate DESC) AS RowNum
    FROM
        tblCommitteeFeedback_HC
)
 
 Select H.ReimbursFromMasterId ,H.ReimbursFromMasterId, EMP.EmpName, COM.ShortName,Fy.FinancialYearId ,H.EmpInfoId ,FY.FinancialYearDesc, Dpt.DepartmentName,H.ActionStatus, * from tbl_ReimbursmentFormMaster_HealthCare H 
LEFT JOIN tblEmpGeneralInfo EMP ON H.EmpInfoId= Emp.EmpInfoId
LEFT JOIN tblCompanyInfo COM ON COM.CompanyId = H.CompanyId
LEFT JOIN tblFinancialYear FY ON FY.FinancialYearId = H.FinancialYearId	
LEFT JOIN tblDesignation DG ON DG.DesignationId = EMP.DesignationId
LEFT JOIN tblDepartment Dpt ON Dpt.DepartmentId = Emp.DepartmentId
LEFT JOIN tblSalaryLocation SL ON EMP.SalaryLoationId= SL.SalaryLoationId
LEFT JOIN (
    SELECT
        ReimbursFromMasterId,
        Feedback
    FROM
        RankedFeedback
    WHERE
        RowNum = 1
) AS Comm ON Comm.ReimbursFromMasterId = H.ReimbursFromMasterId
INNER JOIN (SELECT ReimbursFromMasterId,MAX(Version)MaxVer FROM dbo.tblReimbursementSelfAppLog  GROUP BY ReimbursFromMasterId) AS CELog ON CELog.ReimbursFromMasterId= H.ReimbursFromMasterId
INNER JOIN dbo.tblReimbursementSelfAppLog ON tblReimbursementSelfAppLog.ReimbursFromMasterId = H.ReimbursFromMasterId    
WHERE H.ReimbursFromMasterId IS NOT NULL AND Version=CELog.MaxVer  AND H.CompanyId in ("+ comId + @") AND   EMP.HealthcareCompanyId=1 and      H.CompanyId =" + comId + @"  AND H.ActionStatus ='Verified'  AND comm.Feedback IS NULL  AND H.IsForwardtoDoctor =1 order by H.EntryDate desc  ";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public DataTable Get_ApplicationWaitingList(string param)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@param", param));
                dt = accessManager.GetDataTable("sp_GET_ApplicationWaitingList", aList);
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

            return dt;
        }


        public DataTable Get_CommitteeCheck(string ApplicationType, string SalaryLoationId)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();
                aList.Add(new SqlParameter("@ApplicationType", ApplicationType));
                aList.Add(new SqlParameter("@SalaryLoationId", SalaryLoationId));
                aList.Add(new SqlParameter("@EmpId", HttpContext.Current.Session["EmpInfoId"].ToString()));
                dt = accessManager.GetDataTable("sp_GET_CommitteeFeedbackCheck", aList);
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

            return dt;
        }



        public Int32 SaveCommitteeFeedbackInfo(string feedback, string empId, string reimbursmentmasterId, bool DoctorStatus)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@reimbursmentmasterId", reimbursmentmasterId));
            aSqlParameterlist.Add(new SqlParameter("@empId", empId));
            aSqlParameterlist.Add(new SqlParameter("@feedback", feedback));
            aSqlParameterlist.Add(new SqlParameter("@DoctorStatus", DoctorStatus));

            const string insertQuery = @" 


UPDATE tbl_ReimbursmentFormMaster_HealthCare SET  ShowStatus='Committee Verified',   DoctorStatus=@DoctorStatus,DoctorStatusComm=@feedback  Where ReimbursFromMasterId = @reimbursmentmasterId


           INSERT INTO [dbo].[tblCommitteeFeedback_HC]
           ([EmpInfoId]
           ,[ReimbursFromMasterId]
           ,[Feedback]
           ,[FeedbackDate],FeedbackStatus)
             VALUES
           (@empId,@reimbursmentmasterId,@feedback,GETDATE(),@DoctorStatus)";
            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");

        }


        public DataTable Get_Latest_CommitteeFeedBack(string Id)
        {
            try
            {
                string query = @"Select TOP 1 * from tblCommitteeFeedback_HC Where ReimbursFromMasterId= " + Id + "  Order By ComfeedbackId DESC ";
                return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool Update_Reimbursment_Log(string EmpInfoId, string ReimbursFromMasterId, bool IsDepart)
        {
            bool result = false;

            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aParameters = new List<SqlParameter>();

                aParameters.Add(new SqlParameter("@EmpInfoId", EmpInfoId));
                aParameters.Add(new SqlParameter("@ReimbursFromMasterId", ReimbursFromMasterId));
                aParameters.Add(new SqlParameter("@IsDepartmental", IsDepart));

                result = accessManager.UpdateData("sp_Update_ReimbursmentLog", aParameters);
            }
            catch (Exception e)
            {
                result = false;
                accessManager.SqlConnectionClose(true);
                throw;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }


            return result;
        }
    }
}
