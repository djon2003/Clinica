Imports FreeTextBoxControls

Public Class WebTextPage
    Inherits System.Web.UI.Page

    Private Class CreateLink
        Inherits FreeTextBoxControls.ToolBarButton

        Public Sub New()
            MyBase.New("Ajouter un lien", "createlink") '"../../../WebSite/WebResource.axd?d=IbGCgeLeMjGHQVG2OzD2lZDpM24kOC9B3p1LlzKcrN6zU_eaUjpkv9VQ93K8eWD6Klecu3FcoSCBhS9EQW37l4AWXO9kVPHYUu-Fnah9mtU1&t=632888509040000000") '
            Me.ScriptBlock = "AddLink();"
        End Sub
    End Class

    Private Class AddImgButton
        Inherits FreeTextBoxControls.ToolbarButton

        Public Sub New()
            MyBase.New("Insérer une image","insertimage") ' "../../../WebSite/WebResource.axd?d=IbGCgeLeMjGHQVG2OzD2lZDpM24kOC9B3p1LlzKcrN6zU_eaUjpkv9VQ93K8eWD6unwdqE7jasRs4PMSf-NAUMdEKpQ4Uaq8N2JsD8vxCxM1&t=632888509040000000")
            'Me.Title = "Insérer une image"
            Me.ScriptBlock = "AddImage();"
        End Sub

    End Class

    'Protected WithEvents Form1 As System.Web.UI.HtmlControls.HtmlForm
    'Protected WithEvents AdjustSize As System.Web.UI.WebControls.Button
    'Protected WithEvents SaveButton As System.Web.UI.WebControls.Button
    'Protected WithEvents FreeTextBox1 As FreeTextBoxControls.FreeTextBox
    'Protected PageTitle As System.Web.UI.HtmlControls.HtmlGenericControl
    Protected WithEvents TextWorkingBox As System.Web.UI.WebControls.Literal
    'Protected WithEvents FocusButton As System.Web.UI.HtmlControls.HtmlInputButton

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
        FreeTextBox1.EditorTag = FTBAssembly.CyberInternautes.FTBAssembly.EditingTag.IFRAME
        FreeTextBox1.BreakMode = FreeTextBoxControls.BreakMode.Paragraph
        FreeTextBox1.DisableIEBackButton = True
        FreeTextBox1.Language = "fr-FR"
        FreeTextBox1.EnableHtmlMode = False
        FreeTextBox1.ToolbarStyleConfiguration = FreeTextBoxControls.ToolbarStyleConfiguration.Office2000
        'FreeTextBox1.FontFacesMenuList &= 
        'FreeTextBox1.FontFacesMenuNames &=
        'toolbarlayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu,FontForeColorPicker,FontBackColorsMenu,FontBackColorPicker|Bold,Italic,Underline,Strikethrough,Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImage|Cut,Copy,Paste,Delete;SymbolsMenu,InsertRule,InsertDate,InsertTime|InsertTable,EditTable;InsertTableRowAfter,InsertTableRowBefore,DeleteTableRow;InsertTableColumnAfter,InsertTableColumnBefore,DeleteTableColumn"
        FreeTextBox1.UpdateToolbar = True
        FreeTextBox1.FormatHtmlTagsToXhtml = False
        AddHandler FreeTextBox1.Load, AddressOf ChangeHTMLType
    End Sub

#End Region

    Private Sub ChangeHTMLType(ByVal sender As Object, ByVal e As System.EventArgs)
        '        ctype(sender,Control).
        Dim t As Integer = 0
    End Sub

    Private MyPath As String
    Private initPos As Integer = 0

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            PageTitle.InnerText = "WebTextControl"

            FreeTextBox1.Toolbars.Clear()
            FreeTextBox1.ButtonImagesLocation = ResourceLocation.InternalResource
            Dim SetToolBarStyle As Boolean = False

            'QueryString analyse
            Dim MyQueryString As String = Page.Request.Url.Query
            If MyQueryString.Length = 0 Then GoTo SkipQueryString
            MyQueryString = MyQueryString.Substring(1)
            If MyQueryString.Length = 0 Then GoTo SkipQueryString

            With Page.Request.QueryString
                Dim MyKeys() As String = .AllKeys
                Dim i As Byte

                For i = 0 To MyKeys.Length - 1
                    Select Case MyKeys(i)
                        Case "height"
                            If Me.IsPostBack = False Then FreeTextBox1.Height = New Unit(CInt(.Item(MyKeys(i))), UnitType.Pixel)
                        Case "width"
                            If Me.IsPostBack = False Then FreeTextBox1.Width = New Unit(CInt(.Item(MyKeys(i))), UnitType.Pixel)
                        Case "page"
                            MyPath = System.Web.HttpUtility.UrlDecode(.Item(MyKeys(i)))

                            If Me.IsPostBack = False Then
                                Dim MyFileContent() As String = CyberInternautes.Fichiers.ReadFile(MyPath)
                                If MyFileContent(0).StartsWith("ERROR") = False Then FreeTextBox1.Text = String.Join(vbCrLf, MyFileContent)
                            End If
                        Case "contenu"
                            Dim MyContenu As String = System.Web.HttpUtility.UrlDecode(.Item(MyKeys(i)))

                            If Me.IsPostBack = False Then FreeTextBox1.Text = MyContenu

                        Case "initpos"
                            If Me.IsPostBack = False Then initPos = .Item(MyKeys(i))

                        Case "toolbarstyle"
                            Select Case CInt(.Item(MyKeys(i)))
                                Case 1
                                    ToolBarStyle1()
                                    SetToolBarStyle = True
                                Case 2
                                    ToolBarStyle2()
                                    SetToolBarStyle = True
                            End Select
                    End Select
                Next i
            End With
SkipQueryString:

            If SetToolBarStyle = False Then ToolBarStyle1()
        Catch ex As Exception
        End Try
    End Sub

#Region "Toolbar styles"
    Private Sub ToolBarStyle1()
        Dim toolbar1 As Toolbar = New Toolbar()
        toolbar1.Items.Add(New ParagraphMenu())
        toolbar1.Items.Add(New FontFacesMenu())
        toolbar1.Items.Add(New FontSizesMenu())
        toolbar1.Items.Add(New FontForeColorsMenu())
        toolbar1.Items.Add(New FontForeColorPicker())
        toolbar1.Items.Add(New FontBackColorsMenu())
        toolbar1.Items.Add(New FontBackColorPicker())
        FreeTextBox1.Toolbars.Add(toolbar1)

        Dim toolbar2 As Toolbar = New Toolbar()
        toolbar2.Items.Add(New Bold())
        toolbar2.Items.Add(New Italic())
        toolbar2.Items.Add(New Underline())
        toolbar2.Items.Add(New StrikeThrough())
        toolbar2.Items.Add(New SuperScript())
        toolbar2.Items.Add(New SubScript())
        toolbar2.Items.Add(New RemoveFormat())
        FreeTextBox1.Toolbars.Add(toolbar2)

        Dim toolbar4 As Toolbar = New Toolbar()
        toolbar4.Items.Add(New JustifyLeft())
        toolbar4.Items.Add(New JustifyRight())
        toolbar4.Items.Add(New JustifyCenter())
        toolbar4.Items.Add(New JustifyFull())
        toolbar4.Items.Add(New ToolbarSeparator())
        toolbar4.Items.Add(New BulletedList())
        toolbar4.Items.Add(New NumberedList())
        toolbar4.Items.Add(New Indent())
        toolbar4.Items.Add(New Outdent())
        toolbar4.Items.Add(New ToolbarSeparator())
        toolbar4.Items.Add(New CreateLink())
        toolbar4.Items.Add(New Unlink())
        toolbar4.Items.Add(New AddImgButton())
        FreeTextBox1.Toolbars.Add(toolbar4)

        Dim toolbar3 As Toolbar = New Toolbar()
        toolbar3.Items.Add(New Cut())
        toolbar3.Items.Add(New Copy())
        toolbar3.Items.Add(New Paste())
        toolbar3.Items.Add(New Delete())
        toolbar3.Items.Add(New ToolbarSeparator())
        toolbar3.Items.Add(New SymbolsMenu())
        toolbar3.Items.Add(New InsertRule())
        toolbar3.Items.Add(New InsertDate())
        toolbar3.Items.Add(New InsertTime())
        FreeTextBox1.Toolbars.Add(toolbar3)

        Dim toolbar5 As Toolbar = New Toolbar()
        toolbar5.Items.Add(New InsertTable())
        toolbar5.Items.Add(New EditTable())
        toolbar5.Items.Add(New ToolbarSeparator())
        toolbar5.Items.Add(New InsertTableRowAfter())
        toolbar5.Items.Add(New InsertTableRowBefore())
        toolbar5.Items.Add(New DeleteTableRow())
        toolbar5.Items.Add(New ToolbarSeparator())
        toolbar5.Items.Add(New InsertTableColumnAfter())
        toolbar5.Items.Add(New InsertTableColumnBefore())
        toolbar5.Items.Add(New DeleteTableColumn())
        FreeTextBox1.Toolbars.Add(toolbar5)
    End Sub

    Private Sub ToolBarStyle2()
        Dim toolbar1 As Toolbar = New Toolbar()
        toolbar1.Items.Add(New FontFacesMenu())
        toolbar1.Items.Add(New FontSizesMenu())
        toolbar1.Items.Add(New FontForeColorsMenu())
        toolbar1.Items.Add(New FontForeColorPicker())
        toolbar1.Items.Add(New FontBackColorsMenu())
        toolbar1.Items.Add(New FontBackColorPicker())
        FreeTextBox1.Toolbars.Add(toolbar1)

        Dim toolbar2 As Toolbar = New Toolbar()
        toolbar2.Items.Add(New Bold())
        toolbar2.Items.Add(New Italic())
        toolbar2.Items.Add(New Underline())
        toolbar2.Items.Add(New StrikeThrough())
        toolbar2.Items.Add(New SuperScript())
        toolbar2.Items.Add(New SubScript())
        FreeTextBox1.Toolbars.Add(toolbar2)

        Dim toolbar4 As Toolbar = New Toolbar()
        toolbar4.Items.Add(New JustifyLeft())
        toolbar4.Items.Add(New JustifyRight())
        toolbar4.Items.Add(New JustifyCenter())
        toolbar4.Items.Add(New JustifyFull())
        toolbar4.Items.Add(New ToolbarSeparator())
        toolbar4.Items.Add(New BulletedList())
        toolbar4.Items.Add(New NumberedList())
        toolbar4.Items.Add(New Indent())
        toolbar4.Items.Add(New Outdent())
        toolbar4.Items.Add(New ToolbarSeparator())
        toolbar4.Items.Add(New CreateLink())
        toolbar4.Items.Add(New Unlink())
        FreeTextBox1.Toolbars.Add(toolbar4)

        Dim toolbar3 As Toolbar = New Toolbar()
        toolbar3.Items.Add(New Cut())
        toolbar3.Items.Add(New Copy())
        toolbar3.Items.Add(New Paste())
        toolbar3.Items.Add(New Delete())
        toolbar3.Items.Add(New ToolbarSeparator())
        toolbar3.Items.Add(New SymbolsMenu())
        toolbar3.Items.Add(New InsertRule())
        toolbar3.Items.Add(New InsertDate())
        toolbar3.Items.Add(New InsertTime())
        toolbar3.Items.Add(New AddImgButton())
        toolbar3.Items.Add(New ParagraphMenu())
        FreeTextBox1.Toolbars.Add(toolbar3)

        Dim toolbar5 As Toolbar = New Toolbar()
        toolbar5.Items.Add(New InsertTable())
        toolbar5.Items.Add(New EditTable())
        toolbar5.Items.Add(New ToolbarSeparator())
        toolbar5.Items.Add(New InsertTableRowAfter())
        toolbar5.Items.Add(New InsertTableRowBefore())
        toolbar5.Items.Add(New DeleteTableRow())
        toolbar5.Items.Add(New ToolbarSeparator())
        toolbar5.Items.Add(New InsertTableColumnAfter())
        toolbar5.Items.Add(New InsertTableColumnBefore())
        toolbar5.Items.Add(New DeleteTableColumn())
        FreeTextBox1.Toolbars.Add(toolbar5)
    End Sub
#End Region

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        'Me.Page.Applica = Me.Page.Form.InnerHtml.Replace("IFRAME", "DIV")
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
        Me.Response.Output.Write("<script language=Javascript>var initPos = " & initPos & ";</script>")
    End Sub
End Class

Namespace CyberInternautes
    Class Fichiers
        Public Shared Sub WriteFile(ByRef FullPath As String, ByRef FileContent() As Object, Optional ByVal SortFile As Boolean = False, Optional ByVal DelFileIfEmpty As Boolean = False, Optional ByVal KeepWhiteLine As Boolean = True)
            Dim FileNum, i As Integer
            Dim FEmpty As Boolean = True
            If SortFile = True Then Array.Sort(FileContent)

            FileNum = FreeFile()
            FileOpen(FileNum, FullPath, OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
            If Not FileContent Is Nothing Then
                For i = 0 To UBound(FileContent)
                    If KeepWhiteLine = True Or (KeepWhiteLine = False And FileContent(i) <> "") Then PrintLine(FileNum, FileContent(i))
                    If FEmpty = True And FileContent(i) <> "" Then FEmpty = False
                Next i
            End If
            FileClose(FileNum)

            If FEmpty = True And DelFileIfEmpty = True Then IO.File.Delete(FullPath)
        End Sub

        Public Shared Function ReadFile(ByVal FullPath As String, Optional ByVal FileMask() As Boolean = Nothing, Optional ByVal ReturnWhiteLine As Boolean = True) As Array
            Dim Init, Accept As Boolean
            Dim LineCounter As Short
            Dim Line As String
            Dim Lines As New ArrayList()

            LineCounter = 1
            Init = False

            Line = ""
            If Dir(FullPath, FileAttribute.Archive Or FileAttribute.Hidden Or FileAttribute.ReadOnly Or FileAttribute.System) = "" Then
                Lines.Add("ERROR:NOFILE")
                Return Lines.ToArray(Line.GetType)
            End If

            Dim MyFile As IO.FileStream
            Try
                MyFile = IO.File.Open(FullPath, IO.FileMode.Open, IO.FileAccess.Read)
                Dim MyFileReader As New IO.StreamReader(MyFile, System.Text.Encoding.UTF8)

                Line = MyFileReader.ReadLine()
                Do While (Not Line Is Nothing)
                    If Not Left(Line, 2) = "#§" Then
                        Accept = False
                        If FileMask Is Nothing Then
                            Accept = True
                        Else
                            If UBound(FileMask) < LineCounter Then
                                If FileMask(0) = True Then Accept = True
                            Else
                                Accept = FileMask(LineCounter)
                            End If
                        End If
                        If ReturnWhiteLine = False And Line = "" Then Accept = False
                        If Accept = True Then
                            Init = True
                            Lines.Add(Line)
                        End If

                        LineCounter += 1
                    End If
                    Line = MyFileReader.ReadLine()
                Loop
            Catch
                Lines.Clear()
                Lines.Add("ERROR:FILECANTBEOPENED")
            Finally
                If Not MyFile Is Nothing Then
                    MyFile.Close()
                End If
            End Try

            If Init = False Then
                Lines.Add("ERROR:FILEEMPTY")
            End If

            Line = ""
            Return Lines.ToArray(Line.GetType)
        End Function
    End Class
End Namespace
