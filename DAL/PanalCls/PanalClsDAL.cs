using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL.InternalCls;
using DAL.DataManager;

namespace DAL.PanalCls
{
    public class PanalClsDAL
    {
        private ClsCommonInternalDAL _aCommonInternalDal;
//        public DataTable Login(string loginName, string password)
//        {
//            _aCommonInternalDal = new ClsCommonInternalDAL();
//            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
//            aSqlParameterlist.Add(new SqlParameter("@loginName", loginName));
//            aSqlParameterlist.Add(new SqlParameter("@password", password));

//            string queryString = @" SELECT * FROM dbo.tblUser
//                                    LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblUser.EmpInfoId = dbo.tblEmpGeneralInfo.EmpInfoId where ((tblUser.Email=@loginName) OR (tblUser.ContactNo=@loginName) or (tblUser.UserName=@loginName) ) and Password=@password and tblUser.IsActive=1";
//            return _aCommonInternalDal.DataContainerDataTable(queryString, aSqlParameterlist, DataBase.HRDB);
//        }

        public DataTable Login(string loginName, string password)
        {
            _aCommonInternalDal = new ClsCommonInternalDAL();
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@loginName", SqlDbType.NVarChar, 256) { Value = loginName });
            aSqlParameterlist.Add(new SqlParameter("@password", SqlDbType.NVarChar, 4000) { Value = password });

            string queryString = @" SELECT isnull(isPassChanged,0) isPassChanged, tblEmpGeneralInfo.CompanyId,  tblEmpGeneralInfo.SalaryLoationId, tblUser.EmpInfoId,tblDesignation.Designation Designation,* FROM dbo.tblUser
                                    LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblUser.EmpInfoId = dbo.tblEmpGeneralInfo.EmpInfoId
 LEFT JOIN dbo.tblDesignation ON dbo.tblEmpGeneralInfo.DesignationId = dbo.tblDesignation.DesignationId  
where ((tblUser.Email=@loginName) OR (tblUser.ContactNo=@loginName) or (tblUser.UserName=@loginName) ) and tblUser.[Password]=@password and tblUser.IsActive=1";
            return _aCommonInternalDal.DataContainerDataTable(queryString, aSqlParameterlist, DataBase.HRDB);
        }
        public bool LoginLog(string userId, string LoginName, DateTime loginTime, string ipAddress, string browserName, string browserVersion, string operatingSystem)
        {
            _aCommonInternalDal = new ClsCommonInternalDAL();
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@UserId", userId));
            aSqlParameterlist.Add(new SqlParameter("@LoginName", LoginName));
            aSqlParameterlist.Add(new SqlParameter("@LoginTime", loginTime));

            aSqlParameterlist.Add(new SqlParameter("@ipAddress", ipAddress));
            aSqlParameterlist.Add(new SqlParameter("@browserName", browserName));
            aSqlParameterlist.Add(new SqlParameter("@browserVersion", browserVersion));
            aSqlParameterlist.Add(new SqlParameter("@operatingSystem", operatingSystem));


            string insertQuery = @"insert into tblLoginLog (UserId,LoginName,LoginTime,[ipAddress]
           ,[browserName]
           ,[browserVersion]
           ,[operatingSystem]) 
            values (@UserId,@LoginName,@LoginTime,@ipAddress 
           ,@browserName 
           ,@browserVersion 
           ,@operatingSystem)";

            return _aCommonInternalDal.SaveDataByInsertCommand(insertQuery, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable FindParant(string url)
        {
            _aCommonInternalDal = new ClsCommonInternalDAL();
            DataTable aTableMainMenu = new DataTable();
            string queryString = "SELECT * FROM dbo.tblMainMenu WHERE URL='"+url+"'";
            aTableMainMenu = _aCommonInternalDal.DataContainerDataTable(queryString, DataBase.HRDB);
            return aTableMainMenu;
        }
        public DataTable MainMenu()
        {
            _aCommonInternalDal = new ClsCommonInternalDAL();
            DataTable aTableMainMenu = new DataTable();
            string queryString = "select * from tblMainMenu where ParantId is null or ParantId='' order by SL asc";
            aTableMainMenu = _aCommonInternalDal.DataContainerDataTable(queryString, DataBase.HRDB);
            return aTableMainMenu;
        }
        public DataTable MenuSL(string sl)
        {
            _aCommonInternalDal = new ClsCommonInternalDAL();
            DataTable aTableMainMenu = new DataTable();
            string queryString = "select * from tblMainMenu where SL='"+sl+"'";
            aTableMainMenu = _aCommonInternalDal.DataContainerDataTable(queryString, DataBase.HRDB);
            return aTableMainMenu;
        }
        public DataTable MainMenu(int userId)
        {
            _aCommonInternalDal = new ClsCommonInternalDAL();
            DataTable aTableMainMenu = new DataTable();
            string queryString = @"select tblMainMenu.* from tblMainMenu 
WHERE SL IN (SELECT DISTINCT ParantId FROM dbo.tblMenuGroupPermission
left JOIN dbo.tblMenuGroupPermissionDtl ON tblMenuGroupPermissionDtl.MenuGroupPermissionId = tblMenuGroupPermission.MenuGroupPermissionId
INNER JOIN dbo.tblMenuGroupSetup ON tblMenuGroupSetup.MenuGroupSetupId = tblMenuGroupPermissionDtl.MenuGroupSetupId
INNER JOIN dbo.tblMenuGroupSetupDetail ON tblMenuGroupSetupDetail.MenuGroupSetupId = tblMenuGroupPermissionDtl.MenuGroupSetupId
INNER JOIN dbo.tblMainMenu ON tblMainMenu.MainMenuId = tblMenuGroupSetupDetail.MainMenuId
WHERE UserId='" + userId + "'  AND tblMenuGroupPermissionDtl.IsActive=1) AND ((ParantId is null) or (ParantId='')) order by tblMainMenu.SL ASC";
            aTableMainMenu = _aCommonInternalDal.DataContainerDataTable(queryString, DataBase.HRDB);
            return aTableMainMenu;
        }
        public DataTable SubItem(string Id)
        {
            DataTable aDataTableSubItem = new DataTable();
            _aCommonInternalDal = new ClsCommonInternalDAL();
            string queryString = "select * from tblMainMenu where ParantId='" + Id + "' AND IsInMainMenu='1' order by SL asc";
            aDataTableSubItem = _aCommonInternalDal.DataContainerDataTable(queryString, DataBase.HRDB);
            return aDataTableSubItem;
        }
        public DataTable SubItem(string Id, int userId)
        {
            DataTable aDataTableSubItem = new DataTable();
            _aCommonInternalDal = new ClsCommonInternalDAL();

            string queryString = @"select tblMainMenu.* from tblMainMenu 
                                    WHERE  ParantId='" + Id + "' AND MainMenuId IN (SELECT DISTINCT tblMenuGroupSetupDetail.MainMenuId FROM dbo.tblMenuGroupPermission " +
                        " INNER JOIN dbo.tblMenuGroupPermissionDtl ON tblMenuGroupPermissionDtl.MenuGroupPermissionId = tblMenuGroupPermission.MenuGroupPermissionId " +
                        " INNER JOIN dbo.tblMenuGroupSetup ON tblMenuGroupSetup.MenuGroupSetupId = tblMenuGroupPermissionDtl.MenuGroupSetupId " +
                        " INNER JOIN dbo.tblMenuGroupSetupDetail ON tblMenuGroupSetupDetail.MenuGroupSetupId = tblMenuGroupPermissionDtl.MenuGroupSetupId " +
                        " INNER JOIN dbo.tblMainMenu ON tblMainMenu.MainMenuId = tblMenuGroupSetupDetail.MainMenuId " +
                        " WHERE UserId='" + userId + "' AND tblMenuGroupPermissionDtl.IsActive=1  and tblMenuGroupSetupDetail.IsActive=1) AND IsInMainMenu='1' order by tblMainMenu.SL asc";

            aDataTableSubItem = _aCommonInternalDal.DataContainerDataTable(queryString, DataBase.HRDB);
            return aDataTableSubItem;
        }
        public DataTable SubSubItem(string Id)
        {
            DataTable aDataSubSubItem = new DataTable();
            _aCommonInternalDal = new ClsCommonInternalDAL();
            string queryString = "select * from tblMainMenu where ParantId='" + Id + "' AND IsInMainMenu='1' order by SL asc";
            aDataSubSubItem = _aCommonInternalDal.DataContainerDataTable(queryString, DataBase.HRDB);
            return aDataSubSubItem;
        }
        public DataTable SubSubItem(string Id, int userId)
        {
            DataTable aDataSubSubItem = new DataTable();
            _aCommonInternalDal = new ClsCommonInternalDAL();
            string queryString = @"select tblMainMenu.* from tblMainMenu WHERE  ParantId='" + Id + "' AND MainMenuId IN (SELECT DISTINCT tblMenuGroupSetupDetail.MainMenuId FROM dbo.tblMenuGroupPermission " +
                        " INNER JOIN dbo.tblMenuGroupPermissionDtl ON tblMenuGroupPermissionDtl.MenuGroupPermissionId = tblMenuGroupPermission.MenuGroupPermissionId " +
                        " INNER JOIN dbo.tblMenuGroupSetup ON tblMenuGroupSetup.MenuGroupSetupId = tblMenuGroupPermissionDtl.MenuGroupSetupId " +
                        " INNER JOIN dbo.tblMenuGroupSetupDetail ON tblMenuGroupSetupDetail.MenuGroupSetupId = tblMenuGroupPermissionDtl.MenuGroupSetupId " +
                        " INNER JOIN dbo.tblMainMenu ON tblMainMenu.MainMenuId = tblMenuGroupSetupDetail.MainMenuId " +
                        " WHERE tblMenuGroupSetupDetail.IsActive=1 AND tblMenuGroupPermissionDtl.IsActive=1  and  UserId='" + userId + "') AND IsInMainMenu='1' order by tblMainMenu.SL asc";
            aDataSubSubItem = _aCommonInternalDal.DataContainerDataTable(queryString, DataBase.HRDB);
            return aDataSubSubItem;
        }
        public DataTable SubSubChildItem(string Id)
        {
            DataTable aDataSubSubChildItem = new DataTable();
            _aCommonInternalDal = new ClsCommonInternalDAL();
            string queryString = "select * from tblMainMenu where ParantId='" + Id + "' order by SL asc";
            aDataSubSubChildItem = _aCommonInternalDal.DataContainerDataTable(queryString, DataBase.HRDB);
            return aDataSubSubChildItem;
        }
        public DataTable SubSubChildItem(string Id, int userId)
        {
            DataTable aDataSubSubChildItem = new DataTable();
            _aCommonInternalDal = new ClsCommonInternalDAL();
            string queryString = @"select tblMainMenu.* from tblMainMenu " +
                                    " INNER JOIN dbo.tblMenuDistribution ON dbo.tblMainMenu.SL = dbo.tblMenuDistribution.MenuSL " +
                                    " WHERE UserId='" + userId + "' AND ParantId='" + Id + "' order by tblMainMenu.SL asc";
            aDataSubSubChildItem = _aCommonInternalDal.DataContainerDataTable(queryString, DataBase.HRDB);
            return aDataSubSubChildItem;
        }
    }
}
