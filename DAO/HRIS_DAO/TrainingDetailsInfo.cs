using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class TrainingDetailsInfo
    {

        public int TrainingDetailsId { get; set; }
        public int TrainingMasterId { get; set; }
        public int TrainerId { get; set; }
        public string NotListedName { get; set; }
        public string NotListedDetails { get; set; }
    }
}
