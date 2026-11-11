using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.UA_DAO
{
    public class SupervisorMenuAppDAO
    {
        public int  SuperMenuAppId { get; set; }
        public int CompanyId { get; set; }
        public int MainMenuId { get; set; }
        public int EmpInfoId { get; set; }
        public int FromEmpInfoId { get; set; }
        public bool IsAllEmployee { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateDate { get; set; }

    }
}
