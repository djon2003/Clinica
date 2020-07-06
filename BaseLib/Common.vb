Public Module Common

    Public Const LIMIT_DATE As Date = #1/27/9999#

    Private Const WM_SETREDRAW As Integer = 11

    ''' <summary>
    ''' A stronger "SuspendLayout" completely holds the controls painting until ResumePaint is called
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Public Sub SuspendPaint(ByVal ctrl As Control)

        Dim msgSuspendUpdate As Message = Message.Create(ctrl.Handle, WM_SETREDRAW, System.IntPtr.Zero, System.IntPtr.Zero)

        Dim window As NativeWindow = NativeWindow.FromHandle(ctrl.Handle)

        window.DefWndProc(msgSuspendUpdate)

    End Sub

    ''' <summary>
    ''' Resume from SuspendPaint method
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <remarks></remarks>
    Public Sub ResumePaint(ByVal ctrl As Control)

        Dim wparam As New System.IntPtr(1)
        Dim msgResumeUpdate As Message = Message.Create(ctrl.Handle, WM_SETREDRAW, wparam, System.IntPtr.Zero)

        Dim window As NativeWindow = NativeWindow.FromHandle(ctrl.Handle)

        window.DefWndProc(msgResumeUpdate)

        ctrl.Invalidate()

    End Sub

    Public Function convertToType(ByVal value As Object, ByVal toType As Type) As Object
        Select Case toType.Name
            Case GetType(Boolean).Name
                value = Convert.ToBoolean(value)
            Case GetType(Byte).Name
                value = Convert.ToByte(value)
            Case GetType(Char).Name
                value = Convert.ToChar(value)
            Case GetType(DateTime).Name
                value = Convert.ToDateTime(value)
            Case GetType(Decimal).Name
                value = Convert.ToDecimal(value)
            Case GetType(Double).Name
                value = Convert.ToDouble(value)
            Case GetType(Int16).Name
                value = Convert.ToInt16(value)
            Case GetType(Int32).Name
                value = Convert.ToInt32(value)
            Case GetType(Int64).Name
                value = Convert.ToInt64(value)
            Case GetType(SByte).Name
                value = Convert.ToSByte(value)
            Case GetType(Single).Name
                value = Convert.ToSingle(value)
            Case GetType(String).Name
                value = Convert.ToString(value)
            Case GetType(UInt16).Name
                value = Convert.ToUInt16(value)
            Case GetType(UInt32).Name
                value = Convert.ToUInt32(value)
            Case GetType(UInt64).Name
                value = Convert.ToUInt64(value)
        End Select

        Return value
    End Function

    Public Function addSlash(ByVal path As String) As String
        If path = "" Then Return ""
        If path.EndsWith("\") = False Then Return "\"

        Return ""
    End Function

    Public Function isConnectionAvailable() As Boolean
        Dim objUrl As New System.Uri("http://www.physiotech.ca/")
        Dim objWebReq As System.Net.WebRequest
        Dim objResp As System.Net.WebResponse = Nothing
        Try
            objWebReq = System.Net.WebRequest.Create(objUrl)
            ' Attempt to get response and return True
            objResp = objWebReq.GetResponse
            objResp.Close()
            objWebReq = Nothing
            Return True
        Catch ex As Exception
            ' Error, exit and return False
            If objResp IsNot Nothing Then objResp.Close()
            objWebReq = Nothing
            Return False
        End Try
    End Function
End Module
