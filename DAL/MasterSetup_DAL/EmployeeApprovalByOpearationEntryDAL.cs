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

namespace DAL.MasterSetup_DAL
{
  public  class EmployeeApprovalByOpearationEntryDAL
    {
       // private ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public void GetCompanyListShortNameIntoDropdown(DropDownList ddl)
        {

            string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
        }

        public Int32 SaveInfo(EmployeeApprovalByOpearationMasterDao aEmployeePromotionEntryDAO)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aEmployeePromotionEntryDAO.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aEmployeePromotionEntryDAO.EntryDate));
           
            string insertQuery = @"INSERT INTO [dbo].[tblEmployeeApprovalByOpearationMaster]
           ([EntryBy]
           ,[EntryDate]
           )
     VALUES
           (@EntryBy,  
           @EntryDate
		   )";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

      public bool DeleteEmpTransferAndRedesignationDSSaveInfo(string id, string com)
      {
         // var aSqlParameterlist = new List<SqlParameter>();

          //aSqlParameterlist.Add(new SqlParameter("@Operation", aEmpTransferAndDao.Operation));
          //aSqlParameterlist.Add(new SqlParameter("@CompanyId", aEmpTransferAndDao.CompanyId));
          //aSqlParameterlist.Add(new SqlParameter("@Deleteby", HttpContext.Current.Session["UserId"].ToString()));
          //aSqlParameterlist.Add(new SqlParameter("@DeleteDate", DateTime.Now));
          bool result = false;
          string query = @"Delete from tblEmployeeApprovalByOpearationDetail where  Operation =" + id + " and  CompanyId = '" + com + "'";
             aCommonInternalDal.DeleteDataByDeleteCommand(query, DataBase.H);
            return result;
          //return aCommonInternalDal.DeleteDataByDeleteCommand("DeleteHRApprovalEmployee", aSqlParameterlist, DataBase.HRDB);
      }

      public Int32 EmpTransferAndRedesignationDSSaveInfo(EmployeeApprovalByOpearationDetails aEmpTransferAndDao)
        {
    
            

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MasterId", aEmpTransferAndDao.MasterId));
            aSqlParameterlist.Add(new SqlParameter("@Operation",  aEmpTransferAndDao.Operation));
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aEmpTransferAndDao.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aEmpTransferAndDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@Serial", aEmpTransferAndDao.Serial));
            aSqlParameterlist.Add(new SqlParameter("@Isheadperson", aEmpTransferAndDao.Isheadperson));

            string insertQuery = @"INSERT INTO [dbo].[tblEmployeeApprovalByOpearationDetail]
           ([MasterId]
           ,[Operation]
           ,[CompanyId]
           ,[EmpInfoId],[Serial], Isheadperson)
     VALUES
           (@MasterId, 
           @Operation, 
           @CompanyId,  
           @EmpInfoId,@Serial, @Isheadperson)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

      public void MenuDropDown(DropDownList ddl)
      {
          string queryStr = @"SELECT * FROM dbo.tblMainMenu WHERE IsApprovalPage='1' ";

          aCommonInternalDal.LoadDropDownValue(ddl, "MenuName", "MainMenuId", queryStr, DataBase.HRDB);
      }

      public void MenuDropDownNew(DropDownList ddl)
      {
          string queryStr = @"SELECT * FROM dbo.tblMainMenu WHERE IsApprovalPage='1' AND SL NOT IN (223,321,322)";

          aCommonInternalDal.LoadDropDownValue(ddl, "MenuName", "MainMenuId", queryStr, DataBase.HRDB);
      }
        public DataTable GetManpowerBudgetListInfo()
        {
            var aSqlParameterlist = new List<SqlParameter>();



            string queryStr = @"SELECT bm.ApprovalStatus, * FROM dbo.tblMPBudgetMaster bm
								
								LEFT JOIN dbo.tblCompanyInfo c ON c.CompanyId = bm.CompanyId
                                LEFT JOIN dbo.tblDepartment d ON d.DepartmentId=bm.DepartmentId
                                LEFT JOIN dbo.tblFinancialYear fy ON fy.FinancialYearId = bm.FinancialYearId WHERE

									bm.ActionStatus='Posted' ";

            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetOperationInfo(string id,string com)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            string queryStr = @"SELECT MenuName AS operation,* FROM tblEmployeeApprovalByOpearationMaster m
                              INNER JOIN tblEmployeeApprovalByOpearationDetail d ON m.EmployeeApprovalByOpearationMasterId=d.MasterId
  INNER JOIN dbo.tblMainMenu ON d.operation=tblMainMenu.MainMenuId
     INNER JOIN dbo.tblCompanyInfo ON d.CompanyId=tblCompanyInfo.CompanyId
INNER JOIN dbo.tblEmpGeneralInfo ON d.EmpInfoId=tblEmpGeneralInfo.EmpInfoId
                              WHERE operation =" + id + " and  d.CompanyId = '" + com + "'";

            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
    }
}
