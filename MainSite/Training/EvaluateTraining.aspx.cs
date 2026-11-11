using System.Net;
using System.Net.Mail;
using DAL.COMMON_DAL;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Training_EvaluateTraining : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();

    private int mid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();

                if (mid > 0)
                {

                    DataTable dtD = _trainingDal.GetEvaluationFormDetails2(mid);

                    try
                    {
                        txtComments.Text = dtD.Rows[0].Field<string>("Comments").ToString();
                    }
                    catch (Exception)
                    {
                        txtComments.Text = "";
                        //throw;
                    }
                    ViewState["Evaluation"] = dtD;
                    gv_Topic.DataSource = dtD;
                    gv_Topic.DataBind();

                    GetSingleName();

                    if (Session["ForView"].ToString()!="")
                    {
                        btn_Save.Visible = true;
                    }


                    DataTable dtD22 = _trainingDal.GetEvaluationFormDetails2EditMode(mid);

                    if (dtD22.Rows.Count>0)
                    {
                         
                    }

                }
                else
                {
                    gv_Topic.DataSource = null;
                    gv_Topic.DataBind();

                }
                
            //    if (Session["ForView"].ToString() == "View")
            //{
            //    btn_Save.Visible = false;
            //}
            }

           
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
    public void GetSingleName()
    {
        if (gv_Topic.Rows.Count > 0)
        {


            string masterText = ((Label)gv_Topic.Rows[0].FindControl("gv_heading")).Text;
            for (int i = 1; i < gv_Topic.Rows.Count; i++)
            {
                if (masterText.Trim() == ((Label)gv_Topic.Rows[i].FindControl("gv_heading")).Text.Trim())
                {
                    ((Label)gv_Topic.Rows[i].FindControl("gv_heading")).Text = "";
                }
                else
                {
                    masterText = ((Label)gv_Topic.Rows[i].FindControl("gv_heading")).Text.Trim();
                }
            }
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        EvaluateTrainingMaster aMaster = new EvaluateTrainingMaster();

        aMaster.EvaluationFormMasterId = Convert.ToInt32(hdpk.Value);
        aMaster.TrainingRecordMasterId = Convert.ToInt32(hdpk.Value);
        aMaster.EmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
        aMaster.EntryBy = Session["UserId"].ToString();
        aMaster.Comments = txtComments.Text.Trim();
        aMaster.EntryDate = DateTime.Now;

        using (var db = new HRIS_SMCEntities())
        {
            var comvar = (from t in db.tblEmpGeneralInfoes
                          where t.EmpInfoId == aMaster.EmpInfoId
                          select t).FirstOrDefault();




            int? iddd = null;
            if (comvar != null)
            {
                try
                {
                     iddd = (int)comvar.ReportingEmpId;
                }
                catch (Exception)
                {
                    iddd = null;
                    //throw;
                }
            }

            if (iddd!=null)
            {
                aMaster.ReportingEmpId = Convert.ToInt32(iddd.ToString());
            }
            else
            {
                aMaster.ReportingEmpId = null;

            }
          

        }


        int id = _trainingDal.SaveEvaluationEmployee(aMaster);
        List<EvaluatTrainingDetails> aList = new List<EvaluatTrainingDetails>();

        for (int i = 0; i < gv_Topic.Rows.Count; i++)
        {
            CheckBox failed = (CheckBox)gv_Topic.Rows[i].FindControl("chk_failed");
            CheckBox average = (CheckBox)gv_Topic.Rows[i].FindControl("chk_avarage");
            CheckBox abvaverage = (CheckBox)gv_Topic.Rows[i].FindControl("chk_abvavarage");
            CheckBox exec = (CheckBox)gv_Topic.Rows[i].FindControl("chk_excellent");
            HiddenField detailsId = (HiddenField)gv_Topic.Rows[i].FindControl("detailsId");
            EvaluatTrainingDetails aInfo = new EvaluatTrainingDetails();
            aInfo.EvaluationFormDetailsId = Convert.ToInt32(detailsId.Value);
            //if (failed.Checked)
            {
                aInfo.IsFailed = failed.Checked;
            }
            //if (average.Checked)
            {
                aInfo.IsAverage = average.Checked;
            }
            //if (abvaverage.Checked)
            {
                aInfo.IsAbvAverage = abvaverage.Checked;
            }
            //if (exec.Checked)
            {
                aInfo.IsExcellent = exec.Checked;
            }
            aList.Add(aInfo);
        }

   bool idd=     _trainingDal.SaveEvaluateEmployeeDetails(aList, id);

        int EmpID = aMaster.EmpInfoId;
        string EmpName = "";
        string sTrainingTitle = "";

        int TRMaID = aMaster.TrainingRecordMasterId;
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
                EmpName =  comvar.EmpName;
            }



        }


        SenMailForApprved(aMaster.EmpInfoId, " Training Evaluation ", @"  <br/> Dear Sir, <br/>
 Employee Name: "+EmpName+@". <br/>
 Training Title: " + sTrainingTitle + @". <br/>
has been Evaluated.<br/>
<br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");

        ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successful...');window.location ='EvaluationForEmployee.aspx';",
                true);
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
                comId = (int) comvar.CompanyId;
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

        string ForMailAddress = "";
        string HeadPersonMail = "";
        using (var db = new HRIS_SMCEntities())
        {
            var GetMailAddress = (from t in db.tblEmpGeneralInfoes
                                  where t.EmpInfoId == forEmpID
                                  select t).FirstOrDefault();

            var HeadPersonMailAddress = (from t in db.tblEmpGeneralInfoes
                                         where t.EmpInfoId == HeadPerson
                                  select t).FirstOrDefault();
            if (HeadPersonMailAddress != null)
            {
                HeadPersonMail = HeadPersonMailAddress.OfficialEmail;
            }
            else
            {
                HeadPersonMail = null;
            }
            

            if (GetMailAddress != null)
            {
                ForMailAddress = GetMailAddress.OfficialEmail + HeadPersonMail;
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
             //   showMessageBox("Email has not Sent, Try Once More time");
            }
            catch (Exception exe)
            {
              //  showMessageBox("Email has not Sent, Try Once More time");
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
    protected void chk_failed_OnCheckedChanged(object sender, EventArgs e)
    {

        CheckBox lb = (CheckBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        int i = rowID;
        CheckBox failed = (CheckBox)gv_Topic.Rows[i].FindControl("chk_failed");
        CheckBox average = (CheckBox)gv_Topic.Rows[i].FindControl("chk_avarage");
        CheckBox abvaverage = (CheckBox)gv_Topic.Rows[i].FindControl("chk_abvavarage");
        CheckBox exec = (CheckBox)gv_Topic.Rows[i].FindControl("chk_excellent");
        failed.Checked = true;
        average.Checked = false;
        abvaverage.Checked = false;
        exec.Checked = false;
    }

    protected void chk_avarage_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox lb = (CheckBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        int i = rowID;
        CheckBox failed = (CheckBox)gv_Topic.Rows[i].FindControl("chk_failed");
        CheckBox average = (CheckBox)gv_Topic.Rows[i].FindControl("chk_avarage");
        CheckBox abvaverage = (CheckBox)gv_Topic.Rows[i].FindControl("chk_abvavarage");
        CheckBox exec = (CheckBox)gv_Topic.Rows[i].FindControl("chk_excellent");
        failed.Checked = false;
        average.Checked = true;
        abvaverage.Checked = false;
        exec.Checked = false;
    }

    protected void chk_abvavarage_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox lb = (CheckBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        int i = rowID;
        CheckBox failed = (CheckBox)gv_Topic.Rows[i].FindControl("chk_failed");
        CheckBox average = (CheckBox)gv_Topic.Rows[i].FindControl("chk_avarage");
        CheckBox abvaverage = (CheckBox)gv_Topic.Rows[i].FindControl("chk_abvavarage");
        CheckBox exec = (CheckBox)gv_Topic.Rows[i].FindControl("chk_excellent");
        failed.Checked = false;
        average.Checked = false;
        abvaverage.Checked = true;
        exec.Checked = false;
    }

    protected void chk_excellent_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox lb = (CheckBox)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        int i = rowID;
        CheckBox failed = (CheckBox)gv_Topic.Rows[i].FindControl("chk_failed");
        CheckBox average = (CheckBox)gv_Topic.Rows[i].FindControl("chk_avarage");
        CheckBox abvaverage = (CheckBox)gv_Topic.Rows[i].FindControl("chk_abvavarage");
        CheckBox exec = (CheckBox)gv_Topic.Rows[i].FindControl("chk_excellent");
        failed.Checked = false;
        average.Checked = false;
        abvaverage.Checked = false;
        exec.Checked = true;
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
         Response.Redirect("EvaluationForEmployee.aspx");
    }
}