'
' Child Links for DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2005
' by Scott McCulloch ( smcculloch@iinet.net.au ) ( http://www.smcculloch.net )
'

Imports System.Collections

Imports DnnForge.ChildLinks.Common

Namespace DnnForge.ChildLinks.Entities

    Public Class SettingController

#Region " Private Methods "

        Private Function GetDefaultSetting(ByVal key As String) As String

            Select Case key

                Case Constants.SETTING_HTML_HEADER
                    Return Constants.DEFAULT_HTML_HEADER

                Case Constants.SETTING_HTML_BODY
                    Return Constants.DEFAULT_HTML_BODY

                Case Constants.SETTING_HTML_FOOTER
                    Return Constants.DEFAULT_HTML_FOOTER

                Case Constants.SETTING_HTML_EMPTY
                    Return Constants.DEFAULT_HTML_EMPTY

                Case Constants.SETTING_ANOTHER_PAGE
                    Return "-1"

                Case Else
                    Return ""

            End Select

        End Function

#End Region

#Region " Public Methods "

        Public Function GetSetting(ByVal key As String, ByVal settings As Hashtable) As String

            If (settings.Contains(key)) Then
                Return settings(key).ToString()
            Else
                Return GetDefaultSetting(key)
            End If

        End Function

        Public Shared Function ShowHiddenPages(ByVal settings As Hashtable) As Boolean

            If (settings.Contains(Constants.SETTING_HIDDEN_PAGES)) Then
                Return Convert.ToBoolean(settings(Constants.SETTING_HIDDEN_PAGES))
            Else
                Return Constants.DEFAULT_HIDDEN_PAGES
            End If

        End Function

        Public Shared Function ShowDisabledPages(ByVal settings As Hashtable) As Boolean

            If (settings.Contains(Constants.SETTING_DISABLED_PAGES)) Then
                Return Convert.ToBoolean(settings(Constants.SETTING_DISABLED_PAGES))
            Else
                Return Constants.DEFAULT_DISABLED_PAGES
            End If

        End Function

        Public Shared Function Mode(ByVal settings As Hashtable) As ModeType

            If (settings.Contains(Constants.SETTING_MODE)) Then
                Return CType(System.Enum.Parse(GetType(ModeType), settings(Constants.SETTING_MODE).ToString()), ModeType)
            Else
                Return Constants.DEFAULT_MODE
            End If

        End Function

        Public Shared Function Display(ByVal settings As Hashtable) As DisplayType

            If (settings.Contains(Constants.SETTING_DISPLAY)) Then
                Return CType(System.Enum.Parse(GetType(DisplayType), settings(Constants.SETTING_DISPLAY).ToString()), DisplayType)
            Else
                Return Constants.DEFAULT_DISPLAY
            End If

        End Function

#End Region

    End Class

End Namespace
