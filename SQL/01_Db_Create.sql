USE [master]
GO

IF db_id('BarberShop') IS NULL
  CREATE DATABASE [BarberShop]
GO

USE [BarberShop]
GO


DROP TABLE IF EXISTS [UserProfile];
DROP TABLE IF EXISTS [Customer];
DROP TABLE IF EXISTS [UserService];
DROP TABLE IF EXISTS [Service];
DROP TABLE IF EXISTS [Transaction];
DROP TABLE IF EXISTS [TransactionService];
DROP TABLE IF EXISTS [UserType];



CREATE TABLE [UserProfile] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [FirebaseId] nvarchar (28) NOT NULL,
  [FirstName] nvarchar(255) NOT NULL,
  [LastName] nvarchar(255) NOT NULL,
  [CreateDateTime] datetime NOT NULL,
  [DisplayName] nvarchar(255) NOT NULL,
  [Email] nvarchar(255) NOT NULL,
  [UserTypeId] integer NOT NULL,
)
GO
CREATE TABLE [UserType] (
  [Id] integer PRIMARY KEY IDENTITY,
  [Name] nvarchar(20) NOT NULL
)

CREATE TABLE [Customer] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [FirstName] nvarchar(255) NOT NULL,
  [Lastname] nvarchar(255) NOT NULL,
  [PhoneNumber] nvarchar(255) NOT NULL,
  [CreateDateTime] datetime NOT NULL,
  [Email] nvarchar(255) NOT NULL,
  [Address] nvarchar(255) NOT NULL,
  [UserProfileId] int NOT NULL
)
GO

CREATE TABLE [UserService] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [UserProfileId] int NOT NULL,
  [ServiceId] int NOT NULL
)
GO

CREATE TABLE [Service] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL,
  [Cost] int NOT NULL
)
GO

CREATE TABLE [Transaction] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Comment] nvarchar(255) NOT NULL,
  [CustomerId] int NOT NULL,
  [UserProfileId] int NOT NULL,
  [TransactionDate] datetime NOT NULL
)
GO

CREATE TABLE [TransactionService] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [ServiceId] int NOT NULL,
  [TransactionId] int NOT NULL
)
GO

ALTER TABLE [UserService] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id])
GO

ALTER TABLE [UserProfile] ADD FOREIGN KEY ([UserTypeId]) REFERENCES [UserType] ([Id])
GO

ALTER TABLE [UserService] ADD FOREIGN KEY ([ServiceId]) REFERENCES [Service] ([Id])
GO

ALTER TABLE [Transaction] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id]) 
GO

ALTER TABLE [Transaction] ADD FOREIGN KEY ([CustomerId]) REFERENCES [Customer] ([Id]) 
GO

ALTER TABLE [Customer] ADD FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfile] ([Id]) 
GO

ALTER TABLE [TransactionService] ADD FOREIGN KEY ([ServiceId]) REFERENCES [Service] ([Id])
GO

ALTER TABLE [TransactionService] ADD FOREIGN KEY ([TransactionId]) REFERENCES [Transaction] ([Id]) on Delete cascade
GO
