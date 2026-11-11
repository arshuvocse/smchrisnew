using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.HealthCare_DAL;
using DAL.Permission_DAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class HealthCare_UI_TopSheetGenerateView : System.Web.UI.Page
{

    private CommitteSetupDal aCommitteSetupDal = new CommitteSetupDal();

    private TopSheetDal aSheetDal = new TopSheetDal();
    PermissionDAL aPermissionDal = new PermissionDAL();

    
    ShowMessage aShowMessage = new ShowMessage();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            UserPersmissionValidation();
            EffectiveDateTextBox.Attributes.Add("readonly", "readonly");
            EffectToDate.Attributes.Add("readonly", "readonly");
            InitailLoad();
            EffectiveDateTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            EffectToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            LoadInformation();
           
        }

        try
        {
            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            loadGridView.UseAccessibleHeader = true;
        }
        catch (Exception)
        {

            //throw;
        }
    }

    private void InitailLoad()
    {
        aCommitteSetupDal.GetDDLCompany(ddlCompany);
       // ddlCompany.SelectedValue = 1.ToString();
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

                    btn_New.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
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

            Response.Redirect("/Default.aspx");
        }
    }
    private void LoadInformation()
    {
        DataTable dataTable = aSheetDal.Get_TopSheet(param());

        if (dataTable.Rows.Count > 0)
        {
            loadGridView.DataSource = dataTable;
            loadGridView.DataBind();
            this.loadGridView.ShowFooter = true;
            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }

    private string param()
    {

        string parameter = "   ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND Company.CompanyId =" + ddlCompany.SelectedValue ;
        }

        if (ddlMetStatus.SelectedValue != "0")
        {
            parameter = parameter + " AND M.MeetingStatus = '" + ddlMetStatus.SelectedValue+"'";
        }

        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND M.MeetingDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND M.MeetingDate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND M.MeetingDate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }

        return parameter;

    }


    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("TopSheetGenerate.aspx");
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }



    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "CommitteeSetupEdit")
        {
            var datKey = loadGridView.DataKeys[0];
            if (datKey != null)
            {
                string MId = e.CommandArgument.ToString();
                Response.Redirect("CommitteeApprovalPanel.aspx?MID=" + MId);
            }
        }

        if (e.CommandName == "ViewReport")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                PopUp(Convert.ToInt32(loadGridView.DataKeys[rowindex][0].ToString()),"null");
            }
        }

        if (e.CommandName == "ExcelReport")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                PopUp(Convert.ToInt32(loadGridView.DataKeys[rowindex][0].ToString()), "Exvl");
            }
        }

        if (e.CommandName == "CSVReport")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                PopUp(Convert.ToInt32(loadGridView.DataKeys[rowindex][0].ToString()), "CSV");
            }
        }

    }

    private void PopUp(Int32 MasterId,string param)
    {
        string url = "../Report_UI/HC_TopSheetReportViewer.aspx?rptType=" + MasterId + "&rptExcel=" + param;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
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

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        LoadInformation();
    }

    protected void appraisalResetButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("TopSheetGenerateView.aspx");
    }
}