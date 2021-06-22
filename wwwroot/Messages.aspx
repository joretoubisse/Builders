<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Messages.aspx.cs" Inherits="Messages" %>


<!DOCTYPE html>

<html lang="en" runat="server">
<head runat="server">
		<base href="">
		<meta charset="utf-8" />
		<title>ChurchApp</title>
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
  <script>
      $(function () {
          $("#datepicker").datepicker();
      });


  </script>
          
</head>

	<!-- begin::Body -->
	<body runat="server"  class="kt-quick-panel--right kt-demo-panel--right kt-offcanvas-panel--right kt-header--fixed kt-header-mobile--fixed kt-subheader--fixed kt-aside--enabled kt-aside--left kt-aside--fixed kt-aside--offcanvas-default kt-page--loading">
     

         <form id="form1" runat="server" class="kt-quick-panel--right kt-demo-panel--right kt-offcanvas-panel--right kt-header--fixed kt-header-mobile--fixed kt-subheader--fixed kt-aside--enabled kt-aside--left kt-aside--fixed kt-aside--offcanvas-default kt-page--loading">
                <input runat="server" id="IsAdmin" type="hidden" />
                       <input runat="server" id="lblIDnumber" type="hidden" />
           <input runat="server" id="CompanyID" type="hidden" />
     <input runat="server" id="MessageID" type="hidden" />
        <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieSave" style="display:none;"> Success</button>
         <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieRemove" style="display:none;"> Success</button>    
              <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieSent" style="display:none;"> Success</button>    
                  <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieComplete" style="display:none;"> Success</button>
              
             <button type="button" id="BtnSendMessage" runat="server" onserverclick="BtnSendMessage_ServerClick"  style="display:none;"></button>


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
							 <ul class="kt-menu__nav "  runat="server" visible="false" >
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
								<a href="Notification.aspx" class="btn btn-default btn-sm btn-bold btn-upper">Back</a>
									<div class="dropdown dropdown-inline" data-toggle="kt-tooltip" title="Quick actions" data-placement="top" runat="server" visible="false" id="DivAddMember">
										<a href="#" class="btn btn-icon btn btn-label btn-label-brand btn-bold" data-toggle="dropdown" data-offset="0,5px" aria-haspopup="true" aria-expanded="false">
											<i class="flaticon2-add-1"></i>
										</a>
										<div class="dropdown-menu dropdown-menu-sm dropdown-menu-right dropdown-menu-anim">
											<ul class="kt-nav kt-nav--active-bg" id="m_nav_1" role="tablist">
											
											
											
												<li class="kt-nav__item">
													<a href="Statistics.aspx" class="kt-nav__link">
														<i class="kt-nav__link-icon flaticon2-avatar"></i>
														<span class="kt-nav__link-text">Add Members</span>
													</a>
												</li>
											</ul>
										</div>
									</div>
								</div>
							</div>

							<!-- end:: Sub-header toolbar -->
						</div>
					</div>

					<!-- end:: Subheader -->
					
                    
                    
                    
           		<div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" id="kt_content">

						<!-- begin:: Content -->
						<div class="kt-container  kt-grid__item kt-grid__item--fluid">

							<!--Begin::Inbox-->
							<div class="kt-grid kt-grid--desktop kt-grid--ver-desktop  kt-inbox" id="kt_inbox">

								<!--Begin::Aside Mobile Toggle-->
							
								<!--End:: Aside Mobile Toggle-->


								<!--Begin:: Inbox List-->
								<div class="kt-grid__item kt-grid__item--fluid    kt-portlet    kt-inbox__list kt-inbox__list--shown" id="kt_inbox_list">
									<div class="kt-portlet__body">


                                       

                                        <div runat="server" id="SendMessageDivs">
                                           <div class="form-group">
													<label>Please Select Section</label>
                                                    
											
                                         <asp:DropDownList ID="CmdChangeSelection" OnSelectedIndexChanged="CmdChangeSelection_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="custom-select form-control">
                                        </asp:DropDownList>
													</div>
                                            
                                         <div class="form-group" id="DivIndividual" runat="server"  visible="false">
													<label>Please Select Individual</label>

                                                        	<select class="form-control kt_selectpicker" data-size="7" data-live-search="true" runat="server" id="CmdIndMember">

													</select>
													</div>


                                                <div class="form-group">
													<label>Please Select Template</label>
                                                    
											
                                         <asp:DropDownList ID="CmdTemplates" OnSelectedIndexChanged="CmdTemplates_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="custom-select form-control">
                                        </asp:DropDownList>
													</div>



                                     




                                                   <div class="form-group" id="SingleDiv" runat="server"  visible="false">
											<label>Please</label>
											 <asp:Literal ID="txtHtml" runat="server"></asp:Literal>
									    	</div>
                                            <div runat="server" id="ShowAll" visible="false">
                                           
                                                
                                          

                                            	<div class="form-group" id="DivSubject"  runat="server"  >
													<label>Subject</label>
													<input type="text" class="form-control" id="txtSubject" runat="server" aria-describedby="Subject" placeholder="Enter Subject" autocomplete="off">
													
												</div>


                                            	<div class="form-group"  id="DivMessage" runat="server" >
													<label>Message</label>
													<textarea rows="4" cols="50" class="form-control" id="txtMessage" autocomplete="off" runat="server">
                                                       
                                                        </textarea>
													
												</div>

                                              	<div class="form-group" id="DivButtonSend" runat="server" >
												
													<button type="reset"  runat="server" id="FireOffLoader" onclick="FireLoader();"  class="btn btn-primary pull-right">Send</button>
											<button type="reset" runat="server" id="btnCancel" onserverclick="btnCancel_ServerClick"  class="btn btn-danger pull-right">Cancel</button>
												</div>

												<div class="form-group" id="DivShowLoader" style="display:none" runat="server" >
														<button class="btn btn-primary pull-right" type="button" >
														<span class="spinner-border spinner-border" role="status" aria-hidden="true"></span>
														Sending Message...
													</button>
										</div>
										
                                                  <br /><br /><br /><br /><br />


                                            </div>



								
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

		<!--end::Global Theme Bundle -->

		<!--begin::Page Vendors(used by this page) -->
		<script src="assets/plugins/custom/datatables/datatables.bundle.js" type="text/javascript"></script>

		<!--end::Page Vendors -->

		<!--begin::Page Scripts(used by this page) -->
		<script src="assets/js/pages/components/datatables/data-sources/html.js" type="text/javascript"></script>

    	<script src="assets/js/pages/components/extended/sweetalert2.js" type="text/javascript"></script>
        	<script src="assets/js/pages/components/forms/widgets/bootstrap-select.js" type="text/javascript"></script>
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
               if (val == "Individual") {
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
               else {
                   document.getElementById("SingleDiv").style.display = "none";
                   document.getElementById("DivMessage").style.display = "block";
                   document.getElementById("DivSubject").style.display = "block";
                   document.getElementById("DivButtonSend").style.display = "block";
               }


           }

           function ShowSendMessageBox() {
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
               else {


                   NewID = val;
                   document.getElementById('lblIDnumber').value = NewID;
                   document.getElementById("SingleDiv").style.display = "block";
                   document.getElementById("DivMessage").style.display = "block";
                   document.getElementById("DivSubject").style.display = "block";
                   document.getElementById("DivButtonSend").style.display = "block";

               }




           }

           function FireLoader() {
               LoaderMessage();
               var clickbutton = document.getElementById('BtnSendMessage');
               clickbutton.click();

           }
           function LoaderMessage() {
               swal.fire({
                   title: 'Please be patient',
                   text: 'While we send your message.',

                   onOpen: function () {
                       swal.showLoading()
                   }
               }).then(function (result) {
                   if (result.dismiss === 'timer') {
                       console.log('I was closed by the timer')
                   }
               })
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
	</body>

	<!-- end::Body -->


</html>
