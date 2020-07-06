Imports CI.Base
Imports System.ComponentModel

<Serializable()> _
Public Class Params
    Inherits ParametersBase

    Private Const EXTERNAL_STATUS_KEY As String = "Automatisation du transfert de dossier de la CSST"
    Public Const RESET_NUMBER_FOR_NEXT_FILE_NUMBER As Integer = 100000

    Private Shared mySelf As Params
    Private _nextFileNumber As Integer = 1
    Private _textType_noInitialReport, _textType_noStepReport, _textType_noFinalReport As Integer
    Private _markedAsConfirmed, _markedAsUploaded, _markedAsNotProcessed, _markedAsRefused, _markedAsWaiting As Integer
    Private _lastReturnDateEnsured As Date = LIMIT_DATE

    Private Sub New()
        MyBase.New()
    End Sub

#Region "Properties"

    <Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute("")> _
    Public Property textType_noInitialReport() As Integer
        Get
            Return _textType_noInitialReport
        End Get
        Set(ByVal value As Integer)
            _textType_noInitialReport = value
        End Set
    End Property

    <Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute("")> _
    Public Property textType_noStepReport() As Integer
        Get
            Return _textType_noStepReport
        End Get
        Set(ByVal value As Integer)
            _textType_noStepReport = value
        End Set
    End Property

    <Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute("")> _
    Public Property textType_noFinalReport() As Integer
        Get
            Return _textType_noFinalReport
        End Get
        Set(ByVal value As Integer)
            _textType_noFinalReport = value
        End Set
    End Property

    <Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute("")> _
    Public Property nextFileNumber() As Integer
        Get
            Return _nextFileNumber
        End Get
        Set(ByVal value As Integer)
            If value >= RESET_NUMBER_FOR_NEXT_FILE_NUMBER Then value = 1
            _nextFileNumber = value
        End Set
    End Property

    <Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute("")> _
    Public Property markedAsWaiting() As Integer
        Get
            Return _markedAsWaiting
        End Get
        Set(ByVal value As Integer)
            _markedAsWaiting = value
        End Set
    End Property

    <Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute("")> _
    Public Property markedAsRefused() As Integer
        Get
            Return _markedAsRefused
        End Get
        Set(ByVal value As Integer)
            _markedAsRefused = value
        End Set
    End Property

    <Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute("")> _
    Public Property markedAsNotProcessed() As Integer
        Get
            Return _markedAsNotProcessed
        End Get
        Set(ByVal value As Integer)
            _markedAsNotProcessed = value
        End Set
    End Property

    <Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute("")> _
    Public Property markedAsUploaded() As Integer
        Get
            Return _markedAsUploaded
        End Get
        Set(ByVal value As Integer)
            _markedAsUploaded = value
        End Set
    End Property

    <Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute("")> _
    Public Property markedAsConfirmed() As Integer
        Get
            Return _markedAsConfirmed
        End Get
        Set(ByVal value As Integer)
            _markedAsConfirmed = value
        End Set
    End Property

#End Region

    Public Overrides Sub load()
        MyBase.load()

        If Me.neverConfiged Then
            'Install external statuses needed by CSST automation plugin
            DBLinker.getInstance.writeDB("ExternalStatuses", "ExternalStatus,ExternalKey", "'Non envoyé(e) à la CSST', '" + EXTERNAL_STATUS_KEY.Replace("'", "''") + "'", , , , Me.markedAsNotProcessed)
            DBLinker.getInstance.writeDB("ExternalStatuses", "ExternalStatus,ExternalKey", "'Envoyé(e) à la CSST', '" + EXTERNAL_STATUS_KEY.Replace("'", "''") + "'", , , , Me.markedAsUploaded)
            DBLinker.getInstance.writeDB("ExternalStatuses", "ExternalStatus,ExternalKey", "'Accepté(e) par la CSST', '" + EXTERNAL_STATUS_KEY.Replace("'", "''") + "'", , , , Me.markedAsConfirmed)
            DBLinker.getInstance.writeDB("ExternalStatuses", "ExternalStatus,ExternalKey", "'En attente de la CSST', '" + EXTERNAL_STATUS_KEY.Replace("'", "''") + "'", , , , Me.markedAsWaiting)
            DBLinker.getInstance.writeDB("ExternalStatuses", "ExternalStatus,ExternalKey", "'Refusé(e) par la CSST', '" + EXTERNAL_STATUS_KEY.Replace("'", "''") + "'", , , , Me.markedAsRefused)
            
            'TODO : Missing installation code below

            'Add foldertexttypes required by module
            addFolderTextTypes()

            'Add models required by foldertexts generated upon foldertexttypes added previously

            'Ensure all old data is marked as finished and new one marked as not done.

            'Save params
            Me.save()
        End If

        If Me.hasToUpdate Then
            If textType_noFinalReport = 0 Then
                Dim nos As DataSet = Base.DBLinker.getInstance.readDBForGrid("FolderTexteTypes", "*", "WHERE TexteTitle LIKE 'Rapport CSST%'")
                If nos Is Nothing OrElse nos.Tables.Count = 0 OrElse nos.Tables(0).Rows.Count = 0 Then
                    addFolderTextTypes()
                Else
                    For Each curRow As DataRow In nos.Tables(0).Rows
                        If curRow("TexteTitle") = "Rapport CSST initial" Then
                            _textType_noInitialReport = curRow("NoFolderTexteType")
                        ElseIf curRow("TexteTitle") = "Rapport CSST d'étape" Then
                            _textType_noStepReport = curRow("NoFolderTexteType")
                        ElseIf curRow("TexteTitle") = "Rapport CSST final" Then
                            _textType_noFinalReport = curRow("NoFolderTexteType")
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Private Sub addFolderTextTypes()

    End Sub

    Public Shared ReadOnly Property current() As Params
        Get
            If mySelf Is Nothing Then
                mySelf = New Params()
                mySelf.load()
            End If

            Return mySelf
        End Get
    End Property

    Protected Overrides Function getTypeName() As String
        Return ""
    End Function

    Protected Overrides Function isFieldsCorrectlyFilled() As Boolean
        Return True
    End Function
End Class
