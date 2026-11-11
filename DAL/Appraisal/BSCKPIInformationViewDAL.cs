using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;

namespace DAL.Appraisal
{

       
   public class BSCKPIInformationViewDAL
    {
        ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        DataAccessManager _aAcessManager = new DataAccessManager();

        public void LoadDept(DropDownList ddl, string compId)
        {
            string query = @"SELECT * FROM dbo.tblDepartment WHERE Invisible IS NULL and CompanyId='" + compId + "'";
            _aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", query, DataBase.HRDB);
        }
        public bool UpdateKPIApprovePersonContractural(string masterId, string EmpId, string PreviousForEmpInfoId)
        {

            try
            {
                
                     


                    string query =
                        @"update dbo.tblBSCAppraisalSelfAppLog SET ForEmpInfoId='" + EmpId + "'  , PreviousForEmpInfoId='" + PreviousForEmpInfoId + "'  , ForwardBy='" + HttpContext.Current.Session["UserId"].ToString() + "',ForwardDate=GETDATE()   where BSCAppraisalSelfAppLogId =" + masterId;

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, DataBase.HRDB);
                    return result;

               

            }
            catch (Exception exception)
            {

                throw exception;
            }
            return true;
        }


        public bool UpdateKPIApprovePersonContracturalSame(string masterId, string EmpId, string PreviousForEmpInfoId, string MainId)
        {

            try
            {




                string query =
                    @"   update dbo.tblBSCAppraisalSelfMaster SET  ActionStatus='Drafted'  where BSCAppraisalSelfMasterId ='" + MainId + "'   update dbo.tblBSCAppraisalSelfAppLog SET ForEmpInfoId='" + EmpId + "'  , PreviousForEmpInfoId='" + PreviousForEmpInfoId + "'  , ForwardBy='" + HttpContext.Current.Session["UserId"].ToString() + "',ForwardDate=GETDATE()      where BSCAppraisalSelfAppLogId =" + masterId;

                bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, DataBase.HRDB);
                return result;



            }
            catch (Exception exception)
            {

                throw exception;
            }
            return true;
        }


        public bool UpdateAppprisalApprovePersonContractural(string masterId, string EmpId, string PreviousForEmpInfoId)
        {

            try
            {



                string query =
                      @"update dbo.tblBSCAppraisalMasterAppLog SET ForEmpInfoId='" + EmpId + "'  , PreviousForEmpInfoId='" + PreviousForEmpInfoId + "'  , ForwardBy='" + HttpContext.Current.Session["UserId"].ToString() + "',ForwardDate=GETDATE()   where BSCAppraisalMasterAppLogId =" + masterId;
                

                bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, DataBase.HRDB);
                return result;



            }
            catch (Exception exception)
            {

                throw exception;
            }
            return true;
        }

        public bool UpdateAppprisalApprovePersonContracturalSamePer(string masterId, string EmpId, string PreviousForEmpInfoId, string MainId)
        {

            try
            {



                string query =
                      @"   update dbo.tblBSCAppraisalMaster SET  CurrentStatus='Drafted'  where BSCAppraisalMasterId ='" + MainId + "'  update dbo.tblBSCAppraisalMasterAppLog SET ForEmpInfoId='" + EmpId + "'  , PreviousForEmpInfoId='" + PreviousForEmpInfoId + "'  , ForwardBy='" + HttpContext.Current.Session["UserId"].ToString() + "',ForwardDate=GETDATE()     where BSCAppraisalMasterAppLogId =" + masterId;


                bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, DataBase.HRDB);
                return result;



            }
            catch (Exception exception)
            {

                throw exception;
            }
            return true;
        }

        public bool CancelAppprisalApprovePersonContractural(string MainAppraisalMasterId, string masterId, string EmpId)
        {

            try
            {



                string query =
                      @"
update dbo.tblAppraisalMaster SET CurrentStatus='Verified' WHERE AppraisalMasterId='" + MainAppraisalMasterId + "'       update dbo.tblAppraisalMasterAppLog SET ForEmpInfoId='" + EmpId + "', IsAuditTrail=1,ActionStatus='Verified', CancelBy='" + HttpContext.Current.Session["UserId"].ToString() + "',CancelDate=GETDATE()  WHERE AppraisalMasterAppLogId=" + masterId
 ;


                bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, DataBase.HRDB);
                return result;



            }
            catch (Exception exception)
            {

                throw exception;
            }
            return true;
        }
        public DataTable GetApprisalApprovaer(int id)
        {
            try
            {
                string query = @"SELECT  TOP 1 *
            
            FROM dbo.tblAppraisalMasterAppLog A 
            
			WHERE  A.ActionStatus<>'Drafted' and A.AppraisalMasterId= " + id + "  ORDER BY A.AppraisalMasterAppLogId desc";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataTable GetKPIIfSamePerson(int id)
        {
            try
            {
                string query = @"select EmpInfoId from tblBSCAppraisalSelfMaster  where BSCAppraisalSelfMasterId=" + id;
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetAppraisalIfSamePerson(int id)
        {
            try
            {
                string query = @"select EmpInfoId from tblBSCAppraisalMaster where BSCAppraisalMasterId=" + id;
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public DataTable GetApprisalApprovaerNameTop(int id)
        {
            try
            {
                string query = @"SELECT  TOP 1  empImmediate.EmpMasterCode+' : '+ empImmediate.EmpName empImmediateName,  empLastApprov.EmpMasterCode+' : '+ empLastApprov.EmpName empLastApprovName
            
            FROM dbo.tblAppraisalMasterAppLog A 
			LEFT JOIN dbo.tblEmpGeneralInfo empImmediate ON  empImmediate.EmpInfoId=A.PreEmpInfoId

			LEFT JOIN dbo.tblEmpGeneralInfo empLastApprov ON  empLastApprov.EmpInfoId=A.ForEmpInfoId

            
			WHERE  A.ActionStatus<>'Drafted' AND A.ActionStatus<>'Approved' and A.AppraisalMasterId=  " + id + "   ORDER BY A.AppraisalMasterAppLogId desc";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetKPIApprovaer(int id)
        {
            try
            {
                string query = @"SELECT  TOP 1 *
            
            FROM  dbo.tblBSCAppraisalSelfAppLog  A 
            
			WHERE  A.ActionStatus<>'Drafted' and A.BSCAppraisalSelfMasterId= " + id + "  ORDER BY A.BSCAppraisalSelfAppLogId desc";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetFianncialYearByComIdDDl(int id)
        {
            string query = @"SELECT FinancialYearId as Value,FinancialYearDesc as TextField FROM tblFinancialYear where CompanyId =" + id + " and Status ='Active' ";
            return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);


        }
       public DataTable GetAppraisalByKpiPermission(string companyId,  string param )
       {
           try
           {
               string query = @"SELECT DISTINCT  ISNULL(app.AppraisalMasterId,0) AppraisalMasterId,  ISNULL(SMAster.AppraisalSelfMasterId,0) AppraisalSelfMasterId, tblA.EmpName,e.EmpInfoId ,e.EmpMasterCode,  e.EmpName EmpllName, desg.Designation,
          ('Employee ID: '+ e.EmpMasterCode+', Employee Name: '+ +e.EmpName+ ISNULL(', Designation: '+desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove, (CASE WHEN SMAster.ActionStatus<>'Approved' then 'Not Approved' ELSE SMAster.ActionStatus END)AS ActionStatus,ForEmp.EmpName as PendingEmp
,(CASE WHEN tblAppraisalMasterAppLog.ActionStatus<>'Approved' then 'Not Approved' ELSE tblAppraisalMasterAppLog.ActionStatus END)AS ActionStatusAppraisal,ForEmpApp.EmpName as PendingEmpApp
        FROM    dbo.tblKpiDeadlineMaster A
        LEFT JOIN dbo.tblKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
 INNER JOIN dbo.tblAppraisalSelfMaster SMAster ON B.EmpinfoId = SMAster.EmpinfoId  AND SMAster.FinancialYearId = A.FinancialYearId
		LEFT JOIN dbo.tblAppraisalMaster app ON e.EmpInfoId = app.EmpInfoId AND SMAster.FinancialYearId = app.FinancialYearId 
		   LEFT JOIN (SELECT TOP 1 AppraisalSelfMasterId,tblEmpGeneralInfo.EmpName FROM tblAppraisalSelfAppLog  
		   INNER JOIN dbo.tblEmpGeneralInfo ON  tblAppraisalSelfAppLog.ForEmpInfoId=tblEmpGeneralInfo.EmpInfoId) tblA ON SMAster.AppraisalSelfMasterId = tblA.AppraisalSelfMasterId
LEFT  JOIN (SELECT AppraisalSelfMasterId, MAX(Version)MaxVer FROM dbo.tblAppraisalSelfAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalSelfMasterId) AS CELog ON CELog.AppraisalSelfMasterId= SMAster.AppraisalSelfMasterId
								LEFT  JOIN dbo.tblAppraisalSelfAppLog ON tblAppraisalSelfAppLog.AppraisalSelfMasterId = SMAster.AppraisalSelfMasterId   and tblAppraisalSelfAppLog.Version = CONVERT(INT,CELog.MaxVer) 
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblAppraisalSelfAppLog.ForEmpInfoId

								LEFT  JOIN (SELECT AppraisalMasterId,MAX(Version)MaxVer FROM dbo.tblAppraisalMasterAppLog WHERE ActionStatus NOT IN
								('Review')     GROUP BY AppraisalMasterId) AS ALog ON ALog.AppraisalMasterId= app.AppraisalMasterId

								LEFT  JOIN dbo.tblAppraisalMasterAppLog ON tblAppraisalMasterAppLog.AppraisalMasterId = ALog.AppraisalMasterId and tblAppraisalMasterAppLog.Version = CONVERT(INT,ALog.MaxVer) 
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmpApp ON ForEmpApp.EmpInfoId=tblAppraisalMasterAppLog.ForEmpInfoId

where A.CompanyId = " + companyId + "  " + param + "";

               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }



       }



       public DataTable GetAppraisalByKpiPermission_New__(string companyId, string param, string param2)
       {
           try
           {
               string query = @" SELECT DISTINCT   case when ActionStatusAppraisal='Approved' then PromotionwithIncrement else '' end PromotionwithIncrement, case when ActionStatusAppraisal='Approved' then TotalMarks else '0' end TotalMarks, * FROM(SELECT DISTINCT  CASE WHEN appFin.Other=1 THEN 'Promotion with Increment' +ISNULL(' [Note:'+appFin.Note+']','')  WHEN appFin.GeneralIncrement=1 THEN 'General Increment'   WHEN appFin.IsPromotion=1 THEN 'Promotion'   WHEN appFin.SpecialIncrement=1 THEN 'Special Increment'  WHEN appFin.Pip=1 THEN 'Performance Improvement Plan'   else ' ' END PromotionwithIncrement, sal.GradeCode+ISNULL(+' : '+sal.GradeName,0) GradeCode, FORMAT(e.DateOfJoin,'dd-MMM-yyy') DateOfJoin, ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0) TotalMarks, div.DivisionName,ISNULL(app.AppraisalMasterId,0) AppraisalMasterId,  ISNULL(SMAster.AppraisalSelfMasterId,0) AppraisalSelfMasterId, tblA.EmpName,e.EmpInfoId ,e.EmpMasterCode,  e.EmpName EmpllName, desg.Designation,
          ('Employee ID: '+ e.EmpMasterCode+', Employee Name: '+ +e.EmpName+ ISNULL(', Designation: '+desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove, (CASE WHEN SMAster.ActionStatus<>'Approved' then 'Not Approved' ELSE SMAster.ActionStatus END)AS ActionStatus,ForEmp.EmpMasterCode+' : '+ForEmp.EmpName as PendingEmp
,(CASE WHEN tblAppraisalMasterAppLog.ActionStatus<>'Approved' then 'Not Approved' ELSE tblAppraisalMasterAppLog.ActionStatus END)AS ActionStatusAppraisal,ForEmpApp.EmpMasterCode+' : '+ ForEmpApp.EmpName as PendingEmpApp
        FROM    dbo.tblKpiDeadlineMaster A
        LEFT JOIN dbo.tblKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
 INNER JOIN dbo.tblAppraisalSelfMaster SMAster ON B.EmpinfoId = SMAster.EmpinfoId  AND SMAster.FinancialYearId = A.FinancialYearId
		LEFT JOIN dbo.tblAppraisalMaster app ON e.EmpInfoId = app.EmpInfoId AND SMAster.FinancialYearId = app.FinancialYearId 
LEFT JOIN ( SELECT  SUM(SupervisorMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON app.AppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SupervisorScore) Score ,
                            AppraisalMasterId
                    FROM    tblAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON app.AppraisalMasterId = behave.AppraisalMasterId
				  left join tblSalaryGrade sal on sal.SalaryGradeId=e.SalaryGradeId
				   LEFT JOIN tblDivision div ON e.DivisionId = div.DivisionId
 LEFT JOIN dbo.tblAppraisalFinalStatus appFin ON appFin.AppraisalMasterId = app.AppraisalMasterId
		   LEFT JOIN (SELECT TOP 1 AppraisalSelfMasterId,tblEmpGeneralInfo.EmpName FROM tblAppraisalSelfAppLog  
		   INNER JOIN dbo.tblEmpGeneralInfo ON  tblAppraisalSelfAppLog.ForEmpInfoId=tblEmpGeneralInfo.EmpInfoId) tblA ON SMAster.AppraisalSelfMasterId = tblA.AppraisalSelfMasterId
LEFT  JOIN (SELECT AppraisalSelfMasterId, MAX(Version)MaxVer FROM dbo.tblAppraisalSelfAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalSelfMasterId) AS CELog ON CELog.AppraisalSelfMasterId= SMAster.AppraisalSelfMasterId
								LEFT  JOIN dbo.tblAppraisalSelfAppLog ON tblAppraisalSelfAppLog.AppraisalSelfMasterId = SMAster.AppraisalSelfMasterId   and tblAppraisalSelfAppLog.Version = CONVERT(INT,CELog.MaxVer) 
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblAppraisalSelfAppLog.ForEmpInfoId

								LEFT  JOIN (SELECT AppraisalMasterId,MAX(Version)MaxVer FROM dbo.tblAppraisalMasterAppLog WHERE ActionStatus NOT IN
								('Review')     GROUP BY AppraisalMasterId) AS ALog ON ALog.AppraisalMasterId= app.AppraisalMasterId

								LEFT  JOIN dbo.tblAppraisalMasterAppLog ON tblAppraisalMasterAppLog.AppraisalMasterId = ALog.AppraisalMasterId and tblAppraisalMasterAppLog.Version = CONVERT(INT,ALog.MaxVer) 
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmpApp ON ForEmpApp.EmpInfoId=tblAppraisalMasterAppLog.ForEmpInfoId

where A.CompanyId = " + companyId + "  " + param + @"    union all  

SELECT DISTINCT CASE WHEN appFin.Other=1 THEN 'Promotion with Increment' +ISNULL(' [Note:'+appFin.Note+']','')   WHEN appFin.GeneralIncrement=1 THEN 'General Increment'   WHEN appFin.IsPromotion=1 THEN 'Promotion'   WHEN appFin.SpecialIncrement=1 THEN 'Special Increment'  WHEN appFin.Pip=1 THEN 'Performance Improvement Plan'   else '' END PromotionwithIncrement, sal.GradeCode+ISNULL(+' : '+sal.GradeName,0) GradeCode, FORMAT(e.DateOfJoin,'dd-MMM-yyy') DateOfJoin, ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0) TotalMarks, div.DivisionName, ISNULL(app.AppraisalMasterId,0) AppraisalMasterId,  ISNULL(SMAster.AppraisalSelfMasterId,0) AppraisalSelfMasterId, tblA.EmpName,e.EmpInfoId ,e.EmpMasterCode,  e.EmpName EmpllName, desg.Designation,
          ('Employee ID: '+ e.EmpMasterCode+', Employee Name: '+ +e.EmpName+ ISNULL(', Designation: '+desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove, (CASE WHEN SMAster.ActionStatus<>'Approved' then 'Not Approved' ELSE SMAster.ActionStatus END)AS ActionStatus, ForEmp.EmpMasterCode+' : '+ForEmp.EmpName as PendingEmp
,(CASE WHEN tblAppraisalMasterAppLog.ActionStatus<>'Approved' then 'Not Approved' ELSE tblAppraisalMasterAppLog.ActionStatus END)AS ActionStatusAppraisal,ForEmpApp.EmpMasterCode+' : '+ ForEmpApp.EmpName as PendingEmpApp
        FROM    dbo.tblKpiDeadlineMaster A
        LEFT JOIN dbo.tblKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
 INNER JOIN dbo.tblAppraisalSelfMaster SMAster ON B.EmpinfoId = SMAster.EmpinfoId  AND SMAster.FinancialYearId = A.FinancialYearId

 	 inner JOIN   tblEmpAllRefference reff  ON b.EmpinfoId = reff.RefferenceEmpId 
		LEFT JOIN dbo.tblAppraisalMaster app ON e.EmpInfoId = app.EmpInfoId AND SMAster.FinancialYearId = app.FinancialYearId 
LEFT JOIN ( SELECT  SUM(SupervisorMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON app.AppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SupervisorScore) Score ,
                            AppraisalMasterId
                    FROM    tblAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON app.AppraisalMasterId = behave.AppraisalMasterId
				  left join tblSalaryGrade sal on sal.SalaryGradeId=e.SalaryGradeId
				   LEFT JOIN tblDivision div ON e.DivisionId = div.DivisionId
 LEFT JOIN dbo.tblAppraisalFinalStatus appFin ON appFin.AppraisalMasterId = app.AppraisalMasterId
		   LEFT JOIN (SELECT TOP 1 AppraisalSelfMasterId,tblEmpGeneralInfo.EmpName FROM tblAppraisalSelfAppLog  
		   INNER JOIN dbo.tblEmpGeneralInfo ON  tblAppraisalSelfAppLog.ForEmpInfoId=tblEmpGeneralInfo.EmpInfoId) tblA ON SMAster.AppraisalSelfMasterId = tblA.AppraisalSelfMasterId
LEFT  JOIN (SELECT AppraisalSelfMasterId, MAX(Version)MaxVer FROM dbo.tblAppraisalSelfAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalSelfMasterId) AS CELog ON CELog.AppraisalSelfMasterId= SMAster.AppraisalSelfMasterId
								LEFT  JOIN dbo.tblAppraisalSelfAppLog ON tblAppraisalSelfAppLog.AppraisalSelfMasterId = SMAster.AppraisalSelfMasterId   and tblAppraisalSelfAppLog.Version = CONVERT(INT,CELog.MaxVer) 
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblAppraisalSelfAppLog.ForEmpInfoId

								LEFT  JOIN (SELECT AppraisalMasterId,MAX(Version)MaxVer FROM dbo.tblAppraisalMasterAppLog WHERE ActionStatus NOT IN
								('Review')     GROUP BY AppraisalMasterId) AS ALog ON ALog.AppraisalMasterId= app.AppraisalMasterId

								LEFT  JOIN dbo.tblAppraisalMasterAppLog ON tblAppraisalMasterAppLog.AppraisalMasterId = ALog.AppraisalMasterId and tblAppraisalMasterAppLog.Version = CONVERT(INT,ALog.MaxVer) 
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmpApp ON ForEmpApp.EmpInfoId=tblAppraisalMasterAppLog.ForEmpInfoId
								 	 inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 WHERE        reff.ShowCompany in (ComAssain)   " + param2 + " )HH";

               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }



       }

        
       public DataTable GetAppraisalByKpiPermission_New__BSCOKR(string companyId, string param, string param2)
       {
           try
           {
               string query = @"  SELECT DISTINCT OptionInfo,  case when ActionStatusAppraisal='Approved' then PromotionwithIncrement else '' end PromotionwithIncrement, case when ActionStatusAppraisal='Approved' then TotalMarks else '0' end TotalMarks, * FROM(SELECT DISTINCT   b.OptionInfo, CASE WHEN appFin.Other=1 THEN 'Promotion with Increment' +ISNULL(' [Note:'+appFin.Note+']','')  WHEN appFin.GeneralIncrement=1 THEN 'General Increment'   WHEN appFin.IsPromotion=1 THEN 'Promotion'   WHEN appFin.SpecialIncrement=1 THEN 'Special Increment'  WHEN appFin.Pip=1 THEN 'Performance Improvement Plan'   else ' ' END PromotionwithIncrement, sal.GradeCode+ISNULL(+' : '+sal.GradeName,0) GradeCode, FORMAT(e.DateOfJoin,'dd-MMM-yyy') DateOfJoin, ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0) TotalMarks, div.DivisionName,ISNULL(app.BSCAppraisalMasterId,0) BSCAppraisalMasterId,  ISNULL(SMAster.BSCAppraisalSelfMasterId,0) BSCAppraisalSelfMasterId, tblA.EmpName,e.EmpInfoId ,e.EmpMasterCode,  e.EmpName EmpllName, desg.Designation,
          ('Employee ID: '+ e.EmpMasterCode+', Employee Name: '+ +e.EmpName+ ISNULL(', Designation: '+desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove, (CASE WHEN SMAster.ActionStatus<>'Approved' then 'Not Approved' ELSE SMAster.ActionStatus END)AS ActionStatus,ForEmp.EmpMasterCode+' : '+ForEmp.EmpName as PendingEmp
,(CASE WHEN tblBSCAppraisalMasterAppLog.ActionStatus<>'Approved' then 'Not Approved' ELSE tblBSCAppraisalMasterAppLog.ActionStatus END)AS ActionStatusAppraisal,ForEmpApp.EmpMasterCode+' : '+ ForEmpApp.EmpName as PendingEmpApp
         FROM    dbo.tblBSCKpiDeadlineMaster A
        inner JOIN dbo.tblBSCKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
 INNER JOIN dbo.tblBSCAppraisalSelfMaster SMAster ON B.EmpinfoId = SMAster.EmpinfoId  AND SMAster.FinancialYearId = A.FinancialYearId
		LEFT JOIN dbo.tblBSCAppraisalMaster app ON e.EmpInfoId = app.EmpInfoId AND SMAster.FinancialYearId = app.FinancialYearId 
LEFT JOIN ( SELECT  SUM(SupervisorMark) SupervisorMark ,
                            BSCAppraisalMasterId
                    FROM    tblBSCAppraisalFuncArea
                    GROUP BY BSCAppraisalMasterId
                  ) func ON app.BSCAppraisalMasterId = func.BSCAppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SupervisorScore) Score ,
                            BSCAppraisalMasterId
                    FROM    tblBSCAppraisalBehaveArea
                    GROUP BY BSCAppraisalMasterId
                  ) behave ON app.BSCAppraisalMasterId = behave.BSCAppraisalMasterId
				  left join tblSalaryGrade sal on sal.SalaryGradeId=e.SalaryGradeId
				   LEFT JOIN tblDivision div ON e.DivisionId = div.DivisionId
 LEFT JOIN dbo.tblBSCAppraisalFinalStatus appFin ON appFin.BSCAppraisalMasterId = app.BSCAppraisalMasterId
		   LEFT JOIN (SELECT TOP 1 BSCAppraisalSelfMasterId,tblEmpGeneralInfo.EmpName FROM tblBSCAppraisalSelfAppLog  
		   INNER JOIN dbo.tblEmpGeneralInfo ON  tblBSCAppraisalSelfAppLog.ForEmpInfoId=tblEmpGeneralInfo.EmpInfoId) tblA ON SMAster.BSCAppraisalSelfMasterId = tblA.BSCAppraisalSelfMasterId
LEFT  JOIN (SELECT BSCAppraisalSelfMasterId, MAX(Version)MaxVer FROM dbo.tblBSCAppraisalSelfAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY BSCAppraisalSelfMasterId) AS CELog ON CELog.BSCAppraisalSelfMasterId= SMAster.BSCAppraisalSelfMasterId
								LEFT  JOIN dbo.tblBSCAppraisalSelfAppLog ON tblBSCAppraisalSelfAppLog.BSCAppraisalSelfMasterId = SMAster.BSCAppraisalSelfMasterId   and tblBSCAppraisalSelfAppLog.Version = CONVERT(INT,CELog.MaxVer) 
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblBSCAppraisalSelfAppLog.ForEmpInfoId

								LEFT  JOIN (SELECT BSCAppraisalMasterId,MAX(Version)MaxVer FROM dbo.tblBSCAppraisalMasterAppLog WHERE ActionStatus NOT IN
								('Review')     GROUP BY BSCAppraisalMasterId) AS ALog ON ALog.BSCAppraisalMasterId= app.BSCAppraisalMasterId

								LEFT  JOIN dbo.tblBSCAppraisalMasterAppLog ON tblBSCAppraisalMasterAppLog.BSCAppraisalMasterId = ALog.BSCAppraisalMasterId and tblBSCAppraisalMasterAppLog.Version = CONVERT(INT,ALog.MaxVer) 
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmpApp ON ForEmpApp.EmpInfoId=tblBSCAppraisalMasterAppLog.ForEmpInfoId

where A.CompanyId =" + companyId + "  " + param + @"     union all  

SELECT DISTINCT   b.OptionInfo,CASE WHEN appFin.Other=1 THEN 'Promotion with Increment' +ISNULL(' [Note:'+appFin.Note+']','')   WHEN appFin.GeneralIncrement=1 THEN 'General Increment'   WHEN appFin.IsPromotion=1 THEN 'Promotion'   WHEN appFin.SpecialIncrement=1 THEN 'Special Increment'  WHEN appFin.Pip=1 THEN 'Performance Improvement Plan'   else '' END PromotionwithIncrement, sal.GradeCode+ISNULL(+' : '+sal.GradeName,0) GradeCode, FORMAT(e.DateOfJoin,'dd-MMM-yyy') DateOfJoin, ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0) TotalMarks, div.DivisionName, ISNULL(app.BSCAppraisalMasterId,0) BSCAppraisalMasterId,  ISNULL(SMAster.BSCAppraisalSelfMasterId,0) BSCAppraisalSelfMasterId, tblA.EmpName,e.EmpInfoId ,e.EmpMasterCode,  e.EmpName EmpllName, desg.Designation,
          ('Employee ID: '+ e.EmpMasterCode+', Employee Name: '+ +e.EmpName+ ISNULL(', Designation: '+desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove, (CASE WHEN SMAster.ActionStatus<>'Approved' then 'Not Approved' ELSE SMAster.ActionStatus END)AS ActionStatus, ForEmp.EmpMasterCode+' : '+ForEmp.EmpName as PendingEmp
,(CASE WHEN tblBSCAppraisalMasterAppLog.ActionStatus<>'Approved' then 'Not Approved' ELSE tblBSCAppraisalMasterAppLog.ActionStatus END)AS ActionStatusAppraisal,ForEmpApp.EmpMasterCode+' : '+ ForEmpApp.EmpName as PendingEmpApp
         FROM    dbo.tblBSCKpiDeadlineMaster A
        inner JOIN dbo.tblBSCKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
 INNER JOIN dbo.tblBSCAppraisalSelfMaster SMAster ON B.EmpinfoId = SMAster.EmpinfoId  AND SMAster.FinancialYearId = A.FinancialYearId

 	 inner JOIN   tblEmpAllRefference reff  ON b.EmpinfoId = reff.RefferenceEmpId 
		LEFT JOIN dbo.tblBSCAppraisalMaster app ON e.EmpInfoId = app.EmpInfoId AND SMAster.FinancialYearId = app.FinancialYearId 
LEFT JOIN ( SELECT  SUM(SupervisorMark) SupervisorMark ,
                            BSCAppraisalMasterId
                    FROM    tblBSCAppraisalFuncArea
                    GROUP BY BSCAppraisalMasterId
                  ) func ON app.BSCAppraisalMasterId = func.BSCAppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SupervisorScore) Score ,
                            BSCAppraisalMasterId
                    FROM    tblBSCAppraisalBehaveArea
                    GROUP BY BSCAppraisalMasterId
                  ) behave ON app.BSCAppraisalMasterId = behave.BSCAppraisalMasterId
				  left join tblSalaryGrade sal on sal.SalaryGradeId=e.SalaryGradeId
				   LEFT JOIN tblDivision div ON e.DivisionId = div.DivisionId
 LEFT JOIN dbo.tblBSCAppraisalFinalStatus appFin ON appFin.BSCAppraisalMasterId = app.BSCAppraisalMasterId
		   LEFT JOIN (SELECT TOP 1 BSCAppraisalSelfMasterId,tblEmpGeneralInfo.EmpName FROM tblBSCAppraisalSelfAppLog  
		   INNER JOIN dbo.tblEmpGeneralInfo ON  tblBSCAppraisalSelfAppLog.ForEmpInfoId=tblEmpGeneralInfo.EmpInfoId) tblA ON SMAster.BSCAppraisalSelfMasterId = tblA.BSCAppraisalSelfMasterId
LEFT  JOIN (SELECT BSCAppraisalSelfMasterId, MAX(Version)MaxVer FROM dbo.tblBSCAppraisalSelfAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY BSCAppraisalSelfMasterId) AS CELog ON CELog.BSCAppraisalSelfMasterId= SMAster.BSCAppraisalSelfMasterId
								LEFT  JOIN dbo.tblBSCAppraisalSelfAppLog ON tblBSCAppraisalSelfAppLog.BSCAppraisalSelfMasterId = SMAster.BSCAppraisalSelfMasterId   and tblBSCAppraisalSelfAppLog.Version = CONVERT(INT,CELog.MaxVer) 
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblBSCAppraisalSelfAppLog.ForEmpInfoId

								LEFT  JOIN (SELECT BSCAppraisalMasterId,MAX(Version)MaxVer FROM dbo.tblBSCAppraisalMasterAppLog WHERE ActionStatus NOT IN
								('Review')     GROUP BY BSCAppraisalMasterId) AS ALog ON ALog.BSCAppraisalMasterId= app.BSCAppraisalMasterId

								LEFT  JOIN dbo.tblBSCAppraisalMasterAppLog ON tblBSCAppraisalMasterAppLog.BSCAppraisalMasterId = ALog.BSCAppraisalMasterId and tblBSCAppraisalMasterAppLog.Version = CONVERT(INT,ALog.MaxVer) 
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmpApp ON ForEmpApp.EmpInfoId=tblBSCAppraisalMasterAppLog.ForEmpInfoId
								 	 inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 WHERE        reff.ShowCompany in (ComAssain)       " + param2 + " )HH";

               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }



       }

        
       public DataTable GetAppraisalByKpiPermission_New__SAP(string companyId, string param, string param2)
       {
           try
           {
               string query = @" SELECT DISTINCT  isnull(IsSapApproved,0) IsSapApproved,  case when ActionStatusAppraisal='Approved' then PromotionwithIncrement else '' end PromotionwithIncrement, case when ActionStatusAppraisal='Approved' then TotalMarks else '0' end TotalMarks, * FROM(SELECT DISTINCT  isnull(app.IsSapApproved,0) IsSapApproved,   CASE WHEN appFin.Other=1 THEN 'Promotion with Increment' +ISNULL(' [Note:'+appFin.Note+']','')  WHEN appFin.GeneralIncrement=1 THEN 'General Increment'   WHEN appFin.IsPromotion=1 THEN 'Promotion'   WHEN appFin.SpecialIncrement=1 THEN 'Special Increment'  WHEN appFin.Pip=1 THEN 'Performance Improvement Plan'   else ' ' END PromotionwithIncrement, sal.GradeCode+ISNULL(+' : '+sal.GradeName,0) GradeCode, FORMAT(e.DateOfJoin,'dd-MMM-yyy') DateOfJoin, ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0) TotalMarks, div.DivisionName,ISNULL(app.AppraisalMasterId,0) AppraisalMasterId,  ISNULL(SMAster.AppraisalSelfMasterId,0) AppraisalSelfMasterId, tblA.EmpName,e.EmpInfoId ,e.EmpMasterCode,  e.EmpName EmpllName, desg.Designation,
          ('Employee ID: '+ e.EmpMasterCode+', Employee Name: '+ +e.EmpName+ ISNULL(', Designation: '+desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove, (CASE WHEN SMAster.ActionStatus<>'Approved' then 'Not Approved' ELSE SMAster.ActionStatus END)AS ActionStatus,ForEmp.EmpMasterCode+' : '+ForEmp.EmpName as PendingEmp
,(CASE WHEN tblAppraisalMasterAppLog.ActionStatus<>'Approved' then 'Not Approved' ELSE tblAppraisalMasterAppLog.ActionStatus END)AS ActionStatusAppraisal,ForEmpApp.EmpMasterCode+' : '+ ForEmpApp.EmpName as PendingEmpApp
        FROM    dbo.tblKpiDeadlineMaster A
        LEFT JOIN dbo.tblKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
 INNER JOIN dbo.tblAppraisalSelfMaster SMAster ON B.EmpinfoId = SMAster.EmpinfoId  AND SMAster.FinancialYearId = A.FinancialYearId
		LEFT JOIN dbo.tblAppraisalMaster app ON e.EmpInfoId = app.EmpInfoId AND SMAster.FinancialYearId = app.FinancialYearId 
LEFT JOIN ( SELECT  SUM(SupervisorMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON app.AppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SupervisorScore) Score ,
                            AppraisalMasterId
                    FROM    tblAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON app.AppraisalMasterId = behave.AppraisalMasterId
				  left join tblSalaryGrade sal on sal.SalaryGradeId=e.SalaryGradeId
				   LEFT JOIN tblDivision div ON e.DivisionId = div.DivisionId
 LEFT JOIN dbo.tblAppraisalFinalStatus appFin ON appFin.AppraisalMasterId = app.AppraisalMasterId
		   LEFT JOIN (SELECT TOP 1 AppraisalSelfMasterId,tblEmpGeneralInfo.EmpName FROM tblAppraisalSelfAppLog  
		   INNER JOIN dbo.tblEmpGeneralInfo ON  tblAppraisalSelfAppLog.ForEmpInfoId=tblEmpGeneralInfo.EmpInfoId) tblA ON SMAster.AppraisalSelfMasterId = tblA.AppraisalSelfMasterId
LEFT  JOIN (SELECT AppraisalSelfMasterId, MAX(Version)MaxVer FROM dbo.tblAppraisalSelfAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalSelfMasterId) AS CELog ON CELog.AppraisalSelfMasterId= SMAster.AppraisalSelfMasterId
								LEFT  JOIN dbo.tblAppraisalSelfAppLog ON tblAppraisalSelfAppLog.AppraisalSelfMasterId = SMAster.AppraisalSelfMasterId   and tblAppraisalSelfAppLog.Version = CONVERT(INT,CELog.MaxVer) 
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblAppraisalSelfAppLog.ForEmpInfoId

								LEFT  JOIN (SELECT AppraisalMasterId,MAX(Version)MaxVer FROM dbo.tblAppraisalMasterAppLog WHERE ActionStatus NOT IN
								('Review')     GROUP BY AppraisalMasterId) AS ALog ON ALog.AppraisalMasterId= app.AppraisalMasterId

								LEFT  JOIN dbo.tblAppraisalMasterAppLog ON tblAppraisalMasterAppLog.AppraisalMasterId = ALog.AppraisalMasterId and tblAppraisalMasterAppLog.Version = CONVERT(INT,ALog.MaxVer) 
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmpApp ON ForEmpApp.EmpInfoId=tblAppraisalMasterAppLog.ForEmpInfoId

where A.CompanyId = " + companyId + "  " + param + @"    union all  

SELECT DISTINCT  isnull(IsSapApproved,0) IsSapApproved,CASE WHEN appFin.Other=1 THEN 'Promotion with Increment' +ISNULL(' [Note:'+appFin.Note+']','')   WHEN appFin.GeneralIncrement=1 THEN 'General Increment'   WHEN appFin.IsPromotion=1 THEN 'Promotion'   WHEN appFin.SpecialIncrement=1 THEN 'Special Increment'  WHEN appFin.Pip=1 THEN 'Performance Improvement Plan'   else '' END PromotionwithIncrement, sal.GradeCode+ISNULL(+' : '+sal.GradeName,0) GradeCode, FORMAT(e.DateOfJoin,'dd-MMM-yyy') DateOfJoin, ISNULL(func.SupervisorMark,0)+ISNULL(behave.Score,0) TotalMarks, div.DivisionName, ISNULL(app.AppraisalMasterId,0) AppraisalMasterId,  ISNULL(SMAster.AppraisalSelfMasterId,0) AppraisalSelfMasterId, tblA.EmpName,e.EmpInfoId ,e.EmpMasterCode,  e.EmpName EmpllName, desg.Designation,
          ('Employee ID: '+ e.EmpMasterCode+', Employee Name: '+ +e.EmpName+ ISNULL(', Designation: '+desg.Designation,'')) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove, (CASE WHEN SMAster.ActionStatus<>'Approved' then 'Not Approved' ELSE SMAster.ActionStatus END)AS ActionStatus, ForEmp.EmpMasterCode+' : '+ForEmp.EmpName as PendingEmp
,(CASE WHEN tblAppraisalMasterAppLog.ActionStatus<>'Approved' then 'Not Approved' ELSE tblAppraisalMasterAppLog.ActionStatus END)AS ActionStatusAppraisal,ForEmpApp.EmpMasterCode+' : '+ ForEmpApp.EmpName as PendingEmpApp
        FROM    dbo.tblKpiDeadlineMaster A
        LEFT JOIN dbo.tblKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
 INNER JOIN dbo.tblAppraisalSelfMaster SMAster ON B.EmpinfoId = SMAster.EmpinfoId  AND SMAster.FinancialYearId = A.FinancialYearId

 	 inner JOIN   tblEmpAllRefference reff  ON b.EmpinfoId = reff.RefferenceEmpId 
		LEFT JOIN dbo.tblAppraisalMaster app ON e.EmpInfoId = app.EmpInfoId AND SMAster.FinancialYearId = app.FinancialYearId 
LEFT JOIN ( SELECT  SUM(SupervisorMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON app.AppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SupervisorScore) Score ,
                            AppraisalMasterId
                    FROM    tblAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON app.AppraisalMasterId = behave.AppraisalMasterId
				  left join tblSalaryGrade sal on sal.SalaryGradeId=e.SalaryGradeId
				   LEFT JOIN tblDivision div ON e.DivisionId = div.DivisionId
 LEFT JOIN dbo.tblAppraisalFinalStatus appFin ON appFin.AppraisalMasterId = app.AppraisalMasterId
		   LEFT JOIN (SELECT TOP 1 AppraisalSelfMasterId,tblEmpGeneralInfo.EmpName FROM tblAppraisalSelfAppLog  
		   INNER JOIN dbo.tblEmpGeneralInfo ON  tblAppraisalSelfAppLog.ForEmpInfoId=tblEmpGeneralInfo.EmpInfoId) tblA ON SMAster.AppraisalSelfMasterId = tblA.AppraisalSelfMasterId
LEFT  JOIN (SELECT AppraisalSelfMasterId, MAX(Version)MaxVer FROM dbo.tblAppraisalSelfAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalSelfMasterId) AS CELog ON CELog.AppraisalSelfMasterId= SMAster.AppraisalSelfMasterId
								LEFT  JOIN dbo.tblAppraisalSelfAppLog ON tblAppraisalSelfAppLog.AppraisalSelfMasterId = SMAster.AppraisalSelfMasterId   and tblAppraisalSelfAppLog.Version = CONVERT(INT,CELog.MaxVer) 
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblAppraisalSelfAppLog.ForEmpInfoId

								LEFT  JOIN (SELECT AppraisalMasterId,MAX(Version)MaxVer FROM dbo.tblAppraisalMasterAppLog WHERE ActionStatus NOT IN
								('Review')     GROUP BY AppraisalMasterId) AS ALog ON ALog.AppraisalMasterId= app.AppraisalMasterId

								LEFT  JOIN dbo.tblAppraisalMasterAppLog ON tblAppraisalMasterAppLog.AppraisalMasterId = ALog.AppraisalMasterId and tblAppraisalMasterAppLog.Version = CONVERT(INT,ALog.MaxVer) 
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmpApp ON ForEmpApp.EmpInfoId=tblAppraisalMasterAppLog.ForEmpInfoId
								 	 inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 WHERE        reff.ShowCompany in (ComAssain)   " + param2 + " )HH  where ActionStatusAppraisal='approved'";

               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }



       }



       public DataTable GetSkillWillFinalReport(string companyId, string param)
       {
           try
           {
               string query = @"SELECT case when  M.ActionStatus='Review' then 'Returned' when  M.ActionStatus='Verified' then 'Submitted' else M.ActionStatus end ActionStatus , EMP.EmpMasterCode, EMP.EmpName , dgs.Designation,CAST( CAST(sum(D.SKILL) AS DECIMAL(10,2))/COUNT(D.SKILL)AS DECIMAL(10,2)) SKILL,   CAST( CAST(sum(D.WILL) AS DECIMAL(10,2))/COUNT(D.WILL)AS DECIMAL(10,2)) WILL, case when CAST( CAST(sum(D.SKILL) AS DECIMAL(10,2))/COUNT(D.SKILL)AS DECIMAL(10,2)) >=  4   then 'HIGH' else 'LOW' end SKILLText , case when CAST( CAST(sum(D.WILL) AS DECIMAL(10,2))/COUNT(D.WILL)AS DECIMAL(10,2)) >= 4 then 'HIGH' else 'LOW' end WILLText FROM tblEmpSkillWillAssessmentDetails D
LEFT JOIN  tblEmpSkillWillAssessmentMaster M ON D.EmpSkillWillMasterId = M.EmpSkillWillMasterId
LEFT JOIN tblEmpGeneralInfo EMP ON M.EmpInfoId = EMP.EmpInfoId
LEFT JOIN tblDesignation dgs ON dgs.DesignationId = EMP.DesignationId
WHERE M.EmpSkillWillMasterId IS NOT NULL and emp.CompanyId = " + companyId + "  " + param + " group by M.ActionStatus, EMP.EmpMasterCode, EMP.EmpName , dgs.Designation"




 ;

               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }



       }

       public DataTable GetAppraisalRecommendationReport(string param, string param2)
       {
           try
           {
               string query = @"
UPDATE tblAppraisalFinalStatus SET DocumentLink=NULL WHERE DocumentLink=''

select distinct * from (
SELECT DP.DepartmentName DepartmentName, DS.Designation,  EG.EmpName EmpName, d.FinancialYearDesc, appMaster.FinancialYearId, appMaster.EmpInfoId ,appMaster.AppraisalMasterId, appMaster.AppraisalSelfMasterId, appFin.Justification,  CASE WHEN appFin.DocumentLink IS not  NULL THEN 'btn btn-sm btn-success' ELSE 'alert-warning' END  DocumentLinkStyle, CASE WHEN appFin.DocumentLink IS not  NULL THEN 'Download' ELSE 'No Document Found' END  DocumentLinkText, ISNULL( '../UploadMeetingDocument/'+appFin.DocumentLink, '') DocumentLink, EG.EmpMasterCode, 
    
         CASE WHEN appFin.GeneralIncrement=1 THEN 'General Increment'   WHEN appFin.SpecialIncrement=1 THEN 'Special Increment'   WHEN appFin.IsPromotion=1 THEN 'Promotion'  WHEN appFin.Pip=1 THEN 'Performance Improvement Plan'   WHEN appFin.Other=1 THEN 'Promotion with Increment' WHEN appFin.IsPromotion=1 AND  appFin.GeneralIncrement=1 THEN 'Promotion with Increment' END PromotionwithIncrement, rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate  
  from dbo.tblAppraisalDeadlineMaster A  WITH (NOLOCK) 
   left join (SELECT AppraisalDeadLineMasterId, EmpinfoId from tblAppraisalDeadLineDetails group BY AppraisalDeadLineMasterId, EmpinfoId) B on a.AppraisalDeadLineMasterId = b.AppraisalDeadLineMasterId  
  



   --  left join (SELECT tblAppraisalMaster.EmpInfoId,tblAppraisalMaster.AppraisalSelfMasterId, SUM(ISNULL(aa.SupervisorScore,0))SupervisorScore FROM tblAppraisalMaster
   -- left join dbo.tblAppraisalBehaveArea aa on aa.AppraisalSelfMasterId=tblAppraisalMaster.AppraisalSelfMasterId group by tblAppraisalMaster.EmpInfoId,tblAppraisalMaster.AppraisalSelfMasterId
   --)  mdbehabe on mdtMarks.EmpInfoId = b.EmpinfoId 
  
                                     LEFT JOIN dbo.tblEmpGeneralInfo EG ON  B.EmpinfoId = EG.EmpInfoId
                                     LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId
                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 
                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 
								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 
                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana
                                left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId
LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
   LEFT JOIN tblAppraisalMaster appMaster ON appMaster.EmpInfoId = EG.EmpInfoId  and appMaster.FinancialYearId=d.FinancialYearId
   

  
 inner JOIN dbo.tblAppraisalFinalStatus appFin ON appFin.AppraisalMasterId = appMaster.AppraisalMasterId

  LEFT JOIN dbo.tblEmployeeType empType ON empType.EmpTypeId = EG. EmpTypeId
    where (a.IsDelete is null or a.IsDelete = 0) and     B.EmpinfoId    IN (SELECT tblAppraisalMaster.EmpInfoId FROM tblAppraisalMaster WHERE tblAppraisalMaster.SelfApprove='Approved'  AND     tblAppraisalMaster.CurrentStatus='Approved' )  AND appFin.GeneralIncrement NOT IN(1)    " + param + @" 	union all 


	SELECT DP.DepartmentName DepartmentName, DS.Designation,  EG.EmpName EmpName, d.FinancialYearDesc, appMaster.FinancialYearId, appMaster.EmpInfoId ,appMaster.AppraisalMasterId, appMaster.AppraisalSelfMasterId, appFin.Justification,  CASE WHEN appFin.DocumentLink IS not  NULL THEN 'btn btn-sm btn-success' ELSE 'alert-warning' END  DocumentLinkStyle, CASE WHEN appFin.DocumentLink IS not  NULL THEN 'Download' ELSE 'No Document Found' END  DocumentLinkText, ISNULL( '../UploadMeetingDocument/'+appFin.DocumentLink, '') DocumentLink, EG.EmpMasterCode, 
    
         CASE WHEN appFin.GeneralIncrement=1 THEN 'General Increment'   WHEN appFin.SpecialIncrement=1 THEN 'Special Increment'   WHEN appFin.IsPromotion=1 THEN 'Promotion'  WHEN appFin.Pip=1 THEN 'Performance Improvement Plan'   WHEN appFin.Other=1 THEN 'Promotion with Increment' WHEN appFin.IsPromotion=1 AND  appFin.GeneralIncrement=1 THEN 'Promotion with Increment' END PromotionwithIncrement, rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate  
  from dbo.tblAppraisalDeadlineMaster A  WITH (NOLOCK) 
   left join (SELECT AppraisalDeadLineMasterId, EmpinfoId from tblAppraisalDeadLineDetails group BY AppraisalDeadLineMasterId, EmpinfoId) B on a.AppraisalDeadLineMasterId = b.AppraisalDeadLineMasterId  
  



   --  left join (SELECT tblAppraisalMaster.EmpInfoId,tblAppraisalMaster.AppraisalSelfMasterId, SUM(ISNULL(aa.SupervisorScore,0))SupervisorScore FROM tblAppraisalMaster
   -- left join dbo.tblAppraisalBehaveArea aa on aa.AppraisalSelfMasterId=tblAppraisalMaster.AppraisalSelfMasterId group by tblAppraisalMaster.EmpInfoId,tblAppraisalMaster.AppraisalSelfMasterId
   --)  mdbehabe on mdtMarks.EmpInfoId = b.EmpinfoId 
  
                                     LEFT JOIN dbo.tblEmpGeneralInfo EG ON  B.EmpinfoId = EG.EmpInfoId
                                     LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId
                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 
                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 
								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 
                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana
                                left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId
LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
   LEFT JOIN tblAppraisalMaster appMaster ON appMaster.EmpInfoId = EG.EmpInfoId  and appMaster.FinancialYearId=d.FinancialYearId
   

  
 inner JOIN dbo.tblAppraisalFinalStatus appFin ON appFin.AppraisalMasterId = appMaster.AppraisalMasterId

  LEFT JOIN dbo.tblEmployeeType empType ON empType.EmpTypeId = EG. EmpTypeId


   inner JOIN   tblEmpAllRefference reff  ON EG.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 


    where (a.IsDelete is null or a.IsDelete = 0) and     B.EmpinfoId    IN (SELECT tblAppraisalMaster.EmpInfoId FROM tblAppraisalMaster WHERE tblAppraisalMaster.SelfApprove='Approved'  AND     tblAppraisalMaster.CurrentStatus='Approved' )  AND appFin.GeneralIncrement NOT IN(1)   and  EG.IsActive=1  and     reff.ShowCompany in (ComAssain) " + param2 + "  ) tbl   order by EmpMasterCode asc  ";

               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }



       }
       public DataTable GetDeadlineApproval(string param, string param2)
       {
           try
           {
               string query = @"select distinct * from ( SELECT ( 'Employee ID: '+ e.EmpMasterCode + ', Employee Name: ' + e.EmpName +  ISNULL(', Designation: ' +desg.Designation,'') ) employee,  FORMAT(b.ExtensionDate,'dd-MMM-yyyy') ExtensionDate, a.Description, a.Remarks  , e.EmpInfoId, A.FinYearId FinancialYearId, y.FinancialYearDesc, dpt.DepartmentName , b.DeadlineExtensionRequestDetailsId, e.CompanyId, A.FinYearId, Operation  FROM dbo.tblDeadlineExtensionRequestDetails b
LEFT JOIN dbo.tblDeadlineExtensionRequest A ON A.DeadlineExtensionRequestId = b.DeadlineExtensionRequestId
LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmployeeId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId WHERE b.ApprovalStatus='Posted'  " + param + @" union all 

  SELECT ( 'Employee ID: '+ e.EmpMasterCode + ', Employee Name: ' + e.EmpName +  ISNULL(', Designation: ' +desg.Designation,'') ) employee,  FORMAT(b.ExtensionDate,'dd-MMM-yyyy') ExtensionDate, a.Description, a.Remarks , e.EmpInfoId, A.FinYearId FinancialYearId, y.FinancialYearDesc, dpt.DepartmentName, b.DeadlineExtensionRequestDetailsId, e.CompanyId, A.FinYearId, Operation FROM dbo.tblDeadlineExtensionRequestDetails b
LEFT JOIN dbo.tblDeadlineExtensionRequest A ON A.DeadlineExtensionRequestId = b.DeadlineExtensionRequestId
LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmployeeId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
		 inner JOIN   tblEmpAllRefference reff  ON e.EmpInfoId = reff.RefferenceEmpId      
   inner join (select   NewEmployeeId,OnlyViewComId from tblEmpSpecialTransfer where OnlyView=1) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 WHERE  EmpMasterCode IS NOT  NULL and b.ApprovalStatus='Posted' " + param2+ ") tbl";

               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }



       }
       public DataTable GetEmployeeForKpiSetUpNew(string companyId,  string param)
       {
           try
           {
               string query = @"select A.EmpInfoId, A.EmpMasterCode , A.EmpName , desg.Designation ,dpt.DepartmentName , div.DivisionName    from tblEmpGeneralInfo A " +
                              "left join tblDivision div on a.DivisionId = div.DivisionId " +
                              "left join tblDepartment dpt on a.DepartmentId = dpt.DepartmentId " +
                              "left join tblDesignation desg on a.DesignationId = desg.DesignationId where A.CompanyId = " + companyId + " and a.IsActive=1 " + param + "";
               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
    }
}
