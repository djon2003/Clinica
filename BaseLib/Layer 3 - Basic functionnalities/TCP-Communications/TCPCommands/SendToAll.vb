Namespace TCPCommands


    Public Class SendToAll
        Inherits TCPCommand

        Public Const NAME As String = "TOALL"

        Private Const NUMBER_OF_DEFINED_SUBCOMMAND_ARGS As Byte = 3 'This includes the subCommand itself
        Private subCommand As String = String.Empty
        Private subCommandArgs() As String
        Private sendingUser As Integer
        Private sendingComputer As String = String.Empty
        Private isHost As Boolean = TCPHost.getInstance().isListening

#Region "Constructors"
        Public Sub New(ByVal client As TCPClient, ByVal subCommand As String, ByVal subCommandArgs() As String)
            MyBase.New(client)
            Me.subCommand = subCommand
            Me.subCommandArgs = subCommandArgs
            Me.sendingUser = External.current.currentUser
            Me.sendingComputer = Environment.MachineName
        End Sub

        Public Sub New(ByVal tcpMessage As TCPCommands.SendToAll)
            MyBase.New(tcpMessage)
            Me.subCommand = tcpMessage.subCommand
            Me.subCommandArgs = tcpMessage.subCommandArgs
            Me.sendingComputer = tcpMessage.sendingComputer
            Me.sendingUser = tcpMessage.sendingUser
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
            Me.subCommand = data.args(0)
            Me.sendingUser = data.args(1)
            Me.sendingComputer = data.args(2)
            ReDim Me.subCommandArgs(data.args.Length - NUMBER_OF_DEFINED_SUBCOMMAND_ARGS - 1)
            Array.Copy(data.args, NUMBER_OF_DEFINED_SUBCOMMAND_ARGS, Me.subCommandArgs, 0, data.args.Length - NUMBER_OF_DEFINED_SUBCOMMAND_ARGS)
        End Sub
#End Region

        Public Overrides Sub execute()
            If data Is Nothing Then
                If Not isHost Then
                    sendData()
                Else
                    TCPHost.getInstance().sendToAllClients(Me)
                End If
            Else
                Dim sendToAllCmd As New SendToAll(Me)
                TCPHost.getInstance().sendToAllClients(sendToAllCmd)
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & subCommand & DataTCP.PARAMS_SEPARATOR & sendingUser & DataTCP.PARAMS_SEPARATOR & sendingComputer & DataTCP.PARAMS_SEPARATOR & String.Join(DataTCP.PARAMS_SEPARATOR, subCommandArgs)
        End Function
    End Class


End Namespace