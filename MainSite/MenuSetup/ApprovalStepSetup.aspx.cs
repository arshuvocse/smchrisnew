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

public partial class MenuSetup_ApprovalStepSetup : System.Web.UI.Page
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
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["cid"] = ddlCompany.SelectedValue;
        using (DataTable dt = _menuSetupCommonDal.GetMenuForApprovalStepSetup(ddlCompany.SelectedValue))
        {
            gv_Menu.DataSource = dt;
            gv_Menu.DataBind();

            //using (DataTable dtSteps = _menuSetupCommonDal.GetApprovalStepInfo(ddlCompany.SelectedValue))
            //{
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        CheckBoxList lchk_ApprovalStep = (CheckBoxList)gv_Menu.Rows[i].FindControl("lchk_ApprovalStep");
            //        lchk_ApprovalStep.DataSource = dtSteps;
            //        lchk_ApprovalStep.DataValueField = "Value";
            //        lchk_ApprovalStep.DataTextField = "TextField";
            //        lchk_ApprovalStep.DataBind();
            //    }
            //}
        }
    }

    protected void chkAll_OnCheckedChanged(object sender, EventArgs e)
    {

        GridViewRow Gv2Row = (GridViewRow)((Control)sender).NamingContainer;
        GridView Childgrid = (GridView)(Gv2Row.Parent.Parent);
        GridViewRow Gv1Row = (GridViewRow)(Childgrid.NamingContainer);
        int indexRowMainGrid = Gv1Row.RowIndex;

        GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent);
        int i = Row.RowIndex;
        i++;

        CheckBox cb = (CheckBox)sender;
        GridViewRow row = (GridViewRow)cb.NamingContainer;
        //TextBox txtIssueQty = row.FindControl("txtIssueQty") as TextBox;

        
        
        if (cb.Checked)
        {
            for (int i2 = 0; i2 < Childgrid.Rows.Count; i2++)
            {
                CheckBox chkSingle = (CheckBox)Childgrid.Rows[i2].FindControl("chkSingle");
                chkSingle.Checked = true;
            }
        }
        else
        {
            for (int i2 = 0; i2 < Childgrid.Rows.Count; i2++)
            {
                CheckBox chkSingle = (CheckBox)Childgrid.Rows[i2].FindControl("chkSingle");
                chkSingle.Checked = false;
            }
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
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < gv_Menu.Rows.Count; i++)
            {
                GridView gv_Child = (GridView)gv_Menu.Rows[i].FindControl("gv_Child");
                if (gv_Child.Rows.Count>0)
                {
                    for (int j = 0; j < gv_Child.Rows.Count; j++)
                    {
                        CheckBox chkSingle = (CheckBox)gv_Child.Rows[j].FindControl("chkSingle");
                        HiddenField hdApprovalStepSetupId = (HiddenField)gv_Child.Rows[j].FindControl("hdApprovalStepSetupId");
                        HiddenField hdMainMenuId = (HiddenField)gv_Child.Rows[j].FindControl("hdMainMenuId");
                        CheckBoxList lchk_ApprovalStep = (CheckBoxList)gv_Child.Rows[j].FindControl("lchk_ApprovalStep");
                        bool vercat = false;
                        for (int k = 0; k < lchk_ApprovalStep.Items.Count; k++)
                        {
                            
                            if (lchk_ApprovalStep.Items[k].Value=="3" && lchk_ApprovalStep.Items[k].Selected)
                            {
                                vercat = true;
                            }
                            
                        }
                        if (vercat)
                        {
                            _menuSetupCommonDal.DeleteMenu(hdMainMenuId.Value);
                            _menuSetupCommonDal.SaveMenuWithCat(hdMainMenuId.Value, 2.ToString());
                        }
                        else
                        {
                            _menuSetupCommonDal.DeleteMenu(hdMainMenuId.Value);
                            _menuSetupCommonDal.SaveMenuWithCat(hdMainMenuId.Value, 1.ToString());
                        }
                        int ApprovalStepSetupId = int.Parse(hdApprovalStepSetupId.Value);
                        int MainMenuId = int.Parse(hdMainMenuId.Value);
                        using (var db = new HRIS_SMCEntities())
                        {
                            tblApprovalStepSetup asts = (from m in db.tblApprovalStepSetups where m.ApprovalStepSetupId == ApprovalStepSetupId 
                                                         & m.MainMenuId == MainMenuId
                                                         select m).FirstOrDefault();
                            if (chkSingle.Checked)
                            {
                                if (asts == null)////New Mode
                                {
                                    asts = new tblApprovalStepSetup();
                                    asts.CompanyId = int.Parse(ddlCompany.SelectedValue);
                                    asts.MainMenuId = MainMenuId;
                                    asts.EntryBy = _userId;
                                    asts.EntryDate = DateTime.Now;
                                    asts.IsActive = true;
                                    db.tblApprovalStepSetups.Add(asts);
                                    db.SaveChanges();
                                    foreach (ListItem approvalStep in lchk_ApprovalStep.Items)
                                    {
                                        if (approvalStep.Selected)
                                        {
                                            tblApprovalStepSetupStepDtl sd = new tblApprovalStepSetupStepDtl();
                                            sd.ApprovalStepSetupId = asts.ApprovalStepSetupId;
                                            sd.ApprovalStepInfoId = int.Parse(approvalStep.Value);
                                            sd.IsActive = true;
                                            db.tblApprovalStepSetupStepDtls.Add(sd);
                                        }
                                    }
                                    db.SaveChanges();
                                }////end new mode
                                else
                                {
                                    asts.CompanyId = int.Parse(ddlCompany.SelectedValue);
                                    asts.UpdateBy = _userId;
                                    asts.UpdateDate = DateTime.Now;
                                    asts.MainMenuId = MainMenuId;
                                    asts.IsActive = true;
                                    //db.SaveChanges();

                                    foreach (ListItem approvalStep in lchk_ApprovalStep.Items)
                                    {
                                        int ApprovalStepInfoId = int.Parse(approvalStep.Value);
                                        if (approvalStep.Selected)
                                        {
                                            tblApprovalStepSetupStepDtl sd = (from m in db.tblApprovalStepSetupStepDtls where m.ApprovalStepSetupId == ApprovalStepSetupId & m.ApprovalStepInfoId == ApprovalStepInfoId select m).FirstOrDefault();
                                            //sd.ApprovalStepSetupId = asts.ApprovalStepSetupId;
                                            if (sd != null)////Edit Mode
                                            {
                                               // sd.ApprovalStepInfoId = int.Parse(approvalStep.Value);
                                                sd.IsActive = true;
                                                db.SaveChanges();
                                            }
                                            else
                                            {
                                                sd = new tblApprovalStepSetupStepDtl();
                                                sd.ApprovalStepSetupId = asts.ApprovalStepSetupId;
                                                sd.ApprovalStepInfoId = int.Parse(approvalStep.Value);
                                                sd.IsActive = true;
                                                db.tblApprovalStepSetupStepDtls.Add(sd);
                                                db.SaveChanges();
                                            }
                                        }
                                        else
                                        {
                                            tblApprovalStepSetupStepDtl sd = (from m in db.tblApprovalStepSetupStepDtls where m.ApprovalStepSetupId == ApprovalStepSetupId & m.ApprovalStepInfoId == ApprovalStepInfoId select m).FirstOrDefault();
                                            //sd.ApprovalStepSetupId = asts.ApprovalStepSetupId;
                                            if (sd != null)////Edit Mode
                                            {
                                                sd.IsActive = false;
                                                db.SaveChanges();
                                            }
                                        }
                                    }
                                    
                                }
                            }
                            else
                            {
                                if (ApprovalStepSetupId > 0)
                                {
                                    asts.UpdateBy = _userId;
                                    asts.UpdateDate = DateTime.Now;
                                    asts.IsActive = false;
                                    foreach (ListItem approvalStep in lchk_ApprovalStep.Items)
                                    {
                                        int ApprovalStepInfoId = int.Parse(approvalStep.Value);
                                        
                                            tblApprovalStepSetupStepDtl sd = (from m in db.tblApprovalStepSetupStepDtls
                                                where m.ApprovalStepSetupId == ApprovalStepSetupId &
                                                      m.ApprovalStepInfoId == ApprovalStepInfoId
                                                select m).FirstOrDefault();
                                        if (sd!=null)
                                        {
                                            sd.IsActive = false; 
                                        }
                                        

                                    }

                                    db.SaveChanges();
                                }
                            }
                        }
                        
                        
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successful...');window.location ='ApprovalStepSetup.aspx';",
                true);
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ApprovalStepSetup.aspx");
    }

    protected void gv_Menu_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            HiddenField hfMainMenuId = (HiddenField)e.Row.FindControl("hfMainMenuId");

            GridView gv_Child = (GridView)e.Row.FindControl("gv_Child");

            string MainMenuId = hfMainMenuId.Value;
            using (DataTable dt = _menuSetupCommonDal.GetChildMenuByParentID(MainMenuId))
            {
                gv_Child.DataSource = dt;
                gv_Child.DataBind();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                int ApprovalStepSetupId = int.Parse(dt.Rows[i]["ApprovalStepSetupId"].ToString());

                using (DataTable dtSteps = _menuSetupCommonDal.GetApprovalStepInfo(ApprovalStepSetupId))
                {
                    
                        CheckBox chkSingle = (CheckBox)gv_Child.Rows[i].FindControl("chkSingle");
                        chkSingle.Checked = bool.Parse(dt.Rows[i]["IsActive"].ToString());

                        CheckBoxList lchk_ApprovalStep = (CheckBoxList)gv_Child.Rows[i].FindControl("lchk_ApprovalStep");
                        lchk_ApprovalStep.DataSource = dtSteps;
                        lchk_ApprovalStep.DataValueField = "Value";
                        lchk_ApprovalStep.DataTextField = "TextField";
                        lchk_ApprovalStep.DataBind();

                        for (int j = 0; j < dtSteps.Rows.Count; j++)
                        {
                            lchk_ApprovalStep.Items[j].Selected = bool.Parse(dtSteps.Rows[j]["IsActive"].ToString());
                        }


                    }
                }
            }
        }
    }

    protected void chkSingle_OnCheckedChanged(object sender, EventArgs e)
    {
        GridViewRow Row = ((GridViewRow)((Control)sender).Parent.Parent);
        CheckBox cb = (CheckBox)sender;
        if (cb.Checked)
        {
            CheckBoxList lchk_ApprovalStep = (CheckBoxList)Row.FindControl("lchk_ApprovalStep");
            lchk_ApprovalStep.Items[0].Selected = true;
        }
        else
        {
            CheckBoxList lchk_ApprovalStep = (CheckBoxList)Row.FindControl("lchk_ApprovalStep");
            for (int j = 0; j < lchk_ApprovalStep.Items.Count; j++)
            {
                lchk_ApprovalStep.Items[j].Selected = false;
            }

        }
    }
}