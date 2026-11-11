using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class ClearenceFormDetailDao
    {
        public int DepartmentId { get; set; }
        public int MasterId { get; set; }
        public int PersonId { get; set; }
        public string Resource { get; set; }
        public string MainRemarks { get; set; }
        public string Recommend { get; set; }
        public string ApprovalCondition { get; set; }
        public string SetInfo { get; set; }
        public string Remarks { get; set; }
        public int EmpID { get; set; }
        public int IsDoneEmpId { get; set; }
        public int? exitMasterIdNew { get; set; }
        public int? exitDetailIdNew { get; set; }

        public DateTime IsDoneDate { get; set; }

    }
}
