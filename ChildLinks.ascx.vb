'
' Child Links for DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2005
' by Scott McCulloch ( smcculloch@iinet.net.au ) ( http://www.smcculloch.net )
'

Imports System.Web.UI
Imports System.Web.UI.WebControls

Imports DotNetNuke
Imports DotNetNuke.Common
Imports DotNetNuke.Common.Utilities
Imports DotNetNuke.Entities.Modules
Imports DotNetNuke.Entities.Modules.Actions
Imports DotNetNuke.Entities.Tabs
Imports DotNetNuke.Security
Imports DotNetNuke.Services.Exceptions
Imports DotNetNuke.Services.Localization

Imports DnnForge.ChildLinks.Common
Imports DnnForge.ChildLinks.Entities
Imports DotNetNuke.Application
Imports DotNetNuke.Security.Permissions

Namespace DnnForge.ChildLinks

    Public MustInherit Class ChildLinks

        Inherits PortalModuleBase

#Region " Controls "

        Protected WithEvents rptChildLinks As System.Web.UI.WebControls.Repeater
        Protected WithEvents litEmpty As System.Web.UI.WebControls.Literal

#End Region

#Region " Private Members "

        Private _template As String
        Protected WithEvents ddlChildLinks As System.Web.UI.WebControls.DropDownList
        Protected WithEvents cmdGo As System.Web.UI.WebControls.LinkButton
        Private _tokens As String()

#End Region

#Region " Private Methods "

        Private Sub BindDropDown()

            ddlChildLinks.DataSource = GetTabs()
            ddlChildLinks.DataBind()

            If (ddlChildLinks.Items.Count = 0) Then

                Dim objController As New SettingController
                Dim item As String = objController.GetSetting(Constants.SETTING_HTML_EMPTY, Settings)
                litEmpty.Text = item
                ddlChildLinks.Visible = False
                cmdGo.Visible = False
                rptChildLinks.Visible = False

            Else

                If (SettingController.Display(Me.Settings) = DisplayType.DropdownAlphabetical) Then
                    SortDropDown(ddlChildLinks)
                End If

                ddlChildLinks.Items.Insert(0, New ListItem(Localization.GetString("SelectPage", Me.LocalResourceFile), "-1"))
                ddlChildLinks.Visible = True
                cmdGo.Visible = True
                rptChildLinks.Visible = False

            End If

        End Sub

        Private Sub BindTemplate()

            rptChildLinks.DataSource = GetTabs()
            rptChildLinks.DataBind()

            rptChildLinks.Visible = True
            ddlChildLinks.Visible = False
            cmdGo.Visible = False

            If (rptChildLinks.Items.Count = 0) Then

                Dim objController As New SettingController
                Dim item As String = objController.GetSetting(Constants.SETTING_HTML_EMPTY, Settings)
                litEmpty.Text = item

                rptChildLinks.Visible = False

            End If

        End Sub

        Private Function GetParentID() As Integer

            Dim objSettingController As New SettingController
            Dim objMode As ModeType = SettingController.Mode(Me.Settings)

            Select Case objMode

                Case ModeType.Children
                    Return Me.TabId

                Case ModeType.Siblings
                    Return Me.PortalSettings.ActiveTab.ParentId

                Case ModeType.Another
                    Return Convert.ToInt32(objSettingController.GetSetting(Constants.SETTING_ANOTHER_PAGE, Me.Settings))

                Case ModeType.TopLevel
                    Dim objTabController As New TabController
                    Dim objTab As TabInfo = Me.PortalSettings.ActiveTab

                    While objTab.ParentId <> -1
                        objTab = objTabController.GetTab(objTab.ParentId, PortalId, False)
                    End While

                    Return objTab.TabID

                Case Else
                    Return -1

            End Select

        End Function


        Private Function GetTabs() As ArrayList

            Dim parentID = GetParentID()

            Dim objTabsFiltered As New ArrayList
            If (parentID <> Null.NullInteger) Then

                Dim objTabs As List(Of TabInfo) = TabController.GetTabsByParent(parentID, PortalId)

                For Each objTab As TabInfo In objTabs
                    If ((objTab.IsVisible Or SettingController.ShowHiddenPages(Me.Settings)) And ((Not objTab.DisableLink) Or SettingController.ShowDisabledPages(Me.Settings)) And Not objTab.IsDeleted) Then
                        If ((objTab.StartDate < Now Or objTab.StartDate = Null.NullDate) And (objTab.EndDate > Now Or objTab.EndDate = Null.NullDate)) Or AdminMode = True Then
                            If TabPermissionController.CanViewPage(objTab) = True Then
                                objTabsFiltered.Add(objTab)
                            End If
                        End If
                    End If
                Next


            Else

                Dim objTabs As TabCollection = TabController.Instance.GetTabsByPortal(Me.PortalId)

                For Each objTab As TabInfo In objTabs.Values
                    If (objTab.ParentId = -1) Then
                        If ((objTab.IsVisible Or SettingController.ShowHiddenPages(Me.Settings)) And ((Not objTab.DisableLink) Or SettingController.ShowDisabledPages(Me.Settings)) And Not objTab.IsDeleted) Then
                            If ((objTab.StartDate < Now Or objTab.StartDate = Null.NullDate) And (objTab.EndDate > Now Or objTab.EndDate = Null.NullDate)) Or AdminMode = True Then
                                If TabPermissionController.CanViewPage(objTab) = True Then
                                    objTabsFiltered.Add(objTab)
                                End If
                            End If
                        End If
                    End If
                Next

            End If

            Return objTabsFiltered


        End Function

        Private Sub InitTemplate()

            Dim objController As New SettingController
            Dim item As String = objController.GetSetting(Constants.SETTING_HTML_BODY, Settings)

            Dim delimStr As String = "[]"
            Dim delimiter As Char() = delimStr.ToCharArray()
            _tokens = item.Split(delimiter)

        End Sub

        Private Function ProcessHeader() As String

            Dim objController As New SettingController

            Dim header As String = objController.GetSetting(Constants.SETTING_HTML_HEADER, Settings)

            If (header.IndexOf("[PARENTLINK]") <> -1 Or header.IndexOf("[PARENTNAME]") <> -1 Or header.IndexOf("[DESCRIPTION]") <> -1 Or header.IndexOf("[GRANDPARENTLINK]") <> -1 Or header.IndexOf("[GRANDPARENTNAME]") <> -1 Or header.IndexOf("[GRANDPARENTDESCRIPTION]") <> -1) Then

                Dim objTabController As New TabController
                Dim objParent As TabInfo = objTabController.GetTab(GetParentID(), PortalId, False)

                If Not (objParent Is Nothing) Then
                    header = header.Replace("[PARENTLINK]", objParent.FullUrl)
                    header = header.Replace("[PARENTNAME]", objParent.TabName)
                    header = header.Replace("[DESCRIPTION]", objParent.Description)

                    Dim objGrandParent As TabInfo = objTabController.GetTab(objParent.ParentId, PortalId, False)

                    If Not (objGrandParent Is Nothing) Then
                        header = header.Replace("[GRANDPARENTLINK]", objGrandParent.FullUrl)
                        header = header.Replace("[GRANDPARENTNAME]", objGrandParent.TabName)
                        header = header.Replace("[GRANDPARENTDESCRIPTION]", objGrandParent.Description)
                    End If
                End If

            End If

            Return header

        End Function

        Public Sub ProcessItem(ByRef objPlaceHolder As ControlCollection, ByVal objTab As TabInfo, ByVal itemIndex As Integer)

            For iPtr As Integer = 0 To _tokens.Length - 1 Step 2

                objPlaceHolder.Add(New LiteralControl(_tokens(iPtr).ToString()))

                If iPtr < _tokens.Length - 1 Then
                    Select Case _tokens(iPtr + 1)

                        Case "TABID"
                            Dim objLiteral As New Literal
                            objLiteral.ID = Globals.CreateValidID(Me.TabModuleId.ToString() & "-" & objTab.TabID.ToString() & "-" & iPtr.ToString())
                            objLiteral.Text = objTab.TabID.ToString()
                            objPlaceHolder.Add(objLiteral)

                        Case "ISCURRENT"
                            If (objTab.TabID <> Me.TabId) Then
                                While (iPtr < _tokens.Length - 1)
                                    If (_tokens(iPtr + 1) = "/ISCURRENT") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "/ISCURRENT"
                            ' Do Nothing

                        Case "ISNOTCURRENT"
                            If (objTab.TabID = Me.TabId) Then
                                While (iPtr < _tokens.Length - 1)
                                    If (_tokens(iPtr + 1) = "/ISNOTCURRENT") Then
                                        Exit While
                                    End If
                                    iPtr = iPtr + 1
                                End While
                            End If

                        Case "/ISNOTCURRENT"
                            ' Do Nothing

                        Case "ITEMINDEX"
                            Dim objLiteral As New Literal
                            objLiteral.ID = Globals.CreateValidID(Me.TabModuleId.ToString() & "-" & objTab.TabID.ToString() & "-" & iPtr.ToString())
                            objLiteral.Text = (itemIndex + 1).ToString()
                            objPlaceHolder.Add(objLiteral)

                        Case "LINK"
                            Dim objLiteral As New Literal
                            objLiteral.ID = Globals.CreateValidID(Me.TabModuleId.ToString() & "-" & objTab.TabID.ToString() & "-" & iPtr.ToString())
                            objLiteral.Text = objTab.FullUrl
                            objPlaceHolder.Add(objLiteral)

                        Case "NAME"
                            Dim objLiteral As New Literal
                            objLiteral.ID = Globals.CreateValidID(Me.TabModuleId.ToString() & "-" & objTab.TabID.ToString() & "-" & iPtr.ToString())
                            objLiteral.Text = objTab.TabName
                            objPlaceHolder.Add(objLiteral)

                        Case "TITLE"
                            Dim objLiteral As New Literal
                            objLiteral.ID = Globals.CreateValidID(Me.TabModuleId.ToString() & "-" & objTab.TabID.ToString() & "-" & iPtr.ToString())
                            objLiteral.Text = objTab.Title
                            objPlaceHolder.Add(objLiteral)

                        Case "DESCRIPTION"
                            Dim objLiteral As New Literal
                            objLiteral.ID = Globals.CreateValidID(Me.TabModuleId.ToString() & "-" & objTab.TabID.ToString() & "-" & iPtr.ToString())
                            objLiteral.Text = objTab.Description.Replace("""", "")
                            objPlaceHolder.Add(objLiteral)

                        Case "KEYWORDS"
                            Dim objLiteral As New Literal
                            objLiteral.ID = Globals.CreateValidID(Me.TabModuleId.ToString() & "-" & objTab.TabID.ToString() & "-" & iPtr.ToString())
                            objLiteral.Text = objTab.KeyWords.Replace("""", "")
                            objPlaceHolder.Add(objLiteral)

                        Case "ICON"
                            If (objTab.IconFile <> "") Then
                                Dim icon As String = ""
                                If (objTab.IconFile.StartsWith("~")) Then
                                    icon = "<img src=""" & Page.ResolveUrl(objTab.IconFile()) & """ alt=""" & Localization.GetString("PageIcon", Me.LocalResourceFile) & """ border=""0"" />"
                                Else
                                    icon = "<img src=""" & objTab.IconFile() & """ alt=""" & Localization.GetString("PageIcon", Me.LocalResourceFile) & """ border=""0"" />"
                                End If

                                Dim objLiteral As New Literal
                                objLiteral.ID = Globals.CreateValidID(Me.TabModuleId.ToString() & "-" & objTab.TabID.ToString() & "-" & iPtr.ToString())
                                objLiteral.Text = icon
                                objPlaceHolder.Add(objLiteral)
                            End If

                        Case Else

                            If (_tokens(iPtr + 1).ToUpper().StartsWith("NAME:")) Then
                                Dim field As String = _tokens(iPtr + 1).Substring(5, _tokens(iPtr + 1).Length - 5)

                                If (field <> "") Then
                                    Dim objLiteral As New Literal
                                    objLiteral.ID = Globals.CreateValidID(Me.TabModuleId.ToString() & "-" & objTab.TabID.ToString() & "-" & iPtr.ToString())
                                    If (objTab.TabName.Length > Convert.ToInt32(field)) Then
                                        objLiteral.Text = objTab.TabName.Substring(0, Convert.ToInt32(field))
                                    Else
                                        objLiteral.Text = objTab.TabName
                                    End If
                                    objLiteral.EnableViewState = False
                                    objPlaceHolder.Add(objLiteral)
                                End If

                            End If

                            If (_tokens(iPtr + 1).ToUpper().StartsWith("TITLE:")) Then
                                Dim field As String = _tokens(iPtr + 1).Substring(6, _tokens(iPtr + 1).Length - 6)

                                If (field <> "") Then
                                    Try
                                        Dim objLiteral As New Literal
                                        objLiteral.ID = Globals.CreateValidID(Me.TabModuleId.ToString() & "-" & objTab.TabID.ToString() & "-" & iPtr.ToString())
                                        If (objTab.Title.Length > Convert.ToInt32(field)) Then
                                            objLiteral.Text = objTab.Title.Substring(0, Convert.ToInt32(field))
                                        Else
                                            objLiteral.Text = objTab.Title
                                        End If
                                        objLiteral.EnableViewState = False
                                        objPlaceHolder.Add(objLiteral)
                                    Catch
                                    End Try
                                End If

                            End If

                            If (_tokens(iPtr + 1).ToUpper().StartsWith("DESCRIPTION:")) Then
                                Dim field As String = _tokens(iPtr + 1).Substring(12, _tokens(iPtr + 1).Length - 12)

                                If (field <> "") Then
                                    Try
                                        Dim objLiteral As New Literal
                                        objLiteral.ID = Globals.CreateValidID(Me.TabModuleId.ToString() & "-" & objTab.TabID.ToString() & "-" & iPtr.ToString())
                                        If (objTab.Description.Length > Convert.ToInt32(field)) Then
                                            objLiteral.Text = objTab.Description.Substring(0, Convert.ToInt32(field))
                                        Else
                                            objLiteral.Text = objTab.Description
                                        End If
                                        objLiteral.EnableViewState = False
                                        objPlaceHolder.Add(objLiteral)
                                    Catch
                                    End Try
                                End If

                            End If

                    End Select
                End If

            Next

        End Sub

        Private Function ProcessSeparator() As String

            Dim objController As New SettingController

            Dim separator As String = objController.GetSetting(Constants.SETTING_HTML_SEPARATOR, Settings)

            Return separator

        End Function

        Private Function ProcessFooter() As String

            Dim objController As New SettingController

            Dim header As String = objController.GetSetting(Constants.SETTING_HTML_FOOTER, Settings)

            If (header.IndexOf("[PARENTLINK]") <> -1 Or header.IndexOf("[PARENTNAME]") <> -1) Then

                Dim objTabController As New TabController
                Dim objParent As TabInfo = objTabController.GetTab(GetParentID(), PortalId, False)

                If Not (objParent Is Nothing) Then
                    header = header.Replace("[PARENTLINK]", objParent.FullUrl)
                    header = header.Replace("[PARENTNAME]", objParent.TabName)
                End If

            End If

            Return header

        End Function

        Private Sub SortDropDown(ByRef objDDL As DropDownList)
            Dim textList As New ArrayList()
            Dim valueList As New ArrayList()


            For Each li As ListItem In objDDL.Items
                textList.Add(li.Text)
            Next

            textList.Sort()

            For Each item As Object In textList
                Dim value As String = objDDL.Items.FindByText(item.ToString()).Value
                valueList.Add(value)
            Next
            objDDL.Items.Clear()

            For i As Integer = 0 To textList.Count - 1
                Dim objItem As New ListItem(textList(i).ToString(), valueList(i).ToString())
                objDDL.Items.Add(objItem)
            Next
        End Sub

        Private ReadOnly Property AdminMode() As Boolean
            Get
                Return PortalSecurity.IsInRoles(PortalSettings.AdministratorRoleName) Or PortalSecurity.IsInRoles(TabPermissionController.CanAdminPage(PortalSettings.ActiveTab))
            End Get
        End Property

#End Region

#Region " Event Handlers "

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Try

                Dim objSettingController As New SettingController

                If (SettingController.Display(Me.Settings) = DisplayType.Template) Then

                    InitTemplate()
                    BindTemplate()

                Else

                    If (Page.IsPostBack = False) Then
                        BindDropDown()
                    End If

                End If

            Catch exc As Exception 'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub rptChildLinks_ItemDataBound(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptChildLinks.ItemDataBound

            Try

                If (e.Item.ItemType = ListItemType.Header) Then
                    Dim objLiteral As New Literal
                    objLiteral.Text = ProcessHeader()
                    e.Item.Controls.Add(objLiteral)
                End If

                If (e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem) Then
                    Dim objTab As TabInfo = CType(e.Item.DataItem, TabInfo)
                    Me.ProcessItem(e.Item.Controls, objTab, e.Item.ItemIndex)
                End If

                If (e.Item.ItemType = ListItemType.Separator) Then
                    Dim objLiteral As New Literal
                    objLiteral.Text = ProcessSeparator()
                    e.Item.Controls.Add(objLiteral)
                End If

                If (e.Item.ItemType = ListItemType.Footer) Then
                    Dim objLiteral As New Literal
                    objLiteral.Text = ProcessFooter()
                    e.Item.Controls.Add(objLiteral)
                End If

            Catch exc As Exception 'Module failed to load
                ProcessModuleLoadException(Me, exc)
            End Try

        End Sub

        Private Sub cmdGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdGo.Click

            Try

                If (ddlChildLinks.SelectedValue <> "-1") Then

                    Dim objTabController As New TabController
                    Dim objTab As TabInfo = objTabController.GetTab(Convert.ToInt32(ddlChildLinks.SelectedValue), PortalId, False)

                    If Not (objTab Is Nothing) Then
                        Response.Redirect(objTab.FullUrl, True)
                    End If

                End If

            Catch exc As Exception 'Module failed to load
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