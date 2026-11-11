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

namespace DAL.MasterSetup_DAL
{
    public class InterviewBoardSetupDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public void GetComapnyNameList(DropDownList ddl)
        {
            const string queryStr = "SELECT CompanyId, CompanyName FROM tblCompanyInfo";
            aCommonInternalDal.LoadDropDownValue(ddl, "CompanyName", "CompanyId", queryStr, "HRDB");
        }

        public void GetDepartmentList(DropDownList ddl)
        {
            string queryStr = "SELECT DepartmentId, DepartmentName FROM tblDepartment";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", queryStr, "HRDB");
        }

        public void GetSectionList(DropDownList ddl)
        {
            string queryStr = "SELECT SectionID,SectionName FROM tblSection";
            aCommonInternalDal.LoadDropDownValue(ddl, "SectionName", "SectionID", queryStr, "HRDB");
        }

        public Int32 SaveInterviewBoardInfo(InterviewBoardSetupMasterDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@JobTitleId", aInformationDao.JobTitleId));
            aSqlParameterlist.Add(new SqlParameter("@InterviewNoId", aInformationDao.InterviewNoId));
            aSqlParameterlist.Add(new SqlParameter("@Vanue", aInformationDao.Vanue));

            aSqlParameterlist.Add(new SqlParameter("@IsEmployee", aInformationDao.IsEmployee));
            aSqlParameterlist.Add(new SqlParameter("@IsGuest", aInformationDao.IsGuest));

            aSqlParameterlist.Add(new SqlParameter("@InterviewTime", aInformationDao.InterviewTime));
            aSqlParameterlist.Add(new SqlParameter("@InterviewDate", aInformationDao.InterviewDate));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            const string insertQuery = @"INSERT INTO tblInterviewBoardSetupMaster (CompanyId,JobTitleId,InterviewNoId,Vanue,IsEmployee,IsGuest,InterviewDate,InterviewTime,EntryBy,EntryDate,ApprovalStatus)
                                   VALUES (@CompanyId,@JobTitleId,@InterviewNoId,@Vanue,@IsEmployee,@IsGuest,@InterviewDate,@InterviewTime,@EntryBy,@EntryDate,@ApprovalStatus)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");

        }

        public Int64 SaveEducationalReqDetail(JobCreationEdReqDao jobCreationEdReqDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@JobID", jobCreationEdReqDao.JobID));
            aSqlParameterlist.Add(new SqlParameter("@ERID", jobCreationEdReqDao.ERID));

            string insertQuery = @"INSERT INTO tblJobCreationEdReq (JobID,ERID) VALUES (@JobID,@ERID)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");

        }

        public Int64 SaveJobLocationDetail(JobCreationLocationDao jobCreationLocationDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@JobID", jobCreationLocationDao.JobID));
            aSqlParameterlist.Add(new SqlParameter("@JobLocationID", jobCreationLocationDao.JobLocationID));

            string insertQuery = @"INSERT INTO tblJobCreationLocation (JobID,JobLocationID) VALUES (@JobID,@JobLocationID)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public DataTable GetJobCreationInfos()
        {
            string query = @"SELECT JC.JobID,JC.JobCode, Ci.CompanyName, JC.Position, JC.Vacancy, JL.Location, JC.CirculationStartDate,JC.ApprovalStatus,JC.Progress,
                                   JC.EntryBy, JC.EntryDate, JC.Updateby,JC.UpdateDate  FROM tblJobCreation AS JC 
                                   INNER JOIN tblCompanyInfo AS CI ON JC.CompanyId = CI.CompanyId
                                   INNER JOIN tblJobCreationLocation AS JCL ON JC.JobID = JCL.JobID
                                   INNER JOIN tblJobLocation AS JL ON JCL.JobLocationID = JL.JobLocationID";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetJobCreationInfoByJobId(long jobId)
        {
            string query = @"SELECT * FROM tblJobCreation AS JC
                            INNER JOIN tblCompanyInfo AS CI ON JC.CompanyId = CI.CompanyId
                            INNER JOIN tblDepartment AS DPT ON DPT.DepartmentID = JC.DepartmentID
                            INNER JOIN tblSection AS SEC ON JC.SectionID = SEC.SectionID WHERE JobID = '" + jobId + "'";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetJobCreationEduReqByJobId(long jobId)
        {
            string query = @"SELECT * FROM tblJobCreationEdReq AS EDQ
                            INNER JOIN tblEducationRequirements AS EDQR ON EDQR.ERID = EDQ.ERID WHERE EDQ.JobID ='" + jobId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetJobCreationLocationByJobId(long jobId)
        {
            string query = @"SELECT * FROM tblJobCreationLocation AS JCL
                            INNER JOIN tblJobLocation AS JL ON JL.JobLocationID = JCL.JobLocationID WHERE JCL.JobID = '" + jobId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public bool UpdateJobCreationInformation(JobCreationDao aJobCreationDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@JobCode", aJobCreationDao.JobCode));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aJobCreationDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@DepartmentID", aJobCreationDao.DepartmentID));
            aSqlParameterlist.Add(new SqlParameter("@SectionID", aJobCreationDao.SectionID));
            aSqlParameterlist.Add(new SqlParameter("@Position", aJobCreationDao.Position));
            aSqlParameterlist.Add(new SqlParameter("@Vacancy", aJobCreationDao.Vacancy));
            aSqlParameterlist.Add(new SqlParameter("@JobContext", aJobCreationDao.JobContext));
            aSqlParameterlist.Add(new SqlParameter("@JobResponsibilities", aJobCreationDao.JobResponsibilities));
            aSqlParameterlist.Add(new SqlParameter("@PermanenteEmp", aJobCreationDao.PermanenteEmp));
            aSqlParameterlist.Add(new SqlParameter("@ContractualEmp", aJobCreationDao.ContractualEmp));
            aSqlParameterlist.Add(new SqlParameter("@TraineeEmp", aJobCreationDao.TraineeEmp));
            aSqlParameterlist.Add(new SqlParameter("@CasualEmp", aJobCreationDao.CasualEmp));
            aSqlParameterlist.Add(new SqlParameter("@ExperienceRequirements", aJobCreationDao.ExperienceRequirements));
            aSqlParameterlist.Add(new SqlParameter("@AdditionalRequirements", aJobCreationDao.AdditionalRequirements));
            aSqlParameterlist.Add(new SqlParameter("@Salary", aJobCreationDao.Salary));
            aSqlParameterlist.Add(new SqlParameter("@CompensationandOtherBenefits", aJobCreationDao.CompensationandOtherBenefits));
            aSqlParameterlist.Add(new SqlParameter("@NewspaperDS", aJobCreationDao.NewspaperDS));
            aSqlParameterlist.Add(new SqlParameter("@TVDS", aJobCreationDao.TVDS));
            aSqlParameterlist.Add(new SqlParameter("@OtherDS", aJobCreationDao.OtherDS));
            aSqlParameterlist.Add(new SqlParameter("@Other", aJobCreationDao.Other));

            aSqlParameterlist.Add(new SqlParameter("@CirculationStartDate", aJobCreationDao.CirculationStartDate));
            aSqlParameterlist.Add(new SqlParameter("@CirculationsdeadlineDate", aJobCreationDao.CirculationsdeadlineDate));
            aSqlParameterlist.Add(new SqlParameter("@ProbableIRecruitmentDate", aJobCreationDao.ProbableIRecruitmentDate));
            aSqlParameterlist.Add(new SqlParameter("@ProbableInterviewDate", aJobCreationDao.ProbableInterviewDate));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aJobCreationDao.Remarks));

            aSqlParameterlist.Add(new SqlParameter("@Status", aJobCreationDao.Status));
            aSqlParameterlist.Add(new SqlParameter("@Progress", aJobCreationDao.Progress));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aJobCreationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aJobCreationDao.EntryDate));



            string query = @"UPDATE dbo.tblJobCreation SET CompanyId=@CompanyId,DepartmentID=@DepartmentID,SectionID=@SectionID,Position=@Position,Vacancy=@Vacancy,JobContext=@JobContext,
                           JobResponsibilities=@JobResponsibilities,PermanenteEmp=@PermanenteEmp,
                           ContractualEmp=@ContractualEmp,TraineeEmp=@TraineeEmp,CasualEmp=@CasualEmp,ExperienceRequirements=@ExperienceRequirements,AdditionalRequirements=@AdditionalRequirements,
                           Salary=@Salary,CompensationandOtherBenefits=@CompensationandOtherBenefits,NewspaperDS=@NewspaperDS,
                           TVDS=@TVDS,OtherDS=@OtherDS,Other=@Other,CirculationStartDate=@CirculationStartDate,CirculationsdeadlineDate=@CirculationsdeadlineDate,
                           ProbableInterviewDate=@ProbableIRecruitmentDate,ProbableIRecruitmentDate=@ProbableInterviewDate,Remarks=@Remarks,Updateby=@UpdateBy,
                           UpdateDate=@UpdateDate WHERE JobID = '" + aJobCreationDao.JobID + "'";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }
        public bool DeleteJobCreationEduReqDetailInformationByJobId(long jobId)
        {
            string query = @"DELETE  FROM tblJobCreationEdReq WHERE JobID = '" + jobId + "'";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
        }

        public bool DeleteJobCreationLocationDetailInformationByJobId(long jobId)
        {
            string query = @"DELETE  FROM tblJobLocation WHERE JobID = '" + jobId + "'";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
        }

        public Int32 SaveDegreeInformation(string degree)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@Degree", degree));

            string insertQuery = @"INSERT INTO tblEducationRequirements (EducationRequirements) VALUES (@Degree)";
            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public void GetJobList(DropDownList ddl, string companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            const string queryStr = "SELECT JobID,Position FROM tblJobCreation WHERE CompanyId = @CompanyId";
            aCommonInternalDal.LoadDropDownValue(ddl, "Position", "JobID", queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable LoadEmployeeInformation(string text)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SearchText", text));

            const string queryStr = @"SELECT EMP.EmpInfoId,EMP.EmpName,DSG.Designation,DPT.DepartmentName,CI.CompanyName FROM tblEmpGeneralInfo AS EMP 
                                     INNER JOIN tblCompanyInfo AS CI ON EMP.CompanyInfoId = CI.CompanyId
                                     INNER JOIN tblDesignation AS DSG ON EMP.DesignationId = DSG.DesignationId
                                     INNER JOIN tblDepartment AS DPT ON EMP.DepartmentId = DPT.DepartmentId
                                     WHERE EMP.EmpName = @SearchText";
            return aCommonInternalDal.DataContainerDataTable(queryStr,aSqlParameterlist, "HRDB");
        }

        public int SaveInterviewBoardEmployeeInfo(IEnumerable<InterviewBoardEmpDetailsDao> aInformationDaos)
        {

            Int32 id = 0;

            foreach (var aInformationDao in aInformationDaos)
            {
                var aSqlParameterlist = new List<SqlParameter>();

                aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aInformationDao.EmployeeId));
                aSqlParameterlist.Add(new SqlParameter("@MasterId", aInformationDao.MasterId));

                const string insertQuery = @"INSERT INTO tblInterviewBoardEmpDetails (MasterId,EmployeeId) VALUES (@MasterId,@EmployeeId)";
                id = aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
            }

            return id;
        }

        public int SaveInterviewBoardGuestInfo(IEnumerable<InterviewBoardGuestDetailsDao> aInformationDaos)
        {
            Int32 id = 0;

            foreach (var aInformationDao in aInformationDaos)
            {
                var aSqlParameterlist = new List<SqlParameter>();

                aSqlParameterlist.Add(new SqlParameter("@MasterId", aInformationDao.MasterId));
                aSqlParameterlist.Add(new SqlParameter("@GuestName", aInformationDao.GuestName));
                aSqlParameterlist.Add(new SqlParameter("@Company", aInformationDao.Company));
                aSqlParameterlist.Add(new SqlParameter("@Designation", aInformationDao.Designation));
                aSqlParameterlist.Add(new SqlParameter("@Department", aInformationDao.Department));

                const string insertQuery = @"INSERT INTO tblInterviewBoardGuestDetails (MasterId,GuestName,Company,Designation,Department) VALUES (@MasterId,@GuestName,@Company,@Designation,@Department)";
                id = aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
            }

            return id;
        }

        public DataTable GetInterViewBoardList()
        {
            const string queryStr = @"SELECT IVB.SetupMasterId, CI.CompanyName, JC.Position, IVB .Vanue, IVB.InterviewDate, IVB.InterviewTime, IVB.EntryBy, IVB.EntryDate, IVB.UpdateBy, IVB.UpdateDate FROM tblInterviewBoardSetupMaster AS IVB 
                            INNER JOIN tblCompanyInfo AS CI ON IVB.CompanyId = CI.CompanyId
                            INNER JOIN tblJobCreation AS JC ON IVB.JobTitleId = JC.JobID";

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetBoardInformationById(string boardId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@BoardId", boardId));

            const string queryStr = @"SELECT * FROM tblInterviewBoardSetupMaster WHERE SetupMasterId = @BoardId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist,"HRDB");
        }

        public DataTable LoadEmpDetailInfo(string boardId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@boardId", boardId));

            const string queryStr = @"SELECT EMP.EmpInfoId,EMP.EmpName,DSG.Designation,DPT.DepartmentName,CI.CompanyName FROM tblInterviewBoardEmpDetails AS EM
									 INNER JOIN tblEmpGeneralInfo AS EMP ON EM.EmployeeId = EMP.EmpInfoId
                                     INNER JOIN tblCompanyInfo AS CI ON EMP.CompanyInfoId = CI.CompanyId
                                     INNER JOIN tblDesignation AS DSG ON EMP.DesignationId = DSG.DesignationId
                                     INNER JOIN tblDepartment AS DPT ON EMP.DepartmentId = DPT.DepartmentId
                                     WHERE EM.MasterId = @boardId ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable LoadGuestDetailInfo(string boardId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@boardId", boardId));

            const string queryStr = @"SELECT * FROM tblInterviewBoardGuestDetails AS GST
                                     WHERE GST.MasterId = @boardId ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateInterviewBoardInfo(InterviewBoardSetupMasterDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SetupMasterId", aInformationDao.SetupMasterId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@JobTitleId", aInformationDao.JobTitleId));
            aSqlParameterlist.Add(new SqlParameter("@InterviewNoId", aInformationDao.InterviewNoId));
            aSqlParameterlist.Add(new SqlParameter("@Vanue", aInformationDao.Vanue));

            aSqlParameterlist.Add(new SqlParameter("@IsEmployee", aInformationDao.IsEmployee));
            aSqlParameterlist.Add(new SqlParameter("@IsGuest", aInformationDao.IsGuest));

            aSqlParameterlist.Add(new SqlParameter("@InterviewTime", aInformationDao.InterviewTime));
            aSqlParameterlist.Add(new SqlParameter("@InterviewDate", aInformationDao.InterviewDate));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));

            const string queryStr = @"UPDATE tblInterviewBoardSetupMaster SET CompanyId = @CompanyId,JobTitleId = @JobTitleId,InterviewNoId = @InterviewNoId,Vanue = @Vanue,IsEmployee = @IsEmployee,
                                    IsGuest = @IsGuest,InterviewDate = @InterviewDate,InterviewTime = @InterviewTime,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate WHERE SetupMasterId = @SetupMasterId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteInterviewBoardEmployeeInfo(int boardId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@boardId", boardId));

            string query = @"DELETE  FROM tblInterviewBoardEmpDetails WHERE MasterId = @boardId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query,aSqlParameterlist ,"HRDB");
        }

        public bool DeleteInterviewBoardGuestInfo(int boardId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@boardId", boardId));

            const string query = @"DELETE  FROM tblInterviewBoardGuestDetails WHERE MasterId = @boardId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query,aSqlParameterlist, "HRDB");
        }
    }
}
