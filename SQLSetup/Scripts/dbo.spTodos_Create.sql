USE [TodoDb]
GO

/****** Object: SqlProcedure [dbo].[spTodos_Create] Script Date: 31-May-23 8:42:46 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spTodos_Create]
	@task nvarchar(50),
	@assignedTo int
AS
BEGIN
	INSERT INTO dbo.ToDos (Task, AssignedTo, IsComplete, IsArchived)
	VALUES (@task, @assignedTo, 0, 0)

	SELECT *
	FROM ToDos
	WHERE Id = SCOPE_IDENTITY();
END
