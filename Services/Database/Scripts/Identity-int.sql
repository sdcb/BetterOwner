PRINT N'正在创建 [dbo].[__EFMigrationsHistory]...';

GO
CREATE TABLE [dbo].[__EFMigrationsHistory] (
    [MigrationId]    NVARCHAR (150) NOT NULL,
    [ProductVersion] NVARCHAR (32)  NOT NULL,
    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId] ASC)
);

GO
PRINT N'正在创建 [dbo].[RoleClaims]...';

GO
CREATE TABLE [dbo].[RoleClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [RoleId]     INT            NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_RoleClaims] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
PRINT N'正在创建 [dbo].[RoleClaims].[IX_RoleClaims_RoleId]...';

GO
CREATE NONCLUSTERED INDEX [IX_RoleClaims_RoleId]
    ON [dbo].[RoleClaims]([RoleId] ASC);

GO
PRINT N'正在创建 [dbo].[Roles]...';

GO
CREATE TABLE [dbo].[Roles] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (256) NULL,
    [NormalizedName]   NVARCHAR (256) NULL,
    [ConcurrencyStamp] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
PRINT N'正在创建 [dbo].[Roles].[RoleNameIndex]...';

GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[Roles]([NormalizedName] ASC) WHERE ([NormalizedName] IS NOT NULL);

GO
PRINT N'正在创建 [dbo].[UserClaims]...';

GO
CREATE TABLE [dbo].[UserClaims] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [UserId]     INT            NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_UserClaims] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
PRINT N'正在创建 [dbo].[UserClaims].[IX_UserClaims_UserId]...';

GO
CREATE NONCLUSTERED INDEX [IX_UserClaims_UserId]
    ON [dbo].[UserClaims]([UserId] ASC);

GO
PRINT N'正在创建 [dbo].[UserLogins]...';

GO
CREATE TABLE [dbo].[UserLogins] (
    [LoginProvider]       NVARCHAR (128) NOT NULL,
    [ProviderKey]         NVARCHAR (128) NOT NULL,
    [ProviderDisplayName] NVARCHAR (MAX) NULL,
    [UserId]              INT            NOT NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC)
);

GO
PRINT N'正在创建 [dbo].[UserLogins].[IX_UserLogins_UserId]...';

GO
CREATE NONCLUSTERED INDEX [IX_UserLogins_UserId]
    ON [dbo].[UserLogins]([UserId] ASC);

GO
PRINT N'正在创建 [dbo].[UserRoles]...';

GO
CREATE TABLE [dbo].[UserRoles] (
    [UserId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC)
);

GO
PRINT N'正在创建 [dbo].[UserRoles].[IX_UserRoles_RoleId]...';

GO
CREATE NONCLUSTERED INDEX [IX_UserRoles_RoleId]
    ON [dbo].[UserRoles]([RoleId] ASC);

GO
PRINT N'正在创建 [dbo].[Users]...';

GO
CREATE TABLE [dbo].[Users] (
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
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
PRINT N'正在创建 [dbo].[Users].[EmailIndex]...';

GO
CREATE NONCLUSTERED INDEX [EmailIndex]
    ON [dbo].[Users]([NormalizedEmail] ASC);

GO
PRINT N'正在创建 [dbo].[Users].[UserNameIndex]...';

GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[Users]([NormalizedUserName] ASC) WHERE ([NormalizedUserName] IS NOT NULL);

GO
PRINT N'正在创建 [dbo].[UserTokens]...';

GO
CREATE TABLE [dbo].[UserTokens] (
    [UserId]        INT            NOT NULL,
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [Name]          NVARCHAR (128) NOT NULL,
    [Value]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_UserTokens] PRIMARY KEY CLUSTERED ([UserId] ASC, [LoginProvider] ASC, [Name] ASC)
);

GO
PRINT N'正在创建 [dbo].[FK_RoleClaims_Roles_RoleId]...';

GO
ALTER TABLE [dbo].[RoleClaims]
    ADD CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]) ON DELETE CASCADE;

GO
PRINT N'正在创建 [dbo].[FK_UserClaims_Users_UserId]...';

GO
ALTER TABLE [dbo].[UserClaims]
    ADD CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE;

GO
PRINT N'正在创建 [dbo].[FK_UserLogins_Users_UserId]...';

GO
ALTER TABLE [dbo].[UserLogins]
    ADD CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE;

GO
PRINT N'正在创建 [dbo].[FK_UserRoles_Roles_RoleId]...';

GO
ALTER TABLE [dbo].[UserRoles]
    ADD CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]) ON DELETE CASCADE;

GO
PRINT N'正在创建 [dbo].[FK_UserRoles_Users_UserId]...';

GO
ALTER TABLE [dbo].[UserRoles]
    ADD CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE;

GO
PRINT N'正在创建 [dbo].[FK_UserTokens_Users_UserId]...';

GO
ALTER TABLE [dbo].[UserTokens]
    ADD CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE;

GO