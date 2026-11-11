using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MasterSetup_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class DashBoard_UI_DashboardSettingPage : System.Web.UI.Page
{
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    DashboardSettingPageDAL aEntryDaL = new DashboardSettingPageDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {



            if (Session["UserId"] != null)
            {
                GetOneRecord(Session["UserId"].ToString());
                
            }
        }
    }

    private void GetOneRecord(string id)
    {
        DataTable dataTable = aEntryDaL.GetInformationById(id);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            IdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("DashboardSettingId").ToString(CultureInfo.InvariantCulture);


            ContractualDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("Contractual").ToString();
            ProhibitionDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("Prohibition").ToString();
            RetirementDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<Int32>("Retirement").ToString();
            HiddenFieldUserID.Value = dataTable.Rows[rowIndex].Field<Int32>("UserId").ToString();

            submitButton.Visible = false;
            editButton.Visible = true;

        }
    }
 

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Validation())
        {
            if (HiddenFieldUserID.Value == "")
            {
                try
                {
                    Int32 areaId = Save();

                    if (areaId > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "alert",
                          "alert('Data Saved Successfully...');window.location ='DashboardSettingPage.aspx';",
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


    private Int32 Save()
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

    private DashboardSettingDAO PrepareDataForSave()
    {
        var EntryDao = new DashboardSettingDAO();




        EntryDao.Contractual = Convert.ToInt32(ContractualDropDownList.SelectedValue);
        EntryDao.Prohibition = Convert.ToInt32(ProhibitionDropDownList.SelectedValue);
        EntryDao.Retirement = Convert.ToInt32(RetirementDropDownList.SelectedValue);





        EntryDao.UserId = Convert.ToInt32(Session["UserId"]);


        

        return EntryDao;
    }
    private bool Validation()
    {
        if (ContractualDropDownList.SelectedValue == "0")
        {
            aShowMessage.ShowMessageBox("Please Select This!!!", this);
            ContractualDropDownList.Focus();
            return false;
        }

        if (ProhibitionDropDownList.SelectedValue == "0")
        {
            aShowMessage.ShowMessageBox("Please Select This!!!", this);
            ProhibitionDropDownList.Focus();
            return false;
        }

        if (RetirementDropDownList.SelectedValue == "0")
        {
            aShowMessage.ShowMessageBox("Please Select This!!!", this);
            RetirementDropDownList.Focus();
            return false;
        }



         



        return true;
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (HiddenFieldUserID.Value != "")
        {
            try
            {
                bool area = Updateformation(PrepareDataForUpdate());

                if (area)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Data Updated Successfully...');window.location ='DashboardSettingPage.aspx';",
                        true);
                }
            }
            catch (Exception)
            {
                aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
            }
        }
    }

    private bool Updateformation(DashboardSettingDAO prepareDataForUpdate)
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

    private DashboardSettingDAO PrepareDataForUpdate()
    {
        var aEntryDao = new DashboardSettingDAO();

        aEntryDao.DashboardSettingId = Convert.ToInt32(IdHiddenField.Value);
        aEntryDao.UserId = Convert.ToInt32(HiddenFieldUserID.Value);

        aEntryDao.Contractual = Convert.ToInt32(ContractualDropDownList.SelectedValue);
        aEntryDao.Prohibition = Convert.ToInt32(ProhibitionDropDownList.SelectedValue);
        aEntryDao.Retirement = Convert.ToInt32(RetirementDropDownList.SelectedValue);





        

        return aEntryDao;
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}