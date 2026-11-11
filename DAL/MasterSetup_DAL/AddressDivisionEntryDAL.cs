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
    public class AddressDivisionEntryDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


        public Int32 SaveInfoDEL(AddressDivisionEntryDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@AddressDivisionID", aInformationDao.AddressDivisionID));
            aSqlParameterlist.Add(new SqlParameter("@Title", aInformationDao.Title));


            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));


            const string queryStr = @"INSERT INTO DELtblAddressDivision (AddressDivisionID,Title,EntryBy,EntryDate)
                                   VALUES (@AddressDivisionID,@Title,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public int SaveEntryInfo(AddressDivisionEntryDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@Title", aVacancyEntryDao.Title));
        
          
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aVacancyEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aVacancyEntryDao.EntryDate));


            const string queryStr = @"  Declare @Count Int

select @Count=count(*) from tblAddressDivision where Title=LTRIM(RTRIM(@Title))
 
 if(@Count=0)
INSERT INTO tblAddressDivision (Title,EntryBy,EntryDate)
                                   VALUES (@Title,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }



        public DataTable GetInformationById(string MId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@AddressDivisionID", MId));

            const string queryStr = @"SELECT * FROM tblAddressDivision WHERE AddressDivisionID = @AddressDivisionID";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateEntryInfo(AddressDivisionEntryDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@AddressDivisionID", aEntryDao.AddressDivisionID));
            aSqlParameterlist.Add(new SqlParameter("@Title", aEntryDao.Title));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aEntryDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aEntryDao.UpdateDate));



            const string queryStr = @" 
Declare @Count Int

select @Count=count(*) from tblAddressDivision where Title=LTRIM(RTRIM(@Title)) and AddressDivisionID not in (@AddressDivisionID)
 print @Count
 if(@Count=0) UPDATE tblAddressDivision SET Title = @Title, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate
                                    WHERE AddressDivisionID = @AddressDivisionID";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

     

        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@AddressDivisionID", Id));


            const string queryStr = @"DELETE FROM tblAddressDivision  WHERE AddressDivisionID = @AddressDivisionID";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

      


        public DataTable GetEntryformation( )
        {
            string queryStr = @"SELECT us.UserName EntryBy, usUp.UserName UpdateBy, * FROM tblAddressDivision MTb
left JOIN  dbo.tblUser us   ON  MTb.EntryBy =us.UserId  
left JOIN  dbo.tblUser usUp   ON  MTb.UpdateBy =usUp.UserId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
