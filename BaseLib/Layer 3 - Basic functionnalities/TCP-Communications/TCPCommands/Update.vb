Namespace TCPCommands


    Public Class Update
        Inherits TCPCommand

        Public Const NAME As String = "UPDATE"

        Public Sub New(ByVal client As TCPClient)
            MyBase.New(client)
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
        End Sub

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData()
            Else
                Dim answerError As String = ""
                External.current.canRestart = False
                Dim updateToDo As Boolean = External.current.isUpdateSoftwareToDo()
                Try
                    External.current.updateSoftware(True)
                Catch ex As Exception
                    answerError = ex.Message
                End Try
                External.current.canRestart = True

                Dim updateAnswer As New TCPAnswers.UpdateAnswer(Me, answerError)
                updateAnswer.execute()

                If updateToDo AndAlso answerError = "" Then
                    Threading.Thread.Sleep(1000)
                    External.current.restartSoftware()
                End If
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & "SERVER"
        End Function
    End Class


End Namespace