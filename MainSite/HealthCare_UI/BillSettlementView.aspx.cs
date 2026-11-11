using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.HealthCare_DAL;
using DAL.InternalCls;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class HealthCare_UI_BillSettlementView : System.Web.UI.Page
{


    BillSettlementDal aSettlementDal = new BillSettlementDal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Get_BillSettlementList();
        }
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("BillSettlementListNew.aspx");   
    }

    protected void Get_BillSettlementList()
    {
        DataTable dt = aSettlementDal.Get_BillSettlement_For_View();

        if (dt.Rows.Count > 0)
        {
            loadGridView.DataSource = dt;
            loadGridView.DataBind();
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
    ShowMessage aShowMessage = new ShowMessage();

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ViewReport")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                PopUp(Convert.ToInt32(loadGridView.DataKeys[rowindex][0].ToString()));
            }
        }

        if (e.CommandName == "Delete")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                 
               int  masterId = Convert.ToInt32(loadGridView.DataKeys[rowindex][0].ToString());


                bool ff =  aSettlementDal.DeleteIncrementMaster(masterId.ToString());

                if (ff)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successfully Done...');window.location ='BillSettlementView.aspx';",
                   true);
                   
                   
                }
                else
                {
                   aShowMessage.ShowMessageBox("Operation Failed!", this);
                    
                }



            }
        }
    }

    private void PopUp(Int32 MasterId)
    {
        string url = "../Report_UI/ReimbursmentBillViewer.aspx?rptType=" + MasterId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

   
}