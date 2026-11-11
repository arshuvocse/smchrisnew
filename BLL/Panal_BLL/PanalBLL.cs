using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using DAL.InternalCls;
using DAL.PanalCls;
using DAL.Panal_DAL;
using DAO.Panal_Entities;

namespace BLL.Panal_BLL
{
   public class PanalBLL
    {
       PanalClsDAL aPanalClsDal = new PanalClsDAL();
       PanalDAL aPanalDal = new PanalDAL();
       public void LoadActionBLL(RadioButtonList rdl)
       {
           aPanalDal.LoadActionDAL(rdl);

       }

       public DataTable GetActionPageWiseStepBLL(string manuSL)
       {
           return aPanalDal.GetActionPageWiseStepDAL(manuSL);
       }


       public DataTable GetManuWiseSelectedActionBLL(ObjUserWiseApprovalPermission approvalPermission)
       {
           return aPanalDal.GetManuWiseSelectedActionDAL(approvalPermission);
       }


       public bool UserWiseApprovalPermissionBLL(ObjUserWiseApprovalPermission approvalPermission)
       {

           ClsPrimaryKeyFind aClsPrimaryKeyFind = new ClsPrimaryKeyFind();
           approvalPermission.UWAId = aClsPrimaryKeyFind.PrimaryKeyMax("UWAId", "tblActionUserWiseApproval", "HRDB");
           return aPanalDal.UserWiseApprovalPermissionDAL(approvalPermission);
       }

       public bool MenuSaveBll(ObjPanal aObjPanal)
       {
           ClsPrimaryKeyFind aClsPrimaryKeyFind = new ClsPrimaryKeyFind();
           aObjPanal.SL = aClsPrimaryKeyFind.PrimaryKeyMax("SL", "tblMainMenu", "HRDB");
           return aPanalDal.MenuSaveDal(aObjPanal);
       }
       public DataTable GetApprovalPageStepsBLL(int ManuSL)
       {
           return aPanalDal.GetApprovalPageSteps(ManuSL);
       }

       public bool ApprovalPageStepsSaveBLL(ObjActionPageWiseStep actionPageWise)
       {

           ClsPrimaryKeyFind aClsPrimaryKeyFind = new ClsPrimaryKeyFind();
           actionPageWise.PWASId = aClsPrimaryKeyFind.PrimaryKeyMax("PWASId", "tblActionPageWiseStep", "HRDB");
           return aPanalDal.ApprovalPageStepsSave(actionPageWise);
       }

       public bool ApprovalPageStepsDeleteBLL(int sl)
       {
           return aPanalDal.ApprovalPageStepsDelete(sl);
       }

       public DataTable Login(string loginName,string password)
       {
           return aPanalClsDal.Login(loginName, password);
       }
       public bool LoginLog(string userId, string LoginName, DateTime loginTime, string ipAddress, string browserName, string browserVersion, string operatingSystem)
       {
           return aPanalClsDal.LoginLog(userId, LoginName, loginTime, ipAddress, browserName, browserVersion, operatingSystem);
       }


       public bool SaveMenuBll(int sl, int userId)
       {
           string parantId = string.Empty;
           bool ok = aPanalDal.SaveMainMenu(sl, userId);
           for (int i = 0; i < 2; i++)
           {
               parantId = aPanalDal.GetParantId(sl);
               if (!string.IsNullOrEmpty(parantId))
               {
                   sl = Convert.ToInt32(parantId);
                   if (!aPanalDal.CheckMenuSl(Convert.ToInt32(parantId), userId))
                   {
                       bool ok1 = aPanalDal.SaveMainMenu(Convert.ToInt32(parantId), userId);
                   }
               }
               


           }



           return true;
       }

       public void ActionStepDropDownBLL(DropDownList aDropDownList)
       {
           aPanalDal.ActionStepDropDown(aDropDownList);
       }

       public void MainMenuDropdown(DropDownList aDropDownList)
       {
           aPanalDal.MainMenuDropDown(aDropDownList);
       }
       public void MenuDropdown(DropDownList aDropDownList,string id)
       {
           aPanalDal.MenuDropDown(aDropDownList,id);
       }

       public void UserDdl(DropDownList userDropDownList)
       {
           aPanalDal.UserDdl(userDropDownList);
       }



       public bool MenuPermissionRemove(int sl, int userId)
       {
           return aPanalDal.MenuPermissionRemove(sl, userId);
       }


      public bool SaveMainMenu(int sl,int userId)
      {

       return   aPanalDal.SaveMainMenu(sl, userId);
      }

       public void MainMenu(DropDownList userDropDownList,string userId)
       {
           aPanalDal.MainMenu(userDropDownList, userId);
       }


       public DataTable MainMenuLoad(string userId)
       {

           return aPanalDal.MainMenuLoad(userId);
       }
        public DataTable OtherMenuLoad(string userId,string parantId)
        {

            return aPanalDal.OtherMenuLoad(userId, parantId);
        }

        public DataTable ApprovalMenuLoadBLL(string parantId)
        {
            return aPanalDal.ApprovalMenuLoad(parantId);

        }
       public void ApprovalManuDropDownBLL(DropDownList userDropDownList, string userId, string parantId)
       {
           aPanalDal.ApprovalManuDropDown(userDropDownList, userId, parantId);
       }
    }
}
