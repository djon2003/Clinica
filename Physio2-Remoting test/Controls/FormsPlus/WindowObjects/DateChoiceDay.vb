Public Class DateChoiceDay
    Private _isSpecial As Boolean = False
    Private _backColor As Color = Color.White
    Private _foreColor As Color = Color.Black
    Private _toolTip As String = ""
    Private _font As New Font("Times New Roman", 8)
    Private _startTime As Date
    Private _endTime As Date
    Private _minutesInterval As Integer = 15

    Public Property minutesInterval() As Integer
        Get
            Return _minutesInterval
        End Get
        Set(ByVal value As Integer)
            _minutesInterval = value
        End Set
    End Property

    Public Property startTime() As Date
        Get
            Return _startTime
        End Get
        Set(ByVal value As Date)
            _startTime = value
        End Set
    End Property

    Public Property endTime() As Date
        Get
            Return _endTime
        End Get
        Set(ByVal value As Date)
            _endTime = value
        End Set
    End Property

    Public Property isSpecial() As Boolean
        Get
            Return _isSpecial
        End Get
        Set(ByVal value As Boolean)
            _isSpecial = value
        End Set
    End Property

    Public Property backColor() As Color
        Get
            Return _backColor
        End Get
        Set(ByVal value As Color)
            _backColor = value
        End Set
    End Property

    Public Property foreColor() As Color
        Get
            Return _foreColor
        End Get
        Set(ByVal value As Color)
            _foreColor = value
        End Set
    End Property

    Public Property font() As Font
        Get
            Return _font
        End Get
        Set(ByVal value As Font)
            _font = value
        End Set
    End Property

    Public Property toolTip() As String
        Get
            Return _toolTip
        End Get
        Set(ByVal value As String)
            _toolTip = value
        End Set
    End Property

End Class
