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

public partial class Offering : System.Web.UI.Page
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
        PopulateOffering();
    }

    void PopulateOffering()
    {

        string htmltext = "";
        DataTable table = new DataTable();


        string Getqry = "SELECT  intid,CONVERT(Varchar(16),offeringDate,106),amount FROM Offering WHERE  ChurchID = '" + Session["ChurchID"].ToString() + "' ORDER BY  intid DESC";
        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
      
                    "    <th> Date</th> " +
                    "    <th> Amount </th> " +

                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {




                htmltext += " <tr> " +

                            
                             "   <td >" + Row[1].ToString() + "</td> " +
                             "   <td >R " + Row[2].ToString() + "</td> " +

                        " </tr>";


            }
        }
        else
        {
            htmltext = "No Offering";
        }


        htmltext += "    </tbody> " +
                   " </table>";
        tbTable.Text = htmltext;
    }

    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "Statistics.txt");
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
        Server.Transfer("Dashboard.aspx");
    }
    protected void BtnSave_ServerClick(object sender, EventArgs e)
    {

        int complete = connect.SingleIntSQL("INSERT INTO Offering (ChurchID,OfferingDate,UploadDate,Amount)VALUES ('" + Session["ChurchID"].ToString() + "', '" + txtAmountDate.Value + "',GETDATE(),'" + txtAmount.Value + "')");
        if (complete > 0)
        {
            txtAmountDate.Value = "";
            txtAmount.Value = "";
            PopulateOffering();
            SaveNotie();
        }
    }
}