using DAL.Appraisal;
using DAL.Lunch_Allowance_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Lunch_Allowance_UI_LunchAllownaceCancelSelfEntry : System.Web.UI.Page
{

    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();

    LunchAllowanceCancelDAL allowanceCancelDal = new LunchAllowanceCancelDAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ButtonVisible();

            
            if (!string.IsNullOrEmpty(Request.QueryString["MID"]))
            {

                EmpId.Value = Request.QueryString["MID"].ToString();
                GetEmpinfo(Request.QueryString["MID"]);
                onRecord(Convert.ToInt32(Request.QueryString["MID"]), Session["CancelDate"].ToString());
                Session["CancelDate"] = "";
            }
            else
            {

                if (Session["EmpInfoId"] != null)
                {
                    EmpId.Value = Session["EmpInfoId"].ToString();
                    GetEmpinfo(Session["EmpInfoId"].ToString());
                }

                DateTime dta = DateTime.Now.AddDays(1);
                txtCancelDate.Text = dta.ToString("dd-MMM-yyyy");
                txtCancelToDate.Text = dta.ToString("dd-MMM-yyyy");

                CalendarExtender1.StartDate = DateTime.Now.AddDays(1);
                CalendarExtender2.StartDate = DateTime.Now.AddDays(1);
            }

        }
    }


    private void onRecord(Int32 id,string Date)
    {
        DataTable dtMaster = allowanceCancelDal.GetLunchCancelInfoByEmpId(id, Date);
        if (dtMaster.Rows.Count > 0)
        {

            txtCancelDate.Text = dtMaster.Rows[0]["EffectiveDate"].ToString();
            txtCancelToDate.Text = dtMaster.Rows[0]["EffectiveDate"].ToString();

            txtComments.Text = dtMaster.Rows[0]["Remarks"].ToString();

        }
    }


    public void ButtonVisible()
    {
        if (Session["CancelStatus"] != null)
        {

            if (Session["CancelStatus"].ToString() == "Add")
            {
                btnSubmit.Visible = true;
                DraftButton.Visible = true;

            }

            if (Session["CancelStatus"].ToString() == "Edit")
            {
                btnSubmit.Visible = true;
               // DraftButton.Visible = true;
            }

        }
        else
        {
            Response.Redirect("LunchAllownaceCancelSelfList.aspx");


        }

    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
      //  Response.Redirect("LunchAllownaceCancelSelf.aspx");
        Response.Redirect("LunchAllownaceCancelSelfList.aspx");
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    public void GetEmpinfo(string id)
    {
        EmpInfoId.Value = id;
        DataTable dtEmp = _appPartA.GetEmployeeDetails(Convert.ToInt32(id));
        if (dtEmp.Rows.Count > 0)
        {
            lblEmpId.Text = dtEmp.Rows[0]["EmpMasterCode"].ToString().Trim();
            lblEmployeeName.Text = dtEmp.Rows[0]["EmpName"].ToString().Trim();
            deptNameLabel.Text = dtEmp.Rows[0]["DepartmentName"].ToString().Trim();
            desigNameLabel.Text = dtEmp.Rows[0]["Designation"].ToString().Trim();
            joiningDateLabel.Text = Convert.ToDateTime(dtEmp.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");
            LocationLabel.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
            lblPlace.Text = dtEmp.Rows[0]["Location"].ToString();
            ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();
            HFCompanyId.Value = dtEmp.Rows[0]["CompanyId"].ToString();

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

    protected void DraftButton_OnClick(object sender, EventArgs e)
    {

        if (validfa())
        {

            DateTime timenow = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString());
            DateTime ExcaTime = Convert.ToDateTime(DateTime.Now.ToString("15:00:0"));


            DateTime exxx = ExcaTime.AddDays(1);
            string tttt = timenow.ToString("dd-MMM-yyyy");

            if (txtCancelDate.Text.Trim() == tttt)
            {
                DateTime SubmitTime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString());

                DataTable dtdata = allowanceCancelDal.GetLunchAllowEmpCheckSecond(SubmitTime, exxx);

                int countt = 0;
                if (dtdata.Rows.Count > 0)
                {
                    countt = Convert.ToInt32(dtdata.Rows[0]["CoutSecend"].ToString());
                }
                if (countt > 0)
                {
                    bool save = false;
                    SaveMethod("Draft", save);

                }
                else
                {
                    AlertMessageBoxShow("Please Submit before 3:00PM ");

                }

            }
            else
            {
                bool save = false;
                SaveMethod("Draft", save);
            }
        }

    }


    private void SaveMethod(string ActionStatus, bool save)
    {

        SenMailForApprved(Convert.ToInt32(920), "Employee lunch Cancellation", @"  <br/> Dear Sir, <br/>
Employee ID: " + lblEmpId.Text + @" <br/>
Employee Name: " + lblEmployeeName.Text + @"<br/> 
Department:" + deptNameLabel.Text + @"<br/>
From Date:" + txtCancelDate.Text + @"<br/>
To Date:" + txtCancelToDate.Text + @"<br/><br/><br/>
 please login for the details from the below link.<br/>  https://hris.smc-bd.org/ <br/>
Thank You.
");

        SenMailForApprved(Convert.ToInt32(1393), "Employee lunch Cancellation", @"  <br/> Dear Sir, <br/>
Employee ID: " + lblEmpId.Text + @" <br/>
Employee Name: " + lblEmployeeName.Text + @"<br/> 
Department:" + deptNameLabel.Text + @"<br/>
From Date:" + txtCancelDate.Text + @"<br/>
To Date:" + txtCancelToDate.Text + @"<br/><br/><br/>
 please login for the details from the below link.<br/>  https://hris.smc-bd.org/ <br/>
Thank You.
");
        LunchAllownceCancelDAO allownceCancelDao = new LunchAllownceCancelDAO();
        allownceCancelDao.EmpInfoId = Convert.ToInt32(EmpId.Value);
        DataTable dtdata = allowanceCancelDal.GetEmpInfo(allownceCancelDao.EmpInfoId.ToString());
        if (dtdata.Rows.Count > 0)
        {

            DateTime fromDate = Convert.ToDateTime(txtCancelDate.Text);
            DateTime toDate = Convert.ToDateTime(txtCancelToDate.Text);

            for (DateTime cancelDate = fromDate.Date; cancelDate <= toDate.Date; cancelDate = cancelDate.AddDays(1))
            {
                allownceCancelDao.CompanyId = Convert.ToInt32(Session["CompanyId"].ToString());
                try
                {
                    allownceCancelDao.DepartmentId = Convert.ToInt32(dtdata.Rows[0]["DepartmentId"].ToString());
                }
                catch (Exception)
                {
                    allownceCancelDao.DepartmentId = null;
                    //throw;
                }
                try
                {
                    allownceCancelDao.DivisionId = Convert.ToInt32(dtdata.Rows[0]["DivisionId"].ToString());
                }
                catch (Exception)
                {
                    allownceCancelDao.DivisionId = null;
                    //throw;
                }


                allownceCancelDao.EffectiveDate = cancelDate;

                allownceCancelDao.Remarks = txtComments.Text;
                int divWing = 0;


                try
                {
                    if (dtdata.Rows[0]["DivisionWId"] != null)
                    {
                        divWing = Convert.ToInt32(dtdata.Rows[0]["DivisionWId"].ToString());
                    }
                    allownceCancelDao.DivisionWId = //Convert.ToInt32(dtdata.Rows[0]["DivisionWId"].ToString());
                        divWing > 0 ? int.Parse(divWing.ToString()) : (int?)null;
                }
                catch (Exception)
                {
                    allownceCancelDao.DivisionWId = null;
                }

                try
                {
                    int subSec = Convert.ToInt32(dtdata.Rows[0]["SubSectionId"].ToString());

                    allownceCancelDao.SubSectionId =
                        //Convert.ToInt32(dtdata.Rows[0]["SubSectionId"].ToString());
                        subSec > 0 ? int.Parse(subSec.ToString()) : (int?)null;
                }
                catch (Exception)
                {
                    allownceCancelDao.SubSectionId = null;
                    //throw;
                }

                try
                {
                    int secId = Convert.ToInt32(dtdata.Rows[0]["SectionId"].ToString());
                    allownceCancelDao.SectionId = secId > 0 ? int.Parse(secId.ToString()) : (int?)null;
                }
                catch (Exception)
                {
                    allownceCancelDao.SectionId = null;
                    //throw;
                }

                allownceCancelDao.LunchAlllowCancelId =
                    allownceCancelDao.EmpInfoId = Convert.ToInt32(EmpId.Value);

                try
                {
                    allowanceCancelDal.DeleteLunchAllowCancel(allownceCancelDao);
                }
                catch (Exception)
                {
                    //throw;
                }

                allownceCancelDao.ActionStatus = ActionStatus;
                allowanceCancelDal.SaveLunchAllowCancel(allownceCancelDao);



                save = true;
            }


          
            if (save)
            {

         

                ScriptManager.RegisterStartupScript(this, this.GetType(),
          "alert",
          "alert('Operation Successful...! ');window.location ='LunchAllownaceCancelSelfList.aspx';",
          true);
            }
        }

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

                using (SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;

                    // Use your actual Office 365 credentials
                    smtpClient.Credentials = new NetworkCredential("no-reply@smc-bd.org", "vfwzmbxprdmqhhln");

                    // Set timeout (in milliseconds)
                    smtpClient.Timeout = 20000;

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress("no-reply@smc-bd.org");
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

    private bool Valisddate()
    {

        DateTime timenow = Convert.ToDateTime(DateTime.Now.ToString("hh:mm:ss"));
        DateTime ExcaTime = Convert.ToDateTime(DateTime.Now.ToString("15:00:0"));


        if (timenow > ExcaTime)
        {

        }
        else
        {

            AlertMessageBoxShow("Please Submit before 3:00PM ");
            return true;

        }

        //if (loadGridView.Rows.Count == 0)
        //{
        //    AlertMessageBoxShow("Table Can not be Empty!!");
        //    return true;
        //}


        //for (int i = 0; i < loadGridView.Rows.Count; i++)
        //{
        //    CheckBox chk = ((CheckBox)loadGridView.Rows[i].FindControl("inactiveCheckBox"));
        //    if (chk.Checked == false)
        //    {
        //        AlertMessageBoxShow("You already   cancel for this effective date...");
        //        effectiveDateTextBox.Focus();

        //        return false;
        //    }
        //}

        //if ()
        //{
        //    AlertMessageBoxShow("Please Select Year...");
        //    txtToDate.Focus();
        //    return false;


        //}

        return true;
    }

    private bool validfa()
    {
        

        if ((txtCancelDate.Text == ""))
        {
            AlertMessageBoxShow("Please Select from Cancel Date...");
            txtCancelDate.Focus();
            return false;


        }

        if ((txtCancelToDate.Text == ""))
        {
            AlertMessageBoxShow("Please Select to Cancel Date...");
            txtCancelToDate.Focus();
            return false;


        }

        DateTime fromDate;
        DateTime toDate;
        if (!DateTime.TryParse(txtCancelDate.Text, out fromDate))
        {
            AlertMessageBoxShow("Please Select valid from Cancel Date...");
            txtCancelDate.Focus();
            return false;
        }

        if (!DateTime.TryParse(txtCancelToDate.Text, out toDate))
        {
            AlertMessageBoxShow("Please Select valid to Cancel Date...");
            txtCancelToDate.Focus();
            return false;
        }

        if (toDate.Date < fromDate.Date)
        {
            AlertMessageBoxShow("To Cancel Date can not be earlier than From Cancel Date...");
            txtCancelToDate.Focus();
            return false;
        }

        return true;
    }


    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        if (validfa())
        {
            
      

      //  HiddenField hfEmpInfoId = ((HiddenField)loadGridView.Rows[rowindex].Cells[1].FindControl("hfEmpInfoId"));
        DataTable dttime = allowanceCancelDal.GetLuchSetupTime();
        DateTime timenow = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString());
        DateTime ExcaTime = Convert.ToDateTime(DateTime.Now.ToString(dttime.Rows[0]["LunchTime"].ToString()));

        DateTime exxx = ExcaTime.AddDays(1);
        string tttt = timenow.ToString("dd-MMM-yyyy");

        if (txtCancelDate.Text.Trim() == tttt)
        {
            DateTime SubmitTime = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString());

            DataTable dtdata = allowanceCancelDal.GetLunchAllowEmpCheckSecond(SubmitTime, exxx);

            int countt = 0;
            if (dtdata.Rows.Count > 0)
            {
                countt = Convert.ToInt32(dtdata.Rows[0]["CoutSecend"].ToString());
            }
            if (countt > 0)
            {
                bool save = false;
                SaveMethod("Approved", save);

            }
            else
            {
                AlertMessageBoxShow("Please Submit before " + ExcaTime);

            }

        }
        else
        {
            bool save = false;
            SaveMethod("Approved", save);
        }
        }
    }

  

}
