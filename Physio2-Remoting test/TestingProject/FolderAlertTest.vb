Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports CyberInternautes.Clinica.Accounts.Clients.Folders



'''<summary>
'''Classe de test pour FolderAlertTest, destin�e � contenir tous
'''les tests unitaires FolderAlertTest
'''</summary>
<TestClass()>  _
Public Class FolderAlertTest
    

Private testContextInstance As TestContext

'''<summary>
'''Obtient ou d�finit le contexte de test qui fournit
'''des informations sur la s�rie de tests active ainsi que ses fonctionnalit�s.
'''</summary>
Public Property testContext() As TestContext
    Get
        Return testContextInstance
    End Get
    Set
        testContextInstance = value
    End Set
End Property

#Region "Attributs de tests suppl�mentaires"
'
'Vous pouvez utiliser les attributs suppl�mentaires suivants lors de l'�criture de vos tests�:
'
'Utilisez ClassInitialize pour ex�cuter du code avant d'ex�cuter le premier test dans la classe
'<ClassInitialize()>  _
'Public Shared Sub myClassInitialize(ByVal testContext As TestContext)
'End Sub
'
'Utilisez ClassCleanup pour ex�cuter du code apr�s que tous les tests ont �t� ex�cut�s dans une classe
'<ClassCleanup()>  _
'Public Shared Sub myClassCleanup()
'End Sub
'
'Utilisez TestInitialize pour ex�cuter du code avant d'ex�cuter chaque test
'<TestInitialize()>  _
'Public Sub myTestInitialize()
'End Sub
'
'Utilisez TestCleanup pour ex�cuter du code apr�s que chaque test a �t� ex�cut�
'<TestCleanup()>  _
'Public Sub myTestCleanup()
'End Sub
'
#End region

End Class
