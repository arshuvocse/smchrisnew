using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;

public partial class Appraisal_AppraisalFinal : System.Web.UI.Page
{

    private TrainingDAL _trainingDal = new TrainingDAL();
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();
    private AppraisalPartBDAL _appraisalPartBdal = new AppraisalPartBDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            {
                int masterID = int.Parse(Request.QueryString["masterId"]);
                int empInfoId = int.Parse(Request.QueryString["empInfoId"]);
                decimal partA=0;
                decimal partB=0;
                try
                {
                     partA = decimal.Parse(Request.QueryString["pa"]);
                    partB = decimal.Parse(Request.QueryString["pb"]);
                }
                catch (Exception)
                {
                    
                    //AlertMessageBoxShow("Please Complete Functional & Behavioural Part");
                    //Response.Redirect("AppraisalDashboard.aspx");
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Please Complete Functional & Behavioural Part...');window.location ='AppraisalDashboard.aspx';",
                   true);
                }
                
                id_mastetID.Value = masterID.ToString();

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

                        LocationLabel.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
                      

                        ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();

                        joiningDateLabel.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
                        partAScore.Text = partA.ToString("F");
                        partBScore.Text = partB.ToString("F");
                        totalScore.Text = (partA + partB).ToString("F");
                        decimal total = partA + partB;
                        if (total <= 60)
                        {
                            lblStatus.Text = "Poor";
                        }

                        if (total >= 61 && total <= 70)
                        {
                            lblStatus.Text = "Average";
                        }

                        if (total >= 71 && total <= 80)
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


                  


                  


                        // IniTable();
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
                                recommend.SelectedValue = "5";
                            }
                            if (Other == true)
                            {
                                recommend.SelectedValue = "6";
                                txtnote.Text = dt.Rows[0]["Note"].ToString();
                            }

                            recommend_SelectedIndexChanged(recommend, (EventArgs)e);
                        }
                    }
                }


            }
            CheckImmmiedietSuperVisor();

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
    protected void btn_Save_OnClick(object sender, EventArgs e)
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
            appraisalFinal.SpecialStep = Convert.ToInt32(txtStep.Text);

        }

        if (recom == "3")
        {
            appraisalFinal.IsPromotion = true;
            appraisalFinal.GeneralIncrement = false;
            appraisalFinal.SpecialIncrement = false;
            appraisalFinal.GeneralIncrement = false;
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
        appraisalFinal.AppraisalMasterId = Convert.ToInt32(id_mastetID.Value);
        appraisalFinal.FinalStatus = lblStatus.Text;
        appraisalFinal.TotalScore = Convert.ToDecimal(totalScore.Text);


        bool result = _appraisalPartBdal.SaaveFinalStatus(appraisalFinal);
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
}