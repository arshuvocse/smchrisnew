using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.SKILL_WILL_DAL;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Skill_Will_Assessment_SkillWill_AssessmentListView : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    private KPISETUPListDAL _KPILIST = new KPISETUPListDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    JdDAL _jdDal = new JdDAL();

    private SkillWillDeclarationDal aDeclarationDal = new SkillWillDeclarationDal();

    private Skill_Will_Dal aDal = new Skill_Will_Dal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ReadonlyDateTime();
            LoadInitialDDL();
          //  ButtonVisible();
          if (!string.IsNullOrEmpty(Request.QueryString["masterId"]))
            {
                int mid = int.Parse(Request.QueryString["masterId"]);
                hid_KpiMasrerId.Value = mid.ToString();

                DataTable dt = aDal.GetSkillWillDetailsById(mid);
                subjectTextBox.Text = dt.Rows[0]["EmpName"].ToString();
                gv_allocateEmp.DataSource = dt;
                gv_allocateEmp.DataBind();
                CalculateTotal();
                WILLCalculateTotal();

            }

        }
    }

    protected void CalculateTotal()
    {
        decimal TSkill = 0;
        decimal AVGSkill = 0;

        decimal count = 0;
        if (gv_allocateEmp.Rows.Count > 0)
        {
            for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
            {

                //    if (chkIsActive.Checked)
                {


                    Label txtSkill = (Label)gv_allocateEmp.Rows[i].FindControl("txtSkill");

                    if (txtSkill.Text != "")
                    {
                        count = count + 1;

                    }







                    if (txtSkill.Text == "")
                    {
                        TSkill = TSkill + 0;
                    }
                    else
                    {
                        TSkill = TSkill + Convert.ToInt32(txtSkill.Text.ToString());
                    }






                }

            }

            string skillText = "";
            try
            {
                AVGSkill = TSkill / count;

                if (AVGSkill >= 4)
                {
                    skillText = "HIGH";
                }
                else
                {
                    skillText = "LOW";

                }
            }
            catch (Exception)
            {

                // throw;
            }

            Label lbl_SkillT = (Label)gv_allocateEmp.FooterRow.FindControl("lbl_SkillT");
            Label lbls_SkillT = (Label)gv_allocateEmp.FooterRow.FindControl("lbls_SkillT");

            lbl_SkillT.Text = Math.Round(AVGSkill, 2).ToString();
            lbls_SkillT.Text = skillText.ToString();
            //lblRejectQty.Text = TrQty.ToString();
            //lbl_SewingInputQtyT.Text = SewingInputQtyt.ToString();
        }
    }

    protected void WILLCalculateTotal()
    {
        decimal TSkill = 0;
        decimal AVGSkill = 0;

        decimal count = 0;
        if (gv_allocateEmp.Rows.Count > 0)
        {
            for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
            {

                //    if (chkIsActive.Checked)
                {


                    Label txtSkill = (Label)gv_allocateEmp.Rows[i].FindControl("txtWill");


                    if (txtSkill.Text != "")
                    {
                        count = count + 1;

                    }



                    if (txtSkill.Text == "")
                    {
                        TSkill = TSkill + 0;
                    }
                    else
                    {
                        TSkill = TSkill + Convert.ToInt32(txtSkill.Text.ToString());
                    }






                }

            }

            string skillText = "";
            try
            {
                AVGSkill = TSkill / count;

                if (AVGSkill >= 4)
                {
                    skillText = "HIGH";
                }
                else
                {
                    skillText = "LOW";

                }
            }
            catch (Exception)
            {

                // throw;
            }

            Label lbl_WilllT = (Label)gv_allocateEmp.FooterRow.FindControl("lbl_WilllT");
            Label lbls_WilllTd = (Label)gv_allocateEmp.FooterRow.FindControl("lbls_WilllTd");

            lbl_WilllT.Text = Math.Round(AVGSkill, 2).ToString();
            lbls_WilllTd.Text = skillText.ToString();
            //lblRejectQty.Text = TrQty.ToString();
            //lbl_SewingInputQtyT.Text = SewingInputQtyt.ToString();
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
          
        }
        else
        {
            Response.Redirect("~/Appraisal/KpiSetupList.aspx");
        }

    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }


    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //Session["cid"] = ddlCompany.SelectedValue;
        //DataTable dt = _trainingDal.GetFianncialYearByComIdDDl(Convert.ToInt32(ddlCompany.SelectedValue));
        //ddlFinancialYear.DataSource = dt;
        //ddlFinancialYear.DataValueField = "Value";
        //ddlFinancialYear.DataTextField = "TextField";
        //ddlFinancialYear.DataBind();

        //_jdDal.LoadDept(ddlDept, ddlCompany.SelectedValue);
    }
    private void LoadInitialDDL()
    {
        //using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        //{
        //    ddlCompany.DataSource = dt;
        //    ddlCompany.DataValueField = "Value";
        //    ddlCompany.DataTextField = "TextField";
        //    ddlCompany.DataBind();
        //}
        //ddlCompany.SelectedIndex = 1;
        //ddlCompany_OnSelectedIndexChanged(null, null);
        //_jdDal.LoadEmpCategory(ddlCategory);
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

        //DataTable dt = _jdDal.GetEmployeeForKpiSetUp(Convert.ToInt32(ddlCompany.SelectedValue.ToString()), deadLine, remarks);
        //gv_allocateEmp.DataSource = dt;
        //gv_allocateEmp.DataBind();

        //ViewState["EmpSetup"] = dt;

    }

    protected void txt_deadLine_OnTextChanged(object sender, EventArgs e)
    {
    }

    protected void txt_commonRemarks_OnTextChanged(object sender, EventArgs e)
    {

    }

    protected void chk_Common_OnCheckedChanged(object sender, EventArgs e)
    {
        //if (chk_Common.Checked && ViewState["EmpSetup"] != null)
        //{
        //    DataTable dt = (DataTable) ViewState["EmpSetup"];
        //    string remarks = txt_commonRemarks.Text.Trim().ToString();
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        dt.Rows[i]["DeadLine"] = txt_deadLine.Text.ToString();
        //        dt.Rows[i]["Remarks"] = remarks;
        //    }
        //    ViewState["EmpSetup"] = dt;
        //    gv_allocateEmp.DataSource = dt;
        //    gv_allocateEmp.DataBind();


        //    if (hid_KpiMasrerId.Value != "")
        //    {
        //        DataTable dt2 = _jdDal.GetKpiSetupDetailsByMaster(Convert.ToInt32(hid_KpiMasrerId.Value));

        //        for (int i = 0; i < dt2.Rows.Count; i++)
        //        {
        //            for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
        //            {
        //                CheckBox chk = (CheckBox)gv_allocateEmp.Rows[j].FindControl("txt_check");
        //                HiddenField txt_empInfoId = (HiddenField)gv_allocateEmp.Rows[j].FindControl("txt_empInfoId");
        //                TextBox txt_DeadLine = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_DeadLine");
        //                TextBox txt_Remarks = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_Remarks");

        //                if (txt_empInfoId.Value == dt2.Rows[i]["EmpinfoId"].ToString())
        //                {
        //                    chk.Checked = true;
        //                    txt_DeadLine.Text = Convert.ToDateTime(dt2.Rows[i]["DeadLine"].ToString()).ToString("dd-MMM-yyyy");
        //                    txt_Remarks.Text = dt2.Rows[i]["Remarks"].ToString();

        //                }

        //            }
        //        }
        //    }
        //}
        //else if (ViewState["EmpSetup"] != null && chk_Common.Checked == false)
        //{

        //    DataTable dt = (DataTable)ViewState["EmpSetup"];
        //    string remarks = txt_commonRemarks.Text.Trim().ToString();
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        dt.Rows[i]["DeadLine"] = "";
        //        dt.Rows[i]["Remarks"] = "";
        //    }
        //    ViewState["EmpSetup"] = dt;
        //    gv_allocateEmp.DataSource = dt;
        //    gv_allocateEmp.DataBind();

        //    if (hid_KpiMasrerId.Value != "")
        //    {
        //        DataTable dt2 = _jdDal.GetKpiSetupDetailsByMaster(Convert.ToInt32(hid_KpiMasrerId.Value));

        //        for (int i = 0; i < dt2.Rows.Count; i++)
        //        {
        //            for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
        //            {
        //                CheckBox chk = (CheckBox)gv_allocateEmp.Rows[j].FindControl("txt_check");
        //                HiddenField txt_empInfoId = (HiddenField)gv_allocateEmp.Rows[j].FindControl("txt_empInfoId");
        //                TextBox txt_DeadLine = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_DeadLine");
        //                TextBox txt_Remarks = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_Remarks");

        //                if (txt_empInfoId.Value == dt2.Rows[i]["EmpinfoId"].ToString())
        //                {
        //                    chk.Checked = true;
        //                    txt_DeadLine.Text = Convert.ToDateTime(dt2.Rows[i]["DeadLine"].ToString()).ToString("dd-MMM-yyyy");
        //                    txt_Remarks.Text = dt2.Rows[i]["Remarks"].ToString();

        //                }

        //            }
        //        }
        //    }
        //}


        try
        {






        }
        catch (Exception)
        {


          
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
        Response.Redirect("~/Appraisal/KpiDeadlineSetup.aspx");
    }

    public bool Validation()
    {
        bool isValid = true;

        if (subjectTextBox.Text == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Subject Required ", this);
            subjectTextBox.Focus();
            return false;
        }

        if (gv_allocateEmp.Rows.Count == 0)
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Employee List Requird ", this);
            return false;
        }

        int totalCount = gv_allocateEmp.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("txt_check")).Checked);

        if (totalCount == 0)
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Please Select Employee", this);
            return false;
        }


     

        return isValid;
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Skill_Will_Assessment/SkillWill_AssesmentView.aspx");
    }


    protected void AddNewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Skill_Will_Assessment/Skill_WillAssesmentList.aspx");
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

    

    

    

 
    

    protected void txt_DeadLine_ssOnTextChanged(object sender, EventArgs e)
    {

        for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
        {
            TextBox txt_DeadLine = (TextBox)gv_allocateEmp.Rows[i].FindControl("txt_DeadLine");
            CheckBox chk = (CheckBox)gv_allocateEmp.Rows[i].FindControl("txt_check");


            if (txt_DeadLine.Text.Trim() != "")
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
}