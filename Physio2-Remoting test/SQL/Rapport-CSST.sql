USE Clinica
SELECT DATEADD(minute,DATEPART(minute,GETDATE())*-1,DATEADD(hour,DATEPART(hour,GETDATE())*-1,GETDATE()))
SELECT    Utilisateurs.Nom +','+Utilisateurs.Prenom AS Thérapeute, InfoClients.Nom+','+ InfoClients.Prenom AS Nom, RapportsCSST.Title AS Rapport, RapportsCSST.StartRap AS Début, RapportsCSST.EndRap AS Fin
FROM         (RapportsCSST LEFT JOIN Utilisateurs ON Utilisateurs.NoUser = RapportsCSST.NoTRPTraitant) INNER JOIN
                      InfoClients ON RapportsCSST.NoClient = InfoClients.NoClient
WHERE     (RapportsCSST.StartRap < DATEADD(day,1,DATEADD(minute,DATEPART(minute,GETDATE())*-1,DATEADD(hour,DATEPART(hour,GETDATE())*-1,GETDATE())))) AND (RapportsCSST.EndRap >= DATEADD(minute,DATEPART(minute,GETDATE())*-1,DATEADD(hour,DATEPART(hour,GETDATE())*-1,GETDATE()))) AND (RapportsCSST.RapEnded = 0) AND Title IS NOT NULL