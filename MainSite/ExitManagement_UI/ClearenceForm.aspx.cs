using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.Survey;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using DAO.MeetingMinorsDAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Survey_ClearenceForm : System.Web.UI.Page
{
    ClearenceFormDal aExitDal = new ClearenceFormDal();

    private int EMPID = 0;

    private int ExitDetailIdq = 0;
    private int ExitMasterId = 0;

    private int GetCurrentEmpInfoId()
    {
        int empLatId = 0;
        if (Session["EmpInfoIdLat"] != null && int.TryParse(Session["EmpInfoIdLat"].ToString(), out empLatId))
        {
            return empLatId;
        }
        int empIdSess = 0;
        if (Session["EmpInfoId"] != null && int.TryParse(Session["EmpInfoId"].ToString(), out empIdSess))
        {
            return empIdSess;
        }
        return 0;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (!string.IsNullOrEmpty(Request.QueryString["EMPID"]))
                {
                    EMPID = int.Parse(Request.QueryString["EMPID"]);
                    ExitMasterId = int.Parse(Request.QueryString["ExitMasterId"]);
                    ExitDetailIdq = int.Parse(Request.QueryString["ExitDetailId"]);
                    hfExitMasterId.Value = ExitMasterId.ToString(CultureInfo.InvariantCulture);
                    hfExitDetailId.Value = ExitDetailIdq.ToString(CultureInfo.InvariantCulture);

                    string ApprovalStatus = (Request.QueryString["ApprovalStatus"].Trim());

                    if (ApprovalStatus == "as Supervisor")
                    {
                        showReccomend.Visible = true;

                    }


                    LoadDataEmp(EMPID.ToString());
                    //hfDivision.Value == "18"
                    if (hfDivision.Value == "48")
                    {
                        Emp_Clea.Visible = true;
                        Clearance(EMPID);
                    }

                    ForwardValidatn(ExitMasterId, ExitDetailIdq, GetCurrentEmpInfoId());


                    DataTable dtShowRemarks = aExitDal.GetShowRemarksById(ExitDetailIdq.ToString());
                    if (dtShowRemarks.Rows.Count > 0)
                    {

                        hfSetInfo.Value = dtShowRemarks.Rows[0]["SetInfo"].ToString().Trim();
                        lblRemarks.Text = dtShowRemarks.Rows[0]["ForwardRemarks"].ToString().Trim();
                        txtForwardRemarks.Text = dtShowRemarks.Rows[0]["ForwardRemarks"].ToString().Trim();

                    }

                    if (hfSetInfo.Value == "Dep")
                    {
                        LoadEmpListByDepartment();
                    }
                    else
                    {
                        LoadEmpListByDivision();
                    }

                    DataTable dtDoc = aExitDal.GetDocDataById(ExitMasterId.ToString());
                    if (dtDoc.Rows.Count > 0)
                    {
                        ViewState["DocGrid_List"] = dtDoc;
                        gv_DocumentUpload.DataSource = dtDoc;
                        gv_DocumentUpload.DataBind();
                    }



                    DataTable dtDocNew = aExitDal.GetDocNewDataById(EMPID.ToString());
                    if (dtDocNew.Rows.Count > 0)
                    {
                        ViewState["gvDocGrid_List"] = dtDocNew;
                        gv_Doc.DataSource = dtDocNew;
                        gv_Doc.DataBind();
                    }
                    try
                    {
                        LoadResourceGrid();

                    }
                    catch (Exception)
                    {

                        //throw;
                    }

                }

                LoadDropDownList();
            }
            catch
            {
                ShowMessageBox("Division Not Found!");
            }
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
    private void Clearance(int id)
    {
        DataTable dt = aExitDal.Get_EmpClearance(id);

        if (dt.Rows.Count > 0)
        {


            MoIssueDate.Text = dt.Rows[0]["MobIssuDate"].ToString();
            MoActualPrice.Text = dt.Rows[0]["MobActualPrice"].ToString();
            MoDABeforOne.Text = dt.Rows[0]["MoDABeforeOne"].ToString();
            MoDABeforeOneTwo.Text = dt.Rows[0]["MoDABeforeOnetotwo"].ToString();
            MoAboveTwo.Text = dt.Rows[0]["MoDAAboveTwo"].ToString();
            MoRemark.Text = dt.Rows[0]["MoRemarks"].ToString();

            DBissueDate.Text = dt.Rows[0]["DBIssuDate"].ToString();
            DBActualPrice.Text = dt.Rows[0]["DBActualPrice"].ToString();
            DBDeductionAmount.Text = dt.Rows[0]["DBDeductionAmount"].ToString();
            DBRemark.Text = dt.Rows[0]["DBRemark"].ToString();

            PIActualCost.Text = dt.Rows[0]["PIActualCost"].ToString();
            PIDeductionAmount.Text = dt.Rows[0]["PIDeductionAmount"].ToString();
            PIRemarks.Text = dt.Rows[0]["PIRemark"].ToString();

            IDCard.Text = dt.Rows[0]["IDCard"].ToString();
            IDCardRemark.Text = dt.Rows[0]["IDCardRemark"].ToString();

            MarketDues.Text = dt.Rows[0]["MarketDues"].ToString();
            MarketDuesRemark.Text = dt.Rows[0]["MarketRemarks"].ToString();

            TotalDeductionAmount.Text = dt.Rows[0]["TotalDeductionAmount"].ToString();
            TotalDeductionAmountRemark.Text = dt.Rows[0]["ToDeducAmtRemark"].ToString();


            if (dt.Rows[0]["TickMark"].ToString() ==
                "The Employee Does not have any dues, Retured all company assets provided to him/her.")
            {
                checkboxlist1.SelectedValue = "Nodues";
            }
            else  if (dt.Rows[0]["TickMark"].ToString() ==
                "The Employee has dues, Did not return following company assets provided to him/her.")
            {
                checkboxlist1.SelectedValue = "dues";
            }

            


        }
    }
    public void LoadResourceGrid()
    {

        DataTable dtata= new DataTable();
        if (hfSetInfo.Value == "Dep")
        {
              dtata = aExitDal.ResourceDataForDepartment(int.Parse(Request.QueryString["EMPID"]), Convert.ToInt32(Session["DepartmentId"].ToString()), Request.QueryString["ApprovalStatus"].ToString(), GetExitMasterId(), GetExitDetailId());
        }
        else
        {
            dtata = aExitDal.ResourceDataForDepartment(int.Parse(Request.QueryString["EMPID"]), Convert.ToInt32(Session["DivisionId"].ToString()), Request.QueryString["ApprovalStatus"].ToString(), GetExitMasterId(), GetExitDetailId());
        }

        
        if (dtata.Rows.Count > 0)
        {
            itemsDetailGridView.DataSource = dtata;
            itemsDetailGridView.DataBind();


            txtRemarrks.Text = dtata.Rows[0]["MainRemarks"].ToString().Trim();

            try
            {
                if (Convert.ToBoolean(dtata.Rows[0]["Recommend"].ToString().Trim()) == true)
                {
                    rbRecommend.Items[0].Selected = true;

                }
                else
                {
                    rbRecommend.Items[1].Selected = true;

                }
            }
            catch (Exception)
            {
                rbRecommend.Items[1].Selected = true;
                
                
            }



        }
        else
        {
            LoadInitialGrid();
        }
    }

    public void ForwardValidatn(int masterId, int exitDetailId, int id2)
    {
        DataTable dtata = aExitDal.CheckClearence(masterId, exitDetailId, id2);
        if (dtata.Rows.Count>0)
        {
            int originalEmpId = Convert.ToInt32(dtata.Rows[0]["EmpInfoId"]);
            int approvalEmpId = Convert.ToInt32(dtata.Rows[0]["EmpInfoIdApproval"]);
            int isForwardEmpId = (dtata.Rows[0]["IsForwardEmpId"] != DBNull.Value && !string.IsNullOrEmpty(dtata.Rows[0]["IsForwardEmpId"].ToString()))
                ? Convert.ToInt32(dtata.Rows[0]["IsForwardEmpId"])
                : 0;

            bool isOriginalApprover = (originalEmpId == id2);
            bool isAutoForwardRecipient = aExitDal.IsClearanceAutoForwardRecipient(originalEmpId, id2);
            bool isManualForward = (originalEmpId != approvalEmpId) || (isForwardEmpId > 0 && isForwardEmpId == id2 && isForwardEmpId != originalEmpId);
            bool isFwdBackDone = (dtata.Rows[0]["isForwardBackDone"] != DBNull.Value && Convert.ToBoolean(dtata.Rows[0]["isForwardBackDone"]));

            if (!isOriginalApprover && (isAutoForwardRecipient || isManualForward))
            {
                forward.Visible = false;
                Button1.Visible = true;
                btn_Save.Visible = false;
                divShowRemarks.Visible = true;
            }
            else if (isOriginalApprover && aExitDal.HasActiveClearanceAutoForward(originalEmpId) && !isFwdBackDone)
            {
                forward.Visible = false;
                Button1.Visible = false;
                btn_Save.Visible = true;
                divShowRemarks.Visible = true;
                lblRemarks.Text = "This clearance is currently auto-forwarded to your designated person and is pending their review.";
            }
            else
            {
                forward.Visible = true;
                Button1.Visible = false;
                btn_Save.Visible = true;
                divShowRemarks.Visible = false;
            }
        }
        else
        {
            forward.Visible = false;
            divShowRemarks.Visible = false;
        }

        hfIsAutoForwarded.Value = aExitDal.IsClearanceAutoForwarded(masterId, exitDetailId, id2) ? "1" : "0";
        hfDoneAndForwardedEmpIds.Value = aExitDal.GetDoneAndForwardedEmpIds(masterId, exitDetailId);
    }
    private void LoadEmpListByDepartment()
    {
        int com = Convert.ToInt32(Session["CompanyId"].ToString());

        int SalId = 0;
        int DivId = 0;
        try
        {
              DivId = Convert.ToInt32(Session["DivisionId"].ToString());
            SalId = Convert.ToInt32(Session["SalaryLoationId"].ToString());
        }
        catch (Exception)
        {
            
            //throw;
        }

        if (DivId == 48 && SalId == 108)
        {
            try
            {
                using (
                    DataTable dt22 = _commonDataLoad.GetEmpDDLAActivebyDivisionSalId(com.ToString(), DivId.ToString(),
                        SalId.ToString()))
                {



                    ddlEmpInfoList.DataSource = dt22;
                    ddlEmpInfoList.DataValueField = "EmpInfoId";
                    ddlEmpInfoList.DataTextField = "EmpName";
                    ddlEmpInfoList.DataBind();
                    ddlEmpInfoList.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                    ddlEmpInfoList.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
                ddlEmpInfoList.Items.Clear();
                //throw;
            }
        }
        else
        {

            try
            {
                //int com = Convert.ToInt32(Session["CompanyId"].ToString());
                int Dept = Convert.ToInt32(Session["DepartmentId"].ToString());

                using (DataTable dt222 = aExitDal.GetEmpDDLByDepartMent(com.ToString(), Dept.ToString()))
                {



                    ddlEmpInfoList.DataSource = dt222;
                    ddlEmpInfoList.DataValueField = "EmpInfoId";
                    ddlEmpInfoList.DataTextField = "EmpName";
                    ddlEmpInfoList.DataBind();
                    ddlEmpInfoList.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                    ddlEmpInfoList.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
                ddlEmpInfoList.Items.Clear();
                //throw;
            }
        }
    }

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    private void LoadEmpListByDivision()
    {
         int com = Convert.ToInt32(Session["CompanyId"].ToString());
            int DivId = Convert.ToInt32(Session["DivisionId"].ToString());
            int SalId = 0;
        try
        {
            SalId = Convert.ToInt32(Session["SalaryLoationId"].ToString());
        }
        catch (Exception)
        {
            
            //throw;
        }

            if (DivId == 48 && SalId == 108)
        {
             try
            {
                using (DataTable dt22 = _commonDataLoad.GetEmpDDLAActivebyDivisionSalId(com.ToString(), DivId.ToString(), SalId.ToString()))
                {



                    ddlEmpInfoList.DataSource = dt22;
                    ddlEmpInfoList.DataValueField = "EmpInfoId";
                    ddlEmpInfoList.DataTextField = "EmpName";
                    ddlEmpInfoList.DataBind();
                    ddlEmpInfoList.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                    ddlEmpInfoList.SelectedIndex = 0;
                }
            }
             catch (Exception)
             {
                 ddlEmpInfoList.Items.Clear();
                 //throw;
             }
        }
        else
        {
            try
            {


                using (DataTable dt22 = _commonDataLoad.GetEmpDDLAActivebyDivisionId(com.ToString(), DivId.ToString()))
                {



                    ddlEmpInfoList.DataSource = dt22;
                    ddlEmpInfoList.DataValueField = "EmpInfoId";
                    ddlEmpInfoList.DataTextField = "EmpName";
                    ddlEmpInfoList.DataBind();
                    ddlEmpInfoList.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                    ddlEmpInfoList.SelectedIndex = 0;
                }
            }
            catch (Exception)
            {
                ddlEmpInfoList.Items.Clear();
                //throw;
            }
        }

     

    }

    private void LoadInitialGrid()
    {



        DataTable dtDetails = new DataTable();

        dtDetails.Columns.Add("Otherconsumption");
        
        dtDetails.Columns.Add("Remarks");
    

        DataRow dr = null;
        dr = dtDetails.NewRow();

        dr["Otherconsumption"] = "";
        
        dr["Remarks"] = "";
       

        dtDetails.Rows.Add(dr);

        itemsDetailGridView.DataSource = null;
        itemsDetailGridView.DataBind();
        itemsDetailGridView.DataSource = dtDetails;
        itemsDetailGridView.DataBind();
      
    }
    private void LoadDropDownList()
    {
        aExitDal.LoadCompanyDropDownList(ddlCompany);

    }
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (SaveDataValidation())
        {
            int? forwardedEmpIdForMail = null;

            SqlTransaction transaction;
            using (SqlConnection connection = aExitDal.OpenTransactionalConnection(out transaction))
            {
                try
                {
                    // Server-side ownership check - the Save/Forward buttons are only hidden
                    // client-side for non-approvers (see ForwardValidatn), so the actual write
                    // must independently confirm the logged-in user is still the current
                    // EmpInfoIdApproval for this record before any update happens.
                    int currentEmpId = GetCurrentEmpInfoId();
                    DataTable dtOwnershipCheck = aExitDal.CheckClearence(connection, transaction,
                        Convert.ToInt32(hfExitMasterId.Value),
                        Convert.ToInt32(hfExitDetailId.Value),
                        currentEmpId);
                    if (dtOwnershipCheck.Rows.Count == 0)
                    {
                        throw new InvalidOperationException("This clearance is not assigned to you for approval.");
                    }

                    int originalEmpId = Convert.ToInt32(dtOwnershipCheck.Rows[0]["EmpInfoId"]);
                    if (currentEmpId != originalEmpId && !chkIsForword.Checked)
                    {
                        throw new InvalidOperationException("This clearance is assigned to you for review. Please use 'Save & Forward' to return it to the original approver.");
                    }

                    bool isFwdBackDone = dtOwnershipCheck.Rows[0]["isForwardBackDone"] != DBNull.Value && Convert.ToBoolean(dtOwnershipCheck.Rows[0]["isForwardBackDone"]);
                    if (!chkIsForword.Checked && aExitDal.HasActiveClearanceAutoForward(originalEmpId) && !isFwdBackDone)
                    {
                        throw new InvalidOperationException("This clearance is currently auto-forwarded to your designated person and is pending their review.");
                    }

                    if (chkIsForword.Checked)
                    {
                        //Save resource
                        Int32 masterId = aExitDal.SaveExitMasterInfo(connection, transaction, PrepareDateForMasterSaveForSaveandForward());

                        if (masterId > 0)
                        {

                            List<MiscellaneousInfoDocumentDAO> DocList = new List<MiscellaneousInfoDocumentDAO>();

                            for (int i = 0; i < gv_Doc.Rows.Count; i++)
                            {
                                HiddenField hfDocumentLink = (HiddenField)gv_Doc.Rows[i].FindControl("hfDocumentLink");
                                Label lbl_DocumentNote = (Label)gv_Doc.Rows[i].FindControl("lbl_DocumentNote");
                                HiddenField hfFileName = (HiddenField)gv_Doc.Rows[i].FindControl("hfFileName");


                                MiscellaneousInfoDocumentDAO DocA = new MiscellaneousInfoDocumentDAO();
                                DocA.FileName = hfFileName.Value.ToString();
                                DocA.DocumentLink = hfDocumentLink.Value.ToString();
                                DocA.DocumentNote = lbl_DocumentNote.Text.Trim();

                                DocList.Add(DocA);
                            }

                            aExitDal.SaveDocumentDetails(connection, transaction, DocList, masterId);

                            Int32 detailId = SaveDetailInformation(connection, transaction, masterId);

                            if (hfDivision.Value == "48")
                            {
                                bool status = aExitDal.DeleteDataForClearance(connection, transaction, Convert.ToInt32(hfEmpInfoId.Value));

                                Int32 ClearanceId = aExitDal.SaveClearanceInfo(connection, transaction, DataPrepareForSave(), masterId, Session["UserId"].ToString());
                            }

                            if (detailId > 0)
                            {
                                bool sts = false;
                                if (hfSetInfo.Value == "Dep")
                                {
                                    sts = aExitDal.DeleteResourceDataForDepartment(
                                        connection, transaction,
                                        int.Parse(Request.QueryString["EMPID"]),
                                        Convert.ToInt32(Session["DepartmentId"].ToString()),
                                        Request.QueryString["ApprovalStatus"].ToString(),
                                        GetExitMasterId(),
                                        GetExitDetailId());
                                }
                                else
                                {
                                    sts = aExitDal.DeleteResourceDataForDepartment(
                                        connection, transaction,
                                        int.Parse(Request.QueryString["EMPID"]),
                                        Convert.ToInt32(Session["DivisionId"].ToString()),
                                        Request.QueryString["ApprovalStatus"].ToString(),
                                        GetExitMasterId(),
                                        GetExitDetailId());
                                }

                                {

                                    SaveDetailInformation(connection, transaction);


                                }
                            }
                        }
                        //
                        int forwardedEmpId = Convert.ToInt32(ddlEmpInfoList.SelectedValue);
                        string forwardRemarks = !string.IsNullOrEmpty(txtForwardRemarks.Text.Trim()) 
                            ? txtForwardRemarks.Text.Trim() 
                            : lblRemarks.Text.Trim();
                        bool isForwarded = aExitDal.UpdateForwardtoOtherEmpExitDetail(
                            connection, transaction,
                            Convert.ToInt32(hfEmpInfoId.Value),
                            GetCurrentEmpInfoId(),
                            forwardedEmpId,
                            int.Parse(Request.QueryString["ExitDetailId"]),
                            forwardRemarks);

                        if (!isForwarded)
                        {
                            throw new InvalidOperationException("Employee clearance could not be forwarded.");
                        }

                        transaction.Commit();
                        forwardedEmpIdForMail = forwardedEmpId;

                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                                   "alert",
                                   "alert('Opearation Successful...');window.location ='ClearenceFormView.aspx';",
                                   true);
                    }
                    else
                    {

                        if (valida())
                        {

                            Int32 masterId = aExitDal.SaveExitMasterInfo(connection, transaction, PrepareDateForMasterSave(connection, transaction));

                            if (masterId > 0)
                            {

                                List<MiscellaneousInfoDocumentDAO> DocList = new List<MiscellaneousInfoDocumentDAO>();

                                for (int i = 0; i < gv_Doc.Rows.Count; i++)
                                {
                                    HiddenField hfDocumentLink = (HiddenField)gv_Doc.Rows[i].FindControl("hfDocumentLink");
                                    Label lbl_DocumentNote = (Label)gv_Doc.Rows[i].FindControl("lbl_DocumentNote");
                                    HiddenField hfFileName = (HiddenField)gv_Doc.Rows[i].FindControl("hfFileName");


                                    MiscellaneousInfoDocumentDAO DocA = new MiscellaneousInfoDocumentDAO();
                                    DocA.FileName = hfFileName.Value.ToString();
                                    DocA.DocumentLink = hfDocumentLink.Value.ToString();
                                    DocA.DocumentNote = lbl_DocumentNote.Text.Trim();

                                    DocList.Add(DocA);
                                }

                                aExitDal.SaveDocumentDetails(connection, transaction, DocList, masterId);

                                Int32 detailId = SaveDetailInformation(connection, transaction, masterId);

                                if (hfDivision.Value == "48")
                                {
                                    bool status = aExitDal.DeleteDataForClearance(connection, transaction, Convert.ToInt32(hfEmpInfoId.Value));

                                    Int32 ClearanceId = aExitDal.SaveClearanceInfo(connection, transaction, DataPrepareForSave(), masterId, Session["UserId"].ToString());
                                }

                                if (detailId > 0)
                                {
                                    bool sts = false;
                                    if (hfSetInfo.Value == "Dep")
                                    {
                                        sts = aExitDal.DeleteResourceDataForDepartment(
                                            connection, transaction,
                                            int.Parse(Request.QueryString["EMPID"]),
                                            Convert.ToInt32(Session["DepartmentId"].ToString()),
                                            Request.QueryString["ApprovalStatus"].ToString(),
                                            GetExitMasterId(),
                                            GetExitDetailId());
                                    }
                                    else
                                    {
                                        sts = aExitDal.DeleteResourceDataForDepartment(
                                            connection, transaction,
                                            int.Parse(Request.QueryString["EMPID"]),
                                            Convert.ToInt32(Session["DivisionId"].ToString()),
                                            Request.QueryString["ApprovalStatus"].ToString(),
                                            GetExitMasterId(),
                                            GetExitDetailId());
                                    }



                                        SaveDetailInformation(connection, transaction);

                                    transaction.Commit();

                                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                                        "alert",
                                        "alert('Opearation Successful...');window.location ='ClearenceFormView.aspx';",
                                        true);

                                }


                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ShowMessageBox("Error: " + ex.Message);
                }
            }

            if (forwardedEmpIdForMail.HasValue)
            {
                SenMailForApprved(forwardedEmpIdForMail.Value, "Employee Clearance Review",
                    @"<br/> Dear Sir, <br/>
An employee clearance has been forwarded to you for review. <br/>" +
                    GetEmployeeInformationForMail() + @"
Please log in to HRIS for details.<br/>
<a href='https://hris.smc-bd.org/'>https://hris.smc-bd.org/</a><br/>");
            }
        }
    }
    private EmpClearanceDao DataPrepareForSave()
    {

        EmpClearanceDao aDao = new EmpClearanceDao();

        aDao.EmpInfoId = string.IsNullOrEmpty(hfEmpInfoId.Value) ? 0 : Convert.ToInt32(hfEmpInfoId.Value);

        aDao.MobIssuDate = string.IsNullOrEmpty(MoIssueDate.Text) ? (DateTime?)null : DateTime.Parse(MoIssueDate.Text).Date;
        aDao.MobActualPrice = string.IsNullOrEmpty(MoActualPrice.Text) ? 0 : decimal.Parse(MoActualPrice.Text);
        aDao.MoDABeforeOne = string.IsNullOrEmpty(MoDABeforOne.Text) ? 0 : decimal.Parse(MoDABeforOne.Text);
        aDao.MoDABeforeOnetotwo = string.IsNullOrEmpty(MoDABeforeOneTwo.Text) ? 0 : decimal.Parse(MoDABeforeOneTwo.Text);
        aDao.MoDAAboveTwo = string.IsNullOrEmpty(MoAboveTwo.Text) ? 0 : decimal.Parse(MoAboveTwo.Text);
        aDao.MoRemarks = string.IsNullOrEmpty(MoRemark.Text) ? null : MoRemark.Text;

        aDao.DBIssuDate = string.IsNullOrEmpty(DBissueDate.Text) ? (DateTime?)null : DateTime.Parse(DBissueDate.Text);
        aDao.DBActualPrice = string.IsNullOrEmpty(DBActualPrice.Text) ? 0 : decimal.Parse(DBActualPrice.Text);
        aDao.DBDeductionAmount = string.IsNullOrEmpty(DBDeductionAmount.Text) ? 0 : decimal.Parse(DBDeductionAmount.Text);
        aDao.DBRemark = string.IsNullOrEmpty(DBRemark.Text) ? null : DBRemark.Text;

        aDao.PIActualCost = string.IsNullOrEmpty(PIActualCost.Text) ? 0 : decimal.Parse(PIActualCost.Text);
        aDao.PIDeductionAmount = string.IsNullOrEmpty(PIDeductionAmount.Text) ? 0 : decimal.Parse(PIDeductionAmount.Text);
        aDao.PIRemark = string.IsNullOrEmpty(PIRemarks.Text) ? null : PIRemarks.Text;

        aDao.IDCard = string.IsNullOrEmpty(IDCard.Text) ? null : IDCard.Text;
        aDao.IDCardRemark = string.IsNullOrEmpty(IDCardRemark.Text) ? null : IDCardRemark.Text;

        aDao.MarketDues = string.IsNullOrEmpty(MarketDues.Text) ? 0 : decimal.Parse(MarketDues.Text);
        aDao.MarketRemarks = string.IsNullOrEmpty(MarketDuesRemark.Text) ? null : MarketDuesRemark.Text;

        aDao.TotalDeductionAmount = string.IsNullOrEmpty(TotalDeductionAmount.Text) ? 0 : decimal.Parse(TotalDeductionAmount.Text);
        aDao.ToDeducAmtRemark = string.IsNullOrEmpty(TotalDeductionAmountRemark.Text) ? null : TotalDeductionAmountRemark.Text;


        try
        {
            aDao.TickMark = string.IsNullOrEmpty(checkboxlist1.SelectedItem.Text) ? null : checkboxlist1.SelectedItem.Text;
        }
        catch (Exception)
        {
            aDao.TickMark = "";
            //throw;
        }

        return aDao;
    }
    private bool valida()
    {
        for (int i = 0; i < itemsDetailGridView.Rows.Count; i++)
        {

            TextBox resourceTextBox =
                (TextBox) itemsDetailGridView.Rows[i].Cells[1].FindControl("OtherConsumptionTextBox");

            
            if (resourceTextBox.Text == "")
            {
                ShowMessageBox("Description can not be empty!!!");
                resourceTextBox.Focus();
                return false;
            }
        }

        return true;
    }

    private string GetEmployeeInformationForMail()
    {
        return "<table style='border-collapse:collapse; margin:12px 0;'>" +
               "<tr><td style='border:1px solid #ccc; padding:6px 10px;'><strong>Employee ID</strong></td>" +
               "<td style='border:1px solid #ccc; padding:6px 10px;'>" + WebUtility.HtmlEncode(lblEmpId.Text.Trim()) + "</td></tr>" +
               "<tr><td style='border:1px solid #ccc; padding:6px 10px;'><strong>Employee Name</strong></td>" +
               "<td style='border:1px solid #ccc; padding:6px 10px;'>" + WebUtility.HtmlEncode(lblEmployeeName.Text.Trim()) + "</td></tr>" +
               "<tr><td style='border:1px solid #ccc; padding:6px 10px;'><strong>Designation</strong></td>" +
               "<td style='border:1px solid #ccc; padding:6px 10px;'>" + WebUtility.HtmlEncode(desigNameLabel.Text.Trim()) + "</td></tr>" +
               "<tr><td style='border:1px solid #ccc; padding:6px 10px;'><strong>Department</strong></td>" +
               "<td style='border:1px solid #ccc; padding:6px 10px;'>" + WebUtility.HtmlEncode(deptNameLabel.Text.Trim()) + "</td></tr>" +
               "</table>";
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
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                using (SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("no-reply@smc-bd.org", "vfwzmbxprdmqhhln");
                    smtpClient.Timeout = 20000;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("no-reply@smc-bd.org");
                        mailMessage.IsBodyHtml = true;
                        mailMessage.To.Add(ForMailAddress);
                        mailMessage.Subject = mSubject;
                        mailMessage.Body =
                   "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +
                   mBody
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

    private Int32 SaveDetailInformation(SqlConnection connection, SqlTransaction transaction)
    {
        Int32 retVal;
        try
        {
            retVal = aExitDal.SaveClearenceDetail2(connection, transaction, PrepareJobLocationDataForSaveDetail());
        }
        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }

        return retVal;
    }

    private Int32 SaveDetailInformation(SqlConnection connection, SqlTransaction transaction, Int32 val)
    {
        Int32 retVal;
        try
        {
            retVal = aExitDal.SaveClearenceDetail(connection, transaction, PrepareJobLocationDataForSaveDetail(val));
        }
        catch (Exception ex)
        {
            retVal = 0;
            throw ex;
        }

        return retVal;
    }
    private List<ClearenceFormDetailDao> PrepareJobLocationDataForSaveDetail()
    {
        List<ClearenceFormDetailDao> aLocationDaos = new List<ClearenceFormDetailDao>();
        ClearenceFormDetailDao aLocationDao;

        if (itemsDetailGridView.Rows.Count > 0)
        {
            for (int i = 0; i < itemsDetailGridView.Rows.Count; i++)
            {

                TextBox resourceTextBox = (TextBox)itemsDetailGridView.Rows[i].Cells[1].FindControl("OtherConsumptionTextBox");
                TextBox remarksTextBox = (TextBox)itemsDetailGridView.Rows[i].Cells[1].FindControl("remarksTextBox");

                aLocationDao = new ClearenceFormDetailDao();
               
                aLocationDao.Resource = resourceTextBox.Text;
                aLocationDao.Remarks = remarksTextBox.Text;

                aLocationDao.EmpID = int.Parse(Request.QueryString["EMPID"]);

                 int departmentId = 0;

                
                                


                 DataTable aTable = aExitDal.GetUserDepartmentId(Convert.ToInt32(Session["UserId"]));
        if (hfSetInfo.Value == "Dep")
        {
              departmentId = Convert.ToInt32(aTable.Rows[0]["DepartmentId"].ToString());
        }
        else
        {
            departmentId = Convert.ToInt32(aTable.Rows[0]["DivisionId"].ToString());
        }

               

                aLocationDao. exitMasterIdNew = Convert.ToInt32(GetExitMasterId());
                aLocationDao.exitDetailIdNew = Convert.ToInt32(GetExitDetailId());
                aLocationDao.DepartmentId =Convert.ToInt32(departmentId);

                aLocationDao.IsDoneEmpId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                aLocationDao.IsDoneDate = DateTime.Now;


                aLocationDao.MainRemarks = txtRemarrks.Text.Trim();
                aLocationDao.SetInfo = hfSetInfo.Value.Trim();
                aLocationDao.ApprovalCondition = Request.QueryString["ApprovalStatus"].ToString();

                aLocationDao.Recommend = rbRecommend.SelectedValue.Trim();
                if (rbRecommend.SelectedValue=="")
                {
                    aLocationDao.Recommend = 0.ToString();
                }




                aLocationDaos.Add(aLocationDao);
            }
        }

        return aLocationDaos;
    }
    private List<ClearenceFormDetailDao> PrepareJobLocationDataForSaveDetail(Int32 val)
    {
        List<ClearenceFormDetailDao> aLocationDaos = new List<ClearenceFormDetailDao>();
        ClearenceFormDetailDao aLocationDao;

        if (itemsDetailGridView.Rows.Count > 0)
        {
            for (int i = 0; i < itemsDetailGridView.Rows.Count; i++)
            {

                TextBox resourceTextBox = (TextBox)itemsDetailGridView.Rows[i].Cells[1].FindControl("OtherConsumptionTextBox");
                TextBox remarksTextBox = (TextBox)itemsDetailGridView.Rows[i].Cells[1].FindControl("remarksTextBox");

                aLocationDao = new ClearenceFormDetailDao();
                aLocationDao.MasterId = val;
                aLocationDao.Resource = resourceTextBox.Text;
                aLocationDao.Remarks = remarksTextBox.Text;

                aLocationDaos.Add(aLocationDao);
            }
        }

        return aLocationDaos;
    }

    private ClearenceFormDao PrepareDateForMasterSaveForSaveandForward()
    {

        DataTable aTable = aExitDal.GetUserDepartmentId(Convert.ToInt32(Session["UserId"]));

        //aExitDal.UpdateEmpExitDetail(Convert.ToInt32(hfEmpInfoId.Value),
        //             Convert.ToInt32(Session["EmpInfoIdLat"]));
        int departmentId = 0;
        if (hfSetInfo.Value == "Dep")
        {
              departmentId = Convert.ToInt32(aTable.Rows[0]["DepartmentId"].ToString());
        }
        else
        {
            departmentId = Convert.ToInt32(aTable.Rows[0]["DivisionId"].ToString());
        }

      

        ClearenceFormDao aMasterDao = new ClearenceFormDao();

        //aMasterDao.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
        aMasterDao.EmployeeId = Convert.ToInt32(hfEmpInfoId.Value);
        aMasterDao.EmpCode = empCode.Text.Trim();
        aMasterDao.EmpName = empName.Text.Trim();
        aMasterDao.EntryByPersonId = Convert.ToInt32(Session["UserId"]);

        if (hdfjobLeft.Value!="")
        {
            aMasterDao.EmployeeJobLeftId = Convert.ToInt32(hdfjobLeft.Value);
        }
        else
        {
            aMasterDao.EmployeeJobLeftId = Convert.ToInt32(0);
            
        }

       
        aMasterDao.DepartmentId = departmentId;
        aMasterDao.JoiningDate = Convert.ToDateTime(dtJoining.Text.Trim());
        aMasterDao.DesignationId = Convert.ToInt32(hfDesignation.Value);
        aMasterDao.DivisionId = Convert.ToInt32(hfDivision.Value);
        try
        {
            aMasterDao.SalaryLoationId = Convert.ToInt32(hfSalaryLoationId.Value);
        }
        catch (Exception)
        {
            aMasterDao.SalaryLoationId = null;
            //throw;
        }
        try
        {
            aMasterDao.SalaryGradeId = Convert.ToInt32(hfSalaryGrade.Value);
        }
        catch (Exception)
        {
            aMasterDao.SalaryGradeId = null;
            //throw;
        }
        aMasterDao.Description = descriptionTextbox.Text.Trim();
        aMasterDao.Recommend = rbRecommend.SelectedValue.Trim();
        aMasterDao.Remarks = txtRemarrks.Text.Trim();

        aMasterDao.ActionStatus = "Posted";

        aMasterDao.EntryBy = Session["LoginName"].ToString();
        aMasterDao.EntryDate = DateTime.Now;

        return aMasterDao;




    }

    private ClearenceFormDao PrepareDateForMasterSave(SqlConnection connection, SqlTransaction transaction)
    {

        DataTable aTable = aExitDal.GetUserDepartmentId(Convert.ToInt32(Session["UserId"]));

        aExitDal.UpdateEmpExitDetail(connection, transaction, GetExitMasterId(),
                     Convert.ToInt32(Session["EmpInfoIdLat"]), int.Parse(Request.QueryString["ExitDetailId"]));

        int departmentId = 0;
        if (hfSetInfo.Value == "Dep")
        {
            try
            {
                departmentId = Convert.ToInt32(aTable.Rows[0]["DepartmentId"].ToString());
            }
            catch (Exception)
            {
                departmentId = 0;
                //throw;
            }
        }
        else
        {
            departmentId = Convert.ToInt32(aTable.Rows[0]["DivisionId"].ToString());
        }

            ClearenceFormDao aMasterDao = new ClearenceFormDao();

            //aMasterDao.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
            aMasterDao.EmployeeId = Convert.ToInt32(hfEmpInfoId.Value);
            aMasterDao.EmpCode = empCode.Text.Trim();
            aMasterDao.EmpName = empName.Text.Trim();
            aMasterDao.EntryByPersonId = Convert.ToInt32(Session["UserId"]);

            if (hdfjobLeft.Value!="")
        {
            aMasterDao.EmployeeJobLeftId = Convert.ToInt32(hdfjobLeft.Value);
        }
            else
            {
                aMasterDao.EmployeeJobLeftId = 0;
            }
           
            aMasterDao.DepartmentId = departmentId;
            aMasterDao.JoiningDate = Convert.ToDateTime(dtJoining.Text.Trim());
        try
        {
            aMasterDao.DesignationId = Convert.ToInt32(hfDesignation.Value);
        }
        catch
        {
            aMasterDao.DesignationId = 0;
        }
            aMasterDao.DivisionId = Convert.ToInt32(hfDivision.Value);

        try
        {
            aMasterDao.SalaryLoationId = Convert.ToInt32(hfSalaryLoationId.Value);
        }
        catch (Exception)
        {
            aMasterDao.SalaryLoationId = null;
            //throw;
        }


          try
        {
            aMasterDao.SalaryGradeId = Convert.ToInt32(hfSalaryGrade.Value);
        }
        catch (Exception)
        {
            aMasterDao.SalaryGradeId = null;
            //throw;
        }


            aMasterDao.Description = descriptionTextbox.Text.Trim();
            aMasterDao.Recommend = rbRecommend.SelectedValue.Trim();
            aMasterDao.Remarks = txtRemarrks.Text.Trim();

            aMasterDao.ActionStatus = "Posted";

            aMasterDao.EntryBy = Session["LoginName"].ToString();
            aMasterDao.EntryDate = DateTime.Now;

            return aMasterDao;
       

       

    }

    private bool SaveDataValidation()
    {
        //if (ddlCompany.SelectedValue == "")
        //{
        //    ShowMessageBox("You have to select company !!!");
        //    return false;
        //}

        if (hfEmpInfoId.Value == "")
        {
            ShowMessageBox("You have to select employee !!!");
            return false;
        }

        if (chkIsForword.Checked == true)
        {
            if (ddlEmpInfoList.SelectedValue == "")
            {
                ShowMessageBox("Please Select an employee !!!");
                ddlEmpInfoList.Focus();
                return false;
            }

            int selectedEmpId;
            if (int.TryParse(ddlEmpInfoList.SelectedValue, out selectedEmpId))
            {
                bool isDoneAndFwd = aExitDal.IsEmployeeAlreadyDoneAndForwardedBack(GetExitMasterId(), GetExitDetailId(), selectedEmpId);
                if (!isDoneAndFwd)
                {
                    string message;
                    if (!aExitDal.CheckPendingClearanceAssignment(GetExitMasterId(), selectedEmpId, GetExitDetailId(), out message))
                    {
                        ddlEmpInfoList.SelectedIndex = 0;
                        hfConfirmedReforwardEmpId.Value = string.Empty;
                        ShowMessageBox(message);
                        ddlEmpInfoList.Focus();
                        return false;
                    }
                }
            }
        }

        //if (checkboxlist1.SelectedValue == "")
        //{
        //    ShowMessageBox("Please select capable tick mark !!!");
            
        //    return false;
        //}

        return true;
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        ddlCompany.SelectedValue = "";
        txt_EmpName.Text = "";
        hfEmpInfoId.Value = "";
        empName.Text = "";
        empCode.Text = "";
        ddlDivision.Text = "";
        hfDivision.Value = "";
        ddlDesignation.Text = "";
        hfDesignation.Value = "";
        ddlSalaryGrade.Text = "";
        hfSalaryGrade.Value = "";
        hfSalaryLoationId.Value = "";

        dtJoining.Text = "";

        descriptionTextbox.Text = "";

    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            txt_EmpName.Enabled = true;

            Session["CompanyId"] = "";
            Session["CompanyId"] = ddlCompany.SelectedValue;
        }
        else
        {
            Session["CompanyId"] = "";
            txt_EmpName.Enabled = false;
        }


    }
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    public void LoadDataEmp(string id)
    {
        //SetEmployeeInfo();

        hfEmpInfoId.Value = id;
        if (Session["EmployeeJobLeftId"] != null)
        {
            hdfjobLeft.Value = Session["EmployeeJobLeftId"].ToString();
        }

    
        if (hfEmpInfoId.Value != "")
        {


              AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();
            DataTable dtEmp = _appPartA.GetEmployeeDetails(Convert.ToInt32(id));
            if (dtEmp.Rows.Count > 0)
            {


                lblEmployeeName.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();
                lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();




                deptNameLabel.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();


                desigNameLabel.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();


                joiningDateLabel.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
                LocationLabel.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
                lblPlace.Text = dtEmp.Rows[0]["Location"].ToString();

                ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();




            }
        


            DataTable aTable = aExitDal.LoadEmployeeInfo(hfEmpInfoId.Value);




            if (aTable.Rows.Count > 0)
            {
                ddlDivision.Text = aTable.Rows[0].Field<string>("DivisionName");

                 
                    hfDivision.Value = aTable.Rows[0].Field<Int32>("DivisionId").ToString(CultureInfo.InvariantCulture);
                


                ddlDesignation.Text = aTable.Rows[0].Field<string>("Designation");
                try
                {
                    hfDesignation.Value = aTable.Rows[0].Field<Int32>("DesignationId").ToString(CultureInfo.InvariantCulture);
                }
                catch
                {

                }

                try
                {
                    hfSalaryLoationId.Value = aTable.Rows[0].Field<Int32>("SalaryLoationId").ToString(CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    hfSalaryLoationId.Value = 0.ToString();

                }
              

                ddlSalaryGrade.Text = aTable.Rows[0].Field<string>("GradeName");
                try
                {
                    hfSalaryGrade.Value = aTable.Rows[0].Field<Int32>("SalaryGradeId").ToString(CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    
                    ///throw;
                }

                empCode.Text = aTable.Rows[0].Field<string>("EmpMasterCode");
                empName.Text = aTable.Rows[0].Field<string>("EmpName");

                dtJoining.Text = aTable.Rows[0].Field<DateTime>("DateOfJoin").ToString("dd-MMM-yyyy");

            }
            else
            {
                txt_EmpName.Text = "";
                ShowMessageBox("No Information found !!!");
            }
        }
    }

    protected void txt_EmpName_OnTextChanged(object sender, EventArgs e)
    {
        SetEmployeeInfo();

        if (hfEmpInfoId.Value != "")
        {
            DataTable aTable = aExitDal.LoadEmployeeInfo(hfEmpInfoId.Value, ddlCompany.SelectedValue);

            if (aTable.Rows.Count > 0)
            {
                ddlDivision.Text = aTable.Rows[0].Field<string>("DivisionName");
                hfDivision.Value = aTable.Rows[0].Field<Int32>("DivisionId").ToString(CultureInfo.InvariantCulture);

                ddlDesignation.Text = aTable.Rows[0].Field<string>("Designation");
                hfDesignation.Value = aTable.Rows[0].Field<Int32>("DesignationId").ToString(CultureInfo.InvariantCulture);

                ddlSalaryGrade.Text = aTable.Rows[0].Field<string>("GradeName");
                hfSalaryGrade.Value = aTable.Rows[0].Field<Int32>("SalaryGradeId").ToString(CultureInfo.InvariantCulture);

                empCode.Text = aTable.Rows[0].Field<string>("EmpMasterCode");
                empName.Text = aTable.Rows[0].Field<string>("EmpName");

                dtJoining.Text = aTable.Rows[0].Field<DateTime>("DateOfJoin").ToString("dd-MMM-yyyy");

            }
            else
            {
                txt_EmpName.Text = "";
                ShowMessageBox("No Information found !!!");
            }
        }
    }

    protected void ShowMessageBox(string message)
    {
        string encodedMessage = HttpUtility.JavaScriptStringEncode(message);
        string sScript = String.Format("alert('{0}');", encodedMessage);
        ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(), sScript, true);
    }


    private void SetEmployeeInfo()
    {
        string empName = txt_EmpName.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');
            if (Session["EmpInfoId"] != null)
            {
                hfEmpInfoId.Value = Session["EmpInfoId"].ToString();
                Session["EmpInfoId"] = null;
            }
            else
            {
                hfEmpInfoId.Value = emp[0];
            }
            
        }
        else
        {
            hfEmpInfoId.Value = "";
            ShowMessageBox("Input Correct Data !!");
        }

        txt_EmpName.Text = "";
    }

    protected void itemsDetailGridView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
         
    }

    protected void additemImageButton_Click(object sender, ImageClickEventArgs e)
    {
        int rowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

        DataTable aTable = new DataTable();
        aTable.Columns.Add("Otherconsumption");
        aTable.Columns.Add("Remarks");
      
        
        DataRow dr;

      

        for (int i = 0; i < itemsDetailGridView.Rows.Count; i++)
        {
            dr = aTable.NewRow();
            dr["Otherconsumption"] =
                   ((TextBox)itemsDetailGridView.Rows[i].Cells[2].FindControl("OtherconsumptionTextBox")).Text.Trim();
          
            dr["Remarks"] =
                ((TextBox)itemsDetailGridView.Rows[i].Cells[2].FindControl("remarksTextBox")).Text.Trim();
         
          
          
            aTable.Rows.Add(dr);

            if (rowIndex == i)
            {
                dr = aTable.NewRow();
                dr["Otherconsumption"] = "";
                dr["Remarks"] = "";

                aTable.Rows.Add(dr);
            }
        }

        //Session["table"] = (DataTable)aTable;
        itemsDetailGridView.DataSource = null;
        itemsDetailGridView.DataBind();
        itemsDetailGridView.DataSource = aTable;
        itemsDetailGridView.DataBind();

    }

    protected void deleteitemLButton_Click(object sender, ImageClickEventArgs e)
    {
        int rowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

        DataTable aTable = new DataTable();
         
        aTable.Columns.Add("Remarks");
       
        aTable.Columns.Add("Otherconsumption");
        DataRow dr;
       
        for (int i = 0; i < itemsDetailGridView.Rows.Count; i++)
        {
            if (rowIndex != i)
            {
                dr = aTable.NewRow();
              
                dr["Remarks"] =
                    ((TextBox)itemsDetailGridView.Rows[i].Cells[2].FindControl("remarksTextBox")).Text.Trim();
              
                dr["Otherconsumption"] =
                    ((TextBox)itemsDetailGridView.Rows[i].Cells[2].FindControl("OtherconsumptionTextBox")).Text.Trim();

                aTable.Rows.Add(dr);
            }
        }

        if (aTable.Rows.Count > 0)
        {
            itemsDetailGridView.DataSource = null;
            itemsDetailGridView.DataBind();
            itemsDetailGridView.DataSource = aTable;
            itemsDetailGridView.DataBind();



        }
    }

    protected void Button1_OnClick(object sender, EventArgs e)
    {
        int id = 0;
        int? forwardBackEmpIdForMail = null;

        if (valida())
        {
            SqlTransaction transaction;
            using (SqlConnection connection = aExitDal.OpenTransactionalConnection(out transaction))
            {
                try
                {
                    int currentEmpId = GetCurrentEmpInfoId();
                    DataTable dtata = aExitDal.CheckClearence(connection, transaction, Convert.ToInt32(hfExitMasterId.Value),
                        Convert.ToInt32(hfExitDetailId.Value),
                        currentEmpId);
                    if (dtata.Rows.Count > 0 && dtata.Rows[0]["EmpInfoId"] != DBNull.Value)
                    {
                        id = Convert.ToInt32(dtata.Rows[0]["EmpInfoId"].ToString());
                    }
                    else
                    {
                        DataTable dtRemarks = aExitDal.GetShowRemarksById(Request.QueryString["ExitDetailId"]);
                        if (dtRemarks.Rows.Count > 0 && dtRemarks.Rows[0]["EmpInfoId"] != DBNull.Value)
                        {
                            id = Convert.ToInt32(dtRemarks.Rows[0]["EmpInfoId"].ToString());
                        }
                        else
                        {
                            throw new InvalidOperationException("The original clearance approver could not be found.");
                        }
                    }

                    string forwardBackRemarks = !string.IsNullOrEmpty(lblRemarks.Text.Trim()) 
                        ? lblRemarks.Text.Trim() 
                        : (!string.IsNullOrEmpty(txtRemarrks.Text.Trim()) ? txtRemarrks.Text.Trim() : txtForwardRemarks.Text.Trim());

                    bool isForwardedBack = aExitDal.UpdateForwardBackEmpExitDetail(connection, transaction, Convert.ToInt32(hfEmpInfoId.Value),
                        id, currentEmpId, int.Parse(Request.QueryString["ExitDetailId"]), forwardBackRemarks);

                    if (!isForwardedBack)
                    {
                        throw new InvalidOperationException("Employee clearance could not be forwarded back.");
                    }

                    //Save resource
                    Int32 masterId = aExitDal.SaveExitMasterInfo(connection, transaction, PrepareDateForMasterSaveForSaveandForward());

                    if (masterId > 0)
                    {
                        Int32 detailId = SaveDetailInformation(connection, transaction, masterId);
                        List<MiscellaneousInfoDocumentDAO> DocList = new List<MiscellaneousInfoDocumentDAO>();

                        for (int i = 0; i < gv_Doc.Rows.Count; i++)
                        {
                            HiddenField hfDocumentLink = (HiddenField)gv_Doc.Rows[i].FindControl("hfDocumentLink");
                            Label lbl_DocumentNote = (Label)gv_Doc.Rows[i].FindControl("lbl_DocumentNote");
                            HiddenField hfFileName = (HiddenField)gv_Doc.Rows[i].FindControl("hfFileName");


                            MiscellaneousInfoDocumentDAO DocA = new MiscellaneousInfoDocumentDAO();
                            DocA.FileName = hfFileName.Value.ToString();
                            DocA.DocumentLink = hfDocumentLink.Value.ToString();
                            DocA.DocumentNote = lbl_DocumentNote.Text.Trim();

                            DocList.Add(DocA);
                        }

                        aExitDal.SaveDocumentDetails(connection, transaction, DocList, masterId);
                        if (hfDivision.Value == "48")
                        {
                            bool status = aExitDal.DeleteDataForClearance(connection, transaction, Convert.ToInt32(hfEmpInfoId.Value));

                            Int32 ClearanceId = aExitDal.SaveClearanceInfo(connection, transaction, DataPrepareForSave(), masterId, Session["UserId"].ToString());
                        }
                        if (detailId > 0)
                        {
                            bool sts = false;
                            if (hfSetInfo.Value == "Dep")
                            {
                                sts = aExitDal.DeleteResourceDataForDepartment(
                                    connection, transaction,
                                    int.Parse(Request.QueryString["EMPID"]),
                                    Convert.ToInt32(Session["DepartmentId"].ToString()),
                                    Request.QueryString["ApprovalStatus"].ToString(),
                                    GetExitMasterId(),
                                    GetExitDetailId());
                            }
                            else
                            {
                                sts = aExitDal.DeleteResourceDataForDepartment(
                                    connection, transaction,
                                    int.Parse(Request.QueryString["EMPID"]),
                                    Convert.ToInt32(Session["DivisionId"].ToString()),
                                    Request.QueryString["ApprovalStatus"].ToString(),
                                    GetExitMasterId(),
                                    GetExitDetailId());
                            }

                            {

                                SaveDetailInformation(connection, transaction);


                            }
                        }


                    }

                    transaction.Commit();
                    forwardBackEmpIdForMail = id;

                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Opearation Successful...');window.location ='ClearenceFormView.aspx';",
                        true);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ShowMessageBox("Error: " + ex.Message);
                }
            }

            if (forwardBackEmpIdForMail.HasValue)
            {
                SenMailForApprved(forwardBackEmpIdForMail.Value, "Employee Clearance Forward Back",
                    @"<br/> Dear Sir, <br/>
An employee clearance has been forwarded back to you for review. <br/>" +
                    GetEmployeeInformationForMail() + @"
Please log in to HRIS for details.<br/>
<a href='https://hris.smc-bd.org/'>https://hris.smc-bd.org/</a><br/>");
            }
        }
    }

    protected void chkIsForword_OnCheckedChanged(object sender, EventArgs e)
    {
        divRemarks.Visible = false;
        divShowRemarks.Visible = false;
        if (chkIsForword.Checked)
        {
            divRemarks.Visible = true;
            bool isAutoForwarded = (hfIsAutoForwarded.Value == "1")
                || aExitDal.IsClearanceAutoForwarded(GetExitMasterId(), GetExitDetailId(), GetCurrentEmpInfoId());

            if (isAutoForwarded)
            {
                ShowMessageBox("This clearance request has already been auto-forwarded to you. Do you want to re-forward it?");
            }
        }
        else
        {
            ddlEmpInfoList.SelectedIndex = 0;
            hfConfirmedReforwardEmpId.Value = string.Empty;
            txtForwardRemarks.Text = string.Empty;
        }
    }

    protected void ddlEmpInfoList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (chkIsForword.Checked && !string.IsNullOrEmpty(ddlEmpInfoList.SelectedValue))
        {
            int selectedEmpId;
            if (int.TryParse(ddlEmpInfoList.SelectedValue, out selectedEmpId))
            {
                bool isDoneAndFwd = aExitDal.IsEmployeeAlreadyDoneAndForwardedBack(GetExitMasterId(), GetExitDetailId(), selectedEmpId);
                if (isDoneAndFwd)
                {
                    // Show message to inform the user, and keep ddlEmpInfoList selected (do NOT deselect)
                    ShowMessageBox("This employee has already completed and forwarded this clearance back to you. Do you want to forward it to them again?");
                }
                else
                {
                    string message;
                    if (!aExitDal.CheckPendingClearanceAssignment(GetExitMasterId(), selectedEmpId, GetExitDetailId(), out message))
                    {
                        ddlEmpInfoList.SelectedIndex = 0;
                        ShowMessageBox(message);
                    }
                }
            }
        }
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ClearenceFormView.aspx");
    }
    protected void brnAddDoc_OnClick(object sender, EventArgs e)
    {
        if (docVali())
        {
            AddNewDocGrid_List();
        }

    }
    ShowMessage aShowMessage = new ShowMessage();
    private void AddNewDocGrid_List()
    {
        if (ViewState["gvDocGrid_List"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["gvDocGrid_List"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();


                drCurrentRow["DocumentLink"] = "../UploadMeetingDocument/" + hfDocFile.Value;
                drCurrentRow["FileName"] = hfDocFileName.Value;




                drCurrentRow["DocumentNote"] = txtSummaryNote.Text.Trim();

                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["gvDocGrid_List"] = dtCurrentTable;

                //Rebind the Grid with the current data to reflect changes   
                gv_Doc.DataSource = dtCurrentTable;
                gv_Doc.DataBind();
            }
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("DocumentLink", typeof(string)));
            dt.Columns.Add(new DataColumn("DocumentNote", typeof(string)));
            dt.Columns.Add(new DataColumn("FileName", typeof(string)));


            dr = dt.NewRow();


            dr["DocumentLink"] = "../UploadMeetingDocument/" + hfDocFile.Value;
            dr["FileName"] = hfDocFileName.Value;




            dr["DocumentNote"] = txtSummaryNote.Text.Trim();
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["gvDocGrid_List"] = dt;

            //Bind the Gridview   
            gv_Doc.DataSource = dt;
            gv_Doc.DataBind();
        }
        //Set Previous Data on Postbacks   
        SetDocGrid_List();


        txtSummaryNote.Text = string.Empty;
        // HyperLink2.Text = "No File Uploaded";
        HyperLink2.NavigateUrl = "";
        hfDocFile.Value = "";
    }
    protected void btnDocRemove_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["gvDocGrid_List"] != null)
        {
            DataTable dt = (DataTable)ViewState["gvDocGrid_List"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["gvDocGrid_List"] = dt;
                //Re bind the GridView for the updated data  
                gv_Doc.DataSource = dt;
                gv_Doc.DataBind();
            }
            else
            {
                ViewState["gvDocGrid_List"] = null;
                //Re bind the GridView for the updated data  
                gv_Doc.DataSource = null;
                gv_Doc.DataBind();
            }
        }
        //Set Previous Data on Postbacks  
        SetDocGrid_List();
    }

    private void SetDocGrid_List()
    {
        int rowIndex = 0;
        if (ViewState["gvDocGrid_List"] != null)
        {
            DataTable dt = (DataTable)ViewState["gvDocGrid_List"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField hfDocumentLink = (HiddenField)gv_Doc.Rows[rowIndex].FindControl("hfDocumentLink");
                    HyperLink HLDocumentLink = (HyperLink)gv_Doc.Rows[rowIndex].FindControl("HLDocumentLink");
                    Label lbl_DocumentLink = (Label)gv_Doc.Rows[rowIndex].FindControl("lbl_DocumentLink");

                    Label lbl_DocumentNote = (Label)gv_Doc.Rows[rowIndex].FindControl("lbl_DocumentNote");
                    HiddenField hfFileName = (HiddenField)gv_Doc.Rows[rowIndex].FindControl("hfFileName");


                    if (i < dt.Rows.Count - 1)
                    {
                        hfFileName.Value = dt.Rows[i]["FileName"].ToString();

                        hfDocumentLink.Value = dt.Rows[i]["DocumentLink"].ToString();
                        lbl_DocumentLink.Text = dt.Rows[i]["DocumentLink"].ToString();
                        HLDocumentLink.NavigateUrl = dt.Rows[i]["DocumentLink"].ToString();

                        lbl_DocumentNote.Text = dt.Rows[i]["DocumentNote"].ToString();

                    }

                    rowIndex++;
                }
            }
        }
    }
    private bool docVali()
    {
        if (hfDocFile.Value == "")
        {
            aShowMessage.ShowMessageBox("Please Upload a Document", this);

            return false;
        }
        if (txtSummaryNote.Text == "")
        {
            aShowMessage.ShowMessageBox("Please Enter Summary Note ", this);

            return false;
        }
        return true;

    }
    private int GetExitMasterId()
    {
        int id;
        if (int.TryParse(hfExitMasterId.Value, out id))
        {
            return id;
        }

        int queryValue;
        return int.TryParse(Request.QueryString["ExitMasterId"], out queryValue) ? queryValue : 0;
    }

    private int GetExitDetailId()
    {
        int id;
        if (int.TryParse(hfExitDetailId.Value, out id))
        {
            return id;
        }

        int queryValue;
        return int.TryParse(Request.QueryString["ExitDetailId"], out queryValue) ? queryValue : 0;
    }
    protected void btnDocUp_OnClick(object sender, EventArgs e)
    {
        if (FUDocument.HasFile)
        {
            string _fileExt = System.IO.Path.GetExtension(FUDocument.FileName);
            string AdsFile = "Clear_DOC_" + Guid.NewGuid().ToString() + Path.GetExtension(FUDocument.FileName);
            //  fileName = guid.ToString() + imageFileUpload.FileName;
            FUDocument.SaveAs(Server.MapPath("../UploadImg/") + AdsFile);
            HyperLink2.NavigateUrl = "../UploadImg/" + AdsFile;
            HyperLink2.Text = "Uploaded Successfully";
        }
        else
        {
            HyperLink2.NavigateUrl = "";
            //    HyperLink2.Text = "No File Uploaded";
        }
    }
}
