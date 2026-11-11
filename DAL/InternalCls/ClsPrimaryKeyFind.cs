using System;
using System.Data;

namespace DAL.InternalCls
{
  public  class ClsPrimaryKeyFind
    {
        ClsCommonInternalDAL aCommonInternalDal = new ClsCommonInternalDAL();
        public int PrimaryKeyMax(string columnName, string tableName, string dataBaseName)
        {
            try
            {
                int pk = 0;
                DataTable aTableForPk = new DataTable();
                string query = @"SELECT (isnull(MAX(" + columnName + "),0)+1) as PKMaxNo FROM " + tableName;
                aTableForPk = aCommonInternalDal.DataContainerDataTable(query, dataBaseName);
                pk = Int32.Parse(aTableForPk.Rows[0][0].ToString().Trim());
                return pk;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        //public int PrimaryKeyMax(string columnName, string tableName)
        //{
        //    try
        //    {
        //        int pk = 0;
        //        DataTable aTableForPk = new DataTable();
        //        string query = @"SELECT (isnull(MAX(" + columnName + "),0)+1) as PKMaxNo FROM " + tableName;
        //        aTableForPk = aCommonInternalDal.DataContainerDataTable(query);
        //        pk = Int32.Parse(aTableForPk.Rows[0][0].ToString().Trim());
        //        return pk;
        //    }
        //    catch (Exception exception)
        //    {

        //        throw exception;
        //    }
        //}
    }
}
