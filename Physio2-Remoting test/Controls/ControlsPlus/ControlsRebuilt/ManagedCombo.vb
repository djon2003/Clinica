Public Class ManagedCombo
    Inherits System.Windows.Forms.ComboBox


    Public Sub New()
        Me.DoubleBuffered = True
        Me.AutoCompleteSource = Windows.Forms.AutoCompleteSource.ListItems
    End Sub

    Protected Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Me.itemsToolTip IsNot Nothing Then Me.itemsToolTip.Dispose()
        End If

        MyBase.Dispose(disposing)
    End Sub

    Private _AcceptAlpha As Boolean = True
    Private _AcceptNumeric As Boolean = True
    Private _OnlyAlphabet As Boolean = False
    Private _RefuseAccents As Boolean = False
    Private _AcceptedChars, MyValue, _RefusedChars, _DBField, AcceptedText, _MatchExp, _PathOfList As String
    Private _flCapital As Boolean = False
    Private _OnlyFLCapital As Boolean = False
    Private _AllCapital As Boolean = False
    Private _AllLower As Boolean = False
    Private _InModification As Boolean = False
    Private _CurrencyBox As Boolean = False
    Private _NbDecimals As Short = -1
    Private mySelectionStart As Integer
    Private acceptNegative As Boolean = False
    Private _AutoComplete As Boolean = True
    Private _ManageText As Boolean = True
    Private keyPressed As Keys
    Private _TrimText As Boolean = False
    Private _DoComboDelete As Boolean = True
    Private _ReadOnly As Boolean = False
    Private _AutoSizeDropDown As Boolean = True
    Private itemsToolTip As ToolTip
    Private _ItemsToolTipDuration As Integer = 10000
    Private _ShowItemsToolTip As Boolean = False
    Private lastItemToolTip As String = ""
    Private _BlockOnMinimum As Boolean = False
    Private _BlockOnMaximum As Boolean = False
    Private _cb_AcceptLeftZeros As Boolean = False
    Private _Minimum As Double = 0
    Private _Maximum As Double = 0

    Public Event deleteItem(ByVal currentItem As String)
    Public Event deletedItem(ByVal currentItem As String)

#Region "Propriétés"
    Public Property minimum() As Double
        Get
            Return _Minimum
        End Get
        Set(ByVal Value As Double)
            _Minimum = Value
        End Set
    End Property

    Public Property maximum() As Double
        Get
            Return _Maximum
        End Get
        Set(ByVal Value As Double)
            _Maximum = Value
        End Set
    End Property

    Public Property blockOnMinimum() As Boolean
        Get
            Return _BlockOnMinimum
        End Get
        Set(ByVal Value As Boolean)
            _BlockOnMinimum = Value
        End Set
    End Property

    Public Property blockOnMaximum() As Boolean
        Get
            Return _BlockOnMaximum
        End Get
        Set(ByVal Value As Boolean)
            _BlockOnMaximum = Value
        End Set
    End Property

    Public Property showItemsToolTip() As Boolean
        Get
            Return _ShowItemsToolTip
        End Get
        Set(ByVal value As Boolean)
            _ShowItemsToolTip = value

            If value Then
                itemsToolTip = New ToolTip()
            Else
                itemsToolTip = Nothing
            End If
        End Set
    End Property

    Public Property itemsToolTipDuration() As Integer
        Get
            Return _ItemsToolTipDuration
        End Get
        Set(ByVal value As Integer)
            _ItemsToolTipDuration = value
        End Set
    End Property

    Public Property autoSizeDropDown() As Boolean
        Get
            Return _AutoSizeDropDown
        End Get
        Set(ByVal value As Boolean)
            _AutoSizeDropDown = value
        End Set
    End Property

    Public Property [ReadOnly]() As Boolean
        Get
            Return _ReadOnly
        End Get
        Set(ByVal value As Boolean)
            _ReadOnly = value

            If value Then
                Me.BackColor = SystemColors.ControlLight
            Else
                Me.BackColor = Color.White
            End If
        End Set
    End Property

    Public Property doComboDelete() As Boolean
        Get
            Return _DoComboDelete
        End Get
        Set(ByVal Value As Boolean)
            _DoComboDelete = Value
        End Set
    End Property

    Public Property trimText() As Boolean
        Get
            Return _TrimText
        End Get
        Set(ByVal Value As Boolean)
            _TrimText = Value
        End Set
    End Property

    Public Property dbField() As String
        Get
            Return _DBField
        End Get
        Set(ByVal Value As String)
            If InStr(Value, ".") > 0 Then _DBField = Value
        End Set
    End Property

    Public Property manageText() As Boolean
        Get
            Return _ManageText
        End Get
        Set(ByVal Value As Boolean)
            _ManageText = Value
        End Set
    End Property

    Public Property autoComplete() As Boolean
        Get
            Return _AutoComplete
        End Get
        Set(ByVal Value As Boolean)
            _AutoComplete = Value
        End Set
    End Property

    Public Property pathOfList() As String
        Get
            Return _PathOfList
        End Get
        Set(ByVal Value As String)
            _PathOfList = Value
        End Set
    End Property

    Public Property cb_AcceptNegative() As Boolean
        Get
            Return acceptNegative
        End Get
        Set(ByVal Value As Boolean)
            acceptNegative = Value
            If InStr(acceptedChars, "-") = 0 Then changeChar("-", Value)
        End Set
    End Property

    Public Property cb_AcceptLeftZeros() As Boolean
        Get
            Return _cb_AcceptLeftZeros
        End Get
        Set(ByVal Value As Boolean)
            _cb_AcceptLeftZeros = Value
            If Value Then forceManaging()
        End Set
    End Property

    Public Property nbDecimals() As Short
        Get
            Return _NbDecimals
        End Get
        Set(ByVal Value As Short)
            If Value < 0 Then
                _NbDecimals = -1
            Else
                _NbDecimals = Value
            End If
        End Set
    End Property

    Public Property currencyBox() As Boolean
        Get
            Return _CurrencyBox
        End Get
        Set(ByVal Value As Boolean)
            _CurrencyBox = Value
            If InStr(acceptedChars, ",") = 0 Then changeChar(",", Value)
            If InStr(acceptedChars, ".") = 0 Then changeChar(".", Value)
        End Set
    End Property

    Public Property refuseAccents() As Boolean
        Get
            Return _RefuseAccents
        End Get
        Set(ByVal Value As Boolean)
            _RefuseAccents = Value
        End Set
    End Property

    Public Property onlyAlphabet() As Boolean
        Get
            Return _OnlyAlphabet
        End Get
        Set(ByVal Value As Boolean)
            _OnlyAlphabet = Value
            If Value = False Then refuseAccents = False
        End Set
    End Property

    Public Property acceptAlpha() As Boolean
        Get
            Return _AcceptAlpha
        End Get
        Set(ByVal Value As Boolean)
            _AcceptAlpha = Value
        End Set
    End Property

    Public Property acceptNumeric() As Boolean
        Get
            Return _AcceptNumeric
        End Get
        Set(ByVal Value As Boolean)
            _AcceptNumeric = Value
        End Set
    End Property

    Public Property acceptedChars() As String
        Get
            Return _AcceptedChars
        End Get
        Set(ByVal Value As String)
            _AcceptedChars = Value
        End Set
    End Property

    Public Property refusedChars() As String
        Get
            Return _RefusedChars
        End Get
        Set(ByVal Value As String)
            _RefusedChars = Value
        End Set
    End Property

    Public Property firstLettersCapital() As Boolean
        Get
            Return _flCapital
        End Get
        Set(ByVal Value As Boolean)
            _flCapital = Value
            If Value = True Then firstLetterCapital = True
        End Set
    End Property

    Public Property firstLetterCapital() As Boolean
        Get
            Return _OnlyFLCapital
        End Get
        Set(ByVal Value As Boolean)
            _OnlyFLCapital = Value
            If Value = False Then firstLettersCapital = False
        End Set
    End Property

    Public Property matchExp() As String
        Get
            Return _MatchExp
        End Get
        Set(ByVal Value As String)
            _MatchExp = Value
        End Set
    End Property

    Public Property allCapital() As Boolean
        Get
            Return _AllCapital
        End Get
        Set(ByVal Value As Boolean)
            _AllCapital = Value
            If Value = True Then allLower = False
        End Set
    End Property

    Public Property allLower() As Boolean
        Get
            Return _AllLower
        End Get
        Set(ByVal Value As Boolean)
            _AllLower = Value
            If Value = True Then allCapital = False
        End Set
    End Property
#End Region

#Region "Internal functions"
    Private Function autoCompleting() As Boolean
        If MyValue = "" Then Return False

        Dim i As Integer
        For i = 0 To Me.Items.Count - 1
            If Me.Items(i).ToString.ToUpper.StartsWith(MyValue.ToUpper) Then
                MyValue = Me.Items(i).ToString
                REM Me.DroppedDown = True
                ' When activated, shows the drop list.. Good, but the mouse disapear + some times going down the list with the arrows key skip items (when they both start with the same text)
                ' It seems that the mouse disappearing is not linked with this.. ManagedText too, when typing text, the mouse hides --> Normal behavior.. normal TextBox does this too.
                ' Though, when moving the mouse, the cursor reappear, which is not which ManagedCombo showing the drop list
                Return True
            End If
        Next i

        Return False
    End Function

    Private Sub changeChar(ByVal myChar As Char, ByVal adding As Boolean)
        Dim prefixe As String, NewAcceptedChars As String = ""
        If acceptedChars <> "" Then prefixe = "§" Else prefixe = ""

        If adding = True Then
            If InStr(acceptedChars, myChar) = 0 Then acceptedChars &= prefixe & myChar
        Else
            If acceptedChars <> "" Then
                Dim sac() As String = acceptedChars.Split(New Char() {"§"})
                Dim myCharPos As Short = searchArray(sac, myChar, CompareMethod.Text)
                If myCharPos <> -1 Then
                    Dim i As Short
                    For i = 0 To sac.Length - 1
                        If i <> myCharPos Then NewAcceptedChars &= "§" & sac(i)
                    Next i
                    If NewAcceptedChars.Length >= 1 Then
                        acceptedChars = NewAcceptedChars.Substring(1)
                    Else
                        acceptedChars = ""
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub applyCapitals()
        MyValue = Chaines.firstLetterCapital(MyValue, firstLettersCapital)
    End Sub

    Private Sub applyMatch()
        MyValue = Chaines.applyMatch(MyValue, matchExp)
    End Sub
#End Region

    REM Code requiered to show tooltip on dropdownlist item selection
    'Protected Overrides Sub wndProc(ByRef m As System.Windows.Forms.Message)
    '    Try
    '        MyBase.WndProc(m)
    '    Catch ex As Exception
    '        AddErrorLog(New Exception("(ERR1 : MousePosition=" & Control.MousePosition.ToString & vbCrLf & "Me.Bound=" & Me.Bounds.ToString, ex))
    '    End Try

    '    'Dim myBounds As New Rectangle(Me.PointToScreen(Me.Location), Me.Size)
    '    'If myBounds.Contains(Control.MousePosition) = False Then Exit Sub

    '    Try
    '        If m.Msg = WM_CTLCOLORLISTBOX Then ShowItemToolTip()
    '    Catch ex As Exception
    '        AddErrorLog(New Exception("ERR2 : MousePosition=" & Control.MousePosition.ToString & vbCrLf & "Me.Bound=" & Me.Bounds.ToString, ex))
    '    End Try
    'End Sub

    Private Sub showItemToolTip()
        If Me.showItemsToolTip AndAlso Me.SelectedItem IsNot Nothing AndAlso (lastItemToolTip <> Me.SelectedItem.ToString OrElse itemsToolTip.Active = False) Then
            Dim stringSize As Size = Chaines.measureString(Me.SelectedItem.ToString, Me.Font)
            itemsToolTip.Show(Me.SelectedItem.ToString, Me.FindForm, Screen.PrimaryScreen.WorkingArea.Width, Me.FindForm.PointToScreen(Me.Location).Y, _ItemsToolTipDuration)
            lastItemToolTip = Me.SelectedItem.ToString()
        End If
    End Sub

    Public Overloads Function findString(ByVal value As String, Optional ByVal endsWith As Boolean = False, Optional ByVal findLast As Boolean = False) As Integer
        Dim first As Integer = 0
        Dim last As Integer = Me.Items.Count - 1
        Dim stepping As Integer = 1
        If findLast Then
            first = last
            last = 0
            stepping = -1
        End If

        For i As Integer = first To last Step stepping
            If endsWith AndAlso Me.Items(i).ToString.EndsWith(value) Then Return i
            If endsWith = False AndAlso Me.Items(i).ToString.StartsWith(value) Then Return i
        Next i

        Return -1
    End Function

    Public Sub forceManaging()
        If _InModification = True Then Exit Sub
        Dim lastSelection As Integer = MyBase.SelectionStart
        Dim mySel As Integer
        Dim WordCapital, allWords As Boolean
        If firstLetterCapital = True Or firstLettersCapital = True Then
            WordCapital = True
        Else
            WordCapital = False
        End If
        allWords = firstLettersCapital

        _InModification = True
        MyValue = MyBase.Text

        If Chaines.forceManaging(MyValue, _CurrencyBox, AcceptedText, _AcceptAlpha, _AcceptNumeric, _OnlyAlphabet, _RefuseAccents, _AcceptedChars, _RefusedChars, _MatchExp, WordCapital, allWords, _AllCapital, _AllLower, _NbDecimals, acceptNegative, lastSelection, blockOnMinimum, minimum, blockOnMaximum, maximum, _cb_AcceptLeftZeros) = False Then _InModification = False : Exit Sub

        MyValue = MyValue.TrimStart

        Dim autoCompleted As Boolean = False
        If autoComplete = True And keyPressed <> Keys.Back And keyPressed <> Keys.Delete And Me.Enabled = True Then autoCompleted = autoCompleting()
        If MyValue = "" And Me.currencyBox Then MyValue = 0

        MyBase.Text = MyValue
        If lastSelection < 0 Then lastSelection = 0
        If MyValue <> "" Then
            If autoComplete = True And autoCompleted = True And keyPressed <> Keys.Back And keyPressed <> Keys.Delete Then
                Try
                    MyBase.SelectionStart = lastSelection
                    mySel = MyValue.Length - lastSelection
                    If mySel > 0 And Me.Enabled = True And Me.SelectionStart > 0 Then MyBase.SelectionLength = mySel
                Catch REM Exception not handle
                    REM Erreur de sélection non trouvé, non important
                End Try
            Else
                MyBase.SelectionStart = lastSelection
            End If
        End If

        _InModification = False
    End Sub

    Private Sub managedCombo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click

    End Sub

    Private Sub managedCombo_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DropDown
        'Me.DroppedDown = False
    End Sub

    Private curText As String = ""

    Private Sub managedCombo_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.TextChanged
        If _ManageText = True And _ReadOnly = False Then forceManaging()
        curText = Me.Text
    End Sub

    Private Sub managedCombo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If _ManageText = True Then
            If InStr(MyBase.Text, ",") > 0 And (e.KeyCode = Keys.Oemcomma Or e.KeyCode = Keys.OemPeriod) Then
                AcceptedText = MyBase.Text
                mySelectionStart = MyBase.SelectionStart
            Else
                AcceptedText = ""
            End If
        End If
    End Sub

    Protected Overrides Sub onPreviewKeyDown(ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs)
        keyPressed = e.KeyCode
        If keyPressed = Keys.Enter Or keyPressed = Keys.Tab Then Me.DroppedDown = False
        MyBase.OnPreviewKeyDown(e)
    End Sub

    Protected Overrides Function processKeyMessage(ByRef m As System.Windows.Forms.Message) As Boolean
        If Me.ReadOnly And m.WParam.ToInt32 <> 37 And m.WParam.ToInt32 <> 39 And m.WParam.ToInt32 <> 13 Then Return True

        Return MyBase.ProcessKeyMessage(m)
    End Function

    'Private Function deleteFromDb(ByVal currentValue As String, ByVal currentItem As String) As Boolean
    '    Dim SDBField() As String = Split(DBField, ".")
    '    If DBLinker.GetInstance.ReadOneDBField(SDBField(0), "COUNT(" & SDBField(1) & ")", "WHERE " & SDBField(1) & "='" & CurrentItem.Replace("'", "''") & "' COLLATE French_CI_AS")(0) = "0" Then
    '        MessageBox.Show("Impossible de supprimer l'élément de la liste, car il a déjà été supprimé." & vbCrLf & "Veuillez sélectionner un autre élément à supprimer.", "Élément déjà supprimé", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Me.Items.Remove(CurrentItem)
    '        Return False
    '    End If

    '    Dim doneBool As Boolean = False
    '    Dim UsageTables(,) As String = DBLinker.GetInstance.ReadDB("ClinicaTablesLinked", "TableNameLinked,TableNameLinkedColumn,(SELECT TOP 1 PrimaryKey FROM ClinicaTables WHERE Tablename='" & SDBField(0).Replace("'", "''") & "')", "WHERE Tablename='" & SDBField(0).Replace("'", "''") & "'")
    '    Dim NbCount As Integer = 0
    '    If UsageTables IsNot Nothing AndAlso UsageTables.Length <> 0 Then
    '        Dim SqlStr As String = ""
    '        For i As Integer = 0 To UsageTables.GetUpperBound(1)
    '            SqlStr &= "+(SELECT COUNT(*) FROM " & UsageTables(0, i) & " WHERE " & UsageTables(0, i) & "." & UsageTables(1, i) & "=" & SDBField(0) & "." & UsageTables(2, i) & ")"
    '        Next i
    '        SqlStr = SqlStr.Substring(1)
    '        SqlStr = "SELECT " & SqlStr & " FROM " & SDBField(0) & " WHERE " & DBField & "='" & CurrentItem.Replace("'", "''") & "'"
    '        Dim elementCount(,) As String = DBLinker.GetInstance.ReadDB(SqlStr)
    '        If elementCount Is Nothing OrElse elementCount.Length = 0 Then
    '            MessageBox.Show("L'élément de la liste que vous vouliez supprimé n'existe plus.", "Élément inexistant")
    '        Else
    '            NbCount = elementCount(0, 0)
    '        End If
    '    End If

    '    If NbCount = 0 Then DoneBool = DBLinker.GetInstance.DelDB(SDBField(0), SDBField(1), "'" & CurrentItem.Replace("'", "''") & "' COLLATE French_CI_AS", False, , False)
    '    If DoneBool = False Or NbCount <> 0 Then
    '        If CurrentValue = "" OrElse CurrentValue = CurrentItem OrElse Me.Items.Contains(CurrentValue) = False Then
    '            MessageBox.Show("Impossible de supprimer l'élément de la liste, car il est présentement en utilisation." & vbCrLf & "Veuillez d'abord sélectionner un élément existant et différent depuis la liste déroulante qui remplacera celui à supprimer.", "Élément déjà utilisé", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            doneBool = False
    '        Else
    '            If MessageBox.Show("Impossible de supprimer l'élément de la liste, car il est présentement en utilisation." & vbCrLf & "Désirez-vous modifier ceux qui l'utilise (" & CurrentItem & ") par l'élément de la liste en cours (" & CurrentValue & ") ?", "Élément déjà utilisé", MessageBoxButtons.YesNo) = DialogResult.Yes Then
    '                Me.Text = CurrentValue
    '                doneBool = SwitchItemsForCurrent(CurrentItem, SDBField(0), SDBField(1))
    '                doneBool = doneBool AndAlso DBLinker.GetInstance.DelDB(SDBField(0), SDBField(1), "'" & CurrentItem.Replace("'", "''") & "' AND " & SDBField(1) & "<>'" & CurrentValue.Replace("'", "''") & "' COLLATE French_CI_AS", False, , False)
    '                Me.Text = CurrentItem
    '            End If
    '        End If
    '    End If

    '    Return doneBool
    'End Function

    Protected Overrides Function processKeyEventArgs(ByRef m As System.Windows.Forms.Message) As Boolean
        Dim oldManageText As Boolean = Me.manageText
        Me.manageText = False

        If m.WParam.ToInt32 = 46 Then 'Del Key pressed
            If Me.ReadOnly = False And Me.DroppedDown = True And Me.SelectedIndex >= 0 And (pathOfList <> "" Or dbField <> "") Then
                'TODO
                'REM SHALL be transfered to a derived class which add this code into the DeleteItem event
                ''Droit & Accès
                'If CurrentDroitAcces(54) = False Then
                '    'Message & Exit
                '    MessageBox.Show("Vous n'avez pas le droit de supprimer les éléments d'une liste déroulante d'un formulaire." & vbCrLf & "Merci!", "Droit & Accès")
                '    Exit Function
                'End If
                'Dim CurrentValue As String = Me.Text
                'Dim ErrInt As Integer = -1
                'Dim DoneBool As Boolean = False
                'If MessageBox.Show("Êtes-vous sûr de vouloir supprimer cet élément de la liste ?", "Confirmation de suppression", MessageBoxButtons.YesNo) = DialogResult.No Then Me.Text = CurrentValue : Return True

                'Dim CurrentItem As String = Me.GetItemText(Me.Items(Me.SelectedIndex))
                'RaiseEvent DeleteItem(CurrentItem)

                'If _DoComboDelete = False Then Exit Function 'Quitte si ne doit pas exécuter la suppression interne
                'If DBField <> "" Then
                '    DoneBool = deleteFromDb(CurrentValue, CurrentItem)
                'ElseIf PathOfList <> "" Then
                '    ErrInt = RemoveItemToAList(Me.PathOfList, CurrentItem, , True)
                'End If
                'If ErrInt > 0 Or DoneBool = True Then
                '    Me.Items.RemoveAt(Me.SelectedIndex)
                '    Me.SelectedIndex = -1
                '    If CurrentItem <> CurrentValue Then
                '        Me.SelectedIndex = Me.FindStringExact(CurrentValue)
                '    Else
                '        Me.Text = ""
                '    End If
                '    RaiseEvent DeletedItem(CurrentItem)
                'Else
                '    Me.Text = CurrentValue
                'End If

                Return True
            End If
        End If

        Me.manageText = oldManageText
        MyBase.ProcessKeyEventArgs(m)
    End Function

    'Private Function switchItemsForCurrent(ByVal switchItem As String, ByVal tableName As String, ByVal field As String) As Boolean
    '    Dim TablesLinked(,) As String = DBLinker.GetInstance.ReadDB("ClinicaTablesLinked", "TableNameLinked,TableNameLinkedColumn", "WHERE TableName='" & TableName.Replace("'", "''") & "'")
    '    If TablesLinked Is Nothing OrElse TablesLinked.Length = 0 Then Return False

    '    Dim NoItems(,) As String = DBLinker.GetInstance.ReadDB(TableName, Field & ",*", "WHERE (" & Field & "='" & SwitchItem.Replace("'", "''") & "' COLLATE French_CI_AS) OR (" & Field & "='" & Me.Text.Replace("'", "''") & "' COLLATE French_CI_AS)")
    '    Dim NoCurrent As String
    '    Dim NoSwitch As Integer
    '    If NoItems.Length = 3 Then
    '        Return False
    '    Else
    '        If NoItems(0, 0) = SwitchItem Then
    '            NoSwitch = NoItems(1, 0)
    '            NoCurrent = NoItems(1, 1)
    '        Else
    '            NoSwitch = NoItems(1, 1)
    '            NoCurrent = NoItems(1, 0)
    '        End If
    '    End If

    '    For i As Integer = 0 To TablesLinked.GetUpperBound(1)
    '        DBLinker.GetInstance.UpdateDB(TablesLinked(0, i), TablesLinked(1, i) & "=" & NoCurrent, TablesLinked(1, i), NoSwitch, False)
    '    Next i

    '    Return True
    'End Function

    Protected Overrides Sub onLostFocus(ByVal e As System.EventArgs)
        If _TrimText = True Then MyBase.Text = Trim(MyBase.Text)
    End Sub

    Protected Overrides Sub onDropDown(ByVal e As System.EventArgs)
        If Me.ReadOnly Then
            Me.SuspendLayout()
            Me.DrawMode = Windows.Forms.DrawMode.OwnerDrawVariable
        End If
        If _AutoSizeDropDown Then
            Dim maxWidth As Integer = 0
            For i As Integer = 0 To Me.Items.Count - 1
                maxWidth = Math.Max(measureString(Me.Items(i).ToString, Me.Font).Width, maxWidth)
            Next i

            If maxWidth <> 0 Then Me.DropDownWidth = maxWidth + 15
        End If
        MyBase.OnDropDown(e)
    End Sub


    Protected Overrides Sub onMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        If Me.ReadOnly And Me.DroppedDown Then
            Me.DroppedDown = False
            Me.DrawMode = Windows.Forms.DrawMode.Normal
            Me.ResumeLayout()
            Me.Select()
            Me.Focus()
        End If

        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub onValidating(ByVal e As System.ComponentModel.CancelEventArgs)
        If Me.ReadOnly And Me.Text <> curText Then
            e.Cancel = True
            Me.Text = curText
        End If

        MyBase.OnValidating(e)
    End Sub

    Private Sub managedCombo_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MouseLeave
        If itemsToolTip IsNot Nothing Then Me.itemsToolTip.Hide(Me.FindForm)
    End Sub

    Private Sub managedCombo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Validating

    End Sub

    Private Sub managedCombo_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Validated
        If Me.ReadOnly = False AndAlso Me.Enabled = True Then Me.Text = Me.Text.Trim
    End Sub
End Class

