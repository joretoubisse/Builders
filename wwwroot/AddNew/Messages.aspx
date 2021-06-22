<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Messages.aspx.cs" Inherits="Messages" %>


<!DOCTYPE html>

<!--
Theme: Keen - The Ultimate Bootstrap Admin Theme
Author: KeenThemes
Website: http://www.keenthemes.com/
Contact: support@keenthemes.com
Follow: www.twitter.com/keenthemes
Dribbble: www.dribbble.com/keenthemes
Like: www.facebook.com/keenthemes
License: You must have a valid license purchased only from https://themes.getbootstrap.com/product/keen-the-ultimate-bootstrap-admin-theme/ in order to legally use the theme for your project.
-->
<html lang="en">

	<!-- begin::Head -->
	<head>
		<base href="">
		<meta charset="utf-8" />
		<title>MyDunamis</title>
		<meta name="description" content="User login example">
		<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
		<meta http-equiv="X-UA-Compatible" content="IE=edge" />

	
		<!--begin::Fonts -->
		<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700|Raleway:300,400,500,600,700">
        	<link href="assets/plugins/custom/datatables/datatables.bundle.css" rel="stylesheet" type="text/css" />
		<!--end::Fonts -->

		<!--begin::Page Custom Styles(used by this page) -->
		<link href="assets/css/pages/inbox/inbox.css" rel="stylesheet" type="text/css" />

		<!--end::Page Custom Styles -->

		<!--begin::Global Theme Styles(used by all pages) -->
		<link href="assets/plugins/global/plugins.bundle.css" rel="stylesheet" type="text/css" />
		<link href="assets/css/style.bundle.css" rel="stylesheet" type="text/css" />

		<!--end::Global Theme Styles -->

		<!--begin::Layout Skins(used by all pages) -->
		<link href="assets/css/skins/header/navy.css" rel="stylesheet" type="text/css" />

		<!--end::Layout Skins -->
		<link rel="shortcut icon" href="assets/media/logos/favicon.ico" />
	</head>

	<!-- end::Head -->

	<!-- begin::Body -->
<body class="kt-inbox__aside--left kt-quick-panel--right kt-demo-panel--right kt-offcanvas-panel--right kt-header--fixed kt-header-mobile--fixed kt-subheader--fixed kt-aside--enabled kt-aside--left kt-aside--fixed kt-aside--offcanvas-default kt-page--loading">
  
  
        
         <form id="form1" runat="server">
		<!-- begin:: Page -->
                <input runat="server" id="lblIDnumber" type="hidden" />
           <input runat="server" id="CompanyID" type="hidden" />
     <input runat="server" id="MessageID" type="hidden" />
        <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieSave" style="display:none;"> Success</button>
         <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieRemove" style="display:none;"> Success</button>    
              <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieSent" style="display:none;"> Success</button>    
                  <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieComplete" style="display:none;"> Success</button>
                <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="btnReadMessage" runat="server" onserverclick="btnReadMessage_ServerClick" style="display:none;"> Success</button>
	<!-- begin:: Page -->

		<!-- begin:: Header Mobile -->
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
							<a href="index.html">
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


                                     	<li class="kt-menu__item  kt-menu__item--submenu kt-menu__item--rel" data-ktmenu-submenu-toggle="click" aria-haspopup="true"><a href="Statistics.aspx" class="kt-menu__link kt-menu"><span class="kt-menu__link-text">Statistics Form</span><i class="kt-menu__hor-arrow"></i><i class="kt-menu__ver-arrow la la-angle-right"></i></a>
								
									</li>
                                
                                     <li class="kt-menu__item  kt-menu__item--open kt-menu__item--here kt-menu__item--submenu kt-menu__item--rel kt-menu__item--open kt-menu__item--here" data-ktmenu-submenu-toggle="click" aria-haspopup="true"><a href="Messages.aspx" class="kt-menu__link kt-menu"><span class="kt-menu__link-text">Communication</span><i class="kt-menu__hor-arrow"></i><i class="kt-menu__ver-arrow la la-angle-right"></i></a></li>
                                    
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
									<a href="" class="kt-subheader__breadcrumbs-link kt-subheader__breadcrumbs-link--home">Dashboard</a>
								
								</div>
							</div>

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
					<div id="kt_subh.eader" class="kt-subheader kt-grid__item ">
						<div class="kt-container  kt-container--fluid ">

							<!-- begin:: Subheader Title -->
							<div class="kt-subheader__title">
								<button class="kt-subheader__toggler kt-subheader__toggler--left" id="kt_aside_toggler"><span></span></button>
								<div class="kt-subheader__breadcrumbs">
									<a href="" class="kt-subheader__breadcrumbs-link kt-subheader__breadcrumbs-link--home">Statistics Form</a>
								
								</div>
							</div>

	
						</div>
					</div>

					<!-- end:: Subheader -->
					<div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" id="kt_content">

						<!-- begin:: Content -->
						<div class="kt-container  kt-grid__item kt-grid__item--fluid">

							<!--Begin::Inbox-->
							<div class="kt-grid kt-grid--desktop kt-grid--ver-desktop  kt-inbox" id="kt_inbox">

								<!--Begin::Aside Mobile Toggle-->
								<button class="kt-inbox__aside-close" id="kt_inbox_aside_close">
									<i class="la la-close"></i>
								</button>

								<!--End:: Aside Mobile Toggle-->

								<!--Begin:: Inbox Aside-->
								<div class="kt-grid__item   kt-portlet  kt-inbox__aside" id="kt_inbox_aside">
									<button type="button" class="btn btn-brand  btn-upper btn-bold  kt-inbox__compose"  onclick="ShowSendMessageBox();">
										<i class="flaticon2-send-1"></i> new message
									</button>
									<div class="kt-inbox__nav">
										<ul class="kt-nav">
												<%--<li class="kt-nav__item kt-nav__item--active" id="InboxDiv">
												<a href="#" class="kt-nav__link" data-action="list" data-type="inbox">
													<i class="kt-nav__link-icon flaticon2-mail"></i>
													<span class="kt-nav__link-text">Inbox</span>
													<span class="kt-nav__link-badge">
													<span class="kt-badge kt-badge--unified-success kt-badge--md kt-badge--rounded kt-badge--boldest"></span>
													</span>
												</a>
											</li>--%>
								
											<li class="kt-nav__item kt-nav__item--active"  id="SentDiv">
												<a href="#" onclick="CancelSendMessageBox();" class="kt-nav__link" data-action="list" data-type="sent">
													<i class="kt-nav__link-icon flaticon2-mail-1"></i>
													<span class="kt-nav__link-text">Sent</span>
												</a>
											</li>
								
										</ul>
									</div>
								</div>

								<!--End::Aside-->

								<!--Begin:: Inbox List-->
								<div class="kt-grid__item kt-grid__item--fluid    kt-portlet    kt-inbox__list kt-inbox__list--shown" id="kt_inbox_list">
									<div class="kt-portlet__body">


                                              <div class="table-responsive" runat="server"  id="ShowMessageDiv" >
											       <asp:Literal ID="tbTable" runat="server"></asp:Literal>
													</div>

                                        <div runat="server" id="SendMessageDiv" style="display:none" >
                                           <div class="form-group">
													<label>Please Section</label>
                                                    
											  <select class="custom-select form-control" runat="server" onchange="ChangeSelection(this)" id="CmdChangeSelection">
														<option selected  value="none">Please Select</option>
														<option value="Every One">Every One</option>
														<option value="Ward Cell">Ward Cell</option>
                                                      	<option value="Individual">Individual</option>
                                                     	<option value="All Females">Females</option>
                                                        <option value="All Males">Males</option>
                                                        <option value="Ministries">Ministries</option>
												
													</select>

													</div>



                                            <div class="form-group" id="SingleDiv" style="display:none">
											<label>Name</label>
											 <asp:Literal ID="txtHtml" runat="server"></asp:Literal>
									    	</div>

                                            	<div class="form-group" id="DivSubject"  runat="server"  style="display:none" >
													<label>Subject</label>
													<input type="text" class="form-control" id="txtSubject" runat="server" aria-describedby="Subject" placeholder="Enter Subject" autocomplete="off">
													
												</div>


                                            	<div class="form-group"  id="DivMessage" runat="server"  style="display:none">
													<label>Message</label>
													<textarea rows="4" cols="50" class="form-control" id="txtMessage" autocomplete="off" runat="server">
                                                       
                                                        </textarea>
													
												</div>

                                            	<div class="form-group" id="DivButtonSend" runat="server" style="display:none">
													<button type="reset" runat="server" id="BtnSendMessage" onserverclick="BtnSendMessage_ServerClick"  class="btn btn-primary pull-right">Send</button>
											<button type="reset" runat="server" id="btnCancel" onserverclick="btnCancel_ServerClick"  class="btn btn-danger pull-right">Cancel</button>
												</div>
										
                                                  <br /><br /><br /><br /><br />


                                            </div>



                                              <div runat="server" id="DivViewMsg" style="display:none" >
                                    
                                                  	<div class="form-group" id="Div2"  >
													<label>Subject</label>
													<input type="text" class="form-control" id="ActualSub" runat="server" aria-describedby="Subject" readonly placeholder="Enter Subject" autocomplete="off">
													
												</div>

                                            	<div class="form-group"  >
													<label>Message</label>
													<textarea rows="4" cols="50" class="form-control" id="txtShowMessage" readonly autocomplete="off" runat="server">
                                                       
                                                        </textarea>
													
												</div>

                                            	<div class="form-group">
												
											<button type="reset" runat="server" id="Button3" onserverclick="btnCancel_ServerClick"  class="btn btn-danger pull-right">Back</button>
												</div>
										
                                                  <br /><br /><br /><br /><br />


                                            </div>



								
									</div>
								</div>

				
							</div>

							<!--End::Inbox-->

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
							<li class="kt-menu__item  kt-menu__item--active" aria-haspopup="true"><a href="Statistics.aspx" class="kt-menu__link "><span class="kt-menu__link-text">Statistics Form</span></a></li>
							<li class="kt-menu__item " aria-haspopup="true"><a href="logout.aspx" class="kt-menu__link "><span class="kt-menu__link-text">Logout</span></a></li>
					
						
						
						
						</ul>
					</div>
				</div>

				<!-- end:: Aside Menu -->
			</div>
		</div>

		<!-- end:: Aside -->
		<!-- begin:: Topbar Offcanvas Panels -->





		<!-- end:: Page -->


             
		<!--begin::Global Theme Bundle(used by all pages) -->
		<script src="assets/plugins/global/plugins.bundle.js" type="text/javascript"></script>
		<script src="assets/js/scripts.bundle.js" type="text/javascript"></script>

		<!--end::Global Theme Bundle -->

		<!--begin::Page Vendors(used by this page) -->
		<script src="assets/plugins/custom/datatables/datatables.bundle.js" type="text/javascript"></script>

		<!--end::Page Vendors -->

		<!--begin::Page Scripts(used by this page) -->
		<script src="assets/js/pages/components/datatables/data-sources/html.js" type="text/javascript"></script>

    	<script src="assets/js/pages/components/extended/sweetalert2.js" type="text/javascript"></script>

             	<script src="assets/js/pages/components/forms/widgets/bootstrap-select.js" type="text/javascript"></script>
		<!-- begin::Global Config(global config for global JS sciprts) -->
		<script>
		    $(document).ready(function () {

		        $('#AA').dataTable();

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
		</script>

		<!-- end::Global Config -->




       <script type="text/javascript">


           //SingleDiv
           function ChangeSelection(id) {
               var val = id.value;
               if (val == "Individual")
               {
                   document.getElementById("SingleDiv").style.display = "block";
                   document.getElementById("DivMessage").style.display = "none";
                   document.getElementById("DivSubject").style.display = "none";
                   document.getElementById("DivButtonSend").style.display = "none";
                   
                   
               }
               else if (val == "none") {
                   document.getElementById("SingleDiv").style.display = "none";
                   document.getElementById("DivMessage").style.display = "none";
                   document.getElementById("DivSubject").style.display = "none";
                   document.getElementById("DivButtonSend").style.display = "none";
               }
               else
               {
                   document.getElementById("SingleDiv").style.display = "none";
                   document.getElementById("DivMessage").style.display = "block";
                   document.getElementById("DivSubject").style.display = "block";
                   document.getElementById("DivButtonSend").style.display = "block";
               }


           }

           function ShowSendMessageBox()
           {
               document.getElementById("ShowMessageDiv").style.display = "none";
               document.getElementById("SendMessageDiv").style.display = "block";
           }


           function CancelSendMessageBox() {
               document.getElementById("ShowMessageDiv").style.display = "block";
               document.getElementById("SendMessageDiv").style.display = "none";
           }

           


           function IndividualSelection(id) {
               var val = id.value;
               var NewID = document.getElementById('lblIDnumber').value;
               if (val == "0") {

                   document.getElementById("SingleDiv").style.display = "block";
                   document.getElementById("DivMessage").style.display = "none";
                   document.getElementById("DivSubject").style.display = "none";
                   document.getElementById("DivButtonSend").style.display = "none";
               }
               else
               {
              
                  
                   NewID = val;
                   document.getElementById('lblIDnumber').value = NewID;
                   document.getElementById("SingleDiv").style.display = "block";
                   document.getElementById("DivMessage").style.display = "block";
                   document.getElementById("DivSubject").style.display = "block";
                   document.getElementById("DivButtonSend").style.display = "block";
                 
               }
             
         


           }




           
           function CompleteNotieAlert() {

               var clickbutton = document.getElementById('NotieComplete');
               clickbutton.click();

           }


           function BtnReadMessage(id) {

               document.getElementById('MessageID').value = id;
               var clickbutton = document.getElementById('btnReadMessage');
               clickbutton.click();

           }


           

           function SendNotieAlert() {

               var clickbutton = document.getElementById('NotieSent');
               clickbutton.click();

           }
           function SaveNotieAlert() {

               var clickbutton = document.getElementById('NotieSave');
               clickbutton.click();

           }


</script>
		<!--end::Page Scripts -->
                 </form>
	</body>

	<!-- end::Body -->
</html>