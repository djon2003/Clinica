Imports System
Imports System.Runtime.InteropServices


<ComImport(), Guid("00000118-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)> _
  Public Interface IOleClientSite

    Sub saveObject()
    Sub getMoniker(ByVal dwAssign As Integer, ByVal dwWhichMoniker As Integer, ByRef ppmk As Object)
    Sub getContainer(ByRef ppContainer As Object)
    Sub showObject()
    Sub onShowWindow(ByVal fShow As Boolean)
    Sub requestNewObjectLayout()
End Interface
