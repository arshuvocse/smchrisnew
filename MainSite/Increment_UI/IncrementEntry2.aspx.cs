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
using DAL.MeetingMinorsDAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using Library.DAO.HRM_Entities;
using Label = System.Web.UI.WebControls.Label;

public partial class Increment_UI_IncrementEntry2 : System.Web.UI.Page
{
    readonly IncrementDal aIncrementDal = new IncrementDal();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["IncrementEdit"] = null;
            ButtonVisible();
            LoadDropDownList();
            
            EffectiveDateTextBox.Attributes.Add("readonly", "readonly");
            string id = Request.QueryString["id"];
            if (Session["IncrementEdit"] != null)
            {
                hdpk.Value = Session["IncrementEdit"].ToString();
              Session["IncrementEdit"] = null;
                GetOneData(hdpk.Value);
            }
        }
    }

    private void GetOneData(string id)
    {
        DataTable dtMain = aIncrementDal.LoadGetOneRecordByMasterID(id);
        if (dtMain.Rows.Count > 0)
        {

            HideFilter.Visible = false;
            ddlCompany.SelectedValue = dtMain.Rows[0]["CompanyId"].ToString();
            ddlCompany_SelectedIndexChanged(null, null);
            ddlFinYear.SelectedValue = dtMain.Rows[0]["FinancialYearId"].ToString();
            ddlIncrementType.SelectedValue= dtMain.Rows[0]["IncrementTypeId"].ToString();

            try
            {
                EffectiveDateTextBox.Text = Convert.ToDateTime(dtMain.Rows[0]["EffectiveDate"].ToString()).ToString("dd-MMM-yyyy");
            }
            catch (Exception ex)
            {


            }



            loadGridView.DataSource = dtMain;
            loadGridView.DataBind();

            try
            {
                for (int i = 0; i < loadGridView.Rows.Count; i++)
                {

                    var Chk = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");
                    Chk.Checked = true;
                  //  var ddlDesignation = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("designationDropDownList");
                    //aIncrementDal.LoadDesignation(ddlDesignation);
                    //ddlDesignation.SelectedValue = dtMain.Rows[i].Field<Int32>("DesignationId").ToString(CultureInfo.InvariantCulture);

                   // var ddlDepartment = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("departmentDropDownList");
                    //aIncrementDal.LoadDepartment(ddlDepartment);
                    //ddlDepartment.SelectedValue = dtMain.Rows[i].Field<Int32>("DepartmentId").ToString(CultureInfo.InvariantCulture);

                    //var ddlSalaryGrade = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("salaryGradeDropDownList");
                    //aIncrementDal.LoadSalaryGrade(ddlSalaryGrade);
                    //ddlSalaryGrade.SelectedValue = dtMain.Rows[i].Field<Int32>("SalaryGradeId").ToString(CultureInfo.InvariantCulture);

                    HiddenField HFSalaryGrade = (HiddenField)loadGridView.Rows[i].Cells[0].FindControl("HFSalaryGrade");



                    DropDownList ddlIncrementalStep = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("incrementalStepDropDownList");
                    if (HFSalaryGrade.Value != "")
                    {

                        aIncrementDal.LoadSalaryStep(ddlIncrementalStep, HFSalaryGrade.Value);
                    }

                    ddlIncrementalStep.SelectedValue = dtMain.Rows[i].Field<Int32>("IncreSalaryStepId").ToString(CultureInfo.InvariantCulture);

                }
            }
            catch (Exception)
            {
                
                //throw;
            }

        }
        
    }

    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {


            if (Session["Status"].ToString() == "Add")
            {
                submitButton.Visible = false;
            }

            if (Session["Status"].ToString() == "Edit")
            {
                btn_Update.Visible = true;
            }
           
            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("IncrementView.aspx");
        }

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

            using (DataTable dt = _commonDataLoad.GetDDLSalaryLocation())
            {
                ddlSalaryLocation.DataSource = dt;
                ddlSalaryLocation.DataValueField = "Value";
                ddlSalaryLocation.DataTextField = "TextField";
                ddlSalaryLocation.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetEmpDDLAActiveOnlyView(ddlCompany.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;

            }
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
            ddlSalaryLocation.Items.Clear();
        }
    }

   
    protected void btnSearch_OnClick(object sender, EventArgs e)
    {
        if (CheckSearchValidation())
        {
            DataTable aTable = aIncrementDal.LoadEmployeeInformation(GetParameter(), Convert.ToInt32(ddlFeed.SelectedValue), ddlFeed.SelectedItem.Text, GetParameter_2());

            if (aTable.Rows.Count > 0)
            {
                loadGridView.DataSource = aTable;
                loadGridView.DataBind();

                  for (int i = 0; i < loadGridView.Rows.Count; i++)
                   {
                //        var ddlDesignation = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("designationDropDownList");
                //        aIncrementDal.LoadDesignation(ddlDesignation);
                //        ddlDesignation.SelectedValue = aTable.Rows[i].Field<Int32>("DesignationId").ToString(CultureInfo.InvariantCulture);

                //        var ddlDepartment = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("departmentDropDownList");
                //        aIncrementDal.LoadDepartment(ddlDepartment);
                //        ddlDepartment.SelectedValue = aTable.Rows[i].Field<Int32>("DepartmentId").ToString(CultureInfo.InvariantCulture);

                 //var ddlSalaryGrade = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("salaryGradeDropDownList");
                 //      aIncrementDal.LoadSalaryGrade(ddlSalaryGrade);
                 // ddlSalaryGrade.SelectedValue = aTable.Rows[i].Field<Int32>("SalaryGradeId").ToString(CultureInfo.InvariantCulture);

                //        var ddlSalaryStep = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("salaryStepDropDownList");
                //        aIncrementDal.LoadSalaryStep(ddlSalaryStep, ddlSalaryGrade.SelectedValue);
                //        ddlSalaryStep.SelectedValue = aTable.Rows[i].Field<Int32>("SalaryStepId").ToString(CultureInfo.InvariantCulture);
                       HiddenField HFSalaryGrade = (HiddenField)loadGridView.Rows[i].Cells[0].FindControl("HFSalaryGrade");
                      
                      
    
                       DropDownList ddlIncrementalStep = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("incrementalStepDropDownList");
                       if (HFSalaryGrade.Value!="")
                       {
                          
                           aIncrementDal.LoadSalaryStep(ddlIncrementalStep, HFSalaryGrade.Value);
                       }

                       ddlIncrementalStep.SelectedValue = aTable.Rows[i].Field<Int32>("IncreSalaryStepId").ToString(CultureInfo.InvariantCulture);

                       
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

        if (isProbition.Checked ==true)
        {
            parameter = parameter + " AND EGI.IsProbationary=1 ";
        }
        else
        {
            parameter = parameter + " AND EGI.IsProbationary=0 ";
        }

        if (ddlSalaryLocation.SelectedIndex >0)
        {
            parameter = parameter + " AND EGI.SalaryLoationId = '" + ddlSalaryLocation.SelectedValue + "'";
        }
        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + " AND EGI.EmpInfoId = '" + ddlEmpInfo.SelectedValue + "'";
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


    private string GetParameter_2()
    {
        string parameter = "";




        if (isProbition.Checked == true)
        {
            parameter = parameter + " AND EGI.IsProbationary=1 ";
        }
        else
        {
            parameter = parameter + " AND EGI.IsProbationary=0 ";
        }


        if (ddlSalaryLocation.SelectedIndex > 0)
        {
            parameter = parameter + " AND EGI.SalaryLoationId = '" + ddlSalaryLocation.SelectedValue + "'";
        }
        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + " AND EGI.EmpInfoId = '" + ddlEmpInfo.SelectedValue + "'";
        }

        if (ddlDivision.SelectedValue != "")
        {
            parameter = parameter + " AND EGI.DivisionId = '" + ddlDivision.SelectedValue + "'";
        }

        if (ddlCategory.SelectedValue != "")
        {
            parameter = parameter + " AND EGI.EmpCategoryId = '" + ddlCategory.SelectedValue + "'";
        }

        if (ddlDepartment.SelectedIndex > 0)
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

        //if (ddlEmpInfo.SelectedValue == "")
        //{
            
        //}
       

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
            else
            {
                
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
            if (manualUpdateCheckBox.Items[0].Selected == true)
                {
            aIncrementDal.UpdateSelfApprove(id, false);
            try
            {
                if (Session["EmpInfoId"].ToString() != "")
                {
                    IncrementDao aMaster = new IncrementDao();
                    aMaster.IncrementId
                        = Convert.ToInt32(id);
                    aMaster.ActionStatus = "Verified";
                    bool status = aIncrementDal.UpdateContractural(aMaster);



                    int commentid = aIncrementDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                        " ");

                    IncrementAppLogDAO appLogDaoa = new IncrementAppLogDAO();

                    appLogDaoa.ActionStatus = "Drafted";
                    appLogDaoa.ApproveDate = DateTime.Now;
                    appLogDaoa.ApproveBy = Session["UserId"].ToString();
                    appLogDaoa.PreEmpInfoId = Convert.ToInt32(0);
                    appLogDaoa.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                    appLogDaoa.IncrementId = id;
                    appLogDaoa.Comments = txtComment.Text;
                    appLogDaoa.CommentsId = commentid;

                    int idd = aIncrementDal.SavAppLog(appLogDaoa);


                    DataTable dtempdata =
                        aIncrementDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    IncrementAppLogDAO appLogDao = new IncrementAppLogDAO();
                    {
                        appLogDao.ActionStatus = "Verified";
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                        appLogDao.IncrementId = aMaster.IncrementId;
                        appLogDao.Comments = txtComment.Text;
                        appLogDao.CommentsId = commentid;

                    }
                    ;
                    int iddddd = aIncrementDal.SavAppLog(appLogDao);
                    SenMailForApprved(appLogDao.ForEmpInfoId, " Increment Approval ", @"  <br/> Dear Sir, <br/>
An Increment is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                    aIncrementDal.UpdateJobReqStatus2(aMaster);
                
            }
            }
            catch (Exception)
            {

                //throw;
            }
                }
        }

        return id;
    }


    private int UpdateIncrementInfo(List<IncrementDao> aIncrementDaos)
    {
        Int32 id = 0;
        id = Convert.ToInt32(hdpk.Value);
        foreach (var aDao in aIncrementDaos)
        {


            bool ddd = aIncrementDal.UpdateIncrementInfo(aDao,  id);

            if (ddd)
            {


                if (manualUpdateCheckBox.Items[0].Selected == true)
                {
                    aIncrementDal.UpdateSelfApprove(id, false);
                    try
                    {
                        if (Session["EmpInfoId"].ToString() != "")
                        {
                            IncrementDao aMaster = new IncrementDao();
                            aMaster.IncrementId
                                = Convert.ToInt32(id);
                            aMaster.ActionStatus = "Verified";
                            bool status = aIncrementDal.UpdateContractural(aMaster);



                            int commentid = aIncrementDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                                " ");

                            IncrementAppLogDAO appLogDaoa = new IncrementAppLogDAO();

                            appLogDaoa.ActionStatus = "Drafted";
                            appLogDaoa.ApproveDate = DateTime.Now;
                            appLogDaoa.ApproveBy = Session["UserId"].ToString();
                            appLogDaoa.PreEmpInfoId = Convert.ToInt32(0);
                            appLogDaoa.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDaoa.IncrementId = id;
                            appLogDaoa.Comments = txtComment.Text;
                            appLogDaoa.CommentsId = commentid;

                            int idd = aIncrementDal.SavAppLog(appLogDaoa);


                            DataTable dtempdata =
                                aIncrementDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                            IncrementAppLogDAO appLogDao = new IncrementAppLogDAO();
                            {
                                appLogDao.ActionStatus = "Verified";
                                appLogDao.ApproveDate = DateTime.Now;
                                appLogDao.ApproveBy = Session["UserId"].ToString();
                                appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                                appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                                appLogDao.IncrementId = aMaster.IncrementId;
                                appLogDao.Comments = txtComment.Text;
                                appLogDao.CommentsId = commentid;

                            }
                            ;
                            int iddddd = aIncrementDal.SavAppLog(appLogDao);
                            SenMailForApprved(appLogDao.ForEmpInfoId, " Increment Approval ", @"  <br/> Dear Sir, <br/>
An Increment is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                            aIncrementDal.UpdateJobReqStatus2(aMaster);

                        }
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                }
            }
        }

        return id;
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
                
            }
            catch (Exception exe)
            {
              
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
                HiddenField HFSalaryGrade = (HiddenField)loadGridView.Rows[i].Cells[0].FindControl("HFSalaryGrade");
                HiddenField HFSalaryStep = (HiddenField)loadGridView.Rows[i].Cells[0].FindControl("HFSalaryStep");
                HiddenField hffEmpMasterCode = (HiddenField)loadGridView.Rows[i].Cells[0].FindControl("hffEmpMasterCode");
                
                var ddlIncrementalStep = (DropDownList)loadGridView.Rows[i].Cells[0].FindControl("incrementalStepDropDownList");
                var feedSalary = (TextBox)loadGridView.Rows[i].Cells[0].FindControl("feedSalaryTextBox");
              
                //aDao.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);
                //aDao.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
                aDao.SalaryGradeId = Convert.ToInt32(HFSalaryGrade.Value);
                aDao.CurrentStepId = Convert.ToInt32(HFSalaryStep.Value);
                aDao.IncrementalStepId = Convert.ToInt32(ddlIncrementalStep.SelectedValue);

                //For Employee Master Information update ------------------------------------------------------------------------

                //--------------------------------------------------------------------------------------------------------------

                aDao.FeedSalary = Convert.ToDecimal(feedSalary.Text.Trim());

                if (manualUpdateCheckBox.Items[1].Selected == true)
                {
                    aDao.AutoProcess = "Manually Updated";

                    Int32 empGenId = 0;
                    Int32 stepId = 0;

                    empGenId = Convert.ToInt32(loadGridView.DataKeys[i].Value.ToString());
                    stepId = Convert.ToInt32(ddlIncrementalStep.SelectedValue);

                    UpdateEmployeeStepId(empGenId, stepId);

                    GetEmpSalarybyStepEmpId(empGenId, aDao.SalaryGradeId, stepId, hffEmpMasterCode.Value);

                   // aIncrementDal.UpdateEmployeeIncrementalStepInfo(empGenId, stepId);
                }

                aDao.EntryBy = Convert.ToInt32(Session["UserId"]);
                aDao.EntryDate = DateTime.Now;

                aDaos.Add(aDao);
            }
            else
            {
                //aShowMessage.ShowMessageBox("Employee Already Exists within this effective date!!", this);
            }
        }

        return aDaos;
    }
    MemoPrintIncrementDAL aDAL = new MemoPrintIncrementDAL();

    private void GetEmpSalarybyStepEmpId(int EmpID, int SalaryGradeId, int SalaryStepId, string EmpMasterCode)
    {
     decimal   res = 0;

        string GradeCode = "";
        Decimal basicAmount = 0;
        decimal Medical = 0;
        decimal HouseResnt = 0;
        decimal Conveyance = 0;
        decimal Washing = 0;
            DataTable dtPart = aDAL.LoadParticularsGridView();
        if (dtPart.Rows.Count > 0)
        {
            KeyResponGridView.DataSource = dtPart;
            KeyResponGridView.DataBind();

            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                Label lbl_SalaryBreakUp =
                    (Label)
                        KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUp");
                Label lbl_Particulars =
                    (Label)
                        KeyResponGridView.Rows[i].FindControl("lbl_Particulars");

                try
                {
                    DataTable aDataTable =
                        aDAL.LoadSalaryStepGradeMIdWithCode(Convert.ToInt32(SalaryGradeId),
                            Convert.ToInt32(SalaryStepId));
                   
                    try
                    {
                        basicAmount = Math.Round(Convert.ToDecimal(aDataTable.Rows[0]["BasicAmount"].ToString()), 0);
                    }
                    catch (Exception)
                    {
                    }


                    try
                    {
                        GradeCode = aDataTable.Rows[0]["GradeCode"].ToString();

                    }
                    catch (Exception)
                    {

                        //throw;
                    }

                    if (lbl_Particulars.Text.Trim() == "Basic Pay")
                    {
                        //txtIncrementalStep.Text=


                        lbl_SalaryBreakUp.Text = Math.Round(basicAmount, 0).ToString();



                    }

                    if (GradeCode.Trim() == "Special" || GradeCode.Trim() == "M-1" ||
                        GradeCode.Trim() == "M-2A" || GradeCode.Trim() == "M-2B" || GradeCode.Trim() == "M-3A" ||
                        GradeCode.Trim() == "M-3B" || GradeCode.Trim() == "M-4" || GradeCode.Trim() == "M-5")
                    {
                      

                        if (lbl_Particulars.Text.Trim() == "House Rent")
                        {
                            HouseResnt = (Math.Round(basicAmount, 0)*50)/100;
                            lbl_SalaryBreakUp.Text = Math.Round(HouseResnt, 0).ToString();
                        }

                        if (lbl_Particulars.Text.Trim() == "Medical")
                        {
                            Medical = (Math.Round(basicAmount, 0)*10)/100;
                            lbl_SalaryBreakUp.Text = Math.Round(Medical, 0).ToString();
                        }

                        if (lbl_Particulars.Text.Trim() == "Conveyance")
                        {
                            DataTable dtConveyance =
                                aDAL.CheckConveyanceByMasterCode(EmpMasterCode.Trim());

                            if (dtConveyance.Rows.Count > 0)
                            {
                                Conveyance = 0;
                            }
                            else
                            {
                                Conveyance = 0;
                            }
                            lbl_SalaryBreakUp.Text = Conveyance.ToString();
                        }


                        if (lbl_Particulars.Text.Trim() == "Washing")
                        {
                            lbl_SalaryBreakUp.Text = "0";
                            lbl_Particulars.Text = "";
                            Washing = 0;
                            lbl_Particulars.Visible = false;
                            lbl_SalaryBreakUp.Visible = false;
                        }
                        //basicAmount
                    }


                    if (GradeCode.Trim() == "M-6A" || GradeCode.Trim() == "M-6B" || GradeCode.Trim() == "M-7" ||
                        GradeCode.Trim() == "M-8" || GradeCode.Trim() == "M-9")
                    {
                         

                        if (lbl_Particulars.Text.Trim() == "House Rent")
                        {
                            HouseResnt = (Math.Round(basicAmount, 0)*75)/100;
                            lbl_SalaryBreakUp.Text = Math.Round(HouseResnt, 0).ToString();
                        }

                        if (lbl_Particulars.Text.Trim() == "Medical")
                        {
                            Medical = (Math.Round(basicAmount, 0)*10)/100;
                            lbl_SalaryBreakUp.Text = Math.Round(Medical, 0).ToString();
                        }

                        if (lbl_Particulars.Text.Trim() == "Conveyance")
                        {
                            DataTable dtConveyance =
                                aDAL.CheckConveyanceByMasterCode(EmpMasterCode.Trim());

                            if (dtConveyance.Rows.Count > 0)
                            {
                                Conveyance = 0;
                            }
                            else
                            {
                                Conveyance = (Math.Round(basicAmount, 0)*5)/100;
                            }
                            lbl_SalaryBreakUp.Text = Math.Round(Conveyance, 0).ToString();
                        }

                        if (lbl_Particulars.Text.Trim() == "Washing")
                        {
                            lbl_SalaryBreakUp.Text = "0";
                            lbl_Particulars.Text = "";
                            Washing = 0;
                            lbl_Particulars.Visible = false;
                            lbl_SalaryBreakUp.Visible = false;
                        }
                    }


                    if (GradeCode.Trim() == "S-5" || GradeCode.Trim() == "S-4" ||
                        GradeCode.Trim() == "S-3" || GradeCode.Trim() == "S-2" ||
                        GradeCode.Trim() == "S-1A" ||
                        GradeCode.Trim() == "S-1B" ||
                        GradeCode.Trim() == "SS-5" ||
                        GradeCode.Trim() == "S-1A" ||
                        GradeCode.Trim() == "SS-4" ||
                        GradeCode.Trim() == "S-1A" ||
                        GradeCode.Trim() == "SS-3" ||
                        GradeCode.Trim() == "SS-2" ||
                        GradeCode.Trim() == "S-1A" ||
                        GradeCode.Trim() == "SS-1A" ||
                        GradeCode.Trim() == "SS-1B"
                        ||
                        GradeCode.Trim() == "S-1" ||
                        GradeCode.Trim() == "SS-1" ||
                        GradeCode.Trim() == "SS-1B" ||
                        GradeCode.Trim() == "M-3" ||
                        GradeCode.Trim() == "M-2" ||
                        GradeCode.Trim() == "M-6" ||
                        GradeCode.Trim() == "M-0" ||
                        GradeCode.Trim() == "S-0")
                    {
                        

                        if (lbl_Particulars.Text.Trim() == "House Rent")
                        {
                            HouseResnt = (Math.Round(basicAmount, 0)*63)/100;
                            lbl_SalaryBreakUp.Text = Math.Round(HouseResnt, 0).ToString();
                        }

                        if (lbl_Particulars.Text.Trim() == "Medical")
                        {
                            Medical = 0;
                            lbl_SalaryBreakUp.Text = Medical.ToString();
                        }

                        if (lbl_Particulars.Text.Trim() == "Conveyance")
                        {
                            DataTable dtConveyance =
                                aDAL.CheckConveyanceByMasterCode(EmpMasterCode.Trim());

                            if (dtConveyance.Rows.Count > 0)
                            {
                                Conveyance = 0;
                            }
                            else
                            {
                                Conveyance = 0;
                            }
                            lbl_SalaryBreakUp.Text = Conveyance.ToString();
                        }

                        if (lbl_Particulars.Text.Trim() == "Washing")
                        {
                            lbl_SalaryBreakUp.Text = "0";
                            Washing = 0;
                            lbl_Particulars.Visible = true;
                            lbl_SalaryBreakUp.Visible = true;
                        }
                    }
                }

                catch (Exception)
                {
                    //throw;
                }


                decimal res2 = Convert.ToDecimal(lbl_SalaryBreakUp.Text);

                res += Math.Round(res2, 0);
            }

            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                Label lbl_Particulars =
                    (Label)
                        KeyResponGridView.Rows[i].FindControl("lbl_Particulars");

                Label lbl_SalaryBreakUp =
                    (Label)
                        KeyResponGridView.Rows[i].FindControl("lbl_SalaryBreakUp");


                if (lbl_Particulars.Text.Trim() == "Total")
                {
                    DataTable aDataTable =
                        aDAL.LoadSalaryStepGradeMId(Convert.ToInt32(SalaryGradeId),
                            Convert.ToInt32(SalaryStepId));

                    Decimal GrossAmount = 0;
                    try
                    {
                        GrossAmount = Math.Round(Convert.ToDecimal(aDataTable.Rows[0]["GrossAmount"].ToString()), 0);
                    }
                    catch (Exception)
                    {
                    }

                    lbl_Particulars.Font.Bold = true;
                    lbl_SalaryBreakUp.Text = GrossAmount.ToString();
                }

                if (lbl_Particulars.Text.Trim() == "Medical")
                {
                    DataTable aDataTable =
                        aDAL.LoadSalaryStepGradeMId(Convert.ToInt32(SalaryGradeId),
                            Convert.ToInt32(SalaryStepId));

                    Decimal GrossAmount = 0;
                    try
                    {
                        GrossAmount = Math.Round(Convert.ToDecimal(aDataTable.Rows[0]["GrossAmount"].ToString()), 0);
                    }
                    catch (Exception)
                    {
                    }
                    if (GrossAmount != res)
                    {
                        if (lbl_Particulars.Text.Trim() == "Medical")
                        {
                            decimal newREs = 0;

                            newREs = GrossAmount - res;

                            decimal medi = 0;
                            try
                            {
                                medi = Convert.ToDecimal(lbl_SalaryBreakUp.Text.Trim());
                            }
                            catch (Exception)
                            {
                                //throw;
                            }

                            decimal mainres = medi + newREs;


                            lbl_SalaryBreakUp.Text = mainres.ToString();
                        }
                    }
                }
            }
            DataTable aDataTable2 =
                aDAL.LoadSalaryStepGradeMId(Convert.ToInt32(SalaryGradeId), Convert.ToInt32(SalaryStepId));
            Decimal GrossAmount2 = 0;
            try
            {
                GrossAmount2 = Math.Round(Convert.ToDecimal(aDataTable2.Rows[0]["GrossAmount"].ToString()), 0);
            }
            catch (Exception)
            {
            }




            EmpSalaryInfoDAO aMaster = new EmpSalaryInfoDAO();

            aMaster.EmpSalaryInfoId = 0;
            aMaster.EmpInfoId = EmpID;



            aMaster.BasicPay = Convert.ToDecimal(basicAmount);
            aMaster.HouseRent = Convert.ToDecimal(HouseResnt);

             
                aMaster.Medical = Convert.ToDecimal(Medical);
            

            
                aMaster.Conveyance = Convert.ToDecimal(Conveyance);
            
           
                aMaster.Washing = Convert.ToDecimal(Washing);
           
              DataTable dtMaster = AMAsterDal.GetMasterEmpDataById(EmpID.ToString());

              string PaymentType = "";
              string BankNameId = "";
              string BankAccountNo = "";
              string PF = "";
              string MonthlyTax = "";
            bool ProvidentFundEligible = false;

            int EmpSalaryInfoIdforUp = 0;
            if (dtMaster.Rows.Count > 0)
            {
                EmpSalaryInfoIdforUp = Convert.ToInt32(dtMaster.Rows[0]["EmpSalaryInfoId"].ToString());
                PaymentType = dtMaster.Rows[0]["PaymentType"].ToString();
                BankNameId = dtMaster.Rows[0]["BankNameId"].ToString();

                if (dtMaster.Rows[0]["ProvidentFundEligible"].ToString() == "True")
                {
                    ProvidentFundEligible = true;
                }
                if (dtMaster.Rows[0]["ProvidentFundEligible"].ToString() == "False")
                {
                    ProvidentFundEligible = false;
                }
                BankAccountNo = dtMaster.Rows[0]["BankAccountNo"].ToString();
                PF = dtMaster.Rows[0]["PF"].ToString();
                MonthlyTax = dtMaster.Rows[0]["MonthlyTax"].ToString();
            }
            aMaster.PaymentType = (PaymentType);
            if (BankNameId != "")
            {
                aMaster.BankNameId = Convert.ToInt32(BankNameId);
            }
            else
            {
                aMaster.BankNameId = null;
            }

            aMaster.BankAccountNo = (BankAccountNo);
            if (ProvidentFundEligible)
            {
                aMaster.ProvidentFundEligible = true;

            }
            else
            {
                aMaster.ProvidentFundEligible = false;

            }
            if (PF != "")
            {
                aMaster.PF = Convert.ToDecimal(PF);
            }
            else
            {
                aMaster.PF = null;
            }

            if ( MonthlyTax != "")
            {
                aMaster.MonthlyTax = Convert.ToDecimal(MonthlyTax);
            }
            else
            {
                aMaster.MonthlyTax = null;
            }




              AMAsterDal.UpdateEmpSalStatus(EmpSalaryInfoIdforUp);
            int pssk = AMAsterDal.SaveEmpSalaryMaster(aMaster, Session["UserId"].ToString());
        }
    }
    MiscellaneousInformationDAL AMAsterDal = new MiscellaneousInformationDAL();


    private List<IncrementDao> PrepareDataForUpdate()
    {
        List<IncrementDao> aDaos = new List<IncrementDao>();
        IncrementDao aDao;

        List<EmpGeneralInfo> aList = new List<EmpGeneralInfo>();
        EmpGeneralInfo aInfo;


        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");


            //DataTable aTable =
            //              aIncrementDal.ValidattionForEffectiveDate(
            //                     (loadGridView.DataKeys[i].Value.ToString()), EffectiveDateTextBox.Text, ddlIncrementType.SelectedValue);

            if (chkBoxRows.Checked )
            {
                aDao = new IncrementDao();

                aDao.IncrementId = Convert.ToInt32(hdpk.Value);
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

                if (manualUpdateCheckBox.Items[1].Selected == true)
                {
                    aDao.AutoProcess = "Manually Updated";

                    Int32 empGenId = 0;
                    Int32 stepId = 0;

                    empGenId = Convert.ToInt32(loadGridView.DataKeys[i].Value.ToString());
                    stepId = Convert.ToInt32(ddlIncrementalStep.SelectedValue);

                    UpdateEmployeeStepId(empGenId, stepId);
                }

                aDao.UpdateBy = Convert.ToInt32(Session["UserId"]);
                aDao.UpdateDate = DateTime.Now;

                aDaos.Add(aDao);
            }
            //else
            //{
            //    aShowMessage.ShowMessageBox("Employee Already Exists within this effective date!!", this);
            //}
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

            if (chkBoxRows.Checked )
            {
               // if (ddlIncrementType.SelectedItem.Text != "Step Adjustment")
                {
                    var ddlIncrementalStep = (DropDownList)loadGridView.Rows[i].FindControl("incrementalStepDropDownList");
                    if (ddlIncrementalStep.SelectedValue == "")
                    {
                        ShowMessageBox("Please select incremental step !!!");
                        return false;
                    }   
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
        if (ddlIncrementType.SelectedItem.Text == "Step Adjustment")
        {
            
        }

        else if (ddlIncrementType.SelectedValue == "5")
        {
            
        }
        
        else

        {
            DropDownList dropDown = (DropDownList)sender;
            GridViewRow currentRow = (GridViewRow)dropDown.Parent.Parent;
            int rowindex = 0;
            rowindex = currentRow.RowIndex;
            Label lblSalaryStep = (Label)loadGridView.Rows[rowindex].Cells[0].FindControl("lblSalaryStep");
            HiddenField HFSalaryStep = (HiddenField)loadGridView.Rows[rowindex].Cells[0].FindControl("HFSalaryStep");

            int currentStep = 0;
            int IncreStep = 0;
            int IncreStepVal = 0;
           
            var ddlIncrementalStep = (DropDownList)loadGridView.Rows[rowindex].Cells[0].FindControl("incrementalStepDropDownList");
            string empName = lblSalaryStep.Text.Trim();
            if (empName.Contains('-'))
            {
                string[] emp = empName.Split('-');

                try
                {
                     currentStep = Convert.ToInt32(emp[1]);
                }
                catch (Exception)
                {
                    
                   //throw;
                }
            }

            string IncreempName = ddlIncrementalStep.SelectedItem.Text.Trim();
            if (IncreempName.Contains('-'))
            {
                string[] Incre = IncreempName.Split('-');

                try
                {
                    IncreStep = Convert.ToInt32(Incre[1]);

                    IncreStepVal = Convert.ToInt32(ddlIncrementalStep.SelectedValue);
                }
                catch (Exception)
                {
                    
                    //throw;
                }
            }


            if (IncreStep <= currentStep)
            {
                ddlIncrementalStep.SelectedValue = HFSalaryStep.Value;
                ShowMessageBox("Incremental step must be greater than current salary step !!!");
                ddlIncrementalStep.Focus();
            }
        }
      
    }

    protected void clearButton_OnClick(object sender, EventArgs e)
    {
       Clear();
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("IncrementView.aspx");
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

    protected void btn_Update_OnClick(object sender, EventArgs e)
    {
        if (DataValidation())
        {
            Int32 id = UpdateIncrementInfo(PrepareDataForUpdate());

            if (id > 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Operation Successfully Done...');window.location ='IncrementView.aspx';",
                       true);
                Clear();
            }
            else
            {

            }
        }
    }

    protected void manualUpdateCheckBox_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        submitButton.Visible = true;  
    }
}