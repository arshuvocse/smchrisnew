using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MenuSetup_DAL;
using DAO.HRIS_DAO_EF;

public partial class MenuSetup_MenuGroupSetupList : System.Web.UI.Page
{
    private MenuSetupCommonDAL _menuSetupCommonDal = new MenuSetupCommonDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInitialGrid();
        }
        try
        {
            gv_Menu.UseAccessibleHeader = true;
            gv_Menu.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        catch (Exception ex)
        {
            
            
        }
        
    }

    private void LoadInitialGrid()
    {
        using (DataTable dt = _menuSetupCommonDal.GetMenuGroupSetupList())
        {
            gv_Menu.DataSource = dt;
            gv_Menu.DataBind();
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

    protected void btn_New_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MenuGroupSetup.aspx");
    }

    protected void lb_Edit_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField hdpk = (HiddenField)gv_Menu.Rows[rowID].FindControl("hdpk");

        Response.Redirect("MenuGroupSetup.aspx?mid=" + hdpk.Value);
    }

    protected void lb_Remove_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField hdpk = (HiddenField)gv_Menu.Rows[rowID].FindControl("hdpk");
        int pk = Int32.Parse(hdpk.Value);

        var db = new HRIS_SMCEntities();
        tblMenuGroupSetup IVMaster = (from emd in db.tblMenuGroupSetups where emd.MenuGroupSetupId == pk select emd).FirstOrDefault();
        IVMaster.IsActive = false;
        
        db.SaveChanges();

        LoadInitialGrid();
    }
    protected void gv_Menu_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //check if the row is the header row
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //add the thead and tbody section programatically
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }
}