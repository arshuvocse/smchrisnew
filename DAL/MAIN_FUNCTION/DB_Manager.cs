using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace DAL.MAIN_FUNCTION
{
    internal class DB_Manager
    {
        DbTransaction dbTransaction;
        Database database;
        DbCommand dbCommand;
        DbConnection dbConnection;
        private IDataReader dataReader;
        private DataTable dt;
        private DataSet ds;
        bool ActionStatus;

        internal void CreateConnection(string DataBaseName)
        {
            database = new Microsoft.Practices.
                EnterpriseLibrary.Data.Sql.SqlDatabase(DB_Connection.GenerateString(DataBaseName));
            dbConnection = database.CreateConnection();
            if (dbConnection.State != ConnectionState.Open)
            {
                dbConnection.Open();
                dbTransaction = dbConnection.BeginTransaction();
            }
        }  

        internal void CloseConnection()
        {
            if (dbConnection.State == ConnectionState.Open)
            {
                if (dbTransaction != null)
                {
                    dbTransaction.Commit();
                    dbTransaction.Dispose();
                }

                dbConnection.Close();
            }
        }

        private int ExecuteNonQueryAction(string StoreProcedureName, List<SqlParameter> SqlParameterlist,
            string PrimaryKeyParameter, bool IsPrimaryKey)
        {

            int pk = 0;
            try
            {

                dbCommand = database.GetStoredProcCommand(StoreProcedureName);
                dbCommand.CommandTimeout = 12000;
                dbCommand.Parameters.Clear();
                dbCommand.Parameters.AddRange(SqlParameterlist.ToArray());
                if (IsPrimaryKey)
                {
                    database.AddOutParameter(dbCommand, PrimaryKeyParameter, DbType.Int32, 10);
                    if (database.ExecuteNonQuery(dbCommand, dbTransaction) > 0)
                    {
                        pk = int.Parse(dbCommand.Parameters[PrimaryKeyParameter].Value.ToString());
                        dbCommand.Parameters.Clear();
                    }
                    else
                    {
                        pk = 0;
                        dbCommand.Parameters.Clear();
                    }
                }
                else
                {

                    if (database.ExecuteNonQuery(dbCommand, dbTransaction) > 0)
                    {
                        pk = 1;
                        dbCommand.Parameters.Clear();
                    }
                    else
                    {
                        pk = 0;
                        dbCommand.Parameters.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                dbCommand.Parameters.Clear();
                if (dbTransaction != null)
                {
                    dbTransaction.Rollback();
                }
                dbConnection.Close();
            }
            return pk;
        }

        internal bool SaveAction(string StoreProcedureName, List<SqlParameter> SqlParameterlist)
        {

            try
            {
                ActionStatus =
                    Convert.ToBoolean(ExecuteNonQueryAction(StoreProcedureName, SqlParameterlist, string.Empty, false));
            }
            catch (Exception ex)
            {

                ActionStatus = false;
            }
            return ActionStatus;
        }

        internal int SaveAction(string StoreProcedureName, List<SqlParameter> SqlParameterlist,
            string PrimaryKeyParameter)
        {
            int pk = 0;
            try
            {
                pk = ExecuteNonQueryAction(StoreProcedureName, SqlParameterlist, PrimaryKeyParameter, true);
            }
            catch (Exception ex)
            {
                pk = 0;
            }
            return pk;
        }
        internal bool UpdateAction(string StoreProcedureName, List<SqlParameter> SqlParameterlist)
        {
            try
            {
                ActionStatus =
                    Convert.ToBoolean(ExecuteNonQueryAction(StoreProcedureName, SqlParameterlist, string.Empty, false));
            }
            catch (Exception ex)
            {

                ActionStatus = false;
            }

            return ActionStatus;
        }

        internal bool DeleteAction(string StoreProcedureName, List<SqlParameter> SqlParameterlist)
        {
            try
            {
                ActionStatus =
                    Convert.ToBoolean(ExecuteNonQueryAction(StoreProcedureName, SqlParameterlist, string.Empty, false));
            }
            catch (Exception ex)
            {
                ActionStatus = false;
            }
            return ActionStatus;
        }

        internal DataTable GetDataTableAction(string StoreProcedureName)
        {
            dt = new DataTable();
            try
            {
                dbCommand = database.GetStoredProcCommand(StoreProcedureName);
                dbCommand.CommandTimeout = 12000;
                dt = database.ExecuteDataSet(dbCommand, dbTransaction).Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        internal DataTable GetDataTableAction(string StoreProcedureName, List<SqlParameter> SqlParameterlist)
        {
            dt = new DataTable();

            try
            {
                dbCommand = database.GetStoredProcCommand(StoreProcedureName);
                dbCommand.CommandTimeout = 12000;
                dbCommand.Parameters.Clear();
                dbCommand.Parameters.AddRange(SqlParameterlist.ToArray());
                dt = database.ExecuteDataSet(dbCommand, dbTransaction).Tables[0];
                dbCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                dbCommand.Parameters.Clear();
                throw ex;
            }
            return dt;
        }
        internal void LoadAction(RadioButtonList rdl)
        {
            string query = @"select * from tblAction where IsShow=1 ";
            CreateConnection(DB_Names.SR_DB);
            DataTable dtAction = GetDataTableAction("sp_GET_GetActionName");
            CloseConnection();
            rdl.DataSource = dtAction;
            rdl.DataTextField = "ActionText";
            rdl.DataValueField = "ActionId";
            rdl.DataBind();

        }
        internal DataSet GetDataSetAction(string StoreProcedureName)
        {
            ds = new DataSet();
            try
            {
                dbCommand = database.GetStoredProcCommand(StoreProcedureName);
                dbCommand.CommandTimeout = 12000;
                ds = database.ExecuteDataSet(dbCommand, dbTransaction);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        internal DataSet GetDataSetAction(string StoreProcedureName, List<SqlParameter> SqlParameterlist)
        {
            ds = new DataSet();
            try
            {
                dbCommand = database.GetStoredProcCommand(StoreProcedureName);
                dbCommand.CommandTimeout = 12000;
                dbCommand.Parameters.Clear();
                dbCommand.Parameters.AddRange(SqlParameterlist.ToArray());
                ds = database.ExecuteDataSet(dbCommand, dbTransaction);
                dbCommand.Parameters.Clear();
            }
            catch (Exception ex)
            {
                dbCommand.Parameters.Clear();
                throw ex;
            }

            return ds;
        }

        internal IDataReader GetDataReaderAction(string StoreProcedure, List<SqlParameter> SqlParameterlist,
            string DataBaseName)
        {
            try
            {
                dbCommand = database.GetStoredProcCommand(StoreProcedure);
                dbCommand.CommandTimeout = 12000;
                dbCommand.Parameters.Clear();
                dbCommand.Parameters.AddRange(SqlParameterlist.ToArray());
                dataReader = database.ExecuteReader(dbCommand, dbTransaction);
                dbCommand.Parameters.Clear();
                return dataReader;
            }
            catch (Exception ex)
            {
                dbCommand.Parameters.Clear();
                throw ex;
            }
        }
        internal IDataReader GetDataReaderAction(string StoreProcedure, List<SqlParameter> SqlParameterlist
          )
        {
            try
            {
                dbCommand = database.GetStoredProcCommand(StoreProcedure);
                dbCommand.CommandTimeout = 12000;
                dbCommand.Parameters.Clear();
                dbCommand.Parameters.AddRange(SqlParameterlist.ToArray());
                dataReader = database.ExecuteReader(dbCommand, dbTransaction);
                dbCommand.Parameters.Clear();
                return dataReader;
            }
            catch (Exception ex)
            {
                dbCommand.Parameters.Clear();
                throw ex;
            }
        }

        public void LoadDropDownListData(DropDownList dropDownList, string DisplayField, string ValueField, string StoreProcedure, List<SqlParameter> SqlParameterlist)
        {
            try
            {
                DataTable dataDDL = new DataTable();
                dataDDL = GetDataTableAction(StoreProcedure, SqlParameterlist);
                dropDownList.DataTextField = DisplayField;
                dropDownList.DataValueField = ValueField;
                dropDownList.DataSource = dataDDL;
                dropDownList.DataBind();
                dropDownList.Items.Insert(0, new ListItem("Select--------------------", String.Empty));
                dropDownList.SelectedIndex = 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void LoadDropDownListData(DropDownList dropDownList, string DisplayField, string ValueField, string StoreProcedure)
        {
            try
            {
                DataTable dataDDL = new DataTable();
                dataDDL = GetDataTableAction(StoreProcedure);
                dropDownList.DataTextField = DisplayField;
                dropDownList.DataValueField = ValueField;
                dropDownList.DataSource = dataDDL;
                dropDownList.DataBind();
                dropDownList.Items.Insert(0, new ListItem("Select--------------------", String.Empty));
                dropDownList.SelectedIndex = 0;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        //public void LoadComboBoxData(AjaxControlToolkit.ComboBox dropDownList, string DisplayField, string ValueField, string StoreProcedure, List<SqlParameter> SqlParameterlist)
        //{
        //    try
        //    {
        //        DataTable dataDDL = new DataTable();
        //        dataDDL = GetDataTableAction(StoreProcedure, SqlParameterlist);
        //        dropDownList.DataTextField = DisplayField;
        //        dropDownList.DataValueField = ValueField;
        //        dropDownList.DataSource = dataDDL;
        //        dropDownList.DataBind();
        //        dropDownList.Items.Insert(0, new ListItem("Select--------------------", String.Empty));
        //        dropDownList.SelectedIndex = 0;
        //    }
        //    catch (Exception exception)
        //    {
        //        throw exception;
        //    }
        //}
        //public void LoadComboBoxData(AjaxControlToolkit.ComboBox dropDownList, string DisplayField, string ValueField, string StoreProcedure)
        //{
        //    try
        //    {
        //        DataTable dataDDL = new DataTable();
        //        dataDDL = GetDataTableAction(StoreProcedure);
        //        dropDownList.DataTextField = DisplayField;
        //        dropDownList.DataValueField = ValueField;
        //        dropDownList.DataSource = dataDDL;
        //        dropDownList.DataBind();
        //        dropDownList.Items.Insert(0, new ListItem("Select--------------------", String.Empty));
        //        dropDownList.SelectedIndex = 0;
        //    }
        //    catch (Exception exception)
        //    {
        //        throw exception;
        //    }
        //}

    }
}
