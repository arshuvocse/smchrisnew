using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAO.HRIS_DAO;

public partial class Appraisal_AppraisalTraining : System.Web.UI.Page
{
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();
    private AppraisalPartBDAL _appraisalPartBdal = new AppraisalPartBDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          int masterID = int.Parse(Request.QueryString["masterId"]); //string.IsNullOrEmpty(Request.QueryString["masterId"]).ToString();
            int empInfoId = int.Parse(Request.QueryString["empInfoId"]);
            id_mastetID.Value = masterID.ToString();

            if (empInfoId > 0)
            {
                DataTable dtEmp = _appPartA.GetEmployeeDetails(empInfoId);
                if (dtEmp.Rows.Count > 0)
                {

                   

                    lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();
                    LocationLabel.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
                    lblPlace.Text = dtEmp.Rows[0]["Location"].ToString();
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
                    ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();

                    joiningDateLabel.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
                    
                    joiningDateLabel.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");

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
                for (int i = 0; i < gv_AppraisalTraining.Rows.Count; i++)
                {
                    TextBox txtSkillInfo = (TextBox)gv_AppraisalTraining.Rows[i].FindControl("TrainingNeeds");
                    TextBox txtSupportingEmp = (TextBox)gv_AppraisalTraining.Rows[i].FindControl("TrainingStart");
                    TextBox txtScore = (TextBox)gv_AppraisalTraining.Rows[i].FindControl("TrainingEnd");
                    DropDownList ddlQuater = (DropDownList)gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");

                    gv_AppraisalTraining.Rows[i].Cells[3].Visible = false;
                    gv_AppraisalTraining.HeaderRow.Cells[3].Visible = false;
                    txtSkillInfo.ReadOnly = true;
                    //txtSupportingEmp.ReadOnly = true;
                    //txtScore.ReadOnly = true;
                    ddlQuater.Enabled = false;



                }
            }
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

        gv_AppraisalTraining.DataSource = dt;
        gv_AppraisalTraining.DataBind();
    }

    protected void btn_Add_OnClick(object sender, EventArgs e)
    {
        if (ViewState["TrainingPart"] == null)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("TrainingNeeds", typeof (string)));
            dt.Columns.Add(new DataColumn("quaterID", typeof(string)));
         //   dt.Columns.Add(new DataColumn("TrainingEnd", typeof (string)));

            dr = dt.NewRow();

            dr["TrainingNeeds"] = "";
            dr["quaterID"] = "";
          //  dr["TrainingEnd"] = "";

            dt.Rows.Add(dr);
            ViewState["TrainingPart"] = dt;

            gv_AppraisalTraining.DataSource = dt;
            gv_AppraisalTraining.DataBind();
        }
        else
        {
            DataTable dtCurrentTable = (DataTable)ViewState["TrainingPart"];

            DataRow drCurrentRow = null;

            drCurrentRow = dtCurrentTable.NewRow();



            dtCurrentTable.Rows.Add(drCurrentRow);


            ViewState["TrainingPart"] = dtCurrentTable;

            for (int i = 0; i < gv_AppraisalTraining.Rows.Count; i++)
            {
                TextBox txtSkillInfo = (TextBox)gv_AppraisalTraining.Rows[i].FindControl("TrainingNeeds");
                DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");



                dtCurrentTable.Rows[i]["TrainingNeeds"] = txtSkillInfo.Text.Trim().ToString() == "" ? "" : txtSkillInfo.Text.Trim().ToString();
                dtCurrentTable.Rows[i]["quaterID"] = txt_BranchCountry.SelectedValue;


            }

            gv_AppraisalTraining.DataSource = dtCurrentTable;
            gv_AppraisalTraining.DataBind();
            for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
            {
                DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");
                txt_BranchCountry.SelectedValue = dtCurrentTable.Rows[i]["quaterID"].ToString();
            }
        }
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        //if (ViewState["TrainingPart"] != null && gv_AppraisalTraining.Rows.Count > 1)
        //{

        //    LinkButton lb = (LinkButton)sender;
        //    GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        //    int rowID = gvRow.RowIndex;
        //    DataTable dt = (DataTable)ViewState["TrainingPart"];
        //    dt.Rows.Remove(dt.Rows[rowID]);
        //    if (dt.Rows.Count == 0)
        //    {
        //        ViewState["TrainingPart"] = null;
        //    }
        //    else
        //    {
        //        ViewState["TrainingPart"] = dt;
        //    }


        //    gv_AppraisalTraining.DataSource = dt;
        //    gv_AppraisalTraining.DataBind();

        //}
    }


    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        List<AppraisalTrainingNeeds> aList = new List<AppraisalTrainingNeeds>();
        for (int i = 0; i < gv_AppraisalTraining.Rows.Count; i++)
        {
            TextBox txtSkillInfo = (TextBox)gv_AppraisalTraining.Rows[i].FindControl("TrainingNeeds");
            TextBox txtSupportingEmp = (TextBox)gv_AppraisalTraining.Rows[i].FindControl("TrainingStart");
            TextBox txtScore = (TextBox)gv_AppraisalTraining.Rows[i].FindControl("TrainingEnd");
            DropDownList ddlQuater = (DropDownList)gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");

            if (txtSkillInfo.Text.Trim().ToString() != "" )
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
                    "alert('Operation Successful...');window.location ='AppraisalDashboard.aspx';",
                    true);


 
            }
            else
            {
                AlertMessageBoxShow("Operation Failed");
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

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
       Response.Redirect("AppraisalDashboard.aspx");
    }

    protected void lb_Remove_OnClick(object sender, EventArgs e)
    {
        if (ViewState["TrainingPart"] != null && gv_AppraisalTraining.Rows.Count > 1)
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
            for (int i = 0; i < gv_AppraisalTraining.Rows.Count; i++)
            {
                TextBox txtSkillInfo = (TextBox)gv_AppraisalTraining.Rows[i].FindControl("TrainingNeeds");
                DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");



                dt.Rows[i]["TrainingNeeds"] = txtSkillInfo.Text.Trim().ToString() == "" ? "" : txtSkillInfo.Text.Trim().ToString();
                dt.Rows[i]["quaterID"] = txt_BranchCountry.SelectedValue;


            }
            dt.Rows.Remove(dt.Rows[rowID]);
            gv_AppraisalTraining.DataSource = dt;
            gv_AppraisalTraining.DataBind();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DropDownList txt_BranchCountry = (DropDownList)gv_AppraisalTraining.Rows[i].FindControl("QuaterDropDownList1");
                txt_BranchCountry.SelectedValue = dt.Rows[i]["quaterID"].ToString();
            }

        }
    }
}