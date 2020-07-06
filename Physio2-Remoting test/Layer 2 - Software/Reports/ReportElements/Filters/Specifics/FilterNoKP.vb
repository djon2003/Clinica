Public Class FilterNoKP
    Inherits BasicFilter
    Private _CurrentReturn As KPSelectorReturn
    Private _All As Boolean = False
    Private _CurrentNoKP As Integer = 0
    Private _CurrentKPName As String = ""

#Region "Properties"
    Public Property currentNoKP() As Integer
        Get
            Return _CurrentNoKP
        End Get
        Set(ByVal value As Integer)
            _CurrentNoKP = value
        End Set
    End Property

    Public Property currentKPName() As String
        Get
            Return _CurrentKPName
        End Get
        Set(ByVal value As String)
            _CurrentKPName = value
        End Set
    End Property

    Public Property all() As Boolean
        Get
            Return _All
        End Get
        Set(ByVal value As Boolean)
            _All = value
        End Set
    End Property

    Public ReadOnly Property currentReturn() As KPSelectorReturn
        Get
            Return _CurrentReturn
        End Get
    End Property
#End Region

    Private Function chooseNoKP(ByVal whereNoKPField As String) As KPSelectorReturn
        Dim myReturn As KPSelectorReturn

        Dim oldKP As Integer = _CurrentNoKP
        Dim myKeyPeople As New keypeopleSearch()
        myKeyPeople.selected = True
        myKeyPeople.MdiParent = Nothing
        myKeyPeople.StartPosition = FormStartPosition.CenterScreen
        myKeyPeople.Visible = False
        myReturn = myKeyPeople.showDialog()
        _CurrentNoKP = myReturn.noKP

        If oldKP <> _CurrentNoKP Then
            myReturn.filtrageTexte = "<tr><td>Personne / organisme clé</td><td> : </td><td>" & myReturn.kpFullName & "</td></tr>"
            myReturn.whereStr = whereNoKPField & "=" & myReturn.noKP
        Else
            If all = False Then
                myReturn.canceling = True
            Else
                myReturn.filtrageTexte = "<tr><td>Personne / organisme clé</td><td> : </td><td>Tous/Toutes</td></tr>"
            End If
        End If

        Return myReturn
    End Function

    Protected Overrides Function internalFilter(ByVal filtered As FilteringElement) As FilterResult
        With CType(filtered, FilteringNoKP)
            _CurrentReturn = .currentReturn
            _CurrentReturn.filtrageTexte = "<tr><td>Personne / organimse clé</td><td> : </td><td>" & _CurrentReturn.kpFullName & "</td></tr>"
            _CurrentReturn.whereStr = tableDotField & "=" & _CurrentReturn.noKP

            Return New FilterResult(_CurrentReturn, Me.filterOnReportParts, False)
        End With
    End Function

    Protected Overrides Function internalFilter() As FilterResult
        Dim myReturn As KPSelectorReturn = Me.chooseNoKP(Me.tableDotField)
        _CurrentReturn = myReturn

        Return New FilterResult(myReturn, Me.filterOnReportParts, myReturn.canceling)
    End Function

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "All"
                    _All = myKey.Value
                Case Else
            End Select
        Next

        MyBase.loadProperties(properties)
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)

    End Sub
End Class
