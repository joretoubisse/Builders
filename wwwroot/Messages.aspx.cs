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
        Loadfooter.Text = Session["Footer"].ToString();
        lblName.InnerText = Session["FName"].ToString();

        CompanyID.Value = Session["ChurchID"].ToString();
        PopulateList();
        RunMenus();
    }


    void RunMenus()
    {
        string MenuName = "";
        string Pageurl = "";

        string html = "";


         MenuDatatble MenuT = new MenuDatatble();
        DataTable tMenu = MenuT.ReturnMenuT();
        if (tMenu.Rows.Count > 0)
        {

            html = @"<div class='kt-aside__head'>
				<h3 class='kt-aside__title'>
				" + Session["ChurchName"].ToString() + "";
            html += @"</h3>
				<a href='#' class='kt-aside__close' id='kt_aside_close'><i class='flaticon2-delete'></i></a>
			</div>
			<div class='kt-aside__body'>

			
				<div class='kt-aside-menu-wrapper' id='kt_aside_menu_wrapper'>
					<div id='kt_aside_menu' class='kt-aside-menu ' data-ktmenu-vertical='1' data-ktmenu-scroll='1'>
						<ul class='kt-menu__nav '>";


            foreach (DataRow rows in tMenu.Rows)
            {

                MenuName = rows[0].ToString();
                Pageurl = rows[1].ToString();


                if (MenuName == "Notifications")
                {
                    PageTitle.InnerText = MenuName;
                    html += @"<li class='kt-menu__item ' aria-haspopup='true'><a href='" + Pageurl + "' class='kt-menu__link '><span class='kt-menu__link-text'>" + MenuName + "</span></a></li>";
                }
                else
                {
                    html += @"<li class='kt-menu__item  kt-menu__item--active' aria-haspopup='true'><a href='" + Pageurl + "' class='kt-menu__link '><span class='kt-menu__link-text'>" + MenuName + "</span></a></li>";
                }



            }

            html += @"</ul>
					</div>
				</div>
		</div>";
        }
        MenuStream.Text = html;
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



    void SendSms(string CellNo, string Msg)
    {
        // This URL is used for sending messages
        string myURI = "https://api.bulksms.com/v1/messages";

        // change these values to match your own account
        string myUsername = connect.SingleRespSQL("SELECT smsusername FROM SmsCredentials WHERE churchid = '" + Session["ChurchID"].ToString() + "'");
        string myPassword = connect.SingleRespSQL("SELECT smspassword FROM SmsCredentials WHERE churchid = '" + Session["ChurchID"].ToString() + "'");


        string first_xter = CellNo.Substring(0, 1);
        if (first_xter == "0")
        {
            CellNo = "27" + CellNo.Remove(0, 1);

        }

        // the details of the message we want to send
        string myData = "{to: \"" + CellNo + "\", body:\"" + Msg + "\"}";

        // build the request based on the supplied settings
        var request = WebRequest.Create(myURI);

        // supply the credentials
        request.Credentials = new NetworkCredential(myUsername, myPassword);
        request.PreAuthenticate = true;
        // we want to use HTTP POST
        request.Method = "POST";
        // for this API, the type must always be JSON
        request.ContentType = "application/json";

        // Here we use Unicode encoding, but ASCIIEncoding would also work
        var encoding = new UnicodeEncoding();
        var encodedData = encoding.GetBytes(myData);

        // Write the data to the request stream
        var stream = request.GetRequestStream();
        stream.Write(encodedData, 0, encodedData.Length);
        stream.Close();

        // try ... catch to handle errors nicely
        try
        {
            // make the call to the API
            var response = request.GetResponse();

        }
        catch (WebException ex)
        {
            // show the general message
            logthefile("An error occurred:" + ex.Message);


            // print the detail that comes with the error
            //var reader = new StreamReader(ex.Response.GetResponseStream());
            //logthefile("Error details:" + reader.ReadToEnd());
        }
        catch (Exception ex)
        {
            logthefile("An error occurred:" + ex.Message);
        }



    }


    void SendMessage(string MessageID)
    {


        string query = "";

        #region SelectCategory
        string Category = CmdChangeSelection.SelectedItem.Text;
        int NewVisitors = 0;
        if (Category == "All Members")
        {
            query = "SELECT intid, Name + ' ' + Surname,Celno,sms FROM Stats_Form WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' and IsActive = '1' and MemberType = 'Member'";
        }
        else if (Category == "Active tithers")
        {
            query = "SELECT intid, Name + ' ' + Surname,Celno,sms FROM Stats_Form WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' and IsActive = '1' and MemberType = 'Member' and Tithe = 'Yes'";
        }
        else if (Category == "Non Active tithers")
        {
            query = "SELECT intid, Name + ' ' + Surname,Celno,sms FROM Stats_Form WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' and IsActive = '1' and MemberType = 'Member' and Tithe = 'No'";
        }
        else if (Category == "All Visitors")
        {
            query = "SELECT intid, Name + ' ' + Surname,Celno,sms FROM Stats_Form WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' and IsActive = '1' and MemberType = 'Visitor'";
        }
        else if (Category == "Individual")
        {
            query = "SELECT intid, Name + ' ' + Surname,Celno,sms FROM Stats_Form WHERE intid = '" + CmdIndMember.Value + "'";
        }
        else if (Category == "New Visitors")
        {
            NewVisitors = 1;
            query = "SELECT intid, Name + ' ' + Surname,Celno,sms FROM Stats_Form WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' and IsActive = '1' and MemberType = 'Visitor'";
        }
        else
        {
            query = "SELECT A.intid, A.Name + ' ' + A.Surname,A.Celno,A.sms FROM Stats_Form A INNER JOIN MsgGroups B ON A.intid = B.MemberID  WHERE A.Campus = '" + Session["Campus"].ToString() + "' and A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.IsActive = '1' and B.groupid = '" + CmdChangeSelection.SelectedValue.ToString() + "'";
        }

        #endregion

        int insertHolder = 0;
        DataTable dt = new DataTable();
        logthefile(query);
        dt = connect.DTSQL(query);
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow rows in dt.Rows)
            {


                string IDNumber = "";
                IDNumber = rows[0].ToString();

                if (NewVisitors > 0)
                {
                    int IsSent = 0;
                    IsSent = int.Parse(connect.SingleRespSQL("SELECT COUNT(intid)  FROM Messages  WHERE IDnumber = '" + IDNumber + "'"));
                    if (IsSent == 0)
                    {
                        #region Send Message only if you are a new Visitor
                        if (rows[3].ToString() == "Yes")
                        {
                            SendSms(rows[2].ToString(), txtMessage.Value.TrimStart().TrimEnd());
                            string qryFinal = "INSERT INTO Messages (IDnumber,ChurchID,ActionName,ActionDate,IsRead,MessageType,Category,Messagesubject,MessageID,Message) VALUES (@IDnumber,@ChurchID,@ActionName,getdate(),@IsRead,@MessageType,@Category,@Messagesubject,@MessageID,@Message) SELECT SCOPE_IDENTITY();";
                            SqlParameter[] Nsp =
                                               {
                                                new SqlParameter("@ChurchID",Session["ChurchID"].ToString()),
                                                new SqlParameter("@ActionName", Session["FullName"].ToString()),
                                                new SqlParameter("@MessageType", "SMS"),
                                                new SqlParameter("@IDnumber",IDNumber),
                                                new SqlParameter("@IsRead","0"),

                                                new SqlParameter("@Category", CmdChangeSelection.SelectedItem.Text),
                                                new SqlParameter("@MessageSubject", txtSubject.Value),
                                                new SqlParameter("@Message", txtMessage.Value.TrimStart()),
                                                new SqlParameter("@MessageID", MessageID),


                                               };
                            insertHolder = int.Parse(connect.SingleIntQrySQL(Nsp, qryFinal));
                        }
                        #endregion
                    }
                }
                else
                {
                    #region Send to Everyone as per normal
                    if (rows[3].ToString() == "Yes")
                    {
                        SendSms(rows[2].ToString(), txtMessage.Value.TrimStart().TrimEnd());
                        string qryFinal = "INSERT INTO Messages (IDnumber,ChurchID,ActionName,ActionDate,IsRead,MessageType,Category,Messagesubject,MessageID,Message) VALUES (@IDnumber,@ChurchID,@ActionName,getdate(),@IsRead,@MessageType,@Category,@Messagesubject,@MessageID,@Message) SELECT SCOPE_IDENTITY();";
                        SqlParameter[] Nsp =
                                               {
                                                new SqlParameter("@ChurchID",Session["ChurchID"].ToString()),
                                                new SqlParameter("@ActionName", Session["FullName"].ToString()),
                                                new SqlParameter("@MessageType", "SMS"),
                                                new SqlParameter("@IDnumber",IDNumber),
                                                new SqlParameter("@IsRead","0"),

                                                new SqlParameter("@Category", CmdChangeSelection.SelectedItem.Text),
                                                new SqlParameter("@MessageSubject", txtSubject.Value),
                                                new SqlParameter("@Message", txtMessage.Value.TrimStart()),
                                                new SqlParameter("@MessageID", MessageID),


                                               };
                        insertHolder = int.Parse(connect.SingleIntQrySQL(Nsp, qryFinal));
                    }
                    #endregion

                }



            }
            DivIndividual.Visible = false;
            ShowAll.Visible = false;
        }
        else
        {
            connect.SingleIntSQL("DELETE FROM MessagesHolder WHERE intid = '" + MessageID + "'");
            NotCompleteNotie();
        }
    }



    void PopulateList()
    {

        #region Prebuild Templates
        DataTable PreTemptable = new DataTable();
        PreTemptable.Columns.Add("intid", typeof(string));
        PreTemptable.Columns.Add("groupname", typeof(string));

        PreTemptable.Rows.Add("12000", "All Members");
        PreTemptable.Rows.Add("120002", "All Visitors");
        PreTemptable.Rows.Add("120001", "New Visitors");
        PreTemptable.Rows.Add("120005", "Individual");
        PreTemptable.Rows.Add("120006", "Active tithers");
        PreTemptable.Rows.Add("120007", "Non Active tithers");
        #endregion






        DataTable table = new DataTable();
        table = connect.DTSQL("SELECT CONVERT(varchar(16),intid), groupname FROM MsgGroupHolder WHERE churchid = '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + "' ORDER BY groupname ASC");


        PreTemptable.Merge(table);

        CmdChangeSelection.DataSource = PreTemptable;
        CmdChangeSelection.DataTextField = "groupname";
        CmdChangeSelection.DataValueField = "intid";
        CmdChangeSelection.DataBind();
        CmdChangeSelection.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select", "None"));
        PopulateTemplate();
    }

    void populateINMember()
    {


        DataTable table = connect.DTSQL("SELECT intid, Name + ' ' + Surname + ' - ' + Celno as [Name]  FROM  Stats_Form WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + "' and IsActive = '1' order by name asc");
        CmdIndMember.DataSource = table;
        CmdIndMember.DataTextField = "Name";
        CmdIndMember.DataValueField = "intid";
        CmdIndMember.DataBind();

       // CmdIndMember.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select", "None"));


    }



    void PopulateTemplate()
    {

        DataTable table = new DataTable();
        table = connect.DTSQL("SELECT intid,Messagebody FROM MsgTemplates WHERE churchid = '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + "'");

        CmdTemplates.DataSource = table;
        CmdTemplates.DataTextField = "Messagebody";
        CmdTemplates.DataValueField = "intid";

        CmdTemplates.DataBind();

        CmdTemplates.Items.Insert(0, new System.Web.UI.WebControls.ListItem("None", "None"));
       

    }

    protected void btnCancel_ServerClick(object sender, EventArgs e)
    {

        Server.Transfer("Notification.aspx");
    }

    protected void CmdChangeSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        DivIndividual.Visible = false;
        if (CmdChangeSelection.SelectedValue.ToString() == "None")
        {
            SingleDiv.Visible = false;
            ShowAll.Visible = false;
        }
        else if (CmdChangeSelection.SelectedItem.Text == "All Members")
        {
            SingleDiv.Visible = false;
            ShowAll.Visible = true;
        }
        else if (CmdChangeSelection.SelectedItem.Text == "Active tithers")
        {
            SingleDiv.Visible = false;
            ShowAll.Visible = true;
        }
        else if (CmdChangeSelection.SelectedItem.Text == "Non Active tithers")
        {
            SingleDiv.Visible = false;
            ShowAll.Visible = true;
        }
        else if (CmdChangeSelection.SelectedItem.Text == "All Visitors")
        {
            SingleDiv.Visible = false;
            ShowAll.Visible = true;
        }
        else if (CmdChangeSelection.SelectedItem.Text == "New Visitors")
        {
            SingleDiv.Visible = false;
            ShowAll.Visible = true;
        }
        else if (CmdChangeSelection.SelectedItem.Text == "Individual")
        {
            populateINMember();
            DivIndividual.Visible = true;
            SingleDiv.Visible = false;
            ShowAll.Visible = true;
        }
        else
        {
            SingleDiv.Visible = false;
            ShowAll.Visible = true;
        }
    }
    protected void CmdTemplates_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmdTemplates.SelectedValue.ToString() == "None")
        {
            txtSubject.Value = "";
            txtMessage.Value = "";
        }
        else
        {
            txtSubject.Value = "Template";
            txtMessage.Value = CmdTemplates.SelectedItem.Text;
        }

    }
    protected void BtnSendMessage_ServerClick(object sender, EventArgs e)
    {
        if (txtSubject.Value == "" || txtMessage.Value == "" || CmdChangeSelection.SelectedValue.ToString() == "None")
        {
            DivButtonSend.Visible = true;
            DivShowLoader.Visible = false;
            NotCompleteNotie(); return;
        }


        int insertHolder = 0;
        string qry = "INSERT INTO MessagesHolder (churchid,actionName,actiondate,MessageType,Category,MessageSubject,Message,Campus) VALUES (@churchid,@actionName,getdate(),@MessageType,@Category,@MessageSubject,@Message,@Campus) SELECT SCOPE_IDENTITY();";
        SqlParameter[] sp =
               {
                        new SqlParameter("@churchid",Session["ChurchID"].ToString()),
                        new SqlParameter("@actionName", Session["FullName"].ToString()),
                        new SqlParameter("@MessageType", "SMS"),
                        new SqlParameter("@Category", CmdChangeSelection.SelectedItem.Text),
                        new SqlParameter("@MessageSubject", txtSubject.Value),
                        new SqlParameter("@Message", txtMessage.Value.TrimStart()),
                        new SqlParameter("@Campus", Session["Campus"].ToString()),


                    };

        insertHolder = int.Parse(connect.SingleIntQrySQL(sp, qry));
        SendMessage(insertHolder.ToString());

      
        txtMessage.Value = "";
        txtSubject.Value = "";
        PopulateList();
        SentNotie();
    }
}