using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataManager;
using DAL.InternalCls;

namespace DAL.AllPrintLetter_DAL
{
   public class ITMayConcernDAL
    {
       ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

       public DataTable GetITMayConcernDAL(string id)
       {
           try
           {
               string query = @"SELECT tblEmpGeneralInfo.EmpName,tblDesignation.Designation, dbo.tblEmpGeneralInfo.OfficialEmail, dbo.tblEmpGeneralInfo.OfficialMobile, * FROM dbo.tblITMayConcern 
LEFT JOIN dbo.tblEmpGeneralInfo ON tblEmpGeneralInfo.EmpInfoId = tblITMayConcern.ToEmployeeId
LEFT JOIN dbo.tblDesignation ON tblDesignation.DesignationId = tblEmpGeneralInfo.DesignationId
WHERE ITMayConcernId=" + id + "";
               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception exception)
           {

               throw exception;
           }
       }



       public DataTable GetSeparationITMayConcernDAL(string id)
       {
           try
           {
               string query = @"SELECT tblEmpGeneralInfo.EmpName,tblDesignation.Designation, dbo.tblEmpGeneralInfo.OfficialEmail, dbo.tblEmpGeneralInfo.OfficialMobile, * FROM dbo.tblSeparationITMayConcern 
LEFT JOIN dbo.tblEmpGeneralInfo ON tblEmpGeneralInfo.EmpInfoId = tblSeparationITMayConcern.ToEmployeeId
LEFT JOIN dbo.tblDesignation ON tblDesignation.DesignationId = tblEmpGeneralInfo.DesignationId
WHERE SeparationITMayConcern=" + id + "";
               return aCommonInternalDal.DataContainerDataTable(query, DataBase.HRDB);
           }
           catch (Exception exception)
           {

               throw exception;
           }
       }
    }
}
