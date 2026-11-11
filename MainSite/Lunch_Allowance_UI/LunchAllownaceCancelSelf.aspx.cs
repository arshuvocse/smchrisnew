using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Antlr.Runtime.Misc;
using DAL.COMMON_DAL;
using DAL.Lunch_Allowance_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Lunch_Allowance_UI_LunchAllownaceCancelSelf : System.Web.UI.Page
{
    LunchAllowanceCancelDAL allowanceCancelDal=new LunchAllowanceCancelDAL();
    ShowMessage aShowMessage = new ShowMessage();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime dta = DateTime.Now.AddDays(1);
            effectiveDateTextBox.Text = dta.ToString("dd-MMM-yyyy");

            CalendarExtender1.StartDate = DateTime.Now.AddDays(1);
            //startDate="<%# DateTime.Now.AddDays(1) %>" EndDate="<%# DateTime.Now.AddDays(30) %>"

            DropDownList();
            Button1_OnClick(null, null);
        }
    }
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    public void DropDownList()
    {
        allowanceCancelDal.LoadCompany(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_OnSelectedIndexChanged(null, null);

        using (DataTable dt = _commonDataLoad.GetDDLDesignationAll())
        {
            ddlDesignation.DataSource = dt;
            ddlDesignation.DataValueField = "Value";
            ddlDesignation.DataTextField = "TextField";

            ddlDesignation.DataBind();

            ddlDesignation.SelectedValue = "Please Select One..";
        }
    }

    protected void Button1_OnClick(object sender, EventArgs e)
    {

        if (validfa())
        {
            
     
        DataTable dtdata = allowanceCancelDal.GetLunchAllowEmp(effectiveDateTextBox.Text, GenerateParameter());
        

        if (dtdata.Rows.Count>0)
        {
       //int dateCount=     (int) dtdata.Rows[0]["DateDiffer"];

       DateTime Start = Convert.ToDateTime(effectiveDateTextBox.Text);
       DateTime End = Convert.ToDateTime(dtdata.Rows[0]["ToDateNew"]);
       DataTable dtdata22 = allowanceCancelDal.GetLunchAllowEmpNew(Start, End, GenerateParameter());




       loadGridView.DataSource = dtdata22;
       loadGridView.DataBind();
                for (int i = 0; i < loadGridView.Rows.Count; i++)
                {

                    LinkButton lbDraft = (LinkButton)loadGridView.Rows[i].FindControl("lbDraft");
                    LinkButton lbSubmit = (LinkButton)loadGridView.Rows[i].FindControl("lbSubmit");
                    Label lblDateData =
           ((Label)loadGridView.Rows[i].Cells[1].FindControl("lblDateData"));

                    Label lbMsg =
            ((Label)loadGridView.Rows[i].Cells[1].FindControl("lbMsg"));
                         Label lblActionStatus =
               ((Label)loadGridView.Rows[i].Cells[1].FindControl("lblActionStatus"));

                TextBox txtRemarks =
      ((TextBox)loadGridView.Rows[i].Cells[1].FindControl("txtRemarks"));
                DataTable dtdatas = allowanceCancelDal.GetLunchAllowCancel(lblDateData.Text,
                        loadGridView.DataKeys[i][0].ToString());
                    if (dtdatas.Rows.Count > 0)
                    {


                        lblActionStatus.Text = dtdatas.Rows[0]["ActionStatus"].ToString();
                        txtRemarks.Text = dtdatas.Rows[0]["Remarks"].ToString();

                        if (lblActionStatus.Text == "Submitted")
                        {

                            lbDraft.Visible = false;
                            lbSubmit.Visible = false;
                            lbMsg.Text = "Waiting for Approver";
                            lbMsg.CssClass = "bg-warning";
                        }


                        if ( lblActionStatus.Text == "Canceled")
                        {

                            lbDraft.Visible = false;
                            lbSubmit.Visible = false;
                            lbMsg.Text = "Canceled";
                            lbMsg.CssClass = "bg-warning";
                        }
                    }
                }
            }
    
        }
    }

    private bool validfa()
    {
        if ((companyDropDownList.SelectedValue == "0") )
        {
            AlertMessageBoxShow("Please Select company...");
            companyDropDownList.Focus();
            return false;


        }


        if ((effectiveDateTextBox.Text == ""))
        {
            AlertMessageBoxShow("Please Select effective Date...");
            effectiveDateTextBox.Focus();
            return false;


        }

       

        return true;
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DraftData")
        {
          
            int rowindex = Convert.ToInt32(e.CommandArgument);

            DateTime timenow = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString());
            DateTime ExcaTime = Convert.ToDateTime(DateTime.Now.ToString("15:00:0"));

            Label lblDateData =
                ((Label)loadGridView.Rows[rowindex].Cells[1].FindControl("lblDateData"));

            DateTime exxx = ExcaTime.AddDays(1);
            string tttt = timenow.ToString("dd-MMM-yyyy");

            if (lblDateData.Text.ToString() == tttt)
            {
                DateTime SubmitTime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString());

                DataTable dtdata = allowanceCancelDal.GetLunchAllowEmpCheckSecond(SubmitTime, exxx);

                int countt = 0;
                if ( dtdata.Rows.Count>0)
                {
                    countt = Convert.ToInt32(dtdata.Rows[0]["CoutSecend"].ToString());
                }
                if (countt>0)
                {
                    bool save = false;
                    SaveMethod(rowindex, "Draft", save);
                    
                }
                else
                {
                    AlertMessageBoxShow("Please Submit before 3:00PM ");
                    
                }
               
            }
            else
            {
                bool save = false;
                SaveMethod(rowindex, "Draft", save);
            }

            //HiddenField hfActionStatus =
            //  ((HiddenField)loadGridView.Rows[rowindex].Cells[1].FindControl("hfActionStatus"));

           
        }


        if (e.CommandName == "SubmitData")
        {

            int rowindex = Convert.ToInt32(e.CommandArgument);

            HiddenField hfEmpInfoId =
                ((HiddenField)loadGridView.Rows[rowindex].Cells[1].FindControl("hfEmpInfoId"));

            DateTime timenow = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString());
            DateTime ExcaTime = Convert.ToDateTime(DateTime.Now.ToString("15:00:0"));

            Label lblDateData =
                ((Label)loadGridView.Rows[rowindex].Cells[1].FindControl("lblDateData"));

            DateTime exxx = ExcaTime.AddDays(1);
            string tttt = timenow.ToString("dd-MMM-yyyy");
           
            if (lblDateData.Text.ToString() == tttt)
            {
                DateTime SubmitTime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString());

                DataTable dtdata = allowanceCancelDal.GetLunchAllowEmpCheckSecond(SubmitTime, exxx);

                int countt = 0;
                if (dtdata.Rows.Count > 0)
                {
                    countt = Convert.ToInt32(dtdata.Rows[0]["CoutSecend"].ToString());
                }
                if (countt > 0)
                {
                    bool save = false;
                    SaveMethod(rowindex, "Submitted", save);

                }
                else
                {
                    AlertMessageBoxShow("Please Submit before 3:00PM ");

                }

            }
            else
            {



                bool save = false;
                SaveMethod(rowindex, "Submitted", save);
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

    private string GenerateParameter()
    {
        string parameter = " ";

        //if (companyDropDownList.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    e.CompanyId = '" + companyDropDownList.SelectedValue + "'";
        //}

        //if (ddlDivision.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    e.DivisionId = '" + ddlDivision.SelectedValue + "'";
        //}

        //if (ddlDepartment.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    e.DepartmentId = '" + ddlDepartment.SelectedValue + "'";
        //}

        //if (ddlSection.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    e.SectionId = '" + ddlSection.SelectedValue + "'";
        //}

        //if (ddlSubSection.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    e.SubSectionId = '" + ddlSubSection.SelectedValue + "'";
        //}

        //if (txtSearch.Text != "")
        //{
        //    parameter = parameter + "  and (e.EmpMasterCode LIKE     '%" + txtSearch.Text.Trim() + "%' ) ";
        //}

        //if (NameTextBox.Text != "")
        //{
        //    parameter = parameter + "  and  ( e.EmpName LIKE '%" + NameTextBox.Text.Trim() + "%')";
        //}

        //if (ddlDesignation.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    e.DesignationId = '" + ddlDesignation.SelectedValue + "'";
        //}

        //if (ddlSalaryLocation.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    e.SalaryLoationId = '" + ddlSalaryLocation.SelectedValue + "'";
        //}

        //if (ddlConformationStatus.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    e.ConformationStatus = '" + ddlConformationStatus.SelectedValue + "'";
        //}

        //if (ActiveStatusDropDownList.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    e.IsActive = '" + ActiveStatusDropDownList.SelectedValue + "'";
        //}


        parameter = parameter + "  and tblLunchAllowDetails.EmployeeId= '" + Session["EmpInfoId"].ToString() + "'";

        return parameter;
    }


    protected void submitButton_OnClick(object sender, EventArgs e)
    {



        if (Valisddate())
        {




            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
             //   SaveMethod(i);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successful...! ');window.location ='LunchAllownaceCancelSelf.aspx';",
                true);

        }
    }

    private void SaveMethod(int i, string ActionStatus, bool save)
    {
        
            LunchAllownceCancelDAO allownceCancelDao = new LunchAllownceCancelDAO();
            allownceCancelDao.EmpInfoId = Convert.ToInt32(loadGridView.DataKeys[i][0].ToString());
            DataTable dtdata = allowanceCancelDal.GetEmpInfo(allownceCancelDao.EmpInfoId.ToString());
            if (dtdata.Rows.Count > 0)
            {
                allownceCancelDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
                try
                {
                    allownceCancelDao.DepartmentId = Convert.ToInt32(dtdata.Rows[0]["DepartmentId"].ToString());
                }
                catch (Exception)
                {
                    allownceCancelDao.DepartmentId = null;
                    //throw;
                }
                try
                {
                    allownceCancelDao.DivisionId = Convert.ToInt32(dtdata.Rows[0]["DivisionId"].ToString());
                }
                catch (Exception)
                {
                    allownceCancelDao.DivisionId = null;
                    //throw;
                }
                Label lblDateData =
           ((Label)loadGridView.Rows[i].Cells[1].FindControl("lblDateData"));

                TextBox txtRemarks =
      ((TextBox)loadGridView.Rows[i].Cells[1].FindControl("txtRemarks"));
                allownceCancelDao.EffectiveDate = Convert.ToDateTime(lblDateData.Text);
                allownceCancelDao.Remarks = txtRemarks.Text;
                int divWing = 0;


                try
                {
                    if (dtdata.Rows[0]["DivisionWId"] != null)
                    {
                        divWing = Convert.ToInt32(dtdata.Rows[0]["DivisionWId"].ToString());
                    }
                    allownceCancelDao.DivisionWId = //Convert.ToInt32(dtdata.Rows[0]["DivisionWId"].ToString());
                        divWing > 0 ? int.Parse(divWing.ToString()) : (int?) null;
                }
                catch (Exception)
                {
                    allownceCancelDao.DivisionWId = null;
                }

                try
                {
                    int subSec = Convert.ToInt32(dtdata.Rows[0]["SubSectionId"].ToString());

                    allownceCancelDao.SubSectionId =
                        allownceCancelDao.DivisionWId =
                            //Convert.ToInt32(dtdata.Rows[0]["DivisionWId"].ToString());
                            subSec > 0 ? int.Parse(subSec.ToString()) : (int?) null;
                }
                catch (Exception)
                {
                    allownceCancelDao.SubSectionId = null;
                    //throw;
                }

                try
                {
                    int secId = Convert.ToInt32(dtdata.Rows[0]["SectionId"].ToString());
                    allownceCancelDao.SectionId = secId > 0 ? int.Parse(secId.ToString()) : (int?) null;
                }
                catch (Exception)
                {
                    allownceCancelDao.SectionId = null;
                    //throw;
                }
                allownceCancelDao.LunchAlllowCancelId =
                    allownceCancelDao.EmpInfoId = Convert.ToInt32(loadGridView.DataKeys[i][0].ToString());

                try
                {
                    allowanceCancelDal.DeleteLunchAllowCancel(allownceCancelDao);
                }
                catch (Exception)
                {
                    //throw;
                }

                allownceCancelDao.ActionStatus = ActionStatus;
                allowanceCancelDal.SaveLunchAllowCancel(allownceCancelDao);
                save = true;
                if (save)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
              "alert",
              "alert('Operation Successful...! ');window.location ='LunchAllownaceCancelSelf.aspx';",
              true);
                }
            }
         
    }

    private bool Valisddate()
    {

        DateTime timenow = Convert.ToDateTime(DateTime.Now.ToString("hh:mm:ss"));
        DateTime ExcaTime = Convert.ToDateTime(DateTime.Now.ToString("15:00:0"));


        if (timenow > ExcaTime)
        {
           

        }
        else
        {

            AlertMessageBoxShow("Please Submit before 3:00PM ");
            return true;


        }

        if (loadGridView.Rows.Count == 0)
        {
            AlertMessageBoxShow("Table Can not be Empty!!");
            return true;
        }


        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            CheckBox chk = ((CheckBox)loadGridView.Rows[i].FindControl("inactiveCheckBox"));
            if (chk.Checked == false)
            {
                AlertMessageBoxShow("You already   cancel for this effective date...");
                effectiveDateTextBox.Focus();
                
                return false;
            }
        }
        //if ()
        //{
        //    AlertMessageBoxShow("Please Select Year...");
        //    txtToDate.Focus();
        //    return false;


        //}

        return true;
    }

    protected void Button11_OnClick(object sender, EventArgs e)
    {
        
    }


    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue != "")
        {
            _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
            _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
            _commonDataLoad.GetSectionByDivList(ddlSection, ddlDivision.SelectedValue);
            _commonDataLoad.GetSubSectionListAll(ddlSubSection, ddlDivision.SelectedValue);
        }
        else
        {
            ddlWing.Items.Clear();
            ddlDepartment.Items.Clear();
            ddlSection.Items.Clear();
            ddlSubSection.Items.Clear();
        }
    }

    protected void ddlWing_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
      
    }

    protected void ddlSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlSubSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void EmployeeDropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

        string empName = txtSearch.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            //EmployeeDropDownList.Text = emp[0];
            txtSearch.Text = emp[1];
        }
        //else
        //{
        //    txtSearch.Text = "";
        //    txtSearch.Text = "";
        //    //  EmpInfoIdHiddenField.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}
    }

    protected void EmployeeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string empName = NameTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            //EmployeeDropDownList.Text = emp[0];
            NameTextBox.Text = emp[2];

        }
        //else
        //{
        //    NameTextBox.Text = "";
        //    NameTextBox.Text = "";
        //  //  EmpInfoIdHiddenField.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}

    }

    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("LunchAllownaceCancel.aspx");
    }
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedIndex > 0)
        {
            Session["CompanyId"] = "";
            Session["CompanyId"] = companyDropDownList.SelectedValue;
            using (DataTable dt = _commonDataLoad.GetDDLComDivision(companyDropDownList.SelectedValue))
            {
                ddlDivision.DataSource = dt;
                ddlDivision.DataValueField = "Value";
                ddlDivision.DataTextField = "TextField";
                ddlDivision.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLComWind(companyDropDownList.SelectedValue))
            {
                ddlWing.DataSource = dt;
                ddlWing.DataValueField = "Value";
                ddlWing.DataTextField = "TextField";
                ddlWing.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComDepartment(companyDropDownList.SelectedValue))
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataValueField = "Value";
                ddlDepartment.DataTextField = "TextField";
                ddlDepartment.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComSection(companyDropDownList.SelectedValue))
            {
                ddlSection.DataSource = dt;
                ddlSection.DataValueField = "Value";
                ddlSection.DataTextField = "TextField";
                ddlSection.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComSubSection(companyDropDownList.SelectedValue))
            {
                ddlSubSection.DataSource = dt;
                ddlSubSection.DataValueField = "Value";
                ddlSubSection.DataTextField = "TextField";
                ddlSubSection.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLSalaryLocation())
            {
                ddlSalaryLocation.DataSource = dt;
                ddlSalaryLocation.DataValueField = "Value";
                ddlSalaryLocation.DataTextField = "TextField";
                ddlSalaryLocation.DataBind();
            }
        }
        else
        {
            ddlDivision.Items.Clear();
            ddlWing.Items.Clear();
                ddlDepartment.Items.Clear();
                ddlSection.Items.Clear();
                    ddlSubSection.Items.Clear();
                    ddlSalaryLocation.Items.Clear();
        }
    }

    protected void effectiveDateTextBox_OnTextChanged(object sender, EventArgs e)
    {
        Button1_OnClick(null, null);
    }
}