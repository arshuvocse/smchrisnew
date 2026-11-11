using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.HealthCare_DAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class HealthCare_UI_BillSettlementListNew : System.Web.UI.Page
{

    private CommitteSetupDal aCommitteSetupDal = new CommitteSetupDal();

    private TopSheetDal aSheetDal = new TopSheetDal();

    
    ShowMessage aShowMessage = new ShowMessage();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            LoadInformation();
        }

        try
        {
            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            loadGridView.UseAccessibleHeader = true;
        }
        catch (Exception)
        {

            //throw;
        }
    }


    private void LoadInformation()
    {
        DataTable dataTable = aSheetDal.Get_TopSheet_BillSattelment();

        if (dataTable.Rows.Count > 0)
        {
            loadGridView.DataSource = dataTable;
            loadGridView.DataBind();
            this.loadGridView.ShowFooter = true;
            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }


    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("TopSheetGenerate.aspx");
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }



    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "CommitteeSetupEdit")
        {
            var datKey = loadGridView.DataKeys[0];
            if (datKey != null)
            {
                string MId = e.CommandArgument.ToString();
                Response.Redirect("CommiteeSetup.aspx?MID=" + MId);
            }
        }

        if (e.CommandName == "ViewReport")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                PopUp(Convert.ToInt32(loadGridView.DataKeys[rowindex][0].ToString()));
            }
        }


        if (e.CommandName == "Evaluate")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                //string id = datKey["ReimbursFromMasterId"].ToString();
                Response.Redirect("BillSettlement_New.aspx?id=" + loadGridView.DataKeys[rowindex][0].ToString());
            }
        }
    }

    private void PopUp(Int32 MasterId)
    {
        string url = "../Report_UI/HC_TopSheetReportViewer.aspx?rptType=" + MasterId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
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

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("BillSettlementView.aspx");

    }
}