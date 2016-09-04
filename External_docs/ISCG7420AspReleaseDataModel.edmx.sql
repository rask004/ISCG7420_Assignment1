
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/25/2016 14:20:35
-- Generated from EDMX file: C:\Users\AskewR04\AppData\Local\Temp\VWDWebCache\ftp_dochyper.unitec.ac.nz_asp_assignment_AskewR04\App_Code\ISCG7420AspReleaseDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ASKEWR04sqlserver3];
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

-- Creating table 'Caps'
CREATE TABLE [dbo].[Caps] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(40)  NOT NULL,
    [price] decimal(3,2)  NOT NULL,
    [description] nvarchar(512)  NOT NULL,
    [imageUrl] nvarchar(96)  NOT NULL,
    [supplierId] int  NOT NULL,
    [categoryId] int  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(40)  NOT NULL
);
GO

-- Creating table 'Colours'
CREATE TABLE [dbo].[Colours] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(24)  NOT NULL
);
GO

-- Creating table 'CustomerOrders'
CREATE TABLE [dbo].[CustomerOrders] (
    [id] int IDENTITY(1,1) NOT NULL,
    [userId] int  NOT NULL,
    [status] nvarchar(7)  NOT NULL
);
GO

-- Creating table 'OrderItems'
CREATE TABLE [dbo].[OrderItems] (
    [orderId] int  NOT NULL,
    [capId] int  NOT NULL,
    [colourId] int  NOT NULL,
    [quantity] int  NOT NULL
);
GO

-- Creating table 'SiteUsers'
CREATE TABLE [dbo].[SiteUsers] (
    [id] int IDENTITY(1,1) NOT NULL,
    [login] nvarchar(64)  NOT NULL,
    [password] nvarchar(64)  NOT NULL,
    [userType] char(1)  NOT NULL,
    [emailAddress] nvarchar(100)  NOT NULL,
    [homeNumber] nvarchar(11)  NULL,
    [workNumber] nvarchar(11)  NULL,
    [mobileNumber] nvarchar(13)  NULL,
    [firstName] nvarchar(32)  NULL,
    [lastName] nvarchar(32)  NULL,
    [streetAddress] nvarchar(64)  NULL,
    [suburb] nvarchar(24)  NULL,
    [city] nvarchar(16)  NULL,
    [isDisabled] bit  NULL
);
GO

-- Creating table 'Suppliers'
CREATE TABLE [dbo].[Suppliers] (
    [id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(32)  NOT NULL,
    [contactNumber] nvarchar(11)  NOT NULL,
    [emailAddress] nvarchar(64)  NOT NULL
);
GO

-- Creating table 'CapColour'
CREATE TABLE [dbo].[CapColour] (
    [Caps_id] int  NOT NULL,
    [Colours_id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Caps'
ALTER TABLE [dbo].[Caps]
ADD CONSTRAINT [PK_Caps]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Colours'
ALTER TABLE [dbo].[Colours]
ADD CONSTRAINT [PK_Colours]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'CustomerOrders'
ALTER TABLE [dbo].[CustomerOrders]
ADD CONSTRAINT [PK_CustomerOrders]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [orderId], [capId], [colourId] in table 'OrderItems'
ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [PK_OrderItems]
    PRIMARY KEY CLUSTERED ([orderId], [capId], [colourId] ASC);
GO

-- Creating primary key on [id] in table 'SiteUsers'
ALTER TABLE [dbo].[SiteUsers]
ADD CONSTRAINT [PK_SiteUsers]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Suppliers'
ALTER TABLE [dbo].[Suppliers]
ADD CONSTRAINT [PK_Suppliers]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [Caps_id], [Colours_id] in table 'CapColour'
ALTER TABLE [dbo].[CapColour]
ADD CONSTRAINT [PK_CapColour]
    PRIMARY KEY CLUSTERED ([Caps_id], [Colours_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [categoryId] in table 'Caps'
ALTER TABLE [dbo].[Caps]
ADD CONSTRAINT [FK__Cap__categoryId__37A5467C]
    FOREIGN KEY ([categoryId])
    REFERENCES [dbo].[Categories]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Cap__categoryId__37A5467C'
CREATE INDEX [IX_FK__Cap__categoryId__37A5467C]
ON [dbo].[Caps]
    ([categoryId]);
GO

-- Creating foreign key on [supplierId] in table 'Caps'
ALTER TABLE [dbo].[Caps]
ADD CONSTRAINT [FK__Cap__supplierId__36B12243]
    FOREIGN KEY ([supplierId])
    REFERENCES [dbo].[Suppliers]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Cap__supplierId__36B12243'
CREATE INDEX [IX_FK__Cap__supplierId__36B12243]
ON [dbo].[Caps]
    ([supplierId]);
GO

-- Creating foreign key on [capId] in table 'OrderItems'
ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [FK__OrderItem__capId__3F466844]
    FOREIGN KEY ([capId])
    REFERENCES [dbo].[Caps]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__OrderItem__capId__3F466844'
CREATE INDEX [IX_FK__OrderItem__capId__3F466844]
ON [dbo].[OrderItems]
    ([capId]);
GO

-- Creating foreign key on [Caps_id] in table 'CapColour'
ALTER TABLE [dbo].[CapColour]
ADD CONSTRAINT [FK_CapColour_Cap]
    FOREIGN KEY ([Caps_id])
    REFERENCES [dbo].[Caps]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Colours_id] in table 'CapColour'
ALTER TABLE [dbo].[CapColour]
ADD CONSTRAINT [FK_CapColour_Colour]
    FOREIGN KEY ([Colours_id])
    REFERENCES [dbo].[Colours]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CapColour_Colour'
CREATE INDEX [IX_FK_CapColour_Colour]
ON [dbo].[CapColour]
    ([Colours_id]);
GO

-- Creating foreign key on [colourId] in table 'OrderItems'
ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [FK__OrderItem__colou__403A8C7D]
    FOREIGN KEY ([colourId])
    REFERENCES [dbo].[Colours]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__OrderItem__colou__403A8C7D'
CREATE INDEX [IX_FK__OrderItem__colou__403A8C7D]
ON [dbo].[OrderItems]
    ([colourId]);
GO

-- Creating foreign key on [userId] in table 'CustomerOrders'
ALTER TABLE [dbo].[CustomerOrders]
ADD CONSTRAINT [FK__CustomerO__userI__1367E606]
    FOREIGN KEY ([userId])
    REFERENCES [dbo].[SiteUsers]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CustomerO__userI__1367E606'
CREATE INDEX [IX_FK__CustomerO__userI__1367E606]
ON [dbo].[CustomerOrders]
    ([userId]);
GO

-- Creating foreign key on [orderId] in table 'OrderItems'
ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [FK__OrderItem__order__3E52440B]
    FOREIGN KEY ([orderId])
    REFERENCES [dbo].[CustomerOrders]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------