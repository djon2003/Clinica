Public Class Erreur
    Implements IComparable(Of Erreur)

    Public Enum errorSoftwareSource As Byte
        Clinica = 0
        ClinicaServer = 1
    End Enum

    'To be found backward (to get from the last inner exception)
    Private Const MESSAGE_LINE As String = "Message :"
    Private Const SOURCE_LINE As String = "Source :"
    Private Const NB_LINES_TO_FIND_BACKWARD As Integer = 2

    'To be found forward
    Private Const NB_LINES_TO_FIND_FORWARD As Integer = 5
    Private Const CLINICA_USER_LINE As String = "Utilisateur Clinica :"
    Private Const WINDOWS_USER_LINE As String = "Utilisateur Windows :"
    Private Const COMPUTER_LINE As String = "Ordinateur :"
    Private Const SOFTWARE_VERSION_LINE As String = "Version de Clinica :"
    Private Const DATE_LINE As String = "Date :"

    Private _fullContent As String = String.Empty

#Region "Constructor"
    Public Sub New(ByVal errorContent As String)
        _fullContent = errorContent
        Dim nbFound As Integer = 0
        Dim lines() As String = errorContent.Split(New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries)

        'Forward search
        For i As Integer = 0 To lines.Length - 1
            If lines(i).Trim.StartsWith(SOFTWARE_VERSION_LINE) Then
                clinicaVersion = extractHeaderLine(lines(i))
                nbFound += 1
            End If
            If lines(i).Trim.StartsWith(CLINICA_USER_LINE) Then
                clinicaUser = extractHeaderLine(lines(i))
                nbFound += 1
            End If
            If lines(i).Trim.StartsWith(WINDOWS_USER_LINE) Then
                winUser = extractHeaderLine(lines(i))
                nbFound += 1
            End If
            If lines(i).Trim.StartsWith(COMPUTER_LINE) Then
                computer = extractHeaderLine(lines(i))
                nbFound += 1
            End If
            If lines(i).Trim.StartsWith(DATE_LINE) Then
                Dim strDate As String = extractHeaderLine(lines(i))
                Dim strDateOnly As String = strDate.Substring(0, strDate.IndexOf(" ")).Replace("/", "-").Replace("\", "-")
                Dim sDate() As String = strDateOnly.Split(New Char() {"-"})
                Dim dateYear, dateMonth, dateDay, dateHour, dateMinute, dateSecond As Integer
                If sDate(2) > 31 Then
                    dateYear = sDate(2)
                    dateMonth = sDate(1)
                    dateDay = sDate(0)
                Else
                    dateYear = sDate(0)
                    dateMonth = sDate(1)
                    dateDay = sDate(2)
                End If
                Dim strTime As String = strDate.Substring(strDate.IndexOf(" ") + 1).Trim()
                Dim sTime() As String = strTime.Split(New Char() {":"})
                dateHour = sTime(0)
                If strTime.IndexOf("PM") <> -1 Then dateHour += 12
                dateMinute = sTime(1)
                If sTime.Length >= 3 Then
                    sTime(2) = sTime(2).Replace("AM", "").Replace("PM", "").Trim()
                    dateSecond = sTime(2)
                End If

                [date] = New Date(dateYear, dateMonth, dateDay, dateHour, dateMinute, dateSecond)
                lastDate = [date]
                nbFound += 1
            End If

            If nbFound = NB_LINES_TO_FIND_FORWARD Then Exit For
        Next

        'Backward search
        nbFound = 0
        For i As Integer = lines.Length - 1 To 0 Step -1
            If lines(i).Trim = MESSAGE_LINE Then
                message = lines(i + 1).Trim
                nbFound += 1
            End If
            If lines(i).Trim = SOURCE_LINE Then
                source = lines(i + 1).Trim
                nbFound += 1
            End If
            If nbFound = NB_LINES_TO_FIND_BACKWARD Then Exit For
        Next
        content = errorContent.Substring(errorContent.IndexOf("Exception Stack Trace"))
        extractMethod()

        softSource = IIf(clinicaUser.IndexOf("{") <> -1, Erreur.errorSoftwareSource.ClinicaServer, Erreur.errorSoftwareSource.Clinica)
    End Sub
#End Region

#Region "Definitions"
    Private _softSource As ErrorSoftwareSource
    Private _date, _lastDate As Date
    Private _ClinicaVersion As String = ""
    Private _computer As String = ""
    Private _winUser As String = ""
    Private _ClinicaUser As String = ""
    Private _message As String = ""
    Private _source As String = ""
    Private _content As String = ""
    Private _method As String = ""
    Private _identicalErrors As New Generic.List(Of Erreur)
#End Region

#Region "Properties"

    Public Property identicalErrors() As Generic.List(Of Erreur)
        Get
            Return _identicalErrors
        End Get
        Set(ByVal value As Generic.List(Of Erreur))
            _identicalErrors = value
        End Set
    End Property

    Private Function countIdenticalErrors() As Integer
        Dim nbErrors As Integer = _identicalErrors.Count
        For i As Integer = 0 To nbErrors - 1
            nbErrors += identicalErrors(i).identicalErrorsCount
        Next

        Return nbErrors
    End Function

    Public ReadOnly Property identicalErrorsCount() As Integer
        Get
            Return countIdenticalErrors()
        End Get
    End Property


    Public Property softSource() As ErrorSoftwareSource
        Get
            Return _softSource
        End Get
        Set(ByVal value As ErrorSoftwareSource)
            _softSource = value
        End Set
    End Property
    Public Property [date]() As Date
        Get
            Return _date
        End Get
        Set(ByVal value As Date)
            _date = value
        End Set
    End Property
    Public Property [lastDate]() As Date
        Get
            Dim maxDate As Date = _lastDate

            For i As Integer = 0 To _identicalErrors.Count - 1
                Dim errorDate As Date = _identicalErrors(i).lastDate
                If errorDate > maxDate Then maxDate = errorDate
            Next

            Return maxDate
        End Get
        Set(ByVal value As Date)
            _lastDate = value
        End Set
    End Property
    Public Property clinicaVersion() As String
        Get
            Return _ClinicaVersion
        End Get
        Set(ByVal value As String)
            _ClinicaVersion = value
        End Set
    End Property
    Public Property computer() As String
        Get
            Return _computer
        End Get
        Set(ByVal value As String)
            _computer = value
        End Set
    End Property
    Public Property winUser() As String
        Get
            Return _winUser
        End Get
        Set(ByVal value As String)
            _winUser = value
        End Set
    End Property
    Public Property clinicaUser() As String
        Get
            Return _ClinicaUser
        End Get
        Set(ByVal value As String)
            _ClinicaUser = value
        End Set
    End Property
    Public Property message() As String
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            _message = value
        End Set
    End Property
    Public Property source() As String
        Get
            Return _source
        End Get
        Set(ByVal value As String)
            _source = value
        End Set
    End Property
    Public Property content() As String
        Get
            Return _content
        End Get
        Set(ByVal value As String)
            _content = value
        End Set
    End Property
    Public ReadOnly Property fullContent() As String
        Get
            Return _fullContent
        End Get
    End Property

    Public Property method() As String
        Get
            Return _method
        End Get
        Set(ByVal value As String)
            _method = value
        End Set
    End Property
#End Region

    Private Function findLastInnerError(Optional ByVal startingAt As Integer = -1) As String
        If startingAt = -1 Then startingAt = content.Length
        Dim nextInnerPos As Integer = content.LastIndexOf("InnerException", startingAt)
        If nextInnerPos = -1 Then nextInnerPos = 0
        Dim curInnerPos As Integer = 0
        Dim prevInnerPos As Integer = 0
        'While nextInnerPos <> -1
        '    prevInnerPos = curInnerPos
        '    curInnerPos = nextInnerPos
        '    nextInnerPos = content.IndexOf("InnerException", nextInnerPos + 1)
        'End While

        Return content.Substring(nextInnerPos, startingAt - nextInnerPos)
    End Function

    Private Function _extractMethod(ByVal lastErrorContent As String) As String
        Dim lastPos As Integer = lastErrorContent.IndexOf("Exception Stack Trace", StringComparison.CurrentCultureIgnoreCase)
        Dim firstGoodPos As Integer = lastErrorContent.IndexOf(softSource.ToString, lastPos)
        Dim lastGoodPos As Integer = lastErrorContent.IndexOf(vbCrLf, firstGoodPos)
        Dim curGoodPos As Integer = -1
        Dim lastGoodLine As String = lastErrorContent.Substring(firstGoodPos, lastGoodPos - firstGoodPos)
        lastGoodLine = lastGoodLine.Substring(0, lastGoodLine.IndexOf("("))

        While lastGoodLine.EndsWith("AddErrorLog", StringComparison.OrdinalIgnoreCase) AndAlso curGoodPos <> lastGoodPos
            firstGoodPos = lastErrorContent.IndexOf(softSource.ToString, firstGoodPos + 1)
            If firstGoodPos = -1 Then Exit While
            curGoodPos = lastGoodPos
            lastGoodPos = lastErrorContent.IndexOf(vbCrLf, firstGoodPos)
            lastGoodLine = lastErrorContent.Substring(firstGoodPos, lastGoodPos - firstGoodPos)
            lastGoodLine = lastGoodLine.Substring(0, lastGoodLine.IndexOf("("))
        End While

        Return lastGoodLine
    End Function

    Private Sub extractMethod()
        Dim lastErrorContent As String = findLastInnerError()
        If lastErrorContent.Trim.EndsWith("Aucune") Then lastErrorContent = findLastInnerError(content.IndexOf(lastErrorContent))
        method = _extractMethod(lastErrorContent)

        If method.EndsWith("application_threadexception", StringComparison.OrdinalIgnoreCase) Then
            'try previous inner exception
            method = _extractMethod(findLastInnerError(content.IndexOf(lastErrorContent)))
        End If

        If method.Trim.EndsWith("AddErrorLog", StringComparison.OrdinalIgnoreCase) Then
            Dim a As Byte = 0
        End If
    End Sub

    Private Function extractHeaderLine(ByVal line As String) As String
        Return line.Substring(line.IndexOf(":") + 1).Trim
    End Function

    Public Overrides Function equals(ByVal obj As Object) As Boolean
        With CType(obj, Erreur)
            If softSource <> .softSource Then Return False
            If method <> .method Then Return False
        End With

        Return True
    End Function

    Public Function compareTo(ByVal other As Erreur) As Integer Implements System.IComparable(Of Erreur).CompareTo
        Dim cmp As Integer = softSource.CompareTo(other.softSource)
        If cmp = 0 Then cmp = lastDate.CompareTo(other.lastDate)

        Return cmp
    End Function
End Class
