using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.InternalCls;

namespace DAL.Report_DAL
{
  public  class EmployeeSuspendReportDAL
    {
      readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
      public DataTable EmpSuspendInformation(string suspendId)
      {
          var aSqlParameterlist = new List<SqlParameter>();
          aSqlParameterlist.Add(new SqlParameter("@SuspendId", suspendId));

          const string query = @"
SELECT com.ShortName, Fin.FinancialYearDesc, SPND.SuspendId,EGI.EmpInfoId,EGI.EmpMasterCode,DPT.DepartmentName, ETP.EmpTypeId, ETP.EmpType,ReasonId,SPND.CompanyInfoId,
                                  EGI.DateOfJoin, EGI.EmpName,SPND.EffectiveDate,SPND.Description,
                                  SPND.Remarks,SPNDR.SuspendReasonEntry,DPT.DepartmentId,DPT.DepartmentName,DSG.DesignationId,
								  DSG.Designation, rsn.SuspendReasonEntry, SPND.*  FROM dbo.tblSuspend AS SPND 
                                  LEFT JOIN tblEmpGeneralInfo AS EGI ON EGI.EmpInfoId = SPND.EmpInfoId
                                  LEFT JOIN dbo.tblDepartment AS DPT ON SPND.DeptId = DPT.DepartmentId
                                  LEFT JOIN dbo.tblSuspendReasonEntry AS SPNDR ON SPND.ReasonId =  SPNDR.SuspendReasonEntryId
                                  LEFT JOIN dbo.tblDesignation AS DSG ON SPND.DesigId = DSG.DesignationId
                                  LEFT JOIN dbo.tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
								   
										LEFT JOIN dbo.tblCompanyInfo  com ON SPND.CompanyInfoId=com.CompanyId								
													LEFT JOIN dbo.tblFinancialYear  Fin ON SPND.FinancialYearId=Fin.FinancialYearId	
									LEFT JOIN tblSuspendReasonEntry rsn ON SPND.ReasonId=rsn.SuspendReasonEntryId
								   WHERE SPND.SuspendId =  @SuspendId";

          return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, "HRDB");
      }
    }
}
