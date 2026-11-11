using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.InternalCls;
using DAO.UA_DAO;

namespace DAL.UserPermissions_DAL
{
    public class MenuDataPermissionDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int SaveSupervisorApp(MenuDataPermissionDAO appDao)
        {
            //if (appDao.SuperMenuAppId == 0)
            {
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                aSqlParameterlist.Add(new SqlParameter("@UserId", appDao.UserId));
                aSqlParameterlist.Add(new SqlParameter("@MainMenuId", appDao.MainMenuId));
                aSqlParameterlist.Add(new SqlParameter("@Own", appDao.Own));
                aSqlParameterlist.Add(new SqlParameter("@SMC", appDao.SMC));
                aSqlParameterlist.Add(new SqlParameter("@SMCEL", appDao.SMCEL));



                string query = @"INSERT INTO dbo.tblMenuDataPermission
                            (
                                UserId,
                                MainMenuId,
                                Own,
                                SMC,
                                SMCEL
                            )
                            VALUES
                            (    @UserId,
                                @MainMenuId,
                                @Own,
                                @SMC,
                                @SMCEL
                            )";
                return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, "HRDB");
            }
            

        }
        public DataTable GetUserDataPermission(string userId,string mainmenuid)
        {
            string query = @"SELECT * FROM tblMenuDataPermission WHERE UserId='"+userId+"' AND MainMenuId='"+mainmenuid+"'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public bool DeleteData(string  userId)
        {



            string query = @"DELETE FROM  dbo.tblMenuDataPermission WHERE UserId='"+userId+"' ";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query,  "HRDB");
        }


    }
}
