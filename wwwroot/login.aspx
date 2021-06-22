<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>


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
		<title>Church_App</title>
		<meta name="description" content="User login example">
		<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
		<meta http-equiv="X-UA-Compatible" content="IE=edge" />

		<!--begin::Fonts -->
		<link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700|Raleway:300,400,500,600,700">

		<!--end::Fonts -->

		<!--begin::Page Custom Styles(used by this page) -->
		<link href="assets/css/pages/login/login-v1.css" rel="stylesheet" type="text/css" />

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
	<body style="background-image: url(assets/ImageLogo.jpg); background-size:100%" class="kt-login-v1--enabled kt-quick-panel--right kt-demo-panel--right kt-offcanvas-panel--right kt-header--fixed kt-header-mobile--fixed kt-subheader--fixed kt-aside--enabled kt-aside--left kt-aside--fixed kt-aside--offcanvas-default kt-page--loading">
             <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieSave" style="display:none;"> Success</button>
         <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieRemove" style="display:none;"> Success</button>    
            <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="NotieInvalidPword" style="display:none;"> Success</button>  
		 <button type="button" class="btn btn-success btn-bold btn-font-sm btn-upper" id="AccessDeniedNotie" style="display:none;"> Success</button>  
        
         <form id="form1" runat="server">
		<!-- begin:: Page -->
		<div class="kt-grid kt-grid--ver kt-grid--root">
			<div class="kt-grid__item  kt-grid__item--fluid kt-grid kt-grid--hor kt-login-v1" id="kt_login_v1">

				<!--begin::Item-->
				<div class="kt-grid__item">

					<!--begin::Heade-->
					<div class="kt-login-v1__head">
						<div class="kt-login-v1__logo">
							<a href="#">
								<img src="assets/media/logos/logo-1k.png" width="150px" alt="" />
							</a>
						</div>
						<div class="kt-login-v1__signup">
						
						</div>
					</div>

					<!--begin::Head-->
				</div>

				<!--end::Item-->

				<!--begin::Item-->
				<div class="kt-grid__item  kt-grid kt-grid--ver  kt-grid__item--fluid">

					<!--begin::Body-->
					<div class="kt-login-v1__body">

						<!--begin::Section-->
						<div class="kt-login-v1__section">
							<div class="kt-login-v1__info">
								<h3 class="kt-login-v1__intro">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</h3>
								<p></p>
							</div>
						</div>

						<!--begin::Section-->

						<!--begin::Separator-->
						<div class="kt-login-v1__seaprator"></div>

						<!--end::Separator-->

						<!--begin::Wrapper-->
						<div class="kt-login-v1__wrapper">
							<div class="kt-login-v1__container">
								<h3 class="kt-login-v1__title">
									Sign To Account
								</h3>

								<!--begin::Form-->
								<form class="kt-login-v1__form kt-form" action="" autocomplete="off">
									<div class="form-group">
									   <asp:TextBox runat="server" id="txtUsername" autocomplete="off" TextMode="SingleLine"  Placeholder="Username" CSSClass="form-control"></asp:TextBox>
									</div>
									<div class="form-group">
									    <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" Placeholder="Password" CSSClass="form-control" autocomplete="off"></asp:TextBox>
									</div>
									<label></label>
									<div class="kt-login-v1__actions" >
										<a href="#" class="kt-login-v1__forgot" style="display:none">
											Forgot Password ?
										</a>
                                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
										 <asp:Button ID="btnLogin" runat="server" CSSClass="btn btn-pill btn-elevate" Text="Sign In" OnClick="btnLogin_Click" />  

							
									</div>
								</form>

								<!--end::Form-->

						

						
							</div>
						</div>

						<!--end::Wrapper-->
					</div>

					<!--begin::Body-->
				</div>

				<!--end::Item-->

				<!--begin::Item-->
				<div class="kt-grid__item">
					<div class="kt-login-v1__footer">
				
						<div class="kt-login-v1__copyright">
							
						</div>
					</div>
				</div>

				<!--end::Item-->
			</div>
		</div>

		<!-- end:: Page -->



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

		<!-- end::Global Config -->

		<!--begin::Global Theme Bundle(used by all pages) -->
		<script src="assets/plugins/global/plugins.bundle.js" type="text/javascript"></script>
		<script src="assets/js/scripts.bundle.js" type="text/javascript"></script>

		<!--end::Global Theme Bundle -->

		<!--begin::Page Scripts(used by this page) -->
		<script src="assets/js/pages/custom/user/login.js" type="text/javascript"></script>


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

           function InvalidP() {

               var clickbutton = document.getElementById('NotieInvalidPword');
               clickbutton.click();

		   }

           function AccessDenied() {

			   //alert('Here');
               var clickbutton = document.getElementById('AccessDeniedNotie');
               clickbutton.click();

           }


           


</script>
		<!--end::Page Scripts -->
                 </form>
	</body>

	<!-- end::Body -->
</html>