'
' Child Links for DotNetNuke -  http://www.dotnetnuke.com
' Copyright (c) 2002-2005
' by Scott McCulloch ( smcculloch@iinet.net.au ) ( http://www.smcculloch.net )
'

Imports DnnForge.ChildLinks.Entities

Namespace DnnForge.ChildLinks.Common

    Public Class Constants

        Public Const DEFAULT_HIDDEN_PAGES As Boolean = False
        Public Const DEFAULT_DISABLED_PAGES As Boolean = False
        Public Const DEFAULT_MODE As ModeType = ModeType.Children
        Public Const DEFAULT_DISPLAY As DisplayType = DisplayType.Template
        Public Const DEFAULT_HTML_HEADER As String = ""
        Public Const DEFAULT_HTML_BODY As String = "" _
                & "<a href=""[LINK]"" title=""[DESCRIPTION]"">[NAME]</a><br>"
        Public Const DEFAULT_HTML_SEPARATOR As String = ""
        Public Const DEFAULT_HTML_FOOTER As String = ""
        Public Const DEFAULT_HTML_EMPTY As String = "<div class=""Normal"">No child links exist.</div>"

        Public Const SETTING_HIDDEN_PAGES As String = "HiddenPages"
        Public Const SETTING_DISABLED_PAGES As String = "DisabledPages"
        Public Const SETTING_MODE As String = "ListingMode"
        Public Const SETTING_DISPLAY As String = "DisplayMode"
        Public Const SETTING_ANOTHER_PAGE As String = "AnotherPage"
        Public Const SETTING_HTML_HEADER As String = "HtmlHeader"
        Public Const SETTING_HTML_BODY As String = "HtmlBody"
        Public Const SETTING_HTML_SEPARATOR As String = "HtmlSeparator"
        Public Const SETTING_HTML_FOOTER As String = "HtmlFooter"
        Public Const SETTING_HTML_EMPTY As String = "HtmlEmpty"

    End Class

End Namespace
