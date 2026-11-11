using System;
using System.Data;
using System.Web.UI.WebControls;
using DAL.DataManager;

namespace DAL.InternalCls
{
   public class ClsApprovalAction
    {
       ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
       public void LoadActionControlByUser(RadioButtonList rdl, string pageName, string userId)
       {
           //string query = @"SELECT * FROM dbo.tblActionUserWiseApproval UA INNER JOIN dbo.tblActionPageWiseStep PS ON UA.ManuSL = PS.ManuSL "+
           //             " INNER JOIN dbo.tblActionSteps SA ON PS.ASId = SA.ASId "+
           //             " INNER JOIN tblActionStepDetail AD ON SA.ASId=AD.ASId AND UA.ActionId=AD.ActionId "+
           //             " INNER JOIN dbo.tblMainMenu MM ON UA.ManuSL=MM.SL "+
           //             " WHERE UA.LoginName='" + userName + "' AND MM.URL LIKE '%" + pageName + "%'"; 
           string query = @"SELECT * FROM dbo.tblMenuApprovalGroupPermission
INNER JOIN dbo.tblMenuApprovalGroupPermissionDtl ON tblMenuApprovalGroupPermissionDtl.MenuApprovalGroupPermissionId = tblMenuApprovalGroupPermission.MenuApprovalGroupPermissionId
INNER JOIN dbo.tblMenuApprovalGroupSetup ON tblMenuApprovalGroupSetup.MenuApprovalGroupSetupId = tblMenuApprovalGroupPermissionDtl.MenuApprovalGroupSetupId
INNER JOIN dbo.tblActionGroupWiseApproval ON dbo.tblActionGroupWiseApproval.GroupId=dbo.tblMenuApprovalGroupSetup.MenuApprovalGroupSetupId
INNER JOIN dbo.tblActionPageWiseStep ON tblActionPageWiseStep.ManuSL = tblActionGroupWiseApproval.ManuSL
INNER JOIN dbo.tblActionSteps ON tblActionSteps.ASId = tblActionPageWiseStep.ASId
INNER JOIN dbo.tblAction ON tblAction.ActionId = tblActionGroupWiseApproval.ActionId
LEFT JOIN dbo.tblActionStepDetail ON tblActionStepDetail.ASId = tblActionSteps.ASId AND tblActionStepDetail.ActionId = dbo.tblActionGroupWiseApproval.ActionId
INNER JOIN dbo.tblMainMenu ON dbo.tblActionGroupWiseApproval.ManuSL=dbo.tblMainMenu.MainMenuId
WHERE UserId='" + userId + "' AND URL='" + pageName + "'";

           DataTable dtActionCondition = aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);



           string query1 = @"select * from tblAction where IsShow=1 ";
           DataTable dtAction = aCommonInternalDal.DataContainerDataTable(query1, DataBase.HRDB);
           rdl.DataSource = dtAction;
           rdl.DataTextField = "ActionText";
           rdl.DataValueField = "ActionValue";
           rdl.DataBind();

           
               if (dtActionCondition.Rows.Count>0)
               {
                   for (int i = 0; i < rdl.Items.Count; i++)
                   {
                       if ((rdl.Items[i].Value.Trim()) == (dtActionCondition.Rows[0]["ActionValue"].ToString().Trim()))
                       {
                           rdl.Items[i].Enabled = true;
                           
                           
                       }
                       else
                       {
                           if (rdl.Items[i].Text.ToString() != "Reject")
                           {
                               rdl.Items[i].Enabled = false;
                           }
                       }
                       if (rdl.Items[i].Text.ToString() == dtActionCondition.Rows[0]["ActionCondition"].ToString())
                       {
                           rdl.Items[i].Enabled = false;
                       }
                       if (rdl.Items[i].Text.Trim()== "Reject")
                       {

                           if (Convert.ToBoolean(dtActionCondition.Rows[0]["IsCancel"].ToString()))
                           {
                               rdl.Items[i].Enabled = true;
                           }
                           else
                           {
                               rdl.Items[i].Enabled = false;
                           }
                       }
                   }
               }
               else
               {
                   for (int i = 0; i < rdl.Items.Count; i++)
                   {

                       rdl.Items[i].Enabled = false;
                   }
               }
       }
       public string LoadForApprovalByUserCondition(string pageName, string userId)
       {
           string returnString = "";

           string query = @"SELECT * FROM dbo.tblMenuApprovalGroupPermission
INNER JOIN dbo.tblMenuApprovalGroupPermissionDtl ON tblMenuApprovalGroupPermissionDtl.MenuApprovalGroupPermissionId = tblMenuApprovalGroupPermission.MenuApprovalGroupPermissionId
INNER JOIN dbo.tblMenuApprovalGroupSetup ON tblMenuApprovalGroupSetup.MenuApprovalGroupSetupId = tblMenuApprovalGroupPermissionDtl.MenuApprovalGroupSetupId
INNER JOIN dbo.tblActionGroupWiseApproval ON dbo.tblActionGroupWiseApproval.GroupId=dbo.tblMenuApprovalGroupSetup.MenuApprovalGroupSetupId
INNER JOIN dbo.tblActionPageWiseStep ON tblActionPageWiseStep.ManuSL = tblActionGroupWiseApproval.ManuSL
INNER JOIN dbo.tblActionSteps ON tblActionSteps.ASId = tblActionPageWiseStep.ASId
LEFT JOIN dbo.tblActionStepDetail ON tblActionStepDetail.ASId = tblActionSteps.ASId AND tblActionStepDetail.ActionId = dbo.tblActionGroupWiseApproval.ActionId
INNER JOIN dbo.tblMainMenu ON dbo.tblActionGroupWiseApproval.ManuSL=dbo.tblMainMenu.MainMenuId
WHERE UserId='"+userId+"' AND URL='"+pageName+"'";

           DataTable dtActionCondition = aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);

           if (dtActionCondition.Rows.Count > 0)
           {
               returnString = dtActionCondition.Rows[0]["ActionCondition"].ToString();
           }
           else
           {
               returnString = "";
           }
           return returnString;
       }
    }
}
