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
    public class PlaceInformationDal
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int SaveAreaInfo(AreaInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@RegionId", aInformationDao.RegionId));
            aSqlParameterlist.Add(new SqlParameter("@AreaName", aInformationDao.AreaName));
            aSqlParameterlist.Add(new SqlParameter("@AreaCode", aInformationDao.AreaCode));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            const string queryStr = @"INSERT INTO tblPlaceInformation (OfficeId,PlaceName,PlaceCode,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus)
                                   VALUES (@RegionId,@AreaName,@AreaCode,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable CheckAreaCodeExistOrNot(string areaCode)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@AreaCode", areaCode));

            const string queryStr = @"SELECT * FROM tblPlaceInformation WHERE AreaCode = @AreaCode";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetAreaInformation()
        {
            const string queryStr = @"SELECT * FROM tblPlaceInformation AS AR 
                                      INNER JOIN tblRegion AS RGN ON AR.RegionId = RGN.RegionId ORDER BY AR.AreaId DESC";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetRegionInformationById(string areaId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@AreaId", areaId));

            const string queryStr = @"SELECT * FROM tblPlaceInformation WHERE AreaId = @AreaId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateAreaInfo(AreaInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@AreaId", aInformationDao.AreaId));
            aSqlParameterlist.Add(new SqlParameter("@RegionId", aInformationDao.RegionId));
            aSqlParameterlist.Add(new SqlParameter("@AreaName", aInformationDao.AreaName));
            aSqlParameterlist.Add(new SqlParameter("@AreaCode", aInformationDao.AreaCode));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));

            const string queryStr = @"UPDATE tblPlaceInformation SET RegionId = @RegionId, AreaName = @AreaName,AreaCode = @AreaCode,IsActive = @IsActive,
                                   Description = @Description,Remarks = @Remarks,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate WHERE AreaId = @AreaId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        } 

        public DataTable AreaAllocatedOrNot(string areaId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@AreaId", areaId));

            const string queryStr = @"SELECT * FROM tblJobLocation WHERE AreaId = @AreaId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteAreaInfoById(string areaId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@AreaId", areaId));

            const string queryStr = @"DELETE FROM tblPlaceInformation WHERE AreaId = @AreaId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public void LoadRegionList(DropDownList ddl)
        {
            const string queryStr = @"SELECT * FROM tblPlaceInformation WHERE IsActive = 'True'";
            aCommonInternalDal.LoadDropDownValue(ddl, "OfficeName", "OfficeId", queryStr, "HRDB");
        }
    }
}
