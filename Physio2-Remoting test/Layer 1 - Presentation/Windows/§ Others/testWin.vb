Public Class testWin

    Private Sub propertyGrid1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PropertyGrid1.Click

    End Sub

    Private Sub testWin_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim printerName As String = "CutePDF Writer"
        Dim strQuery As String = "Select * from Win32_PrinterConfiguration"
        Dim currentDefault As String = String.Empty

        Dim oq As New System.Management.ObjectQuery(strQuery)

        Dim query1 As New System.Management.ManagementObjectSearcher(oq)
        Dim queryCollection1 As System.Management.ManagementObjectCollection = query1.Get()
        Dim newDefault As System.Management.ManagementObject = Nothing

        Dim mo As System.Management.ManagementObject
        For Each mo In queryCollection1
            'Dim pdc As System.Management.PropertyDataCollection = mo.Properties
            '//if ((bool)mo["Local"]) 
            '//else if ((bool)mo["Network"]) 


            '// if you want to display all properties of every printer 
            'For Each pd As System.Management.PropertyData In pdc
            '    System.Console.WriteLine(" {0:12} : {1}", pd.Name, mo(pd.Name))
            'Next
            Dim name As String = mo("Name").ToString

            ListBox1.Items.Add(name)
        Next
    End Sub

    Private Sub listBox1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        Dim printerName As String = ListBox1.Text
        Dim strQuery As String = "Select * from Win32_PrinterConfiguration"
        Dim currentDefault As String = String.Empty

        Dim oq As New System.Management.ObjectQuery(strQuery)

        Dim query1 As New System.Management.ManagementObjectSearcher(oq)
        Dim queryCollection1 As System.Management.ManagementObjectCollection = query1.Get()
        Dim newDefault As System.Management.ManagementObject = Nothing

        Dim mo As System.Management.ManagementObject
        For Each mo In queryCollection1
            'Dim pdc As System.Management.PropertyDataCollection = mo.Properties
            '//if ((bool)mo["Local"]) 
            '//else if ((bool)mo["Network"]) 


            '// if you want to display all properties of every printer 
            'For Each pd As System.Management.PropertyData In pdc
            '    System.Console.WriteLine(" {0:12} : {1}", pd.Name, mo(pd.Name))
            'Next
            Dim name As String = mo("Name").ToString

            If mo("Name").ToString.Equals(printerName) Then
                Dim ori As String = mo("Orientation")
                'MsgBox(mo("Orientation"))
                'mo.SetPropertyValue("Orientation", Orientation)
                'mo.Put()
                'MsgBox(mo("Orientation"))
                mo.Scope.Options.Authentication = Management.AuthenticationLevel.Call
                Dim isC As Boolean = mo.Scope.IsConnected
                mo.Scope.Connect()
                mo.SetPropertyValue("Orientation", 1)

                PropertyGrid1.SelectedObject = mo
                Exit For
            End If
        Next
    End Sub

    Private Sub propertyGrid1_PropertyValueChanged(ByVal s As Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles PropertyGrid1.PropertyValueChanged
        Dim mo As System.Management.ManagementObject = PropertyGrid1.SelectedObject
        mo.Put()
    End Sub
End Class
