using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public class DeadlineExtendedEntryDAO
    {
         
    public int    DeadlineExtensionRequestId  { get; set; }
     public int   CompanyId  { get; set; }
    public int    FinYearId  { get; set; }
    public int?    DepartmentId  { get; set; }
     public string   Operation  { get; set; }
     public DateTime   ExtensionDate  { get; set; }
     public string   Description  { get; set; }
     public string   Remarks  { get; set; }
     public int   EntryBy  { get; set; }
    public DateTime    EntryDate  { get; set; }
    public int    UpdateBy  { get; set; }
     public DateTime   UpdateDate  { get; set; }
      public int  DeleteBy  { get; set; }
      public DateTime DeleteDate { get; set; }
      public bool IsDelete { get; set; }
      public bool? IsDepartment { get; set; }
      public bool? IsEmployee { get; set; }
      public string ApprovalStatus { get; set; }
    }
}
