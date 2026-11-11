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
    public class TraingingHeadingEntryDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int SaveVacancyEntryInfo(TraingingHeadingEntryDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@TraingingHeading", aVacancyEntryDao.TraingingHeading));
        
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aVacancyEntryDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aVacancyEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aVacancyEntryDao.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@TraingingSerial", aVacancyEntryDao.TraingingSerial));


            const string queryStr = @"INSERT INTO tblTraingingHeading (TraingingHeading,IsActive,EntryBy,EntryDate, TraingingSerial, IsDelete)
                                   VALUES (@TraingingHeading,@IsActive,@EntryBy,@EntryDate, @TraingingSerial, 0)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }



        public DataTable GetVacaencyInformationById(string VacancyCirculationId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@TraingingHeadingId", VacancyCirculationId));

            const string queryStr = @"SELECT * FROM tblTraingingHeading WHERE TraingingHeadingId = @TraingingHeadingId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateVacancyEntryInfo(TraingingHeadingEntryDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@TraingingHeadingId", aVacancyEntryDao.TraingingHeadingId));
            aSqlParameterlist.Add(new SqlParameter("@TraingingHeading", aVacancyEntryDao.TraingingHeading));
            aSqlParameterlist.Add(new SqlParameter("@TraingingSerial", aVacancyEntryDao.TraingingSerial));
           
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aVacancyEntryDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aVacancyEntryDao.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aVacancyEntryDao.IsActive));


            const string queryStr = @"UPDATE tblTraingingHeading SET TraingingHeading = @TraingingHeading, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate,IsActive = @IsActive, TraingingSerial=@TraingingSerial
                                   WHERE TraingingHeadingId = @TraingingHeadingId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable AreaAllocatedOrNot(string areaId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@AreaId", areaId));

            const string queryStr = @"SELECT * FROM tblJobLocation WHERE AreaId = @AreaId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteVacancyEntryfoById(TraingingHeadingEntryDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@TraingingHeadingId", aVacancyEntryDao.TraingingHeadingId));
            aSqlParameterlist.Add(new SqlParameter("@IsDelete", aVacancyEntryDao.IsDelete));
            aSqlParameterlist.Add(new SqlParameter("@DeleteBy", aVacancyEntryDao.DeleteBy));
            aSqlParameterlist.Add(new SqlParameter("@DeleteDate", aVacancyEntryDao.DeleteDate));

            const string queryStr = @"UPDATE tblTraingingHeading SET IsDelete = @IsDelete,DeleteBy = @DeleteBy, DeleteDate=@DeleteDate WHERE TraingingHeadingId = @TraingingHeadingId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public void LoadRegionList(DropDownList ddl)
        {
            const string queryStr = @"SELECT * FROM tblRegion WHERE IsActive = 'True'";
            aCommonInternalDal.LoadDropDownValue(ddl, "RegionName", "RegionId", queryStr, "HRDB");
        }

        public DataTable GetTrainingTopicByHeadin(string id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ID", id));

            const string queryStr = @"SELECT * FROM dbo.tblTrainingSetupTopic WHERE TraingingHeadingId=@ID";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable GetVacanceyEntryformationParam( )
        {
            string queryStr = @"SELECT * FROM tblTraingingHeading
            LEFT JOIN dbo.tblUser ON dbo.tblTraingingHeading.EntryBy=dbo.tblUser.UserId
             where  (  IsDelete IS NULL OR  IsDelete = 0 )";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
