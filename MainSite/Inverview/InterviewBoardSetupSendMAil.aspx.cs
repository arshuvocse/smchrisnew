using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mail;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DAL.Inverview_DAL;
using DAL.TrainingDAL;
using MailMessage = System.Net.Mail.MailMessage;

public partial class Inverview_InterviewBoardSetupSendMAil : System.Web.UI.Page
{
    private InterviewCommonDAL _interviewCommonDAL = new InterviewCommonDAL();
    private int mid = 0;
    public TrainingRecordDAL _recordDal = new TrainingRecordDAL();
    private InterviewCandidateInfoListDAL aInterviewCandidateInfoList = new InterviewCandidateInfoListDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {



            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();

                int JobId = 0;
                int CompanyId = 0;

                using (DataTable dtg = _interviewCommonDAL.GetIVBoardSetupSendMailByMId(mid))
                {
                    if (dtg.Rows.Count > 0)
                    {

                        JobId = dtg.Rows[0].Field<int>("JobTitleId");
                        CompanyId = dtg.Rows[0].Field<int>("CompanyId");
                        txtMailBody.Text = dtg.Rows[0].Field<string>("EmailBody");
                        Session["JobIDDD"] = "";
                        Session["CompanyId"] = "";
                        Session["CompanyId"] = CompanyId.ToString();
                        Session["JobIDDD"] = JobId.ToString();
                        gv_selectedEmp.DataSource = dtg;
                        gv_selectedEmp.DataBind();
                    } 
                }


                using (
                DataTable dataTable =
                    aInterviewCandidateInfoList.GetInterviewCandidateInfoListForMailSend(" WHERE    JobID=" + JobId))
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        loadGridView.DataSource = dataTable;
                        loadGridView.DataBind();
                    }
                }




                gvGrading.Columns.Clear();
                InterviewCandidateInfoListDAL aInterviewCandidateInfoListDal = new InterviewCandidateInfoListDAL();
                DataTable dtcandidate = aInterviewCandidateInfoListDal.TebulationMarksForMailSend(CompanyId.ToString(), JobId.ToString(), mid);
                string[] mainstring = new string[3];
                int count = 0;
                for (int i = 0; i < dtcandidate.Columns.Count; i++)
                {
                    if (i == 0 || i == 1 || i == 2)
                    {
                        mainstring[count] = dtcandidate.Columns[i].ToString();
                        count++;
                    }
                    else
                    {
                        BoundField test = new BoundField();
                        test.DataField = dtcandidate.Columns[i].ToString();
                        test.HeaderText = dtcandidate.Columns[i].ToString();
                        gvGrading.Columns.Add(test);
                    }


                }
                


                gvGrading.DataKeyNames = mainstring;
                gvGrading.DataSource = dtcandidate;
                gvGrading.DataBind();

               
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
        Response.Redirect("/Default.aspx");
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("InterviewBoardSetupList.aspx");
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (txtMailBody.Text != "")
        {


            try
            {

                string mailadd = "";
                for (int i = 0; i < gv_selectedEmp.Rows.Count; i++)
                {

                    Label EmpMail = (Label)gv_selectedEmp.Rows[i].Cells[1].FindControl("txt_OfficialEmail");
                    mailadd += EmpMail.Text.Trim() + ",";

                }

                    System.Threading.Thread.Sleep(100);

                    MailMessage mail = new MailMessage();

                    mail.To.Add(mailadd.Trim().TrimEnd(','));

                    //mail.To.Add
                    mail.From = new MailAddress(Session["EmailID"].ToString());
                    mail.From.User.ToString();

                    mail.Sender = new System.Net.Mail.MailAddress(Session["EmailID"].ToString());
                    mail.Subject = "Interview Candidate Information";

                    MailAddress copy2 = new MailAddress(Session["EmailID"].ToString());
                    mail.Bcc.Add(copy2);



                    mail.Body = "<div style='background-color: #DFF0D8; border-style: solid; border-color: #39B3D7; color: black; padding: 25px; border-radius: 15px 50px 30px 5px;'> <br/>" +

                         WebUtility.HtmlDecode(txtMailBody.Text)
                        +


                    "</div>";
                    mail.IsBodyHtml = true;
                    mail.Priority = System.Net.Mail.MailPriority.High;

                    //Attach file using FileUpload Control and put the file in memory stream


                    StringWriter sw = new StringWriter();
                    StringWriter sw2 = new StringWriter();
                    HtmlTextWriter htw = new HtmlTextWriter(sw);
                    HtmlTextWriter htw2 = new HtmlTextWriter(sw2);

                    loadGridView.AllowPaging = false;

                    gvGrading.AllowPaging = false;





                    using (
                         DataTable dataTable =
                             aInterviewCandidateInfoList.GetInterviewCandidateInfoListForMailSend(" WHERE    JobID=" + Session["JobIDDD"].ToString()))
                    {
                        if (dataTable.Rows.Count > 0)
                        {
                            loadGridView.DataSource = dataTable;
                            loadGridView.DataBind();
                        }
                    }


                    gvGrading.Columns.Clear();
                    InterviewCandidateInfoListDAL aInterviewCandidateInfoListDal = new InterviewCandidateInfoListDAL();
                    DataTable dtcandidate = aInterviewCandidateInfoListDal.TebulationMarksForMailSend(Session["CompanyId"].ToString(), Session["JobIDDD"].ToString(), Convert.ToInt32(hdpk.Value));
                    string[] mainstring = new string[3];
                    int count = 0;
                    for (int k = 0; k < dtcandidate.Columns.Count; k++)
                    {
                        if (k == 0 || k == 1 || k == 2)
                        {
                            mainstring[count] = dtcandidate.Columns[k].ToString();
                            count++;
                        }
                        else
                        {
                            BoundField test = new BoundField();
                            test.DataField = dtcandidate.Columns[k].ToString();
                            test.HeaderText = dtcandidate.Columns[k].ToString();
                            gvGrading.Columns.Add(test);
                        }


                    }



                    gvGrading.DataKeyNames = mainstring;
                    gvGrading.DataSource = dtcandidate;
                    gvGrading.DataBind();


                    loadGridView.HeaderRow.Style.Add("background-color", "#E5EEF1");
                    gvGrading.HeaderRow.Style.Add("background-color", "#E5EEF1");

                    // Set background color of each cell of GridView1 header row


                    loadGridView.RenderControl(htw);
                    gvGrading.RenderControl(htw2);


                    System.Text.Encoding theEncoding = System.Text.Encoding.ASCII;
                    System.Text.Encoding theEncoding2 = System.Text.Encoding.ASCII;
                    byte[] theByteArray = theEncoding.GetBytes(sw.ToString());
                    byte[] theByteArray2 = theEncoding2.GetBytes(sw2.ToString());
                    MemoryStream theMemoryStream = new MemoryStream(theByteArray, false);
                    MemoryStream theMemoryStream2 = new MemoryStream(theByteArray2, false);
                    mail.Attachments.Add(new Attachment(theMemoryStream, "Interview_Candidate_Info_List.xls"));
                    mail.Attachments.Add(new Attachment(theMemoryStream2, "Interview_Candidate_Grading_Info_List.xls"));
                    //Attach file using FileUpload Control and put the file in memory stream
                    if (fu_cv.HasFile)
                    {
                        mail.Attachments.Add(new Attachment(fu_cv.PostedFile.InputStream, fu_cv.FileName));
                    }

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
                    smtp.Credentials = new System.Net.NetworkCredential(Session["EmailID"].ToString(), Session["AppPass"].ToString());

                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Port = 587;
                    smtp.EnableSsl = true;

                    try
                    {
                        smtp.Send(mail);

                    }
                    catch (System.Net.Mail.SmtpException ex)
                    {
                       // showMessageBox("Email has not Sent, Try Once More time");
                    }
                    catch (Exception exe)
                    {
                       // showMessageBox("Email has not Sent, Try Once More time");
                    }


                    System.Threading.Thread.Sleep(100);


                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                  "alert",
                  "alert('Sent Successfully...! ');window.location ='InterviewBoardSetupList.aspx';",
                  true);

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
    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }  
    private void ExportExcelMethod()
    {
        if (loadGridView.Rows.Count > 0)
        {
            string attachment = "attachment; filename=Interview_Candidate_Info_List.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            loadGridView.AllowPaging = false;





          

            using (
                 DataTable dataTable =
                     aInterviewCandidateInfoList.GetInterviewCandidateInfoListForMailSend(" WHERE    JobID=" + Session["JobIDDD"].ToString()))
            {
                if (dataTable.Rows.Count > 0)
                {
                    loadGridView.DataSource = dataTable;
                    loadGridView.DataBind();
                }
            }
            // Create a form to contain the grid  
       
            //frm.Attributes["runat"] = "server";
            //frm.Controls.Add(loadGridView);
            //frm.RenderControl(htw);

            loadGridView.HeaderRow.Style.Add("background-color", "#E5EEF1");

            // Set background color of each cell of GridView1 header row
            foreach (TableCell tableCell in loadGridView.HeaderRow.Cells)
            {
                tableCell.Style["background-color"] = "#E5EEF1";
            }

            // Set background color of each cell of each data row of GridView1
            foreach (GridViewRow gridViewRow in loadGridView.Rows)
            {
                gridViewRow.BackColor = System.Drawing.Color.White;

                foreach (TableCell gridViewRowTableCell in gridViewRow.Cells)
                {
                    gridViewRowTableCell.Style["background-color"] = "#FFFFFF";

                }
            }

            loadGridView.RenderControl(htw);
            string headerTable = @"<span  style='text-align:left'><h3> Company Name: "    +
                                 "</h3>  </span> <span   style='text-align:right'><h4> Print Date: " +
                                 DateTime.Now.ToString("dd/MMMM/yyyy") + "</h4></span>";

            string SubTi = @"<span   style='text-align:center'>
   <h3>Interview Candidate Info List</h3>
   <span style=' font-size:15px;text-align:center'>Job Circulation:  </span></span>";



            HttpContext.Current.Response.Write(headerTable);
            HttpContext.Current.Response.Write(SubTi);


            string style = @"<style> .text { mso-number-format:\@; } </style> ";
            Response.Write(style);
            Response.Write(sw.ToString());
            Response.End();
        }
        else
        {
            showMessageBox("No Data Found!!");
        }
    }

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }

    protected void loadGridView_OnRowCreated(object sender, GridViewRowEventArgs e)
    {
         
    }

    protected void loadGridView_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }
}