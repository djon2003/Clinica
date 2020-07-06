WITH payes
AS(
SELECT     Utilisateurs.Nom + ',' + Utilisateurs.Prenom AS Nom, PayesUtilisateurs.NoUser, PayesUtilisateurs.DatePaie, CONVERT(float, SUM(InfoVisites.Periode)) 
                      / 60 AS HreTRP,
                          (SELECT     COUNT(Periode) AS Expr1
                            FROM          InfoVisites AS IV3
                            WHERE      (NoStatut = 4) AND (NoTRP = InfoVisites.NoTRP) AND (Evaluation = 0) AND (DateHeure >= PayesUtilisateurs.DatePaie) AND 
                                                   (DateHeure < DATEADD(day, 7, PayesUtilisateurs.DatePaie))) AS NbRV,
                          (SELECT     COUNT(Periode) AS Expr1
                            FROM          InfoVisites AS IV2
                            WHERE      (NoStatut = 4) AND (NoTRP = InfoVisites.NoTRP) AND (Evaluation <> 0) AND (DateHeure >= PayesUtilisateurs.DatePaie) AND 
                                                   (DateHeure < DATEADD(day, 7, PayesUtilisateurs.DatePaie))) AS NbEval, 
                      PayesUtilisateurs.DiNb + PayesUtilisateurs.LuNb + PayesUtilisateurs.MaNb + PayesUtilisateurs.MeNb + PayesUtilisateurs.JeNb + PayesUtilisateurs.VeNb
                       + PayesUtilisateurs.SaNb AS Montant, PayesUtilisateurs.Multiplicateur, CodificationsDossiers.PonderationEval, 
                      CodificationsDossiers.PonderationPresence, REPLACE(PayesUtilisateurs.Type,'Par','/') AS Type
FROM         CodificationsDossiers RIGHT OUTER JOIN
                      InfoFolders ON CodificationsDossiers.NoCodification = InfoFolders.NoCodification RIGHT OUTER JOIN
                      InfoVisites ON InfoFolders.NoFolder = InfoVisites.NoFolder RIGHT OUTER JOIN
                      PayesUtilisateurs INNER JOIN
                      Utilisateurs ON PayesUtilisateurs.NoUser = Utilisateurs.NoUser ON InfoVisites.DateHeure >= PayesUtilisateurs.DatePaie AND 
                      InfoVisites.DateHeure < DATEADD(day, 7, PayesUtilisateurs.DatePaie) AND Utilisateurs.NoUser = InfoVisites.NoTRP AND 
                      InfoVisites.NoStatut = 4
GROUP BY PayesUtilisateurs.NoUser, Utilisateurs.Nom + ',' + Utilisateurs.Prenom, PayesUtilisateurs.DatePaie, PayesUtilisateurs.DiNb, 
                      PayesUtilisateurs.LuNb, PayesUtilisateurs.MaNb, PayesUtilisateurs.MeNb, PayesUtilisateurs.JeNb, PayesUtilisateurs.VeNb, PayesUtilisateurs.SaNb, 
                      PayesUtilisateurs.Multiplicateur, CodificationsDossiers.PonderationEval, CodificationsDossiers.PonderationPresence, InfoVisites.NoTRP, 
                      PayesUtilisateurs.Type
)

SELECT Nom,DatePaie AS Date,Montant AS [Montant des R-V ou Heures travaillées],CONVERT(varchar,Multiplicateur) + ' ' + Type AS Taux,Multiplicateur * Montant AS Total, HreTRP as [Heures thérapeutiques],NbRV+NbEval as [Nb R-V],(NbEval*PonderationEval+NbRV*PonderationPresence)/HreTRP as [Ratio pondéré],(NbRV+NbEval)/HreTRP AS [Ratio réel]
FROM payes
ORDER BY DatePaie