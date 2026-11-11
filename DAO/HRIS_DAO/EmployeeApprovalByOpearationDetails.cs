using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
  public   class EmployeeApprovalByOpearationDetails
    {

      public Int32 EmployeeApprovalByOpearationDetailId { set; get; }
      public Int32 MasterId { set; get; }
      public Int32 Operation { set; get; }
      public Int32 CompanyId { set; get; }
      public Int32 EmpInfoId { set; get; }

      public Int32 Serial { get; set; }
      public bool Isheadperson { get; set; }

        
    }
}
