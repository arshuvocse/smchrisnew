using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.Inverview_DAL;
using DAL.MPBudget;
using DAL.RecruitmentApp_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;

public partial class Inverview_InterviewCandidateGrading : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private InterviewCommonDAL _interviewCommonDAL = new InterviewCommonDAL();
    private static string _userId;
    private DropDownList ddlCompany;
    private TextBox txt_JobCirculation;
    //private TextBox txt_JobTitle;
    private HiddenField hfJobID;
    RecruitmentAppDAL appDal=new RecruitmentAppDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        ddlCompany = (DropDownList)IVSearchControl.FindControl("ddlCompany");
        txt_JobCirculation = (TextBox)IVSearchControl.FindControl("txt_JobCirculation");
        //txt_JobTitle = (TextBox)IVSearchControl.FindControl("txt_JobTitle");
        hfJobID = (HiddenField)IVSearchControl.FindControl("hfJobID");
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {
            //LoadInitialDDL();
            LoadData();
            LoadcandidateInfo();
            RadioTextValue();
        }
    }

    public void LoadcandidateInfo()
    {
        RecruitmentAppDAL appDal=new RecruitmentAppDAL();
        DataTable dtdata = appDal.GetCandidateInfo(Session["JobId"].ToString());
        GridView1.DataSource = dtdata;
        GridView1.DataBind();
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
    //protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Session["cid"] = ddlCompany.SelectedValue;
    //}
    [WebMethod(EnableSession = true)]
    public static List<InterviewCandidateViewModel> LoadInterviewCandidateGrading(string CompanyId, string JobID)
    {
        InterviewCommonDAL _interviewCommonDAL = new InterviewCommonDAL();
        return _interviewCommonDAL.LoadInterviewCandidateGrading(CompanyId, JobID);
    }
    //protected void txt_JobCirculation_OnTextChanged(object sender, EventArgs e)
    //{
    //    var scid = HttpContext.Current.Session["cid"].ToString();

    //    using (var db = new HRIS_SMCEntities())
    //    {
    //        tblJobCreation job = (from j in db.tblJobCreations where j.JobCode.Equals(txt_JobCirculation.Text) select j).FirstOrDefault();
    //        if (job != null)
    //        {
    //            txt_JobTitle.Text = job.Position;
    //            hfJobID.Value = job.JobID.ToString();
    //        }

    //    }

    //}

    [WebMethod(EnableSession = true)]
    public static int RIsRecommended(InterviewCandidateViewModel CandidateGrading)
    {
        int InterviewCandidateGradingId = 0;
        try
        {
            using (var db = new HRIS_SMCEntities())
            {
                tblInterviewCandidateGrading grading = null;
                if (CandidateGrading.InterviewCandidateGradingId > 0)////Edit Mode
                {
                    grading = (from j in db.tblInterviewCandidateGradings 
                               where j.InterviewCandidateGradingId == CandidateGrading.InterviewCandidateGradingId select j).FirstOrDefault();

                    if (grading!=null)
                    {

                        grading.JobID = CandidateGrading.JobID;
                        grading.CandidateID = CandidateGrading.CandidateID;
                        grading.IsRecommended = CandidateGrading.IsRecommended;

                        grading.Attitude = CandidateGrading.Attitude;
                        grading.Language = CandidateGrading.Language;
                        grading.TechnicalSkill = CandidateGrading.TechnicalSkill;
                        grading.IQ = CandidateGrading.IQ;
                        grading.GeneralKnowledge = CandidateGrading.GeneralKnowledge;
                        grading.Others = CandidateGrading.Others;
                        grading.TimeSence = CandidateGrading.TimeSence;
                        grading.TotalMarks = CandidateGrading.TotalMarks;
                        grading.Grade = CandidateGrading.LetterGrade;
                        grading.IsActive = true;

                        grading.UpdateBy = _userId;
                        grading.UpdateDate = DateTime.Now;

                        db.SaveChanges();
                        InterviewCandidateGradingId = grading.InterviewCandidateGradingId;
                    }
                    else
                    {////New Mode
                        grading = new tblInterviewCandidateGrading();
                        grading.JobID = CandidateGrading.JobID;
                        grading.CandidateID = CandidateGrading.CandidateID;
                        grading.IsRecommended = CandidateGrading.IsRecommended;

                        grading.Attitude = CandidateGrading.Attitude;
                        grading.Language = CandidateGrading.Language;
                        grading.TechnicalSkill = CandidateGrading.TechnicalSkill;
                        grading.IQ = CandidateGrading.IQ;
                        grading.GeneralKnowledge = CandidateGrading.GeneralKnowledge;
                        grading.Others = CandidateGrading.Others;
                        grading.TimeSence = CandidateGrading.TimeSence;
                        grading.TotalMarks = CandidateGrading.TotalMarks;
                        grading.Grade = CandidateGrading.LetterGrade;
                        grading.IsActive = true;

                        grading.EntryBy = _userId;
                        grading.EntryDate = DateTime.Now;
                        db.tblInterviewCandidateGradings.Add(grading);
                        db.SaveChanges();
                        InterviewCandidateGradingId = grading.InterviewCandidateGradingId;
                    }

                }
                else
                {////New Mode
                    grading = new tblInterviewCandidateGrading();
                    grading.JobID = CandidateGrading.JobID;
                    grading.CandidateID = CandidateGrading.CandidateID;
                    grading.IsRecommended = CandidateGrading.IsRecommended;

                    grading.Attitude = CandidateGrading.Attitude;
                    grading.Language = CandidateGrading.Language;
                    grading.TechnicalSkill = CandidateGrading.TechnicalSkill;
                    grading.IQ = CandidateGrading.IQ;
                    grading.GeneralKnowledge = CandidateGrading.GeneralKnowledge;
                    grading.Others = CandidateGrading.Others;
                    grading.TimeSence = CandidateGrading.TimeSence;
                    grading.TotalMarks = CandidateGrading.TotalMarks;
                    grading.Grade = CandidateGrading.LetterGrade;
                    grading.IsActive = true;

                    grading.EntryBy = _userId;
                    grading.EntryDate = DateTime.Now;
                    db.tblInterviewCandidateGradings.Add(grading);
                    db.SaveChanges();
                    InterviewCandidateGradingId = grading.InterviewCandidateGradingId;
                }
            }
        }
        catch (Exception ex)
        {
            InterviewCandidateGradingId=0;
        }

        return InterviewCandidateGradingId;
    }
    protected void ddlstatuys_SelectedIndexChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            DropDownList dd = (DropDownList)loadGridView.Rows[i].FindControl("ddlstatus");
        }
    }

    public void LoadData()
    {
        //Session["JobIDDD"] = "";
        //Session["CompanyId"] = "";
        //Session["CompanyId"] = ddlCompany.SelectedValue;
        //Session["JobIDDD"] = hfJobID.Value.ToString();
        loadGridView.Columns.Clear();
        InterviewCandidateInfoListDAL aInterviewCandidateInfoListDal = new InterviewCandidateInfoListDAL();
        DataTable dtcandidate = aInterviewCandidateInfoListDal.TebulationMarks(Session["CompanyId"].ToString(), Session["JobId"].ToString());
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
                loadGridView.Columns.Add(test);
            }


        }



        using (DataTable dtg = _interviewCommonDAL.GetIVBoardSetupSendMailByMIdByJobID(Convert.ToInt32(Session["JobId"].ToString())))
        {
            if (dtg.Rows.Count > 0)
            {

                gv_selectedEmp.DataSource = dtg;
                gv_selectedEmp.DataBind();
            }
        }
        //ITemplate aTemplate;
        //aTemplate.InstantiateIn();
        //DropDownList ddlstatus = new DropDownList();
        //ddlstatus.ID = "ddlstatus";
        //ddlstatus.Items.Add("Selected");
        //ddlstatus.Items.Add("Waiting");
        //ddlstatus.Items.Add("Reject");
        //TemplateField tfield = new TemplateField();
        //tfield.HeaderText = "Status";
        ////tfield.ItemTemplate
        //loadGridView.Columns.Add(tfield);



        //TemplateField ss = new TemplateField();
        //tfield.HeaderText = "Status11";
        //loadGridView.Columns.Add(ss);





        loadGridView.DataKeyNames = mainstring;
        loadGridView.DataSource = dtcandidate;
        loadGridView.DataBind();
        //  AddDropDownControl();



        string jobid = hfJobID.Value;
        LoadGetInterviewCandidateInfoList(jobid);
    }
    protected void btn_LoadList_OnClick(object sender, EventArgs e)
    {



        Session["JobIDDD"] = "";
        Session["CompanyId"] = "";
        Session["CompanyId"] = ddlCompany.SelectedValue;
        Session["JobIDDD"] = hfJobID.Value.ToString();
        loadGridView.Columns.Clear();
        InterviewCandidateInfoListDAL aInterviewCandidateInfoListDal=new InterviewCandidateInfoListDAL();
        DataTable dtcandidate = aInterviewCandidateInfoListDal.TebulationMarks(ddlCompany.SelectedValue, hfJobID.Value);
        string[] mainstring=new string[3];
        int count= 0;
        for (int i = 0; i < dtcandidate.Columns.Count; i++)
        {
            if (i==0 || i==1 || i==2)
            {
                mainstring[count] = dtcandidate.Columns[i].ToString();
                count++;
            }
            else
            {
                BoundField test = new BoundField();
                test.DataField = dtcandidate.Columns[i].ToString();
                test.HeaderText = dtcandidate.Columns[i].ToString();
                loadGridView.Columns.Add(test);    
            }
            
            
        }



        using (DataTable dtg = _interviewCommonDAL.GetIVBoardSetupSendMailByMIdByJobID(Convert.ToInt32(hfJobID.Value)))
        {
            if (dtg.Rows.Count > 0)
            {
 
                gv_selectedEmp.DataSource = dtg;
                gv_selectedEmp.DataBind();
            }
        }
        //ITemplate aTemplate;
        //aTemplate.InstantiateIn();
        //DropDownList ddlstatus = new DropDownList();
        //ddlstatus.ID = "ddlstatus";
        //ddlstatus.Items.Add("Selected");
        //ddlstatus.Items.Add("Waiting");
        //ddlstatus.Items.Add("Reject");
        //TemplateField tfield = new TemplateField();
        //tfield.HeaderText = "Status";
        ////tfield.ItemTemplate
        //loadGridView.Columns.Add(tfield);
        


        //TemplateField ss = new TemplateField();
        //tfield.HeaderText = "Status11";
        //loadGridView.Columns.Add(ss);


         

        
        loadGridView.DataKeyNames = mainstring;
        loadGridView.DataSource = dtcandidate;
        loadGridView.DataBind();
      //  AddDropDownControl();



        string jobid = hfJobID.Value;
        LoadGetInterviewCandidateInfoList(jobid);

    }

    private InterviewCandidateInfoListDAL aInterviewCandidateInfoList = new InterviewCandidateInfoListDAL();
    private void LoadGetInterviewCandidateInfoList(string jobid)
    {
        //string jobfilter = string.Empty;
        //if (!string.IsNullOrEmpty(jobid))
        //{
        //    jobfilter = " AND JobID=" + jobid;
        //}
       
        {
            using (
                DataTable dataTable =
                    aInterviewCandidateInfoList.GetInterviewCandidateInfoList(
                        " WHERE tblInterviewCandidateInfo.JobID is not null " + GetPaeraMeter()))
            {
                if (dataTable.Rows.Count > 0)
                {
                    GridView1.DataSource = dataTable;
                    GridView1.DataBind();
                }
            }
        }

    }
    private string GetPaeraMeter()
    {
        string param = " ";
        if (ddlCompany.SelectedValue != "")
        {
            param = param + " and   com.CompanyId =  '" + ddlCompany.SelectedValue + "' ";
        }

        if (hfJobID.Value != "")
        {
            param = param + " AND  JobID=  '" + hfJobID.Value + "' ";
        }

        return param;
    }
 
    public void AddDropDownControl()
    {
        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            //int rowno = e.Row.RowIndex;
            DropDownList ddlstatus = new DropDownList();
            ddlstatus.ID = "ddlstatus";
            ddlstatus.Items.Add("Selected");
            ddlstatus.Items.Add("Waiting");
            ddlstatus.Items.Add("Reject");
            ddlstatus.SelectedIndexChanged +=new EventHandler(ddlstatuys_SelectedIndexChanged);
            //ddlstatus.AutoPostBack = true;

            //string status = (e.Row.DataItem as DataRowView).Row["Status"].ToString();
            //ddlstatus.Items.FindByText(status).Selected = true;


            //e.Row.Cells[loadGridView.Columns.Count - 1].Controls.Add(ddlstatus);
            loadGridView.Rows[i].Cells[loadGridView.Columns.Count - 1].Controls.Add(ddlstatus);
        }
    }
    protected void loadGridView_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //int rowno = e.Row.RowIndex;
            //DropDownList ddlstatus = new DropDownList();
            //ddlstatus.ID = "ddlstatus";
            //ddlstatus.Items.Add("Selected");
            //ddlstatus.Items.Add("Waiting");
            //ddlstatus.Items.Add("Reject");
            ////string status = (e.Row.DataItem as DataRowView).Row["Status"].ToString();
            ////ddlstatus.Items.FindByText(status).Selected = true;
            
            
            ////e.Row.Cells[loadGridView.Columns.Count - 1].Controls.Add(ddlstatus);
            //loadGridView.Rows[rowno].Cells[loadGridView.Columns.Count - 1].Controls.Add(ddlstatus);
            ////e.Row.Cells[loadGridView.Columns.Count - 1].Controls[0].ID = "ddlstatus";

        }

        //using (var db = new HRIS_SMC_DBEntities())
        //{
        //    GridViewRow row = e.Row;
        //    int rowind = row.RowIndex;
        //    if (rowind != -1)
        //    {
        //        //check if is in edit mode
        //        // if ((e.Row.RowState & DataControlRowState.Edit) > 0)
        //        {
        //          //  DropDownList ddlstatus = (DropDownList) e.Row.FindControl("ddlstatus");
        //            //Bind subcategories data to dropdownlist

        //            var dt = db.tblCandidateSelecTionStatus.ToList();
        //            //ddlstatus.DataTextField = "Name";
        //            //ddlstatus.DataValueField = "CandidateSelecTionStatusId";
        //            //ddlstatus.DataSource = dt;
        //            //ddlstatus.DataBind();
        //            //DataRowView dr = e.Row.DataItem as DataRowView;
        //            //ddlstatus.SelectedValue = null;

        //            DropDownList dd = e.Row.Cells[0].FindControl("ddlstatus") as DropDownList;
        //            // bind the dropdownlist
        //            dd.DataSource = dt;

        //            dd.DataTextField = "Name";
        //            dd.DataValueField = "CandidateSelecTionStatusId";
        //            dd.DataBind();
        //        }
        //    }
        //}

        //GridViewRow row = e.Row;
        //int rowind = row.RowIndex;
        //if (rowind != -1)
        //{


            
        //    //e.Row.Cells[loadGridView.Columns.Count - 1].Controls.Add(ddlstatus);

        //    //     DropDownList DDL1 = (DropDownList)loadGridView.Rows[0].FindControl("ddlstatus");
        //    //   var a= DDL1.SelectedItem.Text;
        //}
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        using (var db = new HRIS_SMC_DBEntities())
        {
            db.Database.ExecuteSqlCommand("Delete FROM dbo.tblInterViewCandidateSelection WHERE JobID={0}",
                                               Convert.ToInt32(hfJobID.Value));
              tblInterViewCandidateSelection Bat = null;
              Bat = new tblInterViewCandidateSelection();
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {



                DropDownList ddlstatus = GridView1.Rows[i].FindControl("ddlstatus") as DropDownList;
                HiddenField CandidateID = GridView1.Rows[i].FindControl("CandidateID") as HiddenField;
                HiddenField JobID = GridView1.Rows[i].FindControl("JobID") as HiddenField;
               
               if (ddlstatus.SelectedValue != "0")
                {
                    Bat.CandidateID = Convert.ToInt32(CandidateID.Value);
                    Bat.JobID = Convert.ToInt32(JobID.Value);
                    Bat.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                    Bat.CandidateStatus = ddlstatus.SelectedValue;

                   


                    db.tblInterViewCandidateSelections.Add(Bat);
                    db.SaveChanges();
                }
               

            }
            RecruitmentSave(Convert.ToInt32(hfJobID.Value));
            ScriptManager.RegisterStartupScript(this, this.GetType(),
          "alert",
          "alert('Operation Successful...! ');window.location ='InterviewCandidateGrading.aspx';",
          true);
        }
    }

    public void RecruitmentSave(int jobId)
    {
        RecruitmentApprovalDAO approvalDao = new RecruitmentApprovalDAO()
        {
            JobId = jobId,
            ActionStatus = "Drafted",
            ActionStatus2 = "Drafted",
            EntryBy = Session["UserId"].ToString(),
            EntryDate = DateTime.Now,
            ApproveBy = Session["UserId"].ToString(),
            ApproveDate = DateTime.Now,
        };
        RecruitmentAppDAL appDal=new RecruitmentAppDAL();
        int id = appDal.SaveRecruitmentApp(approvalDao);
        SaveAppLog(id);
    }

    public void SaveAppLog(int id)
    {
        RecruitmentAppDAL appDal = new RecruitmentAppDAL();
        RecruitmentApprovalDAO aMaster = new RecruitmentApprovalDAO();
        aMaster.RecruitmentId
            = Convert.ToInt32(id);
        aMaster.ActionStatus = "Verified";
        bool status = appDal.UpdateContractural(aMaster.ActionStatus, aMaster.RecruitmentId);


        int commentid = appDal.SaveComment("0",
            Session["EmpInfoId"].ToString(), "");


        RecruitmentApprovalAppLogDAO appLogDao = new RecruitmentApprovalAppLogDAO();

        appLogDao.ActionStatus = "Drafted";
        appLogDao.ApproveDate = DateTime.Now;
        appLogDao.ApproveBy = Session["UserId"].ToString();
        appLogDao.PreEmpInfoId = Convert.ToInt32(0);
        appLogDao.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
        appLogDao.RecruitmentId = id;
        appLogDao.Comments = "";
        appLogDao.CommentsId = commentid;
        int idd = appDal.SavAppLog(appLogDao);


        DataTable dtempdata =
            appDal.GetEmpInfo(" WHERE EmpInfoId='" +
                                                Session["EmpInfoid"].ToString() + "'");
        RecruitmentApprovalAppLogDAO appLogDaoa = new RecruitmentApprovalAppLogDAO();
        {
            appLogDaoa.ActionStatus = "Verified";
            appLogDaoa.ApproveDate = DateTime.Now;
            appLogDaoa.ApproveBy = Session["UserId"].ToString();
            appLogDaoa.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
            appLogDaoa.ForEmpInfoId =
                Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
            appLogDaoa.RecruitmentId = aMaster.RecruitmentId;
            appLogDaoa.Comments = "";
            appLogDaoa.CommentsId = commentid;

        }
        ;
        int ida = appDal.SavAppLog(appLogDaoa);
        appDal.UpdateJobReqStatus2(aMaster);
    }
    private void RadioTextValue()
    {
        //string filepath = Path.GetDirectoryName(Request.Path);
        //filepath = filepath.TrimStart('\\');
        //filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
        DataTable dtdata = null;
        string filepath = "";
        if (Session["AppPage"] != null)
        {
            filepath = Session["AppPage"].ToString();
        }
        if (actionstatusHiddenField.Value == "Approved")
        {
            dtdata = appDal.GetHRAdminEmployeeAppId(" WHERE URL='" + filepath + "' AND Serial IN (SELECT MAX(Serial)AS SL FROM dbo.tblEmployeeApprovalByOpearationDetail" +
                                                                    " LEFT JOIN dbo.tblMainMenu ON dbo.tblEmployeeApprovalByOpearationDetail.Operation=dbo.tblMainMenu.MainMenuId WHERE URL='" + filepath + "'  ) AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "' ORDER BY Serial ASC ");
        }
        else
        {
            dtdata = appDal.GetSupervisorEmployeeAppId(Session["EmpInfoId"].ToString(), entryempinfoIdHiddenField.Value);
        }

        //DataTable dtdata = appDal.GetSupervisorAppId(filepath, " AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");

        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("Value");
        aDataTable.Columns.Add("Text");

        DataRow dataRow = null;


        //if (Session["EmpInfoId"].ToString() != Session["ForEmpInfoId"].ToString())



        if (dtdata.Rows.Count > 0)
        {
            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Approved";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Return";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);

        }
        else
        {
            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Approved";
            dataRow["Value"] = "Verified";
            aDataTable.Rows.Add(dataRow);

            dataRow = aDataTable.NewRow();
            dataRow["Text"] = "Return";
            dataRow["Value"] = "Review";
            aDataTable.Rows.Add(dataRow);
        }

        actionRadioButtonList.DataValueField = "Value";
        actionRadioButtonList.DataTextField = "Text";
        actionRadioButtonList.DataSource = aDataTable;
        actionRadioButtonList.DataBind();
        actionRadioButtonList.SelectedIndex = 0;
        if (actionstatusHiddenField.Value == "Approved")
        {
            submitButton.Visible = false;
            Button1.Visible = true;
        }
        else
        {
            submitButton.Visible = true;
            Button1.Visible = false;
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
    protected void SendMail_Click(object sender, EventArgs e)
    {
        try
        {

            for (int i = 0; i < gv_selectedEmp.Rows.Count; i++)
            {

                Label EmpMail = (Label)gv_selectedEmp.Rows[i].Cells[1].FindControl("txt_OfficialEmail");



                System.Threading.Thread.Sleep(100);

                MailMessage mail = new MailMessage();

                mail.To.Add(EmpMail.Text);

                //mail.To.Add
                mail.From = new MailAddress(Session["EmailID"].ToString());
                mail.From.User.ToString();

                mail.Sender = new System.Net.Mail.MailAddress(Session["EmailID"].ToString());
                mail.Subject = "TB Sheet For " + txt_JobCirculation.Text;

                MailAddress copy2 = new MailAddress(Session["EmailID"].ToString());
                mail.Bcc.Add(copy2);



                mail.Body = "";
                mail.IsBodyHtml = true;
                mail.Priority = System.Net.Mail.MailPriority.High;

                //Attach file using FileUpload Control and put the file in memory stream


               
                StringWriter sw2 = new StringWriter();
               
                HtmlTextWriter htw2 = new HtmlTextWriter(sw2);

                loadGridView.AllowPaging = false;

                loadGridView.Columns.Clear();
                InterviewCandidateInfoListDAL aInterviewCandidateInfoListDal = new InterviewCandidateInfoListDAL();
                DataTable dtcandidate = aInterviewCandidateInfoListDal.TebulationMarks(ddlCompany.SelectedValue, hfJobID.Value);
                string[] mainstring = new string[3];
                int count = 0;
                for (int j= 0; j < dtcandidate.Columns.Count; j++)
                {
                    if (j == 0 || j == 1 || j == 2)
                    {
                        mainstring[count] = dtcandidate.Columns[j].ToString();
                        count++;
                    }
                    else
                    {
                        BoundField test = new BoundField();
                        test.DataField = dtcandidate.Columns[j].ToString();
                        test.HeaderText = dtcandidate.Columns[j].ToString();
                        loadGridView.Columns.Add(test);
                    }


                }



                loadGridView.DataKeyNames = mainstring;
                loadGridView.DataSource = dtcandidate;
                loadGridView.DataBind();






                loadGridView.HeaderRow.Style.Add("background-color", "#E5EEF1");

                // Set background color of each cell of GridView1 header row


                
                loadGridView.RenderControl(htw2);


               
                System.Text.Encoding theEncoding2 = System.Text.Encoding.ASCII;
             
                byte[] theByteArray2 = theEncoding2.GetBytes(sw2.ToString());
               
                MemoryStream theMemoryStream2 = new MemoryStream(theByteArray2, false);
              
                mail.Attachments.Add(new Attachment(theMemoryStream2, "Tabulation_Sheet_Info_List.xls"));
                //Attach file using FileUpload Control and put the file in memory stream
                
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
                    showMessageBox("Email has not Sent, Try Once More time");
                }
                catch (Exception exe)
                {
                    showMessageBox("Email has not Sent, Try Once More time");
                }


                System.Threading.Thread.Sleep(100);
                ScriptManager.RegisterStartupScript(this, this.GetType(),
              "alert",
              "alert('Sent Successfully...! ');window.location ='InterviewBoardSetupList.aspx';",
              true);

            }

        }
        catch (Exception)
        {

            //throw;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }  

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    protected void Button2_OnClick(object sender, EventArgs e)
    {
        //if ())
        {


            RecruitmentApprovalDAO aMaster = new RecruitmentApprovalDAO();
            aMaster.RecruitmentId
                = Convert.ToInt32(Session["RecruitmentId"].ToString());
            aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = appDal.UpdateContractural(aMaster.ActionStatus, Convert.ToInt32(Session["RecruitmentId"].ToString()));
            if (status)
            {
                int commentid = appDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                    commentsTextBox.Text);
                if (aMaster.ActionStatus == "Verified")
                {
                    DataTable dtempdata = appDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    RecruitmentApprovalAppLogDAO appLogDao = new RecruitmentApprovalAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                        appLogDao.RecruitmentId = aMaster.RecruitmentId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;

                    };
                    int id = appDal.SavAppLog(appLogDao);
                    appDal.UpdateJobReqStatus2(aMaster);

                    SenMailForApprved(appLogDao.ForEmpInfoId, " Recruitment Approval ", @"  <br/> Dear Sir, <br/>
Recruitment is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                }
                else if (aMaster.ActionStatus == "Approved")
                {
                    int empid = 0;
                    DataTable dtempdata = appDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() + "' AND Serial='1'");
                    if (dtempdata.Rows.Count > 0)
                    {
                        empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                    }
                    RecruitmentApprovalAppLogDAO appLogDao = new RecruitmentApprovalAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = empid;
                        appLogDao.RecruitmentId = aMaster.RecruitmentId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;
                    };
                    aMaster.ActionStatus = "Verified";
                    appDal.UpdateJobReqStatus2(aMaster);
                    int id = appDal.SavAppLog(appLogDao);

                    SenMailForApprved(appLogDao.ForEmpInfoId, " Recruitment Approval ", @"  <br/> Dear Sir, <br/>
Recruitment is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                }
                else if (aMaster.ActionStatus == "Review")
                {
                    DataTable dtempdata = appDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), Session["RecruitmentId"].ToString());
                    DataTable dtempdata2 = appDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), Session["RecruitmentId"].ToString());

                    if (dtempdata2.Rows.Count > 0)
                    {
                        RecruitmentApprovalAppLogDAO appLogDao = new RecruitmentApprovalAppLogDAO();
                        {
                            appLogDao.ActionStatus = "Verified";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString());
                            appLogDao.RecruitmentId = aMaster.RecruitmentId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        }

                        appDal.UpdateAppLog("Review", Session["AppLogId"].ToString());
                        int id = appDal.SavAppLog(appLogDao);
                        appDal.UpdateJobReqStatus2(aMaster);
                    }
                    else
                    {
                        ShowMessageBox("Please select Approval Status Approved  this!!!");
                    }

                }


            }
            Session["AppLogId"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Operation Successful...');window.location ='RecruimentApprovalView.aspx';",
                       true);
        }
    }
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        //if (Validation())
        {


            RecruitmentApprovalDAO aMaster = new RecruitmentApprovalDAO();
            aMaster.RecruitmentId
                = Convert.ToInt32(Session["RecruitmentId"].ToString());
            aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
            bool status = appDal.UpdateJobReqStatus2(aMaster);
            if (status)
            {
                int commentid = appDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                    commentsTextBox.Text);
                if (aMaster.ActionStatus == "Verified")
                {
                    DataTable dtempdata =
                        appDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                       "' AND EmpInfoId='" + Session["EmpInfoId"].ToString() +
                                                                       "' ");
                    int serial = Convert.ToInt32(dtempdata.Rows[0]["Serial"].ToString()) + 1;
                    DataTable dtempdata2 =
                        appDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                       "' AND Serial='" + serial + "' ");
                    //DataTable dtempdata = appDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                    RecruitmentApprovalAppLogDAO appLogDao = new RecruitmentApprovalAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["EmpInfoId"].ToString());
                        appLogDao.RecruitmentId = aMaster.RecruitmentId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;

                    };
                    int id = appDal.SavAppLog(appLogDao);

                    SenMailForApprved(appLogDao.ForEmpInfoId, " Recruitment Approval ", @"  <br/> Dear Sir, <br/>
Recruitment is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                }
                else if (aMaster.ActionStatus == "Approved")
                {
                    int empid = 0;
                    //DataTable dtempdata = appDal.GetHRAdminEmployeeAppId(" WHERE URL='"+Session["AppPage"].ToString()+"' AND Serial='1'" );
                    //if (dtempdata.Rows.Count>0)
                    //{
                    //    empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                    //}
                    RecruitmentApprovalAppLogDAO appLogDao = new RecruitmentApprovalAppLogDAO();
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = empid;
                        appLogDao.RecruitmentId = aMaster.RecruitmentId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;
                    };


                    int id = appDal.SavAppLog(appLogDao);

                    SenMailForApprved(appLogDao.ForEmpInfoId, " Recruitment Approval ", @"  <br/> Dear Sir, <br/>
Recruitment is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                }
                else if (aMaster.ActionStatus == "Review")
                {

                    string actionst = "";
                    DataTable dtdatastatus = appDal.GetAppLogStatus(Session["RecruitmentId"].ToString().ToString(),
                        Session["EmpInfoId"].ToString());
                    if (dtdatastatus.Rows.Count > 0)
                    {
                        actionst = dtdatastatus.Rows[0]["ActionStatus"].ToString();
                    }

                    DataTable dtempdata = appDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), Session["RecruitmentId"].ToString());
                    DataTable dtempdata2 = appDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), Session["RecruitmentId"].ToString());

                    if (dtempdata2.Rows.Count > 0)
                    {
                        RecruitmentApprovalAppLogDAO appLogDao = new RecruitmentApprovalAppLogDAO();
                        {
                            appLogDao.ActionStatus = "Verified";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString());
                            appLogDao.RecruitmentId = aMaster.RecruitmentId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        }

                        appDal.UpdateAppLog("Review", Session["AppLogId"].ToString());

                        if (actionst == "Approved")
                        {
                            appDal.UpdateContractural("Verified", aMaster.RecruitmentId);
                        }

                        int id = appDal.SavAppLog(appLogDao);
                    }
                    else
                    {
                        ShowMessageBox("Please select Approval Status Approved  this!!!");
                    }

                }


            }
            Session["AppLogId"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Operation Successful...');window.location ='RecruimentApprovalView.aspx';",
                       true);
        }
    }

    protected void Button2a_OnClick(object sender, EventArgs e)
    {
        RecruitmentApprovalDAO aMaster = new RecruitmentApprovalDAO();
        aMaster.RecruitmentId
            = Convert.ToInt32(Session["RecruitmentId"].ToString());
        aMaster.ActionStatus = "Rejected";
        bool status = appDal.UpdateContractural(aMaster.ActionStatus, Convert.ToInt32(Session["RecruitmentId"].ToString()));
        int commentid = appDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                commentsTextBox.Text);
        if (aMaster.ActionStatus == "Rejected")
        {
            //DataTable dtempdata = appDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
            RecruitmentApprovalAppLogDAO appLogDao = new RecruitmentApprovalAppLogDAO();
            {
                appLogDao.ActionStatus = "Rejected";
                appLogDao.ApproveDate = DateTime.Now;
                appLogDao.ApproveBy = Session["UserId"].ToString();
                appLogDao.PreEmpInfoId = 0;
                appLogDao.ForEmpInfoId = 0;
                appLogDao.RecruitmentId = aMaster.RecruitmentId;
                appLogDao.Comments = commentsTextBox.Text;
                appLogDao.CommentsId = commentid;

            };
            int id = appDal.SavAppLog(appLogDao);
            appDal.UpdateJobReqStatus2(aMaster);
        }
        Session["AppLogId"] = null;
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Rejected Successfully...');window.location ='RecruimentApprovalView.aspx';",
                   true);
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
                showMessageBox("Email has not Sent, Try Once More time");
            }
            catch (Exception exe)
            {
                showMessageBox("Email has not Sent, Try Once More time");
            }


            System.Threading.Thread.Sleep(100);
        }



    }

    protected void ShowMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("RecruimentApprovalView.aspx");
    }
}