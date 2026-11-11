using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DAL.UA_DAL;
using DAO.UA_DAO;

namespace BLL.UA_BLL
{
    public class UserAuthentication2BLL
    {
        UserAuthentication2DAL authenticationDal = new UserAuthentication2DAL();
        public void CreateConnection_UA_DB()
        {
            authenticationDal.CreateConnection_UA_DB_DAL();
        }
        public void CloseAllConnection()
        {
            authenticationDal.CloseAllConnection_DAL();
        }

        public Int32 SaveUserInformationBll(UserInformationDAO aUserInformationDao)
        {
            return authenticationDal.SaveUserInformationDal(aUserInformationDao);
        }

        public DataTable LoadEmployee(string EmpMasterCode)
        {
            return authenticationDal.LoadEmployee(EmpMasterCode);
        }
        public DataTable GetUserInfoBLL(string search)
        {
            return authenticationDal.GetUserInfoDal(search);
        }
        public DataTable GetUserInfoBLL(int id)
        {
            return authenticationDal.GetUserInfobyIdDal(id);
        }
        public bool UpdateUserInformationBll(UserInformationDAO aUserInformationDao)
        {
            return authenticationDal.UpdateUserInformationDal(aUserInformationDao);
        }
        public DataTable LoginBLL(string loginName, string password)
        {
            return authenticationDal.LoginDAL(loginName, password);
        }

        public DataTable MenuHTML_BLL(string userId)
        {
            return authenticationDal.MenuHTML_DAL(userId);
        }

        public void LoadActionBLL(RadioButtonList rdl)
        {
            authenticationDal.LoadActionDAL(rdl);
        }

        public DataTable GetActionPageWiseStepBLL(string manuSL)
        {
            return authenticationDal.GetActionPageWiseStepDAL(manuSL);
        }

        public bool MenuSaveBll(UserAuthenticationDAO aUserAuthenticationDAO)
        {
            return authenticationDal.MenuSaveDal(aUserAuthenticationDAO);
        }

        //public DataTable Login(string loginName,string password)
        //{
        //    return authenticationDal.Login(loginName, password);
        //}

        public bool SaveMenuBll(int sl, int userId)
        {
            string parantId = string.Empty;
            bool ok = authenticationDal.SaveMainMenu(sl, userId);
            for (int i = 0; i < 2; i++)
            {
                parantId = authenticationDal.GetParantId(sl);
                if (!string.IsNullOrEmpty(parantId))
                {
                    sl = Convert.ToInt32(parantId);
                    if (!authenticationDal.CheckMenuSl(Convert.ToInt32(parantId), userId))
                    {
                        bool ok1 = authenticationDal.SaveMainMenu(Convert.ToInt32(parantId), userId);
                    }
                }
            }
            return true;
        }

        public void ActionStepDropDownBLL(DropDownList aDropDownList)
        {
            authenticationDal.ActionStepDropDown(aDropDownList);
        }

        public void MainMenuDropdown(DropDownList aDropDownList)
        {
            authenticationDal.MainMenuDropDown(aDropDownList);
        }
        public void MenuDropdown(DropDownList aDropDownList, string id)
        {
            authenticationDal.MenuDropDown(aDropDownList, id);
        }

        public void UserDdl(DropDownList userDropDownList)
        {
            authenticationDal.UserDdl(userDropDownList);
        }

        public bool MenuPermissionRemove(int sl, int userId)
        {
            return authenticationDal.MenuPermissionRemove(sl, userId);
        }


        public bool SaveMainMenu(int sl, int userId)
        {

            return authenticationDal.SaveMainMenu(sl, userId);
        }

        public void MainMenu(DropDownList userDropDownList, string userId)
        {
            authenticationDal.MainMenu(userDropDownList, userId);
        }


        public DataTable MainMenuLoad(string userId)
        {

            return authenticationDal.MainMenuLoad(userId);
        }
        public DataTable OtherMenuLoad(string userId, string parantId)
        {

            return authenticationDal.OtherMenuLoad(userId, parantId);
        }

        public DataTable ApprovalMenuLoadBLL(string parantId)
        {
            return authenticationDal.ApprovalMenuLoad(parantId);

        }
        public void ApprovalManuDropDownBLL(DropDownList userDropDownList, string userId, string parantId)
        {
            authenticationDal.ApprovalManuDropDown(userDropDownList, userId, parantId);
        }
        public bool DeleteDataBll(UserInformationDAO aUserInformationDAO)
        {
            return authenticationDal.DeleteDataDal(aUserInformationDAO);
        }
    }
}




