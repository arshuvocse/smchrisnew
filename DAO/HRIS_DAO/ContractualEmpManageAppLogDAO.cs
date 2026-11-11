using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class ContractualEmpManageAppLogDAO
    {
        public int ContractualEmpManageAppLogId { get; set; }
        public int ContractualEmpManageId { get; set; }
        public int PreEmpInfoId { get; set; }
        public int ForEmpInfoId { get; set; }
        public int Version { get; set; }
        public string ApproveBy { get; set; }
        public DateTime ApproveDate { get; set; }
        public string ActionStatus { get; set; }
        public string Comments { get; set; }
        public int CommentsId { get; set; }
        public bool IsForward { get; set; }
    }
}
