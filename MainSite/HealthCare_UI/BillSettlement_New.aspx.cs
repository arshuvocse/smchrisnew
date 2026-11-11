using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.HealthCare_DAL;
using DAO.HealthCare_Dao;
using HELPER_FUNCTIONS.HELPERS;

public partial class HealthCare_UI_BillSettlement_New : System.Web.UI.Page
{


    private BillSettlementDal aSettlementDal = new BillSettlementDal();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private HealthCareListDal aListDal = new HealthCareListDal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ReadOnltDate();

            using (DataTable dtemp = _commonDataLoad.GetBankName()
         )
            {


                ddlBankName.DataSource = dtemp;
                ddlBankName.DataValueField = "BankId";
                ddlBankName.DataTextField = "BankName";
                ddlBankName.DataBind();
                ddlBankName.Items.Insert(0, new ListItem("Please Select a Bank.....", String.Empty));

            }
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {

                hfReimbursmentFormId.Value = Request.QueryString["id"];
                GET_List(Request.QueryString["id"]);
                onRecord(Convert.ToInt32(Request.QueryString["id"]));
                Payamount();

            }



        }
    }


    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)gv_JdBoard.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_JdBoard.Rows[i].Cells[0].FindControl("ckApproved");
            chkBoxRows.Checked = chkBoxHeader.Checked;

            totaltaPaymayment(0);

        }
    }

    protected void ckApproved_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox lb = (CheckBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        //HiddenField MasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfeimbursFromMasterId");
        //DataTable Dt = aSheetDal.Get_FeedBack(MasterId.Value);

        //if (Dt.Rows.Count == 0)
        //{
        //    CheckBox Check = (CheckBox)gv_JdBoard.Rows[rowID].FindControl("ckApproved");
        //    Check.Checked = false;
        //    aMessage.ShowMessageBox("There is no committee feedback for this Application", this);

        //}

        totaltaPaymayment(0);

    }

    private AdvanceBillDal advanceBill = new AdvanceBillDal();

    private void GettotalMark(decimal markTotal)
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            TextBox Amount = (TextBox)GridView1.Rows[i].FindControl("Amount");


            if (Amount.Text == "")
            {
                markTotal = markTotal + 0;
            }
            else
            {
                markTotal = markTotal + Convert.ToDecimal(Amount.Text.ToString());
            }
        }

        Label tst2 = (Label)GridView1.FooterRow.FindControl("lblTotalMark");
        tst2.Text = markTotal.ToString();
    }


    private void totaltaPaymayment(decimal markTotal)
    {
        for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
        {
            TextBox lblNetPayment = (TextBox)gv_JdBoard.Rows[i].FindControl("lblNetPayment");
            var chkBoxRows = (CheckBox)gv_JdBoard.Rows[i].Cells[0].FindControl("ckApproved");

           
            if (lblNetPayment.Text == "")
            {
                markTotal = markTotal + 0;
            }
            else
            {
                if (chkBoxRows.Checked)
                { markTotal = markTotal + Convert.ToDecimal(lblNetPayment.Text.ToString()); }
                
            }
        }

        
        
PayableAmount.Text = markTotal.ToString();
    }
    protected void onRecord(int id)
    {
        DataTable dt = aSettlementDal.Get_EmpInfoForBillSettlement(id);
        DataTable dtadvance = aSettlementDal.Get_AdvanceBill(id);

        if (dt.Rows.Count > 0)
        {

            txtBankName.Text = dt.Rows[0]["BankName"].ToString(); ;
            txtBankAccountNo.Text = dt.Rows[0]["BankAccountNo"].ToString(); ;
            txtBranchName.Text = dt.Rows[0]["BranchName"].ToString(); ;
            txtRoutingNo.Text = dt.Rows[0]["RoutingNo"].ToString(); ;

            lblEmployeeName.Text = dt.Rows[0]["EmpName"].ToString().Trim();

            hfEmpID.Value = dt.Rows[0]["EmpInfoId"].ToString().Trim();

            lblEmpId.Text = dt.Rows[0]["EmpMasterCode"].ToString().Trim();

            deptNameLabel.Text = dt.Rows[0]["DepartmentName"].ToString().Trim();

            desigNameLabel.Text = dt.Rows[0]["Designation"].ToString().Trim();

            try
            {
                OfficailMobile.Text = dt.Rows[0]["OfficialMobile"].ToString().Trim();
            }
            catch (Exception)
            {
                //throw;
            }

            LocationLabel.Text = dt.Rows[0]["Location"].ToString();
            lblPlace.Text = dt.Rows[0]["SalaryLocation"].ToString();

            ReportingLabel.Text = dt.Rows[0]["ReportingToName"].ToString();
            lblRequisitionNo.Text = dt.Rows[0]["RequitisionNo"].ToString();

            hfCompanyId.Value = dt.Rows[0]["CompanyId"].ToString();
            hfFinancialYearId.Value = dt.Rows[0]["FinancialYearId"].ToString();
            lblAdvanceBal.Text = dt.Rows[0]["AdvanceTK"].ToString();

            divAdd.Visible = false;
            decimal advvv = 0;
            try
            {
                advvv = Convert.ToDecimal(lblAdvanceBal.Text);

                if (dtadvance.Rows.Count>0)
                {
                    lblCarry.Text = dtadvance.Rows[0]["CarryPerson"].ToString();
                    lblRemarks.Text = dtadvance.Rows[0]["Remarks"].ToString();
                }
            }
            catch (Exception)
            {
                
                //throw;
            }

            if (advvv>0)
            {
                divAdd.Visible = true;

            }

            if (dt.Rows[0]["Type"].ToString() == "OPD")
            {
                OPD.Checked = true;
                OPDonCheckedChanged(null, null);
            }
            if (dt.Rows[0]["Type"].ToString() == "IPD")
            {
                IPD.Checked = true;
                IPDonCheckedChanged(null, null);
            }


            DataTable claim = advanceBill.Get_ClaimDetailsById(Convert.ToInt32(hfReimbursmentFormId.Value));

            if (claim.Rows.Count > 0)
            {
                GridView1.DataSource = claim;
                GridView1.DataBind();
                decimal markTotal = 0;
                GettotalMark(markTotal);
            }
            else
            {

            }
            DataTable dtDoc = formDal.Get_FormDocumentById(Convert.ToInt32(hfReimbursmentFormId.Value));

            if (dtDoc.Rows.Count > 0)
            {
                ViewState["DocGrid_List"] = dtDoc;
                gv_DocumentUpload.DataSource = dtDoc;
                gv_DocumentUpload.DataBind();
            }

        }
    }

    private ReimbursmentFormDal formDal = new ReimbursmentFormDal();


    public void Payamount()
    {

        Label tst2 = (Label)GridView1.FooterRow.FindControl("lblTotalMark");
        decimal total = 0;
        decimal extra = 0;
        decimal advance = 0;
        
        try
        {
            total = Convert.ToDecimal(tst2.Text);
        }
        catch (Exception)
        {

            //throw;
        }

        if (isExtra.Checked)
        {
            try
            {
                extra = Convert.ToDecimal(txtExtraAmnt.Text);
            }
            catch (Exception)
            {

                // throw;
            }
        }
         
         

        try
        {
            advance = Convert.ToDecimal(lblAdvanceBal.Text);
        }
        catch (Exception)
        {

            //throw;
        }

        decimal res = total - (extra + advance);

        //PayableAmount.Text = res.ToString();
        PayableAmount.Text = 0.ToString();



    }
    public void Remainingbalance()
    {
        string balanceType = "";

        string type = "";

        if (IPD.Checked)
        {
            balanceType = "IPD";
            type = "IPD";
        }
        else if (OPD.Checked)
        {
            balanceType = "OPD";
            type = "OPD";
        }

        if (hfCompanyId.Value != "" && hfFinancialYearId.Value != "" && hfEmpID.Value != "" && balanceType != "")
        {

            DataTable dt = formDal.Get_RemainningBalance_Finance(hfEmpID.Value, hfCompanyId.Value, hfFinancialYearId.Value, balanceType, type);

            if (dt.Rows.Count > 0)
            {

                RemainBalance.Text = string.Empty;
                RemainBalance.Text = dt.Rows[0]["RMBalance"].ToString();
            }

        }
        else
        {
            RemainBalance.Text = string.Empty;
            // ShowMessage();
        }

    }
    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("BillSettlementList.aspx");
    }

    private void ReadOnltDate()
    {
        OPDIPDBalance.Attributes.Add("readonly", "readonly");
        RemainBalance.Attributes.Add("readonly", "readonly");

        SettlementDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
    }


    protected void btnDocUp_OnClick(object sender, EventArgs e)
    {
        if (FUDocument.HasFile)
        {
            string _fileExt = System.IO.Path.GetExtension(FUDocument.FileName);
            string AdsFile = "Reimburs_form_DOC_" + Guid.NewGuid().ToString() + Path.GetExtension(FUDocument.FileName);
            //  fileName = guid.ToString() + imageFileUpload.FileName;
            FUDocument.SaveAs(Server.MapPath("../UploadHealthCareDoc/") + AdsFile);
            HyperLink2.NavigateUrl = "../UploadHealthCareDoc/" + AdsFile;
            //   HyperLink2.Text = "Uploaded Successfully";
        }
        else
        {
            HyperLink2.NavigateUrl = "";
            //   HyperLink2.Text = "No File Uploaded";
        }
    }

    ShowMessage aShowMessage = new ShowMessage();
    private bool docVali()
    {
        lblMsg.Text = "";
        if (hfDocFile.Value == "")
        {
            aShowMessage.ShowMessageBox("Please click Document Upload Button", this);

            return false;
        }
        if (txtSummaryNote.Text == "")
        {
            aShowMessage.ShowMessageBox("Please Enter Summary Note ", this);
            lblMsg.Text = "<b>" + hfDocFileName.Value + "</b> has been uploaded.";
            return false;
        }
        return true;

    }

    private void AddNewDocGrid_List()
    {
        if (ViewState["DocGrid_List"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["DocGrid_List"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();


                string extension;

                extension = Path.GetExtension(hfDocFile.Value);
                //jpg, png,xlsx,pdf,txt,doc,docx
                if (extension == ".jpg" || extension == ".png")
                {
                    drCurrentRow["DocumentLinkPreview"] = "http://localhost:30407/UploadHealthCareDoc/" + hfDocFile.Value;
                }
                else
                {
                    drCurrentRow["DocumentLinkPreview"] = "https://docs.google.com/gview?url=http://localhost:30407/UploadHealthCareDoc/" + hfDocFile.Value + "&embedded=true";
                }

                drCurrentRow["DocumentLink"] = "../UploadMeetingDocument/" + hfDocFile.Value;
                //drCurrentRow["DocumentLink"] =  @"file:///D:/UploadHealthCareDoc/"+ hfDocFile.Value;
                drCurrentRow["FileName"] = hfDocFileName.Value;




                drCurrentRow["DocumentNote"] = txtSummaryNote.Text.Trim();

                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["DocGrid_List"] = dtCurrentTable;

                //Rebind the Grid with the current data to reflect changes   
                gv_DocumentUpload.DataSource = dtCurrentTable;
                gv_DocumentUpload.DataBind();
            }
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("DocumentLink", typeof(string)));
            dt.Columns.Add(new DataColumn("DocumentNote", typeof(string)));
            dt.Columns.Add(new DataColumn("FileName", typeof(string)));
            dt.Columns.Add(new DataColumn("DocumentLinkPreview", typeof(string)));



            dr = dt.NewRow();
            string extension;

            extension = Path.GetExtension(hfDocFile.Value);
            //jpg, png,xlsx,pdf,txt,doc,docx
            if (extension == ".jpg" || extension == ".png")
            {
                dr["DocumentLinkPreview"] = "http://localhost:30407/UploadHealthCareDoc/" + hfDocFile.Value;
            }
            else
            {
                dr["DocumentLinkPreview"] = "https://docs.google.com/gview?url=http://localhost:30407/UploadHealthCareDoc/" + hfDocFile.Value + "&embedded=true";
            }

            dr["DocumentLink"] = "../UploadHealthCareDoc/" + hfDocFile.Value;
            //dr["DocumentLinkPreview"] = "https://docs.google.com/gview?url=http://182.160.103.234:8088/UploadMeetingDocument/" + hfDocFile.Value + "&embedded=true";
            //  dr["DocumentLink"] = @"file:///D:/UploadMeetingDocument/3eec2898121c4467be57981c13852a9e.png"; //@"file:///D:/UploadMeetingDocument/" + hfDocFile.Value;
            dr["FileName"] = hfDocFileName.Value;


            dr["DocumentNote"] = txtSummaryNote.Text.Trim();
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["DocGrid_List"] = dt;

            //Bind the Gridview   
            gv_DocumentUpload.DataSource = dt;
            gv_DocumentUpload.DataBind();
        }
        //Set Previous Data on Postbacks   
        SetDocGrid_List();


        txtSummaryNote.Text = string.Empty;
        // HyperLink2.Text = "No File Uploaded";
        HyperLink2.NavigateUrl = "";
        hfDocFile.Value = "";
    }


    private void SetDocGrid_List()
    {
        int rowIndex = 0;
        if (ViewState["DocGrid_List"] != null)
        {
            DataTable dt = (DataTable)ViewState["DocGrid_List"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField hfDocumentLink = (HiddenField)gv_DocumentUpload.Rows[rowIndex].FindControl("hfDocumentLink");
                    HiddenField hfFileName = (HiddenField)gv_DocumentUpload.Rows[rowIndex].FindControl("hfFileName");
                    HiddenField hfDocumentLinkPreview = (HiddenField)gv_DocumentUpload.Rows[rowIndex].FindControl("hfDocumentLinkPreview");
                    HyperLink HLDocumentLink = (HyperLink)gv_DocumentUpload.Rows[rowIndex].FindControl("HLDocumentLink");
                    Label lbl_DocumentLink = (Label)gv_DocumentUpload.Rows[rowIndex].FindControl("lbl_DocumentLink");

                    Label lbl_DocumentNote = (Label)gv_DocumentUpload.Rows[rowIndex].FindControl("lbl_DocumentNote");


                    if (i < dt.Rows.Count - 1)
                    {
                        hfDocumentLink.Value = dt.Rows[i]["DocumentLink"].ToString();
                        hfFileName.Value = dt.Rows[i]["FileName"].ToString();
                        hfDocumentLinkPreview.Value = dt.Rows[i]["DocumentLinkPreview"].ToString();
                        lbl_DocumentLink.Text = dt.Rows[i]["DocumentLink"].ToString();
                        HLDocumentLink.NavigateUrl = dt.Rows[i]["DocumentLink"].ToString();

                        lbl_DocumentNote.Text = dt.Rows[i]["DocumentNote"].ToString();

                    }

                    rowIndex++;
                }
            }
        }
    }
    protected void brnAddDoc_OnClick(object sender, EventArgs e)
    {
        if (docVali())
        {
            AddNewDocGrid_List();

        }
    }
    private void GET_List(string MasId)
    {
        DataTable Dt = aListDal.Get_Meeting_ListByTopsheetGeneMasId(MasId);

        if (Dt.Rows.Count > 0)
        {
            gv_JdBoard.DataSource = Dt;
            gv_JdBoard.DataBind();
        }

    }

    protected void btnDocRemove_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["DocGrid_List"] != null)
        {
            DataTable dt = (DataTable)ViewState["DocGrid_List"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["DocGrid_List"] = dt;
                //Re bind the GridView for the updated data  
                gv_DocumentUpload.DataSource = dt;
                gv_DocumentUpload.DataBind();
            }
            else
            {
                ViewState["DocGrid_List"] = null;
                //Re bind the GridView for the updated data  
                gv_DocumentUpload.DataSource = null;
                gv_DocumentUpload.DataBind();
            }
        }
        //Set Previous Data on Postbacks  
        SetDocGrid_List();
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



    private bool Validation()
    {


        if (SettlementDate.Text == "")
        {
            aShowMessage.ShowMessageBox("Please Select Settlement Date", this);
            return false;
        }


        //if ((!inlineRadio1.Checked) &&(!inlineRadio2.Checked))
        //{
            
        //    aShowMessage.ShowMessageBox("Please Select Recommended Payment", this);
        //    return false;         
        //}


        if (isExtra.Checked)
        {
            if (txtExtraAmnt.Text == "")
            {
                aShowMessage.ShowMessageBox("Please input Special Allowance", this);
                return false;
            }
        }
        if (PayableAmount.Text == "")
        {
            aShowMessage.ShowMessageBox("Please input payble amount", this);
            return false;
        }

        //if ((!Cash.Checked)&&(!Check.Checked))
        //{
        //    aShowMessage.ShowMessageBox("Please Select Payment Type", this);
        //    return false;
        //}

        Int32 count = 0;

        for (int i = 0; i < gv_JdBoard.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)gv_JdBoard.Rows[i].Cells[0].FindControl("ckApproved");

            if (chkBoxRows.Checked)
            {
                count++;
            }

            if (count > 0)
            {
                break;
            }
        }

        if (count == 0)
        {
            aShowMessage.ShowMessageBox("Please Select at least one Application !!!", this);
           // aShowMessage.ShowMessageBox("Please Input Cash Date", this);
            return false;
        }


        //if (Cash.Checked)
        //{
        //    if (txtCashDate.Text == "")
        //    {
        //        aShowMessage.ShowMessageBox("Please Input Cash Date", this);
        //        return false;
        //    }


         
        //}

        //if (Check.Checked)
        //{
        //    if (CheckDate.Text == "")
        //    {
        //        aShowMessage.ShowMessageBox("Please Select Checked Date", this);
        //        return false;
        //    }


        //    if (ddlBankName.SelectedValue == "")
        //    {
        //        aShowMessage.ShowMessageBox("Please Select  Bank Name", this);
        //        return false;
        //    }

        //    if (txtBankAccountNo.Text == "")
        //    {
        //        aShowMessage.ShowMessageBox("Please Input  Bank Account No", this);
        //        return false;
        //    }
        //}

        return true;
    }




    //savefunction

    protected void save_onclick(object sender, EventArgs e)
    {

        if (Validation())
        {

            BillSettlement aSettlement = new BillSettlement();
            Int32 rei = 0;
            for (int jj = 0; jj < gv_JdBoard.Rows.Count; jj++)
            {
                CheckBox ckApproved = (CheckBox)gv_JdBoard.Rows[jj].FindControl("ckApproved");
                HiddenField hfeimbursFromMasterId = (HiddenField)gv_JdBoard.Rows[jj].FindControl("hfeimbursFromMasterId");
                Label lblType = (Label)gv_JdBoard.Rows[jj].FindControl("lblType");
                Label lblAmount = (Label)gv_JdBoard.Rows[jj].FindControl("lblAmount");
                Label lblAdvanceAmount = (Label)gv_JdBoard.Rows[jj].FindControl("lblAdvanceAmount");
                TextBox lblAddJustmentAmount = (TextBox)gv_JdBoard.Rows[jj].FindControl("lblAddJustmentAmount");
                TextBox lblNetPayment = (TextBox)gv_JdBoard.Rows[jj].FindControl("lblNetPayment");
                aSettlement.BillSettlmentId = 0;
                aSettlement.ReimbursFromMasterId = Convert.ToInt32(hfeimbursFromMasterId.Value);

                aSettlement.SettlementDate = Convert.ToDateTime(SettlementDate.Text);



                if (inlineRadio1.Checked == true && inlineRadio1.Value == "option1")
                {
                    aSettlement.RecommendedPayment = true;
                }

                if (inlineRadio2.Checked == true && inlineRadio2.Value == "option2")
                {
                    aSettlement.RecommendedPayment = false;
                }


                aSettlement.PayableFrom = lblType.Text;
               

                aSettlement.PaybleAmount = Convert.ToDecimal(PayableAmount.Text);
                if (lblAdvanceBal.Text != "")
                {
                    aSettlement.AddvanceTK = Convert.ToDecimal(lblAdvanceAmount.Text);

                }
                aSettlement.IsExtraAllocate = (isExtra.Checked);


                if (lblAddJustmentAmount.Text != "")
                {
                    aSettlement.RemaiingBlnce = Convert.ToDecimal(lblAddJustmentAmount.Text);

                }
                aSettlement.CurrentBlnce = Convert.ToDecimal(lblNetPayment.Text);
                aSettlement.ApplicationAmount = Convert.ToDecimal(lblAmount.Text);


                if (isExtra.Checked)
                {
                    aSettlement.ExtraAllocateTK = Convert.ToDecimal(txtExtraAmnt.Text);

                }

                if (Cash.Checked)
                {
                    aSettlement.PaymentType = Cash.Text;
                    if (txtCashDate.Text != "")
                    {
                        aSettlement.CashDate = Convert.ToDateTime(txtCashDate.Text);
                    }
                    else
                    {
                        aSettlement.CashDate = null;
                    }
                }
                if (Check.Checked)
                {
                    aSettlement.PaymentType = Check.Text;
                    if (ddlBankName.SelectedValue != "")
                    {
                        aSettlement.BankId = Convert.ToInt32(ddlBankName.SelectedValue);
                    }
                    else
                    {
                        aSettlement.BankId = null;
                    }

                    if (txtBankAccountNo.Text != "")
                    {
                        aSettlement.AccountNo = txtBankAccountNo.Text;
                    }
                    else
                    {
                        aSettlement.AccountNo = null;
                    }

                    if (CheckDate.Text != "")
                    {
                        aSettlement.CheckDate = Convert.ToDateTime(CheckDate.Text);


                    }
                    else
                    {
                        aSettlement.CheckDate = null;

                    }
                }
                aSettlement.EntryBy = Convert.ToInt32(Session["UserId"].ToString());
                aSettlement.MainRemarks = txtMainRemarks.Text;


                List<BillSettlementDocument> DocAlist = new List<BillSettlementDocument>();

                for (int i = 0; i < gv_DocumentUpload.Rows.Count; i++)
                {
                    HiddenField hfDocumentLink = (HiddenField) gv_DocumentUpload.Rows[i].FindControl("hfDocumentLink");
                    Label lbl_DocumentNote = (Label) gv_DocumentUpload.Rows[i].FindControl("lbl_DocumentNote");
                    HiddenField hfFileName = (HiddenField) gv_DocumentUpload.Rows[i].FindControl("hfFileName");
                    BillSettlementDocument DocA = new BillSettlementDocument();
                    DocA.DocumentLink = hfDocumentLink.Value.ToString();
                    DocA.FileName = hfFileName.Value.ToString();
                    DocA.DocumentNote = lbl_DocumentNote.Text.Trim();
                    DocA.BillSettlementDocumentId = 0;
                    DocAlist.Add(DocA);
                }



                  rei = aSettlementDal.data_save(aSettlement, DocAlist);
            }

            if (rei > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfully Done...');window.location ='BillSettlementView.aspx';",
                    true);
            }
            else
            {
                aShowMessage.ShowMessageBox("Already Exist", this);
                
            }
        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select Mandatory Data", this);
        }
    }



    protected void OPDonCheckedChanged(object sender, EventArgs e)
    {
        if (OPD.Checked)
        {
            IPD.Checked = false;
            Speical.Checked = false;
            OT.Visible = false;

            Remainingbalance();
        }

    }

  
    protected void IPDonCheckedChanged(object sender, EventArgs e)
    {     
        if (IPD.Checked)
        {
            OPD.Checked = false;
            Speical.Checked = false;
            OT.Visible = false;
            Remainingbalance();
        }
    }

    protected void OtheronCheckedChanged(object sender, EventArgs e)
    {
        if (Speical.Checked)
        {
            IPD.Checked = false;
            OPD.Checked = false;
            OT.Visible = true;
        }
    }

    protected void Cash_OnCheckedChanged(object sender, EventArgs e)
    {
        divCash.Visible = false;
        divCheck.Visible = false;
        if (Cash.Checked)
        {
            Check.Checked = false;
            divCash.Visible = true;
        }
        else
        {
            Cash.Checked = true;
            

        }
       
    }


    protected void Check_OnCheckedChanged(object sender, EventArgs e)
    {
        divCash.Visible = false;
        divCheck.Visible = false;
        if (Check.Checked)
        {
            Cash.Checked = false;
            divCheck.Visible = true;

        }
        else
        {
            Check.Checked = true;
        }
    }


    protected void isExtra_OnCheckedChanged(object sender, EventArgs e)
    {
        if (isExtra.Checked)
        {
            txtExtraAmnt.ReadOnly = false;
            txtExtraAmnt.Text = "";
        }
        else
        {
            txtExtraAmnt.ReadOnly = true;
            txtExtraAmnt.Text = "";
            
        }
        Payamount();
    }

    protected void PayableAmount_OnTextChanged(object sender, EventArgs e)
    {

           Label tst2 = (Label)GridView1.FooterRow.FindControl("lblTotalMark");
        decimal total = 0;
        decimal extra = 0;
        decimal advance = 0;
        decimal payAmount = 0;
        try
        {
            total = Convert.ToDecimal(tst2.Text);
        }
        catch (Exception)
        {
            
            //throw;
        }

        if (isExtra.Checked)
        {
            try
            {
                extra= Convert.ToDecimal(txtExtraAmnt.Text);
            }
            catch (Exception)
            {
                
               // throw;
            }
        }

        try
        {
            advance = Convert.ToDecimal(lblAdvanceBal.Text);
        }
        catch (Exception)
        {
            
            //throw;
        }

        try
        {
           // payAmount = Convert.ToDecimal(PayableAmount.Text);
        }
        catch (Exception)
        {

            //throw;
        }

        decimal res = extra + payAmount + advance;


        decimal rmain = 0;
        try
        {
            rmain = Convert.ToDecimal(RemainBalance.Text);
        }
        catch (Exception)
        {
            
            //throw;
        }

        hfTotalRemainingBlnc.Value = (rmain - res).ToString();

        if (res == total)
        {
            
        }

        else if (res >= total)
        {
            aShowMessage.ShowMessageBox("Payable Amount Can not be greater than Requisition Amount", this);
            PayableAmount.Text = "";
            PayableAmount.Focus();
             
        }
        Payamount();
    }
}