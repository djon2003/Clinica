Imports DotRas

Public Class VPNCopy
    Inherits CopyLocation

    Private WithEvents dialer As New DotRas.RasDialer()

    Private Sub dialer_StateChanged(ByVal sender As Object, ByVal e As DotRas.StateChangedEventArgs) Handles dialer.StateChanged
        Console.WriteLine("VPN connection : " & e.State)
    End Sub

    Public Overrides Sub copy()
        If Me.sourceDataPath = "" OrElse IO.File.Exists(Me.sourceDataPath) = False Then Throw New Exception("SourceDataPath is invalid")
        'If Me.LocationPath = "" OrElse IO.Directory.Exists(Me.LocationPath) = False Then Throw New Exception("LocationPath is invalid")

        Dim phoneBook As New DotRas.RasPhoneBook()
        phoneBook.Open()

        If phoneBook.Entries.Contains("Tmp_VPN_Backup_Clinica_Connection") Then phoneBook.Entries.Remove("Tmp_VPN_Backup_Clinica_Connection")

        Dim tmpPBEntry As New DotRas.RasEntry("Tmp_VPN_Backup_Clinica_Connection")
        tmpPBEntry = DotRas.RasEntry.CreateVpnEntry("Tmp_VPN_Backup_Clinica_Connection", Me.vpnLocation, DotRas.RasVpnStrategy.L2tpFirst, DotRas.RasDevice.GetDevicesByType("vpn").Item(0))
        phoneBook.Entries.Add(tmpPBEntry)

        'dialer.PhoneNumber = "physiopat.dnsalias.net"
        dialer.EntryName = "Tmp_VPN_Backup_Clinica_Connection"
        Dim rasHandle As DotRas.RasHandle = dialer.Dial(New Net.NetworkCredential(Me.vpnUserName, Me.vpnPassword))

        Dim targetPath As String = Me.locationPath & IIf(Me.locationPath.EndsWith("\"), "", "\") & "Backup-Clinica"

        Dim sourceFile As New IO.FileInfo(Me.sourceDataPath)
        If Me.keepOnlyOneCopy AndAlso IO.Directory.Exists(targetPath) Then
            For Each curFile As String In IO.Directory.GetFiles(targetPath, "*.zip")
                If curFile.EndsWith("\" & sourceFile.Name) = False Then IO.File.Delete(curFile)
            Next
        End If
        IO.Directory.CreateDirectory(targetPath)

        Dim throwingException As Exception = Nothing
        Try
            'SourceFile.CopyTo(targetPath & "\" & SourceFile.Name, True)
            copyFile(sourceFile.FullName, targetPath & "\" & sourceFile.Name)
        Catch ex As Exception
            throwingException = New Exception("Impossible to copy on target path : " & targetPath, ex)
        End Try

        If throwingException Is Nothing Then
            Dim compared As Boolean = compareFile(Me.sourceDataPath, targetPath & "\" & sourceFile.Name)
            If compared = False Then throwingException = New Exception("Comparaison is not valid for target path : " & targetPath)
        End If

        rasHandle.Close()
        phoneBook.Entries.Remove(tmpPBEntry)

        If throwingException IsNot Nothing Then Throw throwingException
    End Sub
End Class
