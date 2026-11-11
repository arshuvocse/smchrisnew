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

public partial class Appraisal_AppraisalPartBOwn : System.Web.UI.Page
{
    private TrainingDAL _trainingDal = new TrainingDAL();
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();
    private AppraisalPartBDAL _appraisalPartBdal = new AppraisalPartBDAL();
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
            DataTable dt33 = _appPartA.GetAppraisalPartB(masterID);
            DataTable dt3 = _appPartA.GetAppraisalSelfB(Convert.ToInt32(selfMaster));
            ViewState["PARTB"] = dt3;


            if (dt33.Rows.Count > 0)
            {
                gv_AppraisalPartB.DataSource = dt33;
                gv_AppraisalPartB.DataBind();
                CalculateB();
                totalSelf();
            }
            else
            {
                gv_AppraisalPartB.DataSource = dt3;
                gv_AppraisalPartB.DataBind();
                CalculateB();
            }
          //  totalSelf();
            if (empInfoId > 0)
            {
                DataTable dtEmp = _appPartA.GetEmployeeDetails(empInfoId);
                if (dtEmp.Rows.Count > 0)
                {

                    lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();
              
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
                    lblPlace.Text = dtEmp.Rows[0]["Location"].ToString();
                    ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString(); 

                 
                    // id_Empid.Value = dtEmp.Rows[0]["EmpInfoId"].ToString();
                }
            }
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

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    private void totalSelf()
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


    protected void btn_Save_OnClick(object sender, EventArgs e)
    {

       // if (Validation() == true)
        {
            List<AppraisalBehaveArea> aList = new List<AppraisalBehaveArea>();

            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {
                Label txtSkillInfo = (Label)gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");
                Label txtSupportingEmp = (Label)gv_AppraisalPartB.Rows[i].FindControl("SupportingEmp");
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

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AppraisalDashboard.aspx");
    }
}