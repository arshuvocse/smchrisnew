using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using DAL.InternalCls;
using Library.DAO.HRM_Entities;


namespace Library.DAL.HRM_DAL
{
    public class EmpGeneralInfoDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        ClsApprovalAction approvalAction = new ClsApprovalAction();
        public void LoadApprovalControlDAL(RadioButtonList rdl, string pageName, string userName)
        {
            approvalAction.LoadActionControlByUser(rdl, pageName, userName);
        }
        public string LoadForApprovalConditionDAL(string pageName, string userName)
        {
            return approvalAction.LoadForApprovalByUserCondition(pageName, userName);
        }
        public bool SaveEmployeeInfo(EmpGeneralInfo aGeneralInfo)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", (object)aGeneralInfo.EmpInfoId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpMasterCode", (object)aGeneralInfo.EmpMasterCode ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpName", (object)aGeneralInfo.EmpName ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ShortName", (object)aGeneralInfo.ShortName ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@FatherName", (object)aGeneralInfo.FatherName ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MotherName", (object)aGeneralInfo.MotherName ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Religion", (object)aGeneralInfo.Religion ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Nationality", (object)aGeneralInfo.Nationality ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DateOfBirth", (object)aGeneralInfo.DateOfBirth ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@PlaceOfBirth", (object)aGeneralInfo.PlaceOfBirth ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@BloodGroup", (object)aGeneralInfo.BloodGroup ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Gender", (object)aGeneralInfo.Gender ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@AddressPresent", (object)aGeneralInfo.AddressPresent ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@AddressPermanent", (object)aGeneralInfo.AddressPermanent ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MedicalInformation", (object)aGeneralInfo.MedicalInformation ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@PhoneNo", (object)aGeneralInfo.PhoneNo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@CellNumber", (object)aGeneralInfo.CellNumber ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Email", (object)aGeneralInfo.Email ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MaritalStatus", (object)aGeneralInfo.MaritalStatus ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@NationalIdNo", (object)aGeneralInfo.NationalIdNo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SpouseName", (object)aGeneralInfo.SpouseName ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SpouseDateOfBirth", (object)aGeneralInfo.SpouseDateOfBirth ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@RefName", (object)aGeneralInfo.RefName ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@RefAddress", (object)aGeneralInfo.RefAddress ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@RefCellNo", (object)aGeneralInfo.RefCellNo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmergencycontactPerson", (object)aGeneralInfo.EmergencycontactPerson ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmergencycontactNumber", (object)aGeneralInfo.EmergencycontactNumber ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@CompanyInfoId", (object)aGeneralInfo.CompanyInfoId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@UnitId", (object)aGeneralInfo.UnitId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DivisionId", (object)aGeneralInfo.DivisionId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DesigId", (object)aGeneralInfo.DesigId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DepId", (object)aGeneralInfo.DepId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SectionId", (object)aGeneralInfo.SectionId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpGradeId", (object)aGeneralInfo.EmpGradeId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SalScaleId", (object)aGeneralInfo.SalScaleId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", (object)aGeneralInfo.EmpTypeId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ShiftId", (object)aGeneralInfo.ShiftId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@JoiningDate", (object)aGeneralInfo.JoiningDate ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Age", (object)aGeneralInfo.Age ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeStatus", (object)aGeneralInfo.EmployeeStatus ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@PayType", (object)aGeneralInfo.PayType ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ProbationPeriodTo", (object)aGeneralInfo.ProbationPeriodTo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ConfirmationDate", (object)aGeneralInfo.ConfirmationDate ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OTAllow", (object)aGeneralInfo.OTAllow ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@BankAccNo", (object)aGeneralInfo.BankAccNo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@BankId", (object)aGeneralInfo.BankId ?? DBNull.Value));
            //aSqlParameterlist.Add(new SqlParameter("@LineId", (object)aGeneralInfo.EmpMasterCode ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", (object)aGeneralInfo.Remarks ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ShiftEmp", (object)aGeneralInfo.ShiftEmp ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@NAge", (object)aGeneralInfo.NAge ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", (object)aGeneralInfo.ActionStatus ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", (object)aGeneralInfo.IsActive ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", (object)aGeneralInfo.EmpCategoryId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@CardNo", (object)aGeneralInfo.CardNo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", (object)aGeneralInfo.EntryDate ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", (object)aGeneralInfo.EntryBy ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@TINNo", (object)aGeneralInfo.TINNo ?? DBNull.Value));
            
            string insertQuery = @"insert into tblEmpGeneralInfo 
                                    (EmpInfoId,EmpMasterCode,EmpName,ShortName,FatherName,MotherName,Religion,Nationality,DateOfBirth,PlaceOfBirth,BloodGroup,Gender,AddressPresent,AddressPermanent,MedicalInformation,PhoneNo,CellNumber,Email,MaritalStatus,
                                     NationalIdNo,SpouseName,SpouseDateOfBirth,RefName,RefAddress,RefCellNo,EmergencycontactPerson,EmergencycontactNumber, CompanyInfoId,UnitId,DivisionId,DesigId,DepId,SectionId,EmpGradeId,SalScaleId,EmpTypeId,ShiftId,JoiningDate,Age,EmployeeStatus,PayType,ProbationPeriodTo,ConfirmationDate,OTAllow,BankAccNo,BankId,
                                     Remarks,ShiftEmp,NAge,ActionStatus,IsActive,EmpCategoryId,CardNo,EntryBy,EntryDate,TINNo) 
                                     values(@EmpInfoId,@EmpMasterCode,@EmpName,@ShortName,@FatherName,@MotherName,@Religion,@Nationality,@DateOfBirth,@PlaceOfBirth,@BloodGroup,@Gender,@AddressPresent,@AddressPermanent,@MedicalInformation,@PhoneNo,@CellNumber,@Email,@MaritalStatus,
                                     @NationalIdNo,@SpouseName,@SpouseDateOfBirth,@RefName,@RefAddress,@RefCellNo,@EmergencycontactPerson,@EmergencycontactNumber,@CompanyInfoId,@UnitId,@DivisionId,@DesigId,@DepId,@SectionId,@EmpGradeId,@SalScaleId,@EmpTypeId,@ShiftId,@JoiningDate,@Age,@EmployeeStatus,@PayType,@ProbationPeriodTo,@ConfirmationDate,@OTAllow,@BankAccNo,@BankId,
                                     @Remarks,@ShiftEmp,@NAge,@ActionStatus,@IsActive,@EmpCategoryId,@CardNo,@EntryBy,@EntryDate,@TINNo)";

            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, aSqlParameterlist, "HRDB");
        }

        public string EmpMasterCodeGeneratorDAL()
        {
            string id = "";
            string query = @"SELECT 'AG'+CONVERT(NVARCHAR(MAX),(ISNULL((MAX(CONVERT(INT,SUBSTRING(EmpMasterCode,3,10)))),1000)+1)) AS EmployeeMasterCode  FROM dbo.tblEmpGeneralInfo";
            id = aCommonInternalDal.DataContainerDataTable(query, "HRDB").Rows[0][0].ToString();
            return id;
        }
        public bool SaveEmpEducationInfo(EmpEducationInfo aEducationInfo)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpEduId", aEducationInfo.EmpEduId));
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aEducationInfo.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@BoardUniverName", aEducationInfo.BoardUniverName));
            aSqlParameterlist.Add(new SqlParameter("@PassingYear", aEducationInfo.PassYear));
            aSqlParameterlist.Add(new SqlParameter("@Qualification", aEducationInfo.Qualification));
            aSqlParameterlist.Add(new SqlParameter("@AreaStudy", aEducationInfo.AreaStudy));
            aSqlParameterlist.Add(new SqlParameter("@Result", aEducationInfo.Result));
            aSqlParameterlist.Add(new SqlParameter("@Exam", aEducationInfo.Exam));
            aSqlParameterlist.Add(new SqlParameter("@ResultType", aEducationInfo.ResultType));
            aSqlParameterlist.Add(new SqlParameter("@EduInstituteId", aEducationInfo.EduInstituteId));
            aSqlParameterlist.Add(new SqlParameter("@ExamId", aEducationInfo.ExamId));
            aSqlParameterlist.Add(new SqlParameter("@QualificationId", aEducationInfo.QualificationId));
            aSqlParameterlist.Add(new SqlParameter("@StudyId", aEducationInfo.StudyId));
            
            string insertQuery = @"insert into tblEmpEducation (EmpEduId,EmpInfoId,BoardUniverName,PassingYear,Qualification,AreaStudy,Result,Exam,ResultType,EduInstituteId,ExamId,QualificationId,StudyId) 
                                    values(@EmpEduId,@EmpInfoId,@BoardUniverName,@PassingYear,@Qualification,@AreaStudy,@Result,@Exam,@ResultType,@EduInstituteId,@ExamId,@QualificationId,@StudyId)";

            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, aSqlParameterlist, "HRDB");
        }

        public bool SaveEmpJobExperianceInfo(JobExperiancInfo aExperiancInfo)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@JobExpId", aExperiancInfo.JobExpId));
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aExperiancInfo.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyName", aExperiancInfo.CompanyName));
            aSqlParameterlist.Add(new SqlParameter("@Designation", aExperiancInfo.Designation));
            aSqlParameterlist.Add(new SqlParameter("@Department", aExperiancInfo.Department));
            aSqlParameterlist.Add(new SqlParameter("@FromDate", aExperiancInfo.FromDate));
            aSqlParameterlist.Add(new SqlParameter("@ToDate", aExperiancInfo.ToDate));
            aSqlParameterlist.Add(new SqlParameter("@Duration", aExperiancInfo.Duration));

            string insertQuery = @"insert into tblJobExperianc (JobExpId,EmpInfoId,CompanyName,Designation,Department,FromDate,ToDate,Duration) 
                                    values(@JobExpId,@EmpInfoId,@CompanyName,@Designation,@Department,@FromDate,@ToDate,@Duration)";

            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, aSqlParameterlist, "HRDB");
        }
        public bool DeleteEmpJobExperianceInfo(JobExperiancInfo aExperiancInfo)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aExperiancInfo.EmpInfoId));
            
            string insertQuery = @"DELETE FROM dbo.tblJobExperianc WHERE EmpInfoId=@EmpInfoId";

            return aCommonInternalDal.DeleteDataByDeleteCommand(insertQuery, aSqlParameterlist, "HRDB");
        }
        public bool DeleteEmpEducationInfo(EmpEducationInfo aEmpEducationInfo)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aEmpEducationInfo.EmpInfoId));

            string insertQuery = @"DELETE FROM dbo.tblEmpEducation WHERE EmpInfoId=@EmpInfoId";

            return aCommonInternalDal.DeleteDataByDeleteCommand(insertQuery, aSqlParameterlist, "HRDB");
        }
        public bool DeleteEmpTrainingeInfo(EmpTrainingInfo aTrainingInfo)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aTrainingInfo.EmpInfoId));
            string insertQuery = @"DELETE FROM dbo.tblTraining WHERE EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(insertQuery, aSqlParameterlist, "HRDB");
        }
        public bool SaveEmpTreningInfo(EmpTrainingInfo aTrainingInfo)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@TrainingId", aTrainingInfo.TrainingId));
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aTrainingInfo.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@TrainingName", aTrainingInfo.TrainingName));
            aSqlParameterlist.Add(new SqlParameter("@InstituteName", aTrainingInfo.InstituteName));
            aSqlParameterlist.Add(new SqlParameter("@Subject", aTrainingInfo.Subject));
            aSqlParameterlist.Add(new SqlParameter("@Duration", aTrainingInfo.Duration));
            aSqlParameterlist.Add(new SqlParameter("@Result", aTrainingInfo.Result));
            aSqlParameterlist.Add(new SqlParameter("@FromDate", aTrainingInfo.FromDate));
            aSqlParameterlist.Add(new SqlParameter("@ToDate", aTrainingInfo.ToDate));
            aSqlParameterlist.Add(new SqlParameter("@Country", aTrainingInfo.Country));

            string insertQuery = @"insert into tblTraining (TraningId,EmpInfoId,TrainingName,InstituteName,Subject,Duration,Result,FromDate,ToDate,Country) 
                                    values(@TrainingId,@EmpInfoId,@TrainingName,@InstituteName,@Subject,@Duration,@Result,@FromDate,@ToDate,@Country)";

            return aCommonInternalDal.SaveDataByInsertCommand(insertQuery, aSqlParameterlist, "HRDB");
        }
        public bool HasEmpGeneralInfo(EmpGeneralInfo aEmpGeneralInfo)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpName", aEmpGeneralInfo.EmpName));
            aSqlParameterlist.Add(new SqlParameter("@FatherName", aEmpGeneralInfo.FatherName));
            aSqlParameterlist.Add(new SqlParameter("@MotherName", aEmpGeneralInfo.MotherName));
            aSqlParameterlist.Add(new SqlParameter("@CellNumber", aEmpGeneralInfo.CellNumber));
            string query = "select * from tblEmpGeneralInfo where EmpName=@EmpName and FatherName=@FatherName and MotherName=@MotherName ";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, aSqlParameterlist, "HRDB");
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    return true;
                }
            }
            return false;
        }
        public bool UpdateEmpImage(EmpGeneralInfo aGeneralInfo)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aGeneralInfo.EmpInfoId));
            string query = @"UPDATE dbo.tblEmpGeneralInfo SET EmpImage=@EmpImage,SignatureImage=@SignatureImage  WHERE EmpInfoId=@EmpInfoId ";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }
        public DataTable LoadEmployeeViewDeptWise(string deptId)
        {
            string query = @"SELECT * FROM tblEmpGeneralInfo 
             LEFT JOIN tblDesignation ON tblEmpGeneralInfo.DesigId = tblDesignation.DesigId 
             LEFT JOIN tblDepartment ON tblEmpGeneralInfo.DepId = tblDepartment.DeptId 
             LEFT JOIN tblSection ON tblEmpGeneralInfo.SectionId = tblSection.SectionId 
             LEFT JOIN dbo.tblEmployeeGrade ON dbo.tblEmpGeneralInfo.EmpGradeId=dbo.tblEmployeeGrade.GradeId
             LEFT JOIN dbo.tblSalaryGradeOrScale ON dbo.tblEmpGeneralInfo.SalScaleId=dbo.tblSalaryGradeOrScale.SalScaleId
             LEFT JOIN tblEmployeeType ON tblEmpGeneralInfo.EmpTypeId = tblEmployeeType.EmpTypeId 
             LEFT JOIN
             (SELECT EmpInfoId,Amount FROM dbo.tblSalaryInformation WHERE ActionStatus in ('Posted') and IsActive=1 AND SalHeadName='Gross') AS tblSal
             ON tblEmpGeneralInfo.EmpInfoId=tblSal.EmpInfoId
             WHERE tblEmpGeneralInfo.ActionStatus in ('Posted','Cancel') and tblEmpGeneralInfo.IsActive=1 and tblEmpGeneralInfo.EmployeeStatus='Inactive' and tblDepartment.DeptId='" + deptId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

//        public DataTable LoadEmployeeViewDeptWise(string deptId)
//        {
//            string query = @"SELECT * FROM tblEmpGeneralInfo 
//             LEFT JOIN tblDesignation ON tblEmpGeneralInfo.DesigId = tblDesignation.DesigId 
//             LEFT JOIN tblDepartment ON tblEmpGeneralInfo.DepId = tblDepartment.DeptId 
//             LEFT JOIN tblSection ON tblEmpGeneralInfo.SectionId = tblSection.SectionId 
//             LEFT JOIN dbo.tblEmployeeGrade ON dbo.tblEmpGeneralInfo.EmpGradeId=dbo.tblEmployeeGrade.GradeId
//             LEFT JOIN dbo.tblSalaryGradeOrScale ON dbo.tblEmpGeneralInfo.SalScaleId=dbo.tblSalaryGradeOrScale.SalScaleId
//             LEFT JOIN tblEmployeeType ON tblEmpGeneralInfo.EmpTypeId = tblEmployeeType.EmpTypeId 
//             LEFT JOIN
//             (SELECT EmpInfoId,Amount FROM dbo.tblSalaryInformation WHERE ActionStatus in ('Posted') and IsActive=1 AND SalHeadName='Gross') AS tblSal
//             ON tblEmpGeneralInfo.EmpInfoId=tblSal.EmpInfoId
//             WHERE  tblDepartment.DeptId='" + deptId + "'";
//            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
//        }

        public DataTable LoadEmployeeViewDeptWiseForApproval()
        {
            string query = @"SELECT * FROM tblEmpGeneralInfo 
             LEFT JOIN tblDesignation ON tblEmpGeneralInfo.DesigId = tblDesignation.DesigId 
             LEFT JOIN tblDepartment ON tblEmpGeneralInfo.DepId = tblDepartment.DeptId 
             LEFT JOIN tblSection ON tblEmpGeneralInfo.SectionId = tblSection.SectionId 
             LEFT JOIN dbo.tblEmployeeGrade ON dbo.tblEmpGeneralInfo.EmpGradeId=dbo.tblEmployeeGrade.GradeId
             LEFT JOIN dbo.tblSalaryGradeOrScale ON dbo.tblEmpGeneralInfo.SalScaleId=dbo.tblSalaryGradeOrScale.SalScaleId
             LEFT JOIN tblEmployeeType ON tblEmpGeneralInfo.EmpTypeId = tblEmployeeType.EmpTypeId 
             INNER JOIN
             (SELECT EmpInfoId,Amount FROM dbo.tblSalaryInformation WHERE ActionStatus='Posted' AND SalHeadName='Gross' AND IsActive=1) AS tblSal
             ON tblEmpGeneralInfo.EmpInfoId=tblSal.EmpInfoId
             WHERE tblEmpGeneralInfo.ActionStatus ='Posted' and tblEmpGeneralInfo.IsActive=1  order by tblEmpGeneralInfo.EmpInfoId desc";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadEmployeeViewDeptWise()
        {
            string query = @"SELECT * FROM tblEmpGeneralInfo 
             LEFT JOIN tblDesignation ON tblEmpGeneralInfo.DesigId = tblDesignation.DesigId 
             LEFT JOIN tblDepartment ON tblEmpGeneralInfo.DepId = tblDepartment.DeptId 
             LEFT JOIN tblSection ON tblEmpGeneralInfo.SectionId = tblSection.SectionId 
             LEFT JOIN dbo.tblEmployeeGrade ON dbo.tblEmpGeneralInfo.EmpGradeId=dbo.tblEmployeeGrade.GradeId
             LEFT JOIN dbo.tblSalaryGradeOrScale ON dbo.tblEmpGeneralInfo.SalScaleId=dbo.tblSalaryGradeOrScale.SalScaleId
             LEFT JOIN tblEmployeeType ON tblEmpGeneralInfo.EmpTypeId = tblEmployeeType.EmpTypeId 
             INNER JOIN
             (SELECT EmpInfoId,Amount FROM dbo.tblSalaryInformation WHERE ActionStatus='Posted' AND SalHeadName='Gross' AND IsActive=1) AS tblSal
             ON tblEmpGeneralInfo.EmpInfoId=tblSal.EmpInfoId
             WHERE tblEmpGeneralInfo.ActionStatus in ('Posted','Cancel') and tblEmpGeneralInfo.IsActive=1 and tblEmpGeneralInfo.EntryBy='" + HttpContext.Current.Session["LoginName"].ToString() + "'  order by tblEmpGeneralInfo.EmpInfoId desc";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadEmployeeView()
        {
            string query = @"SELECT * FROM tblEmpGeneralInfo 
                           LEFT JOIN tblDesignation ON tblEmpGeneralInfo.DesigId = tblDesignation.DesigId 
                           LEFT JOIN tblDepartment ON tblEmpGeneralInfo.DepId = tblDepartment.DeptId 
                           LEFT JOIN tblSection ON tblEmpGeneralInfo.SectionId = tblSection.SectionId 
                           LEFT JOIN dbo.tblEmployeeGrade ON dbo.tblEmpGeneralInfo.EmpGradeId=dbo.tblEmployeeGrade.GradeId
                           LEFT JOIN dbo.tblSalaryGradeOrScale ON dbo.tblEmpGeneralInfo.SalScaleId=dbo.tblSalaryGradeOrScale.SalScaleId
                           LEFT JOIN tblEmployeeType ON tblEmpGeneralInfo.EmpTypeId = tblEmployeeType.EmpTypeId where tblEmpGeneralInfo.ActionStatus in ('Posted','Cancel') and tblEmpGeneralInfo.IsActive=1 and tblEmpGeneralInfo.EntryUser='" + HttpContext.Current.Session["LoginName"].ToString() + "'  order by tblEmpGeneralInfo.EmpInfoId desc ";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadEmployeeReportViewAll(string deptId)
        {
            string query = @"SELECT * FROM tblEmpGeneralInfo 
                           LEFT JOIN tblDepartment ON tblEmpGeneralInfo.DeptId = tblDepartment.DeptId  
                           LEFT JOIN tblSection ON tblEmpGeneralInfo.SectionId = tblSection.SectionId 
                           LEFT JOIN dbo.tblEmployeeGrade ON dbo.tblEmpGeneralInfo.GradeId=dbo.tblEmployeeGrade.GradeId
                           LEFT JOIN dbo.tblSalaryGradeOrScale ON dbo.tblEmpGeneralInfo.SalScaleId=dbo.tblSalaryGradeOrScale.SalScaleId
                           LEFT JOIN tblEmployeeType ON tblEmpGeneralInfo.EmpTypeId = tblEmployeeType.EmpTypeId  where dbo.tblEmpGeneralInfo.DeptId='" + deptId + "' AND tblEmpGeneralInfo.IsActive=1  ";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadEmployeeReportView(string EmpMasterCode,string CompanyInfoId)
        {
            string query = @"SELECT * FROM tblEmpGeneralInfo 
                           LEFT JOIN tblDepartment ON tblEmpGeneralInfo.DeptId = tblDepartment.DeptId  
                           LEFT JOIN tblSection ON tblEmpGeneralInfo.SectionId = tblSection.SectionId 
                           LEFT JOIN dbo.tblEmployeeGrade ON dbo.tblEmpGeneralInfo.GradeId=dbo.tblEmployeeGrade.GradeId
                           LEFT JOIN dbo.tblSalaryGradeOrScale ON dbo.tblEmpGeneralInfo.SalScaleId=dbo.tblSalaryGradeOrScale.SalScaleId
                           LEFT JOIN tblEmployeeType ON tblEmpGeneralInfo.EmpTypeId = tblEmployeeType.EmpTypeId  where dbo.tblEmpGeneralInfo.EmpMasterCode='" + EmpMasterCode + "' AND tblEmpGeneralInfo.IsActive=1";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadEmployee(string EmpMasterCode)
        {
            string query = @"SELECT * FROM tblEmpGeneralInfo where dbo.tblEmpGeneralInfo.EmpMasterCode='" + EmpMasterCode + "' AND tblEmpGeneralInfo.IsActive=1";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable searchID(string EmpMasterCode)
        {
            string query = @"SELECT * FROM tblEmpGeneralInfo where EmpMasterCode='" + EmpMasterCode + "' AND IsActive=1";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        
        public DataTable LoadEucation(string empid)
        {
            string query = @"SELECT BoardUniverName AS 'Institute', Exam, PassingYear,Qualification,AreaStudy,Result,ResultType,EduInstituteId,ExamId,QualificationId,StudyId FROM dbo.tblEmpEducation WHERE EmpInfoId='"+empid+"'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadWeeklyHoliday(string empid)
        {
            string query = @"SELECT * FROM dbo.tblEmpWeeklyHoliday WHERE EmpId='"+empid+"'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable LoadJobExp(string empid)
        {
            string query = @"SELECT CompanyName,Designation,FromDate,ToDate,Duration,Department FROM dbo.tblJobExperianc WHERE EmpInfoId='" + empid + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable LoadTraining(string empid)
        {
            string query = @"SELECT TrainingName,InstituteName,Subject,Duration,Result,FromDate,ToDate,Country FROM dbo.tblTraining WHERE EmpInfoId='"+empid+"'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable RptHeader()
        {
            string query = @"SELECT  RptAddress ,RptEmail ,RptFax ,RptHeader ,dbo.tblRptImage.RptImage ,RptMessage ,RptTel, 'Copyright Creatrix-'+CONVERT(NVARCHAR(MAX),DATEPART(YEAR,GETDATE()))+', All Rights are Reserved'  AS CopyRight FROM dbo.tblReportHeading
                            LEFT JOIN dbo.tblRptImage ON dbo.tblReportHeading.RptId = dbo.tblRptImage.RptId ";
         
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public void LoadBoardName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from dbo.tblEduInstitute ORDER BY EduInstituteName";
            aInternalDal.LoadDropDownValue(ddl, "EduInstituteName", "EduInstituteId", queryStr, "HRDB");
        }

        public void LoadQualificationName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from dbo.tblQualification ORDER BY Qualification";
            aInternalDal.LoadDropDownValue(ddl, "Qualification", "QualificationId", queryStr, "HRDB");
        }

        public void LoadExam(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from dbo.tblExam ORDER BY ExamName";
            aInternalDal.LoadDropDownValue(ddl, "ExamName", "ExamId", queryStr, "HRDB");
        }

        public void LoadCountry(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from dbo.tblCountry ORDER BY name ";
            aInternalDal.LoadDropDownValue(ddl, "name", "id", queryStr, "HRDB");
        }


        public void LoadAreaStudy(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from dbo.tblAreaofStudy ORDER BY AreaofStudy ";
            aInternalDal.LoadDropDownValue(ddl, "AreaofStudy", "StudyId", queryStr, "HRDB");
        }

        public void LoadDesignationName(DropDownList ddl,string empgradeId)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblDesignation  ORDER BY DesigName";
            aInternalDal.LoadDropDownValue(ddl, "DesigName", "DesigId", queryStr, "HRDB");
        }

        public void LoadDepartmentName(DropDownList ddl, string divisionId)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblDepartment where DivisionId='" + divisionId + "' ORDER BY DeptName ";
            aInternalDal.LoadDropDownValue(ddl, "DeptName", "DeptId", queryStr, "HRDB");
        }
        public void LoadEmployeeName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblEmpGeneralInfo";
            aInternalDal.LoadDropDownValue(ddl, "EmployeeName", "EmpInfoId", queryStr, "HRDB");
        }
        public void LoadCompanyName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblCompanyInfo";
            aInternalDal.LoadDropDownValue(ddl, "CompanyName", "CompanyInfoId", queryStr, "HRDB");
        }
        public void LoadUnitName(DropDownList ddl, string companyId)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblCompanyUnit where CompanyInfoId='" + companyId + "' ORDER BY UnitName ";
            aInternalDal.LoadDropDownValue(ddl, "UnitName", "UnitId", queryStr, "HRDB");
        }
        public void LoadDivisionName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblDivision ORDER BY DivName";
            aInternalDal.LoadDropDownValue(ddl, "DivName", "DivisionId", queryStr, "HRDB");
        }
        public void LoadSectionName(DropDownList ddl, string deptId)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblSection where DeptId='" + deptId + "'";
            aInternalDal.LoadDropDownValue(ddl, "SectionName", "SectionId", queryStr, "HRDB");
        }
        public void LoadEmpCategory(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblEmpCategory ";
            aInternalDal.LoadDropDownValue(ddl, "EmpCategoryName", "EmpCategoryId", queryStr, "HRDB");
        }
        public void LoadGradeName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblEmployeeGrade ORDER BY GradeName";
            aInternalDal.LoadDropDownValue(ddl, "GradeName", "GradeId", queryStr, "HRDB");
        }
        public void LoadSalaryScaleName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblSalaryGradeOrScale where SalScaleId in ('4','6') ORDER BY SalScaleName ";
            aInternalDal.LoadDropDownValue(ddl, "SalScaleName", "SalScaleId", queryStr, "HRDB");
        }
        public void LoadEmpTypeName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblEmployeeType ORDER BY EmpType ";
            aInternalDal.LoadDropDownValue(ddl, "EmpType", "EmpTypeId", queryStr, "HRDB");
        }

        public void LoadShift(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblShift";
            aInternalDal.LoadDropDownValue(ddl, "ShiftName", "ShiftId", queryStr, "HRDB");
        }
        
        public void LoadBankName(DropDownList ddl)
        {
            ClsCommonInternalDAL aInternalDal = new ClsCommonInternalDAL();
            string queryStr = "select * from tblBankInfo ORDER BY BankName ";
            aInternalDal.LoadDropDownValue(ddl, "BankName", "BankId", queryStr, "HRDB");
        }
        public EmpGeneralInfo EmpInfoEditLoad(string employeeId)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", employeeId));
            string query = "select * from tblEmpGeneralInfo where EmpInfoId = @EmpInfoId";
            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, aSqlParameterlist, "HRDB");

            EmpGeneralInfo aEmpGeneralInfo = new EmpGeneralInfo();
            if (dataReader != null)
            {
                while (dataReader.Read())
                {
                    aEmpGeneralInfo.EmpInfoId = Int32.Parse(dataReader["EmpInfoId"].ToString());
                    aEmpGeneralInfo.EmpMasterCode = dataReader["EmpMasterCode"].ToString();
                    aEmpGeneralInfo.EmpName = dataReader["EmpName"].ToString();
                    aEmpGeneralInfo.ShortName = dataReader["ShortName"].ToString();
                    aEmpGeneralInfo.FatherName = dataReader["FatherName"].ToString();
                    aEmpGeneralInfo.MotherName = dataReader["MotherName"].ToString();
                    aEmpGeneralInfo.Religion = dataReader["Religion"].ToString();
                    aEmpGeneralInfo.Nationality = dataReader["Nationality"].ToString();
                    aEmpGeneralInfo.DateOfBirth = Convert.ToDateTime(dataReader["DateOfBirth"].ToString());
                    aEmpGeneralInfo.PlaceOfBirth = dataReader["PlaceOfBirth"].ToString();
                    aEmpGeneralInfo.BloodGroup = dataReader["BloodGroup"].ToString();
                    aEmpGeneralInfo.Gender = dataReader["Gender"].ToString();
                    aEmpGeneralInfo.AddressPresent = dataReader["AddressPresent"].ToString();
                    aEmpGeneralInfo.AddressPermanent = dataReader["AddressPermanent"].ToString();
                    aEmpGeneralInfo.MedicalInformation = dataReader["MedicalInformation"].ToString();
                    aEmpGeneralInfo.PhoneNo = dataReader["PhoneNo"].ToString();
                    aEmpGeneralInfo.CellNumber = dataReader["CellNumber"].ToString();
                    aEmpGeneralInfo.Email = dataReader["Email"].ToString();
                    aEmpGeneralInfo.MaritalStatus = dataReader["MaritalStatus"].ToString();
                    aEmpGeneralInfo.NationalIdNo = dataReader["NationalIdNo"].ToString();
                    aEmpGeneralInfo.SpouseName = dataReader["SpouseName"].ToString();
                    aEmpGeneralInfo.SpouseDateOfBirth = dataReader["SpouseDateOfBirth"].ToString();
                    aEmpGeneralInfo.RefName = dataReader["RefName"].ToString();
                    aEmpGeneralInfo.RefAddress = dataReader["RefAddress"].ToString();
                    aEmpGeneralInfo.RefCellNo = dataReader["RefCellNo"].ToString();
                    aEmpGeneralInfo.DepId = Int32.Parse(dataReader["DepId"].ToString());
                    aEmpGeneralInfo.SectionId = Int32.Parse(dataReader["SectionId"].ToString());
                    aEmpGeneralInfo.CompanyInfoId = Convert.ToInt32((dataReader["CompanyInfoId"].ToString()));
                    aEmpGeneralInfo.UnitId = Convert.ToInt32(dataReader["UnitId"].ToString());
                    aEmpGeneralInfo.DivisionId = Convert.ToInt32(dataReader["DivisionId"].ToString());
                    aEmpGeneralInfo.EmpGradeId = Convert.ToInt32((dataReader["EmpGradeId"].ToString()));
                    aEmpGeneralInfo.SalScaleId = Convert.ToInt32((dataReader["SalScaleId"].ToString()));
                    aEmpGeneralInfo.DesigId = Convert.ToInt32((dataReader["DesigId"].ToString()));
                    aEmpGeneralInfo.EmpTypeId = Convert.ToInt32((dataReader["EmpTypeId"].ToString()));
                    aEmpGeneralInfo.JoiningDate = Convert.ToDateTime(dataReader["JoiningDate"].ToString());
                    aEmpGeneralInfo.ShiftId = Convert.ToInt32(dataReader["ShiftId"].ToString());
                    aEmpGeneralInfo.EmployeeStatus = dataReader["EmployeeStatus"].ToString();
                    aEmpGeneralInfo.PayType = dataReader["PayType"].ToString();
                    aEmpGeneralInfo.Age = dataReader["Age"].ToString();
                    aEmpGeneralInfo.ProbationPeriodTo = Convert.ToDateTime((dataReader["ProbationPeriodTo"].ToString()));
                    aEmpGeneralInfo.ConfirmationDate =Convert.ToDateTime((dataReader["ConfirmationDate"].ToString()));
                    aEmpGeneralInfo.OTAllow = dataReader["OTAllow"].ToString();
                    aEmpGeneralInfo.BankId = Convert.ToInt32(dataReader["BankId"].ToString());
                    aEmpGeneralInfo.BankAccNo = dataReader["BankAccNo"].ToString();
                    aEmpGeneralInfo.Remarks = dataReader["Remarks"].ToString();
                    aEmpGeneralInfo.EmpCategoryId = Convert.ToInt32(dataReader["EmpCategoryId"].ToString());
                    aEmpGeneralInfo.CardNo = dataReader["CardNo"].ToString();
                    aEmpGeneralInfo.ShiftEmployee = dataReader["ShiftEmp"].ToString();
                    aEmpGeneralInfo.EmergencycontactNumber = dataReader["EmergencycontactNumber"].ToString();
                    aEmpGeneralInfo.EmergencycontactPerson = dataReader["EmergencycontactPerson"].ToString();
                    aEmpGeneralInfo.TINNo = dataReader["TINNo"].ToString();
                    aEmpGeneralInfo.EntryDate = Convert.ToDateTime(dataReader["EntryDate"].ToString());
                }
            }
            return aEmpGeneralInfo;
        }
        public DataTable EmployeeTinReport(string parameter)
        {
            string query = @"SELECT E.EmpMasterCode,E.EmpName,DeptName,DesigName,E.JoiningDate ,tblG.Amount AS GrossSalary
,tblB.Amount AS BasicSal,E.TINNo,E.NationalIdNo,E.PayType,E.BankAccNo
FROM dbo.tblEmpGeneralInfo E
            LEFT JOIN dbo.tblCompanyInfo ON E.CompanyInfoId = dbo.tblCompanyInfo.CompanyInfoId
            LEFT JOIN dbo.tblCompanyUnit ON E.UnitId=dbo.tblCompanyUnit.UnitId
            LEFT JOIN dbo.tblDivision ON E.DivisionId=dbo.tblDivision.DivisionId
            LEFT JOIN dbo.tblDepartment ON E.DepId=dbo.tblDepartment.DeptId
            LEFT JOIN dbo.tblSection ON E.SectionId=dbo.tblSection.SectionId
            LEFT JOIN dbo.tblDesignation ON E.DesigId=dbo.tblDesignation.DesigId 
             LEFT JOIN (SELECT EmpInfoId,SalHeadName,Amount FROM dbo.tblSalaryInformation WHERE SalHeadName= 'Gross'
             and tblSalaryInformation.IsActive=1 )tblG
             ON E.EmpInfoId=tblG.EmpInfoId 
             
              LEFT JOIN (SELECT EmpInfoId,SalHeadName,Amount FROM dbo.tblSalaryInformation WHERE SalHeadName= 'Basic'
             and tblSalaryInformation.IsActive=1 )tblB
             ON E.EmpInfoId=tblB.EmpInfoId  ";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public void EmpApprovalDAL(string EmpInfoId, string joiningDate, string AppUser, DateTime appDate,string actionstatus)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", actionstatus));
            aSqlParameterlist.Add(new SqlParameter("@AppDate", appDate));

            if (actionstatus == "Accepted")
            {
                aSqlParameterlist.Add(new SqlParameter("@EmployeeStatus", "Active"));
            }
            else
            {
                aSqlParameterlist.Add(new SqlParameter("@EmployeeStatus", "Inactive"));
            }

            string query = @"UPDATE tblEmpGeneralInfo SET ActionStatus=@ActionStatus,ApprovalDate=@AppDate,EmployeeStatus=@EmployeeStatus where  EmpInfoId=@EmpInfoId and IsActive=1";
            aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
            
            List<SqlParameter> aSqlParameterlist1 = new List<SqlParameter>();
            aSqlParameterlist1.Add(new SqlParameter("@EmpInfoId", EmpInfoId));
            aSqlParameterlist1.Add(new SqlParameter("@JoiningDate", joiningDate));
            aSqlParameterlist1.Add(new SqlParameter("@AppUser", AppUser));
            aSqlParameterlist1.Add(new SqlParameter("@AppDate", appDate));
           
            string query1 = @" UPDATE dbo.tblSalaryInformation  SET ActionStatus='"+actionstatus+"',ActiveDate=@JoiningDate,ApprovedDate=@AppDate,ApprovedUser=@AppUser WHERE ActionStatus='Posted' AND EmpInfoId=@EmpInfoId and IsActive=1";
           aCommonInternalDal.UpdateDataByUpdateCommand(query1, aSqlParameterlist1, "HRDB");
        }
        public bool UpdateEmployeeInfo(EmpGeneralInfo aEmpGeneralInfo)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aEmpGeneralInfo.EmpInfoId));
            aSqlParameterlist.Add(new SqlParameter("@EmpName", aEmpGeneralInfo.EmpName));
            aSqlParameterlist.Add(new SqlParameter("@ShortName", aEmpGeneralInfo.ShortName));
            aSqlParameterlist.Add(new SqlParameter("@FatherName", aEmpGeneralInfo.FatherName));
            aSqlParameterlist.Add(new SqlParameter("@MotherName", aEmpGeneralInfo.MotherName));
            aSqlParameterlist.Add(new SqlParameter("@Religion", aEmpGeneralInfo.Religion));
            aSqlParameterlist.Add(new SqlParameter("@Nationality", aEmpGeneralInfo.Nationality));
            aSqlParameterlist.Add(new SqlParameter("@DateOfBirth", aEmpGeneralInfo.DateOfBirth));
            aSqlParameterlist.Add(new SqlParameter("@PlaceOfBirth", aEmpGeneralInfo.PlaceOfBirth));
            aSqlParameterlist.Add(new SqlParameter("@BloodGroup", aEmpGeneralInfo.BloodGroup));
            aSqlParameterlist.Add(new SqlParameter("@Gender", aEmpGeneralInfo.Gender));
            aSqlParameterlist.Add(new SqlParameter("@AddressPresent", aEmpGeneralInfo.AddressPresent));
            aSqlParameterlist.Add(new SqlParameter("@AddressPermanent", aEmpGeneralInfo.AddressPermanent));
            aSqlParameterlist.Add(new SqlParameter("@MedicalInformation", aEmpGeneralInfo.MedicalInformation));
            aSqlParameterlist.Add(new SqlParameter("@PhoneNo", aEmpGeneralInfo.PhoneNo));
            aSqlParameterlist.Add(new SqlParameter("@CellNumber", aEmpGeneralInfo.CellNumber));
            aSqlParameterlist.Add(new SqlParameter("@Email", aEmpGeneralInfo.Email));
            aSqlParameterlist.Add(new SqlParameter("@MaritalStatus", aEmpGeneralInfo.MaritalStatus));
            aSqlParameterlist.Add(new SqlParameter("@NationalIdNo", aEmpGeneralInfo.NationalIdNo));
            aSqlParameterlist.Add(new SqlParameter("@SpouseName", aEmpGeneralInfo.SpouseName));
            aSqlParameterlist.Add(new SqlParameter("@SpouseDateOfBirth", aEmpGeneralInfo.SpouseDateOfBirth));
            aSqlParameterlist.Add(new SqlParameter("@RefName", aEmpGeneralInfo.RefAddress));
            aSqlParameterlist.Add(new SqlParameter("@RefAddress", aEmpGeneralInfo.RefAddress));
            aSqlParameterlist.Add(new SqlParameter("@RefCellNo", aEmpGeneralInfo.RefCellNo));
            aSqlParameterlist.Add(new SqlParameter("@CompanyInfoId", aEmpGeneralInfo.CompanyInfoId));
            aSqlParameterlist.Add(new SqlParameter("@UnitId", aEmpGeneralInfo.UnitId));
            aSqlParameterlist.Add(new SqlParameter("@DivisionId", aEmpGeneralInfo.DivisionId));
            aSqlParameterlist.Add(new SqlParameter("@DesigId", aEmpGeneralInfo.DesigId));
            aSqlParameterlist.Add(new SqlParameter("@DepId", aEmpGeneralInfo.DepId));
            aSqlParameterlist.Add(new SqlParameter("@SectionId", aEmpGeneralInfo.SectionId));
            aSqlParameterlist.Add(new SqlParameter("@EmpGradeId", aEmpGeneralInfo.EmpGradeId));
            aSqlParameterlist.Add(new SqlParameter("@SalScaleId", aEmpGeneralInfo.SalScaleId));
            aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", aEmpGeneralInfo.EmpTypeId));
            aSqlParameterlist.Add(new SqlParameter("@ShiftId", aEmpGeneralInfo.ShiftId));
            aSqlParameterlist.Add(new SqlParameter("@JoiningDate", aEmpGeneralInfo.JoiningDate));
            aSqlParameterlist.Add(new SqlParameter("@Age", aEmpGeneralInfo.Age));
            //aSqlParameterlist.Add(new SqlParameter("@EmployeeStatus", aEmpGeneralInfo.EmployeeStatus));
            aSqlParameterlist.Add(new SqlParameter("@PayType", aEmpGeneralInfo.PayType));
            aSqlParameterlist.Add(new SqlParameter("@ProbationPeriodTo", aEmpGeneralInfo.ProbationPeriodTo));
            aSqlParameterlist.Add(new SqlParameter("@ConfirmationDate", aEmpGeneralInfo.ConfirmationDate));
            aSqlParameterlist.Add(new SqlParameter("@OtAllow", aEmpGeneralInfo.OTAllow));
            aSqlParameterlist.Add(new SqlParameter("@BankAccNo", aEmpGeneralInfo.BankAccNo));
            aSqlParameterlist.Add(new SqlParameter("@BankId", aEmpGeneralInfo.BankId));
            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", aEmpGeneralInfo.EmpCategoryId));
            aSqlParameterlist.Add(new SqlParameter("@CardNo", aEmpGeneralInfo.CardNo));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aEmpGeneralInfo.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@ShiftEmp", aEmpGeneralInfo.ShiftEmployee));
            aSqlParameterlist.Add(new SqlParameter("@EmergencycontactPerson", aEmpGeneralInfo.EmergencycontactPerson));
            aSqlParameterlist.Add(new SqlParameter("@EmergencycontactNumber", aEmpGeneralInfo.EmergencycontactNumber));
            aSqlParameterlist.Add(new SqlParameter("@TINNo", aEmpGeneralInfo.TINNo));
            
            string query = @"UPDATE tblEmpGeneralInfo SET EmpName=@EmpName,ShortName=@ShortName,FatherName=@FatherName,MotherName=@MotherName,Religion=@Religion,Nationality=@Nationality ,
                           DateOfBirth=@DateOfBirth,PlaceOfBirth=@PlaceOfBirth,BloodGroup=@BloodGroup,Gender=@Gender,AddressPresent=@AddressPresent,AddressPermanent=@AddressPermanent,MedicalInformation=@MedicalInformation ,
                           PhoneNo=@PhoneNo,CellNumber=@CellNumber,Email=@Email,MaritalStatus=@MaritalStatus,NationalIdNo=@NationalIdNo ,SpouseName=@SpouseName,SpouseDateOfBirth=@SpouseDateOfBirth, 
                           RefCellNo=@RefCellNo ,RefAddress=@RefAddress,RefName=@RefName ,CompanyInfoId=@CompanyInfoId ,UnitId=@UnitId ,DivisionId=@DivisionId ,DepId=@DepId ,SectionId=@SectionId ,
                           EmpGradeId=@EmpGradeId,SalScaleId=@SalScaleId,DesigId=@DesigId ,EmpTypeId=@EmpTypeId ,ShiftId=@ShiftId,JoiningDate=@JoiningDate , Age=@Age,PayType=@PayType,
                           ProbationPeriodTo=@ProbationPeriodTo,ConfirmationDate=@ConfirmationDate,OTAllow=@OTAllow,BankAccNo=@BankAccNo,BankId=@BankId,
                           Remarks=@Remarks ,ShiftEmp=@ShiftEmp,EmpCategoryId=@EmpCategoryId,CardNo=@CardNo,EmergencycontactPerson=@EmergencycontactPerson,EmergencycontactNumber=@EmergencycontactNumber,TINNo=@TINNo WHERE EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }
        public bool UpdateEmployeeInfoAlter(EmpGeneralInfo aGeneralInfo)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", (object)aGeneralInfo.EmpInfoId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpMasterCode", (object)aGeneralInfo.EmpMasterCode ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpName", (object)aGeneralInfo.EmpName ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ShortName", (object)aGeneralInfo.ShortName ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@FatherName", (object)aGeneralInfo.FatherName ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MotherName", (object)aGeneralInfo.MotherName ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Religion", (object)aGeneralInfo.Religion ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Nationality", (object)aGeneralInfo.Nationality ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DateOfBirth", (object)aGeneralInfo.DateOfBirth ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@PlaceOfBirth", (object)aGeneralInfo.PlaceOfBirth ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@BloodGroup", (object)aGeneralInfo.BloodGroup ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Gender", (object)aGeneralInfo.Gender ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@AddressPresent", (object)aGeneralInfo.AddressPresent ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@AddressPermanent", (object)aGeneralInfo.AddressPermanent ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MedicalInformation", (object)aGeneralInfo.MedicalInformation ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@PhoneNo", (object)aGeneralInfo.PhoneNo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@CellNumber", (object)aGeneralInfo.CellNumber ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Email", (object)aGeneralInfo.Email ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@MaritalStatus", (object)aGeneralInfo.MaritalStatus ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@NationalIdNo", (object)aGeneralInfo.NationalIdNo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SpouseName", (object)aGeneralInfo.SpouseName ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SpouseDateOfBirth", (object)aGeneralInfo.SpouseDateOfBirth ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@RefName", (object)aGeneralInfo.RefName ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@RefAddress", (object)aGeneralInfo.RefAddress ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@RefCellNo", (object)aGeneralInfo.RefCellNo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmergencycontactPerson", (object)aGeneralInfo.EmergencycontactPerson ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmergencycontactNumber", (object)aGeneralInfo.EmergencycontactNumber ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@CompanyInfoId", (object)aGeneralInfo.CompanyInfoId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@UnitId", (object)aGeneralInfo.UnitId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DivisionId", (object)aGeneralInfo.DivisionId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DesigId", (object)aGeneralInfo.DesigId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@DepId", (object)aGeneralInfo.DepId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SectionId", (object)aGeneralInfo.SectionId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpGradeId", (object)aGeneralInfo.EmpGradeId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SalScaleId", (object)aGeneralInfo.SalScaleId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", (object)aGeneralInfo.EmpTypeId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ShiftId", (object)aGeneralInfo.ShiftId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@JoiningDate", (object)aGeneralInfo.JoiningDate ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Age", (object)aGeneralInfo.Age ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeStatus", (object)aGeneralInfo.EmployeeStatus ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@PayType", (object)aGeneralInfo.PayType ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ProbationPeriodTo", (object)aGeneralInfo.ProbationPeriodTo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ConfirmationDate", (object)aGeneralInfo.ConfirmationDate ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@OTAllow", (object)aGeneralInfo.OTAllow ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@BankAccNo", (object)aGeneralInfo.BankAccNo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@BankId", (object)aGeneralInfo.BankId ?? DBNull.Value));
            //aSqlParameterlist.Add(new SqlParameter("@LineId", (object)aGeneralInfo.EmpMasterCode ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", (object)aGeneralInfo.Remarks ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ShiftEmp", (object)aGeneralInfo.ShiftEmp ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@NAge", (object)aGeneralInfo.NAge ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ActionStatus", (object)aGeneralInfo.ActionStatus ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", (object)aGeneralInfo.IsActive ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", (object)aGeneralInfo.EmpCategoryId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@CardNo", (object)aGeneralInfo.CardNo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", (object)aGeneralInfo.EntryDate ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", (object)aGeneralInfo.EntryBy ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@TINNo", (object)aGeneralInfo.TINNo ?? DBNull.Value));

            string query = @"UPDATE tblEmpGeneralInfo SET EmpName=@EmpName,ShortName=@ShortName,FatherName=@FatherName,MotherName=@MotherName,Religion=@Religion,Nationality=@Nationality ,
                           DateOfBirth=@DateOfBirth,PlaceOfBirth=@PlaceOfBirth,BloodGroup=@BloodGroup,Gender=@Gender,AddressPresent=@AddressPresent,AddressPermanent=@AddressPermanent,MedicalInformation=@MedicalInformation ,
                           PhoneNo=@PhoneNo,CellNumber=@CellNumber,Email=@Email,MaritalStatus=@MaritalStatus,NationalIdNo=@NationalIdNo ,SpouseName=@SpouseName,SpouseDateOfBirth=@SpouseDateOfBirth, 
                           RefCellNo=@RefCellNo ,RefAddress=@RefAddress,RefName=@RefName ,CompanyInfoId=@CompanyInfoId ,UnitId=@UnitId ,DivisionId=@DivisionId ,DepId=@DepId ,SectionId=@SectionId ,
                           EmpGradeId=@EmpGradeId,SalScaleId=@SalScaleId,DesigId=@DesigId ,EmpTypeId=@EmpTypeId ,ShiftId=@ShiftId,JoiningDate=@JoiningDate , Age=@Age,PayType=@PayType,
                           ProbationPeriodTo=@ProbationPeriodTo,ConfirmationDate=@ConfirmationDate,OTAllow=@OTAllow,BankAccNo=@BankAccNo,BankId=@BankId,
                           Remarks=@Remarks ,ShiftEmp=@ShiftEmp,EmpCategoryId=@EmpCategoryId,CardNo=@CardNo,EmergencycontactPerson=@EmergencycontactPerson,EmergencycontactNumber=@EmergencycontactNumber,TINNo=@TINNo WHERE EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }

        public bool UpdateWeeklyHoliday(WeeklyHoliday aWeeklyHoliday)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpId", aWeeklyHoliday.EmpId));
            aSqlParameterlist.Add(new SqlParameter("@FirstHolidayName", aWeeklyHoliday.FirstHolidayName));
            aSqlParameterlist.Add(new SqlParameter("@SecondHolidayName", aWeeklyHoliday.SecondHolidayName));
            aSqlParameterlist.Add(new SqlParameter("@DayQty", aWeeklyHoliday.DayQty));

            string query = @"UPDATE dbo.tblEmpWeeklyHoliday SET  FirstHolidayName=@FirstHolidayName,SecondHolidayName=@SecondHolidayName,DayQty=@DayQty WHERE EmpId=@EmpId ";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }

        public List<EmpGeneralInfo> ViewAllEmployee()
        {
            List<EmpGeneralInfo> allEmpGeneralInfoList = new List<EmpGeneralInfo>();
            string query = @"select * from tblEmpGeneralInfo";

            IDataReader dataReader = aCommonInternalDal.DataContainerDataReader(query, "HRDB");

            while (dataReader.Read())
            {
                EmpGeneralInfo aGeneralInfo = new EmpGeneralInfo();
                aGeneralInfo.EmpInfoId = Int32.Parse(dataReader["EmpInfoId"].ToString());
                aGeneralInfo.EmpMasterCode = (dataReader["EmpMasterCode"].ToString());
                aGeneralInfo.EmpName = dataReader["EmpName"].ToString();
                allEmpGeneralInfoList.Add(aGeneralInfo);
            }

            return allEmpGeneralInfoList;
        }

        public List<EmpGeneralInfo> ViewEmpName(string employeeId)
        {
            List<EmpGeneralInfo> singleEmpNameList = ViewAllEmployee();
            List<EmpGeneralInfo> singleEmpName = (from EmpGeneralInfo aGeneralInfo in singleEmpNameList
                                                  where aGeneralInfo.EmpMasterCode == employeeId
                                                  select aGeneralInfo).ToList();
            return singleEmpName;
        }

        public DataTable EmployeeReport(string parameter)
        {
            string query = @"SELECT * FROM dbo.tblEmpGeneralInfo
                            LEFT JOIN dbo.tblCompanyInfo ON dbo.tblEmpGeneralInfo.CompanyInfoId = dbo.tblCompanyInfo.CompanyInfoId
                            LEFT JOIN dbo.tblCompanyUnit ON dbo.tblEmpGeneralInfo.UnitId=dbo.tblCompanyUnit.UnitId
                            LEFT JOIN dbo.tblDivision ON dbo.tblEmpGeneralInfo.DivisionId=dbo.tblDivision.DivisionId
                            LEFT JOIN dbo.tblDepartment ON dbo.tblEmpGeneralInfo.DepId=dbo.tblDepartment.DeptId
                            LEFT JOIN dbo.tblSection ON dbo.tblEmpGeneralInfo.SectionId=dbo.tblSection.SectionId
                            LEFT JOIN dbo.tblEmployeeGrade ON dbo.tblEmpGeneralInfo.EmpGradeId=dbo.tblEmployeeGrade.GradeId
                            LEFT JOIN dbo.tblSalaryGradeOrScale ON dbo.tblEmpGeneralInfo.SalScaleId=dbo.tblSalaryGradeOrScale.SalScaleId
                            LEFT JOIN dbo.tblEmployeeType ON dbo.tblEmpGeneralInfo.EmpTypeId=dbo.tblEmployeeType.EmpTypeId
                            LEFT JOIN dbo.tblShift ON dbo.tblEmpGeneralInfo.ShiftId=dbo.tblShift.ShiftId
                            LEFT JOIN dbo.tblEmpImage ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmpImage.EmpInfoId
                              LEFT JOIN
                             (SELECT EmpInfoId,Amount FROM dbo.tblSalaryInformation WHERE ActionStatus='Accepted'  AND SalHeadName='Gross' and (InactiveDate is null or InactiveDate='')) AS tblSal
             ON tblEmpGeneralInfo.EmpInfoId=tblSal.EmpInfoId
                            LEFT JOIN dbo.tblDesignation ON dbo.tblEmpGeneralInfo.DesigId=dbo.tblDesignation.DesigId 
                   " + parameter + " ";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable EmployeeReportJobleft(string parameter)
        {
            string query = @"SELECT *,EffectiveDate as JobLeftDate FROM dbo.tblEmpGeneralInfo
                            LEFT JOIN dbo.tblCompanyInfo ON dbo.tblEmpGeneralInfo.CompanyInfoId = dbo.tblCompanyInfo.CompanyInfoId
                            LEFT JOIN dbo.tblCompanyUnit ON dbo.tblEmpGeneralInfo.UnitId=dbo.tblCompanyUnit.UnitId
                            LEFT JOIN dbo.tblDivision ON dbo.tblEmpGeneralInfo.DivisionId=dbo.tblDivision.DivisionId
                            LEFT JOIN dbo.tblDepartment ON dbo.tblEmpGeneralInfo.DepId=dbo.tblDepartment.DeptId
                            LEFT JOIN dbo.tblSection ON dbo.tblEmpGeneralInfo.SectionId=dbo.tblSection.SectionId
                            LEFT JOIN dbo.tblEmployeeGrade ON dbo.tblEmpGeneralInfo.EmpGradeId=dbo.tblEmployeeGrade.GradeId
                            LEFT JOIN dbo.tblSalaryGradeOrScale ON dbo.tblEmpGeneralInfo.SalScaleId=dbo.tblSalaryGradeOrScale.SalScaleId
                            LEFT JOIN dbo.tblEmployeeType ON dbo.tblEmpGeneralInfo.EmpTypeId=dbo.tblEmployeeType.EmpTypeId
                            LEFT JOIN dbo.tblShift ON dbo.tblEmpGeneralInfo.ShiftId=dbo.tblShift.ShiftId
                            LEFT JOIN dbo.tblEmpImage ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmpImage.EmpInfoId
                             
                            LEFT JOIN dbo.tblDesignation ON dbo.tblEmpGeneralInfo.DesigId=dbo.tblDesignation.DesigId 
                            LEFT JOIN dbo.tblJobleft ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblJobleft.EmpInfoId
                   " + parameter + " ";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable EmployeeDynamicReport(string whereparameter,string columnparameter)
        {
            string query = @"SELECT "+columnparameter+" FROM dbo.tblEmpGeneralInfo " +
                            " LEFT JOIN dbo.tblCompanyInfo ON dbo.tblEmpGeneralInfo.CompanyInfoId = dbo.tblCompanyInfo.CompanyInfoId"+
                            " LEFT JOIN dbo.tblCompanyUnit ON dbo.tblEmpGeneralInfo.UnitId=dbo.tblCompanyUnit.UnitId"+
                            " LEFT JOIN dbo.tblDivision ON dbo.tblEmpGeneralInfo.DivisionId=dbo.tblDivision.DivisionId"+
                            " LEFT JOIN dbo.tblDepartment ON dbo.tblEmpGeneralInfo.DepId=dbo.tblDepartment.DeptId"+
                            " LEFT JOIN dbo.tblSection ON dbo.tblEmpGeneralInfo.SectionId=dbo.tblSection.SectionId"+
                            " LEFT JOIN dbo.tblEmployeeGrade ON dbo.tblEmpGeneralInfo.EmpGradeId=dbo.tblEmployeeGrade.GradeId"+
                            " LEFT JOIN dbo.tblSalaryGradeOrScale ON dbo.tblEmpGeneralInfo.SalScaleId=dbo.tblSalaryGradeOrScale.SalScaleId"+
                            " LEFT JOIN dbo.tblEmployeeType ON dbo.tblEmpGeneralInfo.EmpTypeId=dbo.tblEmployeeType.EmpTypeId"+
                            " LEFT JOIN dbo.tblShift ON dbo.tblEmpGeneralInfo.ShiftId=dbo.tblShift.ShiftId"+
                            " LEFT JOIN dbo.tblEmpImage ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmpImage.EmpInfoId"+
                             " LEFT JOIN"+
                             " (SELECT EmpInfoId,Amount FROM dbo.tblSalaryInformation WHERE ActionStatus='Accepted'  AND SalHeadName='Gross' and (InactiveDate is null or InactiveDate='')) AS tblSal"+
             " ON tblEmpGeneralInfo.EmpInfoId=tblSal.EmpInfoId"+
                            " LEFT JOIN dbo.tblDesignation ON dbo.tblEmpGeneralInfo.DesigId=dbo.tblDesignation.DesigId "+
                   " "+whereparameter+"  ";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable EmployeeReportactive(string parameter)
        {
            string query = @"SELECT * FROM dbo.tblEmpGeneralInfo
                            LEFT JOIN dbo.tblCompanyInfo ON dbo.tblEmpGeneralInfo.CompanyInfoId = dbo.tblCompanyInfo.CompanyInfoId
                            LEFT JOIN dbo.tblCompanyUnit ON dbo.tblEmpGeneralInfo.UnitId=dbo.tblCompanyUnit.UnitId
                            LEFT JOIN dbo.tblDivision ON dbo.tblEmpGeneralInfo.DivisionId=dbo.tblDivision.DivisionId
                            LEFT JOIN dbo.tblDepartment ON dbo.tblEmpGeneralInfo.DepId=dbo.tblDepartment.DeptId
                            LEFT JOIN dbo.tblSection ON dbo.tblEmpGeneralInfo.SectionId=dbo.tblSection.SectionId
                            LEFT JOIN dbo.tblEmployeeGrade ON dbo.tblEmpGeneralInfo.EmpGradeId=dbo.tblEmployeeGrade.GradeId
                            LEFT JOIN dbo.tblSalaryGradeOrScale ON dbo.tblEmpGeneralInfo.SalScaleId=dbo.tblSalaryGradeOrScale.SalScaleId
                            LEFT JOIN dbo.tblEmployeeType ON dbo.tblEmpGeneralInfo.EmpTypeId=dbo.tblEmployeeType.EmpTypeId
                            LEFT JOIN dbo.tblShift ON dbo.tblEmpGeneralInfo.ShiftId=dbo.tblShift.ShiftId
                            LEFT JOIN dbo.tblEmpImage ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmpImage.EmpInfoId
                              LEFT JOIN
                             (SELECT EmpInfoId,Amount FROM dbo.tblSalaryInformation WHERE ActionStatus='Accepted'  AND SalHeadName='Gross' and (InactiveDate is null or InactiveDate='')) AS tblSal
             ON tblEmpGeneralInfo.EmpInfoId=tblSal.EmpInfoId
                            LEFT JOIN dbo.tblDesignation ON dbo.tblEmpGeneralInfo.DesigId=dbo.tblDesignation.DesigId 
                  where tblEmpGeneralInfo.EmployeeStatus = 'Active'    " + parameter + " ";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable EmployeeReportInactive(string parameter)
        {
            string query = @"SELECT * FROM dbo.tblEmpGeneralInfo
                            LEFT JOIN dbo.tblCompanyInfo ON dbo.tblEmpGeneralInfo.CompanyInfoId = dbo.tblCompanyInfo.CompanyInfoId
                            LEFT JOIN dbo.tblCompanyUnit ON dbo.tblEmpGeneralInfo.UnitId=dbo.tblCompanyUnit.UnitId
                            LEFT JOIN dbo.tblDivision ON dbo.tblEmpGeneralInfo.DivisionId=dbo.tblDivision.DivisionId
                            LEFT JOIN dbo.tblDepartment ON dbo.tblEmpGeneralInfo.DepId=dbo.tblDepartment.DeptId
                            LEFT JOIN dbo.tblSection ON dbo.tblEmpGeneralInfo.SectionId=dbo.tblSection.SectionId
                            LEFT JOIN dbo.tblEmployeeGrade ON dbo.tblEmpGeneralInfo.EmpGradeId=dbo.tblEmployeeGrade.GradeId
                            LEFT JOIN dbo.tblSalaryGradeOrScale ON dbo.tblEmpGeneralInfo.SalScaleId=dbo.tblSalaryGradeOrScale.SalScaleId
                            LEFT JOIN dbo.tblEmployeeType ON dbo.tblEmpGeneralInfo.EmpTypeId=dbo.tblEmployeeType.EmpTypeId
                            LEFT JOIN dbo.tblShift ON dbo.tblEmpGeneralInfo.ShiftId=dbo.tblShift.ShiftId
                            LEFT JOIN dbo.tblEmpImage ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmpImage.EmpInfoId
                              LEFT JOIN
                             (SELECT EmpInfoId,Amount FROM dbo.tblSalaryInformation WHERE ActionStatus='Accepted'  AND SalHeadName='Gross' and (InactiveDate is null or InactiveDate='')) AS tblSal
             ON tblEmpGeneralInfo.EmpInfoId=tblSal.EmpInfoId
                            LEFT JOIN dbo.tblDesignation ON dbo.tblEmpGeneralInfo.DesigId=dbo.tblDesignation.DesigId 
                  where tblEmpGeneralInfo.EmployeeStatus = 'Inactive'    " + parameter + " ";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable EmployeeReportPosted(string parameter)
        {
            string query = @"SELECT * FROM dbo.tblEmpGeneralInfo
                            LEFT JOIN dbo.tblCompanyInfo ON dbo.tblEmpGeneralInfo.CompanyInfoId = dbo.tblCompanyInfo.CompanyInfoId
                            LEFT JOIN dbo.tblCompanyUnit ON dbo.tblEmpGeneralInfo.UnitId=dbo.tblCompanyUnit.UnitId
                            LEFT JOIN dbo.tblDivision ON dbo.tblEmpGeneralInfo.DivisionId=dbo.tblDivision.DivisionId
                            LEFT JOIN dbo.tblDepartment ON dbo.tblEmpGeneralInfo.DepId=dbo.tblDepartment.DeptId
                            LEFT JOIN dbo.tblSection ON dbo.tblEmpGeneralInfo.SectionId=dbo.tblSection.SectionId
                            LEFT JOIN dbo.tblEmployeeGrade ON dbo.tblEmpGeneralInfo.EmpGradeId=dbo.tblEmployeeGrade.GradeId
                            LEFT JOIN dbo.tblSalaryGradeOrScale ON dbo.tblEmpGeneralInfo.SalScaleId=dbo.tblSalaryGradeOrScale.SalScaleId
                            LEFT JOIN dbo.tblEmployeeType ON dbo.tblEmpGeneralInfo.EmpTypeId=dbo.tblEmployeeType.EmpTypeId
                            LEFT JOIN dbo.tblShift ON dbo.tblEmpGeneralInfo.ShiftId=dbo.tblShift.ShiftId
                            LEFT JOIN dbo.tblEmpImage ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblEmpImage.EmpInfoId
                              LEFT JOIN
                             (SELECT EmpInfoId,Amount FROM dbo.tblSalaryInformation WHERE ActionStatus='Accepted'  AND SalHeadName='Gross' and (InactiveDate is null or InactiveDate='')) AS tblSal
             ON tblEmpGeneralInfo.EmpInfoId=tblSal.EmpInfoId
                            LEFT JOIN dbo.tblDesignation ON dbo.tblEmpGeneralInfo.DesigId=dbo.tblDesignation.DesigId 
                            where tblEmpGeneralInfo.ActionStatus ='Posted'" + parameter + " ";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable EmpSalaryInfo(string parameter)
        {
            string query = @"SELECT SalHeadName,Amount FROM dbo.tblSalaryInformation
                            LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblSalaryInformation.EmpInfoId = dbo.tblEmpGeneralInfo.EmpInfoId  " + parameter + " and (tblSalaryInformation.InactiveDate is null or tblSalaryInformation.InactiveDate='')";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable EmpEduInfo(string parameter)
        {
            string query = @"SELECT * FROM dbo.tblEmpEducation
                            LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpEducation.EmpInfoId = dbo.tblEmpGeneralInfo.EmpInfoId " + parameter + "";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable EmpJobInfo(string parameter)
        {
            string query = @"SELECT * FROM dbo.tblJobExperianc
                            LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblJobExperianc.EmpInfoId = dbo.tblEmpGeneralInfo.EmpInfoId " + parameter + "";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable EmpTrainInfo(string parameter)
        {
            string query = @"SELECT * FROM dbo.tblTraining 
                            LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblTraining.EmpInfoId = dbo.tblEmpGeneralInfo.EmpInfoId " + parameter + "";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public bool DeleteEmpInfo(string EmpInfoId)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", EmpInfoId));

            string query = @"DELETE FROM dbo.tblEmpGeneralInfo WHERE EmpInfoId=@EmpInfoId";
            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }
    }
}

