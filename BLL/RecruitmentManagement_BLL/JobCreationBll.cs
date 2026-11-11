using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using DAL.RecruitmentManagement_DAL;
using DAO.HRIS_DAO;

namespace BLL.RecruitmentManagement_BLL
{
    public class JobCreationBll
    {
        JobCreationDal aJobCreationDal = new JobCreationDal();
        public void LoadComapnyNameList(DropDownList ddl)
        {
            aJobCreationDal.GetComapnyNameList(ddl);
        }

        public void LoadDepartmentList(DropDownList ddl)
        {
            aJobCreationDal.GetDepartmentList(ddl);
        }

        public void LoadSectionList(DropDownList ddl)
        {
            aJobCreationDal.GetSectionList(ddl);
        }

        public Int64 SaveDataBll(JobCreationDao aJobCreationDao)
        {
            return aJobCreationDal.SaveJobCreationInfo(aJobCreationDao);
        }

        public Int64 SaveEducationalReqDetail(List<JobCreationEdReqDao> eduReqList)
        {
            Int64 id = 0;

            foreach (var jobCreationEdReqDao in eduReqList)
            {
                id = aJobCreationDal.SaveEducationalReqDetail(jobCreationEdReqDao);
            }

            return id;
        }

        public Int64 SaveJobLocationDetail(List<JobCreationLocationDao> aCreationLocationDaos)
        {
            Int64 id = 0;

            foreach (var JobCreationLocationDao in aCreationLocationDaos)
            {
                id = aJobCreationDal.SaveJobLocationDetail(JobCreationLocationDao);
            }

            return id;
        }

        public DataTable LoadJobCreationInfos(string param)
        {
            return aJobCreationDal.GetJobCreationInfos(param);
        }

        public DataTable LoadJobCreationInfoByJobId(long jobId)
        {
            return aJobCreationDal.GetJobCreationInfoByJobId(jobId);
        }

        public DataTable JobCreationEduReq(long jobId)
        {
            return aJobCreationDal.GetJobCreationEduReqByJobId(jobId);
        }

        public DataTable JobCreationLocation(long jobId)
        {
            return aJobCreationDal.GetJobCreationLocationByJobId(jobId);
        }

        public bool UpdateDataBll(JobCreationDao aJobCreationDao)
        {
            return aJobCreationDal.UpdateJobCreationInformation(aJobCreationDao);
        }

        public bool DeleteJobCreationDetailInformation(long jobId)
        {
            bool status = true;

            status = aJobCreationDal.DeleteJobCreationEduReqDetailInformationByJobId(jobId);
            status = aJobCreationDal.DeleteJobCreationLocationDetailInformationByJobId(jobId);

            return status;
        }

        public Int32 SaveDegreeInfo(string degree)
        {
            return aJobCreationDal.SaveDegreeInformation(degree);
        }




        public DataTable JobLocationKey(int JobId)
        {
            return aJobCreationDal.GetJobLocationByJobId(JobId);
        }


     
        
    }
}
