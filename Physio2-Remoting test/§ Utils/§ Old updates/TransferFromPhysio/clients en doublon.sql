SELECT * FROM InfoClients WHERE Nom+','+Prenom IN
(SELECT     Nom + ','+Prenom as Client
FROM         InfoClients
GROUP BY Nom + ','+Prenom
HAVING      (COUNT(*) > 1))  ORDER BY Nom,Prenom