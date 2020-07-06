
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Windows.Forms
Imports System.Drawing
Imports System.ComponentModel
Imports System.Reflection


'=======================================================
'Converted by ...
'Service provided by Telerik (www.telerik.com)
'Conversion powered by NRefactory.
'Twitter: @telerik, @toddanglin
'Facebook: facebook.com/telerik
'=======================================================

Namespace Windows.Forms


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>Class provided by http://www.codeproject.com/articles/117021/how-to-create-progressbar-column-in-datagridview</remarks>
    Public Class DataGridViewProgressColumn
        Inherits DataGridViewImageColumn
        Public Shared _ProgressBarColor As Color

        Public Sub New()
            CellTemplate = New DataGridViewProgressCell()
            Me.ValueType = GetType(Double)
        End Sub

        Public Overrides Property CellTemplate() As DataGridViewCell
            Get
                Return MyBase.CellTemplate
            End Get
            Set(ByVal value As DataGridViewCell)
                If value IsNot Nothing AndAlso Not value.[GetType]().IsAssignableFrom(GetType(DataGridViewProgressCell)) Then
                    Throw New InvalidCastException("Must be a DataGridViewProgressCell")
                End If
                MyBase.CellTemplate = value
            End Set
        End Property


        <Browsable(True)> _
        Public Property ProgressBarColor() As Color
            Get

                If Me.ProgressBarCellTemplate Is Nothing Then
                    Throw New InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
                End If

                Return Me.ProgressBarCellTemplate.ProgressBarColor
            End Get
            Set(ByVal value As Color)

                If Me.ProgressBarCellTemplate Is Nothing Then
                    Throw New InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
                End If
                Me.ProgressBarCellTemplate.ProgressBarColor = value
                If Me.DataGridView IsNot Nothing Then
                    Dim dataGridViewRows As DataGridViewRowCollection = Me.DataGridView.Rows
                    Dim rowCount As Integer = dataGridViewRows.Count
                    For rowIndex As Integer = 0 To rowCount - 1
                        Dim dataGridViewRow As DataGridViewRow = dataGridViewRows.SharedRow(rowIndex)
                        Dim dataGridViewCell As DataGridViewProgressCell = TryCast(dataGridViewRow.Cells(Me.Index), DataGridViewProgressCell)
                        If dataGridViewCell IsNot Nothing Then
                            dataGridViewCell.SetProgressBarColor(rowIndex, value)
                        End If
                    Next
                    ' TODO: This column and/or grid rows may need to be autosized depending on their
                    '       autosize settings. Call the autosizing methods to autosize the column, rows, 
                    '       column headers / row headers as needed.
                    Me.DataGridView.InvalidateColumn(Me.Index)
                End If
            End Set
        End Property

        Private ReadOnly Property ProgressBarCellTemplate() As DataGridViewProgressCell
            Get

                Return DirectCast(Me.CellTemplate, DataGridViewProgressCell)
            End Get
        End Property
    End Class


End Namespace