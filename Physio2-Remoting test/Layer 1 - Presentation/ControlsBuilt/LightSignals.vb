Public Class LightSignals
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
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
    Friend WithEvents Feux1 As System.Windows.Forms.Panel
    Friend WithEvents Feux2 As System.Windows.Forms.Panel
    Friend WithEvents Feux3 As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.Feux1 = New System.Windows.Forms.Panel
        Me.Feux2 = New System.Windows.Forms.Panel
        Me.Feux3 = New System.Windows.Forms.Panel
        Me.SuspendLayout()
        '
        'Feux1
        '
        Me.Feux1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Feux1.Location = New System.Drawing.Point(0, 0)
        Me.Feux1.Name = "Feux1"
        Me.Feux1.Size = New System.Drawing.Size(9, 16)
        Me.Feux1.TabIndex = 0
        '
        'Feux2
        '
        Me.Feux2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Feux2.Location = New System.Drawing.Point(8, 0)
        Me.Feux2.Name = "Feux2"
        Me.Feux2.Size = New System.Drawing.Size(9, 16)
        Me.Feux2.TabIndex = 1
        '
        'Feux3
        '
        Me.Feux3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Feux3.Location = New System.Drawing.Point(16, 0)
        Me.Feux3.Name = "Feux3"
        Me.Feux3.Size = New System.Drawing.Size(9, 16)
        Me.Feux3.TabIndex = 2
        '
        'Feux
        '
        Me.Controls.Add(Me.Feux3)
        Me.Controls.Add(Me.Feux2)
        Me.Controls.Add(Me.Feux1)
        Me.Name = "Feux"
        Me.Size = New System.Drawing.Size(25, 16)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private _ColorLibre, _ColorRV, _ColorPresence, _ColorClinique, _ColorPresencePaye As Color
    Private checkSum As Byte = 0
    Private al As Boolean = False
    Private arv As Boolean = False
    Private ap As Boolean = False
    Private app As Boolean = False

#Region "Propriétés"
    Public Property colorClinique() As Color
        Get
            Return _ColorClinique
        End Get
        Set(ByVal Value As Color)
            _ColorClinique = Value
        End Set
    End Property

    Public Property colorLibre() As Color
        Get
            Return _ColorLibre
        End Get
        Set(ByVal Value As Color)
            _ColorLibre = Value
        End Set
    End Property

    Public Property colorRV() As Color
        Get
            Return _ColorRV
        End Get
        Set(ByVal Value As Color)
            _ColorRV = Value
        End Set
    End Property

    Public Property colorPresence() As Color
        Get
            Return _ColorPresence
        End Get
        Set(ByVal Value As Color)
            _ColorPresence = Value
        End Set
    End Property

    Public Property colorPresencePaye() As Color
        Get
            Return _ColorPresencePaye
        End Get
        Set(ByVal Value As Color)
            _ColorPresencePaye = Value
        End Set
    End Property

    Public WriteOnly Property activePresencePaye() As Boolean
        Set(ByVal Value As Boolean)
            If Value <> app Then
                app = Value
                If Value = True Then
                    checkSum += 13
                Else
                    checkSum -= 13
                End If
            End If
        End Set
    End Property

    Public WriteOnly Property activeLibre() As Boolean
        Set(ByVal Value As Boolean)
            If Value <> al Then

                al = Value
                If al = True Then
                    checkSum += 1
                Else
                    checkSum -= 1
                End If
            End If
        End Set
    End Property

    Public WriteOnly Property activeRV() As Boolean
        Set(ByVal Value As Boolean)
            If Value <> arv Then
                arv = Value
                If arv = True Then
                    checkSum += 4
                Else
                    checkSum -= 4
                End If
            End If
        End Set
    End Property

    Public WriteOnly Property activePresence() As Boolean
        Set(ByVal Value As Boolean)
            If Value <> ap Then
                ap = Value
                If ap = True Then
                    checkSum += 7
                Else
                    checkSum -= 7
                End If
            End If
        End Set
    End Property
#End Region

    Public Sub coloring()
        Select Case checkSum
            Case 0
                Feux1.BackColor = colorClinique
                Feux2.BackColor = colorClinique
                Feux3.BackColor = colorClinique

            Case 1
                Feux1.BackColor = colorLibre
                Feux2.BackColor = colorLibre
                Feux3.BackColor = colorLibre

            Case 4
                Feux1.BackColor = colorRV
                Feux2.BackColor = colorRV
                Feux3.BackColor = colorRV

            Case 5
                Feux1.BackColor = colorLibre
                Feux2.BackColor = colorLibre
                Feux3.BackColor = colorRV

            Case 7
                Feux1.BackColor = colorPresence
                Feux2.BackColor = colorPresence
                Feux3.BackColor = colorPresence

            Case 8
                Feux1.BackColor = colorLibre
                Feux2.BackColor = colorLibre
                Feux3.BackColor = colorPresence

            Case 11
                Feux1.BackColor = colorRV
                Feux2.BackColor = colorRV
                Feux3.BackColor = colorPresence

            Case 12
                Feux1.BackColor = colorLibre
                Feux2.BackColor = colorRV
                Feux3.BackColor = colorPresence

            Case 13
                Feux1.BackColor = colorPresencePaye
                Feux2.BackColor = colorPresencePaye
                Feux3.BackColor = colorPresencePaye

            Case 14
                Feux1.BackColor = colorLibre
                Feux2.BackColor = colorLibre
                Feux3.BackColor = colorPresencePaye

            Case 17
                Feux1.BackColor = colorRV
                Feux2.BackColor = colorRV
                Feux3.BackColor = colorPresencePaye

            Case 18
                Feux1.BackColor = colorLibre
                Feux2.BackColor = colorRV
                Feux3.BackColor = colorPresencePaye

            Case 20
                Feux1.BackColor = colorPresence
                Feux2.BackColor = colorPresence
                Feux3.BackColor = colorPresencePaye

            Case 21
                Feux1.BackColor = colorLibre
                Feux2.BackColor = colorPresence
                Feux3.BackColor = colorPresencePaye

            Case 24
                Feux1.BackColor = colorRV
                Feux2.BackColor = colorPresence
                Feux3.BackColor = colorPresencePaye

            Case 25 'All codes activated (4 codes) skip ColorPresencePaye
                Feux1.BackColor = colorLibre
                Feux2.BackColor = colorRV
                Feux3.BackColor = colorPresence
        End Select
    End Sub

    Private Sub feux_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        Me.Size = New Size(25, 16)
    End Sub
End Class
