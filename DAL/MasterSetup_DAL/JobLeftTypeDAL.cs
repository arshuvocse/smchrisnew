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
    public class JobLeftTypeDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int SaveVacancyEntryInfo(JobLeftTypeDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@JobLeftType", aVacancyEntryDao.JobLeftType));
        
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aVacancyEntryDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aVacancyEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aVacancyEntryDao.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@IsSubmissionDate", aVacancyEntryDao.IsSubmissionDate));


            const string queryStr = @"INSERT INTO tblJobLeftType (JobLeftType,IsActive,EntryBy,EntryDate, IsSubmissionDate)
                                   VALUES (@JobLeftType,@IsActive,@EntryBy,@EntryDate, @IsSubmissionDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }



        public DataTable GetVacaencyInformationById(string JobLeftTypeId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@JobLeftTypeId", JobLeftTypeId));

            const string queryStr = @"SELECT * FROM tblJobLeftType WHERE JobLeftTypeId = @JobLeftTypeId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateVacancyEntryInfo(JobLeftTypeDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@JobLeftTypeId", aVacancyEntryDao.JobLeftTypeId));
            aSqlParameterlist.Add(new SqlParameter("@JobLeftType", aVacancyEntryDao.JobLeftType));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aVacancyEntryDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aVacancyEntryDao.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aVacancyEntryDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@IsSubmissionDate", aVacancyEntryDao.IsSubmissionDate));


            const string queryStr = @"UPDATE tblJobLeftType SET JobLeftType = @JobLeftType, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate,IsActive = @IsActive, IsSubmissionDate=@IsSubmissionDate
                                    WHERE JobLeftTypeId = @JobLeftTypeId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable AreaAllocatedOrNot(string areaId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@AreaId", areaId));

            const string queryStr = @"SELECT * FROM tblJobLeftType WHERE AreaId = @AreaId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteVacancyEntryfoById(JobLeftTypeDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@JobLeftTypeId", aVacancyEntryDao.JobLeftTypeId));
            aSqlParameterlist.Add(new SqlParameter("@IsDelete", aVacancyEntryDao.IsDelete));
            aSqlParameterlist.Add(new SqlParameter("@DeleteBy", aVacancyEntryDao.DeleteBy));
            aSqlParameterlist.Add(new SqlParameter("@DeleteDate", aVacancyEntryDao.DeleteDate));

            const string queryStr = @"UPDATE tblJobLeftType SET IsDelete = @IsDelete,DeleteBy = @DeleteBy, DeleteDate=@DeleteDate WHERE JobLeftTypeId = @JobLeftTypeId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public void LoadRegionList(DropDownList ddl)
        {
            const string queryStr = @"SELECT * FROM tblRegion WHERE IsActive = 'True'";
            aCommonInternalDal.LoadDropDownValue(ddl, "RegionName", "RegionId", queryStr, "HRDB");
        }


        public DataTable GetVacanceyEntryformationParam( )
        {
            string queryStr = @"SELECT * FROM tblJobLeftType where  (  IsDelete IS NULL
                                      OR  IsDelete = 0
                                    )";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
