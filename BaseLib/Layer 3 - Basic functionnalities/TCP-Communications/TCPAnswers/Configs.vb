Namespace TCPAnswers


    Public Class Configs
        Inherits TCPAnswer

        Private Const CONFIG_SPLITTER As String = "§§§"

        Public Const NAME As String = "CONFIGS"
        Private _configs As List(Of ConfigBase)
        Private Shared tempFolder As String = My.Application.Info.DirectoryPath & addSlash(My.Application.Info.DirectoryPath) & "Temp\ConfigFiles"


        Public Sub New(ByVal client As TCPClient, ByVal configs As List(Of ConfigBase))
            MyBase.New(client)
            Me._configs = configs
        End Sub

        Public Sub New(ByVal tcpMessage As TCPCommands.AskConfig, ByVal configs As List(Of ConfigBase))
            MyBase.New(tcpMessage)
            Me._configs = configs
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
            deserializeConfigs(data.getArgsString())
        End Sub

        Public ReadOnly Property configs() As List(Of ConfigBase)
            Get
                Return _configs
            End Get
        End Property

        Private Sub deserializeConfigs(ByVal objectsString As String)
            _configs = New List(Of ConfigBase)

            'No configs to update
            If objectsString = "" Then Exit Sub

            'Create temp dir only when received in client
            If TCPClient.getInstance.isConnected Then IO.Directory.CreateDirectory(tempFolder)

            For Each curConfigString As String In objectsString.Split(New String() {CONFIG_SPLITTER}, StringSplitOptions.None)
                Dim curConfigInfos As New ConfigInfos(curConfigString, tempFolder)
                _configs.Add(curConfigInfos.config)
            Next
        End Sub

        Private Function serializeConfigs() As String
            Dim sending As String = String.Empty

            For Each curConfig As ConfigBase In _configs
                Dim curConfigInfos As New ConfigInfos(curConfig)
                sending &= CONFIG_SPLITTER & curConfigInfos.ToString()
            Next

            If sending <> String.Empty Then sending = sending.Substring(CONFIG_SPLITTER.Length)

            Return sending
        End Function

        Public Overrides Sub execute()
            If data Is Nothing Then
                serializeConfigs()
                sendData(False)
            Else

            End If
        End Sub

        Public Shared Sub cleanTempFolder()
            Try
                If IO.Directory.Exists(tempFolder) Then My.Computer.FileSystem.DeleteDirectory(tempFolder, FileIO.DeleteDirectoryOption.DeleteAllContents)
            Catch ex As Exception

            End Try
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & serializeConfigs()
        End Function
    End Class


End Namespace