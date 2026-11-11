using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MasterSetup_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MasterSetup_UI_DesignationInformationView : System.Web.UI.Page
{
    DesignationInformationDal aInformationDal = new DesignationInformationDal();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();
            UserPersmissionValidation();
            LoadDesignationInfo();
        }

    }

    public void UserPersmissionValidation()
    {
        try
        {


            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            string text = Path.GetExtension(Request.Path);
            if (text == string.Empty)
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
            }
            else
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            }
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

                    addNewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
                        Convert.ToBoolean(ViewState["Edit"].ToString());
                }
            }
            else
            {
                Response.Redirect("../DashBoard_UI/DashBoard.aspx");
            }
        }
        catch (Exception ex)
        {

            aShowMessage.ShowMessageBox(ex.ToString(), this);
        }
    }
    public void GetCompany()
    {
        try
        {
            DataTable dtcomp = aPermissionDal.GetCompany();
            lchk_Company.DataValueField = "CompanyId";
            lchk_Company.DataTextField = "ShortName";
            lchk_Company.DataSource = dtcomp;
            lchk_Company.DataBind();

            DataTable userdata = aPermissionDal.GetUserCompany(Session["UserId"].ToString());
            for (int i = 0; i < userdata.Rows.Count; i++)
            {
                for (int j = 0; j < lchk_Company.Items.Count; j++)
                {
                    if (lchk_Company.Items[j].Text.Trim() == userdata.Rows[i]["ShortName"].ToString())
                    {
                        lchk_Company.Items[j].Selected = true;


                    }
                }
            }
        }
        catch (Exception)
        {

            Response.Redirect("/Default.aspx");
        }
    }
    private void LoadDesignationInfo()
    {
          DataTable designation= new DataTable();
        try
        {
            //  designation = aInformationDal.GetDesignationInformation(Param() + " and DSG.CompanyId IN (" + CompanyId() + ")");
              designation = aInformationDal.GetDesignationInformation(Param());
        }
        catch (Exception)
        {
              designation = aInformationDal.GetDesignationInformation(Param());
            
        }

        try
        {
            loadGridView.DataSource = designation;
            loadGridView.DataBind();
            this.loadGridView.ShowFooter = true;
            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
        }
        catch (Exception)
        {
            
            //throw;
        }
    }

    private string Param()
    {
        string parameter = " ";


        if (txtSearch.Text != "")
        {
            parameter = parameter + " AND   DSG.Designation   like '%" + txtSearch.Text.Trim() + "%' ";
        }
        return parameter;
    }
    public string CompanyId()
    {
        string companyid = "";
        for (int i = 0; i < lchk_Company.Items.Count; i++)
        {
            if (lchk_Company.Items[i].Selected)
            {
                companyid = companyid + "'" + lchk_Company.Items[i].Value + "'" + ",";
            }
        }
        companyid = companyid.TrimEnd(',');
        return companyid;
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex =0;

            var dataKey = loadGridView.DataKeys[rowindex];
            if (dataKey != null)
            {
                string designationId = e.CommandArgument.ToString();
                Session["Status"] = "Edit";
                Session["designationId"] = "";
                Session["designationId"] = designationId;
            }

            Response.Redirect("DesignationInformation.aspx");
        }


        if (e.CommandName == "ViewData")
        {
            int rowindex = 0;
            string divisionId = e.CommandArgument.ToString();

            Session["designationId"] = "";
            Session["designationId"] = divisionId;
            Session["Status"] = "View";
            Response.Redirect("DesignationInformation.aspx");
        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = 0;
            string divisionId = e.CommandArgument.ToString();

            Session["designationId"] = "";
            Session["designationId"] = divisionId;
            Session["Status"] = "Delete";
            Response.Redirect("DesignationInformation.aspx");
        }

        //if (e.CommandName == "DeleteData")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);
        //    var dataKey = loadGridView.DataKeys[rowindex];
        //    if (dataKey != null)
        //    {
        //        string designationId = dataKey[0].ToString();

        //        if (aInformationDal.DeleteDesgInfoById(designationId))
        //        {
        //            aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
        //            LoadDesignationInfo();
        //        }
        //    }
        //}
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        //const int manuId = 16;
        //DataTable gridPermission = aOperationCredentials.MnageUserOperationOnGridView(Session["UserId"].ToString(), manuId);
        //const int rowIndex = 0;

        //bool edit = false;
        //bool delete = false;

        //if (gridPermission.Rows.Count > 0)
        //{
        //    edit = gridPermission.Rows[rowIndex].Field<bool>("Edit");
        //    delete = gridPermission.Rows[rowIndex].Field<bool>("Delete");
        //}

        //if (edit)
        //{
        //    loadGridView.Columns[14].Visible = true;
        //}

        //if (delete)
        //{
        //    loadGridView.Columns[15].Visible = true;
        //}
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("DesignationInformation.aspx");
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void loadGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        loadGridView.PageIndex = e.NewPageIndex;
        LoadDesignationInfo();
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        LoadDesignationInfo();
    }
}