using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public  class RptHeader
    {
        public int RptId { get; set; }
        public string RptHeading { get; set; }
        public string RptAddress { get; set; }
        public string RptTel { get; set; }
        public string RptFax { get; set; }
        public string RptEmail { get; set; }
        public string RptMessage { get; set; }
        public byte[] RptImage { get; set; }
        public bool IsActive { get; set; }
    }
}
