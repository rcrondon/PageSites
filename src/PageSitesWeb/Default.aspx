<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PageSitesWeb._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div id="conseguir_datos" runat="server">
        <br>
        <br>
        <asp:TextBox id="txt_listado" TextMode="multiline" runat="server" />
        <br>
        <br>
        <asp:Button ID="btn_cargar" runat="server" Text="Conseguir" />
        <asp:Button ID="btn_excel" runat="server" Text="Generar Excel" OnClick="btn_excel_Click" />
    </div>

     <div id="mostrar_datos" runat="server"><%=html_body%></div>

</asp:Content>
