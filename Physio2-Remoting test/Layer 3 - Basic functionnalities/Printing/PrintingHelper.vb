Imports System.Drawing.Printing

Public Class PrintingHelper

    Private Shared _DefHeader As String = ""
    Private Shared _DefFooter As String = "&bPage &p / &P"
    Private Shared sysDefPrinter As String = ""
    Private Shared defBrowser As New Base.Windows.Forms.WebControl
    Private Shared _PrintingInfos As New PrintingInfos(_DefHeader, _DefFooter, -1, -1, -1, -1)


#Region "Propriétés"
    Public Shared ReadOnly Property jobsCount() As Integer
        Get
            Return _jobs.Count + If(currentJob Is Nothing, 0, 1)
        End Get
    End Property


    Public Shared ReadOnly Property systemDefautPrinter() As String
        Get
            If sysDefPrinter = "" Then sysDefPrinter = PrintingHelper.getDefaultSystemPrinter()
            Return sysDefPrinter
        End Get
    End Property

    Public Shared WriteOnly Property printingInfos() As PrintingInfos
        Set(ByVal value As PrintingInfos)
            If value Is Nothing Then
                _PrintingInfos = New PrintingInfos(_DefHeader, _DefFooter, -1, -1, -1, -1)
                Exit Property
            End If
            _PrintingInfos = value
        End Set
    End Property

    Public Shared Property defHeader() As String
        Get
            Return _DefHeader
        End Get
        Set(ByVal value As String)
            _DefHeader = value
        End Set
    End Property

    Public Shared Property defFooter() As String
        Get
            Return _DefFooter
        End Get
        Set(ByVal value As String)
            _DefFooter = value
        End Set
    End Property
#End Region

    Public Shared Event JobsChanged()

    Public Enum JobStatuses As Integer
        InQueue = 0
        PresendingActions = 1
        SendingToPrinter = 2
        PostsendingActions = 3
    End Enum

    Private Shared _jobs As New Generic.Queue(Of PrintingJob)(50)
    Private Shared jobsLock As New Threading.Mutex()
    Private Shared printingThread As Threading.Thread
    Private Shared currentJob As PrintingJob

    Public Shared Sub focusOnPrintersWindow()
        If currentJob Is Nothing Then Exit Sub

        currentJob.setPrintingFocus()
    End Sub

    Public Shared Function getJobs() As Generic.List(Of PrintingJob)
        Dim printJobs As New Generic.List(Of PrintingJob)(_jobs.ToArray())
        If currentJob IsNot Nothing Then printJobs.Add(currentJob)

        Return printJobs
    End Function

    Private Shared Sub internalPrint()
        Try
            jobsLock.WaitOne()
            currentJob = _jobs.Dequeue()
            jobsLock.ReleaseMutex()
            While currentJob IsNot Nothing
                printHtml2(currentJob.getPrintingForm(), currentJob.promptUser, currentJob.waitForSpooling, currentJob.printerName, currentJob.setBackSysDefPrinter)

                jobsLock.WaitOne()
                If _jobs.Count = 0 Then
                    currentJob = Nothing
                Else
                    currentJob = _jobs.Dequeue()
                End If
                jobsLock.ReleaseMutex()

                RaiseEvent JobsChanged()
            End While
        Catch ex As Exception
            addErrorLog(ex)
        End Try
    End Sub

    Private Shared logNumber As Integer = 0

    Public Shared Sub printHtml(ByVal html As String, ByVal jobTitle As String, Optional ByVal promptUser As Boolean = True, Optional ByVal waitForSpooling As Boolean = False, Optional ByVal printerName As String = "", Optional ByVal setBackSysDefPrinter As Boolean = True)

        logNumber += 1
        logToLocal("PrintingHelper " & localLogNumber & " - " & logNumber & " : Begin")

        Dim newJob As New PrintingJob()
        logToLocal("PrintingHelper " & localLogNumber & " - " & logNumber & " : After creating PrintJob")
        newJob.jobTitle = jobTitle
        newJob.html = html
        logToLocal("PrintingHelper " & localLogNumber & " - " & logNumber & " : After setting HTML")
        newJob.printerName = printerName
        newJob.promptUser = promptUser
        newJob.setBackSysDefPrinter = setBackSysDefPrinter
        newJob.waitForSpooling = waitForSpooling

        logToLocal("PrintingHelper " & localLogNumber & " - " & logNumber & " : Before enqueue")

        jobsLock.WaitOne()
        _jobs.Enqueue(newJob)
        jobsLock.ReleaseMutex()

        logToLocal("PrintingHelper " & localLogNumber & " - " & logNumber & " : After enqueue")

        RaiseEvent JobsChanged()

        If printingThread Is Nothing OrElse printingThread.IsAlive = False Then
            printingThread = New Threading.Thread(AddressOf internalPrint)
            printingThread.SetApartmentState(Threading.ApartmentState.STA)
            printingThread.Start()
        End If

        logToLocal("PrintingHelper " & localLogNumber & " - " & logNumber & " : End")
    End Sub


    Public Shared Sub printHtml(ByVal printingWebControl As Base.Windows.Forms.WebControl, ByVal jobTitle As String, Optional ByVal promptUser As Boolean = True, Optional ByVal waitForSpooling As Boolean = False, Optional ByVal printerName As String = "", Optional ByVal setBackSysDefPrinter As Boolean = True)
        Dim controlCreated As Boolean = False
        If printingWebControl Is Nothing Then
            printingWebControl = New Base.Windows.Forms.WebControl
            controlCreated = True
        End If

        'TODO : Try to get an image from the webpage so I could use the .Net printing namespace
        'Tests gave me only the upper left corner of the report which was scaled up and the rest was totally black

        'Dim t As New Bitmap(1000, 3000)
        'printingWebControl.DrawToBitmap(t, New Rectangle(0, 0, t.Width, t.Height))
        'IO.File.WriteAllText("c:\temp\tt.txt", "TEST")
        't.Save("C:\temp\testing.bmp")

        'IO.File.WriteAllText("c:\temp\tt.txt", "TEST")
        'printingWebControl.drawToImage().Save("C:\temp\testing.bmp")

        printHtml(printingWebControl.getHTML(), jobTitle, promptUser, waitForSpooling, printerName, setBackSysDefPrinter)

        If controlCreated Then printingWebControl.Dispose()
    End Sub


    Private Shared Sub printHtml2(ByVal curPrintingForm As PrintingForm, Optional ByVal promptUser As Boolean = True, Optional ByVal waitForSpooling As Boolean = False, Optional ByVal printerName As String = "", Optional ByVal setBackSysDefPrinter As Boolean = True)
        currentJob.jobStatus = JobStatuses.PresendingActions
        RaiseEvent JobsChanged()

        Dim tmp As Long = Date.Now.Ticks
        Dim curSysDefPrinter As String = ""
        'REM Parameter desactivated
        'If PromptUser = False Then
        curSysDefPrinter = systemDefautPrinter
        Dim curPrinter As String = Software.config.defaultPrinter
        If printerName <> "" Then curPrinter = printerName
        If curPrinter <> "" Then
            PrintingHelper.setDefaultSystemPrinter(curPrinter)
            waitForSpooling = True
        Else
            curSysDefPrinter = ""
        End If
        'End If
        If currentUserName = "Administrateur" Then myMainWin.StatusText = "Sub time0:" & ((Date.Now.Ticks - tmp) / 10000000).ToString : tmp = Now.Ticks

        If curPrintingForm Is Nothing Then curPrintingForm = PrintingForm.getInstance()
        If currentUserName = "Administrateur" Then myMainWin.StatusText = "Sub time1:" & ((Date.Now.Ticks - tmp) / 10000000).ToString : tmp = Now.Ticks

        PrintingHelper._PrintingInfos.setToRegistry() 'Change les marges, l'en-tête/pied de page, printBackground,shrinkToFit

        If currentUserName = "Administrateur" Then myMainWin.StatusText = "Sub time2:" & ((Date.Now.Ticks - tmp) / 10000000).ToString : tmp = Now.Ticks
        currentJob.jobStatus = JobStatuses.SendingToPrinter
        RaiseEvent JobsChanged()
        curPrintingForm.print(promptUser, waitForSpooling)
        currentJob.jobStatus = JobStatuses.PostsendingActions
        RaiseEvent JobsChanged()
        If currentUserName = "Administrateur" Then myMainWin.StatusText = "Sub time3:" & ((Date.Now.Ticks - tmp) / 10000000).ToString : tmp = Now.Ticks

        If curSysDefPrinter <> "" And setBackSysDefPrinter Then PrintingHelper.setDefaultSystemPrinter(sysDefPrinter)

        PrintingHelper._PrintingInfos.resetOriginalValues()

        If currentUserName = "Administrateur" Then myMainWin.StatusText = "Sub time4:" & ((Date.Now.Ticks - tmp) / 10000000).ToString
    End Sub

    Public Shared Function getPrintersNames(Optional ByVal includeDefPrinter As Boolean = False) As String()
        Dim nbPrinters As Integer = PrinterSettings.InstalledPrinters.Count
        If includeDefPrinter = False Then nbPrinters -= 1
        Dim printers() As String
        ReDim printers(nbPrinters)

        Dim n As Integer = 0
        If includeDefPrinter Then
            printers(n) = "* Imprimante par défaut *"
            n += 1
        End If

        For Each printer As String In PrinterSettings.InstalledPrinters
            printers(n) = printer
            n += 1
        Next

        Return printers
    End Function

    Public Shared Sub setPrinterorientation(ByVal printerName As String, ByVal orientation As Integer)
        Dim strQuery As String = "Select * from Win32_PrinterConfiguration"
        Dim currentDefault As String = String.Empty

        Dim oq As New System.Management.ObjectQuery(strQuery)

        Dim query1 As New System.Management.ManagementObjectSearcher(oq)
        Dim queryCollection1 As System.Management.ManagementObjectCollection = query1.Get()
        Dim newDefault As System.Management.ManagementObject = Nothing

        For Each mo As System.Management.ManagementObject In queryCollection1
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
                MsgBox(mo("Orientation"))
                mo.SetPropertyValue("Orientation", orientation)
                mo.Put()
                MsgBox(mo("Orientation"))
                Exit For
            End If
        Next
    End Sub

    Public Shared Function getDefaultSystemPrinter() As String
        Dim pd As New PrintDocument
        Return pd.PrinterSettings.PrinterName '/ Get the system default printer
    End Function

    Public Shared Function setDefaultSystemPrinter(ByVal strPrinterName As String) As Boolean
        'ref: http://www.planet-source-code.com/URLSEO/vb/scripts/ShowCode!asp/txtCodeId!3053/lngWid!10/anyname.htm

        ' Imports System.Drawing.Printing
        ' Example: SetDefaultSystemPrinter("Lexmark T620") - local printer
        ' Example: SetDefaultSystemPrinter("\\Server01\Lexmark T522") - network printer
        Dim strOldPrinter As String = ""
        Dim wshNetwork As Object = Nothing
        Dim pd As New PrintDocument
        Dim isOldValid As Boolean = False

        ' Set the default system printer
        Try
            isOldValid = pd.PrinterSettings.IsValid
            strOldPrinter = pd.PrinterSettings.PrinterName '/ Get the system default printer
            pd.PrinterSettings.PrinterName = strPrinterName '/ Specify the printer to use.
            wshNetwork = Microsoft.VisualBasic.CreateObject("WScript.Network")
            Dim installedPrinters() As String
            ReDim installedPrinters(PrinterSettings.InstalledPrinters.Count - 1)
            PrinterSettings.InstalledPrinters.CopyTo(installedPrinters, 0)
            If Array.IndexOf(installedPrinters, strPrinterName) <> -1 AndAlso pd.PrinterSettings.IsValid AndAlso strOldPrinter <> strPrinterName Then '/ Check that the printer exists
                wshNetwork.SetDefaultPrinter(strPrinterName)
                Return True
            Else
                If isOldValid Then wshNetwork.SetDefaultPrinter(strOldPrinter)
                Return False
            End If
        Catch exptd As Exception
            Try
                If wshNetwork IsNot Nothing AndAlso strOldPrinter <> "" AndAlso isOldValid Then wshNetwork.SetDefaultPrinter(strOldPrinter)
            Catch ex As Exception
                addErrorLog(New Exception("isOldValid=" & isOldValid & vbCrLf & "PrinterSettings.InstalledPrinters.Count=" & PrinterSettings.InstalledPrinters.Count & vbCrLf & "strOldPrinter=" & strOldPrinter, ex))
            End Try
            Return False
        Finally
            wshNetwork = Nothing
            pd = Nothing
        End Try
    End Function '/ SetDefaultSystemPrinter

End Class

Public Class cPrinter

#Region " --- Method 2 --- "

    'ref: http://www.dotnet4all.com/dotnet-code/2004/11/changing-printers-in-visual-basic.html
    'Demande une référence System.Management

    'Private Function setDefaultSystemPrinter2(ByVal strPrinterName As String) As Boolean
    '    Dim moReturn As Management.ManagementObjectCollection
    '    Dim moSearch As Management.ManagementObjectSearcher
    '    Dim mo As Management.ManagementObject

    '    moSearch = New Management.ManagementObjectSearcher("Select * from Win32_Printer")
    '    moReturn = moSearch.Get

    '    For Each mo In moReturn
    '        Dim objReturn As Object
    '        Debug.WriteLine(mo("Name"))
    '        If mo("Name").ToString.Trim.ToUpper = strPrinterName.Trim.ToUpper Then
    '            mo.InvokeMethod("SetDefaultPrinter", objReturn)
    '            Return True
    '        End If
    '    Next
    '    Return False
    'End Function

#End Region

#Region " --- Method 3 --- "

    'ref: http://www.vbcity.com/forums/topic.asp?tid=70315

    'Private Declare Function WriteProfileString Lib "kernel32" Alias "WriteProfileStringA" _
    '(ByVal lpszSection As String, ByVal lpszKeyName As String, _
    'ByVal lpszString As String) As Long
    'Private Declare Function SendMessage Lib "user32" Alias "SendMessageA" _
    '     (ByVal hwnd As Long, ByVal wMsg As Long, _
    '     ByVal wParam As Long, ByVal lparam As String) As Long
    'Private Const HWND_BROADCAST As Long = &HFFFF&
    'Private Const WM_WININICHANGE As Long = &H1A

    'Private Function setDefaultSystemPrinter3(ByVal strPrinterName As String) As Boolean
    '    'this method does not valid if the change is correct and does not revert to previous printer if wrong
    '    Dim DeviceLine As String

    '    'rebuild a valid device line string 
    '    DeviceLine = strPrinterName & ",,"

    '    'Store the new printer information in the 
    '    '[WINDOWS] section of the WIN.INI file for 
    '    'the DEVICE= item 
    '    Call WriteProfileString("windows", "Device", DeviceLine)

    '    'Cause all applications to reload the INI file 
    '    Call SendMessage(HWND_BROADCAST, WM_WININICHANGE, 0, "windows")

    '    Return True
    'End Function

#End Region

End Class

