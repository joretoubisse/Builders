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

public partial class Admin : System.Web.UI.Page
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
                ShowCampus.Visible = true;
                SmsPort.Visible = true;
                DivNotAdmin.Visible = false;
                Main.Visible = true;
                IsAdmin.Value = "1";
                DivAddMember.Visible = false;
                CampusRefresh();
                Btn1.Visible = true;
                Btn2.Visible = true;
                Btn3.Visible = true;

            }
            else
            {

                if (Session["AdminRight"].ToString() == "1")
                {
                    DivNotAdmin.Visible = false;
                    Main.Visible = true;
                    IsAdmin.Value = "1";
                    DivAddMember.Visible = false;
                    CampusRefresh();
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


    void CampusRefresh()
    {
        if (Session["Campus"].ToString() == "Benoni Campus")
        {
            Btn1.Attributes["class"] = "btn btn btn-primary btn-sm btn-bold btn-upper";
            Btn2.Attributes["class"] = "btn btn-default btn-sm btn-bold btn-upper";
            Btn3.Attributes["class"] = "btn btn-default btn-sm btn-bold btn-upper";
        }
        else if (Session["Campus"].ToString() == "Delmas Campus")
        {
            Btn1.Attributes["class"] = "btn btn-default btn-sm btn-bold btn-upper";
            Btn2.Attributes["class"] = "btn btn btn-primary btn-sm btn-bold btn-upper";
            Btn3.Attributes["class"] = "btn btn-default btn-sm btn-bold btn-upper";
        }
        else if (Session["Campus"].ToString() == "Eloff Campus")
        {
            Btn1.Attributes["class"] = "btn btn-default btn-sm btn-bold btn-upper";
            Btn2.Attributes["class"] = "btn btn-default btn-sm btn-bold btn-upper";
            Btn3.Attributes["class"] = "btn btn btn-primary btn-sm btn-bold btn-upper";
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


                if (MenuName == "Admin")
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

 

    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "Admin.txt");
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






    protected void Btn1_ServerClick(object sender, EventArgs e)
    {

          Session["Campus"]  = "Benoni Campus";
          Session["FName"] = Session["FNames"].ToString() + "  - " + Session["Campus"].ToString();
          RunOnLoad();
        
    }
    protected void Btn2_ServerClick(object sender, EventArgs e)
    {
        Session["Campus"] = "Delmas Campus";
        Session["FName"] = Session["FNames"].ToString() + "  - " + Session["Campus"].ToString();
        RunOnLoad();
    }
    protected void Btn3_ServerClick(object sender, EventArgs e)
    {
        Session["Campus"] = "Eloff Campus";
        Session["FName"] = Session["FNames"].ToString() + "  - " + Session["Campus"].ToString();
        RunOnLoad();
    }
}