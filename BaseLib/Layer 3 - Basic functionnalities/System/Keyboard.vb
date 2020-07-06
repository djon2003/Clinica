Public Class Keyboard

    Public Shared Property numLockActivated() As Boolean
        Get
            Return My.Computer.Keyboard.NumLock
        End Get
        Set(ByVal value As Boolean)
            If value <> My.Computer.Keyboard.NumLock Then
                Dim wshShell As Object
                wshShell = CreateObject("WScript.Shell")
                wshShell.SendKeys("{NUMLOCK}")
            End If
        End Set
    End Property
End Class
