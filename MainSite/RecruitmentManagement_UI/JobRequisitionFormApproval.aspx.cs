using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.Xml;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Adapters;
using BLL.RecruitmentManagement_BLL;
using DAL.MasterSetup_DAL;
using DAL.RecruitmentManagement_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using Microsoft.Practices.EnterpriseLibrary.Data;

public partial class RecruitmentManagement_UI_JobRequisitionFormApproval : System.Web.UI.Page
{
    EducationRequirementsDetailDAL aEducationRequirementsDetailDAL = new EducationRequirementsDetailDAL();
    OtherRequirementDetailDAL aOtherRequirementDetailDAL = new OtherRequirementDetailDAL();
    EmployeeRequsitionDAL aEmployeeRequsitionDal = new EmployeeRequsitionDAL();
    SubSectionInformationDal asSectionInformationDal = new SubSectionInformationDal();
   JobReqFormBll aJobReqFormBll = new JobReqFormBll();
   private int _EMpId;
   ShowMessage aShowMessage = new ShowMessage();
   Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _EMpId = Convert.ToInt32(Session["EmpInfoId"].ToString());
        }
        catch (Exception)
        {
            _EMpId = 0;
            //throw;
        }
        if (!IsPostBack)
        {


            if (Session["JobReqId"]!=null)
            {
                
           
            ReadonlyDateTime();
            MethodAutoIncri();
            RadioListLoad();
             GetValue();
            HideDivs();
            SetSearchBoxStatus();
            LoadCircularSource();

        //    ButtonVisible();

            //BudgetCodeDropDownList.Visible = isBudgetedCheckBox.Checked;
            //LblOther.Visible = isBudgetedCheckBox.Checked;

            DropDownList();
            LoadEduGrid();
            LoadKeyResponse();

           if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {

                empIdHiddenField.Value = int.Parse(Request.QueryString["mid"]).ToString();
                LoadData(empIdHiddenField.Value);
                Session["JobReqId"] = null;
               
            }

            else
            {
                 Response.Redirect("JobRequisitionFormApprovalView.aspx");
            }
           RadioTextValue();
            }
            else
            {
                Response.Redirect("JobRequisitionFormAproval.aspx");
                
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
    private bool valiHRR()
    {
        DataTable dtempdata = aEmployeeRequsitionDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() + "' AND Serial='1'" +
                                                                       "  AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' ");
        if (dtempdata.Rows.Count == 0)
        {
            showMessageBox("HR Approval Information is not exist!! ");
            return false;
        }


        tblEmpGeneralInfo bm;
        using (var db = new HRIS_SMCEntities())
        {
            bm = (from m in db.tblEmpGeneralInfoes where m.EmpInfoId == _EMpId select m).FirstOrDefault();

            try
            {
                int empid = (int)bm.ReportingEmpId;

                if (empid == null)
                {
                    showMessageBox("Supervisor not Found");
                    return false;
                }
            }
            catch (Exception)
            {
                showMessageBox("Supervisor not Found");
                return false;
                //throw;
            }
        }


        return true;
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
        if (actionstatusHiddenField.Value=="Approved")
        {
            dtdata = aEmployeeRequsitionDal.GetHRAdminEmployeeAppId(" WHERE URL='" + filepath + "' AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' AND Serial IN (SELECT MAX(Serial)AS SL FROM dbo.tblEmployeeApprovalByOpearationDetail" +
                                                                   " LEFT JOIN dbo.tblMainMenu ON dbo.tblEmployeeApprovalByOpearationDetail.Operation=dbo.tblMainMenu.MainMenuId WHERE URL='" + filepath + "'  ) AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "' and  CompanyId='" + Session["CompanyId"].ToString() + "' ORDER BY Serial ASC ");
        }
        else
        {
            dtdata = aEmployeeRequsitionDal.GetSupervisorEmployeeAppId(Session["EmpInfoId"].ToString(), entryempinfoIdHiddenField.Value);    
        }
        
        //DataTable dtdata = aEmployeeRequsitionDal.GetSupervisorAppId(filepath, " AND EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");

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

        if (aDataTable.Rows.Count > 0)
        {
            actionRadioButtonList.Items[0].Selected = true;
        }

        if (actionstatusHiddenField.Value=="Approved")
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

    private void ReadonlyDateTime()
    {
        expDtJoinTextBox.Attributes.Add("readonly", "readonly");
        reqDateTextBox.Attributes.Add("readonly", "readonly");
    }

    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {


            if (Session["Status"].ToString() == "Add")
            {
                submitButton.Visible = true;
              //  btnSubmit.Visible = true;
            }
           
            Session["Status"] = null;
        }
        //else
        //{
        //    Response.Redirect("JobRequisitionFormView.aspx");
        //}

    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    private void LoadCircularSource()
    {
        DataTable aTable = aEmployeeRequsitionDal.CurculationWayList();

        CheckBoxList1.DataValueField = "VacancyCirculationId";
        CheckBoxList1.DataTextField = "CirculationWay";
        CheckBoxList1.DataSource = aTable;
        CheckBoxList1.DataBind();
    }

    private void SetSearchBoxStatus()
    {
        codeLabel.Text = "";
        nameLabel.Text = "";
        desigLabel.Text = "";
        salgradLabel.Text = "";
        divLabel.Text = "";
        DateOfSeperationTextBox.Text = "";

        if (companyDropDownList.SelectedValue != "")
        {
            //EmployeeNameTextBox.Enabled = true;
            //EmployeeNameTextBox.ToolTip = "";
        }
        else
        {
            //EmployeeNameTextBox.Enabled = false;
            //EmployeeNameTextBox.ToolTip = "Select company for enabling textbox !!";
        }
    }

    private void HideDivs()
    {
        lblEmpName.Visible = false;
        lblDatSep.Visible = false;
        detail.Visible = false;

        //DateOfSeperationTextBox.Visible = false;
        //EmployeeNameTextBox.Visible = false;

        //DateOfSeperationTextBox.Text = String.Empty;
        EmployeeNameTextBox.Text = "";
        //jobDesc.Visible = false;
        descriptionTextBox.Text = "";
    }

    private void RadioListLoad()
    {
        string constr = ConfigurationManager.ConnectionStrings["SolutionConnectionStringHRDB"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            string query = @"SELECT EmpTypeId, EmpType FROM tblEmployeeType";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                typeOfPosRadioButtonList.DataSource = cmd.ExecuteReader();
                typeOfPosRadioButtonList.DataTextField = "EmpType";
                typeOfPosRadioButtonList.DataValueField = "EmpTypeId";
                typeOfPosRadioButtonList.DataBind();
                con.Close();
            }
        }
    }

    private void GetValue()
    {
        //BudgetCodeDropDownList.Visible = isBudgetedCheckBox.Checked;
        //lblCodeBudget.Visible = isBudgetedCheckBox.Checked;


        //DateOfSeperationTextBox.Visible = IsReplacementforCheckBox.Checked;
        //EmployeeNameTextBox.Visible = IsReplacementforCheckBox.Checked;
        //DateOfSeperationTextBox.Visible = IsReplacementforCheckBox.Checked;
        //EmployeeDropDownList.Visible = IsReplacementforCheckBox.Checked;
        //EmployeeNameTextBox.Visible = IsReplacementforCheckBox.Checked;

        //lblDatSep.Visible = IsReplacementforCheckBox.Checked;
        //lblEmpName.Visible = IsReplacementforCheckBox.Checked;


       
        //ShowReplacementfor.Visible = false;

        otherTextBox.Visible = OtherCheckBox.Checked;
        LblOther.Visible = OtherCheckBox.Checked;
    }

    public void LoadData(string id)
    {
        DataTable dtdata = new DataTable();
        dtdata = aEmployeeRequsitionDal.LoadEmpJobRequisitionById(id);

        if (dtdata.Rows.Count > 0)
        {
            empIdHiddenField.Value = dtdata.Rows[0].Field<Int32>("JobReqId").ToString();
            entryempinfoIdHiddenField.Value = dtdata.Rows[0]["EmpInfoId"].ToString();
            actionstatusHiddenField.Value = dtdata.Rows[0].Field<String>("ActionStatus").ToString();
            companyDropDownList.SelectedValue = dtdata.Rows[0]["CompanyId"].ToString();
            lblcompanyDropDownList.Text = companyDropDownList.SelectedItem.Text;
            if (companyDropDownList.SelectedValue != "")
            {
                Session["CompanyId"] = "";
                Session["CompanyId"] = companyDropDownList.SelectedValue;
            }
            ddlEmpCategoryEx.SelectedValue = dtdata.Rows[0]["EmployeeCategoryId"].ToString();
            lblEmpCategoryEx.Text = ddlEmpCategoryEx.SelectedItem.Text;
            aEmployeeRequsitionDal.LoadGradeByCatID(gradeDropDownList, ddlEmpCategoryEx.SelectedValue);
            aEmployeeRequsitionDal.LoadFinancialYear(financialYearDropDownList, companyDropDownList.SelectedValue);
            financialYearDropDownList.SelectedValue = dtdata.Rows[0]["FinYearId"].ToString();
            lblfinancialYearDropDownList.Text = financialYearDropDownList.SelectedItem.Text;
            //aEmployeeRequsitionDal.EmployeeNameDropDown(EmployeeDropDownList, companyDropDownList.SelectedValue);
            //EmployeeDropDownList.SelectedValue = dtdata.Rows[0]["EmployeeId"].ToString();

            //if (typeOfPosRadioButtonList.Items[0].Selected == true)
            //{
            //    aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[0].Value;
            //}

            //if (typeOfPosRadioButtonList.Items[1].Selected == true)
            //{
            //    aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[1].Value;
            //}

            //if (typeOfPosRadioButtonList.Items[2].Selected == true)
            //{
            //    aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[2].Value;
            //}


            if (dtdata.Rows[0]["JobSummery"].ToString() != "")
            {
                descriptionTextBox.Text = dtdata.Rows[0]["JobSummery"].ToString();

            }
            else
            {
                descriptionTextBox.Text = "N/A";

            }

            if (dtdata.Rows[0]["JobTitle"].ToString() != "")
            {
                jobTitleTextBox.Text = dtdata.Rows[0]["JobTitle"].ToString();

            }
            else
            {

                jobTitleTextBox.Text = "N/A";
            }


            reqDateTextBox.Text = Convert.ToDateTime(dtdata.Rows[0]["ReqDate"].ToString()).ToString("dd-MMM-yyyy");
            mainFinyearDropDownList.SelectedValue = dtdata.Rows[0]["FinYearId"].ToString();
            JustificationTextBox.Text = dtdata.Rows[0]["Justification"].ToString();

            //if ( dtdata.Rows[0]["EmpTypeId"] !=1.ToString())
            //{
            //    typeOfPosRadioButtonList.Items[0].Selected=true;
            //}

            //if (typeOfPosRadioButtonList.Items[1].Value == dtdata.Rows[0]["EmpTypeId"])
            //{
            //    typeOfPosRadioButtonList.Items[1].Selected = true;
            //}

            //if (typeOfPosRadioButtonList.Items[2].Value == dtdata.Rows[0]["EmpTypeId"])
            //{
            //    typeOfPosRadioButtonList.Items[2].Selected = true;
            //}


            for (int i = 0; i < typeOfPosRadioButtonList.Items.Count; i++)
            {
                if (typeOfPosRadioButtonList.Items[i].Value == dtdata.Rows[0]["EmpTypeId"].ToString())
                {
                    typeOfPosRadioButtonList.Items[i].Selected = true;
                    lbltypeOfPosRadioButtonList.Text = typeOfPosRadioButtonList.Items[i].Text;
                }
            }

            //  project.Visible = false;

            for (int i = 0; i < typeOfPosRadioButtonList.Items.Count; i++)
            {

                if (typeOfPosRadioButtonList.Items[i].Selected)
                {
                    if (typeOfPosRadioButtonList.Items[i].Text.Trim() == "Contractual")
                    {

                        if (companyDropDownList.SelectedValue != "")
                        {
                            //   project.Visible = true;
                            //   MonthDiv.Visible = true;
                            //     FunDDiv.Visible = true;
                            aEmployeeRequsitionDal.GEtProjectDdl(projectDropDownList, companyDropDownList.SelectedValue);
                            projectDropDownList.SelectedValue = dtdata.Rows[0]["ProjectId"].ToString();

                            if (dtdata.Rows[0]["ProjectId"].ToString() != "")
                            {
                                lblprojectDropDownList.Text = projectDropDownList.SelectedItem.Text;

                                if(projectDropDownList.SelectedItem.Text=="Select any one")
                                {
                                    lblprojectDropDownList.Text = "N/A";

                                }

                            }
                            else
                            {
                                lblprojectDropDownList.Text = "N/A";

                            }

                            if (dtdata.Rows[0]["MonthContractual"].ToString() != "")
                            {
                                MonthTextBox.Text = dtdata.Rows[0]["MonthContractual"].ToString();

                            }
                            else
                            {
                                MonthTextBox.Text = "N/A";
                            }

                            if (dtdata.Rows[0]["FundContractual"].ToString() != "")
                            {
                                FundInfoTextBox.Text = dtdata.Rows[0]["FundContractual"].ToString();

                            }
                            else
                            {
                                FundInfoTextBox.Text = "N/A";
                            }


                        }
                        else
                        {
                            typeOfPosRadioButtonList.Items[i].Selected = false;
                            ShowMessageBox("Please select company!!!");
                        }

                    }
                    else
                    {
                        // project.Visible = false;
                        projectDropDownList.Items.Clear();
                        lblprojectDropDownList.Text = "N/A";
                        MonthTextBox.Text = "N/A";
                        FundInfoTextBox.Text = "N/A";
                    }
                }

            }



            //if (typeOfPosRadioButtonList.Items[3].Value == dtdata.Rows[0]["EmpTypeId"])
            //{
            //    typeOfPosRadioButtonList.Items[3].Selected = true;
            //}
            //     aEmployeeRequsitionDal.LoadDesignationByCompanyId(jobtitleDropDownList, companyDropDownList.SelectedValue);
            jobtitleDropDownList.SelectedValue = dtdata.Rows[0]["SupervisorId"].ToString();

            if (dtdata.Rows[0]["InternalContact"].ToString() != "")
            {
                internalConTextBox.Text = dtdata.Rows[0]["InternalContact"].ToString();

            }
            else
            {
                internalConTextBox.Text = "N/A";
            }

            if (dtdata.Rows[0]["ExternalContact"].ToString() != "")
            {
                externalConTextBox.Text = dtdata.Rows[0]["ExternalContact"].ToString();

            }
            else
            {
                externalConTextBox.Text = "N/A";
            }

            // officeDropDownList.SelectedValue = dtdata.Rows[0]["OfficeId"].ToString();



            aEmployeeRequsitionDal.GetJobLocationOnPlaceAll(placeDropDownList);

            if (dtdata.Rows[0]["PlaceId"].ToString() != "")
            {
                placeDropDownList.SelectedValue = dtdata.Rows[0]["PlaceId"].ToString();
            }

            //   aEmployeeRequsitionDal.GetJobLocationOnPlaceDdl(placeDropDownList, officeDropDownList.SelectedValue);




            aEmployeeRequsitionDal.LoadDivisionDdl(dsnDropDownList, companyDropDownList.SelectedValue);


            gradeDropDownList.SelectedValue = dtdata.Rows[0]["GradeId"].ToString();

            if (gradeDropDownList.SelectedValue != "")
            {
                lblgradeDropDownList.Text = gradeDropDownList.SelectedItem.Text;

            }
            else
            {
                lblgradeDropDownList.Text = "N/A";
            }

            if (dtdata.Rows[0]["Nos"].ToString() != "")
            {
                nosTextBox.Text = dtdata.Rows[0]["Nos"].ToString();

            }
            else
            {
                nosTextBox.Text = "N/A";
            }

            if (dtdata.Rows[0]["ReqCode"].ToString() != "")
            {
                ReqCodetextBox.Text = dtdata.Rows[0]["ReqCode"].ToString();

            }
            else
            {
                ReqCodetextBox.Text = "N/A";
            }


            if (dtdata.Rows[0]["Note"].ToString() != "")
            {
                NoteTextBox.Text = dtdata.Rows[0]["Note"].ToString();

            }
            else
            {
                NoteTextBox.Text = "N/A";

            }


            if (dtdata.Rows[0]["Others"].ToString() != "")
            {
                otherTextBox.Text = dtdata.Rows[0]["Others"].ToString();

            }
            else
            {
                otherTextBox.Text = "N/A";
            }

            if (dtdata.Rows[0]["Remarks"].ToString() != "")
            {
                RemarksTextBox.Text = dtdata.Rows[0]["Remarks"].ToString();

            }
            else
            {
                RemarksTextBox.Text = "N/A";
            }

            if (dtdata.Rows[0]["PlaceOfPosting"].ToString() != "")
            {
                placeOfPostingTextBox.Text = dtdata.Rows[0]["PlaceOfPosting"].ToString();

            }
            else
            {
                placeOfPostingTextBox.Text = "N/A";
            }


            if (dtdata.Rows[0]["CmpSkill"].ToString() != "")
            {
                cmpSkillsTextBox.Text = dtdata.Rows[0]["CmpSkill"].ToString();

            }
            else
            {
                cmpSkillsTextBox.Text = "N/A";
            }


            if (dtdata.Rows[0]["ProfCertification"].ToString() != "")
            {
                profCertificationTextBox.Text = dtdata.Rows[0]["ProfCertification"].ToString();

            }

            else
            {
                profCertificationTextBox.Text = "N/A";
            }
            txtOffice.Text = dtdata.Rows[0]["PlaceOffice"].ToString();


            //HFreportTo.Value = dtdata.Rows[0]["ReportingTo"].ToString();
            //LoadDeSignationEmpNAmeForUpdate(HFreportTo.Value);
            if (dtdata.Rows[0]["ReportingTo"].ToString() != "")
            {
                reportToTextBox.Text = dtdata.Rows[0]["ReportingTo"].ToString();

                if (reportToTextBox.Text == "Please Select an Employee.....")
                {
                    reportToTextBox.Text = "N/A";

                }

            }
            else
            {
                reportToTextBox.Text = "N/A";
            }

            divisionDropDownList.SelectedValue = dtdata.Rows[0]["DivisionId"].ToString();
            aEmployeeRequsitionDal.LoadWingsByDivision(divWingDropDownList, divisionDropDownList.SelectedValue);
            divWingDropDownList.SelectedValue = dtdata.Rows[0]["WingId"].ToString();
            aEmployeeRequsitionDal.LoadDepartmentByWings2(deptDropDownList, companyDropDownList.SelectedValue);

            deptDropDownList.SelectedValue = dtdata.Rows[0]["DeptId"].ToString();

            lbldeptDropDownList.Text = deptDropDownList.SelectedItem.Text;
            try
            {
                if ((dtdata.Rows[0].Field<DateTime>("ExpDateOfJoining").ToString() != null))
                {
                    expDtJoinTextBox.Text =
           dtdata.Rows[0].Field<DateTime>("ExpDateOfJoining").ToString("dd-MMM-yyyy");
                }
            }
            catch (Exception)
            {

                expDtJoinTextBox.Text = "N/A";
            }


            if (Convert.ToBoolean(dtdata.Rows[0]["IsBudgeted"]) == true)
            {
                isBudgetedCheckBox.Items[0].Selected = true;
                BudgetCodeDropDownList.Enabled = true;
                aEmployeeRequsitionDal.LoadCodeBudgetYearWise(BudgetCodeDropDownList, financialYearDropDownList.SelectedValue, companyDropDownList.SelectedValue, deptDropDownList.SelectedValue);
                BudgetCodeDropDownList.SelectedValue = dtdata.Rows[0]["BudgetId"].ToString();

                BudgetCodeDropDownList_SelectedIndexChanged(null, null);

                lblIsBudgeted.Text = "Yes";
                lblBudgetPosition.Text = BudgetCodeDropDownList.SelectedItem.Text;
            }

            else
            {
                isBudgetedCheckBox.Items[1].Selected = true;
                BudgetCodeDropDownList.SelectedValue = null;
                lblIsBudgeted.Text = "No";
            }
            //   expDtJoinTextBox.Text = Convert.ToDateTime(dtdata.Rows[0]["ExpDateOfJoining"]).ToString("dd-MMM-yyyy");

            DataTable dtedu = aEmployeeRequsitionDal.LoadReqEducationById(id);
            if (dtedu.Rows.Count>0)
            {
            educationGridView.DataSource = dtedu;
            educationGridView.DataBind();



            }


            DataTable AppLogComm = aJobReqFormBll.AppLogCommByJobReqId(Convert.ToInt32(id));

            if (AppLogComm.Rows.Count > 0)
            {
               
                AppLogCommentGridView.DataSource = AppLogComm;
                AppLogCommentGridView.DataBind();
            }

            DataTable dtkeyres = aEmployeeRequsitionDal.LoadKeyResponseById(id);
            if (dtkeyres.Rows.Count > 0)
            {
                KeyResponGridView.DataSource = dtkeyres;
                KeyResponGridView.DataBind();
            }

            if (Convert.ToBoolean(dtdata.Rows[0]["IsInternalCir"]) == true)
            {

                CheckBoxList1.Items[0].Selected = true;
            }

            if (Convert.ToBoolean(dtdata.Rows[0]["IsInternalCir"]) == true)
            {

                CheckBoxList1.Items[1].Selected = true;
            }

            if (Convert.ToBoolean(dtdata.Rows[0]["IsOnlineCir"]) == true)
            {

                CheckBoxList1.Items[2].Selected = true;
            }


            if (Convert.ToBoolean(dtdata.Rows[0]["IsSMCWeb"]) == true)
            {

                CheckBoxList1.Items[3].Selected = true;
            }

            if (Convert.ToBoolean(dtdata.Rows[0]["IsHeadHuntFirm"]) == true)
            {

                CheckBoxList1.Items[4].Selected = true;
            }


            otherTextBox.Text = dtdata.Rows[0]["OtherCircula"].ToString();

            // isBudgetedCheckBox.Checked = Convert.ToBoolean(dtdata.Rows[0]["IsBudgeted"].ToString());
            //for (int i = 0; i < isBudgetedCheckBox.Items.Count; i++)
            //{
            //    if (isBudgetedCheckBox.Items[i].Text.Trim() == "Yes")
            //    {
            //        isBudgetedCheckBox.Items[i].Selected = true;
            //   //     aEmployeeRequsitionDao.BudgetId = Convert.ToInt32(BudgetCodeDropDownList.SelectedValue);
            //        ShowBudgetInfo.Visible = true;
            //        aEmployeeRequsitionDal.LoadFinancialYear(financialYearDropDownList, companyDropDownList.SelectedValue);
            //        financialYearDropDownList.SelectedValue = dtdata.Rows[0]["FinancialYearId"].ToString();
            //    }
            //    else if (isBudgetedCheckBox.Items[i].Text.Trim() == "No")
            //    {
            //        isBudgetedCheckBox.Items[i].Selected = true;
            //    }

            //}

            //if (isBudgetedCheckBox.Checked)
            //{
            //    ShowBudgetCodeDiv.Visible = true;
            //    aEmployeeRequsitionDal.LoadFinancialYear(financialYearDropDownList, companyDropDownList.SelectedValue);
            //    financialYearDropDownList.SelectedValue = dtdata.Rows[0]["FinancialYearId"].ToString();
            //}
            //else
            //{
            //    financialYearDropDownList.Items.Clear();
            //}

            //if (financialYearDropDownList.SelectedValue != "")
            //{
            //    // aEmployeeRequsitionDal.LoadCodeBudget(BudgetCodeDropDownList);

            //}
            //else
            //{
            //    BudgetCodeDropDownList.Items.Clear();
            //}

            //if (isBudgetedCheckBox. == true)
            //{
            //    ShowBudgetCodeDiv.Visible = true;
            //    BudgetCodeDropDownList.SelectedValue = dtdata.Rows[0]["BudgetId"].ToString();

            //}

            //IsReplacementforCheckBox.Checked = Convert.ToBoolean(dtdata.Rows[0]["IsReplacement"].ToString());

            if ((bool)dtdata.Rows[0]["IsReplacement"])
            {
                for (int i = 0; i < jstRadioButtonList.Items.Count; i++)
                {
                    if (jstRadioButtonList.Items[i].Text.Trim() == "Replacement")
                    {
                        jstRadioButtonList.Items[i].Selected = true;


                    }

                    if (jstRadioButtonList.Items[i].Selected)
                    {
                        lbljstRadioButtonList.Text = "Replacement";
                        RepplaceView.Visible = true;
                        lblEmpName.Visible = true;
                        lblDatSep.Visible = true;
                        detail.Visible = true;

                        DateOfSeperationTextBox.Text = String.Empty;
                        EmployeeNameTextBox.Text = "";

                        codeLabel.Text = "";
                        nameLabel.Text = "";
                        desigLabel.Text = "";
                        salgradLabel.Text = "";
                        divLabel.Text = "";
                        //wingLabel.Text = dtdata.Rows[0]["DivisionWingName"].ToString();
                        //deptLabel.Text = dtdata.Rows[0]["DepartmentName"].ToString();
                        //secLabel.Text = dtdata.Rows[0]["SectionName"].ToString();
                        //subsecLabel.Text = dtdata.Rows[0]["SubSectionName"].ToString();
                        DateOfSeperationTextBox.Text = "";

                        //lblDatSep.Visible = true;
                        //lblEmpName.Visible = true;
                        //DateOfSeperationTextBox.Visible = true;
                        //DateOfSeperationTextBox.Text = Convert.ToDateTime(dtdata.Rows[0]["SeparationDate"]).ToString("dd-MMM-yyyy");
                        ////DateOfSeperationTextBox.Text = dtdata.Rows[0]["SeparationDate"].ToString(); 
                        //EmployeeNameTextBox.Text = dtdata.Rows[0]["EmpName"].ToString();
                        repEmpIdHiddenField.Value = dtdata.Rows[0]["ReplaceEmpId"].ToString();
                        EmployeeNameTextBox.Visible = true;


                        DataTable aDataTable = aEmployeeRequsitionDal.GetEmpData(repEmpIdHiddenField.Value);
                        if (aDataTable.Rows.Count > 0)
                        {
                            Showme.Visible = true;
                            codeLabel.Text = aDataTable.Rows[0]["EmpMasterCode"].ToString();
                            nameLabel.Text = aDataTable.Rows[0]["EmpName"].ToString();
                            desigLabel.Text = aDataTable.Rows[0]["Designation"].ToString();
                            salgradLabel.Text = aDataTable.Rows[0]["GradeName"].ToString();
                            divLabel.Text = aDataTable.Rows[0]["DivisionName"].ToString();

                            EmployeeNameTextBox.Text = aDataTable.Rows[0]["EmpName"].ToString();

                            //salgradLabel.Text = dtdata.Rows[0]["DepartmentName"].ToString();
                            //    divLabel.Text = dtdata.Rows[0]["DivisionName"].ToString();
                            //  wingLabel.Text = dtdata.Rows[0]["EmpType"].ToString();
                            wingLabel.Text = aDataTable.Rows[0]["EmpType"].ToString();
                            deptLabel.Text = aDataTable.Rows[0]["DepartmentName"].ToString();
                            //wingLabel.Text = dtdata.Rows[0]["DivisionWingName"].ToString();
                            //deptLabel.Text = dtdata.Rows[0]["DepartmentName"].ToString();
                            //secLabel.Text = dtdata.Rows[0]["SectionName"].ToString();
                            //subsecLabel.Text = dtdata.Rows[0]["SubSectionName"].ToString();
                            DateOfSeperationTextBox.Text =
                                string.IsNullOrEmpty(aDataTable.Rows[0]["JobLeftDate"].ToString()) ? "" : Convert.ToDateTime(aDataTable.Rows[0]["JobLeftDate"].ToString()).ToString("dd-MMM-yyyy");
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < jstRadioButtonList.Items.Count; i++)
                {
                    if (jstRadioButtonList.Items[i].Text.Trim() == "New")
                    {
                        lbljstRadioButtonList.Text = "New";
                        NewView.Visible = true;
                        jstRadioButtonList.Items[i].Selected = true;
                        Showme.Visible = false;

                        if (BudgetCodeDropDownList.SelectedValue != "")
                        {
                            DivProjecView.Visible = true;
                            BudgetCodeDropDownList_SelectedIndexChanged(null, null);
                        }

                    }
                }
            }

            //if (IsReplacementforCheckBox.Checked == true)
            //{


            //}

            OtherCheckBox.Checked = Convert.ToBoolean(dtdata.Rows[0]["IsOtherCircula"].ToString());
            if (OtherCheckBox.Checked == true)
            {
                otherTextBox.Visible = true;
                otherTextBox.Text = dtdata.Rows[0]["OtherCircula"].ToString();
            }
            else
            {
                otherTextBox.Text = "N/A";
            }


            if (dtdata.Rows[0]["Experience"].ToString() != "")
            {
                experienceTextBox.Text = dtdata.Rows[0]["Experience"].ToString();


            }
            else
            {
                experienceTextBox.Text = "N/A";
            }


            if (dtdata.Rows[0]["Skills"].ToString() != "")
            {
                skillTextBox.Text = dtdata.Rows[0]["Skills"].ToString();


            }
            else
            {
                skillTextBox.Text = "N/A";
            }


            if (dtdata.Rows[0]["Age"].ToString() != "")
            {
                ageTextBox.Text = dtdata.Rows[0]["Age"].ToString();


            }
            else
            {
                ageTextBox.Text = "N/A";
            }


            if (dtdata.Rows[0]["Age"].ToString() != "")
            {
                othersTextBox.Text = dtdata.Rows[0]["OtherExperience"].ToString();


            }
            else
            {
                othersTextBox.Text = "N/A";
            }
            
             

 
            if (dtdata.Rows[0]["IsManagementApproved"].ToString() == null)
            {
                IsMangAppCheckBox.Checked = false;
                lblIsManagaa.Text = "No";
            }
            else
            {
                IsMangAppCheckBox.Checked = Convert.ToBoolean(dtdata.Rows[0]["IsManagementApproved"].ToString());

                if (IsMangAppCheckBox.Checked)
                {
                    lblIsManagaa.Text = "Yes";
                }
                else
                {
                    lblIsManagaa.Text = "No";
                }
            }



            DataTable Office = aJobReqFormBll.OfficeByJobReqId(Convert.ToInt32(id));

            if (Office.Rows.Count > 0)
            {
                OfficeGridView.DataSource = Office;
                OfficeGridView.DataBind();
            }
            else
            {
                lblOfficeGridView.Text = "N/A";
            }



            DataTable keyresPon = aJobReqFormBll.JobCreationKeyRespon(Convert.ToInt32(id));

            if (keyresPon.Rows.Count > 0)
            {
                KeyResponGridView.DataSource = keyresPon;
                KeyResponGridView.DataBind();
            }
            else
            {
                lblKeyResponGridView.Text = "N/A";
            }



            DataTable educationalRequirements = aJobReqFormBll.JobEduReq(Convert.ToInt32(id));

            if (educationalRequirements.Rows.Count > 0)
            {
                EducationRequirementGridView.DataSource = educationalRequirements;
                EducationRequirementGridView.DataBind();
            }
            else
            {
                lblEducationRequirementGridView.Text = "N/A";

            }



            DataTable Requirements = aJobReqFormBll.EducationRequirementsDetail(Convert.ToInt32(id));

            if (Requirements.Rows.Count > 0)
            {
                DirectlySupervicesGridView.DataSource = Requirements;
                DirectlySupervicesGridView.DataBind();
            }
            else
            {
                lblDirectlySupervicesGridView.Text = "N/A";

            }



            DataTable OherRequirements = aJobReqFormBll.OtherRequirementsDetail(Convert.ToInt32(id));

            if (OherRequirements.Rows.Count > 0)
            {
                OtherRequirementsGridView.DataSource = OherRequirements;
                OtherRequirementsGridView.DataBind();
            }
            else
            {
                lblOtherRequirementsGridView.Text = "N/A";

            }



            DataTable aTable = aEmployeeRequsitionDal.GetJCPreferedWayOfCircular(empIdHiddenField.Value);

            //for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            //{
            //    CheckBoxList1.Items[i].Selected = false;
            //}

            if (aTable.Rows.Count > 0)
            {
                for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                {
                    for (int j = 0; j < aTable.Rows.Count; j++)
                    {
                        if (CheckBoxList1.Items[i].Value == aTable.Rows[j].Field<Int32>("WayId").ToString(CultureInfo.InvariantCulture))
                        {
                            CheckBoxList1.Items[i].Selected = true;

                            CheckBoxList1.Enabled = false;
                        }
                        else
                        {

                        }
                    }
                }
            }



        }
    }
    public void DropDownList()

    {

        asSectionInformationDal.GetCompanyListShortNameIntoDropdown(companyDropDownList);

        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_OnSelectedIndexChanged(null, null);
        aEmployeeRequsitionDal.GetSalaryLocationOnOfficeDdl(officeDropDownList);

        //aEmployeeRequsitionDal.LoadCodeBudget(BudgetCodeDropDownList);

        //aEmployeeRequsitionDal.LoadFinancialYear(financialYearDropDownList);

        //aEmployeeRequsitionDal.KeyResponsibilites(KeyResponsibilitesDropDownList.Text);
        aEmployeeRequsitionDal.EducationRequirementDropDownList(EducationRequirementDropDownList);


        aEmployeeRequsitionDal.LoadDesignation(jobtitleDropDownList);
        aEmployeeRequsitionDal.LoadDivision(divisionDropDownList);
        //aEmployeeRequsitionDal.LoadEmpInfo(empCodeDropDownList);
     
        aEmployeeRequsitionDal.GetEmpCategoryDDL(ddlEmpCategoryEx);
        
    }

    protected void divisionDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        aEmployeeRequsitionDal.LoadWingsByDivision(divWingDropDownList,divisionDropDownList.SelectedValue);
    }

    protected void divWingDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        aEmployeeRequsitionDal.LoadDepartmentByWings(deptDropDownList,divWingDropDownList.SelectedValue);
    }

    public void Clear()
    {
        try
        { 
        companyDropDownList.SelectedIndex = 0;
            codeLabel.Text = string.Empty;
            nameLabel.Text = string.Empty;
            desigLabel.Text = string.Empty;
            salgradLabel.Text = string.Empty;
            divLabel.Text = string.Empty;
            wingLabel.Text = string.Empty;
            deptLabel.Text = string.Empty;
            secLabel.Text = string.Empty;
            subsecLabel.Text = string.Empty;
        DateOfSeperationTextBox.Text =
            string.Empty;
            descriptionTextBox.Text = string.Empty;
        //divWingDropDownList.SelectedIndex = 0;

        jobtitleDropDownList.SelectedIndex = 0;
        gradeDropDownList.SelectedIndex = 0;
        nosTextBox.Text = string.Empty;
        NoteTextBox.Text = string.Empty;
        for (int i = 0; i < typeOfPosRadioButtonList.Items.Count; i++)
        {
            typeOfPosRadioButtonList.Items[i].Selected = false;
        }
        placeOfPostingTextBox.Text = string.Empty;
        reportToTextBox.Text = string.Empty;
     //   divisionDropDownList.SelectedIndex = 0;
        deptDropDownList.SelectedIndex = 0;
        expDtJoinTextBox.Text = string.Empty;
        loadGridView.DataSource = null;
        loadGridView.DataBind();


        KeyResponGridView.DataSource = null;
        KeyResponGridView.DataBind();

        EducationRequirementGridView.DataSource = null;
        EducationRequirementGridView.DataBind();

        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        {
            CheckBoxList1.Items[i].Selected = false;
        }
        otherTextBox.Text = string.Empty;
        //replaceForCheckBox.Checked = false;
        //empCodeDropDownList.SelectedIndex = 0;
        //IsReplacementforCheckBox.Checked = false;
        OtherCheckBox.Checked = false;
        //EmployeeDropDownList.SelectedValue = "";
        EmployeeNameTextBox.Text = "";
        RemarksTextBox.Text = string.Empty;
        //justficationTextBox.Text = string.Empty;
        educationGridView.DataSource = null;
        educationGridView.DataBind();
      
        //if (companyDropDownList.SelectedValue != "")
        {
            //lblEmpName.Visible = IsReplacementforCheckBox.Checked;
            //lblDatSep.Visible = IsReplacementforCheckBox.Checked;
            //detail.Visible = IsReplacementforCheckBox.Checked;

            //DateOfSeperationTextBox.Visible = IsReplacementforCheckBox.Checked;
            //EmployeeDropDownList.Visible = IsReplacementforCheckBox.Checked;
            //ShowReplacementfor.Visible = IsReplacementforCheckBox.Checked;
            //EmployeeNameTextBox.Visible = IsReplacementforCheckBox.Checked;

            DateOfSeperationTextBox.Text = String.Empty;
            //EmployeeDropDownList.SelectedValue = String.Empty;
            EmployeeNameTextBox.Text = "";
        }
        //else
        //{
        //    IsReplacementforCheckBox.Checked = false;
        //    ShowMessageBox("Please select a company first !!!");
        //}
        BudgetCodeDropDownList.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            
        }

    }
    public void Clear2()
    {
        try
        {

        
        
        //divWingDropDownList.SelectedIndex = 0;

        jobtitleDropDownList.SelectedIndex = 0;
        gradeDropDownList.SelectedIndex = 0;
        nosTextBox.Text = string.Empty;
        NoteTextBox.Text = string.Empty;
        for (int i = 0; i < typeOfPosRadioButtonList.Items.Count; i++)
        {
            typeOfPosRadioButtonList.Items[i].Selected = false;
        }
        placeOfPostingTextBox.Text = string.Empty;
        reportToTextBox.Text = string.Empty;
        //   divisionDropDownList.SelectedIndex = 0;
        deptDropDownList.SelectedIndex = 0;
        expDtJoinTextBox.Text = string.Empty;
        loadGridView.DataSource = null;
        loadGridView.DataBind();


        KeyResponGridView.DataSource = null;
        KeyResponGridView.DataBind();

        EducationRequirementGridView.DataSource = null;
        EducationRequirementGridView.DataBind();

        for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        {
            CheckBoxList1.Items[i].Selected = false;
        }
        otherTextBox.Text = string.Empty;
        //replaceForCheckBox.Checked = false;
        //empCodeDropDownList.SelectedIndex = 0;
        //IsReplacementforCheckBox.Checked = false;
        OtherCheckBox.Checked = false;
        //EmployeeDropDownList.SelectedValue = "";
        EmployeeNameTextBox.Text = "";
        RemarksTextBox.Text = string.Empty;
        //justficationTextBox.Text = string.Empty;
        educationGridView.DataSource = null;
        educationGridView.DataBind();
        //experienceTextBox.Text = string.Empty;
        //skillTextBox.Text = string.Empty;
        //ageTextBox.Text = string.Empty;
        //othersTextBox.Text = string.Empty;
        BudgetCodeDropDownList.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

            
        }

    }

    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public bool Validation()
    {


         

        int count = 0;

        for (int i = 0; i < actionRadioButtonList.Items.Count; i++)
        {
            if (actionRadioButtonList.Items[i].Selected)
            {
                count++;
            }

            if (count > 0)
            {
                break;
            }
        }

        if (count == 0)
        {
            ShowMessageBox("Please select Approval Status!!!!");
            actionRadioButtonList.Focus();
          
            return false;
        }
       


      


     




        return true;
    }

    public void Save(string acstatus)
    {
        if (Validation())
        {
            EmployeeRequsitionDAO aEmployeeRequsitionDao = new EmployeeRequsitionDAO();


            if (jobtitleDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDao.SupervisorId = Convert.ToInt32(jobtitleDropDownList.SelectedValue);
            }

            
            aEmployeeRequsitionDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
            //aEmployeeRequsitionDao.EmployeeId = Convert.ToInt32(EmployeeDropDownList.SelectedValue);
            //aEmployeeRequsitionDao.EmployeeId = Convert.ToInt32(repEmpIdHiddenField.Value);
            aEmployeeRequsitionDao.Note = NoteTextBox.Text.Trim();
            aEmployeeRequsitionDao.ReqCode = ReqCodetextBox.Text.Trim();
            aEmployeeRequsitionDao.ReqDate = Convert.ToDateTime(reqDateTextBox.Text);
            aEmployeeRequsitionDao.FinYearId = Convert.ToInt32(financialYearDropDownList.SelectedValue);
            aEmployeeRequsitionDao.GradeId = Convert.ToInt32(gradeDropDownList.SelectedValue);
            aEmployeeRequsitionDao.Nos = Convert.ToInt32(nosTextBox.Text.Trim());
         //   aEmployeeRequsitionDao.OtherExperience = othersTextBox.Text.Trim();


            //Newa
            aEmployeeRequsitionDao.JobSummery = descriptionTextBox.Text;
            aEmployeeRequsitionDao.JobTitle = jobTitleTextBox.Text;

            if (projectDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDao.ProjectId = Convert.ToInt32(projectDropDownList.SelectedValue);
            }

            aEmployeeRequsitionDao.EmployeeCategoryId = Convert.ToInt32(ddlEmpCategoryEx.SelectedValue);

            aEmployeeRequsitionDao.MonthContractual = Convert.ToString(MonthTextBox.Text);
            aEmployeeRequsitionDao.FundContractual = Convert.ToString(FundInfoTextBox.Text);

            if (IsMangAppCheckBox.Checked)
            {
                aEmployeeRequsitionDao.IsManagementApproved = true;
            }
            else
            {
                aEmployeeRequsitionDao.IsManagementApproved = false;
            }

            //aEmployeeRequsitionDao.SupervisorId = Convert.ToInt32(jobtitleDropDownList.SelectedValue);
            aEmployeeRequsitionDao.InternalContact = internalConTextBox.Text;
            aEmployeeRequsitionDao.ExternalContact = externalConTextBox.Text;

            if (officeDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDao.OfficeId = Convert.ToInt32(officeDropDownList.SelectedValue);
            }

            if (placeDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDao.PlaceId = Convert.ToInt32(placeDropDownList.SelectedValue);
            }

            aEmployeeRequsitionDao.ProfCertification = profCertificationTextBox.Text;
         //   aEmployeeRequsitionDao.CmpSkill = cmpSkillsTextBox.Text;

            //New

            if (typeOfPosRadioButtonList.Items[0].Selected == true)
            {
                aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[0].Value;
            }

            if (typeOfPosRadioButtonList.Items[1].Selected == true)
            {
                aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[1].Value;
            }

            aEmployeeRequsitionDao.PlaceOfPosting = placeOfPostingTextBox.Text.Trim();
            aEmployeeRequsitionDao.ReportingTo = HFreportTo.Value ;
         

            if (deptDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDao.DeptId = Convert.ToInt32(deptDropDownList.SelectedValue);
            }
                
                aEmployeeRequsitionDao.ExpDateOfJoining = Convert.ToDateTime(expDtJoinTextBox.Text.Trim());


                if (OtherCheckBox.Checked == true)
                {
                    aEmployeeRequsitionDao.OtherCircula = otherTextBox.Text.Trim();
                }

                
       
                //aEmployeeRequsitionDao.IsReplacement = IsReplacementforCheckBox.Checked;


                //if (IsReplacementforCheckBox.Checked==true)
                {
                    aEmployeeRequsitionDao.SeparationDate = DateOfSeperationTextBox.Text.Trim();
                    //aEmployeeRequsitionDao.ReplaceEmpId = Convert.ToInt32(EmployeeDropDownList.SelectedValue);
                    
                }

                for (int i = 0; i < jstRadioButtonList.Items.Count; i++)
                {
                    if (jstRadioButtonList.Items[i].Selected)
                    {
                        if (jstRadioButtonList.Items[i].Text.Trim() == "Replacement")
                        {
                            aEmployeeRequsitionDao.IsReplacement = true;
                            aEmployeeRequsitionDao.ReplaceEmpId = Convert.ToInt32(repEmpIdHiddenField.Value);
                        }
                    }
                }

                //else
                //{
                //    aEmployeeRequsitionDao.SeparationDate = "";
                //}

               


         

            //aEmployeeRequsitionDao.SeparationDate = string.IsNullOrEmpty(DateOfSeperationTextBox.Text) ? (DateTime?)null : DateTime.Parse(DateOfSeperationTextBox.Text); ;
                //aEmployeeRequsitionDao.ReplaceEmpId = Convert.ToInt32(empCodeDropDownList.SelectedValue)
          //    aEmployeeRequsitionDao.IsBudgeted = isBudgetedCheckBox.Items;
                aEmployeeRequsitionDao.Remarks = RemarksTextBox.Text;

            //if (isBudgetedCheckBox.Checked==true)
            //{
            //    aEmployeeRequsitionDao.BudgetId = Convert.ToInt32(BudgetCodeDropDownList.SelectedValue);
            //}


            
                if (isBudgetedCheckBox.Items[0].Selected==true)
                {
                    aEmployeeRequsitionDao.IsBudgeted = true;

                    aEmployeeRequsitionDao.BudgetId = Convert.ToInt32(BudgetCodeDropDownList.SelectedValue);
                  //  aEmployeeRequsitionDao.m = Convert.ToInt32(mainFinyearDropDownList.SelectedValue);
                }
                else if (isBudgetedCheckBox.Items[1].Selected == false)
                {
                    aEmployeeRequsitionDao.IsBudgeted = false;
                    
                }


            aEmployeeRequsitionDao.Justification = JustificationTextBox.Text;
                //SeparationDate = Convert.ToDateTime(seprationdateTextBox.Text),
                //aEmployeeRequsitionDao.Experience = experienceTextBox.Text;
                //aEmployeeRequsitionDao.Skills = skillTextBox.Text;
                //aEmployeeRequsitionDao.Age = ageTextBox.Text;
                //aEmployeeRequsitionDao.IsOtherCircula = CheckBoxList1.Items[4].Selected;
                aEmployeeRequsitionDao.OtherCircula = otherTextBox.Text;
                aEmployeeRequsitionDao.ActionStatus = acstatus;
                aEmployeeRequsitionDao.EntryBy = Convert.ToInt32(Session["UserId"]);
                

                aEmployeeRequsitionDao.EntryDate = DateTime.Now;
                
           

            int id = aEmployeeRequsitionDal.SaveEmpReq(aEmployeeRequsitionDao);
            if (id > 0)
            {


                for (int i = 0; i < OfficeGridView.Rows.Count; i++)
                {
                    OfficeLocationForRequisition OfficeLocation = new OfficeLocationForRequisition();
                    OfficeLocation.ReqMasterId = id;
                    OfficeLocation.SalaryLoationId = OfficeGridView.DataKeys[i][0].ToString() + ","  ;
                    OfficeLocation.SalaryLoationName = OfficeGridView.Rows[i].Cells[0].Text;
                    aEmployeeRequsitionDal.SaveOfficeLocation(OfficeLocation);
                }


                for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                {
                    JobReqKeyRespon aJobReqKeyRespon = new JobReqKeyRespon();

                    aJobReqKeyRespon.JobReqFormId = id;
                  
                    aJobReqKeyRespon.JobReqKeyResName = KeyResponGridView.Rows[i].Cells[0].Text;
                       
                  
                    aEmployeeRequsitionDal.SaveJobReqKeyRespon(aJobReqKeyRespon);
                }
                for (int i = 0; i < EducationRequirementGridView.Rows.Count; i++)
                {
                    EducationRequirDesJobReq aEducationRequirDesJobReq = new EducationRequirDesJobReq()
                    {
                        JobReqFormId = id,
                        EduRequirId = Convert.ToInt32(EducationRequirementGridView.DataKeys[i][0].ToString().Trim())

                    };
                    aEmployeeRequsitionDal.SaveEduRequirSave(aEducationRequirDesJobReq);
                }

                CirculationWayDao aDao;



                for (int i = 0; i < DirectlySupervicesGridView.Rows.Count; i++)
                {
                    EducationRequirementsDetailDao aEducationRequirementsDetailDao =
                        new EducationRequirementsDetailDao();
                    aEducationRequirementsDetailDao.MasterId = id;
                  aEducationRequirementsDetailDao.WayId   = Convert.ToInt32(DirectlySupervicesGridView.DataKeys[i][0].ToString().Trim());
                    aEducationRequirementsDetailDao.Nos = (
                        ((TextBox)DirectlySupervicesGridView.Rows[i].FindControl("DSNoTextBox")).Text);


                  aEducationRequirementsDetailDAL.EducationRequirementsDetailSave(aEducationRequirementsDetailDao);
                }


                for (int i = 0; i < OtherRequirementsGridView.Rows.Count; i++)
                {
                    OtherRequirementDetailDAO aOtherRequirementDetailDAO =
                        new OtherRequirementDetailDAO();
                    aOtherRequirementDetailDAO.MasterId = id;
                    aOtherRequirementDetailDAO.OtherRequirement = OtherRequirementsGridView.Rows[i].Cells[0].Text;


                    aOtherRequirementDetailDAL.OtherRequirementsDetailSave(aOtherRequirementDetailDAO);
                }

             //   EducationRequirementsDetailDao aDetailDao;

                if (id > 0)
                {

                    for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                    {
                        aDao = new CirculationWayDao();


                        if (CheckBoxList1.Items[i].Selected)
                        {
                             aDao.MasterId = id;
                             aDao.WayId = Convert.ToInt32(CheckBoxList1.Items[i].Value);
                             aEmployeeRequsitionDal.SaveCirculationWayDetail(aDao);
                        }

                    }



                    try
                    {
                        if (Session["EmpInfoid"].ToString()!="")
                        {
                            JobReqFormAppLogDAO appLogDao = new JobReqFormAppLogDAO();

                            appLogDao.ActionStatus = "Drafted";
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                            appLogDao.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                            appLogDao.JobReqId = id;
                            appLogDao.Comments = "";

                            int idd = aEmployeeRequsitionDal.SavAppLog(appLogDao);
                        }
                       
                    }
                    catch (Exception)
                    {
                        
                        //throw;
                    }

                          ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfully Done!');window.location ='JobRequisitionFormView.aspx';",
                    true);                 
                }
             
                //ShowMessageBox("Data Saved Successfully !!!");
                MethodAutoIncri();
                Clear();

            }
        }
    }

    protected void adddImageButton_Click(object sender, ImageClickEventArgs e)
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("KeyRespon");

        DataRow dataRow = null;
        if (loadGridView.Rows.Count > 0)
        {
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                dataRow = aDataTable.NewRow();

                dataRow["KeyRespon"] =
                    ((TextBox)loadGridView.Rows[i].Cells[1].FindControl("keyresponsTextBox")).Text.Trim();

                dataRow["JobReqKeyResNameTextBox"] =
                    ((TextBox)loadGridView.Rows[i].Cells[1].FindControl("JobReqKeyResNameTextBox")).Text.Trim();

                aDataTable.Rows.Add(dataRow);
            }
        }
        dataRow = aDataTable.NewRow();


        dataRow["KeyRespon"] = "";

        aDataTable.Rows.Add(dataRow);

        loadGridView.DataSource = null;
        loadGridView.DataBind();
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();
    }

    protected void delImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        ImageButton productCodeTextBox = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)productCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("KeyRespon");

        DataRow dataRow = null;

        if (loadGridView.Rows.Count > 0)
        {
            int sl1 = 1;
            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();

                    dataRow["KeyRespon"] =
                        ((TextBox)loadGridView.Rows[i].Cells[1].FindControl("keyresponsTextBox")).Text.Trim();

                    aDataTable.Rows.Add(dataRow);
                }
            }
        }

        loadGridView.DataSource = null;
        loadGridView.DataBind();
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();
    }

    public void Update(string ststus)
    {
        if (Validation())
        {
          

            EmployeeRequsitionDAO aEmployeeRequsitionDao = new EmployeeRequsitionDAO();


            aEmployeeRequsitionDao.JobReqId = Convert.ToInt32(empIdHiddenField.Value);
            if (jobtitleDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDao.SupervisorId = Convert.ToInt32(jobtitleDropDownList.SelectedValue);
            }


            aEmployeeRequsitionDao.CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue);
            //aEmployeeRequsitionDao.EmployeeId = Convert.ToInt32(EmployeeDropDownList.SelectedValue);
            //  aEmployeeRequsitionDao.EmployeeId = Convert.ToInt32(repEmpIdHiddenField.Value);
            aEmployeeRequsitionDao.Note = NoteTextBox.Text.Trim();
            aEmployeeRequsitionDao.ReqCode = ReqCodetextBox.Text.Trim();
            aEmployeeRequsitionDao.ReqDate = Convert.ToDateTime(reqDateTextBox.Text);
            aEmployeeRequsitionDao.FinYearId = Convert.ToInt32(financialYearDropDownList.SelectedValue);
            aEmployeeRequsitionDao.GradeId = Convert.ToInt32(gradeDropDownList.SelectedValue);
            aEmployeeRequsitionDao.Nos = Convert.ToInt32(nosTextBox.Text.Trim());
           // aEmployeeRequsitionDao.OtherExperience = othersTextBox.Text.Trim();


            //New
            aEmployeeRequsitionDao.JobSummery = descriptionTextBox.Text;
            aEmployeeRequsitionDao.Justification = JustificationTextBox.Text;
            aEmployeeRequsitionDao.JobTitle = jobTitleTextBox.Text;
            aEmployeeRequsitionDao.ActionStatus = ststus;

            if (projectDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDao.ProjectId = Convert.ToInt32(projectDropDownList.SelectedValue);
            }

            aEmployeeRequsitionDao.EmployeeCategoryId = Convert.ToInt32(ddlEmpCategoryEx.SelectedValue);

            aEmployeeRequsitionDao.MonthContractual = Convert.ToString(MonthTextBox.Text);
            aEmployeeRequsitionDao.FundContractual = Convert.ToString(FundInfoTextBox.Text);

            if (IsMangAppCheckBox.Checked)
            {
                aEmployeeRequsitionDao.IsManagementApproved = true;
            }
            else
            {
                aEmployeeRequsitionDao.IsManagementApproved = false;
            }

            //aEmployeeRequsitionDao.SupervisorId = Convert.ToInt32(jobtitleDropDownList.SelectedValue);
            aEmployeeRequsitionDao.InternalContact = internalConTextBox.Text;
            aEmployeeRequsitionDao.ExternalContact = externalConTextBox.Text;

            if (officeDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDao.OfficeId = Convert.ToInt32(officeDropDownList.SelectedValue);
            }

            if (placeDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDao.PlaceId = Convert.ToInt32(placeDropDownList.SelectedValue);
            }

            aEmployeeRequsitionDao.ProfCertification = profCertificationTextBox.Text;
           // aEmployeeRequsitionDao.CmpSkill = cmpSkillsTextBox.Text;

            //New

            if (typeOfPosRadioButtonList.Items[0].Selected == true)
            {
                aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[0].Value;
            }

            if (typeOfPosRadioButtonList.Items[1].Selected == true)
            {
                aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[1].Value;
            }

      
            aEmployeeRequsitionDao.PlaceOfPosting = placeOfPostingTextBox.Text.Trim();
            aEmployeeRequsitionDao.ReportingTo = HFreportTo.Value;
         
            if (deptDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDao.DeptId = Convert.ToInt32(deptDropDownList.SelectedValue);
            }

            aEmployeeRequsitionDao.ExpDateOfJoining = Convert.ToDateTime(expDtJoinTextBox.Text.Trim());


            if (OtherCheckBox.Checked == true)
            {
                aEmployeeRequsitionDao.OtherCircula = otherTextBox.Text.Trim();
            }


       
            //if (IsReplacementforCheckBox.Checked==true)
            {
                aEmployeeRequsitionDao.SeparationDate = DateOfSeperationTextBox.Text.Trim();
                //aEmployeeRequsitionDao.ReplaceEmpId = Convert.ToInt32(EmployeeDropDownList.SelectedValue);

            }

            for (int i = 0; i < jstRadioButtonList.Items.Count; i++)
            {
                if (jstRadioButtonList.Items[i].Selected)
                {
                    if (jstRadioButtonList.Items[i].Text.Trim() == "Replacement")
                    {
                        aEmployeeRequsitionDao.IsReplacement = true;
                        aEmployeeRequsitionDao.ReplaceEmpId = Convert.ToInt32(repEmpIdHiddenField.Value);
                    }
                }
            }

       
            aEmployeeRequsitionDao.Remarks = RemarksTextBox.Text;
           
          
            //aEmployeeRequsitionDao.Experience = experienceTextBox.Text;
            //aEmployeeRequsitionDao.Skills = skillTextBox.Text;
            //aEmployeeRequsitionDao.Age = ageTextBox.Text;
            ////aEmployeeRequsitionDao.IsOtherCircula = CheckBoxList1.Items[4].Selected;
            aEmployeeRequsitionDao.OtherCircula = otherTextBox.Text;


            
            aEmployeeRequsitionDao.UpdateBy = Convert.ToInt32(Session["UserId"]);
            

           
            aEmployeeRequsitionDao.UpdateDate = DateTime.Now;
            
            //int id = aEmployeeRequsitionDal.SaveEmpReq(aEmployeeRequsitionDao);


            bool status = aEmployeeRequsitionDal.UpdateEmpReq(aEmployeeRequsitionDao);
            if (status)
            {


                aEmployeeRequsitionDal.DelOfficeDetail(empIdHiddenField.Value);

                for (int i = 0; i < OfficeGridView.Rows.Count; i++)
                {
                    OfficeLocationForRequisition OfficeLocation = new OfficeLocationForRequisition();
                    OfficeLocation.ReqMasterId = Convert.ToInt32(empIdHiddenField.Value);
                    OfficeLocation.SalaryLoationId = OfficeGridView.DataKeys[i][0].ToString() + ",";
                    OfficeLocation.SalaryLoationName = OfficeGridView.Rows[i].Cells[0].Text;
                    aEmployeeRequsitionDal.SaveOfficeLocation(OfficeLocation);
                }


                aEmployeeRequsitionDal.DelOtherRequirementDetail(empIdHiddenField.Value);


                for (int i = 0; i < OtherRequirementsGridView.Rows.Count; i++)
                {
                    OtherRequirementDetailDAO aOtherRequirementDetailDAO =
                        new OtherRequirementDetailDAO();
                    aOtherRequirementDetailDAO.MasterId = Convert.ToInt32(empIdHiddenField.Value);
                    aOtherRequirementDetailDAO.OtherRequirement = OtherRequirementsGridView.Rows[i].Cells[0].Text;


                    aOtherRequirementDetailDAL.OtherRequirementsDetailSave(aOtherRequirementDetailDAO);
                }


                aEmployeeRequsitionDal.DelEducationRequirementsDetail(empIdHiddenField.Value);

                for (int i = 0; i < DirectlySupervicesGridView.Rows.Count; i++)
                {
                    EducationRequirementsDetailDao aEducationRequirementsDetailDao =
                        new EducationRequirementsDetailDao();
                    aEducationRequirementsDetailDao.MasterId = Convert.ToInt32(empIdHiddenField.Value);
                    aEducationRequirementsDetailDao.WayId = Convert.ToInt32(DirectlySupervicesGridView.DataKeys[i][0].ToString().Trim());
                    aEducationRequirementsDetailDao.Nos = (
                      ((TextBox)DirectlySupervicesGridView.Rows[i].FindControl("DSNoTextBox")).Text);


                    aEducationRequirementsDetailDAL.EducationRequirementsDetailSave(aEducationRequirementsDetailDao);
                }


                aEmployeeRequsitionDal.DelJobReqKeyRespon(empIdHiddenField.Value);

                for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                {
                    JobReqKeyRespon aJobReqKeyRespon = new JobReqKeyRespon();

                    aJobReqKeyRespon.JobReqFormId = Convert.ToInt32(empIdHiddenField.Value);
                    //aJobReqKeyRespon.KeyResId = Convert.ToInt32(KeyResponGridView.DataKeys[i][0].ToString().Trim());
                    aJobReqKeyRespon.JobReqKeyResName = KeyResponGridView.Rows[i].Cells[0].Text;


                    aEmployeeRequsitionDal.SaveJobReqKeyRespon(aJobReqKeyRespon);
                }

                aEmployeeRequsitionDal.DelJobReqEduREqui(empIdHiddenField.Value);

                for (int i = 0; i < EducationRequirementGridView.Rows.Count; i++)
                {
                    EducationRequirDesJobReq aEducationRequirDesJobReq = new EducationRequirDesJobReq()
                    {
                        JobReqFormId = Convert.ToInt32(empIdHiddenField.Value),
                        EduRequirId = Convert.ToInt32(EducationRequirementGridView.DataKeys[i][0].ToString().Trim())

                    };
                    aEmployeeRequsitionDal.SaveEduRequirSave(aEducationRequirDesJobReq);
                }

                CirculationWayDao aDao;

                //aEmployeeRequsitionDal.Delete

                aEmployeeRequsitionDal.DeletePreferedWayOfCircular(empIdHiddenField.Value);

                if (status)
                {
                    for (int i = 0; i < CheckBoxList1.Items.Count; i++)
                    {
                        aDao = new CirculationWayDao();


                        if (CheckBoxList1.Items[i].Selected)
                        {
                            aDao.MasterId = Convert.ToInt32(empIdHiddenField.Value);
                            aDao.WayId = Convert.ToInt32(CheckBoxList1.Items[i].Value);
                            aEmployeeRequsitionDal.SaveCirculationWayDetail(aDao);
                        }

                    }



                    JobReqFormAppLogDAO appLogDao = new JobReqFormAppLogDAO();

                    appLogDao.ActionStatus = "Drafted";
                    appLogDao.ApproveDate = DateTime.Now;
                    appLogDao.ApproveBy = Session["UserId"].ToString();
                    appLogDao.PreEmpInfoId = Convert.ToInt32(0);
                    appLogDao.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoid"].ToString());
                    appLogDao.JobReqId = Convert.ToInt32(empIdHiddenField.Value);
                    appLogDao.Comments = " ";




                    int idd = aEmployeeRequsitionDal.SavAppLog(appLogDao);

                }

                //ShowMessageBox("Data Updated Successfully !!!");
                //Clear();
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Updated Successfully...');window.location ='JobRequisitionFormView.aspx';",
                   true);
                //ShowMessageBox("Data Saved Successfully !!!");
                MethodAutoIncri();
                Clear();

                //{
                //    JobReqId = Convert.ToInt32(empIdHiddenField.Value),
                //    CompanyId = Convert.ToInt32(companyDropDownList.SelectedValue),
                //    JobTitleId = Convert.ToInt32(jobtitleDropDownList.SelectedValue),
                //    Note = Convert.ToString(NoteTextBox.Text),

                //    GradeId = Convert.ToInt32(gradeDropDownList.SelectedValue),
                //    Nos = Convert.ToInt32(nosTextBox.Text),

                //    OtherExperience = othersTextBox.Text,
                //    IsPermanent = typeOfPosRadioButtonList.Items[0].Selected,
                //    IsContract = typeOfPosRadioButtonList.Items[1].Selected,
                //    IsCasual = typeOfPosRadioButtonList.Items[2].Selected,
                //    IsOther = typeOfPosRadioButtonList.Items[3].Selected,
                //    PlaceOfPosting = placeOfPostingTextBox.Text,
                //    ReportingTo = reportToTextBox.Text,
                //    DivisionId = Convert.ToInt32(divisionDropDownList.SelectedValue),
                //    WingId = Convert.ToInt32(divWingDropDownList.SelectedValue),

                //    DeptId = Convert.ToInt32(deptDropDownList.SelectedValue),
                //    ExpDateOfJoining = Convert.ToDateTime(expDtJoinTextBox.Text),
                //    IsInternalCir = CheckBoxList1.Items[0].Selected,
                //    IsOnlineCir = CheckBoxList1.Items[1].Selected,
                //    IsSMCWeb = CheckBoxList1.Items[2].Selected,
                //    IsNewsPaper = CheckBoxList1.Items[3].Selected,
                //    IsHeadHuntFirm = CheckBoxList1.Items[3].Selected,
                //    Others = othersTextBox.Text,
                //    //IsReplacement = replaceForCheckBox.Checked,
                //    //ReplaceEmpId = Convert.ToInt32(empCodeDropDownList.SelectedValue),
                //    //IsBudgeted = CheckBox1.Checked,
                //    //Justification = justficationTextBox.Text,
                //    //SeparationDate = Convert.ToDateTime(seprationdateTextBox.Text),
                //    Experience = experienceTextBox.Text,
                //    Skills = skillTextBox.Text,
                //    Age = ageTextBox.Text,
                //    IsOtherCircula = CheckBoxList1.Items[4].Selected,
                //    OtherCircula = otherTextBox.Text,


                //};
                //bool id = aEmployeeRequsitionDal.UpdateEmpReq(aEmployeeRequsitionDao);
                //if (id == true)
                //{

                //    aEmployeeRequsitionDal.DelEmpReqEducation(empIdHiddenField.Value);
                //    aEmployeeRequsitionDal.DelKeyResponsbility(empIdHiddenField.Value);
                //    for (int i = 0; i < loadGridView.Rows.Count; i++)
                //    {
                //        KeyResponsibilityDAO aKeyResponsibilityDao = new KeyResponsibilityDAO()
                //        {
                //            JobReqId = Convert.ToInt32(empIdHiddenField.Value),
                //            KeyResponsibility =
                //                ((TextBox)loadGridView.Rows[i].Cells[1].FindControl("keyresponsTextBox")).Text.Trim()
                //        };
                //        //aEmployeeRequsitionDal.SaveKeyResponsbility(aKeyResponsibilityDao);
                //    }
                //    for (int i = 0; i < educationGridView.Rows.Count; i++)
                //    {
                //        ReqEducationDAO aReqEducationDao = new ReqEducationDAO()
                //        {
                //            JobReqId = Convert.ToInt32(empIdHiddenField.Value),
                //            Education =
                //                ((TextBox)educationGridView.Rows[i].Cells[1].FindControl("educationTextBox")).Text.Trim()
                //        };
                //        aEmployeeRequsitionDal.SaveEmpReqEducation(aReqEducationDao);
                //    }
                //    ShowMessageBox("Data Update Successfully !!!");
                //    Clear();

                //}
            }
        }
    }


    public void MethodAutoIncri()
    {
        //using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SolutionConnectionStringHRDB"].ConnectionString))
        //{
        //    string s = "select max(JobReqId)+1 from tblJobReqForm";
        //    SqlCommand csm = new SqlCommand(s, connection);

        //    connection.Open();
        //    csm.ExecuteNonQuery();

        //    SqlDataReader dd = csm.ExecuteReader();

        //    while (dd.Read())
        //    {
        //       int n =  dd.GetInt32(0);
        //       ReqCodetextBox.Text =   ReqnNoGenerator(n);
        //    }

        //    connection.Close();
        //}

        DataTable dt = aJobReqFormBll.GetId();
        ReqCodetextBox.Text = ReqnNoGenerator(Convert.ToInt32(dt.Rows[0][0].ToString()));
    }


    private string ReqnNoGenerator(int id)
    {
        string code = string.Empty;
        string Id = id.ToString();

        if (Id.Length == 1)
        {
            Id = "000000" + Id;
        }
        if (Id.Length == 2)
        {
            Id = "00000" + Id;
        }
        if (Id.Length == 3)
        {
            Id = "0000" + Id;
        }
        if (Id.Length == 4)
        {
            Id = "000" + Id;
        }
        if (Id.Length == 5)
        {
            Id = "00" + Id;
        }
        if (Id.Length == 6)
        {
            Id = "0" + Id;
        }
        code = "REQ-" + Id;
        return code;
    }

    protected void Button2_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {

            if (valiHRR())
            {


                EmployeeRequsitionDAO aMaster = new EmployeeRequsitionDAO();
                aMaster.JobReqId
                    = Convert.ToInt32(empIdHiddenField.Value);
                aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
                bool status = aEmployeeRequsitionDal.UpdateContractural(aMaster.ActionStatus,
                    Convert.ToInt32(empIdHiddenField.Value));
                if (status)
                {
                    int commentid = aEmployeeRequsitionDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                        commentsTextBox.Text);
                    if (aMaster.ActionStatus == "Verified")
                    {
                        DataTable dtempdata =
                            aEmployeeRequsitionDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() +
                                                              "'");
                        JobReqFormAppLogDAO appLogDao = new JobReqFormAppLogDAO();
                        {
                            appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                            appLogDao.JobReqId = aMaster.JobReqId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;

                        }
                        ;
                        int id = aEmployeeRequsitionDal.SavAppLog(appLogDao);
                        aEmployeeRequsitionDal.UpdateJobReqStatus2(aMaster);

                        SenMailForApprved(appLogDao.ForEmpInfoId, " Employee Requisition Form Approval ",
                            @"  <br/> Dear Sir, <br/>
An employee requision is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                    }
                    else if (aMaster.ActionStatus == "Approved")
                    {
                        int empid = 0;
                        DataTable dtempdata =
                            aEmployeeRequsitionDal.GetHRAdminEmployeeAppId(" WHERE URL='" +
                                                                           Session["AppPage"].ToString() +
                                                                           "' AND Serial='1'" +
                                                                       "   AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' ");
                        if (dtempdata.Rows.Count > 0)
                        {
                            empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                        }
                        JobReqFormAppLogDAO appLogDao = new JobReqFormAppLogDAO();
                        {
                            appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                            appLogDao.ApproveDate = DateTime.Now;
                            appLogDao.ApproveBy = Session["UserId"].ToString();
                            appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                            appLogDao.ForEmpInfoId = empid;
                            appLogDao.JobReqId = aMaster.JobReqId;
                            appLogDao.Comments = commentsTextBox.Text;
                            appLogDao.CommentsId = commentid;
                        }
                        ;
                        aMaster.ActionStatus = "Verified";
                        aEmployeeRequsitionDal.UpdateJobReqStatus2(aMaster);
                        int id = aEmployeeRequsitionDal.SavAppLog(appLogDao);

                        SenMailForApprved(appLogDao.ForEmpInfoId, " Employee Requisition Form Approval ",
                    @"  <br/> Dear Sir, <br/>
An employee requision is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
                    }
                    else if (aMaster.ActionStatus == "Review")
                    {
                        DataTable dtempdata = aEmployeeRequsitionDal.GetEmpInfoPrevious(
                            Session["EmpInfoid"].ToString(), empIdHiddenField.Value);
                        DataTable dtempdata2 =
                            aEmployeeRequsitionDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(),
                                empIdHiddenField.Value);

                        if (dtempdata2.Rows.Count > 0)
                        {
                            JobReqFormAppLogDAO appLogDao = new JobReqFormAppLogDAO();
                            {
                                appLogDao.ActionStatus = "Verified";
                                appLogDao.ApproveDate = DateTime.Now;
                                appLogDao.ApproveBy = Session["UserId"].ToString();
                                appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString());
                                appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString());
                                appLogDao.JobReqId = aMaster.JobReqId;
                                appLogDao.Comments = commentsTextBox.Text;
                                appLogDao.CommentsId = commentid;
                            }

                            aEmployeeRequsitionDal.UpdateAppLog("Review", Session["AppLogId"].ToString());
                            int id = aEmployeeRequsitionDal.SavAppLog(appLogDao);
                            aEmployeeRequsitionDal.UpdateJobReqStatus2(aMaster);
                        }
                        else
                        {
                            
                                ShowMessageBox("Please select Approval Status Approved  this!!!");
                             

                        }


                        DataTable dtdata = aEmployeeRequsitionDal.GetDataReviewEntryBy(
                      empIdHiddenField.Value, Session["UserId"].ToString(), "Review");
                        if (dtdata.Rows.Count > 0)
                        {
                            Session["Status"] = "Edit";
                            Session["JobReqId"] = "";
                            Session["JobReqId"] = empIdHiddenField.Value;
                            Response.Redirect("JobRequisitionForm.aspx");
                        }

                    }


                }
                Session["AppLogId"] = null;
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfully Done!');window.location ='JobRequisitionFormAproval.aspx';",
                    true);
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

          if (GetMailAddress!=null)
            {
                ForMailAddress = GetMailAddress.OfficialEmail;
            }

           

        }

        if (ForMailAddress!="")
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

    protected void showMessageBox(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public void LoadEduGrid()
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("Education");

        DataRow dataRow = null;
        dataRow = aDataTable.NewRow();


        dataRow["Education"] = "";
        aDataTable.Rows.Add(dataRow);

        educationGridView.DataSource = null;
        educationGridView.DataBind();
        educationGridView.DataSource = aDataTable;
        educationGridView.DataBind();
    }

    public void LoadKeyResponse()
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("JobReqKeyResName");

        DataRow dataRow = null;
        dataRow = aDataTable.NewRow();
        dataRow["JobReqKeyResName"] = "";

        aDataTable.Rows.Add(dataRow);

        loadGridView.DataSource = null;
        loadGridView.DataBind();
        loadGridView.DataSource = aDataTable;
        loadGridView.DataBind();
    }

    protected void addeduImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("Education");

        DataRow dataRow = null;
        if (educationGridView.Rows.Count > 0)
        {
            for (int i = 0; i < educationGridView.Rows.Count; i++)
            {
                dataRow = aDataTable.NewRow();

                dataRow["Education"] =
                    ((TextBox)educationGridView.Rows[i].Cells[1].FindControl("educationTextBox")).Text.Trim();

                aDataTable.Rows.Add(dataRow);
            }
        }
        dataRow = aDataTable.NewRow();


        dataRow["Education"] = "";

        aDataTable.Rows.Add(dataRow);

        educationGridView.DataSource = null;
        educationGridView.DataBind();
        educationGridView.DataSource = aDataTable;
        educationGridView.DataBind();
    }

    protected void deleduImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        ImageButton productCodeTextBox = (ImageButton)sender;
        GridViewRow currentRow = (GridViewRow)productCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        DataTable aDataTable = new DataTable();
        aDataTable.Columns.Add("Education");

        DataRow dataRow = null;

        if (educationGridView.Rows.Count > 0)
        {
            int sl1 = 1;
            for (int i = 0; i < educationGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();

                    dataRow["Education"] =
                        ((TextBox)educationGridView.Rows[i].Cells[1].FindControl("educationTextBox")).Text.Trim();

                    aDataTable.Rows.Add(dataRow);
                }
            }
        }

        educationGridView.DataSource = null;
        educationGridView.DataBind();
        educationGridView.DataSource = aDataTable;
        educationGridView.DataBind();
    }

    protected void isBudgetedCheckBox_CheckedChanged(object sender, EventArgs e)
    {


        if (companyDropDownList.SelectedValue == "")
        
      
        {
            ShowMessageBox("Please Select a company first !!!!");
            isBudgetedCheckBox.Items[0].Selected = false;
            isBudgetedCheckBox.Items[1].Selected = false;
        }

        if (isBudgetedCheckBox.Items[0].Selected==true)
        {
            BudgetCodeDropDownList.Enabled = true;

        }
        else
        {
            BudgetCodeDropDownList.Enabled = false;
        }
       

}
    protected void IsReplacementforCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedValue != "")
        {
            //lblEmpName.Visible = IsReplacementforCheckBox.Checked;
            //lblDatSep.Visible = IsReplacementforCheckBox.Checked;
            //detail.Visible = IsReplacementforCheckBox.Checked;

            //DateOfSeperationTextBox.Visible = IsReplacementforCheckBox.Checked;
            //EmployeeDropDownList.Visible = IsReplacementforCheckBox.Checked;
            //ShowReplacementfor.Visible = IsReplacementforCheckBox.Checked;
            //EmployeeNameTextBox.Visible = IsReplacementforCheckBox.Checked;

            DateOfSeperationTextBox.Text = String.Empty;
            //EmployeeDropDownList.SelectedValue = String.Empty;
            EmployeeNameTextBox.Text = "";
        }
        else
        {
            //IsReplacementforCheckBox.Checked = false;
            ShowMessageBox("Please select a company first !!!");
        }


    }
    
    protected void OtherCheckBox_CheckedChanged(object sender, EventArgs e)
    {

        otherTextBox.Visible = OtherCheckBox.Checked;
        LblOther.Visible = OtherCheckBox.Checked;
        otherTextBox.Text = String.Empty;

    }

    private bool AddEventValidation()
    {

        int count = 0;

        for (int i = 0; i < jdCheckBoxList.Items.Count; i++)
        {
            if (jdCheckBoxList.Items[i].Selected)
            {
                count ++;
            }

            if (count > 0)
            {
                break;
            }
        }

        if (count == 0)
        {
            ShowMessageBox("Please Select at least one JD !!!");
            return false;
        }

        return true;
    }
    protected void addImageButton_OnClick(object sender, ImageClickEventArgs e)
    {

               
}

    private bool CheckItemAlreadyExistOrNot(int i)
    {
        for (int j = 0; j < KeyResponGridView.Rows.Count; j++)
        {
            if (KeyResponGridView.Rows[j].Cells[0].Text == jdCheckBoxList.Items[i].Text)
            {
                return false;
            }
        }

        return true;
    }

    


    protected void deleteImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("JobReqKeyResName");

        DataRow dataRow;

        if (KeyResponGridView.Rows.Count > 0)
        {
            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["JobReqKeyResName"] = KeyResponGridView.Rows[i].Cells[0].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }


        KeyResponGridView.DataSource = aDataTable;
        KeyResponGridView.DataBind();


        // SetState();

    }
    protected void EducationRequirementImageButton_Click(object sender, EventArgs e)
    {
        if (AddEducationRequirementValidation())
        {
            string eRID = EducationRequirementDropDownList.SelectedValue;
            string educationRequirements = EducationRequirementDropDownList.SelectedItem.Text.Trim();

            var aDataTable = new DataTable();

            aDataTable.Columns.Add("ERID");
            aDataTable.Columns.Add("EducationRequirements");



            DataRow dataRow;

            if (EducationRequirementGridView.Rows.Count > 0)
            {
                for (int i = 0; i < EducationRequirementGridView.Rows.Count; i++)
                {
                    if (EducationRequirementGridView.Rows[i].Cells[0].Text != educationRequirements)
                    {
                        dataRow = aDataTable.NewRow();

                        dataRow["ERID"] = EducationRequirementGridView.DataKeys[i][0].ToString();
                        dataRow["EducationRequirements"] = EducationRequirementGridView.Rows[i].Cells[0].Text;

                        aDataTable.Rows.Add(dataRow);
                        EducationRequirementDropDownList.SelectedValue = "";
                    }

                    else
                    {
                        EducationRequirementDropDownList.SelectedValue = "";
                        ShowMessageBox("Data already Exist !!");
                    }
                }

                dataRow = aDataTable.NewRow();

                dataRow["ERID"] = eRID;
                dataRow["EducationRequirements"] = educationRequirements;

                aDataTable.Rows.Add(dataRow);

                EducationRequirementGridView.DataSource = aDataTable;
                EducationRequirementGridView.DataBind();

                EducationRequirementDropDownList.SelectedValue = "";

            }


            else
            {
                dataRow = aDataTable.NewRow();

                dataRow["ERID"] = eRID;
                dataRow["EducationRequirements"] = educationRequirements;

                aDataTable.Rows.Add(dataRow);

                EducationRequirementGridView.DataSource = aDataTable;
                EducationRequirementGridView.DataBind();

                EducationRequirementDropDownList.SelectedValue = "";
            }
        }

    }

    private bool AddEducationRequirementValidation()
    {
        if (EducationRequirementDropDownList.SelectedValue == "")
        {
            ShowMessageBox("Please select Education Requirement !!!");
            return false;
        }

        return true;
    }

    protected void deleteImageButtonEducationRequirement_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("ERID");
        aDataTable.Columns.Add("EducationRequirements");

        DataRow dataRow;

        if (EducationRequirementGridView.Rows.Count > 0)
        {
            for (int i = 0; i < EducationRequirementGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();

                    dataRow["ERID"] = EducationRequirementGridView.DataKeys[i][0].ToString();
                    dataRow["EducationRequirements"] = EducationRequirementGridView.Rows[i].Cells[0].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }


        EducationRequirementGridView.DataSource = aDataTable;
        EducationRequirementGridView.DataBind();


        // SetState();
        
    }
    protected void ViewListButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("JobRequisitionFormView.aspx");
    }

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
     
        if (companyDropDownList.SelectedValue != "")
        {
            aEmployeeRequsitionDal.LoadFinancialYear(financialYearDropDownList,
                       companyDropDownList.SelectedValue);
            aEmployeeRequsitionDal.LoadDepartmentByWings2(deptDropDownList, companyDropDownList.SelectedValue);
            //aEmployeeRequsitionDal.EmployeeNameDropDown(EmployeeDropDownList, companyDropDownList.SelectedValue);
            aEmployeeRequsitionDal.LoadFinancialYear(mainFinyearDropDownList, companyDropDownList.SelectedValue);

            aEmployeeRequsitionDal.LoadCodeBudgetYearWise(BudgetCodeDropDownList, financialYearDropDownList.SelectedValue, companyDropDownList.SelectedValue);
        }
        else
        {
            deptDropDownList.Items.Clear();
            mainFinyearDropDownList.Items.Clear();
        }
        if (companyDropDownList.SelectedValue != "")
        {
            Session["CompanyId"] = "";
            Session["CompanyId"] = companyDropDownList.SelectedValue;

            SetSearchBoxStatus();

            LoadDivisionddl();
        }
        else
        {
            dsnDropDownList.Items.Clear();
        }
       
        Clear2();
    }

    private void LoadDivisionddl()
    {
        aEmployeeRequsitionDal.LoadDivisionDdl(dsnDropDownList,companyDropDownList.SelectedValue);
    }


    protected void EmployeeNameTextBox_OnTextChanged(object sender, EventArgs e)
    {

        string empName = EmployeeNameTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            EmployeeNameTextBox.Text = emp[2];
            repEmpIdHiddenField.Value = emp[0];
            DataTable dtdata = aEmployeeRequsitionDal.GetEmpData(repEmpIdHiddenField.Value);
            if (dtdata.Rows.Count>0)
            {
                Showme.Visible = true;
                codeLabel.Text =  dtdata.Rows[0]["EmpMasterCode"].ToString();
                nameLabel.Text =  dtdata.Rows[0]["EmpName"].ToString();
                desigLabel.Text =  dtdata.Rows[0]["Designation"].ToString();
               salgradLabel.Text =  dtdata.Rows[0]["GradeCode"].ToString();
                divLabel.Text = dtdata.Rows[0]["DivisionName"].ToString();
                wingLabel.Text =  dtdata.Rows[0]["EmpType"].ToString();
               // wingLabel.Text = dtdata.Rows[0]["EmpType"].ToString();
                deptLabel.Text =  dtdata.Rows[0]["DepartmentName"].ToString();
                lblGrossSalary.Text = dtdata.Rows[0]["GrossAmount"].ToString();
                //secLabel.Text = dtdata.Rows[0]["SectionName"].ToString();
                //subsecLabel.Text = dtdata.Rows[0]["SubSectionName"].ToString();
                DateOfSeperationTextBox.Text =
                    string.IsNullOrEmpty(dtdata.Rows[0]["JobLeftDate"].ToString()) ? "" : Convert.ToDateTime(dtdata.Rows[0]["JobLeftDate"].ToString()).ToString("dd-MMM-yyyy");
            }
            //productNameTextBox.Text = productInfo[1];
            //string productCode = productCodeTextBox.Text.Trim();

        }
        else
        {
            
            EmployeeNameTextBox.Text = "";
            repEmpIdHiddenField.Value = "";
            ShowMessageBox("Input Correct Data !!");
        }
    }


    protected void financialYearDropDownList_OnTextChanged(object sender, EventArgs e)
    {
        if (financialYearDropDownList.SelectedValue != "")
        {
            //aEmployeeRequsitionDal.LoadCodeBudget(BudgetCodeDropDownList);
            aEmployeeRequsitionDal.LoadCodeBudgetYearWise(BudgetCodeDropDownList,financialYearDropDownList.SelectedValue,companyDropDownList.SelectedValue);
        }
        else
        {
            BudgetCodeDropDownList.Items.Clear();
        }
    }

    protected void reportToTextBox_OnTextChanged(object sender, EventArgs e)
    {
        string empName = reportToTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            reportToTextBox.Text = emp[2];
            HFreportTo.Value = emp[0];
            LoadDeSignation(HFreportTo.Value);
        }
        else
        {

            reportToTextBox.Text = "";
            ShowMessageBox("Input Correct Data !!");
        }
    }

    private void LoadDeSignation(string EmpId)
    {
        DataTable dtdata = aEmployeeRequsitionDal.GetEmpDataDesignation(EmpId);
        if (dtdata.Rows.Count > 0)
        {
            
            lblReportDesig.Text = dtdata.Rows[0]["Designation"].ToString();
        }
        else
        {
            
        }
    }

    private void LoadDeSignationEmpNAmeForUpdate(string EmpId)
    {
        //DataTable dtdata = aEmployeeRequsitionDal.GetEmpDataDesignation(EmpId);
        //if (dtdata.Rows.Count > 0)
        //{

        //    lblReportDesig.Text = dtdata.Rows[0]["Designation"].ToString();
        //    reportToTextBox.Text = dtdata.Rows[0]["EmpName"].ToString();
        //}
        //else
        //{

        //}
    }

    protected void jobtitleDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (jobtitleDropDownList.SelectedValue != "")
        {
            Session["DesignationId"] = "";
            Session["DesignationId"] = jobtitleDropDownList.SelectedValue;
        }
    }

    protected void jstRadioButtonList_OnTextChanged(object sender, EventArgs e)
    {
        lblEmpName.Visible = false;
        lblDatSep.Visible = false;
        detail.Visible = false;

        DateOfSeperationTextBox.Text = String.Empty;
        EmployeeNameTextBox.Text = "";
        //jobDesc.Visible = false;
      //  descriptionTextBox.Text = "";

        string btnText = jstRadioButtonList.SelectedItem.Text.Trim();

        if (btnText == "Replacement")
        {
            lblEmpName.Visible = true;
            lblDatSep.Visible = true;
            detail.Visible = true;

            DateOfSeperationTextBox.Text = String.Empty;
            EmployeeNameTextBox.Text = "";

            repEmpIdHiddenField.Value = "";
            codeLabel.Text = "";
            nameLabel.Text = "";
            desigLabel.Text = "";
            salgradLabel.Text = "";
            divLabel.Text = "";
        }
        else
        {
            Showme.Visible = false;
        }

        
    }

    protected void dsnDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (dsnDropDownList.SelectedValue != "")
        {
            Session["DsnId"] = "";
            Session["DsnId"] = dsnDropDownList.SelectedValue;

            if (companyDropDownList.SelectedValue != "")
            {
                Session["DsnCmpId"] = "";
                Session["DsnCmpId"] = companyDropDownList.SelectedValue;
            }
            else
            {
                Session["DsnCmpId"] = "";
                ShowMessageBox("Please select company !!");
            }
        }

        else
        {
            Session["DsnId"] = "";
        }

        SetKeyEmpTextboxStatus();
    }

    private void SetKeyEmpTextboxStatus()
    {
        if (dsnDropDownList.SelectedValue != "" && companyDropDownList.SelectedValue != "")
        {
            keyEmpTextBox.Enabled = true;
            keyEmpTextBox.ToolTip = "You have to select both company & Division for enabling textBox !!!";
        }
        else
        {
            keyEmpTextBox.Enabled = false;
            keyEmpTextBox.ToolTip = "";
        }
    }

    protected void keyEmpTextBox_OnTextChanged(object sender, EventArgs e)
    {
        string empName = keyEmpTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');
            keyEmpTextBox.Text = emp[1];

            DataTable aTable = aEmployeeRequsitionDal.GetEmpJDInfoByEmpCode(emp[0]);

            if (aTable.Rows.Count > 0)
            {
                jdCheckBoxList.DataValueField = "JdDetailsId";
                jdCheckBoxList.DataTextField = "JdDetailsInfo";
                jdCheckBoxList.DataSource = aTable;
                jdCheckBoxList.DataBind();
            }

        }
        else
        {
            keyEmpTextBox.Text = "";
            ShowMessageBox("Input Correct Data !!");
        }
    }

    protected void typeOfPosRadioButtonList_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        

        for (int i = 0; i < typeOfPosRadioButtonList.Items.Count; i++)
        {

            if (typeOfPosRadioButtonList.Items[i].Selected)
            {
                if (typeOfPosRadioButtonList.Items[i].Text.Trim() == "Contractual")
                {

                    if (companyDropDownList.SelectedValue != "")
                    {
                        MonthTextBox.Enabled = true;
                        FundInfoTextBox.Enabled = true;
                        projectDropDownList.Enabled = true;
                        aEmployeeRequsitionDal.GEtProjectDdl(projectDropDownList, companyDropDownList.SelectedValue);


                    }
                    else
                    {
                        typeOfPosRadioButtonList.Items[i].Selected = false;
                        ShowMessageBox("Please select company!!!");
                    }
                   
                }
                else
                {
                    MonthTextBox.Enabled = false;
                    FundInfoTextBox.Enabled = false;
                    projectDropDownList.Enabled = false;
                    projectDropDownList.Items.Clear();
                    MonthTextBox.Text = "";
                    FundInfoTextBox.Text = "";

                }
            }
            
        }
        

    }


    protected void officeDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (officeDropDownList.SelectedValue != "")
        {
            aEmployeeRequsitionDal.GetJobLocationOnPlaceDdl(placeDropDownList, officeDropDownList.SelectedValue);

           
        }
        else
        {
            placeDropDownList.Items.Clear();
        }
    }
    protected void BudgetCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {

        // project.Visible = false;
        // projectDropDownList.Items.Clear();

        DataTable dtdata = new DataTable();
        try
        {
            dtdata = aEmployeeRequsitionDal.LoadBudgetData(Convert.ToInt32(BudgetCodeDropDownList.SelectedValue));
        }
        catch ( Exception){

        }

        if (dtdata.Rows.Count > 0)
        {

            lblSFrom.Text = dtdata.Rows[0]["ReqApproxSalary"].ToString();
            lblSTo.Text = dtdata.Rows[0]["ReqTotalSalary"].ToString();
            jobTitleTextBox.Text = dtdata.Rows[0]["Designation"].ToString();
            descriptionTextBox.Text = dtdata.Rows[0]["DtlRemarks"].ToString();

            nosTextBox.Text = dtdata.Rows[0].Field<Int32>("EmployeeRequisition").ToString(CultureInfo.InvariantCulture);

            for (int i = 0; i < typeOfPosRadioButtonList.Items.Count; i++)
            {
                if (dtdata.Rows[0].Field<Int32>("EmployeeTypeId").ToString(CultureInfo.InvariantCulture) == typeOfPosRadioButtonList.Items[i].Value)
                {
                    typeOfPosRadioButtonList.Items[i].Selected = true;
                }

                if (typeOfPosRadioButtonList.Items[i].Selected)
                {
                    if (typeOfPosRadioButtonList.Items[i].Text.Trim() == "Contractual")
                    {
                        //project.Visible = true;
                        try
                        {

                            aEmployeeRequsitionDal.GEtProjectDdl(projectDropDownList, companyDropDownList.SelectedValue);
                        }
                        catch (Exception)
                        {

                        }

                        try
                        {
                            projectDropDownList.SelectedValue = dtdata.Rows[0].Field<Int32>("ProjectId").ToString(CultureInfo.InvariantCulture);

                        }
                        catch (Exception)
                        {
                            projectDropDownList.SelectedValue = null;
                        }
                    }
                }
            }


            gradeDropDownList.SelectedValue = dtdata.Rows[0].Field<Int32>("SalaryGradeId").ToString(CultureInfo.InvariantCulture);
            //  deptDropDownList.SelectedValue = dtdata.Rows[0].Field<Int32>("DepartmentId").ToString(CultureInfo.InvariantCulture);


        }
    }

    protected void jdCheckBoxList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        string jd = "";

        int lastSelectedIndex = 0;
        string lastSelectedValue = string.Empty;

        foreach (ListItem listitem in CheckBoxList1.Items)
        {
            if (listitem.Selected)
            {
                int thisIndex = CheckBoxList1.Items.IndexOf(listitem);

                if (lastSelectedIndex < thisIndex)
                {
                    lastSelectedIndex = thisIndex;
                    lastSelectedValue = listitem.Value;
                }
            }
        }


        jd = jdCheckBoxList.Items[lastSelectedIndex].Text.Trim();

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("JobReqKeyResName");
        DataRow dataRow;

        if (KeyResponGridView.Rows.Count == 0)
        {
            dataRow = aDataTable.NewRow();

            dataRow = aDataTable.NewRow();
            dataRow["JobReqKeyResName"] = jd;

            aDataTable.Rows.Add(dataRow);

            KeyResponGridView.DataSource = aDataTable;
            KeyResponGridView.DataBind();
        }

        else
        {
            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                dataRow = aDataTable.NewRow();
                dataRow["JobReqKeyResName"] = KeyResponGridView.Rows[i].Cells[0].Text;
                aDataTable.Rows.Add(dataRow);
            }

            dataRow = aDataTable.NewRow();
            dataRow["JobReqKeyResName"] = jd;

            aDataTable.Rows.Add(dataRow);

            KeyResponGridView.DataSource = aDataTable;
            KeyResponGridView.DataBind();
        }



       //addImageButton_OnClick(null,null);

    }

    public bool CheckJDInGrid()
    {
        for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
        {
            if (KeyResponGridView.Rows[i].Cells[0].Text.Trim() == jdTextBox.Text.Trim())
            {
                ShowMessageBox("JD Already Exist");
                return false;
            }
        }
        return true;
    }
    protected void textButton_OnClick(object sender, EventArgs e)
    {
        if (CheckJDInGrid())
        {


            var aDataTable = new DataTable();

            aDataTable.Columns.Add("JobReqKeyResName");
            DataRow dataRow;

            if (jdTextBox.Text != "")
            {
                string jd = jdTextBox.Text.Trim();

                if (KeyResponGridView.Rows.Count == 0)
                {
                    dataRow = aDataTable.NewRow();

                    dataRow = aDataTable.NewRow();
                    dataRow["JobReqKeyResName"] = jd;

                    aDataTable.Rows.Add(dataRow);

                    KeyResponGridView.DataSource = aDataTable;
                    KeyResponGridView.DataBind();
                    jdTextBox.Text = string.Empty;
                }

                else
                {
                    for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                    {
                        dataRow = aDataTable.NewRow();
                        dataRow["JobReqKeyResName"] = KeyResponGridView.Rows[i].Cells[0].Text;
                        aDataTable.Rows.Add(dataRow);
                    }

                    dataRow = aDataTable.NewRow();
                    dataRow["JobReqKeyResName"] = jd;

                    aDataTable.Rows.Add(dataRow);

                    KeyResponGridView.DataSource = aDataTable;
                    KeyResponGridView.DataBind();
                    jdTextBox.Text = string.Empty;
                }
            }
        }
    }

    protected void editImageButton_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;


        string jd = KeyResponGridView.Rows[rowindex].Cells[0].Text;
        jdTextBox.Text = jd;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("JobReqKeyResName");

        DataRow dataRow;

        if (KeyResponGridView.Rows.Count > 0)
        {
            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["JobReqKeyResName"] = KeyResponGridView.Rows[i].Cells[0].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }

        KeyResponGridView.DataSource = aDataTable;
        KeyResponGridView.DataBind();

    }

    public bool ValidationOfKeyReq()
    {
        for (int i = 0; i < jdCheckBoxList.Items.Count; i++)
        {
            for (int j = 0; j < KeyResponGridView.Rows.Count; j++)
            {
                if (jdCheckBoxList.Items[i].Text.Trim()==KeyResponGridView.Rows[j].Cells[0].Text.Trim() && jdCheckBoxList.Items[i].Selected)
                {
                    ShowMessageBox("Jod Description Already Inserted");
                    return false;
                }    
            }
            
        }
        return true;
    }

    protected void addImageButton_OnClick(object sender, EventArgs e)
    {
        if (ValidationOfKeyReq())
        {


            var aDataTable = new DataTable();

            aDataTable.Columns.Add("JobReqKeyResName");

            DataRow dataRow;
            for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
            {
                dataRow = aDataTable.NewRow();
                dataRow["JobReqKeyResName"] = KeyResponGridView.Rows[i].Cells[0].Text;

                aDataTable.Rows.Add(dataRow);
            }

            for (int j = 0; j < jdCheckBoxList.Items.Count; j++)
            {

                if (jdCheckBoxList.Items[j].Selected)
                {
                    // if (CheckItemAlreadyExistOrNot(j))
                    {
                        // if (KeyResponGridView.Rows.Count == 0)
                        {
                            //dataRow = aDataTable.NewRow();

                            //dataRow = aDataTable.NewRow();
                            //dataRow["JobReqKeyResName"] = jdCheckBoxList.Items[j].Text;

                            //aDataTable.Rows.Add(dataRow);

                            //KeyResponGridView.DataSource = aDataTable;
                            //KeyResponGridView.DataBind();
                        }

                        //    else
                        {
                            //for (int i = 0; i < KeyResponGridView.Rows.Count; i++)
                            //{
                            //        dataRow = aDataTable.NewRow();
                            //        dataRow["JobReqKeyResName"] = KeyResponGridView.Rows[i].Cells[0].Text;
                            //        aDataTable.Rows.Add(dataRow);
                            //}

                            dataRow = aDataTable.NewRow();
                            dataRow["JobReqKeyResName"] = jdCheckBoxList.Items[j].Text;

                            aDataTable.Rows.Add(dataRow);

                            KeyResponGridView.DataSource = aDataTable;
                            KeyResponGridView.DataBind();
                        }
                    }
                }
            }
        }
    }

    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (empIdHiddenField.Value != string.Empty)
        {
            string ApprovalStatus = "Drafted";
            Update(ApprovalStatus);
        }
          
        
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {

        if (empIdHiddenField.Value != string.Empty)
        {
            Delete();
        }
      
        

           
         
    }

    private void Delete()
    {
        EmployeeRequsitionDAO aEmployeeRequsitionDao = new EmployeeRequsitionDAO();


        aEmployeeRequsitionDao.JobReqId = Convert.ToInt32(empIdHiddenField.Value);

        aEmployeeRequsitionDao.IsDelete = true;


        aEmployeeRequsitionDao.DeleteBy = Convert.ToInt32(Session["UserId"]);



        aEmployeeRequsitionDao.DeleteDate = DateTime.Now;
        //////aEmployeeRequsitionDal.DelOtherRequirementDetail(empIdHiddenField.Value);
        //////aEmployeeRequsitionDal.DelEducationRequirementsDetail(empIdHiddenField.Value);
        bool status = aEmployeeRequsitionDal.DeleteEmpReqById(aEmployeeRequsitionDao);

        if (status)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(),
              "alert",
              "alert('Data Deleted Successfully...');window.location ='JobRequisitionFormView.aspx';",
              true); 
        }
      
        //LoadEmpJobRequisition();
    }

    protected void OtherRequirementsAddButton_OnClick(object sender, EventArgs e)
    {
        var aDataTable = new DataTable();

        aDataTable.Columns.Add("OtherRequirementsName");
        DataRow dataRow;

       // if ( )
        {
           // string jd = othersTextBox.Text.Trim();

            if (OtherRequirementsGridView.Rows.Count == 0)
            {
                dataRow = aDataTable.NewRow();

                dataRow = aDataTable.NewRow();
                //dataRow["OtherRequirementsName"] = jd;

                aDataTable.Rows.Add(dataRow);

                OtherRequirementsGridView.DataSource = aDataTable;
             //   OtherRequirementsGridView.DataBind();
              //  othersTextBox.Text = string.Empty;
            }

            else
            {
                for (int i = 0; i < OtherRequirementsGridView.Rows.Count; i++)
                {
                    
                }

                dataRow = aDataTable.NewRow();
            
                OtherRequirementsGridView.DataSource = aDataTable;
                OtherRequirementsGridView.DataBind();
                
            }
        }
    }

    protected void editOtherRequirementsGridViewButton_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;


        string jd = OtherRequirementsGridView.Rows[rowindex].Cells[0].Text;
        //othersTextBox.Text = jd;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("OtherRequirementsName");

        DataRow dataRow;

        if (OtherRequirementsGridView.Rows.Count > 0)
        {
            for (int i = 0; i < OtherRequirementsGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();
                    dataRow["OtherRequirementsName"] = OtherRequirementsGridView.Rows[i].Cells[0].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }

        OtherRequirementsGridView.DataSource = aDataTable;
        OtherRequirementsGridView.DataBind();
    }

    protected void deleteOtherRequirementsGridViewButton_OnClick(object sender, ImageClickEventArgs e)
    {
        //var itemCodeTextBox = (ImageButton)sender;
        //var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        //int rowindex = 0;
        //rowindex = currentRow.RowIndex;

        //var aDataTable = new DataTable();

        //aDataTable.Columns.Add("OtherRequirementsName");

        //DataRow dataRow;

        //if (KeyResponGridView.Rows.Count > 0)
        //{
        //    for (int i = 0; i < OtherRequirementsGridView.Rows.Count; i++)
        //    {
        //        if (i != rowindex)
        //        {
        //            dataRow = aDataTable.NewRow();
        //            dataRow["OtherRequirementsName"] = OtherRequirementsGridView.Rows[i].Cells[0].Text;
        //            aDataTable.Rows.Add(dataRow);
        //        }
        //    }
        //}


        //OtherRequirementsGridView.DataSource = aDataTable;
        //OtherRequirementsGridView.DataBind();



        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        var aDataTable = new DataTable();


        aDataTable.Columns.Add("OtherRequirementsName");

        DataRow dataRow;

        if (OtherRequirementsGridView.Rows.Count > 0)
        {
            for (int i = 0; i < OtherRequirementsGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();


                    dataRow["OtherRequirementsName"] = OtherRequirementsGridView.Rows[i].Cells[0].Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }


        OtherRequirementsGridView.DataSource = aDataTable;
        OtherRequirementsGridView.DataBind();
    }

    protected void DirectlySupervicesButton_OnClick(object sender, EventArgs e)
    {
        if (AddJobTitleValidation())
        {
            string eRID = jobtitleDropDownList.SelectedValue;
            string educationRequirements = jobtitleDropDownList.SelectedItem.Text.Trim();

            var aDataTable = new DataTable();

            aDataTable.Columns.Add("DesignationId");
            aDataTable.Columns.Add("Designation");
            aDataTable.Columns.Add("Nos");


            DataRow dataRow;

           
                for (int i = 0; i < DirectlySupervicesGridView.Rows.Count; i++)
                {
                    if (DirectlySupervicesGridView.Rows[i].Cells[0].Text != educationRequirements)
                    {
                        dataRow = aDataTable.NewRow();

                        dataRow["DesignationId"] = DirectlySupervicesGridView.DataKeys[i][0].ToString();
                        dataRow["Designation"] = DirectlySupervicesGridView.Rows[i].Cells[0].Text;
                        dataRow["Nos"] = ((TextBox)DirectlySupervicesGridView.Rows[i].FindControl("DSNoTextBox")).Text;
                        aDataTable.Rows.Add(dataRow);
                        jobtitleDropDownList.SelectedValue = "";
                    }

                    else
                    {
                        jobtitleDropDownList.SelectedValue = "";
                        ShowMessageBox("Data already Exist !!");
                    }
                }

                dataRow = aDataTable.NewRow();

                dataRow["DesignationId"] = eRID;
                dataRow["Designation"] = educationRequirements;
                dataRow["Nos"] = "";
                aDataTable.Rows.Add(dataRow);

                DirectlySupervicesGridView.DataSource = aDataTable;
                DirectlySupervicesGridView.DataBind();

                jobtitleDropDownList.SelectedValue = "";
            
        }

    }

    protected void deleteImageButtonDirectlySupervices_OnClick(object sender, ImageClickEventArgs e)
    {
        var itemCodeTextBox = (ImageButton)sender;
        var currentRow = (GridViewRow)itemCodeTextBox.Parent.Parent;
        int rowindex = 0;
        rowindex = currentRow.RowIndex;

        var aDataTable = new DataTable();

        aDataTable.Columns.Add("DesignationId");
        aDataTable.Columns.Add("Designation");
        aDataTable.Columns.Add("Nos");

        DataRow dataRow;

        if (DirectlySupervicesGridView.Rows.Count > 0)
        {
            for (int i = 0; i < DirectlySupervicesGridView.Rows.Count; i++)
            {
                if (i != rowindex)
                {
                    dataRow = aDataTable.NewRow();

                    dataRow["DesignationId"] = DirectlySupervicesGridView.DataKeys[i][0].ToString();
                    dataRow["Designation"] = DirectlySupervicesGridView.Rows[i].Cells[0].Text;
                    dataRow["Nos"] = ((TextBox)DirectlySupervicesGridView.Rows[i].FindControl("DSNoTextBox")).Text;
                    aDataTable.Rows.Add(dataRow);
                }
            }
        }


        DirectlySupervicesGridView.DataSource = aDataTable;
        DirectlySupervicesGridView.DataBind();

    }

    private bool AddJobTitleValidation()
    {
        if (jobtitleDropDownList.SelectedValue == "")
        {
            ShowMessageBox("Please select Directly Supervised !!!");
            jobtitleDropDownList.Focus();
            return false;
        }

        return true;
    }


    private bool AddOfficeValidation()
    {
        if (officeDropDownList.SelectedValue == "")
        {
            ShowMessageBox("Please select Office !!!");
            officeDropDownList.Focus();
            return false;
        }

        return true;
    }

    protected void SelectAll_Checked(object sender, EventArgs e)
    {
        if (SelectAll.Checked)
        {
            foreach (ListItem li in jdCheckBoxList.Items)
            {
                li.Selected = true;
            }      
        }
        else
        {
            foreach (ListItem li in jdCheckBoxList.Items)
            {
                li.Selected = false;
            }  
        }
    }
    protected void deptDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            aEmployeeRequsitionDal.LoadCodeBudgetYearWise(BudgetCodeDropDownList, financialYearDropDownList.SelectedValue, companyDropDownList.SelectedValue, deptDropDownList.SelectedValue);
        }
        catch (Exception)
        {
           // throw;
        }
    }

    protected void ddlEmpCategoryEx_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpCategoryEx.SelectedIndex > 0)
        {




            aEmployeeRequsitionDal.LoadGradeByCatID(gradeDropDownList, ddlEmpCategoryEx.SelectedValue);
        }
    }
    protected void IsMangAppCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        //if (IsMangAppCheckBox.Checked)
        //{
        //    IsBudgetedDiv.Visible = true;
        //}
        //else
        //{
        //    IsBudgetedDiv.Visible = false;
        //}

    }

    protected void OfficeButton_OnClick(object sender, EventArgs e)
    {
        if (AddOfficeValidation())
        {

            if (officemainIdHiddendField.Value ==string.Empty)
            {
                officemainIdHiddendField.Value = officeDropDownList.SelectedValue;    
            }
            

            string eRID = officeDropDownList.SelectedValue;
            string educationRequirements = officeDropDownList.SelectedItem.Text.Trim();

            var aDataTable = new DataTable();

            aDataTable.Columns.Add("SalaryLoationId");
            aDataTable.Columns.Add("SalaryLocation");
            aDataTable.Columns.Add("SalaryLocationMainId");



            DataRow dataRow;

            bool isexist = false;

            for (int i = 0; i < OfficeGridView.Rows.Count; i++)
            {
                if (OfficeGridView.DataKeys[i][1].ToString() == officemainIdHiddendField.Value)
                {


                    isexist = true;

                    dataRow = aDataTable.NewRow();

                    dataRow["SalaryLoationId"] = OfficeGridView.DataKeys[i][0].ToString() + "," + officeDropDownList.SelectedValue;
                    dataRow["SalaryLocation"] = OfficeGridView.Rows[i].Cells[0].Text + ", " + officeDropDownList.SelectedItem.Text;
                    dataRow["SalaryLocationMainId"] = officemainIdHiddendField.Value;

                    aDataTable.Rows.Add(dataRow);
                   // jobtitleDropDownList.SelectedValue = "";
                }
                else
                {
                    dataRow = aDataTable.NewRow();

                    dataRow["SalaryLoationId"] = OfficeGridView.DataKeys[i][0].ToString();
                    dataRow["SalaryLocation"] = OfficeGridView.Rows[i].Cells[0].Text;
                    dataRow["SalaryLocationMainId"] = OfficeGridView.DataKeys[i][1].ToString();

                    aDataTable.Rows.Add(dataRow);
                }
                
                
            }
            if (isexist==false)
            {
                dataRow = aDataTable.NewRow();

                dataRow["SalaryLoationId"] = eRID;
                dataRow["SalaryLocation"] = educationRequirements;
                dataRow["SalaryLocationMainId"] = officemainIdHiddendField.Value;

                aDataTable.Rows.Add(dataRow);
                aEmployeeRequsitionDal.GetJobLocationOtherJoin(officeDropDownList, officeDropDownList.SelectedValue);
            }
            

            OfficeGridView.DataSource = aDataTable;
            OfficeGridView.DataBind();

            

        }

    }

    protected void btnResetForOffice_Onclick(object sender, EventArgs e)
    {
        officemainIdHiddendField.Value = string.Empty;
        aEmployeeRequsitionDal.GetSalaryLocationOnOfficeDdl(officeDropDownList);
    }

    protected void deleteImageOfficeGridView_OnClick(object sender, ImageClickEventArgs e)
    {
        
    }

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        if (empIdHiddenField.Value == string.Empty)
        {
            if (valiHRR())
            {
                string ApprovalStatus = "Submitted";
                Save(ApprovalStatus);
            }
        }
    }

    protected void btnUpdateforSubmit_OnClick(object sender, EventArgs e)
    {
        if (empIdHiddenField.Value != string.Empty)
        {
            string ApprovalStatus = "Submitted";
            Update(ApprovalStatus);
        }
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("JobRequisitionFormAproval.aspx");
    }

    protected void Button1_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            
      
        EmployeeRequsitionDAO aMaster = new EmployeeRequsitionDAO();
        aMaster.JobReqId
            = Convert.ToInt32(empIdHiddenField.Value);
        string ActionStatus = actionRadioButtonList.SelectedValue;

        aMaster.ActionStatus = actionRadioButtonList.SelectedValue;
        bool status = aEmployeeRequsitionDal.UpdateJobReqStatus2(aMaster);
        if (status)
        {
            int commentid = aEmployeeRequsitionDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                commentsTextBox.Text);
            
            if (ActionStatus == "Verified")
            {
                DataTable dtempdata =
                    aEmployeeRequsitionDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                   "' AND EmpInfoId='" + Session["EmpInfoId"].ToString() +
                                                                   "' " +
                                                                       "   AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' ");
                int serial = Convert.ToInt32(dtempdata.Rows[0]["Serial"].ToString())+1;
                DataTable dtempdata2 =
                    aEmployeeRequsitionDal.GetHRAdminEmployeeAppId(" WHERE URL='" + Session["AppPage"].ToString() +
                                                                   "' AND Serial='" + serial + "' " +
                                                                       "  AND tblEmployeeApprovalByOpearationDetail.CompanyId='" + Session["CompanyId"].ToString() + "' ");
                //DataTable dtempdata = aEmployeeRequsitionDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                JobReqFormAppLogDAO appLogDao = new JobReqFormAppLogDAO();
                {
                    try
                    {
                        appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["EmpInfoId"].ToString());
                        appLogDao.JobReqId = aMaster.JobReqId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;

                        int id = aEmployeeRequsitionDal.SavAppLog(appLogDao);
                    }
                    catch (Exception)
                    {

                    }

                    //if (Session["EmpInfoId"].ToString() == "931")
                    //{

                    //    int empid = 0;
                    //    //DataTable dtempdata = aEmployeeRequsitionDal.GetHRAdminEmployeeAppId(" WHERE URL='"+Session["AppPage"].ToString()+"' AND Serial='1'" );
                    //    //if (dtempdata.Rows.Count>0)
                    //    //{
                    //    //    empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                    //    //}
                    //    JobReqFormAppLogDAO appLogDao2 = new JobReqFormAppLogDAO();
                    //    {
                    //        appLogDao2.JobReqId = aMaster.JobReqId;

                    //        appLogDao2.ActionStatus = actionRadioButtonList.SelectedValue;
                    //        appLogDao2.ApproveDate = DateTime.Now;
                    //        appLogDao2.ApproveBy = Session["UserId"].ToString();
                    //        appLogDao2.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                    //        appLogDao2.ForEmpInfoId = empid;
                    //        appLogDao2.JobReqId = aMaster.JobReqId;
                    //        appLogDao2.Comments = commentsTextBox.Text;
                    //        appLogDao2.CommentsId = commentid;
                    //    };


                    //    int id = aEmployeeRequsitionDal.SavAppLog(appLogDao2);
                    //}
                     

                };

                SenMailForApprved(appLogDao.ForEmpInfoId, " Employee Requisition Form Approval ", @"  <br/> Dear Sir, <br/>
An employee requision is waiting for your approval. <br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");
            }
            else if (ActionStatus == "Approved")
            {
                int empid = 0;
                //DataTable dtempdata = aEmployeeRequsitionDal.GetHRAdminEmployeeAppId(" WHERE URL='"+Session["AppPage"].ToString()+"' AND Serial='1'" );
                //if (dtempdata.Rows.Count>0)
                //{
                //    empid = Convert.ToInt32(dtempdata.Rows[0]["EmpInfoId"].ToString());
                //}
                JobReqFormAppLogDAO appLogDao = new JobReqFormAppLogDAO();
                {
                    appLogDao.ActionStatus = actionRadioButtonList.SelectedValue;
                    appLogDao.ApproveDate = DateTime.Now;
                     appLogDao.ApproveBy = Session["UserId"].ToString();
                     appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                    appLogDao.ForEmpInfoId = empid;
                     appLogDao.JobReqId = aMaster.JobReqId;
                     appLogDao.Comments = commentsTextBox.Text;
                    appLogDao.CommentsId = commentid;
                };


                int id = aEmployeeRequsitionDal.SavAppLog(appLogDao);

                DataTable dtLoop = aEmployeeRequsitionDal.GetAppLogEmployeeApprovalID(empIdHiddenField.Value.ToString());
                DataTable finapp = aEmployeeRequsitionDal.getFinalApprovePersonInfo(appLogDao.PreEmpInfoId.ToString());
                string finEmpName = "";
                string finEmpdes = "";
                if (finapp.Rows.Count > 0)
                {
                    finEmpName = finapp.Rows[0]["EmpName"].ToString();
                    finEmpdes = finapp.Rows[0]["Designation"].ToString();
                }

                if (dtLoop.Rows.Count > 0)
                {
                    for (int i = 0; i < dtLoop.Rows.Count; i++)
                    {


                        SenMailForApprved(Convert.ToInt32(dtLoop.Rows[i]["EmpInfoId"].ToString()), " Employee Requisition Form Approved ", @"  <br/> Dear Sir, <br/>
Employee Requisition Form has been Approved. <br/><br/><br/>Final Approved By:<br/>" + finEmpName + @"<br/>"+finEmpdes+@"


<br/><br/>
 please login for the details from the below link.<br/><br/>   http://182.160.103.234:8088/
");

                    }
                }


             
            }
            else if (ActionStatus == "Review")
            {

                string actionst = "";
                DataTable dtdatastatus = aEmployeeRequsitionDal.GetAppLogStatus(empIdHiddenField.Value.ToString(),
                    Session["EmpInfoId"].ToString());
                if (dtdatastatus.Rows.Count > 0)
                {
                    actionst = dtdatastatus.Rows[0]["ActionStatus"].ToString();
                }

                DataTable dtempdata = aEmployeeRequsitionDal.GetEmpInfoPrevious(Session["EmpInfoid"].ToString(), empIdHiddenField.Value);
                DataTable dtempdata2 = aEmployeeRequsitionDal.GetEmpInfoPrevious(dtempdata.Rows[0]["PreEmpInfoId"].ToString(), empIdHiddenField.Value);

                if (dtempdata2.Rows.Count > 0)
                {
                    JobReqFormAppLogDAO appLogDao = new JobReqFormAppLogDAO();
                    {
                        appLogDao.ActionStatus = "Verified";
                        appLogDao.ApproveDate = DateTime.Now;
                        appLogDao.ApproveBy = Session["UserId"].ToString();
                        appLogDao.PreEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["PreEmpInfoId"].ToString());
                        appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata2.Rows[0]["ForEmpInfoId"].ToString());
                        appLogDao.JobReqId = aMaster.JobReqId;
                        appLogDao.Comments = commentsTextBox.Text;
                        appLogDao.CommentsId = commentid;
                    }
                    
                    aEmployeeRequsitionDal.UpdateAppLog("Review", Session["AppLogId"].ToString());

                    if (actionst == "Approved")
                    {
                        aEmployeeRequsitionDal.UpdateContractural("Verified", aMaster.JobReqId);
                    }

                    int id = aEmployeeRequsitionDal.SavAppLog(appLogDao);
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
                   "alert('Operation Successfully Done!');window.location ='JobRequisitionFormAproval.aspx';",
                   true);
        }
    }

    protected void Button2a_OnClick(object sender, EventArgs e)
    {
        EmployeeRequsitionDAO aMaster = new EmployeeRequsitionDAO();
        aMaster.JobReqId
            = Convert.ToInt32(empIdHiddenField.Value);
        aMaster.ActionStatus = "Rejected";
        bool status = aEmployeeRequsitionDal.UpdateContractural(aMaster.ActionStatus, Convert.ToInt32(empIdHiddenField.Value));
        int commentid = aEmployeeRequsitionDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                commentsTextBox.Text);
        if (aMaster.ActionStatus == "Rejected")
        {
            //DataTable dtempdata = aEmployeeRequsitionDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
            JobReqFormAppLogDAO appLogDao = new JobReqFormAppLogDAO();
            {
                appLogDao.ActionStatus = "Rejected";
                appLogDao.ApproveDate = DateTime.Now;
                appLogDao.ApproveBy = Session["UserId"].ToString();
                appLogDao.PreEmpInfoId = 0;
                appLogDao.ForEmpInfoId = 0;
                appLogDao.JobReqId = aMaster.JobReqId;
                appLogDao.Comments = commentsTextBox.Text;
                appLogDao.CommentsId = commentid;

            };
            int id = aEmployeeRequsitionDal.SavAppLog(appLogDao);
            aEmployeeRequsitionDal.UpdateJobReqStatus2(aMaster);
        }
        Session["AppLogId"] = null;
        ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Rejected Successfully...');window.location ='JobRequisitionFormAproval.aspx';",
                   true);
    }
}
