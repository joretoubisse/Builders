<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Members.aspx.cs" Inherits="Members" %>


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
<body class="kt-quick-panel--right kt-demo-panel--right kt-offcanvas-panel--right kt-header--fixed kt-header-mobile--fixed kt-subheader--fixed kt-aside--enabled kt-aside--left kt-aside--fixed kt-aside--offcanvas-default kt-page--loading">


     <form id="form1" runat="server">
           <input runat="server" id="MemberID" type="hidden" />
         <button type="button" id="btnViewMember" runat="server" onserverclick="btnViewMember_ServerClick"  style="display:none;"></button>
             <button type="button" id="btnArchive" runat="server" onserverclick="btnArchive_ServerClick"  style="display:none;"></button>
         <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieSave" style="display:none;"> Success</button>
         <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieRemove" style="display:none;"> Success</button>
           <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="RemoveMember" style="display:none;"> Success</button>
          <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieComplete" style="display:none;"> Success</button>
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




                                       <li class="kt-menu__item  kt-menu__item--open kt-menu__item--here kt-menu__item--submenu kt-menu__item--rel kt-menu__item--open kt-menu__item--here" data-ktmenu-submenu-toggle="click" aria-haspopup="true"><a href="Members.aspx" class="kt-menu__link kt-menu"><span class="kt-menu__link-text">Members</span><i class="kt-menu__hor-arrow"></i><i class="kt-menu__ver-arrow la la-angle-right"></i></a></li>
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

				

							<!--begin: Notifications -->
					
                        <div id="Div1" class="kt-subheader kt-grid__item ">
						<div class="kt-container  kt-container--fluid ">

							<!-- begin:: Subheader Title -->
							<div class="kt-subheader__title">
								<button class="kt-subheader__toggler kt-subheader__toggler--left" id="kt_aside_toggler"><span></span></button>
								<div class="kt-subheader__breadcrumbs">
									<a href="Members.aspx" class="kt-subheader__breadcrumbs-link kt-subheader__breadcrumbs-link--home">Members</a>
								
								</div>
							</div>

                            	<div class="kt-subheader__toolbar">
								<div class="kt-subheader__toolbar-wrapper">
									<a href="Dashboard.aspx" class="btn btn-default btn-sm btn-bold btn-upper">Dashboard</a>
						
									<div class="dropdown dropdown-inline" data-toggle="kt-tooltip" title="Quick actions" data-placement="top">
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
									<a href="" class="kt-subheader__breadcrumbs-link kt-subheader__breadcrumbs-link--home">Members</a>
								
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
					<div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" id="IndMember" runat="server" visible="false">

						<!-- begin:: Content -->
						<div class="kt-container  kt-grid__item kt-grid__item--fluid">
							<div class="row">
								<div class="col-md-12">

									<!--begin::Portlet-->
									<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">Statistics Form</h3>
											</div>
										</div>

										<!--begin::Form-->
										<form class="kt-form">
											<div class="kt-portlet__body">
											

                                                        <div class="form-group">
													<label>Member No</label>
                                                    
													<input type="text" class="form-control" runat="server" id="txtMemberNo" readonly >
													</div>

                                                  <div class="form-group">
													<label>ID Number</label>
                                                    
												 <input id="textfield" type="text" name="Custom Text" placeholder="Enter ID Number" class="form-control" runat="server"  autocomplete="off"/> 
                                                      <span id="IDErrorMessage" style="display:none" class="form-text text-muted">Invalid ID</span>

													</div>


												<div class="form-group">
													<label>Surname</label>
													<input type="text" class="form-control" id="txtSurname" runat="server" aria-describedby="Surnames" placeholder="Enter Surname">
													
												</div>

                                                	<div class="form-group">
													<label>Full Name</label>
													<input type="text" class="form-control" runat="server" readonly id="txtName" aria-describedby="Name" placeholder="Enter Name">
													</div>

                                                   <div class="form-group">
													<label>Known As</label>
													<input type="text" class="form-control" id="txtKnownAS" runat="server" aria-describedby="KnowAS" placeholder="Enter Known As">
													</div>



                                                	<div class="form-group">
													<label>Gender</label>
												   <select class="custom-select form-control" runat="server"  id="CmdGender">
														<option selected>Please Select</option>
														<option value="Female">Female</option>
														<option value="Male">Male</option>
												
													</select>
													</div>


                                                  <div class="form-group">
													<label>D.O.B</label>
                                                  
													<input type="text" class="form-control" id="datepicker" runat="server" >
													</div>


                                                 <div class="form-group">
													<label>Ward</label>
													 <select class="custom-select form-control" runat="server" id="CmdWard">
                                                        <option selected>Please Select</option>
                                                        <option value="One">One</option>
                                                        <option value="Two">Two</option>
                                                        <option value="Three">Three</option>
                                                        <option value="Four">Four</option>
                                                        <option value="Five">Five</option>
                                                        <option value="Six">Six</option>
                                                        <option value="Seven">Seven</option>
												
													</select>
													</div>

                                                 <div class="form-group">
													<label>Marital Status</label>
													 <select class="custom-select form-control" runat="server" id="CmdMarital">
														<option selected>Please Select</option>
														<option value="Single">Single</option>
														<option value="Married">Married</option>
												
													</select>
													</div>
                                             

                                                
                                                  <div class="form-group">
													<label>Occupation or Schooling</label>
													<input type="text" class="form-control" id="txtOccupation" runat="server" aria-describedby="IDNo" placeholder="Enter Occupation or Schooling">
													</div>

                                                   <div class="form-group">
													<label>Employer or School</label>
													<input type="text" class="form-control" id="txtEmployer" runat="server" aria-describedby="Employer" placeholder="Enter Employer or School">
													</div>

                                                 <div class="form-group">
													<label>Skills</label>
													<input type="text" class="form-control" id="txtSkills" runat="server" aria-describedby="Skills" placeholder="Enter Skills">
													</div>


                                                 <div class="form-group">
													<label>Residential Address</label>
													<input type="text" class="form-control" id="txtAddress" runat="server" aria-describedby="address" placeholder="Enter Address">
													</div>

                                                 <div class="form-group">
													<label>Tel. No: (H)</label>
													<input type="text" class="form-control" id="txtTellH" runat="server" aria-describedby="Tell" placeholder="Enter Tel. No: (H)">
													</div>

                                                  <div class="form-group">
													<label>Tel. No: (W)</label>
													<input type="text" class="form-control" id="txtTellW" runat="server" aria-describedby="txtTellW" placeholder="Enter Tel. No: (W)">
													</div>

                                                  <div class="form-group">
													<label>Fax. No</label>
													<input type="text" class="form-control" id="txtFax" runat="server" aria-describedby="txtFax" placeholder="Enter Fax. No">
													</div>

                                                 <div class="form-group">
													<label>Cell phone</label>
													<input type="text" class="form-control" id="txtCellNo" runat="server" aria-describedby="txtCellNo" placeholder="Enter Cell No">
													</div>

                                                <div class="form-group">
													<label>Email</label>
													<input type="email" class="form-control" id="txtEmail" runat="server" aria-describedby="txtEmail" placeholder="Enter Email">
													</div>


                                                     <div class="form-group">
													<label>Ministries</label>
													 <select class="custom-select form-control" runat="server" id="CmdMinistries">
														<option selected>Please Select</option>
														<option value="Sunday School">Sunday School</option>
														<option value="Youth">Youth</option>
                                                         <option value="Christian Women Ministries">Christian Women Ministries</option>
                                                          <option value="Christian Men Ministries">Christian Men Ministries</option>
												
													</select>
													</div>

                                                  <div class="form-group">
													<label>Church Involvement</label>
													<input type="text" class="form-control" id="txtChurchInv" autocomplete="off" runat="server" aria-describedby="txtChurchInv" placeholder="Enter Church Involvement">
													</div>

                                                   <div class="form-group">
													<label>Status</label>
													 <select class="custom-select form-control" runat="server" id="CmdStatus">
														<option selected>Please Select</option>
														<option value="Baptized Member">Baptized Member</option>
														<option value="Catechumen">Catechumen</option>
                                                         <option value="Prospective Member">Prospective Member</option>
                                                          <option value="Full Member">Full Member</option>
												
													</select>
													</div>

                                                  <div class="form-group">
													<label>Date Membership obtained</label>
                                                  
													<input type="text" class="form-control" id="txtMemberObtained" autocomplete="off" runat="server" >
													</div>


                                                 <div class="form-group">
													<label>Spiritual Gifts</label>
													<input type="text" class="form-control" id="txtSpiritualGifts" autocomplete="off" runat="server" aria-describedby="txtChurchInv" placeholder="Enter Spiritual Gifts">
													</div>


                                                   <div class="form-group">
													<label>FFS Member</label>
													 <select class="custom-select form-control" runat="server" id="CmdFFS">
														<option selected>Please Select</option>
														<option value="Yes">Yes</option>
														<option value="No">No</option>
                                                        
												
													</select>
													</div>

                                             

											
											</div>
											<div class="kt-portlet__foot">
												<div class="kt-form__actions">
													<button type="reset" runat="server" id="BtnSave" onserverclick="BtnSave_ServerClick"  class="btn btn-primary">Submit</button>
													<button type="reset" runat="server" id="btnCancel" onserverclick="btnCancel_ServerClick" class="btn btn-secondary">Cancel</button>
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



                    	<div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" id="DivViewMember" runat="server" visible="true">

						<!-- begin:: Content -->
						<div class="kt-container  kt-grid__item kt-grid__item--fluid">
							<div class="row">
								<div class="col-md-12">

									<!--begin::Portlet-->
									<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">Statistics Form</h3>
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

                            <li class="kt-menu__item  kt-menu__item--active" aria-haspopup="true"><a href="Members.aspx" class="kt-menu__link "><span class="kt-menu__link-text">Members</span></a></li>
                            <li class="kt-menu__item " aria-haspopup="true"><a href="Offering.aspx" class="kt-menu__link "><span class="kt-menu__link-text">Offering</span></a></li>
			
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
		
        <script src="assets/plugins/global/plugins.bundle.js" type="text/javascript"></script>
		<script src="assets/js/scripts.bundle.js" type="text/javascript"></script>
    	
		<!--begin::Page Vendors(used by this page) -->
		<script src="assets/plugins/custom/datatables/datatables.bundle.js" type="text/javascript"></script>

		<!--end::Page Vendors -->

		<!--begin::Page Scripts(used by this page) -->
		<script src="assets/js/pages/components/datatables/data-sources/html.js" type="text/javascript"></script>

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
        function CompleteNotieAlert() {

            var clickbutton = document.getElementById('NotieComplete');
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
		   
		    
		    function BtnViewMem(id) {
		        document.getElementById('MemberID').value = id;
		        var clickbutton = document.getElementById('btnViewMember');
		        clickbutton.click();

		    }
		    

		    function BtnArchMem(id) {

		        
		        document.getElementById('MemberID').value = id;
		        var clickbutton = document.getElementById('RemoveMember');
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
            else {

                document.getElementById('IDErrorMessage').style.display = "block";
            }

        });
    });
</script>
		<!-- end::Global Config -->

	

</body>
</html>
