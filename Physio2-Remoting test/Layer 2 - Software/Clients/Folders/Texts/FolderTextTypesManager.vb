Namespace Accounts.Clients.Folders.Codifications

    Public Class FolderTextTypesManager
        Inherits DBItemableManagerBase(Of FolderTextTypesManager, FolderTextType)
        Implements ICloneable

        Protected Sub New()
            MyBase.New()

            autoSaveOnAdd = False
            load()
        End Sub


        Public Overrides Sub load()
            load(0)
        End Sub
        Private Overloads Sub load(ByVal noFolderTextType As Integer)
            Dim data As DataSet = DBLinker.getInstance.readDBForGrid("FolderTexteTypes", "*", "WHERE " & IIf(noFolderTextType = 0, "1=1", "NoFolderTexteType=" & noFolderTextType) & " ORDER BY Position")

            If (data Is Nothing OrElse data.Tables.Count = 0 OrElse data.Tables(0).Rows.Count = 0) Then
                If noFolderTextType <> 0 Then
                    Dim ftt As FolderTextType = getItemable(noFolderTextType)
                    If ftt IsNot Nothing Then MyBase.removeItemable(ftt)
                Else
                    Me.clear()
                End If
                Exit Sub
            End If

            If noFolderTextType <> 0 Then
                Dim ftt As FolderTextType = getItemable(noFolderTextType)
                Dim fttData As New DBItemableData(data.Tables(0).Rows(0))
                If ftt Is Nothing Then
                    MyBase.addItemable(New FolderTextType(fttData))
                    Exit Sub
                End If
                ftt.loadData(fttData)
            Else
                changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)
                Me.clear()
                For Each curRow As DataRow In data.Tables(0).Rows
                    MyBase.addItemable(New FolderTextType(New DBItemableData(curRow)))
                Next
                changingItemablesLock.ReleaseWriterLock()
            End If
        End Sub

        Public Overloads Function addItemable(ByVal texteTitle As String) As String
            Dim maxPos As Integer = 0
            For Each curFTT As FolderTextType In getItemables()
                If curFTT.textTitle = texteTitle Then Return "Impossible d'ajouter ce type de texte, car le titre est déjà utilisé pour cette codification"
                maxPos = Math.Max(maxPos, curFTT.position)
            Next

            Dim newFTT As New FolderTextType()
            newFTT.textTitle = texteTitle
            newFTT.position = maxPos + 1

            Return MyBase.addItemable(newFTT)
        End Function

        Public Overrides Function getItemables() As Generic.List(Of FolderTextType)
            Dim ftt As Generic.List(Of FolderTextType) = MyBase.getItemables()
            ftt.Sort()

            Return ftt
        End Function

        Public Overloads Function getItemable(ByVal texteTitle As String) As FolderTextType
            For Each curFTT As FolderTextType In getItemables()
                If curFTT.textTitle = texteTitle Then Return curFTT
            Next

            Return Nothing
        End Function

        Public Function countFolderTexts(ByVal noFolder As Integer) As String(,)
            If MyBase.count = 0 Then Return New String(,) {{"0"}}

            Dim nosFTT As String = ""
            For Each curFTT As FolderTextType In getItemables()
                nosFTT &= "," & curFTT.noFolderTexteType
            Next
            nosFTT = nosFTT.Substring(1)

            Dim nbFT As String(,) = DBLinker.getInstance.readDB("FolderTexteTypes", "NoFolderTexteType,(SELECT COUNT(*) FROM FolderTextes WHERE " & If(noFolder = 0, "", "NoFolder=" & noFolder & " AND") & " FolderTextes.NoFolderTexteType = FolderTexteTypes.NoFolderTexteType) AS NbFT", "WHERE NoFolderTexteType IN (" & nosFTT & ")")
            If nbFT Is Nothing OrElse nbFT.Length = 0 Then Return New String(,) {{"0"}}

            Return nbFT
        End Function

        Public Function countFolderTexts() As String(,)
            Return countFolderTexts(0)
        End Function

        Public Function clone() As Object Implements System.ICloneable.Clone
            Dim newFTTs As New FolderTextTypesManager
            For Each curFTT As FolderTextType In getItemables()
                Dim newFTT As FolderTextType = curFTT.clone
                newFTTs.addItemable(newFTT)
            Next

            Return newFTTs
        End Function

        Protected Overrides Sub finalize()
            MyBase.Finalize()
        End Sub

        Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
            If dataReceived.function.StartsWith("FTT") = False OrElse dataReceived.fromExternal = False Then Exit Sub

            Select Case dataReceived.function
                Case "FTT"
                    load(dataReceived.params(0))
                Case "FTT-Del"
                    Dim curFTT As FolderTextType = getItemable(dataReceived.params(0))
                    If curFTT IsNot Nothing Then removeItemable(curFTT)
                Case "FTTs"
                    load()
            End Select
        End Sub

        Protected Overrides Sub sendUpdate()
            InternalUpdatesManager.getInstance.sendUpdate("FTTs()")
        End Sub
    End Class

End Namespace
