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
        Private oTcp As S.TcpClient
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
            If oTcp Is Nothing Then Exit Sub

            If encodage Is Nothing Then encodage = System.Text.Encoding.UTF8

            Dim strReponse As New System.Text.StringBuilder()
            Dim collReponse As New ArrayList
            Dim totalBytes As Long = 0

            'Le flux à lire 
            'Dim LireReponse As New StreamReader(oTcp.GetStream, System.Text.UTF7Encoding.UTF7, True)
            REM Using UTF-7 correct some accent problems, but bugs almost all base64 strings
            'Dim LireReponse As New StreamReader(oTcp.GetStream, encodage, True)
            'Dim LireReponse2 As New BinaryReader(oTcp.GetStream)

            'Threading.Thread.Sleep(500)

            'on recupere la pemiere ligne 
            'Dim reponse As String = System.Text.Encoding.UTF8.GetString(LireReponse2.ReadBytes(4096))
            'strReponse.Append(reponse)
            'collReponse.Add(LireReponse.ReadLine)
            'strReponse.Append(collReponse.Item(collReponse.Count - 1).ToString)
            'Dim curBytes As New Generic.List(Of Byte)(4096)
            Dim tmpBytes(4096) As Byte
            oTcp.GetStream.Read(tmpBytes, 0, 4096)


            strDerniereReponse = Text.Encoding.ASCII.GetString(tmpBytes)
            strDerniereReponse = strDerniereReponse.Replace(Chr(0), "").Trim()
            'If strDerniereReponse.LastIndexOf(vbCrLf) <> -1 Then strDerniereReponse = strDerniereReponse.Substring(0, strDerniereReponse.LastIndexOf(vbCrLf))
            If strDerniereReponse <> String.Empty Then Console.WriteLine("TCP-Read:" & strDerniereReponse)
            'curBytes.AddRange(tmpBytes)

            'Dim n As Integer = 0
            'Do While LireReponse.Peek <> -1
            '    'Do While oTcp.GetStream.DataAvailable
            '    strReponse.Append(vbCrLf)
            '    collReponse.Add(LireReponse.ReadLine)
            '    strReponse.Append(collReponse.Item(collReponse.Count - 1).ToString)
            '    totalBytes += LireReponse.CurrentEncoding.GetByteCount(collReponse.Item(collReponse.Count - 1).ToString.ToCharArray) 'Counts bytes

            '    RaiseEvent BytesReceived(totalBytes)

            '    'Application.DoEvents()
            '    n += 1
            'Loop

            'on met à jour les propriétés
            collDerniereReponse = collReponse
            'Dim test As String = System.Text.Encoding.UTF7.GetString(System.Text.Encoding.Convert(System.Text.Encoding.UTF8, System.Text.Encoding.UTF7, curBytes.ToArray))
            'strDerniereReponse = System.Text.Encoding.UTF8.GetString(curBytes.ToArray)


            REM TESTED CODE.. to get the whole thing in bytes directly.. works.. but really really slow.. loosing connection
            'Dim LireReponse As New BinaryReader(oTcp.GetStream, System.Text.Encoding.UTF8)

            'Threading.Thread.Sleep(500)

            ''on recupere la pemiere ligne 
            ''strReponse = LireReponse.ReadByte
            'collReponse.Add(strReponse)
            'Dim curBytes As New Generic.List(Of Byte)
            'Dim lastByte As Integer = LireReponse.ReadByte
            'curBytes.Add(lastByte)
            'Dim bytesBuffer As Byte() = {}
            'Dim n As Integer = 0
            'Try
            '    Do While bytesBuffer.Length = 5 Or bytesBuffer.Length = 0
            '        'strReponse &= vbCrLf
            '        'collReponse.Add(LireReponse.ReadLine)
            '        'strReponse &= collReponse.Item(collReponse.Count - 1).ToString
            '        'totalBytes += LireReponse.CurrentEncoding.GetByteCount(collReponse.Item(collReponse.Count - 1).ToString.ToCharArray) 'Counts bytes
            '        bytesBuffer = LireReponse.ReadBytes(5)
            '        curBytes.AddRange(bytesBuffer)
            '        lastByte = curBytes(curBytes.Count - 1)
            '        'RaiseEvent BytesReceived(totalBytes)
            '        REM MyMainWin.Text = collReponse.Count REM
            '        Application.DoEvents()
            '        n += 1

            '    Loop
            'Catch ex As System.IO.EndOfStreamException
            'End Try

            'strReponse = System.Text.Encoding.UTF8.GetString(curBytes.ToArray)
        End Sub

#End Region

#Region "envoi une commande au serveur, vbCrLf est ajouté automatiquement "
        Protected Sub EnvoiCommande(ByVal Commande As String, Optional ByVal LireReponse As Boolean = True, Optional ByVal encodage As System.Text.Encoding = Nothing)
            If oTcp Is Nothing Then Exit Sub

            Commande &= vbCrLf

            'IO.File.AppendAllText("C:\temp\commands.txt", Commande)

            'converti la commande (string) en tableau d'octet
            Dim Buffer() As Byte = System.Text.Encoding.ASCII.GetBytes(Commande.ToCharArray)

            REM Tried to add synchronisation.. still not
            If System.Threading.Monitor.TryEnter(oTcp.GetStream) = False Then
                System.Threading.Monitor.Wait(oTcp.GetStream)
                '   IO.File.AppendAllText("C:\temp\commands.txt", Commande & "---WAITING")
            Else
                System.Threading.Monitor.Enter(oTcp.GetStream)
            End If
            Try
                oTcp.GetStream.Write(Buffer, 0, Buffer.Length)

                If LireReponse Then
                    'System.Threading.Thread.Sleep(200)
                    'on lit la reponse du serveur 
                    ReponseServeur(encodage)
                End If

            Catch ex As Exception

                RaiseEvent Erreur(ex.Message)

            End Try
            System.Threading.Monitor.Exit(oTcp.GetStream)
            System.Threading.Monitor.Pulse(oTcp.GetStream)

        End Sub
#End Region

        'Public

        Public Sub Connexion(ByVal Serveur As String, ByVal Port As Integer)
            'nouvelle connexion client pour tcp
            oTcp = New S.TcpClient
            Try
                'on se connecte
                oTcp.NoDelay = True
                oTcp.Connect(Serveur, Port)

                'Connection au flux d'information 
                'NetStream récupere la réponse du serveur 
                Dim NetStream As S.NetworkStream = oTcp.GetStream

                'si le flux ne contient rien 
                If NetStream Is Nothing Then
                    oTcp.Close()
                    RaiseEvent Erreur("Impossible d'obtenir une réponse du serveur")
                End If

                'on va chercher la réponse du serveur
                ReponseServeur()

            Catch ex As Exception
                oTcp.Close()
                RaiseEvent Erreur("Erreur de connexion avec le serveur : " & ex.Message)
            End Try
        End Sub

#Region "Deconnection"
        Public Sub Deconnection()
            If oTcp Is Nothing Then Exit Sub

            EnvoiCommande("QUIT")
            oTcp.Close()
            oTcp = Nothing
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
