using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class Line
    {
        public int LineId { get; set; }
        public string LineName { get; set; }
        public bool IsActive { get; set; }
    }
}
