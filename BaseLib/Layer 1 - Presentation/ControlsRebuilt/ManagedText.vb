Imports System.Drawing

Public Class ManagedText
    Inherits System.Windows.Forms.TextBox

    Private _AcceptAlpha As Boolean = True
    Private _AcceptNumeric As Boolean = True
    Private _OnlyAlphabet As Boolean = False
    Private _RefuseAccents As Boolean = False
    Private _RefusedChars As String = ""
    Private _AcceptedChars As String = ""
    Private _flCapital As Boolean = False
    Private _OnlyFLCapital As Boolean = False
    Private _MatchExp As String = ""
    Private _AllCapital As Boolean = False
    Private _AllLower As Boolean = False
    Private _InModification As Boolean = False
    Private _CurrencyBox As Boolean = False
    Private _NbDecimals As Short = -1
    Private _BlockOnMinimum As Boolean = False
    Private _BlockOnMaximum As Boolean = False
    Private _Minimum As Double = 0
    Private _Maximum As Double = 0
    Private myValue As String
    Private acceptedText As String = ""
    Private mySelectionStart As Integer = 0
    Private acceptNegative As Boolean = False
    Private _TrimText As Boolean = False
    Private _ShowInternalContextMenu As Boolean = True
    Private _cb_AcceptLeftZeros As Boolean = False
    Private _ManageText As Boolean = True

#Region "Propriétés"
    Public Property manageText() As Boolean
        Get
            Return _ManageText
        End Get
        Set(ByVal value As Boolean)
            _ManageText = value
        End Set
    End Property

    Public Property showInternalContextMenu() As Boolean
        Get
            Return _ShowInternalContextMenu
        End Get
        Set(ByVal value As Boolean)
            _ShowInternalContextMenu = value
        End Set
    End Property

    Shadows Property [ReadOnly]() As Boolean
        Get
            Return MyBase.ReadOnly
        End Get
        Set(ByVal value As Boolean)
            MyBase.ReadOnly = value

            If value Then
                Me.BackColor = SystemColors.ControlLight
            Else
                Me.BackColor = Color.White
            End If
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

    Public Property cb_AcceptNegative() As Boolean
        Get
            Return acceptNegative
        End Get
        Set(ByVal Value As Boolean)
            acceptNegative = Value
            If acceptedChars Is Nothing OrElse acceptedChars.Length = 0 Then
                'ChangeChar("-", Value)
            Else
                'If AcceptedChars.IndexOf("-") < 0 Then ChangeChar("-", Value)
            End If
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

    Public Property currencyBox() As Boolean
        Get
            Return _CurrencyBox
        End Get
        Set(ByVal Value As Boolean)
            _CurrencyBox = Value
            If acceptedChars Is Nothing OrElse acceptedChars.Length = 0 Then
                '                ChangeChar(",", Value)
                '               ChangeChar(".", Value)
            Else
                '              If AcceptedChars.IndexOf(",") < 0 Then ChangeChar(",", Value)
                '             If AcceptedChars.IndexOf(".") < 0 Then ChangeChar(".", Value)
            End If
            If _AcceptAlpha AndAlso Value Then _AcceptAlpha = False
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
            If _AcceptedChars Is Nothing OrElse _AcceptedChars.Length = 0 Then Return ""
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

    '#Region "Internal functions"
    'Private Sub changeChar(ByVal myChar As Char, ByVal adding As Boolean)
    '    Dim Prefixe As String = "", NewAcceptedChars As String = ""
    '    If AcceptedChars <> "" Then Prefixe = "§"

    '    If Adding = True Then
    '        If AcceptedChars.IndexOf(MyChar) < 0 Then AcceptedChars &= Prefixe & MyChar
    '    Else
    '        If AcceptedChars <> "" Then
    '            Dim SAC() As String = AcceptedChars.Split(New Char() {"§"})
    '            Dim MyCharPos As Short = SearchArray(SAC, MyChar, CompareMethod.Text)
    '            If MyCharPos <> -1 Then
    '                Dim i As Short
    '                For i = 0 To SAC.Length - 1
    '                    If i <> MyCharPos Then NewAcceptedChars &= "§" & SAC(i)
    '                Next i
    '                If NewAcceptedChars.Length >= 1 Then
    '                    AcceptedChars = NewAcceptedChars.Substring(1)
    '                Else
    '                    AcceptedChars = ""
    '                End If
    '            End If
    '        End If
    '    End If
    'End Sub

    'Public Overrides Property Text() As String
    '    Get
    '        Return MyBase.Text
    '    End Get
    '    Set(ByVal value As String)
    '        MyBase.Text = value
    '    End Set
    'End Property

    'Private nos As String

    Protected Overrides Sub wndProc(ByRef m As System.Windows.Forms.Message)
        If _ShowInternalContextMenu OrElse m.Msg <> 123 Then MyBase.WndProc(m)
    End Sub

    Public Sub forceManaging()


        If _InModification = True Then Exit Sub
        Dim lastSelection As Integer = MyBase.SelectionStart
        Dim WordCapital, allWords As Boolean
        If firstLetterCapital = True Or firstLettersCapital = True Then
            WordCapital = True
        Else
            WordCapital = False
        End If
        allWords = firstLettersCapital
        myValue = MyBase.Text
        _InModification = True

        If Strings.forceManaging(myValue, _CurrencyBox, acceptedText, _AcceptAlpha, _AcceptNumeric, _OnlyAlphabet, _RefuseAccents, _AcceptedChars, _RefusedChars, _MatchExp, WordCapital, allWords, _AllCapital, _AllLower, _NbDecimals, acceptNegative, lastSelection, blockOnMinimum, minimum, blockOnMaximum, maximum, _cb_AcceptLeftZeros) = False Then _InModification = False : Exit Sub

        myValue = myValue.TrimStart

        MyBase.Text = myValue
        If lastSelection < 0 Then lastSelection = 0
        MyBase.SelectionStart = lastSelection
        _InModification = False
    End Sub

    Private Sub applyCapitals()
        myValue = Strings.firstLetterCapital(myValue, firstLettersCapital)
    End Sub

    Private Sub applyMatch()
        myValue = Strings.applyMatch(myValue, matchExp)
    End Sub

    Private Sub managedText_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If MyBase.Text.IndexOf(",") >= 0 And (e.KeyCode = 188 Or e.KeyCode = 190) Then
            acceptedText = MyBase.Text
            mySelectionStart = MyBase.SelectionStart
        Else
            acceptedText = ""
        End If
    End Sub

    Private Sub managedText_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.TextChanged
        If Me.ReadOnly = False And _ManageText Then forceManaging()
    End Sub

    Private Sub managedText_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If MyBase.Text.IndexOf(",") >= 0 And (e.KeyCode = 188 Or e.KeyCode = 190) Then
            acceptedText = MyBase.Text
            mySelectionStart = MyBase.SelectionStart
        Else
            acceptedText = ""
        End If
    End Sub

    Protected Overrides Sub onLostFocus(ByVal e As System.EventArgs)
        If _TrimText = True Then MyBase.Text = Trim(MyBase.Text)
    End Sub

    Private Sub managedText_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Me.Validating

    End Sub

    Private Sub managedText_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Validated
        If Me.ReadOnly = False AndAlso Me.Enabled = True Then Me.Text = Me.Text.Trim
    End Sub
End Class
