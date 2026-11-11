using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_DeadlineExtendedEntryApproval : System.Web.UI.Page
{

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private DeadlineExtendedEntryDAL _aFincDal = new DeadlineExtendedEntryDAL();
    ShowMessage aShowMessage = new ShowMessage();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonVisible();
            GetCompany();

            LoadInitialDDL();

            //  UserPersmissionValidation();

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
                ddlCompany_OnSelectedIndexChanged(ddlCompany, (EventArgs)e);
                ddlFinancialYear.SelectedValue = dt.Rows[0]["FinYearId"].ToString();
                RemarksTextBox.Text = dt.Rows[0]["Remarks"].ToString();
               // ddlFinancialYear_OnSelectedIndexChanged(ddlFinancialYear, (EventArgs)e);

               
                OperationDropDownList.Text = dt.Rows[0]["Operation"].ToString();
                RemarksTextBox.Text = dt.Rows[0]["Remarks"].ToString();
              //  ExtendedDateTextBox.Text = //dt.Rows[0]["ExtensionDate"]. ToString("dd-MMM-yyyy");
          //      Convert.ToDateTime(dt.Rows[0]["ExtensionDate"].ToString()).ToString("dd-MMM-yyyy");

                if (Convert.ToBoolean(dt.Rows[0]["IsDepartment"])==true)
                {
                    rbDeptOrEmp.Items[0].Selected = true;
                    rbDeptOrEmp.Items[1].Selected = false;
                    ddlDept.SelectedValue = dt.Rows[0]["DepartmentId"].ToString();
                }
           

                  if (Convert.ToBoolean(dt.Rows[0]["IsEmployee"])==true)
                {
                    rbDeptOrEmp.Items[1].Selected = true;
                    rbDeptOrEmp.Items[0].Selected = false;
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

                DataTable dt3 = _aFincDal.GetEmpForAppraisalDeadLineNew(Convert.ToInt32(ddlCompany.SelectedValue.ToString()), deadLine, Parameter());
                gv_allocateEmp.DataSource = dt3;
                gv_allocateEmp.DataBind();


                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
                    {
                        CheckBox chk = (CheckBox)gv_allocateEmp.Rows[j].FindControl("txt_check");
                        HiddenField txt_empInfoId = (HiddenField)gv_allocateEmp.Rows[j].FindControl("txt_empInfoId");
                        TextBox ExtendedDate = (TextBox)gv_allocateEmp.Rows[j].FindControl("ExtendedDate");
                       

                        if (txt_empInfoId.Value == dt2.Rows[i]["EmpinfoId"].ToString())
                        {
                            chk.Checked = true;
                            ExtendedDate.Text = Convert.ToDateTime(dt2.Rows[i]["ExtendedDate"].ToString()).ToString("dd-MMM-yyyy");
                           

                        }

                        //if (dt.Rows[0]["IsCommon"].ToString() == "True")
                        //{
                        //    ExtendedDate.Text = Convert.ToDateTime(dt2.Rows[i]["ExtendedDate"].ToString()).ToString("dd-MMM-yyyy");
                            
                        //}
                    }
                }
             
            
            }

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

        _aFincDal.LoadDept(ddlDept, ddlCompany.SelectedValue);
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        //string deadLine = "";
       

        //if (chk_Common.Checked)
        //{
        //    deadLine = ExtendedDateTextBox.Text.ToString().Trim();
            
        //}
        if (Validation() == true)
        {
            DataTable dt = _aFincDal.GetAppraisalSetupDetailsForApproval(NewParameter());
            gv_allocateEmp.DataSource = dt;
            gv_allocateEmp.DataBind();

            ViewState["EmpSetup"] = dt;

            if (dt.Rows.Count==0)
            {
                aShowMessage.ShowMessageBox("No Data Found", this);
            }
        }
    }

    private string NewParameter()
    {
        string param = "";
       
            if (ddlCompany.Items.Count > 0)
            {
                if (ddlCompany.SelectedIndex > 0)
                {
                    param = param + " AND Com.CompanyId ='" + ddlCompany.SelectedValue + "' ";
                }
            }

            if (ddlFinancialYear.Items.Count > 0)
            {
                if (ddlFinancialYear.SelectedIndex > 0)
                {
                    param = param + " AND FinY.FinancialYearId ='" + ddlFinancialYear.SelectedValue + "' ";
                }
            }
            if (ddlDept.Items.Count > 0)
            {
               
                if (ddlDept.SelectedIndex > 0)
                {
                    param = param + " AND Dpt.DepartmentId ='" + ddlDept.SelectedValue + "' ";
                }
           
        }

            if (ddlDept.Items.Count > 0)
            {

                if (ddlDept.SelectedIndex > 0)
                {
                    param = param + " AND Req.Operation ='" + OperationDropDownList.Text + "' ";
                }

            }


        if (rbDeptOrEmp.Items[1].Selected == true)
        {
            param = param + " AND us.EmpInfoId ='" + Convert.ToInt32(Session["UserId"]) + "' ";
        }
        return param;
    }

    public string Parameter()
    {
        string param = "";
        if (rbDeptOrEmp.Items[0].Selected == true)
        {
            if (ddlDept.Items.Count > 0)
            {
                if (ddlDept.SelectedIndex > 0)
                {
                    param = param + " AND A.DepartmentId='" + ddlDept.SelectedValue + "' ";
                }
            }
        }

        if (rbDeptOrEmp.Items[1].Selected == true)
        {
            param = param + " AND us.EmpInfoId='" + Convert.ToInt32(Session["UserId"]) + "' ";
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

            aMaster.DepartmentId = ddlDept.Text != "" && ddlDept.Text != null ? (int?)Convert.ToDecimal(ddlDept.Text) : null;
            if (rbDeptOrEmp.Items[0].Selected)
            {
                aMaster.IsDepartment = true;
                aMaster.IsEmployee = false;
            }

            if (rbDeptOrEmp.Items[1].Selected)
            {
                aMaster.IsDepartment = false;
                aMaster.IsEmployee = true;
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
                    aDetails.EmployeeId = Convert.ToInt32(txt_empInfoId.Value);
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

        if ( hid_KpiMasrerId.Value!="") 
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

        if (AppVali() == true)
        {
            List<DeadlineExtensionRequestDetailsDAO> aDetailses = new List<DeadlineExtensionRequestDetailsDAO>();
            List<AppraisalDeadLineDetails> AppDetailses = new List<AppraisalDeadLineDetails>();
            List<KPIDeadLineDetails> KPIDetailses = new List<KPIDeadLineDetails>();



            //  int pk = _aFincDal.SaveDeadlineExtendedEntry(aMaster,Convert.ToInt32(Session["UserId"]));
            for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox) gv_allocateEmp.Rows[i].FindControl("txt_check");
                HiddenField txt_empInfoId = (HiddenField) gv_allocateEmp.Rows[i].FindControl("txt_empInfoId");
                HiddenField HiddenFieldDetailsID =
                    (HiddenField) gv_allocateEmp.Rows[i].FindControl("HiddenFieldDetailsID");
                Label ExtendedDate = (Label) gv_allocateEmp.Rows[i].FindControl("ExtendedDate");



                if (chk.Checked)
                {
                    //string aId = txt_empInfoId.Value.ToString();
                    AppraisalDeadLineDetails AppaDetails = new AppraisalDeadLineDetails();
                    DeadlineExtensionRequestDetailsDAO aDetails = new DeadlineExtensionRequestDetailsDAO();
                    KPIDeadLineDetails KPDetails = new KPIDeadLineDetails();
                    aDetails.DeadlineExtensionRequestDetailsId = Convert.ToInt32(HiddenFieldDetailsID.Value);

                    aDetails.EmployeeId = Convert.ToInt32(txt_empInfoId.Value);
                    aDetails.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                    aDetails.ApproveDate = DateTime.Now;

                    if (OperationDropDownList.SelectedValue == "Apprisal")
                    {
                        AppaDetails.ExtensionDate = Convert.ToDateTime(ExtendedDate.Text);
                        AppDetailses.Add(AppaDetails);
                    }

                    if (OperationDropDownList.SelectedValue == "KPI")
                    {
                        KPDetails.ExtensionDate = Convert.ToDateTime(ExtendedDate.Text);
                        KPIDetailses.Add(KPDetails);
                    }
                  
                    aDetailses.Add(aDetails);
                  

                }
            }

            if (aDetailses.Count > 0)
            {

                //   int pk = _aFincDal.SaveDeadlineExtendedEntry(aMaster, Convert.ToInt32(Session["UserId"]));
                bool result = false;
                if (result == false)
                {
                    result = _aFincDal.SaveDeadlineExtensionRequestDetailsApproval(aDetailses, AppDetailses, KPIDetailses);
                }

                if (result == true)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Approved Successful...');window.location ='DeadlineExtendedEntryApproval.aspx';",
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

        if (ddlFinancialYear.SelectedValue == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Financial Year Required ", this);
            ddlFinancialYear.Focus();
            return false;
        }


        if (ddlDept.SelectedValue == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Department Required ", this);
            ddlDept.Focus();
            return false;
        }

        if (OperationDropDownList.SelectedValue == "Nullss")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Operation Required ", this);
            OperationDropDownList.Focus();
            return false;
        }


     




        //if (
        //    _jdDal.CheckPreviousAppraisalDeadline(Convert.ToInt32(ddlCompany.SelectedValue),
        //    Convert.ToInt32(ddlFinancialYear.SelectedValue), hid_KpiMasrerId.Value == "" ? 0 : Convert.ToInt32(hid_KpiMasrerId.Value)).Rows.Count > 0)
        //{
        //    isValid = false;
        //    aShowMessage.ShowMessageBox("Entry  Already Exists", this);
        //    return false;
        //}
        return isValid;
    }

    public bool AppVali()
        {
            bool isValid = true;
            if (gv_allocateEmp.Rows.Count == 0)
            {
                isValid = false;
                aShowMessage.ShowMessageBox("please Search", this);
                ddlCompany.Focus();
                return false;
            }
            return isValid;
         
    }
    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void ddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void ExtendedDateTextBox_TextChanged(object sender, EventArgs e)
    {

        if (chk_Common.Checked)
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
        }
      
    }
    protected void rbDeptOrEmp_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbDeptOrEmp.Items[0].Selected==true )
        {
            DptShow.Visible = true;
            ddlDept.SelectedValue = "";

        }

        else
        {

            ddlDept.SelectedValue = "";
            DptShow.Visible = false;

            string deadLine = "";


            if (chk_Common.Checked)
            {
                deadLine = ExtendedDateTextBox.Text.ToString().Trim();

            }

            DataTable dt = _aFincDal.GetEmpForAppraisalDeadLineNew(Convert.ToInt32(ddlCompany.SelectedValue.ToString()), deadLine, Parameter());
            gv_allocateEmp.DataSource = dt;
            gv_allocateEmp.DataBind();

            ViewState["EmpSetup"] = dt;

        }
    }

    protected void txt_checkAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox ChkBoxHeader = (CheckBox)gv_allocateEmp.HeaderRow.FindControl("txt_checkAll");
        bool result = ChkBoxHeader.Checked == true ? true : false;

        for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)gv_allocateEmp.Rows[i].FindControl("txt_check");
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
            ((TextBox)gv_allocateEmp.Rows[j].Cells[5].FindControl("ExtensionDate"));


                expectedDate.Text = ExtendedDateTextBox.Text;
            }
        }
    }
}