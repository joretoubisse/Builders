<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Offering.aspx.cs" Inherits="Offering" %>


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

      $(function () {
          $("#txtAmountDate").datepicker();
      });


      

  </script>
          
</head>
<body class="kt-quick-panel--right kt-demo-panel--right kt-offcanvas-panel--right kt-header--fixed kt-header-mobile--fixed kt-subheader--fixed kt-aside--enabled kt-aside--left kt-aside--fixed kt-aside--offcanvas-default kt-page--loading"> <body class="kt-quick-panel--right kt-demo-panel--right kt-offcanvas-panel--right kt-header--fixed kt-header-mobile--fixed kt-subheader--fixed kt-aside--enabled kt-aside--left kt-aside--fixed kt-aside--offcanvas-default kt-page--loading">


     <form id="form1" runat="server">
           <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieSave" style="display:none;"> Success</button>
         <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieRemove" style="display:none;"> Success</button>
           <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieComplete" style="display:none;"> Success</button>
          <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="ExistNotie" style="display:none;"> Success</button>
           <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieInvalidID" style="display:none;"> Success</button>

         
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
									<li class="kt-menu__item  kt-menu__item--submenu kt-menu__item--rel" data-ktmenu-submenu-toggle="click" aria-haspopup="true"><a href="Dashboard.aspx" class="kt-menu__link kt-menu"><span class="kt-menu__link-text">Dashboard</span><i class="kt-menu__hor-arrow"></i><i class="kt-menu__ver-arrow la la-angle-right"></i></a>
								
									</li>

                                    	<li class="kt-menu__item  kt-menu__item--submenu kt-menu__item--rel" data-ktmenu-submenu-toggle="click" aria-haspopup="true"><a href="Members.aspx" class="kt-menu__link kt-menu"><span class="kt-menu__link-text">Members</span><i class="kt-menu__hor-arrow"></i><i class="kt-menu__ver-arrow la la-angle-right"></i></a>
								
									</li>

                                    
                                    <li class="kt-menu__item  kt-menu__item--open kt-menu__item--here kt-menu__item--submenu kt-menu__item--rel kt-menu__item--open kt-menu__item--here" data-ktmenu-submenu-toggle="click" aria-haspopup="true"><a href="Offering.aspx" class="kt-menu__link kt-menu"><span class="kt-menu__link-text">Offering</span><i class="kt-menu__hor-arrow"></i><i class="kt-menu__ver-arrow la la-angle-right"></i></a></li>
                                    
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

				

							<!--begin: Notifications -->
					
                        <div id="Div1" class="kt-subheader kt-grid__item ">
						<div class="kt-container  kt-container--fluid ">

							<!-- begin:: Subheader Title -->
							<div class="kt-subheader__title">
								<button class="kt-subheader__toggler kt-subheader__toggler--left" id="kt_aside_toggler"><span></span></button>
								<div class="kt-subheader__breadcrumbs">
									<a href="" class="kt-subheader__breadcrumbs-link kt-subheader__breadcrumbs-link--home">Offering</a>
								
								</div>
							</div>


                                  <div class="kt-subheader__toolbar">
								<div class="kt-subheader__toolbar-wrapper">
									<a href="Dashboard.aspx" class="btn btn-default btn-sm btn-bold btn-upper">Back</a>
								
							
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
									<a href="" class="kt-subheader__breadcrumbs-link kt-subheader__breadcrumbs-link--home">Offering</a>
								
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
					<div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" id="kt_content">

						<!-- begin:: Content -->
						<div class="kt-container  kt-grid__item kt-grid__item--fluid">
							<div class="row">
								<div class="col-md-3">

									<!--begin::Portlet collect-->
									<div class="kt-portlet" runat="server" id="DivCollectOffering">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title"> Collect Offering</h3>
											</div>
										</div>

										<!--begin::Form-->
									
											<div class="kt-portlet__body">
					
                                                  <br />

                                                 <div class="form-group">
													<label>Amount</label>
													<input type="text" class="form-control" id="txtAmount" autocomplete="off" runat="server" aria-describedby="txtChurchInv" placeholder="">
													</div>

                                                    <div class="form-group">
													<label>Offering Date</label>
                                                  
													<input type="text" class="form-control" id="txtAmountDate" autocomplete="off" runat="server" >
													</div>
                                               

											
											</div>
											<div class="kt-portlet__foot">
												<div class="kt-form__actions">
													<button type="reset" runat="server" id="BtnSave"  onserverclick="BtnSave_ServerClick"  class="btn btn-primary">Submit</button>
													<button type="reset" runat="server" id="btnCancel" onserverclick="btnCancel_ServerClick" class="btn btn-secondary">Cancel</button>

												</div>
											</div>
										

										<!--end::Form-->
									</div>

									<!--end::Portlet  collect-->
								</div>

                                	<div class="col-md-9">

									<!--begin::Portlet collect-->
									<div class="kt-portlet" runat="server" id="Div2">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title"> Offering</h3>
											</div>
										</div>

										<!--begin::Form-->
									
											<div class="kt-portlet__body">
					
                                                  <div class="table-responsive">
											       <asp:Literal ID="tbTable" runat="server"></asp:Literal>
													</div>

											
											</div>
									
										

										<!--end::Form-->
									</div>

									<!--end::Portlet  collect-->
								</div>
							
							</div>
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
						
                            <li class="kt-menu__item " aria-haspopup="true"><a href="Dashboard.aspx" class="kt-menu__link "><span class="kt-menu__link-text">Dashboard</span></a></li>
                            <li class="kt-menu__item " aria-haspopup="true"><a href="Members.aspx" class="kt-menu__link "><span class="kt-menu__link-text">Members</span></a></li>
							<li class="kt-menu__item  kt-menu__item--active" aria-haspopup="true"><a href="Offering.aspx" class="kt-menu__link "><span class="kt-menu__link-text">Offering</span></a></li>
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
    	
		<!--begin::Page Vendors(used by this page) -->
		<script src="assets/plugins/custom/datatables/datatables.bundle.js" type="text/javascript"></script>

		<!--end::Page Vendors -->

		<!--begin::Page Scripts(used by this page) -->
		<script src="assets/js/pages/components/datatables/data-sources/html.js" type="text/javascript"></script>

    	<script src="assets/js/pages/components/extended/sweetalert2.js" type="text/javascript"></script>

       <script type="text/javascript">

           $(document).ready(function () {

               $('#AA').dataTable();

           });

           function RemoveNotieAlert() {

               var clickbutton = document.getElementById('NotieRemove');
               clickbutton.click();

           }
           function SaveNotieAlert() {

               var clickbutton = document.getElementById('NotieSave');
               clickbutton.click();

           }


           function CompleteNotieAlert() {

               var clickbutton = document.getElementById('NotieComplete');
               clickbutton.click();

           }


           function ExistNotie() {

               var clickbutton = document.getElementById('ExistNotie');
               clickbutton.click();

           }

           function InvalidIDNotie() {

               var clickbutton = document.getElementById('NotieInvalidID');
               clickbutton.click();

           }

           
           
</script>



<script>
    $(document).ready(function () {
        $('input#textfield').on('keyup', function () {
            var charCount = $(this).val().replace(/\s/g, '').length;

            if (charCount == 13) {
               
                document.getElementById('IDErrorMessage').style.display = "none";

            }
            else
            {
             
                document.getElementById('IDErrorMessage').style.display = "block";
            }
           
        });
    });
</script>
</body>
</html>
