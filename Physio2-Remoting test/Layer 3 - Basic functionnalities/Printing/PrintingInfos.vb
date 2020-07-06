Imports Microsoft.Win32

Public Class PrintingInfos
    Private Sub New()
    End Sub

    Private header As String = ""
    Private footer As String = ""
    Private margin_left As Double = 0.75118
    Private margin_top As Double = 0.75118
    Private margin_right As Double = 0.75118
    Private margin_bottom As Double = 0.75118
    Private printBackground As Boolean = True
    Private shrinkToFit As Boolean = True

    Private Shared oldHeader As String = ""
    Private Shared oldFooter As String = ""
    Private Shared oldMargin_left As Double = 0.75118
    Private Shared oldMargin_top As Double = 0.75118
    Private Shared oldMargin_right As Double = 0.75118
    Private Shared oldMargin_bottom As Double = 0.75118
    Private Shared oldPrintBackground As Boolean = True
    Private Shared oldShrinkToFit As Boolean = True

    Private Shared isValuesAreMine As Boolean = False
    Private Shared oldValuesGotten As Boolean = False


    Public Sub New(ByVal header As String, ByVal footer As String, ByVal margin_left As Double, ByVal margin_top As Double, ByVal margin_right As Double, ByVal margin_bottom As Double, Optional ByVal printBackground As Boolean = True, Optional ByVal shrinkToFit As Boolean = True)
        Me.header = header
        Me.footer = footer
        Dim blankFooter As Boolean = (footer IsNot Nothing AndAlso footer = "")
        Dim blankHeader As Boolean = (header IsNot Nothing AndAlso header = "")

        Me.margin_bottom = IIf(margin_bottom >= 0 AndAlso blankFooter = False AndAlso margin_bottom < 0.16, 0.16, margin_bottom)
        Me.margin_left = margin_left
        Me.margin_right = margin_right
        Me.margin_top = IIf(margin_top >= 0 AndAlso blankHeader = False AndAlso margin_top < 0.16, 0.16, margin_top)
        Me.printBackground = printBackground
        Me.shrinkToFit = shrinkToFit
    End Sub

    Public Sub setToRegistry()
        Dim curKey As RegistryKey
        Try
            curKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Internet Explorer\\PageSetup", RegistryKeyPermissionCheck.ReadWriteSubTree)
        Catch ex As System.UnauthorizedAccessException
            'TODO : WIN7 having problem... or at least POSTE003 at LEG, because POSTE009 at PAT is on WIN7 too
            oldValuesGotten = True 'Bypass reset too
            Exit Sub
        End Try

        'Prise des anciennes valeurs
        If isValuesAreMine = False Then
            oldValuesGotten = True
            oldHeader = curKey.GetValue("header", "")
            oldFooter = curKey.GetValue("footer", "")
            oldMargin_left = Double.Parse(curKey.GetValue("margin_left", 0.75).ToString.Replace(".", ",").Replace(",", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
            oldMargin_top = Double.Parse(curKey.GetValue("margin_top", 0.75).ToString.Replace(".", ",").Replace(",", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
            oldMargin_right = Double.Parse(curKey.GetValue("margin_right", 0.75).ToString.Replace(".", ",").Replace(",", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
            oldMargin_bottom = Double.Parse(curKey.GetValue("margin_bottom", 0.75).ToString.Replace(".", ",").Replace(",", System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator))
            oldPrintBackground = IIf(curKey.GetValue("Print_Background", "no") = "yes", True, False)
            oldShrinkToFit = IIf(curKey.GetValue("Shrink_To_Fit", "yes") = "yes", True, False)
        End If

        If header IsNot Nothing Then curKey.SetValue("header", header)
        If footer IsNot Nothing Then curKey.SetValue("footer", footer)
        If margin_left >= 0 Then curKey.SetValue("margin_left", margin_left.ToString.Replace(",", "."))
        If margin_top >= 0 Then curKey.SetValue("margin_top", margin_top.ToString.Replace(",", "."))
        If margin_right >= 0 Then curKey.SetValue("margin_right", margin_right.ToString.Replace(",", "."))
        If margin_bottom >= 0 Then curKey.SetValue("margin_bottom", margin_bottom.ToString.Replace(",", "."))
        curKey.SetValue("Print_Background", IIf(printBackground, "yes", "no"))
        curKey.SetValue("Shrink_To_Fit", IIf(shrinkToFit, "yes", "no"))
        isValuesAreMine = True
    End Sub

    Public Sub resetOriginalValues()
        If oldValuesGotten = False OrElse isValuesAreMine = False Then Exit Sub

        Dim curKey As RegistryKey
        curKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Internet Explorer\\PageSetup", True)
        curKey.SetValue("header", oldHeader)
        curKey.SetValue("footer", oldFooter)
        curKey.SetValue("margin_left", oldMargin_left.ToString.Replace(",", "."))
        curKey.SetValue("margin_top", oldMargin_top.ToString.Replace(",", "."))
        curKey.SetValue("margin_right", oldMargin_right.ToString.Replace(",", "."))
        curKey.SetValue("margin_bottom", oldMargin_bottom.ToString.Replace(",", "."))
        curKey.SetValue("Print_Background", IIf(oldPrintBackground, "yes", "no"))
        curKey.SetValue("Shrink_To_Fit", IIf(oldShrinkToFit, "yes", "no"))

        oldValuesGotten = False
        isValuesAreMine = False
    End Sub
End Class
