using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL.API;
using DAL.COMMON_DAL;
using DAL.Increment_DAL;
using DAL.MenuSetup_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Drawing;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading.Tasks;
using ListItem = System.Web.UI.WebControls.ListItem;

public partial class Report_Pages_LeaveReport : System.Web.UI.Page
{
    private static string _userId;
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    PermissionDAL aPermissionDal = new PermissionDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private HttpClient _httpClient;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {
            LoadInitialDDL();
            AddYears();
            //  GetCompany();
            //    UserPersmissionValidation();
            //   LoadEMPInfo();
        }
    }

    private void AddYears()
    {

        int currentYear = System.DateTime.Now.Year;

        for (int i = 2010; i <= currentYear; i++)
        {
            DropDownList1.Items.Add(new ListItem(Convert.ToString(i)));
        }
    }

    protected void ageDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        agesingle.Visible = false;
        agerange.Visible = false;

        if (ageDropDownList.SelectedValue == "5")
        {
            agerange.Visible = true;
        }
        else
        {

            if (ageDropDownList.SelectedValue != "1")
            {
                agesingle.Visible = true;
            }

        }
    }

    protected void Button45_OnClick(object sender, EventArgs e)
    {
        ageDropDownList.SelectedValue = 1.ToString();
        ageTextBox.Text = "";
        ageMinTextBox.Text = "";
        ageMaxTextBox.Text = "";
        ageDropDownList_OnSelectedIndexChanged(null, null);
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


    //public async Task<ActionResult> GetLeaveData(string EmployeeID)
    //{
    //    SMCApiCall ad = new SMCApiCall();

    //    return Json(await ad.GetLeaveData(EmployeeID), JsonRequestBehavior.AllowGet);

    //}


   

    private async Task<List<SmcLeaveData>> LoadEMPInfo()
    {
        
            var EmployeeID = ddlEmpInfo.SelectedValue;

            _httpClient = new HttpClient();
            List<SmcLeaveData> aList = null;
            try
            {
                var response = await _httpClient.GetStringAsync("http://182.160.103.237:8089/smclms/index.php/api/leaveReport/index/" + EmployeeID + "");

                

                 

                DataTable dt = (DataTable)JsonConvert.DeserializeObject(response, (typeof(DataTable)));

                loadGridView.DataSource = dt;
                loadGridView.DataBind();

                for (int i = 0; i < loadGridView.Rows.Count; i++)
                {
                    HiddenField hfleave_type_id = (HiddenField)loadGridView.Rows[i].FindControl("hfleave_type_id");
                    Label lbLeaveType = (Label)loadGridView.Rows[i].FindControl("lbLeaveType");
                    Label lbAvailableLeave = (Label)loadGridView.Rows[i].FindControl("lbAvailableLeave");
                    Label lbtotal_available_leave = (Label)loadGridView.Rows[i].FindControl("lbtotal_available_leave");
                    Label lbtaken_leave = (Label)loadGridView.Rows[i].FindControl("lbtaken_leave");

                    if (hfleave_type_id.Value == "7")
                    {
                        lbLeaveType.Text = "Anual Leave";
                    }

                    if (hfleave_type_id.Value == "8")
                    {
                        lbLeaveType.Text = "Sick Leave";
                    }


                    if (hfleave_type_id.Value == "9")
                    {
                        lbLeaveType.Text = "Casual Leave";
                    }


                    try
                    {
                        decimal total_available = 0;
                        decimal taken_leave = 0;

                        if (lbtotal_available_leave.Text != "")
                        {
                            total_available = Convert.ToDecimal(lbtotal_available_leave.Text);
                        }


                        if (lbtaken_leave.Text != "")
                        {
                            taken_leave = Convert.ToDecimal(lbtaken_leave.Text);

                        }

                        decimal res = total_available - taken_leave;
                        lbAvailableLeave.Text = res.ToString();
                    }
                    catch (Exception)
                    {
                        lbAvailableLeave.Text = "0";
                        //throw;
                    }
                }
          
            }
            catch (Exception ex)
            {

            }

            return aList;
            
            
                

                
        
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
            parameter = parameter + "  and     com.CompanyId = '"+ddlCompany.SelectedValue+"'";
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

        if (NameTextBox.Text != "")
        {
            parameter = parameter + "  and  ( e.EmpName LIKE '%" + NameTextBox.Text.Trim() + "%')";
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

 


        return parameter;
    }

    private string BirthParameter()
    {
        string parameter = " ";
        if (ageDropDownList.SelectedValue != "1")
        {
            if (ageDropDownList.SelectedValue == "2" || ageDropDownList.SelectedValue == "3" ||
                ageDropDownList.SelectedValue == "4")
            {
                parameter = parameter + " AND DATEDIFF(year,ChildrenDOB,GETDATE())" +
                            ageDropDownList.SelectedItem.Text + " '" +
                            ageTextBox.Text + "' ";
            }
            else
            {
                parameter = parameter + " AND (DATEDIFF(year,ChildrenDOB,GETDATE()) between '" + ageMinTextBox.Text +
                            "' AND '" + ageMaxTextBox.Text + "') ";
            }
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



    }

    private void PopUp(Int32 EmpInfoId)
    {
        string url = "../Report_UI/EmployeeInfoListReportViwer.aspx?rptType=" + EmpInfoId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }
    private SMCApiCall _SMCApiCall = new SMCApiCall();

    [System.Web.Script.Services.ScriptMethod]
    protected async void SearchButton_OnClick(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0 && ddlEmpInfo.SelectedIndex > 0)
        {
            IncrementDal aIncrementDal = new IncrementDal();

            DataTable dtMain = aIncrementDal.LoadEmployeeInformationForDesignation(ddlEmpInfo.SelectedValue);
            if (dtMain.Rows.Count > 0)
            {
                hfDesig.Value = "Employee Designation: " + dtMain.Rows[0]["Designation"].ToString();
                hfEmpName.Value = "Employee Name: " + dtMain.Rows[0]["EmpName"].ToString();
            }
          await   LoadEMPInfo();
        }
        else
        {

            hfDesig.Value = "";
            hfEmpName.Value = "";
            ddlCompany.Focus();

            loadGridView.DataSource = null;
            loadGridView.DataBind();
          
            lblCount.Text = "Total : 0";
            aShowMessage.ShowMessageBox("Please Select All Searching Criteria !!!", this);

        }
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
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
            //using (DataTable dt = _commonDataLoad.GetDDLComDivision(ddlCompany.SelectedValue))
            //{
            //    ddlDivision.DataSource = dt;
            //    ddlDivision.DataValueField = "Value";
            //    ddlDivision.DataTextField = "TextField";
            //    ddlDivision.DataBind();
            //}

            //using (DataTable dt = _commonDataLoad.GetDDLComWind(ddlCompany.SelectedValue))
            //{
            //    ddlWing.DataSource = dt;
            //    ddlWing.DataValueField = "Value";
            //    ddlWing.DataTextField = "TextField";
            //    ddlWing.DataBind();
            //}
            //using (DataTable dt = _commonDataLoad.GetDDLComDepartment(ddlCompany.SelectedValue))
            //{
            //    ddlDepartment.DataSource = dt;
            //    ddlDepartment.DataValueField = "Value";
            //    ddlDepartment.DataTextField = "TextField";
            //    ddlDepartment.DataBind();
            //}
            //using (DataTable dt = _commonDataLoad.GetDDLComSection(ddlCompany.SelectedValue))
            //{
            //    ddlSection.DataSource = dt;
            //    ddlSection.DataValueField = "Value";
            //    ddlSection.DataTextField = "TextField";
            //    ddlSection.DataBind();
            //}
            //using (DataTable dt = _commonDataLoad.GetDDLComSubSection(ddlCompany.SelectedValue))
            //{
            //    ddlSubSection.DataSource = dt;
            //    ddlSubSection.DataValueField = "Value";
            //    ddlSubSection.DataTextField = "TextField";
            //    ddlSubSection.DataBind();
            //}

            //using (DataTable dt = _commonDataLoad.GetDDLSalaryLocation())
            //{
            //    ddlSalaryLocation.DataSource = dt;
            //    ddlSalaryLocation.DataValueField = "Value";
            //    ddlSalaryLocation.DataTextField = "TextField";
            //    ddlSalaryLocation.DataBind();
            //}


            using (DataTable dt222 = _commonDataLoad.GetEmpDDLIsActive(ddlCompany.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt222;
                ddlEmpInfo.DataValueField = "EmpMasterCode";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;
            }
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
//       // if (loadGridView.Rows.Count > 0)
//        {
//            string attachment = "attachment; filename=EmployeeInformationList.xls";
//            Response.ClearContent();
//            Response.AddHeader("content-disposition", attachment);
//            Response.ContentType = "application/ms-excel";
//            StringWriter sw = new StringWriter();
//            HtmlTextWriter htw = new HtmlTextWriter(sw);

           


             
           
        

//           loadGridView.AllowPaging = false;
//           this.LoadEMPInfo();
//            // Create a form to contain the grid  
//            HtmlForm frm = new HtmlForm();
//            loadGridView.Parent.Controls.Add(frm);
//            //frm.Attributes["runat"] = "server";
//            //frm.Controls.Add(loadGridView);
//            //frm.RenderControl(htw);

//            try
//            {
//                loadGridView.HeaderRow.Style.Add("background-color", "#E5EEF1");
//            }
//            catch (Exception)
//            {
                
//                //throw;
//            }

//            try
//            {
//                GridViewManagement.HeaderRow.Style.Add("background-color", "#E5EEF1");
//            }
//            catch (Exception)
//            {
                
//                //throw;
//            }

//            // Set background color of each cell of GridView1 header row

//            try
//            {
//                foreach (TableCell tableCell in loadGridView.HeaderRow.Cells)
//                {
//                    tableCell.Style["background-color"] = "#E5EEF1";
//                }
//            }
//            catch (Exception)
//            {
                
//                //throw;
//            }

//            // Set background color of each cell of each data row of GridView1

//            try
//            {
//                foreach (GridViewRow gridViewRow in loadGridView.Rows)
//                {
//                    gridViewRow.BackColor = System.Drawing.Color.White;

//                    foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
//                    {
//                        gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

//                    }
//                }

//            }
//            catch (Exception)
//            {
                
//                //throw;
//            }

//            try
//            {

//                foreach (GridViewRow gridViewRow in GridViewManagement.Rows)
//                {
//                    gridViewRow.BackColor = System.Drawing.Color.White;

//                    foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
//                    {
//                        gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

//                    }
//                }

//            }
//            catch (Exception)
//            {
                
//                //throw;
//            }


//            loadGridView.RenderControl(htw);
//            GridViewManagement.RenderControl(htw);
//            string headerTable = @"<span   style='text-align:center'>
//   <h2>SMC Enterprise Ltd.	</h2>
//   <h5>( Health & Hygiene Factory)	</h5>
//
//</span>";

//            string SubTi = @"<span   style='text-align:right'>
//   <h6> Date: "+DateTime.Now.ToString("dd.MM.yyyy")+"	</h6> </span>";

//            HttpContext.Current.Response.Write(headerTable);
//            HttpContext.Current.Response.Write(SubTi);
//            Response.Write(sw.ToString());
//            Response.End();
//        }
//        //else
//        //{
//        //    showMessageBox("No Data Found!!");
//        //}
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
        Response.Redirect("LeaveReport.aspx");
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
    private int rowofgrouheader = 0;
    private int totalheader = 0;
    private int nxtheader = 1;
    private int prerows = 0;
    private int countrow = 0; private string prerowmonth = "";
    protected void itemsGridView_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {

        
    }

    protected void itemsGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView) sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell = new TableCell();

            HeaderCell = new TableCell();
            HeaderCell.Text = "Management";
            HeaderCell.ColumnSpan = 11;
            HeaderCell.BackColor = Color.White;
            HeaderCell.ForeColor = Color.Black;
            HeaderCell.Font.Bold = true;
            HeaderCell.Font.Size = 13;
            HeaderCell.Height = 30;
            HeaderGridRow.Cells.Add(HeaderCell);
            GridViewManagement.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

            TableCell HeaderCell = new TableCell();


            string com = "";
            if (ddlCompany.SelectedItem.Text == "SMC")
            {
                com = "Social Marketing Company";

            }
            else
            {
                com = "SMC Enterprise Ltd.";
            }

            HeaderCell = new TableCell();
           
            HeaderCell.Text = "<p></p><p style='font-weight:bold!important'>" + com + "</p>" +
                            " <p>Current Leave Balance</p>" +
                            " <p>" + hfEmpName.Value + "</p>" +
                            " <p>" + hfDesig.Value + "</p>" +
                          
                              " <p> Reporting Date" + DateTime.Now.ToString("dd-MM-yyyy") + "</p>" +
                            " <p> &nbsp;" + "</p>";
            HeaderCell.ColumnSpan = 11;
            HeaderCell.BackColor = Color.White;
            HeaderCell.Height = 30;
            HeaderCell.ForeColor = Color.Black;
            HeaderCell.Font.Size = 13;
            HeaderCell.Font.Bold = false;
            HeaderGridRow.Cells.Add(HeaderCell);
            loadGridView.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }
    }
}