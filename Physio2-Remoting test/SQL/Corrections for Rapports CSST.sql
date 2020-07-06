select * from usersalerts where nouser=6
Le rapport CSST d'étape #3 pour le client Joyal,Richard du dossier #20971 est dû le 2009/08/17

select nouseralert from (select nouseralert,cast( substring(nofolder,1,charindex(' ',nofolder)-1) as int) as nofolder from (SELECT NoUserAlert,SUBSTRING(Message, CHARINDEX('#',Message,CHARINDEX('#',Message)+1)+1,10) as nofolder FROM UsersAlerts as ua WHERE Message LIKE '%CSST d''étape%' AND (SELECT COUNT(*) FROM FolderTexteAlerts as FTA WHERE  FTA.NoUserAlert= ua.nouseralert)=0 and ua.alarmdata='') as t) as t2
select * from usersalerts where nouseralert in (select nouseralert from (select nouseralert,cast( substring(nofolder,1,charindex(' ',nofolder)-1) as int) as nofolder from (SELECT NoUserAlert,SUBSTRING(Message, CHARINDEX('#',Message,CHARINDEX('#',Message)+1)+1,10) as nofolder FROM UsersAlerts as ua WHERE Message LIKE '%CSST d''étape%' AND (SELECT COUNT(*) FROM FolderTexteAlerts as FTA WHERE  FTA.NoUserAlert= ua.nouseralert)=0 and ua.alarmdata='') as t) as t2)
DELETE from usersalerts where nouseralert in (select nouseralert from (select nouseralert,cast( substring(nofolder,1,charindex(' ',nofolder)-1) as int) as nofolder from (SELECT NoUserAlert,SUBSTRING(Message, CHARINDEX('#',Message,CHARINDEX('#',Message)+1)+1,10) as nofolder FROM UsersAlerts as ua WHERE Message LIKE '%CSST d''étape%' AND (SELECT COUNT(*) FROM FolderTexteAlerts as FTA WHERE  FTA.NoUserAlert= ua.nouseralert)=0 and ua.alarmdata='') as t) as t2)

Le rapport CSST d'étape #1 pour le client Robert,Guy du dossier #20031 est dû le 2009/08/18
Le rapport CSST d'étape #5 pour le client Duchesne,Sebastien du dossier #19636 est dû le 2009/08/19
Le rapport CSST d'étape #1 pour le client Ethier,Mélissa du dossier #20098 est dû le 2009/08/18

select * from foldertextes where nofolder=19050
select * from infofolders where nofolder=20098
select * from infovisites where nofolder=20098 order by dateheure
select * from foldertextetypes where nocodification=38

INSERT INTO FolderTextes (NoFolderTexteType,NoFolder,TexteTitle,DateStarted,Texte,TextePos,NoMultiple,IsTexte)
VALUES (87,20098,'Rapport CSST d''étape 1','2009/08/11','',-1,1,0)



BEGIN TRANSACTION
INSERT into usersalerts (NoUser,AlertType,AlertData,AffDate,ExpDate,AlarmData,IsHidden,IsNew,Message) 
select t2.* from (select [if].NoTRPTraitant,'OpenClientAccount' as AlertType,[if].NoClient,dateadd(d,ftt.nbdaysmultiple-ftt.nbdaysdiff,datestarted) as FTDate,dateadd(d,ftt.nbdaysmultiple-ftt.nbdaysdiff+1,datestarted) as ExpDate,CAST(dbo.AffTextDate(dateadd(d,ftt.nbdaysmultiple-ftt.nbdaysdiff,datestarted)) AS varchar(10)) + ':00:00:' + cast([if].noclient as varchar(max)) + ':' + cast([if].nofolder as varchar(max)) + ':True' as alarmdata,1 as ishidden,1 as isnew ,'Le rapport CSST d''étape #' + cast((ft.nomultiple+1) as varchar(max)) + ' pour le client ' + ic.Nom + ',' + ic.Prenom + ' du dossier #' + cast(ft.NoFolder as varchar(max)) + ' est dû le ' + CAST(dbo.AffTextDate(datestarted) as varchar(10)) as Title from foldertextes as ft inner join infofolders as [IF] on [if].nofolder=ft.nofolder inner join infoclients as ic on ic.noclient=[if].noclient  inner join foldertextetypes as ftt on ftt.nofoldertextetype=ft.nofoldertextetype  where nofoldertexte in (select max(nofoldertexte) from foldertextes as ft inner join infofolders as [IF] on [if].nofolder=ft.nofolder inner join foldertextetypes as ftt on ftt.nofoldertextetype=ft.nofoldertextetype where ftt.multiple=1 and [if].statutouvert=1 group by ft.nofolder)) as t2 where t2.ftdate<=getdate()
select * from usersalerts where message like '%étape #%' order by alertdata
select * from usersalerts where alertdata in (select alertdata from usersalerts where message like '%étape #%' group by alertdata having count(*)>1) order by alertdata
ROLLBACK TRANSACTION
delete from usersalerts where nouseralert =19347



insert into foldertextealerts (nouseralert,nofoldertexte)
select nouseralert,(SELECT TOP 1 NoFolderTexte FROM FolderTextes as ft where ft.nofolder=t2.nofolder and textetitle like '%CSST d''étape%') as nofoldertexte from (select nouseralert,cast( substring(nofolder,1,charindex(' ',nofolder)-1) as int) as nofolder from (SELECT NoUserAlert,SUBSTRING(Message, CHARINDEX('#',Message,CHARINDEX('#',Message)+1)+1,10) as nofolder FROM UsersAlerts as ua WHERE Message LIKE '%CSST d''étape%' AND (SELECT COUNT(*) FROM FolderTexteAlerts as FTA WHERE  FTA.NoUserAlert= ua.nouseralert)=0) as t) as t2

select max(nofolder) from infofolders



with alertData (NoUser,AlertType,AlertData,AffDate,ExpDate,AlarmData,IsHidden,IsNew,Message) AS (select t2.* from (select [if].NoTRPTraitant,'OpenClientAccount' as AlertType,[if].NoClient,dateadd(d,ftt.nbdaysmultiple-ftt.nbdaysdiff,datestarted) as FTDate,dateadd(d,1,datestarted) as ExpDate,CAST(dbo.AffTextDate(dateadd(d,ftt.nbdaysmultiple-ftt.nbdaysdiff,datestarted)) AS varchar(10)) + ':00:00:' + cast([if].noclient as varchar(max)) + ':' + cast([if].nofolder as varchar(max)) + ':True' as alarmdata,1 as ishidden,1 as isnew ,'Le rapport CSST d''étape ' + cast((ft.nomultiple+1) as varchar(max)) + ' pour le client ' + ic.Nom + ',' + ic.Prenom + ' du dossier #' + cast(ft.NoFolder as varchar(max)) + ' est dû le ' + CAST(dbo.AffTextDate(datestarted) AS varchar(10)) as Title from foldertextes as ft inner join infofolders as [IF] on [if].nofolder=ft.nofolder inner join infoclients as ic on ic.noclient=[if].noclient  inner join foldertextetypes as ftt on ftt.nofoldertextetype=ft.nofoldertextetype  where nofoldertexte in (select max(nofoldertexte) from foldertextes as ft inner join infofolders as [IF] on [if].nofolder=ft.nofolder inner join foldertextetypes as ftt on ftt.nofoldertextetype=ft.nofoldertextetype where ftt.multiple=1 and [if].statutouvert=1 group by ft.nofolder)) as t2 where t2.ftdate<=getdate())
SELECT * FROM alertdata
