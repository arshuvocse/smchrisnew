using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Increment_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Increment_UI_ICSignaturePerson : System.Web.UI.Page
{

    readonly ICSignaturePersonDAL aIncrementDal = new ICSignaturePersonDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonVisible();
            LoadDropDownList();

            
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

            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("ICSignaturePersonView.aspx");
        }

    }
    private void LoadDropDownList()
    {
        try
        {

            aIncrementDal.LoadCompany(ddlCompany);
            ddlCompany.SelectedIndex = 1;
            ddlCompany_SelectedIndexChanged(null, null);
            PopulatePartInformation();

        }
        catch (Exception)
        {

        }



    }
    private void LoadEmployeedataData(int id)
    {
        DataTable dtdata = new DataTable();
        dtdata = aIncrementDal.LoadEmpAllInfofById(id);
        if (dtdata.Rows.Count > 0)
        {

            lblEmp.Text = dtdata.Rows[0]["EmpName"].ToString();


            lblEmployeeCode.Text = dtdata.Rows[0]["EmpMasterCode"].ToString();
            
            lblDesignation.Text = dtdata.Rows[0]["Designation"].ToString();

        


        }
    }

    protected void SearchEmployeeNameTextBoxTextBox_OnTextChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            string empName = SearchEmployeeNameTextBoxTextBox.Text.Trim();

            if (empName.Contains(':'))
            {
                string[] emp = empName.Split(':');

                SearchEmployeeNameTextBoxTextBox.Text = emp[2];
                repEmpIdHiddenField.Value = emp[0];
                LoadEmployeedataData(Convert.ToInt32(repEmpIdHiddenField.Value));
                //productNameTextBox.Text = productInfo[1];
                //string productCode = productCodeTextBox.Text.Trim();

            }
            else
            {

                SearchEmployeeNameTextBoxTextBox.Text = "";
                repEmpIdHiddenField.Value = "";
                aShowMessage.ShowMessageBox("Input Correct Data !!", this);
            }

        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select a Company !!", this);
            SearchEmployeeNameTextBoxTextBox.Text = "";
            repEmpIdHiddenField.Value = "";
            ddlCompany.Focus();
        }
    }

    private void PopulatePartInformation()
    {
        DataTable dtgrade = aIncrementDal.GetGrade();
        CHKGradeList.DataValueField = "SalaryGradeId";
        CHKGradeList.DataTextField = "GradeCode";
        CHKGradeList.DataSource = dtgrade;
        CHKGradeList.DataBind();
    }

    protected void CHKPartCheck_OnCheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < CHKGradeList.Items.Count; i++)
        {
            if (CHKPartCheck.Checked)
            {
                CHKGradeList.Items[i].Selected = true;
            }
            else
            {
                CHKGradeList.Items[i].Selected = false;
            }
        }
    }
    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ICSignaturePersonView.aspx");
    }
    
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            Session["CompanyId"] = ddlCompany.SelectedValue;
        }

    }


    private bool Validation()
    {

        if (ddlCompany.SelectedValue == "")
        {
            showMessageBox("Please fill out this field !!!");
            ddlCompany.Focus();
            return false;
        }




        if (repEmpIdHiddenField.Value == "")
        {
            showMessageBox("Please fill out this field !!!");
            SearchEmployeeNameTextBoxTextBox.Focus();
            return false;
        }



        if (GradeParam() == string.Empty)
        {
            showMessageBox("Please check at least one Grade!!!");
             
            return false;
        }




        return true;
    }


    public string GradeParam()
    {
        string param = "";
        string grade = "";

        for (int i = 0; i < CHKGradeList.Items.Count; i++)
        {
            if (CHKGradeList.Items[i].Selected)
            {
                grade = CHKGradeList.Items[i].Value + "," + grade;
            }
        }
        if (grade != string.Empty)
        {
            param = param + " AND EG.SalaryGradeId " + CHKGradeList.SelectedItem.Text + " (" + grade.TrimEnd(',') +
                    ")";
        }
        else
        {
            param = "";
        }
        return param;

    }


    protected void submitButton_Click(object sender, EventArgs e)
    {

        if (Validation())
        {

            int IDD = 0;
            for (int i = 0; i < CHKGradeList.Items.Count; i++)
            {
                if (CHKGradeList.Items[i].Selected)
                {
                    DataTable aDataTable = aIncrementDal.ChexkLoadIncrementInfo(ddlCompany.SelectedValue, CHKGradeList.Items[i].Value);
                    if (aDataTable.Rows.Count > 0)
                    {

                      

                    }
                    else
                    {
                        IDD = aIncrementDal.SavetoMultiAccount(Convert.ToInt32(ddlCompany.SelectedValue),
                          Convert.ToInt32(CHKGradeList.Items[i].Value), Convert.ToInt32(repEmpIdHiddenField.Value));
                    }


                }
            }

            if (IDD>0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
              "alert",
              "alert('Opearation Successfully Done...');window.location ='ICSignaturePersonView.aspx';",
              true);
            }
            else
            {
                showMessageBox("Already Exist!!");
            }
           
        }
        
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
       
    }
}