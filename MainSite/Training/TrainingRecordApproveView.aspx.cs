using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.InternalCls;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Training_TrainingRecord : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    public TrainingRecordDAL _recordDal = new TrainingRecordDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private int mid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInitialDDL();
            ButtonVisible();
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();
                DataTable master = _recordDal.GetTrainingRecord(mid);
                DataTable emp = _recordDal.GetTrainingRecordEmployee(mid);

                ddlCompany.SelectedValue = master.Rows[0]["CompanyId"].ToString();
                ddlCompany_OnSelectedIndexChanged(ddlCompany, (EventArgs)e);

                ddlFinancialYear.SelectedValue = master.Rows[0]["FinancialYearId"].ToString();
                ddlFinancialYear_OnSelectedIndexChanged(ddlFinancialYear, (EventArgs)e);

                ddlTrainingType.SelectedValue = master.Rows[0]["TrainingTypeId"].ToString();
                ddlBudgetHead.SelectedValue = master.Rows[0]["TrainingBudget2Id"].ToString();
                txtTrainingTitle.Text = master.Rows[0]["TrainingTitle"].ToString();
                txtTrainingDetails.Text = master.Rows[0]["TrainingDetails"].ToString();
                ddlTrainingOrg.SelectedValue = master.Rows[0]["TrainingOrgId"].ToString();
                ddlTrainingOrg_OnSelectedIndexChanged(ddlTrainingOrg, (EventArgs)e);
                ddlLocation.SelectedValue = master.Rows[0]["TrainingOrgLocation"].ToString();

                if (!string.IsNullOrEmpty(master.Rows[0]["TrainingVenue"].ToString()))
                {
                    string a = master.Rows[0]["TrainingVenue"].ToString();
                    isSmcVanue.Checked = true;
                    //venueDiv.Visible = true;
                    isSmcVanue_OnCheckedChanged(isSmcVanue, (EventArgs)e);
                    ddlVenue.SelectedValue = master.Rows[0]["TrainingVenue"].ToString();
                    ddlVenue.SelectedValue = master.Rows[0]["TrainingVenue"].ToString();
                }

                DataTable tr = _recordDal.GetTrainerTrainingRecord(mid);
                gvTrainner.DataSource = _recordDal.GetTrainerTrainingRecord(mid);
                gvTrainner.DataBind();
                ViewState["TrainerTable"] = tr;
                
                
                DataTable empGrd = _recordDal.GetTrainingRecordEmployee(mid);
                gv_selectedEmp.DataSource = empGrd;
                gv_selectedEmp.DataBind();

                ViewState["EmpSelect"] = empGrd;
                txtTotalParticipant.Text = _recordDal.GetTrainingRecordEmployee(mid).Rows.Count.ToString();
                txtTrainingCost.Text = master.Rows[0]["TrainingCost"].ToString();
                txtLogistic.Text = master.Rows[0]["LogisticCost"].ToString();
                txtOtherCost.Text = master.Rows[0]["OtherCost"].ToString();
                txtGrandTotal.Text = master.Rows[0]["GrandTotal"].ToString();
                txtCostPerParticipant.Text = master.Rows[0]["CostPerParticipant"].ToString();


                txtTotalDays.Text = master.Rows[0]["NoOfDays"].ToString();
                txtStartDate.Text = Convert.ToDateTime(master.Rows[0]["StartDate"].ToString()).ToString("dd-MMM-yyyy");
                txtEndDate.Text = Convert.ToDateTime(master.Rows[0]["EndDate"].ToString()).ToString("dd-MMM-yyyy");
                txtStartTime.Text = master.Rows[0]["StartTime"].ToString();
                txtEndTime.Text = master.Rows[0]["EndTime"].ToString();
                txtTotalTrainingHoures.Text = master.Rows[0]["TotalHoure"].ToString();


                string days = master.Rows[0]["TrainingDays"].ToString();
                string[] dayStrings = days.Split(':');

                for (int i = 0; i < dayStrings.Length; i++)
                {
                    if (!string.IsNullOrEmpty(dayStrings[i]))
                    {
                        foreach (ListItem item in chkDays.Items.Cast<ListItem>().ToList())
                        {
                            if (item.Value == dayStrings[i])
                            {
                                item.Selected = true;
                            }
                        }
                    }
                }

            }
            DataLoad();
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

    }
    public void DataLoad()
    {
        ClsApprovalAction approvalAction = new ClsApprovalAction();
        
        string userName = Session["UserId"].ToString();
        
        approvalAction.LoadActionControlByUser(jobreqRadioButtonList, Session["ApprovalPage"].ToString(), userName);
        Session["ApprovalPage"] = null;
        RadItemRemove();
        submitButton.Text = "Submit";

    }

    public void RadItemRemove()
    {
        int[] id = new int[5];
        int count = 0;
        for (int i = 0; i < jobreqRadioButtonList.Items.Count; i++)
        {

            if (jobreqRadioButtonList.Items[i].Enabled == false)
            {
                id[count] = Convert.ToInt32(jobreqRadioButtonList.Items[i].Value);
                count++;

            }
        }
        foreach (int a in id)
        {
            for (int i = 0; i < jobreqRadioButtonList.Items.Count; i++)
            {

                if (jobreqRadioButtonList.Items[i].Value == a.ToString())
                {

                    jobreqRadioButtonList.Items.RemoveAt(i);
                }
            }
        }
    }


    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }


    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("TrainingRecords.aspx");
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ClearGrid();
        Session["CompanyId"] = ddlCompany.SelectedValue;
        DataTable dt = _trainingDal.GetFianncialYearByComIdDDl(Convert.ToInt32(ddlCompany.SelectedValue));
        ddlFinancialYear.DataSource = dt;
        ddlFinancialYear.DataValueField = "Value";
        ddlFinancialYear.DataTextField = "TextField";
        ddlFinancialYear.DataBind();

    }

    private void ClearGrid()
    {

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



        DataTable org = _trainingDal.GetTrainingOrgDDl();
        ddlTrainingOrg.DataSource = org;
        ddlTrainingOrg.DataValueField = "Value";
        ddlTrainingOrg.DataTextField = "TextField";
        ddlTrainingOrg.DataBind();

        DataTable trainingType = _recordDal.GetTrainingType();
        ddlTrainingType.DataSource = trainingType;
        ddlTrainingType.DataValueField = "Value";
        ddlTrainingType.DataTextField = "TextField";
        ddlTrainingType.DataBind();

        List<string> days = new List<string>();
       // Dictionary<int, string> daysOfWeek = new Dictionary<int, string>();
        for (int i = 0; i < 7; i++)
        {
            //daysOfWeek.Add(i, Enum.GetName(typeof(DayOfWeek), i));
            days.Add(Enum.GetName(typeof(DayOfWeek), i));
        }
        chkDays.DataSource = days;
        chkDays.DataBind();

    }

    protected void ddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        DataTable dt = _recordDal.GetTrainingHeadByYear(Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlCompany.SelectedValue));
        ddlBudgetHead.DataSource = dt;

        ddlBudgetHead.DataValueField = "Value";
        ddlBudgetHead.DataTextField = "TextField";
        ddlBudgetHead.DataBind();
    }


    

    protected void AddTrainner_OnClick(object sender, EventArgs e)
    {
        if (ViewState["TrainerTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["TrainerTable"];

            DataRow drCurrentRow = null;
            // drCurrentRow = new DataRow();
            if (dtCurrentTable.Rows.Count > 0 && ValidateTrainnerGrid(Convert.ToInt32(ddlTrainer.SelectedValue)) == true)
            {

                drCurrentRow = dtCurrentTable.NewRow();

                DataTable trainer = _trainingDal.GetTrainnerInfo(Convert.ToInt32(ddlTrainer.SelectedValue));
                drCurrentRow["TrainerName"] = trainer.Rows[0]["TrainerName"];
                drCurrentRow["TrainerDetails"] = trainer.Rows[0]["TrainerDetails"];
                drCurrentRow["TrainerId"] = trainer.Rows[0]["TrainerId"];

                dtCurrentTable.Rows.Add(drCurrentRow);


                ViewState["TrainerTable"] = dtCurrentTable;


                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    Label lblName = (Label)gvTrainner.Rows[i].FindControl("txt_Trainner");
                    Label lblDetails = (Label)gvTrainner.Rows[i].FindControl("txt_TrainnerDetails");
                    Label lblid = (Label)gvTrainner.Rows[i].FindControl("txt_trainerID");

                    lblName.Text = dtCurrentTable.Rows[i]["TrainerName"].ToString();
                    lblDetails.Text = dtCurrentTable.Rows[i]["TrainerDetails"].ToString();
                    lblid.Text = dtCurrentTable.Rows[i]["TrainerId"].ToString();
                }

                gvTrainner.DataSource = dtCurrentTable;
                gvTrainner.DataBind();
            }
        }

        else
        {


            DataTable trainer = _trainingDal.GetTrainnerInfo(Convert.ToInt32(ddlTrainer.SelectedValue));

            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("TrainerName", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainerDetails", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainerId", typeof(string)));
            //dt.Columns.Add(new DataColumn("TrainerId", typeof(string)));
            dr = dt.NewRow();
            dr["TrainerName"] = trainer.Rows[0]["TrainerName"];
            dr["TrainerDetails"] = trainer.Rows[0]["TrainerDetails"];
            dr["TrainerId"] = trainer.Rows[0]["TrainerId"];
            //dr["TrainerName"] = ddlTrainer.SelectedItem;
            dt.Rows.Add(dr);

            ViewState["TrainerTable"] = dt;
            gvTrainner.DataSource = dt;
            gvTrainner.DataBind();
        }
    }

    protected void notListedCheck_OnCheckedChanged(object sender, EventArgs e)
    {
        if (notListedCheck.Checked)
        {
            notListedNameDiv.Visible = true;
            notListedDetailsDiv.Visible = true;
            AddNotListed.Visible = true;
        }
        else
        {
            notListedNameDiv.Visible = false;
            notListedDetailsDiv.Visible = false;
            AddNotListed.Visible = false;
        }
    }

    protected void AddNotListed_OnClick(object sender, EventArgs e)
    {
        if (ViewState["TrainerTable"] != null)
        {

            DataTable dtCurrentTable = (DataTable)ViewState["TrainerTable"];

            DataRow drCurrentRow = null;
            // drCurrentRow = new DataRow();
            if (dtCurrentTable.Rows.Count > 0)
            {

                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["TrainerName"] = txt_NotListedTrainer.Text.Trim();
                drCurrentRow["TrainerDetails"] = txt_NotListedTrainerDetails.Text.Trim();
                drCurrentRow["TrainerId"] = 0;
                dtCurrentTable.Rows.Add(drCurrentRow);


                ViewState["TrainerTable"] = dtCurrentTable;


                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    Label lblName = (Label)gvTrainner.Rows[i].FindControl("txt_Trainner");
                    Label lblDetails = (Label)gvTrainner.Rows[i].FindControl("txt_TrainnerDetails");
                    Label lblid = (Label)gvTrainner.Rows[i].FindControl("txt_trainerID");

                    lblName.Text = dtCurrentTable.Rows[i]["TrainerName"].ToString();
                    lblDetails.Text = dtCurrentTable.Rows[i]["TrainerDetails"].ToString();
                    lblid.Text = dtCurrentTable.Rows[i]["TrainerId"].ToString();
                }

                gvTrainner.DataSource = dtCurrentTable;
                gvTrainner.DataBind();
            }
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("TrainerName", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainerDetails", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainerId", typeof(string)));

            dr = dt.NewRow();
            dr["TrainerName"] = txt_NotListedTrainer.Text.Trim();
            dr["TrainerDetails"] = txt_NotListedTrainerDetails.Text.Trim();
            dr["TrainerId"] = 0;
            //dr["TrainerName"] = ddlTrainer.SelectedItem;
            dt.Rows.Add(dr);

            ViewState["TrainerTable"] = dt;
            gvTrainner.DataSource = dt;
            gvTrainner.DataBind();
        }
        txt_NotListedTrainer.Text = null;
        txt_NotListedTrainerDetails.Text = null;
    }


    private bool ValidateTrainnerGrid(int id)
    {
        bool isValid = true;
        for (int i = 0; i < gvTrainner.Rows.Count; i++)
        {

            Label lblid = (Label)gvTrainner.Rows[i].FindControl("txt_trainerID");

            int trainerId = Convert.ToInt32(lblid.Text);
            if (id == trainerId)
            {


                aShowMessage.ShowMessageBox("Trainner Already Exists", this);
                isValid = false;
                break;
            }

        }

        return isValid;
    }

    protected void lb_RemoveTrainer_OnClick(object sender, EventArgs e)
    {
        if (ViewState["TrainerTable"] != null)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["TrainerTable"];

            dt.Rows.Remove(dt.Rows[rowID]);

            gvTrainner.DataSource = dt;
            gvTrainner.DataBind();
        }
    }

    protected void btnAddEmployee_OnClick(object sender, EventArgs e)
    {


        mpe_1.Show();
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {

            pop_ddlCompany.DataSource = dt;
            pop_ddlCompany.DataValueField = "Value";
            pop_ddlCompany.DataTextField = "TextField";
            pop_ddlCompany.DataBind();
        }

    }

 

    protected void pop_ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        pop_ddlDepartment.DataSource = _recordDal.GetDepartmentByCompany(Convert.ToInt32(pop_ddlCompany.SelectedValue));
        pop_ddlDepartment.DataValueField = "Value";
        pop_ddlDepartment.DataTextField = "TextField";
        pop_ddlDepartment.DataBind();


        pop_ddlGrade.DataSource = _recordDal.GetSalaryGrade();
        pop_ddlGrade.DataValueField = "Value";
        pop_ddlGrade.DataTextField = "TextField";
        pop_ddlGrade.DataBind();
    }


    protected void pop_btnSearch_OnClick(object sender, EventArgs e)
    {


        int company = string.IsNullOrEmpty(pop_ddlCompany.SelectedValue)
            ? 0
            : pop_ddlCompany.SelectedValue == "-1" ? 0 : Convert.ToInt32(pop_ddlCompany.SelectedValue);
        int department = string.IsNullOrEmpty(pop_ddlDepartment.SelectedValue)
      ? 0
      : pop_ddlDepartment.SelectedValue == "-1" ? 0 : Convert.ToInt32(pop_ddlDepartment.SelectedValue);
        int grade = string.IsNullOrEmpty(pop_ddlGrade.SelectedValue)
       ? 0
       : pop_ddlGrade.SelectedValue == "-1" ? 0 : Convert.ToInt32(pop_ddlGrade.SelectedValue);

        DataTable dt = _recordDal.GetEmployee(company, department, grade);
        gv_allocateEmp.DataSource = dt; 
        gv_allocateEmp.DataBind();

    }



    private bool validateAddEmp()
    {
        bool result = true;
        int totalCount = gv_allocateEmp.Rows.Cast<GridViewRow>()
.Count(r => ((CheckBox)r.FindControl("txt_check")).Checked);

        if (totalCount == 0)
        {
            result = false;
            aShowMessage.ShowMessageBox("Please Select Employee !! ", this);
            

        }

        for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
        {
            CheckBox lb_empCheck = (CheckBox)gv_allocateEmp.Rows[i].FindControl("txt_check");
            HiddenField empInfoId = (HiddenField)gv_allocateEmp.Rows[i].FindControl("empInfoId");
            if (lb_empCheck.Checked)
            {
                string emp = empInfoId.Value.Trim();
                for (int j = 0; j < gv_selectedEmp.Rows.Count; j++)
                {
                    HiddenField empInfoId2 = (HiddenField)gv_selectedEmp.Rows[j].FindControl("empInfoId");
                    if (empInfoId2.Value == emp)
                    {
                        result = false;
                       // aShowMessage.ShowMessageBox("Employee Already Exists ", this);
            
                    }
                }
            }
           
        }

        return result;

    }

  

    protected void btnselectedEmpRemove_OnClick(object sender, EventArgs e)
    {
        if (ViewState["EmpSelect"] != null )
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["EmpSelect"];

            dt.Rows.Remove(dt.Rows[rowID]);
            txtTotalParticipant.Text = dt.Rows.Count.ToString();
            
           
            ViewState["EmpSelect"] = dt;
            gv_selectedEmp.DataSource = dt;
            gv_selectedEmp.DataBind();
            txtTrainingCost_OnTextChanged(txtTrainingCost, (EventArgs)e);
        }
    }

    private bool ValidateSave()
    {
        bool isValid = true;
        if (string.IsNullOrEmpty(ddlCompany.SelectedValue) || ddlCompany.SelectedValue == "0" || ddlCompany.SelectedValue == "-1")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Company Required ", this);
        }
        if (string.IsNullOrEmpty(ddlFinancialYear.SelectedValue) || ddlFinancialYear.SelectedValue == "0" || ddlFinancialYear.SelectedValue == "-1")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Financial Year Required ", this);
        }
        if (string.IsNullOrEmpty(ddlTrainingType.SelectedValue) || ddlTrainingType.SelectedValue == "0" || ddlTrainingType.SelectedValue == "-1")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Training Type Required ", this);
        }
        if (string.IsNullOrEmpty(ddlBudgetHead.SelectedValue) || ddlBudgetHead.SelectedValue == "0" || ddlBudgetHead.SelectedValue == "-1")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Training Budget Required ", this);
        }

        if (string.IsNullOrEmpty(txtTrainingTitle.Text.Trim()))
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Training Title Required ", this);
        }
        if (string.IsNullOrEmpty(ddlTrainingOrg.SelectedValue) || ddlTrainingOrg.SelectedValue == "0" || ddlTrainingOrg.SelectedValue == "-1")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Training Organization Required ", this);
        }
        if (string.IsNullOrEmpty(ddlLocation.SelectedValue) || ddlLocation.SelectedValue == "0" || ddlLocation.SelectedValue == "-1" && isSmcVanue.Checked==false)
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Training Location Required ", this);
        }

        if (string.IsNullOrEmpty(ddlVenue.SelectedValue) || ddlVenue.SelectedValue == "0" || ddlVenue.SelectedValue == "-1" && isSmcVanue.Checked)
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Training Venue Required ", this);
        }
        if (string.IsNullOrEmpty(txtTrainingTitle.Text.Trim()))
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Training Title Required ", this);
        }

        if (string.IsNullOrEmpty(txtTotalParticipant.Text.Trim()) || txtTotalParticipant.Text.Trim()=="0")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Participant Required ", this);
        }
        if (string.IsNullOrEmpty(txtTrainingCost.Text.Trim()))
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Training Cost Required ", this);
        }
        if (string.IsNullOrEmpty(txtLogistic.Text.Trim()))
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Logistic Cost Required ", this);
        }
        if (string.IsNullOrEmpty(txtOtherCost.Text.Trim()))
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Other Cost Required ", this);
        }
        if (string.IsNullOrEmpty(txtStartDate.Text.Trim()))
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Training Start Date Required ", this);
        }

        if (string.IsNullOrEmpty(txtEndDate.Text.Trim()))
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Training End Date Required ", this);
        }
        if (string.IsNullOrEmpty(txtStartTime.Text.Trim()))
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Training Start Time Required ", this);
        }
        if (string.IsNullOrEmpty(txtEndTime.Text.Trim()))
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Training End Time Required ", this);
        }
        
        if (string.IsNullOrEmpty(txtEndTime.Text.Trim()))
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Training End Time Required ", this);
        }
        int selectedCount = chkDays.Items.Cast<ListItem>().Count(li => li.Selected);
        if (selectedCount == 0)
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Training Day Required ", this);
        }
        if (gv_selectedEmp.Rows.Count == 0)
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Participants Required ", this);
        }
        if (gvTrainner.Rows.Count == 0)
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Trainer Required ", this);
        }

        return isValid;

    }

    protected void txtEndTime_OnTextChanged(object sender, EventArgs e)
    {
        chkDays_OnSelectedIndexChanged(chkDays, (EventArgs) e);
        

    }


    protected void txtStartTime_OnTextChanged(object sender, EventArgs e)
    {

        chkDays_OnSelectedIndexChanged(chkDays, (EventArgs)e);
    }

    protected void chkDays_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int selectedCount = chkDays.Items.Cast<ListItem>().Count(li => li.Selected);
        List<ListItem> selected = chkDays.Items.Cast<ListItem>()
     .Where(li => li.Selected)
     .ToList();

        double houres = 0;
        if (!string.IsNullOrEmpty(txtEndTime.Text.ToString()) && !string.IsNullOrEmpty(txtStartTime.Text.ToString()))
        {
            TimeSpan endTime = TimeSpan.Parse(txtEndTime.Text.ToString());
            TimeSpan startTime = TimeSpan.Parse(txtStartTime.Text.ToString());
            TimeSpan span = endTime - startTime;
            houres = span.TotalHours;
        }


        if (!string.IsNullOrEmpty(txtStartDate.Text.ToString()) && !string.IsNullOrEmpty(txtEndDate.Text.ToString()))
        {
            DateTime start = Convert.ToDateTime(txtStartDate.Text.Trim());
            DateTime end = Convert.ToDateTime(txtEndDate.Text.Trim());

            double totalDays = (end - start).TotalDays;

            DateTime[] dates = { start, end };

            int DayCount = 0;
           // var mondays = dates.Where(d => d.DayOfWeek == DayOfWeek.Monday);
            foreach (var item in selected)
            {
                ListItem aItem = (ListItem) item;
                var al = aItem.Value;

                for (DateTime dt = start; dt < end; dt = dt.AddDays(1.0))
                {
                    string aa = dt.DayOfWeek.ToString();
                    if (dt.DayOfWeek.ToString() == al)
                    {
                        DayCount = DayCount + 1;
                    }
                }

            }

            txtTotalDays.Text = (DayCount+1).ToString();
            txtTotalTrainingHoures.Text = (houres * (DayCount+1)).ToString();
        }
       
    }

    protected void txtEndDate_OnTextChanged(object sender, EventArgs e)
    {
        chkDays_OnSelectedIndexChanged(chkDays, (EventArgs)e);
    }

    protected void txtStartDate_OnTextChanged(object sender, EventArgs e)
    {
        chkDays_OnSelectedIndexChanged(chkDays, (EventArgs)e);
    }

    protected void txtTrainingCost_OnTextChanged(object sender, EventArgs e)
    {
        double trainingCost = string.IsNullOrEmpty(txtTrainingCost.Text) ? 0 : Convert.ToDouble(txtTrainingCost.Text);
        double logistic = string.IsNullOrEmpty(txtLogistic.Text) ? 0 : Convert.ToDouble(txtLogistic.Text);
        double other = string.IsNullOrEmpty(txtOtherCost.Text) ? 0 : Convert.ToDouble(txtOtherCost.Text);
        double total = trainingCost + logistic + other;
        txtGrandTotal.Text = total.ToString();
        if (gv_selectedEmp.Rows.Count == 0)
        {
            txtCostPerParticipant.Text = "0";
        }
        else
        {
            txtCostPerParticipant.Text = (total/Convert.ToDouble(gv_selectedEmp.Rows.Count)).ToString();
        }
    }

    protected void ddlTrainingOrg_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["TrainerTable"] = null;
        gvTrainner.DataSource = null;
        gvTrainner.DataBind();
        DataTable trainenr = _trainingDal.GetTrainnerDDl(Convert.ToInt32(ddlTrainingOrg.SelectedValue));
        ddlTrainer.DataSource = trainenr;
        ddlTrainer.DataValueField = "Value";
        ddlTrainer.DataTextField = "TextField";
        ddlTrainer.DataBind();

        DataTable location = _trainingDal.GetOrgBranch(Convert.ToInt32(ddlTrainingOrg.SelectedValue));
        
      
            ddlLocation.DataSource = location;
            ddlLocation.DataValueField = "Value";
            ddlLocation.DataTextField = "TextField";
            ddlLocation.DataBind();
            ViewState["Location"] = "OrgBranch";
        
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {

        try
        {
            _trainingDal.UpdateStatus(hdpk.Value,
                    jobreqRadioButtonList.SelectedItem.Text, Session["UserId"].ToString(), DateTime.Now);
            //DataLoad();
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('" + jobreqRadioButtonList.SelectedItem.Text + " Successfully Done');window.location ='TrainingRecordsApproval.aspx';",
                    true);
            //aShowMessage.ShowMessageBox("" + jobreqRadioButtonList.SelectedItem.Text + " Successfully Done", this);
        }
        catch (Exception ex)
        {
            aShowMessage.ShowMessageBox("Please Choose an action for approval", this);
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
    protected List<TrainingDetailsInfo> GetDetailsFromGrid()
    {
        List<TrainingDetailsInfo> detailsList = new List<TrainingDetailsInfo>();
        DataTable dtCurrentTable = (DataTable)ViewState["TrainerTable"];
        if (dtCurrentTable != null)
        {
            for (int i = 0; i < gvTrainner.Rows.Count; i++)
            {
                Label lblName = (Label)gvTrainner.Rows[i].FindControl("txt_Trainner");
                Label lblDetails = (Label)gvTrainner.Rows[i].FindControl("txt_TrainnerDetails");
                Label lblid = (Label)gvTrainner.Rows[i].FindControl("txt_trainerID");
                TrainingDetailsInfo aInfo = new TrainingDetailsInfo();
                int detailsId = Convert.ToInt32(lblid.Text);
                if (detailsId == 0)
                {
                    aInfo.NotListedName = lblName.Text.Trim();
                    aInfo.NotListedDetails = lblDetails.Text.Trim();
                    aInfo.TrainerId = 0;
                }
                else
                {
                    aInfo.TrainerId = detailsId;
                    aInfo.NotListedName = null;
                    aInfo.NotListedDetails = null;
                }
                detailsList.Add(aInfo);
            }


        }
        return detailsList;

    }
    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("TrainingRecord.aspx");
    }

    protected void isSmcVanue_OnCheckedChanged(object sender, EventArgs e)
    {

        if (isSmcVanue.Checked)
        {
            venueDiv.Visible = true;
            DataTable venue = _recordDal.GetTrainingVenue();
            ddlVenue.DataSource = venue;
            ddlVenue.DataValueField = "Value";
            ddlVenue.DataTextField = "TextField";
            ddlVenue.DataBind();
            ViewState["Location"] = "Venue";
        }
        else
        {
            venueDiv.Visible = false;
        }
            

        
    }
    protected void btnEmpSubmit_OnClick(object sender, EventArgs e)
    {
        if (ViewState["EmpSelect"] != null)
        {
            txtTotalParticipant.Text = gv_selectedEmp.Rows.Count.ToString();
            txtTrainingCost_OnTextChanged(txtTrainingCost, (EventArgs)e);
            mpe_1.Hide();
        }
    }

    protected void btnNo_OnClick(object sender, EventArgs e)
    {
        //gv_allocateEmp.DataSource = null;
        //gv_allocateEmp.DataBind();
        ////ViewState["EmpSelect"] = null;
        //gv_selectedEmp.DataSource = null;
        //gv_selectedEmp.DataBind();
        mpe_1.Hide();
    }

    private bool ValidateEmpAdd2(string empInfo)
    {
        bool result = true;
        for (int j = 0; j < gv_selectedEmp.Rows.Count; j++)
        {
            HiddenField empInfoId2 = (HiddenField)gv_selectedEmp.Rows[j].FindControl("empInfoId");
            if (empInfoId2.Value == empInfo)
            {
                result = false;
                break;
                // aShowMessage.ShowMessageBox("Employee Already Exists ", this);

            }
        }
        return result;

    }
    protected void btnAddEmpList_OnClick(object sender, EventArgs e)
    {
        if (true)
        {
            if (ViewState["EmpSelect"] == null)
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("EmpMasterCode", typeof(string)));
                dt.Columns.Add(new DataColumn("EmpInfoId", typeof(string)));
                dt.Columns.Add(new DataColumn("EmpName", typeof(string)));
                dt.Columns.Add(new DataColumn("GradeName", typeof(string)));
                dt.Columns.Add(new DataColumn("DepartmentName", typeof(string)));
                dt.Columns.Add(new DataColumn("Designation", typeof(string)));

                for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
                {
                    CheckBox lb_empCheck = (CheckBox)gv_allocateEmp.Rows[i].FindControl("txt_check");
                    Label txt_empCode = (Label)gv_allocateEmp.Rows[i].FindControl("txt_empCode");
                    Label txt_employee = (Label)gv_allocateEmp.Rows[i].FindControl("txt_employee");
                    Label txt_designation = (Label)gv_allocateEmp.Rows[i].FindControl("txt_designation");
                    Label txt_dptName = (Label)gv_allocateEmp.Rows[i].FindControl("txt_dptName");
                    Label txt_grdName = (Label)gv_allocateEmp.Rows[i].FindControl("txt_grdName");
                    HiddenField empInfoId = (HiddenField)gv_allocateEmp.Rows[i].FindControl("empInfoId");
                    if (lb_empCheck.Checked && ValidateEmpAdd2(empInfoId.Value) == true)
                    {
                        dr = dt.NewRow();
                        dr["EmpMasterCode"] = txt_empCode.Text.Trim();
                        dr["EmpInfoId"] = empInfoId.Value.Trim();
                        dr["EmpName"] = txt_employee.Text.Trim();
                        dr["GradeName"] = txt_grdName.Text.Trim();
                        dr["DepartmentName"] = txt_dptName.Text.Trim();
                        dr["Designation"] = txt_designation.Text.Trim();
                        dt.Rows.Add(dr);
                    }

                }
                gv_selectedEmp.DataSource = dt;

                gv_selectedEmp.DataBind();
                ViewState["EmpSelect"] = dt;
                //txtTotalParticipant.Text = dt.Rows.Count.ToString();
                //txtTrainingCost_OnTextChanged(txtTrainingCost, (EventArgs)e);
                //gv_allocateEmp.DataSource = null;
                //gv_allocateEmp.DataBind();


                //mpe_1.Hide();

            }
            else
            {
                DataTable dtCurrentTable = (DataTable)ViewState["EmpSelect"];

                DataRow drCurrentRow = null;
                for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
                {
                    CheckBox lb_empCheck = (CheckBox)gv_allocateEmp.Rows[i].FindControl("txt_check");
                    Label txt_empCode = (Label)gv_allocateEmp.Rows[i].FindControl("txt_empCode");
                    Label txt_employee = (Label)gv_allocateEmp.Rows[i].FindControl("txt_employee");
                    Label txt_designation = (Label)gv_allocateEmp.Rows[i].FindControl("txt_designation");
                    Label txt_dptName = (Label)gv_allocateEmp.Rows[i].FindControl("txt_dptName");
                    Label txt_grdName = (Label)gv_allocateEmp.Rows[i].FindControl("txt_grdName");
                    HiddenField empInfoId = (HiddenField)gv_allocateEmp.Rows[i].FindControl("empInfoId");
                    if (lb_empCheck.Checked && ValidateEmpAdd2(empInfoId.Value) == true)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();
                        drCurrentRow["EmpMasterCode"] = txt_empCode.Text.Trim();
                        drCurrentRow["EmpInfoId"] = empInfoId.Value.Trim();
                        drCurrentRow["EmpName"] = txt_employee.Text.Trim();
                        drCurrentRow["GradeName"] = txt_grdName.Text.Trim();
                        drCurrentRow["DepartmentName"] = txt_dptName.Text.Trim();
                        drCurrentRow["Designation"] = txt_designation.Text.Trim();
                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }

                }
                gv_selectedEmp.DataSource = dtCurrentTable;
                gv_selectedEmp.DataBind();
                ViewState["EmpSelect"] = dtCurrentTable;
                //txtTotalParticipant.Text = dtCurrentTable.Rows.Count.ToString();
                //txtTrainingCost_OnTextChanged(txtTrainingCost, (EventArgs)e);
                //gv_allocateEmp.DataSource = null;
                //gv_allocateEmp.DataBind();
               // mpe_1.Hide();
            }
        }
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (ValidateSave())
        {


            TrainingRecordMaster aMaster = new TrainingRecordMaster();
            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);
            aMaster.TrainingTypeId = Convert.ToInt32(ddlTrainingType.SelectedValue);
            aMaster.TrainingBudget2Id = Convert.ToInt32(ddlBudgetHead.SelectedValue);
            aMaster.TrainingTitle = txtTrainingTitle.Text.Trim();
            aMaster.TrainingDetails = txtTrainingDetails.Text.Trim();
            aMaster.TrainingOrgId = Convert.ToInt32(ddlTrainingOrg.SelectedValue);
            string check = ViewState["Location"].ToString();
            if (isSmcVanue.Checked == false)
            {
                aMaster.TrainingOrgLocation = Convert.ToInt32(ddlLocation.SelectedValue);
                aMaster.TrainingVenue = 0;
            }
            if (isSmcVanue.Checked)
            {
                aMaster.TrainingVenue = Convert.ToInt32(ddlVenue.SelectedValue);
                aMaster.TrainingOrgLocation = 0;
            }
            aMaster.TrainingCost = Convert.ToDecimal(txtTrainingCost.Text.Trim());
            aMaster.LogisticCost = Convert.ToDecimal(txtLogistic.Text.Trim());
            aMaster.OtherCost = Convert.ToDecimal(txtOtherCost.Text.Trim());
            aMaster.GrandTotal = Convert.ToDecimal(txtGrandTotal.Text.Trim());
            aMaster.CostPerParticipant = Convert.ToDecimal(txtCostPerParticipant.Text.Trim());

            List<ListItem> selected = chkDays.Items.Cast<ListItem>()
                .Where(li => li.Selected)
                .ToList();
            string days = "";
            foreach (ListItem item in selected)
            {
                days += ":" + item.Value;
            }
            aMaster.TrainingDays = days;

            aMaster.NoOfDays = Convert.ToInt32(txtTotalDays.Text.ToString());
            aMaster.StartDate = Convert.ToDateTime(txtStartDate.Text.Trim());
            aMaster.EndDate = Convert.ToDateTime(txtEndDate.Text.Trim());
            aMaster.StartTime = TimeSpan.Parse(txtStartTime.Text.ToString());
            aMaster.EndTime = TimeSpan.Parse(txtEndTime.Text.ToString());
            aMaster.TotalHoure = Convert.ToDecimal(txtTotalTrainingHoures.Text.Trim());
            bool result = false;
            int pk = 0;


            if (string.IsNullOrEmpty(hdpk.Value))
            {
                pk = _recordDal.SaveTrainingRecodMaster(aMaster, Convert.ToInt32(Session["UserId"].ToString()));
            }
            else
            {
                pk = _recordDal.UpdateTrainingRecord(aMaster, Convert.ToInt32(hdpk.Value), Convert.ToInt32(Session["UserId"].ToString()));
            }
            if (pk > 0)
            {
                List<TrainingRecordDetailsEmployee> aEmpList = new List<TrainingRecordDetailsEmployee>();
                for (int i = 0; i < gv_selectedEmp.Rows.Count; i++)
                {
                    TrainingRecordDetailsEmployee aEmployee = new TrainingRecordDetailsEmployee();
                    HiddenField empInfoId2 = (HiddenField)gv_selectedEmp.Rows[i].FindControl("empInfoId");
                    aEmployee.TrainingRecordMasterId = pk;
                    aEmployee.EmpInfoId = Convert.ToInt32(empInfoId2.Value);
                    aEmpList.Add(aEmployee);
                }
                result = _recordDal.SaveTrainingRecordEmployee(aEmpList, pk);


                List<TrainingDetailsInfo> trainer = GetDetailsFromGrid();
                result = _recordDal.SaveTrainingDetails(trainer, pk);

            }
            if (result == true)
            {

                ViewState["EmpSelect"] = null;
                gv_selectedEmp.DataSource = null;
                gv_selectedEmp.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successful...');window.location ='TrainingRecord.aspx';",
                true);

            }
            else
            {
                AlertMessageBoxShow("Operation Failed");
            }

        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        bool result = _recordDal.DeleteTrainingRecord(Convert.ToInt32(hdpk.Value), Convert.ToInt32(Session["UserId"].ToString()));

        if (result == true)
        {
            AlertMessageBoxShow("Operation Successful...");
            //LoadList();
        }
        else
        {

            AlertMessageBoxShow("Operation Failed...");

        }
    }
}