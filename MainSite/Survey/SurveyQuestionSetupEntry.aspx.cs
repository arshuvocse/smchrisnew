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

public partial class Survey_SurveyQuestionSetupEntry : System.Web.UI.Page
{
    ValidationDeleteCommonDAL aValidationDeleteCommonDAL = new ValidationDeleteCommonDAL();

    SurveyQuestionSetupEntryDAL aVaencyEntryDaL = new SurveyQuestionSetupEntryDAL();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private int  Midid=0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            SetCheckBox();
            ButtonVisible();

            LoadDropDownList();

            if (Session["VacancyCirculationId"] != null)
            {
                LoadDropDownListAll();
               
                GetOneRecord(Session["VacancyCirculationId"].ToString());
                Session["VacancyCirculationId"] = null;
            }
        }
    }

    private void LoadDropDownListAll()
    {
        aVaencyEntryDaL.GetDDLSurveyQuestionGroupALL(SurveyQuestionGroupDropDownList);
        aVaencyEntryDaL.GetDDLSurveyQuestionTypeALl(SurveyQuestionTypeDropDownList);
    }

    private void LoadDropDownList()
    {
        aVaencyEntryDaL.GetDDLSurveyQuestionGroup(SurveyQuestionGroupDropDownList);
        aVaencyEntryDaL.GetDDLSurveyQuestionType(SurveyQuestionTypeDropDownList);
    }

    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {
            if (Session["Status"].ToString() == "Add")
            {
                submitButton.Visible = true;
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
            Response.Redirect("SurveyQuestionSetupView.aspx");
        }

    }

    private void GetOneRecord(string Vacaency)
    {
        DataTable dataTable = aVaencyEntryDaL.GetVacaencyInformationById(Vacaency);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            VacancyIdHiddenField.Value = dataTable.Rows[rowIndex].Field<Int32>("SurveyQuestionId").ToString(CultureInfo.InvariantCulture);
            Midid = Convert.ToInt32(VacancyIdHiddenField.Value);

            VacancyNameTextBox.Text = dataTable.Rows[rowIndex].Field<string>("QuestionTitle");
            SurveyQuestionGroupDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<int>("SurveyQuestionGroupId").ToString();
            SurveyQuestionTypeDropDownList.SelectedValue = dataTable.Rows[rowIndex].Field<int>("SurveyQuestionTypeId").ToString();

            if (SurveyQuestionTypeDropDownList.SelectedValue=="1")
            {
                ShowDiv.Visible = true;
            }

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

            int midd = Convert.ToInt32(VacancyIdHiddenField.Value);

            DataTable ssss = aVaencyEntryDaL.GetVacaencyDtlsById(Vacaency);

            if (ssss.Rows.Count>0)
            {
                OtherRequirementsGridView.DataSource = ssss;
                OtherRequirementsGridView.DataBind();
            } 
           
            

        }
    }
    private void SetCheckBox()
    {
        if (!isActiveCheckBox.Checked)
        {
            isActiveCheckBox.Checked = true;
        }
    }
    private bool Validation()
    {
        

        if (VacancyNameTextBox.Text == "")
        {
            aShowMessage.ShowMessageBox("Please Enter This!!", this);
            VacancyNameTextBox.Focus();
            return false;
        }

        if (SurveyQuestionGroupDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select This!!", this);
            SurveyQuestionGroupDropDownList.Focus();
            return false;
        }
        if (SurveyQuestionTypeDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select This!!", this);
            SurveyQuestionTypeDropDownList.Focus();
            return false;
        }

        if (OtherRequirementsGridView.Rows.Count != 4)
        {
            aShowMessage.ShowMessageBox("Answer Quantity Must Be Four!!", this);
            othersTextBox.Focus();
            return false;
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
            if (VacancyIdHiddenField.Value == "")
            {
                try
                {
                    Int32 areaId = SaveVacancyEntry();

                    if (areaId > 0)
                    {
                        if (SurveyQuestionTypeDropDownList.SelectedValue=="1")
                        {
                           
                            using (var db = new HRIS_SMC_DBEntities())
                            {

                                if (OtherRequirementsGridView.Rows.Count == 0)
                                {
                                    db.Database.ExecuteSqlCommand("DELETE FROM  dbo.tblSurveyQuestionAnswer   WHERE SurveyQuestionTitleId={0}",
                                       areaId);
                                }
                                else
                                {
                                    db.Database.ExecuteSqlCommand("DELETE FROM  dbo.tblSurveyQuestionAnswer   WHERE SurveyQuestionTitleId={0}",
                                      areaId);
                                }

                                if (OtherRequirementsGridView.Rows.Count > 0)
                                {
                                    


                                    for (int i = 0; i < OtherRequirementsGridView.Rows.Count; i++)
                                    {
                                        string ans = OtherRequirementsGridView.Rows[i].Cells[0].Text.Trim();
                                        SurveyMasterAnswrDtls mas = new SurveyMasterAnswrDtls();
                                        mas.SurveyQuestionTitleId = areaId;
                                        mas.SurveyQuestionGroupId = Convert.ToInt32(SurveyQuestionTypeDropDownList.SelectedValue);
                                        mas.SurveyQuestionAnswer = ans;

                                        aVaencyEntryDaL.SaveVacancyDtls(mas);
                                    }

                                    //for (int i = 0; i < OtherRequirementsGridView.Rows.Count; i++)
                                    //{

                                     
                                    //    mas.SurveyQuestionTitleId = areaId;
                                    //    mas.SurveyQuestionGroupId = Convert.ToInt32(SurveyQuestionTypeDropDownList.SelectedValue);
                                    //    mas.SurveyQuestionAnswer = ans;
                                    //    mas.EntryDate = DateTime.Now;
                                    //    db.tblSurveyQuestionAnswers.Add(mas);
                                    //    db.SaveChanges();


                                    //}
                                }
                            }
                        }
                       
                     

                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                          "alert",
                          "alert('Data Saved Successfully...');window.location ='SurveyQuestionSetupView.aspx';",
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
    private bool UpdateAreaInformation(SurveyQuestionSetupEntryDAO prepareDataForUpdate)
    {
        bool retVal;
        try
        {
            retVal = aVaencyEntryDaL.UpdateVacancyEntryInfo(PrepareDataForUpdate());
        }
        catch (Exception)
        {
            retVal = false;
        }

        return retVal;
    }
    private SurveyQuestionSetupEntryDAO PrepareDataForUpdate()
    {
        var aVacancyEntryDao = new SurveyQuestionSetupEntryDAO();

        aVacancyEntryDao.SurveyQuestionId = Convert.ToInt32(VacancyIdHiddenField.Value);

        aVacancyEntryDao.QuestionTitle = VacancyNameTextBox.Text.Trim();
        aVacancyEntryDao.SurveyQuestionGroupId = Convert.ToInt32(SurveyQuestionGroupDropDownList.SelectedValue);
        aVacancyEntryDao.SurveyQuestionTypeId = Convert.ToInt32(SurveyQuestionTypeDropDownList.SelectedValue);

        aVacancyEntryDao.IsActive = isActiveCheckBox.Checked;

        aVacancyEntryDao.UpdateBy = Convert.ToInt32(Session["UserId"]);
        aVacancyEntryDao.UpdateDate = DateTime.Now;

        return aVacancyEntryDao;
    }
    private Int32 SaveVacancyEntry()
    {
        Int32 retVal;
        try
        {
            retVal = aVaencyEntryDaL.SaveVacancyEntryInfo(PrepareDataForSave());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }
    private SurveyQuestionSetupEntryDAO PrepareDataForSave()
    {
        var aVacancyEntryDao = new SurveyQuestionSetupEntryDAO();



        aVacancyEntryDao.QuestionTitle = VacancyNameTextBox.Text.Trim();
        aVacancyEntryDao.SurveyQuestionGroupId = Convert.ToInt32(SurveyQuestionGroupDropDownList.SelectedValue);
        aVacancyEntryDao.SurveyQuestionTypeId = Convert.ToInt32(SurveyQuestionTypeDropDownList.SelectedValue);

        aVacancyEntryDao.IsActive = isActiveCheckBox.Checked;

        
        aVacancyEntryDao.EntryBy = Convert.ToInt32(Session["UserId"]);


        aVacancyEntryDao.EntryDate = DateTime.Now;


       


        return aVacancyEntryDao;
    }
    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }
    private void Clear()
    {
       
        VacancyIdHiddenField.Value = "";
        
        VacancyNameTextBox.Text = "";
      
        submitButton.Text = "Save";
    }
    protected void areaCodeTextBox_OnTextChanged(object sender, EventArgs e)
    {
        
    }
     
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("SurveyQuestionSetupView.aspx");
    }
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AreaInformationView.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (VacancyIdHiddenField.Value != "")
        {
            try
            {
              //  if (!CheckHobbyAllocateOrNot(VacancyIdHiddenField.Value))
                {
                    bool area = UpdateAreaInformation(PrepareDataForUpdate());

                    if (area)
                    {


                        int maihhh = Convert.ToInt32(VacancyIdHiddenField.Value);
                        if (SurveyQuestionTypeDropDownList.SelectedValue == "1")
                        {
                          
                            using (var db = new HRIS_SMC_DBEntities())
                            {

                                if (OtherRequirementsGridView.Rows.Count == 0)
                                {
                                    db.Database.ExecuteSqlCommand("DELETE FROM  dbo.tblSurveyQuestionAnswer   WHERE SurveyQuestionTitleId={0}",
                                       VacancyIdHiddenField.Value);
                                }
                                else
                                {
                                    db.Database.ExecuteSqlCommand("DELETE FROM  dbo.tblSurveyQuestionAnswer   WHERE SurveyQuestionTitleId={0}",
                                      VacancyIdHiddenField.Value);
                                }

                                for (int i = 0; i < OtherRequirementsGridView.Rows.Count; i++)
                                {
                                    string ans = OtherRequirementsGridView.Rows[i].Cells[0].Text.Trim();
                                    SurveyMasterAnswrDtls mas = new SurveyMasterAnswrDtls();
                                    mas.SurveyQuestionTitleId = Convert.ToInt32(VacancyIdHiddenField.Value);
                                    mas.SurveyQuestionGroupId = Convert.ToInt32(SurveyQuestionTypeDropDownList.SelectedValue);
                                    mas.SurveyQuestionAnswer = ans;

                                    aVaencyEntryDaL.SaveVacancyDtls(mas);
                                }

                            }
                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Data Updated Successfully...');window.location ='SurveyQuestionSetupView.aspx';",
                            true);
                    }
                }

                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(),
                //        "alert",
                //        "alert('Can not be Updated! Already Defined in Survey Question Group...');window.location ='SurveyQuestionSetupView.aspx';",
                //        true);

                //}
            }
            catch (Exception ex)
            {
                aShowMessage.ShowMessageBox(aMessages.UpdateFailedMessage, this);
                throw;
            }
        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        if (VacancyIdHiddenField.Value != string.Empty)
        {
            Delete();
        }
    }

    private void Delete()
    {
        SurveyQuestionSetupEntryDAO aVacancyEntryDao = new SurveyQuestionSetupEntryDAO();


      //  if (!CheckHobbyAllocateOrNot(VacancyIdHiddenField.Value))
        {

            aVacancyEntryDao.SurveyQuestionId = Convert.ToInt32(VacancyIdHiddenField.Value);
            aVacancyEntryDao.QuestionTitle = VacancyNameTextBox.Text.Trim();
            aVacancyEntryDao.SurveyQuestionGroupId = Convert.ToInt32(SurveyQuestionGroupDropDownList.SelectedValue);
            aVacancyEntryDao.SurveyQuestionTypeId = Convert.ToInt32(SurveyQuestionTypeDropDownList.SelectedValue);
            aVacancyEntryDao.IsActive = isActiveCheckBox.Checked;
            aVacancyEntryDao.EntryBy = Convert.ToInt32(Session["UserId"]);
            aVacancyEntryDao.EntryDate = DateTime.Now;
            
            aVaencyEntryDaL.SaveInfoDEL(aVacancyEntryDao);
            if (aVaencyEntryDaL.DeleteEntryfoById(VacancyIdHiddenField.Value))
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Data Deleted Successfully...');window.location ='SurveyQuestionSetupView.aspx';",
                    true);
            }
        }
        //else
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(),
        //        "alert",
        //        "alert('Can not be Deleted! Already Defined in Survey Question Group...');window.location ='SurveyQuestionSetupView.aspx';",
        //        true);

        //}
    }

    private bool CheckHobbyAllocateOrNot(string hobbyid)
    {
        bool status = false;

        DataTable dataTable = aValidationDeleteCommonDAL.SurveyQuestionGroupAllocatedOrNotEMP(hobbyid);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void SurveyQuestionTypeDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (SurveyQuestionTypeDropDownList.SelectedValue=="1")
        {
            ShowDiv.Visible = true;
        }
        else
        {
            ShowDiv.Visible = false;
            

        }
    }

    protected void OtherRequirementsAddButton_OnClick(object sender, EventArgs e)
    {
        if (valissss())
        {


            var aDataTable = new DataTable();

            aDataTable.Columns.Add("OtherRequirementsName");
            DataRow dataRow;

            if (othersTextBox.Text != "")
            {
                string jd = othersTextBox.Text.Trim();

                if (OtherRequirementsGridView.Rows.Count == 0)
                {
                    dataRow = aDataTable.NewRow();

                    dataRow = aDataTable.NewRow();
                    dataRow["OtherRequirementsName"] = jd;

                    aDataTable.Rows.Add(dataRow);

                    OtherRequirementsGridView.DataSource = aDataTable;
                    OtherRequirementsGridView.DataBind();
                    othersTextBox.Text = string.Empty;
                }

                else
                {
                    for (int i = 0; i < OtherRequirementsGridView.Rows.Count; i++)
                    {
                        if (OtherRequirementsGridView.Rows[i].Cells[0].Text != jd)
                        {
                            dataRow = aDataTable.NewRow();
                            dataRow["OtherRequirementsName"] = OtherRequirementsGridView.Rows[i].Cells[0].Text;
                            aDataTable.Rows.Add(dataRow);
                        }

                        else
                        {
                            othersTextBox.Text = "";
                            ShowMessageBox("Data already Exist !!");
                        }
                    }

                    dataRow = aDataTable.NewRow();
                    dataRow["OtherRequirementsName"] = jd;

                    aDataTable.Rows.Add(dataRow);

                    OtherRequirementsGridView.DataSource = aDataTable;
                    OtherRequirementsGridView.DataBind();
                    othersTextBox.Text = string.Empty;
                }
            }
        }
    }

    private bool valissss()
    {
        if (OtherRequirementsGridView.Rows.Count>=4)
        {
            aShowMessage.ShowMessageBox("Answer Quantity Must Be Four!!", this);
            othersTextBox.Focus();
            return false;
        }
        return true;
    }


    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void editOtherRequirementsGridViewButton_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;


        string jd = OtherRequirementsGridView.Rows[rowindex].Cells[0].Text;
        othersTextBox.Text = jd;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("OtherRequirementsName");

        DataRow dataRow;

        if (OtherRequirementsGridView.Rows.Count > 0)
        {
            for (int i = 0; i < OtherRequirementsGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["OtherRequirementsName"] = OtherRequirementsGridView.Rows[i].Cells[0].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }

        OtherRequirementsGridView.DataSource = aDataTable;
        OtherRequirementsGridView.DataBind();
    }

    protected void deleteOtherRequirementsGridViewButton_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        var aDataTable = new DataTable();


        aDataTable.Columns.Add("OtherRequirementsName");

        DataRow dataRow;

        if (OtherRequirementsGridView.Rows.Count > 0)
        {
            for (int i = 0; i < OtherRequirementsGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();


                    dataRow["OtherRequirementsName"] = OtherRequirementsGridView.Rows[i].Cells[0].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }


        OtherRequirementsGridView.DataSource = aDataTable;
        OtherRequirementsGridView.DataBind();
    }
}