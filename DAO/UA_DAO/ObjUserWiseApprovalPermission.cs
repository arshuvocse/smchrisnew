using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.UA_DAO
{
    public class ObjUserWiseApprovalPermission
    {
        public int UWAId { get; set; }
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public int ManuSL { get; set; }
        public int ActionId { get; set; }
        public bool Add { get; set; }
        public bool View { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Everyone { get; set; }
        public bool Own { get; set; }
    }
}
