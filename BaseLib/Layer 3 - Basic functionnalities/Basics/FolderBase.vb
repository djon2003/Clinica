Public MustInherit Class FolderBase
    Inherits DBItemableBase
    Implements IPropertiesManagable

    Private _NoFolder As Integer = 0
    Private _Folder As String = ""
    Protected oldFolder As String = ""
    Private _NoUser As Integer = 0
    Protected oldNoUser As Integer = 0
    Private curFolderType As FolderType = FolderType.DB
    Private properties As New Hashtable
    Private _IconIndex As Integer = 0
    Private _IconSelectedIndex As Integer = 1
    Private _lastPath As String = ""
    Private _bold As Boolean = False
    Private _nbNewItems As Integer = 0


    Public Enum FolderType
        DB = 0
        Mail = 1
        Contact = 2
    End Enum

#Region "Propriétés"
    Public ReadOnly Property lastPath() As String
        Get
            Return _lastPath
        End Get
    End Property

    Public Property iconSelectedIndex() As Integer
        Get
            Return _IconSelectedIndex
        End Get
        Set(ByVal value As Integer)
            _IconSelectedIndex = value
        End Set
    End Property

    Public Property iconIndex() As Integer
        Get
            Return _IconIndex
        End Get
        Set(ByVal value As Integer)
            _IconIndex = value
        End Set
    End Property

    Public Property noFolder() As Integer
        Get
            Return _NoFolder
        End Get
        Set(ByVal value As Integer)
            _NoFolder = value
        End Set
    End Property

    Public Property noUser() As Integer
        Get
            Return _NoUser
        End Get
        Set(ByVal value As Integer)
            _NoUser = value
        End Set
    End Property

    Public Property folder() As String
        Get
            Return _Folder
        End Get
        Set(ByVal value As String)
            _Folder = value
        End Set
    End Property

    Public Property bold() As Boolean
        Get
            Return _bold
        End Get
        Set(ByVal value As Boolean)
            _bold = value
        End Set
    End Property

    Public ReadOnly Property nbUnreadItems() As Integer
        Get
            Return _nbNewItems
        End Get
    End Property

#End Region

    Private Sub New()

    End Sub

    Protected Sub New(ByVal curFolderType As FolderType)
        Me.curFolderType = curFolderType
    End Sub

    Protected Sub New(ByVal curFolderType As FolderType, ByVal data As DBItemableData)
        loadData(data)
    End Sub


    Public Overrides Sub loadData(ByVal data As DBItemableData)
        Dim curData As DataRow = data.mainData
        _NoUser = IIf(curData("NoUser") Is DBNull.Value, 0, curData("NoUser"))
        oldNoUser = _NoUser
        _NoFolder = curData(Me.getNoFolderColumnName)
        _Folder = curData(Me.getFolderColumnName)
        oldFolder = _Folder
        _lastPath = FolderBase.getPath(_NoUser, oldFolder)
        properties = PropertiesHelper.transformProperties(curData("FolderProperties").ToString, True)
        loadProperties(properties)
    End Sub

    Public Overrides Sub saveData()
        Dim dataChanged As Boolean = False
        saveProperties(properties)
        If noFolder = 0 Then
            DBLinker.getInstance.writeDB(Me.getTableName, "NoUser," & Me.getFolderColumnName & ",FolderProperties", IIf(noUser = 0, "null", noUser) & ",'" & _Folder.Replace("'", "''") & "','" & Me.propertiesToString.Replace("'", "''") & "'", , , , noFolder)
        Else
            DBLinker.getInstance.updateDB(Me.getTableName, "NoUser=" & IIf(noUser = 0, "null", noUser) & "," & Me.getFolderColumnName & "='" & _Folder.Replace("'", "''") & "',FolderProperties='" & Me.propertiesToString.Replace("'", "''") & "'", Me.getNoFolderColumnName, noFolder, False)

            If folder <> oldFolder Or noUser <> oldNoUser Then
                DBLinker.getInstance.updateDB(Me.getTableName, "NoUser=" & IIf(noUser = 0, "null", noUser) & "," & Me.getFolderColumnName & "='" & _Folder.Replace("'", "''") & "' + SUBSTRING(" & Me.getFolderColumnName & "," & (oldFolder.Length + 1) & ",LEN(" & Me.getFolderColumnName & ")-" & (oldFolder.Length) & ")", Me.getFolderColumnName, "'" & oldFolder.Replace("'", "''") & "\%' AND NoUser" & IIf(oldNoUser = 0, " IS NULL", "=" & oldNoUser), False, "LIKE")
                dataChanged = True
            End If
        End If

        If dataChanged Then onDataChanged()

        'Old values have to be affected after dataChanged event
        oldFolder = _Folder
        oldNoUser = _NoUser
        _lastPath = Me.toString

        'TODO: use If autoSendUpdateOnSave Then 
    End Sub

    Protected MustOverride Function getNoFolderColumnName() As String
    Protected MustOverride Function getFolderColumnName() As String
    Protected MustOverride Function getTableName() As String

    Protected Shared Function exists(ByVal folderPath As String, ByVal tableName As String, ByVal folderColumnName As String) As Boolean
        Dim noUser As Integer = 0
        Dim realPath As String = getRealPath(folderPath, noUser)

        Return DBLinker.getInstance.readOneDBField(tableName, "COUNT(*)", "WHERE NoUser" & IIf(noUser = 0, " IS NULL", "=" & noUser) & " AND " & folderColumnName & "='" & realPath.Replace("'", "''") & "'")(0) <> 0
    End Function

    Public Shared Function getRealPath(ByVal path As String, ByRef noUser As Integer) As String
        If path.StartsWith("G") = False And path.StartsWith("U") = False Then Return ""
        If path = "Utilisateurs" Then noUser = -1 : Return ""

        Dim realPath As String = ""
        If path.StartsWith("G") Then
            If path.Length > 8 Then realPath = path.Substring(9)
            noUser = 0
        Else
            Dim sFullPath() As String = path.Split(New Char() {"\"})
            Dim user As String = sFullPath(1).Substring(sFullPath(1).IndexOf("(") + 1)
            noUser = user.Substring(0, user.Length - 1)
            If path.Length > 13 + sFullPath(1).Length Then realPath = path.Substring(13 + sFullPath(1).Length + 1)
        End If

        Return realPath
    End Function

    Public Shared Function getPath(ByVal noUser As Integer, ByVal realPath As String) As String
        If noUser = 0 Then
            Return "Généraux" & addSlash(realPath) & realPath
        ElseIf noUser = -1 Then
            Return "Utilisateurs"
        Else
            Return "Utilisateurs\" & External.current.getUser_ToString(noUser) & addSlash(realPath) & realPath
        End If
    End Function

    Public Overrides Function toString() As String
        Dim path As String = ""
        If _NoUser = 0 Then
            path = "Généraux"
        Else
            If _NoUser = -1 Then
                path = _Folder
            Else
                path = "Utilisateurs\" & External.current.getUser_ToString(_NoUser)
            End If
        End If
        If _Folder <> "" And noUser >= 0 Then path &= "\" & _Folder

        Return path
    End Function

    Protected Sub setNbNewItems(ByVal nbNewItems As Integer)
        Me._nbNewItems = nbNewItems
        onDataChanged()
    End Sub

    Public Function propertiesToString() As String Implements IPropertiesManagable.propertiesToString
        Return PropertiesHelper.transformProperties(properties)
    End Function

    Public MustOverride Sub loadProperties(ByVal properties As System.Collections.Hashtable) Implements IPropertiesManagable.loadProperties
    Public MustOverride Sub saveProperties(ByRef properties As System.Collections.Hashtable) Implements IPropertiesManagable.saveProperties

    Public Overrides ReadOnly Property noItemable() As Integer
        Get
            Return Me.noFolder
        End Get
    End Property
End Class
