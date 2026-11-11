using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class TrainingAttendanceMaster
    {

        public int TrainingAttendanceMasterId { get; set; }

        public int TrainingMasterId { get; set; }

        public int TrainingAllocationId { get; set; }
        public int TrainingRquisitionMaster { get; set; }

        

        public string EntryBy { get; set; }

        public DateTime EntryDate { get; set; }

        public string UpdateBy { get; set; }
        public string Quater { get; set; }

        public DateTime UpdateDate { get; set; }

    }
}
