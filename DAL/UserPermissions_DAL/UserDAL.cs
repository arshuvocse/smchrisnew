using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO_EF;

namespace DAL.UserPermissions_DAL
{
    public class UserDAL
    {
        private ClsCommonInternalDAL _commonInternalDal = new ClsCommonInternalDAL();

        public DataTable GetUserDepartmentPermissionByUserId(int UserId)
        {
            //List<tblDepartment> lDept = new List<tblDepartment>();
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@UserId", UserId));

            string query = @"SELECT d.DepartmentId AS Value,d.DepartmentName AS TextField,ISNULL(ud.IsActive,0) IsActive,ud.UserId FROM dbo.tblDepartment d 
                            LEFT JOIN dbo.tblUserDepartmentPermission ud ON ud.DepartmentId = d.DepartmentId where ud.UserId=@UserId";

            return _commonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
            //using (DataTable dt = _commonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB))
            //{
            //    if (dt.Rows.Count>0)
            //    {
            //        for (int i = 0; i < dt.Rows.Count; i++)
            //        {
            //            tblDepartment dept = new tblDepartment();
            //            dept.DepartmentId = int.Parse(dt.Rows[i]["DepartmentId"].ToString());
            //            dept.DepartmentName = dt.Rows[i]["DepartmentName"].ToString();
            //            dept.IsActive = bool.Parse(dt.Rows[i]["IsActive"].ToString());
            //            lDept.Add(dept);
            //        }
            //    }
                
            //}
            //return lDept;
        }
    }
}
