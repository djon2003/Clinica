Module Module1

    Public basePath As String = "C:\Projects\Physio2-Remoting test\§ Aide"
    Public dirToExtract As String = basePath & "\Extracted"
    Public extractedParts As String = basePath & "\Extracted-Parts\"
    Public fileEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("windows-1252")

    Sub main()
        Dim myExtractor As New Extractor(basePath & "\helpData.htm")
        myExtractor.extract()
    End Sub

End Module
