using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.HealthCare_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;

public partial class HealthCare_UI_ExpenseFormApproval : System.Web.UI.Page
{


    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private ReimbursmentFormDal formDal = new ReimbursmentFormDal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            RadioTextValue();
            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {
                id_mastetID.Value = Request.QueryString["MID"].ToString();
                //id_mastetID.Value = (Request.QueryString["MID"]);
                onRecord(Convert.ToInt32(Request.QueryString["MID"]));

                Remainingbalance();
            }
        }
    }

    protected void AddNewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ExpenseReimbursementFormApproval.aspx");
    }

    protected void onRecord(Int32 id)
    {
        try
        {
            DataTable dtMaster = formDal.Get_ReimbusrmentFormById(id);
        if (dtMaster.Rows.Count > 0)
        {
            string EmpppId = dtMaster.Rows[0]["EmpInfoId"].ToString();
            Company.Text = dtMaster.Rows[0]["ShortName"].ToString();
            lblAplicationDate.Text = dtMaster.Rows[0]["SubmitDate"].ToString();
            lblAplicationType.Text = dtMaster.Rows[0]["Type"].ToString();
            FinancialYear.Text = dtMaster.Rows[0]["FinancialYearDesc"].ToString();
            HFActionStatus.Value = dtMaster.Rows[0]["ActionStatus"].ToString();
            HFApplicationType.Value = dtMaster.Rows[0]["Type"].ToString();
            hfCompanyId.Value = dtMaster.Rows[0]["CompanyId"].ToString();
                hfCompanyIdNew.Value = dtMaster.Rows[0]["CompanyIdNew"].ToString();
            hfFinancialYearId.Value = dtMaster.Rows[0]["FinancialYearId"].ToString(); 
               
            DataTable dtEmp = formDal.GetEmployeeDetails(Convert.ToInt32(EmpppId));
                if (dtEmp.Rows.Count > 0)
                {
                    lblEmployeeName.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();
                    hfEmpID.Value = dtEmp.Rows[0]["EmpInfoId"].ToString().Trim();
                    lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();
                    deptNameLabel.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();
                    desigNameLabel.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();
                    lblSection.Text = dtEmp.Rows[0]["SectionName"].ToString().Trim();
                    try
                    {
                        OfficailMobile.Text = dtEmp.Rows[0]["OfficialMobile"].ToString().Trim();
                    }
                    catch (Exception)
                    {
                        //throw;
                    }

                    LocationLabel.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
                    lblPlace.Text = dtEmp.Rows[0]["Location"].ToString();
                    HFOfficeId.Value = dtEmp.Rows[0]["SalaryLoationId"].ToString();
                    ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();
                }

                NameofPatient.Text = dtMaster.Rows[0]["PatientName"].ToString();
                Age.Text = dtMaster.Rows[0]["PatientAge"].ToString();
                Relationship.Text = dtMaster.Rows[0]["Relationship"].ToString();
                lbl_IllnessDescription.Text = dtMaster.Rows[0]["SelfDate"].ToString();

                string hospitalEmailDoc = dtMaster.Columns.Contains("HospitalNotificationEmailDoc")
                    ? dtMaster.Rows[0]["HospitalNotificationEmailDoc"].ToString().Trim()
                    : string.Empty;
                string hospitalEmailFileName = dtMaster.Columns.Contains("HospitalNotificationEmailFileName")
                    ? dtMaster.Rows[0]["HospitalNotificationEmailFileName"].ToString().Trim()
                    : string.Empty;

                if (!string.IsNullOrWhiteSpace(hospitalEmailDoc))
                {
                    trHospitalNotificationEmail.Visible = true;
                    lnkHospitalNotificationEmail.Visible = true;
                    string previewUrl = (hospitalEmailDoc.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || hospitalEmailDoc.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                        ? hospitalEmailDoc
                        : "../UploadHealthCareDoc/" + hospitalEmailDoc;
                    lnkHospitalNotificationEmail.NavigateUrl = previewUrl;
                    lblHospitalNotificationEmailName.Text = !string.IsNullOrWhiteSpace(hospitalEmailFileName) ? hospitalEmailFileName : "Hospital_Notification_Email.pdf";
                    lblNoHospitalNotificationEmail.Visible = false;
                }
                else
                {
                    trHospitalNotificationEmail.Visible = true;
                    lnkHospitalNotificationEmail.Visible = false;
                    lblHospitalNotificationEmailName.Text = string.Empty;
                    lblNoHospitalNotificationEmail.Visible = true;
                    lblNoHospitalNotificationEmail.Text = "Not Attached";
                }

            // Bief Description 

            DataTable dtDes = formDal.Get_DescriptionById(id);

            if (dtDes.Rows.Count > 0)
            {
               
                loadGridView.DataSource = null;
                loadGridView.DataBind();
                loadGridView.DataSource = dtDes;
                loadGridView.DataBind();


                for (int i = 0; i < loadGridView.Rows.Count; i++)
                {

                    
                    string value = loadGridView.DataKeys[i].Values["YesNo"].ToString();

                    CheckBox yesChkBox = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("Yes");
                    CheckBox noChkBox = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("No");
                

                    if (value != "")
                    {
                        if (value == "True")
                        {
                            yesChkBox.Checked = true;
                            noChkBox.Checked = false;
                        }

                        if (value == "False")
                        {
                            yesChkBox.Checked = false;
                            noChkBox.Checked = true;
                        }

                    }

                }

            }
        }


            //TickMark

            DataTable dtTickMark = formDal.Get_TickMarkById(id);

            if (dtTickMark.Rows.Count > 0)
            {
                GridView1.DataSource = dtTickMark;
                GridView1.DataBind();
            }

            //ClaimDetails

            DataTable dtClaim = formDal.Get_ClaimDetailsById(id);

            if (dtClaim.Rows.Count > 0)
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
                GridView2.DataSource = dtClaim;
                GridView2.DataBind();

                decimal markTotal = 0;
                GettotalMark(markTotal);
            }

            //Employee List Load

            DataTable EmpDt = formDal.Get_EmpListById(id);



            if (EmpDt.Rows.Count > 0)
            {
                ViewState["gv_Member_List"] = EmpDt;
                gv_Member.DataSource = EmpDt;
                gv_Member.DataBind();



                for (int i = 0; i < gv_Member.Rows.Count; i++)
                {
                    
                    HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[i].FindControl("MemEmpInfoId"));


                    Label txt_Designation = ((Label)gv_Member.Rows[i].FindControl("txt_Designation"));

                    Label txt_EmpMasterCode = ((Label)gv_Member.Rows[i].FindControl("txt_EmpMasterCode"));

                    Label txt_EmpName = ((Label)gv_Member.Rows[i].FindControl("txt_EmpName"));

                    if (MemEmpInfoId.Value != "")
                    {
                        int mid = Convert.ToInt32(MemEmpInfoId.Value);
                        using (var db = new HRIS_SMCEntities())
                        {
                            var emp = (from j in db.tblEmpGeneralInfoes

                                       where j.EmpInfoId == mid
                                       select j).FirstOrDefault();

                            txt_EmpMasterCode.Text = emp.EmpMasterCode;
                            txt_EmpName.Text = emp.EmpName;

                            MemEmpInfoId.Value = emp.EmpInfoId.ToString();
                            using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(mid))
                            {
                                txt_Designation.Text = dtdesignation.Rows[0]["Designation"].ToString();

                            }
                        }

                    }

                }
            }

            //Document
            DataTable dtDoc = formDal.Get_FormDocumentById(id);

            if (dtDoc.Rows.Count > 0)
            {
                ViewState["DocGrid_List"] = dtDoc;
                gv_DocumentUpload.DataSource = dtDoc;
                gv_DocumentUpload.DataBind();
            }
        }
        catch (Exception)
        {

            //throw;
        }
        }

    private void GettotalMark(decimal markTotal)
    {
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            TextBox Amount = (TextBox)GridView2.Rows[i].FindControl("Amount");


            if (Amount.Text == "")
            {
                markTotal = markTotal + 0;
            }
            else
            {
                markTotal = markTotal + Convert.ToDecimal(Amount.Text.ToString());
            }
        }

        Label tst2 = (Label)GridView2.FooterRow.FindControl("lblTotalMark");
        tst2.Text = markTotal.ToString();
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

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    private bool Validation()
    {
        int count = 0;
        int row = 0;

        if (actionRadioButtonList.SelectedIndex == -1)
        {
            AlertMessageBoxShow("Please Select Approval Action");
            return false;
        }


        if (actionRadioButtonList.SelectedValue == "Review")
        {
            if (txtcomments.Text.Trim() == "")
            {
                AlertMessageBoxShow("Please Input Return comment");
                txtcomments.Focus();
                return false;
            }
        }

        //for (int i = 0; i < gv_ViewList.Rows.Count; i++)
        //{
        //    row++;
        //    CheckBox check = (CheckBox)gv_ViewList.Rows[i].FindControl("Checked");
        //    if (!check.Checked)
        //    {
        //        count++;
        //    }
        //}
        //if (row == count)
        //{
        //    AlertMessageBoxShow("Please! Select minimum One");
        //    return false;
        //}

        return true;
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {

        actionRadioButtonList.Items[0].Selected = true;
        SubmitApprove();
    }


    public void Remainingbalance()
    {
        try
        {
            string balanceType = "";

            string type = "";

            if (HFApplicationType.Value == "IPD")
            {
                balanceType = "IPD";
                type = "IPD";
            }
            else if (HFApplicationType.Value == "OPD")
            {
                balanceType = "OPD";
                type = "OPD";
            }

            if (hfCompanyId.Value != "" && hfFinancialYearId.Value != "" && hfEmpID.Value != "" && balanceType != "")
            {

                DataTable dt = formDal.Get_RemainningBalance(hfEmpID.Value, hfCompanyId.Value, hfFinancialYearId.Value, balanceType, type);

                if (dt.Rows.Count > 0)
                {

                    RemainingBalance.Text = string.Empty;
                    txtTotalBalance.Text = string.Empty;
                    txtAvailedAmount.Text = string.Empty;
                    RemainingBalance.Text = dt.Rows[0]["RMBalance"].ToString();
                    txtTotalBalance.Text = dt.Rows[0]["TotalBalance"].ToString();
                    txtAvailedAmount.Text = dt.Rows[0]["TotalAvailedAmount"].ToString();
                }

            }
            else
            {
                RemainingBalance.Text = string.Empty;
                txtTotalBalance.Text = string.Empty;
                txtAvailedAmount.Text = string.Empty;
                // ShowMessage();
            }


            if (HFApplicationType.Value == "Special")
            {
                RemainingBalance.Text = "0";
                txtTotalBalance.Text = "0";
                txtAvailedAmount.Text = "0";

            }

        }
        catch (Exception)
        {
            RemainingBalance.Text = "0";
            txtTotalBalance.Text = "0";
            txtAvailedAmount.Text = "0";
            //throw;
        }
    }
    public static bool SenMailForApprved(int forEmpID, string mSubject, string mBody)
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
            try
            {
                // Set TLS 1.2 (Office 365 requires this)
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;

                    // Use your actual Office 365 credentials
                    smtpClient.Credentials = new NetworkCredential("ss----no-reply@smc-bd.org", "vfwzmbxprdmqhhln");

                    // Set timeout (in milliseconds)
                    smtpClient.Timeout = 20000;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("ss----no-reply@smc-bd.org");
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(ForMailAddress);
                        mailMessage.Subject = mSubject;
                        mailMessage.Body =
                   "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +
                   WebUtility.HtmlDecode(mBody)
                   +
                   "</div>";
                        mailMessage.IsBodyHtml = true;

                        smtpClient.Send(mailMessage);

                    }
                }
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null)
                {

                }
            }





            System.Threading.Thread.Sleep(100);
        }


        return true;
    }

    private void SubmitApprove()
    {
        if (Validation())
        {
            string status = actionRadioButtonList.SelectedValue.ToString();

            string FormMasterId = id_mastetID.Value;

            //if (status == "Approved")
            //{
            //    status = "Verified";
            //}


            bool ApprovalStatus = formDal.ExpenseReimbursementFormAppoval(FormMasterId, status);


            if (ApprovalStatus)
            {
                if (status == "Verified")
                {
                    try
                    {
                        DataTable dtempdata = formDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                        ReimbursementSelfAppLogDAO appLogDao = new ReimbursementSelfAppLogDAO();

                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                        appLogDao.ReimbursFromMasterId = int.Parse(FormMasterId);
                        //   Comments = commentsTextBox.Text,
                        appLogDao.CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.HRPanel = false;

                        SenMailForApprved(Convert.ToInt32(appLogDao.ForEmpInfoId), "Employee Health Care", @"  <br/> Dear Sir, <br/>
Employee ID: " + lblEmpId.Text + @" <br/>
Employee Name: " + lblEmployeeName.Text + @"<br/>
Department:" + deptNameLabel.Text + @"<br/> <br/> 
The healthcare request for employee ID " + lblEmpId.Text + @" is waiting for your approval.<br/><br/>
Please log in for more details using the link below:<br/>
http://182.160.103.234:8088/<br/><br/>
Thank you.");

                        int id = formDal.SaveEmpAppLog(appLogDao);

                    }
                    catch
                    {
                        ShowMessageBox("Your supervisor or final approver is not set yet");
                    }
              

                }
                else if (status == "Approved")
                {

                    try
                    {
                        bool acStatus = formDal.ExpenseReimbursementFormAppoval_HeadofDpt(FormMasterId, "Verified");

                    if (acStatus)
                    {
                        DataTable Dt = formDal.Get_Nominated_HR(HFApplicationType.Value, HFOfficeId.Value, hfCompanyIdNew.Value);

                        if (Dt.Rows.Count > 0)
                        {
                            ReimbursementSelfAppLogDAO appLogDao = new ReimbursementSelfAppLogDAO()
                            {
                                ActionStatus = "Verified",
                                ApproveDate = DateTime.Now,
                                ApproveBy = Session["UserId"].ToString(),
                                PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                                ForEmpInfoId = Convert.ToInt32(Dt.Rows[0]["EmpInfoId"].ToString()),
                                ReimbursFromMasterId = int.Parse(FormMasterId),
                                Comments = txtcomments.Text,
                                CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                                HRPanel = true,
                            };
                            int id = formDal.SaveEmpAppLogForHR(appLogDao);


                                SenMailForApprved(Convert.ToInt32(appLogDao.ForEmpInfoId), "Employee Health Care", @"  <br/> Dear Sir, <br/>
Employee ID: " + lblEmpId.Text + @" <br/>
Employee Name: " + lblEmployeeName.Text + @"<br/>
Department:" + deptNameLabel.Text + @"<br/> <br/> 
The healthcare request for employee ID " + lblEmpId.Text + @" is waiting for your approval.<br/><br/>
Please log in for more details using the link below:<br/>
http://182.160.103.234:8088/<br/><br/>
Thank you.");
                            }
                        else
                        {
                            bool Status = formDal.ExpenseReimbursementFormAppoval(FormMasterId, HFActionStatus.Value);
                            if (Status)
                            {
                                ShowMessageBox("Please Add Nominated HR For This Type Of Application");
                            }
                        }
                    }
                        else
                        {
                            ShowMessageBox("Please Add Nominated HR For This Type Of Application");
                        }

                    }
                    catch
                    {
                        ShowMessageBox("Your supervisor or final approver is not set yet");
                    }
                }

                else if (status == "Review")
                {
                    DataTable dtempdata = formDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), id_mastetID.Value);
                    DataTable dtempdata2 = formDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(),
                        id_mastetID.Value);

                    if (dtempdata2.Rows.Count > 0)
                    {
                        ReimbursementSelfAppLogDAO appLogDao = new ReimbursementSelfAppLogDAO()
                        {
                            ActionStatus = "Review",
                            ApproveDate = DateTime.Now,
                            ApproveBy = Session["UserId"].ToString(),
                            PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString()),
                            ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString()),
                            ReimbursFromMasterId = int.Parse(FormMasterId),
                            Comments = txtcomments.Text,
                            CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                            HRPanel = false,
                        };
                        formDal.UpdateAppLog("Review", Session["AppLogId"].ToString());
                        int id = formDal.SaveEmpAppLog(appLogDao);
                        SenMailForApprved(Convert.ToInt32(appLogDao.ForEmpInfoId), "Employee Health Care", @"  <br/> Dear Sir, <br/>
Employee ID: " + lblEmpId.Text + @" <br/>
Employee Name: " + lblEmployeeName.Text + @"<br/>
Department:" + deptNameLabel.Text + @"<br/> <br/> 
The healthcare request for employee ID " + lblEmpId.Text + @" is waiting for your approval.<br/><br/>
Please log in for more details using the link below:<br/>
http://182.160.103.234:8088/<br/><br/>
Thank you.");
                        ApprovalStatus = formDal.ExpenseReimbursementForm_Retun(FormMasterId, status);
                    }
                    else
                    {
                        ShowMessageBox("Please select Approval Status to Approved !!!");
                    }
                }

                Session["AppLogId"] = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfull Done...');window.location ='ExpenseReimbursementFormApproval.aspx';",
                    true);
            }
        }
    }

    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
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

        DataTable dtdata = formDal.GetSupervisorEmployeeAppId(Session["EmpInfoId"].ToString(), Request.QueryString["ForEmpId"]);

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

    //protected void btn_Save_OnClick(object sender, EventArgs e)
    //{

    //    if (actionRadioButtonList.SelectedValue != "")
    //    {
    //        ReimbursementSelfAppLogDAO aMasterApp = new ReimbursementSelfAppLogDAO();

    //        aMasterApp.ReimbursFromMasterId
    //            = Convert.ToInt32(id_mastetID.Value);




    //        aMasterApp.ActionStatus = actionRadioButtonList.SelectedValue;
    //        bool status = formDal.UpdateContractural(aMasterApp);

    //        if (status)
    //        {
                
    //            if (aMasterApp.ActionStatus == "Verified")
    //            {
    //                DataTable dtempdata = formDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
    //                ReimbursementSelfAppLogDAO appLogDao = new ReimbursementSelfAppLogDAO()
    //                {
    //                    ActionStatus = actionRadioButtonList.SelectedValue,
    //                    ApproveDate = DateTime.Now,
    //                    ApproveBy = Session["UserId"].ToString(),
    //                    PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
    //                    ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString()),
    //                    ReimbursFromMasterId = aMasterApp.ReimbursFromMasterId,
    //                 //   Comments = commentsTextBox.Text,
    //                    CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

    //                };
    //                int id = formDal.SaveEmpAppLog(appLogDao);
    //            }
    //            else if (aMasterApp.ActionStatus == "Approved")
    //            {

    //                //DataTable dtaa = formDal.GetCheckApprisalAlreadyExist(Convert.ToInt32(id_mastetID.Value));
    //                //if (dtaa.Rows.Count > 0)
    //                //{
    //                //    int AppraisalMasterId = Convert.ToInt32(dtaa.Rows[0]["AppraisalMasterId"].ToString());

    //                //    formDal.DeleteAppraisalSetupNew(Convert.ToInt32(AppraisalMasterId));

    //                //}


    //                //DataTable dtempdata = aContractualEmpManageDAL.GetEmpInfo(" WHERE EmpInfoId='" + empInfoId.Value + "'");
    //                ReimbursementSelfAppLogDAO appLogDao = new ReimbursementSelfAppLogDAO()
    //                {
    //                    ActionStatus = actionRadioButtonList.SelectedValue,
    //                    ApproveDate = DateTime.Now,
    //                    ApproveBy = Session["UserId"].ToString(),
    //                    PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
    //                    ForEmpInfoId = 0,
    //                    ReimbursFromMasterId = aMasterApp.ReimbursFromMasterId,
    //                 //   Comments = commentsTextBox.Text,
    //                    CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

    //                };
    //                int id = formDal.SaveEmpAppLog(appLogDao);
                   

    //            }
                


    //        }
    //        Session["AppLogId"] = null;
    //        ScriptManager.RegisterStartupScript(this, this.GetType(),
    //                   "alert",
    //                   "alert('Operation Successful...');window.location ='AppraisalSupApprove.aspx';",
    //                   true);
          
    //    }
    //    else
    //    {
    //      //  ShowMessageBox("Please Select Approval Status!");
    //    }


    //}

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        actionRadioButtonList.Items[1].Selected = true;
        SubmitApprove();
    }
}