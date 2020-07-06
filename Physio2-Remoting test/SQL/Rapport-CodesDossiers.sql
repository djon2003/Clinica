SELECT CASE WHEN U.Nom IS NULL THEN '* D�faut *' ELSE U.Nom + ',' + U.Prenom + ' (' + CAST(CD.[NoUser] AS VARCHAR(MAX)) + ')' END AS [Th�rapeute]       ,CD.[Nom]  		+ '<br><table border=0><tr><td><ul><li>Re�u ' + CASE WHEN CD.Recu=1 THEN '�mis' ELSE 'non �mis' END + ' automatiquement</li>'  		+ '<li>Paiement ' + CASE WHEN CD.Paiement =1 THEN '' ELSE 'non ' END + 's�lectionn� automatiquement</li>' 		+ '<li>' + CASE WHEN CD.MsgNoRef=1 THEN 'V' ELSE 'Ne v' END + '�rifie ' + CASE WHEN CD.MsgNoRef=1 THEN '' ELSE 'pas ' END + 'si le n� de r�f�rence a �t� entr�</li>' 		+ '<li>' + CASE WHEN CD.MsgDiagnostic=1 THEN 'V' ELSE 'Ne v' END + '�rifie ' + CASE WHEN CD.MsgDiagnostic=1 THEN '' ELSE 'pas ' END + 'si le diagnostic a �t� entr�</li>' 		+ '<li>' + CASE WHEN CD.DateAccidentActif=1 THEN 'V' ELSE 'Ne v' END + '�rifie ' + CASE WHEN CD.DateAccidentActif=1 THEN '' ELSE 'pas ' END + 'si la date d''accident a �t� entr�e</li>' 		+ '<li>' + CASE WHEN CD.DateRechuteActif=1 THEN 'V' ELSE 'Ne v' END + '�rifie ' + CASE WHEN CD.DateRechuteActif=1 THEN '' ELSE 'pas ' END + 'si la date de rechute a �t� entr�e</li>' 		+ '<li>' + CASE WHEN CD.AutoOpenPaiement=1 THEN 'O' ELSE 'Ne o' END + 'uvre ' + CASE WHEN CD.AutoOpenPaiement=1 THEN '' ELSE 'pas ' END + 'la fen�tre de paiement automatiquement lors de la prise d''une pr�sence</li>' + '<li>' + CASE WHEN CD.[AffPourcentAllTimes]=1 THEN 'D' ELSE 'Ne d' END + 'emande ' + CASE WHEN CD.[AffPourcentAllTimes]=1 THEN '' ELSE 'pas ' END + 'le pourcentage du payeur � chaque prise de pr�sence</li>' + '<li>' + CASE WHEN CD.Confirmation =0 THEN 'Ne confirme aucun rendez-vous' ELSE CASE WHEN CD.Confirmation=1 THEN 'Confirme les �valuations' ELSE CASE WHEN CD.Confirmation=2 THEN 'Confirme les traitements' ELSE 'Confirme tous les rendez-vous' END END END + '</li>' + '</ul></TD><TD><ul><li>Pond�ration d''une �valuation : ' + CAST(CD.PonderationEval AS VARCHAR(MAX)) + '</li>' + '<li>Pond�ration d''un traitement : ' + CAST(CD.PonderationPresence AS VARCHAR(MAX)) + '</li>' + '<li>' + CASE WHEN CD.RapportsOptionsCSST=1 THEN 'A' ELSE 'N''a' END + 'ctive ' + CASE WHEN CD.RapportsOptionsCSST=1 THEN '' ELSE 'pas ' END + 'les rapports et les options CSST</li>' + '<li>' + CASE WHEN CD.DemandeAuthorisation=1 THEN 'A' ELSE 'N''a' END + 'ctive ' + CASE WHEN CD.DemandeAuthorisation=1 THEN '' ELSE 'pas ' END + 'la demande d''autorisation</li>' + '<li>' + CASE WHEN CD.NotConfirmRVOnPasteOfDTRP=1 THEN 'V' ELSE 'Ne v' END + '�rifie ' + CASE WHEN CD.NotConfirmRVOnPasteOfDTRP=1 THEN '' ELSE 'pas ' END + 'si un rendez-vous sera coll� dans l''agenda ayant le m�me th�rapeute que le dossier du rendez-vous</li>' + '</ul></td></tr></table>' AS Codification,       CASE WHEN CDP.IsEval =1 THEN '�valuation' ELSE 'Traitement' END AS [Type de R-V],Periode,CDP.Montant,CASE WHEN CDP.IsDefault=1 THEN 'Oui' ELSE 'Non' END AS [D�faut],CDP.PourcentAbsence*100.0 AS [% d'une absence],CDP.PourcentClient*100.0 AS [% du client],CASE WHEN CDP.NoKP IS NULL THEN 'Aucun(e)' ELSE KeyPeople.Nom END AS [P/O cl�]   FROM [Clinica].[dbo].[CodificationsDossiers]  AS CD INNER JOIN (CodesDossiersPeriodes CDP LEFT JOIN KeyPeople ON KeyPeople.NoKP = CDP.NoKP) INNER JOIN ListePeriode ON ListePeriode.NoPeriode=CDP.NoPeriode ON CD.NoCodification=CDP.NoCodification LEFT JOIN Utilisateurs U ON U.NoUser=CD.NoUser ORDER BY Th�rapeute,Codification,IsEval DESC,CDP.NoPeriode  