using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for Login
/// </summary>
public class LoginClass
{
    #region General Classes
    SqlConnMethod connect = new SqlConnMethod();
    #endregion

    public string Login(string login,string password)
    {
        #region Declaration
        string Email = "";
        string Name = "";
        string Workflow = "";
        string CompanyID = "";
        string AccessRight = "";
        string CompanyName = "";
        string Paid = "";
        string returner = "";
        string strpass = "";
        string strPassword, strEncodedPassword;
        string Package = "";
        #endregion
        DataTable dtnew = new DataTable();
        string sql1 = "SELEct  EmailAddress,Name,Surname,Workflow,CompanyID,RightAccess,organisation,paid,Password,package FROM Login_Table WHERE EmailAddress =  '" + login + "'";
        dtnew = connect.DTSQL(sql1);

        if (dtnew.Rows.Count > 0)
        {
            foreach (DataRow roww in dtnew.Rows)
            {
                Email = roww[0].ToString();
                Name = roww[1].ToString() + " " + roww[2].ToString();
                Workflow = roww[3].ToString();
                CompanyID = roww[4].ToString();
                AccessRight = roww[5].ToString();
                CompanyName = roww[6].ToString();
                Paid = roww[7].ToString();
                strpass = roww[8].ToString();
                Package = roww[9].ToString();
            }

            #region validate Password
            strPassword = password;
            // Encode before comparing the hashes - Must match the one on the database
            byte[] sByBuf = Encoding.ASCII.GetBytes(strPassword);
            for (int nCtr = 0; nCtr < sByBuf.Length; nCtr++)
                sByBuf[nCtr] = Convert.ToByte(Convert.ToInt64(sByBuf[nCtr]) + 4);
            strEncodedPassword = Encoding.ASCII.GetString(sByBuf);
            #endregion

            if (Paid == "0")
            {
                returner = "NotPaid";
            }
            else
            {
                if (strpass == strEncodedPassword)
                {

                    #region Set Session
                    HttpContext.Current.Session["Email"] = Email;
                    HttpContext.Current.Session["Name"] = Name;
                    HttpContext.Current.Session["Workflow"] = Workflow;
                    HttpContext.Current.Session["CompanyID"] = CompanyID;
                    HttpContext.Current.Session["AccessRight"] = AccessRight;
                    HttpContext.Current.Session["CompanyName"] = CompanyName;
                    HttpContext.Current.Session["URL"] = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                    HttpContext.Current.Session["FilePath"] = System.AppDomain.CurrentDomain.BaseDirectory.ToString();
                    HttpContext.Current.Session["Paid"] = Paid;
                    HttpContext.Current.Session["Package"] = Package;
                    HttpContext.Current.Session["LoggedIn"] = "True";
                    #endregion

                    #region Fire Login
                    LastLogin(Email);
                    returner = "LogIN";
                    #endregion

                }
                else
                {
                    returner = "WrongPassword";
                }
            }
        }
        else
        {
            returner = "InvalidEmail";           
        }

        return returner;
    }


    

    void LastLogin(string Email)
    {
        connect.SingleIntSQL("UPDATE Login_Table SET LastLogin = GETDATE()   WHERE EmailAddress = '" + Email + "'");
    }


}