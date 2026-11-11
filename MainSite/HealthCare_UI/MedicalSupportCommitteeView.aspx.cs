using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.HealthCare_DAL;
using DAL.MeetingMinorsDAL;

public partial class HealthCare_UI_MedicalSupportCommitteeView : System.Web.UI.Page
{
    MedicalSupportComDal aRoutingPath = new MedicalSupportComDal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // view();
            Dropdownlist();
        }

    }


    private void Dropdownlist()
    {


        using (DataTable dt = aRoutingPath.GetDDLCompany())
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
            ddlCompany.SelectedIndex = 1;
            ddlCompany_OnSelectedIndexChanged(null, null);

        }



    }




    private void view()
    {
        DataTable Dt = aRoutingPath.GetRoutingpathView(GenerateParameter());
        if (Dt.Rows.Count > 0)
        {
            gv_loadGridView.DataSource = Dt;
            gv_loadGridView.DataBind();

            gv_loadGridView.UseAccessibleHeader = true;
            gv_loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv_loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            gv_loadGridView.UseAccessibleHeader = true;
        }
        else
        {
            ShowMessageBox("Data Not Found");
            gv_loadGridView.DataSource = null;
            gv_loadGridView.DataBind();

        }


    }


    protected void ButtonView_OnClick(object sender, EventArgs e)
    {
        view();
    }
    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
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

            Response.Redirect("~/MeetingMinors/RoutingPathSetup.aspx");
        }

        if (e.CommandName == "DeleteData")
        {
            string MId = Id;
            Session["MId"] = "";
            Session["MId"] = MId;
            Session["Status"] = "Delete";
            Response.Redirect("~/MeetingMinors/RoutingPathSetup.aspx");
        }


        if (e.CommandName == "ViewData")
        {
            string MId = Id;
            Session["MId"] = "";
            Session["MId"] = MId;
            Session["Status"] = "View";
            Response.Redirect("~/MeetingMinors/RoutingPathSetup.aspx");
        }

        //if (e.CommandName == "DeleteData")
        //{
        //    //string code = gv_loadGridView.DataKeys[rowindex][1].ToString();
        //    //string Name = gv_loadGridView.DataKeys[rowindex][2].ToString();
        //    //string company = gv_loadGridView.DataKeys[rowindex][3].ToString();
        //    //string division = gv_loadGridView.DataKeys[rowindex][4].ToString();
        //    //string Department = gv_loadGridView.DataKeys[rowindex][5].ToString();
        //    //string createby = gv_loadGridView.DataKeys[rowindex][6].ToString();
        //    //string createDate = gv_loadGridView.DataKeys[rowindex][7].ToString();
        //    //string Update = gv_loadGridView.DataKeys[rowindex][8].ToString();
        //    //string UpdateDate = gv_loadGridView.DataKeys[rowindex][9].ToString();
        //    //string EmpID = gv_loadGridView.DataKeys[rowindex][10].ToString();

        //    //RoutingPathSetupMaster aPathSetupMaster = new RoutingPathSetupMaster();
        //    //aPathSetupMaster.RoutingPathMaster_ID = Convert.ToInt32(Id);
        //    //aPathSetupMaster.RoutingPath_Code = code;
        //    //aPathSetupMaster.RoutingPath_Name = Name;
        //    //aPathSetupMaster.CompanyId = Convert.ToInt32(company);
        //    //aPathSetupMaster.DivisionId = Convert.ToInt32(division);
        //    //aPathSetupMaster.DepartmentId = Convert.ToInt32(Department);
        //    //aPathSetupMaster.CreateBy = Convert.ToInt32(createby);
        //    //aPathSetupMaster.CreateDate = Convert.ToDateTime(createDate);
        //   // aPathSetupMaster.UpdateBy = Convert.ToInt32(Update);
        //   // aPathSetupMaster.UpdateDate = Convert.ToDateTime(!string.IsNullOrEmpty(UpdateDate));

        //    string DeleteBy = Session["LoginName"].ToString();

        //  //  Int32 Del_Id = aRoutingPath.Del_SaveMaster(aPathSetupMaster, DeleteBy, EmpID);

        //    if (Del_Id >0)
        //    {
        //        if (aRoutingPath.DeleteDetails(Id))
        //        {
        //            aRoutingPath.DeleteMaster(Id); 
        //            ShowMessageBox("Data Delete Successfully");
        //            Response.Redirect("RoutingPathSetupView.aspx");
        //        }
        //    }
        //}
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {

    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("~/HealthCare_UI/MedicalSupportCommittee.aspx");
    }

    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            using (DataTable dt = aRoutingPath.GetDivisionBycompanyId(Convert.ToInt32(ddlCompany.SelectedValue)))
            {
                ddlDivision.DataSource = dt;
                ddlDivision.DataValueField = "Value";
                ddlDivision.DataTextField = "TextField";
                ddlDivision.DataBind();

            }


            using (DataTable dt = aRoutingPath.GetUser(Convert.ToInt32(ddlCompany.SelectedValue)))
            {
                ddlCreateBy.DataSource = dt;
                ddlCreateBy.DataValueField = "Value";
                ddlCreateBy.DataTextField = "UserName";
                ddlCreateBy.DataBind();
                ddlCreateBy.Items.Insert(0, new ListItem("Please Select From List.....", String.Empty));

            }


            //using (DataTable dt = aRoutingPath.GetUser(Convert.ToInt32(ddlCompany.SelectedValue)))
            //{
            //    ddlUser.DataSource = dt;
            //    ddlUser.DataValueField = "Value";
            //    ddlUser.DataTextField = "UserName";
            //    ddlUser.DataBind();
            //}
        }
    }


    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue != "")
        {
            aRoutingPath.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);

        }
    }


    private string GenerateParameter()
    {
        string param = "  ";

        if (ddlCompany.SelectedValue != "-1")
        {
            param = param + "AND MRPM.CompanyId= " + ddlCompany.SelectedValue;
        }


        if (TxtRoutingPathName.Text != "")
        {
            param = param + "  and MRPM.RoutingPath_Name LIKE '%" + TxtRoutingPathName.Text.Trim() + "%'   ";
        }
        if (ddlDivision.SelectedIndex > 0)
        {
            param = param + " AND MRPM.DivisionId = " + ddlDivision.SelectedValue;
        }

        if (ddlDepartment.SelectedIndex > 0)
        {
            param = param + " AND MRPM.DepartmentId = " + ddlDepartment.SelectedValue;
        }

        if (ddlCreateBy.SelectedValue != "")
        {
            param = param + " AND MRPM.CreateBy = " + hfEmpId.Value;
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


    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
        ddlCompany.SelectedValue = string.Empty;
        TxtRoutingPathName.Text = string.Empty;
        ddlDivision.SelectedValue = string.Empty;
        ddlDepartment.SelectedValue = string.Empty;
        ddlCreateBy.SelectedValue = string.Empty;
        TxtCreateDate.Text = string.Empty;
        TxtToDate.Text = string.Empty;
    }


    protected void txtCreateBy_OnTextChanged(object sender, EventArgs e)
    {
        //if (ddlCompany.SelectedValue != "" && ddlCompany.SelectedValue != "-1")
        //{
        //    string empName = txtCreateBy.Text.Trim();

        //    if (empName.Contains(':'.ToString()))
        //    {
        //        string[] emp = empName.Split(':');

        //        txtCreateBy.Text = emp[1];
        //        hfEmpId.Value = emp[0];

        //    }
        //}
        //else
        //{
        //    ShowMessageBox("Please Select Company!!");
        //}

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

    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;


        Session["Status"] = "Edit";
        HiddenField hfMasterId = (HiddenField)gv_loadGridView.Rows[rowID].FindControl("hfMasterId");
        Response.Redirect("MedicalSupportCommittee.aspx?MID=" + hfMasterId.Value.Trim());
    }

    protected void btnRemove_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "Delete";
        HiddenField hfMasterId = (HiddenField)gv_loadGridView.Rows[rowID].FindControl("hfMasterId");
        Response.Redirect("MedicalSupportCommittee.aspx?MID=" + hfMasterId.Value.Trim());
    }

    protected void btnView_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "View";
        HiddenField hfMasterId = (HiddenField)gv_loadGridView.Rows[rowID].FindControl("hfMasterId");
        Response.Redirect("MedicalSupportCommittee.aspx?MID=" + hfMasterId.Value.Trim());
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}