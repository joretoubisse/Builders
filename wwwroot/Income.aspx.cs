using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Income : System.Web.UI.Page
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
                DivAddMember.Visible = false;
            }
            else
            {
                if (Session["AccessRights"].ToString() == "Admin")
                {
                    IsAdmin.Value = "1";
                    DivAddMember.Visible = false;
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


                if (MenuName == "Offering")
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

    public int CountNonSpaceChars(string value)
    {
        int result = 0;
        foreach (char c in value)
        {
            if (!char.IsWhiteSpace(c))
            {
                result++;
            }
        }
        return result;
    }

    void RunUSers()
    {
        string htmltext = "";
        DataTable table = new DataTable();
        string Getqry = "";
        Getqry = "SELECT  intid, incometype, ServiceName,NameOfIncome,amount,CONVERT(Varchar(16),offeringDate,106),capturedBy FROM Offering WHERE IsExpense='0' and status = '1' and   ChurchID = '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + "' ORDER BY  intid DESC";

        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                      "    <th> </th> " +
                        "    <th> Type</th> " +
                     //   "    <th> Experience</th> " +
                        "    <th> Name of income</th> " +
                        "    <th> Amount</th> " +
                        "    <th> Date </th> " +
                        "    <th> Captured By </th> " +

                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {


                string GetAmount = Row[4].ToString();

                if (GetAmount.Contains(","))
                {
                    string[] Array = Row[4].ToString().Split(',');
                    if (CountNonSpaceChars(Array[1].ToString()) == 1)
                    {
                        GetAmount = GetAmount + "0";
                    }

                }
                else
                {

                    if (GetAmount.Contains("."))
                    {
                        string[] Array = Row[4].ToString().Split('.');
                        if (CountNonSpaceChars(Array[1].ToString()) == 1)
                        {
                            GetAmount = GetAmount + "0";
                        }

                    }
                    else
                    {
                        GetAmount = GetAmount + ".00";
                    }


                    
                }

                htmltext += " <tr> " +
                       "<td ><center><a onclick='BtnViewMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> View </a>	&nbsp;	&nbsp;	&nbsp;    <a onclick='BtnArchMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Remove </a> </center></td>" +
                             "   <td >" + Row[1].ToString() + "</td> " +
                                // "   <td >" + Row[2].ToString() + "</td> " +
                                     "   <td >" + Row[3].ToString() + "</td> " +
                                         "   <td >R " + GetAmount + "</td> " +
                                     "   <td >" + Row[5].ToString() + "</td> " +
                            "   <td >" + Row[6].ToString() + "</td> " +

                        " </tr>";
            }
        }
        else
        {
            htmltext = "No Income";
        }


        htmltext += "    </tbody> " +
                   " </table>";
        tbTable.Text = htmltext;
    }

    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "Income.txt");
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
        BtnReturn.Visible = true;
        DivGridUsers.Visible = false;
        EditDiv.Visible = true;
        NormalBack.Visible = false;

        DataTable tb = new DataTable();
        tb = connect.DTSQL("SELECT  intid, incometype, ServiceName,NameOfIncome,amount,CONVERT(Varchar(16),offeringDate,106),capturedBy FROM Offering WHERE intid = '" + MemberID.Value + "'");
        if (tb.Rows.Count > 0)
        {
            foreach (DataRow rows in tb.Rows)
            {
                EditCmdType.SelectedValue = rows[1].ToString();

                if (rows[1].ToString() == "Offering")
                {
                    EditCmdExp.Visible = true;
                    EditExp.SelectedValue = rows[2].ToString();
                }
                else if (rows[1].ToString() == "Other")
                {
                    EditOtherDiv.Visible = true;
                    txteditNameIncome.Value = rows[3].ToString();
                }
                else if (rows[1].ToString() == "Tithe")
                {
                    EditOtherDiv.Visible = false;
                    EditCmdExp.Visible = false;
                }


                txtEditAmount.Value = rows[4].ToString();
                txtAmountEditDate.Value = rows[5].ToString();
            }
        }
    }

  

    protected void btnArchive_ServerClick(object sender, EventArgs e)
    {

        int complete = connect.SingleIntSQL("UPDATE Offering SET status = '0',LastUpdateby= '" + Session["FullName"].ToString() + "',lastUpdateDate = GETDATE()   WHERE intid = '" + MemberID.Value + "' ");
        if (complete > 0)
        {
            RunUSers();
            RemoveNotie();
        }
    }

    protected void btnCancelCampus_ServerClick(object sender, EventArgs e)
    {
        Server.Transfer("offering.aspx");
    }
    protected void btnSaveCampus_ServerClick(object sender, EventArgs e)
    {


        string GetType = CmdType.SelectedValue.ToString();
        string Service = CmdService.SelectedValue.ToString();
        string NameOfIncome = txtIncomeName.Value;
        string Amount = txtAmount.Value;
        string AmountDate = txtAmountDate.Value;



        #region Handle all the checks
        if (GetType == "none")
        {
            NotCompleteNotie();
            return;
        }


        if (GetType == "Tithe")
        {
            Service = "Not Applicable";
            NameOfIncome = GetType;
        }
        else if (GetType == "Offering")
        {
            NameOfIncome = GetType;
            if (Service == "none")
            {
                NotCompleteNotie();
                return;
            
            }
        }
        else if (GetType == "Other")
        {
            Service = "Not Applicable";
            if (NameOfIncome == "")
            {
                NotCompleteNotie();
                return;
            }

        }

        if (Amount == "" || AmountDate == "")
        {
            NotCompleteNotie();
            return;
        }
        #endregion



        Amount = Amount.Replace(",", ".");
        float F = float.Parse(Amount, CultureInfo.InvariantCulture.NumberFormat);
        decimal dvalue = Convert.ToDecimal(F);
        decimal x = Math.Round(dvalue, 2);


        Amount = x.ToString();
    
        int complete = connect.SingleIntSQL("INSERT INTO Offering (ChurchID,OfferingDate,UploadDate,Amount,campus,CapturedBy,IncomeType,ServiceName ,NameOfIncome,status,IsExpense)VALUES ('" + Session["ChurchID"].ToString() + "', '" + AmountDate + "',GETDATE(),'" + Amount.Replace(",", ".") + "','" + Session["Campus"].ToString() + "','" + Session["FullName"].ToString() + "','" + GetType + "','" + RemoveSpecialChars(Service) + "','" + NameOfIncome + "','1','0')");
        if (complete > 0)
        {
            txtAmount.Value = "";
            txtAmountDate.Value = "";
            txtIncomeName.Value = "";
            RunUSers();
            SaveNotie();
        }
    }

   

    protected void btnCancelEdit_ServerClick(object sender, EventArgs e)
    {
        BtnReturn.Visible = false;
        NormalBack.Visible = true;
        DivGridUsers.Visible = true;
        EditDiv.Visible = false;
    }

    protected void BtnReturn_ServerClick(object sender, EventArgs e)
    {
        BtnReturn.Visible = false;
        NormalBack.Visible = true;
        DivGridUsers.Visible = true;
        EditDiv.Visible = false;
    }
    protected void CmdType_SelectedIndexChanged(object sender, EventArgs e)
    {
        CmdExp.Visible = false;
        OtherText.Visible = false;
        if (CmdType.SelectedValue.ToString() == "Offering")
        {
            CmdExp.Visible = true;
        }
        else if (CmdType.SelectedValue.ToString() == "Other")
        {
            OtherText.Visible = true;
        }
    }
    protected void EditCmdType_SelectedIndexChanged(object sender, EventArgs e)
    {
        EditCmdExp.Visible = false;
        EditOtherDiv.Visible = false;
        if (EditCmdType.SelectedValue.ToString() == "Offering")
        {
            EditCmdExp.Visible = true;
        }
        else if (EditCmdType.SelectedValue.ToString() == "Other")
        {
            EditOtherDiv.Visible = true;
        }
    }
    protected void btnSaveEdit_ServerClick(object sender, EventArgs e)
    {

        //Capture Last update and by who

        string GetType = EditCmdType.SelectedValue.ToString();
        string Service = EditExp.SelectedValue.ToString();
        string NameOfIncome = txteditNameIncome.Value;
        string Amount = txtEditAmount.Value;
        string AmountDate = txtAmountEditDate.Value;



        #region Handle all the checks
        if (GetType == "none")
        {
            NotCompleteNotie();
            return;
        }


        if (GetType == "Tithe")
        {
            Service = "Not Applicable";
            NameOfIncome = GetType;
        }
        else if (GetType == "Offering")
        {
            NameOfIncome = GetType;
            if (Service == "none")
            {
                NotCompleteNotie();
                return;

            }
        }
        else if (GetType == "Other")
        {
            Service = "Not Applicable";
            if (NameOfIncome == "")
            {
                NotCompleteNotie();
                return;
            }

        }

        if (Amount == "" || AmountDate == "")
        {
            NotCompleteNotie();
            return;
        }
        #endregion




        Amount = Amount.Replace(",", ".");


        float F = float.Parse(Amount, CultureInfo.InvariantCulture.NumberFormat);
        decimal dvalue = Convert.ToDecimal(F);
        decimal x = Math.Round(dvalue, 2);


        Amount = x.ToString();

        try
        {

            string updateqry = "UPDATE Offering SET OfferingDate = @OfferingDate,Amount =@Amount,IncomeType = @IncomeType,ServiceName = @ServiceName ,NameOfIncome = @NameOfIncome,LastUpdateby= @LastUpdateby,lastUpdateDate = GETDATE() WHERE intid = @intid; SELECT @intid";
            SqlParameter[] updatesp =
            {
                new SqlParameter("@OfferingDate", AmountDate),
                new SqlParameter("@Amount", Amount.Replace(",",".")),
                new SqlParameter("@IncomeType", GetType),
                new SqlParameter("@ServiceName", RemoveSpecialChars(Service)),
                new SqlParameter("@NameOfIncome", NameOfIncome),
                new SqlParameter("@LastUpdateby", Session["FullName"].ToString()),
                new SqlParameter("@intid", MemberID.Value),
            };

            int Complete = int.Parse(connect.SingleIntQrySQL(updatesp, updateqry));
            if (Complete > 0)
            {
                BtnReturn.Visible = false;
                NormalBack.Visible = true;
                DivGridUsers.Visible = true;
                EditDiv.Visible = false;
                RunUSers();
                SaveNotie();
            }

        }
        catch (Exception ex)
        {
            NotCompleteNotie();
            return;
        }

    }

    public string RemoveSpecialChars(string input)
    {
        return Regex.Replace(input, @"[~`!@#$%^&*()+=|\\{}':;,<>/?[\]""_-]", string.Empty);
    }

    protected void btnGoBack_ServerClick(object sender, EventArgs e)
    {
        BtnReturn.Visible = false;
        NormalBack.Visible = true;
        DivGridUsers.Visible = true;
        EditDiv.Visible = false;
    }
}