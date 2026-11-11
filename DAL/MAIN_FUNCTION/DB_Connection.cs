namespace DAL.MAIN_FUNCTION
{
   internal class DB_Connection
    {
       internal static string GenerateString(string DataBaseName)
       {
            //return @"data source=" + DB_Authentication.DataSource + ";Initial Catalog=" + DataBaseName + ";Integrated Security=true;";
            return @"data source=" + DB_Authentication.DataSource + ";Initial Catalog=" + DataBaseName + ";Integrated Security=false; User Id=" + DB_Authentication.UserId + "; password=" + DB_Authentication.Password + ";";
        }
    }
}


