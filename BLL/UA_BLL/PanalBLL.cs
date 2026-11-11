using DAL.UA_DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DAO.UA_DAO;

namespace BLL.UA_BLL
{
    public class PanalBLL
    {
        PanalDAL panalDal = new PanalDAL();
        public void CreateConnection_UA_DB()
        {
            panalDal.CreateConnection_UA_DB_DAL();
        }
        public void CloseAllConnection()
        {
            panalDal.CloseAllConnection_DAL();
        }
        public void LoadUserNameBll(DropDownList ddl)
        {
            panalDal.LoadUserNameDal(ddl);
        }
        public void MainMenu(DropDownList userDropDownList, string id)
        {
            panalDal.MainMenu(userDropDownList, id);
        }
        public DataTable MainMenuLoad(string userId)
        {
            return panalDal.MainMenuLoad(userId);
        }
        public DataTable OtherMenuLoad(string userId, string parantId)
        {
            return panalDal.OtherMenuLoad(userId, parantId);
        }
        public bool SaveMainMenu(int sL, int userId)
        {
            return panalDal.SaveMainMenu(sL, userId);
        }

        public Int32 CopyMenuDist(string mainUserId, string copyUserId)
        {
            return panalDal.CopyMenuDist(mainUserId, copyUserId);
        }

        public DataTable AllMenuDist(string userId)
        {
            return panalDal.AllMenuDist(userId);
        }

        public bool SaveMenuBll(int sl, int userId)
        {
            string parantId = string.Empty;
            bool ok = panalDal.SaveMainMenu(sl, userId);
            for (int i = 0; i < 2; i++)
            {
                parantId = panalDal.GetParantId(sl);
                if (!string.IsNullOrEmpty(parantId))
                {
                    sl = Convert.ToInt32(parantId);
                    if (!panalDal.CheckMenuSl(Convert.ToInt32(parantId), userId))
                    {
                        bool ok1 = panalDal.SaveMainMenu(Convert.ToInt32(parantId), userId);
                    }
                }
            }
            return true;
        }
        public bool MenuPermissionRemove(int sl, int userId)
        {
            return panalDal.MenuPermissionRemove(sl, userId);
        }
        public void MainMenuDropdown(DropDownList aDropDownList)
        {
            panalDal.MainMenuDropDown(aDropDownList);
        }
        public DataTable GetApprovalPageStepsBLL(int ManuSL)
        {
            return panalDal.GetApprovalPageSteps(ManuSL);
        }
        public void ActionStepDropDownBLL(DropDownList aDropDownList)
        {
            panalDal.ActionStepDropDown(aDropDownList);
        }
        public DataTable ApprovalMenuLoadBLL(string parantId)
        {
            return panalDal.ApprovalMenuLoad(parantId);
        }
        public bool ApprovalPageStepsDeleteBLL(int sl)
        {
            return panalDal.ApprovalPageStepsDelete(sl);
        }
        public bool ApprovalPageStepsSaveBLL(ObjActionPageWiseStep actionPageWise)
        {

            return panalDal.ApprovalPageStepsSave(actionPageWise);
        }
        public void UserDdl(DropDownList userDropDownList)
        {
            panalDal.UserDdl(userDropDownList);
        }
        public DataTable GetActionPageWiseStepBLL(string manuSL)
        {
            return panalDal.GetActionPageWiseStepDAL(manuSL);
        }
        public void LoadActionBLL(RadioButtonList rdl)
        {
            panalDal.LoadActionDAL(rdl);

        }
        public void ApprovalManuDropDownBLL(DropDownList userDropDownList, string userId, string parantId)
        {
            panalDal.ApprovalManuDropDown(userDropDownList, userId, parantId);
        }
        public bool UserWiseApprovalPermissionBLL(ObjUserWiseApprovalPermission approvalPermission)
        {
            return panalDal.UserWiseApprovalPermissionDAL(approvalPermission);
        }
        public DataTable GetManuWiseSelectedActionBLL(ObjUserWiseApprovalPermission approvalPermission)
        {
            return panalDal.GetManuWiseSelectedActionDAL(approvalPermission);
        }
    }
}
