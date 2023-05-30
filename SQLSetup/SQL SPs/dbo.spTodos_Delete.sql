USE [TodoDb]
GO

/****** Object: SqlProcedure [dbo].[spTodos_Delete] Script Date: 30-May-23 3:19:15 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spTodos_Delete]
	@assignedTo int,
	@todoId int
AS
BEGIN
	DELETE FROM dbo.ToDos 
	WHERE Id = @todoId 
		AND AssignedTo = @assignedTo
END
