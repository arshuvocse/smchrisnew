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

namespace DAL.Appraisal
{
   public class AppraisalSetupListDAL
    {
       ClsCommonInternalDAL _aCommonInternalDal = new ClsCommonInternalDAL();

       ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
       public void GetCompanyListShortNameIntoDropdown(DropDownList ddl)
       {

           string queryStr = "SELECT CompanyId,CompanyName, ShortName  FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE IsActive = 1 AND UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";
           aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
       }

       public void LoadFinancialYearForSearch(DropDownList ddl, string comapnyId)
       {
           string query = @"SELECT FINY.FinancialYearId,
                            FINY.FinancialYearDesc FROM dbo.tblFinancialYear AS FINY 
                            WHERE FINY.Status = 'Active' AND FINY.CompanyId ='" + comapnyId + "'";

           aCommonInternalDal.LoadDropDownValue(ddl, "FinancialYearDesc", "FinancialYearId", query, "HRDB");
       }

       public int SaveAppraisalSetupMaster(AppraisalDeadlineMaster aMaster, string user)
       {
           try
           {
               int pk = 0;

               if (aMaster.AppraisalDeadLineMasterId == 0)
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                   aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                   aParameters.Add(new SqlParameter("@IsCommon", aMaster.IsCommon));
                   aParameters.Add(new SqlParameter("@EntryBy", user));
                   aParameters.Add(new SqlParameter("@Subject", aMaster.Subject));
                   aParameters.Add(new SqlParameter("@DeclarationDate", aMaster.DeclarationDate));
                   aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));

                   string query =
                       @"insert into tblAppraisalDeadlineMaster (CompanyId, FinancialYearId, IsCommon ,EntryDate, EntryBy,Subject, DeclarationDate,FYDes_AppDec) values(@CompanyId, @FinancialYearId, @IsCommon ,@EntryDate, @EntryBy,@Subject, @DeclarationDate, (SELECT FinancialYearDesc 
                     FROM dbo.tblFinancialYear 
                     WHERE FinancialYearId =@FinancialYearId))";

                   pk = _aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                   return pk;
               }
               else
               {
                   List<SqlParameter> aParameters = new List<SqlParameter>();
                   aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                   aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                   aParameters.Add(new SqlParameter("@AppraisalDeadLineMasterId", aMaster.AppraisalDeadLineMasterId));
                   aParameters.Add(new SqlParameter("@IsCommon", aMaster.IsCommon));
                   aParameters.Add(new SqlParameter("@Subject", aMaster.Subject));
                   aParameters.Add(new SqlParameter("@DeclarationDate", aMaster.DeclarationDate));
                   aParameters.Add(new SqlParameter("@UpdateBy", user));
                   aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));

                   string query = @"update tblAppraisalDeadlineMaster set DeclarationDate=@DeclarationDate, CompanyId = @CompanyId,Subject=@Subject , FinancialYearId = @FinancialYearId , IsCommon = @IsCommon , UpdateBy = @UpdateBy , UpdateDate = @UpdateDate where AppraisalDeadLineMasterId = @AppraisalDeadLineMasterId ";

                   bool result = _aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);

                   if (result == true)
                   {
                       pk = aMaster.AppraisalDeadLineMasterId;
                   }

                   return pk;
               }

           }
           catch (Exception ex)
           {

               throw ex;
           }
       }


       public DataTable GetAppraisalSetupList(string param)
       {

           try
           {
               string query = @"select a.AppraisalDeadLineMasterId , c.CompanyName ,d.FinancialYearDesc , b.TotalEmployee  , CONVERT(nvarchar (11),a.EntryDate , 106)EntryDate ,a.EntryBy, CONVERT(nvarchar (11),a.DeclarationDate , 106)DeclarationDate,*   from tblAppraisalDeadlineMaster A 
                                    left join tblCompanyInfo c on a.CompanyId = c.CompanyId
                                    left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId
                                    
                                    left join (select count(EmpinfoId)TotalEmployee , AppraisalDeadLineMasterId from tblAppraisalDeadLineDetails group by AppraisalDeadLineMasterId) B on a.AppraisalDeadLineMasterId = b.AppraisalDeadLineMasterId  
                                    
                                    where (a.IsDelete is null or a.IsDelete = 0) " + param;
               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception exception)
           {

               throw exception;
           }
       }


       public DataTable GetAppraisalSetupDetailsByMaster(int id)
       {
           try
           {
               string query = @" SELECT  dtls.EmpinfoId,dtls.AppraisalDeadLineDetailsId, dtls.AppraisalDeadLineMasterId, empInfo.EmpMasterCode, empInfo.EmpName,   FORMAT(DeadLine,'dd-MMM-yyyy') DeadLine, dtls.Remarks, 0 AS DivisionName,
 desg.Designation, Dpt.DepartmentName, *  FROM   dbo.tblAppraisalDeadLineDetails dtls

 INNER JOIN dbo.tblEmpGeneralInfo empInfo ON  dtls.EmpinfoId=empInfo.EmpInfoId
left JOIN dbo.tblDesignation desg ON  empInfo.DesignationId=desg.DesignationId
left JOIN dbo.tblDepartment Dpt ON  empInfo.DepartmentId=Dpt.DepartmentId
  where dtls.AppraisalDeadLineMasterId = " +
                   id + " ";
               return _aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception exception)
           {

               throw exception;
           }
       }

    }
}
