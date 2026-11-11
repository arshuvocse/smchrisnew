using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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

        //                        if (dt3.Rows[i]["SkillType"].ToString().Trim() == "Others")
        //                        {

        //                            SkillInfo.Visible = true;
        //                            ddlSkill.Visible = false;
        //                            rbType.Items[5].Selected = true;

        //                            DataTable dtSkill2 =
        //_appPartA.getKPIBehaviourName(rbType.SelectedValue);

        //                            ddlSkill.DataValueField = "KPIBehaviourName";
        //                            ddlSkill.DataTextField = "KPIBehaviourName";
        //                            ddlSkill.DataSource = dtSkill2;
        //                            ddlSkill.DataBind();
        //                            ddlSkill.Items.Insert(0, new ListItem("Select From List", String.Empty));
        //                            ddlSkill.SelectedIndex = 0;

        //                            try
        //                            {


        //                                ddlSkill.SelectedValue = dt3.Rows[i]["SkillInfo"].ToString().Trim();
        //                            }
        //                            catch (Exception)
        //                            {

        //                                //throw;
        //                            }


        //                        }
        //                        else
        //                        {

        //                            ddlSkill.Enabled = false;
        //                            if (dt3.Rows[i]["SkillType"].ToString().Trim() == "Personal")
        //                            {
        //                                rbType.Items[0].Selected = true;
        //                                ddlSkill.Enabled = true;

        //                                DataTable dtSkill2 =
        //     _appPartA.getKPIBehaviourName(rbType.SelectedValue);

        //                                ddlSkill.DataValueField = "KPIBehaviourName";
        //                                ddlSkill.DataTextField = "KPIBehaviourName";
        //                                ddlSkill.DataSource = dtSkill2;
        //                                ddlSkill.DataBind();
        //                                ddlSkill.Items.Insert(0, new ListItem("Select From List", String.Empty));
        //                                ddlSkill.SelectedIndex = 0;

        //                                try
        //                                {


        //                                    ddlSkill.SelectedValue = dt3.Rows[i]["SkillInfo"].ToString().Trim();
        //                                }
        //                                catch (Exception)
        //                                {

        //                                    //throw;
        //                                }

        //                            }

        //                            else  if (dt3.Rows[i]["SkillType"].ToString().Trim() == "Team")
        //                            {
        //                                rbType.Items[1].Selected = true;
        //                                ddlSkill.Enabled = true;

        //                                DataTable dtSkill2 =
        //_appPartA.getKPIBehaviourName(rbType.SelectedValue);

        //                                ddlSkill.DataValueField = "KPIBehaviourName";
        //                                ddlSkill.DataTextField = "KPIBehaviourName";
        //                                ddlSkill.DataSource = dtSkill2;
        //                                ddlSkill.DataBind();
        //                                ddlSkill.Items.Insert(0, new ListItem("Select From List", String.Empty));
        //                                ddlSkill.SelectedIndex = 0;

        //                                try
        //                                {


        //                                    ddlSkill.SelectedValue = dt3.Rows[i]["SkillInfo"].ToString().Trim();
        //                                }
        //                                catch (Exception)
        //                                {

        //                                    //throw;
        //                                }

        //                            }

        //                            else  if (dt3.Rows[i]["SkillType"].ToString().Trim() == "Result Focus")
        //                            {
        //                                rbType.Items[2].Selected = true;
        //                                ddlSkill.Enabled = true;
        //                                DataTable dtSkill2 =
        //_appPartA.getKPIBehaviourName(rbType.SelectedValue);

        //                                ddlSkill.DataValueField = "KPIBehaviourName";
        //                                ddlSkill.DataTextField = "KPIBehaviourName";
        //                                ddlSkill.DataSource = dtSkill2;
        //                                ddlSkill.DataBind();
        //                                ddlSkill.Items.Insert(0, new ListItem("Select From List", String.Empty));
        //                                ddlSkill.SelectedIndex = 0;

        //                                try
        //                                {


        //                                    ddlSkill.SelectedValue = dt3.Rows[i]["SkillInfo"].ToString().Trim();
        //                                }
        //                                catch (Exception)
        //                                {

        //                                    //throw;
        //                                }
        //                            }


        //                            else   if (dt3.Rows[i]["SkillType"].ToString().Trim() == "Interpersonal Skill")
        //                            {
        //                                rbType.Items[3].Selected = true;
        //                                ddlSkill.Enabled = true;

        //                                DataTable dtSkill2 =
        //     _appPartA.getKPIBehaviourName(rbType.SelectedValue);

        //                                ddlSkill.DataValueField = "KPIBehaviourName";
        //                                ddlSkill.DataTextField = "KPIBehaviourName";
        //                                ddlSkill.DataSource = dtSkill2;
        //                                ddlSkill.DataBind();
        //                                ddlSkill.Items.Insert(0, new ListItem("Select From List", String.Empty));
        //                                ddlSkill.SelectedIndex = 0;

        //                                try
        //                                {


        //                                    ddlSkill.SelectedValue = dt3.Rows[i]["SkillInfo"].ToString().Trim();
        //                                }
        //                                catch (Exception)
        //                                {

        //                                    //throw;
        //                                }

        //                            }


        //                            else  if (dt3.Rows[i]["SkillType"].ToString().Trim() == "Leadership")
        //                            {
        //                                rbType.Items[4].Selected = true;
        //                                ddlSkill.Enabled = true;

        //                                DataTable dtSkill2 =
        //      _appPartA.getKPIBehaviourName(rbType.SelectedValue);

        //                                ddlSkill.DataValueField = "KPIBehaviourName";
        //                                ddlSkill.DataTextField = "KPIBehaviourName";
        //                                ddlSkill.DataSource = dtSkill2;
        //                                ddlSkill.DataBind();
        //                                ddlSkill.Items.Insert(0, new ListItem("Select From List", String.Empty));
        //                                ddlSkill.SelectedIndex = 0;

        //                                try
        //                                {


        //                                    ddlSkill.SelectedValue = dt3.Rows[i]["SkillInfo"].ToString().Trim();
        //                                }
        //                                catch (Exception)
        //                                {

        //                                    //throw;
        //                                }
        //                            }
        //                            else
        //                            {
        //                                ddlSkill.Enabled = false;


        //                                DataTable dtSkill =
        //                             _appPartA.getKPIBehaviourNameWithOutParam();

        //                                ddlSkill.DataValueField = "KPIBehaviourName";
        //                                ddlSkill.DataTextField = "KPIBehaviourName";
        //                                ddlSkill.DataSource = dtSkill;
        //                                ddlSkill.DataBind();
        //                                ddlSkill.Items.Insert(0, new ListItem("Select From List", String.Empty));
        //                                ddlSkill.SelectedIndex = 0;
                                       
        //                            }
                                   

        //                            SkillInfo.Visible =  false;


        //                            ddlSkill.Visible = true;

                                    
                                   
                                   
        //                        }


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
            Response.Redirect("BSCOKRAppraisalSelfList.aspx");
        }
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }


    protected void btn_Add_OnClick(object sender, EventArgs e)
    {
        // Get the GridView control from the parent container
        GridViewRow parentRow = (GridViewRow)((LinkButton)sender).NamingContainer;
        GridView gvAppraisalFunc = (GridView)parentRow.NamingContainer;

        // Create a new DataTable to hold the data
        DataTable dt = new DataTable();
        dt.Columns.Add("BSC Dimension");
        dt.Columns.Add("ObjectiveGoal");
        dt.Columns.Add("KPI");
        dt.Columns.Add("Weightage");
        dt.Columns.Add("Initiatives");
        dt.Columns.Add("Deadline");

        // Add existing rows to the DataTable
        foreach (GridViewRow row in gvAppraisalFunc.Rows)
        {
            DataRow dr = dt.NewRow();
            dr["BSC Dimension"] = ((DropDownList)row.FindControl("ddlBSCDimension")).SelectedValue;
            dr["ObjectiveGoal"] = ((TextBox)row.FindControl("txtObjectiveGoal")).Text;

            // Handle nested GridView data
            GridView gvNestedTable = (GridView)row.FindControl("gvNestedTable");
            DataTable nestedDt = new DataTable();
            nestedDt.Columns.Add("KPI");
            nestedDt.Columns.Add("Weightage");
            nestedDt.Columns.Add("Initiatives");
            nestedDt.Columns.Add("Deadline");

            foreach (GridViewRow nestedRow in gvNestedTable.Rows)
            {
                DataRow nestedDr = nestedDt.NewRow();
                nestedDr["KPI"] = ((TextBox)nestedRow.FindControl("txtKPI")).Text;
                nestedDr["Weightage"] = ((TextBox)nestedRow.FindControl("txtWeightage")).Text;
                nestedDr["Initiatives"] = ((TextBox)nestedRow.FindControl("txtInitiatives")).Text;
                nestedDr["Deadline"] = ((TextBox)nestedRow.FindControl("txtDeadline")).Text;
                nestedDt.Rows.Add(nestedDr);
            }

            // Optionally, you might want to handle nestedDt if needed
            // Add the nested data to the main DataTable or process it as required
            dt.Rows.Add(dr);
        }

        // Add a new empty row to the DataTable
        DataRow newRow = dt.NewRow();
        dt.Rows.Add(newRow);

        // Bind the updated DataTable to the GridView
        gvAppraisalFunc.DataSource = dt;
        gvAppraisalFunc.DataBind();

        // Ensure nested GridView in each row is also re-bound or updated if needed
        foreach (GridViewRow row in gvAppraisalFunc.Rows)
        {
            GridView gvNestedTable = (GridView)row.FindControl("gvNestedTable");
            if (gvNestedTable != null)
            {
                DataTable nestedDt = new DataTable();
                nestedDt.Columns.Add("KPI");
                nestedDt.Columns.Add("Weightage");
                nestedDt.Columns.Add("Initiatives");
                nestedDt.Columns.Add("Deadline");

                // Bind an empty row or any required default data to nested GridView
                DataRow nestedNewRow = nestedDt.NewRow();
                nestedDt.Rows.Add(nestedNewRow);

                gvNestedTable.DataSource = nestedDt;
                gvNestedTable.DataBind();
            }
        }
    }


    //protected void btn_AddNested_Click(object sender, EventArgs e)
    //{
    //    // Find the GridViewRow for the clicked button
    //    LinkButton btn = (LinkButton)sender;
    //    GridViewRow mainRow = (GridViewRow)btn.NamingContainer;
    //    GridView nestedGrid = (GridView)mainRow.FindControl("gvNestedTable");

    //    // Retrieve the current data source of the nested GridView
    //    DataTable nestedTable = ViewState["NestedTable"] as DataTable;
    //    if (nestedTable == null)
    //    {
    //        nestedTable = new DataTable();


    //        nestedTable.Columns.Add("KPI");
    //        nestedTable.Columns.Add("Weightage");
    //        nestedTable.Columns.Add("Initiatives");
    //        nestedTable.Columns.Add("Deadline");
    //    }

    //    // Add a new row to the DataTable
    //    DataRow newRow = nestedTable.NewRow();
    //    newRow["KPI"] = "";
    //    newRow["Weightage"] = "";
    //    newRow["Initiatives"] = "";
    //    newRow["Deadline"] = "";
    //    nestedTable.Rows.Add(newRow);

    //    // Store the updated DataTable in ViewState
    //    ViewState["NestedTable"] = nestedTable;

    //    // Re-bind the nested GridView to the updated DataTable
    //    nestedGrid.DataSource = nestedTable;
    //    nestedGrid.DataBind();
    //}
    protected void btn_AddNested_Click(object sender, EventArgs e)
    {
        LinkButton btnAddNested = (LinkButton)sender;
        GridViewRow parentRow = (GridViewRow)btnAddNested.NamingContainer;

        // Find the nested GridView
        GridView gvNestedTable = (GridView)parentRow.FindControl("gvNestedTable");

        // Retrieve the NestedTable from ViewState
        DataTable nestedTable = ViewState["NestedTable"] as DataTable;

        if (nestedTable == null)
        {
            // Initialize the DataTable if it's null (this should match the structure in btn_AddNestedRow_OnClick)
            nestedTable = new DataTable();
            nestedTable.Columns.Add("KPI");
            nestedTable.Columns.Add("Weightage");
            nestedTable.Columns.Add("Initiatives");
            nestedTable.Columns.Add("Deadline");
        }

        // Add a new row
        DataRow dr = nestedTable.NewRow();
        dr["KPI"] = string.Empty;
        dr["Weightage"] = string.Empty;
        dr["Initiatives"] = string.Empty;
        dr["Deadline"] = string.Empty;
        nestedTable.Rows.Add(dr);

        // Store the updated DataTable back into ViewState
        ViewState["NestedTable"] = nestedTable;

        // Rebind the data to the nested GridView
        gvNestedTable.DataSource = nestedTable;
        gvNestedTable.DataBind();

        // Hide the add button after adding one row
        btnAddNested.Visible = false;
    }

    protected void btn_AddNestedRow_OnClick(object sender, EventArgs e)
    {
        // Identify the button that triggered the event and the row it belongs to
        LinkButton btn = (LinkButton)sender;
        GridViewRow mainRow = (GridViewRow)btn.NamingContainer;

        // Find the nested GridView within the current row
        GridView nestedGrid = (GridView)mainRow.FindControl("gvNestedTable");

        // Generate a unique key for the current row
        int masterRowIndex = mainRow.RowIndex;
        string viewStateKey = "NestedTable_" + masterRowIndex;

        // Retrieve the nested table data from ViewState
        DataTable nestedTable = ViewState[viewStateKey] as DataTable;

        if (nestedTable == null)
        {
            nestedTable = new DataTable();
            nestedTable.Columns.Add("KPI");
            nestedTable.Columns.Add("Weightage");
            nestedTable.Columns.Add("Initiatives");
            nestedTable.Columns.Add("Deadline");
        }

        // Copy existing rows from the nested GridView to the DataTable
        //foreach (GridViewRow row in nestedGrid.Rows)
        //{
        //    DataRow dataRow = nestedTable.NewRow();
        //    dataRow["KPI"] = ((TextBox)row.FindControl("txtKPI")).Text;
        //    dataRow["Weightage"] = ((TextBox)row.FindControl("txtWeightage")).Text;
        //    dataRow["Initiatives"] = ((TextBox)row.FindControl("txtInitiatives")).Text;
        //    dataRow["Deadline"] = ((TextBox)row.FindControl("txtDeadline")).Text;
        //    nestedTable.Rows.Add(dataRow);
        //}

        // Add a new empty row at the end
        DataRow newRow = nestedTable.NewRow();
        newRow["KPI"] = string.Empty;
        newRow["Weightage"] = string.Empty;
        newRow["Initiatives"] = string.Empty;
        newRow["Deadline"] = string.Empty;
        nestedTable.Rows.Add(newRow);

        // Save the updated DataTable back to ViewState
        ViewState[viewStateKey] = nestedTable;

        // Rebind the updated DataTable to the nested GridView
        nestedGrid.DataSource = nestedTable;
        nestedGrid.DataBind();

        // Optionally hide the add button if required
        btn.Visible = false;
    }

    // Method to get the current data in the nested grid
    private DataTable GetNestedTableData(GridView gvNestedTable)
    {
        // Create a DataTable to hold the data of the nested grid
        DataTable dt = new DataTable();
        dt.Columns.Add("KPI");
        dt.Columns.Add("Weightage");
        dt.Columns.Add("Initiatives");
        dt.Columns.Add("Deadline");

        // Check if gvNestedTable and its rows are not null
        if (gvNestedTable != null && gvNestedTable.Rows.Count > 0)
        {
            foreach (GridViewRow row in gvNestedTable.Rows)
            {
                DataRow dr = dt.NewRow();
                dr["KPI"] = ((TextBox)row.FindControl("txtKPI")).Text;
                dr["Weightage"] = ((TextBox)row.FindControl("txtWeightage")).Text;
                dr["Initiatives"] = ((TextBox)row.FindControl("txtInitiatives")).Text;
                dr["Deadline"] = ((TextBox)row.FindControl("txtDeadline")).Text;
                dt.Rows.Add(dr);
            }
        }
        return dt;
    }




    protected void btn_RmvNested_Click(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)((LinkButton)sender).Parent.Parent).RowIndex;

        GridViewRow Gv2Row = (GridViewRow)((LinkButton)sender).NamingContainer;
        GridView Childgrid = (GridView)(Gv2Row.Parent.Parent);
        GridViewRow Gv1Row = (GridViewRow)(Childgrid.NamingContainer);
        int masterrowindex = Gv1Row.RowIndex;

        GridView gvNestedTable = ((GridView)gv_AppraisalFunc.Rows[masterrowindex].FindControl("gvNestedTable"));
        DataTable aTableInitial = new DataTable();
        aTableInitial.Columns.Add("KPI");
        aTableInitial.Columns.Add("Weightage");
        aTableInitial.Columns.Add("Initiatives");
        aTableInitial.Columns.Add("Deadline");

        DataRow aRow = null;

        for (int i = 0; i < gvNestedTable.Rows.Count; i++)
        {
            if (i != rowIndex)
            {
                aRow = aTableInitial.NewRow();
                aRow["KPI"] = ((TextBox)gvNestedTable.Rows[i].FindControl("txtKPI")).Text;
                aRow["Weightage"] = ((TextBox)gvNestedTable.Rows[i].FindControl("txtWeightage")).Text;
                aRow["Initiatives"] = ((TextBox)gvNestedTable.Rows[i].FindControl("txtInitiatives")).Text;
                aRow["Deadline"] = ((TextBox)gvNestedTable.Rows[i].FindControl("txtDeadline")).Text;
                aTableInitial.Rows.Add(aRow);
            }
        }

        gvNestedTable.DataSource = aTableInitial;
        gvNestedTable.DataBind();

        // Find the btn_AddNestedRow control and set its visibility
        LinkButton btnAddNestedRow = (LinkButton)gv_AppraisalFunc.Rows[masterrowindex].FindControl("btn_AddNestedRow");
        if (btnAddNestedRow != null)
        {
            // Show button if there are no rows left
         
            if (gvNestedTable.Rows.Count == 0)
            {
                btnAddNestedRow.Visible = true;
                gvNestedTable.DataSource = null;
                gvNestedTable.DataBind();
            }
        }
    }



    //protected void btn_AddNestedRow_OnClick(object sender, EventArgs e)
    //{
    //    // Get the parent row
    //    LinkButton btnAddNestedRow = (LinkButton)sender;
    //    GridViewRow parentRow = (GridViewRow)btnAddNestedRow.NamingContainer;

    //    // Find the nested GridView
    //    GridView gvNestedTable = (GridView)parentRow.FindControl("gvNestedTable");

    //    // Retrieve the current data source of the nested GridView (if any)
    //    DataTable dt = gvNestedTable.DataSource as DataTable;
    //    if (dt == null)
    //    {
    //        dt = new DataTable();
    //        dt.Columns.Add("SubObjective", typeof(string));
    //        dt.Columns.Add("SubMeasure", typeof(string));
    //    }

    //    // Add a new row to the DataTable
    //    DataRow newRow = dt.NewRow();
    //    newRow["SubObjective"] = ""; // You can set default values here
    //    newRow["SubMeasure"] = "";
    //    dt.Rows.Add(newRow);

    //    // Bind the updated DataTable to the nested GridView
    //    gvNestedTable.DataSource = dt;
    //    gvNestedTable.DataBind();
    //}


    // Helper method to get the DataTable from the current GridView rows
    private DataTable GetDataTableFromGridView()
    {
        DataTable dt = new DataTable();

        // Define the columns that match the GridView
        dt.Columns.Add("KpiInfo");
        dt.Columns.Add("KpiWeight", typeof(int));
        dt.Columns.Add("KpiWeightPer", typeof(int));
        dt.Columns.Add("Target", typeof(int));
        dt.Columns.Add("Deadline", typeof(string));
        dt.Columns.Add("IsActive", typeof(bool));

        // Loop through each row in the GridView and add it to the DataTable
        foreach (GridViewRow row in gv_AppraisalFunc.Rows)
        {
            DataRow dr = dt.NewRow();

            dr["KpiInfo"] = ((TextBox)row.FindControl("txtKpi")).Text;
            dr["KpiWeight"] = Convert.ToInt32(((TextBox)row.FindControl("txtWeight")).Text);
            dr["KpiWeightPer"] = Convert.ToInt32(((TextBox)row.FindControl("txtWeightPer")).Text);
            dr["Target"] = Convert.ToInt32(((TextBox)row.FindControl("txtTarget")).Text);
            dr["Deadline"] = ((TextBox)row.FindControl("txtDeadLine")).Text;
            dr["IsActive"] = ((CheckBox)row.FindControl("isActiveCheckBox")).Checked;

            dt.Rows.Add(dr);
        }

        return dt;
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

        if (Validation() == true)
        {

            int empId = int.Parse(Request.QueryString["EmpInfoId"]);

            int FinancialYearId = int.Parse(Request.QueryString["financialYearId"]);


            DataTable dtEmp = _appPartA.CheckAlreadyExist(Convert.ToInt32(empId), FinancialYearId,
                Convert.ToInt32(HFCompanyId.Value));
            if (dtEmp.Rows.Count > 0)
            {
                aShowMessage.ShowMessageBox("Already Exist !!!", this);

            }
            else
            {



                List<AppraisalFunctionalArea> functional = new List<AppraisalFunctionalArea>();

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

                    if (tbKpi.Text != "" && txtTarget.Text != "" && txtWeight.Text != "")
                    {
                        AppraisalFunctionalArea area = new AppraisalFunctionalArea();

                        area.KpiInfo = tbKpi.Text.Trim().ToString();
                        area.KpiWeight = Convert.ToDecimal(txtWeight.Text.Trim().ToString());
                        area.KpiWeightPer = Convert.ToDecimal(txtWeightPer.Text.Trim().ToString());
                        area.Target = Convert.ToDecimal(txtTarget.Text.Trim().ToString());
                        area.TargetPer = Convert.ToDecimal(txtTargetPer.Text.Trim().ToString());
                        area.Deadline = Convert.ToDateTime(txtDeadLine.Text.Trim().ToString());
                        area.IsActive = chkisactive.Checked;
                        area.SupervisorMark = 0;
                        area.MidYearStatus = " ";

                        functional.Add(area);
                    }

                }


                AppraisalMaster aMaster = new AppraisalMaster();

                aMaster.AppraisalMasterId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);
                aMaster.EmpInfoId = Convert.ToInt32(id_Empid.Value);
                aMaster.FinancialYearId = int.Parse(Request.QueryString["financialYearId"]);


                bool result = false;
                if (functional.Count > 0)
                {
                    int pk = _appPartA.SaveAppraisalSelfMaster(aMaster, Session["UserId"].ToString());
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
    }


    private bool SaveAppraisalSelfB(int pk)
    {

        bool result = false;
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

            if (rbType.SelectedValue == "Others")
            {
                area.SkillInfo = SkillInfo.Text.Trim().ToString();
            }
            else
            {
                area.SkillInfo = ddlSkill.SelectedValue.Trim().ToString();
            }


            area.SkillType = rbType.SelectedValue.Trim().ToString();
            area.SupportingEmp = txtSupportingEmp.Text.Trim().ToString();

                area.Score = Convert.ToDecimal(string.IsNullOrEmpty(txtScore.Text.Trim()) ? "0" : txtScore.Text.Trim());

                area.SetScore = Convert.ToDecimal(ddlWeight.SelectedValue);
                aList.Add(area);

            


        }
        if (aList.Count > 0)
        {
            result = _appraisalPartBdal.SaveAppraisalSelfPartBSkillType(aList);
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
                LocationLabel.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
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
        dt.Columns.Add(new DataColumn("ObjectiveGoal", typeof(string)));
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
        dr["ObjectiveGoal"] = "";
       
        dr["SelfMark"] = "";
        dr["IsActive"] = "False";
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
        //if (gv_AppraisalFunc.Rows.Count > 0)
        //{
        //    for (int i = 0; i < gv_AppraisalFunc.Rows.Count; i++)
        //    {
        //        CheckBox chkIsActive = (CheckBox)gv_AppraisalFunc.Rows[i].FindControl("isActiveCheckBox");
        //        if (chkIsActive.Checked)
        //        {


        //            TextBox txtWeight = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtWeight");

        //            TextBox txtMark = (TextBox) gv_AppraisalFunc.Rows[i].FindControl("txtMark");


        //            if (txtWeight.Text == "")
        //            {
        //                weightTotal = weightTotal + 0;
        //            }
        //            else
        //            {
        //                weightTotal = weightTotal + Convert.ToDecimal(txtWeight.Text.ToString());
        //            }
        //            if (txtMark.Text == "")
        //            {
        //                markTotal = markTotal + 0;
        //            }
        //            else
        //            {
        //                markTotal = markTotal + Convert.ToDecimal(txtMark.Text.ToString());
        //            }


        //        }

        //    }

        //    Label tst = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalWeight");
        //    tst.Text = weightTotal.ToString();

        //    Label tst2 = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalMark");
        //    tst2.Text = markTotal.ToString();


           
          

        //}
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
            DropDownList ddlSkill = (DropDownList)gv_AppraisalPartB.Rows[i].FindControl("ddlSkill");
            TextBox txtSupportingInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SupportingEmp");
            RadioButtonList rbType = (RadioButtonList)gv_AppraisalPartB.Rows[i].FindControl("rbType");

            TextBox SkillInfo = (TextBox)gv_AppraisalPartB.Rows[i].FindControl("SkillInfo");

            if (rbType.SelectedValue == "Others")
            {
                if (SkillInfo.Text == "")
                {
                    isVAlid = false;
                    aShowMessage.ShowMessageBox("Behaviral Info Required ", this);
                    SkillInfo.Focus();
                    break;
                }
            }
            else
            {
                if (ddlSkill.SelectedValue == "")
                {
                    isVAlid = false;
                    aShowMessage.ShowMessageBox("Behaviral Info Required ", this);
                    ddlSkill.Focus();
                    break;
                }
            }

        }

        Label tst = (Label)gv_AppraisalFunc.FooterRow.FindControl("lblTotalWeight");

        decimal weightTotal = tst.Text == "" ? 0 : Convert.ToDecimal(tst.Text.ToString());
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

        Label tst2 = (Label)gv_AppraisalPartB.FooterRow.FindControl("lblTotalScore");

        decimal weightTotal2 = tst.Text == "" ? 0 : Convert.ToDecimal(tst2.Text.ToString());
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



            DropDownList ddlWeight = (DropDownList) gv_AppraisalPartB.Rows[i].FindControl("ddlWeight");




            if (ddlWeight.SelectedValue == "")
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
        Response.Redirect("BSCOKRAppraisalSelfList.aspx");
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

        if (Validation() == true)
        {

            //int empId = int.Parse(Request.QueryString["EmpInfoId"]);

            //int FinancialYearId = int.Parse(Request.QueryString["financialYearId"]);


            //DataTable dtEmp = _appPartA.CheckAlreadyExist(Convert.ToInt32(empId), FinancialYearId,
            //    Convert.ToInt32(HFCompanyId.Value));
            //if (dtEmp.Rows.Count > 0)
            //{
            //    aShowMessage.ShowMessageBox("Already Exist !!!", this);

            //}
            //else
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

                    if (tbKpi.Text != "" && txtTarget.Text != "" && txtWeight.Text != "")
                    {
                        AppraisalFunctionalArea area = new AppraisalFunctionalArea();

                        area.KpiInfo = tbKpi.Text.Trim().ToString();
                        area.KpiWeight = Convert.ToDecimal(txtWeight.Text.Trim().ToString());
                        area.KpiWeightPer = Convert.ToDecimal(txtWeightPer.Text.Trim().ToString());
                        area.Target = Convert.ToDecimal(txtTarget.Text.Trim().ToString());
                        area.TargetPer = Convert.ToDecimal(txtTargetPer.Text.Trim().ToString());
                        area.Deadline = Convert.ToDateTime(txtDeadLine.Text.Trim().ToString());
                        area.IsActive = chkisactive.Checked;
                        area.SupervisorMark = 0;
                        area.MidYearStatus = " ";

                        functional.Add(area);
                    }

                }


                AppraisalMaster aMaster = new AppraisalMaster();

                aMaster.AppraisalMasterId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);
                aMaster.EmpInfoId = Convert.ToInt32(id_Empid.Value);
                aMaster.FinancialYearId = int.Parse(Request.QueryString["financialYearId"]);


                bool result = false;
                if (functional.Count > 0)
                {
                    int pk = _appPartA.SaveAppraisalSelfMaster(aMaster, Session["UserId"].ToString());
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

                        if (tbKpi.Text != "" && txtTarget.Text != "" && txtWeight.Text != "")
                        {
                            AppraisalFunctionalArea area = new AppraisalFunctionalArea();

                            area.KpiInfo = tbKpi.Text.Trim().ToString();
                            area.KpiWeight = Convert.ToDecimal(txtWeight.Text.Trim().ToString());
                            area.KpiWeightPer = Convert.ToDecimal(txtWeightPer.Text.Trim().ToString());
                            area.Target = Convert.ToDecimal(txtTarget.Text.Trim().ToString());
                            area.TargetPer = Convert.ToDecimal(txtTargetPer.Text.Trim().ToString());
                            area.Deadline = Convert.ToDateTime(txtDeadLine.Text.Trim().ToString());
                            area.IsActive = chkisactive.Checked;
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
                        repId =
                            (int)dtempdata.Rows[0]["ReportingEmpId"];
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
                                int pk = _appPartA.SaveAppraisalSelfMaster(aMaster, Session["UserId"].ToString());
                                if (pk > 0)
                                {

                                    AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO();

                                    appLogDao.ActionStatus = "Drafted";
                                    appLogDao.ApproveDate = DateTime.Now;
                                    appLogDao.ApproveBy = Session["UserId"].ToString();
                                    appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                                    appLogDao.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                                    appLogDao.AppraisalSelfMasterId = Convert.ToInt32(pk);
                                    appLogDao.Comments = txt_Comments.Text;
                                    appLogDao.CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString());




                                    int idd = _appPartA.SaveEmpAppLog(appLogDao);


                                    AppraisalSelfAppLogDAO aMastera = new AppraisalSelfAppLogDAO();
                                    aMastera.AppraisalSelfMasterId
                                        = Convert.ToInt32(pk);
                                    aMastera.ActionStatus = "Verified";
                                    bool status = _appPartA.UpdateContractural(aMastera);
                                    AppraisalSelfAppLogDAO appLogDao1 = new AppraisalSelfAppLogDAO()
                                    {
                                        ActionStatus = "Verified",
                                        ApproveDate = DateTime.Now,
                                        ApproveBy = Session["UserId"].ToString(),
                                        PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                                        ForEmpInfoId = Convert.ToInt32(CheckFinalApproval.Rows[0]["EmpInfoId"].ToString()),
                                        AppraisalSelfMasterId = Convert.ToInt32(pk),
                                        Comments = txt_Comments.Text,
                                        CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                                    };
                                    int id = _appPartA.SaveEmpAppLog(appLogDao1);

                                    SenMailForApprved(appLogDao1.ForEmpInfoId, " KPI Approval ", @"  <br/> Dear Sir, <br/>
An Employee's KPI is waiting for your approval. <br/><br/>
please login for the details from the below link.<br/>    http://182.160.103.234:8088/
 <br/> Thank You.");


                                    result = _appPartA.SaveAppraialSelfFunctionalDetails(functional, pk);
                                    result = SaveAppraisalSelfB(pk);
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
                                int pk = _appPartA.SaveAppraisalSelfMaster(aMaster, Session["UserId"].ToString());
                                if (pk > 0)
                                {

                                    AppraisalSelfAppLogDAO appLogDao = new AppraisalSelfAppLogDAO();

                                    appLogDao.ActionStatus = "Drafted";
                                    appLogDao.ApproveDate = DateTime.Now;
                                    appLogDao.ApproveBy = Session["UserId"].ToString();
                                    appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                                    appLogDao.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                                    appLogDao.AppraisalSelfMasterId = Convert.ToInt32(pk);
                                    appLogDao.Comments = txt_Comments.Text;
                                    appLogDao.CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString());




                                    int idd = _appPartA.SaveEmpAppLog(appLogDao);


                                    AppraisalSelfAppLogDAO aMastera = new AppraisalSelfAppLogDAO();
                                    aMastera.AppraisalSelfMasterId
                                        = Convert.ToInt32(pk);
                                    aMastera.ActionStatus = "Verified";
                                    bool status = _appPartA.UpdateContractural(aMastera);
                                    AppraisalSelfAppLogDAO appLogDao1 = new AppraisalSelfAppLogDAO()
                                    {
                                        ActionStatus = "Verified",
                                        ApproveDate = DateTime.Now,
                                        ApproveBy = Session["UserId"].ToString(),
                                        PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                                        ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString()),
                                        AppraisalSelfMasterId = Convert.ToInt32(pk),
                                        Comments = txt_Comments.Text,
                                        CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                                    };
                                    int id = _appPartA.SaveEmpAppLog(appLogDao1);

                                    SenMailForApprved(appLogDao1.ForEmpInfoId, " KPI Approval ", @"  <br/> Dear Sir, <br/>
An Employee's KPI is waiting for your approval. <br/><br/>
please login for the details from the below link.<br/>    http://182.160.103.234:8088/
 <br/> Thank You.");


                                    result = _appPartA.SaveAppraialSelfFunctionalDetails(functional, pk);
                                    result = SaveAppraisalSelfB(pk);
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
                    AlertMessageBoxShow("Your Suppervisor or Final Approver  has not been  set yet. Please contact with HR Department !!!");

                }
            }
        }
        catch (Exception)
        {
            AlertMessageBoxShow("Operation Failed");
         //   throw;
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

    protected void btn_AddNested_Click1(object sender, EventArgs e)
    {

    }

    protected void btn_AddrowNested_Click(object sender, EventArgs e)
    {
        
        GridViewRow Gv2Row = (GridViewRow)((LinkButton)sender).NamingContainer;
        GridView Childgrid = (GridView)(Gv2Row.Parent.Parent);
        GridViewRow Gv1Row = (GridViewRow)(Childgrid.NamingContainer);
        int masterrowindex = Gv1Row.RowIndex;

        //int rowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;
        GridView gvNestedTable = ((GridView)gv_AppraisalFunc.Rows[masterrowindex].FindControl("gvNestedTable"));
        DataTable aTableInitial = new DataTable();
      
        aTableInitial.Columns.Add("KPI");
        aTableInitial.Columns.Add("Weightage");
        aTableInitial.Columns.Add("Initiatives");
        aTableInitial.Columns.Add("Deadline");

        DataRow aRow = null;

        for (int i = 0; i < gvNestedTable.Rows.Count - 1; i++)
        {
             
            aRow = aTableInitial.NewRow();
            aRow["KPI"] = ((TextBox)gvNestedTable.Rows[i].FindControl("txtKPI")).Text;
            aRow["Weightage"] = ((TextBox)gvNestedTable.Rows[i].FindControl("txtWeightage")).Text;
       
            aRow["Initiatives"] = ((TextBox)gvNestedTable.Rows[i].FindControl("txtInitiatives")).Text;
            aRow["Deadline"] = ((TextBox)gvNestedTable.Rows[i].FindControl("txtDeadline")).Text;
         
            aTableInitial.Rows.Add(aRow);
        }
        aRow = aTableInitial.NewRow();
        aRow["KPI"] = "";
        aRow["Weightage"] = "";
        aRow["Initiatives"] = "";
        aRow["Deadline"] = "";
        aTableInitial.Rows.Add(aRow);
        aRow = aTableInitial.NewRow();
        aRow["KPI"] = ((TextBox)gvNestedTable.Rows[gvNestedTable.Rows.Count - 1].FindControl("txtKPI")).Text;
        aRow["Weightage"] = ((TextBox)gvNestedTable.Rows[gvNestedTable.Rows.Count - 1].FindControl("txtWeightage")).Text;
        aRow["Initiatives"] = ((TextBox)gvNestedTable.Rows[gvNestedTable.Rows.Count - 1].FindControl("txtInitiatives")).Text;
        aRow["Deadline"] = ((TextBox)gvNestedTable.Rows[gvNestedTable.Rows.Count - 1].FindControl("txtDeadline")).Text;
         
        aTableInitial.Rows.Add(aRow);
        //aRow = aTableInitial.NewRow();
        //aRow["CostCenter"] = "Total";
        //aRow["CostCenterId"] = "";
        //aRow["Amount"] = "";
        //aRow["AmountBDT"] = "";
        //aTableInitial.Rows.Add(aRow);
        gvNestedTable.DataSource = aTableInitial;
        gvNestedTable.DataBind();
        

      
    }
}