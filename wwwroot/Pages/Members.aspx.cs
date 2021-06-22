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

public partial class Members : System.Web.UI.Page
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


                if (MenuName == "Members")
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

            if (Session["ShowAll"].ToString() == "Yes")
            {
                IsAdmin.Value = "1";
                DivAddMember.Visible = true;
                Filter.Visible = true;
            }
            else
            {
                if (Session["MemberRight"].ToString() == "1")
                {
                    IsAdmin.Value = "1";
                    DivAddMember.Visible = true;
                }
            }

            PopulateCampus();
            RunMembers();
        }
        else
        {
            IsAdmin.Value = "1";
            PopulateMembers();
            DivAddMember.Visible = false;
        }


        RunMenus();
       
    }

   
    void PopulateMembers()
    {

        string htmltext = "";
        DataTable table = new DataTable();
        string Getqry = "";
        if (Session["ShowAll"].ToString() == "Yes")
        {
            Getqry = "SELECT intid, Name + ' ' + Surname,Ward,Ministries,Celno,MemberNo FROM Stats_Form WHERE ChurchID = '" + Session["ChurchID"].ToString() + "'  and IsActive = '1' and MemberType = 'Member'  ORDER BY  Name + ' ' + Surname ASC";
        }
        else
        {
            Getqry = "SELECT intid, Name + ' ' + Surname,Ward,Ministries,Celno,MemberNo FROM Stats_Form WHERE ChurchID = '" + Session["ChurchID"].ToString() + "'  and IsActive = '1' and MemberType = 'Member' and campus = '" + Session["Campus"].ToString() + "' ORDER BY  Name + ' ' + Surname ASC";
        }

        
        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                    "    <th>  </th> " +
                    "    <th> Name</th> " +
                    "    <th> Ward </th> " +
                
                     "    <th > Cell No</th> " +
                      "    <th >Member No</th> " +
                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {
           



                htmltext += " <tr> " +

                                 "<td ><center><a onclick='BtnViewMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> View </a>	&nbsp;	&nbsp;	&nbsp;<a onclick='BtnArchMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Remove </a></center></td>" +

                             "   <td >" + Row[1].ToString() + "</td> " +
                             "   <td >" + Row[2].ToString() + "</td> " +
                       
                              "   <td >" + Row[4].ToString() + "</td> " +
                              "   <td >" + Row[5].ToString() + "</td> " +

                        " </tr>";


            }
        }
        else
        {
            htmltext = "No Members";
        }


        htmltext += "    </tbody> " +
                   " </table>";
        tbTable.Text = htmltext;
    }

    void PopulateCampus()
    {
        DataTable table = connect.DTSQL("SELECT  campus,campus FROM Campus WHERE churchid = '" + Session["ChurchID"].ToString() + "'");
        CmdCampus.DataSource = table;
        CmdCampus.DataTextField = "campus";
        CmdCampus.DataValueField = "campus";
        CmdCampus.DataBind();
        CmdCampus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select", "None"));
    }


    void RunMembers()
    {

        string htmltext = "";
        DataTable table = new DataTable();
        string Getqry = "";
        if (Session["ShowAll"].ToString() == "Yes")
        {
            Getqry = "SELECT intid, Name + ' ' + Surname,gender,Ministries,Celno,MemberNo FROM Stats_Form WHERE ChurchID = '" + Session["ChurchID"].ToString() + "'  and IsActive = '1' and MemberType = 'Member'  ORDER BY  Name + ' ' + Surname ASC";
        }
        else
        {
            Getqry = "SELECT intid, Name + ' ' + Surname,gender,Ministries,Celno,MemberNo FROM Stats_Form WHERE ChurchID = '" + Session["ChurchID"].ToString() + "'  and IsActive = '1' and MemberType = 'Member' and campus = '" + Session["Campus"].ToString() + "'  ORDER BY  Name + ' ' + Surname ASC";
        }

        
             
        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                    "    <th>  </th> " +
                    "    <th> Name</th> " +
                    "    <th> Gender </th> " +

                     "    <th > Cell No</th> " +
                      "    <th >Member No</th> " +
                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {




                htmltext += " <tr> " +

                                 "<td ><center><a onclick='BtnViewMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> View </a> 	&nbsp;	&nbsp;	&nbsp;<a onclick='BtnArchMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Remove </a></center></td>" +

                             "   <td >" + Row[1].ToString() + "</td> " +
                             "   <td >" + Row[2].ToString() + "</td> " +

                              "   <td >" + Row[4].ToString() + "</td> " +
                              "   <td >" + Row[5].ToString() + "</td> " +

                        " </tr>";


            }
        }
        else
        {
            htmltext = "No Members";
        }


        htmltext += "    </tbody> " +
                   " </table>";
        tbTable.Text = htmltext;
    }


    void RunMemberOnSearch(string Campus)
    {

        string htmltext = "";
        DataTable table = new DataTable();
        string Getqry = "";
        if (Campus == "All")
        {
            Getqry = "SELECT intid, Name + ' ' + Surname,gender,Ministries,Celno,MemberNo FROM Stats_Form WHERE ChurchID = '" + Session["ChurchID"].ToString() + "'  and IsActive = '1' and MemberType = 'Member'  ORDER BY  Name + ' ' + Surname ASC";
        }
        else
        {
            Getqry = "SELECT intid, Name + ' ' + Surname,gender,Ministries,Celno,MemberNo FROM Stats_Form WHERE ChurchID = '" + Session["ChurchID"].ToString() + "'  and IsActive = '1' and MemberType = 'Member' and campus = '" + Campus + "'  ORDER BY  Name + ' ' + Surname ASC";
        }



        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                    "    <th>  </th> " +
                    "    <th> Name</th> " +
                    "    <th> Gender </th> " +

                     "    <th > Cell No</th> " +
                      "    <th >Member No</th> " +
                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {




                htmltext += " <tr> " +

                                 "<td ><center><a onclick='BtnViewMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> View </a> 	&nbsp;	&nbsp;	&nbsp;<a onclick='BtnArchMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Remove </a></center></td>" +

                             "   <td >" + Row[1].ToString() + "</td> " +
                             "   <td >" + Row[2].ToString() + "</td> " +

                              "   <td >" + Row[4].ToString() + "</td> " +
                              "   <td >" + Row[5].ToString() + "</td> " +

                        " </tr>";


            }
        }
        else
        {
            htmltext = "No Members";
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

        Session["MemType"] = connect.SingleRespSQL("SELECT MemberType  FROM Stats_Form WHERE intid = '" + MemberID.Value + "'");
        Session["MemberID"] = MemberID.Value;
        Server.Transfer("ViewMembers.aspx");
        
    }



    protected void btnArchive_ServerClick(object sender, EventArgs e)
    {

 
        int complete = connect.SingleIntSQL("UPDATE  Stats_Form SET IsActive = '0' WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and intid = '" + MemberID.Value + "' ");
        if (complete > 0)
        {
            if (ConfigurationManager.AppSettings["HideFields"].ToString() == "1")
            {
                RunMembers();
            }
            else
            {
                PopulateMembers();
            }
            RemoveNotie();
        }
    }

    protected void btnSearchCampus_ServerClick(object sender, EventArgs e)
    {
        RunMemberOnSearch(CmdCampus.Value);
    }
}