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

public partial class Appraisal_AppraisalFunctional : System.Web.UI.Page
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
            DataTable dtw = _appPartA.GetAppraisalSelfDetails(Convert.ToInt32(selfMaster));
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
                  

                    ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();



                    id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();

                   // IniKpiTable();
                }
            }
            CheckImmmiedietSuperVisor();
        }
    }

    public void CheckImmmiedietSuperVisor()
    {
        AppraislDashboardDAL appraisl=new AppraislDashboardDAL();
        DataTable dtempdata = appraisl.GetEmpInfo(" WHERE EmpInfoId='" + Request.QueryString["empInfoId"] + "'");
        if (Session["EmpInfoId"].ToString() != Request.QueryString["empInfoId"])
        {


            if (dtempdata.Rows[0]["ReportingEmpId"].ToString() != Session["EmpInfoId"].ToString())
            {
                btn_Save.Visible = false;
                for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
                {
                    TextBox tbKpi = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
                    TextBox txtWeight = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
                    TextBox txtWeightPer = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
                    TextBox txtTarget = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
                    TextBox selfMark = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtselfMark");
                    TextBox txtTargetPer = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtTargetPer");
                    TextBox txtDeadLine = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
                    TextBox txtMidStatus = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
                    TextBox txtResult = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtResult");
                    TextBox txtMark = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                    tbKpi.ReadOnly = true;
                    txtWeight.ReadOnly = true;
                    txtWeightPer.ReadOnly = true;
                    txtTarget.ReadOnly = true;
                    selfMark.ReadOnly = true;
                    txtTargetPer.ReadOnly = true;
                    txtDeadLine.ReadOnly = true;
                    txtMidStatus.ReadOnly = true;
                    txtResult.ReadOnly = true;
                    txtMark.ReadOnly = true;

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

    private void IniKpiTable()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("KpiInfo", typeof(string)));
        dt.Columns.Add(new DataColumn("KpiWeight", typeof(string)));
        dt.Columns.Add(new DataColumn("Target", typeof(string)));
        dt.Columns.Add(new DataColumn("Deadline", typeof(string)));
        dt.Columns.Add(new DataColumn("MidYearStatus", typeof(string)));
        dt.Columns.Add(new DataColumn("ResultYearEnd", typeof(string)));
        dt.Columns.Add(new DataColumn("SupervisorMark", typeof(string)));
        dt.Columns.Add(new DataColumn("SelfMark", typeof(string)));
        dr = dt.NewRow();

        dr["KpiInfo"] = "";
        dr["KpiWeight"] = "";
        dr["Target"] = "";
        dr["Deadline"] = "";
        dr["MidYearStatus"] = "";
        dr["ResultYearEnd"] = "";
        dr["SupervisorMark"] = "";
        dr["SelfMark"] = "";
        dt.Rows.Add(dr);
        ViewState["KPIFUNC"] = dt;

        gv_AppraisalFunc.DataSource = dt;
        gv_AppraisalFunc.DataBind();
    }

    protected void btn_Add_OnClick(object sender, EventArgs e)
    {
        if (ViewState["KPIFUNC"] == null)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("KpiInfo", typeof (string)));
            dt.Columns.Add(new DataColumn("KpiWeight", typeof (string)));
            dt.Columns.Add(new DataColumn("Target", typeof (string)));
            dt.Columns.Add(new DataColumn("Deadline", typeof (string)));
            dt.Columns.Add(new DataColumn("MidYearStatus", typeof (string)));
            dt.Columns.Add(new DataColumn("ResultYearEnd", typeof (string)));
            dt.Columns.Add(new DataColumn("SupervisorMark", typeof(string)));
            dt.Columns.Add(new DataColumn("SelfMark", typeof(string)));
            dr = dt.NewRow();

            dr["KpiInfo"] = "";
            dr["KpiWeight"] = "";
            dr["Target"] = "";
            dr["Deadline"] = "";
            dr["MidYearStatus"] = "";
            dr["ResultYearEnd"] = "";
            dr["SupervisorMark"] = "";
            dr["SelfMark"] = "";
            dt.Rows.Add(dr);
            ViewState["KPIFUNC"] = dt;

            gv_AppraisalFunc.DataSource = dt;
            gv_AppraisalFunc.DataBind();
        }

        else
        {
            DataTable dtCurrentTable = (DataTable)ViewState["KPIFUNC"];

            DataRow drCurrentRow = null;

            drCurrentRow = dtCurrentTable.NewRow();



            dtCurrentTable.Rows.Add(drCurrentRow);


            ViewState["KPIFUNC"] = dtCurrentTable;

            for (int i = 0; i < gv_AppraisalFunc.Rows.Count ; i++)
            {
                TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
                TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
                TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
                TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
                TextBox txtMidStatus = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
                TextBox txtResult = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtResult");
                TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                dtCurrentTable.Rows[i]["KpiInfo"] = tbKpi.Text.Trim().ToString() == "" ? "" : tbKpi.Text.Trim().ToString();
                dtCurrentTable.Rows[i]["KpiWeight"] = txtWeight.Text.Trim().ToString() == "" ? "" : txtWeight.Text.Trim().ToString();
                dtCurrentTable.Rows[i]["Target"] = txtTarget.Text.Trim().ToString() == "" ? "" : txtTarget.Text.Trim().ToString();
                dtCurrentTable.Rows[i]["Deadline"] = txtDeadLine.Text.Trim().ToString() == "" ? "" : txtDeadLine.Text.Trim().ToString();
                dtCurrentTable.Rows[i]["ResultYearEnd"] = txtResult.Text.Trim().ToString() == "" ? 0 : Convert.ToDecimal(txtResult.Text.Trim().ToString());
                dtCurrentTable.Rows[i]["SupervisorMark"] =txtMark.Text.Trim().ToString() ==""?0: Convert.ToDecimal(txtMark.Text.Trim().ToString());
                dtCurrentTable.Rows[i]["MidYearStatus"] = txtMidStatus.Text.Trim().ToString();

            }

            gv_AppraisalFunc.DataSource = dtCurrentTable;
            gv_AppraisalFunc.DataBind();

            //if (dtCurrentTable.Rows.Count > 0)
            //{
            //    drCurrentRow = dtCurrentTable.NewRow();
            //    drCurrentRow["KpiInfo"] = "";
            //    drCurrentRow["KpiWeight"] = "";
            //    drCurrentRow["Target"] = "";
            //    drCurrentRow["Deadline"] = "";
            //    drCurrentRow["MidYearStatus"] = "";
            //    drCurrentRow["ResultYearEnd"] = "";
            //    drCurrentRow["SupervisorMark"] = "";
            //    dtCurrentTable.Rows.Add(drCurrentRow);
            //    ViewState["KPIFUNC"] = dtCurrentTable;
            //    gv_AppraisalFunc.DataSource = dtCurrentTable;
            //    gv_AppraisalFunc.DataBind();


            //}
        }
    }

    protected void lb_Remove_OnClick(object sender, EventArgs e)
    {
        if (ViewState["KPIFUNC"] != null && gv_AppraisalFunc.Rows.Count>1)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["KPIFUNC"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count == 0)
            {
                ViewState["KPIFUNC"] = null;
            }
            else
            {
                ViewState["KPIFUNC"] = dt;
            }


            gv_AppraisalFunc.DataSource = dt;
            gv_AppraisalFunc.DataBind();
           // CalculateTotalParticipant();
        }
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (Validation() == true)
        {
            List<AppraisalFunctionalArea> functional = new List<AppraisalFunctionalArea>();

            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
                TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
                TextBox txtWeightPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
                TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
                TextBox selfMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtselfMark");
                TextBox txtTargetPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTargetPer");
                TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
                TextBox txtMidStatus = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
                TextBox txtResult = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtResult");
                TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                if (tbKpi.Text != "" && txtTarget.Text != "" && txtWeight.Text != "")
                {
                    AppraisalFunctionalArea area = new AppraisalFunctionalArea();
                    area.AppraisalSelfFucAreaId = Convert.ToInt32(gv_AppraisalFunc.DataKeys[i][0].ToString());
                    area.KpiInfo = tbKpi.Text.Trim().ToString();
                    area.KpiWeight = Convert.ToDecimal(txtWeight.Text.Trim().ToString());
                    area.KpiWeightPer = Convert.ToDecimal(txtWeightPer.Text.Trim().ToString());
                    area.Target = Convert.ToDecimal(txtTarget.Text.Trim().ToString());
                    area.SelfMark = string.IsNullOrEmpty(selfMark.Text)?0: Convert.ToDecimal(selfMark.Text.Trim().ToString());
                    area.TargetPer = Convert.ToDecimal(txtTargetPer.Text.Trim().ToString());
                    area.Deadline = Convert.ToDateTime(txtDeadLine.Text.Trim().ToString());
                    area.ResultYearEnd = (txtResult.Text.Trim().ToString());
                    area.SupervisorMark = Convert.ToDecimal(txtMark.Text.Trim().ToString());
                    area.MidYearStatus = txtMidStatus.Text.Trim().ToString();

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
            TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
            TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
            TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
            TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");
            TextBox txtResult = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtResult");
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
            if (txtResult.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Result  Required ", this);
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

   

    private decimal TotalWeight()
    {
        decimal totalWeight = 0;
        for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
        {
            TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");


            totalWeight += txtWeight.Text.Trim().ToString() == "" ? 0 : Convert.ToDecimal(txtWeight.Text.Trim());

        }
        return totalWeight;
    }
    private decimal TotalResultEnd()
    {
        decimal result = 0;
        for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
        {
            TextBox txtResult = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtResult");


            result += txtResult.Text.Trim().ToString() == "" ? 0 : Convert.ToDecimal(txtResult.Text.Trim());

        }
        return result;
    }
    private decimal TotalSelfMark()
    {
        decimal result = 0;
        for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
        {
            TextBox txtselfMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtselfMark");


            result += txtselfMark.Text.Trim().ToString() == "" ? 0 : Convert.ToDecimal(txtselfMark.Text.Trim());

        }
        return result;
    }
    private decimal TotalTarget()
    {
        decimal result = 0;
        for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
        {
            TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");


            result += txtTarget.Text.Trim().ToString() == "" ? 0 : Convert.ToDecimal(txtTarget.Text.Trim());

        }
        return result;
    }
    protected void gv_AppraisalFunc_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lblTotalWeight");
            Label lblresult = (Label)e.Row.FindControl("lblresultend");
            Label lblselfMark = (Label)e.Row.FindControl("lblselfMark");
            Label lbltarget = (Label)e.Row.FindControl("lbltarget");
            lbl.Text = TotalWeight().ToString();
            lblresult.Text = TotalResultEnd().ToString();
            lblselfMark.Text = TotalSelfMark().ToString();
            lbltarget.Text = TotalTarget().ToString();

        }
    }


    protected void txtWeight_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        TextBox txtweight = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtWeight");
        TextBox txtweightper = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtWeightPer");

        double weightNum = string.IsNullOrEmpty(txtweight.Text) ? 0 : Convert.ToDouble(txtweight.Text.Trim());
        double weightper = string.IsNullOrEmpty(txtweightper.Text) ? 0 : Convert.ToDouble(txtweightper.Text.Trim());

        double thePer = (weightNum / (75.0 / 100.0));
        txtweightper.Text = thePer.ToString("#,##0.00");
        CalculateTotal();
    }


    protected void CalculateTotal()
    {
        decimal weightTotal = 0;
        decimal markTotal = 0;
        if (gv_AppraisalFunc.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");

                TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");


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

    //protected void ddlFinancialYear_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DataTable dt = _appPartA.GetApparaisalSelfForAuth(Convert.ToInt32(ddlFinancialYear.SelectedValue),
    //        Convert.ToInt32(id_Empid.Value));
    //    ViewState["KPIFUNC"] = dt;
    //    gv_AppraisalFunc.DataSource = dt;
    //    gv_AppraisalFunc.DataBind();
    //}

    protected void btn_Review_OnClick(object sender, EventArgs e)
    {
        bool result = _appPartA.GoForReview(Convert.ToInt32(id_selfID.Value));
        if (result == true)
        {
            AlertMessageBoxShow("Operation Successfull");
            Response.Redirect("AppraisalDashboard.aspx");
        }
        else
        {
            AlertMessageBoxShow("Operation Failed");
        }
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AppraisalDashboard.aspx");
    }

    protected void txtMark_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        decimal weight = 0,mark=0;
        weight = Convert.ToDecimal(((TextBox) gv_AppraisalFunc.Rows[rowID].FindControl("txtWeight")).Text);
        mark=Convert.ToDecimal(((TextBox) gv_AppraisalFunc.Rows[rowID].FindControl("txtMark")).Text);
        if (weight < mark)
        {
            ((TextBox) gv_AppraisalFunc.Rows[rowID].FindControl("txtMark")).Text = "0";
            double weightTotal = 0;

            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                if (txtWeight.Text == "")
                {
                    weightTotal = weightTotal + 0;
                }
                else
                {
                    weightTotal = weightTotal + Convert.ToDouble(txtWeight.Text.ToString());
                }


            }
            Label tst = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalMark");
            tst.Text = weightTotal.ToString();
            aShowMessage.ShowMessageBox("Supuervisor Marks Cannot Be Greater Then Weight", this);
        }
        else
        {


            double weightTotal = 0;

            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                TextBox txtWeight = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                if (txtWeight.Text == "")
                {
                    weightTotal = weightTotal + 0;
                }
                else
                {
                    weightTotal = weightTotal + Convert.ToDouble(txtWeight.Text.ToString());
                }


            }
            Label tst = (Label) gv_AppraisalFunc.FooterRow.FindControl("lblTotalMark");
            tst.Text = weightTotal.ToString();
        }
    }

    private void CalculateB()
    {
        decimal weightTotal = 0;

        if (gv_AppraisalFunc.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");




                if (txtWeight.Text == "")
                {
                    weightTotal = weightTotal + 0;
                }
                else
                {
                    weightTotal = weightTotal + Convert.ToDecimal(txtWeight.Text.ToString());
                }




            }



            Label tst2 = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalMark");
            tst2.Text = weightTotal.ToString();
        }

    }

    protected void txtWeightPer_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        TextBox txtweight = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtWeight");
        TextBox txtweightper = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtWeightPer");

        double weightNum = string.IsNullOrEmpty(txtweight.Text) ? 0 : Convert.ToDouble(txtweight.Text.Trim());
        double weightper = string.IsNullOrEmpty(txtweightper.Text) ? 0 : Convert.ToDouble(txtweightper.Text.Trim());

        double thenum = (75.00 / 100.00) * weightper;
        txtweight.Text = thenum.ToString("#,##0.00");
        CalculateTotal();
    }

    protected void txtTarget_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        TextBox txtweight = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtTarget");
        TextBox txtweightper = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtTargetPer");

        double weightNum = string.IsNullOrEmpty(txtweight.Text) ? 0 : Convert.ToDouble(txtweight.Text.Trim());
        double weightper = string.IsNullOrEmpty(txtweightper.Text) ? 0 : Convert.ToDouble(txtweightper.Text.Trim());

        double thePer = (weightNum / (75.0 / 100.0));
        txtweightper.Text = thePer.ToString("#,##0.00");
    }

    protected void txtTargetPer_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        TextBox txtweight = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtTarget");
        TextBox txtweightper = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtTargetPer");

        double weightNum = string.IsNullOrEmpty(txtweight.Text) ? 0 : Convert.ToDouble(txtweight.Text.Trim());
        double weightper = string.IsNullOrEmpty(txtweightper.Text) ? 0 : Convert.ToDouble(txtweightper.Text.Trim());

        double thenum = (75.00 / 100.00) * weightper;
        txtweight.Text = thenum.ToString("#,##0.00");
    }
}