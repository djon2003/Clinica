SELECT     'Dossier #' + CAST(Main.NoFolder AS VARCHAR(15)) 
                      + ' (' + CASE WHEN Main.StatutOuvert = 0 THEN 'Inactif' ELSE 'Actif' END + ') - Site de lésion/Codification : ' + CASE WHEN SiteLesion.SiteLesion IS
                       NULL THEN 'Aucun' ELSE SiteLesion.SiteLesion END + ' / ' + CodificationsDossiers.Nom AS Ligne1, 
                      'Médecin : ' + CASE WHEN KeyPeople.Nom IS NULL 
                      THEN 'Aucun' ELSE KeyPeople.Nom END + '&nbsp;&nbsp;&nbsp;Diagnostic/Référence : ' + CASE WHEN Main.Diagnostic IS NULL 
                      THEN 'Aucun' ELSE Main.Diagnostic END + ' / ' + CASE WHEN Main.NoRef IS NULL 
                      THEN 'Aucun' ELSE Main.NoRef END + CASE WHEN Main.DateRef IS NULL THEN '' ELSE ' (' + CAST(Main.DateRef AS VARCHAR(8)) 
                      END + ')' AS Ligne2, 'Date : Accident / Rechute : ' + CASE WHEN Main.DateAccident IS NULL 
                      THEN 'Aucune' ELSE CAST(Main.DateAccident AS VARCHAR(8)) END + ' / ' + CASE WHEN Main.DateRechute IS NULL 
                      THEN 'Aucune' ELSE CAST(Main.DateRechute AS VARCHAR(8)) 
                      END + '&nbsp;&nbsp;&nbsp;Durée/Fréquence : ' + CASE WHEN Main.Duree = 0 THEN 'Aucune' ELSE CAST((Main.Duree * 5) 
                      AS VARCHAR(15)) + ' jours' END + ' / ' + CONVERT(VARCHAR(3), Main.Frequence + 1) + ' X semaine' AS Ligne3, 
                      CASE WHEN Main.Service IS NULL 
                      THEN 'Service manquant' ELSE Main.Service END + ' - Nombre de RV ayant un CAR / Plus vieux CAR : ' + CAST(Main.NbVisiteHavingCAR AS
                       VARCHAR(4)) + ' / ' + CASE WHEN Main.OldiestCAR IS NULL THEN 'Aucune date' ELSE CAST(Main.OldiestCAR AS VARCHAR(8)) 
                      END AS Ligne4, InfoVisites.DateHeure AS [Date du rendez-vous], InfoVisites.Periode AS [Durée (minutes)], InfoVisites.Service, 
                      ListeStatut.NomStatut AS Statut, InfoVisites.NoFacture AS [# facture],(SELECT SUM(MontantFacture) FROM StatFactures WHERE StatFactures.NoFacture=InfoVisites.NoFacture) AS [Montant facturé],(SELECT SUM(MontantPaiement) FROM StatPaiements WHERE StatPaiements.NoFacture = InfoVisites.NoFacture) AS [Montant payé]
FROM         SiteLesion RIGHT OUTER JOIN
                      Utilisateurs RIGHT OUTER JOIN
                      InfoVisites LEFT OUTER JOIN
                      ListeStatut ON InfoVisites.NoStatut = ListeStatut.NoStatut RIGHT OUTER JOIN
                      InfoFolders AS Main INNER JOIN
                      CodificationsDossiers ON Main.NoCodification = CodificationsDossiers.NoCodification ON InfoVisites.NoFolder = Main.NoFolder ON 
                      Utilisateurs.NoUser = Main.NoTRPDemande LEFT OUTER JOIN
                      Utilisateurs AS Utilisateurs_1 ON Main.NoTRPTraitant = Utilisateurs_1.NoUser LEFT OUTER JOIN
                      KeyPeople ON Main.NoKP = KeyPeople.NoKP ON SiteLesion.NoSiteLesion = Main.NoSiteLesion
--WHERE     (Main.Service IS NOT NULL)
ORDER BY Ligne1, Ligne2, Ligne3, Ligne4, [Date du rendez-vous]

/*
, 
                      Main.Service + ' - Nombre de RV ayant un CAR / Plus vieux CAR : ' + CAST(Main.NbVisiteHavingCAR AS VARCHAR(4)) 
                      + Main.OldiestCAR AS Ligne4
*/