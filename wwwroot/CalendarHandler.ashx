<%@ WebHandler Language="C#" Class="CalendarHandler" %>

using System;
using System.Web;
using System.Data;
using Newtonsoft.Json;
using System.Configuration;

public class CalendarHandler  : IHttpHandler,System.Web.SessionState.IReadOnlySessionState
{
    
    public void ProcessRequest (HttpContext context) {

     
       
        context.Response.ContentType = "text/plain";
        System.Data.DataTable MainResults = new System.Data.DataTable();
        System.Data.DataTable FinalTable = new System.Data.DataTable();
        SqlConnMethod Connect = new SqlConnMethod();
        string returnstring = "";

        if (HttpContext.Current.Session["ShowAll"].ToString() == "Yes")
        {
            MainResults = Connect.DTSQL("SELECT  eventName,CONVERT(nvarchar(30), EventDate, 126) ,description    FROM ChurchEvent WHERE churchid = '" + HttpContext.Current.Session["ChurchID"].ToString() + "'");
        }
        else
        {
            MainResults = Connect.DTSQL("SELECT  eventName,CONVERT(nvarchar(30), EventDate, 126) ,description    FROM ChurchEvent WHERE churchid = '" + HttpContext.Current.Session["ChurchID"].ToString() + "' and Campus = '" + HttpContext.Current.Session["Campus"].ToString() + "'");
        }
       
        FinalTable.Columns.Add("title", typeof(System.String));
        FinalTable.Columns.Add("start", typeof(System.String));
        FinalTable.Columns.Add("end", typeof(System.String));
        FinalTable.Columns.Add("description", typeof(System.String));
        FinalTable.Columns.Add("className", typeof(System.String));
             

        if (MainResults.Rows.Count > 0)
        {


            foreach (DataRow MaintbRow in MainResults.Rows)
            {
                DataRow rowadd = FinalTable.NewRow();
                rowadd["title"] = MaintbRow[0].ToString();

                rowadd["start"] = MaintbRow[1].ToString();
                rowadd["description"] = MaintbRow[2].ToString();
                rowadd["end"] = MaintbRow[1].ToString();

                rowadd["className"] = "fc-event-solid-info fc-event-light";
              
                FinalTable.Rows.Add(rowadd);
            }

            string JSONresult;
            JSONresult = Newtonsoft.Json.JsonConvert.SerializeObject(FinalTable);
            returnstring = JSONresult;

        }
        else
        {
            DataRow rowadd = FinalTable.NewRow();
            rowadd["title"] = "0";
            rowadd["start"] = "";
            rowadd["description"] = "";
            rowadd["end"] = "";
            rowadd["className"] = "fc-event-danger";

            FinalTable.Rows.Add(rowadd);

            string JSONresult;
            JSONresult = Newtonsoft.Json.JsonConvert.SerializeObject(FinalTable);
            returnstring = JSONresult;
           

        }

        context.Response.Write(returnstring);
    }

    protected void MessageBoxShow(string str)
    {
       
    } 
  

    public bool IsReusable {
        get {
            return false;
        }
    }

}