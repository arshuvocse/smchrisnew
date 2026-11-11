using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.SuspendAndDiciplinary_Dal;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_KPIInformationDetailsView : System.Web.UI.Page
{

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();
    private KPIMIDAppraisalFunctionalPartDAL _appPartMid = new KPIMIDAppraisalFunctionalPartDAL();
    private AppraisalPartBDAL _appraisalPartBdal = new AppraisalPartBDAL();
    EmployeeSuspendDal aSuspendDal = new EmployeeSuspendDal();
    private JdDAL _jdDal = new JdDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int parsedFinYear;
            if (!string.IsNullOrEmpty(Request.QueryString["EmpInfoId"]) &&
                !int.TryParse(Request.QueryString["financialYearId"], out parsedFinYear))
            {
                Response.Write("<h3 style='text-align:center;margin-top:50px;'>No data found!</h3>");
                Response.End();
                return;
            }
            try
            {
                IniKpiTable();
                IniTable();
                if (Session["EmpInfoId"] != null)
                {
                    GetEmpinfo(Session["EmpInfoId"].ToString());
                }
                if (!string.IsNullOrEmpty(Request.QueryString["EmpInfoId"]))
                {
                    int empId = int.Parse(Request.QueryString["EmpInfoId"]);
                    int finYear = int.Parse(Request.QueryString["financialYearId"]);
                    //  ddlFinancialYear.SelectedIndex = 1;

                    DataTable dtfin = _appPartA.GetFinYear(Convert.ToInt32(finYear));
                    string finYeartxt = "";
                    if (dtfin.Rows.Count > 0)
                    {
                        finYeartxt = dtfin.Rows[0]["FinancialYearDesc"].ToString();
                    }


                    DataTable dtaa = _appPartA.GetApprsaisalSelfByEmpFinYear(empId, finYeartxt);
                    if (dtaa.Rows.Count > 0)
                    {

                        int mid = Convert.ToInt32(dtaa.Rows[0][0]);
                        id_mastetID.Value = mid.ToString();
                        DataTable dt = _appPartA.GetAppraisalSelf(mid);



                        DataTable dtversions = _appPartA.GetApproveLogBySelfMaster(mid);

                        if (dtversions.Rows.Count > 0)
                        {
                            gv_KPI_App.DataSource = dtversions;
                            gv_KPI_App.DataBind();
                        }

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

                        try
                        {
                            DataTable dtMidFun = _appPartMid.GetAppraisalfDetailsFromSup(Convert.ToInt32(mid));
                            gv_MidYearAppraisalFunc.DataSource = dtMidFun;
                            gv_MidYearAppraisalFunc.DataBind();

                            CalculateBFuncSUPMid();
                        }
                        catch
                        {

                        }

                        try
                        {
                            DataTable dtMidBehave = _appPartMid.GetAppraisalPartBAppraisalSelfMasterId(Convert.ToInt32(mid));



                            gv_MidYearAppraisalBehave.DataSource = dtMidBehave;
                            gv_MidYearAppraisalBehave.DataBind();

                            CalculateBPartBSUPMid();
                        }
                        catch
                        {

                        }
                        DataTable dtw2 = _appPartA.GetAppraisalfDetailsFromSup(Convert.ToInt32(mid));
                        GridView1.DataSource = dtw2;
                        GridView1.DataBind();
                        DataTable gtGetApprisalMasterId = _appPartA.GetAppraisalMasterIdFromAppraisalSelfMasterId(Convert.ToInt32(mid));

                        int AppraisalMasterId = 0;
                        if (gtGetApprisalMasterId.Rows.Count > 0)
                        {
                            AppraisalMasterId = Convert.ToInt32(gtGetApprisalMasterId.Rows[0]["AppraisalMasterId"].ToString());
                        }

                        DataTable dta = _appPartA.GetAppraisalTraining(AppraisalMasterId);
                        if (dta.Rows.Count > 0)
                        {
                            ViewState["TrainingPart"] = dta;
                            gv_AppraisalTraining.DataSource = dta;
                            gv_AppraisalTraining.DataBind();
                            for (int i = 0; i < gv_AppraisalTraining.Rows.Count; i++)
                            {
                                DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");
                                txt_BranchCountry.SelectedValue = dta.Rows[i]["Quater"].ToString();
                            }
                        }


                        DataTable dt3a = _appPartA.GetAppraisalPartB(Convert.ToInt32(AppraisalMasterId));

                        ViewState["PARTB"] = dt3a;
                        GridView2.DataSource = dt3a;
                        GridView2.DataBind();
                        CalculateBPartBSUP();


                        DataTable dtfinal = _appPartA.GetFinalMark(AppraisalMasterId);

                        decimal partA = 0;
                        decimal partB = 0;
                        try
                        {
                            partA = Convert.ToDecimal(dtfinal.Rows[0]["SupScoreFunc"].ToString());
                            partB = Convert.ToDecimal(dtfinal.Rows[0]["SupScoreBehave"].ToString());
                        }
                        catch (Exception)
                        {

                            //AlertMessageBoxShow("Please Complete Functional & Behavioural Part");
                            //Response.Redirect("AppraisalDashboard.aspx");

                        }



                        partAScore.Text = partA.ToString("F");
                        partBScore.Text = partB.ToString("F");
                        totalScore.Text = (partA + partB).ToString("F");

                        decimal total = partA + partB;

                        if (total == 0)
                        {
                            lblStatus.Text = "";
                        }
                        else if (total <= 60)
                        {
                            lblStatus.Text = "Poor";
                        }
                        else if (total > 60 && total <= 75)
                        {
                            lblStatus.Text = "Average";
                        }
                        else if (total > 75 && total <= 80)
                        {
                            lblStatus.Text = "Good";
                        }
                        else if (total > 80 && total <= 90)
                        {
                            lblStatus.Text = "Excellent";
                        }
                        else // total > 90
                        {
                            lblStatus.Text = "Outstanding";
                        }






                        if (mid > 0)
                        {
                            DataTable dt45 = _appraisalPartBdal.GetAppraiSalFinalStatus(AppraisalMasterId);
                            if (dt45.Rows.Count > 0)
                            {
                                bool gen = Convert.ToBoolean(dt45.Rows[0]["GeneralIncrement"].ToString());
                                bool SpecialIncrement = Convert.ToBoolean(dt45.Rows[0]["SpecialIncrement"].ToString());
                                bool IsPromotion = Convert.ToBoolean(dt45.Rows[0]["IsPromotion"].ToString());
                                bool Pip = !string.IsNullOrEmpty(dt45.Rows[0]["Pip"].ToString()) && Convert.ToBoolean(dt45.Rows[0]["Pip"].ToString());
                                bool Other = !string.IsNullOrEmpty(dt45.Rows[0]["Other"].ToString()) && Convert.ToBoolean(dt45.Rows[0]["Other"].ToString());

                                int step = string.IsNullOrEmpty(dt45.Rows[0]["SpecialStep"].ToString())
                                    ? 0
                                    : Convert.ToInt32(dt45.Rows[0]["SpecialStep"].ToString());
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
                                    txtnote.Text = dt45.Rows[0]["Note"].ToString();


                                }
                                txtJustification.Text = dt45.Rows[0]["Justification"].ToString();

                                if (dt45.Rows[0]["DocumentLink"].ToString() != "")
                                {
                                    HLDocumentLink.Text = "Preview";
                                    lbFileName.Text = "File Name: " + dt45.Rows[0]["FileName"].ToString();
                                    HLDocumentLink.NavigateUrl = "../UploadMeetingDocument/" + dt45.Rows[0]["DocumentLink"].ToString();

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






                        //Get Versions
                        if (mid > 0)
                        {
                            //DataTable versions = _appPartA.GetApproveLogBySelfMaster(mid);

                            //if (versions.Rows.Count > 0)
                            //{
                            //    gv_Versions.DataSource = versions;
                            //    gv_Versions.DataBind();
                            //}
                        }


                    }

                    else
                    {
                        GetEmpinfo(empId.ToString());
                        // ddlFinancialYear.SelectedValue = int.Parse(Request.QueryString["financialYearId"]);
                    }
                }
                if (!string.IsNullOrEmpty(Request.QueryString["EmpInfoId"]))
                {
                    int empId = int.Parse(Request.QueryString["EmpInfoId"]);
                    int finYear = int.Parse(Request.QueryString["financialYearId"]);
                    ddlFinancialYear.SelectedIndex = 1;

                    DataTable dtfin = _appPartA.GetFinYear(Convert.ToInt32(finYear));
                    string finYeartxt = "";
                    if (dtfin.Rows.Count > 0)
                    {
                        finYeartxt = dtfin.Rows[0]["FinancialYearDesc"].ToString();
                    }


                    DataTable dtaa = _appPartA.GetApprsaisalSelfByEmpFinYear(empId, finYeartxt);
                    if (dtaa.Rows.Count > 0)
                    {

                        int mid = Convert.ToInt32(dtaa.Rows[0][0]);
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

                    else
                    {
                        GetEmpinfo(empId.ToString());
                        // ddlFinancialYear.SelectedValue = int.Parse(Request.QueryString["financialYearId"]);
                    }

                    submitButton.Visible = false;
                    editButton.Visible = false;
                    btn_Save.Visible = false;
                }
            }
            catch
            {

            }
        }

        if (!string.IsNullOrEmpty(id_mastetID.Value))
        {
            BindAppraisalApprovalList(Convert.ToInt32(id_mastetID.Value));
            BindMidYearApprovalList(Convert.ToInt32(id_mastetID.Value));
        }
    }

    private void CalculateBFuncSUPMid()
    {
        decimal MarkTotal = 0;
        decimal WeightTotal = 0;
        decimal TargetTotal = 0;
        decimal ResultTotal = 0;
        decimal tselfMark = 0;


        if (gv_MidYearAppraisalFunc.Rows.Count > 0)
        {
            for (int i = 0; i < gv_MidYearAppraisalFunc.Rows.Count; i++)
            {

                CheckBox chkIsActive = (CheckBox)gv_MidYearAppraisalFunc.Rows[i].FindControl("isActiveCheckBox");
                //if (chkIsActive.Checked)
                {
                    TextBox txtMark = (TextBox)gv_MidYearAppraisalFunc.Rows[i].FindControl("txtMark");
                    TextBox txtWeight = (TextBox)gv_MidYearAppraisalFunc.Rows[i].FindControl("txtWeight");
                    TextBox txtTarget = (TextBox)gv_MidYearAppraisalFunc.Rows[i].FindControl("txtTarget");
                    TextBox txtResult = (TextBox)gv_MidYearAppraisalFunc.Rows[i].FindControl("txtResult");
                    TextBox txtselfMark = (TextBox)gv_MidYearAppraisalFunc.Rows[i].FindControl("txtselfMark");





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
                }
            }



            Label tst2 = (Label)gv_MidYearAppraisalFunc.FooterRow.FindControl("lblTotalMark");
            tst2.Text = MarkTotal.ToString();


            Label lblTotalWeight = (Label)gv_MidYearAppraisalFunc.FooterRow.FindControl("lblTotalWeight");
            lblTotalWeight.Text = WeightTotal.ToString();




            Label lbltarget = (Label)gv_MidYearAppraisalFunc.FooterRow.FindControl("lbltarget");
            lbltarget.Text = TargetTotal.ToString();


            //Label lblresultend = (Label)gv_AppraisalFuncSUP.FooterRow.FindControl("lblresultend");
            //lblresultend.Text = ResultTotal.ToString();


            Label lblselfMark = (Label)gv_MidYearAppraisalFunc.FooterRow.FindControl("lblselfMark");
            lblselfMark.Text = tselfMark.ToString();

        }
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
    private void CalculateBPartBSUP()
    {
        decimal TWeight = 0;
        decimal TSetScore = 0;
        decimal TSelfScore = 0;
        decimal TSupervisorScore = 0;

        if (GridView2.Rows.Count > 0)
        {
            for (int i = 0; i < GridView2.Rows.Count; i++)
            {
                TextBox Weight = (TextBox)GridView2.Rows[i].FindControl("Weight");
                TextBox SetScore = (TextBox)GridView2.Rows[i].FindControl("SetScore");
                TextBox SelfScore = (TextBox)GridView2.Rows[i].FindControl("SelfScore");
                TextBox SupervisorScore = (TextBox)GridView2.Rows[i].FindControl("SupervisorScore");




                if (Weight.Text == "")
                {
                    TWeight = TWeight + 0;
                }
                else
                {
                    TWeight = TWeight + Convert.ToDecimal(Weight.Text.ToString());
                }

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



            Label lblToWeight = (Label)GridView2.FooterRow.FindControl("lblToWeight");
            lblToWeight.Text = TWeight.ToString();



            Label ddllblTotalWeight = (Label)GridView2.FooterRow.FindControl("ddllblTotalWeight");
            ddllblTotalWeight.Text = TSetScore.ToString();


            Label lblselfscrore = (Label)GridView2.FooterRow.FindControl("lblselfscrore");
            lblselfscrore.Text = TSelfScore.ToString();



            Label lblTotalMark = (Label)GridView2.FooterRow.FindControl("lblTotalMark");
            lblTotalMark.Text = TSupervisorScore.ToString();
        }
    }
     private void CalculateBPartBSUPMid()
    {
        decimal TWeight = 0;
        decimal TSetScore = 0;
        decimal TSelfScore = 0;
        decimal TSupervisorScore = 0;

        if (gv_MidYearAppraisalBehave.Rows.Count > 0)
        {
            for (int i = 0; i < gv_MidYearAppraisalBehave.Rows.Count; i++)
            {
                TextBox Weight = (TextBox)gv_MidYearAppraisalBehave.Rows[i].FindControl("Weight");
                TextBox SetScore = (TextBox)gv_MidYearAppraisalBehave.Rows[i].FindControl("SetScore");
                TextBox SelfScore = (TextBox)gv_MidYearAppraisalBehave.Rows[i].FindControl("SelfScore");
                TextBox SupervisorScore = (TextBox)gv_MidYearAppraisalBehave.Rows[i].FindControl("SupervisorScore");




                if (Weight.Text == "")
                {
                    TWeight = TWeight + 0;
                }
                else
                {
                    TWeight = TWeight + Convert.ToDecimal(Weight.Text.ToString());
                }

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



            Label lblToWeight = (Label)gv_MidYearAppraisalBehave.FooterRow.FindControl("lblToWeight");
            lblToWeight.Text = TWeight.ToString();



            Label ddllblTotalWeight = (Label)gv_MidYearAppraisalBehave.FooterRow.FindControl("ddllblTotalWeight");
            ddllblTotalWeight.Text = TSetScore.ToString();


            Label lblselfscrore = (Label)gv_MidYearAppraisalBehave.FooterRow.FindControl("lblselfscrore");
            lblselfscrore.Text = TSelfScore.ToString();



            Label lblTotalMark = (Label)gv_MidYearAppraisalBehave.FooterRow.FindControl("lblTotalMark");
            lblTotalMark.Text = TSupervisorScore.ToString();
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
            //else if (Session["Status"].ToString() == "Delete")
            //{
            //    delButton.Visible = true;
            //}
            Session["Status"] = null;
        }

    }

    protected void btn_Add_OnClick(object sender, EventArgs e)
    {
        if (ViewState["KPIFUNC"] == null)
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

            dt.Columns.Add(new DataColumn("SelfMark", typeof(string)));
            dr = dt.NewRow();

            dr["KpiInfo"] = "";
            dr["KpiWeight"] = "";
            dr["KpiWeightPer"] = "";
            dr["Target"] = "";
            dr["TargetPer"] = "";
            dr["Deadline"] = "";
            dr["MidYearStatus"] = "";

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

            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
                TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
                TextBox txtWeightPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
                TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
                TextBox txtTargetPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTargetPer");
                TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
                TextBox txtMidStatus = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");

                TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                dtCurrentTable.Rows[i]["KpiInfo"] = tbKpi.Text.Trim().ToString() == ""
                    ? ""
                    : tbKpi.Text.Trim().ToString();
                dtCurrentTable.Rows[i]["KpiWeight"] = txtWeight.Text.Trim().ToString() == ""
                    ? ""
                    : txtWeight.Text.Trim().ToString();

                dtCurrentTable.Rows[i]["KpiWeightPer"] = txtWeightPer.Text.Trim().ToString() == ""
                   ? ""
                   : txtWeightPer.Text.Trim().ToString();

                dtCurrentTable.Rows[i]["Target"] = txtTarget.Text.Trim().ToString() == ""
                    ? ""
                    : txtTarget.Text.Trim().ToString();

                dtCurrentTable.Rows[i]["TargetPer"] = txtTargetPer.Text.Trim().ToString() == ""
                    ? ""
                    : txtTargetPer.Text.Trim().ToString();

                dtCurrentTable.Rows[i]["Deadline"] = txtDeadLine.Text.Trim().ToString() == ""
                    ? ""
                    : txtDeadLine.Text.Trim().ToString();

                dtCurrentTable.Rows[i]["SelfMark"] = txtMark.Text.Trim().ToString() == ""
                    ? 0
                    : Convert.ToDecimal(txtMark.Text.Trim().ToString());
                //dtCurrentTable.Rows[i]["MidYearStatus"] = txtMidStatus.Text.Trim().ToString();

            }

            gv_AppraisalFunc.DataSource = dtCurrentTable;
            gv_AppraisalFunc.DataBind();

            CalculateTotal();
        }
    }

    protected void lb_Remove_OnClick(object sender, EventArgs e)
    {
        if (ViewState["KPIFUNC"] != null && gv_AppraisalFunc.Rows.Count > 1)
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
            CalculateTotal();
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
                TextBox txtTargetPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTargetPer");
                TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
                TextBox txtMidStatus = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");

                TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                if (tbKpi.Text != "" && txtTarget.Text != "" && txtWeight.Text != "")
                {
                    AppraisalFunctionalArea area = new AppraisalFunctionalArea();

                    area.KpiInfo = tbKpi.Text.Trim().ToString();
                    area.KpiWeight = Convert.ToDecimal(txtWeight.Text.Trim().ToString());
                    area.KpiWeightPer = Convert.ToDecimal(txtWeightPer.Text.Trim().ToString());
                    area.Target = Convert.ToDecimal(txtTarget.Text.Trim().ToString());
                    area.TargetPer = Convert.ToDecimal(txtTargetPer.Text.Trim().ToString());
                    area.Deadline = Convert.ToDateTime(txtDeadLine.Text.Trim().ToString());

                    area.SupervisorMark = 0;
                    area.MidYearStatus = " ";

                    functional.Add(area);
                }

            }


            AppraisalMaster aMaster = new AppraisalMaster();

            aMaster.AppraisalMasterId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);
            aMaster.EmpInfoId = Convert.ToInt32(id_Empid.Value);
            aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);


            bool result = false;
            if (functional.Count > 0)
            {
                int pk = _appPartA.SaveAppraisalSelfMaster(aMaster, Session["LoginName"].ToString());
                if (pk > 0)
                {
                    result = _appPartA.SaveAppraialSelfFunctionalDetails(functional, pk);
                    result = SaveAppraisalSelfB(pk);
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
                   "alert('Operation Successful...');window.location ='AppraisalSelfList.aspx';",
                   true);
            }
            else
            {
                AlertMessageBoxShow("Operation Failed");
            }
        }
    }


    private bool SaveAppraisalSelfB(int pk)
    {

        bool result = false;
        List<AppraisalBehaveArea> aList = new List<AppraisalBehaveArea>();

        for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
        {
            TextBox txtSkillInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");
            TextBox txtSupportingEmp = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupportingEmp");
            TextBox txtScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("Score");

            if (txtSkillInfo.Text.Trim().ToString() != "" && txtSupportingEmp.Text.Trim().ToString() != "")
            {
                AppraisalBehaveArea area = new AppraisalBehaveArea();
                area.AppraisalMasterId = pk;
                area.SkillInfo = txtSkillInfo.Text.Trim().ToString();
                area.SupportingEmp = txtSupportingEmp.Text.Trim().ToString();
                area.Score = Convert.ToDecimal(txtScore.Text.Trim());
                aList.Add(area);

            }


        }
        if (aList.Count > 0)
        {
            result = _appraisalPartBdal.SaveAppraisalSelfPartB(aList);
        }
        return result;
    }
    private void IniTable()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("SkillInfo", typeof(string)));
        dt.Columns.Add(new DataColumn("SupportingEmp", typeof(string)));
        dt.Columns.Add(new DataColumn("Score", typeof(string)));
        dt.Columns.Add(new DataColumn("SetScore", typeof(string)));


        for (int i = 0; i < 5; i++)
        {
            dr = dt.NewRow();

            dr["SkillInfo"] = "";
            dr["SupportingEmp"] = "";
            dr["Score"] = "5";
            dr["SetScore"] = "";

            dt.Rows.Add(dr);
        }

        ViewState["PARTB"] = dt;

        gv_AppraisalPartB.DataSource = dt;
        gv_AppraisalPartB.DataBind();

        CalculateB();
    }
 
 
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    public void GetEmpinfo(string id)
    {
        //string empid = txt_employee.Text.Trim();
        //if (empid.Contains(":"))
        {
            //string[] strsp = empid.Split(':');
            //int empId = _trainingDal.GetEmployeeIdByCode(strsp[0]);

            DataTable dtEmp = _appPartA.GetEmployeeDetails(Convert.ToInt32(Request.QueryString["EmpInfoId"]));
            if (dtEmp.Rows.Count > 0)
            {
                LoadFinancialYear(Convert.ToInt32(dtEmp.Rows[0]["CompanyId"]));
                txt_employee.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();
                lblEmployeeName.Text = txt_employee.Text;
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
                lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString();

                ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();


                id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();

            }
        }
        //else
        //{
        //    txt_employee.Text = "";

        //    id_Empid.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}
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
        try
        {
            DataTable dt = _trainingDal.GetFianncialYearByComIdDDl(comp);
            ddlFinancialYear.DataSource = dt;
            ddlFinancialYear.DataValueField = "Value";
            ddlFinancialYear.DataTextField = "TextField";
            ddlFinancialYear.DataBind();
        }
        catch (Exception)
        {
            
            //throw;
        }
       
    }

    private void BindMidYearApprovalList(int selfMasterId)
    {
        DataTable dtMidMaster = _appPartMid.GetMidYearMasterBySelfMasterId(selfMasterId);
        if (dtMidMaster.Rows.Count > 0)
        {
            int midYearMasterId = Convert.ToInt32(dtMidMaster.Rows[0]["AppraisalMasterId"].ToString());
            DataTable dtMidApprove = _appPartMid.GetApproveLogByMidYearMasterId(midYearMasterId);
            if (dtMidApprove.Rows.Count > 0)
            {
                gv_KPI_Mid_App.DataSource = dtMidApprove;
                gv_KPI_Mid_App.DataBind();
            }
        }
    }

    private void BindAppraisalApprovalList(int selfMasterId)
    {
        DataTable dtApproval = _appPartA.GetApproveLogBySelfMaster(selfMasterId);
        if (dtApproval.Rows.Count > 0)
        {
            gv_Appraisal_App.DataSource = dtApproval;
            gv_Appraisal_App.DataBind();
        }
    }


    private void IniKpiTable()
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
        dt.Columns.Add(new DataColumn("IsActive", typeof(bool)));
        dt.Columns.Add(new DataColumn("SelfMark", typeof(string)));
        dr = dt.NewRow();

        dr["KpiInfo"] = "";
        dr["KpiWeight"] = "";
        dr["Target"] = "";
        dr["Deadline"] = "";
        dr["MidYearStatus"] = "";
        dr["IsActive"] = "False";

        dr["SelfMark"] = "";
        dt.Rows.Add(dr);
        ViewState["KPIFUNC"] = dt;

        gv_AppraisalFunc.DataSource = dt;
        gv_AppraisalFunc.DataBind();
    }


    protected void btn_Add_B_OnClick(object sender, EventArgs e)
    {
        if (ViewState["PARTB"] == null)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("SkillInfo", typeof(string)));
            dt.Columns.Add(new DataColumn("SupportingEmp", typeof(string)));
            dt.Columns.Add(new DataColumn("Score", typeof(string)));

            dr = dt.NewRow();

            dr["SkillInfo"] = "";
            dr["SupportingEmp"] = "";
            dr["Score"] = "";
            ViewState["PARTB"] = dt;

            gv_AppraisalPartB.DataSource = dt;
            gv_AppraisalPartB.DataBind();
        }
        else
        {
            DataTable dtCurrentTable = (DataTable)ViewState["PARTB"];

            DataRow drCurrentRow = null;

            drCurrentRow = dtCurrentTable.NewRow();



            dtCurrentTable.Rows.Add(drCurrentRow);


            ViewState["PARTB"] = dtCurrentTable;

            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {
                TextBox txtSkillInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");
                TextBox txtSupportingEmp = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupportingEmp");
                TextBox txtScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("Score");



                dtCurrentTable.Rows[i]["SkillInfo"] = txtSkillInfo.Text.Trim().ToString() == "" ? "" : txtSkillInfo.Text.Trim().ToString();
                dtCurrentTable.Rows[i]["SupportingEmp"] = txtSupportingEmp.Text.Trim().ToString() == "" ? "" : txtSupportingEmp.Text.Trim().ToString();
                dtCurrentTable.Rows[i]["Score"] = txtScore.Text.Trim().ToString() == "" ? "" : txtScore.Text.Trim().ToString();


            }

            gv_AppraisalPartB.DataSource = dtCurrentTable;
            gv_AppraisalPartB.DataBind();
            CalculateB();

        }
    }

    private void CalculateB()
    {
        decimal weightTotal = 0;
        decimal SetScore = 0;

        if (gv_AppraisalPartB.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {
                TextBox txtWeight = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("Score");




                if (txtWeight.Text == "")
                {
                    weightTotal = weightTotal + 0;
                }
                else
                {
                    weightTotal = weightTotal + Convert.ToDecimal(txtWeight.Text.ToString());
                }



                TextBox txtSetScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SetScore");




                if (txtSetScore.Text == "")
                {
                    SetScore = SetScore + 0;
                }
                else
                {
                    SetScore = SetScore + Convert.ToDecimal(txtSetScore.Text.ToString());
                }
            }



            Label tst2 = (Label)gv_AppraisalPartB.FooterRow.FindControl("lblTotalScore");
            tst2.Text = weightTotal.ToString();
            Label tst = (Label)gv_AppraisalPartB.FooterRow.FindControl("ddllblTotalWeight");
            tst.Text = SetScore.ToString();

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
    protected void lb_Remove_b_OnClick(object sender, EventArgs e)
    {
        if (ViewState["PARTB"] != null && gv_AppraisalPartB.Rows.Count > 1)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["PARTB"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count == 0)
            {
                ViewState["PARTB"] = null;
            }
            else
            {
                ViewState["PARTB"] = dt;
            }


            gv_AppraisalPartB.DataSource = dt;
            gv_AppraisalPartB.DataBind();
            CalculateB();

        }
    }



    public bool Validation()
    {
        bool isVAlid = true;
        if (gv_AppraisalFunc.Rows.Count <= 0)
        {
            isVAlid = false;
            aShowMessage.ShowMessageBox("Kpi Info Required ", this);

        }
        if (gv_AppraisalPartB.Rows.Count <= 0)
        {
            isVAlid = false;
            aShowMessage.ShowMessageBox("Behaviral Info Required ", this);

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
        for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
        {
            TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
            TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
            TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
            TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");
            TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
            TextBox txtMidStatus = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
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

            //if (txtMark.Text == "")
            //{
            //    isVAlid = false;
            //    aShowMessage.ShowMessageBox("Kpi Mark Required ", this);
            //    break;
            //}
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
        for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
        {
            TextBox txtSkillInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");
            if (txtSkillInfo.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Behaviral Info Required ", this);
                break;
            }
        }

        Label tst = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalWeight");

        decimal weightTotal = tst.Text == "" ? 0 : Convert.ToDecimal(tst.Text.ToString());
        if (weightTotal > 75)
        {
            aShowMessage.ShowMessageBox("Total Weight Can Not be Bigger than 75 In Part A ", this);
            isVAlid = false;
        }


        Label tst2 = (Label)gv_AppraisalPartB.FooterRow.FindControl("lblTotalScore");

        decimal weightTotal2 = tst.Text == "" ? 0 : Convert.ToDecimal(tst2.Text.ToString());
        if (weightTotal2 > 25)
        {
            aShowMessage.ShowMessageBox("Total Score Can Not be Bigger than 25 In Part B ", this);
            isVAlid = false;
        }
        return isVAlid;
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("KPIInformationView.aspx");
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

    protected void txtMark_OnTextChanged(object sender, EventArgs e)
    {
        //double total = 0;

        //for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
        //{
        //    TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");
        //    TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
        //    double a = txtWeight.Text == "" ? 0 : Convert.ToDouble(txtWeight.Text.Trim());
        //    double b = txtMark.Text == "" ? 0 : Convert.ToDouble(txtMark.Text.Trim());
        //    if (b>a)
        //    {
        //        txtMark.Text = a.ToString();
        //        total += a;
        //    }
        //    else
        //    {
        //        total += b;
        //    }


        //}
        //Label tst = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalMark");
        //tst.Text = total.ToString();




    }

    protected void Score_OnTextChanged(object sender, EventArgs e)
    {
        double total = 0;

        for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
        {
            TextBox txtMark = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("Score");



            total += txtMark.Text == "" ? 0 : Convert.ToDouble(txtMark.Text);



        }

        Label tst = (Label)gv_AppraisalPartB.FooterRow.FindControl("lblTotalScore");
        tst.Text = total.ToString();
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            List<AppraisalFunctionalArea> functional = new List<AppraisalFunctionalArea>();

            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
                TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
                TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
                TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
                TextBox txtMidStatus = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");

                TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                if (tbKpi.Text != "" && txtTarget.Text != "" && txtWeight.Text != "")
                {
                    AppraisalFunctionalArea area = new AppraisalFunctionalArea();

                    area.KpiInfo = tbKpi.Text.Trim().ToString();
                    area.KpiWeight = Convert.ToDecimal(txtWeight.Text.Trim().ToString());
                    area.Target = Convert.ToDecimal(txtTarget.Text.Trim().ToString());
                    area.Deadline = Convert.ToDateTime(txtDeadLine.Text.Trim().ToString());

                    area.SupervisorMark = Convert.ToDecimal(txtMark.Text.Trim().ToString());
                    area.MidYearStatus = txtMidStatus.Text.Trim().ToString();

                    functional.Add(area);
                }

            }


            AppraisalMaster aMaster = new AppraisalMaster();

            aMaster.AppraisalMasterId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);
            aMaster.EmpInfoId = Convert.ToInt32(id_Empid.Value);
            aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);


            bool result = false;
            if (functional.Count > 0)
            {
                int pk = _appPartA.SaveAppraisalSelfMaster(aMaster, Session["LoginName"].ToString());
                if (pk > 0)
                {
                    result = _appPartA.SaveAppraialSelfFunctionalDetails(functional, pk);
                    result = SaveAppraisalSelfB(pk);
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
               "alert('Operation Successful...');window.location ='AppraisalSelfList.aspx';",
               true);
            }
            else
            {
                AlertMessageBoxShow("Operation Failed");
            }
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

    private decimal TotalWeight()
    {
        decimal totalWeight = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            TextBox txtWeight = (TextBox)GridView1.Rows[i].FindControl("txtWeight");


            totalWeight += txtWeight.Text.Trim().ToString() == "" ? 0 : Convert.ToDecimal(txtWeight.Text.Trim());

        }
        return totalWeight;
    }
    private decimal TotalResultEnd()
    {
        decimal result = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            TextBox txtResult = (TextBox)GridView1.Rows[i].FindControl("txtResult");


            result += txtResult.Text.Trim().ToString() == "" ? 0 : Convert.ToDecimal(txtResult.Text.Trim());

        }
        return result;
    }
    private decimal TotalSelfMark()
    {
        decimal result = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            TextBox txtselfMark = (TextBox)GridView1.Rows[i].FindControl("txtselfMark");


            result += txtselfMark.Text.Trim().ToString() == "" ? 0 : Convert.ToDecimal(txtselfMark.Text.Trim());

        }
        return result;
    }
    private decimal TotalsupppMark()
    {
        decimal result = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            TextBox txtMark = (TextBox)GridView1.Rows[i].FindControl("txtMark");


            result += txtMark.Text.Trim().ToString() == "" ? 0 : Convert.ToDecimal(txtMark.Text.Trim());

        }
        return result;
    }
    private decimal TotalTarget()
    {
        decimal result = 0;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            TextBox txtTarget = (TextBox)GridView1.Rows[i].FindControl("txtTarget");


            result += txtTarget.Text.Trim().ToString() == "" ? 0 : Convert.ToDecimal(txtTarget.Text.Trim());

        }
        return result;
    }
    protected void gv_AppraisalFunc_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl = (Label)e.Row.FindControl("lblTotalWeight");
            //Label lblresult = (Label)e.Row.FindControl("lblresultend");
            Label lblselfMark = (Label)e.Row.FindControl("lblselfMark");
            Label lbltarget = (Label)e.Row.FindControl("lbltarget");
            Label lblTotalMark = (Label)e.Row.FindControl("lblTotalMark");
            lbl.Text = TotalWeight().ToString();
            //lblresult.Text = TotalResultEnd().ToString();
            lblselfMark.Text = TotalSelfMark().ToString();
            lbltarget.Text = TotalTarget().ToString();


            lblTotalMark.Text = TotalsupppMark().ToString();

        }
    }

    private decimal TotalWeight1()
    {
        decimal result = 0;
        for (int i = 0; i < GridView2.Rows.Count; i++)
        {
            TextBox SelfScore = (TextBox)GridView2.Rows[i].FindControl("SelfScore");


            result += SelfScore.Text.Trim().ToString() == "" ? 0 : Convert.ToDecimal(SelfScore.Text.Trim());

        }
        return result;
    }
    protected void gv_AppraisalFunc1_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblselfscrore = (Label)e.Row.FindControl("lblselfscrore");

            lblselfscrore.Text = TotalWeight1().ToString();

        }
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

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
}
