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

public partial class ViewAttendance : System.Web.UI.Page
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
               // }
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
        string TypeofService = "";
        string totAttendance = "";
        string date = "";
        string BornAgain = "";
        string inviteSouls = "";
        string BapsReady = "";
        string BapsActual = "";
        string ZoneName = "";
        string GroupName = "";
        string IsSchoolConnectgrp = "";
        string CampaignVenue = "";
        DataTable table = new DataTable();
        string Getqry = "";

        Getqry = "SELECT TotNumber,CONVERT(varchar(16),uploaddate,106),TypeService,BornAgain,invitesouls,BaptismReadySouls,BaptismActualSouls,ZoneName,GroupName,IsSchoolConnectGrp,CampaignVenue FROM Attendance WHERE intid = '" + Session["AttendanceID"].ToString() + "'";
        table = connect.DTSQL(Getqry);
        if (table.Rows.Count > 0)
        {
            DivCampaignName.Visible = false;
            DivSouls.Visible = false;
            DivBaptism.Visible = false;
            ConnectDiv.Visible = false;
            DivCampaignName.Visible = false;

            foreach (DataRow rows in table.Rows)
            {
                  totAttendance = rows[0].ToString();
                date = rows[1].ToString();
                TypeofService = rows[2].ToString();
                BornAgain = rows[3].ToString();
                inviteSouls = rows[4].ToString();
                BapsReady = rows[5].ToString();
                BapsActual = rows[6].ToString();
                ZoneName = rows[7].ToString();
                GroupName = rows[8].ToString();
                IsSchoolConnectgrp = rows[9].ToString();
                CampaignVenue = rows[10].ToString();
                #region Handle the page settings
                if (TypeofService == "New Believers Experience" || TypeofService == "iConnect Experience")
                {
                    DivSouls.Visible = true;
                }
                else if (TypeofService == "iConnectGroups")
                {
                    PopulateZone();
                    ConnectDiv.Visible = true;
          
                    DivSouls.Visible = true;
                    CmdZone.Value = ZoneName;
                    CmdGroup.SelectedValue = GroupName;
                    CmdSchoolConnect.Value = IsSchoolConnectgrp;
                }
                else if (TypeofService == "Baptism")
                {

                    DivBaptism.Visible = true;
                }
                else if (TypeofService == "J316" || TypeofService == "Crusades")
                {
                    DivCampaignName.Visible = true;
                }
                else
                {
                    DivSouls.Visible = false;
                }

                CmdService.SelectedValue = TypeofService;
              
          
                txtCampaignName.Value = CampaignVenue;
                txtAmount.Value = totAttendance;
                txtBornAgain.Value = BornAgain;
                txtSoulsInvited.Value = inviteSouls;
                txtReadySoulBap.Value = BapsReady;
                txtActualSoulBap.Value = BapsActual;
                txtAmountDate.Value = date;

                #endregion
            }
        }
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




        int complete = connect.SingleIntSQL("UPDATE Attendance SET TotNumber = '" + txtAmount.Value + "',uploaddate = '" + txtAmountDate.Value + "',BornAgain = '" + txtBornAgain.Value + "',invitesouls = '" + txtSoulsInvited.Value + "',BaptismReadySouls = '" + txtReadySoulBap.Value + "',BaptismActualSouls = '" + txtActualSoulBap.Value + "',ZoneName = '" + ZoneName + "',GroupName = '" + GroupName + "',IsSchoolConnectGrp = '" + isSchoolZone + "',CampaignVenue = '" + txtCampaignName.Value + "' WHERE intid = '" + Session["AttendanceID"].ToString() + "' ");


    
        if (complete > 0)
        {

  
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

    

    
}