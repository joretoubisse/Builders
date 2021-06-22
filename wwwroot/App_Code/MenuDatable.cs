using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

using System.Web;


/// <summary>
/// Summary description for ConnMethods
/// </summary>
public class MenuDatatble
{

    
    public DataTable ReturnMenuT()
    {
		DataTable GetRoles = new DataTable();
		SqlConnMethod connect = new SqlConnMethod();
		string AllRoles = "";
        GetRoles = connect.DTSQL("SELECT  AdminRole,MembersRole ,VisitorRole ,SundaySchoolRole ,AttendanceRole ,Communication,Offering,NewMembersAppRole ,ConnectGroupRole,EventsRole ,ResourceRole,MinistryRole,EvangelistRole ,ReportsRole,DashboardRole FROM ChurchUsers  WHERE intid = '" + HttpContext.Current.Session["UsersID"].ToString() + "'");
		if (GetRoles.Rows.Count > 0)
		{
			foreach (DataRow MenuRoles in GetRoles.Rows)
			{
				#region Dashboard And Logout
				AllRoles += "'My Profile'" + ",";
				AllRoles += "'Logout'" + ",";
				#endregion


                #region Dashboard
                if (MenuRoles[14].ToString() != "" && MenuRoles[14].ToString() != "NA")
                {
                    AllRoles += "'Dashboard'" + ",";
                }
                #endregion


                #region Admin
                if (MenuRoles[0].ToString() != "" && MenuRoles[0].ToString() != "NA")
				{
					AllRoles += "'Admin'" + ",";
				}
				#endregion

				#region Members
				if (MenuRoles[1].ToString() != "" && MenuRoles[1].ToString() != "NA")
				{
					AllRoles += "'Members'" + ",";
				}
				#endregion

				#region Visitors
				if (MenuRoles[2].ToString() != "" && MenuRoles[2].ToString() != "NA")
				{
					AllRoles += "'Visitors'" + ",";
				}
				#endregion

				#region Sunday School
				if (MenuRoles[3].ToString() != "" && MenuRoles[3].ToString() != "NA")
				{
                    AllRoles += "'Builders Kidz'" + ",";
				}
				#endregion

				#region Attendance
				if (MenuRoles[4].ToString() != "" && MenuRoles[4].ToString() != "NA")
				{
					AllRoles += "'Attendance'" + ",";
				}
				#endregion

				#region Communication
				if (MenuRoles[5].ToString() != "" && MenuRoles[5].ToString() != "NA")
				{
                    AllRoles += "'Notifications'" + ",";
				}
				#endregion

				#region Offering
				if (MenuRoles[6].ToString() != "" && MenuRoles[6].ToString() != "NA")
				{
                    AllRoles += "'Offering'" + ",";
				}
				#endregion

				#region New Membership Applications
				if (MenuRoles[7].ToString() != "" && MenuRoles[7].ToString() != "NA")
				{
                    AllRoles += "'New Membership Applications'" + ",";
				}
				#endregion

				#region Connect Groups
				if (MenuRoles[8].ToString() != "" && MenuRoles[8].ToString() != "NA")
				{
                    AllRoles += "'Connect Groups'" + ",";
				}
				#endregion

				#region Events
				if (MenuRoles[9].ToString() != "" && MenuRoles[9].ToString() != "NA")
				{
					AllRoles += "'Events'" + ",";
				}
				#endregion

				#region Resources
				if (MenuRoles[10].ToString() != "" && MenuRoles[10].ToString() != "NA")
				{
					AllRoles += "'Resources'" + ",";
				}
				#endregion

				#region Ministries
				if (MenuRoles[11].ToString() != "" && MenuRoles[11].ToString() != "NA")
				{
                    AllRoles += "'iServe'" + ",";
				}
				#endregion

				#region Evangelism
				if (MenuRoles[12].ToString() != "" && MenuRoles[12].ToString() != "NA")
				{
					AllRoles += "'Evangelism'" + ",";
				}
				#endregion

				#region Reports
				if (MenuRoles[13].ToString() != "" && MenuRoles[13].ToString() != "NA")
				{
					AllRoles += "'Reports'" + ",";
				}
				#endregion


			}
		}

	
		DataTable Response = connect.DTSQL("SELECT   menuname,pageurl FROM MenuItems WHERE isactive = '1' and MenuName in  (" + AllRoles.TrimEnd(',') + ") order by isordering ASC");
		return Response;
    }

   
    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "MenuDatable.txt");
        #region Local
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, true))
        {
            writer.WriteLine(msg);
        }
        #endregion
    }

   
}