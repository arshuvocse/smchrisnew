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
   public class tblEmployeePromotionEntryDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        tblEmployeePromotionEntryDAO atblEmployeePromotionEntryDAO = new tblEmployeePromotionEntryDAO();
        public void EmployeeNameDropDown(DropDownList ddl, string CompanyId)
       {
           string query = "SELECT * FROM dbo.tblEmpGeneralInfo WHERE CompanyId='" + CompanyId + "'";
           aCommonInternalDal.LoadDropDownValue(ddl, "EmpName", "EmpInfoId", query, "HRDB");
         
       }
        public DataTable RPTPreLoadSuperviseEmployee(string id)
        {
            string query = @"SELECT   tblEmpGeneralInfo.EmpMasterCode , tblEmpGeneralInfo.EmpName FROM dbo.tblEmployeePromotionEntryPS
LEFT JOIN dbo.tblEmpGeneralInfo ON tblEmpGeneralInfo.EmpInfoId = tblEmployeePromotionEntryPS.EmpInfoId WHERE tblEmployeePromotionEntryPS.EmployeePromotionEntryId='" + id + "' ORDER BY tblEmpGeneralInfo.EmpMasterCode";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable RPTNewEmpTransferAndRedesignationDS(string id)
        {
            string queryStr = @"SELECT  tblEmpGeneralInfo.EmpMasterCode , tblEmpGeneralInfo.EmpName FROM tblEmployeePromotionEntryDS
LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=tblEmployeePromotionEntryDS.EmpInfoId WHERE EmployeePromotionEntryId='" + id + "'";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public DataTable ValidattionForEffectiveDate(string id, string date)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@EmployeeId", id));
            aSqlParameterlist.Add(new SqlParameter("@EffectiveDate", date));
            string query = @"SELECT EmployeeId, EffectiveDate FROM dbo.tblEmployeePromotionEntry WHERE  (IsDelete = 0 OR IsDelete IS NULL) AND   EmployeeId = @EmployeeId and EffectiveDate=@EffectiveDate";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable DeleteValidattionForEffectiveDate(string id)
        {
            string query = @"SELECT  EmployeePromotionEntryId, EffectiveDate FROM dbo.tblEmployeePromotionEntry WHERE EmployeePromotionEntryId=" + id;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }

        public DataTable editValidattionForEffectiveDate(string id)
        {
            string query = @"SELECT top 1 EmployeePromotionEntryId, EffectiveDate,EmployeeId FROM dbo.tblEmployeePromotionEntry  WHERE EmployeeId=" + id + " order by  EffectiveDate desc ";
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }
        public DataTable editValidattionForEffectiveDate2(string id)
        {
            string query = @"SELECT  EmployeePromotionEntryId, EffectiveDate,EmployeeId FROM dbo.tblEmployeePromotionEntry  WHERE EmployeePromotionEntryId=" + id;
            return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
        }
        public DataTable LoadEmpAllInfofById(int id)
        {
            string query = @" SELECT Egf.EmpMasterCode, Egf.EmpName, Egf.DateOfJoin,   deg.Designation, SG.GradeName SalaryGrade, Com.CompanyName, Div.DivisionName, Wing.DivisionWingName,  Dpt.DepartmentName,     Sec.SectionName, SubSec.SubSectionName, Loc.SalaryLocation, JLoc.Location,  *  FROM dbo.tblEmpGeneralInfo Egf

							left JOIN dbo.tblDesignation  deg ON Egf.DesignationId=deg.DesignationId
							left JOIN dbo.tblSalaryGrade  SG ON Egf.SalaryGradeId=SG.SalaryGradeId
							left JOIN dbo.tblCompanyInfo  Com ON Egf.CompanyId=Com.CompanyId
							left JOIN dbo.tblSalaryLocation  Loc ON Egf.SalaryLoationId=Loc.SalaryLoationId
							left JOIN dbo.tblJobLocation  JLoc ON Egf.JobLocationId=JLoc.JobLocationID
							left JOIN dbo.tblDivision  Div ON Egf.DivisionId=Div.DivisionId
							LEFT JOIN dbo.tblDivisionWing  Wing ON Egf.DivisionWId=Wing.DivisionWId
							left JOIN dbo.tblSection  Sec ON Egf.SectionId=Sec.SectionId
							LEFT JOIN dbo.tblSubSection  SubSec ON Egf.SubSectionId=SubSec.SectionId						
								LEFT JOIN dbo.tblDepartment  Dpt ON Egf.DepartmentId=Dpt.DepartmentId
							
		 
							
							 WHERE Egf.EmpInfoId='" + id + "'";


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

       public bool UpdateSelfApprove(int id, bool selfapp)
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
                       @"update tblEmployeePromotionEntry set IsSelfApp=@IsSelfApp  where EmployeePromotionEntryId = @ID";

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

       public bool UpdateContractural(tblEmployeePromotionEntryDAO aMaster)
       {

           try
           {
               int pk = 0;

               if (aMaster.EmployeePromotionEntryId > 0)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@IncrementId", aMaster.EmployeePromotionEntryId));
                   aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));


                   string query =
                       @"update tblEmployeePromotionEntry set ActionStatus=@ActionStatus  where EmployeePromotionEntryId = @IncrementId";

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

       public bool UpdateEmail(int empInfoId, string personalEmail, string officialEmail)
       {

           try
           {
               int pk = 0;

               if (empInfoId > 0)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@empInfoId", empInfoId));
                   aParameters.Add(new SqlParameter("@personalEmail", personalEmail)); 
                   aParameters.Add(new SqlParameter("@officialEmail", officialEmail)); 


                   string query =
                       @"UPDATE [dbo].[tblEmpGeneralInfo]
   SET     [PersonalEmail] = @PersonalEmail,
       [OfficialEmail] = @OfficialEmail
       
 WHERE EmpInfoId=@empInfoId";

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
       public DataTable GetEmpInfo(string param)
       {
           try
           {
               string query = @"SELECT * FROM dbo.tblEmpGeneralInfo " + param + "";

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public DataTable GetContractualDataInfo(string id)
       {
           //var aSqlParameterlist = new List<SqlParameter>();
           //aSqlParameterlist.Add(new SqlParameter("@Parameter", Parameter));


           string queryStr = @"
SELECT   CONVERT(BIT,(CASE WHEN IsSelfApp IS NULL or IsSelfApp=0 THEN '0' ELSE '1' END ))IsSelfApp,* FROM dbo.tblEmployeePromotionEntry
LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=tblEmployeePromotionEntry.EmployeeId WHERE  EmployeePromotionEntryId='" + id + "'";

           return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
       }
       public bool UpdateJobReqStatus2(tblEmployeePromotionEntryDAO aMaster)
       {

           try
           {
               int pk = 0;

               if (aMaster.EmployeePromotionEntryId > 0)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@IncrementId", aMaster.EmployeePromotionEntryId));
                   aParameters.Add(new SqlParameter("@ActionStatus", aMaster.ActionStatus));


                   string query =
                       @"update tblEmployeePromotionEntry set ActionStatus2=@ActionStatus  where EmployeePromotionEntryId = @IncrementId";

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


       public DataTable GetDataReviewEntryBy(string id, string entryby, string actionstatu)
       {
           //var aSqlParameterlist = new List<SqlParameter>();
           //aSqlParameterlist.Add(new SqlParameter("@Parameter", Parameter));


           string queryStr = @"SELECT * FROM dbo.tblEmployeePromotionEntry WHERE ActionStatus='" + actionstatu + "' AND EntryBy='" + entryby + "' AND EmployeePromotionEntryId='" + id + "'";

           return aCommonInternalDal.DataContainerDataTable(queryStr, DataBase.HRDB);
       }
       public bool UpdateAppLog(string status, string id)
       {

           try
           {
               int pk = 0;

               //if (id.JdMasterId > 0)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@IncrementAppLogId", id));
                   aParameters.Add(new SqlParameter("@ActionStatus", status));


                   string query =
                       @"update tblEmployeePromotionEntryAppLog set ActionStatus=@ActionStatus  where EmployeePromotionEntryIdAppLogId = @IncrementAppLogId";

              

                   bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);
                   return result;

               }

           }
           catch (Exception exception)
           {

               throw exception;
           }
       }
       public DataTable GetEmpInfoPrevious(string forempInfoid, string jdmasterId)
       {
           try
           {
               string query = @"SELECT * FROM dbo.tblEmployeePromotionEntryAppLog WHERE ForEmpInfoId='" + forempInfoid + "' AND EmployeePromotionEntryId='" + jdmasterId + "' AND ActionStatus NOT IN ('Review')  ORDER BY EmployeePromotionEntryIdAppLogId DESC";

  

               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }
       public int SavAppLog(EmployeePromotionEntryAppLogDAO appLogDao)
       {

           try
           {
               int pk = 0;


               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@IncrementId", appLogDao.EmployeePromotionEntryId));
                   aParameters.Add(new SqlParameter("@PreEmpInfoId", appLogDao.PreEmpInfoId));
                   aParameters.Add(new SqlParameter("@ForEmpInfoId", appLogDao.ForEmpInfoId));
                   aParameters.Add(new SqlParameter("@Version", appLogDao.Version));
                   aParameters.Add(new SqlParameter("@ApproveBy", appLogDao.ApproveBy));
                   aParameters.Add(new SqlParameter("@ApproveDate", appLogDao.ApproveDate));
                   aParameters.Add(new SqlParameter("@ActionStatus", appLogDao.ActionStatus));
                   aParameters.Add(new SqlParameter("@Comments", appLogDao.Comments));
                   aParameters.Add(new SqlParameter("@CommentsId", appLogDao.CommentsId));


                   string query = @"INSERT INTO dbo.tblEmployeePromotionEntryAppLog
                                    (
                                    EmployeePromotionEntryId,
                                    PreEmpInfoId,
                                    ForEmpInfoId,
                                    Version,
                                    ApproveBy,
                                    ApproveDate,
                                    ActionStatus,Comments,CommentId
                                    )
                                    VALUES(
                                    @IncrementId,
                                    @PreEmpInfoId,
                                    @ForEmpInfoId,
                                    (SELECT (COUNT(*)+1) FROM dbo.tblEmployeePromotionEntryAppLog WHERE EmployeePromotionEntryId=@IncrementId),
                                    @ApproveBy,
                                    @ApproveDate,
                                    @ActionStatus,@Comments,@CommentsId
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


                   string query = @" INSERT INTO dbo.tblEmployeePromotionEntryAppLogComnt
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
           aSqlParameterlist.Add(new SqlParameter("@NPromoTypeId", (object)aEmployeePromotionEntryDAO.NPromoTypeId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EntryBy", aEmployeePromotionEntryDAO.EntryBy));
           aSqlParameterlist.Add(new SqlParameter("@EntryDate", aEmployeePromotionEntryDAO.EntryDate));
           aSqlParameterlist.Add(new SqlParameter("@PRepEmpId", (object)aEmployeePromotionEntryDAO.PRepEmpId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@NRepEmpId", (object)aEmployeePromotionEntryDAO.NRepEmpId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@Remarks", aEmployeePromotionEntryDAO.Remarks));
           aSqlParameterlist.Add(new SqlParameter("@IsPromotion", (object)aEmployeePromotionEntryDAO.IsPromotion ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@PStepId", (object)aEmployeePromotionEntryDAO.PStepId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@NSalaryStepId", (object)aEmployeePromotionEntryDAO.NSalaryStepId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@Effectivedate", (object)aEmployeePromotionEntryDAO.Effectivedate ?? DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@AutoProcess", (object)aEmployeePromotionEntryDAO.AutoProcess ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsReappointment", (object)aEmployeePromotionEntryDAO.IsReappointment));

           aSqlParameterlist.Add(new SqlParameter("@IsB_DirectlySupervisor", (object)aEmployeePromotionEntryDAO.IsB_DirectlySupervisor ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsB_ReportingBody", (object)aEmployeePromotionEntryDAO.IsB_ReportingBody ?? DBNull.Value));



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
           ,Remarks,PStepId, EmployeeCode,  DivisionId, DivisionWId, DepartmentId,  SectionId, SubSectionId ,
           SalaryLoationId, JobLocationId , EmpTypeId, IsPromotion, Effectivedate, NSalaryStepId,AutoProcess, IsReappointment,IsB_DirectlySupervisor,IsB_ReportingBody )
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
           ,@Remarks,@PStepId, @EmployeeCode,  @DivisionId, @DivisionWId, @DepartmentId,  @SectionId, @SubSectionId ,
           @SalaryLoationId, @JobLocationId , @EmpTypeId,  @IsPromotion, @Effectivedate, @NSalaryStepId, @AutoProcess, @IsReappointment,@IsB_DirectlySupervisor,@IsB_ReportingBody)";

           return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
       }

       public Int32 EmpTransferAndRedesignationDSSaveInfo(EmpTransferAndRedesignationDao aEmpTransferAndDao)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmpTransferAndRedesignationId", aEmpTransferAndDao.EmpTransferAndRedesignationId));
           aSqlParameterlist.Add(new SqlParameter("@PrevEmpReportingBodyId", (object) aEmpTransferAndDao.PrevEmpReportingBodyId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aEmpTransferAndDao.EmpInfoId));

           string insertQuery = @"INSERT INTO [dbo].[tblEmployeePromotionEntryDS]
           (EmployeePromotionEntryId,
            PrevEmpReportingBodyId,
           EmpInfoId
           )
           VALUES
           (@EmpTransferAndRedesignationId,
            @PrevEmpReportingBodyId,
            @EmpInfoId)";

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
           string queryStr = @"SELECT ISNULL(PrevEmpReportingBodyId,0) AS PrevEmpReportingBodyId,* FROM tblEmployeePromotionEntryDS
LEFT JOIN dbo.tblEmpGeneralInfo ON dbo.tblEmpGeneralInfo.EmpInfoId=tblEmployeePromotionEntryDS.EmpInfoId WHERE EmployeePromotionEntryId='" + id + "'";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }

       public DataTable LoadInformationALl(string param,string param2, string ComId, string paramTransfer)
       {
           string queryStr = @"     SELECT DISTINCT * FROM(SELECT EPE.EmployeePromotionEntryId,  Emp.EmpMasterCode,Emp.EmpName,  DEG.Designation,    NDEG.DepartmentName ,  case when EPE.NPromoTypeId is null then 'Re-appointment' else  PT.PromotionTypeName end PromotionTypeName  , EPE.Effectivedate, IsPromotion, EmployeeId  From tblEmployeePromotionEntry EPE   with (NOLOCk)
 left JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId

  left JOIN dbo.tblDesignation  DEG ON Emp.DesignationId = DEG.DesignationId
 
 
  left JOIN dbo.tblDepartment  NDEG ON Emp.DepartmentId = NDEG.DepartmentId
  
 
  
    left JOIN  dbo.tblCompanyInfo com ON com.CompanyId = EPE.CompanyId 
	  left JOIN dbo.tblPromotionType PT ON EPE.NPromoTypeId =PT.PromotionTypeId
    " + param + " " +
                             @" UNION ALL 

									 SELECT 0 EmployeePromotionEntryId, Emp.EmpMasterCode,Emp.EmpName, '' Designation,  '' DepartmentName , case when Reappointmant='yes' then 'Re-appointment' else   TypeOfPromotion end AS PromotionType,  FORMAT(EffectDate, 'dd-MMM-yyyy') Effectivedate , 0 IsPromotion, EmployeeId  FROM tblPromotionUpgrationHistory EPE   with (NOLOCk)
 left JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
  
									   left JOIN  dbo.tblCompanyInfo com ON com.CompanyId = Emp.CompanyId  where EPE.EmployeeID is not null  and ((TypeOfPromotion  in  ('Promotion','Upgradation')) or (Reappointmant='yes'))  " + param2 +




                                                                                                                                                                                                                                   @" Union All    SELECT EPE.EmployeePromotionEntryId,  Emp.EmpMasterCode,Emp.EmpName,  DEG.Designation,    NDEG.DepartmentName ,  case when EPE.NPromoTypeId is null then 'Re-appointment' else  PT.PromotionTypeName end PromotionTypeName  , EPE.Effectivedate, IsPromotion, EPE.EmployeeId  
 From tblEmployeePromotionEntry EPE   with (NOLOCk)
 left JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId

  left JOIN dbo.tblDesignation  DEG ON Emp.DesignationId = DEG.DesignationId
 
 
  left JOIN dbo.tblDepartment  NDEG ON Emp.DepartmentId = NDEG.DepartmentId
  inner JOIN   tblEmpAllRefference reff  ON EPE.EmployeeId = reff.RefferenceEmpId   
  
 
  
    left JOIN  dbo.tblCompanyInfo com ON com.CompanyId = EPE.CompanyId 
	  left JOIN dbo.tblPromotionType PT ON EPE.NPromoTypeId =PT.PromotionTypeId

	  inner join (select   NewEmployeeId,OnlyViewComId from tblEmpSpecialTransfer where OnlyView=1) tblPer on reff.EmployeeId =tblPer.NewEmployeeId
 where reff.EmployeeId is not null   " + paramTransfer + ")HH ORDER BY Effectivedate desc ";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }


       public DataTable LoadIncrementInfoApp()
       {
           string query = @" SELECT  Emp.EmpMasterCode,Emp.EmpName,  DEG.Designation,    NDEG.DepartmentName , PT.PromotionTypeName ,  * From tblEmployeePromotionEntry EPE
 left JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
  left JOIN dbo.tblDesignation  DEG ON Emp.DesignationId = DEG.DesignationId
  left JOIN dbo.tblDepartment  NDEG ON Emp.DepartmentId = NDEG.DepartmentId
    left JOIN  dbo.tblCompanyInfo com ON com.CompanyId = EPE.CompanyId 
	  left JOIN dbo.tblPromotionType PT ON EPE.NPromoTypeId =PT.PromotionTypeId
	   INNER JOIN (SELECT EmployeePromotionEntryId,MAX(Version)MaxVer FROM dbo.tblEmployeePromotionEntryAppLog WHERE ActionStatus NOT IN
								('Review') GROUP BY EmployeePromotionEntryId) AS CELog ON CELog.EmployeePromotionEntryId= EPE.EmployeePromotionEntryId
								INNER JOIN dbo.tblEmployeePromotionEntryAppLog ON tblEmployeePromotionEntryAppLog.EmployeePromotionEntryId =EPE.EmployeePromotionEntryId
                                where (EPE.IsDelete is null or EPE.IsDelete = 0) and Version=CELog.MaxVer  and  ForEmpInfoId =  '" + HttpContext.Current.Session["EmpInfoId"].ToString() + "'";

           return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
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
           string query = @"SELECT tblEmployeePromotionEntry.ActionStatus,  EGE.EmpInfoId as UserEmpInfoId,tblEmployeePromotionEntry.EmployeeId as EmpInfoId,tblEmployeePromotionEntry.EmployeePromotionEntryId, EG.EmpName AS EmployeeName,
 EGNewReportEmp.EmpName NewReportingEmployeeName,tblEmployeePromotionEntry.NRepEmpId,FinancialYearId, SLoc.SalaryLocation, 
 JLoc.Location, * FROM tblEmployeePromotionEntry
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
LEFT JOIN dbo.tblUser AS U ON U.UserId = tblEmployeePromotionEntry.EntryBy
															LEFT JOIN dbo.tblEmpGeneralInfo EGE ON EGE.EmpInfoId=U.EmpInfoId
 WHERE  tblEmployeePromotionEntry.EmployeePromotionEntryId='" + id + "'";


           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }



       public DataTable GetEmployeePromotionEntryIdByIdApp(string id)
       {
           string query = @"SELECT tblEmployeePromotionEntry.ActionStatus,  EGE.EmpInfoId as UserEmpInfoId,tblEmployeePromotionEntry.EmployeeId as EmpInfoId,tblEmployeePromotionEntry.EmployeePromotionEntryId, EG.EmpName AS EmployeeName,
 EGNewReportEmp.EmpName NewReportingEmployeeName,tblEmployeePromotionEntry.NRepEmpId,FinancialYearId, SLoc.SalaryLocation, 
 JLoc.Location, * FROM tblEmployeePromotionEntry
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
LEFT JOIN dbo.tblUser AS U ON U.UserId = tblEmployeePromotionEntry.EntryBy
															INNER JOIN dbo.tblEmpGeneralInfo EGE ON EGE.EmpInfoId=U.EmpInfoId
 WHERE  tblEmployeePromotionEntry.EmployeePromotionEntryId='" + id + "'";


           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
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
       public DataTable GetAppLogCommByJobId(int jobId)
       {
           string query = @" 
SELECT Alg.EmployeePromotionEntryIdAppLogId, emp.EmpName PreEmp, emp2.EmpName ForEmp, Version, Us.UserName ApproveBy, Alg.ActionStatus,
 Alg.ApproveDate, Alg.EmployeePromotionEntryId, Alg.Comments
  FROM tblEmployeePromotionEntryAppLog Alg
LEFT JOIN dbo.tblEmpGeneralInfo emp ON emp.EmpInfoId=Alg.PreEmpInfoId
LEFT JOIN dbo.tblEmpGeneralInfo emp2 ON emp2.EmpInfoId=Alg.ForEmpInfoId
LEFT JOIN dbo.tblUser Us ON Alg.ApproveBy=Us.UserId WHERE  Alg.ActionStatus!= 'Drafted' and Alg.EmployeePromotionEntryId='" + jobId + "'";
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
           aSqlParameterlist.Add(new SqlParameter("@IsReappointment", (object)aEmployeePromotionEntryDAO.IsReappointment));

           aSqlParameterlist.Add(new SqlParameter("@IsB_DirectlySupervisor", (object)aEmployeePromotionEntryDAO.IsB_DirectlySupervisor ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@IsB_ReportingBody", (object)aEmployeePromotionEntryDAO.IsB_ReportingBody ?? DBNull.Value));


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
      ,[Remarks] =  @Remarks,  DivisionId=@DivisionId, DivisionWId=@DivisionWId, DepartmentId=@DepartmentId,  SectionId=@SectionId, SubSectionId=@SubSectionId,  SalaryLoationId=@SalaryLoationId, JobLocationId=@JobLocationId , EmpTypeId=@EmpTypeId,EmployeeCode=@EmployeeCode, NSalaryStepId=@NSalaryStepId, IsReappointment=@IsReappointment, IsB_DirectlySupervisor=@IsB_DirectlySupervisor,  IsB_ReportingBody=@IsB_ReportingBody   WHERE EmployeePromotionEntryId=@EmployeePromotionEntryId";

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
           string query = @"SELECT 0 PrevEmpReportingBodyId, * FROM dbo.tblEmpGeneralInfo WHERE ReportingEmpId='" + id + "'";


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

       public bool UpdateEmployeeExitInfo(int ?empGenId, int ?salaryGradeId, int ?salaryStepId, int ?desigId, int ?reportingBodyId)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           aSqlParameterlist.Add(new SqlParameter("@EmpGradeId", salaryGradeId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalScaleId", salaryStepId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DesigId", desigId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@RptBodyId", reportingBodyId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", empGenId ?? (object)DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@UpdateBy", HttpContext.Current.Session["UserId"].ToString()));
           aSqlParameterlist.Add(new SqlParameter("@UpdateDate", DateTime.Now));


           string query = @"UPDATE tblEmpGeneralInfo SET SalaryGradeId = @EmpGradeId,SalaryStepId = @SalScaleId,DesignationId = @DesigId,ReportingEmpId = @RptBodyId,UpdateBy=@UpdateBy,UpdateDate=@UpdateDate    WHERE EmpInfoId=@EmpInfoId";
           return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, DataBase.HRDB);
       }

       public bool UpdateEmployeeSuperVisorId(EmpGeneralInfo aInfo)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", (object) aInfo.EmpInfoId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@RptBodyId", (object) aInfo.LineId ?? DBNull.Value)); 

           string query = @"UPDATE tblEmpGeneralInfo SET ReportingEmpId = @RptBodyId WHERE EmpInfoId =  @EmpInfoId";
           return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, DataBase.HRDB);
       }

       public DataTable EmpTransferAndRedesignationPS(string value)
       {
           string query = @"SELECT * FROM dbo.tblEmployeePromotionEntryPS WHERE EmployeePromotionEntryId ='" + value + "'";

           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }

       public bool UpdateEmployeeSuperVisorIdToNull(int empId)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

           EmpGeneralInfo aInfo = new EmpGeneralInfo();

           aInfo.EmpInfoId = empId;

           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aInfo.EmpInfoId));
           aSqlParameterlist.Add(new SqlParameter("@RptBodyId", aInfo.LineId ?? (object)DBNull.Value));

           string query = @"UPDATE tblEmpGeneralInfo SET ReportingEmpId = @RptBodyId WHERE EmpInfoId =  @EmpInfoId";
           return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, DataBase.HRDB);
       }

       public DataTable GetEmployeeReportingBodyInfo(int value)
       {
           string query = @"SELECT ReportingEmpId FROM dbo.tblEmpGeneralInfo WHERE EmpInfoId ='" + value + "'";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }

       public DataTable GetEmployeeReportingBodyId(string value)
       {
           string query = @"SELECT ReportingEmpId AS  FROM dbo.tblEmpGeneralInfo WHERE EmpInfoId ='" + value + "'";
           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }
    }
}
