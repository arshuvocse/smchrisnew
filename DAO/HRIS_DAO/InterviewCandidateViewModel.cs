using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class InterviewCandidateViewModel
    {
        public int InterviewCandidateGradingId { get; set; }
        public int JobID { get; set; }
        public string JobCode { get; set; }
        public string Position { get; set; }
        public int CandidateID { get; set; }
        public string CandidateName { get; set; }
        public decimal Attitude { get; set; }
        public decimal Language { get; set; }
        public decimal TechnicalSkill { get; set; }
        public decimal IQ { get; set; }
        public decimal GeneralKnowledge { get; set; }
        public decimal Others { get; set; }
        public decimal TimeSence { get; set; }
        public decimal TotalMarks { get; set; }
        public decimal WrittenMarks { get; set; }
        public decimal WrittenMarksOutOf { get; set; }
        public decimal VivaMarks { get; set; }
        public decimal VivaMarksOutOf { get; set; }
        public decimal OtherMarks { get; set; }
        public decimal OtherMarksOutOf { get; set; }
        public decimal InterviewMarks { get; set; }
        public string LetterGrade { get; set; }
        public bool IsRecommended { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public string Remarks { get; set; }
        
    }
}
