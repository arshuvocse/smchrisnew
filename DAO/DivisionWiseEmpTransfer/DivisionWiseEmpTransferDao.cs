using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DivisionWiseEmpTransfer
{
   public class DivisionWiseEmpTransferDao
    {
       public string EmpInfoId { get; set; }
       public int? CompanyId { get; set; }
       public int? DivisionId { get; set; }
       public int? DivisionWId { get; set; }
       public int? DepartmentId { get; set; }
       public int? SectionId { get; set; }
       public int? SubSectionId { get; set; }
       public int? TransferDivisionId { get; set; }
       public bool? IsEmployeeShiftHierarchyGenerate { get; set; }
       public bool? IsOnlyEmployeeTransfer { get; set; }

    }
}
