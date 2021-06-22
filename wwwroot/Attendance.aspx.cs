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

public partial class Attendance : System.Web.UI.Page
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
                //if (Session["Resources"].ToString() == "1")
                //{

                    IsAdmin.Value = "1";
                    DivGridUsers.Visible = true;
                    DivAddMember.Visible = false;
                //}
            }


            PopulateAttendace();
        }
        else
        {
            IsAdmin.Value = "1";
            PopulateAttendace();
            DivAddMember.Visible = false;
        }

        RunMenus();




       
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
        CmdGroup.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select", "None"));

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


                if (MenuName == "Attendance")
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

    void PopulateAttendace()
    {

        string htmltext = "";
        DataTable table = new DataTable();
        string Getqry = "";
        if (Session["ShowAll"].ToString() == "Yes")
        {
            Getqry = "SELECT TOP(200)  TotNumber,CONVERT(varchar(16),uploaddate,106),FullName,TypeService,BornAgain,invitesouls,intid,BaptismReadySouls,BaptismActualSouls,CampaignVenue FROM Attendance WHERE churchid = '" + Session["ChurchID"].ToString() + "'  and TypeSection = '" + CheckType.Value + "'  ORDER BY  intid DESC";

        }
        else
        {
            Getqry = "SELECT TOP(200) TotNumber,CONVERT(varchar(16),uploaddate,106),FullName ,TypeService,BornAgain,invitesouls,intid,BaptismReadySouls,BaptismActualSouls,CampaignVenue FROM Attendance WHERE churchid = '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + "' and TypeSection = '" + CheckType.Value + "'  ORDER BY  intid DESC";
        }

        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                     "    <th>  </th> " +
                     "    <th> Experience </th> " +
                    "    <th> Attendance </th> " +
                      "    <th> Born Again </th> " +
                   "    <th> Invited Souls </th> " +
                     "    <th> No. of Baptism Ready Souls </th> " +
                     "    <th>No. of Actiual Souls Baptised </th> " +

                     "    <th>Campaign name </th> " +

                   "    <th> Date </th> " +
                       "    <th> Captured by </th> " +

                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {

                htmltext += " <tr> " +

                              "<td ><center><a onclick='BtnViewMem(this.id);' id='" + Row[6].ToString() + "' class='btn btn-secondary'> View  </a> &nbsp;	&nbsp; <a onclick='BtnArchMem(this.id);' id='" + Row[6].ToString() + "' class='btn btn-secondary'> Remove </a>  </center></td> " +
                             "   <td >" + Row[3].ToString() + "</td> " +
                             "   <td >" + Row[0].ToString() + "</td> " +
                                 "   <td >" + Row[4].ToString() + "</td> " +
                            "   <td >" + Row[5].ToString() + "</td> " +
                                 "   <td >" + Row[7].ToString() + "</td> " +
                                   "   <td >" + Row[8].ToString() + "</td> " +
                                     "   <td >" + Row[9].ToString() + "</td> " +
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

        int complete = connect.SingleIntSQL("DELETE FROM Attendance WHERE  intid = '" + MemberID.Value + "' ");
        if (complete > 0)
        {
            PopulateAttendace();
            RemoveNotie();
        }



    }
    protected void btnCancel_ServerClick(object sender, EventArgs e)
    {
        Server.Transfer("Attendance.aspx");
    }
    protected void BtnSave_ServerClick(object sender, EventArgs e)
    {

        string GroupName = "";
        string ZoneName = "";
        string isSchoolZone = "";

        #region Handle all the checks 
        if (txtAmount.Value == "")
        {
            NotCompleteNotie();
            return;
        }

        if (txtAmountDate.Value == "")
        {
            NotCompleteNotie();
            return;
        }

        if (CmdService.SelectedValue.ToString() == "none")
        {
            NotCompleteNotie();
            return;
        }


        if (CmdService.SelectedValue.ToString() == "iConnectGroups")
        {
            if (CmdZone.Value== "None")
            {
                NotCompleteNotie();
                return;
            }

            if (CmdGroup.SelectedValue.ToString() == "None")
            {
                NotCompleteNotie();
                return;
            }


            if (CmdSchoolConnect.Value == "None")
            {
                NotCompleteNotie();
                return;
            }

            ZoneName = CmdZone.Value;
            GroupName = CmdGroup.SelectedValue.ToString();
            isSchoolZone = CmdSchoolConnect.Value;

        }
        #endregion







        int complete = connect.SingleIntSQL("INSERT INTO Attendance (ChurchID,Campus,FullName,TypeSection,UploadDate,CaptureDate,TotNumber,TypeService,BornAgain,invitesouls,BaptismReadySouls,BaptismActualSouls,ZoneName,GroupName,IsSchoolConnectGrp,CampaignVenue)VALUES ('" + Session["ChurchID"].ToString() + "', '" + Session["Campus"].ToString() + "','" + Session["FullName"].ToString() + "','" + CheckType.Value + "','" + txtAmountDate.Value + "',GETDATE(),'" + txtAmount.Value + "','" + CmdService.SelectedValue.ToString() + "','" + txtBornAgain.Value + "','" + txtSoulsInvited.Value + "','" + txtReadySoulBap.Value + "','" + txtActualSoulBap.Value + "','" + ZoneName + "','" + GroupName + "','" + isSchoolZone + "','" + txtCampaignName.Value + "')");
        if (complete > 0)
        {

            txtCampaignName.Value = "";
            txtReadySoulBap.Value = "";
            txtActualSoulBap.Value = "";
            txtBornAgain.Value = "";
            txtAmountDate.Value = "";
            txtAmount.Value = "";
            txtSoulsInvited.Value = "";
            DivCampaignName.Visible = false;
            DivSouls.Visible = false;
            DivBaptism.Visible = false;
            ConnectDiv.Visible = false;
            DivCampaignName.Visible = false;
            PopulateAttendace();
            SaveNotie();
        }
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

    protected void CmdService_SelectedIndexChanged(object sender, EventArgs e)
    {
        DivCampaignName.Visible = false;
        DivSouls.Visible = false;
        DivBaptism.Visible = false;
        ConnectDiv.Visible = false;
        DivCampaignName.Visible = false;
        if (CmdService.SelectedValue.ToString() == "New Believers Experience" || CmdService.SelectedValue.ToString() == "iConnect Experience")
        {
            DivSouls.Visible = true;
        }
        else if (CmdService.SelectedValue.ToString() == "iConnectGroups")
        {
            PopulateZone();
            ConnectDiv.Visible = true;
            DivSouls.Visible = true;
        }
        else if (CmdService.SelectedValue.ToString() == "Baptism")
        {

            DivBaptism.Visible = true;
        }
        else if (CmdService.SelectedValue.ToString() == "J316" || CmdService.SelectedValue.ToString() == "Crusades")
        {
            DivCampaignName.Visible = true;
        }
        else
        {
            DivSouls.Visible = false;
        }
    }


    protected void btnViewMember_ServerClick1(object sender, EventArgs e)
    {
        Session["AttendanceID"] = MemberID.Value;
        Server.Transfer("ViewAttendance.aspx");
    }
}