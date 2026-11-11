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
   public  class KPIMIDAppraisalPartBDAL
    {
       ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();


       public bool SaveAppraisalPartB(List<AppraisalBehaveArea> appraisal  , int id)
       {
           try
           {

               string delQ = @"delete from tblKPIMIDAppraisalBehaveArea where AppraisalSelfMasterId = " + id + "";
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

                   string query = @"Insert into tblKPIMIDAppraisalBehaveArea ( SetScore, AppraisalMasterId, SkillInfo, SupportingEmp, Score,AppraisalSelfMasterId,SelfScore , SupervisorScore, Comments)
                        values (@SetScore, @AppraisalMasterId, @SkillInfo, @SupportingEmp, @Score,@AppraisalSelfMasterId,@SelfScore , @SupervisorScore, @Comments)";

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
  FROM [dbo].[tblAppraisalInfoDocument] WITH (NOLOCK) WHERE AppraisalMasterId=" +
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
           try
           {
               List<SqlParameter> aParametersd = new List<SqlParameter>();
               aParametersd.Add(new SqlParameter("@AppraisalMasterId", masterid));
               string queryDel = @"DELETE FROM [dbo].[tblAppraisalInfoDocument]
      WHERE  AppraisalMasterId=@AppraisalMasterId";

               bool delRes = _aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParametersd, DataBase.HRDB);


               bool result = false;
               foreach (var item in aList)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();

                   aParameters.Add(new SqlParameter("@AppraisalMasterId", masterid));
                   aParameters.Add(new SqlParameter("@DocumentLink", item.DocumentLink));
                   aParameters.Add(new SqlParameter("@DocumentNote", item.DocumentNote));
                   aParameters.Add(new SqlParameter("@FileName", item.FileName));




                   string query = @"INSERT INTO [dbo].[tblAppraisalInfoDocument]
           ([AppraisalMasterId]
           ,[DocumentLink]
           ,[DocumentNote]
           ,[FileName])
     VALUES
           (@AppraisalMasterId
           ,@DocumentLink
           ,@DocumentNote
           ,@FILENAME)";
                   result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);

                   if (result == false)
                   {
                       return false;
                   }


               }
               return result;


           }
           catch (Exception x)
           {

               throw;
           }
       }
       public bool SaveTrainingNeeds(List<AppraisalTrainingNeeds> aList)
       {
           try
           {
               bool result = false;
               string delQ = "delete from tblAppraisalTrainingNeeds where AppraisalMasterId = " +
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
                   string query = @"insert into tblAppraisalTrainingNeeds(AppraisalMasterId, TrainingNeeds, TrainingStart, TrainingEnd,Quater) values(@AppraisalMasterId, @TrainingNeeds, @TrainingStart, @TrainingEnd,@Quater)";
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


       public bool SaaveFinalStatus(AppraisalFinalStatus astatus)
       {
           try
           {
               bool result = false;
               string delquery = @"Delete from tblAppraisalFinalStatus where  AppraisalMasterId = " + astatus.AppraisalMasterId + " ";

               bool del = _aCommonInternalDal.DeleteDataByDeleteCommand(delquery, DataBase.HRDB);
               List<SqlParameter> aParameters = new List<SqlParameter>();
               aParameters.Add(new SqlParameter("@AppraisalMasterId", (object)astatus.AppraisalMasterId ?? DBNull.Value));
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


               string query = @"Insert into tblAppraisalFinalStatus 
                            (AppraisalMasterId, TotalScore, FinalStatus, GeneralIncrement, SpecialIncrement,SpecialStep, IsPromotion, Pip , Other , Note, Justification,DocumentLink,FileName) 
                            values(@AppraisalMasterId, @TotalScore, @FinalStatus, @GeneralIncrement, @SpecialIncrement, @SpecialStep, @IsPromotion, @Pip , @Other , @Note,@Justification,@DocumentLink,@FileName)";
               result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);


               return result;

           }
           catch (Exception ex)
           {

               throw;
           }
       }

       public DataTable GetAppraiSalFinalStatus(int id)
       {
           try
           {
               string query = @"SELECT ApprisalFinalStatusId ,
       AppraisalMasterId ,
       TotalScore ,
       FinalStatus ,
       GeneralIncrement ,
       SpecialIncrement ,
       SpecialStep ,
       SpecialStepPercent ,
       IsPromotion ,
    other,
Note,
       Pip , Justification,DocumentLink,FileName from tblAppraisalFinalStatus where AppraisalMasterId= " + id + " ";

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
                    FROM    dbo.tblKPIMIDAppraisalBehaveArea
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

        

       public DataTable GetAppraiSalFinalStatusrptBSCOKR(int id)
       {
           try
           {
               string query = @"SELECT mas.BSCApprisalFinalStatusId ApprisalFinalStatusId ,
       mas.BSCAppraisalMasterId AppraisalMasterId,
       mas.TotalScore ,
       mas.FinalStatus ,
       mas.GeneralIncrement ,
       mas.SpecialIncrement ,
       isnull(mas.SpecialStep,0)  SpecialStep,
       mas.SpecialStepPercent ,
       mas.IsPromotion ,
    mas.other,
mas.Note,
       mas.Pip, func.SupervisorMark funcMark, behave.SupervisorScore behaveMark from tblBSCAppraisalFinalStatus mas
	     LEFT JOIN ( SELECT  SUM(SupervisorMark) * 0.75 SupervisorMark ,
                            BSCAppraisalMasterId
                    FROM    tblBSCAppraisalFuncArea
                    GROUP BY BSCAppraisalMasterId
                  ) func ON mas.BSCAppraisalMasterId = func.BSCAppraisalMasterId

				    LEFT JOIN ( SELECT  SUM(SupervisorScore) SupervisorScore ,
                            BSCAppraisalMasterId
                    FROM    dbo.tblBSCAppraisalBehaveArea
                    GROUP BY BSCAppraisalMasterId
                  ) behave ON mas.BSCAppraisalMasterId = behave.BSCAppraisalMasterId 
	    where mas.BSCAppraisalMasterId=" + id + " ";

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
                    FROM    dbo.tblKPIMIDAppraisalBehaveArea
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

        

       public DataTable GetAppraiSalFinalStatusrptSelfBSCOKR(int id)
       {
           try
           {
               string query = @"SELECT 0 ApprisalFinalStatusId ,
       mas.BSCAppraisalMasterId  AppraisalMasterId,
       0 TotalScore ,
       '' FinalStatus ,
      cast (0 as bit)  GeneralIncrement ,
        cast (0 as bit)  SpecialIncrement ,
        0  SpecialStep ,
        cast (0 as bit)  SpecialStepPercent ,
        cast (0 as bit)  IsPromotion ,
     cast (0 as bit)  other,
'' Note,
        cast (0 as bit)  Pip, func.SupervisorMark funcMark, behave.SupervisorScore behaveMark from tblBSCAppraisalMaster mas
	     LEFT JOIN ( SELECT  SUM(SelfMark)*0.75 SupervisorMark ,
                            BSCAppraisalMasterId
                    FROM    tblBSCAppraisalFuncArea
                    GROUP BY BSCAppraisalMasterId
                  ) func ON mas.BSCAppraisalMasterId = func.BSCAppraisalMasterId

				    LEFT JOIN ( SELECT  SUM(SelfScore) SupervisorScore ,
                            BSCAppraisalMasterId
                    FROM    dbo.tblBSCAppraisalBehaveArea
                    GROUP BY BSCAppraisalMasterId
                  ) behave ON mas.BSCAppraisalMasterId = behave.BSCAppraisalMasterId 
	    where mas.BSCAppraisalMasterId=" + id + " ";

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
             public DataTable GetAppraiSalFinalStatusrptSelf_KPIBSCOKR(int id)
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
        cast (0 as bit)  Pip, func.SupervisorMark funcMark, behave.SupervisorScore behaveMark from tblBSCAppraisalSelfMaster mas
	     LEFT JOIN ( SELECT  SUM(CAST(ISNULL(0,0) as int)) SupervisorMark ,
                            BSCAppraisalSelfMasterId
                    FROM    tblBSCAppraisalSelfBehaveArea
                    GROUP BY BSCAppraisalSelfMasterId
                  ) func ON mas.BSCAppraisalSelfMasterId = func.BSCAppraisalSelfMasterId

				    LEFT JOIN ( SELECT  SUM(0) SupervisorScore ,
                            BSCAppraisalSelfMasterId
                    FROM    dbo.tblBSCAppraisalSelfFuncArea
                    GROUP BY BSCAppraisalSelfMasterId
                  ) behave ON mas.BSCAppraisalSelfMasterId = behave.BSCAppraisalSelfMasterId 
	    where mas.BSCAppraisalSelfMasterId=" + id + " ";

               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

    }
}
