using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.ExitManagement_DAL;
using DAL.Survey;
using DAL.Transfer_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using Library.DAO.HRM_Entities;

public partial class Survey_ProbationEvaluationForm : System.Web.UI.Page
{
    SurveyCommonDAL _surveyCommonDal = new SurveyCommonDAL();
    ProbationperiodDAL aProbationperiodDal=new ProbationperiodDAL();
    EmpTransferAndRedesignationDAL aEmpTransferAndRedesignationDal = new EmpTransferAndRedesignationDAL();

    private int mid = 0;
    private string _userId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {
            exProDate.Attributes.Add("readonly", "readonly");

            LoadDropDownList();
            //LoadGrid();
            string id = Request.QueryString["id"];
            if (Session["ProbEdit"]!=null)
            {
                hdpk.Value = id;
                Session["ProbEdit"] = null;
                GetProbData(id);
            }
            else
            {
                GetEmpData(id);
                hfEmpInfoId.Value = id;

                DataTable dtadata = aEmpTransferAndRedesignationDal.LoadSuperviseEmployeeActive(hfEmpInfoId.Value);


                loadGridView.DataSource = dtadata;
                loadGridView.DataBind();
            }

            
            
        }
    }

    public void GetProbData(string id)
    {
        DataTable dt = aProbationperiodDal.GetProbationMaster(id);
        if (dt.Rows.Count>0)
        {
            GetEmpData(dt.Rows[0]["EmpInfoId"].ToString());
            ddlreason.SelectedItem.Text = dt.Rows[0]["ProbationEndReason"].ToString();
            ExtendProbationDropDownList.Items[1].Selected = Convert.ToBoolean(dt.Rows[0]["ExProbation"].ToString());
            ExtendProbationDropDownList.Items[2].Selected = Convert.ToBoolean(dt.Rows[0]["ProbationEnd"].ToString());

            try
            {
                exProDate.Text = Convert.ToDateTime(dt.Rows[0]["ExProDate"].ToString()).ToString("dd-MMM-yyyy");
            }
            catch (Exception)
            {

                // throw;
            }
            if (ExtendProbationDropDownList.Items[1].Selected)
            {
                exppr.Visible = true;

            }
            else
            {
                exppr.Visible = false;
                probendreason.Visible = true;
                if (exProDate.Text != string.Empty)
                {
                    exppr.Visible = true;
                   // ddlreason.SelectedIndex = 1;
                }
                else
                {
                    ddlreason.SelectedIndex = 0;
                }
            }
            
        }
    }
    private void LoadDropDownList()
    {
        _surveyCommonDal.LoadCompanyDropDownList(ddlCompany);

    }

    public void GetEmpData(string id)
    {
        DataTable dtempdata = aProbationperiodDal.LoadEmployeeInfo(id);
        if (dtempdata.Rows.Count>0)
        {
            empIdHiddenField.Value = dtempdata.Rows[0]["EmpInfoId"].ToString();
            empCode.Text = dtempdata.Rows[0]["EmpMasterCode"].ToString();
            empName.Text = dtempdata.Rows[0]["EmpName"].ToString();
            ddlCompany.SelectedValue = dtempdata.Rows[0]["CompanyId"].ToString();
            LoadGrid(dtempdata.Rows[0]["CompanyId"].ToString());
            try
            {
                dtJoining.Text = Convert.ToDateTime(dtempdata.Rows[0]["DateOfJoin"].ToString()).ToString("dd-MMM-yyyy");
            }
            catch (Exception ex)
            {
                
                
            }

            ddlDivision.Text = dtempdata.Rows[0]["DivisionName"].ToString();
            ddlDesignation.Text = dtempdata.Rows[0]["Designation"].ToString();
        }
    }
    private void LoadGrid(string id)
    {
        using (DataTable dt = _surveyCommonDal.GetProbationEvaluationRatingByCompanyId(id))
        {
            gv_ProbationEvaluation.DataSource = dt;
            gv_ProbationEvaluation.DataBind();
        }
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

    protected void txt_EmpName_OnTextChanged(object sender, EventArgs e)
    {
        SetEmployeeInfo();

        if (hfEmpInfoId.Value != "")
        {
            DataTable aTable = _surveyCommonDal.LoadEmployeeInfo(hfEmpInfoId.Value, ddlCompany.SelectedValue);

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


    private void SetEmployeeInfo()
    {
        string empName = txt_EmpName.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');
            hfEmpInfoId.Value = emp[0];
        }
        else
        {
            hfEmpInfoId.Value = "";
             ShowMessageBox("Input Correct Data !!");
        }

        txt_EmpName.Text = "";
    }

    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public bool Validation()
    {
        DataTable dtempdata =
            aProbationperiodDal.GetEmpInfo(" WHERE EmpInfoId='" +
                                           Session["EmpInfoid"].ToString() + "'");
        if (dtempdata.Rows.Count<1)
        {
            ShowMessageBox("Set supervisor for this employee");
            return false;
        }

        if (ExtendProbationDropDownList.SelectedValue=="0")
        {
            ShowMessageBox("Action Type is  Required!!!");
            return false;
        }


        if (ExtendProbationDropDownList.SelectedValue == "2")
        {
          
        }

        if (exProDate.Text=="")
        {
            ShowMessageBox("Date is  Required!!!");
            exProDate.Focus();
            return false;
        }
        //DataTable dtexist = aProbationperiodDal.GetPreEmpData(empIdHiddenField.Value);
        //if (dtexist.Rows.Count>0)
        //{
        //    ShowMessageBox("Already Exist !!");
        //    return false;
        //}
        //for (int i = 0; i < gv_ProbationEvaluation.Rows.Count; i++)
        //{
        //    bool countselected = false;
        //    for (int j = 0; j < ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items.Count; j++)
        //    {
        //        if (((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[j].Selected)
        //        {
        //            countselected = true;
        //        }
        //    }
        //    if (countselected == false)
        //    {
        //        ShowMessageBox("Select Rating For Every Category");
        //        return false;
        //    }
        //}
        if (ddlreason.SelectedValue=="1")
        {
            if (loadGridView.Rows.Count == 0)
            {

            }
            else
            {
                ShowMessageBox("Directly Supervised Employee List must be Empty !!!");
                //  JobLeftDateTextBox.Focus();
                return false;
            }
        } 
       

        return true;
    }

    public void SaveDATA()
    {
        if (Validation())
        {
            ProbationEvaluationMasterDAO aEvaluationMasterDao = new ProbationEvaluationMasterDAO()
            {
                EmpInfoId = Convert.ToInt32(empIdHiddenField.Value),
                DeptHeadObserv = txt_DepartmentHeadObservation.Text,
                DivHeadOvserv = txt_DivisionHeadObservation.Text,
                ExProbation = ExtendProbationDropDownList.SelectedValue == "1",
                ProbationEnd = ExtendProbationDropDownList.SelectedValue == "2",
                SupervisorObserv = txt_SupervisorObservation.Text,
            };
            if (exProDate.Text != string.Empty)
            {
                aEvaluationMasterDao.ExProDate = Convert.ToDateTime(exProDate.Text);
                if (ExtendProbationDropDownList.SelectedValue == "1")
                {
                    if (manualUpdateCheckBox.Checked)
                    {
                        Int32 empGenId = 0;
                        string date = "";



                        empGenId = Convert.ToInt32(hfEmpInfoId.Value);


                        date = Convert.ToDateTime(exProDate.Text.Trim()).ToString();


                        UpdateEmployeeProbitionExtendeddate(empGenId, date);
                    }
                }

            }
            if (ddlreason.SelectedValue == "2")
            {
                aEvaluationMasterDao.ConfirmDate = Convert.ToDateTime(exProDate.Text);


                if (ExtendProbationDropDownList.SelectedValue == "2")
                {
                    if (manualUpdateCheckBox.Checked)
                    {
                        Int32 empGenId = 0;
                        string date = "";
                        bool Isconfirm = true;


                        empGenId = Convert.ToInt32(hfEmpInfoId.Value);


                        date = Convert.ToDateTime(exProDate.Text.Trim()).ToString();


                        UpdateEmployeeConfirmationExtendeddate(empGenId, date, Isconfirm);
                    }
                }

            }

            if (ddlreason.SelectedValue == "1")
            {
               


                if (ExtendProbationDropDownList.SelectedValue == "2")
                {
                    if (manualUpdateCheckBox.Checked)
                    {

                        EmployeeJobLeftEntryDAL aEmployeeJobLeftEntryDAL = new EmployeeJobLeftEntryDAL();

                        aEvaluationMasterDao.SeparationDate = Convert.ToDateTime(exProDate.Text);


                        EmployeeJobLeftEntryDAO aEmployeeJobLeftEntryDAO = new EmployeeJobLeftEntryDAO();

                        aEmployeeJobLeftEntryDAO.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                        aEmployeeJobLeftEntryDAO.EmployeeId = Convert.ToInt32(hfEmpInfoId.Value);

                        aEmployeeJobLeftEntryDAO.JobLeftTypeId = Convert.ToInt32(10);

                        // atblEmployeePromotionEntryDAO.PSetpId = Convert.ToInt32(PreviousStepDropDownList.SelectedValue);
                        // atblEmployeePromotionEntryDAO.PRepEmpId = Convert.ToInt32(PReportingBodyDropDownList.SelectedValue);


                        aEmployeeJobLeftEntryDAO.IsClearanceForm = false;




                        aEmployeeJobLeftEntryDAO.IsExitInterview = false;


                        aEmployeeJobLeftEntryDAO.JobLeftDate = Convert.ToDateTime(exProDate.Text);
                        aEmployeeJobLeftEntryDAO.Reason = "";


                        aEmployeeJobLeftEntryDAO.SubmissionDate = null;




                        aEmployeeJobLeftEntryDAO.AutoProcess = "Separation From probation period";

                        aEmployeeJobLeftEntryDAO.EntryBy = Convert.ToInt32(Session["UserId"]);
                        aEmployeeJobLeftEntryDAO.EntryDate = DateTime.Now;

                        aEmployeeJobLeftEntryDAL.EmployeePromotionEntrySaveInfo(aEmployeeJobLeftEntryDAO);
                        Int32 empGenId = 0;
                        bool IsProbistion = true;


                        empGenId = Convert.ToInt32(hfEmpInfoId.Value);

                        IsProbistion = false;


                        UpdateEmployeeStepId(empGenId, "End of probation period");

                    }
                }

            }
            if (aEvaluationMasterDao.ProbationEnd)
            {
                aEvaluationMasterDao.ProbationEndReason = ddlreason.SelectedItem.Text;
            }
            int id = aProbationperiodDal.SaveProbationMaster(aEvaluationMasterDao);
            if (id > 0)
            {
                try
                {
                    aProbationperiodDal.UpdateSelfApprove(id, false);
                    if (manualUpdateCheckBox.Checked == false)
                    {


                        if (Session["EmpInfoid"].ToString() != "")
                        {
                            ProbationEvaluationMasterDAO aMaster = new ProbationEvaluationMasterDAO();
                            aMaster.ProbationEvaluationMasterId
                                = Convert.ToInt32(id);
                            aMaster.ActionStatus = "Verified";
                            bool status = aProbationperiodDal.UpdateContractural(aMaster);


                            int commentid = aProbationperiodDal.SaveComment("0",
                                Session["EmpInfoId"].ToString(), commentsTextBox.Text);


                            ProbationEvaluationAppLogDAO appLogDao = new ProbationEvaluationAppLogDAO();

                            appLogDao.ActionStatus = "Drafted";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                            appLogDao.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                            appLogDao.ProbationEvaluationMasterId = id;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                            int idd = aProbationperiodDal.SaveEmpProbAppLog(appLogDao);


                            DataTable dtempdata =
                                aProbationperiodDal.GetEmpInfo(" WHERE EmpInfoId='" +
                                                                    Session["EmpInfoid"].ToString() + "'");
                            ProbationEvaluationAppLogDAO appLogDaoa = new ProbationEvaluationAppLogDAO();
                            {
                                appLogDaoa.ActionStatus = "Verified";
                                appLogDaoa.ApproveDate = DateTime.Now;
                                appLogDaoa.ApproveBy = Session["UserId"].ToString();
                                appLogDaoa.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                                appLogDaoa.ForEmpInfoId =
                                    Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                                appLogDaoa.ProbationEvaluationMasterId = aMaster.ProbationEvaluationMasterId;
                                appLogDaoa.Comments = commentsTextBox.Text;
                                appLogDaoa.CommentsId = commentid;

                            }
                            ;
                            int ida = aProbationperiodDal.SaveEmpProbAppLog(appLogDaoa);
                            aProbationperiodDal.UpdateJobReqStatus2(aMaster);
                            SenMailForApprved(appLogDao.ForEmpInfoId, "Probation Period Approval ", @"  <br/> Dear Sir, <br/>
A Probation Period Employee is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/>   http://182.160.103.234:8088/  <br/> 
Thank you.
");
                        }
                    }
                    else
                    {
                        aProbationperiodDal.UpdateJobReqStatusAll(id, "Approved", "Approved");
                    }
                }
                catch (Exception)
                {

                    //throw;
                }
                //for (int i = 0; i < gv_ProbationEvaluation.Rows.Count; i++)
                //{
                //    ProbationEvaluationDetailsDAO aProbationEvaluationDetailsDao = new ProbationEvaluationDetailsDAO();

                //    aProbationEvaluationDetailsDao.ProbationEvaluationMasterId = id;
                //    aProbationEvaluationDetailsDao.ValueField =
                //        Convert.ToInt32(((HiddenField) gv_ProbationEvaluation.Rows[i].FindControl("hdpkd")).Value);
                //    aProbationEvaluationDetailsDao.KeyRatingCri =
                //        ((Label) gv_ProbationEvaluation.Rows[i].FindControl("txt_RatingCriterions")).Text;
                //    aProbationEvaluationDetailsDao.IsExcellent =
                //        ((RadioButtonList) gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[0]
                //            .Selected;
                //    aProbationEvaluationDetailsDao.IsGood =
                //        ((RadioButtonList) gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[1]
                //            .Selected;
                //    aProbationEvaluationDetailsDao.IsSatisfactory =
                //        ((RadioButtonList) gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[2]
                //            .Selected;
                //    aProbationEvaluationDetailsDao.IsNotSatisfactory =
                //        ((RadioButtonList) gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[3]
                //            .Selected;

                //    int ida = aProbationperiodDal.SaveProbationDetail(aProbationEvaluationDetailsDao);
                //}
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='ProbationList.aspx';",
                    true);
            }
            //ShowMessageBox("Info Saved Successfully !!!");
        }
    }

    public void UpdateData()
    {
        if (Validation())
        {
            ProbationEvaluationMasterDAO aEvaluationMasterDao = new ProbationEvaluationMasterDAO()
            {
                ProbationEvaluationMasterId = Convert.ToInt32(hdpk.Value),
                EmpInfoId = Convert.ToInt32(empIdHiddenField.Value),
                DeptHeadObserv = txt_DepartmentHeadObservation.Text,
                DivHeadOvserv = txt_DivisionHeadObservation.Text,
                ExProbation = ExtendProbationDropDownList.SelectedValue == "1",
                ProbationEnd = ExtendProbationDropDownList.SelectedValue == "2",
                SupervisorObserv = txt_SupervisorObservation.Text,
            };
            if (exProDate.Text != string.Empty)
            {
                aEvaluationMasterDao.ExProDate = Convert.ToDateTime(exProDate.Text);
                if (ExtendProbationDropDownList.SelectedValue == "1")
                {
                    if (manualUpdateCheckBox.Checked)
                    {
                        Int32 empGenId = 0;
                        string date = "";



                        empGenId = Convert.ToInt32(hfEmpInfoId.Value);


                        date = Convert.ToDateTime(exProDate.Text.Trim()).ToString();


                        UpdateEmployeeProbitionExtendeddate(empGenId, date);
                    }
                }

            }
            if (ddlreason.SelectedValue == "2")
            {
                aEvaluationMasterDao.ConfirmDate = Convert.ToDateTime(exProDate.Text);


                if (ExtendProbationDropDownList.SelectedValue == "2")
                {
                    if (manualUpdateCheckBox.Checked)
                    {
                        Int32 empGenId = 0;
                        string date = "";
                        bool Isconfirm = true;


                        empGenId = Convert.ToInt32(hfEmpInfoId.Value);


                        date = Convert.ToDateTime(exProDate.Text.Trim()).ToString();


                        UpdateEmployeeConfirmationExtendeddate(empGenId, date, Isconfirm);
                    }
                }

            }

            if (ddlreason.SelectedValue == "1")
            {



                if (ExtendProbationDropDownList.SelectedValue == "2")
                {
                    if (manualUpdateCheckBox.Checked)
                    {
                        Int32 empGenId = 0;
                        bool IsProbistion = true;


                        empGenId = Convert.ToInt32(hfEmpInfoId.Value);

                        IsProbistion = false;



                        UpdateEmployeeSeparationExtendeddate(empGenId, IsProbistion);
                    }
                }

            }
            if (aEvaluationMasterDao.ProbationEnd)
            {
                aEvaluationMasterDao.ProbationEndReason = ddlreason.SelectedItem.Text;
            }
            bool st = aProbationperiodDal.UpdateProbationMaster(aEvaluationMasterDao);
            int id = Convert.ToInt32(hdpk.Value);
            if (st)
            {
                try
                {
                    aProbationperiodDal.UpdateSelfApprove(id, false);
                    if (manualUpdateCheckBox.Checked == false)
                    {


                        if (Session["EmpInfoid"].ToString() != "")
                        {
                            ProbationEvaluationMasterDAO aMaster = new ProbationEvaluationMasterDAO();
                            aMaster.ProbationEvaluationMasterId
                                = Convert.ToInt32(id);
                            aMaster.ActionStatus = "Verified";
                            bool status = aProbationperiodDal.UpdateContractural(aMaster);


                            int commentid = aProbationperiodDal.SaveComment("0",
                                Session["EmpInfoId"].ToString(), commentsTextBox.Text);


                            ProbationEvaluationAppLogDAO appLogDao = new ProbationEvaluationAppLogDAO();

                            appLogDao.ActionStatus = "Drafted";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                            appLogDao.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                            appLogDao.ProbationEvaluationMasterId = id;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                            int idd = aProbationperiodDal.SaveEmpProbAppLog(appLogDao);


                            DataTable dtempdata =
                                aProbationperiodDal.GetEmpInfo(" WHERE EmpInfoId='" +
                                                                    Session["EmpInfoid"].ToString() + "'");
                            ProbationEvaluationAppLogDAO appLogDaoa = new ProbationEvaluationAppLogDAO();
                            {
                                appLogDaoa.ActionStatus = "Verified";
                                appLogDaoa.ApproveDate = DateTime.Now;
                                appLogDaoa.ApproveBy = Session["UserId"].ToString();
                                appLogDaoa.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                                appLogDaoa.ForEmpInfoId =
                                    Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                                appLogDaoa.ProbationEvaluationMasterId = aMaster.ProbationEvaluationMasterId;
                                appLogDaoa.Comments = commentsTextBox.Text;
                                appLogDaoa.CommentsId = commentid;

                            }
                            ;
                            int ida = aProbationperiodDal.SaveEmpProbAppLog(appLogDaoa);
                            aProbationperiodDal.UpdateJobReqStatus2(aMaster);
                            SenMailForApprved(appLogDao.ForEmpInfoId, "Probation Period Approval ", @"  <br/> Dear Sir, <br/>
A Probation Period Employee is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                        }
                    }
                    else
                    {
                        aProbationperiodDal.UpdateJobReqStatusAll(id, "Approved", "Approved");
                    }
                }
                catch (Exception)
                {

                    //throw;
                }
                //for (int i = 0; i < gv_ProbationEvaluation.Rows.Count; i++)
                //{
                //    ProbationEvaluationDetailsDAO aProbationEvaluationDetailsDao = new ProbationEvaluationDetailsDAO();

                //    aProbationEvaluationDetailsDao.ProbationEvaluationMasterId = id;
                //    aProbationEvaluationDetailsDao.ValueField =
                //        Convert.ToInt32(((HiddenField) gv_ProbationEvaluation.Rows[i].FindControl("hdpkd")).Value);
                //    aProbationEvaluationDetailsDao.KeyRatingCri =
                //        ((Label) gv_ProbationEvaluation.Rows[i].FindControl("txt_RatingCriterions")).Text;
                //    aProbationEvaluationDetailsDao.IsExcellent =
                //        ((RadioButtonList) gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[0]
                //            .Selected;
                //    aProbationEvaluationDetailsDao.IsGood =
                //        ((RadioButtonList) gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[1]
                //            .Selected;
                //    aProbationEvaluationDetailsDao.IsSatisfactory =
                //        ((RadioButtonList) gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[2]
                //            .Selected;
                //    aProbationEvaluationDetailsDao.IsNotSatisfactory =
                //        ((RadioButtonList) gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[3]
                //            .Selected;

                //    int ida = aProbationperiodDal.SaveProbationDetail(aProbationEvaluationDetailsDao);
                //}
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='ProbationList.aspx';",
                    true);
            }
            //ShowMessageBox("Info Saved Successfully !!!");
        }
    }
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (hdpk.Value!=string.Empty)
        {
            
            UpdateData();
            
        }
        else
        {
            SaveDATA();    
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
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //        "alert",
                //        "alert('Email has not Sent, Try Once More time...');window.location ='ContractualEmpManagementView.aspx';",
                //        true);
            }
            catch (Exception exe)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(),
                //        "alert",
                //        "alert('Email has not Sent, Try Once More time...');window.location ='ContractualEmpManagementView.aspx';",
                //        true);
            }


            System.Threading.Thread.Sleep(100);
        }



    }

    private void UpdateEmployeeProbitionExtendeddate(int empGenId, string ExtensionToDate)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.ExtProbationPeriodDate = Convert.ToDateTime(ExtensionToDate.ToString());

        aInfo.EmpInfoId = empGenId;

        aProbationperiodDal.UpdateEmployeeProbitionExtendeddateInfo(aInfo);

    }


    private void UpdateEmployeeSeparationExtendeddate(int empGenId, bool IsProbition)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.IsProbationary = IsProbition;

        aInfo.EmpInfoId = empGenId;

        aProbationperiodDal.UpdateEmployeeSeparationInfo(aInfo);

    }
    private void UpdateEmployeeStepId(int empGenId, string reason)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.InactiveReason = reason;
        aInfo.IsActive = false;
        aInfo.EmployeeStatus = "InActive";
        aInfo.EmpInfoId = empGenId;

        aProbationperiodDal.UpdateEmployeeExitInfo(aInfo);

    }
  

    private void UpdateEmployeeConfirmationExtendeddate(int empGenId, string ExtensionToDate, bool Isconfirm)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.ConfirmationDate = Convert.ToDateTime(ExtensionToDate.ToString());

        aInfo.EmpInfoId = empGenId;
        aInfo.ConformationStatus = Isconfirm;

        aProbationperiodDal.UpdateEmployeeConfirmationdateInfo(aInfo);

    }
    private void Clear()
    {
        ddlCompany.SelectedValue = "";
        hfEmpInfoId.Value = "";
        empCode.Text = "";
        empName.Text = "";
        dtJoining.Text = "";
        ddlDivision.Text = "";
        ddlDesignation.Text = "";
        //txt_ProbitionFrom.Text = "";
        //txt_ProbitionTo.Text = "";
        //txt_DueDateOfConfirmation.Text = "";
        txt_SupervisorObservation.Text = "";
            txt_DepartmentHeadObservation.Text = "";
            txt_DivisionHeadObservation.Text = "";
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void RBConSep_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //exppr.Visible = false;
        //probendreason.Visible = false;
        //dateprob.Visible = false;
        //exProDate.Visible = false;
        //if (RBConSep.Items[0].Selected)
        //{
        //    exppr.Visible = true;
        //    probendreason.Visible = false;
        //    dateprob.Visible = true;
        //    exProDate.Visible = true;

        //    if (ddlreason.SelectedValue == "1")
        //    {
        //        dateprob.Visible = false;
        //        exProDate.Visible = false;
        //    }

        //}

        //if (RBConSep.Items[1].Selected)
        //{
        //    exppr.Visible = false;
        //    probendreason.Visible = true;
        //}
    }

    protected void ddlreason_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlreason.SelectedIndex == 1)
        //{
        //    exppr.Visible = true;
        //    dateprob.Text = "Date";
        //    exProDate.Visible = true;
        //}

        if (ddlreason.SelectedValue == "1")
        {
           // ExtendProbationDropDownList.SelectedValue = "1";
           // dateprob.Visible = false;
            exProDate.Visible = true;
            exppr.Visible = true;

        }
        if (ddlreason.SelectedValue == "2")
        {
            
          //  ExtendProbationDropDownList.SelectedValue = "1";
         //   dateprob.Visible = true;
            exProDate.Visible = true;
            exppr.Visible = true;
        }

        //if (RadioButtonList1.Items[0].Selected)
        //{
        //    exppr.Visible = true;
        //    probendreason.Visible = false;

        //}
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("ProbationList.aspx");
    }

    protected void ExtendProbationDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ExtendProbationDropDownList.SelectedValue=="1")
        {
            exppr.Visible = true;
            probendreason.Visible = false;
            dateprob.Visible = true;
            exProDate.Visible = true;

            //if (ddlreason.SelectedValue == "1")
            //{
            //    dateprob.Visible = false;
            //    exProDate.Visible = false;
            //}

        }

        if (ExtendProbationDropDownList.SelectedValue == "2")
        {
            exppr.Visible = false;
            probendreason.Visible = true;
            ddlreason_OnSelectedIndexChanged(null, null);
        }


       // 
    }
}