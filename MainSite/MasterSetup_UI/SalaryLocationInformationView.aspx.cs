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

public partial class MasterSetup_UI_SalaryLocationInformationView : System.Web.UI.Page
{
    SalaryLocationInformationDal aInformationDal = new SalaryLocationInformationDal();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserPersmissionValidation();
            LoadSalaryLocationInformation();
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
    private void LoadSalaryLocationInformation()
    {
        DataTable dataTable = aInformationDal.GetSalaryLocationInformation();

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

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var dataKey = loadGridView.DataKeys[0];
            if (dataKey != null)
            {
                string locationId = e.CommandArgument.ToString();
                Session["Status"] = "Edit";
                Session["LocationId"] = "";
                Session["LocationId"] = locationId;
            }

            Response.Redirect("SalaryLocationInformation.aspx");
        }

        if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string divisionId = e.CommandArgument.ToString();

            Session["LocationId"] = "";
            Session["LocationId"] = divisionId;
            Session["Status"] = "View";
            Response.Redirect("SalaryLocationInformation.aspx");
        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string divisionId = e.CommandArgument.ToString();

            Session["LocationId"] = "";
            Session["LocationId"] = divisionId;
            Session["Status"] = "Delete";
            Response.Redirect("SalaryLocationInformation.aspx");
        }

        //if (e.CommandName == "DeleteData")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);
        //    var dataKey = loadGridView.DataKeys[rowindex];
        //    if (dataKey != null)
        //    {
        //        var locationId = dataKey[0].ToString();

        //        //if (!CheckRegionAllocateOrNot(regionId))
        //        //{
        //        if (aInformationDal.DeleteSalaryLocationInfoById(locationId))
        //            {
        //                LoadSalaryLocationInformation();
        //                aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
        //            }
        //        //}
        //        //else
        //        //{
        //        //    aShowMessage.ShowMessageBox(aMessages.SWingDelete, this);
        //        //    LoadSalaryLocationInformation();
        //        //}
        //    }
        //}
    }

    private bool CheckRegionAllocateOrNot(string regionId)
    {
        bool status = false;

        DataTable dataTable = aInformationDal.RegionAllocatedOrNot(regionId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("SalaryLocationInformation.aspx");
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        //const int manuId = 24;
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
        //    loadGridView.Columns[10].Visible = true;
        //}

        //if (delete)
        //{
        //    loadGridView.Columns[11].Visible = true;
        //}
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void loadGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        loadGridView.PageIndex = e.NewPageIndex;
        LoadSalaryLocationInformation();
    }
}