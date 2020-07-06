Public Class TypesFilesManager
    Inherits DBItemableManagerBase(Of TypesFilesManager, TypeFile)

    Protected Sub New()
        load()
    End Sub

    Public Overrides Sub load()
        MyBase.clear()

        Dim curTypes As DataSet = DBLinker.getInstance.readDBForGrid("FileTypes", "(SELECT COUNT(*) FROM DBItems WHERE DBItems.NoFileType=FileTypes.NoFileType) AS NbItems,*")
        If curTypes Is Nothing Then Exit Sub
        With curTypes.Tables(0).Rows
            For i As Integer = 0 To .Count - 1
                addItemable(New TypeFile(New DBItemableData(.Item(i))))
            Next i
        End With
    End Sub

    Public Overloads Function getItemable(ByVal fileType As String) As TypeFile
        For Each curTF As TypeFile In getItemables()
            If curTF.fileType = fileType Then Return curTF
        Next

        Return Nothing
    End Function

    Public Function getTypeFileFromExt(ByVal myExtension As String) As TypeFile
        If myExtension = "" Then Return Nothing

        For Each curType As TypeFile In getItemables()
            If curType.extensions = "" Then Continue For

            Dim myExts() As String = Split(curType.extensions, ";")
            If searchArray(myExts, myExtension, SearchType.ExactMatch) >= 0 Then Return curType
        Next curType

        Return Nothing
    End Function

    Public Shared Sub addTypeFile(ByVal name As String)
        Dim newTypeFile As New TypeFile()
        newTypeFile.fileType = name
        newTypeFile.baseFileType = TypeFile.baseFileTypeEnum.Document
        newTypeFile.saveData()
        'getInstance().addItemable(newTypeFile)

        InternalUpdatesManager.getInstance.sendUpdate("TypesFiles()")
    End Sub

    Public Shared Function getGeneralFiltering() As String
        Dim types As Generic.List(Of TypeFile) = TypesFilesManager.getInstance.getItemables()
        If types Is Nothing OrElse types.Count = 0 Then Return "Tous les fichiers|*.*"

        Dim curFiltering As String = ""
        Dim allExts As String = ""
        types.Sort()
        For Each CurType As TypeFile In types
            allExts &= "*." & CurType.extensions.Replace(";", ";*.") & ";"
            curFiltering &= "|" & CurType.toString & "|*." & CurType.extensions.Replace(";", ";*.")
        Next CurType
        If allExts.EndsWith(";") Then allExts = allExts.Substring(0, allExts.Length - 1)
        curFiltering = "Tous|" & allExts & curFiltering

        Return curFiltering
    End Function

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.function <> "TypesFiles" Then Exit Sub

        load()
    End Sub

    Protected Overrides Sub sendUpdate()
        InternalUpdatesManager.getInstance.sendUpdate("TypesFiles()")
    End Sub
End Class
