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
    public class surveyQuestionGroupEntryDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int SaveVacancyEntryInfo(surveyQuestionGroupEntryDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionGroup", aEntryDao.SurveyQuestionGroup));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aEntryDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aEntryDao.EntryDate));


            const string queryStr = @"INSERT INTO tblSurveyQuestionGroup (SurveyQuestionGroup,IsActive,EntryBy,EntryDate)
                                   VALUES (@SurveyQuestionGroup,@IsActive,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public Int32 SaveInfoDEL(surveyQuestionGroupEntryDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionGroupId", aInformationDao.SurveyQuestionGroupId));
            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionGroup", aInformationDao.SurveyQuestionGroup));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));


            const string queryStr = @"INSERT INTO DELtblSurveyQuestionGroup (SurveyQuestionGroupId,SurveyQuestionGroup,IsActive,EntryBy,EntryDate)
                                   VALUES (@SurveyQuestionGroupId, @SurveyQuestionGroup, @IsActive, @EntryBy, @EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionGroupId", Id));


            const string queryStr = @"DELETE FROM tblSurveyQuestionGroup  WHERE SurveyQuestionGroupId = @SurveyQuestionGroupId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetVacaencyInformationById(string SurveyQuestionGroupId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionGroupId", SurveyQuestionGroupId));

            const string queryStr = @"SELECT * FROM tblSurveyQuestionGroup WHERE SurveyQuestionGroupId = @SurveyQuestionGroupId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateVacancyEntryInfo(surveyQuestionGroupEntryDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionGroupId", aInformationDao.SurveyQuestionGroupId));
            aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionGroup", aInformationDao.SurveyQuestionGroup));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));


            const string queryStr = @"UPDATE tblSurveyQuestionGroup SET SurveyQuestionGroup = @SurveyQuestionGroup, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate,IsActive = @IsActive
                                   WHERE SurveyQuestionGroupId = @SurveyQuestionGroupId";

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
            string queryStr = @"SELECT * FROM tblSurveyQuestionGroup ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
