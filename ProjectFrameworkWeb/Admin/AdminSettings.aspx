<%@ Page Title="Settings Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminSettings.aspx.cs" Inherits="ProjectFrameworkWeb.AdminPages.AdminSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <div class="panel panel-primary">
      <div class="panel-heading"><h4>Configure Site Settings</h4></div>
      <div class="panel-body">

          <div class="form-group">
            <label for="TextBoxAppName">Application Name</label>
                <asp:TextBox ID="TextBoxAppName" class="form-control"  style="min-width: 100%;" runat="server" Width="80%"></asp:TextBox>
          </div>
          <div class="form-group">
            <label for="TextBoxMainHeading">Main Heading</label>
                <asp:TextBox ID="TextBoxMainHeading" class="form-control" style="min-width: 100%;" runat="server" Width="80%"></asp:TextBox>
          </div>
          <div class="form-group">
            <label for="TextBoxMainDesc">Main Description</label>
                <asp:TextBox ID="TextBoxMainDesc" class="form-control" style="min-width: 100%;" runat="server" TextMode="MultiLine" Width="80%"></asp:TextBox>
          </div>
          <div class="form-group">
              <asp:CheckBox ID="CheckEnableAPIAuth" runat="server" Text="Enable Authentication Check for Mobile API Calls" />
          </div>
          <div class="form-group">
              <asp:Button ID="ButtonUpdate" runat="server" Text="Update" class="btn btn-primary" OnClick="ButtonUpdate_Click" />
              
          </div>
          <div class="form-group">
              <asp:Label ID="LabelStatus" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
          </div>
          <div class="form-group">
              <asp:Button ID="ViewSensex" runat="server" Text="View Sensex"  class="btn btn-primary" OnClick="ViewSensex_Click"   />
          </div>

      </div>
    </div>
</asp:Content>
