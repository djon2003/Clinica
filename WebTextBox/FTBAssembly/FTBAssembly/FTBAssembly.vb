Imports FreeTextBoxControls
Imports System.Web.UI.WebControls

Namespace CyberInternautes

    Public Class FTBAssembly
        Inherits FreeTextBox

        Public Enum EditingTag
            IFRAME = 0
            DIV = 1
        End Enum

        Private _EditorTag As EditingTag = EditingTag.IFRAME

        Protected Overrides Function OnBubbleEvent(ByVal source As Object, ByVal args As System.EventArgs) As Boolean
            Dim t As Boolean = MyBase.OnBubbleEvent(source, args)
            Return True
        End Function

        Public Property EditorTag() As EditingTag
            Get
                Return _EditorTag
            End Get
            Set(ByVal value As EditingTag)
                _EditorTag = value
            End Set
        End Property

        Protected Overrides Sub RenderRichEditor(ByVal writer As System.Web.UI.HtmlTextWriter)
            Dim hasToolbars As Boolean = False
            If (Me.Toolbars IsNot Nothing AndAlso Toolbars.Count > 0) Then hasToolbars = True


            writer.WriteLine("<style type=""text/css"">")
            RenderEditorStyles(writer)
            RenderButtonStyles(writer)
            RenderTabStyles(writer)
            writer.WriteLine("</style>")

            writer.WriteLine("<table cellpadding=""2"" cellspacing=""0"" class=""" & Me.ClientID & "_OuterTable""><tr><td>")
            writer.WriteLine("<div>")

            If (hasToolbars AndAlso Me.EnableToolbars) Then
                writer.WriteLine("<div id=""" & Me.ClientID & "_toolbarArea"" style=""padding-bottom:2px;clear:both;"">")

                For Each toolbar As Toolbar In Toolbars
                    toolbar.RepeatDirection = RepeatDirection.Horizontal
                    RenderToolbar(writer, toolbar)
                Next

                writer.WriteLine("</div>")
            End If

            Dim htmlWidth As String = Me.Width.ToString()
            Dim htmlHeight As String = Me.Height.ToString()
            Dim iframeWidth As String = Me.Width.ToString()
            Dim iframeHeight As String = Me.Height.ToString()

            REM Not Accessible
            'If (Me.browserInfo.IsIE5plus) Then
            If (Me.Width.ToString().IndexOf("%") = -1) Then iframeWidth = (Me.Width.Value - 2).ToString() & "px"

            If (Me.Height.ToString().IndexOf("%") = -1) Then iframeHeight = (Me.Height.Value - 1).ToString() & "px"

            'End If

            writer.WriteLine("<div id=""" & Me.ClientID & "_designEditorArea"" style=""clear:both;padding-top:1px;"">" _
              & "<" & EditorTag.ToString & " id=""" & Me.ClientID & "_designEditor"" style=""padding: 0px; width:" & iframeWidth & "; height: " & iframeHeight & ";"" src=""about:blank"" class=""" & Me.ClientID & "_DesignBox""></" & EditorTag.ToString & ">" _
          & "</div>" _
          & "<div id=""" & Me.ClientID & "_htmlEditorArea"" style=""clear:both;display:none;padding-bottom:1px;"">" _
             & "<textarea id=""" & Me.ClientID & """ name=""" & Me.ClientID & """ disabled=""disabled"" style=""padding: 0px; width:" & htmlWidth & "; height: " & htmlHeight & ";" & (IIf(Me.TextDirection = TextDirection.RightToLeft, "direction:rtl;", "").ToString) & """ class=""" & (IIf(Me.HtmlModeCssClass = String.Empty, Me.ClientID & "_HtmlBox", Me.HtmlModeCssClass).ToString) & """>" & Me.Page.Server.HtmlEncode(Me.Text) & "</textarea>" _
          & "</div>" _
           & "<div id=""" & Me.ClientID & "_previewPaneArea"" style=""clear:both;display:none;padding-bottom:1px;"">" _
             & "<iframe id=""" & Me.ClientID & "_previewPane"" style=""padding: 0px; width:" & iframeWidth & "; height: " & iframeHeight & """ src=""about:blank"" class=""" & Me.ClientID & "_DesignBox""></iframe>" _
          & "</div>")

            If (EnableHtmlMode) Then
                writer.WriteLine("<div style=""clear:both;padding-top:2px;"">")
                RenderTabs(writer)
                writer.WriteLine("</div>")
            End If

            writer.WriteLine("</div>")

            writer.WriteLine("</table>")
        End Sub
    End Class

End Namespace