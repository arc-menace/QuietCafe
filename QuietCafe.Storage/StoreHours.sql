CREATE TABLE [dbo].[StoreHours]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [LocationId] UNIQUEIDENTIFIER NOT NULL,
	[DayOfWeek] SMALLINT NOT NULL, 
    [IsOpen24Hours] BIT NOT NULL DEFAULT 0, 
    [OpenTime] TIME NULL, 
    [CloseTime] TIME NULL, 
    CONSTRAINT [FK_StoreHours_Location] FOREIGN KEY ([LocationId]) REFERENCES Location(Id)
)
