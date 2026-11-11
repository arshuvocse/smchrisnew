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
    public class EducationResultEntryDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


        public Int32 SaveInfoDEL(EducationResultEntryDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EducationResultID", aInformationDao.EducationResultID));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Code", aInformationDao.Code));


            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));


            const string queryStr = @"INSERT INTO DELtblEducationResult (EducationResultID,Code,Description,EntryBy,EntryDate)
                                   VALUES (@EducationResultID,@Code,@Description,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public int SaveEntryInfo(EducationResultEntryDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@Description", aVacancyEntryDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Code", aVacancyEntryDao.Code));

        
          
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aVacancyEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aVacancyEntryDao.EntryDate));


            const string queryStr = @"INSERT INTO tblEducationResult (Code,Description,EntryBy,EntryDate)
                                   VALUES (@Code, @Description,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }



        public DataTable GetInformationById(string MId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EducationResultID", MId));

            const string queryStr = @"SELECT * FROM tblEducationResult WHERE EducationResultID = @EducationResultID";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateEntryInfo(EducationResultEntryDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EducationResultID", aEntryDao.EducationResultID));
            aSqlParameterlist.Add(new SqlParameter("@Description", aEntryDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aEntryDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aEntryDao.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@Code", aEntryDao.Code));




            const string queryStr = @"UPDATE tblEducationResult SET Description = @Description, Code=@Code, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate
                                    WHERE EducationResultID = @EducationResultID";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

     

        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EducationResultID", Id));


            const string queryStr = @"DELETE FROM tblEducationResult  WHERE EducationResultID = @EducationResultID";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

      


        public DataTable GetEntryformation( )
        {
            string queryStr = @"SELECT us.UserName EntryBy, usUp.UserName UpdateBy, * FROM tblEducationResult MTb
left JOIN  dbo.tblUser us   ON  MTb.EntryBy =us.UserId  
left JOIN  dbo.tblUser usUp   ON  MTb.UpdateBy =usUp.UserId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
