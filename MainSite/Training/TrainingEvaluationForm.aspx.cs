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
using DAL.Survey;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using Library.DAO.HRM_Entities;

public partial class Training_TrainingEvaluationForm : System.Web.UI.Page
{
    SurveyCommonDAL _surveyCommonDal = new SurveyCommonDAL();
    TrainingEffeftivenessDAL aDal = new TrainingEffeftivenessDAL();
    ProbationperiodDAL aProbationperiodDal = new ProbationperiodDAL();
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
            string EmpID = Request.QueryString["EmpID"];
            if (id!="")
            {
                MasterIDHF.Value = id;
            }
           
          

           

            if (EmpID!="")
            {
                GetEmpData(EmpID);
                hfEmpInfoId.Value = EmpID;
            }
        }
    }

    private void LoadDropDownList()
    {
        _surveyCommonDal.LoadCompanyDropDownList(ddlCompany);

    }

    public void GetEmpData(string id)
    {
        DataTable dtempdata = aDal.LoadEmployeeInfoByTrainingRecordMasterId(id);
        if (dtempdata.Rows.Count>0)
        {
            empIdHiddenField.Value = dtempdata.Rows[0]["EmpInfoId"].ToString();
            empCode.Text = dtempdata.Rows[0]["EmpMasterCode"].ToString();
            empName.Text = dtempdata.Rows[0]["EmpName"].ToString();
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
        using (DataTable dt = _surveyCommonDal.GetTrainingEvaluationRatingByCompanyId(id))
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
         
    }

    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public bool Validation()
    {
        DataTable dtexist = aDal.GetTrainingEmpData(empIdHiddenField.Value);
        if (dtexist.Rows.Count>0)
        {
            ShowMessageBox("Already Exist !!");
            return false;
        }
        return true;
    }
    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            ProbationEvaluationMasterDAO aEvaluationMasterDao = new ProbationEvaluationMasterDAO()
            {
                EmpInfoId = Convert.ToInt32(empIdHiddenField.Value),
                TrainingRecordMasterId = Convert.ToInt32(MasterIDHF.Value),
              
                SupervisorObserv = txt_SupervisorObservation.Text,
            };



            int id = aProbationperiodDal.SaveTrainingMaster(aEvaluationMasterDao);
            if (id > 0)
            {
                try
                {
                    if (Session["EmpInfoid"].ToString() != "")
                    {
                        ProbationEvaluationAppLogDAO appLogDao = new ProbationEvaluationAppLogDAO();
                        {
                            appLogDao.ActionStatus = "Drafted";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                            appLogDao.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                            appLogDao.TrainingEvaluationMasterId = id;
                            appLogDao.Comments = " ";

                        }

                        int idd = aProbationperiodDal.SaveEmpTrainingAppLog(appLogDao);

                        int EmpID = (int)Session["EmpInfoId"];
                        string EmpName = "";
                        string sTrainingTitle = "";

                        int TRMaID = Convert.ToInt32(MasterIDHF.Value);
                        using (var db = new HRIS_SMC_DBEntities())
                        {
                            var sss = (from t in db.tblTrainingRecordMasters
                                       where t.TrainingRecordMasterId == TRMaID
                                       select t).FirstOrDefault();
                            if (sss != null)
                            {
                                sTrainingTitle = sss.TrainingTitle;
                            }

                        }
                        using (var db = new HRIS_SMCEntities())
                        {
                            var comvar = (from t in db.tblEmpGeneralInfoes
                                          where t.EmpInfoId == EmpID
                                          select t).FirstOrDefault();





                            if (comvar != null)
                            {
                                EmpName = comvar.EmpName;
                            }



                        }


                        SenMailForApprved(EmpID, " Training Evaluation ", @"  <br/> Dear Sir, <br/>
 Employee Name: " + EmpName + @". <br/>
 Training Title: " + sTrainingTitle + @". <br/>
has been Evaluated.<br/>
<br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                    }
                }
                catch (Exception)
                {
                    
                    //throw;
                }
                for (int i = 0; i < gv_ProbationEvaluation.Rows.Count; i++)
                {
                    ProbationEvaluationDetailsDAO aProbationEvaluationDetailsDao = new ProbationEvaluationDetailsDAO();

                    aProbationEvaluationDetailsDao.TrainingEvaluationMasterId = id;
                    aProbationEvaluationDetailsDao.ValueField =
                        Convert.ToInt32(((HiddenField) gv_ProbationEvaluation.Rows[i].FindControl("hdpkd")).Value);
                    aProbationEvaluationDetailsDao.KeyRatingCri =
                        ((Label) gv_ProbationEvaluation.Rows[i].FindControl("txt_RatingCriterions")).Text;
                    aProbationEvaluationDetailsDao.IsNochange =
                        ((RadioButtonList) gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[0]
                            .Selected;
                    aProbationEvaluationDetailsDao.IsMinorChange =
                        ((RadioButtonList) gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[1]
                            .Selected;
                    aProbationEvaluationDetailsDao.IsReasonableChange =
                        ((RadioButtonList) gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[2]
                            .Selected;
                    aProbationEvaluationDetailsDao.IsSignificantChange =
                        ((RadioButtonList) gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[3]
                            .Selected;

                    aProbationEvaluationDetailsDao.IsNotapplicable =
                   ((RadioButtonList)gv_ProbationEvaluation.Rows[i].FindControl("rad_RatingScale")).Items[3]
                       .Selected;

                    int ida = aProbationperiodDal.SaveTrainingDetail(aProbationEvaluationDetailsDao);
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...');window.location ='TrainingEffeftivenessList.aspx';",
                    true);
            }
            //ShowMessageBox("Info Saved Successfully !!!");
        }
    }

    private void SenMailForApprved(int forEmpID, string mSubject, string mBody)
    {
        int comId = 0;
        using (var db = new HRIS_SMCEntities())
        {
            var comvar = (from t in db.tblEmpGeneralInfoes
                          where t.EmpInfoId == forEmpID
                          select t).FirstOrDefault();




            if (comvar != null)
            {
                comId = (int)comvar.CompanyId;
            }



        }
        int HeadPerson = 0;

        using (var db = new HRIS_SMC_DBEntities())
        {
            var head = (from t in db.tblEmployeeApprovalByOpearationDetails
                        where t.Operation == 3145 && t.CompanyId == comId && t.Isheadperson == 1
                        select t).FirstOrDefault();

            if (head != null)
            {
                HeadPerson = (int)head.EmpInfoId;
            }

        }

         string HeadPersonMail = "";
        using (var db = new HRIS_SMCEntities())
        {
           

            var HeadPersonMailAddress = (from t in db.tblEmpGeneralInfoes
                where t.EmpInfoId == HeadPerson
                select t).FirstOrDefault();

            if (HeadPersonMailAddress != null)
            {
                HeadPersonMail = HeadPersonMailAddress.OfficialEmail;
            }
        }



        if (HeadPersonMail != "")
        {
            System.Threading.Thread.Sleep(100);

            MailMessage mail = new MailMessage();




            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(Session["EmailID"].ToString());
            try
            {
                mail.To.Add(HeadPersonMail.Trim());
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
                showMessageBox("Email has not Sent, Try Once More time");
            }
            catch (Exception exe)
            {
                showMessageBox("Email has not Sent, Try Once More time");
            }


            System.Threading.Thread.Sleep(100);
        }


    }
    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    private void UpdateEmployeeProbitionExtendeddate(int empGenId, string ExtensionToDate)
    {
        EmpGeneralInfo aInfo = new EmpGeneralInfo();

        aInfo.ContractEndDate = Convert.ToDateTime(ExtensionToDate.ToString());

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
            dateprob.Visible = false;
            exProDate.Visible = false;
        }
        if (ddlreason.SelectedValue == "2")
        {
            
          //  ExtendProbationDropDownList.SelectedValue = "1";
            dateprob.Visible = true;
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
        Response.Redirect("TrainingEffeftivenessList.aspx");
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
        }
    }
}