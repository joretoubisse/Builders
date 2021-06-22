using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Messages : System.Web.UI.Page
{
    #region General Classes
    SqlConnMethod connect = new SqlConnMethod();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            RunOnLoad();
        }
    }


    void RunOnLoad()
    {
        lblName.InnerText = Session["FName"].ToString();
        PopulateMembers();
        CompanyID.Value = Session["ChurchID"].ToString();
        PopulateSingle();
    }

    void PopulateMembers()
    {

        string htmltext = "";
        DataTable table = new DataTable();
        string Getqry = "SELECT intid,Category,MessageSubject,CONVERT(varchar(16),actiondate,106)FROM MessagesHolder WHERE churchid = '" + Session["ChurchID"].ToString() + "' ORDER by actiondate DESC";
        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                    "    <th>  </th> " +
                    "    <th> Subject</th> " +
                    "    <th> Message Category </th> " +

                     "    <th > Sent</th> " +
                
                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {




                htmltext += " <tr> " +

                                 "<td ><center><a onclick='BtnReadMessage(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Read </a>	</center></td>" +

                             "   <td >" + Row[2].ToString() + "</td> " +
                             "   <td >" + Row[1].ToString() + "</td> " +

                              "   <td >" + Row[3].ToString() + "</td> " +
                         

                        " </tr>";


            }
        }
        else
        {
            htmltext = "No Members";
        }


        htmltext += "    </tbody> " +
                   " </table>";
        tbTable.Text = htmltext;
    }

    void PopulateSingle()
    {



        string extraHTML = "";
        #region
        DataTable det = new DataTable();
        det = connect.DTSQL("SELECT  Idnumber, Name + ' ' + Surname FROM Stats_Form  WHERE ChurchID = '" + Session["ChurchID"].ToString() + "'");
        // det = connect.DTSQL("SELECT  Idnumber, Name + ' ' + Surname FROM Stats_Form  WHERE ChurchID = '" + HttpContext.Current.Session["CompanyID"].ToString() + "'");
        #endregion

        extraHTML = "<select class='form-control kt_selectpicker' data-live-search='true' runat='server' onchange='IndividualSelection(this)' id='SingleInd'>" +
                     "<option value='0'>Please Select</option> ";

        if (det.Rows.Count > 0)
        {
            foreach (DataRow drt in det.Rows)
            {
                extraHTML += "<option value='" + drt[0].ToString() + "'>" + drt[1].ToString() + "</option>";
            }
        }

        extraHTML += "</select>";










        txtHtml.Text = extraHTML;
    }
    

    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "login.txt");
        #region Local
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, true))
        {
            writer.WriteLine(msg);
        }
        #endregion
    }



    #region Message Boxes
    void SentNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ SendNotieAlert(); },550);", true);

    }

    void SaveNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ SaveNotieAlert(); },550);", true);

    }

    void NotCompleteNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ CompleteNotieAlert(); },550);", true);

    }
    #endregion


    void SendMessage(string MessageID)
    {
    

        string query = "";

        #region SelectCategory
        string Category = CmdChangeSelection.Value.ToString();

        if (Category == "Every One")
        {
            query = "SELECT DISTINCT Idnumber, Name + ' ' + Surname FROM Stats_Form  WHERE ChurchID = '1'";
        }
        else if (Category == "Ward Cell")
        {

        }
        else if (Category == "Individual")
        {
            query = "SELECT  Idnumber, Name + ' ' + Surname FROM Stats_Form  WHERE ChurchID = '1' and Idnumber = '" + lblIDnumber.Value + "'";
        }
        else if (Category == "All Females")
        {

        }
        else if (Category == "All Males")
        {

        }
        else if (Category == "Ministries")
        {

        }
        #endregion

        int insertHolder = 0;
        DataTable dt = new DataTable();
        dt = connect.DTSQL(query);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow rows in dt.Rows)
            {


                string IDNumber = "";
                IDNumber = rows[0].ToString();
              
                string qryFinal = "INSERT INTO Messages (IDnumber,ChurchID,ActionName,ActionDate,IsRead,MessageType,Category,Messagesubject,MessageID,Message) VALUES (@IDnumber,@ChurchID,@ActionName,getdate(),@IsRead,@MessageType,@Category,@Messagesubject,@MessageID,@Message) SELECT SCOPE_IDENTITY();";
                SqlParameter[] Nsp =
               {
                        new SqlParameter("@ChurchID",Session["ChurchID"].ToString()),
                        new SqlParameter("@ActionName", "Jabu"),
                             new SqlParameter("@MessageType", "Both"),
                        new SqlParameter("@IDnumber",IDNumber),
                           new SqlParameter("@IsRead","0"),

                        new SqlParameter("@Category", CmdChangeSelection.Value.ToString()),
                        new SqlParameter("@MessageSubject", txtSubject.Value),
                        new SqlParameter("@Message", txtMessage.Value.TrimStart()),
                       new SqlParameter("@MessageID", MessageID),


                    };
                insertHolder = int.Parse(connect.SingleIntQrySQL(Nsp, qryFinal));
            }
        }
    }

    protected void BtnSendMessage_ServerClick(object sender, EventArgs e)
    {

        if (txtSubject.Value == "" || txtMessage.Value == "")
        {
            DivMessage.Style.Add("display", "block");
            DivSubject.Style.Add("display", "block");
            DivButtonSend.Style.Add("display", "block");
            SendMessageDiv.Style.Add("display", "block");
            ShowMessageDiv.Style.Add("display", "none");
        
            NotCompleteNotie(); return;
        }

        int insertHolder = 0;
        string qry = "INSERT INTO MessagesHolder (churchid,actionName,actiondate,MessageType,Category,MessageSubject,Message) VALUES (@churchid,@actionName,getdate(),@MessageType,@Category,@MessageSubject,@Message) SELECT SCOPE_IDENTITY();";
        SqlParameter[] sp =
               {
                        new SqlParameter("@churchid",Session["ChurchID"].ToString()),
                        new SqlParameter("@actionName", Session["FullName"].ToString()),
                        new SqlParameter("@MessageType", "Both"),
                        new SqlParameter("@Category", CmdChangeSelection.Value.ToString()),
                        new SqlParameter("@MessageSubject", txtSubject.Value),
                        new SqlParameter("@Message", txtMessage.Value.TrimStart()),
                     


                    };

        insertHolder = int.Parse(connect.SingleIntQrySQL(sp, qry));
        SendMessage(insertHolder.ToString());


        SendMessageDiv.Style.Add("display", "none");
        ShowMessageDiv.Style.Add("display", "block");
        txtMessage.Value = "";
        txtSubject.Value = "";
     
        SentNotie();
    

    }

    protected void btnCancel_ServerClick(object sender, EventArgs e)
    {

        DivViewMsg.Style.Add("display", "none");
        SendMessageDiv.Style.Add("display", "none");
        ShowMessageDiv.Style.Add("display", "block");
        txtMessage.Value = "";
        txtSubject.Value = "";
    }
    protected void btnReadMessage_ServerClick(object sender, EventArgs e)
    {
        DivViewMsg.Style.Add("display", "block");
        SendMessageDiv.Style.Add("display", "none");
        ShowMessageDiv.Style.Add("display", "none");


        DataTable tb = new DataTable();
        tb = connect.DTSQL("SELECT MessageSubject,Message FROM MessagesHolder WHERE  intid = '" + MessageID.Value + "'");
        if (tb.Rows.Count > 0)
        {
            foreach (DataRow rows in tb.Rows)
            {
                ActualSub.Value = rows[0].ToString();
                txtShowMessage.Value = rows[1].ToString();
            }
        }
    }
}