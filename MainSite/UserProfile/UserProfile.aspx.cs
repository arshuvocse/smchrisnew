using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL.COMMON_DAL;
using DAL.UserPermissions_DAL;

public partial class UserProfile_UserProfile : System.Web.UI.Page
{


    UserProfileDAL aUserProfileDAL = new UserProfileDAL();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            if (Session["EmpInfoId"] != null && Session["EmpInfoId"] != "")
            {
               
                GetOneRecord(Session["EmpInfoId"].ToString());
                
            }
        }
    }

    private void GetOneRecord(string id)
    {

        DataTable dataTable = aUserProfileDAL.GetEmployeeInfoDAL(id);

        const int rowIndex = 0;

        if (dataTable.Rows.Count > 0)
        {
            if (dataTable.Rows[rowIndex].Field<string>("EmpImage")!="")
            {
                UserImage.ImageUrl = "~/UploadImg/" + dataTable.Rows[rowIndex].Field<string>("EmpImage"); 
            }
            else
            {
                
            }
           
            lblEmpName.Text = dataTable.Rows[rowIndex].Field<string>("EmpName");
            lblshortName.Text = dataTable.Rows[rowIndex].Field<string>("EmpName");
            lblDesignation.Text = dataTable.Rows[rowIndex].Field<string>("Designation");
       
            lblGender.Text = dataTable.Rows[rowIndex].Field<string>("Gender");
            lblTinNo.Text = dataTable.Rows[rowIndex].Field<string>("TinNo");
            lblFatherName.Text = dataTable.Rows[rowIndex].Field<string>("FatherName");
            lblFatherOccupation.Text = dataTable.Rows[rowIndex].Field<string>("FatherOccupation");
            lblMotherName.Text = dataTable.Rows[rowIndex].Field<string>("MotherName");
            lblMotherOccupation.Text = dataTable.Rows[rowIndex].Field<string>("MotherOccupation");

            if ((dataTable.Rows[rowIndex]["DateOfBirth"] != DBNull.Value))
            {
                lblDateofBirth.Text =
                    Convert.ToDateTime(dataTable.Rows[rowIndex].Field<DateTime>("DateOfBirth")).ToString("dd-MMM-yyyy");
            }

            if ((dataTable.Rows[rowIndex]["DateOfJoin"] != DBNull.Value))
            {
                lblDateofJoin.Text =
                    Convert.ToDateTime(dataTable.Rows[rowIndex].Field<DateTime>("DateOfJoin")).ToString("dd-MMM-yyyy");
            }
            lblReligion.Text = dataTable.Rows[rowIndex].Field<string>("Religion");
            lblMaritalStatus.Text = dataTable.Rows[rowIndex].Field<string>("MaritalStatus");
            lblEmployeeType.Text = dataTable.Rows[rowIndex].Field<string>("EmpType");
            lblBloodGroup.Text = dataTable.Rows[rowIndex].Field<string>("BloodGroup");

            
                lblPlaceOfBirth.Text = (dataTable.Rows[rowIndex].Field<string>("PlaceOfBirth"));    
            
            lblNationality.Text = dataTable.Rows[rowIndex].Field<string>("Nationality");
            lblNationalID.Text = dataTable.Rows[rowIndex].Field<string>("NationalIdNo");
            lblPassportNo.Text = dataTable.Rows[rowIndex].Field<string>("PassportNo");
            lblExpectedServiclength.Text = dataTable.Rows[rowIndex].Field<string>("ExpectedServiceLength");
            if ((dataTable.Rows[rowIndex]["DateOfRetirement"] != DBNull.Value))
            {
                lblDateofRetirement.Text =
                    Convert.ToDateTime(dataTable.Rows[rowIndex].Field<DateTime>("DateOfRetirement"))
                        .ToString("dd-MMM-yyyy");
            }
            if ((dataTable.Rows[rowIndex]["DateOfConformation"] != DBNull.Value))
            {
                lblDateofConformation.Text =
                    Convert.ToDateTime(dataTable.Rows[rowIndex].Field<DateTime>("DateOfConformation"))
                        .ToString("dd-MMM-yyyy");
            }
            lblConformationStatus.Text = dataTable.Rows[rowIndex].Field<string>("ConformationStatus");
            lblReportingBoss.Text = dataTable.Rows[rowIndex].Field<string>("ReportingEmp");

            if (dataTable.Rows[rowIndex]["IsProbationary"] != DBNull.Value)
            {
                lblIsProbationary.Text = dataTable.Rows[rowIndex].Field<string>("IsProbationary").ToString();
            }
           

            if ((dataTable.Rows[rowIndex]["ProbationEndDate"] != DBNull.Value))
            {
                lblProbationaryEndDate.Text =
                    dataTable.Rows[rowIndex].Field<DateTime>("ProbationEndDate").ToString("dd-MMM-yyyy");
            }



            //Employment Information


            
        }

        DataTable EmploymentdataTable = aUserProfileDAL.GetEmployeeInfoDAL(id);

        const int EmploymentrowIndex = 0;

        if (EmploymentdataTable.Rows.Count > 0)
        {


            lblCompany.Text = dataTable.Rows[EmploymentrowIndex].Field<string>("ShortName");
            lblDivision.Text = dataTable.Rows[EmploymentrowIndex].Field<string>("DivisionName");
            lblWing.Text = dataTable.Rows[EmploymentrowIndex].Field<string>("DivisionWingName");
            lblDepartment.Text = dataTable.Rows[EmploymentrowIndex].Field<string>("DepartmentName");
            lblSection.Text = dataTable.Rows[EmploymentrowIndex].Field<string>("SectionName");
            lblSubSection.Text = dataTable.Rows[EmploymentrowIndex].Field<string>("SubSectionName");
            lblEmployeeCategory.Text = dataTable.Rows[EmploymentrowIndex].Field<string>("EmpCategoryName");
            lblSalaryGrade.Text = dataTable.Rows[EmploymentrowIndex].Field<string>("GradeName");
            lblSalaryStep.Text = dataTable.Rows[EmploymentrowIndex].Field<string>("SalaryStepName");
            lblDesignationE.Text = dataTable.Rows[EmploymentrowIndex].Field<string>("Designation");
            lblDesignationType.Text = dataTable.Rows[EmploymentrowIndex].Field<string>("DesigTypeName");
            lblJobLocation.Text = dataTable.Rows[EmploymentrowIndex].Field<string>("Location");
            lblSalaryLocation.Text = dataTable.Rows[EmploymentrowIndex].Field<string>("SalaryLocation");
        }



        DataTable ContactsdataTable = aUserProfileDAL.GetEmployeeInfoDAL(id);

        const int ContactsrowIndex = 0;

        if (ContactsdataTable.Rows.Count > 0)
        {


            lblPresentAddress.Text = dataTable.Rows[ContactsrowIndex].Field<string>("AddressPresent");
            lblPresentDivision.Text = dataTable.Rows[ContactsrowIndex].Field<string>("PresentDivision");
            lblPresentDistrict.Text = dataTable.Rows[ContactsrowIndex].Field<string>("PresentDistrict");
            lblPresentThana.Text = dataTable.Rows[ContactsrowIndex].Field<string>("PresentThana");
            lblPresentTelNo.Text = dataTable.Rows[ContactsrowIndex].Field<string>("PresentTelNo");
            lblParmanentAddress.Text = dataTable.Rows[ContactsrowIndex].Field<string>("AddressPermanent");
            lblParmanentDivision.Text = dataTable.Rows[ContactsrowIndex].Field<string>("ParmanentDivision");
            lblParmanentDistrict.Text = dataTable.Rows[ContactsrowIndex].Field<string>("PermanentDistrict");
            lblParmanentThana.Text = dataTable.Rows[ContactsrowIndex].Field<string>("PermanentThana");
            lblParmanentTelNo.Text = dataTable.Rows[ContactsrowIndex].Field<string>("ParmanentTelNo");
            lblPersonalEmail.Text = dataTable.Rows[ContactsrowIndex].Field<string>("PersonalEmail");
            lblOfficialEmail.Text = dataTable.Rows[ContactsrowIndex].Field<string>("OfficialEmail");
            lblPersonalMobile.Text = dataTable.Rows[ContactsrowIndex].Field<string>("PersonalMobile");
            lblOfficialMobile.Text = dataTable.Rows[ContactsrowIndex].Field<string>("OfficialMobile");
            lblFax.Text = dataTable.Rows[ContactsrowIndex].Field<string>("FaxNo");


            lblEmergencyContactPerson.Text = dataTable.Rows[ContactsrowIndex].Field<string>("EmergencyContactPerson");
            lblEmergencyContactAddress.Text = dataTable.Rows[ContactsrowIndex].Field<string>("EmergencyContactAddress");
            lblEmergencyNumber.Text = dataTable.Rows[ContactsrowIndex].Field<string>("EmergencyContactNumber");
        }



        DataTable  FamilyInformationdataTable = aUserProfileDAL.GetEmployeeInfoDAL(id);

        const int FamilyInformationrowIndex = 0;

        if (FamilyInformationdataTable.Rows.Count > 0)
        {


            using (DataTable dtSpouse = new CommonDataLoadDAL().GetDTEmpSpouseByEmpId(int.Parse(id)))
            {
                gv_Spouse.DataSource = dtSpouse;
                gv_Spouse.DataBind();
            }

            if ((dataTable.Rows[FamilyInformationrowIndex]["DateOfMarriage"] != DBNull.Value))
            {
                lblMarriageDate.Text = dataTable.Rows[FamilyInformationrowIndex].Field<DateTime>("DateOfMarriage").ToString("dd-MMM-yyyy");
            }
       
             
        }

        DataTable ChildrenInformationdataTable = aUserProfileDAL.GetEmpChildrenInfoDAL(id);

       

        if (ChildrenInformationdataTable.Rows.Count > 0)
        {



            
            gv_Children.DataSource = ChildrenInformationdataTable;
            gv_Children.DataBind();

        }


        DataTable EducationInformationdataTable = aUserProfileDAL.GetEmpEducationInfoDAL(id);



        if (EducationInformationdataTable.Rows.Count > 0)
        {




            gv_Education.DataSource = EducationInformationdataTable;
            gv_Education.DataBind();

        }


        DataTable ExperienceInformationdataTable = aUserProfileDAL.GetEmpExperienceInfoDAL(id);
        if (ExperienceInformationdataTable.Rows.Count > 0)
        {
            gv_Experience.DataSource = ExperienceInformationdataTable;
            gv_Experience.DataBind();

        }


        DataTable TrainingInformationdataTable = aUserProfileDAL.GetEmpTrainingInfoDAL(id);
        if (TrainingInformationdataTable.Rows.Count > 0)
        {
            gv_Training.DataSource = TrainingInformationdataTable;
            gv_Training.DataBind();

        }

        DataTable ReferenceInformationdataTable = aUserProfileDAL.GetEmpReferenceInfoDAL(id);
        if (ReferenceInformationdataTable.Rows.Count > 0)
        {
            gv_Reference.DataSource = ReferenceInformationdataTable;
            gv_Reference.DataBind();

        }


        DataTable NomineeInformationdataTable = aUserProfileDAL.GetEmpNomineeInfoDAL(id);
        if (NomineeInformationdataTable.Rows.Count > 0)
        {
            gv_Nominee.DataSource = NomineeInformationdataTable;
            gv_Nominee.DataBind();

        }


        DataTable ExtraCurriculamInformationdataTable = aUserProfileDAL.GetEmpExtraCurriculamInfoDAL(id);
        if (ExtraCurriculamInformationdataTable.Rows.Count > 0)
        {
            ExtraCurriculamGridView.DataSource = ExtraCurriculamInformationdataTable;
            ExtraCurriculamGridView.DataBind();

        }


        DataTable OtherTalentsInformationdataTable = aUserProfileDAL.EmpOtherTalentsInfoDAL(id);
        if (OtherTalentsInformationdataTable.Rows.Count > 0)
        {
            OtherTalentsGridView.DataSource = OtherTalentsInformationdataTable;
            OtherTalentsGridView.DataBind();

        }


        DataTable AchievementsInformationdataTable = aUserProfileDAL.GetEmpAchievementsInfoDAL(id);
        if (AchievementsInformationdataTable.Rows.Count > 0)
        {
            AchievementsGridView.DataSource = AchievementsInformationdataTable;
            AchievementsGridView.DataBind();

        }

        DataTable HobbyInformationdataTable = aUserProfileDAL.OtherEmpHobbyInfoDAL(id);
        if (HobbyInformationdataTable.Rows.Count > 0)
        {
            HobbyGridView.DataSource = HobbyInformationdataTable;
            HobbyGridView.DataBind();

        }
    }



    private void PopUp(Int32 EmpInfoId)
    {
        string url = "../Report_UI/EmployeeInfoListReportViwer.aspx?rptType=" + EmpInfoId;
        string fullURL = "window.open('" + url + "', '_blank', 'height=600,width=900,status=yes,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no,titlebar=no' );";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", fullURL, true);
    }

    protected void btnPrintCv_Click(object sender, EventArgs e)
    {
        
            
                PopUp(Convert.ToInt32(Session["EmpInfoId"].ToString()));
            
         
    }
}