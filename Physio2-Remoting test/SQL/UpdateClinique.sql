SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jonathan Boivin
-- Create date: 
-- Description:	
-- =============================================
CREATE TRIGGER TriggerUpdateClinique 
   ON  dbo.InfoClinique 
   AFTER INSERT,UPDATE
AS 
DECLARE @update nvarchar(MAX)
WHILE ((SELECT TOP 1 [Update] FROM Updates WHERE ([Update] LIKE N'UpdateClinique%')) = '')
SELECT TOP 1 @update= 'C:\ClinicaServer\Server ' + [Update] FROM Updates WHERE ([Update] LIKE N'UpdateClinique%')
BEGIN
IF @update <> ''
	EXEC xp_cmdshell @update;
END

GO
--DECLARE @filepath nvarchar(MAX)
