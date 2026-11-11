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

public partial class Appraisal_AppraisalPartB : System.Web.UI.Page
{
    private TrainingDAL _trainingDal = new TrainingDAL();
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();
    private  AppraisalPartBDAL _appraisalPartBdal = new AppraisalPartBDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int masterID = int.Parse(Request.QueryString["masterId"]); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
            int empInfoId = int.Parse(Request.QueryString["empInfoId"]);

            int selfMaster = int.Parse(Request.QueryString["selfMaster"]);
            id_selfID.Value = selfMaster.ToString();
            DataTable dt = _appPartA.GetAppraisalSelf(Convert.ToInt32(selfMaster));
            id_mastetID.Value = masterID.ToString();
            //DataTable dt3 = _appPartA.GetAppraisalSelfB(Convert.ToInt32(selfMaster));
            DataTable dt3 = _appPartA.GetAppraisalPartB(Convert.ToInt32(masterID));

            ViewState["PARTB"] = dt3;
            gv_AppraisalPartB.DataSource = dt3;
            gv_AppraisalPartB.DataBind();
            CalculateB();
            totalSelf();
            if (empInfoId > 0)
            {
                DataTable dtEmp = _appPartA.GetEmployeeDetails(empInfoId);
                if (dtEmp.Rows.Count > 0)
                {
                    empName.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();
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

                   // id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();
                }
            }
            CheckImmmiedietSuperVisor();
        }
    }


    private void CalculateB()
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
    public void CheckImmmiedietSuperVisor()
    {
        AppraislDashboardDAL appraisl = new AppraislDashboardDAL();
        DataTable dtempdata = appraisl.GetEmpInfo(" WHERE EmpInfoId='" + Request.QueryString["empInfoId"] + "'");
        if (Session["EmpInfoId"].ToString() != Request.QueryString["empInfoId"])
        {


            if (dtempdata.Rows[0]["ReportingEmpId"].ToString() != Session["EmpInfoId"].ToString())
            {
                btn_Save.Visible = false;
                for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
                {
                    TextBox txtSkillInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");
                    TextBox txtSupportingEmp = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupportingEmp");
                    TextBox txtScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("Weight");
                    TextBox txtSelfScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SelfScore");
                    TextBox supervisorScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupervisorScore");
                    txtSkillInfo.ReadOnly = true;
                    txtSupportingEmp.ReadOnly=true;
                    txtScore.ReadOnly = true;
                    txtSelfScore.ReadOnly = true;
                    supervisorScore.ReadOnly = true;
                }
            }
        }
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AppraisalDashboard.aspx");
    }


    private void IniTable()
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
       
        dt.Rows.Add(dr);
        ViewState["PARTB"] = dt;

        gv_AppraisalPartB.DataSource = dt;
        gv_AppraisalPartB.DataBind();
    }

    protected void btn_Add_OnClick(object sender, EventArgs e)
    {


        if (ViewState["PARTB"] == null)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("SkillInfo", typeof (string)));
            dt.Columns.Add(new DataColumn("SupportingEmp", typeof (string)));
            dt.Columns.Add(new DataColumn("Score", typeof (string)));

            dr = dt.NewRow();

            dr["SkillInfo"] = "";
            dr["SupportingEmp"] = "";
            dr["Score"] = "";
            ViewState["PARTB"] = dt;

            gv_AppraisalPartB.DataSource = dt;
            gv_AppraisalPartB.DataBind();
            totalSelf();
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
                TextBox txtScoreself = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SelfScore");
                TextBox txtScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("Score");



                dtCurrentTable.Rows[i]["SkillInfo"] = txtSkillInfo.Text.Trim() == "" ? "" : txtSkillInfo.Text.Trim();
                dtCurrentTable.Rows[i]["SupportingEmp"] = txtSupportingEmp.Text.Trim() == "" ? "" : txtSupportingEmp.Text.Trim();
                dtCurrentTable.Rows[i]["Score"] = txtScoreself.Text.Trim() == "" ? "" : txtScoreself.Text.Trim();
                dtCurrentTable.Rows[i]["SupervisorScore"] = txtScore.Text.Trim() == "" ? 0 : Convert.ToDecimal(txtScore.Text.Trim());
                

            }

            gv_AppraisalPartB.DataSource = dtCurrentTable;
            gv_AppraisalPartB.DataBind();
            totalSelf();
            CalculateTotal();
        }

    }

    protected void lb_Remove_OnClick(object sender, EventArgs e)
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
            totalSelf();
            CalculateTotal();
            
        }
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {

      if (Validation() == true)
        {
            List<AppraisalBehaveArea> aList = new List<AppraisalBehaveArea>();

            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {
                TextBox txtSkillInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");
                TextBox txtSupportingEmp = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupportingEmp");
                TextBox txtScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("Weight");
                TextBox txtSelfScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SelfScore");
                Label SetScore = (Label)gv_AppraisalPartB.Rows[i].FindControl("SetScore");
                TextBox supervisorScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupervisorScore");

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
                      "alert('Operation Successful...');window.location ='AppraisalDashboard.aspx';",
                      true);
                }
                else
                {
                    AlertMessageBoxShow("Operation Failed");
                }
            }
        }
       
    }


    private bool Validation()
    {

        bool isVAlid = true;

        string a = id_mastetID.Value;
        if (a == "0")
        {
            isVAlid = false;
            aShowMessage.ShowMessageBox("Save The Functional Part First  ", this);
        }
        if (gv_AppraisalPartB.Rows.Count == 0)
        {
            isVAlid = false;
            aShowMessage.ShowMessageBox("Kpi Info Required ", this);
        }

        Label tst = (Label)gv_AppraisalPartB.FooterRow.FindControl("lblTotalMark");
       // Label tst2 = (Label)gv_AppraisalPartB.FooterRow.FindControl("lblTotalMarkSelf");
        for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
        {
             
            TextBox SelfScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupervisorScore");
           
           
            if (SelfScore.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Supervisor Score Required ", this);
                break;
            }
            

            //if (txtTarget.Text == "")
            //{
            //    isVAlid = false;
            //    aShowMessage.ShowMessageBox("Score Required ", this);
            //    break;
            //}


        }
        decimal weightTotal = tst.Text == "" ? 0 : Convert.ToDecimal(tst.Text.ToString());
     //   decimal weightTotal2 = tst2.Text == "" ? 0 : Convert.ToDecimal(tst2.Text.ToString());

        if (weightTotal > 25) // || weightTotal2 > 25
        {
            aShowMessage.ShowMessageBox("Total Score Can Not be Bigger than 25 In Part A ", this);
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

    protected void btn_Review_OnClick(object sender, EventArgs e)
    {
        bool result = _appPartA.GoForReview(Convert.ToInt32(id_selfID));
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
    private decimal TotalWeight()
    {
        decimal result = 0;
        for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
        {
            TextBox SelfScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SelfScore");


            result += SelfScore.Text.Trim().ToString() == "" ? 0 : Convert.ToDecimal(SelfScore.Text.Trim());

        }
        return result;
    }
    protected void gv_AppraisalFunc_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblselfscrore = (Label)e.Row.FindControl("lblselfscrore");

            lblselfscrore.Text = TotalWeight().ToString();

        }
    }
    protected void Score_OnTextChanged(object sender, EventArgs e)
    {

        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        TextBox Weight = (TextBox)gv_AppraisalPartB.Rows[rowID].FindControl("Weight");
        TextBox txtsupWeight = (TextBox)gv_AppraisalPartB.Rows[rowID].FindControl("SupervisorScore");
        decimal wet = 0,supervisor=0;
        wet = Convert.ToDecimal(Weight.Text);
        supervisor = Convert.ToDecimal(txtsupWeight.Text);

        if (supervisor>wet)
        {
            txtsupWeight.Text = "0";
            AlertMessageBoxShow("Supervisor Mark Cannot Be Greater Then Weight");
        }


        double weightTotal = 0;

        for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
        {
            TextBox txtWeight = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupervisorScore");

            if (txtWeight.Text == "")
            {
               
                weightTotal = weightTotal + 0;
            }
            else
            {
                weightTotal = weightTotal + Convert.ToDouble(txtWeight.Text.ToString());
            }


        }
        Label tst = (Label)gv_AppraisalPartB.FooterRow.FindControl("lblTotalMark");
        tst.Text = weightTotal.ToString();
    }

    private void CalculateTotal()
    {
         double weightTotal = 0;
             for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
        {
            TextBox txtWeight = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("Score");

            if (txtWeight.Text == "")
            {
                weightTotal = weightTotal + 0;
            }
            else
            {
                weightTotal = weightTotal + Convert.ToDouble(txtWeight.Text.ToString());
            }


        }
        Label tst = (Label)gv_AppraisalPartB.FooterRow.FindControl("lblTotalMark");
        tst.Text = weightTotal.ToString();
    }
    

    private void totalSelf()
    {
        decimal weightTotal = 0;

        if (gv_AppraisalPartB.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {
                TextBox txtWeight = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupervisorScore");




                if (txtWeight.Text == "")
                {
                    weightTotal = weightTotal + 0;
                }
                else
                {
                    weightTotal = weightTotal + (string.IsNullOrEmpty(txtWeight.Text.Trim()) ? 0 : Convert.ToDecimal(txtWeight.Text.ToString()));
                }




            }



            Label tst2 = (Label)gv_AppraisalPartB.FooterRow.FindControl("lblTotalMark");
            tst2.Text = weightTotal.ToString();
        }

    }


    protected void SelfScore_OnTextChanged(object sender, EventArgs e)
    {
       decimal weightTotal = 0;

        if (gv_AppraisalPartB.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {
                TextBox txtWeight = (TextBox) gv_AppraisalPartB.Rows[i].FindControl("SelfScore");




                if (txtWeight.Text == "")
                {
                    weightTotal = weightTotal + 0;
                }
                else
                {
                    weightTotal = weightTotal + Convert.ToDecimal(txtWeight.Text.ToString());
                }




            }



            Label tst2 = (Label) gv_AppraisalPartB.FooterRow.FindControl("lblTotalMarkSelf");
            tst2.Text = weightTotal.ToString();
        }
    }
}