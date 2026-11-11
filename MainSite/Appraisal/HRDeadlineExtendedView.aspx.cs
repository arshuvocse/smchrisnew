using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.Increment_DAL;
using DAL.Permission_DAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_HRDeadlineExtendedView : System.Web.UI.Page
{
    private DeadlineExtendedEntryDAL _jdDal = new DeadlineExtendedEntryDAL();

    ShowMessage aShowMessage = new ShowMessage();
    PermissionDAL aPermissionDal = new PermissionDAL();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    IncrementDal aSuspendDal = new IncrementDal();
    private  userDal userDal = new userDal();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            UserPersmissionValidation();
            LoadDropDownList();
           // GetCompany();
            //  UserPersmissionValidation();

    
        }
    }



    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
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
    protected void EmployeeDropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {


        string empName = txtSearch.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

           // EmployeeDropDownList.Text = emp[0];
            txtSearch.Text = emp[1];
        }
        else
        {
            //txtSearch.Text = "";
            //txtSearch.Text = "";
            //EmpInfoIdHiddenField.Value = "";
            //aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        }
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        EmpIncrementLoad();
    }


    private string GenerateParamiterList()
    {


        string parameter = " ";
          
        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND m.CompanyId = " + ddlCompany.SelectedValue;
        }

        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and D.EmployeeId=" + ddlEmpInfo.SelectedValue.Trim() + "  ";
        }
      

   
        return parameter;
    }

    private void EmpIncrementLoad()
    {
        if (ddlCompany.SelectedValue != "")
        {

            DataTable dt = userDal.GetHrDeadlineExtendedEntryList(GenerateParamiterList());
            loadGridView.DataSource = dt;
            loadGridView.DataBind();

          
            if (dt.Rows.Count > 0)
            {
                loadGridView.DataSource = dt;
                loadGridView.DataBind();
                loadGridView.UseAccessibleHeader = true;
                loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
                loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
                loadGridView.UseAccessibleHeader = true;
            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                loadGridView.DataSource = null;
                loadGridView.DataBind();
            }
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    }

    public void UserPersmissionValidation()
    {
        try
        {


            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            string text = Path.GetExtension(Request.Path);
            if (text == string.Empty)
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
            }
            else
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            }

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

                    addNewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    //loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
                    //    Convert.ToBoolean(ViewState["View"].ToString());
                    //loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
                    //    Convert.ToBoolean(ViewState["Delete"].ToString());
                    //loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
                    //    Convert.ToBoolean(ViewState["Edit"].ToString());
                }
            }
            else
            {
                Response.Redirect("../DashBoard_UI/DashBoard.aspx");
            }
        }
        catch (Exception ex)
        {

            Response.Redirect("/Default.aspx");
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {


            using (DataTable dt222 = _commonDataLoad.GetEmpDDLAActiveOnlyView(ddlCompany.SelectedValue.ToString()))
            {

                ddlEmpInfo.DataSource = dt222;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;
            }


        }
        else
        {
         
        }
    }

    private void LoadDropDownList()
    {
        aSuspendDal.LoadCompany(ddlCompany);
        ddlCompany.SelectedIndex = 1;
        ddlCompany_SelectedIndexChanged(null, null);
    }

    //public void GetCompany()
    //{
    //    DataTable dtcomp = aPermissionDal.GetCompany();
    //    lchk_Company.DataValueField = "CompanyId";
    //    lchk_Company.DataTextField = "ShortName";
    //    lchk_Company.DataSource = dtcomp;
    //    lchk_Company.DataBind();

    //    DataTable userdata = aPermissionDal.GetUserCompany(Session["UserId"].ToString());
    //    for (int i = 0; i < userdata.Rows.Count; i++)
    //    {
    //        for (int j = 0; j < lchk_Company.Items.Count; j++)
    //        {
    //            if (lchk_Company.Items[j].Text.Trim() == userdata.Rows[i]["ShortName"].ToString())
    //            {
    //                lchk_Company.Items[j].Selected = true;


    //            }
    //        }
    //    }
    //}

    //public void UserPersmissionValidation()
    //{
    //    try
    //    {
    //        string filepath = Path.GetDirectoryName(Request.Path);
    //        filepath = filepath.TrimStart('\\');
    //        filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
    //        DataTable dtuserpermission = aPermissionDal.GetPermissionForUser(Session["UserId"].ToString(), filepath);
    //        if (dtuserpermission.Rows.Count > 0)
    //        {
    //            if (dtuserpermission.Rows[0]["UserTypeId"].ToString() != "3" ||
    //                dtuserpermission.Rows[0]["UserTypeId"].ToString() != "4")
    //            {


    //                ViewState["Add"] = dtuserpermission.Rows[0]["Add"].ToString();
    //                ViewState["Edit"] = dtuserpermission.Rows[0]["Edit"].ToString();
    //                ViewState["View"] = dtuserpermission.Rows[0]["View"].ToString();
    //                ViewState["Delete"] = dtuserpermission.Rows[0]["Delete"].ToString();

    //                detailsViewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

    //                loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
    //                    Convert.ToBoolean(ViewState["View"].ToString());
    //                loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
    //                    Convert.ToBoolean(ViewState["Delete"].ToString());
    //                loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
    //                    Convert.ToBoolean(ViewState["Edit"].ToString());
    //            }
    //        }
    //        else
    //        {
    //            Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //        aShowMessage.ShowMessageBox(ex.ToString(), this);
    //    }
    //}

    //public string CompanyId()
    //{
    //    string companyid = "";
    //    for (int i = 0; i < lchk_Company.Items.Count; i++)
    //    {
    //        if (lchk_Company.Items[i].Selected)
    //        {
    //            companyid = companyid + "'" + lchk_Company.Items[i].Value + "'" + ",";
    //        }
    //    }
    //    companyid = companyid.TrimEnd(',');
    //    return companyid;
    //}

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("HRDeadlineExtendedEntry.aspx");
    }

    protected void btn_edit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "Edit";

        HiddenField mastrId = (HiddenField)loadGridView.Rows[rowID].FindControl("DeadlineExtensionRequestId");


        Response.Redirect("DeadlineExtendedEntry.aspx?masterId=" + mastrId.Value + "");
    }

    protected void btn_Remove_OnClick(object sender, EventArgs e)
    {

        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "Delete";

        HiddenField mastrId = (HiddenField)loadGridView.Rows[rowID].FindControl("DeadlineExtensionRequestId");


        Response.Redirect("DeadlineExtendedEntry.aspx?masterId=" + mastrId.Value + "");

        //LinkButton lb = (LinkButton)sender;
        //GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //int rowID = gvRow.RowIndex;

        //HiddenField mastrId = (HiddenField)loadGridView.Rows[rowID].FindControl("AppraisalDeadLineMasterId");

        //bool result = _jdDal.DeleteAppraisalSetup(Convert.ToInt32(mastrId.Value), Session["LoginName"].ToString());

        //if (result == true)
        //{
        //    AlertMessageBoxShow("Operation Successful...");

        //    DataTable dt = _jdDal.GetAppraisalSetupList();
        //    loadGridView.DataSource = dt;
        //    loadGridView.DataBind();

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

        HiddenField mastrId = (HiddenField)loadGridView.Rows[rowID].FindControl("DeadlineExtensionRequestId");


        Response.Redirect("DeadlineExtendedEntry.aspx?masterId=" + mastrId.Value + "");
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
       Response.Redirect("HRDeadlineExtendedEntry.aspx");
    }
}