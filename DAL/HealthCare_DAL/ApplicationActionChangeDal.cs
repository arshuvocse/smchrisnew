using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DAL.DataManager;
using DAL.InternalCls;

namespace DAL.HealthCare_DAL
{
   public class ApplicationActionChangeDal
    {

       ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
       DataAccessManager accessManager = new DataAccessManager();

       public void GetDDLEmployee(DropDownList ddl, string companyId)
       {
           string queryStr = @"SELECT M.EmpInfoId , E.EmpName+':'+ PatientName+':'+ Relationship+':'+ CONVERT(varchar, ClaimTKByUser)  AS EmpName FROM tbl_ReimbursmentFormMaster_HealthCare M 
LEFT JOIN tblEmpGeneralInfo E On M.EmpInfoId=E.EmpInfoId
WHERE M.ActionStatus !='Approved' And M.CompanyId=" + companyId;

           aCommonInternalDal.LoadDropDownValue(ddl, "EmpName", "EmpInfoId", queryStr, DataBase.HRDB);
       }

       public void Get_Application(DropDownList ddl, string EmpId)
       {
           string queryStr = @"SELECT M.EmpInfoId , E.EmpName+':'+ PatientName+':'+ Relationship+':'+ CONVERT(varchar, ClaimTKByUser)  EmpName FROM tbl_ReimbursmentFormMaster_HealthCare M 
                               LEFT JOIN tblEmpGeneralInfo E On M.EmpInfoId=E.EmpInfoId
                               WHERE M.ActionStatus !='Approved' And M.EmpInfoId=" + EmpId;

           aCommonInternalDal.LoadDropDownValue(ddl, "EmpName", "EmpInfoId", queryStr, DataBase.HRDB);
       }

    }
}
