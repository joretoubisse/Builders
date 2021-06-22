using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyProfile : System.Web.UI.Page
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
            DivPass.Visible = true;
            BtnSave.Visible = true;
            IsAdmin.Value = "1";
           
        }
        else
        {
            IsAdmin.Value = "1";
           
          
        }

        PopulateCampus();
        PopulateRoles();
        populateUser();

        DivCampus.Visible = false;
        RunMenus();

       
    }

    void PopulateRoles()
    {
        DataTable table = new DataTable();
        table.Columns.Add("Value", typeof(string));
        table.Columns.Add("Text", typeof(string));


        table.Rows.Add("None", "Please Select");

        table.Rows.Add("Admin", "Admin");
        table.Rows.Add("Users", "Users");
     

        CmdAccess.DataSource = table;
        CmdAccess.DataTextField = "Text";
        CmdAccess.DataValueField = "Value";
        CmdAccess.DataBind();



    }

    void PopulateCampus()
    {
        //DataTable table = connect.DTSQL("SELECT  campus,campus FROM Campus WHERE churchid = '" + Session["ChurchID"].ToString() + "'");
        //CmdCampus.DataSource = table;
        //CmdCampus.DataTextField = "campus";
        //CmdCampus.DataValueField = "campus";
        //CmdCampus.DataBind();
        //CmdCampus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select", "None"));
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


                if (MenuName == "My Profile")
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

   

  

    protected void btnCancel_ServerClick(object sender, EventArgs e)
    {
        Server.Transfer("dashboard.aspx");
    }



    void populateUser()
    {
        DataTable table = new DataTable();
        table = connect.DTSQL("SELECT username,surname,Name,access,Pword,churchid,IsActive,emailadd,UserRoles,Campus,MemberID,AdminRole,MembersRole ,VisitorRole ,SundaySchoolRole ,AttendanceRole ,Communication,Offering,NewMembersAppRole ,ConnectGroupRole,EventsRole ,ResourceRole,MinistryRole,EvangelistRole ,ReportsRole,DashboardRole   FROM ChurchUsers  WHERE intid = '" + Session["UsersID"].ToString() + "'");
        if (table.Rows.Count > 0)
        {
            foreach (DataRow rows in table.Rows)
            {
                txtSurname.Value = rows[1].ToString();
                txtName.Value = rows[2].ToString();
                CmdAccess.Value  = rows[3].ToString();
                txtEmail.Value = rows[7].ToString();
                txtUsername.Value = rows[0].ToString();
                //CmdRoles.Value =  rows[8].ToString();
                CmdCampus.Value = rows[9].ToString();

                #region Decode the Password
                ASCIIEncoding ascii = new ASCIIEncoding();
                string strPassword = rows[4].ToString();
                byte[] sByBuf = Encoding.ASCII.GetBytes(strPassword);
                for (int nCtr = 0; nCtr < sByBuf.Length; nCtr++)
                    sByBuf[nCtr] = Convert.ToByte(Convert.ToInt64(sByBuf[nCtr]) - 4);
                string decodedString = ascii.GetString(sByBuf);
        
                #endregion
                txtConfirmPassword.Value = decodedString;
                txtPassword.Value = decodedString;

                GetMemberID.Value = rows[10].ToString();


                #region Access Rights
                CmdAdminRole.Value = rows[11].ToString();
                CmdMembersRole.Value = rows[12].ToString();
                CmdVisitors.Value = rows[13].ToString();
                CmdSundaySchool.Value = rows[14].ToString();
                CmdAttendance.Value = rows[15].ToString();
                CmdCommunication.Value = rows[16].ToString();
                CmdOffering.Value = rows[17].ToString();

                CmdNewApplicable.Value = rows[18].ToString();
                CmbConnect.Value = rows[19].ToString();
                CmdEvents.Value = rows[20].ToString();
                CmdResource.Value = rows[21].ToString();


                CmdMinistry.Value = rows[22].ToString();
                CmdEvangelist.Value = rows[23].ToString();
                CmdReports.Value = rows[24].ToString();
                CmdDashboardRole.Value = rows[25].ToString();
            
                #endregion
            }
        }

    }


    protected void BtnSave_ServerClick(object sender, EventArgs e)
    {
        #region Encrypt Password
        String strPassword = txtConfirmPassword.Value;
        // Encode before comparing the hashes - Must match the one on the database
        byte[] sByBuf = Encoding.ASCII.GetBytes(strPassword);
        for (int nCtr = 0; nCtr < sByBuf.Length; nCtr++)
            sByBuf[nCtr] = Convert.ToByte(Convert.ToInt64(sByBuf[nCtr]) + 4);
        String strEncodedPassword = Encoding.ASCII.GetString(sByBuf);
        #endregion


        #region Run Checks before Saving in SQL


        if ((CmdAccess.Value == "None") || (txtUsername.Value == "") || (txtPassword.Value == "") || (txtConfirmPassword.Value == ""))
        {
            NotCompleteNotie();
            return;
        }

        if(txtPassword.Value != txtConfirmPassword.Value)
        {
            NotieRunInvalidPass();
            return;
        }
        #endregion



        #region Access Rights
        string AdminRoles =   CmdAdminRole.Value;
        string MembersRole = CmdMembersRole.Value;
        string VisitorsRole = CmdVisitors.Value;
        string SundaySchoolRole = CmdSundaySchool.Value;
        string  AttendanceRole = CmdAttendance.Value;
        string CommunicationRole = CmdCommunication.Value;
        string OfferingRole = CmdOffering.Value;
        string NewApplicationRole = CmdNewApplicable.Value;
        string ConnectGrouRole = CmbConnect.Value;
        string EventsRoles = CmdEvents.Value;
        string ResourcesRole = CmdResource.Value;
        string MinistryRole = CmdMinistry.Value;
        string EvangelistRole = CmdEvangelist.Value;
        string ReportsRole = CmdReports.Value;
        string DashRole = CmdDashboardRole.Value;

        //if ((DashRole == "None") || (ReportsRole == "None") || (EvangelistRole == "None") || (MinistryRole == "None") || (ResourcesRole == "None") || (EventsRoles == "None") || (ConnectGrouRole == "None") || (NewApplicationRole == "None") || (AdminRoles == "None") || (MembersRole == "None") || (VisitorsRole == "None") || (SundaySchoolRole == "None") || (AttendanceRole == "None") || (CommunicationRole == "None") || (OfferingRole == "None"))
        //{
        //    NotCompleteNotie();
        //    return;
        //}
        #endregion





        string Campus = Session["Campus"].ToString();

        string updateqry = "UPDATE ChurchUsers SET surname = @surname,Name = @Name,Pword = @Pword,emailadd = @emailadd,lastupdateby = @lastupdateby ,LastUpdateDate = GETDATE() WHERE intid  = @intid; SELECT @intid ";
        SqlParameter[] updatesp =
        {
            new SqlParameter("@surname", txtSurname.Value),
            new SqlParameter("@Name",  txtName.Value),
 
            new SqlParameter("@Pword", strEncodedPassword),
             new SqlParameter("@emailadd", txtEmail.Value),
            new SqlParameter("@lastupdateby", Session["FullName"].ToString()),

            new SqlParameter("@intid",Session["UsersID"].ToString()),

        };

        int complete = int.Parse(connect.SingleIntQrySQL(updatesp, updateqry));
        if (complete > 0)
        {

            connect.SingleIntSQL("UPDATE Stats_Form SET   name = '" + txtName.Value + "',Surname = '" + txtSurname.Value + "',EmailAdd = '" + txtEmail.Value + "'  WHERE intid = '" + GetMemberID.Value + "'");
            SaveNotie();
        }



    }
   
}