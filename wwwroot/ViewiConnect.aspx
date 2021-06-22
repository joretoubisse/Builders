<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewiConnect.aspx.cs" Inherits="ViewiConnect" %>


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
      $(function () {
          $("#datepicker").datepicker();
      });

      $(function () {
          $("#txtMemberObtained").datepicker();
      });

      $(function () {
          $("#txtMarriageDate").datepicker();
      });
      

      $(function () {
          $("#txtFirstVisit").datepicker();
      });


      $(function () {
          $("#txtDateSalvation").datepicker();
      });


      $(function () {
          $("#txtFirstCall").datepicker();
      });


      
      
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
             <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="btnEditConnectGroups" runat="server" onserverclick="btnEditConnectGroups_ServerClick" style="display:none;"> Success</button>
           <input runat="server" id="GetIDno" type="hidden" />


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
									<a href="ConnectGroups.aspx" class="btn btn-default btn-sm btn-bold btn-upper">Back</a>
								
							
								</div>
							</div>

							<!-- end:: Sub-header toolbar -->
						</div>
					</div>

					<!-- end:: Subheader -->
					
                    
                    
              	<div class="kt-content  kt-grid__item kt-grid__item--fluid kt-grid kt-grid--hor" id="kt_content">

						<!-- begin:: Content -->
						<div class="kt-container  kt-grid__item kt-grid__item--fluid" runat="server" id="MainRow"  >
							<div class="row">
								<div class="col-md-6">

									<!--begin::Portlet-->
									<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title"> Edit Connect Groups</h3>
											</div>
										</div>

										<!--begin::Form-->
									
											<div class="kt-portlet__body">
											



                                             


                                                  <div class="form-group" runat="server" id="DivZone" >
													<label>Zone  * </label>
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


                                                   <div class="form-group" runat="server" id="DivGroup" >
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


                                                   	<div class="form-group" >
													<label>Group Leader * </label>

                                                          <asp:DropDownList ID="CmdGroupLeader" runat="server" CssClass="custom-select form-control">
                                        </asp:DropDownList>
													</div>


                                                        <div class="form-group" >
													<label>Day </label>
                                                    
												 <select class="custom-select form-control" runat="server" id="CmdDauys">
                                                        <option selected value="none">Please Select</option>
                                                        <option value="Monday">Monday</option>
                                               <option value="Tuesday">Tuesday</option>
                                                          <option value="Wednesday">Wednesday</option>
                                                          <option value="Thursday">Thursday</option>
                                                          <option value="Friday">Friday</option>
                                                          <option value="Saturday">Saturday</option>
                                                          <option value="Sunday">Sunday</option>
                          
                                                       
													</select>
													</div>


                                                             <div class="form-group" >
													<label>Time </label>
                                                    
												<input class="form-control" id="kt_timepicker_1" readonly placeholder="Select time" type="text"  runat="server"/>
													</div>



											
											</div>
											<div class="kt-portlet__foot">
												<div class="kt-form__actions">
												
                                                    <button type="reset" runat="server" id="btnSave"   onserverclick="btnSave_ServerClick"   visible="false" class="btn btn-primary">Save</button>
													<button type="reset" runat="server" id="btnCancel" onserverclick="btnCancel_ServerClick" class="btn btn-secondary">Cancel</button>

												</div>
											</div>
									

										<!--end::Form-->
									</div>

									<!--end::Portlet-->

									
								</div>


                                		<div class="col-md-12">

									<!--begin::Portlet--> 
									<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">Members linked</h3>
											</div>
										</div>

										<!--begin::Form-->
									
											<div class="kt-portlet__body">
											



                                             
											       <asp:Literal ID="tbTable" runat="server"></asp:Literal>
												

											
											</div>
										

										<!--end::Form-->
									</div>

									<!--end::Portlet-->

									
								</div>


							
							</div>


                            



                  
						</div>


                      		<div class="kt-container  kt-grid__item kt-grid__item--fluid" runat="server" id="MemberDetails" visible="false" >
                      	<div class="row" >
								<div class="col-md-12">

									<!--begin::Portlet-->
									<div class="kt-portlet">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">Edit Members Connecting Group</h3>
											</div>
										</div>

										<!--begin::Form-->
									
											<div class="kt-portlet__body">
											
                                                     <div class="form-group">
													<label>Name</label>
													<input type="text" class="form-control" id="txtName" readonly runat="server" aria-describedby="Name">
													</div>


                                                    <div class="form-group">
													<label>Surname</label>
													<input type="text" class="form-control" id="txtSurname" readonly runat="server" aria-describedby="txtSurname">
													</div>




                                             


                                                  <div class="form-group" runat="server" id="Div3" >
													<label>Zone  * </label>
													 <select class="custom-select form-control" runat="server" id="CmdEditZone">
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


                                                   <div class="form-group" runat="server" id="Div4" >
													<label>Group * </label>
													 <select class="custom-select form-control" runat="server" id="CmdEditGroup">
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


                                     



											
											</div>
											<div class="kt-portlet__foot">
												<div class="kt-form__actions">
												
                                                    <button type="reset" runat="server" id="btnUpdateMember" onserverclick="btnUpdateMember_ServerClick"   visible="true" class="btn btn-primary">Update</button>
													<button type="reset" runat="server" id="btnBack" onserverclick="btnBack_ServerClick" class="btn btn-secondary">Cancel</button>

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
					<div class="kt-footer kt-grid__item" id="kt_footer" runat="server" visible="false" style="bottom:0; width:100%;">
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




           function BtnViewEdit(id) {
               document.getElementById('GetIDno').value = id;
               var clickbutton = document.getElementById('btnEditConnectGroups');
               clickbutton.click();

           }



           $(document).ready(function () {

               $('#AA').dataTable();

           });


</script>


            <script src="assets/js/pages/components/forms/widgets/bootstrap-timepicker.js" type="text/javascript"></script>
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
	</body>

	<!-- end::Body -->
</html>
