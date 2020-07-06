Public Class InternalType_Text
    Implements IOpenable

    Public Class InternalType_Text_Options
        Implements IOpenableOptions

        Public Sub New()
        End Sub

        Public Sub New(ByVal windowTitle As String)
            Me.windowTitle = windowTitle
        End Sub

        Public isReadOnly As Boolean = True
        Public windowTitle As String = ""
    End Class

    Public Function open(ByVal uri As String, ByVal options As IOpenableOptions) As IOpenable.OpenableReturn Implements IOpenable.open
        Dim uriUpper As String = uri.ToUpper()
        Dim isHTML As Boolean = uriUpper.EndsWith(".HTML")
        Dim isRTF As Boolean = uriUpper.EndsWith(".RTF")
        If options IsNot Nothing AndAlso Not TypeOf options Is InternalType_Text_Options Then Return IOpenable.OpenableReturn.NotOpenable
        If Not isRTF AndAlso Not isHTML AndAlso uriUpper.EndsWith(".TXT") = False Then Return IOpenable.OpenableReturn.NotOpenable

        Dim myOptions As InternalType_Text_Options = options
        If myOptions Is Nothing Then myOptions = New InternalType_Text_Options

        'Open URI
        If Not isRTF Then
            TextWindow.getInstance.texteType = RichTextBoxStreamType.PlainText
        Else
            TextWindow.getInstance.texteType = RichTextBoxStreamType.RichText
        End If
        TextWindow.getInstance.currentData = IO.File.ReadAllText(uri, System.Text.Encoding.UTF7)
        TextWindow.getInstance.Text = "Visualisation : " & myOptions.windowTitle
        TextWindow.getInstance.isHTML = isHTML
        TextWindow.getInstance.isLocked = myOptions.isReadOnly
        Dim mySel As String = TextWindow.getInstance.ShowTexteModif(0)

        Return IOpenable.OpenableReturn.Opened
    End Function

End Class
