using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class HolidayInfo
    {
        public int HolidayId { get; set; }
        public DateTime HolidayfromDate { get; set; }
        public DateTime HolidaytoDate { get; set; }
        public string Details { get; set; }
        public int CompanyInfoId { get; set; }
        public int UnitId { get; set; }
        public bool IsActive { get; set; }
    }
}
