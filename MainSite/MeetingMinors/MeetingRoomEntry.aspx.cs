using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.MasterSetup_DAL;
using DAL.MeetingMinorsDAL;
using DAO.HRIS_DAO;
using DAO.MeetingMinorsDAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MeetingMinors_MeetingRoomEntry : System.Web.UI.Page
{
    ValidationDeleteCommonDAL aValidationDeleteCommonDAL = new ValidationDeleteCommonDAL();

    MeetingRoomDal aEntryDaL = new MeetingRoomDal();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


             ButtonVisible();


             Dropdownlist();


            if (Session["MId"] != null)
            {
                GetOneRecord(Session["MId"].ToString());
                Session["MId"] = null;
            }
        }
    }
    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {
            if (Session["Status"].ToString() == "Add")
            {
                submit_Button.Visible = true;
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
        else
        {
            Response.Redirect("~/MeetingMinors/MeetingRoomView.aspx");
        }

    }




    private void GetOneRecord(string id)
    {
        DataTable dataTable = aEntryDaL.GetInformationById(id);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            try
            {
                MeetingRoomIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("MeetingRoomId").ToString(CultureInfo.InvariantCulture);
            ddlOffice.SelectedValue = dataTable.Rows[rowIndex].Field<int>("OfficeId").ToString();
            Session["LocationId"] = dataTable.Rows[rowIndex].Field<int>("LocationId").ToString();
            Session["FloorId"] = dataTable.Rows[rowIndex].Field<int>("FloorId").ToString();
            txtMeetingRoom.Text = dataTable.Rows[rowIndex].Field<string>("MeetingRoomName").ToString();
            try
            {
                TxtsCapacity.Text = dataTable.Rows[rowIndex].Field<decimal>("MeetingRoomCapacity").ToString();
            }
            catch
            {

            }

            }
            catch
            {

            }

        }

        using (DataTable dt = aEntryDaL.GetDDLJobLocation(ddlOffice.SelectedValue))
        {
            ddlLocation.DataSource = dt;
            ddlLocation.DataValueField = "Value";
            ddlLocation.DataTextField = "TextField";
            ddlLocation.DataBind();
            ddlLocation.SelectedValue = Session["LocationId"].ToString();
        }

        using (DataTable dt = aEntryDaL.GetDDLFloorLocation(ddlLocation.SelectedValue))
        {
            ddlFloor.DataSource = dt;
            ddlFloor.DataValueField = "Value";
            ddlFloor.DataTextField = "TextField";
            ddlFloor.DataBind();
            ddlFloor.SelectedValue = Session["FloorId"].ToString();
        }

    }



    private void Dropdownlist()
    {

        using (DataTable dt = aEntryDaL.GetDDLCompany())
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
            ddlCompany.SelectedIndex = 1;
            ddlCompany_OnSelectedIndexChanged(null, null);
        }

        using (DataTable dt = aEntryDaL.GetDDLSalaryLocation())
        {
            ddlOffice.DataSource = dt;
            ddlOffice.DataValueField = "Value";
            ddlOffice.DataTextField = "TextField";
            ddlOffice.DataBind();
        }


      

    }


    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void ddlOffice_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlOffice.SelectedIndex > 0)
        {
            using (DataTable dt = aEntryDaL.GetDDLJobLocation(ddlOffice.SelectedValue))
            {
                ddlLocation.DataSource = dt;
                ddlLocation.DataValueField = "Value";
                ddlLocation.DataTextField = "TextField";
                ddlLocation.DataBind();
            }
        }
    }


    protected void ddlLocation_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLocation.SelectedIndex > 0)
        {
            using (DataTable dt = aEntryDaL.GetDDLFloorLocation(ddlLocation.SelectedValue))
            {
                ddlFloor.DataSource = dt;
                ddlFloor.DataValueField = "Value";
                ddlFloor.DataTextField = "TextField";
                ddlFloor.DataBind();
            }
        }
    }


    private bool Validation()
    {

        if (ddlCompany.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            return false;
        }


        if (ddlOffice.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            return false;
        }

        if (ddlLocation.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            return false;
        }

        if (ddlFloor.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            return false;
        }


        if (txtMeetingRoom.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            return false;
        }




        //using (DataTable dt = aEntryDaL.GetCheckOffice(ddlOffice.SelectedValue))
        //{
        //    if (MeetingRoomIdHiddenField.Value == "")
        //    {
        //        if (dt.Rows.Count > 0)
        //        {

        //            ScriptManager.RegisterStartupScript(this, this.GetType(),
        //                "alert",
        //                "alert('Office Already Exist!!!');",
        //                true);

        //            ddlOffice.Focus();

        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        DataTable dt2 = aEntryDaL.GetCheckOffice2(ddlOffice.SelectedValue, MeetingRoomIdHiddenField.Value);
        //        if (dt2.Rows.Count > 0)
        //        {

        //            ScriptManager.RegisterStartupScript(this, this.GetType(),
        //                "alert",
        //                "alert('Location Already Exist!!!');",
        //                true);

        //            ddlOffice.Focus();
        //            return false;
        //        }
        //    }
        //}


        //using (DataTable dt = aEntryDaL.GetCheckLocation(ddlLocation.SelectedValue))
        //{
        //    if (MeetingRoomIdHiddenField.Value == "")
        //    {
        //        if (dt.Rows.Count > 0)
        //        {

        //            ScriptManager.RegisterStartupScript(this, this.GetType(),
        //                "alert",
        //                "alert('Floor Already Exist!!!');",
        //                true);

        //            ddlOffice.Focus();

        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        DataTable dt2 = aEntryDaL.GetCheckLocation2(ddlLocation.SelectedValue, MeetingRoomIdHiddenField.Value);
        //        if (dt2.Rows.Count > 0)
        //        {

        //            ScriptManager.RegisterStartupScript(this, this.GetType(),
        //                "alert",
        //                "alert('Already Exist!!!');",
        //                true);

        //            ddlOffice.Focus();
        //            return false;
        //        }
        //    }
        //}


        //using (DataTable dt = aEntryDaL.GetCheckFloor(ddlFloor.SelectedValue))
        //{
        //    if (MeetingRoomIdHiddenField.Value == "")
        //    {
        //        if (dt.Rows.Count > 0)
        //        {

        //            ScriptManager.RegisterStartupScript(this, this.GetType(),
        //                "alert",
        //                "alert('Already Exist!!!');",
        //                true);

        //            ddlLocation.Focus();

        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        DataTable dt2 = aEntryDaL.GetCheckFloor2(ddlFloor.SelectedValue, MeetingRoomIdHiddenField.Value);
        //        if (dt2.Rows.Count > 0)
        //        {

        //            ScriptManager.RegisterStartupScript(this, this.GetType(),
        //                "alert",
        //                "alert('Already Exist!!!');",
        //                true);

        //            ddlFloor.Focus();
        //            return false;
        //        }
        //    }
        //}


        using (DataTable dt = aEntryDaL.GetCheckMeetingRoom(txtMeetingRoom.Text.Trim().ToUpper()))
        {
            if (MeetingRoomIdHiddenField.Value == "")
            {
                if (dt.Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Meeting Room Already Exist!!!');",
                        true);

                    ddlLocation.Focus();

                    return false;
                }
            }
            else
            {
                DataTable dt2 = aEntryDaL.GetCheckMeetingRoom2(txtMeetingRoom.Text.ToUpper(), MeetingRoomIdHiddenField.Value);
                if (dt2.Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Meeting Room Already Exist!!!');",
                        true);

                    ddlFloor.Focus();
                    return false;
                }
            }
        }


        return true;
    }




    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (    MeetingRoomIdHiddenField.Value == "")
            {
                try
                {
                    Int32 areaId = SaveVacancyEntry();

                    if (areaId > 0)
                    {
                        
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "alert",
                          "alert('Data Saved Successfully...');window.location ='MeetingRoomView.aspx';",
                          true);
                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                }
            }


        }
    }
    private bool Updateformation(MeetingRoomDao prepareDataForUpdate)
    {
        bool retVal;
        try
        {
            retVal = aEntryDaL.UpdateEntryInfo(PrepareDataForUpdate());
        }
        catch (Exception)
        {
            retVal = false;
        }

        return retVal;
    }
    private MeetingRoomDao PrepareDataForUpdate()
    {
        var aEntryDao = new MeetingRoomDao();
        aEntryDao.MeetingRoomId = Convert.ToInt32(MeetingRoomIdHiddenField.Value);
        aEntryDao.OfficeId = Convert.ToInt32(ddlOffice.SelectedValue);
        aEntryDao.LocationId = Convert.ToInt32(ddlLocation.SelectedValue);
        aEntryDao.FloorId = Convert.ToInt32(ddlFloor.SelectedValue);
        aEntryDao.MeetingRoomName = txtMeetingRoom.Text.ToString();
        if (TxtsCapacity.Text == "")
        {
            aEntryDao.MeetingRoomCapacity = null;
        }
        else
        {
           aEntryDao.MeetingRoomCapacity = Convert.ToDecimal(TxtsCapacity.Text);
        }
        aEntryDao.UpdateBy = Convert.ToInt32(Session["UserId"]);
        aEntryDao.UpdateDate = DateTime.Now;
        return aEntryDao;
    }
    private Int32 SaveVacancyEntry()
    {
        Int32 retVal;
        try
        {
            retVal = aEntryDaL.SaveEntryInfo(PrepareDataForSave());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }
    private MeetingRoomDao PrepareDataForSave()
    {
        var EntryDao = new MeetingRoomDao();
        EntryDao.OfficeId = Convert.ToInt32(ddlOffice.SelectedValue);
        EntryDao.LocationId = Convert.ToInt32(ddlLocation.SelectedValue);
        EntryDao.FloorId = Convert.ToInt32(ddlFloor.SelectedValue);
        EntryDao.MeetingRoomName = txtMeetingRoom.Text.ToString();
        if (TxtsCapacity.Text == "")
        {
            EntryDao.MeetingRoomCapacity = null;
        }
        else
        {
            EntryDao.MeetingRoomCapacity = Convert.ToDecimal(TxtsCapacity.Text);
        }
        EntryDao.CreateBy = Convert.ToInt32(Session["UserId"]);
        EntryDao.CreateDate = DateTime.Now;
        return EntryDao;
    }


    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
        MeetingRoomIdHiddenField.Value = "";
        ddlOffice.SelectedValue = string.Empty;
        ddlLocation.SelectedValue = string.Empty;
        ddlFloor.SelectedValue = string.Empty;
        txtMeetingRoom.Text = string.Empty;
        TxtsCapacity.Text = string.Empty;
    }
    protected void areaCodeTextBox_OnTextChanged(object sender, EventArgs e)
    {

    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/MeetingMinors/MeetingRoomView.aspx");
    }
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterSetup_UI/AreaInformationView.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {

     
         if ( MeetingRoomIdHiddenField.Value != "")
         {
             try
             {
               
                bool area = Updateformation(PrepareDataForUpdate());

                if (area)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Data Updated Successfully...');window.location ='MeetingRoomView.aspx';",
                        true);
                }

             }
            
             catch 
                 (Exception ex)
            {
                aShowMessage.ShowMessageBox(aMessages.UpdateFailedMessage, this);
                throw;
            }
         }

        }
    }


    

    protected void delButton_OnClick(object sender, EventArgs e)
    {

        if (aEntryDaL.DeleteEntryfoById(MeetingRoomIdHiddenField.Value))
        {
            Int32 departmentId = SaveInformationDEL();

            if (departmentId > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfull Done...');window.location ='MeetingRoomView.aspx';",
                    true);
            }

        }


    }


    private Int32 SaveInformationDEL()
    {
        Int32 retVal;
        try
        {
            retVal = aEntryDaL.SaveInfoDEL(PrepareDataForSaveDEL());

        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }
    private MeetingRoomDao PrepareDataForSaveDEL()
    {
        var EntryDao = new MeetingRoomDao();
        EntryDao.MeetingRoomId = Convert.ToInt32(MeetingRoomIdHiddenField.Value);
        EntryDao.OfficeId = Convert.ToInt32(ddlOffice.SelectedValue);
        EntryDao.LocationId = Convert.ToInt32(ddlLocation.SelectedValue);
        EntryDao.FloorId = Convert.ToInt32(ddlFloor.SelectedValue);
        EntryDao.MeetingRoomName = txtMeetingRoom.Text.ToString();
        if (TxtsCapacity.Text == "")
        {
            EntryDao.MeetingRoomCapacity = null;
        }
        else
        {
            EntryDao.MeetingRoomCapacity = Convert.ToDecimal(TxtsCapacity.Text);
        }
    
        EntryDao.CreateBy = Convert.ToInt32(Session["UserId"]);
        EntryDao.CreateDate = DateTime.Now;
        return EntryDao;
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }


}