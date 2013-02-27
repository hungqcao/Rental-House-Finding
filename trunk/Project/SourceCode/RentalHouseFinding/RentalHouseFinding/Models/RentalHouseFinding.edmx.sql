
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 02/27/2013 14:20:23
-- Generated from EDMX file: C:\Users\MrBlackRose\documents\visual studio 2010\Projects\RentalHouseFinding\RentalHouseFinding\Models\RentalHouseFinding.edmx
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


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(20)  NOT NULL,
    [Password] nvarchar(20)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [PhoneNumber] nvarchar(15)  NULL,
    [Address] nvarchar(max)  NULL,
    [Name] nvarchar(max)  NULL,
    [DOB] datetime  NULL,
    [Avatar] nvarchar(max)  NULL,
    [CreatedDate] datetime  NOT NULL,
    [LastUpdate] datetime  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [OpenIdURL] nvarchar(max)  NOT NULL,
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
    [UserId] int  NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [CategoryId] int  NOT NULL,
    [StatusId] int  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [Views] int  NOT NULL,
    [CreatedDate] datetime  NOT NULL,
    [EditedDate] datetime  NOT NULL,
    [RenewDate] datetime  NULL,
    [DistrictId] int  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Price] float  NOT NULL,
    [Area] float  NOT NULL,
    [Contact] nvarchar(max)  NULL,
    [Phone] nvarchar(20)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Lat] float  NOT NULL,
    [Lon] float  NOT NULL,
    [WaterFee] float  NULL,
    [ElectricityFee] float  NULL,
    [InternetFee] float  NULL,
    [IsStayWithOwner] bit  NULL,
    [RestrictHours] int  NULL,
    [IsParkingLot] bit  NULL,
    [ParkingFee] float  NULL,
    [IsAllowCooking] bit  NULL,
    [VideoLink] nvarchar(max)  NULL,
    [Direction] nvarchar(max)  NULL,
    [District_Id] int  NOT NULL,
    [Category_Id] int  NOT NULL
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

-- Creating table 'PostStatus'
CREATE TABLE [dbo].[PostStatus] (
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
    [IsDeleted] bit  NOT NULL,
    [User_Id] int  NOT NULL
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
    [Word] nvarchar(max)  NOT NULL
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

-- Creating primary key on [Id] in table 'PostStatus'
ALTER TABLE [dbo].[PostStatus]
ADD CONSTRAINT [PK_PostStatus]
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

-- Creating foreign key on [District_Id] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_DistrictsPosts]
    FOREIGN KEY ([District_Id])
    REFERENCES [dbo].[Districts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DistrictsPosts'
CREATE INDEX [IX_FK_DistrictsPosts]
ON [dbo].[Posts]
    ([District_Id]);
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

-- Creating foreign key on [Category_Id] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_CategoriesPosts]
    FOREIGN KEY ([Category_Id])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoriesPosts'
CREATE INDEX [IX_FK_CategoriesPosts]
ON [dbo].[Posts]
    ([Category_Id]);
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

-- Creating foreign key on [StatusId] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_PostStatusPosts]
    FOREIGN KEY ([StatusId])
    REFERENCES [dbo].[PostStatus]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PostStatusPosts'
CREATE INDEX [IX_FK_PostStatusPosts]
ON [dbo].[Posts]
    ([StatusId]);
GO

-- Creating foreign key on [User_Id] in table 'Favorites'
ALTER TABLE [dbo].[Favorites]
ADD CONSTRAINT [FK_UsersFavorites]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UsersFavorites'
CREATE INDEX [IX_FK_UsersFavorites]
ON [dbo].[Favorites]
    ([User_Id]);
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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------