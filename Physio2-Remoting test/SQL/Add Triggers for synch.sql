-- ================================================
-- Template generated from Template Explorer using:
-- Create Trigger (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- See additional Create Trigger templates for more
-- examples of different Trigger statements.
--
-- This block of comments will not be included in
-- the definition of the function.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE TRIGGER dbo.<Table_Name, sysname, Table_Name>TriggerD 
   ON  dbo.<Table_Name, sysname, Table_Name>
   AFTER DELETE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @Path varchar(254)
	SET @Path=(SELECT TOP 1 TriggerSoftPath FROM dbo.InfoLogicielDivers)
	DECLARE @MyNoTrigger int
	SET @MyNoTrigger=(SELECT TOP 1 NoTrigger FROM DELETED)
	SET @Path=(SELECT @Path + ' ' + CONVERT(varchar,@MyNoTrigger))
	EXEC xp_cmdshell @Path

END
GO
USE [Clinica]
GO
/****** Object:  Trigger [dbo].[AgendaTriggerIU]    Script Date: 08/30/2006 16:16:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	
-- =============================================
CREATE TRIGGER [dbo].[<Table_Name, sysname, Table_Name>TriggerIU] 
   ON  [dbo].<Table_Name, sysname, Table_Name>
   AFTER INSERT,UPDATE
AS 
BEGIN
	--Determine if only the only column has been modified
	--@onlyOneColumnUpdated = 0 if not, otherwise @onlyOneColumnUpdated = 1
	DECLARE @sqrtForOneColumn float
	DECLARE @onlyOneColumnUpdated bit
	SET @sqrtForOneColumn=(SELECT SQRT(CONVERT(int,COLUMNS_UPDATED())))
	SET @onlyOneColumnUpdated = 0
	IF POWER(CONVERT(int,COLUMNS_UPDATED()),0.5) = @sqrtForOneColumn
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



