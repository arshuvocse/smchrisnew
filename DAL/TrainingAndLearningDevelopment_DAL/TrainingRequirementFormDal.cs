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

namespace DAL.TrainingAndLearningDevelopment_DAL
{
    public class TrainingRequirementFormDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
         public void GetCompanyListIntoDropdown(DropDownList ddl)
        {
            const string queryStr = "SELECT CompanyId,CompanyName FROM tblCompanyInfo";
            aCommonInternalDal.LoadDropDownValue(ddl, "CompanyName", "CompanyId", queryStr, "HRDB");
        }

        public void GetDivisionListIntoDropdown(DropDownList ddl)
        {
            const string queryStr = @"SELECT DivisionId,DivisionName FROM tblDivision WHERE IsActive = 'True'";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionName", "DivisionId", queryStr, "HRDB");
        }

        public Int32 SaveFormInfo(TrainingRequirementFormDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyInfoId", aInformationDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aInformationDao.DivisionId));
            aSqlParameterlist.Add(new SqlParameter("@DivisionWId", aInformationDao.DivisionWId));
            aSqlParameterlist.Add(new SqlParameter("@DepartmentId", aInformationDao.DepartmentId));
            aSqlParameterlist.Add(new SqlParameter("@SectionId", aInformationDao.SectionId));
            aSqlParameterlist.Add(new SqlParameter("@SubSectionId", aInformationDao.SubSectionId));
            aSqlParameterlist.Add(new SqlParameter("@FinYearId", aInformationDao.FinYearId));
            aSqlParameterlist.Add(new SqlParameter("@TrainingTopicId", aInformationDao.TrainingTopicId));

            aSqlParameterlist.Add(new SqlParameter("@Q1", aInformationDao.Q1));
            aSqlParameterlist.Add(new SqlParameter("@Q2", aInformationDao.Q2));
            aSqlParameterlist.Add(new SqlParameter("@Q3", aInformationDao.Q3));
            aSqlParameterlist.Add(new SqlParameter("@Q4", aInformationDao.Q4));

            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));

            aSqlParameterlist.Add(new SqlParameter("@TrainingExp", aInformationDao.TrainingExp));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));

            const string insertQuery = @"insert into tblTrainingRequirementForm (CompanyId,DivisionId,DivisionWId,DepartmentId,SectionId,SubSectionId,FinYearId,TrainingTopicId,Q1,Q2,Q3,Q4,TrainingExp,ApprovalStatus,EntryBy,EntryDate,IsActive,Remarks) 
            values (@CompanyInfoId,@DivisionId,@DivisionWId,@DepartmentId,@SectionId,@SubSectionId,@FinYearId,@TrainingTopicId,@Q1,@Q2,@Q3,@Q4,@TrainingExp,@ApprovalStatus,@EntryBy,@EntryDate,@IsActive,@Remarks)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public void GetDivisionList(DropDownList ddl, string companyId)
        {
            string queryStr = "SELECT DivisionId,DivisionName FROM tblDivision WHERE IsActive = 'True' AND CompanyId = '" + companyId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionName", "DivisionId", queryStr, "HRDB");
        }

        public void GetDivisionWingList(DropDownList ddl, string divisionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DivisionId", divisionId));

            const string queryStr = "SELECT DivisionWId,DivisionWingName FROM tblDivisionWing WHERE IsActive = 'True' AND DivisionId = @DivisionId";
            aCommonInternalDal.LoadDropDownValue(ddl, "DivisionWingName", "DivisionWId", queryStr, aSqlParameterlist, "HRDB");
        }

        public void GetDepartmentList(DropDownList ddl, string wingId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@wingId", wingId));

            const string queryStr = "SELECT DepartmentId,DepartmentName FROM tblDepartment WHERE IsActive = 'True' AND DivisionWId = @wingId";
            aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", queryStr, aSqlParameterlist, "HRDB");
        }

        public void GetSectionList(DropDownList ddl, string departmentId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@departmentId", departmentId));

            const string queryStr = "SELECT SectionId,SectionName FROM tblSection WHERE IsActive = 'True' AND DepartmentId = @departmentId";
            aCommonInternalDal.LoadDropDownValue(ddl, "SectionName", "SectionId", queryStr, aSqlParameterlist,"HRDB");
        }

        public DataTable GetSubSectionInformation()
        {
            const string queryStr = @"SELECT * FROM tblSubSection AS SBEC
                                    INNER JOIN tblSection AS SEC ON SBEC.SectionId = SEC.SectionId
                                    INNER JOIN tblDepartment AS DPT ON SEC.DepartmentId = DPT.DepartmentId 
                                    INNER JOIN tblDivisionWing AS DVW ON DPT.DivisionWId = DVW.DivisionWId
                                    INNER JOIN tblDivision AS DV ON DVW.DivisionId = DV.DivisionId";

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetInformationById(string formId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@FormId", formId));

            const string queryStr = @"SELECT * FROM tblTrainingRequirementForm AS TRF
                                    INNER JOIN tblSubSection AS SBEC ON TRF.SubSectionId = SBEC.SubSectionId
                                    INNER JOIN tblSection AS SEC ON TRF.SectionId = SEC.SectionId
                                    INNER JOIN tblDepartment AS DPT ON TRF.DepartmentId = DPT.DepartmentId 
                                    INNER JOIN tblDivisionWing AS DVW ON TRF.DivisionWId = DVW.DivisionWId
                                    INNER JOIN tblDivision AS DV ON TRF.DivisionId = DV.DivisionId
									INNER JOIN tblCompanyInfo AS CI ON TRF.CompanyId = CI.CompanyId
									INNER JOIN tblFinancialYear AS FNY ON TRF.FinYearId = FNY.FinancialYearId
									INNER JOIN tblTrainingTopic AS TNP ON TRF.TrainingTopicId = TNP.TrainingTopicTitle WHERE TRF.TrainingRequirementId = @FormId";

            return aCommonInternalDal.DataContainerDataTable(queryStr,aSqlParameterlist, "HRDB");
        }

        public bool UpdateInfo(TrainingRequirementFormDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@TrainingRequirementId", aInformationDao.TrainingRequirementId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyInfoId", aInformationDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aInformationDao.DivisionId));
            aSqlParameterlist.Add(new SqlParameter("@DivisionWId", aInformationDao.DivisionWId));
            aSqlParameterlist.Add(new SqlParameter("@DepartmentId", aInformationDao.DepartmentId));
            aSqlParameterlist.Add(new SqlParameter("@SectionId", aInformationDao.SectionId));
            aSqlParameterlist.Add(new SqlParameter("@SubSectionId", aInformationDao.SubSectionId));
            aSqlParameterlist.Add(new SqlParameter("@FinYearId", aInformationDao.FinYearId));
            aSqlParameterlist.Add(new SqlParameter("@TrainingTopicId", aInformationDao.TrainingTopicId));

            aSqlParameterlist.Add(new SqlParameter("@Q1", aInformationDao.Q1));
            aSqlParameterlist.Add(new SqlParameter("@Q2", aInformationDao.Q2));
            aSqlParameterlist.Add(new SqlParameter("@Q3", aInformationDao.Q3));
            aSqlParameterlist.Add(new SqlParameter("@Q4", aInformationDao.Q4));

            aSqlParameterlist.Add(new SqlParameter("@TrainingExp", aInformationDao.TrainingExp));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));

            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));

            const string queryStr = @"UPDATE tblTrainingRequirementForm SET CompanyId = @CompanyInfoId,DivisionId = @DivisionId,DivisionWId = @DivisionWId ,DepartmentId = @DepartmentId,SectionId = @SectionId,
                                        SubSectionId = @SubSectionId,FinYearId = @FinYearId,TrainingTopicId = @TrainingTopicId,Q1 = @Q1,Q2 = @Q2,Q3 = @Q3,Q4 = @Q4,TrainingExp = @TrainingExp,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate,Remarks = @Remarks WHERE TrainingRequirementId = @TrainingRequirementId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteTrainingInfoById(string formId)
        {
            bool status = false;
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@FormId", formId));

            const string queryStr = "DELETE FROM tblTrainingRequirementForm WHERE TrainingRequirementId = @FormId";
            const string queryStrDetail = "DELETE FROM tblParticipantList WHERE MasterId = @FormId";

            if (aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB"))
            {
                status = aCommonInternalDal.DeleteDataByDeleteCommand(queryStrDetail, aSqlParameterlist, "HRDB");
            }

            return status;
        }

        public void GetSubSectionList(DropDownList ddl, string sectionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SubSectionId", sectionId));

            const string queryStr = "SELECT SubSectionId,SubSectionName FROM tblSubSection WHERE IsActive = 'True' AND SectionId = @SubSectionId";
            aCommonInternalDal.LoadDropDownValue(ddl, "SubSectionName", "SubSectionId", queryStr, aSqlParameterlist, "HRDB");
        }

        public void GetFinYearList(DropDownList ddl, string companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", companyId));

            const string queryStr = "SELECT FinancialYearId,FinancialYearDesc FROM tblFinancialYear WHERE Status = 'Active' AND CompanyId = @CompanyId";
            aCommonInternalDal.LoadDropDownValue(ddl, "FinancialYearDesc", "FinancialYearId", queryStr, aSqlParameterlist,"HRDB");
        }

        public Int32 SaveDetail(IEnumerable<ParticipantListDao> aList)
        {
            Int32 id = 0;

            foreach (ParticipantListDao aListDao in aList)
            {
                var aSqlParameterlist = new List<SqlParameter>();
                
                aSqlParameterlist.Add(new SqlParameter("@MasterId", aListDao.MasterId));
                aSqlParameterlist.Add(new SqlParameter("@ParticipantId", aListDao.ParticipantId));

                const string queryStr = "INSERT INTO tblParticipantList (MasterId,ParticipantId) VALUE (@MasterId, @ParticipantId)";
                id = aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
            }

            return id;
        }

        public DataTable aInformationDalGetParticipantList(string formId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@FormId", formId));

            const string queryStr = "SELECT * FROM tblParticipantList WHERE MasterId = @FormId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeletParticipantList(int masterId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@FormId", masterId));

            const string queryStr = "DELETE FROM tblParticipantList WHERE MasterId = @FormId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetTrainingRequirementInformation()
        {
            const string queryStr = @"SELECT * FROM tblTrainingRequirementForm AS TRF
                                    INNER JOIN tblCompanyInfo AS CI ON TRF.CompanyId = CI.CompanyId
                                    INNER JOIN tblDivision AS DV ON TRF.DivisionId = DV.DivisionId
                                    INNER JOIN tblDivisionWing AS DVW ON TRF.DivisionWId = DVW.DivisionWId
                                    INNER JOIN tblDepartment AS DPT ON TRF.DepartmentId = DPT.DepartmentId 
                                    INNER JOIN tblSection AS SEC ON TRF.SectionId = SEC.SectionId
                                    INNER JOIN tblSubSection AS SBC ON TRF.SubSectionId = SBC.SubSectionId
                                    INNER JOIN tblFinancialYear AS FINY ON TRF.FinYearId = FINY.FinancialYearId
                                    INNER JOIN tblTrainingTopic AS TP ON TRF.TrainingTopicId = TP.TopicId";

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetTrainingTopicId(string trainingTopic)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SearchText", trainingTopic));

            const string queryStr = "SELECT TopicId,TrainingTopicTitle from tblTrainingTopic WHERE TrainingTopicTitle = @SearchText";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetParticipantId(string participantName)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SearchText", participantName));

            const string queryStr = "SELECT EmpInfoId FROM tblEmpGeneralInfo WHERE EmpName = @SearchText";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
    }
}

