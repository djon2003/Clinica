Public Class ErrorsLinks

#Region "Error classes"

    Public Class ErrorPeriodToAnotherClinic
        Public noFolder As Integer
        Public isUsed As Boolean = False

        Public Sub New(ByVal noFolder As Integer, ByVal isUsed As Boolean)
            Me.noFolder = noFolder
            Me.isUsed = isUsed
        End Sub
    End Class

    Public Class ErrorDate
        Public oldDate As Date
        Public isUsed As Boolean = False

        Public Sub New(ByVal oldDate As Date, ByVal isUsed As Boolean)
            Me.oldDate = oldDate
            Me.isUsed = isUsed
        End Sub
    End Class

    Public Class ErrorNAM
        Public noClient As Integer = 0
        Public isUsed As Boolean = False

        Public Sub New(ByVal noClient As Integer, ByVal isUsed As Boolean)
            Me.noClient = noClient
            Me.isUsed = isUsed
        End Sub
    End Class


    Public Class ErrorCSSTNumber
        Public noFolder As Integer = 0
        Public isUsed As Boolean = False

        Public Sub New(ByVal noFolder As Integer, ByVal isUsed As Boolean)
            Me.noFolder = noFolder
            Me.isUsed = isUsed
        End Sub
    End Class

    Public Class ErrorMedecinRef
        Public noKP As Integer = 0
        Public oldNoRef As String = ""
        Public isUsed As Boolean = False
        Public noFolder As Integer = 0

        Public Sub New(ByVal noFolder As Integer, ByVal noKP As Integer, ByVal oldNoRef As String, ByVal isUsed As Boolean)
            Me.noFolder = noFolder
            Me.noKP = noKP
            Me.isUsed = isUsed
            Me.oldNoRef = oldNoRef
        End Sub
    End Class

    Public Class ErrorTRPNoPermit
        Public noUser As Integer = 0
        Public oldNoPermit As String = ""
        Public isUsed As Boolean = False
        Public noFolder As Integer = 0

        Public Sub New(ByVal noFolder As Integer, ByVal noUser As Integer, ByVal oldNoPermit As String, ByVal isUsed As Boolean)
            Me.noFolder = noFolder
            Me.noUser = noUser
            Me.isUsed = isUsed
            Me.oldNoPermit = oldNoPermit
        End Sub
    End Class

#End Region

    Public errorNAMsLinks As New Dictionary(Of String, ErrorNAM)
    Public errorMedecinRefsLinks As New Dictionary(Of Integer, ErrorMedecinRef)
    Public errorTRPNoPermitLinks As New Dictionary(Of Integer, ErrorTRPNoPermit)
    Public errorCSSTNumbersLinks As New Dictionary(Of String, ErrorCSSTNumber)
    Public errorEventDatesLinks As New Dictionary(Of Integer, ErrorDate)
    Public errorOrdonnanceDatesLinks As New Dictionary(Of Integer, ErrorDate)
    Public errorPeriodToAnotherClinicsLinks As New Dictionary(Of String, ErrorPeriodToAnotherClinic)

    Public tryAgainCounters As New Dictionary(Of Integer, Integer)

    Private nbErrors As Integer = 0

    Public Sub New()
    End Sub

    Public Sub load()
        loadErrorOrdonnanceDatesLinks()
        loadErrorCSSTNumbersLinks()
        loadErrorEventDatesLinks()
        loadErrorMedecinRefsLinks()
        loadErrorNAMsLinks()
        loadErrorPeriodToAnotherClinicsLinks()
        loadErrorTRPNoPermitLinks()

        loadTryAgainCounters()
    End Sub

    Public Sub save(ByVal nbErrors As Integer)
        Me.nbErrors = nbErrors

        saveErrorOrdonnanceDatesLinks()
        saveErrorCSSTNumbersLinks()
        saveErrorEventDatesLinks()
        saveErrorMedecinRefsLinks()
        saveErrorNAMsLinks()
        saveErrorPeriodToAnotherClinicsLinks()
        saveErrorTRPNoPermitLinks()

        saveTryAgainCounters()
    End Sub


#Region "Load/Save private functions"
    Private Sub loadErrorPeriodToAnotherClinicsLinks()
        Dim periodtoanotherFile As String = Config.current.outputFolder & addSlash(Config.current.outputFolder) & "periodtoanother.link"
        If Not IO.File.Exists(periodtoanotherFile) Then Exit Sub

        Dim lines() As String = IO.File.ReadAllLines(periodtoanotherFile)
        For Each curLine As String In lines
            If String.IsNullOrEmpty(curLine) Then Continue For

            Dim line() As String = curLine.Split(New Char() {"="})
            errorPeriodToAnotherClinicsLinks.Add(line(0), New ErrorPeriodToAnotherClinic(Integer.Parse(line(1)), False))
        Next
    End Sub

    Private Sub saveErrorPeriodToAnotherClinicsLinks()
        Dim periodtoanotherFile As String = Config.current.outputFolder & addSlash(Config.current.outputFolder) & "periodtoanother.link"
        If IO.File.Exists(periodtoanotherFile) Then IO.File.Delete(periodtoanotherFile)
        If nbErrors = 0 Then Exit Sub

        Dim savingContent As New System.Text.StringBuilder()
        For Each nam As String In errorPeriodToAnotherClinicsLinks.Keys
            savingContent.Append(nam).Append("=").AppendLine(errorPeriodToAnotherClinicsLinks(nam).noFolder)
        Next
        IO.File.WriteAllText(periodtoanotherFile, savingContent.ToString())
    End Sub

    Private Sub loadErrorEventDatesLinks()
        loadErrorDatesLinks("eventdates.link", errorEventDatesLinks)
    End Sub

    Private Sub saveErrorEventDatesLinks()
        saveErrorDatesLinks("eventdates.link", errorEventDatesLinks)
    End Sub

    Private Sub loadErrorOrdonnanceDatesLinks()
        loadErrorDatesLinks("orddates.link", errorOrdonnanceDatesLinks)
    End Sub

    Private Sub saveErrorOrdonnanceDatesLinks()
        saveErrorDatesLinks("orddates.link", errorOrdonnanceDatesLinks)
    End Sub


    Private Sub loadErrorDatesLinks(ByVal fileName As String, ByRef errorsList As Dictionary(Of Integer, ErrorDate))
        Dim eventdatesFile As String = Config.current.outputFolder & addSlash(Config.current.outputFolder) & fileName
        If Not IO.File.Exists(eventdatesFile) Then Exit Sub

        Dim lines() As String = IO.File.ReadAllLines(eventdatesFile)
        For Each curLine As String In lines
            If String.IsNullOrEmpty(curLine) Then Continue For

            Dim line() As String = curLine.Split(New Char() {"="})
            Dim sOldDate() As String = line(1).Split("-")
            Dim oldDate As New Date(Integer.Parse(sOldDate(0)), Integer.Parse(sOldDate(1)), Integer.Parse(sOldDate(2)))
            errorsList.Add(line(0), New ErrorDate(oldDate, False))
        Next
    End Sub

    Private Sub saveErrorDatesLinks(ByVal fileName As String, ByRef errorsList As Dictionary(Of Integer, ErrorDate))
        Dim datesFile As String = Config.current.outputFolder & addSlash(Config.current.outputFolder) & fileName
        If IO.File.Exists(datesFile) Then IO.File.Delete(datesFile)
        If nbErrors = 0 Then Exit Sub

        Dim savingContent As New System.Text.StringBuilder()
        For Each noFolder As Integer In errorsList.Keys
            savingContent.Append(noFolder).Append("=").AppendLine(errorsList(noFolder).oldDate.ToString("yyyy-MM-dd"))
        Next
        IO.File.WriteAllText(datesFile, savingContent.ToString())
    End Sub


    Private Sub loadErrorNAMsLinks()
        Dim namsFile As String = Config.current.outputFolder & addSlash(Config.current.outputFolder) & "nams.link"
        If Not IO.File.Exists(namsFile) Then Exit Sub

        Dim lines() As String = IO.File.ReadAllLines(namsFile)
        For Each curLine As String In lines
            If String.IsNullOrEmpty(curLine) Then Continue For

            Dim line() As String = curLine.Split(New Char() {"="})
            errorNAMsLinks.Add(line(0), New ErrorNAM(line(1), False))
        Next
    End Sub

    Private Sub saveErrorNAMsLinks()
        Dim namsFile As String = Config.current.outputFolder & addSlash(Config.current.outputFolder) & "nams.link"
        If IO.File.Exists(namsFile) Then IO.File.Delete(namsFile)
        If nbErrors = 0 Then Exit Sub

        Dim savingContent As New System.Text.StringBuilder()
        For Each nam As String In errorNAMsLinks.Keys
            savingContent.Append(nam).Append("=").AppendLine(errorNAMsLinks(nam).noClient)
        Next
        IO.File.WriteAllText(namsFile, savingContent.ToString())
    End Sub

    Private Sub loadErrorMedecinRefsLinks()
        Dim medecinFile As String = Config.current.outputFolder & addSlash(Config.current.outputFolder) & "doctors.link"
        If Not IO.File.Exists(medecinFile) Then Exit Sub

        Dim lines() As String = IO.File.ReadAllLines(medecinFile)
        For Each curLine As String In lines
            If String.IsNullOrEmpty(curLine) Then Continue For

            Dim line() As String = curLine.Split(New Char() {"="})
            errorMedecinRefsLinks.Add(line(0), New ErrorMedecinRef(line(0), line(1), line(2), False))
        Next
    End Sub

    Private Sub loadErrorTRPNoPermitLinks()
        Dim trpFile As String = Config.current.outputFolder & addSlash(Config.current.outputFolder) & "trpnopermit.link"
        If Not IO.File.Exists(trpFile) Then Exit Sub

        Dim lines() As String = IO.File.ReadAllLines(trpFile)
        For Each curLine As String In lines
            If String.IsNullOrEmpty(curLine) Then Continue For

            Dim line() As String = curLine.Split(New Char() {"="})
            errorTRPNoPermitLinks.Add(line(0), New ErrorTRPNoPermit(line(0), line(1), line(2), False))
        Next
    End Sub

    Private Sub saveErrorMedecinRefsLinks()
        Dim medecinFile As String = Config.current.outputFolder & addSlash(Config.current.outputFolder) & "doctors.link"
        If IO.File.Exists(medecinFile) Then IO.File.Delete(medecinFile)
        If nbErrors = 0 Then Exit Sub

        Dim savingContent As New System.Text.StringBuilder()
        For Each noFolder As Integer In errorMedecinRefsLinks.Keys
            savingContent.Append(noFolder).Append("=").Append(errorMedecinRefsLinks(noFolder).noKP).Append("=").AppendLine(errorMedecinRefsLinks(noFolder).oldNoRef)
        Next
        IO.File.WriteAllText(medecinFile, savingContent.ToString())
    End Sub

    Private Sub saveErrorTRPNoPermitLinks()
        Dim trpFile As String = Config.current.outputFolder & addSlash(Config.current.outputFolder) & "trpnopermit.link"
        If IO.File.Exists(trpFile) Then IO.File.Delete(trpFile)
        If nbErrors = 0 Then Exit Sub

        Dim savingContent As New System.Text.StringBuilder()
        For Each noFolder As Integer In errorTRPNoPermitLinks.Keys
            savingContent.Append(noFolder).Append("=").Append(errorTRPNoPermitLinks(noFolder).noUser).Append("=").AppendLine(errorTRPNoPermitLinks(noFolder).oldNoPermit)
        Next
        IO.File.WriteAllText(trpFile, savingContent.ToString())
    End Sub

    Private Sub loadErrorCSSTNumbersLinks()
        Dim csstNumbersFile As String = Config.current.outputFolder & addSlash(Config.current.outputFolder) & "noref.link"
        If Not IO.File.Exists(csstNumbersFile) Then Exit Sub

        Dim lines() As String = IO.File.ReadAllLines(csstNumbersFile)
        For Each curLine As String In lines
            If String.IsNullOrEmpty(curLine) Then Continue For

            Dim line() As String = curLine.Split(New Char() {"="})
            errorCSSTNumbersLinks.Add(line(0), New ErrorCSSTNumber(line(1), False))
        Next
    End Sub

    Private Sub saveErrorCSSTNumbersLinks()
        Dim csstNumbersFile As String = Config.current.outputFolder & addSlash(Config.current.outputFolder) & "noref.link"
        If IO.File.Exists(csstNumbersFile) Then IO.File.Delete(csstNumbersFile)
        If nbErrors = 0 Then Exit Sub

        Dim savingContent As New System.Text.StringBuilder()
        For Each noCsstNumber As String In errorCSSTNumbersLinks.Keys
            savingContent.Append(noCsstNumber).Append("=").AppendLine(errorCSSTNumbersLinks(noCsstNumber).noFolder)
        Next
        IO.File.WriteAllText(csstNumbersFile, savingContent.ToString())
    End Sub

    Private Sub loadTryAgainCounters()
        Dim tryAgainFile As String = Config.current.outputFolder & addSlash(Config.current.outputFolder) & "tries-presences.count"
        If Not IO.File.Exists(tryAgainFile) Then Exit Sub

        Dim lines() As String = IO.File.ReadAllLines(tryAgainFile)
        For Each curLine As String In lines
            If String.IsNullOrEmpty(curLine) Then Continue For

            Dim line() As String = curLine.Split(New Char() {"="})
            tryAgainCounters.Add(line(0), line(1))
        Next
    End Sub

    Private Sub saveTryAgainCounters()
        Dim tryAgainFile As String = Config.current.outputFolder & addSlash(Config.current.outputFolder) & "tries-presences.count"
        If IO.File.Exists(tryAgainFile) Then IO.File.Delete(tryAgainFile)

        Dim savingContent As New System.Text.StringBuilder()
        For Each noFolder As String In tryAgainCounters.Keys
            savingContent.Append(noFolder).Append("=").AppendLine(tryAgainCounters(noFolder))
        Next
        IO.File.WriteAllText(tryAgainFile, savingContent.ToString())
    End Sub

#End Region
End Class
