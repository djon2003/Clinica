Option Strict Off
Option Explicit On
Imports System.Runtime.InteropServices
Module APIs

    Public Declare Function ShowScrollBar Lib "user32" (ByVal hWnd As System.IntPtr, ByVal wBar As Integer, ByVal bShow As Boolean) As Boolean
    Public Declare Function ShowWindowAsync Lib "user32" (ByVal hWnd As IntPtr, ByVal nCmdShow As Integer) As Integer

    Public Enum Activations
        NoneToActivate = 0
        Activated = 1
        NotActivated = 2
    End Enum

    Function activatePrevInstance(ByVal argStrAppToFind As String, Optional ByVal endCurrentOne As Boolean = True) As Activations
        Dim prevHndl As IntPtr
        Dim prevProcess As Process
        Dim result As Long

        For Each objProcess As Process In Process.GetProcesses()
            If objProcess.ProcessName.ToUpper = argStrAppToFind.ToUpper And objProcess.Id.Equals(Process.GetCurrentProcess.Id) = False Then
                prevHndl = objProcess.Handle
                prevProcess = objProcess
                If prevHndl.ToInt32 > 0 Then Exit For
            End If
        Next
        If prevHndl.ToInt32 = 0 Or prevProcess Is Nothing Then Return Activations.NoneToActivate

        Dim returning As Activations = Activations.NotActivated
        If prevProcess.MainWindowHandle.ToInt32 <> 0 Then
            result = ShowWindowAsync(prevProcess.MainWindowHandle, 2)
            result = ShowWindowAsync(prevProcess.MainWindowHandle, 9)
            returning = Activations.Activated
            endCurrentOne = True
        Else
            Dim msg As String = "Le programme " & argStrAppToFind & " est déjà exécuté dans une autre session sur le même ordinateur."
            If Not endCurrentOne Then msg &= " Désirez-vous tenter une déconnexion de l'autre " & argStrAppToFind & " ?"
            If MessageBox.Show(msg, "Programme en cours d'exécution", If(endCurrentOne, MessageBoxButtons.OK, MessageBoxButtons.YesNo), If(endCurrentOne, MessageBoxIcon.Error, MessageBoxIcon.Question)) = DialogResult.No Then endCurrentOne = True
        End If
        If endCurrentOne Then End

        Return returning
    End Function

#Region "FindExecutable"
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Copyright ©1996-2009 VBnet, Randy Birch, All Rights Reserved.
    ' Some pages may also contain other copyrights by the author.
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Distribution: You can freely use this code in your own
    '               applications, but you may not reproduce 
    '               or publish this code on any web site,
    '               online service, or distribute as source 
    '               on any media without express permission.
    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    Private Declare Function FindExecutable Lib "shell32" _
       Alias "FindExecutableA" _
      (ByVal lpFile As String, _
       ByVal lpDirectory As String, _
       ByVal sResult As String) As Long

    Private Const max_path As Long = 260
    Private Const error_file_no_association As Long = 31
    Private Const error_file_not_found As Long = 2
    Private Const error_path_not_found As Long = 3
    Private Const error_file_success As Long = 32 'my constant
    Private Const error_bad_format As Long = 11

    Public Function getExeAssociatedWithExtension(ByVal path As String, ByVal fileName As String) As String

        Dim success As Long
        Dim pos As Long
        Dim sResult As String
        Dim msg As String

        sResult = Space$(max_path)

        'lpFile: name of the file of interest
        'lpDirectory: location of lpFile
        'sResult: path and name of executable associated with lpFile
        success = FindExecutable(fileName, path & bar(path), sResult)

        Select Case success
            Case error_file_no_association : msg = ""
            Case error_file_not_found : msg = ""
            Case error_path_not_found : msg = ""
            Case error_bad_format : msg = ""

            Case Is >= error_file_success

                pos = InStr(sResult, Chr(0))

                If pos Then
                    msg = Left$(sResult, pos - 1)
                End If

        End Select

        Return msg
    End Function
#End Region

#Region "Mouse clicking"
    Const MOUSEEVENTF_ABSOLUTE As Short = &H8000S
    Const MOUSEEVENTF_LEFTDOWN As Short = &H2S
    Const MOUSEEVENTF_LEFTUP As Short = &H4S
    Const MOUSEEVENTF_MIDDLEDOWN As Short = &H20S
    Const MOUSEEVENTF_MIDDLEUP As Short = &H40S
    Const MOUSEEVENTF_MOVE As Short = &H1S
    Const MOUSEEVENTF_RIGHTDOWN As Short = &H8S
    Const MOUSEEVENTF_RIGHTUP As Short = &H10S
    Const MOUSEEVENTF_WHEEL As Short = &H80S
    Const MOUSEEVENTF_XDOWN As Short = &H100S
    Const MOUSEEVENTF_XUP As Short = &H200S
    Const WHEEL_DELTA As Short = 120
    Const XBUTTON1 As Short = &H1S
    Const XBUTTON2 As Short = &H2S

    Structure POINT_TYPE
        Dim x As Integer
        Dim y As Integer
    End Structure

    Public Declare Sub mouse_event Lib "user32" (ByVal dwFlags As Integer, ByVal dx As Integer, ByVal dy As Integer, ByVal cButtons As Integer, ByVal dwExtraInfo As Integer)
    Public Declare Function SetCursorPos Lib "user32.dll" (ByVal x As Integer, ByVal y As Integer) As Integer
    Public Declare Function GetCursorPos Lib "user32.dll" (ByRef lpPoint As POINT_TYPE) As Integer


    Public Function mouse(ByRef action As Object, Optional ByRef x As Object = Nothing, Optional ByRef y As Object = Nothing, Optional ByRef button As Object = Nothing) As Object
        Dim coord As POINT_TYPE

        If Not x Is Nothing And Not y Is Nothing Then SetCursorPos(x, y)
        Select Case action.ToString.ToUpper
            Case "CLICK"
                Select Case button.ToString.ToUpper
                    Case "LEFT"
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0)
                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0)
                    Case "RIGHT"
                        mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0)
                        mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0)
                End Select
            Case "POSITIONX"
                GetCursorPos(coord)
                mouse = coord.x
            Case "POSITIONY"
                GetCursorPos(coord)
                mouse = coord.y
        End Select

        Return 0
    End Function
#End Region
End Module
