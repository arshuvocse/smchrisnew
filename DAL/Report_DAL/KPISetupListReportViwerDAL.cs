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
    public class KPISetupListReportViwerDAL
    {

       private ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
       public DataTable GetJdByMaster(int id)
       {
           try
           {
               string query = @"select com.ShortName, Fin.FinancialYearDesc, * from tblKpiDeadlineMaster m
LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = m.CompanyId
LEFT JOIN dbo.tblFinancialYear Fin ON Fin.FinancialYearId = m.FinancialYearId
WHERE m.KPIDeadLineMasterId= " + id + "";
               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

       public DataTable GetJdDetails(int id)
       {
           try
           {
               string query = @"SELECT    dtls.EmpinfoId,KPIDeadLineDetailsId, KPIDeadLineMasterId, empInfo.EmpMasterCode, empInfo.EmpName, DeadLine, dtls.Remarks, 0 AS DivisionName,
 desg.Designation, Dpt.DepartmentName  FROM   tblKPIDeadLineDetails dtls
INNER JOIN dbo.tblEmpGeneralInfo empInfo ON  dtls.EmpinfoId=empInfo.EmpInfoId
left JOIN dbo.tblDesignation desg ON  empInfo.DesignationId=desg.DesignationId
left JOIN dbo.tblDepartment Dpt ON  empInfo.DepartmentId=Dpt.DepartmentId
 
 where KPIDeadLineMasterId = " +
                   id + " ";
               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception exception)
           {

               throw exception;
           }
       }
    }
}
