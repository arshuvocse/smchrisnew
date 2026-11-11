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
        {  ButtonVisible();
            DataLoad();
            LoadWeek();
            LoadInitialDDL();
          
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                
                hdpk.Value = mid.ToString();
                DataTable master = _recordDal.GetTrainingRecord(mid);
                entryempinfoIdHiddenField.Value = master.Rows[0]["UserEmpInfoId"].ToString();
                actionstatusHiddenField.Value = master.Rows[0]["ActionStatus"].ToString();
                DataTable emp = _recordDal.GetTrainingRecordEmployee(mid);

                //DataTable dtschedate = _recordDal.GetTrainingRecordScheDate(mid);

                //gv_daylist.DataSource = dtscheday;
                //gv_daylist.DataBind();

                //GridView1.DataSource = dtschedate;
                //GridView1.DataBind();

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

                if (!string.IsNullOrEmpty(master.Rows[0]["TrainingVenue"].ToString()) && master.Rows[0]["TrainingVenue"].ToString()!="0")
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

                for (int i = 0; i < gv_daylist.Rows.Count; i++)
                {
                    DataTable dtscheday = _recordDal.GetTrainingRecordScheDay(mid, ((Label)gv_daylist.Rows[i].FindControl("Day")).Text);
                    if (dtscheday.Rows.Count > 0)
                    {
                        CheckBox chkDay = (CheckBox)gv_daylist.Rows[i].FindControl("chkDay");
                        chkDay.Checked = true;

                        
                    }
                }
                LoadTrainingSchedule();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    DataTable dtschedate = _recordDal.GetTrainingRecordScheDate(mid, ((Label)GridView1.Rows[i].FindControl("Date")).Text);
                    if (dtschedate.Rows.Count > 0)
                    {
                        CheckBox chkDay = (CheckBox)GridView1.Rows[i].FindControl("chkDay");
                        chkDay.Checked = true;
                        (((TextBox)GridView1.Rows[i].FindControl("StartTime")).Text) =
                            dtschedate.Rows[0]["StartTime"].ToString();


                        (((TextBox)GridView1.Rows[i].FindControl("EndTime")).Text) = dtschedate.Rows[0]["EndTime"].ToString();
                    }
                }

            }

            DataTable AppLogComm = _recordDal.GetAppLogCommByJobId(Convert.ToInt32(mid));

            if (AppLogComm.Rows.Count > 0)
            {
                DivShow.Visible = true;
                AppLogCommentGridView.DataSource = AppLogComm;
                AppLogCommentGridView.DataBind();
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
        //        btnSubmit.Visible = true;
        //        orBTN.Visible = true;
        //    }
        //    else if (Session["Status"].ToString() == "Edit")
        //    {
        //        editButton.Visible = true;
        //        btnUpdateforSubmit.Visible = true;
        //        orUp.Visible = true;

        //    }
        //    else if (Session["Status"].ToString() == "Delete")
        //    {
        //        delButton.Visible = true;
        //    }
        //    Session["Status"] = null;
        //}
        //else
        //{
        //    Response.Redirect("JobRequisitionFormView.aspx");
        //}
    }
    public void RadItemRemove()
    {
        //int[] id = new int[5];
        //int count = 0;
        //for (int i = 0; i < jobreqRadioButtonList.Items.Count; i++)
        //{

        //    if (jobreqRadioButtonList.Items[i].Enabled == false)
        //    {
        //        id[count] = Convert.ToInt32(jobreqRadioButtonList.Items[i].Value);
        //        count++;

        //    }
        //}
        //foreach (int a in id)
        //{
        //    for (int i = 0; i < jobreqRadioButtonList.Items.Count; i++)
        //    {

        //        if (jobreqRadioButtonList.Items[i].Value == a.ToString())
        //        {

        //            jobreqRadioButtonList.Items.RemoveAt(i);
        //        }
        //    }
        //}
    }

    public void DataLoad()
    {
        //ClsApprovalAction approvalAction = new ClsApprovalAction();

        //string userName = Session["UserId"].ToString();

        //approvalAction.LoadActionControlByUser(jobreqRadioButtonList, Session["ApprovalPage"].ToString(), userName);
        //Session["ApprovalPage"] = null;
        //RadItemRemove();
        //submitButton.Text = "Submit";

    }
    public void LoadTrainingSchedule()
    {
        DataTable datatable = new DataTable();
        datatable.Columns.Add("ChkDay");
        datatable.Columns.Add("Date");
        datatable.Columns.Add("DayName");
        datatable.Columns.Add("StartTime");
        datatable.Columns.Add("EndTime");

        DataRow dataRow = null;
        DateTime startdate = Convert.ToDateTime(txtStartDate.Text);
        DateTime enddate = Convert.ToDateTime(txtEndDate.Text);
        for (DateTime i = startdate; i <= enddate; i=i.AddDays(1))
        {
            for (int j = 0; j < gv_daylist.Rows.Count;j++ )
            {
                CheckBox chkDay = (CheckBox)gv_daylist.Rows[j].FindControl("chkDay");
                Label Day = (Label)gv_daylist.Rows[j].FindControl("Day");
                if(chkDay.Checked)
                {
                    if(i.ToString("dddd")==Day.Text)
                    {
                        dataRow = datatable.NewRow();
                        dataRow["Date"] = i.ToString("dd-MMM-yyyy");
                        dataRow["DayName"] = i.ToString("dddd");
                        datatable.Rows.Add(dataRow);
                    }
                }
            }
            
        }
        GridView1.DataSource = datatable;
        GridView1.DataBind();
        
    }
    public void LoadWeek()
    {
        DataTable dataTable=new DataTable();
        dataTable.Columns.Add("Day");
        dataTable.Columns.Add("StartTime");
        dataTable.Columns.Add("EndTime");
        DataRow dataRow = null;
        DateTime startdate = Convert.ToDateTime("7-Apr-2019");
        DateTime enddate = Convert.ToDateTime("13-Apr-2019");

        for (DateTime i = startdate; i <= enddate; i=i.AddDays(1))
        {
            dataRow = dataTable.NewRow();
            dataRow["Day"] = i.ToString("dddd");
            dataRow["StartTime"] = "";
            dataRow["EndTime"] = "";
            dataTable.Rows.Add(dataRow);
        }
        gv_daylist.DataSource = dataTable;
        gv_daylist.DataBind();

    }
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }


    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("TrainingRecordsApproval.aspx");
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
        if (txt_NotListedTrainer.Text != "" && txt_NotListedTrainerDetails.Text!="")
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
        else
        {
            aShowMessage.ShowMessageBox("Enter Name and Details", this);
            txt_NotListedTrainer.Focus();
        }
      
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

        if (isSmcVanue.Checked)
        {
            if (string.IsNullOrEmpty(ddlVenue.SelectedValue) || ddlVenue.SelectedValue == "0" || ddlVenue.SelectedValue == "-1")
            {
                isValid = false;
                aShowMessage.ShowMessageBox("Training Venue Required ", this);
            }
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
            aShowMessage.ShowMessageBox("Trainer  Cost Required ", this);
        }
        //if (string.IsNullOrEmpty(txtLogistic.Text.Trim()))
        //{
        //    isValid = false;
        //    aShowMessage.ShowMessageBox("Logistic Cost Required ", this);
        //}
        //if (string.IsNullOrEmpty(txtOtherCost.Text.Trim()))
        //{
        //    isValid = false;
        //    aShowMessage.ShowMessageBox("Other Cost Required ", this);
        //}
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
        //if (string.IsNullOrEmpty(txtStartTime.Text.Trim()))
        //{
        //    isValid = false;
        //    aShowMessage.ShowMessageBox("Training Start Time Required ", this);
        //}
        //if (string.IsNullOrEmpty(txtEndTime.Text.Trim()))
        //{
        //    isValid = false;
        //    aShowMessage.ShowMessageBox("Training End Time Required ", this);
        //}
        
        //if (string.IsNullOrEmpty(txtEndTime.Text.Trim()))
        //{
        //    isValid = false;
        //    aShowMessage.ShowMessageBox("Training End Time Required ", this);
        //}
        //int selectedCount = chkDays.Items.Cast<ListItem>().Count(li => li.Selected);
        //if (selectedCount == 0)
        //{
        //    isValid = false;
        //    aShowMessage.ShowMessageBox("Training Day Required ", this);
        //}
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
          //var mondays = dates.Where(d => d.DayOfWeek == DayOfWeek.Monday);
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
            txtCostPerParticipant.Text = Math.Round((total/Convert.ToDouble(gv_selectedEmp.Rows.Count)),2).ToString();
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

    //protected void btn_Save_OnClick(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        _trainingDal.UpdateStatus(hdpk.Value,
    //                jobreqRadioButtonList.SelectedItem.Text, Session["UserId"].ToString(), DateTime.Now);
    //        //DataLoad();
    //        ScriptManager.RegisterStartupScript(this, this.GetType(),
    //                "alert",
    //                "alert('" + jobreqRadioButtonList.SelectedItem.Text + " Successfully Done');window.location ='TrainingRecordsApproval.aspx';",
    //                true);
    //        //aShowMessage.ShowMessageBox("" + jobreqRadioButtonList.SelectedItem.Text + " Successfully Done", this);
    //    }
    //    catch (Exception ex)
    //    {
    //        aShowMessage.ShowMessageBox("Please Choose an action for approval", this);
    //    }
    //}
    private void ShowMessageBox(string message)
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
          //  aMaster.LogisticCost = Convert.ToDecimal(txtLogistic.Text.Trim());
           // aMaster.OtherCost = Convert.ToDecimal(txtOtherCost.Text.Trim());






            //if (!String.IsNullOrEmpty(txtTrainingCost.Text.Trim()))
            //{
            //    aMaster.TrainingCost = Convert.ToDecimal(txtTrainingCost.Text.Trim());

            //}
            if ((txtLogistic.Text.Trim()) == "")
            {
                aMaster.LogisticCost = Convert.ToDecimal(0);

            }
            else
            {
                aMaster.LogisticCost = Convert.ToDecimal(txtLogistic.Text.Trim());
                
            }


            if ((txtOtherCost.Text.Trim())=="")
            {
                aMaster.OtherCost = Convert.ToDecimal(0);
            }
            else
            {
                aMaster.OtherCost = Convert.ToDecimal(txtOtherCost.Text.Trim());
                
            }












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
            aMaster.StartTime = TimeSpan.Parse("00:00:00");
            aMaster.EndTime = TimeSpan.Parse("00:00:00");
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

                List<TrainingRecordScheduleDayDAO> aTrainingRecordScheduleDayDaoList = new List<TrainingRecordScheduleDayDAO>();
                for (int i = 0; i < gv_daylist.Rows.Count; i++)
                {
                    CheckBox chkDay = (CheckBox)gv_daylist.Rows[i].FindControl("chkDay");
                    if (chkDay.Checked)
                    {


                        TrainingRecordScheduleDayDAO aTrainingRecordScheduleDayDao = new TrainingRecordScheduleDayDAO()
                        {
                            TrainingRecordMasterId = pk,
                            DayName = ((Label)gv_daylist.Rows[i].FindControl("Day")).Text,
                            
                        };

                        aTrainingRecordScheduleDayDaoList.Add(aTrainingRecordScheduleDayDao);
                    }
                }
                result = _recordDal.SaveTrainingRecordScheduleDay(aTrainingRecordScheduleDayDaoList, pk);

                List<TrainingRecordScheDateDAO> aTrainingRecordScheDateDaoList = new List<TrainingRecordScheDateDAO>();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    CheckBox chkDay = (CheckBox)GridView1.Rows[i].FindControl("chkDay");
                    if (chkDay.Checked)
                    {

                        TrainingRecordScheDateDAO aTrainingRecordScheDateDao = new TrainingRecordScheDateDAO()
                        {
                            TrainingRecordMasterId = pk,
                            Day = ((Label)GridView1.Rows[i].FindControl("Day")).Text,
                            Date = Convert.ToDateTime(((Label)GridView1.Rows[i].FindControl("Date")).Text),
                            StartTime =
                                Convert.ToDateTime(((TextBox)GridView1.Rows[i].FindControl("StartTime")).Text)
                                    .TimeOfDay,
                            EndTime =
                                Convert.ToDateTime(((TextBox)GridView1.Rows[i].FindControl("EndTime")).Text).TimeOfDay,

                        };

                        aTrainingRecordScheDateDaoList.Add(aTrainingRecordScheDateDao);
                    }
                }
                result = _recordDal.SaveTrainingRecordScheDate(aTrainingRecordScheDateDaoList, pk);

            }
            if (result == true)
            {

                ViewState["EmpSelect"] = null;
                gv_selectedEmp.DataSource = null;
                gv_selectedEmp.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                 "alert",
                 "alert('Operation Successful...');window.location ='TrainingRecords.aspx';",
                 true);

            }
            else
            {
                //AlertMessageBoxShow("Operation Failed");
            }

        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        bool result = _recordDal.DeleteTrainingRecord(Convert.ToInt32(hdpk.Value), Convert.ToInt32(Session["UserId"].ToString()));

        if (result == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successful...');window.location ='TrainingRecords.aspx';",
                true);
        }
        else
        {

            //AlertMessageBoxShow("Operation Failed...");

        }
    }

    protected void cbCheckAll_OnCheckedChanged(object sender, EventArgs e)
    {

        CheckBox cb = (CheckBox)sender;

        CheckBox chkAddAll = (CheckBox)gv_allocateEmp.HeaderRow.FindControl("cbCheckAll");

        if (cb.Checked)
        {
            chkAddAll.Checked = true;

            for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_allocateEmp.Rows[i].FindControl("txt_check");
                chkSingle.Checked = true;
                
            }
        }
        else
        {
            chkAddAll.Checked = false;

            for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_allocateEmp.Rows[i].FindControl("txt_check");
                chkSingle.Checked = false;

            }
        }
        //foreach (GridViewRow row in gv_allocateEmp.Rows)
        //{
        //    //find the checkbox:
        //    CheckBox cbox = row.FindControl("cbCheckAll") as CheckBox;
        //    if (cbox != null)
        //    {
        //        CheckBox All = row.FindControl("txt_check") as CheckBox;
        //        All.Checked=true;
        //    }
        //}
    }
    protected void chkDay_CheckedChanged(object sender, EventArgs e)
    {
        if (GridValidation1())
        {
            LoadTrainingSchedule();    
        }
    }

    public bool GridValidation2()
    {
        //if (GridView1.Rows.Count > 0)
        //{


        //    for (int i = 0; i < gv_daylist.Rows.Count; i++)
        //    {
        //        CheckBox chkDay = (CheckBox)gv_daylist.Rows[i].FindControl("chkDay");
        //        if (chkDay.Checked)
        //        {
        //            if (((TextBox)gv_daylist.Rows[i].FindControl("StartTime")).Text == string.Empty ||
        //                ((TextBox)gv_daylist.Rows[i].FindControl("EndTime")).Text == string.Empty)
        //            {
        //                aShowMessage.ShowMessageBox("Choose Start Time & End Time for Selected Day ", this);
        //                return false;
        //            }
        //        }
        //    }
        //}
        return true;
    }
    public bool GridValidation1()
    {
        if (txtStartDate.Text ==string.Empty || txtEndDate.Text ==string.Empty)
        {
            aShowMessage.ShowMessageBox("Choose Start Date & End Date",this);
            return false;
        }
        //if (GridView1.Rows.Count > 0)
        //{


        //    for (int i = 0; i < gv_daylist.Rows.Count; i++)
        //    {
        //        CheckBox chkDay = (CheckBox) gv_daylist.Rows[i].FindControl("chkDay");
        //        if (chkDay.Checked)
        //        {
        //            if (((TextBox) gv_daylist.Rows[i].FindControl("StartTime")).Text == string.Empty ||
        //                ((TextBox) gv_daylist.Rows[i].FindControl("EndTime")).Text == string.Empty)
        //            {
        //                aShowMessage.ShowMessageBox("Choose Start Time & End Time for Selected Day ", this);
        //                return false;
        //            }
        //        }
        //    }
        //}
        return true;
    }
    public void CountChekcedPerRow()
    {
        int count = 0;
        decimal traininghour = 0;
        TimeSpan totalts = Convert.ToDateTime("00:00:00").TimeOfDay;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chkDay = (CheckBox)GridView1.Rows[i].FindControl("chkDay");
            if (chkDay.Checked)
            {

                if (!string.IsNullOrEmpty(((TextBox) GridView1.Rows[i].FindControl("StartTime")).Text) &&
                    !string.IsNullOrEmpty(((TextBox) GridView1.Rows[i].FindControl("EndTime")).Text))
                {


                    DateTime dFrom;
                    DateTime dTo;
                    string sDateFrom = ((TextBox) GridView1.Rows[i].FindControl("StartTime")).Text;
                    string sDateTo = ((TextBox) GridView1.Rows[i].FindControl("EndTime")).Text;
                    if (DateTime.TryParse(sDateFrom, out dFrom) && DateTime.TryParse(sDateTo, out dTo))
                    {
                        TimeSpan TS = dTo - dFrom;

                        totalts += TS;

                    }


                }
                count++;
            }
        }
        int hour = totalts.Hours;
        int mins = totalts.Minutes;

        string timeDiff = hour.ToString("00") + "." + mins.ToString("00");
        traininghour += Convert.ToDecimal(timeDiff);
        txtTotalTrainingHoures.Text = Convert.ToDecimal(traininghour).ToString("F");
        txtTotalDays.Text = count.ToString();
    }

    public void CountChekced()
    {
        int count = 0;
        decimal traininghour = 0;
        TimeSpan totalts = Convert.ToDateTime("00:00:00").TimeOfDay;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox chkDay = (CheckBox)GridView1.Rows[i].FindControl("chkDay");
            if (chkDay.Checked)
            {

                for (int j = 0; j < gv_daylist.Rows.Count; j++)
                {
                    if (((Label)GridView1.Rows[i].FindControl("Day")).Text == ((Label)gv_daylist.Rows[j].FindControl("Day")).Text)
                    {
                        DateTime dFrom;
                        DateTime dTo;
                        string sDateFrom = ((TextBox)gv_daylist.Rows[j].FindControl("StartTime")).Text;
                        string sDateTo = ((TextBox)gv_daylist.Rows[j].FindControl("EndTime")).Text;
                        if (DateTime.TryParse(sDateFrom, out dFrom) && DateTime.TryParse(sDateTo, out dTo))
                        {
                            TimeSpan TS = dTo - dFrom;
                            
                            totalts += TS;
                            
                        }
                    }
                }

                
                count++;
            }
        }
        int hour = totalts.Hours;
        int mins = totalts.Minutes;
        
        string timeDiff = hour.ToString("00") + "." + mins.ToString("00");
        traininghour+=Convert.ToDecimal(timeDiff);
        txtTotalTrainingHoures.Text = Convert.ToDecimal(traininghour).ToString("F");
        txtTotalDays.Text = count.ToString();
    }
    protected void chkDay_OnCheckedChanged(object sender, EventArgs e)
    {
        if (GridValidation2())
        {
            CountChekcedPerRow();
        }
    }

    protected void StartTime_OnTextChanged(object sender, EventArgs e)
    {
        CountChekcedPerRow();
    }

    protected void EndTime_OnTextChanged(object sender, EventArgs e)
    {
        CountChekcedPerRow();
    }
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        try
        {

            int mainid
                = Convert.ToInt32(hdpk.Value);
            string ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = _recordDal.UpdateContractural(ActionStatus, mainid);
            if (status)
            {
                int commentid = _recordDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                    txtComment.Text);
                if (ActionStatus == "Verified")
                {
                    DataTable dtempdata = _recordDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    TrainingRecordMasterAppLogDAO appLogDao = new TrainingRecordMasterAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                        appLogDao.TrainingRecordMasterId = mainid;
                        appLogDao.Comments = txtComment.Text;
                        appLogDao.CommentsId = commentid;

                    };
                    int id = _recordDal.SavAppLog(appLogDao);
                    _recordDal.UpdateJobReqStatus2(ActionStatus, mainid);
                }
                else if (ActionStatus == "Approved")
                {
                    int empid = 0;
                    DataTable dtempdata = _recordDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() + "' AND Serial='1'");
                    if (dtempdata.Rows.Count > 0)
                    {
                        empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                    }
                    TrainingRecordMasterAppLogDAO appLogDao = new TrainingRecordMasterAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = empid;
                        appLogDao.TrainingRecordMasterId = mainid;
                        appLogDao.Comments = txtComment.Text;
                        appLogDao.CommentsId = commentid;
                    };
                    ActionStatus = "Verified";
                    _recordDal.UpdateJobReqStatus2(ActionStatus, mainid);
                    int id = _recordDal.SavAppLog(appLogDao);
                }
                else if (ActionStatus == "Review")
                {
                    string actionst = "";
                    DataTable dtdatastatus = _recordDal.GetAppLogStatus(mainid.ToString(),
                        Session["EmpInfoId"].ToString());
                    if (dtdatastatus.Rows.Count>0)
                    {
                        actionst = dtdatastatus.Rows[0]["ActionStatus"].ToString();
                    }
                    DataTable dtempdata = _recordDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), hdpk.Value);
                    DataTable dtempdata2 = _recordDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), hdpk.Value);

                    if (dtempdata2.Rows.Count > 0)
                    {
                        TrainingRecordMasterAppLogDAO appLogDao = new TrainingRecordMasterAppLogDAO();
                        {
                            appLogDao.ActionStatus = "Verified";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString());
                            appLogDao.TrainingRecordMasterId = mainid;
                            appLogDao.Comments = txtComment.Text;
                            appLogDao.CommentsId = commentid;
                        }

                        _recordDal.UpdateAppLog("Review", Session["AppLogId"].ToString());
                        int id = _recordDal.SavAppLog(appLogDao);
                        if (actionst=="Approved")
                        {
                            _recordDal.UpdateContractural("Verified", mainid);
                        }
                        _recordDal.UpdateJobReqStatus2(ActionStatus, mainid);
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
                       "alert('Data Saved Successfully...');window.location ='TrainingRecordsApproval.aspx';",
                       true);
        }
        catch (Exception ex)
        {
            ShowMessageBox("Please Choose an action for approval");
        }
    }

    protected void Button1_OnClick(object sender, EventArgs e)
    {
        //if (Validation())
        {



            int mainid
                = Convert.ToInt32(hdpk.Value);
            string ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = _recordDal.UpdateJobReqStatus2(ActionStatus, mainid);
            if (status)
            {
                int commentid = _recordDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                    txtComment.Text);
                if (ActionStatus == "Verified")
                {
                    DataTable dtempdata =
                        _recordDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                       "' AND EmpInfoId='" + Session["EmpInfoId"].ToString() +
                                                                       "' ");
                    int serial = Convert.ToInt32(dtempdata.Rows[0]["Serial"].ToString()) + 1;
                    DataTable dtempdata2 =
                        _recordDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                       "' AND Serial='" + serial + "' ");
                    //DataTable dtempdata = aEmployeeRequsitionDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    TrainingRecordMasterAppLogDAO appLogDao = new TrainingRecordMasterAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["EmpInfoId"].ToString());
                        appLogDao.TrainingRecordMasterId = mainid;
                        appLogDao.Comments = txtComment.Text;
                        appLogDao.CommentsId = commentid;

                    };
                    int id = _recordDal.SavAppLog(appLogDao);
                }
                else if (ActionStatus == "Approved")
                {
                    int empid = 0;
                    //DataTable dtempdata = aEmployeeRequsitionDal.GetHRAdminEmployeeAppId(" WHERE URL='"+Session["AppPage"].ToString()+"' AND Serial='1'" );
                    //if (dtempdata.Rows.Count>0)
                    //{
                    //    empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                    //}
                    TrainingRecordMasterAppLogDAO appLogDao = new TrainingRecordMasterAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = empid;
                        appLogDao.TrainingRecordMasterId = mainid;
                        appLogDao.Comments = txtComment.Text;
                        appLogDao.CommentsId = commentid;
                    };


                    int id = _recordDal.SavAppLog(appLogDao);
                }
                else if (ActionStatus == "Review")
                {
                    string actionst = "";
                    DataTable dtdatastatus = _recordDal.GetAppLogStatus(mainid.ToString(),
                        Session["EmpInfoId"].ToString());
                    if (dtdatastatus.Rows.Count > 0)
                    {
                        actionst = dtdatastatus.Rows[0]["ActionStatus"].ToString();
                    }
                    DataTable dtempdata = _recordDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), hdpk.Value);
                    DataTable dtempdata2 = _recordDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), hdpk.Value);

                    if (dtempdata2.Rows.Count > 0)
                    {
                        TrainingRecordMasterAppLogDAO appLogDao = new TrainingRecordMasterAppLogDAO();
                        {
                            appLogDao.ActionStatus = "Verified";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString());
                            appLogDao.TrainingRecordMasterId = mainid;
                            appLogDao.Comments = txtComment.Text;
                            appLogDao.CommentsId = commentid;
                        }

                        _recordDal.UpdateAppLog("Review", Session["AppLogId"].ToString());
                        //int id = _recordDal.SavAppLog(appLogDao);
                        if (actionst == "Approved")
                        {
                            _recordDal.UpdateContractural("Verified", mainid);
                        }
                        int id = _recordDal.SavAppLog(appLogDao);
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
                       "alert('Data Saved Successfully...');window.location ='TrainingRecordsApproval.aspx';",
                       true);
        }
    }

    protected void Button2a_OnClick(object sender, EventArgs e)
    {

        int mainid
            = Convert.ToInt32(hdpk.Value);
        string ActionStatus = "Rejected";
        bool status = _recordDal.UpdateContractural(ActionStatus, mainid);
        int commentid = _recordDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                txtComment.Text);
        if (ActionStatus == "Rejected")
        {
            //DataTable dtempdata = aEmployeeRequsitionDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
            TrainingRecordMasterAppLogDAO appLogDao = new TrainingRecordMasterAppLogDAO();
            {
                appLogDao.ActionStatus = "Rejected";
                appLogDao.ApproveDate = DateTime.Now;
                appLogDao.ApproveBy = Session["UserId"].ToString();
                appLogDao.PreEmpInfoId = 0;
                appLogDao.ForEmpInfoId = 0;
                appLogDao.TrainingRecordMasterId = mainid;
                appLogDao.Comments = txtComment.Text;
                appLogDao.CommentsId = commentid;

            };
            int id = _recordDal.SavAppLog(appLogDao);
            _recordDal.UpdateJobReqStatus2(ActionStatus, mainid);
        }
        Session["AppLogId"] = null;
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Rejected Successfully...');window.location ='TrainingRecordsApproval.aspx';",
                   true);
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
            dtdata = _recordDal.GetHRAdminEmployeeAppId(" WHERE URL='" + filepath + "' AND Serial IN (SELECT MAX(Serial)AS SL FROM dbo.tblEmployeeApprovalByOpearationDetail" +
                                                                    " LEFT JOIN dbo.tblMainMenu ON dbo.tblEmployeeApprovalByOpearationDetail.Operation=dbo.tblMainMenu.MainMenuId WHERE URL='" + filepath + "'  ) AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "' ORDER BY Serial ASC ");
        }
        else
        {
            dtdata = _recordDal.GetSupervisorEmployeeAppId(Session["EmpInfoId"].ToString(), entryempinfoIdHiddenField.Value);
        }

        //DataTable dtdata = aEmployeeRequsitionDal.GetSupervisorAppId(filepath, " AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");

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
            btn_Save.Visible = false;
            Button1.Visible = true;
        }
        else
        {
            btn_Save.Visible = true;
            Button1.Visible = false;
        }
    }


}