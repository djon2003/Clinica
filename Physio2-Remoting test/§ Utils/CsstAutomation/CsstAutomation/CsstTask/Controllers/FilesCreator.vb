Public Class FilesCreator

    Private filesResults As New List(Of FileResult)
    Private outPath As String
    Private data As New DataSet
    Private currentProgession As Integer = 0
    Private totalLines As Integer
    Private outHtml As New System.Text.StringBuilder()

    Private _nbProcessed As Integer = 0
    Private _nbErrors As Integer = 0

#Region "Constructors"
    Public Sub New(ByVal outPath As String, ByVal data As DataSet)
        Me.outPath = outPath
        IO.Directory.CreateDirectory(outPath) 'Ensure directory exists
        Me.data = data
    End Sub

    Public Sub New(ByVal outPath As String, ByVal inPath As String)
        Me.New(outPath, New DataSet())
        If IO.File.Exists(inPath) = False Then Throw New FilesCreationException("inPath xml file doesn't exist")
        data.ReadXml(inPath)
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property results() As List(Of FileResult)
        Get
            Return filesResults
        End Get
    End Property

    Public ReadOnly Property nbErrors() As Integer
        Get
            Return _nbErrors
        End Get
    End Property

    Public ReadOnly Property nbProcessed() As Integer
        Get
            Return _nbProcessed
        End Get
    End Property
#End Region

    Public Event creationProgressed(ByVal sender As Object, ByVal progression As Double)

    Private Sub _creationProgressed(ByVal sender As Object, ByVal e As EventArgs)
        currentProgession += 1
        RaiseEvent creationProgressed(Me, currentProgession * 100 / totalLines)
    End Sub

    Public Sub createTestFile()
        Dim initFile As New FileInitialReports(Config.current.outputFolder, data)
        initFile.createTestFile()
    End Sub

    Public Sub createFiles()
        If data.Tables("clinic") Is Nothing Then Throw New FilesCreationException("clinic table doesn't exist into the xml file")

        'Delete files with ".tomark" extension and their ".save" if it exists.. 
        'The ".cst" files are marked, but not transfered
        Dim filesToDelete() As String = IO.Directory.GetFiles(outPath, "*.tomark")
        For Each curFile As String In filesToDelete
            Dim saveFile As String = curFile.Replace(".tomark", ".save")
            IO.File.Delete(curFile)
            If IO.File.Exists(saveFile) Then IO.File.Delete(saveFile)
        Next

        'Prepare objects needed to create files
        Dim files As New Generic.List(Of File)
        For Each curTable As DataTable In data.Tables
            If curTable.Rows.Count = 0 Then Continue For

            Dim curFile As File = File.createType(curTable.TableName, outPath, data)
            If curFile Is Nothing Then Continue For

            totalLines += curTable.Rows.Count
            files.Add(curFile)
            AddHandler curFile.creationProgressed, AddressOf _creationProgressed
        Next

        'Create each file needed
        For Each curFile As File In files
            curFile.createFile(filesResults)
            filesResults.AddRange(curFile.fileResults)
            _nbErrors += curFile.nbErrors
            _nbProcessed += curFile.nbProcessed
        Next
    End Sub
End Class
