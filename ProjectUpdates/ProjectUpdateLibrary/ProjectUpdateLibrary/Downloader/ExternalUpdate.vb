Namespace Downloader


    Friend MustInherit Class ExternalUpdate
        Implements IExternalUpdate

        Private _newVersion As Integer
        Private curWebClient As Net.WebClient
        Private actionner As ExternalUpdateActionner

        Friend Sub New(ByVal actionner As ExternalUpdateActionner, ByVal curWebClient As Net.WebClient)
            Me.actionner = actionner
            Me.curWebClient = curWebClient
        End Sub

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

        Protected MustOverride ReadOnly Property curVersion() As String
        Protected MustOverride ReadOnly Property updateFolder() As String
        Protected MustOverride ReadOnly Property tmpDir() As String
        Protected MustOverride Sub doPreDownloadActions()
        Protected MustOverride Sub modifyFilesToDownloadList(ByRef filesToDownload As Generic.Dictionary(Of String, String))

        Public MustOverride Sub update() Implements IExternalUpdate.update

        Public Sub download() Implements IExternalUpdate.download
            'Prepare download space
            Dim n As Integer = 0
            IO.Directory.CreateDirectory(tmpDir)
            doPreDownloadActions()

            'Download files
            Dim filesToDownload As New Generic.Dictionary(Of String, String)
            Dim filesToDelete As New Generic.Dictionary(Of String, String)
            For i As Integer = curVersion + 1 To newVersion
                actionner.prepareFiles(i, updateFolder, filesToDownload, filesToDelete, curWebClient)

                modifyFilesToDownloadList(filesToDownload)
            Next i

            If filesToDownload.Count <> 0 Then
                Dim ftd(filesToDownload.Values.Count - 1) As String
                filesToDownload.Values.CopyTo(ftd, 0)

                actionner.downloadFiles(tmpDir, String.Join(vbCrLf, ftd), curWebClient)
            End If

            If filesToDelete.Count <> 0 Then
                'TODO : Manage deleted files (Files could be in usage... SO JOB for these used files should be done by Updater.exe) + Adjust clean to delete these files in BASE folder
            End If
        End Sub
    End Class


End Namespace