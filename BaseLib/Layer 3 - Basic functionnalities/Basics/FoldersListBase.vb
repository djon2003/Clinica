Public MustInherit Class FoldersListBase(Of Self, Managed)
    Inherits DBItemableManagerBase(Of Self, Managed)

    Private _currentNoUser As Integer
    Private _FoldersByPath As New Generic.Dictionary(Of String, Managed)

    Protected Sub New()
        MyBase.New()
    End Sub

#Region "Properties"
    Public Property currentNoUser() As Integer
        Get
            Return _currentNoUser
        End Get
        Set(ByVal value As Integer)
            _currentNoUser = value
            load()
        End Set
    End Property
#End Region

    Protected MustOverride Overloads Sub load(ByVal noFolder As Integer)
    Public Overrides Sub load()
        load(0)
    End Sub
    Protected MustOverride ReadOnly Property isLoading() As Boolean

    Private Function containsFolder(ByVal path As String) As Boolean
        Dim containingFolder As Boolean = False
        changingItemablesLock.AcquireReaderLock(Threading.Timeout.Infinite)
        containingFolder = _FoldersByPath.ContainsKey(path)
        changingItemablesLock.ReleaseReaderLock()

        Return containingFolder
    End Function

    Public Overrides Function addItemable(ByVal newItem As Managed) As String
        Dim path As String
        Dim noFolder As Integer
        With CType(CType(newItem, Object), FolderBase)
            path = .toString
            If Not isLoading Then
                If containsFolder(path) Then Return "Le nom du dossier choisi existe déjà dans ce dossier"

                .saveData()
            End If
            noFolder = .noFolder
        End With

        changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)
        MyBase.addItemable(newItem)
        _FoldersByPath.Add(path, newItem)
        changingItemablesLock.ReleaseWriterLock()

        If Not isLoading Then InternalUpdatesManager.getInstance.sendUpdate("FLB-" & GetType(Managed).ToString & "-add(" & noFolder & ")")
    End Function

    Public Overloads Function addItemable(ByVal path As String, Optional ByRef noFolder As Integer = 0) As String
        Dim constructor As System.Reflection.ConstructorInfo = GetType(Managed).GetConstructor(Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance, Nothing, New System.Type() {}, Nothing)
        If constructor Is Nothing OrElse (constructor.Attributes And Reflection.MethodAttributes.Family) <> Reflection.MethodAttributes.Family Then Throw New Exception("Managed class has to have a constructor with a public scope")
        Dim myManaged As Managed = constructor.Invoke(New Object() {})

        Dim noUser As Integer = 0
        Dim realPath As String = FolderBase.getRealPath(path, noUser)

        Dim newFolder As FolderBase = CType(myManaged, Object)
        newFolder.folder = realPath
        newFolder.noUser = noUser

        Dim returning As String = Me.addItemable(CType(newFolder, Object))
        noFolder = newFolder.noItemable

        Return returning
    End Function

    Public Overrides Sub clear()
        changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)
        MyBase.clear()
        _FoldersByPath.Clear()
        changingItemablesLock.ReleaseWriterLock()
    End Sub

    Public Overloads Function getItemable(ByVal path As String) As Managed
        If containsFolder(path) = False Then Return Nothing

        changingItemablesLock.AcquireReaderLock(Threading.Timeout.Infinite)
        Dim myFolder As Managed = _FoldersByPath(path)
        changingItemablesLock.ReleaseReaderLock()

        Return myFolder
    End Function

    Protected Overrides Sub managedDeleted(ByVal sender As IDBItemable)
        Dim myPath As String = CType(sender, FolderBase).toString

        changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)
        _FoldersByPath.Remove(myPath)
        myPath &= "\"
        Dim keys() As String
        ReDim keys(_FoldersByPath.Keys.Count - 1)
        _FoldersByPath.Keys.CopyTo(keys, 0)

        For Each curFolderKey As String In keys
            If curFolderKey.StartsWith(myPath) Then
                'CType(_FoldersByPath(curFolderKey), IDBItemable).delete()
                MyBase.managedDeleted(_FoldersByPath(curFolderKey))
                _FoldersByPath.Remove(curFolderKey)
            End If
        Next

        MyBase.managedDeleted(sender)
        changingItemablesLock.ReleaseWriterLock()

        InternalUpdatesManager.getInstance.sendUpdate("FLB-" & GetType(Managed).ToString & "-del(" & sender.noItemable & ")")
    End Sub

    Protected Overrides Sub managedChanged(ByVal sender As IDBItemable)
        MyBase.managedChanged(sender)

        'Adjust subfolders and dictionary used as access accelerator
        Dim updateAll As Boolean = False
        With CType(sender, FolderBase)
            If .toString <> .lastPath Then
                changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)

                Dim keys() As String
                ReDim keys(_FoldersByPath.Keys.Count - 1)
                _FoldersByPath.Keys.CopyTo(keys, 0)
                Dim realLastPath As String = FolderBase.getRealPath(.lastPath, 0)

                For Each curFolderKey As String In keys
                    If curFolderKey = .lastPath Then
                        _FoldersByPath.Add(.toString, sender)
                        _FoldersByPath.Remove(curFolderKey)
                    ElseIf curFolderKey.StartsWith(.lastPath & "\") Then
                        Dim curFolder As FolderBase = CType(_FoldersByPath(curFolderKey), Object)

                        curFolder.folder = .folder & curFolder.folder.Substring(realLastPath.Length)
                        curFolder.noUser = .noUser
                        'No need to save because these values are already changed in SQL database

                        _FoldersByPath.Add(curFolder.toString, CType(curFolder, Object))
                        _FoldersByPath.Remove(curFolderKey)

                        updateAll = True
                    End If
                Next

                changingItemablesLock.ReleaseWriterLock()
            End If
        End With

        If updateAll Then
            InternalUpdatesManager.getInstance.sendUpdate("FLB-" & GetType(Managed).ToString & "()")
        Else
            InternalUpdatesManager.getInstance.sendUpdate("FLB-" & GetType(Managed).ToString & "-modif(" & sender.noItemable & ")")
        End If
    End Sub


    Public Overrides Sub removeItemable(ByVal noItem As Integer)
        changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)
        _FoldersByPath.Remove(CType(getItemable(noItem), Object).ToString)
        MyBase.removeItemable(noItem)
        changingItemablesLock.ReleaseWriterLock()
    End Sub

    Public Overrides Sub removeItemable(ByVal delItem As Managed)
        If delItem Is Nothing Then Exit Sub

        Me.removeItemable(CType(delItem, IDBItemable).noItemable)
    End Sub

    Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
        If dataReceived.fromExternal = False OrElse dataReceived.function.StartsWith("FLB-") = False Then Exit Sub
        If dataReceived.function.StartsWith("FLB-" & GetType(Managed).ToString) = False Then Exit Sub

        Dim noFolder As Integer = 0
        If dataReceived.function.EndsWith("-del") = False AndAlso dataReceived.params.Length <> 0 Then Integer.TryParse(dataReceived.params(0), noFolder)
        load(noFolder)
    End Sub

    Protected Overrides Sub sendUpdate()
        InternalUpdatesManager.getInstance.sendUpdate("FLB-" & GetType(Managed).ToString & "()")
    End Sub
End Class
