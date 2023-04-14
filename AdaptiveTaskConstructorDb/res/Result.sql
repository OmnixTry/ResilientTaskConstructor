﻿CREATE TABLE [res].[Result]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Date] DATETIME NOT NULL DEFAULT(GETDATE()),
	[TestId] INT NOT NULL FOREIGN KEY REFERENCES [test].[Test](Id),
	[StudentId] NVARCHAR(450) NOT NULL FOREIGN KEY REFERENCES [dbo].[AspNetUsers](Id),
	[Score] INT NULL,
)
