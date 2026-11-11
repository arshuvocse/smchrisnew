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

public partial class Training_TrainingEvaluation : System.Web.UI.Page
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
            LoadInitialDDL();

            DataTable dtD = _trainingDal.GetEvaluationFormDetails(mid);

            if (dtD.Rows.Count>0)
            {
                ViewState["Evaluation"] = dtD;
                gv_Topic.DataSource = dtD;
                gv_Topic.DataBind();
                if (dtD.Rows.Count > 0)
                {
                    hdpk.Value = dtD.Rows[0]["EvaluationFormMasterId"].ToString();
                }
               
            }
            else
            {
                ViewState["Evaluation"] = null;
            }

           
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();

                if (mid > 0)
                {
                    DataTable dt = _trainingDal.GetEvaluationFromById(mid);
                    
                    
                }
            }
        }
    }

    private void LoadInitialDDL()
    {
        _trainingDal.TriningHeadingDropDown(ddlTrainingheading);
    }
    protected void addTopic_Click(object sender, EventArgs e)
    {

        if (Vali())
        {
            if (ViewState["Evaluation"] == null)
            {

                DataTable dt = new DataTable();
                DataRow dr = null;

                dt.Columns.Add(new DataColumn("EvaluationFormDetailsId", typeof(string)));
                dt.Columns.Add(new DataColumn("TrainingTopicId", typeof(string)));
                dt.Columns.Add(new DataColumn("TraingingHeadingId", typeof(string)));
                dt.Columns.Add(new DataColumn("heading", typeof(string)));
                dt.Columns.Add(new DataColumn("topic", typeof(string)));
                dt.Columns.Add(new DataColumn("failed", typeof(string)));
                dt.Columns.Add(new DataColumn("avarage", typeof(string)));
                dt.Columns.Add(new DataColumn("abvavarage", typeof(string)));
                dt.Columns.Add(new DataColumn("excellent", typeof(string)));
                dt.Columns.Add(new DataColumn("IsActive", typeof(string)));

                dr = dt.NewRow();
                dr["EvaluationFormDetailsId"] = detailIdHiddenField.Value;
                dr["TrainingTopicId"] = ddlTrainingtopic.SelectedValue.Trim();
                dr["TraingingHeadingId"] = ddlTrainingheading.SelectedValue.Trim();
                dr["topic"] = ddlTrainingtopic.SelectedItem.Text.Trim();
                dr["heading"] = ddlTrainingheading.SelectedItem.Text.Trim();
                dr["failed"] = txt_failed.Text.Trim();
                dr["avarage"] = txt_average.Text.Trim();
                dr["IsActive"] = isActiveCheckBox.Checked;
                dr["abvavarage"] = txt_abv_avarage.Text.Trim();
                dr["excellent"] = txt_excellent.Text.Trim();

                dt.Rows.Add(dr);

                ViewState["Evaluation"] = dt;
                gv_Topic.DataSource = dt;
                gv_Topic.DataBind();
            }
            else
            {
                DataTable dtCurrentTable = (DataTable)ViewState["Evaluation"];

                DataRow drCurrentRow = null;
                //if (dtCurrentTable.Rows.Count > 0)
                {


                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["EvaluationFormDetailsId"] = detailIdHiddenField.Value;
                    drCurrentRow["TrainingTopicId"] = ddlTrainingtopic.SelectedValue.Trim();
                    drCurrentRow["TraingingHeadingId"] = ddlTrainingheading.SelectedValue.Trim();
                    drCurrentRow["heading"] = ddlTrainingheading.SelectedItem.Text.Trim();
                    drCurrentRow["topic"] = ddlTrainingtopic.SelectedItem.Text.Trim();
                    drCurrentRow["failed"] = txt_failed.Text.Trim();
                    drCurrentRow["avarage"] = txt_average.Text.Trim();
                    drCurrentRow["abvavarage"] = txt_abv_avarage.Text.Trim();
                    drCurrentRow["excellent"] = txt_excellent.Text.Trim();
                    drCurrentRow["IsActive"] = isActiveCheckBox.Checked;
                    dtCurrentTable.Rows.Add(drCurrentRow);


                    ViewState["Evaluation"] = dtCurrentTable;

                    gv_Topic.DataSource = dtCurrentTable;
                    gv_Topic.DataBind();

                }
            }


            txt_failed.Text = "";
            txt_average.Text = "";
            txt_abv_avarage.Text = "";
            txt_excellent.Text = "";
            isActiveCheckBox.Checked = false;
            
        }
      
        
    }

    private bool Vali()
    {



        if (ddlTrainingheading.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            ddlTrainingheading.Focus();
            return false;


        }


        if (ddlTrainingtopic.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            ddlTrainingtopic.Focus();
            return false;
        }

        return true;
    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {

        if (Validationnn())
        {
            List<EvaluationFormDetails> aList = new List<EvaluationFormDetails>();

            if (gv_Topic.Rows.Count > 0)
            {

                for (int i = 0; i < gv_Topic.Rows.Count; i++)
                {

                    Label topic = (Label)gv_Topic.Rows[i].FindControl("gv_topic");
                    Label failed = (Label)gv_Topic.Rows[i].FindControl("gv_failed");
                    Label avarage = (Label)gv_Topic.Rows[i].FindControl("gv_avarage");
                    Label abvavarage = (Label)gv_Topic.Rows[i].FindControl("gv_above_avarage");
                    Label excellent = (Label)gv_Topic.Rows[i].FindControl("gv_excellent");
                    Label IsActive = (Label)gv_Topic.Rows[i].FindControl("gv_active");

                    EvaluationFormDetails aDetails = new EvaluationFormDetails();
                    try
                    {
                        aDetails.EvaluationFormDetailsId = Convert.ToInt32(gv_Topic.DataKeys[i]["EvaluationFormDetailsId"].ToString());
                    }
                    catch (Exception)
                    {
                        aDetails.EvaluationFormDetailsId = 0;

                    }

                    aDetails.TrainingTopicId = Convert.ToInt32(gv_Topic.DataKeys[i][0].ToString());
                    aDetails.TraingingHeadingId = Convert.ToInt32(gv_Topic.DataKeys[i][1].ToString());
                    aDetails.TopicText = topic.Text.ToString();
                    aDetails.FailedText = failed.Text.ToString();
                    aDetails.AverageText = avarage.Text.ToString();
                    aDetails.AboveAverageText = abvavarage.Text.ToString();
                    aDetails.ExcellentText = excellent.Text.ToString();
                    aDetails.IsActive = Convert.ToBoolean(IsActive.Text.Trim());
                    aList.Add(aDetails);
                }
                EvaluationFormMaster aMaster = new EvaluationFormMaster();




                aMaster.EvaluationFormMasterId = hdpk.Value == "" ? 0 : Convert.ToInt32(hdpk.Value);

                int result = _trainingDal.SaveEvaluationForm(aMaster, Session["UserId"].ToString());

                bool detailsResult = false;


                detailsResult = _trainingDal.SaveEvaluationFormDetails(aList, result);
                if (detailsResult == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "alert",
                          "alert('Data Saved Successfully...');window.location ='TrainingEvaluation.aspx';",
                          true);

                }

            }
            else
            {
                aShowMessage.ShowMessageBox("Please Add Topic ", this);
            }
        }

    }

    private bool Validationnn()
    {
        //if (ddlTrainingheading.SelectedValue == "")
        //{
        //    aShowMessage.ShowMessageBox(aMessages.VArea, this);
        //    ddlTrainingheading.Focus();
        //    return false;


        //}


        //if (ddlTrainingtopic.SelectedValue == "")
        //{
        //    aShowMessage.ShowMessageBox(aMessages.VArea, this);
        //    ddlTrainingtopic.Focus();
        //    return false;
        //}

        if (gv_Topic.Rows.Count==0)
        {
            aShowMessage.ShowMessageBox("Please Add Topic ", this);
          
            return false;
        }

        return true;

    }

    protected void detailsViewButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("EvaluationFormList.aspx");
    }
    protected void lb_remave_Click(object sender, EventArgs e)
    {
        if (ViewState["Evaluation"] != null)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["Evaluation"];

            dt.Rows.Remove(dt.Rows[rowID]);

            gv_Topic.DataSource = dt;
            gv_Topic.DataBind();
        }
    }
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("TrainingEvaluation.aspx");
    }

    protected void ddlTrainingheading_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        _trainingDal.TriningTopicDropDown(ddlTrainingtopic,ddlTrainingheading.SelectedValue);
    }

    protected void LinkButton1_OnClick(object sender, EventArgs e)
    {
        if (ViewState["Evaluation"] != null)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["Evaluation"];
            ddlTrainingheading.SelectedValue = dt.Rows[rowID]["TraingingHeadingId"].ToString();
            _trainingDal.TriningTopicDropDown(ddlTrainingtopic, ddlTrainingheading.SelectedValue);
            ddlTrainingtopic.SelectedValue = dt.Rows[rowID]["TrainingTopicId"].ToString();
            detailIdHiddenField.Value = dt.Rows[rowID]["EvaluationFormDetailsId"].ToString();
            txt_failed.Text = dt.Rows[rowID]["failed"].ToString();
            txt_average.Text = dt.Rows[rowID]["avarage"].ToString();
            txt_abv_avarage.Text = dt.Rows[rowID]["abvavarage"].ToString();
            txt_excellent.Text = dt.Rows[rowID]["excellent"].ToString();
            isActiveCheckBox.Checked = Convert.ToBoolean(dt.Rows[rowID]["IsActive"].ToString());
            dt.Rows.Remove(dt.Rows[rowID]);

            gv_Topic.DataSource = dt;
            gv_Topic.DataBind();

            

        }
    }
}