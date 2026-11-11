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

public partial class Training_TrainingBudgetList : System.Web.UI.Page
{
    private TrainingDAL _trainingDal = new TrainingDAL();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadList();
        }
    }
    protected void btnAddNewTrainingBudget_Click(object sender, EventArgs e)
    {
        Response.Redirect("TrainingBudget.aspx");
    }

    protected void LoadList()
    {
        DataTable dt = _trainingDal.GetTrainingBudgetList();
        gv_trainingBgtList.DataSource = dt;
        gv_trainingBgtList.DataBind();
    }
    protected void lb_Edit_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField hdpk = (HiddenField)gv_trainingBgtList.Rows[rowID].FindControl("hdpk");

        Response.Redirect("TrainingBudget.aspx?mid=" + hdpk.Value);
    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }
    protected void lb_delete_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField hdpk = (HiddenField)gv_trainingBgtList.Rows[rowID].FindControl("hdpk");

        bool result = _trainingDal.DeleteBudget(Convert.ToInt32(hdpk.Value), Session["LoginName"].ToString());

        if (result == true)
        {
            AlertMessageBoxShow("Operation Successful...");
             LoadList();
        }
        else
        {

            AlertMessageBoxShow("Operation Failed...");
            LoadList();

        }
    }
}