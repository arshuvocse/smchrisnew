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
  public  class InterviewBoardSetupReportDAL
    {

        private ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        public DataTable GetMaster(int id)
        {
            try
            {
                string query = @"SELECT com.ShortName, Dpt.DepartmentName, Fin.FinancialYearDesc, Job.CirculationStartDate, Job.CirculationsdeadlineDate,
 (Req.ReqCode+ ' : '+ Req.JobTitle) AS JobTitle,
 Mast.Vanue, Mast.InterviewDate, Mast.InterviewFromTime, Mast.InterviewToTime, Mast.Remarks  FROM  tblInterviewBoardSetupMaster Mast
LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = Mast.CompanyId
LEFT JOIN dbo.tblJobCreation Job ON mast.JobTitleId=Job.JobID
LEFT JOIN dbo.tblJobReqForm Req ON Job.ReqCodeId=Req.JobReqId
LEFT JOIN dbo.tblDepartment Dpt ON Req.DeptId=Dpt.DepartmentId
LEFT JOIN dbo.tblFinancialYear Fin ON Req.FinYearId=Fin.FinancialYearId WHERE Mast.IsActive=1 AND
 Mast.SetupMasterId=" + id + "";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public DataTable GetDetails(int id)
        {
            try
            {
                string query = @"SELECT Emp.EmpMasterCode, CASE
    WHEN dtls.MemberType =1 THEN 'SMC'
    WHEN dtls.MemberType =2  THEN 'SMC EL'
    WHEN dtls.MemberType =3  THEN 'Other'
  END as ShortName,  * FROM  dbo.tblInterviewBoardSetupDetails dtls
LEFT JOIN dbo.tblEmpGeneralInfo Emp ON Emp.EmpInfoId = dtls.EmployeeId  
WHERE dtls.MasterId=" + id + "";

                return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
