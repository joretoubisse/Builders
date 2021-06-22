<%@ WebHandler Language="C#" Class="ViewTithe" %>

using System;
using System.Web;
using System.IO;
using System.Data;

public class ViewTithe : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{

    SqlConnMethod connect = new SqlConnMethod();
    public void ProcessRequest(HttpContext context)
    {


        string Idnumber = context.Request.QueryString["IDNumber"].ToString();
        string CompanyID = context.Session["ChurchID"].ToString();
        string htmlString = "";

        htmlString = populateMessages(Idnumber, CompanyID);
        context.Response.Write(htmlString);
    }




    public string populateMessages(string Idnumber, string CompanyID)
    {
        string htmltext = "";
        string Getotal = "";
        DataTable table = new DataTable();
        string sql = "SELECT  Amount,convert(varchar(16),month,106) FROM tithe WHERE churchid = '" + CompanyID + "' and idnumber = '" + Idnumber + "' ORDER By month DESC";
        table = connect.DTSQL(sql);
      
        if (table.Rows.Count > 0)
        {
            Getotal = connect.SingleRespSQL("SELECT SUM(Amount) FROM tithe WHERE churchid = '" + CompanyID + "' and idnumber = '" + Idnumber + "'");
               htmltext = "<div class='form-group' id='Div2'  >" +
                                                    "<label>Total Amount</label>" +
												"<label class='form-control'>" + "R " +  Getotal + "</label>" +
												"</div>";
            
            
            
            
            
            
            #region Table Headers
            htmltext += "<table class='table table-striped- table-bordered' id='tbtithe' > " +
                  "<thead>" +
                  "  <tr>" +
      
                    "    <th>Amount</th> " +
                    "    <th>Date</th> " +
                 
                  "  </tr> " +
                  "</thead> " +
                  "<tbody> ";
            #endregion

            #region SQL Stuff
            foreach (DataRow dtrow in table.Rows)
            {

                htmltext += " <tr> " +

                              
                                   "   <td >" + "R " + dtrow[0].ToString() + "</td> " +
                                   "   <td >" + dtrow[1].ToString() + "</td> " +

                                  
                           " </tr>";
            }

            #endregion

            htmltext += "</tbody> " +
                   " </table>";
 
        }
        else
        {
           htmltext = "No Tithe";
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