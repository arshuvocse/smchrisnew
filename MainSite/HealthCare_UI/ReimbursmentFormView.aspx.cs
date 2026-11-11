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

public partial class HealthCare_UI_ReimbursmentFormView : System.Web.UI.Page
{

    private CommitteeApprovalPanelDal approvalPanelDal = new CommitteeApprovalPanelDal();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private ReimbursmentFormDal formDal = new ReimbursmentFormDal();

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
        Response.Redirect("CommitteeFeedbackPanel.aspx");
    }

    protected void onRecord(Int32 id)
    {

        DataTable dtMaster = formDal.Get_ReimbusrmentFormById(id);
        if (dtMaster.Rows.Count > 0)
        {

            DataTable Dt = approvalPanelDal.Get_Latest_CommitteeFeedBack(id_mastetID.Value);
            if (Dt.Rows.Count > 0)
            {
                txtFeedback.Text = Dt.Rows[0]["Feedback"].ToString();
            }

            string EmpppId = dtMaster.Rows[0]["EmpInfoId"].ToString();
            Company.Text = dtMaster.Rows[0]["ShortName"].ToString();
            FinancialYear.Text = dtMaster.Rows[0]["FinancialYearDesc"].ToString();

            DataTable dtEmp = formDal.GetEmployeeDetails(Convert.ToInt32(EmpppId));
            if (dtEmp.Rows.Count > 0)
            {

                lblAplicationDate.Text = dtMaster.Rows[0]["SubmitDate"].ToString();
                lblAplicationType.Text = dtMaster.Rows[0]["Type"].ToString();
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


                    bool yesNovalue =false;  
                    try
                    {
                          yesNovalue = Convert.ToBoolean(loadGridView.DataKeys[i][0]);
                    }catch(Exception ex)
                    {

                    }

                    bool datevalue = false;


                    try
                    {
                        datevalue = Convert.ToBoolean(loadGridView.DataKeys[i][1]);
                    }
                    catch (Exception ex)
                    {

                    }


                    string value = loadGridView.DataKeys[i].Values["YesNo"].ToString();

                    CheckBox yesChkBox = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("Yes");
                    CheckBox noChkBox = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("No");
                    TextBox dateTextBox = (TextBox)loadGridView.Rows[i].Cells[0].FindControl("DesDate");


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

                    if (yesNovalue)
                    {
                        yesChkBox.Visible = true;
                        noChkBox.Visible = true;
                    }
                    else
                    {
                        yesChkBox.Visible = false;
                        noChkBox.Visible = false;
                    }

                    if (datevalue)
                    {
                        dateTextBox.Visible = true;
                        
                    }
                    else
                    {
                        dateTextBox.Visible = false;
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


    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {


            string status = "Approved";

            //string FormMasterId = "";
            //for (int i = 0; i < gv_ViewList.Rows.Count; i++)
            //{
            //    CheckBox check = (CheckBox)gv_ViewList.Rows[i].FindControl("Checked");

            //    if (check.Checked)
            //    {
            //        int EmpId = Convert.ToInt32(gv_ViewList.DataKeys[i][0]);

            //        FormMasterId += EmpId + ",";

            //    }
            //}

            bool dStatus = false;

            if (actionRadioButtonList.Items[0].Selected)
            {
                dStatus = true;
            }

            string FormMasterId = id_mastetID.Value;

            bool ApprovalStatus = formDal.ExpenseReimbursementFormAppoval_DoctorStatus(FormMasterId, status, dStatus);

            if (ApprovalStatus)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfull Done...');window.location ='ExpenseReimbursementFormApproval.aspx';",
                    true);
            }
        }

    }


    private bool Validation()
    {
        if (txtFeedback.Text.Trim()=="")
        {
            AlertMessageBoxShow("Please give your valuable feedback !!");
            txtFeedback.Focus();
            return false;
        }

        return true;
    }
    


    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (Session["EmpInfoId"] != null)
            {
                Submit();
            }
        }
    }


    private void Submit()
    {
        if (Validation())
        {


            bool dStatus = false;

            if (actionRadioButtonList.Items[0].Selected)
            {
                dStatus = true;
            }
            int PK = approvalPanelDal.SaveCommitteeFeedbackInfo(txtFeedback.Text, Session["EmpInfoId"].ToString(), id_mastetID.Value, dStatus);

            if (PK > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfully Done...');window.location ='CommitteeFeedbackPanel.aspx';",
                    true);
            }
        }
    }


    protected void btnReturn_OnClick(object sender, EventArgs e)
    {
        if (Comments.Text.Trim() == "")
        {
            AlertMessageBoxShow("Please give you comments");
            Comments.Focus();
        }
        else
        {

            string status = "Review";

            string FormMasterId = id_mastetID.Value;

            bool ApprovalStatus = formDal.ExpenseReimbursementFormAppoval(FormMasterId, status);

            if (ApprovalStatus)
            {
                ReimbursementSelfAppLogDAO appLogDao = new ReimbursementSelfAppLogDAO()
                {
                    ActionStatus = "Review",
                    ApproveDate = DateTime.Now,
                    ApproveBy = Session["UserId"].ToString(),
                    PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"]),
                    ForEmpInfoId = Convert.ToInt32(Session["EmpInfoId"]),
                    ReimbursFromMasterId = int.Parse(FormMasterId),
                    Comments = Comments.Text,
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
                    Comments = Comments.Text,
                    CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                };
 
                int id = formDal.SaveEmpAppLog(appLogDao1);
            }


            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successfully Done...');window.location ='CommitteeFeedbackPanel.aspx';",
                true);
            
        }

    }
}