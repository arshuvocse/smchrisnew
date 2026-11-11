using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.MasterSetup_DAL
{
    public class SalaryLocationInformationDal
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


        public void GetOfficeNameDropdown(DropDownList ddl)
        {
            //const string queryStr = "SELECT CompanyId,ShortName FROM tblCompanyInfo";

            string queryStr = "SELECT * FROM tblSalaryLocation WHERE IsActive=1";
            aCommonInternalDal.LoadDropDownValue(ddl, "SalaryLocation", "SalaryLoationId", queryStr, "HRDB");
        }
        public int SaveSalaryLocationInfo(SalaryLocationInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SalaryLocation", aInformationDao.SalaryLocation));
            aSqlParameterlist.Add(new SqlParameter("@JoinIdSalaryLocation", (object)aInformationDao.JoinIdSalaryLocation ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            const string queryStr = @"INSERT INTO tblSalaryLocation (SalaryLocation,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus, JoinIdSalaryLocation)
                                   VALUES (@SalaryLocation,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus, @JoinIdSalaryLocation)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }


        public int SaveSalaryLocationInfoDEL(SalaryLocationInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", aInformationDao.SalaryLoationId));
            aSqlParameterlist.Add(new SqlParameter("@SalaryLocation", aInformationDao.SalaryLocation));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@ApprovalStatus", aInformationDao.ApprovalStatus));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            const string queryStr = @"INSERT INTO DELtblSalaryLocation (SalaryLoationId,SalaryLocation,IsActive,Description,Remarks,EntryBy,EntryDate,ApprovalStatus)
                                   VALUES (@SalaryLoationId,@SalaryLocation,@IsActive,@Description,@Remarks,@EntryBy,@EntryDate,@ApprovalStatus)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable CheckLocationExistOrNot(string location)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SalaryLocation", location));

            const string queryStr = @"SELECT * FROM tblSalaryLocation WHERE SalaryLocation = @SalaryLocation";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetSalaryLocationInformation()
        {
            const string queryStr = @"SELECT e.SalaryLoationId,
    e.SalaryLocation  ,
    m.SalaryLocation  OtherSalaryLocation , e.*
FROM
    tblSalaryLocation e
LEFT JOIN tblSalaryLocation m ON  e.SalaryLoationId=
  m.JoinIdSalaryLocation ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetSalaryLocationInformationById(string locationId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", locationId));

            const string queryStr = @"SELECT * FROM tblSalaryLocation WHERE SalaryLoationId = @SalaryLoationId";
            return aCommonInternalDal.DataContainerDataTable(queryStr,aSqlParameterlist, "HRDB");
        }

        public bool UpdateSalaryLocationInfo(SalaryLocationInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", aInformationDao.SalaryLoationId));
            aSqlParameterlist.Add(new SqlParameter("@SalaryLocation", aInformationDao.SalaryLocation));
            aSqlParameterlist.Add(new SqlParameter("@JoinIdSalaryLocation", (object)aInformationDao.JoinIdSalaryLocation ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));

            const string queryStr = @"UPDATE tblSalaryLocation SET SalaryLocation = @SalaryLocation,IsActive = @IsActive, JoinIdSalaryLocation=@JoinIdSalaryLocation,
                                   Description = @Description,Remarks = @Remarks,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate WHERE SalaryLoationId = @SalaryLoationId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable RegionAllocatedOrNot(string regionId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@RegionId", regionId));

            const string queryStr = @"SELECT * FROM tblArea WHERE RegionId = @RegionId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist ,"HRDB");
        }

        public bool DeleteSalaryLocationInfoById(string salaryLoationId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", salaryLoationId));

            const string queryStr = @"DELETE FROM tblSalaryLocation WHERE SalaryLoationId = @SalaryLoationId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist ,"HRDB");
        }
    }
}

