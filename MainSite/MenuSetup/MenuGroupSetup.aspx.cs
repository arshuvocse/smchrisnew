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

public partial class MenuSetup_MenuGroupSetup : System.Web.UI.Page
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
            GetAll();
            GetSingleName();
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdMenuGroupSetupId.Value = mid.ToString();
                if (mid > 0)
                {
                    using (var db = new HRIS_SMCEntities())
                    {
                        var mg = (from mgs in db.tblMenuGroupSetups 
                                        where mgs.MenuGroupSetupId == mid
                            select mgs).FirstOrDefault();

                        ddlCompany.SelectedValue = mg.CompanyId.ToString();
                        if (mg.MenuTypeId!=0)
                        {
                            ddlMenuType.SelectedValue = mg.MenuTypeId.ToString();    
                        }
                        
                        
                        ddlMenuType.Enabled = false;
                        ddlMenuType.CssClass = "form-control form-control-sm";
                        Session["cid"] = mg.CompanyId;
                        txt_GroupName.Text = mg.MenuGroupName;
                        txt_GroupDesc.Text = mg.MenuGroupDescription;
                        if (mg.MenuTypeId != 0)
                        {
                            LoadGridEditMode(mid);
                        }
                        else
                        {
                            LoadGridEditMode2(mid);
                        }
                    }
                    GetSingleName();

                }
            }
            //else
            //{
            //    LoadInitialGrid();
            //}
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

        using (DataTable dt = _commonDataLoad.GetMenuTypeDDL())
        {

            ddlMenuType.DataSource = dt;
            ddlMenuType.DataValueField = "Value";
            ddlMenuType.DataTextField = "TextField";
            ddlMenuType.DataBind();
        }
    }

    private void LoadInitialGrid()
    {
        using (DataTable dt = _menuSetupCommonDal.GetMainMenuForMenuGroupSetup())
        {
            gv_Menu.DataSource = dt;
            gv_Menu.DataBind();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["Add"].ToString()) ? "False" : dt.Rows[i]["Add"].ToString()))
                {
                    CheckBox chkSingleAdd = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleAdd");
                    chkSingleAdd.Visible = true;
                }
                else
                {
                    CheckBox chkSingleAdd = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleAdd");
                    chkSingleAdd.Visible = false;
                }
                if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["View"].ToString()) ? "False" : dt.Rows[i]["View"].ToString()))
                {
                    CheckBox chkSingleView = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleView");
                    chkSingleView.Visible = true;
                }
                else
                {
                    CheckBox chkSingleView = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleView");
                    chkSingleView.Visible = false;
                }
                if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["Edit"].ToString()) ? "False" : dt.Rows[i]["Edit"].ToString()))
                {
                    CheckBox chkSingleEdit = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleEdit");
                    chkSingleEdit.Visible = true;
                }
                else
                {
                    CheckBox chkSingleEdit = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleEdit");
                    chkSingleEdit.Visible = false;
                }
                if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["Delete"].ToString()) ? "False" : dt.Rows[i]["Delete"].ToString()))
                {
                    CheckBox chkSingleDelete = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleDelete");
                    chkSingleDelete.Visible = true;
                }
                else
                {
                    CheckBox chkSingleDelete = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleDelete");
                    chkSingleDelete.Visible = false;
                }
            }
        }

    }
    private void LoadGridEditMode2(int mid)
    {
        using (DataTable dt = _menuSetupCommonDal.GetMainMenuForMenuGroupSetupByMIdWithoutType(mid))
        {
            gv_Menu.DataSource = dt;
            gv_Menu.DataBind();

            for (int i = 0; i < dt.Rows.Count; i++)
            {


                if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["Add"].ToString()) ? "False" : dt.Rows[i]["Add"].ToString()))
                {
                    CheckBox chkSingleAdd = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleAdd");
                    chkSingleAdd.Visible = true;
                }
                else
                {
                    CheckBox chkSingleAdd = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleAdd");
                    chkSingleAdd.Visible = false;
                }
                if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["View"].ToString()) ? "False" : dt.Rows[i]["View"].ToString()))
                {
                    CheckBox chkSingleView = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleView");
                    chkSingleView.Visible = true;
                }
                else
                {
                    CheckBox chkSingleView = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleView");
                    chkSingleView.Visible = false;
                }
                if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["Edit"].ToString()) ? "False" : dt.Rows[i]["Edit"].ToString()))
                {
                    CheckBox chkSingleEdit = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleEdit");
                    chkSingleEdit.Visible = true;
                }
                else
                {
                    CheckBox chkSingleEdit = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleEdit");
                    chkSingleEdit.Visible = false;
                }
                if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["Delete"].ToString()) ? "False" : dt.Rows[i]["Delete"].ToString()))
                {
                    CheckBox chkSingleDelete = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleDelete");
                    chkSingleDelete.Visible = true;
                }
                else
                {
                    CheckBox chkSingleDelete = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleDelete");
                    chkSingleDelete.Visible = false;
                }



                if (int.Parse(dt.Rows[i]["IsActive"].ToString()) > 0)
                {
                    CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingle");
                    chkSingle.Checked = true;

                    CheckBox chkSingleAdd = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleAdd");
                    chkSingleAdd.Checked = int.Parse(dt.Rows[i]["PAdd"].ToString()) > 0;

                    CheckBox chkSingleView = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleView");
                    chkSingleView.Checked = int.Parse(dt.Rows[i]["PView"].ToString()) > 0;

                    CheckBox chkSingleEdit = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleEdit");
                    chkSingleEdit.Checked = int.Parse(dt.Rows[i]["PEdit"].ToString()) > 0;

                    CheckBox chkSingleDelete = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleDelete");
                    chkSingleDelete.Checked = int.Parse(dt.Rows[i]["PDelete"].ToString()) > 0;
                }
            }
        }

    }


    private void LoadGridEditMode(int mid)
    {
        using (DataTable dt = _menuSetupCommonDal.GetMainMenuForMenuGroupSetupByMId(mid))
        {
            gv_Menu.DataSource = dt;
            gv_Menu.DataBind();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                

                if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["Add"].ToString()) ? "False" : dt.Rows[i]["Add"].ToString()))
                {
                    CheckBox chkSingleAdd = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleAdd");
                    chkSingleAdd.Visible = true;
                }
                else
                {
                    CheckBox chkSingleAdd = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleAdd");
                    chkSingleAdd.Visible = false;
                }
                if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["View"].ToString()) ? "False" : dt.Rows[i]["View"].ToString()))
                {
                    CheckBox chkSingleView = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleView");
                    chkSingleView.Visible = true;
                }
                else
                {
                    CheckBox chkSingleView = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleView");
                    chkSingleView.Visible = false;
                }
                if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["Edit"].ToString()) ? "False" : dt.Rows[i]["Edit"].ToString()))
                {
                    CheckBox chkSingleEdit = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleEdit");
                    chkSingleEdit.Visible = true;
                }
                else
                {
                    CheckBox chkSingleEdit = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleEdit");
                    chkSingleEdit.Visible = false;
                }
                if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["Delete"].ToString()) ? "False" : dt.Rows[i]["Delete"].ToString()))
                {
                    CheckBox chkSingleDelete = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleDelete");
                    chkSingleDelete.Visible = true;
                }
                else
                {
                    CheckBox chkSingleDelete = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleDelete");
                    chkSingleDelete.Visible = false;
                }



                if (int.Parse(dt.Rows[i]["IsActive"].ToString()) > 0)
                {
                    CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingle");
                    chkSingle.Checked = true;

                    CheckBox chkSingleAdd = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleAdd");
                    chkSingleAdd.Checked = int.Parse(dt.Rows[i]["PAdd"].ToString()) > 0;

                    CheckBox chkSingleView = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleView");
                    chkSingleView.Checked = int.Parse(dt.Rows[i]["PView"].ToString()) > 0;

                    CheckBox chkSingleEdit = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleEdit");
                    chkSingleEdit.Checked = int.Parse(dt.Rows[i]["PEdit"].ToString()) > 0;

                    CheckBox chkSingleDelete = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleDelete");
                    chkSingleDelete.Checked = int.Parse(dt.Rows[i]["PDelete"].ToString()) > 0;
                }
            }
        }

    }
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MenuGroupSetupList.aspx");
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["cid"] = ddlCompany.SelectedValue;
        GetAll();
        GetSingleName();
    }
    public void GetSingleName()
    {
        if (gv_Menu.Rows.Count > 0)
        {


            string masterText = ((Label) gv_Menu.Rows[0].FindControl("txt_Parent")).Text;
            for (int i = 1; i < gv_Menu.Rows.Count; i++)
            {
                if (masterText.Trim() == ((Label) gv_Menu.Rows[i].FindControl("txt_Parent")).Text.Trim())
                {
                    ((Label) gv_Menu.Rows[i].FindControl("txt_Parent")).Text = "";
                }
                else
                {
                    masterText = ((Label) gv_Menu.Rows[i].FindControl("txt_Parent")).Text.Trim();
                }
            }
        }
    }
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        try
        {
            var MenuGroupSetupId = string.IsNullOrEmpty(hdMenuGroupSetupId.Value)
                ? 0
                : int.Parse(hdMenuGroupSetupId.Value);
            var cid = int.Parse(ddlCompany.SelectedValue);
            var MenuTypeId = 0;
            if (ddlMenuType.SelectedIndex!=0)
            {
                MenuTypeId = int.Parse(ddlMenuType.SelectedValue);
            }
             
            var MenuGroupName = txt_GroupName.Text;
            var MenuGroupDescription = txt_GroupDesc.Text;

            #region Validation
            //if (ddlCompany.SelectedIndex <= 0)
            //{
            //    AlertMessageBoxShow("Company required...");
            //    return;
            //}
            if (string.IsNullOrEmpty(MenuGroupName))
            {
                AlertMessageBoxShow("Menu Group Name required...");
                return;
            }

            #endregion

            using (var db = new HRIS_SMCEntities())
            {
                tblMenuGroupSetup mg = null;
                if (MenuGroupSetupId>0)
                {
                    mg =(from m in db.tblMenuGroupSetups where m.MenuGroupSetupId.Equals(MenuGroupSetupId) select m).FirstOrDefault();
                    mg.CompanyId = cid;
                    mg.MenuGroupName = MenuGroupName;
                    mg.MenuGroupDescription = MenuGroupDescription;
                    mg.MenuTypeId = MenuTypeId;
                    mg.IsActive = true;
                    mg.UpdateBy = _userId;
                    mg.UpdateDate = DateTime.Now;
                    db.SaveChanges();
                }
                else
                {
                    mg = new tblMenuGroupSetup()
                    {
                        CompanyId = cid,
                        MenuGroupName = MenuGroupName,
                        MenuGroupDescription = MenuGroupDescription,
                        MenuTypeId = MenuTypeId,
                        EntryBy = _userId,
                        EntryDate = DateTime.Now,
                        IsActive = true
                        
                    };
                    db.tblMenuGroupSetups.Add(mg);
                    db.SaveChanges();
                }
                tblMenuGroupSetupDetail mgd = null;
                for (int i = 0; i < gv_Menu.Rows.Count; i++)
                {
                    CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingle");
                    HiddenField hdpkd = (HiddenField)gv_Menu.Rows[i].FindControl("hdpkd");
                    var MenuGroupSetupDetailId = string.IsNullOrEmpty(hdpkd.Value) ? 0 : int.Parse(hdpkd.Value);
                    if (chkSingle.Checked)
                    {
                        
                        HiddenField hdMainMenuId = (HiddenField)gv_Menu.Rows[i].FindControl("hdMainMenuId");
                        var MainMenuId = int.Parse(hdMainMenuId.Value);

                        ////todo add,view etc 
                        CheckBox chkSingleAdd = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleAdd");
                        CheckBox chkSingleEdit = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleEdit");
                        CheckBox chkSingleView = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleView");
                        CheckBox chkSingleDelete = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleDelete");
                        
                        if (MenuGroupSetupDetailId>0)
                        {
                            mgd = (from md in db.tblMenuGroupSetupDetails
                                where md.MenuGroupSetupDetailId.Equals(MenuGroupSetupDetailId)
                                select md).FirstOrDefault();
                            mgd.MenuGroupSetupId = mg.MenuGroupSetupId;
                            mgd.MainMenuId = MainMenuId;
                            mgd.IsActive = true;
                            mgd.Add = chkSingleAdd.Checked;
                            mgd.Edit = chkSingleEdit.Checked;
                            mgd.View = chkSingleView.Checked;
                            mgd.Delete = chkSingleDelete.Checked;
                        }
                        else
                        {
                            mgd = new tblMenuGroupSetupDetail()
                            {
                                MenuGroupSetupId = mg.MenuGroupSetupId,
                                MainMenuId = MainMenuId,
                                Add = chkSingleAdd.Checked,
                                Edit = chkSingleEdit.Checked,
                                View = chkSingleView.Checked,
                                Delete = chkSingleDelete.Checked,
                                IsActive = true
                            };
                            db.tblMenuGroupSetupDetails.Add(mgd);
                        }
                        
                    }
                    else
                    {
                        if (MenuGroupSetupDetailId > 0)
                        {
                            mgd = (from md in db.tblMenuGroupSetupDetails
                                where md.MenuGroupSetupDetailId.Equals(MenuGroupSetupDetailId)
                                select md).FirstOrDefault();
                            mgd.IsActive = false;
                            mgd.Add = false;
                            mgd.Edit = false;
                            mgd.View = false;
                            mgd.Delete = false;

                        }
                    }
                }
                db.SaveChanges();
                //AlertMessageBoxShow("Operation Successful...");
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='MenuGroupSetup.aspx';",
                    true);
            }
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
    }
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }
    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MenuGroupSetup.aspx");
    }

    public void CheckAllInRow(int i)
    {
        CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingle");
        CheckBox chkAddAll = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleAdd");
        CheckBox chkEditAll = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleEdit");
        CheckBox chkViewAll = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleView");
        CheckBox chkDelAll = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleDelete");
        if (chkSingle.Checked)
        {
            chkEditAll.Checked = true;
            chkAddAll.Checked = true;
            chkViewAll.Checked = true;
            chkDelAll.Checked = true;
        }
        else
        {
            chkEditAll.Checked = false;
            chkAddAll.Checked = false;
            chkViewAll.Checked = false;
            chkDelAll.Checked = false;
        }
    }
    protected void chkAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;

        CheckBox chkAddAll = (CheckBox)gv_Menu.HeaderRow.FindControl("chkAddAll");
        CheckBox chkEditAll = (CheckBox)gv_Menu.HeaderRow.FindControl("chkEditAll");
        CheckBox chkViewAll = (CheckBox)gv_Menu.HeaderRow.FindControl("chkViewAll");
        CheckBox chkDelAll = (CheckBox)gv_Menu.HeaderRow.FindControl("chkDelAll");
        if (cb.Checked)
        {
            chkEditAll.Checked = true;
            chkAddAll.Checked = true;
            chkViewAll.Checked = true;
            chkDelAll.Checked = true;
            for (int i = 0; i < gv_Menu.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingle");
                chkSingle.Checked = true;
                CheckAllInRow(i);
            }
        }
        else
        {
            chkEditAll.Checked = false;
            chkAddAll.Checked = false;
            chkViewAll.Checked = false;
            chkDelAll.Checked = false;
            for (int i = 0; i < gv_Menu.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingle");
                chkSingle.Checked = false;
                CheckAllInRow(i);
            }
        }
    }

    public void GetAll()
    {
        using (DataTable dt = _menuSetupCommonDal.GetMainMenuByMenuTypeId(1.ToString()))
        {
            gv_Menu.DataSource = dt;
            gv_Menu.DataBind();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["Add"].ToString()) ? "False" : dt.Rows[i]["Add"].ToString()))
                {
                    CheckBox chkSingleAdd = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleAdd");
                    chkSingleAdd.Visible = true;
                }
                else
                {
                    CheckBox chkSingleAdd = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleAdd");
                    chkSingleAdd.Visible = false;
                }
                if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["View"].ToString()) ? "False" : dt.Rows[i]["View"].ToString()))
                {
                    CheckBox chkSingleView = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleView");
                    chkSingleView.Visible = true;
                }
                else
                {
                    CheckBox chkSingleView = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleView");
                    chkSingleView.Visible = false;
                }
                if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["Edit"].ToString()) ? "False" : dt.Rows[i]["Edit"].ToString()))
                {
                    CheckBox chkSingleEdit = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleEdit");
                    chkSingleEdit.Visible = true;
                }
                else
                {
                    CheckBox chkSingleEdit = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleEdit");
                    chkSingleEdit.Visible = false;
                }
                if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["Delete"].ToString()) ? "False" : dt.Rows[i]["Delete"].ToString()))
                {
                    CheckBox chkSingleDelete = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleDelete");
                    chkSingleDelete.Visible = true;
                }
                else
                {
                    CheckBox chkSingleDelete = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleDelete");
                    chkSingleDelete.Visible = false;
                }
            }
        }
    }
    protected void ddlMenuType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMenuType.SelectedIndex>0)
        {
            using (DataTable dt = _menuSetupCommonDal.GetMainMenuByMenuTypeId(ddlMenuType.SelectedValue))
            {
                gv_Menu.DataSource = dt;
                gv_Menu.DataBind();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["Add"].ToString()) ? "False" : dt.Rows[i]["Add"].ToString()))
                    {
                        CheckBox chkSingleAdd = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleAdd");
                        chkSingleAdd.Visible = true;
                    }
                    else
                    {
                        CheckBox chkSingleAdd = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleAdd");
                        chkSingleAdd.Visible = false;
                    }
                    if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["View"].ToString()) ? "False" : dt.Rows[i]["View"].ToString()))
                    {
                        CheckBox chkSingleView = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleView");
                        chkSingleView.Visible = true;
                    }
                    else
                    {
                        CheckBox chkSingleView = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleView");
                        chkSingleView.Visible = false;
                    }
                    if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["Edit"].ToString()) ? "False" : dt.Rows[i]["Edit"].ToString()))
                    {
                        CheckBox chkSingleEdit = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleEdit");
                        chkSingleEdit.Visible = true;
                    }
                    else
                    {
                        CheckBox chkSingleEdit = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleEdit");
                        chkSingleEdit.Visible = false;
                    }
                    if (bool.Parse(string.IsNullOrEmpty(dt.Rows[i]["Delete"].ToString()) ? "False" : dt.Rows[i]["Delete"].ToString()))
                    {
                        CheckBox chkSingleDelete = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleDelete");
                        chkSingleDelete.Visible = true;
                    }
                    else
                    {
                        CheckBox chkSingleDelete = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleDelete");
                        chkSingleDelete.Visible = false;
                    }
                }
            }
        }
        else
        {
            GetAll();
        }
        GetSingleName();
    }

    protected void chkAddAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        if (cb.Checked)
        {
            for (int i = 0; i < gv_Menu.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleAdd");
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
                CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleAdd");
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
                CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleEdit");
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
                CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleEdit");
                chkSingle.Checked = false;
            }
        }
    }

    protected void chkViewAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        if (cb.Checked)
        {
            for (int i = 0; i < gv_Menu.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleView");
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
                CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleView");
                chkSingle.Checked = false;
            }
        }
    }

    protected void chkDelAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        if (cb.Checked)
        {
            for (int i = 0; i < gv_Menu.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleDelete");
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
                CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingleDelete");
                chkSingle.Checked = false;
            }
        }
    }

    public void CheckAllUpperMenu(string id)
    {
        for (int i = 0; i < gv_Menu.Rows.Count; i++)
        {
            HiddenField hdMainMenuId = (HiddenField)gv_Menu.Rows[i].FindControl("hdMainMenuId");
            if (hdMainMenuId.Value==id)
            {
                CheckBox chkSingle = (CheckBox)gv_Menu.Rows[i].FindControl("chkSingle");
                chkSingle.Checked = true;
            }
        }
    }
    protected void gv_DocumentUpload_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;

        if ((gv.ShowHeader == true && gv.Rows.Count > 0)
            || (gv.ShowHeaderWhenEmpty == true))
        {
            //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void chkSingle_OnCheckedChanged(object sender, EventArgs e)
    {
        GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent);
        int i = Row.RowIndex;
        HiddenField hdMainMenuId = (HiddenField)gv_Menu.Rows[i].FindControl("hdMainMenuId");
        DataTable dtdata = _menuSetupCommonDal.GetMenuById(hdMainMenuId.Value);
        if (dtdata.Rows.Count>0)
        {
            DataTable dtparantid = _menuSetupCommonDal.GetMenuBySl(dtdata.Rows[0]["ParantId"].ToString());

            CheckAllUpperMenu(dtparantid.Rows[0]["MainMenuId"].ToString());
        }
        CheckAllInRow(i);
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}