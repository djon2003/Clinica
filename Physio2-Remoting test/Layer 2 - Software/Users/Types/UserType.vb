Public Class UserType
    Inherits DBItemableBase

    Private _noUserType As Integer
    Private _name As String = ""
    Private _rights As String = ""
    Private _isTherapist As Boolean = False

#Region "Properties"
    Public ReadOnly Property noUserType() As Integer
        Get
            Return _noUserType
        End Get
    End Property

    Public Property name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public Property rights() As String
        Get
            Return _rights
        End Get
        Set(ByVal value As String)
            _rights = value
        End Set
    End Property

    Public Property isTherapist() As Boolean
        Get
            Return _isTherapist
        End Get
        Set(ByVal value As Boolean)
            _isTherapist = value
        End Set
    End Property
#End Region

    Public Overrides Sub delete()
        If MessageBox.Show("Êtes-vous certain de vouloir enlever ce type ?", "Suppression d'un type", MessageBoxButtons.YesNo) = DialogResult.No Then Exit Sub

        'Vérifie s'il est utilisé
        Dim usersInUse() As String = DBLinker.getInstance.readOneDBField("Utilisateurs INNER JOIN TypeUtilisateur ON TypeUtilisateur.NoType = Utilisateurs.NoType", "NoUser", "WHERE ((NomType)='" & name.Replace("'", "''") & "');")
        If Not usersInUse Is Nothing AndAlso usersInUse.Length <> 0 Then
            MessageBox.Show("Impossible de supprimer ce type, il est présentement en cours d'utilisation.", "Suppression d'un type")
            Exit Sub
        End If

        DBLinker.getInstance.delDB("TypeUtilisateur", "NoType", _noUserType, False)
        onDeleted()
        If autoSendUpdateOnDelete Then UserTypeManager.getInstance.update()

        myMainWin.StatusText = "Types d'utilisateur : Type " & _name & " supprimé"
    End Sub

    Public Overrides Sub loadData(ByVal data As DBItemableData)
        Dim curData As DataRow = data.mainData

        _noUserType = curData("NoType")
        _name = curData("NomType")
        _rights = curData("DroitAcces")
        _isTherapist = curData("IsTherapist")
    End Sub

    Public Overrides Sub saveData()
        'Vérification qu'aucun utilisateur ayant ce type a déjà eu des rendez-vous
        If isTherapist = False Then
            Dim rVs(,) As String = DBLinker.getInstance.readDB("InfoVisites", "NoVisite", "WHERE NoTRP IN (SELECT NoUser FROM Utilisateurs,TypeUtilisateur WHERE Utilisateurs.NoType=TypeUtilisateur.NoType AND NomType='" & name.Replace("'", "''") & "')")
            If Not rVs Is Nothing AndAlso rVs.Length <> 0 Then
                MessageBox.Show("Un/des utilisateur(s) ayant ce type est(sont) lié(s) à des rendez-vous. Le type d'utilisateur doit prendre des clients.", "Type d'utilisateur")
                Exit Sub
            End If
        End If

        If _noUserType = 0 Then
            DBLinker.getInstance.writeDB("TypeUtilisateur", "NomType,DroitAcces,IsTherapist", "'" & name.Replace("'", "''") & "','" & _rights & "','" & _isTherapist & "'", , , , _noUserType)
            myMainWin.StatusText = "Ajout d'un type d'utilisateur"
        Else
            DBLinker.getInstance.updateDB("TypeUtilisateur", "NomType='" & name.Replace("'", "''") & "',DroitAcces='" & _rights & "',IsTherapist='" & _isTherapist & "'", "NoType", _noUserType, False)
            DBLinker.getInstance.updateDB("Utilisateurs", "Utilisateurs.DroitAcces = '" & rights & "', Utilisateurs.IsTherapist = '" & isTherapist & "'", "NoType", "(SELECT TOP 1 NoType FROM TypeUtilisateur WHERE NomType = '" & name.Replace("'", "''") & "')", False)

            onDataChanged()
            UsersManager.getInstance.update()
            myMainWin.StatusText = "Types d'utilisateur : Type " & _name & " modifié"
        End If

        If autoSendUpdateOnSave Then UserTypeManager.getInstance.update()
    End Sub

    Public Overrides Function toString() As String
        Return _name
    End Function

    Public Overrides ReadOnly Property noItemable() As Integer
        Get
            Return Me.noUserType
        End Get
    End Property
End Class
