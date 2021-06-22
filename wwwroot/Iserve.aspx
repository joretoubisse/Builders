<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Iserve.aspx.cs" Inherits="Iserve" %>


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
                <input runat="server" id="GetIserve" type="hidden" />
                 <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieSave" style="display:none;"> Success</button>
         <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieRemove" style="display:none;"> Success</button>
           <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="RemoveMember" style="display:none;"> Success</button>
          <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieComplete" style="display:none;"> Success</button>
          <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieInvalidID" style="display:none;"> Success</button>
          <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieAccessRight" style="display:none;"> Success</button>
          
              <input runat="server" id="MemberID" type="hidden" />
           <button type="button" id="btnCheckingIN" runat="server" onserverclick="btnCheckingIN_ServerClick"  style="display:none;"></button>
                        <button type="button" id="btnViewhistor" runat="server" onserverclick="btnViewhistor_ServerClick"  style="display:none;"></button>

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
						<div class="kt-header-menu-wrapper kt-grid__item" id="kt_header_menu_wrapper" runat="server" >
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
								<a href="Dashboard.aspx" runat="server" id="MainDash"  class="btn btn-default btn-sm btn-bold btn-upper">Dashboard</a>
                                  <button type="reset" runat="server" id="btnReturn" visible="false" onserverclick="btnReturn_ServerClick" class="btn btn-default btn-sm btn-bold btn-upper">Back</button>
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
					
                    
                           
                    <div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" runat="server" id="kt_content">  	<!-- Add Work here -->

						<!-- begin:: Content -->
						<div class="kt-container  kt-grid__item kt-grid__item--fluid">
							<div class="row">
								<div class="col-md-12">

									<!--begin::Portlet-->
									<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">iServe</h3>
											</div>
										</div>

										<!--begin::Form-->
										<form class="kt-form">
											<div class="kt-portlet__body">
											

                                    

                                               

                                                 <div class="form-group" runat="server" id="DivIserve" >
													<label>iServe * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="txtiServe">
                                                            <option selected value="nones">Please Select</option>
                                                            <option value="Trainers">Trainers</option>
                                                            <option value="Hosts">Hosts</option>
                                                            <option value="Ushers">Ushers</option>
                                                            <option value="Protocol">Protocol</option>
                                                            <option value="Hospitality">Hospitality</option>
                                                            <option value="Builders Worship">Builders Worship</option>
                                                            <option value="Multimedia – Sound">Multimedia – Sound</option>
                                                            <option value="Multimedia – Video">Multimedia – Video</option>

                                                            <option value="Builders Kidz">Builders Kidz</option>
                                                            <option value="J316">J316</option>
                                                            <option value="Leadersheep">Leadersheep</option>
                                                            <option value="Tribe Leaders">Tribe Leaders</option>
                                                            <option value="None">None</option>
                                                
												
													</select>
													</div>

                                                 

                                               
											
											</div>
											<div class="kt-portlet__foot">
												<div class="kt-form__actions">
													
                                          
													<button type="reset" runat="server" id="btnCancel" onserverclick="btnCancel_ServerClick" class="btn btn-secondary">Cancel</button>
                                                    <button type="reset" runat="server" id="BtnSave"  onserverclick="BtnSave_ServerClick"   class="btn btn-primary">Search</button>
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



                    
             
                    	<div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" id="MainGrid" runat="server" visible="false">

						<!-- begin:: Content -->
						<div class="kt-container  kt-grid__item kt-grid__item--fluid">
							<div class="row">
								<div class="col-md-12">

									<!--begin::Portlet-->
									<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title"> <label runat="server" id="lblServee"></label></h3>
                                                 
											</div>

                                            <div class="kt-portlet__head-label">
                                                         <button type="reset" runat="server" id="btnShowCheckHistory" visible="false"  onserverclick="btnShowCheckHistory_ServerClick"   class="btn pull-right btn-primary">Checklist History</button>  &nbsp;&nbsp;&nbsp;&nbsp;
                                             <button type="reset" runat="server" id="btnShowChecklist"  visible="false" onserverclick="btnShowChecklist_ServerClick"   class="btn pull-right btn-secondary">Complete Checklist</button>
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



                       <div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" runat="server" id="DivCapture" visible="false">  	<!-- Add Work here -->

						<!-- begin:: Content -->
						<div class="kt-container  kt-grid__item kt-grid__item--fluid">
							<div class="row">
								<div class="col-md-12">

									<!--begin::Portlet-->
									<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">Check In</h3>
											</div>
										</div>

										<!--begin::Form-->
										<form class="kt-form">
											<div class="kt-portlet__body">
											

                                    	<div class="form-group">

                                            	<label>Department </label>
													<input type="text" class="form-control" id="txtDepartMent" runat="server" readonly>
													
												</div>

                                                <div class="form-group">
													<label>Name </label>
													<input type="text" class="form-control" id="txtName" runat="server" readonly>
													
												</div>


                                                            <div class="form-group">
													<label>Arrival Time </label>
										<input class="form-control" id="kt_timepicker_1" readonly placeholder="Select time" type="text"  runat="server"/>
													
												</div>


                                                    <div class="form-group">
													<label>Duties for the day </label>
													<input type="text" class="form-control" id="txtNoTrainies" runat="server" >
													
												</div>


                                                    


                                          


                                   


                                                      <div class="form-group">
													<label>Comments (please state reason if late) </label>
													<input type="text" class="form-control" id="txtComments" autocomplete="off" runat="server" >
													
												</div>

											
											</div>
											<div class="kt-portlet__foot">
												<div class="kt-form__actions">
													
                                          
													<button type="reset" runat="server" id="btnBacks" onserverclick="btnBacks_ServerClick" class="btn btn-secondary">Cancel</button>
                                                    <button type="reset" runat="server" id="btnSaveCheckIN"  onserverclick="btnSaveCheckIN_ServerClick"   class="btn btn-primary">Submit</button>
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



                    	<div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" id="HistoryIserver" runat="server" visible="false">

						<!-- begin:: Content -->
						<div class="kt-container  kt-grid__item kt-grid__item--fluid">
							<div class="row">
								<div class="col-md-12">

									<!--begin::Portlet-->
									<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title"> <label runat="server" id="lblMName"></label></h3>
											</div>
										</div>

										<!--begin::Form-->
										<form class="kt-form">
											<div class="kt-portlet__body">
											

                                                  <div class="table-responsive">
											       <asp:Literal ID="tbtHistory" runat="server"></asp:Literal>
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




                                  
                    <div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" runat="server" id="ChecklistWorship" visible="false">  	

						<!-- begin:: Content -->
						<div class="kt-container  kt-grid__item kt-grid__item--fluid">
							<div class="row">
								<div class="col-md-12">

									<!--begin::Portlet-->
									<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">GLORY EXPERIENCE CHECKLIST
                                                                            BUILDERS WORSHIP</h3>
											</div>
										</div>

										<!--begin::Form-->
									
											<div class="kt-portlet__body">
											

                                    

                                               

                                                 <div class="form-group" runat="server" id="Div2" >
													<label>SATURDAY REHEARSALS ATTENDED * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Worship1">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>



                                                    <div class="form-group" runat="server" id="Div1" >
													<label>DRESS REHEARSALS ATTENDED * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Worship2">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                       <div class="form-group" runat="server" id="Div3" >
													<label>TEAM BRIEFING HELD BEFORE THE SERVICE? * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Worship3">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                  <div class="form-group" runat="server" id="Div4" >
													<label>DRESS CODE AND PRESENTATION * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Worship4">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                 <div class="form-group" runat="server" id="Div5" >
													<label>SEATED IN THE PROPER PLACE * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Worship5">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                 <div class="form-group" runat="server" id="Div6" >
													<label>START ON TIME * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Worship6">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                <div class="form-group" runat="server" id="Div7" >
													<label>FINISH ON TIME * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Worship7">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>



                                                      	<div class="form-group">
													<label>Comments</label>
													<input type="text" class="form-control" runat="server"  id="Worship8" aria-describedby="Name" placeholder="Enter Comments">
													</div>


                                                 

                                               
											
											</div>
											<div class="kt-portlet__foot">
												<div class="kt-form__actions">
													
                                          
													<button type="reset" runat="server" id="btnWorshipCancel" onserverclick="btnWorshipCancel_ServerClick" class="btn btn-secondary">Cancel</button>
                                                    <button type="reset" runat="server" id="btnWorship"  onserverclick="btnWorship_ServerClick"   class="btn btn-primary">Save</button>
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

                     <div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" runat="server" id="ChecklistHosts" visible="false">  	

						<!-- begin:: Content -->
						<div class="kt-container  kt-grid__item kt-grid__item--fluid">
							<div class="row">
								<div class="col-md-12">

									<!--begin::Portlet-->
									<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">GLORY EXPERIENCE CHECKLIST

                                                            HOSTS</h3>
											</div>
										</div>

										<!--begin::Form-->
								
											<div class="kt-portlet__body">
											

                                    

                                               

                                                 <div class="form-group" runat="server" id="Div9" >
													<label>VENUE AND YARD CLEANED * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Hosts1">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>



                                                    <div class="form-group" runat="server" id="Div10" >
													<label>TEAM BRIEFING HELD BEFORE THE SERVICE? * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Hosts2">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                       <div class="form-group" runat="server" id="Div11" >
													<label>DRESS CODE AND PRESENTATION CORRECT * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Hosts3">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                  <div class="form-group" runat="server" id="Div12" >
													<label>CHURCH ARTICLES AVAILABLE AND READY * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Hosts4">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                 <div class="form-group" runat="server" id="Div13" >
													<label>REFRESHMENTS FOR GUESTS ANS PASTORS PREPARED
                                                            WHEN NECESSARY * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Hosts5">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                 <div class="form-group" runat="server" id="Div14" >
													<label>GUESTS WELCOMED AND SITTED PROPERLY * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Hosts6">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                <div class="form-group" runat="server" id="Div15" >
													<label>VISIBILITY AND ALERTNESS DURING THE SERVICE * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Hosts7">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>

                                                   <div class="form-group" runat="server" id="Div8" >
													<label>COFFEE AND JUICE PREPARED ON TIME FOR VISITORS * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Hosts8">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                  <div class="form-group" runat="server" id="Div16" >
													<label>DATA COLECTED AND REPORTED * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Hosts9">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>

                                                <div class="form-group" runat="server" id="Div17" >
													<label>OFFERING WELL RECEIVED AND SAFELY ESCORTED * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Hosts10">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>

                                                 <div class="form-group" runat="server" id="Div18" >
													<label>WAS PRAYER ATTENDED? * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Hosts11">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>



                                                      	<div class="form-group">
													<label>Comments</label>
													<input type="text" class="form-control" runat="server"  id="Hosts12" aria-describedby="Name" placeholder="Enter Comments">
													</div>


                                                 

                                               
											
											</div>
											<div class="kt-portlet__foot">
												<div class="kt-form__actions">
													
                                          
													<button type="reset" runat="server" id="btnCancelHost"  onserverclick="btnWorshipCancel_ServerClick" class="btn btn-secondary">Cancel</button>
                                                    <button type="reset" runat="server" id="btnSaveHosts"  onserverclick="btnSaveHosts_ServerClick"   class="btn btn-primary">Save</button>
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

                     <div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" runat="server" id="ChecklistMULTIMEDIA" visible="false">  	

						<!-- begin:: Content -->
						<div class="kt-container  kt-grid__item kt-grid__item--fluid">
							<div class="row">
								<div class="col-md-12">

									<!--begin::Portlet-->
									<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">MULTIMEDIA</h3>
											</div>
										</div>

										<!--begin::Form-->
									
											<div class="kt-portlet__body">
											

                                    

                                               

                                                 <div class="form-group" runat="server" id="Div20" >
													<label>WERE REHEARSALS ATTENDED * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Multi1">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>



                                                    <div class="form-group" runat="server" id="Div21" >
													<label>EQUIPMENT SET UP ON TIME * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Multi2">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                       <div class="form-group" runat="server" id="Div22" >
													<label>TEAM BRIEFING HELD BEFORE THE SERVICE? * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Multi3">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                  <div class="form-group" runat="server" id="Div23" >
													<label>SOUND CHECK DONE ON TIME * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Multi4">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                 <div class="form-group" runat="server" id="Div24" >
													<label>VISITOR’S DVDS DONE ON TIME * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Multi5">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                 <div class="form-group" runat="server" id="Div25" >
													<label>SALES DESK SET UP AND UP TO DATE * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Multi6">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                <div class="form-group" runat="server" id="Div26" >
													<label>SONG DISPLAY ACCURATE * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Multi7">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>

                                                   <div class="form-group" runat="server" id="Div27" >
													<label>5 MIN TIMER STARTED ON TIME * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Multi8">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                  <div class="form-group" runat="server" id="Div28" >
													<label>ANNOUNCEMENTS QUED CORRECTLY * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Multi9">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>

                                                <div class="form-group" runat="server" id="Div29" >
													<label>WAS THE SOUND QUALITY GOOD? * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Multi10">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>

                                           



                                                      	<div class="form-group">
													<label>Comments</label>
													<input type="text" class="form-control" runat="server"  id="Multi11" aria-describedby="Name" placeholder="Enter Comments">
													</div>


                                                 

                                               
											
											</div>
											<div class="kt-portlet__foot">
												<div class="kt-form__actions">
													
                                          
													<button type="reset" runat="server" id="Button3"  onserverclick="btnWorshipCancel_ServerClick" class="btn btn-secondary">Cancel</button>
                                                    <button type="reset" runat="server" id="btnSaveMulti"  onserverclick="btnSaveMulti_ServerClick"   class="btn btn-primary">Save</button>
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

                      <div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" runat="server" id="ChecklistKids" visible="false">  	

						<!-- begin:: Content -->
						<div class="kt-container  kt-grid__item kt-grid__item--fluid">
							<div class="row">
								<div class="col-md-12">

									<!--begin::Portlet-->
									<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">GLORY EXPERIENCE CHECKLIST

                                                                    BUILDERS KIDZ</h3>
											</div>
										</div>

										<!--begin::Form-->
								
											<div class="kt-portlet__body">
											

                                    

                                               

                                                 <div class="form-group" runat="server" id="Div30" >
													<label>WELCOMING AREA READY AT 9:00AM * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Kids1">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>



                                                    <div class="form-group" runat="server" id="Div31" >
													<label>EQUIPMENT SET UP ON TIME * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Kids2">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                       <div class="form-group" runat="server" id="Div32" >
													<label>TEAM BRIEFING HELD BEFORE THE SERVICE? * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Kids3">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                  <div class="form-group" runat="server" id="Div33" >
													<label>LEASSON PACKS READY ON TIME * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Kids4">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                 <div class="form-group" runat="server" id="Div34" >
													<label>VISITOR’S PACKS READY ON TIME * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Kids5">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                 <div class="form-group" runat="server" id="Div35" >
													<label>SIGN IN FACILITY USED PROPERLY * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Kids6">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>


                                                <div class="form-group" runat="server" id="Div36" >
													<label>DRESS CODE AND PRESENTATION * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Kids7">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>

                                                   <div class="form-group" runat="server" id="Div37" >
													<label>BUILDERS KIDZ SERVICE START ON TIME * </label>
                                                     	 <select class="custom-select form-control" runat="server" id="Kids8">
                                                            <option value="Yes">Yes</option>
                                                            <option value="No">No</option>
													</select>
													</div>





                                                      	<div class="form-group">
													<label>Comments</label>
													<input type="text" class="form-control" runat="server"  id="Kids9" aria-describedby="Name" placeholder="Enter Comments">
													</div>


                                                 

                                               
											
											</div>
											<div class="kt-portlet__foot">
												<div class="kt-form__actions">
													
                                          
													<button type="reset" runat="server" id="Button5"  onserverclick="btnWorshipCancel_ServerClick" class="btn btn-secondary">Cancel</button>
                                                    <button type="reset" runat="server" id="btnSaveJesusKids"  onserverclick="btnSaveJesusKids_ServerClick"   class="btn btn-primary">Save</button>
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



                     	<div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" id="HolderChecklist" runat="server" visible="false">

						<!-- begin:: Content -->
						<div class="kt-container  kt-grid__item kt-grid__item--fluid">
							<div class="row">
								<div class="col-md-12">

									<!--begin::Portlet-->
									<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title"> <label runat="server" id="lblHolderChecklist"></label></h3>
											</div>
										</div>

										<!--begin::Form-->
										<form class="kt-form">
											<div class="kt-portlet__body">
											

                                                  <div class="table-responsive">
											       <asp:Literal ID="tbtChecklistHeader" runat="server"></asp:Literal>
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

		<!-- end:: Aside -->
         	<!-- begin::Offcanvas Toolbar Profile -->
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



        function AddKids() {
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


        
        function CheckIN(id) {
            document.getElementById('MemberID').value = id;
            var clickbutton = document.getElementById('btnCheckingIN');
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
		        var clickbutton = document.getElementById('btnViewhistor');
		        clickbutton.click();

		    }


		    function BtnCheckIN(id) {
		        document.getElementById('MemberID').value = id;
		        var clickbutton = document.getElementById('btnCheckingIN');
		        clickbutton.click();

		    }

		    function BtnCheckOut(id) {
		        document.getElementById('MemberID').value = id;
		        var clickbutton = document.getElementById('btnCheckingOut');
		        clickbutton.click();

		    }




		    function BtnArchMem(id) {


		        var IsAdmin = document.getElementById('IsAdmin').value;
		        if (IsAdmin == "1") {
		            document.getElementById('MemberID').value = id;
		            var clickbutton = document.getElementById('RemoveMember');
		            clickbutton.click();
		        }
		        else {

		            var clickbutton = document.getElementById('NotieAccessRight');
		            clickbutton.click();

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

        	<script src="assets/js/pages/components/forms/widgets/bootstrap-select.js" type="text/javascript"></script>
        <script src="assets/js/pages/components/forms/widgets/bootstrap-timepicker.js" type="text/javascript"></script>
	</body>

	<!-- end::Body -->


</html>
