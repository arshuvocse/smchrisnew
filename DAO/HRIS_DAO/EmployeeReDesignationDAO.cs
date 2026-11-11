using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.HRIS_DAO
{
    public class EmployeeReDesignationDAO
    {

        public int EmployeeReDesignationId { get; set; }

        public int CompanyId { get; set; }
        public Nullable<int> FinancialYearId { get; set; }
        public Nullable<int> PDesignationId { get; set; }
        public Nullable<int> NDesignationId { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> Effectivedate { get; set; }

        public string EntryBy { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }


        public bool IsDelete { get; set; }
        public string DeleteBy { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }

        public Nullable<int> DivisionId { get; set; }
        public Nullable<int> DivisionWId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> SectionId { get; set; }
        public Nullable<int> SubSectionId { get; set; }


        public Nullable<int> EmployeeId { get; set; }
        public Nullable<int> EmpTypeId { get; set; }
        public Nullable<int> SalaryLoationId { get; set; }

        public Nullable<int> EmployeeCode { get; set; }
        public Nullable<int> EmployeeName { get; set; }
        public Nullable<int> DesignationId { get; set; }


        public Nullable<int> JobLocationId { get; set; }







        public string AutoProcess { get; set; }


    }
}
