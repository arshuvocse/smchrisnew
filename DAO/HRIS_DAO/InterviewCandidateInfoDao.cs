using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class InterviewCandidateInfoDao
    {
        public Int32 CandidateID { get; set; }
        public Int32 CompanyId { get; set; }
        public string CompanyName { get; set; }
        public Int32 JobID { get; set; }

        public string JobCode { get; set; }
        public string Position { get; set; }
        public string CandidateName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public string EmailAdress { get; set; }
        public string TotalYearsOfExp { get; set; }
        public string LastOrganization { get; set; }
        public string LastPosition { get; set; }
        public Int32 ExamID { get; set; }
        public string LastExam { get; set; }
        public Int32 MejorID { get; set; }
        public string LastMajor { get; set; }
        public string LastResultType { get; set; }
        public string LastResultDivision { get; set; }
        public string LastResultCGPA { get; set; }
        public string LastResultOutOf { get; set; }
        public string LastPassingYear { get; set; }



        public Int32 ResultID { get; set; }
        public Int32 PassingYearID { get; set; }
        public string Point { get; set; }
        public string OutOf { get; set; }




        public Int32 CvUploadID { get; set; }
        public Int32 PhotoUploadID { get; set; }
        public string ExpectedSalary { get; set; }
        public string CurrentSalary { get; set; }
        public string Remarks { get; set; }
        public string ApprovalStatus { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string Updateby { get; set; }
        public DateTime UpdateDate { get; set; }
        public string VerifiedBy { get; set; }
        public DateTime VerifiedDate { get; set; }
        public string ApproveBy { get; set; }
        public DateTime ApproveDate { get; set; }
        public string InternalNote { get; set; }
    }
}
