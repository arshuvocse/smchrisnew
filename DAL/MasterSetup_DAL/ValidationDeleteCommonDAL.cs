using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.InternalCls;

namespace DAL.MasterSetup_DAL
{
   public class ValidationDeleteCommonDAL
    {
       ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

       public DataTable InterviewBoardSetupAllocatedOrNot(string EmpFOccupation)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@FatherOccupation", EmpFOccupation));

           const string queryStr = @"SELECT  JobTitleId FROM dbo.tblInterviewBoardSetupMaster WHERE IsActive=1 and JobTitleId=@FatherOccupation";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }
       public DataTable ParmanentDivisionAllocatedOrNotEMP(string ParmanentDivisionId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@ParmanentDivision", ParmanentDivisionId));

           const string queryStr = @"SELECT ParmanentDivision FROM dbo.tblEmpGeneralInfo WHERE ParmanentDivision= @ParmanentDivision";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable AllocatedOrNotEMP(string SubjectGroupId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@SubjectGroupId", SubjectGroupId));

           const string queryStr = @"SELECT  SubjectGroupId   FROM dbo.tblEmpEducation WHERE SubjectGroupId=@SubjectGroupId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }



       public DataTable PresentDivisionAllocatedOrNotEMP(string PresentDivisionId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@PresentDivision", PresentDivisionId));

           const string queryStr = @"SELECT PresentDivision FROM dbo.tblEmpGeneralInfo WHERE PresentDivision= @PresentDivision";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable DistrictAllocatedOrNotEMP(string DivisionID)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@DivisionID", DivisionID));

           const string queryStr = @"SELECT DivisionID FROM dbo.tblDistrict WHERE DivisionID= @DivisionID";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }


       public DataTable PresentDistricAllocatedOrNotEMP(string PresentDistrictId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@PresentDistrict", PresentDistrictId));

           const string queryStr = @"SELECT PresentDistrict FROM dbo.tblEmpGeneralInfo WHERE PresentDistrict= @PresentDistrict";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }


       public DataTable ThanaListAllocatedOrNotEMP(string DistrictID)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@DistrictID", DistrictID));

           const string queryStr = @"SELECT DistrictID FROM dbo.tblThana WHERE DistrictID= @DistrictID";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable ParmanentDistricAllocatedOrNotEMP(string PermanentDistrictId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@PermanentDistrict", PermanentDistrictId));

           const string queryStr = @"SELECT PermanentDistrict FROM dbo.tblEmpGeneralInfo WHERE PermanentDistrict= @PermanentDistrict";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }



       public DataTable PresentThanaAllocatedOrNotEMP(string PresentThanaId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@PresentThana", PresentThanaId));

           const string queryStr = @"SELECT PresentThana FROM dbo.tblEmpGeneralInfo WHERE PresentThana= @PresentThana";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable ParmanentThanaAllocatedOrNotEMP(string PermanentThanaId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@PermanentThana", PermanentThanaId));

           const string queryStr = @"SELECT PermanentThana FROM dbo.tblEmpGeneralInfo WHERE PermanentThana=@PermanentThana";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }


       public DataTable EmpFOccupationAllocatedOrNotEMP(string EmpFOccupation)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@FatherOccupation", EmpFOccupation));

           const string queryStr = @"SELECT FatherOccupation FROM dbo.tblEmpGeneralInfo WHERE FatherOccupation=@FatherOccupation";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable EmpMOccupationAllocatedOrNotEMP(string EmpMOccupation)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@MotherOccupation", EmpMOccupation));

           const string queryStr = @"SELECT MotherOccupation FROM dbo.tblEmpGeneralInfo WHERE MotherOccupation=@MotherOccupation";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable NomNomineeOccupationAllocatedOrNotEMP(string NomNomineeOccupation)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@NomineeOccupation", NomNomineeOccupation));

           const string queryStr = @"SELECT NomineeOccupation FROM tblEmpNominee WHERE NomineeOccupation=@NomineeOccupation";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable RefOccupationAllocatedOrNotEMP(string RefOccupation)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@RefOccupation", RefOccupation));

           const string queryStr = @"SELECT RefOccupation FROM dbo.tblEmpReference WHERE RefOccupation=@RefOccupation";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable EmpSpouseOccupationAllocatedOrNotEMP(string EmpSpouseOccupation)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@SpouseOccupation", EmpSpouseOccupation));

           const string queryStr = @"SELECT  SpouseOccupation FROM dbo.tblEmpGeneralInfo WHERE SpouseOccupation=@SpouseOccupation";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }
       public DataTable EmpChildrenOccupationAllocatedOrNotEMP(string EmpChildrenOccupation)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@ChildrenOccupation", EmpChildrenOccupation));

           const string queryStr = @"SELECT ChildrenOccupation FROM dbo.tblEmpChildren   WHERE ChildrenOccupation=@ChildrenOccupation";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }





       public DataTable EducationNameAllocatedOrNotEMP(string EducationNameId) 
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EducationNameId", EducationNameId));

           const string queryStr = @"SELECT EducationNameId FROM tblEmpEducation WHERE EducationNameId= @EducationNameId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable EmpSpouseMaxEduAllocatedOrNotEMP(string SpouseMaxEducation)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@SpouseMaxEducation", SpouseMaxEducation));

           const string queryStr = @"SELECT SpouseMaxEducation FROM dbo.tblEmpGeneralInfo WHERE SpouseMaxEducation=@SpouseMaxEducation";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }



       public DataTable BoardUniversityAllocatedOrNotEMP(string BoardUniversity)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@BoardUniversityId", BoardUniversity));

           const string queryStr = @"SELECT BoardUniversityId FROM dbo.tblEmpEducation WHERE BoardUniversityId= @BoardUniversityId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable EducationalInstituteAllocatedOrNotEMP(string EducationalInstitute)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EducationalInstituteId", EducationalInstitute));

           const string queryStr = @"SELECT EducationalInstituteId FROM dbo.tblEmpEducation WHERE EducationalInstituteId=@EducationalInstituteId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }


       public DataTable HobbyAllocatedOrNotEMP(string Hobby)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@MasterHobbyId", Hobby));

           const string queryStr = @"SELECT MasterHobbyId FROM dbo.tblEmpHobby WHERE MasterHobbyId=@MasterHobbyId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable OtherTalentsAllocatedOrNotEMP(string Hobby)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@MasterOtherTalentsId", Hobby));

           const string queryStr = @"SELECT MasterOtherTalentsId FROM dbo.tblEmpOtherTalents WHERE MasterOtherTalentsId=@MasterOtherTalentsId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }


       public DataTable ExtraCurriculamAllocatedOrNotEMP(string Hobby)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@MasterExtraCurriculamId", Hobby));

           const string queryStr = @"SELECT MasterExtraCurriculamId FROM dbo.tblEmpExtraCurriculam WHERE MasterExtraCurriculamId=@MasterExtraCurriculamId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }


       public DataTable SurveyQuestionGroupAllocatedOrNotEMP(string Hobby)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionGroupId", Hobby));

           const string queryStr = @"SELECT  SurveyQuestionGroupId   FROM dbo.tblSurveyQuestion WHERE SurveyQuestionGroupId=@SurveyQuestionGroupId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }
       public DataTable AchievementsAllocatedOrNotEMP(string MasterAchievementsId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@MasterAchievementsId", MasterAchievementsId));

           const string queryStr = @"SELECT MasterAchievementsId FROM dbo.tblEmpAchievements WHERE MasterAchievementsId=@MasterAchievementsId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable FieldofspecializationAllocatedOrNotEMP(string FieldOfSpecializationId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@FieldOfSpecializationId", FieldOfSpecializationId));

           const string queryStr = @"SELECT FieldOfSpecializationId FROM dbo.tblEmpEducation WHERE FieldOfSpecializationId=@FieldOfSpecializationId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }


       public DataTable NationalityAllocatedOrNotEMP(string Nationality)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@Nationality", Nationality));

           const string queryStr = @"SELECT Nationality FROM dbo.tblEmpGeneralInfo WHERE Nationality=@Nationality";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }



       public DataTable EducationResultAllocatedOrNotEMP(string Result)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@Result", Result));

           const string queryStr = @"SELECT Result FROM dbo.tblEmpEducation WHERE Result=@Result";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable TrainingInstitutionAllocatedOrNotEMP(string TrainingInstitution)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@TrainingInstitution", TrainingInstitution));

           const string queryStr = @"SELECT TrainingInstitution FROM dbo.tblEmpTraining WHERE TrainingInstitution= @TrainingInstitution";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }



       public DataTable EMPDepartmentAllocatedOrNotEMP(string DepartmentId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@DepartmentId", DepartmentId));

           const string queryStr = @"SELECT DepartmentId FROM dbo.tblEmpGeneralInfo WHERE DepartmentId= @DepartmentId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }


       public DataTable EMPDptName(string DepartmentId, string DepartmentName)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@DepartmentId", DepartmentId));
           aSqlParameterlist.Add(new SqlParameter("@DepartmentName", DepartmentName));

           const string queryStr = @"SELECT * FROM dbo.tblDepartment WHERE DepartmentName=@DepartmentName AND    DepartmentId NOT IN ( @DepartmentId)
";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable VivaSetupAllocatedOrNotEMP(string DepartmentId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@DepartmentId", DepartmentId));

           const string queryStr = @"SELECT VivaId FROM dbo.tblInterviewBoardMarksSetup WHERE VivaId= @DepartmentId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }



       public DataTable EMPDivisionAllocatedOrNotEMP(string DivisionId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@DivisionId", DivisionId));

           const string queryStr = @"SELECT DivisionId FROM dbo.tblEmpGeneralInfo WHERE DivisionId= @DivisionId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }


       public DataTable CandidateInfoForItvitationAllocatedOrNotEMP(string DepartmentId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@DepartmentId", DepartmentId));

           const string queryStr = @"SELECT CandidateID FROM dbo.tblInterviewCandidateInvitation WHERE CandidateID= @DepartmentId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }



       public DataTable InterviewCandidateAttandanceForCandidateInvitation(string DepartmentId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@DepartmentId", DepartmentId));

           const string queryStr = @"SELECT CandidateID FROM tblInterviewCandidateAttandance WHERE CandidateID=@DepartmentId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }



       public DataTable WritMarksEntryByJobIdForAttend(string DepartmentId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@DepartmentId", DepartmentId));

           const string queryStr = @"SELECT * FROM tblInterviewMarksDetails WHERE JobId=@DepartmentId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable VivaMarksEntryByJobIdForAttend(string DepartmentId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@DepartmentId", DepartmentId));

           const string queryStr = @"SELECT * FROM tblVivaDetailsMark WHERE JobId=@DepartmentId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable LoadBoardSetupByMasterId(string param)
       {

           string query = @"SELECT ApprovalStatus FROM tblInterviewBoardSetupMaster WHERE SetupMasterId=" + param;
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }

      

       public DataTable EMPDivisionWinGAllocatedOrNotEMP(string DivisionWId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@DivisionWId", DivisionWId));

           const string queryStr = @"SELECT DivisionWId FROM dbo.tblEmpGeneralInfo WHERE DivisionWId= @DivisionWId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }


       public DataTable EMPSectionAllocatedOrNotEMP(string Section)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@SectionId", Section));

           const string queryStr = @"SELECT SectionId FROM dbo.tblEmpGeneralInfo WHERE SectionId= @SectionId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable NominationPurposeAllocatedOrNotEMP(string Nationality)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@NominationPurpose", Nationality));

           const string queryStr = @" SELECT NominationPurpose FROM dbo.tblEmpNominee WHERE NominationPurpose=@NominationPurpose";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }


       public DataTable EMPSubSectionGAllocatedOrNotEMP(string SubSection)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@SubSectionId", SubSection));

           const string queryStr = @"SELECT SubSectionId FROM dbo.tblEmpGeneralInfo WHERE SubSectionId= @SubSectionId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

    }
}
