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


    void ClearSessions()
    {
        Session["KidsRights"] = null;
        Session["ConnectRight"] = null;
        Session["MemberRight"] = null;
        Session["AdminRight"] = null;
        Session["Iserver"] = null;
        Session["VisitorRight"] = null;
        Session["EventRights"] = null;
        Session["NewMemberRights"] = null;
        Session["ReportsRights"] = null;
        Session["NotieRights"] = null;
    }

    void logthefile(string msg)
    {
        //string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "login.txt");
        //#region Local
        //using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, true))
        //{
        //    writer.WriteLine(msg);
        //}
        //#endregion
    }



    #region Message Boxes
    void RemoveNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ RemoveNotieAlert(); },550);", true);

    }

    void InvalidPNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ InvalidP(); },550);", true);

    }




    void SaveNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ SaveNotieAlert(); },550);", true);

    }
    #endregion

    public static string RandomString(int length)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[length];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        var finalString = new String(stringChars);


        return finalString.ToString();
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string unidentifier = RandomString(8);


        if (txtPassword.Text == "")
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            InvalidPNotie();
            return;
        }

        if (txtUsername.Text == "")
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            InvalidPNotie();
            return;
        }



        if (ConfigurationManager.AppSettings["HideFields"].ToString() == "1")
        {

            DataTable Ntable = new DataTable();

            #region validate Password
            string strPassword = txtPassword.Text;
            // Encode before comparing the hashes - Must match the one on the database
            byte[] sByBuf = Encoding.ASCII.GetBytes(strPassword);
            for (int nCtr = 0; nCtr < sByBuf.Length; nCtr++)
                sByBuf[nCtr] = Convert.ToByte(Convert.ToInt64(sByBuf[nCtr]) + 4);
            string strEncodedPassword = Encoding.ASCII.GetString(sByBuf);
            #endregion



            Ntable = connect.DTSQL("SELECT  intid,username, name + ' ' +  surname,access,emailAdd,name,Campus,churchid, Pword,AdminRole,MembersRole ,VisitorRole ,SundaySchoolRole ,AttendanceRole ,Communication,Offering,NewMembersAppRole ,ConnectGroupRole,EventsRole ,ResourceRole,MinistryRole,EvangelistRole ,ReportsRole  FROM ChurchUsers  Where Username = '" + txtUsername.Text + "'");
            if (Ntable.Rows.Count > 0)
            {
                foreach (DataRow rows in Ntable.Rows)
                {

                    if (strEncodedPassword == rows[8].ToString())
                    {

                        connect.SingleIntSQL("INSERT Into  loginhistory (Username,CreatedDate,Identifier) VALUES ('" + txtUsername.Text + "',GETDATE(),'" + unidentifier + "')");
                        Session["UsersID"] = rows[0].ToString();
                        Session["Username"] = rows[1].ToString();
                        Session["Campus"] = rows[6].ToString();
                        Session["AccessRights"] = rows[3].ToString();

                        if (rows[3].ToString() == "Admin")
                        {
                            Session["ShowAll"] = "Yes";
                        }
                        else
                        {
                            Session["ShowAll"] = "No";
                        }

                        Session["FName"] = rows[5].ToString() + "  - " + rows[6].ToString();
                        Session["FNames"] = rows[5].ToString();
                        Session["FullName"] = rows[2].ToString();
                        Session["ChurchID"] = rows[7].ToString();
                        Session["LoggedIn"] = "True";
                    
                        Session["MemberNo"] = "B_Church";
                        Session["URL"] = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                        Session["FilePath"] = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                        Session["ChurchName"] = "Builders Church";
                        Session["Footer"] = @"2020&nbsp;&copy;&nbsp;<a href='logout.aspx' target='_blank'class='kt-link'>Builders Church</a>";
                        Session["identifier"] = unidentifier;

                        #region Set  Access Rights Session
                        Session["AdminRight"] = rows[9].ToString();
                        Session["MemberRight"] = rows[10].ToString();
                        Session["VisitorRight"] = rows[11].ToString();
                        Session["KidsRights"] = rows[12].ToString();
                        Session["Attendance"] = rows[13].ToString();
                        Session["NotieRights"] = rows[14].ToString();
                        Session["Offering"] = rows[15].ToString();
                        Session["NewMemberRights"] = rows[16].ToString();
                        Session["ConnectRight"] = rows[17].ToString();
                        Session["EventRights"] = rows[18].ToString();
                        Session["Resources"] = rows[19].ToString();
                        Session["Iserver"] = rows[20].ToString();
                        Session["Evangelism"] = rows[21].ToString();
                        Session["ReportsRights"] = rows[22].ToString();
                        #endregion

                        
                        Server.Transfer("Dashboard.aspx");
                        

                    }
                    else
                    {
                   
                        InvalidPNotie();
                    }
                }
            }
            else
            {
            
               InvalidPNotie();
            }

        }

    }
}