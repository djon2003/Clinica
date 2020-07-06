Public Class ModelsManager
    Inherits ManagerBase(Of ModelsManager)

    Private modelesCategories As New Generic.List(Of ModelCategory)

    Protected Sub New()
        load()
    End Sub

    Private Sub load()
        Dim modelsCatDataSet As System.Data.DataSet = DBLinker.getInstance.readDBForGrid("ModelesCategories ORDER BY Categorie", "*", , , , "ModelesCategories", modelsCatDataSet)

        modelesCategories.Clear()
        For Each curRow As DataRow In modelsCatDataSet.Tables(0).Rows
            modelesCategories.Add(New ModelCategory(curRow("NoCategorie"), curRow("Categorie")))
        Next
    End Sub

    Public Function getModelsCategories(ByVal noModelCategory() As Integer) As Generic.List(Of ModelCategory)
        Dim cats As New Generic.List(Of ModelCategory)
        For Each curCat As ModelCategory In modelesCategories
            If noModelCategory Is Nothing OrElse Array.IndexOf(noModelCategory, curCat.noCategory) <> -1 Then
                cats.Add(curCat)
            End If
        Next

        Return cats
    End Function

    Public Function createModelsMenu(ByVal noModelCategory() As Integer, ByVal genEventHandler As EventHandler, Optional ByVal persoEventHandler As EventHandler = Nothing, Optional ByVal addNoneMenu As Boolean = False) As System.Windows.Forms.ContextMenu
        Dim i, M, n As Integer
        Dim myMenuItem As New MenuItem()
        Dim menuAucunAdded As Boolean = True
        n = 0
        M = 0
        Dim myMenuModele As New ContextMenu()
        Dim MyMenuItemToAddToGen, myMenuItemToAddToPerso As MenuItem
        Dim noUserCondition As String = "AND (NoUser IS null" & If(persoEventHandler Is Nothing, "", " OR NoUser=" & ConnectionsManager.currentUser) & ")"
        Dim modeles As DataSet = DBLinker.getInstance().readDBForGrid("modeles INNER JOIN modelesCategories ON modelesCategories.NoCategorie = modeles.NoCategorie", "modeles.NoModele,modeles.NoCategorie,Categorie,modeles.Nom,modeles.NoUser", "WHERE (modelesCategories.NoCategorie IN (" & String.Join(",", Array.ConvertAll(noModelCategory, New Converter(Of Integer, String)(AddressOf Convert.ToString))) & ")) " & noUserCondition & " ORDER BY NoUser DESC,modeles.NoCategorie DESC, Nom")

        With myMenuModele
            .MenuItems.Add("Généraux")
            If persoEventHandler IsNot Nothing Then .MenuItems.Add("Utilisateur")
            If addNoneMenu Then
                .MenuItems.Add("-")
                .MenuItems.Add("Aucun", genEventHandler) '.Tag = 0
            End If
        End With
        If modeles Is Nothing Then Return myMenuModele

        Dim curSubMenu As Integer = -1
        Dim changed As Boolean = False
        Dim lastNoCat As Integer = -1
        Dim curMenuItem As MenuItem = myMenuModele.MenuItems(0)
        Dim menuEvent As EventHandler
        With modeles.Tables(0).Rows
            For i = 0 To .Count - 1
                If .Item(i)("NoUser") Is DBNull.Value Then
                    If curMenuItem IsNot myMenuModele.MenuItems(0) Then lastNoCat = -1
                    curMenuItem = myMenuModele.MenuItems(0)
                    menuEvent = genEventHandler
                Else
                    If curMenuItem IsNot myMenuModele.MenuItems(1) Then lastNoCat = -1
                    curMenuItem = myMenuModele.MenuItems(1)
                    menuEvent = persoEventHandler
                End If

                If lastNoCat > 1 AndAlso .Item(i)("NoCategorie") = 1 Then
                    curMenuItem.MenuItems.Add(New MenuItem("-"))
                End If

                If .Item(i)("NoCategorie") = 1 Then curSubMenu = -1

                If lastNoCat <> .Item(i)("NoCategorie") And .Item(i)("NoCategorie") <> 1 Then
                    curSubMenu = curMenuItem.MenuItems.Add(New MenuItem(.Item(i)("Categorie"), menuEvent))
                End If

                Dim modelMenu As New MenuItem(.Item(i)("Nom"), menuEvent)
                modelMenu.Tag = .Item(i)("NoModele")

                If curSubMenu <> -1 Then
                    curMenuItem.MenuItems(curSubMenu).MenuItems.Add(modelMenu)
                Else
                    curMenuItem.MenuItems.Add(modelMenu)
                End If

                lastNoCat = .Item(i)("NoCategorie")
            Next i
        End With

        MyMenuItemToAddToGen = Nothing
        myMenuItemToAddToPerso = Nothing

        Return myMenuModele
    End Function

End Class
