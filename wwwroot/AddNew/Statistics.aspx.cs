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

public partial class Statistics : System.Web.UI.Page
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

       #region Save in SQL
           string sql = "INSERT Into Stats_Form (Idnumber, Surname, Name, Knownas, Gender, DOB, Ward, MaritalStat, Occupation,EmployerCheck, Skills, ResidentAdd, TelnoH, TelnoW,Fax,Celno, EmailAdd, Ministries,MemberNo,ChurchID,CapturedBy,DateCaptured,IsActive,ChurchInvolvement,ChurchStatus,DateMemberObtained,SpiritualGifts,FFS) " +
           "VALUES ('" + textfield.Value + "','" + txtSurname.Value + "', '" + txtName.Value + "', '" + txtKnownAS.Value + "', '" + CmdGender.Value + "', '" + datepicker.Value + "', '" + CmdWard.Value + "', '" + CmdMarital.Value + "', '" + txtOccupation.Value + "', '" + txtEmployer.Value + "', '" + txtSkills.Value + "', '" + txtAddress.Value + "', '" + txtTellH.Value + "', '" + txtTellW.Value + "', '" + txtFax.Value + "', '" + txtCellNo.Value + "', '" + txtEmail.Value + "', '" + CmdMinistries.Value + "', '" + MemberNo + "', '" + Session["ChurchID"].ToString() + "', '" + Session["FullName"].ToString() + "',GETDATE(),'1','" + txtChurchInv.Value + "','" + CmdStatus.Value + "','" + txtMemberObtained.Value + "','" + txtSpiritualGifts.Value + "','" + CmdFFS.Value + "')";

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
        Server.Transfer("Dashboard.aspx");
    }
}