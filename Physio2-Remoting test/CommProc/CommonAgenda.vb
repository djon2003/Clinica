Module CommonAgenda


    Public Function findFreeAgenda() As Short
        Dim i As Short
        If Not myMainWin.menutherapeute.DropDownItems(0).Text = "Aucun" Then
            For i = 0 To myMainWin.menutherapeute.DropDownItems.Count - 1
                Dim strp() As String = System.Text.RegularExpressions.Regex.Split(myMainWin.menutherapeute.DropDownItems(i).Text, " \(")
                If myMainWin.formOuvertes.findStringExact("Agenda : " & myMainWin.menutherapeute.DropDownItems(i).Text) = -1 Then Return CShort(strp(1).Substring(0, strp(1).Length - 1))
            Next i
        Else
            Return 0
        End If
    End Function

    Public Sub updatingALLTRPMenu(Optional ByVal addToUpdateList As Boolean = True)
        If myMainWin IsNot Nothing Then
            With myMainWin
                .updateTRPMenu()
            End With
        End If

        If addToUpdateList = True Then InternalUpdatesManager.getInstance.sendUpdate("ALLTRPMenu()")
    End Sub

    Public Sub uncheckedPeriode(ByVal myForm As MainWin, Optional ByVal nbDaysPeriodeToSelect As Byte = 0)
        internalUncheckedPeriode(myForm, nbDaysPeriodeToSelect)
    End Sub

    Private Sub internalUncheckedPeriode(ByVal myForm As Object, Optional ByVal nbDaysPeriodeToSelect As Byte = 0)
        Dim i As Byte
        With myForm
            For i = 1 To 6
                CType(.menujour(i), ToolStripMenuItem).Checked = False
                If i = nbDaysPeriodeToSelect Then CType(.menujour(i), ToolStripMenuItem).Checked = True
            Next i

            For i = 1 To 4
                CType(.menusemaine.Item(i), ToolStripMenuItem).Checked = False
                If (i * 7) = nbDaysPeriodeToSelect Then CType(.menusemaine(i), ToolStripMenuItem).Checked = True
            Next i

            .menumois.Checked = False
            If 42 = nbDaysPeriodeToSelect Then .menumois.Checked = True
        End With
    End Sub

    Public Sub uncheckedPeriode(ByVal myForm As Agenda, Optional ByVal nbDaysPeriodeToSelect As Byte = 0)
        internalUncheckedPeriode(myForm, nbDaysPeriodeToSelect)
    End Sub

    Public Sub removingAgendaEntry(Optional ByVal noClient As Integer = 0, Optional ByVal noFolder As Integer = 0, Optional ByVal noVisite As Integer = 0, Optional ByVal myDate As Date = LIMIT_DATE, Optional ByVal agendaEntryNo As Integer = 0, Optional ByVal skipFolderDelete As Boolean = False, Optional ByVal skipQueueList As Boolean = False, Optional ByVal trp As String = "", Optional ByVal showStatus As Boolean = False)
        If noClient = 0 And agendaEntryNo = 0 Then Exit Sub
        Dim selfOpened As Boolean = False
        Dim thisDate As Date
        thisDate = myDate

        If DBLinker.getInstance().dbConnected = False Then selfOpened = True : DBLinker.getInstance().dbConnected = True
        Dim noTRP As String = 0
        If trp <> "" Then
            noTRP = trp.Split(New Char() {"("})(1)
            noTRP = noTRP.Substring(0, noTRP.Length - 1)
        End If
        If agendaEntryNo = 0 Then
            delVisite(noClient, noFolder, noVisite, thisDate, skipFolderDelete, noTRP)
        Else
            DBLinker.getInstance.delDB("Agenda", "NoAgenda", agendaEntryNo, False)
            InternalUpdatesManager.getInstance.sendUpdate("Agendas(" & DateFormat.getTextDate(thisDate, DateFormat.TextDateOptions.YYYYMMDD_FullTime) & ",false," & noTRP & ")")
        End If

        If showStatus Then myMainWin.StatusText = "Suppression d'une plage le " & DateFormat.getTextDate(thisDate, DateFormat.TextDateOptions.YYYYMMDD) & " à " & DateFormat.getTextDate(thisDate, DateFormat.TextDateOptions.ShortTime) & IIf(trp <> "", " pour " & trp, "")

        If CType(PreferencesManager.getGeneralPreferences()("ShowQLOnAgendaRemove"), Boolean) = True And skipQueueList = False And trp <> "" Then
            openRestraintQueueList(myDate, noTRP) 'Fait pas apparaître la liste d'attente
        End If

        If selfOpened = True Then DBLinker.getInstance().dbConnected = False
    End Sub
End Module
