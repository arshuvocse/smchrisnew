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
    public class EducationNameEntryDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


        public Int32 SaveInfoDEL(EducationNameEntryDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EducationNameID", aInformationDao.EducationNameID));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));


            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));


            const string queryStr = @"INSERT INTO DELtblEducationName (EducationNameID,Description,EntryBy,EntryDate)
                                   VALUES (@EducationNameID,@Description,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public int SaveEntryInfo(EducationNameEntryDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@Description", aVacancyEntryDao.Description));
        
          
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aVacancyEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aVacancyEntryDao.EntryDate));


            const string queryStr = @"INSERT INTO tblEducationName (Description,EntryBy,EntryDate)
                                   VALUES (@Description,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }



        public DataTable GetInformationById(string MId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EducationNameID", MId));

            const string queryStr = @"SELECT * FROM tblEducationName WHERE EducationNameID = @EducationNameID";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateEntryInfo(EducationNameEntryDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EducationNameID", aEntryDao.EducationNameID));
            aSqlParameterlist.Add(new SqlParameter("@Description", aEntryDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aEntryDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aEntryDao.UpdateDate));



            const string queryStr = @"UPDATE tblEducationName SET Description = @Description, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate
                                    WHERE EducationNameID = @EducationNameID";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

     

        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EducationNameID", Id));


            const string queryStr = @"DELETE FROM tblEducationName  WHERE EducationNameID = @EducationNameID";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

      


        public DataTable GetEntryformation( )
        {
            string queryStr = @"SELECT us.UserName EntryBy, usUp.UserName UpdateBy, * FROM tblEducationName MTb
left JOIN  dbo.tblUser us   ON  MTb.EntryBy =us.UserId  
left JOIN  dbo.tblUser usUp   ON  MTb.UpdateBy =usUp.UserId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
