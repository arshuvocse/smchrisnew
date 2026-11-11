using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.DivisionWiseEmpTransfer;
using DAO.HealthCare_Dao;

namespace DAL.DivisionWiseEmpTransfer
{
   public class DivisionWiseTransferDal
    {
       ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

       DataAccessManager accessManager = new DataAccessManager();
       public DataTable GetOption()
       {
           try
           {
               string query = @"Select ''as Descriptiondate, YesNo as YesNo1,* from tblReimbursmentCheckOption_HealthCare";
               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception ex)
           {

               throw ex;
           }
       }

       public DataTable Get_Section_By_Department(Int32 DepartmentId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@DepartmentId", DepartmentId));
           const string queryStr = @"Select * from tblSection where IsActive =1 and  DepartmentId=@DepartmentId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable Get_SubSection_By_Section(Int32 SectionId)
       {
           var aSqlParameterlist = new List<SqlParameter>();
           aSqlParameterlist.Add(new SqlParameter("@SectionId", SectionId));
           const string queryStr = @"Select * from tblSubSection where IsActive =1 and  SectionId=@SectionId";
           return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
       }

       public DataTable Get_EmployeeInformation(string param)
       {

           string queryStr = @"Select Emp.EmpInfoId, Emp.EmpMasterCode, Emp.EmpName, Com.CompanyId,Com.ShortName as  CompanyName, Divi.DivisionId,Divi.DivisionName,DW.DivisionWId, DW.DivisionWingName,
DT.DepartmentId,DT.DepartmentName, sec.SectionId, sec.SectionName, Ssec.SubSectionId, Ssec.SubSectionName  from tblEmpGeneralInfo Emp 
LEFT JOIN tblCompanyInfo Com ON Com.CompanyId = Emp.CompanyId
LEFT JOIN tblDivision Divi ON Divi.DivisionId = emp.DivisionId
LEFT JOIN tblDivisionWing DW ON DW.DivisionWId = Emp.DivisionWId
LEFT JOIN tblDepartment DT ON DT.DepartmentId = Emp.DepartmentId
LEFT JOIN tblSection sec ON sec.SectionId = Emp.SectionId
LEFT JOIN tblSubSection Ssec On Ssec.SubSectionId = Emp.SubSectionId 
where  Emp.EmpInfoId IS NOT NULL and Emp.IsActive=1 " + param + "  ";
           return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
       }

       public bool Update(DivisionWiseEmpTransferDao aDao)
       {

           accessManager.SqlConnectionOpen(DataBase.H);

           bool result = false;
           try
           {
               if (  aDao.IsEmployeeShiftHierarchyGenerate ==true)
               {
                   var aSqlParameterlist = new List<SqlParameter>();
                   aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aDao.EmpInfoId));
                   aSqlParameterlist.Add(new SqlParameter("@CompanyId", aDao.CompanyId));
                   aSqlParameterlist.Add(new SqlParameter("@DivisionId", aDao.DivisionId));
                   aSqlParameterlist.Add(new SqlParameter("@DivisionWId", (object)aDao.DivisionWId ?? DBNull.Value));
                   aSqlParameterlist.Add(new SqlParameter("@DepartmentId", (object)aDao.DepartmentId ?? DBNull.Value));
                   aSqlParameterlist.Add(new SqlParameter("@SectionId", (object)aDao.SectionId ?? DBNull.Value));
                   aSqlParameterlist.Add(new SqlParameter("@SubSectionId", (object)aDao.SubSectionId ?? DBNull.Value));
                   aSqlParameterlist.Add(new SqlParameter("@TransferDivisionId", (object)aDao.TransferDivisionId ?? DBNull.Value));

                   aSqlParameterlist.Add(new SqlParameter("@EntryBy", HttpContext.Current.Session["UserId"].ToString()));
                   result = accessManager.UpdateData("sp_Update_DivisionWiseEmpTransfer", aSqlParameterlist);
               }

               else
               {
                   var aSqlParameterlist = new List<SqlParameter>();
                   aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aDao.EmpInfoId));
                   aSqlParameterlist.Add(new SqlParameter("@CompanyId", aDao.CompanyId));
                   aSqlParameterlist.Add(new SqlParameter("@DivisionId", aDao.DivisionId));
                   aSqlParameterlist.Add(new SqlParameter("@DivisionWId", (object)aDao.DivisionWId ?? DBNull.Value));
                   aSqlParameterlist.Add(new SqlParameter("@DepartmentId", (object)aDao.DepartmentId ?? DBNull.Value));
                   aSqlParameterlist.Add(new SqlParameter("@SectionId", (object)aDao.SectionId ?? DBNull.Value));
                   aSqlParameterlist.Add(new SqlParameter("@SubSectionId", (object)aDao.SubSectionId ?? DBNull.Value));
                   aSqlParameterlist.Add(new SqlParameter("@TransferDivisionId", (object)aDao.TransferDivisionId ?? DBNull.Value));

                   aSqlParameterlist.Add(new SqlParameter("@EntryBy", HttpContext.Current.Session["UserId"].ToString()));
                   result = accessManager.UpdateData("sp_Update_DivisionWiseEmpTransferOnlyEmployeeTransfer", aSqlParameterlist);
           
               }
                           

                             
               
           }
           catch (Exception e)
           {
               result = false;
               accessManager.SqlConnectionClose(true);
               throw e;
           }
           finally
           {
               accessManager.SqlConnectionClose();
           }

           return result;
       }

    }
}
