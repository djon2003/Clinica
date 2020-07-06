Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports CyberInternautes.Clinica.Accounts.Clients.Folders



'''<summary>
'''Classe de test pour FolderAlertTest, destinée à contenir tous
'''les tests unitaires FolderAlertTest
'''</summary>
<TestClass()>  _
Public Class FolderAlertTest
    

Private testContextInstance As TestContext

'''<summary>
'''Obtient ou définit le contexte de test qui fournit
'''des informations sur la série de tests active ainsi que ses fonctionnalités.
'''</summary>
Public Property testContext() As TestContext
    Get
        Return testContextInstance
    End Get
    Set
        testContextInstance = value
    End Set
End Property

#Region "Attributs de tests supplémentaires"
'
'Vous pouvez utiliser les attributs supplémentaires suivants lors de l'écriture de vos tests :
'
'Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test dans la classe
'<ClassInitialize()>  _
'Public Shared Sub myClassInitialize(ByVal testContext As TestContext)
'End Sub
'
'Utilisez ClassCleanup pour exécuter du code après que tous les tests ont été exécutés dans une classe
'<ClassCleanup()>  _
'Public Shared Sub myClassCleanup()
'End Sub
'
'Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test
'<TestInitialize()>  _
'Public Sub myTestInitialize()
'End Sub
'
'Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
'<TestCleanup()>  _
'Public Sub myTestCleanup()
'End Sub
'
#End region

End Class
