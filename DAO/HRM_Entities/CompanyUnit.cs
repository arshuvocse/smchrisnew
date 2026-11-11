using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class CompanyUnit
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public string UnitCode { get; set; }
        public string UnitAddress { get; set; }
        public string PhoneNo { get; set; }
        public string MobileNo { get; set; }
        public string FaxNo { get; set; }
        public int CompanyInfoId { get; set; }
        public bool IsActive { get; set; }
    }
}
