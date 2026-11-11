using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Training_TrainingRequisition2 : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            LoadInitialDDL();

        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt2 = null;
        ViewState["TrainingDetails"] = null;
        gv_training.DataSource = dt2;
        gv_training.DataBind();
        Session["CompanyId"] = ddlCompany.SelectedValue;
        DataTable dt = _trainingDal.GetFianncialYearByComIdDDl(Convert.ToInt32(ddlCompany.SelectedValue));
        ddlFinancialYear.DataSource = dt;
        ddlFinancialYear.DataValueField = "Value";
        ddlFinancialYear.DataTextField = "TextField";
        ddlFinancialYear.DataBind();
    }

    private void LoadInitialDDL()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {

            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
        }
    }

    protected void ddlFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataTable dt = null;
        ViewState["TrainingDetails"] = null;
        gv_training.DataSource = dt;
        gv_training.DataBind();
        Session["fid"] = ddlFinancialYear.SelectedValue;
        LoadQuater(Convert.ToInt32(ddlFinancialYear.SelectedValue));

        

    }
    protected void LoadQuater(int id)
    {
        FinancialYear ayear = _trainingDal.GetFinancialYear(id);

        DataTable dt = _trainingDal.GetQuaterNew(Convert.ToInt32(ddlCompany.SelectedValue));
        
        ddlQuater.DataSource = dt;
        ddlQuater.DataValueField = "Value";
        ddlQuater.DataTextField = "TextField";
        ddlQuater.DataBind();
    }

    public List<YearQuater> GetAllQuarters(DateTime StartDate, DateTime EndDate)
    {

        return _trainingDal.GetQuater(StartDate, EndDate);

    }

    protected void lb_RemoveEmp_OnClick(object sender, EventArgs e)
    {
       
       
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (SaveValidation() == true)
        {

            TrainingRequisition2Master aMaster = new TrainingRequisition2Master();

            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue.ToString());
            aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue.ToString());
            string empid = txt_Reqemployee.Text.Trim();
            if (empid.Contains(":"))
            {
                string[] strsp = empid.Split(':');
                int empId = _trainingDal.GetEmployeeIdByCode(strsp[0]);
                aMaster.RequisitionBy = empId;
            }

            int ok = _trainingDal.SaveTrainingRequisition2Master(aMaster, Session["LoginName"].ToString());

            if (ok > 0)
            {
                List<TrainingRequisition2Details> aList = new List<TrainingRequisition2Details>();
                for (int i = 0; i < gv_training.Rows.Count; i++)
                {

                    Label TrainingTitle = (Label)gv_training.Rows[i].FindControl("TrainingTitle");
                    Label ExpectedResult = (Label)gv_training.Rows[i].FindControl("ExpectedResult");
                    Label QuaterId = (Label)gv_training.Rows[i].FindControl("QuaterId");
                    Label MonthId = (Label)gv_training.Rows[i].FindControl("MonthId");

                    TrainingRequisition2Details aDetails = new TrainingRequisition2Details();
                    aDetails.TrainingRequisition2Id = ok;
                    aDetails.TrainingTitle = TrainingTitle.Text.ToString();
                    aDetails.ExpectedResult = ExpectedResult.Text.ToString();
                    aDetails.QuaterId = Convert.ToInt32(QuaterId.Text.ToString());
                    aDetails.MonthId = Convert.ToInt32(MonthId.Text.ToString());

                    aList.Add(aDetails);
                }
                bool result = false;
                if (aList.Count > 0)
                {
                    result = _trainingDal.SaveTrainingReq2Details(aList, ok);
                }

                if (result == true)
                {
                    AlertMessageBoxShow("Operation Successful...");
                    Response.Redirect("TrainingRequisition2.aspx");
                }
                else
                {
                    AlertMessageBoxShow("Operation Failed...");
                }
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
    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        
    }


    protected void addList_OnClick(object sender, EventArgs e)
    {
        if (AddValidation() == true)
        {
            if (ViewState["TrainingDetails"] == null)
            {
                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("TrainingTitle", typeof (string)));
                dt.Columns.Add(new DataColumn("ExpectedResult", typeof (string)));
                dt.Columns.Add(new DataColumn("QuaterId", typeof (string)));
                dt.Columns.Add(new DataColumn("Quater", typeof (string)));
                dt.Columns.Add(new DataColumn("MonthId", typeof (string)));
                dt.Columns.Add(new DataColumn("Month", typeof (string)));


                DataRow dr = null;
                dr = dt.NewRow();

                dr["TrainingTitle"] = txt_trainingTopic.Text.ToString();
                dr["ExpectedResult"] = txt_result.Text.ToString();
                dr["QuaterId"] = ddlQuater.SelectedValue;
                dr["Quater"] = ddlQuater.SelectedItem.Text;
                dr["MonthId"] = ddlMonth.SelectedValue;
                dr["Month"] = ddlMonth.SelectedItem.Text;
                dt.Rows.Add(dr);
                ViewState["TrainingDetails"] = dt;
                gv_training.DataSource = dt;
                gv_training.DataBind();
                ClearEntry();
            }
            else
            {
                DataTable dtCurrentTable = (DataTable)ViewState["TrainingDetails"];
                DataRow drCurrentRow = null;

                if (dtCurrentTable.Rows.Count > 0)
                {
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["TrainingTitle"] = txt_trainingTopic.Text.ToString();
                    drCurrentRow["ExpectedResult"] = txt_result.Text.ToString();
                    drCurrentRow["QuaterId"] = ddlQuater.SelectedValue;
                    drCurrentRow["Quater"] = ddlQuater.SelectedItem.Text;
                    drCurrentRow["MonthId"] = ddlMonth.SelectedValue;
                    drCurrentRow["Month"] = ddlMonth.SelectedItem.Text;
                                                   
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["TrainingDetails"] = dtCurrentTable;
                    gv_training.DataSource = dtCurrentTable;
                    gv_training.DataBind();
                    ClearEntry();
                }
            }
        }
    }
    private void ClearEntry()
    {
        txt_trainingTopic.Text = "";
        txt_result.Text = "";


       

    }
    private bool AddValidation()
    {
        bool isValid = true;
        if (txt_trainingTopic.Text.ToString() == "")
        {

            isValid = false;
            aShowMessage.ShowMessageBox("Training title Required", this);

        }

        if (txt_result.Text.ToString() == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Expeced Result  Required", this);
        }
        if (ddlQuater.SelectedValue == "" || ddlQuater.SelectedValue == "-1")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Quater Required", this);
        }
        if (ddlMonth.SelectedValue == "" || ddlMonth.SelectedValue == "-1")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Month Required", this);
        }
        return isValid; 
    }

    protected void ddlQuater_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string aValue = ddlQuater.SelectedValue.ToString();
        DataTable dt = _trainingDal.GetMonthByQuater(Convert.ToInt32(aValue));
        ddlMonth.DataSource = dt;
        ddlMonth.DataTextField = "TextField";
        ddlMonth.DataValueField = "Value";
        ddlMonth.DataBind();
    }

    private bool  SaveValidation()
    {
        bool isValid = true;

        if (ddlCompany.SelectedValue == "" || ddlCompany.SelectedValue =="-1" )
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Company  Required", this);
        }
        if (ddlFinancialYear.SelectedValue == "" || ddlFinancialYear.SelectedValue == "-1")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Financial year  Required", this);
        }
        if (txt_Reqemployee.Text == "")
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Requisition by Required", this);
        }

        if (gv_training.Rows.Count == 0)
        {
            isValid = false;
            aShowMessage.ShowMessageBox("Training Details   Required", this);
        }

        return isValid;
    }
}