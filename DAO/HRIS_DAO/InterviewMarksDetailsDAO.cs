using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class InterviewMarksDetailsDAO
    {
        public int InterviewMarksDetailsId { get; set; }
        public int BoardDetailsId { get; set; }
        public int CandidateID { get; set; }
        public int? VivaId { get; set; }
        public decimal? VivaMarks { get; set; }
        public decimal? WrittenMarks { get; set; }
        public decimal? OtherMarks { get; set; }
    }
}
