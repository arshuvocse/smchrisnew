using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DAL.InternalCls;

namespace DAL.Permission_DAL
{
    public class PermissionDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public DataTable GetPermissionForUser(string userId,string pageName)
        {
            string query = @"SELECT tblMenuGroupPermission.UserId,tblMenuGroupPermission.CompanyId,tblMenuGroupSetupDetail.[Add],tblMenuGroupSetupDetail.Edit,tblMenuGroupSetupDetail.[View],tblMenuGroupSetupDetail.[Delete],URL,UserTypeId,* FROM dbo.tblMenuGroupPermission
INNER JOIN dbo.tblMenuGroupPermissionDtl ON tblMenuGroupPermissionDtl.MenuGroupPermissionId = tblMenuGroupPermission.MenuGroupPermissionId
INNER JOIN dbo.tblMenuGroupSetup ON tblMenuGroupSetup.MenuGroupSetupId = tblMenuGroupPermissionDtl.MenuGroupSetupId
INNER JOIN dbo.tblMenuGroupSetupDetail ON tblMenuGroupSetupDetail.MenuGroupSetupId = tblMenuGroupPermissionDtl.MenuGroupSetupId
INNER JOIN dbo.tblMainMenu ON tblMainMenu.MainMenuId = tblMenuGroupSetupDetail.MainMenuId
INNER JOIN dbo.tblUser ON tblUser.UserId = tblMenuGroupPermission.UserId
WHERE tblMenuGroupPermission.UserId='" + userId + "' AND URL='" + pageName + "'  AND tblMenuGroupPermissionDtl.IsActive=1    order by   tblMenuGroupSetupDetail.MenuGroupSetupDetailId desc  ";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable GetUserCompany(string userId)
        {
            string query = @"SELECT * FROM dbo.tblUser
INNER JOIN dbo.tblUserCompanyMaping ON tblUserCompanyMaping.UserId = tblUser.UserId
INNER JOIN dbo.tblCompanyInfo ON tblCompanyInfo.CompanyId = tblUserCompanyMaping.CompanyId WHERE tblUser.UserId='" + userId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        
        public DataTable GetUserCompanyHC(string userId)
        {
            string query = @"SELECT * FROM dbo.tblUser
INNER JOIN dbo.tblUserCompanyMaping ON tblUserCompanyMaping.UserId = tblUser.UserId
INNER JOIN dbo.tblCompanyInfo ON tblCompanyInfo.CompanyId = tblUserCompanyMaping.CompanyId WHERE tblUser.UserId='" + userId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetUserSuperAdminTrueorFalse(string userId)
        {
            string query = @"SELECT tblUserType.UserType,tblUser.UserTypeId FROM dbo.tblUser
LEFT JOIN dbo.tblUserType ON dbo.tblUser.UserTypeId=dbo.tblUserType.UserTypeId
  WHERE  dbo.tblUser.UserId='" + userId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable GetCompany()
        {
            string query = @"SELECT * FROM dbo.tblCompanyInfo";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable LoadDepartmentByWings(string CompanyId)
        {
            string query = "SELECT * FROM dbo.tblDepartment WHERE CompanyId='" + CompanyId + "' AND DepartmentId IN (SELECT DepartmentId FROM dbo.tblUserDepartmentPermission  WHERE IsActive=1 and UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
    }
}
