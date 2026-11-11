using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
   public  class TrainingOrgInfo
    {

      public int TrainingOrgId{get;set;}
      public string TrainingOrgName{get;set;}
      public int OrgTypeId{get;set;}
      public int CompanyId { get; set; }
      public int CountryID{get;set;}
      public string OrgAddress{get;set;}
      public string ContactPerson{get;set;}
      public string ContactPersonCell{get;set;}
      public string ContactPersonEmail{get;set;}
      public bool? IsForeign{get;set;}
      public bool? IsLocal{get;set;}
      public bool? IsInHouse{get;set;}
      public bool VendorAudit{get;set;}
      public string Remarks{get;set;}
      public string OrgProfile{get;set;}
      public string Clients{get;set;}
      public bool ClientsRecommendation{get;set;}
      public bool LogisticsFacility{get;set;}
      public string Affiliation{get;set;}
      public DateTime RegistrationYear{get;set;}
      public string EntryBy{get;set;}
      public DateTime? EntryDate{get;set;}
      public string UpdateBy{get;set;}
      public DateTime? UpdateDate{get;set;}

      public bool HasTin { get; set; }
      public bool HasVat { get; set; }
      public bool HasBankSolv { get; set; }
      public bool HasTradeLicense { get; set; }
      public string Others { get; set; }
    }
}
