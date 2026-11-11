using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Inverview_DAL;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class Inverview_InterviewCandidateInfo : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private InterviewCommonDAL _interviewCommonDAL = new InterviewCommonDAL();
    private DropDownList ddlJobCirculation;
    private int mid = 0;
    private string _userId;
    private DropDownList ddlCompany;
    private TextBox txt_JobCirculation;
    //private TextBox txt_JobTitle;
  
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        ddlJobCirculation = (DropDownList)IVSearchControl.FindControl("ddlJobCirculation") as DropDownList;
        ddlCompany = (DropDownList)IVSearchControl.FindControl("ddlCompany");
        txt_JobCirculation = (TextBox)IVSearchControl.FindControl("txt_JobCirculation");
        //txt_JobTitle = (TextBox)IVSearchControl.FindControl("txt_JobTitle");
     

        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {

            //dateofBirthTextBox.Attributes.Add("readonly", "readonly");
            ButtonVisible();
            Session["cid"] = null;
            LoadInitialDDL();
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();
                if (mid > 0)
                {
                    using (var db = new HRIS_SMCEntities())
                    {
                        HyperLink2.Visible = true;
                        var candidate = (from ca in db.tblInterviewCandidateInfoes where ca.CandidateID == mid select ca).FirstOrDefault();

                        ddlCompany.SelectedValue = candidate.CompanyId.ToString();
                        ddlCompany_SelectedIndexChanged(null, null);

                        var job = (from j in db.tblJobCreations where j.JobID == candidate.JobID select j).FirstOrDefault();
                        if (job != null)
                        {
                            ddlJobCirculation.SelectedValue = candidate.JobID.ToString();
                            txt_JobCirculation.Text = job.Position;
                            //txt_JobTitle.Text = job.Position;
                        }
                        txt_CandidateName.Text = candidate.CandidateName;
                        txt_CandidateAddress.Text = candidate.Address;
                        dateofBirthTextBox.Text = Convert.ToDateTime(candidate.DateOfBirth).ToString("dd/MMM/yyyy");
                        ageTextBox.Text = candidate.Age;
                        txt_CandidatePhone.Text = candidate.PhoneNo;
                        txt_CandidateEmail.Text = candidate.EmailAdress;


                

                        var aDataTableKeyresp = new DataTable();

                        aDataTableKeyresp.Columns.Add("CompanyName");
                        aDataTableKeyresp.Columns.Add("Experience");
                        DataRow dataRow;
                        
                            string[] commaSpilt = candidate.TotalYearsOfExp.Split(',');
                            foreach (string s in commaSpilt)
                            {
                                try
                                {

                                
                                string compane = s.Split('-')[0];
                                string exp = s.Split('-')[1];

                                dataRow = aDataTableKeyresp.NewRow();
                                dataRow["CompanyName"] = compane;
                                dataRow["Experience"] = exp;
                                aDataTableKeyresp.Rows.Add(dataRow);
                                }
                                catch (Exception)
                                {

                                    
                                }

                            }
                            KeyResponGridView.DataSource = aDataTableKeyresp;
                        KeyResponGridView.DataBind();

                            
                        

                      //  string exp = "";
                        //for (int i = 0; i < candidate.TotalYearsOfExp.Length; i++)
                        //{


                        //  //  exp += KeyResponGridView.Rows[i].Cells[0].Text + " - " + KeyResponGridView.Rows[i].Cells[1].Text + " , ";


                        //    txt_CandidateEmail.Text = candidate.TotalYearsOfExp.Split('-')[0];

                        //    //KeyResponGridView.Rows[i].Cells[1].Text = candidate.TotalYearsOfExp.Split(',')[1];
                        //}

                        //txt_CandidateEmail.Text = candidate.TotalYearsOfExp.Split('-')[0];
                      


                        //ddlExpY.SelectedValue = candidate.TotalYearsOfExp.Split('.')[0];//getting the year part
                        //ddlExpM.SelectedValue = candidate.TotalYearsOfExp.Split('.')[1];//getting the month part

                        txt_LastOrganization.Text = candidate.LastOrganization;
                        txt_LastPosition.Text = candidate.LastPosition;
                        txt_LastExam.Text = candidate.LastExam;
                        txt_MaxMajor.Text = candidate.LastMajor;
                        radResult.SelectedValue = candidate.LastResultType;
                        radResult_OnSelectedIndexChanged(null,null);

                        if (radResult.SelectedValue == "Grading")
                        {
                            txt_LastResultGrading.Text = candidate.LastResultCGPA;
                            //txt_LastResultGradingOutOf.Text = candidate.LastResultOutOf;
                        }
                        else
                        {
                            ddlLastResultDivision.SelectedValue  = candidate.LastResultDivision.ToString();
                        }

                        txt_LastPassingYear.Text = candidate.LastPassingYear;
                        txt_CurrentSalary.Text = candidate.CurrentSalary;
                        txt_ExpectedSalary.Text = candidate.ExpectedSalary;

                        HyperLink2.NavigateUrl = "../UploadMeetingDocument/" + candidate.Cv_File;


                    }

                }
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
                btnSaveNew.Visible = true;
                orBTN.Visible = true;
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                editButton.Visible = true;
            }
            else if (Session["Status"].ToString() == "Delete")
            {
                delButton.Visible = true;
            }
            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("InterviewCandidateInfoList.aspx");
        }

    }
    private void LoadInitialDDL()
    {
        //using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        //{
        //    ddlCompany.DataSource = dt;
        //    ddlCompany.DataValueField = "Value";
        //    ddlCompany.DataTextField = "TextField";
        //    ddlCompany.DataBind();
        //}
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
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("InterviewCandidateInfoList.aspx");
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["cid"] = ddlCompany.SelectedValue;
    }

    //protected void txt_JobCirculation_OnTextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

        
    //    if (!string.IsNullOrEmpty(txt_JobCirculation.Text))
    //    {
    //        string job = txt_JobCirculation.Text;
    //        hfJobID.Value = job.Split(':')[0];
    //        txt_JobCirculation.Text = job.Split(':')[1];
    //        txt_JobTitle.Text = job.Split(':')[2];
    //    }
    //    }
    //    catch (Exception ex)
    //    {
    //        txt_JobCirculation.Text = "";

    //    }
    //}

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        try
        {
            Submit();
            ScriptManager.RegisterStartupScript(this, this.GetType(),
              "alert",
              "alert('Operation Successful...');window.location ='InterviewCandidateInfoList.aspx';",
              true);
        }
        catch (Exception)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
             "alert",
             "alert('Operation Faild...');window.location ='InterviewCandidateInfoList.aspx';",
             true);
            //throw;
        }
    }

    private void Submit()
    {
        try
        {
            #region validation

            if (ddlCompany.SelectedIndex < 0)
            {
                AlertMessageBoxShow("Company is required...");
                ddlCompany.Focus();
                return;
            }
            if (ddlJobCirculation.SelectedIndex < 0)
            {
                AlertMessageBoxShow("Job Circulation is required...");
                ddlJobCirculation.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txt_CandidatePhone.Text))
            {
                AlertMessageBoxShow("Candidate Mobile No. is required...");
                txt_CandidatePhone.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txt_CandidateName.Text))
            {
                AlertMessageBoxShow("Candidate Name is required...");
                txt_CandidateName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(dateofBirthTextBox.Text))
            {
                AlertMessageBoxShow("date of Birth is required...");
                dateofBirthTextBox.Focus();
                return;
            }

            #endregion

            mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
            tblInterviewCandidateInfo candidateInfo = null;
            using (var db = new HRIS_SMCEntities())
            {
                if (mid > 0)
                {
                    candidateInfo =
                        (from j in db.tblInterviewCandidateInfoes where j.CandidateID == mid select j).FirstOrDefault();
                    candidateInfo.CompanyId = int.Parse(ddlCompany.SelectedValue);
                    candidateInfo.JobID = int.Parse(ddlJobCirculation.SelectedValue);
                    candidateInfo.CandidateName = txt_CandidateName.Text;
                    candidateInfo.Address = txt_CandidateAddress.Text;
                    candidateInfo.PhoneNo = txt_CandidatePhone.Text;
                    candidateInfo.EmailAdress = txt_CandidateEmail.Text;

                    string exp = "";
                    for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                    {
                        exp += KeyResponGridView.Rows[i].Cells[0].Text.Trim() + "-" +
                               KeyResponGridView.Rows[i].Cells[1].Text.Trim() + ",";
                    }


                    candidateInfo.TotalYearsOfExp = exp.TrimEnd(',');


                    candidateInfo.LastOrganization = txt_LastOrganization.Text;
                    candidateInfo.LastPosition = txt_LastPosition.Text;
                    candidateInfo.LastExam = txt_LastExam.Text;
                    candidateInfo.LastMajor = txt_MaxMajor.Text;
                    candidateInfo.LastResultType = radResult.SelectedValue;
                    if (radResult.SelectedValue == "Grading")
                    {
                        candidateInfo.LastResultCGPA = txt_LastResultGrading.Text;
                        candidateInfo.LastResultOutOf = txt_LastResultGradingOutOf.Text;
                    }
                    else
                    {
                        candidateInfo.LastResultDivision = int.Parse(ddlLastResultDivision.SelectedValue);
                    }

                    candidateInfo.LastPassingYear = txt_LastPassingYear.Text;
                    candidateInfo.CurrentSalary = txt_CurrentSalary.Text;
                    candidateInfo.ExpectedSalary = txt_ExpectedSalary.Text;
                    candidateInfo.IsActive = true;
                    candidateInfo.Updateby = _userId;
                    candidateInfo.UpdateDate = DateTime.Now;
                    candidateInfo.DateOfBirth = Convert.ToDateTime(dateofBirthTextBox.Text);
                    candidateInfo.Age = ageTextBox.Text;
                    candidateInfo.Cv_File = string.IsNullOrEmpty(hfDocFile.Value).ToString();
                    candidateInfo.Cv_FileName = string.IsNullOrEmpty(hfDocFileName.Value).ToString();



                    db.SaveChanges();
                }
                else
                {
                    candidateInfo = new tblInterviewCandidateInfo();

                    candidateInfo.CompanyId = int.Parse(ddlCompany.SelectedValue);
                    candidateInfo.JobID = int.Parse(ddlJobCirculation.SelectedValue);
                    candidateInfo.CandidateName = txt_CandidateName.Text;
                    candidateInfo.Address = txt_CandidateAddress.Text;
                    candidateInfo.PhoneNo = txt_CandidatePhone.Text;
                    candidateInfo.EmailAdress = txt_CandidateEmail.Text;

                    //candidateInfo.TotalYearsOfExp = ddlExpY.SelectedValue + "." + ddlExpM.SelectedValue;

                    string exp = "";
                    for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                    {
                        exp += KeyResponGridView.Rows[i].Cells[0].Text.Trim() + "-" +
                               KeyResponGridView.Rows[i].Cells[1].Text.Trim() + ",";
                    }


                    candidateInfo.TotalYearsOfExp = exp.TrimEnd(',');


                    candidateInfo.LastOrganization = txt_LastOrganization.Text;
                    candidateInfo.LastPosition = txt_LastPosition.Text;
                    candidateInfo.LastExam = txt_LastExam.Text;
                    candidateInfo.LastMajor = txt_MaxMajor.Text;
                    candidateInfo.LastResultType = radResult.SelectedValue;
                    if (radResult.SelectedValue == "Grading")
                    {
                        candidateInfo.LastResultCGPA = txt_LastResultGrading.Text;
                        candidateInfo.LastResultOutOf = txt_LastResultGradingOutOf.Text;
                    }
                    else
                    {
                        candidateInfo.LastResultDivision = int.Parse(ddlLastResultDivision.SelectedValue);
                    }

                    candidateInfo.LastPassingYear = txt_LastPassingYear.Text;
                    candidateInfo.CurrentSalary = txt_CurrentSalary.Text;
                    candidateInfo.ExpectedSalary = txt_ExpectedSalary.Text;
                    candidateInfo.EntryBy = _userId;
                    candidateInfo.EntryDate = DateTime.Now;
                    candidateInfo.IsActive = true;
                    candidateInfo.DateOfBirth = Convert.ToDateTime(dateofBirthTextBox.Text);
                    candidateInfo.Age = ageTextBox.Text;
                    candidateInfo.Cv_File = (hfDocFile.Value).ToString();
                    candidateInfo.Cv_FileName = (hfDocFileName.Value).ToString();




                    db.tblInterviewCandidateInfoes.Add(candidateInfo);
                    db.SaveChanges();
                 //   PrepareFilesDataForSaveDetail(Convert.ToInt32(candidateInfo.CandidateID));
                }
            }
          
        }
        catch (Exception)
        {
            // throw;
        }
    }


    private int PrepareFilesDataForSaveDetail(int styleId)
    {



        CandidateCvUploadDAO aCandidateCvUploadDAO = new CandidateCvUploadDAO();


        int _size = 500000;// equal 500 kb
        if ((fu_cv.PostedFile != null) && (fu_cv.PostedFile.ContentLength > 0) && (fu_cv.PostedFile.ContentLength <= _size))
        {
            //  aInquiryBll.DeleteFilesbyStyleId(styleId.ToString());

            string fileName = string.Empty;
            string _fileExt = System.IO.Path.GetExtension(fu_cv.FileName);
            foreach (HttpPostedFile postedFile in fu_cv.PostedFiles)
            {
                string filename = Path.GetFileName(postedFile.FileName);
                string contentType = postedFile.ContentType;
                using (Stream fs = postedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        if (_fileExt.ToLower() == ".png" || _fileExt.ToLower() == ".gif" || _fileExt.ToLower() == ".jpeg" ||
                            _fileExt.ToLower() == ".jpg" || _fileExt.ToLower() == ".pdf" || _fileExt.ToLower() == ".doc" || _fileExt.ToLower() == ".docx"  || _fileExt.ToLower() == ".zip" || _fileExt.ToLower() == ".rar")
                        {
                            if (fu_cv.PostedFile.ContentLength <= _size)
                            {





                                string AdsFile = "IterViewCV"+Guid.NewGuid().ToString() + Path.GetExtension(fu_cv.FileName);
                                //  fileName = guid.ToString() + imageFileUpload.FileName;
                                fu_cv.SaveAs(Server.MapPath("../UploadImg/") + AdsFile);
                                // EnsureDirectoriesExist();


                                aCandidateCvUploadDAO.CandidateID = styleId;
                                aCandidateCvUploadDAO.Cv_Upload = AdsFile;

                                int id = _commonDataLoad.SaveCandidateCvInfo(aCandidateCvUploadDAO);

                               
                            }
                            else
                            {
                                fu_cv.Focus();
                                ClientScript.RegisterStartupScript(Type.GetType("System.String"), "messagebox",
                                    "<script type=\"text/javascript\">alert('Max file size is 500 KB ');</script>");
                            }
                        }
                        else
                        {
                            fu_cv.Focus();
                            ClientScript.RegisterStartupScript(Type.GetType("System.String"), "messagebox", "<script type=\"text/javascript\">alert('Only GIF or jpeg or jpg or pdf or doc  or zip or rar allowed');</script>");
                        }

                    }
                }

            }
        }


        return aCandidateCvUploadDAO.CandidateID;


    }


    public void AgeNew()
    {
     

        
        try
        {
            string birthdt = Convert.ToDateTime(dateofBirthTextBox.Text.Trim()).ToString("dd/MMM/yyyy");
            DateTime dob = Convert.ToDateTime(birthdt);
            DateTime PresentYear = DateTime.Now;
            TimeSpan ts = PresentYear - dob;
            DateTime age = new DateTime(PresentYear.Subtract(dob).Ticks);

            ageTextBox.Text = (age.Year - 1) + " years ," + age.Month.ToString() + " months ," + age.Day.ToString() + " days ";
        }
        catch (Exception)
        {

            AlertMessageBoxShow("Give A valid Date of birth !!");
            dateofBirthTextBox.Text = string.Empty;
        }
    }


    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("InterviewCandidateInfo.aspx");
    }

    protected void btn_ImageUpload_OnClick(object sender, EventArgs e)
    {
        StartUpLoad();
    }
    private void StartUpLoad()
    {
        //get the file name of the posted image  
        string imgName = fu_Image.FileName;
        //sets the image path  
        string imgPath = "ImageStorage/" + imgName;
        //get the size in bytes that  

        int imgSize = fu_Image.PostedFile.ContentLength;

        //validates the posted file before saving  
        if (fu_Image.PostedFile != null && fu_Image.PostedFile.FileName != "")
        {
            // 10240 KB means 10MB, You can change the value based on your requirement  
            if (fu_Image.PostedFile.ContentLength > 10240)
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('File is too big.')", true);
            }
            else
            {
                //then save it to the Folder  
                fu_Image.SaveAs(Server.MapPath(imgPath));
                Image1.ImageUrl = "~/" + imgPath;
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Alert", "alert('Image saved!')", true);
            }

        }
    } 
    protected void btn_CvUpload_OnClick(object sender, EventArgs e)
    {
       // StartUpLoad();
    }

    protected void radResult_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (radResult.SelectedValue=="Division")
        {
            ddlLastResultDivision.Visible = true;
            txt_LastResultGrading.Visible = false;
            txt_LastResultGradingOutOf.Visible = false;
        }
        else
        {
            ddlLastResultDivision.Visible = false;
            txt_LastResultGrading.Visible = true;
            txt_LastResultGradingOutOf.Visible = true;
        }
    }
    public bool IsValidEmailAddress(string email)
    {
        try
        {
            var emailChecked = new System.Net.Mail.MailAddress(email);
            return true;
        }
        catch
        {
            return false;
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
    protected void txt_CandidateEmail_OnTextChanged(object sender, EventArgs e)
    {
        //var Email = txt_CandidateEmail.Text;
        //if (!IsValidEmailAddress(Email))
        //{
        //    txt_CandidateEmail.Text = String.Empty;
        //    AlertMessageBoxShow("Not a valid email address...!");
        //    return;
        //}
    }

    protected void txt_CandidatePhone_OnTextChanged(object sender, EventArgs e)
    {
        ////check mobile no on db and fetch candidate if mobile exists
        if (!string.IsNullOrEmpty(txt_CandidatePhone.Text) && txt_CandidatePhone.Text.Length>8)
        {
            using (DataTable dt = _interviewCommonDAL.GetCandidateByPhone(txt_CandidatePhone.Text))
            {
                if (dt.Rows.Count>0)
                {
                    AlertMessageBoxShow("Found a previous entry of candidate with this Mobile no. That data will be loaded for your convenience!");
                    txt_CandidateName.Text = dt.Rows[0]["CandidateName"].ToString();
                    txt_CandidateAddress.Text = dt.Rows[0]["Address"].ToString();
                    txt_CandidateEmail.Text = dt.Rows[0]["EmailAdress"].ToString();

                }
            }
        }
    }

    protected void dateofBirthTextBox_OnTextChanged(object sender, EventArgs e)
    {
        AgeNew();
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        try
        {
            #region validation
            if (ddlCompany.SelectedIndex < 0)
            {
                AlertMessageBoxShow("Company required...");
                return;
            }
            if (string.IsNullOrEmpty(txt_JobCirculation.Text))
            {
                AlertMessageBoxShow("Job Circulation required...");
                return;
            }
            #endregion
            mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
            tblInterviewCandidateInfo candidateInfo = null;
            using (var db = new HRIS_SMCEntities())
            {
                if (mid > 0)
                {
                    candidateInfo = (from j in db.tblInterviewCandidateInfoes where j.CandidateID == mid select j).FirstOrDefault();
                    candidateInfo.CompanyId = int.Parse(ddlCompany.SelectedValue);
                    candidateInfo.JobID = int.Parse(ddlJobCirculation.SelectedValue);
                    candidateInfo.CandidateName = txt_CandidateName.Text;
                    candidateInfo.Address = txt_CandidateAddress.Text;
                    candidateInfo.PhoneNo = txt_CandidatePhone.Text;
                    candidateInfo.EmailAdress = txt_CandidateEmail.Text;

                    string exp = "";
                    for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                    {


                        exp += KeyResponGridView.Rows[i].Cells[0].Text.Trim() + "-" + KeyResponGridView.Rows[i].Cells[1].Text.Trim() + ",";

                    }


                    candidateInfo.TotalYearsOfExp = exp.TrimEnd(',');
                    candidateInfo.LastOrganization = txt_LastOrganization.Text;
                    candidateInfo.LastPosition = txt_LastPosition.Text;
                    candidateInfo.LastExam = txt_LastExam.Text;
                    candidateInfo.LastMajor = txt_MaxMajor.Text;
                    candidateInfo.LastResultType = radResult.SelectedValue;
                    if (radResult.SelectedValue == "Grading")
                    {
                        candidateInfo.LastResultCGPA = txt_LastResultGrading.Text;
                        candidateInfo.LastResultOutOf = txt_LastResultGradingOutOf.Text;
                    }
                    else
                    {
                        candidateInfo.LastResultDivision = int.Parse(ddlLastResultDivision.SelectedValue);
                    }

                    candidateInfo.LastPassingYear = txt_LastPassingYear.Text;
                    candidateInfo.CurrentSalary = txt_CurrentSalary.Text;
                    candidateInfo.ExpectedSalary = txt_ExpectedSalary.Text;
                    candidateInfo.IsActive = true;
                    candidateInfo.Updateby = _userId;
                    candidateInfo.UpdateDate = DateTime.Now;
                    candidateInfo.DateOfBirth = Convert.ToDateTime(dateofBirthTextBox.Text);
                    candidateInfo.Age = ageTextBox.Text;
                    db.SaveChanges();
                }
                else
                {
                    candidateInfo = new tblInterviewCandidateInfo();

                    candidateInfo.CompanyId = int.Parse(ddlCompany.SelectedValue);
                    candidateInfo.JobID = int.Parse(ddlJobCirculation.SelectedValue);
                    candidateInfo.CandidateName = txt_CandidateName.Text;
                    candidateInfo.Address = txt_CandidateAddress.Text;
                    candidateInfo.PhoneNo = txt_CandidatePhone.Text;
                    candidateInfo.EmailAdress = txt_CandidateEmail.Text;

                    string exp = "";
                    for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                    {


                        exp += KeyResponGridView.Rows[i].Cells[0].Text.Trim() + "-" + KeyResponGridView.Rows[i].Cells[1].Text.Trim() + ",";



                    }


                    candidateInfo.TotalYearsOfExp = exp.TrimEnd(',');
                    candidateInfo.LastOrganization = txt_LastOrganization.Text;
                    candidateInfo.LastPosition = txt_LastPosition.Text;
                    candidateInfo.LastExam = txt_LastExam.Text;
                    candidateInfo.LastMajor = txt_MaxMajor.Text;
                    candidateInfo.LastResultType = radResult.SelectedValue;
                    if (radResult.SelectedValue == "Grading")
                    {
                        candidateInfo.LastResultCGPA = txt_LastResultGrading.Text;
                        candidateInfo.LastResultOutOf = txt_LastResultGradingOutOf.Text;
                    }
                    else
                    {
                        candidateInfo.LastResultDivision = int.Parse(ddlLastResultDivision.SelectedValue);
                    }

                    candidateInfo.LastPassingYear = txt_LastPassingYear.Text;
                    candidateInfo.CurrentSalary = txt_CurrentSalary.Text;
                    candidateInfo.ExpectedSalary = txt_ExpectedSalary.Text;
                    candidateInfo.EntryBy = _userId;
                    candidateInfo.EntryDate = DateTime.Now;
                    candidateInfo.IsActive = true;
                    candidateInfo.DateOfBirth = Convert.ToDateTime(dateofBirthTextBox.Text);
                    candidateInfo.Age = ageTextBox.Text;
                    db.tblInterviewCandidateInfoes.Add(candidateInfo);
                    db.SaveChanges();


                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successful...');window.location ='InterviewCandidateInfoList.aspx';",
                true);
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {
       
                if (mid != null)
                    using (var db = new HRIS_SMC_DBEntities())
                    {
                        db.Database.ExecuteSqlCommand(@"INSERT INTO DEltblInterviewCandidateInfo ([CandidateID]
      ,[CompanyId]
      ,[JobID]
      ,[CandidateName]
      ,[Address]
      ,[PhoneNo]
      ,[EmailAdress]
      ,[TotalYearsOfExp]
      ,[LastOrganization]
      ,[LastPosition]
      ,[LastExam]
      ,[LastMajor]
      ,[LastPassingYear]
      ,[LastResultType]
      ,[LastResultDivision]
      ,[LastResultCGPA]
      ,[LastResultOutOf]
      ,[CvUploadID]
      ,[PhotoUploadID]
      ,[CurrentSalary]
      ,[ExpectedSalary]
      ,[Remarks]
      ,[ApprovalStatus]
      ,[EntryBy]
      ,[EntryDate]
      ,[Updateby]
      ,[UpdateDate]
      ,[VerifiedBy]
      ,[VerifiedDate]
      ,[ApproveBy]
      ,[ApproveDate]
      ,[InternalNote]
      ,[IsActive]
      ,[DateOfBirth]
      ,[Age])
SELECT [CandidateID]
      ,[CompanyId]
      ,[JobID]
      ,[CandidateName]
      ,[Address]
      ,[PhoneNo]
      ,[EmailAdress]
      ,[TotalYearsOfExp]
      ,[LastOrganization]
      ,[LastPosition]
      ,[LastExam]
      ,[LastMajor]
      ,[LastPassingYear]
      ,[LastResultType]
      ,[LastResultDivision]
      ,[LastResultCGPA]
      ,[LastResultOutOf]
      ,[CvUploadID]
      ,[PhotoUploadID]
      ,[CurrentSalary]
      ,[ExpectedSalary]
      ,[Remarks]
      ,[ApprovalStatus]
      ,[EntryBy]
      ,[EntryDate]
      ,[Updateby]
      ,[UpdateDate]
      ,[VerifiedBy]
      ,[VerifiedDate]
      ,[ApproveBy]
      ,[ApproveDate]
      ,[InternalNote]
      ,[IsActive]
      ,[DateOfBirth]
      ,[Age] FROM tblInterviewCandidateInfo
  WHERE CandidateID={0}", Convert.ToInt32(hdpk.Value));
                    }
        if (_commonDataLoad.DeleteInterviewCandidateInfoById(hdpk.Value))
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                            "alert",
                            "alert('Operation Successful...');window.location ='InterviewCandidateInfoList.aspx';",
                            true);
                    }
                    else
                    {
                        aShowMessage.ShowMessageBox("Operation Faild!!!", this);
                        
                    }


           
        
        
    }

    protected void editImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;


        string CompanyName = KeyResponGridView.Rows[rowindex].Cells[0].Text;
        string Experience = KeyResponGridView.Rows[rowindex].Cells[1].Text;
        txtCompany.Text = CompanyName;
        txtExperience.Text = Experience;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("CompanyName");
        aDataTable.Columns.Add("Experience");
        DataRow dataRow;

        if (KeyResponGridView.Rows.Count > 0)
        {
            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["CompanyName"] = KeyResponGridView.Rows[i].Cells[0].Text;
                    dataRow["Experience"] = KeyResponGridView.Rows[i].Cells[1].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }

        KeyResponGridView.DataSource = aDataTable;
        KeyResponGridView.DataBind();

    }

    protected void deleteImageButton_OnClick(object sender, ImageClickEventArgs e)
    {

        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("CompanyName");
        aDataTable.Columns.Add("Experience");

        DataRow dataRow;

        if (KeyResponGridView.Rows.Count > 0)
        {
            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["CompanyName"] = KeyResponGridView.Rows[i].Cells[0].Text;
                    dataRow["Experience"] = KeyResponGridView.Rows[i].Cells[1].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }


        KeyResponGridView.DataSource = aDataTable;
        KeyResponGridView.DataBind();
    }

    protected void textButton_OnClick(object sender, EventArgs e)
    {

        if (CheckJDInGrid())
        {


            var aDataTable = new DataTable();

            aDataTable.Columns.Add("CompanyName");
            aDataTable.Columns.Add("Experience");
            DataRow dataRow;

            if (txtCompany.Text != "")
            {
                string CompanyName = txtCompany.Text.Trim();
                string Experience = txtExperience.Text.Trim();

                if (KeyResponGridView.Rows.Count == 0)
                {
                    dataRow = aDataTable.NewRow();

                    dataRow = aDataTable.NewRow();
                    dataRow["CompanyName"] = CompanyName;
                    dataRow["Experience"] = Experience;

                    aDataTable.Rows.Add(dataRow);

                    KeyResponGridView.DataSource = aDataTable;
                    KeyResponGridView.DataBind();
                    txtCompany.Text = string.Empty;
                    txtExperience.Text = string.Empty;
                }

                else
                {
                    for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                    {
                        dataRow = aDataTable.NewRow();
                        dataRow["CompanyName"] = KeyResponGridView.Rows[i].Cells[0].Text;
                        dataRow["Experience"] = KeyResponGridView.Rows[i].Cells[1].Text;
                        aDataTable.Rows.Add(dataRow);
                    }

                    dataRow = aDataTable.NewRow();
                    dataRow["CompanyName"] = CompanyName;
                    dataRow["Experience"] = Experience;

                    aDataTable.Rows.Add(dataRow);

                    KeyResponGridView.DataSource = aDataTable;
                    KeyResponGridView.DataBind();
                    txtCompany.Text = string.Empty;
                    txtExperience.Text = string.Empty;
                }
            }
        }
    }

    public bool CheckJDInGrid()
    {

         

        if (txtCompany.Text.Trim()=="")
        {
            ShowMessageBox("Please Enter Company Name!!!");
            txtCompany.Focus();            return false;
        }

        if (txtExperience.Text.Trim() == "")
        {
            ShowMessageBox("Please Enter Experience!!!");
            txtExperience.Focus();
            return false;
        }


        for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
        {
            if (KeyResponGridView.Rows[i].Cells[0].Text.Trim() == txtCompany.Text.Trim())
            {
                ShowMessageBox("JD Already Exist");
                return false;
            }
        }
        return true;
    }


       public bool CheckDegreeGrid()
    {

         

        if (txtDegreeName.Text.Trim()=="" && txtUniversity.Text.Trim()=="" && txtResult.Text.Trim()=="")
        {
            ShowMessageBox("Please Enter At Least One Data!!!");
            txtCompany.Focus();         
            return false;
        }

        //if (txtExperience.Text.Trim() == "")
        //{
        //    ShowMessageBox("Please Enter Experience!!!");
        //    txtExperience.Focus();
        //    return false;
        //}


        //for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
        //{
        //    if (KeyResponGridView.Rows[i].Cells[0].Text.Trim() == txtCompany.Text.Trim())
        //    {
        //        ShowMessageBox("JD Already Exist");
        //        return false;
        //    }
        //}
        return true;
    }

    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void btnSaveNew_OnClick(object sender, EventArgs e)
    {
        try
        {
            Submit();
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Operation Successful...');",
                   true);
            clearDataField();
        }
        catch (Exception)
        {
            
            //throw;
        }
    }

    private void clearDataField()
    {
        txt_CandidatePhone.Text = string.Empty;
        txt_CandidateName.Text = string.Empty;

            dateofBirthTextBox.Text = string.Empty;
            ageTextBox.Text = string.Empty;
                txt_CandidateEmail.Text = string.Empty;
                txt_LastOrganization.Text = string.Empty;
                    txt_LastPosition.Text = string.Empty;
                    txt_LastExam.Text = string.Empty;
                        txt_MaxMajor.Text = string.Empty;
                        txt_LastPassingYear.Text = string.Empty;
                            txt_CurrentSalary.Text = string.Empty;
                            txt_ExpectedSalary.Text = string.Empty;
        KeyResponGridView.DataSource = null;
        KeyResponGridView.DataBind();
        txtCompany.Text = string.Empty;
            txtExperience.Text = string.Empty;
            txt_LastResultGrading.Text = string.Empty;
        
        
    }

    protected void lb_Download_OnClick(object sender, EventArgs e)
    {

        if (mid > 0)
        {
            using (var db = new HRIS_SMCEntities())
            {
                var candidate =
                    (from ca in db.tblInterviewCandidateInfoes where ca.CandidateID == mid select ca)
                        .FirstOrDefault();

                
            }
        }
        else
        {
            ShowMessageBox("No Document Found !!!");
        }
        //if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
        //{
        //    int idd = Convert.ToInt32(Request.QueryString["mid"]);
        //    using (var db = new HRIS_SMC_DBEntities())
        //    {
              

        //        var ci = db.tblCandidateCvUploads.FirstOrDefault(ici => ici.CandidateID == idd);
        //        if (ci!=null)
        //        {
        //            Session["UpFiless"] = "";
        //            Session["UpFiless"] = ci.Cv_Upload;

        //            Response.Clear();
        //            Response.ContentType = "application/octet-stream";
        //            Response.AppendHeader("Content-Disposition", "filename=" + Session["UpFiless"]);
        //            Response.TransmitFile(Server.MapPath("../UploadImg/" + Session["UpFiless"]));
        //            Response.End();
        //        }
        //        else
        //        {
        //            ShowMessageBox("No Document Found !!!"); 
        //        }

             
        //    }
        //}
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void lblDegree_OnClick(object sender, EventArgs e)
    {
        if (CheckDegreeGrid())
        {


            var aDataTable = new DataTable();

            aDataTable.Columns.Add("DegreeName");
            aDataTable.Columns.Add("University");
            aDataTable.Columns.Add("Result");
         
            DataRow dataRow;

            
                string DegreeName = txtDegreeName.Text.Trim();
                string University = txtUniversity.Text.Trim();
                string  Result = txtResult.Text.Trim();

                if (gv_Degree.Rows.Count == 0)
                {
                    dataRow = aDataTable.NewRow();

                    dataRow = aDataTable.NewRow();
                    dataRow["DegreeName"] = DegreeName;
                    dataRow["University"] = University;
                    dataRow["Result"] = Result;

                    aDataTable.Rows.Add(dataRow);

                    gv_Degree.DataSource = aDataTable;
                    gv_Degree.DataBind();
                    txtDegreeName.Text = string.Empty;
                    txtUniversity.Text = string.Empty;
                    txtResult.Text = string.Empty;
                }

                else
                {
                    for (int i = 0; i < gv_Degree.Rows.Count; i++)
                    {
                        dataRow = aDataTable.NewRow();
                        dataRow["DegreeName"] = gv_Degree.Rows[i].Cells[0].Text;
                        dataRow["University"] = gv_Degree.Rows[i].Cells[1].Text;
                        dataRow["Result"] = gv_Degree.Rows[i].Cells[3].Text;
                        aDataTable.Rows.Add(dataRow);
                    }

                    dataRow = aDataTable.NewRow();
                    dataRow["DegreeName"] = DegreeName;
                    dataRow["University"] = University;
                    dataRow["Result"] = Result;

                    aDataTable.Rows.Add(dataRow);

                    gv_Degree.DataSource = aDataTable;
                    gv_Degree.DataBind();
                    txtDegreeName.Text = string.Empty;
                    txtUniversity.Text = string.Empty;
                    txtResult.Text = string.Empty;
                }
            
        }
     
    }

    protected void addegv_Degree_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;


        string DegreeName = gv_Degree.Rows[rowindex].Cells[0].Text;
        string University = gv_Degree.Rows[rowindex].Cells[1].Text;
        string Result = gv_Degree.Rows[rowindex].Cells[2].Text;
        txtDegreeName.Text = DegreeName;
        txtUniversity.Text = University;
        txtResult.Text = Result;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("DegreeName");
        aDataTable.Columns.Add("University");
        aDataTable.Columns.Add("Result");
        DataRow dataRow;

        if (gv_Degree.Rows.Count > 0)
        {
            for (int i = 0; i < gv_Degree.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["DegreeName"] = gv_Degree.Rows[i].Cells[0].Text;
                    dataRow["University"] = gv_Degree.Rows[i].Cells[1].Text;
                    dataRow["Result"] = gv_Degree.Rows[i].Cells[2].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }

        gv_Degree.DataSource = aDataTable;
        gv_Degree.DataBind();

         
    }
}