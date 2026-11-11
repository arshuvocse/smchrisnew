using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Permission_DAL;
using DAL.Transfer_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Transfer_UI_EmpTransferAndRedesignationApprovalList : System.Web.UI.Page
{

    EmpTransferAndRedesignationDAL aEmpTransferAndRedesignationDal = new EmpTransferAndRedesignationDAL();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDownList();
            GetCompany();
            UserPersmissionValidation();
            EffectiveDateTextBox.Attributes.Add("readonly", "readonly");
            EffectToDate.Attributes.Add("readonly", "readonly");

            LoadEmpTransferAndRedesignationInfo();
        }

        try
        {

            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            loadGridView.UseAccessibleHeader = true;

        }
        catch (Exception ex)
        {


        }

    }

    private void LoadDropDownList()
    {
        aEmpTransferAndRedesignationDal.LoadCompanyDropDownList(ddlCompany);
        ddlCompany.SelectedIndex = 1;
    }

    public void GetCompany()
    {
        DataTable dtcomp = aPermissionDal.GetCompany();
        lchk_Company.DataValueField = "CompanyId";
        lchk_Company.DataTextField = "ShortName";
        lchk_Company.DataSource = dtcomp;
        lchk_Company.DataBind();

        try
        {
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

    private void LoadEmpTransferAndRedesignationInfo()
    {
        DataTable dataTable = aEmpTransferAndRedesignationDal.LoadIncrementInfoApp();

        if (dataTable.Rows.Count > 0)
        {
            loadGridView.DataSource = dataTable;
            loadGridView.DataBind();
            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            loadGridView.UseAccessibleHeader = true;
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
         //   aShowMessage.ShowMessageBox("Data Not Found!!", this);
        }
    }

     
         private string GenerateParamiterList()
    {
 
        string parameter = "   ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND ETR.CompanyId = " + ddlCompany.SelectedValue;
        }

       

        if (ddlFinYear.SelectedValue != "")
        {
            parameter = parameter + "  AND ETR.FinancialYearId = " + ddlFinYear.SelectedValue;
        }


        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND ETR.EffectiveDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND ETR.EffectiveDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND ETR.EffectiveDate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }

        return parameter;
    }
     

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("EmpTransferAndRedesignation.aspx");
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "ApprovalData")
        {


            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string jobReqId = datKey[0].ToString();
                string filepath = Path.GetDirectoryName(Request.Path);
                filepath = filepath.TrimStart('\\');
                string exten = Path.GetExtension(Request.Path);
                if (exten == string.Empty)
                {
                    filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
                }
                else
                {
                    filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
                }
                Session["EmpTransferAndRedesignationId"] = "";
                Session["EmpTransferAndRedesignationId"] = jobReqId;
                Session["AppLogId"] = datKey[2].ToString();
                Session["AppPage"] = filepath;


                Session["Status"] = "Approval";
                string EmployeeId = datKey["EmployeeId"].ToString();
                Response.Redirect("EmpTransferAndRedesignationApproval.aspx?mid=" + jobReqId);


            }

        }
         
    }

    private void PopUp(Int32 EmpTransferAndRedesignation)
    {
        string url = "../Report_UI/EmpTransferAndRedesignationReportViwer.aspx?rptType=" + EmpTransferAndRedesignation;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        aEmpTransferAndRedesignationDal.FinancialYearDropDown(ddlFinYear, ddlCompany.SelectedValue);
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        LoadEmpTransferAndRedesignationInfo();
    }

    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmpTransferAndRedesignationView.aspx");
    }
}