Imports CI.Base

Public Class DummyTask
    Inherits CI.Base.TaskBase

    Private taskThread As Threading.Thread
    Private errorToThrow As Exception = Nothing
    Private Shared stepNames() As String = New String() {"Memory test", "Transaction test", "Long task (10s)"}

    Public Sub New(ByVal creator As CI.Base.PluginTaskBase)
        MyBase.New(creator)
    End Sub

    Protected Overrides Sub gotoNextStep()
        If currentStep >= stepNames.Length Then Exit Sub

        setCurrentStepName(stepNames(currentStep))

        MyBase.gotoNextStep()
    End Sub

    Private memoryTestDone As Boolean = False
    Private sb As New System.Text.StringBuilder()

    Private Sub doMemoryTest()
        Dim t As New System.Threading.Thread(AddressOf memoryTestLoop)
        t.Start()
        t.Join()

        If exToThrow IsNot Nothing Then Throw exToThrow
    End Sub

    Private exToThrow As Exception

    Private Sub memoryTestLoop()
        'exToThrow = New Exception("TEST")
        'Dim s As String = String.empty
        'While True
        '    s &= "1"
        '    IO.File.AppendAllText("c:\TEMP\TEST.LOG", "1")
        'End While
    End Sub

    Private Sub __doTask()
        Try
            _doTask()
        Catch ex As Exception
            errorToThrow = ex
        End Try
    End Sub

    Private Sub _doTask()
        gotoNextStep()

        If Config.current.doMemoryTest Then doMemoryTest()

        gotoNextStep()

        Throw New Exception("I'm here on line 59")

        'Test transaction
        If Config.current.noVisiteForTransactionTest <> 0 Then
            DBLinker.getInstance.beginTransaction()
            Dim oldValue() As String = DBLinker.getInstance.readOneDBField("InfoVisites", "ExternalStatus", "WHERE NoVisite=" & Config.current.noVisiteForTransactionTest)
            DBLinker.getInstance.updateDB("InfoVisites", "ExternalStatus=4", "NoVisite", Config.current.noVisiteForTransactionTest, False)
            MsgBox("Waiting to modify visite #" & Config.current.noVisiteForTransactionTest)
            Dim newValue() As String = DBLinker.getInstance.readOneDBField("InfoVisites", "ExternalStatus", "WHERE NoVisite=" & Config.current.noVisiteForTransactionTest)
            MsgBox("Old value : " & oldValue(0) & vbCrLf & "New value : " & newValue(0))
            If Config.current.actionForTransactionTest = Config.ActionTypes.Commit Then
                DBLinker.getInstance.commitTransaction()
            Else
                DBLinker.getInstance.rollbackTransaction()
            End If
        End If

        Dim out As New Text.StringBuilder()
        'Test long task
        gotoNextStep()
        For i As Integer = 1 To 100
            Threading.Thread.Sleep(100)
            setTaskProgession(i)
            out.Append("Testline " & i & "<BR/>")
        Next
        setTaskProgession(100)
        setNbProcessed(1)
        setResultHtml(Now.ToString() & "<br/>" & out.ToString())

        onTaskEnded(EventArgs.Empty)
    End Sub



    Protected Overrides Sub doTask()
        errorToThrow = Nothing

        taskThread = New Threading.Thread(AddressOf __doTask)
        If taskThread.TrySetApartmentState(Threading.ApartmentState.STA) = False Then
            setCurrentStepName("Error happened !")
            onTaskEnded(EventArgs.Empty)
            Exit Sub
        End If

        taskThread.IsBackground = True
        taskThread.Start()
        taskThread.Join()

        If errorToThrow IsNot Nothing Then Throw New Exception("Test", errorToThrow)
    End Sub

    Public Overrides ReadOnly Property maximumSteps() As Integer
        Get
            Return stepNames.Length
        End Get
    End Property

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
