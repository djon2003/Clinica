Public Class TextControl
    Inherits System.Windows.Forms.RichTextBox

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        Me.menuZoom1.Checked = True
    End Sub

    'UserControl1 overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents FDialog As System.Windows.Forms.FontDialog
    Friend WithEvents menucliquedroit As System.Windows.Forms.ContextMenu
    Friend WithEvents menucouper As System.Windows.Forms.MenuItem
    Friend WithEvents menucopier As System.Windows.Forms.MenuItem
    Friend WithEvents menucoller As System.Windows.Forms.MenuItem
    Friend WithEvents menuselectall As System.Windows.Forms.MenuItem
    Friend WithEvents menuline As System.Windows.Forms.MenuItem
    Friend WithEvents menualign As System.Windows.Forms.MenuItem
    Friend WithEvents menualignement_0 As System.Windows.Forms.MenuItem
    Friend WithEvents menualignement_1 As System.Windows.Forms.MenuItem
    Friend WithEvents menualignement_2 As System.Windows.Forms.MenuItem
    Friend WithEvents menuZoom As System.Windows.Forms.MenuItem
    Friend WithEvents menuZoom1 As System.Windows.Forms.MenuItem
    Friend WithEvents menuZoom2 As System.Windows.Forms.MenuItem
    Friend WithEvents menuZoom3 As System.Windows.Forms.MenuItem
    Friend WithEvents menuZoom4 As System.Windows.Forms.MenuItem
    Friend WithEvents menuZoom5 As System.Windows.Forms.MenuItem
    Friend WithEvents menuZoom6 As System.Windows.Forms.MenuItem
    Friend WithEvents menuZoom7 As System.Windows.Forms.MenuItem
    Friend WithEvents menuZoom8 As System.Windows.Forms.MenuItem
    Friend WithEvents menuZoom9 As System.Windows.Forms.MenuItem
    Friend WithEvents menuZoom10 As System.Windows.Forms.MenuItem
    Friend WithEvents menuinsert As System.Windows.Forms.MenuItem
    Friend WithEvents menuinsertdate As System.Windows.Forms.MenuItem
    Friend WithEvents menuinsertimg As System.Windows.Forms.MenuItem
    Friend WithEvents menufonttype As System.Windows.Forms.MenuItem
    Friend WithEvents FileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents menuCopy As System.Windows.Forms.ContextMenu
    Friend WithEvents menuCopying As System.Windows.Forms.MenuItem
    Friend WithEvents menuSelectingAll As System.Windows.Forms.MenuItem
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.FDialog = New System.Windows.Forms.FontDialog
        Me.menucliquedroit = New System.Windows.Forms.ContextMenu
        Me.menucouper = New System.Windows.Forms.MenuItem
        Me.menucopier = New System.Windows.Forms.MenuItem
        Me.menucoller = New System.Windows.Forms.MenuItem
        Me.menuselectall = New System.Windows.Forms.MenuItem
        Me.menuline = New System.Windows.Forms.MenuItem
        Me.menualign = New System.Windows.Forms.MenuItem
        Me.menualignement_0 = New System.Windows.Forms.MenuItem
        Me.menualignement_1 = New System.Windows.Forms.MenuItem
        Me.menualignement_2 = New System.Windows.Forms.MenuItem
        Me.menuinsert = New System.Windows.Forms.MenuItem
        Me.menuinsertdate = New System.Windows.Forms.MenuItem
        Me.menuinsertimg = New System.Windows.Forms.MenuItem
        Me.menufonttype = New System.Windows.Forms.MenuItem
        Me.menuZoom = New System.Windows.Forms.MenuItem
        Me.menuZoom1 = New System.Windows.Forms.MenuItem
        Me.menuZoom2 = New System.Windows.Forms.MenuItem
        Me.menuZoom3 = New System.Windows.Forms.MenuItem
        Me.menuZoom4 = New System.Windows.Forms.MenuItem
        Me.menuZoom5 = New System.Windows.Forms.MenuItem
        Me.menuZoom6 = New System.Windows.Forms.MenuItem
        Me.menuZoom7 = New System.Windows.Forms.MenuItem
        Me.menuZoom8 = New System.Windows.Forms.MenuItem
        Me.menuZoom9 = New System.Windows.Forms.MenuItem
        Me.menuZoom10 = New System.Windows.Forms.MenuItem
        Me.FileDialog = New System.Windows.Forms.OpenFileDialog
        Me.menuCopy = New System.Windows.Forms.ContextMenu
        Me.menuCopying = New System.Windows.Forms.MenuItem
        Me.menuSelectingAll = New System.Windows.Forms.MenuItem
        '
        'FDialog
        '
        Me.FDialog.ShowColor = True
        '
        'menucliquedroit
        '
        Me.menucliquedroit.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menucouper, Me.menucopier, Me.menucoller, Me.menuselectall, Me.menuline, Me.menualign, Me.menuinsert, Me.menufonttype, Me.menuZoom})
        '
        'menucouper
        '
        Me.menucouper.Index = 0
        Me.menucouper.Text = "Couper"
        '
        'menucopier
        '
        Me.menucopier.Index = 1
        Me.menucopier.Text = "Copier"
        '
        'menucoller
        '
        Me.menucoller.Index = 2
        Me.menucoller.Text = "Coller"
        '
        'menuselectall
        '
        Me.menuselectall.Index = 3
        Me.menuselectall.Text = "Sélectionner tout"
        '
        'menuline
        '
        Me.menuline.Index = 4
        Me.menuline.Text = "-"
        '
        'menualign
        '
        Me.menualign.Index = 5
        Me.menualign.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menualignement_0, Me.menualignement_1, Me.menualignement_2})
        Me.menualign.Text = "Alignement"
        '
        'menualignement_0
        '
        Me.menualignement_0.Index = 0
        Me.menualignement_0.Text = "Gauche"
        '
        'menualignement_1
        '
        Me.menualignement_1.Index = 1
        Me.menualignement_1.Text = "Droit"
        '
        'menualignement_2
        '
        Me.menualignement_2.Index = 2
        Me.menualignement_2.Text = "Centré"
        '
        'menuinsert
        '
        Me.menuinsert.Index = 6
        Me.menuinsert.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuinsertdate, Me.menuinsertimg})
        Me.menuinsert.Text = "Insérer ..."
        '
        'menuinsertdate
        '
        Me.menuinsertdate.Index = 0
        Me.menuinsertdate.Text = "Date"
        '
        'menuinsertimg
        '
        Me.menuinsertimg.Index = 1
        Me.menuinsertimg.Text = "Image à partir d'un fichier"
        '
        'menufonttype
        '
        Me.menufonttype.Index = 7
        Me.menufonttype.Text = "Type d'écriture"
        '
        'menuZoom
        '
        Me.menuZoom.Index = 8
        Me.menuZoom.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuZoom1, Me.menuZoom2, Me.menuZoom3, Me.menuZoom4, Me.menuZoom5, Me.menuZoom6, Me.menuZoom7, Me.menuZoom8, Me.menuZoom9, Me.menuZoom10})
        Me.menuZoom.Text = "Agrandissement"
        '
        'menuZoom1
        '
        Me.menuZoom1.Index = 0
        Me.menuZoom1.Text = "1"
        '
        'menuZoom2
        '
        Me.menuZoom2.Index = 1
        Me.menuZoom2.Text = "2"
        '
        'menuZoom3
        '
        Me.menuZoom3.Index = 2
        Me.menuZoom3.Text = "3"
        '
        'menuZoom4
        '
        Me.menuZoom4.Index = 3
        Me.menuZoom4.Text = "4"
        '
        'menuZoom5
        '
        Me.menuZoom5.Index = 4
        Me.menuZoom5.Text = "5"
        '
        'menuZoom6
        '
        Me.menuZoom6.Index = 5
        Me.menuZoom6.Text = "6"
        '
        'menuZoom7
        '
        Me.menuZoom7.Index = 6
        Me.menuZoom7.Text = "7"
        '
        'menuZoom8
        '
        Me.menuZoom8.Index = 7
        Me.menuZoom8.Text = "8"
        '
        'menuZoom9
        '
        Me.menuZoom9.Index = 8
        Me.menuZoom9.Text = "9"
        '
        'menuZoom10
        '
        Me.menuZoom10.Index = 9
        Me.menuZoom10.Text = "10"
        '
        'FileDialog
        '
        Me.FileDialog.Filter = "Images|*.png;*.wmf;*.tiff;*.bmp;*.jpg;*.gif|Bitmap|*.bmp|GIF|*.gif|JPEG|*.jpg|PNG" & _
        "|*.png|TIFF|*.tiff|WMF|*.wmf"
        '
        'menuCopy
        '
        Me.menuCopy.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuCopying, Me.menuSelectingAll})
        '
        'menuCopying
        '
        Me.menuCopying.Index = 0
        Me.menuCopying.Text = "Copier"
        '
        'menuSelectingAll
        '
        Me.menuSelectingAll.Index = 1
        Me.menuSelectingAll.Text = "Sélectionner tout"
        '
        'TexteControl
        '
        Me.Size = New System.Drawing.Size(168, 152)

    End Sub

#End Region

#Region "Définitions"
    Private curTextType As RichTextBoxStreamType = RichTextBoxStreamType.RichText
    Dim _ShowMenu As Boolean = True
    Dim ancreStr As String
    Dim ancreBool As Boolean
    Dim ar As Boolean
    Dim tabspc As Short
    <System.Runtime.InteropServices.ProgId("AfterAncreEventArgs_NET.AfterAncreEventArgs")> Public NotInheritable Class AfterAncreEventArgs
        Inherits System.EventArgs
        Public keyCode As Short
        Public shift As Boolean
        Public control As Boolean
        Public alt As Boolean
        Public Sub New(ByRef keyCode As Short, ByRef shift As Boolean, ByRef alt As Boolean, ByRef control As Boolean)
            MyBase.New()
            Me.keyCode = keyCode
            Me.shift = shift
            Me.alt = alt
            Me.control = control
        End Sub
    End Class
    Event afterAncre(ByVal sender As System.Object, ByVal e As AfterAncreEventArgs)
    Event willSaveLoad()
#End Region

#Region "Propriétés"
    Public Property ancre() As String
        Get
            ancre = ancreStr
        End Get
        Set(ByVal Value As String)
            ancreStr = Value
        End Set
    End Property

    Public Property ancreON() As Boolean
        Get
            ancreON = ancreBool
        End Get
        Set(ByVal Value As Boolean)
            ancreBool = Value
        End Set
    End Property


    Public Property ancreRemove() As Boolean
        Get
            ancreRemove = ar
        End Get
        Set(ByVal Value As Boolean)
            ar = Value
        End Set
    End Property

    Public Property showImgMenu() As Boolean
        Get
            If menuinsertimg.Visible = True Then
                showImgMenu = True
            Else
                showImgMenu = False
            End If
        End Get
        Set(ByVal Value As Boolean)
            menuinsertimg.Visible = Value
        End Set
    End Property

    Public Property showMenu() As Boolean
        Get
            Return _ShowMenu
        End Get
        Set(ByVal Value As Boolean)
            _ShowMenu = Value
        End Set
    End Property

    Public Property tabSpacing() As Short
        Get
            tabSpacing = tabspc
        End Get
        Set(ByVal Value As Short)
            tabspc = Value
        End Set
    End Property
#End Region

    Public Shadows Sub loadFile(ByVal path As String, ByVal textType As RichTextBoxStreamType)
        curTextType = textType
        MyBase.LoadFile(path, textType)
        If curTextType = RichTextBoxStreamType.UnicodePlainText Then
            Me.Text = Me.Text.Replace("?", "") 'Remove an unreadable character
        End If
    End Sub

    Public Function getCurrentTextType() As RichTextBoxStreamType
        Return curTextType
    End Function

    Public Sub menufonttype_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menufonttype.Click
        If Me.SelectionLength > 0 Then
            FDialog.Font = Me.SelectionFont
            FDialog.Color = Me.SelectionColor
        End If
        'TODO  : REMOVED On Error GoTo Erreur
        FDialog.ShowDialog()
        Me.SelectionFont = FDialog.Font
        Me.SelectionColor = FDialog.Color
Erreur:
    End Sub
    Public Sub menuinsertdate_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuinsertdate.Click
        Me.SelectedText = Format(Date.Today, "dd/MM/yyyy")
    End Sub
    Public Sub menuinsertimg_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuinsertimg.Click
        'TODO  : REMOVED On Error Resume Next
        FileDialog.InitialDirectory = appPath
        FileDialog.ShowDialog()
        If Not FileDialog.FileName = "" Then
            Me.insertIMG(FileDialog.FileName)
        End If
    End Sub
    Public Sub menuselectall_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles menuselectall.Click, menuSelectingAll.Click
        Me.SelectAll()
    End Sub
    Private Sub menualignement_0_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menualignement_0.Click
        Me.SelectionAlignment = HorizontalAlignment.Left
    End Sub

    Private Sub menualignement_1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menualignement_1.Click
        Me.SelectionAlignment = HorizontalAlignment.Right
    End Sub

    Private Sub menualignement_2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menualignement_2.Click
        Me.SelectionAlignment = HorizontalAlignment.Center
    End Sub

    Private Sub menucoller_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menucoller.Click
        Me.Paste()
    End Sub

    Private Sub menucopier_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menucopier.Click, menuCopying.Click
        Me.Copy()
    End Sub

    Private Sub menucouper_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menucouper.Click
        Me.Cut()
    End Sub

    Private Sub texteControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Dim f As Double
        If e.KeyCode = 9 And MyBase.Enabled = False Then Me.SelectedText = Space(tabSpacing)

        If Not ancre = "" And e.Shift = True And e.Control = True Then
            If Me.SelectedText = ancre Then Me.SelectionStart += Me.SelectionLength
            f = Me.Find(ancre, Me.SelectionStart, Len(Me.Text), RichTextBoxFinds.None)
            If f < 0 Then Exit Sub
            Me.SelectionStart = f

            If ancreRemove = True Then
                Me.SelectionLength = Len(ancre)
            Else
                Me.SelectionStart += Len(ancre)
                Me.SelectionLength = 0
            End If
        End If
        RaiseEvent afterAncre(Me, New AfterAncreEventArgs(e.KeyCode, e.Shift, e.Alt, e.Control))
    End Sub
    Public Sub insertIMG(ByVal imgPath As String)
        Dim oldContent As Object = Clipboard.GetDataObject.GetData(DataFormats.Rtf)
        Dim img As New Bitmap(imgPath)
        Clipboard.SetDataObject(img)
        Me.Paste()
        Clipboard.SetDataObject(oldContent)
    End Sub

    Private Sub texteControl_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        If e.Button = MouseButtons.Right And Me.ReadOnly = False And showMenu = True Then
            menucliquedroit.Show(Me, New Point(e.X, e.Y))
        ElseIf e.Button = MouseButtons.Right Then
            menuCopy.Show(Me, New Point(e.X, e.Y))
        End If
    End Sub

    Private Sub zooming_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuZoom1.Click, menuZoom2.Click, menuZoom3.Click, menuZoom4.Click, menuZoom5.Click, menuZoom6.Click, menuZoom7.Click, menuZoom8.Click, menuZoom9.Click, menuZoom10.Click
        Dim i As Byte
        For i = 0 To 9
            CType(sender, MenuItem).Parent.MenuItems(i).Checked = False
        Next i
        CType(sender, MenuItem).Checked = True
        Me.ZoomFactor = CType(sender, MenuItem).Text / 2 + 0.5
    End Sub

End Class
