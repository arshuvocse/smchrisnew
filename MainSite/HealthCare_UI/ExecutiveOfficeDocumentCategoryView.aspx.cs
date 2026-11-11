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
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class HealthCare_UI_ExecutiveOfficeDocumentCategoryView : System.Web.UI.Page
{
    
    ExeOfficeDocDal aEntryDaL = new ExeOfficeDocDal();

    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            loadGridView.UseAccessibleHeader = true;
            loadGridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            loadGridView.FooterRow.TableSection = TableRowSection.TableFooter;
            loadGridView.UseAccessibleHeader = true;
        }
        catch (Exception)
        {

            //throw;
        }
        if (!IsPostBack)
        {

            // UserPersmissionValidation();
            LoadInformation();
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




    private void LoadInformation()
    {
        DataTable dataTable = aEntryDaL.GetEntryformation();

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
            var dataKey = loadGridView.DataKeys[rowindex];
            if (dataKey != null)
            {
                string MId = dataKey[0].ToString();



                Session["Status"] = "Edit";
                Session["MId"] = "";
                Session["MId"] = MId;
                Response.Redirect("ExecutiveOfficeDocumentCategory.aspx");

                //if (MId == "1" || MId == "2")
                //{
                //    showMessageBox("Can not be edited or deleted");
                
                //}
                //else
                //{
                //    Session["Status"] = "Edit";
                //    Session["MId"] = "";
                //    Session["MId"] = MId;
                //    Response.Redirect("ExecutiveOfficeDocumentCategory.aspx");
                //}
            }

        }

        //if (e.CommandName == "ViewData")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);
        //    string MId = loadGridView.DataKeys[rowindex][0].ToString();


        //    Session["MId"] = "";
        //    Session["MId"] = MId;
        //    Session["Status"] = "View";
        //    Response.Redirect("ExecutiveOfficeDocumentCategory.aspx");
        //}

        if (e.CommandName == "DeleteData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string MId = loadGridView.DataKeys[rowindex][0].ToString();

            DataTable dt = aEntryDaL.GetCheck(MId);
            if (dt.Rows.Count == 0)
            {
                Session["MId"] = "";
                Session["MId"] = MId;
                Session["Status"] = "Delete";
                Response.Redirect("ExecutiveOfficeDocumentCategory.aspx");
            }
            else
            {
                showMessageBox("Can not be edited or deleted");
                
            }


        }

        //if (e.CommandName == "DeleteData")
        //{
        //    int rowindex = Convert.ToInt32(e.CommandArgument);
        //    var dataKey = loadGridView.DataKeys[rowindex];
        //    if (dataKey != null)
        //    {
        //        var areaId = dataKey[0].ToString();

        //        if (!CheckAreaAllocateOrNot(areaId))
        //        {
        //            if (aInformationDal.DeleteAreaInfoById(areaId))
        //            {
        //                aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
        //                LoadRegionInformation();
        //            }
        //        }
        //        else
        //        {

        //         //   showMessageBox("Cann't delete because it contains a Region."); 
        //            LoadRegionInformation();
        //            aShowMessage.ShowMessageBox("Can not be deleted because this is used in Job Location.", this);
        //          //aShowMessage.ShowMessageBox(aMessages.SWingDelete, this);
        //            //
        //        }
        //    }
        //}
    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }


    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("ExecutiveOfficeDocumentCategory.aspx");

    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        const int manuId = 12;
        DataTable gridPermission = aOperationCredentials.MnageUserOperationOnGridView(Session["UserId"].ToString(), manuId);
        const int rowIndex = 0;

        bool edit = false;
        bool delete = false;

        if (gridPermission.Rows.Count > 0)
        {
            edit = gridPermission.Rows[rowIndex].Field<bool>("Edit");
            delete = gridPermission.Rows[rowIndex].Field<bool>("Delete");
        }

        if (edit)
        {
            loadGridView.Columns[12].Visible = true;
        }

        if (delete)
        {
            loadGridView.Columns[13].Visible = true;
        }
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}