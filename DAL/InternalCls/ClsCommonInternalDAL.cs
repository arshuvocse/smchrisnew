using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DAL.InternalCls
{
    public class ClsCommonInternalDAL
    {

        internal void LoadAction(RadioButtonList rdl)
        {
            string query = @"select * from tblAction where IsShow=1 ";
            DataTable dtAction = DataContainerDataTable(query, "HRDB");
            rdl.DataSource = dtAction;
            rdl.DataTextField = "ActionText";
            rdl.DataValueField = "ActionId";
            rdl.DataBind();

        }
        internal void LoadDropDownValue(DropDownList ddl, string displayField, string valueField, string queryString, string dataBaseName)
        {
            try
            {
                DataTable dataDDL = new DataTable();
                Database db;
                DbCommand dbCommand;
                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetSqlStringCommand(queryString);

                dataDDL = db.ExecuteDataSet(dbCommand).Tables[0];
                ddl.DataTextField = displayField;
                ddl.DataValueField = valueField;
                ddl.DataSource = dataDDL;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select any one", String.Empty));
                ddl.SelectedIndex = 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }



      

        internal void LoadDropDownValueCompany(DropDownList ddl, string displayField, string valueField, string queryString, string dataBaseName)
        {
            try
            {
                DataTable dataDDL = new DataTable();
                Database db;
                DbCommand dbCommand;
                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetSqlStringCommand(queryString);

                dataDDL = db.ExecuteDataSet(dbCommand).Tables[0];
                ddl.DataTextField = displayField;
                ddl.DataValueField = valueField;
                ddl.DataSource = dataDDL;
                ddl.DataBind();
                
                ddl.SelectedIndex = 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }


        internal void LoadDropDownValue(DropDownList ddl, string displayField, string valueField, string queryString, List<SqlParameter> aSqlParameterlist, string dataBaseName)
        {
            try
            {
                DataTable dataDDL = new DataTable();
                Database db;
                DbCommand dbCommand;
                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand.Parameters.Clear();
                dbCommand.Parameters.AddRange(aSqlParameterlist.ToArray());

                dataDDL = db.ExecuteDataSet(dbCommand).Tables[0];
                ddl.DataTextField = displayField;
                ddl.DataValueField = valueField;
                ddl.DataSource = dataDDL;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select any one", String.Empty));
                ddl.SelectedIndex = 0;
                dbCommand.Parameters.Clear();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        internal IDataReader DataContainerDataReader(string queryString, List<SqlParameter> aSqlParameterlist, string dataBaseName)
        {
            try
            {
                IDataReader dataReader;
                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand.Parameters.Clear();
                dbCommand.Parameters.AddRange(aSqlParameterlist.ToArray());
                dataReader = db.ExecuteReader(dbCommand);
                dbCommand.Parameters.Clear();
                return dataReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal IDataReader DataContainerDataReader(string queryString, string dataBaseName)
        {
            try
            {
                IDataReader dataReader;
                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetSqlStringCommand(queryString);
                dataReader = db.ExecuteReader(dbCommand);
                return dataReader;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DataContainerDataTable(string queryString, List<SqlParameter> aSqlParameterlist, string dataBaseName)
        {
            try
            {
                DataTable dataContain = new DataTable();

                Database db;
                DbCommand dbCommand;
                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand.Parameters.Clear();
                if (aSqlParameterlist != null)
                {
                    dbCommand.Parameters.AddRange(aSqlParameterlist.ToArray());
                }
                dataContain = db.ExecuteDataSet(dbCommand).Tables[0];
                dbCommand.Parameters.Clear();
                return dataContain;
            }
            catch (Exception ex)
            {
                throw  ;
            }
        }
        public DataTable DataContainerDataTableNew(string queryString, List<SqlParameter> aSqlParameterlist, string dataBaseName)
        {
            try
            {
                DataTable dataContain = new DataTable();

                Database db;
                DbCommand dbCommand;
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand.CommandTimeout = 12000;
                dbCommand.Parameters.Clear();
                if (aSqlParameterlist != null)
                {
                    dbCommand.Parameters.AddRange(aSqlParameterlist.ToArray());
                }
                dataContain = db.ExecuteDataSet(dbCommand).Tables[0];
                dbCommand.Parameters.Clear();
                return dataContain;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        internal bool SaveDataByInsertSP(string StoreProcedure, List<SqlParameter> aSqlParameterlist, string dataBaseName)
        {
            try
            {
                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetStoredProcCommand(StoreProcedure);

                dbCommand.Parameters.Clear();
                dbCommand.Parameters.AddRange(aSqlParameterlist.ToArray());

                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    dbCommand.Parameters.Clear();
                    return true;
                }
                else
                {
                    dbCommand.Parameters.Clear();
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        public DataTable GetDataByStoreProcedure(string StoreProcedure, List<SqlParameter> aSqlParameterlist, string dataBaseName)
        {
            try
            {
                DataTable dataContain = new DataTable();

                Database db;
                DbCommand dbCommand;
                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetStoredProcCommand(StoreProcedure);
                dbCommand.Parameters.Clear();
                if (aSqlParameterlist != null)
                {
                    dbCommand.Parameters.AddRange(aSqlParameterlist.ToArray());
                }
                dataContain = db.ExecuteDataSet(dbCommand).Tables[0];
                dbCommand.Parameters.Clear();
                return dataContain;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal DataTable DataContainerDataTable(string queryString, string dataBaseName)
        {
            try
            {
                DataTable dataContain = new DataTable();

                Database db;
                DbCommand dbCommand;
                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetSqlStringCommand(queryString);

                dataContain = db.ExecuteDataSet(dbCommand).Tables[0];
                return dataContain;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal DataTable GetDTforDDL(string queryString, List<SqlParameter> aSqlParameterlist, string dataBaseName)
        {
            DataTable dataContain = null;
            try
            {
                using (dataContain = new DataTable())
                {
                    Database db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                    DbCommand dbCommand= db.GetSqlStringCommand(queryString);
                    dbCommand.Parameters.Clear();
                    if (aSqlParameterlist != null)
                    {
                        dbCommand.Parameters.AddRange(aSqlParameterlist.ToArray());
                    }
                    dataContain = db.ExecuteDataSet(dbCommand).Tables[0];
                    dbCommand.Parameters.Clear();
                    DataRow dr = dataContain.NewRow();
                    dr["Value"] = "-1";
                    dr["TextField"] = "Select...";
                    dataContain.Rows.InsertAt(dr, 0);
                    return dataContain;
                }
            }
            catch (Exception ex)
            {
                ////TODO log the exception into database
                return dataContain;
            }
        }


        internal DataTable GetDTforDDLCheckList(string queryString, List<SqlParameter> aSqlParameterlist, string dataBaseName)
        {
            DataTable dataContain = null;
            try
            {
                using (dataContain = new DataTable())
                {
                    Database db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                    DbCommand dbCommand = db.GetSqlStringCommand(queryString);
                    dbCommand.Parameters.Clear();
                    if (aSqlParameterlist != null)
                    {
                        dbCommand.Parameters.AddRange(aSqlParameterlist.ToArray());
                    }
                    dataContain = db.ExecuteDataSet(dbCommand).Tables[0];
                    dbCommand.Parameters.Clear();
                  
              
                    return dataContain;
                }
            }
            catch (Exception ex)
            {
                ////TODO log the exception into database
                return dataContain;
            }
        }


        internal DataTable GetDTforDDLForChartCompanyLoad(string queryString, List<SqlParameter> aSqlParameterlist, string dataBaseName)
        {
            DataTable dataContain = null;
            try
            {
                using (dataContain = new DataTable())
                {
                    Database db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                    DbCommand dbCommand = db.GetSqlStringCommand(queryString);
                    dbCommand.Parameters.Clear();
                    if (aSqlParameterlist != null)
                    {
                        dbCommand.Parameters.AddRange(aSqlParameterlist.ToArray());
                    }
                    dataContain = db.ExecuteDataSet(dbCommand).Tables[0];
                    dbCommand.Parameters.Clear();
             
                  
                    return dataContain;
                }
            }
            catch (Exception ex)
            {
                ////TODO log the exception into database
                return dataContain;
            }
        }
        internal bool SaveDataByInsertCommand(string queryString, string dataBaseName)
        {
            try
            {
                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetSqlStringCommand(queryString);
               

                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    
                    return true;
                }
                else
                {
                    
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal int SaveDataByInsertCommandById(string queryString, List<SqlParameter> aSqlParameterlist, string dataBaseName)
        {
            try
            {
                Database db;
                DbCommand dbCommand;
                int id = 0;
                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetSqlStringCommand(queryString + "  select scope_identity()as ID ");
                dbCommand.Parameters.Clear();
                dbCommand.Parameters.AddRange(aSqlParameterlist.ToArray());
                id = Convert.ToInt32(db.ExecuteDataSet(dbCommand).Tables[0].Rows[0][0].ToString());
                if (id > 0)
                {
                    dbCommand.Parameters.Clear();
                    return id;
                }
                else
                {
                    dbCommand.Parameters.Clear();
                    return id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal bool SaveDataByInsertCommand(string queryString, List<SqlParameter> aSqlParameterlist, string dataBaseName)
        {
            try
            {
                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand.Parameters.Clear();
                dbCommand.Parameters.AddRange(aSqlParameterlist.ToArray());

                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    dbCommand.Parameters.Clear();
                    return true;
                }
                else
                {
                    dbCommand.Parameters.Clear();
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        internal bool UpdateDataByUpdateCommand(string queryString, string dataBaseName)
        {
            try
            {
                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetSqlStringCommand(queryString);
                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                   
                    return true;
                }
                else
                {
                    
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        internal bool UpdateDataByUpdateCommand(string queryString, List<SqlParameter> aSqlParameterlist, string dataBaseName)
        {
            try
            {
                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand.Parameters.Clear();
                dbCommand.Parameters.AddRange(aSqlParameterlist.ToArray());
                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    dbCommand.Parameters.Clear();
                    return true;
                }
                else
                {
                    dbCommand.Parameters.Clear();
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        internal bool DeleteDataByDeleteCommand(string queryString, List<SqlParameter> aSqlParameterlist, string dataBaseName)
        {
            try
            {
                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetSqlStringCommand(queryString);
                dbCommand.Parameters.Clear();
                dbCommand.Parameters.AddRange(aSqlParameterlist.ToArray());
                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    dbCommand.Parameters.Clear();
                    return true;
                }
                else
                {
                    dbCommand.Parameters.Clear();
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal bool DeleteDataByDeleteCommand(string queryString, string dataBaseName)
        {
            try
            {
                Database db;
                DbCommand dbCommand;

                //Prepare Database Call
                db = DatabaseFactory.CreateDatabase("SolutionConnectionString" + dataBaseName);
                dbCommand = db.GetSqlStringCommand(queryString);

                if (db.ExecuteNonQuery(dbCommand) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal bool DeleteStatusUpdate(string tableName,string primaryField,string primaryId)
        {
            try
            {
                //string query = @"update " + tableName + " SET DeleteBy='" + HttpContext.Current.Session["LoginName"].ToString() + "',DeleteDate='" + System.DateTime.Now + "', IsActive='False' where " + primaryField + " = '" + primaryId + "'";
              //return  UpdateDataByUpdateCommand(query, "HRDB");

                string query = "delete from " + tableName + " where " + primaryField + " = '" + primaryId + "'";
                string insertQuery =
                    "INSERT INTO dbo.tbl_DeleteLog ( TableName ,PrimaryFieldName , PrimaryId , DeleteDate ,DeleteTime , DeleteBy)VALUES  ('" + tableName + "','" + primaryField + "','" + primaryId + "','" + System.DateTime.Today.ToString() + "' ,'" + System.DateTime.Now.ToString() + "','" + HttpContext.Current.Session["LoginName"].ToString() + "' )";
                SaveDataByInsertCommand(insertQuery, "HRDB");
                return DeleteDataByDeleteCommand(query, "HRDB");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
