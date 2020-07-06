Option Strict Off
Option Explicit On 

Imports Microsoft.Win32.Registry
Imports System

Module CommonProc

#Region "Propriétés"
    Private waitingUpdateNo As New Object
    Private waitingNoTriggerNo As New Object

    Public Property updateNo() As Integer
        Get
            Return _UpdateNo
        End Get
        Set(ByVal Value As Integer)
            SyncLock (WaitingUpdateNo)
                _UpdateNo = Value
            End SyncLock
        End Set
    End Property

    Public Property noTriggerNo() As Integer
        Get
            Return _NoTriggerNo
        End Get
        Set(ByVal Value As Integer)
            SyncLock (WaitingNoTriggerNo)
                _NoTriggerNo = Value
            End SyncLock
        End Set
    End Property
#End Region

#Region "Définitions"

    Public tmModified As Boolean = False
    Public emptyHTMLPath As String
    Public Const enterChar As String = Chr(13) & Chr(10)
    Public Const limitDate As Date = #1/27/9999#
    Public nomsJours() As String = {"Dimanche", "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi", "Samedi"}
    Public nomsMois() As String = {"Janvier", "Février", "Mars", "Avril", "Mai", "Juin", "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre"}
    Public infoDivers(,) As String

    Public myRegEx As System.Text.RegularExpressions.Regex
    Public firstUsageDate As Date
    Public appPath As String
    Public currentDroitAcces() As Boolean
    Public currentUser As Integer 'Utilisateur courant
    Public currentUserName As String 'Nom de l'utilisateur courant
    Public maxi As Byte 'maximum de caractères pour le test de mot de passe
    Public noTemp As Object
    Public pref() As Object 'Préférences cliniques
    Public prefUser() As Object 'Préférences utilisateurs
    Public justAppliedFocus As Boolean
    Public forceShowAdmin As Boolean = False
    Public firstLoading As Boolean = True
    Public noTypes() As String
    Public openedNewWindow As Boolean = False 'Indique si la fonction OpenUniqueWindow ouvre une nouvelle fenêtre (true) ou sélectionne une existante (false)
    Public userSettings(,) As String 'Paramètres de l'utilisateur connecté
    Public _UpdateNo As Integer = 0
    Public _NoTriggerNo As Integer = 0

    Public Structure ClientKeys
        Dim nam As String
        Dim noClient As Integer
        Dim fullName As String
    End Structure
    Public Structure KeyPeopleKeys
        Dim noKP As Integer
        Dim kpName As String
    End Structure
    Public Structure AnalysedExpression
        Dim conditions As String
        Dim selTables() As String
        Dim ErrorPos, errorLength As Integer
    End Structure
    Public Structure PositionType
        Dim x As Single
        Dim y As Single
    End Structure
    Public foundClient() As ClientKeys
    Public keyPeopleFrom As Object
    Public codificationFrom As Object
    Public fileContent() As String
    Public lineGen As Short
    Public linePerso As Short
    
    Public Enum FactureAction
        Adjusted = 0
        Created = 1
        Deleted = 2
        Err = 3
    End Enum
    Public Enum AType
        OpenMailSystem = 0
        OpenUserAccount = 1
        OpenNote = 2
        OpenUserAccountWithExpiry = 3
        OpenRapportGenerator = 4
        ContinueQueueListe = 5
    End Enum

    Public Enum WTType
        First = 0
        Last = 1
    End Enum

    Public Enum SType
        Regular_Expression = 0
        Normal = 1
    End Enum

    Public Enum RRType
        Files = 0
        Dirs = 1
        Both = 2
    End Enum

    Public Enum OpeningType
        FILE = 0
        DB = 1
        RAPPORT = 2
        EMAIL = 3
        HTMLPAGE = 4
    End Enum
#End Region

#Region "Horaire"
    
#End Region

#Region "Mot de passe"
    Public Function mdpProcessToModif(ByRef cle As String, ByRef mdp As String) As String
        Dim NoG, NoD, NewMDP, PartieG, PartieD, G, D, DPart, gPart As String
        NoG = "" : NoD = "" : NewMDP = "" : PartieG = "" : PartieD = "" : G = "" : D = "" : DPart = "" : GPart = ""
        Dim lenD, lenG, Max, l As Integer
        Dim i, j As Short
        Dim T4, T2, T1, T3, T5, t6 As Integer
        Dim MultipleG, multipleD As Double
        'Dans la procédure ci-dessous G signifie Gauche
        'Dans la procédure ci-dessous D signifie Droit(e)

        'Détermination des Multiplicateurs pour la partie gauche et droite
        'du mot de passe
        T1 = CInt(Left(Cle, 4))
        T2 = CInt(Mid(Cle, 6, 2))
        T3 = CInt(Mid(Cle, 9, 2))
        T4 = CInt(Mid(Cle, 11, 2))
        T5 = CInt(Mid(Cle, 14, 2))
        T6 = CInt(Right(Cle, 2))
        MultipleD = (T1 * 1000 + T2 * 100 + T3) / 10000
        MultipleG = T4 + T5 + T6

        'Détermination de la séparation du mot de passe
        'Également si la longueur est paire
        'Un caractère de plus à gauche si impaire
        L = Len(mdp)
        If VerifImpair(L) Then
            LenD = Int(L / 2)
            LenG = LenD + 1
        Else
            LenD = L / 2
            LenG = L / 2
        End If

        'Transformation en nombre des parties G & D du mot de passe
        PartieG = Left(mdp, LenG)
        PartieD = Right(mdp, LenD)

        For i = 1 To LenG
            NoG = NoG & Asc(Mid(PartieG, i, 1))
        Next i
        For j = 1 To LenD
            NoD = NoD & Asc(Mid(PartieD, j, 1))
        Next j
        If NoG = "" Then NoG = "0"
        If NoD = "" Then NoD = "0"

        'Multiplacation des parties G & D par les multiplicateurs
        NoG = NoG * MultipleG
        NoD = NoD * MultipleD

        NewMDP = CStr(NoG & NoD)

        'Inversion des caractères deux à deux si le premier chiffre est impair
        If VerifImpair(Left(NewMDP, 1)) Then
            'Vérification si longueur impair du NewMDP
            If VerifImpair(Len(NewMDP)) Then
                Max = Len(NewMDP) - 1
            Else
                Max = Len(NewMDP)
            End If

            For i = 1 To Max Step 2
                G = Mid(NewMDP, i, 1)
                D = Mid(NewMDP, i + 1, 1)
                GPart = Left(NewMDP, i - 1)
                DPart = Right(NewMDP, Len(NewMDP) - i - 1)
                NewMDP = GPart & D & G & DPart
            Next i

            Return NewMDP
        Else
            Return NewMDP
        End If
    End Function

#End Region

    Public Sub addErrorLog(ByVal errorMsg As Exception, Optional ByVal internalCount As Byte = 0)

    End Sub
#Region "Types de fichiers"
   
#End Region


End Module
