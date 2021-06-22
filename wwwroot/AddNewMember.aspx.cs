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

public partial class AddNewMember : System.Web.UI.Page
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


                if (MenuName == "New Membership Applications")
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

    void PopulateCampus()
    {
        DataTable table = connect.DTSQL("SELECT  campus,campus FROM Campus WHERE churchid = '" + Session["ChurchID"].ToString() + "'");
        CmdCampus.DataSource = table;
        CmdCampus.DataTextField = "campus";
        CmdCampus.DataValueField = "campus";
        CmdCampus.DataBind();
        CmdCampus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Please Select", "None"));
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

    void RunOnLoad()
    {

        Loadfooter.Text = Session["Footer"].ToString();
        if (ConfigurationManager.AppSettings["HideFields"].ToString() == "1")
        {
            DivIDNo.Visible = false;
            DivFFS.Visible = false;
            DivSpritiualGifts.Visible = false;
            DivmemberObtain.Visible = false;
            DivStatus.Visible = false;

            DivChurchInvolve.Visible = false;
            DivMinistries.Visible = false;
            DivFax.Visible = false;
            DivTelW.Visible = false;
            DivTelH.Visible = false;
            DivSkills.Visible = false;

            DivEmployer.Visible = false;
            DivOccupation.Visible = false;
            DivWard.Visible = false;
            DivKnownAs.Visible = false;
            BtnSave.Visible = false;
            BtnSaveMembers.Visible = true;
            DivMarriageDate.Visible = true;
            DivZone.Visible = true;
            DivCampus.Visible = true;
            DivGroup.Visible = true;
            DivSms.Visible = true;
            DivNewbeliever.Visible = false;
            DiVIconnect.Visible = false;
            DivBaptism.Visible = false;
            DivComment.Visible = true;
            DivIserve.Visible = true;
            DivGrowthPath.Visible = true;
            ImageDiv.Visible = true;
            PopulateZone();

        }
        lblName.InnerText = Session["FName"].ToString();

        PopulateCampus();
        RunMenus();
    }

    public string RemoveSpecialChars(string input)
    {
        return Regex.Replace(input, @"[~`!@#$%^&*()+=|\\{}':;,<>/?[\]""_-]", string.Empty);
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

    void UnderAgeNotie()
    {
        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ MemberYoung(); },550);", true);

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

    //HideFields =0
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
        txtChurchInv.Value = Regex.Replace(txtFax.Value, @"[^0-9a-zA-Z]+", "");
        txtSpiritualGifts.Value = Regex.Replace(txtFax.Value, @"[^0-9a-zA-Z]+", "");
        string MemberNo = Session["MemberNo"].ToString() + "_" +RandomString(5);

       

       #region Check Member Exist
       int MemberExist = int.Parse(connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE idnumber = '" + textfield.Value + "' and  ChurchID = '" + Session["ChurchID"].ToString() + "'"));
      
       if (MemberExist > 0)
       {
           ExistNotie();
           return;
       }
       #endregion

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

       if (textfield.Value.Length != 13)
       {
           InvalidIDNotie();
           return;
       }
      
       #endregion

       #region Save Image
       string filePath = "";
       if (UploadImage.HasFile)
       {
           filePath = Session["FilePath"].ToString() + @"MembersImage\" + MemberNo + @"\" + MemberNo + ".jpg";
           if (!Directory.Exists(Session["FilePath"].ToString() + @"MembersImage\" + MemberNo))
           {
               Directory.CreateDirectory(Session["FilePath"].ToString() + @"MembersImage\" + MemberNo);
           }

           if (File.Exists(filePath))
           {
               File.Delete(filePath);
           }
           UploadImage.PostedFile.SaveAs(filePath);

       }
       #endregion


       #region Save in SQL
       string sql = "INSERT Into Stats_Form (Idnumber, Surname, Name, Knownas, Gender, DOB, Ward, MaritalStat, Occupation,EmployerCheck, Skills, ResidentAdd, TelnoH, TelnoW,Fax,Celno, EmailAdd, Ministries,MemberNo,ChurchID,CapturedBy,DateCaptured,IsActive,ChurchInvolvement,ChurchStatus,DateMemberObtained,SpiritualGifts,FFS,Membertype) " +
           "VALUES ('" + textfield.Value + "','" + txtSurname.Value + "', '" + txtName.Value + "', '" + txtKnownAS.Value + "', '" + CmdGender.Value + "', '" + datepicker.Value + "', '" + CmdWard.Value + "', '" + CmdMarital.Value + "', '" + txtOccupation.Value + "', '" + txtEmployer.Value + "', '" + txtSkills.Value + "', '" + txtAddress.Value + "', '" + txtTellH.Value + "', '" + txtTellW.Value + "', '" + txtFax.Value + "', '" + txtCellNo.Value + "', '" + txtEmail.Value + "', '" + CmdMinistries.Value + "', '" + MemberNo + "', '" + Session["ChurchID"].ToString() + "', '" + Session["FullName"].ToString() + "',GETDATE(),'1','" + txtChurchInv.Value + "','" + CmdStatus.Value + "','" + txtMemberObtained.Value + "','" + txtSpiritualGifts.Value + "','" + CmdFFS.Value + "','NewMember')";

          complete = connect.SingleIntSQL(sql);
          if (complete > 0)
          {
              SaveNotie();

              textfield.Value = "";
              txtName.Value = "";
              txtSurname.Value = "";
              txtKnownAS.Value = "";
              txtOccupation.Value = "";
              txtEmployer.Value = "";
              txtSkills.Value = "";
              txtAddress.Value = "";
              txtTellH.Value = "";
              txtTellW.Value = "";
              txtFax.Value = "";
              txtCellNo.Value = "";
              txtEmail.Value = "";
          }
       #endregion
      

    }

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
        Server.Transfer("Members.aspx");
    }

    //HideFields =1
    protected void BtnSaveMembers_ServerClick(object sender, EventArgs e)
    {
        int complete = 0;
       
        txtName.Value = Regex.Replace(txtName.Value, @"[^0-9A-Za-z ,]", ",");
        txtSurname.Value = Regex.Replace(txtSurname.Value, @"[^0-9A-Za-z ,]", ",");
    
    
        txtAddress.Value = Regex.Replace(txtAddress.Value, @"[^0-9A-Za-z ,]", ",");
       
        string MemberNo = Session["MemberNo"].ToString() + "_" + RandomString(5);



        #region Check Member Exist
        //int MemberExist = int.Parse(connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE name = '" + txtName.Value + "' and  ChurchID = '" + Session["ChurchID"].ToString() + "'"));

        //if (MemberExist > 0)
        //{
        //    ExistNotie();
        //    return;
        //}
        #endregion

        #region Text Box not complete
        if ((txtName.Value == "") || (txtSurname.Value == "") || (txtAddress.Value == "") || (txtiServe.Value == "nones") || (CmdGender.Value == "none") || (CmdMarital.Value == "none") || (CmdGrowthPath.Value == "nones") || (CmdSms.Value == "none") || (CmdZone.Value == "none") || (CmdCampus.Value == "none") || (CmdGroup.Value == "none")) 
        {
            NotCompleteNotie();
            return;
        }



        DateTime now = DateTime.Now;
        DateTime birthDate = DateTime.Parse(datepicker.Value);
        int age = now.Year - birthDate.Year;

        if (age >= 12)
        {
            //Member needs to be 12 and above
         
        }
        else
        {
            UnderAgeNotie();
            return;
        }

        if (txtEmail.Value != "")
        {
            if (!IsValidEmail(txtEmail.Value))
            {
                txtEmail.Value = "";
            }
        }

        #region Save Image
        string filePath = "";
        if (UploadImage.HasFile)
        {
            filePath = Session["FilePath"].ToString() + @"MembersImage\" + MemberNo + @"\" + MemberNo + ".jpg";
            if (!Directory.Exists(Session["FilePath"].ToString() + @"MembersImage\" + MemberNo))
            {
                Directory.CreateDirectory(Session["FilePath"].ToString() + @"MembersImage\" + MemberNo);
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            UploadImage.PostedFile.SaveAs(filePath);

        }
        #endregion

        #endregion

        #region Save in SQL
        string sql = "INSERT Into Stats_Form (Idnumber, Surname, Name, Knownas, Gender, DOB, Ward, MaritalStat, Occupation,EmployerCheck, Skills, ResidentAdd, TelnoH, TelnoW,Fax,Celno, EmailAdd, Ministries,MemberNo,ChurchID,CapturedBy,DateCaptured,IsActive,marrieddate,churchzone,membertype,sms,newbeliever,iconnect,baptized,iserve,comments,GrowthPath,Campus,ChurchGroup,ZoneDescrip) " +
        "VALUES ('" + textfield.Value + "','" + RemoveSpecialChars(txtSurname.Value) + "', '" + RemoveSpecialChars(txtName.Value) + "', '" + txtKnownAS.Value + "', '" + CmdGender.Value + "', '" + datepicker.Value + "', '" + CmdWard.Value + "', '" + CmdMarital.Value + "', '" + txtOccupation.Value + "', '" + txtEmployer.Value + "', '" + txtSkills.Value + "', '" + RemoveSpecialChars(txtAddress.Value) + "', '" + txtTellH.Value + "', '" + txtTellW.Value + "', '" + txtFax.Value + "', '" + txtCellNo.Value + "', '" + txtEmail.Value + "', '" + CmdMinistries.Value + "', '" + MemberNo + "', '" + Session["ChurchID"].ToString() + "', '" + Session["FullName"].ToString() + "',GETDATE(),'1','" + txtMarriageDate.Value + "','" + CmdZone.Value + "','NewMember','" + CmdSms.Value + "','" + txtNewBeliever.Value + "','" + txtIconnect.Value + "','" + txtBaptism.Value + "','" + txtiServe.Value + "','" + RemoveSpecialChars(txtComment.Value) + "','" + CmdGrowthPath.Value + "','" + CmdCampus.Value + "','" + CmdGroup.Value + "','" + RemoveSpecialChars(txtZoneDesc.Value) + "')";

        complete = connect.SingleIntSQL(sql);
        if (complete > 0)
        {
            SaveNotie();

            textfield.Value = "";
            txtName.Value = "";
            txtSurname.Value = "";
            txtKnownAS.Value = "";
            txtOccupation.Value = "";
            txtEmployer.Value = "";
            txtSkills.Value = "";
            txtAddress.Value = "";
            txtTellH.Value = "";
            txtTellW.Value = "";
            txtFax.Value = "";
            txtCellNo.Value = "";
            txtMarriageDate.Value = "";
            txtNewBeliever.Value = "";
            txtIconnect.Value = "";
            txtBaptism.Value = "";
      
            txtEmail.Value = "";
            txtComment.Value = "";
        }
        #endregion
    }
}