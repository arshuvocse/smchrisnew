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
using EditableControls;
using HELPER_FUNCTIONS.HELPERS;

public partial class UserSetup_EmpEducation : System.Web.UI.Page
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
                       empMasterCode.Text=
                        emp.EmpMasterCode;
                       using (DataTable dtdesignation = _commonDataLoad.GetDTDesignationByEmpId(mid))
                       {
                           lblDesignation.Text = dtdesignation.Rows[0]["Designation"].ToString();

                       }
                       lblEmpName.Text = emp.EmpName;
                        using (DataTable dtEducation = _commonDataLoad.GetDTEmpEducationByEmpId(mid))
                        {
                            if (dtEducation.Rows.Count > 0)
                            {
                                ViewState["EducationTable"] = dtEducation;
                                gv_Education.DataSource = dtEducation;
                                gv_Education.DataBind();
                            }

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
    protected void btnEditInfo_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoList.aspx");
    }
    private void LoadDropDownList()
    {
        using (DataTable dt = _commonDataLoad.GetDDLEducationName())
        {
            ddlEducationName.DataSource = dt;
            ddlEducationName.DataValueField = "Value";
            ddlEducationName.DataTextField = "TextField";
            ddlEducationName.DataBind();

            //EditableDropDownList3.DataSource = dt;

            //EditableDropDownList3.DataTextField = "Value";
            //EditableDropDownList3.DataValueField = "TextField";
            //EditableDropDownList3.DataBind();

             
        }

        using (DataTable dt = _commonDataLoad.GetDDLBoardUniversity())
        {
            ddlBoardUniversity.DataSource = dt;
            ddlBoardUniversity.DataValueField = "Value";
            ddlBoardUniversity.DataTextField = "TextField";
            ddlBoardUniversity.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetDDLSubjectGroup())
        {
            ddlSubjectGroup.DataSource = dt;
            ddlSubjectGroup.DataValueField = "Value";
            ddlSubjectGroup.DataTextField = "TextField";
            ddlSubjectGroup.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetDDLEducationalInstitute())
        {
            ddlEducationalInstitute.DataSource = dt;
            ddlEducationalInstitute.DataValueField = "Value";
            ddlEducationalInstitute.DataTextField = "TextField";
            ddlEducationalInstitute.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetDDLSpecialization())
        {
            ddlSpecialization.DataSource = dt;
            ddlSpecialization.DataValueField = "Value";
            ddlSpecialization.DataTextField = "TextField";
            ddlSpecialization.DataBind();
        }

        using (DataTable dt = _commonDataLoad.GetCGPATotalMarks())
        {
            txt_Result.DataSource = dt;
            txt_Result.DataValueField = "Value";
            txt_Result.DataTextField = "TextField";
            txt_Result.DataBind();
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
 if (gv_Education.Rows.Count == 0)
                    {
                        //making previous inactive
                        db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpEducation SET IsActive=0 WHERE EmpInfoId={0}",
                            emp.EmpInfoId);
                    }
                      #region 5. Education

                   
                        if (gv_Education.Rows.Count > 0)
                        {
                            //making previous inactive
                            db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpEducation SET IsActive=0 WHERE EmpInfoId={0}", emp.EmpInfoId);
                            for (int i = 0; i < gv_Education.Rows.Count; i++)
                            {
                                HiddenField EmpEducationId = (HiddenField)gv_Education.Rows[i].FindControl("EmpEducationId");
                                HiddenField EducationNameId = (HiddenField)gv_Education.Rows[i].FindControl("EducationNameId");
                                HiddenField BoardUniversityId = (HiddenField)gv_Education.Rows[i].FindControl("BoardUniversityId");
                                HiddenField SubjectGroupId = (HiddenField)gv_Education.Rows[i].FindControl("SubjectGroupId");
                                HiddenField EducationalInstituteId = (HiddenField)gv_Education.Rows[i].FindControl("EducationalInstituteId");
                                HiddenField FieldOfSpecializationId = (HiddenField)gv_Education.Rows[i].FindControl("FieldOfSpecializationId");
                                Label lbl_PassingYear = (Label)gv_Education.Rows[i].FindControl("lbl_PassingYear");
                                Label lbl_Result = (Label)gv_Education.Rows[i].FindControl("lbl_Result");
                                Label lbl_CgpaOrTotalMarks = (Label)gv_Education.Rows[i].FindControl("lbl_CgpaOrTotalMarks");
                                Label lbl_EduIsLastLevel = (Label)gv_Education.Rows[i].FindControl("lbl_EduIsLastLevel");
                                Label lbl_IsProfessionalEdu = (Label)gv_Education.Rows[i].FindControl("lbl_IsProfessionalEdu");

                                if (string.IsNullOrEmpty(EmpEducationId.Value))
                                {
                                    tblEmpEducation empEducation = new tblEmpEducation();
                                    empEducation.EmpInfoId = emp.EmpInfoId;
                                    empEducation.EducationNameId = string.IsNullOrEmpty(EducationNameId.Value) ? (int?)null : int.Parse(EducationNameId.Value);
                                    empEducation.BoardUniversityId = string.IsNullOrEmpty(BoardUniversityId.Value) ? (int?)null : int.Parse(BoardUniversityId.Value);
                                    empEducation.SubjectGroupId = string.IsNullOrEmpty(SubjectGroupId.Value) ? (int?)null : int.Parse(SubjectGroupId.Value);
                                    empEducation.EducationalInstituteId = string.IsNullOrEmpty(EducationalInstituteId.Value) ? (int?)null : int.Parse(EducationalInstituteId.Value);
                                    empEducation.FieldOfSpecializationId = string.IsNullOrEmpty(FieldOfSpecializationId.Value) ? (int?)null : int.Parse(FieldOfSpecializationId.Value);
                                    empEducation.PassingYear = string.IsNullOrEmpty(lbl_PassingYear.Text) ? null : lbl_PassingYear.Text;
                                    empEducation.Result = string.IsNullOrEmpty(lbl_Result.Text) ? null : lbl_Result.Text;
                                    empEducation.CgpaOrTotalMarks = string.IsNullOrEmpty(lbl_CgpaOrTotalMarks.Text) ? null : lbl_CgpaOrTotalMarks.Text;
                                    empEducation.EduIsLastLevel = string.IsNullOrEmpty(lbl_EduIsLastLevel.Text) ? (bool?)null : bool.Parse(lbl_EduIsLastLevel.Text);
                                    empEducation.IsProfessionalEdu = string.IsNullOrEmpty(lbl_IsProfessionalEdu.Text) ? (bool?)null : bool.Parse(lbl_IsProfessionalEdu.Text);
                                    empEducation.IsActive = true;
                                    db.tblEmpEducations.Add(empEducation);
                                }
                                else
                                {
                                    int u_EmpEducationId = int.Parse(EmpEducationId.Value);

                                    tblEmpEducation empEducation = (from j in db.tblEmpEducations where j.EmpEducationId == u_EmpEducationId select j).FirstOrDefault();
                                    empEducation.EmpInfoId = emp.EmpInfoId;
                                    empEducation.EducationNameId = string.IsNullOrEmpty(EducationNameId.Value) ? (int?)null : int.Parse(EducationNameId.Value);
                                    empEducation.BoardUniversityId = string.IsNullOrEmpty(BoardUniversityId.Value) ? (int?)null : int.Parse(BoardUniversityId.Value);
                                    empEducation.SubjectGroupId = string.IsNullOrEmpty(SubjectGroupId.Value) ? (int?)null : int.Parse(SubjectGroupId.Value);
                                    empEducation.EducationalInstituteId = string.IsNullOrEmpty(EducationalInstituteId.Value) ? (int?)null : int.Parse(EducationalInstituteId.Value);
                                    empEducation.FieldOfSpecializationId = string.IsNullOrEmpty(FieldOfSpecializationId.Value) ? (int?)null : int.Parse(FieldOfSpecializationId.Value);
                                    empEducation.PassingYear = string.IsNullOrEmpty(lbl_PassingYear.Text) ? null : lbl_PassingYear.Text;
                                    empEducation.Result = string.IsNullOrEmpty(lbl_Result.Text) ? null : lbl_Result.Text;
                                    empEducation.CgpaOrTotalMarks = string.IsNullOrEmpty(lbl_CgpaOrTotalMarks.Text) ? null : lbl_CgpaOrTotalMarks.Text;
                                    empEducation.EduIsLastLevel = string.IsNullOrEmpty(lbl_EduIsLastLevel.Text) ? (bool?)null : bool.Parse(lbl_EduIsLastLevel.Text);
                                    empEducation.IsProfessionalEdu = string.IsNullOrEmpty(lbl_IsProfessionalEdu.Text) ? (bool?)null : bool.Parse(lbl_IsProfessionalEdu.Text);
                                    empEducation.IsActive = true;
                                }
                                db.SaveChanges();
                            }
                        }////End Educations
                        #endregion end 5. Education
                }
                else
                
                {
////Start New Mode
                    //emp = new tblEmpGeneralInfo();
                    //    #region 5. Education

                    //if (gv_Education.Rows.Count == 0)
                    //{
                    //    //making previous inactive
                    //    db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpEducation SET IsActive=0 WHERE EmpInfoId={0}",
                    //        emp.EmpInfoId);
                    //}
                    //    if (gv_Education.Rows.Count > 0)
                    //    {
                    //        for (int i = 0; i < gv_Education.Rows.Count; i++)
                    //        {
                    //            HiddenField EmpEducationId = (HiddenField)gv_Education.Rows[i].FindControl("EmpEducationId");
                    //            HiddenField EducationNameId = (HiddenField)gv_Education.Rows[i].FindControl("EducationNameId");
                    //            HiddenField BoardUniversityId = (HiddenField)gv_Education.Rows[i].FindControl("BoardUniversityId");
                    //            HiddenField SubjectGroupId = (HiddenField)gv_Education.Rows[i].FindControl("SubjectGroupId");
                    //            HiddenField EducationalInstituteId = (HiddenField)gv_Education.Rows[i].FindControl("EducationalInstituteId");
                    //            HiddenField FieldOfSpecializationId = (HiddenField)gv_Education.Rows[i].FindControl("FieldOfSpecializationId");
                    //            Label lbl_PassingYear = (Label)gv_Education.Rows[i].FindControl("lbl_PassingYear");
                    //            Label lbl_Result = (Label)gv_Education.Rows[i].FindControl("lbl_Result");
                    //            Label lbl_CgpaOrTotalMarks = (Label)gv_Education.Rows[i].FindControl("lbl_CgpaOrTotalMarks");
                    //            Label lbl_EduIsLastLevel = (Label)gv_Education.Rows[i].FindControl("lbl_EduIsLastLevel");
                    //            Label lbl_IsProfessionalEdu = (Label)gv_Education.Rows[i].FindControl("lbl_IsProfessionalEdu");

                    //            tblEmpEducation empEducation = new tblEmpEducation();
                    //            empEducation.EmpInfoId = emp.EmpInfoId;
                    //            empEducation.EducationNameId = string.IsNullOrEmpty(EducationNameId.Value) ? (int?)null : int.Parse(EducationNameId.Value);
                    //            empEducation.BoardUniversityId = string.IsNullOrEmpty(BoardUniversityId.Value) ? (int?)null : int.Parse(BoardUniversityId.Value);
                    //            empEducation.SubjectGroupId = string.IsNullOrEmpty(SubjectGroupId.Value) ? (int?)null : int.Parse(SubjectGroupId.Value);
                    //            empEducation.EducationalInstituteId = string.IsNullOrEmpty(EducationalInstituteId.Value) ? (int?)null : int.Parse(EducationalInstituteId.Value);
                    //            empEducation.FieldOfSpecializationId = string.IsNullOrEmpty(FieldOfSpecializationId.Value) ? (int?)null : int.Parse(FieldOfSpecializationId.Value);
                    //            empEducation.PassingYear = string.IsNullOrEmpty(lbl_PassingYear.Text) ? null : lbl_PassingYear.Text;
                    //            empEducation.Result = string.IsNullOrEmpty(lbl_Result.Text) ? null : lbl_Result.Text;
                    //            empEducation.CgpaOrTotalMarks = string.IsNullOrEmpty(lbl_CgpaOrTotalMarks.Text) ? null : lbl_CgpaOrTotalMarks.Text;
                    //            empEducation.EduIsLastLevel = string.IsNullOrEmpty(lbl_EduIsLastLevel.Text) ? (bool?)null : bool.Parse(lbl_EduIsLastLevel.Text);
                    //            empEducation.IsProfessionalEdu = string.IsNullOrEmpty(lbl_IsProfessionalEdu.Text) ? (bool?)null : bool.Parse(lbl_IsProfessionalEdu.Text);
                    //            empEducation.IsActive = true;
                    //            db.tblEmpEducations.Add(empEducation);
                    //        }
                    //    }////End Educations
                    //    #endregion end 5. Education

                    //    EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
                    //    ////Below stored procedure will generate Emp Master Code based on condition, update on database and return the value
                    //    using (DataTable dtEmpCode = _empdal.GetEmpMasterCode(emp.EmpInfoId))
                    //    {
                    //        if (dtEmpCode.Rows.Count > 0)
                    //        {
                    //            EmpMasterCode = dtEmpCode.Rows[0]["EmpMasterCode"].ToString();
                    //        }

                    //    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successful...! Employee Master Code: " + EmpMasterCode + "');window.location ='EmployeeInfoList.aspx';",
                true);
            }
           
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
         #endregion end 
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
        {   string MasterId = string.Empty;
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
                    #region 5. Education

                    if (gv_Education.Rows.Count == 0)
                    {
                        //making previous inactive
                        db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpEducation SET IsActive=0 WHERE EmpInfoId={0}",
                            emp.EmpInfoId);
                    }
                    if (gv_Education.Rows.Count > 0)
                    {
                        //making previous inactive
                        db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpEducation SET IsActive=0 WHERE EmpInfoId={0}", emp.EmpInfoId);
                        for (int i = 0; i < gv_Education.Rows.Count; i++)
                        {
                            HiddenField EmpEducationId = (HiddenField)gv_Education.Rows[i].FindControl("EmpEducationId");
                            HiddenField EducationNameId = (HiddenField)gv_Education.Rows[i].FindControl("EducationNameId");
                            HiddenField BoardUniversityId = (HiddenField)gv_Education.Rows[i].FindControl("BoardUniversityId");
                            HiddenField SubjectGroupId = (HiddenField)gv_Education.Rows[i].FindControl("SubjectGroupId");
                            HiddenField EducationalInstituteId = (HiddenField)gv_Education.Rows[i].FindControl("EducationalInstituteId");
                            HiddenField FieldOfSpecializationId = (HiddenField)gv_Education.Rows[i].FindControl("FieldOfSpecializationId");
                            Label lbl_PassingYear = (Label)gv_Education.Rows[i].FindControl("lbl_PassingYear");
                            Label lbl_Result = (Label)gv_Education.Rows[i].FindControl("lbl_Result");
                            Label lbl_CgpaOrTotalMarks = (Label)gv_Education.Rows[i].FindControl("lbl_CgpaOrTotalMarks");
                            Label lbl_EduIsLastLevel = (Label)gv_Education.Rows[i].FindControl("lbl_EduIsLastLevel");
                            Label lbl_IsProfessionalEdu = (Label)gv_Education.Rows[i].FindControl("lbl_IsProfessionalEdu");

                            if (string.IsNullOrEmpty(EmpEducationId.Value))
                            {
                                tblEmpEducation empEducation = new tblEmpEducation();
                                empEducation.EmpInfoId = emp.EmpInfoId;
                                empEducation.EducationNameId = string.IsNullOrEmpty(EducationNameId.Value) ? (int?)null : int.Parse(EducationNameId.Value);
                                empEducation.BoardUniversityId = string.IsNullOrEmpty(BoardUniversityId.Value) ? (int?)null : int.Parse(BoardUniversityId.Value);
                                empEducation.SubjectGroupId = string.IsNullOrEmpty(SubjectGroupId.Value) ? (int?)null : int.Parse(SubjectGroupId.Value);
                                empEducation.EducationalInstituteId = string.IsNullOrEmpty(EducationalInstituteId.Value) ? (int?)null : int.Parse(EducationalInstituteId.Value);
                                empEducation.FieldOfSpecializationId = string.IsNullOrEmpty(FieldOfSpecializationId.Value) ? (int?)null : int.Parse(FieldOfSpecializationId.Value);
                                empEducation.PassingYear = string.IsNullOrEmpty(lbl_PassingYear.Text) ? null : lbl_PassingYear.Text;
                                empEducation.Result = string.IsNullOrEmpty(lbl_Result.Text) ? null : lbl_Result.Text;
                                empEducation.CgpaOrTotalMarks = string.IsNullOrEmpty(lbl_CgpaOrTotalMarks.Text) ? null : lbl_CgpaOrTotalMarks.Text;
                                empEducation.EduIsLastLevel = string.IsNullOrEmpty(lbl_EduIsLastLevel.Text) ? (bool?)null : bool.Parse(lbl_EduIsLastLevel.Text);
                                empEducation.IsProfessionalEdu = string.IsNullOrEmpty(lbl_IsProfessionalEdu.Text) ? (bool?)null : bool.Parse(lbl_IsProfessionalEdu.Text);
                                empEducation.IsActive = true;
                                db.tblEmpEducations.Add(empEducation);
                            }
                            else
                            {
                                int u_EmpEducationId = int.Parse(EmpEducationId.Value);

                                tblEmpEducation empEducation = (from j in db.tblEmpEducations where j.EmpEducationId == u_EmpEducationId select j).FirstOrDefault();
                                empEducation.EmpInfoId = emp.EmpInfoId;
                                empEducation.EducationNameId = string.IsNullOrEmpty(EducationNameId.Value) ? (int?)null : int.Parse(EducationNameId.Value);
                                empEducation.BoardUniversityId = string.IsNullOrEmpty(BoardUniversityId.Value) ? (int?)null : int.Parse(BoardUniversityId.Value);
                                empEducation.SubjectGroupId = string.IsNullOrEmpty(SubjectGroupId.Value) ? (int?)null : int.Parse(SubjectGroupId.Value);
                                empEducation.EducationalInstituteId = string.IsNullOrEmpty(EducationalInstituteId.Value) ? (int?)null : int.Parse(EducationalInstituteId.Value);
                                empEducation.FieldOfSpecializationId = string.IsNullOrEmpty(FieldOfSpecializationId.Value) ? (int?)null : int.Parse(FieldOfSpecializationId.Value);
                                empEducation.PassingYear = string.IsNullOrEmpty(lbl_PassingYear.Text) ? null : lbl_PassingYear.Text;
                                empEducation.Result = string.IsNullOrEmpty(lbl_Result.Text) ? null : lbl_Result.Text;
                                empEducation.CgpaOrTotalMarks = string.IsNullOrEmpty(lbl_CgpaOrTotalMarks.Text) ? null : lbl_CgpaOrTotalMarks.Text;
                                empEducation.EduIsLastLevel = string.IsNullOrEmpty(lbl_EduIsLastLevel.Text) ? (bool?)null : bool.Parse(lbl_EduIsLastLevel.Text);
                                empEducation.IsProfessionalEdu = string.IsNullOrEmpty(lbl_IsProfessionalEdu.Text) ? (bool?)null : bool.Parse(lbl_IsProfessionalEdu.Text);
                                empEducation.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    }////End Educations
                    #endregion end 5. Education
                }
                else
                {
                    //////Start New Mode
                    //emp = new tblEmpGeneralInfo();

                    //if (gv_Education.Rows.Count == 0)
                    //{
                    //    //making previous inactive
                    //    db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpEducation SET IsActive=0 WHERE EmpInfoId={0}",
                    //        emp.EmpInfoId);
                    //}
                    //#region 5. Education
                    //if (gv_Education.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < gv_Education.Rows.Count; i++)
                    //    {
                    //        HiddenField EmpEducationId = (HiddenField)gv_Education.Rows[i].FindControl("EmpEducationId");
                    //        HiddenField EducationNameId = (HiddenField)gv_Education.Rows[i].FindControl("EducationNameId");
                    //        HiddenField BoardUniversityId = (HiddenField)gv_Education.Rows[i].FindControl("BoardUniversityId");
                    //        HiddenField SubjectGroupId = (HiddenField)gv_Education.Rows[i].FindControl("SubjectGroupId");
                    //        HiddenField EducationalInstituteId = (HiddenField)gv_Education.Rows[i].FindControl("EducationalInstituteId");
                    //        HiddenField FieldOfSpecializationId = (HiddenField)gv_Education.Rows[i].FindControl("FieldOfSpecializationId");
                    //        Label lbl_PassingYear = (Label)gv_Education.Rows[i].FindControl("lbl_PassingYear");
                    //        Label lbl_Result = (Label)gv_Education.Rows[i].FindControl("lbl_Result");
                    //        Label lbl_CgpaOrTotalMarks = (Label)gv_Education.Rows[i].FindControl("lbl_CgpaOrTotalMarks");
                    //        Label lbl_EduIsLastLevel = (Label)gv_Education.Rows[i].FindControl("lbl_EduIsLastLevel");
                    //        Label lbl_IsProfessionalEdu = (Label)gv_Education.Rows[i].FindControl("lbl_IsProfessionalEdu");

                    //        tblEmpEducation empEducation = new tblEmpEducation();
                    //        empEducation.EmpInfoId = emp.EmpInfoId;
                    //        empEducation.EducationNameId = string.IsNullOrEmpty(EducationNameId.Value) ? (int?)null : int.Parse(EducationNameId.Value);
                    //        empEducation.BoardUniversityId = string.IsNullOrEmpty(BoardUniversityId.Value) ? (int?)null : int.Parse(BoardUniversityId.Value);
                    //        empEducation.SubjectGroupId = string.IsNullOrEmpty(SubjectGroupId.Value) ? (int?)null : int.Parse(SubjectGroupId.Value);
                    //        empEducation.EducationalInstituteId = string.IsNullOrEmpty(EducationalInstituteId.Value) ? (int?)null : int.Parse(EducationalInstituteId.Value);
                    //        empEducation.FieldOfSpecializationId = string.IsNullOrEmpty(FieldOfSpecializationId.Value) ? (int?)null : int.Parse(FieldOfSpecializationId.Value);
                    //        empEducation.PassingYear = string.IsNullOrEmpty(lbl_PassingYear.Text) ? null : lbl_PassingYear.Text;
                    //        empEducation.Result = string.IsNullOrEmpty(lbl_Result.Text) ? null : lbl_Result.Text;
                    //        empEducation.CgpaOrTotalMarks = string.IsNullOrEmpty(lbl_CgpaOrTotalMarks.Text) ? null : lbl_CgpaOrTotalMarks.Text;
                    //        empEducation.EduIsLastLevel = string.IsNullOrEmpty(lbl_EduIsLastLevel.Text) ? (bool?)null : bool.Parse(lbl_EduIsLastLevel.Text);
                    //        empEducation.IsProfessionalEdu = string.IsNullOrEmpty(lbl_IsProfessionalEdu.Text) ? (bool?)null : bool.Parse(lbl_IsProfessionalEdu.Text);
                    //        empEducation.IsActive = true;
                    //        db.tblEmpEducations.Add(empEducation);
                    //    }
                    //}////End Educations
                    //#endregion end 5. Education

                    //EmployeeInfoListReportDAL _empdal = new EmployeeInfoListReportDAL();
                    //////Below stored procedure will generate Emp Master Code based on condition, update on database and return the value
                    ///// 
                    ///// 
                    //MasterId = emp.EmpInfoId.ToString();
                    //using (DataTable dtEmpCode = _empdal.GetEmpMasterCode(emp.EmpInfoId))
                    //{
                    //    if (dtEmpCode.Rows.Count > 0)
                    //    {
                    //        EmpMasterCode = dtEmpCode.Rows[0]["EmpMasterCode"].ToString();
                    //    }

                    //}
                }  
                ScriptManager.RegisterStartupScript(this, this.GetType(),
                 "alert",
                 "alert('Operation Successful...! Employee Master Code: " + EmpMasterCode + "');window.location ='EmpExperience.aspx?mid=" + MasterId + "';",
                 true);
            }
        
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
        #endregion end 
    }

    protected void cancelButton_OnClick(object sender, EventArgs e)
    {
         Response.Redirect("EmployeeInfoList.aspx");
    }

    public bool CheckLastLabel()
    {
        for (int i = 0; i < gv_Education.Rows.Count; i++)
        {
            if (Convert.ToString(((Label)gv_Education.Rows[i].FindControl("lbl_EduIsLastLevel")).Text) == "True" && chk_EduIsLastLevel.Checked)
            {
                return false;
            }

            //if (Convert.ToString(((Label)gv_Education.Rows[i].FindControl("lbl_EduIsLastLevel")).Text) != "True")
            //{
            //    return true;
            //}
        }
        return true;
    }
    protected void btnAddEducation_OnClick(object sender, EventArgs e)
    {
        if (CheckLastLabel())
        {


            AddNewToGrid_Education();

        }
        else
        {
            aShowMessage.ShowMessageBox("Last Label Already Added",this);
        }
    }

    protected void lb_EditEducation_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;

        GridViewRow row = (GridViewRow)lb.NamingContainer;
        if (row != null)
        {
            //gets the row index selected
            int index = row.RowIndex;

            //gets the datakey
            // int itemID = gv_Children.DataKeys(index).Value;

            //access row values and assign it to your TextBox
            //  txt_EmpChildrenName.Text = row.Cells[1].Text;
            //  ddlEmpChildrenGender.SelectedValue = row.Cells[2].Text;
            ////  ddlEmpChildrenOccupation.SelectedItem.Text = row.Cells[3].Text;
            //  txt_EmpChildrenDOB.Text = row.Cells[4].Text;


            //If you are using TemplateField then you can access them using FindControl() method

            if ((((HiddenField)row.FindControl("EducationNameId")).Value) != String.Empty)
            {
                if ((Convert.ToInt32(((HiddenField)row.FindControl("EducationNameId")).Value) > 0))
                {
                    try
                    {
                        ddlEducationName.SelectedValue = ((HiddenField)row.FindControl("EducationNameId")).Value;
                    }
                    catch (Exception)
                    {

                        ddlEducationName.SelectedIndex = 0;
                    }
                }
                else
                {
                    ddlEducationName.SelectedIndex = 0;
                }
            }
            else
            {
                ddlEducationName.SelectedIndex = 0;
            }


            if ((((HiddenField)row.FindControl("BoardUniversityId")).Value) != string.Empty)
            {
                if ((Convert.ToInt32(((HiddenField)row.FindControl("BoardUniversityId")).Value) > 0))
                {
                    try
                    {
                        ddlBoardUniversity.SelectedValue = ((HiddenField)row.FindControl("BoardUniversityId")).Value;
                    }
                    catch (Exception)
                    {

                        ddlBoardUniversity.SelectedIndex = 0;
                    }
                }
                else
                {
                    ddlBoardUniversity.SelectedIndex = 0;
                }
            }
            else
            {
                ddlBoardUniversity.SelectedIndex = 0;
            }



            if ((((HiddenField)row.FindControl("SubjectGroupId")).Value) != String.Empty)
            {
                if ((Convert.ToInt32(((HiddenField)row.FindControl("SubjectGroupId")).Value) > 0))
                {
                    try
                    {
                        ddlSubjectGroup.SelectedValue = ((HiddenField)row.FindControl("SubjectGroupId")).Value;
                    }
                    catch (Exception)
                    {

                        ddlSubjectGroup.SelectedIndex = 0;
                    }
                }
                else
                {
                    ddlSubjectGroup.SelectedIndex = 0;
                }
            }
            else
            {
                ddlSubjectGroup.SelectedIndex = 0;
            }




            if ((((HiddenField)row.FindControl("EducationalInstituteId")).Value) != string.Empty)
            {
                if ((Convert.ToInt32(((HiddenField)row.FindControl("EducationalInstituteId")).Value) > 0))
                {
                    try
                    {
                        ddlEducationalInstitute.SelectedValue = ((HiddenField)row.FindControl("EducationalInstituteId")).Value;
                    }
                    catch (Exception)
                    {

                        ddlEducationalInstitute.SelectedIndex = 0;
                    }
                }
                else
                {
                    ddlEducationalInstitute.SelectedIndex = 0;
                }
            }
            else
            {
                ddlEducationalInstitute.SelectedIndex = 0;
            }



            if ((((HiddenField)row.FindControl("FieldOfSpecializationId")).Value) != string.Empty)
            {
                if ((Convert.ToInt32(((HiddenField)row.FindControl("FieldOfSpecializationId")).Value) > 0))
                {
                    try
                    {
                        ddlSpecialization.SelectedValue = ((HiddenField)row.FindControl("FieldOfSpecializationId")).Value;
                    }
                    catch (Exception)
                    {

                        ddlSpecialization.SelectedIndex = 0;
                    }
                }
                else
                {
                    ddlSpecialization.SelectedIndex = 0;
                }
            }
            else
            {
                ddlSpecialization.SelectedIndex = 0;
            }




            // ddlSpecialization.SelectedValue = ((HiddenField)row.FindControl("FieldOfSpecializationId")).Value;
            txt_PassingYear.Text = ((Label)row.FindControl("lbl_PassingYear")).Text;
            txt_Result.SelectedItem.Text = ((Label)row.FindControl("lbl_Result")).Text;
            txt_CGPAMarks.Text = ((Label)row.FindControl("lbl_CgpaOrTotalMarks")).Text;

            if (((Label)row.FindControl("lbl_EduIsLastLevel")).Text == "True")
            {
                chk_EduIsLastLevel.Checked = true;
            }

            if (((Label)row.FindControl("lbl_EduIsLastLevel")).Text == "False")
            {
                chk_EduIsLastLevel.Checked = false;
            }
            if (((Label)row.FindControl("lbl_IsProfessionalEdu")).Text == "True")
            {
                chk_IsProfessionalEdu.Checked = true;
            }

            if (((Label)row.FindControl("lbl_IsProfessionalEdu")).Text == "False")
            {
                chk_IsProfessionalEdu.Checked = false;
            }








            GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
            int rowID = gvRow.RowIndex;
            if (ViewState["EducationTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["EducationTable"];
                dt.Rows.Remove(dt.Rows[rowID]);
                if (dt.Rows.Count > 0)
                {
                    //Store the current data in ViewState for future reference  
                    ViewState["EducationTable"] = dt;
                    //Re bind the GridView for the updated data  
                    gv_Education.DataSource = dt;
                    gv_Education.DataBind();
                }
                else
                {
                    ViewState["EducationTable"] = null;
                    //Re bind the GridView for the updated data  
                    gv_Education.DataSource = null;
                    gv_Education.DataBind();
                }
            }
            //Set Previous Data on Postbacks  
            SetPreviousData_Education();
        }
    }
    protected void lb_RemoveEducation_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["EducationTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["EducationTable"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["EducationTable"] = dt;
                //Re bind the GridView for the updated data  
                gv_Education.DataSource = dt;
                gv_Education.DataBind();
            }
            else
            {
                ViewState["EducationTable"] = null;
                //Re bind the GridView for the updated data  
                gv_Education.DataSource = null;
                gv_Education.DataBind();
            }
        }
        //Set Previous Data on Postbacks  
        SetPreviousData_Education();
    }
    private void AddNewToGrid_Education()
    {
        if (ViewState["EducationTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["EducationTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["EmpEducationId"] = DBNull.Value;
                drCurrentRow["EmpInfoId"] = hdpk.Value;
                if (ddlEducationName.SelectedIndex > 0)
                {
                    drCurrentRow["EducationNameId"] = ddlEducationName.SelectedValue;
                    drCurrentRow["EducationName"] = ddlEducationName.SelectedItem.Text;
                }
                if (ddlBoardUniversity.SelectedIndex > 0)
                {
                    drCurrentRow["BoardUniversityId"] = ddlBoardUniversity.SelectedValue;
                    drCurrentRow["BoardUniversity"] = ddlBoardUniversity.SelectedItem.Text;
                }
                if (ddlSubjectGroup.SelectedIndex > 0)
                {
                    drCurrentRow["SubjectGroupId"] = ddlSubjectGroup.SelectedValue;
                    drCurrentRow["SubjectGroup"] = ddlSubjectGroup.SelectedItem.Text;
                }
                if (ddlEducationalInstitute.SelectedIndex > 0)
                {
                    drCurrentRow["EducationalInstituteId"] = ddlEducationalInstitute.SelectedValue;
                    drCurrentRow["EducationalInstitute"] = ddlEducationalInstitute.SelectedItem.Text;
                }
                if (ddlSpecialization.SelectedIndex > 0)
                {
                    drCurrentRow["FieldOfSpecializationId"] = ddlSpecialization.SelectedValue;
                    drCurrentRow["FieldOfSpecialization"] = ddlSpecialization.SelectedItem.Text;
                }



                drCurrentRow["PassingYear"] = txt_PassingYear.Text;
                drCurrentRow["Result"] = txt_Result.SelectedItem.Text;
                drCurrentRow["CgpaOrTotalMarks"] = txt_CGPAMarks.Text;
                drCurrentRow["EduIsLastLevel"] = chk_EduIsLastLevel.Checked.ToString();
                drCurrentRow["IsProfessionalEdu"] = chk_IsProfessionalEdu.Checked.ToString();
                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["EducationTable"] = dtCurrentTable;

                //Rebind the Grid with the current data to reflect changes   
                gv_Education.DataSource = dtCurrentTable;
                gv_Education.DataBind();
            }
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("EmpEducationId", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpInfoId", typeof(string)));
            dt.Columns.Add(new DataColumn("EducationNameId", typeof(string)));
            dt.Columns.Add(new DataColumn("EducationName", typeof(string)));
            dt.Columns.Add(new DataColumn("BoardUniversityId", typeof(string)));
            dt.Columns.Add(new DataColumn("BoardUniversity", typeof(string)));
            dt.Columns.Add(new DataColumn("SubjectGroupId", typeof(string)));
            dt.Columns.Add(new DataColumn("SubjectGroup", typeof(string)));
            dt.Columns.Add(new DataColumn("EducationalInstituteId", typeof(string)));
            dt.Columns.Add(new DataColumn("EducationalInstitute", typeof(string)));
            dt.Columns.Add(new DataColumn("FieldOfSpecializationId", typeof(string)));
            dt.Columns.Add(new DataColumn("FieldOfSpecialization", typeof(string)));
            dt.Columns.Add(new DataColumn("PassingYear", typeof(string)));
            dt.Columns.Add(new DataColumn("Result", typeof(string)));
            dt.Columns.Add(new DataColumn("CgpaOrTotalMarks", typeof(string)));
            dt.Columns.Add(new DataColumn("EduIsLastLevel", typeof(string)));
            dt.Columns.Add(new DataColumn("IsProfessionalEdu", typeof(string)));

            dr = dt.NewRow();
            dr["EmpEducationId"] = "";
            dr["EmpInfoId"] = hdpk.Value;
            if (ddlEducationName.SelectedIndex > 0)
            {
                dr["EducationNameId"] = ddlEducationName.SelectedValue;
                dr["EducationName"] = ddlEducationName.SelectedItem.Text;
            }
            if (ddlBoardUniversity.SelectedIndex > 0)
            {
                dr["BoardUniversityId"] = ddlBoardUniversity.SelectedValue;
                dr["BoardUniversity"] = ddlBoardUniversity.SelectedItem.Text;
            }
            if (ddlSubjectGroup.SelectedIndex > 0)
            {
                dr["SubjectGroupId"] = ddlSubjectGroup.SelectedValue;
                dr["SubjectGroup"] = ddlSubjectGroup.SelectedItem.Text;
            }
            if (ddlEducationalInstitute.SelectedIndex > 0)
            {
                dr["EducationalInstituteId"] = ddlEducationalInstitute.SelectedValue;
                dr["EducationalInstitute"] = ddlEducationalInstitute.SelectedItem.Text;
            }
            if (ddlSpecialization.SelectedIndex > 0)
            {
                dr["FieldOfSpecializationId"] = ddlSpecialization.SelectedValue;
                dr["FieldOfSpecialization"] = ddlSpecialization.SelectedItem.Text;
            }

            //dr["EducationNameId"] = ddlEducationName.SelectedValue;
            //dr["EducationName"] = ddlEducationName.SelectedItem.Text;
            //dr["BoardUniversityId"] = ddlBoardUniversity.SelectedValue;
            //dr["BoardUniversity"] = ddlBoardUniversity.SelectedItem.Text;
            //dr["SubjectGroupId"] = ddlSubjectGroup.SelectedValue;
            //dr["SubjectGroup"] = ddlSubjectGroup.SelectedItem.Text;
            //dr["EducationalInstituteId"] = ddlEducationalInstitute.SelectedValue;
            //dr["EducationalInstitute"] = ddlEducationalInstitute.SelectedItem.Text;
            //dr["FieldOfSpecializationId"] = ddlSpecialization.SelectedValue;
            //dr["FieldOfSpecialization"] = ddlSpecialization.SelectedItem.Text;
            dr["PassingYear"] = txt_PassingYear.Text;
            dr["Result"] = txt_Result.SelectedItem.Text;
            dr["CgpaOrTotalMarks"] = txt_CGPAMarks.Text;
            dr["EduIsLastLevel"] = chk_EduIsLastLevel.Checked.ToString();
            dr["IsProfessionalEdu"] = chk_IsProfessionalEdu.Checked.ToString();
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["EducationTable"] = dt;

            //Bind the Gridview   
            gv_Education.DataSource = dt;
            gv_Education.DataBind();
        }
        //Set Previous Data on Postbacks   
        SetPreviousData_Education();

        txtBoardUniversity.Text=String.Empty;
        txtEducationName.Text = String.Empty;
        txtEducationalInstitute.Text = String.Empty;

        ddlEducationName.SelectedValue = null;
        ddlBoardUniversity.SelectedValue = null;
        ddlSubjectGroup.SelectedValue = null;
        ddlEducationalInstitute.SelectedValue = null;
        ddlSpecialization.SelectedValue = null;
        txt_PassingYear.Text = string.Empty;
        txt_Result.SelectedValue = null;
        txt_CGPAMarks.Text = string.Empty;
        chk_EduIsLastLevel.Checked = false;
        chk_IsProfessionalEdu.Checked = false;
    }
    private void SetPreviousData_Education()
    {
        int rowIndex = 0;
        if (ViewState["EducationTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["EducationTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField EmpEducationId = (HiddenField)gv_Education.Rows[rowIndex].FindControl("EmpEducationId");
                    Label lbl_EducationName = (Label)gv_Education.Rows[rowIndex].FindControl("lbl_EducationName");
                    HiddenField EducationNameId = (HiddenField)gv_Education.Rows[rowIndex].FindControl("EducationNameId");
                    Label lbl_BoardUniversity = (Label)gv_Education.Rows[rowIndex].FindControl("lbl_BoardUniversity");
                    HiddenField BoardUniversityId = (HiddenField)gv_Education.Rows[rowIndex].FindControl("BoardUniversityId");
                    Label lbl_SubjectGroup = (Label)gv_Education.Rows[rowIndex].FindControl("lbl_SubjectGroup");
                    HiddenField SubjectGroupId = (HiddenField)gv_Education.Rows[rowIndex].FindControl("SubjectGroupId");
                    Label lbl_EducationalInstitute = (Label)gv_Education.Rows[rowIndex].FindControl("lbl_EducationalInstitute");
                    HiddenField EducationalInstituteId = (HiddenField)gv_Education.Rows[rowIndex].FindControl("EducationalInstituteId");
                    Label lbl_FieldOfSpecialization = (Label)gv_Education.Rows[rowIndex].FindControl("lbl_FieldOfSpecialization");
                    HiddenField FieldOfSpecializationId = (HiddenField)gv_Education.Rows[rowIndex].FindControl("FieldOfSpecializationId");
                    Label lbl_PassingYear = (Label)gv_Education.Rows[rowIndex].FindControl("lbl_PassingYear");
                    Label lbl_Result = (Label)gv_Education.Rows[rowIndex].FindControl("lbl_Result");
                    Label lbl_CgpaOrTotalMarks = (Label)gv_Education.Rows[rowIndex].FindControl("lbl_CgpaOrTotalMarks");
                    Label lbl_EduIsLastLevel = (Label)gv_Education.Rows[rowIndex].FindControl("lbl_EduIsLastLevel");
                    Label lbl_IsProfessionalEdu = (Label)gv_Education.Rows[rowIndex].FindControl("lbl_IsProfessionalEdu");

                    if (i < dt.Rows.Count - 1)
                    {
                        EmpEducationId.Value = dt.Rows[i]["EmpEducationId"].ToString();
                        EducationNameId.Value = dt.Rows[i]["EducationNameId"].ToString();
                        lbl_EducationName.Text = dt.Rows[i]["EducationName"].ToString();
                        lbl_BoardUniversity.Text = dt.Rows[i]["BoardUniversity"].ToString();
                        BoardUniversityId.Value = dt.Rows[i]["BoardUniversityId"].ToString();
                        lbl_SubjectGroup.Text = dt.Rows[i]["SubjectGroup"].ToString();
                        SubjectGroupId.Value = dt.Rows[i]["SubjectGroupId"].ToString();
                        lbl_EducationalInstitute.Text = dt.Rows[i]["EducationalInstitute"].ToString();
                        EducationalInstituteId.Value = dt.Rows[i]["EducationalInstituteId"].ToString();
                        lbl_FieldOfSpecialization.Text = dt.Rows[i]["FieldOfSpecialization"].ToString();
                        FieldOfSpecializationId.Value = dt.Rows[i]["FieldOfSpecializationId"].ToString();
                        lbl_PassingYear.Text = dt.Rows[i]["PassingYear"].ToString();
                        lbl_Result.Text = dt.Rows[i]["Result"].ToString();
                        lbl_CgpaOrTotalMarks.Text = dt.Rows[i]["CgpaOrTotalMarks"].ToString();
                        var EduIsLastLevel = bool.Parse(dt.Rows[i]["EduIsLastLevel"].ToString());
                        var IsProfessionalEdu = bool.Parse(dt.Rows[i]["IsProfessionalEdu"].ToString());
                        chk_EduIsLastLevel.Checked = EduIsLastLevel;
                        chk_IsProfessionalEdu.Checked = IsProfessionalEdu;
                    }

                    rowIndex++;
                }
            }
        }
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
            Response.Redirect("EmpExperience?mid=" + EmpId);
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
            Response.Redirect("EmpFamilyInformation?mid=" + EmpId);
        }
        else
        {
            Response.Redirect("EmployeeInfoListUpdate.aspx");
        }
    }

    protected void txtEducationName_OnTextChanged(object sender, EventArgs e)
    {
        string empName = txtEducationName.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');

            

            txtEducationName.Text = emp[0];

            ddlEducationName.SelectedValue = emp[1];
        }
        else
        {
            txtEducationName.Text = "";
            ddlEducationName.Items.Clear();
           
            //  EmpInfoIdHiddenField.Value = "";
            aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        }
    }

    protected void BoardUniversity_OnTextChanged(object sender, EventArgs e)
    {
        string empName = txtBoardUniversity.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');



            txtBoardUniversity.Text = emp[0];

            ddlBoardUniversity.SelectedValue = emp[1];
        }
        else
        {
            txtBoardUniversity.Text = "";
            ddlBoardUniversity.Items.Clear();
            //  EmpInfoIdHiddenField.Value = "";
            aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        }
    }

    protected void txtEducationalInstitute_OnTextChanged(object sender, EventArgs e)
    {
        string empName = txtEducationalInstitute.Text.Trim();

        if (empName.Contains(':'))
        {
            string[] emp = empName.Split(':');



            txtEducationalInstitute.Text = emp[0];

            ddlEducationalInstitute.SelectedValue = emp[1];
        }
        else
        {
            txtEducationalInstitute.Text = "";
            ddlEducationalInstitute.Items.Clear();
            //  EmpInfoIdHiddenField.Value = "";
            aShowMessage.ShowMessageBox("Input Correct Data !!", this);
        }
    }
}