using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class EmpGroup
    {
        public int GroupId { get; set; }
        public int UnitId { get; set; }
        public string GroupName { get; set; }
        public bool IsActive { get; set; }
    }
}
