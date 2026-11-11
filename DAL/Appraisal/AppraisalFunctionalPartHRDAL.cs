using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.Appraisal
{


    public class AppraisalFunctionalPartHRDAL
    {
        ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        DataAccessManager _aAcessManager = new DataAccessManager();



        public DataTable GetBSCAppraisalByKpiPermission(string EmpID, string FinancialYear, string param)
        {
            try
            {
                string query = @"SELECT  ISNULL(app.BSCAppraisalSelfMasterId,0) AppraisalSelfMasterId, e.EmpInfoId ,b.DeadLine,  isnull(b.ExtensionDate,b.DeadLine) as ExtensionDate,
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
        WHERE b.EmpInfoId =  " + Convert.ToInt32(EmpID) + " and y.FinancialYearDesc='" + FinancialYear + "'" + param;

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
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
                string query = @"select   * from tblAppraisalSelfMaster where FinancialYearId='" + FinancialYearId + "' and EmpInfoId=" + empId;
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
	   CONVERT(NVARCHAR(11) , TrainingEnd , 106)TrainingEnd,*,Quater as quaterID
       	 FROM dbo.tblAppraisalTrainingNeeds WHERE AppraisalMasterId = " + id + "";
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
                string query = @"select  * from dbo.tblAppraisalMaster where AppraisalSelfMasterId =" + id + "";
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
inner join tblAppraisalMaster app on appfin.AppraisalMasterId=app.AppraisalMasterId
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
            
            FROM dbo.tblAppraisalMasterAppLog A 
            LEFT JOIN dbo.tblUser u ON a.ApproveBy = u.UserId
			LEFT JOIN dbo.tblEmpGeneralInfo e ON u.EmpInfoId = e.EmpInfoId
			LEFT JOIN dbo.tblDesignation desg ON e.DesignationId = desg.DesignationId
			LEFT JOIN dbo.tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
			WHERE  A.ActionStatus<>'Drafted' and A.AppraisalMasterId= " + id + "  ORDER BY A.AppraisalMasterAppLogId desc ";
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
		 LEFT JOIN dbo.tblAppraisalMaster ON tblAppraisalMaster.AppraisalMasterId = tblAppraisalTrainingNeeds.AppraisalMasterId
		 
		 LEFT JOIN dbo.tblEmpGeneralInfo Eg ON Eg.EmpInfoId = tblAppraisalMaster.EmpinfoId where Eg.EmpMasterCode is not NULL 
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

               aParameters.Add(new SqlParameter("@AppraisalMasterId", aMaster.AppraisalMasterId));
               aParameters.Add(new SqlParameter("@AppraisalSelfMasterId", aMaster.AppraisalSelfMasterId));
               aParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId));
               aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
               aParameters.Add(new SqlParameter("@EntryBy", user));
               aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));
               int pk = aMaster.AppraisalMasterId;
              

               //int 

               
                    string delQuery = @"

INSERT INTO [dbo].[tblAppraisalMasterHR]
           ([FinancialYearId]
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
           ,[AppraisalSelfMasterId]
           ,[ActionVersion]
           ,[CurrentStatus]
           ,[SelfApprove]
           ,[IsSapApproved])
     select [FinancialYearId]
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
           ,[AppraisalSelfMasterId]
           ,[ActionVersion]
           ,[CurrentStatus]
           ,[SelfApprove]
           ,[IsSapApproved] from tblAppraisalMaster where AppraisalSelfMasterId = @AppraisalSelfMasterId


UPDATE [dbo].[tblAppraisalMaster]
   SET  [ChangeMarkBy] =@EntryBy
      ,[ChangeMarkDate] = GETDATE()
 WHERE   AppraisalSelfMasterId = @AppraisalSelfMasterId ";
               bool r = _aCommonInternalDal.DeleteDataByDeleteCommand(delQuery, aParameters, DataBase.HRDB);

            
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
                string getVerionQ = @"select ActionVersion from  tblAppraisalSelfMaster where AppraisalSelfMasterId = " + id + "";

                DataTable GetCurrentVersion = _aCommonInternalDal.DataContainerDataTable(getVerionQ, DataBase.HRDB);

                currentVersion = GetCurrentVersion.Rows.Count <= 0
                    ? 0
                    : Convert.ToInt32(GetCurrentVersion.Rows[0][0].ToString());

                string insertLog = @"INSERT INTO dbo.tblAppraisalSelfApproveLog
                                    ( AppraisalSelfMasterId ,
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

                        selfMasterQ = @"update tblAppraisalSelfMaster set   IsApprove = " +
                                        approveBit + " ," +
                                        " ApproveBy = '" + user + "' , CurrentStatus = '" + status + "'  , ApproveDate = '" +
                                        DateTime.Now + "' , " +
                                        "ActionVersion = " + (currentVersion + 1) + " " +
                                        " where AppraisalSelfMasterId = " + id + " ";
                    }
                    else
                    {
                        selfMasterQ = @"update tblAppraisalSelfMaster set   IsApprove = " + approveBit + " , ApproveBy = '" + user + "' ,  IsPublish = 0 , ActionStatus = 'Drafted'  ,  CurrentStatus = '" + status + "'  , ApproveDate = '" + DateTime.Now + "' , ActionVersion = " + (currentVersion + 1) + " " +
                                         " where AppraisalSelfMasterId = " + id + " ";

                        string delQ = @"Delete from tblAppraisalMaster where AppraisalSelfMasterId = " + id + "";
                        string delD = @"Delete from tblAppraisalFuncArea where AppraisalSelfMasterId = " + id + "";

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
        public int SaveAppraisalSelfMaster(AppraisalMaster aMaster, string user)
        {
            try
            {
                if (aMaster.AppraisalMasterId > 0)
                {
                    /////asdasddasd
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



                    string query = @"Insert into tblAppraisalSelfMaster (EmpInfoId,FinancialYearId,EntryBy,EntryDate,ActionStatus,FYDes_Self ) values(@EmpInfoId,@FinancialYearId,@EntryBy,@EntryDate,@ActionStatus, (SELECT FinancialYearDesc 
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

        public int SaveAppraisalSelfMasterMulti(AppraisalMaster aMaster, string user)
        {
            try
            {
                if (aMaster.AppraisalMasterId > 0)
                {
                    /////asdasddasd
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@AppraisalSelfMasterId", aMaster.AppraisalMasterId));
                    aParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@UpdateBy", user));
                    aParameters.Add(new SqlParameter("@ActionStatus", "Approved"));
                    aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));
                    string query = @"update tblAppraisalSelfMaster set FinancialYearId =@FinancialYearId  , EmpInfoId =@EmpInfoId  , UpdateBy =@UpdateBy  , UpdateDate =@UpdateDate,ActionStatus =@ActionStatus, IsFromGroupKPI=1  where AppraisalSelfMasterId=@AppraisalSelfMasterId";

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
                    aParameters.Add(new SqlParameter("@ActionStatus", "Approved"));
                    aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));



                    string query = @"Insert into tblAppraisalSelfMaster (EmpInfoId,FinancialYearId,EntryBy,EntryDate,ActionStatus,IsFromGroupKPI,FYDes_Self ) values(@EmpInfoId,@FinancialYearId,@EntryBy,@EntryDate,@ActionStatus,1, (SELECT FinancialYearDesc 
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

        public int SaveAppraisalSelfMasterforSupper(AppraisalMaster aMaster, string user)
        {
            try
            {
                if (aMaster.AppraisalMasterId > 0)
                {
                    /////asdasddasd
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@AppraisalSelfMasterId", aMaster.AppraisalMasterId));
                    aParameters.Add(new SqlParameter("@EmpInfoId", aMaster.EmpInfoId));
                    aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                    aParameters.Add(new SqlParameter("@IsFunctionalArea", aMaster.IsFunctionalArea));
                    aParameters.Add(new SqlParameter("@IsBehavioralArea", aMaster.IsBehavioralArea));
                    aParameters.Add(new SqlParameter("@UpdateBy", user));
                  //  aParameters.Add(new SqlParameter("@ActionStatus", "Drafted"));
                    aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));
                    string query = @"update tblAppraisalSelfMaster set IsFunctionalArea=@IsFunctionalArea, IsBehavioralArea=@IsBehavioralArea, FinancialYearId =@FinancialYearId  , EmpInfoId =@EmpInfoId  , UpdateBy =@UpdateBy  , UpdateDate =@UpdateDate where AppraisalSelfMasterId=@AppraisalSelfMasterId";

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
                    //aParameters.Add(new SqlParameter("@ActionStatus", "Drafted"));
                    aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));
                    aParameters.Add(new SqlParameter("@IsFunctionalArea", aMaster.IsFunctionalArea));
                    aParameters.Add(new SqlParameter("@IsBehavioralArea", aMaster.IsBehavioralArea));


                    string query = @"Insert into tblAppraisalSelfMaster (EmpInfoId,FinancialYearId,EntryBy,EntryDate,IsFunctionalArea,IsBehavioralArea,FYDes_Self ) values(@EmpInfoId,@FinancialYearId,@EntryBy,@EntryDate,@IsFunctionalArea,@IsBehavioralArea, (SELECT FinancialYearDesc 
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
        //public bool SaveAppraialFunctionalDetails(List<AppraisalFunctionalArea> aList, int masterid, int selfMaster)
        //{
        //    try
        //    {

        //        string query2 = @"delete from tblAppraisalFuncArea where AppraisalSelfMasterId = " + selfMaster + "";
        //        bool a = _aCommonInternalDal.DeleteDataByDeleteCommand(query2, DataBase.HRDB);

        //        string query3 = @"delete from tblAppraisalSelfFuncArea where AppraisalSelfMasterId = " + selfMaster + "";
        //        bool asd = _aCommonInternalDal.DeleteDataByDeleteCommand(query3, DataBase.HRDB);

        //        bool result = false;
        //        foreach (var item in aList)
        //        {
        //            List<SqlParameter> aParameters = new List<SqlParameter>();

        //            aParameters.Add(new SqlParameter("@AppraisalMasterId", masterid));
        //            aParameters.Add(new SqlParameter("@AppraisalSelfMasterId", selfMaster));
        
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

        //            string query = @"insert into tblAppraisalFuncArea(AppraisalMasterId, KpiInfo, KpiWeight, Deadline, MidYearStatus, ResultYearEnd,SelfMark,  SupervisorMark, Target,AppraisalSelfMasterId,KpiWeightPer,TargetPer) values(@AppraisalMasterId, @KpiInfo, @KpiWeight, @Deadline, @MidYearStatus, @ResultYearEnd, @SelfMark, @SupervisorMark, @Target,@AppraisalSelfMasterId,@KpiWeightPer,@TargetPer)";
        //            result = _aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);



        //            string queryD = @"insert into tblAppraisalSelfFuncArea(AppraisalSelfMasterId, KpiInfo, KpiWeight, Deadline, MidYearStatus,  SelfMark, Target,KpiWeightPer,TargetPer) values(@AppraisalSelfMasterId, @KpiInfo, @KpiWeight, @Deadline, @MidYearStatus,  @SelfMark, @Target,@KpiWeightPer,@TargetPer)";
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

        public bool SaveAppraialFunctionalDetails(List<AppraisalFunctionalArea> aList, int masterid, int selfMaster)
        {
            try
            {

                string query2 = @"

INSERT INTO [dbo].[tblAppraisalFuncAreaHR]
           ([AppraisalMasterId]
           ,[KpiInfo]
           ,[KpiWeight]
           ,[Deadline]
           ,[MidYearStatus]
           ,[ResultYearEnd]
           ,[SelfMark]
           ,[SupervisorMark]
           ,[Target]
           ,[KpiWeightPer]
           ,[TargetPer]
           ,[AppraisalSelfFucAreaId]
           ,[AppraisalSelfMasterId])
    select [AppraisalMasterId]
           ,[KpiInfo]
           ,[KpiWeight]
           ,[Deadline]
           ,[MidYearStatus]
           ,[ResultYearEnd]
           ,[SelfMark]
           ,[SupervisorMark]
           ,[Target]
           ,[KpiWeightPer]
           ,[TargetPer]
           ,[AppraisalSelfFucAreaId]
           ,[AppraisalSelfMasterId] from tblAppraisalFuncArea  where AppraisalSelfMasterId="  + selfMaster + @" 


delete from tblAppraisalFuncArea where AppraisalSelfMasterId = " + selfMaster + "";
                bool a = _aCommonInternalDal.DeleteDataByDeleteCommand(query2, DataBase.HRDB);

                //string query3 = @"delete from tblAppraisalSelfFuncArea where AppraisalSelfMasterId = " + selfMaster + "";
                //bool asd = _aCommonInternalDal.DeleteDataByDeleteCommand(query3, DataBase.HRDB);

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
                    aParameters.Add(new SqlParameter("@ResultYearEnd", item.ResultYearEnd ));

                    aParameters.Add(new SqlParameter("@SupervisorMark", item.SupervisorMark));
                    aParameters.Add(new SqlParameter("@Target", item.Target));
                    aParameters.Add(new SqlParameter("@TargetPer", item.TargetPer));
                    aParameters.Add(new SqlParameter("@IsActive", item.IsActive));

         

                  
                    if (item.AppraisalSelfFucAreaId==0)
                    {
                        string queryD = @"insert into tblAppraisalSelfFuncArea(AppraisalSelfMasterId, KpiInfo, KpiWeight, Deadline, MidYearStatus,  SelfMark, Target,KpiWeightPer,TargetPer,IsActive) values(@AppraisalSelfMasterId, @KpiInfo, @KpiWeight, @Deadline, @MidYearStatus,  @SelfMark, @Target,@KpiWeightPer,@TargetPer,@IsActive)";
                       
                        int pk = _aCommonInternalDal.SaveDataByInsertCommandById(queryD, aParameters, DataBase.HRDB);
                        item.AppraisalSelfFucAreaId = pk;
                        aParameters.Add(new SqlParameter("@AppraisalSelfFucAreaId", item.AppraisalSelfFucAreaId));
                    }
                    else
                    {
                        aParameters.Add(new SqlParameter("@AppraisalSelfFucAreaId", item.AppraisalSelfFucAreaId));
                        string queryD = @"UPDATE  tblAppraisalSelfFuncArea SET AppraisalSelfMasterId=@AppraisalSelfMasterId, KpiInfo=@KpiInfo, KpiWeight=@KpiWeight, Deadline=@Deadline, MidYearStatus=@MidYearStatus,  SelfMark=@SelfMark, Target=@Target,KpiWeightPer=@KpiWeightPer,TargetPer=@TargetPer,IsActive=@IsActive
                                                                     WHERE AppraisalSelfFucAreaId=@AppraisalSelfFucAreaId";
                          result = _aCommonInternalDal.SaveDataByInsertCommand(queryD, aParameters, DataBase.HRDB);
                    }


                    if (item.IsActive==true)
                    {
                        
                
                    string query = @"insert into tblAppraisalFuncArea(AppraisalMasterId, KpiInfo, KpiWeight, Deadline, MidYearStatus, ResultYearEnd,SelfMark,  SupervisorMark, Target,AppraisalSelfMasterId,KpiWeightPer,TargetPer,AppraisalSelfFucAreaId) values(@AppraisalMasterId, @KpiInfo, @KpiWeight, @Deadline, @MidYearStatus, @ResultYearEnd, @SelfMark, @SupervisorMark, @Target,@AppraisalSelfMasterId,@KpiWeightPer,@TargetPer,@AppraisalSelfFucAreaId)";
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
                    aParameters.Add(new SqlParameter("@IsActive", item.IsActive));
                    aParameters.Add(new SqlParameter("@Target", item.Target));
                    aParameters.Add(new SqlParameter("@TargetPer", item.TargetPer));



                    string query = @"insert into tblAppraisalSelfFuncArea(AppraisalSelfMasterId, KpiInfo, KpiWeight, Deadline, MidYearStatus,  SelfMark, Target,KpiWeightPer,TargetPer,IsActive) values(@AppraisalSelfMasterId, @KpiInfo, @KpiWeight, @Deadline, @MidYearStatus,  @SelfMark, @Target,@KpiWeightPer,@TargetPer,@IsActive)";
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

        public DataTable GetAppraisalByPermission3(string MasterId)
        {
            try
            {
                string query = @"select  ForEmpInfoId,* from tblAppraisalSelfAppLog where AppraisalSelfMasterId=" + MasterId + "  and ActionStatus<>'Drafted' order by AppraisalSelfAppLogId desc  ";

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
                string query = @"SELECT Distinct * FROM(SELECT  ISNULL(app.AppraisalSelfMasterId,0) AppraisalSelfMasterId, e.EmpInfoId ,b.DeadLine,  isnull(b.ExtensionDate,b.DeadLine) as ExtensionDate,
        ( 'Employee ID: '+ e.EmpMasterCode + ', Employee Name: ' + e.EmpName +    ISNULL(', Designation: ' + desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove,
        app.CurrentStatus, app.ActionStatus, (CASE WHEN app.ActionStatus<>'Approved' then 'Not Approved' ELSE app.ActionStatus END)AS KPIActionStatus ,ForEmp.EmpName as PendingEmp
        FROM    dbo.tblKpiDeadlineMaster A
        LEFT JOIN dbo.tblKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN dbo.tblAppraisalSelfMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId 
        
	LEFT  JOIN (SELECT AppraisalSelfMasterId,MAX(Version)MaxVer FROM dbo.tblAppraisalSelfAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalSelfMasterId) AS CELog ON CELog.AppraisalSelfMasterId= app.AppraisalSelfMasterId
								LEFT  JOIN dbo.tblAppraisalSelfAppLog ON tblAppraisalSelfAppLog.AppraisalSelfMasterId = app.AppraisalSelfMasterId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblAppraisalSelfAppLog.ForEmpInfoId
        WHERE b.EmpInfoId =  " + Convert.ToInt32(EmpID) + " and y.FinancialYearDesc='" + FinancialYear + "'"  +param + "" +
                               "" +
                               "" +
                               @"	union all SELECT  ISNULL(app.AppraisalSelfMasterId,0) AppraisalSelfMasterId, e.EmpInfoId ,b.DeadLine,  isnull(b.ExtensionDate,b.DeadLine) as ExtensionDate,
        ( 'Employee ID: '+ e.EmpMasterCode + ', Employee Name: ' + e.EmpName +    ISNULL(', Designation: ' + desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove,
        app.CurrentStatus, app.ActionStatus, (CASE WHEN app.ActionStatus<>'Approved' then 'Not Approved' ELSE app.ActionStatus END)AS KPIActionStatus ,ForEmp.EmpName as PendingEmp
        FROM    dbo.tblKpiDeadlineMaster A
        LEFT JOIN dbo.tblKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
 	 inner JOIN   tblEmpAllRefference reff  ON b.EmpinfoId = reff.RefferenceEmpId 

        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN dbo.tblAppraisalSelfMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId 
        
	LEFT  JOIN (SELECT AppraisalSelfMasterId,MAX(Version)MaxVer FROM dbo.tblAppraisalSelfAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalSelfMasterId) AS CELog ON CELog.AppraisalSelfMasterId= app.AppraisalSelfMasterId
								LEFT  JOIN dbo.tblAppraisalSelfAppLog ON tblAppraisalSelfAppLog.AppraisalSelfMasterId = app.AppraisalSelfMasterId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblAppraisalSelfAppLog.ForEmpInfoId
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
                string query = @"SELECT  ISNULL(app.AppraisalSelfMasterId,0) AppraisalSelfMasterId, e.EmpInfoId ,b.DeadLine,  isnull(b.ExtensionDate,b.DeadLine) as ExtensionDate,
        ( 'Employee ID: '+ e.EmpMasterCode + ', Employee Name: ' + e.EmpName +    ISNULL(', Designation: ' + desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove,
        app.CurrentStatus, app.ActionStatus, (CASE WHEN app.ActionStatus<>'Approved' then 'Not Approved' ELSE app.ActionStatus END)AS KPIActionStatus ,ForEmp.EmpName as PendingEmp
        FROM    dbo.tblKpiDeadlineMaster A
        LEFT JOIN dbo.tblKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN dbo.tblAppraisalSelfMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId 
        
	LEFT  JOIN (SELECT AppraisalSelfMasterId,MAX(Version)MaxVer FROM dbo.tblAppraisalSelfAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalSelfMasterId) AS CELog ON CELog.AppraisalSelfMasterId= app.AppraisalSelfMasterId
								LEFT  JOIN dbo.tblAppraisalSelfAppLog ON tblAppraisalSelfAppLog.AppraisalSelfMasterId = app.AppraisalSelfMasterId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblAppraisalSelfAppLog.ForEmpInfoId
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
                string query = @"select  ForEmpInfoId,* from tblAppraisalSelfAppLog where AppraisalSelfMasterId=" + MasterId + "  and ActionStatus<>'Drafted' order by AppraisalSelfAppLogId desc  ";

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
                string query = @"SELECT ISNULL(app.AppraisalSelfMasterId,0) AppraisalSelfMasterId, e.EmpInfoId ,  isnull(b.ExtensionDate,b.DeadLine) as ExtensionDate,
        ( 'Employee ID: '+ e.EmpMasterCode + ', Employee Name: ' + e.EmpName +    ISNULL(', Designation: ' + desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove,
        app.CurrentStatus, app.ActionStatus, (CASE WHEN app.ActionStatus<>'Approved' then 'Not Approved' ELSE app.ActionStatus END)AS KPIActionStatus ,ForEmp.EmpName as PendingEmp
        FROM    dbo.tblKpiDeadlineMaster A
        LEFT JOIN dbo.tblKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN dbo.tblAppraisalSelfMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId 
        
	LEFT  JOIN (SELECT AppraisalSelfMasterId,MAX(Version)MaxVer FROM dbo.tblAppraisalSelfAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalSelfMasterId) AS CELog ON CELog.AppraisalSelfMasterId= app.AppraisalSelfMasterId
								LEFT  JOIN dbo.tblAppraisalSelfAppLog ON tblAppraisalSelfAppLog.AppraisalSelfMasterId = app.AppraisalSelfMasterId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblAppraisalSelfAppLog.ForEmpInfoId
        WHERE b.EmpInfoId =  " + Convert.ToInt32(EmpID) + param + " order by AppraisalSelfMasterId desc";

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
                string query = @"SELECT top 1 ISNULL(app.AppraisalSelfMasterId,0) AppraisalSelfMasterId, e.EmpInfoId ,  MAX(isnull(b.ExtensionDate,b.DeadLine)) as ExtensionDate,
        ( 'Employee ID: '+ e.EmpMasterCode + ', Employee Name: ' + e.EmpName +    ISNULL(', Designation: ' + desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove,
        app.CurrentStatus, app.ActionStatus, (CASE WHEN app.ActionStatus<>'Approved' then 'Not Approved' ELSE app.ActionStatus END)AS KPIActionStatus ,ForEmp.EmpName as PendingEmp
        FROM    dbo.tblKpiDeadlineMaster A
        LEFT JOIN dbo.tblKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN dbo.tblAppraisalSelfMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId 
        
	LEFT  JOIN (SELECT AppraisalSelfMasterId,MAX(Version)MaxVer FROM dbo.tblAppraisalSelfAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalSelfMasterId) AS CELog ON CELog.AppraisalSelfMasterId= app.AppraisalSelfMasterId
								LEFT  JOIN dbo.tblAppraisalSelfAppLog ON tblAppraisalSelfAppLog.AppraisalSelfMasterId = app.AppraisalSelfMasterId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblAppraisalSelfAppLog.ForEmpInfoId
        WHERE b.EmpInfoId = " + Convert.ToInt32(EmpID) + param+@" 	group by ISNULL(app.AppraisalSelfMasterId,0) , e.EmpInfoId ,
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
        FROM    dbo.tblKpiDeadlineMaster A
        LEFT JOIN dbo.tblKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN dbo.tblAppraisalSelfMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId 
	 
        

								 
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
        FROM    dbo.tblKpiDeadlineMaster A
        LEFT JOIN dbo.tblKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN dbo.tblAppraisalSelfMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId 
	 
        

								 
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
        FROM    dbo.tblKpiDeadlineMaster A
        LEFT JOIN dbo.tblKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		LEFT JOIN dbo.tblAppraisalSelfMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId 
	 
              
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
		   dbo.tblKpiDeadlineMaster A
        LEFT JOIN dbo.tblKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
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
		   dbo.tblKpiDeadlineMaster A
    LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN dbo.tblKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
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
                string query = @" SELECT * FROM 
		   dbo.tblAppraisalDeadlineMaster A
        LEFT JOIN dbo.tblAppraisalDeadLineDetails b ON A.AppraisalDeadLineMasterId = b.AppraisalDeadLineMasterId
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
//        FROM    dbo.tblKpiDeadlineMaster A
//        LEFT JOIN dbo.tblKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
//        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
//        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
//        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
//        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
//        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
//		LEFT JOIN dbo.tblAppraisalSelfMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId 
//        left JOIN (SELECT AppraisalSelfMasterId,MAX(Version)MaxVer FROM dbo.tblAppraisalSelfAppLog  GROUP BY AppraisalSelfMasterId) AS CELog ON CELog.AppraisalSelfMasterId= app.AppraisalSelfMasterId
//								left JOIN dbo.tblAppraisalSelfAppLog ON tblAppraisalSelfAppLog.AppraisalSelfMasterId = app.AppraisalSelfMasterId
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
                string query = @"	SELECT A.AppraisalSelfMasterId FROM dbo.tblAppraisalSelfMaster A WHERE a.EmpInfoId = " + emp +
                    " AND A.FinancialYearId = " + year + "";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable GetCheckApprisalAlreadyExist(int AppraisalSelfMasterId)
        {
            try
            {
                string query = @"	SELECT  AppraisalSelfMasterId,AppraisalMasterId FROM dbo.tblAppraisalMaster   WHERE  AppraisalSelfMasterId = " + AppraisalSelfMasterId;

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
                aParameters.Add(new SqlParameter("@AppraisalMasterId", master));


                string query = @"   INSERT INTO [dbo].[DELtblAppraisalMaster]
           ([AppraisalMasterId]
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
      ,[AppraisalSelfMasterId]
      ,[ActionVersion]
      ,[CurrentStatus]
      ,[SelfApprove])
    
SELECT [AppraisalMasterId]
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
      ,[AppraisalSelfMasterId]
      ,[ActionVersion]
      ,[CurrentStatus]
      ,[SelfApprove]
  FROM [dbo].[tblAppraisalMaster] where AppraisalMasterId=@AppraisalMasterId




  INSERT INTO [dbo].[DELtblAppraisalFuncArea]
           ([AppraisalFucAreaId]
      ,[AppraisalMasterId]
      ,[KpiInfo]
      ,[KpiWeight]
      ,[Deadline]
      ,[MidYearStatus]
      ,[ResultYearEnd]
      ,[SelfMark]
      ,[SupervisorMark]
      ,[Target]
      ,[AppraisalSelfMasterId]
      ,[KpiWeightPer]
      ,[TargetPer]
      ,[AppraisalSelfFucAreaId])
    
SELECT [AppraisalFucAreaId]
      ,[AppraisalMasterId]
      ,[KpiInfo]
      ,[KpiWeight]
      ,[Deadline]
      ,[MidYearStatus]
      ,[ResultYearEnd]
      ,[SelfMark]
      ,[SupervisorMark]
      ,[Target]
      ,[AppraisalSelfMasterId]
      ,[KpiWeightPer]
      ,[TargetPer]
      ,[AppraisalSelfFucAreaId]
  FROM [dbo].[tblAppraisalFuncArea] where AppraisalMasterId=@AppraisalMasterId





    INSERT INTO [dbo].[DELtblAppraisalBehaveArea]
           ([AppraisalBehaveId]
      ,[AppraisalMasterId]
      ,[SkillInfo]
      ,[SupportingEmp]
      ,[Score]
      ,[AppraisalSelfMasterId]
      ,[SupervisorScore]
      ,[SetScore]
      ,[SelfScore])
    
SELECT [AppraisalBehaveId]
      ,[AppraisalMasterId]
      ,[SkillInfo]
      ,[SupportingEmp]
      ,[Score]
      ,[AppraisalSelfMasterId]
      ,[SupervisorScore]
      ,[SetScore]
      ,[SelfScore]
  FROM [dbo].[tblAppraisalBehaveArea] where AppraisalMasterId=@AppraisalMasterId


INSERT INTO [dbo].[DeLtblAppraisalTrainingNeeds]
           ([AppraisalTrainingId]
      ,[AppraisalMasterId]
      ,[TrainingNeeds]
      ,[TrainingStart]
      ,[TrainingEnd]
      ,[TrainingType]
      ,[Quater])
    
SELECT [AppraisalTrainingId]
      ,[AppraisalMasterId]
      ,[TrainingNeeds]
      ,[TrainingStart]
      ,[TrainingEnd]
      ,[TrainingType]
      ,[Quater]
  FROM [dbo].[tblAppraisalTrainingNeeds] where AppraisalMasterId=@AppraisalMasterId





   INSERT INTO [dbo].[DELtblAppraisalFinalStatus]
           ([ApprisalFinalStatusId]
      ,[AppraisalMasterId]
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
      ,[AppraisalMasterId]
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
  FROM [dbo].[tblAppraisalFinalStatus] where AppraisalMasterId=@AppraisalMasterId



 INSERT INTO [dbo].[DELtblAppraisalMasterAppLog]
           ([AppraisalMasterAppLogId]
      ,[AppraisalMasterId]
      ,[PreEmpInfoId]
      ,[ForEmpInfoId]
      ,[Version]
      ,[ApproveBy]
      ,[ApproveDate]
      ,[ActionStatus]
      ,[Comments]
      ,[CommentEmpId])
    
SELECT [AppraisalMasterAppLogId]
      ,[AppraisalMasterId]
      ,[PreEmpInfoId]
      ,[ForEmpInfoId]
      ,[Version]
      ,[ApproveBy]
      ,[ApproveDate]
      ,[ActionStatus]
      ,[Comments]
      ,[CommentEmpId]
  FROM [dbo].tblAppraisalMasterAppLog where AppraisalMasterId=@AppraisalMasterId


	  DELETE FROM [dbo].[tblAppraisalMaster]
      WHERE AppraisalMasterId=@AppraisalMasterId


	  DELETE FROM [dbo].[tblAppraisalFuncArea]
      WHERE AppraisalMasterId=@AppraisalMasterId


	    DELETE FROM [dbo].[tblAppraisalBehaveArea]
      WHERE AppraisalMasterId=@AppraisalMasterId


	  
	    DELETE FROM [dbo].tblAppraisalTrainingNeeds
      WHERE AppraisalMasterId=@AppraisalMasterId


	  	  
	    DELETE FROM [dbo].tblAppraisalFinalStatus
      WHERE AppraisalMasterId=@AppraisalMasterId




DELETE FROM [dbo].[tblAppraisalMasterAppLog]
      WHERE AppraisalMasterId=@AppraisalMasterId

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
//                string query = @"select A.EmpInfoId,A.FinancialYearId,a.AppraisalSelfMasterId , (b.EmpMasterCode+':'+b.EmpName+':'+desg.Designation) employee ,dpt.DepartmentName ,c.CompanyName , y.FinancialYearDesc from  tblAppraisalSelfMaster A 
//                left join tblEmpGeneralInfo B on a.EmpInfoId = b.EmpInfoId
//                left join tblCompanyInfo c on b.CompanyId = c.CompanyId
//                left join tblFinancialYear y on a.FinancialYearId = y.FinancialYearId
//                left join tblDesignation desg on b.DesignationId = desg.DesignationId
//                left join tblDepartment dpt on  b.departmentId = dpt.departmentId
//                LEFT JOIN dbo.tblAppraisalDeadlineMaster appM ON a.FinancialYearId = appM.FinancialYearId AND appM.CompanyId = b.CompanyId
//		        LEFT JOIN dbo.tblAppraisalDeadLineDetails appD ON b.ReportingEmpId = appD.EmpinfoId  AND appM.AppraisalDeadLineMasterId = appD.AppraisalDeadLineMasterId
//                where (a.IsDelete is null or a.IsDelete = 0) and  A.ActionStatus='" + actionstatus + "'   AND B.ReportingEmpId =  " + Convert.ToInt32(empId) + " AND b.IsActive = 1 ";
//                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
//                //               string query = @"select A.EmpInfoId,A.FinancialYearId,a.AppraisalSelfMasterId , (b.EmpMasterCode+':'+b.EmpName+':'+desg.Designation) employee ,dpt.DepartmentName ,c.CompanyName , y.FinancialYearDesc from  tblAppraisalSelfMaster A 
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
                string query = @"select A.EmpInfoId,A.FinancialYearId,a.AppraisalSelfMasterId ,  b.EmpMasterCode,b.EmpName,  ISNULL(+desg.Designation,'') Designation ,dpt.DepartmentName ,c.CompanyName , y.FinancialYearDesc,* from  tblAppraisalSelfMaster A 
                left join tblEmpGeneralInfo B on a.EmpInfoId = b.EmpInfoId
                left join tblCompanyInfo c on b.CompanyId = c.CompanyId
                left join tblFinancialYear y on a.FinancialYearId = y.FinancialYearId
                left join tblDesignation desg on b.DesignationId = desg.DesignationId
                left join tblDepartment dpt on  b.departmentId = dpt.departmentId
                LEFT JOIN dbo.tblKpiDeadlineMaster appM ON a.FinancialYearId = appM.FinancialYearId AND appM.CompanyId = b.CompanyId
		        LEFT JOIN dbo.tblKPIDeadLineDetails appD ON b.EmpInfoId = appD.EmpinfoId  AND appM.KPIDeadLineMasterId = appD.KPIDeadLineMasterId
                INNER JOIN (SELECT AppraisalSelfMasterId,MAX(Version)MaxVer FROM dbo.tblAppraisalSelfAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalSelfMasterId) AS CELog ON CELog.AppraisalSelfMasterId= A.AppraisalSelfMasterId
								INNER JOIN dbo.tblAppraisalSelfAppLog ON tblAppraisalSelfAppLog.AppraisalSelfMasterId = A.AppraisalSelfMasterId
                                where (a.IsDelete is null or a.IsDelete = 0) and Version=CELog.MaxVer and  B.EmpInfoId NOT IN (SELECT EmployeeId from tblEmployeeJobLeft)  and    ForEmpInfoId = '" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'";
                
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
                //               string query = @"select A.EmpInfoId,A.FinancialYearId,a.AppraisalSelfMasterId , (b.EmpMasterCode+':'+b.EmpName+':'+desg.Designation) employee ,dpt.DepartmentName ,c.CompanyName , y.FinancialYearDesc from  tblAppraisalSelfMaster A 
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
                string query = @"select b.CompanyId,  A.* , (b.EmpMasterCode+':'+b.EmpName) as employee from tblAppraisalSelfMaster A 
                left join tblEmpGeneralInfo b on a.EmpInfoId = b.EmpInfoId 
                    where A.AppraisalSelfMasterId =  " + id + "";
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
FROM    tblAppraisalSelfMaster A
LEFT JOIN dbo.tblFinancialYear f ON f.FinancialYearId = A.FinancialYearId

WHERE  A.AppraisalSelfMasterId = " + id + "";
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
A.KpiInfo ObjectiveGoal,
         (A.KpiWeight) KpiWeight ,
        A.KpiWeightPer ,
        CONVERT(NVARCHAR(11), A.Deadline, 106) AS Deadline ,
        null as  MidYearStatus ,
        null as  ResultYearEnd ,
        0 as   SelfMark ,
       
         (A.Target)  Target,
        A.TargetPer,
		null as  SupervisorMark,A.IsActive
FROM    tblAppraisalSelfFuncArea A

WHERE  A.AppraisalSelfMasterId = " + id + "";
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
                string query = @"SELECT  A.AppraisalSelfFucAreaId ,
        A.AppraisalSelfMasterId ,
        A.KpiInfo ,
        A.KpiWeight ,
        A.KpiWeightPer ,
        CONVERT(NVARCHAR(11), A.Deadline, 106) AS Deadline ,
         MidYearStatus ,
        ResultYearEnd ,
        SelfMark ,
       
        A.Target,
        A.TargetPer,
	  SupervisorMark,0 IsActive
FROM    tblAppraisalFuncArea A WHERE
a.AppraisalMasterId= " + id + "";
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
                string query = @"SELECT  A.AppraisalSelfFucAreaId ,
        A.AppraisalSelfMasterId ,
        A.KpiInfo ,
        A.KpiWeight ,
        A.KpiWeightPer ,
        CONVERT(NVARCHAR(11), A.Deadline, 106) AS Deadline ,
        isnull(M.MidYearStatus,'') as  MidYearStatus ,
        ISNULL(M.SupervisorMark,0) as  ResultYearEnd ,
        0 as   SelfMark ,
       
        A.Target,
        A.TargetPer,
		null as  SupervisorMark,A.IsActive
FROM    tblAppraisalSelfFuncArea A
left join tblKPIMIDAppraisalFuncArea M  on A.AppraisalSelfFucAreaId=M.AppraisalSelfFucAreaId
WHERE A.IsActive=1 AND   A.AppraisalSelfMasterId = " + id + "";
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
SELECT  A.AppraisalSelfFucAreaId,
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
		A.SupervisorMark,b.IsActive
FROM    tblAppraisalFuncArea A

inner join tblAppraisalSelfFuncArea b on a.AppraisalSelfFucAreaId=b.AppraisalSelfFucAreaId
WHERE  b.IsActive=1 and   A.AppraisalSelfMasterId = " + id + "";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool SaveAppraisalPartB(List<AppraisalBehaveArea> appraisal, int id)
        {
            try
            {

                string delQ = @"delete from tblAppraisalBehaveArea where AppraisalSelfMasterId = " + id + "";
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

                    string query = @"Insert into tblAppraisalBehaveArea ( SetScore, AppraisalMasterId, SkillInfo, SupportingEmp, Score,AppraisalSelfMasterId,SelfScore , SupervisorScore, Comments)
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


        public DataTable GetAppraisalSelfB(int id)
        {
            try
            {
                string query = @"select  CAST(ISNULL(SetScore,0) as int) SetScore,  (Score) Score,*, 0 as SelfScore from tblAppraisalSelfBehaveArea where AppraisalSelfMasterId = " + id + " ";
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
                string query = @"select  CAST(ISNULL(SetScore,0) as int) SetScore,  (0) Score,*, 0 as SelfScore from tblAppraisalSelfBehaveArea where AppraisalSelfMasterId = " + id + " ";
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
                                                    AppraisalSelfMasterId ,
                                                    SkillInfo ,
                                                    SupportingEmp ,
                                                  SelfScore   Score ,
                                                    SupervisorScore ,
                                                    SetScore	, 0 as SelfScore from tblAppraisalBehaveArea WHERE
AppraisalMasterId=" + id + " ";
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
                string query = @"select    SetScore,* from tblAppraisalBehaveArea where AppraisalMasterId = " + id + " ";
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

        public DataTable GetApproveLogBySelfMaster(int id)
        {
            //SELECT (e.EmpMasterCode+' : '+e.EmpName+':'+desg.Designation+''+dpt.DepartmentName) AS Employee , CONVERT(NVARCHAR( 11) , A.EntryDate , 106) AS EntryDate , A.PreviousVersion , A.Remarks , A.ApproveStatus FROM dbo.tblAppraisalSelfAppLog A LEFT JOIN dbo.tblUser u ON a.EntryBy = u.UserName
            //LEFT JOIN dbo.tblEmpGeneralInfo e ON u.EmpInfoId = e.EmpInfoId
            //LEFT JOIN dbo.tblDesignation desg ON e.DesignationId = desg.DesignationId
            //LEFT JOIN dbo.tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
            try
            {
                string query = @"  select    (e.EmpMasterCode+' : '+e.EmpName+ISNULL(' : '+desg.Designation,'')+ISNULL(' : '+dpt.DepartmentName,'')) AS Employee , FORMAT(A.ApproveDate,'dd-MMM-yyyy hh:mm tt') AS EntryDate ,A.Comments  AS PreviousVersion ,'' as Remarks , case when A.ForwardBy is not null then  '  forwarded By ['+ ISNULL(efor.EmpMasterCode+' : '+efor.EmpName,uf.UserName) +']'   when A.ActionStatus='Review' then 'Returned' when a.Version=2 then 'Submitted' else A.ActionStatus end  AS ApproveStatus 
            FROM dbo.tblAppraisalSelfAppLog A 
            LEFT JOIN dbo.tblUser u ON a.ApproveBy = u.UserId
			LEFT JOIN dbo.tblEmpGeneralInfo e ON u.EmpInfoId = e.EmpInfoId
            LEFT JOIN dbo.tblUser uf ON a.ForwardBy = uf.UserId

			LEFT JOIN dbo.tblEmpGeneralInfo efor ON uf.EmpInfoId = efor.EmpInfoId
			LEFT JOIN dbo.tblDesignation desg ON e.DesignationId = desg.DesignationId
			LEFT JOIN dbo.tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
			WHERE  A.ActionStatus<>'Drafted' and A.AppraisalSelfMasterId= " + id + " order by A.AppraisalSelfAppLogId asc ";

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
                string query = @"SELECT * FROM dbo.tblAppraisalFuncArea WHERE AppraisalMasterId = " + id + " ";
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


                string query = @"UPDATE dbo.tblAppraisalMaster SET SelfApprove = 'Posted' WHERE AppraisalMasterId = " +
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
            SELECT @reportTO = emp.ReportingEmpId  ,@appraisalSelf = A.AppraisalSelfMasterId ,  @ActionVersion = ISNULL(a.ActionVersion,0) FROM dbo.tblAppraisalMaster A LEFT JOIN dbo.tblEmpGeneralInfo emp ON a.EmpInfoId = emp.EmpInfoId  WHERE AppraisalMasterId = @masterId
            
            IF(@reportTO = @user)
            BEGIN
            	UPDATE dbo.tblAppraisalMaster SET CurrentStatus = 'Returned' , ActionVersion = @ActionVersion+1 , SelfApprove='Drafted'  ,   IsApprove = 0 , ApproveBy = @entryBy WHERE AppraisalMasterId = @masterId
            	INSERT INTO dbo.tblAppraisalApproveLog
            	        ( AppraisalSelfMasterId ,
            	          AppraisalMasterId ,
            	          PreviousVersion ,
            	          NewVersion ,
            	          EntryDate ,
            	          EntryBy ,
            	          ApproveStatus ,
            	          Remarks
            	        )
            	VALUES  ( @appraisalSelf , -- AppraisalSelfMasterId - int
            	          @masterId , -- AppraisalMasterId - int
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
            	UPDATE dbo.tblAppraisalMaster SET CurrentStatus = 'Returned' , ActionVersion = @ActionVersion+1 ,    IsApprove = 0 , ApproveBy = @entryBy WHERE AppraisalMasterId = @masterId
            	INSERT INTO dbo.tblAppraisalApproveLog
            	        ( AppraisalSelfMasterId ,
            	          AppraisalMasterId ,
            	          PreviousVersion ,
            	          NewVersion ,
            	          EntryDate ,
            	          EntryBy ,
            	          ApproveStatus ,
            	          Remarks
            	        )
            	VALUES  ( @appraisalSelf , -- AppraisalSelfMasterId - int
            	          @masterId , -- AppraisalMasterId - int
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
                string query = @"SELECT *,tblbehav.SupScoreBehave+tblfuc.SupScoreFunc AS TotalScore FROM dbo.tblAppraisalMaster
                                LEFT JOIN 
                                (SELECT AppraisalMasterId,SUM(SupervisorMark)SupScoreFunc FROM dbo.tblAppraisalFuncArea GROUP BY AppraisalMasterId)AS tblfuc ON tblfuc.AppraisalMasterId = tblAppraisalMaster.AppraisalMasterId
                                LEFT JOIN 
                                (SELECT AppraisalMasterId,SUM(SupervisorScore)SupScoreBehave FROM dbo.tblAppraisalBehaveArea GROUP BY AppraisalMasterId) AS tblbehav ON tblbehav.AppraisalMasterId = tblAppraisalMaster.AppraisalMasterId
                                WHERE tblAppraisalMaster.AppraisalMasterId='"+id+"'";
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
                aParameters.Add(new SqlParameter("@AppraisalSelfMasterId", appselfMasterId));

                string query = @"insert into tblAppraisalMaster
(FinancialYearId,EmpInfoId,EntryDate,EntryBy,UpdateBy,UpdateDate,IsDelete,DeleteBy,IsApprove,ApproveBy,ApproveDate,AppraisalSelfMasterId,ActionVersion,CurrentStatus,SelfApprove,FYDes_App) 
select FinancialYearId,EmpInfoId,EntryDate,EntryBy,UpdateBy,UpdateDate,IsDelete,DeleteBy,IsApprove,ApproveBy,ApproveDate,AppraisalSelfMasterId,'0',ActionStatus,'Posted',  (SELECT FinancialYearDesc 
                    FROM dbo.tblFinancialYear 
                    WHERE FinancialYearId = tblAppraisalMaster.FinancialYearId ) from tblAppraisalSelfMaster where AppraisalSelfMasterId=@AppraisalSelfMasterId";


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
            SELECT @reportTO = emp.ReportingEmpId  ,@appraisalSelf = A.AppraisalSelfMasterId ,  @ActionVersion = ISNULL(a.ActionVersion,0) FROM dbo.tblAppraisalMaster A LEFT JOIN dbo.tblEmpGeneralInfo emp ON a.EmpInfoId = emp.EmpInfoId  WHERE AppraisalMasterId = @masterId
            
       UPDATE dbo.tblAppraisalMaster SET CurrentStatus = 'Approved' , ActionVersion = @ActionVersion+1 ,    IsApprove = 1 , ApproveBy =  @entryBy WHERE AppraisalMasterId = @masterId
            	INSERT INTO dbo.tblAppraisalApproveLog
            	        ( AppraisalSelfMasterId ,
            	          AppraisalMasterId ,
            	          PreviousVersion ,
            	          NewVersion ,
            	          EntryDate ,
            	          EntryBy ,
            	          ApproveStatus ,
            	          Remarks
            	        )
            	VALUES  ( @appraisalSelf , -- AppraisalSelfMasterId - int
            	          @masterId , -- AppraisalMasterId - int
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
                    aParameters.Add(new SqlParameter("@AppraisalSelfAppLogId", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", status));


                    string query =
                        @"update tblAppraisalSelfAppLog set ActionStatus=@ActionStatus  where AppraisalSelfAppLogId = @AppraisalSelfAppLogId";

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
                    aParameters.Add(new SqlParameter("@AppraisalSelfMasterId", appLogDao.AppraisalSelfMasterId));
                    aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                    aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                    aParameters.Add(new SqlParameter("@Version", appLogDao.Version));
                    aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                    aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                    aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                    aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));
                    aParameters.Add(new SqlParameter("@CommentsEMPID", appLogDao.CommentsEMP));



                    string query = @"INSERT INTO dbo.tblAppraisalSelfAppLog
                                    (
                                    AppraisalSelfMasterId,
                                    PreEmpInfoId,
                                    ForEmpInfoId,
                                    Version,
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments,CommentsEMPID
                                    )
                                    VALUES(
                                    @AppraisalSelfMasterId,
                                    @PreEmpInfoId,
                                    @ForEmpInfoId,
                                    (SELECT (COUNT(*)+1) FROM dbo.tblAppraisalSelfAppLog WHERE AppraisalSelfMasterId=@AppraisalSelfMasterId),
                                    @ApproveBy,
                                    @ApproveDate,
                                    @ActionStatus,@Comments,@CommentsEMPID
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
                string query = @"SELECT * FROM dbo.tblAppraisalSelfAppLog WHERE ForEmpInfoId='" + forempInfoid + "' AND AppraisalSelfMasterId='" + jdmasterId + "' AND ActionStatus NOT IN ('Review')   order by AppraisalSelfAppLogId desc";

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

                if (aMaster.AppraisalSelfMasterId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@AppraisalSelfMasterId", aMaster.AppraisalSelfMasterId));
                    aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));


                    string query =
                        @"update tblAppraisalSelfMaster set ActionStatus=@ActionStatus  where AppraisalSelfMasterId = @AppraisalSelfMasterId";

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
                string query = @"SELECT * FROM dbo.tblAppraisalSelfAppLog
left JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblAppraisalSelfAppLog.CommentsEMPID
 WHERE AppraisalSelfMasterId='" + id+"' AND tblAppraisalSelfAppLog.ActionStatus<>'Drafted'";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

    }
}
