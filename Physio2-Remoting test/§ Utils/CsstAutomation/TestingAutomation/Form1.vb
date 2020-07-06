Imports CI.Base

Public Class Form1

    Private plugin As PluginTaskBase = New CI.CsstAutomation.Plugin()
    Private creationTask As TaskBase

    Private Sub creationTask_StepChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me.InvokeRequired Then
            Me.Invoke(New EventHandler(AddressOf creationTask_StepChanged), New Object() {sender, e})
        Else
            taskStep.Text = creationTask.currentStepName & " (" & creationTask.currentStep & " / " & creationTask.maximumSteps & ")"
        End If
    End Sub

    Private Sub creationTask_TaskEnded(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me.InvokeRequired Then
            Me.Invoke(New EventHandler(AddressOf creationTask_TaskEnded), New Object() {sender, e})
        Else
            taskStep.Text = creationTask.currentStepName & " (" & creationTask.currentStep & " / " & creationTask.maximumSteps & ")"
        End If
    End Sub

    Private Sub creationTask_TaskStepProgressed(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me.InvokeRequired Then
            Me.Invoke(New EventHandler(AddressOf creationTask_TaskStepProgressed), New Object() {sender, e})
        Else
            Dim progression As Double = creationTask.getTaskProgession()
            If progression = -1 Then
                ProgressBar1.Visible = False
            Else
                ProgressBar1.Visible = True
                If progression > 100 Then progression = 100

                ProgressBar1.Value = progression
                taskPourcent.Text = progression & " %"
            End If
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        creationTask.startTask()

        With CType(creationTask, CI.CsstAutomation.CsstTask)
            '.createFilesUploader()
            '.uploadFiles()
            ' .checkReturn()
        End With
    End Sub

    Public Sub New()

        ' Cet appel est requis par le Concepteur Windows Form.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        creationTask = plugin.createTask()
        AddHandler creationTask.stepChanged, AddressOf creationTask_StepChanged
        AddHandler creationTask.taskStepProgressed, AddressOf creationTask_TaskStepProgressed
        AddHandler creationTask.taskEnded, AddressOf creationTask_TaskEnded
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CI.Base.External.setCurrentExternal(New EmptyBaseLibExternal())

        'loading()
    End Sub

    Private Sub loading()

        leg.Enabled = False
        pat.Enabled = False
        local.Enabled = False
        physiolanaud.Enabled = False
        cmlocal.Enabled = False

        If DBLinker.getInstance.dbConnected Then DBLinker.getInstance.dbConnected = False
        If local.Checked Then
            'LOCAL
            DBLinker.getInstance.initConnection(".\SQLEXPRESS", "ClinicaDev", "", "")
            IO.File.Copy("CI.CsstAutomation.config.local.xml", "CI.CsstAutomation.Config.xml", True)
        ElseIf leg.Checked Then
            'LEG
            DBLinker.getInstance.initConnection("cp02.physiotech-lg.local", 1433, "Clinica", "adminLEG", "vpnLEG155")
            IO.File.Copy("CI.CsstAutomation.config.leg.xml", "CI.CsstAutomation.Config.xml", True)
        ElseIf pat.Checked Then
            'PAT
            DBLinker.getInstance.initConnection("cp03", 1433, "Clinica", "adminPAT", "vpnPAT13605")
            IO.File.Copy("CI.CsstAutomation.config.pat.xml", "CI.CsstAutomation.Config.xml", True)
        ElseIf cmlocal.Checked Then
            'Clinique Médicale St-André
            DBLinker.getInstance.initConnection(".\SQLEXPRESS", "ClinicaCMSA", "", "")
            IO.File.Copy("CI.CsstAutomation.config.cmsa-local.xml", "CI.CsstAutomation.Config.xml", True)
        ElseIf physiolanaud.Checked Then
            'Physio Lanaudière
            If MessageBox.Show("Utilisez la version local ?", "Local", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes Then
                DBLinker.getInstance.initConnection(".\SQLEXPRESS", "ClinicaST", "", "")
            Else
                DBLinker.getInstance.initConnection("24.201.38.200", 1433, "Clinica", "sa", "clinicamdp")
            End If
            IO.File.Copy("CI.CsstAutomation.config.st.xml", "CI.CsstAutomation.Config.xml", True)
        End If

        DBLinker.getInstance.dbConnected = True

        plugin.initialize() 'This shall be called by the plugintasksmanager

        ConfigurationsManager.getInstance.load()
        ConfigurationsManager.getInstance.ensureConfigsUpToDate()

        Button1.Enabled = True
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()

        DBLinker.getInstance.dbConnected = False
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim nam As String = InputBox("NAM", "NAM", "BOIJ83072718")
        If nam = "" Then Exit Sub

        If charMatrix.Count = 0 Then
            For i As Integer = 0 To charKeys.Length - 1
                charMatrix.Add(charKeys(i), charValues(i))
            Next i
        End If

        Dim validator As Integer = createNAMValidationNumber(nam)
        MsgBox("NAM validation " & If(validator = nam.Substring(11), "pass", "fail") & " : Real = " & nam.Substring(11) & " , New = " & validator)
    End Sub

    Private charMatrix As New Generic.Dictionary(Of String, Integer)
    Private charKeys() As String = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"}
    Private charValues() As Integer = {193, 194, 195, 196, 197, 198, 199, 200, 201, 209, 210, 211, 212, 213, 214, 215, 216, 217, 226, 227, 228, 229, 230, 231, 232, 233, 240, 241, 242, 243, 244, 245, 246, 247, 248, 249}
    Private partsMultipler() As Integer = {137, 9, 17, 13, 4, 57, 69, 1}

    Private Function createNAMValidationNumber(ByVal nam As String) As Integer
        nam = nam.Substring(0, 11).ToUpper()
        Dim parts(7) As String
        parts(0) = nam.Substring(0, 3)
        parts(1) = nam.Substring(3, 1)
        Dim namYear As String = nam.Substring(4, 2)
        If namYear >= Date.Now.Year.ToString.Substring(2, 2) Then
            parts(2) = 19
        Else
            parts(2) = 18
        End If
        parts(3) = namYear
        Dim namMonth As Integer = nam.Substring(6, 2)
        parts(4) = If(namMonth > 50, "F", "H")
        parts(5) = (namMonth Mod 50).ToString("D2")
        Dim namDay As Integer = nam.Substring(8, 2)
        parts(6) = (namDay Mod 50).ToString("D2")
        parts(7) = nam.Substring(10, 1)

        Dim sum As Long = 0
        For i As Integer = 0 To 7
            Dim converted As String = ""
            'If i < 6 Then
            For j As Integer = 0 To parts(i).Length - 1
                converted &= charMatrix(parts(i).Chars(j))
            Next j
            'Else
            '    converted = parts(i)
            'End If
            If converted = "" Then converted = 0
            sum += converted * partsMultipler(i)
        Next i

        Return sum Mod 10
    End Function

    Private Sub configs_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles leg.CheckedChanged, pat.CheckedChanged, local.CheckedChanged, physiolanaud.CheckedChanged, cmlocal.CheckedChanged
        If CType(sender, RadioButton).Checked Then loading()
    End Sub
End Class
