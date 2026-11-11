using DAL.MAIN_FUNCTION;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DAO.UA_DAO;

namespace DAL.UA_DAL
{
    public class PanalDAL
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
        public void LoadUserNameDal(DropDownList ddl)
        {
            aDbManager.LoadDropDownListData(ddl, "UserName", "UserId", "sp_GET_AllUserName");
        }
        public void MainMenuDropDown(DropDownList ddl)
        {
            aDbManager.LoadDropDownListData(ddl, "ManuName", "SL", "sp_GET_MainMenuDropDown");
        }
        public void ActionStepDropDown(DropDownList aDropDownList)
        {
            aDbManager.LoadDropDownListData(aDropDownList, "ActionSteps", "ASId", "sp_GET_ActionStepsDropDown");
        }
        public void UserDdl(DropDownList userDropDownList)
        {
            aDbManager.LoadDropDownListData(userDropDownList, "LoginName", "UserId", "sp_GET_AllUserName");
        }
        public void ApprovalManuDropDown(DropDownList userDropDownList, string userId, string parantId)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@UserId", userId));
            aSqlParameterlist.Add(new SqlParameter("@ParantId", parantId));
            aDbManager.LoadDropDownListData(userDropDownList, "ManuName", "SL", "sp_GET_GetApprovalManuDropDown",aSqlParameterlist);
        }
        public void MainMenu(DropDownList ddl, string id)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SL", id));

            aDbManager.LoadDropDownListData(ddl, "ManuName", "SL", "sp_GET_MainMenubyIdDropDown", aSqlParameterlist);
        }
        public DataTable MainMenuLoad(string userId)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@userId", userId));

            return aDbManager.GetDataTableAction("sp_GET_AllMainMenuById", aSqlParameterlist);
        }
        public DataTable GetActionPageWiseStepDAL(string manuSL)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ManuSL", manuSL));
            return aDbManager.GetDataTableAction("sp_GET_GetActionPageWiseStep", aSqlParameterlist);
        }
        public void LoadActionDAL(RadioButtonList rdl)
        {
            aDbManager.LoadAction(rdl);
        }

        public DataTable ApprovalMenuLoad(string parantId)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ParantId", parantId));


            return aDbManager.GetDataTableAction("sp_GET_ApprovalMenuLoad", aSqlParameterlist);
        }
        public DataTable AllMenuDist(string userId)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@UserId", userId));


            return aDbManager.GetDataTableAction("sp_GET_AllMenuDist", aSqlParameterlist);
        }
        public DataTable GetApprovalPageSteps(int ManuSL)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ManuSL", ManuSL));


            return aDbManager.GetDataTableAction("sp_GET_ApprovalPagesSteps", aSqlParameterlist);
        }
        public DataTable OtherMenuLoad(string userId, string parantId)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@userId", userId));
            aSqlParameterlist.Add(new SqlParameter("@parantId", parantId));

            return aDbManager.GetDataTableAction("sp_GET_OtherMainMenubyIdDropDown", aSqlParameterlist);
        }
        public bool SaveMainMenu( int MenuSl, int userId)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            //aSqlParameterlist.Add(new SqlParameter("@SL", SL));
            aSqlParameterlist.Add(new SqlParameter("@UserId", userId));
            aSqlParameterlist.Add(new SqlParameter("@MenuSL", MenuSl));

            return aDbManager.SaveAction("sp_I_MenuDistribution", aSqlParameterlist);
        }
        public DataTable GetManuWiseSelectedActionDAL(ObjUserWiseApprovalPermission approvalPermission)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@UserId", approvalPermission.UserId));
            aSqlParameterlist.Add(new SqlParameter("@ManuSL", approvalPermission.ManuSL));

            return aDbManager.GetDataTableAction("sp_GET_ActionUserWiseApproval", aSqlParameterlist);
        }
        public bool UserWiseApprovalPermissionDAL(ObjUserWiseApprovalPermission approvalPermission)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@UserId", approvalPermission.UserId));
            aSqlParameterlist.Add(new SqlParameter("@LoginName", approvalPermission.LoginName));
            aSqlParameterlist.Add(new SqlParameter("@ManuSL", approvalPermission.ManuSL));
            aSqlParameterlist.Add(new SqlParameter("@ActionId", approvalPermission.ActionId));

            List<SqlParameter> aSqlParameterlist1 = new List<SqlParameter>();
            aSqlParameterlist1.Add(new SqlParameter("@UserId", approvalPermission.UserId));
            aSqlParameterlist1.Add(new SqlParameter("@ManuSL", approvalPermission.ManuSL));

            aDbManager.DeleteAction("sp_DEL_ActionUserWiseApproval", aSqlParameterlist1);
           
            return aDbManager.SaveAction("sp_I_ActionUserWiseApproval", aSqlParameterlist);
        }
        public Int32 CopyMenuDist(string mainUserId,string copyUserId)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MainUserId", mainUserId));
            aSqlParameterlist.Add(new SqlParameter("@CopyUserId", copyUserId));

            return aDbManager.SaveAction("sp_I_CopyMenuDist", aSqlParameterlist, "@CheckData");
        }

        public bool ApprovalPageStepsSave(ObjActionPageWiseStep actionPageWise)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            //aSqlParameterlist.Add(new SqlParameter("@PWASId", actionPageWise.PWASId));
            aSqlParameterlist.Add(new SqlParameter("@ManuSL", actionPageWise.ManuSL));
            aSqlParameterlist.Add(new SqlParameter("@ASId", actionPageWise.ASId));
            return aDbManager.SaveAction("sp_I_ActionPageWiseStep", aSqlParameterlist);
        }
        public bool ApprovalPageStepsDelete(int sl)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ManuSL", sl));

            return aDbManager.DeleteAction("sp_DEL_ApprovalPageStepsDelete", aSqlParameterlist);

        }
        public string GetParantId(int sL)
        {
            string parantId = string.Empty;
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SL", sL));

            DataTable aTable = aDbManager.GetDataTableAction("sp_GET_PariantId", aSqlParameterlist);
            parantId = aTable.Rows[0]["ParantId"].ToString();
            return parantId;

        }
        public bool CheckMenuSl(int MenuSl, int userId)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MenuSl", MenuSl));
            aSqlParameterlist.Add(new SqlParameter("@userId", userId));

            DataTable aTable = aDbManager.GetDataTableAction("sp_GET_CheckMenu", aSqlParameterlist);
            if (aTable.Rows.Count == 0)
            {
                return false;
            }
            return true;
        }
        public bool MenuPermissionRemove(int sl, int userId)
        {
            //string parantId = Convert.ToString(sl);
            //string query = @"DELETE dbo.tblMenuDistribution WHERE MenuSL IN (SELECT SL FROM dbo.tblMainMenu WHERE SL =" + sl + " ) AND UserId=" + userId + "";
            //string query1 = @"DELETE dbo.tblMenuDistribution WHERE MenuSL IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId ='" + parantId + "' ) AND UserId=" + userId + "";
            //string query2 = @"DELETE dbo.tblMenuDistribution WHERE MenuSL IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId ='" + parantId + "') ) AND UserId=" + userId + "";
            //string query3 = @"DELETE dbo.tblMenuDistribution WHERE MenuSL IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId IN (SELECT SL FROM dbo.tblMainMenu WHERE ParantId ='" + parantId + "')) ) AND UserId=" + userId + "";

            //bool ok = aCommonInternalDal.DeleteDataByDeleteCommand(query, "ConfectioneryCPOS");
            //bool ok1 = aCommonInternalDal.DeleteDataByDeleteCommand(query1, "ConfectioneryCPOS");
            //bool ok2 = aCommonInternalDal.DeleteDataByDeleteCommand(query2, "ConfectioneryCPOS");
            //bool ok3 = aCommonInternalDal.DeleteDataByDeleteCommand(query3, "ConfectioneryCPOS");

            //return true;
            string parantId = Convert.ToString(sl);
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SL", sl));
            aSqlParameterlist.Add(new SqlParameter("@userId", userId));
            aSqlParameterlist.Add(new SqlParameter("@parantId", parantId));

            return aDbManager.DeleteAction("sp_DEL_AllMenu", aSqlParameterlist);
        }
    }
}
