using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DAL.InternalCls;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

namespace DAL.MasterSetup_DAL
{
    public class JobLocationDal
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int SaveJobLocationInfo(JobLocationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@AreaId", aInformationDao.AreaId));
            aSqlParameterlist.Add(new SqlParameter("@Location", aInformationDao.Location));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            const string queryStr = @"INSERT INTO tblJobLocation (SalaryLoationId,Location,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus)
                                   VALUES (@AreaId,@Location,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public int SaveJobLocationInfoDEL(JobLocationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@JobLocationID", aInformationDao.JobLocationID));
            aSqlParameterlist.Add(new SqlParameter("@AreaId", aInformationDao.AreaId));
            aSqlParameterlist.Add(new SqlParameter("@Location", aInformationDao.Location));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            const string queryStr = @"INSERT INTO DELtblJobLocation (JobLocationID,SalaryLoationId,Location,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus)
                                   VALUES (@JobLocationID,@AreaId,@Location,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable CheckLocationExistOrNot(string location, string officelocation)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@Location", location));
            aSqlParameterlist.Add(new SqlParameter("@officelocation", officelocation));

            const string queryStr = @"SELECT * FROM tblJobLocation WHERE Location = @Location and SalaryLoationId=@officelocation";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetJobLocationInformation()
        {
            string queryStr = @"SELECT * FROM tblJobLocation AS JL 
                                    INNER JOIN tblSalaryLocation AS AR ON JL.SalaryLoationId = AR.SalaryLoationId
                                ORDER BY  JL.JobLocationID DESC";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
//            const string queryStr = @"SELECT * FROM tblJobLocation AS JL 
//                                    INNER JOIN tblArea AS AR ON JL.AreaId = AR.AreaId
//                                    INNER JOIN tblRegion AS RGN ON AR.RegionId = RGN.RegionId ORDER BY  JL.JobLocationID DESC";
//            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetJobLocationInformationById(string locationId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@LocationId", locationId));

            const string queryStr = @"SELECT * FROM tblJobLocation AS JL 
                                    INNER JOIN tblSalaryLocation AS AR ON JL.SalaryLoationId = AR.SalaryLoationId
                                    WHERE JobLocationID = @LocationId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateAreaInfo(JobLocationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@JobLocationID", aInformationDao.JobLocationID));
            aSqlParameterlist.Add(new SqlParameter("@AreaId", aInformationDao.AreaId));
            aSqlParameterlist.Add(new SqlParameter("@Location", aInformationDao.Location));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));

            const string queryStr = @"UPDATE tblJobLocation SET SalaryLoationId = @AreaId, Location = @Location,IsActive = @IsActive,
                                   Description = @Description,Remarks = @Remarks,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate WHERE JobLocationID = @JobLocationID";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable AreaAllocatedOrNot(string areaId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@AreaId", areaId));

            const string queryStr = @"SELECT * FROM tblJobLocation WHERE AreaId = @AreaId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteJobLocationInfoById(string locationId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@locationId", locationId));

            const string queryStr = @"DELETE FROM tblJobLocation WHERE JobLocationID = @locationId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public void LoadRegionList(DropDownList ddl)
        {
            const string queryStr = @"SELECT * FROM tblRegion WHERE IsActive = 'True'";
            aCommonInternalDal.LoadDropDownValue(ddl, "RegionName", "RegionId", queryStr, "HRDB");
        }
        public void LoadOfficeList(DropDownList ddl)
        {
            const string queryStr = @"SELECT * FROM tblSalaryLocation WHERE IsActive = 'True'";
            aCommonInternalDal.LoadDropDownValue(ddl, "SalaryLocation", "SalaryLoationId", queryStr, "HRDB");
        }

        public void LoadAreaList(DropDownList ddl, string regionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@RegionId", regionId));

            const string queryStr = @"SELECT * FROM tblArea WHERE IsActive = 'True' AND RegionId = @RegionId";
            aCommonInternalDal.LoadDropDownValue(ddl, "AreaName", "AreaId", queryStr, aSqlParameterlist, "HRDB");
        }


    }
}
