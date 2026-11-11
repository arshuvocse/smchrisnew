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

public partial class MeetingMinors_BoardMeetingAuditTrail : System.Web.UI.Page
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

    private void DocumentLoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = AMAsterDal.LoadInfoDocumentAuditTrail(DocumentGenerateParamiterList(), DocumentGenerateParamiterListEdit());
            if (aDataTable.Rows.Count > 0)
            {
                GridViewDocument.DataSource = aDataTable;
                GridViewDocument.DataBind();

                for (int i = 0; i < GridViewDocument.Rows.Count; i++)
                {
                    Label lbl_Statusw = (Label)GridViewDocument.Rows[i].Cells[0].FindControl("lbl_Statusw");

                    if (lbl_Statusw.Text == "Initial")
                    {
                        GridViewDocument.Rows[i].Visible = false;
                    }
                }
           
            }
            else
            {
                aShowMessage.ShowMessageBox("No Data Found!!!", this);
                GridViewDocument.DataSource = null;
                GridViewDocument.DataBind();
            }
        }
        else
        {
            GridViewDocument.DataSource = null;
            GridViewDocument.DataBind();
            aShowMessage.ShowMessageBox("Please select company name!!!", this);
        }
    }
    protected void btn_Search_OnClick(object sender, EventArgs e)
    {
        gv_ViewList.DataSource = null;
        gv_ViewList.DataBind();



        GridViewDocument.DataSource = null;
        GridViewDocument.DataBind();
        if (ddlOperation.SelectedValue == "Board-Meeting")
        {
            LoadGrid();
        }
        else if (ddlOperation.SelectedValue == "Document")
        {
            DocumentLoadGrid();
        }

        else
        {
            aShowMessage.ShowMessageBox("Please select Operation!!!", this);
            ddlOperation.Focus();
        }
      
    }
    ShowMessage aShowMessage = new ShowMessage();

    private string DocumentGenerateParamiterList()
    {


        string parameter = " ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND mas.CompanyId = " + ddlCompany.SelectedValue;
        }

        if (txtTitle.Text != "")
        {
            parameter = parameter + "  and  mas.Title LIKE '%''" + txtTitle.Text.Trim() + "''%'   ";
        }
        if (txtPropuse.Text != "")
        {
            parameter = parameter + "  and  mas.Purpose LIKE '%''" + txtPropuse.Text.Trim() + "''%'   ";
        }
        if (ddlCreatedBy.Text != "")
        {
            parameter = parameter + "  and mas.DeleteBy=" + ddlCreatedBy.SelectedValue.Trim() + "  ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.DeleteDate BETWEEN '" + txtCreatedDate.Text + "'  AND " + " DATEADD(day,1, '" + Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND mas.DeleteDate BETWEEN '" + txtCreatedDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (txtCreatedDate.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.DeleteDate BETWEEN '" + txtToDate.Text + "'  AND " + " DATEADD(day,1, '" + Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }

        if (hfKeySearchId.Value != "")
        {
            parameter = parameter + "  and  mas.AuditLog_MiscellaneousInfoId ='" + (hfKeySearchId.Value.Trim()) + "'   ";
        }

        return parameter;
    }


    private string DocumentGenerateParamiterListEdit()
    {


        string parameter = " ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND mas.CompanyId = " + ddlCompany.SelectedValue;
        }

        if (txtTitle.Text != "")
        {
            parameter = parameter + "  and  mas.Title LIKE '%''" + txtTitle.Text.Trim() + "''%'   ";
        }
        if (txtPropuse.Text != "")
        {
            parameter = parameter + "  and  mas.Purpose LIKE '%''" + txtPropuse.Text.Trim() + "''%'   ";
        }
        if (ddlCreatedBy.Text != "")
        {
            parameter = parameter + "  and mas.UpdateBy=" + ddlCreatedBy.SelectedValue.Trim() + "  ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.UpdateDate BETWEEN '" + txtCreatedDate.Text + "'  AND " + " DATEADD(day,1, '" + Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND mas.UpdateDate BETWEEN '" + txtCreatedDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (txtCreatedDate.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.UpdateDate BETWEEN '" + txtToDate.Text + "'  AND " + " DATEADD(day,1, '" + Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }



        return parameter;
    }

    private string GenerateParamiterList()
    {


        string parameter = " ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND mas.CompanyId = " + ddlCompany.SelectedValue;
        }


        if (ddlCreatedBy.Text != "")
        {
            parameter = parameter + "  and mas.DeleteBy=" + ddlCreatedBy.SelectedValue.Trim() + "  ";
        }

        if (txtCreatedDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.DeleteDate BETWEEN '" + txtCreatedDate.Text + "'  AND " + " DATEADD(day,1, '" + Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND mas.DeleteDate BETWEEN '" + txtCreatedDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (txtCreatedDate.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.DeleteDate BETWEEN '" + txtToDate.Text  + "'  AND " + " DATEADD(day,1, '" + Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }
        



        if (txtKeySearch.Text != "")
        {
            parameter = parameter + "  and  mas.KeySearch LIKE '%" + txtKeySearch.Text.Trim() + "%'   ";
        }
        
        return parameter;
    }

    private string GenerateParamiterListEdit()
    {


        string parameter = " ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND mas.CompanyId = " + ddlCompany.SelectedValue;
        }


        if (ddlCreatedBy.Text != "")
        {
            parameter = parameter + "  and mas.UpdateBy=" + ddlCreatedBy.SelectedValue.Trim() + "  ";
        }

        if (txtCreatedDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.UpdateDate BETWEEN '" + txtCreatedDate.Text + "'  AND " + " DATEADD(day,1, '" + Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND mas.UpdateDate BETWEEN '" + txtCreatedDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (txtCreatedDate.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.UpdateDate BETWEEN '" + txtToDate.Text + "'  AND " + " DATEADD(day,1, '" + Convert.ToDateTime(txtToDate.Text.Trim()) + "')  ";
        }



        

        return parameter;
    }
    protected void vcchomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    private void LoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = AMeetingEntryDAL.LoadInfoAuditTrail(GenerateParamiterList(), GenerateParamiterListEdit());
            if (aDataTable.Rows.Count > 0)
            {
                gv_ViewList.DataSource = aDataTable;
                gv_ViewList.DataBind();
               
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
        Response.Redirect("BoardMeetingAuditTrail.aspx");
        
    }


 

    protected void btnView_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "View";
        HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfMeetingInfoID");
        Label lbl_Status = (Label)gv_ViewList.Rows[rowID].FindControl("lbl_Status");
        Response.Redirect("MeetingEntryViewDetailsAudit.aspx?MID=" + mastrId.Value.Trim() + "&Status=" + lbl_Status.Text.Trim());
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

        if (ddlCompany.SelectedValue!="")
        {
            Session["CompanyId"] = ddlCompany.SelectedValue;
             
        }
        //AMAsterDal.GetUserListDropdown(ddlCreatedBy, ddlCompany.SelectedValue);
        //if (Session["UserTypeId"].ToString() == "3" ||
        //       Session["UserTypeId"].ToString() == "4")
        //{
            

        //  ddlCreatedBy.Enabled = true;
        //   // AMeetingEntryDAL.GetMeetingKeySearchDropdown(ddlKeySearch, ddlCompany.SelectedValue);
        //}
        //else
        //{
        //    ddlCreatedBy.SelectedValue = Session["UserId"].ToString();

        //    ddlCreatedBy.Enabled = false;
        // //   AMeetingEntryDAL.GetMeetingKeySearchDropdown(ddlKeySearch, ddlCompany.SelectedValue, Session["UserId"].ToString());


        //}


       
      
          
    }

    protected void ddlOperation_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        gv_ViewList.DataSource = null;
        gv_ViewList.DataBind();



        GridViewDocument.DataSource = null;
        GridViewDocument.DataBind();
    }

    protected void btnViewDoc_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

       
        HiddenField mastrId = (HiddenField)GridViewDocument.Rows[rowID].FindControl("hfMiscellaneousInfoId");

        //Response.Redirect("MiscellaneousInformationViewDetailsAudit.aspx?rptType=Document&MID=" + mastrId.Value.Trim());

        string url = "../Report_UI/BoardMeetingAuditTrailViwer.aspx?rptType=" + "Document&MID=" + mastrId.Value.Trim();
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);


    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        if (gv_ViewList.Rows.Count > 0)
        {
            // Clear all content output from the buffer stream
            Response.ClearContent();
            // Specify the default file name using "content-disposition" RESPONSE header
            Response.AppendHeader("content-disposition", "attachment; filename=Meeting_Audit_Trail_Information.xls");
            // Set excel as the HTTP MIME type
            Response.ContentType = "application/excel";
            // Create an instance of stringWriter for writing information to a string
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            // Create an instance of HtmlTextWriter class for writing markup 
            // characters and text to an ASP.NET server control output stream


            gv_ViewList.Columns[gv_ViewList.Columns.Count - 1].Visible =
               false;




            gv_ViewList.AllowPaging = false;
            HtmlTextWriter htw = new HtmlTextWriter(stringWriter);

            // Set white color as the background color for gridview header row
            gv_ViewList.HeaderRow.Style.Add("background-color", "#FFFFFF");

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
            string headerTable = @"<span  style='text-align:center'><h3> " + ddlCompany.SelectedItem.Text + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
               <h3>Meeting Audit Trail Information	</h3>
            
            </span>";
            gv_ViewList.RenderControl(htw);
            HttpContext.Current.Response.Write(headerTable);
                    HttpContext.Current.Response.Write(SubTi);
            Response.Write(stringWriter.ToString());
            Response.End();

 
        }
        else   if (GridViewDocument.Rows.Count > 0)
        {


            // Clear all content output from the buffer stream
            Response.ClearContent();
            // Specify the default file name using "content-disposition" RESPONSE header
            Response.AppendHeader("content-disposition", "attachment; filename=Document_Audit_Trail_Information.xls");
            // Set excel as the HTTP MIME type
            Response.ContentType = "application/excel";
            // Create an instance of stringWriter for writing information to a string
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            // Create an instance of HtmlTextWriter class for writing markup 
            // characters and text to an ASP.NET server control output stream


            GridViewDocument.Columns[GridViewDocument.Columns.Count - 1].Visible =
               false;




            GridViewDocument.AllowPaging = false;
            HtmlTextWriter htw = new HtmlTextWriter(stringWriter);

            // Set white color as the background color for gridview header row
            GridViewDocument.HeaderRow.Style.Add("background-color", "#FFFFFF");

            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in GridViewDocument.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in GridViewDocument.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;
                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";
                }
            }
            string headerTable = @"<span  style='text-align:center'><h3> " + ddlCompany.SelectedItem.Text + "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " + DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
               <h3>Document Audit Trail Information	</h3>
            
            </span>";
            GridViewDocument.RenderControl(htw);
            HttpContext.Current.Response.Write(headerTable);
            HttpContext.Current.Response.Write(SubTi);
            Response.Write(stringWriter.ToString());
            Response.End();
          
          
        }
        else
        {
            showMessageBox("No Data Found!!");
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void btnViewMeeting_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;


        HiddenField hfMeetingInfoID = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfMeetingInfoID");

        //Response.Redirect("MiscellaneousInformationViewDetailsAudit.aspx?rptType=Document&MID=" + mastrId.Value.Trim());

        string url = "../Report_UI/BoardMeetingAuditTrailViwer.aspx?rptType=" + "Meeting&MID=" + hfMeetingInfoID.Value.Trim();
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void txtKeySearch_OnTextChanged(object sender, EventArgs e)
    {
        string empid = txtKeySearch.Text.Trim();
        if (empid.Contains("|"))
        {
            string[] strsp = empid.Split('|');
            hfKeySearchId.Value = strsp[1]; 
        }
        else
        {
            hfKeySearchId.Value = "";
            txtKeySearch.Text = "";
            showMessageBox("Please Input Valid Data!!!");
        }
    }
}