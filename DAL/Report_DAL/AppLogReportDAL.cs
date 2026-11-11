using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.COMMON_DAL
{
    public class AppLogReportDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


        public DataTable GetEmpAppLogInfo(string companyId,string fromdate,string todate,string MainTableName,string AppLogTableName,string empColName,string maintableid)
        {

            string query = @"SELECT (ME.EmpName+':'+ME.EmpMasterCode+':'+ISNULL(DS.Designation,'')+':'+ISNULL(DP.DepartmentName,''))Info,PreEmp.EmpName AS FromEmp,
            ForEmp.EmpName AS ToEmp,A.ActionStatus,A.Version,A.ApproveDate FROM dbo."+AppLogTableName+" A "+
            " LEFT JOIN dbo.tblEmpGeneralInfo PreEmp ON PreEmp.EmpInfoId=A.PreEmpInfoId "+
            " LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=A.ForEmpInfoId "+
            " LEFT JOIN dbo."+MainTableName+" M ON M."+maintableid+"=A."+maintableid+" "+
            " LEFT JOIN dbo.tblEmpGeneralInfo ME ON ME.EmpInfoId=M."+empColName+" "+
            " LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId=ME.DesignationId "+
            " LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId=ME.DepartmentId "+
            " WHERE A.ActionStatus<>'Drafted' AND ME.CompanyId='"+companyId+"' AND M.EntryDate BETWEEN '"+fromdate+"' AND '"+todate+"'";
            return aCommonInternalDal.DataContainerDataTable(query,  DataBase.HRDB);
        }
        public DataTable GetOtherAppLogInfo(string companyId, string finyearId,string MainTableName, string AppLogTableName,  string maintableid,string maincol,string deptCol)
        {

            string query = @"SELECT (M." + maincol + "+':'+" + (string.IsNullOrEmpty(deptCol) ? "" : " DP.DepartmentName+':'+ ") + " FN.FinancialYearDesc)Info, " +
                    " PreEmp.EmpName AS FromEmp,ForEmp.EmpName AS ToEmp,A.ActionStatus,A.Version,A.ApproveDate FROM dbo."+AppLogTableName+" A "+
                    " LEFT JOIN dbo.tblEmpGeneralInfo PreEmp ON PreEmp.EmpInfoId=A.PreEmpInfoId "+
                    " LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=A.ForEmpInfoId "+
                    " LEFT JOIN dbo."+MainTableName+" M ON M."+maintableid+"=A."+maintableid+" "+
                    " LEFT JOIN dbo.tblFinancialYear FN ON FN.FinancialYearId = M.FinancialYearId "+
                    (string.IsNullOrEmpty(deptCol) ? "" : " LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId=M." + deptCol + " ") +
                    
                    " WHERE A.ActionStatus<>'Drafted' AND M.CompanyId='"+companyId+"' AND M.FinancialYearId='"+finyearId+"'";
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }
        public DataTable GetFinyear(string companyId, string finyearId)
        {

            string query = @"SELECT  * FROM dbo.tblFinancialYear WHERE CompanyId='"+companyId+"' AND FinancialYearId='"+finyearId+"'";
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

    }
}
