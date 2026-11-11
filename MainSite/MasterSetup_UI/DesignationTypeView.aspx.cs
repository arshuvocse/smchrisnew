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

public partial class MasterSetup_UI_DesignationTypeView : System.Web.UI.Page
{
    DesignationTypeDAL aInformationDal = new DesignationTypeDAL();
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

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
         
    }

    protected void DesigTypeNameTextBox_OnTextChanged(object sender, EventArgs e)
    {
        
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("DesignationType.aspx");
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
         
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var dataKey = loadGridView.DataKeys[rowindex];
            if (dataKey != null)
            {
                string locationId = e.CommandArgument.ToString();
                Session["Status"] = "Edit";
                Session["DesignationTypeId"] = "";
                Session["DesignationTypeId"] = locationId;
            }

            Response.Redirect("DesignationType.aspx");
        }

        if (e.CommandName == "ViewData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string divisionId = e.CommandArgument.ToString();

            Session["DesignationTypeId"] = "";
            Session["DesignationTypeId"] = divisionId;
            Session["Status"] = "View";
            Response.Redirect("DesignationType.aspx");
        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string divisionId = e.CommandArgument.ToString();

            Session["DesignationTypeId"] = "";
            Session["DesignationTypeId"] = divisionId;
            Session["Status"] = "Delete";
            Response.Redirect("DesignationType.aspx");
        }


        //if (e.CommandName == "DeleteData")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);
        //    var dataKey = loadGridView.DataKeys[rowindex];
        //    if (dataKey != null)
        //    {
        //        var locationId = dataKey[0].ToString();

        //        if (!CheckRegionAllocateOrNot(locationId))
        //        {
        //        if (aInformationDal.DeleteSalaryLocationInfoById(locationId))
        //        {
        //            LoadSalaryLocationInformation();
        //            aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
        //        }
        //        }
        //        else
        //        {
        //            aShowMessage.ShowMessageBox("Can not be deleted because this is used in Salary Grade.", this);
        //            LoadSalaryLocationInformation();
        //        }
        //    }
        //}



    }

    private bool CheckRegionAllocateOrNot(string SalaryGradeId)
    {
        bool status = false;

        DataTable dataTable = aInformationDal.SalaryGradeAllocatedOrNot(SalaryGradeId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}