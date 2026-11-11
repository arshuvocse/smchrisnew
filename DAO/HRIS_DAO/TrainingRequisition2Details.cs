using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public  class TrainingRequisition2Details
    {

        public int TrainingRequisition2DetailsId { get; set; }

        public int? TrainingRequisition2Id { get; set; }

        public int? QuaterId { get; set; }

        public int? MonthId { get; set; }

        public string TrainingTitle { get; set; }

        public string ExpectedResult { get; set; }
    }
}
