CREATE TABLE [dbo].[LocationActivityLogs]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [LocationId] UNIQUEIDENTIFIER NOT NULL, 
    [Timestamp] DATETIME2 NOT NULL, 
    [NumberOfPeople] INT NOT NULL, 
    CONSTRAINT [FK_LocationActivityLog_Location] FOREIGN KEY ([LocationId]) REFERENCES Location(Id)
)
