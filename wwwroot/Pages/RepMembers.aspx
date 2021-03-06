<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="Reports" %>


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
    

               <input runat="server" id="GetStartDate" type="hidden" />
                <input runat="server" id="GetEndDate" type="hidden" />
            
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
												<h3 class="kt-portlet__head-title">  <label runat="server" id="HeadLabel"></label> </h3>
											</div>
										</div>
                                         

										<!--begin::Form-->
										<form class="kt-form">

											<div class="kt-portlet__body">

                                                 	<div class="form-group form-group-last">
													<div class="alert alert-secondary" role="alert">
														<div class="alert-icon"><i class="flaticon-warning kt-font-brand"></i></div>
														<div class="alert-text">
															Build your report using date range
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
												<label>&nbsp;&nbsp;&nbsp;Select date range *</label>
											<div class="col-lg-4 col-md-12 col-sm-12">
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
                                                <div runat="server" id="DivWeekDate" visible="false">




                                                        <div class="form-group">
                                                         
													<label>Week 1  *</label>
													<input type="text" class="form-control" runat="server"  id="txtWeek1" autocomplete="off" aria-describedby="Name" placeholder="Enter  Date">

                                                             	
													</div>

                                                          <div class="form-group">
                                                         
													<label>Week 2  *</label>
													<input type="text" class="form-control" runat="server"  id="txtWeek2" autocomplete="off" aria-describedby="Name" placeholder="Enter  Date">

                                                             	
													</div>

                                                          <div class="form-group">
                                                         
													<label>Week 3  *</label>
													<input type="text" class="form-control" runat="server"  id="txtWeek3" autocomplete="off" aria-describedby="Name" placeholder="Enter  Date">

                                                             	
													</div>


                                                          <div class="form-group">
                                                         
													<label>Week 4  *</label>
													<input type="text" class="form-control" runat="server"  id="txtWeek4" autocomplete="off" aria-describedby="Name" placeholder="Enter  Date">

                                                             	
													</div>


                                                    </div>

                                                           <div class="form-group">
                                                               <br />
                                                               <button type="reset" runat="server" id="btnSearchRep" onserverclick="btnSearchRep_ServerClick"  visible="true"  class="btn btn-primary">View Graph</button>
													<button type="reset" runat="server" id="btndownloadPDF" onserverclick="btndownloadPDF_ServerClick" class="btn btn-secondary">Download PDF</button>
                                                               </div>
                                    
                                                	
											
											</div>
										
										</form>







										<!--end::Form-->
									</div>





                                    <div class="kt-portlet" runat="server" id="BarGraphRep" visible="false">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title"><label runat="server" id="BarLal"></label> </h3>
											</div>

										</div>
										<div class="kt-portlet__body">
											<div id="BarGraph" style="height:500px;"></div>
										</div>
									</div>




									<!--begin::Portlet-->
									<div class="kt-portlet" runat="server" id="AllDiv">
										<div class="kt-portlet__head">
											<div class="kt-portlet__head-label">
												<h3 class="kt-portlet__head-title">All Builders Kidz</h3>
											</div>

                                                <div class="kt-portlet__head-label">
                                             <button type="reset" runat="server" id="btnExcel" onserverclick="btnExcel_ServerClick"  visible="true"   class="btn pull-right btn-secondary">Export to excel</button>
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


    <script type="text/javascript">



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


        function KidsBarGraph() {

            $.ajax({
                type: 'POST',
                url: "Reports.aspx/KidRepGraph",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: {},
                success: function (result) {

                    new Morris.Bar({
                        element: 'BarGraph',
                        behaveLikeLine: true,
                        data: result.d,
                        xkey: 'w',
                        ykeys: ['a', 'b', 'c', 'x', 'y', 'z', 'x1', 'y1', 'z1'],
                        labels: ['Attendence 3-5 (Delmas)', 'Attendence 3-5 (Benoni)', 'Eloff 3-5 (Delmas)', 'Attendence 6-8 (Delmas)', 'Attendence 6-8 (Benoni)', 'Attendence 6-8 (Eloff)', 'Attendence 9-12 (Delmas)', 'Attendence 9-12 (Benoni)', 'Attendence 9-12 (Eloff)'],
                        pointSize: 1,
                        hideHover: 'auto',
                        barColors: ['#2abe81', '#24a5ff', '#6e4ff5', '#2abe81', '#24a5ff', '#6e4ff5', '#2abe81', '#24a5ff', '#6e4ff5']

                    });
                },
                error: function (error) { alert(error.responseText); }
            });
        }


        function DiscBarGraph() {

            $.ajax({
                type: 'POST',
                url: "Reports.aspx/DiscRepGraph",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: {},
                success: function (result) {
                   
                    new Morris.Bar({
                        element: 'BarGraph',
                        behaveLikeLine: true,
                        data: result.d,
                        xkey: 'w',
                        ykeys: ['a', 'b', 'c', 'x', 'y', 'z'],
                        labels: ['No. of souls actually attended (Delmas)', 'No. of souls invited (Benoni)', 'No. of souls actually attended (Delmas)', 'No. of souls actually attended (Delmas)', 'No. of souls invited (Benoni)', 'No. of souls invited (Eloff)'],
                        pointSize: 1,
                        hideHover: 'auto',
                        barColors: ['#2abe81', '#24a5ff', '#6e4ff5', '#2abe81', '#24a5ff', '#6e4ff5']

                    });
                },
                error: function (error) { alert(error.responseText); }
            });
        }







        function IconnectBarGraph() {

            $.ajax({
                type: 'POST',
                url: "Reports.aspx/iConnectPastorRepGraph",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: {},
                success: function (result) {
               
                    new Morris.Bar({
                        element: 'BarGraph',
                        behaveLikeLine: true,
                        data: result.d,
                        xkey: 'w',
                        ykeys: ['a', 'b'],
                        labels: ['NO. OF ACTUAL ATTENDANCE', 'NO. OF POSSIBLE ATTENDANCE'],
                        pointSize: 1,
                        hideHover: 'auto',
                        barColors: ['#2abe81', '#24a5ff']

                    });
                },
                error: function (error) { alert(error.responseText); }
            });
        }




        //function BarGraph() {

        //    new Morris.Bar({
        //        element: 'BarGraph',
        //        data: [{
        //            y: 'Attendence 3-5 year',
        //            a: 100,
        //            b: 90,
        //            C: 90
        //        },
                   
        //            {
        //                y: 'Attendence 6-8 year',
        //                a: 50,
        //                b: 40,
        //                C: 30
        //            },
        //            {
        //                y: 'Attendence 9-12 year',
        //                a: 75,
        //                b: 65,
        //                C: 80
        //            }
                   
        //        ],
        //        xkey: 'y',
        //        ykeys: ['a', 'b', 'C'],
        //        labels: ['Delmas Campus', 'Benoni Campus', 'Eloff Campus'],
        //        barColors: ['#2abe81', '#24a5ff', '#6e4ff5']
        //    });
        //}









</script>
      
<%--        <script type="text/javascript">
            $(document).ready(function () {
                $.ajax({
                    type: 'POST',
                    url: "Reports.aspx/GetChartData",
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    data: {},
                    success: function (result) {

                        new Morris.Bar({
                            element: 'BarGraph',
                            behaveLikeLine: true,
                            data: result.d,
                            xkey: 'w',
                            ykeys: ['x', 'y', 'z'],
                            labels: ['Delmas Campus', 'Benoni Campus', 'Eloff Campus'],
                            pointSize: 1,
                            hideHover: 'auto',
                            barColors: ['#2abe81', '#24a5ff', '#6e4ff5']
                           
                        });
                    },
                    error: function (error) { alert(error.responseText); }
                });
            });
</script>--%>

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

		        $('#AA').dataTable();

		    });
		</script>




          <script>
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
