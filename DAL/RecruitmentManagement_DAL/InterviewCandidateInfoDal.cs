using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.RecruitmentManagement_DAL
{
    public class InterviewCandidateInfoDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public void GetComapnyNameList(DropDownList ddl)
        {
            const string queryStr = "SELECT CompanyId, CompanyCode FROM tblCompanyInfo";
            aCommonInternalDal.LoadDropDownValue(ddl, "CompanyCode", "CompanyId", queryStr, "HRDB");
        }

        public void GetJobList(DropDownList ddl)
        {
            const string queryStr = "SELECT JobId,Position FROM tblJobCreation WHERE Progress = 'Posted'";
            aCommonInternalDal.LoadDropDownValue(ddl, "Position", "JobId", queryStr, "HRDB");
        }

        public void GetDegreeList(DropDownList ddl)
        {
            const string queryStr = "SELECT * FROM tblEmployeeDegreeInfo ORDER BY DegreeTitle ASC";
            aCommonInternalDal.LoadDropDownValue(ddl, "DegreeTitle", "DegreeId", queryStr, "HRDB");
        }

        public void GetAreaOfStudy(DropDownList ddl)
        {
            const string queryStr = "SELECT * FROM tblMejor ORDER BY Mejor ASC";
            aCommonInternalDal.LoadDropDownValue(ddl, "Mejor", "MejorId", queryStr, "HRDB");
        }

        public int SaveInterViewCandidateInfo(InterviewCandidateInfoDao aCandidateInfoDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aCandidateInfoDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@JobID", aCandidateInfoDao.JobID));
            aSqlParameterlist.Add(new SqlParameter("@CandidateName", aCandidateInfoDao.CandidateName));
            aSqlParameterlist.Add(new SqlParameter("@Address", aCandidateInfoDao.Address));
            aSqlParameterlist.Add(new SqlParameter("@PhoneNo", aCandidateInfoDao.PhoneNo));
            aSqlParameterlist.Add(new SqlParameter("@EmailAdress", aCandidateInfoDao.EmailAdress));
            aSqlParameterlist.Add(new SqlParameter("@TotalYearsOfExp", aCandidateInfoDao.TotalYearsOfExp));
           // aSqlParameterlist.Add(new SqlParameter("@LastPosition", aCandidateInfoDao.LastPosition));
            aSqlParameterlist.Add(new SqlParameter("@ExamID", aCandidateInfoDao.ExamID));
            aSqlParameterlist.Add(new SqlParameter("@MejorID", aCandidateInfoDao.MejorID));
            aSqlParameterlist.Add(new SqlParameter("@ResultID", aCandidateInfoDao.ResultID));
            aSqlParameterlist.Add(new SqlParameter("@PassingYearID", aCandidateInfoDao.PassingYearID));
            aSqlParameterlist.Add(new SqlParameter("@Point", aCandidateInfoDao.Point));
            aSqlParameterlist.Add(new SqlParameter("@OutOf", aCandidateInfoDao.OutOf));
            aSqlParameterlist.Add(new SqlParameter("@ExpectedSalary", aCandidateInfoDao.ExpectedSalary));
            aSqlParameterlist.Add(new SqlParameter("@CurrentSalary", aCandidateInfoDao.CurrentSalary));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aCandidateInfoDao.Remarks));

            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aCandidateInfoDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aCandidateInfoDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aCandidateInfoDao.EntryDate));

            string insertQuery = @"INSERT INTO tblInterviewCandidateInfo (CompanyId,JobID,CandidateName,Address,PhoneNo,EmailAdress,
                                  TotalYearsOfExp,ExamID,MejorID,ResultID,PassingYearID,Point,OutOf,ExpectedSalary,CurrentSalary,Remarks,ApprovalStatus,
                                  EntryBy,EntryDate) VALUES (@CompanyId,@JobId,@CandidateName,@Address,@PhoneNo,@EmailAdress,@TotalYearsOfExp,
                                 @ExamID,@MejorID,@ResultID,@PassingYearID,@Point,@OutOf,@ExpectedSalary,@CurrentSalary,@ResultID,@ApprovalStatus,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public DataTable GetInterViewCandidateList()
        {
            string query = @"SELECT * FROM tblInterviewCandidateInfo";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetCandidateInfoById(int candidateId)
        {
            string query = @"SELECT * FROM tblInterviewCandidateInfo WHERE CandidateID = '" + candidateId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public bool UpdateInterViewCandidateInfo(InterviewCandidateInfoDao aCandidateInfoDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aCandidateInfoDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@JobID", aCandidateInfoDao.JobID));
            aSqlParameterlist.Add(new SqlParameter("@CandidateName", aCandidateInfoDao.CandidateName));
            aSqlParameterlist.Add(new SqlParameter("@Address", aCandidateInfoDao.Address));
            aSqlParameterlist.Add(new SqlParameter("@PhoneNo", aCandidateInfoDao.PhoneNo));
            aSqlParameterlist.Add(new SqlParameter("@EmailAdress", aCandidateInfoDao.EmailAdress));
            aSqlParameterlist.Add(new SqlParameter("@TotalYearsOfExp", aCandidateInfoDao.TotalYearsOfExp));
            aSqlParameterlist.Add(new SqlParameter("@ExamID", aCandidateInfoDao.ExamID));
            aSqlParameterlist.Add(new SqlParameter("@MejorID", aCandidateInfoDao.MejorID));
            aSqlParameterlist.Add(new SqlParameter("@ResultID", aCandidateInfoDao.ResultID));
            aSqlParameterlist.Add(new SqlParameter("@PassingYearID", aCandidateInfoDao.PassingYearID));
            aSqlParameterlist.Add(new SqlParameter("@Point", aCandidateInfoDao.Point));
            aSqlParameterlist.Add(new SqlParameter("@OutOf", aCandidateInfoDao.OutOf));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aCandidateInfoDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@ExpectedSalary", aCandidateInfoDao.ExpectedSalary));
            aSqlParameterlist.Add(new SqlParameter("@CurrentSalary", aCandidateInfoDao.CurrentSalary));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aCandidateInfoDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aCandidateInfoDao.EntryDate));

            string query = @"UPDATE tblInterviewCandidateInfo SET CompanyId=@CompanyId,JobID=@JobID,CandidateName=@CandidateName,Address=@Address,PhoneNo=@PhoneNo,
                            EmailAdress=@EmailAdress,TotalYearsOfExp=@TotalYearsOfExp,ExamID=@ExamID,MejorID=@MejorID,ResultID=@ResultID,
                            PassingYearID=@PassingYearID,Point=@Point,OutOf=@OutOf,ExpectedSalary=@ExpectedSalary,CurrentSalary=@CurrentSalary,
                            Remarks=@Remarks,Updateby=@UpdateBy,UpdateDate=@UpdateDate WHERE CandidateID = '" + aCandidateInfoDao.CandidateID + "'";

            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }
    }
}
