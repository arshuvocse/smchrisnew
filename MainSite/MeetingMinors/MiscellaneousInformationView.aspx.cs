using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MeetingMinorsDAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class MeetingMinors_MiscellaneousInformationView : System.Web.UI.Page
{
    MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDownList();
        }
        try
        {

            gv_ViewList.UseAccessibleHeader = true;
            gv_ViewList.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv_ViewList.FooterRow.TableSection = TableRowSection.TableFooter;
            gv_ViewList.UseAccessibleHeader = true;

        }
        catch (Exception ex)
        {


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
        Response.Redirect("MiscellaneousInformation.aspx");
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
            parameter = parameter + "  and mas.CreateBy=" + ddlCreatedBy.SelectedValue.Trim() + "  ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.CreateDate BETWEEN '" + txtCreatedDate.Text + "' AND '" + txtToDate.Text + "' ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND mas.CreateDate BETWEEN '" + txtCreatedDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (txtCreatedDate.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.CreateDate BETWEEN '" + txtToDate.Text + "' AND '" + txtToDate.Text + "' ";
        }

        if (txtKeySearch.Text != "")
        {
            parameter = parameter + "  and  mas.KeySearch LIKE '%" + txtKeySearch.Text.Trim() + "%'   ";
        }
        
        return parameter;
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    private void LoadGrid()
    {
        if (ddlCompany.SelectedValue != "")
        {
            DataTable aDataTable = AMAsterDal.LoadInfo(GenerateParamiterList());
            if (aDataTable.Rows.Count > 0)
            {
                gv_ViewList.DataSource = aDataTable;
                gv_ViewList.DataBind();
                gv_ViewList.UseAccessibleHeader = true;
                gv_ViewList.HeaderRow.TableSection = TableRowSection.TableHeader;
                gv_ViewList.FooterRow.TableSection = TableRowSection.TableFooter;
                gv_ViewList.UseAccessibleHeader = true;

                for (int i = 0; i < aDataTable.Rows.Count; i++)
                {

                    LinkButton btnEdit = (LinkButton)gv_ViewList.Rows[i].FindControl("btnEdit");
                    HiddenField hfActionStatus = (HiddenField)gv_ViewList.Rows[i].FindControl("hfActionStatus");
                    LinkButton btnRemove = (LinkButton)gv_ViewList.Rows[i].FindControl("btnRemove");
        string text = aDataTable.Rows[i].Field<string>("isEditBtn").Trim().ToString();

        if (text=="True")
        {
            btnEdit.Visible = true;
        }
        else
        {
            btnEdit.Visible = false;
            
        }

        if (hfActionStatus.Value == "Drafted")
        {
            btnRemove.Visible = true;
        }
        else
        {
            btnRemove.Visible = false;
            
        }
                }
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
        
    }


    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue!="")
        {
            Session["CompanyId"] = "";
            Session["CompanyId"] = ddlCompany.SelectedValue;
            AMAsterDal.GetUserListDropdown(ddlCreatedBy, ddlCompany.SelectedValue);
            if (Session["UserTypeId"].ToString() == "3" ||
                Session["UserTypeId"].ToString() == "4")
            {
                ddlCreatedBy.Enabled = true;
             //   AMAsterDal.GetMiscellaneousKeySearchDropdown(ddlKeySearch, ddlCompany.SelectedValue);
            }
            else
            {
                ddlCreatedBy.SelectedValue = Session["UserId"].ToString();
                ddlCreatedBy.Enabled = false;
              //  AMAsterDal.GetMiscellaneousKeySearchDropdown(ddlKeySearch, ddlCompany.SelectedValue, Session["UserId"].ToString());


            }
          

        }
        else
        {
            ddlCreatedBy.Items.Clear();
        }
    }

    protected void btnView_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "View";
        HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfMiscellaneousInfoId");
        Response.Redirect("MiscellaneousInformationViewDetails.aspx?MID=" + mastrId.Value.Trim());
    }

    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "Edit";
        HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfMiscellaneousInfoId");
        Response.Redirect("MiscellaneousInformation.aspx?MID=" + mastrId.Value.Trim());
    }

    protected void btnRemove_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "Delete";
        HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfMiscellaneousInfoId");
        Response.Redirect("MiscellaneousInformation.aspx?MID=" + mastrId.Value.Trim());
    }

    protected void txtKeySearch_OnTextChanged(object sender, EventArgs e)
    {
        txtKeySearch.Text = txtKeySearch.Text.Trim();
    }
}