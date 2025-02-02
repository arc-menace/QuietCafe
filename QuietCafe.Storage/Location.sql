CREATE TABLE [dbo].[Location]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(100) NOT NULL, 
    [LastRollupCalculation] DATETIME2 NULL, 
    [NumberOfPeopleRollup] INT NOT NULL 
)
