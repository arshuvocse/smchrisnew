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

public partial class MenuSetup_MenuGroupPermission : System.Web.UI.Page
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
            using (DataTable dt = _menuSetupCommonDal.GetMenuGroupForMenuGroupPermission())
            {
                gv_Menu.DataSource = dt;
                gv_Menu.DataBind();
            }
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();
                if (mid > 0)
                {
                    using (var db = new HRIS_SMCEntities())
                    {
                        var mg = (from mgs in db.tblMenuGroupPermissions
                            where mgs.MenuGroupPermissionId == mid
                            select mgs).FirstOrDefault();
                        if (mg != null)
                        {
                            LoadUserUserWise(mg.UserId.ToString());
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
                LoadUser();
                //LoadInitialGrid();
            }
        }
    }

    private void LoadUserUserWise(string userId)
    {
        using (DataTable dt = _commonDataLoad.GetUserDDLUserWise(userId))
        {
            ddlUser.DataSource = dt;
            ddlUser.DataValueField = "Value";
            ddlUser.DataTextField = "TextField";
            ddlUser.DataBind();

            ddlUser.SelectedIndex = 0;
        }
    }

    private void LoadUser()
    {
        using (DataTable dt = _commonDataLoad.GetUserDDLNew2())
        {
            ddlUser.DataSource = dt;
            ddlUser.DataValueField = "Value";
            ddlUser.DataTextField = "TextField";
            ddlUser.DataBind();
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
        using (DataTable dt = _menuSetupCommonDal.GetMenuGroupForMenuGroupPermission())
        {
            gv_Menu.DataSource = dt;
            gv_Menu.DataBind();
        }
    }
    private void LoadGridEditMode(int mid)
    {
        using (DataTable dt = _menuSetupCommonDal.GetMenuGroupForMenuGroupPermissionByMId(mid))
        {
            //gv_Menu.DataSource = dt;
            //gv_Menu.DataBind();

            for (int i = 0; i < gv_Menu.Rows.Count; i++)
            {

                for (int kk = 0; kk < dt.Rows.Count; kk++)
                {
                    try
                    {
                        var IsActive = dt.Rows[kk]["IsActive"].ToString();
                        HiddenField hdMenuGroupSetupId = (HiddenField)gv_Menu.Rows[i].FindControl("hdMenuGroupSetupId");
                        if (IsActive == "1" && hdMenuGroupSetupId.Value == dt.Rows[kk]["MenuGroupSetupId"].ToString())
                        {
                            CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingle");
                            chkSingle.Checked = true;
                        }
                    }
                    catch (Exception)
                    {
                        
                        //throw;
                    }
                }
            }
        }

    }
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MenuGroupPermissionList.aspx");
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
        using (DataTable dt = _menuSetupCommonDal.GetMenuGroupForMenuGroupPermission())
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
            var MenuGroupPermissionId = string.IsNullOrEmpty(hdpk.Value)
                ? 0
                : int.Parse(hdpk.Value);
            var cid = int.Parse(ddlCompany.SelectedValue);
            var userId = int.Parse(ddlUser.SelectedValue);

            using (var db = new HRIS_SMCEntities())
            {
                tblMenuGroupPermission mg = null;
                if (MenuGroupPermissionId > 0)
                {
                    mg = (from m in db.tblMenuGroupPermissions where m.MenuGroupPermissionId.Equals(MenuGroupPermissionId) select m).FirstOrDefault();
                    mg.CompanyId = cid;
                    mg.UserId = userId;
                    mg.IsActive = true;
                    mg.UpdateBy = _userId;
                    mg.UpdateDate = DateTime.Now;
                    db.SaveChanges();
                }
                else
                {
                    mg = new tblMenuGroupPermission()
                    {
                        CompanyId = cid,
                        UserId = userId,
                        EntryBy = _userId,
                        EntryDate = DateTime.Now,
                        IsActive = true

                    };
                    db.tblMenuGroupPermissions.Add(mg);
                    db.SaveChanges();
                }
                tblMenuGroupPermissionDtl mgd = null;
                for (int i = 0; i < gv_Menu.Rows.Count; i++)
                {
                    CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingle");
                    HiddenField hdpkd = (HiddenField)gv_Menu.Rows[i].FindControl("hdpkd");
                    var MenuGroupPermissionDtlId = string.IsNullOrEmpty(hdpkd.Value) ? 0 : int.Parse(hdpkd.Value);
                    //if (chkSingle.Checked)
                    //{

                        HiddenField hdMenuGroupSetupId = (HiddenField)gv_Menu.Rows[i].FindControl("hdMenuGroupSetupId");
                        var MenuGroupSetupId = int.Parse(hdMenuGroupSetupId.Value);

                        //if (MenuGroupPermissionDtlId > 0)
                        //{
                        db.Database.ExecuteSqlCommand("DELETE FROM [dbo].[tblMenuGroupPermissionDtl] WHERE  MenuGroupPermissionId={0}",
                           mg.MenuGroupPermissionId);
                            mgd = new tblMenuGroupPermissionDtl()
                            {
                                MenuGroupPermissionId = mg.MenuGroupPermissionId,
                                MenuGroupSetupId = MenuGroupSetupId,
                                IsActive = chkSingle.Checked
                            };
                            db.tblMenuGroupPermissionDtls.Add(mgd);
                        //}
                        //else
                        //{
                        //    mgd = new tblMenuGroupPermissionDtl()
                        //    {
                        //        MenuGroupPermissionId = mg.MenuGroupPermissionId,
                        //        MenuGroupSetupId = MenuGroupSetupId,
                        //        IsActive = chkSingle.Checked
                        //    };
                        //    db.tblMenuGroupPermissionDtls.Add(mgd);
                        //}

                    //}
                    //else
                    //{
                    //    if (MenuGroupPermissionDtlId > 0)
                    //    {
                    //        mgd = (from md in db.tblMenuGroupPermissionDtls
                    //               where md.MenuGroupPermissionDtlId.Equals(MenuGroupPermissionDtlId)
                    //               select md).FirstOrDefault();
                    //        mgd.IsActive = false;
                    //    }
                    //}
                }
                db.SaveChanges();

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='MenuGroupPermissionList.aspx';",
                    true);
                //AlertMessageBoxShow("Operation Successful...");
            }
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MenuGroupPermission.aspx");
    }

    protected void lb_MenuGroupDetails_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField hdMenuGroupSetupId = (HiddenField)gv_Menu.Rows[rowID].FindControl("hdMenuGroupSetupId");
        Label txt_MenuGroupName = (Label)gv_Menu.Rows[rowID].FindControl("txt_MenuGroupName");
        m_MemberName.Text = txt_MenuGroupName.Text;
        //var pkd = hdpkd.Value;
        //m_hdpkd.Value = pkd;

        using (DataTable dt = _menuSetupCommonDal.GetMenuGroupDetailsByMId(hdMenuGroupSetupId.Value))
        {
            if (dt.Rows.Count>0)
            {
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    lvMenus.Items.Add(dt.Rows[i]["TextField"].ToString());
                //}
                lvMenus.DataSource = dt;
                lvMenus.DataBind();
                
            }
            //lvMenus.item.DataBind();
            //lvMenus.DataBind();
        }
        mpe_1.Show();
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        mpe_1.Hide();
    }
}