using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using System.Web;


/// <summary>
/// Summary description for ConnMethods
/// </summary>
//Data Source = (localdb)\mssqllocaldb; Initial Catalog = buildonlinedb; Persist Security Info=True; User ID = qlitygigs; Password =●●●●
public class SqlConnMethod
{
    string Pass = ConfigurationManager.AppSettings["SQLServerPassword"];
    string ServerName = ConfigurationManager.AppSettings["SQLServerName"];
    string DBUsername = ConfigurationManager.AppSettings["SQLServerUser"];
    string db = "buildonlinedb";

    public string SingleRespSQL(string query)
    {
       
        string result = "";
        string conStr = "workstation id=" + ServerName + ";packet size=4096;user id=" + DBUsername + ";password=" + Pass + ";data source=" + ServerName + ";persist security info=False;initial catalog=" + db;
        SqlConnection connection = new SqlConnection(conStr);
        //persist security info=True
        try
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Connection.Open();
                result = command.ExecuteScalar().ToString();
                command.Connection.Close();
            }
        }
        catch (SqlException sqlex)
        {
            logthefile("SingleRespSQL Error Qry: " + query + " Error : " + sqlex.ToString());
        }
       

        return result;
    }

    public DataTable DTSQL(string query)
    {

        string conStr = "workstation id=" + ServerName + ";packet size=4096;user id=" + DBUsername + ";password=" + Pass + ";data source=" + ServerName + ";persist security info=False;initial catalog=" + db;
        
        DataTable Response = new DataTable();

        SqlConnection connection = new SqlConnection(conStr);

        try
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Connection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command); //c.con is the connection string
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                command.CommandTimeout = 3600;
                dataAdapter.Fill(Response);
                command.Connection.Close();
            }
        }
        catch (SqlException sqlex)
        {
            Console.WriteLine(sqlex.Message);
            logthefile("DTSQL ERROR QRY : " + query + " Error: " + sqlex.ToString());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            logthefile("DTSQL Failed " + ex.ToString());
        }
        finally
        {
            connection.Close();
        }

       // logthefile(Response.Rows.Count.ToString());


        return Response;
    }

    public string SingleRespParaSQL(List<SqlParameter> sqlParams, string query)
    {
        
        string ret = "No Error";
        string conStr = "workstation id=" + ServerName + ";packet size=4096;user id=" + DBUsername + ";password=" + Pass + ";data source=" + ServerName + ";persist security info=False;initial catalog=" + db;
        SqlConnection connection = new SqlConnection(conStr);
        try
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Connection.Open();
                command.Parameters.AddRange(sqlParams.ToArray());
                ret = command.ExecuteNonQuery().ToString();
                command.Connection.Close();
            }

        }
        catch (SqlException sqlex)
        {
            ret = "SingleRespParaSQL: " + sqlex.ToString();
        }
        catch (Exception ex)
        {
            ret = ex.ToString();
        }
        finally
        {
            connection.Close();
        }

        return ret;
    }

    public string SingleRespParaSQLReturnScope(List<SqlParameter> sqlParams, string query)
    {
       
        logthefile("starting");
        string ret = "No Error";
        string conStr = "workstation id=" + ServerName + ";packet size=4096;user id=" + DBUsername + ";password=" + Pass + ";data source=" + ServerName + ";persist security info=False;initial catalog=" + db;
        SqlConnection connection = new SqlConnection(conStr);
        try
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Connection.Open();
                command.Parameters.AddRange(sqlParams.ToArray());
                ret = command.ExecuteScalar().ToString();
                command.Connection.Close();
            }
        }
        catch (SqlException sqlex)
        {
            logthefile("Backup Error: " + sqlex.ToString());
        }
        catch (Exception ex)
        {
            logthefile(ex.ToString());
        }
        finally
        {
            connection.Close();
        }

        return ret;
    }

    public string SingleRespParaSQLScope(List<SqlParameter> sqlParams, string query)
    {
        
        string ret = "No Error";
        string conStr = "workstation id=" + ServerName + ";packet size=4096;user id=" + DBUsername + ";password=" + Pass + ";data source=" + ServerName + ";persist security info=False;initial catalog=" + db;
        SqlConnection connection = new SqlConnection(conStr);
        try
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Connection.Open();
                command.Parameters.AddRange(sqlParams.ToArray());
                var returner = (int)command.ExecuteScalar();
                ret = returner.ToString();
                command.Connection.Close();
            }
        }
        catch (SqlException sqlex)
        {
            logthefile("Backup Error: " + sqlex.ToString());
            ret = "Backup Error: " + sqlex.ToString();
        }
        catch (Exception ex)
        {
            ret = ex.ToString();
        }
        finally
        {
            connection.Close();
        }

        return ret;
    }

    public int SingleIntSQL(string query)
    {
       
        int ret = 0;
        string conStr = "workstation id=" + ServerName + ";packet size=4096;user id=" + DBUsername + ";password=" + Pass + ";data source=" + ServerName + ";persist security info=False;initial catalog=" + db;
        SqlConnection connection = new SqlConnection(conStr);
        try
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Connection.Open();
                ret = command.ExecuteNonQuery();
                command.Connection.Close();
            }

        }
        catch (SqlException sqlex)
        {
            logthefile("SingleIntSQL Error Qry  " + query + " SQL Error: " + sqlex.ToString());
        }
        catch (Exception ex)
        {
            logthefile(query + " General Error: " + ex.ToString());
        }

        return ret;
    }

    public DataTable DTSPSQL(SqlParameter[] sqlParams, string SPName)
    {
        string para = "";

        string conStr = "workstation id=" + ServerName + ";packet size=4096;user id=" + DBUsername + ";password=" + Pass + ";data source=" + ServerName + ";persist security info=False;initial catalog=" + db;
        DataTable ret = new DataTable();
        try
        {
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection())
            {
                using (System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand())
                {
                    conn.ConnectionString = conStr;
                    conn.Open();
                    comm.Connection = conn;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = SPName;
                    if (sqlParams != null)
                    {
                        foreach (SqlParameter p in sqlParams)
                        {
                            para += p.ToString();
                            comm.Parameters.Add(p);
                        }
                    }
                    comm.CommandTimeout = 6500;
                    SqlDataReader dr = comm.ExecuteReader();
                    ret.Load(dr);
                }
            }
        }
        catch (SqlException sqlex)
        {
            logthefile("DTSPSQL Error QRY " + SPName + " SQL Error "  + sqlex.ToString());
        }

        return ret;
    }

    public string SingleIntSPSQL(SqlParameter[] sqlParams, string SPName)
    {
        string para = "";
      
        string ret = "";
        string conStr = "workstation id=" + ServerName + ";packet size=4096;user id=" + DBUsername + ";password=" + Pass + ";data source=" + ServerName + ";persist security info=False;initial catalog=" + db;

        try
        {
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection())
            {
                using (System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand())
                {
                    conn.ConnectionString = conStr;
                    conn.Open();
                    comm.Connection = conn;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = SPName;
                    if (sqlParams != null)
                    {
                        foreach (SqlParameter p in sqlParams)
                        {
                            para += p.ToString();
                            comm.Parameters.Add(p);
                        }
                    }
                    comm.CommandTimeout = 6500;
                    ret = comm.ExecuteScalar().ToString();
                }

            }
        }
        catch(SqlException sqlex)
        {
            logthefile("SingleIntSPSql QRY " + SPName + " Parameters " + para + " SQL Error " + sqlex.ToString());
        }


        return ret;
    }

    public string SingleIntQrySQL(SqlParameter[] sqlParams, string QRY)
    {
        string para = "";
       
        string ret = "";
        string conStr = "workstation id=" + ServerName + ";packet size=4096;user id=" + DBUsername + ";password=" + Pass + ";data source=" + ServerName + ";persist security info=False;initial catalog=" + db;
        
        try
        {
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection())
            {
                using (System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand())
                {
                    conn.ConnectionString = conStr;
                    conn.Open();
                    comm.Connection = conn;
                    comm.CommandText = QRY;
                    if (sqlParams != null)
                    {
                        foreach (SqlParameter p in sqlParams)
                        {
                            para += p.ToString();
                            comm.Parameters.Add(p);
                        }
                    }
                    comm.CommandTimeout = 6500;
                    ret = comm.ExecuteScalar().ToString();
                }

            }
        }
        catch(SqlException sqlex)
        {
            logthefile("Single IntQrySql Error: QRY: " + QRY + " Paramaters : " + para + " SQL Error " + sqlex.ToString());
        }


        return ret;
    }

    public SqlDataReader SQLRDSQL(string paramname, string paramvalue, string query)
    {

        string conStr = "workstation id=" + ServerName + ";packet size=4096;user id=" + DBUsername + ";password=" + Pass + ";data source=" + ServerName + ";persist security info=False;initial catalog=" + db;
        SqlDataReader sdr;
        using (SqlConnection conn = new SqlConnection(conStr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = query;
                cmd.Parameters.AddWithValue(paramname, paramvalue);
                cmd.Connection = conn;
                conn.Open();
                sdr = cmd.ExecuteReader();
                conn.Close();
            }
        }
        return sdr;
    }

    public SqlDataReader SQLReader(string query)
    {

        string conStr = "workstation id=" + ServerName + ";packet size=4096;user id=" + DBUsername + ";password=" + Pass + ";data source=" + ServerName + ";persist security info=False;initial catalog=" + db;
        SqlDataReader sdr;

        using (SqlConnection conn = new SqlConnection(conStr))
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = query;
                cmd.Connection = conn;
                conn.Open();
                sdr = cmd.ExecuteReader();
                conn.Close();
            }
        }

        return sdr;
    }

    public DataTable DTSPSQLNonPara(SqlParameter[] sqlParams, string SPName)
    {
        string para = "";

        string conStr = "workstation id=" + ServerName + ";packet size=4096;user id=" + DBUsername + ";password=" + Pass + ";data source=" + ServerName + ";persist security info=False;initial catalog=" + db;
        DataTable ret = new DataTable();
        try
        {
            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection())
            {
                using (System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand())
                {
                    conn.ConnectionString = conStr;
                    conn.Open();
                    comm.Connection = conn;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = SPName;
                    if (sqlParams != null)
                    {
                        foreach (SqlParameter p in sqlParams)
                        {
                            para += p.ToString();
                            comm.Parameters.Add(p);
                        }
                    }
                    comm.CommandTimeout = 6500;
                    SqlDataReader dr = comm.ExecuteReader();
                    ret.Load(dr);
                }

            }
        }
        catch (SqlException sqlex)
        {
            logthefile("DTSPSQLNonPara QRY " + SPName + " Paramenters " + para + " SQL Error " + sqlex.ToString());
        }
      

        return ret;
    }

    public int SQLInsScope(string query)
    {
       
        query += " SELECT SCOPE_IDENTITY();";
        int ret = 0;
        string conStr = "workstation id=" + ServerName + ";packet size=4096;user id=" + DBUsername + ";password=" + Pass + ";data source=" + ServerName + ";persist security info=False;initial catalog=" + db;
        SqlConnection connection = new SqlConnection(conStr);
        try
        {
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Connection.Open();
                ret = int.Parse(command.ExecuteScalar().ToString());
                command.Connection.Close();
            }

        }
        catch (SqlException sqlex)
        {
            logthefile("SQLInsScope Qry " + query + " Error " + sqlex.ToString());
        }
       
        return ret;
    }

    public List<string> SQLPopulateList(string prefix, string SPName,string listvaluefirst, string listvaluesecond)
    {

        logthefile("Here");
        List<string> List = new List<string>();

        string conStr = "workstation id=" + ServerName + ";packet size=4096;user id=" + DBUsername + ";password=" + Pass + ";data source=" + ServerName + ";persist security info=False;initial catalog=" + db;
        try
        {
                        

            using (System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(conStr))
            {
                using (System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand())
                {
                   
                    comm.CommandText = SPName;
                    comm.Parameters.AddWithValue("@SearchText", prefix);
                    comm.Connection = conn;
                    comm.Connection.Open();
                    using (SqlDataReader sdr = comm.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            List.Add(string.Format("{0}-{1}", sdr[listvaluefirst], sdr[listvaluesecond]));

                        }
                    }
                }

            }

        }
        catch (SqlException sqlex)
        {
            logthefile(sqlex.ToString());
        }
        catch (Exception ex)
        {
            logthefile(ex.ToString());
        }
        
        return List;
    }

   
    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "SQLError.txt");
        #region Local
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, true))
        {
            writer.WriteLine(msg);
        }
        #endregion
    }

   
}








































///  //   string sql = "Insert into Stats_Form (intid,Surname,Name)values('" + init.ToString() + "','" + txtSurname.Value + "','" + txtName.Value + "')";