Public Class ModelesManager
    Inherits ManagerBase(Of ModelesManager)

    Private modelesCategories As New Generic.List(Of ModeleCategorie)

    Protected Sub New()
        load()
    End Sub

    Private Sub load()
        Dim ModelesCatDataSet As System.Data.DataSet = DBLinker.GetInstance.ReadDBForGrid("ModelesCategories ORDER BY Categorie", "*", , , , "ModelesCategories", ModelesCatDataSet)

        modelesCategories.Clear()
        For Each curRow As DataRow In ModelesCatDataSet.Tables(0).Rows
            modelesCategories.Add(New ModeleCategorie(curRow("NoCategorie"), curRow("Categorie")))
        Next
    End Sub

    Public Function getModelesCategories(ByVal noModeleCategorie() As Integer) As Generic.List(Of ModeleCategorie)
        Dim cats As New Generic.List(Of ModeleCategorie)
        For Each curCat As ModeleCategorie In modelesCategories
            If noModeleCategorie Is Nothing OrElse Array.IndexOf(noModeleCategorie, curCat.noCategorie) <> -1 Then
                cats.Add(curCat)
            End If
        Next

        Return cats
    End Function

    Public Function createModelesMenu(ByVal noModeleCategorie() As Integer, ByVal genEventHandler As EventHandler, ByVal persoEventHandler As EventHandler) As Windows.Forms.ContextMenu
        Dim i, M, n As Integer
        Dim MyMenuItem As New MenuItem()
        Dim MenuAucunAdded As Boolean = True
        n = 0
        M = 0
        Dim MyMenuModele As New ContextMenu()
        Dim MyMenuItemToAddToGen, MyMenuItemToAddToPerso As MenuItem
        Dim Modeles As DataSet = DBLinker.GetInstance().ReadDBForGrid("Modeles INNER JOIN ModelesCategories ON ModelesCategories.NoCategorie = Modeles.NoCategorie", "Modeles.NoModele,Modeles.NoCategorie,Categorie,Modeles.Nom,Modeles.NoUser", "WHERE (ModelesCategories.NoCategorie IN (" & String.Join(",", Array.ConvertAll(noModeleCategorie, New Converter(Of Integer, String)(AddressOf Convert.ToString))) & ")) AND (NoUser IS null OR NoUser=" & CurrentUser & ") ORDER BY NoUser DESC,Modeles.NoCategorie DESC, Nom")

        With MyMenuModele
            .MenuItems.Add("Généraux")
            .MenuItems.Add("Utilisateur")
        End With
        If Modeles Is Nothing Then Return MyMenuModele

        Dim CurSubMenu As Integer = -1
        Dim Changed As Boolean = False
        Dim LastNoCat As Integer = -1
        Dim CurMenuItem As MenuItem = MyMenuModele.MenuItems(1)
        Dim menuEvent As EventHandler
        With Modeles.Tables(0).Rows
            For i = 0 To .Count - 1
                If .Item(i)("NoUser") Is DBNull.Value Then
                    If CurMenuItem IsNot MyMenuModele.MenuItems(0) Then LastNoCat = -1
                    CurMenuItem = MyMenuModele.MenuItems(0)
                    menuEvent = genEventHandler
                Else
                    If CurMenuItem IsNot MyMenuModele.MenuItems(1) Then LastNoCat = -1
                    CurMenuItem = MyMenuModele.MenuItems(1)
                    menuEvent = persoEventHandler
                End If

                If LastNoCat > 1 AndAlso .Item(i)("NoCategorie") = 1 Then
                    CurMenuItem.MenuItems.Add(New MenuItem("-"))
                End If

                If .Item(i)("NoCategorie") = 1 Then CurSubMenu = -1

                If LastNoCat <> .Item(i)("NoCategorie") And .Item(i)("NoCategorie") <> 1 Then
                    CurSubMenu = CurMenuItem.MenuItems.Add(New MenuItem(.Item(i)("Categorie"), menuEvent))
                End If

                If CurSubMenu <> -1 Then
                    CurMenuItem.MenuItems(CurSubMenu).MenuItems.Add(.Item(i)("Nom").ToString, menuEvent)
                Else
                    CurMenuItem.MenuItems.Add(.Item(i)("Nom").ToString, menuEvent)
                End If

                LastNoCat = .Item(i)("NoCategorie")
            Next i
        End With

        MyMenuItemToAddToGen = Nothing
        MyMenuItemToAddToPerso = Nothing

        Return MyMenuModele
    End Function

End Class
