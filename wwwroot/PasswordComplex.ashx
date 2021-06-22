<%@ WebHandler Language="C#" Class="PasswordComplex" %>

using System;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;


public class PasswordComplex :  IHttpHandler,System.Web.SessionState.IReadOnlySessionState {

   
    public void ProcessRequest (HttpContext context) {
        string id = context.Request["pword"];
    
        string result = "";
        PasswordScore passwordStrengthScore = PasswordAdvisor.CheckStrength(id);
        switch (passwordStrengthScore)
        {
            case PasswordScore.Blank:
                result = "Blank";
                break;
            case PasswordScore.VeryWeak:
                result = "Very weak";
                break;
            case PasswordScore.Weak:
                result = "Weak";
                break;

            case PasswordScore.Medium:
                result = "Medium";
                break;
            case PasswordScore.Strong:
                result = "Strong";
                break;
            case PasswordScore.VeryStrong:
                result = "Very strong";
                break;
        }

       

        context.Response.Write(result);

    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}

#region PAssword Strength 
public enum PasswordScore
{
    Blank = 0,
    VeryWeak = 1,
    Weak = 2,
    Medium = 3,
    Strong = 4,
    VeryStrong = 5
}

public class PasswordAdvisor
{
    public static  PasswordScore CheckStrength(string password)
    {
        int score = 0;

        if (password.Length < 1)
            return PasswordScore.Blank;

        if (password.Length < 4)
            return PasswordScore.VeryWeak;


        if (password.Length >= 6)
            score++;

        int Numbers =  Regex.Matches(password, @"\d+").Count;
        int Letters = Regex.Matches(password,@"[a-zA-Z]").Count;
        int SpecialCharaters = Regex.Matches(password,@"[!,@,$,%,^,&,*,?,_,~,-,£,(,)]").Count;


        if(Numbers > 0)
        {
            score++;
        }

        if(Letters > 0)
        {
            score++;
        }

        if(SpecialCharaters == 1)
        {
            score++;
        }

        if(SpecialCharaters > 1)
        {
            score++;
            score++;
        }

        return (PasswordScore)score;
    }
}

#endregion