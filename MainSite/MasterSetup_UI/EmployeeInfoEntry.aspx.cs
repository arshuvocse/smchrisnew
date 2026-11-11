using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using DAL.COMMON_DAL;
using DAO.HRIS_DAO_EF;

public partial class UserSetup_EmployeeInfoEntry : System.Web.UI.Page
{
    private CommonDataLoadDAL _commonDataLoad = new CommonDataLoadDAL();
    private int mid = 0;
    private string _userId;

    protected void Page_Load(object sender, EventArgs e)
    {
        //ScriptManager.GetCurrent(this).RegisterPostBackControl(this.btn_ImageUpload);
        if (Session["UserId"] != null)
        {
            _userId = Session["UserId"].ToString();
        }
        if (!IsPostBack)
        {
             ReadOnltDate();



            LoadInitialDDL();
            lbl_Mode.Text = "Mode:New Entry";
            if (!string.IsNullOrEmpty(Request.QueryString["mid"]))
            {
                mid = int.Parse(Request.QueryString["mid"]);
                hdpk.Value = mid.ToString();
                if (mid > 0)
                {
                    using (var db = new HRIS_SMCEntities())
                    {
                        var emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();
                        #region 1. General Information

                        txt_EmpName.Text = emp.EmpName;
                        ddlGender.SelectedValue = emp.Gender;
                        ddlBloodGroup.SelectedValue = emp.BloodGroup;
                        txt_EmpTinNo.Text = emp.TinNo;

                        txt_EmpFatherName.Text = emp.FatherName;
                        ddlEmpFOccupation.SelectedValue = emp.FatherOccupation.ToString();
                        txt_EmpMotherName.Text = emp.MotherName;
                        ddlEmpMOccupation.SelectedValue = emp.MotherOccupation.ToString();
                        txt_EmpDOB.Text = string.IsNullOrEmpty(emp.DateOfBirth.ToString()) ? String.Empty : emp.DateOfBirth.Value.ToString("dd-MMM-yyyy");


                        txt_EmpDOJ.Text = string.IsNullOrEmpty(emp.DateOfJoin.ToString())?String.Empty : emp.DateOfJoin.Value.ToString("dd-MMM-yyyy");
                        ddlEmpReligion.SelectedValue = emp.Religion;
                        ddlEmpMaritalStatus.SelectedValue=emp.MaritalStatus;

                        ddlEmpType.SelectedValue = emp.EmpTypeId.ToString();
                        txt_EmpPlaceOfBirth.Text = emp.PlaceOfBirth;
                        ddlEmpNationality.SelectedValue = emp.Nationality;
                        txt_EmpNationalID.Text = emp.NationalIdNo;
                        if (ddlEmpType.SelectedValue == "3")
                        {
                            txt_ContractEndDate.Text = string.IsNullOrEmpty(emp.ContractEndDate.ToString())?String.Empty : emp.ContractEndDate.Value.ToString("dd-MMM-yyyy");
                            ddlSalFromProject.SelectedValue=emp.SalaryFromProject.ToString();
                            ////TODO
                            //for (int i = 0; i < cbl_ContractProject.Items.Count; i++)
                            //{
                            //    if (cbl_ContractProject.Items[i].Selected)
                            //    {
                            //        var ContractProject = new tblEmpContractProject();
                            //        ContractProject.ProjectId = int.Parse(cbl_ContractProject.Items[i].Value);
                            //        db.tblEmpContractProjects.Add(ContractProject);
                            //    }
                            //}
                        }
                        txt_EmpPassport.Text=emp.PassportNo;
                        txt_EmpExpectedServiceLength.Text=emp.ExpectedServiceLength;
                        txt_EmpDateOfRetirement.Text = string.IsNullOrEmpty(emp.DateOfRetirement.ToString())?String.Empty : emp.DateOfRetirement.Value.ToString("dd-MMM-yyyy");

                        txt_EmpDateOfConformation.Text=string.IsNullOrEmpty(emp.DateOfConformation.ToString())?string.Empty: emp.DateOfConformation.Value.ToString("dd-MMM-yyyy");
                        txt_ConformationStatus.Text=emp.ConformationStatus;
                        if (!string.IsNullOrEmpty(emp.ReportingEmpId.ToString()))
                        {
                            hdReportingBoss.Value = emp.ReportingEmpId.ToString();
                            int ReportingEmpId = int.Parse(emp.ReportingEmpId.ToString());

                            var reportingBoss = (from j in db.vw_EmpInfo where j.EmpInfoId == ReportingEmpId select j).FirstOrDefault();
                            txt_ReportingBoss.Text = reportingBoss.EmpName;
                            txt_ReportingBossDesig.Text = reportingBoss.Designation;
                        }

                        chkIsProbationary.Checked=string.IsNullOrEmpty(emp.IsProbationary.ToString())?false: bool.Parse(emp.IsProbationary.ToString());
                        txt_ProbationaryEndDate.Text = string.IsNullOrEmpty(emp.ProbationEndDate.ToString())?string.Empty: emp.ProbationEndDate.Value.ToString("dd-MMM-yyyy");
                        hfempimg.Value=emp.EmpImage;
                        img_emp.ImageUrl = "~/UploadImg/" + emp.EmpImage;
                        #endregion end 1. General Information

                        #region 2. Employment Information
                        ddlCompany.SelectedValue=emp.CompanyId.ToString();
                        ddlCompany_SelectedIndexChanged(null,null);
                        ddlDivision.SelectedValue=emp.DivisionId.ToString();

                        ddlWing.SelectedValue=emp.DivisionWId.ToString();

                        ddlDepartment.SelectedValue=emp.DepartmentId.ToString();

                        ddlSection.SelectedValue=emp.SectionId.ToString();

                        ddlSubSection.SelectedValue=emp.SubSectionId.ToString();

                        ddlEmpCategory.SelectedValue=emp.EmpCategoryId.ToString();
                        ddlEmpCategory_OnSelectedIndexChanged(null,null);
                        ddlSalaryGrade.SelectedValue=emp.SalaryGradeId.ToString();
                        ddlSalaryGrade_OnSelectedIndexChanged(null,null);

                        ddlSalaryStep.SelectedValue=emp.SalaryStepId.ToString();

                        ddlDesignation.SelectedValue=emp.DesignationId.ToString();
                        ddlDesignationType.SelectedValue = emp.DesignationTypeId.ToString();
                        
                        ddlEmpType.SelectedValue=emp.EmpTypeId.ToString();
                        ddlEmpType_OnSelectedIndexChanged(null,null);
                        ddlJobLocation.SelectedValue=emp.JobLocationId.ToString();

                        ddlSalaryLocation.SelectedValue=emp.SalaryLoationId.ToString();
                        #endregion end 2. Employment Information

                        #region 3. Contacts

                        txt_EmpPresentAddress.Text=emp.AddressPresent;
                        ddlEmpPresentDivision.SelectedValue=emp.PresentDivision.ToString();
                        ddlEmpPresentDivision_OnSelectedIndexChanged(null,null);
                        ddlEmpPresentDist.SelectedValue=emp.PresentDistrict.ToString();
                        ddlEmpPresentDist_OnSelectedIndexChanged(null,null);
                        ddlEmpPresentThana.SelectedValue=emp.PresentThana.ToString();

                        txt_EmpPresentTelNo.Text=emp.PresentTelNo;
                        txt_EmpParmanentAddress.Text=emp.AddressPermanent;
                        ddlEmpParmanentDivision.SelectedValue=emp.ParmanentDivision.ToString();
                        ddlEmpParmanentDivision_OnSelectedIndexChanged(null,null);
                        ddlEmpParmanentDistrict.SelectedValue=emp.PermanentDistrict.ToString();
                        ddlEmpParmanentDistrict_OnSelectedIndexChanged(null,null);
                        ddlEmpParmanentThana.SelectedValue=emp.PermanentThana.ToString();

                        txt_EmpParmanentTelNo.Text=emp.ParmanentTelNo;

                        txt_EmpPersonalEmail.Text=emp.PersonalEmail;
                        txt_EmpOfficialEmail.Text=emp.OfficialEmail;
                        txt_EmpPersonalMobile.Text=emp.PersonalMobile;
                        txt_EmpOfficialMobile.Text=emp.OfficialMobile;
                        txt_EmpFax.Text=emp.FaxNo;
                        txt_EmpEmergencyPerson.Text = emp.EmergencyContactPerson;
                        txt_EmpEmergencyAddress.Text=emp.EmergencyContactAddress;
                        txt_EmpEmergencyNumber.Text=emp.EmergencyContactNumber;
                        #endregion

                        #region 4. Family Information
                        txt_EmpSpouseName.Text=emp.SpouseName;
                        ddlEmpSpouseMaxEdu.SelectedValue=emp.SpouseMaxEducation.ToString();
                        ddlEmpSpouseOccupation.SelectedValue=emp.SpouseOccupation.ToString();

                        
                        txt_EmpSpouseDOB.Text = string.IsNullOrEmpty(emp.SpouseDateOfBirth.ToString()) ? String.Empty : emp.SpouseDateOfBirth.Value.ToString("dd-MMM-yyyy");
                        txt_EmpMarriageDate.Text = string.IsNullOrEmpty(emp.DateOfMarriage.ToString()) ? String.Empty : emp.DateOfMarriage.Value.ToString("dd-MMM-yyyy");



                        empMasterCode.Text = emp.EmpMasterCode;
                        lbl_Mode.Text = "Mode:Update Information";

                        using (DataTable dtChildren = _commonDataLoad.GetDTEmpChildrenByEmpId(mid))
                        {
                            if (dtChildren.Rows.Count>0)
                            {
                                ViewState["ChildrenTable"] = dtChildren;
                                gv_Children.DataSource = dtChildren;
                                gv_Children.DataBind();
                            }

                        }
                        #endregion end 4. Family Information
                        using (DataTable dtEducation = _commonDataLoad.GetDTEmpEducationByEmpId(mid))
                        {
                            if (dtEducation.Rows.Count > 0)
                            {
                                ViewState["EducationTable"] = dtEducation;
                                gv_Education.DataSource = dtEducation;
                                gv_Education.DataBind();
                            }

                        }

                        using (DataTable dtExperience = _commonDataLoad.GetDTEmpExperienceByEmpId(mid))
                        {
                            if (dtExperience.Rows.Count > 0)
                            {
                                ViewState["ExperienceTable"] = dtExperience;
                                gv_Experience.DataSource = dtExperience;
                                gv_Experience.DataBind();
                            }

                        }


                        using (DataTable dtTraining = _commonDataLoad.GetDTEmpTrainingByEmpId(mid))
                        {
                            if (dtTraining.Rows.Count > 0)
                            {
                                ViewState["TrainingTable"] = dtTraining;
                                gv_Training.DataSource = dtTraining;
                                gv_Training.DataBind();
                            }
                        }

                        using (DataTable dtTraining = _commonDataLoad.GetDTEmpTrainingByEmpId(mid))
                        {
                            if (dtTraining.Rows.Count > 0)
                            {
                                ViewState["TrainingTable"] = dtTraining;
                                gv_Training.DataSource = dtTraining;
                                gv_Training.DataBind();
                            }
                        }
                        using (DataTable dtReference = _commonDataLoad.GetDTEmpReferenceByEmpId(mid))
                        {
                            if (dtReference.Rows.Count > 0)
                            {
                                ViewState["ReferenceTable"] = dtReference;
                                gv_Reference.DataSource = dtReference;
                                gv_Reference.DataBind();
                            }
                        }

                        using (DataTable dtNominee = _commonDataLoad.GetDTEmpNomineeByEmpId(mid))
                        {
                            if (dtNominee.Rows.Count > 0)
                            {
                                ViewState["NomineeTable"] = dtNominee;
                                gv_Nominee.DataSource = dtNominee;
                                gv_Nominee.DataBind();
                            }
                        }
                        using (DataTable dtExtraCurriculam = _commonDataLoad.GetDTEmpExtraCurriculamByEmpId(mid))
                        {
                            if (dtExtraCurriculam.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtExtraCurriculam.Rows.Count; i++)
                                {
                                    chkExtraCurriculam.Items.FindByValue(dtExtraCurriculam.Rows[i]["Value"].ToString()).Selected = true;
                                }
                            }
                        }

                        using (DataTable dtOtherTalents = _commonDataLoad.GetDTEmpOtherTalentsByEmpId(mid))
                        {
                            if (dtOtherTalents.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtOtherTalents.Rows.Count; i++)
                                {
                                    chkOtherTalents.Items.FindByValue(dtOtherTalents.Rows[i]["Value"].ToString()).Selected = true;
                                }
                            }
                        }

                        using (DataTable dtAchievements = _commonDataLoad.GetDTEmpAchievementsByEmpId(mid))
                        {
                            if (dtAchievements.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtAchievements.Rows.Count; i++)
                                {
                                    chkAchievements.Items.FindByValue(dtAchievements.Rows[i]["Value"].ToString()).Selected = true;
                                }
                            }
                        }

                        using (DataTable dtHobby = _commonDataLoad.GetDTEmpHobbyByEmpId(mid))
                        {
                            if (dtHobby.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtHobby.Rows.Count; i++)
                                {
                                    chkHobby.Items.FindByValue(dtHobby.Rows[i]["Value"].ToString()).Selected = true;
                                }
                            }
                        }

                        
                    }
                }
            }
        }
    }

    private void ReadOnltDate()
    {
        txt_EmpDOB.Attributes.Add("readonly", "readonly");
        txt_EmpDOJ.Attributes.Add("readonly", "readonly");
        txt_EmpSpouseDOB.Attributes.Add("readonly", "readonly");
        txt_EmpMarriageDate.Attributes.Add("readonly", "readonly");
        txt_EmpChildrenDOB.Attributes.Add("readonly", "readonly");


        txt_ExpFromDate.Attributes.Add("readonly", "readonly");
        txt_ExpToDate.Attributes.Add("readonly", "readonly");
        txt_TrFromDate.Attributes.Add("readonly", "readonly");
        txt_TrToDate.Attributes.Add("readonly", "readonly");
        txt_NomDateOfNomination.Attributes.Add("readonly", "readonly");


        txt_NomNomineeDOB.Attributes.Add("readonly", "readonly");



        txt_EmpDateOfRetirement.Attributes.Add("readonly", "readonly");
        txt_EmpDateOfConformation.Attributes.Add("readonly", "readonly"); 
    //    txt_NomNomineeDOB.Attributes.Add("readonly", "readonly");
        
    }

    public void ButtonVisible()
    {
        if (Session["Status"] != null)
        {


            if (Session["Status"].ToString() == "Add")
            {
                btn_Save.Visible = true;
            }
            else if (Session["Status"].ToString() == "Edit")
            {
                btn_Save.Visible = true;
            }
            else if (Session["Status"].ToString() == "Delete")
            {
                btn_Save.Visible = true;
            }
            Session["Status"] = null;
        }

    }
    
    private void LoadInitialDDL()
    {
        using (DataTable dt = _commonDataLoad.GetCompanyDDL())
        {
            ddlCompany.DataSource = dt;
            ddlCompany.DataValueField = "Value";
            ddlCompany.DataTextField = "TextField";
            ddlCompany.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetMasterExtraCurriculam())
        {
            chkExtraCurriculam.DataSource = dt;
            chkExtraCurriculam.DataValueField = "Value";
            chkExtraCurriculam.DataTextField = "TextField";
            chkExtraCurriculam.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetMasterOtherTalents())
        {
            chkOtherTalents.DataSource = dt;
            chkOtherTalents.DataValueField = "Value";
            chkOtherTalents.DataTextField = "TextField";
            chkOtherTalents.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetMasterAchievements())
        {
            chkAchievements.DataSource = dt;
            chkAchievements.DataValueField = "Value";
            chkAchievements.DataTextField = "TextField";
            chkAchievements.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetMasterHobby())
        {
            chkHobby.DataSource = dt;
            chkHobby.DataValueField = "Value";
            chkHobby.DataTextField = "TextField";
            chkHobby.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetDDLEmpType())
        {
            ddlEmpType.DataSource = dt;
            ddlEmpType.DataValueField = "Value";
            ddlEmpType.DataTextField = "TextField";
            ddlEmpType.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetDDLNominationPurpose())
        {
            ddlNomNominationPurpose.DataSource = dt;
            ddlNomNominationPurpose.DataValueField = "Value";
            ddlNomNominationPurpose.DataTextField = "TextField";
            ddlNomNominationPurpose.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetDDLOccupation())
        {
            ddlEmpFOccupation.DataSource = dt;
            ddlEmpFOccupation.DataValueField = "Value";
            ddlEmpFOccupation.DataTextField = "TextField";
            ddlEmpFOccupation.DataBind();

            ddlEmpMOccupation.DataSource = dt;
            ddlEmpMOccupation.DataValueField = "Value";
            ddlEmpMOccupation.DataTextField = "TextField";
            ddlEmpMOccupation.DataBind();

            ddlNomNomineeOccupation.DataSource = dt;
            ddlNomNomineeOccupation.DataValueField = "Value";
            ddlNomNomineeOccupation.DataTextField = "TextField";
            ddlNomNomineeOccupation.DataBind();

            ddlRefOccupation.DataSource = dt;
            ddlRefOccupation.DataValueField = "Value";
            ddlRefOccupation.DataTextField = "TextField";
            ddlRefOccupation.DataBind();

            ddlEmpSpouseOccupation.DataSource = dt;
            ddlEmpSpouseOccupation.DataValueField = "Value";
            ddlEmpSpouseOccupation.DataTextField = "TextField";
            ddlEmpSpouseOccupation.DataBind();

            ddlEmpChildrenOccupation.DataSource = dt;
            ddlEmpChildrenOccupation.DataValueField = "Value";
            ddlEmpChildrenOccupation.DataTextField = "TextField";
            ddlEmpChildrenOccupation.DataBind();
            ////TODO bind other occupations
            
        }
        using (DataTable dt = _commonDataLoad.GetDDLRelation())
        {
            ddlNomNomineeRelation.DataSource = dt;
            ddlNomNomineeRelation.DataValueField = "Value";
            ddlNomNomineeRelation.DataTextField = "TextField";
            ddlNomNomineeRelation.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetDDLTrainingType())
        {
            ddlTrTrainingType.DataSource = dt;
            ddlTrTrainingType.DataValueField = "Value";
            ddlTrTrainingType.DataTextField = "TextField";
            ddlTrTrainingType.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetDDLTrainingInstitution())
        {
            ddlTrTrainingInstitution.DataSource = dt;
            ddlTrTrainingInstitution.DataValueField = "Value";
            ddlTrTrainingInstitution.DataTextField = "TextField";
            ddlTrTrainingInstitution.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetDDLCountry())
        {
            ddlTrTrainingCountry.DataSource = dt;
            ddlTrTrainingCountry.DataValueField = "Value";
            ddlTrTrainingCountry.DataTextField = "TextField";
            ddlTrTrainingCountry.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetDDLEducationName())
        {
            ddlEducationName.DataSource = dt;
            ddlEducationName.DataValueField = "Value";
            ddlEducationName.DataTextField = "TextField";
            ddlEducationName.DataBind();

            ddlEmpSpouseMaxEdu.DataSource = dt;
            ddlEmpSpouseMaxEdu.DataValueField = "Value";
            ddlEmpSpouseMaxEdu.DataTextField = "TextField";
            ddlEmpSpouseMaxEdu.DataBind();
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

        using (DataTable dt = _commonDataLoad.GetDDLSalFromProject(ddlCompany.SelectedValue))
        {
            ddlSalFromProject.DataSource = dt;
            ddlSalFromProject.DataValueField = "Value";
            ddlSalFromProject.DataTextField = "TextField";
            ddlSalFromProject.DataBind();
        }
        using (DataTable dt = _commonDataLoad.GetAllProject(ddlCompany.SelectedValue))
        {
            cbl_ContractProject.DataSource = dt;
            cbl_ContractProject.DataValueField = "Value";
            cbl_ContractProject.DataTextField = "TextField";
            cbl_ContractProject.DataBind();
        }


        using (DataTable dt = _commonDataLoad.GetDDLDesignation())
        {
            ddlDesignation.DataSource = dt;
            ddlDesignation.DataValueField = "Value";
            ddlDesignation.DataTextField = "TextField";
            ddlDesignation.DataBind();
        }

    }
    protected void detailsViewButton_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeInfoList.aspx");
    }

    protected void btn_Save_OnClick(object sender, EventArgs e)
    {
        try
        {

            mid = string.IsNullOrEmpty(hdpk.Value) ? 0 : int.Parse(hdpk.Value);
            tblEmpGeneralInfo emp = null;
            using (var db = new HRIS_SMCEntities())
            {
                if (mid > 0)
                {
                    emp = (from j in db.tblEmpGeneralInfoes where j.EmpInfoId == mid select j).FirstOrDefault();

                    #region 1. General Information
                    emp.EmpName = string.IsNullOrEmpty(txt_EmpName.Text) ? null : txt_EmpName.Text;
                    emp.Gender = ddlGender.SelectedIndex > 0 ? ddlGender.SelectedValue : null;
                    emp.BloodGroup = ddlBloodGroup.SelectedIndex > 0 ? ddlBloodGroup.SelectedValue : null;
                    emp.TinNo = string.IsNullOrEmpty(txt_EmpTinNo.Text) ? null : txt_EmpTinNo.Text;

                    emp.FatherName = string.IsNullOrEmpty(txt_EmpFatherName.Text) ? null : txt_EmpFatherName.Text;
                    emp.FatherOccupation = ddlEmpFOccupation.SelectedIndex > 0 ? int.Parse(ddlEmpFOccupation.SelectedValue) : (int?)null;
                    emp.MotherName = string.IsNullOrEmpty(txt_EmpMotherName.Text) ? null : txt_EmpMotherName.Text;
                    emp.MotherOccupation = ddlEmpMOccupation.SelectedIndex > 0 ? int.Parse(ddlEmpMOccupation.SelectedValue) : (int?)null;

                    emp.DateOfBirth = string.IsNullOrEmpty(txt_EmpDOB.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDOB.Text).Date;
                    emp.DateOfJoin = string.IsNullOrEmpty(txt_EmpDOJ.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDOJ.Text).Date;
                    emp.Religion = ddlEmpReligion.SelectedIndex > 0 ? ddlEmpReligion.SelectedValue : null;
                    emp.MaritalStatus = ddlEmpMaritalStatus.SelectedIndex > 0 ? ddlEmpMaritalStatus.SelectedValue : null;

                    emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
                    emp.PlaceOfBirth = string.IsNullOrEmpty(txt_EmpPlaceOfBirth.Text) ? null : txt_EmpPlaceOfBirth.Text;
                    emp.Nationality = ddlEmpNationality.SelectedIndex > 0 ? ddlEmpNationality.SelectedValue : null;
                    emp.NationalIdNo = string.IsNullOrEmpty(txt_EmpNationalID.Text) ? null : txt_EmpNationalID.Text;
                    if (ddlEmpType.SelectedValue == "3")
                    {
                        emp.ContractEndDate = string.IsNullOrEmpty(txt_ContractEndDate.Text) ? (DateTime?)null : DateTime.Parse(txt_ContractEndDate.Text).Date;
                        emp.SalaryFromProject = ddlSalFromProject.SelectedIndex > 0 ? int.Parse(ddlSalFromProject.SelectedValue) : (int?)null;
                        ////TODO
                        //for (int i = 0; i < cbl_ContractProject.Items.Count; i++)
                        //{
                        //    if (cbl_ContractProject.Items[i].Selected)
                        //    {
                        //        var ContractProject = new tblEmpContractProject();
                        //        ContractProject.ProjectId = int.Parse(cbl_ContractProject.Items[i].Value);
                        //        db.tblEmpContractProjects.Add(ContractProject);
                        //    }
                        //}
                    }
                    emp.PassportNo = string.IsNullOrEmpty(txt_EmpPassport.Text) ? null : txt_EmpPassport.Text;
                    emp.ExpectedServiceLength = string.IsNullOrEmpty(txt_EmpExpectedServiceLength.Text) ? null : txt_EmpExpectedServiceLength.Text;
                    emp.DateOfRetirement = string.IsNullOrEmpty(txt_EmpDateOfRetirement.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDateOfRetirement.Text).Date;

                    emp.DateOfConformation = string.IsNullOrEmpty(txt_EmpDateOfConformation.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDateOfConformation.Text).Date;
                    emp.ConformationStatus = string.IsNullOrEmpty(txt_ConformationStatus.Text) ? null : txt_ConformationStatus.Text;
                    emp.ReportingEmpId = string.IsNullOrEmpty(hdReportingBoss.Value) ? (int?)null : int.Parse(hdReportingBoss.Value);

                    emp.IsProbationary = chkIsProbationary.Checked;
                    emp.ProbationEndDate = string.IsNullOrEmpty(txt_ProbationaryEndDate.Text) ? (DateTime?)null : DateTime.Parse(txt_ProbationaryEndDate.Text).Date;
                    emp.EmpImage = hfempimg.Value;
                    #endregion end 1. General Information

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
                    emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
                    emp.JobLocationId = ddlJobLocation.SelectedIndex > 0 ? int.Parse(ddlJobLocation.SelectedValue) : (int?)null;

                    emp.SalaryLoationId = ddlSalaryLocation.SelectedIndex > 0 ? int.Parse(ddlSalaryLocation.SelectedValue) : (int?)null;
                    #endregion end 2. Employment Information

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

                    #region 4. Family Information
                    emp.SpouseName = string.IsNullOrEmpty(txt_EmpSpouseName.Text) ? null : txt_EmpSpouseName.Text;
                    emp.SpouseMaxEducation = ddlEmpSpouseMaxEdu.SelectedIndex > 0 ? int.Parse(ddlEmpSpouseMaxEdu.SelectedValue) : (int?)null;
                    emp.SpouseOccupation = ddlEmpSpouseOccupation.SelectedIndex > 0 ? int.Parse(ddlEmpSpouseOccupation.SelectedValue) : (int?)null;
                    emp.SpouseDateOfBirth = string.IsNullOrEmpty(txt_EmpSpouseDOB.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpSpouseDOB.Text).Date;

                    emp.DateOfMarriage = string.IsNullOrEmpty(txt_EmpMarriageDate.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpMarriageDate.Text).Date;

                    emp.IsActive = true;
                    emp.Updateby = _userId;
                    emp.UpdateDate = DateTime.Now;
                    UpsertFirstSpouse(db, emp);

                    if (gv_Children.Rows.Count > 0)
                    {
                        //making previous entris inactive

                        db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpChildren SET IsActive=0 WHERE EmpInfoId={0}", emp.EmpInfoId);
                        for (int i = 0; i < gv_Children.Rows.Count; i++)
                        {
                            HiddenField EmpChildrenId = (HiddenField)gv_Children.Rows[i].FindControl("EmpChildrenId");
                            Label lbl_ChildrenName = (Label)gv_Children.Rows[i].FindControl("lbl_ChildrenName");
                            Label lbl_ChildrenGender = (Label)gv_Children.Rows[i].FindControl("lbl_ChildrenGender");
                            HiddenField hfChildrenOccupation = (HiddenField)gv_Children.Rows[i].FindControl("hfChildrenOccupation");
                            Label lbl_ChildrenDOB = (Label)gv_Children.Rows[i].FindControl("lbl_ChildrenDOB");
                            Label lbl_ChildrenMaritalStatus = (Label)gv_Children.Rows[i].FindControl("lbl_ChildrenMaritalStatus");

                            if (string.IsNullOrEmpty(EmpChildrenId.Value))
                            {
                                tblEmpChildren children = new tblEmpChildren();

                                children.EmpInfoId = emp.EmpInfoId;
                                children.ChildrenName = string.IsNullOrEmpty(lbl_ChildrenName.Text) ? null : lbl_ChildrenName.Text;
                                children.ChildrenGender = string.IsNullOrEmpty(lbl_ChildrenGender.Text) ? null : lbl_ChildrenGender.Text;
                                children.ChildrenOccupation = string.IsNullOrEmpty(hfChildrenOccupation.Value) ? (int?)null : int.Parse(hfChildrenOccupation.Value);
                                children.ChildrenDOB = string.IsNullOrEmpty(lbl_ChildrenDOB.Text) ? (DateTime?)null : DateTime.Parse(lbl_ChildrenDOB.Text).Date;
                                children.ChildrenMaritalStatus = string.IsNullOrEmpty(lbl_ChildrenMaritalStatus.Text) ? null : lbl_ChildrenMaritalStatus.Text;
                                children.IsActive = true;
                                db.tblEmpChildrens.Add(children);
                            }
                            else
                            {
                                int u_EmpChildrenId = int.Parse(EmpChildrenId.Value);
                                tblEmpChildren children = (from j in db.tblEmpChildrens where j.EmpChildrenId == u_EmpChildrenId select j).FirstOrDefault();

                                children.EmpInfoId = emp.EmpInfoId;
                                children.ChildrenName = string.IsNullOrEmpty(lbl_ChildrenName.Text) ? null : lbl_ChildrenName.Text;
                                children.ChildrenGender = string.IsNullOrEmpty(lbl_ChildrenGender.Text) ? null : lbl_ChildrenGender.Text;
                                children.ChildrenOccupation = string.IsNullOrEmpty(hfChildrenOccupation.Value) ? (int?)null : int.Parse(hfChildrenOccupation.Value);
                                children.ChildrenDOB = string.IsNullOrEmpty(lbl_ChildrenDOB.Text) ? (DateTime?)null : DateTime.Parse(lbl_ChildrenDOB.Text).Date;
                                children.ChildrenMaritalStatus = string.IsNullOrEmpty(lbl_ChildrenMaritalStatus.Text) ? null : lbl_ChildrenMaritalStatus.Text;
                                children.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    }////End Childrens
                    #endregion end 4. Family Information

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
                                empEducation.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    }////End Educations
                    #endregion end 5. Education

                    #region 6. Experience
                    if (gv_Experience.Rows.Count > 0)
                    {
                        //making previous inactive
                        db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpExperience SET IsActive=0 WHERE EmpInfoId={0}", emp.EmpInfoId);
                        for (int i = 0; i < gv_Experience.Rows.Count; i++)
                        {
                            HiddenField EmpExperienceId = (HiddenField)gv_Experience.Rows[i].FindControl("EmpExperienceId");
                            Label lbl_ExpCompany = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpCompany");
                            Label lbl_ExpContactPerson = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpContactPerson");
                            Label lbl_ExpAddress = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpAddress");
                            Label lbl_ExpNatureofBusiness = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpNatureofBusiness");
                            Label lbl_ExpJobType = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpJobType");
                            Label lbl_ExpLeavingSalary = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpLeavingSalary");

                            Label lbl_ExpFromDate = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpFromDate");
                            Label lbl_ExpToDate = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpToDate");
                            Label lbl_ExpLastJob = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpLastJob");
                            Label lbl_ExpDesignation = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpDesignation");
                            Label lbl_ExpJobDescription = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpJobDescription");
                            Label lbl_ExpTelNo = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpTelNo");
                            Label lbl_ExpRemarks = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpRemarks");

                            if (string.IsNullOrEmpty(EmpExperienceId.Value))
                            {
                                tblEmpExperience empExperience = new tblEmpExperience();
                                empExperience.EmpInfoId = emp.EmpInfoId;
                                empExperience.ExpCompany = string.IsNullOrEmpty(lbl_ExpCompany.Text) ? null : lbl_ExpCompany.Text;
                                empExperience.ExpContactPerson = string.IsNullOrEmpty(lbl_ExpContactPerson.Text) ? null : lbl_ExpContactPerson.Text;
                                empExperience.ExpAddress = string.IsNullOrEmpty(lbl_ExpAddress.Text) ? null : lbl_ExpAddress.Text;
                                empExperience.ExpNatureofBusiness = string.IsNullOrEmpty(lbl_ExpNatureofBusiness.Text) ? null : lbl_ExpNatureofBusiness.Text;
                                empExperience.ExpJobType = string.IsNullOrEmpty(lbl_ExpJobType.Text) ? null : lbl_ExpJobType.Text;
                                empExperience.ExpLeavingSalary = string.IsNullOrEmpty(lbl_ExpLeavingSalary.Text) ? (decimal?)null : decimal.Parse(lbl_ExpLeavingSalary.Text);
                                empExperience.ExpFromDate = string.IsNullOrEmpty(lbl_ExpFromDate.Text) ? (DateTime?)null : DateTime.Parse(lbl_ExpFromDate.Text).Date;
                                empExperience.ExpToDate = string.IsNullOrEmpty(lbl_ExpToDate.Text) ? (DateTime?)null : DateTime.Parse(lbl_ExpToDate.Text).Date;
                                empExperience.ExpLastJob = string.IsNullOrEmpty(lbl_ExpLastJob.Text) ? (bool?)null : bool.Parse(lbl_ExpLastJob.Text);
                                empExperience.ExpDesignation = string.IsNullOrEmpty(lbl_ExpDesignation.Text) ? null : lbl_ExpDesignation.Text;
                                empExperience.ExpJobDescription = string.IsNullOrEmpty(lbl_ExpJobDescription.Text) ? null : lbl_ExpJobDescription.Text;
                                empExperience.ExpTelNo = string.IsNullOrEmpty(lbl_ExpTelNo.Text) ? null : lbl_ExpTelNo.Text;
                                empExperience.ExpRemarks = string.IsNullOrEmpty(lbl_ExpRemarks.Text) ? null : lbl_ExpRemarks.Text;
                                empExperience.IsActive = true;
                                db.tblEmpExperiences.Add(empExperience);
                            }
                            else
                            {
                                int u_EmpExperienceId = int.Parse(EmpExperienceId.Value);
                                tblEmpExperience empExperience = (from j in db.tblEmpExperiences where j.EmpExperienceId == u_EmpExperienceId select j).FirstOrDefault();
                                empExperience.EmpInfoId = emp.EmpInfoId;
                                empExperience.ExpCompany = string.IsNullOrEmpty(lbl_ExpCompany.Text) ? null : lbl_ExpCompany.Text;
                                empExperience.ExpContactPerson = string.IsNullOrEmpty(lbl_ExpContactPerson.Text) ? null : lbl_ExpContactPerson.Text;
                                empExperience.ExpAddress = string.IsNullOrEmpty(lbl_ExpAddress.Text) ? null : lbl_ExpAddress.Text;
                                empExperience.ExpNatureofBusiness = string.IsNullOrEmpty(lbl_ExpNatureofBusiness.Text) ? null : lbl_ExpNatureofBusiness.Text;
                                empExperience.ExpJobType = string.IsNullOrEmpty(lbl_ExpJobType.Text) ? null : lbl_ExpJobType.Text;
                                empExperience.ExpLeavingSalary = string.IsNullOrEmpty(lbl_ExpLeavingSalary.Text) ? (decimal?)null : decimal.Parse(lbl_ExpLeavingSalary.Text);
                                empExperience.ExpFromDate = string.IsNullOrEmpty(lbl_ExpFromDate.Text) ? (DateTime?)null : DateTime.Parse(lbl_ExpFromDate.Text).Date;
                                empExperience.ExpToDate = string.IsNullOrEmpty(lbl_ExpToDate.Text) ? (DateTime?)null : DateTime.Parse(lbl_ExpToDate.Text).Date;
                                empExperience.ExpLastJob = string.IsNullOrEmpty(lbl_ExpLastJob.Text) ? (bool?)null : bool.Parse(lbl_ExpLastJob.Text);
                                empExperience.ExpDesignation = string.IsNullOrEmpty(lbl_ExpDesignation.Text) ? null : lbl_ExpDesignation.Text;
                                empExperience.ExpJobDescription = string.IsNullOrEmpty(lbl_ExpJobDescription.Text) ? null : lbl_ExpJobDescription.Text;
                                empExperience.ExpTelNo = string.IsNullOrEmpty(lbl_ExpTelNo.Text) ? null : lbl_ExpTelNo.Text;
                                empExperience.ExpRemarks = string.IsNullOrEmpty(lbl_ExpRemarks.Text) ? null : lbl_ExpRemarks.Text;
                                empExperience.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    }////End Experience
                    #endregion end 6. Experience

                    #region 7. Training
                    if (gv_Training.Rows.Count > 0)
                    {
                        //making previous inactive
                        db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpTraining SET IsActive=0 WHERE EmpInfoId={0}", emp.EmpInfoId);
                        for (int i = 0; i < gv_Training.Rows.Count; i++)
                        {
                            HiddenField EmpTrainingId = (HiddenField)gv_Training.Rows[i].FindControl("EmpTrainingId");
                            Label lbl_TrainingName = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingName");
                            HiddenField hfTrainingType = (HiddenField)gv_Training.Rows[i].FindControl("hfTrainingType");
                            Label lbl_TrainingDescription = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingDescription");
                            HiddenField hfTrainingInstitution = (HiddenField)gv_Training.Rows[i].FindControl("hfTrainingInstitution");
                            Label lbl_TrainingPlace = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingPlace");
                            HiddenField hfTrainingCountry = (HiddenField)gv_Training.Rows[i].FindControl("hfTrainingCountry");
                            Label lbl_TrainingAchievment = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingAchievment");
                            Label lbl_TrFromDate = (Label)gv_Training.Rows[i].FindControl("lbl_TrFromDate");
                            Label lbl_TrToDate = (Label)gv_Training.Rows[i].FindControl("lbl_TrToDate");
                            Label lbl_TrainingDays = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingDays");
                            Label lbl_TrRemarks = (Label)gv_Training.Rows[i].FindControl("lbl_TrRemarks");

                            if (string.IsNullOrEmpty(EmpTrainingId.Value))
                            {
                                tblEmpTraining empTraining = new tblEmpTraining();
                                empTraining.EmpInfoId = emp.EmpInfoId;
                                empTraining.TrainingName = string.IsNullOrEmpty(lbl_TrainingName.Text) ? null : lbl_TrainingName.Text;
                                empTraining.TrainingType = string.IsNullOrEmpty(hfTrainingType.Value) ? (int?)null : int.Parse(hfTrainingType.Value);
                                empTraining.TrainingDescription = string.IsNullOrEmpty(lbl_TrainingDescription.Text) ? null : lbl_TrainingDescription.Text;
                                empTraining.TrainingInstitution = string.IsNullOrEmpty(hfTrainingInstitution.Value) ? (int?)null : int.Parse(hfTrainingInstitution.Value);
                                empTraining.TrainingPlace = string.IsNullOrEmpty(lbl_TrainingPlace.Text) ? null : lbl_TrainingPlace.Text;
                                empTraining.TrainingCountry = string.IsNullOrEmpty(hfTrainingCountry.Value) ? (int?)null : int.Parse(hfTrainingCountry.Value);
                                empTraining.TrainingAchievment = string.IsNullOrEmpty(lbl_TrainingAchievment.Text) ? null : lbl_TrainingAchievment.Text;
                                empTraining.TrFromDate = string.IsNullOrEmpty(lbl_TrFromDate.Text) ? (DateTime?)null : DateTime.Parse(lbl_TrFromDate.Text).Date;
                                empTraining.TrToDate = string.IsNullOrEmpty(lbl_TrToDate.Text) ? (DateTime?)null : DateTime.Parse(lbl_TrToDate.Text).Date;
                                empTraining.TrainingDays = string.IsNullOrEmpty(lbl_TrainingDays.Text) ? (int?)null : int.Parse(lbl_TrainingDays.Text);
                                empTraining.TrRemarks = string.IsNullOrEmpty(lbl_TrRemarks.Text) ? null : lbl_TrRemarks.Text;
                                empTraining.IsActive = true;
                                db.tblEmpTrainings.Add(empTraining);
                            }
                            else
                            {
                                int u_EmpTrainingId = int.Parse(EmpTrainingId.Value);
                                tblEmpTraining empTraining = (from j in db.tblEmpTrainings where j.EmpTrainingId == u_EmpTrainingId select j).FirstOrDefault();
                                empTraining.EmpInfoId = emp.EmpInfoId;
                                empTraining.TrainingName = string.IsNullOrEmpty(lbl_TrainingName.Text) ? null : lbl_TrainingName.Text;
                                empTraining.TrainingType = string.IsNullOrEmpty(hfTrainingType.Value) ? (int?)null : int.Parse(hfTrainingType.Value);
                                empTraining.TrainingDescription = string.IsNullOrEmpty(lbl_TrainingDescription.Text) ? null : lbl_TrainingDescription.Text;
                                empTraining.TrainingInstitution = string.IsNullOrEmpty(hfTrainingInstitution.Value) ?(int?)null : int.Parse(hfTrainingInstitution.Value);
                                empTraining.TrainingPlace = string.IsNullOrEmpty(lbl_TrainingPlace.Text) ? null : lbl_TrainingPlace.Text;
                                empTraining.TrainingCountry = string.IsNullOrEmpty(hfTrainingCountry.Value) ? (int?)null : int.Parse(hfTrainingCountry.Value);
                                empTraining.TrainingAchievment = string.IsNullOrEmpty(lbl_TrainingAchievment.Text) ? null : lbl_TrainingAchievment.Text;
                                empTraining.TrFromDate = string.IsNullOrEmpty(lbl_TrFromDate.Text) ? (DateTime?)null : DateTime.Parse(lbl_TrFromDate.Text).Date;
                                empTraining.TrToDate = string.IsNullOrEmpty(lbl_TrToDate.Text) ? (DateTime?)null : DateTime.Parse(lbl_TrToDate.Text).Date;
                                empTraining.TrainingDays = string.IsNullOrEmpty(lbl_TrainingDays.Text) ? (int?)null : int.Parse(lbl_TrainingDays.Text);
                                empTraining.TrRemarks = string.IsNullOrEmpty(lbl_TrRemarks.Text) ? null : lbl_TrRemarks.Text;
                                empTraining.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    }////End Training
                    #endregion end 7. Training

                    #region 8. Reference
                    if (gv_Reference.Rows.Count > 0)
                    {
                        //making previous inactive
                        db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpReference SET IsActive=0 WHERE EmpInfoId={0}", emp.EmpInfoId);
                        for (int i = 0; i < gv_Reference.Rows.Count; i++)
                        {
                            HiddenField EmpReferenceId = (HiddenField)gv_Reference.Rows[i].FindControl("EmpReferenceId");
                            Label lbl_ReferenceName = (Label)gv_Reference.Rows[i].FindControl("lbl_ReferenceName");
                            HiddenField hfRefOccupation = (HiddenField)gv_Reference.Rows[i].FindControl("hfRefOccupation");
                            Label lbl_RefAddress = (Label)gv_Reference.Rows[i].FindControl("lbl_RefAddress");
                            Label lbl_RefMobile = (Label)gv_Reference.Rows[i].FindControl("lbl_RefMobile");

                            if (string.IsNullOrEmpty(EmpReferenceId.Value))
                            {
                                tblEmpReference empReference = new tblEmpReference();
                                empReference.EmpInfoId = emp.EmpInfoId;
                                empReference.ReferenceName = string.IsNullOrEmpty(lbl_ReferenceName.Text) ? null : lbl_ReferenceName.Text;
                                empReference.RefOccupation = string.IsNullOrEmpty(hfRefOccupation.Value) ? (int?)null : int.Parse(hfRefOccupation.Value);
                                empReference.RefAddress = string.IsNullOrEmpty(lbl_RefAddress.Text) ? null : lbl_RefAddress.Text;
                                empReference.RefMobile = string.IsNullOrEmpty(lbl_RefMobile.Text) ? null : lbl_RefMobile.Text;
                                empReference.IsActive = true;
                                db.tblEmpReferences.Add(empReference);
                            }
                            else
                            {
                                int u_EmpReferenceId = int.Parse(EmpReferenceId.Value);
                                tblEmpReference empReference = (from j in db.tblEmpReferences where j.EmpReferenceId == u_EmpReferenceId select j).FirstOrDefault();
                                empReference.EmpInfoId = emp.EmpInfoId;
                                empReference.ReferenceName = string.IsNullOrEmpty(lbl_ReferenceName.Text) ? null : lbl_ReferenceName.Text;
                                empReference.RefOccupation = string.IsNullOrEmpty(hfRefOccupation.Value) ? (int?)null : int.Parse(hfRefOccupation.Value);
                                empReference.RefAddress = string.IsNullOrEmpty(lbl_RefAddress.Text) ? null : lbl_RefAddress.Text;
                                empReference.RefMobile = string.IsNullOrEmpty(lbl_RefMobile.Text) ? null : lbl_RefMobile.Text;
                                empReference.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    }////End Reference
                    #endregion end 8. Reference

                    #region 9. Nominee
                    if (gv_Nominee.Rows.Count > 0)
                    {
                        //making previous inactive
                        db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpNominee SET IsActive=0 WHERE EmpInfoId={0}", emp.EmpInfoId);
                        for (int i = 0; i < gv_Nominee.Rows.Count; i++)
                        {
                            HiddenField EmpNomineeId = (HiddenField)gv_Nominee.Rows[i].FindControl("EmpNomineeId");
                            HiddenField hfNominationPurpose = (HiddenField)gv_Nominee.Rows[i].FindControl("hfNominationPurpose");
                            HiddenField hfNomineeRelation = (HiddenField)gv_Nominee.Rows[i].FindControl("hfNomineeRelation");
                            HiddenField hfNomineeOccupation = (HiddenField)gv_Nominee.Rows[i].FindControl("hfNomineeOccupation");
                            Label lbl_NomineeName = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeName");
                            Label lbl_DateOfNomination = (Label)gv_Nominee.Rows[i].FindControl("lbl_DateOfNomination");
                            Label lbl_NominationPercentage = (Label)gv_Nominee.Rows[i].FindControl("lbl_NominationPercentage");
                            Label lbl_NomineeDOB = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeDOB");
                            Label lbl_NomineeTelephone = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeTelephone");
                            Label lbl_NomineeAddress = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeAddress");

                            if (string.IsNullOrEmpty(EmpNomineeId.Value)) 
                            {
                                tblEmpNominee empNominee = new tblEmpNominee();
                                empNominee.EmpInfoId = emp.EmpInfoId;
                                empNominee.NominationPurpose = string.IsNullOrEmpty(hfNominationPurpose.Value) ? (int?)null : int.Parse(hfNominationPurpose.Value);
                                empNominee.NomineeRelation = string.IsNullOrEmpty(hfNomineeRelation.Value) ? (int?)null : int.Parse(hfNomineeRelation.Value);
                                empNominee.NomineeOccupation = string.IsNullOrEmpty(hfNomineeOccupation.Value) ? (int?)null : int.Parse(hfNomineeOccupation.Value);
                                empNominee.NomineeName = string.IsNullOrEmpty(lbl_NomineeName.Text) ? null : lbl_NomineeName.Text;
                                empNominee.DateOfNomination = string.IsNullOrEmpty(lbl_DateOfNomination.Text) ? (DateTime?)null : DateTime.Parse(lbl_DateOfNomination.Text).Date;
                                empNominee.NominationPercentage = string.IsNullOrEmpty(lbl_NominationPercentage.Text) ? (decimal?)null : decimal.Parse(lbl_NominationPercentage.Text);
                                empNominee.NomineeDOB = string.IsNullOrEmpty(lbl_NomineeDOB.Text) ? (DateTime?)null : DateTime.Parse(lbl_NomineeDOB.Text).Date;

                                empNominee.NomineeTelephone = string.IsNullOrEmpty(lbl_NomineeTelephone.Text) ? null : lbl_NomineeTelephone.Text;
                                empNominee.NomineeAddress = string.IsNullOrEmpty(lbl_NomineeAddress.Text) ? null : lbl_NomineeAddress.Text;
                                empNominee.IsActive = true;
                                db.tblEmpNominees.Add(empNominee);
                            }
                            else
                            {
                                int u_EmpNomineeId = int.Parse(EmpNomineeId.Value);
                                tblEmpNominee empNominee = (from j in db.tblEmpNominees where j.EmpNomineeId == u_EmpNomineeId select j).FirstOrDefault();
                                empNominee.EmpInfoId = emp.EmpInfoId;
                                empNominee.NominationPurpose = string.IsNullOrEmpty(hfNominationPurpose.Value) ? (int?)null : int.Parse(hfNominationPurpose.Value);
                                empNominee.NomineeRelation = string.IsNullOrEmpty(hfNomineeRelation.Value) ? (int?)null : int.Parse(hfNomineeRelation.Value);
                                empNominee.NomineeOccupation = string.IsNullOrEmpty(hfNomineeOccupation.Value) ? (int?)null : int.Parse(hfNomineeOccupation.Value);
                                empNominee.NomineeName = string.IsNullOrEmpty(lbl_NomineeName.Text) ? null : lbl_NomineeName.Text;
                                empNominee.DateOfNomination = string.IsNullOrEmpty(lbl_DateOfNomination.Text) ? (DateTime?)null : DateTime.Parse(lbl_DateOfNomination.Text).Date;
                                empNominee.NominationPercentage = string.IsNullOrEmpty(lbl_NominationPercentage.Text) ? (decimal?)null : decimal.Parse(lbl_NominationPercentage.Text);
                                empNominee.NomineeDOB = string.IsNullOrEmpty(lbl_NomineeDOB.Text) ? (DateTime?)null : DateTime.Parse(lbl_NomineeDOB.Text).Date;

                                empNominee.NomineeTelephone = string.IsNullOrEmpty(lbl_NomineeTelephone.Text) ? null : lbl_NomineeTelephone.Text;
                                empNominee.NomineeAddress = string.IsNullOrEmpty(lbl_NomineeAddress.Text) ? null : lbl_NomineeAddress.Text;
                                empNominee.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    }////End Nominee
                    #endregion end 9. Nominee

                    #region 10. Others

                    //making previous inactive
                    db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpExtraCurriculam SET IsActive=0 WHERE EmpInfoId={0}", emp.EmpInfoId);
                    foreach (ListItem item in chkExtraCurriculam.Items)
                    {
                        if (item.Selected)
                        {
                            var MasterExtraCurriculamId = int.Parse(item.Value);
                            var empExtraCurriculam = (from j in db.tblEmpExtraCurriculams 
                                                      where j.EmpInfoId == emp.EmpInfoId
                                                      && j.MasterExtraCurriculamId == MasterExtraCurriculamId
                                                      select j).FirstOrDefault();
                            if (empExtraCurriculam==null)
                            {
                                empExtraCurriculam = new tblEmpExtraCurriculam();
                                empExtraCurriculam.EmpInfoId = emp.EmpInfoId;
                                empExtraCurriculam.MasterExtraCurriculamId = MasterExtraCurriculamId;
                                empExtraCurriculam.IsActive = true;
                                db.tblEmpExtraCurriculams.Add(empExtraCurriculam);
                            }
                            else
                            {
                                empExtraCurriculam.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    }////End ExtraCurriculam


                    //making previous inactive
                    db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpOtherTalents SET IsActive=0 WHERE EmpInfoId={0}", emp.EmpInfoId);
                    foreach (ListItem item in chkOtherTalents.Items)
                    {
                        if (item.Selected)
                        {
                            var MasterOtherTalentsId = int.Parse(item.Value);
                            var empOtherTalent = (from j in db.tblEmpOtherTalents
                                where j.EmpInfoId == emp.EmpInfoId
                                      && j.MasterOtherTalentsId == MasterOtherTalentsId
                                select j).FirstOrDefault();
                            if (empOtherTalent == null)
                            {
                                empOtherTalent = new tblEmpOtherTalent();
                                empOtherTalent.EmpInfoId = emp.EmpInfoId;
                                empOtherTalent.MasterOtherTalentsId = MasterOtherTalentsId;
                                empOtherTalent.IsActive = true;
                                db.tblEmpOtherTalents.Add(empOtherTalent);
                            }
                            else
                            {
                                empOtherTalent.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    }////End OtherTalents

                    //making previous inactive
                    db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpAchievements SET IsActive=0 WHERE EmpInfoId={0}", emp.EmpInfoId);
                    foreach (ListItem item in chkAchievements.Items)
                    {
                        if (item.Selected)
                        {
                            var MasterAchievementsId = int.Parse(item.Value);
                            var empAchievement = (from j in db.tblEmpAchievements
                                where j.EmpInfoId == emp.EmpInfoId
                                      && j.MasterAchievementsId == MasterAchievementsId
                                select j).FirstOrDefault();
                            if (empAchievement == null)
                            {
                                empAchievement = new tblEmpAchievement();
                                empAchievement.EmpInfoId = emp.EmpInfoId;
                                empAchievement.MasterAchievementsId = MasterAchievementsId;
                                empAchievement.IsActive = true;
                                db.tblEmpAchievements.Add(empAchievement);
                            }
                            else
                            {
                                empAchievement.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    }////End Achievements

                    //making previous inactive
                    db.Database.ExecuteSqlCommand("UPDATE dbo.tblEmpHobby SET IsActive=0 WHERE EmpInfoId={0}", emp.EmpInfoId);
                    foreach (ListItem item in chkHobby.Items)
                    {
                        if (item.Selected)
                        {
                            var MasterHobbyId = int.Parse(item.Value);
                            var empHobby = (from j in db.tblEmpHobbies
                                where j.EmpInfoId == emp.EmpInfoId
                                      && j.MasterHobbyId == MasterHobbyId
                                select j).FirstOrDefault();
                            if (empHobby == null)
                            {
                                empHobby = new tblEmpHobby();
                                empHobby.EmpInfoId = emp.EmpInfoId;
                                empHobby.MasterHobbyId = MasterHobbyId;
                                empHobby.IsActive = true;
                                db.tblEmpHobbies.Add(empHobby);
                            }
                            else
                            {
                                empHobby.IsActive = true;
                            }
                            db.SaveChanges();
                        }
                    }////End Hobby
                    #endregion end 10. Others

                    db.SaveChanges();
                }////End Edit Mode
                else
                {////Start New Mode
                    emp = new tblEmpGeneralInfo();

                    #region 1. General Information
                    emp.EmpName = string.IsNullOrEmpty(txt_EmpName.Text) ? null : txt_EmpName.Text;
                    emp.Gender = ddlGender.SelectedIndex > 0 ? ddlGender.SelectedValue : null;
                    emp.BloodGroup = ddlBloodGroup.SelectedIndex > 0 ? ddlBloodGroup.SelectedValue : null;
                    emp.TinNo = string.IsNullOrEmpty(txt_EmpTinNo.Text) ? null : txt_EmpTinNo.Text;

                    emp.FatherName = string.IsNullOrEmpty(txt_EmpFatherName.Text) ? null : txt_EmpFatherName.Text;
                    emp.FatherOccupation = ddlEmpFOccupation.SelectedIndex > 0 ? int.Parse(ddlEmpFOccupation.SelectedValue) : (int?)null;
                    emp.MotherName = string.IsNullOrEmpty(txt_EmpMotherName.Text) ? null : txt_EmpMotherName.Text;
                    emp.MotherOccupation = ddlEmpMOccupation.SelectedIndex > 0 ? int.Parse(ddlEmpMOccupation.SelectedValue) : (int?)null;

                    emp.DateOfBirth = string.IsNullOrEmpty(txt_EmpDOB.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDOB.Text).Date;
                    emp.DateOfJoin = string.IsNullOrEmpty(txt_EmpDOJ.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDOJ.Text).Date;
                    emp.Religion = ddlEmpReligion.SelectedIndex > 0 ? ddlEmpReligion.SelectedValue : null;
                    emp.MaritalStatus = ddlEmpMaritalStatus.SelectedIndex > 0 ? ddlEmpMaritalStatus.SelectedValue : null;

                    emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
                    emp.PlaceOfBirth = string.IsNullOrEmpty(txt_EmpPlaceOfBirth.Text) ? null : txt_EmpPlaceOfBirth.Text;
                    emp.Nationality = ddlEmpNationality.SelectedIndex > 0 ? ddlEmpNationality.SelectedValue : null;
                    emp.NationalIdNo = string.IsNullOrEmpty(txt_EmpNationalID.Text) ? null : txt_EmpNationalID.Text;
                    if (ddlEmpType.SelectedValue=="3")
                    {
                        emp.ContractEndDate = string.IsNullOrEmpty(txt_ContractEndDate.Text) ? (DateTime?)null : DateTime.Parse(txt_ContractEndDate.Text).Date;
                        emp.SalaryFromProject = ddlSalFromProject.SelectedIndex > 0 ? int.Parse(ddlSalFromProject.SelectedValue) : (int?)null;

                        for (int i = 0; i < cbl_ContractProject.Items.Count; i++)
                        {
                            if (cbl_ContractProject.Items[i].Selected)
                            {
                                var ContractProject = new tblEmpContractProject();
                                ContractProject.ProjectId = int.Parse(cbl_ContractProject.Items[i].Value);
                                db.tblEmpContractProjects.Add(ContractProject);
                            }
                        }
                    }
                    emp.PassportNo = string.IsNullOrEmpty(txt_EmpPassport.Text) ? null : txt_EmpPassport.Text;
                    emp.ExpectedServiceLength = string.IsNullOrEmpty(txt_EmpExpectedServiceLength.Text) ? null : txt_EmpExpectedServiceLength.Text;
                    emp.DateOfRetirement = string.IsNullOrEmpty(txt_EmpDateOfRetirement.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDateOfRetirement.Text).Date;

                    emp.DateOfConformation = string.IsNullOrEmpty(txt_EmpDateOfConformation.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpDateOfConformation.Text).Date;
                    emp.ConformationStatus = string.IsNullOrEmpty(txt_ConformationStatus.Text) ? null : txt_ConformationStatus.Text;
                    emp.ReportingEmpId = string.IsNullOrEmpty(hdReportingBoss.Value) ? (int?)null : int.Parse(hdReportingBoss.Value);

                    emp.IsProbationary = chkIsProbationary.Checked;
                    emp.ProbationEndDate = string.IsNullOrEmpty(txt_ProbationaryEndDate.Text) ? (DateTime?)null : DateTime.Parse(txt_ProbationaryEndDate.Text).Date;
                    emp.EmpImage = hfempimg.Value;
                    #endregion end 1. General Information

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
                    emp.EmpTypeId = ddlEmpType.SelectedIndex > 0 ? int.Parse(ddlEmpType.SelectedValue) : (int?)null;
                    emp.JobLocationId = ddlJobLocation.SelectedIndex > 0 ? int.Parse(ddlJobLocation.SelectedValue) : (int?)null;

                    emp.SalaryLoationId = ddlSalaryLocation.SelectedIndex > 0 ? int.Parse(ddlSalaryLocation.SelectedValue) : (int?)null;
                    #endregion end 2. Employment Information

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

                    #region 4. Family Information
                    emp.SpouseName = string.IsNullOrEmpty(txt_EmpSpouseName.Text) ? null : txt_EmpSpouseName.Text;
                    emp.SpouseMaxEducation = ddlEmpSpouseMaxEdu.SelectedIndex > 0 ? int.Parse(ddlEmpSpouseMaxEdu.SelectedValue) : (int?)null;
                    emp.SpouseOccupation = ddlEmpSpouseOccupation.SelectedIndex > 0 ? int.Parse(ddlEmpSpouseOccupation.SelectedValue) : (int?)null;
                    emp.SpouseDateOfBirth = string.IsNullOrEmpty(txt_EmpSpouseDOB.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpSpouseDOB.Text).Date;

                    emp.DateOfMarriage = string.IsNullOrEmpty(txt_EmpMarriageDate.Text) ? (DateTime?)null : DateTime.Parse(txt_EmpMarriageDate.Text).Date;
                    
                    emp.IsActive = true;
                    emp.EntryBy = _userId;
                    emp.EntryDate = DateTime.Now;
                    db.tblEmpGeneralInfoes.Add(emp);
                    db.SaveChanges();
                    UpsertFirstSpouse(db, emp);
                    emp.EmpMasterCode = "EM" + (1000+emp.EmpInfoId);
                    if (gv_Children.Rows.Count > 0)
                    {
                        for (int i = 0; i < gv_Children.Rows.Count; i++)
                        {
                            HiddenField EmpChildrenId = (HiddenField)gv_Children.Rows[i].FindControl("EmpChildrenId");
                            Label lbl_ChildrenName = (Label)gv_Children.Rows[i].FindControl("lbl_ChildrenName");
                            Label lbl_ChildrenGender = (Label)gv_Children.Rows[i].FindControl("lbl_ChildrenGender");
                            HiddenField hfChildrenOccupation = (HiddenField)gv_Children.Rows[i].FindControl("hfChildrenOccupation");
                            Label lbl_ChildrenDOB = (Label)gv_Children.Rows[i].FindControl("lbl_ChildrenDOB");
                            Label lbl_ChildrenMaritalStatus = (Label)gv_Children.Rows[i].FindControl("lbl_ChildrenMaritalStatus");
                            tblEmpChildren children = new tblEmpChildren();

                            children.EmpInfoId = emp.EmpInfoId;
                            children.ChildrenName = string.IsNullOrEmpty(lbl_ChildrenName.Text) ? null : lbl_ChildrenName.Text;
                            children.ChildrenGender = string.IsNullOrEmpty(lbl_ChildrenGender.Text) ? null : lbl_ChildrenGender.Text;
                            children.ChildrenOccupation = string.IsNullOrEmpty(hfChildrenOccupation.Value) ? (int?)null : int.Parse(hfChildrenOccupation.Value);
                            children.ChildrenDOB = string.IsNullOrEmpty(lbl_ChildrenDOB.Text) ? (DateTime?)null : DateTime.Parse(lbl_ChildrenDOB.Text).Date;
                            children.ChildrenMaritalStatus = string.IsNullOrEmpty(lbl_ChildrenMaritalStatus.Text) ? null : lbl_ChildrenMaritalStatus.Text;
                            children.IsActive = true;
                            db.tblEmpChildrens.Add(children);
                        }
                    }////End Childrens
                    #endregion end 4. Family Information

                    #region 5. Education
                    if (gv_Education.Rows.Count > 0)
                    {
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
                            empEducation.IsActive = true;
                            db.tblEmpEducations.Add(empEducation);
                        }
                    }////End Educations
                    #endregion end 5. Education

                    #region 6. Experience
                    if (gv_Experience.Rows.Count > 0)
                    {
                        for (int i = 0; i < gv_Experience.Rows.Count; i++)
                        {
                            HiddenField EmpExperienceId = (HiddenField)gv_Experience.Rows[i].FindControl("EmpExperienceId");
                            Label lbl_ExpCompany = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpCompany");
                            Label lbl_ExpContactPerson = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpContactPerson");
                            Label lbl_ExpAddress = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpAddress");
                            Label lbl_ExpNatureofBusiness = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpNatureofBusiness");
                            Label lbl_ExpJobType = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpJobType");
                            Label lbl_ExpLeavingSalary = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpLeavingSalary");

                            Label lbl_ExpFromDate = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpFromDate");
                            Label lbl_ExpToDate = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpToDate");
                            Label lbl_ExpLastJob = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpLastJob");
                            Label lbl_ExpDesignation = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpDesignation");
                            Label lbl_ExpJobDescription = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpJobDescription");
                            Label lbl_ExpTelNo = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpTelNo");
                            Label lbl_ExpRemarks = (Label)gv_Experience.Rows[i].FindControl("lbl_ExpRemarks");

                            tblEmpExperience empExperience = new tblEmpExperience();
                            empExperience.EmpInfoId = emp.EmpInfoId;
                            empExperience.ExpCompany = string.IsNullOrEmpty(lbl_ExpCompany.Text) ? null : lbl_ExpCompany.Text;
                            empExperience.ExpContactPerson = string.IsNullOrEmpty(lbl_ExpContactPerson.Text) ? null : lbl_ExpContactPerson.Text;
                            empExperience.ExpAddress = string.IsNullOrEmpty(lbl_ExpAddress.Text) ? null : lbl_ExpAddress.Text;
                            empExperience.ExpNatureofBusiness = string.IsNullOrEmpty(lbl_ExpNatureofBusiness.Text) ? null : lbl_ExpNatureofBusiness.Text;
                            empExperience.ExpJobType = string.IsNullOrEmpty(lbl_ExpJobType.Text) ? null : lbl_ExpJobType.Text;
                            empExperience.ExpLeavingSalary = string.IsNullOrEmpty(lbl_ExpLeavingSalary.Text) ? (decimal?)null : decimal.Parse(lbl_ExpLeavingSalary.Text);
                            empExperience.ExpFromDate = string.IsNullOrEmpty(lbl_ExpFromDate.Text) ? (DateTime?)null : DateTime.Parse(lbl_ExpFromDate.Text).Date;
                            empExperience.ExpToDate = string.IsNullOrEmpty(lbl_ExpToDate.Text) ? (DateTime?)null : DateTime.Parse(lbl_ExpToDate.Text).Date;
                            empExperience.ExpLastJob = string.IsNullOrEmpty(lbl_ExpLastJob.Text) ? (bool?)null : bool.Parse(lbl_ExpLastJob.Text);
                            empExperience.ExpDesignation = string.IsNullOrEmpty(lbl_ExpDesignation.Text) ? null : lbl_ExpDesignation.Text;
                            empExperience.ExpJobDescription = string.IsNullOrEmpty(lbl_ExpJobDescription.Text) ? null : lbl_ExpJobDescription.Text;
                            empExperience.ExpTelNo = string.IsNullOrEmpty(lbl_ExpTelNo.Text) ? null : lbl_ExpTelNo.Text;
                            empExperience.ExpRemarks = string.IsNullOrEmpty(lbl_ExpRemarks.Text) ? null : lbl_ExpRemarks.Text;
                            empExperience.IsActive = true;
                            db.tblEmpExperiences.Add(empExperience);

                        }
                    }////End Experience
                    #endregion end 6. Experience

                    #region 7. Training
                    if (gv_Training.Rows.Count > 0)
                    {
                        for (int i = 0; i < gv_Training.Rows.Count; i++)
                        {
                            HiddenField EmpTrainingId = (HiddenField)gv_Training.Rows[i].FindControl("EmpTrainingId");
                            Label lbl_TrainingName = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingName");
                            HiddenField hfTrainingType = (HiddenField)gv_Training.Rows[i].FindControl("hfTrainingType");
                            Label lbl_TrainingDescription = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingDescription");
                            HiddenField hfTrainingInstitution = (HiddenField)gv_Training.Rows[i].FindControl("hfTrainingInstitution");
                            Label lbl_TrainingPlace = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingPlace");
                            HiddenField hfTrainingCountry = (HiddenField)gv_Training.Rows[i].FindControl("hfTrainingCountry");
                            Label lbl_TrainingAchievment = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingAchievment");
                            Label lbl_TrFromDate = (Label)gv_Training.Rows[i].FindControl("lbl_TrFromDate");
                            Label lbl_TrToDate = (Label)gv_Training.Rows[i].FindControl("lbl_TrToDate");
                            Label lbl_TrainingDays = (Label)gv_Training.Rows[i].FindControl("lbl_TrainingDays");
                            Label lbl_TrRemarks = (Label)gv_Training.Rows[i].FindControl("lbl_TrRemarks");

                            tblEmpTraining empTraining = new tblEmpTraining();
                            empTraining.EmpInfoId = emp.EmpInfoId;
                            empTraining.TrainingName = string.IsNullOrEmpty(lbl_TrainingName.Text) ? null : lbl_TrainingName.Text;
                            empTraining.TrainingType = string.IsNullOrEmpty(hfTrainingType.Value) ? (int?)null : int.Parse( hfTrainingType.Value);
                            empTraining.TrainingDescription = string.IsNullOrEmpty(lbl_TrainingDescription.Text) ? null : lbl_TrainingDescription.Text;
                            empTraining.TrainingInstitution = string.IsNullOrEmpty(hfTrainingInstitution.Value) ? (int?)null : int.Parse(hfTrainingInstitution.Value);
                            empTraining.TrainingPlace = string.IsNullOrEmpty(lbl_TrainingPlace.Text) ? null : lbl_TrainingPlace.Text;
                            empTraining.TrainingCountry = string.IsNullOrEmpty(hfTrainingCountry.Value) ? (int?)null : int.Parse(hfTrainingCountry.Value);
                            empTraining.TrainingAchievment = string.IsNullOrEmpty(lbl_TrainingAchievment.Text) ? null : lbl_TrainingAchievment.Text;
                            empTraining.TrFromDate = string.IsNullOrEmpty(lbl_TrFromDate.Text) ? (DateTime?)null : DateTime.Parse(lbl_TrFromDate.Text).Date;
                            empTraining.TrToDate = string.IsNullOrEmpty(lbl_TrToDate.Text) ? (DateTime?)null : DateTime.Parse(lbl_TrToDate.Text).Date;
                            empTraining.TrainingDays = string.IsNullOrEmpty(lbl_TrainingDays.Text) ? (int?)null : int.Parse(lbl_TrainingDays.Text);
                            empTraining.TrRemarks = string.IsNullOrEmpty(lbl_TrRemarks.Text) ? null : lbl_TrRemarks.Text;
                            empTraining.IsActive = true;
                            db.tblEmpTrainings.Add(empTraining);
                        }
                    }////End Training
                    #endregion end 7. Training

                    #region 8. Reference
                    if (gv_Reference.Rows.Count > 0)
                    {
                        for (int i = 0; i < gv_Reference.Rows.Count; i++)
                        {
                            HiddenField EmpReferenceId = (HiddenField)gv_Reference.Rows[i].FindControl("EmpReferenceId");
                            Label lbl_ReferenceName = (Label)gv_Reference.Rows[i].FindControl("lbl_ReferenceName");
                            HiddenField hfRefOccupation = (HiddenField)gv_Reference.Rows[i].FindControl("hfRefOccupation");
                            Label lbl_RefAddress = (Label)gv_Reference.Rows[i].FindControl("lbl_RefAddress");
                            Label lbl_RefMobile = (Label)gv_Reference.Rows[i].FindControl("lbl_RefMobile");

                            tblEmpReference empReference = new tblEmpReference();
                            empReference.EmpInfoId = emp.EmpInfoId;
                            empReference.ReferenceName = string.IsNullOrEmpty(lbl_ReferenceName.Text) ? null : lbl_ReferenceName.Text;
                            empReference.RefOccupation = string.IsNullOrEmpty(hfRefOccupation.Value) ? (int?)null : int.Parse( hfRefOccupation.Value);
                            empReference.RefAddress = string.IsNullOrEmpty(lbl_RefAddress.Text) ? null : lbl_RefAddress.Text;
                            empReference.RefMobile = string.IsNullOrEmpty(lbl_RefMobile.Text) ? null : lbl_RefMobile.Text;
                            empReference.IsActive = true;
                            db.tblEmpReferences.Add(empReference);

                        }
                    }////End Reference
                    #endregion end 8. Reference

                    #region 9. Nominee
                    if (gv_Nominee.Rows.Count > 0)
                    {
                        for (int i = 0; i < gv_Nominee.Rows.Count; i++)
                        {
                            HiddenField EmpNomineeId = (HiddenField)gv_Nominee.Rows[i].FindControl("EmpNomineeId");
                            HiddenField hfNominationPurpose = (HiddenField)gv_Nominee.Rows[i].FindControl("hfNominationPurpose");
                            HiddenField hfNomineeRelation = (HiddenField)gv_Nominee.Rows[i].FindControl("hfNomineeRelation");
                            HiddenField hfNomineeOccupation = (HiddenField)gv_Nominee.Rows[i].FindControl("hfNomineeOccupation");
                            Label lbl_NomineeName = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeName");
                            Label lbl_DateOfNomination = (Label)gv_Nominee.Rows[i].FindControl("lbl_DateOfNomination");
                            Label lbl_NominationPercentage = (Label)gv_Nominee.Rows[i].FindControl("lbl_NominationPercentage");
                            Label lbl_NomineeDOB = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeDOB");
                            Label lbl_NomineeTelephone = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeTelephone");
                            Label lbl_NomineeAddress = (Label)gv_Nominee.Rows[i].FindControl("lbl_NomineeAddress");


                            tblEmpNominee empNominee = new tblEmpNominee();
                            empNominee.EmpInfoId = emp.EmpInfoId;
                            empNominee.NominationPurpose = string.IsNullOrEmpty(hfNominationPurpose.Value) ? (int?) null: int.Parse(hfNominationPurpose.Value);
                            empNominee.NomineeRelation = string.IsNullOrEmpty(hfNomineeRelation.Value) ? (int?)null : int.Parse(hfNomineeRelation.Value);
                            empNominee.NomineeOccupation = string.IsNullOrEmpty(hfNomineeOccupation.Value) ? (int?)null : int.Parse(hfNomineeOccupation.Value);
                            empNominee.NomineeName = string.IsNullOrEmpty(lbl_NomineeName.Text) ? null : lbl_NomineeName.Text;
                            empNominee.DateOfNomination = string.IsNullOrEmpty(lbl_DateOfNomination.Text) ? (DateTime?)null : DateTime.Parse(lbl_DateOfNomination.Text).Date;
                            empNominee.NominationPercentage = string.IsNullOrEmpty(lbl_NominationPercentage.Text) ? (decimal?)null : decimal.Parse(lbl_NominationPercentage.Text);
                            empNominee.NomineeDOB = string.IsNullOrEmpty(lbl_NomineeDOB.Text) ? (DateTime?)null : DateTime.Parse(lbl_NomineeDOB.Text).Date;

                            empNominee.NomineeTelephone = string.IsNullOrEmpty(lbl_NomineeTelephone.Text) ? null : lbl_NomineeTelephone.Text;
                            empNominee.NomineeAddress = string.IsNullOrEmpty(lbl_NomineeAddress.Text) ? null : lbl_NomineeAddress.Text;
                            empNominee.IsActive = true;
                            db.tblEmpNominees.Add(empNominee);
                        }
                    }////End Nominee
                    #endregion end 9. Nominee

                    #region 10. Others
                    foreach (ListItem item in chkExtraCurriculam.Items)
                    {
                        if (item.Selected)
                        {
                            tblEmpExtraCurriculam empExtraCurriculam = new tblEmpExtraCurriculam();
                            empExtraCurriculam.EmpInfoId = emp.EmpInfoId;
                            empExtraCurriculam.MasterExtraCurriculamId = int.Parse(item.Value);
                            empExtraCurriculam.IsActive = true;
                            db.tblEmpExtraCurriculams.Add(empExtraCurriculam);
                        }
                    }////End ExtraCurriculam
                    foreach (ListItem item in chkOtherTalents.Items)
                    {
                        if (item.Selected)
                        {
                            tblEmpOtherTalent empOtherTalent = new tblEmpOtherTalent();
                            empOtherTalent.EmpInfoId = emp.EmpInfoId;
                            empOtherTalent.MasterOtherTalentsId = int.Parse(item.Value);
                            empOtherTalent.IsActive = true;
                            db.tblEmpOtherTalents.Add(empOtherTalent);
                        }
                    }////End OtherTalents
                    foreach (ListItem item in chkAchievements.Items)
                    {
                        if (item.Selected)
                        {
                            tblEmpAchievement empAchievement = new tblEmpAchievement();
                            empAchievement.EmpInfoId = emp.EmpInfoId;
                            empAchievement.MasterAchievementsId = int.Parse(item.Value);
                            empAchievement.IsActive = true;
                            db.tblEmpAchievements.Add(empAchievement);
                        }
                    }////End Achievements
                    foreach (ListItem item in chkHobby.Items)
                    {
                        if (item.Selected)
                        {
                            tblEmpHobby empHobby = new tblEmpHobby();
                            empHobby.EmpInfoId = emp.EmpInfoId;
                            empHobby.MasterHobbyId = int.Parse(item.Value);
                            empHobby.IsActive = true;
                            db.tblEmpHobbies.Add(empHobby);
                        }
                    }////End Hobby


                    #endregion end 10. Others
                    db.SaveChanges();
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(),
                "alert",
                "alert('Operation Successful...! Employee Master Code: " + emp.EmpMasterCode + "');window.location ='EmployeeInfoEntry.aspx';",
                true);
            //empMasterCode.Text = emp.EmpMasterCode;
        }
        catch (Exception ex)
        {
            AlertMessageBoxShow(ex.Message);
        }
        //catch (DbEntityValidationException ex)
        //{
        //    foreach (var eve in ex.EntityValidationErrors)
        //    {
        //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //        foreach (var ve in eve.ValidationErrors)
        //        {
        //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
        //                ve.PropertyName, ve.ErrorMessage);
        //        }
        //    }
        //    throw;
        //}

    }

    // Keeps tblSpouse (SNo '01') in step with the single spouse fields on this page; the full list
    // is edited on UserSetup/EmpFamilyInformation.aspx. tblSpouse is not in the EF model, so parameterized SQL.
    private void UpsertFirstSpouse(HRIS_SMCEntities db, tblEmpGeneralInfo emp)
    {
        if (string.IsNullOrEmpty(emp.SpouseName))
        {
            db.Database.ExecuteSqlCommand("UPDATE dbo.tblSpouse SET IsActive=0, UpdateDate=GETDATE() WHERE EmpInfoId={0} AND SNo='01'", emp.EmpInfoId);
            return;
        }
        object dob = emp.SpouseDateOfBirth.HasValue ? (object)emp.SpouseDateOfBirth.Value : DBNull.Value;
        object edu = emp.SpouseMaxEducation.HasValue ? (object)emp.SpouseMaxEducation.Value : DBNull.Value;
        object occ = emp.SpouseOccupation.HasValue ? (object)emp.SpouseOccupation.Value : DBNull.Value;
        db.Database.ExecuteSqlCommand(@"IF EXISTS (SELECT 1 FROM dbo.tblSpouse WHERE EmpInfoId={0} AND SNo='01')
    UPDATE dbo.tblSpouse SET SName={1}, SDob={2}, SpouseMaxEducation={3}, SpouseOccupation={4}, IsActive=1, UpdateDate=GETDATE() WHERE EmpInfoId={0} AND SNo='01'
ELSE
    INSERT INTO dbo.tblSpouse (EmpInfoId, SNo, SName, SDob, SpouseMaxEducation, SpouseOccupation, IsActive, EntryDate) VALUES ({0}, '01', {1}, {2}, {3}, {4}, 1, GETDATE())",
            emp.EmpInfoId, emp.SpouseName, dob, edu, occ);
    }

    private void AlertMessageBoxShow(string message)
    {
        string sScript;
        message = message.Replace("'", "\'");
        sScript = String.Format("alert('{0}');", message);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", sScript, true);
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", message, true);

    }
    protected void cancelButton_OnClick(object sender, EventArgs e)
    {

    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex>0)
        {
            Session["cid"] = ddlCompany.SelectedValue;
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
            
            using (DataTable dt = _commonDataLoad.GetDDLJobLocation())
            {
                ddlJobLocation.DataSource = dt;
                ddlJobLocation.DataValueField = "Value";
                ddlJobLocation.DataTextField = "TextField";
                ddlJobLocation.DataBind();
            }
            using (DataTable dt = _commonDataLoad.GetDDLSalaryLocation())
            {
                ddlSalaryLocation.DataSource = dt;
                ddlSalaryLocation.DataValueField = "Value";
                ddlSalaryLocation.DataTextField = "TextField";
                ddlSalaryLocation.DataBind();
            }
            


        }
    }

    protected void btn_ImageUpload_OnClick(object sender, EventArgs e)
    {
        //if (fu_Image.HasFile)
        //{
        //    try
        //    {
        //        if (fu_Image.PostedFile.ContentLength < 1024000)
        //        {
        //            Stream fs = fu_Image.PostedFile.InputStream;
        //            BinaryReader br = new BinaryReader(fs);
        //            byte[] bytes = br.ReadBytes((Int32)fs.Length);
        //            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
        //            Image1.ImageUrl = "data:image/jpeg;base64," + base64String;
        //            //string filename = Path.GetFileName(fu_Image.FileName);
        //            //fu_Image.SaveAs(Server.MapPath("~/") + filename);
        //            //AlertMessageBoxShow("Upload status: File uploaded!");
        //        }
        //        else
        //            AlertMessageBoxShow("Upload status: The file has to be less than 1000 kb!");

        //    }
        //    catch (Exception ex)
        //    {
        //        AlertMessageBoxShow("Upload status: The file could not be uploaded. The following error occured: " + ex.Message);
        //    }
        //}

        ////second try
        //if (fu_Image.HasFile)
        //{
        //    string fileName = fu_Image.FileName;
        //    fu_Image.SaveAs(Server.MapPath("~/UploadImg/" + fileName));
        //    img.ImageUrl = "~/UploadImg/" + fileName;
        //}
    }

    protected void lb_RemoveChildren_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["ChildrenTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["ChildrenTable"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["ChildrenTable"] = dt;
                //Re bind the GridView for the updated data  
                gv_Children.DataSource = dt;
                gv_Children.DataBind();
            }
            else
            {
                ViewState["ChildrenTable"] = null;
                //Re bind the GridView for the updated data  
                gv_Children.DataSource = null;
                gv_Children.DataBind();
            }
        }
        //Set Previous Data on Postbacks  
        SetPreviousData_Children();
    }
    protected void btnAddChildren_OnClick(object sender, EventArgs e)
    {
        AddNewToGrid_Children();
    }
    private void AddNewToGrid_Children()
    {
        if (ViewState["ChildrenTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["ChildrenTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["EmpChildrenId"] = "";
                drCurrentRow["EmpInfoId"] = hdpk.Value;
                drCurrentRow["ChildrenName"] = txt_EmpChildrenName.Text;
                drCurrentRow["ChildrenGender"] = ddlEmpChildrenGender.SelectedValue;
                drCurrentRow["ChildrenOccupation"] = ddlEmpChildrenOccupation.SelectedValue;
                drCurrentRow["ChildrenOccupationName"] = ddlEmpChildrenOccupation.SelectedItem.Text;
                drCurrentRow["ChildrenDOB"] = txt_EmpChildrenDOB.Text;
                drCurrentRow["ChildrenMaritalStatus"] = ddlChildrenMaritalStatus.SelectedValue;
                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["ChildrenTable"] = dtCurrentTable;

                //Rebind the Grid with the current data to reflect changes   
                gv_Children.DataSource = dtCurrentTable;
                gv_Children.DataBind();
            }
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("EmpChildrenId", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpInfoId", typeof(string)));
            dt.Columns.Add(new DataColumn("ChildrenName", typeof(string)));
            dt.Columns.Add(new DataColumn("ChildrenGender", typeof(string)));
            dt.Columns.Add(new DataColumn("ChildrenOccupation", typeof(string)));
            dt.Columns.Add(new DataColumn("ChildrenOccupationName", typeof(string)));
            dt.Columns.Add(new DataColumn("ChildrenDOB", typeof(string)));
            dt.Columns.Add(new DataColumn("ChildrenMaritalStatus", typeof(string)));

            dr = dt.NewRow();
            dr["EmpChildrenId"] = "";
            dr["EmpInfoId"] = hdpk.Value;
            dr["ChildrenName"] = txt_EmpChildrenName.Text;
            dr["ChildrenGender"] = ddlEmpChildrenGender.SelectedValue;
            dr["ChildrenOccupation"] = ddlEmpChildrenOccupation.SelectedValue;
            dr["ChildrenOccupationName"] = ddlEmpChildrenOccupation.SelectedItem.Text;
            dr["ChildrenDOB"] = txt_EmpChildrenDOB.Text;
            dr["ChildrenMaritalStatus"] = ddlChildrenMaritalStatus.SelectedValue;
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["ChildrenTable"] = dt;

            //Bind the Gridview   
            gv_Children.DataSource = dt;
            gv_Children.DataBind();
        }
        //Set Previous Data on Postbacks   
        SetPreviousData_Children();
    }
    private void SetPreviousData_Children()
    {
        int rowIndex = 0;
        if (ViewState["ChildrenTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["ChildrenTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField EmpChildrenId = (HiddenField)gv_Children.Rows[rowIndex].FindControl("EmpChildrenId");
                    Label lbl_ChildrenName = (Label)gv_Children.Rows[rowIndex].FindControl("lbl_ChildrenName");
                    Label lbl_ChildrenGender = (Label)gv_Children.Rows[rowIndex].FindControl("lbl_ChildrenGender");
                    Label lbl_ChildrenOccupation = (Label)gv_Children.Rows[rowIndex].FindControl("lbl_ChildrenOccupation");
                    HiddenField hfChildrenOccupation = (HiddenField)gv_Children.Rows[rowIndex].FindControl("hfChildrenOccupation");
                    Label lbl_ChildrenDOB = (Label)gv_Children.Rows[rowIndex].FindControl("lbl_ChildrenDOB");
                    Label lbl_ChildrenMaritalStatus = (Label)gv_Children.Rows[rowIndex].FindControl("lbl_ChildrenMaritalStatus");

                    if (i < dt.Rows.Count - 1)
                    {
                        EmpChildrenId.Value = dt.Rows[i]["EmpChildrenId"].ToString();
                        lbl_ChildrenName.Text = dt.Rows[i]["ChildrenName"].ToString();
                        lbl_ChildrenGender.Text = dt.Rows[i]["ChildrenGender"].ToString();
                        lbl_ChildrenOccupation.Text = dt.Rows[i]["ChildrenOccupationName"].ToString();
                        hfChildrenOccupation.Value = dt.Rows[i]["ChildrenOccupation"].ToString();
                        lbl_ChildrenDOB.Text = dt.Rows[i]["ChildrenDOB"].ToString();
                        lbl_ChildrenMaritalStatus.Text = dt.Rows[i]["ChildrenMaritalStatus"].ToString();
                    }

                    rowIndex++;
                }
            }
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
    protected void btnAddEducation_OnClick(object sender, EventArgs e)
    {
        AddNewToGrid_Education();
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

                drCurrentRow["EmpEducationId"] = "";
                drCurrentRow["EmpInfoId"] = hdpk.Value;
                drCurrentRow["EducationNameId"] = ddlEducationName.SelectedValue;
                drCurrentRow["EducationName"] = ddlEducationName.SelectedItem.Text;
                drCurrentRow["BoardUniversityId"] = ddlBoardUniversity.SelectedValue;
                drCurrentRow["BoardUniversity"] = ddlBoardUniversity.SelectedItem.Text;
                drCurrentRow["SubjectGroupId"] = ddlSubjectGroup.SelectedValue;
                drCurrentRow["SubjectGroup"] = ddlSubjectGroup.SelectedItem.Text;
                drCurrentRow["EducationalInstituteId"] = ddlEducationalInstitute.SelectedValue;
                drCurrentRow["EducationalInstitute"] = ddlEducationalInstitute.SelectedItem.Text;
                drCurrentRow["FieldOfSpecializationId"] = ddlSpecialization.SelectedValue;
                drCurrentRow["FieldOfSpecialization"] = ddlSpecialization.SelectedItem.Text;
                drCurrentRow["PassingYear"] = txt_PassingYear.Text;
                drCurrentRow["Result"] = txt_Result.Text;
                drCurrentRow["CgpaOrTotalMarks"] = txt_CGPAMarks.Text;
                drCurrentRow["EduIsLastLevel"] = chk_EduIsLastLevel.Checked.ToString();
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

            dr = dt.NewRow();
            dr["EmpEducationId"] = "";
            dr["EmpInfoId"] = hdpk.Value;
            dr["EducationNameId"] = ddlEducationName.SelectedValue;
            dr["EducationName"] = ddlEducationName.SelectedItem.Text;
            dr["BoardUniversityId"] = ddlBoardUniversity.SelectedValue;
            dr["BoardUniversity"] = ddlBoardUniversity.SelectedItem.Text;
            dr["SubjectGroupId"] = ddlSubjectGroup.SelectedValue;
            dr["SubjectGroup"] = ddlSubjectGroup.SelectedItem.Text;
            dr["EducationalInstituteId"] = ddlEducationalInstitute.SelectedValue;
            dr["EducationalInstitute"] = ddlEducationalInstitute.SelectedItem.Text;
            dr["FieldOfSpecializationId"] = ddlSpecialization.SelectedValue;
            dr["FieldOfSpecialization"] = ddlSpecialization.SelectedItem.Text;
            dr["PassingYear"] = txt_PassingYear.Text;
            dr["Result"] = txt_Result.Text;
            dr["CgpaOrTotalMarks"] = txt_CGPAMarks.Text;
            dr["EduIsLastLevel"] = chk_EduIsLastLevel.Checked.ToString();
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["EducationTable"] = dt;

            //Bind the Gridview   
            gv_Education.DataSource = dt;
            gv_Education.DataBind();
        }
        //Set Previous Data on Postbacks   
        SetPreviousData_Education();
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
                        chk_EduIsLastLevel.Checked = EduIsLastLevel;
                    }

                    rowIndex++;
                }
            }
        }
    }


    protected void lb_RemoveExperience_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["ExperienceTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["ExperienceTable"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["ExperienceTable"] = dt;
                //Re bind the GridView for the updated data  
                gv_Experience.DataSource = dt;
                gv_Experience.DataBind();
            }
            else
            {
                ViewState["ExperienceTable"] = null;
                //Re bind the GridView for the updated data  
                gv_Experience.DataSource = null;
                gv_Experience.DataBind();
            }
        }
        //Set Previous Data on Postbacks  
        SetPreviousData_Experience();
    }
    protected void btnAddExperience_OnClick(object sender, EventArgs e)
    {
        AddNewToGrid_Experience();
    }
    private void AddNewToGrid_Experience()
    {
        if (ViewState["ExperienceTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["ExperienceTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["EmpExperienceId"] = "";
                drCurrentRow["EmpInfoId"] = hdpk.Value;
                drCurrentRow["ExpCompany"] = txt_ExpCompany.Text;
                drCurrentRow["ExpContactPerson"] = txt_ExpContactPerson.Text;
                drCurrentRow["ExpAddress"] = txt_ExpAddress.Text;
                drCurrentRow["ExpNatureofBusiness"] = txt_ExpNatureofBusiness.Text;
                drCurrentRow["ExpJobType"] = txt_ExpJobType.Text;
                drCurrentRow["ExpFromDate"] = txt_ExpFromDate.Text;
                drCurrentRow["ExpToDate"] = txt_ExpToDate.Text;
                drCurrentRow["ExpLastJob"] = chk_ExpLastJob.Checked;
                drCurrentRow["ExpDesignation"] = txt_ExpDesignation.Text;
                drCurrentRow["ExpJobDescription"] = txt_ExpJobDescription.Text;
                drCurrentRow["ExpTelNo"] = txt_ExpTelNo.Text;
                drCurrentRow["ExpRemarks"] = txt_ExpRemarks.Text;
                drCurrentRow["ExpLeavingSalary"] = txt_ExpLeavingSalary.Text;
                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["ExperienceTable"] = dtCurrentTable;

                //Rebind the Grid with the current data to reflect changes   
                gv_Experience.DataSource = dtCurrentTable;
                gv_Experience.DataBind();
            }
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("EmpExperienceId", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpInfoId", typeof(string)));
            dt.Columns.Add(new DataColumn("ExpCompany", typeof(string)));
            dt.Columns.Add(new DataColumn("ExpContactPerson", typeof(string)));
            dt.Columns.Add(new DataColumn("ExpAddress", typeof(string)));
            dt.Columns.Add(new DataColumn("ExpNatureofBusiness", typeof(string)));
            dt.Columns.Add(new DataColumn("ExpJobType", typeof(string)));
            dt.Columns.Add(new DataColumn("ExpFromDate", typeof(string)));
            dt.Columns.Add(new DataColumn("ExpToDate", typeof(string)));
            dt.Columns.Add(new DataColumn("ExpLastJob", typeof(string)));
            dt.Columns.Add(new DataColumn("ExpDesignation", typeof(string)));
            dt.Columns.Add(new DataColumn("ExpJobDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("ExpTelNo", typeof(string)));
            dt.Columns.Add(new DataColumn("ExpRemarks", typeof(string)));
            dt.Columns.Add(new DataColumn("ExpLeavingSalary", typeof(string)));

            dr = dt.NewRow();
            dr["EmpExperienceId"] = "";
            dr["EmpInfoId"] = hdpk.Value;
            dr["ExpCompany"] = txt_ExpCompany.Text;
            dr["ExpContactPerson"] = txt_ExpContactPerson.Text;
            dr["ExpAddress"] = txt_ExpAddress.Text;
            dr["ExpNatureofBusiness"] = txt_ExpNatureofBusiness.Text;
            dr["ExpJobType"] = txt_ExpJobType.Text;
            dr["ExpFromDate"] = txt_ExpFromDate.Text;
            dr["ExpToDate"] = txt_ExpToDate.Text;
            dr["ExpLastJob"] = chk_ExpLastJob.Checked;
            dr["ExpDesignation"] = txt_ExpDesignation.Text;
            dr["ExpJobDescription"] = txt_ExpJobDescription.Text;
            dr["ExpTelNo"] = txt_ExpTelNo.Text;
            dr["ExpRemarks"] = txt_ExpRemarks.Text;
            dr["ExpLeavingSalary"] = txt_ExpLeavingSalary.Text;
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["ExperienceTable"] = dt;

            //Bind the Gridview   
            gv_Experience.DataSource = dt;
            gv_Experience.DataBind();
        }
        //Set Previous Data on Postbacks   
        SetPreviousData_Experience();
    }
    private void SetPreviousData_Experience()
    {
        int rowIndex = 0;
        if (ViewState["ExperienceTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["ExperienceTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField EmpExperienceId = (HiddenField)gv_Experience.Rows[rowIndex].FindControl("EmpExperienceId");
                    Label lbl_ExpCompany = (Label)gv_Experience.Rows[rowIndex].FindControl("lbl_ExpCompany");
                    Label lbl_ExpContactPerson = (Label)gv_Experience.Rows[rowIndex].FindControl("lbl_ExpContactPerson");
                    Label lbl_ExpAddress = (Label)gv_Experience.Rows[rowIndex].FindControl("lbl_ExpAddress");
                    Label lbl_ExpNatureofBusiness = (Label)gv_Experience.Rows[rowIndex].FindControl("lbl_ExpNatureofBusiness");
                    Label lbl_ExpJobType = (Label)gv_Experience.Rows[rowIndex].FindControl("lbl_ExpJobType");
                    Label lbl_ExpFromDate = (Label)gv_Experience.Rows[rowIndex].FindControl("lbl_ExpFromDate");
                    Label lbl_ExpToDate = (Label)gv_Experience.Rows[rowIndex].FindControl("lbl_ExpToDate");
                    Label lbl_ExpLastJob = (Label)gv_Experience.Rows[rowIndex].FindControl("lbl_ExpLastJob");
                    Label lbl_ExpDesignation = (Label)gv_Experience.Rows[rowIndex].FindControl("lbl_ExpDesignation");
                    Label lbl_ExpJobDescription = (Label)gv_Experience.Rows[rowIndex].FindControl("lbl_ExpJobDescription");
                    Label lbl_ExpTelNo = (Label)gv_Experience.Rows[rowIndex].FindControl("lbl_ExpTelNo");
                    Label lbl_ExpRemarks = (Label)gv_Experience.Rows[rowIndex].FindControl("lbl_ExpRemarks");
                    Label lbl_ExpLeavingSalary = (Label)gv_Experience.Rows[rowIndex].FindControl("lbl_ExpLeavingSalary");

                    if (i < dt.Rows.Count - 1)
                    {
                        //EmpExperienceId.Value = dt.Rows[i]["EmpExperienceId"].ToString();
                        lbl_ExpCompany.Text = dt.Rows[i]["ExpCompany"].ToString();
                        lbl_ExpContactPerson.Text = dt.Rows[i]["ExpContactPerson"].ToString();
                        lbl_ExpAddress.Text = dt.Rows[i]["ExpAddress"].ToString();
                        lbl_ExpNatureofBusiness.Text = dt.Rows[i]["ExpNatureofBusiness"].ToString();
                        lbl_ExpJobType.Text = dt.Rows[i]["ExpJobType"].ToString();
                        lbl_ExpFromDate.Text = dt.Rows[i]["ExpFromDate"].ToString();
                        lbl_ExpToDate.Text = dt.Rows[i]["ExpToDate"].ToString();
                        lbl_ExpLastJob.Text = dt.Rows[i]["ExpLastJob"].ToString();
                        lbl_ExpDesignation.Text = dt.Rows[i]["ExpDesignation"].ToString();
                        lbl_ExpJobDescription.Text = dt.Rows[i]["ExpJobDescription"].ToString();
                        lbl_ExpTelNo.Text = dt.Rows[i]["ExpTelNo"].ToString();
                        lbl_ExpRemarks.Text = dt.Rows[i]["ExpRemarks"].ToString();
                        lbl_ExpLeavingSalary.Text = dt.Rows[i]["ExpLeavingSalary"].ToString();
                    }

                    rowIndex++;
                }
            }
        }
    }


    protected void lb_RemoveTraining_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["TrainingTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["TrainingTable"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["TrainingTable"] = dt;
                //Re bind the GridView for the updated data  
                gv_Training.DataSource = dt;
                gv_Training.DataBind();
            }
            else
            {
                ViewState["TrainingTable"] = null;
                //Re bind the GridView for the updated data  
                gv_Training.DataSource = null;
                gv_Training.DataBind();
            }
        }
        //Set Previous Data on Postbacks  
        SetPreviousData_Training();
    }
    protected void btnAddTraining_OnClick(object sender, EventArgs e)
    {
        AddNewToGrid_Training();
    }
    private void AddNewToGrid_Training()
    {
        if (ViewState["TrainingTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["TrainingTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["EmpTrainingId"] = "";
                drCurrentRow["EmpInfoId"] = hdpk.Value;
                drCurrentRow["TrainingName"] = txt_TrTrainingName.Text;

                drCurrentRow["TrainingTypeName"] = ddlTrTrainingType.SelectedItem.Text;
                drCurrentRow["TrainingType"] = ddlTrTrainingType.SelectedValue;
                drCurrentRow["TrainingInstitutionName"] = ddlTrTrainingInstitution.SelectedItem.Text;
                drCurrentRow["TrainingInstitution"] = ddlTrTrainingInstitution.SelectedValue;
                drCurrentRow["TrainingCountryName"] = ddlTrTrainingCountry.SelectedItem.Text;
                drCurrentRow["TrainingCountry"] = ddlTrTrainingCountry.SelectedValue;

                drCurrentRow["TrainingDescription"] = txt_TrTrainingDescription.Text;
                drCurrentRow["TrainingPlace"] = txt_TrTrainingPlace.Text;
                drCurrentRow["TrainingAchievment"] = txt_TrTrainingAchievment.Text;
                drCurrentRow["TrFromDate"] = txt_TrFromDate.Text;
                drCurrentRow["TrToDate"] = txt_TrToDate.Text;
                drCurrentRow["TrainingDays"] = txt_TrTrainingDays.Text;
                drCurrentRow["TrRemarks"] = txt_TrRemarks.Text;

                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["TrainingTable"] = dtCurrentTable;

                //Rebind the Grid with the current data to reflect changes   
                gv_Training.DataSource = dtCurrentTable;
                gv_Training.DataBind();
            }
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("EmpTrainingId", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpInfoId", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingName", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingType", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingTypeName", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingDescription", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingInstitution", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingInstitutionName", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingPlace", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingCountry", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingCountryName", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingAchievment", typeof(string)));
            dt.Columns.Add(new DataColumn("TrFromDate", typeof(string)));
            dt.Columns.Add(new DataColumn("TrToDate", typeof(string)));
            dt.Columns.Add(new DataColumn("TrainingDays", typeof(string)));
            dt.Columns.Add(new DataColumn("TrRemarks", typeof(string)));

            dr = dt.NewRow();
            dr["EmpTrainingId"] = "";
            dr["EmpInfoId"] = hdpk.Value;
            dr["TrainingName"] = txt_TrTrainingName.Text;

            dr["TrainingTypeName"] = ddlTrTrainingType.SelectedItem.Text;
            dr["TrainingType"] = ddlTrTrainingType.SelectedValue;
            dr["TrainingInstitutionName"] = ddlTrTrainingInstitution.SelectedItem.Text;
            dr["TrainingInstitution"] = ddlTrTrainingInstitution.SelectedValue;
            dr["TrainingCountryName"] = ddlTrTrainingCountry.SelectedItem.Text;
            dr["TrainingCountry"] = ddlTrTrainingCountry.SelectedValue;

            
            dr["TrainingDescription"] = txt_TrTrainingDescription.Text;
            dr["TrainingPlace"] = txt_TrTrainingPlace.Text;
            dr["TrainingAchievment"] = txt_TrTrainingAchievment.Text;
            dr["TrFromDate"] = txt_TrFromDate.Text;
            dr["TrToDate"] = txt_TrToDate.Text;
            dr["TrainingDays"] = txt_TrTrainingDays.Text;
            dr["TrRemarks"] = txt_TrRemarks.Text;
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["TrainingTable"] = dt;

            //Bind the Gridview   
            gv_Training.DataSource = dt;
            gv_Training.DataBind();
        }
        //Set Previous Data on Postbacks   
        SetPreviousData_Training();
    }
    private void SetPreviousData_Training()
    {
        int rowIndex = 0;
        if (ViewState["TrainingTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["TrainingTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField EmpTrainingId = (HiddenField)gv_Training.Rows[rowIndex].FindControl("EmpTrainingId");
                    Label lbl_TrainingName = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrainingName");

                    Label lbl_TrainingType = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrainingType");
                    HiddenField hfTrainingType = (HiddenField)gv_Training.Rows[rowIndex].FindControl("hfTrainingType");
                    Label lbl_TrainingInstitution = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrainingInstitution");
                    HiddenField hfTrainingInstitution = (HiddenField)gv_Training.Rows[rowIndex].FindControl("hfTrainingInstitution");
                    Label lbl_TrainingCountry = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrainingCountry");
                    HiddenField hfTrainingCountry = (HiddenField)gv_Training.Rows[rowIndex].FindControl("hfTrainingCountry");

                    Label lbl_TrainingDescription = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrainingDescription");
                    Label lbl_TrainingPlace = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrainingPlace");
                    Label lbl_TrainingAchievment = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrainingAchievment");
                    Label lbl_TrFromDate = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrFromDate");
                    Label lbl_TrToDate = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrToDate");
                    Label lbl_TrainingDays = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrainingDays");
                    Label lbl_TrRemarks = (Label)gv_Training.Rows[rowIndex].FindControl("lbl_TrRemarks");

                    if (i < dt.Rows.Count - 1)
                    {
                        EmpTrainingId.Value = dt.Rows[i]["EmpTrainingId"].ToString();
                        lbl_TrainingName.Text = dt.Rows[i]["TrainingName"].ToString();

                        lbl_TrainingType.Text = dt.Rows[i]["TrainingTypeName"].ToString();
                        lbl_TrainingInstitution.Text = dt.Rows[i]["TrainingInstitutionName"].ToString();
                        lbl_TrainingCountry.Text = dt.Rows[i]["TrainingCountryName"].ToString();

                        hfTrainingType.Value = dt.Rows[i]["TrainingType"].ToString();
                        hfTrainingInstitution.Value = dt.Rows[i]["TrainingInstitution"].ToString();
                        hfTrainingCountry.Value = dt.Rows[i]["TrainingCountry"].ToString();

                        lbl_TrainingDescription.Text = dt.Rows[i]["TrainingDescription"].ToString();
                        lbl_TrainingPlace.Text = dt.Rows[i]["TrainingPlace"].ToString();
                        lbl_TrainingAchievment.Text = dt.Rows[i]["TrainingAchievment"].ToString();
                        lbl_TrFromDate.Text = dt.Rows[i]["TrFromDate"].ToString();
                        lbl_TrToDate.Text = dt.Rows[i]["TrToDate"].ToString();
                        lbl_TrainingDays.Text = dt.Rows[i]["TrainingDays"].ToString();
                        lbl_TrRemarks.Text = dt.Rows[i]["TrRemarks"].ToString();
                    }

                    rowIndex++;
                }
            }
        }
    }

    protected void lb_RemoveReference_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["ReferenceTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["ReferenceTable"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["ReferenceTable"] = dt;
                //Re bind the GridView for the updated data  
                gv_Reference.DataSource = dt;
                gv_Reference.DataBind();
            }
            else
            {
                ViewState["ReferenceTable"] = null;
                //Re bind the GridView for the updated data  
                gv_Reference.DataSource = null;
                gv_Reference.DataBind();
            }
        }
        //Set Previous Data on Postbacks  
        SetPreviousData_Reference();
    }
    protected void btnAddReference_OnClick(object sender, EventArgs e)
    {
        AddNewToGrid_Reference();
    }
    private void AddNewToGrid_Reference()
    {
        if (ViewState["ReferenceTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["ReferenceTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["EmpReferenceId"] = "";
                drCurrentRow["EmpInfoId"] = hdpk.Value;
                drCurrentRow["ReferenceName"] = txt_RefReferenceName.Text;
                drCurrentRow["RefOccupation"] = ddlRefOccupation.SelectedValue;
                drCurrentRow["RefOccupationName"] = ddlRefOccupation.SelectedItem.Text;
                drCurrentRow["RefAddress"] = txt_RefAddress.Text;
                drCurrentRow["RefMobile"] = txt_RefMobile.Text;
                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["ReferenceTable"] = dtCurrentTable;

                //Rebind the Grid with the current data to reflect changes   
                gv_Reference.DataSource = dtCurrentTable;
                gv_Reference.DataBind();
            }
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("EmpReferenceId", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpInfoId", typeof(string)));
            dt.Columns.Add(new DataColumn("ReferenceName", typeof(string)));
            dt.Columns.Add(new DataColumn("RefOccupation", typeof(string)));
            dt.Columns.Add(new DataColumn("RefOccupationName", typeof(string)));
            dt.Columns.Add(new DataColumn("RefAddress", typeof(string)));
            dt.Columns.Add(new DataColumn("RefMobile", typeof(string)));

            dr = dt.NewRow();
            dr["EmpReferenceId"] = "";
            dr["EmpInfoId"] = hdpk.Value;
            dr["ReferenceName"] = txt_RefReferenceName.Text;
            dr["RefOccupation"] = ddlRefOccupation.SelectedValue;
            dr["RefOccupationName"] = ddlRefOccupation.SelectedItem.Text;
            dr["RefAddress"] = txt_RefAddress.Text;
            dr["RefMobile"] = txt_RefMobile.Text;
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["ReferenceTable"] = dt;

            //Bind the Gridview   
            gv_Reference.DataSource = dt;
            gv_Reference.DataBind();
        }
        //Set Previous Data on Postbacks   
        SetPreviousData_Reference();
    }
    private void SetPreviousData_Reference()
    {
        int rowIndex = 0;
        if (ViewState["ReferenceTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["ReferenceTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField EmpReferenceId = (HiddenField)gv_Reference.Rows[rowIndex].FindControl("EmpReferenceId");
                    Label lbl_ReferenceName = (Label)gv_Reference.Rows[rowIndex].FindControl("lbl_ReferenceName");
                    HiddenField hfRefOccupation = (HiddenField)gv_Reference.Rows[rowIndex].FindControl("hfRefOccupation");
                    Label lbl_RefOccupation = (Label)gv_Reference.Rows[rowIndex].FindControl("lbl_RefOccupation");
                    Label lbl_RefAddress = (Label)gv_Reference.Rows[rowIndex].FindControl("lbl_RefAddress");
                    Label lbl_RefMobile = (Label)gv_Reference.Rows[rowIndex].FindControl("lbl_RefMobile");

                    if (i < dt.Rows.Count - 1)
                    {
                        EmpReferenceId.Value = dt.Rows[i]["EmpReferenceId"].ToString();
                        lbl_ReferenceName.Text = dt.Rows[i]["ReferenceName"].ToString();
                        hfRefOccupation.Value = dt.Rows[i]["RefOccupation"].ToString();
                        lbl_RefOccupation.Text = dt.Rows[i]["RefOccupationName"].ToString();
                        lbl_RefAddress.Text = dt.Rows[i]["RefAddress"].ToString();
                        lbl_RefMobile.Text = dt.Rows[i]["RefMobile"].ToString();
                    }

                    rowIndex++;
                }
            }
        }
    }

    protected void lb_RemoveNominee_OnClick(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
        int rowID = gvRow.RowIndex;
        if (ViewState["NomineeTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["NomineeTable"];
            dt.Rows.Remove(dt.Rows[rowID]);
            if (dt.Rows.Count > 0)
            {
                //Store the current data in ViewState for future reference  
                ViewState["NomineeTable"] = dt;
                //Re bind the GridView for the updated data  
                gv_Nominee.DataSource = dt;
                gv_Nominee.DataBind();
            }
            else
            {
                ViewState["NomineeTable"] = null;
                //Re bind the GridView for the updated data  
                gv_Nominee.DataSource = null;
                gv_Nominee.DataBind();
            }
        }
        //Set Previous Data on Postbacks  
        SetPreviousData_Nominee();
    }
    protected void btnAddNominee_OnClick(object sender, EventArgs e)
    {
        AddNewToGrid_Nominee();
    }
    private void AddNewToGrid_Nominee()
    {
        if (ViewState["NomineeTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["NomineeTable"];
            DataRow drCurrentRow = null;

            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();

                drCurrentRow["EmpNomineeId"] = "";
                drCurrentRow["EmpInfoId"] = hdpk.Value;
                drCurrentRow["NominationPurpose"] = ddlNomNominationPurpose.SelectedValue;
                drCurrentRow["NominationPurposeName"] = ddlNomNominationPurpose.SelectedItem.Text;
                drCurrentRow["NomineeOccupation"] = ddlNomNomineeOccupation.SelectedValue;
                drCurrentRow["NomineeOccupationName"] = ddlNomNomineeOccupation.SelectedItem.Text;
                drCurrentRow["NomineeRelation"] = ddlNomNomineeRelation.SelectedValue;
                drCurrentRow["NomineeRelationName"] = ddlNomNomineeRelation.SelectedItem.Text;
                drCurrentRow["NomineeName"] = txt_NomNomineeName.Text;
                drCurrentRow["DateOfNomination"] = txt_NomDateOfNomination.Text;
                drCurrentRow["NominationPercentage"] = txt_NomNominationPercentage.Text;
                drCurrentRow["NomineeDOB"] = txt_NomNomineeDOB.Text;
                drCurrentRow["NomineeTelephone"] = txt_NomNomineeTelephone.Text;
                drCurrentRow["NomineeAddress"] = txt_NomNomineeAddress.Text;
                //add new row to DataTable   
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState for future reference   
                ViewState["NomineeTable"] = dtCurrentTable;

                //Rebind the Grid with the current data to reflect changes   
                gv_Nominee.DataSource = dtCurrentTable;
                gv_Nominee.DataBind();
            }
        }
        else
        {
            DataTable dt = new DataTable();
            DataRow dr = null;

            dt.Columns.Add(new DataColumn("EmpNomineeId", typeof(string)));
            dt.Columns.Add(new DataColumn("EmpInfoId", typeof(string)));
            dt.Columns.Add(new DataColumn("NominationPurpose", typeof(string)));
            dt.Columns.Add(new DataColumn("NominationPurposeName", typeof(string)));
            dt.Columns.Add(new DataColumn("NomineeOccupation", typeof(string)));
            dt.Columns.Add(new DataColumn("NomineeOccupationName", typeof(string)));
            dt.Columns.Add(new DataColumn("NomineeRelation", typeof(string)));
            dt.Columns.Add(new DataColumn("NomineeRelationName", typeof(string)));
            dt.Columns.Add(new DataColumn("NomineeName", typeof(string)));
            dt.Columns.Add(new DataColumn("DateOfNomination", typeof(string)));
            dt.Columns.Add(new DataColumn("NominationPercentage", typeof(string)));
            dt.Columns.Add(new DataColumn("NomineeDOB", typeof(string)));
            dt.Columns.Add(new DataColumn("NomineeTelephone", typeof(string)));
            dt.Columns.Add(new DataColumn("NomineeAddress", typeof(string)));

            dr = dt.NewRow();
            dr["EmpNomineeId"] = "";
            dr["EmpInfoId"] = hdpk.Value;
            dr["NominationPurpose"] = ddlNomNominationPurpose.SelectedValue;
            dr["NominationPurposeName"] = ddlNomNominationPurpose.SelectedItem.Text;
            dr["NomineeOccupation"] = ddlNomNomineeOccupation.SelectedValue;
            dr["NomineeOccupationName"] = ddlNomNomineeOccupation.SelectedItem.Text;
            dr["NomineeRelation"] = ddlNomNomineeRelation.SelectedValue;
            dr["NomineeRelationName"] = ddlNomNomineeRelation.SelectedItem.Text;
            dr["NomineeName"] = txt_NomNomineeName.Text;
            dr["DateOfNomination"] = txt_NomDateOfNomination.Text;
            dr["NominationPercentage"] = txt_NomNominationPercentage.Text;
            dr["NomineeDOB"] = txt_NomNomineeDOB.Text;
            dr["NomineeTelephone"] = txt_NomNomineeTelephone.Text;
            dr["NomineeAddress"] = txt_NomNomineeAddress.Text;
            dt.Rows.Add(dr);

            //Store the DataTable in ViewState for future reference   
            ViewState["NomineeTable"] = dt;

            //Bind the Gridview   
            gv_Nominee.DataSource = dt;
            gv_Nominee.DataBind();
        }
        //Set Previous Data on Postbacks   
        SetPreviousData_Nominee();
    }
    private void SetPreviousData_Nominee()
    {
        int rowIndex = 0;
        if (ViewState["NomineeTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["NomineeTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HiddenField EmpNomineeId = (HiddenField)gv_Nominee.Rows[rowIndex].FindControl("EmpNomineeId");
                    Label lbl_NominationPurpose = (Label)gv_Nominee.Rows[rowIndex].FindControl("lbl_NominationPurpose");
                    HiddenField hfNominationPurpose = (HiddenField)gv_Nominee.Rows[rowIndex].FindControl("hfNominationPurpose");
                    Label lbl_NomineeRelation = (Label)gv_Nominee.Rows[rowIndex].FindControl("lbl_NomineeRelation");
                    HiddenField hfNomineeRelation = (HiddenField)gv_Nominee.Rows[rowIndex].FindControl("hfNomineeRelation");
                    Label lbl_NomineeOccupation = (Label)gv_Nominee.Rows[rowIndex].FindControl("lbl_NomineeOccupation");
                    HiddenField hfNomineeOccupation = (HiddenField)gv_Nominee.Rows[rowIndex].FindControl("hfNomineeOccupation");
                    Label lbl_NomineeName = (Label)gv_Nominee.Rows[rowIndex].FindControl("lbl_NomineeName");
                    Label lbl_DateOfNomination = (Label)gv_Nominee.Rows[rowIndex].FindControl("lbl_DateOfNomination");
                    Label lbl_NominationPercentage = (Label)gv_Nominee.Rows[rowIndex].FindControl("lbl_NominationPercentage");
                    Label lbl_NomineeDOB = (Label)gv_Nominee.Rows[rowIndex].FindControl("lbl_NomineeDOB");

                    Label lbl_NomineeTelephone = (Label)gv_Nominee.Rows[rowIndex].FindControl("lbl_NomineeTelephone");
                    Label lbl_NomineeAddress = (Label)gv_Nominee.Rows[rowIndex].FindControl("lbl_NomineeAddress");

                    if (i < dt.Rows.Count - 1)
                    {
                        EmpNomineeId.Value = dt.Rows[i]["EmpNomineeId"].ToString();
                        lbl_NominationPurpose.Text = dt.Rows[i]["NominationPurposeName"].ToString();
                        hfNominationPurpose.Value = dt.Rows[i]["NominationPurpose"].ToString();
                        lbl_NomineeRelation.Text = dt.Rows[i]["NomineeRelationName"].ToString();
                        hfNomineeRelation.Value = dt.Rows[i]["NomineeRelation"].ToString();
                        lbl_NomineeOccupation.Text = dt.Rows[i]["NomineeOccupationName"].ToString();
                        hfNomineeOccupation.Value = dt.Rows[i]["NomineeOccupation"].ToString();
                        lbl_NomineeName.Text = dt.Rows[i]["NomineeName"].ToString();
                        lbl_DateOfNomination.Text = dt.Rows[i]["DateOfNomination"].ToString();
                        lbl_NominationPercentage.Text = dt.Rows[i]["NominationPercentage"].ToString();
                        lbl_NomineeDOB.Text = dt.Rows[i]["NomineeDOB"].ToString();
                        lbl_NomineeTelephone.Text = dt.Rows[i]["NomineeTelephone"].ToString();
                        lbl_NomineeAddress.Text = dt.Rows[i]["NomineeAddress"].ToString();
                    }

                    rowIndex++;
                }
            }
        }
    }


    //protected void ajaxUpload1_OnUploadComplete(object sender, AjaxFileUploadEventArgs e)
    //{
    //    //using (var stream = e.GetStreamContents())
    //    //{
    //    //    BinaryReader br = new BinaryReader(stream);
    //    //    byte[] bytes = br.ReadBytes((Int32)stream.Length);
    //    //    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
    //    //    Image1.ImageUrl = "data:image/jpeg;base64," + base64String;
    //    //}

    //    string fileNametoupload = Server.MapPath("~/UploadImg/") + e.FileName.ToString();
    //    //Directory.CreateDirectory(fileNametoupload);
    //    ////ajaxUpload1.SaveAs(fileNametoupload);
    //    //Image1.ImageUrl = "~/UploadImg/" + e.FileName;
    //}

    protected void txt_ReportingBoss_OnTextChanged(object sender, EventArgs e)
    {
        string Emp = txt_ReportingBoss.Text;
        if (!string.IsNullOrEmpty(Emp))
        {
            hdReportingBoss.Value = Emp.Split('>')[0];
            txt_ReportingBoss.Text = Emp.Split('>')[1];
            txt_ReportingBossDesig.Text = Emp.Split('>')[2];
        }
    }

//    protected void btnProcessData_Click(object sender, EventArgs e)
//    {
        
//    }

    protected void txt_EmpExpectedServiceLength_OnTextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txt_EmpExpectedServiceLength.Text))
        {
            int esl = int.Parse(txt_EmpExpectedServiceLength.Text);
            DateTime doj ;
            if (DateTime.TryParse(txt_EmpDOJ.Text,out doj))
            {
                DateTime dor = doj.AddYears(esl);
                txt_EmpDateOfRetirement.Text = dor.ToString("dd-MMM-yyyy");
            }
        }
    }

    protected void chkIsProbationary_OnCheckedChanged(object sender, EventArgs e)
    {
        
            txt_ProbationaryEndDate.ReadOnly = !chkIsProbationary.Checked;
        
    }

    protected void ddlEmpPresentDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpPresentDivision.SelectedIndex>0)
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

    protected void ddlEmpPresentDist_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpPresentDist.SelectedIndex>0)
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

    protected void ddlEmpParmanentDivision_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpParmanentDivision.SelectedIndex>0)
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
        if (ddlEmpParmanentDistrict.SelectedIndex>0)
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

    protected void ddlEmpCategory_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpCategory.SelectedIndex>0)
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
        if (ddlSalaryGrade.SelectedIndex>0)
        {
            using (DataTable dt = _commonDataLoad.GetDDLSalaryStep(ddlSalaryGrade.SelectedValue))
            {
                ddlSalaryStep.DataSource = dt;
                ddlSalaryStep.DataValueField = "Value";
                ddlSalaryStep.DataTextField = "TextField";
                ddlSalaryStep.DataBind();
            }

           
        }
    }

    protected void ddlEmpType_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEmpType.SelectedIndex>0)
        {
            if (ddlEmpType.SelectedValue == "3")
            {
                using (DataTable dt = _commonDataLoad.GetDDLSalFromProject(ddlCompany.SelectedValue))
                {
                    ddlSalFromProject.DataSource = dt;
                    ddlSalFromProject.DataValueField = "Value";
                    ddlSalFromProject.DataTextField = "TextField";
                    ddlSalFromProject.DataBind();
                }
                using (DataTable dt = _commonDataLoad.GetAllProject(ddlCompany.SelectedValue))
                {
                    cbl_ContractProject.DataSource = dt;
                    cbl_ContractProject.DataValueField = "Value";
                    cbl_ContractProject.DataTextField = "TextField";
                    cbl_ContractProject.DataBind();
                }
            }
            iddivContract.Visible = ddlEmpType.SelectedValue == "3";

            
        }
    }
}