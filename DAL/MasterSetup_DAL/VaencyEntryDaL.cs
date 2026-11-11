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
    public class VaencyEntryDaL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int SaveVacancyEntryInfo(VacancyEntryDao aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CirculationWay", aVacancyEntryDao.CirculationWay));
        
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aVacancyEntryDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aVacancyEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aVacancyEntryDao.EntryDate));


            const string queryStr = @"INSERT INTO tblVacancyCirculation (CirculationWay,IsActive,EntryBy,EntryDate)
                                   VALUES (@CirculationWay,@IsActive,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }



        public DataTable GetVacaencyInformationById(string VacancyCirculationId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@VacancyCirculationId", VacancyCirculationId));

            const string queryStr = @"SELECT * FROM tblVacancyCirculation WHERE VacancyCirculationId = @VacancyCirculationId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateVacancyEntryInfo(VacancyEntryDao aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@VacancyCirculationId", aVacancyEntryDao.VacancyCirculationId));
            aSqlParameterlist.Add(new SqlParameter("@CirculationWay", aVacancyEntryDao.CirculationWay));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aVacancyEntryDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aVacancyEntryDao.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aVacancyEntryDao.IsActive));


            const string queryStr = @"UPDATE tblVacancyCirculation SET CirculationWay = @CirculationWay, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate,IsActive = @IsActive,
                                   Description = @Description,Remarks = @Remarks,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate WHERE VacancyCirculationId = @VacancyCirculationId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable AreaAllocatedOrNot(string areaId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@AreaId", areaId));

            const string queryStr = @"SELECT * FROM tblJobLocation WHERE AreaId = @AreaId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteVacancyEntryfoById(VacancyEntryDao aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@VacancyCirculationId", aVacancyEntryDao.VacancyCirculationId));
            aSqlParameterlist.Add(new SqlParameter("@IsDelete", aVacancyEntryDao.IsDelete));
            aSqlParameterlist.Add(new SqlParameter("@DeleteBy", aVacancyEntryDao.DeleteBy));
            aSqlParameterlist.Add(new SqlParameter("@DeleteDate", aVacancyEntryDao.DeleteDate));

            const string queryStr = @"UPDATE tblVacancyCirculation SET IsDelete = @IsDelete,DeleteBy = @DeleteBy, DeleteDate=@DeleteDate WHERE VacancyCirculationId = @VacancyCirculationId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public void LoadRegionList(DropDownList ddl)
        {
            const string queryStr = @"SELECT * FROM tblRegion WHERE IsActive = 'True'";
            aCommonInternalDal.LoadDropDownValue(ddl, "RegionName", "RegionId", queryStr, "HRDB");
        }


        public DataTable GetVacanceyEntryformationParam( )
        {
            string queryStr = @"SELECT * FROM tblVacancyCirculation where  (  IsDelete IS NULL
                                      OR  IsDelete = 0
                                    )";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
