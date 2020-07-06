Public Interface IHTMLPageProducer
    Sub generateHTMLProperties(ByRef htmlBuilder As System.Text.StringBuilder)
    Sub generateHTMLFirstLine(ByRef htmlBuilder As System.Text.StringBuilder)
    Sub generateHTMLMiddleLines(ByRef htmlBuilder As System.Text.StringBuilder)
    Sub generateHTMLLastLine(ByRef htmlBuilder As System.Text.StringBuilder)
End Interface
