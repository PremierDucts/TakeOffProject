using MySql.Data.MySqlClient;
using System.Data;

namespace TakeOffAPI.DatabaseUtility
{
    public class AdvancedDatabaseQuery
    {
        public static DataTable ExecuteSelectWithParamAdapter(String sqlQuery, Dictionary<String, Object> pars, String connStr)
        {
            if (sqlQuery == null)
                return null;
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        using (var cmd = new MySqlDataAdapter(sqlQuery, conn))
                        {
                            ParseParamsToCommandAdapter(cmd, pars);
                            var dt = new DataTable();
                            cmd.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }

        public static bool CommandExecuteWithParam(String query, Dictionary<String, Object> pars, String connStr)
        {
            if (query == null)
                return false;
            int ret = -1;
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    if (conn.State == ConnectionState.Open)
                    {
                        using (var cmd = new MySqlCommand(query, conn))
                        {
                            ParseParamsToCommand(cmd, pars);
                            ret = cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return ret != -1;
        }

        private static void ParseParamsToCommandAdapter(MySqlDataAdapter cmd, Dictionary<String, Object> pars)
        {
            if (cmd == null)
                return;
            if (pars == null)
                return;
            foreach (var p in pars)
            {
                if (p.Value != null)
                {
                    var vType = p.Value.GetType();
                    if (vType.IsEnum)
                        cmd.SelectCommand.Parameters.AddWithValue(p.Key, (int)p.Value);
                    else
                        cmd.SelectCommand.Parameters.AddWithValue(p.Key, p.Value);
                }
            }
        }

        private static void ParseParamsToCommand(MySqlCommand cmd, Dictionary<String, Object> pars)
        {
            if (cmd == null)
                return;
            if (pars == null)
                return;
            foreach (var p in pars)
            {
                if (p.Value != null)
                {
                    var vType = p.Value.GetType();
                    if (vType.IsEnum)
                        cmd.Parameters.AddWithValue(p.Key, (int)p.Value);
                    else
                        cmd.Parameters.AddWithValue(p.Key, p.Value);
                }
            }
        }

        public static String[] ConvertListStringToArrayStr(params List<String>[] listStrs)
        {
            if (listStrs.Length == 0)
                return null;

            List<String> listRetStr = null;
            if (listStrs.Length == 1)
                listRetStr = listStrs[0]; //For common case
            else
            {
                listRetStr = new List<String>();
                foreach (var listStr in listStrs)
                    listRetStr.AddRange(listStr);
            }
            if (listRetStr.Count == 0)
                return null;

            return listRetStr.ToArray();
        }
    }
}
