Namespace LogCommands
    Friend MustInherit Class LogCommand
        Protected commandLine As String
        Protected params() As Object

        Public Sub new(ByVal commandLine As String)
            Me.commandLine = commandLine
        End Sub

        Public Sub new(ByVal commandLine As String, ByVal params() As Object)
            Me.commandLine = commandLine
            Me.params = params
        End Sub

        Public MustOverride Function getHelp() As String
        Public MustOverride Function getShortDescription() As String
        Public MustOverride Function getAcceptedCommands() As String()
        Public Function isCommand(ByVal command As String) As Boolean
            Dim commands() As String = GetAcceptedCommands()
            For i As Integer = 0 To commands.GetUpperBound(0)
                If command.ToUpper.StartsWith(commands(i).ToUpper) Then Return True
            Next i

            Return False
        End Function
        Public MustOverride Sub execute()

        Public Event executed(ByVal sender As Object, ByVal data As String)

        Protected Overridable Sub onExecuted(ByVal data As String)
            RaiseEvent Executed(Me, data)
        End Sub

    End Class
End Namespace
