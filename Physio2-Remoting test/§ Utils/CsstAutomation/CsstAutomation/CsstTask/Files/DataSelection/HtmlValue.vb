Public Class HtmlValue

    Private Const INPUT_REGEX2 As String = "\<input [^<>]*(value=\""?(?<value>.*)\""? [^<>]*checked[^<>]* name=\""?###NAME###\""?)[^<>]*\>"
    Private Const INPUT_REGEX As String = "\<input [^<>]*(name=\""?###NAME###\""?[^<>]* value=\""?(?<value>.*)\""?[^<>]* checked|value=\""?(?<value>.*)\""?[^<>]* name=\""?###NAME###\""?[^<>]* checked|checked[^<>]* name=\""?###NAME###\""?[^<>]* value=\""?(?<value>.*)\""?|checked[^<>]* value=\""?(?<value>.*)\""?[^<>]* name=\""?###NAME###\""?|name=\""?###NAME###\""?[^<>]* checked[^<>]* value=\""?(?<value>.*)\""?|value=\""?(?<value>.*)\""?[^<>]* checked[^<>]* name=\""?###NAME###\""?)[^<>]*\>"
    Private Const SELECT_REGEX As String = "\<select [^<>]*name=\""?###NAME###\""?[^<>]*\>(<option[^<>]*\>[^<>]*\<\/option\>)*<option [^<>]*(selected[^<>]* value=\""?(?<value>[^<>]*)\""?|value=\""?(?<value>[^<>]*)\""?[^<>]* selected)[^<>]*\>([^<>]*)\<\/option\>(<option[^<>]*\>[^<>]*\<\/option\>)*\<\/select\>"
    Private Const TEXTAREA_REGEX As String = "\<textarea[^<>]* name=\""?###NAME###\""?[^<>]*\>(?<value>[^<>]*)\<\/textarea\>"

    Public Enum HtmlTags As Byte
        INPUT = 0
        [SELECT] = 1
        TEXTAREA = 2
    End Enum

    Private htmlTag As HtmlTags
    Private _name As String = ""
    Private _value As Object

    Public Sub New(ByVal htmlTag As HtmlTags, ByVal name As String)
        Me.htmlTag = htmlTag
        Me._name = name
    End Sub

    Public Function getMatchCaptureIndex() As Integer
        Select Case htmlTag
            Case HtmlTags.SELECT
                Return 1
            Case Else
                Return 0
        End Select
    End Function

    Public Function getRegExPattern() As String
        Select Case htmlTag
            Case HtmlTags.SELECT
                Return SELECT_REGEX.Replace("###NAME###", _name)
            Case HtmlTags.TEXTAREA
                Return TEXTAREA_REGEX.Replace("###NAME###", _name)
            Case Else
                Return INPUT_REGEX.Replace("###NAME###", _name)
        End Select
    End Function

    Public Property [value]() As Object
        Get
            Return _value
        End Get
        Set(ByVal value As Object)
            _value = value
        End Set
    End Property

    Public ReadOnly Property name() As String
        Get
            Return _name
        End Get
    End Property

End Class
