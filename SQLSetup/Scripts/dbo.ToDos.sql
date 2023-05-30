USE [TodoDb]
GO

/****** Object: Table [dbo].[ToDos] Script Date: 30-May-23 3:20:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ToDos] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Task]       NVARCHAR (50) NOT NULL,
    [AssignedTo] INT           NOT NULL,
    [IsComplete] BIT           NOT NULL,
    [IsArchived] BIT           NOT NULL
);


