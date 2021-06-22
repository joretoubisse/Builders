<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyProfile.aspx.cs" Inherits="MyProfile" %>


<!DOCTYPE html>

<html lang="en">
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
	<body id="Body1" runat="server"  class="kt-quick-panel--right kt-demo-panel--right kt-offcanvas-panel--right kt-header--fixed kt-header-mobile--fixed kt-subheader--fixed kt-aside--enabled kt-aside--left kt-aside--fixed kt-aside--offcanvas-default kt-page--loading">
     

         <form id="form1" runat="server" class="kt-quick-panel--right kt-demo-panel--right kt-offcanvas-panel--right kt-header--fixed kt-header-mobile--fixed kt-subheader--fixed kt-aside--enabled kt-aside--left kt-aside--fixed kt-aside--offcanvas-default kt-page--loading">
     <input runat="server" id="IsAdmin" type="hidden" />
           <input runat="server" id="MemberID" type="hidden" />
          <input runat="server" id="GetMemberID" type="hidden" />
           
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
						<a href="dashboard.aspx" class="btn btn-default btn-sm btn-bold btn-upper">Dashboard</a>
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
					
                    
                    
                    
            	<div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" id="DivUsers" runat="server" visible="true" >

						<!-- begin:: Content -->
						<div class="kt-container  kt-grid__item kt-grid__item--fluid">
							<div class="row">
								<div class="col-md-12">

									<!--begin::Portlet-->
									<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">My Profile</h3>
											</div>
										</div>

										<!--begin::Form-->
										<form class="kt-form">
											<div class="kt-portlet__body">
											

                                               

                                        


												<div class="form-group">
													<label>Surname</label>
													<input type="text" class="form-control" id="txtSurname" runat="server" aria-describedby="Surnames" placeholder="Enter Surname">
													
												</div>

                                                	<div class="form-group">
													<label>Full Name</label>
													<input type="text" class="form-control" runat="server"  id="txtName" aria-describedby="Name" placeholder="Enter Name">
													</div>

                                           

                                                          <div class="form-group">
													<label>Email</label>
                                                    
													<input type="email" class="form-control" runat="server" id="txtEmail"  >
													</div>
                                               



                                         <div class="form-group" runat="server" visible="false">
												<label>Access Right * </label>
												   <select class="custom-select form-control" runat="server"  id="CmdAccess">
													<option value="None">Please Select</option>
														<option value="Admin">Admin</option>
                                                        <option value="Users">Users</option>
                                                     
													</select>
													</div>




                                                   <div class="form-group" runat="server" id="DivCampus">
													<label>Campus</label>
													 <select class="custom-select form-control" runat="server" id="CmdCampus">
                                                        <option selected value="none">Please Select</option>
                                                        <option value="Delmas Campus">Delmas Campus</option>
                                                       <option value="Benoni Campus">Benoni Campus</option>
                                                           <option value="Eloff Campus">Eloff Campus</option>
													</select>
													</div>


                                                              <div class="form-group">
													<label>Username</label>
                                                    
													<input type="text" class="form-control" readonly runat="server" id="txtUsername"  >
													</div>


                                                <div runat="server" id="DivPass" visible="false">
                                                  <div class="form-group">
													<label>Password</label>

                                                      <div class="input-group">
                                                       <label id="passwordstrength" style="margin-left:2%;font-size:large;" class="text-warning"></label>
                                                    </div>  



                       
													<input type="text" class="form-control" id="txtPassword" runat="server"  onkeyup="CheckPassword(this.value);"  >
													</div>

                                                    <div class="form-group">
													<label>Confirm Password</label>

                                                                                     <div class="input-group">

                          <label id="lblconfirmNewPassMatch" style="margin-left:2%;font-size:large;" class="text-warning"></label>
                        </div> 
                                                  
													<input type="text" class="form-control" id="txtConfirmPassword" runat="server" onkeyup="CheckPasswordNewMatches(this.value);" >
													</div>

                                                </div>

                                             

											
											</div>
											<div class="kt-portlet__foot">
												<div class="kt-form__actions">
													<button type="reset" runat="server" id="BtnSave" onserverclick="BtnSave_ServerClick"  visible="false"  class="btn btn-primary">Update</button>
													<button type="reset" runat="server" id="btnCancel" onserverclick="btnCancel_ServerClick" class="btn btn-secondary">Cancel</button>
												</div>
											</div>
										</form>

										<!--end::Form-->
									</div>

									<!--end::Portlet-->

									
								</div>


										<div class="col-md-6" runat="server" visible="false">

									<!--begin::Portlet-->
									<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">Roles</h3>
											</div>
										</div>

										<!--begin::Form-->
									
											<div class="kt-portlet__body">


												 	<div class="form-group form-group-last">
													<div class="alert alert-secondary" role="alert">
														<div class="alert-icon"><i class="flaticon-warning kt-font-brand"></i></div>
														<div class="alert-text">
															View Only : Access to view only for that specific module <br />
															Full Access : Access to view and edit for that specific module  <br />
															Hide : This Module is hidden from user   <br />
														</div>

														
													</div>
												</div>
											
															    <div class="form-group">
												<label>Admin * </label>
								 				   <select class="custom-select form-control" runat="server"  id="CmdAdminRole">
									                <option value="None">Please Select</option>
													
                                                      <option value="1">Full Access</option>
                                                         <option value="NA">Hide</option>
													</select>
													</div>


                                                
                                                					    <div class="form-group">
												<label>Dashboard * </label>
								 				   <select class="custom-select form-control" runat="server"  id="CmdDashboardRole">
									                <option value="None">Please Select</option>
										
                                                      <option value="1">Full Access</option>
                                                         <option value="NA">Hide</option>
													</select>
													</div>
                                               

                                         <div class="form-group">
												<label>Members * </label>
												   <select class="custom-select form-control" runat="server"  id="CmdMembersRole">
												<option value="None">Please Select</option>
														<option value="0">View Only</option>
                                                      <option value="1">Full Access</option>
                                                         <option value="NA">Hide</option>
													</select>
													</div>


												    <div class="form-group">
												<label>Visitors * </label>
												   <select class="custom-select form-control" runat="server"  id="CmdVisitors">
											<option value="None">Please Select</option>
														<option value="0">View Only</option>
                                                      <option value="1">Full Access</option>
                                                         <option value="NA">Hide</option>
													</select>
													</div>


														    <div class="form-group">
												<label>Builders Kidz * </label>
												   <select class="custom-select form-control" runat="server"  id="CmdSundaySchool">
												<option value="None">Please Select</option>
														<option value="0">View Only</option>
                                                      <option value="1">Full Access</option>
                                                         <option value="NA">Hide</option>
													</select>
													</div>

															    <div class="form-group">
												<label>Attendance * </label>
												   <select class="custom-select form-control" runat="server"  id="CmdAttendance">
													<option value="None">Please Select</option>
														<option value="0">View Only</option>
                                                      <option value="1">Full Access</option>
                                                         <option value="NA">Hide</option>
													</select>
													</div>


															    <div class="form-group">
												<label>Communication * </label>
												   <select class="custom-select form-control" runat="server"  id="CmdCommunication">
													<option value="None">Please Select</option>
													
                                                      <option value="1">Full Access</option>
                                                         <option value="NA">Hide</option>
													</select>
													</div>


															    <div class="form-group">
												<label>Offering * </label>
												   <select class="custom-select form-control" runat="server"  id="CmdOffering">
													<option value="None">Please Select</option>
														<option value="0">View Only</option>
                                                      <option value="1">Full Access</option>
                                                         <option value="NA">Hide</option>
													</select>
													</div>


												
															    <div class="form-group">
												<label>New Members Applicable * </label>
												   <select class="custom-select form-control" runat="server"  id="CmdNewApplicable">
													<option value="None">Please Select</option>
														<option value="0">View Only</option>
                                                      <option value="1">Full Access</option>
                                                         <option value="NA">Hide</option>
													</select>
													</div>


														
															    <div class="form-group">
												<label>Connect Groups * </label>
												   <select class="custom-select form-control" runat="server"  id="CmbConnect">
													<option value="None">Please Select</option>
														<option value="0">View Only</option>
                                                      <option value="1">Full Access</option>
                                                         <option value="NA">Hide</option>
													</select>
													</div>

																    <div class="form-group">
												<label>Events * </label>
												   <select class="custom-select form-control" runat="server"  id="CmdEvents">
													<option value="None">Please Select</option>
														<option value="0">View Only</option>
                                                      <option value="1">Full Access</option>
                                                         <option value="NA">Hide</option>
													</select>
													</div>


												<div class="form-group">
												<label>Resource * </label>
												   <select class="custom-select form-control" runat="server"  id="CmdResource">
													<option value="None">Please Select</option>
													
                                                      <option value="1">Full Access</option>
                                                         <option value="NA">Hide</option>
													</select>
													</div>


													<div class="form-group">
												<label>iServe * </label>
												   <select class="custom-select form-control" runat="server"  id="CmdMinistry">
													<option value="None">Please Select</option>
														<option value="0">View Only</option>
                                                      <option value="1">Full Access</option>
                                                         <option value="NA">Hide</option>
													</select>
													</div>


												
													<div class="form-group">
												<label>Evangelist * </label>
												   <select class="custom-select form-control" runat="server"  id="CmdEvangelist">
													<option value="None">Please Select</option>
														<option value="0">View Only</option>
                                                      <option value="1">Full Access</option>
                                                         <option value="NA">Hide</option>
													</select>
													</div>


															
													<div class="form-group">
												<label>Reports * </label>
												   <select class="custom-select form-control" runat="server"  id="CmdReports">
													<option value="None">Please Select</option>
														<option value="0">View Only</option>
                                                      <option value="1">Full Access</option>
                                                         <option value="NA">Hide</option>
													</select>
													</div>
                                             

											
											</div>
										
								
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

		<!-- end:: Aside -->


		<!-- begin:: Scrolltop -->
		<div id="kt_scrolltop" class="kt-scrolltop">
			<i class="la la-arrow-up"></i>
		</div>

		<!-- end:: Scrolltop -->

           </form>

	
				
        <script src="assets/plugins/global/plugins.bundle.js" type="text/javascript"></script>
		<script src="assets/js/scripts.bundle.js" type="text/javascript"></script>
    	
		<!--begin::Page Vendors(used by this page) -->
		<script src="assets/plugins/custom/datatables/datatables.bundle.js" type="text/javascript"></script>

		<!--end::Page Vendors -->

		<!--begin::Page Scripts(used by this page) -->
		<script src="assets/js/pages/components/datatables/data-sources/html.js" type="text/javascript"></script>

    	<script src="assets/js/pages/components/extended/sweetalert2.js" type="text/javascript"></script>


    <script type="text/javascript">



        function AddUsers() {
            document.getElementById("IndMember").style.display = "block";

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

		        $('#AA').dataTable();

		    });









		    function CheckPassword(pass) {
		        var pword = pass;


		        var strengthlabl = document.getElementById("passwordstrength");



		        $.get("PasswordComplex.ashx?pword=" + pword, function (data) {

		            if (data == "Blank") {

		                var cla = strengthlabl.className;
		                strengthlabl.classList.remove(cla);
		                strengthlabl.value = "";
		                strengthlabl.classList.add("text-warning");
		                strengthlabl.innerText = data + " password - consider using atleast 1 Number, 1 Upper case letter and 2 or more special characters to improve security";

		            }
		            else if ((data == "Weak") || (data == "Very weak")) {

		                var cla = strengthlabl.className;
		                strengthlabl.classList.remove(cla);
		                strengthlabl.value = "";
		                strengthlabl.classList.add("text-danger");
		                strengthlabl.innerText = data + " password - consider using atleast 1 Number, 1 Upper case letter and 2 or more special characters to improve security";

		            }
		            else if (data == "Medium") {

		                var cla = strengthlabl.className;
		                strengthlabl.classList.remove(cla);
		                strengthlabl.value = "";
		                strengthlabl.classList.add("text-warning");
		                strengthlabl.innerText = data + " password - consider using atleast 1 Number, 1 Upper case letter and 2 or more special characters to improve security";

		            }
		            else {
		                var cla = strengthlabl.className;
		                strengthlabl.classList.remove(cla);
		                strengthlabl.value = "";

		                if (data == "Strong") {
		                    strengthlabl.classList.add("text-green");
		                    strengthlabl.innerText = "Strong password";


		                }
		                else if (data == "Very strong") {

		                    strengthlabl.classList.add("text-green");
		                    strengthlabl.innerText = "Very Strong password";

		                }
		                else {

		                    strengthlabl.classList.add("text-danger");
		                    strengthlabl.innerText = data;

		                }
		            }

		        }).fail(function (a, b, c) {
		            console.log("Ajax Failed");
		        });

		    }



		    function CheckEditPassword(pass) {
		        var pword = pass;


		        var strengthlabl = document.getElementById("passwordEditstrength");



		        $.get("PasswordComplex.ashx?pword=" + pword, function (data) {

		            if (data == "Blank") {
		                var cla = strengthlabl.className;
		                strengthlabl.classList.remove(cla);
		                strengthlabl.value = "";
		                strengthlabl.classList.add("text-warning");
		                strengthlabl.innerText = data + " password - consider using atleast 1 Number, 1 Upper case letter and 2 or more special characters to improve security";

		            }
		            else if ((data == "Weak") || (data == "Very weak")) {
		                var cla = strengthlabl.className;
		                strengthlabl.classList.remove(cla);
		                strengthlabl.value = "";
		                strengthlabl.classList.add("text-danger");
		                strengthlabl.innerText = data + " password - consider using atleast 1 Number, 1 Upper case letter and 2 or more special characters to improve security";

		            }
		            else if (data == "Medium") {
		                var cla = strengthlabl.className;
		                strengthlabl.classList.remove(cla);
		                strengthlabl.value = "";
		                strengthlabl.classList.add("text-warning");
		                strengthlabl.innerText = data + " password - consider using atleast 1 Number, 1 Upper case letter and 2 or more special characters to improve security";

		            }
		            else {
		                var cla = strengthlabl.className;
		                strengthlabl.classList.remove(cla);
		                strengthlabl.value = "";

		                if (data == "Strong") {
		                    strengthlabl.classList.add("text-green");
		                    strengthlabl.innerText = "Strong password";

		                }
		                else if (data == "Very strong") {
		                    strengthlabl.classList.add("text-green");
		                    strengthlabl.innerText = "Very Strong password";

		                }
		                else {
		                    strengthlabl.classList.add("text-danger");
		                    strengthlabl.innerText = data;

		                }
		            }

		        }).fail(function (a, b, c) {
		            console.log("Ajax Failed");
		        });

		    }

		    function CheckPasswordNewMatches(pass) {
		        var NewPass = document.getElementById("txtPassword").value;

		        var lbl = document.getElementById("lblconfirmNewPassMatch");
		        if (NewPass == pass) {

		            lbl.style.visibility = "hidden";

		        }
		        else {
		            lbl.style.visibility = "visible";
		            lbl.innerText = "Passwords do not Match";

		        }
		    }

		    function CheckEditPasswordNewMatches(pass) {
		        var NewPass = document.getElementById("txtEditPass").value;

		        var lbl = document.getElementById("lblEditconfirmNewPassMatch");
		        if (NewPass == pass) {

		            lbl.style.visibility = "hidden";

		        }
		        else {
		            lbl.style.visibility = "visible";
		            lbl.innerText = "Passwords do not Match";
		        }
		    }


































		</script>




<script>
    $(document).ready(function () {
        $('input#textfield').on('keyup', function () {
            var charCount = $(this).val().replace(/\s/g, '').length;

            if (charCount == 13) {

                document.getElementById('IDErrorMessage').style.display = "none";

            }
            else {

                document.getElementById('IDErrorMessage').style.display = "block";
            }

        });
    });
</script>
		<!-- end::Global Config -->

	</body>

	<!-- end::Body -->
</html>
