﻿CREATE TABLE [grp].[GroupStudent]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[GroupId] INT NOT NULL FOREIGN KEY REFERENCES [grp].[Group](Id) ON DELETE CASCADE,
	[StudentId] NVARCHAR(450) NOT NULL,-- FOREIGN KEY REFERENCES [dbo].[AspNetUsers](Id),

	CONSTRAINT UC_GroupStudent UNIQUE ([GroupId], [StudentId]) 
)
