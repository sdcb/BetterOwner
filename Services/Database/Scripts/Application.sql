BEGIN TRAN 

DROP TABLE IF EXISTS [TreasurePicture]
DROP TABLE IF EXISTS [Treasure]
DROP TABLE IF EXISTS [OAUser]

CREATE TABLE [OAUser]
(
	[UserId]   INT PRIMARY KEY NOT NULL, 
	[JobId]    NVARCHAR(10) NOT NULL,  
	[Sex]      TINYINT NOT NULL, 
	[Email]    NVARCHAR(60) NOT NULL, 
	[Phone]    NVARCHAR(50) NOT NULL, 
	[JoinTime] DATETIME2, 
	FOREIGN KEY     ([UserId]) REFERENCES   [User]([Id]), 
	INDEX    [IX_OAUser_JobId] NONCLUSTERED (   [JobId]  ASC), 
	INDEX    [IX_OAUser_Email] NONCLUSTERED (   [Email]  ASC), 
	INDEX    [IX_OAUser_Phone] NONCLUSTERED (   [Phone]  ASC), 
	INDEX [IX_OAUser_JoinTime] NONCLUSTERED ([JoinTime] DESC), 
)

CREATE TABLE [Treasure]
(
	[Id]		   INT PRIMARY KEY NOT NULL IDENTITY(1, 1), 
	[Title]		   NVARCHAR(50) NOT NULL, 
	[Price]		   DECIMAL(19, 2) NOT NULL, 
	[Description]  NVARCHAR(4000) NOT NULL, 
	[CreateTime]   DATETIME2 NOT NULL, 
	[UpdateTime]   DATETIME2 NOT NULL, 
	[CreateUserId] INT NOT NULL, 
	INDEX   [IX_Treasure_UpdateTime] CLUSTERED    ([UpdateTime] DESC), 
	FOREIGN KEY     ([CreateUserId]) REFERENCES   [User]([Id]), 
	INDEX [IX_Treasure_CreateUserId] NONCLUSTERED ([CreateUserId] ASC), 	
	INDEX              [PK_Treasure] NONCLUSTERED ([Id] ASC)
)

CREATE TABLE [TreasurePicture]
(
	[Id] UNIQUEIDENTIFIER ROWGUIDCOL PRIMARY KEY NOT NULL DEFAULT NEWSEQUENTIALID(),
	[TreasureId]  INT NOT NULL, 
	[FileName]	  NVARCHAR(120) NOT NULL, 
	[FileSize]    INT NOT NULL,
	[ContentType] NVARCHAR(120) NOT NULL,
	[FileStream]  VARBINARY(MAX) FILESTREAM NULL
)

COMMIT