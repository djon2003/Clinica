Public Class FilterResult

    Private filterReturn As BasicSelectorReturn
    Private _ErrorOccured As Boolean = False
    Private allowedRapportParts As Report.RapportParts = Report.RapportParts.All

    Public Overridable ReadOnly Property filteringText() As String
        Get
            If filterReturn Is Nothing Then Return String.Empty

            Return filterReturn.filtrageTexte
        End Get
    End Property

    Public Overridable ReadOnly Property filteringWhereHeader() As String
        Get
            If filterReturn Is Nothing Then Return String.Empty
            If Me.allowedRapportParts = Report.RapportParts.All Or Me.allowedRapportParts = Report.RapportParts.Header Or Me.allowedRapportParts = Report.RapportParts.Header_Body Or Me.allowedRapportParts = Report.RapportParts.Header_Footer Then Return filterReturn.whereStr

            Return String.Empty
        End Get
    End Property

    Public Overridable ReadOnly Property filteringWhereBody() As String
        Get
            If filterReturn Is Nothing Then Return String.Empty
            If Me.allowedRapportParts = Report.RapportParts.All Or Me.allowedRapportParts = Report.RapportParts.Body Or Me.allowedRapportParts = Report.RapportParts.Header_Body Or Me.allowedRapportParts = Report.RapportParts.Body_Footer Then Return filterReturn.whereStr

            Return String.Empty
        End Get
    End Property

    Public Overridable ReadOnly Property filteringWhereFooter() As String
        Get
            If filterReturn Is Nothing Then Return String.Empty
            If Me.allowedRapportParts = Report.RapportParts.All Or Me.allowedRapportParts = Report.RapportParts.Footer Or Me.allowedRapportParts = Report.RapportParts.Body_Footer Or Me.allowedRapportParts = Report.RapportParts.Header_Footer Then Return filterReturn.whereStr

            Return String.Empty
        End Get
    End Property

    Public Overridable ReadOnly Property errorOccured() As Boolean
        Get
            Return _ErrorOccured
        End Get
    End Property

    Public Sub New(ByVal filterReturn As BasicSelectorReturn, ByVal allowedRapportParts As Report.RapportParts, ByVal errorOccured As Boolean)
        Me.filterReturn = filterReturn
        _ErrorOccured = errorOccured
        Me.allowedRapportParts = allowedRapportParts
    End Sub

    Public Sub New(ByVal filterReturn As BasicSelectorReturn, ByVal allowedRapportParts As Report.RapportParts)
        Me.filterReturn = filterReturn
        Me.allowedRapportParts = allowedRapportParts
    End Sub
End Class
