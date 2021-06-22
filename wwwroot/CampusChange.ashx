<%@ WebHandler Language="C#" Class="CampusChange" %>

using System;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;


public class CampusChange : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{

    SqlConnMethod connect = new SqlConnMethod();

    public void ProcessRequest (HttpContext context) {

        string GetCampus = context.Request["GetCampus"].ToString();
        string PageName = context.Request["PageName"].ToString();
        context.Session["Campus"] = GetCampus;
        context.Session["FName"] = context.Session["FNames"].ToString() + "  - " + context.Session["Campus"].ToString();
      
        context.Server.Transfer(PageName);
        
        
        
    }



    public bool IsReusable {
        get {
            return false;
        }
    }

}

