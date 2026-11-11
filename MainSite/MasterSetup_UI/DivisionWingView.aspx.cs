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

public partial class MasterSetup_UI_DivisionWingView : System.Web.UI.Page
{
    DivisionWingInformationDal aDivisionInformationDal = new DivisionWingInformationDal();
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
            //if (Session["UserId"] != null)
            //{
            //    const int manuId = 11;
            //    aOperationCredentials.MnageUserOperation("VIEW", Session["UserId"].ToString(), manuId, this);
            //}
           // LoadDivisionWingInformation();

            loadDropDownList();
        }

        try
        {
           
            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            loadGridView.UseAccessibleHeader = true;

        }
        catch (Exception ex)
        {


        }
    }

    private void loadDropDownList()
    {
        aDivisionInformationDal.GetComapnyNameList(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
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


    public void GetCompany()
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

    private void LoadDivisionWingInformation()
    {
        DataTable dataTable = aDivisionInformationDal.GetDivisionWingInformationParam(" And  DVW.CompanyId IN (" + CompanyId() + " ) " + GenerateParameter() + "");

        if (dataTable.Rows.Count > 0)
        {
            loadGridView.DataSource = dataTable;
            loadGridView.DataBind();
            loadGridView.ShowFooter = true;
            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            loadGridView.UseAccessibleHeader = true;
        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            aShowMessage.ShowMessageBox("No Data Found!!!", this);
        }
    }

    private string GenerateParameter()
    {
        string parameter = " ";


        if (companyDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND   DVW.CompanyId =  '" + companyDropDownList.SelectedValue + "' ";
        }



        return parameter;
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex =0;
            string wingId = loadGridView.DataKeys[rowindex][0].ToString();
            Session["Status"] = "Edit";
            Session["wingId"] = "";
            Session["wingId"] = wingId;

            Response.Redirect("DivisionWingInformation.aspx");
        }

           if (e.CommandName == "ViewData")
        {
            int rowindex =0;
            string divisionId = loadGridView.DataKeys[rowindex][0].ToString();

            Session["wingId"] = "";
            Session["wingId"] = divisionId;
            Session["Status"] = "View";
            Response.Redirect("DivisionWingInformation.aspx");
        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = 0;
            string divisionId = loadGridView.DataKeys[rowindex][0].ToString();

            Session["wingId"] = "";
            Session["wingId"] = divisionId;
            Session["Status"] = "Delete";
            Response.Redirect("DivisionWingInformation.aspx");
        }

        //if (e.CommandName == "DeleteData")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);
        //    string wingId = loadGridView.DataKeys[rowindex][0].ToString();

        //    if (!CheckWingAllocateOrNot(wingId))
        //    {
        //        if (aDivisionInformationDal.DeleteDivisionWingInfoById(wingId))
        //        {
        //            aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
        //            LoadDivisionWingInformation();
        //        }
        //    }
        //    else
        //    {
        //        aShowMessage.ShowMessageBox(aMessages.SWingDelete, this);
        //        LoadDivisionWingInformation();
        //    }
            
        //}
    }

    private bool CheckWingAllocateOrNot(string wingId)
    {
        bool status = false;

        DataTable dataTable = aDivisionInformationDal.WingAllocatedOrNot(wingId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("DivisionWingInformation.aspx");
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        //const int manuId = 11;
        //DataTable gridPermission = aOperationCredentials.MnageUserOperationOnGridView(Session["UserId"].ToString(),manuId);
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
        //    loadGridView.Columns[12].Visible = true;
        //}

        //if (delete)
        //{
        //    loadGridView.Columns[13].Visible = true;
        //}
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void GoBackButton_OnClick(object sender, EventArgs e)
    {
          
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            LoadDivisionWingInformation();
        }
        else
        {
            companyDropDownList.Focus();
            aShowMessage.ShowMessageBox("Please Select Company !!!", this);
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            
        }
    }

    protected void loadGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        loadGridView.PageIndex = e.NewPageIndex;
        LoadDivisionWingInformation();
    }
}