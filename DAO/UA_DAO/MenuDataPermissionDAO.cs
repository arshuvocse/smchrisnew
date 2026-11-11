using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.UA_DAO
{
    public class MenuDataPermissionDAO
    {
        public int MenuDataPerId { get; set; }
        public int UserId { get; set; }
        public int MainMenuId { get; set; }
        public bool Own { get; set; }
        public bool SMC { get; set; }
        public bool SMCEL { get; set; }
    }
}
