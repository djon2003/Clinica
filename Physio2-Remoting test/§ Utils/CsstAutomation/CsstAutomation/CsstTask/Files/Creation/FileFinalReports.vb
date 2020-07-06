Public Class FileFinalReports
    Inherits FileReports

    Public Const FILE_PREFIX As String = "rfi"

    Public Sub New(ByVal xmlFile As String)
        MyBase.New(xmlFile)
    End Sub

    Public Sub New(ByVal outPath As String, ByVal data As DataSet)
        MyBase.New(outPath, data)
    End Sub

    Private _nbReports As Integer = 0

    Protected Overrides Sub writeLine()
        outLine.Append(LineType.FINAL)

        writeClientInfo()

        'Date de rechute   SSAAMMJJ  9(8) - Optionel
        writeSpaces(8)

        writeMiddleInfos()

        'Date de la fin des traitements   SSAAMMJJ  9(8)
        Dim lastTreamentDate As Date = Date.Today
        If curRow.Table.Columns.Contains("LastTreamentDate") = False OrElse curRow("LastTreamentDate") Is DBNull.Value Then
            errors.Add(New FieldValidationException("La date du dernier traitement est manquante.", True))
        Else
            lastTreamentDate = curRow("LastTreamentDate")
        End If
        If lastTreamentDate.Date > Date.Today Then errors.Add(New FieldValidationException("La dernière date de traitement ne doit pas être dans le futur.", True))
        writeDateField(lastTreamentDate)

        'Date du congé du médecin   SSAAMMJJ  9(8) - Optionel
        writeSpaces(8)

        'Nombre total de traitements à ce jour   9(3) - Optionel
        writeSpaces(3)

        'État du travailleur à son départ   X(360)
        writeTextField(getSoap(340), 360)

        writeEndInfos()
        _nbReports += 1
    End Sub

    Protected Overrides ReadOnly Property nbReports() As Integer
        Get
            Return _nbReports
        End Get
    End Property

    Protected Overrides ReadOnly Property dataTableName() As String
        Get
            Return "finals"
        End Get
    End Property

    Protected Overrides ReadOnly Property filePrefix() As String
        Get
            Return FILE_PREFIX
        End Get
    End Property

    Protected Overrides Function getExtraResultInfo() As String
        Return String.Empty
    End Function
End Class
