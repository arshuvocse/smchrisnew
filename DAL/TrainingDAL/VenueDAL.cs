using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using DAO.HRIS_DAO;
namespace DAL.TrainingDAL
{
    public class VenueDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int SaveAreaInfo(Venue aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@VenueName", aInformationDao.VenueName));
            aSqlParameterlist.Add(new SqlParameter("@Adress", aInformationDao.Adress));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            const string queryStr = @"INSERT INTO tblSMCTrainingVenue (VenueName,Adress,IsActive,EntryBy,EntryDate)
                                   VALUES (@VenueName,@Adress,@IsActive,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateAreaInfo(Venue aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SMCVenueID", aInformationDao.SMCVenueID));
            aSqlParameterlist.Add(new SqlParameter("@VenueName", aInformationDao.VenueName));
            aSqlParameterlist.Add(new SqlParameter("@Adress", aInformationDao.Adress));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.Updateby));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.Upatedate));

            const string queryStr = @"UPDATE tblSMCTrainingVenue SET VenueName = @VenueName, Adress = @Adress,IsActive = @IsActive,
                                   UpdateBy = @UpdateBy,Upatedate = @UpdateDate WHERE SMCVenueID = @SMCVenueID";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable CheckAreaCodeExistOrNot(string areaCode)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@AreaCode", areaCode));

            const string queryStr = @"SELECT * FROM tblArea WHERE AreaCode = @AreaCode";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetAreaInformation()
        {
            const string queryStr = @"SELECT * FROM dbo.tblSMCTrainingVenue WHERE IsDeleted IS NULL OR IsDeleted  = 0";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetRegionInformationById(string SMCVenueID)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SMCVenueID", SMCVenueID));

            const string queryStr = @"SELECT * FROM tblSMCTrainingVenue WHERE SMCVenueID = @SMCVenueID";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
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
            aSqlParameterlist.Add(new SqlParameter("@ID", areaId));

            string queryStr = @"UPDATE tblSMCTrainingVenue SET IsDeleted='1',DeleteBy='"+HttpContext.Current.Session["UserId"].ToString()+"'," +
                              " DeleteDate='" + DateTime.Now + "'  WHERE SMCVenueID= @ID";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public void LoadRegionList(DropDownList ddl)
        {
            const string queryStr = @"SELECT * FROM tblRegion WHERE IsActive = 'True'";
            aCommonInternalDal.LoadDropDownValue(ddl, "RegionName", "RegionId", queryStr, "HRDB");
        }
    }
}

