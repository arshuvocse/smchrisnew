using DAL.COMMON_DAL;
using DAL.MenuSetup_DAL;
using DAL.MPBudget;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserSetup_UserList : System.Web.UI.Page
{

    ShowMessage aShowMessage = new ShowMessage();
    private static string _userId;
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {
            LoadInitialDDL();

         //   LoadInfo();

        }

        try
        {

            //loadGridView.UseAccessibleHeader = true;
            //loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            //loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            //loadGridView.UseAccessibleHeader = true;

        }
        catch (Exception ex)
        {


        }

    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            LoadInfo();
        }
        else
        {
            ddlCompany.Focus();
            aShowMessage.ShowMessageBox("Please Select This !!!", this);
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
            ActiveStatusDropDownList_OnSelectedIndexChanged(null, null);
            ddlCompany_SelectedIndexChanged(null, null);
        }

    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {



            Session["CompanyId"] = "";
            Session["CompanyId"] = ddlCompany.SelectedValue;

            ActiveStatusDropDownList_OnSelectedIndexChanged(null, null);
        }
    }
    protected void ActiveStatusDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (ActiveStatusDropDownList.SelectedValue == "-1")
        {
            using (DataTable dt = _commonDataLoad.GetEmpDDL(ddlCompany.SelectedValue.ToString()))
            {
                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;

            }
        }
        else if (ActiveStatusDropDownList.SelectedValue == "1")
        {
            using (DataTable dt = _commonDataLoad.GetEmpDDLAActiveOnlyView(ddlCompany.SelectedValue.ToString()))
            {
                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;
            }
        }
        else if (ActiveStatusDropDownList.SelectedValue == "0")
        {
            using (DataTable dt = _commonDataLoad.GetEmpDDLAInactive(ddlCompany.SelectedValue.ToString()))
            {
                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;

            }
        }

    }

    private void LoadInfo()
    {
        UserCommonDAL _userCommonDal = new UserCommonDAL();
        
        DataTable dt= new DataTable();
        dt = _userCommonDal.LoadUserList(GenerateParameter());

        if (dt.Rows.Count > 0)
        {
            loadGridView.DataSource = dt;
            loadGridView.DataBind(); 
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
        }
    }
    private string GenerateParameter()
    {
        string parameter = " ";
        if (ddlUserStatus.SelectedValue != "-1") // -1 মানে All
        {
            parameter += " AND u.IsActive = " + ddlUserStatus.SelectedValue + " ";
        }

        // যদি EmpInfoId সিলেক্ট করা থাকে, তাহলে শুধু এটিই ফিল্টার হবে
        if (!string.IsNullOrEmpty(ddlEmpInfo.SelectedValue))
        {
            parameter += " and ( e.EmpInfoId = " + ddlEmpInfo.SelectedValue.Trim() + " )";
            return parameter; // CompanyId, IsActive বাদ দিয়ে এখানেই return
        }

        if (ddlCompany.SelectedIndex > 0)
        {
            parameter = parameter + "  and    e.CompanyId = '" + ddlCompany.SelectedValue + "'";
        }
        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and  ( e.EmpInfoId=" + ddlEmpInfo.SelectedValue.Trim() + ")";
        }
        
        return parameter;

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

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("UserEntry.aspx");
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
}