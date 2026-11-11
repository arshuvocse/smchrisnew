using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.MasterSetup_DAL
{
    public class OfficeInformationDal
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int SaveRegionInfo(RegionInformatinDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@RegionName", aInformationDao.RegionName));
            aSqlParameterlist.Add(new SqlParameter("@RegionCode", aInformationDao.RegionCode));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            const string queryStr = @"INSERT INTO tblOfficeName (OfficeName,OfficeCode,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus)
                                   VALUES (@RegionName,@RegionCode,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable CheckRegionCodeExistOrNot(string regionCode)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@RegionCode", regionCode));

            const string queryStr = @"SELECT * FROM tblOfficeName WHERE OfficeCode = @RegionCode";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetRegionInformation()
        {
            const string queryStr = @"SELECT * FROM tblOfficeName ORDER BY OfficeId DESC";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetRegionInformationById(string regionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@RegionId", regionId));

            const string queryStr = @"SELECT * FROM tblOfficeName WHERE OfficeId = @RegionId";
            return aCommonInternalDal.DataContainerDataTable(queryStr,aSqlParameterlist, "HRDB");
        }

        public bool UpdateRegionInfo(RegionInformatinDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@RegionId", aInformationDao.RegionId));
            aSqlParameterlist.Add(new SqlParameter("@RegionName", aInformationDao.RegionName));
            aSqlParameterlist.Add(new SqlParameter("@RegionCode", aInformationDao.RegionCode));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));

            const string queryStr = @"UPDATE tblOfficeName SET OfficeName = @RegionName,OfficeCode = @RegionCode,IsActive = @IsActive,
                                   Description = @Description,Remarks = @Remarks,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate WHERE OfficeId = @RegionId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable RegionAllocatedOrNot(string regionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@RegionId", regionId));

            const string queryStr = @"SELECT * FROM tblArea WHERE RegionId = @RegionId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist ,"HRDB");
        }

        public bool DeleteRegionInfoById(string regionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@RegionId", regionId));

            const string queryStr = @"DELETE FROM tblOfficeName WHERE OfficeId = @RegionId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist ,"HRDB");
        }
    }
}
