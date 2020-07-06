Imports CI.ProjectUpdates.ProjectUpdateLibrary.Creator

Module Main

    Const DEFAULT_SETTINGS_FILE = "updates.xml"
    Const SETTINGS_FILE_ARG_KEYWORD = "-file"

    Private basePath As String = ""
    Private settingsFile As String = ""
    Private softNamesToUpload As New List(Of String)
   
    Private Sub askUploading()
        'Ask if update should be upload
        Console.Clear()
        Console.WriteLine("Updates ready to upload for the following softwares :")
        Console.WriteLine(String.Join(vbCrLf, softNamesToUpload.ToArray()))

        Console.WriteLine()
        Console.Write("Do you want to upload them now (Y/N) ? ")
        Dim uploadAnswer As String = Console.ReadLine()
        If uploadAnswer = "" OrElse (uploadAnswer.ToLower.Substring(0, 1) <> "o" AndAlso uploadAnswer.ToLower.Substring(0, 1) <> "y") Then End

        Console.Clear()
    End Sub

    Private Sub onStopCreation(ByVal reason As String)
        stopUpdater(reason)
    End Sub

    Sub Main()
        AddHandler ExternalUpdateType.stopCreation, AddressOf onStopCreation

        basePath = My.Application.Info.DirectoryPath
        basePath &= IIf(basePath.EndsWith("\"), "", "\")

        manageSoftwareArgs()
        Dim myUpdates As Generic.List(Of ExternalUpdateType) = getUpdatesToDo()

        Try
            'Ensure at least one update exists, but do it for all because it is a required function
            Dim haveToUpdate As Boolean = False
            For Each curUpdate As ExternalUpdateType In myUpdates
                If curUpdate.isUpdateExists() Then haveToUpdate = True
            Next
            If Not haveToUpdate Then stopUpdater("No update to do")

            'Create updates
            For Each curUpdate As ExternalUpdateType In myUpdates
                curUpdate.create()
                For Each curSoftName As String In curUpdate.getSoftwareNamesToUpdate()
                    If softNamesToUpload.Contains(curSoftName) Then Continue For

                    softNamesToUpload.Add(curSoftName)
                Next
            Next

            'Upload updates created
            askUploading()
            For Each curUpdate As ExternalUpdateType In myUpdates
                curUpdate.upload()
            Next

            'Upgrade versions
            For Each curUpdate As ExternalUpdateType In myUpdates
                curUpdate.upgradeVersion()
            Next

            'Clean updates
            For Each curUpdate As ExternalUpdateType In myUpdates
                curUpdate.clean()
            Next

        Catch ex As CI.ProjectUpdates.ProjectUpdateLibrary.ExternalUpdateException
            Console.WriteLine()
            Console.WriteLine()
            Console.WriteLine("Error :")
            Console.WriteLine(ex.Message)
        Catch ex As Exception
            Console.WriteLine()
            Console.WriteLine()
            Console.WriteLine("Unknown error :")
            Console.WriteLine(ex.Message)
            Console.WriteLine()
            Console.WriteLine()
            Console.WriteLine("Stack trace :")
            Console.WriteLine(ex.StackTrace)
        End Try

        Console.WriteLine("Ended")
        Console.ReadKey()
    End Sub

    Public Sub stopUpdater(ByVal reason As String)
        Console.Clear()
        Console.WriteLine(reason)
        Console.ReadKey()
        End
    End Sub

    Private Function getUpdatesToDo() As Generic.List(Of ExternalUpdateType)
        Dim myUpdates As New Generic.List(Of ExternalUpdateType)
        Dim myUpdate As ExternalUpdateType
        Dim mySettingsData As New DataSet
        mySettingsData.ReadXml(settingsFile)

        For Each curTable As DataTable In mySettingsData.Tables
            myUpdate = ExternalUpdateType.getUpdateClass(curTable)
            If myUpdate Is Nothing Then
                stopUpdater("Table """ & curTable.TableName & """ is not supported. View help to know the supported names.")
            End If
            myUpdates.Add(myUpdate)
        Next

        Return myUpdates
    End Function

    Private Sub manageSoftwareArgs()
        'Get args
        Dim n As Integer = 0
        For Each curArg As String In Environment.GetCommandLineArgs()
            If curArg.ToLower = SETTINGS_FILE_ARG_KEYWORD Then
                If (n + 1) < Environment.GetCommandLineArgs().Length Then
                    settingsFile = Environment.GetCommandLineArgs(n + 1)
                End If
            End If
            n += 1
        Next

        'Set default args
        If settingsFile = "" Then settingsFile = basePath & DEFAULT_SETTINGS_FILE

        'Ensure values passed are good
        If IO.File.Exists(settingsFile) = False Then stopUpdater("Settings file """ & settingsFile & """ doesn't exist. Please create a valid settings file.")
    End Sub

End Module
