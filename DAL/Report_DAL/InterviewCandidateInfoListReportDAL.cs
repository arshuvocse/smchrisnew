using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.InternalCls;

namespace DAL.Inverview_DAL
{
    public class InterviewCandidateInfoListReportDAL
    {
       ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
       public DataTable GetInterviewCandidateInfoList( string param )
       {
           string query = @"SELECT com.ShortName,* FROM dbo.tblInterviewCandidateInfo 
INNER JOIN dbo.tblCompanyInfo com ON tblInterviewCandidateInfo.CompanyId= com.CompanyId " + param + "";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }

       public DataTable GetInterviewCandidateMarksList(string param)
       {
           string query = @"SELECT c.CandidateName,ISNULL(tblViva.VivaMarks,0) VivaMarks ,ISNULL(tblWritten.WrittenMarks,0) WrittenMarks,ISNULL(tblOtherMarks.OtherMarks,0) OtherMarks, ISNULL(tblViva.VivaMarks,0)+ ISNULL(tblWritten.WrittenMarks,0)+ISNULL(tblOtherMarks.OtherMarks,0) TotalMark FROM dbo.tblInterviewCandidateInfo c 
LEFT JOIN (SELECT CandidateID, JobId, SUM(VivaMarks)VivaMarks FROM dbo.tblVivaDetailsMark GROUP BY CandidateID, JobId) tblViva ON tblViva.CandidateID = c.CandidateID AND tblViva.JobId = c.JobID

LEFT JOIN (SELECT CandidateID, JobId, SUM(WrittenMarks)WrittenMarks FROM dbo.tblInterviewMarksDetails WHERE Tag='Written' GROUP BY CandidateID, JobId) tblWritten ON tblViva.CandidateID = c.CandidateID AND tblViva.JobId = c.JobID

LEFT JOIN (SELECT CandidateID, JobId, SUM(OtherMarks)OtherMarks FROM dbo.tblInterviewMarksDetails WHERE Tag='OtherMarks' GROUP BY CandidateID, JobId) tblOtherMarks ON tblViva.CandidateID = c.CandidateID AND tblViva.JobId = c.JobID " + param + "";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
    }
}
