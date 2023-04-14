﻿CREATE TABLE [test].[TaskOption]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[TestTaskId] INT NOT NULL FOREIGN KEY REFERENCES [test].[TestTask](Id),
	[Value] NVARCHAR(MAX) NOT NULL,
	[Correct] BIT NOT NULL,
)