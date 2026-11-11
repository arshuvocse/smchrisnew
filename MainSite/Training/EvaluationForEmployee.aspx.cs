using DAL.COMMON_DAL;
using DAL.TrainingDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Training_EvaluationForEmployee : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadGrid();
        }
    }
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void gv_DocumentUpload_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;

        if ((gv.ShowHeader == true && gv.Rows.Count > 0)
            || (gv.ShowHeaderWhenEmpty == true))
        {
            //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void LoadGrid()
    {

        //DataTable dt = _trainingDal.GetTrainingEvaluationForm();

        //if (dt.Rows.Count!=0)
        //{
        //    gv_formList.DataSource = dt;
        //    gv_formList.DataBind();
        //}
        //else
        {

            DataTable dtddd = _trainingDal.GetTrainingEvaluationFormForShow();
            gv_formList.DataSource = dtddd;
            gv_formList.DataBind();
        }
      


        for (int i = 0; i < gv_formList.Rows.Count; i++)
        {


            DataTable dt2 = _trainingDal.GetPermissionForEvulate(((HiddenField)gv_formList.Rows[i].FindControl("hdpkd")).Value, Session["EmpInfoId"].ToString());

            for (int h = 0; h < dt2.Rows.Count; h++)
            {
                if (dt2.Rows.Count > 0)
                {
                    ((LinkButton)gv_formList.Rows[i].FindControl("btn_edit")).Visible = false;
                    ((LinkButton)gv_formList.Rows[i].FindControl("lbView")).Visible = true;
                }
                else
                {
                    ((LinkButton)gv_formList.Rows[i].FindControl("btn_edit")).Visible = true;
                    ((LinkButton)gv_formList.Rows[i].FindControl("lbView")).Visible = false;
                }

            }
            
        }
       

     

    }
    protected void detailsViewButton_Click(object sender, EventArgs e)
    {

    }

    protected void btn_edit_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField hdpk = (HiddenField)gv_formList.Rows[rowID].FindControl("hdpkd");
        Session["ForView"] = "";
        Response.Redirect("EvaluateTraining.aspx?mid=" + hdpk.Value);
    }

    protected void lbView_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField hdpk = (HiddenField)gv_formList.Rows[rowID].FindControl("hdpkd");
        Session["ForView"] = "";
        Session["ForView"] = "View";
        Response.Redirect("EvaluateTraining.aspx?mid=" + hdpk.Value);
    }
}