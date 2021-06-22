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

public partial class Notification : System.Web.UI.Page
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
        Loadfooter.Text = Session["Footer"].ToString();
        lblName.InnerText = Session["FName"].ToString();
        if (Session["ShowAll"].ToString() == "Yes")
        {
            IsAdmin.Value = "1";
            DivCollectOffering.Visible = true;
        }
        else
        {
            IsAdmin.Value = "1";
            DivCollectOffering.Visible = true;
        }

        RunGroups();
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


                if (MenuName == "Notifications")
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
        DataTable table = new DataTable();
        string Getqry = "";
        if (Session["ShowAll"].ToString() == "Yes")
        {
            Getqry = "SELECT TotNumber,CONVERT(varchar(16),uploaddate,106),FullName FROM Attendance WHERE churchid = '" + Session["ChurchID"].ToString() + "'  and TypeSection = '" + CheckType.Value + "'  ORDER BY  intid DESC";

        }
        else
        {
            Getqry = "SELECT TotNumber,CONVERT(varchar(16),uploaddate,106),FullName FROM Attendance WHERE churchid = '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + "' and TypeSection = '" + CheckType.Value + "'  ORDER BY  intid DESC";
        }
       
        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
      
                    "    <th> Captured No</th> " +
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

                            
                             "   <td >" + Row[0].ToString() + "</td> " +
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


   
    protected void btnEvan_ServerClick(object sender, EventArgs e)
    {
        Server.Transfer("Messages.aspx");
    }
    protected void btnMem_ServerClick(object sender, EventArgs e)
    {
        btnDash.Visible = false;
        btnBack.Visible = true;
        MainDiv.Visible = false;
        DivHandleGroups.Visible = true;
    }


    void RunAllMembers()
    {

        string ChecIN = "";
        string htmltext = "";
        DataTable table = new DataTable();
        string Getqry = "SELECT DISTINCT A.intid , B.name + ' ' + B.Surname  + ' - ' + B.Celno as [Name]  FROm MsgGroups A INNER JOIN Stats_Form B ON A.MemberID = B.intid WHERE A.groupid = '" + MemberID.Value + "' ORDER BY  [Name]  ASC";
        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                    "    <th>  </th> " +


                    "    <th> Name </th> " +


                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {



                htmltext += " <tr> " +

                                 "<td ><center><a onclick='BtnArchMemA(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Remove </a> </center></td>";

                htmltext += "   <td >" + Row[1].ToString() + "</td> " +
        


           " </tr>";


            }
        }
        else
        {
            htmltext = "No Data";
        }


        htmltext += "    </tbody> " +
                   " </table>";
        txtGroupsMem.Text = htmltext;
    }
    void RunGroups()
    {

        string ChecIN = "";
        string htmltext = "";
        DataTable table = new DataTable();
        string Getqry = "SELECT intid, groupname,fullName,CONVERT(varchar(16),uploaddate,106) FROM MsgGroupHolder WHERE churchid = '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + "' and isMandatory is null ORDER BY  intid DESC";
        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                    "    <th>  </th> " +


                    "    <th> Group Name </th> " +

                     "    <th >Created By</th> " +
                     "    <th > Date</th> " +

                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {

              
  
                htmltext += " <tr> " +

                                 "<td ><center><a onclick='BtnViewMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> View </a>	&nbsp;	&nbsp;	&nbsp;<a onclick='BtnArchMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Remove </a> </center></td>";

                htmltext += "   <td >" + Row[1].ToString() + "</td> " +
                "   <td >" + Row[2].ToString() + "</td> " +

                 "   <td >" + Row[3].ToString() + "</td> " +


           " </tr>";


            }
        }
        else
        {
            htmltext = "No Groups";
        }


        htmltext += "    </tbody> " +
                   " </table>";
        txtGroups.Text = htmltext;
    }

    protected void btnArchive_ServerClick(object sender, EventArgs e)
    {

        int complete = connect.SingleIntSQL("DELETE FROM  MsgGroupHolder  WHERE intid = '" + MemberID.Value + "' ");
        if (complete > 0)
        {
            connect.SingleIntSQL("DELETE FROM  MsgGroups WHERE groupid = '" + MemberID.Value + "'");
            RunGroups();
            RemoveNotie();
        }
    }

    void PopulateMember()
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

    protected void btnSaveGroup_ServerClick(object sender, EventArgs e)
    {
        if (txtGroupName.Value == "")
        {
            NotCompleteNotie();
            return;
        }

          int complete = connect.SingleIntSQL("INSERT INTO MsgGroupHolder (churchid,campus,groupname,fullName,uploaddate)VALUES ('" + Session["ChurchID"].ToString() + "', '" + Session["Campus"].ToString() + "','" + txtGroupName.Value + "','" + Session["FullName"].ToString() + "',GETDATE())");
          if (complete > 0)
          {
              RunGroups();
              txtGroupName.Value = "";
          }


    }
    protected void btnViewMember_ServerClick(object sender, EventArgs e)
    {
        RunAllMembers();
        PopulateMember();
        txtGrpName.InnerText = connect.SingleRespSQL("SELECT groupname FROm MsgGroupHolder WHERE intid = '" +  MemberID.Value + "'");
        lblMemberHeader.InnerText = txtGrpName.InnerText;
        DivHandleGroups.Visible = false;
        AddMembers.Visible = true;
    }
    protected void btnnBack_ServerClick(object sender, EventArgs e)
    {
        DivHandleGroups.Visible = true;
        AddMembers.Visible = false;
    }
    protected void btnSaveMember_ServerClick(object sender, EventArgs e)
    {
        if (PullParents.Value == "")
        {
            NotCompleteNotie();
            return;
        }
        int Exist = int.Parse(connect.SingleRespSQL("SELECT  count(intid) FROM MsgGroups WHERE groupid = '" + MemberID.Value + "' and MemberID = '" + PullParents.Value + "'"));
        if (Exist > 0)
        {
            ExistNotie();
            return;
        }


        int complete = connect.SingleIntSQL("INSERT INTO MsgGroups (groupid,MemberID)VALUES ('" + MemberID.Value + "', '" + PullParents.Value + "')");
        if (complete > 0)
        {
            RunAllMembers();

            PullParents.Value = "";
        }
    }
    protected void RemoveMemberA_ServerClick(object sender, EventArgs e)
    {
        int complete = connect.SingleIntSQL("DELETE FROM  MsgGroups  WHERE intid = '" + GroupID.Value + "' ");
        if (complete > 0)
        {
            RunAllMembers();
            RemoveNotie();
        }
    }
    protected void btnCan_ServerClick(object sender, EventArgs e)
    {
        btnDash.Visible = true;
        btnBack.Visible = false;
        MainDiv.Visible = true;
        DivHandleGroups.Visible = false;
    }
    protected void btnViewMsg_ServerClick(object sender, EventArgs e)
    {
        Server.Transfer("ViewMessages.aspx");
    }
    protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        Server.Transfer("Notification.aspx");
    }
    protected void btnAddTemplate_ServerClick(object sender, EventArgs e)
    {
        Server.Transfer("AddTemplate.aspx");
    }
}