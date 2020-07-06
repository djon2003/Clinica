Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Diagnostics.SymbolStore
Imports System.Runtime.InteropServices
Imports System.Reflection

' Use MDbg's managed wrappers over the corysm.idl (diasymreader.dll) COM APIs
' Must reference MDbgCore.dll from the .NET SDK or corapi.dll from the MDbg sample: 
' http://www.microsoft.com/downloads/details.aspx?familyid=38449a42-6b7a-4e28-80ce-c55645ab1310&displaylang=en
Imports Microsoft.Samples.Debugging.CorSymbolStore

''' <summary>
''' A class for producing stack traces with file and line number information using custom PDB
''' lookup logic.
''' </summary>
''' <remarks>
''' The CLR's StackTrace class will only load PDBs that are next to their corresponding module (or
''' in a few other standard locations like the _NT_SYMBOL_PATH environment variable and system directory).
''' PDBs are considered a development-time-only scenario (not intended for use in production), and so usually 
''' are directly next to the image.  However, this is sometimes too restrictive for some development/testing 
''' scenarios.  Use this class to get stack traces with full source info when you want to find PDBs elsewhere, 
''' such as in specific paths or on a symbol server.
''' 
''' An alternate (often superior) approach that could be taken using the same basic code would be to
''' save the stack traces in a computer readable form (XML perhaps) with module names, method tokens
''' and IL offsets.  Then build a tool that takes this as input and after-the-fact loads PDBs to
''' create a full stack trace with source information.  The main benefit of this is that it avoids
''' having to make your PDBs available to all the test machines running your code.
''' 
''' For error-reporting and diagnosis scenarios in production, Microsoft suggests the use of Windows
''' Error Reporting (https://winqual.microsoft.com/).  
'''  
''' Note that some of this code is adapted from http://blogs.msdn.com/jmstall/pages/sample-pdb2xml.aspx
''' </remarks>
Public Class StackTraceSymbolProvider
    ''' <summary>
    ''' Create a new instance with a specified policy for finding symbols
    ''' </summary>
    ''' <param name="searchPath">A semi-colon separated list of additional paths to check</param>
    Public Sub New(ByVal searchPath As String)
        Me.New(searchPath, SymSearchPolicies.AllowReferencePathAccess)
    End Sub


    Public Sub New(ByVal searchPath As String, ByVal searchPolicy As SymSearchPolicies)
        If searchPolicy = Nothing Then searchPolicy = SymSearchPolicies.AllowReferencePathAccess

        m_searchPath = searchPath
        m_searchPolicy = searchPolicy

        ' Create a COM Metadata dispenser to use for all modules
        Dim dispenserClassID As New Guid(&HE5CB7A31UI, &H7512, &H11D2, &H89, &HCE, &H0, _
         &H80, &HC7, &H92, &HE5, &HD8)
        ' CLSID_CorMetaDataDispenser
        Dim dispenserIID As New Guid(&H809C652EUI, &H7396, &H11D2, &H97, &H71, &H0, _
         &HA0, &HC9, &HB4, &HD5, &HC)
        ' IID_IMetaDataDispenser
        Dim objDispenser As Object
        CoCreateInstance(dispenserClassID, Nothing, 1, dispenserIID, objDispenser)
        m_metadataDispenser = DirectCast(objDispenser, IMetaDataDispenser)

        ' Create a binder from MDbg's wrappers over ISymUnmanagedBinder2
        m_symBinder = New SymbolBinder()
    End Sub

    ''' <summary>
    ''' Create a symbol reader object corresponding to the specified module (DLL/EXE)
    ''' </summary>
    ''' <param name="modulePath">Full path to the module of interest</param>
    ''' <returns>A symbol reader object, or null if no matching PDB symbols can located</returns>
    Private Function CreateSymbolReaderForFile(ByVal modulePath As String) As ISymbolReader
        ' First we need to get a metadata importer for the module to provide to the symbol reader
        ' This is basically the same as MDbg's SymbolAccess.GetReaderForFile method, except that it
        ' unfortunately does not have an overload that allows us to provide the searchPolicies
        Dim importerIID As New Guid(&H7DAC8207, &HD3AE, &H4C75, &H9B, &H67, &H92, _
         &H80, &H1A, &H49, &H7D, &H44)
        ' IID_IMetaDataImport
        ' Open an Importer on the given filename. We'll end up passing this importer straight
        ' through to the Binder.
        Dim objImporter As Object
        m_metadataDispenser.OpenScope(modulePath, 0, importerIID, objImporter)

        ' Call ISymUnmanagedBinder2.GetReaderForFile2 to load the PDB file (if any)
        ' Note that ultimately how this PDB file is located is determined by
        ' IDiaDataSource::loadDataForExe.  See the DIA SDK documentation for details.
        Dim reader As ISymbolReader = m_symBinder.GetReaderForFile(objImporter, modulePath, m_searchPath, m_searchPolicy)
        Return reader
    End Function

    ''' <summary>
    ''' Get or create a symbol reader for the specified module (caching the result)
    ''' </summary>
    ''' <param name="modulePath">Full path to the module of interest</param>
    ''' <returns>A symbol reader for the specified module or null if none could be found</returns>
    Private Function GetSymbolReaderForFile(ByVal modulePath As String) As ISymbolReader
        Dim reader As ISymbolReader
        If Not m_symReaders.TryGetValue(modulePath, reader) Then
            reader = CreateSymbolReaderForFile(modulePath)
            m_symReaders.Add(modulePath, reader)
        End If
        Return reader
    End Function

    ''' <summary>
    ''' Get a texual representing of the supplied stack trace including source file names
    ''' and line numbers, using the PDB lookup options supplied at construction.
    ''' </summary>
    ''' <param name="stackTrace">The stack trace to convert to text</param>
    ''' <returns>A string in a format similar to StackTrace.ToString but whith file names and
    ''' line numbers even when they're not available to the built-in StackTrace class.</returns>
    Public Function StackTraceToStringWithSourceInfo(ByVal stackTrace As StackTrace) As String
        Dim sb As New System.Text.StringBuilder()

        For Each stackFrame As StackFrame In stackTrace.GetFrames()
            Dim method As MethodBase = stackFrame.GetMethod()

            ' Format the stack trace line similarily to how the built-in StackTrace class does.
            ' Some differences (simplifications here): generics, nested types, argument names
            Dim methodString As String = method.ToString()
            ' this is "RetType FuncName(args)
            Dim sig As String = [String].Format("  at {0}.{1}", method.DeclaringType.FullName, methodString.Substring(methodString.IndexOf(" "c) + 1))

            ' Append source location information if we can find PDBs
            Dim sourceLoc As String = GetSourceLoc(method, stackFrame.GetILOffset())
            If sourceLoc IsNot Nothing Then
                sig += " in " & sourceLoc
            End If

            sb.AppendLine(sig)
        Next
        Return sb.ToString()
    End Function

    ''' <summary>
    ''' Get a string representing the source location for the given IL offset and method
    ''' </summary>
    ''' <param name="method">The method of interest</param>
    ''' <param name="ilOffset">The offset into the IL</param>
    ''' <returns>Line number of method</returns>
    Public Function GetSourceLoc(ByVal method As MethodBase, ByVal ilOffset As Integer) As Integer
        ' Get the symbol reader corresponding to the module of the supplied method
        Dim modulePath As String = method.[Module].FullyQualifiedName
        Dim symReader As ISymbolReader = GetSymbolReaderForFile(modulePath)
        If symReader Is Nothing Then
            Return Nothing
        End If
        ' no matching PDB found
        Dim symMethod As ISymbolMethod = symReader.GetMethod(New SymbolToken(method.MetadataToken))

        ' Get all the sequence points for the method
        Dim docs As ISymbolDocument() = New ISymbolDocument(symMethod.SequencePointCount - 1) {}
        Dim lineNumbers As Integer() = New Integer(symMethod.SequencePointCount - 1) {}
        Dim ilOffsets As Integer() = New Integer(symMethod.SequencePointCount - 1) {}
        symMethod.GetSequencePoints(ilOffsets, docs, lineNumbers, Nothing, Nothing, Nothing)

        ' Find the closest sequence point to the requested offset
        ' Sequence points are returned sorted by offset so we're looking for the last one with
        ' an offset less than or equal to the requested offset. 
        ' Note that this won't necessarily match the real source location exactly if 
        ' the code was jit-compiled with optimizations.
        Dim i As Integer
        For i = 0 To symMethod.SequencePointCount - 1
            If ilOffsets(i) > ilOffset Then
                Exit For
            End If
        Next
        ' Found the first mismatch, back up if it wasn't the first
        If i > 0 Then
            i -= 1
        End If

        ' Now return the source file and line number for this sequence point
        Return lineNumbers(i)
    End Function

    ' We could easily add other APIs similar to those available on StackTrace (and StackFrame)

    Private m_metadataDispenser As IMetaDataDispenser
    Private m_symBinder As SymbolBinder
    Private m_searchPath As String
    Private m_searchPolicy As SymSearchPolicies

    ' Map from module path to symbol reader
    Private m_symReaders As New Dictionary(Of String, ISymbolReader)()

#Region "Metadata Imports"

    ' Bare bones COM-interop definition of the IMetaDataDispenser API
    <Guid("809c652e-7396-11d2-9771-00a0c9b4d50c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
    <ComVisible(True)> _
    Private Interface IMetaDataDispenser
        ' We need to be able to call OpenScope, which is the 2nd vtable slot.
        ' Thus we need this one placeholder here to occupy the first slot..
        Sub DefineScope_Placeholder()

        'STDMETHOD(OpenScope)(                   // Return code.
        'LPCWSTR     szScope,                // [in] The scope to open.
        '  DWORD       dwOpenFlags,            // [in] Open mode flags.
        '  REFIID      riid,                   // [in] The interface desired.
        '  IUnknown    **ppIUnk) PURE;         // [out] Return interface on success.
        Sub OpenScope(<[In](), MarshalAs(UnmanagedType.LPWStr)> ByVal szScope As [String], <[In]()> ByVal dwOpenFlags As Int32, <[In]()> ByRef riid As Guid, <Out(), MarshalAs(UnmanagedType.IUnknown)> ByRef punk As [Object])

        ' Don't need any other methods.
    End Interface

    ' Since we're just blindly passing this interface through managed code to the Symbinder, we don't care about actually
    ' importing the specific methods.
    ' This needs to be public so that we can call Marshal.GetComInterfaceForObject() on it to get the
    ' underlying metadata pointer.
    <Guid("7DAC8207-D3AE-4c75-9B67-92801A497D44"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
    <ComVisible(True)> _
    Public Interface IMetadataImport
        ' Just need a single placeholder method so that it doesn't complain about an empty interface.
        Sub Placeholder()
    End Interface
#End Region

    <DllImport("ole32.dll")> _
    Private Shared Function CoCreateInstance(<[In]()> ByRef rclsid As Guid, <[In](), MarshalAs(UnmanagedType.IUnknown)> ByVal pUnkOuter As [Object], <[In]()> ByVal dwClsContext As UInteger, <[In]()> ByRef riid As Guid, <Out(), MarshalAs(UnmanagedType.[Interface])> ByRef ppv As [Object]) As Integer
    End Function

End Class