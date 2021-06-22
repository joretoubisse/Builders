﻿using System;
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

public partial class Resources : System.Web.UI.Page
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
                DivGridUsers.Visible = true;
                DivAddMember.Visible = false;
            }
            else
            {
                if (Session["Resources"].ToString() == "1")
                {

                    IsAdmin.Value = "1";
                    DivGridUsers.Visible = true;
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


                if (MenuName == "Resources")
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
        string Getqry = "SELECT intid,DisplayName,DocType FROM ChurchResource WHERE ChurchID = '" + Session["ChurchID"].ToString() + "'";
        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                    "    <th>  </th> " +
                    "    <th> Name</th> " +
                    "    <th> Type</th> " +
                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {




                htmltext += " <tr> " +

                                 "<td ><center><a onclick='BtnViewMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Download </a>	&nbsp;	&nbsp;	&nbsp;<a onclick='BtnArchMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Remove </a></center></td>" +

                             "   <td >" + Row[1].ToString() + "</td> " +
                          "   <td >" + Row[2].ToString() + "</td> " +

                        " </tr>";


            }
        }
        else
        {
            htmltext = "No Data";
        }


        htmltext += "    </tbody> " +
                   " </table>";
        tbTable.Text = htmltext;
    }


    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "Resources4.txt");
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
        Session["UserID"] = MemberID.Value;
        Server.Transfer("Dashboard.aspx");
    }

  

    protected void btnArchive_ServerClick(object sender, EventArgs e)
    {



        DataTable table = connect.DTSQL("SELECT DocName,DocType,DisplayName FROM ChurchResource WHERE intid = '" + MemberID.Value + "' ");
        if (table.Rows.Count > 0)
        {
            foreach (DataRow rows in table.Rows)
            {
                string filePath = Session["FilePath"].ToString() + @"Resources\" + rows[1].ToString() + @"\" + rows[0].ToString();
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);

                    int complete = connect.SingleIntSQL("DELETE FROM ChurchResource WHERE intid = '" + MemberID.Value + "' ");
                    if (complete > 0)
                    {
                        RunUSers();
                        RemoveNotie();

                    }

                }
            }
        }



    }

    protected void btnCancelCampus_ServerClick(object sender, EventArgs e)
    {
        Server.Transfer("Dashboard.aspx");
    }

    public string DocRandom()
    {
        string otp = "";
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmonpqrstuvwxyz";
        var stringChars = new char[6];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }
        otp = new String(stringChars);

        return otp;
    }
    protected void btnSaveCampus_ServerClick(object sender, EventArgs e)
    {

        string DocName = DocRandom();



        if (CmdType.Value == "none" || txtName.Value == "")
        {
            NotCompleteNotie();
            return;
        }

        

        #region Save Image
        string filePath = "";


        try
        {

            if (UploadImage.HasFile)
            {


                string Extension = Path.GetExtension(UploadImage.PostedFile.FileName.ToString());

                if (!Directory.Exists(Session["FilePath"].ToString() + @"Resources\" + CmdType.Value))
                {
                    Directory.CreateDirectory(Session["FilePath"].ToString() + @"Resources\" + CmdType.Value);
                }

                filePath = Session["FilePath"].ToString() + @"Resources\" + CmdType.Value + @"\" + DocName + Extension;
                UploadImage.PostedFile.SaveAs(filePath);
                string URL = Session["URL"].ToString() + @"/Resources/" + CmdType.Value + "/" + DocName + Extension;



                int complete = connect.SingleIntSQL("INSERT INTO ChurchResource (DocName,DocURL,DocType,ChurchID,DocExtension,DisplayName)VALUES ('" + DocName + Extension + "', '" + URL + "','" + CmdType.Value + "','" + Session["ChurchID"].ToString() + "','" + Extension + "','" + txtName.Value + "')");
                if (complete > 0)
                {

                    txtName.Value = "";
                    RunUSers();
                    SaveNotie();
                }



            }
            else
            {
                NotCompleteNotie();
            }
        }
        catch (Exception ex)
        {
            logthefile("Error : " + ex.ToString());
        }
        #endregion

      

    
    }
    protected void btnViewMember_ServerClick1(object sender, EventArgs e)
    {
        DataTable table = connect.DTSQL("SELECT DocName,DocType FROM ChurchResource WHERE intid = '" + MemberID.Value + "' ");
        if (table.Rows.Count > 0)
        {
            foreach (DataRow rows in table.Rows)
            {

                string path = "";
                path = Path.Combine(Session["FilePath"].ToString() + "/Resources/" + rows[1].ToString() + @"/" +  rows[0].ToString());

                System.IO.FileInfo file = new System.IO.FileInfo(path);
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();
            }
        }
    }
}