Imports System.Reflection

Public Class ConfigInfos

    Private Const INFO_SPLITTER As String = "§§"

    Private _config As ConfigBase
    Private file As String
    Private fileInfo As IO.FileInfo
    Private tempFolder As String

    Public Sub New(ByVal dataString As String, ByVal tempFolder As String)
        Me.tempFolder = tempFolder

        Dim data() As String = dataString.Split(New String() {INFO_SPLITTER}, StringSplitOptions.None)

        'Set/Create config upon the answer has a file or not within it
        If data.Length = 2 OrElse data(2).Trim() = "" Then
            _config = ConfigurationsManager.getInstance.getItemable(data(0))
        Else
            createConfigFromFile(data)
        End If

        _config.load(data(1))
    End Sub

    Private Sub createConfigFromFile(ByVal data() As String)
        file = tempFolder & addSlash(tempFolder) & data(2)
        Try
            IO.File.WriteAllBytes(file, Convert.FromBase64String(data(3)))
        Catch ex As Exception
            'Already loaded
        End Try
        fileInfo = New IO.FileInfo(file)

        Dim loadedAssembly As Assembly = Assembly.LoadFile(file)
        Dim configType As Type = loadedAssembly.GetType(data(0))

        _config = Activator.CreateInstance(configType)
    End Sub

    Public Sub New(ByVal config As ConfigBase)
        _config = config
        file = config.GetType.Assembly.GetFiles(False)(0).Name
        fileInfo = New IO.FileInfo(file)
    End Sub

    Public ReadOnly Property config() As ConfigBase
        Get
            Return _config
        End Get
    End Property

    Private Function serializeConfig(ByVal obj As Object) As String
        Dim js As New Web.Script.Serialization.JavaScriptSerializer()
        Return js.Serialize(obj)
    End Function

    Public Overrides Function ToString() As String
        Dim isClient As Boolean = TCPClient.getInstance.isConnected
        Return _config.GetType.FullName _
                           & INFO_SPLITTER & serializeConfig(_config) _
                           & INFO_SPLITTER & If(isClient, " ", fileInfo.Name) _
                           & INFO_SPLITTER & If(isClient, " ", Convert.ToBase64String(IO.File.ReadAllBytes(file)))
    End Function
End Class
