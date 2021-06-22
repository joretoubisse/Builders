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

public partial class AddiConnect : System.Web.UI.Page
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

        Loadfooter.Text = Session["Footer"].ToString();
    
        lblName.InnerText = Session["FName"].ToString();
        RunMenus();
        PopulateZone();
        populateGroupLeader();
    }


    void populateGroupLeader()
    {


        //DataTable table = connect.DTSQL("SELECT intid, Name + ' ' + Surname as [Name]  FROM  Stats_Form WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and IsActive = '1' and membertype = 'Member' and Campus = '" + Session["Campus"].ToString() + "' order by name asc");
        //CmdGroupLeader.DataSource = table;
        //CmdGroupLeader.DataTextField = "Name";
        //CmdGroupLeader.DataValueField = "intid";
        //CmdGroupLeader.DataBind();
        //CmdGroupLeader.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select", "None"));


    }

    void populateGroupLeaderPergroup(string type)
    {


        DataTable table = connect.DTSQL("SELECT intid, Name + ' ' + Surname as [Name]  FROM  Stats_Form WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and IsActive = '1' and membertype = 'Member' and Campus = '" + Session["Campus"].ToString() + "' and churchgroup = '" + type + "' order by name asc");
        CmdGroupLeader.DataSource = table;
        CmdGroupLeader.DataTextField = "Name";
        CmdGroupLeader.DataValueField = "intid";
        CmdGroupLeader.DataBind();
        CmdGroupLeader.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select", "None"));


    }

    void PopulateZone()
    {
        DataTable table = connect.DTSQL("SELECT  Zone,Zone as [Name] FROM ChurchZone WHERE churchid = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' ORDER by Zone  ASC");
        CmdZone.DataSource = table;
        CmdZone.DataTextField = "Zone";
        CmdZone.DataValueField = "Zone";
        CmdZone.DataBind();
        CmdZone.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select", "None"));

        PopulateGroups();
    }


    void PopulateGroups()
    {
        DataTable table = connect.DTSQL("SELECT  groupname,groupname as [Name] FROM ChurchGroup WHERE churchid = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' ORDER by groupname  ASC");
        CmdGroup.DataSource = table;
        CmdGroup.DataTextField = "groupname";
        CmdGroup.DataValueField = "groupname";
        CmdGroup.DataBind();
        CmdGroup.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select", "none"));

    }

    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "AddVisitors.txt");
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

    void InvalidIDNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ InvalidIDNotie(); },550);", true);

    }

    
    void ExistNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ ExistNotie(); },550);", true);

    }
    #endregion

    //HideFields =0
  

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

    protected void btnCancel_ServerClick(object sender, EventArgs e)
    {
        Server.Transfer("ConnectGroups.aspx");
    }

    public string RemoveSpecialChars(string input)
    {
        return Regex.Replace(input, @"[~`!@#$%^&*()+=|\\{}':;,<>/?[\]""_-]", string.Empty);
    }



    protected void btnSave_ServerClick(object sender, EventArgs e)
    {
        if ((CmdZone.Value == "None") || (CmdGroup.SelectedValue.ToString() == "none") || (CmdGroupLeader.SelectedValue.ToString() == "None") || (CmdDauys.Value == "none"))
        {
            NotCompleteNotie();
            return;
        }



        int complete = connect.SingleIntSQL("INSERT INTO iConnect (zone,GroupName,campus,leaderUserID,ChurchID,Isactive,createdby,createdDate,iconnectDay,iconnectTime)VALUES ('" + CmdZone.Value + "', '" + CmdGroup.SelectedValue.ToString() + "','" + Session["Campus"].ToString() + "','" + CmdGroupLeader.SelectedValue.ToString() + "','" + Session["ChurchID"].ToString() + "','1','" + Session["FullName"].ToString() + "',GETDATE(),'" + CmdDauys.Value + "','" + kt_timepicker_1.Value + "')");
         if (complete > 0)
         {
        
             SaveNotie();

             populateGroupLeader();
             CmdDauys.Value = "none";
             CmdZone.Value = "None";
             CmdGroup.SelectedValue = "none";
         }
    }
    protected void CmdGroup_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (CmdGroupLeader.SelectedValue.ToString() != "None")
        {
            populateGroupLeaderPergroup(CmdGroup.SelectedValue.ToString());
        }
    }
}