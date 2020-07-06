DECLARE @NbFuturForNbDaysMultiple int
SET @NbFuturForNbDaysMultiple = 5
DECLARE @MinDate datetime
SET @MinDate = '2009/01/01'
DECLARE @FTTCategorie int
SET @FTTCategorie = 5
DECLARE @ChosenNoFolder int
SET @ChosenNoFolder = null


SET @NbFuturForNbDaysMultiple = @NbFuturForNbDaysMultiple+1
IF object_id('tempdb..#FolderTextesAnalyse') IS NULL
	CREATE TABLE #FolderTextesAnalyse (NoFolderTexte int null,NoFolderTexteType int, NoFolder int, TexteTitle varchar(MAX),DateStarted datetime,DateFinished datetime,NoMultiple int, Erronus int, Status varchar(MAX))

DELETE FROM #FolderTextesAnalyse

SELECT * FROM FolderDates WHERE nofolder=18587
SELECT * FROM #FolderTextesAnalyse WHERE TexteTitle NOT LIKE '%médecin'
SELECT * FROM #FolderTextesAnalyse WHERE  Erronus>0 and status LIKE '%closingdate=)%' AND NOT (TexteTitle='Historique,Évaluation,Analyse,But,Plan' OR TexteTitle='Notes' OR TexteTitle='Rapport au médecin') AND Status LIKE '%(ClosingDate=)%'  AND NOT Status LIKE '%(Same ClosingDate=%'

SELECT noclient,notrptraitant,* FROM #FolderTextesAnalyse AS FTA inner join infofolders as if1 on if1.nofolder=fta.nofolder WHERE  Erronus=1 and status LIKE '%closingdate=)%' and notrptraitant=6
select * from usersalerts where nouser=6
select * from usersalerts where nouseralert=29096

PRINT 'Should start soon'

SELECT * FROM #FolderTextesAnalyse  WHERE NoFolder=21267 -- 20626
SELECT * FROM infofolders  WHERE NoFolder=17966
SELECT * FROM Folderdates  WHERE NoFolder=17966

DECLARE @NoClient int
DECLARE @NoUserAlert int
DECLARE @NbDaysX int
DECLARE @NbPresencesX int
DECLARE @TypeForMultiple int
DECLARE @NbPresencesMultiple int
DECLARE @NbDaysMultiple int
DECLARE @NbMultipleEnding int
DECLARE @Status varchar(MAX)
DECLARE @Erronus int
DECLARE @Multiple bit
DECLARE @looping bit
DECLARE @NoFolderTexte int
DECLARE @LastNoFolderTexte int
DECLARE @NoFolderTexteType int
DECLARE @WhenToBeCreated int
DECLARE @WhenToBeStopped int
DECLARE @TexteTitle varchar(MAX)
DECLARE @FTTTexteTitle varchar(MAX)
DECLARE @PosVisite int
DECLARE @MaxPosVisite int
DECLARE @RVDate datetime
DECLARE @DateFinished datetime
DECLARE @DateStarted datetime
DECLARE @NoFolder int
DECLARE @CurNoMultiple int 
DECLARE @DateOverToday int
DECLARE @FolderActive bit
DECLARE @FolderClosing datetime
DECLARE @LastPresence datetime
DECLARE @FolderCreation datetime
DECLARE my_cursor CURSOR LOCAL FOR
	--((FTT.WhenToBeCreated=2 AND FTT.TypeForMultiple<=1) OR (FTT.WhenToBeCreated<2 AND FTT.TypeForMultiple=1)) AND 
	SELECT NoFolder,NoFolderTexteType FROM FolderTexteTypes AS FTT INNER JOIN FolderDates ON FolderDates.NoCodification=FTT.NoCodification WHERE (NoModeleCategorie=@FTTCategorie OR @FTTCategorie IS null) AND CreationDate>=@MinDate AND (NoFolder=@ChosenNoFolder OR @ChosenNoFolder IS null)
	 ORDER BY NoFolder
	OPEN my_cursor
	FETCH NEXT FROM my_cursor INTO @NoFolder,@NoFolderTexteType

	WHILE @@FETCH_STATUS = 0
	BEGIN
PRINT '----------------------------------------------->'


	SELECT @FolderCreation=CreationDate,@NoClient=NoClient,@LastPresence=LastTraitement,@NbMultipleEnding=NbMultipleEnding,@FolderClosing=DATEADD(second,DATEPART(second,ClosingDate)*-1,DATEADD(mi,DATEPART(mi,ClosingDate)*-1,DATEADD(hh,DATEPART(hh,ClosingDate)*-1,ClosingDate))),@WhenToBeStopped=WhenToBeStopped,@FolderActive=StatutOuvert,@TypeForMultiple=TypeForMultiple,@NbPresencesX=NbPresencesX,@NbDaysX=NbDaysX,@NbDaysMultiple=NbDaysMultiple,@NbPresencesMultiple=NbPresencesMultiple,@Multiple=Multiple,@FTTTexteTitle=TexteTitle,@WhenToBeCreated=WhenToBeCreated FROM FolderTexteTypes AS FTT INNER JOIN FolderDates ON FolderDates.NoCodification=FTT.NoCodification WHERE FolderDates.NoFolder=@NoFolder AND FTT.NoFolderTexteType=@NoFolderTexteType
	PRINT CAST(@NoClient AS varchar(MAX)) + ':' + CAST(@NoFolder AS varchar(MAX)) + '/' + CAST(@NoFolderTexteType AS varchar(MAX)) + ' - ' + @FTTTexteTitle

	----- Analyse by creating virtually the texts
	--GET RV DATE
	SET @CurNoMultiple=1
--	IF @WhenToBeCreated<2
--	BEGIN
--
--	END
	

	SET @PosVisite = @NbPresencesX
--	IF @WhenToBeCreated<2 AND @TypeForMultiple=1
--	BEGIN
--		SET @PosVisite = @NbPresencesX + @NbPresencesMultiple
--		SET @CurNoMultiple=2
--	END
	PRINT 'PosVisite : ' + CAST(@PosVisite AS varchar(max));
	
	SET @looping = 1
	SET @DateOverToday = 0
	WHILE @looping = 1
	BEGIN
		SET @RVDate = null
		SET @TexteTitle = @FTTTexteTitle;
		IF (@WhenToBeCreated<2 AND @TypeForMultiple<>1) OR (@WhenToBeCreated<2 AND @TypeForMultiple=1 AND @CurNoMultiple<>1)
			SET @RVDate = @FolderCreation
		ELSE
		BEGIN
			IF @WhenToBeCreated=3
				SELECT @RVDate = CASE WHEN @FolderActive = 1 THEN null ELSE @FolderClosing END
			ELSE
				WITH T AS (SELECT ROW_NUMBER() OVER(ORDER BY DateHeure) AS PosVisite,InfoVisites.* FROM InfoVisites WHERE NoFolder=@NoFolder AND NoStatut=4) SELECT @MaxPosVisite=(SELECT MAX(PosVisite) FROM t),@RVDate=DATEADD(mi,DATEPART(mi,DateHeure)*-1,DATEADD(hh,DATEPART(hh,DateHeure)*-1,DateHeure)) FROM T WHERE PosVisite=@PosVisite
		END
		IF @WhenToBeCreated <= 2 AND (@CurNoMultiple=1 OR @TypeForMultiple<>2)
			SELECT @RVDate = DATEADD(d, @NbDaysX, @RVDate)
print @RVDate

		--IF @RVDate > GETDATE() AND @CurNoMultiple=1
		--	SET @DateOverToday = @DateOverToday + 1
	
		--Adjust date for multiple where TypeForMultiple = NbDaysX
--NOT @RVDate IS null AND 
		IF @Multiple=1 AND @TypeForMultiple=0 AND @CurNoMultiple>1
		BEGIN
			SELECT @RVDate = DATEADD(d, (@CurNoMultiple-1) * @NbDaysMultiple, @RVDate)
			--Block to 1 in the future max
print @RVDate
		END
--		IF @RVDate IS null AND @Multiple=1 AND @TypeForMultiple=0 AND @CurNoMultiple>1
--		BEGIN
--			SELECT @RVDate
--		END

		IF @RVDate > GETDATE() AND @Multiple=1 AND @TypeForMultiple=0 AND @CurNoMultiple>1
		BEGIN
			IF @DateOverToday < @NbFuturForNbDaysMultiple
				SELECT @DateOverToday = @DateOverToday + 1
			ELSE
			BEGIN
				SET @DateOverToday = 0
				SET @RVDate = null
			END
		END

PRINT '@DateOverToday:' + CAST(@DateOverToday AS varchar(3))

		-- Multiple is stopped on folderclosed
PRINT CAST(@CurNoMultiple AS varchar(1)) + ':' + dbo.AffTextDate(@RVDate) + '-' + dbo.AffTextDate(@FolderClosing)
----- SHOULD BE @RVDate>@FolderClosing    NOT >=
		IF @Multiple = 1 AND @WhenToBeStopped=0 AND @FolderActive=0 AND @RVDate>@FolderClosing
			SET @RVDate = null

PRINT @RVDate
		-- Multiple is stopped on max
		IF @Multiple = 1 AND @CurNoMultiple>1 AND @WhenToBeStopped=1 AND @NbMultipleEnding<@CurNoMultiple
			SET @RVDate = null
PRINT @RVDate
		IF @RVDate IS null
			SET @looping = 0
		
		IF @looping = 1
		BEGIN
			IF @Multiple = 1
				SET @TexteTitle = @TexteTitle + ' ' + cast(@CurNoMultiple as varchar(max))

			SET @Erronus = 0
			SET @Status = ''
			SET @LastNoFolderTexte = @NoFolderTexte
			SET @NoFolderTexte = null
			SET @NoUserAlert = null
			SET @DateFinished  = null
			SET @DateStarted   = null
			SELECT @NoFolderTexte = NoFolderTexte,@DateFinished=DateFinished,@DateStarted=DateStarted FROM FolderTextes WHERE NoFolder=@NoFolder AND TexteTitle=@TexteTitle
			SELECT @NoUserAlert=NoUserAlert FROM UsersAlerts WHERE ([Message] LIKE '%' + @TexteTitle + '%' OR [Message] LIKE '%' + @FTTTexteTitle + ' #' + CAST(@CurNoMultiple AS varchar(MAX)) + '%') AND AlertData=CAST(@NoClient AS varchar(MAX)) AND AlarmData LIKE '%:' + CAST(@NoClient AS varchar(MAX)) + ':' + CAST(@NoFolder AS varchar(MAX))  +  ':True'

PRINT 'DEBUG'
PRINT @NoFolderTexte
PRINT @Multiple
PRINT @TypeForMultiple
PRINT @NoUserAlert
PRINT '%:' + CAST(@NoClient AS varchar(MAX)) + ':' + CAST(@NoFolder AS varchar(MAX))  +  ':True'
PRINT 'END DEBUG'
			-- Texte doesn't exist
			IF @NoFolderTexte IS NULL --AND (@Multiple=0 OR @TypeForMultiple<>0 OR (@Multiple=1 AND @TypeForMultiple=0 AND @NoUserAlert IS NULL))
			BEGIN
				IF @DateOverToday<2
				BEGIN
					SET @Erronus = 1
					SELECT @Status = CASE WHEN @NoFolderTexte IS NULL AND @NoUserAlert IS NULL THEN 'Le texte et l''alerte' ELSE CASE WHEN @NoFolderTexte IS NULL THEN 'Le texte' ELSE 'L''alerte'  END END + ' est/sont inexistant(e)(s) (' + CASE WHEN @FolderClosing=@RVDate THEN 'Same ' ELSE '' END + 'ClosingDate=' + CASE WHEN @FolderActive=0 THEN dbo.AffTextDate(@FolderClosing) ELSE '' END + ') (LastPresence=' + dbo.AffTextDate(@LastPresence) + ')'
				END
				ELSE
					SELECT @Status = 'Texte futur'
				
				SET @DateStarted = @RVDate
			END
			ELSE
			BEGIN
				IF @DateOverToday<2 AND CAST(dbo.AffTextDate(@DateStarted) AS varchar(10)) <> CAST(dbo.AffTextDate(@RVDate) as varchar(10))
				BEGIN
					SET @Erronus = 2
					SELECT @Status = 'Le texte n''a pas la bonne date de début (' + CAST(dbo.AffTextDate(@RVDate) as varchar(10)) + ')'
				END
			END
			PRINT '>' + @TexteTitle
			INSERT INTO #FolderTextesAnalyse (NoFolderTexte,NoFolderTexteType,NoMultiple, NoFolder, TexteTitle,DateStarted,DateFinished, Erronus , Status )
			VALUES (@NoFolderTexte,@NoFolderTexteType,@CurNoMultiple, @NoFolder, @TexteTitle, @DateStarted,@DateFinished,@Erronus,@Status)

			--Set next
			IF @Multiple = 1
			BEGIN
				SELECT @CurNoMultiple = @CurNoMultiple + 1
				IF @TypeForMultiple = 1
					SET @PosVisite = @PosVisite + @NbPresencesMultiple
			END
			ELSE
				SET @looping=0
		END		
	END

	PRINT ''

	-- Analyse existing texts
	INSERT INTO #FolderTextesAnalyse (NoFolderTexte,NoFolderTexteType,NoMultiple, NoFolder, TexteTitle,DateStarted,DateFinished, Erronus , Status )
	SELECT NoFolderTexte,@NoFolderTexteType,NoMultiple,@NoFolder,TexteTitle,DateStarted,DateFinished,3,'Le texte est en trop (ClosingDate=' + dbo.AffTextDate(@FolderClosing) + ')' FROM FolderTextes WHERE NoFolder=@NoFolder AND NoFolderTexteType=@NoFolderTexteType AND (SELECT COUNT(*) FROM #FolderTextesAnalyse AS FTA WHERE FTA.NoFolderTexte=FolderTextes.NoFolderTexte)=0

PRINT '<-----------------------------------------------'

	FETCH NEXT FROM my_cursor INTO @NoFolder,@NoFolderTexteType
	END

	CLOSE my_cursor
	DEALLOCATE my_cursor

--DROP TABLE #FolderTextesAnalyse




-----------------------------
--Correct Error 1 with has no closing date
--BEGIN TRANSACTION
--delete from usersalerts where message like '%étape%'
--
--INSERT INTO FolderTextes (NoFolderTexteType,NoFolder,TexteTitle,DateStarted,Texte,TextePos,NoMultiple,IsTexte)
--SELECT NoFolderTexteType,NoFolder,TexteTitle,DateStarted,'',-1,NoMultiple,0 FROM #FolderTextesAnalyse WHERE  Erronus=1 and status LIKE '%closingdate=)%'
--
--INSERT INTO UsersAlerts(NoUser,AlertType,AlertData,AffDate,ExpDate,AlarmData,IsHidden,IsNew,Message)
--SELECT NoTRPTraitant,'OpenClientAccount',CAST(if1.NoClient AS varchar(MAX)),DATEADD(d,NbDaysDiff*-1,DateStarted),DATEADD(d,1,DateStarted),CAST(dbo.AffTextDate(DATEADD(d,NbDaysDiff*-1,DateStarted)) as varchar(10)) + ':00:00:' + CAST(if1.NoClient as varchar(max)) + ':'+ CAST(FTA.NoFolder as varchar(max)) + ':True',1,1,'Le rapport CSST d''étape ' + CAST(NoMultiple as varchar(max)) + ' pour le client ' + ic.Nom + ',' + ic.Prenom + ' du dossier #' + CAST(fta.nofolder as varchar(max)) + ' est dû le ' + dbo.afftextdate(datestarted) FROM #FolderTextesAnalyse AS FTA INNER JOIN infofolders as if1 on if1.nofolder=fta.nofolder inner join infoclients as ic on ic.noclient=if1.noclient inner join foldertextetypes as ftt on ftt.nofoldertextetype=fta.nofoldertextetype WHERE  Erronus=1 and status LIKE '%closingdate=)%'
--
--INSERT INTO FolderTexteAlerts(NoUserAlert,NoFolderTexte)
--SELECT (SELECT NoUserAlert FROM UsersAlerts WHERE Message=t.mes),(SELECT NoFolderTexte FROM FolderTextes as ft WHERE ft.NoFolder=t.nofolder and ft.nofoldertextetype=t.nofoldertextetype and ft.nomultiple = t.nomultiple) FROM (SELECT fta.NoFolderTexteType,NoMultiple,fta.NoFolder,'Le rapport CSST d''étape ' + CAST(NoMultiple as varchar(max)) + ' pour le client ' + ic.Nom + ',' + ic.Prenom + ' du dossier #' + CAST(fta.nofolder as varchar(max)) + ' est dû le ' + dbo.afftextdate(datestarted) as mes FROM #FolderTextesAnalyse AS FTA INNER JOIN infofolders as if1 on if1.nofolder=fta.nofolder inner join infoclients as ic on ic.noclient=if1.noclient inner join foldertextetypes as ftt on ftt.nofoldertextetype=fta.nofoldertextetype WHERE  Erronus=1 and status LIKE '%closingdate=)%') as t
--
----select * from usersalerts where message like '%étape%'
--
-------------------------------
----Correct Error 2
--UPDATE FolderTextes SET DateStarted=SUBSTRING(Status,CHARINDEX('(',Status)+1,10) FROM FolderTextes AS FT INNER JOIN #FolderTextesAnalyse AS FTA ON FTA.NoFolderTexte=FT.NoFolderTexte WHERE FTA.Erronus=2

--
-------------------------------
----Correct Error 3
--DELETE FROM FolderTextes WHERE NoFolderTexte IN (SELECT NoFolderTexte FROM #FolderTextesAnalyse WHERE Erronus=3)