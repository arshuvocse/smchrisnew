using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.SuspendAndDiciplinary_Dal;
using DAL.TrainingDAL;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class Appraisal_AppraisalSelfFunctional : System.Web.UI.Page
{

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();
    private AppraisalPartBDAL _appraisalPartBdal = new AppraisalPartBDAL();
    EmployeeSuspendDal aSuspendDal = new EmployeeSuspendDal();
    private JdDAL _jdDal = new JdDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private AppraislDashboardDAL _appDashboard = new AppraislDashboardDAL();
    SupervisorMenuAppDAL appDal = new SupervisorMenuAppDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (Request.QueryString["EmpInfoId"] != "")
            {
                if (Session["EmpInfoId"].ToString() == Request.QueryString["EmpInfoId"])
                {

                }
                else
                {
                    Response.Redirect("../Default.aspx");
                }
            }

            if (!IsPostBack)
            {  ButtonVisible();
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
                        btn_Save.Visible = false;
                        editButton.Visible = true;
                        int mid = Convert.ToInt32(dtaa.Rows[0][0]);
                        //DataTable dtdata = _appPartA.GetPreComments(mid.ToString());
                        //GridView2.DataSource = dtdata;
                        //GridView2.DataBind();

                        id_mastetID.Value = mid.ToString();
                        DataTable dt = _appPartA.GetAppraisalSelf(mid);

                        txt_employee.Text = dt.Rows[0]["employee"].ToString();
                        ddlFinancialYear.SelectedValue = dt.Rows[0]["FinancialYearId"].ToString();
                        HFCompanyId.Value = dt.Rows[0]["CompanyId"].ToString();


                        //  txt_employee_OnTextChanged(txt_employee, (EventArgs) e);
                        DataTable dt2 = _appPartA.GetAppraisalSelfDetails(mid);
                        DataTable dt3 = _appPartA.GetAppraisalSelfB(mid);
                        gv_AppraisalFunc.DataSource = dt2;
                        ViewState["KPIFUNC"] = dt2;
                        ViewState["PARTB"] = dt3;
                        gv_AppraisalFunc.DataBind();
                        gv_AppraisalPartB.DataSource = dt3;
                        gv_AppraisalPartB.DataBind();

                        if (gv_AppraisalPartB.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt3.Rows.Count; i++)
                            {


                                RadioButtonList rbType =
                                   (RadioButtonList)gv_AppraisalPartB.Rows[i].FindControl("rbType");
                                DropDownList ddlSkill =
                                (DropDownList)gv_AppraisalPartB.Rows[i].FindControl("ddlSkill");
                                TextBox SkillInfo =
                       (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");

                                if (dt3.Rows[i]["SkillType"].ToString().Trim() == "Others")
                                {

                                    SkillInfo.Visible = true;
                                    ddlSkill.Visible = false;
                                    rbType.Items[5].Selected = true;

                                    DataTable dtSkill2 =
        _appPartA.getKPIBehaviourName(rbType.SelectedValue);

                                    ddlSkill.DataValueField = "KPIBehaviourName";
                                    ddlSkill.DataTextField = "KPIBehaviourName";
                                    ddlSkill.DataSource = dtSkill2;
                                    ddlSkill.DataBind();
                                    ddlSkill.Items.Insert(0, new ListItem("Select From List", String.Empty));
                                    ddlSkill.SelectedIndex = 0;

                                    try
                                    {


                                        ddlSkill.SelectedValue = dt3.Rows[i]["SkillInfo"].ToString().Trim();
                                    }
                                    catch (Exception)
                                    {

                                        //throw;
                                    }


                                }
                                else
                                {

                                    ddlSkill.Enabled = false;
                                    if (dt3.Rows[i]["SkillType"].ToString().Trim() == "Personal")
                                    {
                                        rbType.Items[0].Selected = true;
                                        ddlSkill.Enabled = true;

                                        DataTable dtSkill2 =
             _appPartA.getKPIBehaviourName(rbType.SelectedValue);

                                        ddlSkill.DataValueField = "KPIBehaviourName";
                                        ddlSkill.DataTextField = "KPIBehaviourName";
                                        ddlSkill.DataSource = dtSkill2;
                                        ddlSkill.DataBind();
                                        ddlSkill.Items.Insert(0, new ListItem("Select From List", String.Empty));
                                        ddlSkill.SelectedIndex = 0;

                                        try
                                        {


                                            ddlSkill.SelectedValue = dt3.Rows[i]["SkillInfo"].ToString().Trim();
                                        }
                                        catch (Exception)
                                        {

                                            //throw;
                                        }

                                    }

                                    else  if (dt3.Rows[i]["SkillType"].ToString().Trim() == "Team")
                                    {
                                        rbType.Items[1].Selected = true;
                                        ddlSkill.Enabled = true;

                                        DataTable dtSkill2 =
        _appPartA.getKPIBehaviourName(rbType.SelectedValue);

                                        ddlSkill.DataValueField = "KPIBehaviourName";
                                        ddlSkill.DataTextField = "KPIBehaviourName";
                                        ddlSkill.DataSource = dtSkill2;
                                        ddlSkill.DataBind();
                                        ddlSkill.Items.Insert(0, new ListItem("Select From List", String.Empty));
                                        ddlSkill.SelectedIndex = 0;

                                        try
                                        {


                                            ddlSkill.SelectedValue = dt3.Rows[i]["SkillInfo"].ToString().Trim();
                                        }
                                        catch (Exception)
                                        {

                                            //throw;
                                        }

                                    }

                                    else  if (dt3.Rows[i]["SkillType"].ToString().Trim() == "Result Focus")
                                    {
                                        rbType.Items[2].Selected = true;
                                        ddlSkill.Enabled = true;
                                        DataTable dtSkill2 =
        _appPartA.getKPIBehaviourName(rbType.SelectedValue);

                                        ddlSkill.DataValueField = "KPIBehaviourName";
                                        ddlSkill.DataTextField = "KPIBehaviourName";
                                        ddlSkill.DataSource = dtSkill2;
                                        ddlSkill.DataBind();
                                        ddlSkill.Items.Insert(0, new ListItem("Select From List", String.Empty));
                                        ddlSkill.SelectedIndex = 0;

                                        try
                                        {


                                            ddlSkill.SelectedValue = dt3.Rows[i]["SkillInfo"].ToString().Trim();
                                        }
                                        catch (Exception)
                                        {

                                            //throw;
                                        }
                                    }


                                    else   if (dt3.Rows[i]["SkillType"].ToString().Trim() == "Interpersonal Skill")
                                    {
                                        rbType.Items[3].Selected = true;
                                        ddlSkill.Enabled = true;

                                        DataTable dtSkill2 =
             _appPartA.getKPIBehaviourName(rbType.SelectedValue);

                                        ddlSkill.DataValueField = "KPIBehaviourName";
                                        ddlSkill.DataTextField = "KPIBehaviourName";
                                        ddlSkill.DataSource = dtSkill2;
                                        ddlSkill.DataBind();
                                        ddlSkill.Items.Insert(0, new ListItem("Select From List", String.Empty));
                                        ddlSkill.SelectedIndex = 0;

                                        try
                                        {


                                            ddlSkill.SelectedValue = dt3.Rows[i]["SkillInfo"].ToString().Trim();
                                        }
                                        catch (Exception)
                                        {

                                            //throw;
                                        }

                                    }


                                    else  if (dt3.Rows[i]["SkillType"].ToString().Trim() == "Leadership")
                                    {
                                        rbType.Items[4].Selected = true;
                                        ddlSkill.Enabled = true;

                                        DataTable dtSkill2 =
              _appPartA.getKPIBehaviourName(rbType.SelectedValue);

                                        ddlSkill.DataValueField = "KPIBehaviourName";
                                        ddlSkill.DataTextField = "KPIBehaviourName";
                                        ddlSkill.DataSource = dtSkill2;
                                        ddlSkill.DataBind();
                                        ddlSkill.Items.Insert(0, new ListItem("Select From List", String.Empty));
                                        ddlSkill.SelectedIndex = 0;

                                        try
                                        {


                                            ddlSkill.SelectedValue = dt3.Rows[i]["SkillInfo"].ToString().Trim();
                                        }
                                        catch (Exception)
                                        {

                                            //throw;
                                        }
                                    }
                                    else
                                    {
                                        ddlSkill.Enabled = false;


                                        DataTable dtSkill =
                                     _appPartA.getKPIBehaviourNameWithOutParam();

                                        ddlSkill.DataValueField = "KPIBehaviourName";
                                        ddlSkill.DataTextField = "KPIBehaviourName";
                                        ddlSkill.DataSource = dtSkill;
                                        ddlSkill.DataBind();
                                        ddlSkill.Items.Insert(0, new ListItem("Select From List", String.Empty));
                                        ddlSkill.SelectedIndex = 0;
                                       
                                    }
                                   

                                    SkillInfo.Visible =  false;


                                    ddlSkill.Visible = true;

                                    
                                   
                                   
                                }


                                DropDownList ddlWeight =
                                              (DropDownList)gv_AppraisalPartB.Rows[i].FindControl("ddlWeight");

                                ddlWeight.SelectedValue = dt3.Rows[i]["SetScore"].ToString();
                                try
                                {
                                    ddlSkill.SelectedValue = dt3.Rows[i]["SkillInfo"].ToString().Trim();

                                }
                                catch (Exception)
                                {

                                    //throw;
                                }

                            }
                        }

                        CalculateBEHAVIORALTotal();
                        CalculateTotal();
                        CalculateB();


                        //Get Versions
                        if (mid > 0)
                        {
                            DataTable versions = _appPartA.GetApproveLogBySelfMaster(mid);

                            if (versions.Rows.Count > 0)
                            {
                                ApprovalComments.Visible = true;
                                GridView2.DataSource = versions;
                                GridView2.DataBind();
                            }
                        }
                    }

                    else
                    {
                        GetEmpinfo(empId.ToString());
                        // ddlFinancialYear.SelectedValue = int.Parse(Request.QueryString["financialYearId"]);
                    }
                }
            }
        }
        catch (Exception)
        {
            
            
        }
       
    }

    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {


            if (Session["Status"].ToString() == "Add")
            {
                btn_Save.Visible = true;
                submitVerifyButton.Visible = true;
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                editButton.Visible = false;
                submitVerifyButton.Visible = true;
            }
            else if (Session["Status"].ToString() == "View")
            {
                editButton.Visible = false;
                submitButton.Visible = false;
                btn_Save.Visible = false;
                orBTN.Visible = false;
            }
         
            //else if (Session["Status"].ToString() == "Delete")
            //{
            //    delButton.Visible = true;
            //}
            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("AppraisalSelfList.aspx");
        }
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }


    protected void btn_Add_OnClick(object sender, EventArgs e)
    {
        if (ViewState["KPIFUNC"] == null)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("KpiInfo", typeof (string)));
            dt.Columns.Add(new DataColumn("KpiWeight", typeof (string)));
            dt.Columns.Add(new DataColumn("KpiWeightPer", typeof (string)));
            dt.Columns.Add(new DataColumn("Target", typeof (string)));
            dt.Columns.Add(new DataColumn("TargetPer", typeof (string)));
            dt.Columns.Add(new DataColumn("Deadline", typeof (string)));
            dt.Columns.Add(new DataColumn("MidYearStatus", typeof (string)));
            
            dt.Columns.Add(new DataColumn("SelfMark", typeof (string)));
            dt.Columns.Add(new DataColumn("IsActive", typeof (bool)));
            dr = dt.NewRow();

            dr["KpiInfo"] = "";
            dr["KpiWeight"] = "";
            dr["KpiWeightPer"] = "";
            dr["Target"] = "";
            dr["TargetPer"] = "";
            dr["Deadline"] = "";
            dr["MidYearStatus"] = "";
            dr["IsActive"] = "False";
           
            dr["SelfMark"] = "";
            dt.Rows.Add(dr);
            ViewState["KPIFUNC"] = dt;

            gv_AppraisalFunc.DataSource = dt;
            gv_AppraisalFunc.DataBind();
        }

        else
        {
            DataTable dtCurrentTable = (DataTable) ViewState["KPIFUNC"];

            DataRow drCurrentRow = null;

            drCurrentRow = dtCurrentTable.NewRow();

            drCurrentRow["IsActive"] = "True";

            dtCurrentTable.Rows.Add(drCurrentRow);


            ViewState["KPIFUNC"] = dtCurrentTable;

            for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
            {
                TextBox tbKpi = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
                TextBox txtWeight = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
                TextBox txtWeightPer = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
                TextBox txtTarget = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
                TextBox txtTargetPer = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtTargetPer");
                TextBox txtDeadLine = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
                TextBox txtMidStatus = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
                CheckBox chkisactive = (CheckBox) gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");
               
                TextBox txtMark = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                dtCurrentTable.Rows[i]["KpiInfo"] = tbKpi.Text.Trim().ToString() == ""
                    ? ""
                    : tbKpi.Text.Trim().ToString();

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
                    dtCurrentTable.Rows[i]["SelfMark"] = txtMark.Text.Trim().ToString() == ""
                ? 0
                : Convert.ToDecimal(txtMark.Text.Trim().ToString());
                }
                catch (Exception)
                {

                    dtCurrentTable.Rows[i]["SelfMark"] = "0";
                }
            
                dtCurrentTable.Rows[i]["IsActive"] = chkisactive.Checked;
                //dtCurrentTable.Rows[i]["MidYearStatus"] = txtMidStatus.Text.Trim().ToString();

            }

            gv_AppraisalFunc.DataSource = dtCurrentTable;
            gv_AppraisalFunc.DataBind();

            CalculateTotal();
        }
    }


    public void Remove(int rowindex)
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("KpiInfo");
        aDataTable.Columns.Add("KpiWeight");
        aDataTable.Columns.Add("KpiWeightPer");
        aDataTable.Columns.Add("Target");
        aDataTable.Columns.Add("TargetPer");
        aDataTable.Columns.Add("Deadline");
        aDataTable.Columns.Add("MidYearStatus");
        aDataTable.Columns.Add("IsActive");
        aDataTable.Columns.Add("SelfMark");

      
        DataRow row = null;

        for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
        {
            if (i != rowindex)
            {


                row = aDataTable.NewRow();

                TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
                TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
                TextBox txtWeightPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
                TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
                TextBox txtTargetPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTargetPer");
                TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
                TextBox txtMidStatus = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
                CheckBox chkisactive = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");

                TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                row["KpiInfo"] = tbKpi.Text.Trim().ToString() == ""
                 ? ""
                 : tbKpi.Text.Trim().ToString();

                try
                {
                    row["KpiWeight"] = txtWeight.Text.Trim().ToString() == ""
                 ? ""
                 : txtWeight.Text.Trim().ToString();
                }
                catch (Exception)
                {
                    row["KpiWeight"] = "0";

                }



                try
                {
                    row["KpiWeightPer"] = txtWeightPer.Text.Trim().ToString() == ""
                  ? ""
                  : txtWeightPer.Text.Trim().ToString();
                }
                catch (Exception)
                {
                    row["KpiWeightPer"] = "0";
                    //throw;
                }

                try
                {
                    row["Target"] = txtTarget.Text.Trim().ToString() == ""
                 ? ""
                 : txtTarget.Text.Trim().ToString();
                }
                catch (Exception)
                {

                    row["Target"] = "0";
                }

                try
                {
                    row["TargetPer"] = txtTargetPer.Text.Trim().ToString() == ""
                  ? ""
                  : txtTargetPer.Text.Trim().ToString();
                }
                catch (Exception)
                {

                    row["TargetPer"] = "0";
                }

                row["Deadline"] = txtDeadLine.Text.Trim().ToString() == ""
                    ? ""
                    : txtDeadLine.Text.Trim().ToString();
                try
                {
                    row["SelfMark"] = txtMark.Text.Trim().ToString() == ""
                ? 0
                : Convert.ToDecimal(txtMark.Text.Trim().ToString());
                }
                catch (Exception)
                {

                    row["SelfMark"] = "0";
                }

                row["IsActive"] = chkisactive.Checked;
                //dtCurrentTable.Rows[i]["MidYearStatus"] = txtMidStatus.Text.Trim().ToString();

                   aDataTable.Rows.Add(row);
            }
        }

        //row = aDataTable.NewRow();

        //row["ByerId"] = "";

        //aDataTable.Rows.Add(row);

        gv_AppraisalFunc.DataSource = aDataTable;
        gv_AppraisalFunc.DataBind();
       



    }


    protected void lb_Remove_OnClick(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
     


        DataTable aTable = new DataTable();
        aTable.Columns.Add(new DataColumn("KpiInfo", typeof(string)));
        aTable.Columns.Add(new DataColumn("KpiWeight", typeof(string)));
        aTable.Columns.Add(new DataColumn("KpiWeightPer", typeof(string)));
        aTable.Columns.Add(new DataColumn("Target", typeof(string)));
        aTable.Columns.Add(new DataColumn("TargetPer", typeof(string)));
        aTable.Columns.Add(new DataColumn("Deadline", typeof(string)));
        aTable.Columns.Add(new DataColumn("MidYearStatus", typeof(string)));
        aTable.Columns.Add(new DataColumn("IsActive", typeof(bool)));
        aTable.Columns.Add(new DataColumn("SelfMark", typeof(string)));
        DataRow dr;

       
        for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
        {
            if (i != rowIndex)
            {
                dr = aTable.NewRow();

                TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
                TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
                TextBox txtWeightPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
                TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
                TextBox txtTargetPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTargetPer");
                TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
                TextBox txtMidStatus = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
                CheckBox chkisactive = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");

                TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                dr["KpiInfo"] = tbKpi.Text.Trim().ToString() == ""
                  ? ""
                  : tbKpi.Text.Trim().ToString();

                try
                {
                    dr["KpiWeight"] = txtWeight.Text.Trim().ToString() == ""
                 ? ""
                 : txtWeight.Text.Trim().ToString();
                }
                catch (Exception)
                {
                    dr["KpiWeight"] = "0";

                }



                try
                {
                    dr["KpiWeightPer"] = txtWeightPer.Text.Trim().ToString() == ""
                  ? ""
                  : txtWeightPer.Text.Trim().ToString();
                }
                catch (Exception)
                {
                    dr["KpiWeightPer"] = "0";
                    //throw;
                }

                try
                {
                    dr["Target"] = txtTarget.Text.Trim().ToString() == ""
                 ? ""
                 : txtTarget.Text.Trim().ToString();
                }
                catch (Exception)
                {

                    dr["Target"] = "0";
                }

                try
                {
                    dr["TargetPer"] = txtTargetPer.Text.Trim().ToString() == ""
                  ? ""
                  : txtTargetPer.Text.Trim().ToString();
                }
                catch (Exception)
                {

                    dr["TargetPer"] = "0";
                }

                dr["Deadline"] = txtDeadLine.Text.Trim().ToString() == ""
                    ? ""
                    : txtDeadLine.Text.Trim().ToString();
                try
                {
                    dr["SelfMark"] = txtMark.Text.Trim().ToString() == ""
                ? 0
                : Convert.ToDecimal(txtMark.Text.Trim().ToString());
                }
                catch (Exception)
                {

                    dr["SelfMark"] = "0";
                }

                dr["IsActive"] = chkisactive.Checked;
                //dtCurrentTable.Rows[i]["MidYearStatus"] = txtMidStatus.Text.Trim().ToString();

                
                aTable.Rows.Add(dr);
            }
        }
        
        gv_AppraisalFunc.DataSource = aTable;
        gv_AppraisalFunc.DataBind();

        //SetPreviousData_Children();
        //if (ViewState["KPIFUNC"] != null)
        //{
        //    DataTable dt = (DataTable)ViewState["KPIFUNC"];
        //    dt.Rows.Remove(dt.Rows[rowID]);
        //    if (dt.Rows.Count > 0)
        //    {
        //        //Store the current data in ViewState for future reference  
        //        ViewState["KPIFUNC"] = dt;
        //        //Re bind the GridView for the updated data  
        //        gv_AppraisalFunc.DataSource = dt;
        //        gv_AppraisalFunc.DataBind();
        //    }
        //    else
        //    {
        //        ViewState["KPIFUNC"] = null;
        //        //Re bind the GridView for the updated data  
        //        gv_AppraisalFunc.DataSource = null;
        //        gv_AppraisalFunc.DataBind();
        //    }
        //}

        CalculateTotal();
      //  SetPreviousData_Children();
        //if (ViewState["KPIFUNC"] != null && gv_AppraisalFunc.Rows.Count > 1)
        //{

          
        //    DataTable dt = (DataTable)ViewState["KPIFUNC"];
        //    dt.Rows.Remove(dt.Rows[rowID]);
        //    if (dt.Rows.Count == 0)
        //    {
        //        ViewState["KPIFUNC"] = null;
        //    }
        //    else
        //    {
        //        ViewState["KPIFUNC"] = dt;
        //    }


        //    gv_AppraisalFunc.DataSource = dt;
        //    gv_AppraisalFunc.DataBind();
        //    // CalculateTotalParticipant();
        //}
    }
    private void SetPreviousData_Children()
    {
        int rowIndex = 0;
        if (ViewState["KPIFUNC"] != null)
        {
            DataTable dt = (DataTable)ViewState["KPIFUNC"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox tbKpi = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtKpi");
                    TextBox txtWeight = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeight");
                    TextBox txtWeightPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
                    TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
                    TextBox txtTargetPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTargetPer");
                    TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
                    TextBox txtMidStatus = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
                    CheckBox chkisactive = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");

                    TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                    if (i < dt.Rows.Count - 1)
                    {
                        txtWeight.Text= dt.Rows[i]["KpiWeight"].ToString();
                        txtWeightPer.Text = dt.Rows[i]["KpiWeightPer"].ToString();
                        txtTarget.Text = dt.Rows[i]["Target"].ToString();
                        txtTargetPer.Text = dt.Rows[i]["TargetPer"].ToString();
                        txtDeadLine.Text = dt.Rows[i]["Deadline"].ToString();
                        txtMark.Text = dt.Rows[i]["SelfMark"].ToString();
                        try
                        {
                            chkisactive.Checked = Convert.ToBoolean(dt.Rows[i]["IsActive"].ToString());
                        }
                        catch (Exception)
                        {
                            chkisactive.Checked = false;
                            //throw;
                        }

                       }

                    rowIndex++;
                }
            }
        }
    }


   
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserId"] == null || Session["EmpInfoId"] == null)
            {
                AlertMessageBoxShow("Session Expired. Please login again.");
                return;
            }

            if (Validation() == true)
            {
                int empId = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["EmpInfoId"]))
                {
                    int.TryParse(Request.QueryString["EmpInfoId"], out empId);
                }
                if (empId == 0 && !string.IsNullOrEmpty(id_Empid.Value))
                {
                    int.TryParse(id_Empid.Value, out empId);
                }

                int FinancialYearId = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["financialYearId"]))
                {
                    int.TryParse(Request.QueryString["financialYearId"], out FinancialYearId);
                }
                if (FinancialYearId == 0 && ddlFinancialYear.SelectedValue != "")
                {
                    int.TryParse(ddlFinancialYear.SelectedValue, out FinancialYearId);
                }

                int companyId = 0;
                if (!string.IsNullOrEmpty(HFCompanyId.Value))
                {
                    int.TryParse(HFCompanyId.Value, out companyId);
                }

                DataTable dtEmp = _appPartA.CheckAlreadyExist(empId, FinancialYearId, companyId);
                if (dtEmp.Rows.Count > 0)
                {
                    aShowMessage.ShowMessageBox("Already Exist !!!", this);
                }
                else
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
                        CheckBox chkisactive = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");
                        TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                        if (tbKpi != null && !string.IsNullOrEmpty(tbKpi.Text.Trim()) &&
                            txtTarget != null && !string.IsNullOrEmpty(txtTarget.Text.Trim()) &&
                            txtWeight != null && !string.IsNullOrEmpty(txtWeight.Text.Trim()))
                        {
                            AppraisalFunctionalArea area = new AppraisalFunctionalArea();

                            area.KpiInfo = tbKpi.Text.Trim();
                            decimal kWeight = 0; decimal.TryParse(txtWeight.Text.Trim(), out kWeight); area.KpiWeight = kWeight;
                            decimal kWeightPer = 0; decimal.TryParse(txtWeightPer.Text.Trim(), out kWeightPer); area.KpiWeightPer = kWeightPer;
                            decimal kTarget = 0; decimal.TryParse(txtTarget.Text.Trim(), out kTarget); area.Target = kTarget;
                            decimal kTargetPer = 0; decimal.TryParse(txtTargetPer.Text.Trim(), out kTargetPer); area.TargetPer = kTargetPer;

                            DateTime deadline = DateTime.Now;
                            if (txtDeadLine != null && !string.IsNullOrEmpty(txtDeadLine.Text.Trim()))
                            {
                                DateTime.TryParse(txtDeadLine.Text.Trim(), out deadline);
                            }
                            area.Deadline = deadline;
                            area.IsActive = chkisactive != null ? chkisactive.Checked : false;
                            area.SupervisorMark = 0;
                            area.MidYearStatus = " ";

                            functional.Add(area);
                        }
                    }

                    AppraisalMaster aMaster = new AppraisalMaster();
                    aMaster.AppraisalMasterId = string.IsNullOrEmpty(id_mastetID.Value) ? 0 : Convert.ToInt32(id_mastetID.Value);
                    aMaster.EmpInfoId = empId > 0 ? empId : (string.IsNullOrEmpty(id_Empid.Value) ? 0 : Convert.ToInt32(id_Empid.Value));
                    aMaster.FinancialYearId = FinancialYearId;

                    bool result = false;
                    if (functional.Count > 0)
                    {
                        // Master save, functional-detail save, and behavioral-part save run under a single
                        // SqlConnection/SqlTransaction inside the DAL (see AppraisalFunctionalPartDAL.SaveAppraisalSelfTransactional)
                        // so a failure partway through rolls back everything instead of leaving a half-saved record.
                        List<AppraisalBehaveArea> behavioral = BuildAppraisalSelfBehaveList(0);
                        try
                        {
                            int pk = _appPartA.SaveAppraisalSelfTransactional(aMaster, Session["UserId"].ToString(), functional, behavioral, null, null);
                            result = pk > 0;
                        }
                        catch (Exception)
                        {
                            result = false;
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
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow("Error during save: " + ex.Message);
        }
    }


    private List<AppraisalBehaveArea> BuildAppraisalSelfBehaveList(int pk)
    {
        try
        {
            List<AppraisalBehaveArea> aList = new List<AppraisalBehaveArea>();

            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {
                RadioButtonList rbType = (RadioButtonList)gv_AppraisalPartB.Rows[i].FindControl("rbType");
                TextBox SkillInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");
                DropDownList ddlSkill = (DropDownList)gv_AppraisalPartB.Rows[i].FindControl("ddlSkill");
                TextBox txtSupportingEmp = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupportingEmp");
                TextBox txtScore = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("Score");
                DropDownList ddlWeight = (DropDownList)gv_AppraisalPartB.Rows[i].FindControl("ddlWeight");

                AppraisalBehaveArea area = new AppraisalBehaveArea();
                area.AppraisalMasterId = pk;

                string selectedRbType = (rbType != null && !string.IsNullOrEmpty(rbType.SelectedValue)) ? rbType.SelectedValue.Trim() : "";

                if (selectedRbType == "Others")
                {
                    area.SkillInfo = SkillInfo != null ? SkillInfo.Text.Trim() : "";
                }
                else
                {
                    area.SkillInfo = (ddlSkill != null && !string.IsNullOrEmpty(ddlSkill.SelectedValue)) ? ddlSkill.SelectedValue.Trim() : "";
                }

                area.SkillType = selectedRbType;
                area.SupportingEmp = txtSupportingEmp != null ? txtSupportingEmp.Text.Trim() : "";

                decimal scoreVal = 0;
                if (txtScore != null && !string.IsNullOrEmpty(txtScore.Text.Trim()))
                {
                    decimal.TryParse(txtScore.Text.Trim(), out scoreVal);
                }
                area.Score = scoreVal;

                decimal setScoreVal = 0;
                if (ddlWeight != null && !string.IsNullOrEmpty(ddlWeight.SelectedValue))
                {
                    decimal.TryParse(ddlWeight.SelectedValue, out setScoreVal);
                }
                area.SetScore = setScoreVal;

                aList.Add(area);
            }
            return aList;
        }
        catch (Exception)
        {
            return null;
        }
    }
    private void IniTable()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("SkillInfo", typeof(string)));
        dt.Columns.Add(new DataColumn("SupportingEmp", typeof(string)));
        dt.Columns.Add(new DataColumn("Score", typeof(string)));


        for (int i = 0; i < 5; i++)
        {
            dr = dt.NewRow();

            dr["SkillInfo"] = "";
            dr["SupportingEmp"] = "";
            dr["Score"] = "5";

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

            DataTable dtEmp = _appPartA.GetEmployeeDetails(Convert.ToInt32(id));
            if (dtEmp.Rows.Count > 0)
            {
                LoadFinancialYear(Convert.ToInt32(dtEmp.Rows[0]["CompanyId"]));

                HFCompanyId.Value = dtEmp.Rows[0]["CompanyId"].ToString();
                txt_employee.Text=dtEmp.Rows[0]["EmpName"].ToString().Trim();
                lblEmployeeName.Text = txt_employee.Text;
                lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();
                
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
                LocationLabel.Text =   dtEmp.Rows[0]["SalaryLocation"].ToString();
                lblPlace.Text = dtEmp.Rows[0]["Location"].ToString();

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
       
        dr["SelfMark"] = "";
        dr["IsActive"] = "False";
        dt.Rows.Add(dr);
        ViewState["KPIFUNC"] = dt;

        gv_AppraisalFunc.DataSource = dt;
        gv_AppraisalFunc.DataBind();

        for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
        {
            CheckBox isActiveCheckBox = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");

            isActiveCheckBox.Checked = true;
        }
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
                CheckBox chkIsActive = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");
                if (chkIsActive.Checked)
                {


                    TextBox txtWeight = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtWeight");

                    TextBox txtMark = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtMark");


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

        if (string.IsNullOrEmpty(id_Empid.Value))
        {
            if (!string.IsNullOrEmpty(Request.QueryString["EmpInfoId"]))
            {
                id_Empid.Value = Request.QueryString["EmpInfoId"];
            }
            else
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Select Employee ", this);
            }
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
            TextBox txtWeightPer = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtWeightPer");
            TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtTarget");
            TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");
            TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtDeadLine");
            TextBox txtMidStatus = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMidStatus");
            if (string.IsNullOrEmpty(txtWeightPer.Text))
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Expected Number Required ", this);
                break;
            }

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

            if (txtDeadLine.Text == "")
            {
                isVAlid = false;
                aShowMessage.ShowMessageBox("Deadline Required ", this);
                break;
            }
        }
        for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
        {
            DropDownList ddlSkill = (DropDownList)gv_AppraisalPartB.Rows[i].FindControl("ddlSkill");
            TextBox txtSupportingInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupportingEmp");
            RadioButtonList rbType = (RadioButtonList)gv_AppraisalPartB.Rows[i].FindControl("rbType");

            TextBox SkillInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");

            if (rbType != null && rbType.SelectedValue == "Others")
            {
                if (SkillInfo != null && SkillInfo.Text == "")
                {
                    isVAlid = false;
                    aShowMessage.ShowMessageBox("Behaviral Info Required ", this);
                    SkillInfo.Focus();
                    break;
                }
            }
            else
            {
                if (ddlSkill != null && ddlSkill.SelectedValue == "")
                {
                    isVAlid = false;
                    aShowMessage.ShowMessageBox("Behaviral Info Required ", this);
                    ddlSkill.Focus();
                    break;
                }
            }
        }

        Label tst = gv_AppraisalFunc.FooterRow != null ? (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalWeight") : null;
        decimal weightTotal = (tst != null && !string.IsNullOrEmpty(tst.Text)) ? Convert.ToDecimal(tst.Text) : 0;
        if (weightTotal > 75)
        {
            aShowMessage.ShowMessageBox("Total Weight Can Not be Bigger than 75 In Part A ", this);
            isVAlid = false;
        }
        if (weightTotal < 75)
        {
            aShowMessage.ShowMessageBox("Total Weight Can Not be Smaller than 75 In Part A ", this);
            isVAlid = false;
        }

        Label tst2 = gv_AppraisalPartB.FooterRow != null ? (Label)gv_AppraisalPartB.FooterRow.FindControl("lblTotalScore") : null;
        decimal weightTotal2 = (tst2 != null && !string.IsNullOrEmpty(tst2.Text)) ? Convert.ToDecimal(tst2.Text) : 0;
        if (weightTotal2 > 25)
        {
            aShowMessage.ShowMessageBox("Total Score Can Not be Bigger than 25 In Part B ", this);
            isVAlid = false;
        }

        if (weightTotal2 < 25)
        {
            aShowMessage.ShowMessageBox("Total Score Can Not be Smaller than 25 In Part B ", this);
            isVAlid = false;
        }

        for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
        {
            DropDownList ddlWeight = (DropDownList)gv_AppraisalPartB.Rows[i].FindControl("ddlWeight");
            if (ddlWeight != null && ddlWeight.SelectedValue == "")
            {
                aShowMessage.ShowMessageBox("Expected Number Can not be Empty!!! ", this);
                ddlWeight.Focus();
                isVAlid = false;
            }
        }

        return isVAlid;
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("AppraisalSelfList.aspx");
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
        try
        {
            if (Session["UserId"] == null || Session["EmpInfoId"] == null)
            {
                AlertMessageBoxShow("Session Expired. Please login again.");
                return;
            }

            if (Validation() == true)
            {
                int empId = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["EmpInfoId"]))
                {
                    int.TryParse(Request.QueryString["EmpInfoId"], out empId);
                }
                if (empId == 0 && !string.IsNullOrEmpty(id_Empid.Value))
                {
                    int.TryParse(id_Empid.Value, out empId);
                }

                int FinancialYearId = 0;
                if (!string.IsNullOrEmpty(Request.QueryString["financialYearId"]))
                {
                    int.TryParse(Request.QueryString["financialYearId"], out FinancialYearId);
                }
                if (FinancialYearId == 0 && ddlFinancialYear.SelectedValue != "")
                {
                    int.TryParse(ddlFinancialYear.SelectedValue, out FinancialYearId);
                }

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
                    CheckBox chkisactive = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");
                    TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                    if (tbKpi != null && !string.IsNullOrEmpty(tbKpi.Text.Trim()) &&
                        txtTarget != null && !string.IsNullOrEmpty(txtTarget.Text.Trim()) &&
                        txtWeight != null && !string.IsNullOrEmpty(txtWeight.Text.Trim()))
                    {
                        AppraisalFunctionalArea area = new AppraisalFunctionalArea();

                        area.KpiInfo = tbKpi.Text.Trim();
                        decimal kWeight = 0; decimal.TryParse(txtWeight.Text.Trim(), out kWeight); area.KpiWeight = kWeight;
                        decimal kWeightPer = 0; decimal.TryParse(txtWeightPer.Text.Trim(), out kWeightPer); area.KpiWeightPer = kWeightPer;
                        decimal kTarget = 0; decimal.TryParse(txtTarget.Text.Trim(), out kTarget); area.Target = kTarget;
                        decimal kTargetPer = 0; decimal.TryParse(txtTargetPer.Text.Trim(), out kTargetPer); area.TargetPer = kTargetPer;

                        DateTime deadline = DateTime.Now;
                        if (txtDeadLine != null && !string.IsNullOrEmpty(txtDeadLine.Text.Trim()))
                        {
                            DateTime.TryParse(txtDeadLine.Text.Trim(), out deadline);
                        }
                        area.Deadline = deadline;
                        area.IsActive = chkisactive != null ? chkisactive.Checked : false;
                        area.SupervisorMark = 0;
                        area.MidYearStatus = " ";

                        functional.Add(area);
                    }
                }

                AppraisalMaster aMaster = new AppraisalMaster();
                aMaster.AppraisalMasterId = string.IsNullOrEmpty(id_mastetID.Value) ? 0 : Convert.ToInt32(id_mastetID.Value);
                aMaster.EmpInfoId = empId > 0 ? empId : (string.IsNullOrEmpty(id_Empid.Value) ? 0 : Convert.ToInt32(id_Empid.Value));
                aMaster.FinancialYearId = FinancialYearId;

                bool result = false;
                if (functional.Count > 0)
                {
                    // Master save, functional-detail save, and behavioral-part save run under a single
                    // SqlConnection/SqlTransaction inside the DAL (see AppraisalFunctionalPartDAL.SaveAppraisalSelfTransactional)
                    // so a failure partway through rolls back everything instead of leaving a half-saved record.
                    List<AppraisalBehaveArea> behavioral = BuildAppraisalSelfBehaveList(0);
                    try
                    {
                        int pk = _appPartA.SaveAppraisalSelfTransactional(aMaster, Session["UserId"].ToString(), functional, behavioral, null, null);
                        result = pk > 0;
                    }
                    catch (Exception)
                    {
                        result = false;
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
        catch (Exception ex)
        {
            AlertMessageBoxShow("Error during update: " + ex.Message);
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
        TextBox txtweight = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtweight");
        TextBox txtTarget = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtTarget");
        TextBox txtTargetPer = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtTargetPer");

        decimal weight = string.IsNullOrEmpty(txtweight.Text) ? 0 : Convert.ToDecimal(txtweight.Text.Trim());
        decimal Target = string.IsNullOrEmpty(txtTarget.Text) ? 0 : Convert.ToDecimal(txtTarget.Text.Trim());

        txtTargetPer.Text = "0";
        if (weight >= Target)
        {
          
        }
        else
        {
           // txtTarget.Text = "";
          //  aShowMessage.ShowMessageBox("Target (Number) Can Not be Bigger than Weight (Number)	", this);
        }

       
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

    protected void isActiveCheckBox_OnCheckedChanged(object sender, EventArgs e)
    {
        try
        {
            decimal weightTotal = 0;
            decimal markTotal = 0;
            if (gv_AppraisalFunc.Rows.Count > 0)
            {
                for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
                {
                    CheckBox chkIsActive = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");
                    if (chkIsActive.Checked)
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

                }

                Label tst = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalWeight");
                tst.Text = weightTotal.ToString();

                Label tst2 = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalMark");
                tst2.Text = markTotal.ToString();



                if (weightTotal > 75)
                {
                    aShowMessage.ShowMessageBox("Total Weight Can Not be Bigger than 75 In Part A ", this);


                    CheckBox lb = (CheckBox) sender;
                    GridViewRow gvRow = (GridViewRow) lb.NamingContainer;
                    int rowID = gvRow.RowIndex;
                    TextBox txtWeight = (TextBox) gv_AppraisalFunc.Rows[rowID].FindControl("txtWeight");

                    TextBox txtWeightPer = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtWeightPer");
                    CheckBox chkIsActive = (CheckBox)gv_AppraisalFunc.Rows[rowID].FindControl("isActiveCheckBox");

                    txtWeight.Text = "0";
                    txtWeightPer.Text = "0";

                    chkIsActive.Checked = false;
                }


            }
        }
        catch (Exception)
        {


        }

        CalculateTotal();

    }

    protected void submitVerifyButton_OnClick(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserId"] == null || Session["EmpInfoId"] == null)
            {
                AlertMessageBoxShow("Session Expired. Please login again.");
                return;
            }

            if (string.IsNullOrEmpty(id_Empid.Value) && !string.IsNullOrEmpty(Request.QueryString["EmpInfoId"]))
            {
                id_Empid.Value = Request.QueryString["EmpInfoId"];
            }

            if (Validation() == true)
            {

                bool SubmitStatus = false;
                DataTable dtFinalApprovalSubmit = new DataTable();
                DataTable dtSuppervisorSubmit = new DataTable();
                int FinalApproveCount = 0;
                DataTable CheckFinalApprovalNew = _appPartA.CheckFinalApprovalConditionNotSuppervisor(id_Empid.Value);


                DataTable dtempdataSup = _appDashboard.GetEmpInfo(" WHERE ReportingEmpId is not null and  EmpInfoId='" + id_Empid.Value + "'");

                String ddd = "";
                try
                {
                    ddd = CheckFinalApprovalNew.Rows[0]["IsAllEmployee"].ToString();
                }
                catch (Exception)
                {

                    //throw;
                }

                if (ddd == "True")
                {
                    SubmitStatus = true;

                }
                else if (dtempdataSup.Rows.Count > 0)
                {


                    DataTable aDataTable = new DataTable();
                    aDataTable.Columns.Add("EmpInfoId");
                    aDataTable.Columns.Add("EmpName");
                    aDataTable.Columns.Add("EmpMasterCode");
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

                if (FinalApproveCount == 1 && CheckFinalApprovalNew.Rows.Count > 0)
                {
                    SubmitStatus = true;
                }

                if (SubmitStatus)
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
                        CheckBox chkisactive = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");
                        TextBox txtMark = (TextBox)gv_AppraisalFunc.Rows[i].FindControl("txtMark");

                        if (tbKpi != null && !string.IsNullOrEmpty(tbKpi.Text.Trim()) &&
                            txtTarget != null && !string.IsNullOrEmpty(txtTarget.Text.Trim()) &&
                            txtWeight != null && !string.IsNullOrEmpty(txtWeight.Text.Trim()))
                        {
                            AppraisalFunctionalArea area = new AppraisalFunctionalArea();

                            area.KpiInfo = tbKpi.Text.Trim();
                            decimal kWeight = 0; decimal.TryParse(txtWeight.Text.Trim(), out kWeight); area.KpiWeight = kWeight;
                            decimal kWeightPer = 0; decimal.TryParse(txtWeightPer.Text.Trim(), out kWeightPer); area.KpiWeightPer = kWeightPer;
                            decimal kTarget = 0; decimal.TryParse(txtTarget.Text.Trim(), out kTarget); area.Target = kTarget;
                            decimal kTargetPer = 0; decimal.TryParse(txtTargetPer.Text.Trim(), out kTargetPer); area.TargetPer = kTargetPer;

                            DateTime deadline = DateTime.Now;
                            if (txtDeadLine != null && !string.IsNullOrEmpty(txtDeadLine.Text.Trim()))
                            {
                                DateTime.TryParse(txtDeadLine.Text.Trim(), out deadline);
                            }
                            area.Deadline = deadline;
                            area.IsActive = chkisactive != null ? chkisactive.Checked : false;
                            area.SupervisorMark = 0;
                            area.MidYearStatus = " ";

                            functional.Add(area);
                        }
                    }

                    AppraisalMaster aMaster = new AppraisalMaster();

                    aMaster.AppraisalMasterId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);
                    aMaster.EmpInfoId = Convert.ToInt32(id_Empid.Value);
                    aMaster.FinancialYearId = int.Parse(Request.QueryString["financialYearId"]);

                    DataTable dtempdata = _appPartA.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");

                    int repId = 0;

                    try
                    {
                        repId = (int)dtempdata.Rows[0]["ReportingEmpId"];
                    }
                    catch (Exception)
                    {
                        //throw;
                    }

                    DataTable CheckFinalApproval =
                        _appPartA.CheckFinalApprovalConditionNotSuppervisor(Session["EmpInfoId"].ToString());

                    bool result = false;

                    if (CheckFinalApproval.Rows.Count > 0)
                    {
                        if (CheckFinalApproval.Rows[0]["IsAllEmployee"].ToString() == "True")
                        {
                            if (functional.Count > 0)
                            {
                                // Master save, approval-log inserts, contractual status update, functional-detail save
                                // and behavioral-part save all run under a single SqlConnection/SqlTransaction inside
                                // the DAL (see AppraisalFunctionalPartDAL.SaveAppraisalSelfTransactional) so a failure
                                // partway through rolls back everything instead of leaving a half-submitted record.
                                AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO
                                {
                                    ActionStatus = "Drafted",
                                    ApproveDate = DateTime.Now,
                                    ApproveBy = Session["UserId"].ToString(),
                                    PreEmpInfoId = Convert.ToInt32(0),
                                    ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString()),
                                    Comments = txt_Comments.Text,
                                    CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString())
                                };

                                AppraisalSelfAppLogDAO appLogDao1 = new AppraisalSelfAppLogDAO
                                {
                                    ActionStatus = "Verified",
                                    ApproveDate = DateTime.Now,
                                    ApproveBy = Session["UserId"].ToString(),
                                    PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                                    ForEmpInfoId = Convert.ToInt32(CheckFinalApproval.Rows[0]["EmpInfoId"].ToString()),
                                    Comments = txt_Comments.Text,
                                    CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString())
                                };

                                List<AppraisalBehaveArea> behavioral = BuildAppraisalSelfBehaveList(0);
                                int pk = 0;
                                try
                                {
                                    pk = _appPartA.SaveAppraisalSelfTransactional(aMaster, Session["UserId"].ToString(), functional, behavioral,
                                        new List<AppraisalSelfAppLogDAO> { appLogDao, appLogDao1 }, "Verified");
                                    result = pk > 0;
                                }
                                catch (Exception)
                                {
                                    result = false;
                                }

                                if (result)
                                {
                                    SenMailForApprved(appLogDao1.ForEmpInfoId, " KPI Approval ", @"  <br/> Dear Sir, <br/>
An Employee's KPI is waiting for your approval. <br/><br/>
please login for the details from the below link.<br/>    http://182.160.103.234:8088/
 <br/> Thank You.");
                                }
                            }
                            else
                            {
                                result = false;
                            }
                        }
                        else if (repId > 0)
                        {
                            if (functional.Count > 0)
                            {
                                // Master save, approval-log inserts, contractual status update, functional-detail save
                                // and behavioral-part save all run under a single SqlConnection/SqlTransaction inside
                                // the DAL (see AppraisalFunctionalPartDAL.SaveAppraisalSelfTransactional) so a failure
                                // partway through rolls back everything instead of leaving a half-submitted record.
                                AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO
                                {
                                    ActionStatus = "Drafted",
                                    ApproveDate = DateTime.Now,
                                    ApproveBy = Session["UserId"].ToString(),
                                    PreEmpInfoId = Convert.ToInt32(0),
                                    ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString()),
                                    Comments = txt_Comments.Text,
                                    CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString())
                                };

                                AppraisalSelfAppLogDAO appLogDao1 = new AppraisalSelfAppLogDAO
                                {
                                    ActionStatus = "Verified",
                                    ApproveDate = DateTime.Now,
                                    ApproveBy = Session["UserId"].ToString(),
                                    PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                                    ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString()),
                                    Comments = txt_Comments.Text,
                                    CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString())
                                };

                                List<AppraisalBehaveArea> behavioral = BuildAppraisalSelfBehaveList(0);
                                int pk = 0;
                                try
                                {
                                    pk = _appPartA.SaveAppraisalSelfTransactional(aMaster, Session["UserId"].ToString(), functional, behavioral,
                                        new List<AppraisalSelfAppLogDAO> { appLogDao, appLogDao1 }, "Verified");
                                    result = pk > 0;
                                }
                                catch (Exception)
                                {
                                    result = false;
                                }

                                if (result)
                                {
                                    SenMailForApprved(appLogDao1.ForEmpInfoId, " KPI Approval ", @"  <br/> Dear Sir, <br/>
An Employee's KPI is waiting for your approval. <br/><br/>
please login for the details from the below link.<br/>    http://182.160.103.234:8088/
 <br/> Thank You.");
                                }
                            }
                            else
                            {
                                result = false;
                            }
                        }
                        else
                        {
                            AlertMessageBoxShow("Your Reporting Employee is not set yet!!!");
                        }
                    }
                    else
                    {
                        AlertMessageBoxShow("final approval has not been set yet!!!");
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
                else
                {
                    AlertMessageBoxShow("Your Suppervisor or Final Approver has not been set yet. Please contact with HR Department !!!");
                }
            }
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow("Operation Failed: " + ex.Message);
        }
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


    protected void txtDeadLine_OnTextChanged(object sender, EventArgs e)
    {
        TextBox lb = (TextBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        TextBox txtDeadLine = (TextBox)gv_AppraisalFunc.Rows[rowID].FindControl("txtDeadLine");



        try
        {
            string birthdt = Convert.ToDateTime(txtDeadLine.Text.Trim()).ToString("dd/MMM/yyyy");

        }
        catch (Exception)
        {
            AlertMessageBoxShow("Give A valid Date !!");
            txtDeadLine.Focus();
            txtDeadLine.Text = string.Empty;
        }

    }

    protected void ddlWeight_OnTextChanged(object sender, EventArgs e)
    {
        CalculateBEHAVIORALTotal();
    }


    protected void CalculateBEHAVIORALTotal()
    {
        decimal weightTotal = 0;
         
        if (gv_AppraisalPartB.Rows.Count > 0)
        {
            for (int i = 0; i < gv_AppraisalPartB.Rows.Count; i++)
            {



                DropDownList ddlWeight = (DropDownList)gv_AppraisalPartB.Rows[i].FindControl("ddlWeight");




                if (ddlWeight.SelectedValue == "")
                    {
                        weightTotal = weightTotal + 0;
                    }
                    else
                    {
                        weightTotal = weightTotal + Convert.ToDecimal(ddlWeight.SelectedValue.ToString());
                    }
                    

                

             
}
            Label tst = (Label)gv_AppraisalPartB.FooterRow.FindControl("ddllblTotalWeight");
            tst.Text = weightTotal.ToString();

            
        }
    }

    protected void rbType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //Session["KPI_Type"] = "";
        RadioButtonList lb = (RadioButtonList)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        RadioButtonList rbType = (RadioButtonList)gv_AppraisalPartB.Rows[rowID].FindControl("rbType");
        DropDownList ddlSkill = (DropDownList)gv_AppraisalPartB.Rows[rowID].FindControl("ddlSkill");
        TextBox SkillInfo = (TextBox)gv_AppraisalPartB.Rows[rowID].FindControl("SkillInfo");

        SkillInfo.Visible = false;
        ddlSkill.Visible = false;

        if (rbType.SelectedValue == "Others")
        {
            SkillInfo.Visible = true;
        }
        else
        {
            ddlSkill.Visible = true;
            DataTable dtSkill =
               _appPartA.getKPIBehaviourName(rbType.SelectedValue);

            ddlSkill.DataValueField = "KPIBehaviourName";
            ddlSkill.DataTextField = "KPIBehaviourName";
            ddlSkill.DataSource = dtSkill;
            ddlSkill.DataBind();
            ddlSkill.Items.Insert(0, new ListItem("Select From List", String.Empty));
            ddlSkill.SelectedIndex = 0;

            try
            {


                ddlSkill.SelectedValue = SkillInfo.Text;
            }
            catch (Exception)
            {

                //throw;
            }
            ddlSkill.Enabled = true;
        }
       
        //if (rbType.SelectedValue!="")
        //{
        //    Session["KPI_Type"] = rbType.SelectedValue;
        //}
    }
}