using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_KPIDeadlineSetupMailSend : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    JdDAL _jdDal = new JdDAL();
    AppraisalSetupListDAL _AppraisalSetupListDAL = new AppraisalSetupListDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonVisible();
            ReadonlyDateTime();
            LoadInitialDDL();
            CalendarExtender1.StartDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy"));
            if (!string.IsNullOrEmpty(Request.QueryString["masterId"]))
            {
                int mid = int.Parse(Request.QueryString["masterId"]);
                hid_KpiMasrerId.Value = mid.ToString();
                DataTable dt = _jdDal.GetKpiSetupByMaster(mid);
                ddlCompany.SelectedValue = dt.Rows[0]["CompanyId"].ToString();
                ddlCompany_OnSelectedIndexChanged(ddlCompany, (EventArgs) e);
                ddlFinancialYear.SelectedValue = dt.Rows[0]["FinancialYearId"].ToString();
                subjectTextBox.Text = dt.Rows[0]["Subject"].ToString();
                DeclarationTextBox.Text = Convert.ToDateTime(dt.Rows[0]["DeclarationDate"].ToString()).ToString("dd-MMM-yyyy");

                ddlFinancialYear_OnSelectedIndexChanged(ddlFinancialYear, (EventArgs) e);
                DataTable dt2 = _jdDal.GetKpiSetupDetailsByMaster(mid);
                
                if (dt.Rows[0]["IsCommon"].ToString() == "True")
                {
                    chk_Common.Checked = true;
                    txt_deadLine.Text = Convert.ToDateTime(dt2.Rows[0]["DeadLine"].ToString()).ToString("dd-MMM-yyyy");
                    txt_commonRemarks.Text = dt2.Rows[0]["Remarks"].ToString();

                }
              //  DataTable dtalll = _jdDal.GetEmpForAppraisalDeadLineNew(Convert.ToInt32(ddlCompany.SelectedValue.ToString()), null, null, Parameter());
                gv_allocateEmp.DataSource = dt2;
                gv_allocateEmp.DataBind();

                //for (int i = 0; i < dt2.Rows.Count; i++)
                //{
                //    for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
                //    {
                //        CheckBox chk = (CheckBox)gv_allocateEmp.Rows[j].FindControl("txt_check");
                //        HiddenField txt_empInfoId = (HiddenField)gv_allocateEmp.Rows[j].FindControl("txt_empInfoId");
                //        TextBox txt_DeadLine = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_DeadLine");
                //        TextBox txt_Remarks = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_Remarks");

                //        if (txt_empInfoId.Value == dt2.Rows[i]["EmpinfoId"].ToString())
                //        {
                //            chk.Checked = true;
                //            txt_DeadLine.Text = Convert.ToDateTime(dt2.Rows[i]["DeadLine"].ToString()).ToString("dd-MMM-yyyy");
                //            txt_Remarks.Text = dt2.Rows[i]["Remarks"].ToString();

                //        }

                //        if (dt.Rows[0]["IsCommon"].ToString() == "True")
                //        {
                //            txt_DeadLine.Text = Convert.ToDateTime(dt2.Rows[i]["DeadLine"].ToString()).ToString("dd-MMM-yyyy");
                //            txt_Remarks.Text = dt2.Rows[i]["Remarks"].ToString();
                //        }
                //    }
                //}
            }

        }
    }

    private void ReadonlyDateTime()
    {

       // txt_deadLine.Attributes.Add("readonly", "readonly");
      //  DeclarationTextBox.Attributes.Add("readonly", "readonly");
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
        else
        {
            Response.Redirect("KpiSetupList.aspx");
        }

    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Session["cid"] = ddlCompany.SelectedValue;
        DataTable dt = _trainingDal.GetFianncialYearByComIdDDl(Convert.ToInt32(ddlCompany.SelectedValue));
        ddlFinancialYear.DataSource = dt;
        ddlFinancialYear.DataValueField = "Value";
        ddlFinancialYear.DataTextField = "TextField";
        ddlFinancialYear.DataBind();
        _jdDal.LoadDept(ddlDept, ddlCompany.SelectedValue);
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
        ddlCompany.SelectedIndex = 1;
        ddlCompany_OnSelectedIndexChanged(null, null);
        _jdDal.LoadEmpCategory(ddlCategory);
    }

    protected void ddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        //string deadLine = "";
        //string remarks = "";

        //if (chk_Common.Checked)
        //{
        //    deadLine = txt_deadLine.Text.ToString().Trim();
        //    remarks = txt_commonRemarks.Text.ToString().Trim();
        //}

        //DataTable dt = _jdDal.GetEmpForAppraisalDeadLine(Convert.ToInt32(ddlCompany.SelectedValue.ToString()), deadLine, remarks);
        //gv_allocateEmp.DataSource = dt;
        //gv_allocateEmp.DataBind();

        //ViewState["EmpSetup"] = dt;

    }

    protected void txt_deadLine_OnTextChanged(object sender, EventArgs e)
    {

         try
        {
            string birthdt = Convert.ToDateTime(txt_deadLine.Text.Trim()).ToString("dd/MMM/yyyy");

        if (ViewState["EmpSetup"] != null && chk_Common.Checked)
        {
            DataTable dt = (DataTable) ViewState["EmpSetup"] ;
            string remarks = txt_commonRemarks.Text.Trim().ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["DeadLine"] = txt_deadLine.Text.ToString();
                dt.Rows[i]["Remarks"] = remarks;
            }
            ViewState["EmpSetup"] = dt;
            gv_allocateEmp.DataSource = dt;
            gv_allocateEmp.DataBind();

            if (hid_KpiMasrerId.Value != "")
            {
                DataTable dt2 = _AppraisalSetupListDAL.GetAppraisalSetupDetailsByMaster(Convert.ToInt32(hid_KpiMasrerId.Value));

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
                    {
                        CheckBox chk = (CheckBox)gv_allocateEmp.Rows[j].FindControl("txt_check");
                        HiddenField txt_empInfoId = (HiddenField)gv_allocateEmp.Rows[j].FindControl("txt_empInfoId");
                        TextBox txt_DeadLine = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_DeadLine");
                        TextBox txt_Remarks = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_Remarks");

                        if (txt_empInfoId.Value == dt2.Rows[i]["EmpinfoId"].ToString())
                        {
                            chk.Checked = true;
                            txt_DeadLine.Text = txt_deadLine.Text.ToString();
                            txt_Remarks.Text = remarks;

                        }

                    }
                }
            }

        }

        }
         catch (Exception)
         {


             AlertMessageBoxShow("Give A valid Date !!");
             txt_deadLine.Focus();
             txt_deadLine.Text = string.Empty;
         }
    }

    protected void txt_DeadLine_ssOnTextChanged(object sender, EventArgs e)
    {

        for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
        {
            TextBox txt_DeadLine = (TextBox)gv_allocateEmp.Rows[i].FindControl("txt_DeadLine");

            if (txt_DeadLine.Text.Trim()!="")
            {
                try
                {
                    string ssss = Convert.ToDateTime(txt_DeadLine.Text.Trim()).ToString("dd/MMM/yyyy");
                }
                catch (Exception)
                {


                    AlertMessageBoxShow("Give A valid Date !!");
                    txt_DeadLine.Focus();
                    txt_DeadLine.Text = string.Empty;
                } 
            }
            
        }
    }

    protected void txt_commonRemarks_OnTextChanged(object sender, EventArgs e)
    {
        if (ViewState["EmpSetup"] != null && chk_Common.Checked)
        {
            DataTable dt = (DataTable)ViewState["EmpSetup"];
            string remarks = txt_commonRemarks.Text.Trim().ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["DeadLine"] = txt_deadLine.Text.ToString();
                dt.Rows[i]["Remarks"] = remarks;
            }
            ViewState["EmpSetup"] = dt;
            gv_allocateEmp.DataSource = dt;
            gv_allocateEmp.DataBind();


            if (hid_KpiMasrerId.Value != "")
            {
                DataTable dt2 = _AppraisalSetupListDAL.GetAppraisalSetupDetailsByMaster(Convert.ToInt32(hid_KpiMasrerId.Value));

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
                    {
                        CheckBox chk = (CheckBox)gv_allocateEmp.Rows[j].FindControl("txt_check");
                        HiddenField txt_empInfoId = (HiddenField)gv_allocateEmp.Rows[j].FindControl("txt_empInfoId");
                        TextBox txt_DeadLine = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_DeadLine");
                        TextBox txt_Remarks = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_Remarks");

                        if (txt_empInfoId.Value == dt2.Rows[i]["EmpinfoId"].ToString())
                        {
                            chk.Checked = true;
                            txt_DeadLine.Text = txt_deadLine.Text.ToString();
                            txt_Remarks.Text = remarks;

                        }

                    }
                }
            }


        }
    }

    protected void chk_Common_OnCheckedChanged(object sender, EventArgs e)
    {
        if (chk_Common.Checked && ViewState["EmpSetup"] != null)
        {
            DataTable dt = (DataTable) ViewState["EmpSetup"];
            string remarks = txt_commonRemarks.Text.Trim().ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["DeadLine"] = txt_deadLine.Text.ToString();
                dt.Rows[i]["Remarks"] = remarks;
            }
            ViewState["EmpSetup"] = dt;
            gv_allocateEmp.DataSource = dt;
            gv_allocateEmp.DataBind();


            if (hid_KpiMasrerId.Value != "")
            {
                DataTable dt2 = _AppraisalSetupListDAL.GetAppraisalSetupDetailsByMaster(Convert.ToInt32(hid_KpiMasrerId.Value));

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
                    {
                        CheckBox chk = (CheckBox)gv_allocateEmp.Rows[j].FindControl("txt_check");
                        HiddenField txt_empInfoId = (HiddenField)gv_allocateEmp.Rows[j].FindControl("txt_empInfoId");
                        TextBox txt_DeadLine = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_DeadLine");
                        TextBox txt_Remarks = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_Remarks");

                        if (txt_empInfoId.Value == dt2.Rows[i]["EmpinfoId"].ToString())
                        {
                            chk.Checked = true;
                            txt_DeadLine.Text = Convert.ToDateTime(dt2.Rows[i]["DeadLine"].ToString()).ToString("dd-MMM-yyyy");
                            txt_Remarks.Text = dt2.Rows[i]["Remarks"].ToString();

                        }

                    }
                }
            }
        }
        else if (ViewState["EmpSetup"] != null && chk_Common.Checked == false)
        {

            DataTable dt = (DataTable)ViewState["EmpSetup"];
            string remarks = txt_commonRemarks.Text.Trim().ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["DeadLine"] = "";
                dt.Rows[i]["Remarks"] = "";
            }
            ViewState["EmpSetup"] = dt;
            gv_allocateEmp.DataSource = dt;
            gv_allocateEmp.DataBind();

            if (hid_KpiMasrerId.Value != "")
            {
                DataTable dt2 = _AppraisalSetupListDAL.GetAppraisalSetupDetailsByMaster(Convert.ToInt32(hid_KpiMasrerId.Value));

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
                    {
                        CheckBox chk = (CheckBox)gv_allocateEmp.Rows[j].FindControl("txt_check");
                        HiddenField txt_empInfoId = (HiddenField)gv_allocateEmp.Rows[j].FindControl("txt_empInfoId");
                        TextBox txt_DeadLine = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_DeadLine");
                        TextBox txt_Remarks = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_Remarks");

                        if (txt_empInfoId.Value == dt2.Rows[i]["EmpinfoId"].ToString())
                        {
                            chk.Checked = true;
                            txt_DeadLine.Text = Convert.ToDateTime(dt2.Rows[i]["DeadLine"].ToString()).ToString("dd-MMM-yyyy");
                            txt_Remarks.Text = dt2.Rows[i]["Remarks"].ToString();

                        }
                       
                    }
                }
            }
        }
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {


        if (Validation()==true)
        {
            AppraisalDeadlineMaster aMaster = new AppraisalDeadlineMaster();

            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);
            aMaster.IsCommon = chk_Common.Checked == true ? true : false;
            aMaster.Subject = subjectTextBox.Text;
            aMaster.DeclarationDate = Convert.ToDateTime(DeclarationTextBox.Text);
            aMaster.AppraisalDeadLineMasterId = hid_KpiMasrerId.Value == "" ? 0 : Convert.ToInt32(hid_KpiMasrerId.Value);

            List<AppraisalDeadLineDetails> aDetailses = new List<AppraisalDeadLineDetails>();

           


            for (int i = 0; i < SaveGridView.Rows.Count; i++)
            {

                HiddenField txt_empInfoId = (HiddenField)SaveGridView.Rows[i].FindControl("txt_empInfoId");
                Label txt_DeadLine = (Label)SaveGridView.Rows[i].FindControl("txt_DeadLine");
                Label txt_Remarks = (Label)SaveGridView.Rows[i].FindControl("txt_Remarks");

                //if (chk.Checked)
                //{
                //    //string aId = txt_empInfoId.Value.ToString();
                //    if (DeadLineValidation())
                //    {



                AppraisalDeadLineDetails aDetails = new AppraisalDeadLineDetails();
                aDetails.EmpinfoId = Convert.ToInt32(txt_empInfoId.Value);
                aDetails.DeadLine = Convert.ToDateTime(txt_DeadLine.Text.Trim());
                aDetails.Remarks = txt_Remarks.Text.Trim();

                aDetailses.Add(aDetails);
                //    }
                //}
            }

            if (aDetailses.Count > 0)
            {

                int pk = _AppraisalSetupListDAL.SaveAppraisalSetupMaster(aMaster, Session["LoginName"].ToString());
                bool result = false;
                if (pk > 0)
                {
                    result = _jdDal.SaveAppraisalSetupDetails(aDetailses, pk);
                }

                if (result == true)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
               "alert",
               "alert('Operation Successful...');window.location ='AppaisalSetupList.aspx';",
               true);
                    


                }
                else
                {
                    AlertMessageBoxShow("Operation Failed...");
                }


            }



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

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AppraisalDeadlineSetup.aspx");
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

        if (ddlFinancialYear.SelectedValue == "-1")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Financial Year Required ", this);
            ddlFinancialYear.Focus();
            return false;
        }

        if (DeclarationTextBox.Text == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Declaration Date Required ", this);
            DeclarationTextBox.Focus();
            return false;
        }

        if (subjectTextBox.Text == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Subject Required ", this);
            subjectTextBox.Focus();
            return false;
        }

        if (SaveGridView.Rows.Count == 0)
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Please Add to List One Employee !!!", this);

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

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("KpiSetupList.aspx");
    }

    protected void txt_checkAll_OnCheckedChanged(object sender, EventArgs e)
    {

        CheckBox ChkBoxHeader = (CheckBox)gv_allocateEmp.HeaderRow.FindControl("txt_checkAll");
        bool result = ChkBoxHeader.Checked==true?true:false;

        for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)gv_allocateEmp.Rows[i].FindControl("txt_check");
            chk.Checked = result;
        }
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation() == true)
        {
            AppraisalDeadlineMaster aMaster = new AppraisalDeadlineMaster();

            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);
            aMaster.IsCommon = chk_Common.Checked == true ? true : false;
            aMaster.Subject = subjectTextBox.Text; 
            aMaster.DeclarationDate = Convert.ToDateTime(DeclarationTextBox.Text);
            aMaster.AppraisalDeadLineMasterId = hid_KpiMasrerId.Value == "" ? 0 : Convert.ToInt32(hid_KpiMasrerId.Value);

            List<AppraisalDeadLineDetails> aDetailses = new List<AppraisalDeadLineDetails>();


            for (int i = 0; i < SaveGridView.Rows.Count; i++)
            {

                HiddenField txt_empInfoId = (HiddenField)SaveGridView.Rows[i].FindControl("txt_empInfoId");
                Label txt_DeadLine = (Label)SaveGridView.Rows[i].FindControl("txt_DeadLine");
                Label txt_Remarks = (Label)SaveGridView.Rows[i].FindControl("txt_Remarks");

                //if (chk.Checked)
                //{
                //    //string aId = txt_empInfoId.Value.ToString();
                //    if (DeadLineValidation())
                //    {



                AppraisalDeadLineDetails aDetails = new AppraisalDeadLineDetails();
                aDetails.EmpinfoId = Convert.ToInt32(txt_empInfoId.Value);
                aDetails.DeadLine = Convert.ToDateTime(txt_DeadLine.Text.Trim());
                aDetails.Remarks = txt_Remarks.Text.Trim();

                aDetailses.Add(aDetails);
                //    }
                //}
            }

            if (aDetailses.Count > 0)
            {

                int pk = _AppraisalSetupListDAL.SaveAppraisalSetupMaster(aMaster, Session["LoginName"].ToString());
                bool result = false;
                if (pk > 0)
                {
                    result = _jdDal.SaveAppraisalSetupDetails(aDetailses, pk);
                }

                if (result == true)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
               "alert",
               "alert('Operation Successful...');window.location ='AppaisalSetupList.aspx';",
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
        //LinkButton lb = (LinkButton)sender;
        //GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //int rowID = gvRow.RowIndex;

        

        bool result = _jdDal.DeleteAppraisalSetup(Convert.ToInt32(hid_KpiMasrerId.Value), Session["LoginName"].ToString());

        if (result == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successful...');window.location ='AppaisalSetupList.aspx';",
                true);

        }
        else
        {
            AlertMessageBoxShow("Operation Failed...");

        }
    }
    public string Parameter()
    {
        string param = "";
        if (ddlCategory.Items.Count > 0)
        {
            if (ddlCategory.SelectedIndex > 0)
            {
                param = param + " AND A.EmpCategoryId='" + ddlCategory.SelectedValue + "' ";
            }
        }
        if (ddlDept.Items.Count > 0)
        {
            if (ddlDept.SelectedIndex > 0)
            {
                param = param + " AND dpt.DepartmentId='" + ddlDept.SelectedValue + "' ";
            }
        }
        return param;

    }
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        string deadLine = "";
        string remarks = "";

        if (chk_Common.Checked)
        {
            deadLine = txt_deadLine.Text.ToString().Trim();
            remarks = txt_commonRemarks.Text.ToString().Trim();
        }

        DataTable dt = _jdDal.GetEmpForAppraisalDeadLineNewUpdate(Convert.ToInt32(ddlCompany.SelectedValue.ToString()), deadLine, remarks, Parameter());
        gv_allocateEmp.DataSource = dt;
        gv_allocateEmp.DataBind();

        ViewState["EmpSetup"] = dt;
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void DeclarationTextBox_OnTextChanged(object sender, EventArgs e)
    {

        try
        {
            
            string Declarationdt = Convert.ToDateTime(DeclarationTextBox.Text.Trim()).ToString("dd/MMM/yyyy");
        }
        catch (Exception)
        {


            AlertMessageBoxShow("Give A valid Date !!");
            DeclarationTextBox.Focus();
            DeclarationTextBox.Text = string.Empty;
        }
    }

    public bool CheckEmpList()
    {
        for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_allocateEmp.Rows[i].Cells[0].FindControl("txt_check");
            for (int j = 0; j < SaveGridView.Rows.Count; j++)
            {
                if (chkBoxRows.Checked)
                {
                    Label SSStxt_empId = (Label)SaveGridView.Rows[j].FindControl("txt_empId");

                    Label EmpDI = (Label)gv_allocateEmp.Rows[i].FindControl("txt_empId");

                    if (EmpDI.Text == SSStxt_empId.Text)
                    {


                        return false;

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

    private bool DeadLineValidation()
    {
        if (gv_allocateEmp.Rows.Count == 0)
        {
            AlertMessageBoxShow("Please select at least one employee !!!");
            return false;
        }

        int totalCount = gv_allocateEmp.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("txt_check")).Checked);

        if (totalCount == 0)
        {

            aShowMessage.ShowMessageBox("Please Select Employee", this);
            return false;
        }





        for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
        {
            CheckBox chk = (CheckBox)gv_allocateEmp.Rows[i].FindControl("txt_check");
            TextBox txt_DeadLine = (TextBox)gv_allocateEmp.Rows[i].FindControl("txt_DeadLine");



            if ((txt_DeadLine.Text == "") && (chk.Checked == true))
            {
                AlertMessageBoxShow("Enter Dead Line Date...");
                txt_DeadLine.Focus();
                return false;


            }

            if ((txt_DeadLine.Text != ""))
            {
                try
                {
                    string Declarationdt = Convert.ToDateTime(txt_DeadLine.Text.Trim()).ToString("dd/MMM/yyyy");
                }
                catch (Exception)
                {


                    AlertMessageBoxShow("Give A valid Date !!");
                    txt_DeadLine.Focus();
                    txt_DeadLine.Text = string.Empty;
                }
            }

        }
        return true;
    }


    protected void textButton_OnClick(object sender, EventArgs e)
    {
        if (DeadLineValidation())
        {
            if (CheckEmpList())
            {

                DataTable aDataTable = new DataTable();
                aDataTable.Columns.Add("EmpInfoId");
                aDataTable.Columns.Add("EmpMasterCode");
                aDataTable.Columns.Add("EmpName");
                aDataTable.Columns.Add("Designation");
                aDataTable.Columns.Add("DepartmentName");
                aDataTable.Columns.Add("DivisionName");
                aDataTable.Columns.Add("DeadLine");
                aDataTable.Columns.Add("Remarks");



                DataRow dataRow = null;

                for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
                {
                    CheckBox ChkBoxRows = (CheckBox)gv_allocateEmp.Rows[i].Cells[0].FindControl("txt_check");
                    HiddenField txt_empInfoId = ((HiddenField)gv_allocateEmp.Rows[i].FindControl("txt_empInfoId"));
                    Label txt_empId = (Label)gv_allocateEmp.Rows[i].FindControl("txt_empId");
                    Label txt_name = (Label)gv_allocateEmp.Rows[i].FindControl("txt_name");
                    Label txt_designation = (Label)gv_allocateEmp.Rows[i].FindControl("txt_designation");
                    Label txt_dptName = (Label)gv_allocateEmp.Rows[i].FindControl("txt_dptName");
                    Label txt_division = (Label)gv_allocateEmp.Rows[i].FindControl("txt_division");
                    TextBox txt_DeadLine = (TextBox)gv_allocateEmp.Rows[i].FindControl("txt_DeadLine");
                    TextBox txt_Remarks = (TextBox)gv_allocateEmp.Rows[i].FindControl("txt_Remarks");
                    if (ChkBoxRows.Checked)
                    {
                        //  if (HasDCStoreId(Convert.ToInt32(loadGridView.DataKeys[i][0].ToString())))
                        {



                            dataRow = aDataTable.NewRow();

                            dataRow["EmpInfoId"] = txt_empInfoId.Value;

                            dataRow["EmpMasterCode"] = txt_empId.Text;
                            dataRow["EmpName"] = txt_name.Text;
                            dataRow["Designation"] = txt_designation.Text;
                            dataRow["DepartmentName"] = txt_dptName.Text;
                            dataRow["DivisionName"] = txt_division.Text;
                            dataRow["DeadLine"] = txt_DeadLine.Text;
                            dataRow["Remarks"] = txt_Remarks.Text;



                            aDataTable.Rows.Add(dataRow);
                        }
                    }
                }
                for (int i = 0; i < SaveGridView.Rows.Count; i++)
                {

                    CheckBox ChkBoxRows = (CheckBox)SaveGridView.Rows[i].Cells[0].FindControl("txt_check");
                    HiddenField txt_empInfoId = ((HiddenField)SaveGridView.Rows[i].FindControl("txt_empInfoId"));
                    Label txt_empId = (Label)SaveGridView.Rows[i].FindControl("txt_empId");
                    Label txt_name = (Label)SaveGridView.Rows[i].FindControl("txt_name");
                    Label txt_designation = (Label)SaveGridView.Rows[i].FindControl("txt_designation");
                    Label txt_dptName = (Label)SaveGridView.Rows[i].FindControl("txt_dptName");
                    Label txt_division = (Label)SaveGridView.Rows[i].FindControl("txt_division");
                    Label txt_DeadLine = (Label)SaveGridView.Rows[i].FindControl("txt_DeadLine");
                    Label txt_Remarks = (Label)SaveGridView.Rows[i].FindControl("txt_Remarks");

                    dataRow = aDataTable.NewRow();
                    dataRow["EmpInfoId"] = txt_empInfoId.Value;

                    dataRow["EmpMasterCode"] = txt_empId.Text;
                    dataRow["EmpName"] = txt_name.Text;
                    dataRow["Designation"] = txt_designation.Text;
                    dataRow["DepartmentName"] = txt_dptName.Text;
                    dataRow["DivisionName"] = txt_division.Text;
                    dataRow["DeadLine"] = txt_DeadLine.Text;
                    dataRow["Remarks"] = txt_Remarks.Text;



                    aDataTable.Rows.Add(dataRow);
                }

                SaveGridView.DataSource = aDataTable;
                SaveGridView.DataBind();
            }
            else
            {
                ShowMessageBox("Already Exist !!!");
            }
        }
    }

    protected void deleteImageButtonDirectlySupervices_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("EmpInfoId");
        aDataTable.Columns.Add("EmpMasterCode");
        aDataTable.Columns.Add("EmpName");
        aDataTable.Columns.Add("Designation");
        aDataTable.Columns.Add("DepartmentName");
        aDataTable.Columns.Add("DivisionName");
        aDataTable.Columns.Add("DeadLine");
        aDataTable.Columns.Add("Remarks");

        DataRow dataRow;

        if (SaveGridView.Rows.Count > 0)
        {
            for (int i = 0; i < SaveGridView.Rows.Count; i++)
            {
                HiddenField txt_empInfoId = ((HiddenField)SaveGridView.Rows[i].FindControl("txt_empInfoId"));
                Label txt_empId = (Label)SaveGridView.Rows[i].FindControl("txt_empId");
                Label txt_name = (Label)SaveGridView.Rows[i].FindControl("txt_name");
                Label txt_designation = (Label)SaveGridView.Rows[i].FindControl("txt_designation");
                Label txt_dptName = (Label)SaveGridView.Rows[i].FindControl("txt_dptName");
                Label txt_division = (Label)SaveGridView.Rows[i].FindControl("txt_division");
                Label txt_DeadLine = (Label)SaveGridView.Rows[i].FindControl("txt_DeadLine");
                Label txt_Remarks = (Label)SaveGridView.Rows[i].FindControl("txt_Remarks");
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();

                    dataRow["EmpInfoId"] = txt_empInfoId.Value;

                    dataRow["EmpMasterCode"] = txt_empId.Text;
                    dataRow["EmpName"] = txt_name.Text;
                    dataRow["Designation"] = txt_designation.Text;
                    dataRow["DepartmentName"] = txt_dptName.Text;
                    dataRow["DivisionName"] = txt_division.Text;
                    dataRow["DeadLine"] = txt_DeadLine.Text;
                    dataRow["Remarks"] = txt_Remarks.Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }


        SaveGridView.DataSource = aDataTable;
        SaveGridView.DataBind();
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        try
        {

            for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
            {
                CheckBox ChkBoxRows = (CheckBox) gv_allocateEmp.Rows[i].Cells[0].FindControl("txt_check");
                Label EmpMail = (Label) gv_allocateEmp.Rows[i].Cells[1].FindControl("txt_MAilID");
                Label txt_DeadLine = (Label) gv_allocateEmp.Rows[i].Cells[1].FindControl("txt_DeadLine");
                Label txt_Remarks = (Label) gv_allocateEmp.Rows[i].Cells[1].FindControl("txt_Remarks");

                if (VallliMail())
                {



                    if (ChkBoxRows.Checked)
                    {
                        System.Threading.Thread.Sleep(100);

                        MailMessage mail = new MailMessage();

                        try
                        {
                            mail.To.Add(EmpMail.Text.Trim());
                        }
                        catch (Exception)
                        {

                            //throw;
                        }

                        //mail.To.Add
                        mail.From = new MailAddress(Session["EmailID"].ToString());
                        mail.From.User.ToString();

                        mail.Sender = new System.Net.Mail.MailAddress(Session["EmailID"].ToString());
                        mail.Subject = subjectTextBox.Text;

                        MailAddress copy2 = new MailAddress(Session["EmailID"].ToString());
                        mail.Bcc.Add(copy2);



                        mail.Body =
                            "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/><br/>" +
 @"Dear Sir, 
<br/><br/>
Your KPI is pending. Your KPI submission deadline is " +    txt_DeadLine.Text+  @".<br/><br/>
 please login for the details from the below link.<br/>    http://182.160.103.234:8088/
 <br/> Thank You.</div>";
                        mail.IsBodyHtml = true;
                        mail.Priority = System.Net.Mail.MailPriority.High;

                        //Attach file using FileUpload Control and put the file in memory stream

                        //Attach file using FileUpload Control and put the file in memory stream

                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                        smtp.Credentials = new System.Net.NetworkCredential
                            (Session["EmailID"].ToString(), Session["AppPass"].ToString());
                        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                        smtp.Port = 587;
                        smtp.EnableSsl = true;

                        try
                        {
                            smtp.Send(mail);

                        }
                        catch (System.Net.Mail.SmtpException ex)
                        {
                           // showMessageBox("Email has not Sent, Try Once More time");
                        }
                        catch (Exception exe)
                        {
                          //  showMessageBox("Email has not Sent, Try Once More time");
                        }


                        System.Threading.Thread.Sleep(100);
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Sent Successfully...! ');window.location ='KpiSetupList.aspx';",
                            true);

                    }
                }
            }

        }
        catch (Exception)
        {

            //throw;
        }
    }

    private bool VallliMail()
    {
        int totalCount = gv_allocateEmp.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("txt_check")).Checked);

        if (totalCount == 0)
        {

            aShowMessage.ShowMessageBox("Please Select Employee", this);
            return false;
        }
        return true;
    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
}