Imports System.Text.RegularExpressions

Module Chaines

    Public Enum SearchType As Integer
        ExactMatch = 0
        InString = 1
        StartsWith = 2
        EndsWith = 3
    End Enum

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

    Public Function convertFrom(ByVal value As String) As String
        Dim start As Integer = value.IndexOf("=?")
        If start <> -1 Then
            start += 2 'Length of searched text (=?) w/o ()
            Dim end1 As Integer = value.ToLower.IndexOf("?b?")
            Dim end2 As Integer = value.ToLower.IndexOf("?q?")
            Dim ending As Integer = -1
            If end1 <> -1 Then
                ending = end1
            ElseIf end2 <> -1 Then
                ending = end2
            Else
                Return value
            End If

            Dim encoding As String = value.Substring(start, ending - start)
            If encoding.ToLower().StartsWith("cp") Then
                Dim newEncoding As String = System.Text.Encoding.GetEncoding(Integer.Parse(encoding.Substring(2))).BodyName
                value = value.Replace(encoding, newEncoding)
                encoding = newEncoding
            End If

            Return convertFrom(value, encoding)
        Else
            Return value
        End If
    End Function

    Public Function convertFrom(ByVal value As String, ByVal encoding As String) As String
        encoding = encoding.ToLower()
        Dim strIso1 As String = "=?" & encoding & "?b?"
        Dim strIso2 As String = "=?" & encoding & "?q?"
        Dim strEnd As String = "?="
        Dim lastIso As String = String.Empty
        value = value.Replace("?= =?", "?==?")

        Dim indexStrIso1 As Integer = value.ToLower.IndexOf(strIso1)
        Dim indexStrIso2 As Integer = value.ToLower.IndexOf(strIso2)
        Dim indexStrEnd As Integer = value.ToLower.IndexOf(strEnd)
        While indexStrIso1 <> -1 Or indexStrIso2 <> -1 Or indexStrEnd <> -1
            If indexStrIso1 <> -1 Then
                lastIso = "?b?"
                Dim afterValue As String = value
                Dim beforeValue As String = value
                Dim stringBytes() As Byte
                beforeValue = value.Substring(0, indexStrIso1)
                value = value.Substring(indexStrIso1 + strIso1.Length)
                afterValue = value.Substring(value.IndexOf(strEnd) + strEnd.Length)

                value = value.Substring(0, value.IndexOf(strEnd))
                stringBytes = convertFromBase64(value)

                value = beforeValue & System.Text.Encoding.GetEncoding(encoding).GetString(stringBytes) & afterValue
            ElseIf indexStrIso2 <> -1 Then
                lastIso = "?q?"
                Dim afterValue As String = value
                Dim beforeValue As String = value
                beforeValue = value.Substring(0, indexStrIso2)
                value = value.Substring(indexStrIso2 + strIso2.Length)
                afterValue = value.Substring(value.IndexOf(strEnd) + strEnd.Length)

                value = value.Substring(0, value.IndexOf(strEnd))

                charset = encoding
                Dim hexRegex As Regex = New Regex("(\=([0-9A-F][0-9A-F]))+", RegexOptions.IgnoreCase)
                value = hexRegex.Replace(value, New MatchEvaluator(AddressOf hexDecoderEvaluator))

                value = System.Text.RegularExpressions.Regex.Replace(value.Trim, "\n", "").Replace(vbLf, "").Replace(vbCr, "").Replace(Chr(9), "")
                value = value.Replace("_", " ")

                value = beforeValue & value & afterValue

            End If

            If indexStrIso1 = -1 AndAlso indexStrIso2 = -1 Then
                'Partial detection (bug in string received)
                Dim partialIndex As Integer = value.ToLower.IndexOf(lastIso)
                If lastIso = String.Empty OrElse partialIndex = -1 OrElse partialIndex > indexStrEnd Then
                    indexStrEnd = -1
                Else
                    value = value.Substring(partialIndex + lastIso.Length)
                    Dim afterValue As String = value.Substring(value.IndexOf(strEnd) + strEnd.Length)
                    value = value.Substring(0, value.IndexOf(strEnd))

                    If lastIso = "?b?" Then
                        Dim stringBytes() As Byte = convertFromBase64(value)
                        value = System.Text.Encoding.GetEncoding(encoding).GetString(stringBytes) & afterValue
                    Else
                        Dim hexRegex As Regex = New Regex("(\=([0-9A-F][0-9A-F]))+", RegexOptions.IgnoreCase)
                        value = hexRegex.Replace(value, New MatchEvaluator(AddressOf hexDecoderEvaluator))

                        value = System.Text.RegularExpressions.Regex.Replace(value.Trim, "\n", "").Replace(vbLf, "").Replace(vbCr, "").Replace(Chr(9), "")
                        value = value.Replace("_", " ")

                        value = value & afterValue
                    End If

                    indexStrEnd = value.ToLower.IndexOf(strEnd)
                End If
            Else
                indexStrIso1 = value.ToLower.IndexOf(strIso1)
                indexStrIso2 = value.ToLower.IndexOf(strIso2)
                indexStrEnd = value.ToLower.IndexOf(strEnd)
            End If
        End While

        Return value
    End Function

    Public Function convertFromHexadecimal(ByVal value As String, ByVal curCharset As String, Optional ByVal starterChar As Char = "=") As String
        If curCharset Is Nothing Then Return value

        charset = curCharset
        value = value.Replace(starterChar & vbCrLf, "")

        Dim hexRegex As Regex = New Regex("(\" & starterChar & "([0-9A-F][0-9A-F]))+")
        value = hexRegex.Replace(value, New MatchEvaluator(AddressOf hexDecoderEvaluator))

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
        Catch 'REM Exception not handle
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

    Public Function transformdiffDateInText(ByVal date1 As Date, ByVal date2 As Date, Optional ByRef diffDate As Integer = 0, Optional ByRef nbWeek As Integer = 0, Optional ByRef nbDay As Integer = 0) As String
        Dim strNbWeek As String
        Dim dureeStr As String = "# semaine et # jour"

        If date1Infdate2(date1, date2.AddMonths(1)) = True Then
            Try
                diffDate = date1.Subtract(date2).Days
            Catch 'REM Exception not handle
                Return ""
            End Try
            strNbWeek = CStr((diffDate / 7)).Replace(".", ",")
            If strNbWeek.IndexOf(",") < 0 Then nbWeek = strNbWeek Else nbWeek = strNbWeek.Substring(0, strNbWeek.IndexOf(",")) : nbDay = diffDate - nbWeek * 7

            If nbWeek = 0 Then
                dureeStr = Microsoft.VisualBasic.Right(dureeStr, "6")
            ElseIf nbDay = 0 Then
                dureeStr = dureeStr.Substring(0, "9")
            End If

            dureeStr = dureeStr.Replace("# semaine", nbWeek & " semaine")
            dureeStr = dureeStr.Replace("# jour", nbDay & " jour")
            If nbWeek > 1 Then dureeStr = dureeStr.Replace("ne", "nes")
            If nbDay > 1 Then dureeStr = dureeStr.Replace("r", "rs")
            Return dureeStr
        Else
            Return "1 mois et plus"
        End If
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

    Public Function analyseExpression(ByVal fields() As String, ByVal myExpression As String) As AnalysedExpression
        Dim spaceDephase As Integer = 0
        Dim returning As New AnalysedExpression
        Dim MyFirstExpression, LastLogicOperator, LastOperator, FieldName, SField(), Value, selTables() As String
        Dim LastFieldPos, LastOperatorPos, LastOperatorPosRel, SA, LenValue, PreviousCursorPos, CursorPos, RelativePos, LeftParentheses, n, AndPos, OrPos, etape As Integer
        Dim noChange As Boolean
        Dim spaceRemoval() As Char = {" "}
        LastLogicOperator = "" : LastOperator = "" : FieldName = ""

        MyFirstExpression = myExpression
        myExpression = myExpression.Replace("=<", "<=").Replace("=>", ">=")

        If myExpression.Length = 0 Then MessageBox.Show("Veuillez entrer une expression", "Expression invalide") : Return returning
        If myExpression.StartsWith("(") = False Then MessageBox.Show("Votre expression doit débuter par une parenthèse de gauche ou un nom de champ (qui inclut une parenthèse avant et après).", "Expression invalide") : returning.ErrorPos = 0 : Return returning
        etape = 1
        ReDim selTables(0)
        selTables(0) = String.Empty

        Do
            PreviousCursorPos = CursorPos
            If etape = 1 Then
                If (CursorPos + 1) < myExpression.Length Then
                    If myExpression.Substring(CursorPos).StartsWith("(") And isAlpha(myExpression.Substring(CursorPos + 1, 1)) = True Then
                        n = InStr(CursorPos + 1, myExpression, ")")
                        If n = 0 Then
                            MessageBox.Show("Il n'existe pas de parenthèse de droite ')' après le " & (CursorPos + 1) & " caractères", "Expression invalide")
                            returning.ErrorPos = (RelativePos + 1)
                            returning.errorLength = (myExpression.Length - RelativePos - 1)
                            Return returning
                        End If

                        FieldName = myExpression.Substring(CursorPos + 1, n - CursorPos - 2)
                        If isAlpha(FieldName, "§'§ ") = False Then
                            MessageBox.Show("Le champ '" & FieldName & "' ne contient pas uniquement des caractères alphabétiques", "Expression invalide")
                            returning.ErrorPos = (RelativePos + 1)
                            returning.errorLength = FieldName.Length
                            Return returning
                        End If

                        SA = searchArray(fields, FieldName & "§", SearchType.StartsWith)
                        If SA < 0 Then
                            MessageBox.Show("Le champ '" & FieldName & "' n'existe pas. Veuillez vous assurer de son ortographe", "Expression invalide")
                            returning.ErrorPos = (RelativePos + 1)
                            returning.errorLength = FieldName.Length
                            Return returning
                        End If

                        SField = Split(fields(SA), "§")
                        myExpression = firstPart(myExpression, CursorPos) & Replace(myExpression, FieldName, SField(1), CursorPos + 1, 1, CompareMethod.Text)
                        LastFieldPos = CursorPos
                        CursorPos += 2 + SField(1).Length
                        RelativePos += 2 + FieldName.Length

                        If selTables(0) = String.Empty Then
                            selTables(0) = SField(2)
                        ElseIf searchArray(selTables, SField(2), SearchType.ExactMatch) < 0 Then
                            ReDim Preserve selTables(selTables.Length) : selTables(selTables.Length - 1) = SField(2)
                        End If

                        etape = 2
                        spaceDephase = 0
                        GoTo NextLoop
                    End If
                End If
            End If
            If myExpression.Substring(CursorPos, 1) = " " Then
                CursorPos += 1
                RelativePos += 1
                spaceDephase += 1
                GoTo NextLoop
            End If
            If etape = 2 Then
                With myExpression.Substring(CursorPos)
                    If (.StartsWith("<") Or .StartsWith(">") Or .StartsWith("<>") Or .StartsWith("=") Or .StartsWith(">=") Or .StartsWith("<=")) Then
                        LastOperatorPosRel = RelativePos
                        LastOperatorPos = CursorPos
                        If .StartsWith("<>") Then
                            CursorPos += 2
                            RelativePos += 2
                            LastOperator = "<>"
                        ElseIf .StartsWith("<=") Then
                            CursorPos += 2
                            RelativePos += 2
                            LastOperator = "<="
                        ElseIf .StartsWith(">=") Then
                            CursorPos += 2
                            RelativePos += 2
                            LastOperator = ">="
                        ElseIf .StartsWith(">") Then
                            CursorPos += 1
                            RelativePos += 1
                            LastOperator = ">"
                        ElseIf .StartsWith("<") Then
                            CursorPos += 1
                            RelativePos += 1
                            LastOperator = "<"
                        ElseIf .StartsWith("=") Then
                            CursorPos += 1
                            RelativePos += 1
                            LastOperator = "="
                        End If

                        etape = 3
                        GoTo NextLoop
                    End If
                End With
            End If
            If etape = 3 Then
                Dim lastLogicOperatorPos As Integer = 0
                AndPos = InStr(CursorPos + 1, myExpression, " AND ", CompareMethod.Text)
                OrPos = InStr(CursorPos + 1, myExpression, " OR ", CompareMethod.Text)
                If AndPos = 0 And OrPos = 0 Then
                    n = myExpression.Length + 1
                    LastLogicOperator = ""
                ElseIf AndPos > 0 And ((AndPos < OrPos And OrPos > 0) Or OrPos = 0) Then
                    LastLogicOperator = "AND"
                    n = AndPos + 1
                    lastLogicOperatorPos = AndPos
                ElseIf OrPos > 0 Then
                    LastLogicOperator = "OR"
                    n = OrPos + 1
                    lastLogicOperatorPos = OrPos
                End If

                Value = myExpression.Substring(CursorPos, n - CursorPos - 1)
                LenValue = Value.Length : noChange = False
                Value = Value.TrimStart(spaceRemoval)
                Value = Value.TrimEnd(spaceRemoval)

                '                SpaceDephase = CursorPos - LastOperatorPos - LastOperator.Length

                Do
                    If Value.StartsWith("(") And Value.EndsWith(")") Then
                        Value = Value.Substring(1, Value.Length - 2)
                        Value = Value.TrimStart(spaceRemoval)
                        Value = Value.TrimEnd(spaceRemoval)
                    Else
                        noChange = True
                    End If
                Loop Until noChange = True Or InStr(Value, "(") = 0

                noChange = False
                Do Until Value.EndsWith(")") = False Or noChange = True
                    If LeftParentheses > 0 Then
                        LeftParentheses -= 1
                        Value = Value.Substring(0, Value.Length - 1)
                    Else
                        noChange = True
                    End If
                Loop

                Select Case SField(3)
                    Case "'"
                        If LastOperator <> "=" And LastOperator <> "<>" Then
                            MessageBox.Show("L'opérateur '" & LastOperator & "' utilisé avec le champ '" & SField(0) & "' n'est pas valide." & vbCrLf & "Veuillez utiliser soit '=' ou '<>'", "Expression invalide")
                            returning.ErrorPos = LastOperatorPosRel
                            returning.errorLength = LastOperator.Length
                            Return returning
                        End If

                        If LastOperator = "=" Then
                            myExpression = firstPart(myExpression, LastOperatorPos) & Replace(myExpression, LastOperator, "LIKE", LastOperatorPos + 1, 1)
                            CursorPos += 3
                        Else
                            myExpression = firstPart(myExpression, LastOperatorPos) & Replace(myExpression, LastOperator, "NOT LIKE", LastOperatorPos + 1, 1)
                            CursorPos += 6
                        End If
                        Dim lenBeforeAdjust As Integer = Value.Length
                        Dim adjustedValue As String = Value.Replace("'", "''")
                        myExpression = firstPart(myExpression, CursorPos) & Replace(myExpression, Value, "'%" & adjustedValue & "%'", CursorPos + 1, 1)
                        CursorPos += LenValue + 4 + (adjustedValue.Length - lenBeforeAdjust)

                    Case "#"
                        If isValidTinyDate(Value) = False Then
                            MessageBox.Show("La date '" & Value & "' utilisée avec le champ '" & SField(0) & "' n'est pas valide. Veuillez utiliser un format Année/Mois/Jour où le mois et le jour sont optionnels.", "Expression invalide")
                            returning.ErrorPos = MyFirstExpression.IndexOf(Value, RelativePos)
                            returning.errorLength = Value.Length
                            Return returning
                        End If

                        If Value.Length = 4 Then
                            Select Case LastOperator
                                Case "="
                                    myExpression = firstPart(myExpression, LastFieldPos) & "((" & SField(1) & ")<'" & (Value + 1) & "/01/01' AND (" & SField(1) & ")>=" & Replace(myExpression, Value, "'" & Value & "/01/01')", CursorPos + 1, 1)
                                Case "<>"
                                    myExpression = firstPart(myExpression, LastFieldPos) & "((" & SField(1) & ")>='" & (Value + 1) & "/01/01' OR (" & SField(1) & ")<" & Replace(myExpression, Value, "'" & Value & "/01/01')", CursorPos + 1, 1)
                                Case ">"
                                    myExpression = firstPart(myExpression, LastFieldPos) & "((" & SField(1) & ")>=" & Replace(myExpression, Value, "'" & (Value + 1) & "/01/01')", CursorPos + 1, 1)
                                Case "<"
                                    myExpression = firstPart(myExpression, LastFieldPos) & "((" & SField(1) & ")<" & Replace(myExpression, Value, "'" & Value & "/01/01')", CursorPos + 1, 1)
                                Case "<="
                                    myExpression = firstPart(myExpression, LastFieldPos) & "((" & SField(1) & ")<" & Replace(myExpression, Value, "'" & (Value + 1) & "/01/01')", CursorPos + 1, 1)
                                Case ">="
                                    myExpression = firstPart(myExpression, LastFieldPos) & "((" & SField(1) & ")>=" & Replace(myExpression, Value, "'" & Value & "/01/01')", CursorPos + 1, 1)
                            End Select

                            If LastLogicOperator <> "" Then
                                CursorPos = InStr(CursorPos + 1, myExpression, LastLogicOperator, CompareMethod.Text) - 1
                                If LastLogicOperator = "AND" And LastOperator = "=" Then CursorPos = InStr(CursorPos + 4, myExpression, LastLogicOperator, CompareMethod.Text) - 1
                                If LastLogicOperator = "OR" And LastOperator = "<>" Then CursorPos = InStr(CursorPos + 2, myExpression, LastLogicOperator, CompareMethod.Text) - 1
                            Else
                                CursorPos = myExpression.Length
                            End If

                        ElseIf Value.LastIndexOf("/") <> Value.IndexOf("/") Then
                            myExpression = firstPart(myExpression, CursorPos) & Replace(myExpression, Value, "'" & Value & "'", CursorPos + 1, 1)
                            CursorPos += LenValue + 2
                        Else
                            Dim sDate() As String = Split(Value, "/")
                            Dim TheMonth, TheNextMonth, theNextYear As String
                            TheMonth = addZeros(sDate(1), 2)
                            theNextYear = sDate(0)
                            If (TheMonth + 1) = 13 Then
                                TheNextMonth = "01"
                                theNextYear += 1
                            Else
                                TheNextMonth = addZeros(TheMonth + 1, 2)
                            End If
                            Select Case LastOperator
                                Case "="
                                    myExpression = firstPart(myExpression, LastFieldPos) & "((" & SField(1) & ")<'" & theNextYear & "/" & TheNextMonth & "/01' AND (" & SField(1) & ")>=" & Replace(myExpression, Value, "'" & sDate(0) & "/" & TheMonth & "/01')", CursorPos + 1, 1)
                                Case "<>"
                                    myExpression = firstPart(myExpression, LastFieldPos) & "((" & SField(1) & ")>='" & theNextYear & "/" & TheNextMonth & "/01' OR (" & SField(1) & ")<" & Replace(myExpression, Value, "'" & sDate(0) & "/" & TheMonth & "/01')", CursorPos + 1, 1)
                                Case ">"
                                    myExpression = firstPart(myExpression, LastFieldPos) & "((" & SField(1) & ")>=" & Replace(myExpression, Value, "'" & theNextYear & "/" & TheNextMonth & "/01')", CursorPos + 1, 1)
                                Case "<"
                                    myExpression = firstPart(myExpression, LastFieldPos) & "((" & SField(1) & ")<" & Replace(myExpression, Value, "'" & sDate(0) & "/" & TheMonth & "/01')", CursorPos + 1, 1)
                                Case "<="
                                    myExpression = firstPart(myExpression, LastFieldPos) & "((" & SField(1) & ")<" & Replace(myExpression, Value, "'" & theNextYear & "/" & TheNextMonth & "/01')", CursorPos + 1, 1)
                                Case ">="
                                    myExpression = firstPart(myExpression, LastFieldPos) & "((" & SField(1) & ")>=" & Replace(myExpression, Value, "'" & sDate(0) & "/" & TheMonth & "/01')", CursorPos + 1, 1)
                            End Select

                            If LastLogicOperator <> "" Then
                                CursorPos = InStr(CursorPos + 1, myExpression, LastLogicOperator, CompareMethod.Text) - 1
                                If LastLogicOperator = "AND" And LastOperator = "=" Then CursorPos = InStr(CursorPos + 4, myExpression, LastLogicOperator, CompareMethod.Text) - 1
                                If LastLogicOperator = "OR" And LastOperator = "<>" Then CursorPos = InStr(CursorPos + 2, myExpression, LastLogicOperator, CompareMethod.Text) - 1
                            Else
                                CursorPos = myExpression.Length
                            End If
                        End If

                    Case "| ", "|'"
                        If LastOperator <> "=" And LastOperator <> "<>" Then
                            MessageBox.Show("L'opérateur '" & LastOperator & "' utilisé avec le champ '" & SField(0) & "' n'est pas valide." & vbCrLf & "Veuillez utiliser soit '=' ou '<>'", "Expression invalide")
                            returning.ErrorPos = LastOperatorPosRel
                            returning.errorLength = LastOperator.Length
                            Return returning
                        End If
                        If SField(3) = "| " AndAlso IsNumeric(Value) = False Then
                            MessageBox.Show("Le nombre '" & Value & "' utilisé avec le champ '" & SField(0) & "' n'est pas valide.", "Expression invalide")
                            returning.ErrorPos = MyFirstExpression.IndexOf(Value, RelativePos)
                            returning.errorLength = Value.Length
                            Return returning
                        End If

                        Dim comma As String = If(SField(3) = "| ", String.Empty, "'")

                        If LastOperator = "=" Then
                            myExpression = firstPart(myExpression, LastFieldPos) & Replace(myExpression, Value, comma & Value & comma & " IN " & "(" & SField(1) & ")", CursorPos + 1, 1)
                            CursorPos += 3 + comma.Length * 2
                        Else
                            myExpression = firstPart(myExpression, LastFieldPos) & Replace(myExpression, Value, comma & Value & comma & " NOT IN " & "(" & SField(1) & ")", CursorPos + 1, 1)
                            CursorPos += 6 + comma.Length * 2
                        End If

                        CursorPos += LenValue - IIf(LastLogicOperator <> "", spaceDephase, 0)
                        'SpaceDephase = 0

                    Case Else
                        If Integer.TryParse(Value, 0) = False Then
                            MessageBox.Show("Le nombre '" & Value & "' utilisé avec le champ '" & SField(0) & "' n'est pas valide.", "Expression invalide")
                            returning.ErrorPos = MyFirstExpression.IndexOf(Value, RelativePos)
                            returning.errorLength = Value.Length
                            Return returning
                        End If
                        CursorPos += LenValue
                End Select

                RelativePos += LenValue

                etape = 4
                GoTo NextLoop
            End If
            If myExpression.Substring(CursorPos, 1) = ")" Then
                MessageBox.Show("La parenthèse de droite au caractère #" & (RelativePos + 1) & " n'est pas valide", "Expression invalide")
                returning.ErrorPos = RelativePos : returning.errorLength = 1 : Return returning
                GoTo NextLoop
            End If
            If etape = 4 Then
                Dim leftPart As String = UCase(myExpression.Substring(CursorPos))
                If leftPart.Length > 0 Then
                    If leftPart.StartsWith(UCase(LastLogicOperator)) Then
                        CursorPos += LastLogicOperator.Length
                        RelativePos += LastLogicOperator.Length
                        etape = 1
                        GoTo NextLoop
                    End If
                End If
            End If
            If myExpression.Substring(CursorPos, 1) = "(" Then
                If etape = 1 Then
                    LeftParentheses += 1
                    CursorPos += 1
                    RelativePos += 1
                Else
                    MessageBox.Show("La parenthèse de gauche au caractère #" & (RelativePos + 1) & " n'est pas valide", "Expression invalide")
                    returning.ErrorPos = RelativePos : returning.errorLength = 1 : Return returning
                End If

                GoTo NextLoop
            End If
            'Change for If Etape=2 below
            'If Etape = 2 And MyExpression.Substring(CursorPos, 1) <> "(" And MyExpression.Substring(CursorPos, 1) <> "=" And MyExpression.Substring(CursorPos, 1) <> ">" And MyExpression.Substring(CursorPos, 1) <> "<" And MyExpression.Substring(CursorPos, 1) <> ")" And MyExpression.Substring(CursorPos, 1) <> " " Then
            '    MessageBox.Show("Le champ '" & SField(0) & "' doit être couplé à une valeur à l'aide d'un opérateur." & vbCrLf & "Veuillez entrer soit '=' ou '<>' ou '>' ou '<' ou '>=' ou '<='.",  "Expression invalide") : Returning.ErrorPos = RelativePos : Return Returning
            'End If
NextLoop:
            If PreviousCursorPos = CursorPos Then Exit Do
        Loop Until CursorPos >= myExpression.Length

        If etape = 1 Then returning.ErrorPos = RelativePos : MessageBox.Show("Il doit y avoir un champ après l'opérateur logique '" & LastLogicOperator & "'.", "Expression invalide") : Return returning
        If etape = 2 Then MessageBox.Show("Le champ '" & SField(0) & "' doit être couplé à une valeur à l'aide d'un opérateur." & vbCrLf & "Veuillez entrer soit '=' ou '<>' ou '>' ou '<' ou '>=' ou '<='.", "Expression invalide") : returning.ErrorPos = RelativePos : Return returning
        If etape = 3 Then MessageBox.Show("Veuillez entrer une valeur pour la champ '" & FieldName & "'.", "Expression invalide") : returning.ErrorPos = RelativePos : Return returning
        If LeftParentheses > 0 Then MessageBox.Show("Il existe trop de parenthèse de gauche ou une parenthèse de droite est manquante dans votre expression.", "Expression invalide") : Return returning

        returning.conditions = myExpression
        returning.selTables = selTables
        Return returning
    End Function

    Public Function convertirMontantEnMots(ByVal chiffre As Double, Optional ByVal decimalsInLetter As Boolean = False) As String
        If (Math.Abs(chiffre) / Math.Floor(Math.Abs(chiffre))) > 1 Then
            'Si le nombre contient des décimales
            Dim decimaleNumber As Double
            Dim number As Double = Math.Floor(Math.Abs(chiffre))

            decimaleNumber = CInt((chiffre - number) * 10 ^ (addZeros(chiffre.ToString, number.ToString.Length + 3, False).Length - number.ToString.Length - 1))
            Dim a As Double = chiffre.ToString.Length - number.ToString.Length - 1
            If decimalsInLetter Then Return subConvertirMontantEnMots(number) & "et " & subConvertirMontantEnMots(decimaleNumber) & "cent(s)"

            Return IIf(chiffre < 0, "moins ", "") & subConvertirMontantEnMots(number) & "et " & decimaleNumber & " cent(s)"
        Else
            Return IIf(chiffre < 0, "moins ", "") & subConvertirMontantEnMots(Math.Abs(chiffre))
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

    Public Function forceManaging(ByRef myValue As String, ByVal currencyBox As Boolean, ByVal acceptedText As String, ByVal acceptAlpha As Boolean, ByVal acceptNumeric As Boolean, ByVal onlyAlphabet As Boolean, ByVal refuseAccents As Boolean, Optional ByVal acceptedChars As String = "", Optional ByVal refusedChars As String = "", Optional ByVal matchExp As String = "", Optional ByVal wordCapital As Boolean = False, Optional ByVal allWords As Boolean = False, Optional ByVal allCapital As Boolean = False, Optional ByVal allLower As Boolean = False, Optional ByVal nbDecimals As Short = 0, Optional ByVal acceptNegative As Boolean = False, Optional ByRef lastSelection As Integer = -1, Optional ByVal blockOnminimum As Boolean = False, Optional ByVal minimum As Double = 0, Optional ByVal blockOnmaximum As Boolean = False, Optional ByVal maximum As Double = 0, Optional ByVal cb_AcceptLeftZeros As Boolean = False, Optional ByVal blockDoubleSpace As Boolean = True) As Boolean
        'Dim tmpSB As New System.Text.StringBuilder(MyValue)
        'Dim managed As Boolean = ForceManaging(tmpSB, CurrencyBox, AcceptedText, AcceptAlpha, AcceptNumeric, OnlyAlphabet, RefuseAccents, AcceptedChars, RefusedChars, MatchExp, WordCapital, AllWords, AllCapital, AllLower, NbDecimals, AcceptNegative, LastSelection)
        'If managed Then MyValue = tmpSB.ToString

        'Return managed
        'Dim tmpTimer As Long = Date.Now.Ticks

        Dim i As Integer
        Dim myValue2 As String
        Dim isNegative As Boolean
        If myValue Is Nothing Then myValue = ""

        If myValue = "" And currencyBox = True Then myValue = "0" : lastSelection = 2
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
        If Not currencyBox AndAlso blockDoubleSpace Then
            Dim oldValue As String = myValue & "1"
            While oldValue <> myValue
                oldValue = myValue
                myValue = myValue.Replace("  ", " ")
                lastSelection -= oldValue.Length - myValue.Length
            End While
        End If

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

            If cb_AcceptLeftZeros = False AndAlso myValue.Length > 1 Then
                If Not myValue.Substring(0, 2) = "0," Then
                    Do While myValue <> "" AndAlso myValue.Substring(0, 1) = "0"
                        myValue = myValue.Substring(1)
                    Loop
                    If myValue = "" Then myValue = "0"
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

            Dim testingValue As Double = 0
            If myValue.EndsWith(",") Then testingValue = myValue.Substring(0, myValue.Length - 1) Else testingValue = myValue
            If blockOnminimum AndAlso testingValue < minimum Then myValue = minimum
            If blockOnmaximum AndAlso testingValue > maximum Then myValue = maximum
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
                    n += 1
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

    Public Function searchArray(ByVal myArray As String(), ByVal searchStr As String, ByVal searchMethod As SearchType, Optional ByVal comparingMethod As CompareMethod = CompareMethod.Text) As Integer
        If myArray Is Nothing Then Return -1

        For i As Integer = 0 To myArray.Count - 1
            If myArray(i) Is Nothing Then Continue For

            If searchMethod = SearchType.StartsWith Then
                If CStr(myArray(i)).StartsWith(searchStr) Then Return i
            ElseIf searchMethod = SearchType.EndsWith Then
                If CStr(myArray(i)).EndsWith(searchStr) Then Return i
            ElseIf searchMethod = SearchType.ExactMatch Then
                If CStr(myArray(i)) = searchStr Then Return i
            Else
                If InStr(CStr(myArray(i)), searchStr, comparingMethod) > 0 Then Return i
            End If
        Next i

        Return -1
    End Function

    Public Function searchArray(ByVal myArray As ArrayList, ByVal searchStr As String, ByVal searchMethod As SearchType, Optional ByVal comparingMethod As CompareMethod = CompareMethod.Text) As Integer
        If myArray Is Nothing Then Return -1

        Return searchArray(myArray.ToArray(), searchStr, searchMethod, comparingMethod)
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
            Catch 'REM Exception not handle
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
        Return annee Mod 4 = 0 AndAlso (annee Mod 100 <> 0 OrElse annee Mod 400 = 0)
    End Function

    Public Function date1Infdate2(ByVal date1 As Date, ByVal date2 As Date, Optional ByVal egale As Boolean = False) As Boolean
        Dim passer As Boolean
        Dim Y1, y2 As Short
        Dim D2, M2, D1, m1 As Byte

        D2 = CByte(date2.Day)
        M2 = CByte(date2.Month)
        y2 = CShort(date2.Year)
        D1 = CByte(date1.Day)
        m1 = CByte(date1.Month)
        Y1 = CShort(date1.Year)
        passer = True

        If Y1 < y2 Then
            passer = False
        ElseIf Y1 = y2 Then
            If m1 < M2 Then
                passer = False
            ElseIf m1 = M2 Then
                If egale = False Then
                    If D1 < D2 Then passer = False
                Else
                    If D1 <= D2 Then passer = False
                End If
            End If
        End If

        If passer = False Then
            Return True
        Else
            Return False
        End If
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
    End Function

    Public Function decrypt(ByVal text As String, ByVal key As String) As String
        If text = "" Then Return ""

        Dim crypto As New System.Security.Cryptography.RSACryptoServiceProvider()
        crypto.ImportParameters(getPasswordkeyParams(key))

        Return System.Text.UTF7Encoding.UTF7.GetString(crypto.Decrypt(Array.ConvertAll(Of String, Byte)(text.Split(New Char() {","}), New Converter(Of String, Byte)(AddressOf Convert.ToByte)), False))
    End Function
End Module
