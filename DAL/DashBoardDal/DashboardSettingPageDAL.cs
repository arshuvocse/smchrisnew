using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.MasterSetup_DAL
{
    public class DashboardSettingPageDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

 
        public void GetDDLDistrict(DropDownList ddl)
        {
            string queryStr = @"SELECT AddressDivisionID  , Title  FROM dbo.tblAddressDivision ";
            aCommonInternalDal.LoadDropDownValue(ddl, "Title", "AddressDivisionID", queryStr, "HRDB");
        }

        public Int32 SaveInfoDEL(AddressDistrictEntryDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DistrictID", aInformationDao.DistrictID));
            aSqlParameterlist.Add(new SqlParameter("@DivisionID", aInformationDao.DivisionID));
            aSqlParameterlist.Add(new SqlParameter("@Titel", aInformationDao.Titel));


            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));


            const string queryStr = @"INSERT INTO DELtblDistrict (DistrictID, DivisionID, Titel,EntryBy,EntryDate)
                                   VALUES (@DistrictID, @DivisionID, @Titel,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public int SaveEntryInfo(DashboardSettingDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@Contractual", aVacancyEntryDao.Contractual));

            aSqlParameterlist.Add(new SqlParameter("@Prohibition", aVacancyEntryDao.Prohibition));
            aSqlParameterlist.Add(new SqlParameter("@Retirement", aVacancyEntryDao.Retirement));
            aSqlParameterlist.Add(new SqlParameter("@UserId", aVacancyEntryDao.UserId));


            const string queryStr = @"INSERT INTO [dbo].[tblDashboardSetting]
           ([Contractual]
           ,[Prohibition]
           ,[Retirement]
           ,[UserId])
     VALUES
           (@Contractual, 
           @Prohibition, 
           @Retirement, 
           @UserId)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }



        public DataTable GetInformationById(string MId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@UserId", MId));

            const string queryStr = @"SELECT * FROM tblDashboardSetting WHERE UserId = @UserId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateEntryInfo(DashboardSettingDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@Contractual", aEntryDao.Contractual));

            aSqlParameterlist.Add(new SqlParameter("@Prohibition", aEntryDao.Prohibition));
            aSqlParameterlist.Add(new SqlParameter("@Retirement", aEntryDao.Retirement));
            aSqlParameterlist.Add(new SqlParameter("@UserId", aEntryDao.UserId));



            const string queryStr = @"UPDATE [dbo].[tblDashboardSetting]
   SET [Contractual] = @Contractual,  
     [Prohibition] = @Prohibition, 
      [Retirement] = @Retirement 
                                    WHERE UserId = @UserId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

     

        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@DistrictID", Id));


            const string queryStr = @"DELETE FROM tblDistrict  WHERE DistrictID = @DistrictID";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

      


        public DataTable GetEntryformation( )
        {
            string queryStr = @"SELECT div.Title AS DivisionName, us.UserName EntryBy, usUp.UserName UpdateBy, * FROM tblDistrict MTb
left JOIN  dbo.tblUser us   ON  MTb.EntryBy =us.UserId  
left JOIN  dbo.tblUser usUp   ON  MTb.UpdateBy =usUp.UserId
LEFT JOIN  dbo.tblAddressDivision div   ON  MTb.DivisionID =div.AddressDivisionID";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
