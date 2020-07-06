Public Class PrintingJob
    Private _html As String = String.Empty
    Private _htmlChanged As Boolean = False
    Private startedTime As Date = Date.Now

    Public jobStatus As PrintingHelper.JobStatuses = PrintingHelper.JobStatuses.InQueue
    Public jobTitle As String = String.Empty
    Public promptUser As Boolean = True
    Public waitForSpooling As Boolean = False
    Public printerName As String = ""
    Public setBackSysDefPrinter As Boolean = True


#Region "Properties"
    Public ReadOnly Property startedAt() As Date
        Get
            Return startedTime
        End Get
    End Property

    Public Property html() As String
        Get
            Return _html
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = String.Empty

            If value <> _html Then
                _html = value
                _htmlChanged = True
            End If
        End Set
    End Property
#End Region



    Public Sub New()

    End Sub

    Private ReadOnly Property printingForm() As PrintingForm
        Get
            Return CI.Clinica.PrintingForm.getInstance()
        End Get
    End Property


    Public Sub setPrintingFocus()
        printingForm.Focus()
    End Sub

    Public Function getPrintingForm() As PrintingForm
        If _htmlChanged Then
            printingForm.Text = jobTitle
            printingForm.setHtml(html)
            _htmlChanged = False
        End If
        Return printingForm
    End Function
End Class