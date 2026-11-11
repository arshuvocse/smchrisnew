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
    public class EducationSubjectGroupEntryDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


        public Int32 SaveInfoDEL(EducationSubjectGroupEntryDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EducationSubjectGroupID", aInformationDao.EducationSubjectGroupID));
            aSqlParameterlist.Add(new SqlParameter("@Code", aInformationDao.Code));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));


            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));


            const string queryStr = @"INSERT INTO DELtblEducationSubjectGroup (EducationSubjectGroupID, Code , Description, EntryBy, EntryDate)
                                   VALUES (@EducationSubjectGroupID, @Code, @Description, @EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public int SaveEntryInfo(EducationSubjectGroupEntryDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@Code", aEntryDao.Code));
            aSqlParameterlist.Add(new SqlParameter("@Description", aEntryDao.Description));


            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aEntryDao.EntryDate));


            const string queryStr = @"INSERT INTO tblEducationSubjectGroup (Code, Description, EntryBy, EntryDate)
                                   VALUES (@Code, @Description, @EntryBy, @EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }



        public DataTable GetInformationById(string MId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EducationSubjectGroupID", MId));

            const string queryStr = @"SELECT * FROM tblEducationSubjectGroup WHERE EducationSubjectGroupID = @EducationSubjectGroupID";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateEntryInfo(EducationSubjectGroupEntryDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EducationSubjectGroupID", aEntryDao.EducationSubjectGroupID));
            aSqlParameterlist.Add(new SqlParameter("@Code", aEntryDao.Code));
            aSqlParameterlist.Add(new SqlParameter("@Description", aEntryDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aEntryDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aEntryDao.UpdateDate));



            const string queryStr = @"UPDATE tblEducationSubjectGroup SET Code = @Code, Description = @Description, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate
                                    WHERE EducationSubjectGroupID = @EducationSubjectGroupID";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

     

        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EducationSubjectGroupID", Id));


            const string queryStr = @"DELETE FROM tblEducationSubjectGroup  WHERE EducationSubjectGroupID = @EducationSubjectGroupID";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

      


        public DataTable GetEntryformation( )
        {
            string queryStr = @"SELECT us.UserName EntryBy, usUp.UserName UpdateBy, * FROM tblEducationSubjectGroup MTb
left JOIN  dbo.tblUser us   ON  MTb.EntryBy =us.UserId  
left JOIN  dbo.tblUser usUp   ON  MTb.UpdateBy =usUp.UserId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
