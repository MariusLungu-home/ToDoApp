USE [TodoDb]
GO

/****** Object: SqlProcedure [dbo].[spTodos_Complete] Script Date: 30-May-23 3:18:48 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spTodos_Complete]
	@assignedTo int,
	@todoId int
AS
BEGIN
	UPDATE dbo.ToDos 
	SET IsComplete = 1
	WHERE Id = @todoId 
		AND AssignedTo = @assignedTo
END
