Namespace LogCommands
    Friend Class CmdAskForFile
        Inherits LogCommand

        Private curPath As String = ""

        Public Sub new(ByVal commandLine As String, ByVal params() As Object)
            MyBase.New(commandLine, params)
            If params IsNot Nothing AndAlso params.Length <> 0 Then
                CurPath = params(0)
            End If
        End Sub

        Public Overrides Sub execute()
            If commandLine.Contains("/?") Then 'Demande de l'aide
                OnExecuted("HELP CHANGE")
                Exit Sub
            End If

            Console.Clear()
StartBack:
            Console.WriteLine("Veuillez entrer le chemin du fichier :")
            REM Doesn't work correctly.. we are writing over the text.. not modifiable (the CurPath text)
            'If CurPath <> "" Then
            '    Console.Write(CurPath)
            '    Console.SetCursorPosition(0, 1)
            'End If
            Dim filePath As String = Console.ReadLine()
            If filePath <> "" AndAlso filePath.StartsWith("\\") = False AndAlso filePath.Substring(1).StartsWith(":\") = False Then filePath = My.Application.Info.DirectoryPath & IIf(My.Application.Info.DirectoryPath.EndsWith("\"), "", "\") & filePath
            If filePath <> "" AndAlso IO.File.Exists(filePath) = False AndAlso IO.Directory.Exists(filePath) = False Then
                Console.Clear()
                Console.WriteLine("Le chemin entré n'existe pas :")
                Console.WriteLine(filePath)
                Console.WriteLine()
                GoTo StartBack
            End If

            OnExecuted(filePath)
        End Sub

        Public Overrides Function getAcceptedCommands() As String()
            Return New String() {"CHANGEFILE", "CHANGE", "CHANGEDIR"}
        End Function

        Public Overrides Function getShortDescription() As String
            Return "Change le fichier ou dossier de LOG actuellement en cours d'utilisation"
        End Function

        Public Overrides Function getHelp() As String
            Return GetShortDescription()
        End Function
    End Class
End Namespace
