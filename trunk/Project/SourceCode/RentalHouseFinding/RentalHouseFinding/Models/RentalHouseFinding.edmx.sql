
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/12/2013 21:49:00
-- Generated from EDMX file: C:\RentalHouseFinding\Project\SourceCode\RentalHouseFinding\RentalHouseFinding\Models\RentalHouseFinding.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RentalHouseFinding];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ProvincesDistricts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Districts] DROP CONSTRAINT [FK_ProvincesDistricts];
GO
IF OBJECT_ID(N'[dbo].[FK_DistrictsPosts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_DistrictsPosts];
GO
IF OBJECT_ID(N'[dbo].[FK_CategoriesPosts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_CategoriesPosts];
GO
IF OBJECT_ID(N'[dbo].[FK_PostsPostImages]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostImages] DROP CONSTRAINT [FK_PostsPostImages];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersFavorites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Favorites] DROP CONSTRAINT [FK_UsersFavorites];
GO
IF OBJECT_ID(N'[dbo].[FK_PostsFavorites]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Favorites] DROP CONSTRAINT [FK_PostsFavorites];
GO
IF OBJECT_ID(N'[dbo].[FK_PostsReportedPosts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReportedPosts] DROP CONSTRAINT [FK_PostsReportedPosts];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersReportedPosts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ReportedPosts] DROP CONSTRAINT [FK_UsersReportedPosts];
GO
IF OBJECT_ID(N'[dbo].[FK_RolesUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_RolesUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_PostsFacilities]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Facilities] DROP CONSTRAINT [FK_PostsFacilities];
GO
IF OBJECT_ID(N'[dbo].[FK_PostsContacts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Contacts] DROP CONSTRAINT [FK_PostsContacts];
GO
IF OBJECT_ID(N'[dbo].[FK_PostStatusesPosts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_PostStatusesPosts];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersPosts]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_UsersPosts];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestionsAnswers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Answers] DROP CONSTRAINT [FK_QuestionsAnswers];
GO
IF OBJECT_ID(N'[dbo].[FK_FacilityTemplatesFacilities]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Facilities] DROP CONSTRAINT [FK_FacilityTemplatesFacilities];
GO
IF OBJECT_ID(N'[dbo].[FK_BadWordTypesBadWords]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BadWords] DROP CONSTRAINT [FK_BadWordTypesBadWords];
GO
IF OBJECT_ID(N'[dbo].[FK_DistrictsLocations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Locations] DROP CONSTRAINT [FK_DistrictsLocations];
GO
IF OBJECT_ID(N'[dbo].[FK_PostsPostLocations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostLocations] DROP CONSTRAINT [FK_PostsPostLocations];
GO
IF OBJECT_ID(N'[dbo].[FK_LocationsPostLocations]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostLocations] DROP CONSTRAINT [FK_LocationsPostLocations];
GO
IF OBJECT_ID(N'[dbo].[FK_PostsQuestions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Questions] DROP CONSTRAINT [FK_PostsQuestions];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersQuestions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Questions] DROP CONSTRAINT [FK_UsersQuestions];
GO
IF OBJECT_ID(N'[dbo].[FK_UsersUserLogs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserLogs] DROP CONSTRAINT [FK_UsersUserLogs];
GO
IF OBJECT_ID(N'[dbo].[FK_PostsPayments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Payments] DROP CONSTRAINT [FK_PostsPayments];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Provinces]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Provinces];
GO
IF OBJECT_ID(N'[dbo].[Districts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Districts];
GO
IF OBJECT_ID(N'[dbo].[Posts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[PostImages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostImages];
GO
IF OBJECT_ID(N'[dbo].[PostStatuses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostStatuses];
GO
IF OBJECT_ID(N'[dbo].[Favorites]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Favorites];
GO
IF OBJECT_ID(N'[dbo].[ReportedPosts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ReportedPosts];
GO
IF OBJECT_ID(N'[dbo].[BadWords]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BadWords];
GO
IF OBJECT_ID(N'[dbo].[Facilities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Facilities];
GO
IF OBJECT_ID(N'[dbo].[Contacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contacts];
GO
IF OBJECT_ID(N'[dbo].[ConfigurationRHFs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConfigurationRHFs];
GO
IF OBJECT_ID(N'[dbo].[Questions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Questions];
GO
IF OBJECT_ID(N'[dbo].[Answers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Answers];
GO
IF OBJECT_ID(N'[dbo].[AdvanceSearchScores]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AdvanceSearchScores];
GO
IF OBJECT_ID(N'[dbo].[FacilityTemplates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FacilityTemplates];
GO
IF OBJECT_ID(N'[dbo].[BadWordTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BadWordTypes];
GO
IF OBJECT_ID(N'[dbo].[Locations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Locations];
GO
IF OBJECT_ID(N'[dbo].[PostLocations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostLocations];
GO
IF OBJECT_ID(N'[dbo].[UserLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserLogs];
GO
IF OBJECT_ID(N'[dbo].[EmailTemplates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmailTemplates];
GO
IF OBJECT_ID(N'[dbo].[Payments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Payments];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(50)  NOT NULL,
    [Password] nvarchar(max)  NULL,
    [Email] nvarchar(50)  NULL,
    [PhoneNumber] nvarchar(15)  NULL,
    [Name] nvarchar(max)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [LastUpdate] datetime  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [OpenIdURL] nvarchar(max)  NULL,
    [RoleId] int  NOT NULL
);
GO

-- Creating table 'Provinces'
CREATE TABLE [dbo].[Provinces] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Lat] float  NOT NULL,
    [Lon] float  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'Districts'
CREATE TABLE [dbo].[Districts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Lat] float  NOT NULL,
    [Lon] float  NOT NULL,
    [ProvinceId] int  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'Posts'
CREATE TABLE [dbo].[Posts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [CategoryId] int  NOT NULL,
    [StatusId] int  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [EditedDate] datetime  NULL,
    [RenewDate] datetime  NULL,
    [DistrictId] int  NOT NULL,
    [NumberAddress] nvarchar(max)  NOT NULL,
    [Price] float  NOT NULL,
    [Area] float  NOT NULL,
    [PhoneActive] nvarchar(20)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Lat] float  NOT NULL,
    [Lon] float  NOT NULL,
    [UserId] int  NULL,
    [Street] nvarchar(50)  NOT NULL,
    [NearbyPlace] nvarchar(max)  NULL,
    [ExpiredDate] datetime  NOT NULL,
    [Code] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'PostImages'
CREATE TABLE [dbo].[PostImages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PostId] int  NOT NULL,
    [Path] nvarchar(max)  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'PostStatuses'
CREATE TABLE [dbo].[PostStatuses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Description] nvarchar(max)  NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'Favorites'
CREATE TABLE [dbo].[Favorites] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [PostId] int  NOT NULL,
    [AddedDate] datetime  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'ReportedPosts'
CREATE TABLE [dbo].[ReportedPosts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PostId] int  NOT NULL,
    [ReportedBy] int  NOT NULL,
    [ReportedDate] datetime  NOT NULL,
    [Reason] nvarchar(max)  NOT NULL,
    [IsIgnored] bit  NOT NULL
);
GO

-- Creating table 'BadWords'
CREATE TABLE [dbo].[BadWords] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Word] nvarchar(100)  NOT NULL,
    [TypeId] int  NOT NULL
);
GO

-- Creating table 'Facilities'
CREATE TABLE [dbo].[Facilities] (
    [HasInternet] bit  NOT NULL,
    [ElectricityFee] float  NULL,
    [WaterFee] float  NULL,
    [HasTVCable] bit  NOT NULL,
    [HasBed] bit  NOT NULL,
    [HasWaterHeater] bit  NOT NULL,
    [IsAllowCooking] bit  NOT NULL,
    [HasMotorParkingLot] bit  NOT NULL,
    [HasToilet] bit  NOT NULL,
    [HasAirConditioner] bit  NOT NULL,
    [HasGarage] bit  NOT NULL,
    [IsStayWithOwner] bit  NOT NULL,
    [RestrictHours] nvarchar(50)  NULL,
    [HasSecurity] bit  NOT NULL,
    [PostIdFacilities] int  NOT NULL,
    [FacilityTemplateId] int  NOT NULL
);
GO

-- Creating table 'Contacts'
CREATE TABLE [dbo].[Contacts] (
    [Phone] nvarchar(20)  NOT NULL,
    [Email] nvarchar(100)  NULL,
    [Yahoo] nvarchar(50)  NULL,
    [Skype] nvarchar(50)  NULL,
    [PostIdContacts] int  NOT NULL,
    [NameContact] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ConfigurationRHFs'
CREATE TABLE [dbo].[ConfigurationRHFs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL,
    [Value] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Questions'
CREATE TABLE [dbo].[Questions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [SenderId] int  NULL,
    [CreatedDate] datetime  NOT NULL,
    [IsRead] bit  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [PostId] int  NOT NULL,
    [Title] nvarchar(50)  NOT NULL,
    [SenderEmail] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Answers'
CREATE TABLE [dbo].[Answers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(max)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [QuestionId] int  NOT NULL,
    [IsRead] bit  NOT NULL
);
GO

-- Creating table 'AdvanceSearchScores'
CREATE TABLE [dbo].[AdvanceSearchScores] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Score] int  NOT NULL
);
GO

-- Creating table 'FacilityTemplates'
CREATE TABLE [dbo].[FacilityTemplates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HasAirConditioner] bit  NOT NULL,
    [HasBed] bit  NOT NULL,
    [HasGarage] bit  NOT NULL,
    [HasInternet] bit  NOT NULL,
    [HasMotorParking] bit  NOT NULL,
    [HasSecurity] bit  NOT NULL,
    [HasToilet] bit  NOT NULL,
    [HasTVCable] bit  NOT NULL,
    [HasWaterHeater] bit  NOT NULL,
    [IsAllowCooking] bit  NOT NULL,
    [IsStayWithOwner] bit  NOT NULL
);
GO

-- Creating table 'BadWordTypes'
CREATE TABLE [dbo].[BadWordTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Locations'
CREATE TABLE [dbo].[Locations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [DistrictId] int  NOT NULL,
    [IsCreatedByUser] bit  NOT NULL
);
GO

-- Creating table 'PostLocations'
CREATE TABLE [dbo].[PostLocations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PostId] int  NOT NULL,
    [LocationId] int  NOT NULL
);
GO

-- Creating table 'UserLogs'
CREATE TABLE [dbo].[UserLogs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Message] nvarchar(max)  NOT NULL,
    [UserId] int  NOT NULL,
    [IsRead] bit  NOT NULL,
    [CreatedDate] datetime  NOT NULL
);
GO

-- Creating table 'EmailTemplates'
CREATE TABLE [dbo].[EmailTemplates] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Template] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Payments'
CREATE TABLE [dbo].[Payments] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PhoneNumber] nvarchar(20)  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [PostsId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Provinces'
ALTER TABLE [dbo].[Provinces]
ADD CONSTRAINT [PK_Provinces]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Districts'
ALTER TABLE [dbo].[Districts]
ADD CONSTRAINT [PK_Districts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [PK_Posts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PostImages'
ALTER TABLE [dbo].[PostImages]
ADD CONSTRAINT [PK_PostImages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PostStatuses'
ALTER TABLE [dbo].[PostStatuses]
ADD CONSTRAINT [PK_PostStatuses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Favorites'
ALTER TABLE [dbo].[Favorites]
ADD CONSTRAINT [PK_Favorites]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ReportedPosts'
ALTER TABLE [dbo].[ReportedPosts]
ADD CONSTRAINT [PK_ReportedPosts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BadWords'
ALTER TABLE [dbo].[BadWords]
ADD CONSTRAINT [PK_BadWords]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [PostIdFacilities] in table 'Facilities'
ALTER TABLE [dbo].[Facilities]
ADD CONSTRAINT [PK_Facilities]
    PRIMARY KEY CLUSTERED ([PostIdFacilities] ASC);
GO

-- Creating primary key on [PostIdContacts] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [PK_Contacts]
    PRIMARY KEY CLUSTERED ([PostIdContacts] ASC);
GO

-- Creating primary key on [Id] in table 'ConfigurationRHFs'
ALTER TABLE [dbo].[ConfigurationRHFs]
ADD CONSTRAINT [PK_ConfigurationRHFs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [PK_Questions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [PK_Answers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AdvanceSearchScores'
ALTER TABLE [dbo].[AdvanceSearchScores]
ADD CONSTRAINT [PK_AdvanceSearchScores]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FacilityTemplates'
ALTER TABLE [dbo].[FacilityTemplates]
ADD CONSTRAINT [PK_FacilityTemplates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BadWordTypes'
ALTER TABLE [dbo].[BadWordTypes]
ADD CONSTRAINT [PK_BadWordTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Locations'
ALTER TABLE [dbo].[Locations]
ADD CONSTRAINT [PK_Locations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PostLocations'
ALTER TABLE [dbo].[PostLocations]
ADD CONSTRAINT [PK_PostLocations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserLogs'
ALTER TABLE [dbo].[UserLogs]
ADD CONSTRAINT [PK_UserLogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmailTemplates'
ALTER TABLE [dbo].[EmailTemplates]
ADD CONSTRAINT [PK_EmailTemplates]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Payments'
ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [PK_Payments]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ProvinceId] in table 'Districts'
ALTER TABLE [dbo].[Districts]
ADD CONSTRAINT [FK_ProvincesDistricts]
    FOREIGN KEY ([ProvinceId])
    REFERENCES [dbo].[Provinces]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProvincesDistricts'
CREATE INDEX [IX_FK_ProvincesDistricts]
ON [dbo].[Districts]
    ([ProvinceId]);
GO

-- Creating foreign key on [DistrictId] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_DistrictsPosts]
    FOREIGN KEY ([DistrictId])
    REFERENCES [dbo].[Districts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DistrictsPosts'
CREATE INDEX [IX_FK_DistrictsPosts]
ON [dbo].[Posts]
    ([DistrictId]);
GO

-- Creating foreign key on [CategoryId] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_CategoriesPosts]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoriesPosts'
CREATE INDEX [IX_FK_CategoriesPosts]
ON [dbo].[Posts]
    ([CategoryId]);
GO

-- Creating foreign key on [PostId] in table 'PostImages'
ALTER TABLE [dbo].[PostImages]
ADD CONSTRAINT [FK_PostsPostImages]
    FOREIGN KEY ([PostId])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostsPostImages'
CREATE INDEX [IX_FK_PostsPostImages]
ON [dbo].[PostImages]
    ([PostId]);
GO

-- Creating foreign key on [UserId] in table 'Favorites'
ALTER TABLE [dbo].[Favorites]
ADD CONSTRAINT [FK_UsersFavorites]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersFavorites'
CREATE INDEX [IX_FK_UsersFavorites]
ON [dbo].[Favorites]
    ([UserId]);
GO

-- Creating foreign key on [PostId] in table 'Favorites'
ALTER TABLE [dbo].[Favorites]
ADD CONSTRAINT [FK_PostsFavorites]
    FOREIGN KEY ([PostId])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostsFavorites'
CREATE INDEX [IX_FK_PostsFavorites]
ON [dbo].[Favorites]
    ([PostId]);
GO

-- Creating foreign key on [PostId] in table 'ReportedPosts'
ALTER TABLE [dbo].[ReportedPosts]
ADD CONSTRAINT [FK_PostsReportedPosts]
    FOREIGN KEY ([PostId])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostsReportedPosts'
CREATE INDEX [IX_FK_PostsReportedPosts]
ON [dbo].[ReportedPosts]
    ([PostId]);
GO

-- Creating foreign key on [ReportedBy] in table 'ReportedPosts'
ALTER TABLE [dbo].[ReportedPosts]
ADD CONSTRAINT [FK_UsersReportedPosts]
    FOREIGN KEY ([ReportedBy])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersReportedPosts'
CREATE INDEX [IX_FK_UsersReportedPosts]
ON [dbo].[ReportedPosts]
    ([ReportedBy]);
GO

-- Creating foreign key on [RoleId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_RolesUsers]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RolesUsers'
CREATE INDEX [IX_FK_RolesUsers]
ON [dbo].[Users]
    ([RoleId]);
GO

-- Creating foreign key on [PostIdFacilities] in table 'Facilities'
ALTER TABLE [dbo].[Facilities]
ADD CONSTRAINT [FK_PostsFacilities]
    FOREIGN KEY ([PostIdFacilities])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [PostIdContacts] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [FK_PostsContacts]
    FOREIGN KEY ([PostIdContacts])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [StatusId] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_PostStatusesPosts]
    FOREIGN KEY ([StatusId])
    REFERENCES [dbo].[PostStatuses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostStatusesPosts'
CREATE INDEX [IX_FK_PostStatusesPosts]
ON [dbo].[Posts]
    ([StatusId]);
GO

-- Creating foreign key on [UserId] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_UsersPosts]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersPosts'
CREATE INDEX [IX_FK_UsersPosts]
ON [dbo].[Posts]
    ([UserId]);
GO

-- Creating foreign key on [QuestionId] in table 'Answers'
ALTER TABLE [dbo].[Answers]
ADD CONSTRAINT [FK_QuestionsAnswers]
    FOREIGN KEY ([QuestionId])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionsAnswers'
CREATE INDEX [IX_FK_QuestionsAnswers]
ON [dbo].[Answers]
    ([QuestionId]);
GO

-- Creating foreign key on [FacilityTemplateId] in table 'Facilities'
ALTER TABLE [dbo].[Facilities]
ADD CONSTRAINT [FK_FacilityTemplatesFacilities]
    FOREIGN KEY ([FacilityTemplateId])
    REFERENCES [dbo].[FacilityTemplates]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FacilityTemplatesFacilities'
CREATE INDEX [IX_FK_FacilityTemplatesFacilities]
ON [dbo].[Facilities]
    ([FacilityTemplateId]);
GO

-- Creating foreign key on [TypeId] in table 'BadWords'
ALTER TABLE [dbo].[BadWords]
ADD CONSTRAINT [FK_BadWordTypesBadWords]
    FOREIGN KEY ([TypeId])
    REFERENCES [dbo].[BadWordTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BadWordTypesBadWords'
CREATE INDEX [IX_FK_BadWordTypesBadWords]
ON [dbo].[BadWords]
    ([TypeId]);
GO

-- Creating foreign key on [DistrictId] in table 'Locations'
ALTER TABLE [dbo].[Locations]
ADD CONSTRAINT [FK_DistrictsLocations]
    FOREIGN KEY ([DistrictId])
    REFERENCES [dbo].[Districts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DistrictsLocations'
CREATE INDEX [IX_FK_DistrictsLocations]
ON [dbo].[Locations]
    ([DistrictId]);
GO

-- Creating foreign key on [PostId] in table 'PostLocations'
ALTER TABLE [dbo].[PostLocations]
ADD CONSTRAINT [FK_PostsPostLocations]
    FOREIGN KEY ([PostId])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostsPostLocations'
CREATE INDEX [IX_FK_PostsPostLocations]
ON [dbo].[PostLocations]
    ([PostId]);
GO

-- Creating foreign key on [LocationId] in table 'PostLocations'
ALTER TABLE [dbo].[PostLocations]
ADD CONSTRAINT [FK_LocationsPostLocations]
    FOREIGN KEY ([LocationId])
    REFERENCES [dbo].[Locations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationsPostLocations'
CREATE INDEX [IX_FK_LocationsPostLocations]
ON [dbo].[PostLocations]
    ([LocationId]);
GO

-- Creating foreign key on [PostId] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [FK_PostsQuestions]
    FOREIGN KEY ([PostId])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostsQuestions'
CREATE INDEX [IX_FK_PostsQuestions]
ON [dbo].[Questions]
    ([PostId]);
GO

-- Creating foreign key on [SenderId] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [FK_UsersQuestions]
    FOREIGN KEY ([SenderId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersQuestions'
CREATE INDEX [IX_FK_UsersQuestions]
ON [dbo].[Questions]
    ([SenderId]);
GO

-- Creating foreign key on [UserId] in table 'UserLogs'
ALTER TABLE [dbo].[UserLogs]
ADD CONSTRAINT [FK_UsersUserLogs]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersUserLogs'
CREATE INDEX [IX_FK_UsersUserLogs]
ON [dbo].[UserLogs]
    ([UserId]);
GO

-- Creating foreign key on [PostsId] in table 'Payments'
ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [FK_PostsPayments]
    FOREIGN KEY ([PostsId])
    REFERENCES [dbo].[Posts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostsPayments'
CREATE INDEX [IX_FK_PostsPayments]
ON [dbo].[Payments]
    ([PostsId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------