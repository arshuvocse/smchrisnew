using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
  public   class OfficeBranch
    {

       public Int32  OfficeBranchId{get;set;}
       public Int32   TrainingOrgId{get;set;}
       public string BranchDetails { get; set; }
       public string BranchAddress { get; set; }
       public int CountryID { get; set; }
    }
}
