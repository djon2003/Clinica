Imports S = System.Net.Sockets
Imports System.IO

#Region "CopyRight & Co"

'*********************************************************************************
'**  File: Protocol.vb
'**  Name: Protocols.protocol
'**  Desc: classe abstraite servant de base pour les communications par sockets (pop smtp, imap ...) 
'**  
'**
'**  Auth: Quentin besson basé sur les travaux de Richard Clark
'**  Date: 01/02/2006
'*********************************************************************************
'**  Change History
'*********************************************************************************
'**  Date:       Author:          Description:
'**  ----------  --------------   -------------------------------------------
'**  01/02/2006  Quentin Besson   Création de la classe
'**  09/04/2006  Quentin Besson   optimisation de la lecture de la reponse du serveur
'**  10/04/2006  Quentin Besson   ajout reponse en arraylist
'**  
'*********************************************************************************

#End Region

Namespace Protocols

    Public MustInherit Class protocol

        'Variables 
        Private oSocket As S.Socket
        Private TabDerniereReponse() As String
        Private strDerniereReponse As String
        Private collDerniereReponse As ArrayList

        'Evenements
        Public Event Erreur(ByVal message As String)
        Protected Event BytesReceived(ByVal totalBytes As Long)


#Region "Constructeur"
        Public Sub New()

        End Sub

        '//Constructeur 
        ''' <summary>
        ''' Ouvre la connexion avec l'hôte
        ''' </summary>
        ''' <param name="Serveur"></param>
        ''' <param name="Port"></param>
        Public Sub New(ByVal Serveur As String, ByVal Port As Integer)
            Connexion(Serveur, Port)
        End Sub

#End Region


        '''''''''''''''''''''''  Méthodes  ''''''''''''''''''''''''

        'Protected
#Region "Lit la réponse du serveur"

        Protected Sub ReponseServeur(Optional ByVal encodage As System.Text.Encoding = Nothing)
            If oSocket Is Nothing Then Exit Sub

            If encodage Is Nothing Then encodage = System.Text.Encoding.ASCII

            Dim strReponse As New System.Text.StringBuilder()
            Dim collReponse As New ArrayList
            Dim totalBytes As Long = 0

            'Le flux à lire 
            Dim bytesReceived As Integer = 1
            Dim sb As New Text.StringBuilder
            Dim buffer(1024) As Byte
            While bytesReceived > 0 AndAlso oSocket.Connected
                bytesReceived = oSocket.Receive(buffer, 1024, Net.Sockets.SocketFlags.None)
                If bytesReceived > 0 Then
                    totalBytes += bytesReceived
                    sb.Append(encodage.GetString(buffer, 0, bytesReceived))
                    Me.strDerniereReponse = sb.ToString
                    If Me.strDerniereReponse.StartsWith("421") Then
                        Me.EnvoiCommande("HELO videotron.com")
                        Exit Sub
                    End If
                    '421 Cannot connect to SMTP server 24.201.245.37 (24.201.245.37:25), connect error 10060

                    RaiseEvent BytesReceived(totalBytes)
                End If
            End While

            Me.strDerniereReponse = sb.ToString
        End Sub

#End Region

#Region "envoi une commande au serveur, vbCrLf est ajouté automatiquement "
        Protected Sub EnvoiCommande(ByVal Commande As String, Optional ByVal LireReponse As Boolean = True, Optional ByVal encodage As System.Text.Encoding = Nothing)
            If oSocket Is Nothing Then Exit Sub

            Commande &= vbCrLf 'Tried with vbCr or vbLf.. still receiving same error ('421 Cannot connect to SMTP server 24.201.245.37 (24.201.245.37:25), connect error 10060)

            'IO.File.AppendAllText("C:\temp\commands.txt", Commande)

            'converti la commande (string) en tableau d'octet
            Dim Buffer() As Byte = System.Text.Encoding.ASCII.GetBytes(Commande.ToCharArray)

            REM Tried to add synchronisation.. still not
            If System.Threading.Monitor.TryEnter(oSocket) = False Then
                System.Threading.Monitor.Wait(oSocket)
                '   IO.File.AppendAllText("C:\temp\commands.txt", Commande & "---WAITING")
            Else
                System.Threading.Monitor.Enter(oSocket)
            End If
            Try
                oSocket.Send(Buffer, Net.Sockets.SocketFlags.None)

                If LireReponse Then
                    System.Threading.Thread.Sleep(200)
                    'on lit la reponse du serveur 
                    ReponseServeur(encodage)
                End If

            Catch ex As Exception

                RaiseEvent Erreur(ex.Message)

            End Try
            System.Threading.Monitor.Exit(oSocket)
            System.Threading.Monitor.Pulse(oSocket)

        End Sub
#End Region

        'Public

        Public Sub Connexion(ByVal Serveur As String, ByVal Port As Integer)
            Dim IPhst As Net.IPHostEntry = Net.Dns.GetHostEntry(Serveur)
            Dim endPt As Net.IPEndPoint = New Net.IPEndPoint(IPhst.AddressList(0), 25)

            'nouvelle connexion client pour tcp
            oSocket = New S.Socket(endPt.AddressFamily, Net.Sockets.SocketType.Stream, Net.Sockets.ProtocolType.Tcp)
            Try
                'on se connecte
                oSocket.Connect(endPt)

                'Connection au flux d'information 
                'NetStream récupere la réponse du serveur 
                'Dim NetStream As S.NetworkStream = osocket.

                'si le flux ne contient rien 
                'If NetStream Is Nothing Then
                ' oTcp.Close()
                'RaiseEvent Erreur("Impossible d'obtenir une réponse du serveur")
                'End If

                'on va chercher la réponse du serveur
                'ReponseServeur()

            Catch ex As Exception
                oSocket.Close()
                RaiseEvent Erreur("Erreur de connexion avec le serveur : " & ex.Message)
            End Try
        End Sub

#Region "Deconnection"
        Public Sub Deconnection()
            If oSocket Is Nothing Then Exit Sub

            EnvoiCommande("QUIT")
            oSocket.Close()
            oSocket = Nothing
        End Sub
#End Region


        '''''''''''''''''''''''  Propriétés  ''''''''''''''''''''''''

        'en Lecture Seule 
#Region " Dernieres Reponses string "

        Public ReadOnly Property DernieresReponses() As String
            Get
                Return strDerniereReponse
            End Get
        End Property
#End Region

#Region " Derniéres Reponses ArrayList"
        Public ReadOnly Property DernieresReponseArray() As ArrayList
            Get
                Return collDerniereReponse
            End Get
        End Property

#End Region


    End Class

End Namespace
