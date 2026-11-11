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
using CrystalDecisions.Shared.Json;
using DAL.COMMON_DAL;
using DAL.ContractualEmployeeManagement_DAL;
using DAL.MasterSetup_DAL;
using DAL.RecruitmentManagement_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;
using Microsoft.Practices.EnterpriseLibrary.Data;

public partial class MasterSetup_UI_HRONJobRequisitionForm : System.Web.UI.Page
{
   CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    EducationRequirementsDetailDAL aEducationRequirementsDetailDAL = new EducationRequirementsDetailDAL();
    OtherRequirementDetailDAL aOtherRequirementDetailDAL = new OtherRequirementDetailDAL();
    EmployeeRequsitionDAL aEmployeeRequsitionDal = new EmployeeRequsitionDAL();
    SubSectionInformationDal asSectionInformationDal = new SubSectionInformationDal();
   JobReqFormBll aJobReqFormBll = new JobReqFormBll();
   private int _EMpId = 0;
   private int _CompanyId = 0;
   ShowMessage aShowMessage = new ShowMessage();
   Messages aMessages = new Messages();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            _EMpId = Convert.ToInt32(Session["EmpInfoId"].ToString());
            _CompanyId = Convert.ToInt32(Session["CompanyId"].ToString());
        }
        catch (Exception)
        {
            _EMpId = 0;
            //throw;
        }
        if (!IsPostBack)
        {

            Session["Status"] = "Add";
            ButtonVisible();
            ReadonlyDateTime();
            MethodAutoIncri();
            RadioListLoad();
          
         


            lOADdATEnEWeMP();
            //BudgetCodeDropDownList.Visible = isBudgetedCheckBox.Checked;
            //LblOther.Visible = isBudgetedCheckBox.Checked;

            DropDownList();

          //  aEmployeeRequsitionDal.GEtProjectDdl(projectDropDownList, companyDropDownList.SelectedValue);
            
            if (Session["JobReqId"] !=null)
            {
                empIdHiddenField.Value = Session["JobReqId"].ToString();
                LoadData(Session["JobReqId"].ToString());
                Session["JobReqId"] = null;
            }

            else
            {
                MethodAutoIncri();
                reqDateTextBox.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
         
        }
    }
    ContractualEmpManageDAL aContractualEmpManageDAL = new ContractualEmpManageDAL();

    private void lOADdATEnEWeMP()
    {
        DataTable dtdata = new DataTable();
        dtdata = aContractualEmpManageDAL.LoadEmpJInfoInTextBoxById(_EMpId);
        if (dtdata.Rows.Count > 0)
        {
            deptLabel.Text = dtdata.Rows[0]["DepartmentName"].ToString();
            hfDepartId.Value = dtdata.Rows[0]["DepartmentId"].ToString();
        }
    }

    private void ReadonlyDateTime()
    {
        expDtJoinTextBox.Attributes.Add("readonly", "readonly");
        reqDateTextBox.Attributes.Add("readonly", "readonly");
      
    }
    private bool validforSupper()
    {
        
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

    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {


            if (Session["Status"].ToString() == "Add")
            {
                submitButton.Visible = true;
                btnSubmit.Visible = true;
                orBTN.Visible = true;
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                editButton.Visible = true;
                btnUpdateforSubmit.Visible = true;
                orUp.Visible = true;

            }
            else if (Session["Status"].ToString() == "Delete")
            {
                delButton.Visible = true;
            }
            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("JobRequisitionFormView.aspx");
        }

    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
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

   

    public void LoadData(string id)
    {
        DataTable dtdata=new DataTable();
        dtdata = aEmployeeRequsitionDal.LoadEmpJobRequisitionById(id);

        if (dtdata.Rows.Count > 0)
        {
            empIdHiddenField.Value = dtdata.Rows[0].Field<Int32>("JobReqId").ToString();
           
            
           
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

        

            reqDateTextBox.Text = Convert.ToDateTime(dtdata.Rows[0]["ReqDate"].ToString()).ToString("dd-MMM-yyyy");
         
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
                }
            }

          //  project.Visible = false;

            for (int i = 0; i < typeOfPosRadioButtonList.Items.Count; i++)
            {

                if (typeOfPosRadioButtonList.Items[i].Selected)
                {
                    if (typeOfPosRadioButtonList.Items[i].Text.Trim() == "Contractual")
                    {

                        if (_CompanyId != 0)
                        {
                         //   project.Visible = true;
                         //   MonthDiv.Visible = true;
                       //     FunDDiv.Visible = true;
                            aEmployeeRequsitionDal.GEtProjectDdl(projectDropDownList, _CompanyId.ToString());
                            projectDropDownList.SelectedValue = dtdata.Rows[0]["ProjectId"].ToString();
                           
                           

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
                    }
                }

            }
        


            //if (typeOfPosRadioButtonList.Items[3].Value == dtdata.Rows[0]["EmpTypeId"])
            //{
            //    typeOfPosRadioButtonList.Items[3].Selected = true;
            //}
       //     aEmployeeRequsitionDal.LoadDesignationByCompanyId(jobtitleDropDownList, companyDropDownList.SelectedValue);
         

           // officeDropDownList.SelectedValue = dtdata.Rows[0]["OfficeId"].ToString();



            aEmployeeRequsitionDal.GetJobLocationOnPlaceAll(placeDropDownList);

            if (dtdata.Rows[0]["PlaceId"].ToString() != "")
            {
                placeDropDownList.SelectedValue = dtdata.Rows[0]["PlaceId"].ToString();
            }
        
             //   aEmployeeRequsitionDal.GetJobLocationOnPlaceDdl(placeDropDownList, officeDropDownList.SelectedValue);
                
           


        

            gradeDropDownList.SelectedValue = dtdata.Rows[0]["GradeId"].ToString();
         


           


            //HFreportTo.Value = dtdata.Rows[0]["ReportingTo"].ToString();
            //LoadDeSignationEmpNAmeForUpdate(HFreportTo.Value);
          
            
       
            

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

                expDtJoinTextBox.Text = "";
            }


            if (Convert.ToBoolean(dtdata.Rows[0]["IsBudgeted"]) == true)
            {
                isBudgetedCheckBox.Items[0].Selected = true;
                
             


            }

            else
            {
                isBudgetedCheckBox.Items[1].Selected = true;
                 
            }

         //   expDtJoinTextBox.Text = Convert.ToDateTime(dtdata.Rows[0]["ExpDateOfJoining"]).ToString("dd-MMM-yyyy");
           
           
            

            

            
 


            DataTable AppLogComm = aJobReqFormBll.AppLogCommByJobReqId(Convert.ToInt32(id));

            if (AppLogComm.Rows.Count > 0)
            {
                DivShow.Visible = true;
                AppLogCommentGridView.DataSource = AppLogComm;
                AppLogCommentGridView.DataBind();
            }
            //if (isBudgetedCheckBox. == true)
            //{
            //    ShowBudgetCodeDiv.Visible = true;
            //    BudgetCodeDropDownList.SelectedValue = dtdata.Rows[0]["BudgetId"].ToString();

            //}

            //IsReplacementforCheckBox.Checked = Convert.ToBoolean(dtdata.Rows[0]["IsReplacement"].ToString());

            if ((bool) dtdata.Rows[0]["IsReplacement"])
            {
                for (int i = 0; i < jstRadioButtonList.Items.Count; i++)
                {
                    if (jstRadioButtonList.Items[i].Text.Trim() == "Replacement")
                    {
                        jstRadioButtonList.Items[i].Selected = true;
                    }

                    if (jstRadioButtonList.Items[i].Selected)
                    {

                       

                        DateOfSeperationTextBox.Text = String.Empty;
                     
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
                    

                     
                    }
                }
            }
            else
            {
                for (int i = 0; i < jstRadioButtonList.Items.Count; i++)
                {
                    if (jstRadioButtonList.Items[i].Text.Trim() == "New")
                    {
                        jstRadioButtonList.Items[i].Selected = true;
                       
 
                    }
                }
            }

            //if (IsReplacementforCheckBox.Checked == true)
            //{
                

            //}

          
 

 
       

            
             
             
              
              

             


       
        }
    }

    protected void ddlSalaryGrade_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (gradeDropDownList.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLSalaryStep(gradeDropDownList.SelectedValue))
            {
                ddlSalaryStep.DataSource = dt;
                ddlSalaryStep.DataValueField = "Value";
                ddlSalaryStep.DataTextField = "TextField";
                ddlSalaryStep.DataBind();
            }

            //using (DataTable dt = _commonDataLoad.GetDDLDesignationByGrade(int.Parse(ddlSalaryGrade.SelectedValue)))
            //{
            //    ddlDesignation.DataSource = dt;
            //    ddlDesignation.DataValueField = "Value";
            //    ddlDesignation.DataTextField = "TextField";
            //    ddlDesignation.DataBind();
            //}
        }
    }
    public void DropDownList()

    {

        aEmployeeRequsitionDal.LoadGrade(gradeDropDownList);

        DataTable dtcom2 = _commonDataLoad.GetEmpDDLForEntry2("");
        ddlReportingBoss.DataSource = dtcom2;
        ddlReportingBoss.DataValueField = "EmpInfoId";
        ddlReportingBoss.DataTextField = "EmpName";
        ddlReportingBoss.DataBind();
        ddlReportingBoss.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
        ddlReportingBoss.SelectedIndex = 0;
        using (DataTable dt = _commonDataLoad.GetDDLDesignationAll())
        {
            ddlDesignation.DataSource = dt;
            ddlDesignation.DataValueField = "Value";
            ddlDesignation.DataTextField = "TextField";

            ddlDesignation.DataBind();

            ddlDesignation.SelectedValue = "Please Select One..";
        }


  
        aEmployeeRequsitionDal.GetSalaryLocationOnOfficeDdl(officeDropDownList);
        aEmployeeRequsitionDal.GetJobLocationOnPlaceAll(placeDropDownList);

        //aEmployeeRequsitionDal.LoadCodeBudget(BudgetCodeDropDownList);

        //aEmployeeRequsitionDal.LoadFinancialYear(financialYearDropDownList);

        //aEmployeeRequsitionDal.KeyResponsibilites(KeyResponsibilitesDropDownList.Text);
        aEmployeeRequsitionDal.EducationRequirementDropDownList(EducationRequirementDropDownList);

       
        
    }

   

  

   
    protected void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }

    public bool Validation()
    {

 


    

        

        if (reqDateTextBox.Text=="")
        {
            ShowMessageBox("Please enter Requisition Date !!!!");
            reqDateTextBox.Focus();
           
            return false;
        }
       



        for (int i = 0; i < jstRadioButtonList.Items.Count; i++)
        {
            if (jstRadioButtonList.Items[i].Selected)
            {
                if (jstRadioButtonList.Items[i].Text.Trim() == "Replacement")
                {

                    
                    
                }
            }
        }

        //if (jstRadioButtonList.SelectedItem.Text.Trim() == "Replacement")
        //{
        //    if (nameLabel.Text == "")
        //    {
                
        //    }
        //}


        
        if (gradeDropDownList.SelectedValue == "")
        {
            ShowMessageBox("Please select grade !!!!");
            gradeDropDownList.Focus();
            return false;
        }

     

        //if (expDtJoinTextBox.Text == "")
        //{
        //    ShowMessageBox("Expected joining date is required !!!!");
        //    expDtJoinTextBox.Focus();
        //    return false;
        //}


     
        int count = 0;

        for (int i = 0; i < typeOfPosRadioButtonList.Items.Count; i++)
        {
            if (typeOfPosRadioButtonList.Items[i].Selected)
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
            ShowMessageBox("Please select Employee Type !!!!");
            typeOfPosRadioButtonList.Focus();
          
            return false;
        }
         

         

        if (JustificationTextBox.Text == "")
        {
            ShowMessageBox("Please enter Justification!!!!");
            JustificationTextBox.Focus();
            return false;
        }
      


    
 

     




        return true;
    }

    public void Save(string acstatus)
    {
        if (Validation())
        {
            EmployeeRequsitionDAO aEmployeeRequsitionDao = new EmployeeRequsitionDAO();



            aEmployeeRequsitionDao.CompanyId = Convert.ToInt32(_CompanyId);
            aEmployeeRequsitionDao.DeptId = Convert.ToInt32(hfDepartId.Value);
            //aEmployeeRequsitionDao.EmployeeId = Convert.ToInt32(repEmpIdHiddenField.Value);
         
            aEmployeeRequsitionDao.ReqDate = Convert.ToDateTime(reqDateTextBox.Text);
           
            aEmployeeRequsitionDao.GradeId = Convert.ToInt32(gradeDropDownList.SelectedValue);
       

           
            if (projectDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDao.ProjectId = Convert.ToInt32(projectDropDownList.SelectedValue);
            }

          
 

           
            //aEmployeeRequsitionDao.SupervisorId = Convert.ToInt32(jobtitleDropDownList.SelectedValue);
           

         //   if (officeDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDao.OfficeId = 0; // Convert.ToInt32(officeDropDownList.SelectedValue);
            }

          //  if (placeDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDao.PlaceId = 0;// Convert.ToInt32(placeDropDownList.SelectedValue);
            }

             

            if (typeOfPosRadioButtonList.Items[0].Selected == true)
            {
                aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[0].Value;
            }

            if (typeOfPosRadioButtonList.Items[1].Selected == true)
            {
                aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[1].Value;
            }
 
         

          
                
              //  ooooaEmployeeRequsitionDao.ExpDateOfJoining = Convert.ToDateTime(expDtJoinTextBox.Text.Trim());
                try
                {
                    aEmployeeRequsitionDao.ExpDateOfJoining =// Convert.ToDateTime(probableInterviewDateTextBox.Text.Trim());

               string.IsNullOrEmpty(expDtJoinTextBox.Text) ? (DateTime?)null : DateTime.Parse(expDtJoinTextBox.Text).Date;

                }
                catch (Exception)
                {


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
               

            //if (isBudgetedCheckBox.Checked==true)
            //{
            //    aEmployeeRequsitionDao.BudgetId = Convert.ToInt32(BudgetCodeDropDownList.SelectedValue);
            //}


            
                if (isBudgetedCheckBox.Items[0].Selected==true)
                {
                    aEmployeeRequsitionDao.IsBudgeted = true;

                    aEmployeeRequsitionDao.BudgetId=
                    0;
                    //  aEmployeeRequsitionDao.m = Convert.ToInt32(mainFinyearDropDownList.SelectedValue);
                }
                else if (isBudgetedCheckBox.Items[1].Selected == false)
                {
                    aEmployeeRequsitionDao.IsBudgeted = false;
                    
                }


            aEmployeeRequsitionDao.Justification = JustificationTextBox.Text;
               
                aEmployeeRequsitionDao.ActionStatus = acstatus;
                aEmployeeRequsitionDao.EntryBy = Convert.ToInt32(Session["UserId"]);
                

                aEmployeeRequsitionDao.EntryDate = DateTime.Now;
                
           

            int id = aEmployeeRequsitionDal.SaveEmpReq(aEmployeeRequsitionDao);
            if (id > 0)
            {


               
             //   EducationRequirementsDetailDao aDetailDao;

                if (id > 0)
                {

                    

                    if (WhichButton.Value != "0")
                    {
                        try
                        {
                            if (Session["EmpInfoId"].ToString() != "")
                            {
                                EmployeeRequsitionDAO aMaster = new EmployeeRequsitionDAO();
                                aMaster.JobReqId
                                    = Convert.ToInt32(id);
                                aMaster.ActionStatus = "Verified";
                                bool status = aEmployeeRequsitionDal.UpdateContractural(aMaster.ActionStatus, aMaster.JobReqId);



                                int commentid = aEmployeeRequsitionDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                                " ");

                                JobReqFormAppLogDAO appLogDaoa = new JobReqFormAppLogDAO();

                                appLogDaoa.ActionStatus = "Drafted";
                                appLogDaoa.ApproveDate = DateTime.Now;
                                appLogDaoa.ApproveBy = Session["UserId"].ToString();
                                appLogDaoa.PreEmpInfoId = Convert.ToInt32(0);
                                appLogDaoa.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                                appLogDaoa.JobReqId = id;
                                appLogDaoa.Comments = txtComment.Text;
                                appLogDaoa.CommentsId = commentid;

                                int idd = aEmployeeRequsitionDal.SavAppLog(appLogDaoa);


                                DataTable dtempdata = aEmployeeRequsitionDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                                JobReqFormAppLogDAO appLogDao = new JobReqFormAppLogDAO();
                                {
                                    appLogDao.ActionStatus = "Verified";
                                    appLogDao.ApproveDate = DateTime.Now;
                                    appLogDao.ApproveBy = Session["UserId"].ToString();
                                    appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                                    appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                                    appLogDao.JobReqId = aMaster.JobReqId;
                                    appLogDao.Comments = txtComment.Text;
                                    appLogDao.CommentsId = commentid;

                                };
                                int iddddd = aEmployeeRequsitionDal.SavAppLog(appLogDao);
                                aEmployeeRequsitionDal.UpdateJobReqStatus2(aMaster);
                            }
                        }
                        catch (Exception)
                        {

                            //throw;
                        }
                    }

                   
                    

                          ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successfully Done!');window.location ='JobRequisitionFormView.aspx';",
                    true);                 
                }
             
                //ShowMessageBox("Data Saved Successfully !!!");
                MethodAutoIncri();
  
            }
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

                    if (_CompanyId != 0)
                    {
                      
                        projectDropDownList.Enabled = true;
                        aEmployeeRequsitionDal.GEtProjectDdl(projectDropDownList, _CompanyId.ToString());


                    }
                    else
                    {
                        typeOfPosRadioButtonList.Items[i].Selected = false;
                        ShowMessageBox("Please select company!!!");
                    }

                }
                else
                {
                    
                    projectDropDownList.Enabled = false;
                    projectDropDownList.Items.Clear();
                   

                }
            }

        }


    }


    public void Submit(string acstatus)
    {
        if (Validation())
        {
            Save(acstatus);

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
  
    public void Update(string ststus)
    {
        if (Validation())
        {
          

            EmployeeRequsitionDAO aEmployeeRequsitionDao = new EmployeeRequsitionDAO();


            aEmployeeRequsitionDao.CompanyId = Convert.ToInt32(_CompanyId);
            //aEmployeeRequsitionDao.EmployeeId = Convert.ToInt32(EmployeeDropDownList.SelectedValue);
            //aEmployeeRequsitionDao.EmployeeId = Convert.ToInt32(repEmpIdHiddenField.Value);

            aEmployeeRequsitionDao.ReqDate = Convert.ToDateTime(reqDateTextBox.Text);

            aEmployeeRequsitionDao.GradeId = Convert.ToInt32(gradeDropDownList.SelectedValue);



            if (projectDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDao.ProjectId = Convert.ToInt32(projectDropDownList.SelectedValue);
            }





            //aEmployeeRequsitionDao.SupervisorId = Convert.ToInt32(jobtitleDropDownList.SelectedValue);


            //   if (officeDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDao.OfficeId = 0; // Convert.ToInt32(officeDropDownList.SelectedValue);
            }

            //  if (placeDropDownList.SelectedValue != "")
            {
                aEmployeeRequsitionDao.PlaceId = 0;// Convert.ToInt32(placeDropDownList.SelectedValue);
            }



            if (typeOfPosRadioButtonList.Items[0].Selected == true)
            {
                aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[0].Value;
            }

            if (typeOfPosRadioButtonList.Items[1].Selected == true)
            {
                aEmployeeRequsitionDao.EmpTypeId = typeOfPosRadioButtonList.Items[1].Value;
            }





            //  ooooaEmployeeRequsitionDao.ExpDateOfJoining = Convert.ToDateTime(expDtJoinTextBox.Text.Trim());
            try
            {
                aEmployeeRequsitionDao.ExpDateOfJoining =// Convert.ToDateTime(probableInterviewDateTextBox.Text.Trim());

           string.IsNullOrEmpty(expDtJoinTextBox.Text) ? (DateTime?)null : DateTime.Parse(expDtJoinTextBox.Text).Date;

            }
            catch (Exception)
            {


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


            //if (isBudgetedCheckBox.Checked==true)
            //{
            //    aEmployeeRequsitionDao.BudgetId = Convert.ToInt32(BudgetCodeDropDownList.SelectedValue);
            //}



            if (isBudgetedCheckBox.Items[0].Selected == true)
            {
                aEmployeeRequsitionDao.IsBudgeted = true;

                aEmployeeRequsitionDao.BudgetId =
                0;
                //  aEmployeeRequsitionDao.m = Convert.ToInt32(mainFinyearDropDownList.SelectedValue);
            }
            else if (isBudgetedCheckBox.Items[1].Selected == false)
            {
                aEmployeeRequsitionDao.IsBudgeted = false;

            }


            aEmployeeRequsitionDao.Justification = JustificationTextBox.Text;

            aEmployeeRequsitionDao.ActionStatus = ststus;
            aEmployeeRequsitionDao.EntryBy = Convert.ToInt32(Session["UserId"]);


            aEmployeeRequsitionDao.EntryDate = DateTime.Now;
                
           

            bool status = aEmployeeRequsitionDal.UpdateEmpReq(aEmployeeRequsitionDao);
            if (status)
            {


               
             

                //aEmployeeRequsitionDal.Delete

                aEmployeeRequsitionDal.DeletePreferedWayOfCircular(empIdHiddenField.Value);

                if (status)
                {
                     

                    if (WhichButton.Value != "0")
                    {
                        try
                        {
                            if (Session["EmpInfoId"].ToString() != "")
                            {
                                EmployeeRequsitionDAO aMaster = new EmployeeRequsitionDAO();
                                aMaster.JobReqId
                                    = Convert.ToInt32(empIdHiddenField.Value);
                                aMaster.ActionStatus = "Verified";
                                bool ssss = aEmployeeRequsitionDal.UpdateContractural(aMaster.ActionStatus, aMaster.JobReqId);



                                int commentid = aEmployeeRequsitionDal.SaveComment("0", Session["EmpInfoId"].ToString(),
                                " ");

                                JobReqFormAppLogDAO appLogDaoa = new JobReqFormAppLogDAO();

                                appLogDaoa.ActionStatus = "Drafted";
                                appLogDaoa.ApproveDate = DateTime.Now;
                                appLogDaoa.ApproveBy = Session["UserId"].ToString();
                                appLogDaoa.PreEmpInfoId = Convert.ToInt32(0);
                                appLogDaoa.ForEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                                appLogDaoa.JobReqId = Convert.ToInt32(empIdHiddenField.Value);
                                appLogDaoa.Comments = txtComment.Text;
                                appLogDaoa.CommentsId = commentid;

                                int idd = aEmployeeRequsitionDal.SavAppLog(appLogDaoa);


                                DataTable dtempdata = aEmployeeRequsitionDal.GetEmpInfo(" WHERE EmpInfoId='" + Session["EmpInfoId"].ToString() + "'");
                                JobReqFormAppLogDAO appLogDao = new JobReqFormAppLogDAO();
                                {
                                    appLogDao.ActionStatus = "Verified";
                                    appLogDao.ApproveDate = DateTime.Now;
                                    appLogDao.ApproveBy = Session["UserId"].ToString();
                                    appLogDao.PreEmpInfoId = Convert.ToInt32(Session["EmpInfoId"].ToString());
                                    appLogDao.ForEmpInfoId = Convert.ToInt32(dtempdata.Rows[0]["ReportingEmpId"].ToString());
                                    appLogDao.JobReqId = aMaster.JobReqId;
                                    appLogDao.Comments = txtComment.Text;
                                    appLogDao.CommentsId = commentid;

                                };
                                int iddddd = aEmployeeRequsitionDal.SavAppLog(appLogDao);
                                aEmployeeRequsitionDal.UpdateJobReqStatus2(aMaster);

                                SenMailForApprved(appLogDao.ForEmpInfoId, " Employee Requisition Form Approval ", @"  <br/> Dear Sir, <br/>
An employee requision is waiting for your approval. <br/>
 please login for the details from the below link. <br/> http://182.160.103.234:8088/
");
                            }
                        }
                        catch (Exception)
                        {

                            //throw;
                        }
                        
                    }

                }

                //ShowMessageBox("Data Updated Successfully !!!");
                //Clear();
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                   "alert",
                   "alert('Data Updated Successfully...');window.location ='JobRequisitionFormView.aspx';",
                   true);
                //ShowMessageBox("Data Saved Successfully !!!");
                MethodAutoIncri();
         

                
            }
        }
    }
    protected void isBudgetedCheckBox_CheckedChanged(object sender, EventArgs e)
    {


        


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
              //  showMessageBox("Email has not Sent, Try Once More time");
            }
            catch (Exception exe)
            {
               // showMessageBox("Email has not Sent, Try Once More time");
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
        if (empIdHiddenField.Value ==string.Empty)
        {
            WhichButton.Value = "0";
            string ApprovalStatus = "Drafted";
            Save(ApprovalStatus);
        }
       
    }

  

  

     
    
    
     
    protected void ViewListButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("JobRequisitionFormView.aspx");
    }

 
     
    protected void reportToTextBox_OnTextChanged(object sender, EventArgs e)
    {
        //string empName = reportToTextBox.Text.Trim();

        //if (empName.Contains(':'))
        //{
        //    string[] emp = empName.Split(':');

        //    reportToTextBox.Text = emp[2];
        //    HFreportTo.Value = emp[0];
        //    LoadDeSignation(HFreportTo.Value);
        //}
        //else
        //{

        //    reportToTextBox.Text = "";
        //    ShowMessageBox("Input Correct Data !!");
        //}
    }
   
    protected void jstRadioButtonList_OnTextChanged(object sender, EventArgs e)
    {
        DivReplacement.Visible = false;

        string btnText = jstRadioButtonList.SelectedItem.Text.Trim();

        if (btnText == "Replacement")
        {
            DivReplacement.Visible = true;

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
     
    protected void editButton_OnClick(object sender, EventArgs e)
    {
        if (empIdHiddenField.Value != string.Empty)
        {
            WhichButton.Value = "0";
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

 
    protected void deleteImageOfficeGridView_OnClick(object sender, ImageClickEventArgs e)
    {
        
    }

    protected void btnSubmit_OnClick(object sender, EventArgs e)
    {
        if (empIdHiddenField.Value == string.Empty)
        {
            WhichButton.Value = "1";
            string ApprovalStatus = "Submitted";

            if (validforSupper())
            {
                Submit(ApprovalStatus);
                
            }

        }
    }

    protected void btnUpdateforSubmit_OnClick(object sender, EventArgs e)
    {
        if (empIdHiddenField.Value != string.Empty)
        {
            WhichButton.Value = "1";
            string ApprovalStatus = "Submitted";
            if (validforSupper())
            {
                Update(ApprovalStatus);
            }
        }
    }
}
