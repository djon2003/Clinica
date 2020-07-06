Imports CI.Base
Imports System.ComponentModel

<Serializable()> _
Public Class Config
    Inherits ConfigBase

    Private _doMemoryTest As Boolean = False
    Private _noVisiteForTransactionTest As Integer = 0
    Private _actionForTransactionTest As ActionTypes = ActionTypes.RollBack

    Public Shared ReadOnly Property current() As Config
        Get
            Return CType(ConfigurationsManager.getInstance().getItemable(GetType(Config)), Object)
        End Get
    End Property


    Public Enum ActionTypes As Integer
        RollBack = 0
        Commit = 1
    End Enum

    <CategoryAttribute("Transaction test"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(True), _
    DescriptionAttribute("SQL action to apply")> _
    Public Property actionForTransactionTest() As ActionTypes
        Get
            Return _actionForTransactionTest
        End Get
        Set(ByVal value As ActionTypes)
            _actionForTransactionTest = value
        End Set
    End Property

    <CategoryAttribute("Transaction test"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(0), _
    DescriptionAttribute("SQL NoVisite of R-V to test")> _
    Public Property noVisiteForTransactionTest() As Integer
        Get
            Return _noVisiteForTransactionTest
        End Get
        Set(ByVal value As Integer)
            If value < 0 Then value = 0
            _noVisiteForTransactionTest = value
        End Set
    End Property

    <CategoryAttribute("Memory error test"), _
    Browsable(True), _
    BindableAttribute(False), _
    DefaultValueAttribute(0), _
    DescriptionAttribute("Activate test")> _
    Public Property doMemoryTest() As Boolean
        Get
            Return _doMemoryTest
        End Get
        Set(ByVal value As Boolean)
            _doMemoryTest = value
        End Set
    End Property

    Protected Overrides Function isFieldsCorrectlyFilled() As Boolean
        Return True
    End Function
End Class
