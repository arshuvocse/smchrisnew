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

public partial class UserSetup_EmpContacts : System.Web.UI.Page
{

    ShowMessage aShowMessage = new ShowMessage();
    Messages aMessages = new Messages();
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private int mid = 0;
    private string _userId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != "")
        {
            _userId = Convert.ToString(Session["UserId"].ToString());
        }
        if (!IsPostBack)
        {

            LoadDropDownList();
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();
                if (mid > 0)
                {
                    using (var db = new HRIS_SMCEntities())
                    {
                        var emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();
                        empMasterCode.Text =
                      emp.EmpMasterCode;


                        using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(mid))
                        {
                            lblDesignation.Text = dtdesignation.Rows[0]["Designation"].ToString();

                        }

                        lblEmpName.Text = emp.EmpName;


                        using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(mid))
                        {
                            lblDesignation.Text = dtdesignation.Rows[0]["Designation"].ToString();

                        }


                        #region 3. Contacts

                        txt_EmpPresentAddress.Text = emp.AddressPresent;
                        ddlEmpPresentDivision.SelectedValue = emp.PresentDivision.ToString();
                        ddlEmpPresentDivision_OnSelectedIndexChanged(null, null);
                        ddlEmpPresentDist.SelectedValue = emp.PresentDistrict.ToString();
                        ddlEmpPresentDist_OnSelectedIndexChanged(null, null);
                        ddlEmpPresentThana.SelectedValue = emp.PresentThana.ToString();

                        txt_EmpPresentTelNo.Text = emp.PresentTelNo;
                        txt_EmpParmanentAddress.Text = emp.AddressPermanent;
                        ddlEmpParmanentDivision.SelectedValue = emp.ParmanentDivision.ToString();
                        ddlEmpParmanentDivision_OnSelectedIndexChanged(null, null);
                        ddlEmpParmanentDistrict.SelectedValue = emp.PermanentDistrict.ToString();
                        ddlEmpParmanentDistrict_OnSelectedIndexChanged(null, null);
                        ddlEmpParmanentThana.SelectedValue = emp.PermanentThana.ToString();

                        txt_EmpParmanentTelNo.Text = emp.ParmanentTelNo;

                        txt_EmpPersonalEmail.Text = emp.PersonalEmail;
                        txt_EmpOfficialEmail.Text = emp.OfficialEmail;
                        txt_EmpPersonalMobile.Text = emp.PersonalMobile;
                        txt_EmpOfficialMobile.Text = emp.OfficialMobile;
                        txt_EmpFax.Text = emp.FaxNo;
                        txt_EmpEmergencyPerson.Text = emp.EmergencyContactPerson;
                        txt_EmpEmergencyAddress.Text = emp.EmergencyContactAddress;
                        txt_EmpEmergencyNumber.Text = emp.EmergencyContactNumber;
                        #endregion
                    }
                }
            }
        }
    }
    protected void homeButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("../DashBoard_UI/DashBoard.aspx");
    }
    protected void btnEditInfo_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoList.aspx");
    }
    private void LoadDropDownList()
    {
        using (DataTable dt = _commonDataLoad.GetDDLAddressDivision())
        {
            ddlEmpPresentDivision.DataSource = dt;
            ddlEmpPresentDivision.DataValueField = "Value";
            ddlEmpPresentDivision.DataTextField = "TextField";
            ddlEmpPresentDivision.DataBind();

            ddlEmpParmanentDivision.DataSource = dt;
            ddlEmpParmanentDivision.DataValueField = "Value";
            ddlEmpParmanentDivision.DataTextField = "TextField";
            ddlEmpParmanentDivision.DataBind();
        }
    }


    protected void ddlEmpPresentDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpPresentDivision.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLDistrict(ddlEmpPresentDivision.SelectedValue))
            {
                ddlEmpPresentDist.DataSource = dt;
                ddlEmpPresentDist.DataValueField = "Value";
                ddlEmpPresentDist.DataTextField = "TextField";
                ddlEmpPresentDist.DataBind();
            }
        }
    }

    protected void ddlEmpParmanentDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpParmanentDivision.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLDistrict(ddlEmpParmanentDivision.SelectedValue))
            {
                ddlEmpParmanentDistrict.DataSource = dt;
                ddlEmpParmanentDistrict.DataValueField = "Value";
                ddlEmpParmanentDistrict.DataTextField = "TextField";
                ddlEmpParmanentDistrict.DataBind();
            }
        }
    }

    protected void ddlEmpParmanentDistrict_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpParmanentDistrict.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLThana(ddlEmpParmanentDistrict.SelectedValue))
            {
                ddlEmpParmanentThana.DataSource = dt;
                ddlEmpParmanentThana.DataValueField = "Value";
                ddlEmpParmanentThana.DataTextField = "TextField";
                ddlEmpParmanentThana.DataBind();
            }
        }
    }

    


    protected void ddlEmpPresentDist_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpPresentDist.SelectedIndex > 0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLThana(ddlEmpPresentDist.SelectedValue))
            {
                ddlEmpPresentThana.DataSource = dt;
                ddlEmpPresentThana.DataValueField = "Value";
                ddlEmpPresentThana.DataTextField = "TextField";
                ddlEmpPresentThana.DataBind();
            }
        }
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
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

                    #region 3. Contacts

                    emp.AddressPresent = string.IsNullOrEmpty(txt_EmpPresentAddress.Text) ? null : txt_EmpPresentAddress.Text;
                    emp.PresentDivision = ddlEmpPresentDivision.SelectedIndex > 0 ? int.Parse(ddlEmpPresentDivision.SelectedValue) : (int?)null;
                    emp.PresentDistrict = ddlEmpPresentDist.SelectedIndex > 0 ? int.Parse(ddlEmpPresentDist.SelectedValue) : (int?)null;
                    emp.PresentThana = ddlEmpPresentThana.SelectedIndex > 0 ? int.Parse(ddlEmpPresentThana.SelectedValue) : (int?)null;

                    emp.PresentTelNo = string.IsNullOrEmpty(txt_EmpPresentTelNo.Text) ? null : txt_EmpPresentTelNo.Text;
                    emp.AddressPermanent = string.IsNullOrEmpty(txt_EmpParmanentAddress.Text) ? null : txt_EmpParmanentAddress.Text;
                    emp.ParmanentDivision = ddlEmpParmanentDivision.SelectedIndex > 0 ? int.Parse(ddlEmpParmanentDivision.SelectedValue) : (int?)null;
                    emp.PermanentDistrict = ddlEmpParmanentDistrict.SelectedIndex > 0 ? int.Parse(ddlEmpParmanentDistrict.SelectedValue) : (int?)null;
                    emp.PermanentThana = ddlEmpParmanentThana.SelectedIndex > 0 ? int.Parse(ddlEmpParmanentThana.SelectedValue) : (int?)null;
                    emp.ParmanentTelNo = string.IsNullOrEmpty(txt_EmpParmanentTelNo.Text) ? null : txt_EmpParmanentTelNo.Text;

                    emp.PersonalEmail = string.IsNullOrEmpty(txt_EmpPersonalEmail.Text) ? null : txt_EmpPersonalEmail.Text;
                    emp.OfficialEmail = string.IsNullOrEmpty(txt_EmpOfficialEmail.Text) ? null : txt_EmpOfficialEmail.Text;
                    emp.PersonalMobile = string.IsNullOrEmpty(txt_EmpPersonalMobile.Text) ? null : txt_EmpPersonalMobile.Text;
                    emp.OfficialMobile = string.IsNullOrEmpty(txt_EmpOfficialMobile.Text) ? null : txt_EmpOfficialMobile.Text;
                    emp.FaxNo = string.IsNullOrEmpty(txt_EmpFax.Text) ? null : txt_EmpFax.Text;
                    emp.EmergencyContactPerson = string.IsNullOrEmpty(txt_EmpEmergencyPerson.Text) ? null : txt_EmpEmergencyPerson.Text;
                    emp.EmergencyContactAddress = string.IsNullOrEmpty(txt_EmpEmergencyAddress.Text) ? null : txt_EmpEmergencyAddress.Text;
                    emp.EmergencyContactNumber = string.IsNullOrEmpty(txt_EmpEmergencyNumber.Text) ? null : txt_EmpEmergencyNumber.Text;
                    #endregion
                    db.SaveChanges();
                }
                else
                {

                    //emp = new tblEmpGeneralInfo();
                    //#region 3. Contacts

                    //emp.AddressPresent = string.IsNullOrEmpty(txt_EmpPresentAddress.Text) ? null : txt_EmpPresentAddress.Text;
                    //emp.PresentDivision = ddlEmpPresentDivision.SelectedIndex > 0 ? int.Parse(ddlEmpPresentDivision.SelectedValue) : (int?)null;
                    //emp.PresentDistrict = ddlEmpPresentDist.SelectedIndex > 0 ? int.Parse(ddlEmpPresentDist.SelectedValue) : (int?)null;
                    //emp.PresentThana = ddlEmpPresentThana.SelectedIndex > 0 ? int.Parse(ddlEmpPresentThana.SelectedValue) : (int?)null;

                    //emp.PresentTelNo = string.IsNullOrEmpty(txt_EmpPresentTelNo.Text) ? null : txt_EmpPresentTelNo.Text;
                    //emp.AddressPermanent = string.IsNullOrEmpty(txt_EmpParmanentAddress.Text) ? null : txt_EmpParmanentAddress.Text;
                    //emp.ParmanentDivision = ddlEmpParmanentDivision.SelectedIndex > 0 ? int.Parse(ddlEmpParmanentDivision.SelectedValue) : (int?)null;
                    //emp.PermanentDistrict = ddlEmpParmanentDistrict.SelectedIndex > 0 ? int.Parse(ddlEmpParmanentDistrict.SelectedValue) : (int?)null;
                    //emp.PermanentThana = ddlEmpParmanentThana.SelectedIndex > 0 ? int.Parse(ddlEmpParmanentThana.SelectedValue) : (int?)null;
                    //emp.ParmanentTelNo = string.IsNullOrEmpty(txt_EmpParmanentTelNo.Text) ? null : txt_EmpParmanentTelNo.Text;

                    //emp.PersonalEmail = string.IsNullOrEmpty(txt_EmpPersonalEmail.Text) ? null : txt_EmpPersonalEmail.Text;
                    //emp.OfficialEmail = string.IsNullOrEmpty(txt_EmpOfficialEmail.Text) ? null : txt_EmpOfficialEmail.Text;
                    //emp.PersonalMobile = string.IsNullOrEmpty(txt_EmpPersonalMobile.Text) ? null : txt_EmpPersonalMobile.Text;
                    //emp.OfficialMobile = string.IsNullOrEmpty(txt_EmpOfficialMobile.Text) ? null : txt_EmpOfficialMobile.Text;
                    //emp.FaxNo = string.IsNullOrEmpty(txt_EmpFax.Text) ? null : txt_EmpFax.Text;
                    //emp.EmergencyContactPerson = string.IsNullOrEmpty(txt_EmpEmergencyPerson.Text) ? null : txt_EmpEmergencyPerson.Text;
                    //emp.EmergencyContactAddress = string.IsNullOrEmpty(txt_EmpEmergencyAddress.Text) ? null : txt_EmpEmergencyAddress.Text;
                    //emp.EmergencyContactNumber = string.IsNullOrEmpty(txt_EmpEmergencyNumber.Text) ? null : txt_EmpEmergencyNumber.Text;
                    //#endregion

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
        #endregion
    }
    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }

    protected void btn_Next_OnClick(object sender, EventArgs e)
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

                    #region 3. Contacts
                    MasterId = emp.EmpInfoId.ToString();
                    emp.AddressPresent = string.IsNullOrEmpty(txt_EmpPresentAddress.Text) ? null : txt_EmpPresentAddress.Text;
                    emp.PresentDivision = ddlEmpPresentDivision.SelectedIndex > 0 ? int.Parse(ddlEmpPresentDivision.SelectedValue) : (int?)null;
                    emp.PresentDistrict = ddlEmpPresentDist.SelectedIndex > 0 ? int.Parse(ddlEmpPresentDist.SelectedValue) : (int?)null;
                    emp.PresentThana = ddlEmpPresentThana.SelectedIndex > 0 ? int.Parse(ddlEmpPresentThana.SelectedValue) : (int?)null;

                    emp.PresentTelNo = string.IsNullOrEmpty(txt_EmpPresentTelNo.Text) ? null : txt_EmpPresentTelNo.Text;
                    emp.AddressPermanent = string.IsNullOrEmpty(txt_EmpParmanentAddress.Text) ? null : txt_EmpParmanentAddress.Text;
                    emp.ParmanentDivision = ddlEmpParmanentDivision.SelectedIndex > 0 ? int.Parse(ddlEmpParmanentDivision.SelectedValue) : (int?)null;
                    emp.PermanentDistrict = ddlEmpParmanentDistrict.SelectedIndex > 0 ? int.Parse(ddlEmpParmanentDistrict.SelectedValue) : (int?)null;
                    emp.PermanentThana = ddlEmpParmanentThana.SelectedIndex > 0 ? int.Parse(ddlEmpParmanentThana.SelectedValue) : (int?)null;
                    emp.ParmanentTelNo = string.IsNullOrEmpty(txt_EmpParmanentTelNo.Text) ? null : txt_EmpParmanentTelNo.Text;

                    emp.PersonalEmail = string.IsNullOrEmpty(txt_EmpPersonalEmail.Text) ? null : txt_EmpPersonalEmail.Text;
                    emp.OfficialEmail = string.IsNullOrEmpty(txt_EmpOfficialEmail.Text) ? null : txt_EmpOfficialEmail.Text;
                    emp.PersonalMobile = string.IsNullOrEmpty(txt_EmpPersonalMobile.Text) ? null : txt_EmpPersonalMobile.Text;
                    emp.OfficialMobile = string.IsNullOrEmpty(txt_EmpOfficialMobile.Text) ? null : txt_EmpOfficialMobile.Text;
                    emp.FaxNo = string.IsNullOrEmpty(txt_EmpFax.Text) ? null : txt_EmpFax.Text;
                    emp.EmergencyContactPerson = string.IsNullOrEmpty(txt_EmpEmergencyPerson.Text) ? null : txt_EmpEmergencyPerson.Text;
                    emp.EmergencyContactAddress = string.IsNullOrEmpty(txt_EmpEmergencyAddress.Text) ? null : txt_EmpEmergencyAddress.Text;
                    emp.EmergencyContactNumber = string.IsNullOrEmpty(txt_EmpEmergencyNumber.Text) ? null : txt_EmpEmergencyNumber.Text;
                    #endregion
                    db.SaveChanges();
                }
                else
                {

                    //emp = new tblEmpGeneralInfo();
                    //#region 3. Contacts

                    //emp.AddressPresent = string.IsNullOrEmpty(txt_EmpPresentAddress.Text) ? null : txt_EmpPresentAddress.Text;
                    //emp.PresentDivision = ddlEmpPresentDivision.SelectedIndex > 0 ? int.Parse(ddlEmpPresentDivision.SelectedValue) : (int?)null;
                    //emp.PresentDistrict = ddlEmpPresentDist.SelectedIndex > 0 ? int.Parse(ddlEmpPresentDist.SelectedValue) : (int?)null;
                    //emp.PresentThana = ddlEmpPresentThana.SelectedIndex > 0 ? int.Parse(ddlEmpPresentThana.SelectedValue) : (int?)null;

                    //emp.PresentTelNo = string.IsNullOrEmpty(txt_EmpPresentTelNo.Text) ? null : txt_EmpPresentTelNo.Text;
                    //emp.AddressPermanent = string.IsNullOrEmpty(txt_EmpParmanentAddress.Text) ? null : txt_EmpParmanentAddress.Text;
                    //emp.ParmanentDivision = ddlEmpParmanentDivision.SelectedIndex > 0 ? int.Parse(ddlEmpParmanentDivision.SelectedValue) : (int?)null;
                    //emp.PermanentDistrict = ddlEmpParmanentDistrict.SelectedIndex > 0 ? int.Parse(ddlEmpParmanentDistrict.SelectedValue) : (int?)null;
                    //emp.PermanentThana = ddlEmpParmanentThana.SelectedIndex > 0 ? int.Parse(ddlEmpParmanentThana.SelectedValue) : (int?)null;
                    //emp.ParmanentTelNo = string.IsNullOrEmpty(txt_EmpParmanentTelNo.Text) ? null : txt_EmpParmanentTelNo.Text;

                    //emp.PersonalEmail = string.IsNullOrEmpty(txt_EmpPersonalEmail.Text) ? null : txt_EmpPersonalEmail.Text;
                    //emp.OfficialEmail = string.IsNullOrEmpty(txt_EmpOfficialEmail.Text) ? null : txt_EmpOfficialEmail.Text;
                    //emp.PersonalMobile = string.IsNullOrEmpty(txt_EmpPersonalMobile.Text) ? null : txt_EmpPersonalMobile.Text;
                    //emp.OfficialMobile = string.IsNullOrEmpty(txt_EmpOfficialMobile.Text) ? null : txt_EmpOfficialMobile.Text;
                    //emp.FaxNo = string.IsNullOrEmpty(txt_EmpFax.Text) ? null : txt_EmpFax.Text;
                    //emp.EmergencyContactPerson = string.IsNullOrEmpty(txt_EmpEmergencyPerson.Text) ? null : txt_EmpEmergencyPerson.Text;
                    //emp.EmergencyContactAddress = string.IsNullOrEmpty(txt_EmpEmergencyAddress.Text) ? null : txt_EmpEmergencyAddress.Text;
                    //emp.EmergencyContactNumber = string.IsNullOrEmpty(txt_EmpEmergencyNumber.Text) ? null : txt_EmpEmergencyNumber.Text;
                    //#endregion

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
               "alert('Operation Successful...! Employee Master Code: " + EmpMasterCode + "');window.location ='EmpFamilyInformation.aspx?mid=" + MasterId + "';",
               true);
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
        #endregion
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
         Response.Redirect("EmployeeInfoList.aspx");
    }

    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoListUpdate.aspx");
    }

    protected void lblNext_OnClick(object sender, EventArgs e)
    {
        string EmpId = Request.QueryString["mid"];
        if (Convert.ToInt32(EmpId) > 0)
        {
            Response.Redirect("EmpFamilyInformation?mid=" + EmpId);
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
            Response.Redirect("EmpEmploymentInformation?mid=" + EmpId);
        }
        else
        {
            Response.Redirect("EmployeeInfoListUpdate.aspx");
        }
    }
}