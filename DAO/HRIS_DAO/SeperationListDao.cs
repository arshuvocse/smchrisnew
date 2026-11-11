using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class SeperationListDao
    {
        public Int32 ConfirmationId { set; get; }
        public Int32 EmpInfoId { set; get; }
        public string EmpMasterCode { set; get; }
        public string ZID { set; get; }
        public DateTime SeperationDate { set; get; }
        public int SeperationTypeId { set; get; }
        public string Approveby { set; get; }
        public DateTime ApproveDate { set; get; }
    }
}
