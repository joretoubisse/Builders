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

public partial class ConnectGroups : System.Web.UI.Page
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

    void PopulateZone()
    {
        DataTable table = connect.DTSQL("SELECT  Zone,Zone FROM ChurchZone WHERE churchid = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' ORDER by isordering  ASC");
        CmdZone.DataSource = table;
        CmdZone.DataTextField = "Zone";
        CmdZone.DataValueField = "Zone";
        CmdZone.DataBind();
        CmdZone.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select", "None"));
    }


    void ShowTheCampus(string Pageurl)
    {
        string html = "";
        if (Session["ShowAll"].ToString() == "Yes")
        {
            kt_offcanvas_toolbar_profile.Visible = true;
        }

        DataTable Campus = new DataTable();
        Campus = connect.DTSQL("SELECT Campus FROM Campus WHERE ChurchID = '" + Session["ChurchID"].ToString() + "'");
        if (Campus.Rows.Count > 0)
        {

            string GetPageName = Pageurl;
            foreach (DataRow rows in Campus.Rows)
            {

                html += @"<div class='kt-widget-1__item'>
							<div class='kt-widget-1__item-info'>
								<a href='CampusChange.ashx?GetCampus=" + rows[0].ToString() + "&PageName=" + GetPageName + "'>" +
                                    @" <div class='kt-widget-1__item-title'>" + rows[0].ToString() + "</div>" +
                                @"</a>
							</div>
						
						</div>";
            }
        }
        ShowAllCampus.Text = html;

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


                if (MenuName == "Connect Groups")
                {
                    ShowTheCampus(Pageurl);
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

    void RunOnLoad()
    {
        lblName.InnerText = Session["FName"].ToString();
        Loadfooter.Text = Session["Footer"].ToString();
        if (ConfigurationManager.AppSettings["HideFields"].ToString() == "1")
        {
            PopulateZone();
            if (Session["ShowAll"].ToString() == "Yes")
            {
                IsAdmin.Value = "1";
                DivAddMember.Visible = true;
            }
            else
            {
                if (Session["ConnectRight"].ToString() == "1")
                {
                    IsAdmin.Value = "1";
                    DivAddMember.Visible = true;
                }
            }


           

            RunMembers("none");
        }
        else
        {
            IsAdmin.Value = "1";
            RunMembers("none");
            DivAddMember.Visible = false;
        }


        RunMenus();
       
    }

   


    void RunMembers(string value)
    {

        string htmltext = "";
        string Getqry = "";
        DataTable table = new DataTable();
        if (value == "none" || value == "")
        {
            lblZone.InnerText = "Zone 1";
            Getqry = "SELECT A.intid, A.GroupName,B.Name + ' ' + B.Surname,A.iconnectDay,A.iconnectTime    FROM iConnect A INNER JOIN Stats_Form B ON A.leaderUserID = B.intid WHERE A.isActive = '1' and A.campus = '" + Session["Campus"].ToString() + "' and A.churchID = '" + Session["ChurchID"].ToString() + "' and A.Zone  = 'Zone 1' ORDER BY  A.groupName ASC";
            //if (Session["ShowAll"].ToString() == "Yes")
            //{
            //    Getqry = "SELECT A.intid, A.GroupName,B.Name + ' ' + B.Surname,A.iconnectDay,A.iconnectTime   FROM iConnect A INNER JOIN Stats_Form B ON A.leaderUserID = B.intid WHERE A.isActive = '1'  and A.churchID = '" + Session["ChurchID"].ToString() + "' and A.Zone  = 'Zone 1' ORDER BY  A.groupName ASC";
            //}
            //else
            //{
            //    Getqry = "SELECT A.intid, A.GroupName,B.Name + ' ' + B.Surname,A.iconnectDay,A.iconnectTime    FROM iConnect A INNER JOIN Stats_Form B ON A.leaderUserID = B.intid WHERE A.isActive = '1' and A.campus = '" + Session["Campus"].ToString() + "' and A.churchID = '" + Session["ChurchID"].ToString() + "' and A.Zone  = 'Zone 1' ORDER BY  A.groupName ASC";
            //}
           
        }
        else
        {
            lblZone.InnerText = value;
            Getqry = "SELECT A.intid, A.GroupName,B.Name + ' ' + B.Surname,A.iconnectDay,A.iconnectTime    FROM iConnect A INNER JOIN Stats_Form B ON A.leaderUserID = B.intid WHERE A.isActive = '1' and A.campus = '" + Session["Campus"].ToString() + "' and A.churchID = '" + Session["ChurchID"].ToString() + "' and A.Zone = '" + value + "' ORDER BY  A.groupName ASC";
            //if (Session["ShowAll"].ToString() == "Yes")
            //{
            //    Getqry = "SELECT A.intid, A.GroupName,B.Name + ' ' + B.Surname,A.iconnectDay,A.iconnectTime    FROM iConnect A INNER JOIN Stats_Form B ON A.leaderUserID = B.intid WHERE A.isActive = '1'  and A.churchID = '" + Session["ChurchID"].ToString() + "' and A.Zone = '" + value + "' ORDER BY  A.groupName ASC";
            //}
            //else
            //{
            //    Getqry = "SELECT A.intid, A.GroupName,B.Name + ' ' + B.Surname,A.iconnectDay,A.iconnectTime    FROM iConnect A INNER JOIN Stats_Form B ON A.leaderUserID = B.intid WHERE A.isActive = '1' and A.campus = '" + Session["Campus"].ToString() + "' and A.churchID = '" + Session["ChurchID"].ToString() + "' and A.Zone = '" + value + "' ORDER BY  A.groupName ASC";
            //}

           
        }
       // logthefile(Getqry);
        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                    "    <th>  </th> " +
                    "    <th> Group Name</th> " +
                    "    <th> Group Leader </th> " +
                      "    <th> Day </th> " +
                        "    <th> Time </th> " +
                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {




                htmltext += " <tr> " +

                                 "<td ><center><a onclick='BtnViewMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> View </a> &nbsp;	&nbsp;	&nbsp;<a onclick='BtnRemoveConnect(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Remove </a></center></td>" +

                             "   <td >" + Row[1].ToString() + "</td> " +
                             "   <td >" + Row[2].ToString() + "</td> " +
                              "   <td >" + Row[3].ToString() + "</td> " +
                               "   <td >" + Row[4].ToString() + "</td> " +


                        " </tr>";


            }
        }
        else
        {
            lblZone.InnerText = "No Connecting Groups";
            htmltext = "";
        }


        htmltext += "    </tbody> " +
                   " </table>";
        tbTable.Text = htmltext;
    }


    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "iConnectGroup.txt");
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
    void MemberConverted()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ ConvertNotieAlert(); },550);", true);

    }
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

       
        Session["iConnectID"] = MemberID.Value;
        Server.Transfer("ViewiConnect.aspx");

    }



    protected void btnArchive_ServerClick(object sender, EventArgs e)
    {


        int complete = connect.SingleIntSQL("UPDATE  iConnect SET IsActive = '0' WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and intid = '" + MemberID.Value + "' ");
        if (complete > 0)
        {
            RunMembers(CmdZone.Value);
            RemoveNotie();
        }
    }



   
    protected void btnSearch_ServerClick(object sender, EventArgs e)
    {
        RunMembers(CmdZone.Value);
    }
}