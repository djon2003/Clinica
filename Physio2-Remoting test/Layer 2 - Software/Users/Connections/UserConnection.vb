Public Class UserConnection
    Inherits DBItemableBase

    Private _noUser, _noConnection As Integer
    Private _computerName As String
    Private _startTime As Date

#Region "Constructors"
    Public Sub New(ByVal noUser As Integer, ByVal computerName As String)
        _noUser = noUser
        _computerName = computerName
        _startTime = Date.Now
    End Sub

    Public Sub New(ByVal data As DBItemableData)
        loadData(data)
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property noUser() As Integer
        Get
            Return _noUser
        End Get
    End Property

    Public ReadOnly Property noConnection() As Integer
        Get
            Return _noConnection
        End Get
    End Property

    Public ReadOnly Property computerName() As String
        Get
            Return _computerName
        End Get
    End Property

    Public ReadOnly Property startTime() As Date
        Get
            Return _startTime
        End Get
    End Property

#End Region

    Public Overrides Sub delete()
        DBLinker.getInstance.delDB("UsersConnected", "NoConnection", _noConnection, False)
        If autoSendUpdateOnDelete Then InternalUpdatesManager.getInstance.sendUpdate("Connections(-" & noConnection & ")")

        MyBase.onDeleted()
    End Sub

    Public Overrides Sub loadData(ByVal data As DBItemableData)
        _noUser = data.mainData("noUser")
        _noConnection = data.mainData("noConnection")
        _startTime = data.mainData("startTime")
        _computerName = data.mainData("computerName")
    End Sub

    Public Overrides ReadOnly Property noItemable() As Integer
        Get
            Return _noConnection
        End Get
    End Property

    Public Overrides Sub saveData()
        If _noConnection = 0 Then
            DBLinker.getInstance.writeDB("Connections", "NoUser,ComputerName,StartTime", noUser & ",'" & computerName.Replace("'", "''") & "','" & startTime.ToString & "'", , , , _noConnection)
        End If
    End Sub
End Class
