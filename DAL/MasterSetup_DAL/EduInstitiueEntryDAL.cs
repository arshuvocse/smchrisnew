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
    public class EduInstitiueEntryDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


        public Int32 SaveInfoDEL(EduInstitiueEntryDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@InstitutionID", aInformationDao.InstitutionID));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Code", aInformationDao.Code));


            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));


            const string queryStr = @"INSERT INTO DELtblEducationalInstitution (InstitutionID,Code,Description,EntryBy,EntryDate)
                                   VALUES (@InstitutionID,@Code,@Description,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public int SaveEntryInfo(EduInstitiueEntryDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@Description", aVacancyEntryDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Code", aVacancyEntryDao.Code));

        
          
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aVacancyEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aVacancyEntryDao.EntryDate));


            const string queryStr = @" Declare @Count Int

select @Count=count(*) from tblEducationalInstitution where Description=LTRIM(RTRIM(@Description))
 
 if(@Count=0)
INSERT INTO tblEducationalInstitution (Code,Description,EntryBy,EntryDate)
                                   VALUES (@Code, @Description,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }



        public DataTable GetInformationById(string MId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@InstitutionID", MId));

            const string queryStr = @"SELECT * FROM tblEducationalInstitution WHERE InstitutionID = @InstitutionID";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateEntryInfo(EduInstitiueEntryDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@InstitutionID", aEntryDao.InstitutionID));
            aSqlParameterlist.Add(new SqlParameter("@Description", aEntryDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aEntryDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aEntryDao.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@Code", aEntryDao.Code));




            const string queryStr = @"
Declare @Count Int

select @Count=count(*) from tblEducationalInstitution where Description=LTRIM(RTRIM(@Description)) and InstitutionID not in (@InstitutionID)
 print @Count
 if(@Count=0) 

 UPDATE tblEducationalInstitution SET Description = @Description, Code=@Code, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate
                                    WHERE InstitutionID = @InstitutionID";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

     

        public bool DeleteEntryfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@InstitutionID", Id));


            const string queryStr = @"DELETE FROM tblEducationalInstitution  WHERE InstitutionID = @InstitutionID";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

      


        public DataTable GetEntryformation( )
        {
            string queryStr = @"SELECT us.UserName EntryBy, usUp.UserName UpdateBy, * FROM tblEducationalInstitution MTb with (nolock)
left JOIN  dbo.tblUser us   with (nolock) ON  MTb.EntryBy =us.UserId  
left JOIN  dbo.tblUser usUp   with (nolock)  ON  MTb.UpdateBy =usUp.UserId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
