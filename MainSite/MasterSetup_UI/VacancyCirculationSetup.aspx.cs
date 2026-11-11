using DAL.MasterSetup_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterSetup_UI_VacancyCirculationSetup : System.Web.UI.Page
{
    VacancyCirculationDAL aVacancyDAL = new VacancyCirculationDAL();

    // ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    // Validation aValidation = new Validation();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Session["VacancyCirculationId"] != null)
            {
                GetOneRecord(Session["VacancyCirculationId"].ToString());
                Session["VacancyCirculationId"] = null;
            }
        }
    }

    private void GetOneRecord(string vacancyCirculation)
    {
        DataTable dataTable = aVacancyDAL.GetVacancyCirculationById(vacancyCirculation);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            VacancyCirculationIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("VacancyCirculationId").ToString(CultureInfo.InvariantCulture);

           circulationWayTextBox.Text = dataTable.Rows[rowIndex].Field<string>("CirculationWay");

            if (dataTable.Rows[rowIndex].Field<bool>("IsActive"))
            {
                if (!chkActive.Checked)
                {
                    chkActive.Checked = true;
                }
            }
            else
            {
                chkActive.Checked = false;
            }


            btnSave.Text = "Update";
        }
    }

    private bool Validation()
    {
        if (circulationWayTextBox.Text == "")
        {
            ShowMessageBox("Please inser Circulation Way!!!");
            return false;
        }


        return true;
    }

    protected void ShowMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);

    }

    protected void submitButton_Click(object sender, EventArgs e)
    {

        if (Validation())
        {
            if (VacancyCirculationIdHiddenField.Value == "")
            {
                try
                {
                    Int32 VacancyCirculationId = SaveVacancy();

                    if (VacancyCirculationId > 0)
                    {
                        aShowMessage.ShowMessageBox(aMessages.SaveSuccessMessage, this);
                        Clear();
                    }
                }
                catch (Exception ex)
                {
                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                    throw;
                }
            }

            if (VacancyCirculationIdHiddenField.Value != "")
            {
                try
                {
                    bool circulation = UpdateVacancyCirculationSetup();

                    if (circulation)
                    {
                        aShowMessage.ShowMessageBox(aMessages.UpdateSuccessMessage, this);
                        Clear();
                    }
                }
                catch (Exception ex)
                {
                    //aShowMessage.ShowMessageBox(aMessages.UpdateFailedMessage, this);
                    throw;
                }
            }
        }
    }



    private bool UpdateVacancyCirculationSetup()
    {
        bool retVal;
        try
        {
            retVal = aVacancyDAL.UpdateVacancyCirculationSetup(PrepareDataForUpdate());

        }
        catch (Exception ex)
        {
            retVal = false;
        }

        return retVal;
    }

    private VacancyCirculationDAO PrepareDataForUpdate()
    {
        var  aVacancyDAO = new VacancyCirculationDAO();

        aVacancyDAO.VacancyCirculationId = Convert.ToInt32(VacancyCirculationIdHiddenField.Value);
        aVacancyDAO.CirculationWay =circulationWayTextBox.Text.Trim();

        aVacancyDAO.IsActive = chkActive.Checked;
        aVacancyDAO.UpdateBy = Session["LoginName"].ToString();
        aVacancyDAO.UpdateDate = DateTime.Now;

        return aVacancyDAO;
    }

    private Int32 SaveVacancy()
    {
        Int32 retVal;
        try
        {
            retVal = aVacancyDAL.SaveVacancyCirculation(PrepareDataForSave());

        }
        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }

        return retVal;
    }


    private VacancyCirculationDAO PrepareDataForSave()
    {
        var aVacancyDAO = new VacancyCirculationDAO();

        aVacancyDAO.CirculationWay = circulationWayTextBox.Text.Trim();

        aVacancyDAO.IsActive = chkActive.Checked;
        aVacancyDAO.EntryBy = Session["LoginName"].ToString();
        aVacancyDAO.EntryDate = DateTime.Now;

        return aVacancyDAO;
    }



    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
       circulationWayTextBox.Text = "";

        chkActive.Checked = false;
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("VacancyCirculationViewer.aspx");
    }
}