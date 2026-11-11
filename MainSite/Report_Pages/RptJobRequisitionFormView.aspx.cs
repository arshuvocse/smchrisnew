using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.MasterSetup_DAL;
using DAL.Permission_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class MasterSetup_UI_RptJobRequisitionFormView : System.Web.UI.Page
{
    EmployeeRequsitionDAL aEmployeeRequsitionDal=new EmployeeRequsitionDAL();

    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();

    PermissionDAL aPermissionDal = new PermissionDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompany();
            ContractualstartDate.Attributes.Add("readonly", "readonly");
            ContractualendDate.Attributes.Add("readonly", "readonly");
           // UserPersmissionValidation();
            //LoadEmpJobRequisition();
            LoadDroDownList();
        }
    }
    private void LoadDroDownList()
    {
        aEmployeeRequsitionDal.GetCompanyListShortNameIntoDropdown(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_SelectedIndexChanged(null, null);
    }

    public void GetCompany()
    {
        try
        {
            DataTable dtcomp = aPermissionDal.GetCompany();
            lchk_Company.DataValueField = "CompanyId";
            lchk_Company.DataTextField = "ShortName";
            lchk_Company.DataSource = dtcomp;
            lchk_Company.DataBind();

            DataTable userdata = aPermissionDal.GetUserCompany(Session["UserId"].ToString());
            for (int i = 0; i < userdata.Rows.Count; i++)
            {
                for (int j = 0; j < lchk_Company.Items.Count; j++)
                {
                    if (lchk_Company.Items[j].Text.Trim() == userdata.Rows[i]["ShortName"].ToString())
                    {
                        lchk_Company.Items[j].Selected = true;


                    }
                }
            }
        }
        catch (Exception)
        {
            
            Response.Redirect("/Default.aspx");
        }
    }

    public void GetDepet()
    {
        try
        {
            DataTable dtcomp = aPermissionDal.LoadDepartmentByWings(companyDropDownList.SelectedValue);
            lchk_Dpt.DataValueField = "DepartmentId";
            lchk_Dpt.DataTextField = "DepartmentName";
            lchk_Dpt.DataSource = dtcomp;
            lchk_Dpt.DataBind();

           // DataTable userdata = aPermissionDal.GetUserCompany(Session["UserId"].ToString());
            //for (int i = 0; i < userdata.Rows.Count; i++)
            //{
                for (int j = 0; j < lchk_Dpt.Items.Count; j++)
                {
                     
                        lchk_Dpt.Items[j].Selected = true;


                    
                }
            //}
        }
        catch (Exception)
        {

            Response.Redirect("/Default.aspx");
        }
    }


    public void UserPersmissionValidation()
    {
        try
        {
            string filepath = Path.GetDirectoryName(Request.Path);
            filepath = filepath.TrimStart('\\');

            string text = Path.GetExtension(Request.Path);
            if (text == string.Empty)
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path) + ".aspx";
            }
            else
            {
                filepath = "../" + filepath + "/" + Path.GetFileName(Request.Path);
            }
            DataTable dtuserpermission = aPermissionDal.GetPermissionForUser(Session["UserId"].ToString(), filepath);
            if (dtuserpermission.Rows.Count > 0)
            {
                if (dtuserpermission.Rows[0]["UserTypeId"].ToString() != "3" ||
                    dtuserpermission.Rows[0]["UserTypeId"].ToString() != "4")
                {


                    ViewState["Add"] = dtuserpermission.Rows[0]["Add"].ToString();
                    ViewState["Edit"] = dtuserpermission.Rows[0]["Edit"].ToString();
                    ViewState["View"] = dtuserpermission.Rows[0]["View"].ToString();
                    ViewState["Delete"] = dtuserpermission.Rows[0]["Delete"].ToString();

                    addNewButton.Visible = Convert.ToBoolean(ViewState["Add"].ToString());

                    loadGridView.Columns[loadGridView.Columns.Count - 1].Visible =
                        Convert.ToBoolean(ViewState["View"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 2].Visible =
                        Convert.ToBoolean(ViewState["Delete"].ToString());
                    loadGridView.Columns[loadGridView.Columns.Count - 3].Visible =
                        Convert.ToBoolean(ViewState["Edit"].ToString());
                }
            }
            else
            {
                Response.Redirect("../DashBoard_UI/DashBoard.aspx");
            }
        }
        catch (Exception ex)
        {

            aShowMessage.ShowMessageBox(ex.ToString(), this);
        }
    }

    public string CompanyId()
    {
        string companyid = "";
        for (int i = 0; i < lchk_Company.Items.Count; i++)
        {
            
                companyid = companyid + "'" + lchk_Company.Items[i].Value + "'" + ",";
            
        }
        companyid = companyid.TrimEnd(',');
        return companyid;
    }


    public string DepartmentId()
    {
        string deptId = "";
        for (int i = 0; i < lchk_Dpt.Items.Count; i++)
        {
            if (lchk_Dpt.Items[i].Selected)
            {
                deptId = deptId + "'" + lchk_Dpt.Items[i].Value + "'" + ",";
            }
        }
        deptId = deptId.TrimEnd(',');
        return deptId;
    }
  
  
    protected void addNewButton_OnClick(object sender, EventArgs e)
    {
        Session["Status"] = "Add";
        Response.Redirect("JobRequisitionForm.aspx");
    }
    private void LoadEmpJobRequisition()
    {
        try
        {

        
        GetDepet();
        DataTable aDataTable=new DataTable();
        aDataTable = aEmployeeRequsitionDal.RptLoadEmpJobRequisition(GenerateParameter());

        if (aDataTable.Rows.Count > 0)
        {
            

            loadGridView.DataSource = aDataTable;
            loadGridView.DataBind();

        }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            aShowMessage.ShowMessageBox("No Data Found!!", this);
        }
        }
        catch (Exception)
        {

            
        }
    }

    private string GenerateParameter()
    {
        string parameter = " ";


        if (companyDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND   r.CompanyId  =  '" + companyDropDownList.SelectedValue + "' ";
        }


        if (financialYearDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND  r.FinYearId =  '" + financialYearDropDownList.SelectedValue + "'  ";
        }

        if (deptDropDownList.SelectedValue != "")
        {
            parameter = parameter + " AND   r.DeptId =" + deptDropDownList.SelectedValue + " ";
        }


        if (ContractualstartDate.Text != string.Empty && ContractualendDate.Text != string.Empty)
        {
            parameter = parameter + " AND r.ReqDate BETWEEN '" + ContractualstartDate.Text + "' AND '" + ContractualendDate.Text + "' ";
        }
        if (ContractualstartDate.Text != string.Empty && ContractualendDate.Text == string.Empty)
        {
            parameter = parameter + " AND r.ReqDate BETWEEN '" + ContractualstartDate.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (ContractualstartDate.Text == string.Empty && ContractualendDate.Text != string.Empty)
        {
            parameter = parameter + " AND r.ReqDate BETWEEN '" + ContractualendDate.Text + "' AND '" + ContractualendDate.Text + "' ";
        }


        return parameter;
    }



    protected void loadGridView_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       


        if (e.CommandName == "ViewData")
        {
            Session["Status"] = "View";
            int rowindex = Convert.ToInt32(e.CommandArgument);


            var datKey = loadGridView.DataKeys[rowindex];
            if (datKey != null)
            {
                string jobReqId = datKey[0].ToString();
                Session["JobReqId"] = "";
                Session["JobReqId"] = jobReqId;

            }

            
            Response.Redirect("/RecruitmentManagement_UI/JobRequisitionFormList.aspx");

        }


       

      

    }

    public string CheckLAstStatus(string id)
    {
        DataTable CheckLastStatus = new DataTable();
        CheckLastStatus = aEmployeeRequsitionDal.LoadCheckLastStatusById(id);
        string JobCirculation = "";
        lblLast.Text = "";
        if (CheckLastStatus.Rows.Count > 0)
        {
            for (int i = 0; i < CheckLastStatus.Rows.Count; i++)
            {

                string dd = CheckLastStatus.Rows[i].Field<Int32>("SerialNo").ToString();
                string dd2 = CheckLastStatus.Rows[i].Field<string>("jobCirculation").ToString();

                if (CheckLastStatus.Rows[i].Field<string>("jobCirculation") == "Not Done")
                {
                    if (CheckLastStatus.Rows[i].Field<Int32>("SerialNo").ToString() == "1")
                    {
                        JobCirculation = "Job Requisition";
                        lblLast.Text = JobCirculation;
                        return JobCirculation;
                    }
                }

                if (CheckLastStatus.Rows[i].Field<string>("jobCirculation") == "Not Done")
                {
                    if (CheckLastStatus.Rows[i].Field<Int32>("SerialNo").ToString() == "2")
                    {
                        JobCirculation = "Job Circulation";
                        lblLast.Text = JobCirculation;
                        return JobCirculation;
                    }
                }

                if (CheckLastStatus.Rows[i].Field<string>("jobCirculation") == "Not Done")
                {
                    if (CheckLastStatus.Rows[i].Field<Int32>("SerialNo").ToString() == "3")
                    {
                        JobCirculation = "Interview Candidate Information";
                        lblLast.Text = JobCirculation;
                        return JobCirculation; 
                    }
                }

                if (CheckLastStatus.Rows[i].Field<string>("jobCirculation") == "Not Done")
                {
                    if (CheckLastStatus.Rows[i].Field<Int32>("SerialNo").ToString() == "4")
                    {
                        JobCirculation = "Interview Board Information";
                        lblLast.Text = JobCirculation;
                        return JobCirculation;
                    }
                }

                if (CheckLastStatus.Rows[i].Field<string>("jobCirculation") == "Not Done")
                {
                    if (CheckLastStatus.Rows[i].Field<Int32>("SerialNo").ToString() == "5")
                    {
                        JobCirculation = "Interview Candidate Invitation";
                        lblLast.Text = JobCirculation;
                        return JobCirculation;
                    }
                }

                if (CheckLastStatus.Rows[i].Field<string>("jobCirculation") == "Not Done")
                {
                    if (CheckLastStatus.Rows[i].Field<Int32>("SerialNo").ToString() == "6")
                    {
                        JobCirculation = "Interview Candidate Invitation";
                        lblLast.Text = JobCirculation;
                        return JobCirculation;
                    }
                }

                if (CheckLastStatus.Rows[i].Field<string>("jobCirculation") == "Not Done")
                {
                    if (CheckLastStatus.Rows[i].Field<Int32>("SerialNo").ToString() == "7")
                    {
                        JobCirculation = "Interview Board Member Marks Entry";
                        lblLast.Text = JobCirculation;
                        return JobCirculation;
                    }
                }
                if (CheckLastStatus.Rows[i].Field<string>("jobCirculation") == "Employee Information")
                {
                    if (CheckLastStatus.Rows[i].Field<Int32>("SerialNo").ToString() == "7")
                    {
                        JobCirculation = "Done";
                        lblLast.Text = JobCirculation;
                        return JobCirculation;
                    }

                }
            }



        }
        else
        {
            JobCirculation = "Job Requisition";
            lblLast.Text = JobCirculation;
        }
       
        return JobCirculation;
       
    }
    private void PopUp(Int32 jobReqId)
    {
        string url = "../Report_UI/JobRequisitionFormViewReportPreview.aspx?rptType=" + jobReqId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void reloadButton_OnClick(object sender, EventArgs e)
    {
        LoadEmpJobRequisition();
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void companyDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            aEmployeeRequsitionDal.LoadFinancialYearForSearch(financialYearDropDownList,
                       companyDropDownList.SelectedValue);
            aEmployeeRequsitionDal.LoadDepartmentByWings(deptDropDownList, companyDropDownList.SelectedValue);
         
        }
        else
        {
            deptDropDownList.Items.Clear();
            financialYearDropDownList.Items.Clear();
        }
    }

    protected void SearchButton_OnClick(object sender, EventArgs e)
    {
         if (companyDropDownList.SelectedValue != "")
        {


            if (financialYearDropDownList.SelectedValue!="")
            {
                LoadEmpJobRequisition();
            }
            else
            {
                loadGridView.DataSource = null;
                loadGridView.DataBind();
                financialYearDropDownList.Focus();
                aShowMessage.ShowMessageBox("Please Select financial Year !!!", this);
            }


              }
        else
        {
            loadGridView.DataSource = null;
            loadGridView.DataBind();
            companyDropDownList.Focus();
            aShowMessage.ShowMessageBox("Please Select Company !!!", this);
        }
    }

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;



        HiddenField mastrId = (HiddenField)loadGridView.Rows[rowID].FindControl("JobReqId");
        HiddenField ActionStatus = (HiddenField)loadGridView.Rows[rowID].FindControl("ActionStatus");

        if (ActionStatus.Value != "Submitted")
        {
            bool status = aEmployeeRequsitionDal.UpdateActionStatus(Convert.ToInt32(mastrId.Value));



            if (status)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Action Status Submitted Successfully...');window.location ='JobRequisitionFormView.aspx';",
                    true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
"alert",
"alert('Already Submitted !!!');",
true);
        }

       
        
    }

    protected void btnStatus_OnClick(object sender, EventArgs e)
    {

       
        
    }

    protected void ShowPopup(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
      
        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);

        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;



        HiddenField mastrId = (HiddenField)loadGridView.Rows[rowID].FindControl("JobReqId");


        DataTable JobCirculationdata = new DataTable();
        JobCirculationdata = aEmployeeRequsitionDal.LoadJobCirculationStatusById(mastrId.Value);

        if (JobCirculationdata.Rows.Count > 0)
        {
            if (JobCirculationdata.Rows[0]["jobCirculation"].ToString() == "Pending")
            {
                PJobCirculation.BackColor = ColorTranslator.FromHtml("#C82333");

            }

            if (JobCirculationdata.Rows[0]["jobCirculation"].ToString() == "Done")
            {
                PJobCirculation.BackColor = ColorTranslator.FromHtml("#218838");
            }
                lblJobCirculation.InnerHtml = JobCirculationdata.Rows[0]["jobCirculation"].ToString();
           
          
        }


        DataTable InterviewCandidateInformation = new DataTable();
        InterviewCandidateInformation = aEmployeeRequsitionDal.LoadInterviewCandidateInformationStatusById(mastrId.Value);

        if (InterviewCandidateInformation.Rows.Count > 0)
        {
            if (InterviewCandidateInformation.Rows[0]["InterviewCandidate"].ToString() == "Pending")
            {
                PewCandidateInformation.BackColor = ColorTranslator.FromHtml("#C82333");

            }
            if (InterviewCandidateInformation.Rows[0]["InterviewCandidate"].ToString() == "Processing")
            {
                PewCandidateInformation.BackColor = ColorTranslator.FromHtml("#E0A800");

            }

            if (InterviewCandidateInformation.Rows[0]["InterviewCandidate"].ToString() == "Done")
            {
                PewCandidateInformation.BackColor = ColorTranslator.FromHtml("#218838");
            }
            lblInterviewCandidateInformation.InnerText = InterviewCandidateInformation.Rows[0]["InterviewCandidate"].ToString();
        }

        DataTable InterviewBoardInformation = new DataTable();
        InterviewBoardInformation = aEmployeeRequsitionDal.LoadInterviewBoardInformationStatusById(mastrId.Value);

        if (InterviewBoardInformation.Rows.Count > 0)
        {

            if (InterviewBoardInformation.Rows[0]["lblInterviewBoardInformation"].ToString() == "Pending")
            {
                PInterviewBoardInformation.BackColor = ColorTranslator.FromHtml("#C82333");

            }

            if (InterviewBoardInformation.Rows[0]["lblInterviewBoardInformation"].ToString() == "Done")
            {
                PInterviewBoardInformation.BackColor = ColorTranslator.FromHtml("#218838");
            }

            lblInterviewBoardInformation.InnerText = InterviewBoardInformation.Rows[0]["lblInterviewBoardInformation"].ToString();
        }



        DataTable InterviewCandidateInvitation = new DataTable();
        InterviewCandidateInvitation = aEmployeeRequsitionDal.LoadInterviewCandidateInvitationStatusById(mastrId.Value);

        if (InterviewCandidateInvitation.Rows.Count > 0)
        {
            if (InterviewCandidateInvitation.Rows[0]["InterviewCandidateInvitation"].ToString() == "Pending")
            {
                PInterviewCandidateInvitation.BackColor = ColorTranslator.FromHtml("#C82333");

            }

            if (InterviewCandidateInvitation.Rows[0]["InterviewCandidateInvitation"].ToString() == "Done")
            {
                PInterviewCandidateInvitation.BackColor = ColorTranslator.FromHtml("#218838");
            }
            lblInterviewCandidateInvitation.InnerText = InterviewCandidateInvitation.Rows[0]["InterviewCandidateInvitation"].ToString();
        }


        DataTable InterviewCandidateAttandance = new DataTable();
        InterviewCandidateAttandance = aEmployeeRequsitionDal.LoadInterviewCandidateAttandanceStatusById(mastrId.Value);

        if (InterviewCandidateAttandance.Rows.Count > 0)
        {

            if (InterviewCandidateAttandance.Rows[0]["InterviewCandidateAttandance"].ToString() == "Pending")
            {
                PCandidateAttandance.BackColor = ColorTranslator.FromHtml("#C82333");

            }

            if (InterviewCandidateAttandance.Rows[0]["InterviewCandidateAttandance"].ToString() == "Done")
            {
                PCandidateAttandance.BackColor = ColorTranslator.FromHtml("#218838");
            }


            lblCandidateAttandance.InnerText = InterviewCandidateAttandance.Rows[0]["InterviewCandidateAttandance"].ToString();
        }


        DataTable  MarksEntry = new DataTable();
        MarksEntry = aEmployeeRequsitionDal.LoadMarksEntryStatusById(mastrId.Value);

        if (MarksEntry.Rows.Count > 0)
        {

            if (MarksEntry.Rows[0]["MarksEntry"].ToString() == "Pending")
            {
                PMarksEntry.BackColor = ColorTranslator.FromHtml("#C82333");

            }

            //if (MarksEntry.Rows[0]["MarksEntry"].ToString() == "Processing")
            //{
            //    PMarksEntry.BackColor = ColorTranslator.FromHtml("#E0A800");

            //}

            if (MarksEntry.Rows[0]["MarksEntry"].ToString() == "Done")
            {
                PMarksEntry.BackColor = ColorTranslator.FromHtml("#218838");
            }


            lblMarksEntry.InnerText = MarksEntry.Rows[0]["MarksEntry"].ToString();
        }


        DataTable Emp = new DataTable();
        Emp = aEmployeeRequsitionDal.LoadEmpReqIdStatusById(mastrId.Value);

        if (Emp.Rows.Count > 0)
        {
            if (Emp.Rows[0]["EmpJob"].ToString() == "Pending")
            {
                PEmployeeInformation.BackColor = ColorTranslator.FromHtml("#C82333");

            }

            if (Emp.Rows[0]["EmpJob"].ToString() == "Done")
            {
                PEmployeeInformation.BackColor = ColorTranslator.FromHtml("#218838");
            }


            lblEmployeeInformation.Text = Emp.Rows[0]["EmpJob"].ToString();
            
        }


        CheckLAstStatus(mastrId.Value);
    }
}