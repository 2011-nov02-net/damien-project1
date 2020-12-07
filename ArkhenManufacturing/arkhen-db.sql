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

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
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
    [Name] nvarchar(max) NULL,
    [AddressId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Location] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Location_Address_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Address] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Admin] (
    [Id] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [Email] nvarchar(450) NOT NULL,
    [LocationId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Admin] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Admin_Location_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Location] ([Id])
);
GO

CREATE TABLE [Customer] (
    [Id] uniqueidentifier NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
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
    [Count] int NOT NULL,
    [Threshold] int NOT NULL,
    CONSTRAINT [PK_InventoryEntry] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_InventoryEntry_Location_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Location] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_InventoryEntry_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Order] (
    [Id] uniqueidentifier NOT NULL,
    [CustomerId] uniqueidentifier NOT NULL,
    [AdminId] uniqueidentifier NOT NULL,
    [LocationId] uniqueidentifier NOT NULL,
    [PlacementDate] datetime2 NOT NULL,
    [DbAdminId] uniqueidentifier NULL,
    [DbLocationId] uniqueidentifier NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Order_Admin_AdminId] FOREIGN KEY ([AdminId]) REFERENCES [Admin] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Order_Admin_DbAdminId] FOREIGN KEY ([DbAdminId]) REFERENCES [Admin] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Order_Customer_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Order_Location_DbLocationId] FOREIGN KEY ([DbLocationId]) REFERENCES [Location] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Order_Location_LocationId] FOREIGN KEY ([LocationId]) REFERENCES [Location] ([Id])
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

CREATE INDEX [IX_Admin_LocationId] ON [Admin] ([LocationId]);
GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

CREATE INDEX [IX_Customer_AddressId] ON [Customer] ([AddressId]);
GO

CREATE INDEX [IX_Customer_DefaultLocationId] ON [Customer] ([DefaultLocationId]);
GO

CREATE UNIQUE INDEX [IX_Customer_Email] ON [Customer] ([Email]);
GO

CREATE INDEX [IX_InventoryEntry_LocationId] ON [InventoryEntry] ([LocationId]);
GO

CREATE INDEX [IX_InventoryEntry_ProductId] ON [InventoryEntry] ([ProductId]);
GO

CREATE INDEX [IX_Location_AddressId] ON [Location] ([AddressId]);
GO

CREATE INDEX [IX_Order_AdminId] ON [Order] ([AdminId]);
GO

CREATE INDEX [IX_Order_CustomerId] ON [Order] ([CustomerId]);
GO

CREATE INDEX [IX_Order_DbAdminId] ON [Order] ([DbAdminId]);
GO

CREATE INDEX [IX_Order_DbLocationId] ON [Order] ([DbLocationId]);
GO

CREATE INDEX [IX_Order_LocationId] ON [Order] ([LocationId]);
GO

CREATE INDEX [IX_OrderLine_OrderId] ON [OrderLine] ([OrderId]);
GO

CREATE INDEX [IX_OrderLine_ProductId] ON [OrderLine] ([ProductId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201205065505_AddIdentity', N'5.0.0');
GO

COMMIT;
GO

