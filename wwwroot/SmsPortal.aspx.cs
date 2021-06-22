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

public partial class SmsPortal : System.Web.UI.Page
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
            }
            else
            {
                if (Session["AccessRights"].ToString() == "Admin")
                {
                    IsAdmin.Value = "1";
                    DivAddMember.Visible = false;
                }
            }


      

            RunUSers();
        }
        else
        {
            IsAdmin.Value = "1";
            RunUSers();
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

    void RunUSers()
    {

        string htmltext = "";
        DataTable table = new DataTable();
        string Getqry = "SELECT smsusername,smspassword FROM SmsCredentials WHERE churchid = '" + Session["ChurchID"].ToString() + "'";
        table = connect.DTSQL(Getqry);

        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {

                txtUsername.Value = Row[0].ToString();
                txtPassword.Value = Row[1].ToString(); 

            }
        }
        else
        {
          
        }

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
        BtnReturn.Visible = true;
        DivGridUsers.Visible = false;
        EditDiv.Visible = true;
        NormalBack.Visible = false;
        txtEditName.Value = connect.SingleRespSQL("SELECT MinistryName FROM ChurchMinistry WHERE intid = '" + MemberID.Value + "'");


    }

  

    protected void btnArchive_ServerClick(object sender, EventArgs e)
    {

        int complete = connect.SingleIntSQL("DELETE FROM ChurchMinistry WHERE intid = '" + MemberID.Value + "' ");
        if (complete > 0)
        {
            RunUSers();
            RemoveNotie();
        }
    }

    protected void btnCancelCampus_ServerClick(object sender, EventArgs e)
    {
        Server.Transfer("Admin.aspx");
    }
    protected void btnSaveCampus_ServerClick(object sender, EventArgs e)
    {

        if (txtUsername.Value == "")
        {
            NotCompleteNotie();
            return;
        }

        if (txtPassword.Value == "")
        {
            NotCompleteNotie();
            return;
        }

        int Complete = connect.SingleIntSQL("UPDATE SmsCredentials SEt  SmsUsername = '" + txtUsername.Value + "',SmsPassword = '" + txtPassword.Value + "',CreatedBy = '" + Session["FullName"].ToString() + "',CreatedDate = GETDATE()  WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus ='" + Session["Campus"].ToString() + "' ");
        if (Complete > 0)
        {

            RunUSers();
            SaveNotie();
        }
    }

    protected void btnEditName_ServerClick(object sender, EventArgs e)
    {
        int Complete = connect.SingleIntSQL("UPDATE ChurchMinistry SEt  MinistryName = '" + txtEditName.Value + "'  WHERE intid = '" + MemberID.Value + "'");
        if (Complete > 0)
        {
            BtnReturn.Visible = false;
            NormalBack.Visible = true;
            DivGridUsers.Visible = true;
            EditDiv.Visible = false;
            RunUSers();
            SaveNotie();
        }
    
    }

    protected void btnCancelEdit_ServerClick(object sender, EventArgs e)
    {
        BtnReturn.Visible = false;
        NormalBack.Visible = true;
        DivGridUsers.Visible = true;
        EditDiv.Visible = false;
    }

    protected void BtnReturn_ServerClick(object sender, EventArgs e)
    {
        BtnReturn.Visible = false;
        NormalBack.Visible = true;
        DivGridUsers.Visible = true;
        EditDiv.Visible = false;
    }
}