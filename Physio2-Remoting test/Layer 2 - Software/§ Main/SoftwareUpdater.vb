Public Class SoftwareUpdater
    'Implements IDataConsumer(Of DataTCP)
    Implements ITcpWaiter

    Private Const MAX_CONFIGURE_ATTEMPTS As Byte = 10
    Private Const NB_100_MS_LOOP_FOR_NEXT_ATTEMPT As Byte = 20
    'Private configsReceived As TCPAnswers.Configs

    Public Sub configureServer()
        Loading.getInstance.forward("Configure le serveur")

        'Clean old temp folder
        TCPAnswers.Configs.cleanTempFolder()

        Dim askConfig As New TCPCommands.AskConfig(TCPClient.getInstance())
        askConfig.execute()
        Dim configsReceived As TCPAnswers.Configs = Nothing
        Dim innerException As Exception = Nothing
        Try
            configsReceived = TCPHelper.getInstance().sendWaitCommunication(Me, askConfig, GetType(TCPAnswers.Configs))
        Catch ex As Exception
            innerException = ex
        End Try

        'If answer not received, quit software and inform user what he could try
        If configsReceived Is Nothing Then
            Throw New SendingServerCmdException(askConfig.ToString, GetType(TCPAnswers.Configs).Name, innerException)
        End If

        'Add remote configs to manager
        For Each curConfig As ConfigBase In configsReceived.configs
            curConfig.writeXmlOnSave = False 'Prevent saving useless local file. It is saved over network in a remote file.
            Base.ConfigurationsManager.getInstance.addItemable(curConfig)
        Next

        'Ensure client and server configurations are update to date
        Base.ConfigurationsManager.getInstance().hasToConfigureOnlyMainOne = False
        Base.ConfigurationsManager.getInstance().ensureConfigsUpToDate()

        If configsReceived.configs.Count <> 0 Then
            Dim configsAnswer As New TCPAnswers.Configs(TCPClient.getInstance(), configsReceived.configs)
            configsAnswer.execute()
        End If
    End Sub


    'Public Sub dataConsume(ByVal dataReceived As Base.DataTCP) Implements Base.IDataConsumer(Of Base.DataTCP).dataConsume
    '    Dim tcpAnswer As TCPAnswers.TCPAnswer = TCPAnswers.TCPAnswer.createTCPAnswer(TCPClient.getInstance(), dataReceived)

    '    If TypeOf tcpAnswer Is TCPAnswers.Configs Then
    '        configsReceived = tcpAnswer
    '    End If
    'End Sub

    'Public ReadOnly Property priority() As Integer Implements Base.IDataConsumer(Of Base.DataTCP).priority
    '    Get
    '        Return 10
    '    End Get
    'End Property

    'Public Function CompareTo(ByVal other As Base.IDataConsumer(Of Base.DataTCP)) As Integer Implements System.IComparable(Of Base.IDataConsumer(Of Base.DataTCP)).CompareTo
    '    Return other.priority.CompareTo(Me.priority)
    'End Function

    Public Sub onOneLoopDone(ByVal currentAttemptTime As Integer, ByVal nbTries As Integer, ByVal maxTries As Integer, ByVal isNewTry As Boolean) Implements Base.ITcpWaiter.onOneLoopDone
        If Loading.getInstance.Visible Then
            If isNewTry Then
                Loading.getInstance.forward("Configure le serveur : Tentative #" & nbTries & " / " & maxTries)
            Else
                Loading.getInstance.appendDetail(".")
            End If
        End If
    End Sub
End Class
