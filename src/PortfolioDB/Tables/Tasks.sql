﻿CREATE TABLE [dbo].[Tasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Description] VARCHAR(256) NOT NULL,
	[CategoryId] INT,
	[CurrentStatus] VARCHAR(256),
	[DueOn] DATETIME,
	[IsCompleted] BIT,
	[CreatedAt] DATETIME NOT NULL DEFAULT GETUTCDATE(),
	[UpdatedAt] DATETIME NOT NULL DEFAULT GETUTCDATE(), 
    [Version] ROWVERSION NOT NULL, 
    CONSTRAINT [FK_Tasks_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id]), 
    CONSTRAINT [FK_Tasks_Statuses] FOREIGN KEY ([CurrentStatus]) REFERENCES [Statuses]([Id])
)
GO

CREATE INDEX [IX_Tasks_IsCompleted] ON [dbo].[Tasks] ([IsCompleted])
GO

CREATE INDEX [IX_Tasks_CurrentStatus] ON [dbo].[Tasks] ([CurrentStatus])
GO