Namespace LogCommands
    Friend Class CmdHelp
        Inherits LogCommand

        Public Sub new(ByVal commandLine As String, ByVal params() As Object)
            MyBase.New(commandLine, params)
        End Sub

        Public Overrides Sub execute()
            Console.Clear()

            If commandLine.ToUpper = "HELP" Then
                Console.WriteLine("Commandes disponibles (Taper la commande + /? pour plus d'informations) :")

                Dim existingsCommands As Generic.List(Of Type) = LogCommander.GetCommandsTypes
                For i As Integer = 0 To existingsCommands.Count - 1
                    Dim curCommand As LogCommand = Activator.CreateInstance(existingsCommands(i), New Object() {"", New Object() {}})
                    Dim commands() As String = CurCommand.GetAcceptedCommands
                    Dim command As String = String.Join(" | ", commands)
                    Console.WriteLine(command & " : " & CurCommand.GetShortDescription)
                Next i

                Console.WriteLine()
                Console.WriteLine("Utiliser le paramètre /f:FILENAME pour envoyer le résultat des commandes dans un fichier nommé FILENAME")
            Else
                Dim curCommand As String = commandLine.Substring(5)
                Dim cCommand As LogCommand = LogCommander.GetCommand(curCommand, Nothing)
                Console.WriteLine("Aide spécifique sur la commande " & curCommand.ToUpper & " :")
                Console.WriteLine(cCommand.GetHelp)
            End If

            OnExecuted("")
        End Sub

        Public Overrides Function getAcceptedCommands() As String()
            Return New String() {"HELP", "?", "/?"}
        End Function

        Public Overrides Function getShortDescription() As String
            Return "Affiche cette aide"
        End Function

        Public Overrides Function getHelp() As String
            Return GetShortDescription() & vbCrLf & "Pour avoir plus d'informations sur une commande, entrer 'HELP ' et le nom de la commande"
        End Function
    End Class
End Namespace
