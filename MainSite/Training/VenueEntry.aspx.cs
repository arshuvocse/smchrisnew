using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MasterSetup_DAL;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Training_VenueEntry : System.Web.UI.Page
{
    VenueDAL aVenueDAL = new VenueDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonVisible();
            LoadDropDownList();
            SetCheckBox();

            if (Session["SMCVenueID"] != null)
            {
                GetOneRecord(Session["SMCVenueID"].ToString());
                Session["SMCVenueID"] = null;
            }
        }
    }
    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {


            if (Session["Status"].ToString() == "Add")
            {
                Button1.Visible = true;
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                editButton.Visible = true;
            }
            else if (Session["Status"].ToString() == "Delete")
            {
                delButton.Visible = true;
            }
            Session["Status"] = null;
        }

    }

    private void LoadDropDownList()
    {
   //     aVenueDAL.LoadRegionList(regionDropDownList);
    }
    private void GetOneRecord(string areaId)
    {
        DataTable dataTable = aVenueDAL.GetRegionInformationById(areaId);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            areaHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("SMCVenueID").ToString(CultureInfo.InvariantCulture);

            venueNameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("VenueName");
            adressTexBox.Text = dataTable.Rows[rowIndex].Field<string>("Adress");

            if (dataTable.Rows[rowIndex].Field<bool>("IsActive"))
            {
                if (!isActiveCheckBox.Checked)
                {
                    isActiveCheckBox.Checked = true;
                }
            }
            else
            {
                isActiveCheckBox.Checked = false;
            }


            Button1.Text = "Update";
        }
    }
    private void SetCheckBox()
    {
        if (!isActiveCheckBox.Checked)
        {
            isActiveCheckBox.Checked = true;
        }
    }
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    private bool Validation()
    {
        if (venueNameTextBox.Text == "")
        {
            showMessageBox("Venue name can not be empty!!!");
            return false;
        }
        return true;
    }
    protected void submitButton_Click(object sender, EventArgs e)
    {
       
    }
    private bool UpdateAreaInformation(Venue prepareDataForUpdate)
    {
        bool retVal;
        try
        {
            retVal = aVenueDAL.UpdateAreaInfo(PrepareDataForUpdate());
        }
        catch (Exception)
        {
            retVal = false;
        }

        return retVal;
    }
    private Venue PrepareDataForUpdate()
    {
        var aInformationDao = new Venue();

        aInformationDao.SMCVenueID = Convert.ToInt32(areaHiddenField.Value);
        aInformationDao.VenueName = venueNameTextBox.Text.Trim();
        aInformationDao.Adress = adressTexBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.Updateby = Session["UserId"].ToString();
        aInformationDao.Upatedate = DateTime.Now;

        return aInformationDao;
    }
    private Int32 SaveAreaInformation()
    {
        Int32 retVal;
        try
        {
            retVal = aVenueDAL.SaveAreaInfo(PrepareDataForSave());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }
    private Venue PrepareDataForSave()
    {
        var aInformationDao = new Venue();

        //aInformationDao.SMCVenueID = Convert.ToInt32(areaHiddenField.Value);
        aInformationDao.VenueName = venueNameTextBox.Text.Trim();
        aInformationDao.Adress = adressTexBox.Text.Trim();
        aInformationDao.IsActive = isActiveCheckBox.Checked;
        aInformationDao.EntryBy = Session["UserId"].ToString();
        aInformationDao.EntryDate = DateTime.Now;

        return aInformationDao;
    }
    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
        areaHiddenField.Value = "";
        areaCodeHiddenField.Value = "";
        venueNameTextBox.Text = "";
        adressTexBox.Text = "";
        remarksTextBox.Text = "";
        Button1.Text = "Submit";
    }
    protected void areaCodeTextBox_OnTextChanged(object sender, EventArgs e)
    {
        //if (areaCodeHiddenField.Value != "")
        //{
        //    if (areaCodeTextBox.Text != areaCodeHiddenField.Value)
        //    {
        //        if (CheckAreaCodeExistOrNot(areaCodeTextBox.Text))
        //        {
        //            aShowMessage.ShowMessageBox(aMessages.CodeExistMessage, this);
        //            areaCodeTextBox.Text = areaCodeHiddenField.Value;
        //        }
        //    }
        //}
        //else
        //{
        //    if (CheckAreaCodeExistOrNot(areaCodeTextBox.Text))
        //    {
        //        aShowMessage.ShowMessageBox(aMessages.CodeExistMessage, this);
        //        areaCodeTextBox.Text = "";
        //    }
        //}
    }
    private bool CheckAreaCodeExistOrNot(string areaCode)
    {
        bool status = false;

        DataTable dataTable = aVenueDAL.CheckAreaCodeExistOrNot(areaCode);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("VenueList.aspx");
    }
   // protected void Button1_OnClick(object sender, EventArgs e)
   // {
      //  Response.Redirect("AreaInformationView.aspx");
   // }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (areaHiddenField.Value == "")
            {
                try
                {
                    Int32 areaId = SaveAreaInformation();

                    if (areaId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfull Done...');window.location ='VenueList.aspx';",
                    true);
                        Clear();
                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                }
            }

            if (areaHiddenField.Value != "")
            {
                try
                {
                    bool area = UpdateAreaInformation(PrepareDataForUpdate());

                    if (area)
                    {
                        aShowMessage.ShowMessageBox(aMessages.UpdateSuccessMessage, this);
                        Clear();
                    }
                }
                catch (Exception ex)
                {
                    aShowMessage.ShowMessageBox(aMessages.UpdateFailedMessage, this);
                    throw;
                }
            }
        }
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            

            if (areaHiddenField.Value != "")
            {
                try
                {
                    bool area = UpdateAreaInformation(PrepareDataForUpdate());

                    if (area)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfull Done...');window.location ='VenueList.aspx';",
                    true);
                        Clear();
                    }
                }
                catch (Exception ex)
                {
                    aShowMessage.ShowMessageBox(aMessages.UpdateFailedMessage, this);
                    throw;
                }
            }
        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        if (areaHiddenField.Value != string.Empty)
        {
            TrainingTypeDAO aTypeDao = new TrainingTypeDAO();
            aTypeDao.TrainingTypeID = Convert.ToInt32(areaHiddenField.Value);
            aTypeDao.DeleteBy = Session["UserId"].ToString();
            aTypeDao.DeleteDate = DateTime.Now;
            aTypeDao.IsDeleted = true;

            bool result = aVenueDAL.DeleteAreaInfoById(areaHiddenField.Value);

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successfull Done...');window.location ='VenueList.aspx';",
                   true);
                Clear();
            }
            else
            {

                AlertMessageBoxShow("Operation Failed...");

            }
        }
    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }
}