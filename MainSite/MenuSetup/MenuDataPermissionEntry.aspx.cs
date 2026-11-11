using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.MenuSetup_DAL;
using DAL.UserPermissions_DAL;
using DAO.UA_DAO;

public partial class MenuSetup_MenuDataPermissionEntry : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    private MenuSetupCommonDAL _menuSetupCommonDal = new MenuSetupCommonDAL();
    MenuDataPermissionDAL aDataPermissionDal=new MenuDataPermissionDAL(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInitialDDL();
            GetAll();
        }
    }
    private void LoadInitialDDL()
    {

        using (DataTable dt = _commonDataLoad.GetUserDDL())
        {
            ddlUser.DataSource = dt;
            ddlUser.DataValueField = "Value";
            ddlUser.DataTextField = "TextField";
            ddlUser.DataBind();
        }


    }
    protected void chkAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        if (cb.Checked)
        {
            for (int i = 0; i < gv_Menu.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingle");
                chkSingle.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < gv_Menu.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingle");
                chkSingle.Checked = false;
            }
        }
    }
    protected void chkAddAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        if (cb.Checked)
        {
            for (int i = 0; i < gv_Menu.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleOwn");
                if (chkSingle.Visible)
                {
                    chkSingle.Checked = true;
                }

            }
        }
        else
        {
            for (int i = 0; i < gv_Menu.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleOwn");
                chkSingle.Checked = false;
            }
        }
    }
    protected void chkEditAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        if (cb.Checked)
        {
            for (int i = 0; i < gv_Menu.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleSMC");
                if (chkSingle.Visible)
                {
                    chkSingle.Checked = true;
                }

            }
        }
        else
        {
            for (int i = 0; i < gv_Menu.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleSMC");
                chkSingle.Checked = false;
            }
        }
    }
    public void GetAll()
    {

        using (DataTable dt = _menuSetupCommonDal.GetMainMenuByMenuTypeId(1.ToString()))
        {
            gv_Menu.DataSource = dt;
            gv_Menu.DataBind();

            
        }
    }

    protected void chkViewAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        if (cb.Checked)
        {
            for (int i = 0; i < gv_Menu.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSMcELView");
                if (chkSingle.Visible)
                {
                    chkSingle.Checked = true;
                }

            }
        }
        else
        {
            for (int i = 0; i < gv_Menu.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSMcELView");
                chkSingle.Checked = false;
            }
        }
    }


    public void CheckAllInRow(int i)
    {
        CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingle");
        CheckBox chkAddAll = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleOwn");
        CheckBox chkEditAll = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleSMC");
        CheckBox chkViewAll = (CheckBox)gv_Menu.Rows[i].FindControl("chkSMcELView");
        
        if (chkSingle.Checked)
        {
            chkEditAll.Checked = true;
            chkAddAll.Checked = true;
            chkViewAll.Checked = true;
            
        }
        else
        {
            chkEditAll.Checked = false;
            chkAddAll.Checked = false;
            chkViewAll.Checked = false;
            
        }
    }

    protected void chkSingle_OnCheckedChanged(object sender, EventArgs e)
    {
        GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent);
        int i = Row.RowIndex;
        CheckAllInRow(i);
    }


    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        aDataPermissionDal.DeleteData(ddlUser.SelectedValue);
        for (int i = 0; i < gv_Menu.Rows.Count; i++)
        {
            CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingle");
            if (chkSingle.Checked)
            {
                MenuDataPermissionDAO aMenuDataPermissionDao = new MenuDataPermissionDAO()
                {
                    UserId = Convert.ToInt32(ddlUser.SelectedValue),
                    MainMenuId = Convert.ToInt32(((HiddenField)gv_Menu.Rows[i].FindControl("hdMainMenuId")).Value),
                    Own = ((CheckBox)gv_Menu.Rows[i].FindControl("chkSingleOwn")).Checked,
                    SMC = ((CheckBox)gv_Menu.Rows[i].FindControl("chkSingleSMC")).Checked,
                    SMCEL = ((CheckBox)gv_Menu.Rows[i].FindControl("chkSMcELView")).Checked,
                };
                int id = aDataPermissionDal.SaveSupervisorApp(aMenuDataPermissionDao);
            }


        }
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='MenuDataPermissionEntry.aspx';",
                    true);
        
    }

    protected void ddlUser_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gv_Menu.Rows.Count; i++)
        {
            DataTable dtdata = aDataPermissionDal.GetUserDataPermission(ddlUser.SelectedValue,
                ((HiddenField) gv_Menu.Rows[i].FindControl("hdMainMenuId")).Value);
            if (dtdata.Rows.Count>0)
            {
                ((CheckBox) gv_Menu.Rows[i].FindControl("chkSingle")).Checked = true;
                ((CheckBox) gv_Menu.Rows[i].FindControl("chkSingleOwn")).Checked = Convert.ToBoolean(dtdata.Rows[0]["Own"].ToString());
                 ((CheckBox)gv_Menu.Rows[i].FindControl("chkSingleSMC")).Checked=Convert.ToBoolean(dtdata.Rows[0]["SMC"].ToString());
                 ((CheckBox)gv_Menu.Rows[i].FindControl("chkSMcELView")).Checked = Convert.ToBoolean(dtdata.Rows[0]["SMCEL"].ToString());
            }
        }
    }
}