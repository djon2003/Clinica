WITH status
AS (SELECT     InfoClients.Nom + ',' + InfoClients.Prenom AS Nom, InfoClients.Telephones, InfoClients.NAM, InfoFolders.NoFolder, InfoFolders.DateAccident,
 (SELECT TOP 1 DateHeure FROM InfoVisites WHERE InfoVisites.NoFolder = InfoFolders.NoFolder AND InfoVisites.NoStatut>2 ORDER BY DateHeure) AS [Date d'�valuation], 
 (SELECT Count(InfoVisites.NoVisite) FROM InfoVisites WHERE InfoVisites.NoFolder = InfoFolders.NoFolder AND InfoVisites.NoStatut>2) AS [Nb traitement],
 (SELECT Count(InfoVisites.NoVisite) FROM InfoVisites WHERE InfoVisites.NoFolder = InfoFolders.NoFolder AND InfoVisites.NoStatut>2) AS [Nb R-V]
FROM         InfoClients INNER JOIN
                      InfoFolders ON InfoClients.NoClient = InfoFolders.NoClient INNER JOIN
                      Utilisateurs ON InfoFolders.NoTRPTraitant = Utilisateurs.NoUser
WHERE     (InfoFolders.StatutOuvert <> 0)
)
SELECT Nom, Telephones, NAM, NoFolder, CASE WHEN DateAccident IS NULL THEN '' ELSE CONVERT(varchar,DateAccident) END As [Date d'accident],[Date d'�valuation],[Nb traitement],[Nb R-V],DATEDIFF(day,[Date d'�valuation],GETDATE()) AS [Nb jour du d�but]
FROM status
WHERE DATEDIFF(day,[Date d'�valuation],GETDATE())>=0
ORDER BY Nom