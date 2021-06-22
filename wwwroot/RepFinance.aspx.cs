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
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using System.Web.Services;

public partial class RepFinance : System.Web.UI.Page
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

    public DataTable GlobalTable()
    {
        DataTable tb = new DataTable();
        string Getqry = "SELECT intid,Name + ' '  +Surname AS [Name],convert(varchar(16),DOB,106) AS [Date of Birth],Gender As [Gender] FROM Kids WHERE churchid = '" + Session["ChurchID"].ToString() + "'  and IsActive = '1' and campus = '" + Session["Campus"].ToString() + "' ORDER BY  Name + ' ' + Surname ASC";
        return tb = connect.DTSQL(Getqry);
    }

    void RunOnLoad()
    {
        lblName.InnerText = Session["FName"].ToString();
        Loadfooter.Text = Session["Footer"].ToString();
      
        ShowAllMembers();

        RunMenus();

      //  PopulatePie();
    }

    void PopulateIncomeGraph()
    {

        GetValues.Value = "";
        GetNames.Value = "";
        string StartDate = Convert.ToDateTime(datepicker.Value).ToString("yyyy-MM-dd");
        string EndDate = Convert.ToDateTime(txtEndDate.Value).ToString("yyyy-MM-dd");
        DataTable tb = new DataTable();

        DataTable getCampus = new DataTable();
        getCampus = connect.DTSQL("SELECT Campus FROM Campus WHERE ChurchID = '1'");
        foreach (DataRow cmRow in getCampus.Rows)
        {

            tb = connect.DTSQL("SELECT CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount','Campus Name'   FROM Offering WHERE isexpense = '0' and status = '1' and campus = '" + cmRow[0].ToString() + "' -- and ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
            if (tb.Rows.Count > 0)
            {
                VisualsViewNew.Visible = true;

                //Income
                //

                titleHeader.InnerText = "Income" + "  " + StartDate + " - " + EndDate;


                HandleLabel.Value = "Income";
                foreach (DataRow rows in tb.Rows)
                {
                    GetValues.Value += rows[0].ToString() + ",";
                    GetNames.Value += cmRow[0].ToString() + ",";

                }

            }
        
        }
    
        GetValues.Value.TrimEnd(',');
        GetNames.Value.TrimEnd(',');

        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ RunIncomeFinance(); },550);", true);

        PopulateExpGraph();
    }



    void PopulateExpGraph()
    {
        GetValuesExp.Value = "";
        GetNamesExp.Value = "";
        string StartDate = Convert.ToDateTime(datepicker.Value).ToString("yyyy-MM-dd");
        string EndDate = Convert.ToDateTime(txtEndDate.Value).ToString("yyyy-MM-dd");
        DataTable tb = new DataTable();

        DataTable getCampus = new DataTable();
        getCampus = connect.DTSQL("SELECT Campus FROM Campus WHERE ChurchID = '1'");
        foreach (DataRow cmRow in getCampus.Rows)
        {

            tb = connect.DTSQL("SELECT CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount','Campus Name'   FROM Offering WHERE isexpense = '1' and status = '1' and campus = '" + cmRow[0].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
            if (tb.Rows.Count > 0)
            {
                VisualsViewNew.Visible = true;

                //Income
                //

                titleHeaderExp.InnerText = "Expense" + "  " + StartDate + " - " + EndDate;


                HandleLabelExp.Value = "Expense";
                foreach (DataRow rows in tb.Rows)
                {
                    GetValuesExp.Value += rows[0].ToString() + ",";
                    GetNamesExp.Value += cmRow[0].ToString() + ",";

                }

            }

        }

        GetValuesExp.Value.TrimEnd(',');
        GetNamesExp.Value.TrimEnd(',');

       // ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ RunIncomeFinance(); },550);", true);
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


                if (MenuName == "Reports")
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


    void PopulateVisuals6MonthsIncome(string Category)
    {
        PieLabel.InnerText = "6 Month doughnut graph visual - " + Category;
        lblBar.InnerText = "6 Month bar graph visual - " + Category;


        VisualsView.Visible = true;

        //1
        lbl1.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(3),DATENAME(mm, DATEADD(MONTH,-5,GETDATE())))");
        Value1.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   DATEPART(month ,OfferingDate) = DATEPART(month , DATEADD(DAY,-155,GETDATE())) and DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(DAY,-155,GETDATE()))");


        //2
        lbl2.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(3),DATENAME(mm, DATEADD(MONTH,-4,GETDATE())))");
        Value2.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   DATEPART(month ,OfferingDate) = DATEPART(month , DATEADD(DAY,-124,GETDATE())) and DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(DAY,-124,GETDATE()))");

        //  3
        lbl3.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(3),DATENAME(mm, DATEADD(MONTH,-3,GETDATE())))");
        Value3.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   DATEPART(month ,OfferingDate) = DATEPART(month , DATEADD(DAY,-93,GETDATE())) and DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(DAY,-93,GETDATE()))");

        ////4
        lbl4.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(3),DATENAME(mm, DATEADD(MONTH,-2,GETDATE())))");
        Value4.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   DATEPART(month ,OfferingDate) = DATEPART(month , DATEADD(DAY,-62,GETDATE())) and DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(DAY,-62,GETDATE()))");

        ////5
        lbl5.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(3),DATENAME(mm, DATEADD(MONTH,-1,GETDATE())))");
        Value5.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   DATEPART(month ,OfferingDate) = DATEPART(month , DATEADD(DAY,-31,GETDATE())) and DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(DAY,-31,GETDATE()))");

        ////6
        lbl6.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(3),DATENAME(mm, GETDATE()))");
        Value6.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   DATEPART(month ,OfferingDate) = DATEPART(month , DATEADD(DAY,-0,GETDATE())) and DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(DAY,-0,GETDATE()))");


        HeaderBar.Value = CmdCategory.SelectedItem.Text;

        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ SixDiffRep(); },550);", true);
    }

    void PopulateVisuals6MonthsIncomeByType(string Category)
    {
        PieLabel.InnerText = "6 Month doughnut graph visual - " + Category;
        lblBar.InnerText = "6 Month bar graph visual - " + Category;


        VisualsView.Visible = true;

        //1
        lbl1.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(3),DATENAME(mm, DATEADD(MONTH,-5,GETDATE())))");
        Value1.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE NameOfIncome = '" + Category + "'  and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   DATEPART(month ,OfferingDate) = DATEPART(month , DATEADD(DAY,-155,GETDATE())) and DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(DAY,-155,GETDATE()))");


        //2
        lbl2.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(3),DATENAME(mm, DATEADD(MONTH,-4,GETDATE())))");
        Value2.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE NameOfIncome = '" + Category + "'  and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   DATEPART(month ,OfferingDate) = DATEPART(month , DATEADD(DAY,-124,GETDATE())) and DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(DAY,-124,GETDATE()))");

        //  3
        lbl3.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(3),DATENAME(mm, DATEADD(MONTH,-3,GETDATE())))");
        Value3.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE NameOfIncome =  '" + Category + "'  and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   DATEPART(month ,OfferingDate) = DATEPART(month , DATEADD(DAY,-93,GETDATE())) and DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(DAY,-93,GETDATE()))");

        ////4
        lbl4.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(3),DATENAME(mm, DATEADD(MONTH,-2,GETDATE())))");
        Value4.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE NameOfIncome =  '" + Category + "'  and  isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   DATEPART(month ,OfferingDate) = DATEPART(month , DATEADD(DAY,-62,GETDATE())) and DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(DAY,-62,GETDATE()))");

        ////5
        lbl5.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(3),DATENAME(mm, DATEADD(MONTH,-1,GETDATE())))");
        Value5.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE NameOfIncome =  '" + Category + "'  and  isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   DATEPART(month ,OfferingDate) = DATEPART(month , DATEADD(DAY,-31,GETDATE())) and DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(DAY,-31,GETDATE()))");

        ////6
        lbl6.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(3),DATENAME(mm, GETDATE()))");
        Value6.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE NameOfIncome =  '" + Category + "'  and  isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   DATEPART(month ,OfferingDate) = DATEPART(month , DATEADD(DAY,-0,GETDATE())) and DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(DAY,-0,GETDATE()))");


        HeaderBar.Value = CmdCategory.SelectedItem.Text;

        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ SixDiffRep(); },550);", true);
    }

    void PopulateVisuals6yearsIncomeByType(string Category)
    {
        PieLabel.InnerText = "6 Year doughnut graph visual - " + Category;
        lblBar.InnerText = "6 Year bar graph visual- " + Category;


        VisualsView.Visible = true;

        //1
        lbl1.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(5),DATENAME(YEAR, DATEADD(YEAR,-5,GETDATE())))");
        Value1.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE  NameOfIncome = '" + Category + "'  and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'  and  DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(YEAR,-5,GETDATE()))");


        //2
        lbl2.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(5),DATENAME(YEAR, DATEADD(YEAR,-4,GETDATE())))");
        Value2.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE   NameOfIncome = '" + Category + "'  and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'  and  DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(YEAR,-4,GETDATE()))");

        //  3
        lbl3.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(5),DATENAME(YEAR, DATEADD(YEAR,-3,GETDATE())))");
        Value3.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE  NameOfIncome = '" + Category + "'  and  isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'  and  DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(YEAR,-3,GETDATE()))");

        ////4
        lbl4.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(5),DATENAME(YEAR, DATEADD(YEAR,-2,GETDATE())))");
        Value4.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE  NameOfIncome = '" + Category + "'  and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'  and  DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(YEAR,-2,GETDATE()))");

        ////5
        lbl5.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(5),DATENAME(YEAR, DATEADD(YEAR,-1,GETDATE())))");
        Value5.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE  NameOfIncome = '" + Category + "'  and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'  and  DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(YEAR,-1,GETDATE()))");

        ////6
        lbl6.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(5),DATENAME(YEAR, GETDATE()))");
        Value6.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE  NameOfIncome = '" + Category + "'  and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'  and  DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(YEAR,-0,GETDATE()))");


        HeaderBar.Value = CmdCategory.SelectedItem.Text;

        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ SixDiffRep(); },550);", true);
    }

    void PopulateVisuals6MonthsExpense(string Category)
    {
        PieLabel.InnerText = "6 Month doughnut graph visual - " + Category;
        lblBar.InnerText = "6 Month bar graph visual - " + Category;


        VisualsView.Visible = true;

        //1
        lbl1.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(3),DATENAME(mm, DATEADD(MONTH,-5,GETDATE())))");
        Value1.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   DATEPART(month ,OfferingDate) = DATEPART(month , DATEADD(DAY,-155,GETDATE())) and DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(DAY,-155,GETDATE()))");


        //2
        lbl2.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(3),DATENAME(mm, DATEADD(MONTH,-4,GETDATE())))");
        Value2.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   DATEPART(month ,OfferingDate) = DATEPART(month , DATEADD(DAY,-124,GETDATE())) and DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(DAY,-124,GETDATE()))");

        //  3
        lbl3.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(3),DATENAME(mm, DATEADD(MONTH,-3,GETDATE())))");
        Value3.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   DATEPART(month ,OfferingDate) = DATEPART(month , DATEADD(DAY,-93,GETDATE())) and DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(DAY,-93,GETDATE()))");

        ////4
        lbl4.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(3),DATENAME(mm, DATEADD(MONTH,-2,GETDATE())))");
        Value4.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   DATEPART(month ,OfferingDate) = DATEPART(month , DATEADD(DAY,-62,GETDATE())) and DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(DAY,-62,GETDATE()))");

        ////5
        lbl5.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(3),DATENAME(mm, DATEADD(MONTH,-1,GETDATE())))");
        Value5.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   DATEPART(month ,OfferingDate) = DATEPART(month , DATEADD(DAY,-31,GETDATE())) and DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(DAY,-31,GETDATE()))");

        ////6
        lbl6.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(3),DATENAME(mm, GETDATE()))");
        Value6.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   DATEPART(month ,OfferingDate) = DATEPART(month , DATEADD(DAY,-0,GETDATE())) and DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(DAY,-0,GETDATE()))");


        HeaderBar.Value = CmdCategory.SelectedItem.Text;

        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ SixDiffRep(); },550);", true);
    }


    void PopulateVisuals6yearsExpense()
    {
        PieLabel.InnerText = "6 Year doughnut graph visual - Expense";
        lblBar.InnerText = "6 Year bar graph visual- Expense";


        VisualsView.Visible = true;

        //1
        lbl1.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(5),DATENAME(YEAR, DATEADD(YEAR,-5,GETDATE())))");
        Value1.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'  and  DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(YEAR,-5,GETDATE()))");


        //2
        lbl2.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(5),DATENAME(YEAR, DATEADD(YEAR,-4,GETDATE())))");
        Value2.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'  and  DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(YEAR,-4,GETDATE()))");

        //  3
        lbl3.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(5),DATENAME(YEAR, DATEADD(YEAR,-3,GETDATE())))");
        Value3.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'  and  DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(YEAR,-3,GETDATE()))");

        ////4
        lbl4.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(5),DATENAME(YEAR, DATEADD(YEAR,-2,GETDATE())))");
        Value4.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'  and  DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(YEAR,-2,GETDATE()))");

        ////5
        lbl5.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(5),DATENAME(YEAR, DATEADD(YEAR,-1,GETDATE())))");
        Value5.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'  and  DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(YEAR,-1,GETDATE()))");

        ////6
        lbl6.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(5),DATENAME(YEAR, GETDATE()))");
        Value6.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'  and  DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(YEAR,-0,GETDATE()))");


        HeaderBar.Value = CmdCategory.SelectedItem.Text;

        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ SixDiffRep(); },550);", true);
    }
    void PopulateVisuals6yearsIncome()
    {
        PieLabel.InnerText = "6 Year doughnut graph visual - Income";
        lblBar.InnerText = "6 Year bar graph visual- Income";


        VisualsView.Visible = true;

        //1
        lbl1.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(5),DATENAME(YEAR, DATEADD(YEAR,-5,GETDATE())))");
        Value1.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'  and  DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(YEAR,-5,GETDATE()))");


        //2
        lbl2.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(5),DATENAME(YEAR, DATEADD(YEAR,-4,GETDATE())))");
        Value2.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'  and  DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(YEAR,-4,GETDATE()))");

        //  3
        lbl3.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(5),DATENAME(YEAR, DATEADD(YEAR,-3,GETDATE())))");
        Value3.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'  and  DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(YEAR,-3,GETDATE()))");

        ////4
        lbl4.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(5),DATENAME(YEAR, DATEADD(YEAR,-2,GETDATE())))");
        Value4.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'  and  DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(YEAR,-2,GETDATE()))");

        ////5
        lbl5.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(5),DATENAME(YEAR, DATEADD(YEAR,-1,GETDATE())))");
        Value5.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'  and  DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(YEAR,-1,GETDATE()))");

        ////6
        lbl6.Value = connect.SingleRespSQL("SELECT CONVERT(varchar(5),DATENAME(YEAR, GETDATE()))");
        Value6.Value = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'  and  DATEPART(YEAR ,OfferingDate) = DATEPART(YEAR , DATEADD(YEAR,-0,GETDATE()))");


        HeaderBar.Value = CmdCategory.SelectedItem.Text;

        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ SixDiffRep(); },550);", true);
    }


    void PopulateIncomeVsExpense()
    {
        PieLabel.InnerText = "Income vs Expenses doughnut graph visual";
        lblBar.InnerText = "Income vs Expenses bar graph visual";
        string StartDate = Convert.ToDateTime(datepicker.Value).ToString("yyyy-MM-dd");
        string EndDate = Convert.ToDateTime(txtEndDate.Value).ToString("yyyy-MM-dd");

        VisualsView.Visible = true;
     


        lblMale.Value = "Income";
        GetMale.Value = connect.SingleRespSQL("SELECT CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");

        lblFemale.Value = "Expense";
        GetFeMale.Value = connect.SingleRespSQL("SELECT CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");

        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ MembersGenders(); },550);", true);
    
    }





    void PopulateVisualsWeeklyAttendance()
    {
        PieLabel.InnerText = "Weekly Attendance doughnut graph visual";
        lblBar.InnerText = "Weekly Attendance bar graph visual";


        VisualsView.Visible = true;
        string StartDate = Convert.ToDateTime(datepicker.Value).ToString("yyyy-MM-dd");
        string EndDate = Convert.ToDateTime(txtEndDate.Value).ToString("yyyy-MM-dd");

        #region Benoni
        //1
        lbl1.Value = "First Time Visitors Benoni";
        Value1.Value = connect.SingleRespSQL("SELECT COUNT(intid) FROM Stats_Form WHERE IsActive = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Benoni Campus' and  MemberType = 'Visitor' and convert(varchar, ServiceDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");


        //2
        lbl2.Value = "8:00 Glory Experience Benoni";
        Value2.Value = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE Campus = 'Benoni Campus' and ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' and  TypeService = 'Glory Experience'");

        //  3
        lbl3.Value = "08:00 Builders Kidz Benoni";
        Value3.Value = connect.SingleRespSQL("SELECT COUNT(DISTINCT A.intid)  FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid =  B.KidID WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus'  and convert(varchar, B.CheckedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
        #endregion


        ////4
        lbl4.Value = "First Time Visitors Delmas";
        Value4.Value = connect.SingleRespSQL("SELECT COUNT(intid) FROM Stats_Form WHERE IsActive = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Delmas Campus' and  MemberType = 'Visitor' and convert(varchar, ServiceDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");

        ////5
        lbl5.Value = "8:00 Glory Experience Delmas";
        Value5.Value = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE Campus = 'Delmas Campus' and ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' and  TypeService = 'Glory Experience'");

        ////6
        lbl6.Value = "08:00 Builders Kidz Delmas";
        Value6.Value = connect.SingleRespSQL("SELECT COUNT(DISTINCT A.intid)  FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid =  B.KidID WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus'  and convert(varchar, B.CheckedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");


        HeaderBar.Value = CmdCategory.SelectedItem.Text;

        ClientScript.RegisterStartupScript(GetType(), "Javascript", "setTimeout(function(){ SixDiffRep(); },550);", true);
    }


    void WeeklyRep()
    {


        string StartDate = "";
        string EndDate = "";
        if (datepicker.Value == "")
        {
            NotCompleteNotie();
            return;
        }
        else if (txtEndDate.Value == "")
        {
            NotCompleteNotie();
            return;
        }
        else
        {
            StartDate = Convert.ToDateTime(datepicker.Value).ToString("yyyy-MM-dd");
            EndDate = Convert.ToDateTime(txtEndDate.Value).ToString("yyyy-MM-dd");
        }




        //  getDate = Convert.ToDateTime("2020-03-26 00:00:00.000").ToString("yyyy-MM-dd");
        string reportName = "Weekly Report" + StartDate + " - " + EndDate;





        DataTable Demo = new DataTable();
        DataTable tableEmp = new DataTable();

        string imagepath = System.AppDomain.CurrentDomain.BaseDirectory.ToString() + @"assets\media\logos\image001.png";

        Document document = new Document();
        MemoryStream memstream = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, memstream);
        document.Open();

        iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(imagepath);
        gif.ScaleToFit(100f, 100f);
        document.Add(gif);

        Paragraph header = new Paragraph("BUILDERS CHURCH WEEKLY REPORT", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
        header.Alignment = 1;

        header.SpacingAfter = 8f;
        document.Add(header);


        Paragraph headera = new Paragraph(StartDate + " - " + EndDate, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.BOLD, BaseColor.BLACK));
        headera.Alignment = 1;
        document.Add(headera);

        iTextSharp.text.Font font5 = iTextSharp.text.FontFactory.GetFont(FontFactory.HELVETICA, 5);








        int FirstRow = 1;
        int GetTotalDel = 0;
        int totRes = 0;
        int GetTotalBen = 0;
        if (FirstRow > 0)
        {
            PdfPTable NewTable = new PdfPTable(2);

            NewTable.HorizontalAlignment = 1;
            //leave a gap before and after the table
            NewTable.SpacingBefore = 20f;
            NewTable.SpacingAfter = 8f;

            PdfPCell Header = new PdfPCell(new Phrase("Attendance", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            Header.Colspan = 2;
            NewTable.AddCell(Header);


            int Ben = 1;
            if (Ben > 0)
            {
                #region
                PdfPCell statHeader = new PdfPCell(new Phrase("First Time Visitors Benoni", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader);
                #endregion

                #region

                string Value = connect.SingleRespSQL("SELECT COUNT(intid) FROM Stats_Form WHERE IsActive = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Benoni Campus' and  MemberType = 'Visitor' and convert(varchar, ServiceDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                PdfPCell statResult = new PdfPCell(new Phrase(Value, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult);
                #endregion

                #region

                PdfPCell statHeader1 = new PdfPCell(new Phrase("8:00 Glory Experience Benoni", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader1);
                #endregion

                #region row3
                string Value2 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE Campus = 'Benoni Campus' and ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' and  TypeService = 'Glory Experience'");
                PdfPCell statResult1 = new PdfPCell(new Phrase(Value2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult1);
                #endregion



                #region
                PdfPCell statHeader2 = new PdfPCell(new Phrase("08:00 Builders Kidz Benoni", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader2);
                #endregion

                #region row4
                string value3 = connect.SingleRespSQL("SELECT COUNT(DISTINCT A.intid)  FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid =  B.KidID WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Benoni Campus'  and convert(varchar, B.CheckedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                PdfPCell statResult2 = new PdfPCell(new Phrase(value3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult2);
                #endregion



                GetTotalBen = int.Parse(Value) + int.Parse(Value2) + int.Parse(value3);

            }
            int Del = 1;
            if (Del > 0)
            {
                #region
                PdfPCell statHeader = new PdfPCell(new Phrase("First Time Visitors Delmas", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader);
                #endregion

                #region

                string Value = connect.SingleRespSQL("SELECT COUNT(intid) FROM Stats_Form WHERE IsActive = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = 'Delmas Campus' and  MemberType = 'Visitor' and convert(varchar, ServiceDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                PdfPCell statResult = new PdfPCell(new Phrase(Value, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult);
                #endregion

                #region

                PdfPCell statHeader1 = new PdfPCell(new Phrase("8:00 Glory Experience Delmas", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader1);
                #endregion

                #region row3
                string Value2 = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE Campus = 'Delmas Campus' and ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' and  TypeService = 'Glory Experience'");
                PdfPCell statResult1 = new PdfPCell(new Phrase(Value2, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult1.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult1);
                #endregion



                #region
                PdfPCell statHeader2 = new PdfPCell(new Phrase("08:00 Builders Kidz Delmas", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statHeader2);
                #endregion

                #region row4
                string value3 = connect.SingleRespSQL("SELECT COUNT(DISTINCT A.intid)  FROM Kids A INNER JOIN KidsCheckedIn B ON A.intid =  B.KidID WHERE A.ChurchID = '" + Session["ChurchID"].ToString() + "' and A.campus = 'Delmas Campus'  and convert(varchar, B.CheckedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                PdfPCell statResult2 = new PdfPCell(new Phrase(value3, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult2.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                NewTable.AddCell(statResult2);
                #endregion


                GetTotalDel = int.Parse(Value) + int.Parse(Value2) + int.Parse(value3);
            }


            PdfPCell HeaderA = new PdfPCell(new Phrase("Total", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            HeaderA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

            NewTable.AddCell(HeaderA);

            totRes = GetTotalBen + GetTotalDel;
            PdfPCell HeaderAA = new PdfPCell(new Phrase(totRes.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

            NewTable.AddCell(HeaderAA);

            document.Add(NewTable);

        }


        int SecondRow = 1;
        if (SecondRow > 0)
        {
            int totvol = 0;
            PdfPTable SecondT = new PdfPTable(2);

            SecondT.HorizontalAlignment = 1;
            //leave a gap before and after the table
            SecondT.SpacingBefore = 20f;
            SecondT.SpacingAfter = 8f;

            PdfPCell Header = new PdfPCell(new Phrase("Volunteers On Duty", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            Header.Colspan = 2;
            SecondT.AddCell(Header);


            int Ben = 1;
            if (Ben > 0)
            {




                DataTable table = new DataTable();
                table.Columns.Add("DocName", typeof(string));
                table.Rows.Add("Trainers");
                table.Rows.Add("Hosts");
                table.Rows.Add("Ushers");
                table.Rows.Add("Protocol");

                table.Rows.Add("Hospitality");
                table.Rows.Add("Builders Worship");
                table.Rows.Add("Multimedia – Sound");
                table.Rows.Add("Multimedia – Video");
                table.Rows.Add("Builders Kidz");

                table.Rows.Add("J316");
                table.Rows.Add("Leadersheep");
                table.Rows.Add("Tribe Leaders");





                foreach (DataRow rows in table.Rows)
                {
                    string GetTotal = connect.SingleRespSQL("SELECT count(intid)  FROM IserveCheck WHERE Iserve = '" + rows[0].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "'  and convert(varchar, CapturedDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");

                    totvol += int.Parse(GetTotal);


                    #region
                    PdfPCell statHeader = new PdfPCell(new Phrase(rows[0].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    SecondT.AddCell(statHeader);
                    #endregion

                    #region

                    string Value = GetTotal;
                    PdfPCell statResult = new PdfPCell(new Phrase(Value, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                    statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                    SecondT.AddCell(statResult);
                    #endregion

                }




                PdfPCell HeaderA = new PdfPCell(new Phrase("Total Volunteers", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                HeaderA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

                SecondT.AddCell(HeaderA);


                PdfPCell HeaderAA = new PdfPCell(new Phrase(totvol.ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

                SecondT.AddCell(HeaderAA);

                document.Add(SecondT);



            }





        }



        int Income = 1;
        if (Income > 0)
        {
            int totvol = 0;
            PdfPTable SecondT = new PdfPTable(3);

            SecondT.HorizontalAlignment = 1;
            //leave a gap before and after the table
            SecondT.SpacingBefore = 20f;
            SecondT.SpacingAfter = 8f;

            PdfPCell Header = new PdfPCell(new Phrase("Income", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            Header.Colspan = 3;
            SecondT.AddCell(Header);


            PdfPCell Header1 = new PdfPCell(new Phrase("Name of income", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header1.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

            SecondT.AddCell(Header1);



            PdfPCell Header2 = new PdfPCell(new Phrase("Amount", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header2.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

            SecondT.AddCell(Header2);


            PdfPCell Header3 = new PdfPCell(new Phrase("Date", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header3.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

            SecondT.AddCell(Header3);



            int Ben = 1;
            if (Ben > 0)
            {




                DataTable table = new DataTable();
                string Getqry = "SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' ,  NameOfIncome,CONVERT(varchar(16),OfferingDate,106)  FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'  group by NameOfIncome,ServiceName,OfferingDate ";
                table = connect.DTSQL(Getqry);


                if (table.Rows.Count > 0)
                {
                    foreach (DataRow rows in table.Rows)
                    {


                        #region
                        PdfPCell statHeader = new PdfPCell(new Phrase(rows[1].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                        statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        SecondT.AddCell(statHeader);
                        #endregion

                        #region


                        PdfPCell statResult = new PdfPCell(new Phrase("R " + rows[0].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                        statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        SecondT.AddCell(statResult);
                        #endregion


                        #region


                        PdfPCell statResultss = new PdfPCell(new Phrase(rows[2].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                        statResultss.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        SecondT.AddCell(statResultss);
                        #endregion

                    }




                    PdfPCell HeaderA = new PdfPCell(new Phrase("Total Income", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                    HeaderA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
                    HeaderA.Colspan = 2;
                    SecondT.AddCell(HeaderA);


                    string GetTotAmount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                    PdfPCell HeaderAA = new PdfPCell(new Phrase("R " + GetTotAmount, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                    HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

                    SecondT.AddCell(HeaderAA);

                    document.Add(SecondT);
                }
                else
                {



                    PdfPCell HeaderAA = new PdfPCell(new Phrase("No Income for that period", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                    HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
                    HeaderAA.Colspan = 3;
                    SecondT.AddCell(HeaderAA);


                    document.Add(SecondT);
                }



            }





        }



        int Expense = 1;
        if (Expense > 0)
        {
            int totvol = 0;
            PdfPTable SecondT = new PdfPTable(3);

            SecondT.HorizontalAlignment = 1;
            //leave a gap before and after the table
            SecondT.SpacingBefore = 20f;
            SecondT.SpacingAfter = 8f;

            PdfPCell Header = new PdfPCell(new Phrase("Expense", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            Header.Colspan = 3;
            SecondT.AddCell(Header);


            PdfPCell Header1 = new PdfPCell(new Phrase("Name of income", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header1.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
    
            SecondT.AddCell(Header1);



            PdfPCell Header2 = new PdfPCell(new Phrase("Amount", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header2.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

            SecondT.AddCell(Header2);


            PdfPCell Header3 = new PdfPCell(new Phrase("Date", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header3.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

            SecondT.AddCell(Header3);





            int Ben = 1;
            if (Ben > 0)
            {




                DataTable table = new DataTable();
                string Getqry = "SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' ,  NameOfIncome,CONVERT(varchar(16),OfferingDate,106)  FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'  group by NameOfIncome,ServiceName,OfferingDate ";
                table = connect.DTSQL(Getqry);


                if (table.Rows.Count > 0)
                {
                    foreach (DataRow rows in table.Rows)
                    {


                        #region
                        PdfPCell statHeader = new PdfPCell(new Phrase(rows[1].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                        statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        SecondT.AddCell(statHeader);
                        #endregion

                        #region


                        PdfPCell statResult = new PdfPCell(new Phrase("R " + rows[0].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                        statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        SecondT.AddCell(statResult);
                        #endregion


                        #region


                        PdfPCell statResultss = new PdfPCell(new Phrase(rows[2].ToString(), new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                        statResultss.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                        SecondT.AddCell(statResultss);
                        #endregion

                    }




                    PdfPCell HeaderA = new PdfPCell(new Phrase("Total Income", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                    HeaderA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
                    HeaderA.Colspan = 2;
                    SecondT.AddCell(HeaderA);


                    string GetTotAmount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                    PdfPCell HeaderAA = new PdfPCell(new Phrase("R " + GetTotAmount, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                    HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

                    SecondT.AddCell(HeaderAA);

                    document.Add(SecondT);
                }
                else
                {
                    PdfPCell HeaderAA = new PdfPCell(new Phrase("No Expense for that period", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                    HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
                    HeaderAA.Colspan = 3;
                    SecondT.AddCell(HeaderAA);


                    document.Add(SecondT);
                }



            }





        }



        int Build = 1;
        if (Build > 0)
        {
            int totvol = 0;
            PdfPTable SecondT = new PdfPTable(4);

            SecondT.HorizontalAlignment = 1;
            //leave a gap before and after the table
            SecondT.SpacingBefore = 20f;
            SecondT.SpacingAfter = 8f;

            PdfPCell Header = new PdfPCell(new Phrase("Mandate Statistics", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            Header.Colspan = 2;
            SecondT.AddCell(Header);


            PdfPCell Header1 = new PdfPCell(new Phrase("No.", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header1.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            Header1.Colspan = 1;
            SecondT.AddCell(Header1);


            PdfPCell Header2 = new PdfPCell(new Phrase("Comments", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
            Header2.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);
            Header2.Colspan = 1;
            SecondT.AddCell(Header2);



            int B = 1;
            if (B > 0)
            {



                PdfPCell HeaderAA = new PdfPCell(new Phrase("B", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

                SecondT.AddCell(HeaderAA);


                #region
                PdfPCell statHeader = new PdfPCell(new Phrase("no. of souls saved", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statHeader);
                #endregion

                #region

                string Value = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                PdfPCell statResult = new PdfPCell(new Phrase(Value, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResult);
                #endregion


                #region


                PdfPCell statResultss = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultss.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResultss);
                #endregion

                //document.Add(SecondT);
            }

            int U = 1;
            if (U > 0)
            {



                PdfPCell HeaderAA = new PdfPCell(new Phrase("U", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

                SecondT.AddCell(HeaderAA);


                #region
                PdfPCell statHeader = new PdfPCell(new Phrase("attendance new believers experience", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statHeader);
                #endregion

                #region

                string Value = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' and  TypeService = 'New Believers Experience'");
                PdfPCell statResult = new PdfPCell(new Phrase(Value, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResult);
                #endregion


                #region


                PdfPCell statResultss = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultss.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResultss);
                #endregion

                // document.Add(SecondT);
            }


            int I = 1;
            if (I > 0)
            {



                PdfPCell HeaderAA = new PdfPCell(new Phrase("I", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

                SecondT.AddCell(HeaderAA);


                #region
                PdfPCell statHeader = new PdfPCell(new Phrase("no. people baptised", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statHeader);
                #endregion

                #region

                string Value = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BaptismActualSouls AS int)) is null then '0' Else SUM(CAST(BaptismActualSouls AS int)) END as 'Count' FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' and  TypeService = 'Baptism'");
                PdfPCell statResult = new PdfPCell(new Phrase(Value, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResult);
                #endregion


                #region


                PdfPCell statResultss = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultss.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResultss);
                #endregion

                //  document.Add(SecondT);
            }


            int L = 1;
            if (L > 0)
            {



                PdfPCell HeaderAA = new PdfPCell(new Phrase("L", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

                SecondT.AddCell(HeaderAA);


                #region
                PdfPCell statHeader = new PdfPCell(new Phrase("no. of iConnect group meetings", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statHeader);
                #endregion

                #region

                string Value = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(TotNumber AS int)) is null then '0' Else SUM(CAST(TotNumber AS int)) END as 'Count' FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' and convert(varchar, UploadDate, 23) between '" + StartDate + "'  and  '" + EndDate + "' and  TypeService = 'iConnectGroups'");
                PdfPCell statResult = new PdfPCell(new Phrase(Value, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResult);
                #endregion


                #region


                PdfPCell statResultss = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultss.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResultss);
                #endregion

                //  document.Add(SecondT);
            }

            int D = 1;
            if (D > 0)
            {



                PdfPCell HeaderAA = new PdfPCell(new Phrase("D", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.WHITE)));
                HeaderAA.BackgroundColor = new iTextSharp.text.BaseColor(10, 115, 181);

                SecondT.AddCell(HeaderAA);


                #region
                PdfPCell statHeader = new PdfPCell(new Phrase("no. of members trained", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statHeader.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statHeader);
                #endregion

                #region

                string Value = "0";
                PdfPCell statResult = new PdfPCell(new Phrase(Value, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResult.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResult);
                #endregion


                #region


                PdfPCell statResultss = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8f, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                statResultss.BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255);
                SecondT.AddCell(statResultss);
                #endregion


            }


            document.Add(SecondT);


        }




        document.Close();

        document.Dispose();

        Response.ContentType = "application/octet-stream";
        String Headers = "Attachment; Filename=" + reportName + ".pdf";
        Response.AppendHeader("Content-Disposition", Headers);
        Response.BinaryWrite(memstream.ToArray());
        Response.End();



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


    void ShowAllMembers()
    {

        string htmltext = "";
        string totAmount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' FROM Offering WHERE isexpense = '0' and status = '1' and  ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'");
        DataTable table = new DataTable();
        string Getqry = Getqry = "SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' ,  NameOfIncome,CONVERT(varchar(16),OfferingDate,106)  FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'  group by NameOfIncome,ServiceName,OfferingDate ";
        table = connect.DTSQL(Getqry);
        htmltext = "<table class='table table-striped- table-bordered' id='MembersT' > " +
                "<caption><h2>Total Amount : R " + totAmount + "</h2></caption>" +
                    "<thead>" +
                    "  <tr>" +
                    "    <th> Name </th> " +
                    "    <th> Amount </th> " +

                "    <th > Date</th> " +

                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {




                htmltext += " <tr> " +


                             "   <td >" + Row[1].ToString() + "</td> " +
                             "   <td >R " + Row[0].ToString() + "</td> " +
                                "   <td >" + Row[2].ToString() + "</td> " +


                        " </tr>";


            }
        }
        else
        {
            htmltext = "No Results for that search";
        }


        htmltext += "    </tbody> " +
                   " </table>";
        tbTable.Text = htmltext;
    }

    void ShowCustomMembers(string Query,string TotAmount)
    {

        string htmltext = "";
        DataTable table = new DataTable();
 
        table = connect.DTSQL(Query);
        htmltext = "<table class='table table-striped- table-bordered' id='MembersT' > " +
                 "<caption><h2>Total Amount : R " + TotAmount + "</h2></caption>" +
                     "<thead>" +
                     "  <tr>" +
                     "    <th> Name </th> " +
                     "    <th> Amount </th> " +

                 "    <th > Date</th> " +

                     "  </tr> " +
                     "</thead> " +
                     "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {




                htmltext += " <tr> " +


                             "   <td >" + Row[1].ToString() + "</td> " +
                             "   <td >R " + Row[0].ToString() + "</td> " +
                                "   <td >" + Row[2].ToString() + "</td> " +
                            
                          
                        " </tr>";


            }
        }
        else
        {
            htmltext = "No Results for that search";
        }


        htmltext += "    </tbody> " +
                   " </table>";
        tbTable.Text = htmltext;
    }

    void ShowCustomAllCampus(string Query, string TotAmount)
    {

        string htmltext = "";
        DataTable table = new DataTable();

        table = connect.DTSQL(Query);
        htmltext = "<table class='table table-striped- table-bordered' id='MembersT' > " +
                 "<caption><h2>Total Amount : R " + TotAmount + "</h2></caption>" +
                     "<thead>" +
                     "  <tr>" +
                     "    <th> Name </th> " +
                     "    <th> Amount </th> " +

                 "    <th > Date</th> " +
                        "    <th > Campus</th> " +
                     "  </tr> " +
                     "</thead> " +
                     "<tbody> ";
        if (table.Rows.Count > 0)
        {
            foreach (DataRow Row in table.Rows)
            {




                htmltext += " <tr> " +


                             "   <td >" + Row[1].ToString() + "</td> " +
                             "   <td >R " + Row[0].ToString() + "</td> " +
                                "   <td >" + Row[2].ToString() + "</td> " +
                                     "   <td >" + Row[3].ToString() + "</td> " +


                        " </tr>";


            }
        }
        else
        {
            htmltext = "No Results for that search";
        }


        htmltext += "    </tbody> " +
                   " </table>";
        tbTable.Text = htmltext;
    }

    protected void btndownloadPDF_ServerClick(object sender, EventArgs e)
    {

        WeeklyRep();

      
       
        
    }


    protected void btnSearchRep_ServerClick(object sender, EventArgs e)
    {
        VisualsView.Visible = false;
        string StartDate = "";
        string EndDate = "";
        string TotAmount = "";
        string Getqry = "";
        int AllCampus = 0;
        if (CmdCategory.SelectedValue.ToString() == "None")
        {
            NotCompleteNotie();
            return;
        }
        else
        {

            tbtlbl.InnerText = CmdCategory.SelectedItem.Text;


       


            if (CmdCategory.SelectedValue.ToString() == "Income")
            {
                #region Date Checker
                if (datepicker.Value == "")
                {
                    NotCompleteNotie();
                    return;
                }
                else if (txtEndDate.Value == "")
                {
                    NotCompleteNotie();
                    return;
                }
                else
                {
                    StartDate = Convert.ToDateTime(datepicker.Value).ToString("yyyy-MM-dd");
                    EndDate = Convert.ToDateTime(txtEndDate.Value).ToString("yyyy-MM-dd");
                }
                #endregion


                Getqry = "SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' ,  NameOfIncome,CONVERT(varchar(16),OfferingDate,106)  FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'  group by NameOfIncome,ServiceName,OfferingDate ";

                TotAmount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                PopulateIncomeVsExpense();
            }
            else if (CmdCategory.SelectedValue.ToString() == "Expense")
            {
                #region Date Checker
                if (datepicker.Value == "")
                {
                    NotCompleteNotie();
                    return;
                }
                else if (txtEndDate.Value == "")
                {
                    NotCompleteNotie();
                    return;
                }
                else
                {
                    StartDate = Convert.ToDateTime(datepicker.Value).ToString("yyyy-MM-dd");
                    EndDate = Convert.ToDateTime(txtEndDate.Value).ToString("yyyy-MM-dd");
                }
                #endregion
                Getqry = "SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' ,  NameOfIncome,CONVERT(varchar(16),OfferingDate,106)  FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'  group by NameOfIncome,ServiceName,OfferingDate ";

                TotAmount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                PopulateIncomeVsExpense();
            }
            else if (CmdCategory.SelectedValue.ToString() == "Expenses for all campuses")
            {
                AllCampus = 1;
                #region Date Checker
                if (datepicker.Value == "")
                {
                    NotCompleteNotie();
                    return;
                }
                else if (txtEndDate.Value == "")
                {
                    NotCompleteNotie();
                    return;
                }
                else
                {
                    StartDate = Convert.ToDateTime(datepicker.Value).ToString("yyyy-MM-dd");
                    EndDate = Convert.ToDateTime(txtEndDate.Value).ToString("yyyy-MM-dd");
                }
                #endregion
                Getqry = "SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' ,  NameOfIncome,CONVERT(varchar(16),OfferingDate,106),campus  FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "'  and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'  group by NameOfIncome,ServiceName,OfferingDate,campus ";

                TotAmount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "'  and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                PopulateIncomeGraph();

            }
            else if (CmdCategory.SelectedValue.ToString() == "Income for all campuses")
            {
                AllCampus = 1;
                #region Date Checker
                if (datepicker.Value == "")
                {
                    NotCompleteNotie();
                    return;
                }
                else if (txtEndDate.Value == "")
                {
                    NotCompleteNotie();
                    return;
                }
                else
                {
                    StartDate = Convert.ToDateTime(datepicker.Value).ToString("yyyy-MM-dd");
                    EndDate = Convert.ToDateTime(txtEndDate.Value).ToString("yyyy-MM-dd");
                }
                #endregion
                Getqry = "SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' ,  NameOfIncome,CONVERT(varchar(16),OfferingDate,106),campus  FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "'  and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'  group by NameOfIncome,ServiceName,OfferingDate,campus ";

                TotAmount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "'  and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
                PopulateIncomeGraph();

            }
            else if (CmdCategory.SelectedValue.ToString() == "Tithe")
            {
                #region Date Checker
                if (datepicker.Value == "")
                {
                    NotCompleteNotie();
                    return;
                }
                else if (txtEndDate.Value == "")
                {
                    NotCompleteNotie();
                    return;
                }
                else
                {
                    StartDate = Convert.ToDateTime(datepicker.Value).ToString("yyyy-MM-dd");
                    EndDate = Convert.ToDateTime(txtEndDate.Value).ToString("yyyy-MM-dd");
                }
                #endregion
                Getqry = "SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' ,  NameOfIncome,CONVERT(varchar(16),OfferingDate,106)  FROM Offering WHERE NameOfIncome = 'Tithe' and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'  group by NameOfIncome,ServiceName,OfferingDate ";

                TotAmount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE NameOfIncome = 'Tithe' and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
            }
            else if (CmdCategory.SelectedValue.ToString() == "Offering")
            {
                #region Date Checker
                if (datepicker.Value == "")
                {
                    NotCompleteNotie();
                    return;
                }
                else if (txtEndDate.Value == "")
                {
                    NotCompleteNotie();
                    return;
                }
                else
                {
                    StartDate = Convert.ToDateTime(datepicker.Value).ToString("yyyy-MM-dd");
                    EndDate = Convert.ToDateTime(txtEndDate.Value).ToString("yyyy-MM-dd");
                }
                #endregion
                Getqry = "SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' ,  NameOfIncome,CONVERT(varchar(16),OfferingDate,106)  FROM Offering WHERE NameOfIncome = 'Offering' and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'  group by NameOfIncome,ServiceName,OfferingDate ";

                TotAmount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE NameOfIncome = 'Offering' and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and convert(varchar, OfferingDate, 23) between '" + StartDate + "'  and  '" + EndDate + "'");
            }
            else if (CmdCategory.SelectedValue.ToString() == "Income for the past 6 months")
            {
                PopulateVisuals6MonthsIncome(CmdCategory.SelectedValue.ToString());
                Getqry = "SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' ,  NameOfIncome,CONVERT(varchar(16),OfferingDate,106)  FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  OfferingDate  BETWEEN DATEADD(DAY,-155,GETDATE()) AND GETDATE()  group by NameOfIncome,ServiceName,OfferingDate ";

                TotAmount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   OfferingDate  BETWEEN DATEADD(DAY,-155,GETDATE()) AND GETDATE()");
            
            }
            else if (CmdCategory.SelectedValue.ToString() == "Expense for the past 6 months")
            {
                PopulateVisuals6MonthsExpense(CmdCategory.SelectedValue.ToString());
                Getqry = "SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' ,  NameOfIncome,CONVERT(varchar(16),OfferingDate,106)  FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  OfferingDate  BETWEEN DATEADD(DAY,-155,GETDATE()) AND GETDATE()  group by NameOfIncome,ServiceName,OfferingDate ";

                TotAmount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   OfferingDate  BETWEEN DATEADD(DAY,-155,GETDATE()) AND GETDATE()");

            }
            else if (CmdCategory.SelectedValue.ToString() == "Income for the past 6 years")
            {
                PopulateVisuals6yearsIncome();
                Getqry = "SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' ,  NameOfIncome,CONVERT(varchar(16),OfferingDate,106)  FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  OfferingDate BETWEEN  DATEADD(YEAR,-5,GETDATE()) AND GETDATE()  group by NameOfIncome,ServiceName,OfferingDate ";

                TotAmount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   OfferingDate BETWEEN  DATEADD(YEAR,-5,GETDATE()) AND GETDATE()");

            }
            else if (CmdCategory.SelectedValue.ToString() == "Expense for the past 6 years")
            {
                PopulateVisuals6yearsExpense();
                Getqry = "SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' ,  NameOfIncome,CONVERT(varchar(16),OfferingDate,106)  FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  OfferingDate BETWEEN  DATEADD(YEAR,-5,GETDATE()) AND GETDATE()  group by NameOfIncome,ServiceName,OfferingDate ";

                TotAmount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE isexpense = '1' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   OfferingDate BETWEEN  DATEADD(YEAR,-5,GETDATE()) AND GETDATE()");

            }
            else if (CmdCategory.SelectedValue.ToString() == "Offering for the past 6 months")
            {
               
                Getqry = "SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' ,  NameOfIncome,CONVERT(varchar(16),OfferingDate,106)  FROM Offering WHERE NameOfIncome = 'Offering' and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  OfferingDate  BETWEEN DATEADD(DAY,-155,GETDATE()) AND GETDATE()  group by NameOfIncome,ServiceName,OfferingDate ";

                TotAmount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE NameOfIncome = 'Offering' and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   OfferingDate  BETWEEN DATEADD(DAY,-155,GETDATE()) AND GETDATE()");
                PopulateVisuals6MonthsIncomeByType("Offering");
            }
            else if (CmdCategory.SelectedValue.ToString() == "Tithe for the past 6 months")
            {

                Getqry = "SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' ,  NameOfIncome,CONVERT(varchar(16),OfferingDate,106)  FROM Offering WHERE NameOfIncome = 'Tithe' and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  OfferingDate  BETWEEN DATEADD(DAY,-155,GETDATE()) AND GETDATE()  group by NameOfIncome,ServiceName,OfferingDate ";
                TotAmount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE NameOfIncome = 'Tithe' and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   OfferingDate  BETWEEN DATEADD(DAY,-155,GETDATE()) AND GETDATE()");
                PopulateVisuals6MonthsIncomeByType("Tithe");
            }
            else if (CmdCategory.SelectedValue.ToString() == "Offering for the past 6 years")
            {
                PopulateVisuals6yearsIncomeByType("Offering");
                  Getqry = "SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' ,  NameOfIncome,CONVERT(varchar(16),OfferingDate,106)  FROM Offering WHERE NameOfIncome = 'Offering' and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  OfferingDate BETWEEN  DATEADD(YEAR,-5,GETDATE()) AND GETDATE()  group by NameOfIncome,ServiceName,OfferingDate ";

                TotAmount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE NameOfIncome = 'Offering' and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   OfferingDate BETWEEN  DATEADD(YEAR,-5,GETDATE()) AND GETDATE()");

            }
            else if (CmdCategory.SelectedValue.ToString() == "Tithe for the past 6 years")
            {
                PopulateVisuals6yearsIncomeByType("Tithe");
                Getqry = "SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' ,  NameOfIncome,CONVERT(varchar(16),OfferingDate,106)  FROM Offering WHERE NameOfIncome = 'Tithe' and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  OfferingDate BETWEEN  DATEADD(YEAR,-5,GETDATE()) AND GETDATE()  group by NameOfIncome,ServiceName,OfferingDate ";
               
                TotAmount = connect.SingleRespSQL("SELECT  CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount'   FROM Offering WHERE NameOfIncome = 'Tithe' and isexpense = '0' and status = '1' and ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and   OfferingDate BETWEEN  DATEADD(YEAR,-5,GETDATE()) AND GETDATE()");

            }

            else if (CmdCategory.SelectedValue.ToString() == "Weekly Attendance")
            {
                Getqry = "";

                PopulateVisualsWeeklyAttendance();

            }




            

        }

        if (Getqry != "")
        {
            AllDiv.Visible = true;
            if (AllCampus > 0)
            {
                ShowCustomAllCampus(Getqry, TotAmount);
            }
            else
            {

                ShowCustomMembers(Getqry, TotAmount);
            }

        
        }
        else
        {
            AllDiv.Visible = false;
        }



    }





    protected void CmdCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisualsView.Visible = false;
        if (CmdCategory.SelectedValue.ToString() == "Tithe for the past 6 years" || CmdCategory.SelectedValue.ToString() == "Tithe for the past 6 months" || CmdCategory.SelectedValue.ToString() == "Income for the past 6 months" || CmdCategory.SelectedValue.ToString() == "Expense for the past 6 months" || CmdCategory.SelectedValue.ToString() == "Income for the past 6 years" || CmdCategory.SelectedValue.ToString() == "Expense for the past 6 years" || CmdCategory.SelectedValue.ToString() == "Offering for the past 6 months" || CmdCategory.SelectedValue.ToString() == "Offering for the past 6 years")
        {
            btndownloadPDF.Visible = false;
            DivDate.Visible = false;
        }
        else
        {
            btndownloadPDF.Visible = true;
            DivDate.Visible = true;
        }
    }
}