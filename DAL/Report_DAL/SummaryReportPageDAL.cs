using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.DataManager;
using DAL.InternalCls;

namespace DAL.Report_DAL
{
  public  class SummaryReportPageDAL
    {
      readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
      public DataTable GetComapnyNameList()
      {
          string queryStr = "SELECT CompanyId as Value,CompanyName, ShortName as TextField FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";


          return aCommonInternalDal.GetDTforDDLForChartCompanyLoad(queryStr, null, DataBase.HRDB);
      }

      public DataTable GetDeptAndGradeWiseNoofEmployee(string companyId)
      {
          string queryStr = "";

          if (companyId == "2") {
              queryStr = @" SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , tblDepartment.DepartmentName,GradeCode,EmpCategoryName,COUNT(EmpInfoId) NoOfEmp FROM dbo.tblEmpGeneralInfo WITH (nolock) 
                               LEFT JOIN dbo.tblSalaryGrade ON tblSalaryGrade.SalaryGradeId = tblEmpGeneralInfo.SalaryGradeId
                               LEFT JOIN tblDepartment ON tblEmpGeneralInfo.DepartmentId = tblDepartment.DepartmentId
                               LEFT JOIN tblEmpCategory ON tblEmpGeneralInfo.EmpCategoryId = tblEmpCategory.EmpCategoryId
                               WHERE  EmployeeStatus='Active' AND tblEmpGeneralInfo.IsActive='1'  and tblEmpGeneralInfo.CompanyId=" + companyId + " GROUP BY tblDepartment.DepartmentName,GradeCode ,EmpCategoryName ORDER BY EmpCategoryName,GradeCode ";

          }
          else
          {
              queryStr = @" SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , tblDepartment.DepartmentName,GradeCode,EmpCategoryName,COUNT(EmpInfoId) NoOfEmp FROM dbo.tblEmpGeneralInfo WITH (nolock) 
                               LEFT JOIN dbo.tblSalaryGrade ON tblSalaryGrade.SalaryGradeId = tblEmpGeneralInfo.SalaryGradeId
                               LEFT JOIN tblDepartment ON tblEmpGeneralInfo.DepartmentId = tblDepartment.DepartmentId
                               LEFT JOIN tblEmpCategory ON tblEmpGeneralInfo.EmpCategoryId = tblEmpCategory.EmpCategoryId
                               WHERE  EmployeeStatus='Active' AND tblEmpGeneralInfo.IsActive='1'  and  tblEmpGeneralInfo.EmpCategoryId=1   and tblEmpGeneralInfo.CompanyId=" + companyId + " GROUP BY tblDepartment.DepartmentName,GradeCode ,EmpCategoryName  " +
                         "" +
                         "" +
                         "" +
                         @"UNION ALL



							   SELECT  100 AS Sl , 'Graded' DepartmentName,'N/A' GradeCode,EmpCategoryName,COUNT(EmpInfoId) NoOfEmp FROM dbo.tblEmpGeneralInfo WITH (nolock) 
                              
                           
                               LEFT JOIN tblEmpCategory ON tblEmpGeneralInfo.EmpCategoryId = tblEmpCategory.EmpCategoryId
                               WHERE  EmployeeStatus='Active' AND tblEmpGeneralInfo.IsActive='1'  and tblEmpGeneralInfo.CompanyId=" + companyId + "  and  tblEmpGeneralInfo.EmpCategoryId=2 GROUP BY  EmpCategoryName      order by Sl";
          }
           

          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }
      public DataTable GetDeptAndTypeWiseNoofEmployee(string companyId)
      {
//          string queryStr = @"SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , DPT.DepartmentName [Department],     ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract], ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [Sub total], 0 AS [Casual], 0 AS Subcontract , 0 AS [Sub contract],ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) [All total], 0 AS [Manager & above],0 AS [Officer to DM], 0 AS [Graded],  0 AS [Sub total], 0 [Casual],0 AS [Sub contract],0 AS[All total], ISNULL(tblMale.Male,0) Male,  (ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0))    MalePer, ISNULL(tblFeMale.Female,0) Female,  ( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) ) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
//LEFT JOIN tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
//LEFT JOIN tblEmployeeType AS TP ON EGI.EmpTypeId = TP.EmpTypeId
// left join (select COUNT(EGI.EmpInfoId) Permnaent, EGI.DepartmentId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
// EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=1 group by EGI.DepartmentId) tblPermnaent on dpt.DepartmentId=tblPermnaent.DepartmentId
//left join (select COUNT(EGI.EmpInfoId) Contractual, EGI.DepartmentId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
// EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=2 group by EGI.DepartmentId) tblContractual on dpt.DepartmentId=tblContractual.DepartmentId
// left join (select COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer,   EGI.DepartmentId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
// EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='Male' group by EGI.DepartmentId) tblMale on dpt.DepartmentId=tblMale.DepartmentId
//
// left join (select COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer, EGI.DepartmentId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
// EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='FeMale' group by EGI.DepartmentId) tblFeMale on dpt.DepartmentId=tblFeMale.DepartmentId
// 
//WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' AND  (DPT.Invisible='0' OR DPT.Invisible IS NULL)  and EGI.CompanyId=" + companyId + @" GROUP BY DPT.DepartmentName,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  
// ORDER BY DPT.DepartmentName";

          string queryStr = @"SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , DPT.DepartmentName [Department],     ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract], ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [Sub total], 0 AS [Casual], 0 AS Subcontract , 0 AS [Sub contract],ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) [All total], CASE WHEN tblManager_Above.[Manager & above] IS Null THEN 0 ElSE tblManager_Above.[Manager & above] END [Manager & above],CASE WHEN tblOfficer_DM.[Officer to DM] IS NULL THEN 0 ELSE tblOfficer_DM.[Officer to DM] END [Officer to DM], 0 AS [Graded],  0 AS [Sub total], 0 [Casual],0 AS [Sub contract],0 AS[All total], ISNULL(tblMale.Male,0) Male,  (ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0))    MalePer, ISNULL(tblFeMale.Female,0) Female,  ( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) ) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
LEFT JOIN tblEmployeeType AS TP ON EGI.EmpTypeId = TP.EmpTypeId
 left join (select COUNT(EGI.EmpInfoId) Permnaent, EGI.DepartmentId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=1 group by EGI.DepartmentId) tblPermnaent on dpt.DepartmentId=tblPermnaent.DepartmentId
left join (select COUNT(EGI.EmpInfoId) Contractual, EGI.DepartmentId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=2 group by EGI.DepartmentId) tblContractual on dpt.DepartmentId=tblContractual.DepartmentId
 left join (select COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer,   EGI.DepartmentId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='Male' group by EGI.DepartmentId) tblMale on dpt.DepartmentId=tblMale.DepartmentId

 left join ( select COUNT(EGI.EmpInfoId) [Manager & above], EGI.DepartmentId from tblEmpGeneralInfo EGI  WITH (NOLOCK)
 LEFT JOIN tblSalaryGrade SG ON  EGI.SalaryGradeId = SG.SalaryGradeId
 where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" And  SG.GradeCode IN ('M-1','M-2A','M-2B','M-3A','M-3B','M-2','M-3','M-4')  group by EGI.DepartmentId) tblManager_Above  on dpt.DepartmentId=tblManager_Above.DepartmentId

 left join ( select COUNT(EGI.EmpInfoId) [Officer to DM], EGI.DepartmentId from tblEmpGeneralInfo EGI  WITH (NOLOCK)
 LEFT JOIN tblSalaryGrade SG ON  EGI.SalaryGradeId = SG.SalaryGradeId
 where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" And  SG.GradeCode IN ('M-0','M-5','M-6','M-6A','M-6B','M-7','M-8','M-9')  group by EGI.DepartmentId) tblOfficer_DM  on dpt.DepartmentId=tblOfficer_DM.DepartmentId


 left join (select COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer, EGI.DepartmentId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='FeMale' group by EGI.DepartmentId) tblFeMale on dpt.DepartmentId=tblFeMale.DepartmentId
 
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' AND  (DPT.Invisible='0' OR DPT.Invisible IS NULL)  and EGI.CompanyId=" + companyId + @" GROUP BY DPT.DepartmentName,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer,tblManager_Above.[Manager & above],tblOfficer_DM.[Officer to DM]  
 ORDER BY DPT.DepartmentName";

          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }
      public DataTable GetDeptAndGenderWiseNoofEmployee(string companyId)
      {
          string queryStr = @"SELECT DPT.DepartmentName,Gender,COUNT(EmpInfoId) NoOfEmp FROM tblEmpGeneralInfo AS EGI 
LEFT JOIN tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
LEFT JOIN tblEmployeeType AS TP ON EGI.EmpTypeId = TP.EmpTypeId
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' 
AND EGI.IsActive='1' and EGI.CompanyId=" + companyId + " GROUP BY DPT.DepartmentName,Gender ORDER BY DPT.DepartmentName,Gender";

          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }
      public DataTable GradeWiseNoofEmployee(string companyId)
      {
            //          string queryStr = @"SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , DPT.Designation [Designation], CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 

            //LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
            //LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
            // left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent, EGI.DesignationId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
            // EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @"  and EmpTypeId=1 group by EGI.DesignationId,EGI.SalaryGradeId) tblPermnaent on dpt.DesignationId=tblPermnaent.DesignationId AND tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
            //left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual, EGI.DesignationId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
            // EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @"  and EmpTypeId=2 group by EGI.DesignationId, EGI.SalaryGradeId) tblContractual on dpt.DesignationId=tblContractual.DesignationId AND tblContractual.SalaryGradeId = EGI.SalaryGradeId
            // left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer,   EGI.DesignationId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
            // EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='Male' group by EGI.DesignationId,EGI.SalaryGradeId) tblMale on dpt.DesignationId=tblMale.DesignationId AND tblMale.SalaryGradeId = EGI.SalaryGradeId

            // left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer, EGI.DesignationId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
            // EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @"  and EGI.Gender='FeMale' group by EGI.DesignationId, EGI.SalaryGradeId) tblFeMale on dpt.DesignationId=tblFeMale.DesignationId AND tblFeMale.SalaryGradeId = EGI.SalaryGradeId

            //WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and EGI.CompanyId=" + companyId + @" GROUP BY DPT.Designation,GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  
            // ORDER BY DPT.Designation";

           string queryStr = "";

          if (companyId == "2")
          {
              

                    queryStr = @"SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode IS NULL THEN 'MD & CEO' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and  EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId
left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and EGI.CompanyId=" + companyId + @" and GradeCode IS NULL GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  

UNION ALL


SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-1' THEN 'GM' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId

left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
 
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and EGI.CompanyId=" + companyId + @" and GradeCode='M-1' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  

UNION ALL


SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-2A' THEN 'Company Secretary/Addl.GM' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId

left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
 
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and EGI.CompanyId=" + companyId + @" and GradeCode='M-2A' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  

UNION ALL


SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-2B' THEN 'Addl.GM/ Head' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId

left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
 
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and EGI.CompanyId=" + companyId + @" and GradeCode='M-2B' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer 


UNION ALL

SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-3A' THEN 'Head' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId

left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
 
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and EGI.CompanyId=" + companyId + @" and GradeCode='M-3A' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  


UNION ALL


SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-3B' THEN 'Senior Manager' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId

left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
 
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and EGI.CompanyId=" + companyId + @" and GradeCode='M-3B'   GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer 


UNION ALL



SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-4' THEN 'Manager' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId

left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
 
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and EGI.CompanyId=" + companyId + @" and GradeCode='M-4'   GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer 

UNION ALL

SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-5' THEN 'Deputy Manager' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
 
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and EGI.CompanyId=" + companyId + @" and GradeCode='M-5'   GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  

UNION ALL

SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-6A' THEN 'Principal Executive' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and  EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId
left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and EGI.CompanyId=" + companyId + @" and GradeCode='M-6A' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  

UNION ALL

SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-6B' THEN 'Senior Executive' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and EGI.CompanyId=" + companyId + @" and GradeCode='M-6B' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  

UNION ALL

SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-7' THEN 'Executive' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and EGI.CompanyId=" + companyId + @" and GradeCode='M-7' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  

UNION ALL


SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-8' THEN 'Senior Officer' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and EGI.CompanyId=" + companyId + @" and GradeCode='M-8' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  


UNION ALL

SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-9' THEN 'Officer' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and EGI.CompanyId=" + companyId + @" and GradeCode='M-9' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer 

UNION ALL


SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-0' THEN 'Consultant/Others' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and EGI.CompanyId=" + companyId + @" and GradeCode='M-0' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  

UNION ALL

SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl ,  DPT.Designation  [Designation], CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 

LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent, EGI.DesignationId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @"  and EmpTypeId=1 group by EGI.DesignationId,EGI.SalaryGradeId) tblPermnaent on dpt.DesignationId=tblPermnaent.DesignationId AND tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual, EGI.DesignationId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EmpTypeId=2 group by EGI.DesignationId, EGI.SalaryGradeId) tblContractual on dpt.DesignationId=tblContractual.DesignationId AND tblContractual.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer,   EGI.DesignationId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='Male' group by EGI.DesignationId,EGI.SalaryGradeId) tblMale on dpt.DesignationId=tblMale.DesignationId AND tblMale.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer, EGI.DesignationId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId + @" and EGI.Gender='FeMale' group by EGI.DesignationId, EGI.SalaryGradeId) tblFeMale on dpt.DesignationId=tblFeMale.DesignationId AND tblFeMale.SalaryGradeId = EGI.SalaryGradeId
 
WHERE EGI.EmpInfoId IS NOT NULL  AND DPT.Designation  IS NOT NULL  AND EmployeeStatus='Active' AND  EGI.IsActive='1' and EGI.CompanyId=" + companyId + @" and GradeCode NOT IN ('M-1','M-2A','M-2B','M-3A','M-3B','M-4','M-5','M-6B','M-6A','M-7','M-8','M-9','M-0') GROUP BY DPT.Designation,GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer 

ORDER BY Sl";

          }


          else
          {
              queryStr = @"SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode IS NULL THEN 'MD & CEO' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and  EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" +
                         companyId +
                         @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId
left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" +
                         companyId +
                         @" and GradeCode IS NULL GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  

UNION ALL


SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-1' THEN 'GM' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId

left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
 
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" +
                         companyId +
                         @" and GradeCode='M-1' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  

UNION ALL


SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-2A' THEN 'Company Secretary/Addl.GM' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId

left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
 
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" +
                         companyId +
                         @" and GradeCode='M-2A' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  

UNION ALL


SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-2B' THEN 'Addl.GM/ Head' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId

left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and  EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
 
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" +
                         companyId +
                         @" and GradeCode='M-2B' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer 


UNION ALL

SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-3A' THEN 'Head' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId

left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
 
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" +
                         companyId +
                         @" and GradeCode='M-3A' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  


UNION ALL


SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-3B' THEN 'Senior Manager' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId

left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
 
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" +
                         companyId +
                         @" and GradeCode='M-3B'   GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer 


UNION ALL



SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-4' THEN 'Manager' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId

left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
 
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" +
                         companyId +
                         @" and GradeCode='M-4'   GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer 

UNION ALL

SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-5' THEN 'Deputy Manager' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
 
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" +
                         companyId +
                         @" and GradeCode='M-5'   GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  

UNION ALL

SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-6A' THEN 'Principal Executive' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and  EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" +
                         companyId +
                         @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId
left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and  EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" +
                         companyId +
                         @" and GradeCode='M-6A' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  

UNION ALL

SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-6B' THEN 'Senior Executive' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and EGI.CompanyId=" + companyId +
                         @" and GradeCode='M-6B' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  

UNION ALL

SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-7' THEN 'Executive' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" +
                         companyId +
                         @" and GradeCode='M-7' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  

UNION ALL


SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-8' THEN 'Senior Officer' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and     EGI.EmpCategoryId=1 and  EGI.CompanyId=" + companyId +
                         @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and     EGI.EmpCategoryId=1 and  EGI.CompanyId=" + companyId +
                         @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" +
                         companyId +
                         @" and GradeCode='M-8' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  


UNION ALL

SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-9' THEN 'Officer' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" +
                         companyId +
                         @" and GradeCode='M-9' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer 

UNION ALL


SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl , CASE WHEN GradeCode='M-0' THEN 'Consultant/Others' END [Designation] , CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @"  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId +
                         @" and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" +
                         companyId +
                         @" and GradeCode='M-0' GROUP BY GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer  

UNION ALL

SELECT  ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl ,  DPT.Designation  [Designation], CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END Grade, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as [# of Staff],   ISNULL(tblPermnaent.Permnaent,0) Permanent,  ISNULL(tblContractual.Contractual,0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(tblMale.Male,0) Male, CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))    MalePer, ISNULL(tblFeMale.Female,0) Female,  CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 

LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent, EGI.DesignationId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @"  and EmpTypeId=1 group by EGI.DesignationId,EGI.SalaryGradeId) tblPermnaent on dpt.DesignationId=tblPermnaent.DesignationId AND tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual, EGI.DesignationId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EmpTypeId=2 group by EGI.DesignationId, EGI.SalaryGradeId) tblContractual on dpt.DesignationId=tblContractual.DesignationId AND tblContractual.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer,   EGI.DesignationId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and  EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='Male' group by EGI.DesignationId,EGI.SalaryGradeId) tblMale on dpt.DesignationId=tblMale.DesignationId AND tblMale.SalaryGradeId = EGI.SalaryGradeId

 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer, EGI.DesignationId from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and   EGI.EmpCategoryId=1 and EGI.CompanyId=" + companyId +
                         @" and EGI.Gender='FeMale' group by EGI.DesignationId, EGI.SalaryGradeId) tblFeMale on dpt.DesignationId=tblFeMale.DesignationId AND tblFeMale.SalaryGradeId = EGI.SalaryGradeId
 
WHERE EGI.EmpInfoId IS NOT NULL  AND DPT.Designation  IS NOT NULL  AND EmployeeStatus='Active' AND  EGI.IsActive='1' and   EGI.EmpCategoryId=1  and  EGI.CompanyId=" +
                         companyId +
                         @" and GradeCode NOT IN ('M-1','M-2A','M-2B','M-3A','M-3B','M-4','M-5','M-6B','M-6A','M-7','M-8','M-9','M-0') GROUP BY DPT.Designation,GradeCode,tblPermnaent.Permnaent,tblContractual.Contractual,tblMale.Male, tblFeMale.Female,tblFeMale.FemalePer,tblMale.MalePer 




union all

SELECT  19 AS Sl ,'Graded' Designation, '' Grade, ISNULL(count(tblPermnaent.Permnaent),0) + ISNULL(COUNT(tblContractual.Contractual),0) as [# of Staff],   ISNULL(COUNT(tblPermnaent.Permnaent),0) Permanent,  ISNULL(COUNT(tblContractual.Contractual),0) [Contract],  0 AS [Casual],  0 AS [Sub contract], ISNULL(COUNT(tblMale.Male),0) Male, CAST(ISNULL(NULLIF(count(tblMale.MalePer),0)/(isnull(count(tblMale.Male),0)+isnull(count(tblFeMale.Female),0)),0) as nvarchar(max))    MalePer, ISNULL(COUNT(tblFeMale.Female),0) Female,  CAST( ISNULL(ISNULL(NULLIF(count(tblFeMale.FemalePer),0)/(isnull((count(tblMale.Male)),0)+isnull(count(tblFeMale.Female),0)),0),0) as nvarchar(max)) FemalePer  FROM tblEmpGeneralInfo AS EGI  WITH (NOLOCK) 
LEFT JOIN tblDesignation AS DPT ON EGI.DesignationId = DPT.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Permnaent  from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId +
                         @"  and   EGI.EmpCategoryId=2  and EmpTypeId=1 group by EGI.SalaryGradeId) tblPermnaent on  tblPermnaent.SalaryGradeId = EGI.SalaryGradeId
left join (select  EGI.SalaryGradeId,COUNT(EGI.EmpInfoId) Contractual from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId +
                         @" and   EGI.EmpCategoryId=2 and EmpTypeId=2 group by  EGI.SalaryGradeId) tblContractual on  tblContractual.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Male,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 )  malePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId +
                         @" and   EGI.EmpCategoryId=2 and EGI.Gender='Male' group by EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId = EGI.SalaryGradeId
 left join (SELECT  EGI.SalaryGradeId, COUNT(EGI.EmpInfoId) Female,(COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FemalePer from tblEmpGeneralInfo EGI  WITH (NOLOCK)  where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + companyId +
                         @" and   EGI.EmpCategoryId=2 and EGI.Gender='FeMale' group by EGI.SalaryGradeId) tblFeMale on  tblFeMale.SalaryGradeId = EGI.SalaryGradeId
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' and EGI.CompanyId=" + companyId +
                         @"  and   EGI.EmpCategoryId=2   



ORDER BY Sl";
          }

          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }

      public DataTable GetDeptAndDesignationWiseNoofEmployee(string companyId)
      {

          string queryStr = @"SELECT DPT.DepartmentName,TP.EmpType,Gender,COUNT(EmpInfoId) NoOfEmp FROM tblEmpGeneralInfo AS EGI 
LEFT JOIN tblDepartment AS DPT ON EGI.DepartmentId = DPT.DepartmentId
LEFT JOIN tblEmployeeType AS TP ON EGI.EmpTypeId = TP.EmpTypeId
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1' 
and EGI.CompanyId=" + companyId + " GROUP BY DPT.DepartmentName,TP.EmpType,Gender ORDER BY DPT.DepartmentName,TP.EmpType,Gender";

          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
          
      }

      public DataTable GetDesigAndGradeWiseNoofEmployee(string companyId)
      {
          string queryStr = @"SELECT DSG.Designation,CASE WHEN GradeCode IS NULL THEN 'N/A' ELSE GradeCode END GradeCode
,Gender,COUNT(EmpInfoId) NoOfEmp FROM tblEmpGeneralInfo AS EGI 
LEFT JOIN tblDesignation AS DSG ON EGI.DesignationId = DSG.DesignationId
LEFT JOIN dbo.tblSalaryGrade AS SG ON SG.SalaryGradeId = EGI.SalaryGradeId
LEFT JOIN tblEmployeeType AS ETP ON EGI.EmpTypeId = ETP.EmpTypeId
WHERE EGI.EmpInfoId IS NOT NULL AND EmployeeStatus='Active' AND EGI.IsActive='1'  
and EGI.CompanyId=" + companyId + " GROUP BY DSG.Designation,GradeCode,EmpType,Gender ORDER BY DSG.Designation,GradeCode,Gender";

          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }

      public DataTable GetTable01_DepartmentEmploymentDataDAL(int company)
      {

          HttpContext.Current.Session["companyNewIdd"] = company;
          string queryStr = @"select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS ItemNo, dpt.DepartmentId,  dpt.DepartmentName , ISNULL(tblPermnaent.Permnaent,0) Permnaent,  ISNULL(tblContractual.Contractual,0) Contractual, 0 as Casual, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as AllTotal,
 ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as SubTotal from tblDepartment dpt
left join (select COUNT(EGI.EmpInfoId) Permnaent, EGI.DepartmentId from tblEmpGeneralInfo EGI where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" +company+@" and EmpTypeId=1 group by EGI.DepartmentId) tblPermnaent on dpt.DepartmentId=tblPermnaent.DepartmentId

 
left join (select COUNT(EGI.EmpInfoId) Contractual, EGI.DepartmentId from tblEmpGeneralInfo EGI where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + company + @" and EmpTypeId=2 group by EGI.DepartmentId) tblContractual on dpt.DepartmentId=tblContractual.DepartmentId

 where  dpt.IsActive=1 and CompanyId=" + company + @"

order by dpt.DepartmentName";
          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }


      public DataTable GeBindTable05_GradeWiseStaffDataDAL(int company)
      {
          string queryStr = @"--Table 05
select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS ItemNo, SG.SalaryGradeId, ISNULL(SG.GradeCode, '')+  ISNULL(' ; '+SG.GradeName,'') as GradeName, ISNULL(tblPermnaent.Permnaent,0) Permnaent,
ISNULL(tblContractual.Contractual,0) Contractual, 
0 as Casual, ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0) as AllTotal, ISNULL(tblMale.Male,0) Male, 
 CAST(ISNULL(tblMale.MalePer,0)/NULLIF((ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0)),0) as nvarchar(max))+'%'    MalePer, ISNULL(tblFemale.Female,0) Female, 
cast( ISNULL(tblFemale.FeMalePer,0)/NULLIF((ISNULL(tblPermnaent.Permnaent,0) + ISNULL(tblContractual.Contractual,0)),0)  as nvarchar(max))+'%' FeMalePer  from  dbo.tblSalaryGrade SG 

left join (select COUNT(EGI.EmpInfoId) Permnaent,  EGI.SalaryGradeId from tblEmpGeneralInfo EGI where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + company + @"  and EmpTypeId=1 group by  EGI.SalaryGradeId) tblPermnaent on SG.SalaryGradeId=tblPermnaent.SalaryGradeId


 left join (select COUNT(EGI.EmpInfoId) Contractual, EGI.SalaryGradeId from tblEmpGeneralInfo EGI where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + company + @" and EmpTypeId=2  group by  EGI.SalaryGradeId) tblContractual on tblContractual.SalaryGradeId=SG.SalaryGradeId


  left join (select COUNT(EGI.EmpInfoId) Male, (COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) MalePer,  EGI.SalaryGradeId from tblEmpGeneralInfo EGI where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + company + @"  and EGI.Gender='Male'  group by  EGI.SalaryGradeId) tblMale on tblMale.SalaryGradeId=SG.SalaryGradeId

  left join (select COUNT(EGI.EmpInfoId) Female , (COUNT(ISNULL(EGI.EmpInfoId,0))* 100 ) FeMalePer,EGI.SalaryGradeId from tblEmpGeneralInfo EGI where EGI.IsActive=1 and 
 EmployeeStatus='Active' and EGI.CompanyId=" + company + @"  and EGI.Gender='Female'  group by  EGI.SalaryGradeId) tblFemale on tblFemale.SalaryGradeId=SG.SalaryGradeId

where SG.IsActive=1 ";
          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }

      public DataTable GeBindTable03_PrjectWiseMaleFemaleDataDAL(int company)
      {
          string queryStr = @"--Table 03
select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS ItemNo, mas.ProjectName, mas.ProjectId, 
ISNULL(tblMale.Male,0) Male, 
CAST(ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0) as nvarchar(max))  +'%' MalePer,
ISNULL(tblFeMale.Female,0) Female,
 CAST( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0) as nvarchar(max))+'%' FemalePer, 
 ISNULL(tblMale.Male,0)+ISNULL(tblFeMale.Female,0) AllTotal from tblProjectSetup mas with (NOLOCK)
left join (select COUNT(PMas.EmpInfoId) as Male, (COUNT(ISNULL(PMas.EmpInfoId,0))* 100) MalePer,   dtls.ProjectId from  tblEmployeeWiseProjectAllocationDetail dtls
           inner join tblEmployeeWiseProjectAllocationMaster PMas on dtls.EmployeeWiseProjectAllocationMasterId=PMas.EmpWiseProjectID
		   inner join tblEmpGeneralInfo EGI on PMas.EmpInfoId=EGI.EmpInfoId
		   where dtls.IsActive=1   and EGI.Gender='Male'
		   group by dtls.ProjectId
		   )  tblMale on tblMale.ProjectId=mas.ProjectId


		   left join (select COUNT(ISNULL(PMas.EmpInfoId,0)) as Female, (COUNT(ISNULL(PMas.EmpInfoId,0))* 100 ) FemalePer,   dtls.ProjectId from  tblEmployeeWiseProjectAllocationDetail dtls
           inner join tblEmployeeWiseProjectAllocationMaster PMas on dtls.EmployeeWiseProjectAllocationMasterId=PMas.EmpWiseProjectID
		   inner join tblEmpGeneralInfo EGI on PMas.EmpInfoId=EGI.EmpInfoId
		   where dtls.IsActive=1   and EGI.Gender='Female'
		   group by dtls.ProjectId
		   )  tblFeMale on tblFeMale.ProjectId=mas.ProjectId
		   where mas.CompanyId=" + company;
          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }


      public DataTable GeBindTable8DataDAL(int company)
      {
          string queryStr = @"--Table 08
select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl, mas.ProjectName [Project], 
 ISNULL(tblMale.Male,0)+ISNULL(tblFeMale.Female,0) [Total # of staff] ,
ISNULL(tblMale.Male,0) Male, 
 (ISNULL(NULLIF(tblMale.MalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0))    MalePer,
ISNULL(tblFeMale.Female,0) Female,   ( ISNULL(ISNULL(NULLIF(tblFeMale.FemalePer,0)/(isnull(tblMale.Male,0)+isnull(tblFeMale.Female,0)),0),0)) FemalePer
from tblProjectSetup mas with (NOLOCK)
left join (select COUNT(PMas.EmpInfoId) as Male, (COUNT(ISNULL(PMas.EmpInfoId,0))* 100) MalePer,   dtls.ProjectId from  tblEmployeeWiseProjectAllocationDetail dtls
           inner join tblEmployeeWiseProjectAllocationMaster PMas on dtls.EmployeeWiseProjectAllocationMasterId=PMas.EmpWiseProjectID
		   inner join tblEmpGeneralInfo EGI on PMas.EmpInfoId=EGI.EmpInfoId
		   where dtls.IsActive=1   and EGI.Gender='Male'  and EGI.EmployeeStatus='Active'
		   group by dtls.ProjectId
		   )  tblMale on tblMale.ProjectId=mas.ProjectId


		   left join (select COUNT(ISNULL(PMas.EmpInfoId,0)) as Female, (COUNT(ISNULL(PMas.EmpInfoId,0))* 100 ) FemalePer,   dtls.ProjectId from  tblEmployeeWiseProjectAllocationDetail dtls
           inner join tblEmployeeWiseProjectAllocationMaster PMas on dtls.EmployeeWiseProjectAllocationMasterId=PMas.EmpWiseProjectID
		   inner join tblEmpGeneralInfo EGI on PMas.EmpInfoId=EGI.EmpInfoId
		   where dtls.IsActive=1   and EGI.Gender='Female'  and EGI.EmployeeStatus='Active'
		   group by dtls.ProjectId
		   )  tblFeMale on tblFeMale.ProjectId=mas.ProjectId
		   where   mas.IsActive=1 and  ISNULL(tblMale.Male,0)+ISNULL(tblFeMale.Female,0)>0 and mas.CompanyId=" + company;
          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }



           public DataTable GeBindTableDataDAL_Head_field(int company)
      {
          string queryStr = @"    select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS Sl, mas.ProjectName [Project], 
 ISNULL(tblHead.HeadOffice,0)+ ISNULL(tblField.HeadOffice,0) [Total # of staff] ,  ISNULL(tblHeadMale.HeadOffice,0) HeadOfficeMale, ISNULL(tblHeadFeMale.HeadOffice,0) HeadOfficeFeMale,  ISNULL(tblHead.HeadOffice,0) HeadOffice, ISNULL(tblFieldMale.HeadOffice,0) FieldMale, ISNULL(tblFieldFeMale.HeadOffice,0) FieldFeMale,    ISNULL(tblField.HeadOffice,0) Field, ISNULL(tblHeadMen.HeadOffice,0) Men,ISNULL(tblHeadWomenMen.HeadOffice,0) Women ,ISNULL(tblHeadMen.HeadOffice,0)+ISNULL(tblHeadWomenMen.HeadOffice,0) Total
from tblProjectSetup mas with (NOLOCK)

left join (select ISNULL(COUNT(PMas.EmpInfoId),0) as HeadOffice ,   dtls.ProjectId from  tblEmployeeWiseProjectAllocationDetail dtls
           inner join tblEmployeeWiseProjectAllocationMaster PMas on dtls.EmployeeWiseProjectAllocationMasterId=PMas.EmpWiseProjectID
		   inner join tblEmpGeneralInfo EGI on PMas.EmpInfoId=EGI.EmpInfoId
		   where dtls.IsActive=1   and  EGI.EmployeeStatus='Active' and EGI.JobLocationId=1
		   group by dtls.ProjectId
		   )  tblHead on tblHead.ProjectId=mas.ProjectId

		   left join (select ISNULL(COUNT(PMas.EmpInfoId),0) as HeadOffice ,   dtls.ProjectId from  tblEmployeeWiseProjectAllocationDetail dtls
           inner join tblEmployeeWiseProjectAllocationMaster PMas on dtls.EmployeeWiseProjectAllocationMasterId=PMas.EmpWiseProjectID
		   inner join tblEmpGeneralInfo EGI on PMas.EmpInfoId=EGI.EmpInfoId
		   where dtls.IsActive=1   and  EGI.EmployeeStatus='Active' and EGI.Gender='Male'  and EGI.JobLocationId=1
		   group by dtls.ProjectId
		   )  tblHeadMale on tblHeadMale.ProjectId=mas.ProjectId


		     left join (select ISNULL(COUNT(PMas.EmpInfoId),0) as HeadOffice ,   dtls.ProjectId from  tblEmployeeWiseProjectAllocationDetail dtls
           inner join tblEmployeeWiseProjectAllocationMaster PMas on dtls.EmployeeWiseProjectAllocationMasterId=PMas.EmpWiseProjectID
		   inner join tblEmpGeneralInfo EGI on PMas.EmpInfoId=EGI.EmpInfoId
		   where dtls.IsActive=1   and  EGI.EmployeeStatus='Active' and EGI.Gender='FeMale'  and EGI.JobLocationId=1
		   group by dtls.ProjectId
		   )  tblHeadFeMale on tblHeadFeMale.ProjectId=mas.ProjectId



		   
left join (select ISNULL(COUNT(PMas.EmpInfoId),0) as HeadOffice ,   dtls.ProjectId from  tblEmployeeWiseProjectAllocationDetail dtls
           inner join tblEmployeeWiseProjectAllocationMaster PMas on dtls.EmployeeWiseProjectAllocationMasterId=PMas.EmpWiseProjectID
		   inner join tblEmpGeneralInfo EGI on PMas.EmpInfoId=EGI.EmpInfoId
		   where dtls.IsActive=1   and  EGI.EmployeeStatus='Active' and EGI.JobLocationId not in( 1)
		   group by dtls.ProjectId
		   )  tblField on tblField.ProjectId=mas.ProjectId

		   left join (select ISNULL(COUNT(PMas.EmpInfoId),0) as HeadOffice ,   dtls.ProjectId from  tblEmployeeWiseProjectAllocationDetail dtls
           inner join tblEmployeeWiseProjectAllocationMaster PMas on dtls.EmployeeWiseProjectAllocationMasterId=PMas.EmpWiseProjectID
		   inner join tblEmpGeneralInfo EGI on PMas.EmpInfoId=EGI.EmpInfoId
		   where dtls.IsActive=1   and  EGI.EmployeeStatus='Active' and EGI.Gender='Male'  and EGI.JobLocationId not in( 1)
		   group by dtls.ProjectId
		   )  tblFieldMale on tblFieldMale.ProjectId=mas.ProjectId


		     left join (select ISNULL(COUNT(PMas.EmpInfoId),0) as HeadOffice ,   dtls.ProjectId from  tblEmployeeWiseProjectAllocationDetail dtls
           inner join tblEmployeeWiseProjectAllocationMaster PMas on dtls.EmployeeWiseProjectAllocationMasterId=PMas.EmpWiseProjectID
		   inner join tblEmpGeneralInfo EGI on PMas.EmpInfoId=EGI.EmpInfoId
		   where dtls.IsActive=1   and  EGI.EmployeeStatus='Active' and EGI.Gender='FeMale'  and EGI.JobLocationId not in( 1)
		   group by dtls.ProjectId
		   )  tblFieldFeMale on tblFieldFeMale.ProjectId=mas.ProjectId



		   left join (select ISNULL(COUNT(PMas.EmpInfoId),0) as HeadOffice ,   dtls.ProjectId from  tblEmployeeWiseProjectAllocationDetail dtls
           inner join tblEmployeeWiseProjectAllocationMaster PMas on dtls.EmployeeWiseProjectAllocationMasterId=PMas.EmpWiseProjectID
		   inner join tblEmpGeneralInfo EGI on PMas.EmpInfoId=EGI.EmpInfoId
		   where dtls.IsActive=1   and  EGI.EmployeeStatus='Active' and EGI.Gender='Male' and   EGI.JobLocationId is not null
		   group by dtls.ProjectId
		   )  tblHeadMen on tblHeadMen.ProjectId=mas.ProjectId


		   	   left join (select ISNULL(COUNT(PMas.EmpInfoId),0) as HeadOffice ,   dtls.ProjectId from  tblEmployeeWiseProjectAllocationDetail dtls
           inner join tblEmployeeWiseProjectAllocationMaster PMas on dtls.EmployeeWiseProjectAllocationMasterId=PMas.EmpWiseProjectID
		   inner join tblEmpGeneralInfo EGI on PMas.EmpInfoId=EGI.EmpInfoId
		   where dtls.IsActive=1   and  EGI.EmployeeStatus='Active' and EGI.Gender='FeMale' and   EGI.JobLocationId is not null
		   group by dtls.ProjectId
		   )  tblHeadWomenMen on tblHeadWomenMen.ProjectId=mas.ProjectId
		  
		  
		   where   mas.IsActive=1 and ISNULL(tblHeadMen.HeadOffice,0)+ISNULL(tblHeadWomenMen.HeadOffice,0)>0 and mas.CompanyId=" + company;
          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }

      public DataTable GeBindTable04_ProjectWiseStaffDAL(int company)
      {
          string queryStr = @"--Table 4
select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS ItemNo, mas.ProjectName, mas.ProjectId , ISNULL(tblPermnaent.Permnaent,0) Permnaent , ISNULL(tblContractual.Contractual,0) Contractual,
0 as Casual,  ISNULL(tblPermnaent.Permnaent,0)+ISNULL(tblContractual.Contractual,0) as AllTotal,  ISNULL(tblPermnaent.Permnaent,0)+ISNULL(tblContractual.Contractual,0) as SubTotal
 from tblProjectSetup mas
left join (select COUNT(PMas.EmpInfoId) as Permnaent,   dtls.ProjectId from  tblEmployeeWiseProjectAllocationDetail dtls
           inner join tblEmployeeWiseProjectAllocationMaster PMas on dtls.EmployeeWiseProjectAllocationMasterId=PMas.EmpWiseProjectID
		   inner join tblEmpGeneralInfo EGI on PMas.EmpInfoId=EGI.EmpInfoId
		   where dtls.IsActive=1   and EGI.EmpTypeId=1
		   group by dtls.ProjectId
		   )  tblPermnaent on tblPermnaent.ProjectId=mas.ProjectId


		   left join (select COUNT(ISNULL(PMas.EmpInfoId,0)) as  Contractual,     dtls.ProjectId from  tblEmployeeWiseProjectAllocationDetail dtls
           inner join tblEmployeeWiseProjectAllocationMaster PMas on dtls.EmployeeWiseProjectAllocationMasterId=PMas.EmpWiseProjectID
		   inner join tblEmpGeneralInfo EGI on PMas.EmpInfoId=EGI.EmpInfoId
		   where dtls.IsActive=1   and EGI.EmpTypeId=2
		   group by dtls.ProjectId
		   )  tblContractual on tblContractual.ProjectId=mas.ProjectId
		   where mas.CompanyId=" + company;
          return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
      }


      
    }
}
