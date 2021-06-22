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

public partial class ViewMembers : System.Web.UI.Page
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

                string Menustream = "";
                if (Session["MemType"].ToString() == "Visitor")
                {
                    Menustream = "Visitors";
                }
                else if (Session["MemType"].ToString() == "Evangelist")
                {
                    Menustream = "Evangelist";
                }
                else if (Session["MemType"].ToString() == "NewMember")
                {
                    Menustream = "New Membership Applications";
                }
                else
                {
                    Menustream = "Members";
                }

                


                if (MenuName == Menustream)
                {
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

    void PopulateCampus()
    {
        DataTable table = connect.DTSQL("SELECT  campus,campus FROM Campus WHERE churchid = '" + Session["ChurchID"].ToString() + "'");
        CmdCampus.DataSource = table;
        CmdCampus.DataTextField = "campus";
        CmdCampus.DataValueField = "campus";
        CmdCampus.DataBind();
        CmdCampus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select", "None"));
    }




    void PopulateDemographics()
    {

        DataTable table = new DataTable();

        string sqlQuery = @"SELECT  idnumber, Surname, Name, Knownas, Gender, Case when DOB = '1900-01-01 00:00:00.000'  THEN null else CONVERT(varchar(16),DOB,106) END as [DAte], Ward, MaritalStat, Occupation,EmployerCheck, 
                            Skills, ResidentAdd, TelnoH, TelnoW,Fax,Celno, EmailAdd, Ministries,MemberNo,ChurchID,CapturedBy,
                            DateCaptured,FFS,SpiritualGifts, Case when DateMemberObtained = '1900-01-01 00:00:00.000'  THEN null else CONVERT(varchar(16),DateMemberObtained,106) END as [DAte],ChurchStatus,ChurchInvolvement,CONVERT(varchar(16),marrieddate,106) ,churchzone,membertype,sms,newbeliever,iconnect,baptized,iserve,comments,GrowthPath,DATEDIFF(YEAR, '0:0', getdate()- DOB),CONVERT(varchar(16),dateofVisit,106),CONVERT(varchar(16),DateofSalvation,106),CONVERT(varchar(16),Firstcall,106),visit,Campus,ChurchGroup,isBornAgain,CONVERT(varchar(16),FirstEmail,106),ZoneDescrip,Tithe,TypeOfService,CONVERT(varchar(16),ServiceDate,106) ,MemStatus FROM Stats_Form WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and intid = '" + MemberID.Value + "'";
        table = connect.DTSQL(sqlQuery);

     

        lblComments.InnerText = connect.SingleRespSQL("SELECT count(intid) FROM VisitorsNotes WHERE MemberNo = '" + MemberID.Value + "'");
        if (table.Rows.Count > 0)
        {
     
            foreach (DataRow rows in table.Rows)
            {


                if (ConfigurationManager.AppSettings["HideFields"].ToString() == "1")
                {
                    IDNumber.Value = MemberID.Value;
                }
                else
                {
                    IDNumber.Value = rows[0].ToString();
                }
              
                lblMemberName.InnerText = rows[2].ToString() +" " + rows[1].ToString();
                textfield.Value = rows[0].ToString();
                txtName.Value = rows[2].ToString();
                txtSurname.Value = rows[1].ToString();
                txtKnownAS.Value = rows[3].ToString();
                CmdGender.Value = rows[4].ToString();
                datepicker.Value = rows[5].ToString();
                CmdWard.Value = rows[6].ToString();
                CmdMarital.Value = rows[7].ToString();

                if (CmdMarital.Value == "Married")
                {
                   // DivMarriageDate.Visible = true;
                    DivMarriageDate.Style.Add("display", "block");
                }
                else
                {
                    //DivMarriageDate.Visible = false;
                    DivMarriageDate.Style.Add("display", "none");
                }

                txtOccupation.Value = rows[8].ToString();
                txtEmployer.Value = rows[9].ToString();
                txtSkills.Value = rows[10].ToString();
                txtAddress.Value = rows[11].ToString();
                txtTellH.Value = rows[12].ToString();
                txtTellW.Value = rows[13].ToString();
                txtFax.Value = rows[14].ToString();
                txtCellNo.Value = rows[15].ToString();
                txtEmail.Value = rows[16].ToString();
                CmdMinistries.Value = rows[17].ToString();
                txtMemberNo.Value = rows[18].ToString();
                CmdFFS.Value  = rows[22].ToString();
                txtSpiritualGifts.Value = rows[23].ToString();
                txtMemberObtained.Value = rows[24].ToString();
                CmdStatus.Value = rows[25].ToString();
                txtChurchInv.Value = rows[26].ToString();





                txtMarriageDate.Value = rows[27].ToString();
                CmdZone.Value = rows[28].ToString();
               // txtChurchInv.Value = rows[29].ToString(); Member type
                CmdSms.Value = rows[30].ToString();
                txtNewBeliever.Value = rows[31].ToString();

                txtIconnect.Value = rows[32].ToString();
                txtBaptism.Value = rows[33].ToString();

                if (rows[34].ToString() == "")
                {
                    txtiServe.Value = "None";
                }
                else
                {
                    txtiServe.Value = rows[34].ToString();
                }
               
                txtComment.Value = rows[35].ToString();
                CmdGrowthPath.Value = rows[36].ToString();
                txtAge.Value = rows[37].ToString();

                txtFirstVisit.Value = rows[38].ToString();

                if (rows[38].ToString() == "01 Jan 1900")
                {
                    txtFirstVisit.Value = "";
                }


                txtDateSalvation.Value = rows[39].ToString();

                if (rows[39].ToString() == "01 Jan 1900")
                {
                    txtDateSalvation.Value = "";
                }

                txtFirstCall.Value = rows[40].ToString();

                if (rows[40].ToString() == "01 Jan 1900")
                {
                    txtFirstCall.Value = "";
                }
                


                txtVisit.Value = rows[41].ToString();
                CmdCampus.Value = rows[42].ToString();
                txtCampus.Value = rows[42].ToString();
            


                CmdGroup.Value = rows[43].ToString();
                CmdBornAgain.Value = rows[44].ToString();

                txtDateFirstEmail.Value = rows[45].ToString();
                txtZoneDesc.Value = rows[46].ToString();
                cmdMemberTithe.Value = rows[47].ToString();

                if (Session["MemType"].ToString() == "Visitor")
                {
                    CmdService.SelectedValue = rows[48].ToString();
                    txtExperienceDate.Value = rows[49].ToString();

                    if (rows[49].ToString() == "01 Jan 1900")
                    {
                        txtExperienceDate.Value = "";
                    }



                }

                CmdMemberStatus.SelectedValue = rows[50].ToString();



                string url = "";
                string CheckPath = Session["FilePath"].ToString() + @"MembersImage\" + rows[18].ToString() + @"\" + rows[18].ToString() + ".jpg";
                if (File.Exists(CheckPath))
                {
                    url = Session["URL"].ToString() + "/MembersImage/" + rows[18].ToString() + "/" + rows[18].ToString() + ".jpg";
                }
                else
                {
                    url = Session["URL"].ToString() + "/MembersImage/avatar.jpg";
                }


                //string CheckPath = Session["FilePath"].ToString() + @"MembersImage\" + rows[18].ToString() + @"\" + rows[18].ToString() + ".jpg";
                //if (File.Exists(CheckPath))
                //{
                //    url = Session["URL"].ToString() + "/MyDunamis/MembersImage/" + rows[18].ToString() + "/" + rows[18].ToString() + ".jpg";
                //}
                //else
                //{
                //    url = Session["URL"].ToString() + "/MyDunamis/MembersImage/avatar.jpg";
                //}

           
                RunImage.Text = @"<div class='kt-avatar__holder' style='background-image: url(" + url + ");'></div>";


            }
        }
        else
        {

            DivViewMember.Visible = true;
        }
    }

    void RunOnLoad()
    {



        Loadfooter.Text = Session["Footer"].ToString();
        MemberID.Value = Session["MemberID"].ToString();
        RunJavascripts();
        if (ConfigurationManager.AppSettings["HideFields"].ToString() == "1")
        {

            if (Session["ShowAll"].ToString() == "Yes")
            {
                BtnSaveDetails.Visible = true;
            }

            dateAstriek.InnerText = "  *";
            DivID.Visible = false;
            DivKnowAs.Visible = false;
            DivWard.Visible = false;
            DivOccupation.Visible = false;
            DivEmpSchool.Visible = false;
            DivSkills.Visible = false;
            DivTelno.Visible = false;
            DivTelnoW.Visible = false;
            DivFax.Visible = false;
            DivMinistries.Visible = false;


            DivChurchInvol.Visible = false;
            DivStatus.Visible = false;
            DivmemberOb.Visible = false;

            DivSpiritualGifts.Visible = false;
            DivFFS.Visible = false;
            DivMinistries.Visible = false;


            DivMinistries.Visible = false;
            DivMinistries.Visible = false;
            DivMinistries.Visible = false;
            DivMarriageDate.Style.Add("display", "block"); 
          //  DivMarriageDate.Visible = true;
            DivZone.Visible = true;
            DivSms.Visible = true;
            DivNewbeliever.Visible = false;
            DiVIconnect.Visible = false;
            DivBaptism.Visible = false;
            DivComment.Visible = true;
            DivIserve.Visible = false;
            DivGrowthPath.Visible = true;


            if (Session["MemType"].ToString() == "Visitor")
            {
                if (Session["VisitorRight"].ToString() == "1")
                {
                    BtnSaveDetails.Visible = true;
                }
                dateAstriek.InnerText = "";
                DivExp.Visible = true;
                DivFirstCall.Visible = true;
                DivBornAgain.Visible = true;
                DivGrowthPath.Visible = false;
                DivZone.Visible = false;
                DivTithe.Visible = false;
                DivFirstVisit.Visible = true;
                DivSalvation.Visible = true;
                DivFirstEmail.Visible = true;
                DivVisit.Visible = false;
                DivMarriage.Visible = false;
                DivMarriageDate.Style.Add("display", "none");
               // DivMarriageDate.Visible = false;
                MenuTop.Visible = true;
                lblHolderN.InnerText = "Visitors";
                ReturnVisitors.Visible = true;
                DivTheCampus.Visible = true;
                DivNotes.Visible = true;
            }
            else if (Session["MemType"].ToString() == "Evangelist")
            {
                if (Session["Evangelism"].ToString() == "1")
                {
                    BtnSaveDetails.Visible = true;
                }
                dateAstriek.InnerText = "";
                DivFirstCall.Visible = true;
                DivBornAgain.Visible = true;
                DivGrowthPath.Visible = false;
                DivZone.Visible = false;
                DivTithe.Visible = false;
                DivFirstVisit.Visible = true;
                DivSalvation.Visible = true;
                DivFirstEmail.Visible = true;
                DivVisit.Visible = false;
                DivMarriage.Visible = false;
                DivMarriageDate.Style.Add("display", "none");
               // DivMarriageDate.Visible = false;
                MenuTop.Visible = true;
                lblHolderN.InnerText = "Evangelism";
                ReturnEvangelist.Visible = true;
                DivTheCampus.Visible = true;
                DivNotes.Visible = true;
            }
            else if (Session["MemType"].ToString() == "NewMember")
            {
                if (Session["NewMemberRights"].ToString() == "1")
                {
                    BtnSaveDetails.Visible = true;
                }
                DivZoneDes.Visible = false;
                DivIserve.Visible = true;
                DivTheCampus.Visible = false;
                AstrekID.InnerText = "  *";
                DivTithe.Visible = false;
                DivNotes.Visible = false;
                DivMarriage.Visible = true;
                DivCampus.Visible = true;
                DivGroup.Visible = true;
                MenuTop.Visible = true;
                lblHolderN.InnerText = "New Member";
                ReturnNewMember.Visible = true;
                DivNotes.Visible = true;
                DivGrowthPath.Visible = false;
            }
            else
            {
                if (Session["MemberRight"].ToString() == "1")
                {
                    BtnSaveDetails.Visible = true;
                }
                MemStat.Visible = true;
                DivMemberTithe.Visible = true;
                DivZoneDes.Visible = false;
                DivIserve.Visible = true;
                DivTheCampus.Visible = false;
                AstrekID.InnerText =  "  *";
                DivTithe.Visible = false;
                DivNotes.Visible = false;
                DivMarriage.Visible = true;
                DivCampus.Visible = true;
                DivGroup.Visible = true;
                lblHolderN.InnerText = "Members";
                ReturnMember.Visible = true;
                MenuNorm.Visible = true;
            }

        }
        else
        {
            BtnSave.Visible = true;
        }



        PopulateZone();
        PopulateCampus();
        PopulateDemographics();
       
        lblName.InnerText = Session["FName"].ToString();
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
        CmdGroup.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select", "none"));

    }



    void RunJavascripts()
    {

        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ CommunicationRobot(); },550);", true);
 
    }

    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "ViewMembers.txt");
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

    protected void BtnSave_ServerClick(object sender, EventArgs e)
    {

       int complete = 0;
   
       textfield.Value = Regex.Replace(textfield.Value, @"[^0-9A-Za-z ,]", ",");
       txtName.Value = Regex.Replace(txtName.Value, @"[^0-9A-Za-z ,]", ",");
       txtSurname.Value = Regex.Replace(txtSurname.Value, @"[^0-9A-Za-z ,]", ",");
       txtKnownAS.Value = Regex.Replace(txtKnownAS.Value, @"[^0-9A-Za-z ,]", ",");
       txtOccupation.Value = Regex.Replace(txtOccupation.Value, @"[^0-9A-Za-z ,]", ",");
       txtEmployer.Value = Regex.Replace(txtEmployer.Value, @"[^0-9A-Za-z ,]", ",");
       txtSkills.Value = Regex.Replace(txtSkills.Value, @"[^0-9A-Za-z ,]", ",");
       txtAddress.Value = Regex.Replace(txtAddress.Value, @"[^0-9A-Za-z ,]", ",");
       txtTellH.Value = Regex.Replace(txtTellH.Value, @"[^0-9a-zA-Z]+", "");
       txtTellW.Value = Regex.Replace(txtTellW.Value, @"[^0-9a-zA-Z]+", "");
       txtFax.Value = Regex.Replace(txtFax.Value, @"[^0-9a-zA-Z]+", "");
       txtChurchInv.Value = Regex.Replace(txtChurchInv.Value, @"[^0-9A-Za-z ,]", ",");
       txtSpiritualGifts.Value = Regex.Replace(txtSpiritualGifts.Value, @"[^0-9A-Za-z ,]", ",");
      
       #region Text Box not complete
       if ((textfield.Value == "") || (txtName.Value == "") || (txtSurname.Value == "") || (txtAddress.Value == "") || (CmdGender.Value == "none") || (CmdWard.Value == "none") || (CmdMarital.Value == "none") || (CmdFFS.Value == "none") || (CmdMarital.Value == "none"))
       {
           NotCompleteNotie();
           return;
       }

       if (txtEmail.Value != "")
       {
           if (!IsValidEmail(txtEmail.Value))
           {
               txtEmail.Value = "";
           }
       }
       #endregion


       string query = "UPDATE Stats_Form  SET Idnumber = '" + textfield.Value + "' , Surname = '" + txtSurname.Value + "', Name = '" + txtName.Value + "', Knownas = '" + txtKnownAS.Value + "', Gender = '" + CmdGender.Value + "' , DOB = '" + datepicker.Value + "', Ward= '" + CmdWard.Value + "', MaritalStat = '" + CmdMarital.Value + "', Occupation = '" + txtOccupation.Value + "', " +
       "EmployerCheck = '" + txtEmployer.Value + "', Skills =  '" + txtSkills.Value + "', ResidentAdd = '" + txtAddress.Value + "', TelnoH = '" + txtTellH.Value + "', TelnoW = '" + txtTellW.Value + "',Fax = '" + txtFax.Value + "' ,Celno = '" + txtCellNo.Value + "', EmailAdd = '" + txtEmail.Value + "', Ministries = '" + CmdMinistries.Value + "',LastUpdate = '" + Session["FullName"].ToString() + "',LastUpdateDate = GETDATE(),ChurchInvolvement = '" + txtChurchInv.Value + "',ChurchStatus = '" + CmdStatus.Value + "',DateMemberObtained = '" + txtMemberObtained.Value + "',FFS = '" + CmdFFS.Value + "',SpiritualGifts = '" + txtSpiritualGifts.Value + "'  WHERE intid = '" + MemberID.Value + "'";
      complete = connect.SingleIntSQL(query);
      if (complete > 0)
      {

        
          DivViewMember.Visible = true;

          RunNotie.Value = "Communication";
          SaveNotie();
          
      }

     
     
    }


    protected void btnSaveFAmount_ServerClick(object sender, EventArgs e)
    {
 
        int complete = connect.SingleIntSQL("INSERT INTO tithe (ChurchID,IDnumber,Month,UploadDate,Amount)VALUES ('" + Session["ChurchID"].ToString() + "', '" + IDNumber.Value + "', '" + DateMonth.Value + "',GETDATE(),'" + txtAmount.Value + "')");
        if (complete > 0)
        {
            txtAmount.Value = "";
            DateMonth.Value = "";
            RunNotie.Value = "Tithe";
            SaveNotie();
        }
    }

    public string RemoveSpecialChars(string input)
    {
        return Regex.Replace(input, @"[~`!@#$%^&*()+=|\\{}':;,<>/?[\]""_-]", string.Empty);
    }

    protected void BtnSaveDetails_ServerClick(object sender, EventArgs e)
    {
        int complete = 0;

   

        txtName.Value = Regex.Replace(txtName.Value, @"[^0-9A-Za-z ,]", ",");
        txtSurname.Value = Regex.Replace(txtSurname.Value, @"[^0-9A-Za-z ,]", ",");


        txtAddress.Value = Regex.Replace(txtAddress.Value, @"[^0-9A-Za-z ,]", ",");

        string NewExp = CmdService.SelectedValue.ToString();

        int CheckGrowthPath = 0;
        int CheckZone = 0;
        int CheckCampus = 0;
        int CheckMarriage = 0;
        int CheckGroup= 0;
        int CheckMemTithe = 0;
      
        if (Session["MemType"].ToString() == "Visitor" || Session["MemType"].ToString() == "NewMember" || Session["MemType"].ToString() == "Evangelist")
        {
            CheckGrowthPath = 1;
            CheckGroup = 1;
            CheckCampus = 1;
            CheckMarriage = 1;
            CheckZone = 1;
            CheckMemTithe = 1;
           
        }
        else
        {
            if (CmdMarital.Value == "none")
            {
                CheckMarriage = 0;
            }
            else
            {
                CheckMarriage = 1;
            }



            if (CmdCampus.Value == "none")
            {
                CheckCampus = 0;
            }
            else
            {
                CheckCampus = 1;
            }

            if (CmdGroup.Value == "none")
            {
                CheckGroup = 0;
            }
            else
            {
                CheckGroup = 1;
            }

            if (CmdZone.Value == "none")
            {
                CheckZone = 0;
            }
            else
            {
                CheckZone = 1;
            }

            if (CmdGrowthPath.Value == "nones")
            {
                CheckGrowthPath = 0;
            }
            else
            {
                CheckGrowthPath = 1;
            }


            if (cmdMemberTithe.Value == "nones")
            {
                CheckMemTithe = 0;
            }
            else
            {
                CheckMemTithe = 1;
            }
            

            

        }

     


        #region Text Box not complete  
        if ((txtName.Value == "") || (txtSurname.Value == "") || (CmdGender.Value == "none")  || (txtiServe.Value == "nones") || (CheckMarriage == 0) || (CheckGrowthPath == 0) || (CmdSms.Value == "none") || (CheckZone == 0) || (CheckCampus == 0) || (CheckGroup == 0) || (CheckMemTithe == 0)) 
        {
            NotCompleteNotie();
            return;
        }

        if (txtEmail.Value != "")
        {
            if (!IsValidEmail(txtEmail.Value))
            {
                txtEmail.Value = "";
            }
        }
        #endregion

        #region Save Image
        string filePath  = "";
        if ((Imageload.PostedFile != null) && (Imageload.PostedFile.ContentLength > 0))
        {
            filePath = Session["FilePath"].ToString() + @"MembersImage\" + txtMemberNo.Value + @"\" + txtMemberNo.Value + ".jpg";


            if (!Directory.Exists(Session["FilePath"].ToString() + @"MembersImage\" + txtMemberNo.Value))
            {
               
                Directory.CreateDirectory(Session["FilePath"].ToString() + @"MembersImage\" + txtMemberNo.Value);
            }
          


            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            Imageload.PostedFile.SaveAs(filePath);
        }
        #endregion


        string query = "UPDATE Stats_Form  SET Idnumber = '" + textfield.Value + "' , Surname = '" + txtSurname.Value + "', Name = '" + txtName.Value + "', Knownas = '" + txtKnownAS.Value + "',Gender = '" + CmdGender.Value + "' , DOB = '" + datepicker.Value + "', Ward= '" + CmdWard.Value + "', MaritalStat = '" + CmdMarital.Value + "', Occupation = '" + txtOccupation.Value + "', " +
        "EmployerCheck = '" + txtEmployer.Value + "', Skills =  '" + txtSkills.Value + "', ResidentAdd = '" + RemoveSpecialChars(txtAddress.Value) + "', TelnoH = '" + txtTellH.Value + "', TelnoW = '" + txtTellW.Value + "',Fax = '" + txtFax.Value + "' ,Celno = '" + txtCellNo.Value + "', EmailAdd = '" + txtEmail.Value + "', Ministries = '" + CmdMinistries.Value + "',LastUpdate = '" + Session["FullName"].ToString() + "',LastUpdateDate = GETDATE(),ChurchInvolvement = '" + txtChurchInv.Value + "',ChurchStatus = '" + CmdStatus.Value + "',DateMemberObtained = '" + txtMemberObtained.Value + "',FFS = '" + CmdFFS.Value + "',SpiritualGifts = '" + txtSpiritualGifts.Value + "'," +
        "marrieddate = '" + txtMarriageDate.Value + "',churchzone = '" + CmdZone.Value + "',sms = '" + CmdSms.Value + "',newbeliever = '" + txtNewBeliever.Value + "',iconnect = '" + txtIconnect.Value + "',baptized = '" + txtBaptism.Value + "',iserve = '" + txtiServe.Value + "',comments = '" + RemoveSpecialChars(txtComment.Value) + "',GrowthPath = '" + CmdGrowthPath.Value + "',dateofVisit = '" + txtFirstVisit.Value + "',DateofSalvation = '" + txtDateSalvation.Value + "',Firstcall = '" + txtFirstCall.Value + "',visit = '" + RemoveSpecialChars(txtVisit.Value) + "',Campus = '" + CmdCampus.Value + "',ChurchGroup = '" + CmdGroup.Value + "',isBornAgain = '" + CmdBornAgain.Value + "',FirstEmail = '" + txtDateFirstEmail.Value + "',ZoneDescrip = '" + RemoveSpecialChars(txtZoneDesc.Value) + "',Tithe = '" + cmdMemberTithe.Value + "' ,TypeOfService = '" + NewExp + "',ServiceDate = '" + txtExperienceDate.Value + "',MemStatus = '" + CmdMemberStatus.SelectedValue.ToString()  + "'   WHERE intid = '" + MemberID.Value + "'"; 
        complete = connect.SingleIntSQL(query);
        if (complete > 0)
        {

            #region Update Users table

            int IsUser = int.Parse(connect.SingleRespSQL("SELECT count(intid) FROM ChurchUsers  WHERE Memberid = '" + MemberID.Value + "'"));
            if (IsUser > 0)
            {
                connect.SingleIntSQL("UPDATE ChurchUsers SET Campus = '" + CmdCampus.Value + "',name = '" + txtName.Value + "',surname = '" + txtSurname.Value + "',emailadd = '" + txtEmail.Value + "'  WHERE  MemberID = '" + MemberID.Value + "'");
            }
            #endregion

            PopulateDemographics();
            DivViewMember.Visible = true;
            RunNotie.Value = "Communication";
            SaveNotie();

        }

        
        
    }


    protected void btnSaveNotes_ServerClick(object sender, EventArgs e)
    {


        int complete = connect.SingleIntSQL("INSERT INTO VisitorsNotes (ChurchID,MemberNo,Msg,ActionDate,ActionBy)VALUES ('" + Session["ChurchID"].ToString() + "', '" + IDNumber.Value + "', '" + txtCommMsg.Text + "',GETDATE(),'" + Session["FullName"].ToString() + "')");
        if (complete > 0)
        {
            lblComments.InnerText = connect.SingleRespSQL("SELECT count(intid) FROM VisitorsNotes WHERE MemberNo = '" + IDNumber.Value + "'");
            RunNotie.Value = "NotesVisitors";
            txtCommMsg.Text = "";
            SaveNotie();
        }

       
    }
}