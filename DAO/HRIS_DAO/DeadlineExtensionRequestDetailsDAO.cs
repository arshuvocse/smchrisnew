using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public class DeadlineExtensionRequestDetailsDAO
    {
       public int DeadlineExtensionRequestDetailsId { get; set; }

       public int DeadlineExtensionRequestId { get; set; }

       public int EmployeeId { get; set; }

       public DateTime? ExtensionDate { get; set; }
       public string ApprovalStatus { get; set; }

       public int ApprovedBy { get; set; }

       public DateTime? ApproveDate { get; set; }




    }
}
