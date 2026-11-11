using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MasterSetup_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MasterSetup_UI_PlaceInformationView : System.Web.UI.Page
{
    AreaInformationDal aInformationDal = new AreaInformationDal();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadRegionInformation();
        }
    }

    private void LoadRegionInformation()
    {
        DataTable dataTable = aInformationDal.GetAreaInformation();

        if (dataTable.Rows.Count > 0)
        {
            loadGridView.DataSource = dataTable;
            loadGridView.DataBind();
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
                string areaId = dataKey[0].ToString();

                Session["AreaId"] = "";
                Session["AreaId"] = areaId;
            }

            Response.Redirect("AreaInformation.aspx");
        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var dataKey = loadGridView.DataKeys[rowindex];
            if (dataKey != null)
            {
                var areaId = dataKey[0].ToString();

                if (!CheckAreaAllocateOrNot(areaId))
                {
                    if (aInformationDal.DeleteAreaInfoById(areaId))
                    {
                        aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
                        LoadRegionInformation();
                    }
                }
                else
                {
                   
                 //   showMessageBox("Cann't delete because it contains a Region."); 
                    LoadRegionInformation();
                    aShowMessage.ShowMessageBox("Can not be deleted because this is used in Job Location.", this);
                  //aShowMessage.ShowMessageBox(aMessages.SWingDelete, this);
                    //
                }
            }
        }
    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    private bool CheckAreaAllocateOrNot(string areaId)
    {
        bool status = false;

        DataTable dataTable = aInformationDal.AreaAllocatedOrNot(areaId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AreaInformation.aspx");
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

}