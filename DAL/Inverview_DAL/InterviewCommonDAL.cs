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

namespace DAL.Inverview_DAL
{
    public class InterviewCommonDAL
    {
        private ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        public DataTable GetIVCandidateForInvitation(string cid, string JobID)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@cid", cid));
            aSqlParameterlist.Add(new SqlParameter("@JobID", JobID));

            const string queryStr = @"SELECT ISNULL(ici.CandidateInvitationId,0) CandidateInvitationId,ic.CandidateID,ic.CompanyId,ic.JobID,ic.CandidateName,ic.Address,ic.PhoneNo,ic.EmailAdress,ic.TotalYearsOfExp,
ic.LastOrganization,ic.LastPosition,j.Position ,ici.Remarks,
ISNULL(ici.EmailInvitationSent,0) EmailInvitationSent,ISNULL(ici.SMSInvitationSent,0) SMSInvitationSent,
ISNULL(ici.PhoneInvitationSent,0) PhoneInvitationSent
FROM dbo.tblInterviewCandidateInfo ic
INNER JOIN dbo.tblJobCreation j ON j.JobID = ic.JobID AND ic.CompanyId=j.CompanyId
LEFT JOIN dbo.tblInterviewCandidateInvitation ici ON ici.CandidateID=ic.CandidateID
WHERE ic.CompanyId=@cid AND ic.JobID=@JobID";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetVivaInformation(string cid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@cid", cid));


            const string queryStr = @"SELECT *,'0'VivaMarks,CONVERT(BIT,'False')IsData FROM dbo.tblVivaSetupInfo WHERE IsActive =1 AND (IsDelete IS NULL
                                      OR IsDelete = 0)
 and CompanyId=@cid";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetVivaInformationForEdit(string mId,string cid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@mid", mId));
            aSqlParameterlist.Add(new SqlParameter("@cid", cid));


            const string queryStr = @"SELECT * FROM (SELECT tblVivaSetupInfo.VivaId,
       CompanyId,
       Category,
       VivaName,
       Remarks,
       IsActive,
       
       
       VivaMarks,CONVERT(BIT, (CASE WHEN tblInterviewBoardMarksSetup.VivaId IS NULL THEN 'False' ELSE 'True' END))IsData FROM dbo.tblVivaSetupInfo
            LEFT JOIN dbo.tblInterviewBoardMarksSetup ON tblInterviewBoardMarksSetup.VivaId = tblVivaSetupInfo.VivaId
            WHERE     IsActive =1 AND (IsDelete IS NULL OR IsDelete = 0) AND SetupMasterId=@mid
UNION ALL 
SELECT VivaId,
       CompanyId,
       Category,
       VivaName,
       Remarks,
       IsActive,
       '0'VivaMarks,CONVERT(BIT,'False')IsData FROM dbo.tblVivaSetupInfo WHERE   IsActive =1 AND (IsDelete IS NULL OR IsDelete = 0) AND  VivaId NOT IN (SELECT VivaId FROM dbo.tblInterviewBoardMarksSetup WHERE SetupMasterId=@mid) and CompanyId=@cid)AS tblt

";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetVivaInformationForEdit2(string mId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@mid", mId));


            const string queryStr = @"SELECT * FROM dbo.tblInterviewBoardMarksSetup WHERE SetupMasterId=@mid";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetIVCandidateForMarksEntry(string cid, string JobID, string InterviewPhase)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@cid", cid));
            aSqlParameterlist.Add(new SqlParameter("@JobID", JobID));
            aSqlParameterlist.Add(new SqlParameter("@InterviewPhase", InterviewPhase));

            const string queryStr = @"SELECT tm.InterviewCandidateTotalMarksId,
       tm.CandidateID,
       tm.InterviewPhase,
       tm.JobID,
       ISNULL(tm.WrittenMarks,0) AS WrittenMarks,
       ISNULL(tm.WrittenMarksOutOf,0)AS WrittenMarksOutOf,
       ISNULL(tm.VivaMarks,0) AS VivaMarks,
       ISNULL(tm.VivaMarksOutOf,0)AS VivaMarksOutOf,
       ISNULL(tm.OtherMarks,0)AS OtherMarks,
       ISNULL(tm.OtherMarksOutOf,0) AS OtherMarksOutOf,
	   ci.CandidateName,
	   ci.Address,
	   ci.PhoneNo
	    FROM dbo.tblInterviewCandidateTotalMarks tm
		INNER JOIN dbo.tblInterviewCandidateInfo ci ON ci.CandidateID = tm.CandidateID
		WHERE tm.JobID=@JobID AND tm.InterviewPhase=@InterviewPhase

		UNION
		SELECT 0 AS InterviewCandidateTotalMarksId,
       ca.CandidateID,
       ca.InterviewPhase,
       ci.JobID,
       0 as WrittenMarks,
       0 as WrittenMarksOutOf,
       0 as VivaMarks,
       0 as VivaMarksOutOf,
       0 as OtherMarks,
       0 as OtherMarksOutOf,
	   ci.CandidateName,
	   ci.Address,
	   ci.PhoneNo	   
	    FROM dbo.tblInterviewCandidateInfo ci
		INNER JOIN dbo.tblInterviewCandidateAttandance ca ON ca.CandidateID = ci.CandidateID
		WHERE ci.JobID=@JobID AND ca.InterviewPhase=@InterviewPhase AND ci.CandidateID NOT IN (SELECT m.CandidateID FROM  dbo.tblInterviewCandidateTotalMarks m WHERE m.JobID=@JobID AND m.InterviewPhase=@InterviewPhase)";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetIVCandidateForAttandance(string cid, string JobID)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@cid", cid));
            aSqlParameterlist.Add(new SqlParameter("@JobID", JobID));

            const string queryStr = @"SELECT ISNULL(ici.InterviewCandidateAttandanceId,0) InterviewCandidateAttandanceId,ic.CandidateID,ic.CompanyId,ic.JobID,ic.CandidateName,ic.Address,ic.PhoneNo,ic.EmailAdress,ic.TotalYearsOfExp,
ic.LastOrganization,ic.LastPosition,j.Position ,ici.Remarks,
ici.InterviewDate,ici.ReportingTime
FROM dbo.tblInterviewCandidateInfo ic
INNER JOIN dbo.tblInterviewCandidateInvitation ci ON ci.CandidateID = ic.CandidateID
INNER JOIN dbo.tblJobCreation j ON j.JobID = ic.JobID AND ic.CompanyId=j.CompanyId
LEFT JOIN dbo.tblInterviewCandidateAttandance ici ON ici.CandidateID=ic.CandidateID
WHERE ic.CompanyId=@cid AND ic.JobID=@JobID";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetIVCandidateForAttandanceByParam(string Param)
        {
            string queryStr = @"SELECT ISNULL(ici.InterviewCandidateAttandanceId,0) InterviewCandidateAttandanceId,ic.CandidateID,ic.CompanyId,ic.JobID,ic.CandidateName,ic.Address,ic.PhoneNo,ic.EmailAdress,ic.TotalYearsOfExp,
ic.LastOrganization,ic.LastPosition,j.Position ,ici.Remarks,
ici.InterviewDate,ici.ReportingTime
FROM dbo.tblInterviewCandidateInfo ic
left JOIN dbo.tblInterviewCandidateInvitation ci ON ci.CandidateID = ic.CandidateID
left JOIN dbo.tblJobCreation j ON j.JobID = ic.JobID AND ic.CompanyId=j.CompanyId
LEFT JOIN dbo.tblInterviewCandidateAttandance ici ON ici.CandidateID=ic.CandidateID
 
WHERE " + Param;
            return _aCommonInternalDal.DataContainerDataTable(queryStr, null, DataBase.HRDB);
        }

        public DataTable GetIVBoardMemberForMarksEntry(int cid, int JobID,int InterviewActivity, int InterviewPhase)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@cid", cid));
            aSqlParameterlist.Add(new SqlParameter("@JobID", JobID));
            aSqlParameterlist.Add(new SqlParameter("@InterviewActivity", InterviewActivity));
            aSqlParameterlist.Add(new SqlParameter("@InterviewPhase", InterviewPhase));

            const string queryStr = @"SELECT ISNULL(ibd.BoardDetailsId,0) as BoardDetailsId,ISNULL(ibd.EmployeeId,0) AS EmployeeId,ibm.JobTitleId,
                                        ibd.Name,ibd.Designation,ibd.Department,ibd.Company,ibm.CompanyId 
                                        ,ibd.Written,ibd.Viva,ibd.Other,
										
CASE
    WHEN ibd.MemberType =1 THEN 'SMC'
    WHEN ibd.MemberType =2  THEN 'SMC EL'
    WHEN ibd.MemberType =3  THEN 'Other'
  END AS MemberType
                                        FROM dbo.tblInterviewBoardSetupDetails ibd
                                        INNER JOIN dbo.tblInterviewBoardSetupMaster ibm ON ibm.SetupMasterId=ibd.MasterId
                                        WHERE (ibd.Name!=' ') and  ibm.CompanyId=@cid AND ibm.JobTitleId=@JobID ";
             //AND ibm.InterviewPhase=@InterviewPhase
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetIVVivaMarks(string cid, string JobID)
        {
            //var aSqlParameterlist = new List<SqlParameter>();
            //aSqlParameterlist.Add(new SqlParameter("@cid", cid));
            //aSqlParameterlist.Add(new SqlParameter("@JobID", JobID));
            //aSqlParameterlist.Add(new SqlParameter("@InterviewActivity", InterviewActivity));
            //aSqlParameterlist.Add(new SqlParameter("@InterviewPhase", InterviewPhase));

            string queryStr = @"SELECT 0 as VivaDetailsMarkId, IBMS.VivaMarks VoutOff, V.VivaName  AS VivaInfo, 0 AS  MainMarks, * FROM dbo.tblInterviewCandidateInfo IVC
            LEFT JOIN dbo.tblInterviewBoardSetupMaster IBM ON IVC.JobID=IBM.JobTitleId
            INNER JOIN dbo.tblInterviewBoardMarksSetup IBMS ON IBMS.SetupMasterId = IBM.SetupMasterId
            LEFT JOIN dbo.tblVivaSetupInfo V ON V.VivaId=IBMS.VivaId
        
            LEFT JOIN dbo.tblInterviewCandidateAttandance IVCA ON IVCA.JobID=IVC.JobID AND IVCA.CandidateID = IVC.CandidateID
INNER JOIN dbo.tblInterviewCandidateAttandance ca ON ca.CandidateID = IVC.CandidateID
            WHERE IBM.JobTitleId='" + JobID + "' AND IBM.CompanyId='" + cid + "'   ORDER BY IVC.CandidateID ASC";
            //AND ibm.InterviewPhase=@InterviewPhase
            return _aCommonInternalDal.DataContainerDataTable(queryStr,  DataBase.HRDB);
        }

        public DataTable GetIVVivaMarksForUpdate(string cid, string pkd, string JobID)
        {
            //var aSqlParameterlist = new List<SqlParameter>();
            //aSqlParameterlist.Add(new SqlParameter("@cid", cid));
            //aSqlParameterlist.Add(new SqlParameter("@JobID", JobID));
            //aSqlParameterlist.Add(new SqlParameter("@InterviewActivity", InterviewActivity));
            //aSqlParameterlist.Add(new SqlParameter("@InterviewPhase", InterviewPhase));

            string queryStr = @"SELECT tblVivaDetailsMark.VivaDetailsMarkId, tblVivaDetailsMark.VivaOutOf VoutOff,V.VivaName AS VivaInfo, tblVivaDetailsMark.VivaMarks AS  MainMarks,* FROM tblVivaDetailsMark 
LEFT JOIN dbo.tblInterviewCandidateInfo ivc ON ivc.CandidateID = tblVivaDetailsMark.CandidateID
LEFT JOIN dbo.tblVivaSetupInfo V ON V.VivaId=tblVivaDetailsMark.VivaId

			WHERE tblVivaDetailsMark.JobId='" + JobID + "' and tblVivaDetailsMark.BoardDetailsId='" + pkd + "' AND v.CompanyId='" + cid + "'   ORDER BY IVC.CandidateID ASC";
            //AND ibm.InterviewPhase=@InterviewPhase
            return _aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
        }
        public DataTable GetIVCandidateMarks(string BoardDetailsId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@BoardDetailsId", BoardDetailsId));
            return _aCommonInternalDal.GetDataByStoreProcedure("usp_GetIVCandidateMarks", aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetIVCandidateMarks_WVO(string BoardDetailsId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@BoardDetailsId", BoardDetailsId));
            return _aCommonInternalDal.GetDataByStoreProcedure("usp_GetIVCandidateMarks_WVO", aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable VIVAaaaaaaaa_WVO(string BoardDetailsId, string JobId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@BoardDetailsId", BoardDetailsId));
            aSqlParameterlist.Add(new SqlParameter("@JobId", JobId));
            return _aCommonInternalDal.GetDataByStoreProcedure("usp_VIVAaaaaaaaa_WVO", aSqlParameterlist, DataBase.HRDB);
        }

        public List<InterviewCandidateInfoDao> LoadInterviewCandidateInfoList()
        {
            List<InterviewCandidateInfoDao> lcinfo = new List<InterviewCandidateInfoDao>();

            using (DataTable dt = _aCommonInternalDal.GetDataByStoreProcedure("usp_GetInterviewCandidateInfoList", null, DataBase.HRDB))
            {
                if (dt.Rows.Count>0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        InterviewCandidateInfoDao item = new InterviewCandidateInfoDao();
                        item.CandidateID = int.Parse(dt.Rows[i]["CandidateID"].ToString());
                        item.CompanyId = int.Parse(dt.Rows[i]["CompanyId"].ToString());
                        item.CompanyName = dt.Rows[i]["CompanyName"].ToString();

                        item.JobID = int.Parse(dt.Rows[i]["JobID"].ToString());
                        item.JobCode = dt.Rows[i]["JobCode"].ToString();
                        item.Position = dt.Rows[i]["Position"].ToString();

                        item.CandidateName = dt.Rows[i]["CandidateName"].ToString();
                        item.Address = dt.Rows[i]["Address"].ToString();
                        item.PhoneNo = dt.Rows[i]["PhoneNo"].ToString();
                        item.EmailAdress = dt.Rows[i]["EmailAdress"].ToString();
                        item.TotalYearsOfExp = dt.Rows[i]["TotalYearsOfExp"].ToString();
                        item.LastOrganization = dt.Rows[i]["LastOrganization"].ToString();
                        item.LastPosition = dt.Rows[i]["LastPosition"].ToString();
                        item.LastExam = dt.Rows[i]["LastExam"].ToString();
                        item.LastMajor = dt.Rows[i]["LastMajor"].ToString();
                        item.LastPassingYear = dt.Rows[i]["LastPassingYear"].ToString();
                        item.LastResultType = dt.Rows[i]["LastResultType"].ToString();
                        item.LastResultDivision = dt.Rows[i]["LastResultDivision"].ToString();
                        item.LastResultCGPA = dt.Rows[i]["LastResultCGPA"].ToString();
                        item.LastResultOutOf = dt.Rows[i]["LastResultOutOf"].ToString();
                        item.CvUploadID = int.Parse(dt.Rows[i]["CvUploadID"].ToString());
                        item.PhotoUploadID = int.Parse(dt.Rows[i]["PhotoUploadID"].ToString());
                        item.CurrentSalary = dt.Rows[i]["CurrentSalary"].ToString();
                        item.ExpectedSalary = dt.Rows[i]["ExpectedSalary"].ToString();
                        item.Remarks = dt.Rows[i]["Remarks"].ToString();
                        item.ApprovalStatus = dt.Rows[i]["ApprovalStatus"].ToString();
                        item.InternalNote = dt.Rows[i]["InternalNote"].ToString();


                        lcinfo.Add(item);
                    }
                    
                }
            }
            return lcinfo;
        }


        public DataTable GetIVBoardSetupDtlByMId(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SetupMasterId", mid));

            const string queryStr = @" SELECT ivm.EmailBody, ivm.CompanyId,  ivm.JobTitleId, ivd.BoardDetailsId,ivd.EmployeeId as EmpInfoId, ivd.MemberType,ivd.Written,ivd.Viva,ivd.Other,ivd.OtherRemarks,'' AS InterviewActivity , 
EmpMasterCode =CASE
 WHEN ivd.MemberType=3 THEN ''  ELSE emp.EmpMasterCode END ,
EmpName =CASE WHEN ivd.MemberType=3 THEN ivd.Name  ELSE emp.EmpName END,
Designation =CASE WHEN ivd.MemberType=3 THEN ivd.Designation  ELSE emp.Designation END,
DepartmentName =CASE WHEN ivd.MemberType=3 THEN ivd.Department  ELSE emp.DepartmentName END,
CompanyName =CASE WHEN ivd.MemberType=3 THEN ivd.Company  ELSE emp.CompanyName END,
ivd.Email OfficialEmail,ivd.Phone OfficialMobile FROM dbo.tblInterviewBoardSetupDetails ivd 
INNER JOIN dbo.tblInterviewBoardSetupMaster ivm ON ivm.SetupMasterId=ivd.MasterId
LEFT JOIN dbo.vw_EmpInfo emp ON emp.EmpInfoId=ivd.EmployeeId
WHERE ivm.SetupMasterId=@SetupMasterId AND ivd.IsActive=1";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }



        public DataTable GetIVBoardSetupSendMailByMId(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SetupMasterId", mid));

            const string queryStr = @" SELECT ivm.EmailBody, ivm.CompanyId,  ivm.JobTitleId, ivd.BoardDetailsId,ivd.EmployeeId as EmpInfoId, ivd.MemberType,ivd.Written,ivd.Viva,ivd.Other,ivd.OtherRemarks,'' AS InterviewActivity , 
EmpMasterCode =CASE
 WHEN ivd.MemberType=3 THEN ''  ELSE emp.EmpMasterCode END ,
EmpName =CASE WHEN ivd.MemberType=3 THEN ivd.Name  ELSE emp.EmpName END,
Designation =CASE WHEN ivd.MemberType=3 THEN ivd.Designation  ELSE emp.Designation END,
DepartmentName =CASE WHEN ivd.MemberType=3 THEN ivd.Department  ELSE emp.DepartmentName END,
CompanyName =CASE WHEN ivd.MemberType=3 THEN ivd.Company  ELSE emp.CompanyName END,
ivd.Email OfficialEmail,ivd.Phone OfficialMobile FROM dbo.tblInterviewBoardSetupDetails ivd 
INNER JOIN dbo.tblInterviewBoardSetupMaster ivm ON ivm.SetupMasterId=ivd.MasterId
LEFT JOIN dbo.vw_EmpInfo emp ON emp.EmpInfoId=ivd.EmployeeId
WHERE (ivd.Name!=' ') and ivm.SetupMasterId=@SetupMasterId AND ivd.IsActive=1";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable GetIVBoardSetupSendMailByMIdByJobID(int mid)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SetupMasterId", mid));

            const string queryStr = @" SELECT ivm.EmailBody, ivm.CompanyId,  ivm.JobTitleId, ivd.BoardDetailsId,ivd.EmployeeId as EmpInfoId, ivd.MemberType,ivd.Written,ivd.Viva,ivd.Other,ivd.OtherRemarks,'' AS InterviewActivity , 
EmpMasterCode =CASE
 WHEN ivd.MemberType=3 THEN ''  ELSE emp.EmpMasterCode END ,
EmpName =CASE WHEN ivd.MemberType=3 THEN ivd.Name  ELSE emp.EmpName END,
Designation =CASE WHEN ivd.MemberType=3 THEN ivd.Designation  ELSE emp.Designation END,
DepartmentName =CASE WHEN ivd.MemberType=3 THEN ivd.Department  ELSE emp.DepartmentName END,
CompanyName =CASE WHEN ivd.MemberType=3 THEN ivd.Company  ELSE emp.CompanyName END,
ivd.Email OfficialEmail,ivd.Phone OfficialMobile FROM dbo.tblInterviewBoardSetupDetails ivd 
INNER JOIN dbo.tblInterviewBoardSetupMaster ivm ON ivm.SetupMasterId=ivd.MasterId
LEFT JOIN dbo.vw_EmpInfo emp ON emp.EmpInfoId=ivd.EmployeeId
WHERE (ivd.Name!=' ') and ivm.JobTitleId=@SetupMasterId AND ivd.IsActive=1";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable GetInterviewPhase(int cid, int interviewActivity, long jobId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", cid));
            aSqlParameterlist.Add(new SqlParameter("@InterviewActivity", interviewActivity));
            aSqlParameterlist.Add(new SqlParameter("@JobTitleId", jobId));

            const string queryStr = @"SELECT ISNULL(MAX(ivm.InterviewPhase),0)+1 AS InterviewPhase FROM dbo.tblInterviewBoardSetupMaster ivm 
WHERE ivm.CompanyId=@CompanyId AND ivm.JobTitleId=@JobTitleId AND ivm.InterviewActivity=@InterviewActivity";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable GetAllInterviewPhase(int cid, int interviewActivity, long jobId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", cid));
            aSqlParameterlist.Add(new SqlParameter("@InterviewActivity", interviewActivity));
            aSqlParameterlist.Add(new SqlParameter("@JobTitleId", jobId));

            const string queryStr = @"SELECT ivm.InterviewPhase AS Value,CAST(ivm.InterviewPhase AS NVARCHAR(50)) AS TextField FROM dbo.tblInterviewBoardSetupMaster ivm 
WHERE ivm.CompanyId=@CompanyId AND ivm.JobTitleId=@JobTitleId AND ivm.InterviewActivity=@InterviewActivity";
            return _aCommonInternalDal.GetDTforDDL(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable CopyIVFromPreviousPhase(int cid, long JobId, int InterviewActivity,int InterviewPhase)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", cid));
            aSqlParameterlist.Add(new SqlParameter("@JobTitleId", JobId));
            aSqlParameterlist.Add(new SqlParameter("@InterviewActivity", InterviewActivity));
            aSqlParameterlist.Add(new SqlParameter("@InterviewPhase", InterviewPhase));

            const string queryStr = @"SELECT TOP 1 ivm.SetupMasterId,
             ivm.CompanyId,
             ivm.JobTitleId,
             ivm.InterviewActivity,
             ivm.InterviewPhase,
             ivm.InterviewNoId,
             ivm.Vanue,
             ivm.InterviewDate,
             ivm.InterviewTime,
			 j.JobCode,j.Position FROM dbo.tblInterviewBoardSetupMaster ivm
 INNER JOIN dbo.tblJobCreation j ON j.JobID=ivm.JobTitleId
WHERE ivm.IsActive=1 and ivm.CompanyId=@CompanyId AND ivm.JobTitleId=@JobTitleId AND ivm.InterviewActivity=@InterviewActivity AND ivm.InterviewPhase=@InterviewPhase";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public List<InterviewCandidateViewModel> LoadInterviewCandidateGrading(string CompanyId, string JobID)
        {
            List<InterviewCandidateViewModel> lCandidate = new List<InterviewCandidateViewModel>();
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@JobID", JobID));

            const string queryStr = @"SELECT * FROM (SELECT 
		g.InterviewCandidateGradingId,
       m.CandidateID,
       SUM(ISNULL(m.WrittenMarks,0)) AS WrittenMarks,
       SUM(ISNULL(m.WrittenMarksOutOf,0)) AS WrittenMarksOutOf,
       SUM(ISNULL(m.VivaMarks,0)) AS VivaMarks,
       SUM(ISNULL(m.VivaMarksOutOf,0)) AS VivaMarksOutOf,
       SUM(ISNULL(m.OtherMarks,0)) AS OtherMarks,
       SUM(ISNULL(m.OtherMarksOutOf,0)) AS OtherMarksOutOf,
	   c.CandidateName,j.JobCode,j.Position ,j.JobID,
	   SUM(ISNULL(m.Attitude,0))+SUM(ISNULL(m.Language,0))+SUM(ISNULL(m.TechnicalSkill,0))+SUM(ISNULL(m.IQ,0))+SUM(ISNULL(m.GeneralKnowledge,0))+SUM(ISNULL(m.Others,0))+SUM(ISNULL(m.TimeSence,0)) AS InterviewMarks,
	   (SUM(ISNULL(m.WrittenMarks,0))+SUM(ISNULL(m.VivaMarks,0))+SUM(ISNULL(m.OtherMarks,0))+SUM(ISNULL(m.Attitude,0))+SUM(ISNULL(m.Language,0))+SUM(ISNULL(m.TechnicalSkill,0))+SUM(ISNULL(m.IQ,0))+SUM(ISNULL(m.GeneralKnowledge,0))+SUM(ISNULL(m.Others,0))+SUM(ISNULL(m.TimeSence,0))) AS TotalMarks,
	   --dbo.fn_GetGradeByMarks(SUM(ISNULL(m.WrittenMarks,0))+SUM(ISNULL(m.VivaMarks,0))+SUM(ISNULL(m.OtherMarks,0))+SUM(ISNULL(m.Attitude,0))+SUM(ISNULL(m.Language,0))+SUM(ISNULL(m.TechnicalSkill,0))+SUM(ISNULL(m.IQ,0))+SUM(ISNULL(m.GeneralKnowledge,0))+SUM(ISNULL(m.Others,0))+SUM(ISNULL(m.TimeSence,0))) AS LetterGrade,
RANK() OVER (ORDER BY SUM(ISNULL(m.WrittenMarks,0))+SUM(ISNULL(m.VivaMarks,0))+SUM(ISNULL(m.OtherMarks,0))+SUM(ISNULL(m.Attitude,0))+SUM(ISNULL(m.Language,0))+SUM(ISNULL(m.TechnicalSkill,0))+SUM(ISNULL(m.IQ,0))+SUM(ISNULL(m.GeneralKnowledge,0))+SUM(ISNULL(m.Others,0))+SUM(ISNULL(m.TimeSence,0)) DESC) AS LetterGrade,
        g.Remarks
	   FROM dbo.tblInterviewMarksDetails m
	   INNER JOIN dbo.tblInterviewCandidateGrading g ON g.CandidateID=m.CandidateID
INNER JOIN dbo.tblInterviewCandidateInfo c ON c.CandidateID = m.CandidateID
INNER JOIN dbo.tblInterviewBoardSetupDetails bd ON bd.BoardDetailsId = m.BoardDetailsId
INNER JOIN dbo.tblJobCreation j ON j.JobID = c.JobID
LEFT JOIN dbo.tblInterviewActivity a ON a.InterviewActivityId = m.InterviewActivity
WHERE j.JobID=@JobID
GROUP BY g.InterviewCandidateGradingId, m.CandidateID,c.CandidateName,j.JobCode,j.Position,j.JobID,g.Remarks
UNION

SELECT 
		0 InterviewCandidateGradingId,
       m.CandidateID,
       SUM(ISNULL(m.WrittenMarks,0)) AS WrittenMarks,
       SUM(ISNULL(m.WrittenMarksOutOf,0)) AS WrittenMarksOutOf,
       SUM(ISNULL(m.VivaMarks,0)) AS VivaMarks,
       SUM(ISNULL(m.VivaMarksOutOf,0)) AS VivaMarksOutOf,
       SUM(ISNULL(m.OtherMarks,0)) AS OtherMarks,
       SUM(ISNULL(m.OtherMarksOutOf,0)) AS OtherMarksOutOf,
	   c.CandidateName,j.JobCode,j.Position ,j.JobID,
	   SUM(ISNULL(m.Attitude,0))+SUM(ISNULL(m.Language,0))+SUM(ISNULL(m.TechnicalSkill,0))+SUM(ISNULL(m.IQ,0))+SUM(ISNULL(m.GeneralKnowledge,0))+SUM(ISNULL(m.Others,0))+SUM(ISNULL(m.TimeSence,0)) AS InterviewMarks,
	   (SUM(ISNULL(m.WrittenMarks,0))+SUM(ISNULL(m.VivaMarks,0))+SUM(ISNULL(m.OtherMarks,0))+SUM(ISNULL(m.Attitude,0))+SUM(ISNULL(m.Language,0))+SUM(ISNULL(m.TechnicalSkill,0))+SUM(ISNULL(m.IQ,0))+SUM(ISNULL(m.GeneralKnowledge,0))+SUM(ISNULL(m.Others,0))+SUM(ISNULL(m.TimeSence,0))) AS TotalMarks,
	   --dbo.fn_GetGradeByMarks(SUM(ISNULL(m.WrittenMarks,0))+SUM(ISNULL(m.VivaMarks,0))+SUM(ISNULL(m.OtherMarks,0))+SUM(ISNULL(m.Attitude,0))+SUM(ISNULL(m.Language,0))+SUM(ISNULL(m.TechnicalSkill,0))+SUM(ISNULL(m.IQ,0))+SUM(ISNULL(m.GeneralKnowledge,0))+SUM(ISNULL(m.Others,0))+SUM(ISNULL(m.TimeSence,0))) AS LetterGrade,
       RANK() OVER (ORDER BY SUM(ISNULL(m.WrittenMarks,0))+SUM(ISNULL(m.VivaMarks,0))+SUM(ISNULL(m.OtherMarks,0))+SUM(ISNULL(m.Attitude,0))+SUM(ISNULL(m.Language,0))+SUM(ISNULL(m.TechnicalSkill,0))+SUM(ISNULL(m.IQ,0))+SUM(ISNULL(m.GeneralKnowledge,0))+SUM(ISNULL(m.Others,0))+SUM(ISNULL(m.TimeSence,0)) DESC) AS LetterGrade,
	   '' Remarks
	   FROM dbo.tblInterviewMarksDetails m
INNER JOIN dbo.tblInterviewCandidateInfo c ON c.CandidateID = m.CandidateID
INNER JOIN dbo.tblInterviewBoardSetupDetails bd ON bd.BoardDetailsId = m.BoardDetailsId
INNER JOIN dbo.tblJobCreation j ON j.JobID = c.JobID
LEFT JOIN dbo.tblInterviewActivity a ON a.InterviewActivityId = m.InterviewActivity
WHERE j.JobID=@JobID AND m.CandidateID NOT IN (SELECT CandidateID FROM dbo.tblInterviewCandidateGrading WHERE JobID=@JobID)
GROUP BY m.CandidateID,c.CandidateName,j.JobCode,j.Position,j.JobID)F ORDER BY F.TotalMarks DESC";
            //return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);

            using (DataTable dt = _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB))
            {
                if (dt.Rows.Count>0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        InterviewCandidateViewModel candidate = new InterviewCandidateViewModel();
                        //candidate.InterviewMarksDetailsId = dt.Rows[i]["InterviewMarksDetailsId"]
                        candidate.InterviewCandidateGradingId = int.Parse(dt.Rows[i]["InterviewCandidateGradingId"].ToString());
                        candidate.CandidateID = int.Parse(dt.Rows[i]["CandidateID"].ToString());
                        candidate.JobID = int.Parse(dt.Rows[i]["JobID"].ToString());
                        //candidate.Attitude = decimal.Parse(dt.Rows[i]["Attitude"].ToString());
                        //candidate.Language = decimal.Parse(dt.Rows[i]["Language"].ToString());
                        //candidate.TechnicalSkill = decimal.Parse(dt.Rows[i]["TechnicalSkill"].ToString());
                        //candidate.IQ = decimal.Parse(dt.Rows[i]["IQ"].ToString());
                        //candidate.GeneralKnowledge = decimal.Parse(dt.Rows[i]["GeneralKnowledge"].ToString());
                        //candidate.Others = decimal.Parse(dt.Rows[i]["Others"].ToString());
                        //candidate.TimeSence = decimal.Parse(dt.Rows[i]["TimeSence"].ToString());
                        candidate.TotalMarks = decimal.Parse(dt.Rows[i]["TotalMarks"].ToString());
                        candidate.WrittenMarks = decimal.Parse(dt.Rows[i]["WrittenMarks"].ToString());
                        candidate.WrittenMarksOutOf = decimal.Parse(dt.Rows[i]["WrittenMarksOutOf"].ToString());
                        candidate.VivaMarks = decimal.Parse(dt.Rows[i]["VivaMarks"].ToString());
                        candidate.VivaMarksOutOf = decimal.Parse(dt.Rows[i]["VivaMarksOutOf"].ToString());
                        candidate.OtherMarks = decimal.Parse(dt.Rows[i]["OtherMarks"].ToString());
                        candidate.OtherMarksOutOf = decimal.Parse(dt.Rows[i]["OtherMarksOutOf"].ToString());
                        candidate.InterviewMarks = decimal.Parse(dt.Rows[i]["InterviewMarks"].ToString());

                        candidate.LetterGrade = dt.Rows[i]["LetterGrade"].ToString();
                        candidate.CandidateName = dt.Rows[i]["CandidateName"].ToString();
                        candidate.JobCode = dt.Rows[i]["JobCode"].ToString();
                        candidate.Position = dt.Rows[i]["Position"].ToString();
                        candidate.Remarks = dt.Rows[i]["Remarks"].ToString();

                        lCandidate.Add(candidate);
                    }
                }
            }

            return lCandidate;
        }

        public DataTable GetCandidateByPhone(string CandidatePhone)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CandidatePhone", CandidatePhone));

            const string queryStr = @"SELECT top 1 ci.CandidateID, ci.CompanyId, ci.JobID, ci.CandidateName, ci.Address, ci.PhoneNo, ci.EmailAdress, ci.TotalYearsOfExp, ci.LastOrganization, ci.LastPosition, ci.LastExam, ci.LastMajor, ci.LastPassingYear, ci.LastResultType, ci.LastResultDivision, ci.LastResultCGPA, ci.LastResultOutOf, ci.CvUploadID, ci.PhotoUploadID, ci.CurrentSalary, ci.ExpectedSalary, ci.Remarks, ci.ApprovalStatus, ci.EntryBy, ci.EntryDate, ci.Updateby, ci.UpdateDate, ci.VerifiedBy, ci.VerifiedDate, ci.ApproveBy, ci.ApproveDate, ci.InternalNote, ci.IsActive FROM dbo.tblInterviewCandidateInfo ci WHERE ci.PhoneNo=@CandidatePhone";
            return _aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public bool SaveBoardMemberVivaSetup(List<InterviewBoardMarksSetupDAO> aBoardMarksSetupDaos, int id)
        {
            try
            {

                string delQ = @"delete from tblInterviewBoardMarksSetup where SetupMasterId = " + id + "";
                bool dd = _aCommonInternalDal.DeleteDataByDeleteCommand(delQ, DataBase.HRDB);

                bool result = false;

                foreach (var item in aBoardMarksSetupDaos)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@SetupMasterId", item.SetupMasterId));
                    aParameters.Add(new SqlParameter("@IsWritten", item.IsWritten ));
                    aParameters.Add(new SqlParameter("@WrittenMarks", item.WrittenMarks ?? (object)DBNull.Value));
                    aParameters.Add(new SqlParameter("@IsOther", item.IsOther));
                    aParameters.Add(new SqlParameter("@OtherMarks", item.OtherMarks ?? (object)DBNull.Value));
                    aParameters.Add(new SqlParameter("@IsViva", item.IsViva));
                    aParameters.Add(new SqlParameter("@VivaId", item.VivaId ?? (object)DBNull.Value));
                    aParameters.Add(new SqlParameter("@VivaMarks", item.VivaMarks ?? (object)DBNull.Value));

                    string query = @"INSERT INTO dbo.tblInterviewBoardMarksSetup
                                    (
                                        SetupMasterId,
                                        IsWritten,
                                        WrittenMarks,
                                        IsOther,
                                        OtherMarks,
                                        IsViva,
                                        VivaId,
                                        VivaMarks
                                    )
                                    VALUES
                                    (   @SetupMasterId,
                                        @IsWritten,
                                        @WrittenMarks,
                                        @IsOther,
                                        @OtherMarks,
                                        @IsViva,
                                        @VivaId,
                                        @VivaMarks
                                    )";

                    result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);
                    if (result == false)
                    {
                        return false;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
