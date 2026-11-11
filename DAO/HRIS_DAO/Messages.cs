using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class Messages
    {
        public  string SaveSuccessMessage = @"Information Created Successfully..";
        public  string ConflictMessage = @"Information Already Exists !!!";
        public  string ErrorMessage = @"Save Opearation Failed !!!";
        public string UpdateFailedMessage = @"Update Opearation Failed !!!";
        public  string UpdateSuccessMessage = @"Information Updated Successfully..";
        public  string DeleteMessage = @"Delete Successfully..";
        public  string RequireFieldMessage = @"Please fill out this field !!!";
        public  string InvalidEmailMessage = @"Please insert valid email address !!!";


        public string CodeExistMessage = @"Code Already Exists !!!";
        public string LocationExistMessage = @"Location Already Exists !!!";

        //Field Empty Message
        public string VShortName = @"Short name can not be empty!!!";
        public string VCompany = @"Company name can not be empty!!!";
        public string VDivision = @"Division name can not be empty!!!";
        public string VDivisionWing = @"Division Wing name can not be empty!!!";
        public string VDepartment = @"Department name can not be empty!!!";
        public string VSection = @"Section name can not be empty!!!";
        public string VSubSection = @"SubSection name can not be empty!!!";
        public string VDesignation = @"Designation name can not be empty!!!";
        public string VDesignationStep = @"DesignationStep name can not be empty!!!";
        public string VRegion = @"Region name can not be empty!!!";
        public string VRegionCode = @"Region Code can not be empty!!!";
        public string VArea = @"can not be empty!!!";
        public string VAreaCode = @"Area Code can not be empty!!!";
        public string VJobLocation = @"Job Location name can not be empty!!!";

        //Field Empty Message
        public string NoData = @"No Data found!!!";
         
        //Salary
        public string VSalaryType = @"Salary Type name can not be empty!!!";
        public string VSalaryGrade = @"Salary Grade name can not be empty!!!";
        public string VSalaryGradeType = @"Salary Grade Type can not be empty!!!";
        public string VSalaryLocation = @"Salary Location can not be empty!!!";
        public string VSalaryStep = @"Salary Step can not be empty!!!";

        //Delete error Message
        public string SDivisionDelete = @"Cann't delete because it contain a department!!!";
        public string SWingDelete = @"Cann't delete because it contain a department!!!";


        //Training Organization
        public string VOrgName = @"Organization Name can not be empty!!!";
        public string VOrgType = @"Organization Type can not be empty!!!";
        public string VTrainingLocation = @"Organization Type can not be empty!!!";


        // Training Setup

        public string TTtriningTitle = @"Training title can not be empty!!!";
        public string TTQuater = @"Quater  can not be empty!!!";
        public string TTFinancialYear = @"Financial Year  can not be empty!!!";
        public string TTrainingStart = @"Training start date  can not be empty!!!";
        public string TTrainingEnd = @"Training end date  can not be empty!!!";
        public string TTrainingExist = @"Trainner already exists !!!";




        public string VOrgTrainer = @"Trainner can not be empty !!!";
        public string VOrgBranches = @"Branches can not be empty !!!";
        public string VOrgRegYear = @"Registration year can not be empty !!!";
        // Training Budget 

        public string Bud_TotalParticipant = @"Total Participant can not be empty!!!";
        public string Bud_CostPer = @"Cost  can not be empty!!!";
        public string Bud_Bud = @"Budget  can not be empty!!!";
        public string Bud_Deprtment = @"Department List  can not be empty!!!";
        public string Bud_Grade = @"Grade List  can not be empty!!!";
        public string Bud_Employee = @"Employee List  can not be empty!!!";
        public string Bud_Participant = @"Choose Target Participant !!!";


        public string Bud_Quater = @"Select Quater !!!";
        public string Bud_FromDate = @"Training From Date Required !!!";
        public string Bud_ToDate = @"Training End Date Required !!!";


        public string Bud_DptInGrid = @"Department Already  Exists in Same Quater !!!";



        public string VOffice = @"Office name can not be empty!!!";
    }
}
