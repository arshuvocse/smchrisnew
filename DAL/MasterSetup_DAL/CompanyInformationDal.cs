using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.InternalCls;
using DAO.HRIS_DAO;

namespace DAL.MasterSetup_DAL
{
    public class CompanyInformationDal
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public Int32 SaveComapnyInfo(CompanyInformationDao aInformationDao)
        {
            List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyName", aInformationDao.CompanyName));
            aSqlParameterlist.Add(new SqlParameter("@ShortName", aInformationDao.ShortName));
            aSqlParameterlist.Add(new SqlParameter("@Address", aInformationDao.Address));
            aSqlParameterlist.Add(new SqlParameter("@PhoneNo", aInformationDao.ContactNo));
            aSqlParameterlist.Add(new SqlParameter("@FaxNumber", aInformationDao.FaxNumber));
            aSqlParameterlist.Add(new SqlParameter("@Pabx", aInformationDao.Pabx));
            aSqlParameterlist.Add(new SqlParameter("@EmailAdress", aInformationDao.EmailAdress));
            aSqlParameterlist.Add(new SqlParameter("@Hotline", aInformationDao.Hotline));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@EntryBy", aInformationDao.EntryBy));
            aSqlParameterlist.Add(new SqlParameter("@EntryDate", aInformationDao.EntryDate));

            string insertQuery = @"INSERT INTO tblCompanyInfo (CompanyName,ShortName,Address,ContactNo,FaxNumber,Pabx,EmailAdress,
                                  Hotline,Description,Remarks,EntryBy,EntryDate) VALUES (@CompanyName,@ShortName,@Address,
                                  @PhoneNo,@FaxNumber,@Pabx,@EmailAdress,@Hotline,@Description,@Remarks,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(insertQuery, aSqlParameterlist, "HRDB");
        }

        public DataTable GetCompanyInformation()
        {
            string query = @"SELECT * FROM tblCompanyInfo";
            return aCommonInternalDal.DataContainerDataTable(query, "HRDB");
        }

        public DataTable GetCompanyInformationById(string companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@companyId", companyId));

            const string query = @"SELECT * FROM tblCompanyInfo WHERE CompanyId = @companyId";
            return aCommonInternalDal.DataContainerDataTable(query, aSqlParameterlist, "HRDB");
        }

        public bool UpdateComapnyInfo(CompanyInformationDao aInformationDao)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@CompanyId", aInformationDao.CompanyId));
            aSqlParameterlist.Add(new SqlParameter("@CompanyName", aInformationDao.CompanyName));
            aSqlParameterlist.Add(new SqlParameter("@ShortName", aInformationDao.ShortName));
            aSqlParameterlist.Add(new SqlParameter("@Address", aInformationDao.Address));
            aSqlParameterlist.Add(new SqlParameter("@PhoneNo", aInformationDao.ContactNo));
            aSqlParameterlist.Add(new SqlParameter("@FaxNumber", aInformationDao.FaxNumber));
            aSqlParameterlist.Add(new SqlParameter("@Pabx", aInformationDao.Pabx));
            aSqlParameterlist.Add(new SqlParameter("@EmailAdress", aInformationDao.EmailAdress));
            aSqlParameterlist.Add(new SqlParameter("@Hotline", aInformationDao.Hotline));
            aSqlParameterlist.Add(new SqlParameter("@Description", aInformationDao.Description));
            aSqlParameterlist.Add(new SqlParameter("@Remarks", aInformationDao.Remarks));
            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", aInformationDao.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", aInformationDao.UpdateDate));

            string query = @"UPDATE tblCompanyInfo SET CompanyName = @CompanyName,ShortName = @ShortName,Address = @Address,ContactNo = @PhoneNo,FaxNumber = @FaxNumber,
                             Pabx = @Pabx,EmailAdress = @EmailAdress,Hotline = @Hotline,Description = @Description,Remarks = @Remarks,UpdateBy = @UpdateBy,UpdateDate = @UpdateDate WHERE CompanyId = @CompanyId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(query, aSqlParameterlist, "HRDB");
        }

        public bool DeleteCompanyInfoById(string companyId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@companyId", companyId));

            const string query = @"DELETE FROM tblCompanyInfo WHERE CompanyId = @companyId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(query, aSqlParameterlist, "HRDB");
        }
    }
}
