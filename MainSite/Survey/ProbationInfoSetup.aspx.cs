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
using DAL.Survey;

public partial class Training_TrainingEvaluation : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private ProbationperiodDAL aProbationperiodDal= new ProbationperiodDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private int mid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInitialDDL();

            //DataTable dtD = _trainingDal.GetEvaluationFormDetails(mid);
            //ViewState["Evaluation"] = dtD;
            //gv_Topic.DataSource = dtD;
            //gv_Topic.DataBind();
            //if (dtD.Rows.Count>0)
            //{
            //    hdpk.Value = dtD.Rows[0]["EvaluationFormMasterId"].ToString();    
            //}
            
            //if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            //{
            //    mid = int.Parse(Request.QueryString["mid"]);
            //    hdpk.Value = mid.ToString();

            //    if (mid > 0)
            //    {
            //        DataTable dt = _trainingDal.GetEvaluationFromById(mid);
                    
                    
            //    }
            //}
        }
    }

    private void LoadInitialDDL()
    {
        aProbationperiodDal.LoadCompanyDropDownList(ddlCompany);
    }
    protected void addTopic_Click(object sender, EventArgs e)
    {


        if (ViewState["Grid"] == null)
        {

            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("tblProbationEvaluationRatingId", typeof(string)));
            dt.Columns.Add(new DataColumn("ValueField", typeof(string)));
            dt.Columns.Add(new DataColumn("TextField", typeof(string)));
            dt.Columns.Add(new DataColumn("IsActive", typeof(bool)));
            

            dr = dt.NewRow();
            dr["tblProbationEvaluationRatingId"] = evuId.Value;
            dr["ValueField"] = "0";
            dr["TextField"] = purposeTextBox.Text.Trim();
            dr["IsActive"] = isActiveCheckBox.Checked;
            //if (isActiveCheckBox.Checked)
            //{
            //    dr["IsActive"] = "1";
            //}
            //else
            //{
            //    dr["IsActive"] = "0";
            //}
            

            dt.Rows.Add(dr);

            ViewState["Grid"] = dt;
            gv_Topic.DataSource = dt;
            gv_Topic.DataBind();
        }
        else
        {
            DataTable dtCurrentTable = (DataTable)ViewState["Grid"];

            DataRow drCurrentRow = null;
            //if (dtCurrentTable.Rows.Count > 0)
            {

               
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["tblProbationEvaluationRatingId"] = evuId.Value;
                    if (isActiveCheckBox.Checked)
                    {
                        drCurrentRow["IsActive"] = isActiveCheckBox.Checked;
                    }
                    else
                    {
                        drCurrentRow["IsActive"] = isActiveCheckBox.Checked;
                    }
                    drCurrentRow["TextField"] = purposeTextBox.Text.Trim();
                    dtCurrentTable.Rows.Add(drCurrentRow);
                

                ViewState["Grid"] = dtCurrentTable;

                gv_Topic.DataSource = dtCurrentTable;
                gv_Topic.DataBind();
              
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
    protected void btn_Save_Click(object sender, EventArgs e)
    {

        List<EvaluationFormDetails> aList = new List<EvaluationFormDetails>();

        if (gv_Topic.Rows.Count > 0)
        {

            for (int i = 0; i < gv_Topic.Rows.Count; i++)
            {
                ProbationEvaluationRating aProbationEvaluationRating = new ProbationEvaluationRating();
                Label purpose = (Label) gv_Topic.Rows[i].FindControl("gv_purpose");

                CheckBox IsActive = (CheckBox) gv_Topic.Rows[i].FindControl("gv_active");
                aProbationEvaluationRating.TextField = purpose.Text;
                aProbationEvaluationRating.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                aProbationEvaluationRating.IsActive = IsActive.Checked;
                aProbationEvaluationRating.ValueField = "";
                aProbationEvaluationRating.tblProbationEvaluationRatingId =
                    string.IsNullOrEmpty(gv_Topic.DataKeys[i][0].ToString())
                        ? 0
                        : Convert.ToInt32(gv_Topic.DataKeys[i][0].ToString());
                aProbationperiodDal.SaveProbatioRating(aProbationEvaluationRating);

            }
            aShowMessage.ShowMessageBox("Data Saved Successfully ",this);


        }

    }
    protected void detailsViewButton_Click(object sender, EventArgs e)
    {
        //Response.Redirect("EvaluationFormList.aspx");
    }
    protected void lb_remave_Click(object sender, EventArgs e)
    {
        if (ViewState["Grid"] != null)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["Grid"];

            dt.Rows.Remove(dt.Rows[rowID]);

            gv_Topic.DataSource = dt;
            gv_Topic.DataBind();
        }
    }
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProbationInfoSetup.aspx");
    }

    
    protected void LinkButton1_OnClick(object sender, EventArgs e)
    {
        if (ViewState["Grid"] != null)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["Grid"];
            evuId.Value = dt.Rows[rowID]["tblProbationEvaluationRatingId"].ToString();
            purposeTextBox.Text = dt.Rows[rowID]["TextField"].ToString();
            isActiveCheckBox.Checked = Convert.ToBoolean(dt.Rows[rowID]["IsActive"].ToString());
            dt.Rows.Remove(dt.Rows[rowID]);

            gv_Topic.DataSource = dt;
            gv_Topic.DataBind();

            

        }
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtdata = aProbationperiodDal.GetProbationRating(ddlCompany.SelectedValue);
        if (dtdata.Rows.Count>0)
        {
            gv_Topic.DataSource = dtdata;
            gv_Topic.DataBind();
        }
    }
}