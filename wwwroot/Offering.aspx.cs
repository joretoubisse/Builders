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

public partial class Offering : System.Web.UI.Page
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
        Loadfooter.Text = Session["Footer"].ToString();
        lblName.InnerText = Session["FName"].ToString();
        if (Session["ShowAll"].ToString() == "Yes")
        {
            DivCollectOffering.Visible = true;
        }
        else
        {
            if (Session["Offering"].ToString() == "1")
            {
                DivCollectOffering.Visible = true;
            }
        }

        if (Session["Offering"].ToString() == "NA")
        {
            Server.Transfer("Dashboard.aspx");
        }

        RunDashboard();
        RunMenus();
    }


    void RunDashboard()
    {
        string html = "";
        html += Combined();
        html += Income();
        html += Expense();
   
    

        txtDash.Text = html;
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

    public string Combined()
    {
        string returner = "";

        decimal Total = 0;

      string IncomeCleanUp =   connect.SingleRespSQL("SELECT CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'  FROM Offering WHERE isexpense = '0' and ChurchID = '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + " ' and status = '1'");
      string ExpCleanUp = connect.SingleRespSQL("SELECT CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'  FROM Offering WHERE isexpense = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + " ' and status = '1'");
     
      decimal Income = decimal.Parse(IncomeCleanUp);
      decimal Expense = decimal.Parse(ExpCleanUp);
 
      Total = Income - Expense;

            returner = @"	<div class='col-lg-4 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--fit kt-portlet--height-fluid'>
										<div class='kt-portlet__body kt-portlet__body--fluid'>
											<div class='kt-widget-3 kt-widget-3--danger'>
												<div class='kt-widget-3__content'>
													<div class='kt-widget-3__content-info'>
														<div class='kt-widget-3__content-section'>
															<div class='kt-widget-3__content-title'>Balance</div>
	                                                      
														</div>
														<div class='kt-widget-3__content-section'>
															<span class='kt-widget-3__content-bedge'>R</span>
															<span class='kt-widget-3__content-number'>" + Total + "<span></span></span> " +
                                                            @" </div>
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
  

        return returner;


    }
    public string Income()
    {
        string returner = "";

        string GetTotalCount = connect.SingleRespSQL("SELECT CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'  FROM Offering WHERE isexpense = '0' and ChurchID = '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + " ' and status = '1'");
        IncomeID.Value = GetTotalCount;
        returner = @"<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>
									<div class='kt-portlet kt-portlet--height-fluid kt-widget-12'>
										<div class='kt-portlet__body'>
											<div class='kt-widget-12__body'>
												<div class='kt-widget-12__head'>
											
													<div class='kt-widget-12__label'>
														<h3 class='kt-widget-12__title'>R " + GetTotalCount + "</h3> ";
        returner += @"	<span class='kt-widget-12__desc'></span>
													</div>
												</div>
												<div class='kt-widget-12__info'>
													Total Income
												</div>
											</div>
										</div>
										<div class='kt-portlet__foot kt-portlet__foot--md'>
											<div class='kt-portlet__foot-wrapper'>
									
												<div class='kt-portlet__foot-toolbar'>
													<a href='Income.aspx' class='btn btn-default btn-sm btn-bold btn-upper'>Manage Income</a>
												</div>
											</div>
										</div>
									</div>
								</div>";
        return returner;


    }

    public string Expense()
    {
        string returner = "";
        string GetTotalCount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'  FROM Offering WHERE isexpense = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + " ' and status = '1'");
        ExpensesID.Value = GetTotalCount;

        returner = @"<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>
									<div class='kt-portlet kt-portlet--height-fluid kt-widget-12'>
										<div class='kt-portlet__body'>
											<div class='kt-widget-12__body'>
												<div class='kt-widget-12__head'>
											
													<div class='kt-widget-12__label'>
														<h3 class='kt-widget-12__title'>R " + GetTotalCount + "</h3> ";
                                       returner += @"	<span class='kt-widget-12__desc'></span>
													</div>
												</div>
												<div class='kt-widget-12__info'>
													Total Expenses
												</div>
											</div>
										</div>
										<div class='kt-portlet__foot kt-portlet__foot--md'>
											<div class='kt-portlet__foot-wrapper'>
									
												<div class='kt-portlet__foot-toolbar'>
													<a href='Expenses.aspx' class='btn btn-default btn-sm btn-bold btn-upper'>Manage Expenses</a>
												</div>
											</div>
										</div>
									</div>
								</div>";
        return returner;


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

    void PopulateOffering()
    {

        string htmltext = "";
        DataTable table = new DataTable();
        string Getqry = "";
        Getqry = "SELECT  intid, incometype, ServiceName,NameOfIncome,amount,CONVERT(Varchar(16),offeringDate,106),capturedBy FROM Offering WHERE  ChurchID = '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + "' ORDER BY  intid DESC";
       
        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +

                        "    <th> Type</th> " +
                        "    <th> Service</th> " +
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

                htmltext += " <tr> " +
                       "<td ><center><a onclick='BtnViewMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> View </a>	&nbsp;	&nbsp;	&nbsp;    <a onclick='BtnArchMem(this.id);' id='" + Row[0].ToString() + "' class='btn btn-secondary'> Remove </a> </center></td>" +
                             "   <td >" + Row[1].ToString() + "</td> " +
                                 "   <td >" + Row[2].ToString() + "</td> " +
                                     "   <td >" + Row[3].ToString() + "</td> " +
                                         "   <td >R " + Row[4].ToString() + "</td> " +
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

    protected void BtnSave_ServerClick(object sender, EventArgs e)
    {

        int complete = connect.SingleIntSQL("INSERT INTO Offering (ChurchID,OfferingDate,UploadDate,Amount,campus)VALUES ('" + Session["ChurchID"].ToString() + "', '" + txtAmountDate.Value + "',GETDATE(),'" + txtAmount.Value + "','" + Session["Campus"].ToString() + "')");
        if (complete > 0)
        {
            txtAmountDate.Value = "";
            txtAmount.Value = "";
            PopulateOffering();
            SaveNotie();
        }
    }
}