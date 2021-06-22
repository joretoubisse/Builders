using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

public partial class Dashboard : System.Web.UI.Page
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




        isDashboardValid();

        Loadfooter.Text = Session["Footer"].ToString();
        lblName.InnerText = Session["FName"].ToString();
      
        RunDashboard();
        RunMenus();
    }


    void isDashboardValid()
    {
        string AllRoles = "";
        int valid = 0;
        DataTable GetRoles = connect.DTSQL("SELECT  AdminRole,MembersRole ,VisitorRole ,SundaySchoolRole ,AttendanceRole ,Communication,Offering,NewMembersAppRole ,ConnectGroupRole,EventsRole ,ResourceRole,MinistryRole,EvangelistRole ,ReportsRole,DashboardRole FROM ChurchUsers  WHERE intid = '" + HttpContext.Current.Session["UsersID"].ToString() + "'");
        if (GetRoles.Rows.Count > 0)
        {
            foreach (DataRow MenuRoles in GetRoles.Rows)
            {
                #region Dashboard And Logout
                //AllRoles += "'Dashboard'" + ",";
               // AllRoles += "'Logout'" + ",";
                #endregion

                if (MenuRoles[14].ToString() == "NA")
                {
                    valid = 1;
                 

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


            if (valid > 0)
            {
               string PageUrl =  connect.SingleRespSQL("SELECT TOP(1) pageurl FROM MenuItems WHERE isactive = '1' and MenuName in  (" + AllRoles.TrimEnd(',') + ") order by isordering ASC");

               Server.Transfer(PageUrl);
            }
           
        }
    
    
    
    }


    void RunDashboard()
    {
         string html = "";
         html += Members();
         html += Visitors();

         html += AttendanceGloryexperience();
         html += AttendanceBaptism();
         html += AttendanceiConnect();
         html += AttendanceNewBelieversexperience();
      
         html += Tithe();
     


         html += TotIncomeWeekly();

         html += NewConverts();
         html += UpcomingEvents();
         html += Anniversary();
         html += NumberOfConnectGrp();


        // html += SundaySchool();
        
       //  html += NewMembersApplication();
        // html += Attendance();
        

         //html += OverallOffering();
         //html += LastOffering();
        // html += Birthdays();

        
         txtDash.Text = html;
    }


    public string Members()
    {
        string returner = "";

        string GetTotalCount = connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Member' and Campus = '" + Session["Campus"].ToString() + "' and  ChurchID = '" + Session["ChurchID"].ToString() + "' ");

        returner = @"<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>
									<div class='kt-portlet kt-portlet--height-fluid kt-widget-12'>
										<div class='kt-portlet__body'>
											<div class='kt-widget-12__body'>
												<div class='kt-widget-12__head'>
													<div class='kt-widget-12__date kt-widget-12__date--success'> 
														<span class='kt-widget-12__day'>" +  GetTotalCount + "</span> " +
														@"<span class='kt-widget-12__month'></span>
													</div>
													<div class='kt-widget-12__label'>
														<h3 class='kt-widget-12__title'>Members</h3>
														<span class='kt-widget-12__desc'></span>
													</div>
												</div>
												<div class='kt-widget-12__info'>
													Total Members
												</div>
											</div>
										</div>
										<div class='kt-portlet__foot kt-portlet__foot--md'>
											<div class='kt-portlet__foot-wrapper'>
									
												<div class='kt-portlet__foot-toolbar'>
													<a href='Members.aspx' class='btn btn-default btn-sm btn-bold btn-upper'>Show More</a>
												</div>
											</div>
										</div>
									</div>
								</div>";
        return returner;
    
    
    }

    public string Visitors()
    {
        string returner = "";

        string GetTotalCount = connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Visitor'  and  DATEPART(month ,ServiceDate) = DATEPART(month , GETDATE()) and DATEPART(year , ServiceDate) = DATEPART(year , GETDATE())");

        string Month = DateTime.Now.ToString("MMMM");
        returner = @"<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>
									<div class='kt-portlet kt-portlet--height-fluid kt-widget-12'>
										<div class='kt-portlet__body'>
											<div class='kt-widget-12__body'>
												<div class='kt-widget-12__head'>
													<div class='kt-widget-12__date kt-widget-12__date--success'> 
														<span class='kt-widget-12__day'>" + GetTotalCount + "</span> " +
                                                        @"<span class='kt-widget-12__month'></span>
													</div>
													<div class='kt-widget-12__label'>
														<h3 class='kt-widget-12__title'>Visitors</h3>
														<span class='kt-widget-12__desc'></span>
													</div>
												</div>
												<div class='kt-widget-12__info'>
													Total Visitors for " + Month + 
												@" </div>
											</div>
										</div>
										<div class='kt-portlet__foot kt-portlet__foot--md'>
											<div class='kt-portlet__foot-wrapper'>
									
												<div class='kt-portlet__foot-toolbar'>
													<a href='Visitors.aspx' class='btn btn-default btn-sm btn-bold btn-upper'>Show More</a>
												</div>
											</div>
										</div>
									</div>
								</div>";
        return returner;


    }
   
    public string SundaySchool()
    {
        string returner = "";

        string GetTotalCount = connect.SingleRespSQL("SELECT COUNT(intid) FROM Kids WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' and IsActive = '1'");

        returner = @"<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>
									<div class='kt-portlet kt-portlet--height-fluid kt-widget-12'>
										<div class='kt-portlet__body'>
											<div class='kt-widget-12__body'>
												<div class='kt-widget-12__head'>
													<div class='kt-widget-12__date kt-widget-12__date--success'> 
														<span class='kt-widget-12__day'>" + GetTotalCount + "</span> " +
														@"<span class='kt-widget-12__month'></span>
													</div>
													<div class='kt-widget-12__label'>
														<h3 class='kt-widget-12__title'>Builders Kidz</h3>
														<span class='kt-widget-12__desc'></span>
													</div>
												</div>
												<div class='kt-widget-12__info'>
													Total Builders Kidz

												</div>
											</div>
										</div>
										<div class='kt-portlet__foot kt-portlet__foot--md'>
											<div class='kt-portlet__foot-wrapper'>
									
												<div class='kt-portlet__foot-toolbar'>
													<a href='Kids.aspx' class='btn btn-default btn-sm btn-bold btn-upper'>Show More</a>
												</div>
											</div>
										</div>
									</div>
								</div>";
        return returner;


    }

    public string NewMembersApplication()
    {
        string returner = "";

        string GetTotalCount = connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'NewMember' and Campus = '" + Session["Campus"].ToString() + "' and  ChurchID = '" + Session["ChurchID"].ToString() + "' ");

        returner = @"<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>
									<div class='kt-portlet kt-portlet--height-fluid kt-widget-12'>
										<div class='kt-portlet__body'>
											<div class='kt-widget-12__body'>
												<div class='kt-widget-12__head'>
													<div class='kt-widget-12__date kt-widget-12__date--success'> 
														<span class='kt-widget-12__day'>" + GetTotalCount + "</span> " +
                                                        @"<span class='kt-widget-12__month'></span>
													</div>
													<div class='kt-widget-12__label'>
														<h3 class='kt-widget-12__title'>New Membership Applications</h3>
														<span class='kt-widget-12__desc'></span>
													</div>
												</div>
												<div class='kt-widget-12__info'>
													Total New Membership Applications

												</div>
											</div>
										</div>
										<div class='kt-portlet__foot kt-portlet__foot--md'>
											<div class='kt-portlet__foot-wrapper'>
									
												<div class='kt-portlet__foot-toolbar'>
													<a href='NewMembers.aspx' class='btn btn-default btn-sm btn-bold btn-upper'>Show More</a>
												</div>
											</div>
										</div>
									</div>
								</div>";
        return returner;


    }

    public string Attendance()
    {
        string returner = "";

        int NoAttendance = int.Parse(connect.SingleRespSQL("SELECT count(intid)  FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "'"));
        if (NoAttendance > 0)
        {
            int totalMember = int.Parse(connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Member' and Campus = '" + Session["Campus"].ToString() + "' and  ChurchID = '" + Session["ChurchID"].ToString() + "' "));

            if (totalMember > 0)
            {

                string AttendanceNumber = connect.SingleRespSQL("SELECT TOP(1)  TotNumber FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "'   ORDER by intid DESC");
                string TypeOfService = connect.SingleRespSQL("SELECT TOP(1)  TypeService FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "'   ORDER by intid DESC");
                string Date = connect.SingleRespSQL("SELECT TOP(1)  CONVERT(varchar(16),UploadDate,106) FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "'   ORDER by intid DESC");

                #region Get Percentage

                if (AttendanceNumber == "")
                {
                    AttendanceNumber = "0";
                }
                int AttendanceNo = Int32.Parse(AttendanceNumber);
                double percent = (double)(AttendanceNo * 100) / totalMember;

                #endregion

                returner = @"	<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid kt-widget-13'>
										<div class='kt-portlet__body'>
											<div id='kt-widget-slider-13-2' class='kt-slider carousel slide' data-ride='carousel' data-interval='4000'>
												<div class='kt-slider__head'>
													<div class='kt-slider__label'>Attendance</div>
													<div class='kt-slider__nav'>
														
												
													</div>
												</div>
												<div class='carousel-inner'>
													<div class='carousel-item active kt-slider__body'>
														<div class='kt-widget-13'>
															<div class='kt-widget-13__body'>
																<a class='kt-widget-13__title' href='#'>" + TypeOfService + "</a> " +
                                                                        @"<div class='kt-widget-13__desc'>" + Date + "</div> " +
                                                                    @" </div>
															<div class='kt-widget-13__foot'>
																<div class='kt-widget-13__progress'>
																	<div class='kt-widget-13__progress-info'>
																		<div class='kt-widget-13__progress-status'>
																			Total Attendance : " + AttendanceNumber + "</div> " +

                                                                                @" <div class='kt-widget-13__progress-value'>" + Math.Floor(percent) + "%</div> " +
                                                                            @" </div>
																	<div class='progress'>
																		<div class='progress-bar kt-bg-brand' role='progressbar' style='width:" + Math.Floor(percent) + "%' aria-valuenow=" + Math.Floor(percent) + " aria-valuemin='0' aria-valuemax='100'></div> " +
                                                                            @" </div>
																</div>
															</div>
														</div>
													</div>
												
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
            }
            else
            {
                returner = @"	<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid'>
										<div class='kt-portlet__head'>
											<div class='kt-portlet__head-label'>
												<h3 class='kt-portlet__head-title'>ATTENDANCE </h3>

											</div>
											<div class='kt-portlet__head-toolbar'>
												<div class='kt-portlet__head-toolbar-wrapper'>
													<div class='dropdown dropdown-inline'>
														
													
													</div>
												</div>
											</div>
										</div>
										<div class='kt-portlet__body'>
											<div class='kt-widget-1'>
										
												<div class='tab-content'>
													<div class='tab-pane fade active show' id='kt_tabs_19_15d80fc3496e24' role='tabpanel'>
														<div class='kt-widget-1__item'>
															<div class='kt-widget-1__item-info'>
														
																<div class='kt-widget-1__item-desc'><h4>No Attendance</h4></div>
                                                           
															</div>
													
														</div>
													
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
            }
        }
        else
        {
            returner = @"	<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid'>
										<div class='kt-portlet__head'>
											<div class='kt-portlet__head-label'>
												<h3 class='kt-portlet__head-title'>ATTENDANCE </h3>

											</div>
											<div class='kt-portlet__head-toolbar'>
												<div class='kt-portlet__head-toolbar-wrapper'>
													<div class='dropdown dropdown-inline'>
														
													
													</div>
												</div>
											</div>
										</div>
										<div class='kt-portlet__body'>
											<div class='kt-widget-1'>
										
												<div class='tab-content'>
													<div class='tab-pane fade active show' id='kt_tabs_19_15d80fc3496e24' role='tabpanel'>
														<div class='kt-widget-1__item'>
															<div class='kt-widget-1__item-info'>
														
																<div class='kt-widget-1__item-desc'><h4>No Attendance</h4></div>
                                                           
															</div>
													
														</div>
													
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
        }
        return returner;


    }

    public string UpcomingEvents()
    {
        string returner = "";

        DataTable Ntable = new DataTable();
        string Date = "";
        string Name = "";
        string Description = "";
        Ntable = connect.DTSQL("SELECT TOP(1)   EventName,convert(varchar(16),EventDate,106),Description FROM ChurchEvent  WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "' and  EventDate >= GETDATE() ORDER by EventDate asc");
        if (Ntable.Rows.Count > 0)
        {
            foreach (DataRow rows in Ntable.Rows)
            {
                Date = rows[1].ToString();
                Name = rows[0].ToString();
                Description = rows[2].ToString();
            
            }

            returner = @"	<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid kt-widget '>
										<div class='kt-portlet__body'>
											<div id='kt-widget-slider-13-1' class='kt-slider carousel slide' data-ride='carousel' data-interval='8000'>
												<div class='kt-slider__head'>
													<div class='kt-slider__label'>Upcoming Event</div>
												
												</div>
												<div class='carousel-inner'>
													<div class='carousel-item active kt-slider__body'>
														<div class='kt-widget-13'>
															<div class='kt-widget-13__body'>
																<a class='kt-widget-13__title' href='#'>" + Name + "</a> " +
                                                             @"<div class='kt-widget-13__desc'>" + Description + "</div> " +
                                                         @"</div>
															<div class='kt-widget-13__foot'>
																<div class='kt-widget-13__label'>
																	<div class='btn btn-sm btn-label btn-bold'> " + Date + "</div> " +
                                                             @" </div>
																<div class='kt-widget-13__toolbar'>
																	<a href='Events.aspx' class='btn btn-default btn-sm btn-bold btn-upper'>More Events</a>
																</div>
															</div>
														</div>
													</div>
													
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
        }
        else
        {
            Name = "No Upcoming Event";

            string GetTotalCount = "0";

            returner = @"<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>
									<div class='kt-portlet kt-portlet--height-fluid kt-widget-12'>
										<div class='kt-portlet__body'>
											<div class='kt-widget-12__body'>
												<div class='kt-widget-12__head'>
													<div class='kt-widget-12__date kt-widget-12__date--success'> 
														<span class='kt-widget-12__day'>" + GetTotalCount + "</span> " +
                                                            @"<span class='kt-widget-12__month'></span>
													</div>
													<div class='kt-widget-12__label'>
														<h3 class='kt-widget-12__title'>Upcoming Events</h3>
														<span class='kt-widget-12__desc'></span>
													</div>
												</div>
												<div class='kt-widget-12__info'>
													No Events

												</div>
											</div>
										</div>
										<div class='kt-portlet__foot kt-portlet__foot--md'>
											<div class='kt-portlet__foot-wrapper'>
									
												<div class='kt-portlet__foot-toolbar'>
													<a href='Events.aspx' class='btn btn-default btn-sm btn-bold btn-upper'>Add Events</a>
												</div>
											</div>
										</div>
									</div>
								</div>";
        }
     
        return returner;


    }

    public string Birthdays()
    {
        string returner = "";
        DataTable Ntable = new DataTable();
        string Name = "";
        Ntable = connect.DTSQL("SELECT Name +  ' ' + Surname  FROM Stats_Form WHERE CONVERT(varchar(16),DOB,106) =  CONVERT(varchar(16),GETDATE(),106) and IsActive = '1' and  membertype = 'Member' and Campus = '" + Session["Campus"].ToString() + "' and  ChurchID = '" + Session["ChurchID"].ToString() + "' ");
        if (Ntable.Rows.Count > 0)
        {


             returner = @"<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>
									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid'>
										<div class='kt-portlet__head'>
											<div class='kt-portlet__head-label'>
												<h3 class='kt-portlet__head-title'>TODAY BIRTHDAY </h3>

											</div>
											<div class='kt-portlet__head-toolbar'>
												<div class='kt-portlet__head-toolbar-wrapper'>
													<div class='dropdown dropdown-inline'>
															<div class='kt-widget-13__toolbar'>
																	<a href='Messages.aspx' class='btn btn-default btn-sm btn-bold btn-upper'>Send A Message</a>
																</div>
													</div>
												</div>
											</div>
										</div>
										<div class='kt-portlet__body'>
											<div class='kt-widget-1'>
										
												<div class='tab-content'>
													<div class='tab-pane fade active show' id='kt_tabs_19_15d80fc3496e24' role='tabpanel'>
														<div class='kt-widget-1__item'>
															<div class='kt-widget-1__item-info'>";

                                                            foreach (DataRow rows in Ntable.Rows)
                                                            {
                                                                Name = rows[0].ToString();
                                                                returner +=@"<div class='kt-widget-1__item-desc'><h4>" +  Name+ "</h4></div>";

                                                            }

                                                            returner += @"</div>
													
														</div>
													
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
        }
        else
        {
            returner = @"	<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid'>
										<div class='kt-portlet__head'>
											<div class='kt-portlet__head-label'>
												<h3 class='kt-portlet__head-title'>BIRTHDAY </h3>

											</div>
											<div class='kt-portlet__head-toolbar'>
												<div class='kt-portlet__head-toolbar-wrapper'>
													<div class='dropdown dropdown-inline'>
														
													
													</div>
												</div>
											</div>
										</div>
										<div class='kt-portlet__body'>
											<div class='kt-widget-1'>
										
												<div class='tab-content'>
													<div class='tab-pane fade active show' id='kt_tabs_19_15d80fc3496e24' role='tabpanel'>
														<div class='kt-widget-1__item'>
															<div class='kt-widget-1__item-info'>
														
																<div class='kt-widget-1__item-desc'><h4>No birthdays today</h4></div>
                                                           
															</div>
													
														</div>
													
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";

        }
       
        return returner;


    }

    public string OverallOffering()
    {
        string returner = "";

        string GetTotalCount = connect.SingleRespSQL("SELECT CASE when SUM(amount) is null THEN '0' ELSE  SUM(amount) END AS 'Amount' FROM Offering WHERE churchid= '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + "'");

        returner = @"<div class='col-lg-4 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--fit kt-portlet--height-fluid'>
										<div class='kt-portlet__body kt-portlet__body--fluid'>
											<div class='kt-widget-3 kt-widget-3--brand'>
												<div class='kt-widget-3__content'>
													<div class='kt-widget-3__content-info'>
														<div class='kt-widget-3__content-section'>
															<div class='kt-widget-3__content-title'>Overall Offering</div>
															<div class='kt-widget-3__content-desc'></div>
														</div>
														<div class='kt-widget-3__content-section'>
															<span class='kt-widget-3__content-bedge'>R</span>
															<span class='kt-widget-3__content-number'>" + GetTotalCount + "<span></span></span> " +
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

    public string LastOffering()
    {
        string returner = "";


        int CheckIfOvering = int.Parse(connect.SingleRespSQL("SELECT COUNT(intid) FROM Offering WHERE churchid= '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + "'"));
        if (CheckIfOvering > 0)
        {
            string GetTotalCount = connect.SingleRespSQL("SELECT TOP(1) CASE when  amount is null THEN '0' ELSE  amount END AS 'Amount'  FROM Offering WHERE churchid= '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + "' ORDER by intid DESC ");
            string Date = connect.SingleRespSQL("SELECT TOP(1) CONVERT(varchar(16),Offeringdate,106)  FROM Offering WHERE churchid= '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + "' ORDER by intid DESC ");
            returner = @"	<div class='col-lg-4 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--fit kt-portlet--height-fluid'>
										<div class='kt-portlet__body kt-portlet__body--fluid'>
											<div class='kt-widget-3 kt-widget-3--danger'>
												<div class='kt-widget-3__content'>
													<div class='kt-widget-3__content-info'>
														<div class='kt-widget-3__content-section'>
															<div class='kt-widget-3__content-title'>Offering</div>
	                                                      
															<div class='kt-widget-2__content-desc'>" + Date + "</div> " +
                                                            @" </div>
														<div class='kt-widget-3__content-section'>
															<span class='kt-widget-3__content-bedge'>R</span>
															<span class='kt-widget-3__content-number'>" + GetTotalCount + "<span></span></span> " +
                                                            @" </div>
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
        }
        else
        {
            //string GetTotalCount = connect.SingleRespSQL("SELECT TOP(1) CASE when  amount is null THEN '0' ELSE  amount END AS 'Amount'  FROM Offering WHERE churchid= '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + "' ORDER by intid DESC ");
            //string Date = connect.SingleRespSQL("SELECT TOP(1) CONVERT(varchar(16),Offeringdate,106)  FROM Offering WHERE churchid= '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + "' ORDER by intid DESC ");
            returner = @"	<div class='col-lg-4 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--fit kt-portlet--height-fluid'>
										<div class='kt-portlet__body kt-portlet__body--fluid'>
											<div class='kt-widget-3 kt-widget-3--danger'>
												<div class='kt-widget-3__content'>
													<div class='kt-widget-3__content-info'>
														<div class='kt-widget-3__content-section'>
															<div class='kt-widget-3__content-title'>Offering</div>
	                                                      
															<div class='kt-widget-1__content-desc'>No Offering</div> " +
                                                            @" </div>
													
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
        }
        
     
        return returner;


    }


    public string AttendanceGloryexperience()
    {
        string returner = "";
        string TypeOfService = "Glory Experience";
        int NoAttendance = int.Parse(connect.SingleRespSQL("SELECT count(intid)  FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' AND TypeService = 'Glory Experience'"));
        if (NoAttendance > 0)
        {
            int totalMember = int.Parse(connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Member' and Campus = '" + Session["Campus"].ToString() + "' and  ChurchID = '" + Session["ChurchID"].ToString() + "' "));

            if (totalMember > 0)
            {

                string AttendanceNumber = connect.SingleRespSQL("SELECT TOP(1)  TotNumber FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' AND TypeService = 'Glory Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "'   ORDER by intid DESC");
            
                string Date = connect.SingleRespSQL("SELECT TOP(1)  CONVERT(varchar(16),UploadDate,106) FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' AND TypeService = 'Glory Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "'   ORDER by intid DESC");

                #region Get Percentage

                if (AttendanceNumber == "")
                {
                    AttendanceNumber = "0";
                }
                int AttendanceNo = Int32.Parse(AttendanceNumber);
                double percent = (double)(AttendanceNo * 100) / totalMember;

                #endregion

                returner = @"	<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid kt-widget-13'>
										<div class='kt-portlet__body'>
											<div id='kt-widget-slider-13-2' class='kt-slider carousel slide' data-ride='carousel' data-interval='4000'>
												<div class='kt-slider__head'>
													<div class='kt-slider__label'>Attendance</div>
													<div class='kt-slider__nav'>
														
												
													</div>
												</div>
												<div class='carousel-inner'>
													<div class='carousel-item active kt-slider__body'>
														<div class='kt-widget-13'>
															<div class='kt-widget-13__body'>
																<a class='kt-widget-13__title' href='#'>" + TypeOfService + "</a> " +
                                                                        @"<div class='kt-widget-13__desc'>" + Date + "</div> " +
                                                                    @" </div>
															<div class='kt-widget-13__foot'>
																<div class='kt-widget-13__progress'>
																	<div class='kt-widget-13__progress-info'>
																		<div class='kt-widget-13__progress-status'>
																			Total Attendance : " + AttendanceNumber + "</div> " +

                                                                                @" <div class='kt-widget-13__progress-value'>" + Math.Floor(percent) + "%</div> " +
                                                                            @" </div>
																	<div class='progress'>
																		<div class='progress-bar kt-bg-brand' role='progressbar' style='width:" + Math.Floor(percent) + "%' aria-valuenow=" + Math.Floor(percent) + " aria-valuemin='0' aria-valuemax='100'></div> " +
                                                                            @" </div>
																</div>
															</div>
														</div>
													</div>
												
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
            }
            else
            {
                returner = @"	<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid'>
										<div class='kt-portlet__head'>
											<div class='kt-portlet__head-label'>
												<h3 class='kt-portlet__head-title'>" +  TypeOfService + " </h3> " + 

										@"	</div>
											<div class='kt-portlet__head-toolbar'>
												<div class='kt-portlet__head-toolbar-wrapper'>
													<div class='dropdown dropdown-inline'>
														
													
													</div>
												</div>
											</div>
										</div>
										<div class='kt-portlet__body'>
											<div class='kt-widget-1'>
										
												<div class='tab-content'>
													<div class='tab-pane fade active show' id='kt_tabs_19_15d80fc3496e24' role='tabpanel'>
														<div class='kt-widget-1__item'>
															<div class='kt-widget-1__item-info'>
														
																<div class='kt-widget-1__item-desc'><h4>No Attendance</h4></div>
                                                           
															</div>
													
														</div>
													
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
            }
        }
        else
        {
            returner = @"	<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid'>
										<div class='kt-portlet__head'>
											<div class='kt-portlet__head-label'>
												<h3 class='kt-portlet__head-title'>" + TypeOfService + " </h3> " +

                                         @"	</div>
											<div class='kt-portlet__head-toolbar'>
												<div class='kt-portlet__head-toolbar-wrapper'>
													<div class='dropdown dropdown-inline'>
														
													
													</div>
												</div>
											</div>
										</div>
										<div class='kt-portlet__body'>
											<div class='kt-widget-1'>
										
												<div class='tab-content'>
													<div class='tab-pane fade active show' id='kt_tabs_19_15d80fc3496e24' role='tabpanel'>
														<div class='kt-widget-1__item'>
															<div class='kt-widget-1__item-info'>
														
																<div class='kt-widget-1__item-desc'><h4>No Attendance</h4></div>
                                                           
															</div>
													
														</div>
													
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
        }
        return returner;


    }

    public string AttendanceBaptism()
    {
        string returner = "";
        string TypeOfService = "Baptism";
        int NoAttendance = int.Parse(connect.SingleRespSQL("SELECT count(intid)  FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' AND TypeService = 'Baptism'"));
        if (NoAttendance > 0)
        {
            int totalMember = int.Parse(connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Member' and Campus = '" + Session["Campus"].ToString() + "' and  ChurchID = '" + Session["ChurchID"].ToString() + "' "));

            if (totalMember > 0)
            {

                string AttendanceNumber = connect.SingleRespSQL("SELECT TOP(1)  TotNumber FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' AND TypeService = 'Baptism' and ChurchID = '" + Session["ChurchID"].ToString() + "'   ORDER by intid DESC");

                string Date = connect.SingleRespSQL("SELECT TOP(1)  CONVERT(varchar(16),UploadDate,106) FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' AND TypeService = 'Baptism' and ChurchID = '" + Session["ChurchID"].ToString() + "'   ORDER by intid DESC");

                #region Get Percentage

                if (AttendanceNumber == "")
                {
                    AttendanceNumber = "0";
                }
                int AttendanceNo = Int32.Parse(AttendanceNumber);
                double percent = (double)(AttendanceNo * 100) / totalMember;

                #endregion

                returner = @"	<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid kt-widget-13'>
										<div class='kt-portlet__body'>
											<div id='kt-widget-slider-13-2' class='kt-slider carousel slide' data-ride='carousel' data-interval='4000'>
												<div class='kt-slider__head'>
													<div class='kt-slider__label'>Attendance</div>
													<div class='kt-slider__nav'>
														
												
													</div>
												</div>
												<div class='carousel-inner'>
													<div class='carousel-item active kt-slider__body'>
														<div class='kt-widget-13'>
															<div class='kt-widget-13__body'>
																<a class='kt-widget-13__title' href='#'>" + TypeOfService + "</a> " +
                                                                        @"<div class='kt-widget-13__desc'>" + Date + "</div> " +
                                                                    @" </div>
															<div class='kt-widget-13__foot'>
																<div class='kt-widget-13__progress'>
																	<div class='kt-widget-13__progress-info'>
																		<div class='kt-widget-13__progress-status'>
																			Total Attendance : " + AttendanceNumber + "</div> " +

                                                                                @" <div class='kt-widget-13__progress-value'>" + Math.Floor(percent) + "%</div> " +
                                                                            @" </div>
																	<div class='progress'>
																		<div class='progress-bar kt-bg-brand' role='progressbar' style='width:" + Math.Floor(percent) + "%' aria-valuenow=" + Math.Floor(percent) + " aria-valuemin='0' aria-valuemax='100'></div> " +
                                                                            @" </div>
																</div>
															</div>
														</div>
													</div>
												
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
            }
            else
            {
                returner = @"	<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid'>
										<div class='kt-portlet__head'>
											<div class='kt-portlet__head-label'>
												<h3 class='kt-portlet__head-title'>" + TypeOfService + " </h3> " +

                                        @"	</div>
											<div class='kt-portlet__head-toolbar'>
												<div class='kt-portlet__head-toolbar-wrapper'>
													<div class='dropdown dropdown-inline'>
														
													
													</div>
												</div>
											</div>
										</div>
										<div class='kt-portlet__body'>
											<div class='kt-widget-1'>
										
												<div class='tab-content'>
													<div class='tab-pane fade active show' id='kt_tabs_19_15d80fc3496e24' role='tabpanel'>
														<div class='kt-widget-1__item'>
															<div class='kt-widget-1__item-info'>
														
																<div class='kt-widget-1__item-desc'><h4>No Attendance</h4></div>
                                                           
															</div>
													
														</div>
													
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
            }
        }
        else
        {
            returner = @"	<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid'>
										<div class='kt-portlet__head'>
											<div class='kt-portlet__head-label'>
												<h3 class='kt-portlet__head-title'>" + TypeOfService + " </h3> " +

                                           @"	</div>
											<div class='kt-portlet__head-toolbar'>
												<div class='kt-portlet__head-toolbar-wrapper'>
													<div class='dropdown dropdown-inline'>
														
													
													</div>
												</div>
											</div>
										</div>
										<div class='kt-portlet__body'>
											<div class='kt-widget-1'>
										
												<div class='tab-content'>
													<div class='tab-pane fade active show' id='kt_tabs_19_15d80fc3496e24' role='tabpanel'>
														<div class='kt-widget-1__item'>
															<div class='kt-widget-1__item-info'>
														
																<div class='kt-widget-1__item-desc'><h4>No Attendance</h4></div>
                                                           
															</div>
													
														</div>
													
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
        }
        return returner;


    }

    public string AttendanceiConnect()
    {
        string returner = "";
        string TypeOfService = "iConnect Experience";
        int NoAttendance = int.Parse(connect.SingleRespSQL("SELECT count(intid)  FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' AND TypeService = 'iConnect Experience'"));
        if (NoAttendance > 0)
        {
            int totalMember = int.Parse(connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Member' and Campus = '" + Session["Campus"].ToString() + "' and  ChurchID = '" + Session["ChurchID"].ToString() + "' "));

            if (totalMember > 0)
            {

                string AttendanceNumber = connect.SingleRespSQL("SELECT TOP(1)  TotNumber FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' AND TypeService = 'iConnect Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "'   ORDER by intid DESC");

                string Date = connect.SingleRespSQL("SELECT TOP(1)  CONVERT(varchar(16),UploadDate,106) FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' AND TypeService = 'iConnect Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "'   ORDER by intid DESC");

                #region Get Percentage

                if (AttendanceNumber == "")
                {
                    AttendanceNumber = "0";
                }
                int AttendanceNo = Int32.Parse(AttendanceNumber);
                double percent = (double)(AttendanceNo * 100) / totalMember;

                #endregion

                returner = @"	<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid kt-widget-13'>
										<div class='kt-portlet__body'>
											<div id='kt-widget-slider-13-2' class='kt-slider carousel slide' data-ride='carousel' data-interval='4000'>
												<div class='kt-slider__head'>
													<div class='kt-slider__label'>Attendance</div>
													<div class='kt-slider__nav'>
														
												
													</div>
												</div>
												<div class='carousel-inner'>
													<div class='carousel-item active kt-slider__body'>
														<div class='kt-widget-13'>
															<div class='kt-widget-13__body'>
																<a class='kt-widget-13__title' href='#'>" + TypeOfService + "</a> " +
                                                                        @"<div class='kt-widget-13__desc'>" + Date + "</div> " +
                                                                    @" </div>
															<div class='kt-widget-13__foot'>
																<div class='kt-widget-13__progress'>
																	<div class='kt-widget-13__progress-info'>
																		<div class='kt-widget-13__progress-status'>
																			Total Attendance : " + AttendanceNumber + "</div> " +

                                                                                @" <div class='kt-widget-13__progress-value'>" + Math.Floor(percent) + "%</div> " +
                                                                            @" </div>
																	<div class='progress'>
																		<div class='progress-bar kt-bg-brand' role='progressbar' style='width:" + Math.Floor(percent) + "%' aria-valuenow=" + Math.Floor(percent) + " aria-valuemin='0' aria-valuemax='100'></div> " +
                                                                            @" </div>
																</div>
															</div>
														</div>
													</div>
												
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
            }
            else
            {
                returner = @"	<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid'>
										<div class='kt-portlet__head'>
											<div class='kt-portlet__head-label'>
												<h3 class='kt-portlet__head-title'>" + TypeOfService + " </h3> " +

                                        @"	</div>
											<div class='kt-portlet__head-toolbar'>
												<div class='kt-portlet__head-toolbar-wrapper'>
													<div class='dropdown dropdown-inline'>
														
													
													</div>
												</div>
											</div>
										</div>
										<div class='kt-portlet__body'>
											<div class='kt-widget-1'>
										
												<div class='tab-content'>
													<div class='tab-pane fade active show' id='kt_tabs_19_15d80fc3496e24' role='tabpanel'>
														<div class='kt-widget-1__item'>
															<div class='kt-widget-1__item-info'>
														
																<div class='kt-widget-1__item-desc'><h4>No Attendance</h4></div>
                                                           
															</div>
													
														</div>
													
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
            }
        }
        else
        {
            returner = @"	<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid'>
										<div class='kt-portlet__head'>
											<div class='kt-portlet__head-label'>
												<h3 class='kt-portlet__head-title'>" + TypeOfService + " </h3> " +

                                         @"	</div>
											<div class='kt-portlet__head-toolbar'>
												<div class='kt-portlet__head-toolbar-wrapper'>
													<div class='dropdown dropdown-inline'>
														
													
													</div>
												</div>
											</div>
										</div>
										<div class='kt-portlet__body'>
											<div class='kt-widget-1'>
										
												<div class='tab-content'>
													<div class='tab-pane fade active show' id='kt_tabs_19_15d80fc3496e24' role='tabpanel'>
														<div class='kt-widget-1__item'>
															<div class='kt-widget-1__item-info'>
														
																<div class='kt-widget-1__item-desc'><h4>No Attendance</h4></div>
                                                           
															</div>
													
														</div>
													
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
        }
        return returner;


    }

    public string AttendanceNewBelieversexperience()
    {
        string returner = "";
        string TypeOfService = "New Believers experience";
        int NoAttendance = int.Parse(connect.SingleRespSQL("SELECT count(intid)  FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString() + "' AND TypeService = 'New Believers Experience'"));
        if (NoAttendance > 0)
        {
            int totalMember = int.Parse(connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Member' and Campus = '" + Session["Campus"].ToString() + "' and  ChurchID = '" + Session["ChurchID"].ToString() + "' "));

            if (totalMember > 0)
            {

                string AttendanceNumber = connect.SingleRespSQL("SELECT TOP(1)  TotNumber FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' AND TypeService = 'New Believers Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "'   ORDER by intid DESC");

                string Date = connect.SingleRespSQL("SELECT TOP(1)  CONVERT(varchar(16),UploadDate,106) FROM Attendance WHERE Campus = '" + Session["Campus"].ToString() + "' AND TypeService = 'New Believers Experience' and ChurchID = '" + Session["ChurchID"].ToString() + "'   ORDER by intid DESC");

                #region Get Percentage

                if (AttendanceNumber == "")
                {
                    AttendanceNumber = "0";
                }
                int AttendanceNo = Int32.Parse(AttendanceNumber);
                double percent = (double)(AttendanceNo * 100) / totalMember;

                #endregion

                returner = @"	<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid kt-widget-13'>
										<div class='kt-portlet__body'>
											<div id='kt-widget-slider-13-2' class='kt-slider carousel slide' data-ride='carousel' data-interval='4000'>
												<div class='kt-slider__head'>
													<div class='kt-slider__label'>Attendance</div>
													<div class='kt-slider__nav'>
														
												
													</div>
												</div>
												<div class='carousel-inner'>
													<div class='carousel-item active kt-slider__body'>
														<div class='kt-widget-13'>
															<div class='kt-widget-13__body'>
																<a class='kt-widget-13__title' href='#'>" + TypeOfService + "</a> " +
                                                                        @"<div class='kt-widget-13__desc'>" + Date + "</div> " +
                                                                    @" </div>
															<div class='kt-widget-13__foot'>
																<div class='kt-widget-13__progress'>
																	<div class='kt-widget-13__progress-info'>
																		<div class='kt-widget-13__progress-status'>
																			Total Attendance : " + AttendanceNumber + "</div> " +

                                                                                @" <div class='kt-widget-13__progress-value'>" + Math.Floor(percent) + "%</div> " +
                                                                            @" </div>
																	<div class='progress'>
																		<div class='progress-bar kt-bg-brand' role='progressbar' style='width:" + Math.Floor(percent) + "%' aria-valuenow=" + Math.Floor(percent) + " aria-valuemin='0' aria-valuemax='100'></div> " +
                                                                            @" </div>
																</div>
															</div>
														</div>
													</div>
												
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
            }
            else
            {
                returner = @"	<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid'>
										<div class='kt-portlet__head'>
											<div class='kt-portlet__head-label'>
												<h3 class='kt-portlet__head-title'>" + TypeOfService + " </h3> " +

                                        @"	</div>
											<div class='kt-portlet__head-toolbar'>
												<div class='kt-portlet__head-toolbar-wrapper'>
													<div class='dropdown dropdown-inline'>
														
													
													</div>
												</div>
											</div>
										</div>
										<div class='kt-portlet__body'>
											<div class='kt-widget-1'>
										
												<div class='tab-content'>
													<div class='tab-pane fade active show' id='kt_tabs_19_15d80fc3496e24' role='tabpanel'>
														<div class='kt-widget-1__item'>
															<div class='kt-widget-1__item-info'>
														
																<div class='kt-widget-1__item-desc'><h4>No Attendance</h4></div>
                                                           
															</div>
													
														</div>
													
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
            }
        }
        else
        {
            returner = @"	<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid'>
										<div class='kt-portlet__head'>
											<div class='kt-portlet__head-label'>
												<h3 class='kt-portlet__head-title'>" + TypeOfService + " </h3> " +

                                         @"	</div>
											<div class='kt-portlet__head-toolbar'>
												<div class='kt-portlet__head-toolbar-wrapper'>
													<div class='dropdown dropdown-inline'>
														
													
													</div>
												</div>
											</div>
										</div>
										<div class='kt-portlet__body'>
											<div class='kt-widget-1'>
										
												<div class='tab-content'>
													<div class='tab-pane fade active show' id='kt_tabs_19_15d80fc3496e24' role='tabpanel'>
														<div class='kt-widget-1__item'>
															<div class='kt-widget-1__item-info'>
														
																<div class='kt-widget-1__item-desc'><h4>No Attendance</h4></div>
                                                           
															</div>
													
														</div>
													
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
        }
        return returner;


    }

    public string Tithe()
    {
        string returner = "";

        string GetTotalCount = connect.SingleRespSQL("SELECT count(intid) FROM Stats_Form WHERE Tithe = 'Yes'  and IsActive = '1' and  membertype = 'Member' and Campus = '" + Session["Campus"].ToString() + "' and  ChurchID = '" + Session["ChurchID"].ToString() + "' ");

        returner = @"<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>
									<div class='kt-portlet kt-portlet--height-fluid kt-widget-12'>
										<div class='kt-portlet__body'>
											<div class='kt-widget-12__body'>
												<div class='kt-widget-12__head'>
													<div class='kt-widget-12__date kt-widget-12__date--success'> 
														<span class='kt-widget-12__day'>" + GetTotalCount + "</span> " +
                                                        @"<span class='kt-widget-12__month'></span>
													</div>
													<div class='kt-widget-12__label'>
														<h3 class='kt-widget-12__title'>Tithe</h3>
														<span class='kt-widget-12__desc'></span>
													</div>
												</div>
												<div class='kt-widget-12__info'>
													Total Members who tithe
												</div>
											</div>
										</div>
										<div class='kt-portlet__foot kt-portlet__foot--md'>
											<div class='kt-portlet__foot-wrapper'>
									
												<div class='kt-portlet__foot-toolbar'>
													<a href='MembersTithe.aspx' class='btn btn-default btn-sm btn-bold btn-upper'>Show Members</a>
												</div>
											</div>
										</div>
									</div>
								</div>";
        return returner;


    }

    public string Anniversary()
    {
        string returner = "";
        DataTable Ntable = new DataTable();
        string Name = "";
        Ntable = connect.DTSQL("SELECT Name +  ' ' + Surname, CONVERT(Varchar(16),MarriedDate,106) FROM Stats_Form WHERE IsActive = '1' and  membertype = 'Member' and Campus = '" + Session["Campus"].ToString() + "' and  ChurchID = '" + Session["ChurchID"].ToString() + "' and DATEPART(month ,MarriedDate) = DATEPART(month , GETDATE())  and MarriedDate <> '1900-01-01 00:00:00.000'");
       
        
        
        if (Ntable.Rows.Count > 0)
        {


            returner = @"<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>
									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid'>
										<div class='kt-portlet__head'>
											<div class='kt-portlet__head-label'>
												<h3 class='kt-portlet__head-title'>Anniversaries </h3>

											</div>
											<div class='kt-portlet__head-toolbar'>
												<div class='kt-portlet__head-toolbar-wrapper'>
													<div class='dropdown dropdown-inline'>
															<div class='kt-widget-13__toolbar'>
																	<a href='Messages.aspx' class='btn btn-default btn-sm btn-bold btn-upper'>Send A Message</a>
																</div>
													</div>
												</div>
											</div>
										</div>
										<div class='kt-portlet__body'>
											<div class='kt-widget-1'>
										
												<div class='tab-content'>
													<div class='tab-pane fade active show' id='kt_tabs_19_15d80fc3496e24' role='tabpanel'>
														<div class='kt-widget-1__item'>
															<div class='kt-widget-1__item-info'>";

            foreach (DataRow rows in Ntable.Rows)
            {
                Name = rows[0].ToString();

                returner += @"<div class='kt-widget-1__item-desc'><h4>" + Name + "</h4> </div>";
                returner += @"<div class='kt-widget-1__item-desc'><h5>" + rows[1].ToString() + "</h5> <Br /></div>";

            }

            returner += @"</div>
													
														</div>
													
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
        }
        else
        {
            returner = @"	<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid'>
										<div class='kt-portlet__head'>
											<div class='kt-portlet__head-label'>
												<h3 class='kt-portlet__head-title'>Anniversaries </h3>

											</div>
											<div class='kt-portlet__head-toolbar'>
												<div class='kt-portlet__head-toolbar-wrapper'>
													<div class='dropdown dropdown-inline'>
														
													
													</div>
												</div>
											</div>
										</div>
										<div class='kt-portlet__body'>
											<div class='kt-widget-1'>
										
												<div class='tab-content'>
													<div class='tab-pane fade active show' id='kt_tabs_19_15d80fc3496e24' role='tabpanel'>
														<div class='kt-widget-1__item'>
															<div class='kt-widget-1__item-info'>
														
																<div class='kt-widget-1__item-desc'><h4>No Anniversaries for this month</h4></div>
                                                           
															</div>
													
														</div>
													
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";

        }

        return returner;


    }


    public string TotIncomeWeekly()
    {
        string returner = "";
        string GetTotalCount = "";

        int CheckIfOvering = int.Parse(connect.SingleRespSQL("SELECT COUNT(intid) FROM Offering WHERE status = '1' and IsExpense = '0' and churchid= '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + "'"));
        if (CheckIfOvering > 0)
        {
            try
            {
                GetTotalCount = connect.SingleRespSQL("SELECT SUM(Amount)  FROM Offering WHERE  status = '1' and IsExpense = '0' and  OfferingDate BETWEEN DATEADD(DAY,-8,GETDATE()) AND GETDATE() and churchid= '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + "'");

                float F = float.Parse(GetTotalCount, CultureInfo.InvariantCulture.NumberFormat);
                decimal dvalue = Convert.ToDecimal(F);


                decimal x = Math.Round(dvalue, 2);

                GetTotalCount = x.ToString();
            
            }
            catch (Exception)
            {
                GetTotalCount = "0";
            }


            returner = @"	<div class='col-lg-4 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--fit kt-portlet--height-fluid'>
										<div class='kt-portlet__body kt-portlet__body--fluid'>
											<div class='kt-widget-3 kt-widget-3--danger'>
												<div class='kt-widget-3__content'>
													<div class='kt-widget-3__content-info'>
														<div class='kt-widget-3__content-section'>
															<div class='kt-widget-3__content-title'>Weekly Income</div>
	                                                      
														</div>
														<div class='kt-widget-3__content-section'>
															<span class='kt-widget-3__content-bedge'>R</span>
															<span class='kt-widget-3__content-number'>" + GetTotalCount + "<span></span></span> " +
                                                            @" </div>
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
        }
        else
        {
            //string GetTotalCount = connect.SingleRespSQL("SELECT TOP(1) CASE when  amount is null THEN '0' ELSE  amount END AS 'Amount'  FROM Offering WHERE churchid= '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + "' ORDER by intid DESC ");
            //string Date = connect.SingleRespSQL("SELECT TOP(1) CONVERT(varchar(16),Offeringdate,106)  FROM Offering WHERE churchid= '" + Session["ChurchID"].ToString() + "' and campus = '" + Session["Campus"].ToString() + "' ORDER by intid DESC ");
            returner = @"	<div class='col-lg-4 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--fit kt-portlet--height-fluid'>
										<div class='kt-portlet__body kt-portlet__body--fluid'>
											<div class='kt-widget-3 kt-widget-3--danger'>
												<div class='kt-widget-3__content'>
													<div class='kt-widget-3__content-info'>
														<div class='kt-widget-3__content-section'>
															<div class='kt-widget-3__content-title'>Income</div>
	                                                      
															<div class='kt-widget-1__content-desc'>No Income</div> " +
                                                            @" </div>
													
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";
        }


        return returner;


    }

    public string NumberOfConnectGrp()
    {
        string returner = "";
        DataTable Ntable = new DataTable();
        string Name = "";
        Ntable = connect.DTSQL("SELECT distinct Zone FROM ChurchZone WHERE ChurchID = '" + Session["ChurchID"].ToString() + "' and Campus = '" + Session["Campus"].ToString() + "'");

   
        if (Ntable.Rows.Count > 0)
        {


            returner = @"<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>
									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid'>
										<div class='kt-portlet__head'>
											<div class='kt-portlet__head-label'>
												<h3 class='kt-portlet__head-title'>Connect Groups </h3>

											</div>
											<div class='kt-portlet__head-toolbar'>
												<div class='kt-portlet__head-toolbar-wrapper'>
													<div class='dropdown dropdown-inline'>
															<div class='kt-widget-13__toolbar'>
																	<a href='ConnectGroups.aspx' class='btn btn-default btn-sm btn-bold btn-upper'>View Connect Groups</a>
																</div>
													</div>
												</div>
											</div>
										</div>
										<div class='kt-portlet__body'>
											<div class='kt-widget-1'>";




            returner += "<table class='table table-striped- table-bordered' id='AA' > " +
                    "<thead>" +
                    "  <tr>" +
                 
                    "    <th> Zone </th> " +

                     "    <th > Total Groups</th> " +
            
    
                    "  </tr> " +
                    "</thead> " +
                    "<tbody> ";


            foreach (DataRow rows in Ntable.Rows)
            {
                Name = rows[0].ToString();
                string GroupCount = connect.SingleRespSQL("SELECT COUNT(intid)  FROM iConnect WHERE Campus = '" + Session["Campus"].ToString() + "' and ChurchID = '" + Session["ChurchID"].ToString()  + "' and IsActive = '1' and Zone = '" + Name + "'");

                returner += " <tr> ";
                returner += "   <td >" + Name + "</td> " +
                "   <td >" + GroupCount + "</td> " +


           " </tr>";

            }



            returner += "    </tbody> " +
                " </table>";

            returner += @"</div>
													
														</div>
													
													</div>
											
								
									<!--end::Portlet-->
								</div>";
        }
        else
        {
            returner = @"	<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>

									<!--begin::Portlet-->
									<div class='kt-portlet kt-portlet--height-fluid'>
										<div class='kt-portlet__head'>
											<div class='kt-portlet__head-label'>
												<h3 class='kt-portlet__head-title'>Connect Groups </h3>

											</div>
											<div class='kt-portlet__head-toolbar'>
												<div class='kt-portlet__head-toolbar-wrapper'>
													<div class='dropdown dropdown-inline'>
														
													
													</div>
												</div>
											</div>
										</div>
										<div class='kt-portlet__body'>
											<div class='kt-widget-1'>
										
												<div class='tab-content'>
													<div class='tab-pane fade active show' id='kt_tabs_19_15d80fc3496e24' role='tabpanel'>
														<div class='kt-widget-1__item'>
															<div class='kt-widget-1__item-info'>
														
																<div class='kt-widget-1__item-desc'><h4>No Connect Groups</h4></div>
                                                           
															</div>
													
														</div>
													
													</div>
											
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>";

        }

        return returner;


    }


    public string NewConverts()
    {
        string returner = "";

        string GetTotalCount = connect.SingleRespSQL("SELECT CASE WHEN SUM(CAST(BornAgain AS int)) is null then '0' Else SUM(CAST(BornAgain AS int)) END as 'Count' FROM Attendance  WHERE ChurchID = '1' and Campus = 'Delmas Campus' and   DATEPART(month ,UploadDate) = DATEPART(month , GETDATE()) and DATEPART(year , UploadDate) = DATEPART(year , GETDATE())");

        string Month = DateTime.Now.ToString("MMMM");
        returner = @"<div class='col-lg-6 col-xl-4 order-lg-1 order-xl-1'>
									<div class='kt-portlet kt-portlet--height-fluid kt-widget-12'>
										<div class='kt-portlet__body'>
											<div class='kt-widget-12__body'>
												<div class='kt-widget-12__head'>
													<div class='kt-widget-12__date kt-widget-12__date--success'> 
														<span class='kt-widget-12__day'>" + GetTotalCount + "</span> " +
                                                        @"<span class='kt-widget-12__month'></span>
													</div>
													<div class='kt-widget-12__label'>
														<h3 class='kt-widget-12__title'>New converts</h3>
														<span class='kt-widget-12__desc'></span>
													</div>
												</div>
												<div class='kt-widget-12__info'>
													Total New converts  for " + Month +
                                                @" </div>
											</div>
										</div>
										<div class='kt-portlet__foot kt-portlet__foot--md'>
											<div class='kt-portlet__foot-wrapper'>
									
												<div class='kt-portlet__foot-toolbar'>
													<a href='ReportsDash.aspx' class='btn btn-default btn-sm btn-bold btn-upper'>View Reports</a>
												</div>
											</div>
										</div>
									</div>
								</div>";
        return returner;


    }
   

  
    void logthefile(string msg)
    {
        string path = Path.Combine(ConfigurationManager.AppSettings["Logsfilelocation"], "Dashboard.txt");
        #region Local
        using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path, true))
        {
            writer.WriteLine(msg);
        }
        #endregion
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
				 html +=@"</h3>
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
               

                if (MenuName == "Dashboard")
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
    #endregion
 

   
}