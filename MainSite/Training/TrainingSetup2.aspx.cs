using DAL.COMMON_DAL;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Antlr.Runtime;

public partial class Trainning_TrainingSetup2 : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private int mid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["EmpOption"] = "Employee";
            LoadInitialDDL();
            LoadTrainingOrg();


            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();

                if (mid > 0)
                {
                    DataTable dt = _trainingDal.GetTrainingById(mid);
                    ddlCompany.SelectedValue = dt.Rows[0]["CompanyId"].ToString();
                    ddlCompany_SelectedIndexChanged(ddlCompany, (EventArgs)e);
                    ddlFinancialYear.SelectedValue = dt.Rows[0]["FinancialYearId"].ToString();
                    ddlFinancialYear_SelectedIndexChanged(ddlFinancialYear, (EventArgs) e);




                    ddlQuater.SelectedValue = dt.Rows[0]["Quater"].ToString();

                    ddlQuater_SelectedIndexChanged(ddlQuater, (EventArgs)e);


                    if (Convert.ToInt32(dt.Rows[0]["TrainingRequisitionMasterId"].ToString()) > 0)
                    {
                        fromReq.Checked = true;
                        fromReq_CheckedChanged(fromReq, (EventArgs)e);
                        ddl_training.SelectedValue = dt.Rows[0]["TrainingRequisitionMasterId"].ToString();
                      

                    }
                    else
                    {
                        ddl_training.SelectedValue = dt.Rows[0]["TrainingBudgetAllocationId"].ToString();
                       
                    }

                    
                    txt_TrainigDetails.Text = dt.Rows[0]["TrainingDetails"].ToString();


                    ddlTrainingOrg.SelectedValue = dt.Rows[0]["TrainingOrgId"].ToString();


                    ddlTrainingOrg_SelectedIndexChanged(ddlTrainingOrg, (EventArgs)e);
                    ddlLocation.SelectedValue = dt.Rows[0]["OfficeBranchId"].ToString();

                    txt_StartDate.Text = Convert.ToDateTime(dt.Rows[0]["TrainingStart"].ToString()).ToString("dd-MMM-yyyy");
                    txt_EndDate.Text = Convert.ToDateTime(dt.Rows[0]["TrainingEnd"].ToString()).ToString("dd-MMM-yyyy");
                    txt_Duration.Text = dt.Rows[0]["TrainingDuration"].ToString();

                    bool evaluation = Convert.ToBoolean(dt.Rows[0]["TrainingEvaluation"].ToString());

                    if (evaluation == true)
                    {
                        radTrainingEvaluation.Items[0].Selected = true;

                       
                    }
                    else
                    {
                        radTrainingEvaluation.Items[1].Selected = true;
                    }

                    DataTable dt2 = _trainingDal.GetTrainingDetails(mid);
                    if (dt2 != null)
                    {
                        ViewState["TrainerTable"] = dt2;
                        gvTrainner.DataSource = dt2;
                        gvTrainner.DataBind();
                    }
                   

                }
            }
        }
    }
    protected void detailsViewButton_Click(object sender, EventArgs e)
    {
        //Response.Redirect("TrainingList.aspx");
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
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["CompanyId"] = ddlCompany.SelectedValue;
        DataTable dt = _trainingDal.GetFianncialYearByComIdDDl(Convert.ToInt32(ddlCompany.SelectedValue));
        ddlFinancialYear.DataSource = dt;
        ddlFinancialYear.DataValueField = "Value";
        ddlFinancialYear.DataTextField = "TextField";
        ddlFinancialYear.DataBind();
    }
    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["fid"] = ddlFinancialYear.SelectedValue;
       // LoadQuater(Convert.ToInt32(ddlFinancialYear.SelectedValue));


        DataTable dt = _trainingDal.GetQuaterNew(Convert.ToInt32(ddlCompany.SelectedValue));
        
        ddlQuater.DataSource = dt;
        ddlQuater.DataValueField = "Value";
        ddlQuater.DataTextField = "TextField";
        ddlQuater.DataBind();
    }
    protected void LoadQuater(int id)
    {
        FinancialYear ayear = _trainingDal.GetFinancialYear(id);

        List<YearQuater> quaters = GetAllQuarters(ayear.StartDate, ayear.EndDate);
        YearQuater aQuaterSelect = new YearQuater { QuarterDetails = "Select Quater", QuarterNum = "-1" };
        quaters.Insert(0, aQuaterSelect);
        ddlQuater.DataSource = quaters;
        ddlQuater.DataValueField = "QuarterNum";
        ddlQuater.DataTextField = "QuarterDetails";
        ddlQuater.DataBind();
    }
    public List<YearQuater> GetAllQuarters(DateTime StartDate, DateTime EndDate)
    {

        return _trainingDal.GetQuater(StartDate, EndDate);

    }
    protected void ddlTrainingOrg_SelectedIndexChanged(object sender, EventArgs e)
    {
        //  DataTable org = _trainingDal.GetTrainingOrgDDl();

        ViewState["TrainerTable"] = null;
        gvTrainner.DataSource = null;
        gvTrainner.DataBind();
        DataTable trainenr = _trainingDal.GetTrainnerDDl(Convert.ToInt32(ddlTrainingOrg.SelectedValue));
        DataTable location = _trainingDal.GetOrgBranch(Convert.ToInt32(ddlTrainingOrg.SelectedValue));

        ddlLocation.DataSource = location;
        ddlLocation.DataValueField = "Value";
        ddlLocation.DataTextField = "TextField";
        ddlLocation.DataBind();

        ddlTrainer.DataSource = trainenr;
        ddlTrainer.DataValueField = "Value";
        ddlTrainer.DataTextField = "TextField";
        ddlTrainer.DataBind();



    }
    protected void LoadTrainingOrg()
    {
        DataTable org = _trainingDal.GetTrainingOrgDDl();
        ddlTrainingOrg.DataSource = org;
        ddlTrainingOrg.DataValueField = "Value";
        ddlTrainingOrg.DataTextField = "TextField";
        ddlTrainingOrg.DataBind();
    }
    protected void AddTrainner_Click(object sender, EventArgs e)
    {
        if (ViewState["TrainerTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["TrainerTable"];

            DataRow drCurrentRow = null;
            // drCurrentRow = new DataRow();
            if (dtCurrentTable.Rows.Count > 0 && ValidateTrainnerGrid(Convert.ToInt32(ddlTrainer.SelectedValue))==true)
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
    protected bool ValidateTrainnerGrid(int id)
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
    protected void AddNotListed_Click(object sender, EventArgs e)
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
   
      
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }
    


    
    
    protected void lb_Remove_Click(object sender, EventArgs e)
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
    protected bool Validation()
    {

        bool isValid = true;
        return isValid;

    }
    protected void ddlQuater_SelectedIndexChanged(object sender, EventArgs e)
    {

       


        ddl_training.DataSource = null;
        ddl_training.DataBind();

        if(fromReq.Checked){
            ddl_training.DataSource = _trainingDal.GetTrainingRequistion2(Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlQuater.SelectedValue));
            ddl_training.DataValueField = "Value";
            ddl_training.DataTextField = "TextField";
            ddl_training.DataBind();
        }
        else
        {
            DataTable dt = _trainingDal.GetTrainingBudget2(Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlQuater.SelectedValue));
            ddl_training.DataSource = dt;
            ddl_training.DataValueField = "Value";
            ddl_training.DataTextField = "TextField";
            ddl_training.DataBind();
        }

       


    }
    protected void fromReq_CheckedChanged(object sender, EventArgs e)
    {
        if (fromReq.Checked)
        {


            ddl_training.DataSource = _trainingDal.GetTrainingRequistion2(Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlQuater.SelectedValue));
            ddl_training.DataValueField = "Value";
            ddl_training.DataTextField = "TextField";
            ddl_training.DataBind();
        }
        else
        {
            DataTable dt = _trainingDal.GetTrainingBudget2(Convert.ToInt32(ddlFinancialYear.SelectedValue), Convert.ToInt32(ddlQuater.SelectedValue));
            ddl_training.DataSource = dt;
            ddl_training.DataValueField = "Value";
            ddl_training.DataTextField = "TextField";
            ddl_training.DataBind();
        }
     }
    protected void notListedCheck_CheckedChanged(object sender, EventArgs e)
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

    protected void addEmployeeToList_OnClick(object sender, EventArgs e)
    {
        if (AddEmpValidation() == true)
        {
            AddEmpDetailsToGrid();
        }
        
    }
    protected void AddEmpDetailsToGrid()
    {
        if (ViewState["EmpDetails"] == null)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            string[] empStr = txt_employee.Text.Trim().Split(':');
            string employee = txt_employee.Text.Trim().Substring(0, 6);

            int empId = _trainingDal.GetEmployeeIdByCode(empStr[0]);


            dt.Columns.Add(new DataColumn("quater", typeof(string)));


            dt.Columns.Add(new DataColumn("employee", typeof(string)));
            dt.Columns.Add(new DataColumn("employeeId", typeof(string)));

            dr = dt.NewRow();
            string a = ddlQuater.SelectedValue.ToString();
            dr["quater"] = ddlQuater.SelectedValue.ToString();

            dr["employee"] = txt_employee.Text.Trim();
            dr["employeeId"] = empId.ToString();


            dt.Rows.Add(dr);
            ViewState["EmpDetails"] = dt;
            gv_EmpDetails.DataSource = dt;
            gv_EmpDetails.DataBind();
        }

        else
        {
            if (EmployeeExistValidation(_trainingDal.GetEmployeeIdByCode(txt_employee.Text.Trim().Split(':')[0])) == false)
            {
                aShowMessage.ShowMessageBox("Employee  Already Exists in  List", this);
                txt_employee.Text = "";
                return;
            }

            DataTable dtCurrentTable = (DataTable)ViewState["EmpDetails"];

            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["quater"] = ddlQuater.SelectedValue.ToString();

                drCurrentRow["employee"] = txt_employee.Text.Trim();

                drCurrentRow["employeeId"] = _trainingDal.GetEmployeeIdByCode(txt_employee.Text.Trim().Split(':')[0]); ;




                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["EmpDetails"] = dtCurrentTable;

                gv_EmpDetails.DataSource = dtCurrentTable;
                gv_EmpDetails.DataBind();


            }
        }
        txt_employee.Text = "";
    }

    private bool AddEmpValidation()
    {
        bool isValid = true;
        if (ddlQuater.SelectedValue == "" || ddlQuater.SelectedValue == "-1")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Quater Required", this);
        }

        return isValid;

    }

    private bool EmployeeExistValidation(int empId)
    {
        bool result = true;
        for (int i = 0; i < gv_EmpDetails.Rows.Count; i++)
        {
            HiddenField empInfoId = (HiddenField)gv_EmpDetails.Rows[i].FindControl("deptId");

            int id = Convert.ToInt32(empInfoId.Value.ToString());
            if (id == empId)
            {

                result = false;
                break;
            }


        }
        return result;
    }

    protected void lb_RemoveEmp_OnClick(object sender, EventArgs e)
    {
        if (ViewState["EmpDetails"] != null)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["EmpDetails"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count == 0)
            {
                ViewState["EmpDetails"] = null;
            }
            else
            {
                ViewState["EmpDetails"] = dt;
            }


            gv_EmpDetails.DataSource = dt;
            gv_EmpDetails.DataBind();

        }
    }


    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (SaveValidation() == true)
        {
            TrainingSetup2Master aMaster = new TrainingSetup2Master();

            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);
            if (fromReq.Checked)
            {
                aMaster.FromReq = true;
                aMaster.TrainingReq2DetailsId = Convert.ToInt32(ddl_training.SelectedValue);
                aMaster.TrainingBudget2DetailsId = 0;
            }
            else
            {
                aMaster.FromReq = false;
                aMaster.TrainingBudget2DetailsId = Convert.ToInt32(ddl_training.SelectedValue);
                aMaster.TrainingReq2DetailsId = 0;
            }
            aMaster.TrainingTitle = ddl_training.SelectedItem.Text;
            aMaster.StartDate = Convert.ToDateTime(txt_StartDate.Text);
            aMaster.EndDate = Convert.ToDateTime(txt_EndDate.Text);
            aMaster.Duration = Convert.ToInt32(txt_Duration.Text);
            aMaster.TrainingDetails = txt_TrainigDetails.Text.Trim().ToString();

            aMaster.TrainingOrgId = ddlTrainingOrg.SelectedValue == ""
                ? 0
                : ddlTrainingOrg.SelectedValue == "-1" ? 0 : Convert.ToInt32(ddlTrainingOrg.SelectedValue);
            aMaster.OfficeBranchId = ddlLocation.SelectedValue == ""
              ? 0
              : ddlLocation.SelectedValue == "-1" ? 0 : Convert.ToInt32(ddlLocation.SelectedValue);
            int pk = _trainingDal.SaveTrainingSetup2(aMaster, Session["LoginName"].ToString());

            if (pk > 0)
            {
                if (gv_EmpDetails.Rows.Count > 0)
                {

                    List<TrainingSetup2Details> aList = new List<TrainingSetup2Details>();
                    for (int i = 0; i < gv_EmpDetails.Rows.Count; i++)
                    {
                        HiddenField deptId = (HiddenField)gv_EmpDetails.Rows[i].FindControl("deptId");
                        TrainingSetup2Details aDetails = new TrainingSetup2Details();
                        aDetails.EmpInfoId = Convert.ToInt32(deptId.Value);
                        aDetails.TrainingSetup2MasterId = pk;
                        aList.Add(aDetails);
                    }

                    List<TrainingDetailsInfo> trainers = GetDetailsFromGrid();
                    

                    bool result = _trainingDal.SaveTrainingSetup2Details(aList , pk);

                    if (trainers.Count > 0)
                    {
                        result = _trainingDal.SaveTrainingDetails(trainers, pk);
                    }
                    if (result == true)
                    {
                        AlertMessageBoxShow("Operation Successful...");
                        Response.Redirect("TrainingSetup2.aspx");
                    }
                    else
                    {
                        AlertMessageBoxShow("Operation Failed...");
                    }

                }
               
            }
        }
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
    private bool SaveValidation()
    {
        bool isValid = true;

        if (ddlCompany.SelectedValue == "" || ddlCompany.SelectedValue == "-1")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Company Required", this);
        }
        if (ddlFinancialYear.SelectedValue == "" || ddlFinancialYear.SelectedValue == "-1")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Financial Year Required", this);
        }
        
        if (ddl_training.SelectedValue == "" || ddl_training.SelectedValue == "-1")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Training  Required", this);
        }
        if (txt_StartDate.Text == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Start Date  Required", this);
        }
        if (txt_EndDate.Text == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("End Date  Required", this);
        }
        if (txt_Duration.Text == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Duration  Required", this);
        }
        if (txt_TrainigDetails.Text == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Details  Required", this);
        }

        if (gv_EmpDetails.Rows.Count==0)
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Employee  Required", this);
        }
        return isValid;
    }


    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
       Response.Redirect("TrainingSetup2.aspx");
    }
}