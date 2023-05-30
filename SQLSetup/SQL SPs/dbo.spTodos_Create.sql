USE [TodoDb]
GO

/****** Object: SqlProcedure [dbo].[spTodos_Create] Script Date: 30-May-23 3:19:09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spTodos_Create]
	@task nvarchar(50),
	@assignedTo int
AS
BEGIN
	INSERT INTO dbo.ToDos (Task, AssignedTo)
	VALUES (@task, @assignedTo)

	SELECT *
	FROM ToDos
	WHERE Id = SCOPE_IDENTITY();
END
