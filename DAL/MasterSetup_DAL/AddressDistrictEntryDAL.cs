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
    public class AddressDistrictEntryDAL
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
        public int SaveEntryInfo(AddressDistrictEntryDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@Titel", aVacancyEntryDao.Titel));

            aSqlParameterlist.Add(new SqlParameter("@DivisionID", aVacancyEntryDao.DivisionID));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aVacancyEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aVacancyEntryDao.EntryDate));


            const string queryStr = @" Declare @Count Int

select @Count=count(*) from tblDistrict where Titel=LTRIM(RTRIM(@Titel)) and DivisionID=@DivisionID
 print @Count
 if(@Count=0) INSERT INTO tblDistrict (DivisionID,Titel,EntryBy,EntryDate)
                                   VALUES (@DivisionID,@Titel,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }



        public DataTable GetInformationById(string MId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@DistrictID", MId));

            const string queryStr = @"SELECT * FROM tblDistrict WHERE DistrictID = @DistrictID";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateEntryInfo(AddressDistrictEntryDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@DistrictID", aEntryDao.DistrictID));
            aSqlParameterlist.Add(new SqlParameter("@DivisionID", aEntryDao.DivisionID));
            aSqlParameterlist.Add(new SqlParameter("@Titel", aEntryDao.Titel));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aEntryDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aEntryDao.UpdateDate));



            const string queryStr = @" 
Declare @Count Int

select @Count=count(*) from tblDistrict where Titel=LTRIM(RTRIM(@Titel)) and  DivisionID=@DivisionID and DistrictID not in (@DistrictID)
 print @Count
 if(@Count=0) UPDATE tblDistrict SET DivisionID=@DivisionID, Titel = @Titel, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate
                                    WHERE DistrictID = @DistrictID";

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
