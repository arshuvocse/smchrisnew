using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataManager;
using DAL.InternalCls;

namespace DAL
{
    public class userDal
    {
        ClsCommonInternalDAL _commonInternalDal = new ClsCommonInternalDAL();



        public bool ResetPass(int UserId, string Pass)
        {

            try
            {
                int pk = 0;

                if (UserId > 0)
                {
                    List<SqlParameter> aParameters = new List<SqlParameter>();
                    aParameters.Add(new SqlParameter("@UserId", UserId));
                    aParameters.Add(new SqlParameter("@Password", Pass.Trim()));


                    string query =
                        @"update tblUser set Password=@Password where UserId=@UserId";

                    bool result = _commonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                    return result;

                }

            }
            catch (Exception exception)
            {

                throw exception;
            }
            return true;
        }




        public DataTable GetHrDeadlineExtendedEntryList(string param)
        {

            try
            {
                string query = @"Select m.DeadlineExtensionRequestId, m.Operation, m.Description, m.Remarks, Com.CompanyName, Fy.FinancialYearDesc, dt.DepartmentName, E.EmpMasterCode,
E.EmpName, Dg.Designation, d.ExtensionDate, D.ApprovalStatus from tblDeadlineExtensionRequest m
LEFT JOIN tblDeadlineExtensionRequestDetails D On D.DeadlineExtensionRequestId = m.DeadlineExtensionRequestId
LEFT JOIN tblEmpGeneralInfo E on E.EmpInfoId = D.EmployeeId 
LEFT JOIN tblDesignation Dg on Dg.DesignationId = E.DesignationId
LEFT JOIN tblCompanyInfo Com on Com.CompanyId = m.CompanyId
LEFT JOIN tblDepartment dt on dt.DepartmentId = m.DepartmentId
LEFT JOIn tblFinancialYear Fy on Fy.FinancialYearId = m.FinYearId
where m.DeadlineExtensionRequestId is not null " + param;
                return _commonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
            }
            catch (Exception exception)
            {

                throw exception;
            }
        }

    }
}
