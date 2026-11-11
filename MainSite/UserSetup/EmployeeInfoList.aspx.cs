using DAL.COMMON_DAL;
using DAL.MenuSetup_DAL;
using DAL.Permission_DAL;
using DAL.Transfer_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ListItem = System.Web.UI.WebControls.ListItem;

public partial class UserSetup_EmployeeInfoList : System.Web.UI.Page
{
    private static string _userId;
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    PermissionDAL aPermissionDal = new PermissionDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {
            LoadInitialDDL();
            GetCompany();
            UserPersmissionValidation();
         //   LoadEMPInfo();
        }
    }




    protected void EmployeeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {

        string empName = NameTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            //EmployeeDropDownList.Text = emp[0];
            NameTextBox.Text = emp[2];
           
        }
        //else
        //{
        //    NameTextBox.Text = "";
        //    NameTextBox.Text = "";
        //  //  EmpInfoIdHiddenField.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}

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

                    btn_New.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
                        Convert.ToBoolean(ViewState["Edit"].ToString());

                    btnEditInfo.Visible = Convert.ToBoolean(ViewState["Edit"].ToString());
                    btn_New.Visible = Convert.ToBoolean(ViewState["Add"].ToString());
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
        jobCreationInfos = _userCommonDal.GetEMpInfosOnlyView("  com.CompanyId IN (" + CompanyId() + ")" + GenerateParameter() + "", GenerateParameterOnlyView());
        if (jobCreationInfos.Rows.Count>0)
        {
            
            loadGridView.DataSource = jobCreationInfos;
            loadGridView.DataBind();
            lblCount.Text ="Total : "+ jobCreationInfos.Rows.Count.ToString() ;

        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            AlertMessageBoxShow("No Data Found!!!");
            lblCount.Text =  "Total : 0";
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

        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and  ( e.EmpInfoId=" + ddlEmpInfo.SelectedValue.Trim() + ")";
        }

        if (ddlDesignation.SelectedIndex > 0)
        {
            parameter = parameter + "  and    desig.DesignationId = '" + ddlDesignation.SelectedValue + "'";
        }

        if (ddlSalaryLocation.SelectedIndex > 0)
        {
            parameter = parameter + "  and    sal.SalaryLoationId = '" + ddlSalaryLocation.SelectedValue + "'";
        }

        if (ddlConformationStatus.SelectedIndex > 0)
        {
            parameter = parameter + "  and    ConformationStatus = '" + ddlConformationStatus.SelectedValue + "'";
        }

        if (ActiveStatusDropDownList.SelectedIndex > 0)
        {
            parameter = parameter + "  and    e.IsActive = '" + ActiveStatusDropDownList.SelectedValue + "'";
        }

        if (SSGradeCheck.Checked)
        {
            parameter = parameter + "  and    e.IsSpTransfer = 1";
        }
       
        return parameter;
    }



    private string GenerateParameterOnlyView()
    {
        string parameter = " ";
 
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

        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and  ( e.EmpInfoId=" + ddlEmpInfo.SelectedValue.Trim() + ")";
        }

        if (ddlDesignation.SelectedIndex > 0)
        {
            parameter = parameter + "  and    desig.DesignationId = '" + ddlDesignation.SelectedValue + "'";
        }

        if (ddlSalaryLocation.SelectedIndex > 0)
        {
            parameter = parameter + "  and    sal.SalaryLoationId = '" + ddlSalaryLocation.SelectedValue + "'";
        }

        if (ddlConformationStatus.SelectedIndex > 0)
        {
            parameter = parameter + "  and    ConformationStatus = '" + ddlConformationStatus.SelectedValue + "'";
        }

        if (ActiveStatusDropDownList.SelectedIndex > 0)
        {
            parameter = parameter + "  and    e.IsActive = '" + ActiveStatusDropDownList.SelectedValue + "'";
        }

        if (SSGradeCheck.Checked)
        {
            parameter = parameter + "  and    e.IsSpTransfer = 1";
        }
       

        return parameter;
    }
    protected void btn_New_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("EmpGeneral.aspx");
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[0];
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
                //+ "&mdid=" + loadGridView.DataKeys[rowindex][1].ToString());    

            }


        }

        if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);



            var datKey = loadGridView.DataKeys[0];

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



            var datKey = loadGridView.DataKeys[0];

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
            //string masterId = loadGridView.DataKeys[rowindex][0].ToString();

            //bool masterStatus = aJobCreationDal.DeleteJobCreationById(masterId);
            //bool detailStatus = aJobCreationDal.DeleteJobCreationDetailById(masterId);

            //if (masterStatus && detailStatus)
            //{
            //    aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
            //    LoadJobCreationInfo();
            //}

            int rowindex = Convert.ToInt32(e.CommandArgument);



            var datKey = loadGridView.DataKeys[0];
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

        if (e.CommandName == "UpdateMail")
        {
            int empInfoId;
            if (int.TryParse(e.CommandArgument.ToString(), out empInfoId))
            {
                BindEmployeeMailForEdit(empInfoId);
                ShowUpdateMailModal();
            }
            else
            {
                aShowMessage.ShowMessageBox("Invalid employee selection.", this);
            }
        }



    }

    private void BindEmployeeMailForEdit(int empInfoId)
    {
        using (var db = new HRIS_SMCEntities())
        {
            var emp = (from e in db.tblEmpGeneralInfoes
                       where e.EmpInfoId == empInfoId
                       select new
                       {
                           e.EmpInfoId,
                           e.EmpMasterCode,
                           e.EmpName,
                           e.PersonalEmail,
                           e.OfficialEmail
                       }).FirstOrDefault();

            if (emp == null)
            {
                hfUpdateMailEmpInfoId.Value = string.Empty;
                txtUpdateMailEmployee.Text = string.Empty;
                txtUpdatePersonalEmail.Text = string.Empty;
                txtUpdateOfficialEmail.Text = string.Empty;
                return;
            }

            hfUpdateMailEmpInfoId.Value = emp.EmpInfoId.ToString();
            txtUpdateMailEmployee.Text = emp.EmpMasterCode + " : " + emp.EmpName;
            txtUpdatePersonalEmail.Text = string.IsNullOrWhiteSpace(emp.PersonalEmail) ? string.Empty : emp.PersonalEmail;
            txtUpdateOfficialEmail.Text = string.IsNullOrWhiteSpace(emp.OfficialEmail) ? string.Empty : emp.OfficialEmail;
        }
    }

    private void ShowUpdateMailModal()
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "ShowUpdateMailModal", "$('#updateMailModal').modal('show');", true);
    }

    private void HideUpdateMailModal()
    {
        ScriptManager.RegisterStartupScript(
            this,
            GetType(),
            "HideUpdateMailModal",
            "$('#updateMailModal').modal('hide');$('body').removeClass('modal-open');$('.modal-backdrop').remove();",
            true);
    }

    protected void btnUpdateMailSave_OnClick(object sender, EventArgs e)
    {
        int empInfoId;
        if (!int.TryParse(hfUpdateMailEmpInfoId.Value, out empInfoId))
        {
            aShowMessage.ShowMessageBox("Employee not found.", this);
            ShowUpdateMailModal();
            return;
        }

        string personalEmail = txtUpdatePersonalEmail.Text.Trim();
        string officialEmail = txtUpdateOfficialEmail.Text.Trim();
        tblEmployeePromotionEntryDAL atblEmployeePromotionEntryDAL = new tblEmployeePromotionEntryDAL();
        try
        {
            bool status = atblEmployeePromotionEntryDAL.UpdateEmail(empInfoId, personalEmail, officialEmail);
            if (status)
            {
                HideUpdateMailModal();
                LoadEMPInfo();
                aShowMessage.ShowMessageBox("Mail updated successfully.", this);
            }
            else
            {
                aShowMessage.ShowMessageBox("Faild to updated.", this);
            }
           
        }
        catch (Exception ex)
        {
            aShowMessage.ShowMessageBox(ex.Message, this);
            ShowUpdateMailModal();
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

   

    protected void loadGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        loadGridView.PageIndex = e.NewPageIndex;
        this.LoadEMPInfo();
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {


           
            Session["CompanyId"] = "";
            Session["CompanyId"] = ddlCompany.SelectedValue;

            ActiveStatusDropDownList_OnSelectedIndexChanged(null, null);
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

            using (DataTable dt = _commonDataLoad.GetDDLSalaryLocation())
            {
                ddlSalaryLocation.DataSource = dt;
                ddlSalaryLocation.DataValueField = "Value";
                ddlSalaryLocation.DataTextField = "TextField";
                ddlSalaryLocation.DataBind();
            }
        }
        else
        {
            ddlEmpInfo.Items.Clear();  
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
            ActiveStatusDropDownList_OnSelectedIndexChanged(null, null);
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
            ddlDivision.Items.Clear();
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
        if (loadGridView.Rows.Count > 0)
        {
            string attachment = "attachment; filename=EmployeeInformationList.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

           


             
            loadGridView.Columns[1].Visible =
                        false;


            loadGridView.Columns[2].Visible =
                        false;


            loadGridView.Columns[3].Visible =
                        false;


            if (loadGridView.Columns.Count > 12)
            {
                loadGridView.Columns[12].Visible = false;
            }
            if (loadGridView.Columns.Count > 13)
            {
                loadGridView.Columns[13].Visible = false;
            }
            if (loadGridView.Columns.Count > 14)
            {
                loadGridView.Columns[14].Visible = false;
            }
        

           loadGridView.AllowPaging = false;
           this.LoadEMPInfo();
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
        HiddenField mastrId = (HiddenField)loadGridView.Rows[rowID].FindControl("JdMasterId");
        PopUpjd(Convert.ToInt32(mastrId.Value));
    }


    private void PopUpjd(Int32 mastrId)
    { 
        string url = "../Report_UI/JobDescriptionReportViwer2.aspx?rptType=" + mastrId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
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
    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}
