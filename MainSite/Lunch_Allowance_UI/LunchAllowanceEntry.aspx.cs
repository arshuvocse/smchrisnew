using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using DAL.COMMON_DAL;
using DAL.Increment_DAL;
using DAL.Lunch_Allowance_DAL;
using DAL.MenuSetup_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using ListItem = System.Web.UI.WebControls.ListItem;

public partial class Lunch_Allowance_UI_LunchAllowanceEntry : System.Web.UI.Page
{
    private static string _userId;
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    PermissionDAL aPermissionDal = new PermissionDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    LunchAllowanceCancelDAL allowanceCancelDal = new LunchAllowanceCancelDAL();

    private int mid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {
           // ButtonVisible();
            LoadInitialDDL();
            GetCompany();
     //    UserPersmissionValidation();
         //   LoadEMPInfo();

            SearchButton_OnClick(null, null);
        }
    }

    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {

            if (Session["Status"].ToString() == "Add")
            {
                btn_Save.Visible = true;
                 
            }
            
            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("LunchAllowanceView");
        }

    }




    protected void EmployeeDropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
       

        string empName = txtSearch.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            //EmployeeDropDownList.Text = emp[0];
            txtSearch.Text = emp[1];
        }
        //else
        //{
        //    txtSearch.Text = "";
        //    txtSearch.Text = "";
        //    //  EmpInfoIdHiddenField.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}
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

                    btn_Save.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                  
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

    UserCommonDAL _userCommonDal = new UserCommonDAL();

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
    private void LoadEMPInfo()
    {
        DataTable jobCreationInfos = new DataTable();
        jobCreationInfos = _userCommonDal.GetEMpInfosForLunchAllowance( GenerateParameter()  ,ddlCompany.SelectedValue);
        if (jobCreationInfos.Rows.Count>0)
        {
            
            
            gv_EmpInfoLoad.DataSource = jobCreationInfos;
            gv_EmpInfoLoad.DataBind();


            try
            {
                for (int i = 0; i < gv_EmpInfoLoad.Rows.Count; i++)
                {
                    if (jobCreationInfos.Rows.Count > 0)
                    {
                        ddlStatus_chang(i);
                        ((CalendarExtender)gv_EmpInfoLoad.Rows[i].FindControl("CalendawrExaqwtender1")).StartDate = DateTime.Now;
                        ((CalendarExtender)gv_EmpInfoLoad.Rows[i].FindControl("CalendarExstdaender1")).StartDate = DateTime.Now;
                        ((CalendarExtender)gv_EmpInfoLoad.Rows[i].FindControl("CalendawqewrExstender1")).StartDate = DateTime.Now;
                        try
                        {
                            ((DropDownList)gv_EmpInfoLoad.Rows[i].FindControl("ddlStatus")).SelectedValue = jobCreationInfos.Rows[i]["AllowStatus"].ToString();



                            if ((((DropDownList)gv_EmpInfoLoad.Rows[i].FindControl("ddlStatus")).SelectedValue == "Inactive"))
                            {
                                ((TextBox)gv_EmpInfoLoad.Rows[i].Cells[5].FindControl("txt_fromDate")).Text = "";
                                ((TextBox)gv_EmpInfoLoad.Rows[i].Cells[5].FindControl("txt_ToDate")).Text = "";

                                ((TextBox)gv_EmpInfoLoad.Rows[i].Cells[5].FindControl("txt_fromDate")).Enabled = false;

                                ((TextBox)gv_EmpInfoLoad.Rows[i].Cells[5].FindControl("txt_ToDate")).Enabled = false;
                            }
                            else
                            {

                                ((TextBox)gv_EmpInfoLoad.Rows[i].Cells[5].FindControl("txt_fromDate")).Enabled = true;

                                ((TextBox)gv_EmpInfoLoad.Rows[i].Cells[5].FindControl("txt_ToDate")).Enabled = true;
                            }

                        }
                        catch (Exception)
                        {

                            ((DropDownList)gv_EmpInfoLoad.Rows[i].FindControl("ddlStatus")).SelectedIndex
                                = 0;
                        }

                        try
                        {
                            ((TextBox)gv_EmpInfoLoad.Rows[i].FindControl("txt_fromDate")).Text = jobCreationInfos.Rows[i].Field<string>("AllowFromDate").ToString();
                        }
                        catch (Exception)
                        {
                            ((TextBox) gv_EmpInfoLoad.Rows[i].FindControl("txt_fromDate")).Text = "";

                        }


                        try
                        {
                            ((TextBox)gv_EmpInfoLoad.Rows[i].FindControl("txt_ToDate")).Text = jobCreationInfos.Rows[i].Field<string>("AllowToDate").ToString();
                        }
                        catch (Exception)
                        {

                            ((TextBox)gv_EmpInfoLoad.Rows[i].FindControl("txt_ToDate")).Text = "";
                        }

                        ddlStatus_chang(i);
                    }
                }
            }
            catch (Exception)
            {
                
                //throw;
            }

            try
            {

                for (int i = 0; i < gv_EmpInfoLoad.Rows.Count; i++)
                {
                    if (jobCreationInfos.Rows.Count > 0)
                    {
                        try
                        {
                            if (((HiddenField)gv_EmpInfoLoad.Rows[i].FindControl("txtEmployeeIdForCheck")).Value != "" && jobCreationInfos.Rows[i]["AllowStatus"].ToString()!="0")
                            {
                                var chkBoxRows = (CheckBox)gv_EmpInfoLoad.Rows[i].Cells[0].FindControl("txt_check");
                                chkBoxRows.Checked = true;

                                if ((((DropDownList) gv_EmpInfoLoad.Rows[i].FindControl("ddlStatus")).SelectedValue ==
                                     "Inactive"))
                                {
                                    System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#FF8C00");
                                    gv_EmpInfoLoad.Rows[i].Cells[0].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[1].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[2].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[3].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[4].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[5].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[6].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[7].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[8].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[9].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[10].BackColor = col;
                                }
                                else
                                {
                                    System.Drawing.Color col = System.Drawing.ColorTranslator.FromHtml("#98FB98");
                                    gv_EmpInfoLoad.Rows[i].Cells[0].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[1].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[2].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[3].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[4].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[5].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[6].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[7].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[8].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[9].BackColor = col;
                                    gv_EmpInfoLoad.Rows[i].Cells[10].BackColor = col;
                                    
                                }

                            }
                            else
                            {
                                var chkBoxRows = (CheckBox)gv_EmpInfoLoad.Rows[i].Cells[0].FindControl("txt_check");
                                chkBoxRows.Checked = false;
                                Color col = Color.Empty;
                                gv_EmpInfoLoad.Rows[i].Cells[0].BackColor = col;
                                gv_EmpInfoLoad.Rows[i].Cells[1].BackColor = col;
                                gv_EmpInfoLoad.Rows[i].Cells[2].BackColor = col;
                                gv_EmpInfoLoad.Rows[i].Cells[3].BackColor = col;
                                gv_EmpInfoLoad.Rows[i].Cells[4].BackColor = col;
                                gv_EmpInfoLoad.Rows[i].Cells[5].BackColor = col;
                                gv_EmpInfoLoad.Rows[i].Cells[6].BackColor = col;
                                gv_EmpInfoLoad.Rows[i].Cells[7].BackColor = col;
                                gv_EmpInfoLoad.Rows[i].Cells[8].BackColor = col;
                                gv_EmpInfoLoad.Rows[i].Cells[9].BackColor = col;
                                gv_EmpInfoLoad.Rows[i].Cells[10].BackColor = col;
                            }


                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
            catch (Exception)
            {
                
                //throw;
            }
           

        }
        else
        {
            gv_EmpInfoLoad.DataSource = null;
            gv_EmpInfoLoad.DataBind();
            AlertMessageBoxShow("No Data Found!!!");
           
        }
    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }
    [WebMethod(EnableSession = true)]
    public static List<tblEmpGeneralInfo> LoadEmployeeInfoList()
    {
        UserCommonDAL _userCommonDal = new UserCommonDAL();
        return _userCommonDal.LoadEmployeeInfoList();
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteEmp(string EmpInfoId)
    {
        string status = "ok";
        int mid = int.Parse(EmpInfoId);
        try
        {
            //using (var db = new HRIS_SMCEntities())
            //{
            //    var mpb = (from u in db.tblEmpGeneralInfoes where u.EmpInfoId == mid select u).FirstOrDefault();
            //    mpb.IsActive = false;
            //    mpb.UpdateDate = DateTime.Now;
            //    mpb.Updateby = _userId;
            //    db.SaveChanges();
            //}

        CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
            bool sts = _commonDataLoad.DeleteEmployeeById(mid);


        }
        catch (Exception ex)
        {
            status = ex.Message;
        }
        return status;
    }


    private string GenerateParameter()
    {
        string parameter = " ";

        if (ddlCompany.SelectedIndex >0)
        {
            parameter = parameter + "  and    com.CompanyId = '"+ddlCompany.SelectedValue+"'";
        }

        if (ddlDivision.SelectedIndex > 0)
        {
            parameter = parameter + "  and    div.DivisionId = '" + ddlDivision.SelectedValue + "'";
        }

        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  and    Dpt.DepartmentId = '" + ddlDepartment.SelectedValue + "'";
        }

        if (ddlSection.SelectedIndex > 0)
        {
            parameter = parameter + "  and    SectionId = '" + ddlSection.SelectedValue + "'";
        }

        if (ddlSubSection.SelectedIndex > 0)
        {
            parameter = parameter + "  and    SubSectionId = '" + ddlSubSection.SelectedValue + "'";
        }

        if (txtSearch.Text != "")
        {
            parameter = parameter + "  and (e.EmpMasterCode LIKE     '%" + txtSearch.Text.Trim() + "%' ) ";
        }

        if (ddlEmpInfo.SelectedValue!="")
        {
            parameter = parameter + "  and  e.EmpInfoId=" + ddlEmpInfo.SelectedValue + "";
        }



        if (ddlCategory.SelectedValue != "")
        {
            parameter = parameter + " AND e.EmpCategoryId = '" + ddlCategory.SelectedValue + "'";
        }

        if (ddlDesignation.SelectedIndex > 0)
        {
            parameter = parameter + "  and    desig.DesignationId = '" + ddlDesignation.SelectedValue + "'";
        }

        if (ddlSalaryLocation.SelectedIndex > 0)
        {
            parameter = parameter + "  and    sal.SalaryLoationId = '" + ddlSalaryLocation.SelectedValue + "'";
        }

        //if (ddlConformationStatus.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    ConformationStatus = '" + ddlConformationStatus.SelectedValue + "'";
        //}
        parameter = parameter + "     and e.IsActive=1";
        if (ddlStatus.SelectedIndex > 0)
        {
            parameter = parameter + "  and    Ldtls.Status = '" + ddlStatus.SelectedValue + "'";
        }


        return parameter;
    }

    protected void btn_New_OnClick(object sender, EventArgs e)
    {
      
        Response.Redirect("LunchAllowanceView.aspx");
    }

    protected void gv_EmpInfoLoad_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = gv_EmpInfoLoad.DataKeys[0];
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
                string id = datKey["EmpInfoId"].ToString();
                //string did = datKey["MPBudgetDetailsId"].ToString();
                Session["Status"] = "Edit";
               
                Response.Redirect("EmployeeInfoEntry.aspx?mid=" +  e.CommandArgument.ToString());
                //+ "&mdid=" + gv_EmpInfoLoad.DataKeys[rowindex][1].ToString());    

            }


        }

        if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);



            var datKey = gv_EmpInfoLoad.DataKeys[0];

            if (datKey != null)
            {
                //Session["ApprovalPage"] = filepath;
                string id = datKey["EmpInfoId"].ToString();
                //string did = datKey["MPBudgetDetailsId"].ToString();
                Session["Status"] = "View";
                Response.Redirect("EmployeeInfoEntry.aspx?mid=" + e.CommandArgument.ToString());
               

            }

           

        }

        if (e.CommandName == "VisitingLetter")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);



            var datKey = gv_EmpInfoLoad.DataKeys[0];

            if (datKey != null)
            {
                //Session["ApprovalPage"] = filepath;
                string id = datKey["EmpInfoId"].ToString();
                //string did = datKey["MPBudgetDetailsId"].ToString();
                Session["Status"] = "Add";
                Response.Redirect("../AllPrintLetter/plToWhomITMayConcern.aspx?mid=" + e.CommandArgument.ToString());


            }



        }

        if (e.CommandName == "DeleteData")
        {
            //int rowindex = Convert.ToInt32(e.CommandArgument);
            //string masterId = gv_EmpInfoLoad.DataKeys[rowindex][0].ToString();

            //bool masterStatus = aJobCreationDal.DeleteJobCreationById(masterId);
            //bool detailStatus = aJobCreationDal.DeleteJobCreationDetailById(masterId);

            //if (masterStatus && detailStatus)
            //{
            //    aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
            //    LoadJobCreationInfo();
            //}

            int rowindex = Convert.ToInt32(e.CommandArgument);



            var datKey = gv_EmpInfoLoad.DataKeys[0];
            if (datKey != null)
            {
                string id = datKey["EmpInfoId"].ToString();
                //string did = datKey["MPBudgetDetailsId"].ToString();
                Session["Status"] = "Delete";
                Response.Redirect("EmployeeInfoEntry.aspx?mid=" + e.CommandArgument.ToString());
            }

            
        }
        if (e.CommandName == "ViewReport")
        {

            PopUp(Convert.ToInt32(e.CommandArgument.ToString()));
        }



    }

    private void PopUp(Int32 EmpInfoId)
    {
        string url = "../Report_UI/EmployeeInfoListReportViwer.aspx?rptType=" + EmpInfoId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
          LoadEMPInfo();
        }
        else
        {
            ddlCompany.Focus();
            aShowMessage.ShowMessageBox("Please Select This !!!", this);
        }
    }

  

    protected void gv_EmpInfoLoad_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_EmpInfoLoad.PageIndex = e.NewPageIndex;
        this.LoadEMPInfo();
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
             
            using (DataTable dt = _commonDataLoad.GetDDLComDivision(ddlCompany.SelectedValue))
            {
                ddlDivision.DataSource = dt;
                ddlDivision.DataValueField = "Value";
                ddlDivision.DataTextField = "TextField";
                ddlDivision.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLComWind(ddlCompany.SelectedValue))
            {
                ddlWing.DataSource = dt;
                ddlWing.DataValueField = "Value";
                ddlWing.DataTextField = "TextField";
                ddlWing.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComDepartment(ddlCompany.SelectedValue))
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataValueField = "Value";
                ddlDepartment.DataTextField = "TextField";
                ddlDepartment.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComSection(ddlCompany.SelectedValue))
            {
                ddlSection.DataSource = dt;
                ddlSection.DataValueField = "Value";
                ddlSection.DataTextField = "TextField";
                ddlSection.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComSubSection(ddlCompany.SelectedValue))
            {
                ddlSubSection.DataSource = dt;
                ddlSubSection.DataValueField = "Value";
                ddlSubSection.DataTextField = "TextField";
                ddlSubSection.DataBind();
            }
            using (DataTable dt222 = _commonDataLoad.GetEmpDDLIsActive(ddlCompany.SelectedValue.ToString()))
            {

                ddlEmpInfo.DataSource = dt222;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;
            }
            using (DataTable dt = _commonDataLoad.GetDDLSalaryLocation())
            {
                ddlSalaryLocation.DataSource = dt;
                ddlSalaryLocation.DataValueField = "Value";
                ddlSalaryLocation.DataTextField = "TextField";
                ddlSalaryLocation.DataBind();

                if (ddlCompany.SelectedValue == "2")
                {
                    ddlSalaryLocation.SelectedValue = "108";
                }
                else
                {
                    ddlSalaryLocation.SelectedValue = "84";
                }
              
            }
        }
        else
        {
            ddlDivision.Items.Clear();
            ddlWing.Items.Clear();
            ddlDepartment.Items.Clear();
            ddlSection.Items.Clear();
            ddlSubSection.Items.Clear();
            ddlSalaryLocation.Items.Clear();
        }
    }
    readonly IncrementDal aIncrementDal = new IncrementDal();

    private void LoadInitialDDL()
    {

        aIncrementDal.LoadCategory(ddlCategory);

        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
            ddlCompany.SelectedIndex = 1;

            ddlCompany_SelectedIndexChanged(null, null);
        }

        using (DataTable dt = _commonDataLoad.GetDDLDesignationAll())
        {
            ddlDesignation.DataSource = dt;
            ddlDesignation.DataValueField = "Value";
            ddlDesignation.DataTextField = "TextField";
            
            ddlDesignation.DataBind();
            
            ddlDesignation.SelectedValue = "Please Select One..";
        }
    }

    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue != "")
        {
            _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
            _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
            _commonDataLoad.GetSectionByDivList(ddlSection, ddlDivision.SelectedValue);
            _commonDataLoad.GetSubSectionListAll(ddlSubSection, ddlDivision.SelectedValue);
        }
        else
        {
            ddlWing.Items.Clear();
            ddlDepartment.Items.Clear();
            ddlSection.Items.Clear();
            ddlSubSection.Items.Clear();
        }
    }

    protected void ddlWing_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWing.SelectedValue != "")
        {
            _commonDataLoad.GetDepartmentList(ddlDepartment, ddlDivision.SelectedValue);
        }
        else
        {
            ddlDepartment.Items.Clear();
        }
    }

    protected void ddlDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedValue != "")
            {
                _commonDataLoad.GetSectionList(ddlSection, ddlDepartment.SelectedValue);
                DataTable dtgetdata = _commonDataLoad.GetDepartmentRelaton(ddlDepartment.SelectedValue, "");
                if (dtgetdata.Rows.Count > 0)
                {
                    if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
                    {
                        wing.Visible = false;
                        ddlWing.Items.Clear();
                        _commonDataLoad.GetDivisionWingListAll(ddlWing, ddlDivision.SelectedValue);
                        ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                    }
                    else
                    {
                        wing.Visible = true;
                        ddlWing.Items.Clear();
                        _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
                        ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                    }
                }
            }
            else
            {
                ddlSection.Items.Clear();
            }
        }
        catch (Exception)
        {
            
            //throw;
        }
    }

    protected void ddlSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtgetdata1 = _commonDataLoad.GetSectionRelaton(ddlSection.SelectedValue, "");
        if (dtgetdata1.Rows.Count > 0)
        {
            if (dtgetdata1.Rows[0]["Invisible"].ToString() == "True")
            {
                dept.Visible = false;
                ddlDepartment.Items.Clear();
                _commonDataLoad.GetDepartmentByDivListAll(ddlDepartment, ddlDivision.SelectedValue);
                ddlDepartment.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
            else
            {
                dept.Visible = true;
                ddlDepartment.Items.Clear();
                _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
                ddlDepartment.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
        }
        DataTable dtgetdata = _commonDataLoad.GetDepartmentRelaton(ddlDepartment.SelectedValue, "");
        if (dtgetdata.Rows.Count > 0)
        {
            if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
            {
                wing.Visible = false;
                ddlWing.Items.Clear();
                _commonDataLoad.GetDivisionWingListAll(ddlWing, ddlDivision.SelectedValue);
                ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
            }
            else
            {
                wing.Visible = true;
                ddlWing.Items.Clear();
                _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
                ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
            }
        }
    }

    protected void ddlSubSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtgetdata2 = _commonDataLoad.GetSubSectionRelaton(ddlSubSection.SelectedValue, "");
        if (dtgetdata2.Rows.Count > 0)
        {
            if (dtgetdata2.Rows[0]["Invisible"].ToString() == "True")
            {
                sec.Visible = false;
                ddlSection.Items.Clear();
                _commonDataLoad.GetSectionByDivListAll(ddlSection, ddlDivision.SelectedValue);
                ddlSection.SelectedValue = dtgetdata2.Rows[0]["SectionId"].ToString();
            }
            else
            {
                sec.Visible = true;
                ddlSection.Items.Clear();
                _commonDataLoad.GetSectionByDivList(ddlSection, ddlDivision.SelectedValue);
                ddlSection.SelectedValue = dtgetdata2.Rows[0]["SectionId"].ToString();
            }
        }
        DataTable dtgetdata1 = _commonDataLoad.GetSectionRelaton(ddlSection.SelectedValue, "");
        if (dtgetdata1.Rows.Count > 0)
        {
            if (dtgetdata1.Rows[0]["Invisible"].ToString() == "True")
            {
                dept.Visible = false;
                ddlDepartment.Items.Clear();
                _commonDataLoad.GetDepartmentByDivListAll(ddlDepartment, ddlDivision.SelectedValue);
                ddlDepartment.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
            else
            {
                dept.Visible = true;
                ddlDepartment.Items.Clear();
                _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
                ddlDepartment.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
        }
        DataTable dtgetdata = _commonDataLoad.GetDepartmentRelaton(ddlDepartment.SelectedValue, "");
        if (dtgetdata.Rows.Count > 0)
        {
            if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
            {
                wing.Visible = false;
                ddlWing.Items.Clear();
                _commonDataLoad.GetDivisionWingListAll(ddlWing, ddlDivision.SelectedValue);
                ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
            }
            else
            {
                wing.Visible = true;
                ddlWing.Items.Clear();
                _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
                ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
            }
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (gv_EmpInfoLoad.Rows.Count > 0)
        {
            string attachment = "attachment; filename=EmployeeInformationList.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

           


             
            gv_EmpInfoLoad.Columns[1].Visible =
                        false;
            gv_EmpInfoLoad.Columns[10].Visible =
               false;
            gv_EmpInfoLoad.Columns[11].Visible =
               false;
        

           gv_EmpInfoLoad.AllowPaging = false;
           this.LoadEMPInfo();
            // Create a form to contain the grid  
            HtmlForm frm = new HtmlForm();
            gv_EmpInfoLoad.Parent.Controls.Add(frm);
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(gv_EmpInfoLoad);
            //frm.RenderControl(htw);

            gv_EmpInfoLoad.HeaderRow.Style.Add("background-color", "#E5EEF1");

            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in gv_EmpInfoLoad.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in gv_EmpInfoLoad.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                }
            }


            gv_EmpInfoLoad.RenderControl(htw);
            string headerTable = @"<span  style='text-align:left'><h3> " + ddlCompany.SelectedItem.Text + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
   <h3>Employee Information List	</h3>

</span>";

            HttpContext.Current.Response.Write(headerTable);
            HttpContext.Current.Response.Write(SubTi);
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        // //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoList.aspx");
    }

    protected void btnEditInfo_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoListUpdate.aspx");
    }

    protected void btn_edit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField mastrId = (HiddenField)gv_EmpInfoLoad.Rows[rowID].FindControl("JdMasterId");
        PopUpjd(Convert.ToInt32(mastrId.Value));
    }


    private void PopUpjd(Int32 mastrId)
    { 
        string url = "../Report_UI/JobDescriptionReportViwer2.aspx?rptType=" + mastrId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void txt_checkAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gv_EmpInfoLoad.HeaderRow.FindControl("txt_checkAll");
        bool result = ChkBoxHeader.Checked == true ? true : false;

        for (int i = 0; i < gv_EmpInfoLoad.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)gv_EmpInfoLoad.Rows[i].FindControl("txt_check");
            chk.Checked = result;
        }
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {


        if (Validations())
        {
            mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
            tblLunchAllowMaster Bat = null;


            try
            {
                using (var db = new HRIS_SMC_DBEntities())
                {
                    if (mid > 0)
                    {
                        Bat = (from j in db.tblLunchAllowMasters where j.LunchAllowID == mid select j).FirstOrDefault();




                        Bat.UpdateBy = Convert.ToInt32(Session["UserId"]);
                        Bat.UpdateDate = DateTime.Now;
                        db.SaveChanges();
                    }
                    else
                    {
                        Bat = new tblLunchAllowMaster();


                        Bat.EntryBy = Convert.ToInt32(Session["UserId"]);
                        Bat.EntryDate = DateTime.Now;
                        db.tblLunchAllowMasters.Add(Bat);
                        db.SaveChanges();




                        if (gv_EmpInfoLoad.Rows.Count > 0)
                        {
                            //making previous entris inactive



                          


                            //db.Database.ExecuteSqlCommand("delete FROM  dbo.tblLunchAllowDetails   WHERE LunchAllowID={0}",
                            //    Bat.LunchAllowID);
                            for (int i = 0; i < gv_EmpInfoLoad.Rows.Count; i++)
                            {

                                var chkBoxRows = (CheckBox)gv_EmpInfoLoad.Rows[i].Cells[0].FindControl("txt_check");

                                TextBox txt_DeadLine = (TextBox)gv_EmpInfoLoad.Rows[i].FindControl("txt_fromDate");



                                //if ((txt_DeadLine.Text == "") && (chkBoxRows.Checked == true))
                                //{
                                //    AlertMessageBoxShow("Enter Dead Line Date...");
                                //    txt_DeadLine.Focus();
                                //    return;


                                //}

                                if (chkBoxRows.Checked)
                                {

                                    HiddenField txt_empInfoIdNew =
                                         (HiddenField)gv_EmpInfoLoad.Rows[i].FindControl("txt_empInfoId");

                              int NewEmpIdUp    = Convert.ToInt32(txt_empInfoIdNew.Value);


                              tblLunchAllowDetail UpEmp = (from j in db.tblLunchAllowDetails where j.EmployeeId == NewEmpIdUp select j).FirstOrDefault();

                                    if (UpEmp != null)
                                    {

                                        HiddenField txt_RateID = (HiddenField)gv_EmpInfoLoad.Rows[i].FindControl("txt_RateID");
                                        HiddenField txt_empInfoId =
                                            (HiddenField)gv_EmpInfoLoad.Rows[i].FindControl("txt_empInfoId");
                                        Label txt_Rate = (Label)gv_EmpInfoLoad.Rows[i].FindControl("txt_Rate");
                                        DropDownList ddlStatus = (DropDownList)gv_EmpInfoLoad.Rows[i].FindControl("ddlStatus");

                                        TextBox txt_fromDate = (TextBox)gv_EmpInfoLoad.Rows[i].FindControl("txt_fromDate");
                                        TextBox txt_ToDate = (TextBox)gv_EmpInfoLoad.Rows[i].FindControl("txt_ToDate");

                                        TextBox txt_InactiveDate = (TextBox)gv_EmpInfoLoad.Rows[i].FindControl("txt_InactiveDate");





                                        UpEmp.LunchAllowID = Convert.ToInt32(Bat.LunchAllowID);
                                        UpEmp.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                                        UpEmp.RateId = Convert.ToInt32(txt_RateID.Value);
                                        UpEmp.EmployeeId = Convert.ToInt32(txt_empInfoId.Value);
                                        UpEmp.Rate = Convert.ToDecimal(txt_Rate.Text);
                                        UpEmp.Status = (ddlStatus.SelectedValue).ToString();


                                        UpEmp.fromDate = string.IsNullOrEmpty(txt_fromDate.Text)
                                            ? (DateTime?)null
                                            : DateTime.Parse(txt_fromDate.Text).Date;


                                        UpEmp.ToDate = string.IsNullOrEmpty(txt_ToDate.Text)
                                            ? (DateTime?)null
                                            : DateTime.Parse(txt_ToDate.Text).Date;


                                        UpEmp.InactiveDate = string.IsNullOrEmpty(txt_InactiveDate.Text)
                                      ? (DateTime?)null
                                      : DateTime.Parse(txt_InactiveDate.Text).Date;


                                        tblLunchAllowDetailsLog delLog = new tblLunchAllowDetailsLog();

                                        delLog.LunchAllowID = Convert.ToInt32(Bat.LunchAllowID);

                                        delLog.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                                        delLog.RateId = Convert.ToInt32(txt_RateID.Value);
                                        delLog.EmployeeId = Convert.ToInt32(txt_empInfoId.Value);
                                        delLog.Rate = Convert.ToDecimal(txt_Rate.Text);
                                        delLog.Status = (ddlStatus.SelectedValue).ToString();


                                        delLog.fromDate = string.IsNullOrEmpty(txt_fromDate.Text)
                                            ? (DateTime?)null
                                            : DateTime.Parse(txt_fromDate.Text).Date;


                                        delLog.ToDate = string.IsNullOrEmpty(txt_ToDate.Text)
                                            ? (DateTime?)null
                                            : DateTime.Parse(txt_ToDate.Text).Date;

                                        delLog.InactiveDate = string.IsNullOrEmpty(txt_InactiveDate.Text)
                                         ? (DateTime?)null
                                         : DateTime.Parse(txt_InactiveDate.Text).Date;
                                        delLog.UpdateBy = Convert.ToInt32(Session["UserId"]);
                                        delLog.UpdateDate = DateTime.Now;

                                        db.tblLunchAllowDetailsLogs.Add(delLog);

                                        db.SaveChanges();

                                      
                                      
                                    }
                                    else
                                    {

                                        HiddenField txt_RateID = (HiddenField)gv_EmpInfoLoad.Rows[i].FindControl("txt_RateID");
                                        HiddenField txt_empInfoId =
                                            (HiddenField)gv_EmpInfoLoad.Rows[i].FindControl("txt_empInfoId");
                                        Label txt_Rate = (Label)gv_EmpInfoLoad.Rows[i].FindControl("txt_Rate");
                                        DropDownList ddlStatus = (DropDownList)gv_EmpInfoLoad.Rows[i].FindControl("ddlStatus");

                                        TextBox txt_fromDate = (TextBox)gv_EmpInfoLoad.Rows[i].FindControl("txt_fromDate");
                                        TextBox txt_ToDate = (TextBox)gv_EmpInfoLoad.Rows[i].FindControl("txt_ToDate");
                                        TextBox txt_InactiveDate = (TextBox)gv_EmpInfoLoad.Rows[i].FindControl("txt_InactiveDate");




                                        tblLunchAllowDetail children = new tblLunchAllowDetail();
                                        tblLunchAllowDetailsLog delLog = new tblLunchAllowDetailsLog();

                                        children.LunchAllowID = Convert.ToInt32(Bat.LunchAllowID);
                                       
                                        children.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                                        children.RateId = Convert.ToInt32(txt_RateID.Value);
                                        children.EmployeeId = Convert.ToInt32(txt_empInfoId.Value);
                                        children.Rate = Convert.ToDecimal(txt_Rate.Text);
                                        children.Status = (ddlStatus.SelectedValue).ToString();


                                        children.fromDate = string.IsNullOrEmpty(txt_fromDate.Text)
                                            ? (DateTime?)null
                                            : DateTime.Parse(txt_fromDate.Text).Date;


                                        children.ToDate = string.IsNullOrEmpty(txt_ToDate.Text)
                                            ? (DateTime?)null
                                            : DateTime.Parse(txt_ToDate.Text).Date;

                                        children.InactiveDate = string.IsNullOrEmpty(txt_InactiveDate.Text)
                                         ? (DateTime?)null
                                         : DateTime.Parse(txt_InactiveDate.Text).Date;


                                        delLog.LunchAllowID = Convert.ToInt32(Bat.LunchAllowID);

                                        delLog.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                                        delLog.RateId = Convert.ToInt32(txt_RateID.Value);
                                        delLog.EmployeeId = Convert.ToInt32(txt_empInfoId.Value);
                                        delLog.Rate = Convert.ToDecimal(txt_Rate.Text);
                                        delLog.Status = (ddlStatus.SelectedValue).ToString();


                                        delLog.fromDate = string.IsNullOrEmpty(txt_fromDate.Text)
                                            ? (DateTime?)null
                                            : DateTime.Parse(txt_fromDate.Text).Date;


                                        delLog.ToDate = string.IsNullOrEmpty(txt_ToDate.Text)
                                            ? (DateTime?)null
                                            : DateTime.Parse(txt_ToDate.Text).Date;

                                        delLog.InactiveDate = string.IsNullOrEmpty(txt_InactiveDate.Text)
                                         ? (DateTime?)null
                                         : DateTime.Parse(txt_InactiveDate.Text).Date;
                                        delLog.EntryBy = Convert.ToInt32(Session["UserId"]);
                                        delLog.EntryDate = DateTime.Now;
                                        db.tblLunchAllowDetailsLogs.Add(delLog);

                                        db.tblLunchAllowDetails.Add(children);
                                        db.SaveChanges();
    
                                    }






                                    


                                }
                            }
                        } ////End Childrens



                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successful...! ');window.location ='LunchAllowanceEntry.aspx';",
                        true);
                }
            }
            catch (Exception)
            {
            } 
        } 

      
        }

    private bool Validations()
    {
        if (gv_EmpInfoLoad.Rows.Count == 0)
        {
            ShowMessageBox("Please Select At Least One employee !!!");
            return false;
        }


        Int32 Questioncount = 0;

        for (int i = 0; i < gv_EmpInfoLoad.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_EmpInfoLoad.Rows[i].Cells[0].FindControl("txt_check");

            if (chkBoxRows.Checked)
            {
                Questioncount++;
            }

            if (Questioncount > 0)
            {
                break;
            }
        }

        if (Questioncount == 0)
        {
            ShowMessageBox("Please select at least one Row !!!");
            return false;
        }


        for (int i = 0; i < gv_EmpInfoLoad.Rows.Count; i++)
        {

            var chkBoxRows = (CheckBox) gv_EmpInfoLoad.Rows[i].Cells[0].FindControl("txt_check");

            TextBox txt_DeadLine = (TextBox) gv_EmpInfoLoad.Rows[i].FindControl("txt_fromDate");
            TextBox txt_ToDate = (TextBox)gv_EmpInfoLoad.Rows[i].FindControl("txt_ToDate");
            DropDownList ddlStatus = (DropDownList)gv_EmpInfoLoad.Rows[i].FindControl("ddlStatus");


            if ((ddlStatus.SelectedValue == "Active") && (chkBoxRows.Checked == true))
            {

                if (txt_DeadLine.Text=="")
                {
                    AlertMessageBoxShow("Enter From Date...");
                    txt_DeadLine.Focus();
                    return false;
                }

                if (txt_ToDate.Text == "")
                {
                    AlertMessageBoxShow("Enter To Date...");
                    txt_ToDate.Focus();
                    return false;
                }

                DateTime fromDate;
                DateTime toDate;
                if (!DateTime.TryParse(txt_DeadLine.Text, out fromDate))
                {
                    AlertMessageBoxShow("Enter valid From Date...");
                    txt_DeadLine.Focus();
                    return false;
                }

                if (!DateTime.TryParse(txt_ToDate.Text, out toDate))
                {
                    AlertMessageBoxShow("Enter valid To Date...");
                    txt_ToDate.Focus();
                    return false;
                }

                if (toDate.Date < fromDate.Date)
                {
                    AlertMessageBoxShow("To Date can not be earlier than From Date...");
                    txt_ToDate.Focus();
                    return false;
                }


            }


            if ((ddlStatus.SelectedValue == "0") && (chkBoxRows.Checked == true))
            {
                AlertMessageBoxShow("Please Select Status...");
                ddlStatus.Focus();
                return false;


            }

            TextBox FromDate = ((TextBox)gv_EmpInfoLoad.Rows[i].Cells[5].FindControl("txt_InactiveDate"));

            if (FromDate.Text != "")
            {
                if ((ddlStatus.SelectedValue != "Inactive"))
                {
                    AlertMessageBoxShow("Please Select Status Inactive...");
                    ddlStatus.Focus();
                    return false;
                }


            }
            

            if ((ddlStatus.SelectedValue == "Inactive") && (chkBoxRows.Checked == true))
            {
               
                if (FromDate.Text=="")
                {
                    AlertMessageBoxShow("Please Select Inactive Date...");

                      FromDate.Focus();
                return false;
                }


                if (FromDate.Text != "")
                {
                    DataTable dttime = allowanceCancelDal.GetLuchSetupTime();
                    DateTime timenow = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString());
                    DateTime ExcaTime = Convert.ToDateTime(DateTime.Now.ToString(dttime.Rows[0]["LunchTime"].ToString()));

                    DateTime exxx = ExcaTime.AddDays(1);
                    string tttt = timenow.ToString("dd-MMM-yyyy");

                    if (FromDate.Text.Trim() == tttt)
                    {
                        DateTime SubmitTime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString());

                        DataTable dtdata = allowanceCancelDal.GetLunchAllowEmpCheckSecond(SubmitTime, exxx);

                        int countt = 0;
                        if (dtdata.Rows.Count > 0)
                        {
                            countt = Convert.ToInt32(dtdata.Rows[0]["CoutSecend"].ToString());
                        }
                        if (countt > 0)
                        {
                        }
                        else
                        {
                            AlertMessageBoxShow("Please Submit before " + ExcaTime);

                            FromDate.Focus();
                            return false;

                        }
                    }


                }
               


            }

            


        }


        return true;
    }
    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    protected void chk_Common_OnCheckedChanged(object sender, EventArgs e)
    {

        txtFromDate.Text = "";
        txtToDate.Text = "";
        txtFromDate.Enabled = false;
        txtToDate.Enabled = false;
        ISCommonDate();
    }

    private void ISCommonDate()
    {
        try
        {



            if (chk_Common.Checked)
            {
               

                txtFromDate.Enabled = true;
                txtToDate.Enabled = true;
                if (txtFromDate.Text.Trim() != "" )
                {


                    string sFromDate = Convert.ToDateTime(txtFromDate.Text.Trim()).ToString("dd-MMM-yyyy");
                   

                    if (sFromDate != "")
                    {
                        for (int j = 0; j < gv_EmpInfoLoad.Rows.Count; j++)
                        {
                            //DataTable dt = (DataTable)ViewState["EmpSetup"];
                            TextBox FromDate = ((TextBox)gv_EmpInfoLoad.Rows[j].Cells[5].FindControl("txt_fromDate"));
                            



                            FromDate.Text = sFromDate;
                        }
                    }


 
                }


                if (txtToDate.Text.Trim() != "")
                {


                   
                    string sTODate = Convert.ToDateTime(txtToDate.Text.Trim()).ToString("dd-MMM-yyyy");


                    

                    if (sTODate != "")
                    {
                        for (int j = 0; j < gv_EmpInfoLoad.Rows.Count; j++)
                        {
                            //DataTable dt = (DataTable)ViewState["EmpSetup"];

                            TextBox ToDate = ((TextBox)gv_EmpInfoLoad.Rows[j].Cells[5].FindControl("txt_ToDate"));



                            ToDate.Text = sTODate;
                        }
                    }
                }



            }
            else
            {
                for (int j = 0; j < gv_EmpInfoLoad.Rows.Count; j++)
                {
                    //DataTable dt = (DataTable)ViewState["EmpSetup"];

                    TextBox FromDate = ((TextBox)gv_EmpInfoLoad.Rows[j].Cells[5].FindControl("txt_fromDate"));
                    TextBox ToDate = ((TextBox)gv_EmpInfoLoad.Rows[j].Cells[5].FindControl("txt_ToDate"));


                    ToDate.Text = "";
                    FromDate.Text = "";
                    txtFromDate.Text = "";
                    txtToDate.Text = "";
                }
            }
            

        }
        catch (Exception)
        {


           
        }
    }

    protected void txtFromDate_OnTextChanged(object sender, EventArgs e)
    {
        ISCommonDate();
    }





    protected void txtToDate_OnTextChanged(object sender, EventArgs e)
    {
        ISCommonDate();
    }
    private void ddlStatus_chang(int rowIndex)
    {
        DropDownList ddlStatus = (DropDownList)gv_EmpInfoLoad.Rows[rowIndex].FindControl("ddlStatus");
        ((TextBox)gv_EmpInfoLoad.Rows[rowIndex].Cells[5].FindControl("txt_fromDate")).Enabled = false;

        ((TextBox)gv_EmpInfoLoad.Rows[rowIndex].Cells[5].FindControl("txt_ToDate")).Enabled = false;
        ((TextBox)gv_EmpInfoLoad.Rows[rowIndex].Cells[5].FindControl("txt_InactiveDate")).Enabled = false;


        if ((ddlStatus.SelectedValue == "Inactive"))
        {
            ((TextBox)gv_EmpInfoLoad.Rows[rowIndex].Cells[5].FindControl("txt_fromDate")).Text = "";
            ((TextBox)gv_EmpInfoLoad.Rows[rowIndex].Cells[5].FindControl("txt_ToDate")).Text = "";

            ((TextBox)gv_EmpInfoLoad.Rows[rowIndex].Cells[5].FindControl("txt_InactiveDate")).Enabled = true;
        }
        else if ((ddlStatus.SelectedValue == "Active"))
        {
            ((TextBox)gv_EmpInfoLoad.Rows[rowIndex].Cells[5].FindControl("txt_InactiveDate")).Text = "";

            ((TextBox)gv_EmpInfoLoad.Rows[rowIndex].Cells[5].FindControl("txt_fromDate")).Enabled = true;

            ((TextBox)gv_EmpInfoLoad.Rows[rowIndex].Cells[5].FindControl("txt_ToDate")).Enabled = true;
        }
    }

    protected void ddlStatus_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;
        DropDownList ddlStatus = (DropDownList)gv_EmpInfoLoad.Rows[rowIndex].FindControl("ddlStatus");

        ((TextBox)gv_EmpInfoLoad.Rows[rowIndex].Cells[5].FindControl("txt_fromDate")).Enabled = false;

        ((TextBox)gv_EmpInfoLoad.Rows[rowIndex].Cells[5].FindControl("txt_ToDate")).Enabled = false;
        ((TextBox)gv_EmpInfoLoad.Rows[rowIndex].Cells[5].FindControl("txt_InactiveDate")).Enabled = false;


        if ((ddlStatus.SelectedValue == "Inactive"))
        {
            ((TextBox)gv_EmpInfoLoad.Rows[rowIndex].Cells[5].FindControl("txt_fromDate")).Text = "";
            ((TextBox)gv_EmpInfoLoad.Rows[rowIndex].Cells[5].FindControl("txt_ToDate")).Text = "";

            ((TextBox)gv_EmpInfoLoad.Rows[rowIndex].Cells[5].FindControl("txt_InactiveDate")).Enabled = true;

        }
        else if ((ddlStatus.SelectedValue == "Active"))
        {
            ((TextBox)gv_EmpInfoLoad.Rows[rowIndex].Cells[5].FindControl("txt_InactiveDate")).Text = "";

            ((TextBox)gv_EmpInfoLoad.Rows[rowIndex].Cells[5].FindControl("txt_fromDate")).Enabled = true;

            ((TextBox)gv_EmpInfoLoad.Rows[rowIndex].Cells[5].FindControl("txt_ToDate")).Enabled = true;
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
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

   
}
 
