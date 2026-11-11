using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.DAO.HRM_Entities
{
    public class FastibalBonusProcess
    {
        public int BonusRecordId { get; set; }
        public int FastivalBonusId { get; set; }
        public int EmpInfoId { get; set; }
        public int CompanyInfoId { get; set; }
        public int UnitId { get; set; }
        public int DivisionId { get; set; }
        public int DeptId { get; set; }
        public int SectionId { get; set; }
        public int DesigId { get; set; }
        public int EmpGradeId { get; set; }
        public int EmpTypeId { get; set; }
        public int SalScaleId { get; set; }
        public int EmpCategoryId { get; set; }
        public DateTime ProcessExecutedTime { get; set; }
        public string ProcessExecutedBy { get; set; }
        public string PayType { get; set; }
        public decimal Gross { get; set; }
        public decimal Basic { get; set; }
        public int FastivalBonusDetailsId { get; set; }
        public decimal BonusPercentage { get; set; }
        public decimal BonusAmount { get; set; }
        
    }
}
