using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.ExitManagement_DAL;
using DAL.MasterSetup_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using Library.DAO.HRM_Entities;

public partial class ExitManagement_UI_EmployeeJobLeftEntry : System.Web.UI.Page
{
    EmployeeJobLeftEntryDAL aEmployeeJobLeftEntryDAL= new EmployeeJobLeftEntryDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ReadonlyDateTime();
            //ButtonVisible();
            LoadDropDownList();
            //LoadBenifitInformation();
          

            if (Session["EmployeeJobLeftId"] != null)
            {
                EmployeeJobLeftIdHiddenField.Value = Session["EmployeeJobLeftId"].ToString();
                GetOneRecord(Session["EmployeeJobLeftId"].ToString());
                Session["EmployeeJobLeftId"] = null;
            }
            else
            {
                Response.Redirect("EmployeeJobLeftApproveView.aspx");
            }
            RadioTextValue();
        }
    }
    private void ReadonlyDateTime()
    {
        SubmissionDateTextBox.Attributes.Add("readonly", "readonly");
        JobLeftDateTextBox.Attributes.Add("readonly", "readonly");
    }

    private void RadioTextValue()
    {
        //string filepath = Path.GetDirectoryName(Request.Path);
        //filepath = filepath.TrimStart('\\');
        //filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
        DataTable dtdata = null;
        string filepath = "";
        if (Session["AppPage"] != null)
        {
            filepath = Session["AppPage"].ToString();
        }
        if (actionstatusHiddenField.Value == "Approved")
        {
            dtdata = aEmployeeJobLeftEntryDAL.GetHRAdminEmployeeAppId(" WHERE URL='" + filepath + "' AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' AND Serial IN (SELECT MAX(Serial)AS SL FROM dbo.tblEmployeeApprovalByOpearationDetail" +
                                                                    " LEFT JOIN dbo.tblMainMenu ON dbo.tblEmployeeApprovalByOpearationDetail.Operation=dbo.tblMainMenu.MainMenuId WHERE URL='" + filepath + "'  ) AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "' ORDER BY Serial ASC ");
        }
        else
        {
            dtdata = aEmployeeJobLeftEntryDAL.GetSupervisorEmployeeAppId(Session["EmpInfoId"].ToString(), entryempinfoIdHiddenField.Value);
        }

        //DataTable dtdata = aEmployeeRequsitionDal.GetSupervisorAppId(filepath, " AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");

        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("Value");
        aDataTable.Columns.Add("Text");

        DataRow dataRow = null;


        //if (Session["EmpInfoId"].ToString() != Session["ForEmpInfoId"].ToString())



        if (dtdata.Rows.Count > 0)
        {
            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Approved";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Review";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);

        }
        else
        {
            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Verified";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Review";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);
        }

        actionRadioButtonList.DataValueField = "Value";
        actionRadioButtonList.DataTextField = "Text";
        actionRadioButtonList.DataSource = aDataTable;
        actionRadioButtonList.DataBind();

        if (actionstatusHiddenField.Value == "Approved")
        {
            submitButton.Visible = false;
            Button1.Visible = true;
        }
        else
        {
            submitButton.Visible = true;
            Button1.Visible = false;
        }
    }
    protected void Button2_OnClick(object sender, EventArgs e)
    {
        //if (Validation())
        {


            EmployeeJobLeftEntryDAO aMaster = new EmployeeJobLeftEntryDAO();
            aMaster.EmployeeJobLeftId
                = Convert.ToInt32(EmployeeJobLeftIdHiddenField.Value);
            aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = aEmployeeJobLeftEntryDAL.UpdateContractural(aMaster);
            if (status)
            {
                int commentid = aEmployeeJobLeftEntryDAL.SaveComment("0", Session["EmpInfoId"].ToString(),
                    commentsTextBox.Text);
                if (aMaster.ActionStatus == "Verified")
                {
                    DataTable dtempdata = aEmployeeJobLeftEntryDAL.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    EmployeeJobLeftAppLogDAO appLogDao = new EmployeeJobLeftAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                        appLogDao.EmployeeJobLeftId = aMaster.EmployeeJobLeftId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;

                    };
                    int id = aEmployeeJobLeftEntryDAL.SavAppLog(appLogDao);
                    aEmployeeJobLeftEntryDAL.UpdateJobReqStatus2(aMaster);
                }
                else if (aMaster.ActionStatus == "Approved")
                {
                    int empid = 0;
                    
                    bool isselfapp = false;
                    DataTable dtdatainfo =
                        aEmployeeJobLeftEntryDAL.GetContractualDataInfo(aMaster.EmployeeJobLeftId.ToString());
                    if (dtdatainfo.Rows.Count > 0)
                    {
                        isselfapp = Convert.ToBoolean(dtdatainfo.Rows[0]["IsSelfApp"].ToString());
                    }
                    if (isselfapp)
                    {
                        DataTable dtempdata = aEmployeeJobLeftEntryDAL.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() + "' AND Serial='1'  AND tblEmployeeApprovalByOpearationDetail.CompanyId='" +
                                                                             Session["CompanyId"].ToString() + "'");
                        if (dtempdata.Rows.Count > 0)
                        {
                            empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                        }
                        EmployeeJobLeftAppLogDAO appLogDao = new EmployeeJobLeftAppLogDAO();
                        {
                            appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = empid;
                            appLogDao.EmployeeJobLeftId = aMaster.EmployeeJobLeftId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        };
                     
                        aEmployeeJobLeftEntryDAL.UpdateJobReqStatus2(aMaster);
                        int id = aEmployeeJobLeftEntryDAL.SavAppLog(appLogDao);
                    }
                    else
                    {
                        empid = Convert.ToInt32(dtdatainfo.Rows[0]["ReportingEmpId"].ToString());
                        EmployeeJobLeftAppLogDAO appLogDao = new EmployeeJobLeftAppLogDAO();
                        {
                            appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = empid;
                            appLogDao.EmployeeJobLeftId = aMaster.EmployeeJobLeftId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        };
                        aMaster.ActionStatus = "Verified";
                        aEmployeeJobLeftEntryDAL.UpdateContractural(aMaster);
                        aEmployeeJobLeftEntryDAL.UpdateJobReqStatus2(aMaster);
                        aEmployeeJobLeftEntryDAL.UpdateSelfApprove(aMaster.EmployeeJobLeftId, true);
                        int id = aEmployeeJobLeftEntryDAL.SavAppLog(appLogDao);

                        SenMailForApprved(appLogDao.ForEmpInfoId, " Employee Separation Approval ", @"  <br/> Dear Sir, <br/>
A Employee Separation is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                       
                    }


                    
                }
                else if (aMaster.ActionStatus == "Review")
                {
                    DataTable dtempdata = aEmployeeJobLeftEntryDAL.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), EmployeeJobLeftIdHiddenField.Value);
                    DataTable dtempdata2 = aEmployeeJobLeftEntryDAL.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), EmployeeJobLeftIdHiddenField.Value);


                    if (dtempdata2.Rows.Count > 0)
                    {
                        EmployeeJobLeftAppLogDAO appLogDao = new EmployeeJobLeftAppLogDAO();
                        {
                            appLogDao.ActionStatus = "Verified";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString());
                            appLogDao.EmployeeJobLeftId = aMaster.EmployeeJobLeftId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        }

                        aEmployeeJobLeftEntryDAL.UpdateAppLog("Review", Session["AppLogId"].ToString());
                        int id = aEmployeeJobLeftEntryDAL.SavAppLog(appLogDao);
                        aEmployeeJobLeftEntryDAL.UpdateJobReqStatus2(aMaster);
                    }
                    else
                    {
                        ShowMessageBox("Please select Approval Status Approved  this!!!");
                    }

                    DataTable dtdata = aEmployeeJobLeftEntryDAL.GetDataReviewEntryBy(
                      EmployeeJobLeftIdHiddenField.Value, Session["UserId"].ToString(), "Review");
                    if (dtdata.Rows.Count > 0)
                    {
                        Session["Status"] = "";
                        Session["Status"] = "Edit";
                        Session["EmployeeJobLeftEdit"] = aMaster.EmployeeJobLeftId.ToString();
                        Response.Redirect("EmployeeJobLeftEntry.aspx?id2=" + aMaster.EmployeeJobLeftId.ToString());
                    }

                }
                Session["AppLogId"] = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                           "alert",
                           "alert('Operation Successfully Done...');window.location ='EmployeeJobLeftApproveView.aspx';",
                           true);

            }
          
        }
    }

    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
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

    protected void Button1_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {


            EmployeeJobLeftEntryDAO aMaster = new EmployeeJobLeftEntryDAO();
            aMaster.EmployeeJobLeftId
                = Convert.ToInt32(EmployeeJobLeftIdHiddenField.Value);
            aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = aEmployeeJobLeftEntryDAL.UpdateJobReqStatus2(aMaster);
            if (status)
            {
                int commentid = aEmployeeJobLeftEntryDAL.SaveComment("0", Session["EmpInfoId"].ToString(),
                    commentsTextBox.Text);
                if (aMaster.ActionStatus == "Verified")
                {
                    DataTable dtempdata =
                        aEmployeeJobLeftEntryDAL.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                       "' AND EmpInfoId='" + Session["EmpInfoId"].ToString() +
                                                                       "'  AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' ");
                    int serial = Convert.ToInt32(dtempdata.Rows[0]["Serial"].ToString()) + 1;
                    DataTable dtempdata2 =
                        aEmployeeJobLeftEntryDAL.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                       "' AND Serial='" + serial + "'  AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' ");
                    //DataTable dtempdata = aEmployeeRequsitionDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    EmployeeJobLeftAppLogDAO appLogDao = new EmployeeJobLeftAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["EmpInfoId"].ToString());
                        appLogDao.EmployeeJobLeftId = aMaster.EmployeeJobLeftId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;

                    };
                    int id = aEmployeeJobLeftEntryDAL.SavAppLog(appLogDao);
                }
                else if (aMaster.ActionStatus == "Approved")
                {
                    int empid = 0;
                    //DataTable dtempdata = aEmployeeRequsitionDal.GetHRAdminEmployeeAppId(" WHERE URL='"+Session["AppPage"].ToString()+"' AND Serial='1'" );
                    //if (dtempdata.Rows.Count>0)
                    //{
                    //    empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                    //}
                    EmployeeJobLeftAppLogDAO appLogDao = new EmployeeJobLeftAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = empid;
                        appLogDao.EmployeeJobLeftId = aMaster.EmployeeJobLeftId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;
                    };


                    int id = aEmployeeJobLeftEntryDAL.SavAppLog(appLogDao);

                    SenMailForApprved(appLogDao.ForEmpInfoId, " Employee Separation Approval ", @"  <br/> Dear Sir, <br/>
A Employee Separation is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                }
                else if (aMaster.ActionStatus == "Review")
                {
                    string actionst = "";
                    DataTable dtempdata = aEmployeeJobLeftEntryDAL.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), EmployeeJobLeftIdHiddenField.Value);
                    if (dtempdata.Rows.Count > 0)
                    {
                        actionst = dtempdata.Rows[0]["ActionStatus"].ToString();
                    }
                    DataTable dtempdata2 = aEmployeeJobLeftEntryDAL.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), EmployeeJobLeftIdHiddenField.Value);
                    int a = 0;
                    for (int i = 0; i < dtempdata2.Rows.Count; i++)
                    {
                        if (dtempdata.Rows[i]["PreEmpInfoId"].ToString() != dtempdata.Rows[i]["ForEmpInfoId"].ToString())
                        {
                            a = i;
                            break;
                        }
                    }
                    if (dtempdata2.Rows.Count > 0)
                    {
                        EmployeeJobLeftAppLogDAO appLogDao = new EmployeeJobLeftAppLogDAO();
                        {
                            //appLogDao.ActionStatus = "Verified";
                            appLogDao.ActionStatus = dtempdata2.Rows[a]["ActionStatus"].ToString();
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[a]["PreEmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[a]["ForEmpInfoId"].ToString());
                            appLogDao.EmployeeJobLeftId = aMaster.EmployeeJobLeftId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        }
                        if (actionst == "Approved")
                        {
                            aMaster.ActionStatus = "Verified";
                            aEmployeeJobLeftEntryDAL.UpdateContractural(aMaster);
                        }
                        aEmployeeJobLeftEntryDAL.UpdateAppLog("Review", Session["AppLogId"].ToString());
                        aEmployeeJobLeftEntryDAL.UpdateAppLog("Review", dtempdata2.Rows[a][0].ToString());
                        int id = aEmployeeJobLeftEntryDAL.SavAppLog(appLogDao);
                    }
                    else
                    {
                        ShowMessageBox("Please select Approval Status Approved  this!!!");
                    }

                }

                Session["AppLogId"] = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                           "alert",
                           "alert('Operation Successfully Done....');window.location ='EmployeeJobLeftApproveView.aspx';",
                           true);
            }
          
        }
    }

    protected void Button2a_OnClick(object sender, EventArgs e)
    {
        EmployeeJobLeftEntryDAO aMaster = new EmployeeJobLeftEntryDAO();
        aMaster.EmployeeJobLeftId
            = Convert.ToInt32(EmployeeJobLeftIdHiddenField.Value);
        aMaster.ActionStatus = "Rejected";
        bool status = aEmployeeJobLeftEntryDAL.UpdateContractural(aMaster);
        int commentid = aEmployeeJobLeftEntryDAL.SaveComment("0", Session["EmpInfoId"].ToString(),
                commentsTextBox.Text);
        if (aMaster.ActionStatus == "Rejected")
        {
            //DataTable dtempdata = aEmployeeRequsitionDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
            EmployeeJobLeftAppLogDAO appLogDao = new EmployeeJobLeftAppLogDAO();
            {
                appLogDao.ActionStatus = "Rejected";
                appLogDao.ApproveDate = DateTime.Now;
                appLogDao.ApproveBy = Session["UserId"].ToString();
                appLogDao.PreEmpInfoId = 0;
                appLogDao.ForEmpInfoId = 0;
                appLogDao.EmployeeJobLeftId = aMaster.EmployeeJobLeftId;
                appLogDao.Comments = commentsTextBox.Text;
                appLogDao.CommentsId = commentid;

            };
            int id = aEmployeeJobLeftEntryDAL.SavAppLog(appLogDao);
            aEmployeeJobLeftEntryDAL.UpdateJobReqStatus2(aMaster);
        }
        Session["AppLogId"] = null;
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Rejected Successfully...');window.location ='EmployeeJobLeftApproveView.aspx';",
                   true);
    }


    public void ButtonVisible()
    {
        //if (Session["Status"] != null)
        //{


        //    if (Session["Status"].ToString() == "Add")
        //    {
        //        submitButton.Visible = true;
        //    }
        //    else if (Session["Status"].ToString() == "Edit")
        //    {
        //        //editButton.Visible = true;
        //    }
        //    else if (Session["Status"].ToString() == "Delete")
        //    {
        //        //delButton.Visible = true;
        //    }
        //    Session["Status"] = null;
        //}
        //else
        //{
        //    Response.Redirect("EmployeeJobLeftEntryView.aspx");
        //}

    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    private void GetOneRecord(string idd)
    {

        //submitButton.Text = "Update";
        //submitButton.BackColor = Color.DodgerBlue;

        DataTable aDataTable = aEmployeeJobLeftEntryDAL.GetEmployeeJobLeftEntryById(idd);

        const int rowIndex = 0;

        if (aDataTable.Rows.Count > 0)
        {




            bool isselfapp = Convert.ToBoolean(aDataTable.Rows[0]["IsSelfApp"].ToString());
            if (isselfapp)
            {
                entryempinfoIdHiddenField.Value = aDataTable.Rows[0]["EmpInfoId"].ToString();
            }
            else
            {
                entryempinfoIdHiddenField.Value = aDataTable.Rows[0]["UserEmpInfoId"].ToString();
            }

            actionstatusHiddenField.Value = aDataTable.Rows[0].Field<String>("ActionStatus").ToString();

            SearchEmployeeNameTextBoxTextBox.Enabled = true;
            EmployeeJobLeftIdHiddenField.Value = Session["EmployeeJobLeftId"].ToString();
            

            companyDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("CompanyId").ToString();
            lblCompany.Text = companyDropDownList.SelectedItem.Text;
            if (companyDropDownList.SelectedValue != "")
            {
                Session["CompanyId"] = companyDropDownList.SelectedValue;
            }
            //  atblEmployeePromotionEntryDAL.EmployeeNameDropDown(EmployeeDropDownList, companyDropDownList.SelectedValue);
            repEmpIdHiddenField.Value = aDataTable.Rows[rowIndex].Field<Int32>("EmployeeId").ToString();

            lblEmp.Text = aDataTable.Rows[0]["EmployeeName"].ToString();

         
            lblEmployeeCode.Text = aDataTable.Rows[0]["EmpMasterCode"].ToString();
            lblJdate.Text = Convert.ToDateTime(aDataTable.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
            lblDesignation.Text = aDataTable.Rows[0]["Designation"].ToString();

            //    PReportingBodyDropDownList.SelectedValue = 1.ToString();

            lblSalaryGrade.Text = aDataTable.Rows[0]["GradeName"].ToString();
            lblDivision.Text = aDataTable.Rows[0]["DivisionName"].ToString();
            lblWing.Text = aDataTable.Rows[0]["DivisionWingName"].ToString();
            lblDepartment.Text = aDataTable.Rows[0]["DepartmentName"].ToString();
            lblSection.Text = aDataTable.Rows[0]["SectionName"].ToString();
            lblSubSection.Text = aDataTable.Rows[0]["SubSectionName"].ToString();
            
            ClearanceFormCheckBoxList.Items[0].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsClearanceForm"].ToString());
            ClearanceFormCheckBoxList.Items[0].Selected = Convert.ToBoolean(aDataTable.Rows[0]["IsExitInterview"].ToString());

            SearchEmployeeNameTextBoxTextBox.Text = aDataTable.Rows[0]["EmployeeName"].ToString();

            if (aDataTable.Rows[0]["AutoProcess"] != null)
            {
                manualUpdateCheckBox.Checked = true;
            }
            manualUpdateCheckBox.Enabled = false;

            //NewGradeDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("NGradeId").ToString();
            JobLeftTypeDropDownList.SelectedValue = aDataTable.Rows[rowIndex].Field<Int32>("JobLeftTypeId").ToString();
            lblJobLeftType.Text = JobLeftTypeDropDownList.SelectedItem.Text;
            JobLeftTypeDropDownList_OnSelectedIndexChanged(null, null);



            try
            {
                JobLeftDateTextBox.Text =
                  aDataTable.Rows[rowIndex].Field<DateTime>("JobLeftDate").ToString("dd-MMM-yyyy");

                lblSeparationDate.Text = JobLeftDateTextBox.Text;
            }
            catch (Exception)
            {

                JobLeftDateTextBox.Text = "";
            }



            try
            {
                SubmissionDateTextBox.Text =
                  aDataTable.Rows[rowIndex].Field<DateTime>("SubmissionDate").ToString("dd-MMM-yyyy");
                lblSubmissionDate.Text = SubmissionDateTextBox.Text;
            }
            catch (Exception)
            {

                SubmissionDateTextBox.Text = "";
            }
          
         



            ReasonTextBox.Text = aDataTable.Rows[rowIndex].Field<string>("Reason").ToString();
            lblReason.Text = ReasonTextBox.Text;
            
            for (int i = 0; i < itemGridView.Rows.Count; i++)
            {
                DataTable dtdata = aEmployeeJobLeftEntryDAL.LoadEmpJobleftBenefitByBenefit(EmployeeJobLeftIdHiddenField.Value, itemGridView.DataKeys[i][0].ToString());
                if (dtdata.Rows.Count>0)
                {
                    ((CheckBox)itemGridView.Rows[i].FindControl("isValueCheckBox")).Checked = Convert.ToBoolean(dtdata.Rows[0]["Active"].ToString()); ;
                    ((RadioButtonList) itemGridView.Rows[i].FindControl("RadioButtonList1")).Items[0].Selected =
                        Convert.ToBoolean(dtdata.Rows[0]["IsAddition"].ToString());
                    ((RadioButtonList) itemGridView.Rows[i].FindControl("RadioButtonList1")).Items[1].Selected =
                        Convert.ToBoolean(dtdata.Rows[0]["IsDeduction"].ToString());
                    ((TextBox) itemGridView.Rows[i].FindControl("rcvQtyTextBox")).Text =
                        dtdata.Rows[0]["Amount"].ToString();
                }
            }



            DataTable AppLogComm = aEmployeeJobLeftEntryDAL.GetAppLogCommByJobId(Convert.ToInt32(idd));

            if (AppLogComm.Rows.Count > 0)
            {

                AppLogCommentGridView.DataSource = AppLogComm;
                AppLogCommentGridView.DataBind();
            }
            else
            {
                AppLogCommentGridView.DataSource = null;
                AppLogCommentGridView.DataBind();
            }

            JobLeftDateTextBox_TextChanged(null, null);
        }

    }

    private void LoadBenifitInformation(string param)
    {


        DataTable dtdata = new DataTable();
        dtdata = aEmployeeJobLeftEntryDAL.LoadBenifitInformation(param);
        if (dtdata.Rows.Count > 0)
        {
            itemGridView.DataSource = dtdata;
            itemGridView.DataBind();
        }
    }
    private void LoadDataGetOneRecord(int id)
    {
       

        DataTable dtdata = new DataTable();
        dtdata = aEmployeeJobLeftEntryDAL.LoadEmpJInfoInTextBoxById(id);
        if (dtdata.Rows.Count > 0)
        {


           
        }
    }



    private void LoadDropDownList()
    {
        aEmployeeJobLeftEntryDAL.LoadCompanyDropDownList(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_OnSelectedIndexChanged(null, null);
        aEmployeeJobLeftEntryDAL.LoadJobLeftTypeDropDownList(JobLeftTypeDropDownList);
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeJobLeftApproveView.aspx");
    }

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
       
        if (companyDropDownList.SelectedValue != "")
        {
            Session["CompanyId"] = companyDropDownList.SelectedValue;
            SearchEmployeeNameTextBoxTextBox.Enabled = true;
        }
        else
        {
            SearchEmployeeNameTextBoxTextBox.Enabled = false;
        }
    }

    protected void SearchEmployeeNameTextBoxTextBox_OnTextChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue!="")
        {
            string empName = SearchEmployeeNameTextBoxTextBox.Text.Trim();

            if (empName.Contains(':'))
            {
                string[] emp = empName.Split(':');

                SearchEmployeeNameTextBoxTextBox.Text = emp[2];
                repEmpIdHiddenField.Value = emp[0];

                LoadData(Convert.ToInt32(repEmpIdHiddenField.Value));
                //productNameTextBox.Text = productInfo[1];
                //string productCode = productCodeTextBox.Text.Trim();
               // SearchEmployeeNameTextBoxTextBox.Text = "";
                if (isprobHiddenField.Value=="1" )
                {
                    if (typeHiddenField.Value=="1")
                    {
                        LoadBenifitInformation(" WHERE EmpCategoryId='" + cateHiddenField.Value + "' AND PermProbation='1' AND SalaryGradeId='" + gradeHiddenField.Value + "'");
                    }
                    if (typeHiddenField.Value=="2")
                    {
                        LoadBenifitInformation(" WHERE EmpCategoryId='" + cateHiddenField.Value + "' AND ContProbation='1' AND SalaryGradeId='" + gradeHiddenField.Value + "'");
                    }
                    if (typeHiddenField.Value=="3")
                    {
                        LoadBenifitInformation(" WHERE EmpCategoryId='" + cateHiddenField.Value + "' AND CasualProbation='1' AND SalaryGradeId='" + gradeHiddenField.Value + "'");
                    }
                }
                if (string.IsNullOrEmpty(isprobHiddenField.Value) || isprobHiddenField.Value=="0")
                {
                    if (typeHiddenField.Value == "1")
                    {
                        LoadBenifitInformation(" WHERE EmpCategoryId='" + cateHiddenField.Value + "' AND PermConfirmed='1' AND SalaryGradeId='" + gradeHiddenField.Value + "'");
                    }
                    if (typeHiddenField.Value == "2")
                    {
                        LoadBenifitInformation(" WHERE EmpCategoryId='" + cateHiddenField.Value + "' AND ContConfirmed='1' AND SalaryGradeId='" + gradeHiddenField.Value + "'");
                    }
                    if (typeHiddenField.Value == "3")
                    {
                        LoadBenifitInformation(" WHERE EmpCategoryId='" + cateHiddenField.Value + "' AND CasualConfirmed='1' AND SalaryGradeId='" + gradeHiddenField.Value + "'");
                    }  
                }
            }
            else
            {

                SearchEmployeeNameTextBoxTextBox.Text = "";
                repEmpIdHiddenField.Value = "";
                aShowMessage.ShowMessageBox("Input Correct Data !!", this);
            }
        }

        else
        {
            SearchEmployeeNameTextBoxTextBox.Text = "";
            repEmpIdHiddenField.Value = "";
            aShowMessage.ShowMessageBox("please Select a Company !!", this);
            companyDropDownList.Focus();
        }
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        
        

    }


    private void Update()
    {
        if (Validation())
        {
            EmployeeJobLeftEntryDAO aEmployeeJobLeftEntryDAO = new EmployeeJobLeftEntryDAO();

            aEmployeeJobLeftEntryDAO.EmployeeJobLeftId = Convert.ToInt32(EmployeeJobLeftIdHiddenField.Value);



            aEmployeeJobLeftEntryDAO.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);

            aEmployeeJobLeftEntryDAO.EmployeeId = Convert.ToInt32(repEmpIdHiddenField.Value);
            //atblEmployeePromotionEntryDAO.PGradeId = Convert.ToInt32(PreviousGradeDropDownList.SelectedValue);
            aEmployeeJobLeftEntryDAO.JobLeftTypeId = Convert.ToInt32(JobLeftTypeDropDownList.SelectedValue);
           
            aEmployeeJobLeftEntryDAO.JobLeftDate = Convert.ToDateTime(JobLeftDateTextBox.Text);
            aEmployeeJobLeftEntryDAO.Reason = Convert.ToString(ReasonTextBox.Text);


            if (ClearanceFormCheckBoxList.Items[0].Selected == true)
            {
                aEmployeeJobLeftEntryDAO.IsClearanceForm = true;

            }
            else
            {
                aEmployeeJobLeftEntryDAO.IsClearanceForm = false;
            }


            if (ClearanceFormCheckBoxList.Items[0].Selected == true)
            {

                aEmployeeJobLeftEntryDAO.IsExitInterview = true;
            }

            else
            {
                aEmployeeJobLeftEntryDAO.IsExitInterview = false;
            }
            if (SubmissionDateTextBox.Text != string.Empty)
            {
                aEmployeeJobLeftEntryDAO.SubmissionDate = Convert.ToDateTime(SubmissionDateTextBox.Text);
            }
            aEmployeeJobLeftEntryDAO.UpdateBy   = Convert.ToInt32(Session["UserId"]);
            aEmployeeJobLeftEntryDAO.UpdateDate = DateTime.Now;

            aEmployeeJobLeftEntryDAL.EmployeeJobLeftUpsateInfo(aEmployeeJobLeftEntryDAO);

            //For Employee Master Information update ------------------------------------------------------------------------

            if (manualUpdateCheckBox.Checked)
            {

                Int32 empGenId = 0;
                string reason = "";

                empGenId = Convert.ToInt32(repEmpIdHiddenField.Value);
                reason = Convert.ToString(JobLeftTypeDropDownList.SelectedItem.Text);

                UpdateEmployeeStepId(empGenId, reason);
            }

            //--------------------------------------------------------------------------------------------------------------

            int id = Convert.ToInt32(EmployeeJobLeftIdHiddenField.Value);
            if (id > 0)
            {
                aEmployeeJobLeftEntryDAL.DeleteEmployeeJobLeftBenefitById(id.ToString());
                for (int i = 0; i < itemGridView.Rows.Count; i++)
                {
                    EmployeeJobLeftEntryDAO aJobLeftEntryDao = new EmployeeJobLeftEntryDAO()
                    {
                        EmployeeJobLeftId = id,
                        BenefitId = Convert.ToInt32(itemGridView.DataKeys[i][0].ToString()),
                        Amount = !string.IsNullOrEmpty(((TextBox)itemGridView.Rows[i].FindControl("rcvQtyTextBox")).Text) ? 0 : Convert.ToDecimal(((TextBox)itemGridView.Rows[i].FindControl("rcvQtyTextBox")).Text),
                        Active = ((CheckBox)itemGridView.Rows[i].FindControl("isValueCheckBox")).Checked,
                        IsAddition = ((RadioButtonList)itemGridView.Rows[i].FindControl("RadioButtonList1")).Items[0].Selected,
                        IsDeduction = ((RadioButtonList)itemGridView.Rows[i].FindControl("RadioButtonList1")).Items[1].Selected,

                    };
                    aEmployeeJobLeftEntryDAL.EmployeePromotionBenefitEntrySaveInfo(aJobLeftEntryDao);
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Updated Successfully...');window.location ='EmployeeJobLeftEntryView.aspx';",
                   true);
        }



    }


    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }


    public void LoadData(int id)
    {
        DataTable dtdata = new DataTable();
        dtdata = aEmployeeJobLeftEntryDAL.LoadEmpJInfoInTextBoxById(id);
        if (dtdata.Rows.Count > 0)
        {


            //EmployeeNameTextBox.Text = dtdata.Rows[0]["EmpName"].ToString();
            //DesignationTextBox.Text = dtdata.Rows[0]["Designation"].ToString();
            //JoiningDateTextBox.Text = Convert.ToDateTime(dtdata.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
            //SalaryGradeTextBox.Text = dtdata.Rows[0]["GradeName"].ToString();


            lblEmp.Text = dtdata.Rows[0]["EmpName"].ToString();

            lblComName.Text = dtdata.Rows[0]["CompanyName"].ToString();
            lblEmployeeCode.Text = dtdata.Rows[0]["EmpMasterCode"].ToString();
            lblJdate.Text = Convert.ToDateTime(dtdata.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
            lblDesignation.Text = dtdata.Rows[0]["Designation"].ToString();
            typeHiddenField.Value = dtdata.Rows[0]["EmpTypeId"].ToString();
            isprobHiddenField.Value = dtdata.Rows[0]["IsProbationary"].ToString();
            cateHiddenField.Value = dtdata.Rows[0]["EmpCategoryId"].ToString();
            gradeHiddenField.Value = dtdata.Rows[0]["SalaryGradeId"].ToString();
            //    PReportingBodyDropDownList.SelectedValue = 1.ToString();

            lblSalaryGrade.Text = dtdata.Rows[0]["SalaryGrade"].ToString();
            lblDivision.Text = dtdata.Rows[0]["DivisionName"].ToString();
            lblWing.Text = dtdata.Rows[0]["DivisionWingName"].ToString();
            lblDepartment.Text = dtdata.Rows[0]["DepartmentName"].ToString();
            lblSection.Text = dtdata.Rows[0]["SectionName"].ToString();
            lblSubSection.Text = dtdata.Rows[0]["EmployeeMentType"].ToString();


        }
    }


    private bool Validation()
    {
        //if (EmployeeDropDownList.SelectedValue == "")
        //{
        //    aShowMessage.ShowMessageBox("Please Select Employe Search !!!", this);
        //    EmployeeDropDownList.Focus();
        //    return false;
        //}


        //if (NewGradeDropDownList.SelectedValue == "")
        //{
        //    aShowMessage.ShowMessageBox("Please Select New Grade !!!", this);
        //    NewGradeDropDownList.Focus();
        //    return false;
        //}

        if (companyDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select New Designation !!!", this);
            companyDropDownList.Focus();
            return false;
        }


      

       

        if (JobLeftTypeDropDownList.SelectedValue == "")
        {
            aShowMessage.ShowMessageBox("Please Select Promotion Type !!!", this);
            JobLeftTypeDropDownList.Focus();
            return false;
        }

        if (SearchEmployeeNameTextBoxTextBox.Text == String.Empty)
        {
            aShowMessage.ShowMessageBox("Please Enter Employee  !!!", this);
            SearchEmployeeNameTextBoxTextBox.Focus();
            return false;
        }

        if (JobLeftDateTextBox.Text == String.Empty)
        {
            aShowMessage.ShowMessageBox("Please Select Active Date !!!", this);
            JobLeftDateTextBox.Focus();
            return false;
        }



        if (chkIsSubmissionDate.Checked)
        {
            if (SubmissionDateTextBox.Text == String.Empty)
            {
                aShowMessage.ShowMessageBox("Please Select Submission Date !!!", this);
                SubmissionDateTextBox.Focus();
                return false;
            }
        }
        

        return true;
    }


    public void Save()
    {
        if (Validation())
        {

            EmployeeJobLeftEntryDAO aEmployeeJobLeftEntryDAO = new EmployeeJobLeftEntryDAO();

            aEmployeeJobLeftEntryDAO.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
            aEmployeeJobLeftEntryDAO.EmployeeId = Convert.ToInt32(repEmpIdHiddenField.Value);

            aEmployeeJobLeftEntryDAO.JobLeftTypeId = Convert.ToInt32(JobLeftTypeDropDownList.SelectedValue);
          
            // atblEmployeePromotionEntryDAO.PSetpId = Convert.ToInt32(PreviousStepDropDownList.SelectedValue);
            // atblEmployeePromotionEntryDAO.PRepEmpId = Convert.ToInt32(PReportingBodyDropDownList.SelectedValue);

            if (ClearanceFormCheckBoxList.Items[0].Selected == true)
            {
                aEmployeeJobLeftEntryDAO.IsClearanceForm = true;
               
            }
            else
            {
                aEmployeeJobLeftEntryDAO.IsClearanceForm = false; 
            }


            if (ClearanceFormCheckBoxList.Items[0].Selected == true)
            {
                
                aEmployeeJobLeftEntryDAO.IsExitInterview = true;
            }
           
              else
            {
                aEmployeeJobLeftEntryDAO.IsExitInterview = false;
            }
             
            aEmployeeJobLeftEntryDAO.JobLeftDate = Convert.ToDateTime(JobLeftDateTextBox.Text);
            aEmployeeJobLeftEntryDAO.Reason = Convert.ToString(ReasonTextBox.Text);

            if (SubmissionDateTextBox.Text!=string.Empty)
            {
                aEmployeeJobLeftEntryDAO.SubmissionDate = Convert.ToDateTime(SubmissionDateTextBox.Text);
            }
         

            if(manualUpdateCheckBox.Checked)
            {
                aEmployeeJobLeftEntryDAO.AutoProcess = "Manually Updated";
            }

            aEmployeeJobLeftEntryDAO.EntryBy = Convert.ToInt32(Session["UserId"]);
            aEmployeeJobLeftEntryDAO.EntryDate = DateTime.Now;

            int id=aEmployeeJobLeftEntryDAL.EmployeePromotionEntrySaveInfo(aEmployeeJobLeftEntryDAO);

            //For Employee Master Information update ------------------------------------------------------------------------

            if (manualUpdateCheckBox.Checked)
            {

                Int32 empGenId = 0;
                string reason = "";

                empGenId = Convert.ToInt32(repEmpIdHiddenField.Value);
                reason = Convert.ToString(JobLeftTypeDropDownList.SelectedItem.Text);

                UpdateEmployeeStepId(empGenId, reason);
            }

            //--------------------------------------------------------------------------------------------------------------


            if (id>0)
            {
                aEmployeeJobLeftEntryDAL.DeleteEmployeeJobLeftBenefitById(id.ToString());
                for (int i = 0; i < itemGridView.Rows.Count ; i++)
                {
                    EmployeeJobLeftEntryDAO aJobLeftEntryDao = new EmployeeJobLeftEntryDAO()
                    {
                        EmployeeJobLeftId = id,
                        BenefitId = Convert.ToInt32(itemGridView.DataKeys[i][0].ToString()),
                        Amount = !string.IsNullOrEmpty(((TextBox)itemGridView.Rows[i].FindControl("rcvQtyTextBox")).Text) ? 0 : Convert.ToDecimal(((TextBox)itemGridView.Rows[i].FindControl("rcvQtyTextBox")).Text),
                        Active = ((CheckBox)itemGridView.Rows[i].FindControl("isValueCheckBox")).Checked,
                        IsAddition = ((RadioButtonList)itemGridView.Rows[i].FindControl("RadioButtonList1")).Items[0].Selected,
                        IsDeduction = ((RadioButtonList)itemGridView.Rows[i].FindControl("RadioButtonList1")).Items[1].Selected,

                    };
                    aEmployeeJobLeftEntryDAL.EmployeePromotionBenefitEntrySaveInfo(aJobLeftEntryDao);
                }





                if (Session["EmpInfoId"].ToString() != "")
                {
                    EmployeeJobLeftEntryDAO aMaster = new EmployeeJobLeftEntryDAO();
                    aMaster.EmployeeJobLeftId
                        = Convert.ToInt32(id);
                    aMaster.ActionStatus = "Verified";
                    bool status = aEmployeeJobLeftEntryDAL.UpdateContractural(aMaster);



                    int commentid = aEmployeeJobLeftEntryDAL.SaveComment("0", Session["EmpInfoId"].ToString(),
                    " ");

                    EmployeeJobLeftAppLogDAO appLogDaoa = new EmployeeJobLeftAppLogDAO();

                    appLogDaoa.ActionStatus = "Drafted";
                    appLogDaoa.ApproveDate = DateTime.Now;
                    appLogDaoa.ApproveBy = Session["UserId"].ToString();
                    appLogDaoa.PreEmpInfoId = Convert.ToInt32(0);
                    appLogDaoa.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                    appLogDaoa.EmployeeJobLeftId = id;
                    appLogDaoa.Comments = "";
                    appLogDaoa.CommentsId = commentid;

                    int idd = aEmployeeJobLeftEntryDAL.SavAppLog(appLogDaoa);


                    //DataTable dtempdata = aEmployeeJobLeftEntryDAL.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    //EmployeeJobLeftAppLogDAO appLogDao = new EmployeeJobLeftAppLogDAO();
                    //{
                    //    appLogDao.ActionStatus = "Verified";
                    //    appLogDao.ApproveDate = DateTime.Now;
                    //    appLogDao.ApproveBy = Session["UserId"].ToString();
                    //    appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                    //    appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                    //    appLogDao.EmployeeJobLeftId = aMaster.EmployeeJobLeftId;
                    //    appLogDao.Comments = " ";
                    //    appLogDao.CommentsId = commentid;

                    //};
                    //int iddddd = aEmployeeJobLeftEntryDAL.SavAppLog(appLogDao);
                    //aEmployeeJobLeftEntryDAL.UpdateJobReqStatus2(aMaster);
                }










            }
            

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                  "alert",
                  "alert('Data Saved Successfully...');window.location ='EmployeeJobLeftEntryView.aspx';",
                  true);
            

        }
    }


    private void UpdateEmployeeStepId(int empGenId, string reason)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.InactiveReason = reason;
        aInfo.IsActive = false;
        aInfo.EmployeeStatus = "InActive";
        aInfo.EmpInfoId = empGenId;

        aEmployeeJobLeftEntryDAL.UpdateEmployeeExitInfo(aInfo);

    }
  

    private void Clear()
    {
        companyDropDownList.SelectedValue = "";
        SearchEmployeeNameTextBoxTextBox.Text = string.Empty;
        //EmployeeNameTextBox.Text = "";
        //JoiningDateTextBox.Text = string.Empty;
        //DesignationTextBox.Text = "";
        //SalaryGradeTextBox.Text = "";
        ReasonTextBox.Text = "";
        JobLeftDateTextBox.Text = "";
        JobLeftTypeDropDownList.SelectedValue = "";
        ClearanceFormCheckBoxList.Items[0].Selected = false;
        ClearanceFormCheckBoxList.Items[1].Selected = false;
        SubmissionDateTextBox.Text = "";
        lblComName.Text = "";
        lblDepartment.Text = "";
        lblDesignation.Text = "";
        lblDivision.Text = "";
        lblEmp.Text = "";
        lblEmployeeCode.Text = "";
        
        lblJdate.Text = "";
        lblSalaryGrade.Text = "";
        lblSection.Text = "";
        lblSubSection.Text = "";
        lblWing.Text = "";
        
    }

    protected void JobLeftDateTextBox_TextChanged(object sender, EventArgs e)
    {
        if (JobLeftDateTextBox.Text != "")
        {
            try
            {
                DateTime.Parse(JobLeftDateTextBox.Text);
                DateTime d = Convert.ToDateTime(SubmissionDateTextBox.Text);
                DateTime c = Convert.ToDateTime(JobLeftDateTextBox.Text);
                DateTime.Parse(SubmissionDateTextBox.Text);
                TimeSpan ts = c - d;

                // Difference in days.

                int differenceInDays = ts.Days; // This is in int
                double differenceInDaysd = ts.TotalDays;
                DurationDateTextBox.Text = differenceInDaysd.ToString();

                lblDurationDays.Text = DurationDateTextBox.Text;
            }
            catch
            {
                JobLeftDateTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
    }

    protected void SubmissionDateTextBox_TextChanged(object sender, EventArgs e)
    {
        
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (EmployeeJobLeftIdHiddenField.Value == string.Empty)
        {
           // Save();
        }
        else
        {
               DataTable aTable =
                             aEmployeeJobLeftEntryDAL.DeleteValidattionForEffectiveDate(EmployeeJobLeftIdHiddenField.Value.ToString());
            if (aTable.Rows.Count > 0)
            {
                string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["JobLeftDate"]).ToString("MMMM dd, yyyy");
                string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                {
                    Update();
                }
                else
                {
                    aShowMessage.ShowMessageBox("Data Can not be Updated !!!", this);
                }
            }
        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {

        if (EmployeeJobLeftIdHiddenField.Value != string.Empty)
        {
             DataTable aTable =
                             aEmployeeJobLeftEntryDAL.DeleteValidattionForEffectiveDate(EmployeeJobLeftIdHiddenField.Value.ToString());
            if (aTable.Rows.Count > 0)
            {
                string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["JobLeftDate"]).ToString("MMMM dd, yyyy");
                string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                {
                    Delete();
                }
                else
                {
                    aShowMessage.ShowMessageBox("Data Can not be Deleted !!!", this);
                }
            }
        }
      
    }


    private void Delete()
    {
        EmployeeJobLeftEntryDAO aJobCreationDao = new EmployeeJobLeftEntryDAO();


        aJobCreationDao.EmployeeJobLeftId = Convert.ToInt32(EmployeeJobLeftIdHiddenField.Value);

        aJobCreationDao.IsDelete = true;


        aJobCreationDao.DeleteBy = Convert.ToInt32(Session["UserId"]);



        aJobCreationDao.DeleteDate = DateTime.Now;
        //////aEmployeeRequsitionDal.DelOtherRequirementDetail(empIdHiddenField.Value);
        //////aEmployeeRequsitionDal.DelEducationRequirementsDetail(empIdHiddenField.Value);
        bool status = aEmployeeJobLeftEntryDAL.DeleteEmployeeJobLeftById(aJobCreationDao);

        if (status)
        {

            ResetEmpGeneralInfo(aJobCreationDao.EmployeeJobLeftId);

            ScriptManager.RegisterStartupScript(this, this.GetType(),
              "alert",
              "alert('Data Deleted Successfully...');window.location ='EmployeeJobLeftEntryView.aspx';",
              true);
        }
    }

    private void ResetEmpGeneralInfo(int jobLeftId)
    {
        DataTable aTable = aEmployeeJobLeftEntryDAL.FetchEmployeeInfoById(jobLeftId);

        if (aTable.Rows.Count > 0)
        {
            Int32 employeeId = aTable.Rows[0].Field<Int32>("EmployeeId");

            EmpGeneralInfo aInfo = new EmpGeneralInfo();

            aInfo.InactiveReason = null;
            aInfo.IsActive = true;
            aInfo.EmployeeStatus = "Active";
            aInfo.EmpInfoId = employeeId;

            aEmployeeJobLeftEntryDAL.UpdateEmployeeExitInfo(aInfo);
            
        }
    }
    JobLeftTypeDAL aVaencyEntryDaL = new JobLeftTypeDAL();
    protected void JobLeftTypeDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
         DataTable dataTable = aVaencyEntryDaL.GetVacaencyInformationById(JobLeftTypeDropDownList.SelectedValue);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            try
            {
                if (dataTable.Rows[rowIndex].Field<bool>("IsSubmissionDate"))
                {

                    chkIsSubmissionDate.Checked = true;

                }
                else
                {
                    chkIsSubmissionDate.Checked = false;
                }
            }
            catch (Exception)
            {

                chkIsSubmissionDate.Checked = false;
            }
        }
    }
}