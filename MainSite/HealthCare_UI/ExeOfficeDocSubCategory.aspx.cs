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
using DAO.ExcOfficeDoc_Dao;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class HealthCare_UI_ExeOfficeDocSubCategory : System.Web.UI.Page
{
    ValidationDeleteCommonDAL aValidationDeleteCommonDAL = new ValidationDeleteCommonDAL();

    ExeOfficeDocSubcategoryDal aEntryDaL = new ExeOfficeDocSubcategoryDal();

    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();

    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();

    ExecutiveOfficeDocUpDal AMAsterDal = new ExecutiveOfficeDocUpDal();

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


    private void Dropdownlist()
    {
        AMAsterDal.GetExeOfficeCategoryListIntoDropdown(ddlCagetory);
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
            Response.Redirect("ExeOfficeDocSubCateView.aspx");
        }

    }
    private void GetOneRecord(string id)
    {
        DataTable dataTable = aEntryDaL.GetInformationById(id);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            HfPk.Value = dataTable.Rows[rowIndex].Field<Int32>("ExeOfficeDocSubCatId").ToString(CultureInfo.InvariantCulture);
            ddlCagetory.SelectedValue = dataTable.Rows[0]["ExeOfficeDocCatId"].ToString();
            txtSubCategory.Text = dataTable.Rows[0]["ExeOfficeDocSubCate"].ToString();
            //  TxtCategory.Text = dataTable.Rows[rowIndex].Field<string>("ExeOfficeDocCategory");

        }
    }

    private bool Validation()
    {

        if (ddlCagetory.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please! Select Category", this);
            ddlCagetory.Focus();
            return false;
        }

        if (txtSubCategory.Text.Trim() == "")
        {
            aShowMessage.ShowMessageBox("Please! Select Sub-Category", this);
            txtSubCategory.Focus();
            return false;
        }

        using (DataTable dt = aEntryDaL.GetCheckPartInfo(ddlCagetory.SelectedValue, txtSubCategory.Text.Trim()))
        {
            if (HfPk.Value == "")
            {
                if (dt.Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Already Exist!!!');",
                        true);
                    ddlCagetory.Focus();
                    return false;
                }
            }
            else
            {
                DataTable dt2 = aEntryDaL.GetCheckPartInfo2(ddlCagetory.SelectedValue,txtSubCategory.Text.Trim(), HfPk.Value);
                if (dt2.Rows.Count > 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Already Exist!!!');",
                        true);
                    ddlCagetory.Focus();
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
            if (HfPk.Value == "")
            {
                try
                {
                    Int32 areaId = SaveVacancyEntry();

                    if (areaId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "alert",
                          "alert('Data Saved Successfully...');window.location ='ExeOfficeDocSubCateView.aspx';",
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
    private bool Updateformation(ExeOffDocSubCategoryDao prepareDataForUpdate)
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
    private ExeOffDocSubCategoryDao PrepareDataForUpdate()
    {
        var aEntryDao = new ExeOffDocSubCategoryDao();

        aEntryDao.ExeOfficeDocSubCatId = Convert.ToInt32(HfPk.Value);
        aEntryDao.ExeOfficeDocCatId = Convert.ToInt32(ddlCagetory.SelectedValue);
        aEntryDao.ExeOfficeDocSubCate = txtSubCategory.Text.Trim();
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
    private ExeOffDocSubCategoryDao PrepareDataForSave()
    {
        var EntryDao = new ExeOffDocSubCategoryDao();

        EntryDao.ExeOfficeDocCatId = Convert.ToInt32(ddlCagetory.SelectedValue);
        EntryDao.ExeOfficeDocSubCate = txtSubCategory.Text;
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

        HfPk.Value = "";

        ddlCagetory.Text = "";


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

            if (HfPk.Value != "")
            {
                try
                {
                    bool area = Updateformation(PrepareDataForUpdate());
                    if (area)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Data Updated Successfully...');window.location ='ExeOfficeDocSubCateView.aspx';",
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

        if (HfPk.Value != "")
        {

            if (aEntryDaL.DeleteEntryfoById(HfPk.Value))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfully Done...');window.location ='ExeOfficeDocSubCateView.aspx';",
                    true);

                //Int32 departmentId = SaveInformationDEL();

                //if (departmentId > 0)
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(),
                //        "alert",
                //        "alert('Operation Successfully Done...');window.location ='ExecutiveOfficeDocumentCategoryView.aspx';",
                //        true);
                //}

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

        EntryDao.ExeOfficeDocCategory = HfPk.Value;
        EntryDao.ExeOfficeDocCatId = Convert.ToInt32(HfPk.Value);
        EntryDao.CreateBy = Convert.ToInt32(Session["UserId"]);
        EntryDao.CreateDate = DateTime.Now;

        return EntryDao;
    }

    
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}