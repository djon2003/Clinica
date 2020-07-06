Public Class frmCSSTBrowser

    Delegate Sub invokeSub()

    Public ReadOnly Property nbReturns() As Integer
        Get
            Return CsstBrowser1.nbReturns
        End Get
    End Property

    Public ReadOnly Property viewLocation() As String
        Get
            Return CsstBrowser1.viewLocation
        End Get
    End Property

    Public Event returnDownloaded()
    Public Event startingReturnDownload()

    Public Overloads Sub focus()
        If Me.InvokeRequired Then
            Me.Invoke(New invokeSub(AddressOf focus))
            Exit Sub
        End If

        MyBase.Focus()
        CsstBrowser1.Select()
    End Sub

    Public Function connect(ByVal username As String, ByVal password As String) As Boolean
        Return CsstBrowser1.connect(username, password)
    End Function

    Public Sub uploadFile(ByVal file As String)
        CsstBrowser1.uploadFile(file)
    End Sub

    Public Function getReturn() As List(Of String)
        Return CsstBrowser1.getReturn()
    End Function

    Public Sub deleteMails()
        CsstBrowser1.deleteMails()
    End Sub

    Public Sub New()

        ' Cet appel est requis par le Concepteur Windows Form.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        AddHandler CsstBrowser1.returnDownloaded, AddressOf onReturnDownloaded
        AddHandler CsstBrowser1.startingReturnDownload, AddressOf onStartingReturnDownload
    End Sub

    Private Sub onReturnDownloaded()
        RaiseEvent returnDownloaded()
    End Sub

    Private Sub onStartingReturnDownload()
        RaiseEvent startingReturnDownload()
    End Sub
End Class