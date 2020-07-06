Public MustInherit Class TCPCommunicationBase

    Private _client As TCPClient
    Private _guid As String = String.Empty
    Protected data As DataTCP

    Private Shared constructorTypes As System.Type() = New System.Type() {GetType(TCPClient), GetType(DataTCP)}
    Private Shared comms As New Dictionary(Of String, Dictionary(Of String, System.Reflection.ConstructorInfo))
    Private Shared lockComms As New Object

#Region "Constructors"
    Public Sub New(ByVal client As TCPClient)
        _client = client
        _guid = generateGUID()
    End Sub

    Public Sub New(ByVal tcpMessage As TCPCommunicationBase)
        _client = tcpMessage._client
        _guid = tcpMessage._guid
    End Sub

    Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
        _client = client
        _guid = data.guid
        Me.data = data
    End Sub

    Protected Shared Function createTCPCommunication(ByVal baseClassType As Type, ByVal client As TCPClient, ByVal receivedData As DataTCP) As TCPCommunicationBase
        Dim comm As TCPCommunicationBase = Nothing
        Dim curNamespace As String = baseClassType.Namespace
        SyncLock lockComms
            If comms.ContainsKey(curNamespace) = False Then
                Dim newList As New Dictionary(Of String, System.Reflection.ConstructorInfo)
                comms.Add(curNamespace, newList)

                Try
                    Dim types() As Type = baseClassType.Assembly.GetTypes()
                    For Each curType As Type In types
                        If curType.Equals(baseClassType) = False AndAlso curType.Namespace = curNamespace Then
                            Dim nameField As Reflection.FieldInfo = curType.GetField("NAME")
                            If nameField Is Nothing Then Throw New CI.Base.TCPMessageMissingNameException(curType)

                            Dim constructor As System.Reflection.ConstructorInfo = curType.GetConstructor(constructorTypes)
                            If constructor Is Nothing Then Throw New MissingConstructorException(curType, Nothing, constructorTypes)

                            Dim typeName As String = nameField.GetValue(Nothing)
                            Dim conflictingType As Type = isNameAlreadyUsed(typeName, curType)
                            If conflictingType IsNot Nothing Then Throw New TCPMessageNameAlreadyExistsException(curType, conflictingType)

                            newList.Add(typeName, constructor)
                        End If
                    Next
                Catch ex As System.Reflection.ReflectionTypeLoadException
                    External.propagateErrorLog(ex)
                    Throw ex
                End Try
            End If
        End SyncLock

        If comms(curNamespace).ContainsKey(receivedData.command) Then
            Dim constructor As System.Reflection.ConstructorInfo = comms(curNamespace)(receivedData.command)
            comm = constructor.Invoke(New Object() {client, receivedData})
        End If

        Return comm
    End Function

    Private Shared Function isNameAlreadyUsed(ByVal typeName As String, ByVal curType As Type) As Type
        For Each list As Dictionary(Of String, System.Reflection.ConstructorInfo) In comms.Values
            If list.ContainsKey(typeName) Then Return list(typeName).DeclaringType
        Next

        Return Nothing
    End Function
#End Region

#Region "Properties"
    Public ReadOnly Property client() As TCPClient
        Get
            Return _client
        End Get
    End Property

    Public ReadOnly Property guid() As String
        Get
            Return _guid
        End Get
    End Property
#End Region

    Public Sub regenerateGuid()
        _guid = generateGUID()
    End Sub

    Protected Sub sendData(Optional ByVal sendMsgNo As Boolean = True)
        _client.sendData(getSendingData(), sendMsgNo)
    End Sub

    Public Function getSendingData() As String
        If _guid = String.Empty Then Return Me.ToString()

        Return _guid & DataTCP.PARAMS_SEPARATOR & Me.ToString()
    End Function

    ''' <summary>
    ''' Get the TCPClient linked with this communication. 
    ''' </summary>
    ''' <returns>TCPClient linked with this communication</returns>
    ''' <remarks>Use only when the TCPClient method doesn't exist in the class</remarks>
    Public Function getClient() As TCPClient
        Return _client
    End Function

    Public MustOverride Sub execute()
End Class
