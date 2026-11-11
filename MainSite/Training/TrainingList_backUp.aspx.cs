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
        LoadList();
    }
 

    protected void LoadList(){
        DataTable dt = _trainingDal.GetTrainingList("");
        gv_TrainingList.DataSource = dt;
        gv_TrainingList.DataBind();
    }
    protected void lb_Edit_Click(object sender, EventArgs e)
    {

        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField hdpk = (HiddenField)gv_TrainingList.Rows[rowID].FindControl("hdpk");

        Response.Redirect("TrainingSetup.aspx?mid=" + hdpk.Value);
    }
    protected void btnAddNewTraining_Click(object sender, EventArgs e)
    {
        Response.Redirect("TrainingSetup.aspx");
    }
}