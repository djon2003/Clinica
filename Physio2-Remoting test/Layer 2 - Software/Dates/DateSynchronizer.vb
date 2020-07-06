Public Class DateSynchronizer
    Implements IDataConsumer(Of DataTCP)

    Private waiting As Boolean = False
    Public Event serverDateReceived(ByVal serverDate As Date)
    Private serverDate As Date = LIMIT_DATE

    Public Sub New()

    End Sub

    Public Function adviseSynch() As Boolean
        Dim isDateGotten As Boolean = getServerDate()

        If isDateGotten AndAlso serverDate.Date.Equals(Date.Today.Date) = False Then
            MessageBox.Show("La date de votre ordinateur n'est pas la même que celle du serveur. Veuillez corriger la situation pour pouvoir continuer." & vbCrLf & vbCrLf & "Date du serveur : " & DateFormat.getTextDate(serverDate), "Date de l'ordinateur invalide", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        ElseIf isDateGotten = False Then
            Return False
        End If

        Return True
    End Function

    Private Function getServerDate(Optional ByVal wait As Boolean = True) As Boolean
        waiting = True
        TCPClient.getInstance.addConsumer(Me)

        Dim timeCommand As New TCPCommands.Time(TCPClient.getInstance)
        timeCommand.execute()
        If wait Then
            Dim nbLoop As Integer = 1
            While waiting
                If nbLoop = 10 Then
                    Try
                        timeCommand.execute()
                    Catch ex As Exception
                        MessageBox.Show("Impossible de vérification la date avec le serveur. Veuillez vous assurer que le serveur de Clinica est ouvert et fonctionnel.", "Vérication impossible", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Return False
                    End Try
                End If
                Threading.Thread.Sleep(100)

                If Loading.getInstance.Visible Then Loading.getInstance().appendDetail(".")
                nbLoop += 1
                If nbLoop = 11 Then nbLoop = 1
            End While
        End If

        Return True
    End Function

    Public Sub dataConsume(ByVal dataReceived As DataTCP) Implements IDataConsumer(Of DataTCP).dataConsume
        Dim tcpAnswer As TCPAnswers.TCPAnswer = TCPAnswers.TCPAnswer.createTCPAnswer(TCPClient.getInstance(), dataReceived)
        If Not TypeOf tcpAnswer Is TCPAnswers.Time Then Exit Sub

        TCPClient.getInstance.removeConsumer(Me)

        serverDate = CType(tcpAnswer, TCPAnswers.Time).serverDate
        RaiseEvent serverDateReceived(serverDate)

        waiting = False
    End Sub

    Protected Overrides Sub finalize()
        MyBase.Finalize()
    End Sub

    Public ReadOnly Property priority() As Integer Implements IDataConsumer(Of DataTCP).priority
        Get
            Return 0
        End Get
    End Property

    Public Function compareTo(ByVal other As IDataConsumer(Of DataTCP)) As Integer Implements System.IComparable(Of IDataConsumer(Of DataTCP)).CompareTo
        Return other.priority.CompareTo(priority)
    End Function
End Class
