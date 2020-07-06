Module Comptabilites

    Public Enum TaxesApplications
        PREFERENCE = -1
        TAX2_ON_TAX1 = 0
        TAX2_ON_AMOUNT = 1
    End Enum

    Public Function createFacturation(ByVal noClient As Integer, ByVal amount As Double, ByVal typeFacturation As String, ByVal factureDate As Date, Optional ByVal noFolder As Integer = 0, Optional ByVal noVisite As Integer = 0, Optional ByVal noPret As Integer = 0, Optional ByVal noFactureRef As String = "", Optional ByVal noVente As Integer = 0, Optional ByVal noFacture As Integer = 0, Optional ByVal writingStats As Boolean = True, Optional ByVal montantPaiement As Double = 0, Optional ByVal noKP As Integer = 0, Optional ByVal noUserFacture As Integer = 0, Optional ByVal parnoClient As Integer = 0, Optional ByVal parnoKP As Integer = 0, Optional ByVal parNoUser As Integer = 0, Optional ByVal pourcentnoClient As Double = 100, Optional ByVal pourcentnoKP As Double = 0, Optional ByVal pourcentNoUser As Double = 0, Optional ByVal montantPaiementUser As Double = 0, Optional ByVal montantPaiementKP As Double = 0, Optional ByVal montantPaiementClinique As Double = 0, Optional ByVal parNoClinique As Integer = 0, Optional ByVal applyTaxes As Boolean = True, Optional ByVal description As String = "", Optional ByVal taxe1 As Double = -1, Optional ByVal taxe2 As Double = -1, Optional ByVal factureComments As String = "", Optional ByVal payeComments As String = "", Optional ByVal taxesApplication As TaxesApplications = TaxesApplications.PREFERENCE) As Integer
        'Ensure pourcentage Sum 100%
        If typeFacturation.ToUpper <> "FACTURE UNIFIÉE" AndAlso parNoClinique = 0 AndAlso (pourcentnoClient + pourcentnoKP + pourcentNoUser) < 100 Then
            If Bill.askPourcentages(parnoClient, parnoKP, parNoUser, pourcentnoClient, pourcentnoKP, pourcentNoUser) = False Then Return 0
        End If

        amount = roundAmount(amount) 'Arrondi si nécessaire

        Dim mfClient As Double = Math.Round(amount * pourcentnoClient / 100, 2)
        Dim mfkp As Double = Math.Round(amount * pourcentnoKP / 100, 2)
        Dim mfUser As Double = Math.Round(amount * pourcentNoUser / 100, 2)
        Dim mfClinique As Double = Math.Round(amount * (100 - pourcentnoClient - pourcentnoKP - pourcentNoUser) / 100, 2)

        If amount <> mfClient + mfkp + mfUser + mfClinique Then
            Dim IsFaultyClient, IsFaultyKP, IsFaultyUser, isFaultyClinique As Boolean
            Try
                Dim nbFaulty As Double = 0
                Dim nbCentsDiff As Double = Math.Round((amount - mfClient - mfkp - mfUser - mfClinique) * 100, 0)
                If mfClient.ToString.Replace(",", ".").IndexOf(".01") > 0 Or mfClient.ToString.Replace(",", ".").IndexOf(".99") > 0 Then IsFaultyClient = True : nbFaulty += 1
                If mfkp.ToString.Replace(",", ".").IndexOf(".01") > 0 Or mfkp.ToString.Replace(",", ".").IndexOf(".99") > 0 Then IsFaultyKP = True : nbFaulty += 1
                If mfUser.ToString.Replace(",", ".").IndexOf(".01") > 0 Or mfUser.ToString.Replace(",", ".").IndexOf(".99") > 0 Then IsFaultyUser = True : nbFaulty += 1
                If mfClinique.ToString.Replace(",", ".").IndexOf(".01") > 0 Or mfClinique.ToString.Replace(",", ".").IndexOf(".99") > 0 Then isFaultyClinique = True : nbFaulty += 1
                If nbFaulty = nbCentsDiff Then
                    Dim multipler As Double = 1
                    If nbCentsDiff < 0 Then multipler = -1
                    If IsFaultyClient Then mfClient += 0.01 * multipler
                    If IsFaultyKP Then mfkp += 0.01 * multipler
                    If IsFaultyUser Then mfUser += 0.01 * multipler
                    If isFaultyClinique Then mfClinique += 0.01 * multipler
                Else
                    'problem with divising amounts
                    addErrorLog(New Exception("Bug with amounts on CreateFacturation(Amount=" & amount & ",PourcentNoClient=" & pourcentnoClient & ", PourcentNoKP=" & pourcentnoKP & ",PourcentNoUser=" & pourcentNoUser & ", MFClient=" & mfClient & " (IsFaulty=" & IsFaultyClient & "), MFKP=" & mfkp & " (IsFaulty=" & IsFaultyKP & "), MFUser=" & mfUser & " (IsFaulty=" & IsFaultyUser & "), MFClinique=" & mfClinique & " (IsFaulty=" & isFaultyClinique & "))"))
                End If
            Catch ex As Exception
                addErrorLog(New Exception("Bug with amounts on CreateFacturation(Amount=" & amount & ",PourcentNoClient=" & pourcentnoClient & ", PourcentNoKP=" & pourcentnoKP & ",PourcentNoUser=" & pourcentNoUser & ", MFClient=" & mfClient & " (IsFaulty=" & IsFaultyClient & "), MFKP=" & mfkp & " (IsFaulty=" & IsFaultyKP & "), MFUser=" & mfUser & " (IsFaulty=" & IsFaultyUser & "), MFClinique=" & mfClinique & " (IsFaulty=" & isFaultyClinique & "))"))
            End Try
        End If

        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True

        Dim done As Boolean = True

        Try
            'REM tran DBLinker.GetInstance.BeginTransaction()

            If noFacture = 0 Then
                DBLinker.getInstance.writeDB("FacturesNumbers", "NoUser", ConnectionsManager.currentUser.ToString, , , , noFacture)
                If noFacture = 0 Then
                    noFacture = 1
                Else
                    DBLinker.getInstance().delDB("FacturesNumbers", "NoFacture", noFacture)
                End If
            End If

            Dim MyTaxe1, myTaxe2 As String
            MyTaxe1 = "0" : myTaxe2 = "0"
            taxesApplication = If(taxesApplication = TaxesApplications.PREFERENCE, If(PreferencesManager.getGeneralPreferences("TaxesApplication").IndexOf("1") = -1, TaxesApplications.TAX2_ON_AMOUNT, TaxesApplications.TAX2_ON_TAX1), taxesApplication)
            'Si ce n'est pas un service ni une facture unifée, attribuer des taxes
            If applyTaxes AndAlso CStr(PreferencesManager.getGeneralPreferences()("Services")).LastIndexOf(typeFacturation.Split(New Char() {":"})(0).Trim()) < 0 And noFactureRef = "" Then
                MyTaxe1 = CStr(IIf(taxe1 = -1, PreferencesManager.getGeneralPreferences()("Taxe1"), taxe1)).Replace(",", ".")
                myTaxe2 = CStr(IIf(taxe2 = -1, PreferencesManager.getGeneralPreferences()("Taxe2"), taxe2)).Replace(",", ".")
            End If

            'Si le montant de la facture n'est pas égale à zéro, alors ajout ds la table Factures
            If amount > 0 Then done = DBLinker.getInstance.writeDB("Factures", "NoFacture,NoUser,DateFacture,NoFolder,NoClient,MontantFacture,TypeFacture,NoVisite,NoPret,NoFactureRef,NoVente,Taxe1,Taxe2,MontantPaiement,NoKP,NoUserFacture,ParNoClient,ParNoKP,ParNoUser,MontantPaiementUser,MontantPaiementKP,MontantPaiementClinique,ParNoClinique,MontantFactureKP,MontantFactureUser,MontantFactureClinique,Description,TaxesApplication", noFacture & "," & ConnectionsManager.currentUser & ",'" & factureDate.Year & "/" & factureDate.Month & "/" & factureDate.Day & "'," & noFolder & "," & IIf(noClient = 0, "null", noClient) & "," & mfClient.ToString.Replace(",", ".") & ",'" & typeFacturation.Replace("'", "''") & "'," & IIf(noVisite = 0, "null", noVisite) & "," & IIf(noPret = 0, "null", noPret) & ",'" & noFactureRef & "'," & IIf(noVente = 0, "null", noVente) & "," & MyTaxe1 & "," & myTaxe2 & "," & montantPaiement.ToString.Replace(",", ".") & "," & IIf(noKP = 0, "null", noKP) & "," & IIf(noUserFacture = 0, "null", noUserFacture) & "," & IIf(parnoClient = 0, "null", parnoClient) & "," & IIf(parnoKP = 0, "null", parnoKP) & "," & IIf(parNoUser = 0, "null", parNoUser) & "," & montantPaiementUser.ToString.Replace(",", ".") & "," & montantPaiementKP.ToString.Replace(",", ".") & "," & montantPaiementClinique.ToString.Replace(",", ".") & "," & IIf(parNoClinique = 0, "null", parNoClinique) & "," & mfkp.ToString.Replace(",", ".") & "," & mfUser.ToString.Replace(",", ".") & "," & mfClinique.ToString.Replace(",", ".") & ",'" & description.Replace("'", "''") & "'," & CInt(taxesApplication))

            If done AndAlso noFactureRef <> "" Then
                Dim noFactures() As String = noFactureRef.Split(New Char() {"§"})
                For i As Integer = 0 To noFactures.GetUpperBound(0)
                    done = done AndAlso DBLinker.getInstance.writeDB("FacturesLinked", "NoFacture, NoFactureLinked", noFacture & "," & noFactures(i))
                Next i
            End If

            If noVisite > 0 Then done = done AndAlso DBLinker.getInstance.updateDB("InfoVisites", "NoFacture=" & noFacture, "NoVisite", noVisite, False)
            If noPret > 0 Then done = done AndAlso DBLinker.getInstance.updateDB("Prets", "NoFacture=" & noFacture, "NoPret", noPret, False)
            If noVente > 0 Then done = done AndAlso DBLinker.getInstance.updateDB("Ventes", "NoFacture=" & noFacture, "NoVente", noVente, False)

            If writingStats = True Then
                If parnoClient > 0 And (pourcentnoClient = 100 OrElse mfClient > 0 OrElse noFactureRef <> "") Then done = done AndAlso DBHelper.writeStats("StatFactures", "NoAction, NoFolder, NoClient, NoFacture, MontantFacture, TypeFacture, Description, NoVisite, NoPret, NoFactureRef, NoVente, Taxe1, Taxe2, DateFacture,NoKP,NoUserFacture,ParNoClient,ParNoKP,ParNoUser,ParNoClinique, NoEntitePayeur,Commentaires,TaxesApplication", "5," & IIf(noFolder = 0, "null", noFolder) & "," & IIf(noClient = 0, "null", noClient) & "," & noFacture & "," & mfClient.ToString.Replace(",", ".") & ",'" & typeFacturation.Replace("'", "''") & "','" & description.Replace("'", "''") & "'," & IIf(noVisite = 0, "null", noVisite) & "," & IIf(noPret = 0, "null", noPret) & ",'" & noFactureRef & "'," & IIf(noVente = 0, "null", noVente) & "," & MyTaxe1 & "," & myTaxe2 & ",'" & factureDate.Year & "/" & factureDate.Month & "/" & factureDate.Day & "'," & IIf(noKP = 0, "null", noKP) & "," & IIf(noUserFacture = 0, "null", noUserFacture) & "," & IIf(parnoClient = 0, "null", parnoClient) & "," & IIf(parnoKP = 0, "null", parnoKP) & "," & IIf(parNoUser = 0, "null", parNoUser) & "," & IIf(parNoClinique = 0, "null", parNoClinique) & ",1,'" & factureComments & "'," & CInt(taxesApplication))
                If parnoKP > 0 And (pourcentnoKP = 100 OrElse mfkp > 0 Or noFactureRef <> "") Then done = done AndAlso DBHelper.writeStats("StatFactures", "NoAction, NoFolder, NoClient, NoFacture, MontantFacture, TypeFacture, Description, NoVisite, NoPret, NoFactureRef, NoVente, Taxe1, Taxe2, DateFacture,NoKP,NoUserFacture,ParNoClient,ParNoKP,ParNoUser,ParNoClinique, NoEntitePayeur,Commentaires,TaxesApplication", "5," & IIf(noFolder = 0, "null", noFolder) & "," & IIf(noClient = 0, "null", noClient) & "," & noFacture & "," & mfkp.ToString.Replace(",", ".") & ",'" & typeFacturation.Replace("'", "''") & "','" & description.Replace("'", "''") & "'," & IIf(noVisite = 0, "null", noVisite) & "," & IIf(noPret = 0, "null", noPret) & ",'" & noFactureRef & "'," & IIf(noVente = 0, "null", noVente) & "," & MyTaxe1 & "," & myTaxe2 & ",'" & factureDate.Year & "/" & factureDate.Month & "/" & factureDate.Day & "'," & IIf(noKP = 0, "null", noKP) & "," & IIf(noUserFacture = 0, "null", noUserFacture) & "," & IIf(parnoClient = 0, "null", parnoClient) & "," & IIf(parnoKP = 0, "null", parnoKP) & "," & IIf(parNoUser = 0, "null", parNoUser) & "," & IIf(parNoClinique = 0, "null", parNoClinique) & ",2,'" & factureComments & "'," & CInt(taxesApplication))
                If parNoUser > 0 And (pourcentNoUser = 100 OrElse mfUser > 0 Or noFactureRef <> "") Then done = done AndAlso DBHelper.writeStats("StatFactures", "NoAction, NoFolder, NoClient, NoFacture, MontantFacture, TypeFacture, Description, NoVisite, NoPret, NoFactureRef, NoVente, Taxe1, Taxe2, DateFacture,NoKP,NoUserFacture,ParNoClient,ParNoKP,ParNoUser,ParNoClinique, NoEntitePayeur,Commentaires,TaxesApplication", "5," & IIf(noFolder = 0, "null", noFolder) & "," & IIf(noClient = 0, "null", noClient) & "," & noFacture & "," & mfUser.ToString.Replace(",", ".") & ",'" & typeFacturation.Replace("'", "''") & "','" & description.Replace("'", "''") & "'," & IIf(noVisite = 0, "null", noVisite) & "," & IIf(noPret = 0, "null", noPret) & ",'" & noFactureRef & "'," & IIf(noVente = 0, "null", noVente) & "," & MyTaxe1 & "," & myTaxe2 & ",'" & factureDate.Year & "/" & factureDate.Month & "/" & factureDate.Day & "'," & IIf(noKP = 0, "null", noKP) & "," & IIf(noUserFacture = 0, "null", noUserFacture) & "," & IIf(parnoClient = 0, "null", parnoClient) & "," & IIf(parnoKP = 0, "null", parnoKP) & "," & IIf(parNoUser = 0, "null", parNoUser) & "," & IIf(parNoClinique = 0, "null", parNoClinique) & ",3,'" & factureComments & "'," & CInt(taxesApplication))
                If parNoClinique > 0 And (mfClinique > 0 Or noFactureRef <> "") Then done = done AndAlso DBHelper.writeStats("StatFactures", "NoAction, NoFolder, NoClient, NoFacture, MontantFacture, TypeFacture, Description, NoVisite, NoPret, NoFactureRef, NoVente, Taxe1, Taxe2, DateFacture,NoKP,NoUserFacture,ParNoClient,ParNoKP,ParNoUser,ParNoClinique, NoEntitePayeur,Commentaires,TaxesApplication", "5," & IIf(noFolder = 0, "null", noFolder) & "," & IIf(noClient = 0, "null", noClient) & "," & noFacture & "," & mfClinique.ToString.Replace(",", ".") & ",'" & typeFacturation.Replace("'", "'" & description.Replace("'", "''") & "'") & "',''," & IIf(noVisite = 0, "null", noVisite) & "," & IIf(noPret = 0, "null", noPret) & ",'" & noFactureRef & "'," & IIf(noVente = 0, "null", noVente) & "," & MyTaxe1 & "," & myTaxe2 & ",'" & factureDate.Year & "/" & factureDate.Month & "/" & factureDate.Day & "'," & IIf(noKP = 0, "null", noKP) & "," & IIf(noUserFacture = 0, "null", noUserFacture) & "," & IIf(parnoClient = 0, "null", parnoClient) & "," & IIf(parnoKP = 0, "null", parnoKP) & "," & IIf(parNoUser = 0, "null", parNoUser) & "," & IIf(parNoClinique = 0, "null", parNoClinique) & ",4,'" & factureComments & "'," & CInt(taxesApplication))
            End If
        Catch ex As Exception
            addErrorLog(ex)
            done = False
        End Try

        If done = False Then
            'REM tran DBLinker.GetInstance.RollbackTransaction()
            noFacture = 0
        Else
            'REM tran DBLinker.GetInstance.CommitTransaction()
        End If

        If selfOpened = True Then DBLinker.getInstance().dbConnected = False
        Return noFacture
    End Function

    Public Function deleteFacturation(ByVal noFacture As Integer, Optional ByVal writingStats As Boolean = True, Optional ByVal ensureRemovingTableLinks As Boolean = True, Optional ByVal waitForSynch As Boolean = True) As Boolean
        Dim myFacture As New Bill(noFacture)
        Dim deleted As Boolean = False

        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        If myFacture.takenFromStats = True OrElse DBLinker.getInstance.delDB("Factures", "NoFacture", noFacture, False, , , , waitForSynch) Then
            If ensureRemovingTableLinks Then
                If myFacture.noVisite > 0 Then DBLinker.getInstance.updateDB("InfoVisites", "NoFacture=null", "NoVisite", myFacture.noVisite, False, , False)
                If myFacture.noPret > 0 Then DBLinker.getInstance.updateDB("Prets", "NoFacture=null", "NoPret", myFacture.noPret, False, , False)
                If myFacture.noVente > 0 Then DBLinker.getInstance.updateDB("Ventes", "NoFacture=null", "NoVente", myFacture.noVente, False, , False)
            End If
            If writingStats = True Then
                With myFacture
                    'Mets à zéro le montant facturé
                    If .parNoClient > 0 Then DBHelper.writeStats("StatFactures", "NoAction, NoFolder, NoClient, NoFacture, MontantFacture, TypeFacture, Description, NoVisite, NoPret, NoFactureRef, NoVente, Taxe1, Taxe2, DateFacture, ParNoKp, ParNoClient, ParNoUser, NoKp, NoUserFacture, NoFactureTransfere,NoEntitePayeur,Commentaires,TaxesApplication", "20," & IIf(.noFolder = 0, "null", .noFolder) & "," & IIf(.noClient = 0, "null", .noClient) & "," & noFacture & ",-" & .amountBilledToClient.ToString.Replace(",", ".") & ",'" & .type.Replace("'", "''") & "','" & .description.Replace("'", "''") & "'," & IIf(.noVisite = 0, "null", .noVisite) & "," & IIf(.noPret = 0, "null", .noPret) & ",'" & .noBillRef & "'," & IIf(.noVente = 0, "null", .noVente) & "," & .taxe1.ToString.Replace(",", ".") & "," & .taxe2.ToString.Replace(",", ".") & ",'" & .dateFacture.Year & "/" & .dateFacture.Month & "/" & .dateFacture.Day & "'," & IIf(.parNoKP = 0, "null", .parNoKP) & "," & IIf(.parNoClient = 0, "null", .parNoClient) & "," & IIf(.parNoUser = 0, "null", .parNoUser) & "," & IIf(.noKP = 0, "null", .noKP) & "," & IIf(.noUserFacture = 0, "null", .noUserFacture) & "," & IIf(.noBillTransfered = 0, "null", .noBillTransfered) & ",1,'Remise à zéro de la facture'," & CInt(.taxesApplication))
                    If .parNoKP > 0 Then DBHelper.writeStats("StatFactures", "NoAction, NoFolder, NoClient, NoFacture, MontantFacture, TypeFacture, Description, NoVisite, NoPret, NoFactureRef, NoVente, Taxe1, Taxe2, DateFacture, ParNoKp, ParNoClient, ParNoUser, NoKp, NoUserFacture, NoFactureTransfere,NoEntitePayeur,Commentaires,TaxesApplication", "20," & IIf(.noFolder = 0, "null", .noFolder) & "," & IIf(.noClient = 0, "null", .noClient) & "," & noFacture & ",-" & .amountBilledToKP.ToString.Replace(",", ".") & ",'" & .type.Replace("'", "''") & "','" & .description.Replace("'", "''") & "'," & IIf(.noVisite = 0, "null", .noVisite) & "," & IIf(.noPret = 0, "null", .noPret) & ",'" & .noBillRef & "'," & IIf(.noVente = 0, "null", .noVente) & "," & .taxe1.ToString.Replace(",", ".") & "," & .taxe2.ToString.Replace(",", ".") & ",'" & .dateFacture.Year & "/" & .dateFacture.Month & "/" & .dateFacture.Day & "'," & IIf(.parNoKP = 0, "null", .parNoKP) & "," & IIf(.parNoClient = 0, "null", .parNoClient) & "," & IIf(.parNoUser = 0, "null", .parNoUser) & "," & IIf(.noKP = 0, "null", .noKP) & "," & IIf(.noUserFacture = 0, "null", .noUserFacture) & "," & IIf(.noBillTransfered = 0, "null", .noBillTransfered) & ",2,'Remise à zéro de la facture'," & CInt(.taxesApplication))
                    If .parNoUser > 0 Then DBHelper.writeStats("StatFactures", "NoAction, NoFolder, NoClient, NoFacture, MontantFacture, TypeFacture, Description, NoVisite, NoPret, NoFactureRef, NoVente, Taxe1, Taxe2, DateFacture, ParNoKp, ParNoClient, ParNoUser, NoKp, NoUserFacture, NoFactureTransfere,NoEntitePayeur,Commentaires,TaxesApplication", "20," & IIf(.noFolder = 0, "null", .noFolder) & "," & IIf(.noClient = 0, "null", .noClient) & "," & noFacture & ",-" & .amountBilledToUser.ToString.Replace(",", ".") & ",'" & .type.Replace("'", "''") & "','" & .description.Replace("'", "''") & "'," & IIf(.noVisite = 0, "null", .noVisite) & "," & IIf(.noPret = 0, "null", .noPret) & ",'" & .noBillRef & "'," & IIf(.noVente = 0, "null", .noVente) & "," & .taxe1.ToString.Replace(",", ".") & "," & .taxe2.ToString.Replace(",", ".") & ",'" & .dateFacture.Year & "/" & .dateFacture.Month & "/" & .dateFacture.Day & "'," & IIf(.parNoKP = 0, "null", .parNoKP) & "," & IIf(.parNoClient = 0, "null", .parNoClient) & "," & IIf(.parNoUser = 0, "null", .parNoUser) & "," & IIf(.noKP = 0, "null", .noKP) & "," & IIf(.noUserFacture = 0, "null", .noUserFacture) & "," & IIf(.noBillTransfered = 0, "null", .noBillTransfered) & ",3,'Remise à zéro de la facture'," & CInt(.taxesApplication))
                    If .parNoClinique > 0 Then DBHelper.writeStats("StatFactures", "NoAction, NoFolder, NoClient, NoFacture, MontantFacture, TypeFacture, Description, NoVisite, NoPret, NoFactureRef, NoVente, Taxe1, Taxe2, DateFacture, ParNoKp, ParNoClient, ParNoUser, NoKp, NoUserFacture, NoFactureTransfere,NoEntitePayeur,Commentaires,TaxesApplication", "20," & IIf(.noFolder = 0, "null", .noFolder) & "," & IIf(.noClient = 0, "null", .noClient) & "," & noFacture & ",-" & .amountBilledToClinic.ToString.Replace(",", ".") & ",'" & .type.Replace("'", "''") & "','" & .description.Replace("'", "''") & "'," & IIf(.noVisite = 0, "null", .noVisite) & "," & IIf(.noPret = 0, "null", .noPret) & ",'" & .noBillRef & "'," & IIf(.noVente = 0, "null", .noVente) & "," & .taxe1.ToString.Replace(",", ".") & "," & .taxe2.ToString.Replace(",", ".") & ",'" & .dateFacture.Year & "/" & .dateFacture.Month & "/" & .dateFacture.Day & "'," & IIf(.parNoKP = 0, "null", .parNoKP) & "," & IIf(.parNoClient = 0, "null", .parNoClient) & "," & IIf(.parNoUser = 0, "null", .parNoUser) & "," & IIf(.noKP = 0, "null", .noKP) & "," & IIf(.noUserFacture = 0, "null", .noUserFacture) & "," & IIf(.noBillTransfered = 0, "null", .noBillTransfered) & ",4,'Remise à zéro de la facture'," & CInt(.taxesApplication))

                    'Mets à zéro le montant payé
                    If .parNoClient > 0 Then DBHelper.writeStats("StatPaiements", "NoAction, NoFacture, MontantPaiement, DateTransaction, TypePaiement, Commentaires, NoClient, NoFolder, NoEntitePayeur, ParNoClient, ParNoKP, ParNoUser,ParNoClinique,NoVisite,NoPret,NoVente,NoKP,NoUserFacture", "20," & noFacture & ",-" & .amountPaidByClient.ToString.Replace(",", ".") & ",'" & .dateFacture.Year & "/" & .dateFacture.Month & "/" & .dateFacture.Day & "','','Remise à zéro de la facture'," & IIf(.noClient = 0, "null", .noClient) & "," & IIf(.noFolder = 0, "null", .noFolder) & ",1," & IIf(.parNoClient = 0, "null", .parNoClient) & "," & IIf(.parNoKP = 0, "null", .parNoKP) & "," & IIf(.parNoUser = 0, "null", .parNoUser) & "," & IIf(.parNoClinique = 0, "null", .parNoClinique) & "," & IIf(.noVisite = 0, "null", .noVisite) & "," & IIf(.noPret = 0, "null", .noPret) & "," & IIf(.noVente = 0, "null", .noVente) & "," & IIf(.noKP = 0, "null", .noKP) & "," & IIf(.noUserFacture = 0, "null", .noUserFacture), Date.Today)
                    If .parNoKP > 0 Then DBHelper.writeStats("StatPaiements", "NoAction, NoFacture, MontantPaiement, DateTransaction, TypePaiement, Commentaires, NoClient, NoFolder, NoEntitePayeur, ParNoClient, ParNoKP, ParNoUser,ParNoClinique,NoVisite,NoPret,NoVente,NoKP,NoUserFacture", "20," & noFacture & ",-" & .amountPaidByKP.ToString.Replace(",", ".") & ",'" & .dateFacture.Year & "/" & .dateFacture.Month & "/" & .dateFacture.Day & "','','Remise à zéro de la facture'," & IIf(.noClient = 0, "null", .noClient) & "," & IIf(.noFolder = 0, "null", .noFolder) & ",2," & IIf(.parNoClient = 0, "null", .parNoClient) & "," & IIf(.parNoKP = 0, "null", .parNoKP) & "," & IIf(.parNoUser = 0, "null", .parNoUser) & "," & IIf(.parNoClinique = 0, "null", .parNoClinique) & "," & IIf(.noVisite = 0, "null", .noVisite) & "," & IIf(.noPret = 0, "null", .noPret) & "," & IIf(.noVente = 0, "null", .noVente) & "," & IIf(.noKP = 0, "null", .noKP) & "," & IIf(.noUserFacture = 0, "null", .noUserFacture), Date.Today)
                    If .parNoUser > 0 Then DBHelper.writeStats("StatPaiements", "NoAction, NoFacture, MontantPaiement, DateTransaction, TypePaiement, Commentaires, NoClient, NoFolder, NoEntitePayeur, ParNoClient, ParNoKP, ParNoUser,ParNoClinique,NoVisite,NoPret,NoVente,NoKP,NoUserFacture", "20," & noFacture & ",-" & .amountPaidByUser.ToString.Replace(",", ".") & ",'" & .dateFacture.Year & "/" & .dateFacture.Month & "/" & .dateFacture.Day & "','','Remise à zéro de la facture'," & IIf(.noClient = 0, "null", .noClient) & "," & IIf(.noFolder = 0, "null", .noFolder) & ",3," & IIf(.parNoClient = 0, "null", .parNoClient) & "," & IIf(.parNoKP = 0, "null", .parNoKP) & "," & IIf(.parNoUser = 0, "null", .parNoUser) & "," & IIf(.parNoClinique = 0, "null", .parNoClinique) & "," & IIf(.noVisite = 0, "null", .noVisite) & "," & IIf(.noPret = 0, "null", .noPret) & "," & IIf(.noVente = 0, "null", .noVente) & "," & IIf(.noKP = 0, "null", .noKP) & "," & IIf(.noUserFacture = 0, "null", .noUserFacture), Date.Today)
                    If .parNoClinique > 0 Then DBHelper.writeStats("StatPaiements", "NoAction, NoFacture, MontantPaiement, DateTransaction, TypePaiement, Commentaires, NoClient, NoFolder, NoEntitePayeur, ParNoClient, ParNoKP, ParNoUser,ParNoClinique,NoVisite,NoPret,NoVente,NoKP,NoUserFacture", "20," & noFacture & ",-" & .amountPaidByClinic.ToString.Replace(",", ".") & ",'" & .dateFacture.Year & "/" & .dateFacture.Month & "/" & .dateFacture.Day & "','','Remise à zéro de la facture'," & IIf(.noClient = 0, "null", .noClient) & "," & IIf(.noFolder = 0, "null", .noFolder) & ",4," & IIf(.parNoClient = 0, "null", .parNoClient) & "," & IIf(.parNoKP = 0, "null", .parNoKP) & "," & IIf(.parNoUser = 0, "null", .parNoUser) & "," & IIf(.parNoClinique = 0, "null", .parNoClinique) & "," & IIf(.noVisite = 0, "null", .noVisite) & "," & IIf(.noPret = 0, "null", .noPret) & "," & IIf(.noVente = 0, "null", .noVente) & "," & IIf(.noKP = 0, "null", .noKP) & "," & IIf(.noUserFacture = 0, "null", .noUserFacture), Date.Today)
                End With
            End If
            deleted = True
        End If
        If selfOpened = True Then DBLinker.getInstance().dbConnected = False

        Return deleted
    End Function

    Public Function adjustFacture(ByVal noFacture As Integer, Optional ByVal newFactureAmount As Double = -1, Optional ByVal newPaiementAmount As Double = -1, Optional ByVal newTypeFacturation As String = "", Optional ByVal noFactureRef As String = "NoRef", Optional ByVal comments As String = "", Optional ByVal writingStats As Boolean = True, Optional ByVal paymentType As String = "", Optional ByVal entitePayeur As String = "C", Optional ByVal statDate As Date = LIMIT_DATE, Optional ByVal secondcomments As String = "", Optional ByVal taxe1 As Double = -1, Optional ByVal taxe2 As Double = -1, Optional ByVal noAction As Integer = 0) As FactureAction
        If newFactureAmount <> -1 Then newFactureAmount = roundAmount(newFactureAmount)
        If newPaiementAmount <> -1 Then newPaiementAmount = roundAmount(newPaiementAmount)


        If statDate = LIMIT_DATE Then statDate = Date.Today
        Dim commentsFacture As String = comments, CommentsPaiements As String = secondcomments
        If newFactureAmount = -1 And CommentsPaiements = "" Then CommentsPaiements = commentsFacture

        Dim myFactureAction As FactureAction = CommonProc.FactureAction.Adjusted
        Dim selfOpened As Boolean = False
        If DBLinker.getInstance().dbConnected = False Then DBLinker.getInstance().dbConnected = True : selfOpened = True

        Dim myFacture As New Bill(noFacture)

        Dim myAction As String = "11"
        If paymentType <> "" Then myAction = "12"
        If noAction <> 0 Then myAction = noAction

        If myFacture.takenFromStats = False Then
            Dim MyMontantFacture, myMontantPaiementTotal As Double

            Dim mfClient As Double = -1
            Dim mfkp As Double = -1
            Dim mfUser As Double = -1
            Dim mfClinique As Double = -1

            'Indique qu'il y a un nouveau montant facturé et le change par type de payeur, sinon, prends le montant déjà incrit
            If newFactureAmount <> -1 Then
                MyMontantFacture = newFactureAmount
                Dim diffF As Double = roundAmount(newFactureAmount - myFacture.getBillTotal())
                mfClient = Math.Round(myFacture.amountBilledToClient + diffF * myFacture.getPourcentClient(), 2)
                mfkp = Math.Round(myFacture.amountBilledToKP + diffF * myFacture.getPourcentKP(), 2)
                mfUser = Math.Round(myFacture.amountBilledToUser + diffF * myFacture.getPourcentUser(), 2)
                mfClinique = Math.Round(myFacture.amountBilledToClinic + diffF * myFacture.getPourcentClinic(), 2)
            Else
                MyMontantFacture = myFacture.getBillTotal
            End If

            'Indique qu'il y a un/des nouveau(x) montant(s) payé(s) par type de payeur et les additionne, sinon, prends le montant déjà incrit
            If newPaiementAmount <> -1 Then
                Select Case entitePayeur
                    Case "C"
                        myMontantPaiementTotal = newPaiementAmount + myFacture.amountPaidByKP + myFacture.amountPaidByUser + myFacture.amountPaidByClinic
                    Case "K"
                        myMontantPaiementTotal = newPaiementAmount + myFacture.amountPaidByClient + myFacture.amountPaidByUser + myFacture.amountPaidByClinic
                    Case "U"
                        myMontantPaiementTotal = newPaiementAmount + myFacture.amountPaidByKP + myFacture.amountPaidByClient + myFacture.amountPaidByClinic
                    Case ""
                        myMontantPaiementTotal = newPaiementAmount + myFacture.amountPaidByClient + myFacture.amountPaidByKP + myFacture.amountPaidByUser
                End Select
            Else
                myMontantPaiementTotal = myFacture.amountPaidByClient + myFacture.amountPaidByKP + myFacture.amountPaidByUser + myFacture.amountPaidByClinic
            End If

            'Si le montant payé n'est pas égal au montant facturé, alors créé l'ajustement. Sinon, supprime la facture des factures à payer
            If MyMontantFacture > myMontantPaiementTotal Then
                Dim setString As String = ""

                If mfClient <> -1 Then setString &= "MontantFacture=" & mfClient.ToString.Replace(",", ".") & ","
                If mfkp <> -1 Then setString &= "MontantFactureKP=" & mfkp.ToString.Replace(",", ".") & ","
                If mfUser <> -1 Then setString &= "MontantFactureUser=" & mfUser.ToString.Replace(",", ".") & ","
                If mfClinique <> -1 Then setString &= "MontantFactureClinique=" & mfClinique.ToString.Replace(",", ".") & ","
                If newPaiementAmount <> -1 And entitePayeur = "C" Then setString &= "MontantPaiement=" & newPaiementAmount.ToString.Replace(",", ".") & ","
                If newPaiementAmount <> -1 And entitePayeur = "K" Then setString &= "MontantPaiementKP=" & newPaiementAmount.ToString.Replace(",", ".") & ","
                If newPaiementAmount <> -1 And entitePayeur = "U" Then setString &= "MontantPaiementUser=" & newPaiementAmount.ToString.Replace(",", ".") & ","
                If newPaiementAmount <> -1 And entitePayeur = "" Then setString &= "MontantPaiementClinique=" & newPaiementAmount.ToString.Replace(",", ".") & ","
                If newTypeFacturation <> "" Then myFacture.type = newTypeFacturation : setString &= "TypeFacturation='" & newTypeFacturation.Replace("'", "''") & "',"
                If noFactureRef <> "NoRef" Then myFacture.noBillRef = noFactureRef : setString &= "NoFactureRef='" & noFactureRef.Replace("'", "''") & "',"
                If setString = "" Then Exit Function

                'Change la taxe si nécessaire
                If taxe1 <> -1 AndAlso taxe1 <> myFacture.taxe1 Then setString &= "Taxe1=-1,"
                If taxe2 <> -1 AndAlso taxe2 <> myFacture.taxe2 Then setString &= "Taxe2=-1,"

                setString = setString.Substring(0, setString.Length - 1)

                DBLinker.getInstance.updateDB("Factures", setString, "NoFacture", noFacture, False)
            Else
                deleteFacturation(noFacture, False, False)
                myFactureAction = CommonProc.FactureAction.Deleted
            End If
        Else 'Facture payé en totalité
            Dim montantFacture As Double = myFacture.getBillTotal
            Dim montantPaiement As Double = 0
            Dim myTaxe1 As Double = IIf(taxe1 = -1, myFacture.taxe1, taxe1)
            Dim myTaxe2 As Double = IIf(taxe2 = -1, myFacture.taxe2, taxe2)
            Select Case entitePayeur
                Case "C"
                    montantPaiement = myFacture.amountPaidByClient
                Case "K"
                    montantPaiement = myFacture.amountPaidByKP
                Case "U"
                    montantPaiement = myFacture.amountPaidByUser
                Case ""
                    montantPaiement = myFacture.amountPaidByClinic
            End Select
            If newFactureAmount <> -1 Then montantFacture = newFactureAmount
            If newPaiementAmount <> -1 Then montantPaiement = newPaiementAmount

            If montantFacture < montantPaiement Then Return CommonProc.FactureAction.Err

            If montantFacture <> montantPaiement Then
                With myFacture
                    Select Case entitePayeur
                        Case "C"
                            If createFacturation(.noClient, montantFacture, .type, .dateFacture, .noFolder, .noVisite, .noPret, .noBillRef, .noVente, noFacture, False, montantPaiement, .noKP, .noUserFacture, .parNoClient, .parNoKP, .parNoUser, .getPourcentClient() * 100, .getPourcentKP() * 100, .getPourcentUser() * 100, .amountPaidByUser, .amountPaidByKP, .amountPaidByClinic, .parNoClinique, True, , taxe1, taxe2, , , .taxesApplication) = 0 Then Return FactureAction.Err
                        Case "K"
                            If createFacturation(.noClient, montantFacture, .type, .dateFacture, .noFolder, .noVisite, .noPret, .noBillRef, .noVente, noFacture, False, .amountPaidByClient, .noKP, .noUserFacture, .parNoClient, .parNoKP, .parNoUser, .getPourcentClient() * 100, .getPourcentKP() * 100, .getPourcentUser() * 100, .amountPaidByUser, montantPaiement, .amountPaidByClinic, .parNoClinique, True, , taxe1, taxe2, , , .taxesApplication) = 0 Then Return FactureAction.Err
                        Case "U"
                            If createFacturation(.noClient, montantFacture, .type, .dateFacture, .noFolder, .noVisite, .noPret, .noBillRef, .noVente, noFacture, False, .amountPaidByClient, .noKP, .noUserFacture, .parNoClient, .parNoKP, .parNoUser, .getPourcentClient() * 100, .getPourcentKP() * 100, .getPourcentUser() * 100, montantPaiement, .amountPaidByKP, .amountPaidByClinic, .parNoClinique, True, , taxe1, taxe2, , , .taxesApplication) = 0 Then Return FactureAction.Err
                        Case ""
                            If createFacturation(.noClient, montantFacture, .type, .dateFacture, .noFolder, .noVisite, .noPret, .noBillRef, .noVente, noFacture, False, .amountPaidByClient, .noKP, .noUserFacture, .parNoClient, .parNoKP, .parNoUser, .getPourcentClient() * 100, .getPourcentKP() * 100, .getPourcentUser() * 100, .amountPaidByUser, .amountPaidByKP, montantPaiement, .parNoClinique, True, , taxe1, taxe2, , , .taxesApplication) = 0 Then Return FactureAction.Err
                        Case "A"
                            If createFacturation(.noClient, montantFacture, .type, .dateFacture, .noFolder, .noVisite, .noPret, .noBillRef, .noVente, noFacture, False, .amountPaidByClient, .noKP, .noUserFacture, .parNoClient, .parNoKP, .parNoUser, .getPourcentClient() * 100, .getPourcentKP() * 100, .getPourcentUser() * 100, .amountPaidByUser, .amountPaidByKP, .amountPaidByClinic, .parNoClinique, True, , taxe1, taxe2, , , .taxesApplication) = 0 Then Return FactureAction.Err
                    End Select
                    .saveDescription()
                End With
                myFactureAction = CommonProc.FactureAction.Created
            End If
        End If

        If noFactureRef = "NoRef" Then noFactureRef = ""

        If writingStats = True Then
            With myFacture
                If newFactureAmount <> -1 Then
                    Dim MyTaxe1, myTaxe2 As String
                    MyTaxe1 = IIf(taxe1 = -1, myFacture.taxe1, taxe1).ToString.Replace(",", ".")
                    myTaxe2 = IIf(taxe2 = -1, myFacture.taxe2, taxe2).ToString.Replace(",", ".")

                    Dim oldTotal As Double = .getBillTotal()
                    myFacture = New Bill(myFacture.noFacture)

                    Dim diffF As Double = roundAmount(newFactureAmount - oldTotal)
                    Dim mfClient As Double = diffF * myFacture.getPourcentClient()
                    Dim mfkp As Double = diffF * myFacture.getPourcentKP()
                    Dim mfUser As Double = diffF * myFacture.getPourcentUser()
                    Dim mfClinique As Double = diffF * myFacture.getPourcentClinic()
                    If myFacture.parNoClient > 0 Then DBHelper.writeStats("StatFactures", "NoAction, NoFolder, NoClient, NoFacture, MontantFacture, TypeFacture, Description, NoVisite, NoPret, NoFactureRef, NoVente, Taxe1, Taxe2, DateFacture, ParNoKp, ParNoClient, ParNoUser, NoKp, NoUserFacture, NoFactureTransfere, Commentaires,ParNoClinique, NoEntitePayeur,taxesApplication", myAction & "," & IIf(.noFolder = 0, "null", .noFolder) & "," & IIf(.noClient = 0, "null", .noClient) & "," & noFacture & "," & mfClient.ToString.Replace(",", ".") & ",'" & .type.Replace("'", "''") & "','" & .description.Replace("'", "''") & "'," & IIf(.noVisite = 0, "null", .noVisite) & "," & IIf(.noPret = 0, "null", .noPret) & ",'" & .noBillRef & "'," & IIf(.noVente = 0, "null", .noVente) & "," & MyTaxe1 & "," & myTaxe2 & ",'" & .dateFacture.Year & "/" & .dateFacture.Month & "/" & .dateFacture.Day & "'," & IIf(.parNoKP = 0, "null", .parNoKP) & "," & IIf(.parNoClient = 0, "null", .parNoClient) & "," & IIf(.parNoUser = 0, "null", .parNoUser) & "," & IIf(.noKP = 0, "null", .noKP) & "," & IIf(.noUserFacture = 0, "null", .noUserFacture) & "," & IIf(.noBillTransfered = 0, "null", .noBillTransfered) & ",'" & commentsFacture.Replace("'", "''") & "'," & IIf(.parNoClinique = 0, "null", .parNoClinique) & ",1," & CInt(.taxesApplication), statDate)
                    If myFacture.parNoKP > 0 Then DBHelper.writeStats("StatFactures", "NoAction, NoFolder, NoClient, NoFacture, MontantFacture, TypeFacture, Description, NoVisite, NoPret, NoFactureRef, NoVente, Taxe1, Taxe2, DateFacture, ParNoKp, ParNoClient, ParNoUser, NoKp, NoUserFacture, NoFactureTransfere, Commentaires,ParNoClinique, NoEntitePayeur,taxesApplication", myAction & "," & IIf(.noFolder = 0, "null", .noFolder) & "," & IIf(.noClient = 0, "null", .noClient) & "," & noFacture & "," & mfkp.ToString.Replace(",", ".") & ",'" & .type.Replace("'", "''") & "','" & .description.Replace("'", "''") & "'," & IIf(.noVisite = 0, "null", .noVisite) & "," & IIf(.noPret = 0, "null", .noPret) & ",'" & .noBillRef & "'," & IIf(.noVente = 0, "null", .noVente) & "," & MyTaxe1 & "," & myTaxe2 & ",'" & .dateFacture.Year & "/" & .dateFacture.Month & "/" & .dateFacture.Day & "'," & IIf(.parNoKP = 0, "null", .parNoKP) & "," & IIf(.parNoClient = 0, "null", .parNoClient) & "," & IIf(.parNoUser = 0, "null", .parNoUser) & "," & IIf(.noKP = 0, "null", .noKP) & "," & IIf(.noUserFacture = 0, "null", .noUserFacture) & "," & IIf(.noBillTransfered = 0, "null", .noBillTransfered) & ",'" & commentsFacture.Replace("'", "''") & "'," & IIf(.parNoClinique = 0, "null", .parNoClinique) & ",2," & CInt(.taxesApplication), statDate)
                    If myFacture.parNoUser > 0 Then DBHelper.writeStats("StatFactures", "NoAction, NoFolder, NoClient, NoFacture, MontantFacture, TypeFacture, Description, NoVisite, NoPret, NoFactureRef, NoVente, Taxe1, Taxe2, DateFacture, ParNoKp, ParNoClient, ParNoUser, NoKp, NoUserFacture, NoFactureTransfere, Commentaires,ParNoClinique, NoEntitePayeur,taxesApplication", myAction & "," & IIf(.noFolder = 0, "null", .noFolder) & "," & IIf(.noClient = 0, "null", .noClient) & "," & noFacture & "," & mfUser.ToString.Replace(",", ".") & ",'" & .type.Replace("'", "''") & "','" & .description.Replace("'", "''") & "'," & IIf(.noVisite = 0, "null", .noVisite) & "," & IIf(.noPret = 0, "null", .noPret) & ",'" & .noBillRef & "'," & IIf(.noVente = 0, "null", .noVente) & "," & MyTaxe1 & "," & myTaxe2 & ",'" & .dateFacture.Year & "/" & .dateFacture.Month & "/" & .dateFacture.Day & "'," & IIf(.parNoKP = 0, "null", .parNoKP) & "," & IIf(.parNoClient = 0, "null", .parNoClient) & "," & IIf(.parNoUser = 0, "null", .parNoUser) & "," & IIf(.noKP = 0, "null", .noKP) & "," & IIf(.noUserFacture = 0, "null", .noUserFacture) & "," & IIf(.noBillTransfered = 0, "null", .noBillTransfered) & ",'" & commentsFacture.Replace("'", "''") & "'," & IIf(.parNoClinique = 0, "null", .parNoClinique) & ",3," & CInt(.taxesApplication), statDate)
                    If myFacture.parNoClinique > 0 Then DBHelper.writeStats("StatFactures", "NoAction, NoFolder, NoClient, NoFacture, MontantFacture, TypeFacture, Description, NoVisite, NoPret, NoFactureRef, NoVente, Taxe1, Taxe2, DateFacture, ParNoKp, ParNoClient, ParNoUser, NoKp, NoUserFacture, NoFactureTransfere, Commentaires,ParNoClinique, NoEntitePayeur,taxesApplication", myAction & "," & IIf(.noFolder = 0, "null", .noFolder) & "," & IIf(.noClient = 0, "null", .noClient) & "," & noFacture & "," & mfClinique.ToString.Replace(",", ".") & ",'" & .type.Replace("'", "''") & "','" & .description.Replace("'", "''") & "'," & IIf(.noVisite = 0, "null", .noVisite) & "," & IIf(.noPret = 0, "null", .noPret) & ",'" & .noBillRef & "'," & IIf(.noVente = 0, "null", .noVente) & "," & MyTaxe1 & "," & myTaxe2 & ",'" & .dateFacture.Year & "/" & .dateFacture.Month & "/" & .dateFacture.Day & "'," & IIf(.parNoKP = 0, "null", .parNoKP) & "," & IIf(.parNoClient = 0, "null", .parNoClient) & "," & IIf(.parNoUser = 0, "null", .parNoUser) & "," & IIf(.noKP = 0, "null", .noKP) & "," & IIf(.noUserFacture = 0, "null", .noUserFacture) & "," & IIf(.noBillTransfered = 0, "null", .noBillTransfered) & ",'" & commentsFacture.Replace("'", "''") & "'," & IIf(.parNoClinique = 0, "null", .parNoClinique) & ",4," & CInt(.taxesApplication), statDate)
                End If
                If newPaiementAmount <> -1 Then
                    Dim nbPayeur As Byte = 0
                    Dim diffP As Double = 0
                    Select Case entitePayeur
                        Case "C"
                            diffP = newPaiementAmount - .amountPaidByClient
                        Case "K"
                            diffP = newPaiementAmount - .amountPaidByKP
                        Case "U"
                            diffP = newPaiementAmount - .amountPaidByUser
                        Case ""
                            diffP = newPaiementAmount - .amountPaidByClinic
                        Case "A"
                            If .parNoClient > 0 Then nbPayeur += 1
                            If .parNoClinique > 0 Then nbPayeur += 1
                            If .parNoKP > 0 Then nbPayeur += 1
                            If .parNoUser > 0 Then nbPayeur += 1
                            diffP = (newPaiementAmount - .getBillPaymentTotal()) / nbPayeur
                    End Select
                    diffP = roundAmount(diffP)
                    If entitePayeur = "A" Then
                        If .parNoClient > 0 Then DBHelper.writeStats("StatPaiements", "NoAction, NoFacture, MontantPaiement, DateTransaction, TypePaiement, Commentaires, NoClient, NoFolder, NoEntitePayeur, ParNoClient, ParNoKP, ParNoUser,ParNoClinique,NoVisite,NoPret,NoVente,NoKP,NoUserFacture", myAction & "," & noFacture & "," & diffP.ToString.Replace(",", ".") & ",'" & .dateFacture.Year & "/" & .dateFacture.Month & "/" & .dateFacture.Day & "','" & paymentType.Replace("'", "''") & "','" & CommentsPaiements.Replace("'", "''") & "'," & IIf(.noClient = 0, "null", .noClient) & "," & IIf(.noFolder = 0, "null", .noFolder) & ",1," & IIf(.parNoClient = 0, "null", .parNoClient) & "," & IIf(.parNoKP = 0, "null", .parNoKP) & "," & IIf(.parNoUser = 0, "null", .parNoUser) & "," & IIf(.parNoClinique = 0, "null", .parNoClinique) & "," & IIf(.noVisite = 0, "null", .noVisite) & "," & IIf(.noPret = 0, "null", .noPret) & "," & IIf(.noVente = 0, "null", .noVente) & "," & IIf(.noKP = 0, "null", .noKP) & "," & IIf(.noUserFacture = 0, "null", .noUserFacture), statDate)
                        If .parNoClinique > 0 Then DBHelper.writeStats("StatPaiements", "NoAction, NoFacture, MontantPaiement, DateTransaction, TypePaiement, Commentaires, NoClient, NoFolder, NoEntitePayeur, ParNoClient, ParNoKP, ParNoUser,ParNoClinique,NoVisite,NoPret,NoVente,NoKP,NoUserFacture", myAction & "," & noFacture & "," & diffP.ToString.Replace(",", ".") & ",'" & .dateFacture.Year & "/" & .dateFacture.Month & "/" & .dateFacture.Day & "','" & paymentType.Replace("'", "''") & "','" & CommentsPaiements.Replace("'", "''") & "'," & IIf(.noClient = 0, "null", .noClient) & "," & IIf(.noFolder = 0, "null", .noFolder) & ",4," & IIf(.parNoClient = 0, "null", .parNoClient) & "," & IIf(.parNoKP = 0, "null", .parNoKP) & "," & IIf(.parNoUser = 0, "null", .parNoUser) & "," & IIf(.parNoClinique = 0, "null", .parNoClinique) & "," & IIf(.noVisite = 0, "null", .noVisite) & "," & IIf(.noPret = 0, "null", .noPret) & "," & IIf(.noVente = 0, "null", .noVente) & "," & IIf(.noKP = 0, "null", .noKP) & "," & IIf(.noUserFacture = 0, "null", .noUserFacture), statDate)
                        If .parNoKP > 0 Then DBHelper.writeStats("StatPaiements", "NoAction, NoFacture, MontantPaiement, DateTransaction, TypePaiement, Commentaires, NoClient, NoFolder, NoEntitePayeur, ParNoClient, ParNoKP, ParNoUser,ParNoClinique,NoVisite,NoPret,NoVente,NoKP,NoUserFacture", myAction & "," & noFacture & "," & diffP.ToString.Replace(",", ".") & ",'" & .dateFacture.Year & "/" & .dateFacture.Month & "/" & .dateFacture.Day & "','" & paymentType.Replace("'", "''") & "','" & CommentsPaiements.Replace("'", "''") & "'," & IIf(.noClient = 0, "null", .noClient) & "," & IIf(.noFolder = 0, "null", .noFolder) & ",2," & IIf(.parNoClient = 0, "null", .parNoClient) & "," & IIf(.parNoKP = 0, "null", .parNoKP) & "," & IIf(.parNoUser = 0, "null", .parNoUser) & "," & IIf(.parNoClinique = 0, "null", .parNoClinique) & "," & IIf(.noVisite = 0, "null", .noVisite) & "," & IIf(.noPret = 0, "null", .noPret) & "," & IIf(.noVente = 0, "null", .noVente) & "," & IIf(.noKP = 0, "null", .noKP) & "," & IIf(.noUserFacture = 0, "null", .noUserFacture), statDate)
                        If .parNoUser > 0 Then DBHelper.writeStats("StatPaiements", "NoAction, NoFacture, MontantPaiement, DateTransaction, TypePaiement, Commentaires, NoClient, NoFolder, NoEntitePayeur, ParNoClient, ParNoKP, ParNoUser,ParNoClinique,NoVisite,NoPret,NoVente,NoKP,NoUserFacture", myAction & "," & noFacture & "," & diffP.ToString.Replace(",", ".") & ",'" & .dateFacture.Year & "/" & .dateFacture.Month & "/" & .dateFacture.Day & "','" & paymentType.Replace("'", "''") & "','" & CommentsPaiements.Replace("'", "''") & "'," & IIf(.noClient = 0, "null", .noClient) & "," & IIf(.noFolder = 0, "null", .noFolder) & ",3," & IIf(.parNoClient = 0, "null", .parNoClient) & "," & IIf(.parNoKP = 0, "null", .parNoKP) & "," & IIf(.parNoUser = 0, "null", .parNoUser) & "," & IIf(.parNoClinique = 0, "null", .parNoClinique) & "," & IIf(.noVisite = 0, "null", .noVisite) & "," & IIf(.noPret = 0, "null", .noPret) & "," & IIf(.noVente = 0, "null", .noVente) & "," & IIf(.noKP = 0, "null", .noKP) & "," & IIf(.noUserFacture = 0, "null", .noUserFacture), statDate)
                    Else
                        DBHelper.writeStats("StatPaiements", "NoAction, NoFacture, MontantPaiement, DateTransaction, TypePaiement, Commentaires, NoClient, NoFolder, NoEntitePayeur, ParNoClient, ParNoKP, ParNoUser,ParNoClinique,NoVisite,NoPret,NoVente,NoKP,NoUserFacture", myAction & "," & noFacture & "," & diffP.ToString.Replace(",", ".") & ",'" & .dateFacture.Year & "/" & .dateFacture.Month & "/" & .dateFacture.Day & "','" & paymentType.Replace("'", "''") & "','" & CommentsPaiements.Replace("'", "''") & "'," & IIf(.noClient = 0, "null", .noClient) & "," & IIf(.noFolder = 0, "null", .noFolder) & ",'" & BillsManager.translatePayingEntity(entitePayeur) & "'," & IIf(.parNoClient = 0, "null", .parNoClient) & "," & IIf(.parNoKP = 0, "null", .parNoKP) & "," & IIf(.parNoUser = 0, "null", .parNoUser) & "," & IIf(.parNoClinique = 0, "null", .parNoClinique) & "," & IIf(.noVisite = 0, "null", .noVisite) & "," & IIf(.noPret = 0, "null", .noPret) & "," & IIf(.noVente = 0, "null", .noVente) & "," & IIf(.noKP = 0, "null", .noKP) & "," & IIf(.noUserFacture = 0, "null", .noUserFacture), statDate)
                    End If
                End If
            End With
        End If
        If selfOpened = True Then DBLinker.getInstance().dbConnected = False

        Return myFactureAction
    End Function

    Public Function calcAmountPlusTaxes(ByVal amount As Double, Optional ByVal taxe1 As Double = -1, Optional ByVal taxe2 As Double = -1, Optional ByVal taxesApplication As TaxesApplications = TaxesApplications.PREFERENCE) As Double
        If taxe1 = 0 AndAlso taxe2 = 0 Then Return roundAmount(amount)

        'Toujours passé un montant avec une virgule et non un point pour indiquer les décimales
        Dim UsedTaxe1, usedTaxe2 As Double
        If taxe1 = -1 Then UsedTaxe1 = PreferencesManager.getGeneralPreferences()("Taxe1") Else UsedTaxe1 = taxe1
        If taxe2 = -1 Then usedTaxe2 = PreferencesManager.getGeneralPreferences()("Taxe2") Else usedTaxe2 = taxe2
        taxesApplication = If(taxesApplication = TaxesApplications.PREFERENCE, If(PreferencesManager.getGeneralPreferences("TaxesApplication").IndexOf("1") = -1, TaxesApplications.TAX2_ON_AMOUNT, TaxesApplications.TAX2_ON_TAX1), taxesApplication)

        If taxesApplication = TaxesApplications.TAX2_ON_TAX1 Then
            amount = amount * (UsedTaxe1 / 100.0F + 1.0F) * (usedTaxe2 / 100.0F + 1.0F)
        Else
            amount = amount * (1.0F + (UsedTaxe1 / 100.0F) + (usedTaxe2 / 100.0F))
        End If

        Return roundAmount(amount)
    End Function

    Public Function calcAmountMinusTaxes(ByVal amount As Double, Optional ByVal taxe1 As Double = -1, Optional ByVal taxe2 As Double = -1, Optional ByVal taxesApplication As TaxesApplications = TaxesApplications.PREFERENCE) As Double
        'Toujours passé un montant avec une virgule et non un point pour indiquer les décimales
        Dim UsedTaxe1, usedTaxe2 As Double
        If taxe1 = -1 Then UsedTaxe1 = PreferencesManager.getGeneralPreferences()("Taxe1") Else UsedTaxe1 = taxe1
        If taxe2 = -1 Then usedTaxe2 = PreferencesManager.getGeneralPreferences()("Taxe2") Else usedTaxe2 = taxe2
        taxesApplication = If(taxesApplication = TaxesApplications.PREFERENCE, If(PreferencesManager.getGeneralPreferences("TaxesApplication").IndexOf("1") = -1, TaxesApplications.TAX2_ON_AMOUNT, TaxesApplications.TAX2_ON_TAX1), taxesApplication)

        If taxesApplication = TaxesApplications.TAX2_ON_TAX1 Then
            amount = amount / (UsedTaxe1 / 100.0F + 1.0F) / (usedTaxe2 / 100.0F + 1.0F)
        Else
            amount = amount / (1.0F + (UsedTaxe1 / 100.0F) + (usedTaxe2 / 100.0F))
        End If

        Return roundAmount(amount, True)
    End Function

    Public Function roundAmount(ByVal amount As Double, Optional ByVal reverse As Boolean = False) As Double
        Dim strAmount As String = amount.ToString.Replace(".", ",")
        Dim virPos As Short
        virPos = strAmount.IndexOf(",")

        If strAmount.Length <= (virPos + 3) Then Return strAmount

        If virPos >= 0 Then
            amount *= 100
            If PreferencesManager.getGeneralPreferences()("TaxeArrondissement").IndexOf("supérieure") >= 0 Then
                If PreferencesManager.getGeneralPreferences()("TaxeArrondissement").IndexOf("Arrondir") >= 0 Then
                    amount = Math.Round(amount / 100, 2) * 100
                Else
                    amount = Math.Ceiling(amount)
                    If reverse = False Then amount = Math.Floor(amount)
                End If
            Else
                amount = Math.Floor(amount)
                If reverse = True Then amount = Math.Ceiling(amount)
            End If
            amount /= 100
        End If

        Return amount
    End Function
End Module


