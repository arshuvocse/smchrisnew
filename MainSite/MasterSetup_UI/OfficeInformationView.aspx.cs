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

public partial class MasterSetup_UI_OfficeInformationView : System.Web.UI.Page
{
    OfficeInformationDal aInformationDal = new OfficeInformationDal();
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
        DataTable dataTable = aInformationDal.GetRegionInformation();

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
                string regionId = dataKey[0].ToString();

                Session["OfficeId"] = "";
                Session["OfficeId"] = regionId;
            }

            Response.Redirect("OfficeInformation.aspx");
        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var dataKey = loadGridView.DataKeys[rowindex];
            if (dataKey != null)
            {
                var regionId = dataKey[0].ToString();

              //  if (!CheckRegionAllocateOrNot(regionId))
                {
                    if (aInformationDal.DeleteRegionInfoById(regionId))
                    {
                        aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
                        LoadRegionInformation();
                    }
                }
                //else
                //{
                //    aShowMessage.ShowMessageBox("Can not be deleted because this is used in Area.", this);
                //    LoadRegionInformation();
                }
            }
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
        Response.Redirect("OfficeInformation.aspx");
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
        //    loadGridView.Columns[11].Visible = true;
        //}

        //if (delete)
        //{
        //    loadGridView.Columns[12].Visible = true;
        //}
    }
}