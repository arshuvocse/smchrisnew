using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.MasterSetup_DAL
{
    public class TrainingTopicEntryDaL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public void GetTrainingHeadingList(DropDownList ddl)
        {
            string queryStr = "SELECT TraingingHeading  TraingingHeading ,*	 FROM tblTraingingHeading WHERE IsActive=1 and  (IsDelete IS NULL OR IsDelete=0)";

            //string queryStr = "SELECT CompanyId, CompanyName FROM tblCompanyInfo";
            aCommonInternalDal.LoadDropDownValue(ddl, "TraingingHeading", "TraingingHeadingId", queryStr, "HRDB");
        }
        public int SaveVacancyEntryInfo(TrainingTopicEntryDAO aSuspendReasonEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@TraingingHeadingId", aSuspendReasonEntryDao.TraingingHeadingId));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", aSuspendReasonEntryDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aSuspendReasonEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aSuspendReasonEntryDao.EntryDate));

            aSqlParameterlist.Add(new SqlParameter("@TrainingTopic", aSuspendReasonEntryDao.TrainingTopic));
            aSqlParameterlist.Add(new SqlParameter("@TraingingSerial", aSuspendReasonEntryDao.TraingingSerial));
           


            const string queryStr = @"INSERT INTO [dbo].[tblTrainingSetupTopic]
           ([TrainingTopic]
           ,[TraingingHeadingId]
           ,[IsActive]
           ,[EntryBy]
           ,[EntryDate],
TraingingSerial,
IsDelete
)
         
     VALUES
           (@TrainingTopic,  
            @TraingingHeadingId, 
         @IsActive,  
           @EntryBy, 
          @EntryDate,
@TraingingSerial,
0
         )";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }



        public DataTable GetVacaencyInformationById(string SuspendReasonEntryId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@TrainingTopicId", SuspendReasonEntryId));

            const string queryStr = @"SELECT * FROM tblTrainingSetupTopic WHERE TrainingTopicId = @TrainingTopicId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool UpdateVacancyEntryInfo(TrainingTopicEntryDAO aSuspendReasonEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@TrainingTopicId", aSuspendReasonEntryDao.TrainingTopicId));
            aSqlParameterlist.Add(new SqlParameter("@TrainingTopic", aSuspendReasonEntryDao.TrainingTopic));
            aSqlParameterlist.Add(new SqlParameter("@TraingingHeadingId", aSuspendReasonEntryDao.TraingingHeadingId));
            aSqlParameterlist.Add(new SqlParameter("@TraingingSerial", aSuspendReasonEntryDao.TraingingSerial));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aSuspendReasonEntryDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aSuspendReasonEntryDao.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aSuspendReasonEntryDao.IsActive));
        

            const string queryStr = @"UPDATE [dbo].[tblTrainingSetupTopic]
   SET [TrainingTopic] = @TrainingTopic, 
      [TraingingHeadingId] = @TraingingHeadingId,  
      [IsActive] = @IsActive, 
     
      [UpdateBy] = @UpdateBy ,
       [UpdateDate] = @UpdateDate, TraingingSerial=@TraingingSerial
 WHERE TrainingTopicId =@TrainingTopicId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }



        public bool DeleteVacancyEntryfoById(TrainingTopicEntryDAO aVacancyEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@TrainingTopicId", aVacancyEntryDao.TrainingTopicId));
         
            aSqlParameterlist.Add(new SqlParameter("@IsDelete", aVacancyEntryDao.IsDelete));
            aSqlParameterlist.Add(new SqlParameter("@DeleteBy", aVacancyEntryDao.DeleteBy));
            aSqlParameterlist.Add(new SqlParameter("@DeleteDate", aVacancyEntryDao.DeleteDate));

            const string queryStr = @"UPDATE tblTrainingSetupTopic SET IsDelete = @IsDelete,DeleteBy = @DeleteBy, DeleteDate=@DeleteDate WHERE TrainingTopicId = @TrainingTopicId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }


        public DataTable GetVacanceyEntryformationParam( )
        {
            string queryStr = @"SELECT H.TraingingHeading , U.UserName EntryBy, U2.UserName UpdateBy, T.* FROM tblTrainingSetupTopic T
LEFT JOIN tblTraingingHeading H ON T.TraingingHeadingId=H.TraingingHeadingId
LEFT JOIN dbo.tblUser U ON T.EntryBy=U.UserId
LEFT JOIN dbo.tblUser U2 ON T.UpdateBy=U2.UserId 
WHERE  (T.IsDelete IS NULL OR T.IsDelete=0)";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetEvaluationData(string id)
        {
            string queryStr = @"SELECT * FROM dbo.tblEvaluationFormMaster
            LEFT JOIN dbo.tblEvaluationFormDetails ON tblEvaluationFormDetails.EvaluationFormMasterId = tblEvaluationFormMaster.EvaluationFormMasterId
            WHERE TrainingTopicId='"+id+"'";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
    }
}
