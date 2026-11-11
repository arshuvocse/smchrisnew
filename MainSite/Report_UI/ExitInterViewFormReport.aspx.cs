using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.ExitManagement_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Report_Pages_ExitInterViewFormReport : System.Web.UI.Page
{
    EmployeeJobLeftEntryDAL aEmployeeJobLeftEntryDAL = new EmployeeJobLeftEntryDAL();

    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();
    CommonDataLoadDAL aCommonDataLoadDal = new CommonDataLoadDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();
           // UserPersmissionValidation();
            EffectiveDateTextBox.Attributes.Add("readonly", "readonly");
            EffectToDate.Attributes.Add("readonly", "readonly");
            //LoadInfo();
            loadDropDownList();
        }
    }
    private void loadDropDownList()
    {
        aCommonDataLoadDal.CompanyDropDown(companyDropDownList, " WHERE CompanyId IN(" + CompanyId() + ")");
        companyDropDownList.SelectedIndex = 1;
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

                    //addNewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
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

    private void LoadInfo()
    {
        DataTable dataTable = aEmployeeJobLeftEntryDAL.LoadExitInterviewInformation(GenerateParamiterList());

        if (dataTable.Rows.Count > 0)
        {
            loadGridView.DataSource = dataTable;
            loadGridView.DataBind();
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            aShowMessage.ShowMessageBox("No Data Found!!", this);
        }
    }


    private string GenerateParamiterList()
    {

        string parameter = "   ";

        if (companyDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND EPE.CompanyId = " + companyDropDownList.SelectedValue;
        }

        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.JobLeftDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND EPE.JobLeftDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.JobLeftDate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }

        return parameter;
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("~/ExitManagement_UI/EmployeeJobLeftEntry.aspx");
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string Idd = datKey[0].ToString();
                DataTable aTable =
                          aEmployeeJobLeftEntryDAL.DeleteValidattionForEffectiveDate(Idd.ToString());

                if (aTable.Rows.Count > 0)
                {
                    string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["JobLeftDate"]).ToString("MMMM dd, yyyy");
                    string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                    if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                    {
                        Session["EmployeeJobLeftId"] = "";
                        Session["EmployeeJobLeftId"] = Idd;

                        Session["Status"] = "Edit";
                        Response.Redirect("~/ExitManagement_UI/EmployeeJobLeftEntry.aspx");
                    }
                    else
                    {
                        aShowMessage.ShowMessageBox("Data can not be Edited !!", this);
                    }
                }
            }
           

        }

        if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string Idd = datKey[0].ToString();
                

                Session["EmployeeJobLeftId"] = "";
                Session["EmployeeJobLeftId"] = Idd;
                Session["Status"] = "View";
                Response.Redirect("~/ExitManagement_UI/EmployeeJobLeftEntry.aspx");

               
            }

        }

        if (e.CommandName == "DeleteData")
        {

            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];

            if (datKey != null)
            {

                string Idd = datKey[0].ToString();
                DataTable aTable =
                          aEmployeeJobLeftEntryDAL.DeleteValidattionForEffectiveDate(Idd.ToString());

                if (aTable.Rows.Count > 0)
                {
                    string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["JobLeftDate"]).ToString("MMMM dd, yyyy");
                    string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                    if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                    {

                        Session["EmployeeJobLeftId"] = "";
                        Session["EmployeeJobLeftId"] = Idd;
                        Session["Status"] = "Delete";
                        Response.Redirect("~/ExitManagement_UI/EmployeeJobLeftEntry.aspx");
                    }
                    else
                    {
                        aShowMessage.ShowMessageBox("Data can not be Deleted !!", this);
                    }
                }


            }
            
        }

        if (e.CommandName == "Preview")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);



            var datKey = loadGridView.DataKeys[0];

            if (datKey != null)
            {

               // Response.Redirect("MemoPrintExitInterviewReport.aspx?mid=" + e.CommandArgument.ToString());
                PopUp(Convert.ToInt32(e.CommandArgument.ToString()));

            }



        }
    }

    private void PopUp(int trainingBudgetId)
    {
        string url = "../Report_UI/ExitManagementReportViwer.aspx?rptType=" + trainingBudgetId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }
    
    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {

    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        LoadInfo();
    }
}