﻿CREATE TABLE [res].[ResultTask]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[TestTaskId] INT NOT NULL FOREIGN KEY REFERENCES [test].[TestTask](Id) ON DELETE CASCADE,
	[ResultId] INT NOT NULL FOREIGN KEY REFERENCES [res].[Result](Id) ON DELETE CASCADE,
	[Score] INT NOT NULL,
	[Hash] TINYINT NOT NULL default(3),
)
