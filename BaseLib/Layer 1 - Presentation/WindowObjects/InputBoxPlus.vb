Public Class InputBoxPlus
    Inherits System.Windows.Forms.Form

    Private isCombo As Boolean = False

#Region " Windows Form Designer generated code "

    Public Sub New(Optional ByVal isComboBox As Boolean = False, Optional ByVal pathList As String = "", Optional ByVal dbField As String = "", Optional ByVal whereSource As String = "", Optional ByVal doComboDelete As Boolean = True)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        Me.whereSource = whereSource
        isCombo = isComboBox
        If isComboBox Then
            Me.MyPrompt = New ManagedCombo()
            AddHandler CType(MyPrompt, ManagedCombo).deleteItem, AddressOf internalComboDelete
        Else
            Me.MyPrompt = New ManagedText()
        End If

        '
        'MyPrompt
        '
        If isComboBox Then
            With CType(Me.MyPrompt, ManagedCombo)
                .acceptAlpha = True
                .acceptedChars = Nothing
                .acceptNumeric = True
                .allCapital = False
                .allLower = False
                .cb_AcceptNegative = False
                .currencyBox = False
                .firstLetterCapital = False
                .firstLettersCapital = False
                .matchExp = ""
                .nbDecimals = CType(-1, Short)
                .onlyAlphabet = False
                .refuseAccents = False
                .refusedChars = ""
                .pathOfList = pathList
                .dbField = dbField
                .doComboDelete = doComboDelete
            End With
        Else
            With CType(Me.MyPrompt, ManagedText)
                .acceptAlpha = True
                .acceptedChars = Nothing
                .acceptNumeric = True
                .allCapital = False
                .allLower = False
                .cb_AcceptNegative = False
                .currencyBox = False
                .firstLetterCapital = False
                .firstLettersCapital = False
                .matchExp = ""
                .nbDecimals = CType(-1, Short)
                .onlyAlphabet = False
                .refuseAccents = False
                .refusedChars = ""
            End With
        End If

        Me.MyPrompt.Name = "MyPrompt"
        Me.MyPrompt.Size = New System.Drawing.Size(424, 20)
        Me.MyPrompt.TabIndex = 2
        Me.MyPrompt.Text = ""
        Me.MyPrompt.Location = New System.Drawing.Point(8, 72)

        Me.Controls.Add(Me.MyPrompt)
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If isCombo Then RemoveHandler CType(MyPrompt, ManagedCombo).deleteItem, AddressOf internalComboDelete
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ok As System.Windows.Forms.Button
    Friend WithEvents annuler As System.Windows.Forms.Button
    Friend WithEvents MyPrompt As System.Windows.Forms.Control
    Friend WithEvents IncrementBox As System.Windows.Forms.Panel
    Friend WithEvents ALabel As System.Windows.Forms.Label
    Friend WithEvents Increment As System.Windows.Forms.CheckBox
    Friend WithEvents MyText As System.Windows.Forms.TextBox
    Friend WithEvents MyText2 As System.Windows.Forms.Label
    Friend WithEvents Upto As ManagedText
    Friend WithEvents From As ManagedText
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents NombreChiffre As ManagedText
    Friend WithEvents Exemple As System.Windows.Forms.Label
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.ok = New System.Windows.Forms.Button
        Me.annuler = New System.Windows.Forms.Button
        Me.IncrementBox = New System.Windows.Forms.Panel
        Me.NombreChiffre = New ManagedText
        Me.Label1 = New System.Windows.Forms.Label
        Me.ALabel = New System.Windows.Forms.Label
        Me.Upto = New ManagedText
        Me.From = New ManagedText
        Me.Increment = New System.Windows.Forms.CheckBox
        Me.MyText = New System.Windows.Forms.TextBox
        Me.MyText2 = New System.Windows.Forms.Label
        Me.Exemple = New System.Windows.Forms.Label
        Me.IncrementBox.SuspendLayout()
        Me.SuspendLayout()
        '
        'ok
        '
        Me.ok.Location = New System.Drawing.Point(376, 8)
        Me.ok.Name = "ok"
        Me.ok.Size = New System.Drawing.Size(56, 24)
        Me.ok.TabIndex = 0
        Me.ok.Text = "OK"
        '
        'annuler
        '
        Me.annuler.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.annuler.Location = New System.Drawing.Point(376, 40)
        Me.annuler.Name = "annuler"
        Me.annuler.Size = New System.Drawing.Size(56, 24)
        Me.annuler.TabIndex = 1
        Me.annuler.Text = "Annuler"
        '
        'IncrementBox
        '
        Me.IncrementBox.Controls.Add(Me.NombreChiffre)
        Me.IncrementBox.Controls.Add(Me.Label1)
        Me.IncrementBox.Controls.Add(Me.ALabel)
        Me.IncrementBox.Controls.Add(Me.Upto)
        Me.IncrementBox.Controls.Add(Me.From)
        Me.IncrementBox.Controls.Add(Me.Increment)
        Me.IncrementBox.Location = New System.Drawing.Point(8, 48)
        Me.IncrementBox.Name = "IncrementBox"
        Me.IncrementBox.Size = New System.Drawing.Size(360, 24)
        Me.IncrementBox.TabIndex = 8
        '
        'NombreChiffre
        '
        Me.NombreChiffre.acceptAlpha = False
        Me.NombreChiffre.acceptedChars = ",§."
        Me.NombreChiffre.acceptNumeric = True
        Me.NombreChiffre.allCapital = False
        Me.NombreChiffre.allLower = False
        Me.NombreChiffre.cb_AcceptNegative = False
        Me.NombreChiffre.currencyBox = True
        Me.NombreChiffre.firstLetterCapital = False
        Me.NombreChiffre.firstLettersCapital = False
        Me.NombreChiffre.Location = New System.Drawing.Point(328, 2)
        Me.NombreChiffre.matchExp = ""
        Me.NombreChiffre.Name = "NombreChiffre"
        Me.NombreChiffre.nbDecimals = CType(0, Short)
        Me.NombreChiffre.onlyAlphabet = False
        Me.NombreChiffre.refuseAccents = False
        Me.NombreChiffre.refusedChars = ""
        Me.NombreChiffre.Size = New System.Drawing.Size(32, 20)
        Me.NombreChiffre.TabIndex = 13
        Me.NombreChiffre.Text = "1"
        Me.NombreChiffre.trimText = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(248, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Nb de chiffres :"
        '
        'ALabel
        '
        Me.ALabel.AutoSize = True
        Me.ALabel.Location = New System.Drawing.Point(192, 4)
        Me.ALabel.Name = "ALabel"
        Me.ALabel.Size = New System.Drawing.Size(13, 13)
        Me.ALabel.TabIndex = 11
        Me.ALabel.Text = "à"
        '
        'Upto
        '
        Me.Upto.acceptAlpha = False
        Me.Upto.acceptedChars = ""
        Me.Upto.acceptNumeric = True
        Me.Upto.allCapital = False
        Me.Upto.allLower = False
        Me.Upto.cb_AcceptNegative = False
        Me.Upto.currencyBox = False
        Me.Upto.firstLetterCapital = False
        Me.Upto.firstLettersCapital = False
        Me.Upto.Location = New System.Drawing.Point(208, 2)
        Me.Upto.matchExp = ""
        Me.Upto.Name = "Upto"
        Me.Upto.nbDecimals = CType(0, Short)
        Me.Upto.onlyAlphabet = False
        Me.Upto.refuseAccents = False
        Me.Upto.refusedChars = ""
        Me.Upto.Size = New System.Drawing.Size(32, 20)
        Me.Upto.TabIndex = 10
        Me.Upto.Text = "1"
        Me.Upto.trimText = False
        '
        'From
        '
        Me.From.acceptAlpha = False
        Me.From.acceptedChars = ""
        Me.From.acceptNumeric = True
        Me.From.allCapital = False
        Me.From.allLower = False
        Me.From.cb_AcceptNegative = False
        Me.From.currencyBox = False
        Me.From.firstLetterCapital = False
        Me.From.firstLettersCapital = False
        Me.From.Location = New System.Drawing.Point(152, 2)
        Me.From.matchExp = ""
        Me.From.Name = "From"
        Me.From.nbDecimals = CType(0, Short)
        Me.From.onlyAlphabet = False
        Me.From.refuseAccents = False
        Me.From.refusedChars = ""
        Me.From.Size = New System.Drawing.Size(32, 20)
        Me.From.TabIndex = 9
        Me.From.Text = "1"
        Me.From.trimText = False
        '
        'Increment
        '
        Me.Increment.Location = New System.Drawing.Point(0, 2)
        Me.Increment.Name = "Increment"
        Me.Increment.Size = New System.Drawing.Size(160, 20)
        Me.Increment.TabIndex = 8
        Me.Increment.Text = "Incrémenter \# à partir de :"
        '
        'MyText
        '
        Me.MyText.BackColor = System.Drawing.SystemColors.Control
        Me.MyText.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.MyText.Location = New System.Drawing.Point(8, 8)
        Me.MyText.Multiline = True
        Me.MyText.Name = "MyText"
        Me.MyText.ReadOnly = True
        Me.MyText.Size = New System.Drawing.Size(100, 20)
        Me.MyText.TabIndex = 9
        '
        'MyText2
        '
        Me.MyText2.AutoSize = True
        Me.MyText2.Location = New System.Drawing.Point(8, 8)
        Me.MyText2.Name = "MyText2"
        Me.MyText2.Size = New System.Drawing.Size(0, 13)
        Me.MyText2.TabIndex = 12
        Me.MyText2.Visible = False
        '
        'Exemple
        '
        Me.Exemple.BackColor = System.Drawing.SystemColors.Info
        Me.Exemple.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Exemple.Location = New System.Drawing.Point(288, 24)
        Me.Exemple.Name = "Exemple"
        Me.Exemple.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Exemple.Size = New System.Drawing.Size(80, 16)
        Me.Exemple.TabIndex = 13
        Me.Exemple.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Exemple.Visible = False
        '
        'InputBoxPlus
        '
        Me.AcceptButton = Me.ok
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.annuler
        Me.ClientSize = New System.Drawing.Size(442, 102)
        Me.Controls.Add(Me.MyText2)
        Me.Controls.Add(Me.Exemple)
        Me.Controls.Add(Me.MyText)
        Me.Controls.Add(Me.IncrementBox)
        Me.Controls.Add(Me.annuler)
        Me.Controls.Add(Me.ok)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "InputBoxPlus"
        Me.ShowIcon = False
        Me.Text = "InputBoxPlus"
        Me.IncrementBox.ResumeLayout(False)
        Me.IncrementBox.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private whereSource As String
    Private myAnswer As String
    Private _Separator As String
    Public Event comboDelete(ByVal currentItem As String)

#Region "Propriétés"
    Default Public ReadOnly Property Prompt(ByVal Texte As String, ByVal Titre As String, Optional ByVal DefaultPrompt As String = "", Optional ByVal ShowIncrement As Boolean = False, Optional ByVal Separator As String = "§") As String
        Get
            Dim espacement As Byte = 8
            Dim maximumTexteWidth As Integer = Screen.PrimaryScreen.WorkingArea.Width / 2
            Me.Text = Titre
            _Separator = Separator

            MyText2.Text = Texte
            MyText.Text = Texte
            If MyText2.Width > maximumTexteWidth Or Texte.IndexOf(vbCrLf) <> -1 Then
                MyText.Width = maximumTexteWidth
                MyText.Height *= 2
            Else
                MyText.Width = MyText2.Width + 12
            End If

            MyPrompt.Text = DefaultPrompt
            If isCombo = True Then loadCombo()

            Dim okAnnulerLocation As Integer = MyText.Width + espacement * 2
            If okAnnulerLocation > ok.Left Then
                ok.Left = okAnnulerLocation
                annuler.Left = okAnnulerLocation
            End If

            Me.Width = ok.Left + ok.Width + espacement * 2
            MyPrompt.Width = Me.Width - espacement * 3

            IncrementBox.Visible = ShowIncrement

            Me.ShowDialog()
            Return myAnswer
        End Get
    End Property

    Public Property acceptAlpha() As Boolean
        Get
            If isCombo = True Then
                Return CType(MyPrompt, ManagedCombo).acceptAlpha
            Else
                Return CType(MyPrompt, ManagedText).acceptAlpha
            End If
        End Get
        Set(ByVal Value As Boolean)
            If isCombo = True Then
                CType(MyPrompt, ManagedCombo).acceptAlpha = Value
            Else
                CType(MyPrompt, ManagedText).acceptAlpha = Value
            End If
        End Set
    End Property

    Public Property acceptedChars() As String
        Get
            If isCombo = True Then
                Return CType(MyPrompt, ManagedCombo).acceptedChars
            Else
                Return CType(MyPrompt, ManagedText).acceptedChars
            End If
        End Get
        Set(ByVal Value As String)
            If isCombo = True Then
                CType(MyPrompt, ManagedCombo).acceptedChars = Value
            Else
                CType(MyPrompt, ManagedText).acceptedChars = Value
            End If
        End Set
    End Property

    Public Property acceptNumeric() As Boolean
        Get
            If isCombo = True Then
                Return CType(MyPrompt, ManagedCombo).acceptNumeric
            Else
                Return CType(MyPrompt, ManagedText).acceptNumeric
            End If
        End Get
        Set(ByVal Value As Boolean)
            If isCombo = True Then
                CType(MyPrompt, ManagedCombo).acceptNumeric = Value
            Else
                CType(MyPrompt, ManagedText).acceptNumeric = Value
            End If
        End Set
    End Property

    Public Property allCapital() As Boolean
        Get
            If isCombo = True Then
                Return CType(MyPrompt, ManagedCombo).allCapital
            Else
                Return CType(MyPrompt, ManagedText).allCapital
            End If
        End Get
        Set(ByVal Value As Boolean)
            If isCombo = True Then
                CType(MyPrompt, ManagedCombo).allCapital = Value
            Else
                CType(MyPrompt, ManagedText).allCapital = Value
            End If
        End Set
    End Property

    Public Property allLower() As Boolean
        Get
            If isCombo = True Then
                Return CType(MyPrompt, ManagedCombo).allLower
            Else
                Return CType(MyPrompt, ManagedText).allLower
            End If
        End Get
        Set(ByVal Value As Boolean)
            If isCombo = True Then
                CType(MyPrompt, ManagedCombo).allLower = Value
            Else
                CType(MyPrompt, ManagedText).allLower = Value
            End If
        End Set
    End Property

    Public Property cb_AcceptNegative() As Boolean
        Get
            If isCombo = True Then
                Return CType(MyPrompt, ManagedCombo).cb_AcceptNegative
            Else
                Return CType(MyPrompt, ManagedText).cb_AcceptNegative
            End If
        End Get
        Set(ByVal Value As Boolean)
            If isCombo = True Then
                CType(MyPrompt, ManagedCombo).cb_AcceptNegative = Value
            Else
                CType(MyPrompt, ManagedText).cb_AcceptNegative = Value
            End If
        End Set
    End Property

    Public Property currencyBox() As Boolean
        Get
            If isCombo = True Then
                Return CType(MyPrompt, ManagedCombo).currencyBox
            Else
                Return CType(MyPrompt, ManagedText).currencyBox
            End If
        End Get
        Set(ByVal Value As Boolean)
            If isCombo = True Then
                CType(MyPrompt, ManagedCombo).currencyBox = Value
            Else
                CType(MyPrompt, ManagedText).currencyBox = Value
            End If
        End Set
    End Property

    Public Property blockOnMaximum() As Boolean
        Get
            If isCombo = True Then
                Return CType(MyPrompt, ManagedCombo).blockOnMaximum
            Else
                Return CType(MyPrompt, ManagedText).blockOnMaximum
            End If
        End Get
        Set(ByVal Value As Boolean)
            If isCombo = True Then
                CType(MyPrompt, ManagedCombo).blockOnMaximum = Value
            Else
                CType(MyPrompt, ManagedText).blockOnMaximum = Value
            End If
        End Set
    End Property

    Public Property blockOnMinimum() As Boolean
        Get
            If isCombo = True Then
                Return CType(MyPrompt, ManagedCombo).blockOnMinimum
            Else
                Return CType(MyPrompt, ManagedText).blockOnMinimum
            End If
        End Get
        Set(ByVal Value As Boolean)
            If isCombo = True Then
                CType(MyPrompt, ManagedCombo).blockOnMinimum = Value
            Else
                CType(MyPrompt, ManagedText).blockOnMinimum = Value
            End If
        End Set
    End Property


    Public Property maximum() As Integer
        Get
            If isCombo = True Then
                Return CType(MyPrompt, ManagedCombo).maximum
            Else
                Return CType(MyPrompt, ManagedText).maximum
            End If
        End Get
        Set(ByVal Value As Integer)
            If isCombo = True Then
                CType(MyPrompt, ManagedCombo).maximum = Value
            Else
                CType(MyPrompt, ManagedText).maximum = Value
            End If
        End Set
    End Property

    Public Property minimum() As Integer
        Get
            If isCombo = True Then
                Return CType(MyPrompt, ManagedCombo).minimum
            Else
                Return CType(MyPrompt, ManagedText).minimum
            End If
        End Get
        Set(ByVal Value As Integer)
            If isCombo = True Then
                CType(MyPrompt, ManagedCombo).minimum = Value
            Else
                CType(MyPrompt, ManagedText).minimum = Value
            End If
        End Set
    End Property

    Public Property firstLettersCapital() As Boolean
        Get
            If isCombo = True Then
                Return CType(MyPrompt, ManagedCombo).firstLettersCapital
            Else
                Return CType(MyPrompt, ManagedText).firstLettersCapital
            End If
        End Get
        Set(ByVal Value As Boolean)
            If isCombo = True Then
                CType(MyPrompt, ManagedCombo).firstLettersCapital = Value
            Else
                CType(MyPrompt, ManagedText).firstLettersCapital = Value
            End If
        End Set
    End Property

    Public Property firstLetterCapital() As Boolean
        Get
            If isCombo = True Then
                Return CType(MyPrompt, ManagedCombo).firstLetterCapital
            Else
                Return CType(MyPrompt, ManagedText).firstLetterCapital
            End If
        End Get
        Set(ByVal Value As Boolean)
            If isCombo = True Then
                CType(MyPrompt, ManagedCombo).firstLetterCapital = Value
            Else
                CType(MyPrompt, ManagedText).firstLetterCapital = Value
            End If
        End Set
    End Property

    Public Property matchExp() As String
        Get
            If isCombo = True Then
                Return CType(MyPrompt, ManagedCombo).matchExp
            Else
                Return CType(MyPrompt, ManagedText).matchExp
            End If
        End Get
        Set(ByVal Value As String)
            If isCombo = True Then
                CType(MyPrompt, ManagedCombo).matchExp = Value
            Else
                CType(MyPrompt, ManagedText).matchExp = Value
            End If
        End Set
    End Property

    Public Property onlyAlphabet() As Boolean
        Get
            If isCombo = True Then
                Return CType(MyPrompt, ManagedCombo).onlyAlphabet
            Else
                Return CType(MyPrompt, ManagedText).onlyAlphabet
            End If
        End Get
        Set(ByVal Value As Boolean)
            If isCombo = True Then
                CType(MyPrompt, ManagedCombo).onlyAlphabet = Value
            Else
                CType(MyPrompt, ManagedText).onlyAlphabet = Value
            End If
        End Set
    End Property

    Public Property refuseAccents() As Boolean
        Get
            If isCombo = True Then
                Return CType(MyPrompt, ManagedCombo).refuseAccents
            Else
                Return CType(MyPrompt, ManagedText).refuseAccents
            End If
        End Get
        Set(ByVal Value As Boolean)
            If isCombo = True Then
                CType(MyPrompt, ManagedCombo).refuseAccents = Value
            Else
                CType(MyPrompt, ManagedText).refuseAccents = Value
            End If
        End Set
    End Property

    Public Property nbDecimals() As Integer
        Get
            If isCombo = True Then
                Return CType(MyPrompt, ManagedCombo).nbDecimals
            Else
                Return CType(MyPrompt, ManagedText).nbDecimals
            End If
        End Get
        Set(ByVal Value As Integer)
            If isCombo = True Then
                CType(MyPrompt, ManagedCombo).nbDecimals = Value
            Else
                CType(MyPrompt, ManagedText).nbDecimals = Value
            End If
        End Set
    End Property

    Public Property refusedChars() As String
        Get
            If isCombo = True Then
                Return CType(MyPrompt, ManagedCombo).refusedChars
            Else
                Return CType(MyPrompt, ManagedText).refusedChars
            End If
        End Get
        Set(ByVal Value As String)
            If isCombo = True Then
                CType(MyPrompt, ManagedCombo).refusedChars = Value
            Else
                CType(MyPrompt, ManagedText).refusedChars = Value
            End If
        End Set
    End Property

    Public Property incrementFrom() As Integer
        Get
            Return From.Text
        End Get
        Set(ByVal Value As Integer)
            From.Text = Value
        End Set
    End Property

    Public Property passwordChar() As Char
        Get
            If isCombo = True Then
                Return ""
            Else
                Return CType(MyPrompt, ManagedText).PasswordChar
            End If
        End Get
        Set(ByVal Value As Char)
            If isCombo = False Then
                CType(MyPrompt, ManagedText).PasswordChar = Value
            End If
        End Set
    End Property

    Public Property maxLength() As Integer
        Get
            If isCombo = True Then
                Return CType(MyPrompt, ManagedCombo).MaxLength
            Else
                Return CType(MyPrompt, ManagedText).MaxLength
            End If
        End Get
        Set(ByVal Value As Integer)
            If isCombo = True Then
                CType(MyPrompt, ManagedCombo).MaxLength = Value
            Else
                CType(MyPrompt, ManagedText).MaxLength = Value
            End If
        End Set
    End Property

    Public Property incrementTo() As Integer
        Get
            Return Upto.Text
        End Get
        Set(ByVal Value As Integer)
            Upto.Text = Value
        End Set
    End Property

    Public Property incrementChecked() As Boolean
        Get
            Return Increment.Checked
        End Get
        Set(ByVal Value As Boolean)
            Increment.Checked = Value
        End Set
    End Property
#End Region

    Public Sub loadCombo()
        If isCombo = False Then Exit Sub

        With CType(MyPrompt, ManagedCombo)
            If .dbField <> "" Then
                Dim sdbField() As String = .dbField.Split(New Char() {"."})
                .Items.Clear()

                Dim loadingData() As String = DBLinker.getInstance.readOneDBField(sdbField(0), sdbField(1), whereSource, True, , False)
                If Not loadingData Is Nothing AndAlso loadingData.Length <> 0 Then .Items.AddRange(loadingData)
                Exit Sub
            End If
            If .pathOfList <> "" Then
                .Items.Clear()

                If IO.File.Exists(.pathOfList) Then
                    Dim loadingData() As String = IO.File.ReadAllLines(.pathOfList)
                    .Items.AddRange(loadingData)
                End If
            End If
        End With
    End Sub

    Private Sub internalComboDelete(ByVal currentItem As String)
        RaiseEvent comboDelete(currentItem)
    End Sub

    Private Sub annuler_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles annuler.Click
        myAnswer = ""
        Me.Close()
    End Sub

    Private Sub ok_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ok.Click
        Dim verifDialogResult As DialogResult
        If MyPrompt.Text.IndexOf("\#") <> -1 And Increment.Checked = False Then
            verifDialogResult = MessageBox.Show("Vous avez entré les caratères d'incrémentation (\#). Désirez-vous incrémenter ?", "Incrémentation", MessageBoxButtons.YesNoCancel)
            If verifDialogResult = DialogResult.Yes Then
                Increment.Checked = True
            ElseIf verifDialogResult = System.Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If
        End If
        If MyPrompt.Text.IndexOf("\#") = -1 And Increment.Checked = True Then
            verifDialogResult = MessageBox.Show("Vous avez coché l'incrémentation, mais les caratères d'incrémentation (\#) ne s'y trouve pas. Désirez-vous incrémenter ?", "Incrémentation", MessageBoxButtons.YesNoCancel)
            If verifDialogResult = DialogResult.No Then
                Increment.Checked = False
            Else
                Exit Sub
            End If
        End If

        If Increment.Checked = False Then
            myAnswer = MyPrompt.Text
        Else
            If From.Text = "" Then MessageBox.Show("Veuillez entrer l'incrémentation de départ", "Information manquante") : From.Focus() : Exit Sub
            If Upto.Text = "" Then MessageBox.Show("Veuillez entrer l'incrémentation de fin", "Information manquante") : From.Focus() : Exit Sub
            If MyPrompt.Text.IndexOf("\#") = -1 Then MessageBox.Show("Veuillez entrer le symbole d'incrémentation (\#) dans la boîte de texte ou décocher l'incrémentation", "Information manquante") : MyPrompt.Focus() : Exit Sub

            Dim i As Integer
            For i = From.Text To Upto.Text
                myAnswer &= _Separator & MyPrompt.Text.Replace("\#", Strings.addZeros(i, NombreChiffre.Text))
            Next i

            myAnswer = myAnswer.Substring(_Separator.Length)
        End If

        Me.Close()
    End Sub

    Private Sub inputBoxPlus_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Escape Then Me.Close() : e.Handled = True
    End Sub

    Private Sub inputBoxPlus_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Centré la fenêtre
        Me.Top = (Screen.PrimaryScreen.Bounds.Height - Me.Height) / 2
        Me.Left = (Screen.PrimaryScreen.Bounds.Width - Me.Width) / 2
    End Sub

    Private Sub inputBoxPlus_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        If isCombo Then
            With CType(MyPrompt, ManagedCombo)
                .SelectionStart = 0
                .SelectionLength = .Text.Length
            End With
        Else
            With CType(MyPrompt, ManagedText)
                .SelectionStart = 0
                .SelectionLength = .TextLength
            End With
        End If
        MyPrompt.Focus()
    End Sub

    Private Sub from_Upto_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Upto.TextChanged, From.TextChanged
        If Integer.TryParse(Upto.Text, 0) AndAlso Integer.TryParse(From.Text, 0) AndAlso Integer.Parse(Upto.Text) < Integer.Parse(From.Text) Then Upto.Text = From.Text
    End Sub

    Private Sub nombreChiffre_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NombreChiffre.TextChanged
        Exemple.Text = addZeros(NombreChiffre.Text, NombreChiffre.Text)
    End Sub

    Private Sub nombreChiffre_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles NombreChiffre.GotFocus
        Exemple.Visible = True
    End Sub

    Private Sub nombreChiffre_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles NombreChiffre.LostFocus
        Exemple.Visible = False
    End Sub
End Class
