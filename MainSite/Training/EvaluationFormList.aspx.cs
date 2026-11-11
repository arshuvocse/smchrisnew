using DAL.COMMON_DAL;
using DAL.TrainingDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Training_EvaluationFormList : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadGrid();
        }

        try
        {

            gv_formList.UseAccessibleHeader = true;
            gv_formList.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv_formList.FooterRow.TableSection = TableRowSection.TableFooter;
            gv_formList.UseAccessibleHeader = true;

        }
        catch (Exception ex)
        {


        }
    }



    protected void LoadGrid()
    {

        try
        {
            DataTable dt = _trainingDal.GetEvaluationForm();
            gv_formList.DataSource = dt;
            gv_formList.DataBind();
            gv_formList.UseAccessibleHeader = true;
            gv_formList.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv_formList.FooterRow.TableSection = TableRowSection.TableFooter;
            gv_formList.UseAccessibleHeader = true;
        }
        catch (Exception)
        {
            
            //throw;
        }
    }
    protected void detailsViewButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("TrainingEvaluation.aspx");
    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }
    
    protected void btn_edit_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField hdpk = (HiddenField)gv_formList.Rows[rowID].FindControl("hdpkd");

        Response.Redirect("TrainingEvaluation.aspx?mid=" + hdpk.Value);
    }
    protected void btn_remove_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField hdpk = (HiddenField)gv_formList.Rows[rowID].FindControl("hdpkd");


        bool result = _trainingDal.DeleteTrainingEvaluationForm(Convert.ToInt32(hdpk.Value), Session["LoginName"].ToString());

        if (result == true)
        {
            AlertMessageBoxShow("Operation Successful...");
            LoadGrid();
        }
        else
        {

            AlertMessageBoxShow("Operation Failed...");

        }
    }

    protected void btn_evaluation_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField hdpk = (HiddenField)gv_formList.Rows[rowID].FindControl("hdpkd");

        Response.Redirect("EvaluateTraining.aspx?mid=" + hdpk.Value);
    }
}