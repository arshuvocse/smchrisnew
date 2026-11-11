using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class JobCreationDao
    {
        public Int64 JobID { get; set; }

        public int ReqCodeId { get; set; }
        public string JobCode { get; set; }
        public Int32 CompanyId { get; set; }
        public Int32 DepartmentID { get; set; }
        public Int32 SectionID { get; set; }
        public string Position { get; set; }
        public string Vacancy { get; set; }
        public string JobContext { get; set; }
        public string JobResponsibilities { get; set; }
        public bool PermanenteEmp { get; set; }
        public bool ContractualEmp { get; set; }
        public bool TraineeEmp { get; set; }
        public bool CasualEmp { get; set; }
        public Int32 EducationalRequirementsID { get; set; }
        public string ExperienceRequirements { get; set; }
        public string AdditionalRequirements { get; set; }
        public Int32 JobLocationID { get; set; }
        public string Salary { get; set; }
        public string CompensationandOtherBenefits { get; set; }
        public bool NewspaperDS { get; set; }
        public bool TVDS { get; set; }
        public bool OtherDS { get; set; }
        public string Other { get; set; }
        public DateTime CirculationStartDate { get; set; }
        public DateTime CirculationsdeadlineDate { get; set; }
        public DateTime? ProbableInterviewDate { get; set; }
        public DateTime ProbableIRecruitmentDate { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public string Progress { get; set; }
        
        public string VerifiedBy { get; set; }
        public DateTime VerifiedDate { get; set; }
        public string ApproveBy { get; set; }
        public DateTime ApproveDate { get; set; }
        public string InternalNote { get; set; }
        public bool IsSalary { get; set; }


        public int EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public int UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public int DeleteBy { get; set; }
        public DateTime DeleteDate { get; set; }
        public bool IsDelete { get; set; }
        public string ActionStatus { get; set; }

    }
}
