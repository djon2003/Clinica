WITH ages AS (SELECT     CodificationsDossiers.Nom AS Codification, InfoClients.Nom + N',' + InfoClients.Prenom AS Nom,
                                                            (SELECT     SUM(MontantFacture) AS Expr1
                                                              FROM          Factures
                                                              WHERE      (DATEDIFF(day, DateFacture, GETDATE()) < 31) AND (NoFacture = F1.NoFacture)) AS N1,
                                                            (SELECT     SUM(MontantFacture) AS Expr1
                                                              FROM          Factures AS Factures_4
                                                              WHERE      (DATEDIFF(day, DateFacture, GETDATE()) < 61) AND (DATEDIFF(day, DateFacture, GETDATE()) > 30) AND 
                                                                                     (NoFacture = F1.NoFacture)) AS N2,
                                                            (SELECT     SUM(MontantFacture) AS Expr1
                                                              FROM          Factures AS Factures_3
                                                              WHERE      (DATEDIFF(day, DateFacture, GETDATE()) < 91) AND (DATEDIFF(day, DateFacture, GETDATE()) > 60) AND 
                                                                                     (NoFacture = F1.NoFacture)) AS N3,
                                                            (SELECT     SUM(MontantFacture) AS Expr1
                                                              FROM          Factures AS Factures_2
                                                              WHERE      (DATEDIFF(day, DateFacture, GETDATE()) > 90) AND (NoFacture = F1.NoFacture)) AS N4, F1.DateFacture, F1.NoFacture, 
                                                        CASE WHEN F1.Type = 'K' THEN KeyPeople.Nom ELSE InfoClients_1.Nom + ',' + InfoClients_1.Prenom END AS Payeur, CONVERT(float, 
                                                        F1.PourcentPayeur) / 100 AS PourcentPayeur
                                 FROM          (SELECT     NoFacture, NoUser, DateFacture, NoFolder, NoClient, MontantFacture, TypeFacture, Description, NoVisite, NoPret, 
                                                                                NoFactureRef, NoVente, Taxe1, Taxe2, NoRecu, NoKP, NoUserFacture, ParNoKP AS Payeur, 
                                                                                PourcentParNoKp AS PourcentPayeur, NoFactureTransfere, MontantPaiementKP AS MP, 'K' AS Type, NoTrigger
                                                         FROM          Factures
                                                         UNION
                                                         SELECT     NoFacture, NoUser, DateFacture, NoFolder, NoClient, MontantFacture, TypeFacture, Description, NoVisite, NoPret, 
                                                                               NoFactureRef, NoVente, Taxe1, Taxe2, NoRecu, NoKP, NoUserFacture, ParNoClient AS Payeur, 
                                                                               PourcentParNoClient AS PourcentPayeur, NoFactureTransfere, MontantPaiement AS MP, 'C' AS Type, NoTrigger
                                                         FROM         Factures AS Factures_2
                                                         UNION
                                                         SELECT     NoFacture, NoUser, DateFacture, NoFolder, NoClient, MontantFacture, TypeFacture, Description, NoVisite, NoPret, 
                                                                               NoFactureRef, NoVente, Taxe1, Taxe2, NoRecu, NoKP, NoUserFacture, ParNoUser AS Payeur, 
                                                                               PourcentParNoUser AS PourcentPayeur, NoFactureTransfere, MontantPaiementUser AS MP, 'U' AS Type, NoTrigger
                                                         FROM         Factures AS Factures_1) AS F1 INNER JOIN
                                                        InfoClients ON F1.NoClient = InfoClients.NoClient INNER JOIN
                                                        InfoFolders ON F1.NoFolder = InfoFolders.NoFolder INNER JOIN
                                                        CodificationsDossiers ON InfoFolders.NoCodification = CodificationsDossiers.NoCodification LEFT OUTER JOIN
                                                        InfoClients AS InfoClients_1 ON F1.Payeur = InfoClients_1.NoClient LEFT OUTER JOIN
                                                        KeyPeople ON F1.Payeur = KeyPeople.NoKP
                                 WHERE      (F1.PourcentPayeur > 0))
    SELECT     Codification, Nom, CASE WHEN N1 IS NULL THEN 0 ELSE N1 * PourcentPayeur END AS [0-30 jrs], CASE WHEN N2 IS NULL 
                            THEN 0 ELSE N2 * PourcentPayeur END AS [31-60 jrs], CASE WHEN N3 IS NULL THEN 0 ELSE N3 * PourcentPayeur END AS [61-90 jrs], 
                            CASE WHEN N4 IS NULL THEN 0 ELSE N4 * PourcentPayeur END AS [90+ jrs], Payeur, DateFacture AS Date
     FROM         ages
     ORDER BY Codification, Nom, Date