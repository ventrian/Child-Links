'
' Child Links for DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2005
' by Scott McCulloch ( smcculloch@iinet.net.au ) ( http://www.smcculloch.net )
'

Imports System.Web.UI.WebControls

Imports DotNetNuke.Common
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Tabs
Imports DotNetNuke.Security
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports DnnForge.ChildLinks.Common
Imports DnnForge.ChildLinks.Entities

Namespace DnnForge.ChildLinks

    Public MustInherit Class ViewOptions
        Inherits ModuleSettingsBase

#Region " Controls "

        Protected WithEvents lblViewOptionsHelp As System.Web.UI.WebControls.Label
        Protected WithEvents txtHtmlHeader As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtHtmlBody As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtHtmlFooter As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtHtmlEmpty As System.Web.UI.WebControls.TextBox
        Protected WithEvents pnlViewOptions As System.Web.UI.WebControls.Panel
        Protected WithEvents cmdUpdate As System.Web.UI.WebControls.LinkButton
        Protected WithEvents tblViewOptions As System.Web.UI.HtmlControls.HtmlTable
        Protected WithEvents tblTemplateHelp As System.Web.UI.HtmlControls.HtmlTable
        Protected WithEvents lblTemplateHelp As System.Web.UI.WebControls.Label
        Protected WithEvents lblLink As System.Web.UI.WebControls.Label
        Protected WithEvents lblName As System.Web.UI.WebControls.Label
        Protected WithEvents lblDescription As System.Web.UI.WebControls.Label
        Protected WithEvents cmdCancel As System.Web.UI.WebControls.LinkButton
        Protected WithEvents txtHtmlSeparator As System.Web.UI.WebControls.TextBox
        Protected WithEvents lstMode As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents drpTab As System.Web.UI.WebControls.DropDownList
        Protected WithEvents chkShowDisabledPages As System.Web.UI.WebControls.CheckBox
        Protected WithEvents lblHeaderFooter As System.Web.UI.WebControls.Label
        Protected WithEvents lblParentLink As System.Web.UI.WebControls.Label
        Protected WithEvents lblParentName As System.Web.UI.WebControls.Label
        Protected WithEvents lblBody As System.Web.UI.WebControls.Label
        Protected WithEvents lblIcon As System.Web.UI.WebControls.Label
        Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
        Protected WithEvents lblTabID As System.Web.UI.WebControls.Label
        Protected WithEvents lblTitle2 As System.Web.UI.WebControls.Label
        Protected WithEvents lblIsCurrent As System.Web.UI.WebControls.Label
        Protected WithEvents lblIsNotCurrent As System.Web.UI.WebControls.Label
        Protected WithEvents lstDisplay As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents lblNameXXX As System.Web.UI.WebControls.Label
        Protected WithEvents lblDescriptionXXX As System.Web.UI.WebControls.Label
        Protected WithEvents trHtmlHeader As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents trHtmlBody As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents trHtmlSeparator As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents trHtmlFooter As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents trHtmlEmpty As System.Web.UI.HtmlControls.HtmlTableRow
        Protected WithEvents chkShowHiddenPages As System.Web.UI.WebControls.CheckBox

#End Region

#Region " Private Methods "

        Private Sub BindDisplay()

            For Each value As Integer In System.Enum.GetValues(GetType(DisplayType))
                Dim li As New ListItem
                li.Value = value.ToString
                li.Text = Localization.GetString(System.Enum.GetName(GetType(DisplayType), value), Me.LocalResourceFile)
                lstDisplay.Items.Add(li)
            Next

        End Sub

        Private Sub BindMode()

            For Each value As Integer In System.Enum.GetValues(GetType(ModeType))
                Dim li As New ListItem
                li.Value = value.ToString
                li.Text = Localization.GetString(System.Enum.GetName(GetType(ModeType), value), Me.LocalResourceFile)
                lstMode.Items.Add(li)
            Next

        End Sub

        Private Sub BindTabs()

            drpTab.DataSource = TabController.GetPortalTabs(PortalId, -1, True, "None", True, False, True, True, False)
            'drpTab.DataSource = GetPortalTabs(PortalSettings.DesktopTabs, -1, True, True, False, True, True)
            drpTab.DataBind()

        End Sub

        Private Sub BindSettings()

            Dim objSettingController As New SettingController

            chkShowHiddenPages.Checked = Convert.ToBoolean(SettingController.ShowHiddenPages(Me.Settings))
            chkShowDisabledPages.Checked = Convert.ToBoolean(SettingController.ShowDisabledPages(Me.Settings))
            lstMode.SelectedValue = SettingController.Mode(Me.Settings)

            If (lstMode.SelectedValue = "2") Then
                drpTab.Visible = True
            Else
                drpTab.Visible = False
            End If

            lstDisplay.SelectedValue = SettingController.Display(Me.Settings)

            If (lstDisplay.SelectedValue = "1") Then
                trHtmlBody.Visible = False
                trHtmlFooter.Visible = False
                trHtmlHeader.Visible = False
                trHtmlSeparator.Visible = False
                trHtmlEmpty.Visible = False
            Else
                trHtmlBody.Visible = True
                trHtmlFooter.Visible = True
                trHtmlHeader.Visible = True
                trHtmlSeparator.Visible = True
                trHtmlEmpty.Visible = True
            End If

            txtHtmlHeader.Text = objSettingController.GetSetting(Constants.SETTING_HTML_HEADER, Me.Settings)
            txtHtmlBody.Text = objSettingController.GetSetting(Constants.SETTING_HTML_BODY, Me.Settings)
            txtHtmlSeparator.Text = objSettingController.GetSetting(Constants.SETTING_HTML_SEPARATOR, Me.Settings)
            txtHtmlFooter.Text = objSettingController.GetSetting(Constants.SETTING_HTML_FOOTER, Me.Settings)
            txtHtmlEmpty.Text = objSettingController.GetSetting(Constants.SETTING_HTML_EMPTY, Me.Settings)

            If Not (drpTab.Items.FindByValue(objSettingController.GetSetting(Constants.SETTING_ANOTHER_PAGE, Me.Settings)) Is Nothing) Then
                drpTab.SelectedValue = objSettingController.GetSetting(Constants.SETTING_ANOTHER_PAGE, Me.Settings)
            End If

        End Sub

        Private Sub SaveSettings()

            Dim objModuleController As New ModuleController

            objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.SETTING_HIDDEN_PAGES, chkShowHiddenPages.Checked)
            objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.SETTING_DISABLED_PAGES, chkShowDisabledPages.Checked)
            objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.SETTING_MODE, lstMode.SelectedValue)
            objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.SETTING_DISPLAY, lstDisplay.SelectedValue)
            objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.SETTING_ANOTHER_PAGE, drpTab.SelectedValue)
            objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.SETTING_HTML_HEADER, txtHtmlHeader.Text)
            objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.SETTING_HTML_BODY, txtHtmlBody.Text)
            objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.SETTING_HTML_SEPARATOR, txtHtmlSeparator.Text)
            objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.SETTING_HTML_FOOTER, txtHtmlFooter.Text)
            objModuleController.UpdateModuleSetting(Me.ModuleId, Constants.SETTING_HTML_EMPTY, txtHtmlEmpty.Text)

        End Sub

#End Region

#Region " Event Handlers "

        Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click

            Try

                Response.Redirect(NavigateURL(), True)

            Catch exc As Exception 'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub lstMode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstMode.SelectedIndexChanged

            Try

                If (lstMode.SelectedValue = "2") Then
                    drpTab.Visible = True
                Else
                    drpTab.Visible = False
                End If

            Catch exc As Exception 'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub lstDisplay_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstDisplay.SelectedIndexChanged

            Try

                If (lstDisplay.SelectedValue = "1" Or lstDisplay.SelectedValue = "2") Then
                    trHtmlBody.Visible = False
                    trHtmlFooter.Visible = False
                    trHtmlHeader.Visible = False
                    trHtmlSeparator.Visible = False
                    trHtmlEmpty.Visible = False
                Else
                    trHtmlBody.Visible = True
                    trHtmlFooter.Visible = True
                    trHtmlHeader.Visible = True
                    trHtmlSeparator.Visible = True
                    trHtmlEmpty.Visible = True
                End If

            Catch exc As Exception 'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

#End Region


#Region " Base Method Implementations "

        Public Overrides Sub LoadSettings()

            Try

                If (Page.IsPostBack = False) Then

                    BindMode()
                    BindDisplay()
                    BindTabs()
                    BindSettings()

                End If

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Public Overrides Sub UpdateSettings()

            Try

                If (Page.IsValid) Then

                    SaveSettings()

                End If

            Catch exc As Exception    'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()

        End Sub

#End Region

    End Class

End Namespace