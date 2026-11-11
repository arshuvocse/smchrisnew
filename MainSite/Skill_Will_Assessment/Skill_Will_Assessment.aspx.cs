using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.Appraisal;
using DAL.COMMON_DAL;
using DAL.SKILL_WILL_DAL;
using DAL.SuspendAndDiciplinary_Dal;
using DAL.TrainingDAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using DAO.SkillWill_Dao;
using HELPER_FUNCTIONS.HELPERS;
using Microsoft.Owin;

public partial class Skill_Will_Assessment_Skill_Will_Assessment : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private TrainingDAL _trainingDal = new TrainingDAL();
    private AppraisalFunctionalPartDAL _appPartA = new AppraisalFunctionalPartDAL();
    private AppraisalPartBDAL _appraisalPartBdal = new AppraisalPartBDAL();
    EmployeeSuspendDal aSuspendDal = new EmployeeSuspendDal();
    private JdDAL _jdDal = new JdDAL();



    private Skill_Will_Dal aWillDal = new Skill_Will_Dal();

    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
  

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
              //  ButtonVisible();


              LoadSKillAssessment();
              LoadWillAssessment();

              IniKRATable();

                if (!string.IsNullOrEmpty(Request.QueryString["financialYearId"]))
                {
                    hfFinYear.Value = (Request.QueryString["financialYearId"]);



                }


                if (!string.IsNullOrEmpty(Request.QueryString["EmpSkillWillMasterId"]))
                {

                    hfEmpSkillWillMasterId.Value = (Request.QueryString["EmpSkillWillMasterId"]);

                    if (hfEmpSkillWillMasterId.Value!="0")
                    {
                        DataTable dt = aWillDal.GetSkillWillDetailsById(Convert.ToInt32(hfEmpSkillWillMasterId.Value));

                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        CalculateTotal();
                        WILLCalculateTotal();

                        submitButton.Text = "Update";
                    }
                }

                if (Session["EmpInfoId"] != null)
                {

                    //  Request.QueryString["rptType"];
                    GetEmpinfo(Request.QueryString["EmpInfoId"].ToString());
                }
            
            }
        }
        catch (Exception)
        {


        }

    }


    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("Skill_WillAssesmentList.aspx");
    }



    private void LoadSKillAssessment()
    {
        DataTable dt = aWillDal.GetSKILLALL();

        if (dt.Rows.Count > 0)
        {
            gv_AppraisalFunc.DataSource = dt;
            gv_AppraisalFunc.DataBind();
        }
    }


    private void LoadWillAssessment()
    {
        DataTable dt = aWillDal.GetWillALL();

        if (dt.Rows.Count > 0)
        {
            //gv_WillAssessment.DataSource = dt;
            //gv_WillAssessment.DataBind();

            gv_AppraisalPartB.DataSource = dt;
            gv_AppraisalPartB.DataBind();
        }
    }



    private void IniKRATable()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("KRA", typeof(string)));
        dt.Columns.Add(new DataColumn("Skill", typeof(string)));
        dt.Columns.Add(new DataColumn("Will", typeof(string)));
        dt.Columns.Add(new DataColumn("Areasconsidered", typeof(string)));
      
        dr = dt.NewRow();

        dr["KRA"] = "";
        dr["Skill"] = "";
        dr["Will"] = "";
        dr["Areasconsidered"] = "";
        dt.Rows.Add(dr);
        ViewState["KRAINFO"] = dt;
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }



    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {


            if (Session["Status"].ToString() == "Add")
            {
                submitButton.Visible = true;
               
            }
            else if (Session["Status"].ToString() == "Edit")
            {
               
            }
            else if (Session["Status"].ToString() == "View")
            {
               
                submitButton.Visible = false;
             //   btn_Save.Visible = false;
              //  orBTN.Visible = false;
            }

            //else if (Session["Status"].ToString() == "Delete")
            //{
            //    delButton.Visible = true;
            //}
            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("~/Appraisal/AppraisalSelfList.aspx");
        }
    }
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
  



    protected void itemaddImageButton_Click(object sender, EventArgs e)
    {
        DataTable aTable = new DataTable();
        aTable.Columns.Add(new DataColumn("KRA", typeof(string)));
        aTable.Columns.Add(new DataColumn("Skill", typeof(string)));
        aTable.Columns.Add(new DataColumn("Will", typeof(string)));
        aTable.Columns.Add(new DataColumn("Areasconsidered", typeof(string)));
        DataRow dr;
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            dr = aTable.NewRow();

            dr["KRA"] = ((TextBox)GridView1.Rows[i].FindControl("txtKRA")).Text.Trim();
            dr["Skill"] = ((TextBox)GridView1.Rows[i].FindControl("txtSkill")).Text.Trim();
            dr["Will"] = ((TextBox)GridView1.Rows[i].FindControl("txtWill")).Text.Trim();
            dr["Areasconsidered"] = ((TextBox)GridView1.Rows[i].FindControl("txtAreasconsidered")).Text.Trim();
            aTable.Rows.Add(dr);
        }
        dr = aTable.NewRow();
        aTable.Rows.Add(dr);
        GridView1.DataSource = aTable;
        GridView1.DataBind();
        CalculateTotal();
        WILLCalculateTotal();

    }


    protected void itemdeleteImageButton_Click(object sender, EventArgs e)
    {
        int rowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;

        DataTable aTable = new DataTable();
        aTable.Columns.Add(new DataColumn("KRA", typeof(string)));
        aTable.Columns.Add(new DataColumn("Skill", typeof(string)));
        aTable.Columns.Add(new DataColumn("Will", typeof(string)));
        aTable.Columns.Add(new DataColumn("Areasconsidered", typeof(string)));

        DataRow dr;

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            if (rowIndex == 0)
            {
                dr = aTable.NewRow();
                dr["KRA"] = ((TextBox)GridView1.Rows[i].FindControl("txtKRA")).Text.Trim();
                dr["Skill"] = ((TextBox)GridView1.Rows[i].FindControl("txtSkill")).Text.Trim();
                dr["Will"] = ((TextBox)GridView1.Rows[i].FindControl("txtWill")).Text.Trim();
                dr["Areasconsidered"] = ((TextBox)GridView1.Rows[i].FindControl("txtAreasconsidered")).Text.Trim();
                aTable.Rows.Add(dr);
            }
            else
            {
                if (i != rowIndex)
                {
                    dr = aTable.NewRow();
                    dr["KRA"] = ((TextBox)GridView1.Rows[i].FindControl("txtKRA")).Text.Trim();
                    dr["Skill"] = ((TextBox)GridView1.Rows[i].FindControl("txtSkill")).Text.Trim();
                    dr["Will"] = ((TextBox)GridView1.Rows[i].FindControl("txtWill")).Text.Trim();
                    dr["Areasconsidered"] = ((TextBox)GridView1.Rows[i].FindControl("txtAreasconsidered")).Text.Trim();

                    aTable.Rows.Add(dr);
                }
            }
          
        }
        GridView1.DataSource = aTable;
        GridView1.DataBind();
        CalculateTotal();
        WILLCalculateTotal();

    }


    private bool Validation()
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            
               TextBox KRA = (TextBox)GridView1.Rows[i].FindControl("txtKRA");

               TextBox Skill = (TextBox)GridView1.Rows[i].FindControl("txtSkill");

               TextBox Will = (TextBox)GridView1.Rows[i].FindControl("txtWill");

               if (KRA.Text.Trim() == "" || Skill.Text.Trim() == "" || Will.Text.Trim() == "")
               {
                   aShowMessage.ShowMessageBox("Please Input All Required Data", this);
                   return false;
               }
        }

        return true;
    }




    protected void btn_Save_OnClick(object sender, EventArgs e)
    {

        if (Validation())
        {

            if (EmpInfoId.Value != "")
            {
                List<EmpSkillWillDetailsDao> functional = new List<EmpSkillWillDetailsDao>();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    EmpSkillWillDetailsDao area = new EmpSkillWillDetailsDao();
                    TextBox KRA = (TextBox)GridView1.Rows[i].FindControl("txtKRA");
                    TextBox SKILL = (TextBox)GridView1.Rows[i].FindControl("txtSkill");
                    TextBox WILL = (TextBox)GridView1.Rows[i].FindControl("txtWill");
                    TextBox txtAreasconsidered = (TextBox)GridView1.Rows[i].FindControl("txtAreasconsidered");
                    area.KRA = string.IsNullOrEmpty(KRA.Text) ? null : KRA.Text;
                    area.SKILL = string.IsNullOrEmpty(SKILL.Text) ? 0 : Convert.ToInt32(SKILL.Text);
                    area.WILL = string.IsNullOrEmpty(WILL.Text) ? 0 : Convert.ToInt32(WILL.Text);
                    area.Areasconsidered = string.IsNullOrEmpty(txtAreasconsidered.Text) ? null : txtAreasconsidered.Text;
                    functional.Add(area);
                }

                EmpSkillWillAssessmentMaster aMaster = new EmpSkillWillAssessmentMaster();

                aMaster.EmpSkillWillMasterId = Convert.ToInt32(hfEmpSkillWillMasterId.Value);
                aMaster.EmpInfoId = Convert.ToInt32(EmpInfoId.Value);
                try
                {
                    aMaster.FinancialYearId = Convert.ToInt32(hfFinYear.Value);
                }
                catch (Exception)
                {
                    aMaster.FinancialYearId = 0;
                    //throw;
                }

                bool result = false;
                if (functional.Count > 0)
                {
                    int pk = aWillDal.SaveEmpSkillWillMaster(aMaster, Session["LoginName"].ToString());
                    if (pk > 0)
                    {
                        result = aWillDal.SaveEmpSkillWillDetails(functional, pk);


                     
                        DataTable CheckFinalApproval = _appPartA.CheckFinalApprovalCondition(HFCompanyId.Value, aMaster.EmpInfoId.ToString());
                        if (CheckFinalApproval.Rows.Count>0)
                        {
                            if (CheckFinalApproval.Rows[0]["EmpInfoId"].ToString() == Session["EmpInfoId"].ToString())
                            {
                                EmpSkillWillAssessmentMasterAppLogDAO aMasterApp = new EmpSkillWillAssessmentMasterAppLogDAO();
                                aMasterApp.EmpSkillWillMasterId
                                    = Convert.ToInt32(pk);




                                aMasterApp.ActionStatus = "Approved";
                                bool status = aWillDal.UpdateContractural(aMasterApp);
                            }
                            else
                            {
                                EmpSkillWillAssessmentMasterAppLogDAO appLogDao = new EmpSkillWillAssessmentMasterAppLogDAO();

                                appLogDao.ActionStatus = "Drafted";
                                appLogDao.ApproveDate = DateTime.Now;
                                appLogDao.ApproveBy = Session["UserId"].ToString();
                                appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                                appLogDao.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                                appLogDao.EmpSkillWillMasterId = Convert.ToInt32(pk);
                                appLogDao.Comments = "";
                                appLogDao.CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString());




                                int idd = aWillDal.SaveEmpAppLog(appLogDao);


                                EmpSkillWillAssessmentMasterAppLogDAO aMastera = new EmpSkillWillAssessmentMasterAppLogDAO();
                                aMastera.EmpSkillWillMasterId
                                    = Convert.ToInt32(pk);
                                aMastera.ActionStatus = "Verified";
                                bool status = aWillDal.UpdateContractural(aMastera);
                                EmpSkillWillAssessmentMasterAppLogDAO appLogDao1 = new EmpSkillWillAssessmentMasterAppLogDAO()
                                {
                                    ActionStatus = "Verified",
                                    ApproveDate = DateTime.Now,
                                    ApproveBy = Session["UserId"].ToString(),
                                    PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString()),
                                    ForEmpInfoId = Convert.ToInt32(CheckFinalApproval.Rows[0]["EmpInfoId"].ToString()),
                                    EmpSkillWillMasterId = Convert.ToInt32(pk),
                                    Comments = "",
                                    CommentsEMP = Convert.ToInt32(Session["EmpInfoId"].ToString()),

                                };
                                int id = aWillDal.SaveEmpAppLog(appLogDao1);

                                SenMailForApprved(appLogDao1.ForEmpInfoId, " Skill Will Assessment  Approval ", @"  <br/> Dear Sir, <br/>
An Employee's Skill Will Assessment is waiting for your approval. <br/><br/>
please login for the details from the below link.<br/>    http://182.160.103.234:8088/
 <br/> Thank You.");
                            }

                      

                        }
                        else
                        {
                            aShowMessage.ShowMessageBox("Final Approval has not been set yet!!", this);
                            result = false;

                        }
                        

                    }
                }
                else
                {
                    result = false;
                }

                if (result == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Operation Successful...');window.location ='SkillWill_AssesmentView.aspx';",
                        true);
                }
            }
        }

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
                LocationLabel.Text = dtEmp.Rows[0]["Location"].ToString();
                lblPlace.Text = dtEmp.Rows[0]["SalaryLocation"].ToString();
                ReportingLabel.Text = dtEmp.Rows[0]["ReportingToName"].ToString();
            HFCompanyId.Value = dtEmp.Rows[0]["CompanyId"].ToString();
            
            }
    }



    protected void CalculateTotal()
    {
        decimal TSkill = 0;
        decimal AVGSkill = 0;

        decimal count = 0;
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                
                //    if (chkIsActive.Checked)
                {


                    TextBox txtSkill = (TextBox)GridView1.Rows[i].FindControl("txtSkill");

                    if (txtSkill.Text != "")
                    {
                        count = count + 1;

                    }







                    if (txtSkill.Text == "")
                    {
                        TSkill = TSkill + 0;
                    }
                    else
                    {
                        TSkill = TSkill + Convert.ToInt32(txtSkill.Text.ToString());
                    }




 

                }

            }

            string skillText = "";
            try
            {
                AVGSkill = TSkill/count;

                if (AVGSkill>=4)
                {
                    skillText = "HIGH";
                }
                else
                {
                    skillText = "LOW";
                    
                }
            }
            catch (Exception)
            {
                
               // throw;
            }

            Label lbl_SkillT = (Label)GridView1.FooterRow.FindControl("lbl_SkillT");
            Label lbls_SkillT = (Label)GridView1.FooterRow.FindControl("lbls_SkillT");

            lbl_SkillT.Text = Math.Round(AVGSkill,2).ToString();
            lbls_SkillT.Text = skillText.ToString();
            //lblRejectQty.Text = TrQty.ToString();
            //lbl_SewingInputQtyT.Text = SewingInputQtyt.ToString();
        }
    }

    protected void WILLCalculateTotal()
    {
        decimal TSkill = 0;
        decimal AVGSkill = 0;

        decimal count = 0;
        if (GridView1.Rows.Count > 0)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
             
                //    if (chkIsActive.Checked)
                {


                    TextBox txtSkill = (TextBox)GridView1.Rows[i].FindControl("txtWill");


                    if (txtSkill.Text != "")
                    {
                        count = count + 1;
                        
                    }



                    if (txtSkill.Text == "")
                    {
                        TSkill = TSkill + 0;
                    }
                    else
                    {
                        TSkill = TSkill + Convert.ToInt32(txtSkill.Text.ToString());
                    }






                }

            }

            string skillText = "";
            try
            {
                AVGSkill = TSkill / count;

                if (AVGSkill >= 4)
                {
                    skillText = "HIGH";
                }
                else
                {
                    skillText = "LOW";

                }
            }
            catch (Exception)
            {

                // throw;
            }

            Label lbl_WilllT = (Label)GridView1.FooterRow.FindControl("lbl_WilllT");
            Label lbls_WilllTd = (Label)GridView1.FooterRow.FindControl("lbls_WilllTd");

            lbl_WilllT.Text = Math.Round(AVGSkill, 2).ToString();
            lbls_WilllTd.Text = skillText.ToString();
            //lblRejectQty.Text = TrQty.ToString();
            //lbl_SewingInputQtyT.Text = SewingInputQtyt.ToString();
        }
    }

    protected void txtSkill_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox dropDown = (TextBox)sender;
            GridViewRow currentRow = (GridViewRow)dropDown.Parent.Parent;
            int rowindex = 0;
            rowindex = currentRow.RowIndex;
            TextBox txtSkill = (TextBox)GridView1.Rows[rowindex].Cells[0].FindControl("txtSkill");

            int skill = 0;

            try
            {
            skill=    Convert.ToInt32(txtSkill.Text);
            }
            catch (Exception)
            {
                
                //throw;
            }

            if (skill<=5)
            {
                 
            }
            else
            {
                txtSkill.Focus();
                txtSkill.Text = "0";
                aShowMessage.ShowMessageBox("SKILL Can not be more than 5.", this);
            }
        }


        catch (Exception)
        {
            
           // throw;
        }

        CalculateTotal();
    }

    protected void txtWill_OnTextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox dropDown = (TextBox)sender;
            GridViewRow currentRow = (GridViewRow)dropDown.Parent.Parent;
            int rowindex = 0;
            rowindex = currentRow.RowIndex;
            TextBox txtSkill = (TextBox)GridView1.Rows[rowindex].Cells[0].FindControl("txtWill");

            int skill = 0;

            try
            {
                skill = Convert.ToInt32(txtSkill.Text);
            }
            catch (Exception)
            {

                //throw;
            }

            if (skill <= 5)
            {

            }
            else
            {
                txtSkill.Focus();
                txtSkill.Text = "0";
                aShowMessage.ShowMessageBox("WILL Can not be more than 5.", this);
            }
        }


        catch (Exception)
        {

            // throw;
        }
        WILLCalculateTotal();
    }
}