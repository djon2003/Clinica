Imports System

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports CI.Clinica



'''<summary>
'''Classe de test pour ScheduleTest, destinée à contenir tous
'''les tests unitaires ScheduleTest
'''</summary>
<TestClass()> _
Public Class ScheduleTest


    Private testContextInstance As TestContext

    '''<summary>
    '''Obtient ou définit le contexte de test qui fournit
    '''des informations sur la série de tests active ainsi que ses fonctionnalités.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = Value
        End Set
    End Property

#Region "Attributs de tests supplémentaires"
    '
    'Vous pouvez utiliser les attributs supplémentaires suivants lors de l'écriture de vos tests :
    '
    'Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test dans la classe
    '<ClassInitialize()>  _
    'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    'End Sub
    '
    'Utilisez ClassCleanup pour exécuter du code après que tous les tests ont été exécutés dans une classe
    '<ClassCleanup()>  _
    'Public Shared Sub MyClassCleanup()
    'End Sub
    '
    'Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test
    <TestInitialize()> _
    Public Sub MyTestInitialize()
        Software.startForTest()
        SchedulesManager.getInstance()
    End Sub
    '
    'Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
    '<TestCleanup()>  _
    'Public Sub MyTestCleanup()
    'End Sub
    '
#End Region


    '''<summary>
    '''Test pour setOpened
    '''</summary>
    <TestMethod()> _
    Public Sub setOpenedTest()
        Dim timeTested As Date = Date.Now
        Dim curSchedule As Schedule = SchedulesManager.getInstance.getSchedule(0, LIMIT_DATE)

        For i As Integer = 1 To 60 * 24 * 7
            curSchedule.setOpened(timeTested, True)
            Assert.IsTrue(curSchedule.isOpened(timeTested))

            curSchedule.setOpened(timeTested, False)
            Assert.IsFalse(curSchedule.isOpened(timeTested))

            timeTested = timeTested.AddMinutes(1)
        Next
    End Sub

End Class
