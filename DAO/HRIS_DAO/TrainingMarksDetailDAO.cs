using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class TrainingMarksDetailDAO
    {
        public int TrainingMarkDetailId { get; set; }
        public int TrainingRecordDetailsEmp { get; set; }
        public int EmpInfoId { get; set; }
        public decimal PreMark { get; set; }
        public decimal PostMark { get; set; }
        public int TrainigMarkId { get; set; }

    }
}
