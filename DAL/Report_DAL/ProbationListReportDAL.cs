using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataManager;
using DAL.InternalCls;

namespace DAL.Report_DAL
{
  public  class ProbationListReportDAL
    {

      private ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();


      public DataTable GetJdByMasterByCom(string Parm)
      {
          try
          {
              string query = @"select distinct * from (SELECT Emp.CompanyId, EGI.ProbationEvaluationMasterId,  CI.ShortName, Emp.EmpMasterCode, Emp.EmpName, Emp.DateOfJoin, DSN.DivisionName, DPT.DepartmentName, Emp.ProbationEndDate, DSG.Designation, EGI.DivHeadOvserv, EGI.SupervisorObserv, EGI.DeptHeadObserv, EGI.ProbationEndReason, EGI.ExProDate,EGI.ActionStatus2,ForEmp.EmpName as AwEmpName  FROM tblProbationEvaluationMaster EGI
  LEFT JOIN dbo.tblEmpGeneralInfo AS Emp ON EGI.EmpInfoId = Emp.EmpInfoId
  LEFT JOIN dbo.tblCompanyInfo AS CI ON Emp.CompanyId = CI.CompanyId
  LEFT JOIN dbo.tblDivision AS DSN ON Emp.DivisionId = DSN.DivisionId     
                                    LEFT JOIN dbo.tblDepartment AS DPT ON Emp.DepartmentId = DPT.DepartmentId
                                    LEFT JOIN dbo.tblDesignation AS DSG ON Emp.DesignationId = DSG.DesignationId
									LEFT JOIN dbo.tblEmployeeType AS ETP ON Emp.EmpTypeId = ETP.EmpTypeId
                                LEFT JOIN (SELECT ProbationEvaluationMasterId,MAX(Version)MaxVer FROM dbo.tblProbationEvaluationAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY ProbationEvaluationMasterId) AS LogApp ON LogApp.ProbationEvaluationMasterId= EGI.ProbationEvaluationMasterId
								LEFT JOIN dbo.tblProbationEvaluationAppLog ON tblProbationEvaluationAppLog.ProbationEvaluationMasterId = EGI.ProbationEvaluationMasterId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblProbationEvaluationAppLog.ForEmpInfoId
                                LEFT JOIN dbo.tblProbationEvaluationAppLog PreLog ON PreLog.ProbationEvaluationMasterId=EGI.ProbationEvaluationMasterId AND PreLog.Version = CONVERT(INT,LogApp.MaxVer)-1
								LEFT JOIN dbo.tblEmpGeneralInfo PreEmp ON PreEmp.EmpInfoId=PreLog.ForEmpInfoId
									WHERE Emp.CompanyId= " + Parm + @"  AND (tblProbationEvaluationAppLog.Version=LogApp.MaxVer OR tblProbationEvaluationAppLog.Version IS NULL)    union all 

									SELECT Emp.CompanyId, EGI.ProbationEvaluationMasterId,  CI.ShortName, Emp.EmpMasterCode, Emp.EmpName, Emp.DateOfJoin, DSN.DivisionName, DPT.DepartmentName, Emp.ProbationEndDate, DSG.Designation, EGI.DivHeadOvserv, EGI.SupervisorObserv, EGI.DeptHeadObserv, EGI.ProbationEndReason, EGI.ExProDate,EGI.ActionStatus2,ForEmp.EmpName as AwEmpName  FROM tblProbationEvaluationMaster EGI
  LEFT JOIN dbo.tblEmpGeneralInfo AS Emp ON EGI.EmpInfoId = Emp.EmpInfoId
  LEFT JOIN dbo.tblCompanyInfo AS CI ON Emp.CompanyId = CI.CompanyId
  LEFT JOIN dbo.tblDivision AS DSN ON Emp.DivisionId = DSN.DivisionId     
                                    LEFT JOIN dbo.tblDepartment AS DPT ON Emp.DepartmentId = DPT.DepartmentId
                                    LEFT JOIN dbo.tblDesignation AS DSG ON Emp.DesignationId = DSG.DesignationId
									LEFT JOIN dbo.tblEmployeeType AS ETP ON Emp.EmpTypeId = ETP.EmpTypeId
                                LEFT JOIN (SELECT ProbationEvaluationMasterId,MAX(Version)MaxVer FROM dbo.tblProbationEvaluationAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY ProbationEvaluationMasterId) AS LogApp ON LogApp.ProbationEvaluationMasterId= EGI.ProbationEvaluationMasterId
								LEFT JOIN dbo.tblProbationEvaluationAppLog ON tblProbationEvaluationAppLog.ProbationEvaluationMasterId = EGI.ProbationEvaluationMasterId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblProbationEvaluationAppLog.ForEmpInfoId
                                LEFT JOIN dbo.tblProbationEvaluationAppLog PreLog ON PreLog.ProbationEvaluationMasterId=EGI.ProbationEvaluationMasterId AND PreLog.Version = CONVERT(INT,LogApp.MaxVer)-1
								LEFT JOIN dbo.tblEmpGeneralInfo PreEmp ON PreEmp.EmpInfoId=PreLog.ForEmpInfoId
									inner JOIN   tblEmpAllRefference reff  ON Emp.EmpInfoId = reff.RefferenceEmpId   
    inner join (select   NewEmployeeId, case when  IsSMCRecordView=1 then '1'   when  IsELRecordView=1 then '2' else '1,2' end ComAssain from tblEmpSpecialTransfer  where OnlyView=1  ) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 WHERE   Emp.IsActive=1  and     reff.ShowCompany in (ComAssain)  AND (tblProbationEvaluationAppLog.Version=LogApp.MaxVer OR tblProbationEvaluationAppLog.Version IS NULL))tbl  order by EmpMasterCode asc";

              return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw;
          }
      }
      public DataTable GetJdByMaster(int id)
      {
          try
          {
              string query = @"SELECT Emp.CompanyId, EGI.ProbationEvaluationMasterId,  CI.ShortName, Emp.EmpMasterCode, Emp.EmpName, Emp.DateOfJoin, DSN.DivisionName, DPT.DepartmentName, Emp.ProbationEndDate, DSG.Designation, EGI.DivHeadOvserv, EGI.SupervisorObserv, EGI.DeptHeadObserv, EGI.ProbationEndReason, EGI.ExProDate  FROM tblProbationEvaluationMaster EGI
  LEFT JOIN dbo.tblEmpGeneralInfo AS Emp ON EGI.EmpInfoId = Emp.EmpInfoId
  LEFT JOIN dbo.tblCompanyInfo AS CI ON Emp.CompanyId = CI.CompanyId
  LEFT JOIN dbo.tblDivision AS DSN ON Emp.DivisionId = DSN.DivisionId     
                                    LEFT JOIN dbo.tblDepartment AS DPT ON Emp.DepartmentId = DPT.DepartmentId
                                    LEFT JOIN dbo.tblDesignation AS DSG ON Emp.DesignationId = DSG.DesignationId
									LEFT JOIN dbo.tblEmployeeType AS ETP ON Emp.EmpTypeId = ETP.EmpTypeId

									WHERE EGI.ProbationEvaluationMasterId=" + id + "";

              return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw;
          }
      }

      public DataTable GetJdDetails(int id)
      {
          try
          {
              string query = @"SELECT * FROM tblProbationEvaluationDetails WHERE ProbationEvaluationMasterId=" + id + "";

              return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception ex)
          {

              throw;
          }
      }
    }
}
