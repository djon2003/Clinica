Imports System
Imports System.Text
Imports System.CodeDom.Compiler
Imports System.Reflection
Imports System.IO
Imports System.Diagnostics

Public Class cVBEvalProvider


    Private m_oCompilerErrors As CompilerErrorCollection
    Private _NbLinesAboveCode As Integer = 6

    Public ReadOnly Property nbLinesAboveCode() As Integer
        Get
            Return _NbLinesAboveCode
        End Get
    End Property

    Public Property compilerErrors() As CompilerErrorCollection
        Get
            Return m_oCompilerErrors
        End Get
        Set(ByVal Value As CompilerErrorCollection)
            m_oCompilerErrors = Value
        End Set
    End Property

    Public Sub New()
        MyBase.New()
        m_oCompilerErrors = New CompilerErrorCollection
    End Sub

    Public Function eval(ByVal vbCode As String, ByVal params() As Object) As Object
        Dim curMethodEval As MethodEvaluator = getMethodEvaluator(vbCode)
        If curMethodEval Is Nothing Then Return Nothing

        Return curMethodEval.eval(params)
    End Function

    <System.Diagnostics.DebuggerStepThrough()> _
    Public Function getMethodEvaluator(ByVal vbCode As String) As MethodEvaluator
        Dim retMethodEval As MethodEvaluator = Nothing

        Dim oCodeProvider As VBCodeProvider = New VBCodeProvider
        ' Obsolete in 2.0 framework
        ' Dim oICCompiler As ICodeCompiler = oCodeProvider.CreateCompiler

        Dim oCParams As CompilerParameters = New CompilerParameters
        Dim oCResults As CompilerResults
        Dim oAssy As System.Reflection.Assembly
        Dim oExecInstance As Object = Nothing

        Try

            ' Setup the Compiler Parameters  
            ' Add any referenced assemblies
            oCParams.ReferencedAssemblies.Add("system.dll")
            oCParams.ReferencedAssemblies.Add("system.xml.dll")
            oCParams.ReferencedAssemblies.Add("system.data.dll")
            oCParams.ReferencedAssemblies.Add("clinica.exe")
            oCParams.CompilerOptions = "/t:library"
            oCParams.GenerateInMemory = True

            ' Generate the Code Framework
            Dim sb As StringBuilder = New StringBuilder("")

            sb.Append("Imports System" & vbCrLf)
            sb.Append("Imports System.Xml" & vbCrLf)
            sb.Append("Imports System.Data" & vbCrLf)
            sb.Append("Imports CI.Clinica.ClinicaDyn" & vbCrLf)

            ' Build a little wrapper code, with our passed in code in the middle 
            'sb.Append("Namespace CI.Clinica" & vbCrLf)
            sb.Append("Namespace dValuate" & vbCrLf)
            sb.Append("Class EvalRunTime " & vbCrLf)
            sb.Append("Public Function EvaluateIt(ByVal params() As object) As Object " & vbCrLf)
            sb.Append(vbCode & vbCrLf)
            sb.Append("End Function " & vbCrLf)
            sb.Append("Public Sub writeStatus(StatusText As String)" & vbCrLf)
            sb.Append("ClinicaDyn.WriteStatus(StatusText)" & vbCrLf)
            sb.Append("End Sub" & vbCrLf)
            sb.Append("End Class " & vbCrLf)
            sb.Append("End Namespace" & vbCrLf)
            'sb.Append("End Namespace" & vbCrLf)
            Debug.WriteLine(sb.ToString())

            Try
                ' Compile and get results 
                ' 2.0 Framework - Method called from Code Provider
                oCResults = oCodeProvider.CompileAssemblyFromSource(oCParams, sb.ToString)
                ' 1.1 Framework - Method called from CodeCompiler Interface
                ' cr = oICCompiler.CompileAssemblyFromSource (cp, sb.ToString)


                ' Check for compile time errors 
                If oCResults.Errors.Count <> 0 Then

                    Me.compilerErrors = oCResults.Errors
                    Throw New Exception("Compile Errors")

                Else
                    ' No Errors On Compile, so continue to process...

                    oAssy = oCResults.CompiledAssembly
                    oExecInstance = oAssy.CreateInstance("dValuate.EvalRunTime")

                    Return New MethodEvaluator(oExecInstance, "EvaluateIt")
                End If

            Catch ex As Exception
                ' Compile Time Errors Are Caught Here
                ' Some other weird error 
                Debug.WriteLine(ex.Message)
                'Stop
            End Try

        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            'Stop
        End Try

        Return Nothing
    End Function

End Class
