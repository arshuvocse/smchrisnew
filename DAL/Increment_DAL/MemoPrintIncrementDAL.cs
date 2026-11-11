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
    public class MemoPrintIncrementDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public DataTable DeleteValidattionForEffectiveDate(string id)
        {
            string query = @"SELECT * FROM  tblMemoIncrement with (nolock) where IncrementId= " + id;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
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
            const string queryStr = @"SELECT   empSig.ImagePath ImagePath,empSig.EmpSign EmpSign,     div.DivisionName, GD.GradeCode  GradeCode , LOWER(tblMemoIncrement.incrementalStep) SalaryStepName, 
FORMAT(HeaderDate, 'MMMM dd, yyyy') AS HeaderDate, empSig.EmpName SignaturePerson,    deg.Designation SignatureDesignation,
 com.CompanyName SignatureCompanyName,  * FROM tblMemoIncrement
LEFT JOIN  dbo.tblEmpGeneralInfo  empSig ON dbo.tblMemoIncrement.ToEmployee=empSig.EmpInfoId
LEFT JOIN  dbo.tblIncrement IncMas ON dbo.tblMemoIncrement.IncrementId=IncMas.IncrementId
 LEFT JOIN  dbo.tblEmpGeneralInfo emp ON IncMas.EmployeeId=emp.EmpInfoId

LEFT JOIN  dbo.tblDivision div ON div.DivisionId=emp.DivisionId
      LEFT JOIN dbo.tblSalaryGrade AS GD ON IncMas.SalaryGradeId = GD.SalaryGradeId
                             LEFT JOIN dbo.tblSalaryStep AS CSTP ON IncMas.CurrentStepId = CSTP.SalaryStepId
LEFT JOIN  dbo.tblDesignation deg ON deg.DesignationId=empSig.DesignationId

LEFT JOIN  dbo.tblCompanyInfo com ON com.CompanyId=empSig.CompanyId
  WHERE tblMemoIncrement.IncrementId=@IncrementId ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable GetMemoPrintIncrementInfoDALrptAll(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@IncrementId", Id));
            string queryStr = @"SELECT    div.DivisionName, GD.GradeCode  GradeCode , tblMemoIncrement.incrementalStep SalaryStepName, 
FORMAT(HeaderDate, 'MMMM dd, yyyy') AS HeaderDate, empSig.EmpName SignaturePerson,    deg.Designation SignatureDesignation,
 com.CompanyName SignatureCompanyName,  * FROM tblMemoIncrement
LEFT JOIN  dbo.tblEmpGeneralInfo  empSig ON dbo.tblMemoIncrement.ToEmployee=empSig.EmpInfoId
LEFT JOIN  dbo.tblIncrement IncMas ON dbo.tblMemoIncrement.IncrementId=IncMas.IncrementId
 LEFT JOIN  dbo.tblEmpGeneralInfo emp ON IncMas.EmployeeId=emp.EmpInfoId

LEFT JOIN  dbo.tblDivision div ON div.DivisionId=emp.DivisionId
      LEFT JOIN dbo.tblSalaryGrade AS GD ON IncMas.SalaryGradeId = GD.SalaryGradeId
                             LEFT JOIN dbo.tblSalaryStep AS CSTP ON IncMas.CurrentStepId = CSTP.SalaryStepId
LEFT JOIN  dbo.tblDesignation deg ON deg.DesignationId=empSig.DesignationId

LEFT JOIN  dbo.tblCompanyInfo com ON com.CompanyId=empSig.CompanyId
  WHERE tblMemoIncrement.IncrementId in(" +Id+")";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable GetMemoPrintPromotionInfoDALrpt(string Id)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@IncrementId", Id));
            const string queryStr = @"SELECT CONVERT(VARCHAR, HeaderDate, 107) AS HeaderDate, emp.EmpName SignaturePerson,  deg.Designation SignatureDesignation, com.CompanyName SignatureCompanyName,  * FROM tblMemoEmployeePromotion
LEFT JOIN  dbo.tblEmpGeneralInfo emp ON dbo.tblMemoEmployeePromotion.ToEmployee=emp.EmpInfoId
LEFT JOIN  dbo.tblDesignation deg ON deg.DesignationId=emp.DesignationId
LEFT JOIN  dbo.tblCompanyInfo com ON com.CompanyId=emp.CompanyId
  WHERE EmployeePromotionEntryId=@IncrementId ";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }


        public DataTable LoadParticularsDetailsrpt(string Mid)
        {
             
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MemoIncrementId", Mid));
            const string queryStr = @"SELECT  case when PName='Medical' then 'Medical allowance'  when PName='Conveyance' then 'Conveyance allowance'  else PName end PName,  ISNULL(FLOOR(tblMemoIncrementDetails.PAmountPre),0) as MemoIncrementDetailsId,  * FROM  tblMemoIncrementDetails  
WHERE     PName<>'Total' and MemoIncrementId=@MemoIncrementId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable LoadParticularsDetailsrpt_Total(string Mid)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MemoIncrementId", Mid));
            const string queryStr = @"SELECT * FROM  tblMemoIncrementDetails  
WHERE     PName='Total' and MemoIncrementId=@MemoIncrementId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable LoadParticularsDetailsrpt_TotalAmountInWord(int Mid)
        {
            ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();
            List<SqlParameter> aPeram = new List<SqlParameter>();

            aPeram.Add(new SqlParameter("@Amount", Mid));

            DataTable aTable = _aCommonInternalDal.GetDataByStoreProcedure("sp_GetAmountinWord", aPeram,
                DataBase.HRDB);
            return aTable;
        }

        public DataTable LoadParticularsDetailsrptALL(string Mid)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MemoIncrementId", Mid));
            string queryStr = @"SELECT  case when PName='Medical' then 'Medical allowance'  when PName='Conveyance' then 'Conveyance allowance'  else PName end PName,  ISNULL(FLOOR(tblMemoIncrementDetails.PAmountPre),0) as MemoIncrementDetailsId,  * FROM  tblMemoIncrementDetails  
WHERE     PName<>'Total' and MemoIncrementId in(" + Mid + ")";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable LoadParticularsPromotionDetailsrpt(string Mid)
        {

            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MemoIncrementId", Mid));
            const string queryStr = @"SELECT * FROM  tblMemoEmployeePromotionDetails  
WHERE MemoEmployeePromotionId=@MemoIncrementId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
        }
        public DataTable LoadMemoPrintIncrementByMId(int id)
        {
            string query = @" SELECT IncMas.EmployeeId, * FROM tblMemoIncrement MemInc
left join tblIncrement IncMas on IncMas.IncrementId=MemInc.IncrementId
where MemInc.IncrementId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable LoadParticularsGridView()
        {
            string query = @" SELECT 0 SalaryBreakUp, 0 as SalaryBreakUpPre, * FROM tblParticularsShow   with (nolock)";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable LoadMemoPrintIncrementDetailsByMId(int id)
        {
            string query = @"Select PName as ParticularsName,ROUND(PAmount,0)  SalaryBreakUp , 0 SalaryBreakUpPre,  * FROM tblMemoIncrementDetails WHERE MemoIncrementId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable LoadMemoPrintIncrementDetailsPreviousByMId(string EmpID)
        {
            string query = @" select  memDtl.PName, memDtl.PAmount  from tblMemoIncrement mem
inner join tblIncrement  inc on mem.IncrementId=inc.IncrementId
 inner join tblMemoIncrementDetails  memDtl on mem.IncrementId=memDtl.MemoIncrementId

where inc.EmployeeId in (" + EmpID + ") and inc.FinancialYearId in (11,12)";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable LoadSalaryStepGradeMId(int SalaryGradeId, int SalaryStepId)
        {
            string query = @"select * from tblSalaryStep   with (nolock) where SalaryGradeId='" + SalaryGradeId + "' AND SalaryStepId='" + SalaryStepId + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable LoadSalaryStepGradeMIdWithCode(int SalaryGradeId, int SalaryStepId)
        {
            string query = @"select salgrd.GradeCode, * from tblSalaryStep   with (nolock)
inner join tblSalaryGrade salgrd on salgrd.SalaryGradeId=tblSalaryStep.SalaryGradeId
 where tblSalaryStep.SalaryGradeId='" + SalaryGradeId + "' AND tblSalaryStep.SalaryStepId='" + SalaryStepId + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
        public DataTable CheckConveyanceByMasterCode(string MasterCode)
        {
            string query = @"SELECT * FROM tblEmpDontConveyance   with (nolock) WHERE EmpCode='" + MasterCode + "' ";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable LoadEmpSalarybyEmpCode(string MasterCode)
        {
            string query = @"SELECT * FROM dbo.tblEmpSalary   with (nolock) WHERE MasterCode='" + MasterCode + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

             public DataTable LoadMemoPrintGetEffectivedateIncrementByMId(int id)
        {
            string query = @" select  tblSalaryGrade.GradeCode,*  from tblIncrement  with (nolock)
left join tblSalaryGrade  with (nolock) on tblSalaryGrade.SalaryGradeId=tblIncrement.SalaryGradeId WHERE IncrementId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


          
             public DataTable LoadMemoPrintIDIncrementByMId(int id)
             {
                 string query = @"SELECT * FROM tblMemoIncrement WHERE IncrementId='" + id + "'";


                 return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
             }

//             public DataTable LoadMemoPrintGetEffectivedateIncrementByMId(int id)
//             {
//                 string query = @" select  tblSalaryGrade.GradeCode,*  from tblIncrement
//left join tblSalaryGrade on tblSalaryGrade.SalaryGradeId=tblIncrement.SalaryGradeId WHERE IncrementId='" + id + "'";


//                 return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
//             }
             public DataTable LoadSignaturePerson(int GradeId, int CompanyId)
             {
                 string query = @"select * from tblIncSignaturePerson   with (nolock) where GradeId ='" + GradeId + "' and CompanyId='" + CompanyId + "'";


                 return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
             }


             public DataTable LoadEmpName(int EmpInfoId)
             {
                 string query = @"select * from tblEmpGeneralInfo   with (nolock) where EmpInfoId ='" + EmpInfoId + "' ";


                 return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
             }

        public DataTable LoadEmpAllInfofById(int id)
        {
            string query = @"SELECT  div.DivisionName,case when emp.Gender='Male' then  'Mr. '+ emp.EmpName
when emp.Gender='FeMale' then  'Ms. '+  emp.EmpName else 'Mr./Ms. '+emp.EmpName end as EmpName, Emp.Gender,  Inc.IncrementId, Inc.CompanyId, Com.ShortName, Com.CompanyName, Inc.EmployeeId,Inc.EmployeeCode, Emp.EmpName, deg.Designation, Dpt.DepartmentName, Loc.SalaryLocation, CurrStep.SalaryStepName CurrentStep, IncreStep.SalaryStepName IncrementalStep,* FROM tblIncrement Inc
left JOIN dbo.tblEmpGeneralInfo  Emp ON Inc.EmployeeId=Emp.EmpInfoId 
left JOIN dbo.tblDesignation  deg ON Emp.DesignationId=deg.DesignationId
left JOIN dbo.tblDivision  div ON Emp.DivisionId=div.DivisionId
left JOIN dbo.tblCompanyInfo  Com ON Emp.CompanyId=Com.CompanyId
left JOIN dbo.tblSalaryLocation  Loc ON Emp.SalaryLoationId=Loc.SalaryLoationId
left JOIN dbo.tblDepartment  Dpt ON Emp.DepartmentId=Dpt.DepartmentId

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

        public Int32 MemoIncrementDetailsSaveInfo(MemoPrintIncrementDetailsDAO aEmpTransferAndDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@MemoIncrementId", aEmpTransferAndDao.MemoIncrementId));
            aSqlParameterlist.Add(new SqlParameter("@PName", aEmpTransferAndDao.PName));
            aSqlParameterlist.Add(new SqlParameter("@PAmount", aEmpTransferAndDao.PAmount));
            aSqlParameterlist.Add(new SqlParameter("@PAmountPre", aEmpTransferAndDao.PAmountPre));

          

            string insertQuery = @"INSERT INTO [dbo].[tblMemoIncrementDetails]
           ([MemoIncrementId]
          
           ,[PName]
           ,[PAmount],PAmountPre)
     VALUES
           (@MemoIncrementId, 
           
           @PName,  
           @PAmount,@PAmountPre )";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        } 
        public int SaveInfo(MemoPrintIncrementDAO aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();


            aSqlParameterlist.Add(new SqlParameter("@IncrementId", (object)aDao.IncrementId ?? DBNull.Value));
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
            aSqlParameterlist.Add(new SqlParameter("@Subject", (object)aDao.Subject ?? DBNull.Value));


            string query = @"INSERT INTO [dbo].[tblMemoIncrement]
           ([IncrementId]
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
           ,[CopyTo], CompanyId, ToEmployee, CompanyName, Subject)
     VALUES
           (@IncrementId, 
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
        @CopyTo, @CompanyId,@ToEmployee, @CompanyName, @Subject)";

            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }


        public bool UpdateInfo(MemoPrintIncrementDAO aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@IncrementId", (object)aDao.IncrementId ?? DBNull.Value));
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
            aSqlParameterlist.Add(new SqlParameter("@Subject", (object)aDao.Subject ?? DBNull.Value));
            string UpdateQuery = @"UPDATE [dbo].[tblMemoIncrement]
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
      ,[CopyTo] = @CopyTo, CompanyId=@CompanyId , ToEmployee=@ToEmployee, CompanyName=@CompanyName , Subject=@Subject  WHERE IncrementId=@IncrementId";

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
