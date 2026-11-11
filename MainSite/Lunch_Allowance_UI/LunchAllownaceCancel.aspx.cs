using DAL.COMMON_DAL;
using DAL.Lunch_Allowance_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Lunch_Allowance_UI_LunchAllownaceCancel : System.Web.UI.Page
{
    LunchAllowanceCancelDAL allowanceCancelDal=new LunchAllowanceCancelDAL();
    ShowMessage aShowMessage = new ShowMessage();
    private Lunch_Allowance_Dal_Common commmDAl = new Lunch_Allowance_Dal_Common();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            submitButton.Visible = false;

            DateTime dta = DateTime.Now;
            effectiveDateTextBox.Text = dta.ToString("dd-MMM-yyyy");
            txtCancelToDate.Text = dta.ToString("dd-MMM-yyyy");

            CalendarExtender1.StartDate = DateTime.Now;
            CalendarExtender2.StartDate = DateTime.Now;
            DropDownList();
        }
    }
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    public void DropDownList()
    {
        allowanceCancelDal.LoadCompany(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_OnSelectedIndexChanged(null, null);
        using (DataTable dt = _commonDataLoad.GetDDLComCategory())
        {
            ddlEmpCategory.DataSource = dt;
            ddlEmpCategory.DataValueField = "Value";
            ddlEmpCategory.DataTextField = "TextField";
            ddlEmpCategory.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetDDLDesignationAll())
        {
            ddlDesignation.DataSource = dt;
            ddlDesignation.DataValueField = "Value";
            ddlDesignation.DataTextField = "TextField";

            ddlDesignation.DataBind();

            ddlDesignation.SelectedValue = "Please Select One..";
        }
    }

    protected void Button1_OnClick(object sender, EventArgs e)
    {
        submitButton.Visible = false;

        if (validfa())
        {


            DataTable dtdata = allowanceCancelDal.GetLunchAllowEmpActive(effectiveDateTextBox.Text, txtCancelToDate.Text, GenerateParameter(), GenerateParameter_mm());
        loadGridView.DataSource = dtdata;
        loadGridView.DataBind();
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            submitButton.Visible = true;
            HiddenField hfActionStatus = ((HiddenField)loadGridView.Rows[i].FindControl("hfActionStatus"));
            if (hfActionStatus.Value == "Approved")
            {
                loadGridView.Rows[i].Visible = false;
            }
             
        }
        }
    }

    private bool validfa()
    {
        if ((companyDropDownList.SelectedValue == "0") )
        {
            AlertMessageBoxShow("Please Select company...");
            companyDropDownList.Focus();
            return false;


        }


        if ((effectiveDateTextBox.Text == ""))
        {
            AlertMessageBoxShow("Please Select from Cancel Date...");
            effectiveDateTextBox.Focus();
            return false;


        }

        if ((txtCancelToDate.Text == ""))
        {
            AlertMessageBoxShow("Please Select to Cancel Date...");
            txtCancelToDate.Focus();
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

    private string GenerateParameter()
    {
        string parameter = " ";

        if (companyDropDownList.SelectedIndex > 0)
        {
            parameter = parameter + "  and    e.CompanyId = '" + companyDropDownList.SelectedValue + "'";
        }

        if (ddlDivision.SelectedIndex > 0)
        {
            parameter = parameter + "  and    e.DivisionId = '" + ddlDivision.SelectedValue + "'";
        }

        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  and    e.DepartmentId = '" + ddlDepartment.SelectedValue + "'";
        }

        if (ddlSection.SelectedIndex > 0)
        {
            parameter = parameter + "  and    e.SectionId = '" + ddlSection.SelectedValue + "'";
        }

        if (ddlSubSection.SelectedIndex > 0)
        {
            parameter = parameter + "  and    e.SubSectionId = '" + ddlSubSection.SelectedValue + "'";
        }

        if (txtSearch.Text != "")
        {
            parameter = parameter + "  and (e.EmpMasterCode LIKE     '%" + txtSearch.Text.Trim() + "%' ) ";
        }

        if (NameTextBox.Text != "")
        {
            parameter = parameter + "  and  ( e.EmpName LIKE '%" + NameTextBox.Text.Trim() + "%')";
        }


        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and  e.EmpInfoId=" + ddlEmpInfo.SelectedValue + "";
        }

        if (ddlDesignation.SelectedIndex > 0)
        {
            parameter = parameter + "  and    e.DesignationId = '" + ddlDesignation.SelectedValue + "'";
        }

        if (ddlSalaryLocation.SelectedIndex > 0)
        {
            parameter = parameter + "  and    e.SalaryLoationId = '" + ddlSalaryLocation.SelectedValue + "'";
        }

        if (ddlConformationStatus.SelectedIndex > 0)
        {
            parameter = parameter + "  and    e.ConformationStatus = '" + ddlConformationStatus.SelectedValue + "'";
        }

        if (ActiveStatusDropDownList.SelectedIndex > 0)
        {
            parameter = parameter + "  and    e.IsActive = '" + ActiveStatusDropDownList.SelectedValue + "'";
        }


        if (ddlEmpCategory.SelectedIndex > 0)
        {
            parameter = parameter + "  and    e.EmpCategoryId = '" + ddlEmpCategory.SelectedValue + "'";
        }

        return parameter;
    }

    private string GenerateParameter_mm()
    {
        string parameter = " ";

        if (companyDropDownList.SelectedIndex > 0)
        {
            parameter = parameter + "  and   tblunchGuestInternInformation.CompanyId = '" + companyDropDownList.SelectedValue + "'";
        }

        if (ddlEmpCategory.SelectedIndex > 0)
        {
            parameter = parameter + "  and    tblunchGuestInternInformation.Type = '" + ddlEmpCategory.SelectedItem.Text + "'";
        }

        return parameter;
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
    protected void submitButton_OnClick(object sender, EventArgs e)
    {

        if (valiSubmit())
        {



            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                CheckBox chk = ((CheckBox) loadGridView.Rows[i].FindControl("chkSelect"));
                if (chk.Checked == true)
                {
                    LunchAllownceCancelDAO allownceCancelDao = new LunchAllownceCancelDAO();
                    allownceCancelDao.EmpInfoId = Convert.ToInt32(loadGridView.DataKeys[i][0].ToString());
                    allownceCancelDao.GuestInternInformationId = Convert.ToInt32(loadGridView.DataKeys[i][1].ToString());
                    TextBox txtComments = (TextBox)loadGridView.Rows[i].Cells[0].FindControl("txtComments");
                    Label hffromDate = (Label)loadGridView.Rows[i].Cells[0].FindControl("hffromDate");
                    if (allownceCancelDao.EmpInfoId > 0)
                    {
                        DataTable dtdata = allowanceCancelDal.GetEmpInfo(allownceCancelDao.EmpInfoId.ToString());
                        if (dtdata.Rows.Count > 0)
                        {

                            DataTable dtGetAllDate = allowanceCancelDal.GetAllDate(effectiveDateTextBox.Text,
                                txtCancelToDate.Text);


                            for (int kk = 0; kk < dtGetAllDate.Rows.Count; kk++)
                            {
                                DateTime ddddate = Convert.ToDateTime(dtGetAllDate.Rows[kk]["DateString"].ToString());
                                allownceCancelDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
                                try
                                {
                                    allownceCancelDao.DepartmentId =
                                        Convert.ToInt32(dtdata.Rows[0]["DepartmentId"].ToString());
                                }
                                catch (Exception)
                                {
                                    allownceCancelDao.DepartmentId = null;
                                    //throw;
                                }
                                try
                                {
                                    allownceCancelDao.DivisionId =
                                        Convert.ToInt32(dtdata.Rows[0]["DivisionId"].ToString());
                                }
                                catch (Exception)
                                {
                                    allownceCancelDao.DivisionId = null;
                                    //throw;
                                }
                                allownceCancelDao.EffectiveDate =
                                    Convert.ToDateTime(ddddate);

                                allownceCancelDao.Remarks = txtComments.Text;
                                int divWing = 0;


                                try
                                {

                                    if (dtdata.Rows[0]["DivisionWId"] != null)
                                    {
                                        divWing = Convert.ToInt32(dtdata.Rows[0]["DivisionWId"].ToString());
                                    }
                                    allownceCancelDao.DivisionWId =
                                        //Convert.ToInt32(dtdata.Rows[0]["DivisionWId"].ToString());
                                        divWing > 0 ? int.Parse(divWing.ToString()) : (int?) null;
                                }
                                catch (Exception)
                                {

                                    allownceCancelDao.DivisionWId = null;
                                }

                                try
                                {
                                    int subSec = Convert.ToInt32(dtdata.Rows[0]["SubSectionId"].ToString());

                                    allownceCancelDao.SubSectionId =
                                        allownceCancelDao.DivisionWId =
                                            //Convert.ToInt32(dtdata.Rows[0]["DivisionWId"].ToString());
                                            subSec > 0 ? int.Parse(subSec.ToString()) : (int?) null;
                                }
                                catch (Exception)
                                {
                                    allownceCancelDao.SubSectionId = null;
                                    //throw;
                                }

                                try
                                {
                                    int secId = Convert.ToInt32(dtdata.Rows[0]["SectionId"].ToString());
                                    allownceCancelDao.SectionId = secId > 0 ? int.Parse(secId.ToString()) : (int?) null;
                                }
                                catch (Exception)
                                {
                                    allownceCancelDao.SectionId = null;
                                    //throw;
                                }
                                allownceCancelDao.LunchAlllowCancelId =
                                    allownceCancelDao.EmpInfoId =
                                        Convert.ToInt32(loadGridView.DataKeys[i][0].ToString());

                                try
                                {
                                    allowanceCancelDal.DeleteLunchAllowCancel(allownceCancelDao);
                                }
                                catch (Exception)
                                {

                                    //throw;
                                }
                                allownceCancelDao.ActionStatus = "Approved";
                                allowanceCancelDal.SaveLunchAllowCancel(allownceCancelDao);
                            }

                        }
                   
                    }
                    else
                    {






                        int idd = commmDAl.SavAppLog(allownceCancelDao.GuestInternInformationId, "Cancel");


                        using (var db = new HRIS_SMC_DBEntities())
                        {
                            db.Database.ExecuteSqlCommand("delete dbo.tblunchGuestInternInformation  WHERE GuestInternInformationId={0}",
                        allownceCancelDao.GuestInternInformationId);
                        }
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successful...! ');window.location ='LunchAllownaceCancel.aspx';",
                true);
        }

    }

    private bool valiSubmit()
    {
        DataTable dttime = allowanceCancelDal.GetLuchSetupTime();
        DateTime timenow = Convert.ToDateTime(DateTime.Now.AddDays(1).ToString());
        DateTime ExcaTime = Convert.ToDateTime(DateTime.Now.ToString(dttime.Rows[0]["LunchTime"].ToString()));

        DateTime exxx = ExcaTime.AddDays(1);
        string tttt = timenow.ToString("dd-MMM-yyyy");

        if (effectiveDateTextBox.Text.Trim() == tttt)
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
            }
            else
            {
                AlertMessageBoxShow("Please Submit before " + ExcaTime);
                effectiveDateTextBox.Focus();
                return false;

            }
        }


        if (txtCancelToDate.Text.Trim() == tttt)
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
            }
            else
            {
                AlertMessageBoxShow("Please Submit before " + ExcaTime);
                txtCancelToDate.Focus();
                return false;

            }
        }

        return true;

    }

    protected void Button11_OnClick(object sender, EventArgs e)
    {
        
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

    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue != "")
        {
            _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
            _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
            _commonDataLoad.GetSectionByDivList(ddlSection, ddlDivision.SelectedValue);
            _commonDataLoad.GetSubSectionListAll(ddlSubSection, ddlDivision.SelectedValue);
        }
        else
        {
            ddlWing.Items.Clear();
            ddlDepartment.Items.Clear();
            ddlSection.Items.Clear();
            ddlSubSection.Items.Clear();
        }
    }

    protected void ddlWing_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
      
    }

    protected void ddlSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlSubSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void EmployeeDropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

        string empName = txtSearch.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            //EmployeeDropDownList.Text = emp[0];
            txtSearch.Text = emp[1];
        }
        //else
        //{
        //    txtSearch.Text = "";
        //    txtSearch.Text = "";
        //    //  EmpInfoIdHiddenField.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}
    }

    protected void EmployeeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string empName = NameTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            //EmployeeDropDownList.Text = emp[0];
            NameTextBox.Text = emp[2];

        }
        //else
        //{
        //    NameTextBox.Text = "";
        //    NameTextBox.Text = "";
        //  //  EmpInfoIdHiddenField.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}

    }

    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("LunchAllownaceCancel.aspx");
    }
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedIndex > 0)
        {
            Session["CompanyId"] = "";
            Session["CompanyId"] = companyDropDownList.SelectedValue;

            using (DataTable dt222 = _commonDataLoad.GetEmpDDLIsActive(companyDropDownList.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt222;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;
            }
            using (DataTable dt = _commonDataLoad.GetDDLComDivision(companyDropDownList.SelectedValue))
            {
                ddlDivision.DataSource = dt;
                ddlDivision.DataValueField = "Value";
                ddlDivision.DataTextField = "TextField";
                ddlDivision.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLComWind(companyDropDownList.SelectedValue))
            {
                ddlWing.DataSource = dt;
                ddlWing.DataValueField = "Value";
                ddlWing.DataTextField = "TextField";
                ddlWing.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComDepartment(companyDropDownList.SelectedValue))
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataValueField = "Value";
                ddlDepartment.DataTextField = "TextField";
                ddlDepartment.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComSection(companyDropDownList.SelectedValue))
            {
                ddlSection.DataSource = dt;
                ddlSection.DataValueField = "Value";
                ddlSection.DataTextField = "TextField";
                ddlSection.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComSubSection(companyDropDownList.SelectedValue))
            {
                ddlSubSection.DataSource = dt;
                ddlSubSection.DataValueField = "Value";
                ddlSubSection.DataTextField = "TextField";
                ddlSubSection.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLSalaryLocation())
            {
                ddlSalaryLocation.DataSource = dt;
                ddlSalaryLocation.DataValueField = "Value";
                ddlSalaryLocation.DataTextField = "TextField";
                ddlSalaryLocation.DataBind();
            }
        }
        else
        {
            ddlDivision.Items.Clear();
            ddlWing.Items.Clear();
                ddlDepartment.Items.Clear();
                ddlSection.Items.Clear();
                    ddlSubSection.Items.Clear();
                    ddlSalaryLocation.Items.Clear();
        }
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("LunchAllownaceCancelApproval.aspx");
    }
}