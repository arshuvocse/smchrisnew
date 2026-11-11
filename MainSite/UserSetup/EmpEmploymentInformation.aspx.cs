using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.UserPermissions_DAL;
using DAO.HRIS_DAO;
using DAO.HRIS_DAO_EF;
using HELPER_FUNCTIONS.HELPERS;

public partial class UserSetup_EmpEmploymentInformation : System.Web.UI.Page
{
    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private int mid = 0;
    private string _userId;
    protected void Page_Load(object sender, EventArgs e)
    {
            if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {

           
            LoadInitialDDL();

            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();
                if (mid > 0)
                {
                    using (var db = new HRIS_SMCEntities())
                    {
                        try
                        {
                            var emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();
                            empMasterCode.Text =
                          emp.EmpMasterCode;

                            using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(mid))
                            {
                                lblDesignation.Text = dtdesignation.Rows[0]["Designation"].ToString();

                            }

                            lblEmpName.Text = emp.EmpName;
                            #region 2. Employment Information
                            ddlCompany.SelectedValue = emp.CompanyId.ToString();
                            ddlCompany_SelectedIndexChanged(null, null);


                            try
                            {
                                ddlDivision.SelectedValue = emp.DivisionId.ToString();
                            }
                            catch (Exception)
                            {
                                ddlDivision.SelectedValue = null;
                                //throw;
                            }

                            ddlDivision_OnSelectedIndexChanged(null, null);


                            try
                            {
                                ddlDepartment.SelectedValue = emp.DepartmentId.ToString();
                            }
                            catch (Exception)
                            {
                                ddlDepartment.SelectedValue = null;
                                //throw;
                            }
                            ddlDepartment_OnSelectedIndexChanged(null, null);
                            try
                            {
                                ddlWing.SelectedValue = emp.DivisionWId.ToString();
                            }
                            catch (Exception)
                            {
                                ddlWing.SelectedValue = null;
                                //throw;
                            }

                            try
                            {
                                ddlSection.SelectedValue = emp.SectionId.ToString();
                            }
                            catch (Exception)
                            {
                                ddlSection.SelectedValue = null;
                                // throw;
                            }

                            try
                            {
                                ddlSubSection.SelectedValue = emp.SubSectionId.ToString();
                            }
                            catch (Exception)
                            {
                                ddlSubSection.SelectedValue = null;
                                //throw;
                            }

                            try
                            {
                                ddlEmpCategory.SelectedValue = emp.EmpCategoryId.ToString();
                            }
                            catch (Exception)
                            {
                                ddlEmpCategory.SelectedValue = null;
                                //throw;
                            }
                            ddlEmpCategory_OnSelectedIndexChanged(null, null);
                            try
                            {
                                ddlSalaryGrade.SelectedValue = emp.SalaryGradeId.ToString();
                            }
                            catch (Exception)
                            {
                                ddlSalaryGrade.SelectedValue = null;
                                // throw;
                            }
                            ddlSalaryGrade_OnSelectedIndexChanged(null, null);

                            try
                            {
                                ddlSalaryStep.SelectedValue = emp.SalaryStepId.ToString();
                            }
                            catch (Exception)
                            {
                                ddlSalaryStep.SelectedValue = null;
                                //throw;
                            }

                            try
                            {
                                ddlDesignation.SelectedValue = emp.DesignationId.ToString();
                            }
                            catch (Exception)
                            {
                                ddlDesignation.SelectedValue = null;
                                //throw;
                            }
                            try
                            {
                                ddlDesignationType.SelectedValue = emp.DesignationTypeId.ToString();
                            }
                            catch (Exception)
                            {
                                ddlDesignationType.SelectedValue = null;
                                //throw;
                            }
                            if (emp.Floor != null)
                            {
                                txtFloor.Text = emp.Floor.ToString();
                            }


                            //ddlEmpType.SelectedValue = emp.EmpTypeId.ToString();
                            //ddlEmpType_OnSelectedIndexChanged(null, null);

                            try
                            {
                                ddlSalaryLocation.SelectedValue = emp.SalaryLoationId.ToString();
                            }
                            catch (Exception)
                            {
                                ddlSalaryLocation.SelectedValue = null;
                                // throw;
                            }
                            using (DataTable dt = _commonDataLoad.GetDDLJobLocation(ddlSalaryLocation.SelectedValue))
                            {
                                ddlJobLocation.DataSource = dt;
                                ddlJobLocation.DataValueField = "Value";
                                ddlJobLocation.DataTextField = "TextField";
                                ddlJobLocation.DataBind();
                            }
                            try
                            {
                                ddlJobLocation.SelectedValue = emp.JobLocationId.ToString();
                            }
                            catch (Exception)
                            {

                                ddlJobLocation.SelectedValue = null;
                                //throw;
                            }



                            #endregion end
                        }
                        catch (Exception)
                        {
                            
                            //throw;
                        }
                    }
                }

            }
        }
    }

    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex > 0)
        {
            Session["cid"] = ddlCompany.SelectedValue;
            Session["CompanyId"] = ddlCompany.SelectedValue;
            using (DataTable dt = _commonDataLoad.GetDDLComDivision(ddlCompany.SelectedValue))
            {
                ddlDivision.DataSource = dt;
                ddlDivision.DataValueField = "Value";
                ddlDivision.DataTextField = "TextField";
                ddlDivision.DataBind();
            }
            

            using (DataTable dt = _commonDataLoad.GetDDLComWind(ddlCompany.SelectedValue))
            {
                ddlWing.DataSource = dt;
                ddlWing.DataValueField = "Value";
                ddlWing.DataTextField = "TextField";
                ddlWing.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComDepartment(ddlCompany.SelectedValue))
            {
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataValueField = "Value";
                ddlDepartment.DataTextField = "TextField";
                ddlDepartment.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComSection(ddlCompany.SelectedValue))
            {
                ddlSection.DataSource = dt;
                ddlSection.DataValueField = "Value";
                ddlSection.DataTextField = "TextField";
                ddlSection.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLComSubSection(ddlCompany.SelectedValue))
            {
                ddlSubSection.DataSource = dt;
                ddlSubSection.DataValueField = "Value";
                ddlSubSection.DataTextField = "TextField";
                ddlSubSection.DataBind();
            }

            using (DataTable dt = _commonDataLoad.GetDDLComCategory())
            {
                ddlEmpCategory.DataSource = dt;
                ddlEmpCategory.DataValueField = "Value";
                ddlEmpCategory.DataTextField = "TextField";
                ddlEmpCategory.DataBind();
            }


            using (DataTable dt = _commonDataLoad.GetDDLDesignationType())
            {
                ddlDesignationType.DataSource = dt;
                ddlDesignationType.DataValueField = "Value";
                ddlDesignationType.DataTextField = "TextField";
                ddlDesignationType.DataBind();
            }

            //using (DataTable dt = _commonDataLoad.GetDDLJobLocation())
            //{
            //    ddlJobLocation.DataSource = dt;
            //    ddlJobLocation.DataValueField = "Value";
            //    ddlJobLocation.DataTextField = "TextField";
            //    ddlJobLocation.DataBind();
            //}
            using (DataTable dt = _commonDataLoad.GetDDLSalaryLocation())
            {
                ddlSalaryLocation.DataSource = dt;
                ddlSalaryLocation.DataValueField = "Value";
                ddlSalaryLocation.DataTextField = "TextField";
                ddlSalaryLocation.DataBind();
            }



        }
    }

    private void LoadInitialDDL()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDLForEdit())
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
        }

        using (DataTable dt = _commonDataLoad.GetDDLDesignation())
        {
            ddlDesignation.DataSource = dt;
            ddlDesignation.DataValueField = "Value";
            ddlDesignation.DataTextField = "TextField";
            ddlDesignation.DataBind();
        }
    }

    protected void ddlDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue != "")
        {
            ddlWing.Enabled = true;
            try
            {
                _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
            }
            catch (Exception)
            {
                
                //throw;
            }



            try
            {
                _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
            }
            catch (Exception)
            {
                
                //throw;
            }

        }
        else
        {
            ddlWing.Items.Clear();
            ddlDepartment.Items.Clear();
        }
    }

    protected void ddlEmpCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpCategory.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLSalaryGrade(ddlEmpCategory.SelectedValue))
            {
                ddlSalaryGrade.DataSource = dt;
                ddlSalaryGrade.DataValueField = "Value";
                ddlSalaryGrade.DataTextField = "TextField";
                ddlSalaryGrade.DataBind();
            }
        }
    }

    protected void ddlSalaryGrade_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSalaryGrade.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLSalaryStep(ddlSalaryGrade.SelectedValue))
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

    protected void ddlSalaryLocation_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSalaryLocation.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLJobLocation(ddlSalaryLocation.SelectedValue))
            {
                ddlJobLocation.DataSource = dt;
                ddlJobLocation.DataValueField = "Value";
                ddlJobLocation.DataTextField = "TextField";
                ddlJobLocation.DataBind();
            }
        }
    }

    protected void ddlWing_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlWing.SelectedValue != "")
        {
            _commonDataLoad.GetDepartmentList(ddlDepartment, ddlWing.SelectedValue);
        }
        else
        {
            ddlDepartment.Items.Clear();
        }
    }

    protected void ddlDepartment_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedValue != "")
            {

                DataTable dtgetdata = _commonDataLoad.GetDepartmentRelaton(ddlDepartment.SelectedValue, "");
                if (dtgetdata.Rows.Count > 0)
                {
                    if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
                    {
                      //  ddlWing.Enabled = false;
                        ddlWing.CssClass = "form-control form-control-sm";
                        ddlWing.Items.Clear();
                        try
                        {
                            _commonDataLoad.GetDivisionWingListAll(ddlWing, ddlDivision.SelectedValue);
                        }
                        catch (Exception)
                        {
                            ddlWing.SelectedValue = null;
                            //throw;
                        }
                        // ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                    }
                    else
                    {
                        ddlWing.Enabled = true;
                        ddlWing.CssClass = "form-control form-control-sm";
                        ddlWing.Items.Clear();
                        try
                        {
                            _commonDataLoad.GetDivisionWingListAll(ddlWing, ddlDivision.SelectedValue);
                        }
                        catch (Exception)
                        {
                            ddlWing.SelectedValue = null;
                            //throw;
                        }

                    }
                }
            }
            else
            {

            }
            if (ddlDepartment.SelectedIndex == 0)
            {
                ddlWing.Enabled = false;
                ddlWing.CssClass = "form-control form-control-sm";
                ddlWing.SelectedValue = "";
                //  ddlWing.DataBind();
                try
                {
                    _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
                }
                catch (Exception)
                {
                    ddlWing.SelectedValue = null;
                    //throw;
                }
            }
        }
        catch (Exception)
        {

            //throw;
        }
    }

    protected void ddlSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtgetdata1 = _commonDataLoad.GetSectionRelaton(ddlSection.SelectedValue, "");
        if (dtgetdata1.Rows.Count > 0)
        {
            if (dtgetdata1.Rows[0]["Invisible"].ToString() == "True")
            {
                dept.Visible = false;
                ddlDepartment.Items.Clear();
                _commonDataLoad.GetDepartmentByDivListAll(ddlDepartment, ddlDivision.SelectedValue);
                ddlDepartment.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
            else
            {
                dept.Visible = true;
                ddlDepartment.Items.Clear();
                _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
                try
                {
                    ddlDepartment.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
                }
                catch (Exception)
                {
                    
                    //throw;
                }
            }
        }
        DataTable dtgetdata = _commonDataLoad.GetDepartmentRelaton(ddlDepartment.SelectedValue, "");
        if (dtgetdata.Rows.Count > 0)
        {
            if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
            {
                wing.Visible = false;
                ddlWing.Items.Clear();
                _commonDataLoad.GetDivisionWingListAll(ddlWing, ddlDivision.SelectedValue);
                try
                {
                    ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                }
                catch (Exception)
                {
                    
                    //throw;
                }
            }
            else
            {
                wing.Visible = true;
                ddlWing.Items.Clear();
                _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);

                try
                {
                    ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                }
                catch (Exception)
                {

                    //throw;
                }
                
            }
        }
        if (ddlSection.SelectedIndex == 0)
        {
            if (wing.Visible == false)
            {
                wing.Visible = true;
                ddlWing.SelectedValue = null;
                ddlWing.DataBind();
                _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);

            }
            if (dept.Visible == false)
            {
                dept.Visible = true;
                ddlDepartment.SelectedValue = null;
                ddlDepartment.DataBind();
                _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
            }
        }
    }

    protected void ddlSubSection_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtgetdata2 = _commonDataLoad.GetSubSectionRelaton(ddlSubSection.SelectedValue, "");
        if (dtgetdata2.Rows.Count > 0)
        {
            if (dtgetdata2.Rows[0]["Invisible"].ToString() == "True")
            {
                sec.Visible = false;
                ddlSection.Items.Clear();
                _commonDataLoad.GetSectionByDivListAll(ddlSection, ddlDivision.SelectedValue);
                try
                {
                    ddlSection.SelectedValue = dtgetdata2.Rows[0]["SectionId"].ToString();
                }
                catch (Exception)
                {
                    
                    //throw;
                }
            }
            else
            {
                sec.Visible = true;
                ddlSection.Items.Clear();
                _commonDataLoad.GetSectionByDivList(ddlSection, ddlDivision.SelectedValue);
                try
                {
                    ddlSection.SelectedValue = dtgetdata2.Rows[0]["SectionId"].ToString();
                }
                catch (Exception)
                {
                    
                   // throw;
                }
            }
        }
        DataTable dtgetdata1 = _commonDataLoad.GetSectionRelaton(ddlSection.SelectedValue, "");
        if (dtgetdata1.Rows.Count > 0)
        {
            if (dtgetdata1.Rows[0]["Invisible"].ToString() == "True")
            {
                dept.Visible = false;
                ddlDepartment.Items.Clear();
                _commonDataLoad.GetDepartmentByDivListAll(ddlDepartment, ddlDivision.SelectedValue);
                ddlDepartment.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
            else
            {
                dept.Visible = true;
                ddlDepartment.Items.Clear();
                _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
                ddlDepartment.SelectedValue = dtgetdata1.Rows[0]["DepartmentId"].ToString();
            }
        }
        DataTable dtgetdata = _commonDataLoad.GetDepartmentRelaton(ddlDepartment.SelectedValue, "");
        if (dtgetdata.Rows.Count > 0)
        {
            if (dtgetdata.Rows[0]["Invisible"].ToString() == "True")
            {
                wing.Visible = false;
                ddlWing.Items.Clear();
                _commonDataLoad.GetDivisionWingListAll(ddlWing, ddlDivision.SelectedValue);
                try
                {
                    ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
                }
                catch (Exception)
                {
                    
                    //throw;
                }
            }
            else
            {
                wing.Visible = true;
                ddlWing.Items.Clear();
                _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);
                ddlWing.SelectedValue = dtgetdata.Rows[0]["DivisionWId"].ToString();
            }
        }

        if (ddlSubSection.SelectedIndex == 0)
        {
            if (wing.Visible == false)
            {
                wing.Visible = true;
                ddlWing.SelectedValue = null;
                ddlWing.DataBind();
                _commonDataLoad.GetDivisionWingList(ddlWing, ddlDivision.SelectedValue);

            }
            if (dept.Visible == false)
            {
                dept.Visible = true;
                ddlDepartment.SelectedValue = null;
                ddlDepartment.DataBind();
                _commonDataLoad.GetDepartmentByDivList(ddlDepartment, ddlDivision.SelectedValue);
            }
            if (sec.Visible == false)
            {
                sec.Visible = true;
                ddlSection.SelectedValue = null;
                ddlSection.DataBind();
                _commonDataLoad.GetSectionByDivList(ddlSection, ddlDivision.SelectedValue);
            }
        }
    }

    protected void btn_Edit_OnClick(object sender, EventArgs e)
    {
        
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            #region fff

            try
            {
                string EmpMasterCode = string.Empty;
                mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
                tblEmpGeneralInfo emp = null;
                using (var db = new HRIS_SMCEntities())
                {
                    if (mid > 0)
                    {
                        emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();
                        EmpMasterCode = emp.EmpMasterCode;

                        #region 2. Employment Information
                        emp.CompanyId = ddlCompany.SelectedIndex > 0 ? int.Parse(ddlCompany.SelectedValue) : (int?)null;
                        emp.DivisionId = ddlDivision.SelectedIndex > 0 ? int.Parse(ddlDivision.SelectedValue) : (int?)null;
                        emp.DivisionWId = ddlWing.SelectedIndex > 0 ? int.Parse(ddlWing.SelectedValue) : (int?)null;
                        emp.DepartmentId = ddlDepartment.SelectedIndex > 0 ? int.Parse(ddlDepartment.SelectedValue) : (int?)null;

                        emp.SectionId = ddlSection.SelectedIndex > 0 ? int.Parse(ddlSection.SelectedValue) : (int?)null;
                        emp.SubSectionId = ddlSubSection.SelectedIndex > 0 ? int.Parse(ddlSubSection.SelectedValue) : (int?)null;
                        emp.EmpCategoryId = ddlEmpCategory.SelectedIndex > 0 ? int.Parse(ddlEmpCategory.SelectedValue) : (int?)null;
                        emp.SalaryGradeId = ddlSalaryGrade.SelectedIndex > 0 ? int.Parse(ddlSalaryGrade.SelectedValue) : (int?)null;

                        emp.SalaryStepId = ddlSalaryStep.SelectedIndex > 0 ? int.Parse(ddlSalaryStep.SelectedValue) : (int?)null;
                        emp.DesignationId = ddlDesignation.SelectedIndex > 0 ? int.Parse(ddlDesignation.SelectedValue) : (int?)null;
                        emp.DesignationTypeId = ddlDesignationType.SelectedIndex > 0 ? int.Parse(ddlDesignationType.SelectedValue) : (int?)null;
                 //       emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
                        emp.JobLocationId = ddlJobLocation.SelectedIndex > 0 ? int.Parse(ddlJobLocation.SelectedValue) : (int?)null;

                        emp.SalaryLoationId = ddlSalaryLocation.SelectedIndex > 0 ? int.Parse(ddlSalaryLocation.SelectedValue) : (int?)null;
                        emp.Floor = txtFloor.Text;
                        emp.Updateby = _userId;
                        emp.UpdateDate = DateTime.Now;
                        #endregion
                        db.SaveChanges();
                    }

                    else
                    {

                        //emp = new tblEmpGeneralInfo();
                        //#region 2. Employment Information
                        //emp.CompanyId = ddlCompany.SelectedIndex > 0 ? int.Parse(ddlCompany.SelectedValue) : (int?)null;
                        //emp.DivisionId = ddlDivision.SelectedIndex > 0 ? int.Parse(ddlDivision.SelectedValue) : (int?)null;
                        //emp.DivisionWId = ddlWing.SelectedIndex > 0 ? int.Parse(ddlWing.SelectedValue) : (int?)null;
                        //emp.DepartmentId = ddlDepartment.SelectedIndex > 0 ? int.Parse(ddlDepartment.SelectedValue) : (int?)null;

                        //emp.SectionId = ddlSection.SelectedIndex > 0 ? int.Parse(ddlSection.SelectedValue) : (int?)null;
                        //emp.SubSectionId = ddlSubSection.SelectedIndex > 0 ? int.Parse(ddlSubSection.SelectedValue) : (int?)null;
                        //emp.EmpCategoryId = ddlEmpCategory.SelectedIndex > 0 ? int.Parse(ddlEmpCategory.SelectedValue) : (int?)null;
                        //emp.SalaryGradeId = ddlSalaryGrade.SelectedIndex > 0 ? int.Parse(ddlSalaryGrade.SelectedValue) : (int?)null;

                        //emp.SalaryStepId = ddlSalaryStep.SelectedIndex > 0 ? int.Parse(ddlSalaryStep.SelectedValue) : (int?)null;
                        //emp.DesignationId = ddlDesignation.SelectedIndex > 0 ? int.Parse(ddlDesignation.SelectedValue) : (int?)null;
                        //emp.DesignationTypeId = ddlDesignationType.SelectedIndex > 0 ? int.Parse(ddlDesignationType.SelectedValue) : (int?)null;
                       
                        //emp.JobLocationId = ddlJobLocation.SelectedIndex > 0 ? int.Parse(ddlJobLocation.SelectedValue) : (int?)null;

                        //emp.SalaryLoationId = ddlSalaryLocation.SelectedIndex > 0 ? int.Parse(ddlSalaryLocation.SelectedValue) : (int?)null;
                        //emp.Floor = txtFloor.Text;
                        //#endregion end


                        //db.tblEmpGeneralInfoes.Add(emp);
                        //db.SaveChanges();
                        //EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
                        //////Below stored procedure will generate Emp Master Code based on condition, update on database and return the value
                        //using (DataTable dtEmpCode = _empdal.GetEmpMasterCode(emp.EmpInfoId))
                        //{
                        //    if (dtEmpCode.Rows.Count > 0)
                        //    {
                        //        EmpMasterCode = dtEmpCode.Rows[0]["EmpMasterCode"].ToString();
                        //    }

                        //}
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                 "alert",
                 "alert('Operation Successful...! Employee Master Code: " + EmpMasterCode + "');window.location ='EmployeeInfoList.aspx';",
                 true);
            }
            catch (Exception ex)
            {
                AlertMessageBoxShow(ex.Message);
            }

              #endregion end
 
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
    protected void btnEditInfo_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoList.aspx");
    }
    private bool Validation()
    {


        if (ddlCompany.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Company", this);
            ddlCompany.Focus();
            return false;
        }
        if (ddlDivision.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Division", this);
            ddlDivision.Focus();
            return false;
        }
        //if (ddlDepartment.SelectedIndex <= 0)
        //{
        //    aShowMessage.ShowMessageBox("Please Select Department", this);
        //    ddlDepartment.Focus();
        //    return false;
        //}

        if (ddlEmpCategory.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Employee Category", this);
            ddlEmpCategory.Focus();
            return false;
        }

        if (ddlSalaryGrade.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Salary Grade", this);
            ddlSalaryGrade.Focus();
            return false;
        }
        if (ddlSalaryStep.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Salary Step", this);
            ddlSalaryStep.Focus();
            return false;
        }

        if (ddlDesignation.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Designation", this);
            ddlDesignation.Focus();
            return false;
        }


        if (ddlSalaryLocation.SelectedIndex <= 0)
        {
            aShowMessage.ShowMessageBox("Please Select Office", this);
            ddlSalaryLocation.Focus();
            return false;
        }
       
      
       



        return true;
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoListUpdate.aspx");
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoList.aspx");
    }

    protected void btn_Next_OnClick(object sender, EventArgs e)
    {
        if (Validation())
        {
            #region fff

            try
            {
                string MasterId = string.Empty;
                string EmpMasterCode = string.Empty;
                mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
                tblEmpGeneralInfo emp = null;
                using (var db = new HRIS_SMCEntities())
                {
                    if (mid > 0)
                    {
                        emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();
                        EmpMasterCode = emp.EmpMasterCode;
                        MasterId = emp.EmpInfoId.ToString();
                        #region 2. Employment Information
                        emp.CompanyId = ddlCompany.SelectedIndex > 0 ? int.Parse(ddlCompany.SelectedValue) : (int?)null;
                        emp.DivisionId = ddlDivision.SelectedIndex > 0 ? int.Parse(ddlDivision.SelectedValue) : (int?)null;
                        emp.DivisionWId = ddlWing.SelectedIndex > 0 ? int.Parse(ddlWing.SelectedValue) : (int?)null;
                        emp.DepartmentId = ddlDepartment.SelectedIndex > 0 ? int.Parse(ddlDepartment.SelectedValue) : (int?)null;

                        emp.SectionId = ddlSection.SelectedIndex > 0 ? int.Parse(ddlSection.SelectedValue) : (int?)null;
                        emp.SubSectionId = ddlSubSection.SelectedIndex > 0 ? int.Parse(ddlSubSection.SelectedValue) : (int?)null;
                        emp.EmpCategoryId = ddlEmpCategory.SelectedIndex > 0 ? int.Parse(ddlEmpCategory.SelectedValue) : (int?)null;
                        emp.SalaryGradeId = ddlSalaryGrade.SelectedIndex > 0 ? int.Parse(ddlSalaryGrade.SelectedValue) : (int?)null;

                        emp.SalaryStepId = ddlSalaryStep.SelectedIndex > 0 ? int.Parse(ddlSalaryStep.SelectedValue) : (int?)null;
                        emp.DesignationId = ddlDesignation.SelectedIndex > 0 ? int.Parse(ddlDesignation.SelectedValue) : (int?)null;
                        emp.DesignationTypeId = ddlDesignationType.SelectedIndex > 0 ? int.Parse(ddlDesignationType.SelectedValue) : (int?)null;
                        //       emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
                        emp.JobLocationId = ddlJobLocation.SelectedIndex > 0 ? int.Parse(ddlJobLocation.SelectedValue) : (int?)null;

                        emp.SalaryLoationId = ddlSalaryLocation.SelectedIndex > 0 ? int.Parse(ddlSalaryLocation.SelectedValue) : (int?)null;
                        emp.Floor = txtFloor.Text;
                        emp.Updateby = _userId;
                        emp.UpdateDate = DateTime.Now;
                        #endregion
                        db.SaveChanges();
                    }

                    else
                    {

                        //emp = new tblEmpGeneralInfo();
                        //#region 2. Employment Information
                        //emp.CompanyId = ddlCompany.SelectedIndex > 0 ? int.Parse(ddlCompany.SelectedValue) : (int?)null;
                        //emp.DivisionId = ddlDivision.SelectedIndex > 0 ? int.Parse(ddlDivision.SelectedValue) : (int?)null;
                        //emp.DivisionWId = ddlWing.SelectedIndex > 0 ? int.Parse(ddlWing.SelectedValue) : (int?)null;
                        //emp.DepartmentId = ddlDepartment.SelectedIndex > 0 ? int.Parse(ddlDepartment.SelectedValue) : (int?)null;

                        //emp.SectionId = ddlSection.SelectedIndex > 0 ? int.Parse(ddlSection.SelectedValue) : (int?)null;
                        //emp.SubSectionId = ddlSubSection.SelectedIndex > 0 ? int.Parse(ddlSubSection.SelectedValue) : (int?)null;
                        //emp.EmpCategoryId = ddlEmpCategory.SelectedIndex > 0 ? int.Parse(ddlEmpCategory.SelectedValue) : (int?)null;
                        //emp.SalaryGradeId = ddlSalaryGrade.SelectedIndex > 0 ? int.Parse(ddlSalaryGrade.SelectedValue) : (int?)null;

                        //emp.SalaryStepId = ddlSalaryStep.SelectedIndex > 0 ? int.Parse(ddlSalaryStep.SelectedValue) : (int?)null;
                        //emp.DesignationId = ddlDesignation.SelectedIndex > 0 ? int.Parse(ddlDesignation.SelectedValue) : (int?)null;
                        //emp.DesignationTypeId = ddlDesignationType.SelectedIndex > 0 ? int.Parse(ddlDesignationType.SelectedValue) : (int?)null;

                        //emp.JobLocationId = ddlJobLocation.SelectedIndex > 0 ? int.Parse(ddlJobLocation.SelectedValue) : (int?)null;

                        //emp.SalaryLoationId = ddlSalaryLocation.SelectedIndex > 0 ? int.Parse(ddlSalaryLocation.SelectedValue) : (int?)null;
                        //emp.Floor = txtFloor.Text;
                        //#endregion end


                        //db.tblEmpGeneralInfoes.Add(emp);
                        //db.SaveChanges();


                        //EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();

                        //MasterId = emp.EmpInfoId.ToString();
                        //////Below stored procedure will generate Emp Master Code based on condition, update on database and return the value
                        //using (DataTable dtEmpCode = _empdal.GetEmpMasterCode(emp.EmpInfoId))
                        //{
                        //    if (dtEmpCode.Rows.Count > 0)
                        //    {
                        //        EmpMasterCode = dtEmpCode.Rows[0]["EmpMasterCode"].ToString();
                        //    }

                        //}
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                 "alert",
                 "alert('Operation Successful...! Employee Master Code: " + EmpMasterCode + "');window.location ='EmpContacts.aspx?mid=" + MasterId + "';",
                 true);
            }
            catch (Exception ex)
            {
                AlertMessageBoxShow(ex.Message);
            }

            #endregion end

        }
    }

    protected void btnSaveExitt_OnClick(object sender, EventArgs e)
    {
       
    }

    protected void lblNext_OnClick(object sender, EventArgs e)
    {
        string EmpId = Request.QueryString["mid"];
        if (Convert.ToInt32(EmpId) > 0)
        {
            Response.Redirect("EmpContacts?mid=" + EmpId);
        }
        else
        {
            Response.Redirect("EmployeeInfoListUpdate.aspx");
        }
    }

    protected void lbPrevious_OnClick(object sender, EventArgs e)
    {
        string EmpId = Request.QueryString["mid"];
        if (Convert.ToInt32(EmpId) > 0)
        {
            Response.Redirect("EmpGeneral?mid=" + EmpId);
        }
        else
        {
            Response.Redirect("EmployeeInfoListUpdate.aspx");
        }
    }
}