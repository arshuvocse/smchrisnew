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
    public class BenefitDAL
    {
        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int SaveBenefitMaster(BenefitMasterDAO aBenefitMasterDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            //aSqlParameterlist.Add(new SqlParameter("@BenefitMasterId", aBenefitMasterDao.BenefitMasterId));
            aSqlParameterlist.Add(new SqlParameter("@Benefit", aBenefitMasterDao.Benefit));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aBenefitMasterDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aBenefitMasterDao.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aBenefitMasterDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aBenefitMasterDao.CompanyId));


            const string queryStr = @"INSERT INTO dbo.tblBenefitMaster
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


        public int SaveBenefitNameEntry(BenefitMasterDAO aBenefitMasterDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            //aSqlParameterlist.Add(new SqlParameter("@BenefitMasterId", aBenefitMasterDao.BenefitMasterId));
            aSqlParameterlist.Add(new SqlParameter("@Benefit", aBenefitMasterDao.Benefit));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aBenefitMasterDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aBenefitMasterDao.EntryDate));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aBenefitMasterDao.IsActive));
            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aBenefitMasterDao.CompanyId));

            const string queryStr = @"INSERT INTO dbo.tblBenefitName
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

        public int SaveBenefitDetail(BenefitDetailDAO aBenefitDetailDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            //aSqlParameterlist.Add(new SqlParameter("@BenefitMasterId", aBenefitMasterDao.BenefitMasterId));
            aSqlParameterlist.Add(new SqlParameter("@BenefitMasterId", aBenefitDetailDao.BenefitMasterId));
            aSqlParameterlist.Add(new SqlParameter("@EmpCategoryId", aBenefitDetailDao.EmpCategoryId));
            aSqlParameterlist.Add(new SqlParameter("@SalaryGradeId", aBenefitDetailDao.SalaryGradeId));

            const string queryStr = @"INSERT INTO dbo.tblBenefitDetail
                                    (
                                        BenefitMasterId,
                                        EmpCategoryId,
                                        SalaryGradeId
                                    )
                                    VALUES
                                    (   @BenefitMasterId,
                                        @EmpCategoryId,
                                        @SalaryGradeId
                                    )";

            return aCommonInternalDal.SaveDataByInsertCommandById(queryStr, aSqlParameterlist, "HRDB");
        }
        public int SaveBenefitJobNature(BenefitJobNatureDAO aBenefitJobNatureDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            //aSqlParameterlist.Add(new SqlParameter("@BenefitMasterId", aBenefitMasterDao.BenefitMasterId));
            aSqlParameterlist.Add(new SqlParameter("@BenefitMasterId", aBenefitJobNatureDao.BenefitMasterId));
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
            

            const string queryStr = @"INSERT INTO dbo.tblBenefitJobNature
                                    (
                                        BenefitMasterId,
                                        PermConfirmed,
                                        PermProbation,
                                        ContConfirmed,
                                        ContProbation,
                                        ContYear,
                                        CasualConfirmed,
                                        CasualProbation,Perma,Contra,Casua
                                    )
                                    VALUES
                                    (   @BenefitMasterId,
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


             string queryStr = @"SELECT dbo.tblCompanyInfo.ShortName,* FROM dbo.tblBenefitMaster
LEFT JOIN dbo.tblBenefitDetail ON tblBenefitDetail.BenefitMasterId = tblBenefitMaster.BenefitMasterId
LEFT JOIN dbo.tblBenefitJobNature ON tblBenefitJobNature.BenefitMasterId = tblBenefitMaster.BenefitMasterId 
INNER JOIN dbo.tblCompanyInfo ON tblCompanyInfo.CompanyId = tblBenefitMaster.CompanyId  WHERE tblBenefitMaster.BenefitMasterId='"+id+"'";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetBenefitList(string param)
        {


            string queryStr = @" SELECT dbo.tblCompanyInfo.ShortName,*, STUFF( ( SELECT CONCAT(',', tblSalaryGrade.GradeCode+' ' +s2.GradeCode, '') FROM tblBenefitDetail mms (NOLOCK)
LEFT JOIN dbo.tblSalaryGrade ON tblSalaryGrade.SalaryGradeId = mms.EmpCategoryId
LEFT JOIN dbo.tblSalaryGrade s2 ON s2.SalaryGradeId = mms.SalaryGradeId 
 WHERE tblBenefitMaster.BenefitMasterId=mms.BenefitMasterId 
 ORDER BY mms.BenefitMasterId FOR XML PATH('') ), 1, 1, '' ) AS GradeList  FROM dbo.tblBenefitMaster
 LEFT JOIN dbo.tblBenefitJobNature ON tblBenefitJobNature.BenefitMasterId = tblBenefitMaster.BenefitMasterId 
INNER JOIN dbo.tblCompanyInfo ON tblCompanyInfo.CompanyId = tblBenefitMaster.CompanyId " + param + "";
            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }
        public bool UpdateBenefitMaster(BenefitMasterDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@BenefitMasterId", aInformationDao.BenefitMasterId));
            aSqlParameterlist.Add(new SqlParameter("@Benefit", aInformationDao.Benefit));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));

            const string queryStr = @"UPDATE tblBenefitMaster SET Benefit = @Benefit, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate,IsActive = @IsActive WHERE BenefitMasterId = @BenefitMasterId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }
        public bool UpdateBenefitName(BenefitMasterDAO aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@BenefitMasterId", aInformationDao.BenefitMasterId));
            aSqlParameterlist.Add(new SqlParameter("@Benefit", aInformationDao.Benefit));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));
            aSqlParameterlist.Add(new SqlParameter("@IsActive", aInformationDao.IsActive));

            const string queryStr = @"UPDATE tblBenefitName SET Benefit = @Benefit, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate,IsActive = @IsActive WHERE BenefitMasterId = @BenefitMasterId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }
        public bool DeleteBenefitMaster(string id)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@BenefitMasterId", id));

            const string queryStr = @"DELETE FROM dbo.tblBenefitMaster WHERE BenefitMasterId=@BenefitMasterId";

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

            const string queryStr = @"DELETE FROM dbo.tblBenefitName WHERE BenefitMasterId=@BenefitMasterId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }



        public bool DeleteBenefitDetail(string id)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@BenefitMasterId", id));

            const string queryStr = @"DELETE FROM dbo.tblBenefitDetail WHERE BenefitMasterId=@BenefitMasterId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }
        public bool DeleteBenefitJobNature(string id)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@BenefitMasterId", id));

            const string queryStr = @"DELETE FROM dbo.tblBenefitJobNature WHERE BenefitMasterId=@BenefitMasterId";

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
