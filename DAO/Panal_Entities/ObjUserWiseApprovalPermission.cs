using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAO.Panal_Entities
{
  public class ObjUserWiseApprovalPermission
    {
        public int UWAId { get; set; }
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public int ManuSL { get; set; }
        public int ActionId { get; set; }
    }
}
