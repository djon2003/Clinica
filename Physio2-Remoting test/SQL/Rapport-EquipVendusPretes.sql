delete from [RapportTypes] where norapporttype=82
delete from [RapportTypes] where norapporttype=83
INSERT INTO [Clinica].[dbo].[RapportTypes]
           ([NoRapportType]
           ,[RapportTitle]
           ,[RapportHeaderName]
           ,[RapportHeaderProperties]
           ,[RapportBodyName]
           ,[RapportBodyProperties]
           ,[RapportFooterName]
           ,[RapportFooterProperties]
           ,[NoRapportCategorie]
           ,[RapportProperties])
     VALUES
           (82
           ,'Liste des équipements prêtés et vendus'
           ,'RapportHeaderSimple'
           ,'§HeaderFileName§=§###CLINICAPATH###Data\Rapports\basicheader.html§§StyleFileName§=§###CLINICAPATH###Data\Rapports\basicheader.css§§SQLStatement§=§SELECT Nom,Adresse,NomVille AS Ville,CodePostal,Telephone FROM InfoClinique INNER JOIN Villes ON Villes.NoVille = InfoClinique.NoVille§'
           ,'RapportBodyTable'
           ,'§SQLStatement§=§SELECT     InfoClients.Nom + N'','' + InfoClients.Prenom AS Client, T.NoFolder AS [# du dossier], Equipements.NomItem + N'' / '' + T.NoItem AS Item,                        Utilisateurs.Nom + N'','' + Utilisateurs.Prenom AS Thérapeute, T.DateHeure AS [Date de transaction], T.DateRetour AS [Date de retour], T.Type,                        T.MF AS [Montant facturé], CASE WHEN T.MP IS NULL THEN 0 ELSE T.MP END AS [Montant payé], T.MF - CASE WHEN T.MP IS NULL THEN 0 ELSE T.MP END AS CAR FROM         (SELECT     (SELECT     SUM(MontantFacture) AS Expr1                                                FROM         StatFactures AS SF                                                WHERE     (NoFacture = Ventes.NoFacture)) AS MF,                                                   (SELECT     SUM(MontantPaiement) AS Expr1                                                     FROM          StatPaiements AS SP                                                     WHERE      (NoFacture = Ventes.NoFacture)) AS MP, DateHeure, NoFolder, NoClient, NoEquipement, NoTRP, NoItem, NULL AS DateRetour,                                                ''Vente'' AS Type                        FROM          Ventes                        UNION ALL                        SELECT     (SELECT     SUM(MontantFacture) AS Expr1                                               FROM          StatFactures AS SF                                               WHERE      (NoFacture = Prets.NoFacture)) AS MF,                                                  (SELECT     SUM(MontantPaiement) AS Expr1                                                    FROM          StatPaiements AS SP                                                    WHERE      (NoFacture = Prets.NoFacture)) AS MP, DateHeure, NoFolder, NoClient, NoEquipement, NoTRP, NoItem, DateRetour,                                               ''Prêt'' AS Type                        FROM         Prets) AS T INNER JOIN                       Equipements ON T.NoEquipement = Equipements.NoEquipement INNER JOIN                       InfoClients ON T.NoClient = InfoClients.NoClient INNER JOIN                       Utilisateurs ON T.NoTRP = Utilisateurs.NoUser  WHEREGEN ORDER BY ORDERGEN§§StyleFileName§=§###CLINICAPATH###Data\Rapports\basicbody.css§§ColsFormat§=§{Montant payé=CURRENCY£Montant facturé=CURRENCY£CAR=CURRENCY£# dossier=INT£Date de transaction=DATE£Date de retour=DATE}§§EndTable§=§False§§OrderDefaultColumn§=§Date de transaction§§OrderColumnsName§=§{Client£Item£Thérapeute£Date de transaction ASK£Date de retour ASK}§§SubTotalColumnsName§=§{Montant facturé£Montant payé£CAR}§'
           ,'RapportFooterTotal'
           ,'§StyleFileName§=§{###CLINICAPATH###Data\Rapports\basicfooter.css£###CLINICAPATH###Data\Rapports\footertotal.css}§'
           ,null
           ,'')
INSERT INTO [Clinica].[dbo].[RapportTypes]
           ([NoRapportType]
           ,[RapportTitle]
           ,[RapportHeaderName]
           ,[RapportHeaderProperties]
           ,[RapportBodyName]
           ,[RapportBodyProperties]
           ,[RapportFooterName]
           ,[RapportFooterProperties]
           ,[NoRapportCategorie]
           ,[RapportProperties])
     VALUES
           (83
           ,'Liste des équipements prêtés non retournés'
           ,'RapportHeaderSimple'
           ,'§HeaderFileName§=§###CLINICAPATH###Data\Rapports\basicheader.html§§StyleFileName§=§###CLINICAPATH###Data\Rapports\basicheader.css§§SQLStatement§=§SELECT Nom,Adresse,NomVille AS Ville,CodePostal,Telephone FROM InfoClinique INNER JOIN Villes ON Villes.NoVille = InfoClinique.NoVille§'
           ,'RapportBodyTable'
           ,'§SQLWhere§=§(Retourne=0 AND Type=''Prêt'')§§SQLStatement§=§SELECT     InfoClients.Nom + N'','' + InfoClients.Prenom AS Client, T.NoFolder AS [# du dossier], Equipements.NomItem + N'' / '' + T.NoItem AS Item,                        Utilisateurs.Nom + N'','' + Utilisateurs.Prenom AS Thérapeute, T.DateHeure AS [Date de transaction], T.DateRetour AS [Date de retour], T.Type,                        T.MF AS [Montant facturé], CASE WHEN T.MP IS NULL THEN 0 ELSE T.MP END AS [Montant payé], T.MF - CASE WHEN T.MP IS NULL THEN 0 ELSE T.MP END AS CAR FROM         (SELECT    Retourne, (SELECT     SUM(MontantFacture) AS Expr1                                               FROM          StatFactures AS SF                                               WHERE      (NoFacture = Prets.NoFacture)) AS MF,                                                  (SELECT     SUM(MontantPaiement) AS Expr1                                                    FROM          StatPaiements AS SP                                                    WHERE      (NoFacture = Prets.NoFacture)) AS MP, DateHeure, NoFolder, NoClient, NoEquipement, NoTRP, NoItem, DateRetour,                                               ''Prêt'' AS Type                        FROM         Prets) AS T INNER JOIN                       Equipements ON T.NoEquipement = Equipements.NoEquipement INNER JOIN                       InfoClients ON T.NoClient = InfoClients.NoClient INNER JOIN                       Utilisateurs ON T.NoTRP = Utilisateurs.NoUser  WHEREGEN ORDER BY ORDERGEN§§StyleFileName§=§###CLINICAPATH###Data\Rapports\basicbody.css§§ColsFormat§=§{Montant payé=CURRENCY£Montant facturé=CURRENCY£CAR=CURRENCY£# dossier=INT£Date de transaction=DATE£Date de retour=DATE}§§EndTable§=§False§§OrderDefaultColumn§=§Date de retour§§OrderColumnsName§=§{Client£Item£Thérapeute£Date de transaction ASK£Date de retour ASK}§§SubTotalColumnsName§=§{Montant facturé£Montant payé£CAR}§'
           ,'RapportFooterTotal'
           ,'§StyleFileName§=§{###CLINICAPATH###Data\Rapports\basicfooter.css£###CLINICAPATH###Data\Rapports\footertotal.css}§'
           ,null
           ,'')



select count(*) from ventes
select count(*) from prets

select * from ventes
select * from prets

