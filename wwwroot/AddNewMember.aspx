<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNewMember.aspx.cs" Inherits="AddNewMember" %>


<!DOCTYPE html>

<html lang="en">
<head runat="server">
		<base href="">
		<meta charset="utf-8" />
	<title>Church_App</title>
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




      function namedata(data) {
          if (data == 'Single') {
              $('#DivMarriageDate').css('display', 'none');
        
          }
          else {
              $('#DivMarriageDate').css('display', 'block');
              
          }
      }


  </script>
          
</head>
	<!-- begin::Body -->
	<body id="Body1" runat="server"  class="kt-quick-panel--right kt-demo-panel--right kt-offcanvas-panel--right kt-header--fixed kt-header-mobile--fixed kt-subheader--fixed kt-aside--enabled kt-aside--left kt-aside--fixed kt-aside--offcanvas-default kt-page--loading">
     

         <form id="form1" runat="server" class="kt-quick-panel--right kt-demo-panel--right kt-offcanvas-panel--right kt-header--fixed kt-header-mobile--fixed kt-subheader--fixed kt-aside--enabled kt-aside--left kt-aside--fixed kt-aside--offcanvas-default kt-page--loading">
            <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieSave" style="display:none;"> Success</button>
         <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieRemove" style="display:none;"> Success</button>
           <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieComplete" style="display:none;"> Success</button>
          <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="ExistNotie" style="display:none;"> Success</button>
           <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieInvalidID" style="display:none;"> Success</button>
             <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieAge" style="display:none;"> Success</button>

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
							<img alt="Logo" width="55px"  src="assets/media/logos/logo-1.png" />
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
								<a href="NewMembers.aspx" class="btn btn-default btn-sm btn-bold btn-upper">Back</a>
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
					
                    
                    
                    
                    <div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" id="kt_content">  	<!-- Add Work here -->

						<!-- begin:: Content -->
						<div class="kt-container  kt-grid__item kt-grid__item--fluid">
							<div class="row">
								<div class="col-md-12">

									<!--begin::Portlet-->
									<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">Membership Form</h3>
											</div>
										</div>

										<!--begin::Form-->
										<form class="kt-form">
											<div class="kt-portlet__body">
											

                                                	<div class="form-group form-group-last">
													<div class="alert alert-secondary" role="alert">
														<div class="alert-icon"><i class="flaticon-warning kt-font-brand"></i></div>
														<div class="alert-text">
															We remove all the special characters on saving
														</div>
													</div>
												</div>


                                                  <div class="form-group" runat="server" id="DivIDNo">
													<label>ID Number *</label>
                                                    
												 <input id="textfield" type="text" name="Custom Text" placeholder="Enter ID Number" class="form-control" runat="server"  autocomplete="off"/> 
                                                      <span id="IDErrorMessage" style="display:none" class="form-text text-muted">Invalid ID</span>

													</div>
                                               
 


												<div class="form-group">
													<label>Surname * </label>
													<input type="text" class="form-control" id="txtSurname" autocomplete="off" runat="server" aria-describedby="Surnames" placeholder="Enter Surname">
													
												</div>

                                                	<div class="form-group">
													<label>Full Name * </label>
													<input type="text" class="form-control" autocomplete="off" runat="server" id="txtName" aria-describedby="Name" placeholder="Enter Name">
													</div>

                                                   <div class="form-group" runat="server" id="DivKnownAs">
													<label>Known As</label>
													<input type="text" class="form-control"  autocomplete="off" id="txtKnownAS" runat="server" aria-describedby="KnowAS" placeholder="Enter Known As">
													</div>



                                                	<div class="form-group">
													<label>Gender * </label>
												   <select class="custom-select form-control" runat="server" id="CmdGender">
														<option selected  value="none">Please Select</option>
														<option value="Female">Female</option>
														<option value="Male">Male</option>
												
													</select>
													</div>


                                                  <div class="form-group">
													<label>Date of birth * </label>
                                                  
													<input type="text" class="form-control" id="datepicker" autocomplete="off" runat="server" >
													</div>


                                                 <div class="form-group" runat="server" id="DivWard">
													<label>Ward * </label>
													 <select class="custom-select form-control" runat="server" id="CmdWard">
                                                        <option selected value="none">Please Select</option>
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
													<label>Marital Status * </label>
													 <select class="custom-select form-control"  onchange="return namedata(this.value);" runat="server" id="CmdMarital">
														<option selected value="none">Please Select</option>
														<option value="Single">Single</option>
														<option value="Married">Married</option>
												
													</select>
													</div>
                                             

                                                            <div class="form-group" runat="server" id="DivMarriageDate" visible="false">
													<label>Marriage Date</label>
                                                  
													<input type="text" class="form-control" id="txtMarriageDate" autocomplete="off" runat="server" >
													</div>

                                                
                                                  <div class="form-group" runat="server" id="DivOccupation">
													<label>Occupation or Schooling</label>
													<input type="text" class="form-control" id="txtOccupation" autocomplete="off" runat="server" aria-describedby="IDNo" placeholder="Enter Occupation or Schooling">
													</div>

                                                   <div class="form-group" runat="server" id="DivEmployer">
													<label>Employer or School</label>
													<input type="text" class="form-control" id="txtEmployer" autocomplete="off" runat="server" aria-describedby="Employer" placeholder="Enter Employer or School">
													</div>

                                                 <div class="form-group" runat="server" id="DivSkills">
													<label>Skills</label>
													<input type="text" class="form-control" id="txtSkills" autocomplete="off" runat="server" aria-describedby="Skills" placeholder="Enter Skills">
													</div>


                                                 <div class="form-group">
													<label>Residential Address * </label>
													<input type="text" class="form-control" id="txtAddress" autocomplete="off" runat="server" aria-describedby="address" placeholder="Enter Address">
													</div>



                                                 <div class="form-group" runat="server" id="DivCampus" visible="false">
													<label>Campus * </label>
													 <select class="custom-select form-control" runat="server" id="CmdCampus">
                                                        <option selected value="none">Please Select</option>
                                                        <option value="Delmas Campus">Delmas Campus</option>
                                                       <option value="Benoni Campus">Benoni Campus</option>
                                                        
													</select>
													</div>


                                                 <div class="form-group" runat="server" id="DivZone" visible="false">
													<label>Zone * </label>
													 <select class="custom-select form-control" runat="server" id="CmdZone">
                                                        <option selected value="none">Please Select</option>
                                                        <option value="Zone 1">Zone 1</option>
                                               <option value="Zone 2">Zone 2</option>
                                                          <option value="Zone 3">Zone 3</option>
                                                          <option value="Zone 4">Zone 4</option>
                                                          <option value="Zone 5">Zone 5</option>
                                                          <option value="Zone 6">Zone 6</option>
                                                          <option value="Zone 7">Zone 7</option>
                                                          <option value="Zone 8">Zone 8</option>
                                                          <option value="Zone 9">Zone 9</option>
                                                          <option value="Zone 10">Zone 10</option>
                                                       
													</select>
													</div>




                                                 <div class="form-group" runat="server" id="DivZonea" visible="false">
													<label>Zone Description  </label>
													<input type="text" class="form-control" id="txtZoneDesc" autocomplete="off" runat="server" aria-describedby="address" placeholder="Enter Zone Description ">
													</div>


                                                 <div class="form-group" runat="server" id="DivGroup" visible="false">
													<label>Group * </label>
													 <select class="custom-select form-control" runat="server" id="CmdGroup">
                                                        <option selected value="none">Please Select</option>
                                                        <option value="Group 1">Group 1</option>
                                               <option value="Group 2">Group 2</option>
                                                          <option value="Group 3">Group 3</option>
                                                          <option value="Group 4">Group 4</option>
                                                          <option value="Group 5">Group 5</option>
                                                          <option value="Group 6">Group 6</option>
                                                          <option value="Group 7">Group 7</option>
                                                          <option value="Group 8">Group 8</option>
                                                          <option value="Group 9">Group 9</option>
                                                          <option value="Group 10">Group 10</option>
                                                       
													</select>
													</div>





                                                 <div class="form-group" runat="server" id="DivTelH">
													<label>Tel. No: (H)</label>
													<input type="text" class="form-control" id="txtTellH" autocomplete="off" runat="server" aria-describedby="Tell" placeholder="Enter Tel. No: (H)">
													</div>

                                                  <div class="form-group" runat="server" id="DivTelW">
													<label>Tel. No: (W)</label>
													<input type="text" class="form-control" id="txtTellW" autocomplete="off" runat="server" aria-describedby="txtTellW" placeholder="Enter Tel. No: (W)">
													</div>

                                                  <div class="form-group" runat="server" id="DivFax">
													<label>Fax. No</label>
													<input type="text" class="form-control" id="txtFax" autocomplete="off" runat="server" aria-describedby="txtFax" placeholder="Enter Fax. No">
													</div>

                                                 <div class="form-group">
													<label>Cell phone</label>
													<input type="text" class="form-control" id="txtCellNo" autocomplete="off" runat="server" aria-describedby="txtCellNo" placeholder="Enter Cell No">
													</div>

                                                <div class="form-group">
													<label>Email</label>
													<input type="email" class="form-control" id="txtEmail" name="email" autocomplete="off" runat="server" aria-describedby="txtEmail" placeholder="Enter Email">
													</div>


                                                     <div class="form-group" runat="server" id="DivMinistries">
													<label>Ministries</label>
													 <select class="custom-select form-control" runat="server" id="CmdMinistries">
														<option selected value="none">Please Select</option>
														<option value="Sunday School">Sunday School</option>
														<option value="Youth">Youth</option>
                                                         <option value="Christian Women Ministries">Christian Women Ministries</option>
                                                          <option value="Christian Men Ministries">Christian Men Ministries</option>
												
													</select>
													</div>

                                                  <div class="form-group" runat="server" id="DivChurchInvolve">
													<label>Church Involvement</label>
													<input type="text" class="form-control" id="txtChurchInv" autocomplete="off" runat="server" aria-describedby="txtChurchInv" placeholder="Enter Church Involvement">
													</div>

                                                <div class="form-group" runat="server" id="DivStatus">
													<label>Status</label>
													 <select class="custom-select form-control" runat="server" id="CmdStatus">
														<option selected value="none">Please Select</option>
														<option value="Baptized Member">Baptized Member</option>
														<option value="Catechumen">Catechumen</option>
                                                         <option value="Prospective Member">Prospective Member</option>
                                                          <option value="Full Member">Full Member</option>
												
													</select>
													</div>
                                             


                                                      <div class="form-group" runat="server" id="DivmemberObtain">
													<label>Date Membership obtained</label>
                                                  
													<input type="text" class="form-control" id="txtMemberObtained" autocomplete="off" runat="server" >
													</div>


                                                 <div class="form-group" runat="server" id="DivSpritiualGifts">
													<label>Spiritual Gifts</label>
													<input type="text" class="form-control" id="txtSpiritualGifts" autocomplete="off" runat="server" aria-describedby="txtChurchInv" placeholder="Enter Spiritual Gifts">
													</div>



                                                      <div class="form-group" runat="server" id="DivNewbeliever" visible="false">
													<label>New Believers Experience</label>
													<input type="text" class="form-control" id="txtNewBeliever" autocomplete="off" runat="server" aria-describedby="txtNewBeliever" placeholder="Enter New Believers Experience">
													</div>


                                                  <div class="form-group" runat="server" id="DiVIconnect" visible="false">
													<label>iConnect Experience</label>
													<input type="text" class="form-control" id="txtIconnect" autocomplete="off" runat="server" aria-describedby="txtNewBeliever" placeholder="Enter iConnect Experience">
													</div>


                                                     <div class="form-group" runat="server" id="DivBaptism" visible="false">
													<label>Baptism</label>
											
                                                      <select class="custom-select form-control" runat="server" id="txtBaptism">
														<option selected value="none">Please Select</option>
														<option value="Yes">Yes</option>
														<option value="No">No</option>
                                                        
												
													</select>


													</div>

                                                   <div class="form-group" runat="server" id="DivFFS">
													<label>FFS Member</label>
													 <select class="custom-select form-control" runat="server" id="CmdFFS">
														<option selected value="none">Please Select</option>
														<option value="Yes">Yes</option>
														<option value="No">No</option>
                                                        
												
													</select>
													</div>

                                                  <div class="form-group" runat="server" id="DivSms" visible="false">
													<label>SMS * </label>
													 <select class="custom-select form-control" runat="server" id="CmdSms">
														<option selected value="none">Please Select</option>
														<option value="Yes">Yes</option>
														<option value="No">No</option>
                                                        
												
													</select>
													</div>


                                                 <div class="form-group" runat="server" id="DivGrowthPath" visible="false">
													<label>Growth Path * </label>
											 <select class="custom-select form-control" runat="server" id="CmdGrowthPath">
                                                        <option selected value="nones">Please Select</option>
                                                        <option value="New Believers Experience">New Believers Experience</option>
                                                        <option value="iConnect Experience">iConnect Experience</option>
                                                        <option value="Baptism">Baptism</option>
                                                            <option value="Member">Member</option>
                                                            <option value="iServe">iServe</option>
                                                           <option value="None">None</option>
                                                
												
													</select>
													</div>


                                                 <div class="form-group" runat="server" id="DivIserve" visible="false">
													<label>iServe * </label>

                                            <select class="custom-select form-control" runat="server" id="txtiServe">
                                                            <option selected value="nones">Please Select</option>
                                                            <option value="Trainers">Trainers</option>
                                                            <option value="Hosts">Hosts</option>
                                                            <option value="Hosts – Ushers">Hosts – Ushers</option>
                                                            <option value="Hosts – Protocol">Hosts – Protocol</option>
                                                            <option value="Hosts – Hospitality">Hosts – Hospitality</option>
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

                                                     <div class="form-group" runat="server" id="DivComment" visible="false">
													<label>Comment</label>
													<input type="text" class="form-control" id="txtComment" autocomplete="off" runat="server" aria-describedby="txtNewBeliever" placeholder="Enter Comment">
													</div>


                                                   <div class="form-group" runat="server" id="ImageDiv" visible="false">
													<label>Add Picture</label>
													   <asp:FileUpload ID="UploadImage" runat="server" CssClass="form-control" />
													</div>

											
											</div>
											<div class="kt-portlet__foot">
												<div class="kt-form__actions">
													<button type="reset" runat="server" id="BtnSave"  onserverclick="BtnSave_ServerClick"  class="btn btn-primary">Submit</button>
                                                    <button type="reset" runat="server" id="BtnSaveMembers"  onserverclick="BtnSaveMembers_ServerClick"  visible="false" class="btn btn-primary">Submit</button>
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
    	<script src="assets/js/pages/components/forms/validation/controls.js" type="text/javascript"></script>
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


           function ExistNotie() {

               var clickbutton = document.getElementById('ExistNotie');
               clickbutton.click();

           }

           function InvalidIDNotie() {

               var clickbutton = document.getElementById('NotieInvalidID');
               clickbutton.click();

           }



           function MemberYoung() {

               var clickbutton = document.getElementById('NotieAge');
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


    $('#txtMemberObtained').datetimepicker({
        format: "yyyy/mm/dd",
        todayHighlight: true,
        autoclose: true,
        startView: 2,
        minView: 2,
        forceParse: 0,
        pickerPosition: 'bottom-right'
    });

    $('#txtMarriageDate').datetimepicker({
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
</script>


       
	</body>

	<!-- end::Body -->
</html>
