Imports System.Runtime.Serialization.Formatters.Binary

Namespace TCPCommands


    Public Class AskConfig
        Inherits TCPCommand

        Public Const NAME As String = "ASKCONFIG"

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
                Dim configs As New List(Of ConfigBase)
                For Each curConfig As ConfigBase In ConfigurationsManager.getInstance.getItemables()
                    If curConfig.hasToUpdate Then configs.Add(curConfig)
                Next

                Dim configsAnswer As New TCPAnswers.Configs(Me, configs)
                configsAnswer.execute()
            End If
        End Sub

        Public Overrides Function ToString() As String
            Return NAME
        End Function
    End Class


End Namespace