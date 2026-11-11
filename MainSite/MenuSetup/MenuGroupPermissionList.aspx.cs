using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MenuSetup_DAL;
using DAO.HRIS_DAO_EF;

public partial class MenuSetup_MenuGroupPermissionList : System.Web.UI.Page
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
        using (DataTable dt = _menuSetupCommonDal.GetMenuGroupPermissionList())
        {
            gv_Menu.DataSource = dt;
            gv_Menu.DataBind();
        }

    }
    protected void btn_New_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MenuGroupPermission.aspx");
    }

    protected void lb_Edit_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField hdpk = (HiddenField)gv_Menu.Rows[rowID].FindControl("hdpk");

        Response.Redirect("MenuGroupPermission.aspx?mid=" + hdpk.Value);
    }

    protected void lb_Remove_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField hdpk = (HiddenField)gv_Menu.Rows[rowID].FindControl("hdpk");
        int pk = Int32.Parse(hdpk.Value);

        var db = new HRIS_SMCEntities();
        tblMenuGroupPermission IVMaster = (from emd in db.tblMenuGroupPermissions where emd.MenuGroupPermissionId == pk select emd).FirstOrDefault();
        IVMaster.IsActive = false;

        db.SaveChanges();

        LoadInitialGrid();
    }

    protected void gv_Menu_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        //check if the row is the header row
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //add the thead and tbody section programatically
            e.Row.TableSection = TableRowSection.TableHeader;
        }
    }
}