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
using DAL.SKILL_WILL_DAL;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using DAO.SkillWill_Dao;
using HELPER_FUNCTIONS.HELPERS;

public partial class Skill_Will_Assessment_SkillWill_Assessment_Declaration : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    private KPISETUPListDAL _KPILIST = new KPISETUPListDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    JdDAL _jdDal = new JdDAL();
    PermissionDAL aPermissionDal = new PermissionDAL();
    private SkillWillDeclarationDal aDeclarationDal = new SkillWillDeclarationDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //UserPersmissionValidation();
            ReadonlyDateTime();
            LoadInitialDDL();
            ButtonVisible();
            CalendarExtender1.StartDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy"));

            if (!string.IsNullOrEmpty(Request.QueryString["masterId"]))
            {
                int mid = int.Parse(Request.QueryString["masterId"]);
                hid_KpiMasrerId.Value = mid.ToString();

                DataTable dt = aDeclarationDal.GetKpiSetupByMaster(mid);
                ddlCompany.SelectedValue = dt.Rows[0]["CompanyId"].ToString();
                ddlCompany_OnSelectedIndexChanged(ddlCompany, (EventArgs)e);
                ddlFinancialYear.SelectedValue = dt.Rows[0]["FinancialYearId"].ToString();
                DeclarationTextBox.Text = Convert.ToDateTime(dt.Rows[0]["DeclarationDate"].ToString()).ToString("dd-MMM-yyyy");
                ddlFinancialYear_OnSelectedIndexChanged(ddlFinancialYear, (EventArgs)e);
                subjectTextBox.Text = dt.Rows[0]["Subject"].ToString();

                HfUpdateDate.Value = dt.Rows[0]["UpdateDate"].ToString();

                lastDate.Text = "Last Declaration Date: " + Convert.ToDateTime(dt.Rows[0]["UpdateDate"].ToString()).ToString("dd-MMM-yyyy hh:mm tt");
                //
                // ViewState["EmpSetup"] = dtForAll;





                DataTable dt2 = aDeclarationDal.GetKpiSetupDetailsByMaster(mid);

                if (dt.Rows[0]["IsCommon"].ToString() == "True")
                {
                    chk_Common.Checked = true;
                    txt_deadLine.Text = Convert.ToDateTime(dt2.Rows[0]["DeadLine"].ToString()).ToString("dd-MMM-yyyy");
                    txt_commonRemarks.Text = dt2.Rows[0]["Remarks"].ToString();


                }



                //   DataTable dtForAll = _jdDal.GetEmployeeForKpiSetUpNew((ddlCompany.SelectedValue.ToString()), null, null, Parameter());
                SaveGridView.DataSource = dt2;
                SaveGridView.DataBind();

            }

        }
    }

  

    private void ReadonlyDateTime()
    {

        // txt_deadLine.Attributes.Add("readonly", "readonly");
        //  DeclarationTextBox.Attributes.Add("readonly", "readonly");
    }
    public void DateValidation()
    {
        try
        {
            DateTime adate = Convert.ToDateTime(txt_deadLine.Text);
        }
        catch (Exception ex)
        {
            txt_deadLine.Focus();
            txt_deadLine.Text = "";
            AlertMessageBoxShow("Enter A Valid Date!!");

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
        else
        {
            Response.Redirect("~/Skill_Will_Assessment/SkillWill_AssessmentDeclarationView.aspx");
        }

    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
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
                param = param + " AND A.DepartmentId='" + ddlDept.SelectedValue + "' ";
            }
        }

        if (chkNewJoinerList.Checked)
        {

            param = param + " AND  A.EntryDate >=  '" + HfUpdateDate.Value + "' ";

        }
        return param;

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
    private DeadlineExtendedEntryDAL _aFincDal = new DeadlineExtendedEntryDAL();
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


        txt_deadLine.Text = "";


    }


    private bool CheckStartEndDateExistOrNot2(DateTime Start, DateTime End)
    {
        bool status = false;

        DataTable dataTable = _aFincDal.CheckStartEndDateExistOrNotDAL2(ddlFinancialYear.SelectedValue, Start, End);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }
    protected void txt_deadLine_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            string birthdt = Convert.ToDateTime(txt_deadLine.Text.Trim()).ToString("dd/MMM/yyyy");




            if (birthdt != "")
            {


                if (CheckStartEndDateExistOrNot2(Convert.ToDateTime(birthdt), Convert.ToDateTime(birthdt)) == true)
                {

                }
                if (CheckStartEndDateExistOrNot2(Convert.ToDateTime(birthdt), Convert.ToDateTime(birthdt)) == false)
                {
                    aShowMessage.ShowMessageBox("Deadline date must be within the finnancial year!!", this);
                    txt_deadLine.Text = "";
                    txt_deadLine.Focus();

                }
            }


            //  DateValidation();
            if (chk_Common.Checked)
            {
                //DataTable dt = (DataTable)ViewState["EmpSetup"];
                //string remarks = txt_commonRemarks.Text.Trim().ToString();
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    dt.Rows[i]["DeadLine"] = txt_deadLine.Text.ToString();
                //    dt.Rows[i]["Remarks"] = remarks;
                //}

                if (birthdt != "")
                {
                    for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
                    {
                        //DataTable dt = (DataTable)ViewState["EmpSetup"];
                        TextBox expectedDate = ((TextBox)gv_allocateEmp.Rows[j].Cells[5].FindControl("txt_DeadLine"));



                        expectedDate.Text = birthdt;
                    }

                    for (int j = 0; j < SaveGridView.Rows.Count; j++)
                    {
                        //DataTable dt = (DataTable)ViewState["EmpSetup"];
                        Label txt_DeadLine = ((Label)SaveGridView.Rows[j].Cells[5].FindControl("txt_DeadLine"));


                        txt_DeadLine.Text = birthdt;

                    }
                }

                //ViewState["EmpSetup"] = dt;
                //gv_allocateEmp.DataSource = dt;
                //gv_allocateEmp.DataBind();

                //if (hid_KpiMasrerId.Value != "")
                //{
                //    DataTable dt2 = _jdDal.GetKpiSetupDetailsByMaster(Convert.ToInt32(hid_KpiMasrerId.Value));

                //    for (int i = 0; i < dt2.Rows.Count; i++)
                //    {
                //        for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
                //        {
                //            CheckBox chk = (CheckBox)gv_allocateEmp.Rows[j].FindControl("txt_check");
                //            HiddenField txt_empInfoId = (HiddenField)gv_allocateEmp.Rows[j].FindControl("txt_empInfoId");
                //            TextBox txt_DeadLine = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_DeadLine");
                //            TextBox txt_Remarks = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_Remarks");

                //            if (txt_empInfoId.Value == dt2.Rows[i]["EmpinfoId"].ToString())
                //            {
                //                chk.Checked = true;
                //                txt_DeadLine.Text = txt_deadLine.Text.ToString();
                //                txt_Remarks.Text = remarks;

                //            }

                //        }
                //    }
                //}

            }

        }
        catch (Exception)
        {


            AlertMessageBoxShow("Give A valid Date !!");
            txt_deadLine.Focus();
            txt_deadLine.Text = string.Empty;
        }
    }

    protected void txt_commonRemarks_OnTextChanged(object sender, EventArgs e)
    {
        //if (ViewState["EmpSetup"] != null && chk_Common.Checked)
        //{
        //DataTable dt = (DataTable)ViewState["EmpSetup"];
        //string remarks = txt_commonRemarks.Text.Trim().ToString();
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    dt.Rows[i]["DeadLine"] = txt_deadLine.Text.ToString();
        //    dt.Rows[i]["Remarks"] = remarks;
        //}
        //ViewState["EmpSetup"] = dt;
        //gv_allocateEmp.DataSource = dt;
        //gv_allocateEmp.DataBind();


        //if (hid_KpiMasrerId.Value != "")
        //{
        //    DataTable dt2 = _jdDal.GetKpiSetupDetailsByMaster(Convert.ToInt32(hid_KpiMasrerId.Value));

        //    for (int i = 0; i < dt2.Rows.Count; i++)
        //    {
        //        for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
        //        {
        //            CheckBox chk = (CheckBox)gv_allocateEmp.Rows[j].FindControl("txt_check");
        //            HiddenField txt_empInfoId = (HiddenField)gv_allocateEmp.Rows[j].FindControl("txt_empInfoId");
        //            TextBox txt_DeadLine = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_DeadLine");
        //            TextBox txt_Remarks = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_Remarks");

        //            if (txt_empInfoId.Value == dt2.Rows[i]["EmpinfoId"].ToString())
        //            {
        //                chk.Checked = true;
        //                txt_DeadLine.Text = txt_deadLine.Text.ToString();
        //                txt_Remarks.Text = remarks;

        //            }

        //        }
        //    }
        //}


        //}




        if (chk_Common.Checked)
        {
            //DataTable dt = (DataTable)ViewState["EmpSetup"];
            //string remarks = txt_commonRemarks.Text.Trim().ToString();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    dt.Rows[i]["DeadLine"] = txt_deadLine.Text.ToString();
            //    dt.Rows[i]["Remarks"] = remarks;
            //}

            for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
            {
                //DataTable dt = (DataTable)ViewState["EmpSetup"];

                TextBox Remarks = ((TextBox)gv_allocateEmp.Rows[j].Cells[5].FindControl("txt_Remarks"));

                Remarks.Text = txt_commonRemarks.Text;

            }

            for (int j = 0; j < SaveGridView.Rows.Count; j++)
            {
                //DataTable dt = (DataTable)ViewState["EmpSetup"];

                Label txt_Remarks = ((Label)SaveGridView.Rows[j].Cells[5].FindControl("txt_Remarks"));

                txt_Remarks.Text = txt_commonRemarks.Text;

            }

            //ViewState["EmpSetup"] = dt;
            //gv_allocateEmp.DataSource = dt;
            //gv_allocateEmp.DataBind();

            //if (hid_KpiMasrerId.Value != "")
            //{
            //    DataTable dt2 = _jdDal.GetKpiSetupDetailsByMaster(Convert.ToInt32(hid_KpiMasrerId.Value));

            //    for (int i = 0; i < dt2.Rows.Count; i++)
            //    {
            //        for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
            //        {
            //            CheckBox chk = (CheckBox)gv_allocateEmp.Rows[j].FindControl("txt_check");
            //            HiddenField txt_empInfoId = (HiddenField)gv_allocateEmp.Rows[j].FindControl("txt_empInfoId");
            //            TextBox txt_DeadLine = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_DeadLine");
            //            TextBox txt_Remarks = (TextBox)gv_allocateEmp.Rows[j].FindControl("txt_Remarks");

            //            if (txt_empInfoId.Value == dt2.Rows[i]["EmpinfoId"].ToString())
            //            {
            //                chk.Checked = true;
            //                txt_DeadLine.Text = txt_deadLine.Text.ToString();
            //                txt_Remarks.Text = remarks;

            //            }

            //        }
            //    }
            //}

        }



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





            if (chk_Common.Checked)
            {
                if (txt_deadLine.Text.Trim() != "")
                {


                    string birthdt = Convert.ToDateTime(txt_deadLine.Text.Trim()).ToString("dd/MMM/yyyy");


                    if (birthdt != "")
                    {
                        for (int j = 0; j < gv_allocateEmp.Rows.Count; j++)
                        {
                            //DataTable dt = (DataTable)ViewState["EmpSetup"];
                            TextBox expectedDate = ((TextBox)gv_allocateEmp.Rows[j].Cells[5].FindControl("txt_DeadLine"));
                            TextBox Remarks = ((TextBox)gv_allocateEmp.Rows[j].Cells[5].FindControl("txt_Remarks"));

                            Remarks.Text = txt_commonRemarks.Text;
                            expectedDate.Text = birthdt;
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

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {


        if (Validation() == true)
        {

            DataTable aTable =
                aDeclarationDal.DeleteValidattionForEffectiveDate((ddlCompany.SelectedValue), (ddlFinancialYear.SelectedValue));
            if (aTable.Rows.Count > 0)
            {
                AlertMessageBoxShow("Already Exist!!!...");
            }
            else
            {


                SkillWillAssessmentDecMasterDao aMaster = new SkillWillAssessmentDecMasterDao();

                aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);
                aMaster.DeclarationDate = Convert.ToDateTime(DeclarationTextBox.Text);
                aMaster.IsCommon = chk_Common.Checked == true ? true : false;

                aMaster.SkillWillAssesDecMasId = hid_KpiMasrerId.Value == "" ? 0 : Convert.ToInt32(hid_KpiMasrerId.Value);
                aMaster.Subject = subjectTextBox.Text;
                List<SkillWillAssesmentDecDetailsDao> aDetailses = new List<SkillWillAssesmentDecDetailsDao>();

                for (int i = 0; i < SaveGridView.Rows.Count; i++)
                {

                    HiddenField txt_empInfoId = (HiddenField)SaveGridView.Rows[i].FindControl("txt_empInfoId");
                    Label txt_DeadLine = (Label)SaveGridView.Rows[i].FindControl("txt_DeadLine");
                    Label txt_Remarks = (Label)SaveGridView.Rows[i].FindControl("txt_Remarks");


                    SkillWillAssesmentDecDetailsDao aDetails = new SkillWillAssesmentDecDetailsDao();
                    aDetails.EmpinfoId = Convert.ToInt32(txt_empInfoId.Value);
                    aDetails.DeadLine = Convert.ToDateTime(txt_DeadLine.Text.Trim());
                    aDetails.Remarks = txt_Remarks.Text.Trim();

                    aDetailses.Add(aDetails);
                    
                }

                if (aDetailses.Count > 0)
                {
                    int pk = aDeclarationDal.SaveSkillWillAssessmentDecMaster(aMaster, Session["LoginName"].ToString());
                    bool result = false;
                    if (pk > 0)
                    {
                        result = aDeclarationDal.SaveKpiSetupDetails(aDetailses, pk);
                    }

                    if (result == true)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Operation Successful...');window.location ='SkillWill_AssessmentDeclarationView.aspx';",
                            true);

                    }
                    else
                    {
                        AlertMessageBoxShow("Operation Failed...");
                    }

                }
            }
        }

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

    protected void deleteImageButtonDirectlySupervices_OnClick(object sender, ImageClickEventArgs e)
    {
        ImageButton lb = (ImageButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField empInfoId = (HiddenField)SaveGridView.Rows[rowID].FindControl("txt_empInfoId");


        DataTable aTable =
            aDeclarationDal.CheckKepiSetpExist(empInfoId.Value, ddlFinancialYear.SelectedValue);
        if (aTable.Rows.Count > 0)
        {
            AlertMessageBoxShow("Can Not be Deleted...");
        }
        else
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




        return isValid;
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/Skill_Will_Assessment/SkillWill_AssessmentDeclarationView.aspx");
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

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation() == true)
        {
            SkillWillAssessmentDecMasterDao aMaster = new SkillWillAssessmentDecMasterDao();

            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);
            aMaster.DeclarationDate = Convert.ToDateTime(DeclarationTextBox.Text);
            aMaster.IsCommon = chk_Common.Checked == true ? true : false;

            aMaster.SkillWillAssesDecMasId = hid_KpiMasrerId.Value == "" ? 0 : Convert.ToInt32(hid_KpiMasrerId.Value);
            aMaster.Subject = subjectTextBox.Text;
            List<SkillWillAssesmentDecDetailsDao> aDetailses = new List<SkillWillAssesmentDecDetailsDao>();

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



                SkillWillAssesmentDecDetailsDao aDetails = new SkillWillAssesmentDecDetailsDao();
                aDetails.EmpinfoId = Convert.ToInt32(txt_empInfoId.Value);
                aDetails.DeadLine = Convert.ToDateTime(txt_DeadLine.Text.Trim());
                aDetails.Remarks = txt_Remarks.Text.Trim();

                aDetailses.Add(aDetails);
                //    }
                //}
            }

            if (aDetailses.Count > 0)
            {

                int pk = aDeclarationDal.SaveSkillWillAssessmentDecMaster(aMaster, Session["LoginName"].ToString());
                bool result = false;
                if (pk > 0)
                {
                    result = aDeclarationDal.SaveKpiSetupDetails(aDetailses, pk);
                }

                if (result == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successful...');window.location ='SkillWill_AssessmentDeclarationView.aspx';",
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
        string empInfoId = "";
        string FinancialYear = ddlFinancialYear.SelectedValue;

        bool result = aDeclarationDal.DeleteSkillWillDeclaration(Convert.ToInt32(hid_KpiMasrerId.Value), Session["LoginName"].ToString());

        if (result == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successful...');window.location ='SkillWill_AssessmentDeclarationView.aspx';",
                true);
            //DataTable dt = _jdDal.GetKpiSetupList();
            //gv_kpiSetup.DataSource = dt;
            //gv_kpiSetup.DataBind();

        }
        else
        {
            AlertMessageBoxShow("Operation Failed...");

        }


        //for (int i = 0; i < SaveGridView.Rows.Count; i++)
        //{


        //    HiddenField txt_empInfoId = ((HiddenField)SaveGridView.Rows[i].FindControl("txt_empInfoId"));


        //    empInfoId += txt_empInfoId.Value + ",";


        //}
        //string Emp = empInfoId.TrimEnd(',');

        //DataTable aTable =
        //    _jdDal.CheckKepiSetpExistMulti(Emp, FinancialYear);
        //if (aTable.Rows.Count > 0)
        //{
        //    AlertMessageBoxShow("Can Not be Deleted...");
        //}
        //else
        //{


        //    bool result = aDeclarationDal.DeleteSkillWillDeclaration(Convert.ToInt32(hid_KpiMasrerId.Value), Session["LoginName"].ToString());

        //    if (result == true)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(),
        //            "alert",
        //            "alert('Operation Successful...');window.location ='SkillWill_AssessmentDeclarationView.aspx';",
        //            true);
        //        //DataTable dt = _jdDal.GetKpiSetupList();
        //        //gv_kpiSetup.DataSource = dt;
        //        //gv_kpiSetup.DataBind();

        //    }
        //    else
        //    {
        //        AlertMessageBoxShow("Operation Failed...");

        //    }
        //}
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
        DataTable dt = aDeclarationDal.GetEmployeeForKpiSetUpNew((ddlCompany.SelectedValue.ToString()), deadLine, remarks, Parameter());
        gv_allocateEmp.DataSource = dt;
        gv_allocateEmp.DataBind();

        if (dt.Rows.Count > 0)
        {

        }
        else
        {
            AlertMessageBoxShow("No Data Found !!");
        }

        ViewState["EmpSetup"] = dt;
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
                aDataTable.Columns.Add("FinancialYearId");



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
                //  ShowMessageBox("Already Exist !!!");
            }
        }

    }
    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
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
}