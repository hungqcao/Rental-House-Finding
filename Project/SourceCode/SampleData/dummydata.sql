DELETE FROM [RentalHouseFinding].[dbo].[Users]
GO

DELETE FROM [RentalHouseFinding].[dbo].[Roles]
GO

INSERT INTO [RentalHouseFinding].[dbo].[Roles]
           ([Name]
           ,[Description]
           ,[IsDeleted])
     VALUES
           ('Admin','Description',0),('Moderator','Description',0),('User','Description',0)
GO

DELETE FROM [RentalHouseFinding].[dbo].[PostStatuses]

GO

INSERT INTO [RentalHouseFinding].[dbo].[PostStatuses] VALUES 
			('Actived','Description',0),('Pending','Description',0),('Violated','Description',0)

GO

DELETE FROM [RentalHouseFinding].[dbo].Categories

GO

INSERT INTO [RentalHouseFinding].[dbo].Categories VALUES 
			('Nha tro','Description',0),('Nha biet thu','Description',0),('Moi gioi','Description',0)

GO


DELETE FROM [RentalHouseFinding].[dbo].Districts

GO


DELETE FROM [RentalHouseFinding].[dbo].Provinces

GO

INSERT INTO [RentalHouseFinding].[dbo].[Provinces] VALUES 
			('Ha Noi',21.033333,105.85,0),('Quang Ninh',21.24325,107.19572,0)

GO


INSERT INTO [RentalHouseFinding].[dbo].[Districts] VALUES 
			('Ba Dinh',21.035852,105.826094,'1', 0),('Ha Dong',20.955835,105.756366,'1', 0),('Ha Long',20.971198,107.044807,'2', 0)

GO