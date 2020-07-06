
<Serializable()> _
Public Class ListItem
    Implements IListItem

    Private Sub New()
    End Sub

    Friend Sub New(ByVal associatedList As Controls.List)
        Me._List = associatedList
    End Sub

#Region "Définitions"
    Private Const iconSpacing As Integer = 2

    Private _icon As Icon
    Private _IconType As Icons.IconType = Icons.IconType.InList
    Private _List As Controls.List
    Private _Alignment As IListItem.AlignmentType = IListItem.AlignmentType.LeftA
    Private _BackColor As System.Drawing.Color = Color.White
    Private _Text As String = ""
    Private _TextSize As New Size(0, 0)
    Private _Font As New Font("Arial", 8)
    Private _ForeColor As System.Drawing.Color = Color.Black
    Private _ToolTipText As String = ""
    Private _ValueA As Object = Nothing
    Private _ValueB As Object = Nothing
    Private _ItemRectangle As New Rectangle(0, 0, 0, 0)
    Private _Clickable As Boolean = True
    Private hasToMeasureString As Boolean = False
    Private _IsSelected As Boolean = False
    Private _IconsShowed As New Generic.List(Of Boolean)
    Private _IconsPosition As Icons.IconPositions = Icons.IconPositions.DefinedByParent
#End Region

#Region "Propriétés"
    Public Property icon() As Icon
        Get
            Return _icon
        End Get
        Set(ByVal value As Icon)
            If _icon IsNot value Then
                _icon = value
                RaiseEvent shouldRedraw()
            End If
        End Set
    End Property

    Public Property iconType() As Icons.IconType Implements IListItem.IconType
        Get
            Return _IconType
        End Get
        Set(ByVal value As Icons.IconType)
            If _IconType <> value Then
                _IconType = value
                RaiseEvent shouldRedraw()
            End If
        End Set
    End Property

    Public Property iconsPosition() As Icons.IconPositions Implements IListItem.IconsPosition
        Get
            Return _IconsPosition
        End Get
        Set(ByVal value As Icons.IconPositions)
            If _IconsPosition <> value Then
                _IconsPosition = value
                RaiseEvent shouldRedraw()
            End If
        End Set
    End Property

    Public Property iconsShowed() As System.Collections.Generic.List(Of Boolean) Implements IListItem.IconsShowed
        Get
            Return _IconsShowed
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of Boolean))
            _IconsShowed = value
            RaiseEvent shouldRedraw()
        End Set
    End Property

    Public Property isSelected() As Boolean Implements IListItem.IsSelected
        Get
            Return _IsSelected
        End Get
        Set(ByVal value As Boolean)
            _IsSelected = value
        End Set
    End Property

    Public Property alignment() As IListItem.AlignmentType Implements IListItem.Alignment
        Get
            Return _Alignment
        End Get
        Set(ByVal value As IListItem.AlignmentType)
            If _Alignment <> value Then
                _Alignment = value
                RaiseEvent shouldRedraw()
            End If
        End Set
    End Property

    Public Property font() As Font Implements IListItem.Font
        Get
            Return _Font
        End Get
        Set(ByVal value As Font)
            If _Font.Equals(value) Then Exit Property

            _Font = value
            If _Text.Length = 0 Then
                _TextSize = New Size(0, 0)
            Else
                hasToMeasureString = True
            End If

            RaiseEvent shouldRedraw()
        End Set
    End Property

    Public Property backColor() As Drawing.Color Implements IListItem.BackColor
        Get
            Return _BackColor
        End Get
        Set(ByVal value As Drawing.Color)
            If _BackColor <> value Then
                _BackColor = value
                RaiseEvent shouldRedraw()
            End If
        End Set
    End Property

    Public Property foreColor() As Drawing.Color Implements IListItem.ForeColor
        Get
            Return _ForeColor
        End Get
        Set(ByVal value As Drawing.Color)
            If _ForeColor <> value Then
                _ForeColor = value
                RaiseEvent shouldRedraw()
            End If
        End Set
    End Property

    Public Property text() As String Implements IListItem.Text
        Get
            Return _Text
        End Get
        Set(ByVal value As String)
            If _Text <> value Then
                If value.Length = 0 Then
                    _TextSize = New Size(0, 0)
                ElseIf _Font IsNot Nothing Then
                    hasToMeasureString = True
                End If
                _Text = value
                RaiseEvent shouldRedraw()
            End If
        End Set
    End Property

    Public Property toolTipText() As String Implements IListItem.ToolTipText
        Get
            Return _ToolTipText
        End Get
        Set(ByVal value As String)
            _ToolTipText = value
        End Set
    End Property

    Public Property valueA() As Object Implements IListItem.ValueA
        Get
            Return _ValueA
        End Get
        Set(ByVal value As Object)
            Dim myList As Boolean = _List.Name IsNot Nothing AndAlso _List.Name = "MyList"
            Dim myControl As Boolean = _List.Parent IsNot Nothing AndAlso _List.Parent.Name IsNot Nothing AndAlso _List.Parent.Name = "FormOuvertes"
            If myList AndAlso myControl AndAlso value Is Nothing Then
                'REM BUG IN TESTING BELOW
                '                Date :2012-01-27 10:57:04
                'Version de Clinica : 1.2.1201.221
                'Ordinateur:     POSTE003()
                '                Utilisateur(Windows) : PHYSIOTECH(-LG / annieevemoreau)
                'Utilisateur Clinica : 1/80/Sasseville,Mélanie/110000110010111001100101001010000010001100000100001001011110100000000111010000010000000100001000010011100000
                'HasRestarted : False
                '                Error Type : System.Exception()
                'Message:
                'Application level exception

                'Exception Stack Trace :
                '                Trace(Not available)

                'Environment stack trace :
                '   at System.Environment.GetStackTrace(Exception e, Boolean needFileInfo)
                '                at(System.Environment.get_StackTrace())
                '   at CyberInternautes.Clinica.CommonProc.AddErrorLog(Exception ErrorMsg, Byte InternalCount) in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\CommProc\CommonProc.vb:line 497
                '   at CyberInternautes.Clinica.CommonProc.Application_ThreadException(Object sender, ThreadExceptionEventArgs e) in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\CommProc\CommonProc.vb:line 392
                '   at System.Windows.Forms.Application.ThreadContext.OnThreadException(Exception t)
                '   at System.Windows.Forms.Control.WndProcException(Exception e)
                '   at System.Windows.Forms.Control.ControlNativeWindow.OnThreadException(Exception e)
                '   at System.Windows.Forms.NativeWindow.Callback(IntPtr hWnd, Int32 msg, IntPtr wparam, IntPtr lparam)
                '   at System.Windows.Forms.UnsafeNativeMethods.DispatchMessageW(MSG& msg)
                '   at System.Windows.Forms.Application.ComponentManager.System.Windows.Forms.UnsafeNativeMethods.IMsoComponentManager.FPushMessageLoop(Int32 dwComponentID, Int32 reason, Int32 pvLoopData)
                '   at System.Windows.Forms.Application.ThreadContext.RunMessageLoopInner(Int32 reason, ApplicationContext context)
                '   at System.Windows.Forms.Application.ThreadContext.RunMessageLoop(Int32 reason, ApplicationContext context)
                '                at(System.Windows.Forms.Application.Run())
                '   at CyberInternautes.Clinica.Software.Start() in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\Layer 2 - Software\§ Main\Software.vb:line 471
                '   at CyberInternautes.Clinica.CommonProc.Main() in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\CommProc\CommonProc.vb:line 372
                'InnerException 1 --->
                '                Error Type : System.InvalidCastException()
                'Message:
                '   Unable to cast object of type 'System.String' to type 'CyberInternautes.Clinica.SingleWindow'.

                'Source:
                '   Clinica -- System.Windows.Forms.Form OpenUniqueWindow(System.Object ByRef, System.String, Boolean, Boolean, Boolean)

                '   Exception Stack Trace :
                '      at CyberInternautes.Clinica.Fenetres.OpenUniqueWindow(Object& MyForm, String Caption, Boolean StartingWith, Boolean ForceReload, Boolean HideSideBar) in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\CommProc\Fenetres.vb:line 354
                '      at CyberInternautes.Clinica.Comptes.OpenAccount(Int32 NoAccount, CompteType AccountType) in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\CommProc\Comptes.vb:line 339
                '      at CyberInternautes.Clinica.Agenda.menuopenaccount_Click(Object eventSender, EventArgs eventArgs) in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\Layer 1 - Presentation\Windows\Uniques\Agenda.vb:line 7274
                '      at CyberInternautes.Clinica.Agenda.DayList_DblClick(Object sender, DblClickEventArgs e) in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\Layer 1 - Presentation\Windows\Uniques\Agenda.vb:line 7940
                '      at CyberInternautes.Controls.List.BorderPanel_DoubleClick(Object sender, EventArgs e) in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\Controls\List\List.vb:line 1052
                '      at System.Windows.Forms.Control.OnDoubleClick(EventArgs e)
                '      at System.Windows.Forms.Control.WmMouseUp(Message& m, MouseButtons button, Int32 clicks)
                '      at System.Windows.Forms.Control.WndProc(Message& m)
                '      at System.Windows.Forms.ScrollableControl.WndProc(Message& m)
                '      at System.Windows.Forms.Control.ControlNativeWindow.OnMessage(Message& m)
                '      at System.Windows.Forms.Control.ControlNativeWindow.WndProc(Message& m)
                '      at System.Windows.Forms.NativeWindow.Callback(IntPtr hWnd, Int32 msg, IntPtr wparam, IntPtr lparam)

                '   Environment stack trace :
                '      at System.Environment.GetStackTrace(Exception e, Boolean needFileInfo)
                '                at(System.Environment.get_StackTrace())
                '      at CyberInternautes.Clinica.CommonProc.AddErrorLog(Exception ErrorMsg, Byte InternalCount) in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\CommProc\CommonProc.vb:line 497
                '      at CyberInternautes.Clinica.CommonProc.Application_ThreadException(Object sender, ThreadExceptionEventArgs e) in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\CommProc\CommonProc.vb:line 392
                '      at System.Windows.Forms.Application.ThreadContext.OnThreadException(Exception t)
                '      at System.Windows.Forms.Control.WndProcException(Exception e)
                '      at System.Windows.Forms.Control.ControlNativeWindow.OnThreadException(Exception e)
                '      at System.Windows.Forms.NativeWindow.Callback(IntPtr hWnd, Int32 msg, IntPtr wparam, IntPtr lparam)
                '      at System.Windows.Forms.UnsafeNativeMethods.DispatchMessageW(MSG& msg)
                '      at System.Windows.Forms.Application.ComponentManager.System.Windows.Forms.UnsafeNativeMethods.IMsoComponentManager.FPushMessageLoop(Int32 dwComponentID, Int32 reason, Int32 pvLoopData)
                '      at System.Windows.Forms.Application.ThreadContext.RunMessageLoopInner(Int32 reason, ApplicationContext context)
                '      at System.Windows.Forms.Application.ThreadContext.RunMessageLoop(Int32 reason, ApplicationContext context)
                '                at(System.Windows.Forms.Application.Run())
                '      at CyberInternautes.Clinica.Software.Start() in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\Layer 2 - Software\§ Main\Software.vb:line 471
                '      at CyberInternautes.Clinica.CommonProc.Main() in C:\DropBoxFolder\My Dropbox\CI\Projects\Physio2-Remoting test\CommProc\CommonProc.vb:line 372
                '   InnerException 2 --->
                '                Aucune()
                '--------------------------------------------------------------------



                Throw New Exception("Testing bug with MainWin.FormOuvertes, should all be SingleWindow type, not string!! With this error, should have the stacktrace at an appropriate moment")
            End If

            _ValueA = value
        End Set
    End Property

    Public Property valueB() As Object Implements IListItem.ValueB
        Get
            Return _ValueB
        End Get
        Set(ByVal value As Object)
            _ValueB = value
        End Set
    End Property

    Public Property clickable() As Boolean Implements IListItem.Clickable
        Get
            Return _Clickable
        End Get
        Set(ByVal value As Boolean)
            _Clickable = value
        End Set
    End Property

    Public Property itemRectangle() As Rectangle Implements IListItem.ItemRectangle
        Get
            Return _ItemRectangle
        End Get
        Set(ByVal value As Rectangle)
            _ItemRectangle = value
        End Set
    End Property

    Public ReadOnly Property textSize() As Size
        Get
            If hasToMeasureString Then
                hasToMeasureString = False
                _TextSize = measureString(_Text, _Font)
            End If

            Return _TextSize
        End Get
    End Property

    Public Property top() As Integer Implements IListItem.Top
        Get
            Return _ItemRectangle.Y
        End Get
        Set(ByVal value As Integer)
            _ItemRectangle.Y = value
        End Set
    End Property

    Public Property left() As Integer Implements IListItem.Left
        Get
            Return _ItemRectangle.X
        End Get
        Set(ByVal value As Integer)
            _ItemRectangle.X = value
        End Set
    End Property

    Public Property width() As Integer Implements IListItem.Width
        Get
            Return _ItemRectangle.Width
        End Get
        Set(ByVal value As Integer)
            _ItemRectangle.Width = value
        End Set
    End Property

    Public Property height() As Integer Implements IListItem.Height
        Get
            Return _ItemRectangle.Height
        End Get
        Set(ByVal value As Integer)
            _ItemRectangle.Height = value
        End Set
    End Property
#End Region

    Public Event shouldRedraw() Implements IListItem.shouldRedraw

    Friend Sub calculateHeightWidth() Implements IListItem.calculateHeightWidth
        Dim iconsSize As Size = getItemMaxIconsSize()

        'Reset values
        _ItemRectangle.Height = 0
        _ItemRectangle.Width = 0

        'Add text width & height
        If _Text.Length > 0 Then
            _ItemRectangle.Height += textSize.Height
            _ItemRectangle.Width += textSize.Width
        End If

        Dim position As Icons.IconPositions = _IconsPosition
        If position = Icons.IconPositions.DefinedByParent Then position = _List.defaultIconsPosition
        Select Case position
            Case Icons.IconPositions.AfterText, Icons.IconPositions.BeforeText
                _ItemRectangle.Width += iconsSize.Width
                _ItemRectangle.Height = Math.Max(_ItemRectangle.Height, iconsSize.Height)

            Case Icons.IconPositions.OverText, Icons.IconPositions.BelowText
                _ItemRectangle.Height += iconsSize.Height
                _ItemRectangle.Width = Math.Max(_ItemRectangle.Width, iconsSize.Width)
        End Select

        'Add borders / margins left & right, top & bottom (pairing explains * 2) 
        _ItemRectangle.Height += IIf(_List.itemBorder = 0, 1, _List.itemBorder) * 2 + _List.itemMargin * 2
        _ItemRectangle.Width += IIf(_List.itemBorder = 0, 1, _List.itemBorder) * 2 + _List.itemMargin * 2


        'Correct if over
        'If position = Icons.IconPositions.AfterText AndAlso iconsMaxHeigth > _ItemRectangle.Height Then _ItemRectangle.Height = iconsMaxHeigth
        'If position <> Icons.IconPositions.AfterText AndAlso iconsTotalWidth > _ItemRectangle.Width Then _ItemRectangle.Width = iconsTotalWidth
    End Sub

    Friend Sub drawBack(ByVal e As PaintEventArgs) Implements IListItem.drawBack
        'Draw background
        Dim curPen As New Pen(_BackColor)
        Dim curBrush As New SolidBrush(_BackColor)
        e.Graphics.DrawRectangle(curPen, e.ClipRectangle)
        e.Graphics.FillRectangle(curBrush, e.ClipRectangle)
        curPen.Dispose()
        curBrush.Dispose()
    End Sub

    Friend Sub drawBorder(ByVal e As PaintEventArgs) Implements IListItem.drawBorder
        Dim startingPt As Integer = 1
        Dim endingPt As Integer = _List.itemBorder
        If endingPt = 0 Then endingPt = 1
        If _IsSelected Then startingPt = Math.Ceiling(endingPt / 2)

        Dim curPen As New Pen(CType(IIf(_List.itemBorder = 0, Me.backColor, _List.borderColor), Color))
        For i As Integer = 0 To startingPt - 2
            e.Graphics.DrawRectangle(curPen, e.ClipRectangle.X + i, e.ClipRectangle.Y + i, e.ClipRectangle.Width - i * 2, e.ClipRectangle.Height - i * 2)
        Next i
        If _IsSelected Then
            curPen.Dispose() : curPen = New Pen(_List.borderSelColor)
        End If
        For i As Integer = startingPt - 1 To endingPt - 1
            e.Graphics.DrawRectangle(curPen, e.ClipRectangle.X + i, e.ClipRectangle.Y + i, e.ClipRectangle.Width - i * 2, e.ClipRectangle.Height - i * 2)
        Next i
        curPen.Dispose()
    End Sub

    Public Function getItemMaxIconsSize() As Size
        Dim IconWidth, iconMaxHeight As Single

        Select Case _IconType
            Case Icons.IconType.InList
                If _List.icons IsNot Nothing Then
                    For i As Integer = 0 To _List.icons.Count - 1
                        If _IconsShowed.Count > i AndAlso _IconsShowed(i) Then
                            iconMaxHeight = Math.Max(iconMaxHeight, _List.icons(i).Height + iconSpacing * 2)
                            IconWidth += iconSpacing + _List.icons(i).Width
                        End If
                    Next
                End If

            Case Icons.IconType.ValueA
                If TypeOf _ValueA Is Icon OrElse TypeOf _ValueA Is Image Then
                    iconMaxHeight = _ValueA.Height + iconSpacing * 2
                    IconWidth = _ValueA.Width + iconSpacing
                End If

            Case Icons.IconType.ValueB
                If TypeOf _ValueB Is Icon OrElse TypeOf _ValueB Is Image Then
                    iconMaxHeight = _ValueB.Height + iconSpacing * 2
                    IconWidth = _ValueB.Width + iconSpacing
                End If

            Case Icons.IconType.Icon
                If _icon IsNot Nothing Then
                    iconMaxHeight = _icon.Height + iconSpacing * 2
                    IconWidth = _icon.Width + iconSpacing
                End If
        End Select
        If IconWidth <> 0 Then IconWidth += iconSpacing

        Return New Size(IconWidth, iconMaxHeight)
    End Function

    Friend Sub drawText(ByVal e As PaintEventArgs) Implements IListItem.drawText
        Dim XPt, yPt As Single
        With Me
            Dim position As Icons.IconPositions = _IconsPosition
            If position = Icons.IconPositions.DefinedByParent Then position = _List.defaultIconsPosition

            yPt = e.ClipRectangle.Top + If(position = Icons.IconPositions.BelowText OrElse position = Icons.IconPositions.OverText, 0, Math.Floor((e.ClipRectangle.Height - textSize.Height) / 2))

            Dim iconsSize As Size = getItemMaxIconsSize()

            If position = Icons.IconPositions.OverText Then
                yPt += iconsSize.Height
            End If
            If .alignment = IListItem.AlignmentType.LeftA Then
                XPt = e.ClipRectangle.Left + If(_IconsPosition = Icons.IconPositions.BeforeText, iconsSize.Width, 0)
            ElseIf Me.alignment = IListItem.AlignmentType.CenterA Then
                XPt = (e.ClipRectangle.Width - Me.textSize.Width - iconsSize.Width) / 2 + e.ClipRectangle.Left
            Else
                XPt = e.ClipRectangle.Right - Me.textSize.Width - iconsSize.Width
            End If

            'Draw text
            Dim fontBrush As New SolidBrush(.foreColor)
            e.Graphics.DrawString(.text, .font, fontBrush, XPt, yPt)
            fontBrush.Dispose()

            drawIcons(e, position, iconsSize)
        End With
    End Sub

    Private Sub drawIcons(ByVal e As PaintEventArgs, ByVal position As Icons.IconPositions, ByVal iconsSize As Size)
        'Draw icons
        Dim curIconXPos As Integer = e.ClipRectangle.Left + iconSpacing
        Dim curIconYPos As Integer = e.ClipRectangle.Top + iconSpacing
        If position = Icons.IconPositions.AfterText Then curIconXPos = e.ClipRectangle.Right - iconsSize.Width - iconSpacing 'XPt + .TextSize.Width + iconSpacing
        If position = Icons.IconPositions.BelowText Then curIconYPos += textSize.Height

        Select Case _IconType
            Case Icons.IconType.InList
                If _List.icons IsNot Nothing Then
                    For i As Integer = 0 To _List.icons.Count - 1
                        If _IconsShowed.Count > i AndAlso _IconsShowed(i) Then
                            e.Graphics.DrawIcon(_List.icons(i), curIconXPos, curIconYPos)
                            curIconXPos += iconSpacing + _List.icons(i).Width
                        End If
                    Next
                End If

            Case Icons.IconType.ValueA
                If TypeOf _ValueA Is Icon Then
                    e.Graphics.DrawIcon(_ValueA, curIconXPos, curIconYPos)
                ElseIf TypeOf _ValueA Is Image Then
                    e.Graphics.DrawImage(_ValueA, curIconXPos, curIconYPos)
                End If

            Case Icons.IconType.ValueB
                If TypeOf _ValueB Is Icon Then
                    e.Graphics.DrawIcon(_ValueB, curIconXPos, curIconYPos)
                ElseIf TypeOf _ValueB Is Image Then
                    e.Graphics.DrawImage(_ValueB, curIconXPos, curIconYPos)
                End If

            Case Icons.IconType.Icon
                If _icon IsNot Nothing Then e.Graphics.DrawIcon(_icon, curIconXPos, curIconYPos)
        End Select
    End Sub

    Friend Sub onPaint(ByVal e As PaintEventArgs) Implements IListItem.onPaint
        drawBack(e)
        drawBorder(e)
        drawText(e)
    End Sub

    Public Function compareTo(ByVal other As IListItem) As Integer Implements System.IComparable(Of IListItem).CompareTo
        Return _Text.CompareTo(other.Text)
    End Function

    Public Function getItems() As System.Collections.Generic.List(Of ListItem) Implements IListItem.getItems
        Dim t As New Generic.List(Of ListItem)
        t.Add(Me)
        Return t
    End Function

    Public Overrides Function toString() As String
        Return Me.text
    End Function
End Class
