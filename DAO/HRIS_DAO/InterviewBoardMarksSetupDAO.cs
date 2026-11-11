using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class InterviewBoardMarksSetupDAO
    {
        public int MarksSetupId { get; set; }
        public int SetupMasterId { get; set; }
        public bool IsWritten { get; set; }
        public decimal? WrittenMarks { get; set; }
        public bool IsOther { get; set; }
        public decimal? OtherMarks { get; set; }
        public bool IsViva { get; set; }
        public int? VivaId { get; set; }
        public decimal? VivaMarks { get; set; }

    }
}
