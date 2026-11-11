using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Increment_DAL;
using DAL.Permission_DAL;
using DAL.Transfer_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Transfer_UI_EmployeePromotionApprovalList : System.Web.UI.Page
{

    tblEmployeePromotionEntryDAO atblEmployeePromotionEntryDAO = new tblEmployeePromotionEntryDAO();
    tblEmployeePromotionEntryDAL atblEmployeePromotionEntryDAL = new tblEmployeePromotionEntryDAL();
    IncrementDal aSuspendDal = new IncrementDal();


    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();
            UserPersmissionValidation();
           
            LoadDropDownList();
            EffectiveDateTextBox.Attributes.Add("readonly", "readonly");
            EffectToDate.Attributes.Add("readonly", "readonly");

            LoadInfo();
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
        aSuspendDal.LoadCompany(ddlCompany);
        ddlCompany.SelectedIndex = 1;
        ddlCompany_SelectedIndexChanged(null, null);
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            aSuspendDal.LoadFinancialYear(ddlFinYear, ddlCompany.SelectedValue);
            aSuspendDal.LoadDivision(ddlDivision, ddlCompany.SelectedValue);
            CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
            using (DataTable dt = _commonDataLoad.GetDDLComDepartment(ddlCompany.SelectedValue))
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataValueField = "Value";
                ddlDepartment.DataTextField = "TextField";
                ddlDepartment.DataBind();
            }
        }
        else
        {
            ddlFinYear.Items.Clear();
        }
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
        catch (Exception ex)
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
            if (text==string.Empty)
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path)+".aspx";    
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

    private void LoadInfo()
    {
        DataTable dataTable = atblEmployeePromotionEntryDAL.LoadIncrementInfoApp();

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
           // aShowMessage.ShowMessageBox("No Data Found!!!",this);
        }
    }

    private string GenerateParamiterList()
    {
 
        string parameter = "   ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND EPE.CompanyId = " + ddlCompany.SelectedValue;
        }

        if (ddlDivision.SelectedValue != "")
        {
            parameter = parameter + "  AND EPE.DivisionId = " + ddlDivision.SelectedValue;
        }

        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  AND EPE.DepartmentId = " + ddlDepartment.SelectedValue;
        }
       

        if (ddlFinYear.SelectedValue != "")
        {
            parameter = parameter + "  AND EPE.FinancialYearId = " + ddlFinYear.SelectedValue;
        }


        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + EffectToDate.Text + "' ";
        }
        if (EffectiveDateTextBox.Text != string.Empty && EffectToDate.Text == string.Empty)
        {
            parameter = parameter + " AND EPE.Effectivedate BETWEEN '" + EffectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EffectiveDateTextBox.Text == string.Empty && EffectToDate.Text != string.Empty)
        {
            parameter = parameter + " AND INC.Effectivedate BETWEEN '" + EffectToDate.Text + "' AND '" + EffectToDate.Text + "' ";
        }

        return parameter;
    }


    private bool CheckAchievementsAllocateOrNot(int MainID)
    {
        bool status = false;
        int result = 0;
        using (var db = new HRIS_SMC_DBEntities())
        {
            result = (from t in db.tblEmployeePromotionEntries
                      where t.EmployeePromotionEntryId == MainID && t.AutoProcess == "Manually Updated"
                      select t).Count();

        }

        if (result > 0)
        {
            status = true;
        }

        return status;
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
                Session["EmployeePromotionEntryId"] = "";
                Session["EmployeePromotionEntryId"] = jobReqId;
                Session["AppLogId"] = datKey[2].ToString();
                Session["AppPage"] = filepath;

              
                string EmployeeId = datKey["EmployeeId"].ToString();
                Response.Redirect("EmployeePromotionApprove.aspx?mid=" + jobReqId);


            }

        }
    }


    private bool CheckPostedCsncel(int MainID)
    {
        bool status = false;
        //int result = 0;
        //using (var db = new HRIS_SMC_DBEntities())
        //{
        //    result = (from t in db.tblEmployeePromotionEntries
        //              where t.EmployeePromotionEntryId == MainID && (t.ActionStatus2 != "Posted" || t.ActionStatus2 != "Cancel")
        //              select t).Count();

        //}

        //if (result > 0)
        //{
        //    status = true;
        //}

        return status;
    }

    private void PopUp(Int32 EmployeePromotion)
    {
        string url = "../Report_UI/EmployeePromotionEntryViewReportViwer.aspx?rptType=" + EmployeePromotion;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    private void PopUp1(Int32 EmployeePromotion)
    {
        string url = "../Report_UI/EmployeePromotionEntryViewReportViwer.aspx?rptType=" + EmployeePromotion;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("EmployeePromotionEntry.aspx");
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
  
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        LoadInfo();
    }

    protected void appraisalResetButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeePromotionEntryView.aspx");
    }
}