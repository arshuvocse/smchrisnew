using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HealthCare_Dao
{
    public class BillSettlementDocument
    {
        public int BillSettlementDocumentId { get; set; }

        public int? BillSettlmentId { get; set; }

        public string DocumentLink { get; set; }

        public string DocumentNote { get; set; }

        public string FileName { get; set; }
    }
}
