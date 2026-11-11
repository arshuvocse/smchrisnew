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
    public class FieldofspecializationEntryDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


        public Int32 SaveInfoDEL(FieldofspecializationEntryDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SpecializationID", aInformationDao.SpecializationID));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Code", aInformationDao.Code));


            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));


            const string queryStr = @"INSERT INTO DELtblSpecialization (SpecializationID,Code,Description,EntryBy,EntryDate)
                                   VALUES (@SpecializationID,@Code,@Description,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public int SaveEntryInfo(FieldofspecializationEntryDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@Description", aVacancyEntryDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Code", aVacancyEntryDao.Code));

        
          
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aVacancyEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aVacancyEntryDao.EntryDate));


            const string queryStr = @"INSERT INTO tblSpecialization (Code,Description,EntryBy,EntryDate)
                                   VALUES (@Code, @Description,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }



        public DataTable GetInformationById(string MId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SpecializationID", MId));

            const string queryStr = @"SELECT * FROM tblSpecialization WHERE SpecializationID = @SpecializationID";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateEntryInfo(FieldofspecializationEntryDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SpecializationID", aEntryDao.SpecializationID));
            aSqlParameterlist.Add(new SqlParameter("@Description", aEntryDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aEntryDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aEntryDao.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@Code", aEntryDao.Code));




            const string queryStr = @"UPDATE tblSpecialization SET Description = @Description, Code=@Code, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate
                                    WHERE SpecializationID = @SpecializationID";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

     

        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SpecializationID", Id));


            const string queryStr = @"DELETE FROM tblSpecialization  WHERE SpecializationID = @SpecializationID";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

      


        public DataTable GetEntryformation( )
        {
            string queryStr = @"SELECT us.UserName EntryBy, usUp.UserName UpdateBy, * FROM tblSpecialization MTb
left JOIN  dbo.tblUser us   ON  MTb.EntryBy =us.UserId  
left JOIN  dbo.tblUser usUp   ON  MTb.UpdateBy =usUp.UserId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
