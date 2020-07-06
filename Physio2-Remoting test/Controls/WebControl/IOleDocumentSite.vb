Imports System
Imports System.Runtime.InteropServices


<ComImport(), Guid("b722bcc7-4e68-101b-a2bc-00aa00404770"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
  Public Interface IOleDocumentSite
    Sub activateMe(ByRef pViewToActivate As Object)
End Interface
