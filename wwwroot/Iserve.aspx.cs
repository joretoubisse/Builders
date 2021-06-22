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

public partial class Iserve : System.Web.UI.Page
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


                if (MenuName == "iServe")
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
       
    }

   


    void RunMembers(string Iserver)
    {
        MainGrid.Visible = true;
        string htmltext = "";
        DataTable table = new DataTable();
        HideTab();
        GetIserve.Value = Iserver;

        if (Iserver == "Multimedia – Sound" || Iserver == "Multimedia – Video" || Iserver == "Hosts" || Iserver == "Hosts – Ushers" || Iserver == "Hosts – Protocol" || Iserver == "Hosts – Hospitality" || Iserver == "Builders Worship" || Iserver == "Builders Kidz")
        {
            btnShowChecklist.Visible = true;
            btnShowCheckHistory.Visible = true;
        }
        else
        {
            btnShowChecklist.Visible = false;
            btnShowCheckHistory.Visible = false;
        }


        string Getqry = "";
        if (Session["ShowAll"].ToString() == "Yes")
        {
             Getqry = "SELECT intid,  Name + ' ' + Surname ,Celno,iserve,CONVERT(varchar(16),iserveLastdate,106) FROM  Stats_form WHERE  ChurchID = '" + Session["ChurchID"].ToString() + "'  and iserve  = '" + Iserver + "' and IsActive = '1'   ORDER BY  Name + ' ' + Surname ASC";
        }
        else
        {
             Getqry = "SELECT intid,  Name + ' ' + Surname ,Celno,iserve,CONVERT(varchar(16),iserveLastdate,106) FROM  Stats_form WHERE campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "'  and iserve  = '" + Iserver + "' and IsActive = '1'   ORDER BY  Name + ' ' + Surname ASC";
        }

      
        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                    "    <th>  </th> " +
                    "    <th> Name</th> " +
                         "    <th> Cell No</th> " +
                    "    <th> Iserve </th> " +
                         "    <th> Last Action </th> " +
                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {


            lblServee.InnerText = Iserver;
            foreach (DataRow Row in table.Rows)
            {

           
                string ChecIN = "&nbsp;	&nbsp;	&nbsp;<a onclick='CheckIN(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Check IN </a> ";
                if (Session["Iserver"].ToString() != "1")
                {
                    ChecIN = "";
                }

                htmltext += " <tr> " +


                                 "<td ><center><a onclick='BtnViewMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> View History </a>" + ChecIN + " </center></td>";

                             htmltext +="    <td >" + Row[1].ToString() + "</td> " +
                             "   <td >" + Row[2].ToString() + "</td> " +

                              "   <td >" + Row[3].ToString() + "</td> " +

                                 "   <td >" + Row[4].ToString() + "</td> " +
                        " </tr>";


            }
        }
        else
        {
            lblServee.InnerText = "IServer";
            htmltext = "No Data";
        }


        htmltext += "    </tbody> " +
                   " </table>";
      tbTable.Text = htmltext;
    }

    void History()
    {
      
        string htmltext = "";
        DataTable table = new DataTable();
        string Getqry = "SELECT iserve,Convert(varchar(16),capturedDate,106),campus,timeCaptured,capturedby,comments FROM IserveCheck WHere memberid= '"+ MemberID.Value + "' ORDER BY intid DESC";
        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                  
                    "    <th> IServe</th> " +
                         "    <th> Date</th> " +
                    "    <th> Time </th> " +
                         "    <th> Comments </th> " +
                                 "    <th> Action By </th> " +
                                            "    <th> Campus </th> " +
                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {


          
            foreach (DataRow Row in table.Rows)
            {


         
                htmltext += " <tr> " +


                                 " <td >" + Row[0].ToString() + "</td> ";

                htmltext += "    <td >" + Row[1].ToString() + "</td> " +
                "   <td >" + Row[3].ToString() + "</td> " +

                 "   <td >" + Row[5].ToString() + "</td> " +

                    "   <td >" + Row[4].ToString() + "</td> " +
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
        tbtHistory.Text = htmltext;
    }


    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "Iserve.txt");
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




    protected void BtnSave_ServerClick(object sender, EventArgs e)
    {
        if (txtiServe.Value != "nones" || txtiServe.Value != "None")
        {

            RunMembers(txtiServe.Value);
        }
    }
    protected void btnCheckingIN_ServerClick(object sender, EventArgs e)
    {
        kt_content.Visible = false;
        MainGrid.Visible = false;
        DivCapture.Visible = true;

        DataTable People = connect.DTSQL("SELECT  Name + ' ' + Surname ,iserve FROM  Stats_form WHERE intid = '" + MemberID.Value + "'");
        if (People.Rows.Count > 0)
        {
            foreach (DataRow rows in People.Rows)
            {
                txtDepartMent.Value = rows[1].ToString();
                txtName.Value = rows[0].ToString();
            }
        }
    }
    protected void btnSaveCheckIN_ServerClick(object sender, EventArgs e)
    {
        int complete = connect.SingleIntSQL("INSERT INTO IserveCheck (Memberid,iserve,capturedDate,churchID,campus,timecaptured,capturedby,comments,notrainies)VALUES ('" + MemberID.Value + "', '" + txtiServe.Value + "',GETDATE(),'" + Session["ChurchID"].ToString() + "','" + Session["Campus"].ToString() + "','" + kt_timepicker_1.Value + "','" + Session["FullName"].ToString() + "','" + txtComments.Value + "','" + txtNoTrainies.Value + "')");
        if (complete > 0)
        {

            connect.SingleIntSQL("UPDATE Stats_form SET iserveLastdate = GETDATE() WHERE intid = '" + MemberID.Value + "' ");
            kt_content.Visible = true;
            MainGrid.Visible = true;
            DivCapture.Visible = false;
            RunMembers(txtiServe.Value);
            SaveNotie();
        }
    }
    protected void btnBacks_ServerClick(object sender, EventArgs e)
    {
        kt_content.Visible = true;
        MainGrid.Visible = true;
        DivCapture.Visible = false;
    }
    protected void btnViewhistor_ServerClick(object sender, EventArgs e)
    {
        MainDash.Visible = false;
        btnReturn.Visible = true;
        kt_content.Visible = false;
        MainGrid.Visible = false;
       
        HistoryIserver.Visible = true;
        lblMName.InnerText = connect.SingleRespSQL("SELECT name + ' ' +  surname  FROM Stats_form WHERE intid = '" + MemberID.Value + "'");
        History();
    }
    protected void btnReturn_ServerClick(object sender, EventArgs e)
    {
        MainDash.Visible = true;
        btnReturn.Visible = false;
        kt_content.Visible = true;
        MainGrid.Visible = true;

        HistoryIserver.Visible = false;
    }
    protected void btnCancel_ServerClick(object sender, EventArgs e)
    {
        Server.Transfer("Dashboard.aspx");
    }
    protected void btnWorship_ServerClick(object sender, EventArgs e)
    {
        #region Questions
        string Q1 = "SATURDAY REHEARSALS ATTENDED";
        string Q2 = "DRESS REHEARSALS ATTENDED";
        string Q3 = "TEAM BRIEFING HELD BEFORE THE SERVICE?";
        string Q4 = "DRESS CODE AND PRESENTATION";
        string Q5 = "SEATED IN THE PROPER PLACE";
        string Q6 = "START ON TIME";
        string Q7 = "FINISH ON TIME";
        string Q8 = "Comments";
        string Q9 = "";
        string Q10 = "";
        string Q11 = "";
        string Q12 = "";
        string Q13 = "";
        string Q14 = "";
        string Q15 = "";
        #endregion

        #region Answer
        string A1 = Worship1.Value;
        string A2 = Worship2.Value;
        string A3 = Worship3.Value;
        string A4 = Worship4.Value;
        string A5 = Worship5.Value;
        string A6 = Worship6.Value;

        string A7 = Worship7.Value;
        string A8 = Worship8.Value;
        string A9 = "";
        string A10 = "";

        string A11 = "";
        string A12 = "";
        string A13 = "";
        string A14 = "";
        string A15 = "";

        #endregion

        #region Handle SQL
        int QuestID = 0;
        string QuestionType = "Worship";
        string qry = "INSERT INTO ChurchChecklistholder (ChurchID,campus,fullName,uploadDate,questiontype) VALUES (@ChurchID,@campus,@fullName,GETDATE(),@questiontype) SELECT SCOPE_IDENTITY();";
        SqlParameter[] sp =
               {
                        new SqlParameter("@ChurchID", Session["ChurchID"].ToString()),
                        new SqlParameter("@campus", Session["Campus"].ToString()),
                        new SqlParameter("@fullName", Session["FullName"].ToString()),
                        new SqlParameter("@questiontype", QuestionType),
                      
                    };
        QuestID = int.Parse(connect.SingleIntQrySQL(sp, qry));
        if (QuestID > 0)
        {
            int complete = connect.SingleIntSQL("INSERT INTO ChurchChecklist (ChurchID ,Campus ,FullName ,UploadDate ,QuestionType,QuestionID,Q1,A1,Q2,A2,Q3,A3,Q4,A4,Q5,A5,Q6,A6,Q7,A7,Q8,A8,Q9,A9,Q10,A10,Q11,A11,Q12,A12,Q13,A13,Q14,A14,Q15,A15)" +
            @"VALUES ('" + Session["ChurchID"].ToString() + "', '" + Session["Campus"].ToString() + "','" + Session["FullName"].ToString() + "',GETDATE(),'" + QuestionType + "','" + QuestID + "' " +
            @",'" + Q1 + "','" + A1 + "' " +
            @",'" + Q2 + "','" + A2 + "' " +
            @",'" + Q3 + "','" + A3 + "' " +
            @",'" + Q4 + "','" + A4 + "' " +
            @",'" + Q5 + "','" + A5 + "' " +
            @",'" + Q6 + "','" + A6 + "' " +
            @",'" + Q7 + "','" + A7 + "' " +
            @",'" + Q8 + "','" + A8 + "' " +
            @",'" + Q9 + "','" + A9 + "' " +
            @",'" + Q10 + "','" + A10 + "' " +
            @",'" + Q11 + "','" + A11 + "' " +
            @",'" + Q12 + "','" + A12 + "' " +
            @",'" + Q13 + "','" + A13 + "' " +
            @",'" + Q14 + "','" + A14 + "' " +
            @",'" + Q15 + "','" + A15 + "')");
            if (complete > 0)
            {
                HideTab();
                SaveNotie();
            }
        }
        #endregion
    }

    protected void btnSaveHosts_ServerClick(object sender, EventArgs e)
    {

        #region Questions
        string Q1 = "VENUE AND YARD CLEANED";
        string Q2 = "TEAM BRIEFING HELD BEFORE THE SERVICE?";
        string Q3 = "DRESS CODE AND PRESENTATION CORRECT";
        string Q4 = "CHURCH ARTICLES AVAILABLE AND READY";
        string Q5 = "REFRESHMENTS FOR GUESTS ANS PASTORS PREPARED WHEN NECESSARY";
        string Q6 = "GUESTS WELCOMED AND SITTED PROPERLY";
        string Q7 = "VISIBILITY AND ALERTNESS DURING THE SERVICE";
        string Q8 = "COFFEE AND JUICE PREPARED ON TIME FOR VISITORS";
        string Q9 = "DATA COLECTED AND REPORTED";
        string Q10 = "OFFERING WELL RECEIVED AND SAFELY ESCORTED";
        string Q11 = "WAS PRAYER ATTENDED?";
        string Q12 = "Comments";
         string Q13 = "";
         string Q14 = "";
         string Q15 = "";
        #endregion

        #region Answer
        string A1 = Hosts1.Value;
        string A2 = Hosts2.Value;
        string A3 = Hosts3.Value;
        string A4 = Hosts4.Value;
        string A5 = Hosts5.Value;
        string A6 = Hosts6.Value;

        string A7 = Hosts7.Value;
        string A8 = Hosts8.Value;
        string A9 = Hosts9.Value;
        string A10 = Hosts10.Value;

        string A11 = Hosts11.Value;
        string A12 = Hosts12.Value;
        string A13 = "";
        string A14 = "";
        string A15  = "";
   
        #endregion

        #region Handle SQL
        int QuestID = 0;
        string QuestionType = "Hosts";
        string qry = "INSERT INTO ChurchChecklistholder (ChurchID,campus,fullName,uploadDate,questiontype) VALUES (@ChurchID,@campus,@fullName,GETDATE(),@questiontype) SELECT SCOPE_IDENTITY();";
        SqlParameter[] sp =
               {
                        new SqlParameter("@ChurchID", Session["ChurchID"].ToString()),
                        new SqlParameter("@campus", Session["Campus"].ToString()),
                        new SqlParameter("@fullName", Session["FullName"].ToString()),
                        new SqlParameter("@questiontype", QuestionType),
                      
                    };
        QuestID = int.Parse(connect.SingleIntQrySQL(sp, qry));
        if (QuestID > 0)
        {
            int complete = connect.SingleIntSQL("INSERT INTO ChurchChecklist (ChurchID ,Campus ,FullName ,UploadDate ,QuestionType,QuestionID,Q1,A1,Q2,A2,Q3,A3,Q4,A4,Q5,A5,Q6,A6,Q7,A7,Q8,A8,Q9,A9,Q10,A10,Q11,A11,Q12,A12,Q13,A13,Q14,A14,Q15,A15)" +
            @"VALUES ('" + Session["ChurchID"].ToString() + "', '" + Session["Campus"].ToString() + "','" + Session["FullName"].ToString() + "',GETDATE(),'" + QuestionType + "','" + QuestID + "' " +
            @",'" +  Q1 + "','" + A1 + "' " +
            @",'" + Q2 + "','" + A2 + "' " +
            @",'" + Q3 + "','" + A3 + "' " +
            @",'" + Q4 + "','" + A4 + "' " +
            @",'" + Q5 + "','" + A5 + "' " +
            @",'" + Q6 + "','" + A6 + "' " +
            @",'" + Q7 + "','" + A7 + "' " +
            @",'" + Q8 + "','" + A8 + "' " +
            @",'" + Q9 + "','" + A9 + "' " +
            @",'" + Q10 + "','" + A10 + "' " +
            @",'" + Q11 + "','" + A11 + "' " +
            @",'" + Q12 + "','" + A12 + "' " +
            @",'" + Q13 + "','" + A13 + "' " +
            @",'" + Q14 + "','" + A14 + "' " +
            @",'" + Q15 + "','" + A15 + "')");
            if (complete > 0)
            {
                HideTab();
                SaveNotie();
            }
        }
        #endregion
    }

    protected void btnSaveMulti_ServerClick(object sender, EventArgs e)
    {
        #region Questions
        string Q1 = "WERE REHEARSALS ATTENDED";
        string Q2 = "EQUIPMENT SET UP ON TIME";
        string Q3 = "TEAM BRIEFING HELD BEFORE THE SERVICE?";
        string Q4 = "SOUND CHECK DONE ON TIME";
        string Q5 = "VISITOR’S DVDS DONE ON TIME";
        string Q6 = "SALES DESK SET UP AND UP TO DATE";
        string Q7 = "SONG DISPLAY ACCURATE";
        string Q8 = "5 MIN TIMER STARTED ON TIME";
        string Q9 = "ANNOUNCEMENTS QUED CORRECTLY";
        string Q10 = "WAS THE SOUND QUALITY GOOD?";
        string Q11 = "Comments";
        string Q12 = "";
        string Q13 = "";
        string Q14 = "";
        string Q15 = "";
        #endregion

        #region Answer
        string A1 = Multi1.Value;
        string A2 = Multi2.Value;
        string A3 = Multi3.Value;
        string A4 = Multi4.Value;
        string A5 = Multi5.Value;
        string A6 = Multi6.Value;

        string A7 = Multi7.Value;
        string A8 = Multi8.Value;
        string A9 = Multi9.Value;
        string A10 = Multi10.Value;

        string A11 = Multi11.Value;
        string A12 = "";
        string A13 = "";
        string A14 = "";
        string A15 = "";

        #endregion

        #region Handle SQL
        int QuestID = 0;
        string QuestionType = "Multimedia";
        string qry = "INSERT INTO ChurchChecklistholder (ChurchID,campus,fullName,uploadDate,questiontype) VALUES (@ChurchID,@campus,@fullName,GETDATE(),@questiontype) SELECT SCOPE_IDENTITY();";
        SqlParameter[] sp =
               {
                        new SqlParameter("@ChurchID", Session["ChurchID"].ToString()),
                        new SqlParameter("@campus", Session["Campus"].ToString()),
                        new SqlParameter("@fullName", Session["FullName"].ToString()),
                        new SqlParameter("@questiontype", QuestionType),
                      
                    };
        QuestID = int.Parse(connect.SingleIntQrySQL(sp, qry));
        if (QuestID > 0)
        {
            int complete = connect.SingleIntSQL("INSERT INTO ChurchChecklist (ChurchID ,Campus ,FullName ,UploadDate ,QuestionType,QuestionID,Q1,A1,Q2,A2,Q3,A3,Q4,A4,Q5,A5,Q6,A6,Q7,A7,Q8,A8,Q9,A9,Q10,A10,Q11,A11,Q12,A12,Q13,A13,Q14,A14,Q15,A15)" +
            @"VALUES ('" + Session["ChurchID"].ToString() + "', '" + Session["Campus"].ToString() + "','" + Session["FullName"].ToString() + "',GETDATE(),'" + QuestionType + "','" + QuestID + "' " +
            @",'" + Q1 + "','" + A1 + "' " +
            @",'" + Q2 + "','" + A2 + "' " +
            @",'" + Q3 + "','" + A3 + "' " +
            @",'" + Q4 + "','" + A4 + "' " +
            @",'" + Q5 + "','" + A5 + "' " +
            @",'" + Q6 + "','" + A6 + "' " +
            @",'" + Q7 + "','" + A7 + "' " +
            @",'" + Q8 + "','" + A8 + "' " +
            @",'" + Q9 + "','" + A9 + "' " +
            @",'" + Q10 + "','" + A10 + "' " +
            @",'" + Q11 + "','" + A11 + "' " +
            @",'" + Q12 + "','" + A12 + "' " +
            @",'" + Q13 + "','" + A13 + "' " +
            @",'" + Q14 + "','" + A14 + "' " +
            @",'" + Q15 + "','" + A15 + "')");
            if (complete > 0)
            {
                HideTab();
                SaveNotie();
            }
        }
        #endregion
    }

    protected void btnSaveJesusKids_ServerClick(object sender, EventArgs e)
    {
        #region Questions
        string Q1 = "WELCOMING AREA READY AT 9:00AM";
        string Q2 = "EQUIPMENT SET UP ON TIME";
        string Q3 = "TEAM BRIEFING HELD BEFORE THE SERVICE?";
        string Q4 = "LEASSON PACKS READY ON TIME ";
        string Q5 = "VISITORS PACKS READY ON TIME";
        string Q6 = "SIGN IN FACILITY USED PROPERLY";
        string Q7 = "DRESS CODE AND PRESENTATION";
        string Q8 = "BUILDERS KIDZ SERVICE START ON TIME";
        string Q9 = "Comments";
        string Q10 = "";
        string Q11 = "";
        string Q12 = "";
        string Q13 = "";
        string Q14 = "";
        string Q15 = "";
        #endregion

        #region Answer
        string A1 = Kids1.Value;
        string A2 = Kids2.Value;
        string A3 = Kids3.Value;
        string A4 = Kids4.Value;
        string A5 = Kids5.Value;
        string A6 = Kids6.Value;

        string A7 = Kids7.Value;
        string A8 = Kids8.Value;
        string A9 = Kids9.Value;
        string A10 = "";

        string A11 = "";
        string A12 = "";
        string A13 = "";
        string A14 = "";
        string A15 = "";

        #endregion

        #region Handle SQL
        int QuestID = 0;
        string QuestionType = "Builders Kidz";
        string qry = "INSERT INTO ChurchChecklistholder (ChurchID,campus,fullName,uploadDate,questiontype) VALUES (@ChurchID,@campus,@fullName,GETDATE(),@questiontype) SELECT SCOPE_IDENTITY();";
        SqlParameter[] sp =
               {
                        new SqlParameter("@ChurchID", Session["ChurchID"].ToString()),
                        new SqlParameter("@campus", Session["Campus"].ToString()),
                        new SqlParameter("@fullName", Session["FullName"].ToString()),
                        new SqlParameter("@questiontype", QuestionType),
                      
                    };
        QuestID = int.Parse(connect.SingleIntQrySQL(sp, qry));
        if (QuestID > 0)
        {
            int complete = connect.SingleIntSQL("INSERT INTO ChurchChecklist (ChurchID ,Campus ,FullName ,UploadDate ,QuestionType,QuestionID,Q1,A1,Q2,A2,Q3,A3,Q4,A4,Q5,A5,Q6,A6,Q7,A7,Q8,A8,Q9,A9,Q10,A10,Q11,A11,Q12,A12,Q13,A13,Q14,A14,Q15,A15)" +
            @"VALUES ('" + Session["ChurchID"].ToString() + "', '" + Session["Campus"].ToString() + "','" + Session["FullName"].ToString() + "',GETDATE(),'" + QuestionType + "','" + QuestID + "' " +
            @",'" + Q1 + "','" + A1 + "' " +
            @",'" + Q2 + "','" + A2 + "' " +
            @",'" + Q3 + "','" + A3 + "' " +
            @",'" + Q4 + "','" + A4 + "' " +
            @",'" + Q5 + "','" + A5 + "' " +
            @",'" + Q6 + "','" + A6 + "' " +
            @",'" + Q7 + "','" + A7 + "' " +
            @",'" + Q8 + "','" + A8 + "' " +
            @",'" + Q9 + "','" + A9 + "' " +
            @",'" + Q10 + "','" + A10 + "' " +
            @",'" + Q11 + "','" + A11 + "' " +
            @",'" + Q12 + "','" + A12 + "' " +
            @",'" + Q13 + "','" + A13 + "' " +
            @",'" + Q14 + "','" + A14 + "' " +
            @",'" + Q15 + "','" + A15 + "')");
            if (complete > 0)
            {
                HideTab();
                SaveNotie();
            }
        }
        #endregion
    }




    void HideTab()
    {
        MainGrid.Visible = true;
        ChecklistWorship.Visible = false;
        ChecklistHosts.Visible = false;
        ChecklistMULTIMEDIA.Visible = false;
        ChecklistKids.Visible = false;
    }
    protected void btnShowCheckHistory_ServerClick(object sender, EventArgs e)
    {
        Session["GetIserve"] = GetIserve.Value;
        Server.Transfer("ViewChecklists.aspx");

    }
    protected void btnShowChecklist_ServerClick(object sender, EventArgs e)
    {
        MainGrid.Visible = false;
        ChecklistWorship.Visible = false;
        ChecklistHosts.Visible = false;
        ChecklistMULTIMEDIA.Visible = false;
        ChecklistKids.Visible = false;
  
        if (GetIserve.Value == "Multimedia – Sound")
        {
            ChecklistMULTIMEDIA.Visible = true;
        }
        else if (GetIserve.Value == "Multimedia – Video")
        {
            ChecklistMULTIMEDIA.Visible = true;
        }
        else if (GetIserve.Value == "Hosts")
        {
            ChecklistHosts.Visible = true;
        }
        else if (GetIserve.Value == "Hosts – Ushers")
        {
            ChecklistHosts.Visible = true;
        }
        else if (GetIserve.Value == "Hosts – Protocol")
        {
            ChecklistHosts.Visible = true;
        }
        else if (GetIserve.Value == "Hosts – Hospitality")
        {
            ChecklistHosts.Visible = true;
        }
        else if (GetIserve.Value == "Builders Worship")
        {
            ChecklistWorship.Visible = true;
        }
        else if (GetIserve.Value == "Builders Kidz")
        {
            ChecklistKids.Visible = true;
        }
    }

    protected void btnWorshipCancel_ServerClick(object sender, EventArgs e)
    {
        HideTab();
    }
}