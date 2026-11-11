using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.Report_DAL;
using DAL.TrainingDAL;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;


public partial class Appraisal_OKRBSCApppraisalForHR : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private HRIS_SMCEntities _hrEntities = new HRIS_SMCEntities();
    private BSCAppraisalFunctionalPartDALHR _appPartA = new BSCAppraisalFunctionalPartDALHR();
    private BSCOKRAppraislDashboardDAL _appDashboard = new BSCOKRAppraislDashboardDAL();
    ShowMessage aShowMessage = new ShowMessage();
    SupervisorMenuAppDAL appDal = new SupervisorMenuAppDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInitialDDL();

            int empId = int.Parse(Request.QueryString["EmpInfoId"]);
            string finYearDes = (Request.QueryString["FinancialYearDesc"]);
            string M = (Request.QueryString["M"]);

            //DataTable dt = _appDashboard.GetAppraisalDashboard(ddlCompany.SelectedValue == "" ? 0 : Convert.ToInt32(ddlCompany.SelectedValue.ToString()));
            //gv_AppraisalBoard.DataSource = dt;
            //gv_AppraisalBoard.DataBind();
            ddlCompany.SelectedIndex = 1;
            int company = Convert.ToInt32(ddlCompany.SelectedValue);
            LoadDiviion(company);
            //if (Session["radStatus"] != null)
            //{
                //radOp.SelectedValue = Session["radStatus"].ToString();
                //FinancialYearDropDownList.SelectedValue = Session["FinStatus"].ToString();
                AppraisalOwn.DataSource = null;
                AppraisalOwn.DataBind();
            //gv_AppraisalBoard.DataSource = null;
            //gv_AppraisalBoard.DataBind();
            //Session["radStatus"] = radOp.SelectedValue;
            //Session["FinStatus"] = FinancialYearDropDownList.SelectedValue;
            //if (radOp.SelectedValue == "Own")
            //{
            //    if (FinancialYearDropDownList.SelectedValue != "")
            //    {


            //        DataTable dt = _appDashboard.GetAppraisalDashboardOwn(Convert.ToInt32(Session["EmpInfoId"]),
            //            Convert.ToInt32(FinancialYearDropDownList.SelectedValue));
            //        if (dt.Rows.Count > 0)
            //        {
            //            AppraisalOwn.DataSource = dt;
            //            AppraisalOwn.DataBind();





            //            DataTable dt2 = _appDashboard.GetAppraisalByPermission2(FinancialYearDropDownList.SelectedValue, Session["EmpInfoId"].ToString());

            //            for (int i = 0; i < AppraisalOwn.Rows.Count; i++)
            //            {


            //                CheckBox MidYearStatus = (CheckBox)AppraisalOwn.Rows[i].FindControl("IsMidYearStatus");


            //                bool IsMidYearStatu = false;
            //                try
            //                {
            //                    IsMidYearStatu = Convert.ToBoolean(dt.Rows[0]["IsMidYearStatus"].ToString());
            //                    MidYearStatus.Checked = IsMidYearStatu;

            //                }
            //                catch (Exception)
            //                {
            //                    IsMidYearStatu = false;
            //                    MidYearStatus.Checked = IsMidYearStatu;

            //                    //throw;
            //                }
            //                HiddenField id_appraisalMaster = (HiddenField)AppraisalOwn.Rows[i].FindControl("id_appraisalMaster");







            //                DataTable dt3 = _appDashboard.GetAppraisalByPermission3(id_appraisalMaster.Value);
            //                string EmpID = "";
            //                string Actions = "";
            //                if (dt3.Rows.Count>0)
            //                {
            //                    EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
            //                    Actions = dt3.Rows[0]["ActionStatus"].ToString();
            //                }
            //                //
            //                if ( EmpID != Session["EmpInfoId"].ToString() || dt2.Rows.Count == 0)
            //                {
            //                    AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = false;
            //                     AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = false;
            //                }

            //                //

            //                if (Actions.ToString() == "Approved" || EmpID != Session["EmpInfoId"].ToString() || dt2.Rows.Count == 0)
            //                {
            //                   AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = false;
            //                   AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = false;

            //                }
            //                if (EmpID == "0")
            //                {
            //                    AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = false;
            //                     AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = false;
            //                }


            //                if (dt3.Rows.Count ==0)
            //                {
            //                 AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = true;
            //                   AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = true;
            //                }


            //                LinkButton PartAOwn = (LinkButton) AppraisalOwn.Rows[i].FindControl("PartAOwn");

            //                if (PartAOwn.Text == "Not Complete")
            //                {
            //                    PartAOwn.CssClass = "btn btn-danger btn-sm ";

            //                }

            //                if (PartAOwn.Text == "Complete")
            //                {
            //                    PartAOwn.CssClass = "btn btn-success btn-sm";

            //                }


            //                try
            //                {
            //                    decimal pppp = 0;
            //                    pppp = Convert.ToDecimal(PartAOwn.Text);
            //                    if (pppp >= 0)
            //                    {
            //                        PartAOwn.CssClass = "btn btn-success btn-sm";

            //                    }

            //                }
            //                catch (Exception)
            //                {

            //                }


            //                LinkButton PartBOwn = (LinkButton) AppraisalOwn.Rows[i].FindControl("PartBOwn");

            //                if (PartBOwn.Text == "Not Complete")
            //                {
            //                    PartBOwn.CssClass = "btn btn-danger btn-sm ";

            //                }

            //                if (PartBOwn.Text == "Complete")
            //                {
            //                    PartBOwn.CssClass = "btn btn-success btn-sm";

            //                }



            //                try
            //                {
            //                    decimal ppppb = 0;
            //                    ppppb = Convert.ToDecimal(PartBOwn.Text);
            //                    if (ppppb >= 0)
            //                    {
            //                        PartBOwn.CssClass = "btn btn-success btn-sm";

            //                    }

            //                }
            //                catch (Exception)
            //                {

            //                }




            //                LinkButton training = (LinkButton) AppraisalOwn.Rows[i].FindControl("training");

            //                if (training.Text == "Not Complete")
            //                {
            //                    training.CssClass = "btn btn-danger btn-sm ";

            //                }

            //                if (training.Text == "Complete")
            //                {
            //                    training.CssClass = "btn btn-success btn-sm";
            //                    training.Text = "Completed";
            //                }
            //                try
            //                {
            //                    decimal trainingD = 0;
            //                    trainingD = Convert.ToDecimal(training.Text);
            //                    if (trainingD >= 0)
            //                    {
            //                        training.CssClass = "btn btn-success btn-sm";

            //                    }

            //                }
            //                catch (Exception)
            //                {

            //                }


            //            }
            //        }
            //        else
            //        {
            //            AppraisalOwn.DataSource = null;
            //            AppraisalOwn.DataBind();
            //        }
            //    }
            //}
            //else
            {
                //  if (FinancialYearDropDownList.SelectedValue != "")
                //{

                LoadGridList(empId, finYearDes);

                //RadioTextValue();
                //}
                //else
                //{
                //    gv_AppraisalBoard.DataSource = null;
                //    gv_AppraisalBoard.DataBind();
                //}

                //}
            }
            // btnSearch_OnClick(null, null);
        }
    }

    private void LoadGridList(int empId, string finYear)
    {
        DataTable DTSupp = _appDashboard.GetAppraisalDashboardForHR(Convert.ToInt32(empId), finYear);

        if (DTSupp.Rows.Count > 0)
        {
            gv_AppraisalBoard.DataSource = DTSupp;
            gv_AppraisalBoard.DataBind();


            //DataTable dt2 = _appDashboard.GetAppraisalByPermission2(FinancialYearDropDownList.SelectedValue, Session["EmpInfoId"].ToString());
            for (int i = 0; i < gv_AppraisalBoard.Rows.Count; i++)
            {



                HiddenField id_appraisalMaster = (HiddenField)gv_AppraisalBoard.Rows[i].FindControl("id_appraisalMaster");

                HiddenField EmpInfoId = (HiddenField)gv_AppraisalBoard.Rows[i].FindControl("id_empId");

                DataTable dt2 = _appDashboard.GetAppraisalByPermission2(FinancialYearDropDownList.SelectedValue, EmpInfoId.Value.ToString());

                if (EmpInfoId.Value == Session["EmpInfoId"].ToString())
                {
                    gv_AppraisalBoard.Rows[i].Visible = false;
                }

                if (dt2.Rows.Count == 0)
                {
                    ((LinkButton)gv_AppraisalBoard.Rows[i].FindControl("btn_View")).Visible = true;

                    //((Label)gv_AppraisalBoard.Rows[i].FindControl("lblExpireStatus")).Text = "Deadline Already expired.";

                }

                //DataTable dt3 = _appDashboard.GetAppraisalByPermission3(id_appraisalMaster.Value);
                //string EmpID = "";
                //string Actions = "";
                //if (dt3.Rows.Count > 0)
                //{
                //    EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
                //    Actions = dt3.Rows[0]["ActionStatus"].ToString();
                //}
                ////
                //if ( EmpID != Session["EmpInfoId"].ToString()  )
                //{
                //    ((RadioButtonList)gv_AppraisalBoard.Rows[i].FindControl("actionRadioButtonList")).Enabled = false;


                //    ((TextBox)gv_AppraisalBoard.Rows[i].FindControl("txt_comments")).ReadOnly = true;

                //    ((Button)gv_AppraisalBoard.Rows[i].FindControl("btn_Save1")).Enabled = false;
                //}

                ////

                //if (Actions.ToString() == "Approved" || EmpID != Session["EmpInfoId"].ToString())
                //{
                //    ((RadioButtonList)gv_AppraisalBoard.Rows[i].FindControl("actionRadioButtonList")).Enabled = false;


                //    ((TextBox)gv_AppraisalBoard.Rows[i].FindControl("txt_comments")).ReadOnly = true;

                //    ((Button)gv_AppraisalBoard.Rows[i].FindControl("btn_Save1")).Enabled = false;

                //}
                //if (EmpID == "0")
                //{
                //    ((RadioButtonList)gv_AppraisalBoard.Rows[i].FindControl("actionRadioButtonList")).Enabled = false;


                //    ((TextBox)gv_AppraisalBoard.Rows[i].FindControl("txt_comments")).ReadOnly = true;

                //    ((Button)gv_AppraisalBoard.Rows[i].FindControl("btn_Save1")).Enabled = false;

                //}




                LinkButton PartA = (LinkButton)gv_AppraisalBoard.Rows[i].FindControl("PartA");

                if (PartA.Text == "Not Complete")
                {
                    PartA.CssClass = "btn btn-danger btn-sm ";

                }

                if (PartA.Text == "Complete")
                {
                    PartA.CssClass = "btn btn-success btn-sm";

                }


                try
                {
                    decimal pppp = 0;
                    pppp = Convert.ToDecimal(PartA.Text);
                    if (pppp >= 0)
                    {
                        PartA.CssClass = "btn btn-success btn-sm";

                    }
                }
                catch (Exception)
                {

                }



                LinkButton PartB = (LinkButton)gv_AppraisalBoard.Rows[i].FindControl("PartB");

                if (PartB.Text == "Not Complete")
                {
                    PartB.CssClass = "btn btn-danger btn-sm ";

                }

                if (PartB.Text == "Complete")
                {
                    PartB.CssClass = "btn btn-success btn-sm";

                }



                try
                {
                    decimal ppppb = 0;
                    ppppb = Convert.ToDecimal(PartB.Text);
                    if (ppppb >= 0)
                    {
                        PartB.CssClass = "btn btn-success btn-sm";

                    }

                }
                catch (Exception)
                {

                }


                decimal partaa = 0; decimal partbb = 0;

                try
                {
                    partaa = Convert.ToDecimal(PartA.Text);
                }
                catch (Exception)
                {

                    // throw;
                }
                try
                {
                    partbb = Convert.ToDecimal(PartB.Text);
                }
                catch (Exception)
                {

                    // throw;
                }
                string status = "";
                decimal total = partaa + partbb;
                if (total <= 60)
                {
                    status = "Poor";
                }

                if (total > 60 && total <= 75)
                {
                    status = "Average";
                }

                if (total >= 76 && total < 81)
                {
                    status = "Good";
                }
                if (total >= 81 && total < 91)
                {
                    status = "Excellent";
                }

                if (total > 90)
                {
                    status = "Outstanding";
                }


                LinkButton PartC = (LinkButton)gv_AppraisalBoard.Rows[i].FindControl("PartC");

                if (PartC.Text == "Not Complete")
                {
                    PartC.CssClass = "btn btn-danger btn-sm ";

                }
                else

                {

                    PartC.Text = status;
                    PartC.CssClass = "btn btn-success btn-sm";

                }


                LinkButton training = (LinkButton)gv_AppraisalBoard.Rows[i].FindControl("Training");

                if (training.Text == "Not Complete")
                {
                    training.CssClass = "btn btn-danger btn-sm ";
                    training.Text = "Training (0)";



                }

                if (training.Text == "Complete")
                {
                    training.CssClass = "btn btn-success btn-sm";

                    DataTable dtTra = _appPartA.GetAppraisalTraining(Convert.ToInt32(id_appraisalMaster.Value));
                    if (dtTra.Rows.Count > 0)
                    {
                        training.Text = "Training (" + dtTra.Rows.Count + ")";
                    }

                }


            }


        }
    }

    private void LoadInitialDDL()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {

            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();

            ddlCompany.SelectedIndex = 1;

            ddlCompany_OnSelectedIndexChanged(null, null);
        }
    }

    private void LoadDiviion(int comId)
    {
       // try
        {
            List<tblDivision> divList = _hrEntities.tblDivisions.Where(a => a.CompanyId == comId).ToList();

            ddlDivision.DataSource = divList;
            ddlDivision.DataValueField = "DivisionId";
            ddlDivision.DataTextField = "DivisionName";
            ddlDivision.DataBind();
        }
        //catch (Exception)
        {
           // 
           // throw;
        }
       

    }
    EmployeeContractualReportDAL aEmployeeJobLeftEntryDAL = new EmployeeContractualReportDAL();
    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
       

        if (ddlCompany.SelectedValue != "")
        {
            int company = Convert.ToInt32(ddlCompany.SelectedValue);
            LoadDiviion(company);
            aEmployeeJobLeftEntryDAL.FinYearByCompDropDown(FinancialYearDropDownList, ddlCompany.SelectedValue);
        }
        else
        {
            FinancialYearDropDownList.Items.Clear();
        }
    }

    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
       
         int divId = Convert.ToInt32( ddlDivision.SelectedValue);
        LoadWing(divId);
    }

    private void LoadWing(int divId)
    {
        List<tblDivisionWing> wingList = _hrEntities.tblDivisionWings.Where(d => d.DivisionId == divId).ToList();
       
        ddlWing.DataSource = wingList;
        ddlWing.DataValueField = "DivisionWId";
        ddlWing.DataTextField = "DivisionWingName";
        ddlWing.DataBind();

    }

    protected void ddlWing_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int wingId = Convert.ToInt32(ddlWing.SelectedValue);
        LoadDepartment(wingId);
    }

    private void LoadDepartment(int wingId)
    {
        List<tblDepartment> dptList = _hrEntities.tblDepartments.Where(w => w.DivisionWId == wingId).ToList();
        ddlDepartment.DataSource = dptList;
        ddlDepartment.DataValueField = "DepartmentId";
        ddlDepartment.DataTextField = "DepartmentName";
        ddlDepartment.DataBind();

    }

    protected void ddlDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int dptId = Convert.ToInt32(ddlDepartment.SelectedValue);
        LoadSection(dptId);
    }

    private void LoadSection(int dpt)
    {

        DataTable sectionList = _appDashboard.LoadSectionDDl(dpt);
        
        ddlSection.DataSource = sectionList;
        ddlSection.DataValueField = "SectionId";
        ddlSection.DataTextField = "SectionName";
        ddlSection.DataBind();

    }

    protected void ddlSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int dptId = Convert.ToInt32(ddlSection.SelectedValue);
        LoadSubsection(dptId);

    }

    private void LoadSubsection(int id)
    {

        DataTable sectionList = _appDashboard.LoadSubsection(id);
        ddlSubsection.DataSource = sectionList;
        ddlSubsection.DataValueField = "SubSectionId";
        ddlSubsection.DataTextField = "SubSectionName";
        ddlSubsection.DataBind();
    }

    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        DataTable dt = _appDashboard.GetAppraisalDashboard(ddlCompany.SelectedValue==""?0:Convert.ToInt32(ddlCompany.SelectedValue.ToString()));
        gv_AppraisalBoard.DataSource = dt;
        gv_AppraisalBoard.DataBind();
    }

    protected void PartA_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_empId");
        HiddenField hfselfMaster = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("selfMaster");
        HiddenField FinancialYearId = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("FinancialYearId");


        string mastrValue = mastrId.Value;
        string emoIdValue = empID.Value;
        int masterID = int.Parse(mastrValue); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
        int empInfoId = int.Parse(emoIdValue);
        int selfMaster = int.Parse(hfselfMaster.Value);
      hfFinancialYearId.Value = int.Parse(FinancialYearId.Value).ToString();

        DataTable dt = _appPartA.GetAppraisalSelf(Convert.ToInt32(selfMaster));
        DataTable dtw = _appPartA.GetAppraisalSelfDetails(Convert.ToInt32(selfMaster));
        DataTable dtw2 = _appPartA.GetAppraisalfDetailsFromSupLatest(Convert.ToInt32(selfMaster));
        id_selfID.Value = selfMaster.ToString();

        if (dtw2.Rows.Count > 0)
        {
            gv_AppraisalFuncSUP.DataSource = dtw2;
            gv_AppraisalFuncSUP.DataBind();
            ViewState["KPIFUNCSUP"] = dtw2;
            try
            {

                for (int i = 0; i < dtw2.Rows.Count; i++)
                {
                    DropDownList txtMidStatus = (DropDownList)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMidStatus");
                    try
                    {
                        txtMidStatus.SelectedValue = dtw2.Rows[i]["MidYearStatus"].ToString();
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                }
            }
            catch (Exception)
            {
                
                //throw;
            }
        }
        else
        {
            gv_AppraisalFuncSUP.DataSource = dtw;
            gv_AppraisalFuncSUP.DataBind();
            ViewState["KPIFUNCSUP"] = dtw;

            try
            {
                for (int i = 0; i < dtw.Rows.Count; i++)
                {
                    DropDownList txtMidStatus = (DropDownList)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMidStatus");
                    try
                    {
                        txtMidStatus.SelectedValue = dtw.Rows[i]["MidYearStatus"].ToString();
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                }
            }
            catch (Exception)
            {
                
                //throw;
            }
        }
        btnAppraisalFuncSUPSave.Visible = true;

        id_mastetID.Value = masterID.ToString();
        id_Empid.Value = empInfoId.ToString();
        CalculateBFuncSUP();
        GetEmpInfoByEmpIDFuncSUP(empInfoId);
        CheckImmmiedietSuperVisor();
        //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('AppraisalFunctional.aspx?masterId=" + mastrId.Value + "&empInfoId=" + empID.Value + "&selfMaster=" + selfMaster.Value  + "' ,'_blank');", true);
        mpFunctionalSup.Show();




        DataTable dt3r = _appDashboard.GetAppraisalByPermission3(id_mastetID.Value);
        string EmpID = "";
        string Actions = "";
        if (dt3r.Rows.Count > 0)
        {
            EmpID = dt3r.Rows[0]["ForEmpInfoId"].ToString();
            Actions = dt3r.Rows[0]["ActionStatus"].ToString();
            if (EmpID != Session["EmpInfoId"].ToString() || Actions == "Approved")
            {
                btnAppraisalFuncSUPSave.Visible = true;
            }
            


            if (EmpID == "0")
            {
                btnAppraisalFuncSUPSave.Visible = true;
            }
            
        }

    }


    public void CheckImmmiedietSuperVisor()
    {
        AppraislDashboardDAL appraisl = new AppraislDashboardDAL();
        DataTable dtempdata = appraisl.GetEmpInfo(" WHERE EmpInfoId='" + id_Empid.Value + "'");

        DataTable CheckFinalApproval = _appPartA.CheckFinalApprovalConditionNotSuppervisor( id_Empid.Value.ToString());
        String ddd = "";
        try
        {
            ddd = CheckFinalApproval.Rows[0]["IsAllEmployee"].ToString();
        }
        catch (Exception)
        {

            //throw;
        }


        if (ddd == "True")
        {

          
        }
        else  if (Session["EmpInfoId"].ToString() != id_Empid.Value)
        {


            if (dtempdata.Rows[0]["ReportingEmpId"].ToString() != Session["EmpInfoId"].ToString())
            {
                btnAppraisalFuncSUPSave.Visible = true;
                //for (int i = 0; i < gv_AppraisalFuncSUP.Rows.Count; i++)
                //{
                //    TextBox tbKpi = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtKpi");
                //    TextBox txtWeight = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtWeight");
                //    TextBox txtWeightPer = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtWeightPer");
                //    TextBox txtTarget = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtTarget");
                //    TextBox selfMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtselfMark");
                //    TextBox txtTargetPer = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtTargetPer");
                //    TextBox txtDeadLine = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtDeadLine");
                //    TextBox txtMidStatus = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMidStatus");
                //    TextBox txtResult = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtResult");
                //    TextBox txtMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMark");

                //    //tbKpi.ReadOnly = true;
                //    txtWeight.ReadOnly = true;
                //    txtWeightPer.ReadOnly = true;
                //    txtTarget.ReadOnly = true;
                //    selfMark.ReadOnly = true;
                //    txtTargetPer.ReadOnly = true;
                //    txtDeadLine.ReadOnly = true;
                //    txtMidStatus.ReadOnly = true;
                //    txtResult.ReadOnly = true;
                //    txtMark.ReadOnly = false;

                //}
            }
        }
    }
    private void CalculateBFuncSUP()
    {
        decimal MarkTotal = 0;
        decimal WeightTotal = 0;
        decimal TargetTotal = 0;
        decimal ResultTotal = 0;
        decimal tselfMark = 0;


        if (gv_AppraisalFuncSUP.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalFuncSUP.Rows.Count; i++)
            {

                 CheckBox chkIsActive = (CheckBox)gv_AppraisalFuncSUP.Rows[i].FindControl("isActiveCheckBox");
                //if (chkIsActive.Checked)
                {
                    TextBox txtMark = (TextBox) gv_AppraisalFuncSUP.Rows[i].FindControl("txtMark");
                    TextBox txtWeight = (TextBox) gv_AppraisalFuncSUP.Rows[i].FindControl("txtWeight");
                    TextBox txtTarget = (TextBox) gv_AppraisalFuncSUP.Rows[i].FindControl("txtTarget");
                    TextBox txtResult = (TextBox) gv_AppraisalFuncSUP.Rows[i].FindControl("txtResult");
                    TextBox txtselfMark = (TextBox) gv_AppraisalFuncSUP.Rows[i].FindControl("txtselfMark");
                    Label lblSelfPer = (Label) gv_AppraisalFuncSUP.Rows[i].FindControl("lblSelfPer");





                    if (txtMark.Text == "")
                    {
                        MarkTotal = MarkTotal + 0;
                    }
                    else
                    {
                        MarkTotal = MarkTotal + Convert.ToDecimal(txtMark.Text.ToString());
                    }

                    if (txtWeight.Text == "")
                    {
                        WeightTotal = WeightTotal + 0;
                    }
                    else
                    {
                        WeightTotal = WeightTotal + Convert.ToDecimal(txtWeight.Text.ToString());
                    }


                    if (txtTarget.Text == "")
                    {
                        TargetTotal = TargetTotal + 0;
                    }
                    else
                    {
                        TargetTotal = TargetTotal + Convert.ToDecimal(txtTarget.Text.ToString());
                    }



                    if (txtResult.Text == "")
                    {
                        ResultTotal = ResultTotal + 0;
                    }
                    else
                    {
                       // ResultTotal = ResultTotal + Convert.ToDecimal(txtResult.Text.ToString());
                    }



                    if (txtselfMark.Text == "")
                    {
                        tselfMark = tselfMark + 0;
                    }
                    else
                    {
                        tselfMark = tselfMark + Convert.ToDecimal(txtselfMark.Text.ToString());
                    }
                    decimal Selfres = 0;
                    try
                    {
                        Selfres = (Convert.ToDecimal(txtselfMark.Text.ToString()) / Convert.ToDecimal(txtWeight.Text.ToString())) * 100;
                        Selfres = Math.Round(Selfres, 2);
                    }
                    catch
                    {

                    }

                    lblSelfPer.Text = "Achi: " + Selfres.ToString() + "%";
                }
            }



            Label tst2 = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblTotalMark");
            tst2.Text = MarkTotal.ToString();


            Label lblTotalWeight = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblTotalWeight");
            lblTotalWeight.Text = WeightTotal.ToString();




            Label lbltarget = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lbltarget");
            lbltarget.Text = TargetTotal.ToString();


            //Label lblresultend = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblresultend");
            //lblresultend.Text = ResultTotal.ToString();


            Label lblselfMark = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblselfMark");
            Label lblTotalSelfPer = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblTotalSelfPer");
            Label lblTotalSelfPerT = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblTotalSelfPerT");
            Label lblSupervisorMarkPer = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblSupervisorMarkPer");
            Label lblSupervisorMarkPerT = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblSupervisorMarkPerT");
            lblselfMark.Text = tselfMark.ToString();


            decimal _tWeight = 0;
            try
            {
                _tWeight = Convert.ToDecimal(lblTotalWeight.Text);
            }
            catch
            {

            }
            decimal _tself = 0;
            try
            {
                _tself = Convert.ToDecimal(lblselfMark.Text);
            }
            catch
            {

            }
            decimal res = 0;
            decimal resMain = 0;
            try
            {
                res = (_tself / _tWeight) * 100;
                res = Math.Round(res, 2);
                resMain = Math.Round(res * Convert.ToDecimal(0.75), 2);
            }
            catch
            {

            }
            
            decimal _tsup = 0;
            try
            {
                _tsup = Convert.ToDecimal(tst2.Text);
            }
            catch
            {

            }
            decimal resSup = 0;
            decimal resSupMain = 0;
            try
            {
                resSup = (_tsup / _tWeight) * 100;
                resSup = Math.Round(resSup, 2);
                resSupMain = Math.Round(resSup * Convert.ToDecimal(0.75), 2);
            }
            catch
            {

            }
            lblSupervisorMarkPer.Text = "Total Achi: " + resSup.ToString() + "%";
            lblSupervisorMarkPerT.Text = "Total Out of 75: " + resSupMain.ToString();
            lblTotalSelfPerT.Text =   "Total Out of 75: " + resMain.ToString() ;
            lblTotalSelfPer.Text = "Total Achi: " + res.ToString() + "%";

        }
    }


    protected void PartAOwn_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_empId");
       HiddenField hfselfMaster = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("selfMaster");


         

        mpe_1.Show();


        int masterID = int.Parse(mastrId.Value); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
        int empInfoId = int.Parse(empID.Value);
        int selfMaster = int.Parse(hfselfMaster.Value);

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
        
        GetEmpInfoByEmpID(empInfoId);

            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                DropDownList txtMidStatus = (DropDownList)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
                for (int j = 0; j < txtMidStatus.Items.Count; j++)
                {
                    if (txtMidStatus.Items[j].Text == gv_AppraisalFunc.DataKeys[i][0].ToString())
                    {
                        txtMidStatus.SelectedIndex = j;
                    }
                }
            }
         CalculateB();




        //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('AppraisalFunctionalSelfMark.aspx?masterId=" + mastrId.Value + "&empInfoId=" + empID.Value + "&selfMaster=" + selfMaster.Value + "' ,'_blank');", true);


         DataTable dt3 = _appDashboard.GetAppraisalByPermission3(id_mastetID.Value);
         string EmpID = "";
         string Actions = "";
         if (dt3.Rows.Count > 0)
         {
             EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
             Actions = dt3.Rows[0]["ActionStatus"].ToString();
             if (EmpID != Session["EmpInfoId"].ToString() || Actions == "Approved")
             {
                 btnFunctionalSave.Visible = false;
             }


             if (EmpID == "0")
             {
                 btnFunctionalSave.Visible = false;
             }
         }
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
    private void GetEmpInfoByEmpID(int empInfoId)
    {
        if (empInfoId > 0)
        {
            DataTable dtEmp = _appPartA.GetEmployeeDetails(empInfoId);
            if (dtEmp.Rows.Count > 0)
            { 

                lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();

                empName.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();


                deptNameLabel.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();


                desigNameLabel.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();


                joiningDateLabel.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");


                LocationLabel.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
                lblPlace.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();


                id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();

                // IniKpiTable();
            }
        }
    }

    private void GetEmpInfoByEmpIDTrainSup(int empInfoId)
    {
        if (empInfoId > 0)
        {
            DataTable dtEmp = _appPartA.GetEmployeeDetails(empInfoId);
            if (dtEmp.Rows.Count > 0)
            {
                lblPlaceTrainingSUP.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();

                lblEmpIdTrainingSUP.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();

                empNameTrainingSUP.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();


                deptNameLabelTrainingSUP.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();


                desigNameLabelTrainingSUP.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();


                joiningDateLabelTrainingSUP.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");


                LocationLabelTrainingSUP.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabelTrainingSUP.Text = dtEmp.Rows[0]["ReportingToName"].ToString();


                id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();

                // IniKpiTable();
            }
        }
    }

    private void GetEmpInfoByEmpIDFinalStatus(int empInfoId)
    {
        if (empInfoId > 0)
        {
            DataTable dtEmp = _appPartA.GetEmployeeDetails(empInfoId);
            if (dtEmp.Rows.Count > 0)
            {
                lblPlaceFinalStatus.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();

                lblEmpIdFinalStatus.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();

                empNameFinalStatus.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();


                deptNameLabelFinalStatus.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();


                desigNameLabelFinalStatus.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();


                joiningDateLabelFinalStatus.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");


                LocationLabelFinalStatus.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabelFinalStatus.Text = dtEmp.Rows[0]["ReportingToName"].ToString();


                lblGrade.Text = dtEmp.Rows[0]["GradeName"].ToString();

                lblStep.Text = dtEmp.Rows[0]["SalaryStepName"].ToString();


                id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();

                // IniKpiTable();
            }
        }
    }



    private void GetEmpInfoByEmpIDPartBSUP(int empInfoId)
    {
        if (empInfoId > 0)
        {
            DataTable dtEmp = _appPartA.GetEmployeeDetails(empInfoId);
            if (dtEmp.Rows.Count > 0)
            {
                lblPlaceBSup.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();

                lblEmpIdBSup.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();

                empNameBSup.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();


                deptNameLabelBSup.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();


                desigNameLabelBSup.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();


                joiningDateLabelBSup.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");


                LocationLabelBSup.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabelBSup.Text = dtEmp.Rows[0]["ReportingToName"].ToString();


                id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();

                // IniKpiTable();
            }
        }
    }

    private void GetEmpInfoByEmpIDFuncSUP(int empInfoId)
    {
        if (empInfoId > 0)
        {
            DataTable dtEmp = _appPartA.GetEmployeeDetails(empInfoId);
            if (dtEmp.Rows.Count > 0)
            {
                lblPlaceFuncSUP.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();

                lblEmpIdFuncSUP.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();

                empNameFuncSUP.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();


                deptNameLabelFuncSUP.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();


                desigNameLabelFuncSUP.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();


                joiningDateLabelFuncSUP.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");


                LocationLabelFuncSUP.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabelFuncSUP.Text = dtEmp.Rows[0]["ReportingToName"].ToString();


                id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();

                // IniKpiTable();
            }
        }
    }


    private void GetEmpInfoByEmpIDBehavioral(int empInfoId)
    {
        if (empInfoId > 0)
        {
            DataTable dtEmp = _appPartA.GetEmployeeDetails(empInfoId);
            if (dtEmp.Rows.Count > 0)
            {
                lblPlaceBehavioral.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();

                lblEmpIdBehavioral.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();

                empNameBehavioral.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();


                deptNameLabelBehavioral.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();


                desigNameLabelBehavioral.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();


                joiningDateLabelBehavioral.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");


                LocationLabelBehavioral.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabelBehavioral.Text = dtEmp.Rows[0]["ReportingToName"].ToString();


                id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();

                // IniKpiTable();
            }
        }
    }



    private void GetEmpInfoByEmpIDTraining(int empInfoId)
    {
        if (empInfoId > 0)
        {
            DataTable dtEmp = _appPartA.GetEmployeeDetails(empInfoId);
            if (dtEmp.Rows.Count > 0)
            {
                lblPlaceTraining.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();

                lblEmpIdTraining.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();

                empNameTraining.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();


                deptNameLabelTraining.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();


                desigNameLabelTraining.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();


                joiningDateLabelTraining.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");


                LocationLabelTraining.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabelTraining.Text = dtEmp.Rows[0]["ReportingToName"].ToString();


                id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();

                // IniKpiTable();
            }
        }
    }


    protected void PartB_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_empId");
        HiddenField hfselfMaster = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("selfMaster");


        string mastrValue = mastrId.Value;
        string emoIdValue = empID.Value;
        
         
       // Response.Redirect("AppraisalPartB.aspx?masterId=" + mastrId.Value + "&empInfoId=" + empID.Value + "&selfMaster=" + selfMaster.Value + "");




        //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('AppraisalPartB.aspx?masterId=" + mastrId.Value + "&empInfoId=" + empID.Value + "&selfMaster=" + hfselfMaster.Value + "' ,'_blank');", true);




        int masterID = int.Parse(mastrValue); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
        int empInfoId = int.Parse(emoIdValue);
        id_Empid.Value = empInfoId.ToString();
        int selfMaster = int.Parse(hfselfMaster.Value);
        id_selfID.Value = selfMaster.ToString();
        DataTable dt = _appPartA.GetAppraisalSelf(Convert.ToInt32(selfMaster));
        id_mastetID.Value = masterID.ToString();
        //DataTable dt3 = _appPartA.GetAppraisalSelfB(Convert.ToInt32(selfMaster));
        DataTable dt3 = _appPartA.GetAppraisalPartB_newd(Convert.ToInt32(masterID));
        btnPartBSUPSave.Visible = true;

        ViewState["PARTB"] = dt3;
        gv_AppraisalPartBSUP.DataSource = dt3;
        gv_AppraisalPartBSUP.DataBind();
        CalculateBPartBSUP();

        CheckImmmiedietSuperVisorPartBSUP(empInfoId.ToString());
        GetEmpInfoByEmpIDPartBSUP(empInfoId);
         MPBSup.Show();

         DataTable dt3r = _appDashboard.GetAppraisalByPermission3(id_mastetID.Value);
         string EmpID = "";
         string Actions = "";
         if (dt3r.Rows.Count > 0)
         {
             EmpID = dt3r.Rows[0]["ForEmpInfoId"].ToString();
             Actions = dt3r.Rows[0]["ActionStatus"].ToString();

           
             if (EmpID != Session["EmpInfoId"].ToString() || Actions == "Approved")
             {
                 btnPartBSUPSave.Visible = true;
             }


             if (EmpID == "0")
             {
                 btnPartBSUPSave.Visible = true;
             }
         }
    }

    private void CalculateBPartBSUP()
    {
        decimal TSetScore = 0;
        decimal TSelfScore = 0;
        decimal TSupervisorScore = 0;

        if (gv_AppraisalPartBSUP.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalPartBSUP.Rows.Count; i++)
            {
                Label SetScore = (Label)gv_AppraisalPartBSUP.Rows[i].FindControl("SetScore");
                TextBox SelfScore = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SelfScore");
                TextBox SupervisorScore = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SupervisorScore");




                if (SetScore.Text == "")
                {
                    TSetScore = TSetScore + 0;
                }
                else
                {
                    TSetScore = TSetScore + Convert.ToDecimal(SetScore.Text.ToString());
                }

                if (SelfScore.Text == "")
                {
                    TSelfScore = TSelfScore + 0;
                }
                else
                {
                    TSelfScore = TSelfScore + Convert.ToDecimal(SelfScore.Text.ToString());
                }



                if (SupervisorScore.Text == "")
                {
                    TSupervisorScore = TSupervisorScore + 0;
                }
                else
                {
                    TSupervisorScore = TSupervisorScore + Convert.ToDecimal(SupervisorScore.Text.ToString());
                }

            }



            Label ddllblTotalSetScore = (Label)gv_AppraisalPartBSUP.FooterRow.FindControl("ddllblTotalSetScore");
            ddllblTotalSetScore.Text = TSetScore.ToString();



            Label lblselfscrore = (Label)gv_AppraisalPartBSUP.FooterRow.FindControl("lblselfscrore");
            lblselfscrore.Text = TSelfScore.ToString();


            Label lblTotalMark = (Label)gv_AppraisalPartBSUP.FooterRow.FindControl("lblTotalMark");
            lblTotalMark.Text = TSupervisorScore.ToString();
        }
    }

    private void CheckImmmiedietSuperVisorPartBSUP(string EmpID)
    {
        AppraislDashboardDAL appraisl = new AppraislDashboardDAL();
        DataTable dtempdata = appraisl.GetEmpInfo(" WHERE EmpInfoId='" + EmpID + "'");
        DataTable CheckFinalApproval = _appPartA.CheckFinalApprovalConditionNotSuppervisor(EmpID.ToString());
        String ddd = "";
        try
        {
            ddd = CheckFinalApproval.Rows[0]["IsAllEmployee"].ToString();
        }
        catch (Exception)
        {

            //throw;
        }


        if (ddd == "True")
        {
            btnPartBSUPSave.Visible = true;

           

        }
        else   if (Session["EmpInfoId"].ToString() != id_Empid.Value)
        {


            if (dtempdata.Rows[0]["ReportingEmpId"].ToString() != Session["EmpInfoId"].ToString().ToString())
            {
                btnPartBSUPSave.Visible = true;
                for (int i = 0; i < gv_AppraisalPartBSUP.Rows.Count; i++)
                {
                     TextBox txtScore = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("Weight");
                    TextBox txtSelfScore = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SelfScore");
                    TextBox supervisorScore = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SupervisorScore");
               //     txtSkillInfo.ReadOnly = true;
                //    txtSupportingEmp.ReadOnly = true;
                    txtScore.ReadOnly = true;
                    txtSelfScore.ReadOnly = true;
                    supervisorScore.ReadOnly = false;
                }
            }
        }
    }

    protected void PartBOwn_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_empId");
        HiddenField hfselfMaster = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("selfMaster");


        string mastrValue = mastrId.Value;


        string emoIdValue = empID.Value;


        if (mastrValue == "0" || mastrValue == "")
        {
            AlertMessageBoxShow("Please Compleate The Functional Part First");
        }
        else
        {
            MPBehavioral.Show();
            int masterID = int.Parse(mastrId.Value); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
            int empInfoId = int.Parse(empID.Value);

            int selfMaster = int.Parse(hfselfMaster.Value);
            id_selfID.Value = selfMaster.ToString();
            DataTable dt = _appPartA.GetAppraisalSelf(Convert.ToInt32(selfMaster));
            id_mastetID.Value = masterID.ToString();
            DataTable dt33 = _appPartA.GetAppraisalPartB(masterID);
            DataTable dt3 = _appPartA.GetAppraisalSelfB(Convert.ToInt32(selfMaster));
            ViewState["PARTB"] = dt3;


            if (dt33.Rows.Count > 0)
            {
                gv_AppraisalPartB.DataSource = dt33;
                gv_AppraisalPartB.DataBind();
                CalculateBehiber();
                totalBehiberSelf();
            }
            else
            {
                gv_AppraisalPartB.DataSource = dt3;
                gv_AppraisalPartB.DataBind();
                CalculateBehiber();
            }

            GetEmpInfoByEmpIDBehavioral(empInfoId);

           


            DataTable dt3r = _appDashboard.GetAppraisalByPermission3(id_mastetID.Value);
            string EmpID = "";
            string Actions = "";
            if (dt3r.Rows.Count > 0)
            {
                EmpID = dt3r.Rows[0]["ForEmpInfoId"].ToString();
                Actions = dt3r.Rows[0]["ActionStatus"].ToString();
                if (EmpID != Session["EmpInfoId"].ToString() || Actions == "Approved")
                {
                    btnBehave.Visible = false;
                }


                if (EmpID == "0")
                {
                    btnBehave.Visible = false;
                }
            }
            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('AppraisalPartBOwn.aspx?masterId=" + mastrId.Value + "&empInfoId=" + empID.Value + "&selfMaster=" + selfMaster.Value  +"' ,'_blank');", true);
        }
        

    }

    private void totalBehiberSelf()
    {
        decimal weightTotal = 0;

        if (gv_AppraisalPartB.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {
                TextBox txtWeight = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SelfScore");




                if (txtWeight.Text == "")
                {
                    weightTotal = weightTotal + 0;
                }
                else
                {
                    weightTotal = weightTotal + Convert.ToDecimal(txtWeight.Text.ToString());
                }




            }



            Label tst2 = (Label)gv_AppraisalPartB.FooterRow.FindControl("lblTotalMarkSelf");
            tst2.Text = weightTotal.ToString();
        }
    }

    private void CalculateBehiber()
    {
        decimal weightTotal = 0;

        if (gv_AppraisalPartB.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {
                Label txtWeight = (Label)gv_AppraisalPartB.Rows[i].FindControl("SetScore");




                if (txtWeight.Text == "")
                {
                    weightTotal = weightTotal + 0;
                }
                else
                {
                    weightTotal = weightTotal + Convert.ToDecimal(txtWeight.Text.ToString());
                }




            }



            Label tst2 = (Label)gv_AppraisalPartB.FooterRow.FindControl("ddllblTotalSetScore");
            tst2.Text = weightTotal.ToString();
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

    protected void Training_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_empId");


        string mastrValue = mastrId.Value;
        string emoIdValue = empID.Value;
        if (mastrValue == "0")
        {
            AlertMessageBoxShow("Please Complete the Functional Area");
        }
        else
        {


            int masterID = int.Parse(mastrValue); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
            int empInfoId = int.Parse(emoIdValue);
            id_mastetID.Value = masterID.ToString();
            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('AppraisalTraining.aspx?masterId=" + mastrId.Value + "&empInfoId=" + empID.Value + "' ,'_blank');", true);



            DataTable dt = _appPartA.GetAppraisalTraining(masterID);
            if (dt.Rows.Count > 0)
            {
                ViewState["TrainingPart"] = dt;
                gv_AppraisalTrainingSUP.DataSource = dt;
                gv_AppraisalTrainingSUP.DataBind();
                for (int i = 0; i < gv_AppraisalTrainingSUP.Rows.Count; i++)
                {
                    DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTrainingSUP.Rows[i].FindControl("QuaterDropDownList1");
                    txt_BranchCountry.SelectedValue = dt.Rows[i]["Quater"].ToString();
                }
            }
            else
            {
               
                IniTable();
             
            }
            CheckImmmiedietSuperVisorTrainingPart(empInfoId.ToString());
            GetEmpInfoByEmpIDTrainSup(Convert.ToInt32(empInfoId.ToString()));
            MPTrainingSUP.Show();

            DataTable dt3 = _appDashboard.GetAppraisalByPermission3(id_mastetID.Value);
            string EmpID = "";
            string Actions = "";
            if (dt3.Rows.Count > 0)
            {
                EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
                Actions = dt3.Rows[0]["ActionStatus"].ToString();
                if (EmpID != Session["EmpInfoId"].ToString() || Actions == "Approved")
                {
                    btnTrainSaveSUP.Visible = true;

                    //for (int i = 0; i < gv_AppraisalTrainingSUP.Rows.Count; i++)
                    //{

                    //    DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTrainingSUP.Rows[i].FindControl("QuaterDropDownList1");
                    //    txt_BranchCountry.Enabled = false;

                    //    TextBox TrainingNeeds = (TextBox)gv_AppraisalTrainingSUP.Rows[i].FindControl("TrainingNeeds");
                    //    TrainingNeeds.ReadOnly = true;
                    //}
                }


                if (EmpID == "0")
                {
                    btnTrainSaveSUP.Visible = true;
                    //for (int i = 0; i < gv_AppraisalTrainingSUP.Rows.Count; i++)
                    //{

                    //    TextBox TrainingNeeds = (TextBox)gv_AppraisalTrainingSUP.Rows[i].FindControl("TrainingNeeds");
                    //    TrainingNeeds.ReadOnly = true;


                    //    DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTrainingSUP.Rows[i].FindControl("QuaterDropDownList1");
                    //    txt_BranchCountry.Enabled = false;
                    //}
                }
            }
           
        }
    }

    private void CheckImmmiedietSuperVisorTrainingPart(string empInfoId)
    {
        AppraislDashboardDAL appraisl = new AppraislDashboardDAL();
        DataTable dtempdata = appraisl.GetEmpInfo(" WHERE EmpInfoId='" + empInfoId + "'");

        DataTable CheckFinalApproval = _appPartA.CheckFinalApprovalConditionNotSuppervisor(empInfoId.ToString());
        String ddd = "";
        try
        {
            ddd = CheckFinalApproval.Rows[0]["IsAllEmployee"].ToString();
        }
        catch (Exception)
        {

            //throw;
        }

        if (ddd == "True")
        {
            btnTrainSaveSUP.Visible = true;
        }


        else  if (Session["EmpInfoId"].ToString() != empInfoId)
        {


            if (dtempdata.Rows[0]["ReportingEmpId"].ToString() != Session["EmpInfoId"].ToString())
            {
                 btnTrainSaveSUP.Visible = true;
                for (int i = 0; i < gv_AppraisalTrainingSUP.Rows.Count; i++)
                {
                    TextBox txtSkillInfo = (TextBox)gv_AppraisalTrainingSUP.Rows[i].FindControl("TrainingNeeds");
                    TextBox txtSupportingEmp = (TextBox)gv_AppraisalTrainingSUP.Rows[i].FindControl("TrainingStart");
                    TextBox txtScore = (TextBox)gv_AppraisalTrainingSUP.Rows[i].FindControl("TrainingEnd");
                    DropDownList ddlQuater = (DropDownList)gv_AppraisalTrainingSUP.Rows[i].FindControl("QuaterDropDownList1");

                  
                    txtSkillInfo.ReadOnly = true;
                    //txtSupportingEmp.ReadOnly = true;
                    //txtScore.ReadOnly = true;
                    ddlQuater.Enabled = false;



                }
            }
        }
    }

    protected void TrainingOwn_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_empId");


        string mastrValue = mastrId.Value;
        string emoIdValue = empID.Value;
        if (mastrValue == "0")
        {
            AlertMessageBoxShow("Please Complete the Functional Area");
        }
        else
        {
            MPTraining.Show();
            int masterID = int.Parse(mastrId.Value); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
            int empInfoId = int.Parse(empID.Value);
            id_mastetID.Value = masterID.ToString();

            DataTable dt = _appPartA.GetAppraisalTraining(masterID);
            if (dt.Rows.Count > 0)
            {
                ViewState["TrainingPart"] = dt;
                gv_AppraisalTraining.DataSource = dt;
                gv_AppraisalTraining.DataBind();
                for (int i = 0; i < gv_AppraisalTraining.Rows.Count; i++)
                {
                    DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");
                    txt_BranchCountry.SelectedValue = dt.Rows[i]["Quater"].ToString();
                }
            }
            else
            {
                IniTable();
            }
            GetEmpInfoByEmpIDTraining(empInfoId);



            DataTable dt3 = _appDashboard.GetAppraisalByPermission3(id_mastetID.Value);
            string EmpID = "";
            string Actions = "";
            if (dt3.Rows.Count > 0)
            {
                EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
                Actions = dt3.Rows[0]["ActionStatus"].ToString();
                if (EmpID != Session["EmpInfoId"].ToString() || Actions == "Approved")
                {
                    btnTrainSave.Visible = false;

                    //for (int i = 0; i < gv_AppraisalTraining.Rows.Count; i++)
                    //{
                    //    gv_AppraisalTraining.Columns[gv_AppraisalTraining.Columns.Count - 1].Visible = false;
                    //    gv_AppraisalTraining.Columns[gv_AppraisalTraining.Columns.Count - 2].Visible = false;
                    //    DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");
                    //    txt_BranchCountry.Enabled = false;

                    //    TextBox TrainingNeeds = (TextBox)gv_AppraisalTraining.Rows[i].FindControl("TrainingNeeds");
                    //    TrainingNeeds.ReadOnly = true;
                    //}
                }


                if (EmpID == "0")
                {
                    btnTrainSave.Visible = false;
                    //for (int i = 0; i < gv_AppraisalTraining.Rows.Count; i++)
                    //{
                    //    gv_AppraisalTraining.Columns[gv_AppraisalTraining.Columns.Count - 1].Visible = false;
                    //    gv_AppraisalTraining.Columns[gv_AppraisalTraining.Columns.Count - 2].Visible = false;


                    //    TextBox TrainingNeeds = (TextBox)gv_AppraisalTraining.Rows[i].FindControl("TrainingNeeds");
                    //    TrainingNeeds.ReadOnly = true;


                    //    DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");
                    //    txt_BranchCountry.Enabled = false;
                    //}
                }
            }
            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('AppraisalTraining.aspx?masterId=" + mastrId.Value + "&empInfoId=" + empID.Value + "' ,'_blank');", true);
        }
    }


    private void IniTable()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("TrainingNeeds", typeof(string)));
        dt.Columns.Add(new DataColumn("quaterID", typeof(string)));
        ////  dt.Columns.Add(new DataColumn("TrainingEnd", typeof(string)));

        dr = dt.NewRow();

        dr["TrainingNeeds"] = "";
        dr["quaterID"] = "";
        //dr["TrainingEnd"] = "";

        dt.Rows.Add(dr);
        ViewState["TrainingPart"] = dt;

        gv_AppraisalTrainingSUP.DataSource = dt;
        gv_AppraisalTrainingSUP.DataBind();
    }


    protected void gv_DocumentUpload_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;

        if ((gv.ShowHeader == true && gv.Rows.Count > 0)
            || (gv.ShowHeaderWhenEmpty == true))
        {
            //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }
    protected void PartC_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_empId");


        LinkButton bButton = (LinkButton)gv_AppraisalBoard.Rows[rowID].FindControl("PartB");
        LinkButton aButton = (LinkButton)gv_AppraisalBoard.Rows[rowID].FindControl("PartA");

        string aValue = aButton.Text;
        btnFinalStatusSave.Visible = true;
        string mastrValue = mastrId.Value;
        string emoIdValue = empID.Value;
        if (mastrValue == "0")
        {
            AlertMessageBoxShow("Please Complete the Functional Area");
        }
        else
        {

            int masterID = int.Parse(mastrValue);
            int empInfoId = int.Parse(emoIdValue);
            decimal partA = 0;
            decimal partB = 0;
            try
            {
                partA = decimal.Parse(aButton.Text);
                partB = decimal.Parse(bButton.Text);
                MPFinalStatus.Show();
            }
            catch (Exception)
            {

                //AlertMessageBoxShow("Please Complete Functional & Behavioural Part");
                //Response.Redirect("AppraisalDashboard.aspx");
                ScriptManager.RegisterStartupScript(this, this.GetType(),
               "alert",
               "alert('Please Complete Functional & Behavioural Part...');",
               true);
            }

            id_mastetID.Value = masterID.ToString();
            id_mastetID.Value = masterID.ToString();

            partA = Convert.ToDecimal(partA);// * Convert.ToDecimal(0.75);
            partAScore.Text = partA.ToString("F");
            partBScore.Text = partB.ToString("F");
            totalScore.Text = (partA + partB).ToString("F");
            decimal total = partA + partB;
            if (total <= 60)
            {
                lblStatus.Text = "Poor";
            }

            if (total >= 61 && total <= 75)
            {
                lblStatus.Text = "Average";
            }

            if (total >= 76 && total <= 80)
            {
                lblStatus.Text = "Good";
            }
            if (total >= 81 && total <= 90)
            {
                lblStatus.Text = "Excellent";
            }

            if (total >= 91)
            {
                lblStatus.Text = "Outstanding";


            }


                  

            DataTable dtDicipinary = _appPartA.GetDiciplinaryActionInfo(" and DCPA.EmpInfoId =" + empID.Value);
             if (dtDicipinary.Rows.Count > 0)
            {

                loadGridView.DataSource = dtDicipinary;
                loadGridView.DataBind();
            }
             else
             {
                 loadGridView.DataSource = null;
                 loadGridView.DataBind(); 
             }

            if (masterID > 0)
            {





                DataTable dt = _appraisalPartBdal.GetAppraiSalFinalStatus(masterID);
                if (dt.Rows.Count > 0)
                {
                    bool gen = Convert.ToBoolean(dt.Rows[0]["GeneralIncrement"].ToString());
                    bool SpecialIncrement = Convert.ToBoolean(dt.Rows[0]["SpecialIncrement"].ToString());
                    bool IsPromotion = Convert.ToBoolean(dt.Rows[0]["IsPromotion"].ToString());
                    bool Pip = !string.IsNullOrEmpty(dt.Rows[0]["Pip"].ToString()) && Convert.ToBoolean(dt.Rows[0]["Pip"].ToString());
                    bool Other = !string.IsNullOrEmpty(dt.Rows[0]["Other"].ToString()) && Convert.ToBoolean(dt.Rows[0]["Other"].ToString());

                    //nothing

                    int step = string.IsNullOrEmpty(dt.Rows[0]["SpecialStep"].ToString())
                        ? 0
                        : Convert.ToInt32(dt.Rows[0]["SpecialStep"].ToString());
                    if (gen == true)
                    {
                        recommend.SelectedValue = "1";
                    }
                    if (SpecialIncrement == true)
                    {
                        recommend.SelectedValue = "2";
                        txtStep.Text = step.ToString();
                    }
                    if (IsPromotion == true)
                    {
                        recommend.SelectedValue = "3";
                    }
                    if (Pip == true)
                    {
                        recommend.SelectedValue = "4";
                    }
                    if (gen == true && IsPromotion == true)
                    {
                        recommend.SelectedValue = "6";
                    }
                    if (Other == true)
                    {
                        recommend.SelectedValue = "6";
                        txtnote.Text = dt.Rows[0]["Note"].ToString();
                    }
                    txtJustification.Text = dt.Rows[0]["Justification"].ToString();

                    if (dt.Rows[0]["DocumentLink"].ToString()!="")
                    {
                        HLDocumentLink.Text = "Preview";
                        lbFileName.Text ="File Name: "+ dt.Rows[0]["FileName"].ToString();
                        HLDocumentLink.NavigateUrl = "../UploadMeetingDocument/" + dt.Rows[0]["DocumentLink"].ToString();
                         
                    }
                    else
                    {
                        lbFileName.Text = "";
                        HLDocumentLink.Text = "No  Document Found";
                        HLDocumentLink.NavigateUrl = "";
                    }
                    recommend_SelectedIndexChanged(recommend, (EventArgs)e);
                }
            }
            btnFinalStatusSave.Visible = true;

            //System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openModal", "window.open('AppraisalFinal.aspx?masterId=" + mastrId.Value + "&empInfoId=" + empID.Value + "&pa=" + aButton.Text + "&pb=" + bButton.Text + "' ,'_blank');", true);
            CheckImmmiedietSuperVisorFinalStatus(empID.Value);
         

            GetEmpInfoByEmpIDFinalStatus(Convert.ToInt32(empID.Value));


            DataTable dt3r = _appDashboard.GetAppraisalByPermission3(id_mastetID.Value);
            string EmpID = "";
            string Actions = "";
            if (dt3r.Rows.Count > 0)
            {
                EmpID = dt3r.Rows[0]["ForEmpInfoId"].ToString();
                Actions = dt3r.Rows[0]["ActionStatus"].ToString();



                if (EmpID != Session["EmpInfoId"].ToString() || Actions == "Approved")
                {
                    btnFinalStatusSave.Visible = true;
                }


                if (EmpID == "0")
                {
                    btnFinalStatusSave.Visible = true;
                }
            }
        }
    }

    private void CheckImmmiedietSuperVisorFinalStatus(string EmpId)
    {
        AppraislDashboardDAL appraisl = new AppraislDashboardDAL();
        DataTable dtempdata = appraisl.GetEmpInfo(" WHERE EmpInfoId='" + EmpId + "'");

        DataTable CheckFinalApproval = _appPartA.CheckFinalApprovalConditionNotSuppervisor(EmpId.ToString());
        String ddd = "";
        try
        {
            ddd = CheckFinalApproval.Rows[0]["IsAllEmployee"].ToString();
        }
        catch (Exception)
        {

            //throw;
        }

        if (ddd == "True")
        {
            btnFinalStatusSave.Visible = true;
        }

        else if (Session["EmpInfoId"].ToString() != EmpId)
        {

            if (dtempdata.Rows[0]["ReportingEmpId"].ToString() !="")
            {
                if (dtempdata.Rows[0]["ReportingEmpId"].ToString() != Session["EmpInfoId"].ToString())
                {
                    btnFinalStatusSave.Visible = true;
                    //for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
                    //{
                    //    TextBox txtSkillInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");
                    //    TextBox txtSupportingEmp = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupportingEmp");
                    //    TextBox txtScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("Weight");
                    //    TextBox txtSelfScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SelfScore");
                    //    TextBox supervisorScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupervisorScore");
                    //    txtSkillInfo.ReadOnly = true;
                    //    txtSupportingEmp.ReadOnly = true;
                    //    txtScore.ReadOnly = true;
                    //    txtSelfScore.ReadOnly = true;
                    //    supervisorScore.ReadOnly = true;
                    //}
                }
            }
        }
    }

    protected void radOp_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (FinancialYearDropDownList.SelectedValue != "")
        {
            string selectedValue = radOp.SelectedValue;
            AppraisalOwn.DataSource = null;
            AppraisalOwn.DataBind();
            gv_AppraisalBoard.DataSource = null;
            gv_AppraisalBoard.DataBind();
            Session["radStatus"] = "";
            Session["radStatus"] = radOp.SelectedValue;


            Session["FinStatus"] = "";
            Session["FinStatus"] = FinancialYearDropDownList.SelectedValue;

            if (selectedValue == "Own")
            {
                DataTable dt = _appDashboard.GetAppraisalDashboardOwn(Convert.ToInt32(Session["EmpInfoId"]),
                    Convert.ToInt32(FinancialYearDropDownList.SelectedValue));


                if (dt.Rows.Count > 0)
                {

                    AppraisalOwn.DataSource = dt;
                    AppraisalOwn.DataBind();



                    DataTable dt2 = _appDashboard.GetAppraisalByPermission2(FinancialYearDropDownList.SelectedValue, Session["EmpInfoId"].ToString());

                    for (int i = 0; i < AppraisalOwn.Rows.Count; i++)
                    {

                        HiddenField id_appraisalMaster = (HiddenField)AppraisalOwn.Rows[i].FindControl("id_appraisalMaster");


                        CheckBox MidYearStatus = (CheckBox)AppraisalOwn.Rows[i].FindControl("IsMidYearStatus");


                        bool IsMidYearStatu = false;
                        try
                        {
                            IsMidYearStatu = Convert.ToBoolean(dt.Rows[0]["IsMidYearStatus"].ToString());
                            MidYearStatus.Checked = IsMidYearStatu;

                        }
                        catch (Exception)
                        {
                            IsMidYearStatu = false;
                            MidYearStatus.Checked = IsMidYearStatu;

                            //throw;
                        }

                        DataTable dt3 = _appDashboard.GetAppraisalByPermission3(id_appraisalMaster.Value);
                        string EmpID = "";
                        string Actions = "";
                        if (dt3.Rows.Count > 0)
                        {
                            EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
                            Actions = dt3.Rows[0]["ActionStatus"].ToString();
                        }
                        //
                        if (EmpID != Session["EmpInfoId"].ToString() || dt2.Rows.Count == 0)
                        {
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = false;
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = false;
                        }

                        //

                        if (Actions.ToString() == "Approved" || EmpID != Session["EmpInfoId"].ToString() || dt2.Rows.Count == 0)
                        {
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = false;
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = false;

                        }
                        if (EmpID == "0")
                        {
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = false;
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = false;
                        }

                        if (dt3.Rows.Count == 0)
                        {
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 1].Visible = true;
                            AppraisalOwn.Columns[AppraisalOwn.Columns.Count - 2].Visible = true;
                        }

                        LinkButton PartAOwn = (LinkButton) AppraisalOwn.Rows[i].FindControl("PartAOwn");

                        if (PartAOwn.Text == "Not Complete")
                        {
                            PartAOwn.CssClass = "btn btn-danger btn-sm ";

                        }

                        if (PartAOwn.Text == "Complete")
                        {
                            PartAOwn.CssClass = "btn btn-success btn-sm";

                        }
                        try
                        {
                            decimal pppp = 0;
                            pppp = Convert.ToDecimal(PartAOwn.Text);
                            if (pppp >= 0)
                            {
                                PartAOwn.CssClass = "btn btn-success btn-sm";

                            }

                        }
                        catch (Exception)
                        {

                        }


                        LinkButton PartBOwn = (LinkButton) AppraisalOwn.Rows[i].FindControl("PartBOwn");

                        if (PartBOwn.Text == "Not Complete")
                        {
                            PartBOwn.CssClass = "btn btn-danger btn-sm ";

                        }

                        if (PartBOwn.Text == "Complete")
                        {
                            PartBOwn.CssClass = "btn btn-success btn-sm";

                        }

                        try
                        {
                            decimal ppppb = 0;
                            ppppb = Convert.ToDecimal(PartBOwn.Text);
                            if (ppppb >= 0)
                            {
                                PartBOwn.CssClass = "btn btn-success btn-sm";

                            }

                        }
                        catch (Exception)
                        {

                        }


                        LinkButton training = (LinkButton) AppraisalOwn.Rows[i].FindControl("training");

                        if (training.Text == "Not Complete")
                        {
                            training.CssClass = "btn btn-danger btn-sm ";

                        }

                        if (training.Text == "Complete")
                        {
                            training.CssClass = "btn btn-success btn-sm";
                            training.Text = "Completed";
                        }


                        try
                        {
                            decimal trainingD = 0;
                            trainingD = Convert.ToDecimal(training.Text);
                            if (trainingD >= 0)
                            {
                                training.CssClass = "btn btn-success btn-sm";

                            }

                        }
                        catch (Exception)
                        {

                        }

                    }
                }

                else
                {
                    AppraisalOwn.DataSource = null;
                    AppraisalOwn.DataBind();
                }





            }
            else
            {
                DataTable DTSupp = _appDashboard.GetAppraisalDashboardSup(Convert.ToInt32(Session["EmpInfoId"]),
                    Convert.ToInt32(FinancialYearDropDownList.SelectedValue));
               
                if (DTSupp.Rows.Count > 0)
                {
                    gv_AppraisalBoard.DataSource = DTSupp;
                    gv_AppraisalBoard.DataBind();
                    //for (int i = 0; i < gv_AppraisalBoard.Rows.Count; i++)
                    //{
                    //    HiddenField id_empId = (HiddenField)gv_AppraisalBoard.Rows[i].FindControl("id_empId");

                    //    if (Session["EmpInfoId"].ToString() == id_empId.Value)
                    //    {
                    //        gv_AppraisalBoard.Rows[i].Visible = false;
                    //    }

                       
                    //}

                    //if (gv_AppraisalBoard.Rows.Count == 0)
                    //{
                    //    gv_AppraisalBoard.DataSource = null;
                    //    gv_AppraisalBoard.DataBind();
                    //}

                    for (int i = 0; i < gv_AppraisalBoard.Rows.Count; i++)
                    {



                        HiddenField id_appraisalMaster = (HiddenField)gv_AppraisalBoard.Rows[i].FindControl("id_appraisalMaster");

                        DataTable dt3 = _appDashboard.GetAppraisalByPermission3(id_appraisalMaster.Value);
                        string EmpID = "";
                        string Actions = "";
                        if (dt3.Rows.Count > 0)
                        {
                            EmpID = dt3.Rows[0]["ForEmpInfoId"].ToString();
                            Actions = dt3.Rows[0]["ActionStatus"].ToString();
                        }
                        //
                        if (EmpID != Session["EmpInfoId"].ToString())
                        {
                            ((RadioButtonList)gv_AppraisalBoard.Rows[i].FindControl("actionRadioButtonList")).Enabled = false;


                            ((TextBox)gv_AppraisalBoard.Rows[i].FindControl("txt_comments")).ReadOnly = true;

                            ((Button)gv_AppraisalBoard.Rows[i].FindControl("btn_Save1")).Enabled = false;
                        }

                        //

                        if (Actions.ToString() == "Approved" || EmpID != Session["EmpInfoId"].ToString())
                        {
                            ((RadioButtonList)gv_AppraisalBoard.Rows[i].FindControl("actionRadioButtonList")).Enabled = false;


                            ((TextBox)gv_AppraisalBoard.Rows[i].FindControl("txt_comments")).ReadOnly = true;

                            ((Button)gv_AppraisalBoard.Rows[i].FindControl("btn_Save1")).Enabled = false;

                        }
                        if (EmpID == "0")
                        {
                            ((RadioButtonList)gv_AppraisalBoard.Rows[i].FindControl("actionRadioButtonList")).Enabled = false;


                            ((TextBox)gv_AppraisalBoard.Rows[i].FindControl("txt_comments")).ReadOnly = true;

                            ((Button)gv_AppraisalBoard.Rows[i].FindControl("btn_Save1")).Enabled = false;

                        }


                        LinkButton PartA = (LinkButton) gv_AppraisalBoard.Rows[i].FindControl("PartA");

                        if (PartA.Text == "Not Complete")
                        {
                            PartA.CssClass = "btn btn-danger btn-sm ";

                        }

                        if (PartA.Text == "Complete")
                        {
                            PartA.CssClass = "btn btn-success btn-sm";

                        }


                        try
                        {
                            decimal pppp = 0;
                            pppp = Convert.ToDecimal(PartA.Text);
                            if (pppp >= 0)
                            {
                                PartA.CssClass = "btn btn-success btn-sm";

                            }
                        }
                        catch (Exception)
                        {

                        }



                        LinkButton PartB = (LinkButton) gv_AppraisalBoard.Rows[i].FindControl("PartB");

                        if (PartB.Text == "Not Complete")
                        {
                            PartB.CssClass = "btn btn-danger btn-sm ";

                        }

                        if (PartB.Text == "Complete")
                        {
                            PartB.CssClass = "btn btn-success btn-sm";

                        }



                        try
                        {
                            decimal ppppb = 0;
                            ppppb = Convert.ToDecimal(PartB.Text);
                            if (ppppb >= 0)
                            {
                                PartB.CssClass = "btn btn-success btn-sm";

                            }

                        }
                        catch (Exception)
                        {

                        }




                        LinkButton PartC = (LinkButton) gv_AppraisalBoard.Rows[i].FindControl("PartC");

                        if (PartC.Text == "Not Complete")
                        {
                            PartC.CssClass = "btn btn-danger btn-sm ";

                        }
                        else
                        {
                            PartC.CssClass = "btn btn-success btn-sm";

                        }


                        LinkButton Training = (LinkButton) gv_AppraisalBoard.Rows[i].FindControl("Training");

                        if (Training.Text == "Not Complete")
                        {
                            Training.CssClass = "btn btn-danger btn-sm ";

                        }

                        if (Training.Text == "Complete")
                        {
                            Training.CssClass = "btn btn-success btn-sm";
                            Training.Text = "Completed";
                        }


                    }




                    RadioTextValue();
                }
                else
                {
                    gv_AppraisalBoard.DataSource = null;
                    gv_AppraisalBoard.DataBind();
                }

                RadioTextValue();
            }
        }
        else
        {
            
        }
    }

    protected void gv_AppraisalBoard_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i = e.Row.RowIndex;
            HiddenField idReport = (HiddenField)e.Row.FindControl("idReport");
            LinkButton a = (LinkButton)e.Row.FindControl("PartA");
            LinkButton b = (LinkButton)e.Row.FindControl("PartB");
            LinkButton c = (LinkButton)e.Row.FindControl("PartC");
            LinkButton t = (LinkButton)e.Row.FindControl("Training");


            //if (idReport.Value.ToString() != Session["EmpInfoId"].ToString())
            //{
            //    a.Enabled = false;
            //    b.Enabled = false;
            //    c.Enabled = false;
            //    t.Enabled = false;

            //}

             
        }
    
    }


    protected void SelfScore_OnTextChanged(object sender, EventArgs e)
    {


        int rowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
        if (Convert.ToDecimal(((Label)gv_AppraisalPartB.Rows[rowIndex].Cells[3].FindControl("Weight")).Text) >=
            Convert.ToDecimal(((TextBox)gv_AppraisalPartB.Rows[rowIndex].Cells[3].FindControl("SelfScore")).Text))
        {
            decimal weightTotal = 0;

            if (gv_AppraisalPartB.Rows.Count > 0)
            {
                for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
                {
                    TextBox txtWeight = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SelfScore");
                    if (txtWeight.Text == "")
                    {
                        weightTotal = weightTotal + 0;
                    }
                    else
                    {
                        weightTotal = weightTotal + Convert.ToDecimal(txtWeight.Text.ToString());
                    }
                }
                Label tst2 = (Label)gv_AppraisalPartB.FooterRow.FindControl("lblTotalMarkSelf");
                tst2.Text = weightTotal.ToString();
            }
        }

        else
        {
            (((TextBox)gv_AppraisalPartB.Rows[rowIndex].Cells[3].FindControl("SelfScore")).Text) = 0.ToString();
            AlertMessageBoxShow("Self-Mark must be less then or Equal to Weight (Number)");
        }






    }

    public void RadioTextValue()
    {
        //string filepath = Path.GetDirectoryName(Request.Path);
        //filepath = filepath.TrimStart('\\');
        //filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
        string filepath = "";
        if (Session["AppPage"] != null)
        {
            filepath = Session["AppPage"].ToString();
        }



        DataTable dtdata = _appDashboard.GetSupervisorEmployeeAppId(Session["EmpInfoId"].ToString(), id_Empid.Value);

        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("Value");
        aDataTable.Columns.Add("Text");

        DataRow dataRow = null;


        //if (Session["ForEmpInfoId"].ToString() != Session["EmpInfoId"].ToString())
        if (dtdata.Rows.Count > 0)
        {
            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Approved";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Return";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);

        }
        else
        {
            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Verified";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Return";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);
        }

        actionRadioButtonList.DataValueField = "Value";
        actionRadioButtonList.DataTextField = "Text";
        actionRadioButtonList.DataSource = aDataTable;
        actionRadioButtonList.DataBind();

        actionRadioButtonList.Items[0].Selected = true;
        try
        {
            if (Session["ForEmpInfoId"].ToString() == Session["EmpInfoId"].ToString())
            {
                actionRadioButtonList.Items[1].Enabled = false;
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {

        try
        {
            DataTable dtFinalApprovalSubmit = new DataTable();
            DataTable dtSuppervisorSubmit = new DataTable();
            int FinalApproveCount = 0;

            DataTable CheckFinalApproval = _appPartA.CheckFinalApprovalConditionNotSuppervisor(id_Empid.Value);


            DataTable dtempdataSup = _appDashboard.GetEmpInfo(" WHERE ReportingEmpId is not null and  EmpInfoId='" + id_Empid.Value + "'");
 

            String ddd = "";
            try
            {
                ddd = CheckFinalApproval.Rows[0]["IsAllEmployee"].ToString();
            }
            catch (Exception)
            {

                //throw;
            }

            if (ddd == "True")
            {


            }
            else if (dtempdataSup.Rows.Count > 0)
            {


                DataTable aDataTable = new DataTable();
                aDataTable.Columns.Add("EmpInfoId");
                aDataTable.Columns.Add("EmpName");
                aDataTable.Columns.Add("EmpMasterCode");
                //DataRow dataRow = null;
                //dataRow = aDataTable.NewRow();
                //dataRow["EmpInfoId"] = "0";
                //dataRow["EmpName"] = "Please Select an Employee.....";
                //dataRow["EmpMasterCode"] = "";
                //aDataTable.Rows.Add(dataRow);
                appDal.ReportingEmpData(id_Empid.Value, aDataTable);


                dtSuppervisorSubmit = aDataTable;

                for (int i = 0; i < dtSuppervisorSubmit.Rows.Count; i++)
                {

                    dtFinalApprovalSubmit = _appPartA.GetFinalApproveByEmpId(id_Empid.Value, dtSuppervisorSubmit.Rows[i]["EmpInfoId"].ToString());
                    if (dtFinalApprovalSubmit.Rows.Count > 0)
                    {
                        FinalApproveCount = FinalApproveCount + 1;
                    }

                }


            }

        DataTable dtdata = _appDashboard.GetSupervisorEmployeeAppIdCheck(id_Empid.Value);


        try
        {
            if (dtdata.Rows.Count > 0 && ddd == "True")
            {
                AppraisalMasterAppLogDAO aMaster = new AppraisalMasterAppLogDAO();
                aMaster.AppraisalMasterId
                    = Convert.ToInt32(id_mastetID.Value);


                if (actionRadioButtonList.SelectedValue == "Review")
                {
                    aMaster.ActionStatus = "Review";

                    bool status = _appDashboard.UpdateContractural(aMaster);
                    if (status)
                    {
                        if (aMaster.ActionStatus == "Review")
                        {


                           


                               DataTable dtempdata = _appDashboard.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(),
                            id_mastetID.Value);
                        DataTable dtempdata2 = _appDashboard.GetEmpInfoPrevious(
                            dtempdata.Rows[0]["PreEmpInfoId"].ToString(), id_mastetID.Value);

                        DataTable dtempdata2empid = _appDashboard.GetEmpIdfromAppraisalInfo(
                        id_mastetID.Value);

                        if (dtempdata2empid.Rows.Count > 0)
                            {
                                AppraisalMasterAppLogDAO appLogDao = new AppraisalMasterAppLogDAO()
                                {
                                    ActionStatus = "Verified",
                                    ApproveDate = DateTime.Now,
                                    ApproveBy = Session["UserId"].ToString(),
                                    PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                                    ForEmpInfoId = Convert.ToInt32(dtempdata2empid.Rows[0]["EmpInfoId"].ToString()),
                                    AppraisalMasterId = aMaster.AppraisalMasterId,
                                    Comments = commentsTextBox.Text

                                };
                                _appDashboard.UpdateAppLog("Review", Session["AppLogId"].ToString());
                                int id = _appDashboard.SaveEmpAppLog(appLogDao);


                                SenMailForApprved(appLogDao.ForEmpInfoId, " Appraisal Setup Approval ",
                                    @"  <br/> Dear Sir, <br/>
A Employee Appraisal Setup is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                                Session["AppLogId"] = null;

                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                    "alert",
                                    "alert('Operation Successfully...');window.location ='OKRBSCApppraisalApprove.aspx';",
                                    true);

                            }

                        }
                        else
                        {
                            //   AlertMessageBoxShow("Your Suppervisor or Final Approver  has not been  set yet. Please contact with HR Department !!!");

                        }
                    }
                }
                else
                {
                    aMaster.ActionStatus = "Approved";

                    bool status = _appDashboard.UpdateContractural(aMaster);
                    if (status)
                    {
                        if (aMaster.ActionStatus == "Approved")
                        {


                            if (hfconfirmstatus.Value == "True")
                            {


                                //DataTable dtempdata = aContractualEmpManageDAL.GetEmpInfo(" WHERE EmpInfoId='" + empInfoId.Value + "'");
                                AppraisalMasterAppLogDAO appLogDao = new AppraisalMasterAppLogDAO()
                                {
                                    ActionStatus = actionRadioButtonList.SelectedValue,
                                    ApproveDate = DateTime.Now,
                                    ApproveBy = Session["UserId"].ToString(),
                                    PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                                    ForEmpInfoId = 0,
                                    AppraisalMasterId = aMaster.AppraisalMasterId,
                                    Comments = commentsTextBox.Text

                                };
                                int id = _appDashboard.SaveEmpAppLog(appLogDao);


                                SenMailForApprved(appLogDao.ForEmpInfoId, " Appraisal Setup Approval ",
                                    @"  <br/> Dear Sir, <br/>
A Employee Appraisal Setup is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                                Session["AppLogId"] = null;

                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                                    "alert",
                                    "alert('Operation Successfully...');window.location ='OKRBSCApppraisalApprove.aspx';",
                                    true);
                            }
                        }
                        else
                        {
                            //   AlertMessageBoxShow("Your Suppervisor or Final Approver  has not been  set yet. Please contact with HR Department !!!");

                        }
                    }
                }

             
            }

            else   if (dtdata.Rows.Count > 0 &&  FinalApproveCount ==1 && dtSuppervisorSubmit.Rows.Count > 0)
            {
                AppraisalMasterAppLogDAO aMaster = new AppraisalMasterAppLogDAO();
                aMaster.AppraisalMasterId
                    = Convert.ToInt32(id_mastetID.Value);




                aMaster.ActionStatus = actionRadioButtonList.SelectedValue;

                bool status = _appDashboard.UpdateContractural(aMaster);
                if (status)
                {

                    if (aMaster.ActionStatus == "Verified")
                    {

                        if (hfconfirmstatus.Value == "True")
                        {
                            DataTable dtempdata =
                                _appDashboard.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                            AppraisalMasterAppLogDAO appLogDao = new AppraisalMasterAppLogDAO()
                            {
                                ActionStatus = actionRadioButtonList.SelectedValue,
                                ApproveDate = DateTime.Now,
                                ApproveBy = Session["UserId"].ToString(),
                                PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                                ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString()),
                                AppraisalMasterId = aMaster.AppraisalMasterId,
                                Comments = commentsTextBox.Text


                            };
                            int id = _appDashboard.SaveEmpAppLog(appLogDao);
                            Session["AppLogId"] = null;
                        }

                        else if (hfconfirmstatus.Value == "False" &&
                           Session["ForEmpInfoId"].ToString() == Session["EmpInfoId"].ToString())
                        {
                            DataTable dtempdata =
                               _appDashboard.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                            AppraisalMasterAppLogDAO appLogDao = new AppraisalMasterAppLogDAO()
                            {
                                ActionStatus = actionRadioButtonList.SelectedValue,
                                ApproveDate = DateTime.Now,
                                ApproveBy = Session["UserId"].ToString(),
                                PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                                ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString()),
                                AppraisalMasterId = aMaster.AppraisalMasterId,
                                Comments = commentsTextBox.Text


                            };
                            int id = _appDashboard.SaveEmpAppLog(appLogDao);
                            Session["AppLogId"] = null;
                        }

                        else
                        {
                            AlertMessageBoxShow("Please Complete All The Procedure ");
                        }
                    }
                    if (aMaster.ActionStatus == "Approved")
                    {


                        if (hfconfirmstatus.Value == "True")
                        {


                            //DataTable dtempdata = aContractualEmpManageDAL.GetEmpInfo(" WHERE EmpInfoId='" + empInfoId.Value + "'");
                            AppraisalMasterAppLogDAO appLogDao = new AppraisalMasterAppLogDAO()
                            {
                                ActionStatus = actionRadioButtonList.SelectedValue,
                                ApproveDate = DateTime.Now,
                                ApproveBy = Session["UserId"].ToString(),
                                PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                                ForEmpInfoId = 0,
                                AppraisalMasterId = aMaster.AppraisalMasterId,
                                Comments = commentsTextBox.Text

                            };
                            int id = _appDashboard.SaveEmpAppLog(appLogDao);


                            SenMailForApprved(appLogDao.ForEmpInfoId, " Appraisal Setup Approval ",
                                @"  <br/> Dear Sir, <br/>
A Employee Appraisal Setup is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                            Session["AppLogId"] = null;
                        }


                        else if (hfconfirmstatus.Value == "False" && Session["ForEmpInfoId"].ToString() == Session["EmpInfoId"].ToString())
                        {


                            //DataTable dtempdata = aContractualEmpManageDAL.GetEmpInfo(" WHERE EmpInfoId='" + empInfoId.Value + "'");
                            AppraisalMasterAppLogDAO appLogDao = new AppraisalMasterAppLogDAO()
                            {
                                ActionStatus = actionRadioButtonList.SelectedValue,
                                ApproveDate = DateTime.Now,
                                ApproveBy = Session["UserId"].ToString(),
                                PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                                ForEmpInfoId = 0,
                                AppraisalMasterId = aMaster.AppraisalMasterId,
                                Comments = commentsTextBox.Text

                            };
                            int id = _appDashboard.SaveEmpAppLog(appLogDao);


                            SenMailForApprved(appLogDao.ForEmpInfoId, " Appraisal Setup Approval ",
                                @"  <br/> Dear Sir, <br/>
A Employee Appraisal Setup is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                            Session["AppLogId"] = null;
                        }
                        else
                        {
                            AlertMessageBoxShow("Please Complete All The Procedure ");
                        }


                    }

                    if (aMaster.ActionStatus == "Review")
                    {
                        DataTable dtempdata = _appDashboard.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(),
                            id_mastetID.Value);
                        DataTable dtempdata2 = _appDashboard.GetEmpInfoPrevious(
                            dtempdata.Rows[0]["PreEmpInfoId"].ToString(), id_mastetID.Value);

                        if (dtempdata2.Rows.Count > 0)
                        {
                            AppraisalMasterAppLogDAO appLogDao = new AppraisalMasterAppLogDAO()
                            {
                                ActionStatus = "Verified",
                                ApproveDate = DateTime.Now,
                                ApproveBy = Session["UserId"].ToString(),
                                PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString()),
                                ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString()),
                                AppraisalMasterId = aMaster.AppraisalMasterId,
                                Comments = commentsTextBox.Text

                            };
                            _appDashboard.UpdateAppLog("Review", Session["AppLogId"].ToString());
                            int id = _appDashboard.SaveEmpAppLog(appLogDao);

                            SenMailForApprved(appLogDao.ForEmpInfoId, " Appraisal Setup Approval ",
                                @"  <br/> Dear Sir, <br/>
A Employee Appraisal Setup is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                            Session["AppLogId"] = null;
                        }
                        else
                        {
                            ShowMessageBox("Please select Approval Status Approved  this!!!");
                        }
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successfully...');window.location ='OKRBSCApppraisalApprove.aspx';",
                        true);

                }

            }
            else
            {
                AlertMessageBoxShow("Your Suppervisor or Final Approver  has not been  set yet. Please contact with HR Department !!!");


            }
      
        }
        catch (Exception)
        {
            AlertMessageBoxShow("Your Suppervisor or Final Approver  has not been  set yet. Please contact with HR Department !!!");

            
            //throw;
        }
        }
        catch (Exception)
        {

            AlertMessageBoxShow("Your Suppervisor or Final Approver  has not been  set yet. Please contact with HR Department !!!");
        }
        

    }

    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public static bool SenMailForApprved(int forEmpID, string mSubject, string mBody)
    {



        string ForMailAddress = "";
        using (var db = new HRIS_SMCEntities())
        {
            var GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                  where t.EmpInfoId == forEmpID
                                  select t).FirstOrDefault();

            if (GetMailAddress != null)
            {
                ForMailAddress = GetMailAddress.OfficialEmail;
            }



        }

        if (ForMailAddress != "")
        {
            try
            {
                // Set TLS 1.2 (Office 365 requires this)
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (SmtpClient smtpClient = new SmtpClient("shuvosmtp.office365.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;

                    // Use your actual Office 365 credentials
                    smtpClient.Credentials = new NetworkCredential("shuvono-reply@smc-bd.org", "vfwzmbxprdmqhhln");

                    // Set timeout (in milliseconds)
                    smtpClient.Timeout = 20000;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("shuvono-reply@smc-bd.org");
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(ForMailAddress);
                        mailMessage.Subject = mSubject;
                        mailMessage.Body =
                   "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +
                   WebUtility.HtmlDecode(mBody)
                   +
                   "</div>";
                        mailMessage.IsBodyHtml = true;

                        smtpClient.Send(mailMessage);

                    }
                }
            }
            catch (Exception ex)
            {

                if (ex.InnerException != null)
                {

                }
            }





            System.Threading.Thread.Sleep(100);
        }


        return true;
    }


    protected void btn_Save1_OnClick(object sender, EventArgs e)
    {
        Button lb = (Button)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        bool confirmstatus = false;
        try
        {
            confirmstatus = Convert.ToBoolean(gv_AppraisalBoard.DataKeys[rowID][1].ToString());
        }
        catch (Exception)
        {
            
            
        }
        if (confirmstatus)
        {





            HiddenField mastrId = (HiddenField) gv_AppraisalBoard.Rows[rowID].FindControl("id_appraisalMaster");
            HiddenField empID = (HiddenField) gv_AppraisalBoard.Rows[rowID].FindControl("id_empId");
            TextBox comments = (TextBox) gv_AppraisalBoard.Rows[rowID].FindControl("txt_comments");
            RadioButtonList actionRadioButtonList =
                (RadioButtonList) gv_AppraisalBoard.Rows[rowID].FindControl("actionRadioButtonList");

            AppraisalMasterAppLogDAO aMaster = new AppraisalMasterAppLogDAO();
            aMaster.AppraisalMasterId
                = Convert.ToInt32(mastrId.Value);
            aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = _appDashboard.UpdateContractural(aMaster);
            if (status)
            {
                if (aMaster.ActionStatus == "Verified")
                {
                    DataTable dtempdata =
                        _appDashboard.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    AppraisalMasterAppLogDAO appLogDao = new AppraisalMasterAppLogDAO()
                    {
                        ActionStatus = actionRadioButtonList.SelectedValue,
                        ApproveDate = DateTime.Now,
                        ApproveBy = Session["UserId"].ToString(),
                        PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                        ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString()),
                        AppraisalMasterId = aMaster.AppraisalMasterId,
                        Comments = comments.Text,

                    };
                    int id = _appDashboard.SaveEmpAppLog(appLogDao);
                }
                else if (aMaster.ActionStatus == "Approved")
                {
                    //DataTable dtempdata = aContractualEmpManageDAL.GetEmpInfo(" WHERE EmpInfoId='" + empInfoId.Value + "'");
                    AppraisalMasterAppLogDAO appLogDao = new AppraisalMasterAppLogDAO()
                    {
                        ActionStatus = actionRadioButtonList.SelectedValue,
                        ApproveDate = DateTime.Now,
                        ApproveBy = Session["UserId"].ToString(),
                        PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                        ForEmpInfoId = 0,
                        AppraisalMasterId = aMaster.AppraisalMasterId,
                        Comments = comments.Text,

                    };
                    int id = _appDashboard.SaveEmpAppLog(appLogDao);
                    //_appDashboard.SaveAppraisalMasterFromAppraisalSelf(aMaster.AppraisalMasterId.ToString());
                }
                else if (aMaster.ActionStatus == "Review")
                {
                    DataTable dtempdata = _appDashboard.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(),
                        mastrId.Value);
                    DataTable dtempdata2 = _appDashboard.GetEmpInfoPrevious(
                        dtempdata.Rows[0]["PreEmpInfoId"].ToString(), mastrId.Value);

                    if (dtempdata2.Rows.Count > 0)
                    {
                        AppraisalMasterAppLogDAO appLogDao = new AppraisalMasterAppLogDAO()
                        {
                            ActionStatus = "Verified",
                            ApproveDate = DateTime.Now,
                            ApproveBy = Session["UserId"].ToString(),
                            PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString()),
                            ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString()),
                            AppraisalMasterId = aMaster.AppraisalMasterId,
                            Comments = comments.Text,

                        };
                        _appDashboard.UpdateAppLog("Review", gv_AppraisalBoard.DataKeys[rowID][0].ToString());
                        int id = _appDashboard.SaveEmpAppLog(appLogDao);
                    }
                    else
                    {
                        AlertMessageBoxShow("Please select Approval Status Approved  this!!!");
                    }
                }


            }
            //Session["AppLogId"] = null;
            gv_AppraisalBoard.DataSource = _appDashboard.GetAppraisalDashboardSup(Convert.ToInt32(Session["EmpInfoId"]),Convert.ToInt32(FinancialYearDropDownList.SelectedValue));
            gv_AppraisalBoard.DataBind();
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Operation Successful...');window.location ='OKRBSCApppraisalApprove.aspx';",
                       true);
        }
        else
        {
            AlertMessageBoxShow("Please Complete All The Procedure ");
        }
    }

    protected void ShowPopup(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;

        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);

        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;



        HiddenField mastrId = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_appraisalMaster");

        DataTable dtdata = _appDashboard.GetAllComments(mastrId.Value);
        GridView1.DataSource = dtdata;
        GridView1.DataBind();


    }

    protected void ShowPopup1(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;

        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);

        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;



        HiddenField mastrId = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_appraisalMaster");

        DataTable dtdata = _appDashboard.GetAllComments(mastrId.Value);
        GridView1.DataSource = dtdata;
        GridView1.DataBind();


    }


    protected void FinancialYearDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        radOp_OnSelectedIndexChanged(null, null);
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        mpe_1.Hide();

    }

    protected void btnFunctionalSave_OnClick(object sender, EventArgs e)
    {
        if (ValidationlFunctional() == true)
        {
            List<AppraisalFunctionalAreaBSC> functional = new List<AppraisalFunctionalAreaBSC>();

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
                    AppraisalFunctionalAreaBSC area = new AppraisalFunctionalAreaBSC();
                    area.BSCAppraisalSelfFucAreaId = Convert.ToInt32(gv_AppraisalFunc.DataKeys[i][1].ToString());
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
            aMaster.FinancialYearId = Convert.ToInt32(FinancialYearDropDownList.SelectedValue);
            aMaster.AppraisalSelfMasterId = Convert.ToInt32(id_selfID.Value);


            bool result = false;
            if (functional.Count > 0)
            {
                int pk = _appPartA.SaveAppraisalMaster(aMaster, Session["UserId"].ToString(),  "HR");
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
                    "alert('Operation Successful...');",
                    true);
                int empId = int.Parse(Request.QueryString["EmpInfoId"]);
                string finYearDes = (Request.QueryString["FinancialYearDesc"]);
                string M = (Request.QueryString["M"]);
                LoadGridList(empId, finYearDes);
                mpe_1.Hide();
            }
            else
            {
                AlertMessageBoxShow("Operation Failed");
            }

        }
    }

    private bool ValidationlFunctional()
    {
        bool isVAlid = true;
        if (gv_AppraisalFunc.Rows.Count == 0)
        {
            aShowMessage.ShowMessageBox("Appraisal Functional Part Required ", this);
        }
        for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
        {

            TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtselfMark");

            


            if (txtMark.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Appraisal Functional Self Mark Required ", this);
                break;
            }


        }
        return isVAlid;
    }

    protected void btnFunctionalCancel_OnClick(object sender, EventArgs e)
    {
        
    }
    protected void CalculateTotalFunc()
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


    protected void CalculateTotalFuncsupp()
    {
        decimal weightTotal = 0;
        decimal markTotal = 0;
        if (gv_AppraisalFuncSUP.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalFuncSUP.Rows.Count; i++)
            {


                TextBox txtMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtselfMark");



                if (txtMark.Text == "")
                {
                    markTotal = markTotal + 0;
                }
                else
                {
                    markTotal = markTotal + Convert.ToDecimal(txtMark.Text.ToString());
                }




            }

            Label tst = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblselfMark");
            tst.Text = markTotal.ToString();

        }
    }
    protected void txtselfMark_OnTextChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
        if (Convert.ToDecimal(((Label)gv_AppraisalFunc.Rows[rowIndex].Cells[3].FindControl("txtWeight")).Text) >=
            Convert.ToDecimal(((TextBox)gv_AppraisalFunc.Rows[rowIndex].Cells[3].FindControl("txtselfMark")).Text))
        {
            CalculateTotalFunc();
        }

        else
        {
            (((TextBox)gv_AppraisalFunc.Rows[rowIndex].Cells[3].FindControl("txtselfMark")).Text) = 0.ToString();
            AlertMessageBoxShow("Self-Mark must be less then or Equal to Weight (Number)");
        }

    }

    protected void btnBehavioralClose_Click(object sender, EventArgs e)
    {
        MPBehavioral.Hide();
    }

    protected void btnTrainingClose_Click(object sender, EventArgs e)
    {
        MPTraining.Hide();
    }


    protected void btn_Add_OnClick(object sender, EventArgs e)
    {
        if (ViewState["TrainingPart"] == null)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("TrainingNeeds", typeof(string)));
            dt.Columns.Add(new DataColumn("quaterID", typeof(string)));
            //   dt.Columns.Add(new DataColumn("TrainingEnd", typeof (string)));

            dr = dt.NewRow();

            dr["TrainingNeeds"] = "";
            dr["quaterID"] = "";
            //  dr["TrainingEnd"] = "";

            dt.Rows.Add(dr);
            ViewState["TrainingPart"] = dt;

            gv_AppraisalTrainingSUP.DataSource = dt;
            gv_AppraisalTrainingSUP.DataBind();
        }
        else
        {
            DataTable dtCurrentTable = (DataTable)ViewState["TrainingPart"];

            DataRow drCurrentRow = null;

            drCurrentRow = dtCurrentTable.NewRow();



            dtCurrentTable.Rows.Add(drCurrentRow);


            ViewState["TrainingPart"] = dtCurrentTable;

            for (int i = 0; i < gv_AppraisalTrainingSUP.Rows.Count; i++)
            {
                TextBox txtSkillInfo = (TextBox)gv_AppraisalTrainingSUP.Rows[i].FindControl("TrainingNeeds");
                DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTrainingSUP.Rows[i].FindControl("QuaterDropDownList1");



                dtCurrentTable.Rows[i]["TrainingNeeds"] = txtSkillInfo.Text.Trim().ToString() == "" ? "" : txtSkillInfo.Text.Trim().ToString();
                dtCurrentTable.Rows[i]["quaterID"] = txt_BranchCountry.SelectedValue;


            }

            gv_AppraisalTrainingSUP.DataSource = dtCurrentTable;
            gv_AppraisalTrainingSUP.DataBind();
            for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
            {
                DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTrainingSUP.Rows[i].FindControl("QuaterDropDownList1");
                txt_BranchCountry.SelectedValue = dtCurrentTable.Rows[i]["quaterID"].ToString();
            }
        }
    }



    protected void lb_Remove_OnClick(object sender, EventArgs e)
    {
        if (ViewState["TrainingPart"] != null && gv_AppraisalTrainingSUP.Rows.Count > 1)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["TrainingPart"];

            if (dt.Rows.Count == 0)
            {
                ViewState["TrainingPart"] = null;
            }
            else
            {
                ViewState["TrainingPart"] = dt;
            }
            for (int i = 0; i < gv_AppraisalTrainingSUP.Rows.Count; i++)
            {
                TextBox TrainingNeeds = (TextBox)gv_AppraisalTrainingSUP.Rows[i].FindControl("TrainingNeeds");
                DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTrainingSUP.Rows[i].FindControl("QuaterDropDownList1");



                try
                {
                    dt.Rows[i]["TrainingNeeds"] = TrainingNeeds.Text.Trim().ToString() == "" ? "" : TrainingNeeds.Text.Trim().ToString();
                }
                catch (Exception)
                {
                 //   dt.Rows[i]["TrainingNeeds"] = "";
                    //throw;
                }
                try
                {
                    dt.Rows[i]["quaterID"] = txt_BranchCountry.SelectedValue;
                }
                catch (Exception)
                {
                    
                    //throw;
                }


            }
            dt.Rows.Remove(dt.Rows[rowID]);
            gv_AppraisalTrainingSUP.DataSource = dt;
            gv_AppraisalTrainingSUP.DataBind();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTrainingSUP.Rows[i].FindControl("QuaterDropDownList1");
                txt_BranchCountry.SelectedValue = dt.Rows[i]["quaterID"].ToString();
            }

        }
    }
    private OKRBSCAppraisalPartBDAL _appraisalPartBdal = new OKRBSCAppraisalPartBDAL();

    protected void btnBehave_OnClick(object sender, EventArgs e)
    {



        if (ValidationBehave() == true)
        {
            List<AppraisalBehaveArea> aList = new List<AppraisalBehaveArea>();

            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {
                TextBox txtSkillInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");
                TextBox txtSupportingEmp = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupportingEmp");
                Label txtScore = (Label)gv_AppraisalPartB.Rows[i].FindControl("Weight");
                Label SetScore = (Label)gv_AppraisalPartB.Rows[i].FindControl("SetScore");
                TextBox txtSelfScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SelfScore");

                if (txtSkillInfo.Text.Trim().ToString() != "" && txtSelfScore.Text.Trim().ToString() != "")
                {
                    AppraisalBehaveArea area = new AppraisalBehaveArea();
                    area.AppraisalMasterId = Convert.ToInt32(id_mastetID.Value);
                    area.AppraisalSelfMasterId = Convert.ToInt32(id_selfID.Value);
                    area.SkillInfo = txtSkillInfo.Text.Trim().ToString();
                    area.SupportingEmp = txtSupportingEmp.Text.Trim().ToString();
                    area.Score = Convert.ToDecimal(txtScore.Text.ToString());
                    area.SetScore = Convert.ToDecimal(SetScore.Text.ToString());
                    area.SelfScore = Convert.ToDecimal(txtSelfScore.Text.ToString());
                    area.Comments = "";
                    aList.Add(area);

                }


            }
            if (aList.Count > 0)
            {
                bool result = _appraisalPartBdal.SaveAppraisalPartB(aList, Convert.ToInt32(id_selfID.Value));
                if (result == true)
                {


                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                      "alert",
                      "alert('Operation Successful...');window.location ='OKRBSCApppraisalApprove.aspx';",
                      true);
                }
                else
                {
                    AlertMessageBoxShow("Operation Failed");
                }
            }
        }

    }

    private bool ValidationBehave()
    {
        bool isVAlid = true;
        if (gv_AppraisalPartB.Rows.Count == 0)
        {
            aShowMessage.ShowMessageBox("Appraisal Behavioral Part Required ", this);
        }
        for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
        {

            TextBox txtSelfScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SelfScore");




            if (txtSelfScore.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Appraisal Behavioral Score Required ", this);
                break;
            }


        }
        return isVAlid;
    }


    private bool ValidationBehaveSupp()
    {
        bool isVAlid = true;
        if (gv_AppraisalPartBSUP.Rows.Count == 0)
        {
            aShowMessage.ShowMessageBox("Appraisal Behavioral Part Required ", this);
        }
        for (int i = 0; i < gv_AppraisalPartBSUP.Rows.Count; i++)
        {

            TextBox SupervisorScore = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SupervisorScore");




            if (SupervisorScore.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Appraisal Behavioral Supervisor Score Required ", this);
                break;
            }


        }
        return isVAlid;
    }

    protected void btnTrainSave_OnClick(object sender, EventArgs e)
    {
        if (ValidationTrain())
        {


            List<AppraisalTrainingNeeds> aList = new List<AppraisalTrainingNeeds>();
            for (int i = 0; i < gv_AppraisalTraining.Rows.Count; i++)
            {
                TextBox txtSkillInfo = (TextBox) gv_AppraisalTraining.Rows[i].FindControl("TrainingNeeds");
                TextBox txtSupportingEmp = (TextBox) gv_AppraisalTraining.Rows[i].FindControl("TrainingStart");
                TextBox txtScore = (TextBox) gv_AppraisalTraining.Rows[i].FindControl("TrainingEnd");
                DropDownList ddlQuater = (DropDownList) gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");

                if (txtSkillInfo.Text.Trim().ToString() != "")
                    //&& txtSupportingEmp.Text.Trim().ToString() != "" &&
                    //txtScore.Text.Trim().ToString() != ""
                {

                    AppraisalTrainingNeeds appraisal = new AppraisalTrainingNeeds();
                    appraisal.AppraisalMasterId = Convert.ToInt32(id_mastetID.Value);
                    appraisal.TrainingNeeds = txtSkillInfo.Text.Trim().ToString();
                    appraisal.TrainingStart = Convert.ToDateTime(DateTime.Now);
                    appraisal.TrainingEnd = Convert.ToDateTime(DateTime.Now);
                    appraisal.Quater = Convert.ToInt32(ddlQuater.SelectedValue);
                    aList.Add(appraisal);
                }



            }

            if (aList.Count > 0)
            {
                bool result = _appraisalPartBdal.SaveTrainingNeeds(aList);
                if (result == true)
                {




                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successful...');window.location ='OKRBSCApppraisalApprove.aspx';",
                        true);



                }
                else
                {
                    AlertMessageBoxShow("Operation Failed");
                }
            }
        }
    }


    protected void isActiveCheckBox_OnCheckedChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;
        CheckBox chkIsActive = (CheckBox)gv_AppraisalFuncSUP.Rows[rowIndex].FindControl("isActiveCheckBox");
        LinkButton btnFunction = (LinkButton)gv_AppraisalFuncSUP.Rows[rowIndex].FindControl("btnFunction");
        if (chkIsActive.Checked == false)
        {
            btnFunction.Visible = true;
        }
        else
        {
            btnFunction.Visible = false;

        }

        try
        {
            if (Convert.ToDecimal(((TextBox)gv_AppraisalFuncSUP.Rows[rowIndex].Cells[3].FindControl("txtWeight")).Text) >=
           Convert.ToDecimal(((TextBox)gv_AppraisalFuncSUP.Rows[rowIndex].Cells[3].FindControl("txtselfMark")).Text))
            {

            }

            else
            {
                (((TextBox)gv_AppraisalFuncSUP.Rows[rowIndex].Cells[3].FindControl("txtselfMark")).Text) = 0.ToString();
                AlertMessageBoxShow("Self-Mark must be less then or Equal to Weight (Number)");
            }
        }
        catch (Exception)
        {
            ((TextBox)gv_AppraisalFuncSUP.Rows[rowIndex].Cells[3].FindControl("txtWeight")).Text = "0";
           // AlertMessageBoxShow("Please Fill Weight (Number)");
            //throw;
        }

         
        CalculateBFuncSUP();
    }
    protected void CalculateTotalsupp()
    {
        decimal weightTotal = 0;
        decimal markTotal = 0;
        if (gv_AppraisalFuncSUP.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalFuncSUP.Rows.Count; i++)
            {
                CheckBox chkIsActive = (CheckBox)gv_AppraisalFuncSUP.Rows[i].FindControl("isActiveCheckBox");
                if (chkIsActive.Checked)
                {


                    TextBox txtWeight = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtWeight");

                    TextBox txtMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMark");

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

            }


            Label tst = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblTotalWeight");
            tst.Text = weightTotal.ToString();

            //Label tst2 = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalMark");
            //tst2.Text = markTotal.ToString();
        }
    }


    protected void btnFunction_OnClick(object sender, EventArgs e)
    {
        if (ViewState["KPIFUNCSUP"] == null)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("KpiInfo", typeof(string)));
            dt.Columns.Add(new DataColumn("KpiWeight", typeof(string)));
            dt.Columns.Add(new DataColumn("KpiWeightPer", typeof(string)));
            dt.Columns.Add(new DataColumn("Target", typeof(string)));
            dt.Columns.Add(new DataColumn("TargetPer", typeof(string)));
            dt.Columns.Add(new DataColumn("Deadline", typeof(string)));
            dt.Columns.Add(new DataColumn("MidYearStatus", typeof(string)));

            dt.Columns.Add(new DataColumn("SupervisorMark", typeof(string)));
            dt.Columns.Add(new DataColumn("SelfMark", typeof(string)));
            dt.Columns.Add(new DataColumn("IsActive", typeof(bool)));
            dr = dt.NewRow();

            dr["KpiInfo"] = "";
            dr["KpiWeight"] = "";
            dr["KpiWeightPer"] = "";
            dr["Target"] = "";
            dr["TargetPer"] = "";
            dr["Deadline"] = "";
            dr["MidYearStatus"] = "";

            dr["SelfMark"] = "";
            dr["SupervisorMark"] = "";


            dr["IsActive"] = "False";

            dr["SelfMark"] = "";
            dt.Rows.Add(dr);
            ViewState["KPIFUNC"] = dt;

            gv_AppraisalFuncSUP.DataSource = dt;
            gv_AppraisalFuncSUP.DataBind();
        }

        else
        {
            DataTable dtCurrentTable = (DataTable)ViewState["KPIFUNCSUP"];

            DataRow drCurrentRow = null;

            drCurrentRow = dtCurrentTable.NewRow();

            drCurrentRow["IsActive"] = "True";

            dtCurrentTable.Rows.Add(drCurrentRow);


            ViewState["KPIFUNC"] = dtCurrentTable;

            for (int i = 0; i < gv_AppraisalFuncSUP.Rows.Count; i++)
            {
                TextBox tbKpi = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtKpi");
                TextBox txtWeight = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtWeight");
                TextBox txtWeightPer = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtWeightPer");
                TextBox txtTarget = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtTarget");
                TextBox txtTargetPer = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtTargetPer");
                DropDownList txtMidStatus = (DropDownList)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMidStatus");
                CheckBox chkisactive = (CheckBox)gv_AppraisalFuncSUP.Rows[i].FindControl("isActiveCheckBox");

                TextBox txtMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMark");

                TextBox txtselfMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtselfMark");
                TextBox txtDeadLine = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtDeadLine");


                dtCurrentTable.Rows[i]["KpiInfo"] = tbKpi.Text.Trim().ToString() == ""
                    ? ""
                    : tbKpi.Text.Trim().ToString();


                tbKpi.ReadOnly = false;

                try
                {
                    dtCurrentTable.Rows[i]["KpiWeight"] = txtWeight.Text.Trim().ToString() == ""
                 ? ""
                 : txtWeight.Text.Trim().ToString();
                }
                catch (Exception)
                {
                    dtCurrentTable.Rows[i]["KpiWeight"] = "0";

                }



                try
                {
                    dtCurrentTable.Rows[i]["KpiWeightPer"] = txtWeightPer.Text.Trim().ToString() == ""
                  ? ""
                  : txtWeightPer.Text.Trim().ToString();
                }
                catch (Exception)
                {
                    dtCurrentTable.Rows[i]["KpiWeightPer"] = "0";
                    //throw;
                }


                try
                {
                    dtCurrentTable.Rows[i]["KpiWeightPer"] = txtWeightPer.Text.Trim().ToString() == ""
                  ? ""
                  : txtWeightPer.Text.Trim().ToString();
                }
                catch (Exception)
                {
                    dtCurrentTable.Rows[i]["KpiWeightPer"] = "0";
                    //throw;
                }



                try
                {
                    dtCurrentTable.Rows[i]["Target"] = txtTarget.Text.Trim().ToString() == ""
                 ? ""
                 : txtTarget.Text.Trim().ToString();
                }
                catch (Exception)
                {

                    dtCurrentTable.Rows[i]["Target"] = "0";
                }

                try
                {
                    dtCurrentTable.Rows[i]["TargetPer"] = txtTargetPer.Text.Trim().ToString() == ""
                  ? ""
                  : txtTargetPer.Text.Trim().ToString();
                }
                catch (Exception)
                {

                    dtCurrentTable.Rows[i]["TargetPer"] = "0";
                }

                dtCurrentTable.Rows[i]["Deadline"] = txtDeadLine.Text.Trim().ToString() == ""
                    ? ""
                    : txtDeadLine.Text.Trim().ToString();
                try
                {
                    dtCurrentTable.Rows[i]["SelfMark"] = txtselfMark.Text.Trim().ToString() == ""
                ? 0
                : Convert.ToDecimal(txtselfMark.Text.Trim().ToString());
                }
                catch (Exception)
                {

                    dtCurrentTable.Rows[i]["SelfMark"] = "0";
                }

                try
                {
                    dtCurrentTable.Rows[i]["SupervisorMark"] = txtMark.Text.Trim().ToString() == ""
                ? 0
                : Convert.ToDecimal(txtMark.Text.Trim().ToString());
                }
                catch (Exception)
                {

                    dtCurrentTable.Rows[i]["SupervisorMark"] = "0";
                }

                dtCurrentTable.Rows[i]["IsActive"] = chkisactive.Checked;
                try
                {
                    dtCurrentTable.Rows[i]["MidYearStatus"] = txtMidStatus.SelectedItem.Text.Trim().ToString();
                }
                catch (Exception)
                {

                    //throw;
                }

            }

            gv_AppraisalFuncSUP.DataSource = dtCurrentTable;
            gv_AppraisalFuncSUP.DataBind();

            CalculateTotalsupp();
            CalculateTotalFuncsupp();
        }

        for (int i = 0; i < gv_AppraisalFuncSUP.Rows.Count; i++)
        {
            TextBox tbKpi = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtKpi");
            TextBox txtWeight = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtWeight");
            TextBox txtWeightPer = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtWeightPer");
            TextBox txtTarget = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtTarget");
            TextBox txtTargetPer = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtTargetPer");
            DropDownList txtMidStatus = (DropDownList)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMidStatus");
            CheckBox chkisactive = (CheckBox)gv_AppraisalFuncSUP.Rows[i].FindControl("isActiveCheckBox");

 

            TextBox txtselfMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtselfMark");
            TextBox txtDeadLine = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtDeadLine");

            int ii = 0;
            try
            {
                ii = Convert.ToInt32(gv_AppraisalFuncSUP.DataKeys[i][0].ToString());
            }
            catch (Exception)
            {
                ii = 0;
                //throw;
            }
            if (ii == 0)
            {
                txtDeadLine.Enabled = true;
                txtMidStatus.Enabled = true;
                tbKpi.ReadOnly = false;
                txtWeight.ReadOnly = false;
                txtTarget.ReadOnly = false;
                txtselfMark.ReadOnly = false;
            }


        }
    }
    private bool ValidationTrain()
    {
        bool isVAlid = true;
        if (gv_AppraisalTraining.Rows.Count == 0)
        {
            aShowMessage.ShowMessageBox("Appraisal Training Part Required ", this);
        }
        for (int i = 0; i < gv_AppraisalTraining.Rows.Count; i++)
        {

            TextBox txtSkillInfo = (TextBox)gv_AppraisalTraining.Rows[i].FindControl("TrainingNeeds");




            if (txtSkillInfo.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Appraisal Training Needs Required ", this);
                break;
            }


        }
        return isVAlid;
    }

    protected void ViewComments_OnClick(object sender, EventArgs e)
    {
         LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)AppraisalOwn.Rows[rowID].FindControl("id_empId");


        string mastrValue = mastrId.Value;
        string emoIdValue = empID.Value;
        if (mastrValue == "0")
        {
            AlertMessageBoxShow("Please Complete all Function");
        }
        else
        {
            mpComm.Show();
            int masterID = int.Parse(mastrId.Value); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
            int empInfoId = int.Parse(empID.Value);
            id_mastetID.Value = masterID.ToString();

            DataTable dt = _appPartA.GetViewComments(masterID);
            if (dt.Rows.Count > 0)
            {
                gv_Versions.DataSource = dt;
                gv_Versions.DataBind();
            }
        }
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("KPIInformationView.aspx");
    }

    protected void btncommClose_Click(object sender, EventArgs e)
    {
        mpComm.Hide();
    }

    protected void btnFunctionalSupClose_Click(object sender, EventArgs e)
    {
        mpFunctionalSup.Hide();
    }

    protected void SupervisorMark_OnTextChanged(object sender, EventArgs e)
    {



        int rowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
        Label lblAutoCalculation = (Label)gv_AppraisalFuncSUP.Rows[rowIndex].FindControl("lblAutoCalculation");
        Label lblAutoCalculationPer = (Label)gv_AppraisalFuncSUP.Rows[rowIndex].FindControl("lblAutoCalculationPer");
        TextBox txtMark = (TextBox)gv_AppraisalFuncSUP.Rows[rowIndex].FindControl("txtMark");
        TextBox txtWeight = (TextBox)gv_AppraisalFuncSUP.Rows[rowIndex].FindControl("txtWeight");

        decimal AutoCalculation = 0;
        try
        {
            AutoCalculation = Convert.ToDecimal(lblAutoCalculation.Text);
        }
        catch
        {

        }

        decimal selfMark = 0;
        try
        {
            selfMark = Convert.ToDecimal(txtMark.Text);
        }
        catch
        {

        }
        decimal KpiWeight = 0;
        try
        {
            KpiWeight = Convert.ToDecimal(txtWeight.Text);
        }
        catch
        {

        }
        if (selfMark > AutoCalculation)
        {
            // Optionally, you can reset the value of txtselfMark to AutoCalculation
            txtMark.Text = AutoCalculation.ToString();
            aShowMessage.ShowMessageBox("Supervisor Mark cannot be greater than Maximum Supervisor-Mark.", this);
            // Or, show an error message to inform the user (if you have a label for that):
            // lblError.Text = "Self mark cannot be greater than the auto-calculated value.";
        }


        try
        {
            selfMark = Convert.ToDecimal(txtMark.Text);
        }
        catch
        {

        }

        decimal PerCal = 0;


        try
        {
            PerCal = (selfMark / KpiWeight) * 100;
            PerCal = Math.Round(PerCal, 2);
        }
        catch
        {

        }
        lblAutoCalculationPer.Text = "Achi: " + PerCal.ToString() + "%";

        CalculateBFuncSUP();
    }

    protected void txtWeightSUP_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        TextBox txtweight = (TextBox)gv_AppraisalFuncSUP.Rows[rowID].FindControl("txtWeight");
        TextBox txtweightper = (TextBox)gv_AppraisalFuncSUP.Rows[rowID].FindControl("txtWeightPer");

        double weightNum = string.IsNullOrEmpty(txtweight.Text) ? 0 : Convert.ToDouble(txtweight.Text.Trim());
        double weightper = string.IsNullOrEmpty(txtweightper.Text) ? 0 : Convert.ToDouble(txtweightper.Text.Trim());

        double thePer = (weightNum / (75.0 / 100.0));
        txtweightper.Text = thePer.ToString("#,##0.00");
        CalculateTotalWeightSUP();
    }
    
    private void CalculateTotalWeightSUP()
    {
        decimal weightTotal = 0;
        decimal markTotal = 0;
        if (gv_AppraisalFuncSUP.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalFuncSUP.Rows.Count; i++)
            {
                 CheckBox chkIsActive = (CheckBox) gv_AppraisalFuncSUP.Rows[i].FindControl("isActiveCheckBox");
                if (chkIsActive.Checked)
                {
                    TextBox txtWeight = (TextBox) gv_AppraisalFuncSUP.Rows[i].FindControl("txtWeight");

                    TextBox txtMark = (TextBox) gv_AppraisalFuncSUP.Rows[i].FindControl("txtMark");


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

            }

            Label tst = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblTotalWeight");
            tst.Text = weightTotal.ToString();

            Label tst2 = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblTotalMark");
            tst2.Text = markTotal.ToString();
        }
    }

    protected void btnAppraisalFuncSUPSave_OnClick(object sender, EventArgs e)
    {
        if (ValidationFunCSUp() == true)
        {
            List<AppraisalFunctionalAreaBSC> functional = new List<AppraisalFunctionalAreaBSC>();

            for (int i = 0; i < gv_AppraisalFuncSUP.Rows.Count; i++)
            {
                CheckBox isActiveCheckBox = (CheckBox)gv_AppraisalFuncSUP.Rows[i].FindControl("isActiveCheckBox");
                HiddenField hdnDimensionStr = (HiddenField)gv_AppraisalFuncSUP.Rows[i].FindControl("hdnDimensionStr");
                HiddenField hdnObjectiveGoal = (HiddenField)gv_AppraisalFuncSUP.Rows[i].FindControl("hdnObjectiveGoal");
                 
                Label txtKPIMeasure = (Label)gv_AppraisalFuncSUP.Rows[i].FindControl("txtKPIMeasure");
                Label txtInitiatives = (Label)gv_AppraisalFuncSUP.Rows[i].FindControl("txtInitiatives");
                TextBox txtMidStatus = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMidStatus");
                TextBox txtResultYearEnd = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtResultYearEnd");
                TextBox txtResult = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtResult");
                TextBox txtselfMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtselfMark");
                TextBox txtSuperMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMark");
                TextBox txtWeight = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtWeight");
                TextBox txtDeadLine = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtDeadLine");
                if (hdnDimensionStr.Value != "" )
                {
                    AppraisalFunctionalAreaBSC area = new AppraisalFunctionalAreaBSC();
                    try
                    {
                        area.BSCAppraisalSelfFucAreaId = Convert.ToInt32(gv_AppraisalFuncSUP.DataKeys[i][1].ToString());
                        area.Dimension = Convert.ToInt32(gv_AppraisalFuncSUP.DataKeys[i][2].ToString());
                    }
                    catch (Exception)
                    {
                        area.BSCAppraisalSelfFucAreaId = 0;
                        area.Dimension = 0;
                        //throw;
                    }
                    //area.IsActive = true;
                    area.DimensionStr = hdnDimensionStr.Value.Trim().ToString();
                    area.ObjectiveGoal = hdnObjectiveGoal.Value.Trim().ToString();
                    area.KPIMeasure = txtKPIMeasure.Text.Trim().ToString();
                    area.Initiatives = txtInitiatives.Text.Trim().ToString();
                   // area.KpiWeight = Convert.ToDecimal(txtKpiWeight.Text.Trim().ToString());

                    area.SelfMark = string.IsNullOrEmpty(txtselfMark.Text) ? 0 : Convert.ToDecimal(txtselfMark.Text.Trim().ToString());
                    area.Deadline = Convert.ToDateTime(txtDeadLine.Text.Trim().ToString());
                    try
                    {
                        area.ResultYearEnd =  (txtResult.Text.Trim().ToString());
                    }
                    catch (Exception)
                    {
                        area.ResultYearEnd = "";
                        //throw;
                    }
                    area.KpiWeight = Convert.ToDecimal(txtWeight.Text.Trim().ToString());
                    area.SupervisorMark = Convert.ToDecimal(txtSuperMark.Text.Trim().ToString());
                    area.MidYearStatus = txtMidStatus.Text.Trim().ToString();
                    area.IsActive =true; //isActiveCheckBox.Checked;

                    functional.Add(area);
                }

            }


            AppraisalMaster aMaster = new AppraisalMaster();

            aMaster.BSCAppraisalMasterId = Convert.ToInt32(id_mastetID.Value);
            aMaster.EmpInfoId = Convert.ToInt32(id_Empid.Value);
            aMaster.FinancialYearId = Convert.ToInt32(hfFinancialYearId.Value);
            aMaster.AppraisalSelfMasterId = Convert.ToInt32(id_selfID.Value);

                        


            bool result = false;
            if (functional.Count > 0)
            {
                int pk = _appPartA.SaveAppraisalMaster(aMaster, Session["LoginName"].ToString(),"HR");
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
                   "alert('Operation Successful...');",
                   true);
                int empId = int.Parse(Request.QueryString["EmpInfoId"]);
                string finYearDes = (Request.QueryString["FinancialYearDesc"]);
                string M = (Request.QueryString["M"]);
                LoadGridList(empId, finYearDes);
                mpFunctionalSup.Hide();
            }
            else
            {
                AlertMessageBoxShow("Operation Failed");
            }

        }
       

    }

    private bool ValidationFunCSUp()
    {
        bool isVAlid = true;
        if (gv_AppraisalFuncSUP.Rows.Count == 0)
        {
            aShowMessage.ShowMessageBox("Appraisal Functional Part Required ", this);
        }


        for (int i = 0; i < gv_AppraisalFuncSUP.Rows.Count; i++)
        {
            TextBox tbKpi = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtKpi");
            TextBox txtWeight = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtWeight");
            TextBox txtWeightPer = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtWeightPer");
            TextBox txtTarget = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtTarget");
            TextBox txtMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMark");
            TextBox txtDeadLine = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtDeadLine");
            TextBox txtMidStatus = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMidStatus");

            TextBox txtselfMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtselfMark");
            if (string.IsNullOrEmpty(txtWeightPer.Text))
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Expected Number Required ", this);
                txtWeight.Focus();
                break;
            }

            //if (tbKpi.Text == "")
            //{
            //    isVAlid = false;
            //    aShowMessage.ShowMessageBox("Key Performance Indicator Required ", this);
            //    tbKpi.Focus();
            //    break;
            //}

            //if (txtTarget.Text == "")
            //{
            //    isVAlid = false;
            //    aShowMessage.ShowMessageBox(" Target Required ", this);
            //    txtTarget.Focus();

            //    break;
            //}
            //if (txtWeight.Text == "")
            //{
            //    isVAlid = false;
            //    aShowMessage.ShowMessageBox("Weight Required ", this);
            //    txtWeight.Focus();

            //    break;
            //}

            if (txtselfMark.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Self-Mark Required ", this);
                break;
            }
            if (txtDeadLine.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Deadline Required ", this);
                break;
            }

            //if (txtMidStatus.Text == "")
            //{
            //    isVAlid = false;
            //    aShowMessage.ShowMessageBox("Mid Year Status Required ", this);
            //    break;
            //}
        }
        //Label tst = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblTotalWeight");

        //decimal ddddd = 0;
        //try
        //{
        //    ddddd = Convert.ToDecimal(tst.Text);
        //}
        //catch (Exception)
        //{


        //}

        //if (ddddd != 75)
        //{
        //    isVAlid = false;
        //    aShowMessage.ShowMessageBox("Weight (Number) Must be 75 ", this);

        //}



        //Label lblselfMark = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblselfMark");

        //decimal selfMark = 0;
        //try
        //{
        //    selfMark = Convert.ToDecimal(lblselfMark.Text);
        //}
        //catch (Exception)
        //{


        //}

        //if (selfMark >= 76)
        //{
        //    isVAlid = false;
        //    aShowMessage.ShowMessageBox("Total Weight Can Not be Bigger than  75 ", this);

        //}



        //Label lblTotalMark = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblTotalMark");

        //decimal TotalMark = 0;
        //try
        //{
        //    TotalMark = Convert.ToDecimal(lblTotalMark.Text);
        //}
        //catch (Exception)
        //{


        //}

        //if (TotalMark >= 76)
        //{
        //    isVAlid = false;
        //    aShowMessage.ShowMessageBox("Total Mark Can Not be Bigger than  75 ", this);

        //}

        //for (int i = 0; i < gv_AppraisalFuncSUP.Rows.Count; i++)
        //{

        //    TextBox txtMark = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtMark");
        //    TextBox txtResult = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtResult");




        //    if (txtMark.Text == "")
        //    {
        //        isVAlid = false;
        //        aShowMessage.ShowMessageBox("Appraisal Functional Supervisor Score Required ", this);
        //        break;
        //    }

        //    if (txtMark.Text == "")
        //    {
        //        isVAlid = false;
        //        aShowMessage.ShowMessageBox("Appraisal Functional Result End Status	 Required ", this);
        //        break;
        //    }
        //}
        return isVAlid;
    }

    protected void btnMPBSupClose_Click(object sender, EventArgs e)
    {
        MPBSup.Hide();
    }

    protected void SupervisorScore_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        TextBox Weight = (TextBox)gv_AppraisalPartBSUP.Rows[rowID].FindControl("Weight");
        TextBox txtsupWeight = (TextBox)gv_AppraisalPartBSUP.Rows[rowID].FindControl("SupervisorScore");
        decimal wet = 0, supervisor = 0;
        wet = Convert.ToDecimal(Weight.Text);
        supervisor = Convert.ToDecimal(txtsupWeight.Text);

        if (supervisor > wet)
        {
            txtsupWeight.Text = "0";
            AlertMessageBoxShow("Supervisor Mark Cannot Be Greater Then Weight");
        }


        double weightTotal = 0;

        for (int i = 0; i < gv_AppraisalPartBSUP.Rows.Count; i++)
        {
            TextBox txtWeight = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SupervisorScore");

            if (txtWeight.Text == "")
            {

                weightTotal = weightTotal + 0;
            }
            else
            {
                weightTotal = weightTotal + Convert.ToDouble(txtWeight.Text.ToString());
            }


        }
        Label tst = (Label)gv_AppraisalPartBSUP.FooterRow.FindControl("lblTotalMark");
        tst.Text = weightTotal.ToString();
    }

    protected void btnPartBSUPSave_OnClick(object sender, EventArgs e)
    {
        if (ValidationBehaveSupp() == true)
        {
            List<AppraisalBehaveArea> aList = new List<AppraisalBehaveArea>();

            for (int i = 0; i < gv_AppraisalPartBSUP.Rows.Count; i++)
            {
                TextBox txtSkillInfo = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SkillInfo");
                TextBox txtSupportingEmp = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SupportingEmp");
                TextBox txtScore = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("Weight");
                TextBox txtSelfScore = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SelfScore");
                Label SetScore = (Label)gv_AppraisalPartBSUP.Rows[i].FindControl("SetScore");
                TextBox supervisorScore = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("SupervisorScore");
                TextBox txtComments = (TextBox)gv_AppraisalPartBSUP.Rows[i].FindControl("txtComments");

                if (txtSkillInfo.Text.Trim().ToString() != "" && supervisorScore.Text.Trim().ToString() != "")
                {
                    AppraisalBehaveArea area = new AppraisalBehaveArea();
                    area.AppraisalMasterId = Convert.ToInt32(id_mastetID.Value);
                    area.AppraisalSelfMasterId = Convert.ToInt32(id_selfID.Value);
                    area.SkillInfo = txtSkillInfo.Text.Trim().ToString();
                    area.SupportingEmp = txtSupportingEmp.Text.Trim().ToString();
                    area.Score = Convert.ToDecimal(txtScore.Text.ToString());
                    area.SetScore = Convert.ToDecimal(SetScore.Text.ToString());
                    area.SelfScore = Convert.ToDecimal(txtSelfScore.Text.ToString());
                    area.SupervisorScore = Convert.ToDecimal(supervisorScore.Text.ToString());
                    area.Comments = txtComments.Text.Trim().ToString();

                    aList.Add(area);

                }


            }
            if (aList.Count > 0)
            {
                bool result = _appraisalPartBdal.SaveAppraisalPartBHR(aList, Convert.ToInt32(id_selfID.Value));
                if (result == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successful...');",
                   true);
                    int empId = int.Parse(Request.QueryString["EmpInfoId"]);
                    string finYearDes = (Request.QueryString["FinancialYearDesc"]);
                    string M = (Request.QueryString["M"]);
                    LoadGridList(empId, finYearDes);
                    MPBSup.Hide();
                }
                else
                {
                    AlertMessageBoxShow("Operation Failed");
                }
            }
        }
    }

    protected void btnFinalStatusClose_Click(object sender, EventArgs e)
    {
       MPFinalStatus.Hide();
    }


    protected void recommend_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (recommend.SelectedValue == "1")
        {
            Divsteps.Visible = false;
            Div1Other.Visible = false;

        }

        if (recommend.SelectedValue == "2")
        {
            Divsteps.Visible = true;
            Div1Other.Visible = false;

        }

        if (recommend.SelectedValue == "3")
        {
            Divsteps.Visible = false;
            Div1Other.Visible = false;
        }
        if (recommend.SelectedValue == "4")
        {
            Divsteps.Visible = false;
            Div1Other.Visible = false;

        }
        if (recommend.SelectedValue == "5")
        {
            Divsteps.Visible = false;
            Div1Other.Visible = false;

        }
        if (recommend.SelectedValue == "6")
        {
            Div1Other.Visible = true;
            Divsteps.Visible = false;
        }
    }

    protected void btnFinalStatusSave_OnClick(object sender, EventArgs e)
    {
        AppraisalFinalStatus appraisalFinal = new AppraisalFinalStatus();

        string recom = recommend.SelectedValue.ToString();

        if (recom == "1")
        {
            appraisalFinal.GeneralIncrement = true;

            appraisalFinal.SpecialIncrement = false;
            appraisalFinal.IsPromotion = false;
            appraisalFinal.Pip = false;
            appraisalFinal.Other = false;

        }

        if (recom == "2")
        {
            appraisalFinal.SpecialIncrement = true;

            appraisalFinal.GeneralIncrement = false;
            appraisalFinal.IsPromotion = false;
            appraisalFinal.Pip = false;
            appraisalFinal.Other = false;
            try
            {
                appraisalFinal.SpecialStep = Convert.ToInt32(txtStep.Text);
            }
            catch (Exception)
            {
                appraisalFinal.SpecialStep = null;
                //throw;
            }

        }

        if (recom == "3")
        {
            appraisalFinal.IsPromotion = true;
            appraisalFinal.GeneralIncrement = false;
            appraisalFinal.SpecialIncrement = false; 
            appraisalFinal.Pip = false;
            appraisalFinal.Other = false;
        }
        if (recom == "4")
        {
            appraisalFinal.Pip = true;

            appraisalFinal.GeneralIncrement = false;
            appraisalFinal.SpecialIncrement = false;
            appraisalFinal.IsPromotion = false;
            appraisalFinal.Other = false;

        }

        if (recom == "5")
        {
            appraisalFinal.Pip = false;

            appraisalFinal.GeneralIncrement = true;
            appraisalFinal.SpecialIncrement = false;
            appraisalFinal.IsPromotion = true;
            appraisalFinal.Other = false;

        }

        if (recom == "6")
        {
            appraisalFinal.Other = true;
            appraisalFinal.Pip = false;
            appraisalFinal.GeneralIncrement = false;
            appraisalFinal.SpecialIncrement = false;
            appraisalFinal.IsPromotion = false;
            appraisalFinal.Note = txtnote.Text.Trim();

        }
        appraisalFinal.BSCAppraisalMasterId = Convert.ToInt32(id_mastetID.Value);
        appraisalFinal.FinalStatus = lblStatus.Text;
        appraisalFinal.Justification = txtJustification.Text;
        appraisalFinal.TotalScore = Convert.ToDecimal(totalScore.Text);
        appraisalFinal.DocumentLink = hfDocFile.Value.ToString();
        appraisalFinal.FileName = hfDocFileName.Value.ToString();

        bool result = _appraisalPartBdal.SaaveFinalStatus(appraisalFinal);
        if (result == true)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successful...');",
                   true);
            int empId = int.Parse(Request.QueryString["EmpInfoId"]);
            string finYearDes = (Request.QueryString["FinancialYearDesc"]);
            string M = (Request.QueryString["M"]);
            LoadGridList(empId, finYearDes);
            MPFinalStatus.Hide();
        }
        else
        {
            AlertMessageBoxShow("Operation Failed");
        }
    }

    protected void btnTrainingSUPClose_Click(object sender, EventArgs e)
    {
       MPTrainingSUP.Hide();
    }

    protected void btnTrainSaveSUP_OnClick(object sender, EventArgs e)
    {
        List<AppraisalTrainingNeeds> aList = new List<AppraisalTrainingNeeds>();
        for (int i = 0; i < gv_AppraisalTrainingSUP.Rows.Count; i++)
        {
            TextBox txtSkillInfo = (TextBox)gv_AppraisalTrainingSUP.Rows[i].FindControl("TrainingNeeds");
            TextBox txtSupportingEmp = (TextBox)gv_AppraisalTrainingSUP.Rows[i].FindControl("TrainingStart");
            TextBox txtScore = (TextBox)gv_AppraisalTrainingSUP.Rows[i].FindControl("TrainingEnd");
            DropDownList ddlQuater = (DropDownList)gv_AppraisalTrainingSUP.Rows[i].FindControl("QuaterDropDownList1");

            if (txtSkillInfo.Text.Trim().ToString() != "")
            //&& txtSupportingEmp.Text.Trim().ToString() != "" &&
            //txtScore.Text.Trim().ToString() != ""
            {

                AppraisalTrainingNeeds appraisal = new AppraisalTrainingNeeds();
                appraisal.AppraisalMasterId = Convert.ToInt32(id_mastetID.Value);
                appraisal.TrainingNeeds = txtSkillInfo.Text.Trim().ToString();
                appraisal.TrainingStart = Convert.ToDateTime(DateTime.Now);
                appraisal.TrainingEnd = Convert.ToDateTime(DateTime.Now);
                appraisal.Quater = Convert.ToInt32(ddlQuater.SelectedValue);
                aList.Add(appraisal);
            }



        }

        if (aList.Count > 0)
        {
            bool result = _appraisalPartBdal.SaveTrainingNeeds(aList);
            if (result == true)
            {




                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='OKRBSCApppraisalApprove.aspx';",
                    true);



            }
            else
            {
                AlertMessageBoxShow("Operation Failed");
            }
        }
    }

    protected void ViewComments2_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_appraisalMaster");
        HiddenField empID = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_empId");


        string mastrValue = mastrId.Value;
        string emoIdValue = empID.Value;
        if (mastrValue == "0")
        {
            AlertMessageBoxShow("Please Complete all Function");
        }
        else
        {
            mpComm.Show();
            int masterID = int.Parse(mastrId.Value); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
            int empInfoId = int.Parse(empID.Value);
            id_mastetID.Value = masterID.ToString();

            DataTable dt = _appPartA.GetViewComments(masterID);
            if (dt.Rows.Count > 0)
            {
                gv_Versions.DataSource = dt;
                gv_Versions.DataBind();
            }
        }
    }

    protected void txtMidStatus_OnTextChanged(object sender, EventArgs e)
    {
        
    }

    protected void txtResult_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        decimal tResult = 0;
       


        for (int i = 0; i < gv_AppraisalFuncSUP.Rows.Count; i++)
        {
            TextBox txtResult = (TextBox)gv_AppraisalFuncSUP.Rows[i].FindControl("txtResult");

            if (txtResult.Text == "")
            {
                tResult = tResult + 0;
            }
            else
            {
                tResult = tResult + Convert.ToDecimal(txtResult.Text.ToString());
            }


        }

        Label lblresultend = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblresultend");
        lblresultend.Text = tResult.ToString();
    }

    protected void btn_View_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField id_empId = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_empId");
        Label lbl_EmpMasterCode = (Label)gv_AppraisalBoard.Rows[rowID].FindControl("lbl_EmpMasterCode");

        HiddenField mastrId = (HiddenField)gv_AppraisalBoard.Rows[rowID].FindControl("id_appraisalMaster");
        LinkButton PartA = (LinkButton)gv_AppraisalBoard.Rows[rowID].FindControl("PartA");
        LinkButton PartB = (LinkButton)gv_AppraisalBoard.Rows[rowID].FindControl("PartB");
        LinkButton PartC = (LinkButton)gv_AppraisalBoard.Rows[rowID].FindControl("PartC");
        LinkButton Training = (LinkButton)gv_AppraisalBoard.Rows[rowID].FindControl("Training");
        
        bool confirmstatus = false;
        try
        {
            confirmstatus = Convert.ToBoolean(gv_AppraisalBoard.DataKeys[rowID][1].ToString());
        }
        catch (Exception)
        {
            
            
        }

        hfconfirmstatus.Value = confirmstatus.ToString();
        //if (confirmstatus)
        //{




          string mastrValue = mastrId.Value;
          string emoIdValue = id_empId.Value;
        if (mastrValue == "0")
        {
            AlertMessageBoxShow("Please Complete all Function");
        }
        //else if (PartA.Text != "Not Complete" || PartB.Text != "Not Complete" || PartC.Text != "Not Complete" || Training.Text != "Not Complete")
        //{
        //    AlertMessageBoxShow("Please Complete all Function");
        //}
        else
        {

            Session["AppLogId"] = gv_AppraisalBoard.DataKeys[rowID][0].ToString();

            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            Session["AppPage"] = filepath;
            //Session["ForEmpInfoId"] = gv_JdBoard.DataKeys[rowID][2].ToString();
            Session["ForEmpInfoId"] = id_empId.Value;

            var datKey = gv_AppraisalBoard.DataKeys[rowID];
            if (datKey != null)
            {
                id_mastetID.Value = mastrId.Value;
                id_Empid.Value = id_empId.Value;


            }
            mpApprove.Show();
                  TrainingDAL _trainingDal = new TrainingDAL();
            DataTable dtFinYearList = _trainingDal.GetFianncialYearByComIdChkList(Convert.ToInt32(ddlCompany.SelectedValue));
            ddlFiny.DataValueField = "Value";
            ddlFiny.DataTextField = "TextField";
            ddlFiny.DataSource = dtFinYearList;
            ddlFiny.DataBind();
            ddlFiny.Items.Insert(0, new ListItem("Please Select .....", String.Empty));
            ddlFiny.SelectedIndex = 0;

            RadioTextValue();

            hfempCode.Value = lbl_EmpMasterCode.Text;
            lblScore.Text = "";
            DataTable dt = _appPartA.GetViewComments(Convert.ToInt32(id_mastetID.Value));
            if (dt.Rows.Count > 0)
            {
                gv_Versions.DataSource = dt;
                gv_Versions.DataBind();
            }
        }

        //}
        //else
        //{
        //    AlertMessageBoxShow("Please Complete All The Procedure ");
        //}
    }

    protected void btnApproveClose_Click(object sender, EventArgs e)
    {
       mpApprove.Hide();
    }

    protected void vcchomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    EmployeeProfileDAL aEmployeeInfoListReportDAL = new EmployeeProfileDAL();

    protected void ddlFiny_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        string rptTypeIdMul = "";
        DataTable dtref = aEmployeeInfoListReportDAL.GetRefEmpInfoDAL(id_Empid.Value);
        if (dtref.Rows.Count > 0)
        {
            DataTable aDataTable = new DataTable();
            aDataTable.Columns.Add("EmpInfoId");

            DataRow dataRow = null;
            dataRow = aDataTable.NewRow();
            dataRow["EmpInfoId"] = "0";

            aDataTable.Rows.Add(dataRow);
            ReportingEmpData(id_Empid.Value, dtref);
            string myId = "";
            for (int i = 0; i < dtref.Rows.Count; i++)
            {
                myId += dtref.Rows[i]["ReferenceID"].ToString().Trim() + ",";
            }


            myId = myId.Trim().TrimEnd(',');
            rptTypeIdMul = id_Empid.Value + "," + myId.Trim();
        }
        DataTable dt = _appPartA.GetHistoryScore(hfempCode.Value, ddlFiny.SelectedItem.Text);
        DataTable dtNew = new DataTable();
        if (rptTypeIdMul == "")
        {
              dtNew = _appPartA.GetHistoryScoreforNew(id_Empid.Value, ddlFiny.SelectedItem.Text);
            
        }

        else
        {
            dtNew = _appPartA.GetHistoryScoreforNew(rptTypeIdMul, ddlFiny.SelectedItem.Text);
            
        }

            if (dt.Rows.Count > 0)
            {
                lblScore.Text = dt.Rows[0]["appScore"].ToString();
            }
            else if (dtNew.Rows.Count > 0)
            {
                lblScore.Text = dtNew.Rows[0]["appScore"].ToString();
            }
            else
            {
                lblScore.Text = "0";
                
            }

    }
    
    EmployeeProfileDAL ddd = new EmployeeProfileDAL();

    SupervisorMenuAppDAL ddddd = new SupervisorMenuAppDAL();

    public void ReportingEmpData(string empinfoid, DataTable aDataTable)
    {
        DataRow dataRow = null;
        DataTable dtdata1 = ddd.GetRefEmpInfoDAL2(empinfoid);
        DataTable dtdata = ddddd.LoadEmpGenInfoGetRef(" AND E.EmpInfoId='" + dtdata1.Rows[0]["ReferenceID"].ToString() + "' ");

        if (dtdata.Rows.Count > 0)
        {
            dataRow = aDataTable.NewRow();
            dataRow["ReferenceID"] = dtdata.Rows[0]["FromEmpInfoId"].ToString();

            aDataTable.Rows.Add(dataRow);

            ReportingEmpData(dtdata.Rows[0]["FromEmpInfoId"].ToString(), aDataTable);
        }

    }
    protected void SSGradeCheck_OnCheckedChanged(object sender, EventArgs e)
    {

        for (int i = 0; i < cblHeader.Items.Count; i++)
        {
            if (SSGradeCheck.Checked)
            {
                cblHeader.Items[i].Selected = true;
            }
            else
            {
                cblHeader.Items[i].Selected = false
                    ;
            }
        }
    }


    private bool validat()
    {
         
        if (cblHeader.SelectedValue == string.Empty)
        {
            ShowMessageBox("Please Select Name of Information");
            cblHeader.Focus();
            return false;
        }


        return true;
    }
    protected void btnViewrpt_OnClick(object sender, EventArgs e)
    {

        if (validat())
        {
            Session["BI"] = string.Empty;
            Session["BI"] = cblHeader.Items[0].Selected;


            Session["AI"] = string.Empty;
            Session["AI"] = cblHeader.Items[1].Selected;


            Session["TWI"] = string.Empty;
            Session["TWI"] = cblHeader.Items[2].Selected;



            Session["FD"] = string.Empty;
            Session["FD"] = cblHeader.Items[3].Selected;

            Session["Exp"] = string.Empty;
            Session["Exp"] = cblHeader.Items[4].Selected;



            Session["NI"] = string.Empty;
            Session["NI"] = cblHeader.Items[5].Selected;


            Session["PA"] = string.Empty;
            Session["PA"] = cblHeader.Items[6].Selected;



            Session["DI"] = string.Empty;
            Session["DI"] = cblHeader.Items[7].Selected;


            Session["PI"] = string.Empty;
            Session["PI"] = cblHeader.Items[8].Selected;



            Session["threeParam"] = string.Empty;
            Session["threeParam"] = cblHeader.Items[9].Selected;

            Session["TI"] = string.Empty;
            Session["TI"] = cblHeader.Items[10].Selected;


            Session["KPI"] = string.Empty;
            Session["KPI"] = cblHeader.Items[11].Selected;




            Session["INI"] = string.Empty;
            Session["INI"] = false;


           




          


           








            PopUp(Convert.ToInt32(id_Empid.Value.Trim()));
       
        }
            
    }


    private void PopUp(Int32 EmpInfoId)
    {
        string url = "../Report_UI/EmployeeProfileReportViwer.aspx?rptType=" + EmpInfoId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void txtsupselfMark_OnTextChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((TextBox)sender).Parent.Parent)).RowIndex;
        try
        {
            if (Convert.ToDecimal(((TextBox)gv_AppraisalFuncSUP.Rows[rowIndex].Cells[3].FindControl("txtWeight")).Text) >=
           Convert.ToDecimal(((TextBox)gv_AppraisalFuncSUP.Rows[rowIndex].Cells[3].FindControl("txtselfMark")).Text))
            {
              //  CalculateTotalFuncsupp();
            }

            else
            {
                (((TextBox)gv_AppraisalFuncSUP.Rows[rowIndex].Cells[3].FindControl("txtselfMark")).Text) = 0.ToString();
                AlertMessageBoxShow("Self-Mark must be less then or Equal to Weight (Number)");
            }
        }
        catch (Exception)
        {
            ((TextBox)gv_AppraisalFuncSUP.Rows[rowIndex].Cells[3].FindControl("txtWeight")).Text = "0";
            AlertMessageBoxShow("Please Fill Weight (Number)");
            //throw;
        }
        CalculateBFuncSUP();

    }

    protected void txtTarget_OnTextChanged(object sender, EventArgs e)
    {
        CalculateBFuncSUP();
    }
}