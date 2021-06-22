<%@ WebHandler Language="C#" Class="Messages" %>

using System;
using System.Web;
using System.IO;
using System.Data;

public class Messages : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{

    SqlConnMethod connect = new SqlConnMethod();
    public void ProcessRequest(HttpContext context)
    {


        string Idnumber =  context.Request.QueryString["IDNumber"].ToString();
        string CompanyID = context.Session["ChurchID"].ToString();
        string htmlString = "";

        htmlString = populateMessages(Idnumber, CompanyID);
        context.Response.Write(htmlString);
    }




    public string populateMessages(string Idnumber, string CompanyID)
    {
        string htmltext = "";
        DataTable table = new DataTable();
        string sql = "SELECT intid, MessageSubject,Category,Case when IsRead = '1' then 'Read' else 'Not Read' END as 'Message',CONVERT(varchar(16),ActionDate,106) FROM Messages WHERE ChurchID = '" + CompanyID + "' and IDnumber = '" + Idnumber + "'";
        table = connect.DTSQL(sql);

        if (table.Rows.Count > 0)
        {
            #region Table Headers
            htmltext = "<table class='table table-striped- table-bordered' id='tbMessages' > " +
                  "<thead>" +
                  "  <tr>" +
                     "    <th>  </th> " +

                    "    <th>Subject</th> " +
                    "    <th>Category</th> " +
                    "    <th>Date</th> " +
                    "    <th>Status</th> " +
                 
                  "  </tr> " +
                  "</thead> " +
                  "<tbody> ";
            #endregion

            #region SQL Stuff
            foreach (DataRow dtrow in table.Rows)
            {

                htmltext += " <tr> " +

                                  "<td ><center><a onclick='BtnReadMessage(this.id);' id='" + dtrow[0].ToString() + "' class='btn btn-secondary'> Read </a>	</center></td>" +
                                   "   <td >" + dtrow[1].ToString() + "</td> " +
                                   "   <td >" + dtrow[2].ToString() + "</td> " +
                                   "   <td >" + dtrow[4].ToString() + "</td> " +
                                         "   <td >" + dtrow[3].ToString() + "</td> " +
                                  
                           " </tr>";
            }

            #endregion

            htmltext += "</tbody> " +
                   " </table>";
 
        }
        else
        {
           htmltext = "No Messages";
        }
        return htmltext;
    }


    void logthefile(string msg)
    {
        string path = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["Logsfilelocation"], "MemberCommuncation.txt");
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