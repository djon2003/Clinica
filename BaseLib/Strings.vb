Imports System.IO.Compression
Imports System.IO
Imports System.Text
Imports System.Drawing

Public Module Strings

    Public Function generateGUID() As String
        Return System.Guid.NewGuid.ToString()
    End Function

    Public Function testGUID(ByVal guid As String) As Boolean
        Return System.Text.RegularExpressions.Regex.IsMatch(guid, "^[0-9A-Z]{8}\-[0-9A-Z]{4}\-[0-9A-Z]{4}\-[0-9A-Z]{4}\-[0-9A-Z]{12}$", RegularExpressions.RegexOptions.IgnoreCase)
    End Function

    Public Enum SearchType As Integer
        ExactMatch = 0
        InString = 1
        StartsWith = 2
        EndsWith = 3
    End Enum

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

    Public Function searchArray(ByVal myArray As String(), ByVal searchStr As String, ByVal searchMethod As SearchType, Optional ByVal comparingMethod As CompareMethod = CompareMethod.Text) As Integer
        If myArray Is Nothing Then Return -1

        For i As Integer = 0 To myArray.Length - 1
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

    ''' String to comressed
    Public Function compressString(ByVal content_ As [String]) As String
        If String.IsNullOrEmpty(content_) Then
            Return String.Empty
        End If

        Dim memoryStream As MemoryStream = Nothing
        'Dim zip As GZipStream = Nothing
        Dim outStream As MemoryStream = Nothing

        Try
            Dim buffer__1 As Byte() = Encoding.Unicode.GetBytes(content_)
            memoryStream = New MemoryStream()

            Using zip = New GZipStream(memoryStream, CompressionMode.Compress, True)
                zip.Write(buffer__1, 0, buffer__1.Length)
            End Using

            memoryStream.Position = 0
            outStream = New MemoryStream()

            Dim compressed As Byte() = New Byte(memoryStream.Length - 1) {}
            memoryStream.Read(compressed, 0, compressed.Length)

            Dim gzBuffer As Byte() = New Byte(compressed.Length + 3) {}
            Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length)
            Buffer.BlockCopy(BitConverter.GetBytes(buffer__1.Length), 0, gzBuffer, 0, 4)

            Return Convert.ToBase64String(gzBuffer)
        Finally
            '#Region "Release objects"


            If memoryStream IsNot Nothing Then
                memoryStream.Dispose()
                memoryStream = Nothing
            End If

            If outStream IsNot Nothing Then
                outStream.Dispose()
                outStream = Nothing

                '#End Region
            End If
        End Try
    End Function

    ''' String to decompress
    Public Function decompressString(ByVal content_ As [String]) As String
        If String.IsNullOrEmpty(content_) Then
            Return String.Empty
        End If

        Dim zip As GZipStream = Nothing
        Dim memoryStream As MemoryStream = Nothing

        Try
            Dim gzBuffer As Byte() = Convert.FromBase64String(content_)
            memoryStream = New MemoryStream()

            Dim contentLength As Integer = BitConverter.ToInt32(gzBuffer, 0)
            memoryStream.Write(gzBuffer, 4, gzBuffer.Length - 4)

            Dim buffer As Byte() = New Byte(contentLength - 1) {}

            memoryStream.Position = 0
            zip = New GZipStream(memoryStream, CompressionMode.Decompress)

            zip.Read(buffer, 0, buffer.Length)

            Return Encoding.Unicode.GetString(buffer)
            ' Catch the format exception, Some of the old data may be in
            '             * decompressed mode So in this case only return the content 
            '             * passed as paramert.
            '             

        Catch generatedExceptionName As FormatException
            Return content_
        Finally
            '#Region "Release Objects"

            If zip IsNot Nothing Then
                zip.Dispose()
                zip = Nothing
            End If

            If memoryStream IsNot Nothing Then
                memoryStream.Dispose()
                memoryStream = Nothing

                '#End Region
            End If
        End Try
    End Function
End Module
