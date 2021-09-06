﻿IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Leagues] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [Country] nvarchar(max) NULL,
    CONSTRAINT [PK_Leagues] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Clubs] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [City] nvarchar(max) NULL,
    [DateOfEstableishment] datetime2 NOT NULL,
    [History] nvarchar(max) NULL,
    [LeagueId] int NULL,
    CONSTRAINT [PK_Clubs] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Clubs_Leagues_LeagueId] FOREIGN KEY ([LeagueId]) REFERENCES [Leagues] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Players] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [ClubId] int NULL,
    CONSTRAINT [PK_Players] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Players_Clubs_ClubId] FOREIGN KEY ([ClubId]) REFERENCES [Clubs] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Clubs_LeagueId] ON [Clubs] ([LeagueId]);

GO

CREATE INDEX [IX_Players_ClubId] ON [Players] ([ClubId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200827130227_FirstTime', N'5.0.0-preview.8.20407.4');

GO

