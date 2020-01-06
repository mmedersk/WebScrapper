CREATE DATABASE SCRAPERBASE;

USE SCRAPERBASE
GO

EXEC sp_configure 'show advanced options', 1
GO

RECONFIGURE
GO

EXEC sp_configure'xp_cmdshell', 1  
GO

RECONFIGURE
GO


CREATE TABLE DANE(

	Title				NVARCHAR(255)	NULL,
	Rooms				INT				NULL,
    Area 				NVARCHAR(255)	NULL,
    Price 				NVARCHAR(255)	NULL,
    Bond 				INT				NULL,    
    BuildingType 		NVARCHAR(255)	NULL,
    FloorsInBuilding 	INT				NULL,
	Windows 			NVARCHAR(255)	NULL,
	BuiltIn 			INT				NULL,
    HeatingType 		NVARCHAR(255)	NULL,
    Materials 			NVARCHAR(255)	NULL,
    Floor 				INT				NULL,
    Address 			NVARCHAR(255)	NULL
);

