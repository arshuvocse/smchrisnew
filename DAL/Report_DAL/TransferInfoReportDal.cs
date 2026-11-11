using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.Report_DAL
{
    public class TransferInfoReportDal
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

        public void LoadFinancialYear(DropDownList ddl, string companyId)
        {
            string query = @"SELECT FNY.FinancialYearId,
                             FNY.FinancialYearDesc FROM dbo.tblFinancialYear AS FNY WHERE FNY.Status = 'Active' AND FNY.CompanyId =" + companyId;
            aCommonInternalDal.LoadDropDownValue(ddl, "FinancialYearDesc", "FinancialYearId", query, DataBase.HRDB);
        }

        public void LoadPromotionTypeDropDownList(DropDownList ddl)
        {
            string query = "SELECT * FROM tblPromotionType";
            aCommonInternalDal.LoadDropDownValue(ddl, "PromotionTypeName", "PromotionTypeId", query, "HRDB");
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

        public DataTable LoadInfoPromotionalInfoDAL(string param)
        {
            List<SqlParameter> aSqlParameters = new List<SqlParameter>();
            aSqlParameters.Add(new SqlParameter("@Pram", param));
            return aCommonInternalDal.GetDataByStoreProcedure("sp_AccountsIntegrationTransfer", aSqlParameters, "HRDB");
        }

        public Int32 SaveSeperationConfirmationList(TransferConfirmationDao aListDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aListDao.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@EmpMasterCode", aListDao.EmpMasterCode));
            aSqlParameterlist.Add(new SqlParameter("@ZID", aListDao.ZID));
            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", aListDao.EffectiveDate));
            aSqlParameterlist.Add(new SqlParameter("@SalaryLocationId", aListDao.SalaryLocationId));
            aSqlParameterlist.Add(new SqlParameter("@JobLocationId", aListDao.JobLocationId));
            aSqlParameterlist.Add(new SqlParameter("@Approveby", aListDao.Approveby));
            aSqlParameterlist.Add(new SqlParameter("@ApproveDate", aListDao.ApproveDate));


            string query = @"INSERT INTO tblAccountsIntegration_TransferList
                           (EmpInfoId
           ,EmpMasterCode
           ,ZID
           ,EffectiveDate
           ,SalaryLocationId
           ,JobLocationId
           ,Approveby
           ,ApproveDate
           )
           VALUES
           (
            @EmpInfoId
           ,@EmpMasterCode
           ,@ZID
           ,@EffectiveDate
           ,@SalaryLocationId
           ,@JobLocationId
           ,@Approveby
           ,@ApproveDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }
    }
}
