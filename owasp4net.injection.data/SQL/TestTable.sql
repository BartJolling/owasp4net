﻿USE [Northwind]
GO

IF OBJECT_ID('dbo.TestTable', 'U') IS NOT NULL 
DROP TABLE [dbo].[TestTable]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[TestTable](
	[Key] [int] NOT NULL,
	[Value] [varchar](50) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO