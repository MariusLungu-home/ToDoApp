USE [TodoDb]
GO

/****** Object: SqlProcedure [dbo].[spTodos_Update] Script Date: 30-May-23 3:19:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spTodos_Update]
	@task nvarchar(50),
	@assignedTo int,
	@todoId int
AS
BEGIN
	UPDATE dbo.ToDos 
	SET Task = @task
	WHERE Id = @todoId 
		AND AssignedTo = @assignedTo

END
