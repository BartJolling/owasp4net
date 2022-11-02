USE [master]
GO

IF EXISTS (SELECT * FROM sys.server_principals WHERE name = N'readonly')
DROP LOGIN [readonly]
GO

CREATE LOGIN [readonly] WITH PASSWORD=N'readonly', DEFAULT_DATABASE=[Northwind], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO

USE [Northwind]
GO

DROP USER IF EXISTS [readonly]
GO

CREATE USER [readonly] FOR LOGIN [readonly] WITH DEFAULT_SCHEMA=[dbo]
GO

USE [Northwind]
GO

EXEC sp_addrolemember db_datareader, [readonly] 
GO