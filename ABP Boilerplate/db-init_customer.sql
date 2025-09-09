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

IF SCHEMA_ID(N'Customer') IS NULL EXEC(N'CREATE SCHEMA [Customer];');
GO

CREATE TABLE [Customer].[SetupOrigins] (
    [Id] int NOT NULL IDENTITY,
    [CountryName] nvarchar(max) NOT NULL,
    [CountryId] int NOT NULL,
    [CityName] nvarchar(max) NOT NULL,
    [CityId] int NOT NULL,
    [WarehouseName] nvarchar(max) NOT NULL,
    [WarehouseAddress] nvarchar(max) NOT NULL,
    [IsActive] bit NOT NULL,
    [TenantId] int NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_SetupOrigins] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Customer].[SetupSkus] (
    [Id] int NOT NULL IDENTITY,
    [Sku] nvarchar(max) NOT NULL,
    [ProductName] nvarchar(max) NOT NULL,
    [HsCode] nvarchar(max) NOT NULL,
    [QtyPerPack] int NOT NULL,
    [WeightPerPack] float NOT NULL,
    [WeightPerPackUnitId] int NOT NULL,
    [SizePerPackLength] float NOT NULL,
    [SizePerPackLengthUnitId] int NOT NULL,
    [SizePerPackWidth] float NOT NULL,
    [SizePerPackWidthUnitId] int NOT NULL,
    [SizePerPackHeight] float NOT NULL,
    [SizePerPackHeightUnitId] int NOT NULL,
    [QtyPackPerMasterBox] int NOT NULL,
    [MasterBoxWeight] float NOT NULL,
    [MasterBoxWeightUnitId] int NOT NULL,
    [MasterBoxSizeLength] float NOT NULL,
    [MasterBoxSizeLengthUnitId] int NOT NULL,
    [MasterBoxSizeWidth] float NOT NULL,
    [MasterBoxSizeWidthUnitId] int NOT NULL,
    [MasterBoxSizeHeight] float NOT NULL,
    [MasterBoxSizeHeightUnitId] int NOT NULL,
    [MasterBoxSizePerPallet] int NOT NULL,
    [IsActive] bit NOT NULL,
    [TenantId] int NOT NULL,
    [SavedFileName] nvarchar(max) NULL,
    [FileType] nvarchar(max) NULL,
    [FolderName] nvarchar(max) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_SetupSkus] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250519101534_v1', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Customer].[SetupInventories] (
    [Id] int NOT NULL IDENTITY,
    [SkuId] int NOT NULL,
    [OriginId] int NOT NULL,
    [CartonQty] int NOT NULL,
    [MasterCartonQty] int NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_SetupInventories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SetupInventories_SetupOrigins_OriginId] FOREIGN KEY ([OriginId]) REFERENCES [Customer].[SetupOrigins] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SetupInventories_SetupSkus_SkuId] FOREIGN KEY ([SkuId]) REFERENCES [Customer].[SetupSkus] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_SetupInventories_OriginId] ON [Customer].[SetupInventories] ([OriginId]);
GO

CREATE INDEX [IX_SetupInventories_SkuId] ON [Customer].[SetupInventories] ([SkuId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250520095511_v2', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Customer].[SetupInventories] ADD [TenantId] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250520101816_v3', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Customer].[SetupDistributorCenters] (
    [Id] int NOT NULL IDENTITY,
    [RegionId] int NOT NULL,
    [CountryId] int NOT NULL,
    [CityId] int NOT NULL,
    [TenantId] int NOT NULL,
    [IsActive] bit NOT NULL,
    [Name] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_SetupDistributorCenters] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250520123934_v4', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250521094015_v17', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Customer].[Distributions] (
    [Id] int NOT NULL IDENTITY,
    [TenantId] int NOT NULL,
    [DistributionChannelId] int NOT NULL,
    [DistributionChannelEnum] nvarchar(max) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_Distributions] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Customer].[Distributor] (
    [Id] int NOT NULL IDENTITY,
    [DistributorName] nvarchar(max) NULL,
    [CustomDistributionId] nvarchar(max) NULL,
    [DistributorSince] datetime2 NOT NULL,
    [DistributorExpiry] datetime2 NOT NULL,
    [CountryId] int NOT NULL,
    [CityId] int NOT NULL,
    [Address] nvarchar(max) NULL,
    [ZipCode] nvarchar(max) NULL,
    [Zone] nvarchar(max) NULL,
    [EmailAddress] nvarchar(max) NULL,
    [NearestLandmark] nvarchar(max) NULL,
    [Website] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [ContactEmailAddress] nvarchar(max) NULL,
    [ContactDestination] nvarchar(max) NULL,
    [ContactPhoneNumber] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [TenantId] int NOT NULL,
    [DistributionTypeId] int NOT NULL,
    [DistributionTypeEnum] int NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_Distributor] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Customer].[Stores] (
    [Id] int NOT NULL IDENTITY,
    [StoreName] nvarchar(max) NULL,
    [StoreRegistrationId] nvarchar(max) NULL,
    [CountryId] int NOT NULL,
    [CityId] int NOT NULL,
    [Address] nvarchar(max) NULL,
    [ZipCode] nvarchar(max) NULL,
    [Zone] nvarchar(max) NULL,
    [EmailAddress] nvarchar(max) NULL,
    [NearestLandmark] nvarchar(max) NULL,
    [Website] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [ContactEmailAddress] nvarchar(max) NULL,
    [ContactDestination] nvarchar(max) NULL,
    [ContactPhoneNumber] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [TenantId] int NOT NULL,
    [DistributionTypeId] int NOT NULL,
    [DistributionTypeEnum] int NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_Stores] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250521094245_v18', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Customer].[Distributor] DROP CONSTRAINT [PK_Distributor];
GO

EXEC sp_rename N'[Customer].[Distributor]', N'Distributors';
GO

ALTER TABLE [Customer].[Distributors] ADD CONSTRAINT [PK_Distributors] PRIMARY KEY ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250521095418_v19', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Customer].[Stores].[ContactDestination]', N'ContactDesignation', N'COLUMN';
GO

EXEC sp_rename N'[Customer].[Distributors].[ContactDestination]', N'ContactDesignation', N'COLUMN';
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Distributors]') AND [c].[name] = N'Zone');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Distributors] DROP CONSTRAINT [' + @var0 + '];');
UPDATE [Customer].[Distributors] SET [Zone] = N'' WHERE [Zone] IS NULL;
ALTER TABLE [Customer].[Distributors] ALTER COLUMN [Zone] nvarchar(max) NOT NULL;
ALTER TABLE [Customer].[Distributors] ADD DEFAULT N'' FOR [Zone];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Distributors]') AND [c].[name] = N'ZipCode');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Distributors] DROP CONSTRAINT [' + @var1 + '];');
UPDATE [Customer].[Distributors] SET [ZipCode] = N'' WHERE [ZipCode] IS NULL;
ALTER TABLE [Customer].[Distributors] ALTER COLUMN [ZipCode] nvarchar(max) NOT NULL;
ALTER TABLE [Customer].[Distributors] ADD DEFAULT N'' FOR [ZipCode];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Distributors]') AND [c].[name] = N'PhoneNumber');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Distributors] DROP CONSTRAINT [' + @var2 + '];');
UPDATE [Customer].[Distributors] SET [PhoneNumber] = N'' WHERE [PhoneNumber] IS NULL;
ALTER TABLE [Customer].[Distributors] ALTER COLUMN [PhoneNumber] nvarchar(max) NOT NULL;
ALTER TABLE [Customer].[Distributors] ADD DEFAULT N'' FOR [PhoneNumber];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Distributors]') AND [c].[name] = N'EmailAddress');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Distributors] DROP CONSTRAINT [' + @var3 + '];');
UPDATE [Customer].[Distributors] SET [EmailAddress] = N'' WHERE [EmailAddress] IS NULL;
ALTER TABLE [Customer].[Distributors] ALTER COLUMN [EmailAddress] nvarchar(max) NOT NULL;
ALTER TABLE [Customer].[Distributors] ADD DEFAULT N'' FOR [EmailAddress];
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Distributors]') AND [c].[name] = N'DistributorName');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Distributors] DROP CONSTRAINT [' + @var4 + '];');
UPDATE [Customer].[Distributors] SET [DistributorName] = N'' WHERE [DistributorName] IS NULL;
ALTER TABLE [Customer].[Distributors] ALTER COLUMN [DistributorName] nvarchar(max) NOT NULL;
ALTER TABLE [Customer].[Distributors] ADD DEFAULT N'' FOR [DistributorName];
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Distributors]') AND [c].[name] = N'DistributionTypeEnum');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Distributors] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Customer].[Distributors] ALTER COLUMN [DistributionTypeEnum] nvarchar(max) NOT NULL;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Distributors]') AND [c].[name] = N'CustomDistributionId');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Distributors] DROP CONSTRAINT [' + @var6 + '];');
UPDATE [Customer].[Distributors] SET [CustomDistributionId] = N'' WHERE [CustomDistributionId] IS NULL;
ALTER TABLE [Customer].[Distributors] ALTER COLUMN [CustomDistributionId] nvarchar(max) NOT NULL;
ALTER TABLE [Customer].[Distributors] ADD DEFAULT N'' FOR [CustomDistributionId];
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Distributors]') AND [c].[name] = N'Address');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Distributors] DROP CONSTRAINT [' + @var7 + '];');
UPDATE [Customer].[Distributors] SET [Address] = N'' WHERE [Address] IS NULL;
ALTER TABLE [Customer].[Distributors] ALTER COLUMN [Address] nvarchar(max) NOT NULL;
ALTER TABLE [Customer].[Distributors] ADD DEFAULT N'' FOR [Address];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250521124340_v20', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[SetupOrigins]') AND [c].[name] = N'CityName');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[SetupOrigins] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Customer].[SetupOrigins] DROP COLUMN [CityName];
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[SetupOrigins]') AND [c].[name] = N'CountryName');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[SetupOrigins] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Customer].[SetupOrigins] DROP COLUMN [CountryName];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250521132024_vf1', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [Customer].[Distributions];
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Distributors]') AND [c].[name] = N'DistributionTypeEnum');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Distributors] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Customer].[Distributors] DROP COLUMN [DistributionTypeEnum];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250522071503_v21', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Stores]') AND [c].[name] = N'DistributionTypeEnum');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Stores] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Customer].[Stores] DROP COLUMN [DistributionTypeEnum];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250522101417_v22', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [Customer].[SetupDistributorCenters];
GO

DROP TABLE [Customer].[SetupInventories];
GO

DROP TABLE [Customer].[SetupOrigins];
GO

DROP TABLE [Customer].[SetupSkus];
GO

CREATE TABLE [Customer].[DistributorCenters] (
    [Id] int NOT NULL IDENTITY,
    [RegionId] int NOT NULL,
    [CountryId] int NOT NULL,
    [CityId] int NOT NULL,
    [TenantId] int NOT NULL,
    [IsActive] bit NOT NULL,
    [Name] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_DistributorCenters] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Customer].[Origins] (
    [Id] int NOT NULL IDENTITY,
    [CountryId] int NOT NULL,
    [CityId] int NOT NULL,
    [WarehouseName] nvarchar(max) NOT NULL,
    [WarehouseAddress] nvarchar(max) NOT NULL,
    [IsActive] bit NOT NULL,
    [TenantId] int NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_Origins] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Customer].[Skus] (
    [Id] int NOT NULL IDENTITY,
    [SkuCode] nvarchar(max) NOT NULL,
    [ProductName] nvarchar(max) NOT NULL,
    [HsCode] nvarchar(max) NOT NULL,
    [QtyPerPack] int NOT NULL,
    [WeightPerPack] float NOT NULL,
    [WeightPerPackUnitId] int NOT NULL,
    [SizePerPackLength] float NOT NULL,
    [SizePerPackLengthUnitId] int NOT NULL,
    [SizePerPackWidth] float NOT NULL,
    [SizePerPackWidthUnitId] int NOT NULL,
    [SizePerPackHeight] float NOT NULL,
    [SizePerPackHeightUnitId] int NOT NULL,
    [QtyPackPerMasterBox] int NOT NULL,
    [MasterBoxWeight] float NOT NULL,
    [MasterBoxWeightUnitId] int NOT NULL,
    [MasterBoxSizeLength] float NOT NULL,
    [MasterBoxSizeLengthUnitId] int NOT NULL,
    [MasterBoxSizeWidth] float NOT NULL,
    [MasterBoxSizeWidthUnitId] int NOT NULL,
    [MasterBoxSizeHeight] float NOT NULL,
    [MasterBoxSizeHeightUnitId] int NOT NULL,
    [MasterBoxSizePerPallet] int NOT NULL,
    [IsActive] bit NOT NULL,
    [TenantId] int NOT NULL,
    [SavedFileName] nvarchar(max) NULL,
    [FileType] nvarchar(max) NULL,
    [FolderName] nvarchar(max) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_Skus] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Customer].[Inventories] (
    [Id] int NOT NULL IDENTITY,
    [SkuId] int NOT NULL,
    [OriginId] int NOT NULL,
    [CartonQty] int NOT NULL,
    [MasterCartonQty] int NOT NULL,
    [TenantId] int NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_Inventories] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Inventories_Origins_OriginId] FOREIGN KEY ([OriginId]) REFERENCES [Customer].[Origins] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Inventories_Skus_SkuId] FOREIGN KEY ([SkuId]) REFERENCES [Customer].[Skus] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Inventories_OriginId] ON [Customer].[Inventories] ([OriginId]);
GO

CREATE INDEX [IX_Inventories_SkuId] ON [Customer].[Inventories] ([SkuId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250523131147_v23', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Customer].[Replenishments] (
    [Id] int NOT NULL IDENTITY,
    [SkuId] int NOT NULL,
    [StoreId] int NOT NULL,
    [DistributorId] int NOT NULL,
    [DistributorCenterId] int NOT NULL,
    [MaxOrderQty] int NOT NULL,
    [LeadTimeDays] int NOT NULL,
    [AverageDailyUsage] int NOT NULL,
    [SafetyStock] int NOT NULL,
    [MaxStockLevel] int NOT NULL,
    [UnitsPerCarton] int NOT NULL,
    [CartonsPerMasterCarton] int NOT NULL,
    [IsActive] bit NOT NULL,
    [TenantId] int NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_Replenishments] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250526073817_v24', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Replenishments]') AND [c].[name] = N'StoreId');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Replenishments] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [Customer].[Replenishments] ALTER COLUMN [StoreId] int NULL;
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Replenishments]') AND [c].[name] = N'DistributorId');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Replenishments] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [Customer].[Replenishments] ALTER COLUMN [DistributorId] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250526092305_v25', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Skus]') AND [c].[name] = N'FileType');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Skus] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [Customer].[Skus] DROP COLUMN [FileType];
GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Skus]') AND [c].[name] = N'FolderName');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Skus] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [Customer].[Skus] DROP COLUMN [FolderName];
GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Skus]') AND [c].[name] = N'SavedFileName');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Skus] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [Customer].[Skus] DROP COLUMN [SavedFileName];
GO

CREATE TABLE [Customer].[SkuDocuments] (
    [Id] int NOT NULL IDENTITY,
    [OriginalFileName] nvarchar(max) NULL,
    [SavedFileName] nvarchar(max) NULL,
    [FilePath] nvarchar(max) NULL,
    [FileType] nvarchar(max) NULL,
    [FolderName] nvarchar(max) NULL,
    [FileSize] bigint NOT NULL,
    [SkuId] int NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_SkuDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SkuDocuments_Skus_SkuId] FOREIGN KEY ([SkuId]) REFERENCES [Customer].[Skus] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_SkuDocuments_SkuId] ON [Customer].[SkuDocuments] ([SkuId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250527141047_v26', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Skus]') AND [c].[name] = N'QtyPackPerMasterBox');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Skus] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [Customer].[Skus] DROP COLUMN [QtyPackPerMasterBox];
GO

EXEC sp_rename N'[Customer].[Skus].[WeightPerPackUnitId]', N'WeightPerCartonUnitId', N'COLUMN';
GO

EXEC sp_rename N'[Customer].[Skus].[WeightPerPack]', N'WeightPerCarton', N'COLUMN';
GO

EXEC sp_rename N'[Customer].[Skus].[SizePerPackWidthUnitId]', N'SizePerCartonWidthUnitId', N'COLUMN';
GO

EXEC sp_rename N'[Customer].[Skus].[SizePerPackWidth]', N'SizePerCartonWidth', N'COLUMN';
GO

EXEC sp_rename N'[Customer].[Skus].[SizePerPackLengthUnitId]', N'SizePerCartonLengthUnitId', N'COLUMN';
GO

EXEC sp_rename N'[Customer].[Skus].[SizePerPackLength]', N'SizePerCartonLength', N'COLUMN';
GO

EXEC sp_rename N'[Customer].[Skus].[SizePerPackHeightUnitId]', N'SizePerCartonHeightUnitId', N'COLUMN';
GO

EXEC sp_rename N'[Customer].[Skus].[SizePerPackHeight]', N'SizePerCartonHeight', N'COLUMN';
GO

EXEC sp_rename N'[Customer].[Skus].[QtyPerPack]', N'CartonQtyPerMasterBox', N'COLUMN';
GO

ALTER TABLE [Customer].[Skus] ADD [IsCartonMasterBox] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250603112713_v27', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[SkuDocuments]') AND [c].[name] = N'FolderName');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[SkuDocuments] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [Customer].[SkuDocuments] DROP COLUMN [FolderName];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250604063628_v28', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Inventories]') AND [c].[name] = N'MasterCartonQty');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Inventories] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [Customer].[Inventories] DROP COLUMN [MasterCartonQty];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250604065951_v29', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var20 sysname;
SELECT @var20 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Stores]') AND [c].[name] = N'DistributionTypeId');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Stores] DROP CONSTRAINT [' + @var20 + '];');
ALTER TABLE [Customer].[Stores] DROP COLUMN [DistributionTypeId];
GO

DECLARE @var21 sysname;
SELECT @var21 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Distributors]') AND [c].[name] = N'DistributionTypeId');
IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Distributors] DROP CONSTRAINT [' + @var21 + '];');
ALTER TABLE [Customer].[Distributors] DROP COLUMN [DistributionTypeId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250611081839_v30', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Customer].[Origins] ADD [Latitude] float NOT NULL DEFAULT 0.0E0;
GO

ALTER TABLE [Customer].[Origins] ADD [Longitude] float NOT NULL DEFAULT 0.0E0;
GO

ALTER TABLE [Customer].[Origins] ADD [PlusCodes] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250616071035_v31', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var22 sysname;
SELECT @var22 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Origins]') AND [c].[name] = N'Longitude');
IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Origins] DROP CONSTRAINT [' + @var22 + '];');
ALTER TABLE [Customer].[Origins] ALTER COLUMN [Longitude] float NULL;
GO

DECLARE @var23 sysname;
SELECT @var23 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Origins]') AND [c].[name] = N'Latitude');
IF @var23 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Origins] DROP CONSTRAINT [' + @var23 + '];');
ALTER TABLE [Customer].[Origins] ALTER COLUMN [Latitude] float NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250616071257_v32', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Customer].[DistributorCenters] ADD [Latitude] float NULL;
GO

ALTER TABLE [Customer].[DistributorCenters] ADD [Longitude] float NULL;
GO

ALTER TABLE [Customer].[DistributorCenters] ADD [PlusCodes] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250616072731_v33', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var24 sysname;
SELECT @var24 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[DistributorCenters]') AND [c].[name] = N'Address');
IF @var24 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[DistributorCenters] DROP CONSTRAINT [' + @var24 + '];');
ALTER TABLE [Customer].[DistributorCenters] DROP COLUMN [Address];
GO

DECLARE @var25 sysname;
SELECT @var25 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[DistributorCenters]') AND [c].[name] = N'CityId');
IF @var25 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[DistributorCenters] DROP CONSTRAINT [' + @var25 + '];');
ALTER TABLE [Customer].[DistributorCenters] DROP COLUMN [CityId];
GO

DECLARE @var26 sysname;
SELECT @var26 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[DistributorCenters]') AND [c].[name] = N'CountryId');
IF @var26 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[DistributorCenters] DROP CONSTRAINT [' + @var26 + '];');
ALTER TABLE [Customer].[DistributorCenters] DROP COLUMN [CountryId];
GO

DECLARE @var27 sysname;
SELECT @var27 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[DistributorCenters]') AND [c].[name] = N'IsActive');
IF @var27 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[DistributorCenters] DROP CONSTRAINT [' + @var27 + '];');
ALTER TABLE [Customer].[DistributorCenters] DROP COLUMN [IsActive];
GO

DECLARE @var28 sysname;
SELECT @var28 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[DistributorCenters]') AND [c].[name] = N'Latitude');
IF @var28 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[DistributorCenters] DROP CONSTRAINT [' + @var28 + '];');
ALTER TABLE [Customer].[DistributorCenters] DROP COLUMN [Latitude];
GO

DECLARE @var29 sysname;
SELECT @var29 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[DistributorCenters]') AND [c].[name] = N'Longitude');
IF @var29 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[DistributorCenters] DROP CONSTRAINT [' + @var29 + '];');
ALTER TABLE [Customer].[DistributorCenters] DROP COLUMN [Longitude];
GO

DECLARE @var30 sysname;
SELECT @var30 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[DistributorCenters]') AND [c].[name] = N'Name');
IF @var30 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[DistributorCenters] DROP CONSTRAINT [' + @var30 + '];');
ALTER TABLE [Customer].[DistributorCenters] DROP COLUMN [Name];
GO

EXEC sp_rename N'[Customer].[DistributorCenters].[RegionId]', N'RecordId', N'COLUMN';
GO

EXEC sp_rename N'[Customer].[DistributorCenters].[PlusCodes]', N'Enum', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250616133503_v34', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [Customer].[DistributorCenters];
GO

CREATE TABLE [Customer].[DistributionCenters] (
    [Id] int NOT NULL IDENTITY,
    [RecordId] int NOT NULL,
    [TenantId] int NOT NULL,
    [Enum] nvarchar(max) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_DistributionCenters] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250617061839_v35', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var31 sysname;
SELECT @var31 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Origins]') AND [c].[name] = N'Longitude');
IF @var31 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Origins] DROP CONSTRAINT [' + @var31 + '];');
ALTER TABLE [Customer].[Origins] ALTER COLUMN [Longitude] nvarchar(max) NULL;
GO

DECLARE @var32 sysname;
SELECT @var32 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Customer].[Origins]') AND [c].[name] = N'Latitude');
IF @var32 IS NOT NULL EXEC(N'ALTER TABLE [Customer].[Origins] DROP CONSTRAINT [' + @var32 + '];');
ALTER TABLE [Customer].[Origins] ALTER COLUMN [Latitude] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250617134135_v36', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Customer].[Replenishments] ADD [DistributorCenterEnum] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250618100140_v37', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Customer].[DistributorDocuments] (
    [Id] int NOT NULL IDENTITY,
    [OriginalFileName] nvarchar(max) NULL,
    [SavedFileName] nvarchar(max) NULL,
    [FilePath] nvarchar(max) NULL,
    [FileType] nvarchar(max) NULL,
    [FileSize] bigint NOT NULL,
    [DistributorId] int NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_DistributorDocuments] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250623085221_v38', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [Customer].[DistributorDocuments];
GO

CREATE TABLE [Customer].[DistributorImages] (
    [Id] int NOT NULL IDENTITY,
    [OriginalFileName] nvarchar(max) NULL,
    [SavedFileName] nvarchar(max) NULL,
    [FilePath] nvarchar(max) NULL,
    [FileType] nvarchar(max) NULL,
    [FileSize] bigint NOT NULL,
    [DistributorId] int NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_DistributorImages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DistributorImages_Distributors_DistributorId] FOREIGN KEY ([DistributorId]) REFERENCES [Customer].[Distributors] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_DistributorImages_DistributorId] ON [Customer].[DistributorImages] ([DistributorId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250623085721_v39', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Customer].[StoreImages] (
    [Id] int NOT NULL IDENTITY,
    [OriginalFileName] nvarchar(max) NULL,
    [SavedFileName] nvarchar(max) NULL,
    [FilePath] nvarchar(max) NULL,
    [FileType] nvarchar(max) NULL,
    [FileSize] bigint NOT NULL,
    [StoreId] int NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_StoreImages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_StoreImages_Stores_StoreId] FOREIGN KEY ([StoreId]) REFERENCES [Customer].[Stores] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_StoreImages_StoreId] ON [Customer].[StoreImages] ([StoreId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250623092448_v40', N'8.0.5');
GO

COMMIT;
GO

