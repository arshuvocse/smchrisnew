using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.MPBudget;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using System.Net;
using System.Net.Mail;

public partial class MPBudget_MPBudgetEntry : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private MPBudgetCommonDAL _mpBudgetCommonDal = new MPBudgetCommonDAL();
    private int mid = 0;
    private string _userId;
    private int _EMpId=0;
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        try
        {
            _EMpId = Convert.ToInt32(Session["EmpInfoId"].ToString());
        }
        catch (Exception)
        {
            _EMpId = 0;
            //throw;
        }
        if (!IsPostBack)
        {
            ButtonVisible();
            LoadInitialDDL();
            //SetInitialRow();
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                if (mid > 0)
                {
                    hdpk.Value = mid.ToString();
                    LoadEditMode(hdpk.Value);
                }
            }


            string id2 = Request.QueryString["id2"];
            if (Session["MPBudgetEdit"] != null)
            {
                hdpk.Value = id2;
                Session["MPBudgetEdit"] = null;

                LoadEditMode(hdpk.Value);


            }
            
        }
    }
    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {
            if (Session["Status"].ToString() == "Add")
            {
                btn_Save.Visible = true;
                SavSub.Visible = true;
                btnSubmit.Visible = true;
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                editButton.Visible = true;
                UpSub.Visible = true;
                btnUpSub.Visible = true;
            }
            else if (Session["Status"].ToString() == "Delete")
            {
                delButton.Visible = true;
            }
            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("MPBudgetList.aspx");
        }
    }


    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }


    private void LoadEditMode(string masid)
    {
        using (DataTable dt = _mpBudgetCommonDal.GetMPBudgetById(Convert.ToInt32(masid)))
        {
            manualUpdateCheckBox.Enabled = false;

            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var CompanyId = dt.Rows[i]["CompanyId"].ToString().Trim();
                    ddlCompany.SelectedValue = CompanyId;
                    ddlCompany_SelectedIndexChanged(null, null);
                    ddlDepartment.SelectedValue = dt.Rows[i]["DepartmentId"].ToString().Trim();
                    ddlDepartment_OnSelectedIndexChanged(null, null);
                    ddlFinYear.SelectedValue = dt.Rows[i]["FinancialYearId"].ToString().Trim();
                    string mdid = Request.QueryString["mdid"];
                    using (DataTable dtDetails = _mpBudgetCommonDal.GetMPBudgetDetailsBymid(Convert.ToInt32(masid)))
                    {
                        ViewState["MPTable"] = dtDetails;
                        gv_MP.DataSource = dtDetails;
                        gv_MP.DataBind();
                    }

                }
            }


            DataTable AppLogComm = _mpBudgetCommonDal.GetAppLogCommById(Convert.ToInt32(masid));

            if (AppLogComm.Rows.Count > 0)
            {
                DivShow.Visible = true;
                AppLogCommentGridView.DataSource = AppLogComm;
                AppLogCommentGridView.DataBind();
            }
        }
    }
    private void LoadInitialDDL()
    {

        _commonDataLoad.GetCompanyListIntoDropdown(ddlCompany);
        ddlCompany.SelectedIndex = 1;
        ddlCompany_SelectedIndexChanged(null, null);
        //using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        //{
        //    ddlCompany.DataSource = dt;
        //    ddlCompany.DataValueField = "Value";
        //    ddlCompany.DataTextField = "TextField";
        //    ddlCompany.DataBind();
        //}

        using (DataTable dt = _commonDataLoad.GetEmpCategoryDDL())
        {
            ddlEmpCategoryEx.DataSource = dt;
            ddlEmpCategoryEx.DataValueField = "Value";
            ddlEmpCategoryEx.DataTextField = "TextField";
            ddlEmpCategoryEx.DataBind();


            dlEmpCategoryEx2.DataSource = dt;
            dlEmpCategoryEx2.DataValueField = "Value";
            dlEmpCategoryEx2.DataTextField = "TextField";
            dlEmpCategoryEx2.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetEmpType())
        {
            radEmpType.DataSource = dt;
            radEmpType.DataValueField = "Value";
            radEmpType.DataTextField = "TextField";
            radEmpType.DataBind();
        }

    }
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("MPBudgetList.aspx");
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            Session["cid"] = ddlCompany.SelectedValue;
            using (DataTable dt = _commonDataLoad.GetDDLDepartmentByCompanyId(int.Parse(ddlCompany.SelectedValue)))
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataValueField = "Value";
                ddlDepartment.DataTextField = "TextField";
                ddlDepartment.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLFinYearByCompanyId(int.Parse(ddlCompany.SelectedValue)))
            {
                ddlFinYear.DataSource = dt;
                ddlFinYear.DataValueField = "Value";
                ddlFinYear.DataTextField = "TextField";
                ddlFinYear.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetQuarterByCompanyId(int.Parse(ddlCompany.SelectedValue)))
            {
                ddlQuarter.DataSource = dt;
                ddlQuarter.DataValueField = "Value";
                ddlQuarter.DataTextField = "TextField";
                ddlQuarter.DataBind();
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
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        WhichButton.Value = "0";
        string Status = "Drafted";

        SaveInfo(Status);
    }

    private void SaveInfo(string Status)
    {
        try
        {
          

            mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
            if (gv_MP.Rows.Count > 0)
            {
                tblMPBudgetMaster bm = null;
                using (var db = new HRIS_SMCEntities())
                {
                    if (mid > 0)
                    {
////EditMode Master
                        bm = (from m in db.tblMPBudgetMasters where m.MPBudgetMasterId == mid select m).FirstOrDefault();
                        bm.CompanyId = int.Parse(ddlCompany.SelectedValue);
                        bm.DepartmentId = int.Parse(ddlDepartment.SelectedValue);
                        bm.FinancialYearId = int.Parse(ddlFinYear.SelectedValue);
                        bm.ApprovalStatus = "Drafted";
                        bm.ActionStatus = Status;
                        bm.IsActive = true;
                        bm.UpdateBy = _userId;
                        bm.UpdateDate = DateTime.Now;
                        bm.isManuallyUpdate = manualUpdateCheckBox.Checked;


                        string details_tobe_inactive_except = string.Empty;

                        db.Database.ExecuteSqlCommand("delete from  dbo.tblMPBudgetDetails WHERE  MPBudgetMasterId={0}",
                          mid);

                        ///save grid in edit mode
                        for (int i = 0; i < gv_MP.Rows.Count; i++)
                        {
                            HiddenField hdpkd = (HiddenField) gv_MP.Rows[i].FindControl("hdpkd");
                            Label lblDesignation = (Label) gv_MP.Rows[i].FindControl("lblDesignation");
                            HiddenField hdEmpCategoryId = (HiddenField) gv_MP.Rows[i].FindControl("hdEmpCategoryId");
                            Label lblEmployeeReq = (Label) gv_MP.Rows[i].FindControl("lblEmployeeReq");
                            Label lblReqSalPerEmp = (Label) gv_MP.Rows[i].FindControl("lblReqSalPerEmp");
                            Label lblReqTotalSalary = (Label) gv_MP.Rows[i].FindControl("lblReqTotalSalary");
                            HiddenField hdQuarter = (HiddenField) gv_MP.Rows[i].FindControl("hdQuarter");
                            TextBox txtDtlRemarks = (TextBox) gv_MP.Rows[i].FindControl("txtDtlRemarks");
                            HiddenField hdSalaryGrade = (HiddenField) gv_MP.Rows[i].FindControl("hdSalaryGrade");
                            HiddenField hdEmployeeType = (HiddenField) gv_MP.Rows[i].FindControl("hdEmployeeType");
                            HiddenField hdProject = (HiddenField) gv_MP.Rows[i].FindControl("hdProject");


                            int MPBudgetDetailsId = int.Parse(hdpkd.Value);
                            SaveLog(bm.MPBudgetMasterId.ToString());
                            if (mid > 0)
                            {
                                //details_tobe_inactive_except += hdpkd.Value + ",";

                                tblMPBudgetDetail bd =
                                    bd = new tblMPBudgetDetail();
                                //    (from m in db.tblMPBudgetDetails where m.MPBudgetDetailsId == MPBudgetDetailsId select m)
                                //        .FirstOrDefault();

                                bd.MPBudgetMasterId = bm.MPBudgetMasterId;
                                bd.Designation = lblDesignation.Text;
                                bd.EmpCategoryId = int.Parse(hdEmpCategoryId.Value);
                                bd.EmployeeRequisition = int.Parse(lblEmployeeReq.Text);
                                bd.ReqApproxSalary = decimal.Parse(lblReqSalPerEmp.Text);
                                bd.ReqTotalSalary = decimal.Parse(lblReqTotalSalary.Text);
                                bd.QuarterId = int.Parse(hdQuarter.Value);
                                bd.SalaryGradeId = int.Parse(hdSalaryGrade.Value);
                                bd.DtlRemarks = txtDtlRemarks.Text;
                                bd.EmployeeTypeId = int.Parse(hdEmployeeType.Value);
                                bd.ProjectId = string.IsNullOrEmpty(hdProject.Value) ? 0 : int.Parse(hdProject.Value);

                                bd.IsActive = true;
                                db.tblMPBudgetDetails.Add(bd);
                                db.SaveChanges();
                            }
                            else
                            {
                                tblMPBudgetDetail bd = new tblMPBudgetDetail();

                                bd.MPBudgetMasterId = bm.MPBudgetMasterId;
                                bd.Designation = lblDesignation.Text;
                                bd.EmpCategoryId = int.Parse(hdEmpCategoryId.Value);
                                bd.EmployeeRequisition = int.Parse(lblEmployeeReq.Text);
                                bd.ReqApproxSalary = decimal.Parse(lblReqSalPerEmp.Text);
                                bd.ReqTotalSalary = decimal.Parse(lblReqTotalSalary.Text);
                                bd.QuarterId = int.Parse(hdQuarter.Value);
                                bd.SalaryGradeId = int.Parse(hdSalaryGrade.Value);
                                bd.DtlRemarks = txtDtlRemarks.Text;
                                bd.EmployeeTypeId = int.Parse(hdEmployeeType.Value);
                                bd.ProjectId = string.IsNullOrEmpty(hdProject.Value) ? 0 : int.Parse(hdProject.Value);
                                bd.IsActive = true;
                                db.tblMPBudgetDetails.Add(bd);
                            }
                        }

                        ////make others inactive
                       // _mpBudgetCommonDal.OrphanDetailsInActive(mid, details_tobe_inactive_except.TrimEnd(','));
                        db.SaveChanges();
                    }
                    else
                    {
////New Mode Master
                        int FinYear = int.Parse(ddlFinYear.SelectedValue);
                        var YearBGT = (from m in db.tblMPBudgetMasters where m.FinancialYearId == FinYear select m)
                            .Count();

                        bm = new tblMPBudgetMaster();
                        bm.BudgetCode = "BGT-(" + ddlFinYear.SelectedItem.Text + ")-" + (YearBGT + 1);
                        bm.CompanyId = int.Parse(ddlCompany.SelectedValue);
                        bm.DepartmentId = int.Parse(ddlDepartment.SelectedValue);
                        bm.FinancialYearId = int.Parse(ddlFinYear.SelectedValue);

                        bm.isManuallyUpdate = manualUpdateCheckBox.Checked;

                        bm.ApprovalStatus = "Drafted";
                        bm.ActionStatus = Status;
                        bm.IsActive = true;
                        bm.EntryBy = _userId;
                        bm.EntryDate = DateTime.Now;

                        db.tblMPBudgetMasters.Add(bm);
                        db.SaveChanges();


                        if (manualUpdateCheckBox.Checked)
                        {
                            db.Database.ExecuteSqlCommand(@"UPDATE [dbo].[tblMPBudgetMaster]
   SET  [ApprovalStatus] = 'Approved'
     
      ,[IsActive] = 1
      
      ,[ActionStatus] = 'Approved'
      ,[ActionStatus2] = 'Approved'
      ,[isManuallyUpdate] = 1
 WHERE MPBudgetMasterId={0}",
                        bm.MPBudgetMasterId);
                        }
                        else
                        {
                            SaveLog(bm.MPBudgetMasterId.ToString());
                            
                        }

                        ///save grid in new mode
                        for (int i = 0; i < gv_MP.Rows.Count; i++)
                        {
                            HiddenField hdpkd = (HiddenField) gv_MP.Rows[i].FindControl("hdpkd");
                            Label lblDesignation = (Label) gv_MP.Rows[i].FindControl("lblDesignation");
                            HiddenField hdEmpCategoryId = (HiddenField) gv_MP.Rows[i].FindControl("hdEmpCategoryId");
                            Label lblEmployeeReq = (Label) gv_MP.Rows[i].FindControl("lblEmployeeReq");
                            Label lblReqSalPerEmp = (Label) gv_MP.Rows[i].FindControl("lblReqSalPerEmp");
                            Label lblReqTotalSalary = (Label) gv_MP.Rows[i].FindControl("lblReqTotalSalary");
                            HiddenField hdQuarter = (HiddenField) gv_MP.Rows[i].FindControl("hdQuarter");
                            TextBox txtDtlRemarks = (TextBox) gv_MP.Rows[i].FindControl("txtDtlRemarks");
                            HiddenField hdSalaryGrade = (HiddenField) gv_MP.Rows[i].FindControl("hdSalaryGrade");
                            HiddenField hdEmployeeType = (HiddenField) gv_MP.Rows[i].FindControl("hdEmployeeType");
                            HiddenField hdProject = (HiddenField) gv_MP.Rows[i].FindControl("hdProject");

                            tblMPBudgetDetail bd = new tblMPBudgetDetail();
                            bd.MPBudgetMasterId = bm.MPBudgetMasterId;
                            bd.Designation = lblDesignation.Text;
                            bd.EmpCategoryId = int.Parse(hdEmpCategoryId.Value);
                            bd.EmployeeRequisition = int.Parse(lblEmployeeReq.Text);
                            bd.ReqApproxSalary = decimal.Parse(lblReqSalPerEmp.Text);
                            bd.ReqTotalSalary = decimal.Parse(lblReqTotalSalary.Text);
                            bd.QuarterId = int.Parse(hdQuarter.Value);
                            bd.SalaryGradeId = int.Parse(hdSalaryGrade.Value);
                            bd.DtlRemarks = txtDtlRemarks.Text;
                            bd.EmployeeTypeId = int.Parse(hdEmployeeType.Value);
                            bd.ProjectId = string.IsNullOrEmpty(hdProject.Value) ? 0 : int.Parse(hdProject.Value);
                            bd.IsActive = true;
                            db.tblMPBudgetDetails.Add(bd);
                        }
                        db.SaveChanges();


                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='MPBudgetList.aspx';",
                    true);
            }
            else
            {
                AlertMessageBoxShow("Nothing to save! Please add to grid.");
            }
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
    }

    public void SaveLog(string id)
    {
        if (WhichButton.Value!="0")
        {
            int mainid
               = Convert.ToInt32(id);


            string ActionStatus = "Verified";
            //bool status = _mpBudgetCommonDal.UpdateContractural(ActionStatus, mainid);

            int commentid = _mpBudgetCommonDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                        " ");

            MPBudgetMasterAppLogDAO appLogDaoa = new MPBudgetMasterAppLogDAO();
            {
                appLogDaoa.ActionStatus = "Drafted";
                appLogDaoa.ApproveDate = DateTime.Now;
                appLogDaoa.ApproveBy = Session["UserId"].ToString();
                appLogDaoa.PreEmpInfoId = 0;
                appLogDaoa.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                appLogDaoa.MPBudgetMasterId = mainid;
                appLogDaoa.Comments = txtComment.Text.Trim();
                appLogDaoa.CommentsId = commentid;

            };
            int ida = _mpBudgetCommonDal.SavAppLog(appLogDaoa);
            _mpBudgetCommonDal.UpdateJobReqStatus2(ActionStatus, mainid);
            //if (ActionStatus == "Verified")
            {
                DataTable dtempdata = _mpBudgetCommonDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                MPBudgetMasterAppLogDAO appLogDao = new MPBudgetMasterAppLogDAO();
                {
                    appLogDao.ActionStatus = "Verified";
                    appLogDao.ApproveDate = DateTime.Now;
                    appLogDao.ApproveBy = Session["UserId"].ToString();
                    appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                    appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                    appLogDao.MPBudgetMasterId = mainid;
                    appLogDao.Comments = txtComment.Text.Trim();
                    appLogDao.CommentsId = commentid;

                };
                int idb = _mpBudgetCommonDal.SavAppLog(appLogDao);
                _mpBudgetCommonDal.UpdateJobReqStatus2(ActionStatus, mainid);

                SenMailForApprved(appLogDao.ForEmpInfoId, " Manpower Budget Approval ", @"  <br/> Dear Sir, <br/>
Manpower Budget is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
            }
        }
       
    }


    private void SenMailForApprved(int forEmpID, string mSubject, string mBody)
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
               // showMessageBox("Email has not Sent, Try Once More time");
            }
            catch (Exception exe)
            {
              //  showMessageBox("Email has not Sent, Try Once More time");
            }


            System.Threading.Thread.Sleep(100);
        }



    }


    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='MPBudgetList.aspx';",
                    true);
    }




    private void SetInitialRow()
    {

        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("MPBudgetDetailsId", typeof(string)));
        dt.Columns.Add(new DataColumn("CompanyId", typeof(string)));
        dt.Columns.Add(new DataColumn("CompanyName", typeof(string)));
        dt.Columns.Add(new DataColumn("DepartmentId", typeof(string)));
        dt.Columns.Add(new DataColumn("DepartmentName", typeof(string)));
        dt.Columns.Add(new DataColumn("FinancialYearId", typeof(string)));
        dt.Columns.Add(new DataColumn("FinancialYearDesc", typeof(string)));
        dt.Columns.Add(new DataColumn("DesignationId", typeof(string)));
        dt.Columns.Add(new DataColumn("Designation", typeof(string)));
        dt.Columns.Add(new DataColumn("ExistingEmployee", typeof(string)));
        dt.Columns.Add(new DataColumn("ExistingSalary", typeof(string)));
        dt.Columns.Add(new DataColumn("SalaryGradeId", typeof(string)));
        dt.Columns.Add(new DataColumn("GradeName", typeof(string)));
        dt.Columns.Add(new DataColumn("SalaryStepId", typeof(string)));
        dt.Columns.Add(new DataColumn("SalaryStepName", typeof(string)));
        dt.Columns.Add(new DataColumn("GExistingEmployee", typeof(string)));
        dt.Columns.Add(new DataColumn("GExistingSalary", typeof(string)));
        dt.Columns.Add(new DataColumn("EmployeeRequisition", typeof(string)));
        dt.Columns.Add(new DataColumn("ReqApproxSalary", typeof(string)));
        dt.Columns.Add(new DataColumn("ReqTotalSalary", typeof(string)));
        dt.Columns.Add(new DataColumn("QuarterId", typeof(string)));
        dt.Columns.Add(new DataColumn("QuarterName", typeof(string)));

        dr = dt.NewRow();
        dr["MPBudgetDetailsId"] = "0";
        dt.Rows.Add(dr);

        //Store the DataTable in ViewState for future reference   
        ViewState["MPTable"] = dt;

        //Bind the Gridview   
        gv_MP.DataSource = dt;
        gv_MP.DataBind();

    }
    protected void btnAdd_OnClick(object sender, EventArgs e)
    {
        var cid = ddlCompany.SelectedValue;
        var quartar = ddlQuarter.SelectedValue;
        var dept = ddlDepartment.SelectedValue;
        var finYear = ddlFinYear.SelectedValue;

        var grade = String.Empty;
        var desig = String.Empty;
        var step = String.Empty;

        var FilterType = string.Empty;

        ////check for new designation
        //if (chk_NewDesignation.Checked)
        //{////New
        //    FilterType = "new";
        //    grade = ddlGradeNew.SelectedValue;
        //    desig = ddlDesignationNew.SelectedValue;
        //    step = ddlStepNew.SelectedValue;
        //}
        //else////Existing
        //{
        //    FilterType = "ex";
        //    grade = ddlGradeEx.SelectedValue;
        //    desig = "";////TODO ddlDesignationEx.SelectedValue;
        //    step = ddlStepEx.SelectedValue;
        //}
        //FilterType = "ex";
        grade = ddlGradeEx2.SelectedValue;
        desig = txt_Designation.Text;
        step = ddlStepEx.SelectedValue;


        //for (int i = 0; i < radEmpType.Items.Count; i++)
        //{
        //    if (radEmpType.Items[i].Text.Trim() == "Contractual")
        //    {
        //        if (ddlProjectName.SelectedIndex == 0)
        //        {
        //            AlertMessageBoxShow("Project Name required...");
        //            return;
        //        }
        //    }
        //}

        #region Validation to add grid

        if (cid == "-1")
        {
            AlertMessageBoxShow("Company required...");
            return;
        }
        if (grade == "-1" || grade == "")
        {
            AlertMessageBoxShow("Grade required...");
            return;
        }
        if (desig == "-1" || desig == "")
        {
            AlertMessageBoxShow("Designation required...");
            return;
        }
        //if (step == "-1" || step == "")
        //{
        //    AlertMessageBoxShow("Step required...");
        //    return;
        //}
        if (quartar == "-1" || quartar == "")
        {
            AlertMessageBoxShow("Quarter required...");
            return;
        }
        if (dept == "-1" || dept == "")
        {
            AlertMessageBoxShow("Department required...");
            return;
        }
        if (finYear == "-1" || finYear == "")
        {
            AlertMessageBoxShow("Financial Year required...");
            return;
        }
        if (string.IsNullOrEmpty(txt_EmployeeRequisition.Text))
        {
            AlertMessageBoxShow("Employee Requisition required...");
            return;
        }
        if (string.IsNullOrEmpty(txt_ReqApproxSalary.Text))
        {
            AlertMessageBoxShow("Salary Range From required...");
            return;
        }
        if (string.IsNullOrEmpty(lbl_ReqTotalSalary.Text))
        {
            AlertMessageBoxShow("Salary Range To required...");
            return;
        }
        int r = 0;
        for ( r = 0; r < radEmpType.Items.Count; r++)
        {
            if (radEmpType.Items[r].Selected)
            {
                break;
            }
        }
        if (r==radEmpType.Items.Count)
        {
            AlertMessageBoxShow("Select Employee Type...");
            return;
        }

        if (ViewState["MPTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["MPTable"];
            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
            {
                if ((dtCurrentTable.Rows[i]["SalaryGradeId"].ToString() == grade)
                    && (dtCurrentTable.Rows[i]["DesignationId"].ToString() == desig)
                    && (dtCurrentTable.Rows[i]["SalaryStepId"].ToString() == step)
                    && (dtCurrentTable.Rows[i]["QuarterId"].ToString() == quartar))
                {
                    AlertMessageBoxShow("This Grade, Designation, Step, Quarter already exists in below table...");
                    return;
                }
            }
        }
        #endregion

        AddNewRowToGrid(FilterType, grade, desig, step);

        #region Form Reset
        //ddlGradeNew.SelectedValue = "-1";
        //ddlDesignationNew.SelectedValue = "-1";
        //ddlStepNew.SelectedValue = "-1";

        //chk_NewDesignation.Checked = false;

        //ddlGradeEx.SelectedValue = "-1";
        ////TODO ddlDesignationEx.SelectedValue = "-1";
        txt_Designation.Text = string.Empty;
        //ddlStepEx.SelectedValue = "-1";

        //lblExGradeTotalEmp.Text = String.Empty;
        ////TODO lblExGradeTotalSal.Text = String.Empty;
        ///lblExGradeMaxSal.Text = String.Empty;
        ///lblExGradeMinSal.Text = String.Empty;

        txt_EmployeeRequisition.Text = String.Empty;
        txt_ReqApproxSalary.Text = String.Empty;
        lbl_ReqTotalSalary.Text = String.Empty;
        txtRemarks.Text = String.Empty;

        //ddlQuarter.DataSource = null;
        //ddlQuarter.DataBind();
        #endregion
    }
    private void AddNewRowToGrid(string FilterType, string grade, string desig, string step)
    {
        if (ViewState["MPTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["MPTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["MPBudgetDetailsId"] = "0";
                drCurrentRow["MPBudgetDetailsId"] = "0";
                drCurrentRow["Designation"] = txt_Designation.Text;
                drCurrentRow["EmpCategoryName"] = dlEmpCategoryEx2.SelectedItem.Text;
                drCurrentRow["EmpCategoryId"] = dlEmpCategoryEx2.SelectedValue;
                drCurrentRow["SalaryGradeId"] = ddlGradeEx2.SelectedValue;
                drCurrentRow["GradeName"] = ddlGradeEx2.SelectedItem.Text;
                drCurrentRow["EmployeeRequisition"] = txt_EmployeeRequisition.Text.Trim();
                drCurrentRow["ReqApproxSalary"] = txt_ReqApproxSalary.Text.Trim();
                drCurrentRow["ReqTotalSalary"] = lbl_ReqTotalSalary.Text;
                drCurrentRow["QuarterId"] = ddlQuarter.SelectedValue;
                drCurrentRow["QuarterName"] = ddlQuarter.SelectedItem.Text;
                drCurrentRow["DtlRemarks"] = txtRemarks.Text;
                drCurrentRow["EmployeeType"] = radEmpType.SelectedItem.Text;
                drCurrentRow["EmployeeTypeId"] = radEmpType.SelectedValue.Trim();
                if (radEmpType.SelectedValue.Trim() == "2")
                {
                    drCurrentRow["Project"] = "(" + ddlProjectName.SelectedItem.Text + ")";
                    drCurrentRow["ProjectId"] = ddlProjectName.SelectedValue;
                }

                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["MPTable"] = dtCurrentTable;
                //Rebind the Grid with the current data to reflect changes   
                gv_MP.DataSource = dtCurrentTable;
                gv_MP.DataBind();
            }
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("MPBudgetDetailsId", typeof(string)));
            dt.Columns.Add(new DataColumn("FilterType", typeof(string)));
            dt.Columns.Add(new DataColumn("DesignationId", typeof(string)));
            dt.Columns.Add(new DataColumn("Designation", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpCategoryId", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpCategoryName", typeof(string)));
            dt.Columns.Add(new DataColumn("SalaryGradeId", typeof(string)));
            dt.Columns.Add(new DataColumn("GradeName", typeof(string)));
            dt.Columns.Add(new DataColumn("SalaryStepId", typeof(string)));
            dt.Columns.Add(new DataColumn("SalaryStepName", typeof(string)));
            dt.Columns.Add(new DataColumn("EmployeeRequisition", typeof(string)));
            dt.Columns.Add(new DataColumn("ReqApproxSalary", typeof(string)));
            dt.Columns.Add(new DataColumn("ReqTotalSalary", typeof(string)));
            dt.Columns.Add(new DataColumn("QuarterId", typeof(string)));
            dt.Columns.Add(new DataColumn("QuarterName", typeof(string)));
            dt.Columns.Add(new DataColumn("DtlRemarks", typeof(string)));

            dt.Columns.Add(new DataColumn("EmployeeType", typeof(string)));
            dt.Columns.Add(new DataColumn("EmployeeTypeId", typeof(string)));
            dt.Columns.Add(new DataColumn("Project", typeof(string)));
            dt.Columns.Add(new DataColumn("ProjectId", typeof(string)));

            dr = dt.NewRow();
            dr["MPBudgetDetailsId"] = "0";
            dr["Designation"] = txt_Designation.Text;
            dr["EmpCategoryName"] = dlEmpCategoryEx2.SelectedItem.Text;
            dr["EmpCategoryId"] = dlEmpCategoryEx2.SelectedValue;
            dr["SalaryGradeId"] = ddlGradeEx2.SelectedValue;
            dr["GradeName"] = ddlGradeEx2.SelectedItem.Text;
            dr["EmployeeRequisition"] = txt_EmployeeRequisition.Text.Trim();
            dr["ReqApproxSalary"] = txt_ReqApproxSalary.Text.Trim();
            dr["ReqTotalSalary"] = lbl_ReqTotalSalary.Text;
            dr["QuarterId"] = ddlQuarter.SelectedValue;
            dr["QuarterName"] = ddlQuarter.SelectedItem.Text;
            dr["DtlRemarks"] = txtRemarks.Text;
            dr["EmployeeType"] = radEmpType.SelectedItem.Text;
            dr["EmployeeTypeId"] = radEmpType.SelectedValue.Trim();
            if (radEmpType.SelectedValue.Trim() == "2")
            {
                dr["Project"] = "("+ ddlProjectName.SelectedItem.Text+")";
                dr["ProjectId"] = ddlProjectName.SelectedValue;
            }
          


            //dr["FilterType"] = FilterType;
            //dr["DesignationId"] = 0;
            ////TODO dr["Designation"] =  FilterType == "new" ? ddlDesignationNew.SelectedItem.Text : ddlDesignationEx.SelectedItem.Text;

            //dr["GradeName"] = grade;
            //dr["GradeName"] =  FilterType == "new" ? ddlGradeNew.SelectedItem.Text : ddlGradeEx.SelectedItem.Text;

            //dr["SalaryStepId"] = step;
            //dr["SalaryStepName"]  =  FilterType == "new" ? ddlStepNew.SelectedItem.Text : ddlStepEx.SelectedItem.Text;
            //dr["SalaryStepName"]  = ddlStepEx.SelectedItem.Text;


            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["MPTable"] = dt;
            //Bind the Gridview   
            gv_MP.DataSource = dt;
            gv_MP.DataBind();
        }
        //Set Previous Data on Postbacks   
        SetPreviousData();
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["MPTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["MPTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField hdpkd = (HiddenField)gv_MP.Rows[rowIndex].FindControl("hdpkd");
                    //HiddenField hdFilterType = (HiddenField)gv_MP.Rows[rowIndex].FindControl("hdFilterType");
                    //HiddenField hdDesignation = (HiddenField)gv_MP.Rows[rowIndex].FindControl("hdDesignation");
                    Label lblDesignation = (Label)gv_MP.Rows[rowIndex].FindControl("lblDesignation");
                    HiddenField hdEmpCategoryId = (HiddenField)gv_MP.Rows[rowIndex].FindControl("hdEmpCategoryId");
                    Label lblEmpCategoryName = (Label)gv_MP.Rows[rowIndex].FindControl("lblEmpCategoryName");

                    HiddenField hdSalaryGrade = (HiddenField)gv_MP.Rows[rowIndex].FindControl("hdSalaryGrade");
                    Label lblSalaryGrade = (Label)gv_MP.Rows[rowIndex].FindControl("lblSalaryGrade");
                    //HiddenField hdSalaryStep = (HiddenField)gv_MP.Rows[rowIndex].FindControl("hdSalaryStep");
                    //Label lblSalaryStep = (Label)gv_MP.Rows[rowIndex].FindControl("lblSalaryStep");
                    Label lblEmpReq = (Label)gv_MP.Rows[rowIndex].FindControl("lblEmployeeReq");
                    Label lblReqSalPerEmp = (Label)gv_MP.Rows[rowIndex].FindControl("lblReqSalPerEmp");
                    Label lblReqTotalSalary = (Label)gv_MP.Rows[rowIndex].FindControl("lblReqTotalSalary");
                    HiddenField hdQuarter = (HiddenField)gv_MP.Rows[rowIndex].FindControl("hdQuarter");
                    Label lblQuarter = (Label)gv_MP.Rows[rowIndex].FindControl("lblQuarter");
                    TextBox txtDtlRemarks = (TextBox)gv_MP.Rows[rowIndex].FindControl("txtDtlRemarks");

                    HiddenField hdProject = (HiddenField)gv_MP.Rows[rowIndex].FindControl("hdProject");
                    Label lblProject = (Label)gv_MP.Rows[rowIndex].FindControl("lblProject");

                    HiddenField hdEmployeeType = (HiddenField)gv_MP.Rows[rowIndex].FindControl("hdEmployeeType");
                    Label lblEmployeeType = (Label)gv_MP.Rows[rowIndex].FindControl("lblEmployeeType");

                    if (i < dt.Rows.Count - 1)
                    {
                        hdpkd.Value = dt.Rows[i]["MPBudgetDetailsId"].ToString();
                        lblDesignation.Text = dt.Rows[i]["Designation"].ToString();
                        hdEmpCategoryId.Value = dt.Rows[i]["EmpCategoryId"].ToString();
                        lblEmpCategoryName.Text = dt.Rows[i]["EmpCategoryName"].ToString();
                        hdSalaryGrade.Value = dt.Rows[i]["SalaryGradeId"].ToString();
                        lblSalaryGrade.Text = dt.Rows[i]["GradeName"].ToString();
                        lblEmpReq.Text = dt.Rows[i]["EmployeeRequisition"].ToString();
                        lblReqSalPerEmp.Text = dt.Rows[i]["ReqApproxSalary"].ToString();
                        lblReqTotalSalary.Text = dt.Rows[i]["ReqTotalSalary"].ToString();
                        hdQuarter.Value = dt.Rows[i]["QuarterId"].ToString();
                        lblQuarter.Text = dt.Rows[i]["QuarterName"].ToString();
                        txtDtlRemarks.Text = dt.Rows[i]["DtlRemarks"].ToString();

                        hdProject.Value = dt.Rows[i]["ProjectId"].ToString();
                        lblProject.Text = dt.Rows[i]["Project"].ToString();

                        hdEmployeeType.Value = dt.Rows[i]["EmployeeTypeId"].ToString();
                        lblEmployeeType.Text = dt.Rows[i]["EmployeeType"].ToString();

                    }

                    rowIndex++;
                }
            }
        }
    }
    protected void lb_Remove_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        //HiddenField hdpkd = (HiddenField)gv_MP.Rows[rowID].FindControl("hdpkd");
        //int pkd = Int32.Parse(hdpkd.Value);

        //var db = new HRIS_SMCEntities();
        //tblInterviewBoardSetupDetail IVDetails = (from emd in db.tblInterviewBoardSetupDetails where emd.BoardDetailsId == pkd select emd).FirstOrDefault();
        //IVDetails.IsActive = false;
        ////db.tblInterviewBoardSetupDetails.Add(IVDetails);
        //db.SaveChanges();


        if (ViewState["MPTable"] != null)
        {

            DataTable dt = (DataTable)ViewState["MPTable"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["MPTable"] = dt;

                //Re bind the GridView for the updated data  
                gv_MP.DataSource = dt;
                gv_MP.DataBind();
            }
            else
            {
                ViewState["MPTable"] = null;

                //Re bind the GridView for the updated data  
                gv_MP.DataSource = null;
                gv_MP.DataBind();
            }


        }

        //Set Previous Data on Postbacks  
        SetPreviousData();
    }
    protected void lb_Edit_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;

        if (ViewState["MPTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["MPTable"];

            dlEmpCategoryEx2.SelectedValue = dt.Rows[rowID]["EmpCategoryId"].ToString();
            dlEmpCategoryEx2_OnSelectedIndexChanged(null, null);

            radEmpType.SelectedValue = dt.Rows[rowID]["EmployeeTypeId"].ToString().Trim();
            //radEmpType.Items.FindByValue(dt.Rows[rowID]["EmployeeTypeId"].ToString().Trim()).Selected = true;
            radEmpType_OnSelectedIndexChanged(null, null);
            if (dt.Rows[rowID]["EmployeeTypeId"].ToString().Trim() == "2")
            {
                ddlProjectName.SelectedValue = dt.Rows[rowID]["ProjectId"].ToString();
            }


            txt_Designation.Text = dt.Rows[rowID]["Designation"].ToString();
            ddlGradeEx2.SelectedValue = dt.Rows[rowID]["SalaryGradeId"].ToString();
            ddlQuarter.SelectedValue = dt.Rows[rowID]["QuarterId"].ToString();

            txt_EmployeeRequisition.Text = dt.Rows[rowID]["EmployeeRequisition"].ToString();
            txt_ReqApproxSalary.Text = dt.Rows[rowID]["ReqApproxSalary"].ToString();
            lbl_ReqTotalSalary.Text = dt.Rows[rowID]["ReqTotalSalary"].ToString();
            txtRemarks.Text = dt.Rows[rowID]["DtlRemarks"].ToString();

          

            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["MPTable"] = dt;
                //Re bind the GridView for the updated data  
                gv_MP.DataSource = dt;
                gv_MP.DataBind();
            }
            else
            {
                ViewState["MPTable"] = null;
                //Re bind the GridView for the updated data  
                gv_MP.DataSource = null;
                gv_MP.DataBind();
            }
        }
        //Set Previous Data on Postbacks  
        SetPreviousData();
    }

    //private void ResetRowID(DataTable dt)
    //{
    //    int rowNumber = 1;
    //    if (dt.Rows.Count > 0)
    //    {
    //        foreach (DataRow row in dt.Rows)
    //        {
    //            row[0] = rowNumber;
    //            rowNumber++;
    //        }
    //    }
    //}

    //protected void txt_ReqApproxSalary_OnTextChanged(object sender, EventArgs e)
    //{
    //    var EmployeeRequisition = decimal.Parse(txt_EmployeeRequisition.Text.Trim());
    //    var ReqApproxSalary = decimal.Parse(txt_ReqApproxSalary.Text.Trim());
    //    lbl_ReqTotalSalary.Text = (EmployeeRequisition * ReqApproxSalary).ToString();
    //}

    //protected void txt_EmployeeRequisition_OnTextChanged(object sender, EventArgs e)
    //{
    //    var EmployeeRequisition = decimal.Parse(string.IsNullOrEmpty(txt_EmployeeRequisition.Text.Trim())?"0": txt_EmployeeRequisition.Text.Trim());
    //    var ReqApproxSalary = decimal.Parse(string.IsNullOrEmpty(txt_ReqApproxSalary.Text.Trim())?"0" :txt_ReqApproxSalary.Text.Trim());
    //    lbl_ReqTotalSalary.Text = (EmployeeRequisition * ReqApproxSalary).ToString();
    //}

    protected void ddlGradeEx_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGradeEx.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLDesignationByGrade(int.Parse(ddlGradeEx.SelectedValue)))
            {
                ////TODO
                //ddlDesignationEx.DataSource = dt;
                //ddlDesignationEx.DataValueField = "Value";
                //ddlDesignationEx.DataTextField = "TextField";
                //ddlDesignationEx.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLStepByGrade(int.Parse(ddlGradeEx.SelectedValue)))
            {
                ddlStepEx.DataSource = dt;
                ddlStepEx.DataValueField = "Value";
                ddlStepEx.DataTextField = "TextField";
                ddlStepEx.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetGradeExChangeInfoNew(int.Parse(ddlGradeEx.SelectedValue)))
            {
                if (dt.Rows.Count > 0)
                {
                    lblExGradeTotalEmp.Text = dt.Rows[0]["ExGradeTotalEmp"].ToString();
                    //lblExGradeTotalSal.Text = dt.Rows[0]["ExGradeTotalSal"].ToString();
                    lblExGradeMaxSal.Text = dt.Rows[0]["ExGradeMaxSal"].ToString();
                    lblExGradeMinSal.Text = dt.Rows[0]["ExGradeMinSal"].ToString();
                }

            }
        }
    }

    protected void ddlDesignationEx_OnSelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlStepEx_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStepEx.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetStepExChangeInfo(int.Parse(ddlStepEx.SelectedValue)))
            {
                if (dt.Rows.Count > 0)
                {
                    ////TODO lblExStepTotalEmp.Text = dt.Rows[0]["ExStepTotalEmp"].ToString();
                }

            }
        }
    }

    protected void ddlDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLGradeByDepartment(int.Parse(ddlDepartment.SelectedValue)))
            {
                ddlGradeEx.DataSource = dt;
                ddlGradeEx.DataValueField = "Value";
                ddlGradeEx.DataTextField = "TextField";
                ddlGradeEx.DataBind();
            }
            ddlEmpCategoryEx.SelectedValue = null;

            lblExGradeTotalEmp.Text = "";
            lblExGradeMinSal.Text = "";
            lblExGradeMaxSal.Text = "";
        }
    }

    //protected void chk_NewDesignation_OnCheckedChanged(object sender, EventArgs e)
    //{
    //    if (chk_NewDesignation.Checked)
    //    {
    //        using (DataTable dt = _commonDataLoad.GetEmpCategoryDDL())
    //        {
    //            ddlEmpCategoryNew.DataSource = dt;
    //            ddlEmpCategoryNew.DataValueField = "Value";
    //            ddlEmpCategoryNew.DataTextField = "TextField";
    //            ddlEmpCategoryNew.DataBind();
    //        }

    //        using (DataTable dt = _commonDataLoad.GetDDLGradeNew(int.Parse(ddlCompany.SelectedValue)))
    //        {
    //            ddlGradeNew.DataSource = dt;
    //            ddlGradeNew.DataValueField = "Value";
    //            ddlGradeNew.DataTextField = "TextField";
    //            ddlGradeNew.DataBind();
    //        }
    //        txt_EmployeeRequisition.Text = "0";
    //        txt_ReqApproxSalary.Text = "0";
    //        lbl_ReqTotalSalary.Text = "0";
    //    }
    //    else
    //    {
    //        ddlGradeNew.DataSource = null;
    //        ddlGradeNew.DataBind();

    //        ddlDesignationNew.DataSource = null;
    //        ddlDesignationNew.DataBind();

    //        ddlStepNew.DataSource = null;
    //        ddlStepNew.DataBind();


    //    }
    //}

    //protected void ddlGradeNew_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlGradeNew.SelectedIndex>0)
    //    {
    //        using (DataTable dt = _commonDataLoad.GetDDLDesignationByGrade(int.Parse(ddlGradeNew.SelectedValue)))
    //        {
    //            ddlDesignationNew.DataSource = dt;
    //            ddlDesignationNew.DataValueField = "Value";
    //            ddlDesignationNew.DataTextField = "TextField";
    //            ddlDesignationNew.DataBind();
    //        }

    //        using (DataTable dt = _commonDataLoad.GetDDLStepByGrade(int.Parse(ddlGradeNew.SelectedValue)))
    //        {
    //            ddlStepNew.DataSource = dt;
    //            ddlStepNew.DataValueField = "Value";
    //            ddlStepNew.DataTextField = "TextField";
    //            ddlStepNew.DataBind();
    //        }
    //    }
    //}


    protected void ddlEmpCategoryEx_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpCategoryEx.SelectedIndex > 0)
        {
            if (ddlDepartment.SelectedIndex > 0)
            {
                using (DataTable dt = _commonDataLoad.GetDDLGradeByDepartmentAndCategory(int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlEmpCategoryEx.SelectedValue)))
                {
                    ddlGradeEx.DataSource = dt;
                    ddlGradeEx.DataValueField = "Value";
                    ddlGradeEx.DataTextField = "TextField";
                    ddlGradeEx.DataBind();
                }
            }
            else
            {
                AlertMessageBoxShow("Please select Department first...");
            }
        }
    }

    //protected void ddlEmpCategoryNew_OnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlEmpCategoryNew.SelectedIndex > 0)
    //    {
    //        using (DataTable dt = _commonDataLoad.GetDDLGradeNewByCategory(int.Parse(ddlCompany.SelectedValue), int.Parse(ddlEmpCategoryNew.SelectedValue)))
    //        {
    //            ddlGradeNew.DataSource = dt;
    //            ddlGradeNew.DataValueField = "Value";
    //            ddlGradeNew.DataTextField = "TextField";
    //            ddlGradeNew.DataBind();
    //        }
    //    }
    //}

    protected void radEmpType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlProjectName.DataSource = null;
        ddlProjectName.DataBind();
        if (radEmpType.SelectedIndex >= 0)
        {
            if (radEmpType.SelectedValue.Trim() == "2")
            {
                if (ddlCompany.SelectedIndex > 0)
                {
                    ddlProjectName.Enabled = true;
                    using (DataTable dt = _commonDataLoad.GetProjectNameDDL(int.Parse(ddlCompany.SelectedValue)))
                    {
                        ddlProjectName.DataSource = dt;
                        ddlProjectName.DataValueField = "Value";
                        ddlProjectName.DataTextField = "TextField";
                        ddlProjectName.DataBind();
                    }

                }
                else
                {
                    for (int i = 0; i < radEmpType.Items.Count; i++)
                    {
                        radEmpType.Items[i].Selected = false;
                    }
                    AlertMessageBoxShow("Company required...!");
                }

            }
            else
            {
                ddlProjectName.Enabled = false;
                ddlProjectName.DataSource = null;
                ddlProjectName.DataBind();
            }
        }
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        WhichButton.Value = "0";
        string Status = "Drafted";
        SaveInfo(Status);
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
          mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
        //if (gv_MP.Rows.Count > 0)
        {
            tblMPBudgetMaster bm = null;
            using (var db = new HRIS_SMCEntities())
            {
                if (mid != null)

                    if (_commonDataLoad.DeleteManpowerInfoById(Convert.ToInt32(mid),Session["UserId"].ToString()))
                    {
                       // _commonDataLoad.DeleteManpowerDeleteInfoById(Convert.ToString(mid));
                       // aShowMessage.ShowMessageBox(aMessages.DeleteMessage, this);
                    }


            }
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='MPBudgetList.aspx';",
                    true);
        }

        
    }

    public void RangeValidation()
    {
        try
        {
            decimal start = string.IsNullOrEmpty(txt_ReqApproxSalary.Text)
                ? 0
                : Convert.ToDecimal(txt_ReqApproxSalary.Text);
            decimal end = string.IsNullOrEmpty(lbl_ReqTotalSalary.Text)
                ? 0
                : Convert.ToDecimal(lbl_ReqTotalSalary.Text);
            if (start>end)
            {
                
                aShowMessage.ShowMessageBox("Please Input Valid Range!!",this);
            }
            
        }
        catch (Exception ex)
        {

            
        }
    }
    protected void txt_ReqApproxSalary_OnTextChanged(object sender, EventArgs e)
    {
        if (lbl_ReqTotalSalary.Text != string.Empty)
        {
            try
            {
                decimal start = string.IsNullOrEmpty(txt_ReqApproxSalary.Text)
                    ? 0
                    : Convert.ToDecimal(txt_ReqApproxSalary.Text);
                decimal end = string.IsNullOrEmpty(lbl_ReqTotalSalary.Text)
                    ? 0
                    : Convert.ToDecimal(lbl_ReqTotalSalary.Text);
                if (start > end)
                {
                    lbl_ReqTotalSalary.Text = string.Empty;
                    aShowMessage.ShowMessageBox("Salary Range from must be small", this);
                }

            }
            catch (Exception ex)
            {


            } 

        }
        
    }

    protected void lbl_ReqTotalSalary_OnTextChanged(object sender, EventArgs e)
    {
        if (txt_ReqApproxSalary.Text != string.Empty)
        {
            try
            {
                decimal start = string.IsNullOrEmpty(txt_ReqApproxSalary.Text)
                    ? 0
                    : Convert.ToDecimal(txt_ReqApproxSalary.Text);
                decimal end = string.IsNullOrEmpty(lbl_ReqTotalSalary.Text)
                    ? 0
                    : Convert.ToDecimal(lbl_ReqTotalSalary.Text);
                if (start > end)
                {
                    txt_ReqApproxSalary.Text = string.Empty;
                    aShowMessage.ShowMessageBox("Salary Range from must be small", this);
                }

            }
            catch (Exception ex)
            {


            }

        }
        //RangeValidation();
    }

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        WhichButton.Value = "1";
        string Status = "submitted";

        if (validforSupper())
        {
            SaveInfo(Status);
        }

       
    }

    private bool validforSupper()
    {
        if (manualUpdateCheckBox.Checked==false)
        {
            tblEmpGeneralInfo bm;
            using (var db = new HRIS_SMCEntities())
            {
                bm = (from m in db.tblEmpGeneralInfoes where m.EmpInfoId == _EMpId select m).FirstOrDefault();

                try
                {
                    int empid = (int)bm.ReportingEmpId;

                    if (empid==null)
                    {
                        AlertMessageBoxShow("Supervisor not Found");
                        return false;
                    }
                }
                catch (Exception)
                {
                    AlertMessageBoxShow("Supervisor not Found");
                    return false;
                    //throw;
                }
            }
        }
        return true;
    }

    protected void btnUpSub_OnClick(object sender, EventArgs e)
    {
        WhichButton.Value = "1";
        string Status = "submitted";
        if (validforSupper())
        {
            SaveInfo(Status);
        }
    }

    protected void dlEmpCategoryEx2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (dlEmpCategoryEx2.SelectedIndex > 0)
        {
             
                using (DataTable dt = _commonDataLoad.GetDDLGradeBycatID(int.Parse(dlEmpCategoryEx2.SelectedValue)))
                {
                    ddlGradeEx2.DataSource = dt;
                    ddlGradeEx2.DataValueField = "Value";
                    ddlGradeEx2.DataTextField = "TextField";
                    ddlGradeEx2.DataBind();
                }
            
        }
        else
        {
            ddlGradeEx2.Items.Clear();
        }
    }

    protected void ddlGradeEx2_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}