using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Events : System.Web.UI.Page
{
    #region General Classes
    SqlConnMethod connect = new SqlConnMethod();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["LoggedIn"] != null)
        {

            if (!Page.IsPostBack)
            {

                #region Startups
                RunOnLoad();
                #endregion
            }

        }
        else
        {
            Server.Transfer("logout.aspx");
        }
    }



    void RunOnLoad()
    {
       
        lblName.InnerText = Session["FName"].ToString();
        Loadfooter.Text = Session["Footer"].ToString();
        if (ConfigurationManager.AppSettings["HideFields"].ToString() == "1")
        {

            if (Session["ShowAll"].ToString() == "Yes")
            {
                IsAdmin.Value = "1";
                DivAddMember.Visible = false;
                HideButtonAdd.Visible = true;
            }
            else
            {
                if (Session["EventRights"].ToString() == "1")
                {

                    IsAdmin.Value = "1";
                    DivAddMember.Visible = false;
                    HideButtonAdd.Visible = true;
                }
            }

           

   
        }
        else
        {
            IsAdmin.Value = "1";
      
            DivAddMember.Visible = false;
        }


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


                if (MenuName == "Events")
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

    void RunEvents()
    {

        string htmltext = "";
        DataTable table = new DataTable();
        string Getqry = "";
        if (Session["ShowAll"].ToString() == "Yes")
        {
            Getqry = "SELECT intid, eventName,CONVERT(nvarchar(30), EventDate, 106) ,description    FROM ChurchEvent WHERE churchid = '" + Session["ChurchID"].ToString() + "'  ORDER by eventDate DESC";
        }
        else
        {
            Getqry = "SELECT intid, eventName,CONVERT(nvarchar(30), EventDate, 106) ,description    FROM ChurchEvent WHERE churchid = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' ORDER by eventDate DESC";
        }


       
        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                    "    <th>  </th> " +
                    "    <th> Event Name</th> " +
                    "    <th> Event Date</th> " +
                  
                 
                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {




                htmltext += " <tr> " +

                                "<td ><center><a onclick='BtnViewMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Edit </a>	&nbsp;	&nbsp;	&nbsp;<a onclick='BtnArchMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Remove </a></center></td>" +

                             "   <td >" + Row[1].ToString() + "</td> " +
                             "   <td >" + Row[2].ToString() + "</td> " +

                        " </tr>";


            }
        }
        else
        {
            htmltext = "No Events";
        }


        htmltext += "    </tbody> " +
                   " </table>";
        tbTable.Text = htmltext;
    }


    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "Members.txt");
        #region Local
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, true))
        {
            writer.WriteLine(msg);
        }
        #endregion
    }

    public static string RandomString(int length)
    {
        var chars = "0123456789";
        var stringChars = new char[length];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new String(stringChars);


        return finalString.ToString();
    }

    #region Message Boxes
    void RemoveNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ RemoveNotieAlert(); },550);", true);
       
    }

    void SaveNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ SaveNotieAlert(); },550);", true);

    }

    void NotCompleteNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ CompleteNotieAlert(); },550);", true);

    }

    void NotieInvalidEmail()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ NotieInvalidEmail(); },550);", true);

    }

    void NotieRunInvalidPass()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ NotieInvalidPass();},750);", true);

    }

    void NotieUserExist()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ ExistsNotieAlert();},750);", true);

    }

    
    
    void InvalidIDNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ InvalidIDNotie(); },550);", true);

    }

    #endregion

    bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }

   

    protected void btnViewMember_ServerClick(object sender, EventArgs e)
    {
      
        DataTable table = connect.DTSQL("SELECT eventname,CONVERT(Varchar(16),eventdate,106),description   FROM ChurchEvent WHERE  intid = '" + MemberID.Value + "'");
        if (table.Rows.Count > 0)
        {
            DivEditEvent.Visible = true;
            DivEventsAdd.Visible = false;
            foreach (DataRow rows in table.Rows)
            {
                txtEditName.Value = rows[0].ToString();
                EditDate.Value = rows[1].ToString();
                EditDes.Value = rows[2].ToString();
            }
        }
   
    }

  

    protected void btnArchive_ServerClick(object sender, EventArgs e)
    {

        int complete = connect.SingleIntSQL("DELETE FROM ChurchEvent WHERE intid = '" + MemberID.Value + "' ");
        if (complete > 0)
        {
            RunEvents();
            RemoveNotie();
        }
    }

    protected void btnCancelCampus_ServerClick(object sender, EventArgs e)
    {
        RunEvents();
        kt_content.Visible = true;
        DivEventsAdd.Visible = false;
        DivEditEvent.Visible = false;
    }
    public string RemoveSpecialChars(string input)
    {
        return Regex.Replace(input, @"[~`!@#$%^&*()+=|\\{}':;,<>/?[\]""_-]", string.Empty);
    }
    protected void AddEvent_ServerClick(object sender, EventArgs e)
    {
        RunEvents();
        kt_content.Visible = false;
        DivEventsAdd.Visible = true;
        DivEditEvent.Visible = false;
    }
    protected void btnSaveEvents_ServerClick(object sender, EventArgs e)
    {

        int complete = 0;

        if ((txtEventName.Value == "") || (EventDate.Value == "") || (txtDes.Value == ""))
        {
            NotCompleteNotie();
            return;
        }

        #region Save in SQL
        string sql = "INSERT Into ChurchEvent (eventname,eventdate,churchid,campus,description) " +
            "VALUES ('" + RemoveSpecialChars(txtEventName.Value) + "','" + EventDate.Value + "', '" + Session["ChurchID"].ToString() + "', '" + Session["Campus"].ToString() + "', '" + RemoveSpecialChars(txtDes.Value) + "')";

        complete = connect.SingleIntSQL(sql);
        if (complete > 0)
        {
            SaveNotie();

            txtEventName.Value = "";
            EventDate.Value = "";
            txtDes.Value = "";
            RunEvents();
          
        }
        #endregion


    }
    protected void btnUpdate_ServerClick(object sender, EventArgs e)
    {
        if ((txtEditName.Value == "") || (EditDes.Value == "") || (EditDate.Value == ""))
        {
            NotCompleteNotie();
            return;
        }

        int complete = connect.SingleIntSQL("UPDATE  ChurchEvent SET eventname = '" + txtEditName.Value + "',eventdate = '" + EditDate.Value + "',description = '" + EditDes.Value + "' WHERE intid = '" + MemberID.Value + "'");

        if (complete > 0)
        {
            kt_content.Visible = false;
            DivEventsAdd.Visible = true;
            DivEditEvent.Visible = false;
            RunEvents();
            SaveNotie();
        }
    }
    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        kt_content.Visible = false;
        DivEventsAdd.Visible = true;
        DivEditEvent.Visible = false;
    }
}