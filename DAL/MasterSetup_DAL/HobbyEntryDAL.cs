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
    public class HobbyEntryDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int SaveVacancyEntryInfo(HobbyEntryDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@HobbyName", aEntryDao.HobbyName));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aEntryDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aEntryDao.EntryDate));


            const string queryStr = @"INSERT INTO tblMasterHobby (HobbyName,IsActive,EntryBy,EntryDate)
                                   VALUES (@HobbyName,@IsActive,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public Int32 SaveInfoDEL(HobbyEntryDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@MasterHobbyId", aInformationDao.MasterHobbyId));
            aSqlParameterlist.Add(new SqlParameter("@HobbyName", aInformationDao.HobbyName));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));


            const string queryStr = @"INSERT INTO DELtblMasterHobby (MasterHobbyId,HobbyName,IsActive,EntryBy,EntryDate)
                                   VALUES (@MasterHobbyId, @HobbyName, @IsActive, @EntryBy, @EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MasterHobbyId", Id));


            const string queryStr = @"DELETE FROM tblMasterHobby  WHERE MasterHobbyId = @MasterHobbyId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetVacaencyInformationById(string VacancyCirculationId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MasterHobbyId", VacancyCirculationId));

            const string queryStr = @"SELECT * FROM tblMasterHobby WHERE MasterHobbyId = @MasterHobbyId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateVacancyEntryInfo(HobbyEntryDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@MasterHobbyId", aInformationDao.MasterHobbyId));
            aSqlParameterlist.Add(new SqlParameter("@HobbyName", aInformationDao.HobbyName));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));


            const string queryStr = @"UPDATE tblMasterHobby SET HobbyName = @HobbyName, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate,IsActive = @IsActive
                                   WHERE MasterHobbyId = @MasterHobbyId";

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


        public DataTable GetVacanceyEntryformationParam( )
        {
            string queryStr = @"SELECT * FROM tblMasterHobby ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
