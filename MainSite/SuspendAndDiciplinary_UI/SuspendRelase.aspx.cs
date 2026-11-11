using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.SuspendAndDiciplinary_Dal;
using DAO.HRIS_DAO;

public partial class SuspendAndDiciplinary_UI_SuspendRelase : System.Web.UI.Page
{

    EmployeeSuspendDal aSuspendDal = new EmployeeSuspendDal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            releaseDateTextBox.Attributes.Add("readonly","readonly");
            LoadDropDownList();
            if (Session["suspendId"] != "")
            {
                GetOneRecord(Session["suspendId"].ToString());
                Session["suspendId"] = "";  
            }
        }
    }

    private void LoadDropDownList()
    {
        aSuspendDal.LoadCompanyDropDownList(companyDropDownList);
        //actionDal.EmployeeTypeList(typeDropDownList);
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }

   
    private void GetOneRecord(string suspendId)
    {

        DataTable aTable = aSuspendDal.EmpSuspendInformation(suspendId);

        const int rowIndex = 0;

        if (aTable.Rows.Count > 0)
        {
            effectDateTexBox.Text = aTable.Rows[rowIndex].Field<DateTime>("EffectiveDate").ToString("dd-MMM-yyyy");
            //  EmpMasterCodeTextBox.Text = aTable.Rows[rowIndex].Field<string>("EmpMasterCode").ToString(CultureInfo.InvariantCulture);
            suspendHiddenField.Value = suspendId;

            companyDropDownList.SelectedValue = aTable.Rows[0]["CompanyInfoId"].ToString();
            if (companyDropDownList.SelectedValue != "")
            {

                aSuspendDal.LoadActionType(actionTypeDropDownList, companyDropDownList.SelectedValue);
                actionTypeDropDownList.SelectedValue = aTable.Rows[0]["ReasonId"].ToString().Trim();
            }
            else
            {
                actionTypeDropDownList.Items.Clear();
            }

            EmpInfoIdHiddenField.Value = aTable.Rows[0]["EmpInfoId"].ToString().Trim();
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

            employeeType.Text = aTable.Rows[0]["EmpType"].ToString().Trim();

            //empGradeLabel.Text = aTable.Rows[0]["GradeName"].ToString().Trim();
            //empGradeIdHiddenField.Value = aTable.Rows[0]["GradeId"].ToString().Trim();

            joiningDateLabel.Text = Convert.ToDateTime(aTable.Rows[0]["DateOfJoin"]).ToString("dd-MMM-yyyy");

            descriptionTexBox.Text = aTable.Rows[rowIndex].Field<string>("Description").ToString(CultureInfo.InvariantCulture);
            remarksTextBox.Text = aTable.Rows[rowIndex].Field<string>("Remarks").ToString(CultureInfo.InvariantCulture);
            //typeDropDownList.SelectedValue = aTable.Rows[rowIndex].Field<Int32>("TypeId").ToString(CultureInfo.InvariantCulture);



            //for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            //{
               
            //    string str = CheckBoxList1.Items[i].Text.Trim();

            //    if (str == "Suspension Letter")
            //    {
            //        if (aTable.Rows[rowIndex].Field<bool>("isSuspensionLetter"))
            //        {
            //            CheckBoxList1.Items[i].Selected = true;
            //        }
            //    }

            //    if (str == "With Pay")
            //    {

            //        if (aTable.Rows[rowIndex].Field<bool>("isWithPay"))
            //        {
            //            CheckBoxList1.Items[i].Selected = true;
            //        }
                   
            //    }

            //    if (str == "Without Pay")
            //    {
            //        if (aTable.Rows[rowIndex].Field<bool>("isWithoutPay"))
            //        {
            //            CheckBoxList1.Items[i].Selected = true;
            //        }
            //    }
                
            //}

            //for (int i = 0; i < CheckBoxList1.Items.Count; i++)
            //{
            //    CheckBoxList1.Items[i].Enabled = false;
            //}
            
        }
    }

    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (suspendHiddenField.Value != "")
        {
            EmployeeSuspendDao aSuspendDao = new EmployeeSuspendDao();

            aSuspendDao.RelesedOn = Convert.ToDateTime(releaseDateTextBox.Text);
            aSuspendDao.isRelease = true;
            aSuspendDao.ReleasedBy = Session["UserId"].ToString();
            aSuspendDao.SuspendId = Convert.ToInt32(suspendHiddenField.Value);
            aSuspendDao.ReleaseExplain = descriptionTexBox.Text;
            aSuspendDao.ReleaseRemarks = remarksTextBox.Text;
            if (aSuspendDal.UpdateSuspendReleaseInfo(aSuspendDao))
            {
                if (aSuspendDal.UpdateSuspendReleaseInfoInEmp(EmpInfoIdHiddenField.Value))
                {


                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                        "alert",
                        "alert('Release Successfully...');window.location ='SuspendReleaseList.aspx';",
                        true);
                    ShowMessageBox("Release Successfully !!");
                }
                //Clear();
            }

        }
    }

    private void Clear()
    {
        effectDateTexBox.Text = "";
        //EmpMasterCodeTextBox.Text = "";

        EmpInfoIdHiddenField.Value = "";
        empNameTexBox.Text = "";

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

        EmpInfoIdHiddenField.Value = "";
        suspendHiddenField.Value = "";

        //for (int i = 0; i < CheckBoxList1.Items.Count; i++)
        //{
        //    if (CheckBoxList1.Items[i].Selected)
        //    {
        //        CheckBoxList1.Items[i].Selected = false;
        //    }
        //}
        //releaseDateTextBox.Text = string.Empty;
        //relremarksTextBox.Text = string.Empty;
        //explainTextBox.Text = string.Empty;


    }

    protected void backToList_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("SuspendReleaseList.aspx");
    }
}