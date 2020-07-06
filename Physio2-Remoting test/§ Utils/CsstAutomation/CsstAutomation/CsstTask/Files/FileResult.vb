Public Class FileResult

    Public noClient, noFolder As Integer
    Public nam As String = String.Empty
    Public clientName As String = String.Empty
    Public resultObject As String = String.Empty
    Public resultMessage As String = String.Empty
    Public noVisites As String = String.Empty
    Public filename As String = String.Empty
    Public isError As Boolean = False
    Public fontColor As String = String.Empty
    Public refDate As Date = Base.LIMIT_DATE
    Public service As String = String.Empty

    Public Sub New(ByVal data As DataRow)
        For Each curColumn As DataColumn In data.Table.Columns
            If data(curColumn.ColumnName) IsNot Nothing AndAlso data(curColumn.ColumnName) IsNot DBNull.Value Then
                Dim fieldToChange As System.Reflection.FieldInfo = Me.GetType().GetField(curColumn.ColumnName)
                If fieldToChange IsNot Nothing Then
                    fieldToChange.SetValue(Me, Base.convertToType(data(curColumn.ColumnName), fieldToChange.FieldType))
                End If
            End If
        Next
    End Sub

    Public Sub New(ByVal isError As Boolean, ByVal noClient As Integer, ByVal clientName As String, ByVal noFolder As Integer, ByVal resultObject As String, ByVal resultMessage As String, ByVal filename As String, ByVal nam As String, ByVal fontColor As String, ByVal noVisites As String, ByVal refDate As Date, ByVal service As String)
        Me.noClient = noClient
        Me.noFolder = noFolder
        Me.clientName = clientName
        Me.resultObject = resultObject
        Me.resultMessage = resultMessage
        Me.filename = filename
        Me.isError = isError
        Me.nam = nam
        Me.fontColor = fontColor
        Me.noVisites = noVisites
        Me.refDate = refDate
        Me.service = service
    End Sub

    Public Function getXML() As String
        Dim xml As New System.Text.StringBuilder()
        xml.AppendLine("<" & Me.GetType().Name & ">")
        For Each curMember As System.Reflection.FieldInfo In Me.GetType().GetFields()
            xml.Append("<" & curMember.Name & ">")
            xml.Append(curMember.GetValue(Me).ToString().Replace(">", "&gt;").Replace("<", "&lt;"))
            xml.AppendLine("</" & curMember.Name & ">")
        Next
        xml.AppendLine("</" & Me.GetType().Name & ">")

        Return xml.ToString()
    End Function

End Class
