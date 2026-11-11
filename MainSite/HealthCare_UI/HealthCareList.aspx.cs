using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.HealthCare_DAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class HealthCare_UI_HealthCareList : System.Web.UI.Page
{

    private HealthCareListDal aListDal = new HealthCareListDal();

    private ShowMessage aMessage = new ShowMessage();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_Initail_DD_List();
        }
    }


    private void Load_Initail_DD_List()
    {
        aListDal.GetDDLCompany(ddlCompany);
        ddlCompany.SelectedValue = 1.ToString();
    }


    protected void gv_JdBoard_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "A")
        {

        }
    }

    private string Generateparam()
    {
        string param = "";

        if (ddlCompany.SelectedValue != "")
        {
            param = param + "AND M.CompanyId = '"+ddlCompany.SelectedValue+"' ";
        }

        return param;
    }


    private void GET_List()
    {
        DataTable Dt = aListDal.Get_All_Employee_List(Generateparam());

        if (Dt.Rows.Count > 0)
        {
            gv_JdBoard.DataSource = Dt;
            gv_JdBoard.DataBind();
        }
        else
        {
            aMessage.ShowMessageBox("Data not found",this);
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

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        GET_List();
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("TopSheetGenerate.aspx");
    }
}