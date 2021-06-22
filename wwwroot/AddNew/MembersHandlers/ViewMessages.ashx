<%@ WebHandler Language="C#" Class="ViewMessages" %>

using System;
using System.Web;
using System.IO;
using System.Data;

public class ViewMessages : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{

    SqlConnMethod connect = new SqlConnMethod();
    public void ProcessRequest(HttpContext context)
    {


        string MessageID = context.Request.QueryString["MessageID"].ToString();
      
        string htmlString = "";

        htmlString = populateMessages(MessageID);
        context.Response.Write(htmlString);
    }




    public string  populateMessages(string MessageID)
    {
        string htmltext = "";
        DataTable tb = new DataTable();
        connect.SingleIntSQL("UPDATE Messages SET IsRead = '1' WHERE  intid = '" + MessageID + "'");
        tb = connect.DTSQL("SELECT MessageSubject,Message FROM Messages WHERE  intid = '" + MessageID + "'");
        if (tb.Rows.Count > 0)
        {
            foreach (DataRow rows in tb.Rows)
            {

                htmltext = @"<div class='form-group' id='Div2'  >
													<label>Subject</label>
												<label class='form-control'>" + rows[0].ToString() + "</label>" +
													
												@"</div>

                                            	<div class='form-group'  >
													<label>Message</label>
													<textarea rows='4' cols='50' class='form-control' id='txtShowMessage' readonly autocomplete='off' runat='server'>" + rows[1].ToString()  +"</textarea> </div>" +
                
                	@"<div class='form-group'><button type='reset'  id='btnCancelMessage' onclick='CommunicationRobot();'  class='btn btn-danger pull-right'>Back</button></div>";

            }
        }
        else
        {
            htmltext = "No Message";
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