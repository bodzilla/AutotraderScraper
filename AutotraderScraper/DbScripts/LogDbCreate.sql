﻿-- CREATE DB
USE [master]
GO

CREATE DATABASE [AutotraderScraperLog]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AutotraderScraperLog', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\AutotraderScraperLog.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AutotraderScraperLog_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\AutotraderScraperLog_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

ALTER DATABASE [AutotraderScraperLog] SET COMPATIBILITY_LEVEL = 140
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AutotraderScraperLog].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [AutotraderScraperLog] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [AutotraderScraperLog] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [AutotraderScraperLog] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [AutotraderScraperLog] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [AutotraderScraperLog] SET ARITHABORT OFF 
GO

ALTER DATABASE [AutotraderScraperLog] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [AutotraderScraperLog] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [AutotraderScraperLog] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [AutotraderScraperLog] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [AutotraderScraperLog] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [AutotraderScraperLog] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [AutotraderScraperLog] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [AutotraderScraperLog] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [AutotraderScraperLog] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [AutotraderScraperLog] SET  ENABLE_BROKER 
GO

ALTER DATABASE [AutotraderScraperLog] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [AutotraderScraperLog] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [AutotraderScraperLog] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [AutotraderScraperLog] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [AutotraderScraperLog] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [AutotraderScraperLog] SET READ_COMMITTED_SNAPSHOT ON 
GO

ALTER DATABASE [AutotraderScraperLog] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [AutotraderScraperLog] SET RECOVERY FULL 
GO

ALTER DATABASE [AutotraderScraperLog] SET  MULTI_USER 
GO

ALTER DATABASE [AutotraderScraperLog] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [AutotraderScraperLog] SET DB_CHAINING OFF 
GO

ALTER DATABASE [AutotraderScraperLog] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [AutotraderScraperLog] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [AutotraderScraperLog] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [AutotraderScraperLog] SET QUERY_STORE = OFF
GO

USE [AutotraderScraperLog]
GO

ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO

ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO

ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO

ALTER DATABASE [AutotraderScraperLog] SET  READ_WRITE 
GO


-- CREATE LOG TABLE
CREATE TABLE [dbo].[Log] (
    [Id] [int] IDENTITY (1, 1) NOT NULL,
    [DateTime] [datetime] NOT NULL,
    [Level] [varchar] (50) NOT NULL,
    [Logger] [varchar] (255) NOT NULL,
    [Message] [varchar] (4000) NOT NULL,
    [Exception] [varchar] (2000) NULL
)
