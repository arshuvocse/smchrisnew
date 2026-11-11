using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Library.DAO.HRM_Entities
{
    public class EmpEducationInfo
    {
       public int EmpEduId { get; set; } 
       public int EmpInfoId { get; set; }
       public string BoardUniverName { get; set; } 
       public string PassYear { get; set; }
       public string Qualification { get; set; }
       public string AreaStudy { get; set; }
       public string Result { get; set; }
       public string ResultType { get; set; }
       public string Exam { get; set; }
       public int EduInstituteId { get; set; }
       public int ExamId { get; set; }
       public int QualificationId { get; set; }
       public int StudyId { get; set; }
       
    }
}
