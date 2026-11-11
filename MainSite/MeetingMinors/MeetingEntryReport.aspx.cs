using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL.MeetingMinorsDAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class MeetingMinors_MeetingEntryReport : System.Web.UI.Page
{
    MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();
    MeetingEntryDAL AMeetingEntryDAL = new MeetingEntryDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDownList();
        }
    }

    private void LoadDropDownList()
    {
        AMAsterDal.GetCompanyListIntoDropdown(ddlCompany);
        ddlCompany.SelectedIndex = 1;
        ddlCompany_OnSelectedIndexChanged(null, null);

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
    protected void AddNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("MeetingEntry.aspx");
    }

   
    protected void btn_Search_OnClick(object sender, EventArgs e)
    {
       LoadGrid();
    }
    ShowMessage aShowMessage = new ShowMessage();



    private string GenerateParamiterList()
    {


        string parameter = " ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND mas.CompanyId = " + ddlCompany.SelectedValue;
        }

        if (txtCreatedDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.MeetingDate BETWEEN '" + txtCreatedDate.Text + "' AND '" + txtToDate.Text + "' ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND mas.MeetingDate BETWEEN '" + txtCreatedDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (txtCreatedDate.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.MeetingDate BETWEEN '" + txtToDate.Text + "' AND '" + txtToDate.Text + "' ";
        }



        if (txtKeySearch.Text != "")
        {
            parameter = parameter + "  and  mas.KeySearch LIKE '%" + txtKeySearch.Text.Trim() + "%'   ";
        }
        
        return parameter;
    }
    private void LoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = AMeetingEntryDAL.LoadInfoReport(GenerateParamiterList());
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
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                gv_ViewList.DataSource = null;
                gv_ViewList.DataBind();
            }
        }
        else
        {
            gv_ViewList.DataSource = null;
            gv_ViewList.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    }

    protected void lbReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MeetingEntryReport.aspx");
    }



    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (gv_ViewList.Rows.Count > 0)
        {
            string attachment = "attachment; filename=Board Minutes Entry Report.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            gv_ViewList.AllowPaging = false;

            LoadGrid();


            // Create a form to contain the grid  
            HtmlForm frm = new HtmlForm();
            gv_ViewList.Parent.Controls.Add(frm);
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(loadGridView);
            //frm.RenderControl(htw);

            gv_ViewList.HeaderRow.Style.Add("background-color", "#E5EEF1");

            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in gv_ViewList.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in gv_ViewList.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                }
            }
            string com = "";
            if (ddlCompany.SelectedValue == "1")
            {
                com = "Social Marketing Company";
            }
            if (ddlCompany.SelectedValue == "2")
            {
                com = "SMC Enterprise Ltd.";
            }
            gv_ViewList.RenderControl(htw);
            string headerTable = @"<span   style='text-align:center'><h3>" + com +
                                 " </h3><h4>Board Minutes Entry Report</h4>  </span> <span   style='text-align:right'><h5> Print Date: " +
                                 DateTime.Now.ToString("dd-MMM-yyyy") + "</h5></span>";



            HttpContext.Current.Response.Write(headerTable);

            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            aShowMessage.ShowMessageBox("No Data Found!!", this);
        }
    }

//     protected void btnExportToExcel_Click(object sender, EventArgs e)
//    {
//        if (gv_ViewList.Rows.Count > 0)
//        {
//            string attachment = "attachment; filename=Employee_List_Info.xls";
//            Response.ClearContent();
//            Response.AddHeader("content-disposition", attachment);
//            Response.ContentType = "application/ms-excel";
//            StringWriter sw = new StringWriter();
//            HtmlTextWriter htw = new HtmlTextWriter(sw);

//            //gv_ViewList.AllowPaging = false;





//            // Create a form to contain the grid  
//            HtmlForm frm = new HtmlForm();
//            gv_ViewList.Parent.Controls.Add(frm);
//            //frm.Attributes["runat"] = "server";
//            //frm.Controls.Add(loadGridView);
//            //frm.RenderControl(htw);

//            gv_ViewList.HeaderRow.Style.Add("background-color", "#E5EEF1");

//            // Set background color of each cell of GridView1 header row
//            foreach (TableCell tableCell in gv_ViewList.HeaderRow.Cells)
//            {
//                tableCell.Style["background-color"] = "#E5EEF1";
//            }

//            // Set background color of each cell of each data row of GridView1
//            foreach (GridViewRow gridViewRow in gv_ViewList.Rows)
//            {
//                gridViewRow.BackColor = System.Drawing.Color.White;

//                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
//                {
//                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

//                }
//            }


//            gv_ViewList.RenderControl(htw);

//            string comName = "";

//            if (ddlCompany.SelectedValue == "1")
//            {
//                comName = "Social Marketing Company";
//            }
//            else
//            {
//                comName = "SMC Enterprise Ltd.";

//            }
//            string headerTable = @"<span  style='text-align:left'><h3> Board Minutes Entry Report </h4>  </span> <span   style='text-align:right'><h4> Print Date: " +
//                                 DateTime.Now.ToString("dd-MMM-yyyy") + "</h4></span>";

//            string SubTi = @"<span   style='text-align:center'>
//   <h5> "+ comName +" </h5></span>";

//            HttpContext.Current.Response.Write(headerTable);
//            HttpContext.Current.Response.Write(SubTi);
//            Response.Write(sw.ToString());
//            Response.End();
//        }
//        else
//        {
//            aShowMessage.ShowMessageBox("No Data Found!!", this);
//        }
//    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
 

    protected void btnView_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "View";
        HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfMeetingInfoID");
        Response.Redirect("MeetingEntryViewDetails.aspx?MID=" + mastrId.Value.Trim());
    }

    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "Edit";
        HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfMeetingInfoID");
        Response.Redirect("MeetingEntry.aspx?MID=" + mastrId.Value.Trim());
    }

    protected void btnRemove_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "Delete";
        HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfMeetingInfoID");
        Response.Redirect("MeetingEntry.aspx?MID=" + mastrId.Value.Trim());
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Session["CompanyId"] = "";
        Session["CompanyId"] = ddlCompany.SelectedValue;
        AMAsterDal.GetUserListDropdown(ddlCreatedBy, ddlCompany.SelectedValue);
        if (Session["UserTypeId"].ToString() == "3" ||
               Session["UserTypeId"].ToString() == "4")
        {
            

          ddlCreatedBy.Enabled = true;
           // AMeetingEntryDAL.GetMeetingKeySearchDropdown(ddlKeySearch, ddlCompany.SelectedValue);
        }
        else
        {
            ddlCreatedBy.SelectedValue = Session["UserId"].ToString();

            ddlCreatedBy.Enabled = false;
         //   AMeetingEntryDAL.GetMeetingKeySearchDropdown(ddlKeySearch, ddlCompany.SelectedValue, Session["UserId"].ToString());


        }


       
      
          
    }
}