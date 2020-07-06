Public Class FilterRefererType
    Inherits BasicFilter

    Private _All As Boolean
    Private _CurrentReturn As RefererSelectorReturn

#Region "Properties"
    Public Property all() As Boolean
        Get
            Return _All
        End Get
        Set(ByVal value As Boolean)
            _All = value
        End Set
    End Property

    Public ReadOnly Property currentReturn() As RefererSelectorReturn
        Get
            Return _CurrentReturn
        End Get
    End Property
#End Region

    Private Function chooseRefererType(ByVal whereDateField As String) As RefererSelectorReturn
        Dim myReturn As New RefererSelectorReturn
        Dim myRefChoosed As String = ""
        myReturn.canceling = True
        If whereDateField = "" Then Return myReturn

        Dim myMultiChoice As New multichoice
        Dim sRefsLine() As String = DBLinker.getInstance.readOneDBField("WITH refs AS (SELECT     CASE WHEN NomReferent LIKE 'KP%' THEN 'Personne / organisme clé(e)' WHEN NomReferent LIKE 'COMPTE%' THEN 'Client' WHEN NomReferent LIKE                        'LIST%' THEN SUBSTRING(NomReferent, 6, LEN(NomReferent) - 5) WHEN NomReferent LIKE 'AUTRE%' THEN SUBSTRING(NomReferent, 7, LEN(NomReferent) - 6) END AS [Type de référent] FROM         InfoClients WHERE     (NomReferent <> N'') AND                       (NomReferent IS NOT NULL) )  SELECT [Type de référent] FROM refs GROUP BY [Type de référent] ORDER BY [Type de référent]")
        If sRefsLine Is Nothing OrElse sRefsLine.Length = 0 Then Return myReturn

        Dim refsLine As String = String.Join("§", sRefsLine)
        If all Then refsLine = "* Tous *§" & refsLine : myRefChoosed = "* Tous *"

        myRefChoosed = myMultiChoice.GetChoice("Sélectionner un type de référent", refsLine, , "§", , myRefChoosed)
        If myRefChoosed = "" Or myRefChoosed.StartsWith("ERROR") Then Return myReturn
        myReturn.canceling = False
        If myRefChoosed.StartsWith("* Tous") Then
            myReturn.filtrageTexte = "<tr><td>Type de référent</td><td> : </td><td>Tous</td></tr>"
            Return myReturn
        End If

        myReturn.refererName = myRefChoosed
        myReturn.filtrageTexte = "<tr><td>Type de référent</td><td> : </td><td>" & myRefChoosed & "</td></tr>"
        myReturn.whereStr = "(" & whereDateField & " LIKE '%" & myRefChoosed & "')"

        Return myReturn
    End Function

    Protected Overrides Function internalFilter() As FilterResult
        Dim myReferer As RefererSelectorReturn = Me.chooseRefererType(Me.tableDotField)
        _CurrentReturn = myReferer
        Return New FilterResult(myReferer, Me.filterOnReportParts, myReferer.canceling)
    End Function

    Protected Overrides Function internalFilter(ByVal filtered As FilteringElement) As FilterResult
        With CType(filtered, FilteringRefererType)
            _CurrentReturn = .currentReturn
            _CurrentReturn.filtrageTexte = "<tr><td>Type de référent</td><td> : </td><td>" & _CurrentReturn.refererName & "</td></tr>"
            _CurrentReturn.whereStr = "(" & Me.tableDotField & " LIKE '%" & _CurrentReturn.refererName & "')"
            Return New FilterResult(_CurrentReturn, Me.filterOnReportParts, False)
        End With
    End Function

    Public Overrides Sub loadProperties(ByVal properties As System.Collections.Hashtable)
        Dim myKey As DictionaryEntry
        For Each myKey In properties
            Select Case myKey.Key.ToString
                Case "All"
                    all = myKey.Value
                Case Else
            End Select
        Next

        MyBase.loadProperties(properties)
    End Sub

    Public Overrides Sub saveProperties(ByRef properties As System.Collections.Hashtable)

    End Sub
End Class
