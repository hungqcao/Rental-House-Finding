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
			(N'Mức độ quan tâm',0),(N'Thấp',5),(N'Vừa',10),(N'Cao',15)

GO

DELETE FROM [RentalHouseFinding].[dbo].[PostStatuses]

GO

INSERT INTO [RentalHouseFinding].[dbo].[PostStatuses] VALUES 
			(N'Đã kích hoạt','Description',0),('Pending','Description',0),('Violated','Description',0),('Expride','Description',0)

GO

DELETE FROM [RentalHouseFinding].[dbo].Categories

GO

INSERT INTO [RentalHouseFinding].[dbo].Categories VALUES 
			(N'Phòng trọ','Description',0),(N'Nhà ở riêng','Description',0),(N'Nhượng phòng','Description',0),(N'Ở ghép','Description',0)

GO


DELETE FROM [RentalHouseFinding].[dbo].Districts

GO


DELETE FROM [RentalHouseFinding].[dbo].Provinces

GO

INSERT INTO Provinces (Name, Lat, Lon, IsDeleted) VALUES (N'Hải Phòng', 20.861361, 106.679802, 0);
INSERT INTO Provinces (Name, Lat, Lon, IsDeleted) VALUES (N'Hà Nội', 21.0333333, 105.85000000000002, 0);
INSERT INTO Provinces (Name, Lat, Lon, IsDeleted) VALUES (N'Hồ Chí Minh', 10.8230989, 106.6296638, 0);


GO

INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Dương Kinh', 20.7784431, 106.71756690000007, 1, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Hồng Bàng', 20.8747238, 106.64976230000002, 1, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Ngô Quyền', 20.8578805, 106.70282500000008, 1, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Hải An', 20.8147445, 106.75884940000003, 1, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Đồ Sơn', 20.7223607, 106.75884940000003, 1, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Lê Chân', 20.8397231, 106.682188, 1, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Kiến An', 20.8097533, 106.63502500000004, 1, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'An Dương', 20.8797382, 106.5701927, 1, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'An Lão', 20.8231168, 106.55840710000007, 1, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Tiên Lãng', 20.6925142, 106.59376580000003, 1, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Kiến Thụy', 20.7317372, 106.66450069999996, 1, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Thủy Nguyên', 20.9524218, 106.65270989999999, 1, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Vĩnh Bảo', 20.6936727, 106.46414589999995, 1, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'đảo Cát Hải', 20.8092672, 106.8709404, 1, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'đảo Bạch Long Vĩ', 20.1368904, 107.7310526, 1, 0);

INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Ba Đình', 21.0358515, 105.82609430000002, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Hoàn Kiếm', 21.0272662, 105.85545130000003, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Tây Hồ', 21.0738016, 105.82315879999999, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Long Biên', 21.0386761, 105.88774940000008, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Cầu Giấy', 21.0423691, 105.78793710000002, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Đống Đa', 21.0134822, 105.82694149999998, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Hai Bà Trưng', 21.0040872, 105.85545130000003, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Hoàng Mai', 20.9713806, 105.86425929999996, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Thanh Xuân', 20.9937774, 105.81141750000006, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Hà Đông', 20.955835, 105.75636580000003, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Đan Phượng', 21.1050845, 105.67644610000002, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Ba Vì', 21.1208466, 105.38338079999994, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Đông Anh', 21.1231639, 105.85251540000002, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Hoài Đức', 21.0332922, 105.69991219999997, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Gia Lâm', 21.0319389, 105.95823719999999, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Mỹ Đức', 20.6727806, 105.71164640000006, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Phúc Thọ', 21.1139044, 105.58261229999994, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Phú Xuyên', 20.7352032, 105.91124239999999, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Mê Linh', 21.182938, 105.71164640000006, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Quốc Oai', 20.9725044, 105.60606610000002, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Sóc Sơn', 21.2622565, 105.85251540000002, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Thạch Thất', 21.0044411, 105.51226939999992, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Thanh Oai', 20.8643542, 105.77032869999994, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Thường Tín', 20.8554997, 105.86425929999996, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Thanh Trì', 20.9272518, 105.84077219999995, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Từ Liêm', 21.0520539, 105.74685350000004, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Ứng Hòa', 20.7103817, 105.80554710000001, 2, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Quận 2', 48.8691667, 2.340555600000016, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Quận 1', 48.86, 2.3411111000000346, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Quận 3', 48.8638194, 2.3616583000000446, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Quận 5', 48.8472222, 2.3444443999999294, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Quận 4', 48.8555556, 2.3555556000000024, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Quận 6', 48.8505306, 2.3322332999999844, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Quận 7', 48.8591667, 2.312777800000049, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Quận 8', 48.8741667, 2.311111100000062, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Quận 9', 48.8782806, 2.336966699999948, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Quận 10', 48.8755083, 2.357852800000046, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Quận 11', 48.8586778, 2.3793055999999524, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Quận 12', 48.8408083, 2.388183300000037, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Thủ Đức', 10.8494094, 106.75370550000002, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Tân Phú', 10.7988311, 106.62323590000005, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Phú Nhuận', 10.4516036, 106.04917999999998, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Bình Thạnh', 10.8105831, 106.70914219999997, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Tân Bình', 10.8084306, 106.64681480000002, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Gò Vấp', 10.8398228, 106.6703963, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Bình Tân', 10.7623139, 106.59376580000003, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Bình Chánh', 10.7430983, 106.54662209999992, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Cần Giờ', 10.5083266, 106.86350040000002, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Củ Chi', 10.9959552, 106.51127120000001, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Hóc Môn', 10.8931299, 106.59376580000003, 3, 0);
INSERT INTO Districts (Name, Lat, Lon, ProvinceId, IsDeleted) VALUES (N'Nhà Bè', 10.6687906, 106.71167000000003, 3, 0);

GO

DELETE FROM [RentalHouseFinding].[dbo].BadWords

GO


DELETE FROM [RentalHouseFinding].[dbo].BadWordTypes

GO


INSERT INTO [RentalHouseFinding].[dbo].BadWordTypes VALUES 
			('Chinh tri'),('Tinh duc'),('Tu long')

GO

INSERT INTO [RentalHouseFinding].[dbo].BadWords VALUES 
			('Fuck', 1),('Ass', 2), ('Hung Vuong', 1)

GO



DELETE FROM [RentalHouseFinding].[dbo].Locations

GO


INSERT INTO [RentalHouseFinding].[dbo].Locations VALUES 
			('Cho dong xuan', '1',21.035852,105.826094,'FALSE'),('Super house', '1',21.035852,105.826094,'FALSE'),('Sieu thi nguyen kim', '1',21.035852,105.826094,'FALSE')

GO

DELETE FROM [RentalHouseFinding].[dbo].ConfigurationRHFs

GO
INSERT INTO [RentalHouseFinding].[dbo].ConfigurationRHFs VALUES 
			('DescriptionColumnScore','10'),('TitleColumnScore','10'),('StreetColumnScore','10'),('NearbyColumnScore','10'),('NumberAddressColumnScore','10'),('DirectionColumnScore','10'),('NoneOfInformationText','N/A'),('ExpiredDate','2'),('ExpiredDateAfterRenew','15')

GO

DELETE FROM [RentalHouseFinding].[dbo].EmailTemplates

GO
INSERT INTO [RentalHouseFinding].[dbo].EmailTemplates VALUES 
			('YouReceiveQuestion',N'Bạn vừa nhận được 1 câu hỏi từ {0} <br/>Tiêu đề: {1} <br/>Nội dung: {2} <br/>Về bài viết tiêu đề: {3} <br/>Cảm ơn bạn!'),
			('YouReceiveMessage',N'Bạn vừa nhận được 1 câu trả lời từ {0} <br/>Nội dung: {1} <br/>Về câu hỏi tiêu đề: {2} <br/>Nội dung câu hỏi: {3} <br/>Cảm ơn bạn!'),
			('ReceiveForgotPassword',N'Chúng tôi nhận được yêu cầu tìm lại mật khẩu của tài khoản {0} vào lúc {1} <br/> Mật khẩu của bạn là {2} <br/> Xin cảm ơn!'),
			('SubjectReceiveForgotPassword',N'Tìm lại mật khẩu'),
			('Welcome',N'Chào mừng bạn đến với HouseFinding!<br/>Thông tin tài khoản:<br/>-Tên tài khoản:{0}<br/>-Mật khẩu:{1}<br/>'),
			('SubjectWelcome',N'Chào mừng bạn!'),
			('WelcomeOpenId',N'Chào mừng bạn đến với HouseFinding!<br/>'),
			('SubjectWelcomeOpenId',N'Welcome!')

GO

USE RentalHouseFinding ;
GO
IF OBJECT_ID ('dbo.V_PostFullInfo', 'V') IS NOT NULL
    DROP VIEW dbo.V_PostFullInfo ;
GO
CREATE VIEW V_PostFullInfo WITH SCHEMABINDING AS
SELECT P.Id, P.ExpiredDate, P.Description, P.NumberAddress, P.Street, P.Title, C.Email, C.Phone, C.Skype, C.Yahoo, P.CategoryId, P.DistrictId, D.ProvinceId, P.NearbyPlace
FROM dbo.Posts P 
	INNER JOIN dbo.Facilities F
		ON(P.Id = F.PostIdFacilities)
	INNER JOIN dbo.Contacts C
		ON(P.Id = C.PostIdContacts)
	INNER JOIN dbo.Districts D
		ON(P.DistrictId = D.Id) 
WHERE P.IsDeleted = 'false' AND D.IsDeleted = 'false' AND P.StatusId = 1
WITH CHECK OPTION ;

GO
CREATE UNIQUE CLUSTERED INDEX UCI_PostId ON V_PostFullInfo(Id);

GO
--------------For change status post when expride.
USE RentalHouseFinding ;
GO
IF OBJECT_ID ('dbo.ChangeStatusPostWhenExpride', 'P') IS NOT NULL
    DROP PROCEDURE dbo.ChangeStatusPostWhenExpride;
    
USE RentalHouseFinding;
GO
CREATE PROCEDURE ChangeStatusPostWhenExpride
    @statusId int = 0
AS 

    UPDATE Posts 
    SET
	StatusId = @statusId
	WHERE 
	Posts.Id 
	In 
	(SELECT Posts.Id 
		FROM Posts
		WHERE Posts.ExpiredDate <=  Convert(datetime, Convert(int, GetDate())) And Posts.IsDeleted = 0
	)
GO

DELETE FROM [RentalHouseFinding].[dbo].Users

GO

INSERT INTO [RentalHouseFinding].[dbo].Users VALUES
			('admin','25d55ad283aa400af464c76d713c07ad','admin@admin.ad',null,'Ad Handsome',GETDATE(),GETDATE(),0,null,1)
-----------
GO

DELETE FROM [RentalHouseFinding].[dbo].Locations
GO
INSERT INTO [RentalHouseFinding].[dbo].Locations VALUES
			--Cầu giấy--
			(N'Viện Huyết Học Truyền Máu Trung Ương',20,null,null,0),
			(N'Bệnh viện 19 - 8',20,null,null,0),
			(N'Bệnh Viện E Trung Ương',20,null,null,0),
			(N'Bệnh viện đa khoa y học cổ truyền',20,null,null,0),
			(N'Bệnh viện Quốc tế Hoa Kỳ',20,null,null,0),
			(N'Trường Đại học Thương Mại',20,null,null,0),
			(N'Đại học Công nghệ - Đại học Quốc Gia Hà Nội',20,null,null,0),
			(N'Đại học FPT',20,null,null,0),
			(N'Đại học Sư phạm Hà Nội',20,null,null,0),
			(N'Đại học Ngoại ngữ - Đại học Quốc gia Hà Nội',20,null,null,0),
			----- Quận Hoàng Mai
			(N'Bệnh viện K - Cơ sở Tam Hiệp',23,null,null,0),
			(N'Bệnh Viện Bưu Điện',23,null,null,0),
			(N'Viện y học Không Quân',23,null,null,0),
			(N'Chợ Trương Định',23,null,null,0),
			(N'Metro Hoàng Mai',23,null,null,0),
			(N'Siêu thị Fivimart Định Công',23,null,null,0),
			(N'Siêu thị Trung Thành',23,null,null,0),
			(N'Sân Định Công',23,null,null,0),
			(N'Sân bóng Quân Đội Hoàng Mai',23,null,null,0),
			(N'Sân Bóng Vĩnh Hưng',23,null,null,0),
			-----II. Quận Long Biên
			(N'Bệnh Viện Đa Khoa Đức Giang',19,null,null,0),
			(N'Bệnh viện đường sắt',19,null,null,0),
			(N'BigC Long Biên',19,null,null,0),
			(N'Savico Mega Mall',19,null,null,0),
			(N'Trung tâm mua sắm trực tuyến EnterShopping',19,null,null,0),
			(N'Fivimart Nguyễn Văn Cừ',19,null,null,0),
			(N'Công ty TNHH Sơn Long - Siêu thị Mart',19,null,null,0),
			(N'Platinum Cineplex - Long Biên',19,null,null,0),
			(N'Công ty TNHH Hựu Hưng',19,null,null,0),
			(N'Hải Huyền - Bia chai Hà Nội',19,null,null,0),
			-----III. Quận Ba Đình
			(N'Bệnh viện Đa khoa Xanh Pôn',16,null,null,0),
			(N'Bệnh viện 354',16,null,null,0),
			(N'Bệnh viện Y học cổ truyền Nam Á',16,null,null,0),
			(N'Bệnh viện quốc tế Thái Lan',16,null,null,0),
			(N'Trung tâm chiếu phim Quốc Gia',16,null,null,0),
			(N'Rạp Ngọc Khánh',16,null,null,0),
			(N'Rạp Đặng Dung',16,null,null,0),
			(N'Siêu thị Bibomart Sơn Tây',16,null,null,0),
			(N'Vietlien Mart',16,null,null,0),
			(N'Fivimart Hoàng Hoa Thám',16,null,null,0),
			---------IV. Ba Vì
			(N'Bệnh viện Đa khoa Ba Vì',27,null,null,0),
			(N'Khu du lịch Đầm Long',27,null,null,0),
			(N'Tản Đà Resort',27,null,null,0),
			(N'Du lịch hồ Suối Hai',27,null,null,0),
			(N'Khu di tích K9',27,null,null,0),
			(N'Du lịch Ao Vua',27,null,null,0),
			(N'Thiên Sơn - Suối Ngà',27,null,null,0),
			(N'Vườn Quốc Gia Ba Vì',27,null,null,0),
			(N'Khu du lịch Long Việt',27,null,null,0),
			(N'Trang trại đồng quê Ba Vì',27,null,null,0),
			-----V. Hai Bà Trưng

			(N'Bệnh Viện Quân đội 108',22,null,null,0),
			(N'Bệnh viện Mắt Sài Gòn - Hà Nội',22,null,null,0),
			(N'Bệnh viện Bạch Mai',22,null,null,0),
			(N'Bệnh viện Đa khoa Hà Nội',22,null,null,0),
			(N'Bệnh viện Hữu nghị Việt Nam - Cu Ba',22,null,null,0),
			(N'Siêu thị BiBoMart Trần Duy Hưng',22,null,null,0),
			(N'Siêu thị Bibomart Bạch Mai',22,null,null,0),
			(N'Vincom City Towers',22,null,null,0),
			(N'Megastar Media Cineplex',22,null,null,0),
			(N'Rạp Sinh viên',22,null,null,0),
			-------------VII. Hoàn Kiếm
			(N'Đại học Dược Hà Nội',17,null,null,0),
			(N'Đại học Mỹ thuật Việt Nam',17,null,null,0),
			(N'Fivimart Lý Thái Tổ',17,null,null,0),
			(N'Fivimart - Trần Quang Khải',17,null,null,0),
			(N'HaproMart - Hàng Đường',17,null,null,0),
			(N'TutiCare Center- Hệ thống Siêu thị Mẹ & bé số 1 Việt Nam',17,null,null,0),
			(N'Siêu thị Cmart',17,null,null,0),
			(N'HaproMart - Cầu Gỗ',17,null,null,0),
			(N'Rạp Kim Đồng',17,null,null,0),
			(N'Hàng Bài - Hoàn Kiếm - Hà Nội',17,null,null,0),
			(N'Rạp Lý Nam Đế',17,null,null,0),
			-----VIII. Gia Lâm
			(N'Hapromart Trâu Quỳ',30,null,null,0),
			(N'Khu Vui Chơi Giải Trí Phương Hiền',30,null,null,0),
			(N'Đình Phú Thị',30,null,null,0),
			(N'Sân bay Gia Lâm',30,null,null,0),
			(N'Bến xe Gia Lâm',30,null,null,0),
			(N'Chợ Phú Thụy',30,null,null,0),
			(N'Chợ Công Đình',30,null,null,0),
			(N'Chợ Vân',30,null,null,0),
			(N'Chợ Trâu Quỳ',30,null,null,0),
			(N'Công viên Gia Lâm',30,null,null,0),
			---------IX. Hà Đông
			(N'Trung Tâm Mua Sắm Nguyễn Kim Hà Đông',25,null,null,0),
			(N'MeLinh Plaza Hà Đông',25,null,null,0),
			(N'siêu thị OceanMart tại Hà Đông',25,null,null,0),
			(N'Hiway Supercenter Hà Đông',25,null,null,0),
			(N'Lotte Hà Đông',25,null,null,0),
			(N'Đại học Thành Tây',25,null,null,0),
			(N'Trường Đại học Thành Tây',25,null,null,0),
			(N'trung Tâm Apollo Games',25,null,null,0),
			(N'Chợ Hà Đông',25,null,null,0),
			(N'Thiên Đường Bảo Sơn',25,null,null,0),
			-------X. Hoài Đức
			(N'chợ cổ Giang Xá',29,null,null,0),
			(N'Làng mỹ nghệ Sơn Đồng',29,null,null,0),
			(N'Làng nghề nhiếp ảnh: Lai Xá - Kim Chung',29,null,null,0),
			(N'Làng nghề tạc tượng: Sơn Đồng',29,null,null,0),
			(N'Làng nghề dệt: La Phù',29,null,null,0),
			(N'Làng nghề nông sản: Dương Liễu',29,null,null,0),
			(N'Trường Văn Hoá Thể Thao',29,null,null,0),
			(N'Trung tâm khiêu vũ thể thao quốc tế 14 Trịnh Hoài Đức ',29,null,null,0),
			(N'Trung tâm TDTT Hoài Đức',29,null,null,0),
			(N'Nhà thi đấu Trịnh Hoài Đức',29,null,null,0),
			------XI. Đan Phượng
			(N'Nhà thi đấu huyện Đan Phượng',26,null,null,0),
			(N'Đền ông Hoàng Mười.',26,null,null,0),
			(N'Khu Liên Hiệp Thể Thao Huyện Đan Phượng',26,null,null,0),
			(N'Phúc Thịnh Tower ',26,null,null,0),
			(N'Khu đô thị sinh thái Đan Phượng',26,null,null,0),
			(N'Siêu thị Hapromart',26,null,null,0),
			(N'Chợ Phùng',26,null,null,0),
			(N'Làng nghề mộc Liên Hà',26,null,null,0),
			(N'Khách sạn Đan Phượng',26,null,null,0),
			(N'Làng nghề Thuý Hội',26,null,null,0),
			-----XII. Đông Anh
			(N'Thành Cổ Loa',28,null,null,0),
			(N'Chợ Đông Trù',28,null,null,0),
			(N'Chợ Sa',28,null,null,0),
			(N'Làng nghề gỗ Đông Hà',28,null,null,0),
			(N' Khu Côn Đảo Xã Nam Hồng',28,null,null,0),
			(N'Chợ trung tâm thị trấn Đông Anh',28,null,null,0),
			(N'Chợ Tó',28,null,null,0),
			(N'Chùa Diên Phúc',28,null,null,0),
			(N'Chùa Bảo Sơn',28,null,null,0),
			(N'Chùa Kim Quy',28,null,null,0),
			---XIII. Đống Đa
			(N'Trung tâm thương mại Parkson',21,null,null,0),
			(N'Hà Thành Plaza',21,null,null,0),
			(N'Siêu thị Techsimex',21,null,null,0),
			(N'Rạp chiếu phim MegaStar - Pico Mall',21,null,null,0),
			(N'Rạp Dân Chủ',21,null,null,0),
			(N'Đại học Giao thông Vận tải - Hà Nội',21,null,null,0),
			(N'Đại học Ngoại Thương Hà Nội',21,null,null,0),
			(N'Đại học Văn hóa - Nghệ thuật Quân đội',21,null,null,0),
			(N'Đại học Thủy lợi',21,null,null,0),
			(N'Bệnh viện Đại học Y Hà Nội',21,null,null,0)
			----End--------
-------------------------------------------------------------------------------------------------------------------------


--USE RentalHouseFinding ;
--GO
--IF OBJECT_ID ('dbo.FullTextSearchPost', 'P') IS NOT NULL
--    DROP PROCEDURE dbo.FullTextSearchPost ;
--GO
--CREATE PROCEDURE FullTextSearchPost @CategoryIdPass int = 0, @ProvinceIdPass int = 0, @DistrictIdPass int = 0, @Keyword nvarchar(100) = null
--AS
--IF(@Keyword IS NULL AND @DistrictIdPass != 0)
--	BEGIN
--		SELECT Id, Title FROM dbo.V_PostFullInfo S WHERE S.CategoryId = @CategoryIdPass AND S.ProvinceId = @ProvinceIdPass AND S.DistrictId = @DistrictIdPass 
--	END
--ELSE IF(@Keyword IS NULL AND @DistrictIdPass = 0)
--	BEGIN
--		SELECT Id, Title FROM dbo.V_PostFullInfo S WHERE S.CategoryId = @CategoryIdPass AND S.ProvinceId = @ProvinceIdPass
--	END
--ELSE IF(@Keyword IS NOT NULL AND @DistrictIdPass != 0)
--	BEGIN
--		SELECT FT_TBL.*, KEY_TBL.RANK
--		FROM 
--		(SELECT Id, Title FROM dbo.V_PostFullInfo S 
--			WHERE S.CategoryId = @CategoryIdPass AND S.ProvinceId = @ProvinceIdPass AND S.DistrictId = @DistrictIdPass)
--		AS FT_TBL 
--			INNER JOIN FREETEXTTABLE(dbo.V_PostFullInfo,*, @Keyword) AS KEY_TBL
--				ON FT_TBL.Id = KEY_TBL.[KEY]
--		ORDER BY KEY_TBL.RANK DESC;
--	END
--ELSE IF(@Keyword IS NOT NULL AND @DistrictIdPass = 0)
--	BEGIN
--		SELECT FT_TBL.*, KEY_TBL.RANK
--		FROM 
--		(SELECT Id, Title FROM dbo.V_PostFullInfo S 
--			WHERE S.CategoryId = @CategoryIdPass AND S.ProvinceId = @ProvinceIdPass)
--		AS FT_TBL 
--			INNER JOIN FREETEXTTABLE(dbo.V_PostFullInfo,*, @Keyword) AS KEY_TBL
--				ON FT_TBL.Id = KEY_TBL.[KEY]
--		ORDER BY KEY_TBL.RANK DESC;
--	END
--GO

USE RentalHouseFinding ;
GO
IF OBJECT_ID ('dbo.FullTextSearchPostWithWeightenScore', 'P') IS NOT NULL
    DROP PROCEDURE dbo.FullTextSearchPostWithWeightenScore ;
GO
CREATE PROCEDURE FullTextSearchPostWithWeightenScore @CategoryIdPass int = 0, 
									@ProvinceIdPass int = 0, 
									@DistrictIdPass int = 0, 
									@Keyword nvarchar(100) = null,
									@TitleScore int = 1,
									@DescriptionScore int = 1,
									@StreetScore int = 1,
									@NearbyScore int = 1,
									@NumberAddressScore int = 1
AS
IF(@Keyword IS NULL AND @DistrictIdPass != 0)
	BEGIN
		SELECT Id, Title FROM dbo.V_PostFullInfo S WHERE S.CategoryId = @CategoryIdPass AND S.ProvinceId = @ProvinceIdPass AND S.DistrictId = @DistrictIdPass AND DATEDIFF(DD, S.ExpiredDate, GETDATE()) <= 0
	END
ELSE IF(@Keyword IS NULL AND @DistrictIdPass = 0)
	BEGIN
		SELECT Id, Title FROM dbo.V_PostFullInfo S WHERE S.CategoryId = @CategoryIdPass AND S.ProvinceId = @ProvinceIdPass AND DATEDIFF(DD, S.ExpiredDate, GETDATE()) <= 0
	END
ELSE IF(@Keyword IS NOT NULL AND @DistrictIdPass != 0)
	BEGIN
		SELECT f.WeightedRank, l.Title, l.Id
		FROM dbo.V_PostFullInfo l INNER JOIN
		(
			SELECT [KEY], SUM(Rank) AS WeightedRank
			FROM
			(
				SELECT Rank * @TitleScore as Rank, [KEY] from FREETEXTTABLE(dbo.V_PostFullInfo, Title , @Keyword)
				UNION
				SELECT Rank * @DescriptionScore as Rank, [KEY] from FREETEXTTABLE(dbo.V_PostFullInfo, Description, @Keyword)
				UNION
				SELECT Rank * @StreetScore as Rank, [KEY] from FREETEXTTABLE(dbo.V_PostFullInfo, Street, @Keyword)
				UNION
				SELECT Rank * @NearbyScore as Rank, [KEY] from FREETEXTTABLE(dbo.V_PostFullInfo, NearbyPlace, @Keyword)
				UNION
				SELECT Rank * @NumberAddressScore as Rank, [KEY] from FREETEXTTABLE(dbo.V_PostFullInfo, NumberAddress, @Keyword)
			) as x
			GROUP BY [KEY]
		) as f
		ON l.Id = f.[KEY]
		WHERE l.CategoryId = @CategoryIdPass AND l.ProvinceId = @ProvinceIdPass AND l.DistrictId = @DistrictIdPass AND DATEDIFF(DD, l.ExpiredDate, GETDATE()) <= 0 
		ORDER BY f.WeightedRank DESC
	END
ELSE IF(@Keyword IS NOT NULL AND @DistrictIdPass = 0)
	BEGIN
		SELECT f.WeightedRank, l.Title, l.Id
		FROM dbo.V_PostFullInfo l INNER JOIN
		(
			SELECT [KEY], SUM(Rank) AS WeightedRank
			FROM
			(
				SELECT Rank * @TitleScore as Rank, [KEY] from FREETEXTTABLE(dbo.V_PostFullInfo, Title , @Keyword)
				UNION
				SELECT Rank * @DescriptionScore as Rank, [KEY] from FREETEXTTABLE(dbo.V_PostFullInfo, Description, @Keyword)
				UNION
				SELECT Rank * @StreetScore as Rank, [KEY] from FREETEXTTABLE(dbo.V_PostFullInfo, Street, @Keyword)
				UNION
				SELECT Rank * @NearbyScore as Rank, [KEY] from FREETEXTTABLE(dbo.V_PostFullInfo, NearbyPlace, @Keyword)
				UNION
				SELECT Rank * @NumberAddressScore as Rank, [KEY] from FREETEXTTABLE(dbo.V_PostFullInfo, NumberAddress, @Keyword)
			) as x
			GROUP BY [KEY]
		) as f
		ON l.Id = f.[KEY]
		WHERE l.CategoryId = @CategoryIdPass AND l.ProvinceId = @ProvinceIdPass AND DATEDIFF(DD, l.ExpiredDate, GETDATE()) <= 0
		ORDER BY f.WeightedRank DESC
	END
GO




USE RentalHouseFinding ;
GO
IF OBJECT_ID ('dbo.V_PostFacilityInfo', 'V') IS NOT NULL
    DROP VIEW dbo.V_PostFacilityInfo ;
GO
CREATE VIEW V_PostFacilityInfo WITH SCHEMABINDING AS
SELECT P.Id, P.ExpiredDate, P.Area, P.Price, F.HasAirConditioner, F.HasBed, F.HasGarage, F.HasInternet, F.HasMotorParkingLot, F.HasSecurity, F.HasToilet, F.HasTVCable, F.HasWaterHeater, F.IsAllowCooking, F.IsStayWithOwner, P.CategoryId, P.DistrictId, D.ProvinceId, F.FacilityTemplateId
FROM dbo.Posts P 
	INNER JOIN dbo.Facilities F
		ON(P.Id = F.PostIdFacilities)
	INNER JOIN dbo.Contacts C
		ON(P.Id = C.PostIdContacts)
	INNER JOIN dbo.Districts D
		ON(P.DistrictId = D.Id) 
WHERE P.IsDeleted = 'false' AND D.IsDeleted = 'false' AND P.StatusId = 1

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

IF(@PriceMax = 0)	
	BEGIN
		SET @PriceMax = CAST('1.79E+308' AS float)
	END
IF(@AreaMax = 0)
	BEGIN
		SET @AreaMax = CAST('1.79E+308' AS float)
	END
IF(@DistrictIdPass != 0)
	BEGIN
		SELECT S.Id, ((T.Column1 * @HasAirConditionerScore) + 
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
			DATEDIFF(DD, F.ExpiredDate, GETDATE()) <= 0 AND
			F.Area <= @AreaMax AND F.Area >= @AreaMin AND
			F.Price <= @PriceMax AND F.Price >= @PriceMin) S
		ON T.Id = S.FacilityTemplateId
		ORDER BY Score
	END
ELSE
	BEGIN
		SELECT S.Id, ((T.Column1 * @HasAirConditionerScore) + 
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
			F.ProvinceId = @ProvinceIdPass AND
			DATEDIFF(DD, F.ExpiredDate, GETDATE()) <= 0 AND
			F.Area <= @AreaMax AND F.Area >= @AreaMin AND
			F.Price <= @PriceMax AND F.Price >= @PriceMin) S
		ON T.Id = S.FacilityTemplateId
		ORDER BY Score
	END

GO

EXEC AdvancedSearchFacility 
	@CategoryIdPass = 2, 
	@ProvinceIdPass = 2, 
	@DistrictIdPass = 0, 
	@AreaMax = 0, 
	@AreaMin = 0, 
	@PriceMax = 0, 
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
	@IsStayWithOwnerScore = 5


EXEC dbo.FullTextSearchPostWithWeightenScore @CategoryIdPass = 1, @ProvinceIdPass = 1, @DistrictIdPass = 0, @KeyWord = 'xau',
												@NumberAddressScore = 10