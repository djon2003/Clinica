Imports System.Text.RegularExpressions

Module Chaines

    Private Function stringToByteArray(ByVal strString As String) As Byte()
        Dim bytBuffer() As Byte
        Dim i As Integer

        ReDim bytBuffer(Len(strString) - 1)
        For i = 0 To UBound(bytBuffer)
            bytBuffer(i) = CByte(Asc(Mid(strString, i + 1, 1)))
        Next

        Return bytBuffer
    End Function


    Private Function hexDecoderEvaluator(ByVal m As Match) As String

        Dim bytes((m.Value.Length / 3) - 1) As Byte

        For i As Integer = 0 To bytes.Length - 1
            Dim hex As String = m.Value.Substring(i * 3 + 1, 2)
            Dim iHex As Integer = Convert.ToInt32(hex, 16)
            bytes(i) = Convert.ToByte(iHex)
        Next i


        Return System.Text.Encoding.GetEncoding(charset).GetString(bytes)
    End Function

    Private charset As String = "utf-8"

    Public Function convertFrom(ByVal value As String, ByVal encoding As String) As String
        encoding = encoding.ToLower()
        Dim strIso1 As String = "=?" & encoding & "?b?"
        Dim strIso2 As String = "=?" & encoding & "?q?"
        Dim strEnd As String = "?="
        Dim indexStrIso1 As Integer = value.ToLower.IndexOf(strIso1)
        Dim indexStrIso2 As Integer = value.ToLower.IndexOf(strIso2)
        value = value.Replace("?= =?", "?==?")


        While indexStrIso1 <> -1 Or indexStrIso2 <> -1
            If indexStrIso1 <> -1 Then
                Dim afterValue As String = value
                Dim beforeValue As String = value
                Dim stringBytes() As Byte
                If value.IndexOf(" ") <> -1 Then
                    beforeValue = value.Substring(0, indexStrIso1)
                    value = value.Substring(indexStrIso1 + strIso1.Length)
                    afterValue = value.Substring(value.IndexOf(strEnd) + strEnd.Length)

                    value = value.Substring(0, value.IndexOf(strEnd))
                    stringBytes = ConvertFromBase64(value)
                Else
                    value = value.Substring(indexStrIso1 + strIso1.Length)
                    value = value.Substring(0, value.IndexOf(strEnd))
                    stringBytes = ConvertFromBase64(value)
                    afterValue = ""
                    beforeValue = ""
                End If

                value = beforeValue & System.Text.Encoding.GetEncoding(encoding).GetString(stringBytes) & afterValue
            End If
            If indexStrIso2 <> -1 Then
                Dim afterValue As String = value
                Dim beforeValue As String = value
                If value.IndexOf(" ") <> -1 Then
                    beforeValue = value.Substring(0, indexStrIso2)
                    value = value.Substring(indexStrIso2 + strIso2.Length)
                    afterValue = value.Substring(value.IndexOf(strEnd) + strEnd.Length)

                    value = value.Substring(0, value.IndexOf(strEnd))

                Else
                    value = value.Substring(indexStrIso2 + strIso2.Length)
                    value = value.Substring(0, value.IndexOf(strEnd))
                    afterValue = ""
                    beforeValue = ""
                End If

                charset = encoding
                Dim hexRegex As Regex = New Regex("(\=([0-9A-F][0-9A-F]))+", RegexOptions.IgnoreCase)
                value = hexRegex.Replace(value, New MatchEvaluator(AddressOf HexDecoderEvaluator))

                If encoding = "iso-8859-1" Then
                    value = System.Text.RegularExpressions.Regex.Replace(value.Trim, "\n", "").Replace(vbLf, "").Replace(vbCr, "").Replace(Chr(9), "")
                    value = value.Replace("_", " ")
                End If

                value = beforeValue & value & afterValue

            End If

            indexStrIso1 = value.ToLower.IndexOf(strIso1)
            indexStrIso2 = value.ToLower.IndexOf(strIso2)
        End While

        Return value
    End Function

    Public Function convertFromHexadecimal(ByVal value As String, ByVal curCharset As String) As String
        If curCharset Is Nothing Then Return value

        charset = curCharset
        value = value.Replace("=" & vbCrLf, "")

        Dim hexRegex As Regex = New Regex("(\=([0-9A-F][0-9A-F]))+")
        value = hexRegex.Replace(value, New MatchEvaluator(AddressOf HexDecoderEvaluator))

        Return value
    End Function

    Public Function convertUTF8_To_UTF7(ByVal [text] As String) As String
        Dim t As New System.Text.UTF7Encoding(True)
        Dim lines() As String = [text].Split(New String() {vbCrLf}, StringSplitOptions.None)
        For i As Integer = 0 To lines.GetUpperBound(0)
            Dim utf8Bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(lines(i))
            Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding(65000)
            Dim utf7Bytes As Byte() = System.Text.Encoding.Convert(System.Text.Encoding.UTF8, System.Text.Encoding.UTF7, utf8Bytes)
            'utf7Bytes = System.Text.Encoding.Convert(System.Text.Encoding.UTF8, enc, utf7Bytes)
            Dim text2 As String = enc.GetString(utf7Bytes)
            lines(i) = text2
            'Dim codes As String = ""
            'For Each ei As System.Text.EncodingInfo In enc.GetEncodings
            '    If ei.GetEncoding.IsSingleByte Then
            '        codes &= ";" & ei.CodePage & "-" & ei.DisplayName
            '    End If
            '    Dim enc1 As System.Text.Encoding = System.Text.Encoding.GetEncoding(ei.CodePage)
            '    Dim utf7Bytes1 As Byte() = System.Text.Encoding.Convert(System.Text.Encoding.Unicode, enc1, utf8Bytes)
            '    Dim text4 As String = enc1.GetString(utf7Bytes1)
            '    If text4.Contains("é") Then
            '        Dim a As Byte = 0
            '    End If
            'Next
        Next i

        Return String.Join(vbCrLf, lines)
    End Function

    Public Function convertFromBase64(ByVal base64 As String) As Byte()
        If base64 Is Nothing Then Throw New ArgumentNullException("base64")
        Return Convert.FromBase64String(base64)
    End Function

    Public Function convertFromBase64(ByVal base64 As String, ByVal returnString As Boolean) As Object
        If base64 Is Nothing Then Throw New ArgumentNullException("base64")

        Try
            Dim bytes() As Byte = ConvertFromBase64(base64)

            If returnString Then
                Dim str As String = ""
                For i As Integer = 0 To bytes.GetUpperBound(0)
                    str &= Chr(bytes(i))
                Next i
                Return str
            End If

            Return bytes
        Catch 'REM Exception not handle
        End Try

        Return ""
    End Function

    Public Function firstPart(ByVal texte As String, ByVal cursorPos As Integer) As String
        If (CursorPos - 1) < 0 Then Return ""

        Return Texte.Substring(0, CursorPos)
    End Function

    Public Function transformdiffDateInText(ByVal date1 As Date, ByVal date2 As Date, Optional ByRef diffDate As Integer = 0, Optional ByRef nbWeek As Integer = 0, Optional ByRef nbDay As Integer = 0) As String
        Dim strNbWeek As String
        Dim dureeStr As String = "# semaine et # jour"

        If Date1InfDate2(Date1, Date2.AddMonths(1)) = True Then
            Try
                DiffDate = Date1.Subtract(Date2).Days
            Catch 'REM Exception not handle
                Return ""
            End Try
            strNbWeek = CStr((DiffDate / 7)).Replace(".", ",")
            If strNbWeek.IndexOf(",") < 0 Then NbWeek = strNbWeek Else NbWeek = strNbWeek.Substring(0, strNbWeek.IndexOf(",")) : NbDay = DiffDate - NbWeek * 7

            If NbWeek = 0 Then
                DureeStr = Microsoft.VisualBasic.Right(DureeStr, "6")
            ElseIf NbDay = 0 Then
                DureeStr = DureeStr.Substring(0, "9")
            End If

            DureeStr = DureeStr.Replace("# semaine", NbWeek & " semaine")
            DureeStr = DureeStr.Replace("# jour", NbDay & " jour")
            If NbWeek > 1 Then DureeStr = DureeStr.Replace("ne", "nes")
            If NbDay > 1 Then DureeStr = DureeStr.Replace("r", "rs")
            Return DureeStr
        Else
            Return "1 mois et plus"
        End If
    End Function

    Public Function isValidTinyDate(ByVal dateToTest As String, Optional ByVal dateSeparator As Char = "/") As Boolean
        Dim sDate() As String = Split(DateToTest, DateSeparator)
        If SDate Is Nothing Then Return False

        Select Case UBound(SDate)
            Case 0
                If IsNumeric(SDate(0)) = False Then Return False
                If SDate(0) < 1900 Or SDate(0) > 9000 Then Return False

            Case 1
                If IsNumeric(SDate(0)) = False Then Return False
                If IsNumeric(SDate(1)) = False Then Return False
                If SDate(0) < 1900 Or SDate(0) > 9000 Then Return False
                If SDate(1) <= 0 Or SDate(1) > 12 Then Return False

            Case 2
                If IsNumeric(SDate(0)) = False Then Return False
                If IsNumeric(SDate(1)) = False Then Return False
                If IsNumeric(SDate(2)) = False Then Return False
                If SDate(0) < 1900 Or SDate(0) > 9000 Then Return False
                If SDate(1) <= 0 Or SDate(1) > 12 Then Return False
                If SDate(1) <= 0 Or SDate(1) > 31 Then Return False
        End Select

        Return True
    End Function

    Public Function convertirMontantEnMots(ByVal chiffre As Double, Optional ByVal decimalsInLetter As Boolean = False) As String
        If (Math.Abs(chiffre) / Math.Floor(Math.Abs(chiffre))) > 1 Then
            'Si le nombre contient des décimales
            Dim decimaleNumber As Double
            Dim number As Double = Math.Floor(Math.Abs(chiffre))

            DecimaleNumber = CInt((chiffre - Number) * 10 ^ (AddZeros(chiffre.ToString, Number.ToString.Length + 3, False).Length - Number.ToString.Length - 1))
            Dim a As Double = chiffre.ToString.Length - Number.ToString.Length - 1
            If DecimalsInLetter Then Return SubConvertirMontantEnMots(Number) & "et " & SubConvertirMontantEnMots(DecimaleNumber) & "cent(s)"

            Return IIf(chiffre < 0, "MOINS ", "") & SubConvertirMontantEnMots(Number) & "et " & DecimaleNumber & " cent(s)"
        Else
            Return IIf(chiffre < 0, "MOINS ", "") & SubConvertirMontantEnMots(Math.Abs(chiffre))
        End If
    End Function

    Private Function subConvertirMontantEnMots(ByVal chiffre As Double) As String
        Dim centaine As Int64
        Dim dizaine As Int64
        Dim unite As Int64
        Dim reste As Int64
        Dim y As Int64
        Dim dix As Boolean = False
        Dim lettre As String = ""
        Dim preUnite As String = ""
        reste = chiffre / 1
        Dim i As Int64 = 1000000000
        While i >= 1
            y = Math.Floor(reste / i)
            If Not (y = 0) Then
                centaine = Math.Floor(y / 100)
                dizaine = Math.Floor((y - (centaine * 100)) / 10)
                unite = y - (centaine * 100) - (dizaine * 10)
                Select Case centaine
                    Case 0
                        ' break 
                    Case 1
                        lettre += "cent "
                        ' break 
                    Case 2
                        If (dizaine = 0) AndAlso (unite = 0) Then
                            lettre += "deux cents "
                        Else
                            lettre += "deux cent "
                        End If
                        ' break 
                    Case 3
                        If (dizaine = 0) AndAlso (unite = 0) Then
                            lettre += "trois cents "
                        Else
                            lettre += "trois cent "
                        End If
                        ' break 
                    Case 4
                        If (dizaine = 0) AndAlso (unite = 0) Then
                            lettre += "quatre cents "
                        Else
                            lettre += "quatre cent "
                        End If
                        ' break 
                    Case 5
                        If (dizaine = 0) AndAlso (unite = 0) Then
                            lettre += "cinq cents "
                        Else
                            lettre += "cinq cent "
                        End If
                        ' break 
                    Case 6
                        If (dizaine = 0) AndAlso (unite = 0) Then
                            lettre += "six cents "
                        Else
                            lettre += "six cent "
                        End If
                        ' break 
                    Case 7
                        If (dizaine = 0) AndAlso (unite = 0) Then
                            lettre += "sept cents "
                        Else
                            lettre += "sept cent "
                        End If
                        ' break 
                    Case 8
                        If (dizaine = 0) AndAlso (unite = 0) Then
                            lettre += "huit cents "
                        Else
                            lettre += "huit cent "
                        End If
                        ' break 
                    Case 9
                        If (dizaine = 0) AndAlso (unite = 0) Then
                            lettre += "neuf cents "
                        Else
                            lettre += "neuf cent "
                        End If
                End Select
                Select Case dizaine
                    Case 0
                        ' break 
                    Case 1
                        dix = True
                        ' break 
                    Case 2
                        lettre += "vingt "
                        ' break 
                    Case 3
                        lettre += "trente "
                        ' break 
                    Case 4
                        lettre += "quarante "
                        ' break 
                    Case 5
                        lettre += "cinquante "
                        ' break 
                    Case 6
                        lettre += "soixante "
                        ' break 
                    Case 7
                        dix = True
                        lettre += "soixante "
                        ' break 
                    Case 8
                        If unite = 0 Then
                            lettre += "quatre-vingts "
                        Else
                            lettre += "quatre-vingt "
                        End If
                        ' break 
                    Case 9
                        dix = True
                        lettre += "quatre-vingt "
                End Select
                If lettre <> "" AndAlso ((dizaine > 1 And dizaine < 9 And unite > 0) OrElse dizaine = 9) Then
                    preUnite = "-"
                    lettre = lettre.Substring(0, lettre.Length - 1)
                Else
                    preUnite = ""
                End If
                Select Case unite
                    Case 0
                        If dix Then
                            lettre += preUnite & "dix "
                        End If
                        ' break 
                    Case 1
                        If preUnite <> "" AndAlso dizaine < 8 AndAlso dizaine <> 1 Then preUnite = " et "
                            
                        If dix Then
                            lettre += preUnite & "onze "
                        Else
                            lettre += preUnite & "un "
                        End If
                        ' break 
                    Case 2
                        If dix Then
                            lettre += preUnite & "douze "
                        Else
                            lettre += preUnite & "deux "
                        End If
                        ' break 
                    Case 3
                        If dix Then
                            lettre += preUnite & "treize "
                        Else
                            lettre += preUnite & "trois "
                        End If
                        ' break 
                    Case 4
                        If dix Then
                            lettre += preUnite & "quatorze "
                        Else
                            lettre += preUnite & "quatre "
                        End If
                        ' break 
                    Case 5
                        If dix Then
                            lettre += preUnite & "quinze "
                        Else
                            lettre += preUnite & "cinq "
                        End If
                        ' break 
                    Case 6
                        If dix Then
                            lettre += preUnite & "seize "
                        Else
                            lettre += preUnite & "six "
                        End If
                        ' break 
                    Case 7
                        If dix Then
                            lettre += preUnite & "dix-sept "
                        Else
                            lettre += preUnite & "sept "
                        End If
                        ' break 
                    Case 8
                        If dix Then
                            lettre += preUnite & "dix-huit "
                        Else
                            lettre += preUnite & "huit "
                        End If
                        ' break 
                    Case 9
                        If dix Then
                            lettre += preUnite & "dix-neuf "
                        Else
                            lettre += preUnite & "neuf "
                        End If
                End Select
                Select Case i
                    Case 1000000000
                        If y > 1 Then
                            lettre += "milliards "
                        Else
                            lettre += "milliard "
                        End If
                        ' break 
                    Case 1000000
                        If y > 1 Then
                            lettre += "millions "
                        Else
                            lettre += "million "
                        End If
                        ' break 
                    Case 1000
                        If y = 1 Then lettre = lettre.Substring(0, lettre.Length - 3)
                        lettre += "mille "
                End Select
            End If
            reste -= y * i
            dix = False
            i /= 1000
        End While
        If lettre.Length = 0 Then
            lettre += "zero"
        End If
        Return lettre
    End Function

    Public Function forceManaging(ByRef myValue As String, ByVal currencyBox As Boolean, ByVal acceptedText As String, ByVal acceptAlpha As Boolean, ByVal acceptNumeric As Boolean, ByVal onlyAlphabet As Boolean, ByVal refuseAccents As Boolean, Optional ByVal acceptedChars As String = "", Optional ByVal refusedChars As String = "", Optional ByVal matchExp As String = "", Optional ByVal wordCapital As Boolean = False, Optional ByVal allWords As Boolean = False, Optional ByVal allCapital As Boolean = False, Optional ByVal allLower As Boolean = False, Optional ByVal nbDecimals As Short = 0, Optional ByVal acceptNegative As Boolean = False, Optional ByRef lastSelection As Integer = -1, Optional ByVal blockOnminimum As Boolean = False, Optional ByVal minimum As Double = 0, Optional ByVal blockOnmaximum As Boolean = False, Optional ByVal maximum As Double = 0, Optional ByVal cb_AcceptLeftZeros As Boolean = False) As Boolean
        'Dim tmpSB As New System.Text.StringBuilder(MyValue)
        'Dim managed As Boolean = ForceManaging(tmpSB, CurrencyBox, AcceptedText, AcceptAlpha, AcceptNumeric, OnlyAlphabet, RefuseAccents, AcceptedChars, RefusedChars, MatchExp, WordCapital, AllWords, AllCapital, AllLower, NbDecimals, AcceptNegative, LastSelection)
        'If managed Then MyValue = tmpSB.ToString

        'Return managed
        'Dim tmpTimer As Long = Date.Now.Ticks

        Dim i As Integer
        Dim myValue2 As String
        Dim isNegative As Boolean
        If MyValue Is Nothing Then MyValue = ""

        If MyValue = "" And CurrencyBox = True Then MyValue = "0" : LastSelection = 2
        If CurrencyBox = True AndAlso AcceptNegative = False AndAlso Integer.TryParse(MyValue, 0) AndAlso MyValue < 0 Then MyValue = 0 : LastSelection = 2 : Return True

        If MyValue.Length = 0 Then Return False
        MyValue = MyValue.Replace("§", "")

        If AcceptAlpha = True Then MyValue = OnlyAlpha(MyValue, AcceptedChars, OnlyAlphabet, AcceptNumeric, RefuseAccents)
        If AcceptAlpha = False And AcceptNumeric = True Then MyValue = OnlyNumeric(MyValue, AcceptedChars)

        If RefusedChars <> "" Then
            For i = 1 To RefusedChars.Length
                MyValue = Replace(MyValue, Mid(RefusedChars, i, 1), "")
            Next i
        End If

        If MyValue Is Nothing Then MyValue = ""

        If MatchExp <> "" Then MyValue = ApplyMatch(MyValue, MatchExp)
        If WordCapital = True Then MyValue = FirstLetterCapital(MyValue, AllWords)
        If AllCapital = True Then MyValue = MyValue.ToUpper
        If AllLower = True Then MyValue = MyValue.ToLower

        If CurrencyBox = True Then
            Dim pos As Short = 0
            If Not AcceptedText = "" Then
                MyValue = AcceptedText
            Else
                Pos = InStr(MyValue, ".")
                If Pos > 0 Then
                    MyValue = MyValue.Replace(".", ",")
                    LastSelection = Pos
                End If

                Do
                    MyValue2 = MyValue
                    MyValue = Replace(MyValue, ",", "", , 1)
                    Pos = InStr(MyValue, ",")
                Loop While Pos > 0

                MyValue = MyValue2
            End If

            If CB_AcceptLeftZeros = False AndAlso MyValue.Length > 1 Then
                If Not MyValue.Substring(0, 2) = "0," Then
                    Do While MyValue <> "" AndAlso MyValue.Substring(0, 1) = "0"
                        MyValue = MyValue.Substring(1)
                    Loop
                    If MyValue = "" Then MyValue = "0"
                End If
            End If

            Pos = InStr(MyValue, ",")
            If Pos > 0 And NbDecimals = 0 Then
                MyValue = MyValue.Replace(",", "")
                LastSelection = Pos
            ElseIf Pos > 0 And NbDecimals > 0 Then
                MyValue = Mid(MyValue, 1, Pos) & Mid(MyValue, Pos + 1, NbDecimals)
            End If

            If InStr(MyValue, "-") > 0 Then IsNegative = True
            MyValue = MyValue.Replace("-", "")
            If MyValue = "" Then MyValue = "0"
            If AcceptNegative = True And IsNegative = True Then MyValue = "-" & MyValue
            If MyValue = "-0" Then MyValue = "0"
            If MyValue.Length > 0 Then If MyValue.Substring(0, 1) = "," Then MyValue = "0" & MyValue : LastSelection = MyValue.Length

            Dim testingValue As Double = 0
            If MyValue.EndsWith(",") Then testingValue = MyValue.Substring(0, MyValue.Length - 1) Else testingValue = MyValue
            If BlockOnMinimum AndAlso testingValue < Minimum Then MyValue = Minimum
            If BlockOnMaximum AndAlso testingValue > Maximum Then MyValue = Maximum
        End If

        '    If MyMainWin IsNot Nothing Then MyMainWin.StatusText = "FM time : " & (Date.Now.Ticks - tmpTimer) / 10000000

        Return True
    End Function

    Public Function forceManaging(ByRef myValue As System.Text.StringBuilder, ByVal currencyBox As Boolean, ByVal acceptedText As String, ByVal acceptAlpha As Boolean, ByVal acceptNumeric As Boolean, ByVal onlyAlphabet As Boolean, ByVal refuseAccents As Boolean, Optional ByVal acceptedChars As String = "", Optional ByVal refusedChars As String = "", Optional ByVal matchExp As String = "", Optional ByVal wordCapital As Boolean = False, Optional ByVal allWords As Boolean = False, Optional ByVal allCapital As Boolean = False, Optional ByVal allLower As Boolean = False, Optional ByVal nbDecimals As Short = 0, Optional ByVal acceptNegative As Boolean = False, Optional ByRef lastSelection As Integer = -1) As Boolean
        '        Dim i As Integer
        '        Dim MyValue2 As String
        '        Dim IsNegative As Boolean
        '        If MyValue Is Nothing Then MyValue = New System.Text.StringBuilder

        '        If MyValue.Length = 0 And CurrencyBox = True Then MyValue.Append("0") : LastSelection = 2 : Return True
        '        If MyValue.Length = 0 Then Return False
        '        MyValue = MyValue.Replace("§", "")

        '        If AcceptAlpha = True And AcceptNumeric = True Then MyValue = OnlyAlpha(MyValue, AcceptedChars, OnlyAlphabet, AcceptNumeric, RefuseAccents)
        '        If AcceptAlpha = False And AcceptNumeric = True Then OnlyNumeric(MyValue, AcceptedChars)

        '        If RefusedChars <> "" Then
        '            For i = 1 To RefusedChars.Length
        '                MyValue = Replace(MyValue, Mid(RefusedChars, i, 1), "")
        '            Next i
        '        End If

        '        If MyValue Is Nothing Then MyValue = ""

        '        If MatchExp <> "" Then MyValue = ApplyMatch(MyValue, MatchExp)
        '        If WordCapital = True Then MyValue = FirstLetterCapital(MyValue, AllWords)
        '        If AllCapital = True Then MyValue = MyValue.ToUpper
        '        If AllLower = True Then MyValue = MyValue.ToLower

        '        If CurrencyBox = True Then
        '            Dim Pos As Short = 0
        '            If Not AcceptedText = "" Then
        '                MyValue = AcceptedText
        '            Else
        '                Pos = InStr(MyValue, ".")
        '                If Pos > 0 Then
        '                    MyValue = MyValue.Replace(".", ",")
        '                    LastSelection = Pos
        '                End If

        '                Do
        '                    MyValue2 = MyValue
        '                    MyValue = Replace(MyValue, ",", "", , 1)
        '                    Pos = InStr(MyValue, ",")
        '                Loop While Pos > 0

        '                MyValue = MyValue2
        '            End If

        '            If MyValue.Length > 1 Then
        '                If Not MyValue.Substring(0, 2) = "0," Then
        '                    On Error GoTo SkipZeros
        '                    Do While MyValue <> "" AndAlso MyValue.Substring(0, 1) = "0"
        '                        MyValue = MyValue.Substring(1)
        '                    Loop
        'SkipZeros:
        '                    If MyValue = "" Then MyValue = "0"
        '                    On Error GoTo 0
        '                End If
        '            End If

        '            Pos = InStr(MyValue, ",")
        '            If Pos > 0 And NbDecimals = 0 Then
        '                MyValue = MyValue.Replace(",", "")
        '                LastSelection = Pos
        '            ElseIf Pos > 0 And NbDecimals > 0 Then
        '                MyValue = Mid(MyValue, 1, Pos) & Mid(MyValue, Pos + 1, NbDecimals)
        '            End If

        '            If InStr(MyValue, "-") > 0 Then IsNegative = True
        '            MyValue = MyValue.Replace("-", "")
        '            If MyValue = "" Then MyValue = "0"
        '            If AcceptNegative = True And IsNegative = True Then MyValue = "-" & MyValue
        '            If MyValue = "-0" Then MyValue = "0"
        '            If MyValue.Length > 0 Then If MyValue.Substring(0, 1) = "," Then MyValue = "0" & MyValue : LastSelection = MyValue.Length
        '        End If

        '        Return True
    End Function

    Public Function applyMatch(ByVal textToApply As String, ByVal matchExp As String) As String
        Dim i, n As Integer

        n = 1
        For i = 1 To MatchExp.Length
            Select Case Mid(MatchExp, i, 1).ToUpper
                Case "A"
                    If IsNumeric(Mid(TextToApply, n, 1)) = False Then
                        n += 1
                    Else
                        Exit For
                    End If

                Case "1"
                    If IsNumeric(Mid(TextToApply, n, 1)) = True Then
                        n += 1
                    Else
                        Exit For
                    End If

                Case Else
            End Select
        Next i

        n -= 1
        If TextToApply <> "" Then
            If n > 0 Then
                If n <= TextToApply.Length Then TextToApply = TextToApply.Substring(0, n)
            Else
                TextToApply = ""
            End If
        End If


        Return TextToApply
    End Function

    Public Function firstLetterCapital(ByVal stringToConvert As String, Optional ByVal allWords As Boolean = False) As String
        Dim i As Integer
        Dim previousChar As Char = Nothing
        Dim myChar As Char = Nothing
        Dim LeftPart, rightPart As String
        Dim upTo As Integer = 0

        For i = 1 To StringToConvert.Length - 1
            MyChar = Mid(StringToConvert, i, 1) : LeftPart = "" : RightPart = ""
            If PreviousChar = Nothing Or PreviousChar = " " Or PreviousChar = "-" Or PreviousChar = "'" Then
                UpTo += 1
                If i > 1 Then LeftPart = StringToConvert.Substring(0, i - 1)
                If i < StringToConvert.Length Then RightPart = StringToConvert.Substring(i, StringToConvert.Length - i)
                StringToConvert = LeftPart & Char.ToUpper(MyChar) & RightPart
                If AllWords = False And UpTo = 1 Then Exit For
            End If
            PreviousChar = MyChar
        Next i

        Return StringToConvert
    End Function

    Public Function firstLettersCapital(ByVal stringToConvert As String) As String
        Return FirstLetterCapital(StringToConvert, True)
    End Function

    Public Function copyArrayToString(ByVal originArray() As Boolean) As String
        Dim stringToCopy As String = ""
        Dim i As Integer

        For i = 0 To UBound(OriginArray)
            If OriginArray(i) = True Then
                StringToCopy &= "1"
            Else
                StringToCopy &= "0"
            End If
        Next i

        Return StringToCopy
    End Function

#Region "CopyArray"
    Public Sub copyArray(ByRef destinationArray() As Boolean, Optional ByVal stringToSplit As String = "", Optional ByVal originArray() As Boolean = Nothing)
        Dim i, arrayMax As Integer
        Dim MyTempArray(), myFalseChar As Char
        MyFalseChar = "0"c

        If StringToSplit <> "" Then
            MyTempArray = SplitStr(StringToSplit)
            ArrayMax = UBound(MyTempArray)
            ReDim DestinationArray(ArrayMax)
            For i = 0 To ArrayMax
                If MyTempArray(i) = MyFalseChar Then
                    DestinationArray(i) = False
                Else
                    DestinationArray(i) = True
                End If
            Next i
        ElseIf Not OriginArray Is Nothing Then
            ArrayMax = UBound(OriginArray)
            ReDim DestinationArray(ArrayMax)
            For i = 0 To ArrayMax
                DestinationArray(i) = OriginArray(i)
            Next i
        End If
    End Sub

    Public Sub copyArray(ByRef destinationArray() As String, Optional ByVal stringToSplit As String = "", Optional ByVal originArray() As String = Nothing)
        Dim i, arrayMax As Integer
        Dim myTempArray() As Char

        If StringToSplit <> "" Then
            MyTempArray = SplitStr(StringToSplit)
            ArrayMax = UBound(MyTempArray)
            ReDim DestinationArray(ArrayMax)
            For i = 0 To ArrayMax
                DestinationArray(i) = MyTempArray(i)
            Next i
        ElseIf Not OriginArray Is Nothing Then
            ArrayMax = UBound(OriginArray)
            ReDim DestinationArray(ArrayMax)
            For i = 0 To ArrayMax
                DestinationArray(i) = OriginArray(i)
            Next i
        End If
    End Sub

    Public Sub copyArray(ByRef destinationArray() As Integer, Optional ByVal stringToSplit As String = "", Optional ByVal originArray() As Integer = Nothing)
        Dim i, arrayMax As Integer
        Dim myTempArray() As Char

        If StringToSplit <> "" Then
            MyTempArray = SplitStr(StringToSplit)
            ArrayMax = UBound(MyTempArray)
            ReDim DestinationArray(ArrayMax)
            For i = 0 To ArrayMax
                If Char.IsDigit(MyTempArray(i)) Then DestinationArray(i) = CInt(MyTempArray(i).ToString)
            Next i
        ElseIf Not OriginArray Is Nothing Then
            ArrayMax = UBound(OriginArray)
            ReDim DestinationArray(ArrayMax)
            For i = 0 To ArrayMax
                DestinationArray(i) = OriginArray(i)
            Next i
        End If
    End Sub
#End Region

    Public Function removeItemArray(ByVal myArray As String(), ByVal indexToRemove As Integer) As String()
        If IndexToRemove < MyArray.GetLowerBound(0) Or IndexToRemove > MyArray.GetUpperBound(0) Then Return MyArray

        Dim myArray2() As String = {}
        Dim i, n As Integer

        n = 0
        For i = MyArray.GetLowerBound(0) To MyArray.GetUpperBound(0)
            If i <> IndexToRemove Then
                ReDim Preserve MyArray2(n)
                MyArray2(n) = MyArray(i)
                n += 1
            End If
        Next i

        Return MyArray2
    End Function

    Public Function searchArray(ByVal myArray As String(), ByVal searchStr As String, Optional ByVal comparingMethod As CompareMethod = CompareMethod.Text) As Integer
        If MyArray Is Nothing Then Return -1
        Dim i As Integer

        For i = MyArray.GetLowerBound(0) To MyArray.GetUpperBound(0)
            If InStr(MyArray(i), SearchStr, ComparingMethod) > 0 Then Return i
        Next i

        Return -1
    End Function

    Public Function searchArray(ByVal myArray As ArrayList, ByVal searchStr As String, Optional ByVal comparingMethod As CompareMethod = CompareMethod.Text, Optional ByVal startsWith As Boolean = True, Optional ByVal endsWith As Boolean = True) As Integer
        If MyArray Is Nothing Then Return -1
        Dim i As Integer

        For i = 0 To MyArray.Count - 1
            If StartsWith = False And EndsWith = True Then
                If CStr(MyArray(i)).StartsWith(SearchStr) Then Return i
            ElseIf StartsWith = True And EndsWith = False Then
                If CStr(MyArray(i)).EndsWith(SearchStr) Then Return i
            Else
                If InStr(MyArray(i), SearchStr, ComparingMethod) > 0 Then Return i
            End If
        Next i

        Return -1
    End Function

    Public Function addZeros(ByVal myString As String, ByVal nbChiffres As Short, Optional ByVal addToLeft As Boolean = True) As String
        Dim nbZeros As Short = NbChiffres - MyString.Length
        Dim i As Short
        If NbZeros <= 0 Then Return MyString

        For i = 1 To NbZeros
            If AddToLeft Then
                MyString = "0" & MyString
            Else
                MyString &= "0"
            End If
        Next i

        Return MyString
    End Function

    Public Function bar(ByVal chemin As String) As String
        If Chemin = "" Then Return ""
        If Not Right(Chemin, 1) = "\" Then Return "\"

        Return ""
    End Function

    Public Function verifImpair(ByRef number As Integer) As Boolean
        If Right(Number, 1) = "1" Or Right(Number, 1) = "3" Or Right(Number, 1) = "5" Or Right(Number, 1) = "7" Or Right(Number, 1) = "9" Then
            VerifImpair = True
        Else
            VerifImpair = False
        End If
    End Function

    Public Function replaceAccents(ByVal myString As String) As String
        'Enlève tous les accents
        Dim i As Integer
        Dim NewLetter, myChar As Char
        For i = 1 To MyString.Length
            MyChar = Mid(MyString, i, 1)

            Select Case LCase(MyChar)
                Case "é", "è", "ê", "ë"
                    NewLetter = "e"
                Case "à", "á", "â", "ä"
                    NewLetter = "a"
                Case "ò", "ó", "ô", "ö"
                    NewLetter = "o"
                Case "ì", "í", "î", "ï"
                    NewLetter = "i"
                Case "ù", "ú", "û", "ü"
                    NewLetter = "u"
                Case Else
                    NewLetter = Nothing
            End Select

            If Not NewLetter = Nothing Then
                If Char.IsUpper(MyChar) = True Then NewLetter = UCase(NewLetter)

                If i = 1 Then
                    MyString = NewLetter & Right(MyString, MyString.Length - 1)
                ElseIf i = MyString.Length Then
                    MyString = Left(MyString, MyString.Length - 1) & NewLetter
                Else
                    MyString = Left(MyString, i - 1) & NewLetter & Right(MyString, MyString.Length - i)
                End If
            End If
        Next i

        Return MyString
    End Function

    Public Function splitStr(ByVal chaine As String) As Char()
        Dim i As Integer
        Dim tempTab() As Char
        ReDim TempTab(Chaine.Length - 1)

        For i = 0 To Chaine.Length - 1
            TempTab(i) = Chaine.Substring(i, 1)
        Next i

        Return TempTab
    End Function

    Public Function splitStr(ByVal chaine As String, ByVal startingPos As Integer) As Boolean()
        Dim i As Integer
        Dim tempTab() As Boolean
        ReDim TempTab(Chaine.Length - 1 - StartingPos)

        For i = StartingPos To Chaine.Length - 1
            Try
                TempTab(i - StartingPos) = CBool(Chaine.Substring(i, 1))
            Catch 'REM Exception not handle
                TempTab(i - StartingPos) = False
            End Try
        Next i

        Return TempTab
    End Function

    Public Function joinStr(ByVal chars() As Char) As String
        Dim i As Integer
        Dim tempChaine As String = ""

        For i = 0 To Chars.GetUpperBound(0)
            TempChaine &= Chars(0)
        Next i

        Return TempChaine
    End Function

    Public Function onlyNumeric(ByRef texte As String, Optional ByVal acceptedChar As String = "") As String
        Dim n, t As Integer
        n = 1
        If AcceptedChar <> "" Then AcceptedChar &= "§"
        AcceptedChar &= "0§1§2§3§4§5§6§7§8§9"

        Do
            If Not Texte = "" Then
                If IsAlpha(Mid(Texte, n, 1), AcceptedChar, True, False, False, True) = False Then
                    t = Len(Texte)
                    If t = 1 Then
                        Texte = ""
                    ElseIf t = 2 And n = 1 Then
                        Texte = Right(Texte, 1)
                    ElseIf t = 2 And n = 2 Then
                        Texte = Left(Texte, 1)
                    Else
                        Texte = Left(Texte, n - 1) & Right(Texte, Len(Texte) - n)
                    End If
                Else
                    n = n + 1
                End If
            End If
        Loop Until n > Len(Texte)

        OnlyNumeric = Texte
    End Function

    Public Function isAlpha(ByRef myChars As String, Optional ByVal extraChar As String = "", Optional ByRef acceptNumeric As Boolean = False, Optional ByVal onlyAlphabet As Boolean = True, Optional ByVal refuseAccents As Boolean = False, Optional ByVal onlyextraChars As Boolean = False) As Boolean
        Dim i As Integer
        Dim AlphaNum(), characters As String
        Dim found As Boolean
        'Formation du tableau Alpha Numérique
        'Index des différentes plages de caractères
        '0-9 : 0-9
        'A-Z : 10-35
        'Accents : 36-56
        'ExtraChar starts at 57
        If Not ExtraChar = "" And OnlyExtraChars = False Then ExtraChar = "§" & ExtraChar
        Dim accents As String = "§à§â§ä§á§è§é§ê§ë§ç§ï§î§ì§í§ò§ó§ô§ö§ù§ú§û§ü"
        Dim numbers As String = "0§1§2§3§4§5§6§7§8§9§"
        If OnlyExtraChars Then
            Characters = ExtraChar
        Else
            Characters = IIf(AcceptNumeric, numbers, "") & "a§b§c§d§e§f§g§h§i§j§k§l§m§n§o§p§q§r§s§t§u§v§w§x§y§z" & IIf(RefuseAccents, "", Accents) & ExtraChar
        End If
        AlphaNum = Split(Characters, "§")

        'If LastChar = -1 Then LastChar = UBound(AlphaNum)

        Dim curChars() As Char = MyChars.ToCharArray()
        For i = 1 To MyChars.Length
            Found = Characters.IndexOf(Char.ToLowerInvariant(curChars(i - 1))) <> -1
            'Found = Array.BinarySearch(AlphaNum, curChars(i - 1).ToString.ToLower) >= 0
            'Found = Array.IndexOf(AlphaNum, curChars(i - 1).ToString.ToLower) <> -1

            If OnlyExtraChars = False AndAlso Found = False AndAlso OnlyAlphabet = False AndAlso IsNumeric(Mid(MyChars, i, 1)) = False Then Found = True

            If Found = False Then Return False
        Next i

        IsAlpha = True
    End Function

    Public Function onlyAlpha(ByRef texte As String, Optional ByRef acceptedChar As String = "", Optional ByVal onlyAlphabet As Boolean = False, Optional ByVal acceptNumeric As Boolean = False, Optional ByVal refuseAccents As Boolean = False) As String
        Dim t, n As Integer
        n = 1

        Do
            If Not Texte = "" Then
                If IsAlpha(Mid(Texte, n, 1), AcceptedChar, AcceptNumeric, OnlyAlphabet, RefuseAccents) = False Then
                    t = Len(Texte)
                    If t = 1 Then
                        Texte = ""
                    ElseIf t = 2 And n = 1 Then
                        Texte = Right(Texte, 1)
                    ElseIf t = 2 And n = 2 Then
                        Texte = Left(Texte, 1)
                    Else
                        Texte = Left(Texte, n - 1) & Right(Texte, Len(Texte) - n)
                    End If
                Else
                    n = n + 1
                End If
            End If
        Loop Until n > Len(Texte) AndAlso (Texte = "" OrElse IsAlpha(Texte, AcceptedChar, AcceptNumeric, OnlyAlphabet, RefuseAccents) = True)

        OnlyAlpha = Texte
    End Function

    Public Function time1Suptime2(ByRef time1 As Date, ByRef time2 As Date, Optional ByRef egale As Boolean = False) As Boolean
        If Egale = True Then
            If Time1.Hour = Time2.Hour And Time1.Minute >= Time2.Minute Then
                Return True
            ElseIf Time1.Hour > Time2.Hour Then
                Return True
            Else
                Return False
            End If
        Else
            If Time1.Hour = Time2.Hour And Time1.Minute > Time2.Minute Then
                Return True
            ElseIf Time1.Hour > Time2.Hour Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Public Function anBissextile(ByVal annee As Integer) As Boolean
        Return Annee Mod 4 = 0 AndAlso (Annee Mod 100 <> 0 OrElse Annee Mod 400 = 0)
    End Function

    Public Function date1Infdate2(ByRef date1 As Date, ByRef date2 As Date, Optional ByRef egale As Boolean = False) As Boolean
        Dim passer As Boolean
        Dim Y1, y2 As Short
        Dim D2, M2, D1, m1 As Byte

        D2 = CByte(Date2.Day)
        M2 = CByte(Date2.Month)
        Y2 = CShort(Date2.Year)
        D1 = CByte(Date1.Day)
        M1 = CByte(Date1.Month)
        Y1 = CShort(Date1.Year)
        Passer = True

        If Y1 < Y2 Then
            Passer = False
        ElseIf Y1 = Y2 Then
            If M1 < M2 Then
                Passer = False
            ElseIf M1 = M2 Then
                If Egale = False Then
                    If D1 < D2 Then Passer = False
                Else
                    If D1 <= D2 Then Passer = False
                End If
            End If
        End If

        If Passer = False Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function trunkingWhiteSpaces(ByVal myString As String, Optional ByVal trunking As Boolean = True) As String
        If Trunking = False Then Return MyString
        Return MyString.Replace(" ", "")
    End Function

    Public Function getPasswordKey() As String
        Dim crypto As New System.Security.Cryptography.RSACryptoServiceProvider()
        Dim params As System.Security.Cryptography.RSAParameters = crypto.ExportParameters(True)

        Dim q As String = String.Join(",", Array.ConvertAll(Of Byte, String)(params.q, New System.Converter(Of Byte, String)(AddressOf Convert.ToString)))
        dim d As String = String.Join(",", Array.ConvertAll(Of Byte, String)(params.d, New System.Converter(Of Byte, String)(AddressOf Convert.ToString)))
        Dim p As String = String.Join(",", Array.ConvertAll(Of Byte, String)(params.p, New System.Converter(Of Byte, String)(AddressOf Convert.ToString)))
        Dim dp As String = String.Join(",", Array.ConvertAll(Of Byte, String)(params.dp, New System.Converter(Of Byte, String)(AddressOf Convert.ToString)))
        Dim modulus As String = String.Join(",", Array.ConvertAll(Of Byte, String)(params.modulus, New System.Converter(Of Byte, String)(AddressOf Convert.ToString)))
        Dim dq As String = String.Join(",", Array.ConvertAll(Of Byte, String)(params.dq, New System.Converter(Of Byte, String)(AddressOf Convert.ToString)))
        Dim inverseQ As String = String.Join(",", Array.ConvertAll(Of Byte, String)(params.inverseQ, New System.Converter(Of Byte, String)(AddressOf Convert.ToString)))
        Dim exponent As String = String.Join(",", Array.ConvertAll(Of Byte, String)(params.exponent, New System.Converter(Of Byte, String)(AddressOf Convert.ToString)))
        Dim cle As String = Q & "-" & D & "-" & P & "-" & DP & "-" & Modulus & "-" & DQ & "-" & InverseQ & "-" & Exponent
        Return Cle
    End Function

    Private Function getPasswordkeyParams(ByVal key As String) As System.Security.Cryptography.RSAParameters
        Dim params As New System.Security.Cryptography.RSAParameters()
        Dim keyParams() As String = Key.Split(New Char() {"-"})
        Dim q() As Byte = Array.ConvertAll(Of String, Byte)(keyParams(0).Split(New Char() {","}), New System.Converter(Of String, Byte)(AddressOf Convert.ToByte))
        Dim d() As Byte = Array.ConvertAll(Of String, Byte)(keyParams(1).Split(New Char() {","}), New System.Converter(Of String, Byte)(AddressOf Convert.ToByte))
        Dim p() As Byte = Array.ConvertAll(Of String, Byte)(keyParams(2).Split(New Char() {","}), New System.Converter(Of String, Byte)(AddressOf Convert.ToByte))
        Dim dp() As Byte = Array.ConvertAll(Of String, Byte)(keyParams(3).Split(New Char() {","}), New System.Converter(Of String, Byte)(AddressOf Convert.ToByte))
        Dim modulus() As Byte = Array.ConvertAll(Of String, Byte)(keyParams(4).Split(New Char() {","}), New System.Converter(Of String, Byte)(AddressOf Convert.ToByte))
        Dim dq() As Byte = Array.ConvertAll(Of String, Byte)(keyParams(5).Split(New Char() {","}), New System.Converter(Of String, Byte)(AddressOf Convert.ToByte))
        Dim inverseQ() As Byte = Array.ConvertAll(Of String, Byte)(keyParams(6).Split(New Char() {","}), New System.Converter(Of String, Byte)(AddressOf Convert.ToByte))
        Dim exponent() As Byte = Array.ConvertAll(Of String, Byte)(keyParams(7).Split(New Char() {","}), New System.Converter(Of String, Byte)(AddressOf Convert.ToByte))
        params.Q = Q
        params.D = D
        params.P = P
        params.DP = DP
        params.Modulus = Modulus
        params.DQ = DQ
        params.InverseQ = InverseQ
        params.Exponent = Exponent

        Return params
    End Function

    Public Function encrypt(ByVal text As String, ByVal key As String) As String
        Dim crypto As New System.Security.Cryptography.RSACryptoServiceProvider()
        crypto.ImportParameters(GetPasswordKeyParams(Key))

        Return String.Join(",", Array.ConvertAll(Of Byte, String)(crypto.Encrypt(System.Text.UTF7Encoding.UTF7.GetBytes(Text), False), New Converter(Of Byte, String)(AddressOf Convert.ToString)))
    End Function

    Public Function decrypt(ByVal text As String, ByVal key As String) As String
        If Text = "" Then Return ""

        Dim crypto As New System.Security.Cryptography.RSACryptoServiceProvider()
        crypto.ImportParameters(GetPasswordKeyParams(Key))

        Return System.Text.UTF7Encoding.UTF7.GetString(crypto.Decrypt(Array.ConvertAll(Of String, Byte)(Text.Split(New Char() {","}), New Converter(Of String, Byte)(AddressOf Convert.ToByte)), False))
    End Function
End Module
