<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RepFinance.aspx.cs" Inherits="RepFinance" %>


<!DOCTYPE html>

<html lang="en">
<head runat="server">
		<base href="">
		<meta charset="utf-8" />
		<title>Builders Church</title>
		<meta name="description" content="Base form control examples">
		<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
		<meta http-equiv="X-UA-Compatible" content="IE=edge" />

		<!--begin::Fonts -->
		<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700|Raleway:300,400,500,600,700">
      
		<!--end::Fonts -->
     

    	<!--begin::Page Vendors Styles(used by this page) -->
		<link href="assets/plugins/custom/datatables/datatables.bundle.css" rel="stylesheet" type="text/css" />

		<!--begin::Global Theme Styles(used by all pages) -->
		<link href="assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/style.bundle.css" rel="stylesheet" type="text/css" />
  
		<!--end::Global Theme Styles -->

		<!--begin::Layout Skins(used by all pages) -->
		<link href="assets/css/skins/header/navy.css" rel="stylesheet" type="text/css" />

		<!--end::Layout Skins -->
     <link rel="shortcut icon" href="assets/media/logos/favicon.ico" />

     <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">

 
  <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>


    
          
</head>
<!-- begin::Body -->
	<body id="Body1" runat="server"  class="kt-quick-panel--right kt-demo-panel--right kt-offcanvas-panel--right kt-header--fixed kt-header-mobile--fixed kt-subheader--fixed kt-aside--enabled kt-aside--left kt-aside--fixed kt-aside--offcanvas-default kt-page--loading">
     

         <form id="form1" runat="server" class="kt-quick-panel--right kt-demo-panel--right kt-offcanvas-panel--right kt-header--fixed kt-header-mobile--fixed kt-subheader--fixed kt-aside--enabled kt-aside--left kt-aside--fixed kt-aside--offcanvas-default kt-page--loading">
         <input runat="server" id="IsAdmin" type="hidden" />
           <input runat="server" id="MemberID" type="hidden" />
     <input runat="server" id="HeaderBar" type="hidden" />
			 <input runat="server" id="lblMale" type="hidden" />
                <input runat="server" id="lblFemale" type="hidden" />



                   <input runat="server" id="GetValues" type="hidden" />
			   <input runat="server" id="GetNames" type="hidden" />
			 	   <input runat="server" id="HandleLabel" type="hidden" />

              <input runat="server" id="GetValuesExp" type="hidden" />
			   <input runat="server" id="GetNamesExp" type="hidden" />
			 	   <input runat="server" id="HandleLabelExp" type="hidden" />




			 	 <input runat="server" id="lbl1" type="hidden" />
			  <input runat="server" id="lbl2" type="hidden" />
			  <input runat="server" id="lbl3" type="hidden" />
			   <input runat="server" id="lbl4" type="hidden" />
			 <input runat="server" id="lbl5" type="hidden" />
			  <input runat="server" id="lbl6" type="hidden" />


			 <input runat="server" id="Value1" type="hidden" />
			  <input runat="server" id="Value2" type="hidden" />
			 	 <input runat="server" id="Value3" type="hidden" />
			  <input runat="server" id="Value4" type="hidden" />
			 	 <input runat="server" id="Value5" type="hidden" />
			  <input runat="server" id="Value6" type="hidden" />

               <input runat="server" id="GetStartDate" type="hidden" />
                <input runat="server" id="GetEndDate" type="hidden" />
			     <input runat="server" id="GetMale" type="hidden" />
              <input runat="server" id="GetFeMale" type="hidden" />
         <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieSave" style="display:none;"> Success</button>
         <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieRemove" style="display:none;"> Success</button>
           <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="RemoveMember" style="display:none;"> Success</button>
          <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieComplete" style="display:none;"> Success</button>
          <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieInvalidID" style="display:none;"> Success</button>
          <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieAccessRight" style="display:none;"> Success</button>
            <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieInvalidEmail" style="display:none;"> Success</button>
          <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieInvalidPass" style="display:none;"> Success</button>
              <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="ExistUserNotie" style="display:none;"> Success</button>

           


		<!-- begin:: Page -->

		<!-- begin:: Header Mobile -->
		<div id="kt_header_mobile" class="kt-header-mobile  kt-header-mobile--fixed ">
			<div class="kt-header-mobile__logo">
				<a href="Dashboard.aspx">
				<img alt="Logo" width="55px"  src="assets/media/logos/logo-1.png" />
				</a>
			</div>
			<div class="kt-header-mobile__toolbar" runat="server" visible="false">
				<button class="kt-header-mobile__toolbar-toggler" id="kt_header_mobile_toggler"><span></span></button>
				<button class="kt-header-mobile__toolbar-topbar-toggler" id="kt_header_mobile_topbar_toggler"><i class="flaticon-more"></i></button>
			</div>
		</div>

		<!-- end:: Header Mobile -->
		<div class="kt-grid kt-grid--hor kt-grid--root">
			<div class="kt-grid__item kt-grid__item--fluid kt-grid kt-grid--ver kt-page">
				<div class="kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor kt-wrapper" id="kt_wrapper">

                       
					<!-- begin:: Header -->
					<div id="kt_header" class="kt-header kt-grid__item kt-grid kt-grid--ver  kt-header--fixed ">

						<!-- begin:: Brand -->
						<div class="kt-header__brand   kt-grid__item" id="kt_header_brand">
							<a href="Dashboard.aspx">
								<img alt="Logo" width="75px"  src="assets/media/logos/logo-1.png" />
							</a>
						</div>

						<!-- end:: Brand -->

						<!-- begin: Header Menu -->
						<button class="kt-header-menu-wrapper-close" id="kt_header_menu_mobile_close_btn"><i class="la la-close"></i></button>
						<div class="kt-header-menu-wrapper kt-grid__item" id="kt_header_menu_wrapper">
							<div id="kt_header_menu" class="kt-header-menu kt-header-menu-mobile ">
							<ul class="kt-menu__nav " runat="server" visible="false">
									<li class="kt-menu__item  kt-menu__item--submenu kt-menu__item--rel" data-ktmenu-submenu-toggle="click" aria-haspopup="true"><a href="Dashboard.aspx" class="kt-menu__link kt-menu"><span class="kt-menu__link-text">Dashboard</span><i class="kt-menu__hor-arrow"></i><i class="kt-menu__ver-arrow la la-angle-right"></i></a>
								
									</li>




                                       <li class="kt-menu__item  kt-menu__item--submenu kt-menu__item--rel" data-ktmenu-submenu-toggle="click" aria-haspopup="true"><a href="Members.aspx" class="kt-menu__link kt-menu"><span class="kt-menu__link-text">Members</span><i class="kt-menu__hor-arrow"></i><i class="kt-menu__ver-arrow la la-angle-right"></i></a></li>
                                    	<li class="kt-menu__item  kt-menu__item--submenu kt-menu__item--rel" ><a href="Offering.aspx" class="kt-menu__link kt-menu"><span class="kt-menu__link-text">Offering</span><i class="kt-menu__hor-arrow"></i><i class="kt-menu__ver-arrow la la-angle-right"></i></a>
								
									</li>


                                     	<li class="kt-menu__item  kt-menu__item--submenu kt-menu__item--rel" data-ktmenu-submenu-toggle="click" aria-haspopup="true"><a href="Messages.aspx" class="kt-menu__link kt-menu"><span class="kt-menu__link-text">Communication</span><i class="kt-menu__hor-arrow"></i><i class="kt-menu__ver-arrow la la-angle-right"></i></a>
								
									</li>
                                    
                                 
                                    	<li class="kt-menu__item  kt-menu__item--submenu kt-menu__item--rel" data-ktmenu-submenu-toggle="click" aria-haspopup="true"><a href="logout.aspx" class="kt-menu__link kt-menu"><span class="kt-menu__link-text">Logout</span><i class="kt-menu__hor-arrow"></i><i class="kt-menu__ver-arrow la la-angle-right"></i></a>
								
									</li>


								</ul>
							</div>
						</div>

						<!-- end: Header Menu -->

						<!-- begin:: Header Topbar -->
						<div class="kt-header__topbar kt-grid__item kt-grid__item--fluid">

				


							<!--begin: User bar -->
							<div class="kt-header__topbar-item kt-header__topbar-item--user" id="kt_offcanvas_toolbar_profile_toggler_btn">
								<div class="kt-header__topbar-welcome">
								
								</div>
								<div class="kt-header__topbar-username">
							       <label runat="server" id="lblName"></label>
								</div>
								<div class="kt-header__topbar-wrapper">
									<img alt="Pic" src="assets/media/users/default.jpg" />
								</div>
							</div>

							<!--end: User bar -->
						</div>

						<!-- end:: Header Topbar -->
					</div>

					<!-- end:: Header -->

					<!-- begin:: Subheader -->
					<div id="kt_subheader" class="kt-subheader kt-grid__item ">
						<div class="kt-container  kt-container--fluid ">

							<!-- begin:: Subheader Title -->
							<div class="kt-subheader__title">
								<button class="kt-subheader__toggler kt-subheader__toggler--left" id="kt_aside_toggler"><span></span></button>
								<div class="kt-subheader__breadcrumbs">
								&nbsp;&nbsp;&nbsp;&nbsp;<label class="kt-subheader__breadcrumbs-link kt-subheader__breadcrumbs-link--home" runat="server" id="PageTitle"></label>
								
								</div>
							</div>

							<!-- end:: Subheader Title -->

							<!-- begin:: Sub-header toolbar -->
							<div class="kt-subheader__toolbar">
								<div class="kt-subheader__toolbar-wrapper">
								<a href="ReportsDash.aspx" id="BackDivs"  runat="server" class="btn btn-default btn-sm btn-bold btn-upper">Back</a>
                                    	<a href="ManageGroup.aspx" id="BackDiv" visible="false" runat="server" class="btn btn-default btn-sm btn-bold btn-upper">Back</a>
							<div class="dropdown dropdown-inline" data-toggle="kt-tooltip" title="Quick actions" data-placement="top" runat="server" visible="false" id="DivAddMember">
										<a href="#" class="btn btn-icon btn btn-label btn-label-brand btn-bold" data-toggle="dropdown" data-offset="0,5px" aria-haspopup="true" aria-expanded="false">
											<i class="flaticon2-add-1"></i>
										</a>
										<div class="dropdown-menu dropdown-menu-sm dropdown-menu-right dropdown-menu-anim">
								
										</div>
									</div>
								</div>
							</div>

							<!-- end:: Sub-header toolbar -->
						</div>
					</div>

					<!-- end:: Subheader -->
					
                    
                    
                    



                    	<div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" id="DivGridUsers" runat="server" visible="true">

						<!-- begin:: Content -->
						<div class="kt-container  kt-grid__item kt-grid__item--fluid">
							<div class="row">
								<div class="col-md-12">

                                       <br />   <br />
                                    				<!--begin::Portlet-->
									<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">  <label runat="server" id="HeadLabel">Weekly Report</label> </h3>
											</div>
										</div>
                                         

										<!--begin::Form-->
										<form class="kt-form">

											<div class="kt-portlet__body">

                                                 	<div class="form-group form-group-last">
													<div class="alert alert-secondary" role="alert">
														<div class="alert-icon"><i class="flaticon-warning kt-font-brand"></i></div>
														<div class="alert-text">
															This reports is divided in three sections. <br />
															Section 1 : Search criteria<br />
															Section 2 : Visualisation if applicable<br />
															Section 3 : Table detail of data (Download PDF and excel )
														</div>
													</div>
												</div>
											
                                           
                                            <div runat="server" id="DivNormalDate">
 	                                                    <div class="form-group" style="display:none">
                                                         
													<label>Start Date  *</label>
													<input type="text" class="form-control" runat="server"  id="datepicker" autocomplete="off" aria-describedby="Name" placeholder="Enter Start Date">

                                                             	
													</div>


                                                    <div class="form-group" style="display:none">
                                                         
													<label>End Date  *</label>
													<input type="text" class="form-control" runat="server"  id="txtEndDate" autocomplete="off" aria-describedby="Name" placeholder="Enter End Date">

                                                             	
													</div>


												
                                                			<div class="form-group">
												<label>&nbsp;&nbsp;&nbsp;Category *</label>
											<div class="col-lg-12 col-md-12 col-sm-12">
										<asp:DropDownList ID="CmdCategory" OnSelectedIndexChanged="CmdCategory_SelectedIndexChanged" AutoPostBack="true"   runat="server" CssClass="custom-select form-control">
										<asp:ListItem Value="None">Please Select</asp:ListItem>
										<asp:ListItem Value="Income">Income</asp:ListItem>
										<asp:ListItem Value="Expenses for all campuses">Expenses for all campuses</asp:ListItem>
                                            	<asp:ListItem Value="Income for all campuses">Income for all campuses</asp:ListItem>
										<asp:ListItem Value="Expense">Expense</asp:ListItem>
										<asp:ListItem Value="Tithe">Tithe</asp:ListItem>
										<asp:ListItem Value="Offering">Offering</asp:ListItem>
                                         <asp:ListItem Value="Weekly Attendance">Weekly Attendance</asp:ListItem>
                                        <asp:ListItem Value="Offering for the past 6 months">Offering for the past 6 months</asp:ListItem>
                                        <asp:ListItem Value="Offering for the past 6 years">Offering for the past 6 years</asp:ListItem>
                                                 <asp:ListItem Value="Tithe for the past 6 months">Tithe for the past 6 months</asp:ListItem>
                                        <asp:ListItem Value="Tithe for the past 6 years">Tithe for the past 6 years</asp:ListItem>
										<asp:ListItem Value="Income for the past 6 months">Income for the past 6 months</asp:ListItem>
										<asp:ListItem Value="Expense for the past 6 months">Expense for the past 6 months</asp:ListItem>
			                        	<asp:ListItem Value="Income for the past 6 years">Income for the past 6 years</asp:ListItem>
										<asp:ListItem Value="Expense for the past 6 years">Expense for the past 6 years</asp:ListItem>
                                        </asp:DropDownList>


											</div>
										</div>


                                                			<div class="form-group" runat="server" id="DivDate" visible="true">
												<label>&nbsp;&nbsp;&nbsp;Select date range *</label>
											<div class="col-lg-12 col-md-12 col-sm-12">
												<div class='input-group pull-right' id='TestDate'>
													<input type='text' class="form-control " readonly placeholder="Select date range" />
													<div class="input-group-append">
														<span class="input-group-text"><i class="la la-calendar-check-o"></i></span> 
													</div>
                                                    <br />
												</div>
											</div>
										</div>
                                                </div>
                                        

                                                           <div class="form-group">
                                                               <br />
                                                               <button type="reset" runat="server" id="btnSearchRep" onserverclick="btnSearchRep_ServerClick"  visible="true"  class="btn btn-primary pull-right">Search</button>
													<button type="reset" runat="server" id="btndownloadPDF" visible="true" onserverclick="btndownloadPDF_ServerClick" class="btn btn-secondary pull-right">Download PDF</button>
                                                               </div>
                                    
                                                	
											
											</div>
										
										</form>







										<!--end::Form-->
									</div>





                              

									
								</div>

							</div>




                            	<div class="row" id="VisualsViewNew" runat="server" visible="false" >
								<div class="col-md-6">
										<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title"><label runat="server" id="titleHeader"></label></h3>
											</div>
										</div>

										<!--begin::Form-->
									
											<div class="kt-portlet__body">
											
												       <div class="form-group">
                                         <canvas id="chartNew" width="80" height="80"></canvas>  
														   </div>
											</div>
									
										
										<!--end::Form-->
									</div>

									</div>


                                    <div class="col-md-6">
										<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title"><label runat="server" id="titleHeaderExp"></label></h3>
											</div>
										</div>

										<!--begin::Form-->
									
											<div class="kt-portlet__body">
											
												       <div class="form-group">
                                         <canvas id="chartExp" width="80" height="80"></canvas>  
														   </div>
											</div>
									
										
										<!--end::Form-->
									</div>

									</div>







									</div>






								<div class="row" id="VisualsView" runat="server" visible="false" >
								<div class="col-md-6">
										<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title"><label runat="server" id="PieLabel" ></label></h3>
											</div>
										</div>

										<!--begin::Form-->
										<form class="kt-form">
											<div class="kt-portlet__body">
											
												       <div class="form-group">
                                         <canvas id="chart" width="150" height="150"></canvas>  
														   </div>
											</div>
									
										</form>

										<!--end::Form-->
									</div>

									</div>


										<div class="col-md-6">
										<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title"><label runat="server" id="lblBar" ></label></h3>
											</div>
										</div>

										<!--begin::Form-->
										<form class="kt-form">
											<div class="kt-portlet__body">
											

                                               
                                                <div class="form-group">
							 <canvas id="BarCharts" width="150" height="150"></canvas>  
													</div>
                                        



                            

											
											</div>
									
										</form>

										<!--end::Form-->
									</div>

									</div>








									</div>

							<div class="row" runat="server" id="AllDiv">
									<div class="col-md-12" >
												<!--begin::Portlet-->
									<div class="kt-portlet" >
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title"><label runat="server" id="tbtlbl" >Income</label></h3>
											</div>

                                        
										</div>


										<!--begin::Form-->
										<form class="kt-form">
											<div class="kt-portlet__body">
											

                                                  <div class="table-responsive">
											       <asp:Literal ID="tbTable" runat="server"></asp:Literal>
													</div>
											</div>
										
										</form>

										<!--end::Form-->
									</div>

									<!--end::Portlet-->
										</div>
								</div>








						</div>

						<!-- end:: Content -->
					</div>


                    	
             


					<!-- begin:: Footer -->
					<div class="kt-footer kt-grid__item" id="kt_footer" style="bottom:0; width:100%;">
						<div class="kt-container ">
							<div class="kt-footer__copyright">
							   <asp:Literal ID="Loadfooter" runat="server"></asp:Literal>
							</div>
							<div class="kt-footer__menu">
								
							</div>
						</div>
					</div>

					<!-- end:: Footer -->
				</div>
			</div>
		</div>

		<!-- end:: Page -->

		<!-- begin:: Aside Footer -->
		<div class="kt-aside  kt-aside--fixed " id="kt_aside">
		 <asp:Literal ID="MenuStream" runat="server"></asp:Literal>
		</div>



             		<div id="kt_offcanvas_toolbar_profile" class="kt-offcanvas-panel" runat="server" visible="false">
			<div class="kt-offcanvas-panel__head">
				<h3 class="kt-offcanvas-panel__title">
				Campus
				</h3>
				<a href="#" class="kt-offcanvas-panel__close" id="kt_offcanvas_toolbar_profile_close"><i class="flaticon2-delete"></i></a>
			</div>
			<div class="kt-offcanvas-panel__body">
			
			
				<div class="kt-widget-1">
					<div class="kt-widget-1__items">

                        	 <asp:Literal ID="ShowAllCampus" runat="server"></asp:Literal>

				

					
					</div>
				</div>
		
			
			</div>
		</div>

		<!-- end:: Aside -->


		<!-- begin:: Scrolltop -->
		<div id="kt_scrolltop" class="kt-scrolltop">
			<i class="la la-arrow-up"></i>
		</div>

		<!-- end:: Scrolltop -->

           </form>

	
		<!--begin::Global Theme Bundle(used by all pages) -->
		
   


        <script src="assets/plugins/global/plugins.bundle.js" type="text/javascript"></script>
		<script src="assets/js/scripts.bundle.js" type="text/javascript"></script>
    	
		<!--begin::Page Vendors(used by this page) -->
		<script src="assets/plugins/custom/datatables/datatables.bundle.js" type="text/javascript"></script>

		<!--end::Page Vendors -->

		<!--begin::Page Scripts(used by this page) -->
		<script src="assets/js/pages/components/datatables/data-sources/html.js" type="text/javascript"></script>

    	<script src="assets/js/pages/components/extended/sweetalert2.js" type="text/javascript"></script>

		   <script src="https://cdn.jsdelivr.net/npm/chart.js@2.9.3/dist/Chart.min.js"></script>   
    <script type="text/javascript">



        function MembersGenders(id) {

			//Set Labels
            var lbl1 = document.getElementById("lblMale").value;
            var lbl2 = document.getElementById("lblFemale").value;
            

            var ChangeLabels = [lbl1, lbl2];


			//ActualValues 
            var GetMale = parseInt(document.getElementById("GetMale").value);
            var GetFemale = parseInt(document.getElementById("GetFeMale").value);
            var ChangeData = [GetMale, GetFemale];
 

            var ctx = document.getElementById('chart').getContext('2d');
            var myChart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: ChangeLabels,
                    datasets: [{
                        label: ChangeLabels,
                        data: ChangeData,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(255, 159, 64, 0.2)'
                        ],
                        borderColor: [
                            'rgba(255, 99, 132, 1)',
                            'rgba(255, 159, 64, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    }
                }
			});


			MembersBar();
        }


        function MembersBar() {


            //Set Labels
            var lbl1 = document.getElementById("lblMale").value;
            var lbl2 = document.getElementById("lblFemale").value;


            var ChangeLabels = [lbl1, lbl2];


            //ActualValues 
            var GetMale = parseInt(document.getElementById("GetMale").value);
            var GetFemale = parseInt(document.getElementById("GetFeMale").value);
            var ChangeData = [GetMale, GetFemale];


            var ctx = document.getElementById('BarCharts').getContext('2d');
            var myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: ChangeLabels,
                    datasets: [{
                        label: ChangeLabels,
                        data: ChangeData,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(255, 159, 64, 0.2)'
                        ],
                        borderColor: [
                            'rgba(255, 99, 132, 1)',
                            'rgba(255, 159, 64, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    }
                }
            });
		}




        function SixDiffRep() {

            //Set Labels
            var lbl1 = document.getElementById("lbl1").value;
            var lbl2 = document.getElementById("lbl2").value;
            var lbl3 = document.getElementById("lbl3").value;
			var lbl4 = document.getElementById("lbl4").value;
			var lbl5 = document.getElementById("lbl5").value;
			var lbl6 = document.getElementById("lbl6").value;

            var ChangeLabels = [lbl1, lbl2, lbl3, lbl4, lbl5, lbl6];


            //ActualValues 
            var GetVal1 = parseInt(document.getElementById("Value1").value);
            var GetVal2 = parseInt(document.getElementById("Value2").value);
			var GetVal3 = parseInt(document.getElementById("Value3").value);
			var GetVal4 = parseInt(document.getElementById("Value4").value);
			var GetVal5 = parseInt(document.getElementById("Value5").value);
			var GetVal6 = parseInt(document.getElementById("Value6").value);


            var ChangeData = [GetVal1, GetVal2, GetVal3, GetVal4, GetVal5, GetVal6];


            var ctx = document.getElementById('chart').getContext('2d');
            var myChart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: ChangeLabels,
                    datasets: [{
                        label: ChangeLabels,
                        data: ChangeData,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)',
                            'rgba(255, 159, 64, 0.2)'

                        ],
                        borderColor: [
                            'rgba(255, 99, 132, 1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    }
                }
            });


           SixDiffRepBar();
        }


		function SixDiffRepBar() {


            var lblHEader = document.getElementById("HeaderBar").value;

            
            //Set Labels
            var lbl1 = document.getElementById("lbl1").value;
            var lbl2 = document.getElementById("lbl2").value;
            var lbl3 = document.getElementById("lbl3").value;
            var lbl4 = document.getElementById("lbl4").value;
            var lbl5 = document.getElementById("lbl5").value;
            var lbl6 = document.getElementById("lbl6").value;

            var ChangeLabels = [lbl1, lbl2, lbl3, lbl4, lbl5, lbl6];


            //ActualValues 
            var GetVal1 = parseInt(document.getElementById("Value1").value);
            var GetVal2 = parseInt(document.getElementById("Value2").value);
            var GetVal3 = parseInt(document.getElementById("Value3").value);
            var GetVal4 = parseInt(document.getElementById("Value4").value);
            var GetVal5 = parseInt(document.getElementById("Value5").value);
            var GetVal6 = parseInt(document.getElementById("Value6").value);


            var ChangeData = [GetVal1, GetVal2, GetVal3, GetVal4, GetVal5, GetVal6];


            var ctx = document.getElementById('BarCharts').getContext('2d');
            var myChart = new Chart(ctx, {
                type: 'horizontalBar',
                data: {
                    labels: ChangeLabels,
                    datasets: [{
                        label: lblHEader,
                        data: ChangeData,
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)',
                            'rgba(255, 159, 64, 0.2)'

                        ],
                        borderColor: [
                            'rgba(255, 99, 132, 1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: {
                    scales: {
                        yAxes: [{
                            ticks: {
                                beginAtZero: true
                            }
                        }]
                    }
                }
            });


            // MembersBar();
		}

		
		function RunIncomeFinance(id) {


		    // var GetMale = parseInt(document.getElementById("GetMale").value);
		    //var GetFemale = parseInt(document.getElementById("GetFeMale").value);
		    // alert(document.getElementById("GetFeMale").value);



		    var GetDisplayheader = document.getElementById("HandleLabel").value;
		    var names = document.getElementById("GetValues").value;
		    var array = names.split(',');
		    var ChangeData = array;

		    var GetName = document.getElementById("GetNames").value;
		    var arrayForName = GetName.split(',');
		    var ChangeLabels = arrayForName;

		    var ctx = document.getElementById('chartNew').getContext('2d');
		    var myChart = new Chart(ctx, {
		        type: 'horizontalBar',
		        data: {
		            labels: ChangeLabels,
		            datasets: [{
		                label: GetDisplayheader,
		                data: ChangeData,
		                backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)',
                            'rgba(255, 159, 64, 0.2)'
		                ],
		                borderColor: [
                            'rgba(255, 99, 132, 1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)'
		                ],
		                borderWidth: 1
		            }]
		        },
		        options: {
		            scales: {
		                yAxes: [{
		                    ticks: {
		                        beginAtZero: true
		                    }
		                }]
		            }
		        }
		    });


		    RunExpenseFinance();
		}


		function RunExpenseFinance(id) {


		    // var GetMale = parseInt(document.getElementById("GetMale").value);
		    //var GetFemale = parseInt(document.getElementById("GetFeMale").value);
		    // alert(document.getElementById("GetFeMale").value);



		    var GetDisplayheader = document.getElementById("HandleLabelExp").value;
		    var names = document.getElementById("GetValuesExp").value;
		    var array = names.split(',');
		    var ChangeData = array;

		    var GetName = document.getElementById("GetNamesExp").value;
		    var arrayForName = GetName.split(',');
		    var ChangeLabels = arrayForName;

		    var ctx = document.getElementById('chartExp').getContext('2d');
		    var myChart = new Chart(ctx, {
		        type: 'horizontalBar',
		        data: {
		            labels: ChangeLabels,
		            datasets: [{
		                label: GetDisplayheader,
		                data: ChangeData,
		                backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)',
                            'rgba(255, 159, 64, 0.2)'
		                ],
		                borderColor: [
                            'rgba(255, 99, 132, 1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)'
		                ],
		                borderWidth: 1
		            }]
		        },
		        options: {
		            scales: {
		                yAxes: [{
		                    ticks: {
		                        beginAtZero: true
		                    }
		                }]
		            }
		        }
		    });



		}



        

        function RemoveNotieAlert() {

            var clickbutton = document.getElementById('NotieRemove');
            clickbutton.click();

        }





        function SaveNotieAlert() {

            var clickbutton = document.getElementById('NotieSave');
            clickbutton.click();

        }
        function NotieInvalidPass() {

            var clickbutton = document.getElementById('NotieInvalidPass');
            clickbutton.click();

        }

        function NotieInvalidEmail() {

            var clickbutton = document.getElementById('NotieInvalidEmail');
            clickbutton.click();

        }

        function CompleteNotieAlert() {

            var clickbutton = document.getElementById('NotieComplete');
            clickbutton.click();

        }
        function ExistsNotieAlert() {

            var clickbutton = document.getElementById('ExistUserNotie');
            clickbutton.click();

		}


        function InvalidIDNotie() {

            var clickbutton = document.getElementById('NotieInvalidID');
            clickbutton.click();

        }



</script>

		<!-- begin::Global Config(global config for global JS sciprts) -->
		<script>



		    // predefined ranges
		    var start = moment().subtract(29, 'days');
		    var end = moment();

		    $('#TestDate').daterangepicker({
		        buttonClasses: 'btn btn-sm',
		        applyClass: 'btn-primary',
		        cancelClass: 'btn-secondary',

		        startDate: start,
		        endDate: end,
		        ranges: {
		            'Today': [moment(), moment()],
		            'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
		            'Last 7 Days': [moment().subtract(6, 'days'), moment()],
		            'Last 30 Days': [moment().subtract(29, 'days'), moment()],
		            'This Month': [moment().startOf('month'), moment().endOf('month')],
		            'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
		        }
		    }, function (start, end, label) {


		        $('#TestDate .form-control').val(start.format('MM/DD/YYYY') + ' / ' + end.format('MM/DD/YYYY'));
		        document.getElementById('datepicker').value = start.format('MM/DD/YYYY');
		        document.getElementById('txtEndDate').value = end.format('MM/DD/YYYY');
		        $('#datepicker .form-control').val(start.format('MM/DD/YYYY'));
		        $('#txtEndDate .form-control').val(end.format('MM/DD/YYYY'));

		    });

		    var KTAppOptions = {
		        "colors": {
		            "state": {
		                "brand": "#1cac81",
		                "metal": "#c4c5d6",
		                "light": "#ffffff",
		                "accent": "#00c5dc",
		                "primary": "#5867dd",
		                "success": "#34bfa3",
		                "info": "#36a3f7",
		                "warning": "#ffb822",
		                "danger": "#fd3995",
		                "focus": "#9816f4"
		            },
		            "base": {
		                "label": [
							"#b9bdc1",
							"#aeb2b7",
							"#414b4c",
							"#343d3e"
		                ],
		                "shape": [
							"#eef4f3",
							"#e0e9e6",
							"#80c3af",
							"#41675c"
		                ]
		            }
		        }
		    };



		    $(document).ready(function () {

                $('#MembersT').dataTable();

		    });
		</script>




          <script>
              $('#MembersT').DataTable({
                  dom: 'Bfrtip',
                  buttons: {
                      dom: {
                          button: {
                              tag: 'button',
                              className: ''
                          }
                      },
                      buttons: [{
                          extend: 'pdfHtml5',
                          className: 'btn btn-sm btn-secondary',
                          titleAttr: 'Export to PDF.',
                          text: 'Download PDF',
                          filename: 'pdf-export',
                          extension: '.pdf'
                      },{
                          extend: 'excel',
                              className: 'btn btn-sm btn-primary',
                          titleAttr: 'Excel export.',
                              text: 'Download Excel',
                          filename: 'excel-export',
                          extension: '.xlsx'
                      }, {
                          extend: 'copy',
                              className: 'btn btn-sm btn-secondary',
                          titleAttr: 'Copy table data.',
                          text: 'Copy'
                      }]
                  }
              });

              $('#txtEndDate').datetimepicker({
                  format: "yyyy/mm/dd",
                  todayHighlight: true,
                  autoclose: true,
                  startView: 2,
                  minView: 2,
                  forceParse: 0,
                  pickerPosition: 'bottom-right'
              });
              $('#datepicker').datetimepicker({
                  format: "yyyy/mm/dd",
                  todayHighlight: true,
                  autoclose: true,
                  startView: 2,
                  minView: 2,
                  forceParse: 0,
                  pickerPosition: 'bottom-right'
              });
              $('#txtWeek1').datetimepicker({
                  format: "yyyy/mm/dd",
                  todayHighlight: true,
                  autoclose: true,
                  startView: 2,
                  minView: 2,
                  forceParse: 0,
                  pickerPosition: 'bottom-right'
              });

              $('#txtWeek2').datetimepicker({
                  format: "yyyy/mm/dd",
                  todayHighlight: true,
                  autoclose: true,
                  startView: 2,
                  minView: 2,
                  forceParse: 0,
                  pickerPosition: 'bottom-right'
              });

              $('#txtWeek3').datetimepicker({
                  format: "yyyy/mm/dd",
                  todayHighlight: true,
                  autoclose: true,
                  startView: 2,
                  minView: 2,
                  forceParse: 0,
                  pickerPosition: 'bottom-right'
              });

              $('#txtWeek4').datetimepicker({
                  format: "yyyy/mm/dd",
                  todayHighlight: true,
                  autoclose: true,
                  startView: 2,
                  minView: 2,
                  forceParse: 0,
                  pickerPosition: 'bottom-right'
              });


  </script>



		<!-- end::Global Config -->
	</body>

	<!-- end::Body -->
</html>
