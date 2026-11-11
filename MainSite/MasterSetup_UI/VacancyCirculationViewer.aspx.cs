using DAL.MasterSetup_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterSetup_UI_VacancyCirculationViewer : System.Web.UI.Page
{
    VacancyCirculationDAL aVacancyDAL = new VacancyCirculationDAL();

    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadVacancyCirculationSetup();
        }
    }

    private void LoadVacancyCirculationSetup()
    {
        DataTable dataTable = aVacancyDAL.GetVacancyCirculationSetup();

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
                string vacancyCirculationId = dataKey[0].ToString();

                Session["VacancyCirculationId"] = "";
                Session["VacancyCirculationId"] = vacancyCirculationId;
            }

            Response.Redirect("VacancyCirculationSetup.aspx");
        }

        if (e.CommandName == "DeleteData")
        {
            int rowindex = Convert.ToInt32(e.CommandArgument);
            var dataKey = loadGridView.DataKeys[rowindex];

            if (dataKey != null)
            {
                var vacancyCirculationId = dataKey[0].ToString();

                
                if (aVacancyDAL.DeleteVacancyCirculationById(vacancyCirculationId))
                {
                    aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
                    LoadVacancyCirculationSetup();
                }
                
            }
        }
    }

    private bool CheckVacancyAllocateOrNot(string vacancyCirculationId)
    {
        bool status = false;

        return status;
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {

        //const int manuId = 32;
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
        //    loadGridView.Columns[15].Visible = true;
        //}

        //if (delete)
        //{
        //    loadGridView.Columns[16].Visible = true;
        //}
    }
    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("VacancyCirculationSetup.aspx");
    }

}