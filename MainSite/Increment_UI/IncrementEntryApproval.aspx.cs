using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Increment_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using Library.DAO.HRM_Entities;

public partial class Increment_UI_IncrementEntryApproval : System.Web.UI.Page
{
    readonly IncrementApprovalDal aIncrementDal = new IncrementApprovalDal();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private int mid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           // ButtonVisible();
            LoadDropDownList();
            
            EffectiveDateTextBox.Attributes.Add("readonly", "readonly");



            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();
                DataTable aTable = aIncrementDal.LoadIncreamentGetApprovalInformation(mid.ToString());

                if (aTable.Rows.Count > 0)
                {


                    bool isselfapp = Convert.ToBoolean(aTable.Rows[0]["IsSelfApp"].ToString());
                    if (isselfapp)
                    {
                        entryempinfoIdHiddenField.Value = aTable.Rows[0]["EmpInfoId"].ToString();
                    }
                    else
                    {
                        entryempinfoIdHiddenField.Value = aTable.Rows[0]["UserEmpInfoId"].ToString();
                    }



                    actionstatusHiddenField.Value = aTable.Rows[0].Field<String>("ActionStatus").ToString();
                    lblCompany.Text = aTable.Rows[0].Field<string>("ShortName").ToString(CultureInfo.InvariantCulture);

                    lblFinancialYearDesc.Text =
                        aTable.Rows[0].Field<string>("FinancialYearDesc").ToString(CultureInfo.InvariantCulture);
                    lblIncreType.Text = aTable.Rows[0]["Name"].ToString();
                    lblEffDate.Text =
                        aTable.Rows[0].Field<string>("EffectiveDate").ToString(CultureInfo.InvariantCulture);
                    lblEmpId.Text = aTable.Rows[0].Field<string>("EmpMasterCode").ToString(CultureInfo.InvariantCulture);
                    lblEmployeeName.Text = aTable.Rows[0].Field<string>("EmpName")
                        .ToString(CultureInfo.InvariantCulture);
                    lblDesignation.Text =
                        aTable.Rows[0].Field<string>("Designation").ToString(CultureInfo.InvariantCulture);
                    lblDepartment.Text =
                        aTable.Rows[0].Field<string>("DepartmentName").ToString(CultureInfo.InvariantCulture);
                    lblDOJ.Text = aTable.Rows[0].Field<string>("DateOfJoin").ToString(CultureInfo.InvariantCulture);
                    lblSalaryGrade.Text =
                        aTable.Rows[0].Field<string>("GradeCode").ToString(CultureInfo.InvariantCulture);
                    lblCurrentSalaryStep.Text =
                        aTable.Rows[0].Field<string>("SalaryStepName").ToString(CultureInfo.InvariantCulture);
                    lblIncrementalStep.Text =
                        aTable.Rows[0].Field<string>("NewSalaryStepName").ToString(CultureInfo.InvariantCulture);
                    lblFeedSalary.Text =
                        aTable.Rows[0].Field<decimal>("FeedSalary").ToString(CultureInfo.InvariantCulture);



                }


                DataTable AppLogComm = aIncrementDal.GetAppLogCommByJobId(Convert.ToInt32(mid));

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

            }
            else
            {
                Response.Redirect("IncrementApprovalView.aspx");
            }
            RadioTextValue();
            actionRadioButtonList.SelectedIndex = 0;
        }
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
            dtdata = aIncrementDal.GetHRAdminEmployeeAppId(" WHERE URL='" + filepath + "' AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' AND Serial IN (SELECT MAX(Serial)AS SL FROM dbo.tblEmployeeApprovalByOpearationDetail" +
                                                                    " LEFT JOIN dbo.tblMainMenu ON dbo.tblEmployeeApprovalByOpearationDetail.Operation=dbo.tblMainMenu.MainMenuId WHERE URL='" + filepath + "'  ) AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "' and  CompanyId='" + Session["CompanyId"].ToString() + "' ORDER BY Serial ASC ");
        }
        else
        {
            dtdata = aIncrementDal.GetSupervisorEmployeeAppId(Session["EmpInfoId"].ToString(), entryempinfoIdHiddenField.Value);
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


            IncrementDao aMaster = new IncrementDao();
            aMaster.IncrementId
                = Convert.ToInt32(hdpk.Value);
            aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = aIncrementDal.UpdateContractural(aMaster);
            if (status)
            {
                int commentid = aIncrementDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                    commentsTextBox.Text);
                if (aMaster.ActionStatus == "Verified")
                {
                    DataTable dtempdata = aIncrementDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    IncrementAppLogDAO appLogDao = new IncrementAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                        appLogDao.IncrementId = aMaster.IncrementId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;

                    };


                    if (appLogDao.ForEmpInfoId != 0)
                    {
                        int id = aIncrementDal.SavAppLog(appLogDao);
                        aIncrementDal.UpdateJobReqStatus2(aMaster);
                    }
                    else
                    {
                        ShowMessageBox("Set supervisor for employee");
                    }

                   
                }
                else if (aMaster.ActionStatus == "Approved")
                {
                    int empid = 0;

                    bool isselfapp = false;
                    DataTable dtdatainfo =
                        aIncrementDal.GetContractualDataInfo(aMaster.IncrementId.ToString());
                    if (dtdatainfo.Rows.Count > 0)
                    {
                        isselfapp = Convert.ToBoolean(dtdatainfo.Rows[0]["IsSelfApp"].ToString());
                    }
                    if (isselfapp)
                    {
                        DataTable dtempdata = aIncrementDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() + "' AND Serial='1'  AND tblEmployeeApprovalByOpearationDetail.CompanyId='" +
                                                                             Session["CompanyId"].ToString() + "'");
                        if (dtempdata.Rows.Count > 0)
                        {
                            empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                        }
                        IncrementAppLogDAO appLogDao = new IncrementAppLogDAO();
                        {
                            appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = empid;
                            appLogDao.IncrementId = aMaster.IncrementId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        };
                        //aMaster.ActionStatus = "Verified";
                    aIncrementDal.UpdateJobReqStatus2(aMaster);
                        int id = aIncrementDal.SavAppLog(appLogDao);

                        SenMailForApprved(appLogDao.ForEmpInfoId, " Increment Approval ", @"  <br/> Dear Sir, <br/>
An Increment is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                    }
                    else
                    {
                        empid = Convert.ToInt32(dtdatainfo.Rows[0]["ReportingEmpId"].ToString());
                        IncrementAppLogDAO appLogDao = new IncrementAppLogDAO();
                        {
                            appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = empid;
                            appLogDao.IncrementId = aMaster.IncrementId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        };
                        aMaster.ActionStatus = "Verified";
                        aIncrementDal.UpdateContractural(aMaster);
                        aIncrementDal.UpdateJobReqStatus2(aMaster);
                        aIncrementDal.UpdateSelfApprove(aMaster.IncrementId, true);
                     
                        //aIncrementDal.UpdateJobReqStatus2(aMaster);
                        int id = aIncrementDal.SavAppLog(appLogDao);
                    }

                    
                }
                else if (aMaster.ActionStatus == "Review")
                {
                    DataTable dtempdata = aIncrementDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), hdpk.Value);
                    DataTable dtempdata2 = aIncrementDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), hdpk.Value);

                    if (dtempdata2.Rows.Count > 0)
                    {
                        IncrementAppLogDAO appLogDao = new IncrementAppLogDAO();
                        {
                            appLogDao.ActionStatus = "Verified";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString());
                            appLogDao.IncrementId = aMaster.IncrementId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        }

                        aIncrementDal.UpdateContactAppLog("Review", Session["AppLogId"].ToString());
                        int id = aIncrementDal.SavAppLog(appLogDao);
                        aIncrementDal.UpdateJobReqStatus2(aMaster);
                    }
                    else
                    {


                       
                            ShowMessageBox("Please select Approval Status Approved  this!!!");
                        

                    }
                    DataTable dtdata = aIncrementDal.GetDataReviewEntryBy(
                        hdpk.Value, Session["UserId"].ToString(), "Review");
                    if (dtdata.Rows.Count > 0)
                    {
                        Session["Status"] = "";
                        Session["Status"] = "Edit";
                        Session["IncrementEdit"] = aMaster.IncrementId.ToString();
                        Response.Redirect("IncrementEntry2.aspx?id=" + aMaster.IncrementId.ToString());
                    }


                }


            }
            Session["AppLogId"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Operation Successfully Done...');window.location ='IncrementApprovalView.aspx';",
                       true);
        }
    }



    private void SenMailForApprved(int forEmpID, string mSubject, string mBody)
    {



        string ForMailAddress = "";
        using (var db = new HRIS_SMCEntities())
        {
            var GetMailAddress = (dynamic)null;
            if (forEmpID > 0)
            {
                GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                  where t.EmpInfoId == forEmpID
                                  select t).FirstOrDefault();
            }
            else
            {
                int EntryEmpID = Convert.ToInt32(entryempinfoIdHiddenField.Value);


                GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                  where t.EmpInfoId == EntryEmpID
                                  select t).FirstOrDefault();
            }


            if (GetMailAddress != null)
            {
                ForMailAddress = GetMailAddress.OfficialEmail;
            }



        }

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
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        //if (Validation())
        {

            IncrementDao aMaster = new IncrementDao();
            aMaster.IncrementId
                = Convert.ToInt32(hdpk.Value);
            aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = aIncrementDal.UpdateJobReqStatus2(aMaster);
            if (status)
            {
                int commentid = aIncrementDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                    commentsTextBox.Text);
                if (aMaster.ActionStatus == "Verified")
                {
                    DataTable dtempdata =
                        aIncrementDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                     
                  "' AND EmpInfoId='" + Session["EmpInfoId"].ToString() +
                                                                       "'  AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' ");
                    int serial = Convert.ToInt32(dtempdata.Rows[0]["Serial"].ToString()) + 1;
                    DataTable dtempdata2 =
                        aIncrementDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                       "' AND Serial='" + serial + "' AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' ");
                    //DataTable dtempdata = aIncrementDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    IncrementAppLogDAO appLogDao = new IncrementAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["EmpInfoId"].ToString());
                        appLogDao.IncrementId = aMaster.IncrementId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;

                    };
                    int id = aIncrementDal.SavAppLog(appLogDao);
                }
                else if (aMaster.ActionStatus == "Approved")
                {
                    int empid = 0;
                    //DataTable dtempdata = aIncrementDal.GetHRAdminEmployeeAppId(" WHERE URL='"+Session["AppPage"].ToString()+"' AND Serial='1'" );
                    //if (dtempdata.Rows.Count>0)
                    //{
                    //    empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                    //}
                    IncrementAppLogDAO appLogDao = new IncrementAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = empid;
                        appLogDao.IncrementId = aMaster.IncrementId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;
                    };


                    int id = aIncrementDal.SavAppLog(appLogDao);

                    SenMailForApprved(appLogDao.ForEmpInfoId, " Increment Approval ", @"  <br/> Dear Sir, <br/>
An Increment is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                }
                else if (aMaster.ActionStatus == "Review")
                {
                    string actionst = "";
                    DataTable dtempdata = aIncrementDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), hdpk.Value);
                    if (dtempdata.Rows.Count > 0)
                    {
                        actionst = dtempdata.Rows[0]["ActionStatus"].ToString();
                    }
                    DataTable dtempdata2 = aIncrementDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), hdpk.Value);
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
                        IncrementAppLogDAO appLogDao = new IncrementAppLogDAO();
                        {
                            //appLogDao.ActionStatus = "Verified";
                            appLogDao.ActionStatus = dtempdata2.Rows[a]["ActionStatus"].ToString();
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[a]["PreEmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[a]["ForEmpInfoId"].ToString());
                            appLogDao.IncrementId = aMaster.IncrementId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        }
                        if (actionst == "Approved")
                        {
                            aMaster.ActionStatus = "Verified";
                            aIncrementDal.UpdateContractural(aMaster);
                        }
                        aIncrementDal.UpdateContactAppLog("Review", Session["AppLogId"].ToString());
                        aIncrementDal.UpdateContactAppLog("Review", dtempdata2.Rows[a][0].ToString());
                        int id = aIncrementDal.SavAppLog(appLogDao);
                    }
                    else
                    {
                        ShowMessageBox("Please select Approval Status Approved  this!!!");
                    }

                }


            }
            Session["AppLogId"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Operation Successfully Done...');window.location ='IncrementApprovalView.aspx';",
                       true);
        }
    }

    protected void Button2a_OnClick(object sender, EventArgs e)
    {
        IncrementDao aMaster = new IncrementDao();
        aMaster.IncrementId
            = Convert.ToInt32(hdpk.Value);
        aMaster.ActionStatus = "Rejected";
        bool status = aIncrementDal.UpdateContractural(aMaster);
        int commentid = aIncrementDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                commentsTextBox.Text);
        if (aMaster.ActionStatus == "Rejected")
        {
            //DataTable dtempdata = aEmployeeRequsitionDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
            IncrementAppLogDAO appLogDao = new IncrementAppLogDAO();
            {
                appLogDao.ActionStatus = "Rejected";
                appLogDao.ApproveDate = DateTime.Now;
                appLogDao.ApproveBy = Session["UserId"].ToString();
                appLogDao.PreEmpInfoId = 0;
                appLogDao.ForEmpInfoId = 0;
                appLogDao.IncrementId = aMaster.IncrementId;
                appLogDao.Comments = commentsTextBox.Text;
                appLogDao.CommentsId = commentid;

            };
            int id = aIncrementDal.SavAppLog(appLogDao);
            aIncrementDal.UpdateJobReqStatus2(aMaster);
        }
        Session["AppLogId"] = null;
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Rejected Successfully...');window.location ='IncrementApprovalView.aspx';",
                   true);
    }



    public void ButtonVisible()
    {
        //if (Session["Status"] != null)
        //{


        //    if (Session["Status"].ToString() == "Approval")
        //    {
        //        submitButton.Visible = true;
        //    }
           
        //    Session["Status"] = null;
        //}
        //else
        //{
        //    Response.Redirect("IncrementApprovalView.aspx");
        //}

    }

    private void LoadDropDownList()
    {
        try
        {
            aIncrementDal.LoadCompany(ddlCompany);
            aIncrementDal.LoadIncrementType(ddlIncrementType);

            using (DataTable dt = _commonDataLoad.GetDDLEmpType())
            {
                ddlEmpType.DataSource = dt;
                ddlEmpType.DataValueField = "Value";
                ddlEmpType.DataTextField = "TextField";
                ddlEmpType.DataBind();
            }
            ddlCompany.SelectedIndex = 1;
            ddlCompany_SelectedIndexChanged(null, null);
        }
        catch (Exception)
        {
            
            Response.Redirect("/Default.aspx");
        }
    }
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
       
            Session["CompanyId"] = ddlCompany.SelectedValue;
            aIncrementDal.LoadFinancialYear(ddlFinYear,ddlCompany.SelectedValue);
            aIncrementDal.LoadDivision(ddlDivision, ddlCompany.SelectedValue);
            aIncrementDal.LoadCategory(ddlCategory);

            using (DataTable dt = _commonDataLoad.GetDDLComDepartment(ddlCompany.SelectedValue))
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataValueField = "Value";
                ddlDepartment.DataTextField = "TextField";
                ddlDepartment.DataBind();
            }
        }
        else
        {
            ddlFinYear.Items.Clear();
            ddlDivision.Items.Clear();
        }
    }

   
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        if (CheckSearchValidation())
        {
            DataTable aTable = aIncrementDal.LoadEmployeeInformation(GetParameter());

            if (aTable.Rows.Count > 0)
            {
                loadGridView.DataSource = aTable;
                loadGridView.DataBind();

                for (int i = 0; i < loadGridView.Rows.Count; i++)
                {
                    var ddlDesignation = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("designationDropDownList");
                    aIncrementDal.LoadDesignation(ddlDesignation);
                    ddlDesignation.SelectedValue = aTable.Rows[i].Field<Int32>("DesignationId").ToString(CultureInfo.InvariantCulture);

                    var ddlDepartment = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("departmentDropDownList");
                    aIncrementDal.LoadDepartment(ddlDepartment);
                    ddlDepartment.SelectedValue = aTable.Rows[i].Field<Int32>("DepartmentId").ToString(CultureInfo.InvariantCulture);

                    var ddlSalaryGrade = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("salaryGradeDropDownList");
                    aIncrementDal.LoadSalaryGrade(ddlSalaryGrade);
                    ddlSalaryGrade.SelectedValue = aTable.Rows[i].Field<Int32>("SalaryGradeId").ToString(CultureInfo.InvariantCulture);

                    var ddlSalaryStep = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("salaryStepDropDownList");
                    aIncrementDal.LoadSalaryStep(ddlSalaryStep, ddlSalaryGrade.SelectedValue);
                    ddlSalaryStep.SelectedValue = aTable.Rows[i].Field<Int32>("SalaryStepId").ToString(CultureInfo.InvariantCulture);

                    var ddlIncrementalStep = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("incrementalStepDropDownList");
                    aIncrementDal.LoadSalaryStep(ddlIncrementalStep, ddlSalaryGrade.SelectedValue);
                    //SetIncrementalStep(i);

                    var feedSalary = (TextBox)loadGridView.Rows[i].Cells[0].FindControl("feedSalaryTextBox");
                    feedSalary.Text = 5.ToString(CultureInfo.InvariantCulture);

                    ddlSalaryGrade.Enabled = false;
                }
            }
            else
            {
                loadGridView.DataSource = null;
                loadGridView.DataBind();
                ShowMessageBox("No Information found !!!");
            }
            
        }
    }

    private void SetIncrementalStep(int i)
    {
        for (int j = 0; j < loadGridView.Rows.Count; j++)
        {
            var ddlSalaryStep = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("salaryStepDropDownList");
            Int32 stepIndex = ddlSalaryStep.SelectedIndex;

            var ddlIncrementalStep = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("incrementalStepDropDownList");
            ddlIncrementalStep.SelectedIndex = stepIndex + 1;
        }
    }

    private string GetParameter()
    {
        string parameter = "";

        if (ddlCompany.SelectedValue != "")
        {
            parameter = parameter + " AND EGI.CompanyId  = '" + ddlCompany.SelectedValue + "'";
        }

        if (ddlFinYear.SelectedValue != "")
        {
            parameter = parameter + " AND FINY.FinancialYearId = '" + ddlFinYear.SelectedValue + "'";
        }

        if (EmployeeIdHiddenField.Value != "")
        {
            parameter = parameter + " AND EGI.EmpInfoId = '" + EmployeeIdHiddenField.Value + "'";
        }


        if (ddlDivision.SelectedValue != "")
        {
            parameter = parameter + " AND EGI.DivisionId = '" + ddlDivision.SelectedValue + "'";
        }

        if (ddlCategory.SelectedValue != "")
        {
            parameter = parameter + " AND EGI.EmpCategoryId = '" + ddlCategory.SelectedValue + "'";
        }

        if (ddlDepartment.SelectedIndex >0)
        {
            parameter = parameter + " AND EGI.DepartmentId = '" + ddlDepartment.SelectedValue + "'";
        }


        if (ddlEmpType.SelectedIndex > 0)
        {
            parameter = parameter + " AND EGI.EmpTypeId = '" + ddlEmpType.SelectedValue + "'";
        }


        return parameter;
    }

    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    private bool CheckSearchValidation()
    {
        if (ddlCompany.SelectedValue == "")
        {
            ShowMessageBox("Please select company !!!");
            return false;
        }

        if (ddlFinYear.SelectedValue == "")
        {
            ShowMessageBox("Please select financial year !!!");
            return false;
        }

        if (EmployeeIdHiddenField.Value == "")
        {
            if (ddlDivision.SelectedValue == "")
            {
                ShowMessageBox("Please Select Division Name !!!");
                return false;
            }
        }
       

        return true;
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)loadGridView.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");
            chkBoxRows.Checked = chkBoxHeader.Checked;
        }
    }

    protected void submitButton_OnClick(object sender, EventArgs e)
    {
        if (DataValidation())
        {
            Int32 id = SaveIncrementInfo(PrepareDataForSave());

            if (id > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Data Saved Successfully...');window.location ='IncrementView.aspx';",
                       true);
                Clear();
            }
        }
    }

    private void Clear()
    {
        ddlCompany.SelectedValue = "";
        ddlFinYear.Items.Clear();
        ddlDivision.Items.Clear();

        loadGridView.DataSource = null;
        loadGridView.DataBind();
    }

    private int SaveIncrementInfo(List<IncrementDao> aIncrementDaos)
    {
        Int32 id = 0;

        foreach (var aDao in aIncrementDaos)
        {
            

            id = aIncrementDal.SaveIncrementInfo(aDao);
        }

        return id;
    }

    private List<IncrementDao> PrepareDataForSave()
    {
        List<IncrementDao> aDaos = new List<IncrementDao>();
        IncrementDao aDao;

        List<EmpGeneralInfo> aList = new List<EmpGeneralInfo>();
        EmpGeneralInfo aInfo;


        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");


            DataTable aTable =
                          aIncrementDal.ValidattionForEffectiveDate(
                                 (loadGridView.DataKeys[i].Value.ToString()), EffectiveDateTextBox.Text,ddlIncrementType.SelectedValue);

            if (chkBoxRows.Checked && aTable.Rows.Count == 0)
            {
                aDao = new IncrementDao();

                aDao.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                aDao.FinancialYearId = Convert.ToInt32(ddlFinYear.SelectedValue);
                aDao.IncrementTypeId = Convert.ToInt32(ddlIncrementType.SelectedValue);
                aDao.EffectiveDate = Convert.ToDateTime(EffectiveDateTextBox.Text);
               

                Int32 empId = Convert.ToInt32(loadGridView.DataKeys[i].Value.ToString());
                aDao.EmployeeId = empId;
                

                aDao.EmployeeCode = loadGridView.Rows[i].Cells[2].Text.Trim();




                if (Convert.ToInt32(loadGridView.DataKeys[i][1].ToString()) > 0)
                {
                    aDao.DivisionId = Convert.ToInt32(loadGridView.DataKeys[i][1].ToString());
                }

             //   int DivisionWId = Convert.ToInt32(loadGridView.DataKeys[i][2].ToString());

               //  int id = (int) loadGridView.DataKeys[i][2];



                if (((string)loadGridView.DataKeys[i][2].ToString()) != "0")
                {
                    aDao.DivisionWId = Convert.ToInt32(loadGridView.DataKeys[i][2].ToString());
                }


                if (((string)loadGridView.DataKeys[i][3].ToString()) != "0")
                {
                    aDao.DepartmentId = Convert.ToInt32(loadGridView.DataKeys[i][3].ToString());
                }
                if (((string)loadGridView.DataKeys[i][4].ToString()) != "0")
                {
                    aDao.SectionId = Convert.ToInt32(loadGridView.DataKeys[i][4].ToString());
                }
                if (((string)loadGridView.DataKeys[i][5].ToString()) != "0")
                {
                    aDao.SubSectionId = Convert.ToInt32(loadGridView.DataKeys[i][5].ToString());
                }
                if (((string)loadGridView.DataKeys[i][6].ToString()) != "0")
                {
                    aDao.DesignationId = Convert.ToInt32(loadGridView.DataKeys[i][6].ToString());
                }
                if (((string)loadGridView.DataKeys[i][7].ToString()) != "0")
                {
                    aDao.SalaryLoationId = Convert.ToInt32(loadGridView.DataKeys[i][7].ToString());
                }

                if (((string)loadGridView.DataKeys[i][8].ToString()) != "0")
                {
                    aDao.JobLocationId = Convert.ToInt32(loadGridView.DataKeys[i][8].ToString());
                }
                if (((string)loadGridView.DataKeys[i][9].ToString()) != "0")
                {
                    aDao.EmpTypeId = Convert.ToInt32(loadGridView.DataKeys[i][9].ToString());
                }




                //var ddlDesignation = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("designationDropDownList");
                //var ddlDepartment = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("departmentDropDownList");
                var ddlSalaryGrade = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("salaryGradeDropDownList");
                var ddlSalaryStep = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("salaryStepDropDownList");
                var ddlIncrementalStep = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("incrementalStepDropDownList");
                var feedSalary = (TextBox)loadGridView.Rows[i].Cells[0].FindControl("feedSalaryTextBox");
              

                //aDao.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);
                //aDao.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
                aDao.SalaryGradeId = Convert.ToInt32(ddlSalaryGrade.SelectedValue);
                aDao.CurrentStepId = Convert.ToInt32(ddlSalaryStep.SelectedValue);
                aDao.IncrementalStepId = Convert.ToInt32(ddlIncrementalStep.SelectedValue);


                //For Employee Master Information update ------------------------------------------------------------------------

                

                //--------------------------------------------------------------------------------------------------------------

                aDao.FeedSalary = Convert.ToDecimal(feedSalary.Text.Trim());

                if (manualUpdateCheckBox.Checked)
                {
                    aDao.AutoProcess = "Manually Updated";

                    Int32 empGenId = 0;
                    Int32 stepId = 0;

                    empGenId = Convert.ToInt32(loadGridView.DataKeys[i].Value.ToString());
                    stepId = Convert.ToInt32(ddlIncrementalStep.SelectedValue);

                    UpdateEmployeeStepId(empGenId, stepId);
                }

                aDao.EntryBy = Convert.ToInt32(Session["UserId"]);
                aDao.EntryDate = DateTime.Now;

                aDaos.Add(aDao);
            }
            else
            {
               // aShowMessage.ShowMessageBox("Some Employee Already Exists within this effective date!!", this);
            }
        }

        return aDaos;
    }

    private void UpdateEmployeeStepId(int empGenId, int stepId)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.SalScaleId = stepId;
        aInfo.EmpInfoId = empGenId;

        aIncrementDal.UpdateEmployeeIncrementalStepInfo(aInfo);

    }

    private bool DataValidation()
    {
        if (ddlCompany.SelectedValue == "")
        {
            ShowMessageBox("Please select a company !!!");
            return false;
        }
        if (ddlFinYear.SelectedValue == "")
        {
            ShowMessageBox("Please Select Financial year !!!");
            return false;
        }


        if (ddlIncrementType.SelectedIndex <= 0)
        {
            ShowMessageBox("Please Select Increment Type !!!");
            return false;
        }


        if (EffectiveDateTextBox.Text == "")
        {
            ShowMessageBox("Please Select Effective Date !!!");
            EffectiveDateTextBox.Focus();
            return false;
        }

        
         

        Int32 count = 0;

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");

            if (chkBoxRows.Checked)
            {
                count ++;
            }

            if (count > 0)
            {
                break;
            }
        }

        if (count == 0)
        {
            ShowMessageBox("Please Select at least one employee !!!");
            return false;
        }

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            

            var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");

            if (chkBoxRows.Checked)
            {
                var ddlIncrementalStep = (DropDownList)loadGridView.Rows[i].FindControl("incrementalStepDropDownList");
                if (ddlIncrementalStep.SelectedValue == "")
                {
                    ShowMessageBox("Please select incremental step !!!");
                    return false;
                }
            }

            
        }

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var feedSalary = (TextBox)loadGridView.Rows[i].Cells[0].FindControl("feedSalaryTextBox");

            if (feedSalary.Text == "")
            {
                ShowMessageBox("You have to select feed salary !!!");
                return false;
            }
        }
        return true;
    }

    protected void incrementalStepDropDownList_OnTextChanged(object sender, EventArgs e)
    {
        DropDownList dropDown = (DropDownList)sender;
        GridViewRow currentRow = (GridViewRow)dropDown.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        var ddlSalaryStep = (DropDownList)loadGridView.Rows[rowindex].Cells[0].FindControl("salaryStepDropDownList");
        Int32 stepIndex = ddlSalaryStep.SelectedIndex;
        var ddlIncrementalStep = (DropDownList)loadGridView.Rows[rowindex].Cells[0].FindControl("incrementalStepDropDownList");

        if (ddlIncrementalStep.SelectedIndex <= ddlSalaryStep.SelectedIndex)
        {
           ddlIncrementalStep.SelectedIndex = stepIndex ;
            ShowMessageBox("Incremental step must be greater than current salary step !!!");
            ddlIncrementalStep.Focus();
        }
    }

    protected void clearButton_OnClick(object sender, EventArgs e)
    {
       Clear();
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("IncrementApprovalView.aspx");
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void SearchEmployeeNameTextBoxTextBox_OnTextChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedValue != "")
        {
            string empName = SearchEmployeeNameTextBoxTextBox.Text.Trim();

            if (empName.Contains(':'))
            {
                string[] emp = empName.Split(':');

                SearchEmployeeNameTextBoxTextBox.Text = emp[2];
                EmployeeIdHiddenField.Value = emp[0];

                
               
            }
            else
            {

                SearchEmployeeNameTextBoxTextBox.Text = "";
                EmployeeIdHiddenField.Value = "";
                aShowMessage.ShowMessageBox("Input Correct Data !!", this);
            }

        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select a Company !!", this);
            SearchEmployeeNameTextBoxTextBox.Text = "";
            EmployeeIdHiddenField.Value = "";
            ddlCompany.Focus();
        }
    }

    protected void EffectiveDateTextBox_Changed(object sender, EventArgs e)
    {
        if (ddlFinYear.SelectedValue != "")
        {
            if (EffectiveDateTextBox.Text != "")
            {


                if (CheckStartEndDateExistOrNot(EffectiveDateTextBox.Text, EffectiveDateTextBox.Text) == true)
                {

                }
                if (CheckStartEndDateExistOrNot(EffectiveDateTextBox.Text, EffectiveDateTextBox.Text) == false)
                {
                    aShowMessage.ShowMessageBox("Effective date must be within the finnancial year!!", this);
                    EffectiveDateTextBox.Text = "";
                    EffectiveDateTextBox.Focus();

                }
            }



        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select this.", this);
            EffectiveDateTextBox.Text = "";
            ddlFinYear.Focus();
        }
    }
    private bool CheckStartEndDateExistOrNot(string Start, string End)
    {
        bool status = false;

        DataTable dataTable = aIncrementDal.CheckStartEndDateExistOrNotDAL(ddlFinYear.SelectedValue, Start, End);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        EffectiveDateTextBox.Text = "";
    }



}