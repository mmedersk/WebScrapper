USE SCRAPERBASE

GO

IF OBJECT_ID('scrap') is not NULL
	DROP TABLE scrap;
GO

CREATE TABLE scrap(

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
 GO

DECLARE @SCRAPING Varchar(MAX)


SELECT @SCRAPING = 
	BulkColumn
	FROM OPENROWSET(BULK'C:\Users\Neron94\Downloads\transformationResults.json', SINGLE_BLOB) JSON



--SELECT @SCRAPING


IF (ISJSON(@SCRAPING) = 1)
	BEGIN
		PRINT 'DATA LOADED';
		
		INSERT INTO scrap
		SELECT *
		FROM OPENJSON(@SCRAPING)
	
		WITH (
			Title				NVARCHAR(255)	'$.Title',
			Rooms				INT				'$.Rooms',
			Area 				NVARCHAR(255)	'$.Area',
			Price 				NVARCHAR(255)	'$.Price',
			Bond 				INT				'$.Bond',    
			BuildingType 		NVARCHAR(255)	'$.BuildingType',
			FloorsInBuilding 	INT				'$.FloorsInBuilding',
			Windows 			NVARCHAR(255)	'$.Windows',
			BuiltIn 			INT				'$.BuiltIn',
			HeatingType 		NVARCHAR(255)	'$.HeatingType',
			Materials 			NVARCHAR(255)	'$.Materials',
			Floor 				INT				'$.Floor',
			Address 			NVARCHAR(255)	'$.Address'	
		);

	MERGE DANE AS TARGET
	USING scrap AS SOURCE
	ON (TARGET.Title = SOURCE.Title)

	WHEN MATCHED AND TARGET.Rooms <> SOURCE.Rooms OR TARGET.Area <> SOURCE.Area OR TARGET.Price <> SOURCE.Price OR TARGET.Bond <> SOURCE.Bond OR TARGET.BuildingType <> SOURCE.BuildingType OR TARGET.FloorsInBuilding <> SOURCE.FloorsInBuilding OR TARGET.Windows <> SOURCE.Windows OR TARGET.BuiltIn <> SOURCE.BuiltIn OR TARGET.HeatingType <> SOURCE.HeatingType OR TARGET.Materials <> SOURCE.Materials OR TARGET.Floor <> SOURCE.Floor OR TARGET.Address <> SOURCE.Address
	THEN UPDATE SET TARGET.Rooms = SOURCE.Rooms, TARGET.Area = SOURCE.Area, TARGET.Price = SOURCE.Price, TARGET.Bond = SOURCE.Bond, TARGET.BuildingType = SOURCE.BuildingType, TARGET.FloorsInBuilding = SOURCE.FloorsInBuilding, TARGET.Windows = SOURCE.Windows, TARGET.BuiltIn = SOURCE.BuiltIn, TARGET.HeatingType = SOURCE.HeatingType, TARGET.Materials = SOURCE.Materials, TARGET.Floor = SOURCE.Floor, TARGET.Address = SOURCE.Address
	WHEN NOT MATCHED BY TARGET
	THEN INSERT (Title,	Rooms, Area, Price, Bond, BuildingType, FloorsInBuilding, Windows, BuiltIn, HeatingType, Materials, Floor, Address ) VALUES (SOURCE.Title, SOURCE.Rooms, SOURCE.Area, SOURCE.Price, SOURCE.Bond, SOURCE.BuildingType, SOURCE.FloorsInBuilding, SOURCE.Windows, SOURCE.BuiltIn, SOURCE.HeatingType, SOURCE.Materials, SOURCE.Floor, SOURCE.Address );


	END

ELSE
	BEGIN
		PRINT 'DATA NOT LOADED';
	END
	
DROP TABLE scrap;

	--SELECT @@SERVERNAME 

	DECLARE @cmd NVARCHAR(1000)
	SET @cmd = 'bcp "select (select Title, Rooms, Area, Price, Bond, BuildingType, FloorsInBuilding, Windows, BuiltIn,HeatingType, Materials, Floor, Address for json path, without_array_wrapper) from DANE" queryout F:\SCRAPER\dane.json -c -d SCRAPERBASE -T'
	EXEC sys.xp_cmdshell @cmd;

	--'bcp SCRAPERBASE.dbo.DANE "select (select Title, Rooms, Area, Price, Bond, BuildingType, FlorsInBuilding, Windows, BuiltIn,HeatingType, Materials, Floor, Adress for json path, without_array_wrapper) from DANE" out F:\SCRAPER\dane.json -c -T'
