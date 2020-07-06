Imports System.Runtime.InteropServices
Imports mshtml

<InterfaceType(ComInterfaceType.InterfaceIsIDispatch)> Public Interface IWebTextInteract
    Sub sendTextTo(ByVal [Text] As String, ByVal waitingNumber As Integer)
    Sub sendPosTo(ByVal pos As Integer, ByVal waitingNumber As Integer)
    Sub savePos(ByVal pos As String, ByVal waitingNumber As Integer)
    Sub pageLoaded()
    Sub textChanged(ByVal [Text] As String)
    Sub addLink()
    Sub addImage()
    Sub editorClick(ByVal url As String)
    Sub catchError(ByVal msg As String, ByVal url As String, ByVal line As Integer, ByVal html As String)
    Sub pasteHTMLFromClipboard()
    Sub pasted()
    Sub insertDate(ByVal fieldId As String, ByVal selectedDate As String)
End Interface
