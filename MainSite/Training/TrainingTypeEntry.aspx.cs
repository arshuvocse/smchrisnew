using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;


public partial class Training_TrainingTypeEntry : System.Web.UI.Page
{
    TrainingTypeDAL aTrainingTypeDal=new TrainingTypeDAL();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonVisible();
            if (Session["TrTypeId"] != null)
            {

                trainingTypeHiddenField.Value = Session["TrTypeId"].ToString();
                LoadData(trainingTypeHiddenField.Value);
                Session["TrTypeId"] = null;
            }
           
        }
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

    }

    public void LoadData(string id)
    {
        DataTable dtdata = aTrainingTypeDal.GetDataForById(id);
        if (dtdata.Rows.Count>0)
        {
            trainingTypeTextBox.Text = dtdata.Rows[0]["TrainingType"].ToString();
            descTextBox.Text = dtdata.Rows[0]["Description"].ToString();
            effecevoRadioButtonList.Items[0].Selected = Convert.ToBoolean(dtdata.Rows[0]["Evalution"].ToString());
            if (effecevoRadioButtonList.Items[0].Selected==false)
            {
                effecevoRadioButtonList.Items[1].Selected = true;
            }
            if (effecevoRadioButtonList.Items[1].Selected==true)
            {
                month.Visible = false;
            }
            if (!string.IsNullOrEmpty(dtdata.Rows[0]["MonthName"].ToString()))
            {
                month.Visible = true;
                 
                     
                        
                        monthNameDropDownList.Text = dtdata.Rows[0]["MonthName"].ToString();
                  
                 
            }
        }
    }

    protected void effecevoRadioButtonList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        month.Visible = false;
        if (effecevoRadioButtonList.Items[0].Selected)
        {
            month.Visible = true;
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
        if (trainingTypeTextBox.Text == "")
        {
            showMessageBox("Training Type can not be empty!!!");
            return false;
        }
        //if (descTextBox.Text == "")
        //{
        //    showMessageBox("Description can not be empty!!!");
        //    return false;
        //}
        if (effecevoRadioButtonList.SelectedValue == "")
        {
            showMessageBox("Training Effectiveness Evaluation can not be empty!!!");
            return false;
        }
        return true;
    }

    public void Clear()
    {
        trainingTypeTextBox.Text = string.Empty;
        descTextBox.Text = string.Empty;
        effecevoRadioButtonList.Items[0].Selected = false;
        effecevoRadioButtonList.Items[1].Selected = false;
        monthNameDropDownList.Text = "";
        trainingTypeHiddenField.Value = string.Empty;
        month.Visible = false;
    }

    public void Save()
    {
        if (Validation())
        {
            TrainingTypeDAO aTrainingTypeDao = new TrainingTypeDAO()
            {
                TrainingType = trainingTypeTextBox.Text,
                Description = descTextBox.Text,
                Evalution = effecevoRadioButtonList.Items[0].Selected,
                IsActive = true,
                EntryBy = Session["UserId"].ToString(),
                EntryDate = DateTime.Now

            };
            if (effecevoRadioButtonList.Items[0].Selected)
            {
                aTrainingTypeDao.MonthName = monthNameDropDownList.Text;
            }
            else
            {
                aTrainingTypeDao.MonthName = " ";
            }
            int id = aTrainingTypeDal.SaveTrainingType(aTrainingTypeDao);
            if (id>0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successful...');window.location ='TrainingTypeList.aspx';",
                   true);
                Clear();
            }
        }
    }
    public void Update()
    {
        if (Validation())
        {
            TrainingTypeDAO aTrainingTypeDao = new TrainingTypeDAO()
            {
                TrainingTypeID = Convert.ToInt32(trainingTypeHiddenField.Value),
                TrainingType = trainingTypeTextBox.Text,
                Description = descTextBox.Text,
                Evalution = effecevoRadioButtonList.Items[0].Selected,
                IsActive = true,
                Updateby = Session["UserId"].ToString(),
                Updatedate = DateTime.Now

            };
            if (effecevoRadioButtonList.Items[0].Selected)
            {
                aTrainingTypeDao.MonthName = monthNameDropDownList.Text;
            }
            else
            {
                aTrainingTypeDao.MonthName = " ";
            }
            bool st = aTrainingTypeDal.UpdateTrainingtype(aTrainingTypeDao);
            if (st)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successful...');window.location ='TrainingTypeList.aspx';",
                   true);
                Clear();
            }
        }
    }
    protected void submitButton_OnClick(object sender, EventArgs e)
    {
        if (trainingTypeHiddenField.Value ==string.Empty)
        {
            Save();
        }
        
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("TrainingTypeList.aspx");
        Clear();
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("TrainingTypeList.aspx");
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (trainingTypeHiddenField.Value != string.Empty)
         
        {
            Update();
        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
        if (trainingTypeHiddenField.Value != string.Empty)

        {
            TrainingTypeDAO aTypeDao = new TrainingTypeDAO();
            aTypeDao.TrainingTypeID = Convert.ToInt32(trainingTypeHiddenField.Value);
            aTypeDao.DeleteBy = Session["UserId"].ToString();
            aTypeDao.DeleteDate = DateTime.Now;
            aTypeDao.IsDeleted = true;

            bool result = aTrainingTypeDal.DeleteTrainingtype(aTypeDao);

            if (result == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='TrainingTypeList.aspx';",
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