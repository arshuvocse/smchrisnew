using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.SuspendAndDiciplinary_Dal;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class SuspendAndDiciplinary_UI_DiciplinaryAction : System.Web.UI.Page
{
    //EmployeeSuspendDal aSuspendDal = new EmployeeSuspendDal();
    ManageUserOperationCredentials aOperationCredentials = new ManageUserOperationCredentials();
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();

    DiciplinaryActionDal actionDal = new DiciplinaryActionDal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonVisible();
            LoadDropDownList();

            effectDateTexBox.Attributes.Add("readonly", "readonly");
            if (Session["MId"] != null)
            {
                suspendHiddenField.Value = Session["MId"].ToString();
                GetOneRecord(Session["MId"].ToString());
                Session["MId"] = null;
            }
        }
    }

    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {


            if (Session["Status"].ToString() == "Add")
            {
                submithButton.Visible = true;
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                btn_Edit.Visible = true;
            }
            else if (Session["Status"].ToString() == "Delete")
            {
                btn_Del.Visible = true;
            }
            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("DiciplinaryActionView.aspx");
        }

    }
    protected void Button1_OnClick(object sender, EventArgs e)
    {
        Session["History"] = "Decip";
        Response.Redirect("History.aspx");
    }
    private void LoadDropDownList()
    {
        actionDal.LoadCompanyDropDownList(companyDropDownList);

        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_OnSelectedIndexChanged(null, null);

        //actionDal.EmployeeTypeList(typeDropDownList);
    }

    private void GetOneRecord(string suspendId)
    {

        DataTable aTable = actionDal.EmpSuspendInformation(suspendId);

        const int rowIndex = 0;

        if (aTable.Rows.Count > 0)
        {
  companyDropDownList.SelectedValue = aTable.Rows[rowIndex].Field<Int32>("CompanyId").ToString(CultureInfo.InvariantCulture);

            companyDropDownList_OnSelectedIndexChanged(null, null);
           // EmployeeDropDownList.Text = aTable.Rows[rowIndex].Field<string>("EmpName").ToString(CultureInfo.InvariantCulture);
            effectDateTexBox.Text = aTable.Rows[rowIndex].Field<DateTime>("EffectiveDate").ToString("dd-MMM-yyyy");
            empCodeLabel.Text = aTable.Rows[rowIndex].Field<string>("EmpMasterCode").ToString(CultureInfo.InvariantCulture);
          

        




            ddlEmpInfo.SelectedValue = aTable.Rows[0]["EmpInfoId"].ToString().Trim();
            empNameTexBox.Text = aTable.Rows[0]["EmpName"].ToString().Trim();
            employeeType.Text = aTable.Rows[0]["EmpType"].ToString().Trim();
            empTypeHiddenField.Value = aTable.Rows[0]["EmpTypeId"].ToString().Trim();
            //comNameLabel.Text = aTable.Rows[0]["CompanyName"].ToString().Trim();
            //comIdHiddenField.Value = aTable.Rows[0]["CompanyId"].ToString().Trim();

            //divisionNameLabel.Text = aTable.Rows[0]["DivisionName"].ToString().Trim();
            //divitionIdHiddenField.Value = aTable.Rows[0]["DivisionId"].ToString().Trim();

            //divWingNameLabel.Text = aTable.Rows[0]["DivisionWingName"].ToString().Trim();
            //divWingIdHiddenField.Value = aTable.Rows[0]["DivisionWId"].ToString().Trim();


            HFDivID.Value = aTable.Rows[0]["DivisionId"].ToString();
            HFDivWingId.Value = aTable.Rows[0]["DivisionWId"].ToString();

            HFSecID.Value = aTable.Rows[0]["SectionId"].ToString();
            HFSubSecID.Value = aTable.Rows[0]["SubSectionId"].ToString();

            HFSalLocID.Value = aTable.Rows[0]["SalaryLoationId"].ToString();
            HFJobLocID.Value = aTable.Rows[0]["JobLocationId"].ToString();

            deptNameLabel.Text = aTable.Rows[0]["DepartmentName"].ToString().Trim();
            deptIdHiddenField.Value = aTable.Rows[0]["DepartmentId"].ToString().Trim();

            //secNameLabel.Text = aTable.Rows[0]["SectionName"].ToString().Trim();
            //secIdHiddenField.Value = aTable.Rows[0]["SectionId"].ToString().Trim();

            //subSectionLabel.Text = aTable.Rows[0]["SubSectionName"].ToString().Trim();
            //subSectionHiddenField.Value = aTable.Rows[0]["SubSectionId"].ToString().Trim();

            desigNameLabel.Text = aTable.Rows[0]["Designation"].ToString().Trim();
            desigIdHiddenField.Value = aTable.Rows[0]["DesignationId"].ToString().Trim();

            //empGradeLabel.Text = aTable.Rows[0]["GradeName"].ToString().Trim();
            //empGradeIdHiddenField.Value = aTable.Rows[0]["SalaryGradeId"].ToString().Trim();

            joiningDateLabel.Text //= aTable.Rows[0]["DateOfJoin"].ToString("dd-MMM-yyyy");
                = aTable.Rows[rowIndex].Field<DateTime>("DateOfJoin").ToString("dd-MMM-yyyy");

            descriptionTexBox.Text = aTable.Rows[rowIndex].Field<string>("Description").ToString(CultureInfo.InvariantCulture);
            remarksTextBox.Text = aTable.Rows[rowIndex].Field<string>("Remarks").ToString(CultureInfo.InvariantCulture);
            //typeDropDownList.SelectedValue = aTable.Rows[rowIndex].Field<Int32>("TypeId").ToString(CultureInfo.InvariantCulture);

            if (companyDropDownList.SelectedValue != "")
            {
                actionDal.FinancialYearDropDown(FinancialYearDropDownList, companyDropDownList.SelectedValue);
                actionDal.LoadActionType(actionTypeDropDownList, companyDropDownList.SelectedValue);

                DataTable dtgrade = actionDal.CheckBoxLoadActionType(companyDropDownList.SelectedValue);
                gradeCheckBoxList.DataValueField = "SuspendReasonEntryId";
                gradeCheckBoxList.DataTextField = "SuspendReasonEntry";
                gradeCheckBoxList.DataSource = dtgrade;
                gradeCheckBoxList.DataBind();
                actionTypeDropDownList.SelectedValue = aTable.Rows[0]["ReasonId"].ToString().Trim();
                FinancialYearDropDownList.SelectedValue = aTable.Rows[0]["FinancialYearId"].ToString().Trim();

                string ReasonIdStr = aTable.Rows[0]["ReasonIdStr"].ToString().Trim();
                if (ReasonIdStr.Contains(','))
                {
                    string[] emp = ReasonIdStr.Split(',');

                    
                   
                    for (int i = 0; i < gradeCheckBoxList.Items.Count; i++)
                    {

                        for (int h = 0; h < emp.Length; h++)
                        {
                            if (gradeCheckBoxList.Items[i].Value == emp[h].Trim())
                            {
                                gradeCheckBoxList.Items[i].Selected = true;
                            }
                        }
                         
                    }


                }
            }
            else
            {
                actionTypeDropDownList.Items.Clear();
            }


        }
    }

    protected void searchButton_Click(object sender, EventArgs e)
    {
        //if (!string.IsNullOrEmpty((Convert.))
        //{
        DataTable aTable = actionDal.LoadEmpInfo(ddlEmpInfo.SelectedValue);

            if (aTable.Rows.Count > 0)
            {

                ddlEmpInfo.SelectedValue = aTable.Rows[0]["EmpInfoId"].ToString().Trim();
                empNameTexBox.Text = aTable.Rows[0]["EmpName"].ToString().Trim();

                //comNameLabel.Text = aTable.Rows[0]["CompanyName"].ToString().Trim();
                //comIdHiddenField.Value = aTable.Rows[0]["CompanyId"].ToString().Trim();

                //divisionNameLabel.Text = aTable.Rows[0]["DivisionName"].ToString().Trim();
                //divitionIdHiddenField.Value = aTable.Rows[0]["DivisionId"].ToString().Trim();

                //divWingNameLabel.Text = aTable.Rows[0]["DivisionWingName"].ToString().Trim();
                //divWingIdHiddenField.Value = aTable.Rows[0]["DivisionWId"].ToString().Trim();


                deptNameLabel.Text = aTable.Rows[0]["DepartmentName"].ToString().Trim();
                deptIdHiddenField.Value = aTable.Rows[0]["DepartmentId"].ToString().Trim();

                //secNameLabel.Text = aTable.Rows[0]["SectionName"].ToString().Trim();
                //secIdHiddenField.Value = aTable.Rows[0]["SectionId"].ToString().Trim();

                //subSectionLabel.Text = aTable.Rows[0]["SubSectionName"].ToString().Trim();
                //subSectionHiddenField.Value = aTable.Rows[0]["SubSectionId"].ToString().Trim();

                desigNameLabel.Text = aTable.Rows[0]["Designation"].ToString().Trim();
                desigIdHiddenField.Value = aTable.Rows[0]["DesignationId"].ToString().Trim();

                //empGradeLabel.Text = aTable.Rows[0]["GradeName"].ToString().Trim();
                //empGradeIdHiddenField.Value = aTable.Rows[0]["SalaryGradeId"].ToString().Trim();

                joiningDateLabel.Text = aTable.Rows[0]["JoiningDate"].ToString();
                
            }
            else
            {
                aShowMessage.ShowMessageBox("Employee not Active", this);
            }
        //}
        //else
        //{
        //    aShowMessage.ShowMessageBox("Please Input Employee Code", this);
        //}
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (DataValidation())
        {
            if (suspendHiddenField.Value == "")
            {
                try
                {
                     DataTable aTable =
                            actionDal.ValidattionForEffectiveDate(
                                  ddlEmpInfo.SelectedValue, effectDateTexBox.Text);

                    if (aTable.Rows.Count > 0)
                    {
                        aShowMessage.ShowMessageBox("Data Can not be Inserted", this);
                    }
                    else
                    {
                        Int32 diciplinaryId = SaveDiciplinaryInformation();

                        if (diciplinaryId > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Operation Successfully Done...');window.location ='DiciplinaryActionView.aspx';",
                       true);
                            Clear();
                        }
                    }
                }
                catch (Exception)
                {
                    aShowMessage.ShowMessageBox(aMessages.ErrorMessage, this);
                }
            }

            
        }
    }

    private bool UpdateRegionInformation(DiciplinaryAction prepareDataForUpdate)
    {
        bool retVal;
        try
        {
            retVal = actionDal.UpdateDataForEmpSuspend(PrepareDataForUpdate());
        }
        catch (Exception)
        {
            retVal = false;
        }

        return retVal;
    }

    private DiciplinaryAction PrepareDataForUpdate()
    {
        var aDiciplinery = new DiciplinaryAction();

        aDiciplinery.DiciplinaryId = Convert.ToInt32(suspendHiddenField.Value);
        aDiciplinery.EmpInfoId = Convert.ToInt32(ddlEmpInfo.SelectedValue);
        aDiciplinery.EffectiveDate = Convert.ToDateTime(effectDateTexBox.Text.Trim());

        aDiciplinery.CompanyInfoId = Convert.ToInt32(companyDropDownList.SelectedValue);
        //aDiciplinery.DivisionWId = Convert.ToInt32(divWingIdHiddenField.Value);
        //aDiciplinery.DivisionId = Convert.ToInt32(divitionIdHiddenField.Value);

        aDiciplinery.DeptId = Convert.ToInt32(deptIdHiddenField.Value);
        //aDiciplinery.SectionId = Convert.ToInt32(secIdHiddenField.Value);
        aDiciplinery.DesigId = Convert.ToInt32(desigIdHiddenField.Value);

        aDiciplinery.EmpTypeId = Convert.ToInt32(empTypeHiddenField.Value);
        aDiciplinery.UpdateBy = Convert.ToInt32(Session["UserId"]);
        aDiciplinery.UpdateDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        aDiciplinery.ActionStatus = "Posted";
        aDiciplinery.JoiningDate = Convert.ToDateTime(joiningDateLabel.Text.Trim());
        aDiciplinery.Description = descriptionTexBox.Text.Trim();

        aDiciplinery.Remarks = remarksTextBox.Text.Trim();
        //aDiciplinery.SubSectionId = Convert.ToInt32(subSectionHiddenField.Value);
        aDiciplinery.IsActive = true;
        string g = GradeParam();

        int kkkk = 0;
        if (g.Contains(','))
        {
            string[] emp = g.Split(',');


            kkkk = Convert.ToInt32(emp[0].Trim());



        }

        aDiciplinery.ReasonId = Convert.ToInt32(kkkk);
        aDiciplinery.ReasonIdStr = g;
        if (HFDivID.Value != "")
        {
            aDiciplinery.DivisionId =
                Convert.ToInt32(HFDivID.Value) > 0 ? int.Parse(HFDivID.Value) : (int?)null;
        }

        if (HFDivWingId.Value != "")
        {
            aDiciplinery.DivisionWId =
                Convert.ToInt32(HFDivWingId.Value) > 0 ? int.Parse(HFDivWingId.Value) : (int?)null;
        }


        if (HFSecID.Value != "")
        {
            aDiciplinery.SectionId =
                Convert.ToInt32(HFSecID.Value) > 0 ? int.Parse(HFSecID.Value) : (int?)null;
        }

        if (HFSubSecID.Value != "")
        {
            aDiciplinery.SubSectionId =
                Convert.ToInt32(HFSubSecID.Value) > 0 ? int.Parse(HFSubSecID.Value) : (int?)null;
        }
        if (HFSalLocID.Value != "")
        {
            aDiciplinery.SalaryLoationId =
                Convert.ToInt32(HFSalLocID.Value) > 0 ? int.Parse(HFSalLocID.Value) : (int?)null;
        }

        if (HFJobLocID.Value != "")
        {
            aDiciplinery.JobLocationId =
                Convert.ToInt32(HFJobLocID.Value) > 0 ? int.Parse(HFJobLocID.Value) : (int?)null;
        }
        aDiciplinery.EmpCode =
            empCodeLabel.Text;

        aDiciplinery.FinancialYearId = Convert.ToInt32(FinancialYearDropDownList.SelectedValue);

        //aSuspend.DivisionWId = Convert.ToInt32(divitionIdHiddenField.Value);
        //aSuspend.SubSectionId = Convert.ToInt32(subSectionHiddenField.Value);

        //aSuspend.TypeId = Convert.ToInt32(empTypeHiddenField.Value);
        aDiciplinery.IsActive = true;

        return aDiciplinery;
    }

    private Int32 SaveDiciplinaryInformation()
    {
        Int32 retVal;
        try
        {
            retVal = actionDal.SaveDataForDiciplinaryAction(PrepareDataForSave());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }


    private Int32 DELSaveDiciplinaryInformation()
    {
        Int32 retVal;
        try
        {
            retVal = actionDal.DELSaveDataForDiciplinaryAction (DELPrepareDataForSave());
        }
        catch (Exception)
        {
            retVal = 0;
        }

        return retVal;
    }

    private DiciplinaryAction PrepareDataForSave()
    {
        var aDiciplinery = new DiciplinaryAction();

        aDiciplinery.EmpInfoId = Convert.ToInt32(ddlEmpInfo.SelectedValue);
        aDiciplinery.EffectiveDate = Convert.ToDateTime(effectDateTexBox.Text.Trim());

        aDiciplinery.CompanyInfoId = Convert.ToInt32(companyDropDownList.SelectedValue);
        //aDiciplinery.DivisionWId = Convert.ToInt32(divWingIdHiddenField.Value);
        //aDiciplinery.DivisionId = Convert.ToInt32(divitionIdHiddenField.Value);

        aDiciplinery.DeptId = Convert.ToInt32(deptIdHiddenField.Value);
        //aDiciplinery.SectionId = Convert.ToInt32(secIdHiddenField.Value);
        aDiciplinery.DesigId = Convert.ToInt32(desigIdHiddenField.Value);

        aDiciplinery.EmpTypeId = Convert.ToInt32(empTypeHiddenField.Value);
        aDiciplinery.EntryBy = Session["UserId"].ToString();
        aDiciplinery.EntryDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        aDiciplinery.ActionStatus = "Posted";
        aDiciplinery.JoiningDate = Convert.ToDateTime(joiningDateLabel.Text.Trim());
        aDiciplinery.Description = descriptionTexBox.Text.Trim();

        aDiciplinery.Remarks = remarksTextBox.Text.Trim();
        //aDiciplinery.SubSectionId = Convert.ToInt32(subSectionHiddenField.Value);
        aDiciplinery.IsActive = true;
       string g= GradeParam();

        int kkkk = 0;
       if (g.Contains(','))
       {
           string[] emp = g.Split(',');


           kkkk = Convert.ToInt32(emp[0].Trim());



       }

       aDiciplinery.ReasonId = Convert.ToInt32(kkkk);
        aDiciplinery.ReasonIdStr = g;
        if (HFDivID.Value != "")
        {
            aDiciplinery.DivisionId =
                Convert.ToInt32(HFDivID.Value) > 0 ? int.Parse(HFDivID.Value) : (int?)null;
        }

        if (HFDivWingId.Value != "")
        {
            aDiciplinery.DivisionWId =
                Convert.ToInt32(HFDivWingId.Value) > 0 ? int.Parse(HFDivWingId.Value) : (int?)null;
        }


        if (HFSecID.Value != "")
        {
            aDiciplinery.SectionId =
                Convert.ToInt32(HFSecID.Value) > 0 ? int.Parse(HFSecID.Value) : (int?)null;
        }

        if (HFSubSecID.Value != "")
        {
            aDiciplinery.SubSectionId =
                Convert.ToInt32(HFSubSecID.Value) > 0 ? int.Parse(HFSubSecID.Value) : (int?)null;
        }
        if (HFSalLocID.Value != "")
        {
            aDiciplinery.SalaryLoationId =
                Convert.ToInt32(HFSalLocID.Value) > 0 ? int.Parse(HFSalLocID.Value) : (int?)null;
        }

        if (HFJobLocID.Value != "")
        {
            aDiciplinery.JobLocationId =
                Convert.ToInt32(HFJobLocID.Value) > 0 ? int.Parse(HFJobLocID.Value) : (int?)null;
        }
        aDiciplinery.EmpCode =
            empCodeLabel.Text;

        aDiciplinery.FinancialYearId = Convert.ToInt32(FinancialYearDropDownList.SelectedValue);

        //for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        //{
        //    if (CheckBoxList1.Items[i].Selected)
        //    {
        //        string str = CheckBoxList1.Items[i].Text.Trim();

        //        if (str == "Warning Letter")
        //        {
        //            aDiciplinery.isWarningLetter = true;
        //        }

        //        if (str == "Hold Increment")
        //        {
        //            aDiciplinery.isHoldIncrement = true;
        //        }

        //        if (str == "Hold Incentive")
        //        {
        //            aDiciplinery.isHoldIncentive = true;
        //        }

        //        if (str == "Termination")
        //        {
        //            aDiciplinery.isTermination = true;
        //        }

        //        if (str == "Deduction of Salary")
        //        {
        //            aDiciplinery.isDeductionOfSalary = true;
        //        }

        //        if (str == "7 Days Salary Deduction")
        //        {
        //            aDiciplinery.is7DaysSalaryDeduction = true;
        //        }
        //    }
        //}


        return aDiciplinery;
    }



    private DiciplinaryAction DELPrepareDataForSave()
    {
        var aDiciplinery = new DiciplinaryAction();

        aDiciplinery.DiciplinaryId = Convert.ToInt32(suspendHiddenField.Value);
        aDiciplinery.EmpInfoId = Convert.ToInt32(ddlEmpInfo.SelectedValue);
        aDiciplinery.EffectiveDate = Convert.ToDateTime(effectDateTexBox.Text.Trim());

        aDiciplinery.CompanyInfoId = Convert.ToInt32(companyDropDownList.SelectedValue);
        //aDiciplinery.DivisionWId = Convert.ToInt32(divWingIdHiddenField.Value);
        //aDiciplinery.DivisionId = Convert.ToInt32(divitionIdHiddenField.Value);

        aDiciplinery.DeptId = Convert.ToInt32(deptIdHiddenField.Value);
        //aDiciplinery.SectionId = Convert.ToInt32(secIdHiddenField.Value);
        aDiciplinery.DesigId = Convert.ToInt32(desigIdHiddenField.Value);

        aDiciplinery.EmpTypeId = Convert.ToInt32(empTypeHiddenField.Value);
        aDiciplinery.EntryBy = Session["UserId"].ToString();
        aDiciplinery.EntryDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        aDiciplinery.ActionStatus = "Posted";
        aDiciplinery.JoiningDate = Convert.ToDateTime(joiningDateLabel.Text.Trim());
        aDiciplinery.Description = descriptionTexBox.Text.Trim();

        aDiciplinery.Remarks = remarksTextBox.Text.Trim();
        //aDiciplinery.SubSectionId = Convert.ToInt32(subSectionHiddenField.Value);
        aDiciplinery.IsActive = true;

        aDiciplinery.ReasonId = Convert.ToInt32(actionTypeDropDownList.SelectedValue);
        if (HFDivID.Value != "")
        {
            aDiciplinery.DivisionId =
                Convert.ToInt32(HFDivID.Value) > 0 ? int.Parse(HFDivID.Value) : (int?)null;
        }

        if (HFDivWingId.Value != "")
        {
            aDiciplinery.DivisionWId =
                Convert.ToInt32(HFDivWingId.Value) > 0 ? int.Parse(HFDivWingId.Value) : (int?)null;
        }


        if (HFSecID.Value != "")
        {
            aDiciplinery.SectionId =
                Convert.ToInt32(HFSecID.Value) > 0 ? int.Parse(HFSecID.Value) : (int?)null;
        }

        if (HFSubSecID.Value != "")
        {
            aDiciplinery.SubSectionId =
                Convert.ToInt32(HFSubSecID.Value) > 0 ? int.Parse(HFSubSecID.Value) : (int?)null;
        }
        if (HFSalLocID.Value != "")
        {
            aDiciplinery.SalaryLoationId =
                Convert.ToInt32(HFSalLocID.Value) > 0 ? int.Parse(HFSalLocID.Value) : (int?)null;
        }

        if (HFJobLocID.Value != "")
        {
            aDiciplinery.JobLocationId =
                Convert.ToInt32(HFJobLocID.Value) > 0 ? int.Parse(HFJobLocID.Value) : (int?)null;
        }
        aDiciplinery.EmpCode =
            empCodeLabel.Text;

        aDiciplinery.FinancialYearId = Convert.ToInt32(FinancialYearDropDownList.SelectedValue);

        //for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        //{
        //    if (CheckBoxList1.Items[i].Selected)
        //    {
        //        string str = CheckBoxList1.Items[i].Text.Trim();

        //        if (str == "Warning Letter")
        //        {
        //            aDiciplinery.isWarningLetter = true;
        //        }

        //        if (str == "Hold Increment")
        //        {
        //            aDiciplinery.isHoldIncrement = true;
        //        }

        //        if (str == "Hold Incentive")
        //        {
        //            aDiciplinery.isHoldIncentive = true;
        //        }

        //        if (str == "Termination")
        //        {
        //            aDiciplinery.isTermination = true;
        //        }

        //        if (str == "Deduction of Salary")
        //        {
        //            aDiciplinery.isDeductionOfSalary = true;
        //        }

        //        if (str == "7 Days Salary Deduction")
        //        {
        //            aDiciplinery.is7DaysSalaryDeduction = true;
        //        }
        //    }
        //}


        return aDiciplinery;
    }

    private bool DataValidation()
    {
        if (effectDateTexBox.Text == "")
        {
            aShowMessage.ShowMessageBox("Effective date is required!!", this);
            return false;
        }

        if (GradeParam() == string.Empty)
        {
            aShowMessage.ShowMessageBox("Please Select action type!!", this);
           return false;
        }

        if (empNameTexBox.Text == "")
        {
            aShowMessage.ShowMessageBox("Employee Name is required!!", this);
            return false;
        }
        if (EffectDate() == false)
        {
            aShowMessage.ShowMessageBox("Please enter Valid Date!!", this);
            return false;
        }

        return true;
    }


    public string GradeParam()
    {
        string param = "";
        string grade = "";

        for (int i = 0; i < gradeCheckBoxList.Items.Count; i++)
        {
            if (gradeCheckBoxList.Items[i].Selected)
            {
                grade = gradeCheckBoxList.Items[i].Value + "," + grade;
            }
        }
        if (grade != string.Empty)
        {
            param = param +   grade.TrimEnd(',')  ;
        }
        else
        {
            param = "";
        }
        return param;

    }
    private bool EffectDate()
    {
        try
        {
            var aDateTime = new DateTime();
            aDateTime = Convert.ToDateTime(effectDateTexBox.Text);
        }
        catch (Exception)
        {
            return false;
        }
        return true;
    }


    private void Clear()
    {
        effectDateTexBox.Text = "";
        //EmpMasterCodeTextBox.Text = "";

        companyDropDownList.SelectedValue = "";
         
        empNameTexBox.Text = "";

        empCodeLabel.Text = "";

        //comNameLabel.Text = "";
        //comIdHiddenField.Value = "";

        //divisionNameLabel.Text = "";
        //divitionIdHiddenField.Value = "";

        //divWingNameLabel.Text = "";
        //divWingIdHiddenField.Value = "";


        deptNameLabel.Text = "";
        deptIdHiddenField.Value = "";

        //secNameLabel.Text = "";
        //secIdHiddenField.Value = "";

        //subSectionLabel.Text = "";
        //subSectionHiddenField.Value = "";

        desigNameLabel.Text = "";
        desigIdHiddenField.Value = "";

        //empGradeLabel.Text = "";
        //empGradeIdHiddenField.Value = "";

        joiningDateLabel.Text = "";

        descriptionTexBox.Text = "";
        remarksTextBox.Text = "";
        //typeDropDownList.SelectedValue = "";

        empTypeHiddenField.Value = "";
        employeeType.Text = "";

  
        suspendHiddenField.Value = "";

        //for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        //{
        //    if (CheckBoxList1.Items[i].Selected)
        //    {
        //        CheckBoxList1.Items[i].Selected = false;
        //    }
        //}

        submithButton.Text = "Save";
        actionTypeDropDownList.Items.Clear();

    }
    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Clear();
    }

    protected void popupButton_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
    }

    protected void refreshButton_Click(object sender, EventArgs e)
    {
        //actionDal.EmployeeTypeList(typeDropDownList);
    }


    [System.Web.Services.WebMethod(EnableSession = true), ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void SaveEmployeeType(string empName)
    {
        var aTypeDao = new EmployeeTypeDao();

        aTypeDao.EmpType = empName.Trim();
        aTypeDao.IsActive = true;
        aTypeDao.EntryBy = Session["LoginName"].ToString();
        aTypeDao.EntryDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        actionDal.SaveEmployeeType(aTypeDao);
    }


    protected void SSGradeCheck_OnCheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < gradeCheckBoxList.Items.Count; i++)
        {
            if (SSGradeCheck.Checked)
            {
                gradeCheckBoxList.Items[i].Selected = true;
            }
            else
            {
                gradeCheckBoxList.Items[i].Selected = false
                    ;
            }
        }

     
    }

    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        //aSuspendDal.EmployeeNameDropDown(EmployeeDropDownList, companyDropDownList.SelectedValue);

        if (companyDropDownList.SelectedValue != "")
        {
            

            actionDal.LoadActionType(actionTypeDropDownList, companyDropDownList.SelectedValue);
            DataTable dtgrade = actionDal.CheckBoxLoadActionType(companyDropDownList.SelectedValue);
            gradeCheckBoxList.DataValueField = "SuspendReasonEntryId";
            gradeCheckBoxList.DataTextField = "SuspendReasonEntry";
            gradeCheckBoxList.DataSource = dtgrade;
            gradeCheckBoxList.DataBind();

            actionDal.FinancialYearDropDown(FinancialYearDropDownList, companyDropDownList.SelectedValue);


            using (DataTable dt = _commonDataLoad.GetEmpDDLAActiveOnlyView(companyDropDownList.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;

            }
        }
        else
        {
            actionTypeDropDownList.Items.Clear();
            ddlEmpInfo.Items.Clear();
        }
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
       Response.Redirect("DiciplinaryActionView.aspx");
    }

    protected void HomeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

    protected void btn_Del_OnClick(object sender, EventArgs e)
    {
        if (suspendHiddenField.Value != "")
        {
            try
            {
          DataTable aTable =
                             actionDal.DeleteValidattionForEffectiveDate(suspendHiddenField.Value.ToString());
                if (aTable.Rows.Count > 0)
                {
                    string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
                    string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                    if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                    {
                        Int32 departmentId = DELSaveDiciplinaryInformation();
                        if (actionDal.DeleteInfofoById(suspendHiddenField.Value))
                        {
                           

                            if (departmentId > 0)
                            {
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                "alert",
                                "alert('Operation Successfull Done...');window.location ='DiciplinaryActionView.aspx';",
                                true);
                            }


                        }
                    }
                    else
                    {
                        aShowMessage.ShowMessageBox("Data Can not be Deleted !!!", this);
                    }
                }
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Operation Faild...');window.location ='DiciplinaryActionView.aspx';",
                       true);
            }
           
        }
    }

    protected void btn_Edit_OnClick(object sender, EventArgs e)
    {
        if (suspendHiddenField.Value != "")
        {
            try
            {
                  DataTable aTable =
                             actionDal.DeleteValidattionForEffectiveDate(suspendHiddenField.Value.ToString());
                if (aTable.Rows.Count > 0)
                {
                    string OldEfDate = Convert.ToDateTime(aTable.Rows[0]["EffectiveDate"]).ToString("MMMM dd, yyyy");
                    string NowDate = DateTime.Now.ToString("MMMM dd, yyyy");

                    if (Convert.ToDateTime(OldEfDate) >= Convert.ToDateTime(NowDate))
                    {
                        bool area = UpdateRegionInformation(PrepareDataForUpdate());

                        if (area)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(),
                                "alert",
                                "alert('Operation Successfull Done...');window.location ='DiciplinaryActionView.aspx';",
                                true);
                        }
                    }
                }
                else
                {
                    aShowMessage.ShowMessageBox("Data Can not be Updated !!!", this);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                       "alert",
                       "alert('Operation Faild...');window.location ='DiciplinaryActionView.aspx';",
                       true);
            }
        }
    }

    protected void FinancialYearDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        effectDateTexBox.Text = "";
    }

    protected void ddddd(object sender, EventArgs e)
    {
        if (FinancialYearDropDownList.SelectedValue != "")
        {
            if (effectDateTexBox.Text != "")
            {


                if (CheckStartEndDateExistOrNot(effectDateTexBox.Text, effectDateTexBox.Text) == true)
                {

                }
                if (CheckStartEndDateExistOrNot(effectDateTexBox.Text, effectDateTexBox.Text) == false)
                {
                    aShowMessage.ShowMessageBox("Effective date must be within the finnancial year!!", this);
                    effectDateTexBox.Text = "";
                    effectDateTexBox.Focus();

                }
            }



        }
        else
        {
            aShowMessage.ShowMessageBox("Please Select this.", this);
            effectDateTexBox.Text = "";
            FinancialYearDropDownList.Focus();
        }
    }
    private bool CheckStartEndDateExistOrNot(string Start, string End)
    {
        bool status = false;

        DataTable dataTable = actionDal.CheckStartEndDateExistOrNotDAL(FinancialYearDropDownList.SelectedValue, Start, End);

        if (dataTable.Rows.Count > 0)
        {
            status = true;
        }

        return status;
    }

    protected void ddlEmpInfo_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable aTable = actionDal.LoadEmpInfoFromEmployee(ddlEmpInfo.SelectedValue);

        if (aTable.Rows.Count > 0)
        {

            ddlEmpInfo.SelectedValue = aTable.Rows[0]["EmpInfoId"].ToString().Trim();
            empNameTexBox.Text = aTable.Rows[0]["EmpName"].ToString().Trim();

            empCodeLabel.Text = aTable.Rows[0]["EmpMasterCode"].ToString().Trim();

            //comNameLabel.Text = aTable.Rows[0]["CompanyName"].ToString().Trim();
            //comIdHiddenField.Value = aTable.Rows[0]["CompanyId"].ToString().Trim();

            //divisionNameLabel.Text = aTable.Rows[0]["DivisionName"].ToString().Trim();
            //divitionIdHiddenField.Value = aTable.Rows[0]["DivisionId"].ToString().Trim();

            //divWingNameLabel.Text = aTable.Rows[0]["DivisionWingName"].ToString().Trim();
            //divWingIdHiddenField.Value = aTable.Rows[0]["DivisionWId"].ToString().Trim();


            deptNameLabel.Text = aTable.Rows[0]["DepartmentName"].ToString().Trim();
            deptIdHiddenField.Value = aTable.Rows[0]["DepartmentId"].ToString().Trim();

            //secNameLabel.Text = aTable.Rows[0]["SectionName"].ToString().Trim();
            //secIdHiddenField.Value = aTable.Rows[0]["SectionId"].ToString().Trim();

            //subSectionLabel.Text = aTable.Rows[0]["SubSectionName"].ToString().Trim();
            //subSectionHiddenField.Value = aTable.Rows[0]["SubSectionId"].ToString().Trim();

            desigNameLabel.Text = aTable.Rows[0]["Designation"].ToString().Trim();
            desigIdHiddenField.Value = aTable.Rows[0]["DesignationId"].ToString().Trim();

            //empGradeLabel.Text = aTable.Rows[0]["GradeName"].ToString().Trim();
            //empGradeIdHiddenField.Value = aTable.Rows[0]["SalaryGradeId"].ToString().Trim();

            HFDivID.Value = aTable.Rows[0]["DivisionId"].ToString();
            HFDivWingId.Value = aTable.Rows[0]["DivisionWId"].ToString();

            HFSecID.Value = aTable.Rows[0]["SectionId"].ToString();
            HFSubSecID.Value = aTable.Rows[0]["SubSectionId"].ToString();

            HFSalLocID.Value = aTable.Rows[0]["SalaryLoationId"].ToString();
            HFJobLocID.Value = aTable.Rows[0]["JobLocationId"].ToString();
            employeeType.Text = aTable.Rows[0]["EmpType"].ToString().Trim();
            empTypeHiddenField.Value = aTable.Rows[0]["EmpTypeId"].ToString().Trim();

            joiningDateLabel.Text = Convert.ToDateTime(aTable.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");

        }
        else
        {
            aShowMessage.ShowMessageBox("Employee not Active", this);
        } 

    }
}