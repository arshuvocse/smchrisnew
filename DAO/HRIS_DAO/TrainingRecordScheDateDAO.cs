using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class TrainingRecordScheDateDAO
    {
        public int TrainingRecordScheDateId { get; set; }
        public int TrainingRecordMasterId { get; set; }
        public DateTime Date { get; set; }
        public string Day { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
