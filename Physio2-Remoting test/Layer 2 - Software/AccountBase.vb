Public MustInherit Class AccountBase

#Region "Definitions"
    Private _noAccount As Integer = 0
#End Region

#Region "Constructors"
    Public Sub New()
    End Sub

    Public Sub New(ByVal noAccount As Integer)
        Me._noAccount = noAccount
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property noAccount() As Integer
        Get
            Return _noAccount
        End Get
    End Property
#End Region

    Protected MustOverride Function getLockSectorFileNames() As String()

    Public Function isSectorLocked() As Boolean
        Dim lockSectorFileNames() As String = getLockSectorFileNames()
        For Each curFileName As String In lockSectorFileNames
            curFileName &= "-" & Me.noAccount & "-"

            If lockedVerification(curFileName, True) Then Return True
        Next

        Return False
    End Function
End Class
