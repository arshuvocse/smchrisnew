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
    public class TraningInstitiueEntryDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


        public Int32 SaveInfoDEL(TraningInstitiueEntryDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@InstitutionID", aInformationDao.InstitutionID));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));


            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));


            const string queryStr = @"INSERT INTO DELtblEmpTrainingInstitution (InstitutionID,Description,EntryBy,EntryDate)
                                   VALUES (@InstitutionID,@Description,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public int SaveEntryInfo(TraningInstitiueEntryDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@Description", aVacancyEntryDao.Description));
        
          
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aVacancyEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aVacancyEntryDao.EntryDate));


            const string queryStr = @"INSERT INTO tblEmpTrainingInstitution (Description,EntryBy,EntryDate)
                                   VALUES (@Description,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }



        public DataTable GetInformationById(string MId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@InstitutionID", MId));

            const string queryStr = @"SELECT * FROM tblEmpTrainingInstitution WHERE InstitutionID = @InstitutionID";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateEntryInfo(TraningInstitiueEntryDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@InstitutionID", aEntryDao.InstitutionID));
            aSqlParameterlist.Add(new SqlParameter("@Description", aEntryDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aEntryDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aEntryDao.UpdateDate));



            const string queryStr = @"UPDATE tblEmpTrainingInstitution SET Description = @Description, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate
                                    WHERE InstitutionID = @InstitutionID";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

     

        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@InstitutionID", Id));


            const string queryStr = @"DELETE FROM tblEmpTrainingInstitution  WHERE InstitutionID = @InstitutionID";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

      


        public DataTable GetEntryformation( )
        {
            string queryStr = @"SELECT us.UserName EntryBy, usUp.UserName UpdateBy, * FROM tblEmpTrainingInstitution MTb
left JOIN  dbo.tblUser us   ON  MTb.EntryBy =us.UserId  
left JOIN  dbo.tblUser usUp   ON  MTb.UpdateBy =usUp.UserId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
