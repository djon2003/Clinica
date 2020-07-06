Imports System
Imports SHDocVw
Imports System.Runtime.InteropServices
Imports mshtml

<ComImport(), Guid("C4D244B0-D43E-11CF-893B-00AA00BDCE1A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
  Public Interface IDocHostShowUI
    <PreserveSig()> _
    Function showMessage(ByVal hwnd As IntPtr, <MarshalAs(UnmanagedType.BStr)> ByVal lpstrText As String, <MarshalAs(UnmanagedType.BStr)> ByVal lpstrCaption As String, ByVal dwType As Integer, <MarshalAs(UnmanagedType.BStr)> ByVal lpstrHelpFile As String, ByVal dwHelpContext As Integer, ByRef lpResult As Integer) As Integer

    <PreserveSig()> _
    Function showHelp(ByVal hwnd As IntPtr, <MarshalAs(UnmanagedType.BStr)> ByVal pszHelpFile As String, ByVal uCommand As Integer, ByVal dwData As Integer, ByVal ptMouse As tagPOINT, <MarshalAs(UnmanagedType.IDispatch)> ByVal pDispatchObjectHit As Object) As Integer

End Interface
