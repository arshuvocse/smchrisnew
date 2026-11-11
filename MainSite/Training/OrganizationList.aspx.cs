using System.Data;
using System.IO;
using DAL.Permission_DAL;
using DAL.TrainingDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HELPER_FUNCTIONS.HELPERS;

public partial class Training_OrganizationSetup : System.Web.UI.Page
{
    private TrainingDAL _trainingDal = new TrainingDAL();

    ShowMessage aShowMessage = new ShowMessage();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();
            UserPersmissionValidation();

            LoadOrgList();
        }

        try
        {

            gv_OrgList.UseAccessibleHeader = true;
            gv_OrgList.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv_OrgList.FooterRow.TableSection = TableRowSection.TableFooter;
        }
        catch (Exception)
        {

            //throw;
        }
    }


    public void GetCompany()
    {
        try
        {
            DataTable dtcomp = aPermissionDal.GetCompany();
            lchk_Company.DataValueField = "CompanyId";
            lchk_Company.DataTextField = "ShortName";
            lchk_Company.DataSource = dtcomp;
            lchk_Company.DataBind();

            DataTable userdata = aPermissionDal.GetUserCompany(Session["UserId"].ToString());
            for (int i = 0; i < userdata.Rows.Count; i++)
            {
                for (int j = 0; j < lchk_Company.Items.Count; j++)
                {
                    if (lchk_Company.Items[j].Text.Trim() == userdata.Rows[i]["ShortName"].ToString())
                    {
                        lchk_Company.Items[j].Selected = true;
                    }
                }
            }
        }
        catch (Exception)
        {
            Response.Redirect("/Default.aspx");
        }
      
    }

    public void UserPersmissionValidation()
    {
        try
        {
            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            DataTable dtuserpermission = aPermissionDal.GetPermissionForUser(Session["UserId"].ToString(), filepath);
            if (dtuserpermission.Rows.Count > 0)
            {
                if (dtuserpermission.Rows[0]["UserTypeId"].ToString() != "3" ||
                    dtuserpermission.Rows[0]["UserTypeId"].ToString() != "4")
                {


                    ViewState["Add"] = dtuserpermission.Rows[0]["Add"].ToString();
                    ViewState["Edit"] = dtuserpermission.Rows[0]["Edit"].ToString();
                    ViewState["View"] = dtuserpermission.Rows[0]["View"].ToString();
                    ViewState["Delete"] = dtuserpermission.Rows[0]["Delete"].ToString();

                    detailsViewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    gv_OrgList.Columns[gv_OrgList.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    gv_OrgList.Columns[gv_OrgList.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    gv_OrgList.Columns[gv_OrgList.Columns.Count - 3].Visible =
                        Convert.ToBoolean(ViewState["Edit"].ToString());
                }
            }
            else
            {
                Response.Redirect("../DashBoard_UI/DashBoard.aspx");
            }
        }
        catch (Exception ex)
        {

            aShowMessage.ShowMessageBox(ex.ToString(), this);
        }
    }

    public string CompanyId()
    {
        string companyid = "";
        for (int i = 0; i < lchk_Company.Items.Count; i++)
        {
            if (lchk_Company.Items[i].Selected)
            {
                companyid = companyid + "'" + lchk_Company.Items[i].Value + "'" + ",";
            }
        }
        companyid = companyid.TrimEnd(',');
        return companyid;
    }

    protected void detailsViewButton_Click1(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("OrganizationSetup.aspx");
    }


    public void LoadOrgList()
    {

        try
        {
            gv_OrgList.DataSource = _trainingDal.GetOrganizationList(" AND A.CompanyId IN (" + CompanyId() + ")");
            gv_OrgList.DataBind();
            gv_OrgList.UseAccessibleHeader = true;
            gv_OrgList.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv_OrgList.FooterRow.TableSection = TableRowSection.TableFooter;
        }
        catch (Exception)
        {
            gv_OrgList.DataSource =null;
            gv_OrgList.DataBind();
            //throw;
        }
        //gv_OrgList.UseAccessibleHeader = false;
      //gv_OrgList.HeaderRow.TableSection = TableRowSection.TableHeader; 


    }
    protected void lb_Edit_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "Edit";

        HiddenField hdpk = (HiddenField)gv_OrgList.Rows[rowID].FindControl("hdpk");

        Response.Redirect("OrganizationSetup.aspx?mid=" + hdpk.Value);
    }
    protected void gv_OrgList_PreRender(object sender, EventArgs e)
    {
        if (gv_OrgList.Rows.Count > 0)
        {
            gv_OrgList.UseAccessibleHeader = true;
            gv_OrgList.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv_OrgList.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }
    protected void lb_delete_Click(object sender, EventArgs e)
    {
        //LinkButton lb = (LinkButton)sender;
        //GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //int rowID = gvRow.RowIndex;


        //HiddenField hdpk = (HiddenField)gv_OrgList.Rows[rowID].FindControl("hdpk");

        //bool result = _trainingDal.DeleteOrgInfo(Convert.ToInt32(hdpk.Value), Convert.ToInt32(Session["UserId"].ToString()));

        //if (result == true)
        //{
        //    AlertMessageBoxShow("Operation Successful...");
        //    LoadOrgList();
        //}
        //else
        //{
        //    AlertMessageBoxShow("Operation Failed...");
        //}


        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "Delete";

        HiddenField hdpk = (HiddenField)gv_OrgList.Rows[rowID].FindControl("hdpk");

        Response.Redirect("OrganizationSetup.aspx?mid=" + hdpk.Value);

        Session["Status"] = "Delete";

    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    protected void lb_View_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "View";

        HiddenField hdpk = (HiddenField)gv_OrgList.Rows[rowID].FindControl("hdpk");

        Response.Redirect("OrganizationSetup.aspx?mid=" + hdpk.Value);
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}