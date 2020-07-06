Namespace TCPAnswers


    Public Class TasksTypesInfo
        Inherits TCPAnswer

        Private Const TASK_SPLITTER As String = "§§§"
        Private Const INFO_SPLITTER As String = "§§"

        Public Const NAME As String = "TASKSTYPES"
        Private infos As List(Of TaskTypeInfos)

        Public Sub New(ByVal client As TCPClient, ByVal infos As List(Of TaskTypeInfos))
            MyBase.New(client)
            Me.infos = infos
        End Sub

        Public Sub New(ByVal tcpMessage As TCPCommands.GetTasksTypesInfos, ByVal infos As List(Of TaskTypeInfos))
            MyBase.New(tcpMessage)
            Me.infos = infos
        End Sub

        Public Sub New(ByVal client As TCPClient, ByVal data As DataTCP)
            MyBase.New(client, data)
        End Sub

        Public Overrides Sub execute()
            If data Is Nothing Then
                sendData(False)
            Else
                If data.getArgsString() = "" Then Exit Sub

                For Each curData As String In data.getArgsString().Split(New String() {TASK_SPLITTER}, StringSplitOptions.None)
                    PluginTasksManager.getInstance.addItemable(New PluginTaskClient(curData.Split(New String() {INFO_SPLITTER}, StringSplitOptions.None)))
                Next
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME & DataTCP.PARAMS_SEPARATOR & String.Join(TASK_SPLITTER, Array.ConvertAll(Of TaskTypeInfos, String)(Me.infos.ToArray(), New Converter(Of TaskTypeInfos, String)(AddressOf Convert.ToString)))
        End Function
    End Class


End Namespace