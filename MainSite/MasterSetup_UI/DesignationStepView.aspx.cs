using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MasterSetup_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MasterSetup_UI_DesignationStepView : System.Web.UI.Page
{
    DesignationStepInformationDal aDesignationStepInformationDal = new DesignationStepInformationDal();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if (Session["UserId"] != null)
            {
                const int manuId = 15;
                aOperationCredentials.MnageUserOperation("ADD", Session["UserId"].ToString(), manuId, this);
            }
            LoadDesignationStepInfo();
        }

    }

    private void LoadDesignationStepInfo()
    {
        var designationStep = new DataTable();

        designationStep = aDesignationStepInformationDal.GetDesignationStepInformation();

        loadGridView.DataSource = designationStep;
        loadGridView.DataBind();
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);

            var dataKey = loadGridView.DataKeys[rowindex];
            if (dataKey != null)
            {
                string designationStepId = dataKey[0].ToString();

                Session["designationStepId"] = "";
                Session["designationStepId"] = designationStepId;
            }

            Response.Redirect("DesignationStepInformation.aspx");
        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            string designationStepId = loadGridView.DataKeys[rowindex][0].ToString();

            if (aDesignationStepInformationDal.DeleteDesgInfoById(designationStepId))
            {
                aShowMessage.ShowMessageBox(aMessages.DeleteMessage,this);
                LoadDesignationStepInfo();
            }
        }
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
        const int manuId = 15;
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
            loadGridView.Columns[9].Visible = true;
        }

        if (delete)
        {
            loadGridView.Columns[10].Visible = true;
        }
    }

}