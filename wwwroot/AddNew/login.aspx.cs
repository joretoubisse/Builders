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

public partial class login : System.Web.UI.Page
{
    #region General Classes
    SqlConnMethod connect = new SqlConnMethod();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        
        }
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
    void RemoveNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ RemoveNotieAlert(); },550);", true);

    }

    void SaveNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ SaveNotieAlert(); },550);", true);

    }
    #endregion


    protected void btnSignIN_ServerClick(object sender, EventArgs e)
    {

        //if (txtUsername.Value == "urcsprings@telkomsa.net" || txtUsername.Value == "jabu")
        //{
        //    if (txtPassword.Value == "123")
        //    {
        //        Session["FName"] = "Nomasonto";
        //        Session["FullName"] = "Nomasonto Mathipa";
        //        Session["ChurchID"] = "1";
        //        Server.Transfer("Dashboard.aspx");
        //    }
        //}

        Session["MemberNo"] = "URCSA";
        Session["FName"] = "Nomasonto";
        Session["FullName"] = "Nomasonto Mathipa";
        Session["ChurchID"] = "1";
        Session["LoggedIn"] = "True";
        Server.Transfer("Dashboard.aspx");
    }
}