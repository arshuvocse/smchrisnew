using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.ExecutiveOfficeDocDal;
using DAL.MasterSetup_DAL;
using DAL.MeetingMinorsDAL;
using DAO.ExcOfficeDoc_Dao;
using DAO.HRIS_DAO;
using DAO.MeetingMinorsDAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class HealthCare_UI_ExecutiveOfficeDocumentCategory : System.Web.UI.Page
{
    ValidationDeleteCommonDAL aValidationDeleteCommonDAL = new ValidationDeleteCommonDAL();
    ExeOfficeDocDal aEntryDaL = new ExeOfficeDocDal();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonVisible();

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
                btn_Save.Visible = true;
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
            Response.Redirect("ExecutiveOfficeDocumentCategoryView.aspx");
        }

    }
    private void GetOneRecord(string id)
    {
        DataTable dataTable = aEntryDaL.GetInformationById(id);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            MeetingCategoryIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("ExeOfficeDocCatId").ToString(CultureInfo.InvariantCulture);

            TxtCategory.Text = dataTable.Rows[rowIndex].Field<string>("ExeOfficeDocCategory");


        }
    }
    private bool Validation()
    {


        if (TxtCategory.Text == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            return false;
        }


        using (DataTable dt = aEntryDaL.GetCheckPartInfo(TxtCategory.Text.Trim().ToUpper()))
        {
            if (MeetingCategoryIdHiddenField.Value == "")
            {
                if (dt.Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Document Category Already Exist!!!');",
                        true);

                    TxtCategory.Focus();

                    return false;
                }
            }
            else
            {
                DataTable dt2 = aEntryDaL.GetCheckPartInfo2(TxtCategory.Text.Trim().ToUpper(), MeetingCategoryIdHiddenField.Value);
                if (dt2.Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Document Category Already Exist!!!');",
                        true);

                    TxtCategory.Focus();
                    return false;
                }
            }
        }



        //if (String.IsNullOrEmpty(myTextBox.Text))
        //{
        //    //Tell the user that the text provided is unacceptable.
        //    aShowMessage.ShowMessageBox("The content of the Textbox is not valid.",this);
        //    //Validation was unsuccessful.
        //    return false;
        //}



        return true;
    }
    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (MeetingCategoryIdHiddenField.Value == "")
            {
                try
                {
                    Int32 areaId = SaveVacancyEntry();

                    if (areaId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "alert",
                          "alert('Data Saved Successfully...');window.location ='ExecutiveOfficeDocumentCategoryView.aspx';",
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
    private bool Updateformation(ExeOfficeDocCategoryDao prepareDataForUpdate)
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
    private ExeOfficeDocCategoryDao PrepareDataForUpdate()
    {
        var aEntryDao = new ExeOfficeDocCategoryDao();

        aEntryDao.ExeOfficeDocCatId = Convert.ToInt32(MeetingCategoryIdHiddenField.Value);

        aEntryDao.ExeOfficeDocCategory = TxtCategory.Text.Trim();

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
    private ExeOfficeDocCategoryDao PrepareDataForSave()
    {
        var EntryDao = new ExeOfficeDocCategoryDao();


        EntryDao.ExeOfficeDocCategory = TxtCategory.Text.Trim();


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

        MeetingCategoryIdHiddenField.Value = "";

        TxtCategory.Text = "";


    }
    protected void areaCodeTextBox_OnTextChanged(object sender, EventArgs e)
    {

    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ExecutiveOfficeDocumentCategoryView.aspx");

    }
    protected void Button1_OnClick(object sender, EventArgs e)
    {
       Response.Redirect("ExecutiveOfficeDocumentCategoryView.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {

            if (MeetingCategoryIdHiddenField.Value != "")
            {
                try
                {
                    bool area = Updateformation(PrepareDataForUpdate());
                    if (area)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Data Updated Successfully...');window.location ='ExecutiveOfficeDocumentCategoryView.aspx';",
                            true);
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

        if (MeetingCategoryIdHiddenField.Value != "")
        {

            if (aEntryDaL.DeleteEntryfoById(MeetingCategoryIdHiddenField.Value))
            {
                Int32 departmentId = SaveInformationDEL();

                if (departmentId > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successfully Done...');window.location ='ExecutiveOfficeDocumentCategoryView.aspx';",
                        true);
                }

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
    private ExeofficeDocCategory_DEL_Dao PrepareDataForSaveDEL()
    {
        var EntryDao = new ExeofficeDocCategory_DEL_Dao();

        EntryDao.ExeOfficeDocCategory = TxtCategory.Text.Trim();
        EntryDao.ExeOfficeDocCatId = Convert.ToInt32(MeetingCategoryIdHiddenField.Value);
        EntryDao.CreateBy = Convert.ToInt32(Session["UserId"]);
        EntryDao.CreateDate = DateTime.Now;

        return EntryDao;
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}
