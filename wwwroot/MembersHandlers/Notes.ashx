<%@ WebHandler Language="C#" Class="Notes" %>

using System;
using System.Web;
using System.IO;
using System.Data;

public class Notes : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{

    SqlConnMethod connect = new SqlConnMethod();
    public void ProcessRequest(HttpContext context)
    {


        string Idnumber =  context.Request.QueryString["IDNumber"].ToString();
        string CompanyID = context.Session["ChurchID"].ToString();
        string htmlString = "";

        htmlString = populateNotes(Idnumber, CompanyID);
        context.Response.Write(htmlString);
    }




    public string populateNotes(string Idnumber, string CompanyID)
    {
        string htmltext = "";


        DataTable table = connect.DTSQL("SELECT  Msg,convert(varchar(17),actiondate,106),actionby FROM [VisitorsNotes] WHERE churchID = '" + CompanyID + "' and Memberno = '" + Idnumber + "'");
        if (table.Rows.Count > 0)
        {
            foreach (DataRow rows in table.Rows)
            {
                htmltext += @"<div class='kt-blog-post__thread'>
													<div class='kt-blog-post__body'>
														<div class='kt-blog-post__top'>
															<div class='kt-blog-post__author'>
																<div class='kt-blog-post__label'>
																	<b><span>" + rows[2].ToString() + ",  &nbsp;&nbsp; </span>   " + rows[1].ToString() + "</b> " + 
																@"</div>
															</div>
														
														</div>
														<div class='kt-blog-post__content'>
															<p>"  + rows[0].ToString() + " </p>" +
														@" </div>
													</div>";
            
            }
        }
        else
        {
            htmltext = "";
        }
       
       
        return htmltext;
    }


    void logthefile(string msg)
    {
        string path = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["Logsfilelocation"], "Notes.txt");
        #region Local
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, true))
        {
            writer.WriteLine(msg);
        }
        #endregion
    }



    public bool IsReusable {
        get {
            return false;
        }
    }

}