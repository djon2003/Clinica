

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[AbsencesRaisonsTriggerIU] 
   ON  [dbo].[AbsencesRaisons]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[AgendaTriggerIU] 
   ON  [dbo].[Agenda]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[CodificationsDossiersTriggerIU] 
   ON  [dbo].[CodificationsDossiers]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[CodificationsPayesTriggerIU] 
   ON  [dbo].[CodificationsPayes]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[CommDeATriggerIU] 
   ON  [dbo].[CommDeA]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[CommDeAKPTriggerIU] 
   ON  [dbo].[CommDeAKP]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[CommunicationsTriggerIU] 
   ON  [dbo].[Communications]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[CommunicationsKPTriggerIU] 
   ON  [dbo].[CommunicationsKP]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[ECategorieTriggerIU] 
   ON  [dbo].[ECategorie]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[EmployeursTriggerIU] 
   ON  [dbo].[Employeurs]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[EquipementsTriggerIU] 
   ON  [dbo].[Equipements]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[FacturesTriggerIU] 
   ON  [dbo].[Factures]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[HorairesTriggerIU] 
   ON  [dbo].[Horaires]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[InfoClientsTriggerIU] 
   ON  [dbo].[InfoClients]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[InfoCliniqueTriggerIU] 
   ON  [dbo].[InfoClinique]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[InfoFoldersTriggerIU] 
   ON  [dbo].[InfoFolders]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[InfoLogicielDiversTriggerIU] 
   ON  [dbo].[InfoLogicielDivers]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[InfoVisitesTriggerIU] 
   ON  [dbo].[InfoVisites]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[KeyPeopleTriggerIU] 
   ON  [dbo].[KeyPeople]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[KPCategorieTriggerIU] 
   ON  [dbo].[KPCategorie]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[KPSearchListTriggerIU] 
   ON  [dbo].[KPSearchList]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[ListeActionTriggerIU] 
   ON  [dbo].[ListeAction]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[ListeAttenteTriggerIU] 
   ON  [dbo].[ListeAttente]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[ListeAttenteTriggerIU] 
   ON  [dbo].[ListeAttente]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[ListeMontantRembourseTriggerIU] 
   ON  [dbo].[ListeMontantRembourse]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[ListeRepetitionTriggerIU] 
   ON  [dbo].[ListeRepetition]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[ListeStatutTriggerIU] 
   ON  [dbo].[ListeStatut]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[ListeTypeEmployeTriggerIU] 
   ON  [dbo].[ListeTypeEmploye]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[ListeTypeItemTriggerIU] 
   ON  [dbo].[ListeTypeItem]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[MetiersTriggerIU] 
   ON  [dbo].[Metiers]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[NotesTitlesTriggerIU] 
   ON  [dbo].[NotesTitles]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[PayeCategorieTriggerIU] 
   ON  [dbo].[PayeCategorie]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[PayesUtilisateursTriggerIU] 
   ON  [dbo].[PayesUtilisateurs]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[PretsTriggerIU] 
   ON  [dbo].[Prets]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[RapportTypesTriggerIU] 
   ON  [dbo].[RapportTypes]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[SearchListTriggerIU] 
   ON  [dbo].[SearchList]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[SettingsFolderTriggerIU] 
   ON  [dbo].[SettingsFolder]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[SettingsUserTriggerIU] 
   ON  [dbo].[SettingsUser]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[SiteLesionTriggerIU] 
   ON  [dbo].[SiteLesion]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[StatFacturesTriggerIU] 
   ON  [dbo].[StatFactures]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[StatFoldersTriggerIU] 
   ON  [dbo].[StatFolders]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[StatPaiementsTriggerIU] 
   ON  [dbo].[StatPaiements]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[StatVisitesTriggerIU] 
   ON  [dbo].[StatVisites]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[TitresTriggerIU] 
   ON  [dbo].[Titres]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[TypeUtilisateurTriggerIU] 
   ON  [dbo].[TypeUtilisateur]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[UtilisateursTriggerIU] 
   ON  [dbo].[Utilisateurs]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[VentesTriggerIU] 
   ON  [dbo].[Ventes]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[VillesTriggerIU] 
   ON  [dbo].[Villes]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
ALTER TRIGGER [dbo].[WorkHoursTriggerIU] 
   ON  [dbo].[WorkHours]
   AFTER INSERT,UPDATE
AS
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn = (SELECT CONVERT(bigint,COLUMNS_UPDATED()))
	IF @sqrtForOneColumn < 0
		SET @sqrtForOneColumn = @sqrtForOneColumn * -1
	SET @sqrtForOneColumn=(SELECT (LOG(@sqrtForOneColumn)/LOG(2)))
	SET @onlyOneColumnUpdated = 0
	IF CONVERT(bigint,@sqrtForOneColumn) = @sqrtForOneColumn
		SET @onlyOneColumnUpdated = 1

	--If the column NoTrigger has not been modified or not alone, then send
	--synchronisation message
	IF ((NOT UPDATE(NoTrigger) AND @onlyOneColumnUpdated=1) OR @onlyOneColumnUpdated=0)
	BEGIN
		DECLARE @Path varchar(254)
		SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
		DECLARE @MyNoTrigger int
		SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM INSERTED)
		SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
		EXEC xp_cmdshell @Path
	END
END
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

