Public Class FilterNoFacture
    Inherits FilterInputFieldData

    Public Sub New()
        Me.currencyBox = True
        Me.nbDecimals = 0
        Me.acceptAlpha = False
        Me.acceptNumeric = True
    End Sub

    Protected Overrides Function internalFilter() As FilterResult
        choosePerso()
        Dim noFacture As Integer
        If Integer.TryParse(_CurrentReturn.persoChoice, noFacture) = False Then
            _CurrentReturn.canceling = True
        Else
            Dim choosedBill As New Bill(noFacture)
            If choosedBill.noBillRef <> "" Then
                _CurrentReturn.whereStr = Me.tableDotField & " IN (" & choosedBill.getAllBillsLinked() & ")"
            End If
        End If

        With CType(Me.parent, FilterComposite)
            Dim filterIndex As Integer = .indexOf("FilterPassif")
            If filterIndex <> -1 Then CType(.Item(.indexOf("FilterPassif")), FilterPassive).passiveValue = _CurrentReturn.persoChoice
        End With

        Return New FilterResult(currentReturn, Me.filterOnReportParts, currentReturn.canceling)
    End Function

    Protected Overrides Function internalFilter(ByVal filtered As FilteringElement) As FilterResult
        With CType(filtered, FilteringInputFieldData)
            _CurrentReturn = .currentReturn
            If _CurrentReturn.filtrageTexte = "" Then _CurrentReturn.filtrageTexte = "<tr><td>Filtré par</td><td> : </td><td>" & _CurrentReturn.persoChoice & "</td></tr>"
            Dim startsWithStr As String = ""
            Dim endsWithStr As String = ""
            If startsWith Then startsWithStr = "%"
            If endsWith Then endsWithStr = "%"
            Dim choosedBill As New Bill(Integer.Parse(_CurrentReturn.persoChoice))
            If choosedBill.noBillRef <> "" Then
                _CurrentReturn.whereStr = Me.tableDotField & " IN (" & choosedBill.getAllBillsLinked() & ")"
            Else
                If _CurrentReturn.whereStr = "" Then _CurrentReturn.whereStr = tableDotField & "='" & endsWithStr & _CurrentReturn.persoChoice.Replace("'", "''") & startsWithStr & "'"
            End If

            Try
                With CType(Me.parent, FilterComposite)
                    CType(.Item(.indexOf("FilterPassif")), FilterPassive).passiveValue = _CurrentReturn.persoChoice
                End With
            Catch
            End Try

            Return New FilterResult(_CurrentReturn, Me.filterOnReportParts, False)
        End With
    End Function

End Class
