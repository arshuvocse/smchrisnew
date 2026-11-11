using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.MenuSetup_DAL;
using DAO.HRIS_DAO_EF;

public partial class MenuSetup_MenuApprovalGroupSetup : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private MenuSetupCommonDAL _menuSetupCommonDal = new MenuSetupCommonDAL();
    private int mid = 0;
    private string _userId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {
            LoadInitialDDL();

            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();
                if (mid > 0)
                {
                    using (var db = new HRIS_SMCEntities())
                    {
                        var mg = (from mgs in db.tblMenuApprovalGroupSetups
                                  where mgs.MenuApprovalGroupSetupId == mid
                            select mgs).FirstOrDefault();

                        ddlCompany.SelectedValue = mg.CompanyId.ToString();
                        Session["cid"] = mg.CompanyId;
                        txt_GroupName.Text = mg.MenuApprovalGroupName;

                        LoadGridEditMode(mid);
                    }

                }
            }
            else
            {
                LoadInitialGrid();
            }
            LoadGridRadio();
            RadVisible();
        }
    }

    public void LoadGridRadio()
    {
        for (int i = 0; i < gv_Menu.Rows.Count; i++)
        {
            RadioButtonList radAction = (RadioButtonList)gv_Menu.Rows[i].FindControl("actionRadioButtonList");
            DataTable dtdata = _menuSetupCommonDal.GetMenuData();
            radAction.DataValueField = "ActionId";
            radAction.DataTextField = "ActionValue";
            radAction.DataSource = dtdata;
            radAction.DataBind();
        }
    }
    private void LoadInitialDDL()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {

            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
        }
    }
    private void LoadInitialGrid()
    {
        using (DataTable dt = _menuSetupCommonDal.GetMainMenuForMenuApprovalGroupSetupNew())
        {
            gv_Menu.DataSource = dt;
            gv_Menu.DataBind();
        }
    }
    private void LoadGridEditMode(int mid)
    {
        using (DataTable dt = _menuSetupCommonDal.GetMainMenuForMenuApprovalGroupSetupNewByMId(mid))
        {
            gv_Menu.DataSource = dt;
            gv_Menu.DataBind();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["IsActive"].ToString()) ? "False" : dt.Rows[i]["IsActive"].ToString()))
                {
                    CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingle");
                    chkSingle.Checked = true;
                    RadioButtonList radAction = (RadioButtonList)gv_Menu.Rows[i].FindControl("actionRadioButtonList");
                    radAction.SelectedValue = dt.Rows[i]["ActionId"].ToString();
                    CheckBox chkCancel = (CheckBox)gv_Menu.Rows[i].FindControl("cancelCheckBox");
                    chkCancel.Checked = Convert.ToBoolean(dt.Rows[i]["IsCancel"].ToString());
                }
            }
        }

    }
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MenuApprovalGroupSetupList.aspx");
    }
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["cid"] = ddlCompany.SelectedValue;
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

    public void RadVisible()
    {
        for (int i = 0; i < gv_Menu.Rows.Count; i++)
        {
            HiddenField asidHiddenField = (HiddenField)gv_Menu.Rows[i].FindControl("asidHiddenField");
            RadioButtonList radAction = (RadioButtonList)gv_Menu.Rows[i].FindControl("actionRadioButtonList");
            if (asidHiddenField.Value=="1")
            {
                for (int j = 0; j < radAction.Items.Count; j++)
                {
                    if (radAction.Items[j].Value=="2")
                    {
                        //radAction.Items[i].Enabled = false;
                        radAction.Items.RemoveAt(j);
                    }
                }
            }
        }
    }
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        try
        {
            var MenuGroupSetupId = string.IsNullOrEmpty(hdpk.Value)
                ? 0
                : int.Parse(hdpk.Value);
            var cid = int.Parse(ddlCompany.SelectedValue);
            var MenuGroupName = txt_GroupName.Text;

            using (var db = new HRIS_SMCEntities())
            {
                tblMenuApprovalGroupSetup mg = null;
                if (MenuGroupSetupId > 0)
                {
                    mg = (from m in db.tblMenuApprovalGroupSetups where m.MenuApprovalGroupSetupId.Equals(MenuGroupSetupId) select m).FirstOrDefault();
                    mg.CompanyId = cid;
                    mg.MenuApprovalGroupName = MenuGroupName;
                    mg.IsActive = true;
                    mg.UpdateBy = _userId;
                    mg.UpdateDate = DateTime.Now;
                    db.SaveChanges();
                }
                else
                {
                    mg = new tblMenuApprovalGroupSetup()
                    {
                        CompanyId = cid,
                        MenuApprovalGroupName = "AG-" + MenuGroupName,
                        EntryBy = _userId,
                        EntryDate = DateTime.Now,
                        IsActive = true

                    };
                    db.tblMenuApprovalGroupSetups.Add(mg);
                    db.SaveChanges();
                }
                tblMenuApprovalGroupSetupDetail mgd = null;
                for (int i = 0; i < gv_Menu.Rows.Count; i++)
                {
                    CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingle");
                    HiddenField hdpkd = (HiddenField)gv_Menu.Rows[i].FindControl("hdpkd");
                    var MenuApprovalGroupSetupDetailId = string.IsNullOrEmpty(hdpkd.Value) ? 0 : int.Parse(hdpkd.Value);
                    HiddenField hdMainMenuId = (HiddenField)gv_Menu.Rows[i].FindControl("hdMainMenuId");
                    if (chkSingle.Checked)
                    {

                        
                        var MainMenuId = int.Parse(hdMainMenuId.Value);
                        RadioButtonList radAction = (RadioButtonList)gv_Menu.Rows[i].FindControl("actionRadioButtonList");
                        CheckBox cancelCheckBox = (CheckBox)gv_Menu.Rows[i].FindControl("cancelCheckBox");
                        
                        if (MenuApprovalGroupSetupDetailId > 0)
                        {
                            _menuSetupCommonDal.DeleteMenuGroup(hdMainMenuId.Value,
                                mg.MenuApprovalGroupSetupId.ToString());
                            _menuSetupCommonDal.SaveMenuGroup(hdMainMenuId.Value, radAction.SelectedValue,
                            mg.MenuApprovalGroupSetupId.ToString(), cancelCheckBox.Checked);
                            mgd = (from md in db.tblMenuApprovalGroupSetupDetails
                                   where md.MenuApprovalGroupSetupDetailId.Equals(MenuApprovalGroupSetupDetailId)
                                select md).FirstOrDefault();
                            mgd.MenuApprovalGroupSetupId = mg.MenuApprovalGroupSetupId;
                            mgd.MainMenuId = MainMenuId;
                            mgd.IsActive = true;
                        }
                        else
                        {
                            _menuSetupCommonDal.SaveMenuGroup(hdMainMenuId.Value, radAction.SelectedValue,
                            mg.MenuApprovalGroupSetupId.ToString(), cancelCheckBox.Checked);
                            mgd = new tblMenuApprovalGroupSetupDetail()
                            {
                                MenuApprovalGroupSetupId = mg.MenuApprovalGroupSetupId,
                                MainMenuId = MainMenuId,
                                IsActive = true
                            };
                            db.tblMenuApprovalGroupSetupDetails.Add(mgd);
                        }

                    }
                    else
                    {
                        if (MenuApprovalGroupSetupDetailId > 0)
                        {
                            _menuSetupCommonDal.DeleteMenuGroup(hdMainMenuId.Value,
                                mg.MenuApprovalGroupSetupId.ToString());
                            mgd = (from md in db.tblMenuApprovalGroupSetupDetails
                                   where md.MenuApprovalGroupSetupDetailId.Equals(MenuApprovalGroupSetupDetailId)
                                select md).FirstOrDefault();
                            mgd.IsActive = false;
                        }
                    }
                }
                db.SaveChanges();
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='MenuApprovalGroupSetup.aspx';",
                    true);
            }
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MenuApprovalGroupSetup.aspx");
    }
}