using DAL.TrainingDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Training_TrainingList : System.Web.UI.Page
{
    private TrainingDAL _trainingDal = new TrainingDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadList();
        }
       
    }
    protected void btnAddNewTraining_Click(object sender, EventArgs e)
    {
        Response.Redirect("TrainingSetup.aspx");
    }

    protected void LoadList()
    {
        DataTable dt = _trainingDal.GetTrainingList("");
        gv_trainingList.DataSource = dt;
        gv_trainingList.DataBind();
    }
    protected void lb_Edit_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField hdpk = (HiddenField)gv_trainingList.Rows[rowID].FindControl("hdpk");

        Response.Redirect("TrainingSetup.aspx?mid=" + hdpk.Value);
    }
    protected void lb_remove_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField hdpk = (HiddenField)gv_trainingList.Rows[rowID].FindControl("hdpk");

       

        bool result = _trainingDal.DeleteTrainingSetup(Convert.ToInt32(hdpk.Value), Session["LoginName"].ToString());

        if (result == true)
        {
            AlertMessageBoxShow("Operation Successful...");
            LoadList();
        }
        else
        {

            AlertMessageBoxShow("Operation Failed...");

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
}