Imports System.Text

Imports System.Net.Sockets
Imports System.IO

Namespace Protocols
    Public Class POP3
        Inherits Protocols.protocol

        'Variables
        Private oMessagesSurServeur As New MessagesSurServeur ' tous les message sur le serveur
        Private oMessageSurServeur As MessageSurServeur

        'variables des propriété
        Private booConnexion As Boolean = False
        Private strLogin As String
        Private strMdp As String
        Private curNoMessage As Integer = 0

        'Evenements
        Public Event message(ByVal message As String)
        Public Event downloadEnded(ByVal sender As POP3)
        Public Event downloading(ByVal status As POP3DownloadingStatus)
        Public Event messageDownloaded(ByVal message As POP3MessageDownloaded)



#Region "Constructeur : on se connecte au serveur"

        ''' <summary>
        ''' Constructeur Pop3
        ''' </summary>
        ''' <param name="Serveur">Serveur pop3 ex pop.free.fr</param>
        ''' <param name="login">login d'authentification au serveur </param>
        ''' <param name="Mdp">Mot de passe d'authentification au serveur</param>
        ''' <param name="port">port par defaut pour protocol pop3 = 110</param>
        ''' <remarks></remarks>
        Sub New(ByVal serveur As String, ByVal login As String, ByVal mdp As String, Optional ByVal port As Int32 = 110)
            'on se connecte au serveur
            MyBase.New(serveur, port)

            If MyBase.lastServerAnswer Is Nothing OrElse MyBase.lastServerAnswer = String.Empty OrElse MyBase.lastServerAnswer.Substring(0, 1) = "-" Then
                Dim serverMsg As String = String.Empty
                If MyBase.lastServerAnswer IsNot Nothing Then
                    Dim spaceIndex As Integer = MyBase.lastServerAnswer.IndexOf(" ")
                    If spaceIndex <> -1 Then serverMsg = " : Message du serveur POP3 : " & MyBase.lastServerAnswer.Substring(spaceIndex + 1)
                End If
                RaiseEvent message("Impossible de se connecter au serveur" & serverMsg)
                booConnexion = False
            Else
                RaiseEvent message("Connexion au serveur établie")
                booConnexion = True
            End If

            'on recupére les données 
            strLogin = login
            strMdp = mdp
        End Sub

#End Region


        '''''''''''''''''''''''  Méthodes  ''''''''''''''''''''''''

        'publiques

#Region "Etat Serveur"

        Public Sub collectServerInfos(ByVal existingUID As String())
            If Not isConnected Then Throw New POP3ConnectionFailedException()

            'variables 
            Dim collMessageId As ArrayList = Nothing
            Dim collMessageTaille As ArrayList = Nothing
            Dim strTaille As String
            Dim i As Integer = 0


            'on recupere la taille des messages en ko 
            MyBase.sendServerCommand("LIST")
            collMessageTaille = New ArrayList()
            collMessageTaille.AddRange(MyBase.lastServerAnswer.Split(vbCrLf))

            'on recupere tous les message sur le serveur

            MyBase.sendServerCommand("UIDL")
            collMessageId = New ArrayList()
            collMessageId.AddRange(MyBase.lastServerAnswer.Split(vbCrLf))

            i = 0


            If collMessageTaille.Count > 1 Then

                For Each Ligne As String In collMessageId
                    Ligne = Ligne.Trim()

                    'on ne prend pas lea premiere ni la derniere ligne de la collection 
                    If Ligne <> "" AndAlso Ligne.Substring(0, 1) <> "+" AndAlso Ligne.Substring(0, 1) <> "." Then
                        Dim messageId As String = Ligne.Substring(Ligne.IndexOf(" ") + 1)
                        If existingUID Is Nothing OrElse Array.IndexOf(existingUID, messageId) = -1 Then
                            oMessageSurServeur = New MessageSurServeur
                            With oMessageSurServeur
                                .isNew = True
                                .numeroMessage = Convert.ToInt32(Ligne.Substring(0, Ligne.IndexOf(" ")))
                                .identifiantMessage = messageId

                                'on recupere la taille dans l'autre tableau
                                strTaille = collMessageTaille.Item(i).ToString
                                .size = Convert.ToInt32(strTaille.Substring(strTaille.LastIndexOf(" ") + 1))
                            End With
                            'on ajoute le message
                            oMessagesSurServeur.Add(oMessageSurServeur)
                        End If
                    End If
                    i += 1
                Next
            End If

        End Sub

#End Region

#Region "Identification"

        ''' <summary>
        ''' Authentification des identifiants
        ''' </summary>
        Public Function identification() As Boolean
            If Not isConnected Then Throw New POP3ConnectionFailedException()

            MyBase.sendServerCommand("USER " & strLogin, , , "")
            MyBase.sendServerCommand("PASS " & strMdp, , , "")

            Try
                If MyBase.lastServerAnswer.Substring(0, 1) = "-" OrElse MyBase.lastServerAnswer.ToUpper().IndexOf(" BAD ") <> -1 Then
                    RaiseEvent message("Mot de passe ou login invalide")
                    Return False
                Else
                    RaiseEvent message("Authentification Ok")
                    Return True
                End If
            Catch ex As Exception
                RaiseEvent message("Mot de passe ou login invalide")
                Return False
            End Try

            Return False
        End Function

#End Region

#Region "On recupere les mails   LIST + UIDL + TOP + RETR"
        Public Sub gatherMails(ByVal existingUID As String(), Optional ByVal eraseMails As Boolean = False)
            If Not isConnected Then Throw New POP3ConnectionFailedException()

            Dim strReponse As String = ""
            Dim i As Integer = 0
            Dim intNumero As Int16

            'on fait un etat du serveur
            collectServerInfos(existingUID)

            's'il y a pas de mail on sort 
            If oMessagesSurServeur.Count <= 0 Then
                RaiseEvent message("Pas de nouveau message")
                RaiseEvent downloadEnded(Me)
                Exit Sub
            End If


            'on enregistre les nouveaux messages et on les decodes
            For Each M As MessageSurServeur In oMessagesSurServeur
                oMessageSurServeur = M
                curNoMessage = i + 1

                If M.isNew AndAlso (existingUID Is Nothing OrElse Array.IndexOf(existingUID, M.identifiantMessage) = -1) Then
                    RaiseEvent downloading(New POP3DownloadingStatus("Début de réception", i + 1, 1, 0, Me.newMessagesCount))

                    i += 1
                    'on recupere le numero du mail sur le serveur
                    intNumero = Convert.ToInt16(M.numeroMessage)

                    RaiseEvent downloading(New POP3DownloadingStatus("Téléchargement du message", i, 2, 0, Me.newMessagesCount))

                    Dim curEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("latin1")

                    'on recupere le mail
                    strReponse = ""
                    Try
                        MyBase.sendServerCommand("RETR " & intNumero, , curEncoding)
                    Catch ex As Exception
                        External.propagateErrorLog(New Exception("Email identification : " & M.identifiantMessage & vbCrLf & "Server : " & login & "@" & serverName & ":" & serverPort, ex))
                        Exit For
                    End Try

                    Try
                        RaiseEvent messageDownloaded(New POP3MessageDownloaded(M.identifiantMessage, MyBase.lastServerAnswerBytes.ToArray(), M.size))
                    Catch ex As Exception
                        External.propagateErrorLog(New Exception("Email identification : " & M.identifiantMessage & vbCrLf & "Server : " & login & "@" & serverName & ":" & serverPort, ex))
                        Exit For
                    End Try
                    M.markHasDownloaded()

                    'on a fini 
                    RaiseEvent downloading(New POP3DownloadingStatus("Réception du message terminé", i, 3, 100, Me.newMessagesCount))
                End If

                'Efface le courriel si désiré
                If eraseMails Then
                    MyBase.sendServerCommand("DELE " & intNumero, , , "")
                    strReponse = MyBase.lastServerAnswer
                End If
            Next

            MyBase.sendServerCommand("QUIT", , , "")
            RaiseEvent downloadEnded(Me)
        End Sub

#End Region


#Region "Properties"
        Public ReadOnly Property newMessagesCount() As Int32
            Get
                Return oMessagesSurServeur.newMessagesCount
            End Get
        End Property

        Public ReadOnly Property newMessagesDownloadedCount() As Int32
            Get
                Return oMessagesSurServeur.newMessagesDownloadedCount
            End Get
        End Property

        Public ReadOnly Property newMessagesDownloadedSize() As Integer
            Get
                Return oMessagesSurServeur.newMessagesDownloadedSize
            End Get
        End Property

        Public ReadOnly Property newMessagesSize() As Integer
            Get
                Return oMessagesSurServeur.newMessagesSize
            End Get
        End Property

        Public ReadOnly Property isConnected() As Boolean
            Get
                Return booConnexion
            End Get
        End Property

        Public Property login() As String
            Get
                Return strLogin
            End Get
            Set(ByVal value As String)
                If strLogin = value Then Exit Property
                strLogin = value
            End Set
        End Property

        Public Property mdp() As String
            Get
                Return strMdp
            End Get
            Set(ByVal value As String)
                If strMdp = value Then Exit Property
                strMdp = value
            End Set
        End Property

#End Region



        'classes 
#Region "Messages sur le serveur"

        Friend Class MessagesSurServeur
            Inherits Collections.Generic.List(Of MessageSurServeur)

            'méthodes
#Region "Classer Message"

            'Public Function classerMessage(ByVal ms As Mails) As Boolean
            '    Dim oMailSServeur As MessageSurServeur
            '    Dim booTrouve As Boolean

            '    For i As Integer = 0 To Me.Count - 1
            '        'on recupere le mail 
            '        oMailSServeur = Me.Item(i)
            '        booTrouve = False

            '        For Each Ml As Mail In Ms

            '            If Ml.Identifiant = oMailSServeur.IdentifiantMessage Then
            '                booTrouve = True
            '                Exit For
            '            End If

            '        Next

            '        'si on la trouvé le mail n'est pas un nouveau message 
            '        If booTrouve Then
            '            oMailSServeur.NouveauMessage = False
            '        Else
            '            oMailSServeur.NouveauMessage = True
            '        End If

            '    Next


            'End Function

#End Region

            'propriétés
#Region "Nombre de Nouveau Message"

            Public ReadOnly Property newMessagesDownloadedCount() As Int32
                Get
                    Dim j As Int32 = 0
                    Dim nbr As Integer = Me.Count

                    If Me.Count > 0 Then
                        For i As Int32 = 0 To nbr - 1
                            If Me.Item(i).isNew AndAlso Me.Item(i).isDownloaded Then j += 1
                        Next
                    End If


                    Return j
                End Get

            End Property


#End Region

#Region "Nombre de message"

            Public ReadOnly Property newMessagesCount() As Int32
                Get
                    Return Me.Count
                End Get
            End Property


#End Region

#Region "Taille des nouveauMessage"

            Public ReadOnly Property newMessagesDownloadedSize() As Int32
                Get
                    Dim intTaille As Int32 = 0

                    If Me.Count > 0 Then
                        For i As Int32 = 0 To Me.Count - 1
                            If Me.Item(i).isNew AndAlso Me.Item(i).isDownloaded Then intTaille += Me.Item(i).size
                        Next
                    End If

                    Return intTaille
                End Get
            End Property

#End Region

#Region "Taille total des messages"

            Public ReadOnly Property newMessagesSize() As Int32
                Get
                    Dim intTaille As Int32 = 0

                    If Me.Count > 0 Then
                        For i As Int32 = 0 To Me.Count - 1
                            intTaille += Me.Item(i).size
                        Next
                    End If

                    Return intTaille
                End Get
            End Property

#End Region
        End Class

        Public Class MessageSurServeur
            'variables 
            Private intNumeroMessage As Int32
            Private strIdentifiant As String
            Private intTailleMessage As Integer
            Private booNouveauMessage As Boolean = False
            Private _isDownloaded As Boolean = False

#Region "Constructeur"

            Public Sub New()

            End Sub

#End Region



            '''''''''''''''propriétés'''''''''''''''''

#Region "Numero du Message sur le serveur"

            Public Property numeroMessage() As Int32
                Get
                    Return intNumeroMessage
                End Get
                Set(ByVal value As Int32)
                    If intNumeroMessage = value Then Exit Property
                    intNumeroMessage = value
                End Set
            End Property

#End Region

#Region "Identifiant du message"

            Public Property identifiantMessage() As String
                Get
                    Return strIdentifiant
                End Get
                Set(ByVal value As String)
                    If strIdentifiant = value Then Exit Property
                    strIdentifiant = value
                End Set
            End Property

#End Region

#Region "Taille du Message"

            Public Property size() As Integer
                Get
                    Return intTailleMessage
                End Get
                Set(ByVal value As Integer)
                    If intTailleMessage = value Then Exit Property
                    intTailleMessage = value
                End Set
            End Property

#End Region

#Region "Etat du message : Nouveau ou pas "

            Public Property isNew() As Boolean
                Get
                    Return booNouveauMessage
                End Get
                Set(ByVal value As Boolean)
                    If booNouveauMessage = value Then Exit Property
                    booNouveauMessage = value
                End Set
            End Property

#End Region


            Public ReadOnly Property isDownloaded() As Boolean
                Get
                    Return _isDownloaded
                End Get
            End Property

            Public Sub markHasDownloaded()
                _isDownloaded = True
            End Sub

        End Class
#End Region


        Private Sub pop3_BytesReceived(ByVal totalBytes As Long) Handles Me.bytesReceived
            If oMessageSurServeur Is Nothing Then Exit Sub

            RaiseEvent downloading(New POP3DownloadingStatus("Téléchargement du message", curNoMessage, 2, CDbl(totalBytes) / CDbl(oMessageSurServeur.size) * 100, Me.newMessagesCount))
        End Sub
    End Class

End Namespace
