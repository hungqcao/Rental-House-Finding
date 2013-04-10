USE [RentalHouseFinding]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 04/10/2013 12:20:56 ******/
SET IDENTITY_INSERT [dbo].[Posts] ON
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (1, N'Phòng trọ tiện nghi thoáng mát', 1, 1, 0, CAST(0x0000A19A012C96F2 AS DateTime), NULL, CAST(0x0000A19B0159115E AS DateTime), 20, N'Ngõ 175', 2.7, 20, N'0978866909', N'Nhà đẹp, thoáng mát, an ninh tốt', 21.03600311732383, 105.78479869259036, NULL, N'Xuân Thủy', N'N/A', CAST(0x0000A1AB012C96F2 AS DateTime), N'5e8a')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (2, N'cho thuê nhà trọ', 1, 1, 1, CAST(0x0000A19A01300C2B AS DateTime), CAST(0x0000A19B0142FBE8 AS DateTime), CAST(0x0000A19B0142FBE8 AS DateTime), 20, N'ngõ 155', 2, 18, N'0978866909', N'', 21.035963062065, 105.782846044428, NULL, N'xuân thủy', N'Đại học Ngoại ngữ - Đại học Quốc gia Hà Nội, Đại học Sư phạm Hà Nội, Đại học FPT, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10', CAST(0x0000A19C01300C2B AS DateTime), N'49mc')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (3, N'Phòng trọ chính chủ', 1, 1, 0, CAST(0x0000A19A0132210F AS DateTime), NULL, NULL, 20, N'79', 2, 20, N'0978866909', N'Có xép, không gian rộng, thoải mái.', 21.035943034431583, 105.78615052593386, NULL, N'Xuân thủy', N'Bệnh viện 19 - 8, Đại học Sư phạm Hà Nội, Đại học Ngoại ngữ - Đại học Quốc gia Hà Nội', CAST(0x0000A19C0132210F AS DateTime), N'nvy5')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (4, N'Phòng trọ đẹp giá phải chăng', 1, 1, 0, CAST(0x0000A19A0135EBA8 AS DateTime), NULL, NULL, 20, N'155', 1.8, 18, N'0978866909', N'Đóng tiền nhà 2 tháng/lần', 21.031952461081016, 105.79870370632932, NULL, N'Cầu Giấy', N'Bệnh viện Quốc tế Hoa Kỳ, Đại học Sư phạm Hà Nội, Đại học Ngoại ngữ - Đại học Quốc gia Hà Nội', CAST(0x0000A19C0135EBA8 AS DateTime), N'rFUG')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (5, N'Cho người đi làm thuê nhà trọ', 1, 1, 0, CAST(0x0000A19A01384465 AS DateTime), NULL, NULL, 20, N'131', 3.5, 25, N'0978866909', N'Chỉ cho người đi làm thuê. SV miễn liên hệ.', 21.04271648491298, 105.79033960370487, NULL, N'Nguyễn Phong Sắc', N'Bệnh viện Quốc tế Hoa Kỳ, Đại học Sư phạm Hà Nội, Đại học Ngoại ngữ - Đại học Quốc gia Hà Nội', CAST(0x0000A19C01384465 AS DateTime), N'MuCa')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (6, N'Cho thuê phòng trọ', 1, 1, 0, CAST(0x0000A19A01402434 AS DateTime), NULL, NULL, 20, N'199', 2.5, 25, N'0978866909', N'Có gác xép. Gần chợ và nhiều trường Đại học.', 21.040256836985517, 105.76400759941407, NULL, N'Hồ Tùng Mậu', N'Bệnh viện 19 - 8', CAST(0x0000A19C01402434 AS DateTime), N'eTtN')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (7, N'Nhà trọ cho sinh viên', 1, 1, 0, CAST(0x0000A19A0141D628 AS DateTime), NULL, NULL, 20, N'Trung Yên 12', 2, 18, N'0978866909', N'Hợp đồng 3 tháng/lần', 21.017750705855665, 105.80214939364009, NULL, N'Nguyễn Khang', N'N/A', CAST(0x0000A19C0141D628 AS DateTime), N'andU')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (8, N'Nhà trọ sinh viên giá rẻ', 1, 1, 0, CAST(0x0000A19A01446F29 AS DateTime), NULL, NULL, 20, N'ngõ 20/7', 2.3, 20, N'0978866909', N'Nhà có gác xép, sạch sẽ rộng rãi', 21.030841580467165, 105.78354198410034, NULL, N'Duy tân', N'Đại học FPT', CAST(0x0000A19C01446F29 AS DateTime), N'klek')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (9, N'trung cư mini cao cấp', 1, 1, 0, CAST(0x0000A19A01477459 AS DateTime), NULL, NULL, 20, N'Ngõ 165', 3.2, 20, N'0978866909', N'Chung cư mini có 6 tầng. Mỗi tầng 4 phòng. 20m2 /1 phong. ', 21.032813669953569, 105.79539922482297, NULL, N'Cầu giấy', N'Bưu điện cầu giấy', CAST(0x0000A19C01477459 AS DateTime), N'HjdT')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (10, N'Cho thuê chung cư mini', 1, 1, 0, CAST(0x0000A19A014A5A8B AS DateTime), NULL, NULL, 20, N'603', 3, 27, N'0978866909', N'Tiện nghi đầy đủ, phù hợp hộ gia đình, người đi làm.', 21.066880471293846, 105.81067800521851, NULL, N'Lạc Long Quân', N'N/A', CAST(0x0000A19C014A5A8B AS DateTime), N'TQnv')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (11, N'Nhà riêng cần cho thuê', 2, 1, 1, CAST(0x0000A19A014ED19A AS DateTime), CAST(0x0000A19B01200B51 AS DateTime), CAST(0x0000A19B01200B51 AS DateTime), 20, N'166/12', 10, 50, N'0978866909', N'Tiện nghi đầy đủ. Vào ở luôn. Điện nước giá nhà nước.', 21.012707, 105.801044, NULL, N'Trần Duy Hưng', N'N/A', CAST(0x0000A19C014ED19A AS DateTime), N'OS4i')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (12, N'Nhà riêng chính chủ cần cho thuê', 2, 1, 1, CAST(0x0000A19A0150290D AS DateTime), CAST(0x0000A19B0120C84E AS DateTime), CAST(0x0000A19B0120C84E AS DateTime), 20, N'36', 5, 40, N'0978866909', N'Ngõ rộng ô tô vào được. Điện nước giá rẻ.', 21.0342129, 105.80538, NULL, N'Đào Tấn', N'N/A', CAST(0x0000A19C0150290D AS DateTime), N'HIlM')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (13, N'Nhà riêng chính chủ cho thuê', 2, 1, 0, CAST(0x0000A19A015253D5 AS DateTime), NULL, NULL, 20, N'29 Dịch Vọng', 10, 80, N'0978866909', N'3 tầng, 3 phòng ngủ. Đêện nước giá rẻ.', 21.034455961448778, 105.79432634121702, NULL, N'Cầu Giấy', N'N/A', CAST(0x0000A19C015253D5 AS DateTime), N'mMse')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (14, N'Nhà mới xây cần cho thuê', 2, 1, 0, CAST(0x0000A19A0154267B AS DateTime), NULL, NULL, 20, N'173', 7, 50, N'0978866909', N'N/A', 21.0384538, 105.79265429999998, NULL, N'Trần Đăng Ninh', N'N/A', CAST(0x0000A19C0154267B AS DateTime), N'MJIv')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (15, N'Cho thuê nhà riêng Cầu Giấy', 2, 1, 0, CAST(0x0000A19A01553A7F AS DateTime), NULL, NULL, 20, N'1014', 10, 80, N'0978866909', N'Đóng tiền nhà 3 tháng/lần. Cho Công ty thuê làm VP. Điện nước giá rẻ.', 21.0261396, 105.79845249999994, NULL, N'Láng', N'Điện máy Trần Anh, Đại học GTVT', CAST(0x0000A19C01553A7F AS DateTime), N'Yt7Y')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (16, N'Cho thuê nhà riêng hot', 2, 1, 0, CAST(0x0000A19A01568496 AS DateTime), NULL, NULL, 20, N'181', 12, 50, N'0978866909', N'Phù hợp hộ gia đình, văn phòng công ty.', 21.041582382550793, 105.79435674445801, NULL, N'Tô Hiệu', N'Trường Tiểu học Nghĩa Tân, Công viên Nghĩa Tân, Chợ Nghĩa Tân', CAST(0x0000A19C01568496 AS DateTime), N'EsGI')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (17, N'Nhà riêng cho thuê', 2, 1, 0, CAST(0x0000A19A0157C49A AS DateTime), NULL, NULL, 20, N'259', 8, 40, N'0978866909', N'N/A', 21.0218044, 105.79087200000004, NULL, N'Yên Hòa', N'Viện Huyết Học Truyền Máu Trung Ương', CAST(0x0000A19C0157C49A AS DateTime), N'8duI')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (18, N'Có nhà riêng cho thuê', 2, 1, 0, CAST(0x0000A19A0158C1C1 AS DateTime), NULL, NULL, 20, N'175', 12, 45, N'0978866909', N'Nhà 4 tầng. Diện tích sàn 45m2. Đóng tiền nhà 2 tháng/lần.', 21.036644, 105.78509910000003, NULL, N'Xuân Thủy', N'Đại học Sư phạm Hà Nội, Đại học Ngoại ngữ - Đại học Quốc gia Hà Nội, Chợ Xanh', CAST(0x0000A19C0158C1C1 AS DateTime), N'fqHN')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (19, N'Cho thuê nhà riêng mới ', 2, 1, 0, CAST(0x0000A19A0159EBB5 AS DateTime), NULL, NULL, 20, N'111', 12, 55, N'0978866909', N'Sàn gỗ, tiện nghi đày đủ. Vào ở luôn. Điện nước giá rẻ.', 21.045373707176118, 105.79031467437744, NULL, N'Nguyễn Phong Sắc', N'Học viện báo chí tuyên truyền', CAST(0x0000A19C0159EBB5 AS DateTime), N'JXyE')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (20, N'nha1advc', 2, 1, 1, CAST(0x0000A19B015A7EF0 AS DateTime), CAST(0x0000A19C0017B32E AS DateTime), CAST(0x0000A19B016F3850 AS DateTime), 20, N'166/12', 1, 20, N'0978866909', N'', 21.0468451489086, 105.791066784656, 1, N'nghĩa tân', N'N/A', CAST(0x0000A1BB015A7EF0 AS DateTime), N'X5I3')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (21, N'FUCK', 2, 1, 1, CAST(0x0000A19B016BC197 AS DateTime), CAST(0x0000A19B01700497 AS DateTime), NULL, 20, N'số 8', 2, 20, N'0978866909', N'', 21.036644, 105.7850991, 1, N'xuân thủy', N'N/A', CAST(0x0000A19D016BC197 AS DateTime), N'Mzwd')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (22, N'F U C K 3', 2, 1, 1, CAST(0x0000A19B01746B83 AS DateTime), CAST(0x0000A19D01746B83 AS DateTime), NULL, 20, N'3', 3, 20, N'0978866909', N'', 21.0303609, 105.7870825, 1, N'Duy tân', N'N/A', CAST(0x0000A19D01746B83 AS DateTime), N'nL4B')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (23, N'nhà đẹp', 2, 1, 1, CAST(0x0000A19B0174E7F4 AS DateTime), CAST(0x0000A19D017633C9 AS DateTime), NULL, 20, N'3', 3, 20, N'0978866909', N'', 21.0303609, 105.7870825, NULL, N'Duy tân ', N'N/A', CAST(0x0000A19D017633C9 AS DateTime), N'J38U')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (24, N'F U C K 3 ', 2, 1, 1, CAST(0x0000A19C00B0002A AS DateTime), CAST(0x0000A19C00B046DC AS DateTime), NULL, 20, N'166/12', 1, 20, N'0978866909', N'', 21.036644, 105.7850991, 2, N'xuân thủy', N'N/A', CAST(0x0000A19E00B0002A AS DateTime), N'0sjM')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (25, N'F U C K 1', 2, 1, 1, CAST(0x0000A19C00B074CD AS DateTime), NULL, NULL, 20, N'166/12', 3, 20, N'0978866909', N'N/A', 21.036644, 105.78509910000003, 2, N'xuân thủy', N'N/A', CAST(0x0000A19E00B074CD AS DateTime), N'xFbu')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (26, N'F U C K 1', 2, 1, 1, CAST(0x0000A19C00B0B4DE AS DateTime), NULL, NULL, 20, N'1', 1, 20, N'0978866909', N'N/A', 21.036644, 105.78509910000003, 2, N'xuân thủy ', N'N/A', CAST(0x0000A19E00B0B4DE AS DateTime), N'INNK')
INSERT [dbo].[Posts] ([Id], [Title], [CategoryId], [StatusId], [IsDeleted], [CreatedDate], [EditedDate], [RenewDate], [DistrictId], [NumberAddress], [Price], [Area], [PhoneActive], [Description], [Lat], [Lon], [UserId], [Street], [NearbyPlace], [ExpiredDate], [Code]) VALUES (27, N'F U C K 2', 2, 1, 1, CAST(0x0000A19C00B0D652 AS DateTime), NULL, NULL, 20, N'số 250', 1, 1, N'0978866909', N'N/A', 21.036644, 105.78509910000003, 1, N'xuân thủy', N'N/A', CAST(0x0000A19E00B0D652 AS DateTime), N'Ut5h')
SET IDENTITY_INSERT [dbo].[Posts] OFF
/****** Object:  Table [dbo].[Facilities]    Script Date: 04/10/2013 12:20:56 ******/
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (1, 5, 15, 0, 1, 0, 1, 1, 1, 1, 0, 0, N'23h30', 0, 1, 1747)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (1, 5, 20, 0, 1, 0, 1, 1, 1, 0, 0, 0, N'11h', 0, 2, 723)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (1, 3, 15, 1, 0, 0, 1, 0, 1, 0, 0, 0, N'23', 1, 3, 187)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (1, 3.5, 15, 1, 0, 0, 1, 1, 1, 0, 0, 1, N'0', 1, 4, 252)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (1, 3500, 14000, 1, 1, 1, 1, 0, 1, 1, 0, 0, N'0', 1, 5, 1727)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (1, 3500, 15000, 1, 0, 1, 1, 1, 1, 0, 0, 0, N'23', 1, 6, 255)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (1, 4000, 15000, 0, 0, 0, 1, 1, 1, 0, 0, 0, N'23', 0, 7, 211)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (1, 5, 20, 0, 1, 0, 1, 1, 1, 0, 0, 0, N'11h', 0, 8, 723)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (1, 5, 20, 1, 1, 1, 1, 1, 1, 1, 0, 0, N'23h', 1, 9, 1791)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (1, 3500, 15000, 1, 1, 1, 1, 1, 1, 1, 0, 0, N'0', 1, 10, 1791)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, N'0', 1, 11, 1759)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, N'0', 0, 12, 1759)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (1, 0, 0, 1, 1, 1, 1, 0, 1, 1, 0, 0, N'0', 0, 13, 1695)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, N'0', 0, 14, 1759)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, N'0', 0, 15, 2016)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, N'0', 0, 16, 2015)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (1, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 0, N'0', 0, 17, 1247)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (1, 0, 0, 1, 1, 1, 1, 0, 1, 1, 0, 0, N'0', 0, 18, 1695)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 0, 0, N'0', 0, 19, 1759)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'0', 0, 20, 1)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'0', 0, 21, 1)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'66h', 0, 22, 1)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'23h', 0, 23, 1)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'0', 0, 24, 1)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'0', 0, 25, 1)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'0', 0, 26, 1)
INSERT [dbo].[Facilities] ([HasInternet], [ElectricityFee], [WaterFee], [HasTVCable], [HasBed], [HasWaterHeater], [IsAllowCooking], [HasMotorParkingLot], [HasToilet], [HasAirConditioner], [HasGarage], [IsStayWithOwner], [RestrictHours], [HasSecurity], [PostIdFacilities], [FacilityTemplateId]) VALUES (0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, N'0', 0, 27, 1)
/****** Object:  Table [dbo].[Contacts]    Script Date: 04/10/2013 12:20:56 ******/
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', N'bacviet@yahoo.com', N'vietvh01388', N'vietvh_01388', 1, N'Bác Việt.')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', NULL, NULL, NULL, 2, N'cô Hoa')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', N'vuthuylinh1801@gmail.com', NULL, N'vuthuylinh1801', 3, N'Ms Linh')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', NULL, NULL, NULL, 4, N'Cô Hà')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', NULL, NULL, NULL, 5, N'Bác Long')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', NULL, NULL, NULL, 6, N'Bác Bành')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', NULL, NULL, NULL, 7, N'Bà Lành')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', NULL, NULL, N'vietvh_01388', 8, N'Tài')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', N'chungcumini@giare.com', N'vietvh01388', N'vietvh_01388', 9, N'Giáp')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', NULL, NULL, NULL, 10, N'Chị Hồng')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', NULL, NULL, NULL, 11, N'Anh Hưng')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', NULL, NULL, NULL, 12, N'Chú Công')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', NULL, NULL, NULL, 13, N'Anh Long')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', NULL, NULL, NULL, 14, N'Ông Công')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', NULL, NULL, NULL, 15, N'chị Loan')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', NULL, NULL, NULL, 16, N'Bà Hà')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', NULL, NULL, NULL, 17, N'Ông Nam')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', NULL, NULL, NULL, 18, N'Bà Bích')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', NULL, NULL, NULL, 19, N'Ông Hưng')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', NULL, NULL, NULL, 20, N'')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866888', NULL, NULL, NULL, 21, N'')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866888', NULL, NULL, NULL, 22, N'')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866888', NULL, NULL, NULL, 23, N'')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866888', NULL, NULL, NULL, 24, N'')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866888', NULL, NULL, NULL, 25, N'N/A')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866909', NULL, NULL, NULL, 26, N'N/A')
INSERT [dbo].[Contacts] ([Phone], [Email], [Yahoo], [Skype], [PostIdContacts], [NameContact]) VALUES (N'0978866888', NULL, NULL, NULL, 27, N'N/A')
/****** Object:  Table [dbo].[PostLocations]    Script Date: 04/10/2013 12:20:56 ******/
SET IDENTITY_INSERT [dbo].[PostLocations] ON
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (1, 2, 23)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (2, 2, 22)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (3, 2, 21)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (4, 3, 15)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (5, 3, 22)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (6, 3, 23)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (7, 4, 18)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (8, 4, 22)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (9, 4, 23)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (10, 5, 18)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (11, 5, 22)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (12, 5, 23)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (13, 6, 15)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (14, 8, 21)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (15, 9, 24)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (16, 15, 25)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (17, 15, 26)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (18, 16, 27)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (19, 16, 28)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (20, 16, 29)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (21, 17, 14)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (22, 18, 22)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (23, 18, 23)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (24, 18, 30)
INSERT [dbo].[PostLocations] ([Id], [PostId], [LocationId]) VALUES (25, 19, 31)
SET IDENTITY_INSERT [dbo].[PostLocations] OFF
