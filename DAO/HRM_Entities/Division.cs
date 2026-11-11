using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class Division
    {
        public int DivisionId { get; set; }
        public string DivCode{ get; set; }
        public string DivName { get; set; }
        public string DivShortName { get; set; }
        public bool IsActive { get; set; }
    }
}
