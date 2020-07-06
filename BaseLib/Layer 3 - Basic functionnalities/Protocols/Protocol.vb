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

    Public MustInherit Class Protocol

        'Variables 
        Private oTcp As S.TcpClient
        Private tcpLock As New Object
        Private tabDerniereReponse() As String
        Private _lastServerAnswer As String
        Private _defaultEncoding As System.Text.Encoding = System.Text.Encoding.ASCII
        Private _lastServerAnswerBytes As Generic.List(Of Byte)

        'Evenements
        Protected Event bytesReceived(ByVal totalBytes As Long)

        Private _serverName As String = String.Empty
        Private _serverPort As Integer = 0


#Region "Constructeur"

        '//Constructeur 
        ''' <summary>
        ''' Ouvre la connexion avec l'hôte
        ''' </summary>
        ''' <param name="server"></param>
        ''' <param name="Port"></param>
        Public Sub New(ByVal server As String, ByVal port As Integer)
            _serverName = server
            _serverPort = port

            Try
                connect(server, port)
            Catch ex As Exception
                _lastServerAnswer = String.Empty
                External.propagateErrorLog(ex)
            End Try
        End Sub

#End Region

#Region "Properties"
        Protected ReadOnly Property serverName() As String
            Get
                Return _serverName
            End Get
        End Property

        Protected ReadOnly Property serverPort() As Integer
            Get
                Return _serverPort
            End Get
        End Property

        Public Property defaultEncoding() As System.Text.Encoding
            Get
                Return _defaultEncoding
            End Get
            Set(ByVal value As System.Text.Encoding)
                _defaultEncoding = value
            End Set
        End Property
#End Region


        '''''''''''''''''''''''  Méthodes  ''''''''''''''''''''''''

        'Protected
#Region "Lit la réponse du serveur"

        Private Sub cleanTmpBytes(ByRef tmpBytes() As Byte, ByVal initialNbBytesToRead As Integer, ByRef nbBytesToRead As Integer)
            If initialNbBytesToRead <> nbBytesToRead Then
                ReDim Preserve tmpBytes(nbBytesToRead - 1)
            End If

            'Dim nbToKeep As Integer = tmpBytes.Length
            'For i As Integer = 0 To tmpBytes.Length - 1
            '    If tmpBytes(i) = 0 Then
            '        nbToKeep = i
            '        Exit For
            '    End If
            'Next i

            ''Adjust
            'If nbToKeep <> tmpBytes.Length Then
            '    ReDim Preserve tmpBytes(nbToKeep - 1)
            '    nbBytesToRead = nbToKeep
            'End If
        End Sub

        Protected Sub getServerAnswer(Optional ByVal encoding As System.Text.Encoding = Nothing, Optional ByVal stopOnLine As String = "")
            If oTcp Is Nothing Then Exit Sub

            If encoding Is Nothing Then encoding = _defaultEncoding

            Dim totalBytes As Long = 0
            Dim sb As New System.Text.StringBuilder()

            'Le flux à lire 
            _lastServerAnswerBytes = New Generic.List(Of Byte)
            Dim nbBytesToRead As Integer = 1000 * encoding.GetMaxByteCount(1)
            Dim initialNbBytesToRead As Integer = nbBytesToRead
            Dim tmpBytes(nbBytesToRead - 1) As Byte
            nbBytesToRead = oTcp.GetStream.Read(tmpBytes, 0, initialNbBytesToRead)

            cleanTmpBytes(tmpBytes, initialNbBytesToRead, nbBytesToRead)

            totalBytes += nbBytesToRead

            _lastServerAnswer = encoding.GetString(tmpBytes).Replace(Chr(0), "")
            _lastServerAnswerBytes.AddRange(tmpBytes)
            sb.Append(_lastServerAnswer)

            Dim firstLine As String = _lastServerAnswer.Replace(vbCr, "").Replace(vbLf, "").Replace(Chr(0), "")
            Dim n As Integer = 0
            Dim endIndex As Integer = -1
            Dim lines() As String = {""}

            If stopOnLine <> "" Then
                Dim done As Boolean = False
                Do
                    Dim nbTriesToRead As Integer = 0

                    While oTcp.GetStream.DataAvailable OrElse oTcp.Available > 0 'OrElse (firstLine.EndsWith(".") = False AndAlso lines(lines.Length - 1).Trim <> ".")
                        If oTcp.Available = 0 Then Continue While
                        nbBytesToRead = initialNbBytesToRead
                        ReDim tmpBytes(initialNbBytesToRead - 1)
                        If nbBytesToRead > oTcp.Available Then nbBytesToRead = oTcp.Available

                        Dim readError As Boolean = False
                        Try
                            nbBytesToRead = oTcp.GetStream.Read(tmpBytes, 0, nbBytesToRead)
                        Catch ex As ArgumentOutOfRangeException
                            readError = True
                            nbTriesToRead += 1
                            Console.WriteLine("Protocol read error :" & nbTriesToRead)

                            If nbTriesToRead > 3 Then Throw ex
                        End Try

                        If readError Then Continue While

                        cleanTmpBytes(tmpBytes, initialNbBytesToRead, nbBytesToRead)

                        _lastServerAnswerBytes.AddRange(tmpBytes)
                        _lastServerAnswer = encoding.GetString(tmpBytes, 0, nbBytesToRead).Replace(Chr(0), "")
                        sb.Append(_lastServerAnswer)
                        totalBytes += nbBytesToRead
                        lines = _lastServerAnswer.Split(vbCrLf)

                        RaiseEvent bytesReceived(totalBytes)

                        n += 1
                    End While

                    endIndex = sb.ToString().IndexOf(vbCrLf & stopOnLine)

                    While endIndex <> -1 AndAlso done = False
                        If endIndex + 2 + stopOnLine.Length = sb.Length OrElse sb.ToString(endIndex + 2 + stopOnLine.Length, sb.Length - endIndex - 2 - stopOnLine.Length).StartsWith(vbCrLf) Then
                            'Found end !!
                            done = True
                        Else
                            endIndex = sb.ToString().IndexOf(vbCrLf & stopOnLine, endIndex + 1)
                        End If
                    End While
                Loop Until done
            End If

            _lastServerAnswer = sb.ToString
            If stopOnLine <> "" Then _lastServerAnswer = _lastServerAnswer.Substring(0, endIndex)

        End Sub

#End Region

#Region "envoi une commande au serveur, vbCrLf est ajouté automatiquement "
        Protected Sub sendServerCommand(ByVal command As String, Optional ByVal readAnswer As Boolean = True, Optional ByVal encoding As System.Text.Encoding = Nothing, Optional ByVal stopOnLine As String = ".")
            If oTcp Is Nothing Then Exit Sub

            command &= vbCrLf

            'converti la commande (string) en tableau d'octet
            Dim buffer() As Byte = System.Text.Encoding.ASCII.GetBytes(command.ToCharArray)

            'Lock to ensure only one thread at a time
            If System.Threading.Monitor.TryEnter(tcpLock) = False Then
                System.Threading.Monitor.Wait(tcpLock)
            Else
                System.Threading.Monitor.Enter(tcpLock)
            End If

            oTcp.GetStream.Write(buffer, 0, buffer.Length)

            If readAnswer Then
                'on lit la reponse du serveur 
                getServerAnswer(encoding, stopOnLine)
            End If


            System.Threading.Monitor.Exit(tcpLock)
            System.Threading.Monitor.Pulse(tcpLock)

        End Sub
#End Region

        'Public

        Public Sub connect(ByVal server As String, ByVal port As Integer)
            'nouvelle connexion client pour tcp
            oTcp = New S.TcpClient()
            Try
                'on se connecte
                oTcp.Connect(server, port)

                'Connection au flux d'information 
                'NetStream récupere la réponse du serveur 
                Dim netStream As S.NetworkStream = oTcp.GetStream

                'si le flux ne contient rien 
                If netStream Is Nothing Then
                    oTcp.Close()
                    Throw New Exception("Impossible d'obtenir une réponse du serveur")
                End If

                'on va chercher la réponse du serveur
                getServerAnswer()

            Catch ex As Exception
                oTcp.Close()
                Throw New Exception("Erreur de connexion avec le serveur : " & ex.Message, ex)
            End Try
        End Sub

#Region "Deconnection"
        Public Sub disconnect()
            If oTcp Is Nothing Then Exit Sub

            sendServerCommand("QUIT", , , "")
            oTcp.Close()
            oTcp = Nothing
        End Sub
#End Region


        '''''''''''''''''''''''  Propriétés  ''''''''''''''''''''''''

        'en Lecture Seule 
#Region " Dernieres Reponses string "

        Public ReadOnly Property lastServerAnswer() As String
            Get
                Return _lastServerAnswer
            End Get
        End Property

        Protected ReadOnly Property lastServerAnswerBytes() As Generic.List(Of Byte)
            Get
                Return _lastServerAnswerBytes
            End Get
        End Property

#End Region


    End Class

End Namespace
