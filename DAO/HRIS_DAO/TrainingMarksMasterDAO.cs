using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class TrainingMarksMasterDAO
    {
        public int TrainigMarkId { get; set; }
        public int TrainingRecordMasterId { get; set; }
        public decimal OutOfMark { get; set; }
        public string Remarks { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
