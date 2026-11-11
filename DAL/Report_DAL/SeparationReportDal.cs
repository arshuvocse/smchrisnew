using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.Report_DAL
{
    public class SeparationReportDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public void LoadCompanyDropDownList(DropDownList ddl)
        {
            try
            {
                string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
                aCommonInternalDal.LoadDropDownValueCompany(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
            }
            catch (Exception)
            {

                //throw;
            }
        }

        public DataTable GetAllDivision(string compId)
        {
            string queryStr = @"SELECT * FROM dbo.tblDivision  WITH (NOLOCK) WHERE IsActive='1' AND CompanyId='" + compId + "'";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllWing(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblDivisionWing  WITH (NOLOCK) 
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
  WHERE tblDivisionWing.IsActive='1' AND (Invisible='0' OR Invisible IS NULL) " + param + " ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllDepartment(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblDepartment  WITH (NOLOCK) 
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
WHERE tblDepartment.IsActive='1' AND (tblDepartment.Invisible='0' OR tblDepartment.Invisible IS NULL) " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllSection(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblSection  WITH (NOLOCK) 
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
WHERE tblSection.IsActive='1' AND (tblSection.Invisible='0' OR tblSection.Invisible IS NULL) " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable GetAllSubSection(string param)
        {
            string queryStr = @"SELECT * FROM dbo.tblSubSection  WITH (NOLOCK) 
LEFT JOIN dbo.tblSection ON tblSection.SectionId = tblSubSection.SectionId
LEFT JOIN dbo.tblDepartment ON tblDepartment.DepartmentId = tblSection.DepartmentId
LEFT JOIN dbo.tblDivisionWing ON tblDivisionWing.DivisionWId = tblDepartment.DivisionWId
LEFT JOIN dbo.tblDivision ON tblDivision.DivisionId = tblDivisionWing.DivisionId
WHERE tblSubSection.IsActive='1'  " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }


        public DataTable LoadInfoSeparationDAL(string param, string  Parameter2)
        {

            List<SqlParameter> aSqlParameters = new List<SqlParameter>();

            aSqlParameters.Add(new SqlParameter("@Parameter", param));
            aSqlParameters.Add(new SqlParameter("@Parameter2", Parameter2));
           
            return aCommonInternalDal.GetDataByStoreProcedure("sp_AccountsIntegrationSeperation", aSqlParameters, "HRDB");

//            string queryStr = @"SELECT  EG.ContractPeriod, EG.DateOfJoin,  EG.SMCOldCode, ST.GrossAmount,  ISNULL(ISNULL( 'Father: '+EG.FatherName,NULL) +ISNULL( +' , Mother: '+ EG.MotherName,NULL),'') ParentsInfo , rptBody.EmpName Supervisor, com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
//cat.EmpCategoryName  Category, SG.GradeCode+' : '+SG.GradeName Grade , ST.SalaryStepName Step, SL.SalaryLocation Office,
//JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.NationalIdNo, EG.BloodGroup,
//EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
//EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
//  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
//  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
//  CAST((DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate)  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate), EG.DateOfJoin) > EPE.JobLeftDate THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +
//
//CAST( MONTH(EPE.JobLeftDate - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +
//
//CAST(DAY(EPE.JobLeftDate - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate), EG.DateOfJoin) > EPE.JobLeftDate THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days' ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate ,
//   (CASE WHEN EIM.ExitMasterId
//IS NULL THEN 'Pending' ELSE 'Completed' END) AS ExitFormStatus,CAST((DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate)  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate), EG.DateOfJoin) > EPE.JobLeftDate THEN 1 ELSE 0 END)) AS NVARCHAR(max)) +' Years, ' +
//
//CAST( MONTH(EPE.JobLeftDate - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +
//
//CAST(DAY(EPE.JobLeftDate - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, EPE.JobLeftDate), EG.DateOfJoin) > EPE.JobLeftDate THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'  as LengthServicewithSMC, JType.JobLeftType, * From tblEmployeeJobLeft EPE  WITH (NOLOCK)
//                                    INNER JOIN dbo.tblEmpGeneralInfo  EG ON EPE.EmployeeId = EG.EmpInfoId
//                                    INNER JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
//                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
//                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
//                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
//                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId
//
//                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
//                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
//                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
//                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
//                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
//                               
//
//                                LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
//                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
//                                  LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 
//
//                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
//                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 
//
//								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
//                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 
//
//                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
//                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana                             
//						  LEFT JOIN dbo.tblUser  UpBY ON EPE.UpdateBy = UpBY.UserId   
//						  LEFT JOIN dbo.tblExitInterviewFormMaster EIM ON EIM.EmployeeId = EPE.EmployeeId  
//
//LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
//  INNER JOIN dbo.tblJobLeftType  JType ON EPE.JobLeftTypeId = JType.JobLeftTypeId 
//"
//                + HttpContext.Current.Session["finalup"] + " Where ( EPE.IsDelete IS NULL OR EPE.IsDelete = 0) " + param + "  ORDER BY EG.EmpMasterCode ASC";
            //return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
            
        }


        public DataTable LoadSystemLogDAL(string param)
        {

            List<SqlParameter> aSqlParameters = new List<SqlParameter>();

            aSqlParameters.Add(new SqlParameter("@Parameter", param));
          

            return aCommonInternalDal.GetDataByStoreProcedure("sp_SystemLogList", aSqlParameters, "HRDB");

           
        }

        public Int32 SaveSeperationConfirmationList(SeperationListDao aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aDao.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@EmpMasterCode", aDao.EmpMasterCode));
            aSqlParameterlist.Add(new SqlParameter("@ZID", aDao.ZID));
            aSqlParameterlist.Add(new SqlParameter("@SeperationDate", aDao.SeperationDate));
            aSqlParameterlist.Add(new SqlParameter("@SeperationTypeId", aDao.SeperationTypeId));
            aSqlParameterlist.Add(new SqlParameter("@Approveby", aDao.Approveby));
            aSqlParameterlist.Add(new SqlParameter("@ApproveDate", aDao.ApproveDate));


            string query = @"INSERT INTO tblAccountsIntegration_SeperationList
                           (EmpInfoId
           ,EmpMasterCode
           ,ZID
           ,SeperationDate
           ,SeperationTypeId
           ,Approveby
           ,ApproveDate
           )
           VALUES
           (
            @EmpInfoId
           ,@EmpMasterCode
           ,@ZID
           ,@SeperationDate
           ,@SeperationTypeId
           ,@Approveby
           ,@ApproveDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }
    }



}
