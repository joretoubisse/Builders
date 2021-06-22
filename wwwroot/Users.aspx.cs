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

public partial class Users : System.Web.UI.Page
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


    void PopulateParents()
    {

        DataTable table = new DataTable();
        if (Session["ShowAll"].ToString() == "Yes")
        {
            table = connect.DTSQL("SELECT DISTINCT name + ' ' + Surname  + ' - ' + Celno as [Name],intid FROM  Stats_Form WHERE MemberType = 'Member' and IsActive = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' ORDER BY Name ASC");
        }
        else
        {
            table = connect.DTSQL("SELECT DISTINCT name + ' ' + Surname  + ' - ' + Celno as [Name],intid FROM  Stats_Form WHERE MemberType = 'Member' and IsActive = '1' and Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' ORDER BY Name ASC");
        }

        PullParents.DataSource = table;
        PullParents.DataTextField = "Name";
        PullParents.DataValueField = "intid";
        PullParents.DataBind();

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
            }
            else
            {
                if (Session["AdminRight"].ToString() == "Yes")
                {
                    IsAdmin.Value = "1";
                    DivAddMember.Visible = true;
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

        PopulateCampus();
        PopulateParents();
        PopulateRoles();
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

        string Getqry = "";
        if (Session["ShowAll"].ToString() == "Yes")
        {
            Getqry = "SELECT intid , username,name + ' '  +surname,access,emailadd,Campus  FROM ChurchUsers  WHERE churchid = '" + Session["ChurchID"].ToString() + "'  and IsActive = '1'  ORDER BY  Name + ' ' + Surname ASC";
        }
        else
        {
            Getqry = "SELECT intid , username,name + ' '  +surname,access,emailadd,Campus  FROM ChurchUsers  WHERE churchid = '" + Session["ChurchID"].ToString() + "'  and IsActive = '1' and Campus = '" + Session["Campus"].ToString() + "' ORDER BY  Name + ' ' + Surname ASC";
        }
       
        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                    "    <th>  </th> " +
                    "    <th> Username</th> " +
                    "    <th> Name </th> " +

                     "    <th > Access Right</th> " +
                     "    <th > Campus</th> " +
                      "    <th >Email </th> " +
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

                              "   <td >" + Row[3].ToString() + "</td> " +
                               "   <td >" + Row[5].ToString() + "</td> " +
                              "   <td >" + Row[4].ToString() + "</td> " +

                        " </tr>";


            }
        }
        else
        {
            htmltext = "No Users";
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
        Response.Redirect("EditUsers.aspx");
    
    }

    protected void btnCancel_ServerClick(object sender, EventArgs e)
    {
        txtSurname.Value = "";
        txtName.Value = "";

        txtEmail.Value = "";
        txtUsername.Value = "";
        txtConfirmPassword.Value = "";
        txtPassword.Value = "";
        DivUsers.Visible = false;
       // PopulateCampus();
        PopulateParents();
        PopulateRoles();
        DivGridUsers.Visible = true;
    }

    protected void btnArchive_ServerClick(object sender, EventArgs e)
    {

        int complete = connect.SingleIntSQL("UPDATE  ChurchUsers SET IsActive = '0' ,lastupdateby = '" + Session["FullName"].ToString() + "' ,LastUpdateDate = GETDATE() WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and intid = '" + MemberID.Value + "' ");
        if (complete > 0)
        {
            RunUSers();
            RemoveNotie();
        }
    }


    void RefreshDropDowns()
    {
        CmdAdminRole.Value = "None";
        CmdMembersRole.Value = "None";
        CmdVisitors.Value = "None";
        CmdSundaySchool.Value = "None";
        CmdAttendance.Value = "None";
        CmdCommunication.Value = "None";
        CmdOffering.Value = "None";
        CmdNewApplicable.Value = "None";
        CmbConnect.Value = "None";
        CmdEvents.Value = "None";
        CmdResource.Value = "None";
        CmdMinistry.Value = "None";
        CmdEvangelist.Value = "None";
        CmdReports.Value = "None";
        CmdDashboardRole.Value = "None";
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
        int CheckUserExists = int.Parse(connect.SingleRespSQL("SELECT COUNT(intid) FROM ChurchUsers WHERE  isactive = '1' and username = '" + txtUsername.Value + "' and churchid = '" + Session["ChurchID"].ToString() + "'"));
        if (CheckUserExists > 0)
        {
            NotieUserExist();
            return;
        }


        if ((CmdAccess.Value == "None") || (txtUsername.Value == "") || (txtPassword.Value == "") || (txtConfirmPassword.Value == "") )
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
        string AdminRoles = CmdAdminRole.Value;
        string MembersRole = CmdMembersRole.Value;
        string VisitorsRole = CmdVisitors.Value;
        string SundaySchoolRole = CmdSundaySchool.Value;
        string AttendanceRole = CmdAttendance.Value;
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


        if ((DashRole == "None") || (ReportsRole == "None") || (EvangelistRole == "None") || (MinistryRole == "None") || (ResourcesRole == "None") || (EventsRoles == "None") || (ConnectGrouRole == "None") || (NewApplicationRole == "None") || (AdminRoles == "None") || (MembersRole == "None") || (VisitorsRole == "None") || (SundaySchoolRole == "None") || (AttendanceRole == "None") || (CommunicationRole == "None") || (OfferingRole == "None"))
        {
            NotCompleteNotie();
            return;
        }
        #endregion


        string Campus = "";
        DataTable GetInfo = connect.DTSQL("SELECT  name,Surname,emailadd,Campus FROM  Stats_Form WHERE intid = '" + PullParents.Value + "'");
        if (GetInfo.Rows.Count > 0)
        {
            foreach (DataRow rows in GetInfo.Rows)
            {
                txtSurname.Value = rows[1].ToString();
                txtName.Value = rows[0].ToString();
                txtEmail.Value = rows[2].ToString();
                Campus =  rows[3].ToString();
            }
        }
        else
        {
            NotCompleteNotie();
            return;
        }



        int complete = connect.SingleIntSQL("INSERT INTO ChurchUsers (DashboardRole,AdminRole,MembersRole ,VisitorRole ,SundaySchoolRole ,AttendanceRole ,Communication,Offering,NewMembersAppRole ,ConnectGroupRole,EventsRole ,ResourceRole,MinistryRole,EvangelistRole ,ReportsRole,username,surname,Name,access,Pword,churchid,IsActive,emailadd,createdby,createdDate,UserRoles,Campus,MemberID)VALUES ('" + DashRole + "','" + AdminRoles + "','" + MembersRole + "','" + VisitorsRole + "','" + SundaySchoolRole + "','" + AttendanceRole + "','" + CommunicationRole + "','" + OfferingRole + "','" + NewApplicationRole + "','" + ConnectGrouRole + "','" + EventsRoles + "','" + ResourcesRole + "','" + MinistryRole + "','" + EvangelistRole + "','" + ReportsRole + "','" + txtUsername.Value + "', '" + txtSurname.Value + "','" + txtName.Value + "','" + CmdAccess.Value + "','" + strEncodedPassword + "','" + Session["ChurchID"].ToString() + "','1','" + txtEmail.Value + "', '" + Session["FullName"].ToString() + "',GETDATE(),'','" + Campus + "','" + PullParents.Value + "')");
        if (complete > 0)
        {


            txtSurname.Value = "";
            txtName.Value = "";
            RefreshDropDowns();
            txtEmail.Value = "";
            txtUsername.Value = "";
            txtConfirmPassword.Value = "";
            txtPassword.Value = "";
            PopulateCampus();
            PopulateParents();
            PopulateRoles();
            RunUSers();
            SaveNotie();
        }



    }
    protected void AddUsers_ServerClick(object sender, EventArgs e)
    {
        DivUsers.Visible = true;
        DivGridUsers.Visible = false;
    }
}