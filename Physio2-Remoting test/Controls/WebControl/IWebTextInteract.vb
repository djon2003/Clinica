Imports System.Runtime.InteropServices
Imports mshtml

<InterfaceType(ComInterfaceType.InterfaceIsIDispatch)> Public Interface IWebTextInteract
    Sub sendTextTo(ByVal [Text] As String, ByVal waitingNumber As Integer)
    Sub sendposTo(ByVal pos As Integer, ByVal waitingNumber As Integer)
    Sub pageLoaded()
    Sub textChanged(ByVal [Text] As String)
    Sub addLink()
    Sub addImage()
    Sub editorClick(ByVal url As String)
    Sub onError(ByVal sMsg As String, ByVal sUrl As String, ByVal sLine As String)
End Interface
