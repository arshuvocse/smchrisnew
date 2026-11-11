using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class EmpSalaryGenerator
    {
        public int SalGeneratId { get; set; }
        public int SalaryId { get; set; }
        public int EmpInfoId { get; set; }
        public DateTime SalaryStartDate { get; set; }
        public DateTime SalaryEndDate { get; set; }
        public DateTime JoiningDate { get; set; }
        public string EmpMasterCode { get; set; }
        public int CompanyInfoId { get; set; }
        public int UnitId { get; set; }
        public int DivisionId { get; set; }
        public int DeptId { get; set; }
        public int SectionId { get; set; }
        public int DesigId { get; set; }
        public int EmpTypeId { get; set; }
        public int EmpGradeId { get; set; }
        public int SalScaleId { get; set; }
        public int MonthDays { get; set; }
        public int PunchDay { get; set; }
        public int TotalHoliday { get; set; }
        public int TotalLeave { get; set; }
        public int WorkingDays { get; set; }
        public int AbsentDays { get; set; }
        public decimal Gross { get; set; }
        public decimal ActualGross { get; set; }
        public decimal Basic { get; set; }
        public decimal HouseRent { get; set; }
        public decimal Medical { get; set; }
        public decimal ConveyanceAllowance { get; set; }
        public decimal LunchAllowance { get; set; }
        public decimal OtherAllowances { get; set; }
        public decimal MobileAllowance { get; set; }
        public decimal SpecialAllowance { get; set; }
        public decimal HolidayBillAmt { get; set; }
        public decimal NightBillAmt { get; set; }
        public decimal Arrear { get; set; }
        public string OTHours { get; set; }
        public decimal OTRate { get; set; }
        public decimal OTAmount { get; set; }
        public decimal TiffinCharge { get; set; }
        public decimal AttnBonusRate { get; set; }
        public decimal SpecialBonus { get; set; }
        public decimal SalaryAdvance { get; set; }
        public decimal Tax { get; set; }
        public decimal Absenteeism { get; set; }
        public decimal OtherDeduction { get; set; }
        public decimal SalAdvDeduct { get; set; }
        public decimal FoodCharge { get; set; }
        public decimal NetPayable { get; set; }
        public int BankId { get; set; }
        public string PayBankorCash { get; set; }
        public string BankAccNo { get; set; }
        public decimal PrFund { get; set; }
        public bool IsActive { get; set; }
        
    }
}
