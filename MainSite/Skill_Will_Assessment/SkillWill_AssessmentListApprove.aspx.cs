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
using DAL.SKILL_WILL_DAL;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class Skill_Will_SkillWill_AssessmentListApprove : System.Web.UI.Page
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


            RadioTextValue();

            //  ButtonVisible();
            if (!string.IsNullOrEmpty(Request.QueryString["masterId"]))
            {
                int mid = int.Parse(Request.QueryString["masterId"]);
                hid_KpiMasrerId.Value = mid.ToString();

                DataTable dt = aDal.GetSkillWillDetailsById(mid);
                subjectTextBox.Text = dt.Rows[0]["EmpName"].ToString();

                GetEmpinfo(dt.Rows[0]["EmpInfoId"].ToString());
                gv_allocateEmp.DataSource = dt;
                gv_allocateEmp.DataBind();
                CalculateTotal();
                WILLCalculateTotal();

            }

        }
    }
    public void RadioTextValue()
    {
        //string filepath = Path.GetDirectoryName(Request.Path);
        //filepath = filepath.TrimStart('\\');
        //filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
        string filepath = "";
        if (Session["AppPage"] != null)
        {
            filepath = Session["AppPage"].ToString();
        }

        DataTable dtdata = _appPartA.GetSupervisorEmployeeAppId(Session["EmpInfoId"].ToString(), Request.QueryString["EmpInfoId"]);

        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("Value");
        aDataTable.Columns.Add("Text");

        DataRow dataRow = null;


        //if (Session["ForEmpInfoId"].ToString() != Session["EmpInfoId"].ToString())
        if (dtdata.Rows.Count > 0)
        {
            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Approved";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Return";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);

        }
        else
        {
            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Verified";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Return";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);
        }

        actionRadioButtonList.DataValueField = "Value";
        actionRadioButtonList.DataTextField = "Text";
        actionRadioButtonList.DataSource = aDataTable;
        actionRadioButtonList.DataBind();
        //actionRadioButtonList.Items[0].Selected = true;

        try
        {
            if (Session["ForEmpInfoId"].ToString() == Session["EmpInfoId"].ToString())
            {
                actionRadioButtonList.Items[1].Enabled = false;
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();

    public void GetEmpinfo(string id)
    {
        //string empid = txt_employee.Text.Trim();
        //if (empid.Contains(":"))
        {
            //string[] strsp = empid.Split(':');
            //int empId = _trainingDal.GetEmployeeIdByCode(strsp[0]);

            DataTable dtEmp = _appPartA.GetEmployeeDetails(Convert.ToInt32(id));
            if (dtEmp.Rows.Count > 0)
            {
                
                HFCompanyId.Value = dtEmp.Rows[0]["CompanyId"].ToString();
              
                lblEmployeeName.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();
                lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();

             

              
 


                deptNameLabel.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();
                

                
 

                desigNameLabel.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();
                

                joiningDateLabel.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
                LocationLabel.Text = dtEmp.Rows[0]["Location"].ToString();
                lblPlace.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();

                ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();

                 

            }
        }
        //else
        //{
        //    txt_employee.Text = "";

        //    id_Empid.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}
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
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {

        if (actionRadioButtonList.SelectedValue != "")
        {
            EmpSkillWillAssessmentMasterAppLogDAO aMasterApp = new EmpSkillWillAssessmentMasterAppLogDAO();
            aMasterApp.EmpSkillWillMasterId
                = Convert.ToInt32(hid_KpiMasrerId.Value);




            aMasterApp.ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = aDal.UpdateContractural(aMasterApp);





            if (status)
            {

                if (aMasterApp.ActionStatus == "Approved")
                {




                    //DataTable dtempdata = aContractualEmpManageDAL.GetEmpInfo(" WHERE EmpInfoId='" + empInfoId.Value + "'");
                    EmpSkillWillAssessmentMasterAppLogDAO appLogDao = new EmpSkillWillAssessmentMasterAppLogDAO()
                    {
                        ActionStatus = actionRadioButtonList.SelectedValue,
                        ApproveDate = DateTime.Now,
                        ApproveBy = Session["UserId"].ToString(),
                        PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                        ForEmpInfoId = 0,
                        EmpSkillWillMasterId = aMasterApp.EmpSkillWillMasterId,
                        Comments = commentsTextBox.Text,
                        CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                    };
                    int id = aDal.SaveEmpAppLog(appLogDao);


                    SenMailForApprved(appLogDao.ForEmpInfoId, "Skill Will Assessment  Approval ", @"  <br/> Dear Sir, <br/>
An Employee's Skill Will Assessment  Approval is waiting for your approval. <br/><br/>
please login with the below link.<br/><br/>   http://182.160.103.234:8088/
<br/> Thank You.");

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "alert",
                      "alert('Operation Successful...');window.location ='SkillWill_AssesmentApprovalList.aspx';",
                      true);
                }
                else if (aMasterApp.ActionStatus == "Review")
                {
                    DataTable dtempdata = aDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), hid_KpiMasrerId.Value);
                    DataTable dtempdata2 = aDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), hid_KpiMasrerId.Value);

                    if (dtempdata2.Rows.Count > 0)
                    {
                        EmpSkillWillAssessmentMasterAppLogDAO appLogDao = new EmpSkillWillAssessmentMasterAppLogDAO()
                        {
                            ActionStatus = "Verified",
                            ApproveDate = DateTime.Now,
                            ApproveBy = Session["UserId"].ToString(),
                            PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString()),
                            ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString()),
                            EmpSkillWillMasterId = aMasterApp.EmpSkillWillMasterId,
                            Comments = commentsTextBox.Text,
                            CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                        };
                        aDal.UpdateAppLog("Review", Session["AppLogId"].ToString());
                        int id = aDal.SaveEmpAppLog(appLogDao);



                        SenMailForApprved(appLogDao.ForEmpInfoId, " KPI Setup Approval ", @"  <br/> Dear Sir, <br/>
Returned Skill Will Assessment  Approval. <br/><br/>
please login with the below link.<br/><br/>   http://182.160.103.234:8088/
<br/> Thank You.");

                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Operation Successful...');window.location ='SkillWill_AssesmentApprovalList.aspx';",
                       true);
                    }
                }
            }
        }
        else
        {
            AlertMessageBoxShow("Please Select Approval Status");
        }
    }
                    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    private void SenMailForApprved(int forEmpID, string mSubject, string mBody)
    {



        string ForMailAddress = "";
        using (var db = new HRIS_SMCEntities())
        {
            var GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                  where t.EmpInfoId == forEmpID
                                  select t).FirstOrDefault();

            if (GetMailAddress != null)
            {
                ForMailAddress = GetMailAddress.OfficialEmail;
            }



        }

        if (ForMailAddress != "")
        {
            System.Threading.Thread.Sleep(100);

            MailMessage mail = new MailMessage();




            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(Session["EmailID"].ToString());
            try
            {
                mail.To.Add(ForMailAddress.Trim());
            }
            catch (Exception)
            {
                //throw;
            }
            mail.Subject = mSubject;
            mail.Body =
                "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +
                WebUtility.HtmlDecode(mBody)
                +
                "</div>";

            //Attach file using FileUpload Control and put the file in memory stream

            mail.IsBodyHtml = true;
            mail.Priority = System.Net.Mail.MailPriority.High;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(Session["EmailID"].ToString(),
                Session["AppPass"].ToString());
            SmtpServer.EnableSsl = true;


            try
            {
                SmtpServer.Send(mail);
            }
            catch (System.Net.Mail.SmtpException ex)
            {

            }
            catch (Exception exe)
            {

            }


            System.Threading.Thread.Sleep(100);
        }



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
        Response.Redirect("SkillWill_AssesmentApprovalList.aspx");
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