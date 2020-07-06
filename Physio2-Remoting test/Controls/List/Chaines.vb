Imports System.Text.RegularExpressions

Module Chaines

    Public Function convertFromISO8859_1(ByVal value As String) As String
        Dim strIso1 As String = "=?iso-8859-1?q?"
        Dim strIso2 As String = "=?iso-8859-1?b?"
        Dim isIso1 As Boolean = False
        If value.ToLower.IndexOf(strIso1) >= 0 Then
            isIso1 = True
            value = System.Text.RegularExpressions.Regex.Replace(value, "\=\?iso\-8859\-1\?q\?(([^?])+)\?\=", "$1", RegexOptions.IgnoreCase)
            value = System.Text.RegularExpressions.Regex.Replace(value.Trim, "\n", "").Replace(vbLf, "").Replace(vbCr, "").Replace(Chr(9), "")
            value = value.Replace("_", " ")
        End If

        If value.ToLower.IndexOf(strIso2) >= 0 Then
            isIso1 = True
            value = System.Text.RegularExpressions.Regex.Replace(value, "\=\?iso\-8859\-1\?b\?(([^?])+)\?\=", "$1", RegexOptions.IgnoreCase)
            'Ensure no spaces
            Dim tmpValue As String = value
            If value.IndexOf(" ") <> -1 Then
                value = value.Substring(0, value.IndexOf(" "))
                value = convertFromBase64(value, True)
                value &= tmpValue.Substring(tmpValue.IndexOf(" "))
            Else
                value = convertFromBase64(value, True)
            End If
        End If

        'If isIso1 Then
        ''on remplace certain carateres sepeciaux
        value = value.Replace("=01", ChrW(1)).Replace("=02", ChrW(2)).Replace("=03", ChrW(3)).Replace("=04", ChrW(4)).Replace("=05", ChrW(5)).Replace("=06", ChrW(6)).Replace("=07", ChrW(7)).Replace("=08", ChrW(8)) _
        .Replace("=09", ChrW(9)).Replace("=0A", ChrW(10)).Replace("=0B", ChrW(11)).Replace("=0C", ChrW(12)).Replace("=0D", ChrW(13)).Replace("=0E", ChrW(14)).Replace("=0F", ChrW(15)).Replace("=10", ChrW(16)).Replace("=11", ChrW(17)) _
        .Replace("=12", ChrW(18)).Replace("=13", ChrW(19)).Replace("=14", ChrW(20)).Replace("=15", ChrW(21)).Replace("=16", ChrW(22)).Replace("=17", ChrW(23)).Replace("=18", ChrW(24)).Replace("=19", ChrW(25)).Replace("=1A", ChrW(26)) _
        .Replace("=1B", ChrW(27)).Replace("=1C", ChrW(28)).Replace("=1D", ChrW(29)).Replace("=1E", ChrW(30)).Replace("=1F", ChrW(31)).Replace("=20", ChrW(32)).Replace("=21", ChrW(33)).Replace("=22", ChrW(34)).Replace("=23", ChrW(35)) _
        .Replace("=26", ChrW(38)).Replace("=2E", ChrW(46)).Replace("=7B", ChrW(123)).Replace("=7C", ChrW(124)).Replace("=7D", ChrW(125)) _
        .Replace("=A0", ChrW(160)).Replace("=A1", ChrW(161)).Replace("=A2", ChrW(162)).Replace("=A3", ChrW(163)).Replace("=A4", ChrW(164)).Replace("=A5", ChrW(165)) _
        .Replace("=A6", ChrW(166)).Replace("=A7", ChrW(167)).Replace("=A8", ChrW(168)).Replace("=A9", ChrW(169)).Replace("=AA", ChrW(170)).Replace("=AB", ChrW(171)).Replace("=AC", ChrW(172)) _
        .Replace("=AD", ChrW(173)).Replace("=AE", ChrW(174)).Replace("=AF", ChrW(175)).Replace("=B0", ChrW(176)).Replace("=B1", ChrW(177)).Replace("=B2", ChrW(178)).Replace("=B3", ChrW(179)) _
        .Replace("=B4", ChrW(180)).Replace("=B5", ChrW(181)).Replace("=B6", ChrW(182)).Replace("=B7", ChrW(183)).Replace("=B8", ChrW(184)).Replace("=B9", ChrW(185)).Replace("=BA", ChrW(186)) _
        .Replace("=BB", ChrW(187)).Replace("=BC", ChrW(188)).Replace("=BD", ChrW(189)).Replace("=BE", ChrW(190)).Replace("=BF", ChrW(191)).Replace("=C0", ChrW(192)).Replace("=C1", ChrW(193)) _
        .Replace("=C2", ChrW(194)).Replace("=C3", ChrW(195)).Replace("=C4", ChrW(196)).Replace("=C5", ChrW(197)).Replace("=C6", ChrW(198)).Replace("=C7", ChrW(199)).Replace("=C8", ChrW(200)) _
        .Replace("=C9", ChrW(201)).Replace("=CA", ChrW(202)).Replace("=CB", ChrW(203)).Replace("=CC", ChrW(204)).Replace("=CD", ChrW(205)).Replace("=CE", ChrW(206)).Replace("=CF", ChrW(207)) _
        .Replace("=D0", ChrW(208)).Replace("=D1", ChrW(209)).Replace("=D2", ChrW(210)).Replace("=D3", ChrW(211)).Replace("=D4", ChrW(212)).Replace("=D5", ChrW(213)).Replace("=D6", ChrW(214)) _
        .Replace("=D7", ChrW(215)).Replace("=D8", ChrW(216)).Replace("=D9", ChrW(217)).Replace("=DA", ChrW(218)).Replace("=DB", ChrW(219)).Replace("=DC", ChrW(220)).Replace("=DD", ChrW(221)) _
        .Replace("=DE", ChrW(222)).Replace("=DF", ChrW(223)).Replace("=E0", ChrW(224)).Replace("=E1", ChrW(225)).Replace("=E2", ChrW(226)).Replace("=E3", ChrW(227)).Replace("=E4", ChrW(228)) _
        .Replace("=E5", ChrW(229)).Replace("=E6", ChrW(230)).Replace("=E7", ChrW(231)).Replace("=E8", ChrW(232)).Replace("=E9", ChrW(233)).Replace("=EA", ChrW(234)).Replace("=EB", ChrW(235)) _
        .Replace("=EC", ChrW(236)).Replace("=ED", ChrW(237)).Replace("=EE", ChrW(238)).Replace("=EF", ChrW(239)).Replace("=F0", ChrW(240)).Replace("=F1", ChrW(241)).Replace("=F2", ChrW(242)) _
        .Replace("=F3", ChrW(243)).Replace("=F4", ChrW(244)).Replace("=F5", ChrW(245)).Replace("=F6", ChrW(246)).Replace("=F7", ChrW(247)).Replace("=F8", ChrW(248)).Replace("=F9", ChrW(249)) _
        .Replace("=FA", ChrW(250)).Replace("=FB", ChrW(251)).Replace("=FC", ChrW(252)).Replace("=FD", ChrW(253)).Replace("=FE", ChrW(254)).Replace("=FF", ChrW(255)) _
        .Replace("=3D", ChrW(61)).Replace("=2C", ChrW(44)).Replace("=" & vbCrLf, "").Replace("=3F", "?").Replace("=3B", "")
        'End If

        Return value
    End Function

    Public Function convertFromWindows1252(ByVal value As String) As String
        Dim strIso As String = "=?windows-1252?q?"
        If value.ToLower.IndexOf(strIso) >= 0 Then
            value = System.Text.RegularExpressions.Regex.Replace(value, "\=\?windows\-1252\?q\?(([^?][^=]|[?][^=]?|[?])+)\?\=", "$1", System.Text.RegularExpressions.RegexOptions.IgnoreCase)
            value = System.Text.RegularExpressions.Regex.Replace(value.Trim, "\n", "").Replace(vbLf, "").Replace(vbCr, "").Replace(Chr(9), "")
            value = value.Replace("_", " ")
        End If

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
            Dim bytes() As Byte = convertFromBase64(base64)

            If returnString Then
                Dim str As String = ""
                For i As Integer = 0 To bytes.GetUpperBound(0)
                    str &= Chr(bytes(i))
                Next i
                Return str
            End If

            Return bytes
        Catch
        End Try

        Return ""
    End Function

    Public Function firstPart(ByVal texte As String, ByVal cursorPos As Integer) As String
        If (cursorPos - 1) < 0 Then Return ""

        Return texte.Substring(0, cursorPos)
    End Function

    Public Function measureString(ByVal strText As String, ByVal strFont As Font) As Size
        Return measureString(strText, strFont.FontFamily.Name, strFont.Size, strFont.Style).ToSize
        'Windows.Forms.TextRenderer.MeasureText(StrText, StrFont, New Size(0, 0), TextFormatFlags.NoPadding)
    End Function

    Public Function measureString(ByVal bannerText As String, ByVal fontName As String, ByVal fontSize As Single, ByVal fontStyle As FontStyle) As SizeF
        Dim b As Bitmap
        Dim g As Graphics
        Dim f As New Font(fontName, fontSize, fontStyle)

        ' Compute the string dimensions in the given font
        b = New Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        g = Graphics.FromImage(b)
        Dim stringSize As SizeF = g.MeasureString(bannerText, f)
        g.Dispose()
        b.Dispose()

        Return stringSize
    End Function

    Public Function isValidTinyDate(ByVal dateToTest As String, Optional ByVal dateSeparator As Char = "/") As Boolean
        Dim sDate() As String = Split(dateToTest, dateSeparator)
        If sDate Is Nothing Then Return False

        Select Case UBound(sDate)
            Case 0
                If IsNumeric(sDate(0)) = False Then Return False
                If sDate(0) < 1900 Or sDate(0) > 9000 Then Return False

            Case 1
                If IsNumeric(sDate(0)) = False Then Return False
                If IsNumeric(sDate(1)) = False Then Return False
                If sDate(0) < 1900 Or sDate(0) > 9000 Then Return False
                If sDate(1) <= 0 Or sDate(1) > 12 Then Return False

            Case 2
                If IsNumeric(sDate(0)) = False Then Return False
                If IsNumeric(sDate(1)) = False Then Return False
                If IsNumeric(sDate(2)) = False Then Return False
                If sDate(0) < 1900 Or sDate(0) > 9000 Then Return False
                If sDate(1) <= 0 Or sDate(1) > 12 Then Return False
                If sDate(1) <= 0 Or sDate(1) > 31 Then Return False
        End Select

        Return True
    End Function

    Public Function convertirMontantEnMots(ByVal chiffre As Single, Optional ByVal decimalsInLetter As Boolean = False) As String
        If (Math.Abs(chiffre) / Math.Floor(Math.Abs(chiffre))) > 1 Then
            'Si le nombre contient des décimales
            Dim decimaleNumber As Single
            Dim number As Single = Math.Floor(Math.Abs(chiffre))

            decimaleNumber = CInt((chiffre - number) * 10 ^ (addZeros(chiffre.ToString, number.ToString.Length + 3, False).Length - number.ToString.Length - 1))
            Dim a As Single = chiffre.ToString.Length - number.ToString.Length - 1
            If decimalsInLetter Then Return subConvertirMontantEnMots(number) & "et " & subConvertirMontantEnMots(decimaleNumber) & "cent(s)"

            Return IIf(chiffre < 0, "MOINS ", "") & subConvertirMontantEnMots(number) & "et " & decimaleNumber & " cent(s)"
        Else
            Return IIf(chiffre < 0, "MOINS ", "") & subConvertirMontantEnMots(Math.Abs(chiffre))
        End If
    End Function

    Private Function subConvertirMontantEnMots(ByVal chiffre As Single) As String
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
                dizaine = Math.Floor((y - centaine * 100) / 10)
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
                If (dizaine = 1 And unite > 0) Or (dizaine > 1 And dizaine < 9 And unite > 0) Or dizaine = 9 Then
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
                        If preUnite <> "" And dizaine < 8 Then preUnite = " et "
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

    Public Function forceManaging(ByRef myValue As String, ByVal currencyBox As Boolean, ByVal acceptedText As String, ByVal acceptAlpha As Boolean, ByVal acceptNumeric As Boolean, ByVal onlyAlphabet As Boolean, ByVal refuseAccents As Boolean, Optional ByVal acceptedChars As String = "", Optional ByVal refusedChars As String = "", Optional ByVal matchExp As String = "", Optional ByVal wordCapital As Boolean = False, Optional ByVal allWords As Boolean = False, Optional ByVal allCapital As Boolean = False, Optional ByVal allLower As Boolean = False, Optional ByVal nbDecimals As Short = 0, Optional ByVal acceptNegative As Boolean = False, Optional ByRef lastSelection As Integer = -1) As Boolean
        'Dim tmpSB As New System.Text.StringBuilder(MyValue)
        'Dim managed As Boolean = ForceManaging(tmpSB, CurrencyBox, AcceptedText, AcceptAlpha, AcceptNumeric, OnlyAlphabet, RefuseAccents, AcceptedChars, RefusedChars, MatchExp, WordCapital, AllWords, AllCapital, AllLower, NbDecimals, AcceptNegative, LastSelection)
        'If managed Then MyValue = tmpSB.ToString

        'Return managed
        'Dim tmpTimer As Long = Date.Now.Ticks

        Dim i As Integer
        Dim myValue2 As String
        Dim isNegative As Boolean
        If myValue Is Nothing Then myValue = ""

        If myValue = "" And currencyBox = True Then myValue = "0" : lastSelection = 2 : Return True
        If currencyBox = True AndAlso acceptNegative = False AndAlso Integer.TryParse(myValue, 0) AndAlso myValue < 0 Then myValue = 0 : lastSelection = 2 : Return True

        If myValue.Length = 0 Then Return False
        myValue = myValue.Replace("§", "")

        If acceptAlpha = True Then myValue = onlyAlpha(myValue, acceptedChars, onlyAlphabet, acceptNumeric, refuseAccents)
        If acceptAlpha = False And acceptNumeric = True Then myValue = onlyNumeric(myValue, acceptedChars)

        If refusedChars <> "" Then
            For i = 1 To refusedChars.Length
                myValue = Replace(myValue, Mid(refusedChars, i, 1), "")
            Next i
        End If

        If myValue Is Nothing Then myValue = ""

        If matchExp <> "" Then myValue = applyMatch(myValue, matchExp)
        If wordCapital = True Then myValue = firstLetterCapital(myValue, allWords)
        If allCapital = True Then myValue = myValue.ToUpper
        If allLower = True Then myValue = myValue.ToLower

        If currencyBox = True Then
            Dim pos As Short = 0
            If Not acceptedText = "" Then
                myValue = acceptedText
            Else
                pos = InStr(myValue, ".")
                If pos > 0 Then
                    myValue = myValue.Replace(".", ",")
                    lastSelection = pos
                End If

                Do
                    myValue2 = myValue
                    myValue = Replace(myValue, ",", "", , 1)
                    pos = InStr(myValue, ",")
                Loop While pos > 0

                myValue = myValue2
            End If

            If myValue.Length > 1 Then
                If Not myValue.Substring(0, 2) = "0," Then
                    'TODO  : REMOVED On Error GoTo SkipZeros
                    Do While myValue <> "" AndAlso myValue.Substring(0, 1) = "0"
                        myValue = myValue.Substring(1)
                    Loop
SkipZeros:
                    If myValue = "" Then myValue = "0"
                    'TODO  : REMOVED On Error GoTo 0
                End If
            End If

            pos = InStr(myValue, ",")
            If pos > 0 And nbDecimals = 0 Then
                myValue = myValue.Replace(",", "")
                lastSelection = pos
            ElseIf pos > 0 And nbDecimals > 0 Then
                myValue = Mid(myValue, 1, pos) & Mid(myValue, pos + 1, nbDecimals)
            End If

            If InStr(myValue, "-") > 0 Then isNegative = True
            myValue = myValue.Replace("-", "")
            If myValue = "" Then myValue = "0"
            If acceptNegative = True And isNegative = True Then myValue = "-" & myValue
            If myValue = "-0" Then myValue = "0"
            If myValue.Length > 0 Then If myValue.Substring(0, 1) = "," Then myValue = "0" & myValue : lastSelection = myValue.Length
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
        For i = 1 To matchExp.Length
            Select Case Mid(matchExp, i, 1).ToUpper
                Case "A"
                    If IsNumeric(Mid(textToApply, n, 1)) = False Then
                        n += 1
                    Else
                        Exit For
                    End If

                Case "1"
                    If IsNumeric(Mid(textToApply, n, 1)) = True Then
                        n += 1
                    Else
                        Exit For
                    End If

                Case Else
            End Select
        Next i

        n -= 1
        If textToApply <> "" Then
            If n > 0 Then
                If n <= textToApply.Length Then textToApply = textToApply.Substring(0, n)
            Else
                textToApply = ""
            End If
        End If


        Return textToApply
    End Function

    Public Function firstLetterCapital(ByVal stringToConvert As String, Optional ByVal allWords As Boolean = False) As String
        Dim i As Integer
        Dim previousChar As Char = Nothing
        Dim myChar As Char = Nothing
        Dim LeftPart, rightPart As String
        Dim upTo As Integer = 0

        For i = 1 To stringToConvert.Length - 1
            myChar = Mid(stringToConvert, i, 1) : LeftPart = "" : rightPart = ""
            If previousChar = Nothing Or previousChar = " " Or previousChar = "-" Or previousChar = "'" Then
                upTo += 1
                If i > 1 Then LeftPart = stringToConvert.Substring(0, i - 1)
                If i < stringToConvert.Length Then rightPart = stringToConvert.Substring(i, stringToConvert.Length - i)
                stringToConvert = LeftPart & Char.ToUpper(myChar) & rightPart
                If allWords = False And upTo = 1 Then Exit For
            End If
            previousChar = myChar
        Next i

        Return stringToConvert
    End Function

    Public Function firstLettersCapital(ByVal stringToConvert As String) As String
        Return firstLetterCapital(stringToConvert, True)
    End Function

    Public Function copyArrayToString(ByVal originArray() As Boolean) As String
        Dim stringToCopy As String = ""
        Dim i As Integer

        For i = 0 To UBound(originArray)
            If originArray(i) = True Then
                stringToCopy &= "1"
            Else
                stringToCopy &= "0"
            End If
        Next i

        Return stringToCopy
    End Function

#Region "CopyArray"
    Public Sub copyArray(ByRef destinationArray() As Boolean, Optional ByVal stringToSplit As String = "", Optional ByVal originArray() As Boolean = Nothing)
        Dim i, arrayMax As Integer
        Dim MyTempArray(), myFalseChar As Char
        myFalseChar = "0"c

        If stringToSplit <> "" Then
            MyTempArray = splitStr(stringToSplit)
            arrayMax = UBound(MyTempArray)
            ReDim destinationArray(arrayMax)
            For i = 0 To arrayMax
                If MyTempArray(i) = myFalseChar Then
                    destinationArray(i) = False
                Else
                    destinationArray(i) = True
                End If
            Next i
        ElseIf Not originArray Is Nothing Then
            arrayMax = UBound(originArray)
            ReDim destinationArray(arrayMax)
            For i = 0 To arrayMax
                destinationArray(i) = originArray(i)
            Next i
        End If
    End Sub

    Public Sub copyArray(ByRef destinationArray() As String, Optional ByVal stringToSplit As String = "", Optional ByVal originArray() As String = Nothing)
        Dim i, arrayMax As Integer
        Dim myTempArray() As Char

        If stringToSplit <> "" Then
            myTempArray = splitStr(stringToSplit)
            arrayMax = UBound(myTempArray)
            ReDim destinationArray(arrayMax)
            For i = 0 To arrayMax
                destinationArray(i) = myTempArray(i)
            Next i
        ElseIf Not originArray Is Nothing Then
            arrayMax = UBound(originArray)
            ReDim destinationArray(arrayMax)
            For i = 0 To arrayMax
                destinationArray(i) = originArray(i)
            Next i
        End If
    End Sub

    Public Sub copyArray(ByRef destinationArray() As Integer, Optional ByVal stringToSplit As String = "", Optional ByVal originArray() As Integer = Nothing)
        Dim i, arrayMax As Integer
        Dim myTempArray() As Char

        If stringToSplit <> "" Then
            myTempArray = splitStr(stringToSplit)
            arrayMax = UBound(myTempArray)
            ReDim destinationArray(arrayMax)
            For i = 0 To arrayMax
                If Char.IsDigit(myTempArray(i)) Then destinationArray(i) = CInt(myTempArray(i).ToString)
            Next i
        ElseIf Not originArray Is Nothing Then
            arrayMax = UBound(originArray)
            ReDim destinationArray(arrayMax)
            For i = 0 To arrayMax
                destinationArray(i) = originArray(i)
            Next i
        End If
    End Sub
#End Region

    Public Function removeItemArray(ByVal myArray As String(), ByVal indexToRemove As Integer) As String()
        If indexToRemove < myArray.GetLowerBound(0) Or indexToRemove > myArray.GetUpperBound(0) Then Return myArray

        Dim myArray2() As String = {}
        Dim i, n As Integer

        n = 0
        For i = myArray.GetLowerBound(0) To myArray.GetUpperBound(0)
            If i <> indexToRemove Then
                ReDim Preserve myArray2(n)
                myArray2(n) = myArray(i)
                n += 1
            End If
        Next i

        Return myArray2
    End Function

    Public Function searchArray(ByVal myArray As String(), ByVal searchStr As String, Optional ByVal comparingMethod As CompareMethod = CompareMethod.Text) As Integer
        If myArray Is Nothing Then Return -1
        Dim i As Integer

        For i = myArray.GetLowerBound(0) To myArray.GetUpperBound(0)
            If InStr(myArray(i), searchStr, comparingMethod) > 0 Then Return i
        Next i

        Return -1
    End Function

    Public Function searchArray(ByVal myArray As ArrayList, ByVal searchStr As String, Optional ByVal comparingMethod As CompareMethod = CompareMethod.Text, Optional ByVal startsWith As Boolean = True, Optional ByVal endsWith As Boolean = True) As Integer
        If myArray Is Nothing Then Return -1
        Dim i As Integer

        For i = 0 To myArray.Count - 1
            If startsWith = False And endsWith = True Then
                If CStr(myArray(i)).StartsWith(searchStr) Then Return i
            ElseIf startsWith = True And endsWith = False Then
                If CStr(myArray(i)).EndsWith(searchStr) Then Return i
            Else
                If InStr(myArray(i), searchStr, comparingMethod) > 0 Then Return i
            End If
        Next i

        Return -1
    End Function

    Public Function addZeros(ByVal myString As String, ByVal nbChiffres As Short, Optional ByVal addToLeft As Boolean = True) As String
        Dim nbZeros As Short = nbChiffres - myString.Length
        Dim i As Short
        If nbZeros <= 0 Then Return myString

        For i = 1 To nbZeros
            If addToLeft Then
                myString = "0" & myString
            Else
                myString &= "0"
            End If
        Next i

        Return myString
    End Function

    Public Function bar(ByVal chemin As String) As String
        If chemin = "" Then Return ""
        If Not Right(chemin, 1) = "\" Then Return "\"

        Return ""
    End Function

    Public Function verifImpair(ByRef number As Integer) As Boolean
        If Right(number, 1) = "1" Or Right(number, 1) = "3" Or Right(number, 1) = "5" Or Right(number, 1) = "7" Or Right(number, 1) = "9" Then
            verifImpair = True
        Else
            verifImpair = False
        End If
    End Function

    Public Function replaceAccents(ByVal myString As String) As String
        'Enlève tous les accents
        Dim i As Integer
        Dim NewLetter, myChar As Char
        For i = 1 To myString.Length
            myChar = Mid(myString, i, 1)

            Select Case LCase(myChar)
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
                If Char.IsUpper(myChar) = True Then NewLetter = UCase(NewLetter)

                If i = 1 Then
                    myString = NewLetter & Right(myString, myString.Length - 1)
                ElseIf i = myString.Length Then
                    myString = Left(myString, myString.Length - 1) & NewLetter
                Else
                    myString = Left(myString, i - 1) & NewLetter & Right(myString, myString.Length - i)
                End If
            End If
        Next i

        Return myString
    End Function

    Public Function splitStr(ByVal chaine As String) As Char()
        Dim i As Integer
        Dim tempTab() As Char
        ReDim tempTab(chaine.Length - 1)

        For i = 0 To chaine.Length - 1
            tempTab(i) = chaine.Substring(i, 1)
        Next i

        Return tempTab
    End Function

    Public Function splitStr(ByVal chaine As String, ByVal startingPos As Integer) As Boolean()
        Dim i As Integer
        Dim tempTab() As Boolean
        ReDim tempTab(chaine.Length - 1 - startingPos)

        For i = startingPos To chaine.Length - 1
            Try
                tempTab(i - startingPos) = CBool(chaine.Substring(i, 1))
            Catch
                tempTab(i - startingPos) = False
            End Try
        Next i

        Return tempTab
    End Function

    Public Function joinStr(ByVal chars() As Char) As String
        Dim i As Integer
        Dim tempChaine As String = ""

        For i = 0 To chars.GetUpperBound(0)
            tempChaine &= chars(0)
        Next i

        Return tempChaine
    End Function

    Public Function onlyNumeric(ByRef texte As String, Optional ByVal acceptedChar As String = "") As String
        Dim n, t As Integer
        n = 1
        If acceptedChar <> "" Then acceptedChar &= "§"
        acceptedChar &= "0§1§2§3§4§5§6§7§8§9"

        Do
            If Not texte = "" Then
                If isAlpha(Mid(texte, n, 1), acceptedChar, True, False, False, True) = False Then
                    t = Len(texte)
                    If t = 1 Then
                        texte = ""
                    ElseIf t = 2 And n = 1 Then
                        texte = Right(texte, 1)
                    ElseIf t = 2 And n = 2 Then
                        texte = Left(texte, 1)
                    Else
                        texte = Left(texte, n - 1) & Right(texte, Len(texte) - n)
                    End If
                Else
                    n = n + 1
                End If
            End If
        Loop Until n > Len(texte)

        onlyNumeric = texte
    End Function

    Public Function isEmail(ByVal email As String) As Boolean
        Return Regex.IsMatch(email, "^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$")
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
        If Not extraChar = "" And onlyextraChars = False Then extraChar = "§" & extraChar
        Dim accents As String = "§à§â§ä§á§è§é§ê§ë§ç§ï§î§ì§í§ò§ó§ô§ö§ù§ú§û§ü"
        Dim numbers As String = "0§1§2§3§4§5§6§7§8§9§"
        If onlyextraChars Then
            characters = extraChar
        Else
            characters = IIf(acceptNumeric, numbers, "") & "a§b§c§d§e§f§g§h§i§j§k§l§m§n§o§p§q§r§s§t§u§v§w§x§y§z" & IIf(refuseAccents, "", accents) & extraChar
        End If
        AlphaNum = Split(characters, "§")

        'If LastChar = -1 Then LastChar = UBound(AlphaNum)

        Dim curChars() As Char = myChars.ToCharArray()
        For i = 1 To myChars.Length
            found = characters.IndexOf(Char.ToLowerInvariant(curChars(i - 1))) <> -1
            'Found = Array.BinarySearch(AlphaNum, curChars(i - 1).ToString.ToLower) >= 0
            'Found = Array.IndexOf(AlphaNum, curChars(i - 1).ToString.ToLower) <> -1

            If onlyextraChars = False AndAlso found = False AndAlso onlyAlphabet = False AndAlso IsNumeric(Mid(myChars, i, 1)) = False Then found = True

            If found = False Then Return False
        Next i

        isAlpha = True
    End Function

    Public Function onlyAlpha(ByRef texte As String, Optional ByRef acceptedChar As String = "", Optional ByVal onlyAlphabet As Boolean = False, Optional ByVal acceptNumeric As Boolean = False, Optional ByVal refuseAccents As Boolean = False) As String
        Dim t, n As Integer
        n = 1

        Do
            If Not texte = "" Then
                If isAlpha(Mid(texte, n, 1), acceptedChar, acceptNumeric, onlyAlphabet, refuseAccents) = False Then
                    t = Len(texte)
                    If t = 1 Then
                        texte = ""
                    ElseIf t = 2 And n = 1 Then
                        texte = Right(texte, 1)
                    ElseIf t = 2 And n = 2 Then
                        texte = Left(texte, 1)
                    Else
                        texte = Left(texte, n - 1) & Right(texte, Len(texte) - n)
                    End If
                Else
                    n = n + 1
                End If
            End If
        Loop Until n > Len(texte) AndAlso (texte = "" OrElse isAlpha(texte, acceptedChar, acceptNumeric, onlyAlphabet, refuseAccents) = True)

        onlyAlpha = texte
    End Function

    Public Function time1Suptime2(ByRef time1 As Date, ByRef time2 As Date, Optional ByRef egale As Boolean = False) As Boolean
        If egale = True Then
            If time1.Hour = time2.Hour And time1.Minute >= time2.Minute Then
                Return True
            ElseIf time1.Hour > time2.Hour Then
                Return True
            Else
                Return False
            End If
        Else
            If time1.Hour = time2.Hour And time1.Minute > time2.Minute Then
                Return True
            ElseIf time1.Hour > time2.Hour Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Public Function anBissextile(ByVal annee As Integer) As Boolean
        Dim an3, an2, an1 As String
        an1 = CStr(annee / 400)
        an2 = CStr(annee / 100)
        an3 = CStr(annee / 4)
        If InStr(1, an3, ",") = 0 And InStr(1, an3, ".") = 0 Then Return True
        If InStr(1, an2, ",") = 0 And InStr(1, an2, ".") = 0 Then Return False
        If InStr(1, an1, ",") = 0 And InStr(1, an1, ".") = 0 Then Return True
    End Function

    Public Function trunkingWhiteSpaces(ByVal myString As String, Optional ByVal trunking As Boolean = True) As String
        If trunking = False Then Return myString
        Return myString.Replace(" ", "")
    End Function

    Public Function getPasswordKey() As String
        Dim crypto As New System.Security.Cryptography.RSACryptoServiceProvider()
        Dim params As System.Security.Cryptography.RSAParameters = crypto.ExportParameters(True)

        Dim q As String = String.Join(",", Array.ConvertAll(Of Byte, String)(params.Q, New System.Converter(Of Byte, String)(AddressOf Convert.ToString)))
        Dim d As String = String.Join(",", Array.ConvertAll(Of Byte, String)(params.D, New System.Converter(Of Byte, String)(AddressOf Convert.ToString)))
        Dim p As String = String.Join(",", Array.ConvertAll(Of Byte, String)(params.P, New System.Converter(Of Byte, String)(AddressOf Convert.ToString)))
        Dim dp As String = String.Join(",", Array.ConvertAll(Of Byte, String)(params.DP, New System.Converter(Of Byte, String)(AddressOf Convert.ToString)))
        Dim modulus As String = String.Join(",", Array.ConvertAll(Of Byte, String)(params.Modulus, New System.Converter(Of Byte, String)(AddressOf Convert.ToString)))
        Dim dq As String = String.Join(",", Array.ConvertAll(Of Byte, String)(params.DQ, New System.Converter(Of Byte, String)(AddressOf Convert.ToString)))
        Dim inverseQ As String = String.Join(",", Array.ConvertAll(Of Byte, String)(params.InverseQ, New System.Converter(Of Byte, String)(AddressOf Convert.ToString)))
        Dim exponent As String = String.Join(",", Array.ConvertAll(Of Byte, String)(params.Exponent, New System.Converter(Of Byte, String)(AddressOf Convert.ToString)))
        Dim cle As String = q & "-" & d & "-" & p & "-" & dp & "-" & modulus & "-" & dq & "-" & inverseQ & "-" & exponent
        Return cle
    End Function

    Private Function getPasswordkeyParams(ByVal key As String) As System.Security.Cryptography.RSAParameters
        Dim params As New System.Security.Cryptography.RSAParameters()
        Dim keyParams() As String = key.Split(New Char() {"-"})
        Dim q() As Byte = Array.ConvertAll(Of String, Byte)(keyParams(0).Split(New Char() {","}), New System.Converter(Of String, Byte)(AddressOf Convert.ToByte))
        Dim d() As Byte = Array.ConvertAll(Of String, Byte)(keyParams(1).Split(New Char() {","}), New System.Converter(Of String, Byte)(AddressOf Convert.ToByte))
        Dim p() As Byte = Array.ConvertAll(Of String, Byte)(keyParams(2).Split(New Char() {","}), New System.Converter(Of String, Byte)(AddressOf Convert.ToByte))
        Dim dp() As Byte = Array.ConvertAll(Of String, Byte)(keyParams(3).Split(New Char() {","}), New System.Converter(Of String, Byte)(AddressOf Convert.ToByte))
        Dim modulus() As Byte = Array.ConvertAll(Of String, Byte)(keyParams(4).Split(New Char() {","}), New System.Converter(Of String, Byte)(AddressOf Convert.ToByte))
        Dim dq() As Byte = Array.ConvertAll(Of String, Byte)(keyParams(5).Split(New Char() {","}), New System.Converter(Of String, Byte)(AddressOf Convert.ToByte))
        Dim inverseQ() As Byte = Array.ConvertAll(Of String, Byte)(keyParams(6).Split(New Char() {","}), New System.Converter(Of String, Byte)(AddressOf Convert.ToByte))
        Dim exponent() As Byte = Array.ConvertAll(Of String, Byte)(keyParams(7).Split(New Char() {","}), New System.Converter(Of String, Byte)(AddressOf Convert.ToByte))
        params.Q = q
        params.D = d
        params.P = p
        params.DP = dp
        params.Modulus = modulus
        params.DQ = dq
        params.InverseQ = inverseQ
        params.Exponent = exponent

        Return params
    End Function

    Public Function encrypt(ByVal text As String, ByVal key As String) As String
        Dim crypto As New System.Security.Cryptography.RSACryptoServiceProvider()
        crypto.ImportParameters(getPasswordkeyParams(key))

        Return String.Join(",", Array.ConvertAll(Of Byte, String)(crypto.Encrypt(System.Text.UTF7Encoding.UTF7.GetBytes(text), False), New Converter(Of Byte, String)(AddressOf Convert.ToString)))

        REM OLD method
        '        Dim Y2, X2, Y, X As Integer
        '        Encrypt = ""

        '        If Len(Text) = 2 Then
        '            Encrypt = Text
        '            Exit Function
        '        End If
        '        Dim Mat(), Code(), Txt(), AmountOfRows As Short

        '        'Get the numbers from Matrix and put them in Mat
        '        ReDim Preserve Mat(0)
        '        Do Until Matrix = ""
        '            ReDim Preserve Mat(UBound(Mat) + 1)

        '            If InStr(Matrix, ",") <> 0 Then
        '                Mat(UBound(Mat)) = CShort(Left(Matrix, InStr(Matrix, ",") - 1))
        '                Matrix = Right(Matrix, Len(Matrix) - InStr(Matrix, ","))
        '            Else
        '                Mat(UBound(Mat)) = CShort(Matrix)
        '                Matrix = ""
        '            End If
        '        Loop
        '        Do Until Int(System.Math.Sqrt(UBound(Mat))) = System.Math.Sqrt(UBound(Mat))
        '            ReDim Preserve Mat(UBound(Mat) + 1)
        '            Mat(UBound(Mat)) = 3
        '        Loop
        '        AmountOfRows = Int(System.Math.Sqrt(UBound(Mat)))


        '        ReDim Preserve Txt(AmountOfRows)
        '        ReDim Preserve Code(AmountOfRows)
        '        Do Until Len(Text) < AmountOfRows
        '            'Get values from text
        '            For X = 1 To UBound(Code)
        '                Txt(X) = Asc(Left(Text, 1))
        '                Text = Right(Text, Len(Text) - 1)
        '            Next

        '            'Multiply the matrixes
        '            For X = 1 To UBound(Txt)
        '                Code(X) = 0
        '                For Y = 1 To AmountOfRows
        '                    Code(X) = Code(X) + (Mat(((UBound(Mat) / AmountOfRows) * (Y - 1)) + X) * Txt(Y))
        '                Next
        '            Next

        '            'Put the new values in the text
        '            For X = 1 To UBound(Txt)
        '                'More often then not, the value after being run
        '                'through the matrix is over 255. Because we can't
        '                'use the Chr function with numbers over 255 we need
        '                'a more complex way to put the value in the text.
        '                'Hear we will do it by finding a number to divide
        '                'it by then insert that number and the result. This
        '                'doubles the length, but what can you do?
        '                Y = 1
        '                Do Until Code(X) / Y <= 256 And System.Math.Round(Code(X) / Y, 0) * Y = Code(X)
        '                    Y = Y + 1
        '                    If Code(X) = Y Then 'The number is prime. Now we have to
        '                        'assign a number to every prime number
        '                        'above 255(1-257,2-263,3-269,ect). Then
        '                        'insert an identifier character and the
        '                        'prime number id.
        '                        Y = 0
        '                        X2 = 255
        '                        Do
        '                            X2 = X2 + 1
        '                            For Y2 = 2 To Code(X)
        '                                If Int(X2 / Y2) = X2 / Y2 Then GoTo NextNum
        '                                If Y2 = X2 - 1 Then
        '                                    Y = Y + 1
        '                                    If X2 = Code(X) Then
        '                                        X2 = 0
        '                                    End If
        '                                    GoTo NextNum
        '                                End If
        '                            Next
        'NextNum:

        '                            If X2 = 0 Then Exit Do
        '                        Loop
        '                        Encrypt = Encrypt & Chr(255) & Chr(Y) 'Chr$(255) will be the identifier
        '                        Y = 0
        '                        Exit Do
        '                    End If
        '                Loop
        '                If Y <> 0 Then Encrypt = Encrypt & Chr(Y) & Chr(Code(X) / Y)
        '            Next
        '        Loop
        '        Encrypt = Encrypt & Text
    End Function

    Public Function decrypt(ByVal text As String, ByVal key As String) As String
        If text = "" Then Return ""

        Dim crypto As New System.Security.Cryptography.RSACryptoServiceProvider()
        crypto.ImportParameters(getPasswordkeyParams(key))

        Return System.Text.UTF7Encoding.UTF7.GetString(crypto.Decrypt(Array.ConvertAll(Of String, Byte)(text.Split(New Char() {","}), New Converter(Of String, Byte)(AddressOf Convert.ToByte)), False))

        REM OLD Method
        'Decrypt = ""

        'Dim Y2, X2, X, Y As Integer
        'If Len(Text) = 2 Then
        '    Decrypt = Text
        '    Exit Function
        'End If
        'Dim Mat() As Double
        'Dim Mat2() As Double
        'Dim Determinant As Short
        'Dim Code() As Short
        'Dim Txt() As Short
        'Dim AmountOfRows As Short

        ''Get the numbers from Matrix and put them in Mat
        'ReDim Preserve Mat(0)
        'Do Until Matrix = ""
        '    ReDim Preserve Mat(UBound(Mat) + 1)

        '    If InStr(Matrix, ",") <> 0 Then
        '        Mat(UBound(Mat)) = CDbl(Left(Matrix, InStr(Matrix, ",") - 1))
        '        Matrix = Right(Matrix, Len(Matrix) - InStr(Matrix, ","))
        '    Else
        '        Mat(UBound(Mat)) = CDbl(Matrix)
        '        Matrix = ""
        '    End If
        'Loop
        'Do Until Int(System.Math.Sqrt(UBound(Mat))) = System.Math.Sqrt(UBound(Mat))
        '    ReDim Preserve Mat(UBound(Mat) + 1)
        '    Mat(UBound(Mat)) = 3
        'Loop
        'AmountOfRows = Int(System.Math.Sqrt(UBound(Mat)))

        ''Find the inverse of mat
        ''The inverse of the matrix [a b]  is [1 / (a * d - c * b) * d          1 / (a * d - c * b) * (b * -1)]
        ''                          [c d]     [1 / (a * d - c * b) * (c * -1)   1 / (a * d - c * b) * a       ]
        ''a * d - c * b is called the determinant

        ''Get the determinant
        'Determinant = (Mat(1) * Mat(4)) - (Mat(2) * Mat(3))

        ''Put the inverse of Mat into Mat2
        'ReDim Mat2(UBound(Mat))
        'For X = 1 To UBound(Mat)
        '    If X = 1 Then
        '        Mat2(X) = (1 / Determinant) * Mat(4)
        '    ElseIf X = 4 Then
        '        Mat2(X) = (1 / Determinant) * Mat(1)
        '    Else
        '        Mat2(X) = (1 / Determinant) * (Mat(X) * -1)
        '    End If
        'Next

        ''Put valuse of Mat2 in Mat
        'For X = 1 To UBound(Mat)
        '    Mat(X) = Mat2(X)
        'Next

        'ReDim Preserve Txt(AmountOfRows)
        'ReDim Preserve Code(AmountOfRows)
        'Do Until Len(Text) < AmountOfRows
        '    'Get values from code
        '    For X = 1 To UBound(Txt)
        '        If Asc(Left(Text, 1)) = 255 Then
        '            Y = 0
        '            Code(X) = Asc(Right(Left(Text, 2), 1))
        '            X2 = 255
        '            Do
        '                X2 = X2 + 1
        '                Y2 = 1
        '                Do
        '                    Y2 = Y2 + 1
        '                    If Int(X2 / Y2) = X2 / Y2 Then Exit Do
        '                    If Y2 * 2 > X2 - 1 Then
        '                        Y = Y + 1
        '                        If Y = Code(X) Then Code(X) = X2 : X2 = 0
        '                        Exit Do
        '                    End If
        '                Loop
        '                If X2 = 0 Then Exit Do
        '            Loop
        '        Else
        '            Code(X) = Asc(Left(Text, 1)) * Asc(Right(Left(Text, 2), 1))
        '        End If
        '        Text = Right(Text, Len(Text) - 2)
        '    Next

        '    'Multiply the matrixes
        '    For X = 1 To UBound(Txt)
        '        Txt(X) = 0
        '        For Y = 1 To AmountOfRows
        '            Txt(X) = Txt(X) + (Mat(((UBound(Mat) / AmountOfRows) * (Y - 1)) + X) * Code(Y))
        '        Next
        '    Next

        '    'Take value from the Txt and make text
        '    For X = 1 To UBound(Txt)
        '        Decrypt = Decrypt & Chr(Txt(X))
        '    Next
        'Loop
        'Decrypt = Decrypt & Text
    End Function
End Module
