using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.MasterSetup_DAL;
using DAL.RecruitmentManagement_DAL;

namespace BLL.RecruitmentManagement_BLL
{
    public class JobReqFormBll
    {
        EmployeeRequsitionDAL aEmployeeRequsitionDAL = new EmployeeRequsitionDAL();
        public DataTable JobCreationKeyRespon(int JobReqId)
        {
            return aEmployeeRequsitionDAL.GetJobCreationKeyResponByJobId(JobReqId);
        }

        public DataTable OfficeByJobReqId(int JobReqId)
        {
            return aEmployeeRequsitionDAL.GetOfficeByJobId(JobReqId);
        }

        public DataTable AppLogCommByJobReqId(int JobReqId)
        {
            return aEmployeeRequsitionDAL.GetAppLogCommByJobId(JobReqId);
        }

        public DataTable GetId()
        {
            return aEmployeeRequsitionDAL.GetId();
        }

        public DataTable JobEduReq(int JobReqId)
        {
            return aEmployeeRequsitionDAL.GetJobEduReqByJobId(JobReqId);
        }


        public DataTable EducationRequirementsDetail(int JobReqId)
        {
            return aEmployeeRequsitionDAL.GetEducationRequirementsDetailId(JobReqId);
        }

        public DataTable OtherRequirementsDetail(int JobReqId)
        {
            return aEmployeeRequsitionDAL.GetOtherRequirementsDetailId(JobReqId);
        }
    }
}
