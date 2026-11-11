using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MeetingMinorsDAL;

public partial class MeetingMinors_MeetingApprovalView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // LoadDropDownList();
            LoadGrid();
        }
    }
    MeetingEntryDAL AMeetingEntryDAL = new MeetingEntryDAL();
    private void LoadGrid()
    {

        DataTable aDataTable = AMeetingEntryDAL.LoadInfoApproveList();
        if (aDataTable.Rows.Count > 0)
        {
            gv_ViewList.DataSource = aDataTable;
            gv_ViewList.DataBind();
            gv_ViewList.UseAccessibleHeader = true;
            gv_ViewList.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv_ViewList.FooterRow.TableSection = TableRowSection.TableFooter;
            gv_ViewList.UseAccessibleHeader = true;
        }
        else
        {

            gv_ViewList.DataSource = null;
            gv_ViewList.DataBind();
        }

    }
    protected void AddNewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MiscellaneousInformation.aspx");
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

    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;


        Session["Status"] = "Edit";
        HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfMasterId");
        Response.Redirect("MeetingEntryApprrove.aspx?MID=" + mastrId.Value.Trim());
    }
}