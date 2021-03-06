﻿/*
Deployment script for WicSchool.DB

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "WicSchool.DB"
:setvar DefaultFilePrefix "WicSchool.DB"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Starting rebuilding table [dbo].[Accountants]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Accountants] (
    [Id]         INT        IDENTITY (1, 1) NOT NULL,
    [EmployeeId] INT        NOT NULL,
    [CPANumber]  NCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Accountants])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Accountants] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Accountants] ([Id], [EmployeeId], [CPANumber])
        SELECT   [Id],
                 [EmployeeId],
                 [CPANumber]
        FROM     [dbo].[Accountants]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Accountants] OFF;
    END

DROP TABLE [dbo].[Accountants];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Accountants]', N'Accountants';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[Technicians]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Technicians] (
    [Id]         INT        IDENTITY (1, 1) NOT NULL,
    [EmployeeId] INT        NOT NULL,
    [ACSNumber]  NCHAR (50) NOT NULL,
    [Expire]     DATETIME   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Technicians])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Technicians] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Technicians] ([Id], [EmployeeId], [ACSNumber], [Expire])
        SELECT   [Id],
                 [EmployeeId],
                 [ACSNumber],
                 [Expire]
        FROM     [dbo].[Technicians]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Technicians] OFF;
    END

DROP TABLE [dbo].[Technicians];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Technicians]', N'Technicians';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Update complete.';


GO
