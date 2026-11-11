using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;

namespace DAL.Report_DAL
{
    public class TrainingAttendanceReportDal
    {
        ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        public void TrainingRecordDropDown(DropDownList ddl, string companyId, string finYearId)
        {
            string queryStr = @"SELECT TrainingTitle+' [ S.Date: '+ FORMAT(StartDate,'dd-MMM-yyy') + ' to E.Date: '+FORMAT(EndDate, 'dd-MMM-yyyy')+']' AS TrainingTitle, TrainingRecordMasterId  FROM dbo.tblTrainingRecordMaster WHERE   ( IsDelete IS NULL OR IsDelete = 0)   AND CompanyId='" + companyId + "' AND FinancialYearId='" + finYearId + "'";
            _aCommonInternalDal.LoadDropDownValue(ddl, "TrainingTitle", "TrainingRecordMasterId", queryStr, DataBase.HRDB);
        }

        public DataTable GetTrainingAttendanceReport(string generateParameter)
        {
            string query = @"SELECT TANDC.TrainingAttId,CI.CompanyName,FNY.FinancialYearDesc,MRKSM.TrainingTitle,EGI.EmpName,  EGI.EmpMasterCode, DSG.Designation,DPT.DepartmentName,TANDC.ATTDate,
                            CASE WHEN TANDC.IsPresent = 1 THEN 'Present' WHEN  TANDC.IsPresent = 0 THEN 'Absent' END AS Status
                            FROM dbo.tblTrainingAttendance AS TANDC
                            LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON EGI.EmpInfoId = TANDC.EmpInfoId
                            LEFT JOIN dbo.tblDesignation AS DSG ON DSG.DesignationId = EGI.DesignationId
                            LEFT JOIN dbo.tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
                            LEFT JOIN dbo.tblTrainingRecordMaster AS MRKSM ON MRKSM.TrainingRecordMasterId = TANDC.TrainingRecordMasterId
                            LEFT JOIN dbo.tblFinancialYear AS FNY ON FNY.FinancialYearId = MRKSM.FinancialYearId
                            LEFT JOIN dbo.tblCompanyInfo AS CI ON CI.CompanyId = MRKSM.CompanyId 
							WHERE TANDC.TrainingAttId IS NOT NULL " + generateParameter + " ORDER BY EGI.EmpMasterCode";
            return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);
        }
    }
}
