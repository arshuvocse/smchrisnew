using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;
using DAO.MeetingMinorsDAO;

namespace DAL.Appraisal
{
   public  class OKRBSCAppraisalPartBDAL
    {
       ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();


       public bool SaveAppraisalPartB(List<AppraisalBehaveArea> appraisal  , int id)
       {
           try
           {

               string delQ = @"delete from tblBSCAppraisalBehaveArea where BSCAppraisalSelfMasterId = " + id + "";
               bool dd = _aCommonInternalDal.DeleteDataByDeleteCommand(delQ, DataBase.HRDB);

               bool result = false;

               foreach (var item in appraisal)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@AppraisalMasterId", item.AppraisalMasterId));
                   aParameters.Add(new SqlParameter("@AppraisalSelfMasterId", item.AppraisalSelfMasterId));
                   aParameters.Add(new SqlParameter("@SkillInfo", item.SkillInfo));
                   aParameters.Add(new SqlParameter("@SupportingEmp", item.SupportingEmp));
                   aParameters.Add(new SqlParameter("@Score", item.Score));
                   aParameters.Add(new SqlParameter("@SetScore", item.SetScore));
                   aParameters.Add(new SqlParameter("@SelfScore", item.SelfScore));
                   aParameters.Add(new SqlParameter("@SupervisorScore", item.SupervisorScore));
                   aParameters.Add(new SqlParameter("@Comments", (object)item.Comments ?? DBNull.Value));
                   aParameters.Add(new SqlParameter("@MidYearSuperScore", (object)item.MidYearSuperScore ?? DBNull.Value));

                   string query = @"Insert into tblBSCAppraisalBehaveArea ( SetScore, BSCAppraisalMasterId, SkillInfo, SupportingEmp, Score,BSCAppraisalSelfMasterId,SelfScore , SupervisorScore, Comments,MidYearSuperScore)
                        values (@SetScore, @AppraisalMasterId, @SkillInfo, @SupportingEmp, @Score,@AppraisalSelfMasterId,@SelfScore , @SupervisorScore, @Comments, @MidYearSuperScore)";

                   result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);
                   if (result == false)
                   {
                       return false;
                   }
               }
               return result;
           }
           catch (Exception ex)
           {

               throw;
           }

       }

       // Transaction-aware overload so the delete+insert of behavioral-area rows runs on the same
       // connection/transaction as SaveAppraisalMaster in btnBehave_OnClick - previously each insert
       // opened its own connection and committed independently, so a failure partway through the loop
       // could leave some rows saved and others not, with no way to roll back what had already committed.
       public bool SaveAppraisalPartB(SqlConnection connection, SqlTransaction transaction, List<AppraisalBehaveArea> appraisal, int id)
       {
           string delQ = @"delete from tblBSCAppraisalBehaveArea where BSCAppraisalSelfMasterId = " + id + "";
           ExecuteNonQuery(connection, transaction, delQ, new List<SqlParameter>());

           bool result = false;

           foreach (var item in appraisal)
           {
               List<SqlParameter> aParameters = new List<SqlParameter>();
               aParameters.Add(new SqlParameter("@AppraisalMasterId", item.AppraisalMasterId));
               aParameters.Add(new SqlParameter("@AppraisalSelfMasterId", item.AppraisalSelfMasterId));
               aParameters.Add(new SqlParameter("@SkillInfo", item.SkillInfo));
               aParameters.Add(new SqlParameter("@SupportingEmp", item.SupportingEmp));
               aParameters.Add(new SqlParameter("@Score", item.Score));
               aParameters.Add(new SqlParameter("@SetScore", item.SetScore));
               aParameters.Add(new SqlParameter("@SelfScore", item.SelfScore));
               aParameters.Add(new SqlParameter("@SupervisorScore", item.SupervisorScore));
               aParameters.Add(new SqlParameter("@Comments", (object)item.Comments ?? DBNull.Value));
               aParameters.Add(new SqlParameter("@MidYearSuperScore", (object)item.MidYearSuperScore ?? DBNull.Value));

               string query = @"Insert into tblBSCAppraisalBehaveArea ( SetScore, BSCAppraisalMasterId, SkillInfo, SupportingEmp, Score,BSCAppraisalSelfMasterId,SelfScore , SupervisorScore, Comments,MidYearSuperScore)
                    values (@SetScore, @AppraisalMasterId, @SkillInfo, @SupportingEmp, @Score,@AppraisalSelfMasterId,@SelfScore , @SupervisorScore, @Comments, @MidYearSuperScore)";

               int inserted = ExecuteNonQuery(connection, transaction, query, aParameters);
               result = inserted > 0;
               if (!result)
               {
                   return false;
               }
           }
           return result;
       }

       private static int ExecuteNonQuery(SqlConnection connection, SqlTransaction transaction, string queryString, List<SqlParameter> aSqlParameterlist)
       {
           using (SqlCommand command = new SqlCommand(queryString, connection, transaction))
           {
               command.Parameters.AddRange(aSqlParameterlist.ToArray());
               return command.ExecuteNonQuery();
           }
       }



       public bool SaveAppraisalPartBHR(List<AppraisalBehaveArea> appraisal  , int id)
       {
           try
           {

               string delQ = @"

INSERT INTO [dbo].[tblBSCAppraisalBehaveAreaHR]
           ([BSCAppraisalMasterId]
           ,[SkillInfo]
           ,[SupportingEmp]
           ,[Score]
           ,[BSCAppraisalSelfMasterId]
           ,[SupervisorScore]
           ,[SelfScore]
           ,[SetScore]
           ,[Comments]
           ,[MidYearSuperScore])
   select [BSCAppraisalMasterId]
           ,[SkillInfo]
           ,[SupportingEmp]
           ,[Score]
           ,[BSCAppraisalSelfMasterId]
           ,[SupervisorScore]
           ,[SelfScore]
           ,[SetScore]
           ,[Comments]
           ,[MidYearSuperScore] from tblBSCAppraisalBehaveArea where  BSCAppraisalSelfMasterId = "  + id + @"
delete from tblBSCAppraisalBehaveArea where BSCAppraisalSelfMasterId = " + id + "";
               bool dd = _aCommonInternalDal.DeleteDataByDeleteCommand(delQ, DataBase.HRDB);

               bool result = false;

               foreach (var item in appraisal)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@AppraisalMasterId", item.AppraisalMasterId));
                   aParameters.Add(new SqlParameter("@AppraisalSelfMasterId", item.AppraisalSelfMasterId));
                   aParameters.Add(new SqlParameter("@SkillInfo", item.SkillInfo));
                   aParameters.Add(new SqlParameter("@SupportingEmp", item.SupportingEmp));
                   aParameters.Add(new SqlParameter("@Score", item.Score));
                   aParameters.Add(new SqlParameter("@SetScore", item.SetScore));
                   aParameters.Add(new SqlParameter("@SelfScore", item.SelfScore));
                   aParameters.Add(new SqlParameter("@SupervisorScore", item.SupervisorScore));
                   aParameters.Add(new SqlParameter("@Comments", (object)item.Comments ?? DBNull.Value));
                   aParameters.Add(new SqlParameter("@MidYearSuperScore", (object)item.MidYearSuperScore ?? DBNull.Value));

                   string query = @"Insert into tblBSCAppraisalBehaveArea ( SetScore, BSCAppraisalMasterId, SkillInfo, SupportingEmp, Score,BSCAppraisalSelfMasterId,SelfScore , SupervisorScore, Comments,MidYearSuperScore)
                        values (@SetScore, @AppraisalMasterId, @SkillInfo, @SupportingEmp, @Score,@AppraisalSelfMasterId,@SelfScore , @SupervisorScore, @Comments, @MidYearSuperScore)";

                   result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);
                   if (result == false)
                   {
                       return false;
                   }
               }
               return result;
           }
           catch (Exception ex)
           {
                
               throw;
           }
           
       }




       public bool SaveAppraisalSelfPartB(List<AppraisalBehaveArea> appraisal)
       {
           try
           {
               bool result = false;

                List<SqlParameter> aParameters2 = new List<SqlParameter>();
                   aParameters2.Add(new SqlParameter("@AppraisalSelfMasterId", appraisal.First().AppraisalMasterId));
               string queryDel = @"Delete from tblAppraisalSelfBehaveArea where AppraisalSelfMasterId = @AppraisalSelfMasterId";

               bool delQ = _aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParameters2, DataBase.HRDB);
               foreach (var item in appraisal)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@AppraisalSelfMasterId", item.AppraisalMasterId));
                   aParameters.Add(new SqlParameter("@SkillInfo", item.SkillInfo));
                   aParameters.Add(new SqlParameter("@SupportingEmp", item.SupportingEmp));
                   aParameters.Add(new SqlParameter("@Score", item.Score));
                   aParameters.Add(new SqlParameter("@SetScore", item.SetScore));

                   string query = @"Insert into tblAppraisalSelfBehaveArea ( AppraisalSelfMasterId, SkillInfo, SupportingEmp, Score, SetScore)
                        values ( @AppraisalSelfMasterId, @SkillInfo, @SupportingEmp, @Score, @SetScore)";

                   result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);
                   if (result == false)
                   {
                       return false;
                   }
               }
               return result;
           }
           catch (Exception ex)
           {

               throw;
           }

       }

       public bool SaveAppraisalSelfPartBSkillType(List<AppraisalBehaveArea> appraisal)
       {
           try
           {
               bool result = false;

               List<SqlParameter> aParameters2 = new List<SqlParameter>();
               aParameters2.Add(new SqlParameter("@AppraisalSelfMasterId", appraisal.First().AppraisalMasterId));
               string queryDel = @"Delete from tblAppraisalSelfBehaveArea where AppraisalSelfMasterId = @AppraisalSelfMasterId";

               bool delQ = _aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParameters2, DataBase.HRDB);
               foreach (var item in appraisal)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@AppraisalSelfMasterId", item.AppraisalMasterId));
                   aParameters.Add(new SqlParameter("@SkillInfo", item.SkillInfo));
                   aParameters.Add(new SqlParameter("@SupportingEmp", item.SupportingEmp));
                   aParameters.Add(new SqlParameter("@Score", item.Score));
                   aParameters.Add(new SqlParameter("@SetScore", item.SetScore));
                   aParameters.Add(new SqlParameter("@SkillType", item.SkillType));

                   string query = @"Insert into tblAppraisalSelfBehaveArea ( AppraisalSelfMasterId, SkillInfo, SupportingEmp, Score, SetScore,SkillType)
                        values ( @AppraisalSelfMasterId, @SkillInfo, @SupportingEmp, @Score, @SetScore, @SkillType)";

                   result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);
                   if (result == false)
                   {
                       return false;
                   }
               }
               return result;
           }
           catch (Exception ex)
           {

               throw;
           }

       }
       public DataTable GetDocDataById(string ID)
       {
           try
           {
               string query = @"	SELECT  *
  FROM [dbo].[tblBSCOKRAppraisalInfoDocument] WITH (NOLOCK) WHERE BSCAppraisalMasterId=" +
                              ID;

               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public bool SaveDocumentDetails(List<AppraisalInfoDocumentDAO> aList, int masterid)
       {
           // Delete + re-insert of the document rows must run as one unit: the old code ran the delete
           // and each insert on its own connection via ClsCommonInternalDAL, so a failure partway through
           // the insert loop left the master's documents deleted with only some rows re-inserted. Everything
           // below now runs on a single SqlConnection/SqlTransaction so a failure anywhere rolls back the whole save.
           using (SqlConnection connection = new SqlConnection(DAL.MAIN_FUNCTION.DB_Connection.GenerateString(DataBase.H)))
           {
               connection.Open();
               using (SqlTransaction transaction = connection.BeginTransaction())
               {
                   try
                   {
                       List<SqlParameter> aParametersd = new List<SqlParameter>();
                       aParametersd.Add(new SqlParameter("@AppraisalMasterId", masterid));
                       string queryDel = @"DELETE FROM [dbo].[tblBSCOKRAppraisalInfoDocument]
      WHERE  BSCAppraisalMasterId=@AppraisalMasterId";

                       ExecuteNonQuery(connection, transaction, queryDel, aParametersd);

                       bool result = false;
                       foreach (var item in aList)
                       {
                           List<SqlParameter> aParameters = new List<SqlParameter>();

                           aParameters.Add(new SqlParameter("@AppraisalMasterId", masterid));
                           aParameters.Add(new SqlParameter("@DocumentLink", item.DocumentLink));
                           aParameters.Add(new SqlParameter("@DocumentNote", item.DocumentNote));
                           aParameters.Add(new SqlParameter("@FileName", item.FileName));

                           string query = @"INSERT INTO [dbo].[tblBSCOKRAppraisalInfoDocument]
           ([BSCAppraisalMasterId]
           ,[DocumentLink]
           ,[DocumentNote]
           ,[FileName])
     VALUES
           (@AppraisalMasterId
           ,@DocumentLink
           ,@DocumentNote
           ,@FILENAME)";
                           int rows = ExecuteNonQuery(connection, transaction, query, aParameters);
                           result = rows > 0;

                           if (result == false)
                           {
                               transaction.Rollback();
                               return false;
                           }
                       }

                       transaction.Commit();
                       return result;
                   }
                   catch (Exception)
                   {
                       transaction.Rollback();
                       throw;
                   }
               }
           }
       }
       public bool SaveTrainingNeeds(List<AppraisalTrainingNeeds> aList)
       {
           try
           {
               bool result = false;
               string delQ = "delete from tblBSCAppraisalTrainingNeeds where BSCAppraisalMasterId = " +
                             aList[0].AppraisalMasterId + " ";
               bool delQd = _aCommonInternalDal.DeleteDataByDeleteCommand(delQ, DataBase.HRDB);
               foreach (var item in aList)
               {

                   List<SqlParameter> aParameters = new List<SqlParameter>();

                   aParameters.Add(new SqlParameter("@TrainingNeeds", item.TrainingNeeds));
                   aParameters.Add(new SqlParameter("@AppraisalMasterId", item.AppraisalMasterId));
                   aParameters.Add(new SqlParameter("@TrainingStart", item.TrainingStart));
                   aParameters.Add(new SqlParameter("@TrainingEnd", item.TrainingEnd));
                   aParameters.Add(new SqlParameter("@Quater", item.Quater));
                   string query = @"insert into tblBSCAppraisalTrainingNeeds(BSCAppraisalMasterId, TrainingNeeds, TrainingStart, TrainingEnd,Quater) values(@AppraisalMasterId, @TrainingNeeds, @TrainingStart, @TrainingEnd,@Quater)";
                   result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);
                   if (result == false)
                   {
                       return false;
                   }

               }
               return result;
           }
           catch (Exception exception)
           {

               throw;
           }
       }


       // Transaction-aware overload so the delete+insert of training-needs rows runs on the same
       // connection/transaction as the caller (btnTrainSave_OnClick), instead of each call opening its
       // own connection and committing independently, which could leave a partial delete/insert on failure.
       public bool SaveTrainingNeeds(SqlConnection connection, SqlTransaction transaction, List<AppraisalTrainingNeeds> aList)
       {
           string delQ = "delete from tblBSCAppraisalTrainingNeeds where BSCAppraisalMasterId = " +
                         aList[0].AppraisalMasterId + " ";
           ExecuteNonQuery(connection, transaction, delQ, new List<SqlParameter>());

           bool result = false;
           foreach (var item in aList)
           {
               List<SqlParameter> aParameters = new List<SqlParameter>();
               aParameters.Add(new SqlParameter("@TrainingNeeds", item.TrainingNeeds));
               aParameters.Add(new SqlParameter("@AppraisalMasterId", item.AppraisalMasterId));
               aParameters.Add(new SqlParameter("@TrainingStart", item.TrainingStart));
               aParameters.Add(new SqlParameter("@TrainingEnd", item.TrainingEnd));
               aParameters.Add(new SqlParameter("@Quater", item.Quater));
               string query = @"insert into tblBSCAppraisalTrainingNeeds(BSCAppraisalMasterId, TrainingNeeds, TrainingStart, TrainingEnd,Quater) values(@AppraisalMasterId, @TrainingNeeds, @TrainingStart, @TrainingEnd,@Quater)";

               int inserted = ExecuteNonQuery(connection, transaction, query, aParameters);
               result = inserted > 0;
               if (!result)
               {
                   return false;
               }
           }
           return result;
       }

       // Transactional: the archive+delete batch and the re-insert must succeed or fail together.
       // Previously each ran on its own connection via ClsCommonInternalDAL, so a failure on the
       // insert left the master's final-status row archived and deleted with nothing re-inserted.
       // Both statements now run on a single SqlConnection/SqlTransaction, matching the pattern
       // used by SaveDocumentDetails in this same file.
       public bool SaaveFinalStatus(AppraisalFinalStatus astatus)
       {
           using (SqlConnection connection = new SqlConnection(DAL.MAIN_FUNCTION.DB_Connection.GenerateString(DataBase.H)))
           {
               connection.Open();
               using (SqlTransaction transaction = connection.BeginTransaction())
               {
                   try
                   {
                       string delquery = @"
INSERT INTO [dbo].[tblBSCAppraisalFinalStatusHR]
           ([BSCAppraisalMasterId]
           ,[TotalScore]
           ,[FinalStatus]
           ,[GeneralIncrement]
           ,[SpecialIncrement]
           ,[SpecialStep]
           ,[SpecialStepPercent]
           ,[IsPromotion]
           ,[Pip]
           ,[Other]
           ,[Note]
           ,[Justification]
           ,[DocumentLink]
           ,[FileName])
    select  [BSCAppraisalMasterId]
           ,[TotalScore]
           ,[FinalStatus]
           ,[GeneralIncrement]
           ,[SpecialIncrement]
           ,[SpecialStep]
           ,[SpecialStepPercent]
           ,[IsPromotion]
           ,[Pip]
           ,[Other]
           ,[Note]
           ,[Justification]
           ,[DocumentLink]
           ,[FileName] from tblBSCAppraisalFinalStatus  where BSCAppraisalMasterId =   " + astatus.BSCAppraisalMasterId + @"

Delete from tblBSCAppraisalFinalStatus where  BSCAppraisalMasterId = " + astatus.BSCAppraisalMasterId + " ";

                       ExecuteNonQuery(connection, transaction, delquery, new List<SqlParameter>());

                       List<SqlParameter> aParameters = new List<SqlParameter>();
                       aParameters.Add(new SqlParameter("@AppraisalMasterId", (object)astatus.BSCAppraisalMasterId ?? DBNull.Value));
                       aParameters.Add(new SqlParameter("@FinalStatus", (object)astatus.FinalStatus ?? DBNull.Value));
                       aParameters.Add(new SqlParameter("@Justification", (object)astatus.Justification ?? DBNull.Value));
                       aParameters.Add(new SqlParameter("@TotalScore", (object)astatus.TotalScore ?? DBNull.Value));
                       aParameters.Add(new SqlParameter("@GeneralIncrement", (object)astatus.GeneralIncrement ?? DBNull.Value));
                       aParameters.Add(new SqlParameter("@SpecialIncrement", (object)astatus.SpecialIncrement ?? DBNull.Value));
                       aParameters.Add(new SqlParameter("@SpecialStep", (object)astatus.SpecialStep ?? DBNull.Value));
                       aParameters.Add(new SqlParameter("@IsPromotion", (object)astatus.IsPromotion ?? DBNull.Value));
                       aParameters.Add(new SqlParameter("@Other", (object)astatus.Other ?? DBNull.Value));
                       aParameters.Add(new SqlParameter("@Note", (object)astatus.Note ?? DBNull.Value));
                       aParameters.Add(new SqlParameter("@Pip", (object)astatus.Pip ?? DBNull.Value));

                       aParameters.Add(new SqlParameter("@DocumentLink", (object)astatus.DocumentLink ?? DBNull.Value));
                       aParameters.Add(new SqlParameter("@FileName", (object)astatus.FileName ?? DBNull.Value));

                       string query = @"Insert into tblBSCAppraisalFinalStatus
                            (BSCAppraisalMasterId, TotalScore, FinalStatus, GeneralIncrement, SpecialIncrement,SpecialStep, IsPromotion, Pip , Other , Note, Justification,DocumentLink,FileName)
                            values(@AppraisalMasterId, @TotalScore, @FinalStatus, @GeneralIncrement, @SpecialIncrement, @SpecialStep, @IsPromotion, @Pip , @Other , @Note,@Justification,@DocumentLink,@FileName)";
                       int inserted = ExecuteNonQuery(connection, transaction, query, aParameters);
                       bool result = inserted > 0;

                       if (!result)
                       {
                           transaction.Rollback();
                           return false;
                       }

                       transaction.Commit();
                       return result;
                   }
                   catch (Exception)
                   {
                       transaction.Rollback();
                       throw;
                   }
               }
           }
       }

       public DataTable GetAppraiSalFinalStatus(int id)
       {
           try
           {
               string query = @"SELECT BSCApprisalFinalStatusId ,
       BSCAppraisalMasterId ,
       TotalScore ,
       FinalStatus ,
       GeneralIncrement ,
       SpecialIncrement ,
       SpecialStep ,
       SpecialStepPercent ,
       IsPromotion ,
    other,
Note,
       Pip , Justification,DocumentLink,FileName from tblBSCAppraisalFinalStatus where BSCAppraisalMasterId= " + id + " ";

              return  _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }

       public DataTable GetAppraiSalOKRBSCInfo(int FinancialYearId, int EmpinfoId)
       {
           try
           {
               string query = @" SELECT distinct  dd.OptionInfo     FROM dbo.tblBSCAppraisalDeadlineMaster dM    INNER JOIN dbo.tblBSCAppraisalDeadLineDetails dd ON dm.BSCAppraisalDeadLineMasterId = dd.BSCAppraisalDeadLineMasterId    WHERE dd.EmpinfoId="  + EmpinfoId +  "  and FinancialYearId=" + FinancialYearId + "   ";

              return  _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }


       public DataTable GetAppraiSalFinalStatusrpt(int id)
       {
           try
           {
               string query = @"SELECT mas.ApprisalFinalStatusId ,
       mas.AppraisalMasterId ,
       mas.TotalScore ,
       mas.FinalStatus ,
       mas.GeneralIncrement ,
       mas.SpecialIncrement ,
       isnull(mas.SpecialStep,0)  SpecialStep,
       mas.SpecialStepPercent ,
       mas.IsPromotion ,
    mas.other,
mas.Note,
       mas.Pip, func.SupervisorMark funcMark, behave.SupervisorScore behaveMark from tblAppraisalFinalStatus mas
	     LEFT JOIN ( SELECT  SUM(SupervisorMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON mas.AppraisalMasterId = func.AppraisalMasterId

				    LEFT JOIN ( SELECT  SUM(SupervisorScore) SupervisorScore ,
                            AppraisalMasterId
                    FROM    dbo.tblAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON mas.AppraisalMasterId = behave.AppraisalMasterId 
	    where mas.AppraisalMasterId=" + id + " ";

               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }


       public DataTable GetAppraiSalFinalStatusrptSelf(int id)
       {
           try
           {
               string query = @"SELECT 0 ApprisalFinalStatusId ,
       mas.AppraisalMasterId ,
       0 TotalScore ,
       '' FinalStatus ,
      cast (0 as bit)  GeneralIncrement ,
        cast (0 as bit)  SpecialIncrement ,
        0  SpecialStep ,
        cast (0 as bit)  SpecialStepPercent ,
        cast (0 as bit)  IsPromotion ,
     cast (0 as bit)  other,
'' Note,
        cast (0 as bit)  Pip, func.SupervisorMark funcMark, behave.SupervisorScore behaveMark from tblAppraisalMaster mas
	     LEFT JOIN ( SELECT  SUM(SelfMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON mas.AppraisalMasterId = func.AppraisalMasterId

				    LEFT JOIN ( SELECT  SUM(SelfScore) SupervisorScore ,
                            AppraisalMasterId
                    FROM    dbo.tblAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON mas.AppraisalMasterId = behave.AppraisalMasterId 
	    where mas.AppraisalMasterId=" + id + " ";

               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }


       public DataTable GetAppraiSalFinalStatusrptSelf_KPI(int id)
       {
           try
           {
               string query = @"SELECT 0 ApprisalFinalStatusId ,
       0 AppraisalMasterId ,
       0 TotalScore ,
       'ss' FinalStatus ,
      cast (0 as bit)  GeneralIncrement ,
        cast (0 as bit)  SpecialIncrement ,
       0  SpecialStep ,
        cast (0 as bit)  SpecialStepPercent ,
        cast (0 as bit)  IsPromotion ,
     cast (0 as bit)  other,
's' Note,
        cast (0 as bit)  Pip, func.SupervisorMark funcMark, behave.SupervisorScore behaveMark from tblAppraisalSelfMaster mas
	     LEFT JOIN ( SELECT  SUM(CAST(ISNULL(0,0) as int)) SupervisorMark ,
                            AppraisalSelfMasterId
                    FROM    tblAppraisalSelfBehaveArea
                    GROUP BY AppraisalSelfMasterId
                  ) func ON mas.AppraisalSelfMasterId = func.AppraisalSelfMasterId

				    LEFT JOIN ( SELECT  SUM(0) SupervisorScore ,
                            AppraisalSelfMasterId
                    FROM    dbo.tblAppraisalSelfFuncArea
                    GROUP BY AppraisalSelfMasterId
                  ) behave ON mas.AppraisalSelfMasterId = behave.AppraisalSelfMasterId 
	    where mas.AppraisalSelfMasterId=" + id + " ";

               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

    }
}
