Imports CI.ProjectUpdates.ProjectUpdateLibrary.Downloader

Public Class ExternalUpdateServer
    Implements IExternalUpdate, DataConsumer(Of DataTCP)

    Private isServerUpdated, isCheckingForServerUpdate As Boolean
    Private updateAnswer As TCPAnswers.UpdateAnswer
    Private _newVersion As Integer

#Region "Properties"
    Public Property newVersion() As Integer Implements IExternalUpdate.newVersion
        Get
            Return _newVersion
        End Get
        Set(ByVal value As Integer)
            _newVersion = value
        End Set
    End Property
#End Region


    Public Sub download() Implements IExternalUpdate.download
        'Nothing to do... 
    End Sub

    Public Sub update() Implements IExternalUpdate.update
        'Update Chat server
        TCPClient.getInstance.addConsumer(Me)
        isServerUpdated = True
        isCheckingForServerUpdate = True
        Dim isUpdateCommand As New TCPCommands.IsUpdate(TCPClient.getInstance())
        isUpdateCommand.execute()

        Dim maxLoop As Integer = 90000
        While isCheckingForServerUpdate = True And maxLoop > 0
            Threading.Thread.Sleep(500)
            maxLoop -= 1
        End While
        If maxLoop <= 0 Then MessageBox.Show("Impossible de mettre à jour le serveur de Clinica. Veuillez demander à l'administrateur de le faire manuellement.", "Mise à jour serveur Clinica")

        If maxLoop > 0 AndAlso isServerUpdated = False Then
            Dim updateCommand As New TCPCommands.Update(TCPClient.getInstance())
            updateCommand.execute()

            isCheckingForServerUpdate = True
            While isCheckingForServerUpdate
                Threading.Thread.Sleep(100)
            End While
            If updateAnswer.answerError <> "" Then
                MessageBox.Show("Impossible de mettre à jour le serveur de Clinica. Veuillez demander à l'administrateur de le faire manuellement." & vbCrLf & vbCrLf & "Message du serveur :" & vbCrLf & updateAnswer.answerError, "Mise à jour serveur Clinica")
            End If
        End If
        TCPClient.getInstance.removeConsumer(Me)
    End Sub

    Public Sub dataConsume(ByVal data As Base.DataTCP) Implements Base.DataConsumer(Of Base.DataTCP).dataConsume
        Dim tcpAnswer As TCPAnswers.TCPAnswer = TCPAnswers.TCPAnswer.createTCPAnswer(TCPClient.getInstance(), data)
        If TypeOf tcpAnswer Is TCPAnswers.IsUpdated Then
            isServerUpdated = CType(tcpAnswer, TCPAnswers.IsUpdated).isUpdated
            isCheckingForServerUpdate = False
            Exit Sub
        End If

        If TypeOf tcpAnswer Is TCPAnswers.UpdateAnswer Then
            updateAnswer = tcpAnswer
            isCheckingForServerUpdate = False
        End If
    End Sub

    Public ReadOnly Property priority() As Integer Implements DataConsumer(Of DataTCP).priority
        Get
            Return 10
        End Get
    End Property

    Public Function compareTo(ByVal other As DataConsumer(Of DataTCP)) As Integer Implements System.IComparable(Of DataConsumer(Of DataTCP)).CompareTo
        other.priority.CompareTo(priority)
    End Function
End Class
