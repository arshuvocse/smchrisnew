using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class Designation
    {
        public int DesignationId { get; set; }
        public string DesignaitonCode { get; set; }
        public string DesignationName { get; set; }
        public decimal HDutyBill { get; set; }
        public decimal NightBill { get; set; }
        public decimal OTRate { get; set; }
        public decimal TiffinAllow { get; set; }
        public bool IsActive { get; set; }
    }
}
