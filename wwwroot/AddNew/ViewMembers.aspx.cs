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



    void PopulateDemographics()
    {

        DataTable table = new DataTable();
        string sqlQuery = @"SELECT  idnumber, Surname, Name, Knownas, Gender, CONVERT(varchar(16),DOB,106), Ward, MaritalStat, Occupation,EmployerCheck, 
                            Skills, ResidentAdd, TelnoH, TelnoW,Fax,Celno, EmailAdd, Ministries,MemberNo,ChurchID,CapturedBy,
                            DateCaptured,FFS,SpiritualGifts, CONVERT(varchar(16),DateMemberObtained,106),ChurchStatus,ChurchInvolvement  FROM Stats_Form WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and intid = '" + MemberID.Value + "'";
        table = connect.DTSQL(sqlQuery);


        if (table.Rows.Count > 0)
        {
     
            foreach (DataRow rows in table.Rows)
            {
                IDNumber.Value = rows[0].ToString();
                lblMemberName.InnerText = rows[2].ToString() +" " + rows[1].ToString();
                textfield.Value = rows[0].ToString();
                txtName.Value = rows[2].ToString();
                txtSurname.Value = rows[1].ToString();
                txtKnownAS.Value = rows[3].ToString();
                CmdGender.Value = rows[4].ToString();
                datepicker.Value = rows[5].ToString();
                CmdWard.Value = rows[6].ToString();
                CmdMarital.Value = rows[7].ToString();

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

            }
        }
        else
        {

            DivViewMember.Visible = true;
        }
    }

    void RunOnLoad()
    {


        MemberID.Value = Session["MemberID"].ToString();
        RunJavascripts();
        PopulateDemographics();
       
        lblName.InnerText = Session["FName"].ToString();
   
    }

    void RunJavascripts()
    {

        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ CommunicationRobot(); },550);", true);
 
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
}