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

namespace DAL.AllPrintLetter_DAL
{
    public class MemoPromotionDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


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
        public DataTable LoadMemoPrintIncrementByMId(int id)
        {
            string query = @" SELECT * FROM tblMemoEmployeePromotion WHERE EmployeePromotionEntryId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable LoadMemoPrintIncrementDetailsByMId(int id)
        {
            string query = @"SELECT * FROM tblMemoEmployeePromotionDetails WHERE MemoEmployeePromotionId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
       
       


             public DataTable LoadMemoPrintGetEffectivedateIncrementByMId(int id)
        {
            string query = @" SELECT * FROM  tblEmployeePromotionEntry WHERE EmployeePromotionEntryId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable LoadEmpAllInfofById(int id)
        {
            string query = @"SELECT Inc.EmployeePromotionEntryId, Inc.CompanyId, Com.ShortName, Com.CompanyName, Inc.EmployeeId,Inc.EmployeeCode, Emp.EmpName, deg.Designation, Dpt.DepartmentName, Loc.SalaryLocation , 
Pgrade.GradeCode PSalaryGrade, Ngrade.GradeCode NSalaryGrade,
PStep.SalaryStepName PSalaryStepName, PStep.GrossAmount PGrossAmount, NStep.SalaryStepName NSalaryStepName, NStep.GrossAmount NGrossAmount, PDesignation.Designation PDesignation , 
NDesignation.Designation NDesignation, Inc.Effectivedate, Ptype.PromotionTypeName   NPromoType
   FROM tblEmployeePromotionEntry Inc
left JOIN dbo.tblEmpGeneralInfo  emp ON Inc.EmployeeId=Emp.EmpInfoId
left JOIN dbo.tblDesignation  deg ON emp.DesignationId=deg.DesignationId
left JOIN dbo.tblCompanyInfo  Com ON Inc.CompanyId=Com.CompanyId
left JOIN dbo.tblSalaryLocation  Loc ON Inc.SalaryLoationId=Loc.SalaryLoationId
left JOIN dbo.tblDepartment  Dpt ON Inc.DepartmentId=Dpt.DepartmentId
left JOIN dbo.tblSalaryGrade  Pgrade ON Inc.PSalGradeId=Pgrade.SalaryGradeId
left JOIN dbo.tblSalaryGrade  Ngrade ON Inc.NSalGradeId=Ngrade.SalaryGradeId
LEFT JOIN dbo.tblSalaryStep PStep ON Inc.PStepId=PStep.SalaryStepId
LEFT JOIN dbo.tblSalaryStep  NStep ON Inc.NSalaryStepId=NStep.SalaryStepId

left JOIN dbo.tblDesignation PDesignation ON Inc.PDesignationId=PDesignation.DesignationId
left JOIN dbo.tblDesignation  NDesignation ON Inc.NDesignationId=NDesignation.DesignationId
left JOIN dbo.tblPromotionType  Ptype ON Inc.NPromoTypeId=Ptype.PromotionTypeId

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
            string query = @"SELECT P.ParticularsId, P.Particulars,D.PreStepId, D.NewStepId FROM tblMemoIncrementDetails D
LEFT JOIN tblParticulars P ON D.ParticularsId=P.ParticularsId WHERE MemoIncrementId=" + Mid + "";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public bool DeleteMemoIncrementDetails(string id)
        {
            string query = "DELETE FROM tblMemoEmployeePromotionDetails WHERE MemoEmployeePromotionId='" + id +
                           "'";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
        }

        public Int32 MemoIncrementDetailsSaveInfo(MemoPrintIncrementDetailsDAO aEmpTransferAndDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MemoEmployeePromotionId", aEmpTransferAndDao.MemoEmployeePromotionId));
            aSqlParameterlist.Add(new SqlParameter("@PName", aEmpTransferAndDao.PName));
            aSqlParameterlist.Add(new SqlParameter("@PAmount", aEmpTransferAndDao.PAmount));



            string insertQuery = @"INSERT INTO [dbo].[tblMemoEmployeePromotionDetails]
           ([MemoEmployeePromotionId]
          
           ,[PName]
           ,[PAmount])
     VALUES
           (@MemoEmployeePromotionId, 
           
           @PName,  
           @PAmount )";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }
        public int SaveInfo(MemoPrintPromotionDAO aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@EmployeePromotionEntryId", (object)aDao.EmployeePromotionEntryId ?? DBNull.Value));
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
            aSqlParameterlist.Add(new SqlParameter("@ToEmployee", (object)aDao.ToEmployee ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@CompanyName", (object)aDao.CompanyName ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ThirdPara", (object)aDao.ThirdPara ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@FourthPara", (object)aDao.FourthPara ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Header", (object)aDao.Header ?? DBNull.Value));


            string query = @"INSERT INTO [dbo].[tblMemoEmployeePromotion]
           ([EmployeePromotionEntryId]
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
           ,[CopyTo], CompanyId, ToEmployee, CompanyName, ThirdPara,FourthPara , Header)
     VALUES
           (@EmployeePromotionEntryId, 
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
        @CopyTo, @CompanyId,@ToEmployee, @CompanyName, @ThirdPara, @FourthPara, @Header)";

            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }


        public bool UpdateInfo(MemoPrintPromotionDAO aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmployeePromotionEntryId", (object)aDao.EmployeePromotionEntryId ?? DBNull.Value));
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
            aSqlParameterlist.Add(new SqlParameter("@ToEmployee", (object)aDao.ToEmployee ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@CompanyName", (object)aDao.CompanyName ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@ThirdPara", (object)aDao.ThirdPara ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@FourthPara", (object)aDao.FourthPara ?? DBNull.Value));
            aSqlParameterlist.Add(new SqlParameter("@Header", (object)aDao.Header ?? DBNull.Value));

            string UpdateQuery = @"UPDATE [dbo].[tblMemoEmployeePromotion]
   SET   
     
        HeaderInfo= @HeaderInfo,
        HeaderDate= @HeaderDate,
       EmpCode=  @EmpCode, 
      EmpName=    @EmpName, 
        Designation=   @Designation,
        Department=  @Department,
    PlaceofPosting= @PlaceofPosting, 
        PreviousStep=  @PreviousStep, 
   IncrementalStep=   @IncrementalStep, 
        Salutation=  @Salutation, 
        FirstParagraph= @FirstParagraph, 
       ComplimentaryClose= @ComplimentaryClose, 
       YoursSincerely=  @YoursSincerely, 
    NAME=    @NAME, 
      CopyTo=  @CopyTo,CompanyId= @CompanyId,ToEmployee=@ToEmployee,CompanyName= @CompanyName, ThirdPara=@ThirdPara, FourthPara=@FourthPara, Header=@Header   WHERE EmployeePromotionEntryId=@EmployeePromotionEntryId";

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
