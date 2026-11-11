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
  public  class KPISETUPListDAL
    {
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

      public DataTable GetKpiSetupList(string param)
      //string param
      {

          try
          {
              string query = @"select a.KPIDeadLineMasterId , c.CompanyName ,d.FinancialYearDesc , b.TotalEmployee  , CONVERT(nvarchar (11),a.EntryDate , 106)EntryDate ,a.EntryBy, CONVERT(nvarchar (11),a.DeclarationDate , 106)DeclarationDate,*   from tblKpiDeadlineMaster A 
                                    left join tblCompanyInfo c on a.CompanyId = c.CompanyId
                                    left join tblFinancialYear d on a.FinancialYearId = d.FinancialYearId
                                    
                                    left join (select count(EmpinfoId)TotalEmployee , KPIDeadLineMasterId from tblKPIDeadLineDetails group by KPIDeadLineMasterId) B on a.KPIDeadLineMasterId = b.KPIDeadLineMasterId  
                                    
                                    where (a.IsDelete is null or a.IsDelete = 0) " + param;
              //+ param
              return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
          }
          catch (Exception exception)
          {

              throw exception;
          }
      }

      public int SaveKpiSetupMaster(KpiDeadlineMaster aMaster, string user)
      {
          try
          {
              int pk = 0;

              if (aMaster.KPIDeadLineMasterId == 0)
              {
                  List<SqlParameter> aParameters = new List<SqlParameter>();
                  aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                  aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                  aParameters.Add(new SqlParameter("@Subject", aMaster.Subject));
                  aParameters.Add(new SqlParameter("@IsCommon", aMaster.IsCommon));
                  aParameters.Add(new SqlParameter("@DeclarationDate", aMaster.DeclarationDate));
                  aParameters.Add(new SqlParameter("@EntryBy", user));
                  aParameters.Add(new SqlParameter("@EntryDate", System.DateTime.Now));

                  string query =
                      @"insert into tblKpiDeadlineMaster (CompanyId, FinancialYearId, IsCommon ,EntryDate, EntryBy,Subject, DeclarationDate) values(@CompanyId, @FinancialYearId, @IsCommon ,@EntryDate, @EntryBy,@Subject, @DeclarationDate)";

                  pk = aCommonInternalDal.SaveDataByInsertCommandById(query, aParameters, DataBase.HRDB);
                  return pk;
              }
              else
              {
                  List<SqlParameter> aParameters = new List<SqlParameter>();
                  aParameters.Add(new SqlParameter("@CompanyId", aMaster.CompanyId));
                  aParameters.Add(new SqlParameter("@FinancialYearId", aMaster.FinancialYearId));
                  aParameters.Add(new SqlParameter("@KPIDeadLineMasterId", aMaster.KPIDeadLineMasterId));
                  aParameters.Add(new SqlParameter("@IsCommon", aMaster.IsCommon));
                  aParameters.Add(new SqlParameter("@Subject", aMaster.Subject));
                  aParameters.Add(new SqlParameter("@UpdateBy", user));
                  aParameters.Add(new SqlParameter("@UpdateDate", System.DateTime.Now));
                  aParameters.Add(new SqlParameter("@DeclarationDate", aMaster.DeclarationDate));

                  string query = @"update tblKpiDeadlineMaster set CompanyId = @CompanyId ,Subject=@Subject, FinancialYearId = @FinancialYearId , IsCommon = @IsCommon , UpdateBy = @UpdateBy , UpdateDate = @UpdateDate, DeclarationDate=@DeclarationDate where KPIDeadLineMasterId = @KPIDeadLineMasterId ";

                  bool result = aCommonInternalDal.UpdateDataByUpdateCommand(query, aParameters, DataBase.HRDB);

                  if (result == true)
                  {
                      pk = aMaster.KPIDeadLineMasterId;
                  }

                  return pk;
              }

          }
          catch (Exception ex)
          {

              throw ex;
          }
      }

    }
}
