<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProjectFrameworkWeb.Login" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container center_div">
  <h2>Login to Site</h2>
  
    <div class="form-group">
      <label for="email">User Name or EMail:</label>
        <asp:TextBox ID="TextBoxUserName" class="form-control" runat="server"></asp:TextBox>
      
    </div>
    <div class="form-group">
      <label for="pwd">Password:</label>
         <asp:TextBox ID="TextBoxPassword" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
    </div>
        <asp:Button ID="ButtonSubmit" class="btn btn-primary" runat="server" Text="Submit" OnClick="ButtonSubmit_Click" /><br/><br/>
        <asp:Label ID="LabelStatusMessage" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
</div>
    

</asp:Content>
