USE [TodoDb]
GO

/****** Object: SqlProcedure [dbo].[spTodos_GetAllAssigned] Script Date: 30-May-23 3:19:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP PROCEDURE [dbo].[spTodos_GetAllAssigned];


GO

CREATE PROCEDURE [dbo].[spTodos_GetAllAssigned]
	@assignedTo int
AS
BEGIN
	SELECT Id, Task, AssignedTo, IsComplete
	FROM dbo.ToDos
	WHERE AssignedTo = @assignedTo
END
