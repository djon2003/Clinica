Namespace LogCommands
    Friend Class LogCommander

        Private commandsTypes As Generic.List(Of Type)
        Private Shared mySelf As LogCommander
        Private curFilePath As String = ""
        Private helpCmd As String = ""

        Private Sub new()

        End Sub

        Public Shared Sub start()
            Console.SetBufferSize(150, 20000)

            MySelf = New LogCommander
            MySelf.commandsTypes = GetCommandsTypes()
            MySelf.LoopForMessage()
        End Sub

        Private Sub loopForMessage()
            Dim firstCmd As LogCommand = GetCommand("CHANGEFILE", Nothing)
            AddHandler FirstCmd.Executed, AddressOf ExecutionDone
            FirstCmd.Execute()

            If CurFilePath = "" Then Exit Sub

            Dim continueAsking As Boolean = True
            Do
                continueAsking = AskForCommand()
            Loop Until continueAsking = False
        End Sub

        Private Function askForCommand(Optional ByVal invalidCommand As Boolean = False) As Boolean
            Dim helpCmd As LogCommand = GetCommand(IIf(Me.helpCmd <> "", Me.helpCmd, "HELP").ToString, Nothing)
            Me.HelpCmd = ""
            HelpCmd.Execute()
            Console.WriteLine()
            Console.Write("Veuillez entrer une commande ")
            If InvalidCommand Then Console.Write("(Commande précédente invalide) ")
            Console.WriteLine(":")
            Dim cmd As String = Console.ReadLine
            If cmd = "" Then Return False
            If cmd.ToUpper.StartsWith("HELP") Then
                Me.HelpCmd = cmd
                Return True
            End If

            Dim outputFile As String = ""
            Dim writer As IO.StreamWriter = Nothing
            If cmd.Contains("/f:") Then
                outputFile = cmd.Substring(cmd.IndexOf("/f:") + 3)
                If outputFile.IndexOf("/") <> -1 Then outputFile = outputFile.Substring(0, outputFile.IndexOf("/"))
                If outputFile.Contains("\") = False Then outputFile = My.Application.Info.DirectoryPath & IIf(My.Application.Info.DirectoryPath.EndsWith("\"), "", "\") & outputFile

                Try
                    ' Attempt to open output file.
                    writer = New IO.StreamWriter(outputFile)
                    ' Redirect standard output from the console to the output file.
                    Console.SetOut(writer)
                Catch e As IO.IOException
                End Try
            End If

            Dim curCmd As LogCommand = GetCommand(cmd, New Object() {Me.CurFilePath})
            If CurCmd Is Nothing Then AskForCommand(True) : Return True

            AddHandler CurCmd.Executed, AddressOf Me.ExecutionDone
            CurCmd.Execute()

            If cmd.Contains("/f:") Then
                writer.Flush()
                writer.Close()
                Dim standardOutput As New IO.StreamWriter(Console.OpenStandardOutput(), System.Text.Encoding.GetEncoding(850))
                standardOutput.AutoFlush = True
                Console.SetOut(standardOutput)

                Console.WriteLine("Résultat de la commande enregistrée dans le fichier :")
                Console.WriteLine(outputFile)
            End If

            If Me.HelpCmd = "" Then
                Console.WriteLine()
                Console.WriteLine("Appuyer sur une touche pour continuer...")
                Console.ReadKey() 'Wait
            End If
            Return True
        End Function

        Public Sub executionDone(ByVal sender As Object, ByVal data As String)
            If TypeOf sender Is LogCommands.CmdAskForFile Then
                CurFilePath = data
            End If
            If data.StartsWith("HELP") Then HelpCmd = data
        End Sub

        Public Shared Function getCommand(ByVal commandLine As String, ByVal params() As Object) As LogCommand
            Dim curCommand As LogCommand

            For i As Integer = 0 To MySelf.commandsTypes.Count - 1
                CurCommand = Activator.CreateInstance(MySelf.commandsTypes(i), New Object() {commandLine, params})
                If CurCommand.IsCommand(commandLine) Then Return CurCommand
            Next i

            Return Nothing
        End Function

        Public Shared Function getCommandsTypes() As Generic.List(Of Type)
            Dim commands As New Generic.List(Of Type)
            With System.Reflection.Assembly.GetExecutingAssembly
                Dim curTypes() As Type = .GetTypes
                For i As Integer = 0 To CurTypes.GetUpperBound(0)
                    If CurTypes(i).BaseType IsNot Nothing AndAlso CurTypes(i).BaseType.Name = "LogCommand" Then
                        commands.Add(CurTypes(i))
                    End If
                Next i
            End With

            Return commands
        End Function
    End Class
End Namespace
