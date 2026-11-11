using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Inverview_DAL;
using DAL.MasterSetup_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class Inverview_InterviewCandidateInvitation : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    ValidationDeleteCommonDAL aCommonDAL = new ValidationDeleteCommonDAL();
    private InterviewCommonDAL _interviewCommonDal = new InterviewCommonDAL();
    private string _userId;
    private DropDownList ddlCompany;
    private DropDownList ddlJobCirculation;
    //private TextBox txt_JobTitle;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        ddlCompany = (DropDownList)IVSearchControl.FindControl("ddlCompany");
        ddlJobCirculation = (DropDownList)IVSearchControl.FindControl("ddlJobCirculation") as DropDownList;
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {
            UserPersmissionValidation();
            ButtonVisible();
            Session["cid"] = null;
            LoadInitialDDL();
            //SetInitialRow();
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
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    public void UserPersmissionValidation()
    {
        try
        {
            PermissionDAL aPermissionDal = new PermissionDAL();

            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');
            filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            DataTable dtuserpermission = aPermissionDal.GetPermissionForUser(Session["UserId"].ToString(), filepath);
            if (dtuserpermission.Rows.Count > 0)
            {
                if (dtuserpermission.Rows[0]["UserTypeId"].ToString() != "3" ||
                    dtuserpermission.Rows[0]["UserTypeId"].ToString() != "4")
                {


                    ViewState["Add"] = dtuserpermission.Rows[0]["Add"].ToString();
                    ViewState["Edit"] = dtuserpermission.Rows[0]["Edit"].ToString();
                    ViewState["View"] = dtuserpermission.Rows[0]["View"].ToString();
                    ViewState["Delete"] = dtuserpermission.Rows[0]["Delete"].ToString();
                    
                }
            }
            else
            {
                Response.Redirect("../DashBoard_UI/DashBoard.aspx");
            }
        }
        catch (Exception ex)
        {

            AlertMessageBoxShow(ex.ToString());
        }
    }
    public void ButtonVisible()
    {
        try
        {
            btn_Save.Visible = Convert.ToBoolean(ViewState["Add"].ToString());
        }
        catch (Exception)
        {
            
            //throw;
        }
        //if (Session["Status"] != null)
        //{


        //    if (Session["Status"].ToString() == "Add")
        //    {
        //        btn_Save.Visible = true;
        //    }
        //    //else if (Session["Status"].ToString() == "Edit")
        //    //{
        //    //    editButton.Visible = true;
        //    //}
        //    //else if (Session["Status"].ToString() == "Delete")
        //    //{
        //    //    delButton.Visible = true;
        //    //}
        //    Session["Status"] = null;
        //}

    }
    private void LoadInitialDDL()
    {
        //using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        //{

        //    ddlCompany.DataSource = dt;
        //    ddlCompany.DataValueField = "Value";
        //    ddlCompany.DataTextField = "TextField";
        //    ddlCompany.DataBind();
        //}
    }
    private void SetInitialRow()
    {

        DataTable dt = new DataTable();
        DataRow dr = null;


        dt.Columns.Add(new DataColumn("BoardDetailsId", typeof(string)));
        dt.Columns.Add(new DataColumn("MemberType", typeof(string)));
        dt.Columns.Add(new DataColumn("EmpMasterCode", typeof(string)));
        dt.Columns.Add(new DataColumn("EmpName", typeof(string)));
        dt.Columns.Add(new DataColumn("Designation", typeof(string)));
        dt.Columns.Add(new DataColumn("DepartmentName", typeof(string)));
        dt.Columns.Add(new DataColumn("CompanyName", typeof(string)));
        dt.Columns.Add(new DataColumn("Email", typeof(string)));
        dt.Columns.Add(new DataColumn("CellNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Remarks", typeof(string)));

        dr = dt.NewRow();
        dr["EmpMasterCode"] = "";
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState for future reference   
        ViewState["CurrentTable"] = dt;

        //Bind the Gridview   
        gv_InterviewCList.DataSource = dt;
        gv_InterviewCList.DataBind();

    }
    //protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Session["cid"] = ddlCompany.SelectedValue;
    //}

    //protected void txt_JobCirculation_OnTextChanged(object sender, EventArgs e)
    //{
    //    string Emp = txt_JobCirculation.Text;
    //    if (!string.IsNullOrEmpty(Emp) && Emp.Length > 5)
    //    {
    //        hdJobID.Value = Emp.Split(':')[0];
    //        txt_JobCirculation.Text = Emp.Split(':')[1];
    //        txt_JobTitle.Text = Emp.Split(':')[2];
    //    }
    //}

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("InterviewCandidateInvitation.aspx");
    }
    private void SenMailForApprved(string ForMailAddress, string mSubject, string mBody)
    {



       
          

        if (ForMailAddress != "")
        {
            System.Threading.Thread.Sleep(100);

            MailMessage mail = new MailMessage();




            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(Session["EmailID"].ToString());
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
            mail.Priority = System.Net.Mail.MailPriority.High;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(Session["EmailID"].ToString(),
                Session["AppPass"].ToString());
            SmtpServer.EnableSsl = true;


            try
            {
                SmtpServer.Send(mail);
            }
            catch (System.Net.Mail.SmtpException ex)
            {

            }
            catch (Exception exe)
            {

            }


            System.Threading.Thread.Sleep(100);
        }



    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        try
        {
            using (var db = new HRIS_SMCEntities())
            {
                if (Validation())
        {
                for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
                {
                    CheckBox chkSingle = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingle");
                    if (chkSingle.Checked)
                    {
                        CheckBox chkSinglePhone = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSinglePhone");
                        CheckBox chkSingleEmail = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingleEmail");
                        CheckBox chkSingleSMS = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingleSMS");

                        Label txt_EmailAdress = (Label)gv_InterviewCList.Rows[i].FindControl("txt_EmailAdress");
                        HiddenField hdCandidateID = (HiddenField)gv_InterviewCList.Rows[i].FindControl("hdCandidateID");
                        HiddenField hdCompanyId = (HiddenField)gv_InterviewCList.Rows[i].FindControl("hdCompanyId");
                        HiddenField hdJobID = (HiddenField)gv_InterviewCList.Rows[i].FindControl("hdJobID");
                        TextBox txt_Remarks = (TextBox)gv_InterviewCList.Rows[i].FindControl("txt_Remarks");
                        Label txt_CandidateName = (Label)gv_InterviewCList.Rows[i].FindControl("txt_CandidateName");


                        HiddenField hdpkd = (HiddenField)gv_InterviewCList.Rows[i].FindControl("hdpkd");


                        if (chkSingleEmail.Checked)
                        {
                            SenMailForApprved(txt_EmailAdress.Text, "Interview Invitation", "Dear Mr./Mrs. " + txt_CandidateName.Text + ", <br/>" + txt_Remarks.Text + "  <br/>  <br/> Thanks");
                        }

                         if (!CheckEmpDepartmentAllocateOrNot(hdCandidateID.Value.ToString()))
                            {
                        int pkd = int.Parse(hdpkd.Value);
                        if (pkd > 0)////Edit Mode
                        {
                            tblInterviewCandidateInvitation ci = db.tblInterviewCandidateInvitations.FirstOrDefault(ici => ici.CandidateInvitationId == pkd);

                            ci.CandidateID = int.Parse(hdCandidateID.Value);
                            ci.InterviewPhase = int.Parse(ddlInterviewPhase.SelectedValue);
                            ci.PhoneInvitationSent = chkSinglePhone.Checked
                                ? ci.PhoneInvitationSent + 1
                                : ci.PhoneInvitationSent;
                            ci.EmailInvitationSent = chkSingleEmail.Checked
                                ? ci.EmailInvitationSent + 1
                                : ci.EmailInvitationSent;
                            ci.SMSInvitationSent = chkSingleSMS.Checked ? ci.SMSInvitationSent + 1 : ci.SMSInvitationSent;
                            ci.Remarks = txt_Remarks.Text;
                            ci.UpdateBy = _userId;
                            ci.UpdateDate = DateTime.Now;
                            //db.tblInterviewCandidateInvitations.Add(ci);
                        }
                        else
                        {////New Mode


 
                                tblInterviewCandidateInvitation ci = new tblInterviewCandidateInvitation();
                                ci.CandidateInvitationId = pkd;
                                ci.CandidateID = int.Parse(hdCandidateID.Value);
                                ci.InterviewPhase = int.Parse(ddlInterviewPhase.SelectedValue);
                                ci.PhoneInvitationSent = chkSinglePhone.Checked
                                    ? ci.PhoneInvitationSent + 1
                                    : ci.PhoneInvitationSent;
                                ci.EmailInvitationSent = chkSingleEmail.Checked
                                    ? ci.EmailInvitationSent + 1
                                    : ci.EmailInvitationSent;
                                ci.SMSInvitationSent = chkSingleSMS.Checked
                                    ? ci.SMSInvitationSent + 1
                                    : ci.SMSInvitationSent;
                                ci.Remarks = txt_Remarks.Text;
                                ci.EntryBy = _userId;
                                ci.EntryDate = DateTime.Now;
                                db.tblInterviewCandidateInvitations.Add(ci);
                            
                        }

                            }

                         else
                         {
                             ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Can Not be Inserted!!!');",
                            true);

                         }

                    }
                }
                db.SaveChanges();
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='InterviewCandidateInvitation.aspx';",
                    true);
        }
            }
            
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
    }


    private bool CheckEmpDepartmentAllocateOrNot(string departmentId)
    {
        bool status = false;

        DataTable dataTable = aCommonDAL.InterviewCandidateAttandanceForCandidateInvitation(departmentId);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private bool Validation()
    {


        if (ddlCompany.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            ddlCompany.Focus();
            return false;
        }

        if (ddlJobCirculation.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox(aMessages.VArea, this);
            ddlJobCirculation.Focus();
            return false;
        }

        if (gv_InterviewCList.Rows.Count < 0)
        {
            aShowMessage.ShowMessageBox("Interview Candidate List", this);
          
            return false;
        }


        //if (gv_InterviewCList.Rows.Count > 0)
        //{
        //    for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
        //    {
        //        if (((CheckBox)gv_InterviewCList.Rows[i].Cells[4].FindControl("chkSingle")).Checked == false)
        //        {

        //            aShowMessage.ShowMessageBox("Please Candidate Information field in Item Grid!!", this);
        //            return false;

        //        }
        //    }
        //}


        int totalCount = gv_InterviewCList.Rows.Cast<GridViewRow>().Count(r => ((CheckBox)r.FindControl("chkSingle")).Checked);

        if (totalCount == 0)
        {

            aShowMessage.ShowMessageBox("Please Select At Least One Candidate", this);
            return false;
        }


 
        return true;
    }
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }
    protected void btn_LoadList_OnClick(object sender, EventArgs e)
    {
      

        if (vali())
        {
            string cid = ddlCompany.SelectedValue;
            string jobid = ddlJobCirculation.SelectedValue;

            using (DataTable dt = _interviewCommonDal.GetIVCandidateForInvitation(cid, jobid))
            {
                if (dt.Rows.Count > 0)
                {
                    gv_InterviewCList.DataSource = dt;
                    gv_InterviewCList.DataBind();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                     
                        HiddenField hdpkd = (HiddenField)gv_InterviewCList.Rows[i].FindControl("hdpkd");
                        int masid = 0;
                        try
                        {
                            masid = Convert.ToInt32(hdpkd.Value);
                        }
                        catch (Exception ex)
                        {
                            
                        }
                        if (masid>0)
                        {
                            gv_InterviewCList.Rows[i].Cells[0].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[1].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[2].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[3].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[4].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[5].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[6].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[7].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[8].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[9].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[10].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[11].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[12].BackColor = Color.Coral;
                            gv_InterviewCList.Rows[i].Cells[13].BackColor = Color.Coral;
                        }
                        CheckBox chkSinglePhone = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSinglePhone");
                        CheckBox chkSingleEmail = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingleEmail");
                        CheckBox chkSingleSMS = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingleSMS");

                        chkSinglePhone.Checked = int.Parse(dt.Rows[i]["PhoneInvitationSent"].ToString()) > 0;
                        chkSingleEmail.Checked = int.Parse(dt.Rows[i]["EmailInvitationSent"].ToString()) > 0;
                        chkSingleSMS.Checked = int.Parse(dt.Rows[i]["SMSInvitationSent"].ToString()) > 0;
                    }
                }
                else
                {
                    gv_InterviewCList.DataSource = null;
                    gv_InterviewCList.DataBind();
                    AlertMessageBoxShow("No matching data found...!");
                }

                //for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
                //{
                //    TextBox txt_Remarks = (TextBox)gv_InterviewCList.Rows[i].FindControl("txt_Remarks");
                //    txt_Remarks.Text = txt_remarks.Text.Trim();
                //}

            }
        }

        
    }

    private bool vali()
    {
        if (ddlCompany.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Company!!", this);
            ddlCompany.Focus();
            return false;
        }
        if (ddlJobCirculation.SelectedValue == "")
        {
            AlertMessageBoxShow("Please Select Job Circulation !!!");
            return false;

        }
        return true;
    }

    protected void chkAll_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        //GridViewRow gvRow = (GridViewRow)cb.NamingContainer;
        //int rowID = gvRow.RowIndex;

        if (cb.Checked)
        {
            for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingle");
                chkSingle.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
            {
                CheckBox chkSingle = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingle");
                chkSingle.Checked = false;
            }
        }
    }

    protected void chkAllPhone_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        if (cb.Checked)
        {
            for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
            {
                CheckBox chkSinglePhone = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSinglePhone");
                chkSinglePhone.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
            {
                CheckBox chkSinglePhone = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSinglePhone");
                chkSinglePhone.Checked = false;
            }
        }
    }

    protected void chkAllEmail_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        //GridViewRow gvRow = (GridViewRow)cb.NamingContainer;
        //int rowID = gvRow.RowIndex;

        if (cb.Checked)
        {
            for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
            {
                CheckBox chkSingleEmail = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingleEmail");
                chkSingleEmail.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
            {
                CheckBox chkSingleEmail = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingleEmail");
                chkSingleEmail.Checked = false;
            }
        }
    }

    protected void chkAllSMS_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        //GridViewRow gvRow = (GridViewRow)cb.NamingContainer;
        //int rowID = gvRow.RowIndex;

        if (cb.Checked)
        {
            for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
            {
                CheckBox chkSingleSMS = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingleSMS");
                chkSingleSMS.Checked = true;
            }
        }
        else
        {
            for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
            {
                CheckBox chkSingleSMS = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingleSMS");
                chkSingleSMS.Checked = false;
            }
        }
    }

    protected void btn_SubmitWithMail_OnClick(object sender, EventArgs e)
    {
        try
        {
            using (var db = new HRIS_SMCEntities())
            {
                if (Validation())
                {
                    for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
                    {
                        CheckBox chkSingle = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingle");
                        if (chkSingle.Checked)
                        {
                            CheckBox chkSinglePhone = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSinglePhone");
                            CheckBox chkSingleEmail = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingleEmail");
                            CheckBox chkSingleSMS = (CheckBox)gv_InterviewCList.Rows[i].FindControl("chkSingleSMS");

                            HiddenField hdpkd = (HiddenField)gv_InterviewCList.Rows[i].FindControl("hdpkd");
                            HiddenField hdCandidateID = (HiddenField)gv_InterviewCList.Rows[i].FindControl("hdCandidateID");
                            HiddenField hdCompanyId = (HiddenField)gv_InterviewCList.Rows[i].FindControl("hdCompanyId");
                            HiddenField hdJobID = (HiddenField)gv_InterviewCList.Rows[i].FindControl("hdJobID");
                            TextBox txt_Remarks = (TextBox)gv_InterviewCList.Rows[i].FindControl("txt_Remarks");

                            if (!CheckEmpDepartmentAllocateOrNot(hdCandidateID.Value.ToString()))
                            {
                                int pkd = int.Parse(hdpkd.Value);
                                if (pkd > 0)////Edit Mode
                                {
                                    tblInterviewCandidateInvitation ci = db.tblInterviewCandidateInvitations.FirstOrDefault(ici => ici.CandidateInvitationId == pkd);

                                    ci.CandidateID = int.Parse(hdCandidateID.Value);
                                    ci.InterviewPhase = int.Parse(ddlInterviewPhase.SelectedValue);
                                    ci.PhoneInvitationSent = chkSinglePhone.Checked
                                        ? ci.PhoneInvitationSent + 1
                                        : ci.PhoneInvitationSent;
                                    ci.EmailInvitationSent = chkSingleEmail.Checked
                                        ? ci.EmailInvitationSent + 1
                                        : ci.EmailInvitationSent;
                                    ci.SMSInvitationSent = chkSingleSMS.Checked ? ci.SMSInvitationSent + 1 : ci.SMSInvitationSent;
                                    ci.Remarks = txt_Remarks.Text;
                                    ci.UpdateBy = _userId;
                                    ci.UpdateDate = DateTime.Now;
                                    //db.tblInterviewCandidateInvitations.Add(ci);
                                }
                                else
                                {////New Mode



                                    tblInterviewCandidateInvitation ci = new tblInterviewCandidateInvitation();
                                    ci.CandidateInvitationId = pkd;
                                    ci.CandidateID = int.Parse(hdCandidateID.Value);
                                    ci.InterviewPhase = int.Parse(ddlInterviewPhase.SelectedValue);
                                    ci.PhoneInvitationSent = chkSinglePhone.Checked
                                        ? ci.PhoneInvitationSent + 1
                                        : ci.PhoneInvitationSent;
                                    ci.EmailInvitationSent = chkSingleEmail.Checked
                                        ? ci.EmailInvitationSent + 1
                                        : ci.EmailInvitationSent;
                                    ci.SMSInvitationSent = chkSingleSMS.Checked
                                        ? ci.SMSInvitationSent + 1
                                        : ci.SMSInvitationSent;
                                    ci.Remarks = txt_Remarks.Text;
                                    ci.EntryBy = _userId;
                                    ci.EntryDate = DateTime.Now;
                                    db.tblInterviewCandidateInvitations.Add(ci);

                                }

                            }

                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(),
                               "alert",
                               "alert('Can Not be Inserted!!!');",
                               true);

                            }

                        }
                    }
                    db.SaveChanges();
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successful...');window.location ='InterviewCandidateInvitation.aspx';",
                        true);
                }
            }

        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
    }

    protected void txt_remarks_OnTextChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gv_InterviewCList.Rows.Count; i++)
        {
            TextBox txt_Remarks = (TextBox)gv_InterviewCList.Rows[i].FindControl("txt_Remarks");
            txt_Remarks.Text = txt_remarks.Text.Trim();
        }
    }
}