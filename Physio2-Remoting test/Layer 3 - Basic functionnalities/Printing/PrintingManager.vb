Imports System.Drawing.Printing

Public Class PrintingHelper

    Private Shared _DefHeader As String = ""
    Private Shared _DefFooter As String = "&bPage &p / &P"
    Private Shared sysDefPrinter As String = ""
    Private Shared defBrowser As New Base.Windows.Forms.WebControl
    Private Shared _PrintingInfos As New PrintingInfos(_DefHeader, _DefFooter, -1, -1, -1, -1)


#Region "Propriétés"
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

    Public Shared Sub printHtml(ByVal html As String, Optional ByVal promptUser As Boolean = True, Optional ByVal waitForSpooling As Boolean = False, Optional ByVal printerName As String = "", Optional ByVal printingWebControl As Base.Windows.Forms.WebControl = Nothing, Optional ByVal setBackSysDefPrinter As Boolean = True)
        Dim controlCreated As Boolean = False
        If printingWebControl Is Nothing Then
            printingWebControl = New Base.Windows.Forms.WebControl
            controlCreated = True
        End If

        printingWebControl.waitForDoc()
        printingWebControl.sethtml(html)
        printingWebControl.waitForDoc()

        printHtml(printingWebControl, promptUser, waitForSpooling, printerName, setBackSysDefPrinter)

        If controlCreated Then printingWebControl.Dispose()
    End Sub

    Public Shared Sub printHtml(ByVal printingWebControl As Base.Windows.Forms.WebControl, Optional ByVal promptUser As Boolean = True, Optional ByVal waitForSpooling As Boolean = False, Optional ByVal printerName As String = "", Optional ByVal setBackSysDefPrinter As Boolean = True)
        Dim tmp As Long = Now.Ticks
        Dim curSysDefPrinter As String = ""
        'REM Parameter desactivated
        'If PromptUser = False Then
        curSysDefPrinter = systemDefautPrinter
        Dim curPrinter As String = PreferencesManager.getGeneralPreferences()("DefPrinter")
        If printerName <> "" Then curPrinter = printerName
        If curPrinter <> "" Then
            PrintingHelper.setDefaultSystemPrinter(curPrinter)
            waitForSpooling = True
        Else
            curSysDefPrinter = ""
        End If
        'End If
        If currentUserName = "Administrateur" Then myMainWin.StatusText = "Sub time0:" & ((Now.Ticks - tmp) / 10000000).ToString : tmp = Now.Ticks

        Dim controlCreated As Boolean = False
        If printingWebControl Is Nothing Then
            printingWebControl = New Base.Windows.Forms.WebControl
            controlCreated = True
        End If
        If currentUserName = "Administrateur" Then myMainWin.StatusText = "Sub time1:" & ((Now.Ticks - tmp) / 10000000).ToString : tmp = Now.Ticks

        PrintingHelper._PrintingInfos.setToRegistry() 'Change les marges, l'en-tête/pied de page, printBackground,shrinkToFit

        If currentUserName = "Administrateur" Then myMainWin.StatusText = "Sub time2:" & ((Now.Ticks - tmp) / 10000000).ToString : tmp = Now.Ticks
        printingWebControl.print(promptUser, waitForSpooling)
        If currentUserName = "Administrateur" Then myMainWin.StatusText = "Sub time3:" & ((Now.Ticks - tmp) / 10000000).ToString : tmp = Now.Ticks

        If curSysDefPrinter <> "" And setBackSysDefPrinter Then PrintingHelper.setDefaultSystemPrinter(sysDefPrinter)
        If controlCreated Then printingWebControl.Dispose()

        PrintingHelper._PrintingInfos.resetOriginalValues()

        If currentUserName = "Administrateur" Then myMainWin.StatusText = "Sub time4:" & ((Now.Ticks - tmp) / 10000000).ToString
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

