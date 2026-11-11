using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.ViewerObjectModel;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.HealthCare_DAL;
using DAO.HealthCare_Dao;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class HealthCare_UI_CommitteViewPanel : System.Web.UI.Page
{
    private HealthCareListDal aListDal = new HealthCareListDal();
    private TopSheetDal aSheetDal = new TopSheetDal();
    private ShowMessage aMessage = new ShowMessage();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private HRPanelDal aPanelDal = new HRPanelDal();

    private KPIInformationViewDAL _aFincDal = new KPIInformationViewDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             Load_Initail_DD_List();

          //  GET_List();
            // Time.Text = DateTime.Now.ToString("hh:mm:ss");
           // txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
        }
    }


    private void Load_Initail_DD_List()
    {
        // aListDal.GetDDLCompany(ddlCompany);
        //  ddlCompany.SelectedValue = 1.ToString();
        using (DataTable dt = _commonDataLoad.GetCompany())
        {

            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
            ddlCompany.SelectedIndex = 1;
            ddlCompany_OnSelectedIndexChanged(null, null);
        }
   
        using (DataTable dt = _commonDataLoad.GetDDLSalaryLocation())
        {
            //ddlSalaryLocation.DataSource = dt;
            //ddlSalaryLocation.DataValueField = "Value";
            //ddlSalaryLocation.DataTextField = "TextField";
            //ddlSalaryLocation.DataBind();
        }
    }


    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {

       // Session["cid"] = ddlCompany.SelectedValue;
        DataTable dt = _aFincDal.GetFianncialYearByComIdDDl(Convert.ToInt32(ddlCompany.SelectedValue));
        ddlFinancialYear.DataSource = dt;
        ddlFinancialYear.DataValueField = "Value";
        ddlFinancialYear.DataTextField = "TextField";
        ddlFinancialYear.DataBind();


    }

    protected void gv_JdBoard_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "A")
        {

        }
    }

    //private string Generateparam()
    //{
    //    //string param = "";

    //    //if (ddlCompany.SelectedValue != "")
    //    //{
    //    //    param = param + "AND M.CompanyId = '" + ddlCompany.SelectedValue + "' ";
    //    //}

    //    //return param;
    //}

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)gv_JdBoard.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_JdBoard.Rows[i].Cells[0].FindControl("ckApproved");
            chkBoxRows.Checked = chkBoxHeader.Checked;
        }
    }



    private string GenerateParam()
    {
        string param = "";

        if (ddlCompany.SelectedValue != "")
        {
            param = param + "AND M.CompanyId=" + ddlCompany.SelectedValue;
        }

        //if (ddlSalaryLocation.SelectedValue != "-1")
        //{
        //    param = param + "AND sal.SalaryInfoId=" + ddlSalaryLocation.SelectedValue;
        //}

        return param;
    }


    private string ParamGenerate()
    {
        string param = "";

        if (ddlCompany.SelectedValue != "")
        {
            param = param + " AND M.CompanyId="+ddlCompany.SelectedValue;
        }

        if (ddlFinancialYear.SelectedIndex> 0)
        {
            param = param + " AND m.FinancialYearId=" + ddlFinancialYear.SelectedValue;
        }

        if (ddlType.SelectedValue != "")
        {
            param = param + " and  M.Type='"+ddlType.SelectedValue+"'";
        }

       

        //if (MeetFromDate.Text.Trim() != "" && MeetingTodate.Text.Trim() == "")
        //{
        //    param = param + " AND M.MeetingDate BETWEEN '" + MeetFromDate.Text + "' AND '" + DateTime.Now + "' ";
        //}

        if (MeetFromDate.Text.Trim() != "" && MeetingTodate.Text.Trim() != "")
        {
            param = param + " AND M.SubmitDate BETWEEN '" + MeetFromDate.Text + "' AND '" + MeetingTodate.Text + "' ";
        }

      
        return param;
    }



    private void GET_List()
    {
        gv_JdBoard.DataSource = null;
        gv_JdBoard.DataBind();
        DataTable Dt = aListDal.Get_All_Employee_List_CommitteFeadBackPanel(ParamGenerate(), Session["EmpInfoId"].ToString(), ddlStatus.SelectedValue);

        if (Dt.Rows.Count > 0)
        {

            //for (int i = 0; i < Dt.Rows.Count; i++)
            //{

            //    string hfSalaryLoationId = Dt.Rows[i]["SalaryLoationId"].ToString();
            //    string hfApplicationType = Dt.Rows[i]["Type"].ToString();

            //    DataTable DtCheck = aPanelDal.Get_HRPanelCheckCommeti(hfApplicationType, hfSalaryLoationId);
            //    if (DtCheck.Rows.Count == 0)
            //    {
            //        Dt.Rows.RemoveAt(i);
            //        i--;
            //    }
            //}


            gv_JdBoard.DataSource = Dt;
            gv_JdBoard.DataBind();
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

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
        //if (ddlStatus.SelectedValue != "")
        //{
        //    GET_List();
        //}
        //else
        //{
        //    aMessage.ShowMessageBox("Please select status",this);
        //}

        GET_List();
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../HealthCare_UI/TopSheetGenerateView.aspx");
    }


    #region Save_Operation

    private bool Validation()
    {
      
        int count = 0;

        for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
        {
            CheckBox checkBox = (CheckBox)gv_JdBoard.Rows[i].FindControl("ckApproved");

            if (checkBox.Checked)
            {
                count++;
            }
        }

        if (count == 0)
        {
            aMessage.ShowMessageBox("Please Select At least One", this);
            return false;
        }

        return true;
    }

    protected void btn_save_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            int Id = 0;

            Id = aSheetDal.Save_TopSheet(MasterDataPrepareForSave(), DetailsDataPrepareForSave(), SelfLogList());

            if (Id > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfully Done...');window.location ='TopSheetGenerateView.aspx';",
                    true);
            }
        }

    }

    private TopSheetGeneMasterDao MasterDataPrepareForSave()
    {
        TopSheetGeneMasterDao aMasterDao = new TopSheetGeneMasterDao();
        aMasterDao.TopsheetGeneMasId = 0;
      
        aMasterDao.Isactive = true;
        return aMasterDao;
    }

    private List<TopSheetGenerateDetailsDao> DetailsDataPrepareForSave()
    {
        List<TopSheetGenerateDetailsDao> aList = new List<TopSheetGenerateDetailsDao>();
        for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
        {
            var aInfo = new TopSheetGenerateDetailsDao();
            TopSheetGenerateDetailsDao aDao = new TopSheetGenerateDetailsDao();
            HiddenField Forward = (HiddenField)gv_JdBoard.Rows[i].FindControl("hfeimbursFromMasterId");
            CheckBox check = (CheckBox)gv_JdBoard.Rows[i].FindControl("ckApproved");
            if (check.Checked)
            {
                aInfo.ReimbursFromMasterId = int.Parse(Forward.Value);
                aList.Add(aInfo);
            }
        }
        return aList;
    }

    private List<ReimbursementSelfAppLogDAO> SelfLogList()
    {
        List<ReimbursementSelfAppLogDAO> aList = new List<ReimbursementSelfAppLogDAO>();

        for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
        {
            var aInfo = new ReimbursementSelfAppLogDAO();
            HiddenField Forward = (HiddenField)gv_JdBoard.Rows[i].FindControl("hfeimbursFromMasterId");
            CheckBox check = (CheckBox)gv_JdBoard.Rows[i].FindControl("ckApproved");
            if (check.Checked)
            {
                HiddenField company = (HiddenField)gv_JdBoard.Rows[i].FindControl("hfCompanyId");
                HiddenField type = (HiddenField)gv_JdBoard.Rows[i].FindControl("HFApplicationType");
                HiddenField SalaryLocaion = (HiddenField)gv_JdBoard.Rows[i].FindControl("HFSalaryLocaion");
                DataTable Dt = aSheetDal.Get_Nominated_Committee(type.Value, SalaryLocaion.Value, company.Value);
                if (Dt.Rows.Count > 0)
                {
                    aInfo.ReimbursFromMasterId = int.Parse(Forward.Value);
                    aInfo.ActionStatus = "Verified";
                    aInfo.ApproveDate = DateTime.Now;
                    aInfo.ApproveBy = Session["UserId"].ToString();
                    aInfo.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                    aInfo.ForEmpInfoId = Convert.ToInt32(Dt.Rows[0]["EmpInfoId"].ToString());
                    aInfo.Comments = "";
                    aInfo.CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString());
                    aList.Add(aInfo);
                }
            }
        }

        return aList;
    }
    #endregion


    protected void ckApproved_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox lb = (CheckBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        HiddenField MasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfeimbursFromMasterId");
        DataTable Dt = aSheetDal.Get_FeedBack(MasterId.Value);

        if (Dt.Rows.Count == 0)
        {
            CheckBox Check = (CheckBox)gv_JdBoard.Rows[rowID].FindControl("ckApproved");
            Check.Checked = false;
            aMessage.ShowMessageBox("There is no committee feedback for this Application", this);

        }

    }

    protected void txtMeetingNo_OnTextChanged(object sender, EventArgs e)
    {
       
    }

    protected void btn_view_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        Session["dsdadf"] = "";
        Session["dsdadf"] = "View";
        HiddenField MasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfeimbursFromMasterId");
        Response.Redirect("ApplicationView_CommitteePanel.aspx?MID=" + MasterId.Value.Trim());
    }
}