using System;
using System.Activities.Validation;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class Training_TrainingBudget2 : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ButtonVisible();
            LoadInitialDDL();
           // LoadGrade();
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                int mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();

                if (mid > 0)
                {

                    DataTable dtM = _trainingDal.GetTrainingBudget2ById(mid);
                    ddlCompany.SelectedValue = dtM.Rows[0]["CompanyId"].ToString();
                    hdpk.Value = dtM.Rows[0]["TrainingBudget2Id"].ToString();
                    ddlCompany_OnSelectedIndexChanged(ddlCompany, (EventArgs) e);
                    ddlFinancialYear.SelectedValue = dtM.Rows[0]["FinancialYearId"].ToString();
                    ddlFinancialYear_OnSelectedIndexChanged(ddlFinancialYear, (EventArgs) e);
                  //  txtToalYearlyBudget.Text = dtM.Rows[0]["TotalYearlyBudgetCost"].ToString();
                    txt_toalBudget.Text = dtM.Rows[0]["TotalBudget"].ToString();

                    DataTable dt2 = _trainingDal.GetTrainingBudget2Details(mid);
                    if (dt2.Rows.Count > 0)
                    {
                        ViewState["TrainingDetails"] = dt2;
                    }
                    gv_training.DataSource = dt2;
                    gv_training.DataBind();

                    //if (ViewState["TrainingDetails"] != null)
                    //{
                    //    int i = 0;

                    //    Label TrainingTitle = (Label) gv_training.Rows[i].FindControl("TrainingTitle");
                    //    Label ExpectedResult = (Label) gv_training.Rows[i].FindControl("ExpectedResult");
                    //    Label Quater = (Label) gv_training.Rows[i].FindControl("Quater");
                    //    Label Grade = (Label) gv_training.Rows[i].FindControl("Grade");
                    //    Label Category = (Label) gv_training.Rows[i].FindControl("Category");
                    //    Label InternalExternal = (Label) gv_training.Rows[i].FindControl("InternalExternal");
                    //    Label ForeignLocal = (Label) gv_training.Rows[i].FindControl("ForeignLocal");
                    //    Label MonthId = (Label) gv_training.Rows[i].FindControl("MonthId");
                    //    Label TotalParticipant = (Label) gv_training.Rows[i].FindControl("TotalParticipant");
                    //    Label BudgetCostParticipant = (Label) gv_training.Rows[i].FindControl("BudgetCostParticipant");
                    //    Label Budget = (Label) gv_training.Rows[i].FindControl("Budget");
                    //    Label Referance = (Label) gv_training.Rows[i].FindControl("Referance");
                    //    Label Remarks = (Label) gv_training.Rows[i].FindControl("Remarks");
                    //    txt_TrainingTitle.Text = TrainingTitle.Text;
                    //    txt_ExpectedOutcome.Text = ExpectedResult.Text;
                    //    txt_totalQty.Text = TotalParticipant.Text;
                    //    txt_CostPerParticipant.Text = BudgetCostParticipant.Text;
                    //    txt_budget.Text = Budget.Text;
                    //    ddlEmpCategoryEx.SelectedValue = Category.Text;
                    //    LoadGrade();
                    //    foreach (ListItem item in radExIn.Items)
                    //    {
                    //        if (item.Text.Trim() == InternalExternal.Text.Trim())
                    //        {
                    //            item.Selected = true;
                    //        }
                    //    }
                    //    foreach (ListItem item in rad_fLocal.Items)
                    //    {
                    //        if (item.Text.Trim() == ForeignLocal.Text.Trim())
                    //        {
                    //            item.Selected = true;
                    //        }
                    //    }
                    //    string[] quater = new[] {""};
                    //    if (Quater.Text.Contains(','))
                    //    {
                    //        quater = Quater.Text.Split(',');
                    //    }
                    //    else
                    //    {
                    //        quater = new[] {Quater.Text};
                    //    }
                    //    foreach (string name in quater)
                    //    {
                    //        foreach (ListItem item in ddlQuater.Items)
                    //        {
                    //            if (item.Text == name)
                    //            {
                    //                item.Selected = true;
                    //            }
                    //        }
                    //    }
                    //    string[] grade = new[] {""};
                    //    if (Grade.Text.Contains(','))
                    //    {
                    //        grade = Grade.Text.Split(',');
                    //    }
                    //    else
                    //    {
                    //        grade = new[] {Grade.Text};
                    //    }
                    //    foreach (string name in grade)
                    //    {
                    //        foreach (ListItem item in chk_Grade.Items)
                    //        {
                    //            if (item.Text == name)
                    //            {
                    //                item.Selected = true;
                    //            }
                    //        }
                    //    }
                    //    txt_ref.Text = Referance.Text;
                    //    txt_remarks.Text = Remarks.Text;

                    //    int count = 0;
                    //    for (int j = 0; j < ddlQuater.Items.Count; j++)
                    //    {
                    //        if (ddlQuater.Items[j].Selected)
                    //        {
                    //            count++;
                    //            if (count >= 2)
                    //            {
                    //                break;
                    //            }
                    //        }
                    //    }
                    //    if (count < 2)
                    //    {


                    //        string aValue = ddlQuater.SelectedValue.ToString();
                    //        DataTable dta = _trainingDal.GetMonthByQuater(Convert.ToInt32(aValue));
                    //        ddlMonth.DataSource = dta;
                    //        ddlMonth.DataTextField = "TextField";
                    //        ddlMonth.DataValueField = "Value";
                    //        ddlMonth.DataBind();
                    //    }
                    //    else
                    //    {
                    //        ddlMonth.Items.Clear();
                    //    }
                    //    if (ddlMonth.Items.Count > 0)
                    //    {
                    //        ddlMonth.SelectedValue = MonthId.Text;
                    //    }
                    //    DataTable dt = (DataTable) ViewState["TrainingDetails"];
                    //    dt.Rows.Remove(dt.Rows[0]);
                    //    ViewState["TrainingDetails"] = dt;
                    //    gv_training.DataSource = dt;
                    //    gv_training.DataBind();
                    //    CalculateBudget();

                    //}
                }
            }
            RadioTextValue();
           
        }
    }

    public void ButtonVisible()
    {
        //if (Session["Status"] != null)
        //{


        //    if (Session["Status"].ToString() == "Add")
        //    {
        //        submitButton.Visible = true;
        //    }
        //    else if (Session["Status"].ToString() == "Edit")
        //    {
        //        editButton.Visible = true;
        //    }
        //    else if (Session["Status"].ToString() == "Delete")
        //    {
        //        delButton.Visible = true;
        //    }
        //    Session["Status"] = null;
        //}
        //else
        //{
        //    Response.Redirect("TrainingBudget2List.aspx");
        //}

    }

    private void LoadInitialDDL()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {

            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();

            ddlCompany.SelectedIndex = 1;
            ddlCompany_OnSelectedIndexChanged(null, null);
        }

        using (DataTable dt = _commonDataLoad.GetEmpCategoryDDL())
        {
            ddlEmpCategoryEx.DataSource = dt;
            ddlEmpCategoryEx.DataValueField = "Value";
            ddlEmpCategoryEx.DataTextField = "TextField";
            ddlEmpCategoryEx.DataBind();


            ddlEmpCategoryEx.Items.Add(new ListItem("Both", "3"));

        }
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
      //  ClearGrid();
        Session["CompanyId"] = ddlCompany.SelectedValue;
        DataTable dt = _trainingDal.GetFianncialYearByComIdDDl(Convert.ToInt32(ddlCompany.SelectedValue));
        ddlFinancialYear.DataSource = dt;
        ddlFinancialYear.DataValueField = "Value";
        ddlFinancialYear.DataTextField = "TextField";
        ddlFinancialYear.DataBind();
    }

    protected void ddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        LoadQuater(Convert.ToInt32(ddlFinancialYear.SelectedValue));
        GetPreTotalBudget(ddlCompany.SelectedValue,ddlFinancialYear.SelectedValue);
    }

    public void GetPreTotalBudget(string comId,string finId)
    {
        DataTable dtdata = _trainingDal.GetTotalBudgetCostSum(comId, finId);
        if (dtdata.Rows.Count>0)
        {
            txtToalYearlyBudget.Text = dtdata.Rows[0][0].ToString();
            amountHiddenField.Value= dtdata.Rows[0][0].ToString();
        }
    }

    public void CalculateBudget()
    {
        decimal total = 0;
        for (int i = 0; i < gv_training.Rows.Count; i++)
        {
            Label Budget = (Label)gv_training.Rows[i].FindControl("Budget");
            total += Convert.ToDecimal(Budget.Text);
        }
        txt_toalBudget.Text = total.ToString("F");
        //txtToalYearlyBudget.Text = ((string.IsNullOrEmpty(amountHiddenField.Value)
        //    ? 0
        //    : Convert.ToDecimal(amountHiddenField.Value)) + total).ToString();
        txtToalYearlyBudget.Text = (total).ToString();
    }
    protected void LoadQuater(int id)
    {
        //FinancialYear ayear = _trainingDal.GetFinancialYear(id);

        DataTable dt = _trainingDal.GetQuaterNew(Convert.ToInt32(ddlCompany.SelectedValue));
        //YearQuater aQuaterSelect = new YearQuater { QuarterDetails = "Select Quater", QuarterNum = "-1" };
        //quaters.Insert(0, aQuaterSelect);
        ddlQuater.DataSource = dt;
        ddlQuater.DataValueField = "Value";
        ddlQuater.DataTextField = "TextField";
        ddlQuater.DataBind();
    }
    protected void LoadGrade()
    {
        //FinancialYear ayear = _trainingDal.GetFinancialYear(id);

        if (ddlEmpCategoryEx.SelectedValue == "3")
        {
            DataTable dt = _trainingDal.GetGradeAll();
           
            chk_Grade.DataSource = dt;
            chk_Grade.DataValueField = "SalaryGradeId";
            chk_Grade.DataTextField = "GradeCode";
            chk_Grade.DataBind();
        }

        else
        {
            DataTable dt = _trainingDal.GetGrade(ddlEmpCategoryEx.SelectedValue);
            //YearQuater aQuaterSelect = new YearQuater { QuarterDetails = "Select Quater", QuarterNum = "-1" };
            //quaters.Insert(0, aQuaterSelect);
            chk_Grade.DataSource = dt;
            chk_Grade.DataValueField = "SalaryGradeId";
            chk_Grade.DataTextField = "GradeCode";
            chk_Grade.DataBind();
        }
    }
    protected void ddlQuater_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int count = 0;
        for (int i = 0; i < ddlQuater.Items.Count; i++)
        {
            if (ddlQuater.Items[i].Selected)
            {
                count++;
                if (count>=2)
                {
                    break;
                }
            }
        }
        if (count < 2)
        {
            try
            {
                string aValue = ddlQuater.SelectedValue.ToString();
                DataTable dt = _trainingDal.GetMonthByQuater(Convert.ToInt32(aValue));
                ddlMonth.DataSource = dt;
                ddlMonth.DataTextField = "TextField";
                ddlMonth.DataValueField = "Value";
                ddlMonth.DataBind();
            }
            catch
            {
                ddlMonth.Items.Clear();
            }

         
        }
        else
        {
            ddlMonth.Items.Clear();
        }
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
       Response.Redirect("TrainingBudget2List.aspx");
    }

    protected void addToList_OnClick(object sender, EventArgs e)
    {
        if (AddValidation() == true)
        {
            DataTable dttbl = (DataTable)ViewState["TrainingDetails"];

            if (dttbl == null || dttbl.Rows.Count<1)
            {
                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("TrainingBudget2DetailsId", typeof(string)));
                dt.Columns.Add(new DataColumn("TrainingTitle", typeof(string)));
                dt.Columns.Add(new DataColumn("ExpectedResult", typeof(string)));
                dt.Columns.Add(new DataColumn("QuaterId", typeof(string)));
                dt.Columns.Add(new DataColumn("Quater", typeof(string)));
                dt.Columns.Add(new DataColumn("MonthId", typeof(string)));
                dt.Columns.Add(new DataColumn("Month", typeof(string)));

                dt.Columns.Add(new DataColumn("InternalExternal", typeof(string)));
                dt.Columns.Add(new DataColumn("ForeignLocal", typeof(string)));
                dt.Columns.Add(new DataColumn("TotalParticipant", typeof(string)));
                dt.Columns.Add(new DataColumn("BudgetCostParticipant", typeof(string)));
                dt.Columns.Add(new DataColumn("Budget", typeof(string)));
                dt.Columns.Add(new DataColumn("Referance", typeof(string)));
                dt.Columns.Add(new DataColumn("Remarks", typeof(string)));
                dt.Columns.Add(new DataColumn("Grade", typeof(string)));
                dt.Columns.Add(new DataColumn("Category", typeof(int)));
                DataRow dr = null;
                dr = dt.NewRow();

                dr["TrainingBudget2DetailsId"] = "0";
                dr["TrainingTitle"] = txt_TrainingTitle.Text.ToString();
                dr["ExpectedResult"] = txt_ExpectedOutcome.Text.ToString();

                string quater = "";
                string grade = "";
                foreach (ListItem item in ddlQuater.Items)
                {
                    if (item.Selected)
                    {
                        quater = quater+"," + item.Text;
                    }
                }
                foreach (ListItem item in chk_Grade.Items)
                {
                    if (item.Selected)
                    {
                        grade = grade + "," + item.Text;
                    }
                }
                dr["QuaterId"] = "0";
                dr["Quater"] = quater.TrimStart(','); 
                dr["Grade"] = grade.TrimStart(',');
              
                if (ddlMonth.Items.Count>0)
                {
                    dr["MonthId"] = ddlMonth.SelectedValue;
                    dr["Month"] = ddlMonth.SelectedItem.Text;
                }
                else
                {
                    dr["MonthId"] = "0";
                    dr["Month"] = "";
                }
                dr["TotalParticipant"] = txt_totalQty.Text;
                dr["BudgetCostParticipant"] = txt_CostPerParticipant.Text;
                dr["Budget"] = txt_budget.Text;
                dr["Referance"] = txt_ref.Text;
                dr["Remarks"] = txt_remarks.Text;
                dr["Category"] = ddlEmpCategoryEx.SelectedValue;

                List<string> selectedExIn = radExIn.Items.Cast<ListItem>()
                    .Where(li => li.Selected)
                    .Select(li => li.Value)
                    .ToList();
                List<string> selectedForLo = rad_fLocal.Items.Cast<ListItem>()
                    .Where(li => li.Selected)
                    .Select(li => li.Value)
                    .ToList();

                string exIn = selectedExIn[0];

                string forLocal = selectedForLo[0];


                if (exIn == "External")
                {


                    dr["InternalExternal"] = "External";
                }
                else
                {
                    dr["InternalExternal"] = "Internal";
                }

                if (forLocal == "Foreign")
                {


                    dr["ForeignLocal"] = "Foreign";
                }
                else
                {
                    dr["ForeignLocal"] = "Local";
                }

                dt.Rows.Add(dr);
                ViewState["TrainingDetails"] = dt;
                gv_training.DataSource = dt;
                gv_training.DataBind();
                ClearEntry();
            }
            else
            {
                DataTable dtCurrentTable = (DataTable)ViewState["TrainingDetails"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    string quater = "";
                    string grade = "";
                    foreach (ListItem item in ddlQuater.Items)
                    {
                        if (item.Selected)
                        {
                            quater = quater + "," + item.Text;
                        }
                    }
                    foreach (ListItem item in chk_Grade.Items)
                    {
                        if (item.Selected)
                        {
                            grade = grade + "," + item.Text;
                        }
                    }
                    //drCurrentRow["QuaterId"] = "0";
                    drCurrentRow["TrainingBudget2DetailsId"] = "0";
                    drCurrentRow["Quater"] = quater.TrimStart(',');
                    drCurrentRow["Grade"] = grade.TrimStart(',');
                  
                    drCurrentRow["TrainingTitle"] = txt_TrainingTitle.Text.ToString();
                    drCurrentRow["ExpectedResult"] = txt_ExpectedOutcome.Text.ToString();
                    //drCurrentRow["QuaterId"] = ddlQuater.SelectedValue;
                    //drCurrentRow["Quater"] = ddlQuater.SelectedItem.Text;
                    if (ddlMonth.Items.Count > 0)
                    {
                        drCurrentRow["MonthId"] = ddlMonth.SelectedValue;
                        drCurrentRow["Month"] = ddlMonth.SelectedItem.Text;
                    }
                    else
                    {
                        drCurrentRow["MonthId"] = "0";
                        drCurrentRow["Month"] = "";
                    }
                   
                    drCurrentRow["TotalParticipant"] = txt_totalQty.Text;
                    drCurrentRow["BudgetCostParticipant"] = txt_CostPerParticipant.Text;
                    drCurrentRow["Budget"] = txt_budget.Text;
                    drCurrentRow["Referance"] = txt_ref.Text;
                    drCurrentRow["Remarks"] = txt_remarks.Text;
                    drCurrentRow["Category"] = ddlEmpCategoryEx.SelectedValue;
                    List<string> selectedExIn = radExIn.Items.Cast<ListItem>()
                        .Where(li => li.Selected)
                        .Select(li => li.Value)
                        .ToList();
                    List<string> selectedForLo = rad_fLocal.Items.Cast<ListItem>()
                        .Where(li => li.Selected)
                        .Select(li => li.Value)
                        .ToList();

                    string exIn = selectedExIn[0];

                    string forLocal = selectedForLo[0];


                    if (exIn == "External")
                    {


                        drCurrentRow["InternalExternal"] = "External";
                    }
                    else
                    {
                        drCurrentRow["InternalExternal"] = "Internal";
                    }

                    if (forLocal == "Foreign")
                    {


                        drCurrentRow["ForeignLocal"] = "Foreign";
                    }
                    else
                    {
                        drCurrentRow["ForeignLocal"] = "Local";
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["TrainingDetails"] = dtCurrentTable;
                    gv_training.DataSource = dtCurrentTable;
                    gv_training.DataBind();
                    CalculateBudget();
                    ClearEntry();
                }
            }
        }
       CalculateBudget();
       ddlQuater.DataSource = null;
       ddlQuater.DataValueField = "Value";
       ddlQuater.DataTextField = "TextField";
       ddlQuater.DataBind();

    }

    protected void lb_Remove_OnClick(object sender, EventArgs e)
    {
        if (ViewState["TrainingDetails"] != null)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["TrainingDetails"];
            dt.Rows.Remove(dt.Rows[rowID]);
            ViewState["TrainingDetails"] = dt;
            gv_training.DataSource = dt;
            gv_training.DataBind();

            CalculateBudget();
        }
    }

    private void ClearEntry()
    {
        txt_TrainingTitle.Text = "";
        txt_ExpectedOutcome.Text="";
     
       
        txt_totalQty.Text = "";
        txt_CostPerParticipant.Text = "";
        txt_budget.Text = "";
       txt_ref.Text = "";
        txt_remarks.Text = "";

        for (int i = 0; i < ddlQuater.Items.Count; i++)
        {
            ddlQuater.Items[i].Selected = false;
        }
        for (int i = 0; i < chk_Grade.Items.Count; i++)
        {
            chk_Grade.Items[i].Selected = false;
        }
        for (int i = 0; i < radExIn.Items.Count; i++)
        {
            radExIn.Items[i].Selected = false;
        }
        for (int i = 0; i < rad_fLocal.Items.Count; i++)
        {
            rad_fLocal.Items[i].Selected = false;
        }

    }

    private bool AddValidation()
    {
        bool isValid = true;
        if (txt_TrainingTitle.Text.ToString() == "")
        {

            isValid = false;
            aShowMessage.ShowMessageBox("Budget Head Required", this);
            
        }

        
        if (txt_totalQty.Text.ToString() == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Total Participant  Required", this);
        }
        if (txt_CostPerParticipant.Text.ToString() == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Budget Cost Per Participant  Required", this);
        }
        if (txt_budget.Text.ToString() == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Budget  Required", this);
        }

        if (ddlQuater.SelectedValue == "" || ddlQuater.SelectedValue == "-1")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Quater Required", this);
        }
        if (ddlMonth.Items.Count > 0)
        {


            if (ddlMonth.SelectedValue == "" || ddlMonth.SelectedValue == "-1")
            {
                isValid = false;
                aShowMessage.ShowMessageBox("Month Required", this);

            }
        }
        List<string> selectedExIn = radExIn.Items.Cast<ListItem>()
                        .Where(li => li.Selected)
                        .Select(li => li.Value)
                        .ToList();
        List<string> selectedForLo = rad_fLocal.Items.Cast<ListItem>()
            .Where(li => li.Selected)
            .Select(li => li.Value)
            .ToList();

        if (selectedExIn.Count == 0)
        {
            isValid = false;
            aShowMessage.ShowMessageBox("External or Internal Required", this);
        }
        if (selectedForLo.Count == 0)
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Foreign or Local Required", this);
        }

        return isValid;
    }

    public bool SaveValidation()
    {
        bool isValid = true;
        if (ddlCompany.SelectedValue == "" || ddlCompany.SelectedValue == "-1")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Company Required", this);
        }
        if (txtToalYearlyBudget.Text == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Yearly Budget Required", this);
        }
        if (txt_toalBudget.Text == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox(" Budget Required", this);
        }

        if (gv_training.Rows.Count==0)
        {
            isValid = false;
            aShowMessage.ShowMessageBox(" Training Details Required ", this);
        }
        return isValid;
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
       Response.Redirect("TrainingBudget2.aspx");
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (SaveValidation() == true)
        {
            TrainingBudget2Master aMaster = new TrainingBudget2Master();


            aMaster.TrainingBudget2Id = hdpk.Value == "" ? 0 : Convert.ToInt32(hdpk.Value);
            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);
            aMaster.TotalYearlyBudgetCost = Convert.ToDecimal(txtToalYearlyBudget.Text.ToString());
            aMaster.TotalBudget = Convert.ToDecimal(txt_toalBudget.Text.ToString());


            int ok = _trainingDal.SaveTrainingBudget2Master(aMaster, Session["UserId"].ToString());
            if (ok > 0)
            {
                SaveAppLog(ok);
                List<TrainingBudget2Details> aList = new List<TrainingBudget2Details>();


                for (int i = 0; i < gv_training.Rows.Count; i++)
                {
                    Label TrainingTitle = (Label)gv_training.Rows[i].FindControl("TrainingTitle");
                    Label ExpectedResult = (Label)gv_training.Rows[i].FindControl("ExpectedResult");
                    Label Quater = (Label)gv_training.Rows[i].FindControl("Quater");
                    Label Grade = (Label)gv_training.Rows[i].FindControl("Grade");
                    Label Category = (Label)gv_training.Rows[i].FindControl("Category");
                    Label InternalExternal = (Label)gv_training.Rows[i].FindControl("InternalExternal");
                    Label ForeignLocal = (Label)gv_training.Rows[i].FindControl("ForeignLocal");
                    Label MonthId = (Label)gv_training.Rows[i].FindControl("MonthId");
                    Label TotalParticipant = (Label)gv_training.Rows[i].FindControl("TotalParticipant");
                    Label BudgetCostParticipant = (Label)gv_training.Rows[i].FindControl("BudgetCostParticipant");
                    Label Budget = (Label)gv_training.Rows[i].FindControl("Budget");
                    Label Referance = (Label)gv_training.Rows[i].FindControl("Referance");
                    Label Remarks = (Label)gv_training.Rows[i].FindControl("Remarks");

                    TrainingBudget2Details aDetails = new TrainingBudget2Details();
                    aDetails.TrainingBudget2DetailsId = Convert.ToInt32(gv_training.DataKeys[i][0].ToString());
                    aDetails.TrainingBudget2Id = ok;
                    aDetails.TrainingTitle = TrainingTitle.Text.ToString();
                    aDetails.ExpectedResult = ExpectedResult.Text.ToString();
                    aDetails.Quater = Quater.Text;
                    aDetails.Grade = Grade.Text;
                    aDetails.EmpCategoryId = Category.Text;
                    if (Convert.ToInt32(MonthId.Text)!=0)
                    {
                        aDetails.MonthId = Convert.ToInt32(MonthId.Text.ToString());
                    }
                    else
                    {

                        aDetails.MonthId = 0;
                    }
                              
                    aDetails.IsInternal = InternalExternal.Text.Trim() == "Internal" ? true : false;
                              aDetails.IsExternal =InternalExternal.Text.Trim() == "External" ? true : false;
                              aDetails.IsForeign = ForeignLocal.Text.Trim() == "Foreign" ? true : false;
                              aDetails.IsLocal =ForeignLocal.Text.Trim() == "Local" ? true : false;
                    aDetails.TotalParticipant = Convert.ToInt32(TotalParticipant.Text);
                              aDetails.BudgetCostParticipant = Convert.ToDecimal(BudgetCostParticipant.Text);
                              aDetails.Budget = Convert.ToDecimal(Budget.Text);
                              aDetails.Referance = Referance.Text.ToString();
                              aDetails.Remarks = Remarks.Text.ToString();
                    aDetails.EntryBy = Session["UserId"].ToString();
                    aDetails.EntryDate = DateTime.Now;
                    aDetails.UpdateBy = Session["UserId"].ToString();
                    aDetails.UpdateDate = DateTime.Now;
                              aList.Add(aDetails);
                }


                bool result = false;
                if (aList.Count > 0)
                {
                     result = _trainingDal.SaveTrainingBudget2Details(aList, ok);
                }
                if (result == true)
                {
                    //AlertMessageBoxShow("Operation Successful...");
                    gv_training.DataSource = null;
                    gv_training.DataBind();
                    Clear();

                    ViewState["TrainingDetails"] = null;

                    ViewState.Remove("TrainingDetails");

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='TrainingBudget2List.aspx';",
                    true);
                }
               
            }
        }
    }

    public void Clear()
    {
        ddlCompany.SelectedIndex = 0;
        ddlFinancialYear.SelectedIndex = 0;
        txtToalYearlyBudget.Text = string.Empty;
    }
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    public void SaveAppLog(int id)
    {
        TrainingBudget2Master aMaster = new TrainingBudget2Master();
        aMaster.TrainingBudget2Id
            = Convert.ToInt32(id);
        aMaster.ActionStatus = "Verified";
        bool status = _trainingDal.UpdateContractural(aMaster.ActionStatus, aMaster.TrainingBudget2Id);



        int commentid = _trainingDal.SaveComment("0", Session["EmpInfoId"].ToString(),
        " ");

        TrainingBudget2MasterAppLogDAO appLogDaoa = new TrainingBudget2MasterAppLogDAO();

        appLogDaoa.ActionStatus = "Drafted";
        appLogDaoa.ApproveDate = DateTime.Now;
        appLogDaoa.ApproveBy = Session["UserId"].ToString();
        appLogDaoa.PreEmpInfoId = Convert.ToInt32(0);
        appLogDaoa.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
        appLogDaoa.TrainingBudget2Id = id;
        appLogDaoa.Comments = "";
        appLogDaoa.CommentsId = commentid;

        int idd = _trainingDal.SavAppLog(appLogDaoa);


        DataTable dtempdata = _trainingDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
        TrainingBudget2MasterAppLogDAO appLogDao = new TrainingBudget2MasterAppLogDAO();
        {
            appLogDao.ActionStatus = "Verified";
            appLogDao.ApproveDate = DateTime.Now;
            appLogDao.ApproveBy = Session["UserId"].ToString();
            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
            appLogDao.TrainingBudget2Id = aMaster.TrainingBudget2Id;
            appLogDao.Comments = "";
            appLogDao.CommentsId = commentid;

        };
        int iddddd = _trainingDal.SavAppLog(appLogDao);
        _trainingDal.UpdateJobReqStatus2(aMaster);
    }
    protected void LinkButton1_OnClick(object sender, EventArgs e)
    {
        if (ViewState["TrainingDetails"] != null)
        {
            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            int i = gvRow.RowIndex;

            Label TrainingTitle = (Label)gv_training.Rows[i].FindControl("TrainingTitle");
            Label ExpectedResult = (Label)gv_training.Rows[i].FindControl("ExpectedResult");
            Label Quater = (Label)gv_training.Rows[i].FindControl("Quater");
            Label Grade = (Label)gv_training.Rows[i].FindControl("Grade");
            Label Category = (Label)gv_training.Rows[i].FindControl("Category");
            Label InternalExternal = (Label)gv_training.Rows[i].FindControl("InternalExternal");
            Label ForeignLocal = (Label)gv_training.Rows[i].FindControl("ForeignLocal");
            Label MonthId = (Label)gv_training.Rows[i].FindControl("MonthId");
            Label TotalParticipant = (Label)gv_training.Rows[i].FindControl("TotalParticipant");
            Label BudgetCostParticipant = (Label)gv_training.Rows[i].FindControl("BudgetCostParticipant");
            Label Budget = (Label)gv_training.Rows[i].FindControl("Budget");
            Label Referance = (Label)gv_training.Rows[i].FindControl("Referance");
            Label Remarks = (Label)gv_training.Rows[i].FindControl("Remarks");
            txt_TrainingTitle.Text = TrainingTitle.Text;
            txt_ExpectedOutcome.Text = ExpectedResult.Text;
            txt_totalQty.Text = TotalParticipant.Text;
            txt_CostPerParticipant.Text = BudgetCostParticipant.Text;
            txt_budget.Text = Budget.Text;
            ddlEmpCategoryEx.SelectedValue = Category.Text;
            LoadGrade();
            foreach (ListItem item in radExIn.Items)
            {
                if (item.Text.Trim()==InternalExternal.Text.Trim())
                {
                    item.Selected = true;
                }
            }
            foreach (ListItem item in rad_fLocal.Items)
            {
                if (item.Text.Trim()==ForeignLocal.Text.Trim())
                {
                    item.Selected = true;
                }
            }
            string[] quater = new[] {""};
            if (Quater.Text.Contains(','))
            {
                quater = Quater.Text.Split(',');
            }
            else
            {
                quater = new[] {Quater.Text};
            }
            foreach (string name in quater)
            {
                foreach (ListItem item in ddlQuater.Items)
                {
                    if (item.Text==name)
                    {
                        item.Selected = true;
                    }
                }
            }
            string[] grade = new[] { "" };
            if (Grade.Text.Contains(','))
            {
                grade = Grade.Text.Split(',');
            }
            else
            {
                grade = new[] { Grade.Text };
            }
            foreach (string name in grade)
            {
                foreach (ListItem item in chk_Grade.Items)
                {
                    if (item.Text == name)
                    {
                        item.Selected = true;
                    }
                }
            }
            txt_ref.Text = Referance.Text;
            txt_remarks.Text = Remarks.Text;
           
            int count = 0;
            for (int j = 0; j < ddlQuater.Items.Count; j++)
            {
                if (ddlQuater.Items[j].Selected)
                {
                    count++;
                    if (count >= 2)
                    {
                        break;
                    }
                }
            }
            if (count < 2)
            {


                string aValue = ddlQuater.SelectedValue.ToString();
                DataTable dta = _trainingDal.GetMonthByQuater(Convert.ToInt32(aValue));
                ddlMonth.DataSource = dta;
                ddlMonth.DataTextField = "TextField";
                ddlMonth.DataValueField = "Value";
                ddlMonth.DataBind();
            }
            else
            {
                ddlMonth.Items.Clear();
            }
            if (ddlMonth.Items.Count>0)
            {
                ddlMonth.SelectedValue = MonthId.Text;
            }
            DataTable dt = (DataTable)ViewState["TrainingDetails"];
            dt.Rows.Remove(dt.Rows[rowID]);
            ViewState["TrainingDetails"] = dt;
            gv_training.DataSource = dt;
            gv_training.DataBind();
            CalculateBudget();
        }
    }

    protected void txt_totalQty_OnTextChanged(object sender, EventArgs e)
    {
        
        CalculateBudgetPer();
    }

    public void CalculateBudgetPer()
    {
        try
        {
            decimal qty = 0;
            qty = string.IsNullOrEmpty(txt_totalQty.Text) ? 0 : Convert.ToDecimal(txt_totalQty.Text);
            decimal amount = 0;
            amount = string.IsNullOrEmpty(txt_budget.Text) ? 0 : Convert.ToDecimal(txt_budget.Text);
            txt_CostPerParticipant.Text = (amount / qty).ToString("F");
        }
        catch (Exception)
        {
            //throw;
        }

    }

    protected void txt_budget_OnTextChanged(object sender, EventArgs e)
    {
        CalculateBudgetPer();
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (SaveValidation() == true)
        {
            TrainingBudget2Master aMaster = new TrainingBudget2Master();


            aMaster.TrainingBudget2Id = hdpk.Value == "" ? 0 : Convert.ToInt32(hdpk.Value);
            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);
            aMaster.TotalYearlyBudgetCost = Convert.ToDecimal(txtToalYearlyBudget.Text.ToString());
            aMaster.TotalBudget = Convert.ToDecimal(txt_toalBudget.Text.ToString());


            int ok = _trainingDal.SaveTrainingBudget2Master(aMaster, Session["UserId"].ToString());
            if (ok > 0)
            {

                List<TrainingBudget2Details> aList = new List<TrainingBudget2Details>();


                for (int i = 0; i < gv_training.Rows.Count; i++)
                {
                    Label TrainingTitle = (Label)gv_training.Rows[i].FindControl("TrainingTitle");
                    Label ExpectedResult = (Label)gv_training.Rows[i].FindControl("ExpectedResult");
                    Label Quater = (Label)gv_training.Rows[i].FindControl("Quater");
                    Label Grade = (Label)gv_training.Rows[i].FindControl("Grade");
                    Label Category = (Label)gv_training.Rows[i].FindControl("Category");
                    Label InternalExternal = (Label)gv_training.Rows[i].FindControl("InternalExternal");
                    Label ForeignLocal = (Label)gv_training.Rows[i].FindControl("ForeignLocal");
                    Label MonthId = (Label)gv_training.Rows[i].FindControl("MonthId");
                    Label TotalParticipant = (Label)gv_training.Rows[i].FindControl("TotalParticipant");
                    Label BudgetCostParticipant = (Label)gv_training.Rows[i].FindControl("BudgetCostParticipant");
                    Label Budget = (Label)gv_training.Rows[i].FindControl("Budget");
                    Label Referance = (Label)gv_training.Rows[i].FindControl("Referance");
                    Label Remarks = (Label)gv_training.Rows[i].FindControl("Remarks");

                    TrainingBudget2Details aDetails = new TrainingBudget2Details();
                    aDetails.TrainingBudget2DetailsId = Convert.ToInt32(gv_training.DataKeys[i][0].ToString());
                    aDetails.TrainingBudget2Id = ok;
                    aDetails.TrainingTitle = TrainingTitle.Text.ToString();
                    aDetails.ExpectedResult = ExpectedResult.Text.ToString();
                    aDetails.Quater = Quater.Text;
                    aDetails.Grade = Grade.Text;
                    aDetails.EmpCategoryId = Category.Text;
                    if (Convert.ToInt32(MonthId.Text) != 0)
                    {
                        aDetails.MonthId = Convert.ToInt32(MonthId.Text.ToString());
                    }
                    else
                    {

                        aDetails.MonthId = 0;
                    }

                    aDetails.IsInternal = InternalExternal.Text.Trim() == "Internal" ? true : false;
                    aDetails.IsExternal = InternalExternal.Text.Trim() == "External" ? true : false;
                    aDetails.IsForeign = ForeignLocal.Text.Trim() == "Foreign" ? true : false;
                    aDetails.IsLocal = ForeignLocal.Text.Trim() == "Local" ? true : false;
                    aDetails.TotalParticipant = Convert.ToInt32(TotalParticipant.Text);
                    aDetails.BudgetCostParticipant = Convert.ToDecimal(BudgetCostParticipant.Text);
                    aDetails.Budget = Convert.ToDecimal(Budget.Text);
                    aDetails.Referance = Referance.Text.ToString();
                    aDetails.Remarks = Remarks.Text.ToString();
                    aDetails.EntryBy = Session["UserId"].ToString();
                    aDetails.EntryDate = DateTime.Now;
                    aDetails.UpdateBy = Session["UserId"].ToString();
                    aDetails.UpdateDate = DateTime.Now;
                    aList.Add(aDetails);
                }


                bool result = false;
                if (aList.Count > 0)
                {
                    result = _trainingDal.SaveTrainingBudget2Details(aList, ok);
                }
                if (result == true)
                {
                    //AlertMessageBoxShow("Operation Successful...");
                    gv_training.DataSource = null;
                    gv_training.DataBind();
                    Clear();

                    ViewState["TrainingDetails"] = null;

                    ViewState.Remove("TrainingDetails");

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='TrainingBudget2List.aspx';",
                    true);
                }

            }
        }
    }


    protected void delButton_OnClick(object sender, EventArgs e)
    {
        //LinkButton lb = (LinkButton)sender;
        //GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //int rowID = gvRow.RowIndex;

        //HiddenField hdpk = (HiddenField)gv_trainingBgtList.Rows[rowID].FindControl("hdpk");

        bool result = _trainingDal.DeleteBudget2(Convert.ToInt32(hdpk.Value), Session["UserId"].ToString());

        if (result == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='TrainingBudget2List.aspx';",
                    true);
            //LoadList();
        }
        else
        {

            AlertMessageBoxShow("Operation Failed...");
            //LoadList();

        }
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void ddlEmpCategoryEx_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpCategoryEx.SelectedIndex > 0)
        {

            LoadGrade();
           
        }
    }

    protected void SelectAll_Checked(object sender, EventArgs e)
    {
        if (SelectAll.Checked)
        {
            foreach (ListItem li in chk_Grade.Items)
            {
                li.Selected = true;
            }
        }
        else
        {
            foreach (ListItem li in chk_Grade.Items)
            {
                li.Selected = false;
            }
        }
    }

    protected void Button2_OnClick(object sender, EventArgs e)
    {
        //if (Validation())
        {


            TrainingBudget2Master aMaster = new TrainingBudget2Master();
            aMaster.TrainingBudget2Id
                = Convert.ToInt32(hdpk.Value);
            aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = _trainingDal.UpdateContractural(aMaster.ActionStatus, Convert.ToInt32(hdpk.Value));
            if (status)
            {
                int commentid = _trainingDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                    commentsTextBox.Text);
                if (aMaster.ActionStatus == "Verified")
                {
                    DataTable dtempdata = _trainingDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    TrainingBudget2MasterAppLogDAO appLogDao = new TrainingBudget2MasterAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                        appLogDao.TrainingBudget2Id = aMaster.TrainingBudget2Id;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;

                    };
                    int id = _trainingDal.SavAppLog(appLogDao);
                    _trainingDal.UpdateJobReqStatus2(aMaster);

                    SenMailForApprved(appLogDao.ForEmpInfoId, " Employee Requisition Form Approval ", @"  <br/> Dear Sir, <br/>
An employee requision is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                }
                else if (aMaster.ActionStatus == "Approved")
                {
                    int empid = 0;
                    DataTable dtempdata = _trainingDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() + "' AND Serial='1'");
                    if (dtempdata.Rows.Count > 0)
                    {
                        empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                    }
                    TrainingBudget2MasterAppLogDAO appLogDao = new TrainingBudget2MasterAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = empid;
                        appLogDao.TrainingBudget2Id = aMaster.TrainingBudget2Id;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;
                    };
                    aMaster.ActionStatus = "Verified";
                    _trainingDal.UpdateJobReqStatus2(aMaster);
                    int id = _trainingDal.SavAppLog(appLogDao);

                    SenMailForApprved(appLogDao.ForEmpInfoId, " Employee Requisition Form Approval ", @"  <br/> Dear Sir, <br/>
An employee requision is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                }
                else if (aMaster.ActionStatus == "Review")
                {
                    DataTable dtempdata = _trainingDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), hdpk.Value);
                    DataTable dtempdata2 = _trainingDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), hdpk.Value);

                    if (dtempdata2.Rows.Count > 0)
                    {
                        TrainingBudget2MasterAppLogDAO appLogDao = new TrainingBudget2MasterAppLogDAO();
                        {
                            appLogDao.ActionStatus = "Verified";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString());
                            appLogDao.TrainingBudget2Id = aMaster.TrainingBudget2Id;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        }

                        _trainingDal.UpdateAppLog("Review", Session["AppLogId"].ToString());
                        int id = _trainingDal.SavAppLog(appLogDao);
                        _trainingDal.UpdateJobReqStatus2(aMaster);
                    }
                    else
                    {
                        ShowMessageBox("Please select Approval Status Approved  this!!!");
                    }

                }


            }
            Session["AppLogId"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Data Saved Successfully...');window.location ='JobRequisitionFormAproval.aspx';",
                       true);
        }
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
                showMessageBox("Email has not Sent, Try Once More time");
            }
            catch (Exception exe)
            {
                showMessageBox("Email has not Sent, Try Once More time");
            }


            System.Threading.Thread.Sleep(100);
        }



    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    protected void ShowMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    private void RadioTextValue()
    {
        //string filepath = Path.GetDirectoryName(Request.Path);
        //filepath = filepath.TrimStart('\\');
        //filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
        DataTable dtdata = null;
        string filepath = "";
        if (Session["AppPage"] != null)
        {
            filepath = Session["AppPage"].ToString();
        }
        if (actionstatusHiddenField.Value == "Approved")
        {
            dtdata = _trainingDal.GetHRAdminEmployeeAppId(" WHERE URL='" + filepath + "' AND Serial IN (SELECT MAX(Serial)AS SL FROM dbo.tblEmployeeApprovalByOpearationDetail" +
                                                                    " LEFT JOIN dbo.tblMainMenu ON dbo.tblEmployeeApprovalByOpearationDetail.Operation=dbo.tblMainMenu.MainMenuId WHERE URL='" + filepath + "'  ) AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "' ORDER BY Serial ASC ");
        }
        else
        {
            dtdata = _trainingDal.GetSupervisorEmployeeAppId(Session["EmpInfoId"].ToString(), entryempinfoIdHiddenField.Value);
        }

        //DataTable dtdata = _trainingDal.GetSupervisorAppId(filepath, " AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");

        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("Value");
        aDataTable.Columns.Add("Text");

        DataRow dataRow = null;


        //if (Session["EmpInfoId"].ToString() != Session["ForEmpInfoId"].ToString())



        if (dtdata.Rows.Count > 0)
        {
            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Approved";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Review";
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
            dataRow["Text"] = "Review";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);
        }

        actionRadioButtonList.DataValueField = "Value";
        actionRadioButtonList.DataTextField = "Text";
        actionRadioButtonList.DataSource = aDataTable;
        actionRadioButtonList.DataBind();

        if (actionstatusHiddenField.Value == "Approved")
        {
            submitButton.Visible = false;
            Button1.Visible = true;
        }
        else
        {
            submitButton.Visible = true;
            Button1.Visible = false;
        }
    }

    protected void Button1_OnClick(object sender, EventArgs e)
    {
        //if (Validation())
        {


            TrainingBudget2Master aMaster = new TrainingBudget2Master();
            aMaster.TrainingBudget2Id
                = Convert.ToInt32(hdpk.Value);
            aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = _trainingDal.UpdateJobReqStatus2(aMaster);
            if (status)
            {
                int commentid = _trainingDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                    commentsTextBox.Text);
                if (aMaster.ActionStatus == "Verified")
                {
                    DataTable dtempdata =
                        _trainingDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                       "' AND EmpInfoId='" + Session["EmpInfoId"].ToString() +
                                                                       "' ");
                    int serial = Convert.ToInt32(dtempdata.Rows[0]["Serial"].ToString()) + 1;
                    DataTable dtempdata2 =
                        _trainingDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                       "' AND Serial='" + serial + "' ");
                    //DataTable dtempdata = _trainingDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    TrainingBudget2MasterAppLogDAO appLogDao = new TrainingBudget2MasterAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["EmpInfoId"].ToString());
                        appLogDao.TrainingBudget2Id = aMaster.TrainingBudget2Id;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;

                    };
                    int id = _trainingDal.SavAppLog(appLogDao);

                    SenMailForApprved(appLogDao.ForEmpInfoId, " Employee Requisition Form Approval ", @"  <br/> Dear Sir, <br/>
An employee requision is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                }
                else if (aMaster.ActionStatus == "Approved")
                {
                    int empid = 0;
                    //DataTable dtempdata = _trainingDal.GetHRAdminEmployeeAppId(" WHERE URL='"+Session["AppPage"].ToString()+"' AND Serial='1'" );
                    //if (dtempdata.Rows.Count>0)
                    //{
                    //    empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                    //}
                    TrainingBudget2MasterAppLogDAO appLogDao = new TrainingBudget2MasterAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = empid;
                        appLogDao.TrainingBudget2Id = aMaster.TrainingBudget2Id;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;
                    };


                    int id = _trainingDal.SavAppLog(appLogDao);

                    SenMailForApprved(appLogDao.ForEmpInfoId, " Employee Requisition Form Approval ", @"  <br/> Dear Sir, <br/>
An employee requision is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                }
                else if (aMaster.ActionStatus == "Review")
                {

                    string actionst = "";
                    DataTable dtdatastatus = _trainingDal.GetAppLogStatus(hdpk.Value.ToString(),
                        Session["EmpInfoId"].ToString());
                    if (dtdatastatus.Rows.Count > 0)
                    {
                        actionst = dtdatastatus.Rows[0]["ActionStatus"].ToString();
                    }

                    DataTable dtempdata = _trainingDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), hdpk.Value);
                    DataTable dtempdata2 = _trainingDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), hdpk.Value);

                    if (dtempdata2.Rows.Count > 0)
                    {
                        TrainingBudget2MasterAppLogDAO appLogDao = new TrainingBudget2MasterAppLogDAO();
                        {
                            appLogDao.ActionStatus = "Verified";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString());
                            appLogDao.TrainingBudget2Id = aMaster.TrainingBudget2Id;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        }

                        _trainingDal.UpdateAppLog("Review", Session["AppLogId"].ToString());

                        if (actionst == "Approved")
                        {
                            _trainingDal.UpdateContractural("Verified", aMaster.TrainingBudget2Id);
                        }

                        int id = _trainingDal.SavAppLog(appLogDao);
                    }
                    else
                    {
                        ShowMessageBox("Please select Approval Status Approved  this!!!");
                    }

                }


            }
            Session["AppLogId"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Data Saved Successfully...');window.location ='JobRequisitionFormAproval.aspx';",
                       true);
        }
    }

    protected void Button2a_OnClick(object sender, EventArgs e)
    {
        TrainingBudget2Master aMaster = new TrainingBudget2Master();
        aMaster.TrainingBudget2Id
            = Convert.ToInt32(hdpk.Value);
        aMaster.ActionStatus = "Rejected";
        bool status = _trainingDal.UpdateContractural(aMaster.ActionStatus, Convert.ToInt32(hdpk.Value));
        int commentid = _trainingDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                commentsTextBox.Text);
        if (aMaster.ActionStatus == "Rejected")
        {
            //DataTable dtempdata = _trainingDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
            TrainingBudget2MasterAppLogDAO appLogDao = new TrainingBudget2MasterAppLogDAO();
            {
                appLogDao.ActionStatus = "Rejected";
                appLogDao.ApproveDate = DateTime.Now;
                appLogDao.ApproveBy = Session["UserId"].ToString();
                appLogDao.PreEmpInfoId = 0;
                appLogDao.ForEmpInfoId = 0;
                appLogDao.TrainingBudget2Id = aMaster.TrainingBudget2Id;
                appLogDao.Comments = commentsTextBox.Text;
                appLogDao.CommentsId = commentid;

            };
            int id = _trainingDal.SavAppLog(appLogDao);
            _trainingDal.UpdateJobReqStatus2(aMaster);
        }
        Session["AppLogId"] = null;
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Rejected Successfully...');window.location ='JobRequisitionFormAproval.aspx';",
                   true);
    }


}