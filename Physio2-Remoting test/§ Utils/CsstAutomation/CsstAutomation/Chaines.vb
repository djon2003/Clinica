Imports System.Text.RegularExpressions

Module Chaines

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

    Public Function addSlash(ByVal path As String) As String
        If path = "" Then Return ""
        If Not path.Substring(path.Length - 1, 1) = "\" Then Return "\"

        Return ""
    End Function
End Module
