using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.HealthCare_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;

public partial class HealthCare_UI_ExpenseFormMDApproval : System.Web.UI.Page
{


    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private ReimbursmentFormDal formDal = new ReimbursmentFormDal();
    private HRPanelDal aPanelDal = new HRPanelDal();

    private CommitteeApprovalPanelDal approvalPanelDal = new CommitteeApprovalPanelDal();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {
                id_mastetID.Value = Request.QueryString["MID"].ToString();
                //id_mastetID.Value = (Request.QueryString["MID"]);
                onRecord(Convert.ToInt32(Request.QueryString["MID"]));
            }
        }
    }

    protected void AddNewButton_OnClick(object sender, EventArgs e)
    {

        try
        {
            Session["HRReturn"] = "";
        }
        catch (Exception)
        {
            
          //  throw;
        }

        try
        {
            if (Session["HRReturn"].ToString() == "Return")
        {
            Response.Redirect("HRPanel.aspx");
        }

        if (Session["MDReturn"].ToString() == "MDReturn")
        {
            Response.Redirect("SpecialApproval.aspx");
        }
        }
        catch (Exception)
        {

            //  throw;
        }

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
                FinancialYear.Text = dtMaster.Rows[0]["FinancialYearDesc"].ToString();

                lblAplicationDate.Text = dtMaster.Rows[0]["SubmitDate"].ToString();
                lblAplicationType.Text = dtMaster.Rows[0]["Type"].ToString();
                HFActionStatus.Value = dtMaster.Rows[0]["ActionStatus"].ToString();
                HFApplicationType.Value = dtMaster.Rows[0]["Type"].ToString();
                hfCompanyId.Value = dtMaster.Rows[0]["CompanyId"].ToString();

                DataTable dtEmp = formDal.GetEmployeeDetails(Convert.ToInt32(EmpppId));
                if (dtEmp.Rows.Count > 0)
                {
                    lblEmployeeName.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();
                    hfEmpID.Value = dtEmp.Rows[0]["EmpInfoId"].ToString().Trim();
                    lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();
                    deptNameLabel.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();
                    desigNameLabel.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();
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

        try
        {
            if (Session["HRReturn"].ToString() != "")
            {
                if (Session["HRReturn"].ToString() == "Return")
                {
                    if (actionRadioButtonList.SelectedValue == "Approved")
                    {
                        AlertMessageBoxShow(" You can not Select Approved");
                        return false;
                    }
                }
            }
        }
        catch (Exception )
        {
            
        }

        


        //if (actionRadioButtonList.SelectedValue == "Review")
        //{
        //    if (txtcomments.Text.Trim() == "")
        //    {
        //        AlertMessageBoxShow("Please Input Return comment");
        //        txtcomments.Focus();
        //        return false;
        //    }
        //}

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
        if (Validation())
        {
            string status = actionRadioButtonList.SelectedValue.ToString();
            string FormMasterId = id_mastetID.Value;
            bool ApprovalStatus = false;

            try
            { 
                    if (Session["HRReturn"] != null)
                    {
                        if (Session["HRReturn"].ToString() == "Return")
                        {
                            ApprovalStatus = formDal.ExpenseReimbursementForm_Retun(FormMasterId, status);

                            if (ApprovalStatus)
                            {
                                if (status == "Review")
                                {
                                    ReimbursementSelfAppLogDAO appLogDao = new ReimbursementSelfAppLogDAO()
                                    {
                                        ActionStatus = "Review",
                                        ApproveDate = DateTime.Now,
                                        ApproveBy = Session["UserId"].ToString(),
                                        PreEmpInfoId = Convert.ToInt32(Request.QueryString["PreeEmpId"]),
                                        ForEmpInfoId = Convert.ToInt32(hfEmpID.Value),
                                        ReimbursFromMasterId = int.Parse(FormMasterId),
                                        Comments = txtcomments.Text,
                                        CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                                    };

                                    formDal.UpdateAppLog("Review", Request.QueryString["LogID"]);
                                    int id = formDal.SaveEmpAppLog(appLogDao);

                                    bool Stutus = approvalPanelDal.Returnfrom_FROmHR(FormMasterId);

                                    Session["HRReturn"] = "";

                                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                                            "alert",
                                            "alert('Operation Successfull Done...');window.location ='../HealthCare_UI/HRPanel.aspx';",
                                            true);
                                }
                            }

                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                      "alert",
                                      "alert('Operation Successfull Done...');window.location ='../HealthCare_UI/HRPanel.aspx';",
                                      true);

                        }
                    }

                    if (Session["MDReturn"] != null)
                    {
                        if (Session["MDReturn"].ToString() == "MDReturn")
                        {
                            ApprovalStatus = aPanelDal.MDApprovalPermission(FormMasterId, status,"");
                            if (ApprovalStatus)
                            {
                                if (status == "Review")
                                {
                                    //Akhane main table a status update hobe 
                                    ApprovalStatus = formDal.ExpenseReimbursementFormAppoval(FormMasterId, status);

                                    if (ApprovalStatus)
                                    {
                                        ReimbursementSelfAppLogDAO appLogDao = new ReimbursementSelfAppLogDAO()
                                        {
                                            ActionStatus = "Review",
                                            ApproveDate = DateTime.Now,
                                            ApproveBy = Session["UserId"].ToString(),
                                            PreEmpInfoId = Convert.ToInt32(Request.QueryString["ForEmpId"]),
                                            ForEmpInfoId = Convert.ToInt32(Session["EmpInfoId"]),
                                            ReimbursFromMasterId = int.Parse(FormMasterId),
                                            Comments = txtcomments.Text,
                                            CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                                        };

                                        int idd = formDal.SaveEmpAppLog(appLogDao);

                                        ReimbursementSelfAppLogDAO appLogDao1 = new ReimbursementSelfAppLogDAO()
                                        {
                                            ActionStatus = "Verified",
                                            ApproveDate = DateTime.Now,
                                            ApproveBy = Session["UserId"].ToString(),
                                            PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"]),
                                            ForEmpInfoId = Convert.ToInt32(hfEmpID.Value),
                                            ReimbursFromMasterId = int.Parse(FormMasterId),
                                            Comments = txtcomments.Text,
                                            CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                                        };

                                        int id = formDal.SaveEmpAppLog(appLogDao1);
                                    }

                                    Session["MDReturn"] = "";


                                  
                                }

                            }
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                          "alert",
                                          "alert('Operation Successfull Done...');window.location ='SpecialApproval.aspx';",
                                          true);
                        }
                    }
            }
            catch (Exception )
            {
                    
            }


            
        }

    }

    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }


    

   
}