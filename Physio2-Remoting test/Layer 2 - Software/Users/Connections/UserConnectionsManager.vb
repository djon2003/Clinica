Public Class ConnectionsManager
    Inherits DBItemableManagerBase(Of ConnectionsManager, UserConnection)

    Private _currentUser As Integer
    Private Shared _isLoaded As Boolean = False

    Protected Sub New()
        load()
        _isLoaded = True
    End Sub

    Public Shared ReadOnly Property isLoaded() As Boolean
        Get
            Return _isLoaded
        End Get
    End Property

    Public Shared Property currentUser() As Integer
        Get
            Return ConnectionsManager.getInstance._currentUser
        End Get
        Set(ByVal value As Integer)
            ConnectionsManager.getInstance._currentUser = value
        End Set
    End Property

    Public Function createConnection(ByVal noUser As Integer) As UserConnection
        'REM SHALL do method Access.checkForExistingConnections

        Dim myConn As New UserConnection(noUser, Environment.MachineName)
        ConnectionsManager.getInstance.addItemable(myConn)
        InternalUpdatesManager.getInstance.sendUpdate("Connections(" & myConn.noConnection & ")")
        _currentUser = noUser

        Return myConn
    End Function

    Public Overloads Function getItemable(ByVal noUser As Integer, ByVal computerName As String) As UserConnection
        For Each curConn As UserConnection In MyBase.getItemables()
            If curConn.noUser = noUser AndAlso curConn.computerName = computerName Then Return curConn
        Next

        Return Nothing
    End Function

    Public Overloads Sub removeItemable(ByVal noUser As Integer, ByVal computerName As String)
        Dim curConn As UserConnection = getItemable(noUser, computerName)
        If curConn IsNot Nothing Then MyBase.removeItemable(curConn)
    End Sub

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.function <> "Connections" Then Exit Sub

        Select Case dataReceived.params.Length
            Case 0
                load()
            Case 1
                If dataReceived.params(0) < 0 Then
                    Dim conn As UserConnection = getItemable(Math.Abs(Integer.Parse(dataReceived.params(0))))
                    If conn IsNot Nothing Then removeItemable(conn)
                Else
                    load(dataReceived.params(0))
                End If
        End Select
    End Sub

    Public Overrides Sub load()
        load(0)
    End Sub
    Private Overloads Sub load(ByVal noConnection As Integer)
        Dim data As DataSet = DBLinker.getInstance.readDBForGrid("UsersConnected", "*", IIf(noConnection = 0, "", "noConnection=" & noConnection))

        If noConnection = 0 Then Me.clear()
        If noConnection <> 0 AndAlso (data Is Nothing OrElse data.Tables.Count = 0 OrElse data.Tables(0).Rows.Count = 0) Then
            Dim conn As UserConnection = getItemable(noConnection)
            If conn IsNot Nothing Then MyBase.removeItemable(conn)
            Exit Sub
        End If
        If (data Is Nothing OrElse data.Tables.Count = 0 OrElse data.Tables(0).Rows.Count = 0) Then Exit Sub

        If noConnection <> 0 Then
            Dim conn As UserConnection = getItemable(noConnection)
            Dim connData As New DBItemableData(data.Tables(0).Rows(0))
            If conn Is Nothing Then
                MyBase.addItemable(New UserConnection(connData))
                Exit Sub
            End If
            conn.loadData(connData)
        Else
            For Each curRow As DataRow In data.Tables(0).Rows
                Dim connection As New UserConnection(New DBItemableData(curRow))
                If Me.getItemable(connection.noItemable) IsNot Nothing Then Continue For ' The connection had been modified by another at the same time as loading

                MyBase.addItemable(connection)
            Next
        End If
    End Sub

    Protected Overrides Sub sendUpdate()
        InternalUpdatesManager.getInstance.sendUpdate("Connections()")
    End Sub
End Class
