using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_DeadlineExtendedEntry : System.Web.UI.Page
{

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private DeadlineExtendedEntryDAL _aFincDal = new DeadlineExtendedEntryDAL();
    private ShowMessage aShowMessage = new ShowMessage();
    private PermissionDAL aPermissionDal = new PermissionDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserPersmissionValidation();
            ReadonlyDateTime();
            ButtonVisible();

            LoadInitialDDL();
            GetCompany();

            if (Session["EmpInfoId"] != null)
            {
                DataTable dtEmpComp = _aFincDal.GetEmployeeDetails(Convert.ToInt32(Session["EmpInfoId"]));
                if (dtEmpComp.Rows.Count > 0 && dtEmpComp.Rows[0]["CompanyId"] != DBNull.Value)
                {
                    string empCompId = dtEmpComp.Rows[0]["CompanyId"].ToString();
                    if (ddlCompany.Items.FindByValue(empCompId) != null)
                    {
                        ddlCompany.SelectedValue = empCompId;
                        Session["cid"] = empCompId;
                    }
                }
            }

            if (string.IsNullOrEmpty(ddlCompany.SelectedValue))
            {
                if (Session["cid"] != null && ddlCompany.Items.FindByValue(Session["cid"].ToString()) != null)
                {
                    ddlCompany.SelectedValue = Session["cid"].ToString();
                }
                else
                {
                    string aaa = lchk_Company.SelectedValue;
                    if (!string.IsNullOrEmpty(aaa) && ddlCompany.Items.FindByValue(aaa) != null)
                    {
                        ddlCompany.SelectedValue = aaa;
                    }
                }
            }


            DataTable dtaa = _aFincDal.GetFianncialYearByComIdDDl(Convert.ToInt32(ddlCompany.SelectedValue));
            ddlFinancialYear.DataSource = dtaa;
            ddlFinancialYear.DataValueField = "Value";
            ddlFinancialYear.DataTextField = "TextField";
            ddlFinancialYear.DataBind();
            ddlCompany_OnSelectedIndexChanged(null, null);



            if (DateTime.Now != null)
            {


                if (CheckStartEndDateExistOrNot(DateTime.Now, DateTime.Now) == true)
                {

                }

            }



            string deadLinea = "";


            if (chk_Common.Checked)
            {
                deadLinea = ExtendedDateTextBox.Text.ToString().Trim();

            }

            DataTable dta =
                _aFincDal.GetEmpForAppraisalDeadLineNewAnother(Convert.ToInt32(ddlCompany.SelectedValue.ToString()),
                    deadLinea, Parameter());


            if (dta.Rows.Count > 0)
            {
                DataTable dtEmp = _aFincDal.GetEmployeeDetails(Convert.ToInt32(Session["EmpInfoId"]));
                if (dtEmp.Rows.Count > 0)
                {

                    lblEmployeeName.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();

                    lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();





                    deptNameLabel.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();




                    desigNameLabel.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();


                    try
                    {

                        joiningDateLabel.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
                    }
                    catch (Exception)
                    {

                        //throw;
                    }

                    LocationLabel.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
                    lblPlace.Text = dtEmp.Rows[0]["Location"].ToString();

                    ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();


                }


                gv_allocateEmp.DataSource = dta;
                gv_allocateEmp.DataBind();
                gv_allocateEmp.Visible = true;
                ViewState["EmpSetup"] = dta;
            }
            else
            {
                gv_allocateEmp.DataSource = null;
                gv_allocateEmp.DataBind();
                gv_allocateEmp.Visible = true;
                ViewState["EmpSetup"] = null;
            }









            // UserPersmissionValidation();

            //DataTable dt = _aFincDal.GetSelfAppraisalList();
            //DataTable dt = _aFincDal.GetAppraisalByKpiPermission( );

            //gv_JdBoard.DataSource = dt;
            //gv_JdBoard.DataBind();


            // SearchButton_OnClick(null, null);

            if (!string.IsNullOrEmpty(Request.QueryString["masterId"]))
            {
                int mid = int.Parse(Request.QueryString["masterId"]);
                hid_KpiMasrerId.Value = mid.ToString();
                DataTable dt = _aFincDal.GetAppraisalSetupByMaster(mid);
                ddlCompany.SelectedValue = dt.Rows[0]["CompanyId"].ToString();
                ddlCompany_OnSelectedIndexChanged(ddlCompany, (EventArgs) e);
                ddlFinancialYear.SelectedValue = dt.Rows[0]["FinYearId"].ToString();
                RemarksTextBox.Text = dt.Rows[0]["Remarks"].ToString();
                // ddlFinancialYear_OnSelectedIndexChanged(ddlFinancialYear, (EventArgs)e);


                OperationDropDownList.Text = dt.Rows[0]["Operation"].ToString();
                RemarksTextBox.Text = dt.Rows[0]["Remarks"].ToString();
                //  ExtendedDateTextBox.Text = //dt.Rows[0]["ExtensionDate"]. ToString("dd-MMM-yyyy");
                //      Convert.ToDateTime(dt.Rows[0]["ExtensionDate"].ToString()).ToString("dd-MMM-yyyy");

                if (Convert.ToBoolean(dt.Rows[0]["IsDepartment"]) == true)
                {
                    rbDeptOrEmp.Items[0].Selected = true;
                    rbDeptOrEmp.Items[1].Selected = false;
                    DptShow.Visible = true;
                    ddlDept.SelectedValue = dt.Rows[0]["DepartmentId"].ToString();
                }


                if (Convert.ToBoolean(dt.Rows[0]["IsEmployee"]) == true)
                {
                    rbDeptOrEmp.Items[1].Selected = true;
                    rbDeptOrEmp.Items[0].Selected = false;
                    HideNDEPT.Value = dt.Rows[0]["DepartmentId"].ToString();
                }

                DescriptionTextBox.Text = dt.Rows[0]["Description"].ToString();


                DataTable dt2 = _aFincDal.GetAppraisalSetupDetailsByMaster(mid);

                //if (dt.Rows[0]["IsCommon"].ToString() == "True")
                //{
                //    chk_Common.Checked = true;
                //    ExtendedDateTextBox.Text = Convert.ToDateTime(dt2.Rows[0]["ExtensionDate"].ToString()).ToString("dd-MMM-yyyy");


                //}
                gv_allocateEmp.DataSource = dt2;
                gv_allocateEmp.DataBind();


                string deadLine = "";


                if (chk_Common.Checked)
                {
                    deadLine = ExtendedDateTextBox.Text.ToString().Trim();

                }

                DataTable dt3 =
                    _aFincDal.GetEmpForAppraisalDeadLineNew(Convert.ToInt32(ddlCompany.SelectedValue.ToString()),
                        deadLine, Parameter());
                gv_allocateEmp.DataSource = dt3;
                gv_allocateEmp.DataBind();


                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
                    {
                        CheckBox chk = (CheckBox) gv_allocateEmp.Rows[j].FindControl("txt_check");
                        HiddenField txt_empInfoId = (HiddenField) gv_allocateEmp.Rows[j].FindControl("txt_empInfoId");
                        TextBox ExtendedDate = (TextBox) gv_allocateEmp.Rows[j].FindControl("ExtendedDate");


                        if (txt_empInfoId.Value == dt2.Rows[i]["EmpinfoId"].ToString())
                        {
                            chk.Checked = true;
                            ExtendedDate.Text =
                                Convert.ToDateTime(dt2.Rows[i]["ExtendedDate"].ToString()).ToString("dd-MMM-yyyy");


                        }

                        //if (dt.Rows[0]["IsCommon"].ToString() == "True")
                        //{
                        //    ExtendedDate.Text = Convert.ToDateTime(dt2.Rows[i]["ExtendedDate"].ToString()).ToString("dd-MMM-yyyy");

                        //}
                    }
                }


            }
            rbDeptOrEmp.Items[0].Selected = false;

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

                    btn_Save.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    
                    //loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
                    //    Convert.ToBoolean(ViewState["Edit"].ToString());
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
    

    private void ReadonlyDateTime()
    {
        ExtendedDateTextBox.Attributes.Add("readonly", "readonly");

    }

    private bool CheckStartEndDateExistOrNot(DateTime Start, DateTime End)
    {
        bool status = false;
        string COMID = Session["cid"].ToString();

        DataTable dataTable = _aFincDal.CheckStartEndDateExistOrNotDAL(Start, End, COMID);

        if (dataTable.Rows.Count > 0)
        {
            ddlFinancialYear.SelectedValue = dataTable.Rows[0]["FinancialYearId"].ToString();
            status = true;
        }

        return status;
    }


    private bool CheckStartEndDateExistOrNot2(DateTime Start, DateTime End)
    {
        bool status = false;

        DataTable dataTable = _aFincDal.CheckStartEndDateExistOrNotDAL2(ddlFinancialYear.SelectedValue, Start, End);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {
            if (Session["Status"].ToString() == "Add")
            {
                btn_Save.Visible = true;
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                editButton.Visible = true;
            }
            else if (Session["Status"].ToString() == "Delete")
            {
                delButton.Visible = true;
            }
            Session["Status"] = null;
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

    private void LoadInitialDDL()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {

            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
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


    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("DeadlineExtendedEntryView.aspx");
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Session["cid"] = ddlCompany.SelectedValue;
        DataTable dt = _aFincDal.GetFianncialYearByComIdDDl(Convert.ToInt32(ddlCompany.SelectedValue));
        ddlFinancialYear.DataSource = dt;
        ddlFinancialYear.DataValueField = "Value";
        ddlFinancialYear.DataTextField = "TextField";
        ddlFinancialYear.DataBind();
        Clear();
        _aFincDal.LoadDept(ddlDept, ddlCompany.SelectedValue);

        using (DataTable dtemp = _commonDataLoad.GetEmpDDLAActive(ddlCompany.SelectedValue.ToString())
            )
        {


            ddlForwordEmp.DataSource = dtemp;
            ddlForwordEmp.DataValueField = "EmpInfoId";
            ddlForwordEmp.DataTextField = "EmpName";
            ddlForwordEmp.DataBind();
            ddlForwordEmp.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
            try
            {
                if (Session["EmpInfoId"] != null)
                {
                    string empIdStr = Session["EmpInfoId"].ToString();
                    if (ddlForwordEmp.Items.FindByValue(empIdStr) != null)
                    {
                        ddlForwordEmp.SelectedValue = empIdStr;
                        ddlForwordEmp_OnSelectedIndexChanged(null, null);
                    }
                    else
                    {
                        ddlForwordEmp.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception)
            {
                ddlForwordEmp.SelectedIndex = 0;
            }
        }
        if (Session["UserTypeId"].ToString() == "3" || Session["UserTypeId"].ToString() == "4" || Session["DepartmentId"].ToString() == "71" || Session["DepartmentId"].ToString() == "40")
        {
            comp.Visible = true;
            ddlForwordEmp.Enabled = true;
        }
        else
        {
            comp.Visible = false;

            ddlForwordEmp.Enabled = false;
            
        }
    }

    private void Clear()
    {

        for (int i = 0; i < rbDeptOrEmp.Items.Count; i++)
        {
            rbDeptOrEmp.Items[i].Selected = false;
        }
        DptShow.Visible = false;


    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        string deadLine = "";


        if (chk_Common.Checked)
        {
            deadLine = ExtendedDateTextBox.Text.ToString().Trim();

        }

        DataTable dt =
            _aFincDal.GetEmpForAppraisalDeadLineNewAnother(Convert.ToInt32(ddlCompany.SelectedValue.ToString()),
                deadLine, Parameter());

        if (dt.Rows.Count > 0)
        {

            gv_allocateEmp.DataSource = dt;
            gv_allocateEmp.DataBind();
            gv_allocateEmp.Visible = true;
            ViewState["EmpSetup"] = dt;
        }
        else
        {
            gv_allocateEmp.DataSource = null;
            gv_allocateEmp.DataBind();
            gv_allocateEmp.Visible = false;
            ViewState["EmpSetup"] = "";
        }

    }

    public string Parameter()
    {
        string param = "";
        //if (rbDeptOrEmp.Items[0].Selected == true)
        //{
        //    if (ddlDept.Items.Count > 0)
        //    {
        //        if (ddlDept.SelectedIndex > 0)
        //        {
        //            param = param + " AND A.DepartmentId='" + ddlDept.SelectedValue + "' ";
        //        }
        //    }
        //}

        //if (rbDeptOrEmp.Items[0].Selected == true)
        {
            param = param + " AND us.EmpInfoId='" + Convert.ToInt32(Session["EmpInfoId"]) + "' ";

        }
        return param;

    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation() == true)
        {
            DeadlineExtendedEntryDAO aMaster = new DeadlineExtendedEntryDAO();
            aMaster.DeadlineExtensionRequestId = hid_KpiMasrerId.Value == ""
                ? 0
                : Convert.ToInt32(hid_KpiMasrerId.Value);
            aMaster.DepartmentId = ddlDept.Text != "" && ddlDept.Text != null
                ? (int?) Convert.ToDecimal(ddlDept.Text)
                : null;
            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            aMaster.FinYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);


            if (rbDeptOrEmp.Items[0].Selected)
            {
                aMaster.IsDepartment = true;
                aMaster.IsEmployee = false;
                aMaster.DepartmentId = Convert.ToInt32(ddlDept.SelectedValue);
            }

            if (rbDeptOrEmp.Items[1].Selected)
            {
                aMaster.IsDepartment = false;
                aMaster.IsEmployee = true;
                aMaster.DepartmentId = Convert.ToInt32(HideNDEPT.Value);
            }


            aMaster.Operation = OperationDropDownList.Text;

            aMaster.Description = DescriptionTextBox.Text;
            aMaster.Remarks = RemarksTextBox.Text;
            aMaster.DeadlineExtensionRequestId = hid_KpiMasrerId.Value == ""
                ? 0
                : Convert.ToInt32(hid_KpiMasrerId.Value);

            List<DeadlineExtensionRequestDetailsDAO> aDetailses = new List<DeadlineExtensionRequestDetailsDAO>();



            //  int pk = _aFincDal.SaveDeadlineExtendedEntry(aMaster,Convert.ToInt32(Session["UserId"]));
            for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox) gv_allocateEmp.Rows[i].FindControl("txt_check");
                HiddenField txt_empInfoId = (HiddenField) gv_allocateEmp.Rows[i].FindControl("txt_empInfoId");
                TextBox ExtendedDate = (TextBox) gv_allocateEmp.Rows[i].FindControl("ExtendedDate");


                if (chk.Checked)
                {
                    //string aId = txt_empInfoId.Value.ToString();

                    DeadlineExtensionRequestDetailsDAO aDetails = new DeadlineExtensionRequestDetailsDAO();
                    aDetails.EmployeeId = Convert.ToInt32(gv_allocateEmp.DataKeys[i][0].ToString());
                    aDetails.ExtensionDate = Convert.ToDateTime(ExtendedDate.Text.Trim());


                    aDetailses.Add(aDetails);

                }
            }

            if (aDetailses.Count > 0)
            {

                int pk = _aFincDal.SaveDeadlineExtendedEntry(aMaster, Convert.ToInt32(Session["UserId"]));
                bool result = false;
                if (pk > 0)
                {
                    result = _aFincDal.SaveAppraisalSetupDetails(aDetailses, pk);
                }

                if (result == true)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successful...');window.location ='DeadlineExtendedEntryView.aspx';",
                        true);



                }
                else
                {
                    AlertMessageBoxShow("Operation Failed...");
                }
            }
        }


    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {


        Delete();


    }


    private void Delete()
    {
        DeadlineExtendedEntryDAO aMaster = new DeadlineExtendedEntryDAO();
        aMaster.DeadlineExtensionRequestId = hid_KpiMasrerId.Value == "" ? 0 : Convert.ToInt32(hid_KpiMasrerId.Value);

        if (hid_KpiMasrerId.Value != "")
        {
            aMaster.DeadlineExtensionRequestId = Convert.ToInt32(hid_KpiMasrerId.Value);

            aMaster.IsDelete = true;


            aMaster.DeleteBy = Convert.ToInt32(Session["UserId"]);



            aMaster.DeleteDate = DateTime.Now;
            //////aEmployeeRequsitionDal.DelOtherRequirementDetail(empIdHiddenField.Value);
            //////aEmployeeRequsitionDal.DelEducationRequirementsDetail(empIdHiddenField.Value);
            bool status = _aFincDal.DeleteEmployeeJobLeftById(aMaster);

            if (status)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Data Deleted Successfully...');window.location ='DeadlineExtendedEntryView.aspx';",
                    true);
            }
        }


    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {


        if (Validation() == true)
        {
            DeadlineExtendedEntryDAO aMaster = new DeadlineExtendedEntryDAO();

            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            aMaster.FinYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);



            if (rbDeptOrEmp.Items[0].Selected)
            {
                aMaster.IsDepartment = true;
                aMaster.IsEmployee = false;
                aMaster.DepartmentId = Convert.ToInt32(ddlDept.SelectedValue);
            }

            //  if (rbDeptOrEmp.Items[1].Selected)
            //{
            //    aMaster.IsDepartment = false;
            //    aMaster.IsEmployee = true;
            //    aMaster.DepartmentId = Convert.ToInt32(HideNDEPT.Value);
            //}


            aMaster.Operation = OperationDropDownList.Text;
            //   aMaster.ExtensionDate = Convert.ToDateTime(ExtendedDateTextBox.Text);
            aMaster.Description = DescriptionTextBox.Text;
            if (RemarksTextBox.Text != string.Empty)
            {
                aMaster.Remarks = RemarksTextBox.Text;
            }


            aMaster.DeadlineExtensionRequestId = hid_KpiMasrerId.Value == ""
                ? 0
                : Convert.ToInt32(hid_KpiMasrerId.Value);

            List<DeadlineExtensionRequestDetailsDAO> aDetailses = new List<DeadlineExtensionRequestDetailsDAO>();
            //  int pk = _aFincDal.SaveDeadlineExtendedEntry(aMaster,Convert.ToInt32(Session["UserId"]));
            for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox) gv_allocateEmp.Rows[i].FindControl("txt_check");
                HiddenField txt_empInfoId = (HiddenField) gv_allocateEmp.Rows[i].FindControl("txt_empInfoId");
                TextBox ExtendedDate = (TextBox) gv_allocateEmp.Rows[i].FindControl("ExtendedDate");


                //if (chk.Checked)
                {
                    //string aId = txt_empInfoId.Value.ToString();

                    DeadlineExtensionRequestDetailsDAO aDetails = new DeadlineExtensionRequestDetailsDAO();
                    aDetails.EmployeeId = Convert.ToInt32(gv_allocateEmp.DataKeys[i][0].ToString());
                    aDetails.ExtensionDate = Convert.ToDateTime(ExtendedDate.Text.Trim());


                    aDetailses.Add(aDetails);

                }
            }

            if (aDetailses.Count > 0)
            {

                int pk = _aFincDal.SaveDeadlineExtendedEntry(aMaster, Convert.ToInt32(Session["UserId"]));
                bool result = false;
                if (pk > 0)
                {
                    result = _aFincDal.SaveAppraisalSetupDetails(aDetailses, pk);
                }

                if (result == true)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successful...');window.location ='DeadlineExtendedEntry.aspx';",
                        true);



                }
                else
                {
                    AlertMessageBoxShow("Operation Failed...");
                }


            }



        }
    }



    public bool Validation()
    {
        bool isValid = true;
        if (ddlCompany.SelectedValue == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Company Required ", this);
            ddlCompany.Focus();
            return false;
        }

        if (string.IsNullOrEmpty(ddlForwordEmp.SelectedValue))
        {
            if (Session["EmpInfoId"] != null && ddlForwordEmp.Items.FindByValue(Session["EmpInfoId"].ToString()) != null)
            {
                ddlForwordEmp.SelectedValue = Session["EmpInfoId"].ToString();
            }
            else
            {
                isValid = false;
                aShowMessage.ShowMessageBox("Employee Required ", this);
                ddlForwordEmp.Focus();
                return false;
            }
        }

        if (ddlFinancialYear.SelectedValue == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Financial Year Required ", this);
            ddlFinancialYear.Focus();
            return false;
        }

        if (rbDeptOrEmp.Items[0].Selected)
        {
            if (ddlDept.SelectedValue == "")
            {
                isValid = false;
                aShowMessage.ShowMessageBox("Department Required ", this);
                ddlDept.Focus();
                return false;
            }
        }

        if (OperationDropDownList.SelectedIndex == 0)
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Operation Required ", this);
            OperationDropDownList.Focus();
            return false;
        }

        if (DescriptionTextBox.Text == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Operation Required ", this);
            DescriptionTextBox.Focus();
            return false;
        }

        if (
            _aFincDal.CheckPreviousDeadlineExtentionDateDetail(Convert.ToInt32(ddlCompany.SelectedValue),
                Convert.ToString(OperationDropDownList.Text),
                Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlForwordEmp.SelectedValue))
                .Rows.Count > 0)
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Entry  Already Exists", this);
            return false;
        }

        if (OperationDropDownList.SelectedValue == "KPI")
        {
            DateTime? ddDate = null;
            DateTime? extDate = null;
            extDate = Convert.ToDateTime(DateTime.Now);
            DataTable dt = _aFincDalcheck.GetAppraisalByKpiPermission(ddlForwordEmp.SelectedValue,
                ddlFinancialYear.SelectedItem.Text, "  and   tblAppraisalSelfAppLog.Version=CELog.MaxVer ");
            if (dt.Rows.Count > 0)
            {
                ddDate = Convert.ToDateTime(dt.Rows[0]["DeadLine"]); //  dt.Rows[0]["DeadLine"].ToString();


                if (ddDate >= extDate)
                {
                    AlertMessageBoxShow("Your KPI Deadline has not been expired yet!!!");
                    ExtendedDateTextBox.Text = "";
                    ExtendedDateTextBox.Focus();
                    isValid = false;
                }
                else
                {



                }

            }
            else
            {
                DataTable dt22 = _aFincDalcheck.GetAppraisalByKpiPermission(ddlForwordEmp.SelectedValue,
                    ddlFinancialYear.SelectedItem.Text, "");

                if (dt22.Rows.Count > 0)
                {
                    ddDate = Convert.ToDateTime(dt22.Rows[0]["DeadLine"]);
                    if (ddDate >= extDate)
                    {
                        AlertMessageBoxShow("Your KPI Deadline has not been expired yet!!!");
                        ExtendedDateTextBox.Text = "";
                        ExtendedDateTextBox.Focus();
                        isValid = false;
                    }
                    else
                    {


                    }
                }
                else
                {
                    aShowMessage.ShowMessageBox("Your KPI has not been declared yet!!!", this);
                    ExtendedDateTextBox.Text = "";
                    ExtendedDateTextBox.Focus();
                    isValid = false;
                }

            }
        }
        return isValid;
    }


    protected void cancelButton_OnClick(object sender, EventArgs e)
    {

    }

    protected void ddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ExtendedDateTextBox.Text = "";
    }

    private AppraisalFunctionalPartDAL _aFincDalcheck = new AppraisalFunctionalPartDAL();

    protected void ExtendedDateTextBox_TextChanged(object sender, EventArgs e)
    {
        try
        {
        if (OperationDropDownList.SelectedValue == "KPI")
        {

            if (ExtendedDateTextBox.Text != "")
            {
                for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
                {
                    //DataTable dt = (DataTable)ViewState["EmpSetup"];
                    TextBox ExtendedDate =
                        ((TextBox) gv_allocateEmp.Rows[j].Cells[5].FindControl("ExtendedDate"));


                    ExtendedDate.Text = ExtendedDateTextBox.Text;
                }
            }


            if (ExtendedDateTextBox.Text != "")
            {


                //if (
                //    CheckStartEndDateExistOrNot2(Convert.ToDateTime(ExtendedDateTextBox.Text),
                //        Convert.ToDateTime(ExtendedDateTextBox.Text)) == true)
                {





                    if (OperationDropDownList.SelectedValue == "KPI")
                    {


                        DateTime? ddDate = null;
                        DateTime? extDate = null;
                        extDate = Convert.ToDateTime(ExtendedDateTextBox.Text);
                        DataTable dt = _aFincDalcheck.GetAppraisalByKpiPermission(ddlForwordEmp.SelectedValue,
                            ddlFinancialYear.SelectedItem.Text, "  and   tblAppraisalSelfAppLog.Version=CELog.MaxVer ");
                        if (dt.Rows.Count > 0)
                        {
                            ddDate = Convert.ToDateTime(dt.Rows[0]["DeadLine"]); //  dt.Rows[0]["DeadLine"].ToString();


                            if (ddDate >= extDate)
                            {
                                AlertMessageBoxShow("Your KPI Deadline has not been expired yet!!!");
                                ExtendedDateTextBox.Text = "";
                                ExtendedDateTextBox.Focus();
                            }
                            else
                            {



                            }

                        }
                        else
                        {
                            DataTable dt22 = _aFincDalcheck.GetAppraisalByKpiPermission(ddlForwordEmp.SelectedValue,
                                ddlFinancialYear.SelectedItem.Text, "");

                            if (dt22.Rows.Count > 0)
                            {
                                ddDate = Convert.ToDateTime(dt22.Rows[0]["DeadLine"]);
                                if (ddDate >= extDate)
                                {
                                    AlertMessageBoxShow("Your KPI Deadline has not been expired yet!!!");
                                    ExtendedDateTextBox.Text = "";
                                    ExtendedDateTextBox.Focus();
                                }
                                else
                                {


                                }
                            }
                            else
                            {
                                aShowMessage.ShowMessageBox("Your KPI has not been declared yet!!!", this);
                                ExtendedDateTextBox.Text = "";
                                ExtendedDateTextBox.Focus();
                            }

                        }
                    }
                }
                //   else
                {
                    //aShowMessage.ShowMessageBox("Extension date must be within the finnancial year!!", this);
                    //ExtendedDateTextBox.Text = "";
                    //ExtendedDateTextBox.Focus();
                }
                //if (CheckStartEndDateExistOrNot2(Convert.ToDateTime(ExtendedDateTextBox.Text), Convert.ToDateTime(ExtendedDateTextBox.Text)) == false)
                //{


                //}
            }

        }


       else if (OperationDropDownList.SelectedValue == "BSC/OKR")
        {

            if (ExtendedDateTextBox.Text != "")
            {
                for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
                {
                    //DataTable dt = (DataTable)ViewState["EmpSetup"];
                    TextBox ExtendedDate =
                        ((TextBox)gv_allocateEmp.Rows[j].Cells[5].FindControl("ExtendedDate"));


                    ExtendedDate.Text = ExtendedDateTextBox.Text;
                }
            }


            if (ExtendedDateTextBox.Text != "")
            {


                //if (
                //    CheckStartEndDateExistOrNot2(Convert.ToDateTime(ExtendedDateTextBox.Text),
                //        Convert.ToDateTime(ExtendedDateTextBox.Text)) == true)
                {





                    if (OperationDropDownList.SelectedValue == "BSC/OKR")
                    {


                        DateTime? ddDate = null;
                        DateTime? extDate = null;
                        extDate = Convert.ToDateTime(ExtendedDateTextBox.Text);
                        DataTable dt = _aFincDalcheck.GetBSCAppraisalByKpiPermission(ddlForwordEmp.SelectedValue,
                            ddlFinancialYear.SelectedItem.Text, "  and   tblBSCAppraisalSelfAppLog.Version=CELog.MaxVer ");
                        if (dt.Rows.Count > 0)
                        {
                            ddDate = Convert.ToDateTime(dt.Rows[0]["DeadLine"]); //  dt.Rows[0]["DeadLine"].ToString();


                            if (ddDate >= extDate)
                            {
                                AlertMessageBoxShow("Your BSC/OKR Deadline has not been expired yet!!!");
                                ExtendedDateTextBox.Text = "";
                                ExtendedDateTextBox.Focus();
                            }
                            else
                            {



                            }

                        }
                        else
                        {
                            DataTable dt22 = _aFincDalcheck.GetBSCAppraisalByKpiPermission(ddlForwordEmp.SelectedValue,
                                ddlFinancialYear.SelectedItem.Text, "");

                            if (dt22.Rows.Count > 0)
                            {
                                ddDate = Convert.ToDateTime(dt22.Rows[0]["DeadLine"]);
                                if (ddDate >= extDate)
                                {
                                    AlertMessageBoxShow("Your BSC/OKR Deadline has not been expired yet!!!");
                                    ExtendedDateTextBox.Text = "";
                                    ExtendedDateTextBox.Focus();
                                }
                                else
                                {


                                }
                            }
                            else
                            {
                                aShowMessage.ShowMessageBox("Your BSC/OKR has not been declared yet!!!", this);
                                ExtendedDateTextBox.Text = "";
                                ExtendedDateTextBox.Focus();
                            }

                        }
                    }
                }
                //   else
                {
                    //aShowMessage.ShowMessageBox("Extension date must be within the finnancial year!!", this);
                    //ExtendedDateTextBox.Text = "";
                    //ExtendedDateTextBox.Focus();
                }
                //if (CheckStartEndDateExistOrNot2(Convert.ToDateTime(ExtendedDateTextBox.Text), Convert.ToDateTime(ExtendedDateTextBox.Text)) == false)
                //{


                //}
            }

        }
        else if (OperationDropDownList.SelectedValue == "Apprisal")
        {


            if (ExtendedDateTextBox.Text != "")
            {
                for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
                {
                    //DataTable dt = (DataTable)ViewState["EmpSetup"];
                    TextBox ExtendedDate =
                        ((TextBox) gv_allocateEmp.Rows[j].Cells[5].FindControl("ExtendedDate"));


                    ExtendedDate.Text = ExtendedDateTextBox.Text;
                }
            }
        }
        
        else if (OperationDropDownList.SelectedValue == "BSC/OKRApprisal")
        {


            if (ExtendedDateTextBox.Text != "")
            {
                for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
                {
                    //DataTable dt = (DataTable)ViewState["EmpSetup"];
                    TextBox ExtendedDate =
                        ((TextBox) gv_allocateEmp.Rows[j].Cells[5].FindControl("ExtendedDate"));


                    ExtendedDate.Text = ExtendedDateTextBox.Text;
                }
            }
        }
        else
        {
            AlertMessageBoxShow("Please Select Operation!!");
            ExtendedDateTextBox.Text = "";
        }
        }
        catch (Exception ex)
        {
            aShowMessage.ShowMessageBox(ex.Message, this);
        }
    }

    protected void rbDeptOrEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbDeptOrEmp.Items[0].Selected == true)
        {
            DptShow.Visible = true;
            ddlDept.SelectedValue = "";
            gv_allocateEmp.Visible = false;

        }

        if (rbDeptOrEmp.Items[1].Selected == true)
        {

            if (ValidEmp())
            {
                try
                {
                    ddlDept.SelectedValue = "";
                    DptShow.Visible = false;

                    string deadLine = "";


                    if (chk_Common.Checked)
                    {
                        deadLine = ExtendedDateTextBox.Text.ToString().Trim();

                    }

                    DataTable dt =
                        _aFincDal.GetEmpForAppraisalDeadLineNew(Convert.ToInt32(ddlCompany.SelectedValue.ToString()),
                            deadLine, Parameter());
                    if (rbDeptOrEmp.Items[1].Selected == true)
                    {

                        HideNDEPT.Value = dt.Rows[0]["EmpDepartmentNameID"].ToString();
                    }
                    gv_allocateEmp.DataSource = dt;
                    gv_allocateEmp.DataBind();

                    ViewState["EmpSetup"] = dt;
                }
                catch (Exception)
                {

                    // throw;
                }

            }


        }
    }

    public bool ValidEmp()
    {
        if (ddlCompany.SelectedIndex == 0)
        {
            AlertMessageBoxShow("Please Select this!!");
            ddlCompany.Focus();
            Clear();
        }

        if (ddlFinancialYear.SelectedIndex == 0)
        {
            AlertMessageBoxShow("Please Select this!!");
            ddlFinancialYear.Focus();
            Clear();
        }
        return true;
    }

    protected void txt_checkAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox) gv_allocateEmp.HeaderRow.FindControl("txt_checkAll");
        bool result = ChkBoxHeader.Checked == true ? true : false;

        for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox) gv_allocateEmp.Rows[i].FindControl("txt_check");
            chk.Checked = result;
        }
    }

    protected void chk_Common_OnCheckedChanged(object sender, EventArgs e)
    {

    }

    protected void deliveryDateTextBox_TextChanged(object sender, EventArgs e)
    {
        if (ExtendedDateTextBox.Text != "")
        {
            for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
            {
                //DataTable dt = (DataTable)ViewState["EmpSetup"];
                TextBox expectedDate =
                    ((TextBox) gv_allocateEmp.Rows[j].Cells[5].FindControl("ExtensionDate"));


                expectedDate.Text = ExtendedDateTextBox.Text;
            }
        }
    }

    protected void OperationDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ExtendedDateTextBox.Text = "";

    }

    protected void ddlForwordEmp_OnSelectedIndexChanged(object sender, EventArgs e)
    {


        string deadLinea = "";


        if (chk_Common.Checked)
        {
            deadLinea = ExtendedDateTextBox.Text.ToString().Trim();

        }
        DataTable dta =
            _aFincDal.GetEmpForAppraisalDeadLineNewAnother(Convert.ToInt32(ddlCompany.SelectedValue.ToString()),
                deadLinea, " AND us.EmpInfoId='" + Convert.ToInt32(ddlForwordEmp.SelectedValue) + "' ");


        if (dta.Rows.Count > 0)
        {
            DataTable dtEmp = _aFincDal.GetEmployeeDetails(Convert.ToInt32(ddlForwordEmp.SelectedValue));
            if (dtEmp.Rows.Count > 0)
            {

                lblEmployeeName.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();

                lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();





                deptNameLabel.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();




                desigNameLabel.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();


                try
                {

                    joiningDateLabel.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
                }
                catch (Exception)
                {

                    //throw;
                }

                LocationLabel.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
                lblPlace.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();


            }


            gv_allocateEmp.DataSource = dta;
            gv_allocateEmp.DataBind();
            gv_allocateEmp.Visible = true;
            ViewState["EmpSetup"] = dta;


        }
        else
        {
            gv_allocateEmp.DataSource = null;
            gv_allocateEmp.DataBind();
            gv_allocateEmp.Visible = true;
            ViewState["EmpSetup"] = null;
        }

    }
}