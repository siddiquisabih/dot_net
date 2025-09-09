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

CREATE TABLE [AbpAuditLogs] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantId] int NULL,
    [UserId] bigint NULL,
    [ServiceName] nvarchar(256) NULL,
    [MethodName] nvarchar(256) NULL,
    [Parameters] nvarchar(1024) NULL,
    [ReturnValue] nvarchar(max) NULL,
    [ExecutionTime] datetime2 NOT NULL,
    [ExecutionDuration] int NOT NULL,
    [ClientIpAddress] nvarchar(64) NULL,
    [ClientName] nvarchar(128) NULL,
    [BrowserInfo] nvarchar(512) NULL,
    [ExceptionMessage] nvarchar(1024) NULL,
    [Exception] nvarchar(2000) NULL,
    [ImpersonatorUserId] bigint NULL,
    [ImpersonatorTenantId] int NULL,
    [CustomData] nvarchar(2000) NULL,
    CONSTRAINT [PK_AbpAuditLogs] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AbpBackgroundJobs] (
    [Id] bigint NOT NULL IDENTITY,
    [JobType] nvarchar(512) NOT NULL,
    [JobArgs] nvarchar(max) NOT NULL,
    [TryCount] smallint NOT NULL,
    [NextTryTime] datetime2 NOT NULL,
    [LastTryTime] datetime2 NULL,
    [IsAbandoned] bit NOT NULL,
    [Priority] tinyint NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    CONSTRAINT [PK_AbpBackgroundJobs] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AbpDynamicProperties] (
    [Id] int NOT NULL IDENTITY,
    [PropertyName] nvarchar(256) NULL,
    [DisplayName] nvarchar(max) NULL,
    [InputType] nvarchar(max) NULL,
    [Permission] nvarchar(max) NULL,
    [TenantId] int NULL,
    CONSTRAINT [PK_AbpDynamicProperties] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AbpEditions] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(32) NOT NULL,
    [DisplayName] nvarchar(64) NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_AbpEditions] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AbpEntityChangeSets] (
    [Id] bigint NOT NULL IDENTITY,
    [BrowserInfo] nvarchar(512) NULL,
    [ClientIpAddress] nvarchar(64) NULL,
    [ClientName] nvarchar(128) NULL,
    [CreationTime] datetime2 NOT NULL,
    [ExtensionData] nvarchar(max) NULL,
    [ImpersonatorTenantId] int NULL,
    [ImpersonatorUserId] bigint NULL,
    [Reason] nvarchar(256) NULL,
    [TenantId] int NULL,
    [UserId] bigint NULL,
    CONSTRAINT [PK_AbpEntityChangeSets] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AbpLanguages] (
    [Id] int NOT NULL IDENTITY,
    [TenantId] int NULL,
    [Name] nvarchar(128) NOT NULL,
    [DisplayName] nvarchar(64) NOT NULL,
    [Icon] nvarchar(128) NULL,
    [IsDisabled] bit NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_AbpLanguages] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AbpLanguageTexts] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantId] int NULL,
    [LanguageName] nvarchar(128) NOT NULL,
    [Source] nvarchar(128) NOT NULL,
    [Key] nvarchar(256) NOT NULL,
    [Value] nvarchar(max) NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    CONSTRAINT [PK_AbpLanguageTexts] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AbpNotifications] (
    [Id] uniqueidentifier NOT NULL,
    [NotificationName] nvarchar(96) NOT NULL,
    [Data] nvarchar(max) NULL,
    [DataTypeName] nvarchar(512) NULL,
    [EntityTypeName] nvarchar(250) NULL,
    [EntityTypeAssemblyQualifiedName] nvarchar(512) NULL,
    [EntityId] nvarchar(96) NULL,
    [Severity] tinyint NOT NULL,
    [UserIds] nvarchar(max) NULL,
    [ExcludedUserIds] nvarchar(max) NULL,
    [TenantIds] nvarchar(max) NULL,
    [TargetNotifiers] nvarchar(1024) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    CONSTRAINT [PK_AbpNotifications] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AbpNotificationSubscriptions] (
    [Id] uniqueidentifier NOT NULL,
    [TenantId] int NULL,
    [UserId] bigint NOT NULL,
    [NotificationName] nvarchar(96) NULL,
    [EntityTypeName] nvarchar(250) NULL,
    [EntityTypeAssemblyQualifiedName] nvarchar(512) NULL,
    [EntityId] nvarchar(96) NULL,
    [TargetNotifiers] nvarchar(1024) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    CONSTRAINT [PK_AbpNotificationSubscriptions] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AbpOrganizationUnitRoles] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantId] int NULL,
    [RoleId] int NOT NULL,
    [OrganizationUnitId] bigint NOT NULL,
    [IsDeleted] bit NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    CONSTRAINT [PK_AbpOrganizationUnitRoles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AbpOrganizationUnits] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantId] int NULL,
    [ParentId] bigint NULL,
    [Code] nvarchar(95) NOT NULL,
    [DisplayName] nvarchar(128) NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_AbpOrganizationUnits] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AbpOrganizationUnits_AbpOrganizationUnits_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [AbpOrganizationUnits] ([Id])
);
GO

CREATE TABLE [AbpTenantNotifications] (
    [Id] uniqueidentifier NOT NULL,
    [TenantId] int NULL,
    [NotificationName] nvarchar(96) NOT NULL,
    [Data] nvarchar(max) NULL,
    [DataTypeName] nvarchar(512) NULL,
    [EntityTypeName] nvarchar(250) NULL,
    [EntityTypeAssemblyQualifiedName] nvarchar(512) NULL,
    [EntityId] nvarchar(96) NULL,
    [Severity] tinyint NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    CONSTRAINT [PK_AbpTenantNotifications] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AbpUserAccounts] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantId] int NULL,
    [UserId] bigint NOT NULL,
    [UserLinkId] bigint NULL,
    [UserName] nvarchar(256) NULL,
    [EmailAddress] nvarchar(256) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_AbpUserAccounts] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AbpUserLoginAttempts] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantId] int NULL,
    [TenancyName] nvarchar(64) NULL,
    [UserId] bigint NULL,
    [UserNameOrEmailAddress] nvarchar(256) NULL,
    [ClientIpAddress] nvarchar(64) NULL,
    [ClientName] nvarchar(128) NULL,
    [BrowserInfo] nvarchar(512) NULL,
    [Result] tinyint NOT NULL,
    [FailReason] nvarchar(1024) NULL,
    [CreationTime] datetime2 NOT NULL,
    CONSTRAINT [PK_AbpUserLoginAttempts] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AbpUserNotifications] (
    [Id] uniqueidentifier NOT NULL,
    [TenantId] int NULL,
    [UserId] bigint NOT NULL,
    [TenantNotificationId] uniqueidentifier NOT NULL,
    [State] int NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [TargetNotifiers] nvarchar(1024) NULL,
    CONSTRAINT [PK_AbpUserNotifications] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AbpUserOrganizationUnits] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantId] int NULL,
    [UserId] bigint NOT NULL,
    [OrganizationUnitId] bigint NOT NULL,
    [IsDeleted] bit NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    CONSTRAINT [PK_AbpUserOrganizationUnits] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AbpUsers] (
    [Id] bigint NOT NULL IDENTITY,
    [NIDNumber] nvarchar(max) NULL,
    [Rank] nvarchar(max) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    [AuthenticationSource] nvarchar(64) NULL,
    [UserName] nvarchar(256) NOT NULL,
    [TenantId] int NULL,
    [EmailAddress] nvarchar(256) NOT NULL,
    [Name] nvarchar(64) NOT NULL,
    [Surname] nvarchar(64) NOT NULL,
    [Password] nvarchar(128) NOT NULL,
    [EmailConfirmationCode] nvarchar(328) NULL,
    [PasswordResetCode] nvarchar(328) NULL,
    [LockoutEndDateUtc] datetime2 NULL,
    [AccessFailedCount] int NOT NULL,
    [IsLockoutEnabled] bit NOT NULL,
    [PhoneNumber] nvarchar(32) NULL,
    [IsPhoneNumberConfirmed] bit NOT NULL,
    [SecurityStamp] nvarchar(128) NULL,
    [IsTwoFactorEnabled] bit NOT NULL,
    [IsEmailConfirmed] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [NormalizedUserName] nvarchar(256) NOT NULL,
    [NormalizedEmailAddress] nvarchar(256) NOT NULL,
    [ConcurrencyStamp] nvarchar(128) NULL,
    CONSTRAINT [PK_AbpUsers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AbpUsers_AbpUsers_CreatorUserId] FOREIGN KEY ([CreatorUserId]) REFERENCES [AbpUsers] ([Id]),
    CONSTRAINT [FK_AbpUsers_AbpUsers_DeleterUserId] FOREIGN KEY ([DeleterUserId]) REFERENCES [AbpUsers] ([Id]),
    CONSTRAINT [FK_AbpUsers_AbpUsers_LastModifierUserId] FOREIGN KEY ([LastModifierUserId]) REFERENCES [AbpUsers] ([Id])
);
GO

CREATE TABLE [AbpWebhookEvents] (
    [Id] uniqueidentifier NOT NULL,
    [WebhookName] nvarchar(max) NOT NULL,
    [Data] nvarchar(max) NULL,
    [CreationTime] datetime2 NOT NULL,
    [TenantId] int NULL,
    [IsDeleted] bit NOT NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_AbpWebhookEvents] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AbpWebhookSubscriptions] (
    [Id] uniqueidentifier NOT NULL,
    [TenantId] int NULL,
    [WebhookUri] nvarchar(max) NOT NULL,
    [Secret] nvarchar(max) NOT NULL,
    [IsActive] bit NOT NULL,
    [Webhooks] nvarchar(max) NULL,
    [Headers] nvarchar(max) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    CONSTRAINT [PK_AbpWebhookSubscriptions] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Attachments] (
    [Id] int NOT NULL IDENTITY,
    [EntityId] int NOT NULL,
    [AttachmentPath] nvarchar(max) NULL,
    [AttachmentName] nvarchar(max) NULL,
    [AttachmentType] nvarchar(max) NULL,
    [FileType] nvarchar(max) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_Attachments] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AuditTypes] (
    [Id] int NOT NULL IDENTITY,
    [AuditTypeCode] nvarchar(max) NULL,
    [AuditTypeName] nvarchar(max) NULL,
    [CreatorUserId] bigint NULL,
    [CreationTime] datetime2 NOT NULL,
    [LastModifierUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_AuditTypes] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [CustomerInformations] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [Email] nvarchar(max) NULL,
    [PoBox] nvarchar(max) NULL,
    [RegistrationType] nvarchar(max) NULL,
    [RegistrationCode] nvarchar(max) NULL,
    [RegistrationTypeId] int NOT NULL,
    [CompanyName] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    [Website] nvarchar(max) NULL,
    [LinkedinProfile] nvarchar(max) NULL,
    [InstagramProfile] nvarchar(max) NULL,
    [RegionId] int NOT NULL,
    [CountryId] int NOT NULL,
    [StateId] int NOT NULL,
    [CityId] int NOT NULL,
    [TenantId] int NOT NULL,
    CONSTRAINT [PK_CustomerInformations] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [NotificationGroups] (
    [Id] int NOT NULL IDENTITY,
    [GroupName] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [CreatorUserId] bigint NULL,
    [CreationTime] datetime2 NOT NULL,
    [LastModifierUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_NotificationGroups] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Regions] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Regions] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [AbpDynamicEntityProperties] (
    [Id] int NOT NULL IDENTITY,
    [EntityFullName] nvarchar(256) NULL,
    [DynamicPropertyId] int NOT NULL,
    [TenantId] int NULL,
    CONSTRAINT [PK_AbpDynamicEntityProperties] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AbpDynamicEntityProperties_AbpDynamicProperties_DynamicPropertyId] FOREIGN KEY ([DynamicPropertyId]) REFERENCES [AbpDynamicProperties] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AbpDynamicPropertyValues] (
    [Id] bigint NOT NULL IDENTITY,
    [Value] nvarchar(max) NOT NULL,
    [TenantId] int NULL,
    [DynamicPropertyId] int NOT NULL,
    CONSTRAINT [PK_AbpDynamicPropertyValues] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AbpDynamicPropertyValues_AbpDynamicProperties_DynamicPropertyId] FOREIGN KEY ([DynamicPropertyId]) REFERENCES [AbpDynamicProperties] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AbpFeatures] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantId] int NULL,
    [Name] nvarchar(128) NOT NULL,
    [Value] nvarchar(2000) NOT NULL,
    [Discriminator] nvarchar(21) NOT NULL,
    [EditionId] int NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    CONSTRAINT [PK_AbpFeatures] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AbpFeatures_AbpEditions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [AbpEditions] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AbpEntityChanges] (
    [Id] bigint NOT NULL IDENTITY,
    [ChangeTime] datetime2 NOT NULL,
    [ChangeType] tinyint NOT NULL,
    [EntityChangeSetId] bigint NOT NULL,
    [EntityId] nvarchar(48) NULL,
    [EntityTypeFullName] nvarchar(192) NULL,
    [TenantId] int NULL,
    CONSTRAINT [PK_AbpEntityChanges] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AbpEntityChanges_AbpEntityChangeSets_EntityChangeSetId] FOREIGN KEY ([EntityChangeSetId]) REFERENCES [AbpEntityChangeSets] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AbpRoles] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    [TenantId] int NULL,
    [Name] nvarchar(32) NOT NULL,
    [DisplayName] nvarchar(64) NOT NULL,
    [IsStatic] bit NOT NULL,
    [IsDefault] bit NOT NULL,
    [NormalizedName] nvarchar(32) NOT NULL,
    [ConcurrencyStamp] nvarchar(128) NULL,
    CONSTRAINT [PK_AbpRoles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AbpRoles_AbpUsers_CreatorUserId] FOREIGN KEY ([CreatorUserId]) REFERENCES [AbpUsers] ([Id]),
    CONSTRAINT [FK_AbpRoles_AbpUsers_DeleterUserId] FOREIGN KEY ([DeleterUserId]) REFERENCES [AbpUsers] ([Id]),
    CONSTRAINT [FK_AbpRoles_AbpUsers_LastModifierUserId] FOREIGN KEY ([LastModifierUserId]) REFERENCES [AbpUsers] ([Id])
);
GO

CREATE TABLE [AbpSettings] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantId] int NULL,
    [UserId] bigint NULL,
    [Name] nvarchar(256) NOT NULL,
    [Value] nvarchar(max) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    CONSTRAINT [PK_AbpSettings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AbpSettings_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AbpUsers] ([Id])
);
GO

CREATE TABLE [AbpTenants] (
    [Id] int NOT NULL IDENTITY,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    [TenancyName] nvarchar(64) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [ConnectionString] nvarchar(1024) NULL,
    [IsActive] bit NOT NULL,
    [EditionId] int NULL,
    CONSTRAINT [PK_AbpTenants] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AbpTenants_AbpEditions_EditionId] FOREIGN KEY ([EditionId]) REFERENCES [AbpEditions] ([Id]),
    CONSTRAINT [FK_AbpTenants_AbpUsers_CreatorUserId] FOREIGN KEY ([CreatorUserId]) REFERENCES [AbpUsers] ([Id]),
    CONSTRAINT [FK_AbpTenants_AbpUsers_DeleterUserId] FOREIGN KEY ([DeleterUserId]) REFERENCES [AbpUsers] ([Id]),
    CONSTRAINT [FK_AbpTenants_AbpUsers_LastModifierUserId] FOREIGN KEY ([LastModifierUserId]) REFERENCES [AbpUsers] ([Id])
);
GO

CREATE TABLE [AbpUserClaims] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantId] int NULL,
    [UserId] bigint NOT NULL,
    [ClaimType] nvarchar(256) NULL,
    [ClaimValue] nvarchar(max) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    CONSTRAINT [PK_AbpUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AbpUserClaims_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AbpUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AbpUserLogins] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantId] int NULL,
    [UserId] bigint NOT NULL,
    [LoginProvider] nvarchar(128) NOT NULL,
    [ProviderKey] nvarchar(256) NOT NULL,
    CONSTRAINT [PK_AbpUserLogins] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AbpUserLogins_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AbpUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AbpUserRoles] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantId] int NULL,
    [UserId] bigint NOT NULL,
    [RoleId] int NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    CONSTRAINT [PK_AbpUserRoles] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AbpUserRoles_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AbpUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AbpUserTokens] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantId] int NULL,
    [UserId] bigint NOT NULL,
    [LoginProvider] nvarchar(128) NULL,
    [Name] nvarchar(128) NULL,
    [Value] nvarchar(512) NULL,
    [ExpireDate] datetime2 NULL,
    CONSTRAINT [PK_AbpUserTokens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AbpUserTokens_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AbpUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Notifications] (
    [Id] int NOT NULL IDENTITY,
    [UserId] bigint NULL,
    [IsRead] bit NOT NULL,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [SourceId] int NULL,
    [SourceType] nvarchar(max) NULL,
    [CreatorUserId] bigint NULL,
    [CreationTime] datetime2 NOT NULL,
    [LastModifierUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [Link] nvarchar(max) NULL,
    [TaskGroupId] int NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [Status] int NULL,
    CONSTRAINT [PK_Notifications] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Notifications_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AbpUsers] ([Id])
);
GO

CREATE TABLE [UserSignalrConnections] (
    [Id] int NOT NULL IDENTITY,
    [ConnectionId] nvarchar(max) NULL,
    [UserId] bigint NOT NULL,
    [Platform] nvarchar(max) NULL,
    [ConnectionStartTime] datetime2 NOT NULL,
    CONSTRAINT [PK_UserSignalrConnections] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserSignalrConnections_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AbpUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AbpWebhookSendAttempts] (
    [Id] uniqueidentifier NOT NULL,
    [WebhookEventId] uniqueidentifier NOT NULL,
    [WebhookSubscriptionId] uniqueidentifier NOT NULL,
    [Response] nvarchar(max) NULL,
    [ResponseStatusCode] int NULL,
    [CreationTime] datetime2 NOT NULL,
    [LastModificationTime] datetime2 NULL,
    [TenantId] int NULL,
    CONSTRAINT [PK_AbpWebhookSendAttempts] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AbpWebhookSendAttempts_AbpWebhookEvents_WebhookEventId] FOREIGN KEY ([WebhookEventId]) REFERENCES [AbpWebhookEvents] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AuditLogs] (
    [Id] int NOT NULL IDENTITY,
    [UserId] bigint NOT NULL,
    [TimeStamp] datetime2 NOT NULL,
    [EntityId] int NOT NULL,
    [EntityType] nvarchar(max) NULL,
    [AuditTypeId] int NOT NULL,
    CONSTRAINT [PK_AuditLogs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AuditLogs_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AbpUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AuditLogs_AuditTypes_AuditTypeId] FOREIGN KEY ([AuditTypeId]) REFERENCES [AuditTypes] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [CustomerDocuments] (
    [Id] int NOT NULL IDENTITY,
    [OriginalFileName] nvarchar(max) NULL,
    [SavedFileName] nvarchar(max) NULL,
    [FilePath] nvarchar(max) NULL,
    [FileType] nvarchar(max) NULL,
    [FolderName] nvarchar(max) NULL,
    [ExpiryDate] datetime2 NULL,
    [CustomerInfoId] int NOT NULL,
    CONSTRAINT [PK_CustomerDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CustomerDocuments_CustomerInformations_CustomerInfoId] FOREIGN KEY ([CustomerInfoId]) REFERENCES [CustomerInformations] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserNotificationGroup] (
    [Id] int NOT NULL IDENTITY,
    [NotificationGroupId] int NOT NULL,
    [UserId] bigint NOT NULL,
    [CreatorUserId] bigint NULL,
    [CreationTime] datetime2 NOT NULL,
    [LastModifierUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    CONSTRAINT [PK_UserNotificationGroup] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserNotificationGroup_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AbpUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserNotificationGroup_NotificationGroups_NotificationGroupId] FOREIGN KEY ([NotificationGroupId]) REFERENCES [NotificationGroups] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Countries] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [CountryCode] nvarchar(max) NULL,
    [CountryShortCode] nvarchar(max) NULL,
    [PhoneCode] nvarchar(max) NULL,
    [Capital] nvarchar(max) NULL,
    [Currency] nvarchar(max) NULL,
    [CurrencyName] nvarchar(max) NULL,
    [CurrencySymbol] nvarchar(max) NULL,
    [Latitude] float NOT NULL,
    [Longitude] float NOT NULL,
    [RegionId] int NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Countries_Regions_RegionId] FOREIGN KEY ([RegionId]) REFERENCES [Regions] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [AbpDynamicEntityPropertyValues] (
    [Id] bigint NOT NULL IDENTITY,
    [Value] nvarchar(max) NOT NULL,
    [EntityId] nvarchar(max) NULL,
    [DynamicEntityPropertyId] int NOT NULL,
    [TenantId] int NULL,
    CONSTRAINT [PK_AbpDynamicEntityPropertyValues] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AbpDynamicEntityPropertyValues_AbpDynamicEntityProperties_DynamicEntityPropertyId] FOREIGN KEY ([DynamicEntityPropertyId]) REFERENCES [AbpDynamicEntityProperties] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AbpEntityPropertyChanges] (
    [Id] bigint NOT NULL IDENTITY,
    [EntityChangeId] bigint NOT NULL,
    [NewValue] nvarchar(512) NULL,
    [OriginalValue] nvarchar(512) NULL,
    [PropertyName] nvarchar(96) NULL,
    [PropertyTypeFullName] nvarchar(192) NULL,
    [TenantId] int NULL,
    [NewValueHash] nvarchar(max) NULL,
    [OriginalValueHash] nvarchar(max) NULL,
    CONSTRAINT [PK_AbpEntityPropertyChanges] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AbpEntityPropertyChanges_AbpEntityChanges_EntityChangeId] FOREIGN KEY ([EntityChangeId]) REFERENCES [AbpEntityChanges] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AbpPermissions] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantId] int NULL,
    [Name] nvarchar(128) NOT NULL,
    [IsGranted] bit NOT NULL,
    [Discriminator] nvarchar(21) NOT NULL,
    [RoleId] int NULL,
    [UserId] bigint NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    CONSTRAINT [PK_AbpPermissions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AbpPermissions_AbpRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AbpRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AbpPermissions_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AbpUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AbpRoleClaims] (
    [Id] bigint NOT NULL IDENTITY,
    [TenantId] int NULL,
    [RoleId] int NOT NULL,
    [ClaimType] nvarchar(256) NULL,
    [ClaimValue] nvarchar(max) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    CONSTRAINT [PK_AbpRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AbpRoleClaims_AbpRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AbpRoles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [RegistrationTypes] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [CountryId] int NULL,
    CONSTRAINT [PK_RegistrationTypes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RegistrationTypes_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Countries] ([Id])
);
GO

CREATE TABLE [States] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [Latitude] float NOT NULL,
    [Longitude] float NOT NULL,
    [CountryId] int NOT NULL,
    CONSTRAINT [PK_States] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_States_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Countries] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Cities] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [Latitude] float NOT NULL,
    [Longitude] float NOT NULL,
    [CountryId] int NOT NULL,
    [StateId] int NOT NULL,
    CONSTRAINT [PK_Cities] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Cities_Countries_CountryId] FOREIGN KEY ([CountryId]) REFERENCES [Countries] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Cities_States_StateId] FOREIGN KEY ([StateId]) REFERENCES [States] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_AbpAuditLogs_TenantId_ExecutionDuration] ON [AbpAuditLogs] ([TenantId], [ExecutionDuration]);
GO

CREATE INDEX [IX_AbpAuditLogs_TenantId_ExecutionTime] ON [AbpAuditLogs] ([TenantId], [ExecutionTime]);
GO

CREATE INDEX [IX_AbpAuditLogs_TenantId_UserId] ON [AbpAuditLogs] ([TenantId], [UserId]);
GO

CREATE INDEX [IX_AbpBackgroundJobs_IsAbandoned_NextTryTime] ON [AbpBackgroundJobs] ([IsAbandoned], [NextTryTime]);
GO

CREATE INDEX [IX_AbpDynamicEntityProperties_DynamicPropertyId] ON [AbpDynamicEntityProperties] ([DynamicPropertyId]);
GO

CREATE UNIQUE INDEX [IX_AbpDynamicEntityProperties_EntityFullName_DynamicPropertyId_TenantId] ON [AbpDynamicEntityProperties] ([EntityFullName], [DynamicPropertyId], [TenantId]) WHERE [EntityFullName] IS NOT NULL AND [TenantId] IS NOT NULL;
GO

CREATE INDEX [IX_AbpDynamicEntityPropertyValues_DynamicEntityPropertyId] ON [AbpDynamicEntityPropertyValues] ([DynamicEntityPropertyId]);
GO

CREATE UNIQUE INDEX [IX_AbpDynamicProperties_PropertyName_TenantId] ON [AbpDynamicProperties] ([PropertyName], [TenantId]) WHERE [PropertyName] IS NOT NULL AND [TenantId] IS NOT NULL;
GO

CREATE INDEX [IX_AbpDynamicPropertyValues_DynamicPropertyId] ON [AbpDynamicPropertyValues] ([DynamicPropertyId]);
GO

CREATE INDEX [IX_AbpEntityChanges_EntityChangeSetId] ON [AbpEntityChanges] ([EntityChangeSetId]);
GO

CREATE INDEX [IX_AbpEntityChanges_EntityTypeFullName_EntityId] ON [AbpEntityChanges] ([EntityTypeFullName], [EntityId]);
GO

CREATE INDEX [IX_AbpEntityChangeSets_TenantId_CreationTime] ON [AbpEntityChangeSets] ([TenantId], [CreationTime]);
GO

CREATE INDEX [IX_AbpEntityChangeSets_TenantId_Reason] ON [AbpEntityChangeSets] ([TenantId], [Reason]);
GO

CREATE INDEX [IX_AbpEntityChangeSets_TenantId_UserId] ON [AbpEntityChangeSets] ([TenantId], [UserId]);
GO

CREATE INDEX [IX_AbpEntityPropertyChanges_EntityChangeId] ON [AbpEntityPropertyChanges] ([EntityChangeId]);
GO

CREATE INDEX [IX_AbpFeatures_EditionId_Name] ON [AbpFeatures] ([EditionId], [Name]);
GO

CREATE INDEX [IX_AbpFeatures_TenantId_Name] ON [AbpFeatures] ([TenantId], [Name]);
GO

CREATE INDEX [IX_AbpLanguages_TenantId_Name] ON [AbpLanguages] ([TenantId], [Name]);
GO

CREATE INDEX [IX_AbpLanguageTexts_TenantId_Source_LanguageName_Key] ON [AbpLanguageTexts] ([TenantId], [Source], [LanguageName], [Key]);
GO

CREATE INDEX [IX_AbpNotificationSubscriptions_NotificationName_EntityTypeName_EntityId_UserId] ON [AbpNotificationSubscriptions] ([NotificationName], [EntityTypeName], [EntityId], [UserId]);
GO

CREATE INDEX [IX_AbpNotificationSubscriptions_TenantId_NotificationName_EntityTypeName_EntityId_UserId] ON [AbpNotificationSubscriptions] ([TenantId], [NotificationName], [EntityTypeName], [EntityId], [UserId]);
GO

CREATE INDEX [IX_AbpOrganizationUnitRoles_TenantId_OrganizationUnitId] ON [AbpOrganizationUnitRoles] ([TenantId], [OrganizationUnitId]);
GO

CREATE INDEX [IX_AbpOrganizationUnitRoles_TenantId_RoleId] ON [AbpOrganizationUnitRoles] ([TenantId], [RoleId]);
GO

CREATE INDEX [IX_AbpOrganizationUnits_ParentId] ON [AbpOrganizationUnits] ([ParentId]);
GO

CREATE INDEX [IX_AbpOrganizationUnits_TenantId_Code] ON [AbpOrganizationUnits] ([TenantId], [Code]);
GO

CREATE INDEX [IX_AbpPermissions_RoleId] ON [AbpPermissions] ([RoleId]);
GO

CREATE INDEX [IX_AbpPermissions_TenantId_Name] ON [AbpPermissions] ([TenantId], [Name]);
GO

CREATE INDEX [IX_AbpPermissions_UserId] ON [AbpPermissions] ([UserId]);
GO

CREATE INDEX [IX_AbpRoleClaims_RoleId] ON [AbpRoleClaims] ([RoleId]);
GO

CREATE INDEX [IX_AbpRoleClaims_TenantId_ClaimType] ON [AbpRoleClaims] ([TenantId], [ClaimType]);
GO

CREATE INDEX [IX_AbpRoles_CreatorUserId] ON [AbpRoles] ([CreatorUserId]);
GO

CREATE INDEX [IX_AbpRoles_DeleterUserId] ON [AbpRoles] ([DeleterUserId]);
GO

CREATE INDEX [IX_AbpRoles_LastModifierUserId] ON [AbpRoles] ([LastModifierUserId]);
GO

CREATE INDEX [IX_AbpRoles_TenantId_NormalizedName] ON [AbpRoles] ([TenantId], [NormalizedName]);
GO

CREATE UNIQUE INDEX [IX_AbpSettings_TenantId_Name_UserId] ON [AbpSettings] ([TenantId], [Name], [UserId]);
GO

CREATE INDEX [IX_AbpSettings_UserId] ON [AbpSettings] ([UserId]);
GO

CREATE INDEX [IX_AbpTenantNotifications_TenantId] ON [AbpTenantNotifications] ([TenantId]);
GO

CREATE INDEX [IX_AbpTenants_CreatorUserId] ON [AbpTenants] ([CreatorUserId]);
GO

CREATE INDEX [IX_AbpTenants_DeleterUserId] ON [AbpTenants] ([DeleterUserId]);
GO

CREATE INDEX [IX_AbpTenants_EditionId] ON [AbpTenants] ([EditionId]);
GO

CREATE INDEX [IX_AbpTenants_LastModifierUserId] ON [AbpTenants] ([LastModifierUserId]);
GO

CREATE INDEX [IX_AbpTenants_TenancyName] ON [AbpTenants] ([TenancyName]);
GO

CREATE INDEX [IX_AbpUserAccounts_EmailAddress] ON [AbpUserAccounts] ([EmailAddress]);
GO

CREATE INDEX [IX_AbpUserAccounts_TenantId_EmailAddress] ON [AbpUserAccounts] ([TenantId], [EmailAddress]);
GO

CREATE INDEX [IX_AbpUserAccounts_TenantId_UserId] ON [AbpUserAccounts] ([TenantId], [UserId]);
GO

CREATE INDEX [IX_AbpUserAccounts_TenantId_UserName] ON [AbpUserAccounts] ([TenantId], [UserName]);
GO

CREATE INDEX [IX_AbpUserAccounts_UserName] ON [AbpUserAccounts] ([UserName]);
GO

CREATE INDEX [IX_AbpUserClaims_TenantId_ClaimType] ON [AbpUserClaims] ([TenantId], [ClaimType]);
GO

CREATE INDEX [IX_AbpUserClaims_UserId] ON [AbpUserClaims] ([UserId]);
GO

CREATE INDEX [IX_AbpUserLoginAttempts_TenancyName_UserNameOrEmailAddress_Result] ON [AbpUserLoginAttempts] ([TenancyName], [UserNameOrEmailAddress], [Result]);
GO

CREATE INDEX [IX_AbpUserLoginAttempts_UserId_TenantId] ON [AbpUserLoginAttempts] ([UserId], [TenantId]);
GO

CREATE UNIQUE INDEX [IX_AbpUserLogins_ProviderKey_TenantId] ON [AbpUserLogins] ([ProviderKey], [TenantId]) WHERE [TenantId] IS NOT NULL;
GO

CREATE INDEX [IX_AbpUserLogins_TenantId_LoginProvider_ProviderKey] ON [AbpUserLogins] ([TenantId], [LoginProvider], [ProviderKey]);
GO

CREATE INDEX [IX_AbpUserLogins_TenantId_UserId] ON [AbpUserLogins] ([TenantId], [UserId]);
GO

CREATE INDEX [IX_AbpUserLogins_UserId] ON [AbpUserLogins] ([UserId]);
GO

CREATE INDEX [IX_AbpUserNotifications_UserId_State_CreationTime] ON [AbpUserNotifications] ([UserId], [State], [CreationTime]);
GO

CREATE INDEX [IX_AbpUserOrganizationUnits_TenantId_OrganizationUnitId] ON [AbpUserOrganizationUnits] ([TenantId], [OrganizationUnitId]);
GO

CREATE INDEX [IX_AbpUserOrganizationUnits_TenantId_UserId] ON [AbpUserOrganizationUnits] ([TenantId], [UserId]);
GO

CREATE INDEX [IX_AbpUserRoles_TenantId_RoleId] ON [AbpUserRoles] ([TenantId], [RoleId]);
GO

CREATE INDEX [IX_AbpUserRoles_TenantId_UserId] ON [AbpUserRoles] ([TenantId], [UserId]);
GO

CREATE INDEX [IX_AbpUserRoles_UserId] ON [AbpUserRoles] ([UserId]);
GO

CREATE INDEX [IX_AbpUsers_CreatorUserId] ON [AbpUsers] ([CreatorUserId]);
GO

CREATE INDEX [IX_AbpUsers_DeleterUserId] ON [AbpUsers] ([DeleterUserId]);
GO

CREATE INDEX [IX_AbpUsers_LastModifierUserId] ON [AbpUsers] ([LastModifierUserId]);
GO

CREATE INDEX [IX_AbpUsers_TenantId_NormalizedEmailAddress] ON [AbpUsers] ([TenantId], [NormalizedEmailAddress]);
GO

CREATE INDEX [IX_AbpUsers_TenantId_NormalizedUserName] ON [AbpUsers] ([TenantId], [NormalizedUserName]);
GO

CREATE INDEX [IX_AbpUserTokens_TenantId_UserId] ON [AbpUserTokens] ([TenantId], [UserId]);
GO

CREATE INDEX [IX_AbpUserTokens_UserId] ON [AbpUserTokens] ([UserId]);
GO

CREATE INDEX [IX_AbpWebhookSendAttempts_WebhookEventId] ON [AbpWebhookSendAttempts] ([WebhookEventId]);
GO

CREATE INDEX [IX_AuditLogs_AuditTypeId] ON [AuditLogs] ([AuditTypeId]);
GO

CREATE INDEX [IX_AuditLogs_UserId] ON [AuditLogs] ([UserId]);
GO

CREATE INDEX [IX_Cities_CountryId] ON [Cities] ([CountryId]);
GO

CREATE INDEX [IX_Cities_StateId] ON [Cities] ([StateId]);
GO

CREATE INDEX [IX_Countries_RegionId] ON [Countries] ([RegionId]);
GO

CREATE INDEX [IX_CustomerDocuments_CustomerInfoId] ON [CustomerDocuments] ([CustomerInfoId]);
GO

CREATE INDEX [IX_Notifications_UserId] ON [Notifications] ([UserId]);
GO

CREATE INDEX [IX_RegistrationTypes_CountryId] ON [RegistrationTypes] ([CountryId]);
GO

CREATE INDEX [IX_States_CountryId] ON [States] ([CountryId]);
GO

CREATE INDEX [IX_UserNotificationGroup_NotificationGroupId] ON [UserNotificationGroup] ([NotificationGroupId]);
GO

CREATE INDEX [IX_UserNotificationGroup_UserId] ON [UserNotificationGroup] ([UserId]);
GO

CREATE INDEX [IX_UserSignalrConnections_UserId] ON [UserSignalrConnections] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250507070649_v1', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [CustomerDocuments] DROP CONSTRAINT [FK_CustomerDocuments_CustomerInformations_CustomerInfoId];
GO

DROP INDEX [IX_CustomerDocuments_CustomerInfoId] ON [CustomerDocuments];
GO

ALTER TABLE [CustomerDocuments] ADD [CustomerInformationId] int NULL;
GO

CREATE INDEX [IX_CustomerDocuments_CustomerInformationId] ON [CustomerDocuments] ([CustomerInformationId]);
GO

ALTER TABLE [CustomerDocuments] ADD CONSTRAINT [FK_CustomerDocuments_CustomerInformations_CustomerInformationId] FOREIGN KEY ([CustomerInformationId]) REFERENCES [CustomerInformations] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250507074205_v2', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [CustomerDocuments] DROP CONSTRAINT [FK_CustomerDocuments_CustomerInformations_CustomerInformationId];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerDocuments]') AND [c].[name] = N'CustomerInfoId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [CustomerDocuments] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [CustomerDocuments] DROP COLUMN [CustomerInfoId];
GO

DROP INDEX [IX_CustomerDocuments_CustomerInformationId] ON [CustomerDocuments];
DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerDocuments]') AND [c].[name] = N'CustomerInformationId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [CustomerDocuments] DROP CONSTRAINT [' + @var1 + '];');
UPDATE [CustomerDocuments] SET [CustomerInformationId] = 0 WHERE [CustomerInformationId] IS NULL;
ALTER TABLE [CustomerDocuments] ALTER COLUMN [CustomerInformationId] int NOT NULL;
ALTER TABLE [CustomerDocuments] ADD DEFAULT 0 FOR [CustomerInformationId];
CREATE INDEX [IX_CustomerDocuments_CustomerInformationId] ON [CustomerDocuments] ([CustomerInformationId]);
GO

ALTER TABLE [CustomerDocuments] ADD CONSTRAINT [FK_CustomerDocuments_CustomerInformations_CustomerInformationId] FOREIGN KEY ([CustomerInformationId]) REFERENCES [CustomerInformations] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250507080836_v3', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [CustomerInformations] ADD [CityName] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [CustomerInformations] ADD [CountryName] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [CustomerInformations] ADD [CreationTime] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [CustomerInformations] ADD [CreatorUserId] bigint NULL;
GO

ALTER TABLE [CustomerInformations] ADD [DeleterUserId] bigint NULL;
GO

ALTER TABLE [CustomerInformations] ADD [DeletionTime] datetime2 NULL;
GO

ALTER TABLE [CustomerInformations] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [CustomerInformations] ADD [LastModificationTime] datetime2 NULL;
GO

ALTER TABLE [CustomerInformations] ADD [LastModifierUserId] bigint NULL;
GO

ALTER TABLE [CustomerInformations] ADD [RegionName] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [CustomerInformations] ADD [StateName] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [CustomerDocuments] ADD [CreationTime] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [CustomerDocuments] ADD [CreatorUserId] bigint NULL;
GO

ALTER TABLE [CustomerDocuments] ADD [DeleterUserId] bigint NULL;
GO

ALTER TABLE [CustomerDocuments] ADD [DeletionTime] datetime2 NULL;
GO

ALTER TABLE [CustomerDocuments] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [CustomerDocuments] ADD [LastModificationTime] datetime2 NULL;
GO

ALTER TABLE [CustomerDocuments] ADD [LastModifierUserId] bigint NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250507124849_v4', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerInformations]') AND [c].[name] = N'StateName');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [CustomerInformations] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [CustomerInformations] ALTER COLUMN [StateName] nvarchar(max) NULL;
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerInformations]') AND [c].[name] = N'RegionName');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [CustomerInformations] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [CustomerInformations] ALTER COLUMN [RegionName] nvarchar(max) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerInformations]') AND [c].[name] = N'CountryName');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [CustomerInformations] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [CustomerInformations] ALTER COLUMN [CountryName] nvarchar(max) NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerInformations]') AND [c].[name] = N'CityName');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [CustomerInformations] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [CustomerInformations] ALTER COLUMN [CityName] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250507124952_v5', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [RegistrationTypes] ADD [Regex] nvarchar(max) NULL;
GO

ALTER TABLE [CustomerInformations] ADD [BusinessType] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250512065143_v6', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [CustomerInformations] ADD [CompanyLogo] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250513113125_v7', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [CustomerDocuments] ADD [FileSize] bigint NOT NULL DEFAULT CAST(0 AS bigint);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250513113936_v8', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerInformations]') AND [c].[name] = N'CityName');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [CustomerInformations] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [CustomerInformations] DROP COLUMN [CityName];
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerInformations]') AND [c].[name] = N'CountryName');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [CustomerInformations] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [CustomerInformations] DROP COLUMN [CountryName];
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerInformations]') AND [c].[name] = N'RegionName');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [CustomerInformations] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [CustomerInformations] DROP COLUMN [RegionName];
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerInformations]') AND [c].[name] = N'RegistrationType');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [CustomerInformations] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [CustomerInformations] DROP COLUMN [RegistrationType];
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerInformations]') AND [c].[name] = N'StateName');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [CustomerInformations] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [CustomerInformations] DROP COLUMN [StateName];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250513120103_v9', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Units] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_Units] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SizeUnits] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [UnitId] int NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_SizeUnits] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SizeUnits_Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [Units] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [WeightUnit] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [UnitId] int NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_WeightUnit] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_WeightUnit_Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [Units] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_SizeUnits_UnitId] ON [SizeUnits] ([UnitId]);
GO

CREATE INDEX [IX_WeightUnit_UnitId] ON [WeightUnit] ([UnitId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250514062713_v10', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [WeightUnit] DROP CONSTRAINT [FK_WeightUnit_Units_UnitId];
GO

ALTER TABLE [WeightUnit] DROP CONSTRAINT [PK_WeightUnit];
GO

EXEC sp_rename N'[WeightUnit]', N'WeightUnits';
GO

EXEC sp_rename N'[WeightUnits].[IX_WeightUnit_UnitId]', N'IX_WeightUnits_UnitId', N'INDEX';
GO

ALTER TABLE [WeightUnits] ADD CONSTRAINT [PK_WeightUnits] PRIMARY KEY ([Id]);
GO

ALTER TABLE [WeightUnits] ADD CONSTRAINT [FK_WeightUnits_Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [Units] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250514062922_v11', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [WeightUnits] ADD [Description] nvarchar(max) NULL;
GO

ALTER TABLE [Units] ADD [Description] nvarchar(max) NULL;
GO

ALTER TABLE [SizeUnits] ADD [Description] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250514074206_v12', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [SizeUnits];
GO

DROP TABLE [WeightUnits];
GO

ALTER TABLE [Units] ADD [Enum] nvarchar(max) NULL;
GO

ALTER TABLE [Units] ADD [IsActive] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Units] ADD [UnitTypeId] int NOT NULL DEFAULT 0;
GO

CREATE TABLE [UnitTypes] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    [Enum] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_UnitTypes] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_Units_UnitTypeId] ON [Units] ([UnitTypeId]);
GO

ALTER TABLE [Units] ADD CONSTRAINT [FK_Units_UnitTypes_UnitTypeId] FOREIGN KEY ([UnitTypeId]) REFERENCES [UnitTypes] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250516124138_v13', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [DistributionTypes] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [IsActive] bit NOT NULL,
    [Enum] nvarchar(max) NULL,
    CONSTRAINT [PK_DistributionTypes] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250521072741_v14', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [DistributionTypes] ADD [CreationTime] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
GO

ALTER TABLE [DistributionTypes] ADD [CreatorUserId] bigint NULL;
GO

ALTER TABLE [DistributionTypes] ADD [DeleterUserId] bigint NULL;
GO

ALTER TABLE [DistributionTypes] ADD [DeletionTime] datetime2 NULL;
GO

ALTER TABLE [DistributionTypes] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [DistributionTypes] ADD [LastModificationTime] datetime2 NULL;
GO

ALTER TABLE [DistributionTypes] ADD [LastModifierUserId] bigint NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250521072923_v15', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [DistributionTypes] ADD [TenantId] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250521074533_v16', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[DistributionTypes]') AND [c].[name] = N'Enum');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [DistributionTypes] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [DistributionTypes] DROP COLUMN [Enum];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250522071152_v17', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerDocuments]') AND [c].[name] = N'FolderName');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [CustomerDocuments] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [CustomerDocuments] DROP COLUMN [FolderName];
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AbpUsers]') AND [c].[name] = N'NIDNumber');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [AbpUsers] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [AbpUsers] DROP COLUMN [NIDNumber];
GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AbpUsers]') AND [c].[name] = N'Rank');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [AbpUsers] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [AbpUsers] DROP COLUMN [Rank];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250604063541_v18', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250613071855_v20', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [Attachments];
GO

CREATE TABLE [UserProfileImages] (
    [Id] int NOT NULL IDENTITY,
    [OriginalFileName] nvarchar(max) NULL,
    [SavedFileName] nvarchar(max) NULL,
    [FilePath] nvarchar(max) NULL,
    [FileType] nvarchar(max) NULL,
    [FileSize] bigint NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_UserProfileImages] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250613095110_v21', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AbpUsers] ADD [UserProfileImageId] int NULL;
GO

CREATE INDEX [IX_AbpUsers_UserProfileImageId] ON [AbpUsers] ([UserProfileImageId]);
GO

ALTER TABLE [AbpUsers] ADD CONSTRAINT [FK_AbpUsers_UserProfileImages_UserProfileImageId] FOREIGN KEY ([UserProfileImageId]) REFERENCES [UserProfileImages] ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250613095259_v22', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [UserProfileImages] ADD [UserId] int NOT NULL DEFAULT 0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250613095454_v23', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [UserProfileImages] (
    [Id] int NOT NULL IDENTITY,
    [OriginalFileName] nvarchar(max) NULL,
    [SavedFileName] nvarchar(max) NULL,
    [FilePath] nvarchar(max) NULL,
    [FileType] nvarchar(max) NULL,
    [FileSize] bigint NOT NULL,
    [UserId] bigint NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_UserProfileImages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserProfileImages_AbpUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AbpUsers] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_UserProfileImages_UserId] ON [UserProfileImages] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250613103740_v25', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [SystemDistributionCenters] (
    [Id] int NOT NULL IDENTITY,
    [RegionId] int NOT NULL,
    [CountryId] int NOT NULL,
    [CityId] int NOT NULL,
    [IsActive] bit NOT NULL,
    [TenantId] int NOT NULL,
    [Name] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    [PlusCodes] nvarchar(max) NULL,
    [Latitude] float NULL,
    [Longitude] float NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_SystemDistributionCenters] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250616103956_v26', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [CustomerDistributionCenters] (
    [Id] int NOT NULL IDENTITY,
    [RegionId] int NOT NULL,
    [CountryId] int NOT NULL,
    [CityId] int NOT NULL,
    [IsActive] bit NOT NULL,
    [TenantId] int NOT NULL,
    [Name] nvarchar(max) NULL,
    [Address] nvarchar(max) NULL,
    [PlusCodes] nvarchar(max) NULL,
    [Latitude] float NULL,
    [Longitude] float NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_CustomerDistributionCenters] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250616121418_v27', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SystemDistributionCenters] ADD [Enum] nvarchar(max) NULL;
GO

ALTER TABLE [CustomerDistributionCenters] ADD [Enum] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250616121617_v28', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SystemDistributionCenters]') AND [c].[name] = N'Longitude');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [SystemDistributionCenters] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [SystemDistributionCenters] ALTER COLUMN [Longitude] nvarchar(max) NULL;
GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SystemDistributionCenters]') AND [c].[name] = N'Latitude');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [SystemDistributionCenters] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [SystemDistributionCenters] ALTER COLUMN [Latitude] nvarchar(max) NULL;
GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerDistributionCenters]') AND [c].[name] = N'Longitude');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [CustomerDistributionCenters] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [CustomerDistributionCenters] ALTER COLUMN [Longitude] nvarchar(max) NULL;
GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[CustomerDistributionCenters]') AND [c].[name] = N'Latitude');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [CustomerDistributionCenters] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [CustomerDistributionCenters] ALTER COLUMN [Latitude] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250617134551_v29', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [SystemDistributionCenterDoc] (
    [Id] int NOT NULL IDENTITY,
    [OriginalFileName] nvarchar(max) NULL,
    [SavedFileName] nvarchar(max) NULL,
    [FilePath] nvarchar(max) NULL,
    [FileType] nvarchar(max) NULL,
    [FileSize] bigint NOT NULL,
    [SystemDistributionCenterId] int NOT NULL,
    [CreationTime] datetime2 NOT NULL,
    [CreatorUserId] bigint NULL,
    [LastModificationTime] datetime2 NULL,
    [LastModifierUserId] bigint NULL,
    [IsDeleted] bit NOT NULL,
    [DeleterUserId] bigint NULL,
    [DeletionTime] datetime2 NULL,
    CONSTRAINT [PK_SystemDistributionCenterDoc] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SystemDistributionCenterDoc_SystemDistributionCenters_SystemDistributionCenterId] FOREIGN KEY ([SystemDistributionCenterId]) REFERENCES [SystemDistributionCenters] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_SystemDistributionCenterDoc_SystemDistributionCenterId] ON [SystemDistributionCenterDoc] ([SystemDistributionCenterId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250618052503_v30', N'8.0.5');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [SystemDistributionCenterDoc] DROP CONSTRAINT [FK_SystemDistributionCenterDoc_SystemDistributionCenters_SystemDistributionCenterId];
GO

ALTER TABLE [SystemDistributionCenterDoc] DROP CONSTRAINT [PK_SystemDistributionCenterDoc];
GO

EXEC sp_rename N'[SystemDistributionCenterDoc]', N'SystemDistributionCenterDocs';
GO

EXEC sp_rename N'[SystemDistributionCenterDocs].[IX_SystemDistributionCenterDoc_SystemDistributionCenterId]', N'IX_SystemDistributionCenterDocs_SystemDistributionCenterId', N'INDEX';
GO

ALTER TABLE [SystemDistributionCenterDocs] ADD CONSTRAINT [PK_SystemDistributionCenterDocs] PRIMARY KEY ([Id]);
GO

ALTER TABLE [SystemDistributionCenterDocs] ADD CONSTRAINT [FK_SystemDistributionCenterDocs_SystemDistributionCenters_SystemDistributionCenterId] FOREIGN KEY ([SystemDistributionCenterId]) REFERENCES [SystemDistributionCenters] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250618055149_v31', N'8.0.5');
GO

COMMIT;
GO

