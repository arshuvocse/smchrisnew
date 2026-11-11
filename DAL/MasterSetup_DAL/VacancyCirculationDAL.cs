using DAL.InternalCls;
using DAO.HRIS_DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MasterSetup_DAL
{
  public  class VacancyCirculationDAL
    {

        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public Int32 SaveVacancyCirculation(VacancyCirculationDAO aVacancyDAO)
        {
            List<SqlParameter> aSqlParameterList = new List<SqlParameter>();

            aSqlParameterList.Add(new SqlParameter("@CirculationWay", aVacancyDAO.CirculationWay));

            aSqlParameterList.Add(new SqlParameter("@IsActive", aVacancyDAO.IsActive));
            aSqlParameterList.Add(new SqlParameter("@EntryBy", aVacancyDAO.EntryBy));
            aSqlParameterList.Add(new SqlParameter("@EntryDate", aVacancyDAO.EntryDate));

            string inserQuery = @"INSERT INTO tblVacancyCirculation (CirculationWay,IsActive,EntryBy,EntryDate) VALUES(@CirculationWay,@IsActive,@EntryBy,@EntryDate)";

            return aCommonInternalDal.SaveDataByInsertCommandById(inserQuery, aSqlParameterList, "HRDB");
        }

 

        public DataTable GetVacancyCirculationSetup()
        {
            const string queryStr = @"SELECT * FROM tblVacancyCirculation";

            return aCommonInternalDal.DataContainerDataTable(queryStr, "HRDB");
        }

        public DataTable GetVacancyCirculationById(string vacancyCirculationId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@vacancyCirculationId", vacancyCirculationId));

            const string queryStr = @"SELECT * FROM tblVacancyCirculation WHERE VacancyCirculationId = @VacancyCirculationId";

            return aCommonInternalDal.DataContainerDataTable(queryStr, aSqlParameterlist, "HRDB");
        }


        public bool UpdateVacancyCirculationSetup(VacancyCirculationDAO vacancyCirculationDAO)
        {
            var aSqlParameterlist = new List<SqlParameter>();

            aSqlParameterlist.Add(new SqlParameter("@VacancyCirculationId", vacancyCirculationDAO.VacancyCirculationId));
            aSqlParameterlist.Add(new SqlParameter("@CirculationWay", vacancyCirculationDAO.CirculationWay));

            aSqlParameterlist.Add(new SqlParameter("@IsActive", vacancyCirculationDAO.IsActive));

            aSqlParameterlist.Add(new SqlParameter("@UpdateBy", vacancyCirculationDAO.UpdateBy));
            aSqlParameterlist.Add(new SqlParameter("@UpdateDate", vacancyCirculationDAO.UpdateDate));

            const string queryStr = @"UPDATE tblVacancyCirculation SET  CirculationWay = @CirculationWay,
                                      IsActive = @IsActive, UpdateBy = @UpdateBy,UpdateDate = @UpdateDate WHERE VacancyCirculationId = @VacancyCirculationId";

            return aCommonInternalDal.UpdateDataByUpdateCommand(queryStr, aSqlParameterlist, "HRDB");
        }


        public bool DeleteVacancyCirculationById(string vacancyCirculationId)
        {
            var aSqlParameterlist = new List<SqlParameter>();
            aSqlParameterlist.Add(new SqlParameter("@VacancyCirculationId", vacancyCirculationId));

            const string queryStr = "DELETE FROM tblVacancyCirculation WHERE VacancyCirculationId = @VacancyCirculationId";
            return aCommonInternalDal.DeleteDataByDeleteCommand(queryStr, aSqlParameterlist, "HRDB");
        }





    }

}
