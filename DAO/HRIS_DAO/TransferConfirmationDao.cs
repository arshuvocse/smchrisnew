using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class TransferConfirmationDao
    {
        public Int32 ConfirmationId { set; get; }
        public Int32 EmpInfoId { set; get; }
        public string EmpMasterCode { set; get; }
        public string ZID { set; get; }
        public DateTime EffectiveDate { set; get; }
        public int SalaryLocationId { set; get; }
        public int JobLocationId { set; get; }
        public string Approveby { set; get; }
        public DateTime ApproveDate { set; get; }
    }
}
