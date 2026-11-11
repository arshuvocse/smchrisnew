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

namespace DAL.Appraisal
{
   public  class AppraisalFunctionalPartDAL
    {
        ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        DataAccessManager _aAcessManager = new DataAccessManager();
       public DataTable GetEmployeeDetails(int id)
       {
           try
           {
               string query = @"SELECT R.EmpName AS ReportingToName,EGI.EmpInfoId,EGI.EmpName, EGI.DateOfJoin, CI.CompanyId, CI.CompanyName, DSN.DivisionId, DSN.DivisionName, DSNW.DivisionWId, DSNW.DivisionWingName,DPT.DepartmentId, DPT.DepartmentName,
                                    SEC.SectionId, SEC.SectionName, SSEC.SubSectionId, SSEC.SubSectionName, DSG.DesignationId, DSG.Designation,  ETP.EmpTypeId, ETP.EmpType, Jloc.Location

                                    FROM tblEmpGeneralInfo AS EGI 
                                    LEFT JOIN dbo.tblCompanyInfo AS CI ON EGI.CompanyId = CI.CompanyId
                                    LEFT JOIN dbo.tblDivision AS DSN ON EGI.DivisionId = DSN.DivisionId
                                    LEFT JOIN dbo.tblDivisionWing AS DSNW ON EGI.DivisionWId = DSNW.DivisionWId
                                    LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                                    LEFT JOIN dbo.tblSection AS SEC ON EGI.SectionId = SEC.SectionId
                                    LEFT JOIN dbo.tblSubSection AS SSEC ON EGI.SubSectionId = SSEC.SubSectionId
                                    LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
									LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
									LEFT JOIN dbo.tblJobLocation AS Jloc ON EGI.JobLocationId = Jloc.JobLocationID
									LEFT JOIN dbo.tblEmpGeneralInfo AS R ON EGI.ReportingEmpId = R.EmpInfoId
                                    where  EGI.EmpInfoId = " + id+"  AND EGI.IsActive=1 and EGI.EmployeeStatus='Active' ";
               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       }
       public bool UpdateStatus(string jobReqId, string status, string approveby, DateTime approvedate)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@AppraisalSelfMasterId", jobReqId));
           aSqlParameterlist.Add(new SqlParameter("@ActionStatus", status));
           aSqlParameterlist.Add(new SqlParameter("@ApproveBy", approveby));
           aSqlParameterlist.Add(new SqlParameter("@ApproveDate", approvedate));

           const string query = @"UPDATE dbo.tblAppraisalSelfMaster SET ActionStatus=@ActionStatus,ApproveBy=@ApproveBy,ApproveDate=@ApproveDate WHERE AppraisalSelfMasterId=@AppraisalSelfMasterId";
           return _aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
       }
       public DataTable GetAppraisalTraining(int id)
       {
           try
           {
               string query = @"SELECT 
       TrainingNeeds ,
       CONVERT(NVARCHAR(11) , TrainingStart , 106)TrainingStart ,
	   CONVERT(NVARCHAR(11) , TrainingEnd , 106)TrainingEnd
       	 FROM dbo.tblAppraisalTrainingNeeds WHERE AppraisalMasterId = " + id + "";
               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       

       public int SaveAppraisalMaster(AppraisalMaster aMaster, string user)
       {
           try
           {
               List<SqlParameter> aParameters = new List<SqlParameter>();

               aParameters.Add(new SqlParameter("@AppraisalMasterId", aMaster.AppraisalMasterId));
               aParameters.Add(new SqlParameter("@AppraisalSelfMasterId", aMaster.AppraisalSelfMasterId));
               aParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId));
               aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
               aParameters.Add(new SqlParameter("@EntryBy", user));
               aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));

               string delQuery = @"Delete from tblAppraisalMaster where AppraisalSelfMasterId = @AppraisalSelfMasterId ";
               bool r = _aCommonInternalDal.DeleteDataByDeleteCommand(delQuery, aParameters, DataBase.HRDB);

               string query = @"Insert into tblAppraisalMaster (EmpInfoId,FinancialYearId,EntryBy,EntryDate,AppraisalSelfMasterId,FYDes_App) values(@EmpInfoId,@FinancialYearId,@EntryBy,@EntryDate,@AppraisalSelfMasterId, (SELECT FinancialYearDesc 
                     FROM dbo.tblFinancialYear 
                     WHERE FinancialYearId =@FinancialYearId))";

               int pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
               return pk;
           }
           catch (Exception ex)
           {
                
               throw;
           }
       }


       public int SaveAppraisalSelfMaster(AppraisalMaster aMaster, string user)
       {
           try
           {
               if (aMaster.AppraisalMasterId > 0)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@AppraisalSelfMasterId", aMaster.AppraisalMasterId));
                   aParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId));
                   aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                   aParameters.Add(new SqlParameter("@UpdateBy", user));
                   aParameters.Add(new SqlParameter("@ActionStatus", "Drafted"));
                   aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));
                   string query = @"update tblAppraisalSelfMaster set FinancialYearId =@FinancialYearId  , EmpInfoId =@EmpInfoId  , UpdateBy =@UpdateBy  , UpdateDate =@UpdateDate,ActionStatus =@ActionStatus  where AppraisalSelfMasterId=@AppraisalSelfMasterId";

                   bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);

                   if (result == true)
                   {
                       return aMaster.AppraisalMasterId;
                   }
                   else
                   {
                       return 0;
                   }

               }
               else
               {
                   
              
               List<SqlParameter> aParameters = new List<SqlParameter>();

               aParameters.Add(new SqlParameter("@AppraisalSelfMasterId", aMaster.AppraisalMasterId));
               aParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId));
               aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
               aParameters.Add(new SqlParameter("@EntryBy", user));
               aParameters.Add(new SqlParameter("@ActionStatus", "Drafted"));
               aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));



               string query = @"Insert into tblAppraisalSelfMaster (EmpInfoId,FinancialYearId,EntryBy,EntryDate,ActionStatus,FYDes_Self) values(@EmpInfoId,@FinancialYearId,@EntryBy,@EntryDate,@ActionStatus, (SELECT FinancialYearDesc 
                     FROM dbo.tblFinancialYear 
                     WHERE FinancialYearId =@FinancialYearId))";

               int pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
               return pk;
               }
           }
           catch (Exception ex)
           {

               throw;
           }
       }


       public bool SaveAppraialFunctionalDetails(List<AppraisalFunctionalArea> aList, int masterid , int selfMaster)
       {
           try
           {
              
               string query2 = @"delete from tblAppraisalFuncArea where AppraisalSelfMasterId = "+selfMaster+"";
               bool a = _aCommonInternalDal.DeleteDataByDeleteCommand(query2, DataBase.HRDB);

               string query3 = @"delete from tblAppraisalSelfFuncArea where AppraisalSelfMasterId = " + selfMaster + "";
               bool asd = _aCommonInternalDal.DeleteDataByDeleteCommand(query3, DataBase.HRDB);

               bool result = false;
               foreach (var item in aList)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();

                   aParameters.Add(new SqlParameter("@AppraisalMasterId", masterid));
                   aParameters.Add(new SqlParameter("@AppraisalSelfMasterId", selfMaster));
                   aParameters.Add(new SqlParameter("@KpiInfo", item.KpiInfo));
                   aParameters.Add(new SqlParameter("@Deadline", item.Deadline));
                   aParameters.Add(new SqlParameter("@KpiWeight", item.KpiWeight));
                   aParameters.Add(new SqlParameter("@SelfMark", item.SelfMark));
                   aParameters.Add(new SqlParameter("@KpiWeightPer", item.KpiWeightPer));
                   aParameters.Add(new SqlParameter("@MidYearStatus", item.MidYearStatus));
                   aParameters.Add(new SqlParameter("@ResultYearEnd", item.ResultYearEnd));
                   aParameters.Add(new SqlParameter("@SupervisorMark", item.SupervisorMark));
                   aParameters.Add(new SqlParameter("@Target", item.Target));
                   aParameters.Add(new SqlParameter("@TargetPer", item.TargetPer));

                   string query = @"insert into tblAppraisalFuncArea(AppraisalMasterId, KpiInfo, KpiWeight, Deadline, MidYearStatus, ResultYearEnd,  SupervisorMark, Target,AppraisalSelfMasterId,KpiWeightPer,TargetPer) values(@AppraisalMasterId, @KpiInfo, @KpiWeight, @Deadline, @MidYearStatus, @ResultYearEnd,  @SupervisorMark, @Target,@AppraisalSelfMasterId,@KpiWeightPer,@TargetPer)";
                   result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);



                   string queryD = @"insert into tblAppraisalSelfFuncArea(AppraisalSelfMasterId, KpiInfo, KpiWeight, Deadline, MidYearStatus,  SelfMark, Target,KpiWeightPer,TargetPer) values(@AppraisalSelfMasterId, @KpiInfo, @KpiWeight, @Deadline, @MidYearStatus,  @SelfMark, @Target,@KpiWeightPer,@TargetPer)";
                   result = _aCommonInternalDal.SaveDataByInsertCommand(queryD, aParameters, DataBase.HRDB);



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


       public bool SaveAppraialSelfFunctionalDetails(List<AppraisalFunctionalArea> aList, int masterid)
       {
           try
           {
               List<SqlParameter> aParametersd = new List<SqlParameter>();
               aParametersd.Add(new SqlParameter("@AppraisalSelfMasterId", masterid));
               string queryDel = @"Delete from tblAppraisalSelfFuncArea where AppraisalSelfMasterId = @AppraisalSelfMasterId";

               bool delRes = _aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParametersd, DataBase.HRDB);
                

               bool result = false;
               foreach (var item in aList)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   
                   aParameters.Add(new SqlParameter("@AppraisalSelfMasterId", masterid));
                   aParameters.Add(new SqlParameter("@KpiInfo", item.KpiInfo));
                   aParameters.Add(new SqlParameter("@Deadline", item.Deadline));
                   aParameters.Add(new SqlParameter("@KpiWeight", item.KpiWeight));
                   aParameters.Add(new SqlParameter("@KpiWeightPer", item.KpiWeightPer));
                   aParameters.Add(new SqlParameter("@MidYearStatus", item.MidYearStatus));
                  
                   aParameters.Add(new SqlParameter("@SelfMark", item.SupervisorMark));
                   aParameters.Add(new SqlParameter("@Target", item.Target));
                   aParameters.Add(new SqlParameter("@TargetPer", item.TargetPer));



                   string query = @"insert into tblAppraisalSelfFuncArea(AppraisalSelfMasterId, KpiInfo, KpiWeight, Deadline, MidYearStatus,  SelfMark, Target,KpiWeightPer,TargetPer) values(@AppraisalSelfMasterId, @KpiInfo, @KpiWeight, @Deadline, @MidYearStatus,  @SelfMark, @Target,@KpiWeightPer,@TargetPer)";
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



       public DataTable GetSelfAppraisalList()
       {
           try
           {
               string query = @"select a.AppraisalSelfMasterId , (b.EmpMasterCode+':'+b.EmpName+':'+desg.Designation) employee ,dpt.DepartmentName ,c.CompanyName , y.FinancialYearDesc from  tblAppraisalSelfMaster A 
                left join tblEmpGeneralInfo B on a.EmpInfoId = b.EmpInfoId
                left join tblCompanyInfo c on b.CompanyId = c.CompanyId
                left join tblFinancialYear y on a.FinancialYearId = y.FinancialYearId
                left join tblDesignation desg on b.DesignationId = desg.DesignationId
                left join tblDepartment dpt on  b.departmentId = dpt.departmentId
                where (a.IsDelete is null or a.IsDelete = 0) and (a.ActionStatus ='Drafted') and ( a. ApproveFromSup = 0 or a. ApproveFromSup is null )";
               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception exception)
           {
               throw exception;
           }
       }


       public DataTable GetAppraisalByKpiPermission(string EmpID)
       {
           try
           {
               string query = @"SELECT  e.EmpInfoId ,
        ( e.EmpMasterCode + ':' + e.EmpName + ':' + desg.Designation ) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove
        FROM    dbo.tblKpiDeadlineMaster A
        LEFT JOIN dbo.tblKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN dbo.tblAppraisalMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId   WHERE b.EmpInfoId ='" + EmpID + "'"; 

               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {
               
               throw ex;
           }
       
       
       
       }

       public DataTable GetApprsaisalSelfByEmpFinYear(int emp, int year)
       {
           try
           {
               string query =@"	SELECT A.AppraisalSelfMasterId FROM dbo.tblAppraisalSelfMaster A WHERE a.EmpInfoId = " + emp +
                   " AND A.FinancialYearId = " + year + "";

               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {
                
               throw ex;
           }
       }
       public DataTable GetSelfAppraisalListApprove(string actionstatus)
       {
           try
           {
               string query = @"select A.EmpInfoId,A.FinancialYearId,a.AppraisalSelfMasterId , (b.EmpMasterCode+':'+b.EmpName+':'+desg.Designation) employee ,dpt.DepartmentName ,c.CompanyName , y.FinancialYearDesc from  tblAppraisalSelfMaster A 
                left join tblEmpGeneralInfo B on a.EmpInfoId = b.EmpInfoId
                left join tblCompanyInfo c on b.CompanyId = c.CompanyId
                left join tblFinancialYear y on a.FinancialYearId = y.FinancialYearId
                left join tblDesignation desg on b.DesignationId = desg.DesignationId
                left join tblDepartment dpt on  b.departmentId = dpt.departmentId
                where (a.IsDelete is null or a.IsDelete = 0) and  A.ActionStatus='" + actionstatus + "'";
               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception exception)
           {

               throw exception;
           }

       }
       public DataTable GetAppraisalSelf(int id)
       {
           try
           {
               string query = @"select A.* , (b.EmpMasterCode+':'+b.EmpName) as employee from tblAppraisalSelfMaster A 
                left join tblEmpGeneralInfo b on a.EmpInfoId = b.EmpInfoId 
                    where A.AppraisalSelfMasterId =  " + id + "";
               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

       public DataTable GetAppraisalSelfDetails(int id)
       {

           try
           {
               string query = @"SELECT  A.AppraisalSelfFucAreaId ,
        A.AppraisalSelfMasterId ,
        A.KpiInfo ,
        A.KpiWeight ,
        A.KpiWeightPer ,
        CONVERT(NVARCHAR(11), A.Deadline, 106) AS Deadline ,
        null as  MidYearStatus ,
        null as  ResultYearEnd ,
        A.SelfMark ,
       
        A.Target,
        A.TargetPer,
		null as  SupervisorMark
FROM    tblAppraisalSelfFuncArea A

WHERE   A.AppraisalSelfMasterId = " + id + "";
               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {
                
               throw ex;
           }
       }



       public DataTable GetAppraisalfDetailsFromSup(int id)
       {

           try
           {
               string query = @"SELECT  
        A.AppraisalSelfMasterId ,
        A.KpiInfo ,
        A.KpiWeight ,
        A.KpiWeightPer ,
        CONVERT(NVARCHAR(11), A.Deadline, 106) AS Deadline ,
        a.MidYearStatus ,
        a. ResultYearEnd ,
        A.SelfMark ,
       
        A.Target,
        A.TargetPer,
		A.SupervisorMark
FROM    tblAppraisalFuncArea A

WHERE   A.AppraisalSelfMasterId = " + id + "";
               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

       public DataTable GetAppraisalSelfB(int id)
       {
           try
           {
               string query = @"select * from tblAppraisalSelfBehaveArea where AppraisalSelfMasterId = " + id + " ";
               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {
                
               throw ex;
           }
       }

       public DataTable GetApparaisalSelfForAuth(int empInfo, int financila)
       {
           try
           {
               string query = @"select A.AppraisalSelfFucAreaId, A.AppraisalSelfMasterId, A.KpiInfo, A.KpiWeight, convert( nvarchar  (11),A.Deadline,106) as Deadline , A.MidYearStatus, A.ResultYearEnd, A.SelfMark, A.SupervisorMark, A.Target
   from tblAppraisalSelfFuncArea A left join tblAppraisalSelfMaster B on a.AppraisalSelfMasterId = b.AppraisalSelfMasterId
                where EmpInfoId = " + empInfo + " and FinancialYearId = " + financila + " ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           
           catch (Exception ex)
           {
                
               throw ex;
           }
       }

       public bool GoForReview(int finyear)
       {

           try
           {
               List<SqlParameter> aParameters = new List<SqlParameter>();
               aParameters.Add(new SqlParameter("@AppraisalSelfMasterId", finyear));
              

               string query =
                   @"update tblAppraisalSelfMaster set IsPublish = 0 where AppraisalSelfMasterId =@AppraisalSelfMasterId ";

               bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
               return result;
           }
           catch (Exception ex)
           {
                
               throw ex;
           }
       }


       public bool ApproveSup(int id)
       {
           try
           {
               List<SqlParameter> aParameters = new List<SqlParameter>();
               aParameters.Add(new SqlParameter("@AppraisalSelfMasterId", id));


               string query =
                   @"update tblAppraisalSelfMaster set ApproveFromSup = 1 where AppraisalSelfMasterId =@AppraisalSelfMasterId ";

               bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
               return result;
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

       
    }
}
