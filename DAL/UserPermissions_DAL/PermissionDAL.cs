using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DAL.InternalCls;
using DAO.Panal_Entities;
using ObjUserWiseApprovalPermission = DAO.UA_DAO.ObjUserWiseApprovalPermission;

namespace DAL.UserPermissions_DAL
{
    public class PermissionDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public DataTable SubMenus(string parentId)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ID", parentId));

            string query = @"select * from tblMainmenu where ParantId =@ID";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, "HRDB");
        }
        public DataTable MainMenus()
        {


            string query = @"select * from tblMainmenu where ParantId is null";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public void UserDropDown(DropDownList aDropDownList)
        {
            string query = "select * from tblUser ";
            aCommonInternalDal.LoadDropDownValue(aDropDownList, "UserName", "UserId", query, "HRDB");
        }

        public bool UpdateAll(string columnname,string status,string id)
        {
            string query = "update tblMenuDistribution set "+columnname+"='"+status+"' where SL='"+id+"'";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query,  "HRDB");
        }
        public bool DeleteData(string  id)
        {
            string query = "delete from  tblMenuDistribution where MenuSL='" + id + "'";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, "HRDB");
        }
        public int SaveMenuDist(ObjUserWiseApprovalPermission  aObjPanal)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@UserId", aObjPanal.UserId));
            aSqlParameterlist.Add(new SqlParameter("@MenuSL", aObjPanal.ManuSL));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", "True"));
            //aSqlParameterlist.Add(new SqlParameter("@Add", aObjPanal.Add));
            //aSqlParameterlist.Add(new SqlParameter("@View", aObjPanal.View));
            //aSqlParameterlist.Add(new SqlParameter("@Edit", aObjPanal.Edit));
            //aSqlParameterlist.Add(new SqlParameter("@Delete", aObjPanal.Delete));
            //aSqlParameterlist.Add(new SqlParameter("@Everyone", aObjPanal.Everyone));
            //aSqlParameterlist.Add(new SqlParameter("@Own", aObjPanal.Own));
            string query = @"insert into tblMenuDistribution (UserId,MenuSL,IsActive) values(@UserId,@MenuSL,@IsActive)";
            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, "HRDB");
        }
    }
}
