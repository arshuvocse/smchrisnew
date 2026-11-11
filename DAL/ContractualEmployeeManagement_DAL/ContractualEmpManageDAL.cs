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
using DAO.MeetingMinorsDAO;
using Library.DAO.HRM_Entities;

namespace DAL.ContractualEmployeeManagement_DAL
{
   public class ContractualEmpManageDAL
    {

       public bool SaveRoutingPathDetails(List<MiscellaneousInfoRoutingPathDAO> aList, int masterid)
       {
           try
           {
               List<SqlParameter> aParametersd = new List<SqlParameter>();
               aParametersd.Add(new SqlParameter("@ContractualEmpManageId", masterid));
               string queryDel = @"DELETE FROM [dbo].[tblContractualRoutingPath]
       WHERE  ContractualEmpManageId=@ContractualEmpManageId";

               bool delRes = aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParametersd, DataBase.HRDB);


               bool result = false;
               foreach (var item in aList)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();

                   aParameters.Add(new SqlParameter("@ContractualEmpManageId", masterid));
                   aParameters.Add(new SqlParameter("@EmpInfoId", item.EmpInfoId));
                   aParameters.Add(new SqlParameter("@Seq_No", item.Seq_No));
                   




                   string query = @"
INSERT INTO [dbo].[tblContractualRoutingPath]
           ([ContractualEmpManageId]
           ,[EmpInfoId]
           ,[Seq_No]
           )
     VALUES
           (@ContractualEmpManageId 
           ,@EmpInfoId 
           ,@Seq_No 
          )";
                   result = aCommonInternalDal.SaveDataByInsertCommand(query, aParameters, DataBase.HRDB);

                   if (result == false)
                   {
                       return false;
                   }


               }
               return result;


           }
           catch (Exception x)
           {

               throw;
           }
       }
       public int SaveContractualtionDetail(ProbationEvaluationDetailsDAO aEvaluationDetailsDao)
       {
           try
           {
               List<SqlParameter> aParameters = new List<SqlParameter>();

               aParameters.Add(new SqlParameter("@EmpInfoId", aEvaluationDetailsDao.EmpInfoId));
               aParameters.Add(new SqlParameter("@ContractualEmpManageId", aEvaluationDetailsDao.ContractualEmpManageId));
               aParameters.Add(new SqlParameter("@ValueField", aEvaluationDetailsDao.ValueField));
               aParameters.Add(new SqlParameter("@KeyRatingCri", aEvaluationDetailsDao.KeyRatingCri));
               aParameters.Add(new SqlParameter("@IsExcellent", aEvaluationDetailsDao.IsExcellent));
               aParameters.Add(new SqlParameter("@IsGood", aEvaluationDetailsDao.IsGood));
               aParameters.Add(new SqlParameter("@IsSatisfactory", aEvaluationDetailsDao.IsSatisfactory));
               aParameters.Add(new SqlParameter("@IsNotSatisfactory", aEvaluationDetailsDao.IsNotSatisfactory));

               string query = @"INSERT INTO [dbo].[tblContractualEvaluationDetails]
           ([EmpInfoId]
           ,[ContractualEmpManageId]
           ,[ValueField]
           ,[KeyRatingCri]
           ,[IsExcellent]
           ,[IsGood]
           ,[IsSatisfactory]
           ,[IsNotSatisfactory])
     VALUES
           (@EmpInfoId,
            @ContractualEmpManageId,
           @ValueField, 
           @KeyRatingCri, 
           @IsExcellent, 
           @IsGood,
           @IsSatisfactory, 
           @IsNotSatisfactory )";

               int pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
               return pk;
           }
           catch (Exception ex)
           {

               throw;
           }
       }

       public bool DeleteEvalution(int aMaster)
       {

           try
           {
               int pk = 0;

               // if (aMaster.ProbationEvaluationMasterId > 0)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@EmpInfoId", aMaster));

                   string query =
                       @"delete from [dbo].[tblContractualEvaluationDetails] where ContractualEmpManageId = @EmpInfoId";

                   bool result = aCommonInternalDal.DeleteDataByDeleteCommand(query, aParameters, DataBase.HRDB);
                   return result;

               }

           }
           catch (Exception exception)
           {

               throw exception;
           }
           return true;
       }

       public DataTable GetContractualEvaluationRating()
       {

           string queryStr = @"SELECT r.ContractualEvaluationRatingId as ValueField,r.TextField FROM dbo.tblContractualEvaluationRating r";
           return aCommonInternalDal.DataContainerDataTable(queryStr, null, DataBase.HRDB);
       }
       public int Save_Uplift(EmpGeneralInfo aInfo)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId",(object) aInfo.EmpInfoId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@CompanyInfoId", (object) aInfo.CompanyInfoId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DivisionId", (object)aInfo.DivisionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DivisionWId", (object)aInfo.DivisionWId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DepId", (object) aInfo.DepId ?? DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@SectionId", (object) aInfo.SectionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SubSectionId", (object)aInfo.SubSectionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", aInfo.EmpCategoryId));
           aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId",(object) aInfo.SalaryGradeId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryStepId", (object)aInfo.SalaryStepId ?? DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@DesigId", (object) aInfo.DesigId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DesignationTypeId", (object)aInfo.DesignationTypeId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", (object)aInfo.SalaryLoationId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@JobLocationId", (object)aInfo.JobLocationId ??DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@Floor", (object)aInfo.Floor ??DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EntryBy", HttpContext.Current.Session["UserId"].ToString()));


           aSqlParameterlist.Add(new SqlParameter("@IsSeparation", (object)aInfo.IsSeparation ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@JobLeftTypeId", (object)aInfo.JobLeftTypeId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SeparationDate", (object)aInfo.SeparationDate ?? DBNull.Value));


           const string queryStr = @"INSERT INTO [dbo].[tbl_Uplift]
           ([EmpInfoId]
           ,[Old_CompanyId]
           ,[New_CompanyId]
           ,[Old_DivisionId]
           ,[New_DivisionId]
           ,[Old_DivisionWId]
           ,[New_DivisionWId]
           ,[Old_DepartmentId]
           ,[New_DepartmentId]
           ,[Old_SectionId]
           ,[New_SectionId]
           ,[Old_SubSectionId]
           ,[New_SubSectionId]
           ,[Old_EmpCategoryId]
           ,[New_EmpCategoryId]
           ,[Old_SalaryGradeId]
           ,[New_SalaryGradeId]
           ,[Old_SalaryStepId]
           ,[New_SalaryStepId]
           ,[Old_DesignationId]
           ,[New_DesignationId]
           ,[Old_DesignationTypeId]
           ,[New_DesignationTypeId]
           ,[Old_SalaryLoationId]
           ,[New_SalaryLoationId]
           ,[Old_JobLocationId]
           ,[New_JobLocationId]
           ,[Old_Floor]
           ,[New_Floor]
           ,[EntryBy]
           ,[EntryDate],IsSeparation, JobLeftTypeId, SeparationDate)
      	   
SELECT  
       @EmpInfoId
	  ,[CompanyId]
      ,ISNULL(@CompanyInfoId,NULL)
	  ,[DivisionId] 
      ,ISNULL(@DivisionId,NULL)
	  ,[DivisionWId]
      ,ISNULL(@DivisionWId,NULL)
	  ,[DepartmentId]
      ,ISNULL(@DepId,NULL)
	  ,[SectionId]
      ,ISNULL(@SectionId,NULL)
	  ,[SubSectionId]
      ,ISNULL(@SubSectionId,NULL)
	  ,[EmpCategoryId]
      ,ISNULL(@EmpCategoryId,NULL)
	  ,[SalaryGradeId]
      ,ISNULL(@SalaryGradeId,NULL)
	  ,[SalaryStepId]
      ,ISNULL(@SalaryStepId,NULL)
	  ,[DesignationId]
      ,ISNULL(@DesigId,NULL) 
	  ,[DesignationTypeId]
      ,ISNULL(@DesignationTypeId,NULL)
	  ,[SalaryLoationId]
      ,ISNULL(@SalaryLoationId,NULL)
	  ,[JobLocationId]
      ,ISNULL(@JobLocationId,NULL)
	  ,[FLOOR]
	  ,ISNULL(@Floor,NULL)
      ,EntryBy
      ,GETDATE(),@IsSeparation, @JobLeftTypeId, @SeparationDate
      FROM [dbo].[tblEmpGeneralInfo] WHERE EmpInfoId=@EmpInfoId";

           return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
       }

       public int Save_UpliftForSupervisor(UpLiftEmpDAO aInfo, string ContractualEmpManageIdHid)
       {

           List<SqlParameter> aParameters2 = new List<SqlParameter>();
           aParameters2.Add(new SqlParameter("@ContractualEmpManageId", ContractualEmpManageIdHid));
           string queryDel = @"Delete from tbl_UpliftForSupervisor where ContractualEmpManageId = @ContractualEmpManageId";

           bool delQ = aCommonInternalDal.DeleteDataByDeleteCommand(queryDel, aParameters2, DataBase.HRDB);
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", (object)aInfo.EmpInfoId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@CompanyInfoId", (object)aInfo.CompanyInfoId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DivisionId", (object)aInfo.DivisionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DivisionWId", (object)aInfo.DivisionWId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DepId", (object)aInfo.DepId ?? DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@SectionId", (object)aInfo.SectionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SubSectionId", (object)aInfo.SubSectionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", aInfo.EmpCategoryId));
           aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", (object)aInfo.SalaryGradeId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryStepId", (object)aInfo.SalaryStepId ?? DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@DesigId", (object)aInfo.DesigId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DesignationTypeId", (object)aInfo.DesignationTypeId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", (object)aInfo.SalaryLoationId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@JobLocationId", (object)aInfo.JobLocationId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@Floor", (object)aInfo.Floor ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EntryBy", HttpContext.Current.Session["UserId"].ToString()));

           aSqlParameterlist.Add(new SqlParameter("@IsSeparation", (object)aInfo.IsSeparation ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsOrganization", (object)aInfo.IsOrganization ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsSalary", (object)aInfo.IsSalary ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsPlace", (object)aInfo.IsPlace ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsDesignation", (object)aInfo.IsDesignation ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@JobLeftTypeId", (object)aInfo.JobLeftTypeId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SeparationDate", (object)aInfo.SeparationDate ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@ContractualEmpManageId", (object) ContractualEmpManageIdHid ?? DBNull.Value));


           const string queryStr = @" INSERT INTO [dbo].[tbl_UpliftForSupervisor]
           ([EmpInfoId]
           ,[Old_CompanyId]
           ,[New_CompanyId]
           ,[Old_DivisionId]
           ,[New_DivisionId]
           ,[Old_DivisionWId]
           ,[New_DivisionWId]
           ,[Old_DepartmentId]
           ,[New_DepartmentId]
           ,[Old_SectionId]
           ,[New_SectionId]
           ,[Old_SubSectionId]
           ,[New_SubSectionId]
           ,[Old_EmpCategoryId]
           ,[New_EmpCategoryId]
           ,[Old_SalaryGradeId]
           ,[New_SalaryGradeId]
           ,[Old_SalaryStepId]
           ,[New_SalaryStepId]
           ,[Old_DesignationId]
           ,[New_DesignationId]
           ,[Old_DesignationTypeId]
           ,[New_DesignationTypeId]
           ,[Old_SalaryLoationId]
           ,[New_SalaryLoationId]
           ,[Old_JobLocationId]
           ,[New_JobLocationId]
           ,[Old_Floor]
           ,[New_Floor]
           ,[EntryBy]
           ,[EntryDate],IsSeparation, JobLeftTypeId, SeparationDate,IsOrganization,IsSalary,IsPlace,IsDesignation,ContractualEmpManageId)
      	   
SELECT  
       @EmpInfoId
	  ,[CompanyId]
      ,ISNULL(@CompanyInfoId,NULL)
	  ,[DivisionId] 
      ,ISNULL(@DivisionId,NULL)
	  ,[DivisionWId]
      ,ISNULL(@DivisionWId,NULL)
	  ,[DepartmentId]
      ,ISNULL(@DepId,NULL)
	  ,[SectionId]
      ,ISNULL(@SectionId,NULL)
	  ,[SubSectionId]
      ,ISNULL(@SubSectionId,NULL)
	  ,[EmpCategoryId]
      ,ISNULL(@EmpCategoryId,NULL)
	  ,[SalaryGradeId]
      ,ISNULL(@SalaryGradeId,NULL)
	  ,[SalaryStepId]
      ,ISNULL(@SalaryStepId,NULL)
	  ,[DesignationId]
      ,ISNULL(@DesigId,NULL) 
	  ,[DesignationTypeId]
      ,ISNULL(@DesignationTypeId,NULL)
	  ,[SalaryLoationId]
      ,ISNULL(@SalaryLoationId,NULL)
	  ,[JobLocationId]
      ,ISNULL(@JobLocationId,NULL)
	  ,[FLOOR]
	  ,ISNULL(@Floor,NULL)
      ,EntryBy
      ,GETDATE(),@IsSeparation, @JobLeftTypeId, @SeparationDate,@IsOrganization,@IsSalary,@IsPlace,@IsDesignation,@ContractualEmpManageId
      FROM [dbo].[tblEmpGeneralInfo] WHERE EmpInfoId=@EmpInfoId";

           return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable GetDataReviewEntryBy(string id, string entryby, string actionstatu)
       {
           //var aSqlParameterlist = new List<SqlParameter>();
           //aSqlParameterlist.Add(new SqlParameter("@Parameter", Parameter));


           string queryStr = @"SELECT * FROM dbo.tblContractualEmpManage WHERE ActionStatus='" + actionstatu + "' AND EntryBy='" + entryby + "' AND ContractualEmpManageId='" + id + "'";

           return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
       }
       public bool UpdateEmployeeContractEndDateInfo(EmpGeneralInfo aInfo)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           aSqlParameterlist.Add(new SqlParameter("@ContractStartDate", aInfo.ContractStartDate ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@ContractEndDate", aInfo.ContractEndDate ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aInfo.EmpInfoId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@ContractPeriod", aInfo.ContractPeriod ?? (object)DBNull.Value));


           aSqlParameterlist.Add(new SqlParameter("@Updateby", HttpContext.Current.Session["UserId"] ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@UpdateDate", DateTime.Now.ToShortDateString() ?? (object)DBNull.Value));




           string query = @"UPDATE tblEmpGeneralInfo SET ContractStartDate = @ContractStartDate, ContractEndDate = @ContractEndDate, ContractPeriod=@ContractPeriod, Updateby=@Updateby, UpdateDate=@UpdateDate  WHERE EmpInfoId=@EmpInfoId";
           return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, DataBase.HRDB);
       }

       public bool UpdateEmployePermanenttoContractualInfoEmpTypeID(EmpGeneralInfo aInfo)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           aSqlParameterlist.Add(new SqlParameter("@ContractEndDate", aInfo.ContractEndDate ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aInfo.EmpInfoId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", aInfo.EmpTypeId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@ContractPeriod", aInfo.ContractPeriod ?? (object)DBNull.Value));

           string query = @"UPDATE tblEmpGeneralInfo SET ContractEndDate = @ContractEndDate, EmpTypeId=@EmpTypeId, ContractPeriod=@ContractPeriod  WHERE EmpInfoId =  @EmpInfoId";
           return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, DataBase.HRDB);
       }

       public bool UpdateEmployeEmpTypeID(EmpGeneralInfo aInfo)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", aInfo.EmpTypeId ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aInfo.EmpInfoId ?? (object)DBNull.Value));
          

           string query = @"UPDATE tblEmpGeneralInfo SET   EmpTypeId=@EmpTypeId WHERE EmpInfoId =  @EmpInfoId";
           return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, DataBase.HRDB);
       }
       public DataTable LoadProject(int id)
       {
           string query = @"SELECT Pset.ProjectId, mas.EmpInfoId,  Pset.ProjectName, Pset.ProjectStartDate, Pset.ProjectEndDate,* FROM tblEmployeeWiseProjectAllocationMaster mas
LEFT JOIN tblEmployeeWiseProjectAllocationDetail Det ON  mas.EmpWiseProjectID=Det.EmployeeWiseProjectAllocationMasterId
LEFT JOIN dbo.tblProjectSetup Pset ON  Det.ProjectId=Pset.ProjectId  WHERE mas.EmpInfoId='" + id + "'";


           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
       public bool DeleteInfo(ContractualEmpManageDAO aContractualEmpManageDAO)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           aSqlParameterlist.Add(new SqlParameter("@ContractualEmpManageId",
               aContractualEmpManageDAO.ContractualEmpManageId));
           aSqlParameterlist.Add(new SqlParameter("@IsDelete", aContractualEmpManageDAO.IsDelete));
           aSqlParameterlist.Add(new SqlParameter("@DeleteBy", aContractualEmpManageDAO.DeleteBy));
           aSqlParameterlist.Add(new SqlParameter("@DeleteDate", aContractualEmpManageDAO.DeleteDate));
          

           
           string UpdateQuery = @"UPDATE [dbo].[tblContractualEmpManage]
   SET  IsDelete=@IsDelete,DeleteBy=@DeleteBy,DeleteDate=@DeleteDate
           WHERE ContractualEmpManageId=@ContractualEmpManageId
";



           return aCommonInternalDal.UpdateDataByUpdateCommand(UpdateQuery, aSqlParameterlist, "HRDB");
       }


       public bool DeleteUpdateEmployeEmpTypeID(EmpGeneralInfo aInfo)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           aSqlParameterlist.Add(new SqlParameter("@EmpMasterCode", aInfo.EmpMasterCode ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@ContractEndDate", aInfo.ContractEndDate ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@ContractPeriod", aInfo.ContractPeriod ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", aInfo.EmpTypeId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aInfo.EmpInfoId ?? (object)DBNull.Value));


           string query = @"UPDATE tblEmpGeneralInfo SET EmpMasterCode=@EmpMasterCode, ContractEndDate=@ContractEndDate, ContractPeriod=@ContractPeriod, EmpTypeId=@EmpTypeId WHERE EmpInfoId =  @EmpInfoId";
           return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, DataBase.HRDB);
       }

       public bool ContractualEmpManageUpdateInfo(ContractualEmpManageDAO aContractualEmpManageDAO)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           aSqlParameterlist.Add(new SqlParameter("@ContractualEmpManageId", aContractualEmpManageDAO.ContractualEmpManageId));
           aSqlParameterlist.Add(new SqlParameter("@CompanyId", aContractualEmpManageDAO.CompanyId));
           aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aContractualEmpManageDAO.EmployeeId));
           aSqlParameterlist.Add(new SqlParameter("@IsExtension", aContractualEmpManageDAO.IsExtension ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@IsRenew", aContractualEmpManageDAO.IsRenew ?? (object)DBNull.Value));


           aSqlParameterlist.Add(new SqlParameter("@IsPermanentToContractual", aContractualEmpManageDAO.IsPermanentToContractual ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsContractualToPermanent", aContractualEmpManageDAO.IsContractualToPermanent ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@ExtensionFromDate", aContractualEmpManageDAO.ExtensionFromDate ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@ExtensionToDate", aContractualEmpManageDAO.ExtensionToDate ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@PermanentToContractualEndDate", aContractualEmpManageDAO.PermanentToContractualEndDate ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@RenewStartDate", aContractualEmpManageDAO.RenewStartDate ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@RenewToDate", aContractualEmpManageDAO.RenewToDate ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@PermanentToContractualEffectiveDate", aContractualEmpManageDAO.PermanentToContractualEffectiveDate ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@ContractualToPermanentDate", aContractualEmpManageDAO.ContractualToPermanentDate ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@IsSMCContracttoSMCFundedProjects", aContractualEmpManageDAO.IsSMCContracttoSMCFundedProjects ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsSMCFundedProjectstoSMCContract", aContractualEmpManageDAO.IsSMCFundedProjectstoSMCContract ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@IsSalaryIncrement", aContractualEmpManageDAO.IsSalaryIncrement ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@IsNoIncrement", aContractualEmpManageDAO.IsNoIncrement ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsFacilityIncluded", aContractualEmpManageDAO.IsFacilityIncluded ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@IsNoFacility", aContractualEmpManageDAO.IsNoFacility ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@Remarks", aContractualEmpManageDAO.Remarks));

           aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", aContractualEmpManageDAO.EffectiveDate ?? (object)DBNull.Value));


           aSqlParameterlist.Add(new SqlParameter("@UpdateBy", HttpContext.Current.Session["UserId"].ToString()));
           aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aContractualEmpManageDAO.UpdateDate));




           aSqlParameterlist.Add(new SqlParameter("@DesignationId", (object)aContractualEmpManageDAO.DesignationId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DivisionId", (object)aContractualEmpManageDAO.DivisionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DivisionWId", (object)aContractualEmpManageDAO.DivisionWId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DepartmentId", (object)aContractualEmpManageDAO.DepartmentId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SectionId", (object)aContractualEmpManageDAO.SectionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SubSectionId", (object)aContractualEmpManageDAO.SubSectionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", (object)aContractualEmpManageDAO.SalaryLoationId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@JobLocationId", (object)aContractualEmpManageDAO.JobLocationId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", (object)aContractualEmpManageDAO.EmpTypeId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmployeeCode", (object)aContractualEmpManageDAO.EmployeeCode ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@AutoProcess", (object)aContractualEmpManageDAO.AutoProcess ?? DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", (object)aContractualEmpManageDAO.SalaryGradeId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryStepId", (object)aContractualEmpManageDAO.SalaryStepId ?? DBNull.Value));
         aSqlParameterlist.Add(new SqlParameter("@ContractPreiod", (object)aContractualEmpManageDAO.ContractPreiod ?? DBNull.Value));
          
           //aSqlParameterlist.Add(new SqlParameter("@ActionStatus", "Drafted"));
           string UpdateQuery = @"UPDATE [dbo].[tblContractualEmpManage]
   SET  CompanyId=@CompanyId,EmployeeId=@EmployeeId,IsExtension=@IsExtension, PermanentToContractualEndDate=@PermanentToContractualEndDate,
            IsRenew=@IsRenew
           ,IsPermanentToContractual=@IsPermanentToContractual
           ,IsContractualToPermanent=@IsContractualToPermanent
 ,ExtensionFromDate=@ExtensionFromDate
           ,ExtensionToDate=@ExtensionToDate
           ,IsSalaryIncrement=@IsSalaryIncrement
           ,IsNoIncrement=@IsNoIncrement
           ,IsFacilityIncluded=@IsFacilityIncluded
           ,IsNoFacility=@IsNoFacility
           ,Remarks=@Remarks
         , RenewStartDate=@RenewStartDate, RenewToDate=@RenewToDate, PermanentToContractualEffectiveDate=@PermanentToContractualEffectiveDate, ContractualToPermanentDate=@ContractualToPermanentDate, UpdateBy=@UpdateBy,UpdateDate=@UpdateDate,
 EffectiveDate=@EffectiveDate,  DivisionId=@DivisionId, DivisionWId=@DivisionWId, DepartmentId=@DepartmentId,  SectionId=@SectionId, SubSectionId=@SubSectionId, DesignationId=@DesignationId,SalaryLoationId=@SalaryLoationId, JobLocationId=@JobLocationId , EmpTypeId=@EmpTypeId,EmployeeCode=@EmployeeCode, AutoProcess=@AutoProcess, SalaryGradeId=@SalaryGradeId,
SalaryStepId=@SalaryStepId, IsSMCContracttoSMCFundedProjects=@IsSMCContracttoSMCFundedProjects, IsSMCFundedProjectstoSMCContract=@IsSMCFundedProjectstoSMCContract,ContractPreiod=@ContractPreiod
WHERE ContractualEmpManageId=@ContractualEmpManageId
";

         

           return aCommonInternalDal.UpdateDataByUpdateCommand(UpdateQuery, aSqlParameterlist, "HRDB");
       }

       public bool UpdateRenewEndDateChange(long? EmpInfoId, DateTime? RenewStartDate, DateTime? ContractEndDate, int? ContractPeriod, int? Updateby)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", EmpInfoId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@ContractStartDate", RenewStartDate ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@ContractEndDate", ContractEndDate ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@ContractPeriod", ContractPeriod ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@Updateby", Updateby ?? (object)DBNull.Value ?? (object)DBNull.Value));

           string UpdateQuery = @"UPDATE dbo.tblEmpGeneralInfo SET ContractStartDate=@ContractStartDate, ContractEndDate=@ContractEndDate, ContractPeriod=@ContractPeriod, Updateby=@Updateby, UpdateDate=GETDATE()   WHERE EmpInfoId=@EmpInfoId
";



           return aCommonInternalDal.UpdateDataByUpdateCommand(UpdateQuery, aSqlParameterlist, "HRDB");
       }

       public DataTable GetContractualEmpManageById(string id)
       {
           string query = @"  SELECT ISNULL(tblContractualEmpManage.EmpTypeId,0) EmpTypeId,tblContractualEmpManage.ContractPreiod,  tblContractualEmpManage.FromProject, tblContractualEmpManage.ToProject,tblContractualEmpManage.EffectiveDate,tblContractualEmpManage.EmployeeId MainEmpId,   empt.EmpType,  EG.EmpName AS EmployeeName ,  EG.EmpMasterCode, deg.Designation, SG.GradeCode+':'+ SG.GradeName AS GradeName,   Div.DivisionName, Wing.DivisionWingName, Sec.SectionName, SubSec.SubSectionName, Dpt.DepartmentName,   SLP.SalaryLocation, JL.Location, degT.DesigTypeName, St.SalaryStepName, *,tblUser.EmpInfoId AS UserEmpInfoId FROM dbo.tblContractualEmpManage
           INNER JOIN dbo.tblEmpGeneralInfo EG ON tblContractualEmpManage.EmployeeId= EG.EmpInfoId 
LEFT JOIN dbo.tblDesignation  deg ON EG.DesignationId=deg.DesignationId
							LEFT JOIN dbo.tblSalaryGrade  SG ON EG.SalaryGradeId=SG.SalaryGradeId
							LEFT JOIN dbo.tblDivision  Div ON EG.DivisionId=Div.DivisionId
							LEFT JOIN dbo.tblDivisionWing  Wing ON EG.DivisionWId=Wing.DivisionWId
							LEFT JOIN dbo.tblSection  Sec ON EG.SectionId=Sec.SectionId
							LEFT JOIN dbo.tblSubSection  SubSec ON EG.SubSectionId=SubSec.SubSectionId						
								LEFT JOIN dbo.tblDepartment  Dpt ON EG.DepartmentId=Dpt.DepartmentId
								LEFT JOIN dbo.tblUser ON dbo.tblUser.UserId=dbo.tblContractualEmpManage.EntryBy
								LEFT JOIN dbo.tblEmployeeType empt ON empt.EmpTypeId=EG.EmpTypeId

								 LEFT JOIN dbo.tblSalaryLocation SLP ON SLP.SalaryLoationId = EG.SalaryLoationId 
 LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
LEFT JOIN dbo.tblDesignationType  degT ON EG.DesignationTypeId=degT.DesignationTypeId
LEFT JOIN dbo.tblSalaryStep  St ON EG.SalaryStepId=St.SalaryStepId
           WHERE  ContractualEmpManageId='" + id + "'";
     return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }


       public DataTable GetSpTransferCheckByEmpId(string id)
       {
           string query = @" select  top 1 e.IsSpTransfer,sp.IsSMCRecordView,sp.IsELRecordView  from       tblEmpGeneralInfo e
inner join tblEmpSpecialTransfer sp on e.EmpInfoId=sp.NewEmployeeId
  where e.IsSpTransfer=1 and e.EmpInfoId='" + id + "'     order by sp.EmpSpecialTransferId desc";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
       public DataTable getMemoData(string id)
       {
           string query = @"     SELECT  'The detailed information after '+case when  mas.IsExtension=1 then 'Extension' when  mas.IsRenew=1 then 'Renew' when  mas.IsPermanentToContractual=1 then 'Permanent to Contractual'  when  mas.IsContractualToPermanent=1 then 'Contractual to Permanent'    when  mas.isToProject=1 then 'Project Change' else '' end +' their employment agreement with upliftment and salary enhancement is given below:'  as   GridviewBefore, 'Please be noted that the salary of  '+emp.EmpName+'   has been fixed at the closest highest of the next salary grade and then given one step normal increment to  '+emp.EmpName+'.' as   Footer01, com.CompanyName, 'The following employee of '+ISNULL(pro.ProjectName,'-')+' project have joined '+ISNULL(com.ShortName,'')+' under the '+div.DivisionName+' as contractual employees for the period of '+ CAST(DATEDIFF(Month, emp.ContractStartDate, emp.ContractEndDate) as nvarchar(max))+' Months. Their agreement expired during '+ FORMAT(emp.ContractEndDate,'MMMM, yyyy')+'.

In this context, we sent a memo to the '+div.DivisionName+' for the recommendation regarding continuation of their service.

The '+ISNULL(dgs.Designation,' ' )+ISNULL(' '+divEntry.DivisionName,' ')+'.  Program Operations have recommended  renewing the employment agreement of '+emp.EmpName+', '+ISNULL(dgsNew.Designation,'')+' for another'+  case when  mas.IsExtension=1 then CAST(DATEDIFF(Month, mas.ExtensionFromDate, mas.ExtensionToDate) as nvarchar(max)) when  mas.IsRenew=1 then CAST(DATEDIFF(Month, mas.RenewStartDate, mas.RenewToDate) as nvarchar(max)) when  mas.IsPermanentToContractual=1 then CAST(DATEDIFF(Month, mas.PermanentToContractualEffectiveDate, mas.PermanentToContractualEndDate) as nvarchar(max))  when  mas.IsContractualToPermanent=1 then '-'    when  mas.isToProject=1 then CAST(DATEDIFF(Month, mas.PermanentToContractualEffectiveDate, mas.PermanentToContractualEndDate) as nvarchar(max)) else '' end  +' Months, including a salary grade upliftment from their current salary grade. They have also recommended giving '+emp.EmpName+' a normal one-step increment which is provided at the time of contract renewal.

Detailed justifications (enclosed) of their upliftment of salary grades and salary enhancement have been given where their key duties and responsibilities, past performance, competency level and management tasks etc. are elaborately mentioned.' BodyLetter, FORMAT(mas.EffectiveDate,'MMMM dd, yyyy') EffectiveDate, case when  mas.IsExtension=1 then 'Extension' when  mas.IsRenew=1 then 'Renew' when  mas.IsPermanentToContractual=1 then 'Permanent to Contractual'  when  mas.IsContractualToPermanent=1 then 'Contractual to Permanent'    when  mas.isToProject=1 then 'Project Change' else '' end+ ' of employment agreement' Subject_, ISNULL(dgs.Designation,'') From_ , STUFF( (SELECT CONCAT(' -- ', dgs.Designation , ' ') FROM tblContractualEmpManageAppLog mm (NOLOCK) INNER JOIN dbo.tblEmpGeneralInfo mgd ON mgd.EmpInfoId=mm.ForEmpInfoId 
 LEFT JOIN dbo.tblDesignation dgs ON mgd.DesignationId = dgs.DesignationId	WHERE mm.ContractualEmpManageId=mas.ContractualEmpManageId  ORDER BY mm.Version asc FOR XML PATH ('') ),1,1,'') AS    TO_   from [dbo].tblContractualEmpManage mas
 left JOIN  dbo.tbl_UpliftForSupervisor ul   ON  mas.ContractualEmpManageId =ul.ContractualEmpManageId  
 left JOIN  dbo.tblUser us   ON  mas.EntryBy =us.UserId  
left JOIN  dbo.tblEmpGeneralInfo usemp   ON  us.EmpInfoId =usemp.EmpInfoId
  
 LEFT JOIN dbo.tblDesignation dgs ON usemp.DesignationId = dgs.DesignationId
 LEFT JOIN dbo.tblDivision divEntry ON usemp.DivisionId = divEntry.DivisionId
left JOIN  dbo.tblEmpGeneralInfo emp   ON  mas.EmployeeId =emp.EmpInfoId  
left JOIN  dbo.tblProjectSetup pro   ON  pro.ProjectId =emp.ProjectId  
left JOIN  dbo.tblCompanyInfo com   ON  com.CompanyId =emp.CompanyId  
left JOIN  dbo.tblDivision div   ON  div.DivisionId =emp.DivisionId  
 LEFT JOIN dbo.tblDesignation dgsNew ON emp.DesignationId = dgsNew.DesignationId
 WHERE   mas.ContractualEmpManageId= '" + id + "'";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
       public DataTable getUpLiftSuppervisorData(string id)
       {
           string query = @"       SELECT mas.IsSeparation, jlt.JobLeftType, FORMAT(mas.SeparationDate,'dd-MMM-yyyy') SeparationDate, mas.IsOrganization,  DivP.DivisionName PDivisionName,  Div.DivisionName, WingP.DivisionWingName PDivisionWingName, Wing.DivisionWingName,SecP.SectionName PSectionName, Sec.SectionName, SubSecP.SubSectionName PSubSectionName,SubSec.SubSectionName,DptP.DepartmentName PDepartmentName, Dpt.DepartmentName, mas.IsDesignation,degP.Designation PDesignation, deg.Designation,degTP.DesigTypeName PDesigTypeName, degT.DesigTypeName, mas.IsSalary,SGP.GradeCode PSalaryGrade,  SG.GradeCode SalaryGrade,StP.SalaryStepName PSalaryStepName,StP.GrossAmount PBasicAmount,St.GrossAmount BasicAmount,ABS(CONVERT(decimal(18,2),ISNULL(( (NULLIF( ISNULL((StP.GrossAmount-St.GrossAmount),0),0) )/ NULLIF((ISNULL((StP.GrossAmount+St.GrossAmount),0)/2)*100,0)) ,0)) )  PercentAmount ,St.SalaryStepName, mas.IsPlace,JLP.Location PPlace,  JL.Location Place,  SLP.SalaryLocation , SLP.SalaryLocation POffice, SL.SalaryLocation Office, mas.Old_Floor ,mas.New_Floor from [dbo].[tbl_UpliftForSupervisor] mas
 left join tblJobLeftType jlt on mas.JobLeftTypeId=jlt.JobLeftTypeId
 	LEFT JOIN dbo.tblDivision  Div ON mas.New_DivisionId=Div.DivisionId
 	LEFT JOIN dbo.tblDivision  DivP ON mas.Old_DivisionId=DivP.DivisionId
LEFT JOIN dbo.tblDivisionWing  Wing ON mas.New_DivisionWId=Wing.DivisionWId
LEFT JOIN dbo.tblDivisionWing  WingP ON mas.Old_DivisionWId=WingP.DivisionWId
LEFT JOIN dbo.tblSection  Sec ON mas.New_SectionId=Sec.SectionId
LEFT JOIN dbo.tblSection  SecP ON mas.Old_SectionId=SecP.SectionId
LEFT JOIN dbo.tblSubSection  SubSec ON mas.New_SubSectionId=SubSec.SubSectionId						
LEFT JOIN dbo.tblSubSection  SubSecP ON mas.Old_SubSectionId=SubSecP.SubSectionId						
LEFT JOIN dbo.tblDepartment  Dpt ON mas.New_DepartmentId=Dpt.DepartmentId
LEFT JOIN dbo.tblDepartment  DptP ON mas.Old_DepartmentId=DptP.DepartmentId

LEFT JOIN dbo.tblDesignation  deg ON mas.New_DesignationId=deg.DesignationId
LEFT JOIN dbo.tblDesignation  degP ON mas.old_DesignationId=degP.DesignationId
LEFT JOIN dbo.tblDesignationType  degT ON mas.New_DesignationTypeId=degT.DesignationTypeId
LEFT JOIN dbo.tblDesignationType  degTP ON mas.old_DesignationTypeId=degTP.DesignationTypeId


LEFT JOIN dbo.tblSalaryGrade  SG ON mas.New_SalaryGradeId=SG.SalaryGradeId
LEFT JOIN dbo.tblSalaryGrade  SGP ON mas.old_SalaryGradeId=SGP.SalaryGradeId
LEFT JOIN dbo.tblSalaryStep  St ON mas.New_SalaryStepId=St.SalaryStepId
LEFT JOIN dbo.tblSalaryStep  StP ON mas.Old_SalaryStepId=StP.SalaryStepId

 LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = mas.New_SalaryLoationId 
 LEFT JOIN dbo.tblSalaryLocation SLP ON SLP.SalaryLoationId = mas.old_SalaryLoationId 
 LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = mas.New_JobLocationId 
  LEFT JOIN dbo.tblJobLocation JLP ON JLP.JobLocationID = mas.old_JobLocationId 
           WHERE   ContractualEmpManageId='" + id + "'";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }


       public DataTable getUpLiftSuppervisorDataOne(string id)
       {
           string query = @"        select FORMAT(SeparationDate,'dd-MMM-yyyy') SeparationDate,  * from tbl_UpliftForSupervisor
           WHERE   ContractualEmpManageId='" + id + "'";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }


       public DataTable getMemoDataData(string id)
       {
           string query = @"       SELECT mas.IsSeparation, jlt.JobLeftType, FORMAT(mas.SeparationDate,'dd-MMM-yyyy') SeparationDate, mas.IsOrganization,  DivP.DivisionName PDivisionName,  Div.DivisionName, WingP.DivisionWingName PDivisionWingName, Wing.DivisionWingName,SecP.SectionName PSectionName, Sec.SectionName, SubSecP.SubSectionName PSubSectionName,SubSec.SubSectionName,DptP.DepartmentName PDepartmentName, Dpt.DepartmentName, mas.IsDesignation,degP.Designation PDesignation, deg.Designation,degTP.DesigTypeName PDesigTypeName, degT.DesigTypeName, mas.IsSalary,SGP.GradeCode PSalaryGrade,  SG.GradeCode SalaryGrade,StP.SalaryStepName PSalaryStepName,StP.GrossAmount PBasicAmount,St.GrossAmount BasicAmount,ISNULL(( (NULLIF( ISNULL((StP.GrossAmount-St.GrossAmount),0),0) )/ NULLIF((ISNULL((StP.GrossAmount+St.GrossAmount),0)/2)*100,0)) ,0)   PercentAmount ,St.SalaryStepName, mas.IsPlace,JLP.Location PPlace,  JL.Location Place,  SLP.SalaryLocation , SLP.SalaryLocation POffice, SL.SalaryLocation Office, mas.Old_Floor ,mas.New_Floor from [dbo].[tbl_UpliftForSupervisor] mas
 left join tblJobLeftType jlt on mas.JobLeftTypeId=jlt.JobLeftTypeId
 	LEFT JOIN dbo.tblDivision  Div ON mas.New_DivisionId=Div.DivisionId
 	LEFT JOIN dbo.tblDivision  DivP ON mas.Old_DivisionId=DivP.DivisionId
LEFT JOIN dbo.tblDivisionWing  Wing ON mas.New_DivisionWId=Wing.DivisionWId
LEFT JOIN dbo.tblDivisionWing  WingP ON mas.Old_DivisionWId=WingP.DivisionWId
LEFT JOIN dbo.tblSection  Sec ON mas.New_SectionId=Sec.SectionId
LEFT JOIN dbo.tblSection  SecP ON mas.Old_SectionId=SecP.SectionId
LEFT JOIN dbo.tblSubSection  SubSec ON mas.New_SubSectionId=SubSec.SubSectionId						
LEFT JOIN dbo.tblSubSection  SubSecP ON mas.Old_SubSectionId=SubSecP.SubSectionId						
LEFT JOIN dbo.tblDepartment  Dpt ON mas.New_DepartmentId=Dpt.DepartmentId
LEFT JOIN dbo.tblDepartment  DptP ON mas.Old_DepartmentId=DptP.DepartmentId

LEFT JOIN dbo.tblDesignation  deg ON mas.New_DesignationId=deg.DesignationId
LEFT JOIN dbo.tblDesignation  degP ON mas.old_DesignationId=degP.DesignationId
LEFT JOIN dbo.tblDesignationType  degT ON mas.New_DesignationTypeId=degT.DesignationTypeId
LEFT JOIN dbo.tblDesignationType  degTP ON mas.old_DesignationTypeId=degTP.DesignationTypeId


LEFT JOIN dbo.tblSalaryGrade  SG ON mas.New_SalaryGradeId=SG.SalaryGradeId
LEFT JOIN dbo.tblSalaryGrade  SGP ON mas.old_SalaryGradeId=SGP.SalaryGradeId
LEFT JOIN dbo.tblSalaryStep  St ON mas.New_SalaryStepId=St.SalaryStepId
LEFT JOIN dbo.tblSalaryStep  StP ON mas.Old_SalaryStepId=StP.SalaryStepId

 LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = mas.New_SalaryLoationId 
 LEFT JOIN dbo.tblSalaryLocation SLP ON SLP.SalaryLoationId = mas.old_SalaryLoationId 
 LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = mas.New_JobLocationId 
  LEFT JOIN dbo.tblJobLocation JLP ON JLP.JobLocationID = mas.old_JobLocationId 
           WHERE   ContractualEmpManageId='" + id + "'";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }

       public DataTable GetContractualEmpChangeById(string id)
       {
           string query = @" 
select * from  [dbo].[tblContractEmpInfo]
      WHERE ContractualEmpManageId='" + id + "'";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }

       public DataTable GetUpliftingDate(string id)
       {
           string query = @" 
select * from tbl_Uplift with (nolock)  where EmpInfoId='" + id + "'";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }


       public DataTable  GetContractualApplogForwordById(string id)
       {
           string query = @" 
select * from  [dbo].[tblContractualEmpManageAppLog] where IsForward=1 and ForEmpInfoId='" + HttpContext.Current.Session["EmpInfoId"].ToString() + @"'
      and  ContractualEmpManageId='" + id + "'";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }

       public DataTable GetContractualSequence(string id,  string parm)
       {
           string query = @" 
select top 1 * from tblContractualRoutingPath where ContractualEmpManageId='" + id + @"'   "+parm+" order by Seq_No asc";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }


       public DataTable LoadInformationALl(string param, string param2, string ComId, string ParameterTransfer)
       {
           string queryStr = @"    SELECT DISTINCT * FROM(SELECT EPE.ContractualEmpManageId, Emp.EmpInfoId,  Emp.EmpName, Emp.EmpMasterCode, Com.CompanyName, Ds.Designation, Dpt.DepartmentName, EffectiveDate, us.UserName EntryBy, usupda.UserName UpdateBy,    ForEmp.EmpName as PendingEmp, EPE.EntryDate, EPE.UpdateDate ,EPE.ActionStatus2  From tblContractualEmpManage EPE
 left JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
 left JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
 left JOIN dbo.tblDesignation  Ds ON Emp.DesignationId = Ds.DesignationId
 left JOIN dbo.tblDepartment  Dpt ON Emp.DepartmentId = Dpt.DepartmentId
 left JOIN dbo.tblUser  us ON EPE.EntryBy = us.UserId
 left JOIN dbo.tblUser  usupda ON EPE.UpdateBy = usupda.UserId
 LEFT JOIN (SELECT ContractualEmpManageId,MAX(Version)MaxVer FROM dbo.tblContractualEmpManageAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY ContractualEmpManageId) AS LogApp ON LogApp.ContractualEmpManageId= EPE.ContractualEmpManageId
								LEFT JOIN dbo.tblContractualEmpManageAppLog ON tblContractualEmpManageAppLog.ContractualEmpManageId = EPE.ContractualEmpManageId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblContractualEmpManageAppLog.ForEmpInfoId
                                LEFT JOIN dbo.tblContractualEmpManageAppLog PreLog ON PreLog.ContractualEmpManageId=EPE.ContractualEmpManageId AND PreLog.Version = CONVERT(INT,LogApp.MaxVer)-1
								LEFT JOIN dbo.tblEmpGeneralInfo PreEmp ON PreEmp.EmpInfoId=PreLog.ForEmpInfoId
  WHERE ((IsDelete=0) OR (IsDelete IS NULL)) AND (tblContractualEmpManageAppLog.Version=LogApp.MaxVer OR tblContractualEmpManageAppLog.Version IS NULL)  " + param + @" UNION ALL SELECT 0 ContractualEmpManageId, EGI.EmpInfoId  EmpInfoId , EGI.EmpName, EGI.EmpMasterCode EmpMasterCode, '' CompanyName,Ds.Designation, Dpt.DepartmentName,  mas.EffectiveDate, 
								''  EntryBy, '' UpdateBy,    ''as PendingEmp ,'' EntryDate, '' UpdateDate ,'' ActionStatus2
									  FROM tblStateChange_HistoricalDataId mas

									   INNER JOIN dbo.tblEmpGeneralInfo AS EGI ON mas.EmployeeId = EGI.EmpInfoId
									    left JOIN dbo.tblDesignation  Ds ON EGI.DesignationId = Ds.DesignationId
 left JOIN dbo.tblDepartment  Dpt ON EGI.DepartmentId = Dpt.DepartmentId  where StateChange_HistoricalDataId is not null " +   param2   + " " +
                             "" +
                             @" union all  SELECT EPE.ContractualEmpManageId, Emp.EmpInfoId,  Emp.EmpName, Emp.EmpMasterCode, Com.CompanyName, Ds.Designation, Dpt.DepartmentName, EffectiveDate, us.UserName EntryBy, usupda.UserName UpdateBy,    ForEmp.EmpName as PendingEmp, EPE.EntryDate, EPE.UpdateDate ,EPE.ActionStatus2  From tblContractualEmpManage EPE
 left JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
 left JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
 left JOIN dbo.tblDesignation  Ds ON Emp.DesignationId = Ds.DesignationId
 left JOIN dbo.tblDepartment  Dpt ON Emp.DepartmentId = Dpt.DepartmentId
 left JOIN dbo.tblUser  us ON EPE.EntryBy = us.UserId
 left JOIN dbo.tblUser  usupda ON EPE.UpdateBy = usupda.UserId
 LEFT JOIN (SELECT ContractualEmpManageId,MAX(Version)MaxVer FROM dbo.tblContractualEmpManageAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY ContractualEmpManageId) AS LogApp ON LogApp.ContractualEmpManageId= EPE.ContractualEmpManageId
								LEFT JOIN dbo.tblContractualEmpManageAppLog ON tblContractualEmpManageAppLog.ContractualEmpManageId = EPE.ContractualEmpManageId
								LEFT JOIN dbo.tblEmpGeneralInfo ForEmp ON ForEmp.EmpInfoId=dbo.tblContractualEmpManageAppLog.ForEmpInfoId
                                LEFT JOIN dbo.tblContractualEmpManageAppLog PreLog ON PreLog.ContractualEmpManageId=EPE.ContractualEmpManageId AND PreLog.Version = CONVERT(INT,LogApp.MaxVer)-1
								LEFT JOIN dbo.tblEmpGeneralInfo PreEmp ON PreEmp.EmpInfoId=PreLog.ForEmpInfoId

								 inner JOIN   tblEmpAllRefference reff  ON EPE.EmployeeId = reff.RefferenceEmpId 

								  inner join (select   NewEmployeeId,OnlyViewComId from tblEmpSpecialTransfer where OnlyView=1) tblPer on reff.EmployeeId =tblPer.NewEmployeeId   
  WHERE ((IsDelete=0) OR (IsDelete IS NULL)) AND (tblContractualEmpManageAppLog.Version=LogApp.MaxVer OR tblContractualEmpManageAppLog.Version IS NULL)      " + ParameterTransfer + ")HH  ORDER BY EffectiveDate DESC ";
   
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }
        public DataTable LoadInformationAppALl(string param)
       {
           string queryStr = @"SELECT  Emp.EmpName, Com.CompanyName,   *,Dept.DepartmentName,Desig.Designation,Emp.EmpMasterCode,Emp.DateOfJoin,
(CASE 
WHEN EPE.IsExtension=1 THEN 'Extension' 
WHEN EPE.IsRenew=1 THEN 'Renew' 
WHEN EPE.IsContractualToPermanent=1 THEN 'Contractual To Permanent'
WHEN EPE.IsPermanentToContractual='1' THEN 'Permanent To Contractual' ELSE '' END  )StateChangeReq
 From tblContractualEmpManage EPE
 INNER JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
 INNER JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
 LEFT JOIN dbo.tblDepartment Dept ON dept.DepartmentId=Emp.DepartmentId
 LEFT JOIN dbo.tblDesignation Desig ON Desig.DesignationId=Emp.DesignationId  
INNER JOIN (SELECT ContractualEmpManageId,MAX(Version)MaxVer FROM dbo.tblContractualEmpManageAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY ContractualEmpManageId) AS CELog ON CELog.ContractualEmpManageId= EPE.ContractualEmpManageId
								INNER JOIN dbo.tblContractualEmpManageAppLog ON tblContractualEmpManageAppLog.ContractualEmpManageId = EPE.ContractualEmpManageId
                                where Version=CELog.MaxVer  and  ForEmpInfoId = '" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'";
   
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }

        public DataTable LoadInformationAppALlCheck(string param)
        {
            string queryStr = @"SELECT  Emp.EmpName, Com.CompanyName,   *,Dept.DepartmentName,Desig.Designation,Emp.EmpMasterCode,Emp.DateOfJoin,
(CASE 
WHEN EPE.IsExtension=1 THEN 'Extension' 
WHEN EPE.IsRenew=1 THEN 'Renew' 
WHEN EPE.IsContractualToPermanent=1 THEN 'Contractual To Permanent'
WHEN EPE.IsPermanentToContractual='1' THEN 'Permanent To Contractual' ELSE '' END  )StateChangeReq
 From tblContractualEmpManage EPE
 INNER JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
 INNER JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
 LEFT JOIN dbo.tblDepartment Dept ON dept.DepartmentId=Emp.DepartmentId
 LEFT JOIN dbo.tblDesignation Desig ON Desig.DesignationId=Emp.DesignationId  
INNER JOIN (SELECT ContractualEmpManageId,MAX(Version)MaxVer FROM dbo.tblContractualEmpManageAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY ContractualEmpManageId) AS CELog ON CELog.ContractualEmpManageId= EPE.ContractualEmpManageId
								INNER JOIN dbo.tblContractualEmpManageAppLog ON tblContractualEmpManageAppLog.ContractualEmpManageId = EPE.ContractualEmpManageId
                                where Version=CELog.MaxVer  and  ForEmpInfoId = '" + param + "'";

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

       public bool DeleteContractualEmpManageById(string ContractualEmpManageId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@ContractualEmpManageId", ContractualEmpManageId));

           const string query = @"DELETE FROM tblContractualEmpManage WHERE ContractualEmpManageId = @ContractualEmpManageId";
           return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
       }

       public DataTable LoadEmpJInfoInTextBoxById(int id)
       {
           string query = @" SELECT case when empt.EmpType='Contractual'  then  case when Egf.IsSMCFundedProjects=1 then 'SMC Funded Projects' when Egf.IsProgramContractual=1 then 'Other Project' when   Egf.IsSMCFundedProjects=0 and Egf.IsProgramContractual=0 then 'SMC Contract'  else '' end else '' end  as ProjectType, empt.EmpType, Egf.ContractEndDate, Pro.ProjectName, Egf.EmpMasterCode, Egf.EmpName, Egf.DateOfJoin,   deg.Designation, SG.GradeCode +' : '+ SG.GradeName SalaryGrade, Com.CompanyId, Com.CompanyName, Div.DivisionName, Wing.DivisionWingName,  Dpt.DepartmentName,     Sec.SectionName, SubSec.SubSectionName,Egf.DepartmentId, *  FROM dbo.tblEmpGeneralInfo Egf
							left JOIN dbo.tblDesignation  deg ON Egf.DesignationId=deg.DesignationId
							left JOIN dbo.tblSalaryGrade  SG ON Egf.SalaryGradeId=SG.SalaryGradeId
							left JOIN dbo.tblCompanyInfo  Com ON Egf.CompanyId=Com.CompanyId
							left JOIN dbo.tblSalaryLocation  Loc ON Egf.SalaryLoationId=Loc.SalaryLoationId
							left JOIN dbo.tblJobLocation  JLoc ON Egf.JobLocationId=JLoc.JobLocationID
							left JOIN dbo.tblDivision  Div ON Egf.DivisionId=Div.DivisionId
							LEFT JOIN dbo.tblDivisionWing  Wing ON Egf.DivisionWId=Wing.DivisionWId
							left JOIN dbo.tblSection  Sec ON Egf.SectionId=Sec.SectionId
							LEFT JOIN dbo.tblSubSection  SubSec ON Egf.SubSectionId=SubSec.SubSectionId						
								LEFT JOIN dbo.tblDepartment  Dpt ON Egf.DepartmentId=Dpt.DepartmentId					
					LEFT JOIN dbo.tblProjectSetup  Pro ON Egf.ProjectID=Pro.ProjectId
					LEFT JOIN dbo.tblEmployeeType empt ON empt.EmpTypeId=Egf.EmpTypeId
                            WHERE Egf.EmpInfoId='" + id + "'";


           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }



       public DataTable LoadEmppInfo01(int id)
       {
           string query = @"SELECT LastOraga.ExperieceCompany, ST.GrossAmount, ISNULL(FORMAT(dtEmployeePromotion.Effectivedate, 'dd-MMM-yyyy'),dtEmployeePromotionNew.EffectivedateNew)
    
    AS LastPromotion,  CAST(ISNULL( EmpExp.Experiece ,0)+  ISNULL((DATEDIFF(year, EG.DateOfJoin, GETDATE())  - (CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 1 ELSE 0 END)) ,0) AS NVARCHAR(max))+ ' Years, '+ CAST( MONTH(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) - 1 AS NVARCHAR(max)) + ' Months, ' +

CAST(DAY(GETDATE() - DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin)) -(CASE WHEN DATEADD(year, DATEDIFF(year, EG.DateOfJoin, GETDATE()), EG.DateOfJoin) > GETDATE() THEN 0 ELSE 3 END)  AS NVARCHAR(max)) +' Days'   as  EmpExperiece,  com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office, deg.Designation, DP.DepartmentName,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , rptBody.EmpName Supervisor,
* FROM dbo.tblEmpGeneralInfo EG  WITH (NOLOCK)
                                LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDesignation deg ON deg.DesignationId = EG.DesignationId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId

                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               

                                LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 

                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 

								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 

                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana 
                                LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId


								 LEFT JOIN (SELECT EmpInfoId,tblEmpExperience.ExpCompany ExperieceCompany FROM dbo.tblEmpExperience WITH (NOLOCK) WHERE IsActive=1 AND ExpLastJob=1  ) AS LastOraga ON EG.EmpInfoId = LastOraga.EmpInfoId


								 LEFT JOIN (SELECT EmpInfoId,ISNULL(SUM(DATEDIFF(YEAR,ExpFromDate,ExpToDate)),0) Experiece FROM dbo.tblEmpExperience WITH (NOLOCK) WHERE IsActive=1  GROUP BY EmpInfoId) AS EmpExp ON EG.EmpInfoId = EmpExp.EmpInfoId


								 left JOIN (SELECT EmployeeId,MAX(EffectDate)AS Effectivedate  FROM  dbo.tblPromotionUpgrationHistory WHERE TypeOfPromotion IN ('Promotion','Upgradation') GROUP BY EmployeeId
 
   )dtEmployeePromotion ON dtEmployeePromotion.EmployeeId=EG.EmpInfoId   


   left JOIN (SELECT EPE.EmployeeId,MAX(EPE.Effectivedate)AS EffectivedateNew  FROM  dbo.tblEmployeePromotionEntry EPE WHERE EPE.NPromoTypeId IN (1,2) AND  ( (EPE.IsDelete IS NULL) OR (EPE.IsDelete = 0) )  GROUP BY EPE.EmployeeId
 
   )dtEmployeePromotionNew ON dtEmployeePromotionNew.EmployeeId=EG.EmpInfoId 

                            WHERE EG.EmpInfoId ='" + id + "'";


           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }


       public DataTable LoadEmppInfo02(int id)
       {
           string query = @" SELECT com.ShortName, DV.DivisionName Division, DW.DivisionWingName Wing,  Sec.SectionName Section, SubSec.SubSectionName SubSection, 
cat.EmpCategoryName  Category, SG.GradeCode Grade , ST.SalaryStepName Step, SL.SalaryLocation Office, deg.Designation, DP.DepartmentName,
JL.Location  Place, EG.DateOfBirth, nat.Description Nationality, EG.NationalIdNo, EG.PassportNo Passport, EG.BloodGroup,
EG.Gender,  EG.Religion, EG.PlaceOfBirth PlaceofBirth, EG.AddressPresent PresentAddress,
EG.AddressPermanent PermanentAddress , DivP.DivisionName PresentDivision,DivPer.DivisionName PermanentDivision,
  DisP.Titel PresentDistrict,DisPer.Titel PermanentDistrict,
  ThanaP.Title PresentThana ,ThanaPer.Title PermanentThana, EG.DateOfConformation DateOfConformation,
   cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())/12) as varchar) + ' Year, ' + 
       cast((DATEDIFF(m, EG.DateOfJoin, GETDATE())%12) as varchar) + ' Month, '
	   
	   +    cast((DATEDIFF(d, EG.DateOfJoin, GETDATE())%12) as varchar) + ' day'  ServiceLength, EG.DateOfRetirement DateofRetirement, EG.ProbationEndDate ProbitionEndDate ,EG.ContractEndDate ContractualEndDate , rptBody.EmpName Supervisor,
* FROM dbo.tblEmpGeneralInfo EG  WITH (NOLOCK)
                                LEFT JOIN dbo.tblCompanyInfo com ON com.CompanyId = EG.CompanyId
                                LEFT JOIN dbo.tblDivision DV ON DV.DivisionId = EG.DivisionId
                                LEFT JOIN dbo.tblDesignation deg ON deg.DesignationId = EG.DesignationId
                                LEFT JOIN dbo.tblDivisionWing DW ON DW.DivisionWId = EG.DivisionWId
                                LEFT JOIN dbo.tblDepartment DP ON DP.DepartmentId = EG.DepartmentId
                                LEFT JOIN dbo.tblDesignation DS ON DS.DesignationId = EG.DesignationId

                                LEFT JOIN dbo.tblSection Sec ON Sec.SectionId = EG.SectionId
                                LEFT JOIN dbo.tblSubSection SubSec ON SubSec.SubSectionId = EG.SubSectionId
                                LEFT JOIN dbo.tblEmpCategory cat ON cat.EmpCategoryId = EG.EmpCategoryId
                                LEFT JOIN dbo.tblSalaryGrade SG ON SG.SalaryGradeId = EG.SalaryGradeId
                                LEFT JOIN dbo.tblSalaryStep ST ON ST.SalaryStepId = EG.SalaryStepId
                               

                                LEFT JOIN dbo.tblSalaryLocation SL ON SL.SalaryLoationId = EG.SalaryLoationId 
                                LEFT JOIN dbo.tblJobLocation JL ON JL.JobLocationID = EG.JobLocationId 
                                LEFT JOIN dbo.tblNationality nat ON nat.Nationality = EG.Nationality 

                                LEFT JOIN dbo.tblDivision DivP ON DivP.DivisionId = EG.PresentDivision 
                                LEFT JOIN dbo.tblDivision DivPer ON DivPer.DivisionId = EG.ParmanentDivision 

								   LEFT JOIN dbo.tblDistrict DisP ON DisP.DistrictID = EG.PresentDistrict 
                                LEFT JOIN dbo.tblDistrict DisPer ON DisPer.DistrictID = EG.PermanentDistrict 

                                LEFT JOIN dbo.tblThana ThanaP ON ThanaP.ThanaID = EG.PresentThana 
                                LEFT JOIN dbo.tblThana ThanaPer ON ThanaPer.ThanaID = EG.PermanentThana 
                                LEFT JOIN dbo.tblEmpGeneralInfo rptBody ON rptBody.EmpInfoId = EG. ReportingEmpId
                            WHERE EG.EmpInfoId='" + id + "'";


           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }


       public DataTable ValidattionForEffectiveDate(string id, string date)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmployeeId", id));
           aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", date));
           string query = @"SELECT EmployeeId, EffectiveDate FROM dbo.tblContractualEmpManage WHERE  EmployeeId=@EmployeeId and EffectiveDate=@EffectiveDate";
           return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
       }

       public DataTable editValidattionForEffectiveDate(string id, string date, string MasterId)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmployeeId", id));
           aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", date));
           string query = @"SELECT EmployeeId, EffectiveDate FROM dbo.tblContractualEmpManage WHERE  ContractualEmpManageId not in (" + MasterId + ")  and EmployeeId=@EmployeeId and EffectiveDate=@EffectiveDate";
           return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
       }


       public DataTable DeleteValidattionForEffectiveDate(string id)
       {
           string query = @"SELECT  ContractualEmpManageId, EffectiveDate FROM dbo.tblContractualEmpManage WHERE ContractualEmpManageId=" + id;
           return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
       }

       ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

       public Int32 ExtensionSaveInfo(ContractualEmpManageDAO aContractualEmpManageDAO)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           aSqlParameterlist.Add(new SqlParameter("@CompanyId", aContractualEmpManageDAO.CompanyId));
           aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aContractualEmpManageDAO.EmployeeId));
           aSqlParameterlist.Add(new SqlParameter("@IsExtension", aContractualEmpManageDAO.IsExtension ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@IsRenew", aContractualEmpManageDAO.IsRenew ?? (object)DBNull.Value));


           aSqlParameterlist.Add(new SqlParameter("@IsPermanentToContractual", aContractualEmpManageDAO.IsPermanentToContractual ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsContractualToPermanent", aContractualEmpManageDAO.IsContractualToPermanent ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@IsSMCFundedProjectstoSMCContract", aContractualEmpManageDAO.IsSMCFundedProjectstoSMCContract ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@IsSMCContracttoSMCFundedProjects", aContractualEmpManageDAO.IsSMCContracttoSMCFundedProjects ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@isToProject", aContractualEmpManageDAO.isToProject ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@ExtensionFromDate", aContractualEmpManageDAO.ExtensionFromDate ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@ExtensionToDate", aContractualEmpManageDAO.ExtensionToDate ?? (object)DBNull.Value));


           aSqlParameterlist.Add(new SqlParameter("@RenewStartDate", aContractualEmpManageDAO.RenewStartDate ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@RenewToDate", aContractualEmpManageDAO.RenewToDate ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@PermanentToContractualEffectiveDate", aContractualEmpManageDAO.PermanentToContractualEffectiveDate ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@PermanentToContractualEndDate", aContractualEmpManageDAO.PermanentToContractualEndDate ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@ContractualToPermanentDate", aContractualEmpManageDAO.ContractualToPermanentDate ?? (object)DBNull.Value));





           aSqlParameterlist.Add(new SqlParameter("@IsSalaryIncrement", aContractualEmpManageDAO.IsSalaryIncrement ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@IsNoIncrement", aContractualEmpManageDAO.IsNoIncrement ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsFacilityIncluded", aContractualEmpManageDAO.IsFacilityIncluded ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@IsNoFacility", aContractualEmpManageDAO.IsNoFacility ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@Remarks", aContractualEmpManageDAO.Remarks));
           aSqlParameterlist.Add(new SqlParameter("@TypeOfPromotion", aContractualEmpManageDAO.TypeOfPromotion ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", aContractualEmpManageDAO.EffectiveDate ?? (object)DBNull.Value));
        



           aSqlParameterlist.Add(new SqlParameter("@EntryBy", aContractualEmpManageDAO.EntryBy));
           aSqlParameterlist.Add(new SqlParameter("@EntryDate", aContractualEmpManageDAO.EntryDate));
           aSqlParameterlist.Add(new SqlParameter("@ActionStatus", "Drafted"));



           aSqlParameterlist.Add(new SqlParameter("@DesignationId", (object)aContractualEmpManageDAO.DesignationId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DivisionId", (object)aContractualEmpManageDAO.DivisionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DivisionWId", (object)aContractualEmpManageDAO.DivisionWId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DepartmentId", (object)aContractualEmpManageDAO.DepartmentId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SectionId", (object)aContractualEmpManageDAO.SectionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SubSectionId", (object)aContractualEmpManageDAO.SubSectionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", (object)aContractualEmpManageDAO.SalaryLoationId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@JobLocationId", (object)aContractualEmpManageDAO.JobLocationId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", (object)aContractualEmpManageDAO.EmpTypeId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmployeeCode", (object)aContractualEmpManageDAO.EmployeeCode ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@ContractEndDate", (object)aContractualEmpManageDAO.ContractEndDate ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@AutoProcess", (object)aContractualEmpManageDAO.AutoProcess ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@ContractPreiod", (object)aContractualEmpManageDAO.ContractPreiod ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", (object)aContractualEmpManageDAO.SalaryGradeId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryStepId", (object)aContractualEmpManageDAO.SalaryStepId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@isReappointment", (object)aContractualEmpManageDAO.isReappointment ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsRedesignation", (object)aContractualEmpManageDAO.IsRedesignation ?? DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@FromProject", aContractualEmpManageDAO.FromProject ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@ToProject", aContractualEmpManageDAO.ToProject ?? (object)DBNull.Value));

           string insertQuery = @"INSERT INTO [dbo].[tblContractualEmpManage]
           (CompanyId,EmployeeId,IsExtension
           ,IsRenew
           ,IsPermanentToContractual
           ,IsContractualToPermanent
 ,ExtensionFromDate
           ,ExtensionToDate
           ,IsSalaryIncrement
           ,IsNoIncrement
           ,IsFacilityIncluded
           ,IsNoFacility
           ,Remarks
           ,EntryBy
           ,EntryDate, RenewStartDate, RenewToDate, PermanentToContractualEffectiveDate, ContractualToPermanentDate,ActionStatus,EffectiveDate, DivisionId, DivisionWId, DepartmentId,  SectionId, SubSectionId, DesignationId,SalaryLoationId, JobLocationId , EmpTypeId,EmployeeCode,ContractEndDate, PermanentToContractualEndDate, AutoProcess,  ContractPreiod,SalaryGradeId,SalaryStepId, IsSMCFundedProjectstoSMCContract,IsSMCContracttoSMCFundedProjects,isToProject,isReappointment,IsRedesignation,TypeOfPromotion,FromProject,ToProject)
     VALUES
           (@CompanyId,@EmployeeId,@IsExtension
           ,@IsRenew
           ,@IsPermanentToContractual
           ,@IsContractualToPermanent
 ,@ExtensionFromDate
           ,@ExtensionToDate
           ,@IsSalaryIncrement
           ,@IsNoIncrement
           ,@IsFacilityIncluded
           ,@IsNoFacility
           ,@Remarks
           ,@EntryBy
           ,@EntryDate, @RenewStartDate, @RenewToDate, @PermanentToContractualEffectiveDate, @ContractualToPermanentDate,@ActionStatus, @EffectiveDate,  @DivisionId, @DivisionWId, @DepartmentId,  @SectionId, @SubSectionId, @DesignationId, @SalaryLoationId, @JobLocationId , @EmpTypeId, @EmployeeCode,@ContractEndDate, @PermanentToContractualEndDate, @AutoProcess, @ContractPreiod,@SalaryGradeId,@SalaryStepId,@IsSMCFundedProjectstoSMCContract,@IsSMCContracttoSMCFundedProjects,@isToProject,@isReappointment,@IsRedesignation,@TypeOfPromotion,@FromProject,@ToProject)";

           return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
       }







       public bool InsertNewColumnInEmpGeneralTableByEmpID(string str, bool? IsSMCFundedProjects, bool? IsProgramContractual, string EmpInfoId, DateTime? EffectiveDate, DateTime? RenewStartDate, DateTime? ContractEndDate, int? ContractPeriod, int? EmpTypeId, int? Updateby)
       {  
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", EmpInfoId));
           aSqlParameterlist.Add(new SqlParameter("@InactiveReason", str));
           aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", EffectiveDate?? (object) DBNull.Value)) ;
           aSqlParameterlist.Add(new SqlParameter("@ContractStartDate", RenewStartDate ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@ContractEndDate", ContractEndDate ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@ContractPeriod", ContractPeriod ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", EmpTypeId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@Updateby", Updateby ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsSMCFundedProjects", IsSMCFundedProjects ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsProgramContractual", IsProgramContractual ?? (object)DBNull.Value));



           const string queryStr = @"
--tblEmpGeneralInfo
INSERT INTO [dbo].[tblEmpGeneralInfo]  (  
       [ReportingEmpId]
      ,[EmpMasterCode]
      ,[CompanyId]
      ,[DivisionId]
      ,[DivisionWId]
      ,[DepartmentId]
      ,[SectionId]
      ,[SubSectionId]
      ,[EmpCategoryId]
      ,[SalaryGradeId]
      ,[SalaryStepId]
      ,[DesignationId]
      ,[DesignationTypeId]
      ,[SalaryLoationId]
      ,[JobLocationId]
      ,[EmpName]
      ,[ShortName]
      ,[Gender]
      ,[BloodGroup]
      ,[TinNo]
      ,[FatherName]
      ,[FatherOccupation]
      ,[MotherName]
      ,[MotherOccupation]
      ,[DateOfBirth]
      ,[DateOfJoin]
      ,[Religion]
      ,[MaritalStatus]
      ,[EmpTypeId]
      ,[ProjectID]
      ,[SalaryFromProject]
      ,[IsProbationary]
      ,[ProbationEndDate]
      ,[ExtProbationPeriodDate]
      ,[ContractEndDate]
      ,[ExtContractDate]
      ,[Nationality]
      ,[NationalityID]
      ,[PassportNo]
      ,[ExpectedServiceLength]
      ,[DateOfRetirement]
      ,[DateOfConformation]
      ,[PlaceOfBirth]
      ,[AddressPresent]
      ,[PresentDivision]
      ,[PresentDistrict]
      ,[PresentThana]
      ,[PresentTelNo]
      ,[AddressPermanent]
      ,[ParmanentDivision]
      ,[PermanentDistrict]
      ,[PermanentThana]
      ,[ParmanentTelNo]
      ,[PersonalEmail]
      ,[OfficialEmail]
      ,[PersonalMobile]
      ,[OfficialMobile]
      ,[FaxNo]
      ,[EmergencyContactPerson]
      ,[EmergencyContactAddress]
      ,[EmergencyContactNumber]
      ,[SpouseName]
      ,[SpouseDateOfBirth]
      ,[SpouseMaxEducation]
      ,[SpouseOccupation]
      ,[DateOfMarriage]
      ,[DateOfConfirmation]
      ,[ConformationStatus]
      ,[Age]
      ,[NAge]
      ,[IsActive]
      ,[ActionStatus]
      ,[MedicalInformation]
      ,[JobID]
      ,[CandidateID]
      ,[Remarks]
      ,[EntryBy]
      ,[EntryDate]
      ,[Updateby]
      ,[UpdateDate]
      ,[EmployeeStatus]
      ,[InactiveReason]
      ,[ApprovalDate]
      ,[IsDeleted]
      ,[DeleteBy]
      ,[DeleteDate]
      ,[EmpImage]
      ,[EmpSign]
      ,[PayType]
      ,[SMCOldCode]
      ,[NationalIdNo]
      ,[IsProgramContractual]
      ,[NomineeImage]
      ,[ContractPeriod]
      ,[Floor],[ReferenceID], ContractStartDate,IsSMCFundedProjects  )

 

SELECT  [ReportingEmpId]
      ,NULL
      ,[CompanyId]
      ,[DivisionId]
      ,[DivisionWId]
      ,[DepartmentId]
      ,[SectionId]
      ,[SubSectionId]
      ,[EmpCategoryId]
      ,[SalaryGradeId]
      ,[SalaryStepId]
      ,[DesignationId]
      ,[DesignationTypeId]
      ,[SalaryLoationId]
      ,[JobLocationId]
      ,[EmpName]
      ,[ShortName]
      ,[Gender]
      ,[BloodGroup]
      ,[TinNo]
      ,[FatherName]
      ,[FatherOccupation]
      ,[MotherName]
      ,[MotherOccupation]
      ,[DateOfBirth]
      ,ISNULL(@EffectiveDate,NULL)
      ,[Religion]
      ,[MaritalStatus]
      ,@EmpTypeId
      ,[ProjectID]
      ,[SalaryFromProject]
      ,[IsProbationary]
      ,[ProbationEndDate]
      ,[ExtProbationPeriodDate]
      ,ISNULL(@ContractEndDate,NULL)
      ,[ExtContractDate]
      ,[Nationality]
      ,[NationalityID]
      ,[PassportNo]
      ,[ExpectedServiceLength]
      ,[DateOfRetirement]
      ,ISNULL(@EffectiveDate,NULL)
      ,[PlaceOfBirth]
      ,[AddressPresent]
      ,[PresentDivision]
      ,[PresentDistrict]
      ,[PresentThana]
      ,[PresentTelNo]
      ,[AddressPermanent]
      ,[ParmanentDivision]
      ,[PermanentDistrict]
      ,[PermanentThana]
      ,[ParmanentTelNo]
      ,[PersonalEmail]
      ,[OfficialEmail]
      ,[PersonalMobile]
      ,[OfficialMobile]
      ,[FaxNo]
      ,[EmergencyContactPerson]
      ,[EmergencyContactAddress]
      ,[EmergencyContactNumber]
      ,[SpouseName]
      ,[SpouseDateOfBirth]
      ,[SpouseMaxEducation]
      ,[SpouseOccupation]
      ,[DateOfMarriage]
      ,ISNULL(@EffectiveDate,NULL)
      ,[ConformationStatus]
      ,[Age]
      ,[NAge]
      ,[IsActive]
      ,[ActionStatus]
      ,[MedicalInformation]
      ,[JobID]
      ,[CandidateID]
      ,[Remarks]
      ,@Updateby
      ,GETDATE()
      ,NULL
      ,NULL
      ,[EmployeeStatus]
      ,[InactiveReason]
      ,[ApprovalDate]
      ,[IsDeleted]
      ,[DeleteBy]
      ,[DeleteDate]
      ,[EmpImage]
      ,[EmpSign]
      ,[PayType]
      ,[SMCOldCode]
      ,[NationalIdNo]
      ,@IsProgramContractual
      ,[NomineeImage]
      ,ISNULL(@ContractPeriod,NULL)
      ,[Floor],@EmpInfoId,  ISNULL(@ContractStartDate,NULL), @IsSMCFundedProjects
  FROM [dbo].[tblEmpGeneralInfo] WHERE EmpInfoId=@EmpInfoId

UPDATE tblEmpGeneralInfo SET IsActive=0, EmployeeStatus='InActive', InactiveReason=@InactiveReason, Updateby=@Updateby, UpdateDate=GETDATE()  WHERE EmpInfoId=@EmpInfoId
-- Get Max EmpInfoId From  tblEmpGeneralInfo
DECLARE @EmpMaxId BIGINT =NULL
SELECT @EmpMaxId=   MAX(EmpInfoId) FROM dbo.tblEmpGeneralInfo 



DECLARE @EmpMasterCode NVARCHAR(max)
select @EmpMasterCode=EmpMasterCode FROM [dbo].[tblEmpGeneralInfo] WHERE EmpInfoId=@EmpInfoId
 UPDATE tblEmpGeneralInfo SET  SMCOldCode=@EmpMasterCode  WHERE EmpInfoId=@EmpMaxId


update tblEmpGeneralInfo set leaveRecommenderId=@EmpMaxId where leaveRecommenderId=@EmpInfoId

update tblEmpGeneralInfo set LeaveApprovalId=@EmpMaxId where LeaveApprovalId=@EmpInfoId

Update dbo.tblSupevisorMenuApproval SET 
                                EmpInfoId=@EmpMaxId 
                            WHERE EmpInfoId=@EmpInfoId

update tblEmpGeneralInfo set ReportingEmpId=@EmpMaxId where ReportingEmpId=@EmpInfoId

-- tblEmpChildren
INSERT INTO [dbo].[tblEmpChildren]
           ([EmpInfoId]
           ,[ChildrenName]
           ,[ChildrenGender]
           ,[ChildrenOccupation]
           ,[ChildrenDOB]
           ,[ChildrenMaritalStatus]
           ,[IsActive])

		   SELECT  @EmpMaxId
      ,[ChildrenName]
      ,[ChildrenGender]
      ,[ChildrenOccupation]
      ,[ChildrenDOB]
      ,[ChildrenMaritalStatus]
      ,[IsActive]
  FROM [dbo].[tblEmpChildren]  WHERE EmpInfoId=@EmpInfoId


-- tblEmpEducation
INSERT INTO [dbo].[tblEmpEducation]
           ([EmpInfoId]
           ,[EducationNameId]
           ,[SubjectGroupId]
           ,[BoardUniversityId]
           ,[Result]
           ,[EducationalInstituteId]
           ,[PassingYear]
           ,[CgpaOrTotalMarks]
           ,[FieldOfSpecializationId]
           ,[EduIsLastLevel]
           ,[IsActive]
           ,[IsProfessionalEdu])

		   SELECT  @EmpMaxId
      ,[EducationNameId]
           ,[SubjectGroupId]
           ,[BoardUniversityId]
           ,[Result]
           ,[EducationalInstituteId]
           ,[PassingYear]
           ,[CgpaOrTotalMarks]
           ,[FieldOfSpecializationId]
           ,[EduIsLastLevel]
           ,[IsActive]
           ,[IsProfessionalEdu]
  FROM [dbo].[tblEmpEducation]  WHERE EmpInfoId=@EmpInfoId



--tblEmpExperience

INSERT INTO [dbo].[tblEmpExperience]
           ([EmpInfoId]
           ,[ExpContactPerson]
           ,[ExpCompany]
           ,[ExpAddress]
           ,[ExpTelNo]
           ,[ExpNatureofBusiness]
           ,[ExpDesignation]
           ,[ExpJobDescription]
           ,[ExpFromDate]
           ,[ExpToDate]
           ,[ExpJobType]
           ,[ExpLastJob]
           ,[ExpRemarks]
           ,[ExpLeavingSalary]
           ,[IsActive])

		   SELECT  @EmpMaxId
      ,[ExpContactPerson]
           ,[ExpCompany]
           ,[ExpAddress]
           ,[ExpTelNo]
           ,[ExpNatureofBusiness]
           ,[ExpDesignation]
           ,[ExpJobDescription]
           ,[ExpFromDate]
           ,[ExpToDate]
           ,[ExpJobType]
           ,[ExpLastJob]
           ,[ExpRemarks]
           ,[ExpLeavingSalary]
           ,[IsActive]
  FROM [dbo].[tblEmpExperience]  WHERE EmpInfoId=@EmpInfoId



--tblEmpTraining

INSERT INTO [dbo].[tblEmpTraining]
           ([EmpInfoId]
           ,[TrainingName]
           ,[TrainingType]
           ,[TrainingDescription]
           ,[TrainingInstitution]
           ,[TrainingPlace]
           ,[TrainingCountry]
           ,[TrainingAchievment]
           ,[TrFromDate]
           ,[TrToDate]
           ,[TrainingDays]
           ,[TrRemarks]
           ,[IsActive])

		   SELECT  @EmpMaxId
      ,[TrainingName]
           ,[TrainingType]
           ,[TrainingDescription]
           ,[TrainingInstitution]
           ,[TrainingPlace]
           ,[TrainingCountry]
           ,[TrainingAchievment]
           ,[TrFromDate]
           ,[TrToDate]
           ,[TrainingDays]
           ,[TrRemarks]
           ,[IsActive]
  FROM [dbo].[tblEmpTraining]  WHERE EmpInfoId=@EmpInfoId



--tblEmpReference

INSERT INTO [dbo].[tblEmpReference]
           ([EmpInfoId]
           ,[ReferenceName]
           ,[RefOccupation]
           ,[RefAddress]
           ,[RefMobile]
           ,[IsActive])

		   SELECT  @EmpMaxId
       ,[ReferenceName]
           ,[RefOccupation]
           ,[RefAddress]
           ,[RefMobile]
           ,[IsActive]
  FROM [dbo].[tblEmpReference]  WHERE EmpInfoId=@EmpInfoId


--tblEmpNominee

INSERT INTO [dbo].[tblEmpNominee]
           ([EmpInfoId]
           ,[NominationPurpose]
           ,[NomineeName]
           ,[NomineeOccupation]
           ,[DateOfNomination]
           ,[NominationPercentage]
           ,[NomineeDOB]
           ,[NomineeRelation]
           ,[NomineeAddress]
           ,[NomineeTelephone]
           ,[IsActive]
           ,[NomNomineImg])

		   SELECT  @EmpMaxId
       ,[NominationPurpose]
           ,[NomineeName]
           ,[NomineeOccupation]
           ,[DateOfNomination]
           ,[NominationPercentage]
           ,[NomineeDOB]
           ,[NomineeRelation]
           ,[NomineeAddress]
           ,[NomineeTelephone]
           ,[IsActive]
           ,[NomNomineImg]
  FROM [dbo].[tblEmpNominee]  WHERE EmpInfoId=@EmpInfoId


--tblEmpExtraCurriculam

INSERT INTO [dbo].[tblEmpExtraCurriculam]
           ([EmpInfoId]
           ,[MasterExtraCurriculamId]
           ,[IsActive])

		   SELECT  @EmpMaxId
       ,[MasterExtraCurriculamId]
           ,[IsActive]
  FROM [dbo].[tblEmpExtraCurriculam]  WHERE EmpInfoId=@EmpInfoId

--tblEmpOtherTalents

INSERT INTO [dbo].[tblEmpOtherTalents]
           ([EmpInfoId]
           ,[MasterOtherTalentsId]
           ,[IsActive])

		   SELECT  @EmpMaxId
       ,[MasterOtherTalentsId]
           ,[IsActive]
  FROM [dbo].[tblEmpOtherTalents]  WHERE EmpInfoId=@EmpInfoId


--tblEmpAchievements

INSERT INTO [dbo].[tblEmpAchievements]
           ([EmpInfoId]
           ,[MasterAchievementsId]
           ,[IsActive])

		   SELECT  @EmpMaxId
        ,[MasterAchievementsId]
           ,[IsActive]
  FROM [dbo].[tblEmpAchievements]  WHERE EmpInfoId=@EmpInfoId

--tblEmpHobby

INSERT INTO [dbo].[tblEmpHobby]
           ([EmpInfoId]
           ,[MasterHobbyId]
           ,[IsActive])

		   SELECT  @EmpMaxId
         ,[MasterHobbyId]
           ,[IsActive]
  FROM [dbo].[tblEmpHobby]  WHERE EmpInfoId=@EmpInfoId


";
           return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
       }

       public bool UpdateUplifitingInfo( string EmpInfoId, int? Updateby, int? CompanyId, int? DivisionId, int? DivisionWId, int? DepartmentId, int? SectionId, int? SubSectionId, int? EmpCategoryId, int? SalaryGradeId, int? SalaryStepId, int? DesignationId, int? DesignationTypeId, int? JobLocationId, int? SalaryLoationId, string Floor)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", EmpInfoId));
          


           aSqlParameterlist.Add(new SqlParameter("@CompanyId", CompanyId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DivisionId", DivisionId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DivisionWId", DivisionWId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DepartmentId", DepartmentId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SectionId", SectionId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SubSectionId", SubSectionId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", EmpCategoryId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", SalaryGradeId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryStepId", SalaryStepId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DesignationId", DesignationId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DesignationTypeId", DesignationTypeId ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@JobLocationId", JobLocationId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", SalaryLoationId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@Floor", Floor ?? (object)DBNull.Value));




           const string queryStr = @"

         UPDATE [dbo].[tblEmpGeneralInfo]
   SET [CompanyId] = @CompanyId 
     
      ,[DivisionId] = @DivisionId 
      ,[DivisionWId] = @DivisionWId 
      ,[DepartmentId] = @DepartmentId 
      ,[SectionId] = @SectionId 
      ,[SubSectionId] = @SubSectionId 
      ,[EmpCategoryId] = @EmpCategoryId 
      ,[SalaryGradeId] = @SalaryGradeId 
      ,[SalaryStepId] = @SalaryStepId 
      ,[DesignationId] = @DesignationId 
      ,[DesignationTypeId] = @DesignationTypeId 
      ,[SalaryLoationId] = @SalaryLoationId 
      ,[JobLocationId] = @JobLocationId 
      
      ,[Floor] = @Floor 
     
 WHERE EmpInfoId=@EmpInfoId

";
           return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
       }

       public bool InsertNewColumnInEmpGeneralTableByEmpIDReappointment(string str, bool? IsSMCFundedProjects, bool? IsProgramContractual, string EmpInfoId, DateTime? EffectiveDate, DateTime? RenewStartDate, DateTime? ContractEndDate, int? ContractPeriod, int? EmpTypeId, int? Updateby,  int? CompanyId,  int? DivisionId,  int? DivisionWId, int?  DepartmentId,  int? SectionId,  int? SubSectionId, int?  EmpCategoryId, int?  SalaryGradeId, int?  SalaryStepId,  int? DesignationId, int?  DesignationTypeId,  int? JobLocationId,  int? SalaryLoationId, string Floor)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", EmpInfoId));
           aSqlParameterlist.Add(new SqlParameter("@InactiveReason", str));
           aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", EffectiveDate ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@ContractStartDate", RenewStartDate ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@ContractEndDate", ContractEndDate ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@ContractPeriod", ContractPeriod ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", EmpTypeId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@Updateby", Updateby ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsSMCFundedProjects", IsSMCFundedProjects ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsProgramContractual", IsProgramContractual ?? (object)DBNull.Value));



           aSqlParameterlist.Add(new SqlParameter("@CompanyId", CompanyId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DivisionId", DivisionId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DivisionWId", DivisionWId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DepartmentId", DepartmentId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SectionId", SectionId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SubSectionId", SubSectionId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", EmpCategoryId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", SalaryGradeId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryStepId", SalaryStepId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DesignationId", DesignationId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DesignationTypeId", DesignationTypeId ?? (object)DBNull.Value));
         
           aSqlParameterlist.Add(new SqlParameter("@JobLocationId", JobLocationId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", SalaryLoationId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@Floor", Floor ?? (object)DBNull.Value));




           const string queryStr = @"
--tblEmpGeneralInfo
INSERT INTO [dbo].[tblEmpGeneralInfo]  (  
       [ReportingEmpId]
      ,[EmpMasterCode]
      ,[CompanyId]
      ,[DivisionId]
      ,[DivisionWId]
      ,[DepartmentId]
      ,[SectionId]
      ,[SubSectionId]
      ,[EmpCategoryId]
      ,[SalaryGradeId]
      ,[SalaryStepId]
      ,[DesignationId]
      ,[DesignationTypeId]
      ,[SalaryLoationId]
      ,[JobLocationId]
      ,[EmpName]
      ,[ShortName]
      ,[Gender]
      ,[BloodGroup]
      ,[TinNo]
      ,[FatherName]
      ,[FatherOccupation]
      ,[MotherName]
      ,[MotherOccupation]
      ,[DateOfBirth]
      ,[DateOfJoin]
      ,[Religion]
      ,[MaritalStatus]
      ,[EmpTypeId]
      ,[ProjectID]
      ,[SalaryFromProject]
      ,[IsProbationary]
      ,[ProbationEndDate]
      ,[ExtProbationPeriodDate]
      ,[ContractEndDate]
      ,[ExtContractDate]
      ,[Nationality]
      ,[NationalityID]
      ,[PassportNo]
      ,[ExpectedServiceLength]
      ,[DateOfRetirement]
      ,[DateOfConformation]
      ,[PlaceOfBirth]
      ,[AddressPresent]
      ,[PresentDivision]
      ,[PresentDistrict]
      ,[PresentThana]
      ,[PresentTelNo]
      ,[AddressPermanent]
      ,[ParmanentDivision]
      ,[PermanentDistrict]
      ,[PermanentThana]
      ,[ParmanentTelNo]
      ,[PersonalEmail]
      ,[OfficialEmail]
      ,[PersonalMobile]
      ,[OfficialMobile]
      ,[FaxNo]
      ,[EmergencyContactPerson]
      ,[EmergencyContactAddress]
      ,[EmergencyContactNumber]
      ,[SpouseName]
      ,[SpouseDateOfBirth]
      ,[SpouseMaxEducation]
      ,[SpouseOccupation]
      ,[DateOfMarriage]
      ,[DateOfConfirmation]
      ,[ConformationStatus]
      ,[Age]
      ,[NAge]
      ,[IsActive]
      ,[ActionStatus]
      ,[MedicalInformation]
      ,[JobID]
      ,[CandidateID]
      ,[Remarks]
      ,[EntryBy]
      ,[EntryDate]
      ,[Updateby]
      ,[UpdateDate]
      ,[EmployeeStatus]
      ,[InactiveReason]
      ,[ApprovalDate]
      ,[IsDeleted]
      ,[DeleteBy]
      ,[DeleteDate]
      ,[EmpImage]
      ,[EmpSign]
      ,[PayType]
      ,[SMCOldCode]
      ,[NationalIdNo]
      ,[IsProgramContractual]
      ,[NomineeImage]
      ,[ContractPeriod]
      ,[Floor],[ReferenceID], ContractStartDate,IsSMCFundedProjects  )

 

SELECT  [ReportingEmpId]
      ,NULL
      ,@CompanyId
      ,@DivisionId
      ,@DivisionWId
      ,@DepartmentId
      ,@SectionId
      ,@SubSectionId
      ,@EmpCategoryId
      ,@SalaryGradeId
      ,@SalaryStepId
      ,@DesignationId
      ,@DesignationTypeId
      ,@SalaryLoationId
      ,@JobLocationId
      ,[EmpName]
      ,[ShortName]
      ,[Gender]
      ,[BloodGroup]
      ,[TinNo]
      ,[FatherName]
      ,[FatherOccupation]
      ,[MotherName]
      ,[MotherOccupation]
      ,[DateOfBirth]
      ,ISNULL(@EffectiveDate,NULL)
      ,[Religion]
      ,[MaritalStatus]
      ,@EmpTypeId
      ,[ProjectID]
      ,[SalaryFromProject]
      ,[IsProbationary]
      ,[ProbationEndDate]
      ,[ExtProbationPeriodDate]
      ,ISNULL(@ContractEndDate,NULL)
      ,[ExtContractDate]
      ,[Nationality]
      ,[NationalityID]
      ,[PassportNo]
      ,[ExpectedServiceLength]
      ,[DateOfRetirement]
      ,ISNULL(@EffectiveDate,NULL)
      ,[PlaceOfBirth]
      ,[AddressPresent]
      ,[PresentDivision]
      ,[PresentDistrict]
      ,[PresentThana]
      ,[PresentTelNo]
      ,[AddressPermanent]
      ,[ParmanentDivision]
      ,[PermanentDistrict]
      ,[PermanentThana]
      ,[ParmanentTelNo]
      ,[PersonalEmail]
      ,[OfficialEmail]
      ,[PersonalMobile]
      ,[OfficialMobile]
      ,[FaxNo]
      ,[EmergencyContactPerson]
      ,[EmergencyContactAddress]
      ,[EmergencyContactNumber]
      ,[SpouseName]
      ,[SpouseDateOfBirth]
      ,[SpouseMaxEducation]
      ,[SpouseOccupation]
      ,[DateOfMarriage]
      ,ISNULL(@EffectiveDate,NULL)
      ,[ConformationStatus]
      ,[Age]
      ,[NAge]
      ,[IsActive]
      ,[ActionStatus]
      ,[MedicalInformation]
      ,[JobID]
      ,[CandidateID]
      ,[Remarks]
      ,@Updateby
      ,GETDATE()
      ,NULL
      ,NULL
      ,[EmployeeStatus]
      ,[InactiveReason]
      ,[ApprovalDate]
      ,[IsDeleted]
      ,[DeleteBy]
      ,[DeleteDate]
      ,[EmpImage]
      ,[EmpSign]
      ,[PayType]
      ,[SMCOldCode]
      ,[NationalIdNo]
      ,@IsProgramContractual
      ,[NomineeImage]
      ,ISNULL(@ContractPeriod,NULL)
      ,@Floor,@EmpInfoId,  ISNULL(@ContractStartDate,NULL), @IsSMCFundedProjects
  FROM [dbo].[tblEmpGeneralInfo] WHERE EmpInfoId=@EmpInfoId

UPDATE tblEmpGeneralInfo SET IsActive=0, EmployeeStatus='InActive', InactiveReason=@InactiveReason, Updateby=@Updateby, UpdateDate=GETDATE()  WHERE EmpInfoId=@EmpInfoId
-- Get Max EmpInfoId From  tblEmpGeneralInfo
DECLARE @EmpMaxId BIGINT =NULL
SELECT @EmpMaxId=   MAX(EmpInfoId) FROM dbo.tblEmpGeneralInfo 




update tblEmpGeneralInfo set leaveRecommenderId=@EmpMaxId where leaveRecommenderId=@EmpInfoId

update tblEmpGeneralInfo set LeaveApprovalId=@EmpMaxId where LeaveApprovalId=@EmpInfoId

Update dbo.tblSupevisorMenuApproval SET 
                                EmpInfoId=@EmpMaxId 
                            WHERE EmpInfoId=@EmpInfoId

update tblEmpGeneralInfo set ReportingEmpId=@EmpMaxId where ReportingEmpId=@EmpInfoId



-- tblEmpChildren
INSERT INTO [dbo].[tblEmpChildren]
           ([EmpInfoId]
           ,[ChildrenName]
           ,[ChildrenGender]
           ,[ChildrenOccupation]
           ,[ChildrenDOB]
           ,[ChildrenMaritalStatus]
           ,[IsActive])

		   SELECT  @EmpMaxId
      ,[ChildrenName]
      ,[ChildrenGender]
      ,[ChildrenOccupation]
      ,[ChildrenDOB]
      ,[ChildrenMaritalStatus]
      ,[IsActive]
  FROM [dbo].[tblEmpChildren]  WHERE EmpInfoId=@EmpInfoId


-- tblEmpEducation
INSERT INTO [dbo].[tblEmpEducation]
           ([EmpInfoId]
           ,[EducationNameId]
           ,[SubjectGroupId]
           ,[BoardUniversityId]
           ,[Result]
           ,[EducationalInstituteId]
           ,[PassingYear]
           ,[CgpaOrTotalMarks]
           ,[FieldOfSpecializationId]
           ,[EduIsLastLevel]
           ,[IsActive]
           ,[IsProfessionalEdu])

		   SELECT  @EmpMaxId
      ,[EducationNameId]
           ,[SubjectGroupId]
           ,[BoardUniversityId]
           ,[Result]
           ,[EducationalInstituteId]
           ,[PassingYear]
           ,[CgpaOrTotalMarks]
           ,[FieldOfSpecializationId]
           ,[EduIsLastLevel]
           ,[IsActive]
           ,[IsProfessionalEdu]
  FROM [dbo].[tblEmpEducation]  WHERE EmpInfoId=@EmpInfoId



--tblEmpExperience

INSERT INTO [dbo].[tblEmpExperience]
           ([EmpInfoId]
           ,[ExpContactPerson]
           ,[ExpCompany]
           ,[ExpAddress]
           ,[ExpTelNo]
           ,[ExpNatureofBusiness]
           ,[ExpDesignation]
           ,[ExpJobDescription]
           ,[ExpFromDate]
           ,[ExpToDate]
           ,[ExpJobType]
           ,[ExpLastJob]
           ,[ExpRemarks]
           ,[ExpLeavingSalary]
           ,[IsActive])

		   SELECT  @EmpMaxId
      ,[ExpContactPerson]
           ,[ExpCompany]
           ,[ExpAddress]
           ,[ExpTelNo]
           ,[ExpNatureofBusiness]
           ,[ExpDesignation]
           ,[ExpJobDescription]
           ,[ExpFromDate]
           ,[ExpToDate]
           ,[ExpJobType]
           ,[ExpLastJob]
           ,[ExpRemarks]
           ,[ExpLeavingSalary]
           ,[IsActive]
  FROM [dbo].[tblEmpExperience]  WHERE EmpInfoId=@EmpInfoId



--tblEmpTraining

INSERT INTO [dbo].[tblEmpTraining]
           ([EmpInfoId]
           ,[TrainingName]
           ,[TrainingType]
           ,[TrainingDescription]
           ,[TrainingInstitution]
           ,[TrainingPlace]
           ,[TrainingCountry]
           ,[TrainingAchievment]
           ,[TrFromDate]
           ,[TrToDate]
           ,[TrainingDays]
           ,[TrRemarks]
           ,[IsActive])

		   SELECT  @EmpMaxId
      ,[TrainingName]
           ,[TrainingType]
           ,[TrainingDescription]
           ,[TrainingInstitution]
           ,[TrainingPlace]
           ,[TrainingCountry]
           ,[TrainingAchievment]
           ,[TrFromDate]
           ,[TrToDate]
           ,[TrainingDays]
           ,[TrRemarks]
           ,[IsActive]
  FROM [dbo].[tblEmpTraining]  WHERE EmpInfoId=@EmpInfoId



--tblEmpReference

INSERT INTO [dbo].[tblEmpReference]
           ([EmpInfoId]
           ,[ReferenceName]
           ,[RefOccupation]
           ,[RefAddress]
           ,[RefMobile]
           ,[IsActive])

		   SELECT  @EmpMaxId
       ,[ReferenceName]
           ,[RefOccupation]
           ,[RefAddress]
           ,[RefMobile]
           ,[IsActive]
  FROM [dbo].[tblEmpReference]  WHERE EmpInfoId=@EmpInfoId


--tblEmpNominee

INSERT INTO [dbo].[tblEmpNominee]
           ([EmpInfoId]
           ,[NominationPurpose]
           ,[NomineeName]
           ,[NomineeOccupation]
           ,[DateOfNomination]
           ,[NominationPercentage]
           ,[NomineeDOB]
           ,[NomineeRelation]
           ,[NomineeAddress]
           ,[NomineeTelephone]
           ,[IsActive]
           ,[NomNomineImg])

		   SELECT  @EmpMaxId
       ,[NominationPurpose]
           ,[NomineeName]
           ,[NomineeOccupation]
           ,[DateOfNomination]
           ,[NominationPercentage]
           ,[NomineeDOB]
           ,[NomineeRelation]
           ,[NomineeAddress]
           ,[NomineeTelephone]
           ,[IsActive]
           ,[NomNomineImg]
  FROM [dbo].[tblEmpNominee]  WHERE EmpInfoId=@EmpInfoId


--tblEmpExtraCurriculam

INSERT INTO [dbo].[tblEmpExtraCurriculam]
           ([EmpInfoId]
           ,[MasterExtraCurriculamId]
           ,[IsActive])

		   SELECT  @EmpMaxId
       ,[MasterExtraCurriculamId]
           ,[IsActive]
  FROM [dbo].[tblEmpExtraCurriculam]  WHERE EmpInfoId=@EmpInfoId

--tblEmpOtherTalents

INSERT INTO [dbo].[tblEmpOtherTalents]
           ([EmpInfoId]
           ,[MasterOtherTalentsId]
           ,[IsActive])

		   SELECT  @EmpMaxId
       ,[MasterOtherTalentsId]
           ,[IsActive]
  FROM [dbo].[tblEmpOtherTalents]  WHERE EmpInfoId=@EmpInfoId


--tblEmpAchievements

INSERT INTO [dbo].[tblEmpAchievements]
           ([EmpInfoId]
           ,[MasterAchievementsId]
           ,[IsActive])

		   SELECT  @EmpMaxId
        ,[MasterAchievementsId]
           ,[IsActive]
  FROM [dbo].[tblEmpAchievements]  WHERE EmpInfoId=@EmpInfoId

--tblEmpHobby

INSERT INTO [dbo].[tblEmpHobby]
           ([EmpInfoId]
           ,[MasterHobbyId]
           ,[IsActive])

		   SELECT  @EmpMaxId
         ,[MasterHobbyId]
           ,[IsActive]
  FROM [dbo].[tblEmpHobby]  WHERE EmpInfoId=@EmpInfoId


";
           return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
       }




       public bool InsertNewContractEmpChange(int ? id, string empGenId ,int? CompanyId,int?  DivisionId,int?  DivisionWId,int?  DepartmentId,
                                   int?  SectionId,int?  SubSectionId, int? EmpCategoryId, int? SalaryGradeId, int? SalaryStepId,int?  DesignationId,
                                   int? DesignationTypeId, int? JobLocationId, int? SalaryLoationId, string Floor, bool? IsSeparation, int? JobLeftTypeId, string jobleftDate)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@ContractualEmpManageId", id));
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", empGenId ?? (object)DBNull.Value));




           aSqlParameterlist.Add(new SqlParameter("@CompanyId", CompanyId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DivisionId", DivisionId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DivisionWId", DivisionWId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DepartmentId", DepartmentId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SectionId", SectionId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SubSectionId", SubSectionId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", EmpCategoryId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", SalaryGradeId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryStepId", SalaryStepId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DesignationId", DesignationId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DesignationTypeId", DesignationTypeId ?? (object)DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@JobLocationId", JobLocationId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", SalaryLoationId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@Floor", Floor ?? (object)DBNull.Value));


           aSqlParameterlist.Add(new SqlParameter("@IsSeparation", IsSeparation ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@JobLeftTypeId", JobLeftTypeId ?? (object)DBNull.Value));

           if (jobleftDate == "")
           {
               aSqlParameterlist.Add(new SqlParameter("@SeparationDate", null ?? (object)DBNull.Value));
               
           }
           else
           {
               aSqlParameterlist.Add(new SqlParameter("@SeparationDate", jobleftDate ?? (object)DBNull.Value));
               
           }







           const string queryStr = @"


DELETE FROM [dbo].[tblContractEmpInfo]
      WHERE ContractualEmpManageId=@ContractualEmpManageId
INSERT INTO [dbo].[tblContractEmpInfo]
           ([CompanyId]
           ,[DivisionId]
           ,[DivisionWId]
           ,[DepartmentId]
           ,[SectionId]
           ,[SubSectionId]
           ,[EmpCategoryId]
           ,[SalaryGradeId]
           ,[SalaryStepId]
           ,[DesignationId]
           ,[DesignationTypeId]
           ,[JobLocationId]
           ,[SalaryLoationId]
           ,[Floor]
           ,[EmpInfoId]
           ,[IsSeparation]
           ,[JobLeftTypeId]
           ,[SeparationDate]
           ,[ContractualEmpManageId])
     VALUES
           (@CompanyId 
           ,@DivisionId 
           ,@DivisionWId 
           ,@DepartmentId 
           ,@SectionId 
           ,@SubSectionId 
           ,@EmpCategoryId 
           ,@SalaryGradeId 
           ,@SalaryStepId 
           ,@DesignationId 
           ,@DesignationTypeId 
           ,@JobLocationId 
           ,@SalaryLoationId 
           ,@Floor 
           ,@EmpInfoId 
           ,@IsSeparation 
           ,@JobLeftTypeId 
           ,@SeparationDate 
           ,@ContractualEmpManageId )

";
           return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
       }

       public bool UpdateContactAppLog(string status, string id)
       {

           try
           {
               int pk = 0;

               //if (id.JdMasterId > 0)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@ContractualEmpManageAppLogId", id));
                   aParameters.Add(new SqlParameter("@ActionStatus", status));


                   string query =
                       @"update tblContractualEmpManageAppLog set ActionStatus=@ActionStatus  where ContractualEmpManageAppLogId = @ContractualEmpManageAppLogId";

                   bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                   return result;

               }

           }
           catch (Exception exception)
           {

               throw exception;
           }
       }
       public int SaveEmpContractAppLogForWard(ContractualEmpManageAppLogDAO appLogDao)
       {

           try
           {
               int pk = 0;


               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@ContractualEmpManageId", appLogDao.ContractualEmpManageId));
                   aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                   aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                   aParameters.Add(new SqlParameter("@Version", appLogDao.Version));
                   aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                   aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                   aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                   aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));
                   aParameters.Add(new SqlParameter("@CommentId", appLogDao.CommentsId));


                   string query = @"INSERT INTO dbo.tblContractualEmpManageAppLog
                                    (
                                    ContractualEmpManageId,
                                    PreEmpInfoId,
                                    ForEmpInfoId,
                                    Version,
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments,CommentId,IsForward,IsForwardApprove
                                    )
                                    VALUES(
                                    @ContractualEmpManageId,
                                    @PreEmpInfoId,
                                    @ForEmpInfoId,
                                    (SELECT (COUNT(*)+1) FROM dbo.tblContractualEmpManageAppLog WHERE ContractualEmpManageId=@ContractualEmpManageId),
                                    @ApproveBy,
                                    @ApproveDate,
                                    @ActionStatus,@Comments,@CommentId,1,0
                                    )";

                   pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
               }


               return pk;
           }
           catch (Exception exception)
           {

               throw exception;
           }
       }
       public int SaveEmpContractAppLog(ContractualEmpManageAppLogDAO appLogDao)
       {

           try
           {
               int pk = 0;


               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@ContractualEmpManageId", appLogDao.ContractualEmpManageId));
                   aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                   aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                   aParameters.Add(new SqlParameter("@Version", appLogDao.Version));
                   aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                   aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                   aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                   aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));
                   aParameters.Add(new SqlParameter("@CommentId", appLogDao.CommentsId));


                   string query = @"INSERT INTO dbo.tblContractualEmpManageAppLog
                                    (
                                    ContractualEmpManageId,
                                    PreEmpInfoId,
                                    ForEmpInfoId,
                                    Version,
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments,CommentId
                                    )
                                    VALUES(
                                    @ContractualEmpManageId,
                                    @PreEmpInfoId,
                                    @ForEmpInfoId,
                                    (SELECT (COUNT(*)+1) FROM dbo.tblContractualEmpManageAppLog WHERE ContractualEmpManageId=@ContractualEmpManageId),
                                    @ApproveBy,
                                    @ApproveDate,
                                    @ActionStatus,@Comments,@CommentId
                                    )";

                   pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
               }


               return pk;
           }
           catch (Exception exception)
           {

               throw exception;
           }
       }



       public int SaveEmpContractAppLogForward(ContractualEmpManageAppLogDAO appLogDao)
       {

           try
           {
               int pk = 0;


               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@ContractualEmpManageId", appLogDao.ContractualEmpManageId));
                   aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                   aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                   aParameters.Add(new SqlParameter("@Version", appLogDao.Version));
                   aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                   aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                   aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                   aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));
                   aParameters.Add(new SqlParameter("@CommentId", appLogDao.CommentsId));
                   aParameters.Add(new SqlParameter("@IsForward", appLogDao.IsForward));


                   string query = @"INSERT INTO dbo.tblContractualEmpManageAppLog
                                    (
                                    ContractualEmpManageId,
                                    PreEmpInfoId,
                                    ForEmpInfoId,
                                    Version,
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments,CommentId,IsForward
                                    )
                                    VALUES(
                                    @ContractualEmpManageId,
                                    @PreEmpInfoId,
                                    @ForEmpInfoId,
                                    (SELECT (COUNT(*)+1) FROM dbo.tblContractualEmpManageAppLog WHERE ContractualEmpManageId=@ContractualEmpManageId),
                                    @ApproveBy,
                                    @ApproveDate,
                                    @ActionStatus,@Comments,@CommentId,@IsForward
                                    )";

                   pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
               }


               return pk;
           }
           catch (Exception exception)
           {

               throw exception;
           }
       }
       public DataTable GetEmpInfo(string param)
       {
           try
           {
               string query = @"SELECT *FROM dbo.tblEmpGeneralInfo " + param + "";

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

       public DataTable GetEmpInfoNextApprover(string param)
       {
           try
           {
               string query = @"SELECT  rpt.EmpMasterCode+ISNULL( ' , '+ rpt. EmpName,'') AwEmpName  FROM dbo.tblEmpGeneralInfo emp
inner join tblEmpGeneralInfo rpt on rpt.EmpInfoId=emp.ReportingEmpId   " + param + "";

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }


       public DataTable GetEmpInfoNextApproverEmp(string param)
       {
           try
           {
               string query = @"SELECT  emp.EmpMasterCode+ISNULL( ' , '+ emp. EmpName,'') AwEmpName  FROM dbo.tblEmpGeneralInfo emp
    " + param + "";

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public DataTable GetEmpInfoPrevious(string forempInfoid, string jdmasterId)
       {
           try
           {
               string query = @"SELECT * FROM dbo.tblContractualEmpManageAppLog WHERE ForEmpInfoId='" + forempInfoid + "' AND ContractualEmpManageId='" + jdmasterId + "' AND ActionStatus NOT IN ('Review') ORDER BY ContractualEmpManageAppLogId DESC ";


              
               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public DataTable GetSupervisorAppId(string url, string param)
       {
           try
           {
               string query = @"SELECT * FROM dbo.tblSupevisorMenuApproval
LEFT JOIN dbo.tblMainMenu ON tblMainMenu.MainMenuId = tblSupevisorMenuApproval.MainMenuId WHERE URL='" + url + "' " + param + "";

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public bool UpdateContractural(ContractualEmpManageDAO aMaster)
       {

           try
           {
               int pk = 0;

               if (aMaster.ContractualEmpManageId > 0)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@ContractualEmpManageId", aMaster.ContractualEmpManageId));
                   aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));


                   string query =
                       @"update tblContractualEmpManage set ActionStatus=@ActionStatus  where ContractualEmpManageId = @ContractualEmpManageId";

                   bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                   return result;

               }

           }
           catch (Exception exception)
           {

               throw exception;
           }
           return true;
       }

       public DataTable GetSupervisorEmployeeAppId(string empinfoId, string fromempInfoId)
       {
           try
           {
               string query = @"SELECT * FROM dbo.tblSupevisorMenuApproval WHERE EmpInfoId='" + empinfoId + "' AND FromEmpInfoId='" + fromempInfoId + "'";

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public DataTable GetHRAdminEmployeeAppId(string parameter)
       {
           try
           {
               string query = @"SELECT * FROM dbo.tblEmployeeApprovalByOpearationDetail
            LEFT JOIN dbo.tblMainMenu ON dbo.tblEmployeeApprovalByOpearationDetail.Operation=dbo.tblMainMenu.MainMenuId " + parameter + "";

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }


       public int SaveComment(string masterId, string empinfoId, string comments)
       {

           try
           {
               int pk = 0;


               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   //aParameters.Add(new SqlParameter("@Id", masterId));
                   aParameters.Add(new SqlParameter("@EmpInfoId", empinfoId));
                   aParameters.Add(new SqlParameter("@Comments", comments));


                   string query = @" INSERT INTO dbo.tblContractualEmpManageAppLogComnt
                                    (
                                        EmpInfoId,
                                        Comments
                                    )
                                    VALUES
                                    (   @EmpInfoId,
                                        @Comments
                                    )";

                   pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
               }


               return pk;
           }
           catch (Exception exception)
           {

               throw exception;
           }
       }



       public bool UpdateEmpSequence(string MainId, bool Isapproved)
       {

           try
           {
               int pk = 0;

               
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@ContractualRoutingPathID", MainId));
                   aParameters.Add(new SqlParameter("@Isapproved", Isapproved)); 


                   string query =
                       @"update tblContractualRoutingPath set Isapproved=@Isapproved, ApprovedDate=GETDATE()  where ContractualRoutingPathID = @ContractualRoutingPathID";

                   bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                   return result;

                

           }
           catch (Exception exception)
           {

               throw exception;
           }
           return true;
       }
       public bool UpdateJobReqStatus2(ContractualEmpManageDAO aMaster)
       {

           try
           {
               int pk = 0;

               if (aMaster.ContractualEmpManageId > 0)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@ContractualEmpManageId", aMaster.ContractualEmpManageId));
                   aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));


                   string query =
                       @"update tblContractualEmpManage set ActionStatus2=@ActionStatus  where ContractualEmpManageId = @ContractualEmpManageId";

                   bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                   return result;

               }

           }
           catch (Exception exception)
           {

               throw exception;
           }
           return true;
       }
       public bool UpdateSelfApprove(int id,bool selfapp)
       {

           try
           {
               int pk = 0;

               if (id > 0)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@ID", id));
                   aParameters.Add(new SqlParameter("@IsSelfApp", selfapp));


                   string query =
                       @"update tblContractualEmpManage set IsSelfApp=@IsSelfApp  where ContractualEmpManageId = @ID";

                   bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                   return result;

               }

           }
           catch (Exception exception)
           {

               throw exception;
           }
           return true;
       }
       public DataTable GetContractualDataInfo(string id)
       {
           //var aSqlParameterlist = new List<SqlParameter>();
           //aSqlParameterlist.Add(new SqlParameter("@Parameter", Parameter));


           string queryStr = @"SELECT *,CONVERT(BIT,(CASE WHEN IsSelfApp IS NULL THEN '0' ELSE '1' END ))IsSelfApp FROM dbo.tblContractualEmpManage
LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=dbo.tblContractualEmpManage.EmployeeId WHERE ContractualEmpManageId='" + id + "'";

           return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
       }
       public void LoadCompanyDropDownList(DropDownList ddl)
       {
           string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
           aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
       }
       public void LoadCompanyDropDownListNew(DropDownList ddl)
       {
           string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo";
           aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
       }

       public Int32 RenewSaveInfo(ContractualEmpManageDAO aContractualEmpManageDAO)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@IsExtension", aContractualEmpManageDAO.IsExtension));

           aSqlParameterlist.Add(new SqlParameter("@IsRenew", aContractualEmpManageDAO.IsRenew));


           aSqlParameterlist.Add(new SqlParameter("@IsPermanentToContractual", aContractualEmpManageDAO.IsPermanentToContractual));
           aSqlParameterlist.Add(new SqlParameter("@IsContractualToPermanent", aContractualEmpManageDAO.IsContractualToPermanent));
     




           aSqlParameterlist.Add(new SqlParameter("@RenewStartDate", aContractualEmpManageDAO.RenewStartDate));
           aSqlParameterlist.Add(new SqlParameter("@RenewToDate", aContractualEmpManageDAO.RenewToDate));
           aSqlParameterlist.Add(new SqlParameter("@IsSalaryIncrement", aContractualEmpManageDAO.IsSalaryIncrement));
           aSqlParameterlist.Add(new SqlParameter("@IsNoIncrement", aContractualEmpManageDAO.IsNoIncrement));
           aSqlParameterlist.Add(new SqlParameter("@IsFacilityIncluded", aContractualEmpManageDAO.IsFacilityIncluded));

           aSqlParameterlist.Add(new SqlParameter("@IsNoFacility", aContractualEmpManageDAO.IsNoIncrement));
           aSqlParameterlist.Add(new SqlParameter("@Remarks", aContractualEmpManageDAO.Remarks));




           aSqlParameterlist.Add(new SqlParameter("@EntryBy", aContractualEmpManageDAO.EntryBy));
           aSqlParameterlist.Add(new SqlParameter("@EntryDate", aContractualEmpManageDAO.EntryDate));

           string insertQuery = @"INSERT INTO [dbo].[tblContractualEmpManage]
           (IsExtension
           ,IsRenew
           ,IsPermanentToContractual
           ,IsContractualToPermanent
 ,RenewStartDate
           ,RenewToDate
           ,IsSalaryIncrement
           ,IsNoIncrement
           ,IsFacilityIncluded
           ,IsNoFacility
           ,Remarks
           ,EntryBy
           ,EntryDate )
     VALUES
           (@IsExtension
           ,@IsRenew
           ,@IsPermanentToContractual
           ,@IsContractualToPermanent
 ,@RenewStartDate
           ,@RenewToDate
           ,@IsSalaryIncrement
           ,@IsNoIncrement
           ,@IsFacilityIncluded
           ,@IsNoFacility
           ,@Remarks
           ,@EntryBy
           ,@EntryDate)";

           return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
       }



       public Int32 PermanentToContractualSaveInfo(ContractualEmpManageDAO aContractualEmpManageDAO)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@IsExtension", aContractualEmpManageDAO.IsExtension));

           aSqlParameterlist.Add(new SqlParameter("@IsRenew", aContractualEmpManageDAO.IsRenew));


           aSqlParameterlist.Add(new SqlParameter("@IsPermanentToContractual", aContractualEmpManageDAO.IsPermanentToContractual));
           aSqlParameterlist.Add(new SqlParameter("@IsContractualToPermanent", aContractualEmpManageDAO.IsContractualToPermanent));
           aSqlParameterlist.Add(new SqlParameter("@PermanentToContractualEffectiveDate", aContractualEmpManageDAO.PermanentToContractualEffectiveDate));




         
           aSqlParameterlist.Add(new SqlParameter("@IsSalaryIncrement", aContractualEmpManageDAO.IsSalaryIncrement));

           aSqlParameterlist.Add(new SqlParameter("@IsNoIncrement", aContractualEmpManageDAO.IsNoIncrement));
           aSqlParameterlist.Add(new SqlParameter("@IsFacilityIncluded", aContractualEmpManageDAO.IsFacilityIncluded));

           aSqlParameterlist.Add(new SqlParameter("@IsNoFacility", aContractualEmpManageDAO.IsNoIncrement));
           aSqlParameterlist.Add(new SqlParameter("@Remarks", aContractualEmpManageDAO.Remarks));




           aSqlParameterlist.Add(new SqlParameter("@EntryBy", aContractualEmpManageDAO.EntryBy));
           aSqlParameterlist.Add(new SqlParameter("@EntryDate", aContractualEmpManageDAO.EntryDate));

           string insertQuery = @"INSERT INTO [dbo].[tblContractualEmpManage]
           (IsExtension
           ,IsRenew
           ,IsPermanentToContractual
           ,IsContractualToPermanent
 ,PermanentToContractualEffectiveDate
           
           ,IsSalaryIncrement
           ,IsNoIncrement
           ,IsFacilityIncluded
           ,IsNoFacility
           ,Remarks
           ,EntryBy
           ,EntryDate )
     VALUES
           (@IsExtension
           ,@IsRenew
           ,@IsPermanentToContractual
           ,@IsContractualToPermanent
 ,@PermanentToContractualEffectiveDate
           
           ,@IsSalaryIncrement
           ,@IsNoIncrement
           ,@IsFacilityIncluded
           ,@IsNoFacility
           ,@Remarks
           ,@EntryBy
           ,@EntryDate)";

           return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
       }


       public Int32 ContractualToPermanentSaveInfo(ContractualEmpManageDAO aContractualEmpManageDAO)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@IsExtension", aContractualEmpManageDAO.IsExtension));

           aSqlParameterlist.Add(new SqlParameter("@IsRenew", aContractualEmpManageDAO.IsRenew));


           aSqlParameterlist.Add(new SqlParameter("@IsPermanentToContractual", aContractualEmpManageDAO.IsPermanentToContractual));
           aSqlParameterlist.Add(new SqlParameter("@IsContractualToPermanent", aContractualEmpManageDAO.IsContractualToPermanent));
           aSqlParameterlist.Add(new SqlParameter("@ContractualToPermanentDate", aContractualEmpManageDAO.ContractualToPermanentDate));




      
           aSqlParameterlist.Add(new SqlParameter("@IsSalaryIncrement", aContractualEmpManageDAO.IsSalaryIncrement));

           aSqlParameterlist.Add(new SqlParameter("@IsNoIncrement", aContractualEmpManageDAO.IsNoIncrement));
           aSqlParameterlist.Add(new SqlParameter("@IsFacilityIncluded", aContractualEmpManageDAO.IsFacilityIncluded));

           aSqlParameterlist.Add(new SqlParameter("@IsNoFacility", aContractualEmpManageDAO.IsNoIncrement));
           aSqlParameterlist.Add(new SqlParameter("@Remarks", aContractualEmpManageDAO.Remarks));




           aSqlParameterlist.Add(new SqlParameter("@EntryBy", aContractualEmpManageDAO.EntryBy));
           aSqlParameterlist.Add(new SqlParameter("@EntryDate", aContractualEmpManageDAO.EntryDate));

           string insertQuery = @"INSERT INTO [dbo].[tblContractualEmpManage]
           (IsExtension
           ,IsRenew
           ,IsPermanentToContractual
           ,IsContractualToPermanent
 ,ContractualToPermanentDate
           
           ,IsSalaryIncrement
           ,IsNoIncrement
           ,IsFacilityIncluded
           ,IsNoFacility
           ,Remarks
           ,EntryBy
           ,EntryDate )
     VALUES
           (@IsExtension
           ,@IsRenew
           ,@IsPermanentToContractual
           ,@IsContractualToPermanent
 ,@ContractualToPermanentDate
           
           ,@IsSalaryIncrement
           ,@IsNoIncrement
           ,@IsFacilityIncluded
           ,@IsNoFacility
           ,@Remarks
           ,@EntryBy
           ,@EntryDate)";

           return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
       }

       public DataTable GetContractualData(string Parameter)
       {
           //var aSqlParameterlist = new List<SqlParameter>();
           //aSqlParameterlist.Add(new SqlParameter("@Parameter", Parameter));


           string queryStr = @"SELECT *,ISNULL(convert(varchar, E.ExtContractDate, 10) ,convert(varchar, E.ContractEndDate, 10)    )AS ContractEndDate FROM tblEmpGeneralInfo E  
 INNER JOIN dbo.tblEmployeeType T ON E.EmpTypeId = T.EmpTypeId
  LEFT JOIN dbo.tblProjectSetup P ON E.ProjectID = P.ProjectId      
 left JOIN dbo.tblDepartment D ON E.DepartmentId = D.DepartmentId 
 INNER JOIN dbo.tblDesignation DS ON E.DesignationId = DS.DesignationId 


                            
                                where ( (E.EmpTypeId=2) OR (E.EmpTypeId=3)) AND E.IsActive=1 AND E.EmployeeStatus='Active'  " + Parameter + "";
         
           return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
       }

       public DataTable GetApprovalComments(string id)
       {
           //var aSqlParameterlist = new List<SqlParameter>();
           //aSqlParameterlist.Add(new SqlParameter("@Parameter", Parameter));


           string queryStr = @"SELECT * from(SELECT Version Seq_No, Alg.ContractualEmpManageAppLogId,   emp.EmpName  PreEmp, '' ForEmp, Version, Us.UserName ApproveBy, CASE   WHEN  Alg.ActionStatus='Review' THEN 'Returned'     WHEN  Alg.ActionStatus='Initiator' THEN 'Initiated' ELSE 'Recommended' END ActionStatus, Alg.ApproveDate, Alg.ContractualEmpManageId, Alg.Comments
  FROM tblContractualEmpManageAppLog Alg WITH (NOLOCK)
LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId=Alg.PreEmpInfoId

LEFT JOIN dbo.tblUser Us ON Alg.ApproveBy=Us.UserId WHERE Version<>2 and  Alg.ActionStatus!= 'Drafted'  AND    Alg.ContractualEmpManageId=" + id + @"

UNION all
SELECT top 1 0 Seq_No, 0  Meeting_MiscellaneousInfoAppLogId, emp2.EmpName PreEmp, '' ForEmp,'' Version,'' ApproveBy, 'Initiated' ActionStatus,masRp.EntryDate ApprovedDate, 0 MiscellaneousInfoId,  '' Comments FROM dbo.tblContractualEmpManage masRp  WITH (NOLOCK)
 
 LEFT JOIN dbo.tblUser Us ON masRp.EntryBy=Us.UserId
LEFT JOIN dbo.tblEmpGeneralInfo emp2 ON emp2.EmpInfoId=Us.EmpInfoId
 WHERE    Us.EmpInfoId   IN(SELECT  top 1 ForEmpInfoId  FROM tblContractualEmpManageAppLog WHERE ContractualEmpManageId=" + id + @" AND PreEmpInfoId=0) AND masRp.ContractualEmpManageId=" + id + @"      UNION all
SELECT top 1 100 Seq_No, 0  Meeting_MiscellaneousInfoAppLogId, emp2.EmpName PreEmp, '' ForEmp,'' Version,'' ApproveBy, 'Approved' ActionStatus,masRp.EntryDate ApprovedDate, 0 MiscellaneousInfoId,  '' Comments FROM dbo.tblContractualEmpManage masRp  WITH (NOLOCK)
 
 LEFT JOIN dbo.tblUser Us ON masRp.EntryBy=Us.UserId
LEFT JOIN dbo.tblEmpGeneralInfo emp2 ON emp2.EmpInfoId=Us.EmpInfoId
 WHERE    Us.EmpInfoId   IN(SELECT  top 1 ForEmpInfoId  FROM tblContractualEmpManageAppLog WHERE ContractualEmpManageId=" + id + @" AND PreEmpInfoId=0 and Version<>1 order by  Version desc
)  ) tblmain  ORDER BY Seq_No ASC  ";

           return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
       }

        //public bool DeleteContractualEmpManageById(string ContractualEmpManageId)UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
       //{
       //    var aSqlParameterlist = new List<SqlParameter>();
       //    aSqlParameterlist.Add(new SqlParameter("@ContractualEmpManageId", ContractualEmpManageId));

       //    const string query = @"DELETE FROM tblContractualEmpManage WHERE ContractualEmpManageId = @ContractualEmpManageId";
       //    return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
       //}


      
    }
}
