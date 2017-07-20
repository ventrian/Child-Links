<%@ Control language="vb" Inherits="DnnForge.ChildLinks.ViewOptions" CodeBehind="ViewOptions.ascx.vb" AutoEventWireup="false" Explicit="True" %>
<%@ Register TagPrefix="dnn" TagName="SectionHead" Src="~/controls/SectionHeadControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<table id="tblViewOptions" cellspacing="0" cellpadding="2" width="525" summary="View Options Design Table"
	border="0" runat="server">
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">
			<dnn:label id="plShowHiddenPages" runat="server" resourcekey="ShowHiddenPages" controlname="chkShowHiddenPages"
				suffix=":"></dnn:label></TD>
		<TD align="left" width="325">
			<asp:checkbox id="chkShowHiddenPages" cssclass="NormalTextBox" runat="server"></asp:checkbox></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">
			<dnn:label id="plShowDisabledPages" runat="server" resourcekey="ShowDisabledPages" controlname="chkShowDisabledPages"
				suffix=":"></dnn:label></TD>
		<TD align="left" width="325">
			<asp:checkbox id="chkShowDisabledPages" cssclass="NormalTextBox" runat="server"></asp:checkbox></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">
			<dnn:label id="plMode" runat="server" resourcekey="Mode" controlname="lstMode" suffix=":"></dnn:label></TD>
		<TD align="left" width="325">
			<asp:RadioButtonList id="lstMode" CssClass="NormalTextBox" Runat="server" AutoPostBack="True"></asp:RadioButtonList>
			<asp:dropdownlist id="drpTab" cssclass="NormalTextBox" runat="server" width="300" datatextfield="TabName"
				datavaluefield="TabId"></asp:dropdownlist></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">
			<dnn:label id="plDisplay" runat="server" resourcekey="Display" controlname="lstDisplay" suffix=":"></dnn:label></TD>
		<TD align="left" width="325">
			<asp:RadioButtonList id="lstDisplay" CssClass="NormalTextBox" Runat="server" AutoPostBack="True"></asp:RadioButtonList></TD>
	</TR>
	<TR vAlign="top" runat="server" id="trHtmlHeader">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">
			<dnn:label id="plHtmlHeader" runat="server" resourcekey="HtmlHeader" controlname="txtHtmlHeader"
				suffix=":"></dnn:label></TD>
		<TD align="left" width="325">
			<asp:textbox id="txtHtmlHeader" cssclass="NormalTextBox" runat="server" width="300" maxlength="50"
				TextMode="MultiLine" Rows="2"></asp:textbox></TD>
	</TR>
	<TR vAlign="top" runat="server" id="trHtmlBody">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">
			<dnn:label id="plHtmlBody" runat="server" resourcekey="HtmlBody" controlname="txtHtmlBody"
				suffix=":"></dnn:label></TD>
		<TD align="left" width="325">
			<asp:textbox id="txtHtmlBody" cssclass="NormalTextBox" runat="server" width="300" maxlength="50"
				TextMode="MultiLine" Rows="6"></asp:textbox></TD>
	</TR>
	<TR vAlign="top" runat="server" id="trHtmlSeparator">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">
			<dnn:label id="plHtmlSeparator" runat="server" resourcekey="HtmlSeparator" controlname="txtHtmlSeparator"
				suffix=":"></dnn:label></TD>
		<TD align="left" width="325">
			<asp:textbox id="txtHtmlSeparator" cssclass="NormalTextBox" runat="server" width="300" maxlength="50"
				TextMode="MultiLine" Rows="2"></asp:textbox></TD>
	</TR>
	<TR vAlign="top" runat="server" id="trHtmlFooter">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">
			<dnn:label id="plHtmlFooter" runat="server" resourcekey="HtmlFooter" controlname="txtHtmlFooter"
				suffix=":"></dnn:label></TD>
		<TD align="left" width="325">
			<asp:textbox id="txtHtmlFooter" cssclass="NormalTextBox" runat="server" width="300" maxlength="50"
				TextMode="MultiLine" Rows="2"></asp:textbox></TD>
	</TR>
	<TR vAlign="top" runat="server" id="trHtmlEmpty">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">
			<dnn:label id="plHtmlEmpty" runat="server" resourcekey="HtmlEmpty" controlname="txtHtmlEmpty"
				suffix=":"></dnn:label></TD>
		<TD align="left" width="325">
			<asp:textbox id="txtHtmlEmpty" cssclass="NormalTextBox" runat="server" width="300" maxlength="50"
				TextMode="MultiLine" Rows="2" /></TD>
	</TR>
</table>
<br />
<dnn:sectionhead id="dshTemplateHelp" cssclass="Head" runat="server" includerule="True" resourcekey="TemplateHelp"
	section="tblTemplateHelp" text="Template Help" isExpanded="false"></dnn:sectionhead>
<TABLE id="tblTemplateHelp" cellSpacing="0" cellPadding="2" width="525" summary="Template Help Design Table"
	border="0" runat="server">
	<TR>
		<TD colSpan="3">
			<asp:label id="lblTemplateHelp" cssclass="Normal" runat="server" resourcekey="TemplateHelpDescription"
				enableviewstate="False"></asp:label></TD>
	</TR>
	<TR>
		<TD colSpan="3">
			<asp:label id="lblHeaderFooter" cssclass="NormalBold" runat="server" resourcekey="HeaderFooter"
				enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">[DESCRIPTION]</TD>
		<TD class="Normal" align="left" width="325">
			<asp:label id="lblParentDescription" cssclass="Normal" runat="server" resourcekey="ParentDescription" enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">[PARENTLINK]</TD>
		<TD class="Normal" align="left" width="325">
			<asp:label id="lblParentLink" cssclass="Normal" runat="server" resourcekey="ParentLink" enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">[PARENTNAME]</TD>
		<TD class="Normal" align="left" width="325">
			<asp:label id="lblParentName" cssclass="Normal" runat="server" resourcekey="ParentName" enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">[GRANDPARENTDESCRIPTION]</TD>
		<TD class="Normal" align="left" width="325">
			<asp:label id="Label1" cssclass="Normal" runat="server" resourcekey="GrandParentDescription" enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">[GRANDPARENTLINK]</TD>
		<TD class="Normal" align="left" width="325">
			<asp:label id="Label2" cssclass="Normal" runat="server" resourcekey="GrandParentLink" enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">[GRANDPARENTNAME]</TD>
		<TD class="Normal" align="left" width="325">
			<asp:label id="Label3" cssclass="Normal" runat="server" resourcekey="GrandParentName" enableviewstate="False"></asp:label></TD>
	</TR>
	<TR>
		<TD colSpan="3">
			<asp:label id="lblBody" cssclass="NormalBold" runat="server" resourcekey="Body" enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">[LINK]</TD>
		<TD class="Normal" align="left" width="325">
			<asp:label id="lblLink" cssclass="Normal" runat="server" resourcekey="Link" enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">[NAME]</TD>
		<TD class="Normal" align="left" width="325">
			<asp:label id="lblName" cssclass="Normal" runat="server" resourcekey="LinkName" enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">[NAME:XXX]</TD>
		<TD class="Normal" align="left" width="325">
			<asp:label id="lblNameXXX" cssclass="Normal" runat="server" resourcekey="LinkNameXXX" enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">[DESCRIPTION]</TD>
		<TD class="Normal" align="left" width="325">
			<asp:label id="lblDescription" cssclass="Normal" runat="server" resourcekey="LinkDescription"
				enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">[DESCRIPTION:XXX]</TD>
		<TD class="Normal" align="left" width="325">
			<asp:label id="lblDescriptionXXX" cssclass="Normal" runat="server" resourcekey="LinkDescriptionXXX"
				enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">[ICON]</TD>
		<TD class="Normal" align="left" width="325">
			<asp:label id="lblIcon" cssclass="Normal" runat="server" resourcekey="Icon" enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">[ITEMINDEX]</TD>
		<TD class="Normal" align="left" width="325">
			<asp:label id="lblItemIndex" cssclass="Normal" runat="server" resourcekey="ItemIndex" enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">[KEYWORDS]</TD>
		<TD class="Normal" align="left" width="325">
			<asp:label id="lblKeywords" cssclass="Normal" runat="server" resourcekey="Keywords" enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">[TABID]</TD>
		<TD class="Normal" align="left" width="325">
			<asp:label id="lblTabID" cssclass="Normal" runat="server" resourcekey="TabID" enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">[TITLE]</TD>
		<TD class="Normal" align="left" width="325">
			<asp:label id="lblTitle" cssclass="Normal" runat="server" resourcekey="Title" enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">[TITLE:XXX]</TD>
		<TD class="Normal" align="left" width="325">
			<asp:label id="lblTitle2" cssclass="Normal" runat="server" resourcekey="TitleXXX" enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">[ISCURRENT][/ISCURRENT]</TD>
		<TD class="Normal" align="left" width="325">
			<asp:label id="lblIsCurrent" cssclass="Normal" runat="server" resourcekey="IsCurrent" enableviewstate="False"></asp:label></TD>
	</TR>
	<TR vAlign="top">
		<TD width="25"></TD>
		<TD class="SubHead" width="150">[ISNOTCURRENT][/ISNOTCURRENT]</TD>
		<TD class="Normal" align="left" width="325">
			<asp:label id="lblIsNotCurrent" cssclass="Normal" runat="server" resourcekey="IsNotCurrent"
				enableviewstate="False"></asp:label></TD>
	</TR>
</TABLE>
