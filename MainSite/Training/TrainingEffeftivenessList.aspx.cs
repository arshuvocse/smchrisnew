using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.MPBudget;
using DAL.Permission_DAL;
using DAL.Survey;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class Training_TrainingEffeftivenessList : System.Web.UI.Page
{
    private static string _userId;
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    PermissionDAL aPermissionDal = new PermissionDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    TrainingEffeftivenessDAL aDal = new TrainingEffeftivenessDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {
            ReadOnltDate();
            GetCompany();
            LoadManpowerBudgetInfo();
            //UserPersmissionValidation();
            LoadInitialDDL();
            //List<MPBudgetViewModel> lModels = _mpBudgetCommonDal.GetMPBudgetList();
            //LoadManpowerBudgetInfo();
           
        }

        try
        {

            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
        }
        catch (Exception)
        {

            //throw;
        }
    }

    private void ReadOnltDate()
    {
        startDate.Attributes.Add("readonly", "readonly");
        endDate.Attributes.Add("readonly", "readonly");
    }

    public void GetCompany()
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

                    btn_New.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

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

    private void LoadManpowerBudgetInfo()
    {

        try
        {
            DataTable dtuserType = aPermissionDal.GetUserSuperAdminTrueorFalse(Session["UserId"].ToString());
            const int rowIndex = 0;
            if (dtuserType.Rows.Count > 0)
            {
                if (dtuserType.Rows[rowIndex].Field<Int32>("UserTypeId").ToString(CultureInfo.InvariantCulture) == "4")
                {
                    DataTable dtdata = aDal.LoadEmployeeDataBySuperAdmin(null);
                    loadGridView.DataSource = dtdata;
                    loadGridView.DataBind();
                    loadGridView.UseAccessibleHeader = true;
                    loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
                    loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
                }
                else
                {
                    DataTable dtdata2 = aDal.LoadEmployeeData(null);
                    loadGridView.DataSource = dtdata2;
                    loadGridView.DataBind();
                    loadGridView.UseAccessibleHeader = true;
                    loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
                    loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
                }

            }
        }
        catch (Exception)
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            //throw;
        }

        //DataTable dtdata = aDal.LoadEmployeeData(Parameter());
        //loadGridView.DataSource = dtdata;
        //loadGridView.DataBind();


        //if (ddlCompany.SelectedValue != "")
        //{
        //    if (ddlDepartment.SelectedValue != "")
        //    {
        //        if (ddlFinYear.SelectedValue != "")
        //        {
        //            DataTable aTable = _commonDataLoad.GetManpowerBudgetListInfo(ddlCompany.SelectedValue, ddlDepartment.SelectedValue, ddlFinYear.SelectedValue, " And bm.CompanyId IN (" + CompanyId() + ")");

        //            //if (aTable.Rows.Count > 0)
        //            {
        //                loadGridView.DataSource = aTable;
        //                loadGridView.DataBind();
        //            }
        //        }
        //        else
        //        {
        //            ddlFinYear.Focus();
        //        }
        //    }
        //    else
        //    {
        //        ddlDepartment.Focus();
        //    }
        //}
        //else
        //{
        //    ddlCompany.Focus();
        //}         
    }

    public string Parameter()
    {
        string param = "";
        if (ddlCompany.SelectedIndex>0)
        {

            param = param + " AND EGI.CompanyId='"+ddlCompany.SelectedValue+"' ";
        }
        //if (ddlDepartment.SelectedIndex>0)
        //{
        //    param = param + " AND EGI.DepartmentId='" + ddlDepartment.SelectedValue + "' ";
            
        //}

      
        if (startDate.Text !=string.Empty && endDate.Text!=string.Empty)
        {
            param = param + " AND (ISNULL(EGI.ExtProbationPeriodDate,EGI.ProbationEndDate) BETWEEN '" + startDate.Text + "' AND '" + endDate.Text + "')";
        }

        if (startDate.Text != string.Empty && endDate.Text == string.Empty)
        {
            param = param + " AND (ISNULL(EGI.ExtProbationPeriodDate,EGI.ProbationEndDate) BETWEEN '" + startDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "')";
        }

        if (startDate.Text == string.Empty && endDate.Text != string.Empty)
        {
            param = param + " AND (ISNULL(EGI.ExtProbationPeriodDate,EGI.ProbationEndDate) BETWEEN '" + endDate.Text + "' AND '" + endDate.Text + "')";
        }
        return param;
    }
    private void LoadInitialDDL()
    {
        _commonDataLoad.GetCompanyListIntoDropdown(ddlCompany);
    }

    //Response.Redirect("MPBudgetEntry.aspx");
    protected void btn_New_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("MPBudgetEntry.aspx");
    }

    [WebMethod(EnableSession = true)]
    public static List<MPBudgetViewModel> LoadMPBudgetTable()
    {
        MPBudgetCommonDAL _mpBudgetCommonDal = new MPBudgetCommonDAL();
       return _mpBudgetCommonDal.GetMPBudgetList();
    }

    [WebMethod(EnableSession = true)]
    public static List<MPBudgetViewModel> FilterMPBudgetTable(string company,string dept, string finyear)
    {
        MPBudgetCommonDAL _mpBudgetCommonDal = new MPBudgetCommonDAL();
      
            return _mpBudgetCommonDal.FilterMPBudgetTable(company, dept, finyear);
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteMPBudgetRowByMId(string MPBudgetId)
    {
        string status = "ok";
        int mid = int.Parse(MPBudgetId);
        try
        {
            using (var db = new HRIS_SMCEntities())
            {
                var mpb = (from u in db.tblMPBudgetMasters where u.MPBudgetMasterId ==  mid select u).FirstOrDefault();
                mpb.IsActive = false;
                mpb.UpdateDate = DateTime.Now;
                mpb.UpdateBy = _userId;
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            status = ex.Message;
        }
        return status;
    }


    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (ddlCompany.SelectedIndex > 0)
        {
            Session["cid"] = ddlCompany.SelectedValue;
            using (DataTable dt = _commonDataLoad.GetDDLDepartmentByCompanyUserId(int.Parse(ddlCompany.SelectedValue)))
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataValueField = "Value";
                ddlDepartment.DataTextField = "TextField";
                ddlDepartment.DataBind();
            }
            //using (DataTable dt = _commonDataLoad.GetDDLFinYearByCompanyId(int.Parse(ddlCompany.SelectedValue)))
            //{
            //    ddlFinYear.DataSource = dt;
            //    ddlFinYear.DataValueField = "Value";
            //    ddlFinYear.DataTextField = "TextField";
            //    ddlFinYear.DataBind();
            //}

        }
    }


    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                //string filepath = Path.GetDirectoryName(Request.Path);
                //filepath = filepath.TrimStart('\\');
                //string exten = Path.GetExtension(Request.Path);
                //if (exten == string.Empty)
                //{
                //    filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
                //}
                //else
                //{
                //    filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
                //}
                //Session["ApprovalPage"] = filepath;
                string id = datKey["MPBudgetMasterId"].ToString();
                HiddenField HDEmpID = (HiddenField)loadGridView.Rows[0].FindControl("HDEmpID");
                HiddenField hdpkd = (HiddenField)loadGridView.Rows[0].FindControl("hdpkd");
                //string did = datKey["MPBudgetDetailsId"].ToString();
                Session["Status"] = "Edit";
                Response.Redirect("MPBudgetEntry.aspx?mid=" + hdpkd.Value + "&EmpID=" + HDEmpID.Value);
                //+ "&mdid=" + loadGridView.DataKeys[rowindex][1].ToString());    

            }

            

        }
        if (e.CommandName == "Evaluate")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                //string filepath = Path.GetDirectoryName(Request.Path);
                //filepath = filepath.TrimStart('\\');
                //string exten = Path.GetExtension(Request.Path);
                //if (exten == string.Empty)
                //{
                //    filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
                //}
                //else
                //{
                //    filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
                //}
                //Session["ApprovalPage"] = filepath;
                string id = datKey["TrainingRecordMasterId"].ToString();



                HiddenField HDEmpID = (HiddenField)loadGridView.Rows[rowindex].FindControl("HDEmpID");
                HiddenField hdpkd = (HiddenField)loadGridView.Rows[rowindex].FindControl("hdpkd");
                //string did = datKey["MPBudgetDetailsId"].ToString();
                //Session["Status"] = "Edit";
                Response.Redirect("TrainingEvaluationForm.aspx?id=" + hdpkd.Value + "&EmpID=" + HDEmpID.Value);
                //+ "&mdid=" + loadGridView.DataKeys[rowindex][1].ToString());    

            }

            

        }

          if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var datKey = loadGridView.DataKeys[rowindex];

            if (datKey != null)
            {


                string id = datKey["MPBudgetMasterId"].ToString();
               // string did = datKey["MPBudgetDetailsId"].ToString();
                Session["Status"] = "View";
                Response.Redirect("MPBudgetEntry.aspx?mid=" + loadGridView.DataKeys[rowindex][0].ToString());// + "&mdid=" + loadGridView.DataKeys[rowindex][1].ToString());
            }
        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var datKey = loadGridView.DataKeys[rowindex];

            if (datKey != null)
            {


                string id = datKey["MPBudgetMasterId"].ToString();
               // string did = datKey["MPBudgetDetailsId"].ToString();
                Session["Status"] = "Delete";
                Response.Redirect("MPBudgetEntry.aspx?mid=" + loadGridView.DataKeys[rowindex][0].ToString());// + "&mdid=" + loadGridView.DataKeys[rowindex][1].ToString());    
            }
        }

    }

    protected void btnFilterSearch_OnClick(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex>0)
        {


          

            LoadManpowerBudgetInfo();
        }
        else
        {
            aShowMessage.ShowMessageBox("Please select a compnay", this);
            ddlCompany.Focus();
        }
        
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void vcchomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }  
    protected void btnExport_OnClick(object sender, EventArgs e)
    {
         if (loadGridView.Rows.Count>0)
        {
        string attachment = "attachment; filename=ProbationListInfo.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        loadGridView.AllowPaging = false;



        loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
                    false;
        //loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
        //   false;
        //loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
        //   false;

        this.LoadManpowerBudgetInfo();

        // Create a form to contain the grid  
        HtmlForm frm = new HtmlForm();
        loadGridView.Parent.Controls.Add(frm);
        //frm.Attributes["runat"] = "server";
        //frm.Controls.Add(loadGridView);
        //frm.RenderControl(htw);

        loadGridView.HeaderRow.Style.Add("background-color", "#E5EEF1");

        // Set background color of each cell of GridView1 header row
        foreach (TableCell tableCell in loadGridView.HeaderRow.Cells)
        {
            tableCell.Style["background-color"] = "#E5EEF1";
        }

        // Set background color of each cell of each data row of GridView1
        foreach (GridViewRow gridViewRow in loadGridView.Rows)
        {
            gridViewRow.BackColor = System.Drawing.Color.White;

            foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
            {
                gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

            }
        }

        loadGridView.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
        }
         else
         {
             showMessageBox("No Data Found!!");
         }
        
    }
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
}