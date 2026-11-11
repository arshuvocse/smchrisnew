using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.ExecutiveOfficeDocDal;
using DAL.MeetingMinorsDAL;
using DAL.Permission_DAL;
using HELPER_FUNCTIONS.HELPERS;

public partial class HealthCare_UI_ExecutiveOfficeDocumentUpload : System.Web.UI.Page
{
    // MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();

    ExecutiveOfficeDocUpDal AMAsterDal = new ExecutiveOfficeDocUpDal();

    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDownList();
            LoadGrid();

            // UserPersmissionValidation();
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



    public void UserPersmissionValidation()
    {
        try
        {
            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            DataTable dtuserpermission = aPermissionDal.GetPermissionForUser(Session["UserId"].ToString(), filepath);
            if (dtuserpermission.Rows.Count > 0)
            {
                if (dtuserpermission.Rows[0]["UserTypeId"].ToString() != "3" ||
                    dtuserpermission.Rows[0]["UserTypeId"].ToString() != "4")
                {


                    ViewState["Add"] = dtuserpermission.Rows[0]["Add"].ToString();
                    ViewState["Edit"] = dtuserpermission.Rows[0]["Edit"].ToString();
                    ViewState["View"] = dtuserpermission.Rows[0]["View"].ToString();
                    ViewState["Delete"] = dtuserpermission.Rows[0]["Delete"].ToString();

                    //  addNewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    //loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
                    //    Convert.ToBoolean(ViewState["View"].ToString());

                    //loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
                    //    Convert.ToBoolean(ViewState["Delete"].ToString());
                    //loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
                    //    Convert.ToBoolean(ViewState["Edit"].ToString());
                }
                else
                {

                }
            }
            else
            {
                // Response.Redirect("../DashBoard_UI/DashBoard.aspx");
            }
        }
        catch (Exception ex)
        {

            aShowMessage.ShowMessageBox(ex.ToString(), this);
        }
    }





    private void LoadDropDownList()
    {
        //  AMAsterDal.GetCompanyListIntoDropdown(ddlCompany);
        //   ddlCompany.SelectedIndex = 1;
        // ddlCompany_OnSelectedIndexChanged(null, null);
        AMAsterDal.GetCompanyListIntoDropdown(ddlCompany);
        ddlCompany.SelectedIndex = 1;
        AMAsterDal.GetExeOfficeCategoryListIntoDropdown(ddlcategory);
        AMAsterDal.GetExeOfficeCategoryListIntoDropdown(ddlPreCat);
        AMAsterDal.GetExeOfficeCategoryListIntoDropdown(ddlNewCat);
    }
    protected void ddlcategory_OnTextChanged(object sender, EventArgs e)
    {
        Session["Cat"] = "";
        Session["SubCat"] = "";
        if (ddlcategory.SelectedValue != "")
        {

            Session["Cat"] = ddlcategory.SelectedValue;
            try
            {
                using (DataTable dtt = AMAsterDal.GetSubCategory(ddlcategory.SelectedValue.ToString()))
                {
                    if (dtt.Rows.Count > 0)
                    {
                        ddlSubCategory.DataSource = dtt;
                        ddlSubCategory.DataValueField = "ExeOfficeDocSubCatId";
                        ddlSubCategory.DataTextField = "ExeOfficeDocSubCate";
                        ddlSubCategory.DataBind();
                        ddlSubCategory.Items.Insert(0, new ListItem("Select any one", String.Empty));
                    }
                    else
                    {
                        ddlSubCategory.Items.Clear();
                    }

                }
            }
            catch (Exception)
            {

            }
        }
        else
        {

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
    protected void AddNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("DocUpload.aspx");
    }

    protected void btn_Search_OnClick(object sender, EventArgs e)
    {
        try
        {

            if (ddlCompany.SelectedValue != "")
            {
                LoadGrid();
            }
            else
            {
                gv_ViewList.DataSource = null;
                gv_ViewList.DataBind();
                aShowMessage.ShowMessageBox("Please Select Company", this);
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }



    ShowMessage aShowMessage = new ShowMessage();

    //private string GenerateParamiterList()
    //{


    //    string parameter = " ";

    //    if (ddlCompany.SelectedValue != "")
    //    {
    //        parameter = parameter + " AND mas.CompanyId = " + ddlCompany.SelectedValue;
    //    }

    //    if (txtTitle.Text != "")
    //    {
    //        parameter = parameter + "  and  mas.Title LIKE '%''" + txtTitle.Text.Trim() + "''%'   ";
    //    }
    //    if (txtPropuse.Text != "")
    //    {
    //        parameter = parameter + "  and  mas.Purpose LIKE '%''" + txtPropuse.Text.Trim() + "''%'   ";
    //    }
    //    if (ddlCreatedBy.Text != "")
    //    {
    //        parameter = parameter + "  and mas.CreateBy=" + ddlCreatedBy.SelectedValue.Trim() + "  ";
    //    }
    //    if (txtCreatedDate.Text != string.Empty && txtToDate.Text != string.Empty)
    //    {
    //        parameter = parameter + " AND mas.CreateDate BETWEEN '" + txtCreatedDate.Text + "' AND '" + txtToDate.Text + "' ";
    //    }
    //    if (txtCreatedDate.Text != string.Empty && txtToDate.Text == string.Empty)
    //    {
    //        parameter = parameter + " AND mas.CreateDate BETWEEN '" + txtCreatedDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
    //    }

    //    if (txtCreatedDate.Text == string.Empty && txtToDate.Text != string.Empty)
    //    {
    //        parameter = parameter + " AND mas.CreateDate BETWEEN '" + txtToDate.Text + "' AND '" + txtToDate.Text + "' ";
    //    }

    //    if (txtKeySearch.Text != "")
    //    {
    //        parameter = parameter + "  and  mas.KeySearch LIKE '%" + txtKeySearch.Text.Trim() + "%'   ";
    //    }

    //    return parameter;
    //}

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }


    private string param()
    {
        string param = "";

        if (Session["UserId"] != null)
        {
            param = param + " And mas.CreateBy= " + Session["UserId"].ToString();
        }


        return param;
    }

    private string GenerateParamiterList()
    {


        string parameter = " ";



        //if (Session["UserTypeId"].ToString() == "3" ||
        //    Session["UserTypeId"].ToString() == "4")
        //{


        //}
        //else
        //{
        //    parameter = parameter + " And mas.CreateBy= " + Session["UserId"].ToString();
        //}

        try
        {
            if (Session["Co"] != null)
            {
                ddlCompany.SelectedValue = Session["Co"].ToString();
            }
        }
        catch (Exception)
        {

            //throw;
        }

        if (ddlCompany.SelectedValue != "")
        {
            Session["Co"] = ddlCompany.SelectedValue;
            parameter = parameter + " AND mas.CompanyId = " + ddlCompany.SelectedValue;
        }



        try
        {
            if (Session["Cat"] != null)
            {
                ddlcategory.SelectedValue = Session["Cat"].ToString();


            }
        }
        catch (Exception)
        {

            //throw;
        }

        if (ddlcategory.SelectedValue != "")
        {
            Session["Cat"] = ddlcategory.SelectedValue;

            try
            {
                using (DataTable dtt = AMAsterDal.GetSubCategory(ddlcategory.SelectedValue.ToString()))
                {
                    if (dtt.Rows.Count > 0)
                    {
                        ddlSubCategory.DataSource = dtt;
                        ddlSubCategory.DataValueField = "ExeOfficeDocSubCatId";
                        ddlSubCategory.DataTextField = "ExeOfficeDocSubCate";
                        ddlSubCategory.DataBind();
                        ddlSubCategory.Items.Insert(0, new ListItem("Select any one", String.Empty));
                    }
                    else
                    {
                        ddlSubCategory.Items.Clear();
                    }

                }
            }
            catch (Exception)
            {

            }

            parameter = parameter + " AND mas.ExeOfficeDocCatId = " + ddlcategory.SelectedValue;
        }

        else
        {
            Session["Cat"] = "";
        }



        try
        {
            if (Session["SubCat"] != null)
            {
                ddlSubCategory.SelectedValue = Session["SubCat"].ToString();
            }
        }
        catch (Exception)
        {

            //throw;
        }


        if (ddlSubCategory.SelectedValue != "")
        {
            Session["SubCat"] = ddlSubCategory.SelectedValue;

            parameter = parameter + " AND mas.ExeOfficeDocSubCatId = " + ddlSubCategory.SelectedValue;
        }
        else
        {
            Session["SubCat"] = "";
        }



        try
        {
            if (Session["frmDate"] != null)
            {
                EfectiveDate.Text = Session["frmDate"].ToString();
            }
        }
        catch (Exception)
        {

            //throw;
        }


        try
        {
            if (Session["toDate"] != null)
            {
                txtToDate.Text = Session["toDate"].ToString();
            }
        }
        catch (Exception)
        {

            //throw;
        }

        if (EfectiveDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            Session["frmDate"] = EfectiveDate.Text;
            Session["toDate"] = txtToDate.Text;
            parameter = parameter + " AND mas.DocumentEntryDate BETWEEN '" + EfectiveDate.Text + "' AND '" + txtToDate.Text + "' ";
        }
        else
        {
            Session["frmDate"] = "";
            Session["toDate"] = "";
        }
        if (EfectiveDate.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND mas.DocumentEntryDate BETWEEN '" + EfectiveDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (EfectiveDate.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.DocumentEntryDate BETWEEN '" + txtToDate.Text + "' AND '" + txtToDate.Text + "' ";
        }


        return parameter;
    }
    private void LoadGrid()
    {


        DataTable aDataTable = new DataTable();


        aDataTable = AMAsterDal.LoadInfo(GenerateParamiterList());



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





    protected void lbReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ExecutiveOfficeDocUploadView.aspx");
    }


    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlCompany.SelectedValue != "")
        //{
        //    Session["CompanyId"] = "";
        //    Session["CompanyId"] = ddlCompany.SelectedValue;
        //    AMAsterDal.GetUserListDropdown(ddlCreatedBy, ddlCompany.SelectedValue);
        //    if (Session["UserTypeId"].ToString() == "3" ||
        //        Session["UserTypeId"].ToString() == "4")
        //    {
        //        ddlCreatedBy.Enabled = true;

        //    }
        //    else
        //    {
        //        ddlCreatedBy.SelectedValue = Session["UserId"].ToString();
        //        ddlCreatedBy.Enabled = false;


        //    }


        //}
        //else
        //{
        //    ddlCreatedBy.Items.Clear();
        //}
    }

    protected void btnView_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        Session["Status"] = "";
        Session["Status"] = "View";
        HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("HfMasterId");
        Response.Redirect("DocUpload.aspx?MID=" + mastrId.Value.Trim());
    }




    protected void btnEdit_OnClick(object sender, EventArgs e)
    {

        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField hfActionStatus = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfActionStatus");
        HiddenField hfisEditBtn = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfisEditBtn");

        Session["Status"] = "Edit";
        HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("HfMasterId");
        Response.Redirect("DocUpload.aspx?MID=" + mastrId.Value.Trim());


        //if (hfisEditBtn.Value == "True")
        //{

        //    Session["Status"] = "Edit";
        //    HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("HfMasterId");
        //    Response.Redirect("DocUpload.aspx?MID=" + mastrId.Value.Trim());
        //}
        //else
        //{
        //    aShowMessage.ShowMessageBox("Can not be edited or deleted !!!", this);

        //}

    }

    protected void btnRemove_OnClick(object sender, EventArgs e)
    {



        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField HfMasterId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("HfMasterId");


        bool status = AMAsterDal.UpdateJobReqStatus2(HfMasterId.Value);

        if (status)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successfully Done......');window.location ='ExecutiveOfficeDocUploadView.aspx';",
                true);
        }
        else
        {
            showMessageBox("Operation Faild!");
        }

    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    protected void txtKeySearch_OnTextChanged(object sender, EventArgs e)
    {
        // txtKeySearch.Text = txtKeySearch.Text.Trim();
    }

    protected void EfectiveDate_OnTextChanged(object sender, EventArgs e)
    {
        if (EfectiveDate.Text != string.Empty)
        {
            Session["frmDate"] = EfectiveDate.Text;


        }
        else
        {
            Session["frmDate"] = "";

        }
    }

    protected void txtToDate_OnTextChanged(object sender, EventArgs e)
    {
        if (txtToDate.Text != string.Empty)
        {

            Session["toDate"] = txtToDate.Text;

        }
        else
        {

            Session["toDate"] = "";
        }

    }

    protected void ddlSubCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Session["SubCat"] = "";
        if (ddlSubCategory.SelectedValue != "")
        {
            Session["SubCat"] = ddlSubCategory.SelectedValue;
        }
    }

    protected void ddlPreCat_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            using (DataTable dtt = AMAsterDal.GetSubCategory(ddlPreCat.SelectedValue.ToString()))
            {
                if (dtt.Rows.Count > 0)
                {
                    ddlPreSubCat.DataSource = dtt;
                    ddlPreSubCat.DataValueField = "ExeOfficeDocSubCatId";
                    ddlPreSubCat.DataTextField = "ExeOfficeDocSubCate";
                    ddlPreSubCat.DataBind();
                    ddlPreSubCat.Items.Insert(0, new ListItem("Select any one", String.Empty));
                }
                else
                {
                    ddlPreSubCat.Items.Clear();
                }

            }
        }
        catch (Exception)
        {

        }
    }

    protected void ddlNewCat_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            using (DataTable dtt = AMAsterDal.GetSubCategory(ddlNewCat.SelectedValue.ToString()))
            {
                if (dtt.Rows.Count > 0)
                {
                    ddlNewSubCat.DataSource = dtt;
                    ddlNewSubCat.DataValueField = "ExeOfficeDocSubCatId";
                    ddlNewSubCat.DataTextField = "ExeOfficeDocSubCate";
                    ddlNewSubCat.DataBind();
                    ddlNewSubCat.Items.Insert(0, new ListItem("Select any one", String.Empty));
                }
                else
                {
                    ddlNewSubCat.Items.Clear();
                }

            }
        }
        catch (Exception)
        {

        }
    }

    protected void btnTransfer_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField HfMasterId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("HfMasterId");

        id_mastetID.Value = HfMasterId.Value;


        DataTable dtMaster = AMAsterDal.GetMasterDataById(id_mastetID.Value);
        if (dtMaster.Rows.Count > 0)
        {


            ddlPreCat.SelectedValue = dtMaster.Rows[0]["ExeOfficeDocCatId"].ToString();
            ddlPreCat_OnTextChanged(null, null);


            try
            {
                ddlPreSubCat.SelectedValue = dtMaster.Rows[0]["ExeOfficeDocSubCatId"].ToString();

            }
            catch (Exception)
            {
                ddlSubCategory.SelectedValue = null;
                //throw;
            }

            MPBehavioral.Show();
        }




    }

    protected void btnBehavioralClose_OnClick(object sender, EventArgs e)
    {
        MPBehavioral.Hide();

    }

    protected void submitButton_OnClick(object sender, EventArgs e)
    {

        if (ddlNewCat.SelectedValue != "")
        {
            int? ExeOfficeDocSubCatId = null;
            try
            {
                ExeOfficeDocSubCatId = Convert.ToInt32(ddlNewSubCat.SelectedValue);
            }
            catch (Exception)
            {

            }
            bool status = AMAsterDal.UpdateCatSubCat(id_mastetID.Value, ddlNewCat.SelectedValue, ExeOfficeDocSubCatId);

            if (status)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfully Done......');window.location ='ExecutiveOfficeDocUploadView.aspx';",
                    true);
            }
            else
            {
                showMessageBox("Operation Faild!");
            }

        }

        else
        {
            showMessageBox("Select Transfer Category!");
        }
    }
}