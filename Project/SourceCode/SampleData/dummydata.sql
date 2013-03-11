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

DELETE FROM [RentalHouseFinding].[dbo].[AdvanceSearchScores]

GO

INSERT INTO [RentalHouseFinding].[dbo].[AdvanceSearchScores] VALUES 
			('Low',5),('Medium',10),('High',15)

GO

DELETE FROM [RentalHouseFinding].[dbo].[PostStatuses]

GO

INSERT INTO [RentalHouseFinding].[dbo].[PostStatuses] VALUES 
			('Actived','Description',0),('Pending','Description',0),('Violated','Description',0)

GO

DELETE FROM [RentalHouseFinding].[dbo].Categories

GO

INSERT INTO [RentalHouseFinding].[dbo].Categories VALUES 
			('Nha','Description',0),('Nha biet thu','Description',0),('Moi gioi','Description',0)

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

USE RentalHouseFinding ;
GO
IF OBJECT_ID ('dbo.V_PostFullInfo', 'V') IS NOT NULL
    DROP VIEW dbo.V_PostFullInfo ;
GO
CREATE VIEW V_PostFullInfo WITH SCHEMABINDING AS
SELECT P.Id, P.Description, P.NumberAddress, P.Street, P.Title, F.Direction, C.Email, C.Phone, C.Skype, C.Yahoo, P.CategoryId, P.DistrictId, D.ProvinceId
FROM dbo.Posts P 
	INNER JOIN dbo.Facilities F
		ON(P.Id = F.PostIdFacilities)
	INNER JOIN dbo.Contacts C
		ON(P.Id = C.PostIdContacts)
	INNER JOIN dbo.Districts D
		ON(P.DistrictId = D.Id) 
WHERE P.IsDeleted = 'false' AND D.IsDeleted = 'false'
WITH CHECK OPTION ;

GO

CREATE UNIQUE CLUSTERED INDEX UCI_PostId ON V_PostFullInfo(Id);

GO

USE RentalHouseFinding ;
GO
IF OBJECT_ID ('dbo.FullTextSearchPost', 'P') IS NOT NULL
    DROP PROCEDURE dbo.FullTextSearchPost ;
GO
CREATE PROCEDURE FullTextSearchPost @CategoryIdPass int = 0, @ProvinceIdPass int = 0, @DistrictIdPass int = 0, @Keyword nvarchar(100) = null
AS
IF(@Keyword IS NULL AND @DistrictIdPass != 0)
	BEGIN
		SELECT Id, Title FROM dbo.V_PostFullInfo S WHERE S.CategoryId = @CategoryIdPass AND S.ProvinceId = @ProvinceIdPass AND S.DistrictId = @DistrictIdPass 
	END
ELSE IF(@Keyword IS NULL AND @DistrictIdPass = 0)
	BEGIN
		SELECT Id, Title FROM dbo.V_PostFullInfo S WHERE S.CategoryId = @CategoryIdPass AND S.ProvinceId = @ProvinceIdPass
	END
ELSE IF(@Keyword IS NOT NULL AND @DistrictIdPass != 0)
	BEGIN
		SELECT FT_TBL.*, KEY_TBL.RANK
		FROM 
		(SELECT Id, Title FROM dbo.V_PostFullInfo S 
			WHERE S.CategoryId = @CategoryIdPass AND S.ProvinceId = @ProvinceIdPass AND S.DistrictId = @DistrictIdPass)
		AS FT_TBL 
			INNER JOIN FREETEXTTABLE(dbo.V_PostFullInfo,*, @Keyword) AS KEY_TBL
				ON FT_TBL.Id = KEY_TBL.[KEY]
		ORDER BY KEY_TBL.RANK DESC;
	END
ELSE IF(@Keyword IS NOT NULL AND @DistrictIdPass = 0)
	BEGIN
		SELECT FT_TBL.*, KEY_TBL.RANK
		FROM 
		(SELECT Id, Title FROM dbo.V_PostFullInfo S 
			WHERE S.CategoryId = @CategoryIdPass AND S.ProvinceId = @ProvinceIdPass)
		AS FT_TBL 
			INNER JOIN FREETEXTTABLE(dbo.V_PostFullInfo,*, @Keyword) AS KEY_TBL
				ON FT_TBL.Id = KEY_TBL.[KEY]
		ORDER BY KEY_TBL.RANK DESC;
	END
GO

EXEC FullTextSearchPost @CategoryIdPass = 1, @ProvinceIdPass = 1, @DistrictIdPass = 0, @KeyWord = 'post'

USE RentalHouseFinding ;
GO
IF OBJECT_ID ('dbo.V_PostFacilityInfo', 'V') IS NOT NULL
    DROP VIEW dbo.V_PostFacilityInfo ;
GO
CREATE VIEW V_PostFacilityInfo WITH SCHEMABINDING AS
SELECT P.Id, P.Area, P.Price, F.HasAirConditioner, F.HasBed, F.HasGarage, F.HasInternet, F.HasMotorParkingLot, F.HasSecurity, F.HasToilet, F.HasTVCable, F.HasWaterHeater, F.IsAllowCooking, F.IsStayWithOwner, P.CategoryId, P.DistrictId, D.ProvinceId, F.FacilityTemplateId
FROM dbo.Posts P 
	INNER JOIN dbo.Facilities F
		ON(P.Id = F.PostIdFacilities)
	INNER JOIN dbo.Contacts C
		ON(P.Id = C.PostIdContacts)
	INNER JOIN dbo.Districts D
		ON(P.DistrictId = D.Id) 
WHERE P.IsDeleted = 'false' AND D.IsDeleted = 'false'

GO


IF OBJECT_ID ('dbo.AdvancedSearchFacility', 'P') IS NOT NULL
    DROP PROCEDURE dbo.AdvancedSearchFacility ;
GO
CREATE PROCEDURE AdvancedSearchFacility 
	@CategoryIdPass int = 0, 
	@ProvinceIdPass int = 0, 
	@DistrictIdPass int = 0, 
	@AreaMax float = 0,
	@AreaMin float = 0,
	@PriceMax float = 0,
	@PriceMin float = 0,
	@HasAirConditionerScore int = 0,
	@HasBedScore int = 0,
	@HasGarageScore int = 0,
	@HasInternetScore int = 0,
	@HasMotorParkingLotScore int = 0,
	@HasSecurityScore int = 0,
	@HasToilet int = 0,
	@HasTVCableScore int = 0,
	@HasWaterHeaterScore int = 0,
	@IsAllowCookingScore int = 0,
	@IsStayWithOwnerScore int = 0
AS

SELECT T.Id, ((T.Column1 * @HasAirConditionerScore) + 
				(T.Column2 * @HasBedScore) +
				(T.Column3 * @HasGarageScore) +
				(T.Column4 * @HasInternetScore) +
				(T.Column5 * @HasMotorParkingLotScore) + 
				(T.Column6 * @HasSecurityScore) +
				(T.Column7 * @HasToilet) +
				(T.Column8 * @HasTVCableScore) +
				(T.Column9 * @HasWaterHeaterScore) +
				(T.Column10 * @IsAllowCookingScore) +
				(T.Column11 * @IsStayWithOwnerScore)) AS Score
 FROM dbo.FacilityTemplates T
INNER JOIN
(SELECT F.Id, F.FacilityTemplateId FROM dbo.V_PostFacilityInfo F 
WHERE
	F.CategoryId = @CategoryIdPass AND
	F.DistrictId = @DistrictIdPass AND
	F.ProvinceId = @ProvinceIdPass AND
	F.Area <= @AreaMax AND F.Area >= @AreaMin AND
	F.Price <= @PriceMax AND F.Price >= @PriceMin) S
ON T.Id = S.FacilityTemplateId
GO

EXEC AdvancedSearchFacility 
	@CategoryIdPass = 1, 
	@ProvinceIdPass = 1, 
	@DistrictIdPass = 1, 
	@AreaMax = 20000, 
	@AreaMin = 0, 
	@PriceMax = 3000000, 
	@PriceMin = 0,
	@HasAirConditionerScore = 5,
	@HasBedScore = 5,
	@HasGarageScore = 5,
	@HasInternetScore = 5,
	@HasMotorParkingLotScore = 5,
	@HasSecurityScore = 5,
	@HasToilet = 5,
	@HasTVCableScore = 5,
	@HasWaterHeaterScore = 5,
	@IsAllowCookingScore = 5,
	@IsStayWithOwnerScore = 0