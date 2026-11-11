using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_JobDescription : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();
    private JdDesigDAL _JdDesigDAL = new JdDesigDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {  Session["EmpOption"] = "Employee";
        if (!IsPostBack)
        {
            DropDownList();
            LoadInitialDDL();
            IniGrid();
            if (!string.IsNullOrEmpty(Request.QueryString["masterId"]))
            {
                int mid = int.Parse(Request.QueryString["masterId"]);
                masterId.Value = mid.ToString();

                DataTable dt = _JdDesigDAL.GetJdDesigByMaster(mid);
                //empInfoId.Value = dt.Rows[0]["EmpInfoId"].ToString();
                ddlCompany.SelectedValue = dt.Rows[0]["CompanyId"].ToString();
                ddlCompany_OnSelectedIndexChanged(ddlCompany, (EventArgs) e);
                ddlFinancialYear.SelectedValue = dt.Rows[0]["FinancialYearId"].ToString();
                //txt_employee.Text = dt.Rows[0]["employee"].ToString();
                //txt_employee_OnTextChanged(txt_employee, (EventArgs) e);
                txtJobSummary.Text = dt.Rows[0]["JdSummary"].ToString();
                DataTable dt2 = _JdDesigDAL.GetJdDesigDetails(Convert.ToInt32(mid));
                gv_JdDetails.DataSource = dt2;
                gv_JdDetails.DataBind();
                _JdDesigDAL.DivDropDown(divDropDownList, ddlCompany.SelectedValue);
                desigDropDownList.SelectedValue = dt.Rows[0]["DesignationId"].ToString();
                divDropDownList.SelectedValue = dt.Rows[0]["DivisionId"].ToString();
                jobLocDropDownList.SelectedValue = dt.Rows[0]["JobLocationId"].ToString();
                reportToDropDownList.SelectedValue = dt.Rows[0]["ReportToId"].ToString();
                directSuperDropDownList.SelectedValue = dt.Rows[0]["DirectSuperId"].ToString();
                interContDropDownList.SelectedValue = dt.Rows[0]["InterContId"].ToString();
                extContDropDownList.SelectedValue = dt.Rows[0]["ExterContId"].ToString();
                educationTextBox.Text = dt.Rows[0]["Education"].ToString();
                relexpTextBox.Text = dt.Rows[0]["RelExp"].ToString();
                specialSkillTextBox.Text = dt.Rows[0]["SpecialSkill"].ToString();
                otherReqTextBox.Text = dt.Rows[0]["OtherReq"].ToString();
                compSkillTextBox.Text = dt.Rows[0]["CompSkill"].ToString();
                
            }
            
        }
       
    }

    public void DropDownList()
    {
        _JdDesigDAL.DesignationDropDown(desigDropDownList);
        _JdDesigDAL.DesignationDropDown(reportToDropDownList);
        _JdDesigDAL.DesignationDropDown(directSuperDropDownList);
        _JdDesigDAL.DesignationDropDown(interContDropDownList);
        _JdDesigDAL.DesignationDropDown(extContDropDownList);
        //_JdDesigDAL.DivDropDown(divDropDownList);
        _JdDesigDAL.JobLocationDropDown(jobLocDropDownList);
        
    }

    private void LoadInitialDDL()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {

            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
        }
    }


    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
       string comp= ddlCompany.SelectedValue;
        Session["CompanyId"] = comp;
        _JdDesigDAL.DivDropDown(divDropDownList,ddlCompany.SelectedValue);
        DataTable dt = _trainingDal.GetFianncialYearByComIdDDl(Convert.ToInt32(ddlCompany.SelectedValue));
        ddlFinancialYear.DataSource = dt;
        ddlFinancialYear.DataValueField = "Value";
        ddlFinancialYear.DataTextField = "TextField";
        ddlFinancialYear.DataBind();
    }

    protected void txt_employee_OnTextChanged(object sender, EventArgs e)
    {
        //string id = txt_employee.Text.Trim().Substring(0, 6);
        //int empId = _trainingDal.GetEmployeeIdByCode(id);
        //DataTable dtEmp = _appPartA.GetEmployeeDetails(empId);
        //if (dtEmp.Rows.Count > 0)
        //{
        //    //LoadFinancialYear(Convert.ToInt32(dtEmp.Rows[0]["CompanyId"]));

        //    lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString();
        //    lblEMpName.Text = dtEmp.Rows[0]["EmpName"].ToString();
        //    lblDepartment.Text = dtEmp.Rows[0]["DepartmentName"].ToString();
        //    lblDesignation.Text = dtEmp.Rows[0]["Designation"].ToString();
        //    lblDoj.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"].ToString()).ToString("dd-MMM-yyyy");
        //    empInfoId.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();
            
        //}
    }

    private void IniGrid()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("JdDetailsInfo", typeof(string)));
        dr = dt.NewRow();

        dr["JdDetailsInfo"] = "";
        ViewState["JdDetailsInfo"] = dt;
        dt.Rows.Add(dr);
        gv_JdDetails.DataSource = dt;
        gv_JdDetails.DataBind();
    }

    protected void btn_Add_OnClick(object sender, EventArgs e)
    {
        if (ViewState["JdDetailsInfo"] == null)
        {

            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("JdDetailsInfo", typeof(string)));
            dr = dt.NewRow();

            dr["JdDetailsInfo"] = "";
            ViewState["JdDetailsInfo"] = dt;
            dt.Rows.Add(dr);
            gv_JdDetails.DataSource = dt;
            gv_JdDetails.DataBind();
        }
        else
        {
            DataTable dtCurrentTable = (DataTable)ViewState["JdDetailsInfo"];

            DataRow drCurrentRow = null;

            drCurrentRow = dtCurrentTable.NewRow();



            dtCurrentTable.Rows.Add(drCurrentRow);


            ViewState["JdDetailsInfo"] = dtCurrentTable;
            for (int i = 0; i < gv_JdDetails.Rows.Count; i++)
            {
                TextBox tbKpi = (TextBox)gv_JdDetails.Rows[i].FindControl("txtJdDetails");
                dtCurrentTable.Rows[i]["JdDetailsInfo"] = tbKpi.Text.Trim().ToString() == "" ? "" : tbKpi.Text.Trim().ToString();
            }

            gv_JdDetails.DataSource = dtCurrentTable;
            gv_JdDetails.DataBind();

        }
    }

    protected void lb_Remove_OnClick(object sender, EventArgs e)
    {
        if (ViewState["JdDetailsInfo"] != null && gv_JdDetails.Rows.Count > 1)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["JdDetailsInfo"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count == 0)
            {
                ViewState["JdDetailsInfo"] = null;
            }
            else
            {
                ViewState["JdDetailsInfo"] = dt;
            }


            gv_JdDetails.DataSource = dt;
            gv_JdDetails.DataBind();
            // CalculateTotalParticipant();
        }
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("JobDescription.aspx");
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            JdDesigMaster aMaster = new JdDesigMaster();
            aMaster.JdDesigMasterId = masterId.Value == "" ? 0 : Convert.ToInt32(masterId.Value);
            aMaster.Designationid = Convert.ToInt32(desigDropDownList.SelectedValue);
            aMaster.CompanyId= Convert.ToInt32(ddlCompany.SelectedValue);
            aMaster.DivisionId= Convert.ToInt32(divDropDownList.SelectedValue);
            aMaster.JobLocationId= Convert.ToInt32(jobLocDropDownList.SelectedValue);
            aMaster.ReportToId = Convert.ToInt32(reportToDropDownList.SelectedValue);
            aMaster.DirectSuperId= Convert.ToInt32(directSuperDropDownList.SelectedValue);
            aMaster.InterContId= Convert.ToInt32(interContDropDownList.SelectedValue);
            aMaster.ExterContId= Convert.ToInt32(extContDropDownList.SelectedValue);
            aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);
            aMaster.JdSummary = txtJobSummary.Text.Trim();
            aMaster.Education = educationTextBox.Text.Trim();
            aMaster.RelExp = relexpTextBox.Text.Trim();
            aMaster.SpecialSkill= specialSkillTextBox.Text.Trim();
            aMaster.OtherReq= otherReqTextBox.Text.Trim();
            aMaster.CompSkill= compSkillTextBox.Text.Trim();

            int pk = _JdDesigDAL.SaveJdDesigMaster(aMaster, Session["LoginName"].ToString());
           
            ///masterId.Value
            bool result = false;
            if (pk > 0)
            {
                List<JdDesigDetails> aList = new List<JdDesigDetails>();

                for (int i = 0; i < gv_JdDetails.Rows.Count; i++)
                {
                    TextBox aBox = (TextBox)gv_JdDetails.Rows[i].FindControl("txtJdDetails");

                    if (aBox.Text.Trim() != "")
                    {
                        JdDesigDetails aDetails = new JdDesigDetails();
                        aDetails.JdDetailsInfo = aBox.Text.Trim();
                        aDetails.JdDesigMasterId = pk;
                        aList.Add(aDetails);

                    }
                }

                if (aList.Count > 0)
                {
                    result = _JdDesigDAL.SaveJdDesigDetails(aList, pk);
                    if (result == true)
                    {
                        AlertMessageBoxShow("Operation Successful...");
                        Response.Redirect("JobDescriptionDesig.aspx");
                    }
                    else
                    {
                        AlertMessageBoxShow("Operation Failed...");

                    }
                }
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
    private bool Validation()
    {
        if (ddlFinancialYear.SelectedValue == "" || ddlFinancialYear.SelectedValue == "-1")
        {
            aShowMessage.ShowMessageBox("Financial Year Required ", this);
            return false;
        }
        if (desigDropDownList.SelectedIndex ==0)
        {
            aShowMessage.ShowMessageBox("Give Job Title", this);
            return false;
        }
        if (divDropDownList.SelectedIndex == 0)
        {
            aShowMessage.ShowMessageBox("Give Division", this);
            return false;
        }
        if (jobLocDropDownList.SelectedIndex == 0)
        {
            aShowMessage.ShowMessageBox("Give Job Location", this);
            return false;
        }
        if (reportToDropDownList.SelectedIndex == 0)
        {
            aShowMessage.ShowMessageBox("Give Report To", this);
            return false;
        }
        if (directSuperDropDownList.SelectedIndex == 0)
        {
            aShowMessage.ShowMessageBox("Give Directly Supervisor", this);
            return false;
        }
        if (interContDropDownList.SelectedIndex == 0)
        {
            aShowMessage.ShowMessageBox("Give Internal Contacts", this);
            return false;
        }
        if (extContDropDownList.SelectedIndex == 0)
        {
            aShowMessage.ShowMessageBox("Give External Contacts", this);
            return false;
        }

        if (txtJobSummary.Text == "")
        {
            aShowMessage.ShowMessageBox("Summary Required ", this);
            return false;
        }
        if (gv_JdDetails.Rows.Count == 0)
        {
            aShowMessage.ShowMessageBox("Job Description Required ", this);
            return false;
        }

        if (gv_JdDetails.Rows.Count > 0)
        {
            for (int i = 0; i < gv_JdDetails.Rows.Count; i++)
            {
                TextBox tbKpi = (TextBox)gv_JdDetails.Rows[i].FindControl("txtJdDetails");
                if (tbKpi.Text.Trim() == "")
                {
                    aShowMessage.ShowMessageBox("Job Description Required ", this);
                    return false;
                }
            }
        }
        return true;
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("JdDesigList.aspx");
    }

    protected void desigDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = _JdDesigDAL.GetSalGrade(Convert.ToInt32(desigDropDownList.SelectedValue));
        if (dt.Rows.Count>0)
        {
            lblSalGrade.Text = dt.Rows[0]["GradeName"].ToString();
        }
    }
}