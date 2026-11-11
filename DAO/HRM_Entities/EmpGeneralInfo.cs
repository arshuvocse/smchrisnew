using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Library.DAO.HRM_Entities
{
   public class EmpGeneralInfo
    {
        public int? EmpInfoId { get; set; }
        public string EmpMasterCode { get; set; } 
        public string EmpName { get; set; }
        public string ShortName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PlaceOfBirth { get; set; }
        public string BloodGroup { get; set; }
        public string Gender { get; set; }
        public string AddressPresent { get; set; }
        public string AddressPermanent { get; set; }
        public string MedicalInformation { get; set; }
        public string PhoneNo { get; set; }
        public string CellNumber { get; set; }
        public string Email { get; set; }
        public int LineId { get; set; }
        public string WHoliday { get; set; }
        public string MaritalStatus { get; set; }
        public string SpouseName { get; set; }
        public string SpouseDateOfBirth { get; set; }
        public string NationalIdNo { get; set; }
        public string RefName { get; set; }
        public string RefAddress { get; set; }
        public string RefCellNo { get; set; }
        public string CardNo { get; set; }
        public int CompanyInfoId { get; set; }
        public int UnitId { get; set; }
        public int DivisionId { get; set; }
        public int DesigId { get; set; }
        public int DepId { get; set; }
        public int SectionId { get; set; }
        public int EmpGradeId { get; set; }
        public int SalScaleId { get; set; }
        public int EmpTypeId { get; set; }
        public int ShiftId { get; set; }
        public string NAge { get; set; }
        public string Age { get; set; }
        public string BankAccNo { get; set; }
        public int BankId { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string EmployeeStatus { get; set; }
        public string PayType { get; set; }
        
        public DateTime? ProbationPeriodTo { get; set; }
        public DateTime? ExtProbationPeriod { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public string PFEligbility { get; set; }
        public string OTAllow { get; set; }
        public string Remarks { get; set; }        
        public string EntryBy { get; set; }
        public string InactiveReason { get; set; }
        public DateTime EntryDate { get; set; }
        public string ShiftEmployee { get; set; }
        public string ActionStatus { get; set; }
        public bool? IsActive { get; set; }
        public int EmpCategoryId { get; set; }
        public string EmergencycontactPerson { get; set; }
        public string EmergencycontactNumber { get; set; }
        public string TINNo { get; set; }
        public bool? IsSameData { get; set; }
        public bool IsSalesPersonal { get; set; }
        public string SalesPersonalType { get; set; }
        public int RegionId { get; set; }
        public int AreaId { get; set; }
        public int TerritoryId { get; set; }
        public int JobLocationId { get; set; }
    }
}
