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
    public class AddressThanaEntryDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

 
        public void GetDDLDivision(DropDownList ddl)
        {
            string queryStr = @"SELECT  AddressDivisionID,
                           Title  FROM dbo.tblAddressDivision  ";
            aCommonInternalDal.LoadDropDownValue(ddl, "Title", "AddressDivisionID", queryStr, "HRDB");
        }


        public void LoadDistricByDivisionId(DropDownList ddl, string DivisionId)
        {
            string query = @"SELECT  DivisionID , DistrictID,
                            Titel  FROM dbo.tblDistrict  WHERE DivisionID='" + DivisionId + "'";

            aCommonInternalDal.LoadDropDownValue(ddl, "Titel", "DistrictID", query, "HRDB");
        }


        public void LoadDistric(DropDownList ddl)
        {
            string query = @" SELECT FINY.DivisionID,
                            FINY.Titel  FROM dbo.tblDistrict AS FINY 
                          ";

            aCommonInternalDal.LoadDropDownValue(ddl, "Titel", "DivisionID", query, "HRDB");
        }


        public Int32 SaveInfoDEL(AddressThanaEntryDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@ThanaID", aInformationDao.ThanaID));
            aSqlParameterlist.Add(new SqlParameter("@DistrictID", aInformationDao.DistrictID));
            aSqlParameterlist.Add(new SqlParameter("@Title", aInformationDao.Title));


            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));


            const string queryStr = @"INSERT INTO DELtblThana (ThanaID, DistrictID, Title,EntryBy,EntryDate)
                                   VALUES (@ThanaID, @DistrictID, @Title,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public int SaveEntryInfo(AddressThanaEntryDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@Title", aVacancyEntryDao.Title));

            aSqlParameterlist.Add(new SqlParameter("@DistrictID", aVacancyEntryDao.DistrictID));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aVacancyEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aVacancyEntryDao.EntryDate));


            const string queryStr = @" Declare @Count Int

select @Count=count(*) from tblThana where Title=LTRIM(RTRIM(@Title)) and DistrictID=@DistrictID
 print @Count
 if(@Count=0) INSERT INTO tblThana (DistrictID,Title,EntryBy,EntryDate)
                                   VALUES (@DistrictID,@Title,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }



        public DataTable GetInformationById(string MId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ThanaID", MId));

            const string queryStr = @"SELECT  tblDistrict.DistrictID,tblAddressDivision.AddressDivisionID , * FROM tblThana
LEFT JOIN dbo.tblDistrict ON tblDistrict.DistrictID = tblThana.DistrictID
LEFT JOIN dbo.tblAddressDivision ON tblAddressDivision.AddressDivisionID = tblDistrict.DivisionID
 WHERE ThanaID = @ThanaID";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateEntryInfo(AddressThanaEntryDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@ThanaID", aEntryDao.ThanaID));
            aSqlParameterlist.Add(new SqlParameter("@DistrictID", aEntryDao.DistrictID));

            aSqlParameterlist.Add(new SqlParameter("@Title", aEntryDao.Title));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aEntryDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aEntryDao.UpdateDate));



            const string queryStr = @" 
Declare @Count Int

select @Count=count(*) from tblThana where Title=LTRIM(RTRIM(@Title)) and  DistrictID=@DistrictID and ThanaID not in (@ThanaID)
 print @Count
 if(@Count=0) UPDATE tblThana SET DistrictID=@DistrictID, Title = @Title, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate
                                    WHERE ThanaID = @ThanaID";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

     

        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ThanaID", Id));


            const string queryStr = @"DELETE FROM tblThana  WHERE ThanaID = @ThanaID";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

      


        public DataTable GetEntryformation( )
        {
            string queryStr = @"SELECT tblAddressDivision.Title AS DivisionName, tblDistrict.Titel AS DistrictName,  us.UserName EntryBy, usUp.UserName UpdateBy, * FROM tblThana MTb
left JOIN  dbo.tblUser us   ON  MTb.EntryBy =us.UserId  
left JOIN  dbo.tblUser usUp   ON  MTb.UpdateBy =usUp.UserId
LEFT JOIN dbo.tblDistrict ON tblDistrict.DistrictID = MTb.DistrictID
LEFT JOIN dbo.tblAddressDivision ON tblAddressDivision.AddressDivisionID = tblDistrict.DivisionID";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
