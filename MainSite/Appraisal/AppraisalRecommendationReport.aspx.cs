using System;
using System.Activities.Statements;
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
using DAL.Permission_DAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_AppraisalRecommendationReport : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private KPIInformationViewDAL _aFincDal = new KPIInformationViewDAL();
    ShowMessage aShowMessage = new ShowMessage();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();

            LoadInitialDDL();

          //  UserPersmissionValidation();

            //DataTable dt = _aFincDal.GetSelfAppraisalList();
            //DataTable dt = _aFincDal.GetAppraisalByKpiPermission( );

            //gv_JdBoard.DataSource = dt;
            //gv_JdBoard.DataBind();

        }
        try
        {

            gv_JdBoard.UseAccessibleHeader = true;
            gv_JdBoard.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv_JdBoard.FooterRow.TableSection = TableRowSection.TableFooter;
        }
        catch (Exception)
        {

            //throw;
        }

    }


    private void LoadInitialDDL()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
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

                    detailsViewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    gv_JdBoard.Columns[gv_JdBoard.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    gv_JdBoard.Columns[gv_JdBoard.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    gv_JdBoard.Columns[gv_JdBoard.Columns.Count - 3].Visible =
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
        Response.Redirect("AppraisalSelfFunctional.aspx");
    }

    protected void btn_edit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");

        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("AppraisalSelfMasterId");
        Session["Status"] = "Edit";



        Response.Redirect("AppraisalSelfFunctional.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "");
    }

    protected void btn_eview_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");

        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("AppraisalSelfMasterId");
        Session["Status"] = "Edit";


 
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('KPIInformationDetailsView.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "' ,'_blank');", true);
    }


    protected void btn_print_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");

        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("AppraisalSelfMasterId");


        string url = "../Report_UI/KPIInformationReportViewer.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value ;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
       
       // Response.Redirect("../Report_UI/KPIInformationReportViewer.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "");
    }

    protected void btn_view_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");

        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("AppraisalSelfMasterId");
        Session["Status"] = "Edit";



        Response.Redirect("KPIInformationDetailsView.aspx?EmpInfoId=" + EmpInfoId.Value + "&financialYearId=" + FinancialYearId.Value + "");
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Session["cid"] = ddlCompany.SelectedValue;
        DataTable dt = _aFincDal.GetFianncialYearByComIdDDl(Convert.ToInt32(ddlCompany.SelectedValue));
        ddlFinancialYear.DataSource = dt;
        ddlFinancialYear.DataValueField = "Value";
        ddlFinancialYear.DataTextField = "TextField";
        ddlFinancialYear.DataBind();



        using (DataTable dt2 = _commonDataLoad.GetDDLComDepartment(ddlCompany.SelectedValue))
        {
            ddlDept.DataSource = dt2;
            ddlDept.DataValueField = "Value";
            ddlDept.DataTextField = "TextField";
            ddlDept.DataBind();
        }

      
    }

    protected void ddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        try
        {

            if (ddlCompany.Items.Count > 0)
            {
                if (ddlCompany.SelectedIndex > 0)
                {

                    DataTable dt = _aFincDal.GetAppraisalRecommendationReport(Parameter(), Parameter_SP());

                    if (dt.Rows.Count > 0)
                    {
                        gv_JdBoard.DataSource = dt;
                        gv_JdBoard.DataBind();
                        gv_JdBoard.UseAccessibleHeader = true;
                        gv_JdBoard.HeaderRow.TableSection = TableRowSection.TableHeader;
                        gv_JdBoard.FooterRow.TableSection = TableRowSection.TableFooter;


                        ViewState["EmpSetup"] = dt;
                    }
                    else
                    {
                        aShowMessage.ShowMessageBox("No Data Found!!", this);

                        gv_JdBoard.DataSource = null;
                        gv_JdBoard.DataBind();
                    }
                }
                else
                {
                    gv_JdBoard.DataSource = null;
                    gv_JdBoard.DataBind();
                    aShowMessage.ShowMessageBox("Please Select Company!!", this);
                    
                }
            }
            else
            {
                gv_JdBoard.DataSource = null;
                gv_JdBoard.DataBind();
                aShowMessage.ShowMessageBox("Please Select Company!!", this);
            }

        }
        catch (Exception)
        {
            gv_JdBoard.DataSource = null;
            gv_JdBoard.DataBind();
            ViewState["EmpSetup"] = null;
            aShowMessage.ShowMessageBox("No Data Found!!", this);

            //throw;
        }
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
    public string Parameter()
    {
        string param = "";

          if (ddlCompany.Items.Count > 0)
        {
            if (ddlCompany.SelectedIndex > 0)
            {
                param = param + " AND EG.CompanyId='" + ddlCompany.SelectedValue + "' ";
            }
        }
       
        if (ddlFinancialYear.Items.Count > 0)
        {
            if (ddlFinancialYear.SelectedIndex > 0)
            {
                param = param + " AND d.FinancialYearDesc='" + ddlFinancialYear.SelectedItem.Text + "' ";
            }
        }
        if (ddlDept.Items.Count > 0)
        {
            if (ddlDept.SelectedIndex > 0)
            {
                param = param + " AND e.DepartmentId='" + ddlDept.SelectedValue + "' ";
            }
        }

        //param = param +
        //       " AND  tblAppraisalSelfAppLog.Version=CELog.MaxVer  ";    //AND tblAppraisalMasterAppLog.Version=ALog.MaxVer

        return param;

    }

    public string Parameter_SP()
    {
        string param = "";

        //if (ddlCompany.Items.Count > 0)
        //{
        //    if (ddlCompany.SelectedIndex > 0)
        //    {
        //        param = param + " AND EG.CompanyId='" + ddlCompany.SelectedValue + "' ";
        //    }
        //}

        if (ddlFinancialYear.Items.Count > 0)
        {
            if (ddlFinancialYear.SelectedIndex > 0)
            {
                param = param + " AND d.FinancialYearDesc='" + ddlFinancialYear.SelectedItem.Text + "' ";
            }
        }
        if (ddlDept.Items.Count > 0)
        {
            if (ddlDept.SelectedIndex > 0)
            {
                param = param + " AND e.DepartmentId='" + ddlDept.SelectedValue + "' ";
            }
        }

        //param = param +
        //       " AND  tblAppraisalSelfAppLog.Version=CELog.MaxVer  ";    //AND tblAppraisalMasterAppLog.Version=ALog.MaxVer

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
        if (e.CommandName=="A")
        {
            
        }
    }

   

    protected void btn_KPI_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");

        HiddenField hfAppraisalMasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfAppraisalMasterId");
        
        HiddenField hfAppraisalSelfMasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfAppraisalSelfMasterId");


        Label ActionStatus = (Label)gv_JdBoard.Rows[rowID].FindControl("ActionStatus");

        isKPI.Checked = false;

        if (ActionStatus.Text.Trim() == "Approved")
        {
            aShowMessage.ShowMessageBox("Already Approved!!", this);
        }
        else if (ActionStatus.Text.Trim() == "Not Approved")
        {
            mpFunctionalSup.Show();
            isKPI.Checked = true;
          
            using (DataTable dt = _commonDataLoad.GetEmpDDLAActive(ddlCompany.SelectedValue.ToString())
              )
            {



                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;



                ddlForwordEmp.DataSource = dt;
                ddlForwordEmp.DataValueField = "EmpInfoId";
                ddlForwordEmp.DataTextField = "EmpName";
                ddlForwordEmp.DataBind();
                ddlForwordEmp.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlForwordEmp.SelectedIndex = 0;
            }

            DataTable dtapprove = _aFincDal.GetKPIApprovaer(Convert.ToInt32(hfAppraisalSelfMasterId.Value));
            if (dtapprove.Rows.Count > 0)
            {
                ddlEmpInfo.SelectedValue = dtapprove.Rows[0]["ForEmpInfoId"].ToString();
                hfMasterId.Value = dtapprove.Rows[0]["AppraisalSelfAppLogId"].ToString();
            }
            lblHeader.InnerText = " KPI Reset Approval Person";
         
        }
        else
        {
            aShowMessage.ShowMessageBox("KPI has not set yet", this);
            
        }
    }

    protected void btnApprisal_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField EmpInfoId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("EmpInfoId");
        HiddenField FinancialYearId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("FinancialYearId");

        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("AppraisalSelfMasterId");


        HiddenField hfAppraisalMasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfAppraisalMasterId");

        HiddenField hfAppraisalSelfMasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfAppraisalSelfMasterId");

        Label Awaitingf = (Label)gv_JdBoard.Rows[rowID].FindControl("Awaitingf");

        isKPI.Checked = false;
        if (Awaitingf.Text.Trim() == "Approved")
        {
            aShowMessage.ShowMessageBox("Already Approved!!", this);
        }
        else if (Awaitingf.Text.Trim() == "Not Approved")
        {
            isKPI.Checked = false;

            

            using (DataTable dt = _commonDataLoad.GetEmpDDLAActive(ddlCompany.SelectedValue.ToString())
                )
            {



                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;



                ddlForwordEmp.DataSource = dt;
                ddlForwordEmp.DataValueField = "EmpInfoId";
                ddlForwordEmp.DataTextField = "EmpName";
                ddlForwordEmp.DataBind();
                ddlForwordEmp.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlForwordEmp.SelectedIndex = 0;
            }

            DataTable dtapprove = _aFincDal.GetApprisalApprovaer(Convert.ToInt32(hfAppraisalMasterId.Value));
              if (dtapprove.Rows.Count > 0)
            {
                ddlEmpInfo.SelectedValue = dtapprove.Rows[0]["ForEmpInfoId"].ToString();
                hfMasterId.Value = dtapprove.Rows[0]["AppraisalMasterAppLogId"].ToString();
            }
              lblHeader.InnerText = " Appraisal Reset Approval Person";  
            mpFunctionalSup.Show();

        }
        else
        {
            aShowMessage.ShowMessageBox("Appraisal has not set yet", this);
            
        }
    }

    protected void btnFunctionalSupClose_Click(object sender, EventArgs e)
    {
      mpFunctionalSup.Hide();
    }

    protected void btnAppraisalFuncSUPSave_OnClick(object sender, EventArgs e)
    {

        if (ddlForwordEmp.SelectedValue!="")
        {
            bool status = false;
            if (isKPI.Checked)
            {
                status = _aFincDal.UpdateKPIApprovePersonContractural(hfMasterId.Value, ddlForwordEmp.SelectedValue, ddlEmpInfo.SelectedValue);
            }
            else
            {
                status = _aFincDal.UpdateAppprisalApprovePersonContractural(hfMasterId.Value, ddlForwordEmp.SelectedValue, ddlEmpInfo.SelectedValue);

            }

            if (status)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Operation Successfully...');window.location ='KPIInformationView.aspx';",
                       true);
            }
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select Forward Approval Person", this);
            ddlForwordEmp.Focus();
        }
       
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        // //required to avoid the runtime error "  
        //Control 'CheckupGridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (gv_JdBoard.Rows.Count > 0)
        {
            string attachment = "attachment; filename=Appraisal_Recommendation_Information.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv_JdBoard.AllowPaging = false;

 
           gv_JdBoard.Columns[gv_JdBoard.Columns.Count -1].Visible = false;
            //gv_JdBoard.Columns[gv_JdBoard.Columns.Count - 2].Visible = false;
            //gv_JdBoard.Columns[gv_JdBoard.Columns.Count - 3].Visible = false;


            // Create a form to contain the grid  
            HtmlForm frm = new HtmlForm();
            gv_JdBoard.Parent.Controls.Add(frm);
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(loadGridView);
            //frm.RenderControl(htw);

            gv_JdBoard.HeaderRow.Style.Add("background-color", "#E5EEF1");

            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in gv_JdBoard.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in gv_JdBoard.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                }
            }
            string com = "";
            if(ddlCompany.SelectedValue=="1")
            {
                com = "Social Marketing Company";
            }
            if (ddlCompany.SelectedValue == "2")
            {
                com = "SMC Enterprise Ltd.";
            }
            gv_JdBoard.RenderControl(htw);
            string headerTable = @"<span   style='text-align:center'><h3>" + com +
                                 " </h3><h4>Appraisal Recommendation Information</h4>  </span> <span   style='text-align:right'><h5> Print Date: " +
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
}