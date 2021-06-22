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

public partial class Kids : System.Web.UI.Page
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
                DivAddMember.Visible = true;
            }
            else
            {
                if (Session["KidsRights"].ToString() == "1")
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


                if (MenuName.Contains("Builders Kidz"))
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

    void RunUSers()
    {

        string ChecIN = "";
        string htmltext = "";
        DataTable table = new DataTable();
        string Getqry = "SELECT intid,Name + ' '  +Surname,convert(varchar(16),DOB,106),Gender FROM Kids WHERE churchid = '" + Session["ChurchID"].ToString() + "'  and IsActive = '1' and campus = '" + Session["Campus"].ToString() + "' ORDER BY  Name + ' ' + Surname ASC";
        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                    "    <th>  </th> " +
              
               
                    "    <th> Name </th> " +

                     "    <th > Date of Birth</th> " +
                     "    <th > Gender</th> " +
    
                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {

                int CheckNewKid = int.Parse(connect.SingleRespSQL("SELECT  count(intid)   FROM  KidsCheckedIn  WHERE KidID = '" + Row[0].ToString() + "'"));
                if (CheckNewKid > 0)
                {
                    string PullStatus = connect.SingleRespSQL("SELECT  TOP(1) isChecked FROM  KidsCheckedIn  WHERE KidID = '" + Row[0].ToString() + "' ORDER by intid  DESC");
                    if (PullStatus == "Yes")
                    {

                        ChecIN = "&nbsp;	&nbsp;	&nbsp;<a onclick='BtnCheckOut(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Check OUT </a> ";
                       
                    }
                    else
                    {
                        ChecIN = "&nbsp;	&nbsp;	&nbsp;<a onclick='BtnCheckIN(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Check IN </a> ";
                    }

                }
                else
                {
                    ChecIN = "&nbsp;	&nbsp;	&nbsp;<a onclick='BtnCheckIN(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Check IN </a> ";
                }


                if (Session["KidsRights"].ToString() != "1")
                {
                    ChecIN = "";
                }

             

                htmltext += " <tr> " +

                                 "<td ><center><a onclick='BtnViewMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> View </a>	&nbsp;	&nbsp;	&nbsp;<a onclick='BtnArchMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Remove </a> " + ChecIN + " </center></td>";
                            
                             htmltext +="   <td >" + Row[1].ToString() + "</td> " +
                             "   <td >" + Row[2].ToString() + "</td> " +

                              "   <td >" + Row[3].ToString() + "</td> " +
        

                        " </tr>";


            }
        }
        else
        {
            htmltext = "No Builders Kidz";
        }


        htmltext += "    </tbody> " +
                   " </table>";
        tbTable.Text = htmltext;
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


    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "Kids.txt");
        #region Local
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, true))
        {
            writer.WriteLine(msg);
        }
        #endregion
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
        Session["KidsID"] = MemberID.Value;
        Server.Transfer("ViewKids.aspx");
    }

    protected void btnCancel_ServerClick(object sender, EventArgs e)
    {
        RunUSers();
        ReturnDash.Visible = true;
        btnBack.Visible = false;
        txtSurname.Value = "";
        txtName.Value = "";
        txtDOB.Value = "";
        txtSurname.Value = "";
        txtAddress.Value = "";
        txtCellNo.Value = "";
        txtMomName.Value = "";
        txtMomCell.Value = "";
        txtDadCell.Value = "";
        txtDadName.Value = "";
        DivKids.Visible = false;
        DivGridKids.Visible = true;
    }

    protected void btnArchive_ServerClick(object sender, EventArgs e)
    {

        int complete = connect.SingleIntSQL("UPDATE  Kids SET IsActive = '0' ,lastupdateby = '" + Session["FullName"].ToString() + "' ,LastUpdateDate = GETDATE() WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and intid = '" + MemberID.Value + "' ");
        if (complete > 0)
        {
            RunUSers();
            RemoveNotie();
        }
    }


    void populateParents()
    {


        DataTable table = connect.DTSQL("SELECT intid, Name + ' ' + Surname + ' - ' + Celno as [Name]  FROM  Stats_Form WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + "' and IsActive = '1' order by name asc");
        CmdMom.DataSource = table;
        CmdMom.DataTextField = "Name";
        CmdMom.DataValueField = "intid";
        CmdMom.DataBind();
      
      CmdMom.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select", "None"));


        CmdDad.DataSource = table;
        CmdDad.DataTextField = "Name";
        CmdDad.DataValueField = "intid";
        CmdDad.DataBind();
       CmdDad.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select", "None"));
    }

    protected void BtnSave_ServerClick(object sender, EventArgs e)
    {

        string isOnSystem = CmdOnSystem.SelectedValue.ToString();
        string MomsName = "";
        string MomCellNo = "";
        string DadsName = "";
        string DadCellNo = "";



        if ((txtSurname.Value == "") || (txtName.Value == "") || (txtAddress.Value == "") && (CmdGender.Value == "None") || (txtDOB.Value == "") || (isOnSystem == "None"))
        {
            NotCompleteNotie();
            return;
        }

        if (isOnSystem == "Yes")
        {
            DadCellNo = "";
            MomCellNo = "";
            MomsName = CmdMom.Value;
            DadsName = CmdDad.Value;
        }
        else
        {
            DadCellNo = txtDadCell.Value;
            MomCellNo = txtMomCell.Value;
            MomsName = txtMomName.Value;
            DadsName = txtDadName.Value;
        
        }


        int complete = connect.SingleIntSQL("INSERT INTO Kids (surname,Name,Gender,Address,DOB,CellNo,IsSystem,MomName,MomCell,DadName,DadCell,createdby,createddate,ChurchID,IsActive,campus)VALUES ('" + txtSurname.Value + "', '" + txtName.Value + "','" + CmdGender.Value + "','" + txtAddress.Value + "','" + txtDOB.Value + "','" + txtCellNo.Value + "','" + isOnSystem + "','" + MomsName + "', '" + MomCellNo + "','" + DadsName + "','" + DadCellNo + "','" + Session["FullName"].ToString() + "',GETDATE(),'" + Session["ChurchID"].ToString() + "','1','" + Session["Campus"].ToString() + "')");
        if (complete > 0)
        {
            txtSurname.Value = "";
            txtName.Value = "";
            txtDOB.Value = "";
            txtSurname.Value = "";
            txtAddress.Value = "";
            txtCellNo.Value = "";
            txtMomName.Value = "";
            txtMomCell.Value = "";
            txtDadCell.Value = "";
            txtDadName.Value = "";
            RunUSers();
            SaveNotie();
        }



    }

    protected void AddKids_ServerClick(object sender, EventArgs e)
    {

        ReturnDash.Visible = false;
        btnBack.Visible = true;
        DivKids.Visible = true;
        DivGridKids.Visible = false;
    }
    protected void CmdOnSystem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmdOnSystem.SelectedValue.ToString() == "Yes")
        {
            //populateParents();
            PrentNotInSystem.Visible = false;
            PrentsInSystem.Visible = true;
        }
        else
        {
            PrentNotInSystem.Visible = true;
            PrentsInSystem.Visible = false;
        }
    }
    protected void btnCheckingIN_ServerClick(object sender, EventArgs e)
    {
        NotSame.Visible = true;
        DicSamePerson.Visible = false;
        DivCheckIDText.Visible = false;
        lblCheckIN.InnerText = "Check IN";
        DivGridKids.Visible = false;
        DivCheckingIN.Visible = true;
        btnSaveCheckIN.Visible = true;
        
        DivParentOnSystem.Visible = false;
        txtSurnames.Value = "";
        NotONSystem.Visible = false;
        DivVisitor.Visible = false;
        CmdVisitor.Value = "none";
        CmdGurp.Value = "none";
       // PopulateParents();


        //int complete = connect.SingleIntSQL("INSERT INTO KidsCheckedIn (KidID,isChecked,checkedDate,churchID,createdby,createdDate)VALUES ('" + MemberID.Value + "', 'Yes',GETDATE(),'" + Session["ChurchID"].ToString() + "','" + Session["FullName"].ToString() + "',GETDATE())");
        //if (complete > 0)
        //{
        //    RunUSers();
        //    SaveNotie();
        //}
    }
    protected void btnCheckingOut_ServerClick(object sender, EventArgs e)
    {
        NotSame.Visible = false;
        DicSamePerson.Visible = true;
        txtCheckedIn.Value = connect.SingleRespSQL("SELECT  TOP(1) ParentName FROM KidsCheckedIn WHERE KidID = '" + MemberID.Value + "'  ORDER BY intid DESC");
        DivCheckIDText.Visible = true;
        lblCheckIN.InnerText = "Check OUT";
        btnSaveCheckIN.Visible = false;
        btnSaveCheckOUT.Visible = true;
        DivGridKids.Visible = false;
        DivCheckingIN.Visible = true;
        DivParentOnSystem.Visible = false;
        txtSurnames.Value = "";
        NotONSystem.Visible = false;
        DivVisitor.Visible = false;
       
        CmdVisitor.Value = "none";
        CmdGurp.Value = "none";
       // PopulateParents();
        //int complete = connect.SingleIntSQL("INSERT INTO KidsCheckedIn (KidID,isChecked,checkedDate,churchID,createdby,createdDate)VALUES ('" + MemberID.Value + "', 'No',GETDATE(),'" + Session["ChurchID"].ToString() + "','" + Session["FullName"].ToString() + "',GETDATE())");
        //if (complete > 0)
        //{
        //    RunUSers();
        //    SaveNotie();
        //}
    }
    protected void cmdParentONSystem_SelectedIndexChanged(object sender, EventArgs e)
    {
      

       
           
        if (cmdParentONSystem.SelectedValue.ToString() == "Yes")
        {
            DivVisitor.Visible = false;
            DivParentOnSystem.Visible = true;
            NotONSystem.Visible = false;
        }
        else if (cmdParentONSystem.SelectedValue.ToString() == "No")
        {
            DivVisitor.Visible = true;
            DivParentOnSystem.Visible = false;
            NotONSystem.Visible = true;
        }
        else
        {
            DivVisitor.Visible = false;
            DivParentOnSystem.Visible = false;
            NotONSystem.Visible = false;
        }

    }
    protected void btnSaveCheckIN_ServerClick(object sender, EventArgs e)
    {

        string Name = "";
        string CellNo = "";
        string Visitor = "";
        if (CmdGurp.Value == "none" )
        {
            NotCompleteNotie();
            return;
        }

        if (cmdParentONSystem.SelectedValue.ToString() == "Yes")
        {
            Visitor = "Member";
            if (PullParents.Value == "" || PullParents.Value == "None")
            {
                NotCompleteNotie();
                return;
            }
            else
            {

                DataTable GetInfo = connect.DTSQL("SELECT  name + ' ' + Surname as [Name],celno FROM  Stats_Form WHERE intid = '" + PullParents.Value + "'");
                if (GetInfo.Rows.Count > 0)
                {
                    foreach (DataRow rows in GetInfo.Rows)
                    {
                        Name = rows[0].ToString();
                        
                        CellNo = rows[1].ToString();
                    }
                }

           
            }
        }
        else if (cmdParentONSystem.SelectedValue.ToString() == "No")
        {
            if (CmdVisitor.Value == "none")
            {
                NotCompleteNotie();
                return;
            }


            if (CmdVisitor.Value == "Yes")
            {
                Visitor = "Visitor";
            }
            else
            {
                Visitor = "Not a Visitor";
            }

            if (txtPName.Value == "" || txtPCell.Value == "" || txtSurnames.Value == "")
            {
                NotCompleteNotie();
                return;
            }
            else
            {
                Name = txtPName.Value + " " + txtSurnames.Value;
                CellNo = txtPCell.Value;
            }

        }
        else if (cmdParentONSystem.SelectedValue.ToString() == "None")
        {
            NotCompleteNotie();
            return;
        }

       


        int complete = connect.SingleIntSQL("INSERT INTO KidsCheckedIn (KidID,isChecked,checkedDate,churchID,createdby,createdDate,ParentName,ParentCell,parentype,IsVisitor)VALUES ('" + MemberID.Value + "', 'Yes',GETDATE(),'" + Session["ChurchID"].ToString() + "','" + Session["FullName"].ToString() + "',GETDATE(),'" + Name + "','" + CellNo + "','" + CmdGurp.Value + "','" +  Visitor + "')");
        if (complete > 0)
        {

            if (CmdVisitor.Value == "Yes")
            {
                string MemberNo = Session["MemberNo"].ToString() + "_" + RandomString(5);
                connect.SingleIntSQL("INSERT INTO Stats_Form (name,Surname,ChurchID,MemberNo,Campus,MemberType,IsActive,Celno,CapturedBy,DateCaptured)VALUES ('" + txtPName.Value + "','" + txtSurnames.Value + "', '" + Session["ChurchID"].ToString() + "','" + MemberNo + "','" + Session["Campus"].ToString() + "','Visitor','1','" + CellNo + "','" + Session["FullName"].ToString() + "',GETDATE())");
            }
            txtSurnames.Value = "";
            DivParentOnSystem.Visible = false;
            DivVisitor.Visible = false;
            CmdVisitor.Value = "none";
            CmdGurp.Value = "none";
            txtPName.Value = "";
            NotONSystem.Visible = false;
            txtPCell.Value = "";
            DivCheckingIN.Visible = false;
            btnSaveCheckOUT.Visible = false;
            DivGridKids.Visible = true;
            DivCheckingIN.Visible = false;
            PopulateParents();
            RunUSers();
            SaveNotie();
        }
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

    protected void btnSaveCheckOUT_ServerClick(object sender, EventArgs e)
    {

        string Name = "";
        string CellNo = "";
        string Visitor = "";




        if (CmdSameCareTaker.SelectedValue.ToString() == "None")
        {
            NotCompleteNotie();
            return;
        }


        if (CmdSameCareTaker.SelectedValue.ToString() == "Yes")
        {

            Name = connect.SingleRespSQL("SELECT  TOP(1) ParentName  FROM KidsCheckedIn WHERE KidID = '" + MemberID.Value + "'  ORDER BY intid DESC");
            CellNo = connect.SingleRespSQL("SELECT  TOP(1) ParentCell  FROM KidsCheckedIn WHERE KidID = '" + MemberID.Value + "'  ORDER BY intid DESC");
            Visitor = connect.SingleRespSQL("SELECT  TOP(1) IsVisitor  FROM KidsCheckedIn WHERE KidID = '" + MemberID.Value + "'  ORDER BY intid DESC");
            string Tye = connect.SingleRespSQL("SELECT  TOP(1) parentype  FROM KidsCheckedIn WHERE KidID = '" + MemberID.Value + "'  ORDER BY intid DESC");
            int complete = connect.SingleIntSQL("INSERT INTO KidsCheckedIn (KidID,isChecked,checkedDate,churchID,createdby,createdDate,ParentName,ParentCell,parentype,IsVisitor)VALUES ('" + MemberID.Value + "', 'No',GETDATE(),'" + Session["ChurchID"].ToString() + "','" + Session["FullName"].ToString() + "',GETDATE(),'" + Name + "','" + CellNo + "','" + Tye + "','" + Visitor + "')");
            if (complete > 0)
            {

       
                DivParentOnSystem.Visible = false;
                txtSurnames.Value = "";
                NotONSystem.Visible = false;
                DivVisitor.Visible = false;
                CmdVisitor.Value = "none";
                CmdGurp.Value = "none";
                txtPName.Value = "";
                txtPCell.Value = "";
                DivCheckingIN.Visible = false;
                btnSaveCheckOUT.Visible = false;
                DivGridKids.Visible = true;
                DivCheckingIN.Visible = false;
                PopulateParents();
                RunUSers();
                SaveNotie();
            }
        }
        else
        {

            if (CmdGurp.Value == "none")
            {
                NotCompleteNotie();
                return;
            }

            if (cmdParentONSystem.SelectedValue.ToString() == "Yes")
            {
                Visitor = "Member";
                if (PullParents.Value == "" || PullParents.Value == "None")
                {
                    NotCompleteNotie();
                    return;
                }
                else
                {

                    DataTable GetInfo = connect.DTSQL("SELECT  name + ' ' + Surname as [Name],celno FROM  Stats_Form WHERE intid = '" + PullParents.Value + "'");
                    if (GetInfo.Rows.Count > 0)
                    {
                        foreach (DataRow rows in GetInfo.Rows)
                        {
                            Name = rows[0].ToString();
                            CellNo = rows[1].ToString();
                        }
                    }


                }
            }
            else if (cmdParentONSystem.SelectedValue.ToString() == "No")
            {
                if (CmdVisitor.Value == "none")
                {
                    NotCompleteNotie();
                    return;
                }


                if (CmdVisitor.Value == "Yes")
                {
                    Visitor = "Visitor";
                }
                else
                {
                    Visitor = "Not a Visitor";
                }

                if (txtPName.Value == "" || txtPCell.Value == "" || txtSurnames.Value == "")
                {
                    NotCompleteNotie();
                    return;
                }
                else
                {
                    Name = txtPName.Value + " " + txtSurnames.Value;
                    CellNo = txtPCell.Value;
                }

            }
            else if (cmdParentONSystem.SelectedValue.ToString() == "None")
            {
                NotCompleteNotie();
                return;
            }

            int complete = connect.SingleIntSQL("INSERT INTO KidsCheckedIn (KidID,isChecked,checkedDate,churchID,createdby,createdDate,ParentName,ParentCell,parentype,IsVisitor)VALUES ('" + MemberID.Value + "', 'No',GETDATE(),'" + Session["ChurchID"].ToString() + "','" + Session["FullName"].ToString() + "',GETDATE(),'" + Name + "','" + CellNo + "','" + CmdGurp.Value + "','" + Visitor + "')");
            if (complete > 0)
            {

                if (CmdVisitor.Value == "Yes")
                {
                    string MemberNo = Session["MemberNo"].ToString() + "_" + RandomString(5);
                    connect.SingleIntSQL("INSERT INTO Stats_Form (name,Surname,ChurchID,MemberNo,Campus,MemberType,IsActive,Celno,CapturedBy,DateCaptured)VALUES ('" + txtPName.Value + "','" + txtSurnames.Value + "', '" + Session["ChurchID"].ToString() + "','" + MemberNo + "','" + Session["Campus"].ToString() + "','Visitor','1','" + CellNo + "','" + Session["FullName"].ToString() + "',GETDATE())");
                }
                DivParentOnSystem.Visible = false;
                txtSurnames.Value = "";
                NotONSystem.Visible = false;
                DivVisitor.Visible = false;
                CmdVisitor.Value = "none";
                CmdGurp.Value = "none";
                txtPName.Value = "";
                txtPCell.Value = "";
                DivCheckingIN.Visible = false;
                btnSaveCheckOUT.Visible = false;
                DivGridKids.Visible = true;
                DivCheckingIN.Visible = false;
                PopulateParents();
                RunUSers();
                SaveNotie();
            }
        }
    }
    protected void btnCncel_ServerClick(object sender, EventArgs e)
    {
        DivCheckingIN.Visible = false;
        btnSaveCheckOUT.Visible = false;
        DivGridKids.Visible = true;
        DivCheckingIN.Visible = false;
    }
    protected void CmdSameCareTaker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CmdSameCareTaker.SelectedValue.ToString() == "Yes")
        {
            DicSamePerson.Visible = true;
            NotSame.Visible = false;
      
        }
        else if (CmdSameCareTaker.SelectedValue.ToString() == "No")
        {
            DicSamePerson.Visible = true;
            NotSame.Visible = true;
        }

    }
    //New codes button
    protected void btnSaveCheckINS_ServerClick(object sender, EventArgs e)
    {
        PopulateParents();
        PullParents.Visible = true;
    }
        protected void btnBack_ServerClick(object sender, EventArgs e)
    {
        RunUSers();
        ReturnDash.Visible = true;
        btnBack.Visible = false;
        txtSurname.Value = "";
        txtName.Value = "";
        txtDOB.Value = "";
        txtSurname.Value = "";
        txtAddress.Value = "";
        txtCellNo.Value = "";
        txtMomName.Value = "";
        txtMomCell.Value = "";
        txtDadCell.Value = "";
        txtDadName.Value = "";
        DivKids.Visible = false;
        DivGridKids.Visible = true;
    }
}