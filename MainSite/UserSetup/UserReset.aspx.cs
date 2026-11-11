using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using DAL.COMMON_DAL;
using DAL.MenuSetup_DAL;
using DAO.HRIS_DAO_EF;

public partial class UserSetup_UserReset : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    private static string _userId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {

            LoadInitialDDL();
           

        }

        try
        {

            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            loadGridView.UseAccessibleHeader = true;

        }
        catch (Exception ex)
        {


        }

    }
    private void LoadInitialDDL()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {

            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();

            ddlCompany.SelectedIndex = 1;

            ddlCompany_OnSelectedIndexChanged(null, null);
        }
    }

    private void LoadInfo(string comId)
    {
        UserCommonDAL _userCommonDal = new UserCommonDAL();

        DataTable dt = new DataTable();
        dt = _userCommonDal.LoadUserListActiveNEqw(comId);

        if (dt.Rows.Count > 0)
        {
            loadGridView.DataSource = dt;
            loadGridView.DataBind();
            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            loadGridView.UseAccessibleHeader = true;
        }
    }

    //[WebMethod(EnableSession = true)]
    //public static List<UserEntryDAO> LoadUserList()
    //{
    //    UserCommonDAL _userCommonDal = new UserCommonDAL();
    //    return _userCommonDal.LoadUserList();
    //}

    protected void btn_New_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("UserEntry.aspx");
    }

    [WebMethod(EnableSession = true)]
    public static string DeleteUser(string UserId)
    {
        string status = "ok";
        int mid = int.Parse(UserId);
        try
        {
            using (var db = new HRIS_SMCEntities())
            {
                var mpb = (from u in db.tblUsers where u.UserId == mid select u).FirstOrDefault();
                mpb.IsActive = false;
                mpb.UpdateDate = DateTime.Now;
                mpb.UpdateBy = _userId;
                db.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            status = ex.Message;
        }
        return status;
    }


    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {




        if (e.CommandName == "ResetData")
        {

  
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string id = loadGridView.DataKeys[rowindex][0].ToString();
            string datKey = loadGridView.DataKeys[rowindex][1].ToString();

        
            if (id != "" && datKey != "")
            {
                var dal = new userDal();
                if (dal.ResetPass(Convert.ToInt32(id), datKey))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='UserReset.aspx';",
                    true);
                    //AlertMessageBoxShow("Operation Successful...");
                }
                
        
            }

        }



        if (e.CommandName == "EditData")
        {
            int rowindex = 0;
            var dataKey = loadGridView.DataKeys[rowindex];
            if (dataKey != null)
            {

                string MId = e.CommandArgument.ToString();
                Session["Status"] = "Edit";

                Response.Redirect("UserEntry.aspx?mid=" + MId);
            }


        }

        if (e.CommandName == "ViewData")
        {
            int rowindex = 0;
            string MId = e.CommandArgument.ToString();


            Session["Status"] = "View";
            Response.Redirect("UserEntry.aspx?mid=" + MId);
        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = 0;
            string MId = e.CommandArgument.ToString();



            Session["Status"] = "Delete";
            Response.Redirect("UserEntry.aspx?mid=" + MId);
        }
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
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex >0)
        {
            LoadInfo(ddlCompany.SelectedValue);
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
        }
    }
}