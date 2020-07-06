Public Class InternalDBItem
    Inherits DBItemableBase

    Public Sub New()
    End Sub

    Public Sub New(ByVal path As String, ByVal name As String)
        name = Fichiers.replaceIllegalChars(name)
        loadData(path, name)
    End Sub

    Public Sub New(ByVal noDBItem As Integer)
        loadData(noDBItem)
    End Sub

    Public Sub New(ByVal data As DBItemableData)
        loadData(data)
    End Sub

    Protected _UniqueNo As Integer = 0
    Private _NoDBItem As Integer = 0
    Private _NoDBFolder As Integer = 0
    Private _dbItem As String = ""
    Private oldDBItem As String = ""
    Private _dbItemFile As String = ""
    Private _NoFileType As Integer = 0
    Private _Description As String = ""
    Private _IsReadOnly, _IsHidden As Boolean
    Private _keywords() As String
    Private _CreationDate As Date
    Private _LastModifDate As Date

#Region "Propriétés"
    Public ReadOnly Property uniqueNo() As Integer
        Get
            Return _UniqueNo
        End Get
    End Property

    Public ReadOnly Property creationDate() As Date
        Get
            Return _CreationDate
        End Get
    End Property

    Public ReadOnly Property lastModifDate() As Date
        Get
            Return _LastModifDate
        End Get
    End Property

    Public Property keywords() As String()
        Get
            Return _keywords
        End Get
        Set(ByVal value As String())
            _keywords = value
        End Set
    End Property

    Public ReadOnly Property noDBItem() As Integer
        Get
            Return _NoDBItem
        End Get
    End Property

    Public Property noDBFolder() As Integer
        Get
            Return _NoDBFolder
        End Get
        Set(ByVal value As Integer)
            _NoDBFolder = value
        End Set
    End Property

    Public Property description() As String
        Get
            Return _Description
        End Get
        Set(ByVal value As String)
            _Description = value.Replace(vbCrLf, "<BR>").Replace(vbLf, "").Replace(vbCr, "")
        End Set
    End Property

    Public Property noFileType() As Integer
        Get
            Return _NoFileType
        End Get
        Set(ByVal value As Integer)
            _NoFileType = value
        End Set
    End Property

    Public Property dbItem() As String
        Get
            Return _dbItem
        End Get
        Set(ByVal value As String)
            _dbItem = Fichiers.replaceIllegalChars(value)
        End Set
    End Property

    Public Property dbItemFile() As String
        Get
            Return _dbItemFile
        End Get
        Set(ByVal value As String)
            _dbItemFile = Fichiers.replaceIllegalChars(value)
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
#End Region

    Public Shared Function exists(ByVal noDBItem As Integer) As Boolean
        Dim data As DataSet = DBLinker.getInstance.readDBForGrid("DBItems", "COUNT(*)", "WHERE NoDBItem=" & noDBItem & " OR UniqueNo=" & noDBItem)

        Return data.Tables(0).Rows(0)(0) > 0
    End Function

    Public Shared Function exists(ByVal path As String, ByVal name As String) As Boolean
        name = Fichiers.replaceIllegalChars(name)
        If path.EndsWith("\") Then path = path.Substring(0, path.Length - 1)
        Dim curDBFolder As InternalDBFolder = InternalDBManager.getInstance.getDBFolder(path)
        If curDBFolder Is Nothing Then Return False

        Dim data As DataSet = DBLinker.getInstance.readDBForGrid("DBItems", "COUNT(*)", "WHERE DBItem='" & name.Replace("'", "''") & "' AND NoDBFolder=" & curDBFolder.noDBFolder)

        Return data.Tables(0).Rows(0)(0) > 0
    End Function

    Public Function getKeywordsString(Optional ByVal joiner As String = "§") As String
        If _keywords Is Nothing Then Return ""

        Return String.Join(joiner, _keywords)
    End Function

    Public Function getDBFolder() As InternalDBFolder
        Return InternalDBManager.getInstance.getDBFolder(_NoDBFolder)
    End Function

    Public Shared Function getItemFromLink(ByVal link As String) As InternalDBItem
        'Remove protocol and remove action
        If link.ToLower.StartsWith(WebTextControl.PROTOCOL_CLINICA) Then
            link = link.Substring(10)
            link = Web.HttpUtility.UrlDecode(link)
            link = link.Substring(3) 'Remove "DB|"
        End If
        'Remove Virtual Disk
        If link.ToLower.StartsWith("db:\") Then
            link = link.Substring(4)
        End If

        Dim curNoDBItem As Integer = link.Substring(0, link.IndexOf("\"))
        link = link.Substring(link.IndexOf("\") + 1)
        Dim myFileName As String = link.Substring(link.LastIndexOf("\") + 1)
        Dim folderPath As String = link.Substring(0, link.LastIndexOf("\"))
        Dim curDBItem As InternalDBItem

        If exists(curNoDBItem) = False Then
            If exists(folderPath, myFileName) = False Then
                Throw New InexistantInternalDBItemException("L'item demandé n'existe plus." & vbCrLf & vbCrLf & "Veuillez ajouter un item dans le dossier (" & folderPath & ") portant le nom de :" & vbCrLf & myFileName)
            Else
                curDBItem = New InternalDBItem(folderPath, myFileName)
            End If
        Else
            curDBItem = New InternalDBItem(curNoDBItem)
        End If

        Return curDBItem
    End Function

    Public Function getTypeFile() As TypeFile
        Return TypesFilesManager.getInstance.getItemable(_NoFileType)
    End Function

    Public Sub cut()
        _copy(True)
        Try
            delete()
        Catch ex As Exception
            'Enlever la copie en mode couper
            Dim myCopyPath As String = appPath & bar(appPath) & "Users\Temp\" & ConnectionsManager.currentUser & "\DBCopy\"
            IO.File.Delete(myCopyPath & _dbItemFile)
            IO.File.Delete(myCopyPath & _NoDBItem & "\info")
            IO.Directory.Delete(myCopyPath & _NoDBItem)
            Throw ex
        End Try
    End Sub

    Public Sub copy()
        _copy(False)
    End Sub

    Private Sub _copy(ByVal isCutting As Boolean)
        Dim curPath As String
        Dim myCopyPath As String = appPath & bar(appPath) & "Users\Temp\" & ConnectionsManager.currentUser & "\DBCopy\"
        ensureGoodPath(myCopyPath)
        curPath = appPath & bar(appPath) & "DB\" & _dbItemFile
        IO.File.Copy(curPath, myCopyPath & _dbItemFile)

        Dim mycontent As String = Me.uniqueNo & vbCrLf & isCutting & vbCrLf & Me._dbItem & vbCrLf & Me._dbItemFile & vbCrLf & Me._Description.Replace(vbCrLf, "<BR>") & vbCrLf & Me._IsHidden & vbCrLf & Me._IsReadOnly & vbCrLf & Me.getKeywordsString & vbCrLf & Me._NoFileType
        IO.Directory.CreateDirectory(myCopyPath & bar(myCopyPath) & _NoDBItem.ToString)
        IO.File.WriteAllText(myCopyPath & _NoDBItem.ToString & "\info", mycontent)
    End Sub

    Public Shared Sub paste(ByVal noItemToCopy As Integer, ByVal destFolder As InternalDBFolder)
        Dim curPath As String
        Dim myCopyPath As String = appPath & bar(appPath) & "Users\Temp\" & ConnectionsManager.currentUser & "\DBCopy\"

        curPath = appPath & bar(appPath) & "DB\"
        Dim copyContent() As String = IO.File.ReadAllLines(myCopyPath & noItemToCopy & "\info")
        Dim newDBItem As New InternalDBItem()
        If copyContent(1) = True Then
            newDBItem._UniqueNo = copyContent(0)
            copyContent(1) = False
            IO.File.WriteAllLines(myCopyPath & noItemToCopy & "\info", copyContent)
        End If
        newDBItem.dbItem = copyContent(2)
        newDBItem.dbItemFile = copyContent(3)
        newDBItem.description = copyContent(4).Replace("<BR>", vbCrLf)
        newDBItem.isHidden = copyContent(5)
        newDBItem.isReadOnly = copyContent(6)
        If copyContent(5) <> "" Then newDBItem.keywords = copyContent(7).Split(New Char() {"§"})
        newDBItem.noFileType = copyContent(8)
        newDBItem.noDBFolder = destFolder.noDBFolder
        Dim errorMsg As String = InternalDBManager.getInstance.addItem(newDBItem, False)
        If errorMsg <> "" Then MessageBox.Show(errorMsg, "Impossible d'ajouter l'item") : Exit Sub
        'newDBItem.saveData()

        Dim newFileContent As String = newDBItem.noDBItem & "." & copyContent(2) & "." & newDBItem.dbItemFile.Substring(newDBItem.dbItemFile.LastIndexOf(".") + 1)

        IO.File.Copy(myCopyPath & newDBItem.dbItemFile, appPath & bar(appPath) & "DB\" & newFileContent)
        newDBItem.dbItemFile = newFileContent
        newDBItem.saveData()
    End Sub

    Public Overrides Sub delete()
        If Me._dbItemFile <> "" Then
            Try
                IO.File.Delete(appPath & bar(appPath) & "DB\" & Me._dbItemFile)
            Catch ex As System.IO.IOException
                Dim list As New Generic.List(Of IDBItemable)
                list.Add(Me)
                Throw New DBItemableUndeletable(list)
            End Try
        End If
        DBLinker.getInstance.delDB("DBItems", "NoDBItem", _NoDBItem, False)
        onDeleted()

        'TODO : If autoSendUpdateOnDelete Then 
    End Sub

    Public Overloads Sub loadData(ByVal noDBItem As Integer)
        Dim data As DataSet = DBLinker.getInstance.readDBForGrid("DBItems", "*", "WHERE NoDBItem=" & noDBItem & " OR UniqueNo=" & noDBItem)
        If data Is Nothing OrElse data.Tables(0).Rows.Count = 0 Then Exit Sub

        loadData(New DBItemableData(data.Tables(0).Rows(0)))
    End Sub

    Public Overloads Sub loadData(ByVal path As String, ByVal name As String)
        name = Fichiers.replaceIllegalChars(name)
        If path.EndsWith("\") Then path = path.Substring(0, path.Length - 1)

        Dim curDBFolder As InternalDBFolder = InternalDBManager.getInstance.getDBFolder(path)
        Dim data As DataSet = DBLinker.getInstance.readDBForGrid("DBItems", "*", "WHERE DBItem='" & name.Replace("'", "''") & "' AND NoDBFolder=" & curDBFolder.noDBFolder)
        If data Is Nothing OrElse data.Tables(0).Rows.Count = 0 Then Exit Sub

        loadData(New DBItemableData(data.Tables(0).Rows(0)))
    End Sub

    Public Overrides Sub loadData(ByVal data As DBItemableData)
        Dim curData As DataRow = data.mainData

        _NoDBItem = curData("NoDBItem")
        _NoDBFolder = curData("NoDBFolder")
        _dbItem = curData("DBItem")
        oldDBItem = _dbItem
        _dbItemFile = curData("DBItemFile")
        _NoFileType = curData("NoFileType")
        _Description = curData("Description").ToString.Replace("<BR>", vbCrLf)
        _IsReadOnly = curData("IsReadOnly")
        _IsHidden = curData("IsHidden")
        _LastModifDate = curData("LastModifDate")
        _CreationDate = curData("CreationDate")
        _UniqueNo = curData("UniqueNo")

        _keywords = DBLinker.getInstance.readOneDBField("DBMotsCles INNER JOIN DBItemsMotsCles ON DBMotsCles.NoMotCle=DBItemsMotsCles.NoMotCle", "MotCle", "WHERE NoDBItem=" & _NoDBItem)
    End Sub

    Private Sub saveKeywords()
        Dim motsClesScript As String = "DELETE FROM DBItemsMotsCles WHERE NoDBItem=" & _NoDBItem & ";"
        If _keywords IsNot Nothing AndAlso _keywords.Length <> 0 Then
            For i As Integer = 0 To _keywords.GetUpperBound(0)
                If _keywords(i) <> "" Then motsClesScript &= "INSERT INTO DBItemsMotsCles (NoDBItem,NoMotCle) VALUES(" & _NoDBItem & "," & DBHelper.addItemToADBList("DBMotsCles", "MotCle", _keywords(i), "NoMotCle") & ");"
            Next i
        End If

        DBLinker.executeSQLScript(motsClesScript)
    End Sub

    Public Overrides Sub saveData()
        If _NoDBItem = 0 Then
            DBLinker.getInstance.writeDB("DBItems", "NoDBFolder,DBItem,DBItemFile,NoFileType,Description,IsReadOnly,IsHidden,CreationDate,LastModifDate,UniqueNo", Me._NoDBFolder & ",'" & _dbItem.Replace("'", "''") & "','" & _dbItemFile.Replace("'", "''") & "'," & _NoFileType & ",'" & _Description.Replace("'", "''") & "','" & _IsReadOnly & "','" & _IsHidden & "','" & DateFormat.getTextDate(Date.Today) & "','" & DateFormat.getTextDate(Date.Today) & "'," & _UniqueNo, , , , _NoDBItem)
            If _UniqueNo = 0 Then _UniqueNo = _NoDBItem
        Else
            If oldDBItem <> "" AndAlso oldDBItem <> dbItem Then
                Dim curExt() As String = dbItemFile.Split(New Char() {"."})
                Try
                    IO.File.Move(appPath & bar(appPath) & "DB\" & dbItemFile, appPath & bar(appPath) & "DB\" & noDBItem & "." & dbItem & "." & curExt(curExt.GetUpperBound(0)))
                Catch ex As Exception
                    _dbItem = oldDBItem
                    Throw ex
                End Try
                _dbItemFile = noDBItem & "." & dbItem & "." & curExt(curExt.GetUpperBound(0))
            End If
            DBLinker.getInstance.updateDB("DBItems", "NoDBFolder=" & Me._NoDBFolder & ",DBItem='" & _dbItem.Replace("'", "''") & "',DBItemFile='" & _dbItemFile.Replace("'", "''") & "',NoFileType=" & _NoFileType & ",Description='" & _Description.Replace("'", "''") & "',IsReadOnly='" & _IsReadOnly & "',IsHidden='" & _IsHidden & "',CreationDate='" & DateFormat.getTextDate(Me.creationDate) & "',LastModifDate='" & DateFormat.getTextDate(Date.Today) & "',UniqueNo=" & uniqueNo, "NoDBItem", _NoDBItem, False)
            onDataChanged()
        End If

        saveKeywords()

        If autoSendUpdateOnSave Then
            InternalUpdatesManager.getInstance.sendUpdate("DBFolderItems(" & _NoDBFolder & ")")
            InternalUpdatesManager.getInstance.sendUpdate("DBItem(" & _NoDBItem & ")")
        End If
    End Sub

    Public Overrides Function toString() As String
        Return _dbItem
    End Function

    Public Overrides ReadOnly Property noItemable() As Integer
        Get
            Return Me.noDBItem
        End Get
    End Property
End Class
