using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace kt_system.Helpers
{
    public static class DaoHelper
    {
        private static string _connStr;       
        public static void Init(IConfiguration config)
        {_connStr = config.GetConnectionString("DefaultConnection");}

        public static string DLookup(string field, string table, string where)
        {
            using SqlConnection conn = new(_connStr);
            conn.Open();
            string sql = $"SELECT {field} FROM {table} WHERE {where}";

            using SqlCommand cmd = new(sql, conn);
            object result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? null : result.ToString();
        }

        public static int DCount(string table, string where)
        {
            using SqlConnection conn = new(_connStr);
            conn.Open();
            string sql = $"SELECT COUNT(*) FROM {table} WHERE {where}";

            using SqlCommand cmd = new(sql, conn);
            object result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
        }

        public static string DMax(string field, string table, string where)
        {
            using SqlConnection conn = new(_connStr);
            conn.Open();
            string sql = $"SELECT Max({field}) FROM {table} WHERE {where}";

            using SqlCommand cmd = new(sql, conn);
            object result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? null : result.ToString();
        }

        public static string DMin(string field, string table, string where)
        {
            using SqlConnection conn = new(_connStr);
            conn.Open();
            string sql = $"SELECT Min({field}) FROM {table} WHERE {where}";

            using SqlCommand cmd = new(sql, conn);
            object result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? null : result.ToString();
        }

        public static int DMaxInt(string field, string table, string where)
        {
            using SqlConnection conn = new(_connStr);
            conn.Open();
            string sql = $"SELECT Max({field}) FROM {table} WHERE {where}";

            using SqlCommand cmd = new(sql, conn);
            object result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
        }

        public static bool DExists(string table, string where) 
        {return DCount(table, where) > 0;}                                              //if (DbHelper.DExists("users4", $"EmpId={UserObj.EmpId}")) {//duplicate}

        public static int DExecute(string sql)
        {
            using SqlConnection conn = new(_connStr);
            conn.Open();

            using SqlCommand cmd = new(sql, conn);
            int affected = cmd.ExecuteNonQuery();

            return affected;   // number of rows affected
        }

        
        public static List<Dictionary<string, object>> DList(string sql)    //DList (multiple rows)
        {
            var list = new List<Dictionary<string, object>>();

            using SqlConnection conn = new(_connStr);
            conn.Open();

            using SqlCommand cmd = new(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var row = new Dictionary<string, object>();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    object value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                    row[reader.GetName(i)] = value;
                }

                list.Add(row);
            }

            return list;
            //var types = DList("SELECT Id, Name FROM LeaveTypes ORDER BY Name");
        }



        public static DataTable GetDataTable(string sql, Dictionary<string, object>? parameters = null)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = CommandType.Text;

                        if (parameters != null)
                        {
                            foreach (var param in parameters)
                            {
                                cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                            }
                        }

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in GetDataTable: {ex.Message}");
                throw;
            }

            return dt;
        }


        public static Dictionary<string, object> DRow(string sql)   //DRow (single row)
        {
            var row = new Dictionary<string, object>();

            using SqlConnection conn = new(_connStr);
            conn.Open();

            using SqlCommand cmd = new(sql, conn);
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    object value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                    row[reader.GetName(i)] = value;
                }
            }

            return row;
            //Usage example of DRow:
            //var row = DRow("SELECT * FROM Leaves WHERE id=5");
            //string emp = row["EmpId"].ToString();
        }



    }
}




/*
    DList — Full Implementation


    Usage Examples
    ✔ Get all users
    var users = DList("SELECT * FROM users4 ORDER BY UserName");


    ✔ Loop through results✔ Loop through results
    foreach (var row in users)
    {
        string name = row["UserName"]?.ToString();
        string dept = row["Dept"]?.ToString();
        int empId = Convert.ToInt32(row["EmpId"]);

        // use values...
    }

*/












































/*
if (result == null || result == DBNull.Value)
{
    return null;
}
else
{
    return result.ToString();
}
*/

/*
if (result == null || result == DBNull.Value)
    return 0;
return Convert.ToInt32(result);
*/






//using Microsoft.Data.SqlClient;
//namespace kt_system.Helpers
//{
//    public static class DbHelper
//    {
//        public static object DLookup(string field, string table, string where, string connStr)
//        {
//            string sql = $"SELECT {field} FROM {table} WHERE {where}";

//            using SqlConnection conn = new(connStr);
//            conn.Open();

//            using SqlCommand cmd = new(sql, conn);
//            object result = cmd.ExecuteScalar();

//            return result == DBNull.Value ? null : result;
//        }
//    }
//}
