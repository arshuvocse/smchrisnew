using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.HealthCare_DAL;
using DAO.HRIS_DAO_EF;

public partial class HealthCare_UI_SapIntegretionView : System.Web.UI.Page
{
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


                Remainingbalance();
            }
        }
    }

    protected void AddNewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("SAPIntegrationPoint.aspx");
    }

    protected void onRecord(Int32 id)
    {
        DataTable dtMaster = formDal.Get_ReimbusrmentFormById(id);
        if (dtMaster.Rows.Count > 0)
        {
            string EmpppId = dtMaster.Rows[0]["EmpInfoId"].ToString();
            Company.Text = dtMaster.Rows[0]["ShortName"].ToString();
            FinancialYear.Text = dtMaster.Rows[0]["FinancialYearDesc"].ToString();




            HFActionStatus.Value = dtMaster.Rows[0]["ActionStatus"].ToString();
            HFApplicationType.Value = dtMaster.Rows[0]["Type"].ToString();
            hfCompanyId.Value = dtMaster.Rows[0]["CompanyId"].ToString();
            hfFinancialYearId.Value = dtMaster.Rows[0]["FinancialYearId"].ToString();




            lblBankname.Text = dtMaster.Rows[0]["BankName"].ToString();
            lblAccount.Text = dtMaster.Rows[0]["BankAccountNo"].ToString();
            lblBranch.Text = dtMaster.Rows[0]["BranchName"].ToString();
            lblRoutingNo.Text = dtMaster.Rows[0]["RoutingNo"].ToString();




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
                ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();
            }

            NameofPatient.Text = dtMaster.Rows[0]["PatientName"].ToString();
            Age.Text = dtMaster.Rows[0]["PatientAge"].ToString();
            Relationship.Text = dtMaster.Rows[0]["Relationship"].ToString();

            lbl_IllnessDescription.Text = dtMaster.Rows[0]["SelfDate"].ToString();

            // Bief Description 

            DataTable dtDes = formDal.Get_DescriptionById(id);

            if (dtDes.Rows.Count > 0)
            {

                loadGridView.DataSource = null;
                loadGridView.DataBind();
                loadGridView.DataSource = dtDes;
                loadGridView.DataBind();


                //for (int i = 0; i < loadGridView.Rows.Count; i++)
                //{
                //    string value = loadGridView.DataKeys[i].Values["YesNo"].ToString();


                //    CheckBox yesChkBox = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("Yes");
                //    CheckBox noChkBox = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("No");


                //    if (value != "")
                //    {
                //        if (value == "True")
                //        {
                //            yesChkBox.Checked = true;
                //            noChkBox.Checked = false;
                //        }

                //        if (value == "False")
                //        {
                //            yesChkBox.Checked = false;
                //            noChkBox.Checked = true;
                //        }

                //    }



                //}



                for (int i = 0; i < loadGridView.Rows.Count; i++)
                {

                    bool yesNovalue = Convert.ToBoolean(loadGridView.DataKeys[i][0]);
                    bool datevalue = Convert.ToBoolean(loadGridView.DataKeys[i][1]);


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
                        //dateTextBox.Text = string.Empty;
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

            string FormMasterId = id_mastetID.Value;

            bool ApprovalStatus = formDal.ExpenseReimbursementFormAppoval(FormMasterId, status);

            if (ApprovalStatus)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfull Done...');window.location ='ExpenseReimbursementFormApproval.aspx';",
                    true);
            }
        }

    }
    
}