using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Antlr.Runtime.Misc;
using DAL.COMMON_DAL;
using DAL.Lunch_Allowance_DAL;
using DAO.HRIS_DAO;
using HELPER_FUNCTIONS.HELPERS;

public partial class Lunch_Allowance_UI_LunchForGuestIntern : System.Web.UI.Page
{
    private int mid = 0;
    LunchAllowanceCancelDAL allowanceCancelDal=new LunchAllowanceCancelDAL();
    ShowMessage aShowMessage = new ShowMessage();
    private Lunch_Allowance_Dal_Common commmDAl = new Lunch_Allowance_Dal_Common();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ButtonVisible();
            DateTime dta = DateTime.Now.AddDays(1);
            effectiveDateTextBox.Text = dta.ToString("dd-MMM-yyyy");
            txtToDate.Text = dta.ToString("dd-MMM-yyyy");


            effectiveDateTextBox.Attributes.Add("readonly", "readonly");
            txtToDate.Attributes.Add("readonly", "readonly");
            
            //CalendarExtender1.StartDate = DateTime.Now.AddDays(1);
            //CalendarExtender2.StartDate = DateTime.Now.AddDays(1);
            //startDate="<%# DateTime.Now.AddDays(1) %>" EndDate="<%# DateTime.Now.AddDays(30) %>"

            DropDownList();
            rbTransferType_OnSelectedIndexChanged(null, null);
            // Button1_OnClick(null, null);

            try
            {
                mid = Convert.ToInt32(Session["VacancyCirculationId"].ToString());
            }
            catch (Exception)
            {
                
                //throw;
            }
            (hdpk.Value) = mid.ToString();
            if (mid > 0)
            {


                CalendarExtender2.StartDate = dta;
              
              
                using (var db = new HRIS_SMC_DBEntities())
                {
                    var Bat = (from j in db.tblunchGuestInternInformationMasters where j.GuestInternInformationMasterId == mid select j).FirstOrDefault();
                    companyDropDownList.SelectedValue = Bat.CompanyId.ToString();
                    companyDropDownList_OnSelectedIndexChanged(null, null);



                    try
                    {
                            rbTransferType.SelectedValue = Bat.Type;
                        rbTransferType_OnSelectedIndexChanged(null, null);
                    }
                    catch (Exception)
                    {
                        
                       //throw;
                    }
                    txtName.Text = Bat.Name;
                    try
                    {
                        

                    ddlDivision.SelectedValue = Bat.DivisionId.ToString();
                    }
                    catch (Exception)
                    {

                        //throw;
                    }

                     try
                    {
                      
                    ddlDepartment.SelectedValue = Bat.DepartmentId.ToString();
                    }
                     catch (Exception)
                     {

                         //throw;
                     }
                     try
                    {
                     effectiveDateTextBox.Text = string.IsNullOrEmpty(Bat.LunchDate.ToString()) ? String.Empty : Bat.LunchDate.Value.ToString("dd-MMM-yyyy");
                    }
                     catch (Exception)
                     {

                         //throw;
                     }
                      try
                    {
                        txtToDate.Text = string.IsNullOrEmpty(Bat.LunchToDate.ToString()) ? String.Empty : Bat.LunchToDate.Value.ToString("dd-MMM-yyyy");
                    }
                      catch (Exception)
                      {

                          //throw;
                      }

                       try
                    {
                    ddlEmpInfo.SelectedValue = Bat.ReferancePersonId.ToString();
                    }
                       catch (Exception)
                       {

                           //throw;
                       }

                       try
                    {
                  txtRemarks.Text=  Bat.Remarks  ;
                    }
                       catch (Exception)
                       {

                           //throw;
                       }


                    effectiveDateTextBox.Enabled = false;

                }
            }
            Session["VacancyCirculationId"] = null;
        }
    }

    public void ButtonVisible()
    {
        submitButton.Visible = false;
        delButton.Visible = false;
        Button1.Visible = false;
        if (Session["Status"] != null)
        {
            if (Session["Status"].ToString() == "Add")
            {
                Button1.Visible = true;
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                Button1.Visible = true;
            }
            else if (Session["Status"].ToString() == "Delete")
            {
                delButton.Visible = true;
            }
            Session["Status"] = null;
        }
        else
        {
            Response.Redirect("LunchForGuestInternList.aspx");
        }

    }
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    public void DropDownList()
    {
        allowanceCancelDal.LoadCompany(companyDropDownList);
        companyDropDownList.SelectedIndex = 1;
        companyDropDownList_OnSelectedIndexChanged(null, null);

        using (DataTable dt = _commonDataLoad.GetDDLComCategory__Lunch())
        {
            rbTransferType.DataSource = dt;
            rbTransferType.DataValueField = "TextField";
            rbTransferType.DataTextField = "TextField";
            rbTransferType.DataBind();
        }


      

        using (DataTable dt = _commonDataLoad.GetDDLDesignationAll())
        {
            ddlDesignation.DataSource = dt;
            ddlDesignation.DataValueField = "Value";
            ddlDesignation.DataTextField = "TextField";

            ddlDesignation.DataBind();

            ddlDesignation.SelectedValue = "Please Select One..";
        }
    }

    protected void Button1_OnClick(object sender, EventArgs e)
    {
        mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
        tblunchGuestInternInformation Bat = null;
        tblunchGuestInternInformationMaster BatMAs = null;
        if (mid > 0)
        {
            if (validfa())
            {

                bool isSuc = false;
                using (var db = new HRIS_SMC_DBEntities())
                {

                    int idd = commmDAl.SavAppLog(mid, "Update");
                    BatMAs = (from j in db.tblunchGuestInternInformationMasters where j.GuestInternInformationMasterId == mid select j).FirstOrDefault();



                    BatMAs.UpdateBy = Convert.ToInt32(Session["UserId"]);
                    BatMAs.UpdateDate = DateTime.Now;
                    BatMAs.Type = rbTransferType.SelectedValue;
                    BatMAs.Name = string.IsNullOrEmpty(txtName.Text) ? null : txtName.Text;
                    BatMAs.CompanyId = companyDropDownList.SelectedIndex > 0
                        ? int.Parse(companyDropDownList.SelectedValue)
                        : (int?)null;
                    BatMAs.DivisionId = ddlDivision.SelectedIndex > 0
                        ? int.Parse(ddlDivision.SelectedValue)
                        : (int?)null;
                    BatMAs.DepartmentId = ddlDepartment.SelectedIndex > 0
                        ? int.Parse(ddlDepartment.SelectedValue)
                        : (int?)null;
                    BatMAs.LunchDate = string.IsNullOrEmpty(effectiveDateTextBox.Text)
                                              ? (DateTime?)null
                                              : DateTime.Parse(effectiveDateTextBox.Text).Date;

                    BatMAs.LunchToDate = string.IsNullOrEmpty(txtToDate.Text)
                                             ? (DateTime?)null
                                             : DateTime.Parse(txtToDate.Text).Date;
                    BatMAs.ReferancePersonId = ddlEmpInfo.SelectedIndex > 0
                        ? int.Parse(ddlEmpInfo.SelectedValue)
                        : (int?)null;
                    BatMAs.Remarks = string.IsNullOrEmpty(txtRemarks.Text) ? null : txtRemarks.Text;

                  

                    db.SaveChanges();

                    //  var max = db.tblunchGuestInternInformationMasters.DefaultIfEmpty().Max(r => r == null ? 0 : r.GuestInternInformationMasterId);


                    db.Database.ExecuteSqlCommand("delete dbo.tblunchGuestInternInformation  WHERE MasterId={0}",
                            mid);
                   
                    DataTable dtGetAllDate = allowanceCancelDal.GetAllDate(effectiveDateTextBox.Text, txtToDate.Text);

                    for (int i = 0; i < dtGetAllDate.Rows.Count; i++)
                    {
                        Bat = new tblunchGuestInternInformation();

                        DateTime ddddate = Convert.ToDateTime(dtGetAllDate.Rows[i]["DateString"].ToString());
                        Bat.MasterId = mid;
                        Bat.EntryBy = Convert.ToInt32(Session["UserId"]);
                        Bat.EntryDate = DateTime.Now;
                        Bat.Type = rbTransferType.SelectedValue;
                        Bat.Name = string.IsNullOrEmpty(txtName.Text) ? null : txtName.Text;
                        Bat.CompanyId = companyDropDownList.SelectedIndex > 0
                            ? int.Parse(companyDropDownList.SelectedValue)
                            : (int?)null;
                        Bat.DivisionId = ddlDivision.SelectedIndex > 0
                            ? int.Parse(ddlDivision.SelectedValue)
                            : (int?)null;
                        Bat.DepartmentId = ddlDepartment.SelectedIndex > 0
                            ? int.Parse(ddlDepartment.SelectedValue)
                            : (int?)null;
                        Bat.LunchDate = ddddate;

                        Bat.LunchToDate = ddddate;
                        Bat.ReferancePersonId = ddlEmpInfo.SelectedIndex > 0
                            ? int.Parse(ddlEmpInfo.SelectedValue)
                            : (int?)null;
                        Bat.Remarks = string.IsNullOrEmpty(txtRemarks.Text) ? null : txtRemarks.Text;

                        db.tblunchGuestInternInformations.Add(Bat);
                        db.SaveChanges();
                    }
                    
                   
                    isSuc = true;

                    if (isSuc == true)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                 "alert",
                 "alert('Operation Successful...! ');window.location ='LunchForGuestInternList.aspx';",
                 true);
                    }
                    else
                    {
                        ShowMessageBox("Operation Faild!!");
                    }
                }
            }
        }

        else
        {
            if (validfa())
            {

                bool isSuc = false;
                using (var db = new HRIS_SMC_DBEntities())
                {
                    BatMAs = new tblunchGuestInternInformationMaster();

                    BatMAs.EntryBy = Convert.ToInt32(Session["UserId"]);
                    BatMAs.EntryDate = DateTime.Now;
                    BatMAs.Type = rbTransferType.SelectedValue;
                    BatMAs.Name = string.IsNullOrEmpty(txtName.Text) ? null : txtName.Text;
                    BatMAs.CompanyId = companyDropDownList.SelectedIndex > 0
                        ? int.Parse(companyDropDownList.SelectedValue)
                        : (int?)null;
                    BatMAs.DivisionId = ddlDivision.SelectedIndex > 0
                        ? int.Parse(ddlDivision.SelectedValue)
                        : (int?)null;
                    BatMAs.DepartmentId = ddlDepartment.SelectedIndex > 0
                        ? int.Parse(ddlDepartment.SelectedValue)
                        : (int?)null;
                    BatMAs.LunchDate = string.IsNullOrEmpty(effectiveDateTextBox.Text)
                                              ? (DateTime?)null
                                              : DateTime.Parse(effectiveDateTextBox.Text).Date;

                    BatMAs.LunchToDate = string.IsNullOrEmpty(txtToDate.Text)
                                             ? (DateTime?)null
                                             : DateTime.Parse(txtToDate.Text).Date;
                    BatMAs.ReferancePersonId = ddlEmpInfo.SelectedIndex > 0
                        ? int.Parse(ddlEmpInfo.SelectedValue)
                        : (int?)null;
                    BatMAs.Remarks = string.IsNullOrEmpty(txtRemarks.Text) ? null : txtRemarks.Text;

                    db.tblunchGuestInternInformationMasters.Add(BatMAs);

                    db.SaveChanges();

                  //  var max = db.tblunchGuestInternInformationMasters.DefaultIfEmpty().Max(r => r == null ? 0 : r.GuestInternInformationMasterId);

                    int pk = BatMAs.GuestInternInformationMasterId;
                    DataTable dtGetAllDate = allowanceCancelDal.GetAllDate(effectiveDateTextBox.Text, txtToDate.Text);

                    for (int i = 0; i < dtGetAllDate.Rows.Count; i++)
                    {
                        Bat = new tblunchGuestInternInformation();

                        DateTime ddddate = Convert.ToDateTime(dtGetAllDate.Rows[i]["DateString"].ToString());
                        Bat.MasterId = pk;
                        Bat.EntryBy = Convert.ToInt32(Session["UserId"]);
                        Bat.EntryDate = DateTime.Now;
                        Bat.Type = rbTransferType.SelectedValue;
                        Bat.Name = string.IsNullOrEmpty(txtName.Text) ? null : txtName.Text;
                        Bat.CompanyId = companyDropDownList.SelectedIndex > 0
                            ? int.Parse(companyDropDownList.SelectedValue)
                            : (int?) null;
                        Bat.DivisionId = ddlDivision.SelectedIndex > 0
                            ? int.Parse(ddlDivision.SelectedValue)
                            : (int?) null;
                        Bat.DepartmentId = ddlDepartment.SelectedIndex > 0
                            ? int.Parse(ddlDepartment.SelectedValue)
                            : (int?) null;
                        Bat.LunchDate = ddddate;

                        Bat.LunchToDate = ddddate;
                        Bat.ReferancePersonId = ddlEmpInfo.SelectedIndex > 0
                            ? int.Parse(ddlEmpInfo.SelectedValue)
                            : (int?) null;
                        Bat.Remarks = string.IsNullOrEmpty(txtRemarks.Text) ? null : txtRemarks.Text;

                        db.tblunchGuestInternInformations.Add(Bat);
                        db.SaveChanges();
                    }
                    isSuc = true;

                    if (isSuc == true)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                 "alert",
                 "alert('Operation Successful...! ');window.location ='LunchForGuestInternList.aspx';",
                 true);
                    }
                    else
                    {
                        ShowMessageBox("Operation Faild!!");
                    }
                }
            }
        }
       
    }

    private bool validfa()
    {


        if (rbTransferType.SelectedValue == "Select...")

        {
            AlertMessageBoxShow("Please Select Type!");
            rbTransferType.Focus();
            return false;

        }
        if ((txtName.Text == ""))
        {
            AlertMessageBoxShow("Please Enter  Name!");
            txtName.Focus();
            return false;


        }

        if ((companyDropDownList.SelectedValue == "") )
        {
            AlertMessageBoxShow("Please Select company!");
            companyDropDownList.Focus();
            return false;


        }


        if ((ddlDivision.SelectedIndex == 0))
        {
            AlertMessageBoxShow("Please Select Division!");
            ddlDivision.Focus();
            return false;


        }

        if ((ddlDepartment.SelectedIndex == 0))
        {
            AlertMessageBoxShow("Please Select Department!");
            ddlDepartment.Focus();
            return false;


        }

        if ((effectiveDateTextBox.Text == ""))
        {
            AlertMessageBoxShow("Please Select  Lunch From Date!");
            effectiveDateTextBox.Focus();
            return false;


        }

        if ((txtToDate.Text == ""))
        {
            AlertMessageBoxShow("Please Select Lunch To Date!");
            txtToDate.Focus();
            return false;


        }

        if (rbTransferType.SelectedValue == "Guest" )
      
        {

            if ((ddlEmpInfo.SelectedValue == ""))
            {
                AlertMessageBoxShow("Please Select Referance Person!");
                ddlEmpInfo.Focus();
                return false;


            }
        }

        return true;
    }

   

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    private string GenerateParameter()
    {
        string parameter = " ";

        if (companyDropDownList.SelectedIndex > 0)
        {
            parameter = parameter + "  and    alo.CompanyId = '" + companyDropDownList.SelectedValue + "'";
        }

        if (ddlDivision.SelectedIndex > 0)
        {
            parameter = parameter + "  and    alo.DivisionId = '" + ddlDivision.SelectedValue + "'";
        }

        if (ddlDepartment.SelectedIndex > 0)
        {
            parameter = parameter + "  and   alo.DepartmentId = '" + ddlDepartment.SelectedValue + "'";
        }

        //if (ddlSection.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    e.SectionId = '" + ddlSection.SelectedValue + "'";
        //}

        //if (ddlSubSection.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    e.SubSectionId = '" + ddlSubSection.SelectedValue + "'";
        //}

        //if (txtSearch.Text != "")
        //{
        //    parameter = parameter + "  and (e.EmpMasterCode LIKE     '%" + txtSearch.Text.Trim() + "%' ) ";
        //}

        //if (NameTextBox.Text != "")
        //{
        //    parameter = parameter + "  and  ( e.EmpName LIKE '%" + NameTextBox.Text.Trim() + "%')";
        //}

        if (ddlDesignation.SelectedIndex > 0)
        {
            parameter = parameter + "  and    e.DesignationId = '" + ddlDesignation.SelectedValue + "'";
        }


        if (ddlEmpInfo.SelectedValue != "")
        {
            parameter = parameter + "  and  alo.EmpInfoId=" + ddlEmpInfo.SelectedValue + "";
        }


        //if (ddlSalaryLocation.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    e.SalaryLoationId = '" + ddlSalaryLocation.SelectedValue + "'";
        //}

        //if (ddlConformationStatus.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    e.ConformationStatus = '" + ddlConformationStatus.SelectedValue + "'";
        //}

        //if (ActiveStatusDropDownList.SelectedIndex > 0)
        //{
        //    parameter = parameter + "  and    e.IsActive = '" + ActiveStatusDropDownList.SelectedValue + "'";
        //}

        if (effectiveDateTextBox.Text != string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND alo.EffectiveDate BETWEEN '" + effectiveDateTextBox.Text + "' AND '" + txtToDate.Text + "' ";
        }
        if (effectiveDateTextBox.Text != string.Empty && txtToDate.Text == string.Empty)
        {
            parameter = parameter + " AND alo.EffectiveDate BETWEEN '" + effectiveDateTextBox.Text + "' AND '" + DateTime.Now.ToString("dd-MMM-yyyy") + "' ";
        }

        if (effectiveDateTextBox.Text == string.Empty && txtToDate.Text != string.Empty)
        {
            parameter = parameter + " AND alo.EffectiveDate BETWEEN '" + txtToDate.Text + "' AND '" + txtToDate.Text + "' ";
        }


        parameter = parameter + "  and alo.ActionStatus= 'Approved'   order by alo.EffectiveDate desc";

        return parameter;
    }

    protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
    {
        var chkBoxHeader = (CheckBox)loadGridView.HeaderRow.FindControl("chkSelectAll");

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");
            chkBoxRows.Checked = chkBoxHeader.Checked;
        }
    }
    protected void submitButton_OnClick(object sender, EventArgs e)
    {


        if (Valisddate())
        {

            for (int i = 0; i < loadGridView.Rows.Count; i++)
            {
                 SaveMethod(i);
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successful...! ');window.location ='LunchAllownaceCancelSelf.aspx';",
                true);

        }
    }

    private void SaveMethod(int i)
    {
        
            LunchAllownceCancelDAO allownceCancelDao = new LunchAllownceCancelDAO();

            HiddenField hfLunchAlllowCancelId = (HiddenField)loadGridView.Rows[i].Cells[0].FindControl("hfLunchAlllowCancelId");
            TextBox lblDateData = (TextBox)loadGridView.Rows[i].Cells[0].FindControl("lblDateData");
            CheckBox chkSelect = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");
            TextBox Remark = (TextBox)loadGridView.Rows[i].FindControl("txtApprovalRemarks");

        if (chkSelect.Checked)
        {

            if (lblDateData.Text != "") {
                bool issucc = allowanceCancelDal.ApproveLunchAllowCancel(hfLunchAlllowCancelId.Value, actionRadioButtonList.SelectedValue, Remark.Text.Trim(), lblDateData.Text);
            if (issucc)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                    "alert",
                    "alert('Operation Successful...! ');window.location ='LunchAllownaceCancelApproval.aspx';",
                    true);
            }
            else
            {
                ShowMessageBox("Operation Faild!!");
            }
            }
            else
            {
                ShowMessageBox("Please Select Cancel Date!!");
                lblDateData.Focus();
            }
        }
       



    }
    private void ShowMessageBox(string message)
    {
        message = message.Replace("'", "\'");
        string sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
    }
    private bool Valisddate()
    {

         

        Int32 count = 0;

        for (int i = 0; i < loadGridView.Rows.Count; i++)
        {
            var chkBoxRows = (CheckBox)loadGridView.Rows[i].Cells[0].FindControl("chkSelect");

            if (chkBoxRows.Checked)
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
            ShowMessageBox("Please Select at least one employee !!!");
            return false;
        }
        //if ()
        //{
        //    AlertMessageBoxShow("Please Select Year...");
        //    txtToDate.Focus();
        //    return false;


        //}

        return true;
    }

    protected void Button11_OnClick(object sender, EventArgs e)
    {
        
    }


    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue != "")
        {
            _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
            _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
            _commonDataLoad.GetSectionByDivList(ddlSection, ddlDivision.SelectedValue);
            _commonDataLoad.GetSubSectionListAll(ddlSubSection, ddlDivision.SelectedValue);
        }
        else
        {
            ddlWing.Items.Clear();
            ddlDepartment.Items.Clear();
            ddlSection.Items.Clear();
            ddlSubSection.Items.Clear();
        }
    }

    protected void ddlWing_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
      
    }

    protected void ddlSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void ddlSubSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void EmployeeDropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

        string empName = txtSearch.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            //EmployeeDropDownList.Text = emp[0];
            txtSearch.Text = emp[1];
        }
        //else
        //{
        //    txtSearch.Text = "";
        //    txtSearch.Text = "";
        //    //  EmpInfoIdHiddenField.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}
    }

    protected void EmployeeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        string empName = NameTextBox.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            //EmployeeDropDownList.Text = emp[0];
            NameTextBox.Text = emp[2];

        }
        //else
        //{
        //    NameTextBox.Text = "";
        //    NameTextBox.Text = "";
        //  //  EmpInfoIdHiddenField.Value = "";
        //    aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        //}

    }

    protected void btnReset_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("LunchForGuestIntern.aspx");
    }
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();

    protected void companyDropDownList_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (companyDropDownList.SelectedIndex > 0)
        {
            Session["CompanyId"] = "";
            Session["CompanyId"] = companyDropDownList.SelectedValue;
            using (DataTable dt = _commonDataLoad.GetDDLComDivision(companyDropDownList.SelectedValue))
            {
                ddlDivision.DataSource = dt;
                ddlDivision.DataValueField = "Value";
                ddlDivision.DataTextField = "TextField";
                ddlDivision.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLComWind(companyDropDownList.SelectedValue))
            {
                ddlWing.DataSource = dt;
                ddlWing.DataValueField = "Value";
                ddlWing.DataTextField = "TextField";
                ddlWing.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComDepartment(companyDropDownList.SelectedValue))
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataValueField = "Value";
                ddlDepartment.DataTextField = "TextField";
                ddlDepartment.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComSection(companyDropDownList.SelectedValue))
            {
                ddlSection.DataSource = dt;
                ddlSection.DataValueField = "Value";
                ddlSection.DataTextField = "TextField";
                ddlSection.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComSubSection(companyDropDownList.SelectedValue))
            {
                ddlSubSection.DataSource = dt;
                ddlSubSection.DataValueField = "Value";
                ddlSubSection.DataTextField = "TextField";
                ddlSubSection.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLSalaryLocation())
            {
                ddlSalaryLocation.DataSource = dt;
                ddlSalaryLocation.DataValueField = "Value";
                ddlSalaryLocation.DataTextField = "TextField";
                ddlSalaryLocation.DataBind();
            }

            using (DataTable dt222 = _commonDataLoad.GetEmpDDLIsActive(companyDropDownList.SelectedValue.ToString()))
            {



                ddlEmpInfo.DataSource = dt222;
                ddlEmpInfo.DataValueField = "EmpInfoId";
                ddlEmpInfo.DataTextField = "EmpName";
                ddlEmpInfo.DataBind();
                ddlEmpInfo.Items.Insert(0, new ListItem("Please Select an Employee.....", String.Empty));
                ddlEmpInfo.SelectedIndex = 0;
            }
        }
        else
        {
            ddlDivision.Items.Clear();
            ddlWing.Items.Clear();
                ddlDepartment.Items.Clear();
                ddlSection.Items.Clear();
                    ddlSubSection.Items.Clear();
                    ddlSalaryLocation.Items.Clear();
        }
    }

    protected void effectiveDateTextBox_OnTextChanged(object sender, EventArgs e)
    {
        Button1_OnClick(null, null);
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("LunchForGuestInternList.aspx");
    }

    protected void rbTransferType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        divReff.Visible = false;
        if (rbTransferType.SelectedValue == "Guest" || rbTransferType.SelectedValue == "Casual")
        {
            divReff.Visible = true;
        }
    }

    protected void delButton_OnClick(object sender, EventArgs e)
    {

        try
        {
            mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
            tblunchGuestInternInformationMaster Bat = null;
            tblunchGuestInternInformation dtl = null;
            if (mid > 0)
            {
                using (var db = new HRIS_SMC_DBEntities())
                {

                    int idd = commmDAl.SavAppLog(mid,"Del");

                    Bat = (from j in db.tblunchGuestInternInformationMasters where j.GuestInternInformationMasterId == mid select j).FirstOrDefault();
                    db.tblunchGuestInternInformationMasters.Remove(Bat);
                    db.SaveChanges();

                    dtl = (from j in db.tblunchGuestInternInformations where j.MasterId == mid select j).FirstOrDefault();
                    db.tblunchGuestInternInformations.Remove(dtl);
                    db.SaveChanges();



                    ScriptManager.RegisterStartupScript(this, this.GetType(),
                     "alert",
                     "alert('Operation Successful...! ');window.location ='LunchForGuestInternList.aspx';",
                     true);
                }
            }
        }
        catch (Exception)
        {

            ShowMessageBox("Operation Faild!!");
        }
     
    }

    
}