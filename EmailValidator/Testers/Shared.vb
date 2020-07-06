Module SharedCode

    Public Function AskNsLookup(ByVal arguments As String)
        Dim nslookupCmd As New Process()
        Dim procInfos As New ProcessStartInfo("nslookup.exe", arguments)
        procInfos.RedirectStandardOutput = True
        procInfos.UseShellExecute = False
        nslookupCmd.StartInfo = procInfos
        Dim s As String = ""
        nslookupCmd.Start()
        Dim newS As String = nslookupCmd.StandardOutput.ReadLine
        While (newS IsNot Nothing)
            s &= newS & vbCrLf
            newS = nslookupCmd.StandardOutput.ReadLine
        End While

        Return s
    End Function

End Module
