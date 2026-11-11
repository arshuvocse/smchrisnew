using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class TrainingAttendanceDAO
    {
        public int TrainingAttId { get; set; }
        public int TrainingRecordMasterId { get; set; }
        public DateTime ATTDate { get; set; }
        public int TrainingRecordScheDateId { get; set; }
        public int EmpInfoId { get; set; }
        public bool IsPresent { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public int TrainingRecordDetailsEmp { get; set; }


    }
}
