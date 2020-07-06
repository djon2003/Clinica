Imports System
Imports System.Windows.Forms
Imports System.Runtime.InteropServices


<ComImport(), Guid("00000112-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
  Public Interface IOleObject

    Sub setClientSite(ByVal pClientSite As IOleClientSite)
    Sub getClientSite(ByRef ppClientSite As IOleClientSite)
    Sub setHostNames(ByVal szContainerApp As Object, ByVal szContainerObj As Object)
    Sub close(ByVal dwSaveOption As Integer)
    Sub setMoniker(ByVal dwWhichMoniker As Integer, ByVal pmk As Object)
    Sub getMoniker(ByVal dwAssign As Integer, ByVal dwWhichMoniker As Integer, ByVal ppmk As Object)
    Sub initFromData(ByVal pDataObject As IDataObject, ByVal fCreation As Boolean, ByVal dwReserved As Integer)
    Sub getClipboardData(ByVal dwReserved As Integer, ByRef ppDataObject As IDataObject)
    Sub doVerb(ByVal iVerb As Integer, ByVal lpmsg As Integer, ByVal pActiveSite As Object, ByVal lindex As Integer, ByVal hwndParent As Integer, ByVal lprcPosRect As Integer)
    Sub enumVerbs(ByRef ppEnumOleVerb As Object)
    Sub update()
    Sub isUpToDate()
    Sub getUserClassID(ByVal pClsid As Integer)
    Sub getUserType(ByVal dwFormOfType As Integer, ByVal pszUserType As Integer)
    Sub setExtent(ByVal dwDrawAspect As Integer, ByVal psizel As Integer)
    Sub getExtent(ByVal dwDrawAspect As Integer, ByVal psizel As Integer)
    Sub advise(ByVal pAdvSink As Object, ByVal pdwConnection As Integer)
    Sub unadvise(ByVal dwConnection As Integer)
    Sub enumAdvise(ByRef ppenumAdvise As Object)
    Sub getMiscStatus(ByVal dwAspect As Integer, ByVal pdwStatus As Integer)
    Sub setColorScheme(ByVal pLogpal As Object)
End Interface
