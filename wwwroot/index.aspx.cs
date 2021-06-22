using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
    #region General Classes
    SqlConnMethod connect = new SqlConnMethod();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
            if (!Page.IsPostBack)
            {
                Session["FName"] = null;
                Session["FullName"] = null;
                Session["ChurchID"] = null;
                Session["LoggedIn"] = null;
                
                Response.Redirect("login.aspx");
            }
    }

     




    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "Logout.txt");
        #region Local
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, true))
        {
            writer.WriteLine(msg);
        }
        #endregion
    }



}