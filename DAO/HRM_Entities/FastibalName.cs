using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class FastibalName
    {
        public int FestivalId { get; set; }
        public string FestivalName { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsActive { get; set; }
        
    }
}
