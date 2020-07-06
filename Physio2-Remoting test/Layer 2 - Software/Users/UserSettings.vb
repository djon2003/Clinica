Public Class UserSettings
    Inherits DBItemableBase


    Public Sub New()
        'Ensure no DBNull values
        For Each p As String In properties
            data.Add(p, String.Empty)
        Next
    End Sub

    Private _noUser As Integer
    Private loadedProperties As New Generic.List(Of String)
    Private modifiedProperties As New Generic.HashSet(Of String)
    Private data As New Generic.Dictionary(Of String, String)
    Private Shared properties() As String = New String() {"accountEquipmentStyle", "dbStyle", "billsManaging", "instantMSG", "clientLastTabs", "mailContact", "mailSystem", "mainWin", "newRV", "persoNotes", "massMailing", "punch", "reportGeneration", "searchClientStyle", "searchDBStyle", "searchKPStyle", "sendMessage", "folderExportation"}

#Region "Properties"
    Public Property folderExportation() As String
        Get
            Return data("folderExportation")
        End Get
        Set(ByVal value As String)
            data("folderExportation") = value
            modifiedProperties.Add("folderExportation")
        End Set
    End Property
    Public Property dbStyle() As String
        Get
            Return data("dbStyle")
        End Get
        Set(ByVal value As String)
            data("dbStyle") = value
            modifiedProperties.Add("dbStyle")
        End Set
    End Property
    Public Property searchDBStyle() As String
        Get
            Return data("searchDBStyle")
        End Get
        Set(ByVal value As String)
            data("searchDBStyle") = value
            modifiedProperties.Add("searchDBStyle")
        End Set
    End Property
    Public Property searchClientStyle() As String
        Get
            Return data("searchClientStyle")
        End Get
        Set(ByVal value As String)
            data("searchClientStyle") = value
            modifiedProperties.Add("searchClientStyle")
        End Set
    End Property
    Public Property newRV() As String
        Get
            Return data("newRV")
        End Get
        Set(ByVal value As String)
            data("newRV") = value
            modifiedProperties.Add("newRV")
        End Set
    End Property
    Public Property clientLastTabs() As String
        Get
            Return data("clientLastTabs")
        End Get
        Set(ByVal value As String)
            data("clientLastTabs") = value
            modifiedProperties.Add("clientLastTabs")
        End Set
    End Property
    Public Property sendMessage() As String
        Get
            Return data("sendMessage")
        End Get
        Set(ByVal value As String)
            data("sendMessage") = value
            modifiedProperties.Add("sendMessage")
        End Set
    End Property
    Public Property instantMSG() As String
        Get
            Return data("instantMSG")
        End Get
        Set(ByVal value As String)
            data("instantMSG") = value
            modifiedProperties.Add("instantMSG")
        End Set
    End Property
    Public Property searchKPStyle() As String
        Get
            Return data("searchKPStyle")
        End Get
        Set(ByVal value As String)
            data("searchKPStyle") = value
            modifiedProperties.Add("searchKPStyle")
        End Set
    End Property
    Public Property mainWin() As String
        Get
            Return data("mainWin")
        End Get
        Set(ByVal value As String)
            data("mainWin") = value
            modifiedProperties.Add("mainWin")
        End Set
    End Property
    Public Property accountEquipmentStyle() As String
        Get
            Return data("accountEquipmentStyle")
        End Get
        Set(ByVal value As String)
            data("accountEquipmentStyle") = value
            modifiedProperties.Add("accountEquipmentStyle")
        End Set
    End Property
    Public Property massMailing() As String
        Get
            Return data("massMailing")
        End Get
        Set(ByVal value As String)
            data("massMailing") = value
            modifiedProperties.Add("massMailing")
        End Set
    End Property
    Public Property punch() As String
        Get
            Return data("punch")
        End Get
        Set(ByVal value As String)
            data("punch") = value
            modifiedProperties.Add("punch")
        End Set
    End Property
    Public Property billsManaging() As String
        Get
            Return data("billsManaging")
        End Get
        Set(ByVal value As String)
            data("billsManaging") = value
            modifiedProperties.Add("billsManaging")
        End Set
    End Property
    Public Property reportGeneration() As String
        Get
            Return data("reportGeneration")
        End Get
        Set(ByVal value As String)
            data("reportGeneration") = value
            modifiedProperties.Add("reportGeneration")
        End Set
    End Property
    Public Property mailSystem() As String
        Get
            Return data("mailSystem")
        End Get
        Set(ByVal value As String)
            data("mailSystem") = value
            modifiedProperties.Add("mailSystem")
        End Set
    End Property
    Public Property mailContact() As String
        Get
            Return data("mailContact")
        End Get
        Set(ByVal value As String)
            data("mailContact") = value
            modifiedProperties.Add("mailContact")
        End Set
    End Property
    Public Property persoNotes() As String
        Get
            Return data("persoNotes")
        End Get
        Set(ByVal value As String)
            data("persoNotes") = value
            modifiedProperties.Add("persoNotes")
        End Set
    End Property
    Public Property noUser() As Integer
        Get
            Return _noUser
        End Get
        Set(ByVal value As Integer)
            _noUser = value
        End Set
    End Property

#End Region

    Public Overrides Sub delete()
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub loadData(ByVal data As DBItemableData)
        For Each d As DataRow In data.multipleData
            _noUser = d("NoUser")

            Dim p As String = d("SectorName")
            If properties.Contains(p) Then
                loadedProperties.Add(p)

                If d("Settings") Is DBNull.Value Then
                    Me.data(p) = ""
                Else
                    Me.data(p) = d("Settings")
                End If
            End If
        Next
    End Sub

    Public Overrides ReadOnly Property noItemable() As Integer
        Get
            Return noUser
        End Get
    End Property

    Public Overrides Sub saveData()
        DBLinker.getInstance().beginBatching()
        For Each p As String In properties
            If loadedProperties.Contains(p) Then
                If modifiedProperties.Contains(p) Then
                    DBLinker.getInstance.updateDB("UsersSettings", "Settings='" & data(p).Replace("'", "''") & "'", "NoUser", _noUser & " AND SectorName='" & p.Replace("'", "''") & "'", False)
                End If
            Else
                loadedProperties.Add(p)
                DBLinker.getInstance.writeDB("UsersSettings", "NoUser,SectorName,Settings", _noUser & ",'" & p.Replace("'", "''") & "','" & data(p).Replace("'", "''") & "'")
            End If
        Next
        DBLinker.getInstance().endBatching()
        modifiedProperties.Clear()
    End Sub
End Class
