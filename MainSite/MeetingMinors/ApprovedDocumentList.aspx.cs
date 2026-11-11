using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MeetingMinorsDAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using DAO.MeetingMinorsDAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MeetingMinors_ApprovedDocumentList : System.Web.UI.Page
{
    MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            gv_ViewList.UseAccessibleHeader = true;
            gv_ViewList.HeaderRow.TableSection = TableRowSection.TableHeader;
            gv_ViewList.FooterRow.TableSection = TableRowSection.TableFooter;
            gv_ViewList.UseAccessibleHeader = true;
        }
        catch (Exception)
        {
            
            //throw;
        }
        if (!IsPostBack)
        {
           // LoadDropDownList();
            LoadGrid();
        }
    }
    protected void vcchomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    private void LoadDropDownList()
    {
        AMAsterDal.GetCompanyListIntoDropdown(ddlCompany);
        ddlCompany.SelectedIndex = 1;
        ddlCompany_OnSelectedIndexChanged(null, null);

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
    protected void AddNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("MiscellaneousInformation.aspx");
    }

   
    protected void btn_Search_OnClick(object sender, EventArgs e)
    {
     
    }
    ShowMessage aShowMessage = new ShowMessage();



    private string GenerateParamiterList()
    {


        string parameter = " ";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND mas.CompanyId = " + ddlCompany.SelectedValue;
        }

        if (txtTitle.Text != "")
        {
            parameter = parameter + "  and  mas.Title LIKE '%''" + txtTitle.Text.Trim() + "''%'   ";
        }
        if (txtPropuse.Text != "")
        {
            parameter = parameter + "  and  mas.Purpose LIKE '%''" + txtPropuse.Text.Trim() + "''%'   ";
        }
        if (ddlCreatedBy.Text != "")
        {
            parameter = parameter + "  and mas.CreateBy=" + ddlCreatedBy.SelectedValue.Trim() + "  ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.CreateDate BETWEEN '" + txtCreatedDate.Text + "' AND '" + txtToDate.Text + "' ";
        }
        if (txtCreatedDate.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND mas.CreateDate BETWEEN '" + txtCreatedDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (txtCreatedDate.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND mas.CreateDate BETWEEN '" + txtToDate.Text + "' AND '" + txtToDate.Text + "' ";
        }

        if (ddlKeySearch.SelectedValue != "")
        {
            parameter = parameter + "  and  mas.KeySearch LIKE '%" + ddlKeySearch.SelectedValue.Trim() + "%'   ";
        }
        
        return parameter;
    }
    private void LoadGrid()
    {

        DataTable aDataTable = AMAsterDal.LoadInfoApprovedListDone();
            if (aDataTable.Rows.Count > 0)
            {
                gv_ViewList.DataSource = aDataTable;
                gv_ViewList.DataBind();
                gv_ViewList.UseAccessibleHeader = true;
                gv_ViewList.HeaderRow.TableSection = TableRowSection.TableHeader;
                gv_ViewList.FooterRow.TableSection = TableRowSection.TableFooter;
                gv_ViewList.UseAccessibleHeader = true;
            }
            else
            {
                
                gv_ViewList.DataSource = null;
                gv_ViewList.DataBind();
            }
       
    }

    protected void lbReset_OnClick(object sender, EventArgs e)
    {
        
    }


    protected void ddlCompany_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue!="")
        {
            AMAsterDal.GetUserListDropdown(ddlCreatedBy, ddlCompany.SelectedValue);
            if (Session["UserTypeId"].ToString() == "3" ||
                Session["UserTypeId"].ToString() == "4")
            {
                ddlCreatedBy.Enabled = true;
                AMAsterDal.GetMiscellaneousKeySearchDropdown(ddlKeySearch, ddlCompany.SelectedValue);
            }
            else
            {
                ddlCreatedBy.SelectedValue = Session["UserId"].ToString();
                ddlCreatedBy.Enabled = false;
                AMAsterDal.GetMiscellaneousKeySearchDropdown(ddlKeySearch, ddlCompany.SelectedValue, Session["UserId"].ToString());

                
            }
          

        }
        else
        {
            ddlCreatedBy.Items.Clear();
        }
    }

    protected void btnView_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        Session["Status"] = "View";
        HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfMiscellaneousInfoId");
        Response.Redirect("MiscellaneousInformationViewDetails.aspx?MID=" + mastrId.Value.Trim());
    }
    protected void chkIsEdit_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;

        CheckBox chkIsEdit = ((CheckBox)gv_Details_Save.Rows[rowIndex].FindControl("chkIsEdit"));

        HiddenField hfCanEdit = ((HiddenField)gv_Details_Save.Rows[rowIndex].FindControl("hfCanEdit"));


        hfCanEdit.Value = chkIsEdit.Checked.ToString();

    }
    protected void chkMimimumCount_OnCheckedChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((CheckBox)sender).Parent.Parent)).RowIndex;

        CheckBox chkMimimumCount = ((CheckBox)gv_Details_Save.Rows[rowIndex].FindControl("chkMimimumCount"));

        HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_Details_Save.Rows[rowIndex].FindControl("hfIsMinimumApprovalPerson"));


        hfIsMinimumApprovalPerson.Value = chkMimimumCount.Checked.ToString();
    }
    protected void chkNotificationApp_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((CheckBoxList)sender).Parent.Parent)).RowIndex;

        CheckBoxList chkNotificationApp =
            ((CheckBoxList)gv_Details_Save.Rows[rowIndex].FindControl("chkNotification"));

        HiddenField HiNotificationEmailApp =
            ((HiddenField)gv_Details_Save.Rows[rowIndex].FindControl("HiNotificationEmail"));
        HiddenField hfNotificationSMSApp =
            ((HiddenField)gv_Details_Save.Rows[rowIndex].FindControl("hfNotificationSMS"));

        HiNotificationEmailApp.Value = chkNotificationApp.Items[0].Selected.ToString();
        hfNotificationSMSApp.Value = chkNotificationApp.Items[1].Selected.ToString();


    }

    protected void btnEdit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfMiscellaneousInfoId");
        id_mastetID.Value = mastrId.Value;
        DataTable dtDetailos = AMAsterDal.GetDelsDataById(mastrId.Value);
        if (dtDetailos.Rows.Count > 0)
        {
            ViewState["gv_Details_List"] = dtDetailos;
            gv_Details_Save.DataSource = dtDetailos;
            gv_Details_Save.DataBind();


            for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
            {
                DropDownList ddlSequenceList = (DropDownList)gv_Details_Save.Rows[i].FindControl("ddlSequenceList");


                ddlSequenceList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select One.....", String.Empty));
                for (int k = 1; k < gv_Details_Save.Rows.Count + 1; k++)
                {
                    ddlSequenceList.Items.Insert(k, new ListItem(k.ToString()));
                }
            }

            for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
            {
                DropDownList ddlSequenceList = (DropDownList)gv_Details_Save.Rows[i].Cells[0].FindControl("ddlSequenceList");
                HiddenField hfSeq_No = (HiddenField)gv_Details_Save.Rows[i].Cells[0].FindControl("hfSeq_No");

                ddlSequenceList.SelectedValue = hfSeq_No.Value;




                HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfIsMinimumApprovalPerson"));
                HiddenField hfCanEdit = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfCanEdit"));
                HiddenField HiNotificationEmailApp = ((HiddenField)gv_Details_Save.Rows[i].FindControl("HiNotificationEmail"));
                HiddenField hfNotificationSMSApp = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfNotificationSMS"));


                CheckBox chkMimimumCount = (CheckBox)gv_Details_Save.Rows[i].Cells[0].FindControl("chkMimimumCount");

                if (hfIsMinimumApprovalPerson.Value != "")
                {

                    chkMimimumCount.Checked = Convert.ToBoolean(hfIsMinimumApprovalPerson.Value);

                }


                CheckBox chkIsEdit = (CheckBox)gv_Details_Save.Rows[i].Cells[0].FindControl("chkIsEdit");

                if (hfCanEdit.Value != "")
                {
                    chkIsEdit.Checked = Convert.ToBoolean(hfCanEdit.Value);

                }
                CheckBoxList chkNotificationApp = (CheckBoxList)gv_Details_Save.Rows[i].Cells[0].FindControl("chkNotification");


                if (HiNotificationEmailApp.Value != "")
                {


                    chkNotificationApp.Items[0].Selected = Convert.ToBoolean(HiNotificationEmailApp.Value);


                }

                if (hfNotificationSMSApp.Value != "")
                {


                    chkNotificationApp.Items[1].Selected = Convert.ToBoolean(hfNotificationSMSApp.Value);






                }

            }
        }
        MPAppraisalApproval.Show();
        
    }

    protected void btnRemove_OnClick(object sender, EventArgs e)
    {


         
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;


        Session["Status"] = "Delete";
        HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfMiscellaneousInfoId");
        Response.Redirect("MiscellaneousInformation.aspx?MID=" + mastrId.Value.Trim());

    }

    protected void btnApprasalResetClose_Click(object sender, EventArgs e)
    {
        MPAppraisalApproval.Hide();

    }

    protected void lbCancelAppraisalSubmit_OnClick(object sender, EventArgs e)
    {

        MiscellaneousInfoDAO aMaster = new MiscellaneousInfoDAO();

        aMaster.MiscellaneousInfoId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);
        List<MiscellaneousInfoRoutingPathDAO> RoutingPath = new List<MiscellaneousInfoRoutingPathDAO>();

        int _RefEmpId = 0;
        int _RefSeqNo = 0;
        int _RefMinAppCount = 0;

        for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
        {
            CheckBox chkMimimumCount = (CheckBox) gv_Details_Save.Rows[i].FindControl("chkMimimumCount");
            DropDownList ddlSequenceList = (DropDownList) gv_Details_Save.Rows[i].FindControl("ddlSequenceList");


            HiddenField ShfEmpInfoId = (HiddenField) gv_Details_Save.Rows[i].FindControl("ShfEmpInfoId");


            CheckBox chkIsEdit = (CheckBox) gv_Details_Save.Rows[i].FindControl("chkIsEdit");
            CheckBoxList chkNotificationApp =
                (CheckBoxList) gv_Details_Save.Rows[i].FindControl("chkNotification");

            MiscellaneousInfoRoutingPathDAO Routing = new MiscellaneousInfoRoutingPathDAO();


            Routing.IsMinimumApprovalPerson = chkMimimumCount.Checked;
            Routing.Seq_No = Convert.ToInt32(ddlSequenceList.SelectedValue);
            if (chkMimimumCount.Checked)
            {
                _RefMinAppCount++;
                if (Routing.Seq_No == 1)
                {
                    _RefEmpId = Convert.ToInt32(ShfEmpInfoId.Value.Trim());
                    _RefSeqNo = (int) Routing.Seq_No;
                }
            }

            Routing.EmpInfoId = Convert.ToInt32(ShfEmpInfoId.Value.Trim());

            Routing.CanEdit = chkIsEdit.Checked;

            Routing.IsEmailNotification = chkNotificationApp.Items[0].Selected;
            if (Routing.Seq_No == 1)
            {
                SenMailForApprved(Convert.ToInt32(Routing.EmpInfoId),
                    " Document Approval System (DAS) ",
                    @"  <br/> Dear Sir, <br/> 
A document is waiting for your approval in the system.
To login into the system please click the below link.<br/>  http://182.160.103.234:8088/
" + "<br/>Thank You.  ");
            }

            Routing.IsSMSNotification = chkNotificationApp.Items[1].Selected;

            RoutingPath.Add(Routing);
        }

        aMaster.RefEmpId = _RefEmpId;
        aMaster.RefSeqNo = _RefSeqNo;
        aMaster.RefMinAppCount = _RefMinAppCount;

        aMaster.RefMinAppCountCheck = 0;
    
        if (gv_Details_Save.Rows.Count == 0)
        {
            aMaster.Isapproved = true;
            aMaster.RefEmpId = 0;
            aMaster.ActionStatus = "Approved";
        }
        else
        {
            aMaster.ActionStatus = "Initiator";
        }
        bool status = AMAsterDal.UpdateApprovalMasterforNotIsMinApprovalPersonByIdForNew(aMaster);
    if (status)
        {
            AMAsterDal.SaveRoutingPathDetails(RoutingPath, aMaster.MiscellaneousInfoId);


            try
            {
                if (Session["EmpInfoId"].ToString() != "")
                {




                    MiscellaneousInfoAppLogDAO appLogDaoa = new MiscellaneousInfoAppLogDAO();

                    appLogDaoa.ActionStatus = "Drafted";
                    appLogDaoa.ApprovedDate = DateTime.Now;
                    appLogDaoa.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                    appLogDaoa.PreEmpInfoId = Convert.ToInt32(0);
                    appLogDaoa.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                    appLogDaoa.MiscellaneousInfoId = aMaster.MiscellaneousInfoId;

                    appLogDaoa.Comments = "";


                    int idd = AMAsterDal.SavAppLog(appLogDaoa);


                    //DataTable dtempdata =
                    //    AMAsterDal.GetEmpRoutingPath(pk.ToString());
                    MiscellaneousInfoAppLogDAO appLogDao = new MiscellaneousInfoAppLogDAO();
                    {



                        appLogDao.ActionStatus = "Initiator";
                        appLogDao.ApprovedDate = DateTime.Now;
                        appLogDao.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(_RefEmpId);
                        appLogDao.MiscellaneousInfoId = aMaster.MiscellaneousInfoId;

                        appLogDao.Comments = "";


                    }

                    int iddddd = AMAsterDal.SavAppLog(appLogDao);
                    //                            SenMailForApprved(appLogDao.ForEmpInfoId, " Increment Approval ", @"  <br/> Dear Sir, <br/>
                    //An Increment is waiting for your approval. <br/><br/>
                    // please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
                    //");


                }
            }
            catch (Exception)
            {

                //throw;
            }
           
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='ApprovedDocumentList.aspx';",
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

    protected void btn_DetailsRemove_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["gv_Details_List"] != null)
        {
            DataTable dt = (DataTable)ViewState["gv_Details_List"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["gv_Details_List"] = dt;
                //Re bind the GridView for the updated data  
                gv_Details_Save.DataSource = dt;
                gv_Details_Save.DataBind();


                for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
                {
                    DropDownList ddlSequenceList = (DropDownList)gv_Details_Save.Rows[i].FindControl("ddlSequenceList");


                    ddlSequenceList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select One.....", String.Empty));
                    for (int k = 1; k < gv_Details_Save.Rows.Count + 1; k++)
                    {
                        ddlSequenceList.Items.Insert(k, new ListItem(k.ToString()));
                    }
                }

                for (int i = 0; i < gv_Details_Save.Rows.Count; i++)
                {
                    DropDownList ddlSequenceList = (DropDownList)gv_Details_Save.Rows[i].Cells[0].FindControl("ddlSequenceList");
                    HiddenField hfSeq_No = (HiddenField)gv_Details_Save.Rows[i].Cells[0].FindControl("hfSeq_No");

                    ddlSequenceList.SelectedValue = hfSeq_No.Value;




                    HiddenField hfIsMinimumApprovalPerson = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfIsMinimumApprovalPerson"));
                    HiddenField hfCanEdit = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfCanEdit"));
                    HiddenField HiNotificationEmailApp = ((HiddenField)gv_Details_Save.Rows[i].FindControl("HiNotificationEmail"));
                    HiddenField hfNotificationSMSApp = ((HiddenField)gv_Details_Save.Rows[i].FindControl("hfNotificationSMS"));


                    CheckBox chkMimimumCount = (CheckBox)gv_Details_Save.Rows[i].Cells[0].FindControl("chkMimimumCount");

                    if (hfIsMinimumApprovalPerson.Value != "")
                    {
                        chkMimimumCount.Checked = false;
                        if (hfIsMinimumApprovalPerson.Value == "0")
                        {
                            chkMimimumCount.Checked = false;

                        }
                        else
                        {
                            chkMimimumCount.Checked = true;

                        }

                    }


                    CheckBox chkIsEdit = (CheckBox)gv_Details_Save.Rows[i].Cells[0].FindControl("chkIsEdit");

                    if (hfCanEdit.Value != "")
                    {
                        chkIsEdit.Checked = false;

                        if (hfCanEdit.Value == "0")
                        {
                            chkIsEdit.Checked = false;

                        }
                        else
                        {
                            chkIsEdit.Checked = true;

                        }

                    }
                    CheckBoxList chkNotificationApp = (CheckBoxList)gv_Details_Save.Rows[i].Cells[0].FindControl("chkNotification");


                    if (HiNotificationEmailApp.Value != "")
                    {

                        if (HiNotificationEmailApp.Value == "0")
                        {
                            chkNotificationApp.Items[0].Selected = false;

                        }
                        else
                        {
                            chkNotificationApp.Items[0].Selected = true;

                        }

                    }

                    if (hfNotificationSMSApp.Value != "")
                    {

                        if (hfNotificationSMSApp.Value == "0")
                        {
                            chkNotificationApp.Items[1].Selected = false;

                        }
                        else
                        {
                            chkNotificationApp.Items[1].Selected = true;

                        }




                    }

                }
            }
            else
            {
                ViewState["gv_Details_List"] = null;
                //Re bind the GridView for the updated data  
                gv_Details_Save.DataSource = null;
                gv_Details_Save.DataBind();
            }
        }

       
    }

    protected void btnReturned_OnClick(object sender, EventArgs e)
    {

       

        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        HiddenField mastrId = (HiddenField)gv_ViewList.Rows[rowID].FindControl("hfMiscellaneousInfoId");
        TextBox txtComments = (TextBox)gv_ViewList.Rows[rowID].FindControl("txtComments");
        Label lbl_Title = (Label)gv_ViewList.Rows[rowID].FindControl("lbl_Title");
        id_mastetID.Value = mastrId.Value;
        hfSubject.Value = lbl_Title.Text;
        if (txtComments.Text!="")
        {

       
         DataTable dtMaster = AMAsterDal.GetMasterDataById(id_mastetID.Value);
        if (dtMaster.Rows.Count > 0)
        {
            hfSeqNo.Value = dtMaster.Rows[0]["RefSeqNo"].ToString();
        }
        try
        {
            int pk = Convert.ToInt32(id_mastetID.Value);
            if (Session["EmpInfoId"].ToString() != "")
            {




 





                //if (dtempdata.Rows.Count > 0)
                //{

                //    DataTable dtIsApptove =
                // AMAsterDal.GetCheckMinimumApproval(pk.ToString());
                //    bool isAppPerson = false;

                //    if (dtIsApptove.Rows.Count > 0)
                //    {
                //        isAppPerson = Convert.ToBoolean(dtIsApptove.Rows[0]["IsMinimumApprovalPerson"].ToString());
                //    }
                //    bool status = false;
                //    if (isAppPerson)
                //    {
                //        MiscellaneousInfoDAO aMaster = new MiscellaneousInfoDAO();

                //        aMaster.MiscellaneousInfoId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);


                //        aMaster.RefEmpId = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                //        ;
                //        aMaster.RefSeqNo = Convert.ToInt32(dtempdata.Rows[0]["Seq_No"].ToString());
                //        ;

                //        aMaster.Isapproved = false;
                //        aMaster.ActionStatus = "Return";

                //        status = AMAsterDal.UpdateApprovalMasterReturrnById(aMaster);
                //    }
                //    else
                //    {
                //        MiscellaneousInfoDAO aMaster = new MiscellaneousInfoDAO();

                //        aMaster.MiscellaneousInfoId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);


                //        aMaster.RefEmpId = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                //        ;
                //        aMaster.RefSeqNo = Convert.ToInt32(dtempdata.Rows[0]["Seq_No"].ToString());
                //        ;

                //        aMaster.Isapproved = false;
                //        aMaster.ActionStatus = "Return";

                //        status = AMAsterDal.UpdateApprovalMasterforNotIsMinApprovalPersonById(aMaster);
                //    }

                //    if (status)
                //    {
                //        MiscellaneousInfoAppLogDAO appLogDao = new MiscellaneousInfoAppLogDAO();
                //        {



                //            appLogDao.ActionStatus = "Return";
                //            appLogDao.ApprovedDate = DateTime.Now;
                //            appLogDao.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                //            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                //            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                //            appLogDao.MiscellaneousInfoId = pk;

                //            appLogDao.Comments = commentsTextBox.Text.Trim();


                //        }
                //        int iddddd = AMAsterDal.SavAppLog(appLogDao);
                //    }



                //    ScriptManager.RegisterStartupScript(this, this.GetType(),
                //             "alert",
                //             "alert('Operation Successful...');window.location ='MiscellaneousInformationApprovalList.aspx';",
                //             true);
                //}
                //else
                {
                    MiscellaneousInfoDAO aMaster = new MiscellaneousInfoDAO();

                    aMaster.MiscellaneousInfoId = id_mastetID.Value == "" ? 0 : Convert.ToInt32(id_mastetID.Value);

                    aMaster.ActionStatus = "Returned";
                    aMaster.RefEmpId = 0;
                    aMaster.RefSeqNo = 0;
                    aMaster.RefMinAppCount = 0;
                    aMaster.ReturnComments = txtComments.Text.Trim();
                    bool status = AMAsterDal.UpdateApprovalMasterByIdForReturn(aMaster);




                    MiscellaneousInfoAppLogDAO appLogDao = new MiscellaneousInfoAppLogDAO();
                    {



                        appLogDao.ActionStatus = "Returned";
                        appLogDao.ApprovedDate = DateTime.Now;
                        appLogDao.ApprovedBy = Convert.ToInt32(Session["UserId"].ToString());
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = 0;
                        appLogDao.MiscellaneousInfoId = pk;

                        appLogDao.Comments = txtComments.Text.Trim();


                    }
                    int iddddd = AMAsterDal.SavAppLog(appLogDao);

                    if (status)
                    {
                        DataTable dtEntryBy = AMAsterDal.GetEmpEntryBy(pk.ToString());

                        SenMailForApprved(Convert.ToInt32(dtEntryBy.Rows[0]["EmpInfoId"].ToString()), hfSubject.Value.Trim(), @" <br/> <br/> Dear Sir, <br/> 
Document is submitted for your Recommendation/Approval in the system.
To login, click the below link.<br/>  http://182.160.103.234:8088/
" + "<br/>Thank You.   ");

                        DataTable dtLoop =
                      AMAsterDal.GetEmpRoutingPathRreturn(pk.ToString(), hfSeqNo.Value);


                        for (int i = 0; i < dtLoop.Rows.Count; i++)
                        {


                            SenMailForApprved(Convert.ToInt32(dtLoop.Rows[i]["EmpInfoId"].ToString()), hfSubject.Value.Trim(), @" <br/> <br/> Dear Sir, <br/> 
                         Document is submitted for your Recommendation/Approval in the system.
To login, click the below link.<br/>  http://182.160.103.234:8088/
" + "<br/>Thank You.   ");

                        }

                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                                 "alert",
                                 "alert('Operation Successful...');window.location ='ApprovedDocumentList.aspx';",
                                 true);
                    }
                }




                //                SenMailForApprved(appLogDao.ForEmpInfoId, " Increment Approval ", @"  <br/> Dear Sir, <br/>
                //                An Increment is waiting for your approval. <br/><br/>
                //                 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
                //                ");


            }
        }
        catch (Exception)
        {

            //throw;
        }
        }
        else
        {
            AlertMessageBoxShow("Please Enter Comments!!");
        }
    }
}