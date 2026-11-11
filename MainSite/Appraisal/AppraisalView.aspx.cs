using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.InternalCls;
using DAL.SuspendAndDiciplinary_Dal;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_AppraisalView : System.Web.UI.Page
{

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();
    private AppraisalPartBDAL _appraisalPartBdal = new AppraisalPartBDAL();
    EmployeeSuspendDal aSuspendDal = new EmployeeSuspendDal();
    private JdDAL _jdDal = new JdDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           

            if (!string.IsNullOrEmpty(Request.QueryString["masterId"]))
            {

                int mid = int.Parse(Request.QueryString["masterId"]);
                id_mastetID.Value = mid.ToString();
                DataTable dt = _appPartA.GetAppraisalSelf(mid);

                txt_employee.Text = dt.Rows[0]["employee"].ToString();
                ddlFinancialYear.SelectedValue = dt.Rows[0]["FinancialYearId"].ToString();


                txt_employee_OnTextChanged(txt_employee, (EventArgs)e);
                DataTable dt2 = _appPartA.GetAppraisalSelfDetails(mid);
                DataTable dt3 = _appPartA.GetAppraisalSelfB(mid);
                gv_AppraisalFunc.DataSource = dt2;
                ViewState["KPIFUNC"] = dt2;
                ViewState["PARTB"] = dt3;
                gv_AppraisalFunc.DataBind();
                gv_AppraisalPartB.DataSource = dt3;
                gv_AppraisalPartB.DataBind();
                CalculateTotal();
                CalculateB();

            }
            DataLoad();
        }
    }

    private void CalculateB()
    {
        decimal weightTotal = 0;

        if (gv_AppraisalPartB.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {
                Label txtWeight = (Label)gv_AppraisalPartB.Rows[i].FindControl("Score");

                if (txtWeight.Text == "")
                {
                    weightTotal = weightTotal + 0;
                }
                else
                {
                    weightTotal = weightTotal + Convert.ToDecimal(txtWeight.Text.ToString());
                }

            }



            Label tst2 = (Label)gv_AppraisalPartB.FooterRow.FindControl("lblTotalScore");
            tst2.Text = weightTotal.ToString();
        }

    }
    protected void CalculateTotal()
    {
        decimal weightTotal = 0;
        decimal markTotal = 0;
        if (gv_AppraisalFunc.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                Label txtWeight = (Label)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");

                Label txtMark = (Label)gv_AppraisalFunc.Rows[i].FindControl("txtMark");


                if (txtWeight.Text == "")
                {
                    weightTotal = weightTotal + 0;
                }
                else
                {
                    weightTotal = weightTotal + Convert.ToDecimal(txtWeight.Text.ToString());
                }
                if (txtMark.Text == "")
                {
                    markTotal = markTotal + 0;
                }
                else
                {
                    markTotal = markTotal + Convert.ToDecimal(txtMark.Text.ToString());
                }




            }

            Label tst = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalWeight");
            tst.Text = weightTotal.ToString();

            Label tst2 = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalMark");
            tst2.Text = markTotal.ToString();
        }
    }
    protected void txt_employee_OnTextChanged(object sender, EventArgs e)
    {
        string empid = txt_employee.Text.Trim();
        if (empid.Contains(":"))
        {
            string[] strsp = empid.Split(':');
            int empId = _trainingDal.GetEmployeeIdByCode(strsp[0]);

            DataTable dtEmp = _appPartA.GetEmployeeDetails(empId);
            if (dtEmp.Rows.Count > 0)
            {
                LoadFinancialYear(Convert.ToInt32(dtEmp.Rows[0]["CompanyId"]));

                comNameLabel.Text = dtEmp.Rows[0]["CompanyName"].ToString().Trim();
                comIdHiddenField.Value = dtEmp.Rows[0]["CompanyId"].ToString().Trim();

                divisionNameLabel.Text = dtEmp.Rows[0]["DivisionName"].ToString().Trim();
                divitionIdHiddenField.Value = dtEmp.Rows[0]["DivisionId"].ToString().Trim();

                divWingNameLabel.Text = dtEmp.Rows[0]["DivisionWingName"].ToString().Trim();
                divWingIdHiddenField.Value = dtEmp.Rows[0]["DivisionWId"].ToString().Trim();


                deptNameLabel.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();
                deptIdHiddenField.Value = dtEmp.Rows[0]["DepartmentId"].ToString().Trim();

                secNameLabel.Text = dtEmp.Rows[0]["SectionName"].ToString().Trim();
                secIdHiddenField.Value = dtEmp.Rows[0]["SectionId"].ToString().Trim();

                subSectionLabel.Text = dtEmp.Rows[0]["SubSectionName"].ToString().Trim();
                subSectionHiddenField.Value = dtEmp.Rows[0]["SubSectionId"].ToString().Trim();

                desigNameLabel.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();
                desigIdHiddenField.Value = dtEmp.Rows[0]["DesignationId"].ToString().Trim();

                joiningDateLabel.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");

                id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();

            }
        }
        else
        {
            txt_employee.Text = "";

            id_Empid.Value = "";
            aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        }
        
    }
    private void LoadFinancialYear(int comp)
    {
        DataTable dt = _trainingDal.GetFianncialYearByComIdDDl(comp);
        ddlFinancialYear.DataSource = dt;
        ddlFinancialYear.DataValueField = "Value";
        ddlFinancialYear.DataTextField = "TextField";
        ddlFinancialYear.DataBind();
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AppraisalSupApprove.aspx");
    }

    protected void btn_Review_OnClick(object sender, EventArgs e)
    {
        bool result = _appPartA.GoForReview(Convert.ToInt32(id_mastetID.Value));
        if (result == true)
        {
            AlertMessageBoxShow("Operation Successfull");
            Response.Redirect("AppraisalSupApprove.aspx");
        }
        else
        {
            AlertMessageBoxShow("Operation Failed");
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
    public void DataLoad()
    {
        ClsApprovalAction approvalAction = new ClsApprovalAction();

        string userName = Session["UserId"].ToString();

        approvalAction.LoadActionControlByUser(actionRadioButtonList, Session["ApprovalPage"].ToString(), userName);
        Session["ApprovalPage"] = null;
        RadItemRemove();
        //submitButton.Text = "Submit";

    }

    public void RadItemRemove()
    {
        int[] id = new int[5];
        int count = 0;
        for (int i = 0; i < actionRadioButtonList.Items.Count; i++)
        {

            if (actionRadioButtonList.Items[i].Enabled == false)
            {
                id[count] = Convert.ToInt32(actionRadioButtonList.Items[i].Value);
                count++;

            }
        }
        foreach (int a in id)
        {
            for (int i = 0; i < actionRadioButtonList.Items.Count; i++)
            {

                if (actionRadioButtonList.Items[i].Value == a.ToString())
                {

                    actionRadioButtonList.Items.RemoveAt(i);
                }
            }
        }
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        try
        {
            _appPartA.UpdateStatus(id_mastetID.Value,
                    actionRadioButtonList.SelectedItem.Text, Session["UserId"].ToString(), DateTime.Now);
            //DataLoad();
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('" + actionRadioButtonList.SelectedItem.Text + " Successfully Done');window.location ='AppraisalSupApprove.aspx';",
                    true);
            //aShowMessage.ShowMessageBox("" + jobreqRadioButtonList.SelectedItem.Text + " Successfully Done", this);
        }
        catch (Exception ex)
        {
            aShowMessage.ShowMessageBox("Please Choose an action for approval", this);
        }
        //bool result = _appPartA.ApproveSup(Convert.ToInt32(id_mastetID.Value));
        //if (result == true)
        //{
        //    AlertMessageBoxShow("Operation Successfull");
        //    Response.Redirect("AppraisalSupApprove.aspx");
        //}
        //else
        //{
        //    AlertMessageBoxShow("Operation Failed");
        //}
    }
}