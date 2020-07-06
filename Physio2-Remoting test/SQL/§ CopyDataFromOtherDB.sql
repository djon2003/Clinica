DBCC CHECKIDENT ('Clinica.dbo.Villes', NORESEED)
DBCC CHECKIDENT (Villes, RESEED, 76)

SET IDENTITY_INSERT Villes ON
SELECT * FROM Villes
INSERT INTO Villes (NoVille, NomVille)
SELECT NoVille, NomVille FROM Clinica.dbo.Villes
SET IDENTITY_INSERT Villes OFF

INSERT INTO TypeUtilisateur (NomType, DroitAcces, IsTherapist)
SELECT NomType, DroitAcces, IsTherapist FROM Clinica.dbo.TypeUtilisateur

INSERT INTO Titres (Titre)
SELECT Titre FROM Clinica.dbo.Titres


INSERT INTO Utilisateurs (Adresse, Cle, CodePostal, Courriel, DateDebut, DateFin, DroitAcces, IsTherapist, MDP, NoPermis, NoTitre, NoType, NoTypeEmploye, NoVille, Nom, NotConfirmRVOnPasteOfDTRP, Prenom, Services, Telephones, URL)
SELECT Adresse, Cle, CodePostal, Courriel, DateDebut, DateFin, DroitAcces, IsTherapist, MDP, NoPermis, NoTitre, NoType, NoTypeEmploye, NoVille, Nom, NotConfirmRVOnPasteOfDTRP, Prenom, Services, Telephones, URL FROM Clinica.dbo.Utilisateurs
WHERE NoUser IN (14,20,29,24)

INSERT INTO Thresholds (NoGroup, ThresholdMin, ThresholdMax, Name)
SELECT NoGroup, ThresholdMin, ThresholdMax, Name FROM Clinica.dbo.Thresholds

INSERT INTO TelephoneTitles (TelePhoneTitle)
SELECT TelePhoneTitle FROM Clinica.dbo.TelephoneTitles

INSERT INTO SiteLesion (SiteLesion)
SELECT SiteLesion FROM Clinica.dbo.SiteLesion


UPDATE Preferences SET Preferences = (SELECT TOP 1 Preferences FROM Clinica.dbo.Preferences WHERE NoUser IS NULL) WHERE NoUser IS NULL

INSERT INTO Pays (Pays)
SELECT Pays FROM Clinica.dbo.Pays

INSERT INTO Modeles (NoCategorie, NoUser, Nom, Modele)
SELECT NoCategorie, NoUser, Nom, Modele FROM Clinica.dbo.Modeles

INSERT INTO Metiers (Metier)
SELECT Metier FROM Clinica.dbo.Metiers

INSERT INTO KPCategorie (Categorie)
SELECT Categorie FROM Clinica.dbo.KPCategorie

INSERT INTO Employeurs (Employeur)
SELECT Employeur FROM Clinica.dbo.Employeurs

INSERT INTO KeyPeople (Adresse, AutreInfos, CodePostal, Courriel, DateHeureCreation, NoCategorie, NoClient, NoEmployeur, NoRef, NoUser, NoVille, Nom, Publipostage, Telephones, URL, WorkPlace)
SELECT Adresse, AutreInfos, CodePostal, Courriel, DateHeureCreation, NoCategorie, NoClient, NoEmployeur, NoRef, 0, NoVille, Nom, Publipostage, Telephones, URL, WorkPlace FROM Clinica.dbo.KeyPeople

INSERT INTO FolderTexteTypes (AlertMessageArticle, AlertNbDaysForExpiry, CopyTextToOtherText, IsActive, IsDefault, IsNbDaysDiffBefore, ModelAppliedOnCreation, Multiple, NbDaysDiff, NbDaysMultiple, NbDaysX, NbMultipleEnding, NbPresencesMultiple,NbPresencesX, NoModeleCategorie, Position, ResetTextOnCopy, ShowAlarm, ShowAlert, StartingExternalStatus, TexteTitle, TypeForMultiple, WhenToBeCreated, WhenToBeStopped)
SELECT AlertMessageArticle, AlertNbDaysForExpiry, CopyTextToOtherText, IsActive, IsDefault, IsNbDaysDiffBefore, ModelAppliedOnCreation, Multiple, NbDaysDiff, NbDaysMultiple, NbDaysX, NbMultipleEnding, NbPresencesMultiple,NbPresencesX, NoModeleCategorie, Position, ResetTextOnCopy, ShowAlarm, ShowAlert, StartingExternalStatus, TexteTitle, TypeForMultiple, WhenToBeCreated, WhenToBeStopped FROM Clinica.dbo.FolderTexteTypes

INSERT INTO FinMoisTypesRapports (NoClinique, NoRapportType)
SELECT NoClinique, NoRapportType FROM Clinica.dbo.FinMoisTypesRapports

SELECT * FROM CodesDossiersCodes
INSERT INTO CodesDossiersCodes (NoCodeUnique, CodeName)
SELECT NoCodeUnique, CodeName FROM Clinica.dbo.CodesDossiersCodes

-- Triggers from CodificationsDossiers shall be disabled!!!

DBCC CHECKIDENT ('Clinica.dbo.CodificationsDossiers', NORESEED)
DBCC CHECKIDENT (CodificationsDossiers, RESEED, 46)

SET IDENTITY_INSERT CodificationsDossiers ON
SELECT * FROM CodificationsDossiers
INSERT INTO CodificationsDossiers (NoCodification,[NoUser]           ,[NoUnique]           ,[FirstEffectiveTime]           ,[LastEffectiveTime]           ,[Recu]           ,[Paiement]           ,[MsgNoRef]           ,[MsgDiagnostic]           ,[DateAccidentActif]           ,[DateRechuteActif]           ,[AutoOpenPaiement]           ,[AffPourcentAllTimes]           ,[Confirmation]           ,[PonderationEval]           ,[PonderationPresence]           ,[MethodePaiementDefaut]           ,[DemandeAuthorisation]           ,[NotConfirmRVOnPasteOfDTRP]           ,[StartingExternalStatus])
SELECT NoCodification,[NoUser]           ,[NoUnique]           ,[FirstEffectiveTime]           ,[LastEffectiveTime]           ,[Recu]           ,[Paiement]           ,[MsgNoRef]           ,[MsgDiagnostic]           ,[DateAccidentActif]           ,[DateRechuteActif]           ,[AutoOpenPaiement]           ,[AffPourcentAllTimes]           ,[Confirmation]           ,[PonderationEval]           ,[PonderationPresence]           ,[MethodePaiementDefaut]           ,[DemandeAuthorisation]           ,[NotConfirmRVOnPasteOfDTRP]           ,[StartingExternalStatus] FROM Clinica.dbo.CodificationsDossiers WHERE NoUser IN (14,20,29,24) OR NoUser IS NULL
SET IDENTITY_INSERT CodificationsDossiers OFF

DBCC CHECKIDENT ('Clinica.dbo.CodesDossiersPeriodes', NORESEED)
DBCC CHECKIDENT (CodesDossiersPeriodes, RESEED, 56)

SET IDENTITY_INSERT CodesDossiersPeriodes ON
SELECT * FROM CodesDossiersPeriodes
INSERT INTO CodesDossiersPeriodes (NoCDPeriode,[NoCodification]           ,[IsEval]           ,[NoPeriode]           ,[Montant]           ,[IsDefault]           ,[PourcentAbsence]           ,[PourcentClient],[NoKP])
SELECT NoCDPeriode,[NoCodification]           ,[IsEval]           ,[NoPeriode]           ,[Montant]           ,[IsDefault]           ,[PourcentAbsence]           ,[PourcentClient],[NoKP] FROM Clinica.dbo.CodesDossiersPeriodes WHERE NoCodification IN (SELECT NoCodification FROM Clinica.dbo.CodificationsDossiers WHERE NoUser IN (14,20,29,24) OR NoUser IS NULL)
SET IDENTITY_INSERT CodesDossiersPeriodes OFF
