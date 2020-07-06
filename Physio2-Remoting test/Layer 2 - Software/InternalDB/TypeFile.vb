Public Class TypeFile
    Inherits DBItemableBase
    Implements IComparable(Of TypeFile)

    Public Enum baseFileTypeEnum As Integer
        Document = 1
        Image = 2
        Lien = 3
        Son = 4
        Vidéo = 5
    End Enum

    Private _NoTypeFile As Integer = 0
    Private _NbItems As Integer = 0
    Private _FileType As String = ""
    Private _BaseFileType As baseFileTypeEnum
    Private _Extensions As String = ""
    Private _IsInternal, _IsReadOnly, _IsHidden, _SearchInContent, _Printable, _dbSelectable As Boolean

#Region "Properties"
    Public ReadOnly Property noTypeFile() As Integer
        Get
            Return _NoTypeFile
        End Get
    End Property

    Public ReadOnly Property nbItems() As Integer
        Get
            Return _NbItems
        End Get
    End Property

    Public Property fileType() As String
        Get
            Return _FileType
        End Get
        Set(ByVal value As String)
            _FileType = value
        End Set
    End Property

    Public Property baseFileType() As baseFileTypeEnum
        Get
            Return _BaseFileType
        End Get
        Set(ByVal value As baseFileTypeEnum)
            _BaseFileType = value
        End Set
    End Property

    Public ReadOnly Property baseFileTypeName() As String
        Get
            Select Case _BaseFileType
                Case baseFileTypeEnum.Document
                    Return "Document"
                Case baseFileTypeEnum.Image
                    Return "Image"
                Case baseFileTypeEnum.Lien
                    Return "Lien"
                Case baseFileTypeEnum.Son
                    Return "Son"
                Case baseFileTypeEnum.Vidéo
                    Return "Vidéo"
            End Select
        End Get
    End Property

    Public Property extensions() As String
        Get
            Return _Extensions
        End Get
        Set(ByVal value As String)
            _Extensions = value
        End Set
    End Property

    Public Property isInternal() As Boolean
        Get
            Return _IsInternal
        End Get
        Set(ByVal value As Boolean)
            _IsInternal = value
        End Set
    End Property

    Public Property isReadOnly() As Boolean
        Get
            Return _IsReadOnly
        End Get
        Set(ByVal value As Boolean)
            _IsReadOnly = value
        End Set
    End Property

    Public Property isHidden() As Boolean
        Get
            Return _IsHidden
        End Get
        Set(ByVal value As Boolean)
            _IsHidden = value
        End Set
    End Property

    Public Property searchInContent() As Boolean
        Get
            Return _SearchInContent
        End Get
        Set(ByVal value As Boolean)
            _SearchInContent = value
        End Set
    End Property

    Public Property printable() As Boolean
        Get
            Return _Printable
        End Get
        Set(ByVal value As Boolean)
            _Printable = value
        End Set
    End Property

    Public Property dbSelectable() As Boolean
        Get
            Return _dbSelectable
        End Get
        Set(ByVal value As Boolean)
            _dbSelectable = value
        End Set
    End Property
#End Region

    Public Sub New()

    End Sub

    Public Sub New(ByVal data As DBItemableData)
        loadData(data)
    End Sub

    Public Overrides Sub delete()
        DBLinker.getInstance.delDB("FileTypes", "NoFileType", _NoTypeFile, False)
        onDeleted()
        If autoSendUpdateOnDelete Then InternalUpdatesManager.getInstance.sendUpdate("TypesFiles()")
    End Sub

    Public Overrides Sub loadData(ByVal data As DBItemableData)
        Dim curData As DataRow = data.mainData

        _NoTypeFile = curData("NoFileType")
        _NbItems = curData("NbItems")
        _FileType = curData("FileType")
        _BaseFileType = curData("NoBaseFileType")
        _Extensions = curData("Extensions")
        _IsInternal = curData("IsInterne")
        _IsReadOnly = curData("IsReadOnly")
        _IsHidden = curData("IsHidden")
        _SearchInContent = curData("SearchInContent")
        _Printable = curData("Printable")
        _dbSelectable = curData("DBSelectable")
    End Sub

    Public Overrides Sub saveData()
        If _NoTypeFile = 0 Then
            DBLinker.getInstance.writeDB("FileTypes", "FileType,NoBaseFileType,Extensions,IsInterne,IsReadOnly,IsHidden,SearchInContent,Printable,dbSelectable", "'" & _FileType.Replace("'", "''") & "'," & CInt(_BaseFileType) & ",'" & _Extensions.Replace("'", "''") & "','" & _IsInternal & "','" & _IsReadOnly & "','" & _IsHidden & "','" & _SearchInContent & "','" & _Printable & "','" & _dbSelectable & "'")
        Else
            DBLinker.getInstance.updateDB("FileTypes", "FileType='" & _FileType.Replace("'", "''") & "',NoBaseFileType=" & CInt(_BaseFileType) & ",Extensions='" & _Extensions.Replace("'", "''") & "',IsInterne='" & _IsInternal & "',IsReadOnly='" & _IsReadOnly & "',IsHidden='" & _IsHidden & "',SearchInContent='" & _SearchInContent & "',Printable='" & _Printable & "',dbSelectable='" & _dbSelectable & "'", "NoFileType", _NoTypeFile, False)
            onDataChanged()
        End If

        If autoSendUpdateOnSave Then InternalUpdatesManager.getInstance.sendUpdate("TypesFiles()")
    End Sub

    Public Overrides Function toString() As String
        Return _FileType
    End Function

    Public Overrides ReadOnly Property noItemable() As Integer
        Get
            Return Me.noTypeFile
        End Get
    End Property

    Public Function compareTo(ByVal other As TypeFile) As Integer Implements System.IComparable(Of TypeFile).CompareTo
        Return Me.fileType.CompareTo(other.fileType)
    End Function
End Class
