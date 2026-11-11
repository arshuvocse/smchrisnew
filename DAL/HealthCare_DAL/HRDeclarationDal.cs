using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HealthCare_Dao;
using DAO.HRIS_DAO;

namespace DAL.HealthCare_DAL
{
   public class HRDeclarationDal
    {


        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        DataAccessManager accessManager = new DataAccessManager();

        public Int32 SaveDesignationInfo(HRDecleration aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            //aSqlParameterlist.Add(new SqlParameter("@HRDeclerationId", aInformationDao.HRDeclerationId));
            aSqlParameterlist.Add(new SqlParameter("@FinancialId", aInformationDao.FinancialId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@IPD", aInformationDao.IPD));
            aSqlParameterlist.Add(new SqlParameter("@OPD", aInformationDao.OPD));
 
                aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
                aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

                const string insertQuery = @"Declare @Count Int select @Count=count(*) from tblHRDecleration_Healthcare where FinancialId=LTRIM(RTRIM(@FinancialId)) and CompanyId=@CompanyId
                                              print @Count
                                              if(@Count=0)  INSERT INTO tblHRDecleration_Healthcare (CompanyId,FinancialId,IPD,OPD,EntryBy,EntryDate)
                                              VALUES (@CompanyId,@FinancialId,@IPD,@OPD,@EntryBy,@EntryDate)";
                return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
            
        }



        public DataTable UpdateIsCheck(Int32 FinancialId, Int32 HRDeclerationId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@FinancialId", FinancialId));
            aSqlParameterlist.Add(new SqlParameter("@HRDeclerationId", HRDeclerationId));
            const string queryStr = @"SELECT * FROM dbo.tblHRDecleration_Healthcare WHERE FinancialId=@FinancialId AND  HRDeclerationId NOT IN (@HRDeclerationId)";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }



        public bool UpdateHeDeclarationInfo(HRDecleration aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            //aSqlParameterlist.Add(new SqlParameter("@HRDeclerationId", aInformationDao.HRDeclerationId));
            aSqlParameterlist.Add(new SqlParameter("@FinancialId", aInformationDao.FinancialId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@IPD", aInformationDao.IPD));
            aSqlParameterlist.Add(new SqlParameter("@OPD", aInformationDao.OPD));
 
                aSqlParameterlist.Add(new SqlParameter("@HRDeclerationId", aInformationDao.HRDeclerationId));
                aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
                aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));

                const string insertQuery = @"Declare @Count Int
                                            select @Count=count(*)
                                           from tblHRDecleration_Healthcare where FinancialId=LTRIM(RTRIM(@FinancialId)) and  CompanyId=@CompanyId and HRDeclerationId not in (@HRDeclerationId)
                                           print @Count
                                           if(@Count=0) UPDATE tblHRDecleration_Healthcare SET CompanyId=@CompanyId, FinancialId = @FinancialId,IPD=@IPD, OPD=@OPD ,UpdateBy = @UpdateBy, UpdateDate = @UpdateDate 
                                           WHERE HRDeclerationId = @HRDeclerationId ";
                return aCommonInternalDal.UpdateDataByUpdateCommand(insertQuery, aSqlParameterlist, "HRDB");
               
        }


        public void GetFinancialList(DropDownList ddl)
        {
            const string queryStr = @"Select * from tblFinancialYear";
            aCommonInternalDal.LoadDropDownValue(ddl, "FinancialYearDesc", "FinancialYearId", queryStr, "HRDB");
        }


        public DataTable IsNotExeistFinacialYear(Int32 FinancialId, Int32 CompanyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@FinancialId", FinancialId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", CompanyId));
            const string queryStr = @"select * from tblHRDecleration_Healthcare where FinancialId=LTRIM(RTRIM(@FinancialId)) and CompanyId=@CompanyId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetHrDeclerationList()
        {
            string queryStr = @"Select  case when usEmp.EmpInfoId is null then  us.UserName else   usEmp.EmpName+ISNULL(' : '+dgs.Designation,'') end  CreateBy, Fy.FinancialYearDesc,* from  tblHRDecleration_Healthcare HR  with (nolock)
                                LEFT JOIN tblCompanyInfo Com ON Com.CompanyId = HR.CompanyId
                                LEFT JOIN tblFinancialYear Fy ON Fy.FinancialYearId = HR.FinancialId
								left JOIN  dbo.tblUser us   ON  HR.EntryBy =us.UserId  
 
 left JOIN  dbo.tblEmpGeneralInfo usEmp   ON  us.EmpInfoId =usEmp.EmpInfoId
 LEFT JOIN dbo.tblDesignation dgs ON usEmp.DesignationId = dgs.DesignationId";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }


        public DataTable Get_HrDeclarationById(int ID)
        {
            DataTable dt = new DataTable();
            try
            {
                accessManager.SqlConnectionOpen(DataBase.H);
                List<SqlParameter> aList = new List<SqlParameter>();

                aList.Add(new SqlParameter("@Id", ID));
                dt = accessManager.GetDataTable("sp_GET_HRDeclaration", aList);
            }
            catch (Exception e)
            {
                accessManager.SqlConnectionClose(true);
                throw e;
            }
            finally
            {
                accessManager.SqlConnectionClose();
            }

            return dt;
        }


    }
}
