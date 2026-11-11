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
    public class KPIMIDAppraislDashboardDAL
    {

        ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        DataAccessManager _aAcessManager = new DataAccessManager();

        public DataTable LoadSectionDDl(int dptId)
        {
            try
            {
                string query = @"Select * from tblSection where DepartmentId = " + dptId + "";
                return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable LoadSubsection(int sectionId)
        {
            try
            {
                string query = @"Select * from tblSubSection wwhere SectionId = " + sectionId + "";
                return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetAppraisalDashboard(int com)
        {
            try
            {
                //_aAcessManager.SqlConnectionOpen(DataBase.HRDB);
                List<SqlParameter> aPeram = new List<SqlParameter>();

                aPeram.Add(new SqlParameter("@company", com));

                //DataSet ds = _aAcessManager.GetDataSet("sp_Get_AppraisalDahsboard" , aPeram);
                //return ds.Tables[0];
                DataTable aTable = _aCommonInternalDal.GetDataByStoreProcedure("sp_Get_AppraisalDahsboard", aPeram,
                    DataBase.HRDB);
                return aTable;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataTable GetAllComments(string masterId)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblKPIMIDAppraisalMasterAppLog
                LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblKPIMIDAppraisalMasterAppLog.CommentEmpId WHERE tblKPIMIDAppraisalMasterAppLog.ActionStatus<>'Drafted'
                AND AppraisalMasterId='"+masterId+"'";
                return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

//        public DataTable GetAppraisalDashboardOwn(int emp)
//        {
//            try
//            {
//                //_aAcessManager.SqlConnectionOpen(DataBase.HRDB);


//                string query = @"SELECT  
//	emp.EmpInfoId,
//	emp.EmpMasterCode ,
//	aax.AppraisalSelfMasterId,
//        emp.EmpName  ,
//        CONVERT(nvarchar(11),emp.DateOfJoin,106)DateOfJoin ,
//        dpt.DepartmentName ,
//        desg.Designation ,
//		 ISNULL(appMaster.FinancialYearId,0)FinancialYearId ,
//		case when appMaster.AppraisalMasterId is null then 0 else appMaster.AppraisalMasterId  end as AppraisalMasterId,
//        case when func.SupervisorMark is null or func.SupervisorMark=0 then
//		'Not Complete'
//		else CONVERT(nvarchar(50), func.SupervisorMark) end as PartA,
//        --func.SupervisorMark,
//		case when behave.Score is null then 'Not Complete' else CONVERT(nvarchar(50),behave.Score) end as PartB,
//		 case when finalStatus.FinalStatus is null then 'Not Complete' else convert(nvarchar(50),finalStatus.FinalStatus) end as PartC,
//		case when training.traininingCount is null then 'Not Complete' else 'Complete' end as training
//
//FROM tblKPIMIDAppraisalMaster aax    
//
//      left join   tblEmpGeneralInfo emp on  aax.EmpInfoId= emp.EmpInfoId    
//		
//        LEFT JOIN tblSubSection subSec ON emp.SubSectionId = subSec.SubSectionId
//        LEFT JOIN tblSection sec ON emp.SectionId = sec.SectionId
//        LEFT JOIN tblDepartment dpt ON emp.DepartmentId = dpt.DepartmentId
//        LEFT JOIN tblDivisionWing wing ON emp.DivisionWId = wing.DivisionWId
//        LEFT JOIN tblDivision div ON emp.DivisionId = div.DivisionId
//        LEFT JOIN tblCompanyInfo comp ON emp.CompanyId = comp.CompanyId
//                                         AND div.CompanyId = comp.CompanyId
//        LEFT JOIN tblDesignation desg ON emp.DesignationId = desg.DesignationId
//        LEFT JOIN tblKPIMIDAppraisalMaster appMaster ON appMaster.EmpInfoId = emp.EmpInfoId 
//        LEFT JOIN ( SELECT  SUM(SelfMark) SupervisorMark ,
//                            AppraisalMasterId
//                    FROM    tblKPIMIDAppraisalFuncArea
//                    GROUP BY AppraisalMasterId
//                  ) func ON appMaster.AppraisalMasterId = func.AppraisalMasterId
//        LEFT JOIN ( SELECT  SUM(SelfScore) Score ,
//                            AppraisalMasterId
//                    FROM    tblKPIMIDAppraisalBehaveArea
//                    GROUP BY AppraisalMasterId
//                  ) behave ON appMaster.AppraisalMasterId = behave.AppraisalMasterId
//        LEFT JOIN tblAppraisalFinalStatus finalStatus ON appMaster.AppraisalMasterId = finalStatus.AppraisalMasterId
//        LEFT JOIN ( SELECT  COUNT(AppraisalTrainingId) traininingCount ,
//                            AppraisalMasterId
//                    FROM    tblAppraisalTrainingNeeds
//                    GROUP BY AppraisalMasterId
//                  ) training ON appMaster.AppraisalMasterId = training.AppraisalMasterId
//
//				  where (appMaster.SelfApprove = 'Posted' OR  appMaster.SelfApprove IS NULL ) and emp.EmpInfoId = " + emp + "  and emp.empinfoId in " +
//                               "(SELECT dd.EmpinfoId   FROM dbo.tblAppraisalDeadlineMaster dM  INNER JOIN dbo.tblAppraisalDeadLineDetails dd ON" +
//                               " dm.AppraisalDeadLineMasterId = dd.AppraisalDeadLineMasterId WHERE  CONVERT(DATE , GETDATE()) <= dd.DeadLine  or   " +
//                               "dd.ExtensionDate >= CASE WHEN dd.ExtensionDate IS NULL THEN NULL ELSE CONVERT(date,GETDATE()) END ) ";

//                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);


//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }

//        }
        public DataTable GetAppraisalDashboardOwn(int emp, int FinancialYear)
        {
            try
            {
                //_aAcessManager.SqlConnectionOpen(DataBase.HRDB);


                string query = @"SELECT  aax.IsMidYearStatus,
	emp.EmpInfoId,
	emp.EmpMasterCode ,
	aax.AppraisalSelfMasterId,
        emp.EmpName  ,
        CONVERT(nvarchar(11),emp.DateOfJoin,106)DateOfJoin ,
        dpt.DepartmentName ,
        desg.Designation ,
		 ISNULL(appMaster.FinancialYearId,0)FinancialYearId ,
		case when appMaster.AppraisalMasterId is null then 0 else appMaster.AppraisalMasterId  end as AppraisalMasterId,
        case when func.SupervisorMark is null or func.SupervisorMark=0 then
		'Not Complete'
		else CONVERT(nvarchar(50), func.SupervisorMark) end as PartA,
        --func.SupervisorMark,
		case when behave.Score is null then 'Not Complete' else CONVERT(nvarchar(50),behave.Score) end as PartB,
		 case when finalStatus.FinalStatus is null then 'Not Complete' else convert(nvarchar(50),finalStatus.FinalStatus) end as PartC,
		case when training.traininingCount is null then 'Not Complete' else 'Complete' end as training

FROM tblKPIMIDAppraisalMaster aax    

      left join   tblEmpGeneralInfo emp on  aax.EmpInfoId= emp.EmpInfoId    
		
        LEFT JOIN tblSubSection subSec ON emp.SubSectionId = subSec.SubSectionId
        LEFT JOIN tblSection sec ON emp.SectionId = sec.SectionId
        LEFT JOIN tblDepartment dpt ON emp.DepartmentId = dpt.DepartmentId
        LEFT JOIN tblDivisionWing wing ON emp.DivisionWId = wing.DivisionWId
        LEFT JOIN tblDivision div ON emp.DivisionId = div.DivisionId
        LEFT JOIN tblCompanyInfo comp ON emp.CompanyId = comp.CompanyId
                                         AND div.CompanyId = comp.CompanyId
        LEFT JOIN tblDesignation desg ON emp.DesignationId = desg.DesignationId
        LEFT JOIN tblKPIMIDAppraisalMaster appMaster ON appMaster.EmpInfoId = emp.EmpInfoId  and appMaster.FinancialYearId=aax.FinancialYearId
        LEFT JOIN ( SELECT  SUM(SelfMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON appMaster.AppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SelfScore) Score ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON appMaster.AppraisalMasterId = behave.AppraisalMasterId
        LEFT JOIN tblAppraisalFinalStatus finalStatus ON appMaster.AppraisalMasterId = finalStatus.AppraisalMasterId
        LEFT JOIN ( SELECT  COUNT(AppraisalTrainingId) traininingCount ,
                            AppraisalMasterId
                    FROM    tblAppraisalTrainingNeeds
                    GROUP BY AppraisalMasterId
                  ) training ON appMaster.AppraisalMasterId = training.AppraisalMasterId
                LEFT JOIN (SELECT AppraisalMasterId,MAX(Version)MaxVer FROM dbo.tblKPIMIDAppraisalMasterAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalMasterId) AS CELog ON CELog.AppraisalMasterId= appMaster.AppraisalMasterId
								LEFT JOIN dbo.tblKPIMIDAppraisalMasterAppLog ON tblKPIMIDAppraisalMasterAppLog.AppraisalMasterId = appMaster.AppraisalMasterId


				  where  emp.EmpInfoId = " + emp + " and aax.FinancialYearId=" + FinancialYear + "  and emp.empinfoId in " +
                               "(SELECT dd.EmpinfoId   FROM dbo.tblAppraisalDeadlineMaster dM  INNER JOIN dbo.tblAppraisalDeadLineDetails dd ON" +
                               " dm.AppraisalDeadLineMasterId = dd.AppraisalDeadLineMasterId WHERE  CONVERT(DATE , GETDATE()) <= dd.DeadLine  or   " +
                               "dd.ExtensionDate >= CASE WHEN dd.ExtensionDate IS NULL THEN NULL ELSE CONVERT(date,GETDATE()) END ) AND  (Version=CELog.MaxVer OR Version IS NULL)   ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public DataTable GetAppraisalDashboardOwn333Dash(int emp,  string Parm)
        {
            try
            {
                //_aAcessManager.SqlConnectionOpen(DataBase.HRDB);


                string query = @"SELECT  aax.IsMidYearStatus,
	emp.EmpInfoId,
	emp.EmpMasterCode ,
	aax.AppraisalSelfMasterId,
        emp.EmpName  ,
        CONVERT(nvarchar(11),emp.DateOfJoin,106)DateOfJoin ,
        dpt.DepartmentName ,
        desg.Designation ,
		 ISNULL(appMaster.FinancialYearId,0)FinancialYearId ,
		case when appMaster.AppraisalMasterId is null then 0 else appMaster.AppraisalMasterId  end as AppraisalMasterId,
        case when func.SupervisorMark is null or func.SupervisorMark=0 then
		'Not Complete'
		else CONVERT(nvarchar(50), func.SupervisorMark) end as PartA,
        --func.SupervisorMark,
		case when behave.Score is null then 'Not Complete' else CONVERT(nvarchar(50),behave.Score) end as PartB,
		 case when finalStatus.FinalStatus is null then 'Not Complete' else convert(nvarchar(50),finalStatus.FinalStatus) end as PartC,
		case when training.traininingCount is null then 'Not Complete' else 'Complete' end as training
		,(CASE WHEN tblKPIMIDAppraisalMasterAppLog.ActionStatus<>'Approved' then 'Not Approved' ELSE tblKPIMIDAppraisalMasterAppLog.ActionStatus END)AS ActionStatusAppraisal,ForEmpApp.EmpName as PendingEmpApp
FROM tblKPIMIDAppraisalMaster aax    

      left join   tblEmpGeneralInfo emp on  aax.EmpInfoId= emp.EmpInfoId    
		
        LEFT JOIN tblSubSection subSec ON emp.SubSectionId = subSec.SubSectionId
        LEFT JOIN tblSection sec ON emp.SectionId = sec.SectionId
        LEFT JOIN tblDepartment dpt ON emp.DepartmentId = dpt.DepartmentId
        LEFT JOIN tblDivisionWing wing ON emp.DivisionWId = wing.DivisionWId
        LEFT JOIN tblDivision div ON emp.DivisionId = div.DivisionId
        LEFT JOIN tblCompanyInfo comp ON emp.CompanyId = comp.CompanyId
                                         AND div.CompanyId = comp.CompanyId
        LEFT JOIN tblDesignation desg ON emp.DesignationId = desg.DesignationId
        LEFT JOIN tblKPIMIDAppraisalMaster appMaster ON appMaster.EmpInfoId = emp.EmpInfoId  and appMaster.FinancialYearId=aax.FinancialYearId
        LEFT JOIN ( SELECT  SUM(SelfMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON appMaster.AppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SelfScore) Score ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON appMaster.AppraisalMasterId = behave.AppraisalMasterId
        LEFT JOIN tblAppraisalFinalStatus finalStatus ON appMaster.AppraisalMasterId = finalStatus.AppraisalMasterId
        LEFT JOIN ( SELECT  COUNT(AppraisalTrainingId) traininingCount ,
                            AppraisalMasterId
                    FROM    tblAppraisalTrainingNeeds
                    GROUP BY AppraisalMasterId
                  ) training ON appMaster.AppraisalMasterId = training.AppraisalMasterId
                LEFT JOIN (SELECT AppraisalMasterId,MAX(Version)MaxVer FROM dbo.tblKPIMIDAppraisalMasterAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalMasterId) AS CELog ON CELog.AppraisalMasterId= appMaster.AppraisalMasterId
								LEFT JOIN dbo.tblKPIMIDAppraisalMasterAppLog ON tblKPIMIDAppraisalMasterAppLog.AppraisalMasterId = appMaster.AppraisalMasterId
									LEFT JOIN dbo.tblEmpGeneralInfo ForEmpApp ON ForEmpApp.EmpInfoId=dbo.tblKPIMIDAppraisalMasterAppLog.ForEmpInfoId


				  where  emp.EmpInfoId = " + emp +  "  "+ Parm + "  and emp.empinfoId in " +
                               "(SELECT dd.EmpinfoId   FROM dbo.tblAppraisalDeadlineMaster dM  INNER JOIN dbo.tblAppraisalDeadLineDetails dd ON" +
                               " dm.AppraisalDeadLineMasterId = dd.AppraisalDeadLineMasterId WHERE  CONVERT(DATE , GETDATE()) <= dd.DeadLine  or   " +
                               "dd.ExtensionDate >= CASE WHEN dd.ExtensionDate IS NULL THEN NULL ELSE CONVERT(date,GETDATE()) END ) AND  (Version=CELog.MaxVer OR Version IS NULL)   ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataTable GetAppraisalDashboardOwn333DashOnly(int emp, string Parm)
        {
            try
            {
                //_aAcessManager.SqlConnectionOpen(DataBase.HRDB);


                string query = @"SELECT top 1  aax.IsMidYearStatus,
	emp.EmpInfoId,
	emp.EmpMasterCode ,
	aax.BSCAppraisalSelfMasterId AppraisalSelfMasterId,
        emp.EmpName  ,
        CONVERT(nvarchar(11),emp.DateOfJoin,106)DateOfJoin ,
        dpt.DepartmentName ,
        desg.Designation ,
		 ISNULL(appMaster.FinancialYearId,0)FinancialYearId ,
		case when appMaster.BSCAppraisalMasterId is null then 0 else appMaster.BSCAppraisalMasterId  end as AppraisalMasterId,
        case when func.SupervisorMark is null or func.SupervisorMark=0 then
		'Not Complete'
		else CONVERT(nvarchar(50), func.SupervisorMark) end as PartA,
        --func.SupervisorMark,
		case when behave.Score is null then 'Not Complete' else CONVERT(nvarchar(50),behave.Score) end as PartB,
		 case when finalStatus.FinalStatus is null then 'Not Complete' else convert(nvarchar(50),finalStatus.FinalStatus) end as PartC,
		case when training.traininingCount is null then 'Not Complete' else 'Complete' end as training
		,(CASE WHEN tblBSCAppraisalMasterAppLog.ActionStatus<>'Approved' then 'Not Approved' ELSE tblBSCAppraisalMasterAppLog.ActionStatus END)AS ActionStatusAppraisal,ForEmpApp.EmpName as PendingEmpApp
FROM tblBSCAppraisalSelfMaster aax    

      left join   tblEmpGeneralInfo emp on  aax.EmpInfoId= emp.EmpInfoId    
		
        LEFT JOIN tblSubSection subSec ON emp.SubSectionId = subSec.SubSectionId
        LEFT JOIN tblSection sec ON emp.SectionId = sec.SectionId
        LEFT JOIN tblDepartment dpt ON emp.DepartmentId = dpt.DepartmentId
        LEFT JOIN tblDivisionWing wing ON emp.DivisionWId = wing.DivisionWId
        LEFT JOIN tblDivision div ON emp.DivisionId = div.DivisionId
        LEFT JOIN tblCompanyInfo comp ON emp.CompanyId = comp.CompanyId
                                         AND div.CompanyId = comp.CompanyId
        LEFT JOIN tblDesignation desg ON emp.DesignationId = desg.DesignationId
        LEFT JOIN tblBSCAppraisalMaster appMaster ON appMaster.EmpInfoId = emp.EmpInfoId   and appMaster.FYDes_BSCApp=aax.FYDes_BSCSelf
        LEFT JOIN ( SELECT  SUM(SelfMark) SupervisorMark ,
                            BSCAppraisalMasterId AppraisalMasterId
                    FROM    tblBSCAppraisalFuncArea
                    GROUP BY BSCAppraisalMasterId
                  ) func ON appMaster.BSCAppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SelfScore) Score ,
                           BSCAppraisalMasterId AppraisalMasterId
                    FROM    tblBSCAppraisalBehaveArea
                    GROUP BY BSCAppraisalMasterId
                  ) behave ON appMaster.BSCAppraisalMasterId = behave.AppraisalMasterId
        LEFT JOIN tblBSCAppraisalFinalStatus finalStatus ON appMaster.BSCAppraisalMasterId = finalStatus.BSCAppraisalMasterId
        LEFT JOIN ( SELECT  COUNT(BSCAppraisalTrainingId) traininingCount ,
                         BSCAppraisalMasterId   AppraisalMasterId
                    FROM    tblBSCAppraisalTrainingNeeds
                    GROUP BY BSCAppraisalMasterId
                  ) training ON appMaster.BSCAppraisalMasterId = training.AppraisalMasterId
                LEFT JOIN (SELECT BSCAppraisalMasterId,MAX(Version)MaxVer FROM dbo.tblBSCAppraisalMasterAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY BSCAppraisalMasterId) AS CELog ON CELog.BSCAppraisalMasterId= appMaster.BSCAppraisalMasterId
								LEFT JOIN dbo.tblBSCAppraisalMasterAppLog ON tblBSCAppraisalMasterAppLog.BSCAppraisalMasterId = appMaster.BSCAppraisalMasterId
									LEFT JOIN dbo.tblEmpGeneralInfo ForEmpApp ON ForEmpApp.EmpInfoId=dbo.tblBSCAppraisalMasterAppLog.ForEmpInfoId

				  where emp.EmpInfoId = " + emp + "  " + Parm + "  and emp.empinfoId in " +
                               "(SELECT dd.EmpinfoId   FROM dbo.tblBSCAppraisalDeadlineMaster dM  INNER JOIN dbo.tblBSCAppraisalDeadLineDetails dd ON" +
                               " dm.BSCAppraisalDeadLineMasterId = dd.BSCAppraisalDeadLineMasterId WHERE  CONVERT(DATE , GETDATE()) <= dd.DeadLine  or   " +
                               "dd.ExtensionDate >= CASE WHEN dd.ExtensionDate IS NULL THEN NULL ELSE CONVERT(date,GETDATE()) END ) AND  (Version=CELog.MaxVer OR Version IS NULL)   	  order by appMaster.FinancialYearId desc ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public DataTable GetAppraisalDashboardOwn333DashNew_(int emp, string Parm)
        {
            try
            {
                //_aAcessManager.SqlConnectionOpen(DataBase.HRDB);


                string query = @"SELECT  aax.IsMidYearStatus,
	emp.EmpInfoId,
	emp.EmpMasterCode ,
	aax.AppraisalSelfMasterId,
        emp.EmpName  ,
        CONVERT(nvarchar(11),emp.DateOfJoin,106)DateOfJoin ,
        dpt.DepartmentName ,
        desg.Designation ,
		 ISNULL(appMaster.FinancialYearId,0)FinancialYearId ,
		case when appMaster.AppraisalMasterId is null then 0 else appMaster.AppraisalMasterId  end as AppraisalMasterId,
        case when func.SupervisorMark is null or func.SupervisorMark=0 then
		'Not Complete'
		else CONVERT(nvarchar(50), func.SupervisorMark) end as PartA,
        --func.SupervisorMark,
		case when behave.Score is null then 'Not Complete' else CONVERT(nvarchar(50),behave.Score) end as PartB,
		 case when finalStatus.FinalStatus is null then 'Not Complete' else convert(nvarchar(50),finalStatus.FinalStatus) end as PartC,
		case when training.traininingCount is null then 'Not Complete' else 'Complete' end as training
		,(CASE WHEN tblKPIMIDAppraisalMasterAppLog.ActionStatus<>'Approved' then 'Not Approved' ELSE tblKPIMIDAppraisalMasterAppLog.ActionStatus END)AS ActionStatusAppraisal,ForEmpApp.EmpName as PendingEmpApp
FROM tblKPIMIDAppraisalMaster aax    

      left join   tblEmpGeneralInfo emp on  aax.EmpInfoId= emp.EmpInfoId    
		
        LEFT JOIN tblSubSection subSec ON emp.SubSectionId = subSec.SubSectionId
        LEFT JOIN tblSection sec ON emp.SectionId = sec.SectionId
        LEFT JOIN tblDepartment dpt ON emp.DepartmentId = dpt.DepartmentId
        LEFT JOIN tblDivisionWing wing ON emp.DivisionWId = wing.DivisionWId
        LEFT JOIN tblDivision div ON emp.DivisionId = div.DivisionId
        LEFT JOIN tblCompanyInfo comp ON emp.CompanyId = comp.CompanyId
                                         AND div.CompanyId = comp.CompanyId
        LEFT JOIN tblDesignation desg ON emp.DesignationId = desg.DesignationId
        LEFT JOIN tblKPIMIDAppraisalMaster appMaster ON appMaster.EmpInfoId = emp.EmpInfoId  and appMaster.FinancialYearId=aax.FinancialYearId
        LEFT JOIN ( SELECT  SUM(SelfMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON appMaster.AppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SelfScore) Score ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON appMaster.AppraisalMasterId = behave.AppraisalMasterId
        LEFT JOIN tblAppraisalFinalStatus finalStatus ON appMaster.AppraisalMasterId = finalStatus.AppraisalMasterId
        LEFT JOIN ( SELECT  COUNT(AppraisalTrainingId) traininingCount ,
                            AppraisalMasterId
                    FROM    tblAppraisalTrainingNeeds
                    GROUP BY AppraisalMasterId
                  ) training ON appMaster.AppraisalMasterId = training.AppraisalMasterId
                LEFT JOIN (SELECT AppraisalMasterId,MAX(Version)MaxVer FROM dbo.tblKPIMIDAppraisalMasterAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalMasterId) AS CELog ON CELog.AppraisalMasterId= appMaster.AppraisalMasterId
								LEFT JOIN dbo.tblKPIMIDAppraisalMasterAppLog ON tblKPIMIDAppraisalMasterAppLog.AppraisalMasterId = appMaster.AppraisalMasterId
									LEFT JOIN dbo.tblEmpGeneralInfo ForEmpApp ON ForEmpApp.EmpInfoId=dbo.tblKPIMIDAppraisalMasterAppLog.ForEmpInfoId


				  where  emp.EmpInfoId = " + emp + "  " + Parm + "  and (Version=CELog.MaxVer OR Version IS NULL) and  tblKPIMIDAppraisalMasterAppLog.ActionStatus<>'Approved' ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public DataTable GetAppraisalDashboardOwn333(int emp, int FinancialYear, string Parm)
        {
            try
            {
                //_aAcessManager.SqlConnectionOpen(DataBase.HRDB);


                string query = @"SELECT tblKPIMIDAppraisalMasterAppLog.ActionStatus ActionStatusdd,  aax.IsMidYearStatus,
	emp.EmpInfoId,
	emp.EmpMasterCode ,
	aax.AppraisalSelfMasterId,
        emp.EmpName  ,
        CONVERT(nvarchar(11),emp.DateOfJoin,106)DateOfJoin ,
        dpt.DepartmentName ,
        desg.Designation ,
		 ISNULL(appMaster.FinancialYearId,0)FinancialYearId ,
		case when appMaster.AppraisalMasterId is null then 0 else appMaster.AppraisalMasterId  end as AppraisalMasterId,
        case when func.SupervisorMark is null or func.SupervisorMark=0 then
		'Not Complete'
		else CONVERT(nvarchar(50), func.SupervisorMark) end as PartA,
        --func.SupervisorMark,
		case when behave.Score is null then 'Not Complete' else CONVERT(nvarchar(50),behave.Score) end as PartB,
		 case when finalStatus.FinalStatus is null then 'Not Complete' else convert(nvarchar(50),finalStatus.FinalStatus) end as PartC,
		case when training.traininingCount is null then 'Not Complete' else 'Complete' end as training
		,(CASE WHEN tblKPIMIDAppraisalMasterAppLog.ActionStatus<>'Approved' then 'Not Approved' ELSE tblKPIMIDAppraisalMasterAppLog.ActionStatus END)AS ActionStatusAppraisal,ForEmpApp.EmpName as PendingEmpApp
FROM tblKPIMIDAppraisalMaster aax    

      left join   tblEmpGeneralInfo emp on  aax.EmpInfoId= emp.EmpInfoId    
		
        LEFT JOIN tblSubSection subSec ON emp.SubSectionId = subSec.SubSectionId
        LEFT JOIN tblSection sec ON emp.SectionId = sec.SectionId
        LEFT JOIN tblDepartment dpt ON emp.DepartmentId = dpt.DepartmentId
        LEFT JOIN tblDivisionWing wing ON emp.DivisionWId = wing.DivisionWId
        LEFT JOIN tblDivision div ON emp.DivisionId = div.DivisionId
        LEFT JOIN tblCompanyInfo comp ON emp.CompanyId = comp.CompanyId
                                         AND div.CompanyId = comp.CompanyId
        LEFT JOIN tblDesignation desg ON emp.DesignationId = desg.DesignationId
        LEFT JOIN tblKPIMIDAppraisalMaster appMaster ON appMaster.EmpInfoId = emp.EmpInfoId  and appMaster.FinancialYearId=aax.FinancialYearId
        LEFT JOIN ( SELECT  SUM(SelfMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON appMaster.AppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SelfScore) Score ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON appMaster.AppraisalMasterId = behave.AppraisalMasterId
        LEFT JOIN tblAppraisalFinalStatus finalStatus ON appMaster.AppraisalMasterId = finalStatus.AppraisalMasterId
        LEFT JOIN ( SELECT  COUNT(AppraisalTrainingId) traininingCount ,
                            AppraisalMasterId
                    FROM    tblAppraisalTrainingNeeds
                    GROUP BY AppraisalMasterId
                  ) training ON appMaster.AppraisalMasterId = training.AppraisalMasterId
                LEFT JOIN (SELECT AppraisalMasterId,MAX(Version)MaxVer FROM dbo.tblKPIMIDAppraisalMasterAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalMasterId) AS CELog ON CELog.AppraisalMasterId= appMaster.AppraisalMasterId
								LEFT JOIN dbo.tblKPIMIDAppraisalMasterAppLog ON tblKPIMIDAppraisalMasterAppLog.AppraisalMasterId = appMaster.AppraisalMasterId
									LEFT JOIN dbo.tblEmpGeneralInfo ForEmpApp ON ForEmpApp.EmpInfoId=dbo.tblKPIMIDAppraisalMasterAppLog.ForEmpInfoId


				  where  emp.EmpInfoId = " + emp + " and aax.FinancialYearId=" + FinancialYear + Parm  ;

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        
        public DataTable GetAppraisalDashboardOwn333fin(int emp, int FinancialYear, string Parm, string Financial)
        {
            try
            {
                //_aAcessManager.SqlConnectionOpen(DataBase.HRDB);


                string query = @"SELECT DISTINCT * FROM(SELECT  fin.FinancialYearDesc, tblKPIMIDAppraisalMasterAppLog.ActionStatus ActionStatusdd,  aax.IsMidYearStatus,
	emp.EmpInfoId,
	emp.EmpMasterCode ,
	aax.AppraisalSelfMasterId,
        emp.EmpName  ,
        CONVERT(nvarchar(11),emp.DateOfJoin,106)DateOfJoin ,
        dpt.DepartmentName ,
        desg.Designation ,
		 ISNULL(appMaster.FinancialYearId,0)FinancialYearId ,
		case when appMaster.AppraisalMasterId is null then 0 else appMaster.AppraisalMasterId  end as AppraisalMasterId,
        case when func.SupervisorMark is null or func.SupervisorMark=0 then
		'Not Complete'
		else CONVERT(nvarchar(50), func.SupervisorMark) end as PartA,
        --func.SupervisorMark,
		case when behave.Score is null then 'Not Complete' else CONVERT(nvarchar(50),behave.Score) end as PartB,
		 case when finalStatus.FinalStatus is null then 'Not Complete' else convert(nvarchar(50),finalStatus.FinalStatus) end as PartC,
		case when training.traininingCount is null then 'Not Complete' else 'Complete' end as training
		,(CASE WHEN tblKPIMIDAppraisalMasterAppLog.ActionStatus<>'Approved' then 'Not Approved' ELSE tblKPIMIDAppraisalMasterAppLog.ActionStatus END)AS ActionStatusAppraisal,ForEmpApp.EmpName as PendingEmpApp
FROM tblAppraisalSelfMaster aax    

      left join   tblEmpGeneralInfo emp on  aax.EmpInfoId= emp.EmpInfoId    
		
        LEFT JOIN tblSubSection subSec ON emp.SubSectionId = subSec.SubSectionId
        LEFT JOIN tblSection sec ON emp.SectionId = sec.SectionId
        LEFT JOIN tblDepartment dpt ON emp.DepartmentId = dpt.DepartmentId
        LEFT JOIN tblDivisionWing wing ON emp.DivisionWId = wing.DivisionWId
        LEFT JOIN tblDivision div ON emp.DivisionId = div.DivisionId
        LEFT JOIN tblCompanyInfo comp ON emp.CompanyId = comp.CompanyId
                                         AND div.CompanyId = comp.CompanyId
        LEFT JOIN tblDesignation desg ON emp.DesignationId = desg.DesignationId
         LEFT JOIN tblKPIMIDAppraisalMaster appMaster ON appMaster.EmpInfoId = emp.EmpInfoId      and appMaster.FYDes_MSelf=aax.FYDes_Self
	LEFT JOIN tblFinancialYear fin ON fin.FinancialYearId = aax.FinancialYearId  
	LEFT JOIN tblFinancialYear finappMaster ON finappMaster.FinancialYearId = appMaster.FinancialYearId  
        LEFT JOIN ( SELECT  SUM(SelfMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON appMaster.AppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SelfScore) Score ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON appMaster.AppraisalMasterId = behave.AppraisalMasterId
        LEFT JOIN tblAppraisalFinalStatus finalStatus ON appMaster.AppraisalMasterId = finalStatus.AppraisalMasterId
        LEFT JOIN ( SELECT  COUNT(AppraisalTrainingId) traininingCount ,
                            AppraisalMasterId
                    FROM    tblAppraisalTrainingNeeds
                    GROUP BY AppraisalMasterId
                  ) training ON appMaster.AppraisalMasterId = training.AppraisalMasterId
                LEFT JOIN (SELECT AppraisalMasterId,MAX(Version)MaxVer FROM dbo.tblKPIMIDAppraisalMasterAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalMasterId) AS CELog ON CELog.AppraisalMasterId= appMaster.AppraisalMasterId
								LEFT JOIN dbo.tblKPIMIDAppraisalMasterAppLog ON tblKPIMIDAppraisalMasterAppLog.AppraisalMasterId = appMaster.AppraisalMasterId
									LEFT JOIN dbo.tblEmpGeneralInfo ForEmpApp ON ForEmpApp.EmpInfoId=dbo.tblKPIMIDAppraisalMasterAppLog.ForEmpInfoId


				  where  emp.EmpInfoId = " + emp  + " and fin.FinancialYearDesc='" + Financial + "'"    + Parm + @"   				)HH";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


     
        public DataTable GetAppraisalDashboardOwn333fin_dsh(int emp, string Financial, string Parm)
        {
            try
            {
                //_aAcessManager.SqlConnectionOpen(DataBase.HRDB);


                string query = @"SELECT DISTINCT * FROM(SELECT  fin.FinancialYearDesc, tblKPIMIDAppraisalMasterAppLog.ActionStatus ActionStatusdd,  aax.IsMidYearStatus,
	emp.EmpInfoId,
	emp.EmpMasterCode ,
	aax.AppraisalSelfMasterId,
        emp.EmpName  ,
        CONVERT(nvarchar(11),emp.DateOfJoin,106)DateOfJoin ,
        dpt.DepartmentName ,
        desg.Designation ,
		 ISNULL(appMaster.FinancialYearId,0)FinancialYearId ,
		case when appMaster.AppraisalMasterId is null then 0 else appMaster.AppraisalMasterId  end as AppraisalMasterId,
        case when func.SupervisorMark is null or func.SupervisorMark=0 then
		'Not Complete'
		else CONVERT(nvarchar(50), func.SupervisorMark) end as PartA,
        --func.SupervisorMark,
		case when behave.Score is null then 'Not Complete' else CONVERT(nvarchar(50),behave.Score) end as PartB,
		 case when finalStatus.FinalStatus is null then 'Not Complete' else convert(nvarchar(50),finalStatus.FinalStatus) end as PartC,
		case when training.traininingCount is null then 'Not Complete' else 'Complete' end as training
		,(CASE WHEN tblKPIMIDAppraisalMasterAppLog.ActionStatus<>'Approved' then 'Not Approved' ELSE tblKPIMIDAppraisalMasterAppLog.ActionStatus END)AS ActionStatusAppraisal,ForEmpApp.EmpName as PendingEmpApp
FROM tblAppraisalSelfMaster aax    

      left join   tblEmpGeneralInfo emp on  aax.EmpInfoId= emp.EmpInfoId    
		
        LEFT JOIN tblSubSection subSec ON emp.SubSectionId = subSec.SubSectionId
        LEFT JOIN tblSection sec ON emp.SectionId = sec.SectionId
        LEFT JOIN tblDepartment dpt ON emp.DepartmentId = dpt.DepartmentId
        LEFT JOIN tblDivisionWing wing ON emp.DivisionWId = wing.DivisionWId
        LEFT JOIN tblDivision div ON emp.DivisionId = div.DivisionId
        LEFT JOIN tblCompanyInfo comp ON emp.CompanyId = comp.CompanyId
                                         AND div.CompanyId = comp.CompanyId
        LEFT JOIN tblDesignation desg ON emp.DesignationId = desg.DesignationId
        LEFT JOIN tblKPIMIDAppraisalMaster appMaster ON appMaster.EmpInfoId = emp.EmpInfoId  and appMaster.FYDes_MSelf=aax.FYDes_Self
	LEFT JOIN tblFinancialYear fin ON fin.FinancialYearId = aax.FinancialYearId  
        LEFT JOIN ( SELECT  SUM(SelfMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON appMaster.AppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SelfScore) Score ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON appMaster.AppraisalMasterId = behave.AppraisalMasterId
        LEFT JOIN tblAppraisalFinalStatus finalStatus ON appMaster.AppraisalMasterId = finalStatus.AppraisalMasterId
        LEFT JOIN ( SELECT  COUNT(AppraisalTrainingId) traininingCount ,
                            AppraisalMasterId
                    FROM    tblAppraisalTrainingNeeds
                    GROUP BY AppraisalMasterId
                  ) training ON appMaster.AppraisalMasterId = training.AppraisalMasterId
                LEFT JOIN (SELECT AppraisalMasterId,MAX(Version)MaxVer FROM dbo.tblKPIMIDAppraisalMasterAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalMasterId) AS CELog ON CELog.AppraisalMasterId= appMaster.AppraisalMasterId
								LEFT JOIN dbo.tblKPIMIDAppraisalMasterAppLog ON tblKPIMIDAppraisalMasterAppLog.AppraisalMasterId = appMaster.AppraisalMasterId
									LEFT JOIN dbo.tblEmpGeneralInfo ForEmpApp ON ForEmpApp.EmpInfoId=dbo.tblKPIMIDAppraisalMasterAppLog.ForEmpInfoId


				  where  emp.EmpInfoId = " + emp + " and FinancialYearDesc='" + Financial + "'" + Parm + @"   				)HH";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public DataTable GetAppraisalByPermission2(string FInYear,string EmpID)
        {
            try
            {
                string query = @" select * from tblMidYearKPISetup mas
	LEFT JOIN tblFinancialYear fin ON fin.FinancialYearId = mas.FinancialYearId  

where mas.SelectedActionStatus<>'Stop' and mas.CompanyId in (select CompanyId from tblEmpGeneralInfo where EmpInfoId=" + EmpID + ") and fin.FinancialYearDesc='"+ FInYear + "'  ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }
        public DataTable GetAppraisalByPermissionOKRBSC(string FInYear,string EmpID)
        {
            try
            {
                string query = @" select * from tblMidYearOKRBSCSetup mas
	LEFT JOIN tblFinancialYear fin ON fin.FinancialYearId = mas.FinancialYearId  

where mas.SelectedActionStatus<>'Stop' and mas.CompanyId in (select CompanyId from tblEmpGeneralInfo where EmpInfoId=" + EmpID + ") and fin.FinancialYearDesc='"+ FInYear + "'  ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }


        public DataTable GetAppraisalByPermission3(string MasterId)
        {
            try
            {
                string query = @"select  ForEmpInfoId,* from tblKPIMIDAppraisalMasterAppLog where AppraisalMasterId="+MasterId+"  and ActionStatus<>'Drafted' order by AppraisalMasterAppLogId desc  ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public DataTable GetAppraisalDashboardOwnMarkApprove(int emp)
        {
            try
            {
                //_aAcessManager.SqlConnectionOpen(DataBase.HRDB);


                string query = @"SELECT  
	emp.EmpInfoId,
	emp.EmpMasterCode ,
	aax.AppraisalSelfMasterId,
        emp.EmpName  ,
        CONVERT(nvarchar(11),emp.DateOfJoin,106)DateOfJoin ,
        dpt.DepartmentName ,
        desg.Designation ,
		 ISNULL(appMaster.FinancialYearId,0)FinancialYearId ,
		case when appMaster.AppraisalMasterId is null then 0 else appMaster.AppraisalMasterId  end as AppraisalMasterId,
        case when func.SupervisorMark is null or func.SupervisorMark=0 then
		'Not Complete'
		else CONVERT(nvarchar(50), func.SupervisorMark) end as PartA,
        --func.SupervisorMark,
		case when behave.Score is null then 'Not Complete' else CONVERT(nvarchar(50),behave.Score) end as PartB,
		 case when finalStatus.FinalStatus is null then 'Not Complete' else convert(nvarchar(50),finalStatus.FinalStatus) end as PartC,
		case when training.traininingCount is null then 'Not Complete' else 'Complete' end as training

FROM tblKPIMIDAppraisalMaster aax    

      left join   tblEmpGeneralInfo emp on  aax.EmpInfoId= emp.EmpInfoId    
		
        LEFT JOIN tblSubSection subSec ON emp.SubSectionId = subSec.SubSectionId
        LEFT JOIN tblSection sec ON emp.SectionId = sec.SectionId
        LEFT JOIN tblDepartment dpt ON emp.DepartmentId = dpt.DepartmentId
        LEFT JOIN tblDivisionWing wing ON emp.DivisionWId = wing.DivisionWId
        LEFT JOIN tblDivision div ON emp.DivisionId = div.DivisionId
        LEFT JOIN tblCompanyInfo comp ON emp.CompanyId = comp.CompanyId
                                         AND div.CompanyId = comp.CompanyId
        LEFT JOIN tblDesignation desg ON emp.DesignationId = desg.DesignationId
        LEFT JOIN tblKPIMIDAppraisalMaster appMaster ON appMaster.EmpInfoId = emp.EmpInfoId 
        LEFT JOIN ( SELECT  SUM(SelfMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON appMaster.AppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SelfScore) Score ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON appMaster.AppraisalMasterId = behave.AppraisalMasterId
        LEFT JOIN tblAppraisalFinalStatus finalStatus ON appMaster.AppraisalMasterId = finalStatus.AppraisalMasterId
        LEFT JOIN ( SELECT  COUNT(AppraisalTrainingId) traininingCount ,
                            AppraisalMasterId
                    FROM    tblAppraisalTrainingNeeds
                    GROUP BY AppraisalMasterId
                  ) training ON appMaster.AppraisalMasterId = training.AppraisalMasterId
                LEFT JOIN (SELECT AppraisalMasterId,MAX(Version)MaxVer FROM dbo.tblKPIMIDAppraisalMasterAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalMasterId) AS CELog ON CELog.AppraisalMasterId= appMaster.AppraisalMasterId
								LEFT JOIN dbo.tblKPIMIDAppraisalMasterAppLog ON tblKPIMIDAppraisalMasterAppLog.AppraisalMasterId = appMaster.AppraisalMasterId


				  where  emp.EmpInfoId = " + emp + "   and emp.empinfoId in " +
                               "(SELECT dd.EmpinfoId   FROM dbo.tblAppraisalDeadlineMaster dM  INNER JOIN dbo.tblAppraisalDeadLineDetails dd ON" +
                               " dm.AppraisalDeadLineMasterId = dd.AppraisalDeadLineMasterId WHERE  CONVERT(DATE , GETDATE()) <= dd.DeadLine  or   " +
                               "dd.ExtensionDate >= CASE WHEN dd.ExtensionDate IS NULL THEN NULL ELSE CONVERT(date,GETDATE()) END ) AND  (Version=CELog.MaxVer OR Version IS NULL)   ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }





        public DataTable GetAppraisalDashboardSup(int emp, int FinancialYear)
        {
            try
            {
                //_aAcessManager.SqlConnectionOpen(DataBase.HRDB);


                string query = @"SELECT *,(CASE WHEN tblt.PartA<>'Not Complete' AND tblt.PartB<>'Not Complete'   THEN 'True' ELSE 'False' END )CompleteStatus FROM ( SELECT  emp.EmpInfoId ,
        emp.EmpMasterCode ,
        emp.ReportingEmpId ,
        aax.AppraisalSelfMasterId ,
        emp.EmpName ,
        CONVERT(NVARCHAR(11), emp.DateOfJoin, 106) DateOfJoin ,
        dpt.DepartmentName ,
        desg.Designation ,
        ISNULL(aax.FinancialYearId, 0) FinancialYearId ,
        CASE WHEN aax.AppraisalMasterId IS NULL THEN 0
             ELSE aax.AppraisalMasterId
        END AS AppraisalMasterId ,
        CASE WHEN func.SupervisorMark IS NULL
                  OR func.SupervisorMark = 0 THEN 'Not Complete'
             ELSE CONVERT(NVARCHAR(50), func.SupervisorMark)
        END AS PartA ,
        --func.SupervisorMark,
        CASE WHEN (behave.Score IS NULL OR behave.Score=0) THEN 'Not Complete'
             ELSE CONVERT(NVARCHAR(50), behave.Score)
        END AS PartB ,
        CASE WHEN finalStatus.FinalStatus IS NULL THEN 'Not Complete'
             ELSE CONVERT(NVARCHAR(50), finalStatus.FinalStatus)
        END AS PartC ,
        CASE WHEN training.traininingCount IS NULL THEN 'Not Complete'
             ELSE 'Complete'
        END AS training,AppraisalMasterAppLogId
FROM    tblKPIMIDAppraisalMaster aax
        LEFT JOIN tblEmpGeneralInfo emp ON aax.EmpInfoId = emp.EmpInfoId
        LEFT JOIN tblSubSection subSec ON emp.SubSectionId = subSec.SubSectionId
        LEFT JOIN tblSection sec ON emp.SectionId = sec.SectionId
        LEFT JOIN tblDepartment dpt ON emp.DepartmentId = dpt.DepartmentId
        LEFT JOIN tblDivisionWing wing ON emp.DivisionWId = wing.DivisionWId
        LEFT JOIN tblDivision div ON emp.DivisionId = div.DivisionId
        LEFT JOIN tblCompanyInfo comp ON emp.CompanyId = comp.CompanyId
                                         AND div.CompanyId = comp.CompanyId
        LEFT JOIN tblDesignation desg ON emp.DesignationId = desg.DesignationId
        LEFT JOIN ( SELECT  SUM(SupervisorMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON aax.AppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SupervisorScore) Score ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON aax.AppraisalMasterId = behave.AppraisalMasterId
        LEFT JOIN tblAppraisalFinalStatus finalStatus ON aax.AppraisalMasterId = finalStatus.AppraisalMasterId
        LEFT JOIN ( SELECT  COUNT(AppraisalTrainingId) traininingCount ,
                            AppraisalMasterId
                    FROM    tblAppraisalTrainingNeeds
                    GROUP BY AppraisalMasterId
                  ) training ON aax.AppraisalMasterId = training.AppraisalMasterId

LEFT JOIN (SELECT AppraisalMasterId,MAX(Version)MaxVer FROM dbo.tblKPIMIDAppraisalMasterAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalMasterId) AS CELog ON CELog.AppraisalMasterId= aax.AppraisalMasterId
								LEFT JOIN dbo.tblKPIMIDAppraisalMasterAppLog ON tblKPIMIDAppraisalMasterAppLog.AppraisalMasterId = aax.AppraisalMasterId

				  where (Version=CELog.MaxVer )  and  (emp.ReportingEmpId = '" + emp + "' ) and aax.FinancialYearId=" + FinancialYear + " )tblt ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public DataTable GetAppraisalDashboardSupApproval(int emp)
        {
            try
            {
                //_aAcessManager.SqlConnectionOpen(DataBase.HRDB);


                string query = @"SELECT     FinancialYearId,*,(CASE WHEN tblt.PartA<>'Not Complete' AND tblt.PartB<>'Not Complete'   THEN 'True' ELSE 'False' END )CompleteStatus FROM ( SELECT CASE WHEN ISNULL(dici.DiciplinaryCount,'0')=0 THEN 'No' ELSE 'Yes' End DiciplinaryCount, emp.EmpInfoId ,
        emp.EmpMasterCode ,
        emp.ReportingEmpId ,
        aax.AppraisalSelfMasterId ,
        emp.EmpName ,
        CONVERT(NVARCHAR(11), emp.DateOfJoin, 106) DateOfJoin ,
        dpt.DepartmentName ,
        desg.Designation ,
        ISNULL(aax.FinancialYearId, 0) FinancialYearId ,
        CASE WHEN aax.AppraisalMasterId IS NULL THEN 0
             ELSE aax.AppraisalMasterId
        END AS AppraisalMasterId ,
        CASE WHEN func.SupervisorMark IS NULL
                  OR func.SupervisorMark = 0 THEN 'Not Complete'
             ELSE CONVERT(NVARCHAR(50), func.SupervisorMark)
        END AS PartA ,
        --func.SupervisorMark,
        CASE WHEN (behave.Score IS NULL OR behave.Score=0) THEN 'Not Complete'
             ELSE CONVERT(NVARCHAR(50), behave.Score)
        END AS PartB ,
        CASE WHEN finalStatus.FinalStatus IS NULL THEN 'Not Complete'
             ELSE CONVERT(NVARCHAR(50), finalStatus.FinalStatus)
        END AS PartC ,
        CASE WHEN training.traininingCount IS NULL THEN 'Not Complete'
             ELSE 'Complete'
        END AS training,AppraisalMasterAppLogId
FROM    tblKPIMIDAppraisalMaster aax
        LEFT JOIN tblEmpGeneralInfo emp ON aax.EmpInfoId = emp.EmpInfoId
        LEFT JOIN tblSubSection subSec ON emp.SubSectionId = subSec.SubSectionId
        LEFT JOIN tblSection sec ON emp.SectionId = sec.SectionId
        LEFT JOIN tblDepartment dpt ON emp.DepartmentId = dpt.DepartmentId
        LEFT JOIN tblDivisionWing wing ON emp.DivisionWId = wing.DivisionWId
        LEFT JOIN tblDivision div ON emp.DivisionId = div.DivisionId
        LEFT JOIN tblCompanyInfo comp ON emp.CompanyId = comp.CompanyId
                                         AND div.CompanyId = comp.CompanyId
        LEFT JOIN tblDesignation desg ON emp.DesignationId = desg.DesignationId
        LEFT JOIN ( SELECT  SUM(SupervisorMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON aax.AppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SupervisorScore) Score ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON aax.AppraisalMasterId = behave.AppraisalMasterId
        LEFT JOIN tblAppraisalFinalStatus finalStatus ON aax.AppraisalMasterId = finalStatus.AppraisalMasterId
        LEFT JOIN ( SELECT  COUNT(AppraisalTrainingId) traininingCount ,
                            AppraisalMasterId
                    FROM    tblAppraisalTrainingNeeds
                    GROUP BY AppraisalMasterId
                  ) training ON aax.AppraisalMasterId = training.AppraisalMasterId


				   LEFT JOIN ( SELECT EmpInfoId,  COUNT(*) DiciplinaryCount  
                            
                    FROM    tblDiciplinaryAction
                    GROUP BY EmpInfoId
                  ) dici ON emp.EmpInfoId = dici.EmpInfoId

LEFT JOIN (SELECT AppraisalMasterId,MAX(Version)MaxVer FROM dbo.tblKPIMIDAppraisalMasterAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY AppraisalMasterId) AS CELog ON CELog.AppraisalMasterId= aax.AppraisalMasterId
								LEFT JOIN dbo.tblKPIMIDAppraisalMasterAppLog ON tblKPIMIDAppraisalMasterAppLog.AppraisalMasterId = aax.AppraisalMasterId

				  where (Version=CELog.MaxVer )  and  (ForEmpInfoId = '" + emp + "' )  )tblt  Where tblt.EmpInfoId NOT IN (SELECT EmployeeId from tblEmployeeJobLeft)  ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);


            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
//        public DataTable GetAppraisalDashboardSup(int emp)
//        {
//            try
//            {
//                //_aAcessManager.SqlConnectionOpen(DataBase.HRDB);


//                string query = @"SELECT  emp.EmpInfoId ,
//        emp.EmpMasterCode ,
//        emp.ReportingEmpId ,
//        aax.AppraisalSelfMasterId ,
//        emp.EmpName ,
//        CONVERT(NVARCHAR(11), emp.DateOfJoin, 106) DateOfJoin ,
//        dpt.DepartmentName ,
//        desg.Designation ,
//        ISNULL(aax.FinancialYearId, 0) FinancialYearId ,
//        CASE WHEN aax.AppraisalMasterId IS NULL THEN 0
//             ELSE aax.AppraisalMasterId
//        END AS AppraisalMasterId ,
//        CASE WHEN func.SupervisorMark IS NULL
//                  OR func.SupervisorMark = 0 THEN 'Not Complete'
//             ELSE CONVERT(NVARCHAR(50), func.SupervisorMark)
//        END AS PartA ,
//        --func.SupervisorMark,
//        CASE WHEN behave.Score IS NULL THEN 'Not Complete'
//             ELSE CONVERT(NVARCHAR(50), behave.Score)
//        END AS PartB ,
//        CASE WHEN finalStatus.FinalStatus IS NULL THEN 'Not Complete'
//             ELSE CONVERT(NVARCHAR(50), finalStatus.FinalStatus)
//        END AS PartC ,
//        CASE WHEN training.traininingCount IS NULL THEN 'Not Complete'
//             ELSE 'Complete'
//        END AS training
//FROM    tblKPIMIDAppraisalMaster aax
//        LEFT JOIN tblEmpGeneralInfo emp ON aax.EmpInfoId = emp.EmpInfoId
//        LEFT JOIN tblSubSection subSec ON emp.SubSectionId = subSec.SubSectionId
//        LEFT JOIN tblSection sec ON emp.SectionId = sec.SectionId
//        LEFT JOIN tblDepartment dpt ON emp.DepartmentId = dpt.DepartmentId
//        LEFT JOIN tblDivisionWing wing ON emp.DivisionWId = wing.DivisionWId
//        LEFT JOIN tblDivision div ON emp.DivisionId = div.DivisionId
//        LEFT JOIN tblCompanyInfo comp ON emp.CompanyId = comp.CompanyId
//                                         AND div.CompanyId = comp.CompanyId
//        LEFT JOIN tblDesignation desg ON emp.DesignationId = desg.DesignationId
//        LEFT JOIN ( SELECT  SUM(SupervisorMark) SupervisorMark ,
//                            AppraisalMasterId
//                    FROM    tblKPIMIDAppraisalFuncArea
//                    GROUP BY AppraisalMasterId
//                  ) func ON aax.AppraisalMasterId = func.AppraisalMasterId
//        LEFT JOIN ( SELECT  SUM(SupervisorScore) Score ,
//                            AppraisalMasterId
//                    FROM    tblKPIMIDAppraisalBehaveArea
//                    GROUP BY AppraisalMasterId
//                  ) behave ON aax.AppraisalMasterId = behave.AppraisalMasterId
//        LEFT JOIN tblAppraisalFinalStatus finalStatus ON aax.AppraisalMasterId = finalStatus.AppraisalMasterId
//        LEFT JOIN ( SELECT  COUNT(AppraisalTrainingId) traininingCount ,
//                            AppraisalMasterId
//                    FROM    tblAppraisalTrainingNeeds
//                    GROUP BY AppraisalMasterId
//                  ) training ON aax.AppraisalMasterId = training.AppraisalMasterId
//
//				  where aax.CurrentStatus ='Approved'  and emp.ReportingEmpId =  " + emp + " or   aax.EmpInfoId IN (SELECT EmpInfoId FROM dbo.tblEmpGeneralInfo WHERE ReportingEmpId  =  " + emp + " AND IsActive = 1 ) AND aax.IsApprove=1 and aax.SelfApprove='Posted' ";

//                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);


//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }

//        }




        public DataTable AppraisalApproveFinal(int userEmpId)
        {
            try
            {
                string dataTable = @"SELECT  
	emp.EmpInfoId,
	emp.EmpMasterCode ,
    emp.ReportingEmpId,
	aax.AppraisalMasterId,
        emp.EmpName  ,
        CONVERT(nvarchar(11),emp.DateOfJoin,106)DateOfJoin ,
        dpt.DepartmentName ,
        desg.Designation ,
		 ISNULL(appMaster.FinancialYearId,0)FinancialYearId ,
		case when appMaster.AppraisalMasterId is null then 0 else appMaster.AppraisalMasterId  end as AppraisalMasterId,
        case when appMaster.AppraisalMasterId is null or appMaster.AppraisalMasterId=0 then
		'Not Complete'
		else CONVERT(nvarchar(50), func.SupervisorMark) end as PartA,
        --func.SupervisorMark,
		case when behave.Score is null then 'Not Complete' else CONVERT(nvarchar(50),behave.Score) end as PartB
		

FROM tblKPIMIDAppraisalMaster aax    

      left join   tblEmpGeneralInfo emp on  aax.EmpInfoId= emp.EmpInfoId    
		
        LEFT JOIN tblSubSection subSec ON emp.SubSectionId = subSec.SubSectionId
        LEFT JOIN tblSection sec ON emp.SectionId = sec.SectionId
        LEFT JOIN tblDepartment dpt ON emp.DepartmentId = dpt.DepartmentId
        LEFT JOIN tblDivisionWing wing ON emp.DivisionWId = wing.DivisionWId
        LEFT JOIN tblDivision div ON emp.DivisionId = div.DivisionId
        LEFT JOIN tblCompanyInfo comp ON emp.CompanyId = comp.CompanyId
                                         AND div.CompanyId = comp.CompanyId
        LEFT JOIN tblDesignation desg ON emp.DesignationId = desg.DesignationId
        LEFT JOIN tblKPIMIDAppraisalMaster appMaster ON appMaster.EmpInfoId = emp.EmpInfoId 
        LEFT JOIN ( SELECT  SUM(SelfMark) SupervisorMark ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalFuncArea
                    GROUP BY AppraisalMasterId
                  ) func ON appMaster.AppraisalMasterId = func.AppraisalMasterId
        LEFT JOIN ( SELECT  SUM(SelfScore) Score ,
                            AppraisalMasterId
                    FROM    tblKPIMIDAppraisalBehaveArea
                    GROUP BY AppraisalMasterId
                  ) behave ON appMaster.AppraisalMasterId = behave.AppraisalMasterId
       
      

		 WHERE aax.SelfApprove='Posted'  AND behave.Score>0 AND func.SupervisorMark>0  AND emp.ReportingEmpId =  " + userEmpId + " ";

                return _aCommonInternalDal.DataContainerDataTable(dataTable, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }





        public bool ApproveRejectAppraisalFinal(int master , int selfMaster ,  string user,string status )
        {
            try
            {
                string q = @"select ActionVersion from tblKPIMIDAppraisalMaster where AppraisalMasterId = " + master +
                           "  and  AppraisalSelfMasterId = " + selfMaster + " ";
                
               DataTable dt =  _aCommonInternalDal.DataContainerDataTable(q, DataBase.HRDB);


                int version = 0;
                if (dt.Rows.Count <= 0)
                {
                    version = 0;
                }
                else
                {
                    version = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
                int isApp = status == "Approve" ? 1 : 0;
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@AppraisalSelfMasterId" ,selfMaster ));
                aList.Add(new SqlParameter("@AppraisalMasterId", master));
                aList.Add(new SqlParameter("@PreviousVersion", version));
                aList.Add(new SqlParameter("@EntryBy", user));
                aList.Add(new SqlParameter("@EntryDate", DateTime.Now.ToString()));
                aList.Add(new SqlParameter("@NewVersion", (version+1)));
                aList.Add(new SqlParameter("@ApproveStatus", status));
                aList.Add(new SqlParameter("@Remarks", ""));
                string insert = @"INSERT INTO dbo.tblAppraisalApproveLog
	                               ( AppraisalSelfMasterId ,
	                                 AppraisalMasterId ,
	                                 PreviousVersion ,
	                                 NewVersion ,
	                                 EntryDate ,
	                                 EntryBy ,
	                                 ApproveStatus ,
	                                 Remarks
	                               )
	                       VALUES  ( @AppraisalSelfMasterId ,
	                                 @AppraisalMasterId ,
	                                 @PreviousVersion ,
	                                 @NewVersion ,
	                                 @EntryDate ,
	                                 @EntryBy ,
	                                 @ApproveStatus ,
	                                 @Remarks
	                               )";


                
                bool result = _aCommonInternalDal.SaveDataByInsertCommand(insert, aList, DataBase.HRDB);

                string up = @"update tblKPIMIDAppraisalMaster set IsApprove = " + isApp + " , ApproveBy = '" + user +
                            "' , ApproveDate = '" + DateTime.Now + "' , ActionVersion = " + (version + 1) +
                            " , CurrentStatus = '" + status + "' where  AppraisalMasterId = " + master +
                            " and  AppraisalSelfMasterId = " + selfMaster + " ";

                result = _aCommonInternalDal.UpdateDataByUpdateCommand(up, DataBase.HRDB);
                return result;





            }
            catch (Exception exception)
            {

                throw exception;
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
                    aParameters.Add(new SqlParameter("@AppraisalMasterAppLogId", id));
                    aParameters.Add(new SqlParameter("@ActionStatus", status));


                    string query =
                        @"update tblKPIMIDAppraisalMasterAppLog set ActionStatus=@ActionStatus  where AppraisalMasterAppLogId = @AppraisalMasterAppLogId";

                    bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    return result;

                }

            }
            catch (Exception exception)
            {

                throw exception;
            }
        }

        public int SaveEmpAppLog(AppraisalMasterAppLogDAO appLogDao)
        {

            try
            {
                int pk = 0;


                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@AppraisalMasterId", appLogDao.AppraisalMasterId));
                    aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                    aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                    aParameters.Add(new SqlParameter("@Version", appLogDao.Version));
                    aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                    aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                    aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                    aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));
                    aParameters.Add(new SqlParameter("@CommentEmpId", HttpContext.Current.Session["EmpInfoId"].ToString()));


                    string query = @"INSERT INTO dbo.tblKPIMIDAppraisalMasterAppLog
                                    (
                                    AppraisalMasterId,
                                    PreEmpInfoId,
                                    ForEmpInfoId,
                                    Version,
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments,CommentEmpId
                                    )
                                    VALUES(
                                    @AppraisalMasterId,
                                    @PreEmpInfoId,
                                    @ForEmpInfoId,
                                    (SELECT (COUNT(*)+1) FROM dbo.tblKPIMIDAppraisalMasterAppLog WHERE AppraisalMasterId=@AppraisalMasterId),
                                    @ApproveBy,
                                    @ApproveDate,
                                    @ActionStatus,@Comments,@CommentEmpId
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
                string query = @"SELECT *FROM dbo.tblEmpGeneralInfo " + param + "";

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
                string query = @"SELECT * FROM dbo.tblKPIMIDAppraisalMasterAppLog WHERE ForEmpInfoId='" + forempInfoid + "' AND AppraisalMasterId='" + jdmasterId + "' AND ActionStatus NOT IN ('Review')";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetEmpIdfromAppraisalInfo( string jdmasterId)
        {
            try
            {
                string query = @"select EmpInfoId from tblKPIMIDAppraisalMaster where AppraisalMasterId='" + jdmasterId + "'";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetEmpIdfromKPIInfo(string jdmasterId)
        {
            try
            {
                string query = @"select EmpInfoId from tblKPIMIDAppraisalMaster where AppraisalSelfMasterId='" + jdmasterId + "'";

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
                string query = @"SELECT * FROM dbo.tblSupevisorMenuApproval WHERE EmpInfoId='" + empinfoId + "' AND FromEmpInfoId='" + fromempInfoId + "'";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetSupervisorEmployeeAppIdCheck(string empinfoId)
        {
            try
            {
                string query = @"SELECT * FROM dbo.tblSupevisorMenuApproval WHERE FromEmpInfoId='" + empinfoId + "'  ";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool UpdateContractural(AppraisalMasterAppLogDAO aMaster)
        {

            try
            {
                int pk = 0;

                if (aMaster.AppraisalMasterId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@AppraisalMasterId", aMaster.AppraisalMasterId));
                    aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));


                    string query =
                        @"update tblKPIMIDAppraisalMaster set CurrentStatus=@ActionStatus,SelfApprove='Approved'  where AppraisalMasterId = @AppraisalMasterId";

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


        public bool UpdateMidYearStatus(int id, string MidYear)
        {

            try
            {
                int pk = 0;

                if (id > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@AppraisalSelfFucAreaId", id));
                    aParameters.Add(new SqlParameter("@MidYearStatus", MidYear));


                    string query =
                        @"update tblAppraisalSelfFuncArea set MidYearStatus=@MidYearStatus   where AppraisalSelfFucAreaId = @AppraisalSelfFucAreaId


update tblKPIMIDAppraisalFuncArea set MidYearStatus=@MidYearStatus   where AppraisalSelfFucAreaId = @AppraisalSelfFucAreaId
";

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



        public bool UpdateIsMidYearStatus ( int  id, bool MidYear)
        {

            try
            {
                int pk = 0;

                if (id > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@AppraisalMasterId", id));
                    aParameters.Add(new SqlParameter("@IsMidYearStatus", MidYear));


                    string query =
                        @"update tblKPIMIDAppraisalMaster set IsMidYearStatus=@IsMidYearStatus   where AppraisalSelfMasterId = @AppraisalMasterId";

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
                string query = @"SELECT * FROM dbo.tblKPIMIDAppraisalMasterAppLog
Inner JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblKPIMIDAppraisalMasterAppLog.PreEmpInfoId
 WHERE AppraisalSelfMasterId='" + id + "' AND tblKPIMIDAppraisalMasterAppLog.ActionStatus<>'Drafted'";
                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

    }
}
