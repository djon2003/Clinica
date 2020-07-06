Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports CI.Clinica



'''<summary>
'''Classe de test pour SchedulesManagerTest, destinée à contenir tous
'''les tests unitaires SchedulesManagerTest
'''</summary>
<TestClass()> _
Public Class SchedulesManagerTest


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
    End Sub
    '
    'Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
    '<TestCleanup()>  _
    'Public Sub MyTestCleanup()
    'End Sub
    '
#End Region


    '''<summary>
    '''Test pour Constructeur SchedulesManager
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Clinica.exe")> _
    Public Sub SchedulesManagerConstructorTest()
        Try
            SchedulesManager.getInstance()
            Assert.IsTrue(True)
        Catch ex As Exception
            Assert.Fail("Loading breaks." & ex.Message)
        End Try
    End Sub

    '''<summary>
    '''Test loading speeding of schedules manager
    '''</summary>
    <TestMethod(), _
     DeploymentItem("Clinica.exe")> _
    Public Sub SchedulesManagerLoadSpeedTest()
        Dim start As Date = Date.Now
        SchedulesManager.getInstance()
        Dim ending As Date = Date.Now

        Dim firstTime As Double = TimeSpan.FromTicks(ending.Ticks - start.Ticks).TotalMilliseconds
        Dim nbLoop As Integer = 10
        Dim totalSeconds As Double = 0
        For i As Integer = 1 To nbLoop
            start = Date.Now
            SchedulesManager.getInstance.load()
            ending = Date.Now
            totalSeconds += TimeSpan.FromTicks(ending.Ticks - start.Ticks).TotalMilliseconds
        Next i

        totalSeconds /= nbLoop

        Dim outText As String = "Total milliseconds (mean time of " & nbLoop & "): " & totalSeconds & vbCrLf & _
                                "Total milliseconds (first time) : " & firstTime & vbCrLf & _
                                "Total milliseconds of DB (previous test) : 357,8125" & vbCrLf & _
                                "Total milliseconds (base time before change): 2409,375" & vbCrLf & _
                                "Total milliseconds (base time before change minus copyArray of days within Schedule class load method): 548,4375"


        Console.WriteLine(outText)
        Assert.IsTrue(True, outText)
    End Sub
End Class
