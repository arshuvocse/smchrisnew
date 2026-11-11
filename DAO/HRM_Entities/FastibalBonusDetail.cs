using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class FastibalBonusDetail
    {
        public int FastivalBonusDetailsId { get; set; }
        public int FastivalBonusId { get; set; }
        public int FromMonth { get; set; }
        public int ToMonth { get; set; }
        public decimal Percentige { get; set; }
        public string EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        
    }
}
