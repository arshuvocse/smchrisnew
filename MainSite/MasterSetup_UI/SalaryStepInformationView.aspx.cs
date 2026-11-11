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

public partial class MasterSetup_UI_SalaryStepInformationView : System.Web.UI.Page
{
    SalaryStepInforamtionDal aInformationDal = new SalaryStepInforamtionDal();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UserPersmissionValidation();
          
            LoadSalaryStepInformation();
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

    private void LoadSalaryStepInformation()
    {
        DataTable dataTable = aInformationDal.GetSalaryStepInformation(Param());

        if (dataTable.Rows.Count > 0)
        {
            loadGridView.DataSource = dataTable;
            loadGridView.DataBind();
            this.loadGridView.ShowFooter = true;
            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            //loadGridView.UseAccessibleHeader = true;
            //loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            aShowMessage.ShowMessageBox("No Data Found!!!", this);
            
        }
    }

    private string Param()
    {
        string parameter = " ";


        if (txtSearch.Text != "")
        {
            parameter = parameter + " AND   SG.GradeCode   like '%" + txtSearch.Text.Trim() + "%' ";
        }
        return parameter;
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
          
            int rowindex =0;


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string salaryStepId = e.CommandArgument.ToString();
                Session["Status"] = "Edit";
                Session["SalaryStepId"] = "";
                Session["SalaryStepId"] = salaryStepId;

            }


            Response.Redirect("SalaryStepInformation.aspx");
        }


        if (e.CommandName == "ViewData")
        {
            int rowindex =0;
            string divisionId = e.CommandArgument.ToString();

            Session["SalaryStepId"] = "";
            Session["SalaryStepId"] = divisionId;
            Session["Status"] = "View";
            Response.Redirect("SalaryStepInformation.aspx");
        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = 0;
            string divisionId = e.CommandArgument.ToString();

            DataTable dataTable = aInformationDal.CheckStepExistOrNotDelete(divisionId);

            if (dataTable.Rows.Count > 0)
            {
                aShowMessage.ShowMessageBox("Can not be deleted", this);


            }
            else
            {

                Session["SalaryStepId"] = "";
                Session["SalaryStepId"] = divisionId;
                Session["Status"] = "Delete";
                Response.Redirect("SalaryStepInformation.aspx");
            }
        }


        //if (e.CommandName == "DeleteData")
        //{
        //    int rowindex = 0;
        //    var dataKey = loadGridView.DataKeys[rowindex];
        //    if (dataKey != null)
        //    {
        //        var stepId = e.CommandArgument.ToString();

        //        DataTable dataTable = aInformationDal.CheckStepExistOrNotDelete(stepId);

        //                if (dataTable.Rows.Count>0)
        //                {
        //                    aShowMessage.ShowMessageBox("Cann't be delete", this);
              
             
        //        }
        //        else
        //        {
        //            if (aInformationDal.DeleteSalaryStepInfoById(stepId))
        //            {
        //                aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
        //                LoadSalaryStepInformation();
        //            }
                   
        //        }
        //    }
        //}
    }

    private bool CheckGradeAllocateOrNot(string gradeId)
    {
        bool status = false;

        DataTable dataTable = aInformationDal.GradeAllocateOrNot(gradeId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("SalaryStepInformation.aspx");
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void loadGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        loadGridView.PageIndex = e.NewPageIndex;
        LoadSalaryStepInformation();
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        LoadSalaryStepInformation();
    }
}