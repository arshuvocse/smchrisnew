using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Permission_DAL;
using DAL.TrainingDAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class Report_Pages_TrainingBudgetReport : System.Web.UI.Page
{
    TrainingDAL aTrainingDal = new TrainingDAL();
    ShowMessage aShowMessage = new ShowMessage();
    PermissionDAL aPermissionDal = new PermissionDAL();
    CommonDataLoadDAL aCommonDataLoadDal = new CommonDataLoadDAL();
    TrainingBudgetDal aBudgetDal = new TrainingBudgetDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();
            //UserPersmissionValidation();

            // LoadList();

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

            aCommonDataLoadDal.CompanyDropDown(ddlCompany, " WHERE CompanyId IN (" + CompanyId() + ") ");
            ddlCompany.SelectedIndex = 1;
            ddlCompany_OnSelectedIndexChanged(null, null);
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

                    //addNewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    gv_trainingBgtList.Columns[gv_trainingBgtList.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    gv_trainingBgtList.Columns[gv_trainingBgtList.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    gv_trainingBgtList.Columns[gv_trainingBgtList.Columns.Count - 3].Visible =
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

    private void LoadList()
    {

        if (ddlCompany.SelectedValue != "")
        {
            DataTable dt = aBudgetDal.TrainingBudget2List(Parameter());
            gv_trainingBgtList.DataSource = dt;
            gv_trainingBgtList.DataBind();
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select a company !!!",this);
        }
        
    }

    public string Parameter()
    {
        string param = "";
        if (ddlCompany.SelectedIndex > 0)
        {
            param = param + " AND A.CompanyId='" + ddlCompany.SelectedValue + "' ";
        }
        if (ddlFinancialYear.SelectedIndex > 0)
        {
            param = param + " AND A.FinancialYearId='" + ddlFinancialYear.SelectedValue + "' ";
        }
        return param;
    }
    protected void lb_Edit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "Edit";

        HiddenField hdpk = (HiddenField)gv_trainingBgtList.Rows[rowID].FindControl("hdpk");

        Response.Redirect("TrainingBudget2.aspx?mid=" + gv_trainingBgtList.DataKeys[rowID][0].ToString());
    }

    protected void lb_delete_OnClick(object sender, EventArgs e)
    {
        //LinkButton lb = (LinkButton)sender;
        //GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //int rowID = gvRow.RowIndex;

        //HiddenField hdpk = (HiddenField)gv_trainingBgtList.Rows[rowID].FindControl("hdpk");

        //bool result = aTrainingDal.DeleteBudget2(Convert.ToInt32(gv_trainingBgtList.DataKeys[rowID][0].ToString()), Session["UserId"].ToString());

        //if (result == true)
        //{
        //    AlertMessageBoxShow("Operation Successful...");
        //    LoadList();
        //}
        //else
        //{

        //    AlertMessageBoxShow("Operation Failed...");
        //    LoadList();

        //}


        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "Delete";

        HiddenField hdpk = (HiddenField)gv_trainingBgtList.Rows[rowID].FindControl("hdpk");

        Response.Redirect("TrainingBudget2.aspx?mid=" + gv_trainingBgtList.DataKeys[rowID][0].ToString());
    }

    protected void btnAddNewTrainingBudget_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("~/Training/TrainingBudget2.aspx");
    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    protected void lb_View_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "View";

        HiddenField hdpk = (HiddenField)gv_trainingBgtList.Rows[rowID].FindControl("hdpk");

        Response.Redirect("TrainingBudget2.aspx?mid=" + gv_trainingBgtList.DataKeys[rowID][0].ToString());
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        aCommonDataLoadDal.FinYearByCompDropDown(ddlFinancialYear, ddlCompany.SelectedValue);
    }

    protected void ddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btn_Search_OnClick(object sender, EventArgs e)
    {
        LoadList();
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewReport")
        {
            PopUp(Convert.ToInt32(e.CommandArgument.ToString()));
        }
    }

    private void PopUp(int trainingBudgetId)
    {
        string url = "../Report_UI/TrainingBudgetReportViewer.aspx?rptType=" + trainingBudgetId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }


}