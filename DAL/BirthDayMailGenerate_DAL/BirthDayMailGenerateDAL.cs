using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.DataManager;
using DAL.InternalCls;

namespace DAL.BirthDayMailGenerate_DAL
{
  public  class BirthDayMailGenerateDAL
    {

        ClsCommonInternalDAL _commonInternalDal = new ClsCommonInternalDAL();

        public DataTable GetEMpInfos(string param)
        {
            string query = @"SELECT rptBody.EmpName Supervisor, SL.SalaryLocation Office,
JL.Location  Place,  EG.EmployeeStatus, EG.EmpInfoId, EG.EmpMasterCode, EG.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, div.DivisionName, * FROM dbo.tblEmpGeneralInfo EG
INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId 
left JOIN  dbo.tblDivision div ON div.DivisionId = EG.DivisionId  
left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = EG.DepartmentId 
left JOIN  dbo.tblDesignation desig ON desig.DesignationId = EG.DesignationId 
left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = EG.SalaryLoationId 
left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = EG.EmpTypeId 
 LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID =EG.JobLocationId 
LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG.ReportingEmpId
 WHERE  EG.IsActive=1  " + param + "  ORDER BY EG.EmpMasterCode ASC ";

            return _commonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetCompanyDDL()
        {
            string queryStr = @"SELECT CompanyId as Value,CompanyName, ShortName as TextField FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            //string query = @"SELECT CompanyId as Value, ShortName as TextField FROM tblCompanyInfo";
            return _commonInternalDal.GetDTforDDL(queryStr, null, DataBase.HRDB);
        }
    }
}
