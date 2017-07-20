<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ChildLinks.ascx.vb" Inherits="DnnForge.ChildLinks.ChildLinks" %>
<asp:Repeater ID="rptChildLinks" Runat="server" EnableViewState="False">
	<HeaderTemplate />
	<ItemTemplate />
	<SeparatorTemplate />
	<FooterTemplate />
</asp:Repeater>
<asp:DropDownList ID="ddlChildLinks" runat="server" CssClass="NormalTextBox" DataValueField="TabID" DataTextField="TabName" />
<asp:LinkButton ID="cmdGo" Runat="server" CssClass="CommandButton" ResourceKey="Go" />
<asp:Literal ID="litEmpty" runat="server" EnableViewState="false" />