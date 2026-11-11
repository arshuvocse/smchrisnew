using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DAL.MAIN_FUNCTION;
using DAO.UA_DAO;

namespace DAL.UA_DAL
{
    public class UserAuthentication2DAL
    {
        DB_Manager aDbManager = new DB_Manager();
        public void CreateConnection_UA_DB_DAL()
        {
            aDbManager.CreateConnection(DB_Names.SR_DB);
        }

        public void CloseAllConnection_DAL()
        {
            aDbManager.CloseConnection();
        }

        public Int32 SaveUserInformationDal(UserInformationDAO aUserInformationDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@UserName", aUserInformationDao.UserName));
            aSqlParameterlist.Add(new SqlParameter("@UserType", aUserInformationDao.UserType));
            aSqlParameterlist.Add(new SqlParameter("@EmpMasterCode", aUserInformationDao.EmpMasterCode));
            aSqlParameterlist.Add(new SqlParameter("@LoginName", aUserInformationDao.LoginName));
            aSqlParameterlist.Add(new SqlParameter("@Password", aUserInformationDao.Password));
            aSqlParameterlist.Add(new SqlParameter("@UserStatus", aUserInformationDao.UserStatus));
            aSqlParameterlist.Add(new SqlParameter("@Email", aUserInformationDao.Email));
            aSqlParameterlist.Add(new SqlParameter("@ContactNo", aUserInformationDao.ContactNo));

            return aDbManager.SaveAction("sp_I_UserInformation", aSqlParameterlist, "@UserId");
        }
        public bool UpdateUserInformationDal(UserInformationDAO aUserInformationDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@UserId", aUserInformationDao.UserId));
            aSqlParameterlist.Add(new SqlParameter("@UserName", aUserInformationDao.UserName));
            aSqlParameterlist.Add(new SqlParameter("@UserType", aUserInformationDao.UserType));
            aSqlParameterlist.Add(new SqlParameter("@EmpMasterCode", aUserInformationDao.EmpMasterCode));
            aSqlParameterlist.Add(new SqlParameter("@LoginName", aUserInformationDao.LoginName));
            aSqlParameterlist.Add(new SqlParameter("@Password", aUserInformationDao.Password));
            aSqlParameterlist.Add(new SqlParameter("@UserStatus", aUserInformationDao.UserStatus));
            aSqlParameterlist.Add(new SqlParameter("@Email", aUserInformationDao.Email));
            aSqlParameterlist.Add(new SqlParameter("@ContactNo", aUserInformationDao.ContactNo));

            return aDbManager.UpdateAction("sp_UD_User", aSqlParameterlist);
        }
        public DataTable GetUserInfoDal(string search)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@UserName", search));

            return aDbManager.GetDataTableAction("sp_GET_AllUser", aSqlParameterlist);
        }
        public DataTable GetUserInfobyIdDal(int id)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@Userid", id));

            return aDbManager.GetDataTableAction("sp_GET_UserBy_ID", aSqlParameterlist);
        }
        public DataTable LoadEmployee(string EmpMasterCode)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@Id", EmpMasterCode));

            return aDbManager.GetDataTableAction("sp_GET_EmployeeDetail", aSqlParameterlist);
        }

        public DataTable LoginDAL(string loginName, string password)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@loginName", loginName));
            aSqlParameterlist.Add(new SqlParameter("@Password", password));
            return aDbManager.GetDataTableAction("sp_GET_UserInfo", aSqlParameterlist);
        }
        public bool DeleteDataDal(UserInformationDAO aUserInformationDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@UserId", aUserInformationDao.UserId));

            return aDbManager.DeleteAction("sp_DEL_User", aSqlParameterlist);
        }

        public DataTable MenuHTML_DAL(string userId)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            // aSqlParameterlist.Add(new SqlParameter("@userId", userId));
            aSqlParameterlist.Add(new SqlParameter("@userId", userId)); //temporary(Real)

            return aDbManager.GetDataTableAction("sp_GET_MenuHTML", aSqlParameterlist);
        }
        //
        public bool MenuSaveDal(UserAuthenticationDAO aUserAuthenticationDAO)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SL", aUserAuthenticationDAO.SL));
            aSqlParameterlist.Add(new SqlParameter("@ManuName", aUserAuthenticationDAO.ManuName));
            aSqlParameterlist.Add(new SqlParameter("@URL", aUserAuthenticationDAO.URL));
            aSqlParameterlist.Add(new SqlParameter("@ParantId", aUserAuthenticationDAO.ParantId));
            aSqlParameterlist.Add(new SqlParameter("@IsApprovalPage", aUserAuthenticationDAO.IsApprovalPage));
            aSqlParameterlist.Add(new SqlParameter("@ModuleId", aUserAuthenticationDAO.ModuleId));
            aSqlParameterlist.Add(new SqlParameter("@MenuModule", aUserAuthenticationDAO.MenuModule));

            return aDbManager.SaveAction("sp_I_MainMenu", aSqlParameterlist);
        }

        public bool MenuPermissionRemove(int sl, int userId)
        {
            //string parantId = Convert.ToString(sl);
            //string query = @"DELETE dbo.tblMenuDistribution WHERE MenuSL IN (SELECT SL FROM dbo.tblMainMenu WHERE SL =" + sl + " ) AND UserId=" + userId + "";
            //string query1 = @"DELETE dbo.tblMenuDistribution WHERE MenuSL IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId ='" + parantId + "' ) AND UserId=" + userId + "";
            //string query2 = @"DELETE dbo.tblMenuDistribution WHERE MenuSL IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId ='" + parantId + "') ) AND UserId=" + userId + "";
            //string query3 = @"DELETE dbo.tblMenuDistribution WHERE MenuSL IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId ='" + parantId + "')) ) AND UserId=" + userId + "";

            //bool ok = aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
            //bool ok1 = aCommonInternalDal.DeleteDataByDeleteCommand(query1, "HRDB");
            //bool ok2 = aCommonInternalDal.DeleteDataByDeleteCommand(query2, "HRDB");
            //bool ok3 = aCommonInternalDal.DeleteDataByDeleteCommand(query3, "HRDB");

            return true;
        }

        public void MainMenuDropDown(DropDownList aDropDownList)
        {
            aDbManager.LoadDropDownListData(aDropDownList, "ManuName", "SL", "sp_GET_MainMenuDropDown");
        }

        public void ActionStepDropDown(DropDownList aDropDownList)
        {
            //string query = "select * from tblActionSteps order by ASId";
            //aCommonInternalDal.LoadDropDownValue(aDropDownList, "ActionSteps", "ASId", query, "HRDB");
        }
        public void MenuDropDown(DropDownList aDropDownList, string id)
        {
            //string query = "select * from tblMainMenu where  ParantId='" + id + "'";
            //aCommonInternalDal.LoadDropDownValue(aDropDownList, "ManuName", "SL", query, "HRDB");

        }

        public bool CheckMenuSl(int MenuSl, int userId)
        {
            //string query = @"select * from tblMenuDistribution where MenuSL=" + MenuSl + " and UserId=" + userId + "";
            //DataTable aTable = new DataTable();
            //aTable = aCommonInternalDal.DataContainerDataTable(query, "HRDB");
            //if (aTable.Rows.Count == 0)
            //{
            //    return false;
            //}

            return true;
        }

        public string GetParantId(int sL)
        {
            //string parantId = string.Empty;
            //string query = "select * from tblMainMenu where  SL=" + sL + "";
            //DataTable aTable = new DataTable();
            //aTable = aCommonInternalDal.DataContainerDataTable(query, "HRDB");
            //parantId = aTable.Rows[0]["ParantId"].ToString();

            //return parantId;
            //*
            string s = 0.ToString();
            return s;
            //*
        }

        public bool SaveMainMenu(int MenuSl, int userId)
        {
            //List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            //aSqlParameterlist.Add(new SqlParameter("@SL", SL));
            //aSqlParameterlist.Add(new SqlParameter("@UserId", userId));
            //aSqlParameterlist.Add(new SqlParameter("@MenuSL", MenuSl));

            //string query = @"INSERT INTO tblMenuDistribution ( SL ,UserId ,MenuSL ,Status) VALUES  ( @SL ,@UserId ,@MenuSL ,  'True'  )";
            //return aCommonInternalDal.SaveDataByInsertCommand(query, aSqlParameterlist, "HRDB");

            //*
            return true;
            //*
        }



        public void UserDdl(DropDownList userDropDownList)
        {
            //string query = "select * from tblUser";
            //aCommonInternalDal.LoadDropDownValue(userDropDownList, "LoginName", "UserId", query, "HRDB");
        }

        public void MainMenu(DropDownList userDropDownList, string userId)
        {
            //string query = "select * from tblMainMenu INNER JOIN  tblMenuDistribution ON tblMainMenu.SL=tblMenuDistribution.MenuSL  where UserId='" + userId + "' and ( ParantId IS NULL OR ParantId='')";
            //aCommonInternalDal.LoadDropDownValue(userDropDownList, "ManuName", "SL", query, "HRDB");
        }
        public DataTable MainMenuLoad(string userId)
        {
            //string query = @"   SELECT  tbltemp.* FROM  (SELECT MM.*,ISNULL(MD.Status,'False') AS Status  FROM dbo.tblMainMenu MM " +
            //                  " LEFT JOIN tblMenuDistribution MD ON MM.SL=MD.MenuSL " +
            //                  " WHERE MD.UserId = '" + userId + "'  " +
            //                  " union  " +
            //                  " SELECT MM.*, Status=0  FROM dbo.tblMainMenu MM   " +
            //                  " WHERE MM.SL NOT IN (SELECT MenuSL FROM tblMenuDistribution WHERE UserId='" + userId + "')  )   " +
            //                  " AS tbltemp WHERE tbltemp.ParantId IS NULL OR tbltemp.ParantId =''   " +
            //                  " ORDER BY tbltemp.SL";


            //return aCommonInternalDal.DataContainerDataTable(query, "HRDB");

            //*
            DataTable dt = new DataTable();
            return dt;
            //*
        }
        public DataTable ApprovalMenuLoad(string parantId)
        {
            //string Query = @" SELECT tblA.* FROM(SELECT * FROM dbo.tblMainMenu WHERE ParantId='"+parantId+"' "+
            //                  "  UNION ALL "+
            //                  "    SELECT * FROM dbo.tblMainMenu WHERE ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId='"+parantId+"') "+
            //                   "   UNION ALL "+
            //                 "     SELECT * FROM dbo.tblMainMenu WHERE ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId='"+parantId+"'))) "+
            //                 "     AS tblA WHERE tblA.IsApprovalPage=1 ORDER BY tblA.SL ASC";


            //return aCommonInternalDal.DataContainerDataTable(Query, "HRDB");
            //*
            DataTable dt = new DataTable();
            return dt;
            //*
        }

        public void ApprovalManuDropDown(DropDownList userDropDownList, string userId, string parantId)
        {
            //string Query = @" SELECT MM.*,ISNULL(MD.Status,'False') AS [Status]  FROM dbo.tblMainMenu MM      "+
            //                 " INNER JOIN tblMenuDistribution MD ON MM.SL=MD.MenuSL      " +
            //               //" INNER JOIN dbo.tblActionUserWiseApproval AA ON MD.MenuSL =AA.ManuSL AND MD.UserId =AA.UserId " +
            //               " INNER JOIN dbo.tblActionPageWiseStep AA ON MD.MenuSL =AA.ManuSL" +
            //                 " WHERE (MM.ParantId IS NOT NULL OR MM.ParantId <>'') and MD.UserId = '" + userId + "' AND MM.ModuleId='" + parantId + "' AND MM.IsApprovalPage=1 AND MD.Status=1";
            //aCommonInternalDal.LoadDropDownValue(userDropDownList, "ManuName", "SL", Query, "HRDB");
        }
        public DataTable OtherMenuLoad(string userId, string parantId)
        {
            string Query = @" SELECT tblSelected.* FROM  (SELECT tblMenuTemp.*,tbl1.[Status]  FROM  (SELECT * FROM dbo.tblMainMenu WHERE  " +
                                "  ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE SL ='" + parantId + "') " +

                                "    UNION " +

                                "   SELECT * FROM dbo.tblMainMenu WHERE " +
                                "   ParantId IN(SELECT SL FROM dbo.tblMainMenu WHERE  " +
                                "   ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE SL ='" + parantId + "')) " +

                                 "   UNION  " +

                                "   SELECT * FROM dbo.tblMainMenu WHERE " +
                                "   ParantId IN(SELECT SL FROM dbo.tblMainMenu WHERE  " +
                                "   ParantId IN(SELECT SL FROM dbo.tblMainMenu WHERE  " +
                                "   ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE SL ='" + parantId + "'))))   " +
                                "   AS tblMenuTemp   " +

                                "   INNER JOIN (SELECT MM.*,ISNULL(MD.Status,'False') AS [Status]  FROM dbo.tblMainMenu MM   " +
                                "   LEFT JOIN tblMenuDistribution MD ON MM.SL=MD.MenuSL   " +
                                "   WHERE (MM.ParantId IS NOT NULL OR MM.ParantId <>'') and MD.UserId = '" + userId + "') AS tbl1 ON tblMenuTemp.SL = tbl1.SL)   " +
                                "   AS tblSelected  " +


                               "    UNION   " +

                                "   SELECT tblMenuTemp.* ,Status=0 FROM  (SELECT * FROM dbo.tblMainMenu WHERE   " +
                                "   ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE SL ='" + parantId + "')  " +

                                "    UNION  " +

                                "   SELECT * FROM dbo.tblMainMenu WHERE   " +
                                "   ParantId IN(SELECT SL FROM dbo.tblMainMenu WHERE   " +
                                "   ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE SL ='" + parantId + "'))  " +

                                "    UNION  " +

                                "   SELECT * FROM dbo.tblMainMenu WHERE   " +
                                "   ParantId IN(SELECT SL FROM dbo.tblMainMenu WHERE   " +
                               "    ParantId IN(SELECT SL FROM dbo.tblMainMenu WHERE   " +
                               "    ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE SL ='" + parantId + "'))))   " +
                              "     AS tblMenuTemp WHERE tblMenuTemp.SL NOT IN (SELECT MenuSL FROM tblMenuDistribution WHERE UserId='" + userId + "')";


            //return aCommonInternalDal.DataContainerDataTable(Query, "HRDB");
            //*
            DataTable dt = new DataTable();
            return dt;
            //*
        }
        public void LoadActionDAL(RadioButtonList rdl)
        {
            //aCommonInternalDal.LoadAction(rdl);

        }

        public DataTable GetActionPageWiseStepDAL(string manuSL)
        {
            //List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            //aSqlParameterlist.Add(new SqlParameter("@ManuSL", manuSL));
            //string query = @"SELECT * from tblActionPageWiseStep WHERE ManuSL=@ManuSL";
            //return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, "HRDB");
            //*
            DataTable dt = new DataTable();
            return dt;
            //*
        }

    }
}

