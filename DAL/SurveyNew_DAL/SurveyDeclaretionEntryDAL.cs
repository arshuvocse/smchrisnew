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
using Library.DAO.HRM_Entities;

namespace DAL.Transfer_DAL
{
    public class SurveyDeclaretionEntryDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


        public DataTable GeInformationById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SurveyMasterId", Id));

            const string queryStr = @"SELECT * FROM tblSurveyMaster 
                                    WHERE SurveyMasterId=@SurveyMasterId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GeQuestionById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SurveyMasterId", Id));

            const string queryStr = @"  SELECT tblSurveyDetails.SurveyQuestionId AS SurveyQuestionId, tblSurveyQuestion.QuestionTitle, tblSurveyQuestionGroup.SurveyQuestionGroup, tblSurveyQuestionType.SurveyQuestionType  FROM tblSurveyDetails 
	           LEFT JOIN tblSurveyQuestion ON tblSurveyDetails.SurveyQuestionId=tblSurveyQuestion.SurveyQuestionId
	           LEFT JOIN dbo.tblSurveyQuestionGroup ON tblSurveyQuestionGroup.SurveyQuestionGroupId = tblSurveyQuestion.SurveyQuestionGroupId
               LEFT JOIN dbo.tblSurveyQuestionType ON tblSurveyQuestionType.SurveyQuestionTypeId = tblSurveyQuestion.SurveyQuestionTypeId
               WHERE tblSurveyDetails.SurveyMasterId=@SurveyMasterId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GeEmpGeneralInfoById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SurveyMasterId", Id));

            const string queryStr = @"SELECT e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType, * FROM tblSurveyParticipate
 INNER JOIN  dbo.tblEmpGeneralInfo e ON tblSurveyParticipate.EmployeeId = e.EmpInfoId 

 INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
                            left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
                            left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
                            left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
                            left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId 
 WHERE tblSurveyParticipate.SurveyMasterId=@SurveyMasterId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
        public int SaveEntryInfo(SurveyMasterDeclarationDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aEntryDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", aEntryDao.FinancialYearId));
            aSqlParameterlist.Add(new SqlParameter("@SurveyName", aEntryDao.SurveyName));
            aSqlParameterlist.Add(new SqlParameter("@SurveyFrom", aEntryDao.SurveyFrom));
            aSqlParameterlist.Add(new SqlParameter("@SurveyTo", aEntryDao.SurveyTo));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aEntryDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aEntryDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aEntryDao.EntryDate));


            const string queryStr = @"INSERT INTO tblSurveyMaster (CompanyId, FinancialYearId, SurveyName , SurveyFrom, SurveyTo, IsActive, EntryBy,EntryDate)
                                   VALUES (@CompanyId, @FinancialYearId, @SurveyName , @SurveyFrom, @SurveyTo, @IsActive,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public bool DeleteQuestionByById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SurveyMasterId", Id));


            const string queryStr = @"DELETE FROM tblSurveyDetails  WHERE SurveyMasterId = @SurveyMasterId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public bool DeleteEmpParticipateByById(string Id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@SurveyMasterId", Id));


            const string queryStr = @"DELETE FROM tblSurveyParticipate  WHERE SurveyMasterId = @SurveyMasterId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }
        public bool UpdateEntryInfo(SurveyMasterDeclarationDAO aEntryDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SurveyMasterId", aEntryDao.SurveyMasterId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aEntryDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", aEntryDao.FinancialYearId));
            aSqlParameterlist.Add(new SqlParameter("@SurveyName", aEntryDao.SurveyName));
            aSqlParameterlist.Add(new SqlParameter("@SurveyFrom", aEntryDao.SurveyFrom));
            aSqlParameterlist.Add(new SqlParameter("@SurveyTo", aEntryDao.SurveyTo));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aEntryDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aEntryDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aEntryDao.UpdateDate));


            const string queryStr = @"UPDATE tblSurveyMaster SET  CompanyId=@CompanyId, FinancialYearId=@FinancialYearId, SurveyName=@SurveyName , SurveyFrom=@SurveyFrom, SurveyTo =@SurveyTo, IsActive=@IsActive, UpdateBy=@UpdateBy, UpdateDate=@UpdateDate where SurveyMasterId=@SurveyMasterId ";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }
        public void EmployeeNameDropDown(DropDownList ddl, string CompanyId)
       {
           string query = "SELECT * FROM dbo.tblEmpGeneralInfo WHERE CompanyId='" + CompanyId + "'";
           aCommonInternalDal.LoadDropDownValue(ddl, "EmpName", "EmpInfoId", query, "HRDB");
         
       }


        public int SaveSurveyDetailsInfo(SurveyDetailsDeclarationDAO aDao)
        {
            
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

                aSqlParameterlist.Add(new SqlParameter("@SurveyMasterId", aDao.SurveyMasterId));
                aSqlParameterlist.Add(new SqlParameter("@SurveyQuestionId", aDao.SurveyQuestionId));
                string query = @"INSERT INTO [dbo].[tblSurveyDetails]
           ([SurveyMasterId]
           ,[SurveyQuestionId])
     VALUES
           (@SurveyMasterId, 
           @SurveyQuestionId)";

           return     aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
            
        }

        public int SaveSurveyParticipateInfo(SurveyParticipateDeclarationDAO aDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@SurveyMasterId", aDao.SurveyMasterId));
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aDao.EmployeeId));
            string query = @"INSERT INTO [dbo].[tblSurveyParticipate]
           ([SurveyMasterId]
           ,[EmployeeId])
     VALUES
           (@SurveyMasterId, 
           @EmployeeId)";

            return aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);
        }



        public DataTable GetEMpInfos(string param)
        {
            string query = @"SELECT e.EmpInfoId, e.EmpMasterCode, e.EmpName, Dpt.DepartmentName, desig.Designation,  sal.SalaryLocation, Etype.EmpType,  * FROM dbo.tblEmpGeneralInfo e
                            INNER JOIN  dbo.tblCompanyInfo com ON com.CompanyId = e.CompanyId 
                            left JOIN  dbo.tblDepartment Dpt ON Dpt.DepartmentId = e.DepartmentId 
                            left JOIN  dbo.tblDesignation desig ON desig.DesignationId = e.DesignationId 
                            left JOIN  dbo.tblSalaryLocation sal ON sal.SalaryLoationId = e.SalaryLoationId 
                            left JOIN  dbo.tblEmployeeType Etype ON Etype.EmpTypeId = e.EmpTypeId 
                             WHERE   " + param + " ORDER BY e.EmpMasterCode DESC ";

            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetQuestionGVformation()
        {
                        string queryStr = @"SELECT tblSurveyQuestionGroup.SurveyQuestionGroup, tblSurveyQuestionType.SurveyQuestionType, * FROM tblSurveyQuestion
                              LEFT JOIN dbo.tblSurveyQuestionGroup ON tblSurveyQuestionGroup.SurveyQuestionGroupId = tblSurveyQuestion.SurveyQuestionGroupId
                        LEFT JOIN dbo.tblSurveyQuestionType ON tblSurveyQuestionType.SurveyQuestionTypeId = tblSurveyQuestion.SurveyQuestionTypeId  WHERE tblSurveyQuestion.IsActive=1";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable ValidattionForEffectiveDate(string id, string date)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", id));
            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", date));
            string query = @"SELECT EmployeeId, EffectiveDate FROM dbo.tblEmployeePromotionEntry WHERE  EmployeeId=@EmployeeId and EffectiveDate=@EffectiveDate";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable DeleteValidattionForEffectiveDate(string id)
        {
            string query = @"SELECT  EmployeePromotionEntryId, EffectiveDate FROM dbo.tblEmployeePromotionEntry WHERE EmployeePromotionEntryId=" + id;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }
        public DataTable LoadAllInfofById()
        {
            string query = @"SELECT Com.ShortName, Fin.FinancialYearDesc, UserR.UserName EntryBy, UpBY.UserName UpdateBy,  * FROM tblSurveyMaster EPE
  INNER JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
  INNER JOIN dbo.tblFinancialYear  Fin ON EPE.FinancialYearId = Fin.FinancialYearId
 LEFT JOIN dbo.tblUser  UserR ON EPE.EntryBy = UserR.UserId                                  
						  LEFT JOIN dbo.tblUser  UpBY ON EPE.UpdateBy = UpBY.UserId   
Where ( EPE.IsDelete IS NULL OR EPE.IsDelete = 0)";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }



        public DataTable LoadAllInfofByEmployeeIDSingle(string EmpId)
        {
            string query = @"SELECT * FROM tblSurveyParticipate par 
INNER JOIN tblSurveyMaster EPE ON EPE.SurveyMasterId = par.SurveyMasterId 
 INNER JOIN dbo.tblCompanyInfo  Com ON EPE.CompanyId = Com.CompanyId
  INNER JOIN dbo.tblFinancialYear  Fin ON EPE.FinancialYearId = Fin.FinancialYearId
 -- LEFT JOIN  (SELECT SurveyID FROM dbo.tblSurveySubmitMaster GROUP BY SurveyID)tblSur ON EPE.SurveyMasterId=tblSur.SurveyID
-- AND tblSur.SurveyID NOT IN(EPE.SurveyMasterId)

Where ( EPE.IsDelete IS NULL OR EPE.IsDelete = 0)  AND par.EmployeeId=" + EmpId;


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public void FinancialYearDropDown(DropDownList ddl, string CompanyId)
        {
            string query = "SELECT  FinancialYearId, FinancialYearDesc FROM dbo.tblFinancialYear WHERE Status='Active' and CompanyId='" + CompanyId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "FinancialYearDesc", "FinancialYearId", query, "HRDB");

        }

        public void LoadSalaryStepDropDownList(DropDownList ddl)
        {
            string query = "SELECT * FROM dbo.tblSalaryStep  WHERE IsActive=1 ";
            aCommonInternalDal.LoadDropDownValue(ddl, "SalaryStepName", "SalaryStepId", query, "HRDB");

        }
        public void LoadSalaryStepDropDownListOld(DropDownList ddl)
        {
            string query = "SELECT * FROM dbo.tblSalaryStep";
            aCommonInternalDal.LoadDropDownValue(ddl, "SalaryStepName", "SalaryStepId", query, "HRDB");

        }

        public void LoadSalaryStepDropDownListBySalaryGradeId(DropDownList ddl, string SalarygradeId)
        {
            string query = "SELECT * FROM dbo.tblSalaryStep WHERE IsActive=1 and SalaryGradeId='" + SalarygradeId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "SalaryStepName", "SalaryStepId", query, "HRDB");

        }

        public void LoadNewdesignationDropDownList(DropDownList ddl)
        {
            string query = "SELECT * FROM tblDesignation WHERE IsActive=1 ORDER BY Designation  ";
            aCommonInternalDal.LoadDropDownValue(ddl, "Designation", "DesignationId", query, "HRDB");
        }
        public void LoadOlddesignationDropDownList(DropDownList ddl)
        {
            string query = "SELECT * FROM tblDesignation ORDER BY Designation";
            aCommonInternalDal.LoadDropDownValue(ddl, "Designation", "DesignationId", query, "HRDB");
        }

        public void LoadNewdesignationDropDownListBySalaryId(DropDownList ddl, string SalaryGradeId)
        {
           
            string query = "SELECT * FROM tblDesignation WHERE IsActive=1 AND SalaryGradeId='" + SalaryGradeId + "'";
            aCommonInternalDal.LoadDropDownValue(ddl, "Designation", "DesignationId", query, "HRDB");
        }

        public void LoadPreSalaryStepDropDownList(DropDownList ddl)
       {
           string query = " SELECT * FROM dbo.tblSalaryStep";
           aCommonInternalDal.LoadDropDownValue(ddl, "SalaryStepName", "SalaryStepId", query, "HRDB");
       }
       public void LoadPreGradeDropDownList(DropDownList ddl)
       {
           string query = "SELECT * FROM tblEmployeeGrade";
           aCommonInternalDal.LoadDropDownValue(ddl, "GradeName", "GradeId", query, "HRDB");
       }

       public void LoadPreSalaryGradeDropDownList(DropDownList ddl)
       {
           string query = "SELECT * FROM tblSalaryGrade";
           aCommonInternalDal.LoadDropDownValue(ddl, "GradeCode", "SalaryGradeId", query, "HRDB");
       }

       public void LoadNewSalaryGradeDropDownList(DropDownList ddl)
       {
           string query = "SELECT * FROM tblSalaryGrade WHERE IsActive=1";
           aCommonInternalDal.LoadDropDownValue(ddl, "GradeCode", "SalaryGradeId", query, "HRDB");
       }


           public void LoadPromotionTypeDropDownList(DropDownList ddl)
       {
           string query = "SELECT * FROM tblPromotionType";
           aCommonInternalDal.LoadDropDownValue(ddl, "PromotionTypeName", "PromotionTypeId", query, "HRDB");
       }



       public DataTable LoadEmpJInfoInTextBoxById(int id)
       {
           string query = @" SELECT  deg.Designation,SG.GradeName,    Egf.ReportingEmpId,    *  FROM dbo.tblEmpGeneralInfo Egf

							left JOIN dbo.tblDesignation  deg ON Egf.DesignationId=deg.DesignationId
							left JOIN dbo.tblSalaryGrade  SG ON Egf.SalaryGradeId=SG.SalaryGradeId
							left JOIN dbo.tblCompanyInfo  Com ON Egf.CompanyId=Com.CompanyId
							
			
							
							 WHERE Egf.EmpInfoId='" + id + "'";


           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }



       public Int32 EmployeePromotionEntrySaveInfo(tblEmployeePromotionEntryDAO aEmployeePromotionEntryDAO)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aEmployeePromotionEntryDAO.EmployeeId));
           aSqlParameterlist.Add(new SqlParameter("@CompanyId", aEmployeePromotionEntryDAO.CompanyId));
           aSqlParameterlist.Add(new SqlParameter("@DivisionId", (object)aEmployeePromotionEntryDAO.DivisionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DivisionWId", (object)aEmployeePromotionEntryDAO.DivisionWId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DepartmentId", (object)aEmployeePromotionEntryDAO.DepartmentId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SectionId", (object)aEmployeePromotionEntryDAO.SectionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SubSectionId", (object)aEmployeePromotionEntryDAO.SubSectionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", (object)aEmployeePromotionEntryDAO.SalaryLoationId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@JobLocationId", (object)aEmployeePromotionEntryDAO.JobLocationId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", (object)aEmployeePromotionEntryDAO.EmpTypeId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmployeeCode", (object)aEmployeePromotionEntryDAO.EmployeeCode ?? DBNull.Value));



           aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", (object)aEmployeePromotionEntryDAO.FinancialYearId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@PSalGradeId", (object)aEmployeePromotionEntryDAO.PSalGradeId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@PDesignationId", (object)aEmployeePromotionEntryDAO.PDesignationId ?? DBNull.Value));



           aSqlParameterlist.Add(new SqlParameter("@NDesignationId", (object)aEmployeePromotionEntryDAO.NDesignationId ?? DBNull.Value));
          
           aSqlParameterlist.Add(new SqlParameter("@NSalGradeId", (object)aEmployeePromotionEntryDAO.NSalGradeId ?? DBNull.Value));
           
          
       
           
           aSqlParameterlist.Add(new SqlParameter("@NPromoTypeId", aEmployeePromotionEntryDAO.NPromoTypeId));
           aSqlParameterlist.Add(new SqlParameter("@EntryBy", aEmployeePromotionEntryDAO.EntryBy));
           aSqlParameterlist.Add(new SqlParameter("@EntryDate", aEmployeePromotionEntryDAO.EntryDate));


         

           aSqlParameterlist.Add(new SqlParameter("@PRepEmpId", (object)aEmployeePromotionEntryDAO.PRepEmpId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@NRepEmpId", (object)aEmployeePromotionEntryDAO.NRepEmpId ?? DBNull.Value));


           aSqlParameterlist.Add(new SqlParameter("@Remarks", aEmployeePromotionEntryDAO.Remarks));
           aSqlParameterlist.Add(new SqlParameter("@IsPromotion", aEmployeePromotionEntryDAO.IsPromotion));

           aSqlParameterlist.Add(new SqlParameter("@PStepId", (object)aEmployeePromotionEntryDAO.PStepId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@NSalaryStepId", (object)aEmployeePromotionEntryDAO.NSalaryStepId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@Effectivedate", (object)aEmployeePromotionEntryDAO.Effectivedate ?? DBNull.Value));

           






           string insertQuery = @"INSERT INTO [dbo].[tblEmployeePromotionEntry]
           (CompanyId
           ,EmployeeId
           ,FinancialYearId
           ,PSalGradeId
           ,PDesignationId
           ,NSalGradeId
           ,NDesignationId
           
          
           ,NPromoTypeId
           ,EntryBy
           ,EntryDate
           ,PRepEmpId
           ,NRepEmpId
           ,Remarks,PStepId, EmployeeCode,  DivisionId, DivisionWId, DepartmentId,  SectionId, SubSectionId ,SalaryLoationId, JobLocationId , EmpTypeId, IsPromotion, Effectivedate, NSalaryStepId)
     VALUES
           (@CompanyId
           ,@EmployeeId
           ,@FinancialYearId
           ,@PSalGradeId
           ,@PDesignationId
           ,@NSalGradeId
           ,@NDesignationId           
           ,@NPromoTypeId
           ,@EntryBy
           ,@EntryDate
           ,@PRepEmpId
           ,@NRepEmpId
           ,@Remarks,@PStepId, @EmployeeCode,  @DivisionId, @DivisionWId, @DepartmentId,  @SectionId, @SubSectionId ,@SalaryLoationId, @JobLocationId , @EmpTypeId,  @IsPromotion, @Effectivedate, @NSalaryStepId)";

           return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
       }

       public Int32 EmpTransferAndRedesignationDSSaveInfo(EmpTransferAndRedesignationDao aEmpTransferAndDao)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpTransferAndRedesignationId", aEmpTransferAndDao.EmpTransferAndRedesignationId));

           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aEmpTransferAndDao.EmpInfoId));

           string insertQuery = @"INSERT INTO [dbo].[tblEmployeePromotionEntryDS]
           (EmployeePromotionEntryId
           ,EmpInfoId
           )
           VALUES
           (@EmpTransferAndRedesignationId
           ,@EmpInfoId)";

           return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
       }
       public Int32 EmpTransferAndRedesignationPSSaveInfo(EmpTransferAndRedesignationDao aEmpTransferAndDao)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpTransferAndRedesignationId", aEmpTransferAndDao.EmpTransferAndRedesignationId));

           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aEmpTransferAndDao.EmpInfoId));

           string insertQuery = @"INSERT INTO [dbo].[tblEmployeePromotionEntryPS]
           (EmployeePromotionEntryId
           ,EmpInfoId
           )
     VALUES
           (@EmpTransferAndRedesignationId
           ,@EmpInfoId)";

           return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
       }

       public bool DeleteDirectlyS(string id)
       {
           string query = "DELETE FROM tblEmployeePromotionEntryDS WHERE EmployeePromotionEntryId='" + id +
                          "'";
           return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
       }
       public bool DeletePS(string id)
       {
           string query = "DELETE FROM tblEmployeePromotionEntryPS WHERE EmployeePromotionEntryId='" + id +
                          "'";
           return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
       }
       public DataTable EmpTransferAndRedesignationDS(string id)
       {
           string queryStr = @"SELECT * FROM tblEmployeePromotionEntryDS
LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=tblEmployeePromotionEntryDS.EmpInfoId WHERE EmployeePromotionEntryId='" + id + "'";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }

       public DataTable LoadInformationALl(string param)
       {
           string queryStr = @"  SELECT  Emp.EmpMasterCode,Emp.EmpName,  DEG.Designation,    NDEG.DepartmentName , PT.PromotionTypeName ,  * From tblEmployeePromotionEntry EPE
 left JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId

  left JOIN dbo.tblDesignation  DEG ON Emp.DesignationId = DEG.DesignationId
 
 
  left JOIN dbo.tblDepartment  NDEG ON Emp.DepartmentId = NDEG.DepartmentId
  
 
  
    left JOIN  dbo.tblCompanyInfo com ON com.CompanyId = EPE.CompanyId 
	  left JOIN dbo.tblPromotionType PT ON EPE.NPromoTypeId =PT.PromotionTypeId
    " + param + " ORDER BY EPE.EmployeePromotionEntryId DESC  ";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }

       public bool DeleteEmployeePromotionEntryById(string companyId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmployeePromotionEntryId", companyId));

           const string query = @"DELETE FROM tblEmployeePromotionEntry WHERE EmployeePromotionEntryId = @EmployeePromotionEntryId";
           return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
       }


       public DataTable GetEmployeePromotionEntryIdById(string id)
       {
           string query = @"SELECT  EG.EmpName AS EmployeeName, EGNewReportEmp.EmpName NewReportingEmployeeName,tblEmployeePromotionEntry.NRepEmpId,FinancialYearId, SLoc.SalaryLocation, JLoc.Location, * FROM tblEmployeePromotionEntry
left JOIN dbo.tblEmpGeneralInfo EG ON tblEmployeePromotionEntry.EmployeeId= EG.EmpInfoId 
left JOIN dbo.tblEmpGeneralInfo EGNewReportEmp ON tblEmployeePromotionEntry.NRepEmpId= EGNewReportEmp.EmpInfoId 
LEFT JOIN dbo.tblDesignation  deg ON EG.DesignationId=deg.DesignationId
							LEFT JOIN dbo.tblSalaryGrade  SG ON EG.SalaryGradeId=SG.SalaryGradeId
							LEFT JOIN dbo.tblDivision  Div ON EG.DivisionId=Div.DivisionId
							LEFT JOIN dbo.tblDivisionWing  Wing ON EG.DivisionWId=Wing.DivisionWId
							LEFT JOIN dbo.tblSection  Sec ON EG.SectionId=Sec.SectionId
							LEFT JOIN dbo.tblSubSection  SubSec ON EG.SubSectionId=SubSec.SubSectionId						
								LEFT JOIN dbo.tblDepartment  Dpt ON EG.DepartmentId=Dpt.DepartmentId
																LEFT JOIN dbo.tblSalaryLocation  SLoc ON EG.SalaryLoationId=SLoc.SalaryLoationId
														LEFT JOIN dbo.tblJobLocation  JLoc ON EG.JobLocationId=JLoc.JobLocationID
 WHERE  EmployeePromotionEntryId='" + id + "'";


           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }







       public bool EmployeePromotionEntryUpsateInfo(tblEmployeePromotionEntryDAO aEmployeePromotionEntryDAO)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmployeePromotionEntryId", aEmployeePromotionEntryDAO.EmployeePromotionEntryId));
           aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aEmployeePromotionEntryDAO.EmployeeId));
           aSqlParameterlist.Add(new SqlParameter("@CompanyId", aEmployeePromotionEntryDAO.CompanyId));
           aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", aEmployeePromotionEntryDAO.FinancialYearId));
           aSqlParameterlist.Add(new SqlParameter("@PSalGradeId", (object)aEmployeePromotionEntryDAO.PSalGradeId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@PDesignationId", (object)aEmployeePromotionEntryDAO.PDesignationId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@NDesignationId", (object)aEmployeePromotionEntryDAO.NDesignationId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@PStepId", (object)aEmployeePromotionEntryDAO.PStepId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@NSalGradeId", (object)aEmployeePromotionEntryDAO.NSalGradeId ?? DBNull.Value));
         
           aSqlParameterlist.Add(new SqlParameter("@Effectivedate", (object)aEmployeePromotionEntryDAO.Effectivedate ?? DBNull.Value));


           aSqlParameterlist.Add(new SqlParameter("@NPromoTypeId", (object)aEmployeePromotionEntryDAO.NPromoTypeId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DivisionId", (object)aEmployeePromotionEntryDAO.DivisionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DivisionWId", (object)aEmployeePromotionEntryDAO.DivisionWId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DepartmentId", (object)aEmployeePromotionEntryDAO.DepartmentId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SectionId", (object)aEmployeePromotionEntryDAO.SectionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SubSectionId", (object)aEmployeePromotionEntryDAO.SubSectionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", (object)aEmployeePromotionEntryDAO.SalaryLoationId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@JobLocationId", (object)aEmployeePromotionEntryDAO.JobLocationId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", (object)aEmployeePromotionEntryDAO.EmpTypeId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmployeeCode", (object)aEmployeePromotionEntryDAO.EmployeeCode ?? DBNull.Value));


           aSqlParameterlist.Add(new SqlParameter("@PRepEmpId", (object)aEmployeePromotionEntryDAO.PRepEmpId ?? DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@NRepEmpId", (object)aEmployeePromotionEntryDAO.NRepEmpId ?? DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@Remarks", (object)aEmployeePromotionEntryDAO.Remarks ?? DBNull.Value));


           aSqlParameterlist.Add(new SqlParameter("@UpdateBy", (object)aEmployeePromotionEntryDAO.UpdateBy ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@UpdateDate", (object)aEmployeePromotionEntryDAO.UpdateDate ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@NSalaryStepId", (object)aEmployeePromotionEntryDAO.NSalaryStepId ?? DBNull.Value));

           string UpdateQuery = @"UPDATE [dbo].[tblEmployeePromotionEntry]
   SET [CompanyId] =  @CompanyId
      ,[EmployeeId] =  @EmployeeId
      ,[FinancialYearId] =  @FinancialYearId
      ,[PSalGradeId] =  @PSalGradeId
      ,[PDesignationId] =  @PDesignationId
      ,[NSalGradeId] =  @NSalGradeId
      ,[NDesignationId] =  @NDesignationId
    
    
      ,[NPromoTypeId] =  @NPromoTypeId
   
      ,[UpdateBy] =  @UpdateBy
      ,[UpdateDate] =  @UpdateDate
      ,[PRepEmpId] = @PRepEmpId
      ,[NRepEmpId] =  @NRepEmpId
      ,[Remarks] =  @Remarks,  DivisionId=@DivisionId, DivisionWId=@DivisionWId, DepartmentId=@DepartmentId,  SectionId=@SectionId, SubSectionId=@SubSectionId,  SalaryLoationId=@SalaryLoationId, JobLocationId=@JobLocationId , EmpTypeId=@EmpTypeId,EmployeeCode=@EmployeeCode, NSalaryStepId=@NSalaryStepId   WHERE EmployeePromotionEntryId=@EmployeePromotionEntryId";

           return aCommonInternalDal.UpdateDataByUpdateCommand(UpdateQuery, aSqlParameterlist, "HRDB");
       }

       public void GetCompanyListShortNameIntoDropdown(DropDownList ddl)
       {
           string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
           aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
       }
       public DataTable GetEmployeePromotion(string empinfoId)
       {
           string query = @"SELECT * FROM dbo.tblEmployeePromotionEntry WHERE EmployeeId='" + empinfoId + "' AND (IsPromotion IS NULL OR IsPromotion='0')";

           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
       public DataTable LoadSuperviseEmployee(string id)
       {
           string query = @"SELECT * FROM dbo.tblEmpGeneralInfo WHERE ReportingEmpId='" + id + "'";


           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }

       public DataTable CheckStartEndDateExistOrNotDAL(string FinancialYearId, string StartDate, string EndDate)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", FinancialYearId));
           aSqlParameterlist.Add(new SqlParameter("@StartDate", StartDate));
           aSqlParameterlist.Add(new SqlParameter("@EndDate", EndDate));


           const string queryStr = @"
SELECT FinancialYearId ,
       StartDate ,
       EndDate 
       FROM tblFinancialYear  WHERE  FinancialYearId=@FinancialYearId and StartDate <= @StartDate AND EndDate >= @EndDate";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }
       public bool DeleteUpdateEmployeePromotionEntryById(string id)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@ID", id));

           string query = @"UPDATE dbo.tblEmployeePromotionEntry SET IsDelete='1',DeleteBy='"+HttpContext.Current.Session["UserId"].ToString()+"',DeleteDate='"+DateTime.Now+"' WHERE EmployeePromotionEntryId=@ID";
           return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
       }
       public DataTable GetEmployeePromotionInfoDALrpt(string Id)
       {

           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmployeePromotionEntryId", Id));
           const string queryStr = @"SELECT  Sec.SectionName SASectionName, SubSec.SubSectionName SSubSectionName, Loc.SalaryLocation SSalaryLocation, JLoc.Location SLocation, Dpt.DepartmentName SDepartmentName, EG.DateOfJoin SDateOfJoin, Div.DivisionName SDivisionName, Wing.DivisionWingName SDivisionWingName,  EG.EmpMasterCode, EG.EmpName SearchEmpName,  EGOldReportEmp.EmpName oldReportingEmployeeName, NproTpe.PromotionTypeName, NSG.GradeName NewGradeName, NDSG.Designation NewDesignation, PSG.GradeName PreGradeName, PDSG.Designation PreDesignation, 
 Fyear.FinancialYearDesc,  com.ShortName, EG.EmpName AS EmployeeName, EGNewReportEmp.EmpName NewReportingEmployeeName,tblEmployeePromotionEntry.NRepEmpId,
  tblEmployeePromotionEntry.IsPromotion, * FROM tblEmployeePromotionEntry
left JOIN dbo.tblEmpGeneralInfo EG ON tblEmployeePromotionEntry.EmployeeId= EG.EmpInfoId 
left JOIN dbo.tblEmpGeneralInfo EGNewReportEmp ON tblEmployeePromotionEntry.NRepEmpId= EGNewReportEmp.EmpInfoId 
left JOIN dbo.tblEmpGeneralInfo EGOldReportEmp ON tblEmployeePromotionEntry.PRepEmpId= EGOldReportEmp.EmpInfoId 
LEFT JOIN dbo.tblDesignation  deg ON EG.DesignationId=deg.DesignationId
							LEFT JOIN dbo.tblSalaryGrade  SG ON EG.SalaryGradeId=SG.SalaryGradeId
							LEFT JOIN dbo.tblDivision  Div ON EG.DivisionId=Div.DivisionId
							LEFT JOIN dbo.tblDivisionWing  Wing ON EG.DivisionWId=Wing.DivisionWId
							left JOIN dbo.tblSalaryLocation  Loc ON EG.SalaryLoationId=Loc.SalaryLoationId
							left JOIN dbo.tblJobLocation  JLoc ON EG.JobLocationId=JLoc.JobLocationID
							LEFT JOIN dbo.tblSection  Sec ON EG.SectionId=Sec.SectionId
							LEFT JOIN dbo.tblSubSection  SubSec ON EG.SubSectionId=SubSec.SubSectionId						
								LEFT JOIN dbo.tblDepartment  Dpt ON EG.DepartmentId=Dpt.DepartmentId
								LEFT JOIN dbo.tblCompanyInfo  com ON tblEmployeePromotionEntry.CompanyId=com.CompanyId
								LEFT JOIN dbo.tblFinancialYear  Fyear ON tblEmployeePromotionEntry.FinancialYearId=Fyear.FinancialYearId

									LEFT JOIN dbo.tblSalaryGrade  PSG ON tblEmployeePromotionEntry.PSalGradeId=PSG.SalaryGradeId
									LEFT JOIN dbo.tblDesignation  PDSG ON tblEmployeePromotionEntry.PDesignationId=PDSG.DesignationId

									LEFT JOIN dbo.tblSalaryGrade  NSG ON tblEmployeePromotionEntry.NSalGradeId=NSG.SalaryGradeId
									LEFT JOIN dbo.tblDesignation  NDSG ON tblEmployeePromotionEntry.NDesignationId=NDSG.DesignationId
									LEFT JOIN dbo.tblPromotionType  NproTpe ON tblEmployeePromotionEntry.NPromoTypeId=NproTpe.PromotionTypeId
 WHERE  EmployeePromotionEntryId=@EmployeePromotionEntryId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, DataBase.HRDB);
       }

       public bool UpdateEmployeeExitInfo(EmpGeneralInfo aInfo)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           aSqlParameterlist.Add(new SqlParameter("@EmpGradeId", aInfo.EmpGradeId));
           aSqlParameterlist.Add(new SqlParameter("@SalScaleId", aInfo.SalScaleId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DesigId", aInfo.DesigId));
           aSqlParameterlist.Add(new SqlParameter("@RptBodyId", aInfo.LineId));
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aInfo.EmpInfoId));

           string query = @"UPDATE tblEmpGeneralInfo SET SalaryGradeId = @EmpGradeId,SalaryStepId = @SalScaleId,DesignationId = @DesigId,ReportingEmpId = @RptBodyId WHERE EmpInfoId =  @EmpInfoId";
           return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, DataBase.HRDB);
       }

       public bool UpdateEmployeeSuperVisorId(int empId)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", empId)); 

           string query = @"UPDATE tblEmpGeneralInfo SET ReportingEmpId = @RptBodyId WHERE EmpInfoId =  @EmpInfoId";
           return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, DataBase.HRDB);
       }
    }
}
