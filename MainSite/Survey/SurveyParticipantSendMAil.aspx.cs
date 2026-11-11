using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.TrainingDAL;
using DAL.Transfer_DAL;

public partial class Survey_SurveyParticipantSendMAil : System.Web.UI.Page
{
    private int mid = 0;
    SurveyDeclaretionEntryDAL atblEmployeePromotionEntryDAL = new SurveyDeclaretionEntryDAL();
    public TrainingRecordDAL _recordDal = new TrainingRecordDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();


                DataTable EmployeedataTable = atblEmployeePromotionEntryDAL.GeEmpGeneralInfoById(hdpk.Value);

                if (EmployeedataTable.Rows.Count > 0)
                {
                    gv_selectedEmp.DataSource = EmployeedataTable;
                    gv_selectedEmp.DataBind();
                }

               
                
            }
        }
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
         
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
       Response.Redirect("SurveyDeclaretionView.aspx");
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {

        if (txtMailBody.Text!="")
        {
            try
            {

                for (int i = 0; i < gv_selectedEmp.Rows.Count; i++)
                {

                    Label EmpMail = (Label)gv_selectedEmp.Rows[i].Cells[1].FindControl("txt_OfficialEmail");

                    

                    System.Threading.Thread.Sleep(100);

                    MailMessage mail = new MailMessage();

                   

                    //mail.To.Add
                 


                  

                    //Attach file using FileUpload Control and put the file in memory stream

                 


                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress(Session["EmailID"].ToString());
                  

                    try
                    {
                        mail.To.Add(EmpMail.Text.Trim()  );
                    }
                    catch (Exception)
                    {

                        //throw;
                    }
                    mail.Subject = "Survey Invitation";
                    mail.Body = "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +

                        WebUtility.HtmlDecode(txtMailBody.Text)
                       +


                   "</div>";

                    //Attach file using FileUpload Control and put the file in memory stream
                    if (fu_cv.HasFile)
                    {
                        mail.Attachments.Add(new Attachment(fu_cv.PostedFile.InputStream, fu_cv.FileName));
                    }
                    mail.IsBodyHtml = true;
                    mail.Priority = System.Net.Mail.MailPriority.High;

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential(Session["EmailID"].ToString(), Session["AppPass"].ToString());
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                 "alert",
                 "alert('Sent Successfully...! ');window.location ='SurveyDeclaretionView.aspx';",
                 true);

                }

            }
            catch (Exception)
            {

                //throw;
            }
        }
        else
        {
            showMessageBox("Please Enter Mail Body !!!");
        }

        
    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
}