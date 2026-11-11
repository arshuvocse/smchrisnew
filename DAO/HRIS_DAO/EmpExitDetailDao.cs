using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class EmpExitDetailDao
    {
        public int ExitDetailId { get; set; }
        public int MasterId { get; set; }
        public int DepartmentId { get; set; }
        public int EmpInfoId { get; set; }
        public int EmployeeIdForClearance { get; set; }
        public string ApprovalStatus { get; set; }
        public string SetInfo { get; set; }
        
    }
}
