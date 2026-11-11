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

public partial class MenuSetup_MenuApprovalGroupPermission : System.Web.UI.Page
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
                        var mg = (from mgs in db.tblMenuApprovalGroupPermissions
                                  where mgs.MenuApprovalGroupPermissionId == mid
                            select mgs).FirstOrDefault();
                        if (mg != null)
                        {
                            ddlCompany.SelectedValue = mg.CompanyId.ToString();
                            ddlUser.SelectedValue = mg.UserId.ToString();
                            Session["cid"] = mg.CompanyId;

                            LoadGridEditMode(mid);
                        }
                    }
                }
            }
            else
            {
                //LoadInitialGrid();
            }
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
        using (DataTable dt = _commonDataLoad.GetUserDDLNew())
        {
            ddlUser.DataSource = dt;
            ddlUser.DataValueField = "Value";
            ddlUser.DataTextField = "TextField";
            ddlUser.DataBind();
        }


    }
    private void LoadInitialGrid()
    {
        using (DataTable dt = _menuSetupCommonDal.GetMenuGroupForMenuGroupPermission())
        {
            gv_Menu.DataSource = dt;
            gv_Menu.DataBind();
        }
    }
    private void LoadGridEditMode(int mid)
    {
        using (DataTable dt = _menuSetupCommonDal.GetMenuApprovalGroupForMenuApprovalGroupPermissionByMId(mid))
        {
            gv_Menu.DataSource = dt;
            gv_Menu.DataBind();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["IsActive"].ToString()) ? "False" : dt.Rows[i]["IsActive"].ToString()))
                {
                    CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingle");
                    chkSingle.Checked = true;
                }
            }
        }

    }
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MenuApprovalGroupPermissionList.aspx");
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
        using (DataTable dt = _menuSetupCommonDal.GetMenuApprovalGroupForMenuApprovalGroupPermission(ddlCompany.SelectedValue))
        {
            gv_Menu.DataSource = dt;
            gv_Menu.DataBind();
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

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        try
        {
            var MenuApprovalGroupPermissionId = string.IsNullOrEmpty(hdpk.Value)
                ? 0
                : int.Parse(hdpk.Value);
            var cid = int.Parse(ddlCompany.SelectedValue);
            var userId = int.Parse(ddlUser.SelectedValue);

            using (var db = new HRIS_SMCEntities())
            {
                tblMenuApprovalGroupPermission mg = null;
                if (MenuApprovalGroupPermissionId > 0)
                {
                    mg = (from m in db.tblMenuApprovalGroupPermissions where m.MenuApprovalGroupPermissionId.Equals(MenuApprovalGroupPermissionId) select m).FirstOrDefault();
                    mg.CompanyId = cid;
                    mg.UserId = userId;
                    mg.IsActive = true;
                    mg.UpdateBy = _userId;
                    mg.UpdateDate = DateTime.Now;
                    db.SaveChanges();
                }
                else
                {
                    mg = new tblMenuApprovalGroupPermission()
                    {
                        CompanyId = cid,
                        UserId = userId,
                        EntryBy = _userId,
                        EntryDate = DateTime.Now,
                        IsActive = true

                    };
                    db.tblMenuApprovalGroupPermissions.Add(mg);
                    db.SaveChanges();
                }
                tblMenuApprovalGroupPermissionDtl mgd = null;
                for (int i = 0; i < gv_Menu.Rows.Count; i++)
                {
                    CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingle");
                    HiddenField hdpkd = (HiddenField)gv_Menu.Rows[i].FindControl("hdpkd");
                    var MenuApprovalGroupPermissionDtlId = string.IsNullOrEmpty(hdpkd.Value) ? 0 : int.Parse(hdpkd.Value);
                    if (chkSingle.Checked)
                    {

                        HiddenField hdMenuApprovalGroupSetupId = (HiddenField)gv_Menu.Rows[i].FindControl("hdMenuApprovalGroupSetupId");
                        var MenuApprovalGroupSetupId = int.Parse(hdMenuApprovalGroupSetupId.Value);

                        if (MenuApprovalGroupPermissionDtlId > 0)
                        {
                            mgd = (from md in db.tblMenuApprovalGroupPermissionDtls
                                   where md.MenuApprovalGroupPermissionDtlId.Equals(MenuApprovalGroupPermissionDtlId)
                                select md).FirstOrDefault();
                            mgd.MenuApprovalGroupPermissionId = mg.MenuApprovalGroupPermissionId;
                            mgd.MenuApprovalGroupSetupId = MenuApprovalGroupSetupId;
                            mgd.IsActive = true;
                        }
                        else
                        {
                            mgd = new tblMenuApprovalGroupPermissionDtl()
                            {
                                MenuApprovalGroupPermissionId = mg.MenuApprovalGroupPermissionId,
                                MenuApprovalGroupSetupId = MenuApprovalGroupSetupId,
                                IsActive = true
                            };
                            db.tblMenuApprovalGroupPermissionDtls.Add(mgd);
                        }

                    }
                    else
                    {
                        if (MenuApprovalGroupPermissionDtlId > 0)
                        {
                            mgd = (from md in db.tblMenuApprovalGroupPermissionDtls
                                   where md.MenuApprovalGroupPermissionDtlId.Equals(MenuApprovalGroupPermissionDtlId)
                                select md).FirstOrDefault();
                            mgd.IsActive = false;
                        }
                    }
                }
                db.SaveChanges();
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='MenuApprovalGroupPermission.aspx';",
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
        Response.Redirect("MenuApprovalGroupPermission.aspx");
    }

    protected void lb_MenuApprovalGroupDetails_OnClick(object sender, EventArgs e)
    {

    }
}