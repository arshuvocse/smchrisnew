using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;

namespace DAL.Appraisal
{

       
   public class KPIInformationViewDAL
    {
        ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        DataAccessManager _aAcessManager = new DataAccessManager();

        public void LoadDept(DropDownList ddl, string compId)
        {
            string query = @"SELECT * FROM dbo.tblDepartment WHERE Invisible IS NULL and CompanyId='" + compId + "'";
            _aCommonInternalDal.LoadDropDownValue(ddl, "DepartmentName", "DepartmentId", query, DataBase.HRDB);
        }

        public DataTable GetFianncialYearByComIdDDl(int id)
        {
            string query = @"SELECT FinancialYearId as Value,FinancialYearDesc as TextField FROM tblFinancialYear where CompanyId =" + id + " and Status ='Active' ";
            return _aCommonInternalDal.GetDTforDDL(query, null, DataBase.HRDB);


        }
       public DataTable GetAppraisalByKpiPermission(string companyId,  string param )
       {
           try
           {
               string query = @"SELECT  e.EmpInfoId ,
        ( e.EmpMasterCode + ':' + e.EmpName + ':' + desg.Designation ) employee ,
        dpt.DepartmentName ,
        c.CompanyName ,
        a.FinancialYearId,
        y.FinancialYearDesc,
		app.IsApprove
        FROM    dbo.tblKpiDeadlineMaster A
        LEFT JOIN dbo.tblKPIDeadLineDetails b ON A.KPIDeadLineMasterId = b.KPIDeadLineMasterId
        LEFT JOIN dbo.tblEmpGeneralInfo e ON b.EmpinfoId = e.EmpInfoId
        LEFT JOIN tblCompanyInfo c ON e.CompanyId = c.CompanyId
        LEFT JOIN tblDesignation desg ON e.DesignationId = desg.DesignationId
        LEFT JOIN tblFinancialYear y ON A.FinancialYearId = y.FinancialYearId
        LEFT JOIN tblDepartment dpt ON e.DepartmentId = dpt.DepartmentId
 INNER JOIN dbo.tblAppraisalSelfMaster SMAster ON B.EmpinfoId = SMAster.EmpinfoId
		LEFT JOIN dbo.tblAppraisalMaster app ON e.EmpInfoId = app.EmpInfoId AND a.FinancialYearId = app.FinancialYearId where A.CompanyId = " + companyId + "  " + param + "";

               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }



       }

       public DataTable GetEmployeeForKpiSetUpNew(string companyId,  string param)
       {
           try
           {
               string query = @"select A.EmpInfoId, A.EmpMasterCode , A.EmpName , desg.Designation ,dpt.DepartmentName , div.DivisionName    from tblEmpGeneralInfo A " +
                              "left join tblDivision div on a.DivisionId = div.DivisionId " +
                              "left join tblDepartment dpt on a.DepartmentId = dpt.DepartmentId " +
                              "left join tblDesignation desg on a.DesignationId = desg.DesignationId where A.CompanyId = " + companyId + " and a.IsActive=1 " + param + "";
               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
    }
}
