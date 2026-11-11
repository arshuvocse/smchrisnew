using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HealthCare_Dao
{
   public class ReimbursmentMaster
    {
        public int ReimbursFromMasterId { get; set; }

        public string Type { get; set; }

        public decimal? RemainingBalance { get; set; }

        public int? CompanyId { get; set; }

        public int? FinancialYearId { get; set; }
        public int? HospitalNameId { get; set; }
        public DateTime? HospitalAdmissionDate { get; set; }
        public DateTime? HospitalDischargeDate { get; set; }
        public string HospitalNotificationEmailDoc { get; set; }
        public string HospitalNotificationEmailFileName { get; set; }

        public int? EmpInfoId { get; set; }

        public string EmpMasterCode { get; set; }

        public int? DivisionId { get; set; }

        public int? DivisionWId { get; set; }

        public int? DepartmentId { get; set; }

        public int? SectionId { get; set; }

        public int? SubSectionId { get; set; }
        public int? DesignationId { get; set; }
        public int? EmpTypeId { get; set; }
        public string PatientName { get; set; }
        public int? PatientAge { get; set; }
        public string Relationship { get; set; }
        public string ActionStatus { get; set; }
        public string BankName { get; set; }
        public string BankAccountNo { get; set; }
        public string BranchName { get; set; }
        public string RoutingNo { get; set; }
        public bool? IsActive { get; set; }
        public int? EntryBy { get; set; }
        public DateTime? EntryDate { get; set; }
        public string SelfDate { get; set; }
        public string OfficialMobile { get; set; }
        public DateTime? SubmitDate { get; set; }
        public List<ReimbursmentDocument> ReimbursmentDocuments { get; set; }
        public List<ReimbursmentEnclosuretickMark> ReimbursmentEnclosuretickMarks { get; set; }
        public List<ReimbursmentberifDiscriptionDao> ReimbursmentberifDiscription { get; set; } 
    }
}
