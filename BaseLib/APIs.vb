Option Strict Off
Option Explicit On
Imports System.Runtime.InteropServices
Module APIs

    Private Const WM_SETREDRAW As Integer = 11

    'Public Declare Function SendMessage Lib "user32" (ByVal hWnd As IntPtr, ByVal wMsg As Int32, ByVal wParam As Boolean, ByVal lParam As Int32) As Integer
    Public Declare Auto Function SendMessage Lib "user32" ( _
    ByVal hWnd As IntPtr, _
    ByVal wMsg As Integer, _
    ByVal wParam As IntPtr, _
    ByVal lParam As IntPtr _
 ) As Integer

    Public Sub SuspendDrawing(ByVal parent As Control)
        SendMessage(parent.Handle, WM_SETREDRAW, False, 0)
    End Sub

    Public Sub ResumeDrawing(ByVal parent As Control)
        SendMessage(parent.Handle, WM_SETREDRAW, True, 0)
        parent.Refresh()
    End Sub

End Module
