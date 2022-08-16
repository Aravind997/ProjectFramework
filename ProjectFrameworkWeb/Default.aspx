<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProjectFrameworkWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1><asp:Label ID="LabelMainHeading" runat="server" Text="ASP.NET"></asp:Label> </h1>
        <p class="lead"><asp:Label ID="LabelMainDesc" runat="server" Text="ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript."></asp:Label></p>
        <p><a href="http://www.ktsinfotech.com" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>
    
    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                This <span style="color: #000099"><strong>ASP.NET Web Project Framework</strong></span> lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="http://training.ktsinfotech.com">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Database Support</h2>
            <p>
                Database Support is Given For OLD DB Database as well as ODBC Based Databases . 
                Default Database script in App_Data Folder for MS Access and MySQL. <span style="color: #0000FF">Default Connection String in Web.config is for MS Access.The site default user name / password is admin / admin </span>
            </p> 
            <p>
                <a class="btn btn-default" href="http://training.ktsinfotech.com">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications. <span style="color: #0000CC">Some of the Hosting providers for which this Site Framework tested includes Shared Hosting Servers like Godaddy , Hostgator and Dedicated Servers like Amazon Lightsail etc. </span>
            </p>
            <p>
                <a class="btn btn-default" href="http://training.ktsinfotech.com">Learn more &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
