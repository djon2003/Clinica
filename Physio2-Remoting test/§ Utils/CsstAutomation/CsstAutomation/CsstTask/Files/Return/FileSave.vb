Public Class FileSave
    Inherits File

    Public Sub New(ByVal xmlFile As String)
        MyBase.New(selectProperFilePath(xmlFile))
    End Sub

    Private Shared Function selectProperFilePath(ByVal xmlFile As String)
        If xmlFile.Substring(0, 2) = "\\" OrElse xmlFile.Substring(1, 2) = ":\" Then 'Absolute path
            If IO.File.Exists(xmlFile) Then Return xmlFile

            Return String.Empty
        End If

        Dim fileName As String = xmlFile.Substring(0, xmlFile.IndexOf(".")) & ".save"
        Dim path As String = Config.current.outputFolder & addSlash(Config.current.outputFolder)
        If IO.File.Exists(path & fileName) Then Return path & fileName
        path &= "verified\"
        If IO.File.Exists(path & fileName) Then Return path & fileName

        Return String.Empty
    End Function


    Protected Overrides ReadOnly Property dataTableName() As String
        Get
            Return String.Empty
        End Get
    End Property

    Protected Overrides ReadOnly Property filePrefix() As String
        Get
            Return String.Empty
        End Get
    End Property

    Protected Overrides Function getExtraResultInfo() As String
        Return String.Empty
    End Function

    Protected Overrides Function getLineObject() As String
        Return String.Empty
    End Function

    Protected Overrides ReadOnly Property nbReports() As Integer
        Get
            Return Me.fileResults.Count
        End Get
    End Property

    Protected Overrides Sub writeLine()

    End Sub
End Class
