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
    public class EmployeeReDesignationDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        EmployeeReDesignationDAO aEmployeeReDesignationDAO = new EmployeeReDesignationDAO();
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
            string query = @"SELECT EmployeeId, EffectiveDate FROM dbo.tblEmployeeReDesignation WHERE IsDelete = 0 AND EmployeeId = @EmployeeId and EffectiveDate=@EffectiveDate";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, DataBase.HRDB);
        }

        public DataTable DeleteValidattionForEffectiveDate(string id)
        {
            string query = @"SELECT  EmployeeReDesignationId, EffectiveDate FROM dbo.tblEmployeeReDesignation WHERE EmployeeReDesignationId=" + id;
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



       public Int32 EmployeePromotionEntrySaveInfo(EmployeeReDesignationDAO aEmployeePromotionEntryDAO)
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
         
           aSqlParameterlist.Add(new SqlParameter("@PDesignationId", (object)aEmployeePromotionEntryDAO.PDesignationId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@NDesignationId", (object)aEmployeePromotionEntryDAO.NDesignationId ?? DBNull.Value));          
          
           aSqlParameterlist.Add(new SqlParameter("@EntryBy", aEmployeePromotionEntryDAO.EntryBy));
           aSqlParameterlist.Add(new SqlParameter("@EntryDate", aEmployeePromotionEntryDAO.EntryDate));
        
           aSqlParameterlist.Add(new SqlParameter("@Remarks", aEmployeePromotionEntryDAO.Remarks));
         
           aSqlParameterlist.Add(new SqlParameter("@Effectivedate", (object)aEmployeePromotionEntryDAO.Effectivedate ?? DBNull.Value));

           aSqlParameterlist.Add(new SqlParameter("@AutoProcess", (object)aEmployeePromotionEntryDAO.AutoProcess ?? DBNull.Value));



           string insertQuery = @"INSERT INTO [dbo].[tblEmployeeReDesignation]
           (CompanyId
           ,EmployeeId
           ,FinancialYearId
        
           ,PDesignationId
         
           ,NDesignationId
          
           ,EntryBy
           ,EntryDate
         
           ,Remarks,  EmployeeCode,  DivisionId, DivisionWId, DepartmentId,  SectionId, SubSectionId ,
           SalaryLoationId, JobLocationId , EmpTypeId, Effectivedate,  AutoProcess  )
     VALUES
           (@CompanyId
           ,@EmployeeId
           ,@FinancialYearId
        
           ,@PDesignationId
         
           ,@NDesignationId
          
           ,@EntryBy
           ,@EntryDate
         
           ,@Remarks,  @EmployeeCode,  @DivisionId, @DivisionWId, @DepartmentId,  @SectionId, @SubSectionId ,
           @SalaryLoationId, @JobLocationId , @EmpTypeId, @Effectivedate,  @AutoProcess)";

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

       public DataTable LoadInformationALl(string param, string ComId)
       {
           string queryStr = @" SELECT EPE.EmployeeReDesignationId  , EPE.EmployeeId, EPE.Effectivedate, Emp.EmpMasterCode,Emp.EmpName,  NDEs.Designation NDesignation,  PDEG.Designation PDesignation,   NDEG.DepartmentName   From tblEmployeeReDesignation EPE
 inner JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId

  left JOIN dbo.tblDesignation  PDEG ON EPE.PDesignationId = PDEG.DesignationId
  left JOIN dbo.tblDesignation  NDEs ON EPE.NDesignationId = NDEs.DesignationId
 
 
  left JOIN dbo.tblDepartment  NDEG ON EPE.DepartmentId = NDEG.DepartmentId
  
 
  
    left JOIN  dbo.tblCompanyInfo com ON com.CompanyId = EPE.CompanyId 
	  
    " + param + @"   union all 
 SELECT   EPE.EmployeeReDesignationId  , EPE.EmployeeId, EPE.Effectivedate, Emp.EmpMasterCode,Emp.EmpName,  NDEs.Designation NDesignation,  PDEG.Designation PDesignation,   NDEG.DepartmentName  From tblEmployeeReDesignation EPE
 inner JOIN dbo.tblEmpGeneralInfo  Emp ON EPE.EmployeeId = Emp.EmpInfoId
  inner JOIN   tblEmpAllRefference reff  ON EPE.EmployeeId = reff.RefferenceEmpId   

  left JOIN dbo.tblDesignation  PDEG ON EPE.PDesignationId = PDEG.DesignationId
  left JOIN dbo.tblDesignation  NDEs ON EPE.NDesignationId = NDEs.DesignationId
 
 
  left JOIN dbo.tblDepartment  NDEG ON EPE.DepartmentId = NDEG.DepartmentId
  
 
  
    left JOIN  dbo.tblCompanyInfo com ON com.CompanyId = EPE.CompanyId

	 inner join (select   NewEmployeeId,OnlyViewComId from tblEmpSpecialTransfer where OnlyView=1) tblPer on reff.EmployeeId =tblPer.NewEmployeeId

	  where (EPE.IsDelete is null or EPE.IsDelete=0)   ORDER BY EPE.EmployeeReDesignationId DESC  ";
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
           string query = @"SELECT  EG.EmpName AS EmployeeName,  FinancialYearId, SLoc.SalaryLocation, JLoc.Location, * FROM tblEmployeeReDesignation ERD
left JOIN dbo.tblEmpGeneralInfo EG ON ERD.EmployeeId= EG.EmpInfoId 
 
LEFT JOIN dbo.tblDesignation  deg ON EG.DesignationId=deg.DesignationId
							LEFT JOIN dbo.tblSalaryGrade  SG ON EG.SalaryGradeId=SG.SalaryGradeId
							LEFT JOIN dbo.tblDivision  Div ON EG.DivisionId=Div.DivisionId
							LEFT JOIN dbo.tblDivisionWing  Wing ON EG.DivisionWId=Wing.DivisionWId
							LEFT JOIN dbo.tblSection  Sec ON EG.SectionId=Sec.SectionId
							LEFT JOIN dbo.tblSubSection  SubSec ON EG.SubSectionId=SubSec.SubSectionId						
								LEFT JOIN dbo.tblDepartment  Dpt ON EG.DepartmentId=Dpt.DepartmentId
																LEFT JOIN dbo.tblSalaryLocation  SLoc ON EG.SalaryLoationId=SLoc.SalaryLoationId
														LEFT JOIN dbo.tblJobLocation  JLoc ON EG.JobLocationId=JLoc.JobLocationID
 WHERE  EmployeeReDesignationId='" + id + "'";


           return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
       }







       public bool EmployeePromotionEntryUpsateInfo(EmployeeReDesignationDAO aEmployeePromotionEntryDAO)
       {
           List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@EmployeeReDesignationId", aEmployeePromotionEntryDAO.EmployeeReDesignationId));
           aSqlParameterlist.Add(new SqlParameter("@EmployeeId", aEmployeePromotionEntryDAO.EmployeeId));
           aSqlParameterlist.Add(new SqlParameter("@CompanyId", aEmployeePromotionEntryDAO.CompanyId));
           aSqlParameterlist.Add(new SqlParameter("@FinancialYearId", aEmployeePromotionEntryDAO.FinancialYearId));
          
           aSqlParameterlist.Add(new SqlParameter("@PDesignationId", (object)aEmployeePromotionEntryDAO.PDesignationId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@NDesignationId", (object)aEmployeePromotionEntryDAO.NDesignationId ?? DBNull.Value));
         
           aSqlParameterlist.Add(new SqlParameter("@Effectivedate", (object)aEmployeePromotionEntryDAO.Effectivedate ?? DBNull.Value));


          
           aSqlParameterlist.Add(new SqlParameter("@DivisionId", (object)aEmployeePromotionEntryDAO.DivisionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DivisionWId", (object)aEmployeePromotionEntryDAO.DivisionWId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@DepartmentId", (object)aEmployeePromotionEntryDAO.DepartmentId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SectionId", (object)aEmployeePromotionEntryDAO.SectionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SubSectionId", (object)aEmployeePromotionEntryDAO.SubSectionId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@SalaryLoationId", (object)aEmployeePromotionEntryDAO.SalaryLoationId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@JobLocationId", (object)aEmployeePromotionEntryDAO.JobLocationId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmpTypeId", (object)aEmployeePromotionEntryDAO.EmpTypeId ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@EmployeeCode", (object)aEmployeePromotionEntryDAO.EmployeeCode ?? DBNull.Value));


       

           aSqlParameterlist.Add(new SqlParameter("@Remarks", (object)aEmployeePromotionEntryDAO.Remarks ?? DBNull.Value));


           aSqlParameterlist.Add(new SqlParameter("@UpdateBy", (object)aEmployeePromotionEntryDAO.UpdateBy ?? DBNull.Value));
           aSqlParameterlist.Add(new SqlParameter("@UpdateDate", (object)aEmployeePromotionEntryDAO.UpdateDate ?? DBNull.Value));


           string UpdateQuery = @"UPDATE [dbo].[tblEmployeeReDesignation]
   SET [CompanyId] =  @CompanyId
      ,[EmployeeId] =  @EmployeeId
      ,[FinancialYearId] =  @FinancialYearId
      
      ,[PDesignationId] =  @PDesignationId
     
      ,[NDesignationId] =  @NDesignationId
    
    
   
   
      ,[UpdateBy] =  @UpdateBy
      ,[UpdateDate] =  @UpdateDate
    
     
      ,[Remarks] =  @Remarks,  DivisionId=@DivisionId, DivisionWId=@DivisionWId, DepartmentId=@DepartmentId,  SectionId=@SectionId, 
	  SubSectionId=@SubSectionId,  SalaryLoationId=@SalaryLoationId, JobLocationId=@JobLocationId , EmpTypeId=@EmpTypeId,EmployeeCode=@EmployeeCode        WHERE EmployeeReDesignationId=@EmployeeReDesignationId";

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

           string query = @"UPDATE dbo.tblEmployeeReDesignation SET IsDelete='1',DeleteBy='" + HttpContext.Current.Session["UserId"].ToString() + "',DeleteDate='" + DateTime.Now + "' WHERE EmployeeReDesignationId=@ID";
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

           
           aSqlParameterlist.Add(new SqlParameter("@DesigId", aInfo.DesigId ?? (object)DBNull.Value));
          
           aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aInfo.EmpInfoId ?? (object)DBNull.Value));

           string query = @"UPDATE tblEmpGeneralInfo SET  DesignationId = @DesigId   WHERE EmpInfoId =  @EmpInfoId";
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
