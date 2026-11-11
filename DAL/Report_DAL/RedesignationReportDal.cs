using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DAL.InternalCls;

namespace DAL.Report_DAL
{
    public class RedesignationReportDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public void LoadCompanyDropDownList(DropDownList ddl)
        {
            try
            {
                string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
                aCommonInternalDal.LoadDropDownValueCompany(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
            }
            catch (Exception)
            {

                //throw;
            }
        }

        public DataTable GetAllDivision(string compId)
        {
            string queryStr = @"SELECT * FROM dbo.tblDivision  WITH (NOLOCK) WHERE IsActive='1' AND CompanyId='" + compId + "'";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllWing(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblDivisionWing  WITH (NOLOCK) 
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
  WHERE tblDivisionWing.IsActive='1' AND (Invisible='0' OR Invisible IS NULL) " + param + " ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllDepartment(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblDepartment  WITH (NOLOCK) 
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
WHERE tblDepartment.IsActive='1' AND (tblDepartment.Invisible='0' OR tblDepartment.Invisible IS NULL) " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllSection(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblSection  WITH (NOLOCK) 
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
WHERE tblSection.IsActive='1' AND (tblSection.Invisible='0' OR tblSection.Invisible IS NULL) " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllSubSection(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblSubSection  WITH (NOLOCK) 
LEFT JOIN dbo.tblSection ON tblSection.SectionId = tblSubSection.SectionId
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
WHERE tblSubSection.IsActive='1'  " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable LoadInforedesignationDAL(string param)
        {
            List<SqlParameter> aSqlParameters = new List<SqlParameter>();

            aSqlParameters.Add(new SqlParameter("@Pram", param));

            return aCommonInternalDal.GetDataByStoreProcedure("sp_AccountsIntegrationRedesignation", aSqlParameters, "HRDB");

//            string queryStr = @"SELECT com.ShortName,  SG.GradeCode+' : '+SG.GradeName Grade , EG.EmpMasterCode,EG.EmpName,  NDEs.Designation NDesignation,  PDEG.Designation PDesignation,   NDEG.DepartmentName , EPE.Effectivedate,  * From tblEmployeeReDesignation EPE with (nolock)
// inner JOIN dbo.tblEmpGeneralInfo  EG ON EPE.EmployeeId = EG.EmpInfoId
//
//  left JOIN dbo.tblDesignation  PDEG ON EPE.PDesignationId = PDEG.DesignationId
//  left JOIN dbo.tblDesignation  NDEs ON EPE.NDesignationId = NDEs.DesignationId
// 
// 
//  left JOIN dbo.tblDepartment  NDEG ON EPE.DepartmentId = NDEG.DepartmentId
//  
// 
//  
//    left JOIN  dbo.tblCompanyInfo com ON com.CompanyId = EPE.CompanyId 
//	                            LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
//	                           	where EPE.EmployeeReDesignationId is not null
//						 " + param + "  ORDER BY EG.EmpMasterCode ASC";
//            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
//            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

    }
}
