Namespace Accounts.Clients.Folders.Codifications

    Public Class FolderAlertTypesManager
        Inherits DBItemableManagerBase(Of FolderAlertTypesManager, FolderAlertType)
        Implements ICloneable

        Protected Sub New()
            MyBase.New()

            autoSaveOnAdd = False
            load()
        End Sub

        Public Overrides Sub load()
            load(0)
        End Sub
        Private Overloads Sub load(ByVal noFolderAlertType As Integer)
            Dim data As DataSet = DBLinker.getInstance.readDBForGrid("FolderAlertTypes", "*", "WHERE " & IIf(noFolderAlertType = 0, "1=1", "NoFolderAlertType=" & noFolderAlertType))

            If (data Is Nothing OrElse data.Tables.Count = 0 OrElse data.Tables(0).Rows.Count = 0) Then
                If noFolderAlertType = 0 Then
                    Me.clear()
                Else
                    Dim fat As FolderAlertType = getItemable(noFolderAlertType)
                    If fat IsNot Nothing Then MyBase.removeItemable(fat)
                End If
                Exit Sub
            End If
            
            If noFolderAlertType <> 0 Then
                Dim fat As FolderAlertType = getItemable(noFolderAlertType)
                Dim fatData As New DBItemableData(data.Tables(0).Rows(0))
                If fat Is Nothing Then
                    MyBase.addItemable(New FolderAlertType(fatData))
                    Exit Sub
                End If
                fat.loadData(fatData)
            Else
                Dim curFATs As New Generic.List(Of FolderAlertType)
                For Each curRow As DataRow In data.Tables(0).Rows
                    curFATs.Add(New FolderAlertType(New DBItemableData(curRow)))
                Next

                changingItemablesLock.AcquireWriterLock(Threading.Timeout.Infinite)
                Me.clear()
                For Each curRow As DataRow In data.Tables(0).Rows
                    MyBase.addItemable(New FolderAlertType(New DBItemableData(curRow)))
                Next
                changingItemablesLock.ReleaseWriterLock()
            End If
        End Sub

        Public Overloads Function addItemable(ByVal alertTitle As String) As String
            Dim newFCA As New FolderAlertType()
            newFCA.alertTitle = alertTitle

            Return MyBase.addItemable(newFCA)
        End Function

        Public Overrides Function getItemables() As Generic.List(Of FolderAlertType)
            Dim fca As Generic.List(Of FolderAlertType) = MyBase.getItemables()
            fca.Sort()

            Return fca
        End Function

        Public Overloads Function getItemable(ByVal alertTitle As String) As FolderAlertType
            For Each curFCA As FolderAlertType In getItemables()
                If curFCA.alertTitle = alertTitle Then Return curFCA
            Next

            Return Nothing
        End Function

        Public Function countFolderAlerts(ByVal noFolder As Integer) As String(,)
            If MyBase.count = 0 Then Return New String(,) {{"0"}}

            Dim nosFAT As String = ""
            For Each curFAT As FolderAlertType In getItemables()
                nosFAT &= "," & curFAT.noFolderAlertType
            Next
            nosFAT = nosFAT.Substring(1)

            Dim nbFA As String(,) = DBLinker.getInstance.readDB("FolderAlertTypes", "NoFolderAlertType,(SELECT COUNT(*) FROM FolderAlerts WHERE " & If(noFolder = 0, "", "NoFolder=" & noFolder & " AND") & " FolderAlerts.NoFolderAlertType = FolderAlertTypes.NoFolderAlertType) AS NbFA", "WHERE NoFolderAlertType IN (" & nosFAT & ")")
            If nbFA Is Nothing OrElse nbFA.Length = 0 Then Return New String(,) {{"0"}}

            Return nbFA
        End Function

        Public Function countFolderAlerts() As String(,)
            Return countFolderAlerts(0)
        End Function

        Public Function clone() As Object Implements System.ICloneable.Clone
            Dim newFCAs As New FolderAlertTypesManager
            For Each curFCA As FolderAlertType In Me.getItemables
                Dim newFCA As FolderAlertType = curFCA.clone
                newFCAs.addItemable(newFCA)
            Next

            Return newFCAs
        End Function

        Public Overrides Sub dataConsume(ByVal dataReceived As DataInternalUpdate)
            If dataReceived.function.StartsWith("FAT") = False OrElse dataReceived.fromExternal = False Then Exit Sub

            Select Case dataReceived.function
                Case "FAT"
                    load(dataReceived.params(0))
                Case "FAT-Del"
                    Dim curFAT As FolderAlertType = getItemable(dataReceived.params(0))
                    If curFAT IsNot Nothing Then removeItemable(curFAT)
                Case "FATs"
                    load()
            End Select
        End Sub

        Protected Overrides Sub sendUpdate()
            InternalUpdatesManager.getInstance.sendUpdate("FATs()")
        End Sub
    End Class

End Namespace
