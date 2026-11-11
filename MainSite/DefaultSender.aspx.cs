using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DefaultSender : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //protected void btnSendEmail_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        // SMTP configuration
    //        SmtpClient smtpClient = new SmtpClient("shuvosmtp.office365.com", 587);
    //        smtpClient.EnableSsl = true;
    //        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
    //        smtpClient.UseDefaultCredentials = false;

    //        // Replace with your Office 365 credentials
    //        smtpClient.Credentials = new NetworkCredential("shuvono-reply@smc-bd.org", "vfwzmbxprdmqhhln");

    //        // Create email message
    //        MailMessage mailMessage = new MailMessage();
    //        mailMessage.From = new MailAddress("shuvono-reply@smc-bd.org");
    //        mailMessage.To.Add("jabe.saruarzahan.cse@gmail.com");
    //        mailMessage.Subject = "Test Email from ASP.NET";
    //        mailMessage.Body = "This is a test email sent from an ASP.NET web form using Office 365 SMTP.";

    //        // Send email
    //        smtpClient.Send(mailMessage);

    //        lblStatus.Text = "Email sent successfully!";
    //    }
    //    catch (Exception ex)
    //    {
    //        lblStatus.Text = "Error sending email: " + ex.Message;
    //    }
    //}

    protected void btnSendEmail_Click(object sender, EventArgs e)
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
                    mailMessage.To.Add("saruarzahan.cse@gmail.com");
                    mailMessage.Subject = "Test Email from ASP.NET";
                    mailMessage.Body = "This email was sent successfully!";
                    mailMessage.IsBodyHtml = false;

                    smtpClient.Send(mailMessage);
                    lblStatus.Text = "Email sent successfully!";
                }
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text =  ex.Message;
            if (ex.InnerException != null)
            {
                lblStatus.Text +=   ex.InnerException.Message;
            }
        }
    }
}