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

namespace DAL.Increment_DAL
{
    public class MemoPrintJobCirculationDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public Int32 OtherRequirementsDetailSave(MemoOtherRequirementDetailDAO aOtherRequirementDetailDAO)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@MemoJobCreationId", aOtherRequirementDetailDAO.MemoJobCreationId));
            aSqlParameterlist.Add(new SqlParameter("@OtherRequirement", aOtherRequirementDetailDAO.OtherRequirement));

            string insertQuery = @"INSERT INTO [dbo].[MemoOtherRequirementDetail]
           ([MemoJobCreationId]
           ,[OtherRequirement])
     VALUES
           (@MemoJobCreationId,  
           @OtherRequirement)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");

        }
        public Int32 SaveJobReqKeyRespon(MemotblJobReqKeyResponDAO aJobReqKeyRespon)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@MemoJobCreationId", aJobReqKeyRespon.MemoJobCreationId));
            // aSqlParameterlist.Add(new SqlParameter("@KeyResId", aJobReqKeyRespon.KeyResId));
            aSqlParameterlist.Add(new SqlParameter("@JobReqKeyResName", aJobReqKeyRespon.JobReqKeyResName));

            string insertQuery = @"INSERT INTO [dbo].[MemotblJobReqKeyRespon]
           ([JobReqKeyResName]
           ,[MemoJobCreationId])
                VALUES
           (@JobReqKeyResName,  
           @MemoJobCreationId )";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");

        }
        public DataTable GetOtherRequirementsDetailId(string jobId)
        {
            string query = @" SELECT  ks.EducationRequirements  OtherRequirementsName FROM dbo.tblEducationRequirDesJobReq KRS
 INNER JOIN dbo.tblEducationRequirements ks ON ks.ERID=KRS.EduRequirId WHERE KRS.JobReqFormId='" + jobId + "'" +

" UNION ALL "+
" SELECT   Education AS OtherRequirement   FROM tblJobReqEducation   WHERE tblJobReqEducation.JobReqId='" + jobId + "'" +  
"  UNION ALL "+
     " SELECT (JRFM.Experience + JRFM.Skills + JRFM.Age + JRFM.OtherExperience) AS OtherRequirementsName FROM tblJobReqForm JRFM WHERE JRFM.JobReqId='" + jobId + "'" + 
 
   " UNION ALL "+
 " SELECT        OtherRequirement OtherRequirementsName FROM OtherRequirementDetail WHERE OtherRequirementDetail.MasterId='" + jobId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable UpGetOtherRequirementsDetailId(string jobId)
        {
            string query = @"SELECT   OtherRequirement as OtherRequirementsName, * FROM dbo.MemoOtherRequirementDetail
 
 WHERE MemoJobCreationId='" + jobId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadKeyResponseById(string id)
        {
            string query = @" SELECT * FROM tblJobReqKeyRespon WHERE JobReqFormId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable UpLoadKeyResponseById(string id)
        {
            string query = @" SELECT * FROM MemotblJobReqKeyRespon WHERE MemoJobCreationId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable GetJobResponsiblitiesByJobId(int jobId)
        {
            string query = @"   SELECT *, ks.JobLocationID, ks.Location  FROM dbo.tblKeyJobLocationCirculation KRS INNER JOIN dbo.tblJobCreation JRF ON KRS.JobCreationId=JRF.JobID
							INNER JOIN dbo.tblJobLocation ks ON ks.JobLocationID=KRS.JobLocationId WHERE JRF.JobID='" + jobId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable GetJobCreationInformationById(string id)
        {
            string query = @"SELECT  Det.DepartmentName, tblJobReqForm.JobTitle, tblJobReqForm.Nos, tblJobReqForm.JobReqId JobReqId, tblJobCreation.Position, dbo.tblJobCreation.JobCode, Com.ShortName  FROM dbo.tblJobCreation 
LEFT JOIN  tblJobReqForm ON tblJobCreation.ReqCodeId = tblJobReqForm.JobReqId
  left JOIN dbo.tblDepartment Det ON tblJobCreation.DepartmentID=Det.DepartmentId
    left JOIN dbo.tblCompanyInfo Com ON tblJobCreation.CompanyId=Com.CompanyId
  Where tblJobCreation.JobID='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable GetId(int ComId)
        {
            try
            {
                string query = @"SELECT COUNT(*)A  FROM dbo.tblMemoIncrement WHERE CompanyId=" + ComId + " ";

                return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public DataTable GetMemoPrintIncrementInfoDALrpt(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@IncrementId", Id));
            const string queryStr = @"SELECT CONVERT(VARCHAR, HeaderDate, 107) AS HeaderDate,* FROM tblMemoIncrement  WHERE IncrementId=@IncrementId ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable LoadParticularsDetailsrpt(string Mid)
        {
             
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MemoIncrementId", Mid));
            const string queryStr = @"SELECT P.ParticularsId, P.Particulars,D.PreStepId, D.NewStepId FROM tblMemoIncrementDetails D
LEFT JOIN tblParticulars P ON D.ParticularsId=P.ParticularsId WHERE MemoIncrementId=@MemoIncrementId ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable LoadMemoJobCreationByMId(int id)
        {
            string query = @" SELECT * FROM MemotblJobCreation WHERE JobCreationId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable rptLoadMemoJobCreationByMId(int id)
        {
            string query = @" SELECT * FROM MemotblJobCreation WHERE MemoJobCreationId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }



        public DataTable LoadEmpAllInfofById(int id)
        {
            string query = @"SELECT Inc.IncrementId, Inc.CompanyId, Com.ShortName, Inc.EmployeeId,Inc.EmployeeCode, Emp.EmpName, deg.Designation, Dpt.DepartmentName, Loc.SalaryLocation, CurrStep.SalaryStepName CurrentStep, IncreStep.SalaryStepName IncrementalStep FROM tblIncrement Inc
left JOIN dbo.tblDesignation  deg ON Inc.DesignationId=deg.DesignationId
left JOIN dbo.tblCompanyInfo  Com ON Inc.CompanyId=Com.CompanyId
left JOIN dbo.tblSalaryLocation  Loc ON Inc.SalaryLoationId=Loc.SalaryLoationId
left JOIN dbo.tblDepartment  Dpt ON Inc.DepartmentId=Dpt.DepartmentId
left JOIN dbo.tblEmpGeneralInfo  Emp ON Inc.EmployeeId=Emp.EmpInfoId
left JOIN dbo.tblSalaryStep  CurrStep ON Inc.CurrentStepId=CurrStep.SalaryStepId
left JOIN dbo.tblSalaryStep  IncreStep ON Inc.IncrementalStepId=IncreStep.SalaryStepId

WHERE Inc.IncrementId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }



        public DataTable LoadParticularsForEntry()
        {
            string query = @"																 SELECT *
FROM  tblParticulars
 ";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }



        public DataTable LoadParticularsForUpdate(string Mid)
        {
            string query = @"SELECT P.ParticularsId, P.Particulars,D.PreStepId, D.NewStepId FROM tblMemoIncrementDetails D
LEFT JOIN tblParticulars P ON D.ParticularsId=P.ParticularsId WHERE MemoIncrementId=" + Mid + "";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public bool DeleteMemoIncrementDetails(string id)
        {
            string query = "DELETE FROM tblMemoIncrementDetails WHERE MemoIncrementId='" + id +
                           "'";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
        }

        public bool DeleteMemoKeyResponDetails(string id)
        {
            string query = "DELETE FROM MemotblJobReqKeyRespon WHERE MemoJobCreationId='" + id +
                           "'";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
        }

        public bool DeleteMemoRequirementsDetails(string id)
        {
            string query = "DELETE FROM MemoOtherRequirementDetail WHERE MemoJobCreationId='" + id +
                           "'";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
        }

        public Int32 MemoIncrementDetailsSaveInfo(MemoPrintIncrementDetailsDAO aEmpTransferAndDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MemoIncrementId", aEmpTransferAndDao.MemoIncrementId));
            //aSqlParameterlist.Add(new SqlParameter("@ParticularsId", aEmpTransferAndDao.ParticularsId));
            //aSqlParameterlist.Add(new SqlParameter("@PreStepId", aEmpTransferAndDao.PreStepId));

            //aSqlParameterlist.Add(new SqlParameter("@NewStepId", aEmpTransferAndDao.NewStepId));

            string insertQuery = @"INSERT INTO [dbo].[tblMemoIncrementDetails]
           ([MemoIncrementId]
           ,[ParticularsId]
           ,[PreStepId]
           ,[NewStepId])
     VALUES
           (@MemoIncrementId, 
           @ParticularsId, 
           @PreStepId,  
           @NewStepId )";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }
        public int SaveInfo(MemotblJobCreationDAO aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@JobCreationId", (object)aDao.JobCreationId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@HeaderTitle", (object)aDao.HeaderTitle ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@HeaderDate", (object)aDao.HeaderDate ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@FirstTitle", (object)aDao.FirstTitle ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SecondTitle", (object)aDao.SecondTitle ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Positiontitle", (object)aDao.Positiontitle ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@WeOffer", (object)aDao.WeOffer ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@APPLYINSTRACTIONS", (object)aDao.APPLYINSTRACTIONS ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NameDesignation", (object)aDao.NameDesignation ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", (object)aDao.EntryBy ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", (object)aDao.EntryDate ?? DBNull.Value));




            string query = @"INSERT INTO [dbo].[MemotblJobCreation]
           ([JobCreationId]
           ,[HeaderTitle]
           ,[HeaderDate]
           ,[FirstTitle]
           ,[SecondTitle]
           ,[Positiontitle]
           ,[WeOffer]
           ,[APPLYINSTRACTIONS]
           ,[NameDesignation]
           ,[EntryBy]
           ,[EntryDate]
           )
     VALUES
           (@JobCreationId, 
           @HeaderTitle, 
           @HeaderDate, 
          @FirstTitle, 
           @SecondTitle,  
           @Positiontitle,  
           @WeOffer,  
           @APPLYINSTRACTIONS, 
           @NameDesignation,  
           @EntryBy,  
           @EntryDate 
           )";

            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }


        public bool UpdateInfo(MemotblJobCreationDAO aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MemoJobCreationId", (object)aDao.MemoJobCreationId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@JobCreationId", (object)aDao.JobCreationId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@HeaderTitle", (object)aDao.HeaderTitle ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@HeaderDate", (object)aDao.HeaderDate ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@FirstTitle", (object)aDao.FirstTitle ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@SecondTitle", (object)aDao.SecondTitle ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Positiontitle", (object)aDao.Positiontitle ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@WeOffer", (object)aDao.WeOffer ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@APPLYINSTRACTIONS", (object)aDao.APPLYINSTRACTIONS ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NameDesignation", (object)aDao.NameDesignation ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", (object)aDao.UpdateBy ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", (object)aDao.UpdateDate ?? DBNull.Value));

            string UpdateQuery = @"UPDATE [dbo].[MemotblJobCreation]
   SET [JobCreationId] = @JobCreationId, 
       [HeaderTitle] = @HeaderTitle, 
       [HeaderDate] = @HeaderDate,  
       [FirstTitle] = @FirstTitle, 
       [SecondTitle] = @SecondTitle,  
      [Positiontitle] = @Positiontitle, 
       [WeOffer] = @WeOffer,  
       [APPLYINSTRACTIONS] = @APPLYINSTRACTIONS,  
       [NameDesignation] = @NameDesignation, 
       [UpdateBy] = @UpdateBy, 
       [UpdateDate] = @UpdateDate 
 WHERE       MemoJobCreationId=@MemoJobCreationId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(UpdateQuery, aSqlParameterlist, "HRDB");
        }


        public DataTable LoadIncrementInfo(string parameter)
        {
            string query = @"SELECT INC.IncrementId,INC.EmployeeCode,E.EmpName,DSG.Designation,DPT.DepartmentName,CSTP.SalaryStepName AS PreviousStep, 
                             ISTP.SalaryStepName AS IncrementalStep, INC.EffectiveDate FROM dbo.tblIncrement AS INC
                             LEFT JOIN dbo.tblDesignation AS DSG ON INC.DesignationId = DSG.DesignationId
                             LEFT JOIN dbo.tblEmpGeneralInfo AS EGI ON INC.EmployeeId = EGI.EmpInfoId
                             LEFT JOIN dbo.tblDepartment AS DPT ON INC.DepartmentId = DPT.DepartmentId
                             LEFT JOIN dbo.tblSalaryGrade AS GD ON INC.SalaryGradeId = GD.SalaryGradeId
                             LEFT JOIN dbo.tblSalaryStep AS CSTP ON INC.CurrentStepId = CSTP.SalaryStepId
                             LEFT JOIN dbo.tblSalaryStep AS ISTP ON INC.IncrementalStepId = ISTP.SalaryStepId
							LEFT JOIN dbo.tblEmpGeneralInfo AS E ON INC.EmployeeId = E.EmpInfoId " + parameter;

            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public bool DeleteIncrementMaster(IncrementDao aDao)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@IncrementId", aDao.IncrementId));
            aSqlParameterlist.Add(new SqlParameter("@IsDelete", aDao.IsDelete));
            aSqlParameterlist.Add(new SqlParameter("@DeleteBy", aDao.DeleteBy));
            aSqlParameterlist.Add(new SqlParameter("@DeleteDate", aDao.DeleteDate));
            string query = @"Update  dbo.tblIncrement SET IsDelete =  @IsDelete
      ,DeleteBy =  @DeleteBy
      ,DeleteDate =  @DeleteDate WHERE IncrementId =@IncrementId ";

            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }
    }
}
