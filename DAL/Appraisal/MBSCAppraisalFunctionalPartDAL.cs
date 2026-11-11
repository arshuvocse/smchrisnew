using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using System.Windows.Forms;

namespace DAL.Appraisal
{
    public class MBSCAppraisalFunctionalPartDAL
    {
        ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        DataAccessManager _aAcessManager = new DataAccessManager();

       
        public DataTable GetEmployeeDetails(int id)
        {
            try
            {
                string query = @"SELECT  sg.GradeCode+ isnull(' : '+sg.GradeName,'') GradeName, Stp.SalaryStepName, Sloc.SalaryLocation, R.EmpMasterCode+' : '+ R.EmpName+isnull(' : '+DSGR.Designation,'') AS ReportingToName,EGI.EmpInfoId,EGI.EmpName, EGI.DateOfJoin, CI.CompanyId, CI.CompanyName, DSN.DivisionId, DSN.DivisionName, DSNW.DivisionWId, DSNW.DivisionWingName,DPT.DepartmentId, DPT.DepartmentName,
                                    SEC.SectionId, SEC.SectionName, SSEC.SubSectionId, SSEC.SubSectionName, DSG.DesignationId, DSG.Designation,  ETP.EmpTypeId, ETP.EmpType, Jloc.Location, EGI.EmpMasterCode, * 

                                    FROM tblEmpGeneralInfo AS EGI with (nolock)
                                    LEFT JOIN dbo.tblCompanyInfo AS CI ON EGI.CompanyId = CI.CompanyId
                                    LEFT JOIN dbo.tblDivision AS DSN ON EGI.DivisionId = DSN.DivisionId
                                    LEFT JOIN dbo.tblDivisionWing AS DSNW ON EGI.DivisionWId = DSNW.DivisionWId
                                    LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                                    LEFT JOIN dbo.tblSection AS SEC ON EGI.SectionId = SEC.SectionId
                                    LEFT JOIN dbo.tblSubSection AS SSEC ON EGI.SubSectionId = SSEC.SubSectionId
                                    LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
									LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
									LEFT JOIN dbo.tblJobLocation AS Jloc ON EGI.JobLocationId = Jloc.JobLocationID
									LEFT JOIN dbo.tblSalaryLocation AS Sloc ON EGI.SalaryLoationId = Sloc.SalaryLoationId
									LEFT JOIN dbo.tblEmpGeneralInfo AS R ON EGI.ReportingEmpId = R.EmpInfoId

									 LEFT JOIN dbo.tblDesignation AS DSGR ON R.DesignationId = DSGR.DesignationId

									LEFT JOIN dbo.tblSalaryGrade AS sg ON sg.SalaryGradeId = EGI.SalaryGradeId
									LEFT JOIN dbo.tblSalaryStep AS Stp ON EGI.salarystepid = stp.salarystepid
                                    where  EGI.EmpInfoId = " + id + "   ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public DataTable GetEmployeeDetailsKPIREport(int id)
        {
            try
            {
                string query = @"SELECT     ISNULL(  ISNULL( dtEmployeePromotion.Effectivedate,dtEmployeePromotionHis.Effectivedate), EGI.DateOfJoin) ss, CAST((DATEDIFF(year, ISNULL(  ISNULL( dtEmployeePromotion.Effectivedate,dtEmployeePromotionHis.Effectivedate), EGI.DateOfJoin), GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, ISNULL(  ISNULL( dtEmployeePromotion.Effectivedate,dtEmployeePromotionHis.Effectivedate), EGI.DateOfJoin), GETDATE()), ISNULL(  ISNULL( dtEmployeePromotion.Effectivedate,dtEmployeePromotionHis.Effectivedate), EGI.DateOfJoin)) > GETDATE() THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +

CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, ISNULL(  ISNULL( dtEmployeePromotion.Effectivedate,dtEmployeePromotionHis.Effectivedate), EGI.DateOfJoin), GETDATE()), ISNULL(  ISNULL( dtEmployeePromotion.Effectivedate,dtEmployeePromotionHis.Effectivedate), EGI.DateOfJoin))) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, ISNULL(  ISNULL( dtEmployeePromotion.Effectivedate,dtEmployeePromotionHis.Effectivedate), EGI.DateOfJoin), GETDATE()), ISNULL(  ISNULL( dtEmployeePromotion.Effectivedate,dtEmployeePromotionHis.Effectivedate), EGI.DateOfJoin))) -(CASE WHEN DATEADD(year, DATEDIFF(year, ISNULL(  ISNULL( dtEmployeePromotion.Effectivedate,dtEmployeePromotionHis.Effectivedate), EGI.DateOfJoin), GETDATE()), ISNULL(  ISNULL( dtEmployeePromotion.Effectivedate,dtEmployeePromotionHis.Effectivedate), EGI.DateOfJoin)) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  ServiceLength, sg.GradeCode+ isnull(' : '+sg.GradeName,'') GradeName, Stp.SalaryStepName, Sloc.SalaryLocation, R.EmpName AS ReportingToName,EGI.EmpInfoId,EGI.EmpName, EGI.DateOfJoin, CI.CompanyId, CI.CompanyName, DSN.DivisionId, DSN.DivisionName, DSNW.DivisionWId, DSNW.DivisionWingName,DPT.DepartmentId, DPT.DepartmentName,
                                    SEC.SectionId, SEC.SectionName, SSEC.SubSectionId, SSEC.SubSectionName, DSG.DesignationId, DSG.Designation,  ETP.EmpTypeId, ETP.EmpType, Jloc.Location, EGI.EmpMasterCode, * 

                                    FROM tblEmpGeneralInfo AS EGI with (nolock)
                                    LEFT JOIN dbo.tblCompanyInfo AS CI ON EGI.CompanyId = CI.CompanyId
                                    LEFT JOIN dbo.tblDivision AS DSN ON EGI.DivisionId = DSN.DivisionId
                                    LEFT JOIN dbo.tblDivisionWing AS DSNW ON EGI.DivisionWId = DSNW.DivisionWId
                                    LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                                    LEFT JOIN dbo.tblSection AS SEC ON EGI.SectionId = SEC.SectionId
                                    LEFT JOIN dbo.tblSubSection AS SSEC ON EGI.SubSectionId = SSEC.SubSectionId
                                    LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
									LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
									LEFT JOIN dbo.tblJobLocation AS Jloc ON EGI.JobLocationId = Jloc.JobLocationID
									LEFT JOIN dbo.tblSalaryLocation AS Sloc ON EGI.SalaryLoationId = Sloc.SalaryLoationId
									LEFT JOIN dbo.tblEmpGeneralInfo AS R ON EGI.ReportingEmpId = R.EmpInfoId
									LEFT JOIN dbo.tblSalaryGrade AS sg ON sg.SalaryGradeId = EGI.SalaryGradeId
									LEFT JOIN dbo.tblSalaryStep AS Stp ON EGI.salarystepid = stp.salarystepid

 
     left JOIN (SELECT EmployeeId,MAX(Effectivedate)AS Effectivedate  FROM  dbo.tblEmployeePromotionEntry GROUP BY EmployeeId
 
   )dtEmployeePromotion ON dtEmployeePromotion.EmployeeId=EGI.EmpInfoId 
   
    left JOIN (SELECT EmployeeId,MAX(EffectDate)AS Effectivedate  FROM  dbo.tblPromotionUpgrationHistory where TypeOfPromotion in ('Promotion','Upgradation') GROUP BY EmployeeId
 
   )dtEmployeePromotionHis ON dtEmployeePromotionHis.EmployeeId=EGI.EmpInfoId 

                                    where  EGI.EmpInfoId = " + id + "   ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public DataTable CheckAlreadyExist(int empId,int FinancialYearId,int CompanyId)
        {
            try
            {
                string query = @"select   * from tblBSCAppraisalSelfMaster where FinancialYearId='" + FinancialYearId + "' and EmpInfoId=" + empId;
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
            aSqlParameterlist.Add(new SqlParameter("@BSCAppraisalSelfMasterId", jobReqId));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", status));
            aSqlParameterlist.Add(new SqlParameter("@ApproveBy", approveby));
            aSqlParameterlist.Add(new SqlParameter("@ApproveDate", approvedate));

            const string query = @"UPDATE dbo.tblBSCAppraisalSelfMaster SET ActionStatus=@ActionStatus,ApproveBy=@ApproveBy,ApproveDate=@ApproveDate WHERE BSCAppraisalSelfMasterId=@BSCAppraisalSelfMasterId";
            return _aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }
        public DataTable GetAppraisalTraining(int id)
        {
            try
            {
                string query = @"SELECT 
       TrainingNeeds ,
       CONVERT(NVARCHAR(11) , TrainingStart , 106)TrainingStart ,
	   CONVERT(NVARCHAR(11) , TrainingEnd , 106)TrainingEnd,*,Quater as quaterID
       	 FROM dbo.tblBSCAppraisalTrainingNeeds WHERE  BSCAppraisalMasterId = "+ id;
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public DataTable GetDiciplinaryActionInfo(string param)
        {

            string query = @"SELECT us.UserName, SPNDR.SuspendReasonEntry, Dg.Designation, dpt.DepartmentName, DCPA.DiciplinaryId, EGI.EmpMasterCode ,EGI.EmpName,DCPA.EffectiveDate,DCPA.Description, DCPA.Remarks, DCPA.* FROM tblDiciplinaryAction AS DCPA 
								   LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON DCPA.EmpInfoId = EGI.EmpInfoId
								   LEFT JOIN dbo.tblDesignation AS Dg ON EGI.DesignationId = Dg.DesignationId
LEFT JOIN dbo.tblDepartment AS dpt ON EGI.DepartmentId= dpt.DepartmentId
LEFT JOIN dbo.tblUser AS us ON us.UserId = DCPA.EntryBy
LEFT JOIN dbo.tblSuspendReasonEntry AS SPNDR ON DCPA.ReasonId =  SPNDR.SuspendReasonEntryId
								   WHERE DCPA.ActionStatus in ('Posted','Cancel') and DCPA.IsActive = 1
								  " + param + " ORDER BY DCPA.DiciplinaryId DESC";

            return _aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable GetAppraisalMasterIdFromAppraisalSelfMasterId(int id)
        {
            try
            {
                string query = @"select  * from dbo.tblMBSCAppraisalMaster where BSCAppraisalSelfMasterId =" + id + "";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetHistoryScore(string id, string Finy)
        {
            try
            {
                string query = @"select  Score  appScore from tblHistoryAppraisalScore where EmpMasterID= '" + id + "' and FinYear='" + Finy + "'";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetHistoryScoreforNew(string id, string Finy)
        {
            try
            {
                string query = @"select   ISNULL(SUM(ISNULL(appfin.TotalScore,0)),0) appScore from tblAppraisalFinalStatus appfin
inner join tblMBSCAppraisalMaster app on appfin.BSCAppraisalMasterId=app.BSCAppraisalMasterId
left join tblFinancialYear fy on fy.FinancialYearId=app.FinancialYearId

where app.CurrentStatus='Approved' and app.EmpInfoId in (" + id + ") and fy.FinancialYearDesc='" + Finy+"'";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetViewComments(int id)
        {
            try
            {
                string query = @"SELECT (e.EmpMasterCode+' : '+e.EmpName+ISNULL(' : '+desg.Designation,'')+ISNULL(' : '+dpt.DepartmentName,''))  AS Employee , CONVERT(NVARCHAR( 11) , A.ApproveDate , 106) AS EntryDate ,A.Comments  AS PreviousVersion ,'' as Remarks , A.ActionStatus AS ApproveStatus 
            
            FROM dbo.tblMBSCAppraisalMasterAppLog A 
            LEFT JOIN dbo.tblUser u ON a.ApproveBy = u.UserId
			LEFT JOIN dbo.tblEmpGeneralInfo e ON u.EmpInfoId = e.EmpInfoId
			LEFT JOIN dbo.tblDesignation desg ON e.DesignationId = desg.DesignationId
			LEFT JOIN dbo.tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
			WHERE  A.ActionStatus<>'Drafted' and A.BSCAppraisalMasterId=" + id + "  ORDER BY A.BSCAppraisalMasterAppLogId desc ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable ReportGetAppraisalTraining(string parm)
        {
            try
            {
                string query = @"SELECT 
       TrainingNeeds ,
       CONVERT(NVARCHAR(11) , TrainingStart , 106)TrainingStart ,
	   CONVERT(NVARCHAR(11) , TrainingEnd , 106)TrainingEnd,*,Quater as quaterID, *
       	 FROM dbo.tblAppraisalTrainingNeeds 
		 LEFT JOIN dbo.tblMBSCAppraisalMaster ON tblMBSCAppraisalMaster.BSCAppraisalMasterId = tblAppraisalTrainingNeeds.BSCAppraisalMasterId
		 
		 LEFT JOIN dbo.tblEmpGeneralInfo Eg ON Eg.EmpInfoId = tblMBSCAppraisalMaster.EmpinfoId where Eg.EmpMasterCode is not NULL 
 " + parm + "  ORDER BY Eg.EmpMasterCode";
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

               aParameters.Add(new SqlParameter("@BSCAppraisalMasterId", aMaster.BSCAppraisalMasterId));
               aParameters.Add(new SqlParameter("@BSCAppraisalSelfMasterId", aMaster.AppraisalSelfMasterId));
               aParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId));
               aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
               aParameters.Add(new SqlParameter("@EntryBy", user));
               aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));
               int pk = 0;
              

               //int 

               if (aMaster.BSCAppraisalMasterId > 0)
               {
                   pk = aMaster.BSCAppraisalMasterId;
               }
               else
               {
                    string delQuery = @"Delete from tblMBSCAppraisalMaster where BSCAppraisalSelfMasterId = @BSCAppraisalSelfMasterId ";
               bool r = _aCommonInternalDal.DeleteDataByDeleteCommand(delQuery, aParameters, DataBase.HRDB);

               string query = @"Insert into tblMBSCAppraisalMaster (EmpInfoId,FinancialYearId,EntryBy,EntryDate,BSCAppraisalSelfMasterId,FYDes_BSCApp) values(@EmpInfoId,@FinancialYearId,@EntryBy,@EntryDate,@BSCAppraisalSelfMasterId, (SELECT FinancialYearDesc 
                    FROM dbo.tblFinancialYear 
                    WHERE FinancialYearId = @FinancialYearId ))";
               
              pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB); 
               }
               return pk;
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

        public bool SaveAppraisalSelfApprove(int id, string status, string user, string remarks)
        {
            try
            {
                int approveBit = status == "Approved" ? 1 : 0;
                string selfMasterQ = null;
                int currentVersion = 0;
                bool insert = false;
                string getVerionQ = @"select ActionVersion from  tblBSCAppraisalSelfMaster where BSCAppraisalSelfMasterId = " + id + "";

                DataTable GetCurrentVersion = _aCommonInternalDal.DataContainerDataTable(getVerionQ, DataBase.HRDB);

                currentVersion = GetCurrentVersion.Rows.Count <= 0
                    ? 0
                    : Convert.ToInt32(GetCurrentVersion.Rows[0][0].ToString());

                string insertLog = @"INSERT INTO dbo.tblAppraisalSelfApproveLog
                                    ( BSCAppraisalSelfMasterId ,
                                      PreviousVersion ,
                                      NewVersion ,
                                      EntryDate ,
                                      EntryBy ,
                                      ApproveStatus ,
                                      Remarks
                                    )  values ( " + id + " , " + currentVersion + " , " + (currentVersion + 1) + " , '" + DateTime.Now + "' , '" + user + "' , '" + status + "' , '" + remarks + "' )";


                insert = _aCommonInternalDal.SaveDataByInsertCommand(insertLog, DataBase.HRDB);

                if (insert == true)
                {
                    if (status == "Approved")
                    {

                        selfMasterQ = @"update tblBSCAppraisalSelfMaster set   IsApprove = " +
                                        approveBit + " ," +
                                        " ApproveBy = '" + user + "' , CurrentStatus = '" + status + "'  , ApproveDate = '" +
                                        DateTime.Now + "' , " +
                                        "ActionVersion = " + (currentVersion + 1) + " " +
                                        " where BSCAppraisalSelfMasterId = " + id + " ";
                    }
                    else
                    {
                        selfMasterQ = @"update tblBSCAppraisalSelfMaster set   IsApprove = " + approveBit + " , ApproveBy = '" + user + "' ,  IsPublish = 0 , ActionStatus = 'Drafted'  ,  CurrentStatus = '" + status + "'  , ApproveDate = '" + DateTime.Now + "' , ActionVersion = " + (currentVersion + 1) + " " +
                                         " where BSCAppraisalSelfMasterId = " + id + " ";

                        string delQ = @"Delete from tblMBSCAppraisalMaster where BSCAppraisalSelfMasterId = " + id + "";
                        string delD = @"Delete from tblBSCAppraisalSelfFuncArea where BSCAppraisalSelfMasterId = " + id + "";

                        bool deel = _aCommonInternalDal.DeleteDataByDeleteCommand(delD, DataBase.HRDB);
                        deel = _aCommonInternalDal.DeleteDataByDeleteCommand(delQ, DataBase.HRDB);
                    }

                    insert = _aCommonInternalDal.SaveDataByInsertCommand(selfMasterQ, DataBase.HRDB);
                }
                return insert;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int SaveAppraisalSelfMaster(BASAppraisalMaster aMaster, List<FuncData> FuncData, List<PartBData> PartBData, string Frm, string user)
        {
            try
            {
                if (aMaster.BSCAppraisalSelfMasterId > 0)
                {
                    /////asdasddasd
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@BSCAppraisalSelfMasterId", aMaster.BSCAppraisalSelfMasterId));
                    aParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@UpdateBy", user));
                    aParameters.Add(new SqlParameter("@ActionStatus", "Drafted"));
                    aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));
                    string query = "";
                    if (Frm == "Sup")
                    {
                          query = @"update tblBSCAppraisalSelfMaster set FinancialYearId =@FinancialYearId  , EmpInfoId =@EmpInfoId ,IsFunctionalArea=1 , UpdateBy =@UpdateBy  , UpdateDate =@UpdateDate   where BSCAppraisalSelfMasterId=@BSCAppraisalSelfMasterId";
                    }
                    else
                    {
                          query = @"update tblBSCAppraisalSelfMaster set FinancialYearId =@FinancialYearId  , EmpInfoId =@EmpInfoId  , UpdateBy =@UpdateBy  , UpdateDate =@UpdateDate,ActionStatus =@ActionStatus  where BSCAppraisalSelfMasterId=@BSCAppraisalSelfMasterId";
                    }
                  

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);

                    if (result == true)
                    {
                        int pk = aMaster.BSCAppraisalSelfMasterId;
                        FuntionalDATA(FuncData, pk);
                        PartBDATA(PartBData, pk);
                        return aMaster.BSCAppraisalSelfMasterId;
                    }
                    else
                    {
                        return 0;
                    }

                }
                else
                {


                    List<SqlParameter> aParameters = new List<SqlParameter>();

                    aParameters.Add(new SqlParameter("@BSCAppraisalSelfMasterId", aMaster.BSCAppraisalSelfMasterId));
                    aParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@EntryBy", user));
                    aParameters.Add(new SqlParameter("@ActionStatus", "Drafted"));
                    aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));



                    string query = @"Insert into tblBSCAppraisalSelfMaster (EmpInfoId,FinancialYearId,EntryBy,EntryDate,ActionStatus ) values(@EmpInfoId,@FinancialYearId,@EntryBy,@EntryDate,@ActionStatus)";

                    int pk =  _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);

                    if (pk > 0)
                    {
                          FuntionalDATA(FuncData, pk);
                         PartBDATA(PartBData, pk);
                    }
                    return pk;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private int FuntionalDATA(List<FuncData> FuncData, int pk)
        {


            List<SqlParameter> aParameters2 = new List<SqlParameter>();
            aParameters2.Add(new SqlParameter("@BSCAppraisalSelfMasterId", pk));
            string queryDel = @"Delete from tblBSCAppraisalSelfFuncArea where BSCAppraisalSelfMasterId = @BSCAppraisalSelfMasterId";

            bool delQ = _aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParameters2, DataBase.HRDB);
            foreach (FuncData item in FuncData)
            {
                List<SqlParameter> aSqlParameterList = new List<SqlParameter>();

                aSqlParameterList.Add(new SqlParameter("@BSCAppraisalSelfMasterId", pk));
                aSqlParameterList.Add(new SqlParameter("@Dimension", (object)item.Dimension ?? DBNull.Value)); aSqlParameterList.Add(new SqlParameter("@DimensionStr", (object)item.DimensionStr ?? DBNull.Value));
                aSqlParameterList.Add(new SqlParameter("@ObjectiveGoal", (object)item.ObjectiveGoal ?? DBNull.Value));
                aSqlParameterList.Add(new SqlParameter("@KPIMeasure", (object)item.KPIMeasure ?? DBNull.Value));
                aSqlParameterList.Add(new SqlParameter("@KpiWeight", (object)item.KpiWeight ?? DBNull.Value));
                aSqlParameterList.Add(new SqlParameter("@Initiatives", (object)item.Initiatives ?? DBNull.Value));
                aSqlParameterList.Add(new SqlParameter("@Deadline", (object)item.Deadline ?? DBNull.Value));

                string queryFuncData = @"INSERT INTO [dbo].[tblBSCAppraisalSelfFuncArea]
           ([BSCAppraisalSelfMasterId]
           ,[Dimension]
           ,[ObjectiveGoal]
           ,[KPIMeasure]
           ,[KpiWeight]
           ,[Initiatives]
           ,[Deadline],DimensionStr)
     VALUES
           (@BSCAppraisalSelfMasterId 
           ,@Dimension 
           ,@ObjectiveGoal 
           ,@KPIMeasure 
           ,@KpiWeight 
           ,@Initiatives 
           ,@Deadline,@DimensionStr)";

                 _aCommonInternalDal.SaveDataByInsertCommandById(queryFuncData, aSqlParameterList, DataBase.HRDB);

            }

            return pk;
        }
        
        private int PartBDATA(List<PartBData> FuncData, int pk)
        {


            List<SqlParameter> aParameters2 = new List<SqlParameter>();
            aParameters2.Add(new SqlParameter("@BSCAppraisalSelfMasterId", pk));
            string queryDel = @"Delete from tblBSCAppraisalSelfBehaveArea where BSCAppraisalSelfMasterId = @BSCAppraisalSelfMasterId";

            bool delQ = _aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParameters2, DataBase.HRDB);
            foreach (PartBData item in FuncData)
            {
                List<SqlParameter> aSqlParameterList = new List<SqlParameter>();

                aSqlParameterList.Add(new SqlParameter("@BSCAppraisalSelfMasterId", pk));
                aSqlParameterList.Add(new SqlParameter("@SkillType",  (object)item.SkillType ?? DBNull.Value));
                aSqlParameterList.Add(new SqlParameter("@SkillInfo",  (object)item.SkillInfo ?? DBNull.Value));
                aSqlParameterList.Add(new SqlParameter("@SupportingEmp", (object)item.SupportingEmp ?? DBNull.Value));
                aSqlParameterList.Add(new SqlParameter("@Score", (object)item.Score ?? DBNull.Value));
                aSqlParameterList.Add(new SqlParameter("@SetScore", (object)item.SetScore ?? DBNull.Value));


                string queryFuncData = @"INSERT INTO [dbo].[tblBSCAppraisalSelfBehaveArea]
           ([BSCAppraisalSelfMasterId]
           ,[SkillInfo]
           ,[SupportingEmp]
           ,[Score], SkillType, SetScore )
     VALUES
           (@BSCAppraisalSelfMasterId 
           ,@SkillInfo 
           ,@SupportingEmp 
           ,@Score,@SkillType ,@SetScore)";

                _aCommonInternalDal.SaveDataByInsertCommandById(queryFuncData, aSqlParameterList, DataBase.HRDB);

            }

            return pk;
        }

        public int SaveAppraisalSelfMasterMulti(AppraisalMaster aMaster, string user)
        {
            try
            {
                if (aMaster.BSCAppraisalMasterId > 0)
                {
                    /////asdasddasd
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@BSCAppraisalSelfMasterId", aMaster.BSCAppraisalMasterId));
                    aParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@UpdateBy", user));
                    aParameters.Add(new SqlParameter("@ActionStatus", "Approved"));
                    aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));
                    string query = @"update tblBSCAppraisalSelfMaster set FinancialYearId =@FinancialYearId  , EmpInfoId =@EmpInfoId  , UpdateBy =@UpdateBy  , UpdateDate =@UpdateDate,ActionStatus =@ActionStatus, IsFromGroupKPI=1  where BSCAppraisalSelfMasterId=@BSCAppraisalSelfMasterId";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);

                    if (result == true)
                    {
                        return aMaster.BSCAppraisalMasterId;
                    }
                    else
                    {
                        return 0;
                    }

                }
                else
                {


                    List<SqlParameter> aParameters = new List<SqlParameter>();

                    aParameters.Add(new SqlParameter("@BSCAppraisalSelfMasterId", aMaster.BSCAppraisalMasterId));
                    aParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@EntryBy", user));
                    aParameters.Add(new SqlParameter("@ActionStatus", "Approved"));
                    aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));



                    string query = @"Insert into tblBSCAppraisalSelfMaster (EmpInfoId,FinancialYearId,EntryBy,EntryDate,ActionStatus,IsFromGroupKPI ) values(@EmpInfoId,@FinancialYearId,@EntryBy,@EntryDate,@ActionStatus,1)";

                    int pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                    return pk;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int SaveAppraisalSelfMasterforSupper(AppraisalMaster aMaster, string user)
        {
            try
            {
                if (aMaster.BSCAppraisalMasterId > 0)
                {
                    /////asdasddasd
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@BSCAppraisalSelfMasterId", aMaster.BSCAppraisalMasterId));
                    aParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@IsFunctionalArea", aMaster.IsFunctionalArea));
                    aParameters.Add(new SqlParameter("@IsBehavioralArea", aMaster.IsBehavioralArea));
                    aParameters.Add(new SqlParameter("@UpdateBy", user));
                  //  aParameters.Add(new SqlParameter("@ActionStatus", "Drafted"));
                    aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));
                    string query = @"update tblBSCAppraisalSelfMaster set IsFunctionalArea=@IsFunctionalArea, IsBehavioralArea=@IsBehavioralArea, FinancialYearId =@FinancialYearId  , EmpInfoId =@EmpInfoId  , UpdateBy =@UpdateBy  , UpdateDate =@UpdateDate where BSCAppraisalSelfMasterId=@BSCAppraisalSelfMasterId";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);

                    if (result == true)
                    {
                        return aMaster.BSCAppraisalMasterId;
                    }
                    else
                    {
                        return 0;
                    }

                }
                else
                {


                    List<SqlParameter> aParameters = new List<SqlParameter>();

                    aParameters.Add(new SqlParameter("@BSCAppraisalSelfMasterId", aMaster.BSCAppraisalMasterId));
                    aParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@EntryBy", user));
                    //aParameters.Add(new SqlParameter("@ActionStatus", "Drafted"));
                    aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));
                    aParameters.Add(new SqlParameter("@IsFunctionalArea", aMaster.IsFunctionalArea));
                    aParameters.Add(new SqlParameter("@IsBehavioralArea", aMaster.IsBehavioralArea));


                    string query = @"Insert into tblBSCAppraisalSelfMaster (EmpInfoId,FinancialYearId,EntryBy,EntryDate,IsFunctionalArea,IsBehavioralArea ) values(@EmpInfoId,@FinancialYearId,@EntryBy,@EntryDate,@IsFunctionalArea,@IsBehavioralArea)";

                    int pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                    return pk;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //public bool SaveAppraialFunctionalDetails(List<AppraisalFunctionalArea> aList, int masterid, int selfMaster)
        //{
        //    try
        //    {

        //        string query2 = @"delete from tblBSCAppraisalSelfFuncArea where BSCAppraisalSelfMasterId = " + selfMaster + "";
        //        bool a = _aCommonInternalDal.DeleteDataByDeleteCommand(query2, DataBase.HRDB);

        //        string query3 = @"delete from tblAppraisalSelfFuncArea where BSCAppraisalSelfMasterId = " + selfMaster + "";
        //        bool asd = _aCommonInternalDal.DeleteDataByDeleteCommand(query3, DataBase.HRDB);

        //        bool result = false;
        //        foreach (var item in aList)
        //        {
        //            List<SqlParameter> aParameters = new List<SqlParameter>();

        //            aParameters.Add(new SqlParameter("@BSCAppraisalMasterId", masterid));
        //            aParameters.Add(new SqlParameter("@BSCAppraisalSelfMasterId", selfMaster));

        //            aParameters.Add(new SqlParameter("@KpiInfo", item.KpiInfo));
        //            aParameters.Add(new SqlParameter("@Deadline", item.Deadline));
        //            aParameters.Add(new SqlParameter("@KpiWeight", item.KpiWeight));
        //            aParameters.Add(new SqlParameter("@SelfMark", item.SelfMark));
        //            aParameters.Add(new SqlParameter("@KpiWeightPer", item.KpiWeightPer));
        //            aParameters.Add(new SqlParameter("@MidYearStatus", item.MidYearStatus));
        //            aParameters.Add(new SqlParameter("@ResultYearEnd", item.ResultYearEnd == null ? 0 : item.ResultYearEnd));

        //            aParameters.Add(new SqlParameter("@SupervisorMark", item.SupervisorMark));
        //            aParameters.Add(new SqlParameter("@Target", item.Target));
        //            aParameters.Add(new SqlParameter("@TargetPer", item.TargetPer));

        //            string query = @"insert into tblBSCAppraisalSelfFuncArea(BSCAppraisalMasterId, KpiInfo, KpiWeight, Deadline, MidYearStatus, ResultYearEnd,SelfMark,  SupervisorMark, Target,BSCAppraisalSelfMasterId,KpiWeightPer,TargetPer) values(@BSCAppraisalMasterId, @KpiInfo, @KpiWeight, @Deadline, @MidYearStatus, @ResultYearEnd, @SelfMark, @SupervisorMark, @Target,@BSCAppraisalSelfMasterId,@KpiWeightPer,@TargetPer)";
        //            result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);



        //            string queryD = @"insert into tblAppraisalSelfFuncArea(BSCAppraisalSelfMasterId, KpiInfo, KpiWeight, Deadline, MidYearStatus,  SelfMark, Target,KpiWeightPer,TargetPer) values(@BSCAppraisalSelfMasterId, @KpiInfo, @KpiWeight, @Deadline, @MidYearStatus,  @SelfMark, @Target,@KpiWeightPer,@TargetPer)";
        //            result = _aCommonInternalDal.SaveDataByInsertCommand(queryD, aParameters, DataBase.HRDB);



        //            if (result == false)
        //            {
        //                return false;
        //            }


        //        }
        //        return result;


        //    }
        //    catch (Exception x)
        //    {

        //        throw;
        //    }
        //}


        public bool SaveAppraialFunctionalDetails(List<AppraisalFunctionalAreaBSC> aList, int masterid, int selfMaster)
        {
            try
            {

                string query2 = @"delete from tblMBSCAppraisalFuncArea where BSCAppraisalSelfMasterId = " + selfMaster + "";
                bool a = _aCommonInternalDal.DeleteDataByDeleteCommand(query2, DataBase.HRDB);

                //string query3 = @"delete from tblAppraisalSelfFuncArea where AppraisalSelfMasterId = " + selfMaster + "";
                //bool asd = _aCommonInternalDal.DeleteDataByDeleteCommand(query3, DataBase.HRDB);

                bool result = false;
                foreach (var item in aList)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();

                    aParameters.Add(new SqlParameter("@BSCAppraisalMasterId", masterid));
                    aParameters.Add(new SqlParameter("@BSCAppraisalSelfMasterId", selfMaster)); 
                    aParameters.Add(new SqlParameter("@BSCAppraisalSelfFucAreaId", item.BSCAppraisalSelfFucAreaId)); 
                    aParameters.Add(new SqlParameter("@SelfMark", (object)item.SelfMark ?? DBNull.Value)); 
                    aParameters.Add(new SqlParameter("@MidYearStatus", (object)item.MidYearStatus ?? DBNull.Value)); 
                    aParameters.Add(new SqlParameter("@ResultYearEnd", (object)item.ResultYearEnd ?? DBNull.Value)); 
                    aParameters.Add(new SqlParameter("@SupervisorMark", (object)item.SupervisorMark ?? DBNull.Value)); 
                    aParameters.Add(new SqlParameter("@IsActive", (object)item.IsActive ?? DBNull.Value)); 

                    aParameters.Add(new SqlParameter("@Dimension", (object)item.Dimension ?? DBNull.Value));
                    aParameters.Add(new SqlParameter("@DimensionStr", (object)item.DimensionStr ?? DBNull.Value));
                    aParameters.Add(new SqlParameter("@ObjectiveGoal", (object)item.ObjectiveGoal ?? DBNull.Value));
                    aParameters.Add(new SqlParameter("@KPIMeasure", (object)item.KPIMeasure ?? DBNull.Value));
                    aParameters.Add(new SqlParameter("@KpiWeight", (object)item.KpiWeight ?? DBNull.Value));
                    aParameters.Add(new SqlParameter("@Initiatives", (object)item.Initiatives ?? DBNull.Value));
                    aParameters.Add(new SqlParameter("@Deadline", (object)item.Deadline ?? DBNull.Value));
                    aParameters.Add(new SqlParameter("@CommentsFunc", (object)item.CommentsFunc ?? DBNull.Value));
                    aParameters.Add(new SqlParameter("@SupervisorComments", (object)item.SupervisorComments ?? DBNull.Value));





    //                if (item.BSCAppraisalSelfFucAreaId == 0)
    //                {
    //                    string queryD = @"   INSERT INTO [dbo].[tblBSCAppraisalSelfFuncArea]
    //       ([BSCAppraisalSelfFucAreaId]
    //       ,[Dimension]
    //       ,[ObjectiveGoal]
    //       ,[KPIMeasure]
    //       ,[KpiWeight]
    //       ,[Initiatives]
    //       ,[Deadline]
    //       ,[DimensionStr]
    //       ,[IsActive]
    //       ,[SelfMark]
    //       ,[SupervisorMark]
    //       ,[MidYearStatus]
    //       ,[ResultYearEnd])
    // VALUES
    //       (@BSCAppraisalSelfFucAreaId
    //       ,@Dimension
    //       ,@ObjectiveGoal
    //       ,@KPIMeasure
    //       ,@KpiWeight
    //       ,@Initiatives
    //       ,@Deadline
    //       ,@DimensionStr
    //       ,@IsActive
    //       ,@SelfMark
    //       ,@SupervisorMark
    //       ,@MidYearStatus
    //       ,@ResultYearEnd);
    //";

    //                    int pk = _aCommonInternalDal.SaveDataByInsertCommandById(queryD, aParameters, DataBase.HRDB);
    //                    item.BSCAppraisalSelfFucAreaId = pk;
    //                    aParameters.Add(new SqlParameter("@BSCAppraisalSelfFuncAreaId", item.BSCAppraisalSelfFucAreaId));
    //                }
    //                else
    //                {
    //                    aParameters.Add(new SqlParameter("@BSCAppraisalSelfFuncAreaId", item.BSCAppraisalSelfFucAreaId));
    //                    string queryD = @"    UPDATE [dbo].[tblBSCAppraisalSelfFuncArea]
    //SET
    //    [BSCAppraisalSelfMasterId] = @BSCAppraisalSelfMasterId,
    //    [Dimension] = @Dimension,
    //    [ObjectiveGoal] = @ObjectiveGoal,
    //    [KPIMeasure] = @KPIMeasure,
    //    [KpiWeight] = @KpiWeight,
    //    [Initiatives] = @Initiatives,
    //    [Deadline] = @Deadline,
    //    [DimensionStr] = @DimensionStr,
    //    [IsActive] = @IsActive,
    //    [SelfMark] = @SelfMark,
    //    [SupervisorMark] = @SupervisorMark,
    //    [MidYearStatus] = @MidYearStatus,
    //    [ResultYearEnd] = @ResultYearEnd
    //WHERE 
    //    [BSCAppraisalSelfFuncAreaId] = @BSCAppraisalSelfFuncAreaId; ";
    //                    result = _aCommonInternalDal.SaveDataByInsertCommand(queryD, aParameters, DataBase.HRDB);
    //                }


                    if (item.IsActive == true)
                    {


                        string query = @"   INSERT INTO [dbo].[tblMBSCAppraisalFuncArea]
           ([BSCAppraisalMasterId]
           ,[BSCAppraisalSelfMasterId]
           ,[BSCAppraisalSelfFucAreaId]
           ,[SelfMark]
           ,[MidYearStatus]
           ,[ResultYearEnd]
           ,[SupervisorMark]
           ,[IsActive]
           ,[Dimension]
           ,[DimensionStr]
           ,[ObjectiveGoal]
           ,[KPIMeasure]
           ,[KpiWeight]
           ,[Initiatives]
           ,[Deadline],CommentsFunc,SupervisorComments)
     VALUES
           (@BSCAppraisalMasterId
           ,@BSCAppraisalSelfMasterId
           ,@BSCAppraisalSelfFucAreaId
           ,@SelfMark
           ,@MidYearStatus
           ,@ResultYearEnd
           ,@SupervisorMark
           ,@IsActive
           ,@Dimension
           ,@DimensionStr
           ,@ObjectiveGoal
           ,@KPIMeasure
           ,@KpiWeight
           ,@Initiatives
           ,@Deadline,@CommentsFunc,@SupervisorComments);";
                        result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);

                    }


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
                aParametersd.Add(new SqlParameter("@BSCAppraisalSelfMasterId", masterid));
                string queryDel = @"Delete from tblAppraisalSelfFuncArea where BSCAppraisalSelfMasterId = @BSCAppraisalSelfMasterId";

                bool delRes = _aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParametersd, DataBase.HRDB);


                bool result = false;
                foreach (var item in aList)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();

                    aParameters.Add(new SqlParameter("@BSCAppraisalSelfMasterId", masterid));
                    aParameters.Add(new SqlParameter("@KpiInfo", item.KpiInfo));
                    aParameters.Add(new SqlParameter("@Deadline", item.Deadline));
                    aParameters.Add(new SqlParameter("@KpiWeight", item.KpiWeight));
                    aParameters.Add(new SqlParameter("@KpiWeightPer", item.KpiWeightPer));
                    aParameters.Add(new SqlParameter("@MidYearStatus", item.MidYearStatus));

                    aParameters.Add(new SqlParameter("@SelfMark", item.SupervisorMark));
                    aParameters.Add(new SqlParameter("@IsActive", item.IsActive));
                    aParameters.Add(new SqlParameter("@Target", item.Target));
                    aParameters.Add(new SqlParameter("@TargetPer", item.TargetPer));



                    string query = @"insert into tblAppraisalSelfFuncArea(BSCAppraisalSelfMasterId, KpiInfo, KpiWeight, Deadline, MidYearStatus,  SelfMark, Target,KpiWeightPer,TargetPer,IsActive) values(@BSCAppraisalSelfMasterId, @KpiInfo, @KpiWeight, @Deadline, @MidYearStatus,  @SelfMark, @Target,@KpiWeightPer,@TargetPer,@IsActive)";
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
                string query = @"select a.BSCAppraisalSelfMasterId , (b.EmpMasterCode+':'+b.EmpName+':'+desg.Designation) employee ,dpt.DepartmentName ,c.CompanyName , y.FinancialYearDesc from  tblBSCAppraisalSelfMaster A 
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

        public DataTable GetAppraisalByPermission3(string MasterId)
        {
            try
            {
                string query = @"select  ForEmpInfoId,* from tblBSCAppraisalSelfAppLog where BSCAppraisalSelfMasterId=" + MasterId + "  and ActionStatus<>'Drafted' order by BSCAppraisalSelfAppLogId desc  ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public DataTable GetAppraisalByKpiPermissionsss(string EmpID, string FinancialYear, string param, string param22)
        {
            try
            {
                string query = @"SELECT Distinct * FROM(SELECT  ISNULL(app.BSCAppraisalSelfMasterId,0) BSCAppraisalSelfMasterId, e.EmpInfoId ,b.DeadLine, 'Set '+b.OptionInfo OptionInfo,  isnull(b.ExtensionDate,b.DeadLine) as ExtensionDate,
        ( 'Employee ID: '+ e.EmpMasterCode + ', Employee Name: ' + e.EmpName +    ISNULL(', Designation: ' + desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove,
        app.CurrentStatus, app.ActionStatus, (CASE WHEN app.ActionStatus<>'Approved' then 'Not Approved' ELSE app.ActionStatus END)AS KPIActionStatus ,ForEmp.EmpName as PendingEmp
        FROM    dbo.tblBSCKpiDeadlineMaster A
        LEFT JOIN dbo.tblBSCKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN dbo.tblBSCAppraisalSelfMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId 
        
	LEFT  JOIN (SELECT BSCAppraisalSelfMasterId,MAX(Version)MaxVer FROM dbo.tblBSCAppraisalSelfAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY BSCAppraisalSelfMasterId) AS CELog ON CELog.BSCAppraisalSelfMasterId= app.BSCAppraisalSelfMasterId
								LEFT  JOIN dbo.tblBSCAppraisalSelfAppLog ON tblBSCAppraisalSelfAppLog.BSCAppraisalSelfMasterId = app.BSCAppraisalSelfMasterId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblBSCAppraisalSelfAppLog.ForEmpInfoId
        WHERE b.EmpInfoId =  " + Convert.ToInt32(EmpID) + " and y.FinancialYearDesc='" + FinancialYear + "'"  +param + "" +
                               "" +
                               "" +
                               @"	union all SELECT  ISNULL(app.BSCAppraisalSelfMasterId,0) BSCAppraisalSelfMasterId, e.EmpInfoId ,b.DeadLine, 'Set '+b.OptionInfo OptionInfo,  isnull(b.ExtensionDate,b.DeadLine) as ExtensionDate,
        ( 'Employee ID: '+ e.EmpMasterCode + ', Employee Name: ' + e.EmpName +    ISNULL(', Designation: ' + desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove,
        app.CurrentStatus, app.ActionStatus, (CASE WHEN app.ActionStatus<>'Approved' then 'Not Approved' ELSE app.ActionStatus END)AS KPIActionStatus ,ForEmp.EmpName as PendingEmp
        FROM    dbo.tblBSCKpiDeadlineMaster A
        LEFT JOIN dbo.tblBSCKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
 	 inner JOIN   tblEmpAllRefference reff  ON b.EmpinfoId = reff.RefferenceEmpId 

        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN dbo.tblBSCAppraisalSelfMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId 
        
	LEFT  JOIN (SELECT BSCAppraisalSelfMasterId,MAX(Version)MaxVer FROM dbo.tblBSCAppraisalSelfAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY BSCAppraisalSelfMasterId) AS CELog ON CELog.BSCAppraisalSelfMasterId= app.BSCAppraisalSelfMasterId
								LEFT  JOIN dbo.tblBSCAppraisalSelfAppLog ON tblBSCAppraisalSelfAppLog.BSCAppraisalSelfMasterId = app.BSCAppraisalSelfMasterId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblBSCAppraisalSelfAppLog.ForEmpInfoId
inner join (select   NewEmployeeId,OnlyViewComId from tblEmpSpecialTransfer where OnlyView=1) tblPer on reff.EmployeeId =tblPer.NewEmployeeId

        WHERE reff.EmployeeId =  " + Convert.ToInt32(EmpID) + " and y.FinancialYearDesc='" + FinancialYear + "'" + param + param22 + " )HH";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public DataTable GetAppraisalByKpiPermission(string EmpID, string FinancialYear, string param)
        {
            try
            {
                string query = @"SELECT  ISNULL(app.BSCAppraisalSelfMasterId,0) BSCAppraisalSelfMasterId, e.EmpInfoId ,b.DeadLine,  isnull(b.ExtensionDate,b.DeadLine) as ExtensionDate,
        ( 'Employee ID: '+ e.EmpMasterCode + ', Employee Name: ' + e.EmpName +    ISNULL(', Designation: ' + desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove,
        app.CurrentStatus, app.ActionStatus, (CASE WHEN app.ActionStatus<>'Approved' then 'Not Approved' ELSE app.ActionStatus END)AS KPIActionStatus ,ForEmp.EmpName as PendingEmp
        FROM    dbo.tblBSCKpiDeadlineMaster A
        LEFT JOIN dbo.tblBSCKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN dbo.tblBSCAppraisalSelfMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId 
        
	LEFT  JOIN (SELECT BSCAppraisalSelfMasterId,MAX(Version)MaxVer FROM dbo.tblBSCAppraisalSelfAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY BSCAppraisalSelfMasterId) AS CELog ON CELog.BSCAppraisalSelfMasterId= app.BSCAppraisalSelfMasterId
								LEFT  JOIN dbo.tblBSCAppraisalSelfAppLog ON tblBSCAppraisalSelfAppLog.BSCAppraisalSelfMasterId = app.BSCAppraisalSelfMasterId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblBSCAppraisalSelfAppLog.ForEmpInfoId
        WHERE b.EmpInfoId =  " + Convert.ToInt32(EmpID) + " and y.FinancialYearDesc='" + FinancialYear+"'" + param;

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {
             
                 throw ex;
            }


        }

        public DataTable GetAppraisalByPermission3sssssdsds(string MasterId)
        {
            try
            {
                string query = @"select  ForEmpInfoId,* from tblBSCAppraisalSelfAppLog where BSCAppraisalSelfMasterId=" + MasterId + "  and ActionStatus<>'Drafted' order by bscAppraisalSelfAppLogId desc  ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public DataTable GetAppraisalByKpiPermissionDashBoard(string EmpID,   string param)
        {
            try
            {
                string query = @"SELECT ISNULL(app.BSCAppraisalSelfMasterId,0) BSCAppraisalSelfMasterId, e.EmpInfoId ,  isnull(b.ExtensionDate,b.DeadLine) as ExtensionDate,
        ( 'Employee ID: '+ e.EmpMasterCode + ', Employee Name: ' + e.EmpName +    ISNULL(', Designation: ' + desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove,
        app.CurrentStatus, app.ActionStatus, (CASE WHEN app.ActionStatus<>'Approved' then 'Not Approved' ELSE app.ActionStatus END)AS KPIActionStatus ,ForEmp.EmpName as PendingEmp
        FROM    dbo.tblBSCKpiDeadlineMaster A
        LEFT JOIN dbo.tblBSCKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN dbo.tblBSCAppraisalSelfMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId 
        
	LEFT  JOIN (SELECT BSCAppraisalSelfMasterId,MAX(Version)MaxVer FROM dbo.tblBSCAppraisalSelfAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY BSCAppraisalSelfMasterId) AS CELog ON CELog.BSCAppraisalSelfMasterId= app.BSCAppraisalSelfMasterId
								LEFT  JOIN dbo.tblBSCAppraisalSelfAppLog ON tblBSCAppraisalSelfAppLog.BSCAppraisalSelfMasterId = app.BSCAppraisalSelfMasterId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblBSCAppraisalSelfAppLog.ForEmpInfoId
        WHERE b.EmpInfoId =  " + Convert.ToInt32(EmpID) + param + " order by BSCAppraisalSelfMasterId desc";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public DataTable GetApprovalDependencySum(string EmpID)
        {
            try
            {
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@EmpInfoId", EmpID));
                string query = @" 


select SUM(EmpCount) EmpCount from (


 


  SELECT  'Manpower Budget Pending' Parti, COUNT(*) EmpCount   FROM dbo.tblMPBudgetMaster bm
								 
								LEFT JOIN dbo.tblCompanyInfo c ON c.CompanyId = bm.CompanyId
                                LEFT JOIN dbo.tblDepartment d ON d.DepartmentId=bm.DepartmentId
                                LEFT JOIN dbo.tblFinancialYear fy ON fy.FinancialYearId = bm.FinancialYearId 
                                INNER JOIN (SELECT MPBudgetMasterId,MAX(Version)MaxVer FROM dbo.tblMPBudgetMasterAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY MPBudgetMasterId) AS CELog ON CELog.MPBudgetMasterId= bm.MPBudgetMasterId
								INNER JOIN dbo.tblMPBudgetMasterAppLog ON tblMPBudgetMasterAppLog.MPBudgetMasterId = bm.MPBudgetMasterId
                                where (bm.IsDelete is null or bm.IsDelete = 0) and Version=CELog.MaxVer  and  ForEmpInfoId = @EmpInfoId   


								UNION ALL


								SELECT 'Requisition Pending' Parti, COUNT(*) EmpCount  FROM dbo.tblJobReqForm AS JQF
                            INNER JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = JQF.CompanyId
                            INNER JOIN dbo.tblFinancialYear AS FINY ON JQF.FinYearId = FINY.FinancialYearId
							INNER JOIN (SELECT JobReqId,MAX(Version)MaxVer FROM dbo.tblJobReqFormAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY JobReqId) AS CELog ON CELog.JobReqId= JQF.JobReqId
								INNER JOIN dbo.tblJobReqFormAppLog ON tblJobReqFormAppLog.JobReqId = JQF.JobReqId
                                where (JQF.IsDelete is null or JQF.IsDelete = 0) and Version=CELog.MaxVer  and  ForEmpInfoId = @EmpInfoId


								UNION ALL 
SELECT 'Recruitment Pending' Parti, COUNT(*) EmpCount FROM dbo.tblRecruitmentApproval RA 
LEFT JOIN tblJobCreation JC ON JC.JobID = RA.JobId

                                   LEFT JOIN tblCompanyInfo AS CI ON JC.CompanyId = CI.CompanyId
                                   LEFT JOIN tblJobCreationLocation AS JCL ON JC.JobID = JCL.JobID
                                  
                                   LEFT JOIN dbo.tblJobReqForm AS RF ON JC.ReqCodeId = RF.JobReqId
                                   LEFT JOIN dbo.tblFinancialYear AS FY ON RF.FinYearId = FY.FinancialYearId
                                   LEFT JOIN dbo.tblDepartment AS dpt ON RF.DeptId = dpt.DepartmentId 
								   
							LEFT JOIN dbo.tblFinancialYear AS FINY ON RF.FinYearId = FINY.FinancialYearId
                            INNER JOIN (SELECT RecruitmentId,MAX(Version)MaxVer FROM dbo.tblRecruitmentApprovalAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY RecruitmentId) AS CELog ON CELog.RecruitmentId= RA.RecruitmentId
							INNER JOIN dbo.tblRecruitmentApprovalAppLog ON tblRecruitmentApprovalAppLog.RecruitmentId = RA.RecruitmentId
                                where Version=CELog.MaxVer   and  ForEmpInfoId = @EmpInfoId


								union all
								SELECT 'Clearence Form Pending' Parti, COUNT(*) EmpCount  From dbo.tblEmpExitMaster EIM
	 LEFT JOIN tblEmployeeJobLeft EPE  ON EIM.EmployeeId = EPE.EmployeeId 
                                    left JOIN dbo.tblEmpGeneralInfo  Emp ON EIM.EmployeeId = Emp.EmpInfoId
                                    INNER JOIN dbo.tblCompanyInfo  Com ON Emp.CompanyId = Com.CompanyId
                                    left JOIN dbo.tblJobLeftType  JType ON EPE.JobLeftTypeId = JType.JobLeftTypeId         
								 LEFT JOIN dbo.tblUser  UserR ON EIM.EntryBy = UserR.UserName
								 LEFT JOIN dbo.tblEmpGeneralInfo  UserEmp ON UserR.EmpInfoId = UserEmp.EmpInfoId                        								                                 
						  LEFT JOIN dbo.tblUser  UpBY ON EPE.UpdateBy = UpBY.UserId 
					 
				 

					  LEFT JOIN (SELECT DISTINCT E.ExitDetailId,MasterId, ApprovalStatus FROM tblEmpExitDetail E WHERE E.IsDone=0 AND E.EmpInfoIdApproval=@EmpInfoId)tblD ON EIM.ExitMasterId=tblD.MasterId
					 
					        
                                                          
 Where   ( EPE.IsDelete IS NULL OR EPE.IsDelete = 0) AND      EIM.ExitMasterId IN (SELECT MasterId FROM dbo.tblEmpExitDetail WHERE  IsDone=0 AND EmpInfoIdApproval=@EmpInfoId)  


 union all

 		


SELECT 'Document Approval Pending' Parti, COUNT(*) EmpCount FROM tblMeeting_MiscellaneousInfo mas  WITH (NOLOCK)
LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = mas.CompanyId
left JOIN  dbo.tblUser us   ON  mas.CreateBy =us.UserId  
left JOIN  dbo.tblEmpGeneralInfo usemp   ON  us.EmpInfoId =usemp.EmpInfoId   
 LEFT JOIN dbo.tblDesignation dgs ON usemp.DesignationId = dgs.DesignationId
 
left JOIN  dbo.tblUser usUp   ON  mas.UpdateBy =usUp.UserId

 INNER JOIN (SELECT MiscellaneousInfoId,MAX(Version)MaxVer FROM dbo.tblMeeting_MiscellaneousInfoAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY MiscellaneousInfoId) AS CELog ON CELog.MiscellaneousInfoId= mas.MiscellaneousInfoId
								INNER JOIN dbo.tblMeeting_MiscellaneousInfoAppLog ON tblMeeting_MiscellaneousInfoAppLog.MiscellaneousInfoId = mas.MiscellaneousInfoId
                                where   ((Version=CELog.MaxVer) OR (Version IS NULL))    and  ForEmpInfoId=@EmpInfoId


								union all

								SELECT 'Employee Probation Period' Parti, COUNT(*) EmpCount 
                                    FROM tblEmpGeneralInfo AS EGI 
                                    LEFT JOIN dbo.tblCompanyInfo AS CI ON EGI.CompanyId = CI.CompanyId
                                    LEFT JOIN dbo.tblDivision AS DSN ON EGI.DivisionId = DSN.DivisionId
                                    LEFT JOIN dbo.tblDivisionWing AS DSNW ON EGI.DivisionWId = DSNW.DivisionWId
                                    LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                                    LEFT JOIN dbo.tblSection AS SEC ON EGI.SectionId = SEC.SectionId
                                    LEFT JOIN dbo.tblSubSection AS SSEC ON EGI.SubSectionId = SSEC.SubSectionId
                                    LEFT JOIN dbo.tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
									LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
									LEFT JOIN dbo.tblSalaryGrade AS SLG ON EGI.SalaryGradeId = SLG.SalaryGradeId
									INNER JOIN dbo.tblProbationEvaluationMaster PM ON PM.EmpInfoId=EGI.EmpInfoId
									INNER JOIN (SELECT ProbationEvaluationMasterId,MAX(Version)MaxVer FROM dbo.tblProbationEvaluationAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY ProbationEvaluationMasterId) AS ProbLog ON ProbLog.ProbationEvaluationMasterId = PM.ProbationEvaluationMasterId
								INNER JOIN dbo.tblProbationEvaluationAppLog ON tblProbationEvaluationAppLog.ProbationEvaluationMasterId = PM.ProbationEvaluationMasterId
									
									 WHERE EGI.IsActive='1' AND EGI.EmployeeStatus='Active' AND Version=ProbLog.MaxVer AND ForEmpInfoId = @EmpInfoId AND EGI.EmpInfoId IN (SELECT EmpInfoId FROM tblProbationEvaluationMaster)  



									 union all

									 SELECT   'Employemnt Type Change Approval' Parti, COUNT(*) EmpCount 
 From tblContractualEmpManage EPE
 INNER JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
 INNER JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
 LEFT JOIN dbo.tblDepartment Dept ON dept.DepartmentId=Emp.DepartmentId
 LEFT JOIN dbo.tblDesignation Desig ON Desig.DesignationId=Emp.DesignationId  
INNER JOIN (SELECT ContractualEmpManageId,MAX(Version)MaxVer FROM dbo.tblContractualEmpManageAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY ContractualEmpManageId) AS CELog ON CELog.ContractualEmpManageId= EPE.ContractualEmpManageId
								INNER JOIN dbo.tblContractualEmpManageAppLog ON tblContractualEmpManageAppLog.ContractualEmpManageId = EPE.ContractualEmpManageId
                                where Version=CELog.MaxVer  and  ForEmpInfoId = @EmpInfoId


								union all

								SELECT   'Skill Will Assessment Setup' Parti, COUNT(*) EmpCount 
        FROM    dbo.tblSkillWillAssesDecMaster A
        LEFT JOIN dbo.tblSkillWillAssesDecDetails b ON A.SkillWillAssesDecMasId = b.SkillWillAssesDecMasId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
		Inner JOIn  tblEmpGeneralInfo Se ON Se.ReportingEmpId = e.EmpInfoId
		 LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN tblFinancialYear y ON y.FinancialYearId = A.FinancialYearId
        LEFT JOIN tblEmpSkillWillAssessmentMaster SWAM ON Se.EmpInfoId=SWAM.EmpInfoId

		left join (select   top 1 a.EmpSkillWillMasterId, a.Comments from tblEmpSkillWillAssessmentMasterAppLog a where a.ActionStatus='Review' group by a.EmpSkillWillMasterId, a.Comments) tblapp on  SWAM.EmpSkillWillMasterId=tblapp.EmpSkillWillMasterId
	 
        WHERE  ((SWAM.ActionStatus is  null) or  (SWAM.ActionStatus not in ('Verified','Approved'))) and   b.EmpinfoId =  @EmpInfoId


		union all

		SELECT 'Skill Will Assessment Approval' Parti, COUNT(*) EmpCount  FROM tblEmpSkillWillAssessmentMaster M 
LEFT JOIN tblEmpGeneralInfo EMP ON M.EmpInfoId = EMP.EmpInfoId
LEFT JOIN tblDesignation desg ON EMP.DesignationId = desg.DesignationId
LEFT JOIN tblDepartment dpt ON EMP.DepartmentId = dpt.DepartmentId
 INNER JOIN (SELECT EmpSkillWillMasterId,MAX(Version)MaxVer FROM dbo.tblEmpSkillWillAssessmentMasterAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY EmpSkillWillMasterId) AS CELog ON CELog.EmpSkillWillMasterId= M.EmpSkillWillMasterId
								INNER JOIN dbo.tblEmpSkillWillAssessmentMasterAppLog ON tblEmpSkillWillAssessmentMasterAppLog.EmpSkillWillMasterId = M.EmpSkillWillMasterId
                                where    Version=CELog.MaxVer and      ForEmpInfoId = @EmpInfoId    and      EntryEmpId  not in ( @EmpInfoId)

							union all 
							
							
							SELECT 'Expense Reimbursement Form Approval' Parti, COUNT(*) EmpCount  FROM tbl_ReimbursmentFormMaster_HealthCare H
	LEFT JOIN tblEmpGeneralInfo Emp ON Emp.EmpInfoId = H.EmpInfoId
	LEFT JOIN tblCompanyInfo Com ON Com.CompanyId = H.CompanyId
	LEFT JOIN tblDesignation Dg ON Dg.DesignationId = H.DesignationId
	INNER JOIN (SELECT ReimbursFromMasterId,MAX(Version)MaxVer FROM dbo.tblReimbursementSelfAppLog WHERE ActionStatus NOT IN
	('Review') GROUP BY ReimbursFromMasterId) AS CELog ON CELog.ReimbursFromMasterId= H.ReimbursFromMasterId
	INNER JOIN dbo.tblReimbursementSelfAppLog ON tblReimbursementSelfAppLog.ReimbursFromMasterId = H.ReimbursFromMasterId                              
	where H.ReimbursFromMasterId IS Not NULL      AND   Version=CELog.MaxVer and  ForEmpInfoId = @EmpInfoId)tbl ";

                return _aCommonInternalDal.DataContainerDataTable(query, aParameters, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public DataTable GetApprovalDependency(string EmpID )
        {
            try
            {
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@EmpInfoId", EmpID));
                                 DataTable aTable = _aCommonInternalDal.GetDataByStoreProcedure("sp_AllApprovalInfo", aParameters,
            DataBase.HRDB);
                return aTable;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public DataTable GetAppraisalByKpiPermissionDashBoard_New(string EmpID, string param)
        {
            try
            {
                string query = @"SELECT top 1 ISNULL(app.BSCAppraisalSelfMasterId,0) BSCAppraisalSelfMasterId, e.EmpInfoId ,  MAX(isnull(b.ExtensionDate,b.DeadLine)) as ExtensionDate,
        ( 'Employee ID: '+ e.EmpMasterCode + ', Employee Name: ' + e.EmpName +    ISNULL(', Designation: ' + desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove,
        app.CurrentStatus, app.ActionStatus, (CASE WHEN app.ActionStatus<>'Approved' then 'Not Approved' ELSE app.ActionStatus END)AS KPIActionStatus ,ForEmp.EmpName as PendingEmp
        FROM    dbo.tblBSCKpiDeadlineMaster A
        LEFT JOIN dbo.tblBSCKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN dbo.tblBSCAppraisalSelfMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId 
        
	LEFT  JOIN (SELECT BSCAppraisalSelfMasterId,MAX(Version)MaxVer FROM dbo.tblBSCAppraisalSelfAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY BSCAppraisalSelfMasterId) AS CELog ON CELog.BSCAppraisalSelfMasterId= app.BSCAppraisalSelfMasterId
								LEFT  JOIN dbo.tblBSCAppraisalSelfAppLog ON tblBSCAppraisalSelfAppLog.BSCAppraisalSelfMasterId = app.BSCAppraisalSelfMasterId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblBSCAppraisalSelfAppLog.ForEmpInfoId
        WHERE b.EmpInfoId = " + Convert.ToInt32(EmpID) + param+@" 	group by ISNULL(app.BSCAppraisalSelfMasterId,0) , e.EmpInfoId ,
         e.EmpMasterCode , e.EmpName ,    desg.Designation,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove,
        app.CurrentStatus, app.ActionStatus, ForEmp.EmpName";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        public DataTable GetAppraisalByKpiPermissionSup(string EmpID, string FinancialYear, string param)
        {
            try
            {
                string query = @"SELECT  e.EmpInfoId ,  isnull(b.ExtensionDate,b.DeadLine) as ExtensionDate,
        ( 'Employee ID: '+ e.EmpMasterCode + ', Employee Name: ' + e.EmpName +    ISNULL(', Designation: ' + desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove,
        app.CurrentStatus,  app.ActionStatus, '' KPIActionStatus ,'' PendingEmp
        FROM    dbo.tblBSCKpiDeadlineMaster A
        LEFT JOIN dbo.tblBSCKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN dbo.tblBSCAppraisalSelfMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId 
	 
        

								 
        WHERE e.ReportingEmpId =  " + Convert.ToInt32(EmpID) + " and A.FinancialYearId=" + FinancialYear + param;

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        public DataTable GetAppraisalByKpiPermissionSupss(string EmpID, string FinancialYear, string param)
        {
            try
            {
                string query = @"SELECT  e.EmpInfoId ,  isnull(b.ExtensionDate,b.DeadLine) as ExtensionDate,
        ( 'Employee ID: '+ e.EmpMasterCode + ', Employee Name: ' + e.EmpName +    ISNULL(', Designation: ' + desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove,
        app.CurrentStatus,  app.ActionStatus, '' KPIActionStatus ,'' PendingEmp
        FROM    dbo.tblBSCKpiDeadlineMaster A
        LEFT JOIN dbo.tblBSCKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN dbo.tblBSCAppraisalSelfMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId 
	 
        

								 
        WHERE e.ReportingEmpId =  " + Convert.ToInt32(EmpID) + " and A.FinancialYearId=" + FinancialYear + param+"" +
                               "" +
                               @"SELECT  e.EmpInfoId ,  isnull(b.ExtensionDate,b.DeadLine) as ExtensionDate,
        ( 'Employee ID: '+ e.EmpMasterCode + ', Employee Name: ' + e.EmpName +    ISNULL(', Designation: ' + desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove,
        app.CurrentStatus,  app.ActionStatus, '' KPIActionStatus ,'' PendingEmp
        FROM    dbo.tblBSCKpiDeadlineMaster A
        LEFT JOIN dbo.tblBSCKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN dbo.tblBSCAppraisalSelfMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId 
	 
              
 	 inner JOIN   tblEmpAllRefference reff  ON e.ReportingEmpId = reff.RefferenceEmpId 

							inner join (select   NewEmployeeId,OnlyViewComId from tblEmpSpecialTransfer where OnlyView=1) tblPer on reff.EmployeeId =tblPer.NewEmployeeId

								 
        WHERE    e.ReportingEmpId =  " + Convert.ToInt32(EmpID) + " and A.FinancialYearId=" + FinancialYear + param;

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public DataTable GetAppraisalByKpiPermission2(string EmpID)
        {
            try
            {
                string query = @" SELECT * FROM 
		   dbo.tblBSCKpiDeadlineMaster A
        LEFT JOIN dbo.tblBSCKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
		where
         b.EmpInfoId =  " + Convert.ToInt32(EmpID) +
                               "  and isnull(b.ExtensionDate,b.DeadLine)>=CONVERT(date,GETDATE())    ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        public DataTable GetAppraisalByKpiPermission2FinYear(string EmpID, string FinId)
        {
            try
            {
                string query = @" SELECT * FROM 
		   dbo.tblBSCKpiDeadlineMaster A
    LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN dbo.tblBSCKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
		where
         b.EmpInfoId =  " + Convert.ToInt32(EmpID) +
                               "  and isnull(b.ExtensionDate,b.DeadLine)>=CONVERT(date,GETDATE())   and  y.FinancialYearDesc=  '" + FinId+"'";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public DataTable GetAppraisalPermission2FinYear(string EmpID, string FinId)
        {
            try
            {
                string query = @"  SELECT * FROM 
		   dbo.tblBSCAppraisalDeadlineMaster A
        LEFT JOIN dbo.tblBSCAppraisalDeadLineDetails b ON A.BSCAppraisalDeadLineMasterId = b.BSCAppraisalDeadLineMasterId
		 LEFT JOIN dbo.tblFinancialYear fin ON A.FinancialYearId = fin.FinancialYearId
		where
         b.EmpInfoId =  " + Convert.ToInt32(EmpID) +
                               "  and isnull(b.ExtensionDate,b.DeadLine)>=CONVERT(date,GETDATE())   and fin.FinancialYearDesc=  '" + FinId+"'";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

//        }public DataTable GetAppraisalByKpiPermission(string EmpID)
//        {
//            try
//            {
//                string query = @"SELECT  e.EmpInfoId ,
//        ( e.EmpMasterCode + ':' + e.EmpName + ':' + desg.Designation ) employee ,
//        dpt.DepartmentName ,
//        c.CompanyName ,
//        a.FinancialYearId,
//        y.FinancialYearDesc,
//		app.IsApprove,
//        app.CurrentStatus
//        FROM    dbo.tblBSCKpiDeadlineMaster A
//        LEFT JOIN dbo.tblBSCKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
//        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
//        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
//        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
//        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
//        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
//		LEFT JOIN dbo.tblBSCAppraisalSelfMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId 
//        left JOIN (SELECT BSCAppraisalSelfMasterId,MAX(Version)MaxVer FROM dbo.tblBSCAppraisalSelfAppLog  GROUP BY BSCAppraisalSelfMasterId) AS CELog ON CELog.BSCAppraisalSelfMasterId= app.BSCAppraisalSelfMasterId
//								left JOIN dbo.tblBSCAppraisalSelfAppLog ON tblBSCAppraisalSelfAppLog.BSCAppraisalSelfMasterId = app.BSCAppraisalSelfMasterId
//
//								 
//        WHERE b.EmpInfoId =  " + Convert.ToInt32(EmpID) + " and (b.DeadLine >= CONVERT(date,GETDATE())  OR   b.ExtensionDate >= CASE WHEN b.ExtensionDate IS NULL THEN NULL ELSE CONVERT(date,GETDATE()) END ) AND (Version=MaxVer or Version is  null) and (ForEmpInfoId='" + Convert.ToInt32(EmpID) + "' or ForEmpInfoId is null)   ";

//                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }



//        }

        public DataTable GetApprsaisalSelfByEmpFinYear(int emp, int year)
        {
            try
            {
                string query = @"	SELECT A.BSCAppraisalSelfMasterId, isnull(mas.BSCAppraisalMasterId ,0)BSCAppraisalMasterId FROM dbo.tblBSCAppraisalSelfMaster a
left join tblMBSCAppraisalMaster mas on mas.BSCAppraisalSelfMasterId=a.BSCAppraisalSelfMasterId  WHERE a.EmpInfoId = " + emp +
                    " AND A.FinancialYearId = " + year + "";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable GetCheckApprisalAlreadyExist(int BSCAppraisalSelfMasterId)
        {
            try
            {
                string query = @"	SELECT  BSCAppraisalSelfMasterId,BSCAppraisalMasterId FROM dbo.tblMBSCAppraisalMaster   WHERE  BSCAppraisalSelfMasterId = " + BSCAppraisalSelfMasterId;

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public bool DeleteAppraisalSetupNew(int master)
        {
            try
            {
                bool result = false;
                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@BSCAppraisalMasterId", master));


                string query = @"   INSERT INTO [dbo].[DELtblAppraisalMaster]
           ([BSCAppraisalMasterId]
      ,[FinancialYearId]
      ,[EmpInfoId]
      ,[EntryDate]
      ,[EntryBy]
      ,[UpdateBy]
      ,[UpdateDate]
      ,[IsDelete]
      ,[DeleteBy]
      ,[IsApprove]
      ,[ApproveBy]
      ,[ApproveDate]
      ,[BSCAppraisalSelfMasterId]
      ,[ActionVersion]
      ,[CurrentStatus]
      ,[SelfApprove])
    
SELECT [BSCAppraisalMasterId]
      ,[FinancialYearId]
      ,[EmpInfoId]
      ,[EntryDate]
      ,[EntryBy]
      ,[UpdateBy]
      ,[UpdateDate]
      ,[IsDelete]
      ,[DeleteBy]
      ,[IsApprove]
      ,[ApproveBy]
      ,[ApproveDate]
      ,[BSCAppraisalSelfMasterId]
      ,[ActionVersion]
      ,[CurrentStatus]
      ,[SelfApprove]
  FROM [dbo].[tblMBSCAppraisalMaster] where BSCAppraisalMasterId=@BSCAppraisalMasterId




  INSERT INTO [dbo].[DELtblBSCAppraisalSelfFuncArea]
           ([AppraisalFucAreaId]
      ,[BSCAppraisalMasterId]
      ,[KpiInfo]
      ,[KpiWeight]
      ,[Deadline]
      ,[MidYearStatus]
      ,[ResultYearEnd]
      ,[SelfMark]
      ,[SupervisorMark]
      ,[Target]
      ,[BSCAppraisalSelfMasterId]
      ,[KpiWeightPer]
      ,[TargetPer]
      ,[AppraisalSelfFucAreaId])
    
SELECT [AppraisalFucAreaId]
      ,[BSCAppraisalMasterId]
      ,[KpiInfo]
      ,[KpiWeight]
      ,[Deadline]
      ,[MidYearStatus]
      ,[ResultYearEnd]
      ,[SelfMark]
      ,[SupervisorMark]
      ,[Target]
      ,[BSCAppraisalSelfMasterId]
      ,[KpiWeightPer]
      ,[TargetPer]
      ,[AppraisalSelfFucAreaId]
  FROM [dbo].[tblBSCAppraisalSelfFuncArea] where BSCAppraisalMasterId=@BSCAppraisalMasterId





    INSERT INTO [dbo].[DELtblAppraisalBehaveArea]
           ([AppraisalBehaveId]
      ,[BSCAppraisalMasterId]
      ,[SkillInfo]
      ,[SupportingEmp]
      ,[Score]
      ,[BSCAppraisalSelfMasterId]
      ,[SupervisorScore]
      ,[SetScore]
      ,[SelfScore])
    
SELECT [AppraisalBehaveId]
      ,[BSCAppraisalMasterId]
      ,[SkillInfo]
      ,[SupportingEmp]
      ,[Score]
      ,[BSCAppraisalSelfMasterId]
      ,[SupervisorScore]
      ,[SetScore]
      ,[SelfScore]
  FROM [dbo].[tblAppraisalBehaveArea] where BSCAppraisalMasterId=@BSCAppraisalMasterId


INSERT INTO [dbo].[DeLtblAppraisalTrainingNeeds]
           ([AppraisalTrainingId]
      ,[BSCAppraisalMasterId]
      ,[TrainingNeeds]
      ,[TrainingStart]
      ,[TrainingEnd]
      ,[TrainingType]
      ,[Quater])
    
SELECT [AppraisalTrainingId]
      ,[BSCAppraisalMasterId]
      ,[TrainingNeeds]
      ,[TrainingStart]
      ,[TrainingEnd]
      ,[TrainingType]
      ,[Quater]
  FROM [dbo].[tblAppraisalTrainingNeeds] where BSCAppraisalMasterId=@BSCAppraisalMasterId





   INSERT INTO [dbo].[DELtblAppraisalFinalStatus]
           ([ApprisalFinalStatusId]
      ,[BSCAppraisalMasterId]
      ,[TotalScore]
      ,[FinalStatus]
      ,[GeneralIncrement]
      ,[SpecialIncrement]
      ,[SpecialStep]
      ,[SpecialStepPercent]
      ,[IsPromotion]
      ,[Pip]
      ,[Other]
      ,[Note])
    
SELECT [ApprisalFinalStatusId]
      ,[BSCAppraisalMasterId]
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
  FROM [dbo].[tblAppraisalFinalStatus] where BSCAppraisalMasterId=@BSCAppraisalMasterId



 INSERT INTO [dbo].[DELtblAppraisalMasterAppLog]
           ([AppraisalMasterAppLogId]
      ,[BSCAppraisalMasterId]
      ,[PreEmpInfoId]
      ,[ForEmpInfoId]
      ,[Version]
      ,[ApproveBy]
      ,[ApproveDate]
      ,[ActionStatus]
      ,[Comments]
      ,[CommentEmpId])
    
SELECT [AppraisalMasterAppLogId]
      ,[BSCAppraisalMasterId]
      ,[PreEmpInfoId]
      ,[ForEmpInfoId]
      ,[Version]
      ,[ApproveBy]
      ,[ApproveDate]
      ,[ActionStatus]
      ,[Comments]
      ,[CommentEmpId]
  FROM [dbo].tblAppraisalMasterAppLog where BSCAppraisalMasterId=@BSCAppraisalMasterId


	  DELETE FROM [dbo].[tblMBSCAppraisalMaster]
      WHERE BSCAppraisalMasterId=@BSCAppraisalMasterId


	  DELETE FROM [dbo].[tblBSCAppraisalSelfFuncArea]
      WHERE BSCAppraisalMasterId=@BSCAppraisalMasterId


	    DELETE FROM [dbo].[tblAppraisalBehaveArea]
      WHERE BSCAppraisalMasterId=@BSCAppraisalMasterId


	  
	    DELETE FROM [dbo].tblAppraisalTrainingNeeds
      WHERE BSCAppraisalMasterId=@BSCAppraisalMasterId


	  	  
	    DELETE FROM [dbo].tblAppraisalFinalStatus
      WHERE BSCAppraisalMasterId=@BSCAppraisalMasterId




DELETE FROM [dbo].[tblAppraisalMasterAppLog]
      WHERE BSCAppraisalMasterId=@BSCAppraisalMasterId

";

                result = _aCommonInternalDal.DeleteDataByDeleteCommand(query, aParameters, DataBase.HRDB);
                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


//        public DataTable GetSelfAppraisalListApprove(string actionstatus, string empId)
//        {
//            try
//            {
//                actionstatus = "Drafted";
//                string query = @"select A.EmpInfoId,A.FinancialYearId,a.BSCAppraisalSelfMasterId , (b.EmpMasterCode+':'+b.EmpName+':'+desg.Designation) employee ,dpt.DepartmentName ,c.CompanyName , y.FinancialYearDesc from  tblBSCAppraisalSelfMaster A 
//                left join tblEmpGeneralInfo B on a.EmpInfoId = b.EmpInfoId
//                left join tblCompanyInfo c on b.CompanyId = c.CompanyId
//                left join tblFinancialYear y on a.FinancialYearId = y.FinancialYearId
//                left join tblDesignation desg on b.DesignationId = desg.DesignationId
//                left join tblDepartment dpt on  b.departmentId = dpt.departmentId
//                LEFT JOIN dbo.tblAppraisalDeadlineMaster appM ON a.FinancialYearId = appM.FinancialYearId AND appM.CompanyId = b.CompanyId
//		        LEFT JOIN dbo.tblAppraisalDeadLineDetails appD ON b.ReportingEmpId = appD.EmpinfoId  AND appM.AppraisalDeadLineMasterId = appD.AppraisalDeadLineMasterId
//                where (a.IsDelete is null or a.IsDelete = 0) and  A.ActionStatus='" + actionstatus + "'   AND B.ReportingEmpId =  " + Convert.ToInt32(empId) + " AND b.IsActive = 1 ";
//                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
//                //               string query = @"select A.EmpInfoId,A.FinancialYearId,a.BSCAppraisalSelfMasterId , (b.EmpMasterCode+':'+b.EmpName+':'+desg.Designation) employee ,dpt.DepartmentName ,c.CompanyName , y.FinancialYearDesc from  tblBSCAppraisalSelfMaster A 
//                //                left join tblEmpGeneralInfo B on a.EmpInfoId = b.EmpInfoId
//                //                left join tblCompanyInfo c on b.CompanyId = c.CompanyId
//                //                left join tblFinancialYear y on a.FinancialYearId = y.FinancialYearId
//                //                left join tblDesignation desg on b.DesignationId = desg.DesignationId
//                //                left join tblDepartment dpt on  b.departmentId = dpt.departmentId
//                //                LEFT JOIN dbo.tblAppraisalDeadlineMaster appM ON a.FinancialYearId = appM.FinancialYearId AND appM.CompanyId = b.CompanyId
//                //		        INNER JOIN dbo.tblAppraisalDeadLineDetails appD ON b.ReportingEmpId = appD.EmpinfoId  AND appM.AppraisalDeadLineMasterId = appD.AppraisalDeadLineMasterId
//                //                where (a.IsDelete is null or a.IsDelete = 0) and  A.ActionStatus='" + actionstatus + "'   AND B.ReportingEmpId =  " + Convert.ToInt32(empId) + " AND b.IsActive = 1 " +
//                //                              "AND ( appD.DeadLine >= CONVERT(DATE , GETDATE()) OR appD.ExtensionDate >= CASE WHEN appD.ExtensionDate IS NULL THEN NULL ELSE CONVERT(DATE , GETDATE()) END    ) " +
//                //                              "or  ( B.ReportingEmpId IN ( SELECT   EmpInfoId FROM  dbo.tblEmpGeneralInfo WHERE    ReportingEmpId = " + Convert.ToInt32(empId) + " AND IsActive = 1 AND A.ActionVersion > 0 AND A.ActionStatus = 'Posted' ) )   ";
//                //               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
//            }
//            catch (Exception exception)
//            {

//                throw exception;
//            }

//        }
        public DataTable GetSelfAppraisalListApprove(string actionstatus, string empId)
        {
            try
            {
                actionstatus = "Drafted";
                string query = @"select   appD.OptionInfo, A.EmpInfoId,A.FinancialYearId,a.BSCAppraisalSelfMasterId ,  b.EmpMasterCode,b.EmpName,  ISNULL(+desg.Designation,'') Designation ,dpt.DepartmentName ,c.CompanyName , y.FinancialYearDesc,* from  tblBSCAppraisalSelfMaster A 
                left join tblEmpGeneralInfo B on a.EmpInfoId = b.EmpInfoId
                left join tblCompanyInfo c on b.CompanyId = c.CompanyId
                left join tblFinancialYear y on a.FinancialYearId = y.FinancialYearId
                left join tblDesignation desg on b.DesignationId = desg.DesignationId
                left join tblDepartment dpt on  b.departmentId = dpt.departmentId
                INNER JOIN dbo.tblBSCKpiDeadlineMaster appM ON a.FinancialYearId = appM.FinancialYearId AND appM.CompanyId = b.CompanyId
		        INNER JOIN dbo.tblBSCKPIDeadLineDetails appD ON b.EmpInfoId = appD.EmpinfoId  AND appM.KPIDeadLineMasterId = appD.KPIDeadLineMasterId
                INNER JOIN (SELECT BSCAppraisalSelfMasterId,MAX(Version)MaxVer FROM dbo.tblBSCAppraisalSelfAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY BSCAppraisalSelfMasterId) AS CELog ON CELog.BSCAppraisalSelfMasterId= A.BSCAppraisalSelfMasterId
								INNER JOIN dbo.tblBSCAppraisalSelfAppLog ON tblBSCAppraisalSelfAppLog.BSCAppraisalSelfMasterId = A.BSCAppraisalSelfMasterId
                                where (a.IsDelete is null or a.IsDelete = 0) and Version=CELog.MaxVer and  B.EmpInfoId NOT IN (SELECT EmployeeId from tblEmployeeJobLeft)  and    ForEmpInfoId = '" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'";
                
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
                //               string query = @"select A.EmpInfoId,A.FinancialYearId,a.BSCAppraisalSelfMasterId , (b.EmpMasterCode+':'+b.EmpName+':'+desg.Designation) employee ,dpt.DepartmentName ,c.CompanyName , y.FinancialYearDesc from  tblBSCAppraisalSelfMaster A 
                //                left join tblEmpGeneralInfo B on a.EmpInfoId = b.EmpInfoId
                //                left join tblCompanyInfo c on b.CompanyId = c.CompanyId
                //                left join tblFinancialYear y on a.FinancialYearId = y.FinancialYearId
                //                left join tblDesignation desg on b.DesignationId = desg.DesignationId
                //                left join tblDepartment dpt on  b.departmentId = dpt.departmentId
                //                LEFT JOIN dbo.tblAppraisalDeadlineMaster appM ON a.FinancialYearId = appM.FinancialYearId AND appM.CompanyId = b.CompanyId
                //		        INNER JOIN dbo.tblAppraisalDeadLineDetails appD ON b.ReportingEmpId = appD.EmpinfoId  AND appM.AppraisalDeadLineMasterId = appD.AppraisalDeadLineMasterId
                //                where (a.IsDelete is null or a.IsDelete = 0) and  A.ActionStatus='" + actionstatus + "'   AND B.ReportingEmpId =  " + Convert.ToInt32(empId) + " AND b.IsActive = 1 " +
                //                              "AND ( appD.DeadLine >= CONVERT(DATE , GETDATE()) OR appD.ExtensionDate >= CASE WHEN appD.ExtensionDate IS NULL THEN NULL ELSE CONVERT(DATE , GETDATE()) END    ) " +
                //                              "or  ( B.ReportingEmpId IN ( SELECT   EmpInfoId FROM  dbo.tblEmpGeneralInfo WHERE    ReportingEmpId = " + Convert.ToInt32(empId) + " AND IsActive = 1 AND A.ActionVersion > 0 AND A.ActionStatus = 'Posted' ) )   ";
                //               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
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
                string query = @"select b.CompanyId,  A.* , (b.EmpMasterCode+':'+b.EmpName) as employee from tblBSCAppraisalSelfMaster A 
                left join tblEmpGeneralInfo b on a.EmpInfoId = b.EmpInfoId 
                    where A.BSCAppraisalSelfMasterId =  " + id + "";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetAppraisalMAstet(int id)
        {

            try
            {
                string query = @"SELECT f.FinancialYearDesc 
FROM    tblBSCAppraisalSelfMaster A
LEFT JOIN dbo.tblFinancialYear f ON f.FinancialYearId = A.FinancialYearId

WHERE  A.BSCAppraisalSelfMasterId = " + id + "";
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
                string query = @"SELECT '' CommentsFunc,'' SupervisorComments,  1 IsActive, 0 AutoCalculationPerSup, 0 AS KpiWeightPer, A.BSCAppraisalSelfFucAreaId AppraisalSelfFucAreaId ,
        A.BSCAppraisalSelfMasterId AppraisalMasterId ,
        A.KPIMeasure KpiInfo ,
        A.KpiWeight ,
        [Dimension]
      ,[ObjectiveGoal]
      ,[KPIMeasure]
      ,[KpiWeight]
      ,[Initiatives],
         A.Deadline  Deadline ,
         format(A.Deadline ,'dd-MMM-yyyy') deadlineDate ,
        0 MidYearStatus ,
       0 ResultYearEnd ,
       0 SelfMark ,
       DimensionStr DimensionStr , DimensionStr DimensionStrHdn ,ObjectiveGoal  ObjectiveGoalHdn,
       cast( ROUND(A.KpiWeight * 1.2, 2) as decimal(18,2))	AutoCalculation,  '' AutoCalculationPer,
        KpiWeight Target,
        0 TargetPer,
	 Initiatives SupervisorMark,0 IsActive 
FROM    tblBSCAppraisalSelfFuncArea A

inner join tblBSCOKRDimentionOrder ord on A.Dimension=ord.BSCOKRDimentionId
WHERE
a.BSCAppraisalSelfMasterId  =" + id + "  order by ord.OrderById asc";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetAppraisalSelfDetailsRpt(int id, bool _Option)
        {

            try
            {
                string query = @"
SELECT  b.DimensionStr, b.DimensionStr BSCAppraisalSelfFucAreaId,  b.DimensionStr AppraisalSelfFucAreaId ,
         b.ObjectiveGoal AppraisalMasterId ,
        A.KPIMeasure KpiInfo ,
        A.KpiWeight ,
         b.Dimension
      , b.ObjectiveGoal
      , b.KPIMeasure
      , b.KpiWeight
      , b.Initiatives,
        CONVERT(NVARCHAR(11),  b.Deadline, 106) AS Deadline ,
        b.MidYearStatus MidYearStatus ,
        b.ObjectiveGoal ResultYearEnd ,
       b.KpiWeight SelfMark ,
       
         b.KpiWeight Target,
         b.Initiatives TargetPer,
	  b.ObjectiveGoal SupervisorMark,'" + _Option + @"' IsActive
FROM    tblBSCAppraisalSelfFuncArea A 
inner join tblBSCOKRDimentionOrder ord on A.Dimension=ord.BSCOKRDimentionId
inner join tblMBSCAppraisalFuncArea b on a.BSCAppraisalSelfFucAreaId=b.BSCAppraisalSelfFucAreaId
WHERE  ISNULL(b.IsActive,0)=1 and  
b.BSCAppraisalMasterId  =" + id + "  order by ord.OrderById asc";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        

        public DataTable GetAppraisalSelfFuncRpt(int id, bool _Option)
        {

            try
            {
                string query = @"
SELECT  A.DimensionStr, A.DimensionStr BSCAppraisalSelfFucAreaId,  A.DimensionStr AppraisalSelfFucAreaId ,
         A.ObjectiveGoal AppraisalMasterId ,
        A.KPIMeasure KpiInfo ,
        A.KpiWeight ,
         A.Dimension
      , A.ObjectiveGoal
      , A.KPIMeasure
      , A.KpiWeight
      , A.Initiatives,
        CONVERT(NVARCHAR(11),  A.Deadline, 106) AS Deadline ,
        A.MidYearStatus MidYearStatus ,
        A.ObjectiveGoal ResultYearEnd ,
       A.KpiWeight SelfMark ,
       
         A.KpiWeight Target,
         A.Initiatives TargetPer,
	  A.ObjectiveGoal SupervisorMark,'" + _Option + @"' IsActive
FROM    tblBSCAppraisalSelfFuncArea A 
inner join tblBSCOKRDimentionOrder ord on A.Dimension=ord.BSCOKRDimentionId 
WHERE    
A.BSCAppraisalSelfMasterId=" + id + "  order by ord.OrderById asc";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        

        public DataTable GetAppraisalFuncRpt(int id, bool _Option)
        {

            try
            {
                string query = @"
SELECT  b.DimensionStr, b.DimensionStr BSCAppraisalSelfFucAreaId,  b.DimensionStr AppraisalSelfFucAreaId ,
         b.ObjectiveGoal AppraisalMasterId ,
        A.KPIMeasure KpiInfo ,
        A.KpiWeight ,
         b.Dimension
      , b.ObjectiveGoal
      , b.KPIMeasure
      , b.KpiWeight
      , b.Initiatives,
        CONVERT(NVARCHAR(11),  b.Deadline, 106) AS Deadline ,
        b.MidYearStatus MidYearStatus ,
        b.ObjectiveGoal ResultYearEnd ,
       b.SelfMark SelfMark ,
       
         b.KpiWeight Target,
         b.Initiatives TargetPer,
	  b.SupervisorMark SupervisorMark,'" + _Option + @"' IsActive
FROM    tblBSCAppraisalSelfFuncArea A 
inner join tblBSCOKRDimentionOrder ord on A.Dimension=ord.BSCOKRDimentionId
inner join tblMBSCAppraisalFuncArea b on a.BSCAppraisalSelfFucAreaId=b.BSCAppraisalSelfFucAreaId
WHERE  ISNULL(b.IsActive,0)=1 and  
b.BSCAppraisalMasterId  =" + id + "  order by ord.OrderById asc";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable GetAppraisalFuncDetails(int id)
        {

            try
            {
                string query = @"SELECT DimensionStr, A.BSCAppraisalSelfFucAreaId AppraisalSelfFucAreaId ,
        A.BSCAppraisalSelfMasterId AppraisalMasterId ,
        A.KPIMeasure KpiInfo ,
        A.KpiWeight ,
        [Dimension]
      ,[ObjectiveGoal]
      ,[KPIMeasure]
      ,[KpiWeight]
      ,[Initiatives],
        CONVERT(NVARCHAR(11), A.Deadline, 106) AS Deadline ,
        0 MidYearStatus ,
       0 ResultYearEnd ,
       0 SelfMark ,
       
        KpiWeight Target,
        0 TargetPer,
	 Initiatives SupervisorMark,0 IsActive
FROM    tblBSCAppraisalSelfFuncArea A

inner join tblBSCOKRDimentionOrder ord on A.Dimension=ord.BSCOKRDimentionId
WHERE
a.BSCAppraisalSelfMasterId  = " + id + "   order by ord.OrderById asc ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetAppraisalSelfDetailsNew(int id)
        {

            try
            {
                string query = @"SELECT '' KpiInfo, A.Initiatives,  A.BSCAppraisalSelfFucAreaId AppraisalSelfFucAreaId ,
        A.BSCAppraisalSelfMasterId AppraisalSelfMasterId ,
        A.DimensionStr  ,
        A.Dimension  ,
		A.ObjectiveGoal,
		A.KPIMeasure,  
        A.KpiWeight ,
	  cast( ROUND(A.KpiWeight * 1.2, 2) as decimal(18,2))	AutoCalculation,  '' AutoCalculationPer,
        0 KpiWeightPer ,
        CONVERT(NVARCHAR(11), A.Deadline, 106) AS Deadline ,
        MidYearStatus as  MidYearStatus ,
        null as  ResultYearEnd ,
        SelfMark as   SelfMark ,
        '' as  CommentsFunc ,
        
        A.Target,
        A.TargetPer,
		null as  SupervisorMark,ISNULL(A.IsActive,1) IsActive
FROM    tblBSCAppraisalSelfFuncArea A
inner join tblBSCOKRDimentionOrder ord on A.Dimension=ord.BSCOKRDimentionId
WHERE ISNULL(A.IsActive,1)=1 and   A.BSCAppraisalSelfMasterId =  " + id + "  order by ord.OrderById asc";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetAppraisalSelfDetailsLatest(int id)
        {

            try
            {
                string query = @"WITH CTE AS (
    SELECT '' CommentsFunc, OrderById, 
        '' AS KpiInfo,
        A.Initiatives,
        A.BSCAppraisalSelfFucAreaId AS AppraisalSelfFucAreaId,
        A.BSCAppraisalSelfMasterId AS AppraisalSelfMasterId,
        A.DimensionStr,
        A.Dimension,
        A.ObjectiveGoal,
        A.KPIMeasure,
        A.KpiWeight,
        CAST(ROUND(A.KpiWeight * 1.2, 2) AS DECIMAL(18, 2)) AS AutoCalculation,
        '' AS AutoCalculationPer,
        0 AS KpiWeightPer,
        CONVERT(NVARCHAR(11), A.Deadline, 106) AS Deadline,
        A.MidYearStatus,
        NULL AS ResultYearEnd,
        A.SelfMark,
        A.Target,
        A.TargetPer,
        NULL AS SupervisorMark,
        ISNULL(A.IsActive, 1) AS IsActive,
        ROW_NUMBER() OVER (PARTITION BY A.DimensionStr, A.ObjectiveGoal ORDER BY ord.OrderById) AS RowNum
    FROM tblBSCAppraisalSelfFuncArea A
    INNER JOIN tblBSCOKRDimentionOrder ord 
        ON A.Dimension = ord.BSCOKRDimentionId
    WHERE ISNULL(A.IsActive, 1) = 1
        AND A.BSCAppraisalSelfMasterId =   " + id + @" 
)
SELECT CommentsFunc, DimensionStr DimensionStrHdn,ObjectiveGoal  ObjectiveGoalHdn,
    CASE WHEN RowNum = 1 THEN DimensionStr ELSE '' END AS DimensionStr,
    CASE WHEN RowNum = 1 THEN ObjectiveGoal ELSE '' END AS ObjectiveGoal,
    KpiInfo,
    Initiatives,
    AppraisalSelfFucAreaId,
    AppraisalSelfMasterId,
    Dimension,
    KPIMeasure,
    KpiWeight,
    AutoCalculation,
    AutoCalculationPer,
    KpiWeightPer,
    Deadline,
    MidYearStatus,
    ResultYearEnd,
    SelfMark,
    Target,
    TargetPer,
    SupervisorMark,
    IsActive
FROM CTE
ORDER BY   OrderById;";
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
                string query = @"



SELECT '' KpiInfo, A.Initiatives,  A.BSCAppraisalSelfFucAreaId AppraisalSelfFucAreaId ,
        A.BSCAppraisalSelfMasterId AppraisalSelfMasterId ,
        A.DimensionStr  ,
        A.Dimension  ,
		A.ObjectiveGoal,
		A.KPIMeasure,
        A.KpiWeight ,
        0 KpiWeightPer ,
        CONVERT(NVARCHAR(11), A.Deadline, 106) AS Deadline ,
        b.MidYearStatus as  MidYearStatus ,
        b.ResultYearEnd as  ResultYearEnd ,
        b.SelfMark as   SelfMark ,
        ISNULL(b.CommentsFunc,'') as CommentsFunc ,
        ISNULL(b.SupervisorComments,'') as SupervisorComments ,
            cast( ROUND(b.KpiWeight * 1.2, 2) as decimal(18,2)) AS AutoCalculation,    'Achi: '+ cast(cast(ROUND(((b.SelfMark/ b.KpiWeight)*100),2) as decimal(18,2)) as nvarchar(max))+'%'  AutoCalculationPer,    cast( ROUND(b.KpiWeight * 1.2, 2) as decimal(18,2)) AS AutoCalculation,  case when ISNULL(b.SupervisorMark,0)=0 then  '' else  'Achi: '+ cast(cast(ROUND(((b.SelfMark/ b.KpiWeight)*100),2) as decimal(18,2)) as nvarchar(max))+'%' end  AutoCalculationPerSup,
        A.Target,
        A.TargetPer,
		ISNULL(b.SupervisorMark,0)  as  SupervisorMark,ISNULL(b.IsActive,0) IsActive
FROM    tblBSCAppraisalSelfFuncArea A
inner join tblBSCOKRDimentionOrder ord on A.Dimension=ord.BSCOKRDimentionId
inner join tblMBSCAppraisalFuncArea b on a.BSCAppraisalSelfFucAreaId=b.BSCAppraisalSelfFucAreaId
WHERE ISNULL(b.IsActive,0)=1 and     A.BSCAppraisalSelfMasterId = " + id + "";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetAppraisalfDetailsFromSupLatest(int id)
        {

            try
            {
                string query = @"


 

WITH CTE AS (
    SELECT  CommentsFunc,
        ISNULL(b.SupervisorComments,'') as SupervisorComments,
        '' AS KpiInfo,
        b.Initiatives,
        b.BSCAppraisalSelfFucAreaId AS AppraisalSelfFucAreaId,
        b.BSCAppraisalSelfMasterId AS AppraisalSelfMasterId,
        b.DimensionStr,
        b.Dimension,
        b.ObjectiveGoal,
        b.KPIMeasure,
        b.KpiWeight,
        0 AS KpiWeightPer,
        CONVERT(NVARCHAR(11), A.Deadline, 106) AS Deadline,
        b.MidYearStatus AS MidYearStatus,
        b.ResultYearEnd AS ResultYearEnd,
        b.SelfMark AS SelfMark,
        CAST(ROUND(b.KpiWeight * 1.2, 2) AS DECIMAL(18, 2)) AS AutoCalculation,
        'Achi: ' + CAST(CAST(ROUND(((b.SelfMark / b.KpiWeight) * 100), 2) AS DECIMAL(18, 2)) AS NVARCHAR(MAX)) + '%' AS AutoCalculationPer,
       
        CASE 
            WHEN ISNULL(b.SupervisorMark, 0) = 0 THEN '' 
            ELSE 'Achi: ' + CAST(CAST(ROUND(((b.SelfMark / b.KpiWeight) * 100), 2) AS DECIMAL(18, 2)) AS NVARCHAR(MAX)) + '%' 
        END AS AutoCalculationPerSup,
        A.Target,
        A.TargetPer,
         b.SupervisorMark  AS SupervisorMark,
        ISNULL(b.IsActive, 0) AS IsActive,
        ROW_NUMBER() OVER (PARTITION BY b.DimensionStr, b.ObjectiveGoal ORDER BY ord.OrderById) AS RowNum,OrderById
    FROM tblBSCAppraisalSelfFuncArea A
    INNER JOIN tblBSCOKRDimentionOrder ord 
        ON A.Dimension = ord.BSCOKRDimentionId
    INNER JOIN tblMBSCAppraisalFuncArea b 
        ON A.BSCAppraisalSelfFucAreaId = b.BSCAppraisalSelfFucAreaId
    WHERE ISNULL(b.IsActive, 0) = 1
        AND A.BSCAppraisalSelfMasterId = " + id + @"
)
SELECT   CommentsFunc,SupervisorComments, DimensionStr DimensionStrHdn,ObjectiveGoal  ObjectiveGoalHdn,
    CASE WHEN RowNum = 1 THEN DimensionStr ELSE '' END AS DimensionStr,
    CASE WHEN RowNum = 1 THEN ObjectiveGoal ELSE '' END AS ObjectiveGoal,
    KpiInfo,
    Initiatives,
    AppraisalSelfFucAreaId,
    AppraisalSelfMasterId,
    Dimension,
    KPIMeasure,
    KpiWeight,
    KpiWeightPer,
    Deadline,
    MidYearStatus,
    ResultYearEnd,
    SelfMark,
    AutoCalculation,
    AutoCalculationPer,
    AutoCalculationPerSup,
    Target,
    TargetPer,
    SupervisorMark,
    IsActive
FROM CTE
ORDER BY    OrderById;
 ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetAppraisalfDetailsFromSupRpt(int id)
        {

            try
            {
                string query = @"



SELECT '' KpiInfo, A.Initiatives,  A.BSCAppraisalSelfFucAreaId AppraisalSelfFucAreaId ,
        A.BSCAppraisalSelfMasterId AppraisalSelfMasterId ,
        A.DimensionStr  ,
        A.Dimension  ,
		A.ObjectiveGoal,
		A.KPIMeasure,
        A.KpiWeight ,
        0 KpiWeightPer ,
        CONVERT(NVARCHAR(11), A.Deadline, 106) AS Deadline ,
        b.MidYearStatus as  MidYearStatus ,
        b.ResultYearEnd as  ResultYearEnd ,
        b.SelfMark as   SelfMark ,
       
        A.Target,
        A.TargetPer,
		ISNULL(b.SupervisorMark,0)  as  SupervisorMark,ISNULL(b.IsActive,0) IsActive
FROM    tblBSCAppraisalSelfFuncArea A
inner join tblBSCOKRDimentionOrder ord on A.Dimension=ord.BSCOKRDimentionId
inner join tblMBSCAppraisalFuncArea b on a.BSCAppraisalSelfFucAreaId=b.BSCAppraisalSelfFucAreaId
WHERE ISNULL(b.IsActive,0)=1 and     A.BSCAppraisalSelfMasterId = " + id + "";
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
                string query = @"SELECT 0 SelfScore,  *
FROM    tblBSCAppraisalSelfBehaveArea A with (nolock)

WHERE  A.BSCAppraisalSelfMasterId = " + id + " ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetAppraisalSelfB_Rpt(int id)
        {
            try
            {
                string query = @"select  CAST(ISNULL(SetScore,0) as int) SetScore,  CAST(ISNULL(SetScore,0) as int) Score,*, 0 as SelfScore from tblBSCAppraisalSelfBehaveArea where BSCAppraisalSelfMasterId = " + id + " ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetAppraisalB_Rpt(int id)
        {
            try
            {
                string query = @"select  CAST(ISNULL(SetScore,0) as int) SetScore,  CAST(ISNULL(SelfScore,0) as int) Score,  ISNULL(SelfScore,0) as SelfScore,* from tblMBSCAppraisalBehaveArea where BSCAppraisalMasterId = " + id + " ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetAppraisalfB(int id)
        {
            try
            {
                string query = @"select  CAST(ISNULL(SetScore,0) as int) SetScore,	0	AppraisalSelfBehaveId ,
                                                    BSCAppraisalSelfMasterId ,
                                                    SkillInfo ,
                                                    SupportingEmp ,
                                                  0   Score ,
                                                    SupervisorScore ,
                                                    SetScore	, 0 as SelfScore from tblBSCAppraisalSelfBehaveArea WHERE
BSCAppraisalSelfMasterId=" + id + " ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public DataTable GetAppraisalfBView(int id)
        {
            try
            {
                string query = @"select  CAST(ISNULL(SetScore,0) as int) SetScore,	0	AppraisalSelfBehaveId ,
                                                    BSCAppraisalSelfMasterId ,
                                                    SkillInfo ,
                                                    SupportingEmp ,
                                                  0   Score ,
                                                    SupervisorScore ,
                                                    SetScore	, 0 as SelfScore from tblBSCAppraisalSelfBehaveArea WHERE
BSCAppraisalSelfMasterId=" + id + " ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetAppraisalPartB(int id)
        {
            try
            {
                string query = @"SELECT 
    ISNULL([Score], 0) AS Score,
    ISNULL([BSCAppraisalSelfMasterId], 0) AS BSCAppraisalSelfMasterId,
    ISNULL([SupervisorScore], 0) AS SupervisorScore,
    ISNULL([SelfScore], 0) AS SelfScore,
    ISNULL([SetScore], 0) AS SetScore,
    *
FROM 
    tblMBSCAppraisalBehaveArea 
 where BSCAppraisalSelfMasterId = " + id + " ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public DataTable GetAppraisalPartB_new(int id)
        {
            try
            {
                string query = @"SELECT 
    ISNULL([Score], 0) AS Score,
    ISNULL([BSCAppraisalSelfMasterId], 0) AS BSCAppraisalSelfMasterId,
    ISNULL([SupervisorScore], 0) AS SupervisorScore,
    ISNULL([SelfScore], 0) AS SelfScore,
    ISNULL([SetScore], 0) AS SetScore,
    *
FROM 
    tblMBSCAppraisalBehaveArea 
 where BSCAppraisalMasterId = " + id + " ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetAppraisalPartB_newd(int id)
        {
            try
            {
                string query = @"SELECT 
    ISNULL([Score], 0) AS Score,
    ISNULL([BSCAppraisalSelfMasterId], 0) AS BSCAppraisalSelfMasterId,
    ISNULL([SupervisorScore], 0) AS SupervisorScore,
    ISNULL([SelfScore], 0) AS SelfScore,
    ISNULL([SetScore], 0) AS SetScore,
    *
FROM 
    tblMBSCAppraisalBehaveArea 
 where BSCAppraisalMasterId = " + id + " ";
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
                string query = @"select A.AppraisalSelfFucAreaId, A.BSCAppraisalSelfMasterId, A.KpiInfo, A.KpiWeight, convert( nvarchar  (11),A.Deadline,106) as Deadline , A.MidYearStatus, A.ResultYearEnd, A.SelfMark, A.SupervisorMark, A.Target
   from tblAppraisalSelfFuncArea A left join tblBSCAppraisalSelfMaster B on a.BSCAppraisalSelfMasterId = b.BSCAppraisalSelfMasterId
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
                aParameters.Add(new SqlParameter("@BSCAppraisalSelfMasterId", finyear));


                string query =
                    @"update tblBSCAppraisalSelfMaster set IsPublish = 0 where BSCAppraisalSelfMasterId =@BSCAppraisalSelfMasterId ";

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
                aParameters.Add(new SqlParameter("@BSCAppraisalSelfMasterId", id));


                string query =
                    @"update tblBSCAppraisalSelfMaster set ApproveFromSup = 1 where BSCAppraisalSelfMasterId =@BSCAppraisalSelfMasterId ";

                bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetApproveLogBySelfMaster(int id)
        {
            //SELECT (e.EmpMasterCode+' : '+e.EmpName+':'+desg.Designation+''+dpt.DepartmentName) AS Employee , CONVERT(NVARCHAR( 11) , A.EntryDate , 106) AS EntryDate , A.PreviousVersion , A.Remarks , A.ApproveStatus FROM dbo.tblBSCAppraisalSelfAppLog A LEFT JOIN dbo.tblUser u ON a.EntryBy = u.UserName
            //LEFT JOIN dbo.tblEmpGeneralInfo e ON u.EmpInfoId = e.EmpInfoId
            //LEFT JOIN dbo.tblDesignation desg ON e.DesignationId = desg.DesignationId
            //LEFT JOIN dbo.tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
            try
            {
                string query = @"    select   ROW_NUMBER() OVER (ORDER BY A.bscAppraisalSelfAppLogId ASC) AS SerialNumber, (e.EmpMasterCode+' : '+e.EmpName+ISNULL(' : '+desg.Designation,'')+ISNULL(' : '+dpt.DepartmentName,'')) AS Employee , FORMAT(A.ApproveDate,'dd-MMM-yyyy hh:mm tt') AS EntryDate ,A.Comments  AS PreviousVersion ,'' as Remarks , case when A.ForwardBy is not null then  '  forwarded By ['+ ISNULL(efor.EmpMasterCode+' : '+efor.EmpName,uf.UserName) +']'   when A.ShowStatus='Return' then 'Returned'   else A.ShowStatus end  AS ApproveStatus 
            FROM dbo.tblBSCAppraisalSelfAppLog A 
            LEFT JOIN dbo.tblUser u ON a.ApproveBy = u.UserId
			LEFT JOIN dbo.tblEmpGeneralInfo e ON u.EmpInfoId = e.EmpInfoId
            LEFT JOIN dbo.tblUser uf ON a.ForwardBy = uf.UserId

			LEFT JOIN dbo.tblEmpGeneralInfo efor ON uf.EmpInfoId = efor.EmpInfoId
			LEFT JOIN dbo.tblDesignation desg ON e.DesignationId = desg.DesignationId
			LEFT JOIN dbo.tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
			WHERE  A.ActionStatus<>'Drafted' and A.BSCAppraisalSelfMasterId= " + id + " order by A.bscAppraisalSelfAppLogId asc ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetApraisalFunctionalByMaster(int id)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblBSCAppraisalSelfFuncArea WHERE BSCAppraisalMasterId = " + id + " ";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool ApproveAppraisalOwnMark(int id)
        {
            try
            {


                string query = @"UPDATE dbo.tblMBSCAppraisalMaster SET SelfApprove = 'Posted' WHERE BSCAppraisalMasterId = " +
                               id + "";

                return _aCommonInternalDal.UpdateDataByUpdateCommand(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public bool AppraisalRejectBySup(int user, int masterId, string entryBy, string remarks)
        {
            try
            {

                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@user", user));
                aParameters.Add(new SqlParameter("@entryBy", entryBy));
                aParameters.Add(new SqlParameter("@masterId", masterId));
                aParameters.Add(new SqlParameter("@remarks", remarks));
                aParameters.Add(new SqlParameter("@entryDate", System.DateTime.Now));
                string query = @"DECLARE @reportTO INT 
            DECLARE @ActionVersion INT 
            DECLARE @appraisalSelf INT 
            SELECT @reportTO = emp.ReportingEmpId  ,@appraisalSelf = A.BSCAppraisalSelfMasterId ,  @ActionVersion = ISNULL(a.ActionVersion,0) FROM dbo.tblMBSCAppraisalMaster A LEFT JOIN dbo.tblEmpGeneralInfo emp ON a.EmpInfoId = emp.EmpInfoId  WHERE BSCAppraisalMasterId = @masterId
            
            IF(@reportTO = @user)
            BEGIN
            	UPDATE dbo.tblMBSCAppraisalMaster SET CurrentStatus = 'Returned' , ActionVersion = @ActionVersion+1 , SelfApprove='Drafted'  ,   IsApprove = 0 , ApproveBy = @entryBy WHERE BSCAppraisalMasterId = @masterId
            	INSERT INTO dbo.tblAppraisalApproveLog
            	        ( BSCAppraisalSelfMasterId ,
            	          BSCAppraisalMasterId ,
            	          PreviousVersion ,
            	          NewVersion ,
            	          EntryDate ,
            	          EntryBy ,
            	          ApproveStatus ,
            	          Remarks
            	        )
            	VALUES  ( @appraisalSelf , -- BSCAppraisalSelfMasterId - int
            	          @masterId , -- BSCAppraisalMasterId - int
            	          @ActionVersion , -- PreviousVersion - int
            	          @ActionVersion+1 , -- NewVersion - int
            	            @entryDate , -- EntryDate - date
            	          @entryBy , -- EntryBy - nvarchar(50)
            	          'Returned' , -- ApproveStatus - nvarchar(50)
            	          @remarks  -- Remarks - nvarchar(max)
            	        )
            END
            ELSE 
            BEGIN 
            	UPDATE dbo.tblMBSCAppraisalMaster SET CurrentStatus = 'Returned' , ActionVersion = @ActionVersion+1 ,    IsApprove = 0 , ApproveBy = @entryBy WHERE BSCAppraisalMasterId = @masterId
            	INSERT INTO dbo.tblAppraisalApproveLog
            	        ( BSCAppraisalSelfMasterId ,
            	          BSCAppraisalMasterId ,
            	          PreviousVersion ,
            	          NewVersion ,
            	          EntryDate ,
            	          EntryBy ,
            	          ApproveStatus ,
            	          Remarks
            	        )
            	VALUES  ( @appraisalSelf , -- BSCAppraisalSelfMasterId - int
            	          @masterId , -- BSCAppraisalMasterId - int
            	          @ActionVersion , -- PreviousVersion - int
            	          @ActionVersion+1 , -- NewVersion - int
            	           @entryDate  , -- EntryDate - date
            	          @entryBy , -- EntryBy - nvarchar(50)
            	          'Returned' , -- ApproveStatus - nvarchar(50)
            	         @remarks -- Remarks - nvarchar(max)
            	        )
            END";


                bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                return result;

            }


            catch (Exception ex)
            {

                throw ex;
            }


        }
        public DataTable GetFinalMark(int id)
        {
            try
            {
                string query = @"SELECT *,tblbehav.SupScoreBehave+tblfuc.SupScoreFunc AS TotalScore FROM dbo.tblMBSCAppraisalMaster
                                LEFT JOIN 
                                (SELECT BSCAppraisalMasterId,SUM(SupervisorMark)SupScoreFunc FROM dbo.tblMBSCAppraisalFuncArea GROUP BY BSCAppraisalMasterId)AS tblfuc ON tblfuc.BSCAppraisalMasterId = tblMBSCAppraisalMaster.BSCAppraisalMasterId
                                LEFT JOIN 
                                (SELECT BSCAppraisalMasterId,SUM(SupervisorScore)SupScoreBehave FROM dbo.tblMBSCAppraisalBehaveArea GROUP BY BSCAppraisalMasterId) AS tblbehav ON tblbehav.BSCAppraisalMasterId = tblMBSCAppraisalMaster.BSCAppraisalMasterId
                                WHERE tblMBSCAppraisalMaster.BSCAppraisalMasterId='" + id+"'";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool SaveAppraisalMasterFromAppraisalSelf(string appselfMasterId)
        {
            try
            {

                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@BSCAppraisalSelfMasterId", appselfMasterId));

                string query = @"insert into tblMBSCAppraisalMaster
(FinancialYearId,EmpInfoId,EntryDate,EntryBy,UpdateBy,UpdateDate,IsDelete,DeleteBy,IsApprove,ApproveBy,ApproveDate,BSCAppraisalSelfMasterId,ActionVersion,CurrentStatus,SelfApprove,FYDes_BSCApp) 
select FinancialYearId,EmpInfoId,EntryDate,EntryBy,UpdateBy,UpdateDate,IsDelete,DeleteBy,IsApprove,ApproveBy,ApproveDate,BSCAppraisalSelfMasterId,'0',ActionStatus,'Posted', (SELECT FinancialYearDesc 
                    FROM dbo.tblFinancialYear 
                    WHERE FinancialYearId = tblBSCAppraisalSelfMaster.FinancialYearId ) from tblBSCAppraisalSelfMaster where BSCAppraisalSelfMasterId=@BSCAppraisalSelfMasterId";


                bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                return result;

            }


            catch (Exception ex)
            {

                throw ex;
            }


        }
        public bool ApproveAppraisalBySup(int user, int masterId, string entryBy, string remarks)
        {
            try
            {

                List<SqlParameter> aParameters = new List<SqlParameter>();
                aParameters.Add(new SqlParameter("@user", user));
                aParameters.Add(new SqlParameter("@entryBy", entryBy));
                aParameters.Add(new SqlParameter("@masterId", masterId));
                aParameters.Add(new SqlParameter("@remarks", remarks));
                aParameters.Add(new SqlParameter("@entryDate", System.DateTime.Now));
                string query = @"DECLARE @reportTO INT 
            DECLARE @ActionVersion INT 
            DECLARE @appraisalSelf INT 
            SELECT @reportTO = emp.ReportingEmpId  ,@appraisalSelf = A.BSCAppraisalSelfMasterId ,  @ActionVersion = ISNULL(a.ActionVersion,0) FROM dbo.tblMBSCAppraisalMaster A LEFT JOIN dbo.tblEmpGeneralInfo emp ON a.EmpInfoId = emp.EmpInfoId  WHERE BSCAppraisalMasterId = @masterId
            
       UPDATE dbo.tblMBSCAppraisalMaster SET CurrentStatus = 'Approved' , ActionVersion = @ActionVersion+1 ,    IsApprove = 1 , ApproveBy =  @entryBy WHERE BSCAppraisalMasterId = @masterId
            	INSERT INTO dbo.tblAppraisalApproveLog
            	        ( BSCAppraisalSelfMasterId ,
            	          BSCAppraisalMasterId ,
            	          PreviousVersion ,
            	          NewVersion ,
            	          EntryDate ,
            	          EntryBy ,
            	          ApproveStatus ,
            	          Remarks
            	        )
            	VALUES  ( @appraisalSelf , -- BSCAppraisalSelfMasterId - int
            	          @masterId , -- BSCAppraisalMasterId - int
            	          @ActionVersion , -- PreviousVersion - int
            	          @ActionVersion+1 , -- NewVersion - int
            	            @entryDate , -- EntryDate - date
            	          @entryBy , -- EntryBy - nvarchar(50)
            	          'Returned' , -- ApproveStatus - nvarchar(50)
            	          @remarks  -- Remarks - nvarchar(max)
            	        )";

                bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool UpdateAppLog(string status, string id)
        {

            try
            {
                int pk = 0;

                //if (id.JdMasterId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@bscAppraisalSelfAppLogId", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", status));


                    string query =
                        @"update tblBSCAppraisalSelfAppLog set ActionStatus=@ActionStatus  where bscAppraisalSelfAppLogId = @bscAppraisalSelfAppLogId";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    return result;

                }

            }
            catch (Exception exception)
            {

                throw exception;
            }
        }

        public int SaveEmpAppLog(AppraisalSelfAppLogDAO appLogDao)
        {

            try
            {
                int pk = 0;


                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@BSCAppraisalSelfMasterId", appLogDao.BSCAppraisalSelfMasterId));
                    aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                    aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                    aParameters.Add(new SqlParameter("@Version", appLogDao.Version));
                    aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                    aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                    aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                    aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));
                    aParameters.Add(new SqlParameter("@CommentsEMPID", appLogDao.CommentsEMP));
                    aParameters.Add(new SqlParameter("@ShowStatus", appLogDao.ShowStatus));



                    string query = @"INSERT INTO dbo.tblBSCAppraisalSelfAppLog
                                    (
                                    BSCAppraisalSelfMasterId,
                                    PreEmpInfoId,
                                    ForEmpInfoId,
                                    Version,
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments,CommentsEMPID, ShowStatus
                                    )
                                    VALUES(
                                    @BSCAppraisalSelfMasterId,
                                    @PreEmpInfoId,
                                    @ForEmpInfoId,
                                    (SELECT (isnull(COUNT(*),0)+1) FROM dbo.tblBSCAppraisalSelfAppLog WHERE BSCAppraisalSelfMasterId=@BSCAppraisalSelfMasterId),
                                    @ApproveBy,
                                    @ApproveDate,
                                    @ActionStatus,@Comments,@CommentsEMPID,@ShowStatus
                                    )";

                    pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                }


                return pk;
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }
        public DataTable GetEmpInfo(string param)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblEmpGeneralInfo  WITH (NOLOCK)  " + param + "";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        public DataTable CheckFinalApprovalCondition(string ComId, string EmpID)
        {
            try
            {
                string query = @" SELECT * FROM dbo.tblSupevisorMenuApproval  WITH (Nolock)  WHERE  CompanyId='" + ComId + "' AND FromEmpInfoId ='" + EmpID + "' AND EmpInfoId IS NOT NULL ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable CheckFinalApprovalConditionNotSuppervisor(string EmpID)
        {
            try
            {
                string query = @" SELECT * FROM dbo.tblSupevisorMenuApproval  WITH (Nolock)  WHERE   FromEmpInfoId ='" + EmpID + "' AND EmpInfoId IS NOT NULL ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable getKPIBehaviourName(string KPI_Type)
        {
            try
            {
                string query = @" SELECT KPIBehaviourName FROM tblKPIBehaviour WITH (NOLOCK)
WHERE  Type=LTRIM(RTRIM('" + KPI_Type + "')) ORDER BY KPIBehaviourName asc";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable getKPIBehaviourNameWithOutParam()
        {
            try
            {
                string query = @" SELECT KPIBehaviourName FROM tblKPIBehaviour WITH (NOLOCK) ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public DataTable GetFinalApproveByEmpId(string EmpID, string suppId)
        {
            try
            {
                string query = @"select * from  dbo.tblSupevisorMenuApproval SMA  With (nolock)  where SMA.FromEmpInfoId=" + EmpID + " and SMA.EmpInfoId=" + suppId;

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetEmpInfoPrevious(string forempInfoid, string jdmasterId)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblBSCAppraisalSelfAppLog WHERE ForEmpInfoId='" + forempInfoid + "' AND BSCAppraisalSelfMasterId='" + jdmasterId + "' AND ActionStatus NOT IN ('Review')   order by bscAppraisalSelfAppLogId desc";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetSupervisorAppId(string url, string param)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblSupevisorMenuApproval
LEFT JOIN dbo.tblMainMenu ON tblMainMenu.MainMenuId = tblSupevisorMenuApproval.MainMenuId WHERE URL='" + url + "' " + param + "";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public DataTable GetSupervisorEmployeeAppId(string empinfoId, string fromempInfoId)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblSupevisorMenuApproval WHERE EmpInfoId='"+empinfoId+"' AND FromEmpInfoId='"+fromempInfoId+"'";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool UpdateContractural(AppraisalSelfAppLogDAO aMaster)
        {

            try
            {
                int pk = 0;

                if (aMaster.BSCAppraisalSelfMasterId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@BSCAppraisalSelfMasterId", aMaster.BSCAppraisalSelfMasterId));
                    aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));


                    string query =
                        @"update tblBSCAppraisalSelfMaster set ActionStatus=@ActionStatus  where BSCAppraisalSelfMasterId = @BSCAppraisalSelfMasterId";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    return result;

                }

            }
            catch (Exception exception)
            {

                throw exception;
            }
            return true;
        }

        public DataTable GetPreComments(string id)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblBSCAppraisalSelfAppLog
left JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblBSCAppraisalSelfAppLog.CommentsEMPID
 WHERE BSCAppraisalSelfMasterId='" + id+"' AND tblBSCAppraisalSelfAppLog.ActionStatus<>'Drafted'";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

    }
}
