'MailVerificationProtocol
Public Class MVP
    Inherits Protocols.protocol

    Public Shared notAccessibleServers As New Generic.List(Of String)

    Private server As String = ""
    Private keywordAccepted() As String = New String() {"connection refused", "blacklist", "black list", "reject"}

    Public Sub New(ByVal server As String, ByVal port As Integer)
        MyBase.New()
        Me.server = server
        If notAccessibleServers.Contains(server) = False Then MyBase.Connexion(server, port)
    End Sub


    Public Function validateEmail(ByVal emailTo As String, ByVal emailFrom As String) As Boolean
        If notAccessibleServers.Contains(server) = True Then Return True

        If Me.DernieresReponses IsNot Nothing AndAlso Me.DernieresReponses.StartsWith("421 ") Then
            Return False
        End If
        If Me.DernieresReponses IsNot Nothing AndAlso Me.DernieresReponses.StartsWith("2") = False Then
            notAccessibleServers.Add(Me.server)
            Return True
        End If

        MyBase.EnvoiCommande("HELO " & emailFrom.Substring(emailFrom.IndexOf("@") + 1))
        If Me.DernieresReponses IsNot Nothing AndAlso Me.DernieresReponses.StartsWith("2") = False AndAlso containsKeyword() Then Return True
        If Me.DernieresReponses IsNot Nothing AndAlso Me.DernieresReponses.StartsWith("2") = False Then Return False

        Dim isValid As Boolean = True
        Dim validSet As Boolean = False

        MyBase.EnvoiCommande("MAIL FROM:<" & emailFrom & ">")
        If Me.DernieresReponses IsNot Nothing AndAlso Me.DernieresReponses.StartsWith("550") Then
            isValid = True 'IF IP on block list (or maybe other errors)
            validSet = True
        End If
        If Not validSet AndAlso Me.DernieresReponses IsNot Nothing AndAlso Me.DernieresReponses.StartsWith("2") = False Then
            isValid = False
            validSet = True
        End If

        If Not validSet Then
            MyBase.EnvoiCommande("RCPT TO:<" & emailTo & ">")
            If Me.DernieresReponses IsNot Nothing AndAlso Me.DernieresReponses.StartsWith("2") = False AndAlso containsKeyword() = False Then
                isValid = False
                validSet = True
            End If
        End If

        MyBase.EnvoiCommande("QUIT")

        Return isValid
    End Function

    Private Function containsKeyword() As Boolean
        For i As Integer = 0 To keywordAccepted.Length - 1
            If Me.DernieresReponses.ToLower.Contains(keywordAccepted(i).ToLower) = True Then Return True
        Next i

        Return False
    End Function

    Private Sub MVP_BytesReceived(ByVal totalBytes As Long) Handles Me.BytesReceived

    End Sub

    Private Sub MVP_Erreur(ByVal message As String) Handles Me.Erreur
        Console.WriteLine("MVP_Erreur:" & message)
    End Sub
End Class
