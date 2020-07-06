Public Class UserTypeManager
    Inherits ManagerBase(Of UserTypeManager)
    Implements IDataConsumer(Of DataInternalUpdate)

    Private userTypes As New Generic.List(Of UserType)

    Protected Sub New()
        load()
        InternalUpdatesManager.getInstance.addConsumer(Me)
    End Sub

    Private Sub load()
        Dim userTypes As DataSet = DBLinker.getInstance.readDBForGrid("TypeUtilisateur", "*")
        If userTypes Is Nothing OrElse userTypes.Tables.Count = 0 Then Exit Sub

        Me.userTypes.Clear()
        For Each curRow As DataRow In userTypes.Tables(0).Rows
            Dim newUserType As New UserType
            newUserType.loadData(New DBItemableData(curRow))
            Me.userTypes.Add(newUserType)
        Next
    End Sub

    Public Function getUserType(ByVal noUserType As Integer) As UserType
        For Each curUT As UserType In userTypes
            If curUT.noUserType = noUserType Then Return curUT
        Next

        Return Nothing
    End Function

    Public Function getUserTypes() As Generic.List(Of UserType)
        Return userTypes
    End Function

    Public Sub update()
        load()
        InternalUpdatesManager.getInstance.sendUpdate("UserTypes()")
    End Sub

    Public Sub dataConsume(ByVal dataReceived As DataInternalUpdate) Implements IDataConsumer(Of DataInternalUpdate).dataConsume
        If dataReceived.function <> "UserTypes" OrElse dataReceived.fromExternal = False Then Exit Sub

        load()
    End Sub

    Public ReadOnly Property priority() As Integer Implements IDataConsumer(Of DataInternalUpdate).priority
        Get
            Return 0
        End Get
    End Property

    Public Function compareTo(ByVal other As IDataConsumer(Of DataInternalUpdate)) As Integer Implements System.IComparable(Of IDataConsumer(Of DataInternalUpdate)).CompareTo
        Return other.priority.CompareTo(priority)
    End Function
End Class
