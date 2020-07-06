Public Class ContextMenuItem
    Inherits ToolStripMenuItem

    'CODE by Darren M. Bork, free to use because MS pissed me off too much with lost features in VB6. :)

    Private pContext As Point 'specific point for the context menu instead of the default screen position.
    Private bIsContext As Boolean = False 'tracks if the next display show be with different coordinates.
    ''' <summary>
    ''' Shows the specified ContextMenuItem
    ''' </summary>
    ''' <param name="bContext">bContext=True - displays the context menu at the current screen co-ordinates</param>
    ''' <remarks></remarks>
    Shadows Sub showDropDown(ByVal bContext As Boolean)
        If bContext Then
            bIsContext = True
        Else
            bIsContext = False
        End If
        pContext = Nothing
        MyBase.ShowDropDown()
    End Sub
    ''' <summary>
    ''' Shows the specified ContextMenuItem at the screen point specified
    ''' </summary>
    ''' <param name="pSpecificScreenPoint">The Point to display the menu at, if Nothing the MousePosition is used.</param>
    ''' <remarks></remarks>
    Shadows Sub showDropDown(ByVal pSpecificScreenPoint As Point)
        bIsContext = True
        pContext = pSpecificScreenPoint
        MyBase.ShowDropDown()
    End Sub
    ''' <summary>
    ''' Shows the specified ContextMenuItem at the default position in the menu.
    ''' </summary>
    ''' <remarks></remarks>
    Shadows Sub showDropDown()
        bIsContext = False
        MyBase.ShowDropDown()
    End Sub
    ''' <summary>
    ''' Overriden to allow reset of the Context option that informas the control to display as context.
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub onMouseEnter(ByVal e As System.EventArgs)
        bIsContext = False
        MyBase.OnMouseEnter(e)
    End Sub
    ''' <summary>
    ''' Overriden to change the location of drawing the menu based on the developers request.
    ''' </summary>
    ''' <value></value>
    ''' <returns>The Point to draw the menu at</returns>
    ''' <remarks></remarks>
    Protected Overrides ReadOnly Property dropDownLocation() As System.Drawing.Point
        Get
            Dim pBase As Point = MyBase.DropDownLocation
            Dim pOut As Point
            If Not bIsContext Then 'normal render of menu.
                pOut = pBase
            ElseIf pContext = Nothing Then 'context menu at the current mouse co-ord's
                pOut = Control.MousePosition()
            Else 'context menu at the point specified in the function call.
                pOut = pContext
            End If
            Return pOut
        End Get
    End Property
End Class

