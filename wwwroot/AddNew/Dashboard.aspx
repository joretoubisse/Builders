<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>


<!DOCTYPE html>

<html lang="en">
<head runat="server">
		<base href="">
		<meta charset="utf-8" />
		<title>MyDunamis</title>
		<meta name="description" content="Base form control examples">
		<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
		<meta http-equiv="X-UA-Compatible" content="IE=edge" />

		<!--begin::Fonts -->
		<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700|Raleway:300,400,500,600,700">
      
		<!--end::Fonts -->

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
  <script>
      $(function () {
          $("#datepicker").datepicker();
      });


  </script>
          
</head>
<body class="kt-quick-panel--right kt-demo-panel--right kt-offcanvas-panel--right kt-header--fixed kt-header-mobile--fixed kt-subheader--fixed kt-aside--enabled kt-aside--left kt-aside--fixed kt-aside--offcanvas-default kt-page--loading">


     <form id="form1" runat="server">
           <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieSave" style="display:none;"> Success</button>
         <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieRemove" style="display:none;"> Success</button>
 <div id="kt_header_mobile" class="kt-header-mobile  kt-header-mobile--fixed ">
			<div class="kt-header-mobile__logo">
				<a href="index.html">
					<img alt="Logo" src="assets/media/logos/logo-1.png" />
				</a>
			</div>
			<div class="kt-header-mobile__toolbar">
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
							<a href="dashboard.aspx">
								<img alt="Logo" src="assets/media/logos/logo-1.png" />
							</a>
						</div>

						<!-- end:: Brand -->

						<!-- begin: Header Menu -->
						<button class="kt-header-menu-wrapper-close" id="kt_header_menu_mobile_close_btn"><i class="la la-close"></i></button>
						<div class="kt-header-menu-wrapper kt-grid__item" id="kt_header_menu_wrapper">
							<div id="kt_header_menu" class="kt-header-menu kt-header-menu-mobile ">
								<ul class="kt-menu__nav ">

                                     <li class="kt-menu__item  kt-menu__item--open kt-menu__item--here kt-menu__item--submenu kt-menu__item--rel kt-menu__item--open kt-menu__item--here" data-ktmenu-submenu-toggle="click" aria-haspopup="true"><a href="Dashboard.aspx" class="kt-menu__link kt-menu"><span class="kt-menu__link-text">Dashboard</span><i class="kt-menu__hor-arrow"></i><i class="kt-menu__ver-arrow la la-angle-right"></i></a></li>
								

                                    	<li class="kt-menu__item  kt-menu__item--submenu kt-menu__item--rel" data-ktmenu-submenu-toggle="click" aria-haspopup="true"><a href="Members.aspx" class="kt-menu__link kt-menu"><span class="kt-menu__link-text">Members</span><i class="kt-menu__hor-arrow"></i><i class="kt-menu__ver-arrow la la-angle-right"></i></a>
								
									</li>

                                    
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
<div class="kt-header__topbar kt-grid__item kt-grid__item--fluid">

				

							<!--begin: Notifications -->
					

							<!--begin: User bar -->
							<div class="kt-header__topbar-item kt-header__topbar-item--user" id="kt_offcanvas_toolbar_profile_toggler_btn">
						
								<div class="kt-header__topbar-username">
                                    <label runat="server" id="lblName"></label>
									
								</div>
								<div class="kt-header__topbar-wrapper">
									<img alt="Pic" src="assets/media/users/default.jpg" />
								</div>
							</div>

							<!--end: User bar -->
						</div>
					</div>

					<!-- end:: Header -->

					<!-- begin:: Subheader -->
					<div id="kt_subheader" class="kt-subheader kt-grid__item ">
						<div class="kt-container  kt-container--fluid ">

							<!-- begin:: Subheader Title -->
							<div class="kt-subheader__title">
								<button class="kt-subheader__toggler kt-subheader__toggler--left" id="kt_aside_toggler"><span></span></button>
								<div class="kt-subheader__breadcrumbs">
									<a href="" class="kt-subheader__breadcrumbs-link kt-subheader__breadcrumbs-link--home">Dashboard</a>
								
								</div>
							</div>

							<!-- end:: Subheader Title -->

							<!-- begin:: Sub-header toolbar -->
				        	<!--		<div class="kt-subheader__toolbar">
								<div class="kt-subheader__toolbar-wrapper">
									<a href="#" class="btn btn-default btn-sm btn-bold btn-upper">Create</a>
									<a href="#" class="btn btn-default btn-sm btn-bold btn-upper">Update</a>
									<a href="#" class="btn btn-default btn-sm btn-bold btn-upper">Settings</a>
									<div class="dropdown dropdown-inline" data-toggle="kt-tooltip" title="Quick actions" data-placement="top">
										<a href="#" class="btn btn-icon btn btn-label btn-label-brand btn-bold" data-toggle="dropdown" data-offset="0,5px" aria-haspopup="true" aria-expanded="false">
											<i class="flaticon2-add-1"></i>
										</a>
										<div class="dropdown-menu dropdown-menu-sm dropdown-menu-right dropdown-menu-anim">
											<ul class="kt-nav kt-nav--active-bg" id="m_nav_1" role="tablist">
												<li class="kt-nav__item">
													<a href="" class="kt-nav__link">
														<i class="kt-nav__link-icon flaticon2-psd"></i>
														<span class="kt-nav__link-text">Document</span>
													</a>
												</li>
												<li class="kt-nav__item">
													<a class="kt-nav__link" role="tab" id="m_nav_link_1">
														<i class="kt-nav__link-icon flaticon2-supermarket"></i>
														<span class="kt-nav__link-text">Message</span>
													</a>
												</li>
												<li class="kt-nav__item">
													<a href="" class="kt-nav__link">
														<i class="kt-nav__link-icon flaticon2-shopping-cart"></i>
														<span class="kt-nav__link-text">Product</span>
													</a>
												</li>
												<li class="kt-nav__item">
													<a class="kt-nav__link" role="tab" id="m_nav_link_2">
														<i class="kt-nav__link-icon flaticon2-chart2"></i>
														<span class="kt-nav__link-text">Report</span>
														<span class="kt-nav__link-badge">
															<span class="kt-badge kt-badge--danger kt-badge--inline kt-badge--rounded">pdf</span>
														</span>
													</a>
												</li>
												<li class="kt-nav__item">
													<a href="" class="kt-nav__link">
														<i class="kt-nav__link-icon flaticon2-sms"></i>
														<span class="kt-nav__link-text">Post</span>
													</a>
												</li>
												<li class="kt-nav__item">
													<a href="" class="kt-nav__link">
														<i class="kt-nav__link-icon flaticon2-avatar"></i>
														<span class="kt-nav__link-text">Customer</span>
													</a>
												</li>
											</ul>
										</div>
									</div>
								</div>
							</div>-->

							<!-- end:: Sub-header toolbar -->
						</div>
					</div>

					<!-- end:: Subheader -->
				

                    		<div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" id="Div1">

						<!-- begin:: Content -->
						<div class="kt-container  kt-grid__item kt-grid__item--fluid">

							<!--begin::Dashboard 1-->

							<!--begin::Row-->
							<div class="row">
								<div class="col-lg-6 col-xl-4 order-lg-1 order-xl-1">

									<!--begin::Portlet-->
									<div class="kt-portlet kt-portlet--height-fluid">
										<div class="kt-portlet__head kt-portlet__head--noborder">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">Offering</h3>
											</div>
											<div class="kt-portlet__head-toolbar">
												<div class="kt-portlet__head-toolbar-wrapper">
													<div class="dropdown dropdown-inline">
														<button type="button" class="btn btn-clean btn-sm btn-icon btn-icon-md" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
															<i class="flaticon-more-1"></i>
														</button>
														<div class="dropdown-menu dropdown-menu-right">
															<ul class="kt-nav">
																<li class="kt-nav__section kt-nav__section--first">
																	<span class="kt-nav__section-text">Export Tools</span>
																</li>
															
															</ul>
														</div>
													</div>
												</div>
											</div>
										</div>
										<div class="kt-portlet__body kt-portlet__body--fluid">
											<div class="kt-widget-19">
												<div class="kt-widget-19__title">
													<div class="kt-widget-19__label"><small>R</small> <label runat="server" id="totOfferAmount"></label></div>
													<img class="kt-widget-19__bg" src="assets/media/misc/iconbox_bg.png" alt="bg" />
												</div>
												<div class="kt-widget-19__data">

													<!--Doc: For the chart bars you can use state helper classes: kt-bg-success, kt-bg-info, kt-bg-danger. Refer: components/custom/colors.html -->
													<div class="kt-widget-19__chart">
														<div class="kt-widget-19__bar">
															<div class="kt-widget-19__bar-45" data-toggle="kt-tooltip" data-skin="brand" data-placement="top" title="45"></div>
														</div>
														<div class="kt-widget-19__bar">
															<div class="kt-widget-19__bar-95" data-toggle="kt-tooltip" data-skin="brand" data-placement="top" title="95"></div>
														</div>
														<div class="kt-widget-19__bar">
															<div class="kt-widget-19__bar-63" data-toggle="kt-tooltip" data-skin="brand" data-placement="top" title="63"></div>
														</div>
														<div class="kt-widget-19__bar">
															<div class="kt-widget-19__bar-11" data-toggle="kt-tooltip" data-skin="brand" data-placement="top" title="11"></div>
														</div>
														<div class="kt-widget-19__bar">
															<div class="kt-widget-19__bar-46" data-toggle="kt-tooltip" data-skin="brand" data-placement="top" title="46"></div>
														</div>
														<div class="kt-widget-19__bar">
															<div class="kt-widget-19__bar-88" data-toggle="kt-tooltip" data-skin="brand" data-placement="top" title="88"></div>
														</div>
													</div>
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>
								<div class="col-lg-6 col-xl-4 order-lg-1 order-xl-1">

									<!--begin::Portlet-->
										<div class="kt-portlet kt-portlet--height-fluid">
										<div class="kt-portlet__head kt-portlet__head--noborder">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">Tithe</h3>
											</div>
											<div class="kt-portlet__head-toolbar">
												<div class="kt-portlet__head-toolbar-wrapper">
													<div class="dropdown dropdown-inline">
														<button type="button" class="btn btn-clean btn-sm btn-icon btn-icon-md" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
															<i class="flaticon-more-1"></i>
														</button>
														<div class="dropdown-menu dropdown-menu-right">
															<ul class="kt-nav">
																<li class="kt-nav__section kt-nav__section--first">
																	<span class="kt-nav__section-text">Export Tools</span>
																</li>
															
															</ul>
														</div>
													</div>
												</div>
											</div>
										</div>
										<div class="kt-portlet__body kt-portlet__body--fluid">
											<div class="kt-widget-19">
												<div class="kt-widget-19__title">
													<div class="kt-widget-19__label"><small>R</small><label runat="server" id="totTithe"></label></div>
													<img class="kt-widget-19__bg" src="assets/media/misc/iconbox_bg.png" alt="bg" />
												</div>
												<div class="kt-widget-19__data">

													<!--Doc: For the chart bars you can use state helper classes: kt-bg-success, kt-bg-info, kt-bg-danger. Refer: components/custom/colors.html -->
													<div class="kt-widget-19__chart">
														<div class="kt-widget-19__bar">
															<div class="kt-widget-19__bar-45" data-toggle="kt-tooltip" data-skin="brand" data-placement="top" title="45"></div>
														</div>
														<div class="kt-widget-19__bar">
															<div class="kt-widget-19__bar-95" data-toggle="kt-tooltip" data-skin="brand" data-placement="top" title="95"></div>
														</div>
														<div class="kt-widget-19__bar">
															<div class="kt-widget-19__bar-63" data-toggle="kt-tooltip" data-skin="brand" data-placement="top" title="63"></div>
														</div>
														<div class="kt-widget-19__bar">
															<div class="kt-widget-19__bar-11" data-toggle="kt-tooltip" data-skin="brand" data-placement="top" title="11"></div>
														</div>
														<div class="kt-widget-19__bar">
															<div class="kt-widget-19__bar-46" data-toggle="kt-tooltip" data-skin="brand" data-placement="top" title="46"></div>
														</div>
														<div class="kt-widget-19__bar">
															<div class="kt-widget-19__bar-88" data-toggle="kt-tooltip" data-skin="brand" data-placement="top" title="88"></div>
														</div>
													</div>
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>
								<div class="col-lg-6 col-xl-4 order-lg-1 order-xl-1">

									<!--begin::Portlet-->
									<div class="kt-portlet kt-portlet--height-fluid" runat="server" visible="false">
										<div class="kt-portlet__head  kt-portlet__head--noborder">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">2019 Budget</h3>
											</div>
											<div class="kt-portlet__head-toolbar">
												<div class="kt-portlet__head-toolbar-wrapper">
													<div class="dropdown dropdown-inline">
														<button type="button" class="btn btn-clean btn-sm btn-icon btn-icon-md" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
															<i class="flaticon-more-1"></i>
														</button>
														<div class="dropdown-menu dropdown-menu-right">
															<ul class="kt-nav">
																<li class="kt-nav__section kt-nav__section--first">
																	<span class="kt-nav__section-text">Export Tools</span>
																</li>
																
															</ul>
														</div>
													</div>
												</div>
											</div>
										</div>
										<div class="kt-portlet__body kt-portlet__body--fluid">
											<div class="kt-widget-20">
												<div class="kt-widget-20__title">
													<div class="kt-widget-20__label">R17M</div>
													<img class="kt-widget-20__bg" src="assets/media/misc/iconbox_bg.png" alt="bg" />
												</div>
												<div class="kt-widget-20__data">
													<div class="kt-widget-20__chart">

														<!--Doc: For the chart initialization refer to "widgetTotalOrdersChart" function in "src\theme\app\scripts\custom\dashboard.js" -->
														<canvas id="kt_widget_total_orders_chart"></canvas>
													</div>
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>
								<div class="col-lg-6 col-xl-4 order-lg-1 order-xl-1">

									<!--begin::Portlet-->
									<div class="kt-portlet kt-portlet--height-fluid kt-widget ">
										<div class="kt-portlet__body">
											<div id="kt-widget-slider-13-1" class="kt-slider carousel slide" data-ride="carousel" data-interval="8000">
												<div class="kt-slider__head">
													<div class="kt-slider__label">Events</div>
													<div class="kt-slider__nav">
														<a class="kt-slider__nav-prev carousel-control-prev" href="#kt-widget-slider-13-1" role="button" data-slide="prev">
															<i class="fa fa-angle-left"></i>
														</a>
														<a class="kt-slider__nav-next carousel-control-next" href="#kt-widget-slider-13-1" role="button" data-slide="next">
															<i class="fa fa-angle-right"></i>
														</a>
													</div>
												</div>
												<div class="carousel-inner">
													<div class="carousel-item active kt-slider__body">
														<div class="kt-widget-13">
															<div class="kt-widget-13__body">
																<a class="kt-widget-13__title" href="#">Keen Admin Launch Day</a>
																<div class="kt-widget-13__desc">
																	To start a blog, think of a topic about and first brainstorm party is ways to write details
																</div>
															</div>
															<div class="kt-widget-13__foot">
																<div class="kt-widget-13__label">
																	<div class="btn btn-sm btn-label btn-bold">
																		07 OCT, 2018
																	</div>
																</div>
																<div class="kt-widget-13__toolbar">
																	<a href="#" class="btn btn-default btn-sm btn-bold btn-upper">View</a>
																</div>
															</div>
														</div>
													</div>
													<div class="carousel-item kt-slider__body">
														<div class="kt-widget-13">
															<div class="kt-widget-13__body">
																<a class="kt-widget-13__title" href="#">Incredibly Positive Reviews</a>
																<div class="kt-widget-13__desc">
																	To start a blog, think of a topic about and first brainstorm party is ways to write details
																</div>
															</div>
															<div class="kt-widget-13__foot">
																<div class="kt-widget-13__title">
																	<div class="btn btn-sm btn-label btn-bold">
																		17 NOV, 2018
																	</div>
																</div>
																<div class="kt-widget-13__toolbar">
																	<a href="#" class="btn btn-default btn-sm btn-bold btn-upper">View</a>
																</div>
															</div>
														</div>
													</div>
													<div class="carousel-item kt-slider__body">
														<div class="kt-widget-13">
															<div class="kt-widget-13__body">
																<a class="kt-widget-13__title" href="#">Award Winning Theme</a>
																<div class="kt-widget-13__desc">
																	To start a blog, think of a topic about and first brainstorm party is ways to write details
																</div>
															</div>
															<div class="kt-widget-13__foot">
																<div class="kt-widget-13__label">
																	<div class="btn btn-sm btn-label btn-bold">
																		03 SEP, 2018
																	</div>
																</div>
																<div class="kt-widget-13__toolbar">
																	<a href="#" class="btn btn-default btn-sm btn-bold btn-upper">View</a>
																</div>
															</div>
														</div>
													</div>
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>
								<div class="col-lg-6 col-xl-4 order-lg-1 order-xl-1"  runat="server" visible="false">

									<!--begin::Portlet-->
									<div class="kt-portlet kt-portlet--height-fluid kt-widget-13">
										<div class="kt-portlet__body">
											<div id="kt-widget-slider-13-2" class="kt-slider carousel slide" data-ride="carousel" data-interval="4000">
												<div class="kt-slider__head">
													<div class="kt-slider__label">Projects</div>
													<div class="kt-slider__nav">
														<a class="kt-slider__nav-prev carousel-control-prev" href="#kt-widget-slider-13-2" role="button" data-slide="prev">
															<i class="fa fa-angle-left"></i>
														</a>
														<a class="kt-slider__nav-next carousel-control-next" href="#kt-widget-slider-13-2" role="button" data-slide="next">
															<i class="fa fa-angle-right"></i>
														</a>
													</div>
												</div>
												<div class="carousel-inner">
													<div class="carousel-item active kt-slider__body">
														<div class="kt-widget-13">
															<div class="kt-widget-13__body">
																<a class="kt-widget-13__title" href="#">Keen Admin Launch Day</a>
																<div class="kt-widget-13__desc">
																	To start a blog, think of a topic about and first brainstorm party is ways to write details
																</div>
															</div>
															<div class="kt-widget-13__foot">
																<div class="kt-widget-13__progress">
																	<div class="kt-widget-13__progress-info">
																		<div class="kt-widget-13__progress-status">
																			Progress
																		</div>
																		<div class="kt-widget-13__progress-value">78%</div>
																	</div>
																	<div class="progress">
																		<div class="progress-bar kt-bg-brand" role="progressbar" style="width: 78%" aria-valuenow="78" aria-valuemin="0" aria-valuemax="100"></div>
																	</div>
																</div>
															</div>
														</div>
													</div>
													<div class="carousel-item kt-slider__body">
														<div class="kt-widget-13">
															<div class="kt-widget-13__body">
																<a class="kt-widget-13__title" href="#">First Milestone Achivement</a>
																<div class="kt-widget-13__desc">
																	To start a blog, think of a topic about and first brainstorm party is ways to write details
																</div>
															</div>
															<div class="kt-widget-13__foot">
																<div class="kt-widget-13__progress">
																	<div class="kt-widget-13__progress-info">
																		<div class="kt-widget-13__progress-status">
																			Progress
																		</div>
																		<div class="kt-widget-13__progress-value">55%</div>
																	</div>
																	<div class="progress">
																		<div class="progress-bar kt-bg-brand" role="progressbar" style="width: 55%" aria-valuenow="55" aria-valuemin="0" aria-valuemax="100"></div>
																	</div>
																</div>
															</div>
														</div>
													</div>
													<div class="carousel-item kt-slider__body">
														<div class="kt-widget-13">
															<div class="kt-widget-13__body">
																<a class="kt-widget-13__title" href="#">Reached 50,000 sales</a>
																<div class="kt-widget-13__desc">
																	To start a blog, think of a topic about and first brainstorm party is ways to write details
																</div>
															</div>
															<div class="kt-widget-13__foot">
																<div class="kt-widget-13__progress">
																	<div class="kt-widget-13__progress-info">
																		<div class="kt-widget-13__progress-status">
																			Progress
																		</div>
																		<div class="kt-widget-13__progress-value">24%</div>
																	</div>
																	<div class="progress">
																		<div class="progress-bar kt-bg-brand" role="progressbar" style="width: 24%" aria-valuenow="24" aria-valuemin="0" aria-valuemax="100"></div>
																	</div>
																</div>
															</div>
														</div>
													</div>
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>
								<div class="col-lg-6 col-xl-4 order-lg-1 order-xl-1">

									<!--begin::Portlet-->
									<div class="kt-portlet kt-portlet--height-fluid kt-widget-13"  runat="server" visible="false">
										<div class="kt-portlet__body">
											<div id="kt-widget-slider-13-3" class="kt-slider carousel slide" data-ride="carousel" data-interval="12000">
												<div class="kt-slider__head">
													<div class="kt-slider__label">Today's Schedule</div>
													<div class="kt-slider__nav">
														<a class="kt-slider__nav-prev carousel-control-prev" href="#kt-widget-slider-13-3" role="button" data-slide="prev">
															<i class="fa fa-angle-left"></i>
														</a>
														<a class="kt-slider__nav-next carousel-control-next" href="#kt-widget-slider-13-3" role="button" data-slide="next">
															<i class="fa fa-angle-right"></i>
														</a>
													</div>
												</div>
												<div class="carousel-inner">
													<div class="carousel-item active kt-slider__body">
														<div class="kt-widget-13">
															<div class="kt-widget-13__body">
																<a class="kt-widget-13__title" href="#">Octa Pre-Launch Meeting</a>
																<div class="kt-widget-13__desc kt-widget-13__desc--xl kt-font-brand">
																	9:20AM - 10:00AM
																</div>
															</div>
															<div class="kt-widget-13__foot">
																<div class="kt-widget-13__label">
																	<i class="fa fa-map-marker-alt kt-label-font-color-2"></i>
																	<span class="kt-label-font-color-2">490 E Main St. Norwich...</span>
																</div>
																<div class="kt-widget-13__toolbar">
																	<a href="#" class="btn btn-default btn-sm btn-bold btn-upper">View Map</a>
																</div>
															</div>
														</div>
													</div>
													<div class="carousel-item kt-slider__body">
														<div class="kt-widget-13">
															<div class="kt-widget-13__body">
																<a class="kt-widget-13__title" href="#">UI/UX Design Updates</a>
																<div class="kt-widget-13__desc kt-widget-13__desc--xl kt-font-brand">
																	11:15AM - 12:30PM
																</div>
															</div>
															<div class="kt-widget-13__foot">
																<div class="kt-widget-13__label">
																	<i class="fa fa-map-marker-alt kt-label-font-color-2"></i>
																	<span class="kt-label-font-color-2">246 R St. Manhattan NY...</span>
																</div>
																<div class="kt-widget-13__toolbar">
																	<a href="#" class="btn btn-default btn-sm btn-bold btn-upper">View Map</a>
																</div>
															</div>
														</div>
													</div>
													<div class="carousel-item kt-slider__body">
														<div class="kt-widget-13">
															<div class="kt-widget-13__body">
																<a class="kt-widget-13__title" href="#">Sales Report Summary Meet-up</a>
																<div class="kt-widget-13__desc kt-widget-13__desc--xl kt-font-brand">
																	4:30PM - 5:30PM
																</div>
															</div>
															<div class="kt-widget-13__foot">
																<div class="kt-widget-13__label">
																	<i class="fa fa-map-marker-alt kt-label-font-color-2"></i>
																	<span class="kt-label-font-color-2">492 F Sub St. Norwich CT...</span>
																</div>
																<div class="kt-widget-13__toolbar">
																	<a href="#" class="btn btn-default btn-sm btn-bold btn-upper">View Map</a>
																</div>
															</div>
														</div>
													</div>
												</div>
											</div>
										</div>
									</div>

									<!--end::Portlet-->
								</div>
							</div>

							<!--end::Row-->

						

							<!--end::Dashboard 1-->
						</div>

						<!-- end:: Content -->
					</div>
					<!-- begin:: Footer -->
					<div class="kt-footer kt-grid__item" id="kt_footer" style="bottom:0; width:100%;">
						<div class="kt-container ">
							<div class="kt-footer__copyright">
								2019&nbsp;&copy;&nbsp;<a href="logout.aspx" target="_blank" class="kt-link">My Dunamis</a>
							</div>
						
						</div>
					</div>

					<!-- end:: Footer -->
				</div>
			</div>
		</div>

		<!-- end:: Page -->

		<!-- begin:: Aside -->
		<div class="kt-aside  kt-aside--fixed " id="kt_aside">
			<div class="kt-aside__head">
				<h3 class="kt-aside__title">
				My Dunamis
				</h3>
				<a href="#" class="kt-aside__close" id="kt_aside_close"><i class="flaticon2-delete"></i></a>
			</div>
			<div class="kt-aside__body">

				<!-- begin:: Aside Menu -->
				<div class="kt-aside-menu-wrapper" id="kt_aside_menu_wrapper">
					<div id="kt_aside_menu" class="kt-aside-menu " data-ktmenu-vertical="1" data-ktmenu-scroll="1">
						<ul class="kt-menu__nav ">
						

                            <li class="kt-menu__item  kt-menu__item--active" aria-haspopup="true"><a href="Dashboard.aspx" class="kt-menu__link "><span class="kt-menu__link-text">Dashboard</span></a></li>
                         
                            <li class="kt-menu__item " aria-haspopup="true"><a href="Members.aspx" class="kt-menu__link "><span class="kt-menu__link-text">Members</span></a></li>
							<li class="kt-menu__item" aria-haspopup="true"><a href="Statistics.aspx" class="kt-menu__link "><span class="kt-menu__link-text">Statistics Form</span></a></li>
							<li class="kt-menu__item " aria-haspopup="true"><a href="logout.aspx" class="kt-menu__link "><span class="kt-menu__link-text">Logout</span></a></li>
					
						
						
						
						</ul>
					</div>
				</div>

				<!-- end:: Aside Menu -->
			</div>
		</div>

		<!-- end:: Aside -->

	


	


	


     </form>
    	<!--begin::Global Theme Bundle(used by all pages) -->
		
     
		<!-- begin::Global Config(global config for global JS sciprts) -->
		<script>



		  

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
		</script>

   

	
    <script src="assets/plugins/global/plugins.bundle.js" type="text/javascript"></script>
		<script src="assets/js/scripts.bundle.js" type="text/javascript"></script>
    <script src="assets/js/pages/components/extended/sweetalert2.js" type="text/javascript"></script>
       <script type="text/javascript">



           function RemoveNotieAlert() {

               var clickbutton = document.getElementById('NotieRemove');
               clickbutton.click();

           }
           function SaveNotieAlert() {

               var clickbutton = document.getElementById('NotieSave');
               clickbutton.click();

           }


</script>
</body>
</html>
