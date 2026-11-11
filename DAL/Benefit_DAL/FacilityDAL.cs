using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.Benefit_DAL
{
    public class FacilityDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int SaveBenefitMaster(FacilityMasterDAO aBenefitMasterDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            //aSqlParameterlist.Add(new SqlParameter("@BenefitMasterId", aBenefitMasterDao.BenefitMasterId));
            aSqlParameterlist.Add(new SqlParameter("@Benefit", aBenefitMasterDao.Benefit));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aBenefitMasterDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aBenefitMasterDao.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aBenefitMasterDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aBenefitMasterDao.CompanyId));


            const string queryStr = @"INSERT INTO dbo.tblFacilityMaster
                                    (
                                        Benefit,
                                        EntryBy,
                                        EntryDate,
                                        IsActive,
                                        CompanyId
                                    )
                                    VALUES
                                    (   @Benefit,
                                        @EntryBy,
                                        @EntryDate,
                                        @IsActive,
                                        @CompanyId
                                    )";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }


        public int SaveBenefitNameEntry(FacilityMasterDAO aBenefitMasterDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            //aSqlParameterlist.Add(new SqlParameter("@BenefitMasterId", aBenefitMasterDao.BenefitMasterId));
            aSqlParameterlist.Add(new SqlParameter("@Benefit", aBenefitMasterDao.Benefit));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aBenefitMasterDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aBenefitMasterDao.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aBenefitMasterDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aBenefitMasterDao.CompanyId));

            const string queryStr = @"INSERT INTO dbo.tblFacilityMaster
                                    (
                                        Benefit,
                                        EntryBy,
                                        EntryDate,
                                        IsActive,
                                        CompanyId
                                    )
                                    VALUES
                                    (   @Benefit,
                                        @EntryBy,
                                        @EntryDate,
                                        @IsActive,
                                        @CompanyId
                                    )";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }

        public int SaveBenefitDetail(FacilityDetailDAO aBenefitDetailDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            //aSqlParameterlist.Add(new SqlParameter("@BenefitMasterId", aBenefitMasterDao.BenefitMasterId));
            aSqlParameterlist.Add(new SqlParameter("@FacilityMasterId", aBenefitDetailDao.FacilityMasterId));
            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", aBenefitDetailDao.EmpCategoryId));
            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", aBenefitDetailDao.SalaryGradeId));

            const string queryStr = @"INSERT INTO dbo.tblFacilityDetail
                                    (
                                        FacilityMasterId,
                                        EmpCategoryId,
                                        SalaryGradeId
                                    )
                                    VALUES
                                    (   @FacilityMasterId,
                                        @EmpCategoryId,
                                        @SalaryGradeId
                                    )";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public int SaveBenefitJobNature(FacilityJobNatureDAO aBenefitJobNatureDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            //aSqlParameterlist.Add(new SqlParameter("@BenefitMasterId", aBenefitMasterDao.BenefitMasterId));
            aSqlParameterlist.Add(new SqlParameter("@FacilityMasterId", aBenefitJobNatureDao.FacilityMasterId));
            aSqlParameterlist.Add(new SqlParameter("@PermConfirmed", aBenefitJobNatureDao.PermConfirmed));
            aSqlParameterlist.Add(new SqlParameter("@PermProbation", aBenefitJobNatureDao.PermProbation));
            aSqlParameterlist.Add(new SqlParameter("@ContConfirmed", aBenefitJobNatureDao.ContConfirmed));
            aSqlParameterlist.Add(new SqlParameter("@ContProbation", aBenefitJobNatureDao.ContProbation));
            aSqlParameterlist.Add(new SqlParameter("@ContYear", aBenefitJobNatureDao.ContYear));
            aSqlParameterlist.Add(new SqlParameter("@CasualConfirmed", aBenefitJobNatureDao.CasualConfirmed));
            aSqlParameterlist.Add(new SqlParameter("@CasualProbation", aBenefitJobNatureDao.CasualProbation));
            aSqlParameterlist.Add(new SqlParameter("@Perma", aBenefitJobNatureDao.Perma));
            aSqlParameterlist.Add(new SqlParameter("@Contra", aBenefitJobNatureDao.Contra));
            aSqlParameterlist.Add(new SqlParameter("@Casua", aBenefitJobNatureDao.Casua));


            const string queryStr = @"INSERT INTO dbo.tblFacilityJobNature
                                    (
                                        FacilityMasterId,
                                        PermConfirmed,
                                        PermProbation,
                                        ContConfirmed,
                                        ContProbation,
                                        ContYear,
                                        CasualConfirmed,
                                        CasualProbation,Perma,Contra,Casua
                                    )
                                    VALUES
                                    (   @FacilityMasterId,
                                        @PermConfirmed,
                                        @PermProbation,
                                        @ContConfirmed,
                                        @ContProbation,
                                        isnull(@ContYear,0),
                                        @CasualConfirmed,
                                        @CasualProbation,@Perma,@Contra,@Casua
                                    )";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable GetGradeData(string id)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@ID", id));

            const string queryStr = @"SELECT * FROM dbo.tblSalaryGrade WHERE  EmpCategoryId=@ID";
            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }
        public DataTable GetBenefit()
        {
            

            const string queryStr = @"SELECT * FROM dbo.tblBenefitMaster";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }




        public DataTable GetBenefitNameView()
        {


            const string queryStr = @"SELECT * FROM dbo.tblBenefitName";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }




        public DataTable GetBenefit(string id)
        {


            string queryStr = @"SELECT dbo.tblCompanyInfo.ShortName,* FROM dbo.tblFacilityMaster
 LEFT JOIN dbo.tblFacilityDetail ON tblFacilityDetail.FacilityMasterId = tblFacilityMaster.FacilityMasterId 
 LEFT JOIN dbo.tblFacilityJobNature ON tblFacilityJobNature.FacilityMasterId = tblFacilityMaster.FacilityMasterId 
INNER JOIN dbo.tblCompanyInfo ON tblCompanyInfo.CompanyId = tblFacilityMaster.CompanyId  WHERE tblFacilityMaster.FacilityMasterId='" + id + "'";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetBenefitList(string param)
        {


            string queryStr = @"SELECT dbo.tblCompanyInfo.ShortName, *,STUFF( ( SELECT CONCAT(',', tblSalaryGrade.GradeCode+' ' +s2.GradeCode, '') FROM dbo.tblFacilityDetail mms (NOLOCK)
LEFT JOIN dbo.tblSalaryGrade ON tblSalaryGrade.SalaryGradeId = mms.EmpCategoryId
LEFT JOIN dbo.tblSalaryGrade s2 ON s2.SalaryGradeId = mms.SalaryGradeId 
WHERE tblFacilityMaster.FacilityMasterId=mms.FacilityMasterId 
 ORDER BY mms.FacilityMasterId FOR XML PATH('') ), 1, 1, '' ) AS GradeList FROM dbo.tblFacilityMaster
--LEFT JOIN dbo.tblFacilityDetail ON tblFacilityDetail.FacilityMasterId = tblFacilityMaster.FacilityMasterId 
 LEFT JOIN dbo.tblFacilityJobNature ON tblFacilityJobNature.FacilityMasterId = tblFacilityMaster.FacilityMasterId 
INNER JOIN dbo.tblCompanyInfo ON tblCompanyInfo.CompanyId = tblFacilityMaster.CompanyId   " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public bool UpdateBenefitMaster(FacilityMasterDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@FacilityMasterId", aInformationDao.FacilityMasterId));
            aSqlParameterlist.Add(new SqlParameter("@Benefit", aInformationDao.Benefit));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));

            const string queryStr = @"UPDATE tblFacilityMaster SET Benefit = @Benefit, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate,IsActive = @IsActive WHERE FacilityMasterId = @FacilityMasterId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }
        public bool UpdateBenefitName(FacilityMasterDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@FacilityMasterId", aInformationDao.FacilityMasterId));
            aSqlParameterlist.Add(new SqlParameter("@Benefit", aInformationDao.Benefit));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));

            const string queryStr = @"UPDATE tblBenefitName SET Benefit = @Benefit, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate,IsActive = @IsActive WHERE FacilityMasterId = @FacilityMasterId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }
        public bool DeleteBenefitMaster(string id)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@FacilityMasterId", id));

            const string queryStr = @"DELETE FROM dbo.tblFacilityMaster WHERE FacilityMasterId=@FacilityMasterId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public DataTable GetBenefitName(string id)
        {


            string queryStr = @"SELECT * FROM dbo.tblBenefitName
 WHERE tblBenefitName.BenefitMasterId='" + id + "'";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }


        public bool DeleteBenefitName(string id)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@BenefitMasterId", id));

            const string queryStr = @"DELETE FROM dbo.tblFacilityMaster WHERE BenefitMasterId=@BenefitMasterId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }



        public bool DeleteBenefitDetail(string id)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@FacilityMasterId", id));

            const string queryStr = @"DELETE FROM dbo.tblFacilityDetail WHERE FacilityMasterId=@FacilityMasterId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }
        public bool DeleteBenefitJobNature(string id)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@FacilityMasterId", id));

            const string queryStr = @"DELETE FROM dbo.tblFacilityJobNature WHERE FacilityMasterId=@FacilityMasterId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }

        public void GetComapnyNameList(DropDownList ddl)
        {
            string queryStr = "SELECT CompanyId,CompanyName, ShortName FROM tblCompanyInfo WHERE CompanyId IN (SELECT CompanyId FROM dbo.tblUserCompanyMaping WHERE UserId='" + HttpContext.Current.Session["UserId"].ToString() + "')";

            //string queryStr = "SELECT CompanyId, CompanyName FROM tblCompanyInfo";
            aCommonInternalDal.LoadDropDownValue(ddl, "ShortName", "CompanyId", queryStr, "HRDB");
        }

    }
}
