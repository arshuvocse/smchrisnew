using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.Permission_DAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_AppaisalSetupList : System.Web.UI.Page
{

    JdDAL _jdDal = new JdDAL();

    ShowMessage aShowMessage = new ShowMessage();
    PermissionDAL aPermissionDal = new PermissionDAL();
    AppraisalSetupListDAL _AppraisalSetupListDAL=new AppraisalSetupListDAL();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            GetCompany();
            UserPersmissionValidation();

            LoadDroDownList();
        }
        try
        {
            gv_kpiSetup.UseAccessibleHeader = true;
            gv_kpiSetup.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv_kpiSetup.FooterRow.TableSection = TableRowSection.TableFooter;
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
    private void LoadDroDownList()
    {
        _AppraisalSetupListDAL.GetCompanyListShortNameIntoDropdown(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_SelectedIndexChanged(null, null);
    }

    protected void lb_SendMail_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "SendMail";

        HiddenField hdpk = (HiddenField)gv_kpiSetup.Rows[rowID].FindControl("AppraisalDeadLineMasterId");

        Response.Redirect("AppraisalDeadlineSetupMailSend.aspx?masterId=" + hdpk.Value);
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

                    gv_kpiSetup.Columns[gv_kpiSetup.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    gv_kpiSetup.Columns[gv_kpiSetup.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    gv_kpiSetup.Columns[gv_kpiSetup.Columns.Count - 3].Visible =
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

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("AppraisalDeadlineSetup.aspx");
    }

    protected void btn_edit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "Edit";

        HiddenField mastrId = (HiddenField)gv_kpiSetup.Rows[rowID].FindControl("AppraisalDeadLineMasterId");


        Response.Redirect("AppraisalDeadlineSetup.aspx?masterId=" + mastrId.Value + "");
    }
   
    protected void btn_Remove_OnClick(object sender, EventArgs e)
    {

        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "Delete";

        HiddenField mastrId = (HiddenField)gv_kpiSetup.Rows[rowID].FindControl("AppraisalDeadLineMasterId");


        Response.Redirect("AppraisalDeadlineSetup.aspx?masterId=" + mastrId.Value + "");

        //LinkButton lb = (LinkButton)sender;
        //GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //int rowID = gvRow.RowIndex;

        //HiddenField mastrId = (HiddenField)gv_kpiSetup.Rows[rowID].FindControl("AppraisalDeadLineMasterId");

        //bool result = _jdDal.DeleteAppraisalSetup(Convert.ToInt32(mastrId.Value), Session["LoginName"].ToString());

        //if (result == true)
        //{
        //    AlertMessageBoxShow("Operation Successful...");

        //    DataTable dt = _jdDal.GetAppraisalSetupList();
        //    gv_kpiSetup.DataSource = dt;
        //    gv_kpiSetup.DataBind();

        //}
        //else
        //{
        //    AlertMessageBoxShow("Operation Failed...");

        //}
    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    protected void btn_View_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "View";

        HiddenField mastrId = (HiddenField)gv_kpiSetup.Rows[rowID].FindControl("AppraisalDeadLineMasterId");


        Response.Redirect("AppraisalDeadlineSetup.aspx?masterId=" + mastrId.Value + "");
    }

    protected void companyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            _AppraisalSetupListDAL.LoadFinancialYearForSearch(financialYearDropDownList,
                       companyDropDownList.SelectedValue);


        }
        else
        {

            financialYearDropDownList.Items.Clear();
        }
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = _AppraisalSetupListDAL.GetAppraisalSetupList(" AND a.CompanyId IN (" + CompanyId() + ") " + GenerateParameter() + "");
            gv_kpiSetup.DataSource = dt;
            gv_kpiSetup.DataBind();
            gv_kpiSetup.UseAccessibleHeader = true;
            gv_kpiSetup.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv_kpiSetup.FooterRow.TableSection = TableRowSection.TableFooter;
        }
        catch (Exception)
        {
            gv_kpiSetup.DataSource = null;
            gv_kpiSetup.DataBind();
            //throw;
        }
    }

    private string GenerateParameter()
    {
        string parameter = " ";


        if (companyDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND   c.CompanyId  =  '" + companyDropDownList.SelectedValue + "' ";
        }


        if (financialYearDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND  d.FinancialYearId =  '" + financialYearDropDownList.SelectedValue + "'  ";
        }
        return parameter;
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}