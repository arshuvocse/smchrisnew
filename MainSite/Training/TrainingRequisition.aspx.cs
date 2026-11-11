using DAL.COMMON_DAL;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Training_TrainingRequisition : System.Web.UI.Page
{

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private int mid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
            if (!IsPostBack)
            {
            Session["EmpOption"] = "Employee";
            LoadInitialDDL();

            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();

                if (mid > 0)
                {

                    DataTable dt = _trainingDal.GetTrainingRequisitionMaster(mid);
                    ddlCompany.SelectedValue = dt.Rows[0]["CompanyId"].ToString();

                    ddlCompany_SelectedIndexChanged(ddlCompany, (EventArgs)e);
                    ddlFinancialYear.SelectedValue = dt.Rows[0]["FinancialYearId"].ToString();
                    txt_trainingTopic.Text = dt.Rows[0]["TrainingTitle"].ToString();
                    ddlFinancialYear_SelectedIndexChanged(ddlFinancialYear, (EventArgs)e);
                    ddlQuater.SelectedValue = dt.Rows[0]["Quater"].ToString();
                    DataTable dt2 = _trainingDal.GetTrainingRequisitionDetails(mid);

                    ViewState["EmpDetails"] = dt2;
                    gv_EmpDetails.DataSource = dt2;
                    gv_EmpDetails.DataBind();

                }
            }
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["cid"] = ddlCompany.SelectedValue;
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
        Session["fid"] = ddlFinancialYear.SelectedValue;
        LoadQuater(Convert.ToInt32(ddlFinancialYear.SelectedValue));
    }
    protected void LoadQuater(int id)
    {
        DataTable dt = _trainingDal.GetQuaterNew(Convert.ToInt32(ddlCompany.SelectedValue));
        //YearQuater aQuaterSelect = new YearQuater { QuarterDetails = "Select Quater", QuarterNum = "-1" };
        //quaters.Insert(0, aQuaterSelect);
        ddlQuater.DataSource = dt;
        ddlQuater.DataValueField = "Value";
        ddlQuater.DataTextField = "TextField";
        ddlQuater.DataBind();
    }
    public List<YearQuater> GetAllQuarters(DateTime StartDate, DateTime EndDate)
    {

        return _trainingDal.GetQuater(StartDate, EndDate);

    }

    protected void AddEmpDetailsToGrid()
    {
        if (ViewState["EmpDetails"] == null)
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            string [] empStr = txt_employee.Text.Trim().Split(':');
            string employee = txt_employee.Text.Trim().Substring(0, 6);

            int empId = _trainingDal.GetEmployeeIdByCode(empStr[0]);


            dt.Columns.Add(new DataColumn("quater", typeof(string)));
           

            dt.Columns.Add(new DataColumn("employee", typeof(string)));
            dt.Columns.Add(new DataColumn("employeeId", typeof(string)));
           
            dr = dt.NewRow();
            string a = ddlQuater.SelectedValue.ToString();
            dr["quater"] = ddlQuater.SelectedValue.ToString();
            
            dr["employee"] = txt_employee.Text.Trim();
            dr["employeeId"] = empId.ToString();
            

            dt.Rows.Add(dr);
            ViewState["EmpDetails"] = dt;
            gv_EmpDetails.DataSource = dt;
            gv_EmpDetails.DataBind();
        }

        else
        {
            if (EmployeeExistValidation(_trainingDal.GetEmployeeIdByCode(txt_employee.Text.Trim().Substring(0, 6))) == false)
            {
                aShowMessage.ShowMessageBox("Employee  Already Exists in  List", this);
                txt_employee.Text = "";
                return;
            }

            DataTable dtCurrentTable = (DataTable)ViewState["EmpDetails"];

            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["quater"] = ddlQuater.SelectedValue.ToString();
              
                drCurrentRow["employee"] = txt_employee.Text.Trim();

                drCurrentRow["employeeId"] = _trainingDal.GetEmployeeIdByCode(txt_employee.Text.Trim().Substring(0, 6)); ;
                



                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["EmpDetails"] = dtCurrentTable;

                gv_EmpDetails.DataSource = dtCurrentTable;
                gv_EmpDetails.DataBind();


            }
        }
        txt_employee.Text = "";
    }

    private bool EmployeeExistValidation(int empId)
    {
        bool result = true;
        for (int i = 0; i < gv_EmpDetails.Rows.Count; i++)
        {
            HiddenField empInfoId = (HiddenField)gv_EmpDetails.Rows[i].FindControl("deptId");

            int id = Convert.ToInt32(empInfoId.Value.ToString());
            if (id == empId)
            {

                result = false;
                break;
            }


        }
        return result;
    }
   
    protected void addEmployeeToList_Click(object sender, EventArgs e)
    {
        AddEmpDetailsToGrid();
    }
    protected void lb_RemoveEmp_Click(object sender, EventArgs e)
    {
        if (ViewState["EmpDetails"] != null)
        {

            LinkButton lb = (LinkButton)sender;
            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            DataTable dt = (DataTable)ViewState["EmpDetails"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count == 0)
            {
                ViewState["EmpDetails"] = null;
            }
            else
            {
                ViewState["EmpDetails"] = dt;
            }


            gv_EmpDetails.DataSource = dt;
            gv_EmpDetails.DataBind();
           
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (gv_EmpDetails.Rows.Count > 0)
        {

            TrainingRequisitionMaster aMaster = new TrainingRequisitionMaster();

            aMaster.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            aMaster.FinancialYearId = Convert.ToInt32(ddlFinancialYear.SelectedValue);
            aMaster.Quater = ddlQuater.SelectedValue;
            aMaster.TrainingTitle = txt_trainingTopic.Text.Trim().ToString();
            aMaster.ReqBy =_trainingDal.GetEmployeeIdByCode(txt_Reqemployee.Text.Trim().ToString().Substring(0, 6));
            aMaster.TrainingRequisitionMasterId = hdpk.Value == "" ? 0 : Convert.ToInt32(hdpk.Value);
            if (aMaster.TrainingRequisitionMasterId == 0)
            {
                aMaster.EntryBy = Session["LoginName"].ToString();
                aMaster.EntryDate = System.DateTime.Now;
            }
            else
            {
                aMaster.UpdateBy = Session["LoginName"].ToString();
                aMaster.UpdateDate = System.DateTime.Now;
            }
           

            int result = _trainingDal.SaveTrainingReqMaster(aMaster, Session["LoginName"].ToString());


            List<TrainingRequisitionDetails> aList = new List<TrainingRequisitionDetails>();

            for (int i = 0; i < gv_EmpDetails.Rows.Count; i++)
            {

                 TrainingRequisitionDetails aEmp = new TrainingRequisitionDetails();
                 HiddenField deptId = (HiddenField)gv_EmpDetails.Rows[i].FindControl("deptId");
                 aEmp.EmpInfoId = Convert.ToInt32(deptId.Value);
                 aList.Add(aEmp);
            }

            bool save = _trainingDal.SaveTrainingReqDetails(aList, result);

            if (save == true)
            {
                AlertMessageBoxShow("Operation Successful...");
                Response.Redirect("TrainingReqList.aspx");
            }
            else
            {
                AlertMessageBoxShow("Operation Failed...");
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
    protected void cancelButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("TrainingRequisition.aspx");
    }
    protected void detailsViewButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("TrainingList.aspx");
    }
}