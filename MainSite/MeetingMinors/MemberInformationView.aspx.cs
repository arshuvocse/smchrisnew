using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MeetingMinorsDAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class MeetingMinors_MemberInformationView : System.Web.UI.Page
{


    MemberInfoDaL aMinors = new MemberInfoDaL();

    ShowMessage aShowMessage = new ShowMessage();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Dropdownlist();
            
            // load();
        }

    }



    private string GenerateParameter()
    {
        string param = "    ";

        if (ddlCompany.SelectedValue !="-1")
        {
            param = param + "AND BMSM.CompanyId= " + ddlCompany.SelectedValue;
        }

        if (ddlCategory.SelectedValue != "")
        {
            param = param + " AND BMSM.CategoryID = " + ddlCategory.SelectedValue;
        }

        if (ddlCreateBy.SelectedValue != "")
        {
            param = param + " AND BMSM.CreateBy = " + hfEmpId.Value;
        }

        if (TxtCreateDate.Text != string.Empty && TxtToDate.Text != string.Empty)
        {
            param = param + " AND  MRPM.CreateDate BETWEEN '" + TxtCreateDate.Text + "' AND '" + TxtToDate.Text + "' ";
        }


        if (TxtCreateDate.Text != string.Empty && TxtToDate.Text == string.Empty)
        {
            param = param + " AND MRPM.CreateDate BETWEEN '" + TxtCreateDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (TxtCreateDate.Text == string.Empty && TxtToDate.Text != string.Empty)
        {
            param = param + " AND  MRPM.CreateDate BETWEEN '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' AND '" + TxtToDate.Text + "' ";
        }

        return param;
    }


    private void load()
    {
        try
        {
            DataTable dt = aMinors.loadMember(" and BMSM.BMSM.CompanyId=" + ddlCompany.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                gv_loadGridView.DataSource = dt;
                gv_loadGridView.DataBind();
                gv_loadGridView.UseAccessibleHeader = true;
                gv_loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
                gv_loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
                gv_loadGridView.UseAccessibleHeader = true;
            }
            else
            {

                gv_loadGridView.DataSource = null;
                gv_loadGridView.DataBind();
                // aShowMessage.ShowMessageBox("Data Not Found",this);
            }
        }
        catch
        {
            gv_loadGridView.DataSource = null;
            gv_loadGridView.DataBind();
        }

      

    }

    private void LoadFilteredMembers()
    {
        int selectedValue;
        var criteria = new MemberInformationSearchCriteria();

        if (ddlCompany.SelectedIndex > 0 && int.TryParse(ddlCompany.SelectedValue, out selectedValue))
        {
            criteria.CompanyId = selectedValue;
        }

        DataTable dt = aMinors.loadMember(criteria);
        if (dt.Rows.Count > 0)
        {
            gv_loadGridView.DataSource = dt;
            gv_loadGridView.DataBind();
            gv_loadGridView.UseAccessibleHeader = true;
            gv_loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv_loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
        }
        else
        {
            gv_loadGridView.DataSource = null;
            gv_loadGridView.DataBind();
            aShowMessage.ShowMessageBox("No Data Found", this);
        }
    }

    protected void ButtonView_OnClick(object sender, EventArgs e)
    {
        LoadFilteredMembers();
    }

    MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();

    private void Dropdownlist()
    {

        using (DataTable dt = aMinors.GetDDLCompany())
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
            ddlCompany.SelectedIndex = 1;
            ddlCompany_OnSelectedIndexChanged(null, null);
        }
        using (DataTable dt = aMinors.GetDDLMemberType())
        {
            ddlMemberTypeSearch.DataSource = dt;
            ddlMemberTypeSearch.DataValueField = "Value";
            ddlMemberTypeSearch.DataTextField = "TextField";
            ddlMemberTypeSearch.DataBind();
        }
        AMAsterDal.GetCategoryListIntoDropdown(ddlCategory);
       

    }


    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowindex = Convert.ToInt32(e.CommandArgument);
        string Id = gv_loadGridView.DataKeys[rowindex][0].ToString();

        if (e.CommandName == "EditData")
        {

            string MId = Id;
            Session["Status"] = "Edit";
            Session["MId"] = "";
            Session["MId"] = MId;

            Response.Redirect("MemberInformation.aspx");
        }

        if (e.CommandName == "DeleteData")
        {
            string MId = Id;
            Session["MId"] = "";
            Session["MId"] = MId;
            Session["Status"] = "Delete";
            Response.Redirect("MemberInformation.aspx");
        }

        if (e.CommandName == "ViewData")
        {
            string MId = Id;
            Session["MId"] = "";
            Session["MId"] = MId;
            Session["Status"] = "View";
            Response.Redirect("MemberInformation.aspx");
        }


        
        //
        //int rowIndex = Convert.ToInt32(e.CommandArgument);


        //string Id = gv_loadGridView.DataKeys[rowIndex][0].ToString();

        //if (e.CommandName == "DeleteData")
        //{
        //    if (aMinors.deleteDetails(Id))
        //    {
        //        aMinors.DeleteMaster(Id);
        //        aShowMessage.ShowMessageBox("Data Delete Successfully",this);
        //    }
        //}

        //if (e.CommandName == "EditData")
        //{
        //    Response.Redirect("AttendeeGroupSetup.aspx?ID="+Id);
        //}


    }



  


    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            load();
            using (DataTable dt = aMinors.GetUser(Convert.ToInt32(ddlCompany.SelectedValue)))
            {
                ddlCreateBy.DataSource = dt;
                ddlCreateBy.DataValueField = "Value";
                ddlCreateBy.DataTextField = "UserName";
                ddlCreateBy.DataBind();
                ddlCreateBy.Items.Insert(0, new ListItem("Please Select From List.....", String.Empty));
            }
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {

    }



    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MemberInformationView.aspx");
    }


    private void Clear()
    {
        ddlCompany.SelectedValue = string.Empty;
        ddlCreateBy.SelectedValue = string.Empty;
        TxtCreateDate.Text = string.Empty;
        TxtToDate.Text = string.Empty;
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("MemberInformation.aspx");
    }
    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;


        Session["Status"] = "Edit";
        HiddenField hfMasterId = (HiddenField)gv_loadGridView.Rows[rowID].FindControl("hfMasterId");
        Response.Redirect("MemberInformation.aspx?MID=" + hfMasterId.Value.Trim());
    }

    protected void btnRemove_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField hfMasterId = (HiddenField)gv_loadGridView.Rows[rowID].FindControl("hfMasterId");


        DataTable dtBoardMeeting = new DataTable();
        DataTable dtSubcommittee = new DataTable();
        dtBoardMeeting = aMinors.CheckBoardMeeting(hfMasterId.Value);
        dtSubcommittee = aMinors.CheckSubcommittee(hfMasterId.Value);
        if (dtSubcommittee.Rows.Count == 0 )
        {

            if (dtBoardMeeting.Rows.Count == 0)
            {
                Session["Status"] = "Delete";

                Response.Redirect("MemberInformation.aspx?MID=" + hfMasterId.Value.Trim());
            }
            else
            {
                aShowMessage.ShowMessageBox("Can not be Deleted", this);
            }

        }
        else
        {
            aShowMessage.ShowMessageBox("Can not be Deleted",this);
        }
    }

    protected void btnView_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "View";
        HiddenField hfMasterId = (HiddenField)gv_loadGridView.Rows[rowID].FindControl("hfMasterId");
        Response.Redirect("MemberInformation.aspx?MID=" + hfMasterId.Value.Trim());
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
