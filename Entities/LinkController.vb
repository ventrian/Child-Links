Imports System.Xml

Imports DotNetNuke.Common
Imports DotNetNuke.Entities.Modules

Namespace DnnForge.ChildLinks.Entities

    Public Class LinkController
        Implements IPortable

#Region " Optional Interfaces "

        Public Function ExportModule(ByVal ModuleID As Integer) As String Implements DotNetNuke.Entities.Modules.IPortable.ExportModule

            Dim strXML As String = ""

            Dim objModuleController As New ModuleController
            Dim objSettings As Hashtable = objModuleController.GetModuleSettings(ModuleID)

            strXML += "<settings>"
            Dim settings As IDictionaryEnumerator = objSettings.GetEnumerator
            While settings.MoveNext
                strXML += "<setting>"
                strXML += "<name>" & DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(settings.Key.ToString()) & "</name>"
                strXML += "<value>" & DotNetNuke.Common.Utilities.XmlUtils.XMLEncode(settings.Value.ToString()) & "</value>"
                strXML += "</setting>"
            End While
            strXML += "</settings>"

            Return strXML

        End Function

        Public Sub ImportModule(ByVal ModuleID As Integer, ByVal Content As String, ByVal Version As String, ByVal UserID As Integer) Implements DotNetNuke.Entities.Modules.IPortable.ImportModule

            Dim objModuleController As New ModuleController

            Dim xmlSettings As XmlNode = GetContent(Content, "settings")
            For Each xmlSetting As XmlNode In xmlSettings
                objModuleController.UpdateModuleSetting(ModuleID, xmlSetting.Item("name").InnerText, xmlSetting.Item("value").InnerText)
            Next

        End Sub

#End Region

    End Class

End Namespace

