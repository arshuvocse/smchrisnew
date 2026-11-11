using DAL.COMMON_DAL;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Trainning_TrainingSetup : System.Web.UI.Page
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
                        ddl_training_SelectedIndexChanged(ddl_training, (EventArgs)e);

                    }
                    else
                    {
                        ddl_training.SelectedValue = dt.Rows[0]["TrainingBudgetAllocationId"].ToString();
                        ddl_training_SelectedIndexChanged(ddl_training, (EventArgs) e);
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
        Response.Redirect("TrainingList.aspx");
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
        Session["cid"] = ddlCompany.SelectedValue;
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


                aShowMessage.ShowMessageBox(aMessages.TTrainingExist, this);
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
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        bool result = false;
        mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
        if (mid > 0)
        {


            if (Validation() == true)
            {
                result = UpdateEntry(mid);
                if (result == true)
                {

                    AlertMessageBoxShow("Operation Successful...");
                    Response.Redirect("TrainingList.aspx");


                }
                else
                {
                    AlertMessageBoxShow("Operation Failed...");
                }
            }
        }
        else
        {

            if (Validation() == true)
            {
                result = SaveNewEntry();
                if (result == true)
                {

                    AlertMessageBoxShow("Operation Successful...");
                    Response.Redirect("TrainingList.aspx");


                }
                else
                {
                    AlertMessageBoxShow("Operation Failed...");
                }
            }

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
    public bool UpdateEntry(int id)
    {
        bool result = false;
        List<string> selectedValues = ddlQuater.Items.Cast<ListItem>()
  .Where(li => li.Selected)
  .Select(li => li.Value)
  .ToList();
        TrainingMasterInfo aMaster = new TrainingMasterInfo();

       

        aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        aMaster.TrainingMasterId = id;
        aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);
        aMaster.TrainingTitle = ddl_training.SelectedItem.Text.ToString().Substring(14);
        if (fromReq.Checked)
        {
            aMaster.TrainingRequisitionMasterId = Convert.ToInt32(ddl_training.SelectedValue);
            aMaster.TrainingBudgetAllocationId = 0;
        }
        else
        {
            aMaster.TrainingRequisitionMasterId = 0;
            aMaster.TrainingBudgetAllocationId = Convert.ToInt32(ddl_training.SelectedValue);
        }
        aMaster.TrainingDetails = txt_TrainigDetails.Text.Trim();
        
        aMaster.Quater = Convert.ToInt32(ddlQuater.SelectedValue);

        aMaster.TrainingOrgId = Convert.ToInt32(ddlTrainingOrg.SelectedValue);
        aMaster.TrainingStart = Convert.ToDateTime(txt_StartDate.Text.Trim());
        aMaster.TrainingEnd = Convert.ToDateTime(txt_EndDate.Text.Trim());
        aMaster.TrainingDuration = Convert.ToInt32(txt_Duration.Text.Trim());
        aMaster.OfficeBranchId = Convert.ToInt32(ddlLocation.SelectedValue);
        string evaluation = radTrainingEvaluation.SelectedValue.ToString();

        aMaster.TrainingEvaluation = evaluation == "Yes" ? true : false;

        result = _trainingDal.UpdatTrainingMaster(aMaster, Session["LoginName"].ToString());

        if (result == true)
        {

            List<TrainingDetailsInfo> trainers = GetDetailsFromGrid();
            if (trainers.Count > 0)
            {
                result = _trainingDal.UpdateTrainingDetails(trainers, id);
            }
            else
            {
                result = true;
            }
            result = _trainingDal.UpdateAllocationDetails(GetParticipants());
           
        }
        result = _trainingDal.UpdateAllocationDetails(GetParticipants(), fromReq.Checked);
        return result;

    }
    protected bool SaveNewEntry()
    {
        bool result = false;
        string selectedValues = ddlQuater.SelectedValue;
        TrainingMasterInfo aMaster = new TrainingMasterInfo();

        aMaster.Quater = Convert.ToInt32(ddlQuater.SelectedValue);
       

        aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);
        aMaster.TrainingTitle = ddl_training.SelectedItem.Text.ToString().Substring(14);
        if (fromReq.Checked)
        {
            aMaster.TrainingRequisitionMasterId = Convert.ToInt32(ddl_training.SelectedValue);
            aMaster.TrainingBudgetAllocationId = 0;
        }
        else
        {
             aMaster.TrainingRequisitionMasterId = 0;
             aMaster.TrainingBudgetAllocationId = Convert.ToInt32(ddl_training.SelectedValue);
        }

       
        aMaster.TrainingDetails = txt_TrainigDetails.Text.Trim();
     

        aMaster.TrainingOrgId = ddlTrainingOrg.SelectedValue==""?0: Convert.ToInt32(ddlTrainingOrg.SelectedValue);
        aMaster.TrainingStart = Convert.ToDateTime(txt_StartDate.Text.Trim());
        aMaster.TrainingEnd = Convert.ToDateTime(txt_EndDate.Text.Trim());
        aMaster.TrainingDuration = Convert.ToInt32(txt_Duration.Text.Trim());
        aMaster.OfficeBranchId =ddlLocation.SelectedValue==""?0: Convert.ToInt32(ddlLocation.SelectedValue);
        string evaluation = radTrainingEvaluation.SelectedValue.ToString();

        aMaster.TrainingEvaluation = evaluation == "Yes" ? true : false;

       
        int pk = _trainingDal.SaveTrainingSetupMaster(aMaster, Session["LoginName"].ToString());

        List<TrainingDetailsInfo> trainers = GetDetailsFromGrid();
        if (pk > 0 && trainers.Count > 0)
        {
           
            result = _trainingDal.SaveTrainingDetails(trainers, pk);

           
        }
        else
        {
            if (pk > 0)
            {
                result = true;
            }
        }

        
        result = _trainingDal.UpdateAllocationDetails(GetParticipants(),fromReq.Checked);
        return result;
    }

    private List<TrainingBudgetAllocationDetails> GetParticipants()
    {

        List<TrainingBudgetAllocationDetails> aList = new List<TrainingBudgetAllocationDetails>();
        for (int i = 0; i < gv_allocateEmp.Rows.Count; i++)
        {
            TrainingBudgetAllocationDetails aDetails = new TrainingBudgetAllocationDetails();
            CheckBox txt_Quater = (CheckBox)gv_allocateEmp.Rows[i].FindControl("txt_check");
            HiddenField txt_DetailsId = (HiddenField)gv_allocateEmp.Rows[i].FindControl("detailsID");
            HiddenField txt_emp = (HiddenField)gv_allocateEmp.Rows[i].FindControl("empInfoId");

            aDetails.TrainingBudgetAllocationId = Convert.ToInt32(ddl_training.SelectedValue);
            aDetails.EmpInfoId = Convert.ToInt32(txt_emp.Value);
            aDetails.TrainingBudgetAllocationDetailsId = Convert.ToInt32(txt_DetailsId.Value);
            if (txt_Quater.Checked)
            {
                aDetails.IsActive = true;
              
            }
            else
            {
                aDetails.IsActive = false;
            }
            aList.Add(aDetails);
        }
        return aList;
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
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("TrainingSetup.aspx");
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
        if (ddlCompany.SelectedValue == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox(aMessages.VCompany, this);
            return false;
        }

        if (ddlFinancialYear.SelectedValue == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox(aMessages.TTFinancialYear, this);
            return false;
        }

        if (ddlQuater.SelectedValue=="")
        {
            isValid = false;

            aShowMessage.ShowMessageBox(aMessages.TTQuater, this);
            return false;
        }
        int totalCount = gv_allocateEmp.Rows.Cast<GridViewRow>()
.Count(r => ((CheckBox)r.FindControl("txt_check")).Checked);
        if (totalCount <= 0)
        {
            aShowMessage.ShowMessageBox("Please Check Participants !!!", this);
            return false;
        }
        //if (txt_TrainingTitle.Text == "")
        //{
        //    isValid = false;
        //    aShowMessage.ShowMessageBox(aMessages.TTtriningTitle, this);
        //    return false;
        //}

        //if (ddlTrainingOrg.SelectedValue == "")
        //{
        //    isValid = false;
        //    aShowMessage.ShowMessageBox(aMessages.VOrgName, this);
        //    return false;
        //}

        //if (gvTrainner.Rows.Count == 0)
        //{
        //    isValid = false;
        //    aShowMessage.ShowMessageBox(aMessages.VOrgTrainer, this);
        //    return false;
        //}
        if (txt_StartDate.Text == "")
        {

            isValid = false;
            aShowMessage.ShowMessageBox(aMessages.TTrainingStart, this);
            return false;
        }
        if (txt_StartDate.Text == "")
        {

            isValid = false;
            aShowMessage.ShowMessageBox(aMessages.TTrainingEnd, this);
            return false;
        }

        return isValid;

    }
    protected void ddlQuater_SelectedIndexChanged(object sender, EventArgs e)
    {

       


        ddl_training.DataSource = null;
        ddl_training.DataBind();

        if(fromReq.Checked){
            ddl_training.DataSource = _trainingDal.GetTrainingRequistion(Convert.ToInt32(ddlFinancialYear.SelectedValue), ddlQuater.SelectedValue);
            ddl_training.DataValueField = "Value";
            ddl_training.DataTextField = "TextField";
            ddl_training.DataBind();
        }
        else
        {
            DataTable dt = _trainingDal.GetTrainingBudget(Convert.ToInt32(ddlFinancialYear.SelectedValue), ddlQuater.SelectedValue);
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
           

            ddl_training.DataSource = _trainingDal.GetTrainingRequistion(Convert.ToInt32(ddlFinancialYear.SelectedValue), ddlQuater.SelectedValue);
            ddl_training.DataValueField = "Value";
            ddl_training.DataTextField = "TextField";
            ddl_training.DataBind();
        }
        else
        {
            DataTable dt = _trainingDal.GetTrainingBudget(Convert.ToInt32(ddlFinancialYear.SelectedValue), ddlQuater.SelectedValue);
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
    protected void ddl_training_SelectedIndexChanged(object sender, EventArgs e)
    {
        string theAlloc = ddl_training.SelectedValue;

        if (fromReq.Checked)
        {

            DataTable dt = _trainingDal.GetEmployeeByRequisitionMasterMaster(Convert.ToInt32(theAlloc));
            gv_allocateEmp.DataSource = dt;
            gv_allocateEmp.DataBind();
        }
        else
        {
            DataTable dt = _trainingDal.GetEmployeeByAlocationMaster(Convert.ToInt32(theAlloc));
            gv_allocateEmp.DataSource = dt;
            gv_allocateEmp.DataBind();
        }

        //DataTable dt = _trainingDal.GetEmployeeByAlocationMaster(Convert.ToInt32(theAlloc));
        //gv_allocateEmp.DataSource = dt;
        //gv_allocateEmp.DataBind();


    }
}