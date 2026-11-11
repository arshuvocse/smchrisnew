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
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class HealthCare_UI_ExpenseReimbursementFormEntry : System.Web.UI.Page
{


    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private ReimbursmentFormDal formDal = new ReimbursmentFormDal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
                
            LoadInitialGrid();
            Option();
           
            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {

                hfMasterId.Value = Request.QueryString["MID"].ToString();
                LoadInitialDDL();    

                //id_mastetID.Value = (Request.QueryString["MID"]);
                onRecord( Convert.ToInt32(Request.QueryString["MID"]));
            }
            else
            {
                LoadInitialDDL();   
                
                LoadInitialgv_Member_Save();
            }

          


        }
    }



    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ExpenseReimbursementFormList.aspx");
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }


    protected void onRecord(Int32 id)
    {

        if (Convert.ToString(Session["dsdadf"]) == "View")
        {
            SearchButton.Visible = false;
            btnReset.Visible = false;
            drrft.Visible = false;
            Session["dsdadf"] = "";
        }


        DataTable dtMaster = formDal.Get_ReimbusrmentFormById(id);
        if (dtMaster.Rows.Count > 0)
        {




            if (Convert.ToBoolean(dtMaster.Rows[0]["IsOPD"].ToString()))
            {
                inlineRadio2.Checked = true;
                inlineRadio1.Checked = false;

            }
            else
            {
                inlineRadio1.Checked = true;
                inlineRadio2.Checked = false;

            }

            ddlCompany.SelectedValue = dtMaster.Rows[0]["CompanyId"].ToString();
            ddlCompany_OnSelectedIndexChanged(null, null);
            ddlForwordEmp.SelectedValue = dtMaster.Rows[0]["EmpInfoId"].ToString();
            ddlForwordEmp_OnSelectedIndexChanged(null, null);
            ddlFinancialYear.SelectedValue = dtMaster.Rows[0]["FinancialYearId"].ToString();

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

            if (dtClaim.Rows.Count > 0)
            {

                GridView1.DataSource = null;
                GridView1.DataBind();
                GridView1.DataSource = dtClaim;
                GridView1.DataBind();
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
                ViewState["DocGrid_List"] = dtDoc;
                gv_DocumentUpload.DataSource = dtDoc;
                gv_DocumentUpload.DataBind();
            }

        }
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
                    dateTextBox.Visible = true;
                    dateTextBox.Text = string.Empty;
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
        aTable.Columns.Add("SINoOfEncloseVoucher");
        aTable.Columns.Add("Amount");
        aTable.Columns.Add("OIPDHeadOfExpenseId");
        DataRow dr;
        dr = aTable.NewRow();
        dr["HeadOfExpense"] = "";
        dr["Dates"] = "";
        dr["SINoOfEncloseVoucher"] = "";
        dr["Amount"] = "";
        dr["OIPDHeadOfExpenseId"] = "";
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


    if (hfMasterId.Value == "")
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {

            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
            ddlCompany.SelectedIndex = 1;

            if (ddlCompany.SelectedValue != "")
            {
                using (DataTable dtt = formDal.Get_FinancialById(ddlCompany.SelectedValue.ToString()))
                {
                    ddlFinancialYear.DataSource = dtt;
                    ddlFinancialYear.DataValueField = "FinancialYearId";
                    ddlFinancialYear.DataTextField = "FinancialYearDesc";
                    ddlFinancialYear.DataBind();
                    ddlFinancialYear.Items.Insert(0, new ListItem("Please Select an Financial Year.....", String.Empty));
                }


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
            }


            load_IPD_OPD(0, ddlCompany.SelectedValue, hfEmpID.Value);

        

        }
    }
    else
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {

            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
            ddlCompany.SelectedIndex = 1;

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
                OfficailMobile.Text = dtEmp.Rows[0]["OfficialMobile"].ToString().Trim();
                }
                catch (Exception)
                {       
                    //throw;
                }

                LocationLabel.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
            lblPlace.Text = dtEmp.Rows[0]["Location"].ToString();

            ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();

                //Bank Info

                lblBankAccountNo.Text = dtEmp.Rows[0]["BankAccountNo"].ToString().Trim();
                lblBankName.Text = dtEmp.Rows[0]["AccountName"].ToString().Trim();
                lblBranchName.Text = dtEmp.Rows[0]["BranchName"].ToString().Trim();
                lblRoutingNo.Text = dtEmp.Rows[0]["RoutingNo"].ToString().Trim();


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

    protected void IPD_Click(object sender, EventArgs e)
    {
        if (inlineRadio1.Checked)
        {
            inlineRadio2.Checked = false;      
        }
        else
        {
            inlineRadio1.Checked = true;
        }

        Remainingbalance();

        if (ddlCompany.SelectedValue!="")
        {
            load_IPD_OPD(0, ddlCompany.SelectedValue, hfEmpID.Value);
        }
       
    }

    protected void OPD_Click(object sender, EventArgs e)
    {


        if (inlineRadio2.Checked)
        {
            inlineRadio1.Checked = false;
            
        }
        else
        {
            inlineRadio1.Checked = true;
        }

        Remainingbalance();
        if (ddlCompany.SelectedValue!="")
        {
            load_IPD_OPD(1, ddlCompany.SelectedValue, hfEmpID.Value);
            
        }

    }

    public void Remainingbalance()

    {
        string balanceType = "";

        int type =0;

        if (inlineRadio1.Checked)
        {
            balanceType = "IPD";
            type = 0;
        }
        else if (inlineRadio2.Checked)
        {
            balanceType = "OPD";
            type = 1;
        }

        if (ddlCompany.SelectedValue != "" && ddlFinancialYear.SelectedValue != "" && hfEmpID.Value != "" && balanceType != "")
        {

            DataTable dt = formDal.Get_RemainningBalance(hfEmpID.Value, ddlCompany.SelectedValue, ddlFinancialYear.SelectedValue, balanceType,type.ToString());

            if (dt.Rows.Count > 0)
            {

                RemainingBalance.Text = string.Empty;
                RemainingBalance.Text = dt.Rows[0]["RMBalance"].ToString();
            }

        }
        else
        {
            RemainingBalance.Text = string.Empty;
            // ShowMessage();
        }

    }



    public void load_IPD_OPD(int IsOPD, string ComId, string EmpId)



    {

        string balanceType = "";

        if (inlineRadio1.Checked)
        {
            balanceType = "IPD";
            
        }
        else if (inlineRadio2.Checked)
        {
            balanceType = "OPD";
           
        }
        DataTable dt = formDal.Get_IpdOpd(IsOPD, ComId, EmpId, balanceType);
        GridView1.DataSource = dt;
        GridView1.DataBind();
        decimal markTotal = 0;
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TextBox date = (TextBox)GridView1.Rows[i].FindControl("Date");

                TextBox Voucher = (TextBox)GridView1.Rows[i].FindControl("Voucher");
                TextBox Amount = (TextBox)GridView1.Rows[i].FindControl("Amount");
                
                date.Text = string.Empty;
                Voucher.Text = string.Empty;

 
            }


            GettotalMark(markTotal);
        }
    }

    private void GettotalMark(decimal markTotal)
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            TextBox Amount = (TextBox) GridView1.Rows[i].FindControl("Amount");


            if (Amount.Text == "")
            {
                markTotal = markTotal + 0;
            }
            else
            {
                markTotal = markTotal + Convert.ToDecimal(Amount.Text.ToString());
            }


        }

        Label tst2 = (Label) GridView1.FooterRow.FindControl("lblTotalMark");
        tst2.Text = markTotal.ToString();
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

    ShowMessage aShowMessage = new ShowMessage();

    private bool docVali()
    {
        lblMsg.Text = "";
        if (hfDocFile.Value == "")
        {
            aShowMessage.ShowMessageBox("Please click Document Upload Button", this);

            return false;
        }
        if (txtSummaryNote.Text == "")
        {
            aShowMessage.ShowMessageBox("Please Enter Summary Note ", this);
            lblMsg.Text = "<b>" + hfDocFileName.Value + "</b> has been uploaded.";
            return false;
        }
        return true;

    }

    private void AddNewDocGrid_List()
    {
        if (ViewState["DocGrid_List"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["DocGrid_List"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();


                string extension;

                extension = Path.GetExtension(hfDocFile.Value);
                //jpg, png,xlsx,pdf,txt,doc,docx
                if (extension == ".jpg" || extension == ".png")
                {
                    drCurrentRow["DocumentLinkPreview"] = "http://182.160.103.234:8088/UploadHealthCareDoc/" + hfDocFile.Value;
                }
                else
                {
                    drCurrentRow["DocumentLinkPreview"] = "https://docs.google.com/gview?url=http://182.160.103.234:8088/UploadHealthCareDoc/" + hfDocFile.Value + "&embedded=true";
                }

                drCurrentRow["DocumentLink"] = "../UploadMeetingDocument/" + hfDocFile.Value;
                //drCurrentRow["DocumentLink"] =  @"file:///D:/UploadHealthCareDoc/"+ hfDocFile.Value;
                drCurrentRow["FileName"] = hfDocFileName.Value;




                drCurrentRow["DocumentNote"] = txtSummaryNote.Text.Trim();

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



            dr = dt.NewRow();
            string extension;

            extension = Path.GetExtension(hfDocFile.Value);
            //jpg, png,xlsx,pdf,txt,doc,docx
            if (extension == ".jpg" || extension == ".png")
            {
                dr["DocumentLinkPreview"] = "http://182.160.103.234:8088/UploadHealthCareDoc/" + hfDocFile.Value;
            }
            else
            {
                dr["DocumentLinkPreview"] = "https://docs.google.com/gview?url=http://182.160.103.234:8088/UploadHealthCareDoc/" + hfDocFile.Value + "&embedded=true";
            }

            dr["DocumentLink"] = "../UploadHealthCareDoc/" + hfDocFile.Value;
            //dr["DocumentLinkPreview"] = "https://docs.google.com/gview?url=http://182.160.103.234:8088/UploadMeetingDocument/" + hfDocFile.Value + "&embedded=true";
            //  dr["DocumentLink"] = @"file:///D:/UploadMeetingDocument/3eec2898121c4467be57981c13852a9e.png"; //@"file:///D:/UploadMeetingDocument/" + hfDocFile.Value;
            dr["FileName"] = hfDocFileName.Value;


            dr["DocumentNote"] = txtSummaryNote.Text.Trim();
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


        if (NameofPatient.Text == "" )
        {
            aShowMessage.ShowMessageBox("Please! Select input patient name", this);
            NameofPatient.Focus();
            return false;
        }

        //if (Age.Text == "")
        //{
        //    aShowMessage.ShowMessageBox("Please! Select input patient Age", this);
        //    Age.Focus();
        //    return false;
        //}

        if (Relationship.Text == "")
        {
            aShowMessage.ShowMessageBox("Please! Select input Relationship", this);
            Relationship.Focus();
            return false;
        }

        if (GridView1.Rows.Count==0)
        {
            aShowMessage.ShowMessageBox("Claim Information List can not be Empty!!", this);
           
            return false;
        }

        Label tst2 = (Label)GridView1.FooterRow.FindControl("lblTotalMark");
        if (tst2.Text=="0")
        {
            aShowMessage.ShowMessageBox("Amount (BDT) can not be Zero", this);
         //   Relationship.Focus();
            return false;
        }

        return true;
    }

    //save command

    protected void Save_OnClick(object sender, EventArgs e)
    {
        string Action = "Submitted";

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
            if (Age.Text != "")
            {
                aMaster.PatientAge = int.Parse(Age.Text);

                
            }
            aMaster.PatientAge = null;

            aMaster.PatientName = NameofPatient.Text;
            aMaster.Relationship = Relationship.Text;
            aMaster.ActionStatus = Action;
            if (hfMasterId.Value == "")
            {
                aMaster.ReimbursFromMasterId = 0;
            }
            else
            {
                aMaster.ReimbursFromMasterId = int.Parse(hfMasterId.Value);
            }
            if (inlineRadio2.Checked)
            {
                //aMaster.IsOPD = true;
            }
            else
            {
             //   aMaster.IsOPD = false;
            }

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

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                HiddenField hfheadId = (HiddenField) GridView1.Rows[i].FindControl("hfHeadOfExpenseId");
                TextBox Date = (TextBox) GridView1.Rows[i].FindControl("Date");
                TextBox Voucher = (TextBox) GridView1.Rows[i].FindControl("Voucher");
                TextBox Amount = (TextBox) GridView1.Rows[i].FindControl("Amount");

                ClaimDetailsDao DocA = new ClaimDetailsDao();
                DocA.OIPDHeadOfExpenseId = Convert.ToInt32(hfheadId.Value.ToString());

                if (Date.Text != "")
                {
                    DocA.Dates = Convert.ToDateTime(Date.Text);
                }

                if (Voucher.Text != "")
                {
                    DocA.SINoOfEncloseVoucher = Voucher.Text.Trim();
                }

                if (Amount.Text != "")
                {
                    DocA.Amount = Convert.ToDecimal(Amount.Text.ToString());
                }

                claimDetailslList.Add(DocA);
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
                    aListDao.EmpInfoId = Convert.ToInt32(empId.Value);
                    empList.Add(aListDao);
                }
               
            }

            Int32 rei = 0;

            if (rei > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfull Done...');window.location ='ExpenseReimbursementFormList.aspx';",
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

        bool yesCheck = yesBox.Checked;

        if (yesCheck)
        {
            No.Checked = false;
            Date.Enabled = true;
            Date.Text = "";

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
        bool NoCheck = No.Checked;

        if (NoCheck)
        {
            yesBox.Checked = false;

            Date.Enabled = false;
            Date.Text = "";
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
            try
            {
                amount = Convert.ToDecimal(GridView1.DataKeys[rowID][0].ToString());
            }
            catch (Exception exception)
            {
                //
            }


            TextBox amountBox = (TextBox)GridView1.Rows[rowID].FindControl("Amount");

            HiddenField masterId = (HiddenField)GridView1.Rows[rowID].FindControl("hfHeadOfExpenseId");


            try
            {
                if (Convert.ToDecimal(amountBox.Text) <= amount)
                {

                }
                else
                {
                    if (Convert.ToInt32(masterId.Value) != 16)
                    {
                        amountBox.Text = string.Empty;
                        aShowMessage.ShowMessageBox("Amount must be less then equal " + amount, this);
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
                        NameofPatient.Text = emp[1].Trim();
                        Relationship.Text = emp[0].Trim();

                        NameofPatient.Attributes.Add("readonly", "readonly");
                        Relationship.Attributes.Add("readonly", "readonly");
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
}