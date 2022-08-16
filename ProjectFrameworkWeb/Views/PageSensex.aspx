<%@ Page Title="Settings Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PageSensex.aspx.cs" Inherits="ProjectFrameworkWeb.Views.PageSensex" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <div class="panel panel-primary">
      <div class="panel-heading"><h4>Configure Site Settings</h4></div>
      <div class="panel-body">

          <div class="form-group">
            <label for="TextBoxFolioName" style="width: 101px">Folio Name</label>
                <asp:TextBox ID="TextBoxFolioName" class="form-control"  style="min-width: 100%;" runat="server" Width="80%"></asp:TextBox>
          </div>
          <div class="form-group">
            <label for="TextBoxFolioAmount" style="width: 102px">Folio Amount</label>
                <asp:TextBox ID="TextBoxFolioAmount" class="form-control" style="min-width: 100%;" runat="server" Width="80%"></asp:TextBox>
          </div>
          <div class="form-group">
            <label for="TextBoxYear">Year of Purchase</label>
                <asp:TextBox ID="TextBoxYear" class="form-control" style="min-width: 100%;" runat="server" TextMode="MultiLine" Width="80%"></asp:TextBox>
          </div>
          <div class="form-group">
              <asp:CheckBox ID="CheckEnableAPIAuthen" runat="server" Text="Enable Authentication Check for Mobile API Calls" />
          </div>
          <div class="form-group">
              <asp:Button ID="ButtonUpdatePortfolio" runat="server" Text="Update" class="btn btn-primary" OnClick="UpdateFolio_Click"/>
              
          </div> 
          <div class="form-group">
              <asp:Label ID="LabelDone" runat="server" Text="" ForeColor="Green" Font-Bold="true"></asp:Label>
          </div>

      </div>
    </div>
</asp:Content>
