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
    public class InterviewCandidateInfoBll
    {
        InterviewCandidateInfoDal aCandidateInfoDal = new InterviewCandidateInfoDal();
        public void LoadComapnyNameList(DropDownList ddl)
        {
            aCandidateInfoDal.GetComapnyNameList(ddl);
        }

        public void LoadJobList(DropDownList ddl)
        {
            aCandidateInfoDal.GetJobList(ddl);
        }

        public void LoadDegreeList(DropDownList ddl)
        {
            aCandidateInfoDal.GetDegreeList(ddl);
        }

        public void LoadAreaOfStudy(DropDownList ddl)
        {
            aCandidateInfoDal.GetAreaOfStudy(ddl);
        }


        public Int32 SaveDataBll(InterviewCandidateInfoDao aCandidateInfoDao)
        {
            return aCandidateInfoDal.SaveInterViewCandidateInfo(aCandidateInfoDao);
        }

        public DataTable LoadInterViewCandidateList()
        {
            return aCandidateInfoDal.GetInterViewCandidateList();
        }

        public DataTable LoadCandidateInfoById(int candidateId)
        {
            return aCandidateInfoDal.GetCandidateInfoById(candidateId);
        }

        public bool UpdateDataBll(InterviewCandidateInfoDao aCandidateInfoDao)
        {
            return aCandidateInfoDal.UpdateInterViewCandidateInfo(aCandidateInfoDao);
        }
    }
}
