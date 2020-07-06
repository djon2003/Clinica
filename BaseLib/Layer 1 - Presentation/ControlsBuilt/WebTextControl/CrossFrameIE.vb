' FREE code from CODECENTRIX
' http://www.codecentrix.com/
' http://codecentrix.blogspot.com/
Imports System.Runtime.InteropServices
Imports mshtml


Public Class CrossFrameIE
    ' Returns null in case of failure.
    Public Shared Function GetDocumentFromWindow(ByVal htmlWindow As IHTMLWindow2) As IHTMLDocument2
        If htmlWindow Is Nothing Then
            Return Nothing
        End If

        ' First try the usual way to get the document.
        Try
            Dim doc As IHTMLDocument2 = htmlWindow.document
            Return doc
        Catch comEx As COMException
            ' I think COMException won't be ever fired but just to be sure ...
            If comEx.ErrorCode <> E_ACCESSDENIED Then
                Return Nothing
            End If
        Catch generatedExceptionName As System.UnauthorizedAccessException
        Catch
            ' Any other error.
            Return Nothing
        End Try

        ' At this point the error was E_ACCESSDENIED because the frame contains a document from another domain.
        ' IE tries to prevent a cross frame scripting security issue.
        Try
            ' Convert IHTMLWindow2 to IWebBrowser2 using IServiceProvider.
            Dim sp As IServiceProvider = DirectCast(htmlWindow, IServiceProvider)

            ' Use IServiceProvider.QueryService to get IWebBrowser2 object.
            Dim brws As [Object] = Nothing
            sp.QueryService(IID_IWebBrowserApp, IID_IWebBrowser2, brws)

            ' Get the document from IWebBrowser2.
            Dim browser As SHDocVw.IWebBrowser2 = DirectCast(brws, SHDocVw.IWebBrowser2)

            Return DirectCast(browser.Document, IHTMLDocument2)
        Catch
        End Try

        Return Nothing
    End Function

    Private Const E_ACCESSDENIED As Integer = CInt(&H80070005)
    Private Shared IID_IWebBrowserApp As New Guid("0002DF05-0000-0000-C000-000000000046")
    Private Shared IID_IWebBrowser2 As New Guid("D30C1661-CDAF-11D0-8A3E-00C04FC9E26E")
End Class

' This is the COM IServiceProvider interface, not System.IServiceProvider .Net interface!
<ComImport(), ComVisible(True), Guid("6D5140C1-7436-11CE-8034-00AA006009FA"), InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)> _
Public Interface IServiceProvider
    <PreserveSig()> _
    Function QueryService(ByRef guidService As Guid, ByRef riid As Guid, <MarshalAs(UnmanagedType.[Interface])> ByRef ppvObject As Object) As <MarshalAs(UnmanagedType.I4)> Integer
End Interface