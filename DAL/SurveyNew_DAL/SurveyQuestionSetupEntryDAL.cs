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
    public class SurveyQuestionSetupEntryDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


        public void GetDDLSurveyQuestionGroup(DropDownList ddl)
        {
            string queryStr = @"SELECT * FROM tblSurveyQuestionGroup WHERE IsActive=1";
            aCommonInternalDal.LoadDropDownValue(ddl, "SurveyQuestionGroup", "SurveyQuestionGroupId", queryStr, "HRDB");
        }
        public void GetDDLSurveyQuestionGroupALL(DropDownList ddl)
        {
            string queryStr = @"SELECT * FROM tblSurveyQuestionGroup ";
            aCommonInternalDal.LoadDropDownValue(ddl, "SurveyQuestionGroup", "SurveyQuestionGroupId", queryStr, "HRDB");
        }

        public void GetDDLSurveyQuestionType(DropDownList ddl)
        {
            string queryStr = @"SELECT  * FROM dbo.tblSurveyQuestionType WHERE IsActive=1";
            aCommonInternalDal.LoadDropDownValue(ddl, "SurveyQuestionType", "SurveyQuestionTypeId", queryStr, "HRDB");
        }
        public void GetDDLSurveyQuestionTypeALl(DropDownList ddl)
        {
            string queryStr = @"SELECT  * FROM dbo.tblSurveyQuestionType";
            aCommonInternalDal.LoadDropDownValue(ddl, "SurveyQuestionType", "SurveyQuestionTypeId", queryStr, "HRDB");
        }
        public int SaveVacancyEntryInfo(SurveyQuestionSetupEntryDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@QuestionTitle", aEntryDao.QuestionTitle));
            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionTypeId", aEntryDao.SurveyQuestionTypeId));
            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionGroupId", aEntryDao.SurveyQuestionGroupId));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aEntryDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aEntryDao.EntryDate));


            const string queryStr = @"INSERT INTO tblSurveyQuestion (QuestionTitle, SurveyQuestionTypeId, SurveyQuestionGroupId,IsActive,EntryBy,EntryDate)
                                   VALUES (@QuestionTitle,@SurveyQuestionTypeId, @SurveyQuestionGroupId,@IsActive,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }


        public int SaveVacancyDtls(SurveyMasterAnswrDtls aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionTitleId", aEntryDao.SurveyQuestionTitleId));
            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionGroupId", aEntryDao.SurveyQuestionGroupId));
            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionAnswer", aEntryDao.SurveyQuestionAnswer));

        

            const string queryStr = @"INSERT INTO [dbo].[tblSurveyQuestionAnswer]
           ([SurveyQuestionTitleId]
           ,[SurveyQuestionGroupId]
           ,[SurveyQuestionAnswer]
            )
     VALUES
           ( @SurveyQuestionTitleId,  
           @SurveyQuestionGroupId, 
         @SurveyQuestionAnswer 
          )";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public Int32 SaveInfoDEL(SurveyQuestionSetupEntryDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionId", aEntryDao.SurveyQuestionId));
            aSqlParameterlist.Add(new SqlParameter("@QuestionTitle", aEntryDao.QuestionTitle));
            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionTypeId", aEntryDao.SurveyQuestionTypeId));
            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionGroupId", aEntryDao.SurveyQuestionGroupId));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aEntryDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aEntryDao.EntryDate));


            const string queryStr = @"INSERT INTO DELtblSurveyQuestion (SurveyQuestionId, QuestionTitle, SurveyQuestionTypeId, SurveyQuestionGroupId,IsActive,EntryBy,EntryDate)
                                   VALUES (@SurveyQuestionId, @QuestionTitle,@SurveyQuestionTypeId, @SurveyQuestionGroupId,@IsActive,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionId", Id));


            const string queryStr = @"DELETE FROM tblSurveyQuestion  WHERE SurveyQuestionId = @SurveyQuestionId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetVacaencyInformationById(string SurveyQuestionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionId", SurveyQuestionId));

            const string queryStr = @"SELECT * FROM tblSurveyQuestion WHERE SurveyQuestionId = @SurveyQuestionId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable GetVacaencyDtlsById(string SurveyQuestionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionId", SurveyQuestionId));

            const string queryStr = @"SELECT SurveyQuestionAnswer OtherRequirementsName,* FROM tblSurveyQuestionAnswer WHERE SurveyQuestionTitleId = @SurveyQuestionId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateVacancyEntryInfo(SurveyQuestionSetupEntryDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionId", aInformationDao.SurveyQuestionId));
            aSqlParameterlist.Add(new SqlParameter("@QuestionTitle", aInformationDao.QuestionTitle));
            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionTypeId", aInformationDao.SurveyQuestionTypeId));
            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionGroupId", aInformationDao.SurveyQuestionGroupId));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));


            const string queryStr = @"UPDATE tblSurveyQuestion SET QuestionTitle = @QuestionTitle, SurveyQuestionTypeId=@SurveyQuestionTypeId, SurveyQuestionGroupId=@SurveyQuestionGroupId, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate,IsActive = @IsActive
                                   WHERE SurveyQuestionId = @SurveyQuestionId"; 

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable AreaAllocatedOrNot(string areaId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@AreaId", areaId));

            const string queryStr = @"SELECT * FROM tblJobLocation WHERE AreaId = @AreaId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }



        public void LoadRegionList(DropDownList ddl)
        {
            const string queryStr = @"SELECT * FROM tblRegion WHERE IsActive = 'True'";
            aCommonInternalDal.LoadDropDownValue(ddl, "RegionName", "RegionId", queryStr, "HRDB");
        }


        public DataTable GetVacanceyEntryformationParam()
        {
            string queryStr = @"SELECT tblSurveyQuestionGroup.SurveyQuestionGroup, tblSurveyQuestionType.SurveyQuestionType, * FROM tblSurveyQuestion
LEFT JOIN dbo.tblSurveyQuestionGroup ON tblSurveyQuestionGroup.SurveyQuestionGroupId = tblSurveyQuestion.SurveyQuestionGroupId
LEFT JOIN dbo.tblSurveyQuestionType ON tblSurveyQuestionType.SurveyQuestionTypeId = tblSurveyQuestion.SurveyQuestionTypeId ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
