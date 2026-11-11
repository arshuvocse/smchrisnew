using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.HealthCare_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class HealthCare_UI_HealthcareReport : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private KPIInformationViewDAL _aFincDal = new KPIInformationViewDAL();
    ShowMessage aShowMessage = new ShowMessage();
    PermissionDAL aPermissionDal = new PermissionDAL();
    private HRPanelDal aPanelDal = new HRPanelDal();

    private ReimbursmentFormDal formDal = new ReimbursmentFormDal();

    int visibleRowCount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();

            LoadInitialDDL();
           // load_HR_Panel();
            // load_HR_Panel();

            //  UserPersmissionValidation();

            //DataTable dt = _aFincDal.GetSelfAppraisalList();
            //DataTable dt = _aFincDal.GetAppraisalByKpiPermission( );

            //gv_OPD.DataSource = dt;
            //gv_OPD.DataBind();         
        }
        

    }
    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue != "")
        {
            
            _commonDataLoad.GetDepartmentByDivList(ddlDept, ddlDivision.SelectedValue); 
        }
        else
        {
            ddlDept.Items.Clear();
        }
    }

    protected void ActiveStatusDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (ActiveStatusDropDownList.SelectedValue == "-1")
        {
            using (DataTable dt = _commonDataLoad.GetEmpDDL(ddlCompany.SelectedValue.ToString()))
            {
                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;

            }
        }
        else if (ActiveStatusDropDownList.SelectedValue == "1")
        {
            using (DataTable dt = _commonDataLoad.GetEmpDDLAActiveOnlyView(ddlCompany.SelectedValue.ToString()))
            {
                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;
            }
        }
        else if (ActiveStatusDropDownList.SelectedValue == "0")
        {
            using (DataTable dt = _commonDataLoad.GetEmpDDLAInactive(ddlCompany.SelectedValue.ToString()))
            {
                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;

            }
        }

    }
    private void LoadInitialDDL()
    {
        using (DataTable dt = _commonDataLoad.GetCompany())
        {

            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
            ddlCompany.SelectedIndex = 1;
            ddlCompany_OnSelectedIndexChanged(null, null);
        }

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

                    gv_OPD.Columns[gv_OPD.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    gv_OPD.Columns[gv_OPD.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    gv_OPD.Columns[gv_OPD.Columns.Count - 3].Visible =
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

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
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
        Response.Redirect("~/Appraisal/AppraisalSelfFunctional.aspx");
    }

    protected void btn_edit_OnClick(object sender, EventArgs e)
    {
        //LinkButton lb = (LinkButton)sender;
        //GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //int rowID = gvRow.RowIndex;

        //HiddenField EmpInfoId = (HiddenField)gv_OPD.Rows[rowID].FindControl("EmpInfoId");
        //HiddenField FinancialYearId = (HiddenField)gv_OPD.Rows[rowID].FindControl("FinancialYearId");

        //HiddenField mastrId = (HiddenField)gv_OPD.Rows[rowID].FindControl("AppraisalSelfMasterId");
        //Session["Status"] = "Edit";

        //Response.Redirect("AppraisalSelfFunctional.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "");
    }





    protected void btn_eview_OnClick(object sender, EventArgs e)
    {

        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField hfMasterId = (HiddenField)gv_OPD.Rows[rowID].FindControl("hfAppraisalSelfMasterId");
        Label Status = (Label)gv_OPD.Rows[rowID].FindControl("ActionStatus");

        string ForEmpId =  gv_OPD.DataKeys[rowID][0].ToString();

        if (Session["EmpInfoId"] != null)
        {


            Response.Redirect("ExpenseReimbursementFormForHRPayment.aspx?MID=" + hfMasterId.Value.Trim() + "&Mode=HR");

            //if (Session["EmpInfoId"].ToString() == ForEmpId)
            //{
            //    //if (Status.Text.Trim() == "Approved" || Status.Text.Trim() == "Review" || Status.Text.Trim() == "Draft")
            //    //{
            //    //    aShowMessage.ShowMessageBox("Can not Edit", this);
            //    //}
            //    //else
            //    //{
            //    //    Response.Redirect("ExpenseReimbursementFormSelfEntry.aspx?MID=" + hfMasterId.Value.Trim());
            //    //}

            //    Response.Redirect("ExpenseReimbursementFormSelfEntry.aspx?MID=" + hfMasterId.Value.Trim());
            //}
            //else
            //{
            //    aShowMessage.ShowMessageBox("Can not edit", this);
            //}
        }

        //if (dt.Rows.Count > 0)
        //{
        //    if (dt.Rows[0]["ActionStatus"].ToString().Trim() == "Draft" || dt.Rows[0]["ActionStatus"].ToString().Trim() == "Review")
        //    {
        //        Response.Redirect("ExpenseReimbursementFormSelfEntry.aspx?MID=" + hfMasterId.Value.Trim());
        //    }
        //    else
        //    {

        //        AlertMessageBoxShow("You Can not edit this.....");

        //    }
        //}

 
    }

    protected void btn_Fview_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton) sender;
        GridViewRow gvRow = (GridViewRow) lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        string MasterId =  gv_OPD.DataKeys[rowID]["ReimbursFromMasterId"].ToString();
        DataTable DT = aPanelDal.Get_CommitteeFeedback(MasterId);
        if (DT.Rows.Count > 0)
        {
            gv_EmpListSearch.DataSource = DT;
            gv_EmpListSearch.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal23').modal('show')", true);

               // data-toggle="modal" data-target="#exampleModal23"
        }
        else
        {
            aShowMessage.ShowMessageBox("There is no feedback for this Application", this);
        }


        //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('KPIInformationDetailsView.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "' ,'_blank');", true);
    }


    protected void btn_print_OnClick(object sender, EventArgs e)
    {

        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        Session["dsdadf"] = "";
        Session["dsdadf"] = "View";
        HiddenField MasterId = (HiddenField)gv_OPD.Rows[rowID].FindControl("hfAppraisalSelfMasterId");
      //  Response.Redirect("../HealthCare_UI/ApplicationView_HR.aspx?MID=" + MasterId.Value.Trim());


        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('ApplicationView_HR.aspx?MID=" + MasterId.Value.Trim() + "' ,'_blank');", true);

     //   string url = "../HealthCare_UI/ApplicationView_HR.aspx?MID=" + MasterId.Value;
      //  string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
      //  ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
  

        //LinkButton lb = (LinkButton)sender;
        //GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //int rowID = gvRow.RowIndex;

        //HiddenField EmpInfoId = (HiddenField)gv_OPD.Rows[rowID].FindControl("EmpInfoId");
        //HiddenField FinancialYearId = (HiddenField)gv_OPD.Rows[rowID].FindControl("FinancialYearId");

        //HiddenField mastrId = (HiddenField)gv_OPD.Rows[rowID].FindControl("AppraisalSelfMasterId");


        //string url = "../Report_UI/KPIInformationReportViewer.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value;
        //string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);

        // Response.Redirect("../Report_UI/KPIInformationReportViewer.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "");
    }

    protected void btn_view_OnClick(object sender, EventArgs e)
    {
        //LinkButton lb = (LinkButton)sender;
        //GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //int rowID = gvRow.RowIndex;

        //HiddenField EmpInfoId = (HiddenField)gv_OPD.Rows[rowID].FindControl("EmpInfoId");
        //HiddenField FinancialYearId = (HiddenField)gv_OPD.Rows[rowID].FindControl("FinancialYearId");

        //HiddenField mastrId = (HiddenField)gv_OPD.Rows[rowID].FindControl("AppraisalSelfMasterId");
        //Session["Status"] = "Edit";



        //Response.Redirect("KPIInformationDetailsView.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "");
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Session["cid"] = ddlCompany.SelectedValue;
        DataTable dt = _aFincDal.GetFianncialYearByComIdDDl(Convert.ToInt32(ddlCompany.SelectedValue));
        ddlFinancialYear.DataSource = dt;
        ddlFinancialYear.DataValueField = "Value";
        ddlFinancialYear.DataTextField = "TextField";
        ddlFinancialYear.DataBind();

        ActiveStatusDropDownList_OnSelectedIndexChanged(null, null);

        //using (DataTable dt2 = _commonDataLoad.GetDDLComDepartment(ddlCompany.SelectedValue))
        //{
        //    ddlDept.DataSource = dt2;
        //    ddlDept.DataValueField = "Value";
        //    ddlDept.DataTextField = "TextField";
        //    ddlDept.DataBind();
        //}

        using (DataTable dt2 = _commonDataLoad.GetDDLComDivision(ddlCompany.SelectedValue))
        {
            ddlDivision.DataSource = dt2;
            ddlDivision.DataValueField = "Value";
            ddlDivision.DataTextField = "TextField";
            ddlDivision.DataBind();
        }


    }

    protected void ddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
       
        if (ddlActoinStatus.SelectedValue != "Review")
        {

            gridContainer14.Visible = false;
            gridContainer1.Visible = true;

            load_HR_Panel();
        }
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        if (ddlActoinStatus.SelectedValue != "Review")
        {
           
            gridContainer14.Visible = false;
            gridContainer1.Visible = true;
                
            load_HR_Panel();
        }
        else
        {

            DataTable Dt = aPanelDal.Get_HRPanel_ReturnApplication("");
            if (Dt.Rows.Count > 0)
            {

                GridView1.DataSource = null;
                GridView1.DataBind();

                gridContainer14.Visible = true;
                gridContainer1.Visible = false;
                
                for (int i = 0; i < Dt.Rows.Count; i++)
                {

                    string hfSalaryLoationId = Dt.Rows[i]["SalaryLoationId"].ToString();
                    string hfApplicationType = Dt.Rows[i]["Type"].ToString();

                    DataTable DtCheck = aPanelDal.Get_HRPanelCheckCommeti(hfApplicationType, hfSalaryLoationId);
                    if (DtCheck.Rows.Count == 0)
                    {
                        Dt.Rows.RemoveAt(i);
                        i--;
                    }
                }

                GridView1.DataSource = Dt;
                GridView1.DataBind();


            }

        }
  
    }


    private void load_HR_Panel()
    {
        try
        {

            gv_OPD.DataSource = null;
            gv_OPD.DataBind();

            gv_IPD.DataSource = null;
            gv_IPD.DataBind();

            DataTable Dt = new DataTable();
            if (ddlType.SelectedValue == "OPD" || ddlType.SelectedValue == "Special")
            {
                Dt = aPanelDal.Get_HealthCareReport(Parameter(), ddlType.SelectedValue);

                gv_OPD.DataSource = Dt;
                gv_OPD.DataBind();

                try
                {

                    gv_OPD.FooterRow.Cells[11].Text = "Total:";



                    decimal ApplicableAmount = Dt.AsEnumerable().Sum(row => row.Field<decimal?>("ApplicableAmount") == null ? 0 : row.Field<decimal>("ApplicableAmount"));
                    gv_OPD.FooterRow.Cells[12].Text = Math.Round(ApplicableAmount).ToString("#,##0");



                    decimal Ceilling = Dt.AsEnumerable().Sum(row => row.Field<decimal?>("Ceilling") == null ? 0 : row.Field<decimal>("Ceilling"));
                    gv_OPD.FooterRow.Cells[13].Text = Math.Round(Ceilling).ToString("#,##0");



                    decimal RemainingBalance = Dt.AsEnumerable().Sum(row => row.Field<decimal?>("RemainingBalance") == null ? 0 : row.Field<decimal>("RemainingBalance"));
                    gv_OPD.FooterRow.Cells[14].Text = Math.Round(RemainingBalance).ToString("#,##0");



                    gv_OPD.FooterRow.BackColor = System.Drawing.Color.Bisque;
                    gv_OPD.FooterRow.Font.Bold = true;
                    gv_OPD.FooterRow.HorizontalAlign = HorizontalAlign.Right;
                }

                catch { }
            }
            else
            {
                Dt = aPanelDal.Get_HealthCareReport(Parameter(), ddlType.SelectedValue);


                gv_IPD.DataSource = Dt;
                gv_IPD.DataBind();

                try
                {

                    gv_IPD.FooterRow.Cells[12].Text = "Total:";

                    decimal ApplicableAmount = Dt.AsEnumerable().Sum(row => row.Field<decimal?>("ApplicableAmount") == null ? 0 : row.Field<decimal>("ApplicableAmount"));
                    gv_IPD.FooterRow.Cells[13].Text = Math.Round(ApplicableAmount).ToString("#,##0");

                

                    decimal Ceilling = Dt.AsEnumerable().Sum(row => row.Field<decimal?>("Ceilling") == null ? 0 : row.Field<decimal>("Ceilling"));
                    gv_IPD.FooterRow.Cells[14].Text = Math.Round(Ceilling).ToString("#,##0");

                

                    decimal RemainingBalance = Dt.AsEnumerable().Sum(row => row.Field<decimal?>("RemainingBalance") == null ? 0 : row.Field<decimal>("RemainingBalance"));
                    gv_IPD.FooterRow.Cells[15].Text = Math.Round(RemainingBalance).ToString("#,##0");

                

                    gv_IPD.FooterRow.BackColor = System.Drawing.Color.Bisque;
                    gv_IPD.FooterRow.Font.Bold = true;
                    gv_IPD.FooterRow.HorizontalAlign = HorizontalAlign.Right;
                }

                catch { }
            }
          
          
            
            //gv_OPD.UseAccessibleHeader = true;
         //gv_OPD.HeaderRow.TableSection = TableRowSection.TableHeader;
         //if (gv_OPD.TopPagerRow != null)
         //{
         //    gv_OPD.TopPagerRow.TableSection = TableRowSection.TableHeader;
         //}
         //if (gv_OPD.BottomPagerRow != null)
         //{
         //    gv_OPD.BottomPagerRow.TableSection = TableRowSection.TableFooter;
         //}
        }
        catch (Exception)
        {
            //throw;
        }


      
    }

    public string Parameter()
    {
        string param = "";

        if (ddlCompany.SelectedValue != "")
        {
            param = param + " AND RM.CompanyId='" + ddlCompany.SelectedValue + "' ";
        }


        if (ddlFinancialYear.Items.Count > 0)
        {
            if (ddlFinancialYear.SelectedIndex > 0)
            {
                param = param + " AND RM.FinancialYearId='" + ddlFinancialYear.SelectedValue + "' ";
            }
        }

        if (ddlDept.Items.Count > 0)
        {
            if (ddlDept.SelectedIndex > 0)
            {
                param = param + " AND RM.DepartmentId='" + ddlDept.SelectedValue + "' ";
            }
        }
        if (ddlDivision.Items.Count > 0)
        {
            if (ddlDivision.SelectedIndex > 0)
            {
                param = param + " AND RM.DivisionId='" + ddlDivision.SelectedValue + "' ";
            }
        }

        if (ddlEmpInfo.Items.Count > 0)
        {
            if (ddlEmpInfo.SelectedIndex > 0)
            {
                param = param + " AND RM.EmpInfoId='" + ddlEmpInfo.SelectedValue + "' ";
            }
        }

        if (ddlActoinStatus.SelectedValue != "")
        {
            param = param + " AND RM.ActionStatus='" + ddlActoinStatus.SelectedValue + "' ";
        }

        if (ddlType.SelectedValue != "")
        {
            param = param + " AND RM.Type='" + ddlType.SelectedValue + "' ";
        }

        if (App_FromDate.Text.Trim() != "" && App_ToDate.Text.Trim() !="")
        {
            param = param + " AND RM.SubmitDate  between '" + App_FromDate.Text.Trim() + "' and '" + App_ToDate.Text.Trim() + "'  ";
        }
     
        if (App_FromDate.Text.Trim() != "" && App_ToDate.Text.Trim() == "")
        {
            param = param + " AND RM.SubmitDate  between '" + App_FromDate.Text.Trim() + "' and '" + DateTime.Now + "'  ";
        }

        if (App_FromDate.Text.Trim() == "" && App_ToDate.Text.Trim() != "")
        {
            param = param + " AND  RM.SubmitDate  between '" + DateTime.Now + "' and '" + App_ToDate.Text.Trim() + "' ";
        }


        return param;
    }

    public string Parameter2()
    {
        string param = "";
        if (ddlFinancialYear.Items.Count > 0)
        {
            if (ddlFinancialYear.SelectedIndex > 0)
            {
                param = param + " AND a.FinancialYearId='" + ddlFinancialYear.SelectedValue + "' ";
            }
        }

        if (ddlDept.Items.Count > 0)
        {
            if (ddlDept.SelectedIndex > 0)
            {
                param = param + " AND e.DepartmentId='" + ddlDept.SelectedValue + "' ";
            }
        }

        param = param + " AND  tblAppraisalSelfAppLog.Version=CELog.MaxVer ";
        // param = param + " AND  tblAppraisalSelfAppLog.Version=CELog.MaxVer ";
        // param = param + " AND  tblAppraisalSelfAppLog.Version=CELog.MaxVer ";

        return param;

    }

    protected void gv_JdBoard_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "A")
        {

        }
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        // //required to avoid the runtime error "  
        //Control 'CheckupGridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (gv_OPD.Rows.Count > 0)
        {
            string attachment = "attachment; filename=Healthcare_Report_"+ ddlType.SelectedValue+"_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv_OPD.AllowPaging = false;
        
            //Create a form to contain the grid  
            HtmlForm frm = new HtmlForm();
            gv_OPD.Parent.Controls.Add(frm);
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(loadGridView);
            //frm.RenderControl(htw);
            gv_OPD.HeaderRow.Style.Add("background-color", "#E5EEF1");
            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in gv_OPD.HeaderRow.Cells)
            { 
                tableCell.Style["background-color"] = "#E5EEF1";
            }
            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in gv_OPD.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;
                    foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                    {
                        gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
                    }
            }

            string com = "";
            if (ddlCompany.SelectedValue == "1")
            {
                com = "Social Marketing Company";
            }

            if (ddlCompany.SelectedValue == "2")
            {
                com = "SMC Enterprise Ltd.";
            }

            gv_OPD.RenderControl(htw);
            string headerTable = @"<span   style='text-align:center'><h3>" + com +
                                 " </h3><h4> Health Care Application Form List</h4>  </span> <span   style='text-align:right'><h5> Print Date: " +
                                 DateTime.Now.ToString("dd-MMM-yyyy") + "</h5></span>";

            HttpContext.Current.Response.Write(headerTable);

            Response.Write(sw.ToString());
            Response.End();

        }


        if (gv_IPD.Rows.Count > 0)
        {
            string attachment = "attachment; filename=Healthcare_Report_" + ddlType.SelectedValue + "_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv_IPD.AllowPaging = false;

            //Create a form to contain the grid  
            HtmlForm frm = new HtmlForm();
            gv_IPD.Parent.Controls.Add(frm);
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(loadGridView);
            //frm.RenderControl(htw);
            gv_IPD.HeaderRow.Style.Add("background-color", "#E5EEF1");
            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in gv_IPD.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }
            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in gv_IPD.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
                }
            }

            string com = "";
            if (ddlCompany.SelectedValue == "1")
            {
                com = "Social Marketing Company";
            }

            if (ddlCompany.SelectedValue == "2")
            {
                com = "SMC Enterprise Ltd.";
            }

            gv_IPD.RenderControl(htw);
            string headerTable = @"<span   style='text-align:center'><h3>" + com +
                                 " </h3><h4> Health Care Report</h4>  </span> <span   style='text-align:right'><h5> Print Date: " +
                                 DateTime.Now.ToString("dd-MMM-yyyy") + "</h5></span>";

            HttpContext.Current.Response.Write(headerTable);

            Response.Write(sw.ToString());
            Response.End();

        }
        else
        {
            aShowMessage.ShowMessageBox("No Data Found!!", this);
        }
    }

    protected void gv_DocumentUpload_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView) sender;

        if ((gv.ShowHeader == true && gv.Rows.Count > 0)
            || (gv.ShowHeaderWhenEmpty == true))
        {
            //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }

    }


    protected void btn_MD_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton) sender;
        GridViewRow gvRow = (GridViewRow) lb.NamingContainer;
        int rowIndex = gvRow.RowIndex;
        HiddenField hffMasterId = (HiddenField)gv_OPD.Rows[rowIndex].FindControl("hfAppraisalSelfMasterId");
        HiddenField IsMdApproval = (HiddenField)gv_OPD.Rows[rowIndex].FindControl("HFIsMDApproval");
        HiddenField hfActionStatus = (HiddenField)gv_OPD.Rows[rowIndex].FindControl("hfActionStatus");
        LinkButton btn = (LinkButton)gv_OPD.Rows[rowIndex].FindControl("btn_MD");
        hfMasterId.Value = hffMasterId.Value;
        //if (hfActionStatus.Value.Trim() == "Approved")
        {
            if (IsMdApproval.Value == "False" || IsMdApproval.Value == "")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#MDModal').modal('show')", true);
              
            }
            else
            {
                aShowMessage.ShowMessageBox("Already Forwarded to MD Sir", this);
            }
        }
        //else
        //{
        //    aShowMessage.ShowMessageBox("Can Not Forward To MD Sir", this);
        //}


        //LinkButton lb = (LinkButton)sender;
        //GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //int rowID = gvRow.RowIndex;
        //Session["dsdadf"] = "";
        //Session["dsdadf"] = "View";
        //HiddenField hfMasterId = (HiddenField)loadGridView.Rows[rowID].FindControl("hfReimbursFromMasterId");

    }

    protected void btn_Return_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowIndex = gvRow.RowIndex;
        HiddenField hffMasterId = (HiddenField)gv_OPD.Rows[rowIndex].FindControl("hfAppraisalSelfMasterId");

         DataTable Dt = aPanelDal.Get_MaxLogInformation(hffMasterId.Value);

        if (Dt.Rows.Count > 0)
        {
          //   if (Session["EmpInfoId"].ToString() == Dt.Rows[0]["ForEmpInfoId"].ToString())
            {
                Session["HRReturn"] = "Return";
                Response.Redirect("ExpenseFormMDApproval.aspx?MID=" + hffMasterId.Value + "&PreeEmpId=" + Dt.Rows[0]["ForEmpInfoId"].ToString() + "&LogID=" + Dt.Rows[0]["ReimbursementSelfAppLogId"].ToString() + "");
            }
             //else
             //{
             //    aShowMessage.ShowMessageBox("Can not return Now", this);
             //}
        }
        else
        {
            aShowMessage.ShowMessageBox("Can not return Now", this);
        }

    }

    protected void btn_ForwardDoctor_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowIndex = gvRow.RowIndex;
        HiddenField hffMasterId = (HiddenField)gv_OPD.Rows[rowIndex].FindControl("hfAppraisalSelfMasterId");

        bool Status = formDal.ExpenseReimbursementForwardtoDoctor(hffMasterId.Value);
        if (Status)
        {
            aShowMessage.ShowMessageBox("Forward to doctor Successfully!", this);


            load_HR_Panel();

        }
    }

    protected void btnFtoMD_OnClick(object sender, EventArgs e)
    {
        //MDApprovalPermission- This Method also Use in ExpenseFormMDApproval Page  

        if (hfMasterId.Value!="")
        {

            bool status = aPanelDal.MDApprovalPermission(hfMasterId.Value, "1", txtcomments.Text.Trim());
            if (status)
            {
                //btn.Enabled = false;
                //btn.CssClass = "btn btn-sm btnMyDesignOne block";
                aShowMessage.ShowMessageBox("Forwarded To MD Sir", this);

                load_HR_Panel();
            }
        }
        
    }


    protected void loadGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_OPD.PageIndex = e.NewPageIndex;
        this. load_HR_Panel();
    }



    protected void gv_JdBoard_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                visibleRowCount++;
               Label labelSL = e.Row.FindControl("LabelSL") as Label;
                if (labelSL != null)
                {
                    labelSL.Text = visibleRowCount.ToString(); // Set the label text to the visible row count
                }
            }
        }
    }

    protected void btnReturnExport_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=Healthcare_Return_Report_" + ddlType.SelectedValue + "_" + DateTime.Now.ToString("dd_MMM_yyyy_hh_mm_tt") + ".xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);

        GridView1.AllowPaging = false;

        //Create a form to contain the grid  
        HtmlForm frm = new HtmlForm();
        GridView1.Parent.Controls.Add(frm);
        //frm.Attributes["runat"] = "server";
        //frm.Controls.Add(loadGridView);
        //frm.RenderControl(htw);
        GridView1.HeaderRow.Style.Add("background-color", "#E5EEF1");
        // Set background color of each cell of GridView1 header row
        foreach (TableCell tableCell in GridView1.HeaderRow.Cells)
        {
            tableCell.Style["background-color"] = "#E5EEF1";
        }
        // Set background color of each cell of each data row of GridView1
        foreach (GridViewRow gridViewRow in GridView1.Rows)
        {
            gridViewRow.BackColor = System.Drawing.Color.White;
            foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
            {
                gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
            }
        }

        string com = "";
        if (ddlCompany.SelectedValue == "1")
        {
            com = "Social Marketing Company";
        }

        if (ddlCompany.SelectedValue == "2")
        {
            com = "SMC Enterprise Ltd.";
        }

        GridView1.RenderControl(htw);
        string headerTable = @"<span   style='text-align:center'><h3>" + com +
                             " </h3><h4> Health Care Application Form List</h4>  </span> <span   style='text-align:right'><h5> Print Date: " +
                             DateTime.Now.ToString("dd-MMM-yyyy") + "</h5></span>";

        HttpContext.Current.Response.Write(headerTable);

        Response.Write(sw.ToString());
        Response.End();
    }
}
    