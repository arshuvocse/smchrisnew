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

namespace DAL.Increment_DAL
{
    public class PrintJobCirculationDAL
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();


        public DataTable GetJobCreationInformationById(string id)
        {
            string query = @"SELECT  Det.DepartmentName,  (Det.DepartmentName + ', '+ tblJobReqForm.JobTitle + ' ('+ CONVERT(NVARCHAR,tblJobReqForm.Nos))+ ') ' AS JobTitle, tblJobReqForm.Nos, tblJobReqForm.JobReqId JobReqId, tblJobCreation.Position, dbo.tblJobCreation.JobCode, Com.ShortName  FROM dbo.tblJobCreation 
LEFT JOIN  tblJobReqForm ON tblJobCreation.ReqCodeId = tblJobReqForm.JobReqId
  left JOIN dbo.tblDepartment Det ON dbo.tblJobReqForm.DeptId=Det.DepartmentId
    left JOIN dbo.tblCompanyInfo Com ON tblJobCreation.CompanyId=Com.CompanyId
  Where tblJobCreation.JobID='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }


        public DataTable LoadKeyResponseByJobReqFormId(string id)
        {
            string query = @" SELECT * FROM tblJobReqKeyRespon WHERE JobReqFormId='" + id + "'";


            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }



        public DataTable GetOtherRequirementsDetailId(string jobId)
        {
            string query = @" SELECT  ks.EducationRequirements  OtherRequirementsName FROM dbo.tblEducationRequirDesJobReq KRS
 INNER JOIN dbo.tblEducationRequirements ks ON ks.ERID=KRS.EduRequirId WHERE KRS.JobReqFormId='" + jobId + "'" +

" UNION ALL " +
" SELECT   Education AS OtherRequirement   FROM tblJobReqEducation   WHERE tblJobReqEducation.JobReqId='" + jobId + "'" +
"  UNION ALL " +
     " SELECT (JRFM.Experience + JRFM.Skills + JRFM.Age + JRFM.OtherExperience) AS OtherRequirementsName FROM tblJobReqForm JRFM WHERE JRFM.JobReqId='" + jobId + "'" +

   " UNION ALL " +
 " SELECT        OtherRequirement OtherRequirementsName FROM OtherRequirementDetail WHERE OtherRequirementDetail.MasterId='" + jobId + "'";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }
    }
}
