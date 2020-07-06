Public Interface IPrintable

    Sub print(Optional ByVal promptUser As Boolean = True, Optional ByVal waitForSpooling As Boolean = False)
    Sub printPreview()
    Sub printOptions()
End Interface
