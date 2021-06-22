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

public partial class ViewChecklists : System.Web.UI.Page
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


                if (MenuName == "iServe")
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



    void RunOnLoad()
    {
        lblName.InnerText = Session["FName"].ToString();
        Loadfooter.Text = Session["Footer"].ToString();
        if (ConfigurationManager.AppSettings["HideFields"].ToString() == "1")
        {
            if (Session["Iserver"].ToString() == "1")
            {

                IsAdmin.Value = "1";
                DivAddMember.Visible = false;
            }


        }
        else
        {
            IsAdmin.Value = "1";

            DivAddMember.Visible = false;
        }




        if (Session["AccessRights"].ToString() == "Iserve Teams")
        {
            kt_header_menu_wrapper.Visible = false;
        }

        RunMenus();
        RunMembers();
    }

   


    void RunMembers()
    {

        string htmltext = "";
        DataTable table = new DataTable();

        lblHolderChecklist.InnerText = Session["GetIserve"].ToString();
        string Getqry = "";
        if (Session["GetIserve"].ToString() == "Multimedia – Sound")
        {
            Getqry = "SELECT intid, Fullname,CONVERT(varchar(16),uploadDate,106) ,questionType FROM ChurchChecklistholder WHERE Campus = '" + Session["Campus"].ToString() + "' and churchid = '" + Session["ChurchID"].ToString() + "' and questionType = 'Multimedia' ORDER by intid DESC";
        }
        else if (Session["GetIserve"].ToString() == "Multimedia – Video")
        {
            Getqry = "SELECT intid, Fullname,CONVERT(varchar(16),uploadDate,106) ,questionType FROM ChurchChecklistholder WHERE Campus = '" + Session["Campus"].ToString() + "' and churchid = '" + Session["ChurchID"].ToString() + "' and questionType = 'Multimedia' ORDER by intid DESC";
        }
        else if (Session["GetIserve"].ToString() == "Hosts")
        {
            Getqry = "SELECT intid, Fullname,CONVERT(varchar(16),uploadDate,106) ,questionType FROM ChurchChecklistholder WHERE Campus = '" + Session["Campus"].ToString() + "' and churchid = '" + Session["ChurchID"].ToString() + "' and questionType = 'Hosts' ORDER by intid DESC";
        }
        else if (Session["GetIserve"].ToString() == "Hosts – Ushers")
        {
            Getqry = "SELECT intid, Fullname,CONVERT(varchar(16),uploadDate,106) ,questionType FROM ChurchChecklistholder WHERE Campus = '" + Session["Campus"].ToString() + "' and churchid = '" + Session["ChurchID"].ToString() + "' and questionType = 'Hosts' ORDER by intid DESC";
        }
        else if (Session["GetIserve"].ToString() == "Hosts – Protocol")
        {
            Getqry = "SELECT intid, Fullname,CONVERT(varchar(16),uploadDate,106) ,questionType FROM ChurchChecklistholder WHERE Campus = '" + Session["Campus"].ToString() + "' and churchid = '" + Session["ChurchID"].ToString() + "' and questionType = 'Hosts' ORDER by intid DESC";
        }
        else if (Session["GetIserve"].ToString() == "Hosts – Hospitality")
        {
            Getqry = "SELECT intid, Fullname,CONVERT(varchar(16),uploadDate,106) ,questionType FROM ChurchChecklistholder WHERE Campus = '" + Session["Campus"].ToString() + "' and churchid = '" + Session["ChurchID"].ToString() + "' and questionType = 'Hosts' ORDER by intid DESC";
        }
        else if (Session["GetIserve"].ToString() == "Builders Worship")
        {
            Getqry = "SELECT intid, Fullname,CONVERT(varchar(16),uploadDate,106) ,questionType FROM ChurchChecklistholder WHERE Campus = '" + Session["Campus"].ToString() + "' and churchid = '" + Session["ChurchID"].ToString() + "' and questionType = 'Worship' ORDER by intid DESC";
        }
        else if (Session["GetIserve"].ToString() == "Builders Kidz")
        {
            Getqry = "SELECT intid, Fullname,CONVERT(varchar(16),uploadDate,106) ,questionType FROM ChurchChecklistholder WHERE Campus = '" + Session["Campus"].ToString() + "' and churchid = '" + Session["ChurchID"].ToString() + "' and questionType = 'BuildersKidz' ORDER by intid DESC";
        }

        table = connect.DTSQL(Getqry);


        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                    "    <th>  </th> " +
                    "    <th> Last action by</th> " +
                    "    <th> Date </th> " +

                     "    <th > Type</th> " +
                   
                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {




                htmltext += " <tr> " +

                                 "<td ><center><a onclick='BtnViewMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> View </a> &nbsp;	&nbsp;	&nbsp;</center></td>" +

                             "   <td >" + Row[1].ToString() + "</td> " +
                             "   <td >" + Row[2].ToString() + "</td> " +

                              "   <td >" + Row[3].ToString() + "</td> " +
                      

                        " </tr>";


            }
        }
        else
        {
            htmltext = "No Data";
        }


        htmltext += "    </tbody> " +
                   " </table>";
        tbtChecklistHeader.Text = htmltext;
    }


    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "MemberApp.txt");
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
    void MemberConverted()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ ConvertNotieAlert(); },550);", true);

    }
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




    protected void btnViewChecklist_ServerClick(object sender, EventArgs e)
    {
       // CheckListID

        FirstBack.Visible = false;
        btnReturn.Visible = true;

        kt_content.Visible = false;
        DivFullDisplay.Visible = true;
        string htmltext = "";
        DataTable table = new DataTable();

        lblHeader.InnerText = Session["GetIserve"].ToString();
        string Getqry = "";
        Getqry = "SELECT Q1,A1,Q2,A2,Q3,A3,Q4,A4,Q5,A5,Q6,A6,Q7,A7,Q8,A8,Q9,A9,Q10,A10,Q11,A11,Q12,A12,Q13,A13,Q14,A14,Q15,A15 FROM ChurchChecklist WHERE  QuestionID = '" + CheckListID.Value + "'";

        logthefile(Getqry);
        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                      "<thead>" +
                      "  <tr>" +
                         "    <th>Questions</th> " +
                         "    <th>Answers</th> " +

                      "  </tr> " +
                      "</thead> " +
                      "<tbody> ";

     
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {


                if (Session["GetIserve"].ToString().Contains("Multimedia"))
                {
                    Div12.Visible = false;
                    Label1.InnerText = Row[0].ToString();
                    Label2.InnerText = Row[2].ToString();
                    Label3.InnerText = Row[4].ToString();
                    Label4.InnerText = Row[6].ToString();
                    Label5.InnerText = Row[8].ToString();
                    Label6.InnerText = Row[10].ToString();
                    Label7.InnerText = Row[12].ToString();
                    Label8.InnerText = Row[14].ToString();
                    Label9.InnerText = Row[16].ToString();
                    Label10.InnerText = Row[18].ToString();
                    Label11.InnerText = Row[20].ToString();
                   // Label12.InnerText = Row[22].ToString();





                    Text1.Value = Row[1].ToString();
                    Text2.Value = Row[3].ToString();
                    Text3.Value = Row[5].ToString();
                    Text4.Value = Row[7].ToString();
                    Text5.Value = Row[9].ToString();
                    Text6.Value = Row[11].ToString();
                    Text7.Value = Row[13].ToString();
                    Text8.Value = Row[15].ToString();
                    Text9.Value = Row[17].ToString();
                    Text10.Value = Row[19].ToString();
                    Text11.Value = Row[21].ToString();
                   // Text12.Value = Row[23].ToString();
              
                }
                else  if (Session["GetIserve"].ToString().Contains("Hosts"))
                {
                    Label1.InnerText = Row[0].ToString();
                    Label2.InnerText = Row[2].ToString();
                    Label3.InnerText = Row[4].ToString();
                    Label4.InnerText = Row[6].ToString();
                    Label5.InnerText = Row[8].ToString();
                    Label6.InnerText = Row[10].ToString();
                    Label7.InnerText = Row[12].ToString();
                    Label8.InnerText = Row[14].ToString();
                    Label9.InnerText = Row[16].ToString();
                    Label10.InnerText = Row[18].ToString();
                    Label11.InnerText = Row[20].ToString();
                    Label12.InnerText = Row[22].ToString();





                    Text1.Value = Row[1].ToString();
                    Text2.Value = Row[3].ToString();
                    Text3.Value = Row[5].ToString();
                    Text4.Value = Row[7].ToString();
                    Text5.Value = Row[9].ToString();
                    Text6.Value = Row[11].ToString();
                    Text7.Value = Row[13].ToString();
                    Text8.Value = Row[15].ToString();
                    Text9.Value = Row[17].ToString();
                    Text10.Value = Row[19].ToString();
                    Text11.Value = Row[21].ToString();
                    Text12.Value = Row[23].ToString();
              
                }
                else if (Session["GetIserve"].ToString() == "Builders Worship")
                {
                    Div9.Visible = false;
                    Div10.Visible = false;
                    Div11.Visible = false;
                    Div12.Visible = false;
     
                    Label1.InnerText = Row[0].ToString();
                    Label2.InnerText = Row[2].ToString();
                    Label3.InnerText = Row[4].ToString();
                    Label4.InnerText = Row[6].ToString();
                    Label5.InnerText = Row[8].ToString();
                    Label6.InnerText = Row[10].ToString();
                    Label7.InnerText = Row[12].ToString();
                    Label8.InnerText = Row[14].ToString();
                    Label9.InnerText = Row[16].ToString();
                    Label10.InnerText = Row[18].ToString();
                    Label11.InnerText = Row[20].ToString();
                   // Label12.InnerText = Row[22].ToString();





                    Text1.Value = Row[1].ToString();
                    Text2.Value = Row[3].ToString();
                    Text3.Value = Row[5].ToString();
                    Text4.Value = Row[7].ToString();
                    Text5.Value = Row[9].ToString();
                    Text6.Value = Row[11].ToString();
                    Text7.Value = Row[13].ToString();
                    Text8.Value = Row[15].ToString();
                    Text9.Value = Row[17].ToString();
                    Text10.Value = Row[19].ToString();
                    Text11.Value = Row[21].ToString();
                   // Text12.Value = Row[23].ToString();
              
                }
                else if (Session["GetIserve"].ToString() == "Builders Kidz")
                {
                    Div10.Visible = false;
                    Div11.Visible = false;
                    Div12.Visible = false;
                    Label1.InnerText = Row[0].ToString();
                    Label2.InnerText = Row[2].ToString();
                    Label3.InnerText = Row[4].ToString();
                    Label4.InnerText = Row[6].ToString();
                    Label5.InnerText = Row[8].ToString();
                    Label6.InnerText = Row[10].ToString();
                    Label7.InnerText = Row[12].ToString();
                    Label8.InnerText = Row[14].ToString();
                    Label9.InnerText = Row[16].ToString();
                    Label10.InnerText = Row[18].ToString();
                    Label11.InnerText = Row[20].ToString();
                    // Label12.InnerText = Row[22].ToString();





                    Text1.Value = Row[1].ToString();
                    Text2.Value = Row[3].ToString();
                    Text3.Value = Row[5].ToString();
                    Text4.Value = Row[7].ToString();
                    Text5.Value = Row[9].ToString();
                    Text6.Value = Row[11].ToString();
                    Text7.Value = Row[13].ToString();
                    Text8.Value = Row[15].ToString();
                    Text9.Value = Row[17].ToString();
                    Text10.Value = Row[19].ToString();
                    Text11.Value = Row[21].ToString();
                    // Text12.Value = Row[23].ToString();
                }
   
   

              


            }
        }
        else
        {
            htmltext = "No Data";
        }


        htmltext += "    </tbody> " +
                   " </table>";
      //  txtFullHistory.Text = htmltext;

    }
    protected void btnReturn_ServerClick(object sender, EventArgs e)
    {
        FirstBack.Visible = true;
        btnReturn.Visible = false;
        kt_content.Visible = true;
        DivFullDisplay.Visible = false;




    }
    protected void FirstBack_ServerClick(object sender, EventArgs e)
    {
        Server.Transfer("Iserve.aspx");
    }
}