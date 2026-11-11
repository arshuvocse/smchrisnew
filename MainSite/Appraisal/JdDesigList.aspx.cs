using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;

public partial class Appraisal_JdList : System.Web.UI.Page
{
    private JdDesigDAL _JdDesigDAL = new JdDesigDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = _JdDesigDAL.GetJdList();
        gv_JdBoard.DataSource = dt;
        gv_JdBoard.DataBind();
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("JobDescriptionDesig.aspx");
    }

    protected void btn_edit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("JdMasterId");




        Response.Redirect("JobDescriptionDesig.aspx?masterId=" + mastrId.Value + "");
    }

    protected void btn_Remove_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("JdMasterId");

        bool result = _JdDesigDAL.DeleteJd(Convert.ToInt32(mastrId.Value), Session["LoginName"].ToString());

        if (result == true)
        {
            AlertMessageBoxShow("Operation Successful...");

            DataTable dt = _JdDesigDAL.GetJdList();
            gv_JdBoard.DataSource = dt;
            gv_JdBoard.DataBind();
           
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