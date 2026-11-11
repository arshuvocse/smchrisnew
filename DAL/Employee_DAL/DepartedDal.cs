using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.DataManager;
using DAL.InternalCls;
using DAO.HRIS_DAO;
using iTextSharp.text;

namespace DAL.Employee_DAL
{
    public class DepartedDal
    {

        readonly ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();

        public int SaveDepartedInfo(List<DepartedDao> aList)
        {

            int id = 0;

            foreach (var list in aList)
            {
                List<SqlParameter> aSqlParameterlist = new List<SqlParameter>();
                DepartedDao aDepartedDao = new DepartedDao();

                //aDepartedDao.DepartedId = list.DepartedId;
                aDepartedDao.EmpInfoId = list.EmpInfoId;
                aDepartedDao.Relative = list.Relative;
                aDepartedDao.Name = list.Name;
                aDepartedDao.DeathofDate = list.DeathofDate;
                aDepartedDao.Remarks = list.Remarks;
                aDepartedDao.UploadImage = list.UploadImage;
                aDepartedDao.ImagePath = list.ImagePath;

                //aSqlParameterlist.Add(new SqlParameter("@DepartedId", aDepartedDao.DepartedId));
                aSqlParameterlist.Add(new SqlParameter("@EmpInfoId", aDepartedDao.EmpInfoId));
                aSqlParameterlist.Add(new SqlParameter("@Relative", (object)aDepartedDao.Relative ?? DBNull.Value));
                aSqlParameterlist.Add(new SqlParameter("@Name", (object)aDepartedDao.Name ?? DBNull.Value));
                aSqlParameterlist.Add(new SqlParameter("@DeathofDate", (object) aDepartedDao.DeathofDate ?? DBNull.Value));
                aSqlParameterlist.Add(new SqlParameter("@Remarks", (object)aDepartedDao.Remarks ?? DBNull.Value));
                //aSqlParameterlist.Add(new SqlParameter("@UploadImage", (object) aDepartedDao.UploadImage ?? DBNull.Value));
                //aSqlParameterlist.Add(new SqlParameter("@ImagePath", (object)aDepartedDao.ImagePath ?? DBNull.Value));
                aSqlParameterlist.Add(new SqlParameter("@EntryBy", HttpContext.Current.Session["UserId"].ToString()));
                aSqlParameterlist.Add(new SqlParameter("@EntryDate",   DateTime.Now));

                string query = @"INSERT INTO dbo.tblDepartedInfo
                           (
                            EmpInfoId, Relative,                        
                            Name,
                            Remarks,
                            DeathofDate,
                       
                            EntryBy,
                            EntryDate                         
                            )
                            VALUES
                            (
                            @EmpInfoId, @Relative,                          
                            @Name,
                            @Remarks,
                            @DeathofDate,
                   
                            @EntryBy,
                            @EntryDate
                            )";

                id= aCommonInternalDal.SaveDataByInsertCommandById(query, aSqlParameterlist, DataBase.HRDB);

            }

            return id;
        }



        public bool DeleteDetails(string Id)
        {
            string query = @"Delete from tblDepartedInfo where EmpInfoId=" + Id;

            return aCommonInternalDal.DeleteDataByDeleteCommand(query, "HRDB");
        }
        
        
    }
}
