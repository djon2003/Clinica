
''' <summary>
''' Data holder to transit across the software
''' </summary>
''' <remarks></remarks>
Public Class DataTCP
    Inherits DataToConsume

    Public Const PARAMS_SEPARATOR As Char = "|"
    Public Const MESSAGE_DEFINITION As String = "Shall be string defined as : NOMSG" & PARAMS_SEPARATOR & "GUID" & PARAMS_SEPARATOR & "CMD[" & PARAMS_SEPARATOR & "OTHER][...]"

    Private Const MINIMUM_PARAMS As Byte = 3
    Private Const MINIMUM_PARAMS_OLD As Byte = 2
    Private data As String = String.Empty
    Private _noMessage As Integer = 0
    Private _command As String = String.Empty
    Private _args() As String = {}
    Private _client As TCPClient
    Private _guid As String = String.Empty

    Public Sub New(ByVal client As TCPClient, ByVal data As String)
        Dim sData() As String = data.Split(New Char() {PARAMS_SEPARATOR})
        'Test if second param is a GUID
        Dim hasGuid As Boolean = False
        If sData.Length >= MINIMUM_PARAMS_OLD Then hasGuid = testGUID(sData(1))

        If hasGuid AndAlso sData.Length < MINIMUM_PARAMS Then Throw New FormatException(MESSAGE_DEFINITION & vbCrLf & "Data was """ & data & """")
        If Integer.TryParse(sData(0), _noMessage) = False Then Throw New FormatException(MESSAGE_DEFINITION)

        Me._guid = If(hasGuid, sData(1), String.Empty)
        Me._command = sData(2 - If(hasGuid, 0, 1))
        Dim hasArgs As Boolean = sData.Length > (MINIMUM_PARAMS - If(hasGuid, 0, 1))
        If hasArgs Then
            ReDim _args(sData.Length - MINIMUM_PARAMS - If(hasGuid, 1, 0))
            Array.Copy(sData, MINIMUM_PARAMS - If(hasGuid, 0, 1), _args, 0, _args.Length)
        End If

        Me.data = data
        _client = client
    End Sub

    Public ReadOnly Property guid() As String
        Get
            Return _guid
        End Get
    End Property

    Public ReadOnly Property client() As TCPClient
        Get
            Return _client
        End Get
    End Property

    Public ReadOnly Property noMessage() As Integer
        Get
            Return _noMessage
        End Get
    End Property

    Public ReadOnly Property command() As String
        Get
            Return _command
        End Get
    End Property

    Public ReadOnly Property args() As String()
        Get
            Return _args
        End Get
    End Property

    Public Overrides Function toString() As String
        Return data
    End Function

    Public Function getArgsString() As String
        If _args Is Nothing OrElse _args.Length = 0 Then Return String.Empty

        Return String.Join(DataTCP.PARAMS_SEPARATOR, _args)
    End Function
End Class
