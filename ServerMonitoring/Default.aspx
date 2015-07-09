<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<meta charset="utf-8" />
	<meta http-equiv="X-UA-Compatible" content="IE=edge" />
	<meta name="viewport" content="width=device-width, initial-scale=1" />
	<title>Server Monitoring</title>

	<link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" />
	<link href='http://fonts.googleapis.com/css?family=Lato:300,400,700,300italic,400italic,700italic' rel='stylesheet' type='text/css' />
	<link rel="stylesheet" type="text/css" href="/CSS/bootstrap.min.css" />
	<!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
	<!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
	<!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
      <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
	<link rel="stylesheet" type="text/css" href="/CSS/flat-ui.css" />
	<link rel="stylesheet" type="text/css" href="/CSS/style.css" />

	<!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
	<script src="/js/jquery.min.js"></script>

</head>
<body>
	<form id="form1" runat="server">
		<div class="login">

			<div class="title">
				Tips'n Tricks
			</div>

			<div class="login-screen">

				<div class="login-form">
					<div class="form-group">
						<asp:TextBox runat="server" ID="tbLogin" CssClass="form-control login-field" placeholder="Login"></asp:TextBox>
						<i class="fa fa-user fa-lg login-field-icon"></i>
					</div>

					<div class="form-group">
						<asp:TextBox runat="server" ID="tbPassword" CssClass="form-control login-field" TextMode="Password" placeholder="Mot de passe"></asp:TextBox>
						<i class="fa fa-lock fa-lg login-field-icon"></i>
					</div>

					<asp:Button runat="server" ID="btnConnecter" Text="Se connecter" CssClass="btn btn-primary btn-lg btn-block" />
					<a class="login-link" href="#">Mot de passe perdu ?</a>
				</div>
			</div>
		</div>

		<footer>
			&#169; 2015 So4dev
		</footer>
	</form>
</body>
</html>
