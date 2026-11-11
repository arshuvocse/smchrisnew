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
    public class ExtraCurriculamActivitiesEntryDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int SaveVacancyEntryInfo(ExtraCurriculamActivitiesEntryDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@ExtraCurriculamName", aEntryDao.ExtraCurriculamName));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aEntryDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aEntryDao.EntryDate));


            const string queryStr = @"INSERT INTO tblMasterExtraCurriculam (ExtraCurriculamName,IsActive,EntryBy,EntryDate)
                                   VALUES (@ExtraCurriculamName,@IsActive,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public Int32 SaveInfoDEL(ExtraCurriculamActivitiesEntryDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@MasterExtraCurriculamId", aInformationDao.MasterExtraCurriculamId));
            aSqlParameterlist.Add(new SqlParameter("@ExtraCurriculamName", aInformationDao.ExtraCurriculamName));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));


            const string queryStr = @"INSERT INTO DELtblMasterExtraCurriculam (MasterExtraCurriculamId,ExtraCurriculamName,IsActive,EntryBy,EntryDate)
                                   VALUES (@MasterExtraCurriculamId, @ExtraCurriculamName, @IsActive, @EntryBy, @EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MasterExtraCurriculamId", Id));


            const string queryStr = @"DELETE FROM tblMasterExtraCurriculam  WHERE MasterExtraCurriculamId = @MasterExtraCurriculamId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetVacaencyInformationById(string MasterExtraCurriculamId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MasterExtraCurriculamId", MasterExtraCurriculamId));

            const string queryStr = @"SELECT * FROM tblMasterExtraCurriculam WHERE MasterExtraCurriculamId = @MasterExtraCurriculamId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateVacancyEntryInfo(ExtraCurriculamActivitiesEntryDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@MasterExtraCurriculamId", aInformationDao.MasterExtraCurriculamId));
            aSqlParameterlist.Add(new SqlParameter("@ExtraCurriculamName", aInformationDao.ExtraCurriculamName));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));


            const string queryStr = @"UPDATE tblMasterExtraCurriculam SET ExtraCurriculamName = @ExtraCurriculamName, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate,IsActive = @IsActive
                                   WHERE MasterExtraCurriculamId = @MasterExtraCurriculamId";

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
            string queryStr = @"SELECT * FROM tblMasterExtraCurriculam ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
