IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Address] (
    [Id] uniqueidentifier NOT NULL,
    [Line1] nvarchar(max) NOT NULL,
    [Line2] nvarchar(max) NULL,
    [City] nvarchar(max) NOT NULL,
    [State] nvarchar(max) NULL,
    [Country] nvarchar(max) NOT NULL,
    [ZipCode] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Admin] (
    [Id] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [UserName] nvarchar(450) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Email] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Admin] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Product] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Location] (
    [Id] uniqueidentifier NOT NULL,
    [AddressId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Location_Address_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Address] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Customer] (
    [Id] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [UserName] nvarchar(450) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Email] nvarchar(450) NOT NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [AddressId] uniqueidentifier NOT NULL,
    [SignUpDate] datetime2 NOT NULL,
    [BirthDate] datetime2 NOT NULL,
    [DefaultLocationId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Customer] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Customer_Address_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Address] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Customer_Location_DefaultLocationId] FOREIGN KEY ([DefaultLocationId]) REFERENCES [Location] ([Id])
);
GO

CREATE TABLE [InventoryEntry] (
    [Id] uniqueidentifier NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [LocationId] uniqueidentifier NOT NULL,
    [Price] decimal(18,2) NOT NULL,
    [Discount] decimal(3,3) NOT NULL,
    CONSTRAINT [PK_InventoryEntry] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_InventoryEntry_Location_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Location] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_InventoryEntry_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [LocationAdmin] (
    [LocationId] uniqueidentifier NOT NULL,
    [AdminId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_LocationAdmin] PRIMARY KEY ([LocationId], [AdminId]),
    CONSTRAINT [FK_LocationAdmin_Admin_AdminId] FOREIGN KEY ([AdminId]) REFERENCES [Admin] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_LocationAdmin_Location_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Location] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Order] (
    [Id] uniqueidentifier NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [AdminId] uniqueidentifier NOT NULL,
    [LocationId] uniqueidentifier NOT NULL,
    [PlacementDate] datetime2 NOT NULL,
    [AdminId1] uniqueidentifier NULL,
    [LocationId1] uniqueidentifier NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Order_Admin_AdminId] FOREIGN KEY ([AdminId]) REFERENCES [Admin] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Order_Admin_AdminId1] FOREIGN KEY ([AdminId1]) REFERENCES [Admin] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Order_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Order_Location_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Location] ([Id]),
    CONSTRAINT [FK_Order_Location_LocationId1] FOREIGN KEY ([LocationId1]) REFERENCES [Location] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [OrderLine] (
    [Id] uniqueidentifier NOT NULL,
    [OrderId] uniqueidentifier NOT NULL,
    [ProductId] uniqueidentifier NOT NULL,
    [Count] int NOT NULL,
    [PricePerUnit] decimal(18,2) NOT NULL,
    [Discount] decimal(3,3) NOT NULL,
    CONSTRAINT [PK_OrderLine] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderLine_Order_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Order] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderLine_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Admin_Email] ON [Admin] ([Email]);
GO

CREATE UNIQUE INDEX [IX_Admin_UserName] ON [Admin] ([UserName]);
GO

CREATE INDEX [IX_Customer_AddressId] ON [Customer] ([AddressId]);
GO

CREATE INDEX [IX_Customer_DefaultLocationId] ON [Customer] ([DefaultLocationId]);
GO

CREATE UNIQUE INDEX [IX_Customer_Email] ON [Customer] ([Email]);
GO

CREATE UNIQUE INDEX [IX_Customer_UserName] ON [Customer] ([UserName]);
GO

CREATE INDEX [IX_InventoryEntry_LocationId] ON [InventoryEntry] ([LocationId]);
GO

CREATE INDEX [IX_InventoryEntry_ProductId] ON [InventoryEntry] ([ProductId]);
GO

CREATE INDEX [IX_Location_AddressId] ON [Location] ([AddressId]);
GO

CREATE INDEX [IX_LocationAdmin_AdminId] ON [LocationAdmin] ([AdminId]);
GO

CREATE INDEX [IX_Order_AdminId] ON [Order] ([AdminId]);
GO

CREATE INDEX [IX_Order_AdminId1] ON [Order] ([AdminId1]);
GO

CREATE INDEX [IX_Order_CustomerId] ON [Order] ([CustomerId]);
GO

CREATE INDEX [IX_Order_LocationId] ON [Order] ([LocationId]);
GO

CREATE INDEX [IX_Order_LocationId1] ON [Order] ([LocationId1]);
GO

CREATE INDEX [IX_OrderLine_OrderId] ON [OrderLine] ([OrderId]);
GO

CREATE INDEX [IX_OrderLine_ProductId] ON [OrderLine] ([ProductId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201123235935_InitialCreate', N'5.0.0');
GO

COMMIT;
GO

