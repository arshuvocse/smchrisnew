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
   public class JobDescriptionViewReportDAL
    {

       private ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
       public DataTable GetJdByMaster(int id)
       {
           try
           {
               string query = @" SELECT a.JdMasterId, com.ShortName, b.EmpName as employeeName, b.EmpMasterCode, div.DivisionName, Dpt.DepartmentName,  DSG.Designation, ETP.EmpType, b.DateOfJoin, a.JdSummary , a.ActionStatus, a.EmpInfoId , a.FinancialYearId    from tblJdMaster A 
 LEFT join tblEmpGeneralInfo b on a.EmpInfoId = b.EmpInfoId
 LEFT join dbo.tblDivision div on b.DivisionId = div.DivisionId
 LEFT join dbo.tblDepartment Dpt on b.DepartmentId = Dpt.DepartmentId
  LEFT JOIN dbo.tblDesignation AS DSG ON b.DesignationId = DSG.DesignationId
	 LEFT JOIN dbo.tblEmployeeType AS ETP ON b.EmpTypeId = ETP.EmpTypeId
	 LEFT JOIN dbo.tblCompanyInfo AS com ON b.CompanyId = com.CompanyId

                where a.JdMasterId =" + id + "";

               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw;
           }
       }


       public DataTable GetJdByMaster2(int id)
       {
           try
           {
               string query = @" SELECT a.JdMasterId, com.ShortName, b.EmpName as employeeName, b.EmpMasterCode, div.DivisionName, Dpt.DepartmentName,  DSG.Designation, ETP.EmpType, b.DateOfJoin, a.JdSummary , a.ActionStatus, a.EmpInfoId , a.FinancialYearId    from tblJdMaster A 
 LEFT join tblEmpGeneralInfo b on a.EmpInfoId = b.EmpInfoId
 LEFT join dbo.tblDivision div on b.DivisionId = div.DivisionId
 LEFT join dbo.tblDepartment Dpt on b.DepartmentId = Dpt.DepartmentId
  LEFT JOIN dbo.tblDesignation AS DSG ON b.DesignationId = DSG.DesignationId
	 LEFT JOIN dbo.tblEmployeeType AS ETP ON b.EmpTypeId = ETP.EmpTypeId
	 LEFT JOIN dbo.tblCompanyInfo AS com ON b.CompanyId = com.CompanyId

                where a.EmpInfoId =" + id + "";

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
               string query = @"Select JdMasterId, JdDetailsInfo from tblJdDetails where JdMasterId=" + id + "";

               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw;
           }
       }
    }
}
