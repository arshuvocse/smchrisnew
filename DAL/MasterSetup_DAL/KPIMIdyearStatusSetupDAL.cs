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
    public class KPIMIdyearStatusSetupDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


        public Int32 SaveInfoDEL(OccupationEntryDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@OccupationID", aInformationDao.OccupationID));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));


            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));


            const string queryStr = @"INSERT INTO DELtblOccupation (OccupationID,Description,EntryBy,EntryDate)
                                   VALUES (@OccupationID,@Description,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public int SaveEntryInfo(MidYearKPISetupDAO aKPIEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aKPIEntryDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", aKPIEntryDao.FinancialYearId));
            aSqlParameterlist.Add(new SqlParameter("@SelectedActionStatus", aKPIEntryDao.SelectedActionStatus));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aKPIEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aKPIEntryDao.EntryDate));

            const string queryStr = @"INSERT INTO tblMidYearKPISetup 
                              (CompanyId, FinancialYearId, SelectedActionStatus, EntryBy, EntryDate) 
                              VALUES 
                              (@CompanyId, @FinancialYearId, @SelectedActionStatus, @EntryBy, @EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }



        public DataTable GetInformationById(string comId, string FinId )
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@comId", comId));
            aSqlParameterlist.Add(new SqlParameter("@FinId", FinId));

            const string queryStr = @"SELECT * FROM tblMidYearKPISetup WHERE CompanyId = @comId and FinancialYearId=@FinId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateEntryInfo(MidYearKPISetupDAO aKPIEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@MidYearKPISetupId", aKPIEntryDao.MidYearKPISetupId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aKPIEntryDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", aKPIEntryDao.FinancialYearId));
            aSqlParameterlist.Add(new SqlParameter("@SelectedActionStatus", aKPIEntryDao.SelectedActionStatus));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aKPIEntryDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aKPIEntryDao.UpdateDate));

            const string queryStr = @"UPDATE tblMidYearKPISetup 
                              SET 
                                  CompanyId = @CompanyId, 
                                  FinancialYearId = @FinancialYearId, 
                                  SelectedActionStatus = @SelectedActionStatus, 
                                  UpdateBy = @UpdateBy, 
                                  UpdateDate = @UpdateDate
                              WHERE 
                                  MidYearKPISetupId = @MidYearKPISetupId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }



        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@OccupationID", Id));


            const string queryStr = @"DELETE FROM tblOccupation  WHERE OccupationID = @OccupationID";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

      


        public DataTable GetEntryformation( )
        {
            string queryStr = @"SELECT us.UserName EntryBy, usUp.UserName UpdateBy, * FROM tblOccupation MTb
left JOIN  dbo.tblUser us   ON  MTb.EntryBy =us.UserId  
left JOIN  dbo.tblUser usUp   ON  MTb.UpdateBy =usUp.UserId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
