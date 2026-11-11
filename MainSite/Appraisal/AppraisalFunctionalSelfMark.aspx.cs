using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_AppraisalFunctionalSelfMark : System.Web.UI.Page
{
    private TrainingDAL _trainingDal = new TrainingDAL();
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int masterID = int.Parse(Request.QueryString["masterId"]); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
            int empInfoId = int.Parse(Request.QueryString["empInfoId"]);
            int selfMaster = int.Parse(Request.QueryString["selfMaster"]);

            DataTable dt = _appPartA.GetAppraisalSelf(Convert.ToInt32(selfMaster));
            DataTable dtw = _appPartA.GetAppraisalSelfDetailsNew(Convert.ToInt32(selfMaster));
            DataTable dtw2 = _appPartA.GetAppraisalfDetailsFromSup(Convert.ToInt32(selfMaster));
            id_selfID.Value = selfMaster.ToString();

            if (dtw2.Rows.Count > 0)
            {
                gv_AppraisalFunc.DataSource = dtw2;
                gv_AppraisalFunc.DataBind();
                ViewState["KPIFUNC"] = dtw2;
            }
            else
            {
                gv_AppraisalFunc.DataSource = dtw;
                gv_AppraisalFunc.DataBind();
                ViewState["KPIFUNC"] = dtw;
            }

            id_mastetID.Value = masterID.ToString();
            id_Empid.Value = empInfoId.ToString();
            CalculateB();
            if (empInfoId > 0)
            {
                DataTable dtEmp = _appPartA.GetEmployeeDetails(empInfoId);
                if (dtEmp.Rows.Count > 0)
                {
                     

                    lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();
                    LoadFinancialYear(Convert.ToInt32(dtEmp.Rows[0]["CompanyId"]));
                    ddlFinancialYear.SelectedValue = dt.Rows[0]["FinancialYearId"].ToString();
                    empName.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();
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


                    LocationLabel.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
                    lblPlace.Text = dtEmp.Rows[0]["Location"].ToString();
                    ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();



                    id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();
                    CalculateB();
                    // IniKpiTable();
                }

                for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
                {
                    DropDownList txtMidStatus = (DropDownList)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
                    for (int j = 0; j < txtMidStatus.Items.Count; j++)
                    {
                        if (txtMidStatus.Items[j].Text==gv_AppraisalFunc.DataKeys[i][0].ToString())
                        {
                            txtMidStatus.SelectedIndex = j;
                        }
                    }
                }
            }
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

    private void CalculateB()
    {
        decimal weightTotal = 0;
        decimal markTotal = 0;

        if (gv_AppraisalFunc.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {

                TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtselfMark");
            //    TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");
                Label txtWeight = (Label)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");




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



            Label tst2 = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalMark");
            tst2.Text = markTotal.ToString();

            Label tst1 = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalWeight");
            tst1.Text = weightTotal.ToString();
        }

    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (Validation() == true)
        {
            List<AppraisalFunctionalArea> functional = new List<AppraisalFunctionalArea>();

            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                Label tbKpi = (Label)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
                Label txtWeight = (Label)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
                Label txtWeightPer = (Label)gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
                Label txtTarget = (Label)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
                TextBox selfMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtselfMark");
                TextBox txtTargetPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTargetPer");
                Label txtDeadLine = (Label)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
                DropDownList txtMidStatus = (DropDownList)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
                TextBox txtResult = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtResult");
                TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtselfMark");

                if (tbKpi.Text != "" && txtTarget.Text != "" && txtWeight.Text != "")
                {
                    AppraisalFunctionalArea area = new AppraisalFunctionalArea();
                    area.AppraisalSelfFucAreaId = Convert.ToInt32(gv_AppraisalFunc.DataKeys[i][1].ToString());
                    area.KpiInfo = tbKpi.Text.Trim().ToString();
                    area.KpiWeight = Convert.ToDecimal(txtWeight.Text.Trim().ToString());
                    area.KpiWeightPer = Convert.ToDecimal(txtWeightPer.Text.Trim().ToString());
                    area.Target = Convert.ToDecimal(txtTarget.Text.Trim().ToString());
                    area.SelfMark = string.IsNullOrEmpty(selfMark.Text) ? 0 : Convert.ToDecimal(selfMark.Text.Trim().ToString());
                    area.TargetPer = Convert.ToDecimal(txtTargetPer.Text.Trim().ToString());
                    area.Deadline = Convert.ToDateTime(txtDeadLine.Text.Trim().ToString());

                    area.SupervisorMark = 0;
                    area.MidYearStatus = txtMidStatus.SelectedItem.Text.Trim().ToString();

                    functional.Add(area);
                }

            }


            AppraisalMaster aMaster = new AppraisalMaster();

            aMaster.AppraisalMasterId = Convert.ToInt32(id_mastetID.Value);
            aMaster.EmpInfoId = Convert.ToInt32(id_Empid.Value);
            aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);
            aMaster.AppraisalSelfMasterId = Convert.ToInt32(id_selfID.Value);


            bool result = false;
            if (functional.Count > 0)
            {
                int pk = _appPartA.SaveAppraisalMaster(aMaster, Session["LoginName"].ToString());
                if (pk > 0)
                {
                    result = _appPartA.SaveAppraialFunctionalDetails(functional, pk, aMaster.AppraisalSelfMasterId);
                }
            }
            else
            {
                result = false;
            }

            if (result == true)
            {
               

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='AppraisalDashboard.aspx';",
                    true);
            }
            else
            {
                AlertMessageBoxShow("Operation Failed");
            }

        }
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }


    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AppraisalDashboard.aspx");
    }
    
    private bool Validation()
    {
        bool isVAlid = true;
        if (gv_AppraisalFunc.Rows.Count <= 0)
        {
            isVAlid = false;
            aShowMessage.ShowMessageBox("Kpi Info Required ", this);

        }
        if (id_Empid.Value == "")
        {
            isVAlid = false;
            aShowMessage.ShowMessageBox("Select Employee ", this);
        }

        if (ddlFinancialYear.SelectedValue == "")
        {
            isVAlid = false;
            aShowMessage.ShowMessageBox("Select Fianncial Year ", this);
        }
        if (gv_AppraisalFunc.Rows.Count == 0)
        {
            aShowMessage.ShowMessageBox("Kpi Info Required ", this);
        }
        for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
        {
            Label tbKpi = (Label)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
            Label txtWeight = (Label)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
            Label txtTarget = (Label)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
            TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtselfMark");
            
            if (tbKpi.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Kpi Info Required ", this);
                break;
            }

            if (txtTarget.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Kpi Target Required ", this);
                break;
            }
            if (txtWeight.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Kpi Weight Required ", this);
                break;
            }
           

            if (txtMark.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Kpi Mark Required ", this);
                break;
            }


        }

        Label tst = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalMark");

        decimal weightTotal = tst.Text == "" ? 0 : Convert.ToDecimal(tst.Text.ToString());

        if (weightTotal > 75)
        {
            aShowMessage.ShowMessageBox("Total Score Can Not be Bigger than 75 In Part A ", this);
            isVAlid = false;
        }
        return isVAlid;
    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }


    protected void txtselfMark_OnTextChanged(object sender, EventArgs e)
    {

        int rowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
        if (Convert.ToDecimal(((Label)gv_AppraisalFunc.Rows[rowIndex].Cells[3].FindControl("txtWeight")).Text) >=
            Convert.ToDecimal(((TextBox)gv_AppraisalFunc.Rows[rowIndex].Cells[3].FindControl("txtselfMark")).Text))

        {
            CalculateTotal();
        }

        else
        {
            (((TextBox) gv_AppraisalFunc.Rows[rowIndex].Cells[3].FindControl("txtselfMark")).Text) = 0.ToString();
            AlertMessageBoxShow("Self-Mark must be less then or Equal to Weight (Number)");
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
              

                TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtselfMark");


               
                if (txtMark.Text == "")
                {
                    markTotal = markTotal + 0;
                }
                else
                {
                    markTotal = markTotal + Convert.ToDecimal(txtMark.Text.ToString());
                }




            }

            Label tst = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalMark");
            tst.Text = markTotal.ToString();

        }
    }
}