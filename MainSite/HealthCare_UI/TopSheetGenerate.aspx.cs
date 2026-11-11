using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.DataManager;
using DAL.HealthCare_DAL;
using DAL.Panal_DAL;
using DAO.HealthCare_Dao;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using Microsoft.Ajax.Utilities;

public partial class HealthCare_UI_TopSheetGenerate : System.Web.UI.Page
{
    private HealthCareListDal aListDal = new HealthCareListDal();
    private TopSheetDal aSheetDal = new TopSheetDal();
    private ShowMessage aMessage = new ShowMessage();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
          //  Load_Initail_DD_List();

            GET_List();
            // Time.Text = DateTime.Now.ToString("hh:mm:ss");
            txtDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

        }
    }

    private void Load_Initail_DD_List()
    {
       // aListDal.GetDDLCompany(ddlCompany);
      //  ddlCompany.SelectedValue = 1.ToString();
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
            HiddenField HFSalaryLocaion = (HiddenField)gv_JdBoard.Rows[i].FindControl("HFSalaryLocaion");
            Label employfee = (Label)gv_JdBoard.Rows[i].FindControl("employfee");
            if (string.IsNullOrEmpty(HFSalaryLocaion.Value))
            {
                aMessage.ShowMessageBox(employfee.Text + " for this employee Office Missing!", this);
                chkBoxRows.Checked = false;
            }
            else
            {
                chkBoxRows.Checked = chkBoxHeader.Checked;
            }
            
        }
    }
    private HRPanelDal aPanelDal = new HRPanelDal();
    private void GET_List()
    {
        DataTable Dt = new DataTable();
        DataTable Dt2 = new DataTable();
        

      


            DataTable dtPer = new DataTable();
        DataTable dtCom = new DataTable();

        try
        {
            dtPer = _commonDataLoad.GetblHCTwoComPermisionHC(" and Md.IsForward=1");
        }
        catch (Exception)
        {
            // Optionally log error
        }

        if (dtPer.Rows.Count > 0)
        {
            dtCom = _commonDataLoad.GetCompanyAllHC();
        }
        else
        {
            dtCom = _commonDataLoad.GetCompany();
        }


        int com4 = 0;
        List<string> validCompanyIds = new List<string>();
        string condition = "";
        string condition4 = "";

        for (int ik = 0; ik < dtCom.Rows.Count; ik++)
        {
            int companyId = Convert.ToInt32(dtCom.Rows[ik]["Value"].ToString());

            if (companyId == -1)
                continue;

            if (companyId == 1 || companyId == 2)
            {
                validCompanyIds.Add(companyId.ToString());
            }
            else
            {
                com4 = companyId;
            } 
        }

        // after the loop, check if we have any IDs and build the condition
        if (validCompanyIds.Count > 0)
        {
            string joinedIds = string.Join(",", validCompanyIds);
            condition = " and M.CompanyId IN (" + joinedIds + ") and EMP.HealthcareCompanyId is null";
        }



        if (com4>0)
        {
            condition4 = " and EMP.HealthcareCompanyId = 4";
        }


     


            Dt = aListDal.Get_All_Employee_List(condition + " and EMP.HealthcareCompanyId is null AND Emp.SalaryLoationId IN (Select  DISTINCT ISNULL(SalaryLoationId,0) from tblCommiteeSetupMaster M left join tblCommiteeSetupDetails D ON M.ComSetupMasId= D.ComSetupMasId Where IsForward=1  And EmpInfoId IN(" + Session["EmpInfoId"].ToString() + ") )");

            if (com4 == 4)
            {
                Dt2 = aListDal.Get_All_Employee_List(condition4 + " and EMP.HealthcareCompanyId =4 AND Emp.SalaryLoationId IN (Select  DISTINCT ISNULL(SalaryLoationId,0) from tblCommiteeSetupMaster M left join tblCommiteeSetupDetails D ON M.ComSetupMasId= D.ComSetupMasId Where IsForward=1  And EmpInfoId IN(" + Session["EmpInfoId"].ToString() + ") )");

                if (Dt2 != null && Dt2.Rows.Count > 0)
                {
                    Dt.Merge(Dt2); // Merges Dt2 into Dt
                }
            }
      
        if (Dt.Rows.Count > 0)
        {
            for (int i = Dt.Rows.Count - 1; i >= 0; i--) // reverse loop for safe removal
            {
                string hfSalaryLoationId = Dt.Rows[i]["SalaryLoationId"].ToString();
                string hfApplicationType = Dt.Rows[i]["Type"].ToString();

                bool isValid = false;

                for (int ik = 0; ik < dtCom.Rows.Count; ik++)
                {
                    int companyId = Convert.ToInt32(dtCom.Rows[ik]["Value"].ToString());

                    if (companyId == -1)
                    {
                        continue; // skip invalid companyId
                    }

                    DataTable DtCheck = aPanelDal.Get_HRPanelCheckCommetiCompany(
                        hfApplicationType,
                        hfSalaryLoationId,
                        companyId);

                    // Valid data found, keep this row
                    if (DtCheck.Rows.Count > 0)
                    {
                        isValid = true;
                        break;
                    }
                }

                // If not valid, then remove
                if (!isValid)
                {
                    Dt.Rows.RemoveAt(i);
                }
            }

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

        if (txtMeetingTitle.Text == "")
        {
            aMessage.ShowMessageBox("Please Add Meeting Title..!!", this);
            txtMeetingTitle.Focus();
            return false;
        }

        if (txtMeetingNo.Text == "")
        {
            aMessage.ShowMessageBox("Please Select Meeting..!!",this);
            txtMeetingNo.Focus();
            return false;
        }

        if (txtDate.Text == "")
        {
            aMessage.ShowMessageBox("Please Select Date..!!", this);
            txtDate.Focus();
            return false;
        }

        if (txtVenue.Text == "")
        {
            aMessage.ShowMessageBox("Please Input Venue Name..!!", this);
            txtVenue.Focus();
            return false;
        }

        if (txtTime.Text == "")
        {
            aMessage.ShowMessageBox("Please Select Time..!!", this);
            txtTime.Focus();
            return false;
        }

        if (gv_JdBoard.Rows.Count == 0)
        {
            aMessage.ShowMessageBox("Please Add Application Information!!", this);
            txtTime.Focus();
            return false;
        }

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
            aMessage.ShowMessageBox("Please Select At least One",this);
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

            if (Id>0)
            {
                SenMailForApprved(Id, "IPD/OPD Disbursement", @"  <br> Dear Sir, <br/>
               I am writing to inform you about an upcoming important meeting regarding the IPD/OPD disbursement.<br/><br/>
               Meeting Title: " + txtMeetingTitle.Text + "" + " <br/><br/> Reference:" + txtMeetingNo.Text + "<br/><br/> Venue: " + txtVenue.Text + "" + "<br/><br/> Date: " + txtDate.Text + "" + "<br/><br/> Time:" + txtTime.Text + ""
                                                             + "<br/><br/> please login for the details from the below link.<br/><br/> http://182.160.103.234:8088/");
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfully Done...');window.location ='TopSheetGenerateView.aspx';",
                    true);
            }
        }
    }

    private void SenMailForApprved(int Id,string mSubject, string mBody)
    {
        MailMessage mail = new MailMessage();
        SmtpClient smtpServer = new SmtpClient("shuvosmtp.office365.com");

        // Set the sender email and application password
        string senderEmail = "shuvono-reply@smc-bd.org"; // Replace with your actual sender email address
        string appPassword = "vfwzmbxprdmqhhln"; // Replace with your actual application password

        DataTable aMasterTable = new DataTable();
        aMasterTable = aSheetDal.Get_TopSheet_RPT(Convert.ToInt32(Id));
        
        string CompanyId = aMasterTable.Rows[0]["CompanyId"].ToString(); ;
        string ApplicationType = aMasterTable.Rows[0]["Type"].ToString(); ;
        string SalaryLoationId = aMasterTable.Rows[0]["SalaryLoationId"].ToString(); ;

                        TopSheetDal aDal = new TopSheetDal();
                        DataTable aTable = aDal.Get_Member_RPT(ApplicationType, CompanyId, SalaryLoationId);
                        DataTable dt = aDal.Get_Convenor_MemberSecretory_RPT_mail(ApplicationType, CompanyId, SalaryLoationId);

                        // Member_Mail_All
                        #region MemberMail
                        if (aTable.Rows.Count > 0)
                        {
                            for (int j = 0; j < aTable.Rows.Count; j++)
                            {
                                string ForMailAddress = "";
                                using (var db = new HRIS_SMCEntities())
                                {
                                    string ConvenorID_ = aTable.Rows[j]["ConvenorID"].ToString();

                                    var GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                                          where t.EmpMasterCode == ConvenorID_
                                                          select t).FirstOrDefault();
                                    if (GetMailAddress != null)
                                    {
                                        ForMailAddress = GetMailAddress.OfficialEmail;
                                    }
                                }
                                if (ForMailAddress != "")
                                {
                                    System.Threading.Thread.Sleep(100);

                                    
                                    
                                    mail.From = new MailAddress(senderEmail);
                                    try
                                    {
                                        mail.To.Add(ForMailAddress.Trim());
                                    }
                                    catch (Exception)
                                    {
                                        //throw;
                                    }
                                    mail.Subject = mSubject;
                                    mail.Body =
                                        "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +
                                        WebUtility.HtmlDecode(mBody)
                                        +
                                        "</div>";

                    //Attach file using FileUpload Control and put the file in memory stream



                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;

                    // Set the SMTP client properties
                    smtpServer.Port = 587;
                    smtpServer.Credentials = new NetworkCredential(senderEmail, appPassword);
                    smtpServer.EnableSsl = true;

                    try
                                    {
                        smtpServer.Send(mail);
                                    }
                                    catch (System.Net.Mail.SmtpException ex)
                                    {
                                        // showMessageBox("Email has not Sent, Try Once More time");
                                    }
                                    catch (Exception exe)
                                    {
                                        //  showMessageBox("Email has not Sent, Try Once More time");
                                    }

                                    System.Threading.Thread.Sleep(100);
                                }

                            }
                        }

                        #endregion
                        
                        if (dt.Rows.Count > 0)
                        {
                            #region DoctorMail
                            string ForMailAddressdoc = "";
                            using (var db = new HRIS_SMCEntities())
                            {

                                string ConvenorID = dt.Rows[0]["ConvenorID"].ToString();

                                var GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                                      where t.EmpMasterCode == ConvenorID
                                                      select t).FirstOrDefault();

                                if (GetMailAddress != null)
                                {
                                    ForMailAddressdoc = GetMailAddress.OfficialEmail;
                                   
                                }
                            }

                            if (ForMailAddressdoc != "")
                            {
                                System.Threading.Thread.Sleep(100);

                mail.From = new MailAddress(senderEmail);
                try
                                {
                                    mail.To.Add(ForMailAddressdoc.Trim());
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                                mail.Subject = mSubject;
                                mail.Body =
                                    "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +
                                    WebUtility.HtmlDecode(mBody)
                                    +
                                    "</div>";

                //Attach file using FileUpload Control and put the file in memory stream


                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                // Set the SMTP client properties
                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential(senderEmail, appPassword);
                smtpServer.EnableSsl = true;
                try
                                {
                    smtpServer.Send(mail);
                                }
                                catch (System.Net.Mail.SmtpException ex)
                                {
                                    // showMessageBox("Email has not Sent, Try Once More time");
                                }
                                catch (Exception exe)
                                {
                                    //  showMessageBox("Email has not Sent, Try Once More time");
                                }

                                System.Threading.Thread.Sleep(100);
                            }

                            #endregion

                            #region MemberSecretary

                            string ForMailAddressMsec = "";
                            using (var db = new HRIS_SMCEntities())
                            {

                                string MemberSecretoryID = dt.Rows[0]["MemberSecretoryID"].ToString();

                                var GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                                      where t.EmpMasterCode == MemberSecretoryID
                                                      select t).FirstOrDefault();

                                if (GetMailAddress != null)
                                {
                                    ForMailAddressMsec = GetMailAddress.OfficialEmail;

                                  
                                }
                            }

                            if (ForMailAddressMsec != "")
                            {
                                System.Threading.Thread.Sleep(100);

                mail.From = new MailAddress(senderEmail);
                try
                                {
                                    mail.To.Add(ForMailAddressMsec.Trim());
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                                mail.Subject = mSubject;
                                mail.Body =
                                    "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +
                                    WebUtility.HtmlDecode(mBody)
                                    +
                                    "</div>";

                //Attach file using FileUpload Control and put the file in memory stream

                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                // Set the SMTP client properties
                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential(senderEmail, appPassword);
                smtpServer.EnableSsl = true;


                try
                {
                    smtpServer.Send(mail);
                                }
                                catch (System.Net.Mail.SmtpException ex)
                                {
                                    // showMessageBox("Email has not Sent, Try Once More time");
                                }
                                catch (Exception exe)
                                {
                                    //  showMessageBox("Email has not Sent, Try Once More time");
                                }

                                System.Threading.Thread.Sleep(100);
                            }

                            #endregion

                            #region Convenermail
                            string ForMailAddressConve = "";
                            using (var db = new HRIS_SMCEntities())
                            {

                                string convenorName = dt.Rows[0]["ConvenorName"].ToString();


                                var GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                                      where t.EmpMasterCode == convenorName
                                                      select t).FirstOrDefault();
                                if (GetMailAddress != null)
                                {
                                    ForMailAddressConve = GetMailAddress.OfficialEmail;
                                    
                                }
                            }
                            if (ForMailAddressConve != "")
                            {
                                System.Threading.Thread.Sleep(100);


                mail.From = new MailAddress(senderEmail);
                try
                                {
                                    mail.To.Add(ForMailAddressConve.Trim());
                                }
                                catch (Exception)
                                {
                                    //throw;
                                }
                                mail.Subject = mSubject;
                                mail.Body =
                                    "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +
                                    WebUtility.HtmlDecode(mBody)
                                    +
                                    "</div>";

                //Attach file using FileUpload Control and put the file in memory stream

                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                // Set the SMTP client properties
                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential(senderEmail, appPassword);
                smtpServer.EnableSsl = true;

                try
                                {
                    smtpServer.Send(mail);
                                }
                                catch (System.Net.Mail.SmtpException ex)
                                {
                                    // showMessageBox("Email has not Sent, Try Once More time");
                                }
                                catch (Exception exe)
                                {
                                    //  showMessageBox("Email has not Sent, Try Once More time");
                                }
                                System.Threading.Thread.Sleep(100);
                            }

                            #endregion
                        }
    }

    private TopSheetGeneMasterDao MasterDataPrepareForSave()
    {
        TopSheetGeneMasterDao aMasterDao = new TopSheetGeneMasterDao();
        aMasterDao.TopsheetGeneMasId = 0;
        aMasterDao.MeetingNo = txtMeetingNo.Text;
        aMasterDao.MeetingTitle = txtMeetingTitle.Text;
        aMasterDao.Venue = txtVenue.Text;
        aMasterDao.MeetingDate = Convert.ToDateTime(txtDate.Text);
        aMasterDao.MeetingTime = TimeSpan.Parse(txtTime.Text);
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

        HiddenField HFSalaryLocaion = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("HFSalaryLocaion");
        Label employfee = (Label)gv_JdBoard.Rows[rowID].FindControl("employfee");
        
        if (string.IsNullOrEmpty(HFSalaryLocaion.Value))
        {
            CheckBox Check = (CheckBox)gv_JdBoard.Rows[rowID].FindControl("ckApproved");
            Check.Checked = false;

            aMessage.ShowMessageBox(employfee.Text+ " for this employee Office Missing!", this);
        }
        DataTable Dt = aSheetDal.Get_FeedBack(MasterId.Value);

        if (Dt.Rows.Count == 0)
        {
            CheckBox Check = (CheckBox)gv_JdBoard.Rows[rowID].FindControl("ckApproved");
            Check.Checked = false;
            aMessage.ShowMessageBox("There is no committee feedback for this Application",this);

        }

    }

    protected void txtMeetingNo_OnTextChanged(object sender, EventArgs e)
    {
        if (txtMeetingNo.Text.Trim() != "")
        {
            DataTable Dt = aSheetDal.Get_TopSheet_meetingCheck(txtMeetingNo.Text.Trim());

            if (Dt.Rows.Count > 0)
            {
                if (txtMeetingNo.Text.Trim().ToUpper() == Dt.Rows[0]["MeetingNo"].ToString().ToUpper())
                {
                    txtMeetingNo.Text = "";
                    aMessage.ShowMessageBox("Meeting No Already Exists", this);

                }
            }
        }
    }

    protected void btn_view_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        Session["dsdadf"] = "";
        Session["dsdadf"] = "View";
        HiddenField MasterId = (HiddenField)gv_JdBoard.Rows[rowID].FindControl("hfeimbursFromMasterId");
        Response.Redirect("ApplicationView_Committee.aspx?MID=" + MasterId.Value.Trim());
    }
}