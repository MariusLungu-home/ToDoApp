USE [TodoDb]
GO

/****** Object: SqlProcedure [dbo].[spTodos_GetOneAssigned] Script Date: 30-May-23 3:19:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spTodos_GetOneAssigned]
	@assignedTo int,
	@id int
AS
BEGIN
	SELECT Id, Task, AssignedTo, IsComplete
	FROM dbo.ToDos
	WHERE AssignedTo = @assignedTo 
		AND Id = @id
END
