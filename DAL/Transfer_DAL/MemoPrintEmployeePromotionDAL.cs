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
    public class MemoPrintEmployeePromotionDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

      

        public DataTable GetId(int ComId)
        {
            try
            {
                string query = @"SELECT COUNT(*)A  FROM dbo.tblMemoPrintEmployeePromotion WHERE CompanyId=" + ComId + " ";

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
            const string queryStr = @"SELECT CONVERT(VARCHAR, HeaderDate, 107) AS HeaderDate,* FROM tblMemoPrintEmployeePromotion  WHERE EmployeePromotionId=@EmployeePromotionId ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable LoadParticularsDetailsrpt(string Mid)
        {
             
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MemoEmployeePromotionId", Mid));
            const string queryStr = @"SELECT P.ParticularsId, P.Particulars,D.PreStepId, D.NewStepId FROM tblMemoPrintEmployeePromotionDetails D
LEFT JOIN tblParticulars P ON D.ParticularsId=P.ParticularsId WHERE MemoEmployeePromotionId=@MemoEmployeePromotionId ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable LoadMemoPrintIncrementByMId(int id)
        {
            string query = @" SELECT * FROM tblMemoPrintEmployeePromotion WHERE EmployeePromotionId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable LoadEmpAllInfofById(int id)
        {
            string query = @"SELECT Inc.EmployeePromotionEntryId, Inc.CompanyId, Com.ShortName, Inc.EmployeeId,Inc.EmployeeCode, Emp.EmpName, deg.Designation, Olddeg.Designation oldDesignation, Dpt.DepartmentName, Loc.SalaryLocation, CurrStep.SalaryStepName CurrentStep, IncreStep.SalaryStepName IncrementalStep,
 CurrGrade.GradeName CurrentGrade, IncreGrade.GradeName IncrementalGrade
 FROM dbo.tblEmployeePromotionEntry Inc
left JOIN dbo.tblDesignation  deg ON Inc.NDesignationId=deg.DesignationId
left JOIN dbo.tblDesignation  Olddeg ON Inc.PDesignationId=Olddeg.DesignationId
left JOIN dbo.tblCompanyInfo  Com ON Inc.CompanyId=Com.CompanyId
left JOIN dbo.tblSalaryLocation  Loc ON Inc.SalaryLoationId=Loc.SalaryLoationId
left JOIN dbo.tblDepartment  Dpt ON Inc.DepartmentId=Dpt.DepartmentId
left JOIN dbo.tblEmpGeneralInfo  Emp ON Inc.EmployeeId=Emp.EmpInfoId
left JOIN dbo.tblSalaryStep  CurrStep ON Inc.PStepId=CurrStep.SalaryStepId
left JOIN dbo.tblSalaryStep  IncreStep ON Inc.NSalaryStepId=IncreStep.SalaryStepId

left JOIN dbo.tblSalaryGrade  CurrGrade ON Inc.PSalGradeId=CurrGrade.SalaryGradeId
left JOIN dbo.tblSalaryGrade  IncreGrade ON Inc.NSalGradeId=IncreGrade.SalaryGradeId

WHERE Inc.EmployeePromotionEntryId='" + id + "'";


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
            string query = @"SELECT P.ParticularsId, P.Particulars,D.PreStepId, D.NewStepId FROM tblMemoPrintEmployeePromotionDetails D
LEFT JOIN tblParticulars P ON D.ParticularsId=P.ParticularsId WHERE MemoEmployeePromotionId=" + Mid + "";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public bool DeleteMemoIncrementDetails(string id)
        {
            string query = "DELETE FROM tblMemoPrintEmployeePromotionDetails WHERE MemoEmployeePromotionId='" + id +
                           "'";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
        }

        public Int32 MemoIncrementDetailsSaveInfo(MemoPrintEmployeePromotionDetailsDAO aEmpTransferAndDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MemoEmployeePromotionId", aEmpTransferAndDao.MemoEmployeePromotionId));
            aSqlParameterlist.Add(new SqlParameter("@ParticularsId", (object)aEmpTransferAndDao.ParticularsId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@PreStepId", (object)aEmpTransferAndDao.PreStepId ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@NewStepId", (object)aEmpTransferAndDao.NewStepId ?? DBNull.Value));

            string insertQuery = @"INSERT INTO [dbo].[tblMemoPrintEmployeePromotionDetails]
           ([MemoEmployeePromotionId]
           ,[ParticularsId]
           ,[PreStepId]
           ,[NewStepId])
     VALUES
           (@MemoEmployeePromotionId, 
           @ParticularsId, 
           @PreStepId,  
           @NewStepId )";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }
        public int SaveInfo(MemoPrintEmployeePromotionDAO aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@EmployeePromotionId", (object)aDao.EmployeePromotionId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@HeaderInfo", (object)aDao.HeaderInfo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@HeaderDate", (object)aDao.HeaderDate ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpCode", (object)aDao.EmpCode ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpName", (object)aDao.EmpName ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Designation", (object)aDao.Designation ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Department", (object)aDao.Department ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@PlaceofPosting", (object)aDao.PlaceofPosting ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@PreviousStep", (object)aDao.PreviousStep ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@IncrementalStep", (object)aDao.IncrementalStep ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@Salutation", (object)aDao.Salutation ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@FirstParagraph", (object)aDao.FirstParagraph ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ComplimentaryClose", (object)aDao.ComplimentaryClose ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@YoursSincerely", (object)aDao.YoursSincerely ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@NAME", (object)aDao.Name ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@CopyTo", (object)aDao.CopyTo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", (object)aDao.CompanyId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@PreviousSalaryGrade", (object)aDao.PreviousSalaryGrade ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@NewSalaryGrade", (object)aDao.NewSalaryGrade ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@PreviousDesignation", (object)aDao.PreviousDesignation ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@NewDesignation", (object)aDao.NewDesignation ?? DBNull.Value));


            string query = @"INSERT INTO [dbo].[tblMemoPrintEmployeePromotion]
           ([EmployeePromotionId]
           ,[HeaderInfo]
           ,[HeaderDate]
           ,[EmpCode]
           ,[EmpName]
           ,[Designation]
           ,[Department]
           ,[PlaceofPosting]
           ,[PreviousStep]
           ,[IncrementalStep]
           ,[Salutation]
           ,[FirstParagraph]
           ,[ComplimentaryClose]
           ,[YoursSincerely]
           ,[Name]
           ,[CopyTo], CompanyId, PreviousSalaryGrade, NewSalaryGrade, PreviousDesignation, NewDesignation)
     VALUES
           (@EmployeePromotionId, 
         @HeaderInfo,
         @HeaderDate,
         @EmpCode, 
          @EmpName, 
           @Designation,
          @Department,
     @PlaceofPosting, 
          @PreviousStep, 
      @IncrementalStep, 
          @Salutation, 
         @FirstParagraph, 
         @ComplimentaryClose, 
         @YoursSincerely, 
        @NAME, 
        @CopyTo, @CompanyId, @PreviousSalaryGrade, @NewSalaryGrade, @PreviousDesignation, @NewDesignation)";

            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }


        public bool UpdateInfo(MemoPrintEmployeePromotionDAO aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmployeePromotionId", (object)aDao.EmployeePromotionId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@HeaderInfo", (object)aDao.HeaderInfo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@HeaderDate", (object)aDao.HeaderDate ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpCode", (object)aDao.EmpCode ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@EmpName", (object)aDao.EmpName ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Designation", (object)aDao.Designation ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Department", (object)aDao.Department ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@PlaceofPosting", (object)aDao.PlaceofPosting ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@PreviousStep", (object)aDao.PreviousStep ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@IncrementalStep", (object)aDao.IncrementalStep ?? DBNull.Value));

            aSqlParameterlist.Add(new SqlParameter("@Salutation", (object)aDao.Salutation ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@FirstParagraph", (object)aDao.FirstParagraph ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ComplimentaryClose", (object)aDao.ComplimentaryClose ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@YoursSincerely", (object)aDao.YoursSincerely ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@NAME", (object)aDao.Name ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@CopyTo", (object)aDao.CopyTo ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", (object)aDao.CompanyId ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@PreviousSalaryGrade", (object)aDao.PreviousSalaryGrade ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@NewSalaryGrade", (object)aDao.NewSalaryGrade ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@PreviousDesignation", (object)aDao.PreviousDesignation ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@NewDesignation", (object)aDao.NewDesignation ?? DBNull.Value));


            string UpdateQuery = @"UPDATE [dbo].[tblMemoPrintEmployeePromotion]
   SET   
       [HeaderInfo] = @HeaderInfo 
      ,[HeaderDate] = @HeaderDate 
      ,[EmpCode] = @EmpCode 
      ,[EmpName] = @EmpName 
      ,[Designation] = @Designation 
      ,[Department] = @Department 
      ,[PlaceofPosting] = @PlaceofPosting 
      ,[PreviousStep] = @PreviousStep 
      ,[IncrementalStep] = @IncrementalStep 
      ,[Salutation] = @Salutation 
      ,[FirstParagraph] = @FirstParagraph 
      ,[ComplimentaryClose] = @ComplimentaryClose 
      ,[YoursSincerely] = @YoursSincerely 
      ,[Name] = @NAME 
      ,[CopyTo] = @CopyTo, CompanyId=@CompanyId, PreviousSalaryGrade=@PreviousSalaryGrade, NewSalaryGrade=@NewSalaryGrade, PreviousDesignation=@PreviousDesignation, NewDesignation=@NewDesignation    WHERE EmployeePromotionId=@EmployeePromotionId";

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
