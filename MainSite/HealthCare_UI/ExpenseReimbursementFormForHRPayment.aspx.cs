using System.Activities.Statements;
using System.Data.Entity.Design;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using DAL.COMMON_DAL;
using DAL.HealthCare_DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAO.HealthCare_Dao;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using System.Globalization;

public partial class HealthCare_UI_ExpenseReimbursementFormForHRPayment : System.Web.UI.Page
{


    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private ReimbursmentFormDal formDal = new ReimbursmentFormDal();


    public decimal amount=0;  

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                LoadInitialGrid();
                Option();

                SubmitDate.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");


                NameofPatient.Attributes.Add("readonly", "readonly");
                Relationship.Attributes.Add("readonly", "readonly");
                Age.Attributes.Add("readonly", "readonly");

                if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
                {
                    hfMasterId.Value = Request.QueryString["MID"].ToString();
                    //   LoadInitialDDL();    
                    // id_mastetID.Value = (Request.QueryString["MID"]);

                    string hrMode = "";

                    try
                    {
                        hrMode = Request.QueryString["Mode"].ToString();

                        hfMode.Value = Request.QueryString["Mode"].ToString();
                         
                    }
                    catch (Exception)
                    {
                        
                        //throw;
                    }
                    BindHospitalNameDropdown();

                    onRecord(Convert.ToInt32(Request.QueryString["MID"]), hrMode);

                    inlineRadio.Enabled = false;
                }
                else
                {

                    LoadInitialDDL();
                    LoadInitialgv_Member_Save();

                    inlineRadio.SelectedValue = "IPD";
                    inlineRadio_OnSelectedIndexChanged(null, null);


                    //DataTable Dt = formDal.GetCheckEmpStatus(Session["EmpInfoId"].ToString());
                    //if (Dt.Rows.Count>0)
                    //{
                    //    LoadInitialDDL();

                    //    LoadInitialgv_Member_Save();
                    //}
                    //else
                    //{
                    //    aShowMessage.ShowMessageBox("You are not eligible for this application",this);
                    //}

                }

            }
            catch (Exception)
            {
                
                //throw;
            }

        }

        ApplyAttachmentCriteriaAvailability(false);
    }

    private void BindHospitalNameDropdown()
    {
        using (DataTable dt = _commonDataLoad.GetHospitalNameDDL())
        {
            ddlHospitalName.DataSource = dt;
            ddlHospitalName.DataValueField = "HospitalNameId";
            ddlHospitalName.DataTextField = "HospitalName";
            ddlHospitalName.DataBind();
            ddlHospitalName.Items.Insert(0, new ListItem("Please Select Hospital.....", String.Empty));
        }
    }

    

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../HealthCare_UI/HRPanel.aspx");
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }


    protected void onRecord(Int32 id, string hrMode)
    {      
        DataTable dtMaster = formDal.Get_ReimbusrmentFormById(id);
        if (dtMaster.Rows.Count > 0)
        {
           txtBankName.Text = dtMaster.Rows[0]["BankName"].ToString(); ;
           txtBankAccountNo.Text = dtMaster.Rows[0]["BankAccountNo"].ToString(); ;
             txtBranchName.Text = dtMaster.Rows[0]["BranchName"].ToString(); ;
             txtRoutingNo.Text = dtMaster.Rows[0]["RoutingNo"].ToString(); ;
            //if (Convert.ToBoolean(dtMaster.Rows[0]["IsOPD"].ToString()))
            //{
            //    inlineRadio2.Checked = true;
            //    inlineRadio1.Checked = false;
               

            //}
            //else
            //{
            //    inlineRadio1.Checked = true;
            //    inlineRadio2.Checked = false;
              
            //}


            //if (Convert.ToBoolean(dtMaster.Rows[0]["Special"].ToString()))
            //{
                
            //    inlineRadio1.Checked = false;
            //    inlineRadio2.Checked = false;

            //}
             if (hrMode == "HR")
            {
                drrft.Visible = false;
                LinkButton3.Visible = true;
            }
          

            HFReimbursmentId.Value = dtMaster.Rows[0]["ReimbursFromMasterId"].ToString();
            HFEntryBy.Value = dtMaster.Rows[0]["EntryBy"].ToString();
            inlineRadio.SelectedValue = dtMaster.Rows[0]["Type"].ToString();
            SelfDate.Text = dtMaster.Rows[0]["SelfDate"].ToString();
            Age.Text = dtMaster.Rows[0]["PatientAge"].ToString();
            using (DataTable dt = _commonDataLoad.GetCompanyDDL_Edit())
            {
                    ddlCompany.DataSource = dt;
                    ddlCompany.DataValueField = "Value";
                    ddlCompany.DataTextField = "TextField";
                    ddlCompany.DataBind();
                    ddlCompany.SelectedValue = dtMaster.Rows[0]["CompanyId"].ToString();

                    using (DataTable dtemp = _commonDataLoad.GetEmpDDLAActive(ddlCompany.SelectedValue.ToString()))
                    {

                        ddlForwordEmp.DataSource = dtemp;
                        ddlForwordEmp.DataValueField = "EmpInfoId";
                        ddlForwordEmp.DataTextField = "EmpName";
                        ddlForwordEmp.DataBind();
                        ddlForwordEmp.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                        try
                        {
                            ddlForwordEmp.SelectedValue = dtMaster.Rows[0]["EmpInfoId"].ToString();

                            hfEmpID.Value = dtMaster.Rows[0]["EmpInfoId"].ToString();
                        }
                        catch (Exception)
                        {
                            ddlForwordEmp.SelectedIndex = 0;
                            //throw;
                        }
                    }

                    using (DataTable dt2 = formDal.Get_FinancialById(ddlCompany.SelectedValue.ToString()))
                    {
                        ddlFinancialYear.DataSource = dt2;
                        ddlFinancialYear.DataValueField = "FinancialYearId";
                        ddlFinancialYear.DataTextField = "FinancialYearDesc";
                        ddlFinancialYear.DataBind();
                        ddlFinancialYear.Items.Insert(0, new ListItem("Please Select an Financial Year.....", String.Empty));
                    }

                    try
                    {
                        ddlFinancialYear.SelectedValue = dtMaster.Rows[0]["FinancialYearId"].ToString();
                        ddlFinancialYear.Enabled = false;
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                    load_IPD_OPD(0, ddlCompany.SelectedValue, hfEmpID.Value);
                    Remainingbalance();
                    //ddlCompany_OnSelectedIndexChanged(null, null);
            }
            GridView1.DataSource = null;
            GridView1.DataBind();

            gv_OPD.DataSource = null;
            gv_OPD.DataBind();
            if (dtMaster.Rows[0]["Type"].ToString() == "OPD" || dtMaster.Rows[0]["Type"].ToString() == "Special")
            {
                load_OPD(1, ddlCompany.SelectedValue);
                     
            }
            else
            {
                load_IPD_OPD(0, ddlCompany.SelectedValue, hfEmpID.Value);
            }
            
          using (DataTable dtemp = _commonDataLoad.GetEmpDDLAActive_edit())
          {

            //  ddlForwordEmp.DataSource = null;
            //  ddlForwordEmp.DataBind();

              ddlForwordEmp.DataSource = dtemp;
              ddlForwordEmp.DataValueField = "EmpInfoId";
              ddlForwordEmp.DataTextField = "EmpName";
              ddlForwordEmp.DataBind();
              ddlForwordEmp.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
              try
              {
                  ddlForwordEmp.SelectedValue = dtMaster.Rows[0]["EmpInfoId"].ToString();
                  ddlForwordEmp_OnSelectedIndexChanged(null, null);

              }
              catch (Exception)
              {
                  ddlForwordEmp.SelectedIndex = 0;
                  //throw;
              }
          }
            
          
       

            using (DataTable dtt = formDal.Get_FinancialByIForSelecttedValueEdit(ddlCompany.SelectedValue.ToString()))
            {
                ddlFinancialYear.DataSource = dtt;
                ddlFinancialYear.DataValueField = "FinancialYearId";
                ddlFinancialYear.DataTextField = "FinancialYearDesc";
                ddlFinancialYear.DataBind();
                ddlFinancialYear.Items.Insert(0, new ListItem("Please Select an Financial Year.....", String.Empty));
                ddlFinancialYear.SelectedValue = dtMaster.Rows[0]["FinancialYearId"].ToString();
                ddlFinancialYear.Enabled = false;
            }

            try
            {
                SubmitDate.Text = FormatHospitalDateForEdit(dtMaster.Rows[0]["SubmitDate"].ToString());
            }
            catch (Exception e)
            {
                
            }

            if (ddlForwordEmp.SelectedValue != "0")
            {

                DataTable dtEmp = formDal.GetEmployeeDetails(Convert.ToInt32(ddlForwordEmp.SelectedValue));
                if (dtEmp.Rows.Count > 0)
                {
                    lblEmployeeName.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();
                    hfEmpID.Value = dtEmp.Rows[0]["EmpInfoId"].ToString().Trim();
                    lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();
                    deptNameLabel.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();
                    desigNameLabel.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();
                    try
                    {
                        txtOfficailMobile.Text = dtEmp.Rows[0]["OfficialMobile"].ToString().Trim();
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

                //
                Remainingbalance();
            }



            // Bief Description 

            DataTable dtDes = formDal.Get_DescriptionById(id);

            if (dtDes.Rows.Count > 0)
            {
                // Descriptiondate
                loadGridView.DataSource = null;
                loadGridView.DataBind();
                loadGridView.DataSource = dtDes;
                loadGridView.DataBind();

                for (int i = 0; i < loadGridView.Rows.Count; i++)
                {

                    bool yesNovalue = Convert.ToBoolean(loadGridView.DataKeys[i][0]);
                    bool datevalue = Convert.ToBoolean(loadGridView.DataKeys[i][1]);


                    string value = loadGridView.DataKeys[i].Values["YesNo"].ToString();

                    CheckBox yesChkBox = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("Yes");
                    CheckBox noChkBox = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("No");
                    TextBox dateTextBox = (TextBox)loadGridView.Rows[i].Cells[0].FindControl("Date");


                 

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


                        if (dateTextBox.Text!="")
                        {
                            yesChkBox.Checked = true;
                            noChkBox.Checked = false;
                        }

                    }

                }


            }


            //TickMark

            DataTable dtTickMark = formDal.Get_TickMarkById(id);

            if (dtTickMark.Rows.Count > 0)
            {

                for (int i = 0; i < dtTickMark.Rows.Count; i++)
                {

                    if (Prescription.Text == dtTickMark.Rows[i]["EnclosuresTickMark"].ToString())
                    {
                        Prescription.Checked = true;
                    }

                    if (BillofConsaltation.Text == dtTickMark.Rows[i]["EnclosuresTickMark"].ToString())
                    {
                        BillofConsaltation.Checked = true;
                    }

                    if (BillofMedicine.Text == dtTickMark.Rows[i]["EnclosuresTickMark"].ToString())
                    {
                        BillofMedicine.Checked = true;
                    }

                    if (BillofHospitalization.Text == dtTickMark.Rows[i]["EnclosuresTickMark"].ToString())
                    {
                        BillofHospitalization.Checked = true;
                    }

                    if (SpecialDocumentUpload.Text == dtTickMark.Rows[i]["EnclosuresTickMark"].ToString())
                    {
                        SpecialDocumentUpload.Checked = true;
                    }

                    if (BillofInvestigation.Text == dtTickMark.Rows[i]["EnclosuresTickMark"].ToString())
                    {
                        BillofInvestigation.Checked = true;
                    }

                    if (PhotoCopyofInvestigation.Text == dtTickMark.Rows[i]["EnclosuresTickMark"].ToString())
                    {
                        PhotoCopyofInvestigation.Checked = true;
                    }

                    if (BillForCharges.Text == dtTickMark.Rows[i]["EnclosuresTickMark"].ToString())
                    {
                        BillForCharges.Checked = true;
                    }

                    if (Other.Text == dtTickMark.Rows[i]["EnclosuresTickMark"].ToString())
                    {
                        Other.Checked = true;
                    }

                }
            }


            //ClaimDetails
            DataTable dtClaim = formDal.Get_ClaimDetailsById(id);

            if (dtMaster.Rows[0]["Type"].ToString() == "OPD" || dtMaster.Rows[0]["Type"].ToString() == "Special")
            {


                if (dtClaim.Rows.Count > 0)
                {
                    for (int i = 0; i < gv_OPD.Rows.Count; i++)
                    {
                        HiddenField HeadId = (HiddenField) gv_OPD.Rows[i].FindControl("hfHeadOfExpenseId");
                        CheckBox check = (CheckBox)gv_OPD.Rows[i].FindControl("valueCheck");
                        TextBox Date = (TextBox)gv_OPD.Rows[i].FindControl("txtDate");
                        TextBox NumberOfdays = (TextBox)gv_OPD.Rows[i].FindControl("NumberOfdays");
                        TextBox Voucher = (TextBox)gv_OPD.Rows[i].FindControl("Voucher");
                        TextBox Amount = (TextBox)gv_OPD.Rows[i].FindControl("Amount");
                        TextBox txtRent = (TextBox)gv_OPD.Rows[i].FindControl("txtRent");
                        TextBox txt_RemaksforHR = (TextBox)gv_OPD.Rows[i].FindControl("txt_RemaksforHR");

                        for (int j = 0; j < dtClaim.Rows.Count; j++)
                        {
                            string Id = dtClaim.Rows[j]["OIPDHeadOfExpenseId"].ToString();

                            if (HeadId.Value == Id)
                            {
                                check.Checked = true;
                                Amount.Text = dtClaim.Rows[j]["Amount"].ToString();
                                NumberOfdays.Text = dtClaim.Rows[j]["NoOfDays"].ToString();
                                Voucher.Text = dtClaim.Rows[j]["SINoOfEncloseVoucher"].ToString();
                                txtRent.Text = dtClaim.Rows[j]["Rent"].ToString();
                                txt_RemaksforHR.Text = dtClaim.Rows[j]["RemaksforHR"].ToString();
                                if (dtClaim.Rows[j]["Dates"] != null)
                                {
                                    Date.Text = dtClaim.Rows[j]["Dates"].ToString();
                                }

                            }
                        }
                    }


                    //GridView1.DataSource = null;
                    //GridView1.DataBind();
                    //GridView1.DataSource = dtClaim;
                    //GridView1.DataBind();

                    decimal markTotal = 0;
                    GettotalMark_OPD(markTotal);


                }

            }

            else
            {
                if (dtClaim.Rows.Count > 0)
                {
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        HiddenField HeadId = (HiddenField)GridView1.Rows[i].FindControl("hfHeadOfExpenseId");
                        CheckBox check = (CheckBox)GridView1.Rows[i].FindControl("valueCheck");
                        TextBox Date = (TextBox)GridView1.Rows[i].FindControl("txtDate");
                        TextBox NumberOfdays = (TextBox)GridView1.Rows[i].FindControl("NumberOfdays");
                        TextBox Voucher = (TextBox)GridView1.Rows[i].FindControl("Voucher");
                        TextBox Amount = (TextBox)GridView1.Rows[i].FindControl("Amount");
                        TextBox txtRent = (TextBox)GridView1.Rows[i].FindControl("txtRent");
                        TextBox txtChildrenNo = (TextBox)GridView1.Rows[i].FindControl("txtChildrenNo");
                        TextBox txt_RemaksforHR = (TextBox)GridView1.Rows[i].FindControl("txt_RemaksforHR");
                        for (int j = 0; j < dtClaim.Rows.Count; j++)
                        {
                            string Id = dtClaim.Rows[j]["OIPDHeadOfExpenseId"].ToString();

                            if (HeadId.Value == Id)
                            {
                                check.Checked = true;
                                Amount.Text = dtClaim.Rows[j]["Amount"].ToString();
                                NumberOfdays.Text = dtClaim.Rows[j]["NoOfDays"].ToString();
                                Voucher.Text = dtClaim.Rows[j]["SINoOfEncloseVoucher"].ToString();
                                txtRent.Text = dtClaim.Rows[j]["Rent"].ToString();
                                txt_RemaksforHR.Text = dtClaim.Rows[j]["RemaksforHR"].ToString();
                                txtChildrenNo.Text = dtClaim.Rows[j]["ChildrenNo"].ToString();
                                if (dtClaim.Rows[j]["Dates"] != null)
                                {
                                    Date.Text = dtClaim.Rows[j]["Dates"].ToString();
                                }

                            }
                        }
                    }


                    //GridView1.DataSource = null;
                    //GridView1.DataBind();
                    //GridView1.DataSource = dtClaim;
                    //GridView1.DataBind();

                    decimal markTotal = 0;
                    GettotalMark(markTotal);


                }
            }


            if (inlineRadio.SelectedValue == "OPD" || inlineRadio.SelectedValue == "Special")
            {
                for (int i = 0; i < loadGridView.Rows.Count; i++)
                {
                    CheckBox Yes = (CheckBox)loadGridView.Rows[i].FindControl("Yes");
                    TextBox Date = (TextBox)loadGridView.Rows[i].FindControl("Date");
                    if (Yes.Checked)
                    {
                        Date.Enabled = true;

                    }
                    

                }

            }
            else
            {
                {                  
                    for (int i = 0; i < loadGridView.Rows.Count; i++)
                    {
                        CheckBox Yes = (CheckBox)loadGridView.Rows[i].FindControl("Yes");
                        TextBox Date = (TextBox)loadGridView.Rows[i].FindControl("Date");
                        Label Description = (Label)loadGridView.Rows[i].FindControl("Description");

                        if (Description.Text.Trim() == "Have you consulted with Company Doctor about the sickness/treatment?")
                        {
                            if (Yes.Checked)
                            {
                                Date.Enabled = true;
                            }
                        }


                        if (Description.Text.Trim() == "Have you taken the measure(s) as per the advice of Company Doctor?")
                        {

                        }


                        if (Description.Text.Trim() == "When did you inform Concerened HRD/Medical SupportCommittee for hospitalization?")
                        {
                            Date.Enabled = true;
                        }

                    }

                }
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
                    DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[i].FindControl("ddlEmpName"));
                    using (DataTable dt = _commonDataLoad.GetEmpDDLNewMeetinig())
                    {



                        ddlEmpName.DataSource = dt;
                        ddlEmpName.DataValueField = "EmpInfoId";
                        ddlEmpName.DataTextField = "EmpName";
                        ddlEmpName.DataBind();
                        ddlEmpName.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                        ddlEmpName.SelectedIndex = 0;

                    }
                }

                for (int i = 0; i < gv_Member.Rows.Count; i++)
                {
                    RadioButtonList rbType = ((RadioButtonList)gv_Member.Rows[i].FindControl("rbType"));

                    rbType.SelectedValue = ((HiddenField)gv_Member.Rows[i].FindControl("hfType"))
                          .Value;
                    HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[i].FindControl("MemEmpInfoId"));
                    DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[i].FindControl("ddlEmpName"));
                    TextBox txt_EmpName = (TextBox)gv_Member.Rows[i].FindControl("txt_EmpName");

                    TextBox txt_Designation = ((TextBox)gv_Member.Rows[i].FindControl("txt_Designation"));

                    TextBox txt_EmpMasterCode = ((TextBox)gv_Member.Rows[i].FindControl("txt_EmpMasterCode"));

                    //ddlEmpName.Visible = true;
                    //txt_EmpName.Visible = false;


                    ddlEmpName.SelectedValue = MemEmpInfoId.Value;


                    if (ddlEmpName.SelectedValue != "")
                    {
                        int mid = Convert.ToInt32(ddlEmpName.SelectedValue);
                        using (var db = new HRIS_SMCEntities())
                        {
                            var emp = (from j in db.tblEmpGeneralInfoes

                                       where j.EmpInfoId == mid
                                       select j).FirstOrDefault();

                            txt_EmpMasterCode.Text = emp.EmpMasterCode;
                            //txt_EmpName.Text = emp.EmpName;

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
                EnsureDocumentCriteriaColumn(dtDoc);
                ViewState["DocGrid_List"] = dtDoc;
                gv_DocumentUpload.DataSource = dtDoc;
                gv_DocumentUpload.DataBind();
            }
            if (inlineRadio.SelectedValue == "IPD")
            {
                divHospital.Visible = true;
                divHospitalAdmissionDischargeDate.Visible = true;
                divHospitalDischargeDate.Visible = true;
                try
                {
                    ddlHospitalName.SelectedValue = dtMaster.Rows[0]["HospitalNameId"].ToString();
                    txtHospitalName.Text = ddlHospitalName.SelectedIndex > 0 ? ddlHospitalName.SelectedItem.Text : string.Empty;
                }
                catch
                {
                    txtHospitalName.Text = string.Empty;
                }

                string admissionDate = GetFirstAvailableColumnValue(
                    dtMaster.Rows[0],
                    "HospitalAdmissionDate",
                    "HospitalAddmissionDate");

                string dischargeDate = GetFirstAvailableColumnValue(
                    dtMaster.Rows[0],
                    "HospitalDischargeDate");

                if (string.IsNullOrWhiteSpace(admissionDate) || string.IsNullOrWhiteSpace(dischargeDate))
                {
                    string legacyDateRange = GetFirstAvailableColumnValue(
                        dtMaster.Rows[0],
                        "HospitalAddmissionDischargeDate",
                        "HospitalAdmissionDischargeDate");

                    string legacyAdmissionDate;
                    string legacyDischargeDate;
                    SplitHospitalDateRange(legacyDateRange, out legacyAdmissionDate, out legacyDischargeDate);

                    if (string.IsNullOrWhiteSpace(admissionDate))
                    {
                        admissionDate = legacyAdmissionDate;
                    }

                    if (string.IsNullOrWhiteSpace(dischargeDate))
                    {
                        dischargeDate = legacyDischargeDate;
                    }
                }

                txtHospitalAdmissionDate.Text = FormatHospitalDateForEdit(admissionDate);
                txtHospitalDischargeDate.Text = FormatHospitalDateForEdit(dischargeDate);
            }
        }
    }

    private static string GetFirstAvailableColumnValue(DataRow row, params string[] columnNames)
    {
        if (row == null || row.Table == null || columnNames == null)
        {
            return string.Empty;
        }

        foreach (string columnName in columnNames)
        {
            if (!string.IsNullOrWhiteSpace(columnName) && row.Table.Columns.Contains(columnName))
            {
                object value = row[columnName];
                return value == DBNull.Value ? string.Empty : value.ToString();
            }
        }

        return string.Empty;
    }

    private static string FormatHospitalDateForEdit(string dateValue)
    {
        if (string.IsNullOrWhiteSpace(dateValue))
        {
            return string.Empty;
        }

        DateTime parsedDate;
        if (DateTime.TryParse(dateValue.Trim(), out parsedDate))
        {
            return parsedDate.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
        }

        return dateValue.Trim();
    }

    private static void SplitHospitalDateRange(string dateRangeValue, out string admissionDate, out string dischargeDate)
    {
        admissionDate = string.Empty;
        dischargeDate = string.Empty;

        if (string.IsNullOrWhiteSpace(dateRangeValue))
        {
            return;
        }

        string normalizedValue = dateRangeValue.Trim();
        string[] separators = { " to ", " TO ", " To ", " \u09A5\u09C7\u0995\u09C7 ", " \u2013 ", " \u2014 ", " - " };

        foreach (string separator in separators)
        {
            int separatorIndex = normalizedValue.IndexOf(separator, StringComparison.Ordinal);
            if (separatorIndex > 0)
            {
                admissionDate = normalizedValue.Substring(0, separatorIndex).Trim();
                dischargeDate = normalizedValue.Substring(separatorIndex + separator.Length).Trim();
                return;
            }
        }

        admissionDate = normalizedValue;
    }

    protected void Option()
    {
        DataTable tb1 = formDal.GetOption();

        loadGridView.DataSource = tb1;
        loadGridView.DataBind();

            int j = 0;
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {


                bool yesNovalue =false;

                try
                {
                    yesNovalue = Convert.ToBoolean(loadGridView.DataKeys[i][0]);
                }
                catch (Exception)
                {
                    
                    //throw;
                }
                bool datevalue =false;

                try
                {
                    datevalue = Convert.ToBoolean(loadGridView.DataKeys[i][1]);
                }
                catch (Exception)
                {
                    
                    //throw;
                }

                CheckBox yesChkBox = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("Yes");
                CheckBox noChkBox = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("No");
                TextBox dateTextBox = (TextBox)loadGridView.Rows[i].Cells[0].FindControl("Date");

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

                    if (yesNovalue == false)
                    {
                        dateTextBox.Visible = true;
                        dateTextBox.Enabled = true;
                    }
                    else
                    {
                        dateTextBox.Visible = true;
                        dateTextBox.Enabled = false;
                       // dateTextBox.Text = string.Empty;
                    }

                }
                else
                {
                    dateTextBox.Visible = false;
                }


            }

    }

    protected void grid_onclick(object sender, EventArgs e)
    {
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            CheckBox yes = (CheckBox) loadGridView.Rows[i].FindControl("Yes");

            CheckBox NO = (CheckBox) loadGridView.Rows[i].FindControl("No");

            bool yesck = yes.Checked;

            if (yesck)
            {
                NO.Checked = false;
            }
           
        }
    }


    private void LoadInitialGrid()
    {

        DataTable aTable = new DataTable();
        aTable.Columns.Add("HeadOfExpense");
        aTable.Columns.Add("Dates");
        aTable.Columns.Add("NoOfDays");
        aTable.Columns.Add("SINoOfEncloseVoucher");
        aTable.Columns.Add("Amount");
        aTable.Columns.Add("OIPDHeadOfExpenseId");
        aTable.Columns.Add("IsIncrement");
        aTable.Columns.Add("EnRollStartDate");
        aTable.Columns.Add("EnRollEndDate");
        aTable.Columns.Add("Amount_new");
        aTable.Columns.Add("Rent");
        aTable.Columns.Add("RemaksforHR");
        aTable.Columns.Add("ChildrenNo");
        aTable.Columns.Add("YourConditionField");
        DataRow dr;
        dr = aTable.NewRow();
        dr["HeadOfExpense"] = "";
        dr["Dates"] = "";
        dr["NoOfDays"] = "";
        dr["SINoOfEncloseVoucher"] = "";
        dr["Amount"] = "";
        dr["OIPDHeadOfExpenseId"] = "";
        dr["IsIncrement"] = "";
        dr["EnRollStartDate"] = "";
        dr["EnRollEndDate"] = "";
        dr["Amount_new"] = "";
        dr["Rent"] = "";
        dr["RemaksforHR"] = "";
        dr["ChildrenNo"] = "";
        dr["YourConditionField"] = "";
        aTable.Rows.Add(dr);
        GridView1.DataSource = aTable;
        GridView1.DataBind();
    
        GridView1.Rows[0].Cells[0].Visible = false;
        GridView1.Rows[0].Cells[1].Visible = false;
        GridView1.Rows[0].Cells[2].Visible = false;
        GridView1.Rows[0].Cells[3].Visible = false;


    }


    private void LoadInitialDDL()
    {
        BindHospitalNameDropdown();

        if (hfMasterId.Value == "")
        {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {

            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();

            try
            {
                ddlCompany.SelectedValue = Session["CompanyId"].ToString();
            }
            catch (Exception)
            {

                ddlCompany.SelectedIndex = 1;
            }
           

          
            if (ddlCompany.SelectedValue != "")
            {
                using (DataTable dtt = formDal.Get_FinancialByIForSelecttedValueEdit(ddlCompany.SelectedValue.ToString()))
                {
                    ddlFinancialYear.DataSource = dtt;
                    ddlFinancialYear.DataValueField = "FinancialYearId";
                    ddlFinancialYear.DataTextField = "FinancialYearDesc";
                    ddlFinancialYear.DataBind();
                    ddlFinancialYear.Items.Insert(0, new ListItem("Please Select an Financial Year.....", String.Empty));
                    ddlFinancialYear.SelectedIndex = 1;
                    ddlFinancialYear.Enabled = false;
                    ddlFinancialYear_OnSelectedIndexChanged(null, null);
                }


                using (DataTable dtemp = _commonDataLoad.GetEmpDDLAActive(ddlCompany.SelectedValue.ToString()))
                {

                    if (dtemp.Rows.Count > 0)
                    {
                        ddlForwordEmp.DataSource = dtemp;
                        ddlForwordEmp.DataValueField = "EmpInfoId";
                        ddlForwordEmp.DataTextField = "EmpName";
                        ddlForwordEmp.DataBind();
                        ddlForwordEmp.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));



                        //ddlForwordEmp.SelectedValue = Convert.ToInt32(Session["EmpInfoId"]).ToString();
                        //ddlForwordEmp_OnSelectedIndexChanged(null, null);

                        try
                        {
                            ddlForwordEmp.SelectedValue = Convert.ToInt32(Session["EmpInfoId"]).ToString();
                            ddlForwordEmp_OnSelectedIndexChanged(null, null);
                        }
                        catch (Exception)
                        {
                            ddlForwordEmp.SelectedIndex = 0;
                            //throw;
                        }
                    }
                    
                }
            }
            load_IPD_OPD(0, ddlCompany.SelectedValue, hfEmpID.Value);


        

        }
    }
   


    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        using (DataTable dtemp = _commonDataLoad.GetEmpDDLAActive(ddlCompany.SelectedValue.ToString()))
        {

            ddlForwordEmp.DataSource = dtemp;
            ddlForwordEmp.DataValueField = "EmpInfoId";
            ddlForwordEmp.DataTextField = "EmpName";
            ddlForwordEmp.DataBind();
            ddlForwordEmp.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
            try
            {
                ddlForwordEmp.SelectedValue = Convert.ToInt32(Session["EmpInfoId"]).ToString();
            }
            catch (Exception)
            {
                ddlForwordEmp.SelectedIndex = 0;
                //throw;
            }
        }

        using (DataTable dt = formDal.Get_FinancialById(ddlCompany.SelectedValue.ToString()))
        {
            ddlFinancialYear.DataSource = dt;
            ddlFinancialYear.DataValueField = "FinancialYearId";
            ddlFinancialYear.DataTextField = "FinancialYearDesc";
            ddlFinancialYear.DataBind();
            ddlFinancialYear.Items.Insert(0, new ListItem("Please Select an Financial Year.....", String.Empty));
        }
        load_IPD_OPD(0, ddlCompany.SelectedValue, hfEmpID.Value);
        Remainingbalance();

    
    }

    protected void ddlForwordEmp_OnSelectedIndexChanged(object sender, EventArgs e)
    {


        try
        {
            DataTable dtEmp = formDal.GetEmployeeDetails(Convert.ToInt32(ddlForwordEmp.SelectedValue));
            if (dtEmp.Rows.Count > 0)
            {

                lblEmployeeName.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();

                hfEmpID.Value = dtEmp.Rows[0]["EmpInfoId"].ToString().Trim();

                lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();

                deptNameLabel.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();

                desigNameLabel.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();

                try
                {
                    txtOfficailMobile.Text = dtEmp.Rows[0]["OfficialMobile"].ToString().Trim();
                }
                catch (Exception)
                {
                    //throw;
                }

                LocationLabel.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
                lblPlace.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();

                //Bank Info

                //lblBankAccountNo.Text = dtEmp.Rows[0]["BankAccountNo"].ToString().Trim();
                //lblBankName.Text = dtEmp.Rows[0]["BankName"].ToString().Trim();
                //lblBranchName.Text = dtEmp.Rows[0]["BranchName"].ToString().Trim();
                //lblRoutingNo.Text = dtEmp.Rows[0]["RoutingNo"].ToString().Trim();


                Remainingbalance();

                if (hfEmpID.Value != "")
                {
                    NameofPatient.ReadOnly = false;
                    Relationship.ReadOnly = false;

                    NameofPatient.Text = "";
                    Age.Text = "";
                    Relationship.Text = "";

                    loadFamilyMember(Convert.ToInt32(hfEmpID.Value));
                }


            }
        }
        catch (Exception)
        {
            
            //throw;
        }

    }

    
    public void Remainingbalance()
    {
        try
        {
            string balanceType = "";

            string type = "";

            if (inlineRadio.SelectedValue == "IPD")
            {
                balanceType = "IPD";
                type = "IPD";
            }
            else if (inlineRadio.SelectedValue == "OPD")
            {
                balanceType = "OPD";
                type = "OPD";
            }

            if (ddlCompany.SelectedValue != "" && ddlFinancialYear.SelectedValue != "" && hfEmpID.Value != "" && balanceType != "")
            {

                DataTable dt = formDal.Get_RemainningBalance(hfEmpID.Value, ddlCompany.SelectedValue, ddlFinancialYear.SelectedValue, balanceType, type);

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


            if (inlineRadio.SelectedValue == "Special")
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

    public void load_IPD_OPD(int IsOPD, string ComId, string EmpId)
    {
        DataTable dt = formDal.Get_IpdOpd(IsOPD, ComId, EmpId, inlineRadio.SelectedValue);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        decimal markTotal = 0;
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TextBox date = (TextBox)GridView1.Rows[i].FindControl("txtDate");
                TextBox Voucher = (TextBox)GridView1.Rows[i].FindControl("Voucher");
                TextBox Amount = (TextBox)GridView1.Rows[i].FindControl("Amount");
                date.Text = string.Empty;
                Voucher.Text = string.Empty;
            }
            GettotalMark(markTotal);
        }
    }


    public void load_OPD(int IsOPD, string ComId)
    {
        DataTable dt = formDal.Get_IpdOpd(IsOPD, ComId, ComId, inlineRadio.SelectedValue);
        gv_OPD.DataSource = dt;
        gv_OPD.DataBind();
        decimal markTotal = 0;
        if (gv_OPD.Rows.Count > 0)
        {
            for (int i = 0; i < gv_OPD.Rows.Count; i++)
            {
                TextBox date = (TextBox)gv_OPD.Rows[i].FindControl("txtDate");
                TextBox Voucher = (TextBox)gv_OPD.Rows[i].FindControl("Voucher");
                TextBox Amount = (TextBox)gv_OPD.Rows[i].FindControl("Amount");
                date.Text = string.Empty;
                Voucher.Text = string.Empty;
            }
            GettotalMark_OPD(markTotal);
        }
    }

    private void GettotalMark(decimal markTotal)
    {
        ApplyCaesareanDeliveryExclusiveRule();

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            TextBox Amount = (TextBox)GridView1.Rows[i].FindControl("Amount");
            decimal rowAmount = 0;
            if (Amount != null)
            {
                decimal.TryParse(Amount.Text.Trim(), out rowAmount);
            }

            markTotal = markTotal + rowAmount;

        }

        Label tst2 = (Label)GridView1.FooterRow.FindControl("lblTotalMark");
        tst2.Text = markTotal.ToString();

        amount = markTotal;




        {
            if (RemainingBalance.Text != "")
            {
                if (Convert.ToDecimal(RemainingBalance.Text.Trim()) < Convert.ToDecimal(markTotal))
                {
                    //aShowMessage.ShowMessageBox("Total claim bill must be less then Remaining Balance", this);
                }
            }

        }

    }

    private void ApplyCaesareanDeliveryExclusiveRule()
    {
        bool hasCaesareanData = false;
        bool hasNonCaesareanData = false;

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            Label headOfExpense = (Label)GridView1.Rows[i].FindControl("HeadOfExpense");
            if (headOfExpense == null)
            {
                continue;
            }

            TextBox txtDate = (TextBox)GridView1.Rows[i].FindControl("txtDate");
            TextBox voucher = (TextBox)GridView1.Rows[i].FindControl("Voucher");
            TextBox txtRent = (TextBox)GridView1.Rows[i].FindControl("txtRent");

            if (!HasCaesareanTriggerData(txtDate, voucher, txtRent))
            {
                continue;
            }

            if (string.Equals(headOfExpense.Text.Trim(), "Caesarean Delivery", StringComparison.OrdinalIgnoreCase))
            {
                hasCaesareanData = true;
            }
            else
            {
                hasNonCaesareanData = true;
            }
        }

        bool lockNonCaesareanRows = hasCaesareanData && !hasNonCaesareanData;
        bool lockCaesareanRow = hasNonCaesareanData;

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            GridViewRow row = GridView1.Rows[i];

            Label headOfExpense = (Label)row.FindControl("HeadOfExpense");
            TextBox txtDate = (TextBox)row.FindControl("txtDate");
            TextBox voucher = (TextBox)row.FindControl("Voucher");
            TextBox numberOfDays = (TextBox)row.FindControl("NumberOfdays");
            TextBox txtRent = (TextBox)row.FindControl("txtRent");
            TextBox txtChildrenNo = (TextBox)row.FindControl("txtChildrenNo");
            TextBox totalAmount = (TextBox)row.FindControl("Amount");

            string headText = headOfExpense == null ? string.Empty : headOfExpense.Text.Trim();
            bool isCaesarean = string.Equals(headText, "Caesarean Delivery", StringComparison.OrdinalIgnoreCase);
            bool isNormalDelivery = string.Equals(headText, "Normal Delivery", StringComparison.OrdinalIgnoreCase);

            if (numberOfDays != null)
            {
                numberOfDays.ReadOnly = false;
            }

            if (txtChildrenNo != null)
            {
                txtChildrenNo.ReadOnly = !(isCaesarean || isNormalDelivery);
            }

            if (txtRent != null && !isCaesarean && !isNormalDelivery)
            {
                txtRent.ReadOnly = false;
            }

            bool shouldDimRow =
                (lockNonCaesareanRows && !isCaesarean) ||
                (lockCaesareanRow && isCaesarean);

            if (shouldDimRow)
            {
                if (txtRent != null)
                {
                    txtRent.Text = "0";
                }

                if (numberOfDays != null)
                {
                    numberOfDays.Text = "0";
                }

                if (totalAmount != null)
                {
                    totalAmount.Text = "0";
                }

                SetClaimInputState(txtDate, false);
                SetClaimInputState(voucher, false);
                SetClaimInputState(numberOfDays, false);
                SetClaimInputState(txtRent, false);
            }
            else
            {
                SetClaimInputState(txtDate, true);
                SetClaimInputState(voucher, true);
                SetClaimInputState(numberOfDays, true);
                SetClaimInputState(txtRent, true);
            }
        }
    }

    private void SetClaimInputState(WebControl control, bool isEnabled)
    {
        if (control == null)
        {
            return;
        }

        control.Enabled = isEnabled;
        control.Style["opacity"] = isEnabled ? "1" : "0.45";
        control.Style["pointer-events"] = isEnabled ? "auto" : "none";
    }

    private bool HasCaesareanTriggerData(TextBox txtDate, TextBox voucher, TextBox txtRent)
    {
        bool hasDate = txtDate != null
                       && !string.IsNullOrWhiteSpace(txtDate.Text)
                       && txtDate.Text.Trim() != "0";

        bool hasVoucher = voucher != null
                          && !string.IsNullOrWhiteSpace(voucher.Text)
                          && voucher.Text.Trim() != "0";

        decimal rent = 0;
        bool hasRent = txtRent != null
                       && decimal.TryParse(txtRent.Text.Trim(), out rent)
                       && rent > 0;

        return hasDate || hasVoucher || hasRent;
    }



    private void GettotalMark_OPD(decimal markTotal)
    {
        // parameter দিয়ে যা আসুক, আমরা এখানে থেকেই নতুন করে হিসাব শুরু করবো
        markTotal = 0m;

        for (int i = 0; i < gv_OPD.Rows.Count; i++)
        {
            TextBox txtRent = (TextBox)gv_OPD.Rows[i].FindControl("txtRent");
            CheckBox Check = (CheckBox)gv_OPD.Rows[i].FindControl("valueCheck");

            // যদি শুধু checked row গুলা নিতে চাও তাহলে নিচের if uncomment করবে
            // if (Check.Checked)
            {
                decimal rent = 0m;

                // txtRent.Text null/empty/invalid হলেও rent ডিফল্ট 0 থাকবে
                if (!string.IsNullOrWhiteSpace(txtRent.Text))
                {
                    decimal.TryParse(txtRent.Text.Trim(), out rent);
                }

                markTotal += rent;
            }
        }

        // Footer label এ total দেখাই
        Label lblRentTotalMark = (Label)gv_OPD.FooterRow.FindControl("lblRentTotalMark");
        lblRentTotalMark.Text = markTotal.ToString("0.##");   // চাইলে format change করতে পারো

        // global / class-level variable ধরছি amount
        amount = markTotal;

        // Special হলে balance check skip
        if (inlineRadio.SelectedValue != "Special")
        {
            // RemainingBalance safe ভাবে নিই
            if (!string.IsNullOrWhiteSpace(RemainingBalance.Text))
            {
                decimal remaining = 0m;
                if (decimal.TryParse(RemainingBalance.Text.Trim(), out remaining))
                {
                    if (remaining < markTotal)
                    {
                        // aShowMessage.ShowMessageBox("Total claim bill must be less then Remaining Balance", this);

                        // চাইলে এখানে auto adjust করতে পারো:
                        // amount = remaining;
                        // lblRentTotalMark.Text = remaining.ToString("0.##");
                    }
                }
                else
                {
                    // RemainingBalance invalid হলে চাইলে এখানে message দিতে পারো
                    // aShowMessage.ShowMessageBox("Invalid remaining balance amount.", this);
                }
            }
        }
    }


    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();

        cell = new TableHeaderCell();
        cell.ColumnSpan = 1;
        cell.Text = "";
        cell.BackColor = ColorTranslator.FromHtml("#ffffff");
        cell.ForeColor = Color.Black;
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 3;
        cell.Text = "Claim Details";
        cell.BackColor = ColorTranslator.FromHtml("#B4C6E7");
        cell.ForeColor = Color.Black;
        row.Controls.Add(cell);

        GridView1.HeaderRow.Parent.Controls.AddAt(0, row);
    }

    protected void btnDocUp_OnClick(object sender, EventArgs e)
    {
        if (FUDocument.HasFile)
        {
            string _fileExt = System.IO.Path.GetExtension(FUDocument.FileName);
            string AdsFile = "Reimburs_form_DOC_" + Guid.NewGuid().ToString() + Path.GetExtension(FUDocument.FileName);
            //  fileName = guid.ToString() + imageFileUpload.FileName;
            FUDocument.SaveAs(Server.MapPath("../UploadHealthCareDoc/") + AdsFile);
            HyperLink2.NavigateUrl = "../UploadHealthCareDoc/" + AdsFile;
            //   HyperLink2.Text = "Uploaded Successfully";
        }
        else
        {
            HyperLink2.NavigateUrl = "";
            //   HyperLink2.Text = "No File Uploaded";
        }
    }

    protected void brnAddDoc_OnClick(object sender, EventArgs e)
    {
        if (docVali())
        {
            AddNewDocGrid_List();

        }
    }

    protected void AttachmentCriteria_OnCheckedChanged(object sender, EventArgs e)
    {
        ApplyAttachmentCriteriaAvailability(false);
    }
     
    ShowMessage aShowMessage = new ShowMessage();

    private bool docVali()
    {
        lblMsg.Text = "";
        if (hfDocFile.Value == "")
        {
            aShowMessage.ShowMessageBox("Please click Document Upload Button", this);

            return false;
        }

        if (ckOther.Checked)
        {
            if (txtSummaryNote.Text == "")
            {
                aShowMessage.ShowMessageBox("Please Enter Summary Note ", this);
                lblMsg.Text = "<b>" + hfDocFileName.Value + "</b> has been uploaded.";
                return false;
            }
        }

        if (!HasAnyAttachmentCriteriaSelected())
        {
            aShowMessage.ShowMessageBox("Select at least one criteria (a. Prescriptions / b. Bills / c. Reports / d. Discharge Paper / e. Other Documents).", this);
            return false;
        }

        if (HasMultipleAttachmentCriteriaSelected())
        {
            aShowMessage.ShowMessageBox("Select only one criteria (Prescriptions / Bills / Reports / Discharge Paper / Other Documents).", this);
            return false;
        }

        if (gv_DocumentUpload.Rows.Count >= 5)
        {
            aShowMessage.ShowMessageBox("Attachment limit reached. Maximum 5 documents allowed.", this);
            return false; // Stop further processing
        }
        return true;

    }

    private string GetComboBoxtext()
    {
        if (chkPrescriptions.Checked)
        {
            return chkPrescriptions.Text;
        }
        if (chkBills.Checked)
        {
            return chkBills.Text;
        }
        if (chkReports.Checked)
        {
            return chkReports.Text;
        }
        if (chkDischargePaper.Checked)
        {
            return chkDischargePaper.Text;
        }
        if (ckOther.Checked)
        {
            return ckOther.Text;
        }

        return string.Empty;
    }


    private bool HasAnyAttachmentCriteriaSelected()
    {
        return chkPrescriptions.Checked || chkBills.Checked || chkReports.Checked || chkDischargePaper.Checked || ckOther.Checked;
    }

    private bool HasMultipleAttachmentCriteriaSelected()
    {
        int selectedCount = 0;

        if (chkPrescriptions.Checked) selectedCount++;
        if (chkBills.Checked) selectedCount++;
        if (chkReports.Checked) selectedCount++;
        if (chkDischargePaper.Checked) selectedCount++;
        if (ckOther.Checked) selectedCount++;

        return selectedCount > 1;
    }

    private void EnsureDocumentCriteriaColumn(DataTable dt)
    {
        if (dt == null)
        {
            return;
        }

        if (!dt.Columns.Contains("DocumentCriteria"))
        {
            dt.Columns.Add(new DataColumn("DocumentCriteria", typeof(string)));
        }

        foreach (DataRow row in dt.Rows)
        {
            string criteria = Convert.ToString(row["DocumentCriteria"]).Trim();
            if (criteria == string.Empty)
            {
                criteria = ResolveAttachmentCriteriaFromNote(Convert.ToString(row["DocumentNote"]));
                row["DocumentCriteria"] = criteria;
            }
        }
    }

    private string GetSelectedAttachmentCriteria()
    {
        return GetComboBoxtext();
    }

    private string ResolveAttachmentCriteriaFromNote(string documentNote)
    {
        if (string.IsNullOrWhiteSpace(documentNote))
        {
            return string.Empty;
        }

        string normalizedNote = documentNote.Trim();
        if (string.Equals(normalizedNote, chkPrescriptions.Text, StringComparison.OrdinalIgnoreCase))
        {
            return chkPrescriptions.Text;
        }
        if (string.Equals(normalizedNote, chkBills.Text, StringComparison.OrdinalIgnoreCase))
        {
            return chkBills.Text;
        }
        if (string.Equals(normalizedNote, chkReports.Text, StringComparison.OrdinalIgnoreCase))
        {
            return chkReports.Text;
        }
        if (string.Equals(normalizedNote, chkDischargePaper.Text, StringComparison.OrdinalIgnoreCase))
        {
            return chkDischargePaper.Text;
        }

        return ckOther.Text;
    }

    private void ApplyAttachmentCriteriaAvailability(bool clearSelection)
    {
        DataTable docTable = ViewState["DocGrid_List"] as DataTable;
        HashSet<string> usedCriteria = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        EnsureDocumentCriteriaColumn(docTable);

        if (docTable != null)
        {
            foreach (DataRow row in docTable.Rows)
            {
                string criteria = Convert.ToString(row["DocumentCriteria"]).Trim();
                if (criteria != string.Empty)
                {
                    usedCriteria.Add(criteria);
                }
            }
        }

        if (clearSelection)
        {
            chkPrescriptions.Checked = false;
            chkBills.Checked = false;
            chkReports.Checked = false;
            chkDischargePaper.Checked = false;
            ckOther.Checked = false;
            txtSummaryNote.Text = string.Empty;
        }

        SetAttachmentCriteriaState(
            chkPrescriptions,
            FindAttachmentCriteriaContainer("attachmentPrescriptionContainer"),
            usedCriteria.Contains(chkPrescriptions.Text));

        SetAttachmentCriteriaState(
            chkBills,
            FindAttachmentCriteriaContainer("attachmentBillsContainer"),
            usedCriteria.Contains(chkBills.Text));

        SetAttachmentCriteriaState(
            chkReports,
            FindAttachmentCriteriaContainer("attachmentReportsContainer"),
            usedCriteria.Contains(chkReports.Text));

        SetAttachmentCriteriaState(
            chkDischargePaper,
            FindAttachmentCriteriaContainer("attachmentDischargeContainer"),
            usedCriteria.Contains(chkDischargePaper.Text));

        SetAttachmentCriteriaState(
            ckOther,
            FindAttachmentCriteriaContainer("attachmentOtherContainer"),
            usedCriteria.Contains(ckOther.Text));

        Description_.Visible = ckOther.Checked && ckOther.Enabled;
        if (!Description_.Visible && clearSelection)
        {
            txtSummaryNote.Text = string.Empty;
        }
    }

    private System.Web.UI.HtmlControls.HtmlGenericControl FindAttachmentCriteriaContainer(string controlId)
    {
        Control container = FindControlRecursive(this, controlId);
        return container as System.Web.UI.HtmlControls.HtmlGenericControl;
    }

    private Control FindControlRecursive(Control parent, string controlId)
    {
        if (parent == null)
        {
            return null;
        }

        Control matchedControl = parent.FindControl(controlId);
        if (matchedControl != null)
        {
            return matchedControl;
        }

        foreach (Control childControl in parent.Controls)
        {
            matchedControl = FindControlRecursive(childControl, controlId);
            if (matchedControl != null)
            {
                return matchedControl;
            }
        }

        return null;
    }

    private void SetAttachmentCriteriaState(RadioButton criteriaControl, System.Web.UI.HtmlControls.HtmlGenericControl container, bool isUsed)
    {
        criteriaControl.Enabled = !isUsed;
        if (isUsed)
        {
            criteriaControl.Checked = false;
        }

        if (container != null)
        {
            if (isUsed)
            {
                container.Attributes["class"] = "form-group_2 attachment-criteria-item attachment-criteria-disabled";
            }
            else
            {
                container.Attributes["class"] = "form-group_2 attachment-criteria-item";
            }
        }
    }

    private void AddNewDocGrid_List()
    {
        string selectedCriteria = GetSelectedAttachmentCriteria();

        if (ViewState["DocGrid_List"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["DocGrid_List"];
            EnsureDocumentCriteriaColumn(dtCurrentTable);
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();


                string extension;

                extension = Path.GetExtension(hfDocFile.Value);
                //jpg, png,xlsx,pdf,txt,doc,docx

                drCurrentRow["DocumentLink"] = "http://182.160.103.234:8090/" + hfDocFile.Value;
                //drCurrentRow["DocumentLink"] =  @"file:///D:/UploadHealthCareDoc/"+ hfDocFile.Value;
                drCurrentRow["FileName"] = hfDocFileName.Value;


                drCurrentRow["DocumentNote"] = txtSummaryNote.Text.Trim();


                if (ckOther.Checked)
                {

                    drCurrentRow["DocumentNote"] = txtSummaryNote.Text.Trim();
                }
                else
                {
                    string comment = GetComboBoxtext();

                    drCurrentRow["DocumentNote"] = comment;
                }

                drCurrentRow["DocumentCriteria"] = selectedCriteria;

                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["DocGrid_List"] = dtCurrentTable;

                //Rebind the Grid with the current data to reflect changes   
                gv_DocumentUpload.DataSource = dtCurrentTable;
                gv_DocumentUpload.DataBind();
            }
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("DocumentLink", typeof(string)));
            dt.Columns.Add(new DataColumn("DocumentNote", typeof(string)));
            dt.Columns.Add(new DataColumn("FileName", typeof(string)));
            dt.Columns.Add(new DataColumn("DocumentLinkPreview", typeof(string)));
            dt.Columns.Add(new DataColumn("DocumentCriteria", typeof(string)));



            dr = dt.NewRow();
            string extension;

            extension = Path.GetExtension(hfDocFile.Value);
            //jpg, png,xlsx,pdf,txt,doc,docx


            dr["DocumentLink"] = "http://182.160.103.234:8090/" + hfDocFile.Value;
            //dr["DocumentLinkPreview"] = "https://docs.google.com/gview?url=http://182.160.103.234:8088/UploadMeetingDocument/" + hfDocFile.Value + "&embedded=true";
            //  dr["DocumentLink"] = @"file:///D:/UploadMeetingDocument/3eec2898121c4467be57981c13852a9e.png"; //@"file:///D:/UploadMeetingDocument/" + hfDocFile.Value;
            dr["FileName"] = hfDocFileName.Value;


            dr["DocumentNote"] = txtSummaryNote.Text.Trim();

            if (ckOther.Checked)
            {
                dr["DocumentNote"] = txtSummaryNote.Text.Trim();
            }
            else
            {
                string comment = GetComboBoxtext();
                dr["DocumentNote"] = comment;
            }

            dr["DocumentCriteria"] = selectedCriteria;


            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["DocGrid_List"] = dt;

            //Bind the Gridview   
            gv_DocumentUpload.DataSource = dt;
            gv_DocumentUpload.DataBind();
        }
        //Set Previous Data on Postbacks   
        SetDocGrid_List();


        txtSummaryNote.Text = string.Empty;
        // HyperLink2.Text = "No File Uploaded";
        HyperLink2.NavigateUrl = "";
        hfDocFile.Value = "";
        ApplyAttachmentCriteriaAvailability(true);
    }


    private void SetDocGrid_List()
    {
        int rowIndex = 0;
        if (ViewState["DocGrid_List"] != null)
        {
            DataTable dt = (DataTable)ViewState["DocGrid_List"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField hfDocumentLink = (HiddenField)gv_DocumentUpload.Rows[rowIndex].FindControl("hfDocumentLink");
                    HiddenField hfFileName = (HiddenField)gv_DocumentUpload.Rows[rowIndex].FindControl("hfFileName");
                    HiddenField hfDocumentLinkPreview = (HiddenField)gv_DocumentUpload.Rows[rowIndex].FindControl("hfDocumentLinkPreview");
                    HyperLink HLDocumentLink = (HyperLink)gv_DocumentUpload.Rows[rowIndex].FindControl("HLDocumentLink");
                    Label lbl_DocumentLink = (Label)gv_DocumentUpload.Rows[rowIndex].FindControl("lbl_DocumentLink");

                    Label lbl_DocumentNote = (Label)gv_DocumentUpload.Rows[rowIndex].FindControl("lbl_DocumentNote");


                    if (i < dt.Rows.Count - 1)
                    {
                        hfDocumentLink.Value = dt.Rows[i]["DocumentLink"].ToString();
                        hfFileName.Value = dt.Rows[i]["FileName"].ToString();
                        hfDocumentLinkPreview.Value = dt.Rows[i]["DocumentLinkPreview"].ToString();
                        lbl_DocumentLink.Text = dt.Rows[i]["DocumentLink"].ToString();
                        HLDocumentLink.NavigateUrl = dt.Rows[i]["DocumentLink"].ToString();

                        lbl_DocumentNote.Text = dt.Rows[i]["DocumentNote"].ToString();

                    }

                    rowIndex++;
                }
            }
        }
    }

    protected void btnDocRemove_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["DocGrid_List"] != null)
        {
            DataTable dt = (DataTable)ViewState["DocGrid_List"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["DocGrid_List"] = dt;
                //Re bind the GridView for the updated data  
                gv_DocumentUpload.DataSource = dt;
                gv_DocumentUpload.DataBind();
            }
            else
            {
                ViewState["DocGrid_List"] = null;
                //Re bind the GridView for the updated data  
                gv_DocumentUpload.DataSource = null;
                gv_DocumentUpload.DataBind();
            }
        }
        //Set Previous Data on Postbacks  
        SetDocGrid_List();
        ApplyAttachmentCriteriaAvailability(true);
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



    //validation

    private bool Validation()
    {

        if (ddlCompany.SelectedValue == " " || ddlCompany.SelectedIndex == 0)
        {
            aShowMessage.ShowMessageBox("Please! Select Company", this);
            ddlCompany.Focus();
            return false;
        }
        if (ddlFinancialYear.SelectedValue == " " || ddlFinancialYear.SelectedIndex == 0)
        {
            aShowMessage.ShowMessageBox("Please! Select Financial Year", this);
            ddlFinancialYear.Focus();
            return false;
        }
        if (ddlForwordEmp.SelectedValue == " " || ddlForwordEmp.SelectedIndex == 0)
        {
            aShowMessage.ShowMessageBox("Please! Select Employee",this);
            ddlForwordEmp.Focus();
            return false;
        }

        if (inlineRadio.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please! Select Application Type", this);
            inlineRadio.Focus();
            return false;
        }



        if (RemainingBalance.Text == "")
        {
            aShowMessage.ShowMessageBox("Remaining Balance can not be empty!", this);
            SubmitDate.Focus();
            return false;
        }
        if (txtOfficailMobile.Text == "")
        {
            aShowMessage.ShowMessageBox("Cell No can not be empty!", this);
            txtOfficailMobile.Focus();
            return false;
        }
        if (SubmitDate.Text == "")
        {
            aShowMessage.ShowMessageBox("Please! Select Submit Date", this);
            SubmitDate.Focus();
            return false;
        }

        if (SelfDate.Text == "")
        {
            aShowMessage.ShowMessageBox("Please! Input Illness Description ", this);
            SelfDate.Focus();
            return false;
        }


        if (txtBankName.Text == "")
        {
            aShowMessage.ShowMessageBox("Account Name can not be empty!!", this);
            txtBankName.Focus();
            return false;
        }

        if (txtBankAccountNo.Text == "")
        {
            aShowMessage.ShowMessageBox("Account No can not be empty!!", this);
            txtBankAccountNo.Focus();
            return false;
        }


        if (txtBranchName.Text == "")
        {
            aShowMessage.ShowMessageBox("Bank & Branch Name can not be empty!!", this);
            txtBranchName.Focus();
            return false;
        }


        if (txtRoutingNo.Text == "")
        {
            aShowMessage.ShowMessageBox("Routing No can not be empty!!", this);
            txtRoutingNo.Focus();
            return false;
        }

        if (NameofPatient.Text == "" )
        {
            aShowMessage.ShowMessageBox("Please! Select input patient name", this);
            NameofPatient.Focus();
            return false;
        }

        if (Relationship.Text == "")
        {
            aShowMessage.ShowMessageBox("Please! Select input Relationship", this);
            Relationship.Focus();
            return false;
        }
     
        

        List<string> ticklist = new List<string>();

        if (Prescription.Checked)
        {
            ticklist.Add(Prescription.Text);
        }

        if (BillofConsaltation.Checked)
        {
            ticklist.Add(BillofConsaltation.Text);
        }

        if (BillofMedicine.Checked)
        {
            ticklist.Add(BillofMedicine.Text);
        }

        if (BillofHospitalization.Checked)
        {
            ticklist.Add(BillofHospitalization.Text);
        }

        if (SpecialDocumentUpload.Checked)
        {
            ticklist.Add(SpecialDocumentUpload.Text);
        }

        if (BillofInvestigation.Checked)
        {
            ticklist.Add(BillofInvestigation.Text);
        }

        if (PhotoCopyofInvestigation.Checked)
        {
            ticklist.Add(PhotoCopyofInvestigation.Text);
        }

        if (BillForCharges.Checked)
        {
            ticklist.Add(BillForCharges.Text);
        }

        if (Other.Checked)
        {
            ticklist.Add(Other.Text);
        }



        if (ticklist.Count == 0)
        {
            aShowMessage.ShowMessageBox("Please Select minimum One Enclosures(Tick) mark", this);
            //   Relationship.Focus();
            return false;
        }

        if (inlineRadio.SelectedValue == "OPD")
        {

        }

        else
        {
            
        }



        if (inlineRadio.SelectedValue == "IPD")
        {
            DateTime hospitalAdmissionDate;
            DateTime hospitalDischargeDate;

            bool hasAdmissionDate = !string.IsNullOrWhiteSpace(txtHospitalAdmissionDate.Text);
            bool hasDischargeDate = !string.IsNullOrWhiteSpace(txtHospitalDischargeDate.Text);

            bool admissionDateParsed = !hasAdmissionDate || DateTime.TryParse(txtHospitalAdmissionDate.Text.Trim(), out hospitalAdmissionDate);
            bool dischargeDateParsed = !hasDischargeDate || DateTime.TryParse(txtHospitalDischargeDate.Text.Trim(), out hospitalDischargeDate);

            if (!admissionDateParsed)
            {
                aShowMessage.ShowMessageBox("Hospital Admission Date is not valid!", this);
                txtHospitalAdmissionDate.Focus();
                return false;
            }

            if (!dischargeDateParsed)
            {
                aShowMessage.ShowMessageBox("Hospital Discharge Date is not valid!", this);
                txtHospitalDischargeDate.Focus();
                return false;
            }

            if (hasAdmissionDate
                && hasDischargeDate
                && DateTime.TryParse(txtHospitalAdmissionDate.Text.Trim(), out hospitalAdmissionDate)
                && DateTime.TryParse(txtHospitalDischargeDate.Text.Trim(), out hospitalDischargeDate)
                && hospitalDischargeDate < hospitalAdmissionDate)
            {
                aShowMessage.ShowMessageBox("Hospital Discharge Date can not be earlier than Admission Date!!", this);
                txtHospitalDischargeDate.Focus();
                return false;
            }

            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                CheckBox Yes = (CheckBox)loadGridView.Rows[i].FindControl("Yes");
                TextBox Date = (TextBox)loadGridView.Rows[i].FindControl("Date");
                Label Description = (Label)loadGridView.Rows[i].FindControl("Description");

                if (Description.Text.Trim() == "Have you consulted with Company Doctor about the sickness/treatment?")
                { 
                if (Yes.Checked)
                {
                    if (Date.Text == "")
                    {
                        aShowMessage.ShowMessageBox("Date can not be Empty!!", this);
                        Date.Focus();
                        return false;
                    }  
                }
                }


                if (Description.Text.Trim() == "Have you taken the measure(s) as per the advice of Company Doctor?")
                {
                    
                }


                if (Description.Text.Trim() == "When did you inform Concerened HRD/Medical SupportCommittee for hospitalization?")
                {
                    if (Date.Text == "")
                    {
                        aShowMessage.ShowMessageBox("Date can not be Empty!!", this);
                        Date.Focus();
                        return false;
                    }  
                }

            }

        }

        if (inlineRadio.SelectedValue == "OPD")
        {
            if (gv_OPD.Rows.Count == 0)
            {
                aShowMessage.ShowMessageBox("Claim Information List can not be Empty!!", this);

                return false;
            }


            Label tst2 = (Label)gv_OPD.FooterRow.FindControl("lblRentTotalMark");
            if (tst2.Text == "0")
            {
                aShowMessage.ShowMessageBox("Amount can not be Zero", this);
                //   Relationship.Focus();
                return false;
            }

            if (Convert.ToDecimal(tst2.Text.Trim()) > Convert.ToDecimal(RemainingBalance.Text.Trim())+Convert.ToDecimal(txtAvailedAmount.Text.Trim()))
            {
                aShowMessage.ShowMessageBox("Total claim bill will less then remaining balance", this);

                return false;
            }


            for (int i = 0; i < gv_OPD.Rows.Count; i++)
            {
                TextBox txtRent = (TextBox)gv_OPD.Rows[i].FindControl("txtRent");
                TextBox Date = (TextBox)gv_OPD.Rows[i].FindControl("txtDate");
                
                TextBox Voucher = (TextBox)gv_OPD.Rows[i].FindControl("Voucher");

                decimal dd = 0;
                try
                {
                    dd = Convert.ToDecimal(txtRent.Text);
                }
                catch (Exception)
                {
                    
                 //   throw;
                }

                if (dd>0)
                {
                    if (Date.Text=="")
                    {
                        aShowMessage.ShowMessageBox("Document Date can not be Empty", this);
                        Date.Focus();
                        return false;
                    }

                    if (Voucher.Text == "")
                    {
                        aShowMessage.ShowMessageBox("SI. No of Enclosed Voucher(Ref. No) can not be Empty", this);
                        Voucher.Focus();
                        return false;
                    }
                }
            }
       
            
        }

        else
            if (inlineRadio.SelectedValue == "Special")
            {
                if (gv_OPD.Rows.Count == 0)
                {
                    aShowMessage.ShowMessageBox("Claim Information List can not be Empty!!", this);

                    return false;
                }


                Label tst2 = (Label)gv_OPD.FooterRow.FindControl("lblRentTotalMark");
                if (tst2.Text == "0")
                {
                    aShowMessage.ShowMessageBox("Amount can not be Zero", this);
                    //   Relationship.Focus();
                    return false;
                }


                //if (Convert.ToDecimal(tst2.Text.Trim()) > Convert.ToDecimal(RemainingBalance.Text.Trim()))
                //{
                //    aShowMessage.ShowMessageBox("Total claim bill will less then remaining balance", this);

                //    return false;
                //}


                for (int i = 0; i < gv_OPD.Rows.Count; i++)
                {
                    TextBox txtRent = (TextBox)gv_OPD.Rows[i].FindControl("txtRent");
                    TextBox Date = (TextBox)gv_OPD.Rows[i].FindControl("txtDate");

                    TextBox Voucher = (TextBox)gv_OPD.Rows[i].FindControl("Voucher");

                    decimal dd = 0;
                    try
                    {
                        dd = Convert.ToDecimal(txtRent.Text);
                    }
                    catch (Exception)
                    {

                        //   throw;
                    }

                    if (dd > 0)
                    {
                        if (Date.Text == "")
                        {
                            aShowMessage.ShowMessageBox("Document Date can not be Empty", this);
                            Date.Focus();
                            return false;
                        }

                        if (Voucher.Text == "")
                        {
                            aShowMessage.ShowMessageBox("SI. No of Enclosed Voucher(Ref. No) can not be Empty", this);
                            Voucher.Focus();
                            return false;
                        }
                    }
                }


            }
         else
         {
             if (GridView1.Rows.Count == 0)
             {
                 aShowMessage.ShowMessageBox("Claim Information List can not be Empty!!", this);

                 return false;
             }


             Label tst2 = (Label)GridView1.FooterRow.FindControl("lblTotalMark");
             if (tst2.Text == "0")
             {
                 aShowMessage.ShowMessageBox("Amount (BDT) can not be Zero", this);
                 //   Relationship.Focus();
                 return false;
             }
            decimal TotalBalance = 0;

            try
            {
                TotalBalance = Convert.ToDecimal(txtTotalBalance.Text);
            }
            catch { }

             //if (  TotalBalance > Convert.ToDecimal(RemainingBalance.Text.Trim())+ Convert.ToDecimal(tst2.Text.Trim()))
             //{
             //    aShowMessage.ShowMessageBox("Total claim bill will less then remaining balance", this);

             //    return false;
             //}

            if (Convert.ToDecimal(tst2.Text.Trim()) > Convert.ToDecimal(RemainingBalance.Text.Trim()) + Convert.ToDecimal(txtAvailedAmount.Text.Trim()))
            {
                aShowMessage.ShowMessageBox("Total claim bill will less then remaining balance", this);

                return false;
            }


            for (int i = 0; i < GridView1.Rows.Count; i++)
             {
                 TextBox txtRent = (TextBox)GridView1.Rows[i].FindControl("txtRent");
                 TextBox Date = (TextBox)GridView1.Rows[i].FindControl("txtDate");

                 TextBox Voucher = (TextBox)GridView1.Rows[i].FindControl("Voucher");
                 TextBox NumberOfdays = (TextBox)GridView1.Rows[i].FindControl("NumberOfdays");

                 decimal dd = 0;
                 try
                 {
                     dd = Convert.ToDecimal(txtRent.Text);
                 }
                 catch (Exception)
                 {

                     //   throw;
                 }

                 if (dd > 0)
                 {
                     if (Date.Text == "")
                     {
                         aShowMessage.ShowMessageBox("Document Date can not be Empty", this);
                         Date.Focus();
                         return false;
                     }

                     if (Voucher.Text == "")
                     {
                         aShowMessage.ShowMessageBox("SI. No of Enclosed Voucher(Ref. No) can not be Empty", this);
                         Voucher.Focus();
                         return false;
                     }

                     if (NumberOfdays.Text == "")
                     {
                         aShowMessage.ShowMessageBox("Number of Days  can not be Empty", this);
                         NumberOfdays.Focus();
                         return false;
                     }
                 }
             }
       
         }

        if (gv_DocumentUpload.Rows.Count == 0)
        {
            aShowMessage.ShowMessageBox("Need to upload  document!", this);
            return false;
        }

        
            //if (inlineRadio.SelectedValue == "IPD")
            //{
            //    if (ddlHospitalName.SelectedValue == "")
            //    {
            //        aShowMessage.ShowMessageBox("Hospital Name can not be empty!!", this);
            //        txtBankAccountNo.Focus();
            //        return false;
            //    }


            //}
     
        return true;
    }

    //save command

    protected void Save_OnClick(object sender, EventArgs e)
    {
        string Action = "Verified";

        Submit(Action);
    }

    private void Submit(string Action)
    {
        if (Validation())
        {
            ReimbursmentMaster aMaster = new ReimbursmentMaster();
            aMaster.CompanyId = int.Parse(ddlCompany.SelectedValue);
            aMaster.FinancialYearId = int.Parse(ddlFinancialYear.SelectedValue);
            aMaster.EmpInfoId = int.Parse(hfEmpID.Value);
            aMaster.PatientAge = string.IsNullOrEmpty(Age.Text) ? 0 : int.Parse(Age.Text);
            aMaster.PatientName = NameofPatient.Text;
            aMaster.Relationship = Relationship.Text;

            aMaster.BankName = txtBankName.Text;
            aMaster.BankAccountNo = txtBankAccountNo.Text;
            aMaster.BranchName = txtBranchName.Text;
            aMaster.RoutingNo = txtRoutingNo.Text;
            aMaster.ActionStatus = Action;
            if (hfMasterId.Value == "")
            {
                aMaster.ReimbursFromMasterId = 0;
            }
            else
            {
                aMaster.ReimbursFromMasterId = int.Parse(hfMasterId.Value);
            }
            aMaster.Type = inlineRadio.SelectedValue.Trim();
            aMaster.SelfDate = SelfDate.Text.ToString();
            aMaster.SubmitDate = DateTime.Parse(SubmitDate.Text);

            if (inlineRadio.SelectedValue == "IPD")
            {
                aMaster.HospitalNameId = formDal.GetOrCreateHospitalNameId(txtHospitalName.Text);

                DateTime hospitalAdmissionDate;
                if (DateTime.TryParse(txtHospitalAdmissionDate.Text.Trim(), out hospitalAdmissionDate))
                {
                    aMaster.HospitalAdmissionDate = hospitalAdmissionDate;
                }
                else
                {
                    aMaster.HospitalAdmissionDate = null;
                }

                DateTime hospitalDischargeDate;
                if (DateTime.TryParse(txtHospitalDischargeDate.Text.Trim(), out hospitalDischargeDate))
                {
                    aMaster.HospitalDischargeDate = hospitalDischargeDate;
                }
                else
                {
                    aMaster.HospitalDischargeDate = null;
                }
            }
            else
            {
                aMaster.HospitalNameId = 0;
                aMaster.HospitalAdmissionDate = null;
                aMaster.HospitalDischargeDate = null;
            }
            aMaster.OfficialMobile = txtOfficailMobile.Text.ToString();
            aMaster.EntryBy = Convert.ToInt32(Session["UserId"].ToString());

            List<ReimbursmentberifDiscriptionDao> discriptionlist = new List<ReimbursmentberifDiscriptionDao>();

            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                HiddenField ID = (HiddenField) loadGridView.Rows[i].FindControl("hfReibCheckOppId");

                CheckBox Yes = (CheckBox) loadGridView.Rows[i].FindControl("Yes");
                CheckBox NO = (CheckBox) loadGridView.Rows[i].FindControl("No");
                TextBox Date = (TextBox) loadGridView.Rows[i].FindControl("Date");

                bool datevalue = Convert.ToBoolean(loadGridView.DataKeys[i][1]);

                ReimbursmentberifDiscriptionDao DocA = new ReimbursmentberifDiscriptionDao();

                DocA.ReibCheckOppId = Convert.ToInt32(ID.Value);

                bool yescheck = Yes.Checked;

                bool Nocheck = NO.Checked;

                if (yescheck)
                {
                    DocA.YesNo = true;
                }

                if (Nocheck)
                {
                    DocA.YesNo = false;
                }

                if (Date.Text != "")
                {
                    DocA.Descriptiondate = Convert.ToDateTime(Date.Text);
                }

                if (datevalue)
                {
                    DocA.Date = true;
                }
                else
                {
                    DocA.Date = false;
                }

                discriptionlist.Add(DocA);
            }

            List<string> firstlist = new List<string>();


            if (Prescription.Checked)
            {
                firstlist.Add(Prescription.Text);
            }

            if (BillofConsaltation.Checked)
            {
                firstlist.Add(BillofConsaltation.Text);
            }

            if (BillofMedicine.Checked)
            {
                firstlist.Add(BillofMedicine.Text);
            }

            if (BillofHospitalization.Checked)
            {
                firstlist.Add(BillofHospitalization.Text);
            }

            if (SpecialDocumentUpload.Checked)
            {
                firstlist.Add(SpecialDocumentUpload.Text);
            }
            
            if (BillofInvestigation.Checked)
            {
                firstlist.Add(BillofInvestigation.Text);
            }

            if (PhotoCopyofInvestigation.Checked)
            {
                firstlist.Add(PhotoCopyofInvestigation.Text);
            }

            if (BillForCharges.Checked)
            {
                firstlist.Add(BillForCharges.Text);
            }

            if (Other.Checked)
            {
                firstlist.Add(Other.Text);
            }

            List<ReimbursmentEnclosuretickMark> Ticklist = new List<ReimbursmentEnclosuretickMark>();

            for (int i = 0; i < firstlist.Count; i++)
            {
                ReimbursmentEnclosuretickMark DocA = new ReimbursmentEnclosuretickMark();
                DocA.EnclosuresTickMark = firstlist[i];
                Ticklist.Add(DocA);
            }

            List<ClaimDetailsDao> claimDetailslList = new List<ClaimDetailsDao>();
            if (inlineRadio.SelectedValue == "OPD")
            {
                for (int i = 0; i < gv_OPD.Rows.Count; i++)
                {
                    HiddenField hfheadId = (HiddenField)gv_OPD.Rows[i].FindControl("hfHeadOfExpenseId");
                    TextBox Date = (TextBox)gv_OPD.Rows[i].FindControl("txtDate");
                    TextBox Days = (TextBox)gv_OPD.Rows[i].FindControl("NumberOfdays");
                    TextBox Voucher = (TextBox)gv_OPD.Rows[i].FindControl("Voucher");
                    TextBox txtRent = (TextBox)gv_OPD.Rows[i].FindControl("txtRent");
                     
                    TextBox txt_RemaksforHR = (TextBox)gv_OPD.Rows[i].FindControl("txt_RemaksforHR");
                    CheckBox check = (CheckBox)gv_OPD.Rows[i].FindControl("valueCheck");

                   decimal dd = 0;
                try
                {
                    dd = Convert.ToDecimal(txtRent.Text);
                }
                catch (Exception)
                {
                    
                 //   throw;
                }

                if (dd>0)
                {
                    
                        ClaimDetailsDao DocA = new ClaimDetailsDao();
                        DocA.OIPDHeadOfExpenseId = Convert.ToInt32(hfheadId.Value.ToString());

                        if (Date.Text != "")
                        {
                            DocA.Dates = Convert.ToDateTime(Date.Text);
                        }

                        DocA.Numberofdays = int.Parse(Days.Text);

                        if (Voucher.Text != "")
                        {
                            DocA.SINoOfEncloseVoucher = Voucher.Text.Trim();
                        }

                        DocA.RemaksforHR = txt_RemaksforHR.Text.Trim();
                        DocA.ChildrenNo = 0;

                        if (txtRent.Text != "")
                        {
                            DocA.Rent = Convert.ToDecimal(txtRent.Text.ToString());
                            DocA.Amount = Convert.ToDecimal(txtRent.Text.ToString());
                        }

                        claimDetailslList.Add(DocA);
                    }

                }

            }


            else if (inlineRadio.SelectedValue == "Special")
            {
                for (int i = 0; i < gv_OPD.Rows.Count; i++)
                {
                    HiddenField hfheadId = (HiddenField)gv_OPD.Rows[i].FindControl("hfHeadOfExpenseId");
                    TextBox Date = (TextBox)gv_OPD.Rows[i].FindControl("txtDate");
                    TextBox Days = (TextBox)gv_OPD.Rows[i].FindControl("NumberOfdays");
                    TextBox Voucher = (TextBox)gv_OPD.Rows[i].FindControl("Voucher");
                    TextBox txtRent = (TextBox)gv_OPD.Rows[i].FindControl("txtRent");
                    TextBox txt_RemaksforHR = (TextBox)gv_OPD.Rows[i].FindControl("txt_RemaksforHR");
                    CheckBox check = (CheckBox)gv_OPD.Rows[i].FindControl("valueCheck");

                    decimal dd = 0;
                    try
                    {
                        dd = Convert.ToDecimal(txtRent.Text);
                    }
                    catch (Exception)
                    {

                        //   throw;
                    }

                    if (dd > 0)
                    {

                        ClaimDetailsDao DocA = new ClaimDetailsDao();
                        DocA.OIPDHeadOfExpenseId = Convert.ToInt32(hfheadId.Value.ToString());

                        if (Date.Text != "")
                        {
                            DocA.Dates = Convert.ToDateTime(Date.Text);
                        }

                        DocA.Numberofdays = int.Parse(Days.Text);

                        if (Voucher.Text != "")
                        {
                            DocA.SINoOfEncloseVoucher = Voucher.Text.Trim();
                        }
                        DocA.RemaksforHR = txt_RemaksforHR.Text.Trim();
                        if (txtRent.Text != "")
                        {
                            DocA.Rent = Convert.ToDecimal(txtRent.Text.ToString());
                            DocA.Amount = Convert.ToDecimal(txtRent.Text.ToString());
                        }
                        DocA.ChildrenNo = 0;

                        claimDetailslList.Add(DocA);
                    }

                }

            }

            else
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    HiddenField hfheadId = (HiddenField)GridView1.Rows[i].FindControl("hfHeadOfExpenseId");
                    TextBox Date = (TextBox)GridView1.Rows[i].FindControl("txtDate");
                    TextBox Days = (TextBox)GridView1.Rows[i].FindControl("NumberOfdays");
                    TextBox Voucher = (TextBox)GridView1.Rows[i].FindControl("Voucher");
                    TextBox Amount = (TextBox)GridView1.Rows[i].FindControl("Amount");
                    TextBox txtRent = (TextBox)GridView1.Rows[i].FindControl("txtRent");

                    TextBox txt_RemaksforHR = (TextBox)GridView1.Rows[i].FindControl("txt_RemaksforHR");
                    CheckBox check = (CheckBox)GridView1.Rows[i].FindControl("valueCheck");
                    TextBox txtChildrenNo = (TextBox)GridView1.Rows[i].FindControl("txtChildrenNo");
                    decimal dd = 0;
                    try
                    {
                        dd = Convert.ToDecimal(txtRent.Text);
                    }
                    catch (Exception)
                    {

                        //   throw;
                    }

                    if (dd > 0)
                    {
                        ClaimDetailsDao DocA = new ClaimDetailsDao();
                        DocA.OIPDHeadOfExpenseId = Convert.ToInt32(hfheadId.Value.ToString());

                        if (Date.Text != "")
                        {
                            DocA.Dates = Convert.ToDateTime(Date.Text);
                        }

                        DocA.Numberofdays = 1;

                        if (Days.Text != "")
                        {
                            DocA.Numberofdays = Convert.ToInt32(Days.Text.Trim());
                        }

                        if (Voucher.Text != "")
                        {
                            DocA.SINoOfEncloseVoucher = Voucher.Text.Trim();
                        }

                        if (Amount.Text != "")
                        {
                            DocA.Amount = Convert.ToDecimal(Amount.Text.ToString());
                        }
                        if (txtRent.Text != "")
                        {
                            DocA.Rent = Convert.ToDecimal(txtRent.Text.ToString());
                        }

                        try
                        {
                            DocA.ChildrenNo = Convert.ToInt32(txtChildrenNo.Text);
                        }
                        catch (Exception)
                        {
                            DocA.ChildrenNo = 0;
                            //   throw;
                        }

                        DocA.RemaksforHR = txt_RemaksforHR.Text.Trim();
                        claimDetailslList.Add(DocA);
                    }

                }

            }
            List<ReimbursmentDocument> DocAlist = new List<ReimbursmentDocument>();

            for (int i = 0; i < gv_DocumentUpload.Rows.Count; i++)
            {
                HiddenField hfDocumentLink = (HiddenField) gv_DocumentUpload.Rows[i].FindControl("hfDocumentLink");
                Label lbl_DocumentNote = (Label) gv_DocumentUpload.Rows[i].FindControl("lbl_DocumentNote");
                HiddenField hfFileName = (HiddenField) gv_DocumentUpload.Rows[i].FindControl("hfFileName");

                ReimbursmentDocument DocA = new ReimbursmentDocument();

                DocA.DocumentLink = hfDocumentLink.Value.ToString();
                DocA.FileName = hfFileName.Value.ToString();
                DocA.DocumentNote = lbl_DocumentNote.Text.Trim();
                DocA.ReimbursmentDocumentId = 0;
                DocA.ReimbursFromMasterId = 0;

                DocAlist.Add(DocA);
            }

            List<ReimbursmentFromEmpListDao> empList = new List<ReimbursmentFromEmpListDao>();
            for (int i = 0; i < gv_Member.Rows.Count; i++)
            {
                HiddenField empId = (HiddenField) gv_Member.Rows[i].FindControl("MemEmpInfoId");
                if (empId.Value != "")
                {
                    ReimbursmentFromEmpListDao aListDao = new ReimbursmentFromEmpListDao();
                    aListDao.ReimbursmentEmpListId = 0;
                    aListDao.EmpInfoId = string.IsNullOrEmpty(empId.Value) ? 0 : Convert.ToInt32(empId.Value);
                    empList.Add(aListDao);
                }
               
            }

            Int32 rei = formDal.data_save(aMaster, discriptionlist, Ticklist, claimDetailslList, DocAlist, empList, HFEntryBy.Value, hfMode.Value);

            if (rei > 0)
            {
                if (Action == "Verified")
                {
                    if (hfMasterId.Value == "")
                    {
                        ApprovalLog(rei);
                    }

                    if (HFEntryBy.Value == Session["UserId"].ToString())
                    {
                        ApprovalLog(rei);
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfully Done...');window.location ='HRPanel.aspx';",
                    true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Something went wrong...');",
                    true);
            }

        }
    }


    private void ApprovalLog(int MasterId)
    {
        DataTable dtempdata = formDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");

        DataTable CheckFinalApproval = formDal.CheckFinalApprovalCondition(ddlCompany.SelectedValue, Session["EmpInfoId"].ToString());


        bool result = false;

        if (CheckFinalApproval.Rows.Count > 0)
        {


            if (CheckFinalApproval.Rows[0]["IsAllEmployee"].ToString() == "True")
            {

                       ReimbursementSelfAppLogDAO appLogDao = new ReimbursementSelfAppLogDAO();

                        appLogDao.ActionStatus = "Drafted";
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                        appLogDao.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                        appLogDao.ReimbursFromMasterId = Convert.ToInt32(MasterId);
                       // appLogDao.Comments = txt_Comments.Text;
                        appLogDao.CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString());

                        int idd = formDal.SaveEmpAppLog(appLogDao);

                        ReimbursementSelfAppLogDAO aMastera = new ReimbursementSelfAppLogDAO();
                        aMastera.ReimbursFromMasterId
                            = Convert.ToInt32(MasterId);
                        aMastera.ActionStatus = "Verified";
                        bool status = formDal.UpdateContractural(aMastera);
                        ReimbursementSelfAppLogDAO appLogDao1 = new ReimbursementSelfAppLogDAO()
                        {
                            ActionStatus = "Verified",
                            ApproveDate = DateTime.Now,
                            ApproveBy = Session["UserId"].ToString(),
                            PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                            ForEmpInfoId = Convert.ToInt32(CheckFinalApproval.Rows[0]["EmpInfoId"].ToString()),
                            ReimbursFromMasterId = Convert.ToInt32(MasterId),
                           // Comments = txt_Comments.Text,
                            CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                        };
                        int id = formDal.SaveEmpAppLog(appLogDao1);
                    
                
            }
            else if (dtempdata.Rows[0]["ReportingEmpId"] != null)
            {
                
                    //int pk = _appPartA.SaveAppraisalSelfMaster(aMaster, Session["UserId"].ToString());
                    //if (pk > 0)
                    //{

                ReimbursementSelfAppLogDAO appLogDao = new ReimbursementSelfAppLogDAO();

                        appLogDao.ActionStatus = "Drafted";
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                        appLogDao.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                        appLogDao.ReimbursFromMasterId = Convert.ToInt32(MasterId);
                       // appLogDao.Comments = txt_Comments.Text;
                        appLogDao.CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString());


                        int idd = formDal.SaveEmpAppLog(appLogDao);


                        ReimbursementSelfAppLogDAO aMastera = new ReimbursementSelfAppLogDAO();
                        aMastera.ReimbursFromMasterId
                            = Convert.ToInt32(MasterId);
                        aMastera.ActionStatus = "Verified";
                        bool status = formDal.UpdateContractural(aMastera);
                        ReimbursementSelfAppLogDAO appLogDao1 = new ReimbursementSelfAppLogDAO()
                        {
                            ActionStatus = "Verified",
                            ApproveDate = DateTime.Now,
                            ApproveBy = Session["UserId"].ToString(),
                            PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                            ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString()),
                            ReimbursFromMasterId = Convert.ToInt32(MasterId),
                           // Comments = txt_Comments.Text,
                            CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                        };
                        int id = formDal.SaveEmpAppLog(appLogDao1);

                  //  }

            }
            else
            {
              //  AlertMessageBoxShow("Your Reporting Employee is not set yet!!!");
            }

        }
    }




    //protected void gv_DocumentUpload_PreRender(object sender, EventArgs e)
    //{
    //    GridView gv = (GridView)sender;

    //    if ((gv.ShowHeader == true && gv.Rows.Count > 0) || (gv.ShowHeaderWhenEmpty == true))
    //    {
    //        //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
    //        gv.HeaderRow.TableSection = TableRowSection.TableHeader;
    //        gv.ShowHeader.ToString();
    //    }


    protected void btnRemove_OnClick(object sender, EventArgs e)
    {

        CheckBox lb = (CheckBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        CheckBox yesBox = (CheckBox)loadGridView.Rows[rowID].FindControl("Yes");
        CheckBox No = (CheckBox)loadGridView.Rows[rowID].FindControl("No");
        TextBox Date = (TextBox)loadGridView.Rows[rowID].FindControl("Date");
        if (inlineRadio.SelectedValue == "IPD")
        {
            Date.Enabled = true;
            Date.Text = "";

             bool yesCheck = yesBox.Checked;

            if (yesCheck)
            {
                No.Checked = false;
            }


        }

        else
        {
       

            bool yesCheck = yesBox.Checked;

            if (yesCheck)
            {
                No.Checked = false;
                Date.Enabled = false;
                Date.Text = "";

            }
        }

    }


    protected void NoCheck_OnChanged(object sender, EventArgs e)
    {

        CheckBox lb = (CheckBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        CheckBox yesBox = (CheckBox)loadGridView.Rows[rowID].FindControl("Yes");
        CheckBox No = (CheckBox)loadGridView.Rows[rowID].FindControl("No");
        TextBox Date = (TextBox)loadGridView.Rows[rowID].FindControl("Date");
        if (inlineRadio.SelectedValue == "IPD")
        {
            Date.Enabled = false;
            Date.Text = "";
             bool NoCheck = No.Checked;

            if (NoCheck)
            {
                yesBox.Checked = false;
            }
        }

        else
        {

            bool NoCheck = No.Checked;

            if (NoCheck)
            {
                yesBox.Checked = false;

                Date.Enabled = false;
                Date.Text = "";
            }
        }

    }

    protected void Amount_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        var datKey = GridView1.DataKeys[rowID];
        if (datKey != null)
        {


            decimal amount = 0;

            bool IsIncrement = false;
            try
            {
                amount = Convert.ToDecimal(GridView1.DataKeys[rowID][0].ToString());

                IsIncrement = Convert.ToBoolean(GridView1.DataKeys[rowID][1].ToString());
            }
            catch (Exception exception)
            {
                //
            }


            TextBox amountBox = (TextBox)GridView1.Rows[rowID].FindControl("Amount");

            HiddenField masterId = (HiddenField)GridView1.Rows[rowID].FindControl("hfHeadOfExpenseId");


            try
            {

                if (amount != 0 && IsIncrement == false)
                {
                    if (Convert.ToDecimal(amountBox.Text) <= amount)
                    {

                    }
                    else
                    {
                        if (Convert.ToInt32(masterId.Value) != 16)
                        {
                            amountBox.Text = amount.ToString();
                            aShowMessage.ShowMessageBox("Amount must be less then equal " + amount, this);
                        }

                    }

                }

            }
            catch (Exception)
            {

            }


        }
        decimal markTotal = 0;
        GettotalMark(markTotal);

    }


    //Employee list 


    private void LoadInitialgv_Member_Save()
    {
        DataTable dtDetails = new DataTable();

        dtDetails.Columns.Add("Type");

        dtDetails.Columns.Add("EmpMasterCode");
        dtDetails.Columns.Add("EmpInfoId");
        dtDetails.Columns.Add("EmpName");
        dtDetails.Columns.Add("Designation");

        DataRow dr = null;
        dr = dtDetails.NewRow();

        dr["Type"] = "";

        dr["EmpMasterCode"] = "";
        dr["EmpInfoId"] = "";
        dr["EmpName"] = "";
        dr["Designation"] = "";

        dtDetails.Rows.Add(dr);

        gv_Member.DataSource = null;
        gv_Member.DataBind();
        gv_Member.DataSource = dtDetails;
        gv_Member.DataBind();

        for (int i = 0; i < gv_Member.Rows.Count; i++)
        {
            DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[i].FindControl("ddlEmpName"));
            using (DataTable dt = _commonDataLoad.GetEmpDDLNewMeetinig())
            {
                ddlEmpName.DataSource = dt;
                ddlEmpName.DataValueField = "EmpInfoId";
                ddlEmpName.DataTextField = "EmpName";
                ddlEmpName.DataBind();
                ddlEmpName.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpName.SelectedIndex = 0;

            }
        }

    }


    protected void rbType_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        int rowIndex = ((GridViewRow)(((RadioButtonList)sender).Parent.Parent)).RowIndex;

        RadioButtonList rbType = ((RadioButtonList)gv_Member.Rows[rowIndex].FindControl("rbType"));

        HiddenField hfType = ((HiddenField)gv_Member.Rows[rowIndex].FindControl("hfType"));

        DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[rowIndex].FindControl("ddlEmpName"));
        TextBox txt_EmpName = ((TextBox)gv_Member.Rows[rowIndex].FindControl("txt_EmpName"));

        TextBox txt_EmpMasterCode = ((TextBox)gv_Member.Rows[rowIndex].FindControl("txt_EmpMasterCode"));
        HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[rowIndex].FindControl("MemEmpInfoId"));
        TextBox txt_Designation = ((TextBox)gv_Member.Rows[rowIndex].FindControl("txt_Designation"));

        hfType.Value = rbType.SelectedValue;

        if (rbType.SelectedValue == "Employee")
        {
            ddlEmpName.Visible = true;
            //txt_EmpName.Visible = false;

            //txt_EmpName.Text = "";

            using (DataTable dt = _commonDataLoad.GetEmpDDLNewMeetinig())
            {

                ddlEmpName.DataSource = dt;
                ddlEmpName.DataValueField = "EmpInfoId";
                ddlEmpName.DataTextField = "EmpName";
                ddlEmpName.DataBind();
                ddlEmpName.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpName.SelectedIndex = 0;

            }
        }

        //if (rbType.SelectedValue == "Guest")
        //{
        //    ddlEmpName.Visible = false;
        //    txt_EmpName.Visible = true;
        //    ddlEmpName.SelectedValue = "";
        //    txt_EmpName.Text = "";
        //    txt_EmpMasterCode.Text = "";
        //    MemEmpInfoId.Value = "";
        //    txt_Designation.Text = "";

        //    ddlEmpName.Items.Clear();
        //}

    }


    protected void ddlEmpName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((DropDownList)sender).Parent.Parent)).RowIndex;

        TextBox txt_EmpName = ((TextBox)gv_Member.Rows[rowIndex].FindControl("txt_EmpName"));
        TextBox txt_EmpMasterCode = ((TextBox)gv_Member.Rows[rowIndex].FindControl("txt_EmpMasterCode"));
        HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[rowIndex].FindControl("MemEmpInfoId"));
        TextBox txt_Designation = ((TextBox)gv_Member.Rows[rowIndex].FindControl("txt_Designation"));
        DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[rowIndex].FindControl("ddlEmpName"));

        if (ddlEmpName.SelectedValue != "")
        {
            int mid = Convert.ToInt32(ddlEmpName.SelectedValue);
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
        else
        {
            txt_EmpMasterCode.Text = "";
            MemEmpInfoId.Value = "";
            txt_Designation.Text = "";
        }
    }


    protected void btn_gv_MemberAdd_OnClick(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;

        DataTable aTable = new DataTable();

        aTable.Columns.Add("Type");
        aTable.Columns.Add("EmpMasterCode");
        aTable.Columns.Add("EmpInfoId");
        aTable.Columns.Add("EmpName");
        aTable.Columns.Add("Designation");

        DataRow dr;


        for (int i = 0; i < gv_Member.Rows.Count; i++)
        {
            dr = aTable.NewRow();
            HiddenField hfType = ((HiddenField)gv_Member.Rows[i].FindControl("hfType"));
            TextBox txt_EmpMasterCode = ((TextBox)gv_Member.Rows[i].FindControl("txt_EmpMasterCode"));
            HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[i].FindControl("MemEmpInfoId"));
            TextBox txt_EmpName = (TextBox)gv_Member.Rows[i].FindControl("txt_EmpName");
            TextBox txt_Designation = (TextBox)gv_Member.Rows[i].FindControl("txt_Designation");


            RadioButtonList rbType = ((RadioButtonList)gv_Member.Rows[i].FindControl("rbType"));

            dr["EmpInfoId"] = MemEmpInfoId.Value;

            dr["EmpMasterCode"] = txt_EmpMasterCode.Text;
            dr["EmpName"] = txt_EmpName.Text;
            dr["Designation"] = txt_Designation.Text;


            dr["Type"] = hfType.Value.Trim();
            if (dr["Type"].ToString() != "")
            {

                dr["Type"] = rbType.SelectedValue;
            }


            aTable.Rows.Add(dr);

            if (rowIndex == i)
            {
                dr = aTable.NewRow();


                dr["EmpInfoId"] = "";

                dr["EmpMasterCode"] = "";
                dr["EmpName"] = "";
                dr["Designation"] = "";

                dr["Type"] = hfType.Value.Trim();
                if (dr["Type"].ToString() != "")
                {

                    rbType.SelectedValue = hfType.Value.Trim();
                }


                aTable.Rows.Add(dr);
            }
        }

        //Session["table"] = (DataTable)aTable;
        gv_Member.DataSource = null;
        gv_Member.DataBind();
        gv_Member.DataSource = aTable;
        ViewState["gv_Member_List"] = aTable;

        gv_Member.DataBind();


        for (int i = 0; i < gv_Member.Rows.Count; i++)
        {
            DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[i].FindControl("ddlEmpName"));
            using (DataTable dt = _commonDataLoad.GetEmpDDLNewMeetinig())
            {



                ddlEmpName.DataSource = dt;
                ddlEmpName.DataValueField = "EmpInfoId";
                ddlEmpName.DataTextField = "EmpName";
                ddlEmpName.DataBind();
                ddlEmpName.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpName.SelectedIndex = 0;

            }
        }

        for (int i = 0; i < gv_Member.Rows.Count; i++)
        {
            RadioButtonList rbType = ((RadioButtonList)gv_Member.Rows[i].FindControl("rbType"));

            rbType.SelectedValue = ((HiddenField)gv_Member.Rows[i].FindControl("hfType"))
                  .Value;
            HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[i].FindControl("MemEmpInfoId"));
            DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[i].FindControl("ddlEmpName"));
            TextBox txt_EmpName = (TextBox)gv_Member.Rows[i].FindControl("txt_EmpName");


            

                ddlEmpName.Visible = true;
                txt_EmpName.Visible = false;


                ddlEmpName.SelectedValue = MemEmpInfoId.Value;
             
 




        }

    }

    protected void btn_gv_Member_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["gv_Member_List"] != null)
        {
            DataTable dt = (DataTable)ViewState["gv_Member_List"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["gv_Member_List"] = dt;
                //Re bind the GridView for the updated data  
                gv_Member.DataSource = dt;
                gv_Member.DataBind();

                for (int i = 0; i < gv_Member.Rows.Count; i++)
                {
                    DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[i].FindControl("ddlEmpName"));
                    using (DataTable dt22 = _commonDataLoad.GetEmpDDLNewMeetinig())
                    {

                        ddlEmpName.DataSource = dt22;
                        ddlEmpName.DataValueField = "EmpInfoId";
                        ddlEmpName.DataTextField = "EmpName";
                        ddlEmpName.DataBind();
                        ddlEmpName.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                        ddlEmpName.SelectedIndex = 0;

                    }
                }

                for (int i = 0; i < gv_Member.Rows.Count; i++)
                {
                    RadioButtonList rbType = ((RadioButtonList)gv_Member.Rows[i].FindControl("rbType"));

                    rbType.SelectedValue = ((HiddenField)gv_Member.Rows[i].FindControl("hfType"))
                          .Value;
                    HiddenField MemEmpInfoId = ((HiddenField)gv_Member.Rows[i].FindControl("MemEmpInfoId"));
                    DropDownList ddlEmpName = ((DropDownList)gv_Member.Rows[i].FindControl("ddlEmpName"));
                    TextBox txt_EmpName = (TextBox)gv_Member.Rows[i].FindControl("txt_EmpName");


                   

                        ddlEmpName.Visible = true;
                        txt_EmpName.Visible = false;

                        ddlEmpName.SelectedValue = MemEmpInfoId.Value;
                    
                }

            }
            else
            {
                ViewState["gv_Member_List"] = null;
                //Re bind the GridView for the updated data  
                gv_Member.DataSource = null;
                gv_Member.DataBind();
            }
        }

    }


    protected void drrft_OnClick(object sender, EventArgs e)
    {
        string Action = "Draft";

        Submit(Action);
    }

    protected void chkSelect_OnCheckedChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox chkSelect = ((CheckBox)gv_EmpListSearch.Rows[rowIndex].FindControl("chkSelect"));
        Label Family = (Label)gv_EmpListSearch.Rows[rowIndex].FindControl("lbl_Family");
        string age =  gv_EmpListSearch.DataKeys[rowIndex].Values[1].ToString();
        for (int i = 0; i < gv_EmpListSearch.Rows.Count; i++)
        {
            CheckBox chkCount = ((CheckBox)gv_EmpListSearch.Rows[i].FindControl("chkSelect"));

            if (rowIndex == i)
            {
                if (chkSelect.Checked)
                {
                    //if (Family.Text != "")
                    //{
                    string empName = Family.Text.Trim();

                    if (empName != "")
                    {
                        string[] emp = empName.Split('-');
                        NameofPatient.Text = "";
                        Relationship.Text = "";

                            if (emp[0].Trim() == "Children")
                            {
                                DataTable Dt = formDal.Get_Employee_ChildrenCount(gv_EmpListSearch.DataKeys[rowIndex][0].ToString(), ddlFinancialYear.SelectedValue);
                              
                                if (Dt.Rows.Count <= 2)
                                {
                                    NameofPatient.Text = emp[1].Trim();
                                    Relationship.Text = emp[0].Trim();
                                    Age.Text =  age;
                                    NameofPatient.Attributes.Add("readonly", "readonly");
                                    Relationship.Attributes.Add("readonly", "readonly");
                                }
                                else
                                {
                                    aShowMessage.ShowMessageBox("You already applied  for two children", this);
                                }
                            }
                            else
                            {
                                NameofPatient.Text = emp[1].Trim();
                                Relationship.Text = emp[0].Trim();
                                Age.Text = age;
                                NameofPatient.Attributes.Add("readonly", "readonly");
                                Relationship.Attributes.Add("readonly", "readonly");
                            }


                            Age.Attributes.Add("readonly", "readonly");

                            //if (age != "")
                            //{ 
                            //    Age.Attributes.Add("readonly", "readonly");
                            //}
                            //else
                            //{
                                
                            //    Age.Attributes.Remove("readonly");
                            //}

                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "$('#exampleModal2').modal('show')", true);
                    }
                    else
                    {
                        NameofPatient.Text = "";
                        Relationship.Text = "";
                    }

                }

                chkCount.Checked = true;
            }
            else
            {
                chkCount.Checked = false;
            }
        }

    }

    private void loadFamilyMember(int empid)
    {

        DataTable dt = formDal.Get_FamilyMemberById(empid);
        gv_EmpListSearch.DataSource = dt;
        gv_EmpListSearch.DataBind();

        for (int i = 0; i < gv_EmpListSearch.Rows.Count; i++)
        {
            Label Family = (Label)gv_EmpListSearch.Rows[i].FindControl("lbl_Family");

            if (Family.Text == "")
            {
                gv_EmpListSearch.Rows[i].Visible = false;

            }
        }

    }

    protected void ddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Remainingbalance();
    }

    protected void Date_OnTextChanged(object sender, EventArgs e)
    {

        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        TextBox date = (TextBox)GridView1.Rows[rowID].FindControl("txtDate");
        TextBox numberofDay = (TextBox)GridView1.Rows[rowID].FindControl("NumberOfdays");

        //If Application type isn't Special
        if (inlineRadio.SelectedValue != "Special")
        {
            if (inlineRadio.SelectedValue != "")
            {
                //Submitted date not be null 
                if (SubmitDate.Text != "")
                {
                    if (date.Text != "")
                    {
                        //Day calculation between to date 
                        DateTime date1 = Convert.ToDateTime(SubmitDate.Text);

                        DateTime date2 = Convert.ToDateTime(date.Text);

                        TimeSpan value = date1.Subtract(date2);

                        if (value.Days < 0)
                        {
                            date.Text = "";
                         //   numberofDay.Text = "";
                            aShowMessage.ShowMessageBox("Invalid Date times", this);
                        }
                        else
                        {
                            //Claim of OPD BILL to be submitted within 30 days 
                            if (inlineRadio.SelectedValue == "OPD")
                            {
                                if (value.Days <= 30)
                                {
                                   // numberofDay.Text = value.Days.ToString();
                                }
                                else
                                {
                                    date.Text = "";
                                 //   numberofDay.Text = "";
                                    aShowMessage.ShowMessageBox("Document Submitted withing 30 days", this);
                                }
                            }
                            //Claim of IPD BILL to be submitted within 30 days
                            if (inlineRadio.SelectedValue == "IPD")
                            {
                                if (value.Days <= 15)
                                {
                                   // numberofDay.Text = value.Days.ToString();
                                }
                                else
                                {
                                    date.Text = "";
                                   // numberofDay.Text = "";
                                    aShowMessage.ShowMessageBox("Document Submitted withing 15 days", this);
                                }
                            }

                        }
                    }


                }
                else
                {
                    date.Text = "";
                  //  numberofDay.Text = "";
                    aShowMessage.ShowMessageBox("Please Select Submit Date", this);
                }
            }
            else
            {
                date.Text = "";
             //   numberofDay.Text = "";
                aShowMessage.ShowMessageBox("Please Select Application Type", this);
            }
        }
        else
        {

            if (SubmitDate.Text != "")
            {
                DateTime date1 = Convert.ToDateTime(SubmitDate.Text);

                DateTime date2 = Convert.ToDateTime(date.Text);

                TimeSpan value = date1.Subtract(date2);

                if (value.Days < 0)
                {
                    date.Text = "";
                 //   numberofDay.Text = "";
                    aShowMessage.ShowMessageBox("Invalid Date times", this);
                }
                else
                {
                   // numberofDay.Text = value.Days.ToString();
                }
            }
            else
            {
                date.Text = "";
              //  numberofDay.Text = "";
                aShowMessage.ShowMessageBox("Please Select Submit Date", this);
            }
                 
        }

        ApplyCaesareanDeliveryExclusiveRule();
        decimal markTotal = 0;
        GettotalMark(markTotal);

    }

    protected void Voucher_OnTextChanged(object sender, EventArgs e)
    {
        ApplyCaesareanDeliveryExclusiveRule();
        decimal markTotal = 0;
        GettotalMark(markTotal);
    }

    protected void inlineRadio_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        divHospital.Visible = false;
        divHospitalAdmissionDischargeDate.Visible = false;
        divHospitalDischargeDate.Visible = false;
        GridView1.DataSource = null;
        GridView1.DataBind();

        gv_OPD.DataSource = null;
        gv_OPD.DataBind();
        if (inlineRadio.SelectedValue == "IPD")
        {
            Remainingbalance();

            if (ddlCompany.SelectedValue != "")
            {
                load_IPD_OPD(0, ddlCompany.SelectedValue, hfEmpID.Value);
            }
            divHospital.Visible = true;
            divHospitalAdmissionDischargeDate.Visible = true;
            divHospitalDischargeDate.Visible = true;
        }

        if (inlineRadio.SelectedValue == "Special")
        {
            Remainingbalance();

            if (ddlCompany.SelectedValue != "")
            {
                load_OPD(1, ddlCompany.SelectedValue);
            }

            RemainingBalance.Text = "0";

        }

        if (inlineRadio.SelectedValue == "OPD")
        {
            Remainingbalance();
            if (ddlCompany.SelectedValue != "")
            {
                load_OPD(1, ddlCompany.SelectedValue);
            }
        }

        if (inlineRadio.SelectedValue != "IPD")
        {
            txtHospitalName.Text = string.Empty;
            if (ddlHospitalName.Items.Count > 0)
            {
                ddlHospitalName.SelectedIndex = 0;
            }
            txtHospitalAdmissionDate.Text = string.Empty;
            txtHospitalDischargeDate.Text = string.Empty;
        }

        

        //if (inlineRadio.SelectedValue == "IPD")
        //{
        //    for (int i = 0; i < loadGridView.Rows.Count; i++)
        //    {
        //        CheckBox Yes = (CheckBox)loadGridView.Rows[i].FindControl("Yes");
        //        TextBox Date = (TextBox)loadGridView.Rows[i].FindControl("Date");
        //        if (Yes.Checked)
        //        {
        //            Date.Enabled = true;
                    
        //        }
        //            Date.Text = "";
                 
        //    }

        //}
        //else
        //{
        //    {
        //        for (int i = 0; i < loadGridView.Rows.Count; i++)
        //        {
        //            TextBox Date = (TextBox)loadGridView.Rows[i].FindControl("Date");
                   
        //                Date.Enabled = false;
        //                Date.Text = "";
                    
        //        }

        //    }
        //}



        if (inlineRadio.SelectedValue == "IPD")
        {
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                CheckBox Yes = (CheckBox)loadGridView.Rows[i].FindControl("Yes");
                TextBox Date = (TextBox)loadGridView.Rows[i].FindControl("Date");
                Label Description = (Label)loadGridView.Rows[i].FindControl("Description");

                if (Description.Text.Trim() == "Have you consulted with Company Doctor about the sickness/treatment?")
                {

                    if (Yes.Checked)
                    {
                        Date.Enabled = true;
                        Date.Text = "";
                    }
                   
                }


                if (Description.Text.Trim() == "Have you taken the measure(s) as per the advice of Company Doctor?")
                {

                }


                if (Description.Text.Trim() == "When did you inform Concerened HRD/Medical SupportCommittee for hospitalization?")
                {
                    Date.Enabled = true;
                    Date.Text = "";
                }

            }

        }
        else
        {
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                CheckBox Yes = (CheckBox)loadGridView.Rows[i].FindControl("Yes");
                TextBox Date = (TextBox)loadGridView.Rows[i].FindControl("Date");
                Label Description = (Label)loadGridView.Rows[i].FindControl("Description");

                if (Description.Text.Trim() == "Have you consulted with Company Doctor about the sickness/treatment?")
                {

                    if (Yes.Checked)
                    {
                        Date.Enabled = false;
                        Date.Text = "";
                    }

                }


                if (Description.Text.Trim() == "Have you taken the measure(s) as per the advice of Company Doctor?")
                {

                }


                if (Description.Text.Trim() == "When did you inform Concerened HRD/Medical SupportCommittee for hospitalization?")
                {
                    Date.Enabled = false;
                    Date.Text = "";
                }

            }
        }


    }

    //protected void valueCheck_OnCheckedChanged(object sender, EventArgs e)
    //{
    //    decimal markTotal = 0;
    //    GettotalMark(markTotal);
    //}

    protected void txtRent_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;


        TextBox txtChildrenNo = (TextBox)GridView1.Rows[rowID].FindControl("txtChildrenNo");
        TextBox txtRent = (TextBox)GridView1.Rows[rowID].FindControl("txtRent");
        Label HeadOfExpense = (Label)GridView1.Rows[rowID].FindControl("HeadOfExpense");


        TextBox Amount = (TextBox)GridView1.Rows[rowID].FindControl("Amounttt");
        TextBox numberofDay = (TextBox)GridView1.Rows[rowID].FindControl("NumberOfdays");
        TextBox TotalAmount = (TextBox)GridView1.Rows[rowID].FindControl("Amount");


        if (HeadOfExpense.Text.Trim() == "Caesarean Delivery" || HeadOfExpense.Text.Trim() == "Normal Delivery")
        {
            int noofChild = 0;
            try
            {
                noofChild = Convert.ToInt32(txtChildrenNo.Text);
            }
            catch (Exception)
            {
                //txtRent.Text = 0.ToString();
                //txtRent.ReadOnly = true;
                //numberofDay.Text = 1.ToString();
                ////Amount.Text = 0.ToString();
                //TotalAmount.Text = 0.ToString();
                txtChildrenNo.Text = 0.ToString();

                //throw;
            }
            if (noofChild >= 3)
            {
                txtRent.Text = 0.ToString();
                txtRent.ReadOnly = true;
                numberofDay.Text = 1.ToString();
                //Amount.Text = 0.ToString();
                TotalAmount.Text = 0.ToString();
                aShowMessage.ShowMessageBox("You can not claim this!", this);
            }
            else
            {
                //   txtRent.Text = 0.ToString();
                txtRent.ReadOnly = false;

            }
        }

        ApplyCaesareanDeliveryExclusiveRule();
        ClaimCalculation(rowID);
    }


    private void ClaimCalculation(int RowId)
    {
        decimal Amunt = 0;

        int FixedAmunt = 0;

        TextBox Amount = (TextBox)GridView1.Rows[RowId].FindControl("Amounttt");
        TextBox numberofDay = (TextBox)GridView1.Rows[RowId].FindControl("NumberOfdays");
        TextBox txtRent = (TextBox)GridView1.Rows[RowId].FindControl("txtRent");
        TextBox TotalAmount = (TextBox)GridView1.Rows[RowId].FindControl("Amount");
        Label HeadOfExpense = (Label)GridView1.Rows[RowId].FindControl("HeadOfExpense");

        FixedAmunt = Convert.ToInt32(Amount.Text);

        if (numberofDay.Text != "" && txtRent.Text != "")
        {
            switch (FixedAmunt)
            {
                case 0:
                    Amunt = Convert.ToInt32(numberofDay.Text) * Convert.ToDecimal(txtRent.Text);
                    TotalAmount.Text = Amunt.ToString();
                    break;
                default:

                    Amunt = Convert.ToInt32(numberofDay.Text) * Convert.ToDecimal(txtRent.Text);
                    if (HeadOfExpense.Text.Trim() == "Daily Hospital Rent")
                    {
                        decimal rentt = Convert.ToDecimal(txtRent.Text);
                        decimal mRent = Convert.ToDecimal(Amount.Text);

                        if (mRent < rentt)
                        {
                            txtRent.Text = "";
                            TotalAmount.Text = "";
                            numberofDay.Text = "1";
                            aShowMessage.ShowMessageBox("Can not Cross Max limit", this);
                        }
                        else
                        {
                            Amunt = Convert.ToInt32(numberofDay.Text) * Convert.ToDecimal(txtRent.Text);

                            TotalAmount.Text = Amunt.ToString();
                        }
                    }

                    else
                    {
                        if (Convert.ToDecimal(Amount.Text) < Convert.ToDecimal(Amunt))
                        {
                            txtRent.Text = "";
                            TotalAmount.Text = "";
                            numberofDay.Text = "1";
                            aShowMessage.ShowMessageBox("Can not Cross Max limit", this);
                        }
                        else
                        {
                            Amunt = Convert.ToInt32(numberofDay.Text) * Convert.ToDecimal(txtRent.Text);

                            TotalAmount.Text = Amunt.ToString();
                        }
                    }

                    break;
            }
        }

        decimal markTotal = 0;
        GettotalMark(markTotal);
    }


    private void ClaimCalculation_OPD(int RowId)
    {
        TextBox Amounttt = (TextBox)gv_OPD.Rows[RowId].FindControl("Amounttt");
        TextBox txtRent = (TextBox)gv_OPD.Rows[RowId].FindControl("txtRent");

        // ---- Safe decimal parsing ----
        decimal amount = 0;
        decimal rent = 0;

        // Amounttt empty বা invalid হলে amount = 0 থাকবে
        decimal.TryParse((Amounttt.Text ?? "").Trim(), out amount);

        // txtRent empty বা invalid হলে rent = 0 থাকবে
        decimal.TryParse((txtRent.Text ?? "").Trim(), out rent);

        // --------------------------------
        // এখন comparison করার আগে condition দেই
        // --------------------------------
        if (!inlineRadio.Items[2].Selected)
        {
            // শুধু তখনই check করবো যখন rent > 0
            if (rent > 0 && amount < rent)
            {
                // txtRent.Text = "";
                // aShowMessage.ShowMessageBox("Can not Cross Max limit", this);

                // চাইলে এখানে rent কে max limit এ set করতে পারো:
                // txtRent.Text = amount.ToString("0.##");
            }
        }

        decimal markTotal = 0;
        GettotalMark_OPD(markTotal);
    }


    protected void NumberOfdays_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        ClaimCalculation(rowID);
    }

    protected void btnBankDtls_OnClick(object sender, EventArgs e)
    {
        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('../UserSetup/EmpSalaryInfoForEmployee.aspx?mid=" + ddlForwordEmp.SelectedValue + "' ,'_blank');", true);
    }

    protected void btnReload_OnClick(object sender, EventArgs e)
    {
        try
        {
            DataTable dtEmp = formDal.GetEmployeeDetails(Convert.ToInt32(ddlForwordEmp.SelectedValue));
            if (dtEmp.Rows.Count > 0)
            {

                //lblBankAccountNo.Text = dtEmp.Rows[0]["BankAccountNo"].ToString().Trim();
                //lblBankName.Text = dtEmp.Rows[0]["BankName"].ToString().Trim();
                //lblBranchName.Text = dtEmp.Rows[0]["BranchName"].ToString().Trim();
                //lblRoutingNo.Text = dtEmp.Rows[0]["RoutingNo"].ToString().Trim();

            }
        }
        catch
        {
            
        }}

    protected void txtRentOPD_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        ClaimCalculation_OPD(rowID);
    }

    protected void OPDDate_OnTextChanged(object sender, EventArgs e)
    {

        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        TextBox date = (TextBox)gv_OPD.Rows[rowID].FindControl("txtDate");
        TextBox numberofDay = (TextBox)gv_OPD.Rows[rowID].FindControl("NumberOfdays");

        //If Application type isn't Special
        if (inlineRadio.SelectedValue != "Special")
        {
            if (inlineRadio.SelectedValue != "")
            {
                //Submitted date not be null 
                if (SubmitDate.Text != "")
                {
                    if (date.Text != "")
                    {
                        //Day calculation between to date 
                        DateTime date1 = Convert.ToDateTime(SubmitDate.Text);

                        DateTime date2 = Convert.ToDateTime(date.Text);

                        TimeSpan value = date1.Subtract(date2);

                        if (value.Days < 0)
                        {
                            date.Text = "";
                            //   numberofDay.Text = "";
                            aShowMessage.ShowMessageBox("Invalid Date times", this);
                        }
                        else
                        {
                            //Claim of OPD BILL to be submitted within 30 days 
                            if (inlineRadio.SelectedValue == "OPD")
                            {
                                if (value.Days <= 30)
                                {
                                    // numberofDay.Text = value.Days.ToString();
                                }
                                else
                                {
                                    date.Text = "";
                                    //   numberofDay.Text = "";
                                    aShowMessage.ShowMessageBox("Document Submitted withing 30 days", this);
                                }
                            }
                            //Claim of IPD BILL to be submitted within 30 days
                            if (inlineRadio.SelectedValue == "IPD")
                            {
                                if (value.Days <= 15)
                                {
                                    // numberofDay.Text = value.Days.ToString();
                                }
                                else
                                {
                                    date.Text = "";
                                    // numberofDay.Text = "";
                                    aShowMessage.ShowMessageBox("Document Submitted withing 15 days", this);
                                }
                            }

                        }
                    }


                }
                else
                {
                    date.Text = "";
                    //  numberofDay.Text = "";
                    aShowMessage.ShowMessageBox("Please Select Submit Date", this);
                }
            }
            else
            {
                date.Text = "";
                //   numberofDay.Text = "";
                aShowMessage.ShowMessageBox("Please Select Application Type", this);
            }
        }
        else
        {

            if (SubmitDate.Text != "")
            {
                DateTime date1 = Convert.ToDateTime(SubmitDate.Text);

                DateTime date2 = Convert.ToDateTime(date.Text);

                TimeSpan value = date1.Subtract(date2);

                if (value.Days < 0)
                {
                    date.Text = "";
                    //   numberofDay.Text = "";
                    aShowMessage.ShowMessageBox("Invalid Date times", this);
                }
                else
                {
                    // numberofDay.Text = value.Days.ToString();
                }
            }
            else
            {
                date.Text = "";
                //  numberofDay.Text = "";
                aShowMessage.ShowMessageBox("Please Select Submit Date", this);
            }

        }

    }

    protected void txtChildrenNo_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        TextBox txtChildrenNo = (TextBox)GridView1.Rows[rowID].FindControl("txtChildrenNo");
        TextBox txtRent = (TextBox)GridView1.Rows[rowID].FindControl("txtRent");
        Label HeadOfExpense = (Label)GridView1.Rows[rowID].FindControl("HeadOfExpense");


        TextBox Amount = (TextBox)GridView1.Rows[rowID].FindControl("Amounttt");
        TextBox numberofDay = (TextBox)GridView1.Rows[rowID].FindControl("NumberOfdays");
        TextBox TotalAmount = (TextBox)GridView1.Rows[rowID].FindControl("Amount");


        if (HeadOfExpense.Text.Trim() == "Caesarean Delivery" || HeadOfExpense.Text.Trim() == "Normal Delivery")
        {
            int noofChild = 0;
            try
            {
                noofChild = Convert.ToInt32(txtChildrenNo.Text);
            }
            catch (Exception)
            {
                txtRent.Text = 0.ToString();
                txtRent.ReadOnly = true;
                numberofDay.Text = 1.ToString();
                //Amount.Text = 0.ToString();
                TotalAmount.Text = 0.ToString();
                txtChildrenNo.Text = 0.ToString();

                //throw;
            }
            if (noofChild >= 3)
            {
                txtRent.Text = 0.ToString();
                txtRent.ReadOnly = true;
                numberofDay.Text = 1.ToString();
                //Amount.Text = 0.ToString();
                TotalAmount.Text = 0.ToString();
                aShowMessage.ShowMessageBox("You can not claim this!", this);
            }
            else
            {
                //  txtRent.Text = 0.ToString();
                txtRent.ReadOnly = false;

            }
        }

        decimal markTotal = 0;
        GettotalMark(markTotal);

    }
}
