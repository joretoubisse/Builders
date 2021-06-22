using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

public class TrackJourney
{
    SqlConnMethod connect = new SqlConnMethod();

    public void TrackIT(string page)
    {        
        string sql = "INSERT Into CPComUserJourney(loginname, actiondate, pagevisited, identifier) VALUES('" + HttpContext.Current.Session["loginname"].ToString() + "', GetDate(), '" + page + "', '" + HttpContext.Current.Session["identifier"].ToString() + "')";
        
        connect.SingleIntSQL(sql);
    }

    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "TrackJourney.txt");
        #region Local
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, true))
        {
            writer.WriteLine(msg);
        }
        #endregion
    }
}