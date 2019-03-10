PRINT N'正在创建 [dbo].[__EFMigrationsHistory]...';

GO
CREATE TABLE [dbo].[__EFMigrationsHistory] (
    [MigrationId]    NVARCHAR (150) NOT NULL,
    [ProductVersion] NVARCHAR (32)  NOT NULL,
    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId] ASC)
);

GO
PRINT N'正在创建 [dbo].[RoleClaim]...';

GO
CREATE TABLE [dbo].[RoleClaim] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [RoleId]     INT            NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_RoleClaim] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
PRINT N'正在创建 [dbo].[RoleClaim].[IX_RoleClaims_RoleId]...';

GO
CREATE NONCLUSTERED INDEX [IX_RoleClaims_RoleId]
    ON [dbo].[RoleClaim]([RoleId] ASC);

GO
PRINT N'正在创建 [dbo].[Role]...';

GO
CREATE TABLE [dbo].[Role] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (256) NULL,
    [NormalizedName]   NVARCHAR (256) NULL,
    [ConcurrencyStamp] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
PRINT N'正在创建 [dbo].[Role].[RoleNameIndex]...';

GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[Role]([NormalizedName] ASC) WHERE ([NormalizedName] IS NOT NULL);

GO
PRINT N'正在创建 [dbo].[UserClaim]...';

GO
CREATE TABLE [dbo].[UserClaim] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     INT            NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
PRINT N'正在创建 [dbo].[UserClaim].[IX_UserClaims_UserId]...';

GO
CREATE NONCLUSTERED INDEX [IX_UserClaims_UserId]
    ON [dbo].[UserClaim]([UserId] ASC);

GO
PRINT N'正在创建 [dbo].[UserLogin]...';

GO
CREATE TABLE [dbo].[UserLogin] (
    [LoginProvider]       NVARCHAR (128) NOT NULL,
    [ProviderKey]         NVARCHAR (128) NOT NULL,
    [ProviderDisplayName] NVARCHAR (MAX) NULL,
    [UserId]              INT            NOT NULL,
    CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC)
);

GO
PRINT N'正在创建 [dbo].[UserLogin].[IX_UserLogins_UserId]...';

GO
CREATE NONCLUSTERED INDEX [IX_UserLogins_UserId]
    ON [dbo].[UserLogin]([UserId] ASC);

GO
PRINT N'正在创建 [dbo].[UserRole]...';

GO
CREATE TABLE [dbo].[UserRole] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC)
);

GO
PRINT N'正在创建 [dbo].[UserRole].[IX_UserRoles_RoleId]...';

GO
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId]
    ON [dbo].[UserRole]([RoleId] ASC);

GO
PRINT N'正在创建 [dbo].[User]...';

GO
CREATE TABLE [dbo].[User] (
    [Id]                   INT                IDENTITY (1, 1) NOT NULL,
    [UserName]             NVARCHAR (256)     NULL,
    [NormalizedUserName]   NVARCHAR (256)     NULL,
    [Email]                NVARCHAR (256)     NULL,
    [NormalizedEmail]      NVARCHAR (256)     NULL,
    [EmailConfirmed]       BIT                NOT NULL,
    [PasswordHash]         NVARCHAR (MAX)     NULL,
    [SecurityStamp]        NVARCHAR (MAX)     NULL,
    [ConcurrencyStamp]     NVARCHAR (MAX)     NULL,
    [PhoneNumber]          NVARCHAR (MAX)     NULL,
    [PhoneNumberConfirmed] BIT                NOT NULL,
    [TwoFactorEnabled]     BIT                NOT NULL,
    [LockoutEnd]           DATETIMEOFFSET (7) NULL,
    [LockoutEnabled]       BIT                NOT NULL,
    [AccessFailedCount]    INT                NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
PRINT N'正在创建 [dbo].[User].[EmailIndex]...';

GO
CREATE NONCLUSTERED INDEX [EmailIndex]
    ON [dbo].[User]([NormalizedEmail] ASC);

GO
PRINT N'正在创建 [dbo].[User].[UserNameIndex]...';

GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[User]([NormalizedUserName] ASC) WHERE ([NormalizedUserName] IS NOT NULL);

GO
PRINT N'正在创建 [dbo].[UserToken]...';

GO
CREATE TABLE [dbo].[UserToken] (
    [UserId]        INT            NOT NULL,
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [Name]          NVARCHAR (128) NOT NULL,
    [Value]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_UserToken] PRIMARY KEY CLUSTERED ([UserId] ASC, [LoginProvider] ASC, [Name] ASC)
);

GO
PRINT N'正在创建 [dbo].[FK_RoleClaims_Roles_RoleId]...';

GO
ALTER TABLE [dbo].[RoleClaim]
    ADD CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id]) ON DELETE CASCADE;

GO
PRINT N'正在创建 [dbo].[FK_UserClaims_Users_UserId]...';

GO
ALTER TABLE [dbo].[UserClaim]
    ADD CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE;

GO
PRINT N'正在创建 [dbo].[FK_UserLogins_Users_UserId]...';

GO
ALTER TABLE [dbo].[UserLogin]
    ADD CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE;

GO
PRINT N'正在创建 [dbo].[FK_UserRoles_Roles_RoleId]...';

GO
ALTER TABLE [dbo].[UserRole]
    ADD CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id]) ON DELETE CASCADE;

GO
PRINT N'正在创建 [dbo].[FK_UserRoles_Users_UserId]...';

GO
ALTER TABLE [dbo].[UserRole]
    ADD CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE;

GO
PRINT N'正在创建 [dbo].[FK_UserTokens_Users_UserId]...';

GO
ALTER TABLE [dbo].[UserToken]
    ADD CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE;

GO